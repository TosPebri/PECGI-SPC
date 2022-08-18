Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.Data
Imports OfficeOpenXml
Imports DevExpress.Web
Imports DevExpress.XtraGrid.Views.Grid

Public Class ProductionSampleVerificationList
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Dim Factory_Sel As String = "1"
    Dim ItemType_Sel As String = "2"
    Dim Line_Sel As String = "3"
    Dim ItemCheck_Sel As String = "4"
    Private dt As DataTable

    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False

#End Region

#Region "Procedure"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            up_Fillcombo()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "B040")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If

        sGlobal.getMenu("B040")
        Master.SiteTitle = sGlobal.menuName
        show_error(MsgTypeEnum.Info, "", 0)
    End Sub

    Private Sub GridMenu_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridMenu.CustomCallback
        Try
            Dim pAction As String = Split(e.Parameters, "|")(0)
            Dim cls As New clsProductionSampleVerificationList

            If pAction = "Load" Then

                Dim Factory As String = HideValue.Get("FactoryCode")
                Dim Itemtype As String = HideValue.Get("ItemType_Code")
                Dim Line As String = HideValue.Get("LineCode")
                Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
                Dim ProdDateFrom As String = Convert.ToDateTime(dtFromDate.Value).ToString("yyyy-MM-dd")
                Dim ProdDateTo As String = Convert.ToDateTime(dtToDate.Value).ToString("yyyy-MM-dd")
                Dim MKVerification As String = cboMK.Value
                Dim QCVerification As String = cboQC.Value
                Dim msgErr As String = ""

                cls.FactoryCode = Factory
                cls.ItemType_Code = Itemtype
                cls.LineCode = Line
                cls.ItemCheck_Code = ItemCheck
                cls.ProdDateFrom = ProdDateFrom
                cls.ProdDateTo = ProdDateTo
                cls.MKVerification = MKVerification
                cls.QCVerification = QCVerification

                UpGridLoad(cls)
            ElseIf pAction = "Clear" Then
                UpGridLoad(cls)
            End If

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Sub cboFactory_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboFactory.Callback
        Try
            Dim data As New clsProductionSampleVerificationList()
            Dim a As String

            Dim ErrMsg As String = ""
            dt = clsProductionSampleVerificationListDB.FillCombo(Factory_Sel, data, ErrMsg)
            With cboFactory
                .DataSource = dt
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If cboFactory.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboFactory.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("FactoryCode", a)
            data.FactoryCode = HideValue.Get("FactoryCode")

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, ex.Message, 0)
        End Try
    End Sub

    Private Sub cboItemType_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboItemType.Callback
        Try
            Dim data As New clsProductionSampleVerificationList()
            Dim a As String

            Dim ErrMsg As String = ""
            dt = clsProductionSampleVerificationListDB.FillCombo(ItemType_Sel, data, ErrMsg)
            With cboItemType
                .DataSource = dt
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If cboItemType.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboItemType.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("ItemType_Code", a)
            data.ItemType_Code = HideValue.Get("ItemType_Code")

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, ex.Message, 0)
        End Try
    End Sub

    Private Sub cboLineID_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboLineID.Callback
        Try
            Dim data As New clsProductionSampleVerificationList()
            data.FactoryCode = e.Parameter
            Dim a As String

            Dim ErrMsg As String = ""
            dt = clsProductionSampleVerificationListDB.FillCombo(Line_Sel, data, ErrMsg)
            With cboLineID
                .DataSource = dt
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If cboLineID.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboLineID.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("LineCode", a)
            data.LineCode = HideValue.Get("LineCode")

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, ex.Message, 0)
        End Try
    End Sub

    Private Sub cboItemCheck_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboItemCheck.Callback
        Try
            Dim data As New clsProductionSampleVerificationList()
            data.FactoryCode = Split(e.Parameter, "|")(0)
            data.ItemType_Code = Split(e.Parameter, "|")(1)
            data.LineCode = Split(e.Parameter, "|")(2)
            Dim a As String

            Dim ErrMsg As String = ""
            dt = clsProductionSampleVerificationListDB.FillCombo(ItemCheck_Sel, data, ErrMsg)
            With cboItemCheck
                .DataSource = dt
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If cboItemCheck.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboItemCheck.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("ItemCheck_Code", a)
            data.ItemCheck_Code = HideValue.Get("ItemCheck_Code")

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, ex.Message, 0)
        End Try
    End Sub

    'Private Sub GridMenu_RowStyle(ByVal sender As Object, ByVal e As RowStyleEventArgs)
    '    e.Appearance.BackColor = Color.LightGreen
    'End Sub

    'Private Sub gridView_RowCellStyle(ByVal sender As Object, ByVal e As RowStyleEventArgs)
    '    Dim View As GridView = TryCast(sender, GridView)

    '    Dim status As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Status"))
    '    Dim priority As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("Priority"))

    '    If status = "New" AndAlso priority = "High" Then
    '        e.Appearance.BackColor = Color.FromArgb(150, Color.Salmon)
    '        e.Appearance.BackColor2 = Color.FromArgb(150, Color.Salmon)
    '    End If
    'End Sub

    'Private Sub GridMenu1_RowCellStyle(ByVal sender As Object, ByVal e As RowCellStyleEventArgs) Handles GridMenu.RowCellStyle
    '    Dim view As GridView = sender
    '    If view Is Nothing Then
    '        Return
    '    End If
    '    If e.RowHandle <> view.FocusedRowHandle And
    '    ((e.RowHandle Mod 2 = 0 And e.Column.VisibleIndex Mod 2 = 1) Or
    '    (e.Column.VisibleIndex Mod 2 = 0 And e.RowHandle Mod 2 = 1)) Then _
    '        e.Appearance.BackColor = Color.NavajoWhite
    'End Sub

#End Region

#Region "Procedure"
    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        GridMenu.JSProperties("cp_message") = ErrMsg
        GridMenu.JSProperties("cp_type") = msgType
        GridMenu.JSProperties("cp_val") = pVal
    End Sub
    Private Sub up_Fillcombo()
        Try
            Dim data As New clsProductionSampleVerificationList()
            Dim ErrMsg As String = ""
            Dim a As String

            '============ FILL COMBO FACTORY CODE ================'
            dt = clsProductionSampleVerificationListDB.FillCombo(Factory_Sel, data, ErrMsg)
            With cboFactory
                .DataSource = dt
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If cboFactory.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboFactory.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("FactoryCode", a)
            data.FactoryCode = HideValue.Get("FactoryCode")
            '======================================================'


            '============== FILL COMBO ITEM TYPE =================='
            dt = clsProductionSampleVerificationListDB.FillCombo(ItemType_Sel, data, ErrMsg)
            With cboItemType
                .DataSource = dt
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If cboItemType.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboItemType.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("ItemType_Code", a)
            data.ItemType_Code = HideValue.Get("ItemType_Code")
            '======================================================'


            '============== FILL COMBO LINE CODE =================='
            dt = clsProductionSampleVerificationListDB.FillCombo(Line_Sel, data, ErrMsg)
            With cboLineID
                .DataSource = dt
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If cboLineID.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboLineID.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("LineCode", a)
            data.LineCode = HideValue.Get("LineCode")
            '======================================================'


            '============== FILL COMBO ITEM CHECK =================='
            dt = clsProductionSampleVerificationListDB.FillCombo(ItemCheck_Sel, data, ErrMsg)
            With cboItemCheck
                .DataSource = dt
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If cboItemCheck.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboItemCheck.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("ItemCheck_Code", a)
            data.ItemCheck_Code = HideValue.Get("ItemCheck_Code")
            '======================================================'

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, "", 0)
        End Try
    End Sub

    Private Sub UpGridLoad(cls As clsProductionSampleVerificationList)
        Dim msgErr As String = ""
        Try

            dt = clsProductionSampleVerificationListDB.LoadGrid(cls, msgErr)
            GridMenu.DataSource = dt
            GridMenu.DataBind()
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

#End Region

End Class