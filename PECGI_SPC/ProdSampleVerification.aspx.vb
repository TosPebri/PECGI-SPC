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

    Dim GetHeader_ProdDate As String = "1"
    Dim GetHeader_ShifCode As String = "2"
    Dim GetHeader_Time As String = "3"
    Dim GetGridData As String = "4"
    Dim GetGridData_Activity As String = "5"
    Dim GetColumnBrowse As String = "6"

    Dim UCL As Decimal = 0
    Dim LCL As Decimal = 0
    Dim USL As Decimal = 0
    Dim LSL As Decimal = 0
    Dim ColumnBrowse As String = ""
    Dim TotRow As Integer = 0
    Dim nRow As Integer = 0
    Dim VerifyStatus As String = "1"
    Dim DescIndex As String = ""

    Dim dt As DataTable
    Dim ds As DataSet

    Dim prmFactoryCode = ""
    Dim prmItemType = ""
    Dim prmLineCode = ""
    Dim prmItemCheck = ""
    Dim prmProdDate = ""
    Dim prmShifCode = ""
    Dim prmSeqNo = ""

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
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("prm") IsNot Nothing Then
                Dim data = Request.QueryString("prm").ToString()
                prmFactoryCode = Session("prmFactoryCode")
                prmItemType = Session("prmItemType")
                prmLineCode = Session("prmLineCode")
                prmItemCheck = Session("prmItemCheck")
                prmProdDate = Session("prmProdDate")
                prmShifCode = Session("prmShiftCode")
                prmSeqNo = Session("prmSeqNo")

                dtProdDate.Value = Convert.ToDateTime(prmProdDate)
                dtProdDate.Enabled = False

                Dim ShiftCode = ""
                If prmShifCode = "1" Then
                    ShiftCode = "SH001"
                ElseIf prmShifCode = "2" Then
                    ShiftCode = "SH002"
                End If

                btnBrowse.Enabled = False
                btnClear.Enabled = False

                UpFillCombo()

                Up_GridLoad(prmFactoryCode, prmItemType, prmLineCode, prmItemCheck, Convert.ToDateTime(prmProdDate).ToString("yyyy-MM-dd"), ShiftCode, prmSeqNo)
                Up_GridLoadActivities(prmFactoryCode, prmItemType, prmLineCode, prmItemCheck, Convert.ToDateTime(prmProdDate).ToString("yyyy-MM-dd"), ShiftCode, prmSeqNo)

                If VerifyStatus = 0 Then
                    btnVerification.Enabled = True
                End If
                Grid.JSProperties("cp_Verify") = VerifyStatus
            Else
                dtProdDate.Value = DateTime.Now
                UpFillCombo()
                Up_GridLoadActivities("", "", "", "", "", "", "")
                Grid.JSProperties("cp_Verify") = VerifyStatus
                btnBack.Visible = False
            End If

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
                Dim Factory As String = HideValue.Get("FactoryCode")
                Dim Itemtype As String = HideValue.Get("ItemType_Code")
                Dim Line As String = HideValue.Get("LineCode")
                Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
                Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
                Dim Shift As String = HideValue.Get("ShiftCode")
                Dim Seq As String = HideValue.Get("Seq")

                nRow = 0
                Up_GridLoad(Factory, Itemtype, Line, ItemCheck, ProdDate, Shift, Seq)
                Grid.JSProperties("cp_Verify") = VerifyStatus
            ElseIf pAction = "Verify" Then
                Dim SpcResultID As String
                Dim Factory As String = HideValue.Get("FactoryCode")
                Dim Itemtype As String = HideValue.Get("ItemType_Code")
                Dim Line As String = HideValue.Get("LineCode")
                Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
                Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
                Dim Shift As String = HideValue.Get("ShiftCode")
                Dim Seq As String = HideValue.Get("Seq")

                cls.FactoryCode = Factory
                cls.ItemType_Code = Itemtype
                cls.LineCode = Line
                cls.ItemCheck_Code = ItemCheck
                cls.ProdDate = ProdDate
                cls.ShiftCode = Shift
                cls.Seq = Seq

                ds = clsProdSampleVerificationDB.GridLoad(GetColumnBrowse, cls, "")
                Dim dtColBrowse As DataTable = ds.Tables(0)
                If dtColBrowse.Rows.Count > 0 Then
                    SpcResultID = dtColBrowse.Rows(0)("SPCResultID")
                End If
                Verify(SpcResultID)
            End If

        Catch ex As Exception
            show_errorGrid(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Sub GridMenu_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridMenu.CustomCallback
        Try
            Dim cls As New clsProdSampleVerification
            Dim msgErr As String = ""
            Dim pAction As String = Split(e.Parameters, "|")(0)

            If pAction = "Load" Then
                Dim Factory As String = HideValue.Get("FactoryCode")
                Dim Itemtype As String = HideValue.Get("ItemType_Code")
                Dim Line As String = HideValue.Get("LineCode")
                Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
                Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
                Dim Shift As String = HideValue.Get("ShiftCode")
                Dim Seq As String = HideValue.Get("Seq")

                Up_GridLoadActivities(Factory, Itemtype, Line, ItemCheck, ProdDate, Shift, Seq)
            ElseIf pAction = "Clear" Then
                'dt = clsProdSampleVerificationDB.LoadGrid(cls, msgErr)
                'GridMenu.DataSource = dt
                'GridMenu.DataBind()          
            End If

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub
    Private Sub GridMenu_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridMenu.BeforeGetCallbackResult
        If GridMenu.IsNewRowEditing Then
            GridMenu.SettingsCommandButton.UpdateButton.Text = "Save"
        End If
    End Sub

    Private Sub Grid_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles Grid.HtmlDataCellPrepared
        Try
            If e.DataColumn.FieldName = "nDescIndex" Then
                DescIndex = e.CellValue
            ElseIf e.DataColumn.FieldName <> "nDesc" Then

                If DescIndex = "EachData" Then
                    If e.CellValue < LSL Or e.CellValue > USL Then
                        e.Cell.BackColor = Color.Red
                    ElseIf e.CellValue < LSL Or e.CellValue > UCL Then
                        e.Cell.BackColor = Color.Pink
                    End If
                ElseIf DescIndex = "XBar" Then
                    If e.CellValue < LSL Or e.CellValue > USL Then
                        e.Cell.BackColor = Color.Red
                    ElseIf e.CellValue < LSL Or e.CellValue > UCL Then
                        e.Cell.BackColor = Color.Yellow
                    End If
                ElseIf DescIndex = "Judgement" Then
                    If e.CellValue = "NG" Then
                        e.Cell.BackColor = Color.Red
                    End If
                ElseIf DescIndex = "Correction" Then
                    If e.CellValue = "C" Then
                        e.Cell.BackColor = Color.Orange
                    End If
                ElseIf DescIndex = "View" Then
                     e.Cell.ForeColor = Color.Blue
                End If

                If (e.DataColumn.FieldName = ColumnBrowse) Then
                    If DescIndex = "Verification" Then
                        If IsDBNull(e.CellValue) Then
                            e.Cell.BackColor = Color.Yellow
                        End If
                    End If

                End If

            End If
        Catch ex As Exception
            Throw New Exception("Error_EditingGrid !" & ex.Message)
        End Try
    End Sub

    Protected Sub GridMenu_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles GridMenu.RowInserting
        e.Cancel = True
        Dim Factory As String = HideValue.Get("FactoryCode")
        Dim Itemtype As String = HideValue.Get("ItemType_Code")
        Dim Line As String = HideValue.Get("LineCode")
        Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
        Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
        Dim Shift As String = HideValue.Get("ShiftCode")
        Dim Seq As String = HideValue.Get("Seq")

        Dim data As New clsProdSampleVerification With {
            .FactoryCode = Factory,
            .ItemType_Code = Itemtype,
            .LineCode = Line,
            .ItemCheck_Code = ItemCheck,
            .ProdDate = ProdDate,
            .ShiftCode = Shift,
            .Action = e.NewValues("Action") & "",
            .Result = e.NewValues("Result") & "",
            .Remark = e.NewValues("Remark") & "",
            .User = pUser}
        Try
            Dim Insert = clsProdSampleVerificationDB.Activity_Insert("CREATE", data)
            If Insert = True Then
                show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
                GridMenu.CancelEdit()
                Up_GridLoadActivities(Factory, Itemtype, Line, ItemCheck, ProdDate, Shift, Seq)
                Return
            End If
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub
    Protected Sub GridMenu_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles GridMenu.RowUpdating
        e.Cancel = True
        Dim Factory As String = HideValue.Get("FactoryCode")
        Dim Itemtype As String = HideValue.Get("ItemType_Code")
        Dim Line As String = HideValue.Get("LineCode")
        Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
        Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
        Dim Shift As String = HideValue.Get("ShiftCode")
        Dim Seq As String = HideValue.Get("Seq")

        Dim data As New clsProdSampleVerification With {
            .ActivityID = e.NewValues("ActivityID") & "",
            .Action = e.NewValues("Action") & "",
            .Result = e.NewValues("Result") & "",
            .Remark = e.NewValues("Remark") & "",
            .User = pUser}
        Try
            Dim Update = clsProdSampleVerificationDB.Activity_Insert("UPDATE", data)
            If Update = True Then
                show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
                GridMenu.CancelEdit()
                Up_GridLoadActivities(Factory, Itemtype, Line, ItemCheck, ProdDate, Shift, Seq)
                Return
            End If
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Protected Sub GridMenu_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles GridMenu.RowDeleting
        e.Cancel = True
        Dim Factory As String = HideValue.Get("FactoryCode")
        Dim Itemtype As String = HideValue.Get("ItemType_Code")
        Dim Line As String = HideValue.Get("LineCode")
        Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
        Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
        Dim Shift As String = HideValue.Get("ShiftCode")
        Dim Seq As String = HideValue.Get("Seq")

        Dim data As New clsProdSampleVerification With {
            .ActivityID = e.Values("ActivityID")}
        Try
            Dim Delete = clsProdSampleVerificationDB.Activity_Insert("DELETE", data)
            show_error(MsgTypeEnum.Success, "Delete data successfully!", 1)
            GridMenu.CancelEdit()
            Up_GridLoadActivities(Factory, Itemtype, Line, ItemCheck, ProdDate, Shift, Seq)
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
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
            data.User = pUser

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

    Private Sub show_errorGrid(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        Grid.JSProperties("cp_message") = ErrMsg
        Grid.JSProperties("cp_type") = msgType
        Grid.JSProperties("cp_val") = pVal
    End Sub
    Private Sub UpFillCombo()
        Try
            Dim data As New clsProdSampleVerification()
            data.User = pUser
            Dim ErrMsg As String = ""
            Dim a As String

            '============ FILL COMBO FACTORY CODE ================'
            dt = clsProdSampleVerificationDB.FillCombo(Factory_Sel, data, ErrMsg)
            With cboFactory
                .DataSource = dt
                .DataBind()
            End With
            If prmFactoryCode <> "" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("CODE") = prmFactoryCode Then
                        cboFactory.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboFactory.Enabled = False
            End If
            If cboFactory.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboFactory.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("FactoryCode", a)
            data.FactoryCode = HideValue.Get("FactoryCode")
            '======================================================'

            '============== FILL COMBO ITEM TYPE =================='
            dt = clsProdSampleVerificationDB.FillCombo(ItemType_Sel, data, ErrMsg)
            With cboItemType
                .DataSource = dt
                .DataBind()
            End With
            If prmItemType <> "" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("CODE") = prmItemType Then
                        cboItemType.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboItemType.Enabled = False
            End If
            If cboItemType.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboItemType.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("ItemType_Code", a)
            data.ItemType_Code = HideValue.Get("ItemType_Code")
            '======================================================'

            '============== FILL COMBO LINE CODE =================='
            dt = clsProdSampleVerificationDB.FillCombo(Line_Sel, data, ErrMsg)
            With cboLineID
                .DataSource = dt
                .DataBind()
            End With
            If prmLineCode <> "" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("CODE") = prmLineCode Then
                        cboLineID.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboLineID.Enabled = False
            End If
            If cboLineID.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboLineID.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("LineCode", a)
            data.LineCode = HideValue.Get("LineCode")
            '======================================================'


            '============== FILL COMBO ITEM CHECK =================='
            dt = clsProdSampleVerificationDB.FillCombo(ItemCheck_Sel, data, ErrMsg)
            With cboItemCheck
                .DataSource = dt
                .DataBind()
            End With
            If prmItemCheck <> "" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("CODE") = prmItemCheck Then
                        cboItemCheck.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboItemCheck.Enabled = False
            End If
            If cboItemCheck.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboItemCheck.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("ItemCheck_Code", a)
            data.ItemCheck_Code = HideValue.Get("ItemCheck_Code")
            '======================================================'

            '============== FILL COMBO SHIFY =================='
            dt = clsProdSampleVerificationDB.FillCombo(Shift_Sel, data, ErrMsg)
            With cboShift
                .DataSource = dt
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If prmShifCode <> "" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("CODE") = prmShifCode Then
                        cboShift.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboShift.Enabled = False
            End If
            If cboShift.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboShift.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("ShiftCode", a)
            data.ShiftCode = HideValue.Get("ShiftCode")
            '======================================================'

            '============== FILL COMBO SEQ =================='
            dt = clsProdSampleVerificationDB.FillCombo(Seq_Sel, data, ErrMsg)
            With cboSeq
                .DataSource = dt
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If prmSeqNo <> "" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("CODE") = prmSeqNo Then
                        cboSeq.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboSeq.Enabled = False
            End If
            If cboSeq.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboSeq.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("Seq", a)
            data.Seq = HideValue.Get("Seq")
            '======================================================'

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, "", 0)
        End Try
    End Sub

    Private Sub Up_GridLoad(Factory As String, ItemType As String, Line As String, ItemCheck As String, ProdDate As String, Shift As String, Seq As String)
        Dim msgErr As String = ""

        Dim cls As New clsProdSampleVerification
        cls.FactoryCode = Factory
        cls.ItemType_Code = ItemType
        cls.LineCode = Line
        cls.ItemCheck_Code = ItemCheck
        cls.ProdDate = ProdDate
        cls.ShiftCode = Shift
        cls.Seq = Seq

        With Grid
            .Columns.Clear()
            Dim ColDescIndex As New GridViewDataTextColumn
            ColDescIndex.FieldName = "nDescIndex"
            ColDescIndex.Width = 0
            ColDescIndex.CellStyle.HorizontalAlign = HorizontalAlign.Center
            .Columns.Add(ColDescIndex)

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
            If dtDate.Rows.Count > 0 Then
                For i = 0 To dtDate.Rows.Count - 1
                    Dim Col_ProdDate As New GridViewBandColumn
                    Dim nProdDate = dtDate.Rows(i)("ProdDate")
                    Col_ProdDate.Caption = nProdDate
                    .Columns.Add(Col_ProdDate)

                    cls.ProdDate_Grid = Convert.ToDateTime(nProdDate).ToString("yyyy-MM-dd")
                    ds = clsProdSampleVerificationDB.GridLoad(GetHeader_ShifCode, cls, "")
                    Dim dtShift As DataTable = ds.Tables(0)
                    If dtShift.Rows.Count > 0 Then
                        For n = 0 To dtShift.Rows.Count - 1

                            Dim Col_Shift As New GridViewBandColumn
                            Dim nShiftCode = dtShift.Rows(n)("ShiftCode")
                            If nShiftCode = "SH001" Then
                                nShiftCode = "Shift 1"
                            ElseIf nShiftCode = "SH002" Then
                                nShiftCode = "Shift 2"
                            End If

                            Col_Shift.Caption = nShiftCode
                            Col_ProdDate.Columns.Add(Col_Shift)

                            cls.Shiftcode_Grid = dtShift.Rows(n)("ShiftCode")
                            ds = clsProdSampleVerificationDB.GridLoad(GetHeader_Time, cls, "")
                            Dim dtSeq As DataTable = ds.Tables(0)
                            If dtSeq.Rows.Count > 0 Then
                                For r = 0 To dtSeq.Rows.Count - 1
                                    Dim Col_Seq As New GridViewDataTextColumn
                                    Col_Seq.Width = 70
                                    Col_Seq.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                    Col_Seq.CellStyle.HorizontalAlign = HorizontalAlign.Center
                                    Col_Seq.FieldName = dtSeq.Rows(r)("nTime")
                                    Col_Seq.Caption = dtSeq.Rows(r)("nTimeDesc")
                                    Col_Shift.Columns.Add(Col_Seq)
                                Next
                            End If
                        Next
                    End If
                Next

                ds = clsProdSampleVerificationDB.GridLoad(GetGridData, cls, "")
                Dim dtGrid As DataTable = ds.Tables(0)
                If dtGrid.Rows.Count > 0 Then
                    .KeyFieldName = "nDesc"
                    .DataSource = dtGrid
                    .DataBind()
                    .Styles.CommandColumn.BackColor = Color.White
                    .Styles.CommandColumn.ForeColor = Color.Black
                    TotRow = dtGrid.Rows.Count
                End If

                ds = clsProdSampleVerificationDB.GridLoad(GetColumnBrowse, cls, "")
                Dim dtColBrowse As DataTable = ds.Tables(0)
                If dtColBrowse.Rows.Count > 0 Then
                    ColumnBrowse = dtColBrowse.Rows(0)("nTime")
                    UCL = dtColBrowse.Rows(0)("UCL")
                    LCL = dtColBrowse.Rows(0)("LCL")
                    USL = dtColBrowse.Rows(0)("USL")
                    LSL = dtColBrowse.Rows(0)("LSL")
                    VerifyStatus = dtColBrowse.Rows(0)("VerifyStatus")
                End If
            Else
                show_errorGrid(MsgTypeEnum.Warning, "Data Not Found", 1)
            End If
        End With
    End Sub

    Private Sub Up_GridLoadActivities(Factory As String, ItemType As String, Line As String, ItemCheck As String, ProdDate As String, Shift As String, Seq As String)

        Dim cls As New clsProdSampleVerification
        cls.FactoryCode = Factory
        cls.ItemType_Code = ItemType
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

    Private Sub Verify(SpcResultId As String)
        Dim cls As New clsProdSampleVerification
        cls.SPCResultID = SpcResultId
        cls.User = pUser

        Dim Factory As String = HideValue.Get("FactoryCode")
        Dim Itemtype As String = HideValue.Get("ItemType_Code")
        Dim Line As String = HideValue.Get("LineCode")
        Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
        Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
        Dim Shift As String = HideValue.Get("ShiftCode")
        Dim Seq As String = HideValue.Get("Seq")

        Try
            Dim Verify = clsProdSampleVerificationDB.Verify(cls)
            If Verify = True Then
                show_errorGrid(MsgTypeEnum.Success, "Verify data successfully!", 1)
                Up_GridLoad(Factory, Itemtype, Line, ItemCheck, ProdDate, Shift, Seq)
                Return
            End If
        Catch ex As Exception
            show_errorGrid(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub



    'Private Sub UpFillCombo()
    '    Try
    '        Dim data As New clsProdSampleVerification()
    '        Dim ErrMsg As String = ""
    '        Dim a As String
    '        data.User = pUser

    '        '============ FILL COMBO FACTORY CODE ================'
    '        dt = clsProdSampleVerificationDB.FillCombo(Factory_Sel, data, ErrMsg)
    '        With cboFactory
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '        If prmFactoryCode <> "" Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("CODE") = prmFactoryCode Then
    '                    cboFactory.SelectedIndex = i
    '                    Exit For
    '                End If
    '            Next
    '            cboFactory.Enabled = False
    '        Else
    '            cboFactory.SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End If
    '        If cboFactory.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboFactory.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("FactoryCode", a)
    '        data.FactoryCode = HideValue.Get("FactoryCode")
    '        '======================================================'

    '        '============== FILL COMBO ITEM TYPE =================='
    '        dt = clsProdSampleVerificationDB.FillCombo(ItemType_Sel, data, ErrMsg)
    '        With cboItemType
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '        If prmItemType <> "" Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("CODE") = prmItemType Then
    '                    cboItemType.SelectedIndex = i
    '                    Exit For
    '                End If
    '            Next
    '            cboItemType.Enabled = False
    '        Else
    '            cboItemType.SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End If
    '        If cboItemType.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboItemType.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("ItemType_Code", a)
    '        data.ItemType_Code = HideValue.Get("ItemType_Code")
    '        '======================================================'

    '        '============== FILL COMBO LINE CODE =================='
    '        dt = clsProdSampleVerificationDB.FillCombo(Line_Sel, data, ErrMsg)
    '        With cboLineID
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '        If prmLineCode <> "" Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("CODE") = prmLineCode Then
    '                    cboLineID.SelectedIndex = i
    '                    Exit For
    '                End If
    '            Next
    '            cboLineID.Enabled = False
    '        Else
    '            cboLineID.SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End If
    '        If cboLineID.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboLineID.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("LineCode", a)
    '        data.LineCode = HideValue.Get("LineCode")
    '        '======================================================'


    '        '============== FILL COMBO ITEM CHECK =================='
    '        dt = clsProdSampleVerificationDB.FillCombo(ItemCheck_Sel, data, ErrMsg)
    '        With cboItemCheck
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '        If prmItemCheck <> "" Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("CODE") = prmItemCheck Then
    '                    cboItemCheck.SelectedIndex = i
    '                    Exit For
    '                End If
    '            Next
    '            cboItemCheck.Enabled = False
    '        Else
    '            cboItemCheck.SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End If
    '        If cboItemCheck.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboItemCheck.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("ItemCheck_Code", a)
    '        data.ItemCheck_Code = HideValue.Get("ItemCheck_Code")
    '        '======================================================'

    '        '============== FILL COMBO SHIFY =================='
    '        dt = clsProdSampleVerificationDB.FillCombo(Shift_Sel, data, ErrMsg)
    '        With cboShift
    '            .DataSource = dt
    '            .DataBind()
    '            .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End With
    '        If prmShifCode <> "" Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("CODE") = prmShifCode Then
    '                    cboShift.SelectedIndex = i
    '                    Exit For
    '                End If
    '            Next
    '            cboShift.Enabled = False
    '        Else
    '            cboShift.SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End If
    '        If cboShift.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboShift.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("ShiftCode", a)
    '        data.ShiftCode = HideValue.Get("ShiftCode")
    '        '======================================================'

    '        '============== FILL COMBO SEQ =================='
    '        dt = clsProdSampleVerificationDB.FillCombo(Seq_Sel, data, ErrMsg)
    '        With cboSeq
    '            .DataSource = dt
    '            .DataBind()
    '            .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End With
    '        If prmSeqNo <> "" Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("CODE") = prmSeqNo Then
    '                    cboSeq.SelectedIndex = i
    '                    Exit For
    '                End If
    '            Next
    '            cboSeq.Enabled = False
    '        Else
    '            cboSeq.SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End If
    '        If cboSeq.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboSeq.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("Seq", a)
    '        data.Seq = HideValue.Get("Seq")
    '        '======================================================'

    '    Catch ex As Exception
    '        show_error(MsgTypeEnum.Info, "", 0)
    '    End Try
    'End Sub

#End Region
End Class