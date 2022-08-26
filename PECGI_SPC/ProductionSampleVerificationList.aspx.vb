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

Public Class ProductionSampleVerificationList
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Dim Factory_Sel As String = "1"
    Dim ItemType_Sel As String = "2"
    Dim Line_Sel As String = "3"
    Dim ItemCheck_Sel As String = "4"
    Dim MK_Sel As String = "5"
    Dim QC_Sel As String = "6"
    Dim UCL As Decimal = 0
    Dim LCL As Decimal = 0
    Dim USL As Decimal = 0
    Dim LSL As Decimal = 0
    Private dt As DataTable

    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False

#End Region

#Region "Event"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            up_Fillcombo()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "B030")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If

        sGlobal.getMenu("B030")
        Master.SiteTitle = sGlobal.menuName
        show_error(MsgTypeEnum.Info, "", 0)
    End Sub

    Private Sub GridMenu_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridMenu.CustomCallback
        Try
            Dim cls As New clsProductionSampleVerificationList
            Dim msgErr As String = ""
            Dim pAction As String = Split(e.Parameters, "|")(0)

            If pAction = "Load" Then
                Dim Factory As String = Split(e.Parameters, "|")(1)
                Dim Itemtype As String = Split(e.Parameters, "|")(2)
                Dim Line As String = Split(e.Parameters, "|")(3)
                Dim ItemCheck As String = Split(e.Parameters, "|")(4)
                Dim ProdDateFrom As String = Convert.ToDateTime(Split(e.Parameters, "|")(5)).ToString("yyyy-MM-dd")
                Dim ProdDateTo As String = Convert.ToDateTime(Split(e.Parameters, "|")(6)).ToString("yyyy-MM-dd")
                Dim MKVerification As String = Split(e.Parameters, "|")(7)
                Dim QCVerification As String = Split(e.Parameters, "|")(8)

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
                dt = clsProductionSampleVerificationListDB.LoadGrid(cls)
                GridMenu.DataSource = dt
                GridMenu.DataBind()
            End If

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Sub cboLineID_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboLineID.Callback
        Try
            Dim data As New clsProductionSampleVerificationList()
            data.FactoryCode = e.Parameter

            Dim ErrMsg As String = ""
            dt = clsProductionSampleVerificationListDB.FillCombo(Line_Sel, data)
            With cboLineID
                .DataSource = dt
                .DataBind()
            End With

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 0)
        End Try
    End Sub

    Private Sub cboItemCheck_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboItemCheck.Callback
        Try
            Dim data As New clsProductionSampleVerificationList()
            data.FactoryCode = Split(e.Parameter, "|")(0)
            data.ItemType_Code = Split(e.Parameter, "|")(1)
            data.LineCode = Split(e.Parameter, "|")(2)

            Dim ErrMsg As String = ""
            dt = clsProductionSampleVerificationListDB.FillCombo(ItemCheck_Sel, data)
            With cboItemCheck
                .DataSource = dt
                .DataBind()
            End With
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 0)
        End Try
    End Sub

    Private Sub GridMenu_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles GridMenu.HtmlDataCellPrepared
        Try
            If (e.DataColumn.FieldName = "nMin" Or e.DataColumn.FieldName = "nMax" Or e.DataColumn.FieldName = "nAVG") Then
                If e.CellValue < LSL Or e.CellValue > USL Then
                    e.Cell.BackColor = Color.Red
                ElseIf e.CellValue < LCL Or e.CellValue > UCL Then
                    e.Cell.BackColor = Color.Yellow
                End If
            End If

            If (e.DataColumn.FieldName = "Cor_Sts") Then
                If e.CellValue = "C" Then
                    e.Cell.BackColor = Color.Orange
                End If
            End If
            If (e.DataColumn.FieldName = "Result") Then
                If e.CellValue = "NG" Then
                    e.Cell.BackColor = Color.Red
                End If
            End If
            If (e.DataColumn.FieldName = "MK_PIC") Then
                If IsDBNull(e.CellValue) Then
                    e.Cell.BackColor = Color.Yellow
                End If
            End If
            If (e.DataColumn.FieldName = "MK_Time") Then
                If IsDBNull(e.CellValue) Then
                    e.Cell.BackColor = Color.Yellow
                End If
            End If
            If (e.DataColumn.FieldName = "QC_PIC") Then
                If IsDBNull(e.CellValue) Then
                    e.Cell.BackColor = Color.Yellow
                End If
            End If
            If (e.DataColumn.FieldName = "QC_Time") Then
                If IsDBNull(e.CellValue) Then
                    e.Cell.BackColor = Color.Yellow
                End If
            End If
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 0)
        End Try
    End Sub

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

            '============ FILL COMBO FACTORY CODE ================'
            dt = clsProductionSampleVerificationListDB.FillCombo(Factory_Sel, data)
            With cboFactory
                .DataSource = dt
                .DataBind()
            End With
            '======================================================'

            '============== FILL COMBO ITEM TYPE =================='
            dt = clsProductionSampleVerificationListDB.FillCombo(ItemType_Sel, data)
            With cboItemType
                .DataSource = dt
                .DataBind()
            End With
            '======================================================'

            '============== FILL MK VERIFICATION =================='
            dt = clsProductionSampleVerificationListDB.FillCombo(MK_Sel, data)
            With cboMK
                .DataSource = dt
                .DataBind()
            End With
            '======================================================'


            '============== FILL QC VERIFICATION =================='
            dt = clsProductionSampleVerificationListDB.FillCombo(QC_Sel, data)
            With cboQC
                .DataSource = dt
                .DataBind()
            End With
            '======================================================'

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 0)
        End Try
    End Sub
    Private Sub UpGridLoad(cls As clsProductionSampleVerificationList)
        Try
            dt = clsProductionSampleVerificationListDB.LoadGrid(cls)
            GridMenu.DataSource = dt
            GridMenu.DataBind()

            If dt.Rows.Count > 0 Then
                LCL = dt.Rows(0)("LCL")
                UCL = dt.Rows(0)("UCL")
                LsL = dt.Rows(0)("LSL")
                UsL = dt.Rows(0)("USL")
            Else
                show_error(MsgTypeEnum.Warning, "Data Not Found !", 1)
            End If

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    'Private Sub up_Fillcombo()
    '    Try
    '        Dim data As New clsProductionSampleVerificationList()
    '        Dim ErrMsg As String = ""
    '        Dim a As String

    '        '============ FILL COMBO FACTORY CODE ================'
    '        dt = clsProductionSampleVerificationListDB.FillCombo(Factory_Sel, data, ErrMsg)
    '        With cboFactory
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '        If cboFactory.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboFactory.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("FactoryCode", a)
    '        data.FactoryCode = HideValue.Get("FactoryCode")
    '        '======================================================'


    '        '============== FILL COMBO ITEM TYPE =================='
    '        dt = clsProductionSampleVerificationListDB.FillCombo(ItemType_Sel, data, ErrMsg)
    '        With cboItemType
    '            .DataSource = dt
    '            .DataBind()
    '            .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End With
    '        If cboItemType.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboItemType.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("ItemType_Code", a)
    '        data.ItemType_Code = HideValue.Get("ItemType_Code")
    '        '======================================================'


    '        '============== FILL COMBO LINE CODE =================='
    '        dt = clsProductionSampleVerificationListDB.FillCombo(Line_Sel, data, ErrMsg)
    '        With cboLineID
    '            .DataSource = dt
    '            .DataBind()
    '            .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End With
    '        If cboLineID.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboLineID.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("LineCode", a)
    '        data.LineCode = HideValue.Get("LineCode")
    '        '======================================================'


    '        '============== FILL COMBO ITEM CHECK =================='
    '        dt = clsProductionSampleVerificationListDB.FillCombo(ItemCheck_Sel, data, ErrMsg)
    '        With cboItemCheck
    '            .DataSource = dt
    '            .DataBind()
    '            .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End With
    '        If cboItemCheck.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboItemCheck.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("ItemCheck_Code", a)
    '        data.ItemCheck_Code = HideValue.Get("ItemCheck_Code")
    '        '======================================================'

    '        '============== FILL MK VERIFICATION =================='
    '        dt = clsProductionSampleVerificationListDB.FillCombo(MK_Sel, data, ErrMsg)
    '        With cboMK
    '            .DataSource = dt
    '            .DataBind()
    '            .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End With
    '        If cboMK.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboMK.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("MK", a)
    '        '======================================================'


    '        '============== FILL QC VERIFICATION =================='
    '        dt = clsProductionSampleVerificationListDB.FillCombo(QC_Sel, data, ErrMsg)
    '        With cboQC
    '            .DataSource = dt
    '            .DataBind()
    '            .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End With
    '        If cboQC.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboQC.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("QC", a)
    '        '======================================================'

    '    Catch ex As Exception
    '        show_error(MsgTypeEnum.Info, "", 0)
    '    End Try
    'End Sub

#End Region

End Class