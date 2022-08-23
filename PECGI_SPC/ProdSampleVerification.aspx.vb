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

Public Class ProdSampleVerification
    Inherits System.Web.UI.Page

#Region "Declaration"

    Dim pUser As String = ""
    Dim Factory_Sel As String = "1"
    Dim ItemType_Sel As String = "2"
    Dim Line_Sel As String = "3"
    Dim ItemCheck_Sel As String = "4"
    Dim Shift_Sel As String = "5"
    Dim Seq_Sel As String = "6"
    Dim irow As Integer = 0

    Dim GetHeader_ProdDate As String = "1"
    Dim GetHeader_ShifCode As String = "2"
    Dim GetHeader_Time As String = "3"
    Dim GetGridData As String = "4"
    Dim GetGridData_Activity As String = "5"
    Dim GetColumnBrowse As String = "6"

    Dim UCL As Decimal = 0
    Dim LCL As Decimal = 0
    Dim ColumnBrowse As String = ""
    Dim TotRow As Integer = 0
    Dim RowNonSeq As Integer = 9

    Dim dt As DataTable

    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False

#End Region

#Region "StartForm"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "B040")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If
        sGlobal.getMenu("B040")
        Master.SiteTitle = sGlobal.menuName
        show_error(MsgTypeEnum.Info, "", 0)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            UpFillCombo()
        End If
    End Sub
#End Region

#Region "Event"
    Private Sub Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Grid.CustomCallback
        Try
            Dim cls As New clsProdSampleVerification
            Dim msgErr As String = ""
            Dim pAction As String = Split(e.Parameters, "|")(0)

            If pAction = "Load" Then
                Up_GridLoad()
                irow = 0
            ElseIf pAction = "Clear" Then
                'dt = clsProdSampleVerificationDB.LoadGrid(cls, msgErr)
                'GridMenu.DataSource = dt
                'GridMenu.DataBind()
            End If

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Sub GridMenu_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridMenu.CustomCallback
        Try
            Dim cls As New clsProdSampleVerification
            Dim msgErr As String = ""
            Dim pAction As String = Split(e.Parameters, "|")(0)

            If pAction = "Load" Then
                Up_GridLoadActivities()
            ElseIf pAction = "Clear" Then
                'dt = clsProdSampleVerificationDB.LoadGrid(cls, msgErr)
                'GridMenu.DataSource = dt
                'GridMenu.DataBind()
            End If

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Sub Grid_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles Grid.HtmlDataCellPrepared

        If (e.DataColumn.FieldName = ColumnBrowse) Then
            Dim RowSeq = TotRow - RowNonSeq

            If irow < RowSeq Then
                If e.CellValue < LCL Then
                    e.Cell.BackColor = Color.Red
                ElseIf e.CellValue > UCL Then
                    e.Cell.BackColor = Color.Red
                End If
            ElseIf (irow = totRow - 2) Or (irow = totRow - 3) Or (irow = totRow - 4) Then
                If IsDBNull(e.CellValue) Then
                    e.Cell.BackColor = Color.Yellow
                ElseIf e.CellValue = "NG" Then
                    e.Cell.BackColor = Color.Red
                End If
            ElseIf irow = totRow - 1 Then
                If e.CellValue = "C" Then
                    e.Cell.BackColor = Color.Orange
                End If
            ElseIf irow = TotRow Then

            End If

            irow = irow + 1
        End If

    End Sub

    Private Sub cboLineID_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboLineID.Callback
        Try
            Dim data As New clsProdSampleVerification()
            data.User = pUser
            data.FactoryCode = e.Parameter

            Dim ErrMsg As String = ""
            dt = clsProdSampleVerificationDB.FillCombo(Line_Sel, data, ErrMsg)
            With cboLineID
                .DataSource = dt
                .DataBind()
            End With
        Catch ex As Exception
            show_error(MsgTypeEnum.Info, ex.Message, 0)
        End Try
    End Sub

    Private Sub cboItemCheck_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboItemCheck.Callback
        Try
            Dim data As New clsProdSampleVerification()
            data.FactoryCode = Split(e.Parameter, "|")(0)
            data.ItemType_Code = Split(e.Parameter, "|")(1)
            data.LineCode = Split(e.Parameter, "|")(2)

            Dim ErrMsg As String = ""
            dt = clsProdSampleVerificationDB.FillCombo(ItemCheck_Sel, data, ErrMsg)
            With cboItemCheck
                .DataSource = dt
                .DataBind()
            End With
        Catch ex As Exception
            show_error(MsgTypeEnum.Info, ex.Message, 0)
        End Try
    End Sub

    Private Sub cboShift_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboShift.Callback
        Try
            Dim data As New clsProdSampleVerification()
            data.FactoryCode = Split(e.Parameter, "|")(0)
            data.ItemType_Code = Split(e.Parameter, "|")(1)
            data.LineCode = Split(e.Parameter, "|")(2)
            data.ItemCheck_Code = Split(e.Parameter, "|")(3)

            Dim ErrMsg As String = ""
            dt = clsProdSampleVerificationDB.FillCombo(Shift_Sel, data, ErrMsg)
            With cboShift
                .DataSource = dt
                .DataBind()
            End With

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, ex.Message, 0)
        End Try
    End Sub

    Private Sub cboSeq_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboSeq.Callback
        Try
            Dim data As New clsProdSampleVerification()
            data.FactoryCode = Split(e.Parameter, "|")(0)
            data.ItemType_Code = Split(e.Parameter, "|")(1)
            data.LineCode = Split(e.Parameter, "|")(2)
            data.ItemCheck_Code = Split(e.Parameter, "|")(3)
            data.ShiftCode = Split(e.Parameter, "|")(4)

            Dim ErrMsg As String = ""
            dt = clsProdSampleVerificationDB.FillCombo(Seq_Sel, data, ErrMsg)
            With cboSeq
                .DataSource = dt
                .DataBind()
            End With

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, ex.Message, 0)
        End Try
    End Sub
#End Region

#Region "Procedure"
    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        GridMenu.JSProperties("cp_message") = ErrMsg
        GridMenu.JSProperties("cp_type") = msgType
        GridMenu.JSProperties("cp_val") = pVal
    End Sub
    Private Sub UpFillCombo()
        Try
            Dim data As New clsProdSampleVerification()
            Dim ErrMsg As String = ""

            '============ FILL COMBO FACTORY CODE ================'
            dt = clsProdSampleVerificationDB.FillCombo(Factory_Sel, data, ErrMsg)
            With cboFactory
                .DataSource = dt
                .DataBind()
            End With
            '======================================================'

            '============== FILL COMBO ITEM TYPE =================='
            dt = clsProdSampleVerificationDB.FillCombo(ItemType_Sel, data, ErrMsg)
            With cboItemType
                .DataSource = dt
                .DataBind()
            End With
            '======================================================'

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, "", 0)
        End Try
    End Sub

    Private Sub Up_GridLoad()


        Dim Factory As String = HideValue.Get("FactoryCode")
        Dim Itemtype As String = HideValue.Get("ItemType_Code")
        Dim Line As String = HideValue.Get("LineCode")
        Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
        Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
        Dim Shift As String = HideValue.Get("ShiftCode")
        Dim Seq As String = HideValue.Get("Seq")
        Dim msgErr As String = ""
        Dim ds As DataSet

        Dim cls As New clsProdSampleVerification
        cls.FactoryCode = Factory
        cls.ItemType_Code = Itemtype
        cls.LineCode = Line
        cls.ItemCheck_Code = ItemCheck
        cls.ProdDate = ProdDate
        cls.ShiftCode = Shift
        cls.Seq = Seq

        With Grid
            .Columns.Clear()
            Dim Band1 As New GridViewBandColumn
            Band1.Caption = "Date"
            .Columns.Add(Band1)

            Dim Band2 As New GridViewBandColumn
            Band2.Caption = "Shift"
            Band1.Columns.Add(Band2)

            Dim ColDesc As New GridViewDataTextColumn
            ColDesc.FieldName = "nDesc"
            ColDesc.Caption = "Time"
            ColDesc.Width = 80
            ColDesc.CellStyle.HorizontalAlign = HorizontalAlign.Center
            Band2.Columns.Add(ColDesc)

            ds = clsProdSampleVerificationDB.GridLoad(GetHeader_ProdDate, cls, "")
            Dim dtDate As DataTable = ds.Tables(0)
            For i = 0 To dtDate.Rows.Count - 1
                Dim Col_ProdDate As New GridViewBandColumn
                Dim nProdDate = dtDate.Rows(i)("ProdDate")
                Col_ProdDate.Caption = nProdDate
                .Columns.Add(Col_ProdDate)

                cls.ProdDate_Grid = Convert.ToDateTime(nProdDate).ToString("yyyy-MM-dd")
                ds = clsProdSampleVerificationDB.GridLoad(GetHeader_ShifCode, cls, "")
                Dim dtShift As DataTable = ds.Tables(0)
                For n = 0 To dtShift.Rows.Count - 1
                    Dim Col_Shift As New GridViewBandColumn
                    Dim nShiftCode = dtShift.Rows(n)("ShiftCode")
                    Col_Shift.Caption = nShiftCode
                    Col_ProdDate.Columns.Add(Col_Shift)

                    cls.Shiftcode_Grid = nShiftCode
                    ds = clsProdSampleVerificationDB.GridLoad(GetHeader_Time, cls, "")
                    Dim dtSeq As DataTable = ds.Tables(0)
                    For r = 0 To dtSeq.Rows.Count - 1
                        Dim Col_Seq As New GridViewDataTextColumn
                        Col_Seq.Width = 70
                        Col_Seq.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                        Col_Seq.CellStyle.HorizontalAlign = HorizontalAlign.Center
                        Col_Seq.FieldName = dtSeq.Rows(r)("nTime")
                        Col_Seq.Caption = dtSeq.Rows(r)("nTimeDesc")
                        Col_Shift.Columns.Add(Col_Seq)
                    Next
                Next
            Next

            ds = clsProdSampleVerificationDB.GridLoad(GetGridData, cls, "")
            Dim dtGrid As DataTable = ds.Tables(0)
            TotRow = dtGrid.Rows.Count
            .KeyFieldName = "nDesc"
            .Enabled = False
            .DataSource = dtGrid
            .DataBind()
        End With

        ds = clsProdSampleVerificationDB.GridLoad(GetColumnBrowse, cls, "")
        Dim dtColBrowse As DataTable = ds.Tables(0)
        If dtColBrowse.Rows.Count > 0 Then
            ColumnBrowse = dtColBrowse.Rows(0)("nTime")
            UCL = dtColBrowse.Rows(0)("UCL")
            LCL = dtColBrowse.Rows(0)("LCL")
        End If

    End Sub

    Private Sub Up_GridLoadActivities()

        Dim Factory As String = HideValue.Get("FactoryCode")
        Dim Itemtype As String = HideValue.Get("ItemType_Code")
        Dim Line As String = HideValue.Get("LineCode")
        Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
        Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
        Dim Shift As String = HideValue.Get("ShiftCode")
        Dim Seq As String = HideValue.Get("Seq")
        Dim msgErr As String = ""
        Dim ds As DataSet

        Dim cls As New clsProdSampleVerification
        cls.FactoryCode = Factory
        cls.ItemType_Code = Itemtype
        cls.LineCode = Line
        cls.ItemCheck_Code = ItemCheck
        cls.ProdDate = ProdDate
        cls.ShiftCode = Shift
        cls.Seq = Seq

        With GridMenu
            ds = clsProdSampleVerificationDB.GridLoad(GetGridData_Activity, cls, "")
            Dim dtGridMenu As DataTable = ds.Tables(0)
            .DataSource = dtGridMenu
            .DataBind()
        End With
    End Sub

#End Region
End Class