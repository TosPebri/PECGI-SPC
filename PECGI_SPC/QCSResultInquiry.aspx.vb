Imports DevExpress.Web
Imports System.Data.SqlClient
Imports OfficeOpenXml
Imports System.IO

Public Class QCSResultInquiry
    Inherits System.Web.UI.Page
    Dim LastCycle As Integer
    Dim pUser As String = ""
    Public AuthApprove As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public ValueType As String
    Dim GlobalPrm As String = ""
    Dim NoProcess11 As String
    Dim NoProcess12 As String
    Dim NoProcess13 As String
    Dim NoProcess14 As String
    Dim NoProcess15 As String

    Dim NoProcess21 As String
    Dim NoProcess22 As String
    Dim NoProcess23 As String
    Dim NoProcess24 As String
    Dim NoProcess25 As String

    Dim NoProcess31 As String
    Dim NoProcess32 As String
    Dim NoProcess33 As String
    Dim NoProcess34 As String
    Dim NoProcess35 As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GlobalPrm = Request.QueryString("Date") & ""
        sGlobal.getMenu("B040")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user") & ""
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B040")
        show_error(MsgTypeEnum.Info, "", 0)
        up_FillComboLine()
        If Not IsPostBack And Not IsCallback Then
            If GlobalPrm <> "" Then
                dtDate.Value = CDate(Request.QueryString("Date"))

                Dim pLineID As String = Request.QueryString("LineID")
                cboLineID.Value = pLineID
                Dim dt As DataTable
                dt = ClsSubLineDB.GetDataSubLine(pLineID, "")
                cboSubLine.DataSource = dt
                cboSubLine.DataBind()
                Dim pSubLineID As String = Request.QueryString("SubLineID") & ""
                cboSubLine.Value = pSubLineID

                dt = clsQCSResultDB.GetPart(pLineID)
                cboPartID.DataSource = dt
                cboPartID.DataBind()
                Dim pPartID As String = Request.QueryString("PartID") & ""
                cboPartID.Value = pPartID

                txtpartname.Text = Request.QueryString("PartName")
                Dim pShift As String = cboShift.Value
                Dim Rv As List(Of clsQCSResultShift) = clsQCSResultDB.GetRevNo(dtDate.Value, pPartID, pLineID, pSubLineID, pShift)
                If Rv.Count > 0 Then
                    Dim Revlist As New List(Of String)
                    For Each Rev In Rv
                        cboRevNo.Items.Add(Rev.RevNo)
                    Next
                    cboRevNo.SelectedIndex = 0
                    cboRevNo.ClientEnabled = Rv.Count > 1
                End If
                GridLoad(Format(dtDate.Value, "yyyy-MM-dd"), pLineID, pSubLineID, cboPartID.Value, cboShift.Value, cboRevNo.Value, txtLotNo.Text)
            Else
                dtDate.Value = Now.Date
            End If
        End If
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        grid.JSProperties("cp_message") = ErrMsg
        grid.JSProperties("cp_type") = msgType
        grid.JSProperties("cp_val") = pVal
    End Sub

    Private Sub up_FillComboLine()
        Dim ErrMsg As String = ""
        Dim dsline As DataTable
        Dim pStatus As Integer
        If pUser = "" Then
            Return
        End If
        If ClsQCSMasterDB.GetDataQE(pUser, "") = True Then
            pStatus = 1
        Else
            pStatus = 0
        End If
        dsline = ClsQCSMasterDB.GetDataLine(pStatus, pUser, ErrMsg)
        If ErrMsg = "" Then
            cboLineID.DataSource = dsline
            cboLineID.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub SetHeader(pShift As Integer)
        With grid
            Dim qt As clsQCSTimeCycle = clsQCSTimeDB.GetCycle(1)
            Dim qs As clsQCSLastUpdate = clsQCSTimeDB.GetLastUpdate(dtDate.Value, cboLineID.Value, cboSubLine.Value, cboPartID.Value, cboRevNo.Value)

            .Columns("Cycle11").Caption = "STD " & qt.Cycle1Time & vbCrLf & "ACT " & qs.LastUpdate11
            .Columns("Cycle12").Caption = "STD " & qt.Cycle2Time & vbCrLf & "ACT " & qs.LastUpdate12
            .Columns("Cycle13").Caption = "STD " & qt.Cycle3Time & vbCrLf & "ACT " & qs.LastUpdate13
            .Columns("Cycle14").Caption = "STD " & qt.Cycle4Time & vbCrLf & "ACT " & qs.LastUpdate14
            .Columns("Cycle15").Caption = "STD " & qt.Cycle5Time & vbCrLf & "ACT " & qs.LastUpdate15

            qt = clsQCSTimeDB.GetCycle(2)
            .Columns("Cycle21").Caption = "STD " & qt.Cycle1Time & vbCrLf & "ACT " & qs.LastUpdate21
            .Columns("Cycle22").Caption = "STD " & qt.Cycle2Time & vbCrLf & "ACT " & qs.LastUpdate22
            .Columns("Cycle23").Caption = "STD " & qt.Cycle3Time & vbCrLf & "ACT " & qs.LastUpdate23
            .Columns("Cycle24").Caption = "STD " & qt.Cycle4Time & vbCrLf & "ACT " & qs.LastUpdate24
            .Columns("Cycle25").Caption = "STD " & qt.Cycle5Time & vbCrLf & "ACT " & qs.LastUpdate25

            qt = clsQCSTimeDB.GetCycle(3)
            .Columns("Cycle31").Caption = "STD " & qt.Cycle1Time & vbCrLf & "ACT " & qs.LastUpdate31
            .Columns("Cycle32").Caption = "STD " & qt.Cycle2Time & vbCrLf & "ACT " & qs.LastUpdate32
            .Columns("Cycle33").Caption = "STD " & qt.Cycle3Time & vbCrLf & "ACT " & qs.LastUpdate33
            .Columns("Cycle34").Caption = "STD " & qt.Cycle4Time & vbCrLf & "ACT " & qs.LastUpdate34
            .Columns("Cycle35").Caption = "STD " & qt.Cycle5Time & vbCrLf & "ACT " & qs.LastUpdate35

            .Columns("Cycle11").ParentBand.ParentBand.Visible = pShift = 1 Or pShift = 0
            .Columns("Cycle21").ParentBand.ParentBand.Visible = pShift = 2 Or pShift = 0
            .Columns("Cycle31").ParentBand.ParentBand.Visible = pShift = 3 Or pShift = 0
        End With
    End Sub

    Private Sub Grid_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles grid.HtmlDataCellPrepared
        If e.DataColumn.FieldName = "SeqNo" Or e.DataColumn.FieldName = "ProcessName" Or e.DataColumn.FieldName = "KPointStatus" _
        Or e.DataColumn.FieldName = "Item" Or e.DataColumn.FieldName = "Standard" Then
            If e.KeyValue >= 998 Then
                e.Cell.BackColor = System.Drawing.Color.Silver
            End If
        End If
        If e.KeyValue = 2000 Then
            If e.DataColumn.FieldName = "Cycle11" Or e.DataColumn.FieldName = "Cycle12" Or e.DataColumn.FieldName = "Cycle13" Then
                e.Cell.ColumnSpan = 5
                e.Cell.HorizontalAlign = HorizontalAlign.Left
            ElseIf e.DataColumn.FieldName <> "MeasuringInstrument" Then
                'e.Cell.Visible = False
                'e.Cell.BorderStyle = BorderStyle.None
            End If
        End If
        For iShift = 1 To 3
            For i = 1 To 5
                Dim fn As String = "Cycle" & iShift & i
                Dim NoProcess As String = ""
                If iShift = 1 Then
                    If i = 1 Then
                        NoProcess = NoProcess11
                    ElseIf i = 2 Then
                        NoProcess = NoProcess12
                    ElseIf i = 3 Then
                        NoProcess = NoProcess13
                    ElseIf i = 4 Then
                        NoProcess = NoProcess14
                    ElseIf i = 5 Then
                        NoProcess = NoProcess15
                    End If
                ElseIf iShift = 2 Then
                    If i = 1 Then
                        NoProcess = NoProcess21
                    ElseIf i = 2 Then
                        NoProcess = NoProcess22
                    ElseIf i = 3 Then
                        NoProcess = NoProcess23
                    ElseIf i = 4 Then
                        NoProcess = NoProcess24
                    ElseIf i = 5 Then
                        NoProcess = NoProcess25
                    End If
                ElseIf iShift = 3 Then
                    If i = 1 Then
                        NoProcess = NoProcess31
                    ElseIf i = 2 Then
                        NoProcess = NoProcess32
                    ElseIf i = 3 Then
                        NoProcess = NoProcess33
                    ElseIf i = 4 Then
                        NoProcess = NoProcess34
                    ElseIf i = 5 Then
                        NoProcess = NoProcess35
                    End If
                End If
                Dim KeyValue As Integer = 0
                If e.DataColumn.FieldName = fn Then
                    If NoProcess = "1" Or NoProcess = "2" Or NoProcess = "3" Or NoProcess = "4" Then
                        If e.KeyValue IsNot Nothing Then
                            KeyValue = e.KeyValue.ToString.Split("|")(0)
                        End If
                        If KeyValue < 1000 Then
                            e.Cell.BackColor = System.Drawing.Color.Yellow
                        End If
                    ElseIf e.GetValue("Status" & iShift & i) & "" = "NG" Then
                        e.Cell.BackColor = System.Drawing.Color.Red
                    End If
                End If
            Next
        Next
    End Sub

    Private Sub GridLoad(pDate As Date, pLineID As String, pSubLineID As String, ByVal pPartID As String, ByVal pShift As Integer, pRevNo As String, pLotNo As String)
        Dim ErrMsg As String = ""
        If pLineID Is Nothing Then
            Return
        End If
        Dim ds As DataSet
        Dim dt As DataTable
        If pLotNo = "" Then
            ds = clsQCSResultDB.GetTableInquiry(Format(pDate, "yyyy-MM-dd"), pLineID, pSubLineID, pPartID, pShift, Val(pRevNo))
            dt = ds.Tables(0)
        Else
            ds = clsQCSResultDB.GetTableInquiry(pLotNo)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    Dim Part As ClsPart = clsQCSResultDB.GetPartName(.Item("PartID") & "")
                    If Part IsNot Nothing Then
                        grid.JSProperties("cp_PartName") = Part.PartName
                    End If
                    grid.JSProperties("cp_Date") = CDate(.Item("Date"))
                    grid.JSProperties("cp_LineID") = RTrim(.Item("LineID") & "")
                    grid.JSProperties("cp_SubLineID") = RTrim(.Item("SubLineID") & "")
                    grid.JSProperties("cp_PartID") = RTrim(.Item("PartID") & "")
                    grid.JSProperties("cp_RevNo") = CInt(Val(.Item("RevNo") & ""))
                End With
            End If
        End If

        If ErrMsg = "" Then
            If dt.Rows.Count = 0 Then
                NoProcess11 = ""
                NoProcess12 = ""
                NoProcess13 = ""
                NoProcess14 = ""
                NoProcess15 = ""

                NoProcess21 = ""
                NoProcess22 = ""
                NoProcess23 = ""
                NoProcess24 = ""
                NoProcess25 = ""

                NoProcess31 = ""
                NoProcess32 = ""
                NoProcess33 = ""
                NoProcess34 = ""
                NoProcess35 = ""
            Else
                Dim iRow As Integer = 0
                For iRow = 0 To dt.Rows.Count - 1
                    If dt.Rows(iRow)("LevelNo") = 1001 Then
                        Exit For
                    End If
                Next
                If iRow > 0 Then
                    NoProcess11 = Val(dt.Rows(iRow)("Cycle11") & "")
                    NoProcess12 = Val(dt.Rows(iRow)("Cycle12") & "")
                    NoProcess13 = Val(dt.Rows(iRow)("Cycle13") & "")
                    NoProcess14 = Val(dt.Rows(iRow)("Cycle14") & "")
                    NoProcess15 = Val(dt.Rows(iRow)("Cycle15") & "")

                    NoProcess21 = Val(dt.Rows(iRow)("Cycle21") & "")
                    NoProcess22 = Val(dt.Rows(iRow)("Cycle22") & "")
                    NoProcess23 = Val(dt.Rows(iRow)("Cycle23") & "")
                    NoProcess24 = Val(dt.Rows(iRow)("Cycle24") & "")
                    NoProcess25 = Val(dt.Rows(iRow)("Cycle25") & "")

                    NoProcess31 = Val(dt.Rows(iRow)("Cycle31") & "")
                    NoProcess32 = Val(dt.Rows(iRow)("Cycle32") & "")
                    NoProcess33 = Val(dt.Rows(iRow)("Cycle33") & "")
                    NoProcess34 = Val(dt.Rows(iRow)("Cycle34") & "")
                    NoProcess35 = Val(dt.Rows(iRow)("Cycle35") & "")
                End If
            End If

            LastCycle = 0
            grid.DataSource = dt
            grid.DataBind()
            SetHeader(pShift)
            grid.JSProperties("cpEnableApprove") = ""
            grid.JSProperties("cpEnableSave") = ""

            Dim dt2 As DataTable = ds.Tables(1)
            If dt2.Rows.Count > 0 Then
                With ds.Tables(1).Rows(0)
                    For iShift = 1 To 3
                        For iCycle = 1 To 5
                            Dim ShiftCycle As String = "Notes" & iShift.ToString & iCycle.ToString
                            Session("cp" & ShiftCycle) = .Item(ShiftCycle)
                        Next
                    Next
                End With
            End If
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub grid_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs) Handles grid.BatchUpdate
        Dim R As New clsQCSResult
        R.QCSDate = dtDate.Value
        R.LineID = cboLineID.Value
        R.SubLineID = cboSubLine.Value
        R.PartID = cboPartID.Value
        R.CreateUser = Session("user") & ""

        Dim Tr As SqlTransaction
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Tr = Cn.BeginTransaction(IsolationLevel.ReadCommitted)
            clsQCSResultDB.Insert(R, Cn, Tr)

            Dim Rs As New clsQCSResultShift
            Rs.QCSResultID = R.QCSResultID
            Rs.Shift = cboShift.Value
            clsQCSResultShiftDB.Insert(Rs, Cn, Tr)

            For iCycle = 1 To 5
                Dim Rsc As New clsQCSResultShiftCycle
                Rsc.QCSResultShiftID = Rs.QCSResultShiftID
                Rsc.Cycle = iCycle
                Rsc.LotNo = ""
                Rsc.PIC = ""
                clsQCSResultShiftCycleDB.Insert(Rsc, Cn, Tr)

                For i As Integer = 0 To e.UpdateValues.Count - 1
                    If e.UpdateValues(i).Keys("LevelNo") = 998 Then
                        Rsc.LotNo = e.UpdateValues(i).NewValues("Cycle" & iCycle) & ""
                        clsQCSResultShiftCycleDB.UpdateLotNo(Rsc, Cn, Tr)
                    ElseIf e.UpdateValues(i).Keys("LevelNo") = 999 Then
                        Rsc.PIC = e.UpdateValues(i).NewValues("Cycle" & iCycle) & ""
                        clsQCSResultShiftCycleDB.UpdatePIC(Rsc, Cn, Tr)
                    Else
                        Dim LineID As String = e.UpdateValues(i).OldValues("LineID")
                        Dim ItemID As String = e.UpdateValues(i).OldValues("ItemID")
                        Dim Cycle As String = e.UpdateValues(i).NewValues.Keys.Count

                        Dim Rsci As New clsQCSResultShiftCycleItem
                        Rsci.QCSResultShiftCycleID = Rsc.QCSResultShiftCycleID
                        Rsci.ItemID = ItemID
                        Dim Value As String = e.UpdateValues(i).NewValues("Cycle" & iCycle)
                        If IsDBNull(e.UpdateValues(i).NewValues("Cycle" & iCycle)) Then
                            Rsci.NumValue = Nothing
                        ElseIf IsNumeric(e.UpdateValues(i).NewValues("Cycle" & iCycle)) Then
                            Rsci.NumValue = e.UpdateValues(i).NewValues("Cycle" & iCycle)
                        Else
                            Rsci.TextValue = e.UpdateValues(i).NewValues("Cycle" & iCycle) & ""
                        End If
                        clsQCSResultShiftCycleItemDB.Insert(Rsci, Cn, Tr)
                    End If
                Next
            Next
            Tr.Commit()
        End Using
    End Sub

    Private Sub grid_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles grid.CustomCallback
        Dim pFunction As String = Split(e.Parameters, "|")(0)
        Select Case pFunction
            Case "clear"
                GridLoad(Now.Date, "", "", "", 0, 0, "")
                grid.JSProperties("cp_cleargrid") = 1
            Case "load", "save", "approve"
                Dim pDate As String = Split(e.Parameters, "|")(1)
                Dim pLineID As String = Split(e.Parameters, "|")(2)
                Dim pSubLineID As String = Split(e.Parameters, "|")(3)
                Dim pPartID As String = Split(e.Parameters, "|")(4)
                Dim pShift As Integer = Val(Split(e.Parameters, "|")(5))
                Dim pRevNo As String = Split(e.Parameters, "|")(6)
                Dim pLotNo As String = Split(e.Parameters, "|")(7)
                GridLoad(pDate, pLineID, pSubLineID, pPartID, pShift, pRevNo, pLotNo)
        End Select
        If pFunction = "save" Then
            show_error(MsgTypeEnum.Success, "Save data successful", 1)
        ElseIf pFunction = "approve" Then
            show_error(MsgTypeEnum.Success, "Approve data successful", 1)
        End If
    End Sub

    Protected Sub grid_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles grid.RowInserting
        e.Cancel = True
    End Sub

    Private Sub grid_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles grid.RowUpdating
        e.Cancel = True
    End Sub

    Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles grid.RowDeleting
        e.Cancel = True
    End Sub

    Protected Sub grid_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles grid.StartRowEditing

    End Sub

    Protected Sub grid_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs)

    End Sub

    Protected Sub grid_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles grid.AfterPerformCallback
        Dim commandColumn = TryCast(grid.Columns(0), GridViewCommandColumn)
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            GridLoad(Format(dtDate.Value, "yyyy-MM-dd"), cboLineID.Value, cboSubLine.Value, cboPartID.Value, cboShift.Value, cboRevNo.Value, txtLotNo.Text)
        End If
    End Sub

    Protected Sub grid_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles grid.CellEditorInitialize

    End Sub

    Private Sub cbopartid_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cboPartID.Callback
        Dim pLineID As String = Split(e.Parameter, "|")(1)
        Dim dt As DataTable
        dt = clsQCSResultDB.GetPart(pLineID)
        cboPartID.DataSource = dt
        cboPartID.DataBind()
    End Sub

    Private Sub cboSubLine_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cboSubLine.Callback
        Dim pLineID As String = Split(e.Parameter, "|")(1)
        Dim dt As DataTable
        dt = ClsSubLineDB.GetDataSubLine(pLineID, "")
        cboSubLine.DataSource = dt
        cboSubLine.DataBind()
    End Sub

    Private Function Adbl(value As Object) As String
        If IsDBNull(value) Then
            Return ""
        ElseIf IsNumeric(value) Then
            Return CDbl(value)
        Else
            Return ""
        End If
    End Function

    Private Sub DownloadExcel()
        Dim CatererID As String = Session("CatererID") & ""
        Dim dt As New DataTable

        Dim RevNo As Integer
        If hfRevNo.Count = 0 Then
            RevNo = cboRevNo.Value
        Else
            RevNo = Val(hfRevNo("revno"))
        End If


        Dim ds As DataSet
        If txtLotNo.Text = "" Then
            ds = clsQCSResultDB.GetTableInquiry(Format(dtDate.Value, "yyyy-MM-dd"), cboLineID.Value, cboSubLine.Value, cboPartID.Value, cboShift.Value, RevNo)
        Else
            ds = clsQCSResultDB.GetTableInquiry(txtLotNo.Text)
        End If        
        dt = ds.Tables(0)
        If dt.Rows.Count = 0 Then            
            Return
        End If
        Dim colShift1 As Integer = 7
        Dim colShift2 As Integer = 12
        Dim colShift3 As Integer = 17
        Dim dt2 As DataTable = ds.Tables(1)
        Using Pck As New ExcelPackage
            Dim ws As ExcelWorksheet = Pck.Workbook.Worksheets.Add("Sheet1")
            With ws
                .Cells(1, 1, 1, 1).Value = "No"
                .Cells(1, 2, 1, 2).Value = "Process"
                .Cells(1, 3, 1, 3).Value = "KPoint Status"
                .Cells(1, 4, 1, 4).Value = "Item"
                .Cells(1, 5, 1, 5).Value = "Standard"
                .Cells(1, 6, 1, 6).Value = "Range"

                .Cells(1, colShift1).Value = "SHIFT 1"
                .Cells(2, colShift1).Value = "1"
                .Cells(2, 8).Value = "2"
                .Cells(2, 9).Value = "3"
                .Cells(2, 10).Value = "4"
                .Cells(2, 11).Value = "5"

                .Cells(1, colShift2).Value = "SHIFT 2"
                .Cells(2, colShift2).Value = "1"
                .Cells(2, 13).Value = "2"
                .Cells(2, 14).Value = "3"
                .Cells(2, 15).Value = "4"
                .Cells(2, 16).Value = "5"

                .Cells(1, colShift3).Value = "SHIFT 3"
                .Cells(2, colShift3).Value = "1"
                .Cells(2, 18).Value = "2"
                .Cells(2, 19).Value = "3"
                .Cells(2, 20).Value = "4"
                .Cells(2, 21).Value = "5"

                Dim n As Integer = dt.Rows.Count + 2
                .Cells(1, 1, n, 21).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                .Cells(1, 1, n, 21).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                .Cells(1, 1, n, 21).Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                .Cells(1, 1, n, 21).Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                .Cells(3, 7, n - 5, 21).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                .Cells(n - 4, 7, n, 21).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left

                .Cells(1, 1, 2, 21).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                .Cells(1, 1, 2, 21).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                .Cells(1, 1, 2, 21).Style.Font.Color.SetColor(System.Drawing.Color.White)

                .Cells(1, 1, 2, 21).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                .Cells(1, 1, 2, 21).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                .Cells(1, 1, 2, 21).Style.WrapText = True
                .Column(1).Width = 4
                .Column(2).Width = 16
                .Column(3).Width = 6
                .Column(4).Width = 25
                .Column(5).Width = 20
                .Column(6).Width = 20

                For i = 1 To 6
                    .Cells(1, i, 2, i).Merge = True
                Next
                .Cells(1, 7, 1, 11).Merge = True
                .Cells(1, 12, 1, 16).Merge = True
                .Cells(1, 17, 1, 21).Merge = True
                .Row(1).Height = 15
                For i = 7 To 21
                    .Column(i).Width = 8
                Next
                For iRow = 0 To dt.Rows.Count - 1
                    .Cells(iRow + 3, 1).Value = dt.Rows(iRow)("SeqNo")
                    .Cells(iRow + 3, 2).Value = dt.Rows(iRow)("ProcessName")
                    .Cells(iRow + 3, 3).Value = dt.Rows(iRow)("KPointStatus")
                    .Cells(iRow + 3, 4).Value = dt.Rows(iRow)("Item")
                    .Cells(iRow + 3, 5).Value = dt.Rows(iRow)("Standard")
                    .Cells(iRow + 3, 6).Value = dt.Rows(iRow)("Range")

                    .Cells(iRow + 3, 7).Value = dt.Rows(iRow)("Cycle11")
                    .Cells(iRow + 3, 8).Value = dt.Rows(iRow)("Cycle12")
                    .Cells(iRow + 3, 9).Value = dt.Rows(iRow)("Cycle13")
                    .Cells(iRow + 3, 10).Value = dt.Rows(iRow)("Cycle14")
                    .Cells(iRow + 3, 11).Value = dt.Rows(iRow)("Cycle15")

                    .Cells(iRow + 3, 12).Value = dt.Rows(iRow)("Cycle21")
                    .Cells(iRow + 3, 13).Value = dt.Rows(iRow)("Cycle22")
                    .Cells(iRow + 3, 14).Value = dt.Rows(iRow)("Cycle23")
                    .Cells(iRow + 3, 15).Value = dt.Rows(iRow)("Cycle24")
                    .Cells(iRow + 3, 16).Value = dt.Rows(iRow)("Cycle25")

                    .Cells(iRow + 3, 17).Value = dt.Rows(iRow)("Cycle31")
                    .Cells(iRow + 3, 18).Value = dt.Rows(iRow)("Cycle32")
                    .Cells(iRow + 3, 19).Value = dt.Rows(iRow)("Cycle33")
                    .Cells(iRow + 3, 20).Value = dt.Rows(iRow)("Cycle34")
                    .Cells(iRow + 3, 21).Value = dt.Rows(iRow)("Cycle35")

                    If dt2.Rows.Count > 0 Then
                        If dt.Rows(iRow)("Status11") & "" = "NG" And dt2.Rows(0)("Notes11") & "" = "" Then
                            .Cells(iRow + 3, 7, iRow + 3, 7).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 7, iRow + 3, 7).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                        If dt.Rows(iRow)("Status12") & "" = "NG" And dt2.Rows(0)("Notes12") & "" = "" Then
                            .Cells(iRow + 3, 8, iRow + 3, 8).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 8, iRow + 3, 8).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                        If dt.Rows(iRow)("Status13") & "" = "NG" And dt2.Rows(0)("Notes13") & "" = "" Then
                            .Cells(iRow + 3, 9, iRow + 3, 9).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 9, iRow + 3, 9).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                        If dt.Rows(iRow)("Status14") & "" = "NG" And dt2.Rows(0)("Notes14") & "" = "" Then
                            .Cells(iRow + 3, 10, iRow + 3, 10).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 10, iRow + 3, 10).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                        If dt.Rows(iRow)("Status15") & "" = "NG" And dt2.Rows(0)("Notes15") & "" = "" Then
                            .Cells(iRow + 3, 11, iRow + 3, 11).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 11, iRow + 3, 11).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If

                        If dt.Rows(iRow)("Status21") & "" = "NG" Then
                            .Cells(iRow + 3, 12, iRow + 3, 12).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 12, iRow + 3, 12).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                        If dt.Rows(iRow)("Status22") & "" = "NG" Then
                            .Cells(iRow + 3, 13, iRow + 3, 13).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 13, iRow + 3, 13).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                        If dt.Rows(iRow)("Status23") & "" = "NG" Then
                            .Cells(iRow + 3, 14, iRow + 3, 14).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 14, iRow + 3, 14).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                        If dt.Rows(iRow)("Status24") & "" = "NG" Then
                            .Cells(iRow + 3, 15, iRow + 3, 15).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 15, iRow + 3, 15).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                        If dt.Rows(iRow)("Status25") & "" = "NG" Then
                            .Cells(iRow + 3, 16, iRow + 3, 16).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 16, iRow + 3, 16).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If

                        If dt.Rows(iRow)("Status31") & "" = "NG" Then
                            .Cells(iRow + 3, 17, iRow + 3, 17).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 17, iRow + 3, 17).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                        If dt.Rows(iRow)("Status32") & "" = "NG" Then
                            .Cells(iRow + 3, 18, iRow + 3, 18).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 18, iRow + 3, 18).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                        If dt.Rows(iRow)("Status33") & "" = "NG" Then
                            .Cells(iRow + 3, 19, iRow + 3, 19).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 19, iRow + 3, 19).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                        If dt.Rows(iRow)("Status34") & "" = "NG" Then
                            .Cells(iRow + 3, 20, iRow + 3, 20).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 20, iRow + 3, 20).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                        If dt.Rows(iRow)("Status35") & "" = "NG" Then
                            .Cells(iRow + 3, 21, iRow + 3, 21).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(iRow + 3, 21, iRow + 3, 21).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                        End If
                    End If
                Next
                .Cells(n - 1, 12).Value = .Cells(n - 1, 8).Value
                .Cells(n - 1, 17).Value = .Cells(n - 1, 9).Value

                .Cells(n, 12).Value = .Cells(n, 8).Value
                .Cells(n, 17).Value = .Cells(n, 9).Value

                If .Cells(n, 1).Value & "" = "" Then
                    .Cells(n, 7, n, 11).Merge = True
                    .Cells(n, 12, n, 16).Merge = True
                    .Cells(n, 17, n, 21).Merge = True
                    .Cells(n - 4, 1, n, 5).Merge = True
                    .Cells(n - 4, 1, n, 5).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    .Cells(n - 4, 1, n, 5).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)
                End If
                If n > 5 Then
                    For iCol = 7 To 21
                        If .Cells(n - 1, iCol).Value & "" <> "" And .Cells(2, iCol).Value & "" <> "" Then
                            .Cells(3, iCol, n - 5, iCol).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(3, iCol, n - 5, iCol).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                        End If
                    Next
                End If

                .InsertRow(1, 7)
                .Cells(3, 1).Value = "Date"
                .Cells(4, 1).Value = "Shift"
                .Cells(5, 1).Value = "Line No."
                .Cells(6, 1).Value = "Sub Line No."
                .Cells(3, 6).Value = "Part No."
                .Cells(4, 6).Value = "Part Name"
                .Cells(5, 6).Value = "Lot No."
                If txtLotNo.Text = "" Then
                    .Cells(3, 3).Value = ": " & Format(dtDate.Value, "dd MMM yyyy")
                    .Cells(4, 3).Value = ": " & cboShift.Text
                    .Cells(5, 3).Value = ": " & cboLineID.Text
                    .Cells(6, 3).Value = ": " & cboSubLine.Text
                    .Cells(3, 7).Value = ": " & cboPartID.Value
                    .Cells(4, 7).Value = ": " & txtPartName.Text
                Else
                    If dt.Rows.Count > 0 Then
                        .Cells(3, 3).Value = ": " & Format(dt.Rows(0)("Date"), "dd MMM yyyy")
                        .Cells(4, 3).Value = ": ALL"
                        .Cells(5, 3).Value = ": " & dt.Rows(0)("LineID")
                        .Cells(6, 3).Value = ": " & dt.Rows(0)("SubLineID")
                        .Cells(3, 7).Value = ": " & dt.Rows(0)("PartID")
                        Dim Part As ClsPart = clsQCSResultDB.GetPartName(dt.Rows(0)("PartID") & "")
                        If Part IsNot Nothing Then
                            .Cells(4, 7).Value = ": " & Part.PartName
                        End If
                        .Cells(5, 7).Value = ": " & txtLotNo.Text
                    End If
                End If
                If cboShift.Value = 1 Then
                    .DeleteColumn(colShift3, 5)
                    .DeleteColumn(colShift2, 5)
                ElseIf cboShift.Value = 2 Then
                    .DeleteColumn(colShift3, 5)
                    .DeleteColumn(colShift1, 5)
                ElseIf cboShift.Value = 3 Then
                    .DeleteColumn(colShift2, 5)
                    .DeleteColumn(colShift1, 5)
                End If
                .Cells(1, 1).Style.Font.Bold = True
                .Cells(1, 1).Style.Font.Size = 16
                .Cells(1, 1).Value = "SQC Result Inquiry"
                .Cells(1, 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                .Cells(1, 1, 1, 13).Merge = True
                Dim FinalRow As Integer = .Dimension.End.Row
                Dim ColumnString As String = "A2:M" & FinalRow
                .Cells(ColumnString).Style.Font.Name = "Segoe UI"
                .Cells(ColumnString).Style.Font.Size = 10
            End With

            Dim stream As MemoryStream = New MemoryStream(Pck.GetAsByteArray())
            Response.AppendHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            Response.AppendHeader("Content-Disposition", "attachment; filename=SQCResult_" & Format(Date.Now, "yyyy-MM-dd") & ".xlsx")
            Response.BinaryWrite(stream.ToArray())
            Response.End()

        End Using
    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        DownloadExcel()
    End Sub

    Private Sub cbkRefresh_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkRefresh.Callback
        Dim pDate As String = Split(e.Parameter, "|")(1)
        Dim pLineID As String = Split(e.Parameter, "|")(2)
        Dim pSubLineID As String = Split(e.Parameter, "|")(3)
        Dim pPartID As String = Split(e.Parameter, "|")(4)
        Dim pShift As String = Split(e.Parameter, "|")(5)
        Dim pLotNo As String = Split(e.Parameter, "|")(6)
        FillCboRevNo(pDate, pPartID, pLineID, pSubLineID, pShift)
    End Sub

    Private Sub FillCboRevNo(pDate As Date, pPartID As String, pLineID As String, pSubLineID As String, pShift As String)
        Dim Rv As List(Of clsQCSResultShift) = clsQCSResultDB.GetRevNo(pDate, pPartID, pLineID, pSubLineID, pShift)
        If Rv.Count = 0 Then
            cbkRefresh.JSProperties("cpRevNoList") = ""
            cbkRefresh.JSProperties("cpRevNo") = ""
            cbkRefresh.JSProperties("cpRevDate") = ""
        Else
            Dim Revlist As New List(Of String)
            For Each Rev In Rv
                Revlist.Add(Rev.RevNo)
            Next
            cbkRefresh.JSProperties("cpRevNoList") = Revlist.ToArray
            cbkRefresh.JSProperties("cpRevNo") = Rv(0).RevNo.ToString
            cbkRefresh.JSProperties("cpRevDate") = Format(Rv(0).RevDate, "dd MMM yyyy")
        End If
    End Sub
End Class