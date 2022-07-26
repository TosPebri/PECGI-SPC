Imports DevExpress.Web.ASPxGridView
Imports System.Data.SqlClient
Imports DevExpress.XtraCharts
Imports System.Drawing
Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Web.ASPxEditors
Imports OfficeOpenXml
Imports System.IO
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrintingLinks
Imports DevExpress.XtraCharts.Native

Public Class QCSXRChart
    Inherits System.Web.UI.Page
    Dim LastCycle As Integer
    Dim pUser As String = ""
    Public AuthApprove As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public ValueType As String
    Dim MinCount As Integer = 1
    Dim MaxCount As Integer = 60
    Dim XRList As List(Of clsXRView)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sGlobal.getMenu("B070")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user") & ""
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B070")
        show_error(MsgTypeEnum.Info, "", 0)
        FillCboLine()
        'If Not IsPostBack And Not IsCallback Then
        '    dtFrom.Value = "03 Des 2018"
        '    cboLineID.Value = "MGS"
        '    cboSubLine.Value = "MGS01"
        '    cboMachine.Value = "45033"
        '    cboPartID.Value = "23541-5T0-3001"

        '    Dim LineID As String = "MGS"
        '    Dim PartID As String = "23541-5T0-3001"
        '    Dim dt As DataTable = clsXRChartDB.GetXRCode(PartID, LineID)
        '    cboItem.DataSource = dt
        '    cboItem.DataBind()
        'End If
        GridLoadXR()
    End Sub

    Private Sub GridLoadXR()
        Dim ds As DataSet = ClsQCSXRDataDB.GetList
        gridDet.DataSource = ds.Tables(0)
        gridDet.DataBind()
    End Sub

    Private Sub GridLoadAction(pDateFrom As Date, pDateTo As Date, pLineID As String, pSubLineID As String, pProcess As String, pPartID As String, pXRCode As String)
        Dim dt As DataTable = clsQCSXRActionDB.GetTable(pDateFrom, pDateTo, pLineID, pSubLineID, pProcess, pPartID, pXRCode)
        gridAction.DataSource = dt
        gridAction.DataBind()
    End Sub

    Private Sub GridLoadSummary(pDateFrom As Date, pDateTo As Date, pLineID As String, pSubLineID As String, pPartID As String, pMachineNo As String, ItemID As String, pUCL As String, pLCL As String)
        pUCL = Val(pUCL)
        pLCL = Val(pLCL)
        ItemID = Val(ItemID)
        Dim dt As DataTable = clsXRChartDB.GetSummary(pDateFrom, pDateTo, pLineID, pSubLineID, pPartID, pMachineNo, ItemID, pUCL, pLCL)
        gridSummary.DataSource = dt
        gridSummary.DataBind()
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        grid.JSProperties("cp_message") = ErrMsg
        grid.JSProperties("cp_type") = msgType
        grid.JSProperties("cp_val") = pVal
    End Sub

    Private Sub ShowMsgAction(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        gridAction.JSProperties("cp_message") = ErrMsg
        gridAction.JSProperties("cp_type") = msgType
        gridAction.JSProperties("cp_val") = pVal
    End Sub

    Private Sub GridLoad(pDateFrom As Date, pDateTo As Date, pLineNo As String, pSubLine As String, pMachineNo As String, pPartNo As String, pItem As String, pProcess As String, pXRCode As String)
        With grid
            .Columns.Clear()
            Dim Col0 As New GridViewDataTextColumn
            Col0.FieldName = "Seq"
            Col0.Caption = "Seq"
            Col0.Visible = False
            .Columns.Add(Col0)

            Dim Band1 As New GridViewBandColumn
            Band1.Caption = "DATE"
            .Columns.Add(Band1)

            Dim Band2 As New GridViewBandColumn
            Band2.Caption = "SHIFT"
            Band1.Columns.Add(Band2)

            Dim Col1 As New GridViewDataTextColumn
            Col1.FieldName = "Des"
            Col1.Caption = "SEQUENCE"
            Col1.Width = 90
            Col1.FixedStyle = GridViewColumnFixedStyle.Left
            Band2.Columns.Add(Col1)
            pItem = Val(pItem)
            Dim ds As DataSet = clsXRChartDB.GetDataset(pDateFrom, pDateTo, pPartNo, pLineNo, pSubLine, pItem)
            Dim dt0 As DataTable = ds.Tables(0)
            Dim dtShift As DataTable = ds.Tables(3)

            Dim dtSeq As DataTable = ds.Tables(4)
            Dim SeqCount As Integer = dtSeq.Rows.Count
            If SeqCount < MinCount Then
                Session("XBar") = ""
                Session("RBar") = ""
                show_error(MsgTypeEnum.Warning, "There is only " & SeqCount & " sequence between the selected date. Minimum is " & MinCount, 1)
                Return
            ElseIf SeqCount > MaxCount Then
                'show_error(MsgTypeEnum.Warning, "Only showing " & MaxCount & " from " & SeqCount & " sequence", 1)
            End If

            Dim dtCount As DataTable = ds.Tables(5)

            Dim iDay As Integer
            Dim Seq As Integer
            Dim TotalSeq As Integer
            Dim SelDay As Date = pDateFrom
            XRList = New List(Of clsXRView)
            Do Until SelDay >= pDateTo Or Seq >= MaxCount
                iDay = iDay + 1
                If dt0.Rows.Count = 0 Then
                    Exit Do
                End If
                If dt0.Rows(0)(iDay + 1) & "" = "" Then
                    Exit Do
                End If

                Dim BandDay As New GridViewBandColumn
                Dim DayCaption As Date = dt0.Rows(0)(iDay + 1)
                BandDay.Caption = Format(DayCaption, "dd MMM yyyy")
                .Columns.Add(BandDay)
                For iShift = 1 To 3
                    If Seq >= MaxCount Then
                        Seq = Seq + 1
                        Exit For
                    End If
                    Dim BandShift As New GridViewBandColumn
                    BandShift.Caption = iShift
                    BandDay.Columns.Add(BandShift)

                    Dim ColShift As New GridViewDataTextColumn
                    If Not IsDBNull(dtShift.Rows(0)(Seq)) Then
                        If dtCount.Rows(0)(Seq + 2) = 5 Then
                            TotalSeq = TotalSeq + 1
                            ColShift.Caption = TotalSeq
                            Dim xrv As New clsXRView
                            xrv.QCSDate = BandDay.Caption
                        Else
                            ColShift.Caption = " "
                            BandShift.Visible = False
                        End If
                    Else
                        ColShift.Caption = " "
                        BandShift.Visible = False
                    End If
                    If iShift = 3 Then
                        Dim PB As GridViewBandColumn = BandShift.ParentBand
                        Dim HasChildren As Boolean = False
                        For Each child As GridViewBandColumn In PB.Columns
                            If child.Visible Then
                                HasChildren = True
                                Exit For
                            End If
                        Next
                        PB.Visible = HasChildren
                    End If
                    Seq = Seq + 1
                    ColShift.Width = 60
                    ColShift.FieldName = Seq
                    ColShift.Name = Format(DayCaption, "yyyy-MM-dd")
                    BandShift.Columns.Add(ColShift)
                Next
                SelDay = SelDay.AddDays(1)
            Loop
            Dim dt As DataTable = ds.Tables(1)
            grid.DataSource = dt
            grid.DataBind()

            Dim i As Integer = -1
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("Seq") = 102 Then
                    Exit For
                End If
            Next
            Dim XBar As String = ""
            For iCol = 1 To Seq
                Dim s As String
                If IsDBNull(dt.Rows(i)(iCol + 1)) Then
                    s = ""
                Else
                    s = dt.Rows(i)(iCol + 1)
                    XBar = XBar & s & "/"
                End If
            Next
            grid.JSProperties("cp_XBar") = XBar
            Session("XBar") = XBar

            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("Seq") = 103 Then
                    Exit For
                End If
            Next
            Dim RBar As String = ""
            For iCol = 1 To Seq
                Dim s As String
                If IsDBNull(dt.Rows(i)(iCol + 1)) Then
                    s = ""
                Else
                    s = dt.Rows(i)(iCol + 1)
                    RBar = RBar & s & "/"
                End If
            Next
            grid.JSProperties("cp_RBar") = RBar
            Session("RBar") = RBar

            '=====================================================================================================

            Dim XREnd As Date = DateSerial(pDateFrom.Year, pDateFrom.Month, 1).AddDays(-1)
            Dim XRStart As Date = XREnd
            Dim nDate As Integer = 25
            Do While nDate > 0
                If XRStart.DayOfWeek <> DayOfWeek.Saturday And XRStart.DayOfWeek <> DayOfWeek.Sunday Then
                    nDate = nDate - 1
                End If
                XRStart = XRStart.AddDays(-1)
            Loop
            Dim Total As Single
            Dim TotalX As Single
            Dim TotalR As Single

            Dim Period As Date = DateAdd(DateInterval.Month, -1, pDateFrom)
            Period = DateSerial(Period.Year, Period.Month, 1)
            Dim xr As clsQCSXRHistory = clsQCSXRHistoryDB.GetData(Period, pLineNo, pSubLine, pProcess, pPartNo, pXRCode)
            If xr IsNot Nothing Then
                grid.JSProperties("cp_XUCLAdj") = Format(xr.XUCLAdjusted, "0.00")
                grid.JSProperties("cp_XLCLAdj") = Format(xr.XLCLAdjusted, "0.00")
                grid.JSProperties("cp_RUCLAdj") = Format(xr.RUCLAdjusted, "0.00")
            Else
                grid.JSProperties("cp_XUCLAdj") = ""
                grid.JSProperties("cp_XLCLAdj") = ""
                grid.JSProperties("cp_RUCLAdj") = ""
            End If

            ds = clsXRChartDB.GetDataset(XRStart, XREnd, pPartNo, pLineNo, pSubLine, pItem)
            dt = ds.Tables(1)
            If dt.Rows.Count = 0 Then
                grid.JSProperties("cp_Total") = Nothing
                grid.JSProperties("cp_TotalX") = Nothing
                grid.JSProperties("cp_TotalR") = Nothing
                grid.JSProperties("cp_TotalSeq") = Nothing

                grid.JSProperties("cp_ResultX") = Nothing
                grid.JSProperties("cp_ResultR") = Nothing

                grid.JSProperties("cp_XUCL") = Nothing
                grid.JSProperties("cp_XLCL") = Nothing
                grid.JSProperties("cp_RUCL") = Nothing

                grid.JSProperties("cp_A2Value") = Nothing
                grid.JSProperties("cp_D4Value") = Nothing
                Return
            End If

            Seq = 0
            dtShift = ds.Tables(3)
            If dtShift.Rows.Count > 0 Then
                For i = 0 To dtShift.Columns.Count - 1
                    If Not IsDBNull(dtShift.Rows(0)(i)) Then
                        Seq = Seq + 1
                    End If
                Next
            End If
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("Seq") = 101 Then
                    Exit For
                End If
            Next
            If i > 1 Then
                Total = dt.Rows(i)("Total")
                TotalX = dt.Rows(i + 1)("Total")
                TotalR = dt.Rows(i + 2)("Total")
            End If
            grid.JSProperties("cp_Total") = Total
            grid.JSProperties("cp_TotalX") = TotalX
            grid.JSProperties("cp_TotalR") = TotalR
            grid.JSProperties("cp_TotalSeq") = TotalSeq

            Dim ResultX As Single
            Dim ResultR As Single
            If TotalSeq = 0 Then
                grid.JSProperties("cp_ResultX") = ""
                grid.JSProperties("cp_ResultR") = ""
            Else
                ResultX = TotalX / Seq
                ResultR = TotalR / Seq
                grid.JSProperties("cp_ResultX") = Math.Round(ResultX, 2)
                grid.JSProperties("cp_ResultR") = Math.Round(ResultR, 2)
            End If

            Dim dt1 As DataTable = ClsQCSXRDataDB.GetList.Tables(0)
            Dim n As Integer = dt1.Rows.Count - 1
            Dim A2Value As Single = dt1.Rows(n)("A2Value")
            Dim D4Value As Single = dt1.Rows(n)("D4Value")

            Dim XUCL As Single
            XUCL = ResultX + (A2Value * ResultR)
            grid.JSProperties("cp_XUCL") = Math.Round(XUCL, 2)
            grid.JSProperties("cp_A2Value") = Format(A2Value, "0.0000")

            Dim XLCL As Single
            XLCL = ResultX - (A2Value * ResultR)
            grid.JSProperties("cp_XLCL") = Math.Round(XLCL, 2)

            Dim RUCL As Single
            RUCL = D4Value * ResultR
            grid.JSProperties("cp_D4Value") = Format(D4Value, "0.0000")
            grid.JSProperties("cp_RUCL") = Math.Round(RUCL, 2)
        End With
    End Sub

    Private Sub LoadChartX(UCL As Single, LCL As Single, XBar As String)
        Dim xr As List(Of clsXRChart) = clsXRChartDB.GetChartX(UCL, LCL, XBar)
        With chartX
            CType(.Diagram, XYDiagram).SecondaryAxesY.Clear()
            Dim myAxisY As New SecondaryAxisY("my Y-Axis")
            CType(.Diagram, XYDiagram).SecondaryAxesY.Add(myAxisY)

            CType(.Series("Warning").View, XYDiagramSeriesViewBase).AxisY = myAxisY

            .DataSource = xr
            .SeriesDataMember = "Description"
            .SeriesTemplate.ArgumentDataMember = "Seq"
            .SeriesTemplate.ValueDataMembers.AddRange(New String() {"Value"})
            '.AutoBindingSettingsEnabled = False
            '.SeriesTemplate.ArgumentScaleType = ScaleType.Qualitative
            '.SeriesTemplate.ValueScaleType = ScaleType.Numerical
            '.SeriesSorting = SortingMode.None
            '.SeriesTemplate.SeriesPointsSorting = SortingMode.None
            '.SeriesTemplate.SeriesPointsSortingKey = SeriesPointKey.Argument
            .DataBind()

            '.DataSource = xr
            '.SeriesDataMember = "Description"
            '.SeriesTemplate.ArgumentDataMember = "Seq"
            '.SeriesTemplate.ValueDataMembers.AddRange(New String() {"Qty"})
            '.DataBind()
        End With
    End Sub

    Private Sub LoadChartR(UCL As Single, RBar As String)
        Dim xr As List(Of clsXRChart) = clsXRChartDB.GetChartR(UCL, RBar)
        With chartR
            CType(.Diagram, XYDiagram).SecondaryAxesY.Clear()
            Dim myAxisY As New SecondaryAxisY("my Y-Axis")
            CType(.Diagram, XYDiagram).SecondaryAxesY.Add(myAxisY)
            CType(.Series("Warning").View, XYDiagramSeriesViewBase).AxisY = myAxisY

            .DataSource = xr
            .SeriesDataMember = "Description"
            .SeriesTemplate.ArgumentDataMember = "Seq"
            .SeriesTemplate.ValueDataMembers.AddRange("Value")
            '.SeriesTemplate.ArgumentScaleType = ScaleType.Qualitative
            '.SeriesTemplate.ValueScaleType = ScaleType.Numerical
            .DataBind()
        End With
    End Sub

    Private Sub FillCboLine()
        Dim pUser As String = Session("User") & ""
        Dim dsline As DataTable
        dsline = ClsQCSMasterDB.GetDataLine(1, pUser, "")
        cboLineID.DataSource = dsline
        cboLineID.DataBind()
    End Sub

    Private Sub cboSubLine_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cboSubLine.Callback
        Dim pLineID As String = Split(e.Parameter, "|")(1)
        Dim dt As DataTable
        dt = ClsSubLineDB.GetDataSubLine(pLineID, "")
        cboSubLine.DataSource = dt
        cboSubLine.DataBind()
    End Sub

    Protected Sub grid_CellEditorInitialize(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs)

    End Sub

    Private Sub grid_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles grid.CustomCallback
        Dim pDateFrom As String = Split(e.Parameters, "|")(1)
        Dim pDateTo As String = Split(e.Parameters, "|")(2)
        Dim pLineID As String = Split(e.Parameters, "|")(3) & ""
        Dim pSubLineID As String = Split(e.Parameters, "|")(4) & ""
        Dim pMachineID As String = Split(e.Parameters, "|")(5) & ""
        Dim pPartID As String = Split(e.Parameters, "|")(6) & ""
        Dim pItem As String = Split(e.Parameters, "|")(7) & ""
        Dim pProcess As String = Split(e.Parameters, "|")(8) & ""
        Dim pXRCode As String = Split(e.Parameters, "|")(9) & ""
        GridLoad(pDateFrom, pDateTo, pLineID, pSubLineID, pMachineID, pPartID, pItem, pProcess, pXRCode)
    End Sub

    Protected Sub grid_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
        e.Cancel = True
    End Sub

    Protected Sub grid_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        e.Cancel = True
    End Sub

    Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
        e.Cancel = True
    End Sub

    Protected Sub grid_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs)
        If (Not grid.IsNewRowEditing) Then
            grid.DoRowValidation()
        End If
        show_error(MsgTypeEnum.Info, "", 0)
    End Sub

    Protected Sub gridAction_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs)
        If (Not gridAction.IsNewRowEditing) Then
            gridAction.DoRowValidation()
        End If
        show_error(MsgTypeEnum.Info, "", 0)
    End Sub

    Protected Sub grid_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs)

    End Sub

    Protected Sub grid_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles grid.AfterPerformCallback
        Dim commandColumn = TryCast(grid.Columns(0), GridViewCommandColumn)
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then

        End If
    End Sub

    Private Sub cboPartID_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cboPartID.Callback
        Dim pLineID As String = Split(e.Parameter, "|")(2)
        Dim dt As DataTable
        dt = clsQCSResultDB.GetPart(pLineID)
        cboPartID.DataSource = dt
        cboPartID.DataBind()
    End Sub

    Private Sub grid_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs) Handles grid.HtmlDataCellPrepared
        If e.KeyValue = 101 Then
            e.Cell.BackColor = System.Drawing.Color.Silver
        ElseIf e.KeyValue = 200 Then            
            If e.DataColumn.FieldName <> "Des" Then
                e.Cell.ForeColor = Color.Blue
                e.Cell.Text = "View"
            Else
                e.Cell.Text = "Inquiry"
            End If
        End If
        If e.DataColumn.FieldName = "Des" Then
            If e.KeyValue >= 102 Then
                e.Cell.BackColor = System.Drawing.Color.Silver
            End If
        End If
    End Sub

    Private Sub chartX_BoundDataChanged(sender As Object, e As System.EventArgs) Handles chartX.BoundDataChanged
        With chartX
            If .Series("XBar") Is Nothing Then
                Return
            End If
            'Dim seriesRatio As Series = New Series("Side-by-Side Bar Series 1", ViewType.Bar)
            DirectCast(.Series("Warning").View, PointSeriesView).Color = Color.OrangeRed
            DirectCast(.Series("Warning").View, PointSeriesView).PointMarkerOptions.FillStyle.FillMode = FillMode.Solid

            .Series("XBar").View.Color = Color.Blue
            .Series("UCL").View.Color = Color.Orange
            If .Series("LCL") IsNot Nothing Then
                .Series("LCL").View.Color = Color.Green
            End If
            .Series("Warning").View.Color = Color.Red
        End With
    End Sub

    Private Sub chartX_CustomCallback(sender As Object, e As DevExpress.XtraCharts.Web.CustomCallbackEventArgs) Handles chartX.CustomCallback
        Dim pUCL As String = Split(e.Parameter, "|")(0)
        Dim pLCL As String = Split(e.Parameter, "|")(1)
        Dim pXBar As String = Split(e.Parameter, "|")(2)
        LoadChartX(Val(pUCL), Val(pLCL), pXBar)
    End Sub

    Private Sub chartR_BoundDataChanged(sender As Object, e As System.EventArgs) Handles chartR.BoundDataChanged
        With chartR
            If .Series("RBar") Is Nothing Then
                Return
            End If
            DirectCast(.Series("Warning").View, PointSeriesView).Color = Color.OrangeRed
            DirectCast(.Series("Warning").View, PointSeriesView).PointMarkerOptions.FillStyle.FillMode = FillMode.Solid

            .Series("RBar").View.Color = Color.Blue
            .Series("UCL").View.Color = Color.Orange
            .Series("Warning").View.Color = Color.Red
        End With
    End Sub

    Private Sub chartR_CustomCallback(sender As Object, e As DevExpress.XtraCharts.Web.CustomCallbackEventArgs) Handles chartR.CustomCallback
        Dim pUCL As String = Split(e.Parameter, "|")(0)
        Dim pRBar As String = Split(e.Parameter, "|")(1)
        LoadChartR(Val(pUCL), pRBar)
    End Sub

    Private Sub cboMachine_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cboMachine.Callback
        Dim pLineID As String = Split(e.Parameter, "|")(1)
        Dim pSubLineID As String = Split(e.Parameter, "|")(2)
        Dim dt As DataTable
        dt = clsQCSResultDB.GetMachine(pLineID, pSubLineID)
        cboMachine.DataSource = dt
        cboMachine.DataBind()
    End Sub

    Private Sub cboItem_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cboItem.Callback
        Dim LineID As String = Split(e.Parameter, "|")(1)
        Dim PartID As String = Split(e.Parameter, "|")(2)
        Dim MachineNo As String = Split(e.Parameter, "|")(3)
        Dim dt As DataTable = clsXRChartDB.GetXRCode(PartID, LineID, MachineNo)
        cboItem.DataSource = dt
        cboItem.DataBind()
    End Sub

    Private Sub gridAction_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles gridAction.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            GridLoadAction(dtFrom.Value, dtTo.Value, cboLineID.Value, cboSubLine.Value, txtProcessID.Text, cboPartID.Value, txtXRCode.Text)
        End If
    End Sub

    Private Sub gridAction_CellEditorInitialize(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs) Handles gridAction.CellEditorInitialize
        If Not gridAction.IsNewRowEditing Then
            If e.Column.FieldName = "Date" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Silver
            End If
        End If
    End Sub

    Private Sub gridAction_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridAction.BeforeGetCallbackResult
        If gridAction.IsNewRowEditing Then
            gridAction.SettingsCommandButton.UpdateButton.Text = "Save"
        End If
    End Sub

    Private Sub gridAction_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles gridAction.CustomCallback
        Dim pDateFrom As String = Split(e.Parameters, "|")(0)
        Dim pDateTo As String = Split(e.Parameters, "|")(1)
        Dim pLineID As String = Split(e.Parameters, "|")(2)
        Dim pSubLineID As String = Split(e.Parameters, "|")(3)
        Dim pProcess As String = Split(e.Parameters, "|")(4)
        Dim pPartID As String = Split(e.Parameters, "|")(5)
        Dim pXRCode As String = Split(e.Parameters, "|")(6)
        GridLoadAction(pDateFrom, pDateTo, pLineID, pSubLineID, pProcess, pPartID, pXRCode)
    End Sub

    Private Sub gridSummary_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles gridSummary.CustomCallback
        Dim pDateFrom As String = Split(e.Parameters, "|")(0)
        Dim pDateTo As String = Split(e.Parameters, "|")(1)
        Dim pLineID As String = Split(e.Parameters, "|")(2)
        Dim pSubLineID As String = Split(e.Parameters, "|")(3)
        Dim pPartID As String = Split(e.Parameters, "|")(4)
        Dim pMachineNo As String = Split(e.Parameters, "|")(5)
        Dim pItemID As String = Split(e.Parameters, "|")(6)
        Dim pUCL As String = Split(e.Parameters, "|")(7)
        Dim pLCL As String = Split(e.Parameters, "|")(8)
        GridLoadSummary(pDateFrom, pDateTo, pLineID, pSubLineID, pPartID, pMachineNo, pItemID, pUCL, pLCL)
    End Sub

    Protected Sub gridAction_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
        e.Cancel = True
        Dim xr As New clsQCSXRAction
        'xr.ActionDate = Now.Date
        xr.ActionDate = e.NewValues("Date")
        xr.LineID = cboLineID.Value
        xr.SubLineID = cboSubLine.Value
        xr.ProcessID = txtProcessID.Text
        xr.PartID = cboPartID.Value
        xr.PIC = e.NewValues("PIC") & ""
        xr.XRCode = txtXRCode.Text
        xr.Action = e.NewValues("Action") & ""
        xr.Result = e.NewValues("Result") & ""
        xr.User = Session("user") & ""
        clsQCSXRActionDB.Insert(xr)

        gridAction.CancelEdit()
        GridLoadAction(dtFrom.Value, dtTo.Value, cboLineID.Value, cboSubLine.Value, cboMachine.Value, cboPartID.Value, cboItem.Value)
        ShowMsgAction(MsgTypeEnum.Success, "Save data successfully!", 1)
    End Sub

    Protected Sub gridAction_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
        Dim GridData As DevExpress.Web.ASPxGridView.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView.ASPxGridView)
        For Each column As GridViewColumn In gridAction.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn IsNot Nothing Then
                If dataColumn.FieldName = "Date" Then
                    If IsNothing(e.NewValues("Date")) OrElse e.NewValues("Date").ToString.Trim = "" Then
                        e.Errors(dataColumn) = "Please input Date!"
                    ElseIf clsQCSXRActionDB.GetExistData(e.NewValues("Date"), cboLineID.Value, cboSubLine.Value, txtProcessID.Value, cboPartID.Value, txtXRCode.Value) = True Then
                        e.Errors(dataColumn) = "Date is already exist!"
                        gridAction.JSProperties("cp_dataexist") = "1"
                    End If
                End If
                If dataColumn.FieldName = "PIC" Then
                    If IsNothing(e.NewValues("PIC")) OrElse e.NewValues("PIC").ToString.Trim = "" Then
                        e.Errors(dataColumn) = "Please input PIC!"
                    End If
                End If
                If dataColumn.FieldName = "Action" Then
                    If IsNothing(e.NewValues("Action")) OrElse e.NewValues("Action").ToString.Trim = "" Then
                        e.Errors(dataColumn) = "Please input Action!"
                    End If
                End If
                If dataColumn.FieldName = "Result" Then
                    If IsNothing(e.NewValues("Result")) OrElse e.NewValues("Result").ToString.Trim = "" Then
                        e.Errors(dataColumn) = "Please input Result!"
                    End If
                End If
            End If
        Next column

        For Each column As GridViewColumn In grid.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn IsNot Nothing Then
                Dim f As String = dataColumn.FieldName
                If f = "Date" Or f = "PIC" Or f = "Action" Or f = "Result" Then
                    If IsNothing(e.NewValues(f)) OrElse e.NewValues(f).ToString.Trim = "" Then
                        ShowMsgAction(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                        Exit Sub
                    End If
                End If
            End If


        Next column
    End Sub

    Protected Sub gridAction_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        e.Cancel = True
        Dim xr As New clsQCSXRAction
        'xr.ActionDate = Now.Date
        xr.ActionDate = e.OldValues("Date")
        xr.LineID = cboLineID.Value
        xr.SubLineID = cboSubLine.Value
        xr.ProcessID = txtProcessID.Text
        xr.PartID = cboPartID.Value
        xr.PIC = e.NewValues("PIC") & ""
        xr.XRCode = txtXRCode.Text
        xr.Action = e.NewValues("Action") & ""
        xr.Result = e.NewValues("Result") & ""
        xr.User = Session("user") & ""
        clsQCSXRActionDB.Insert(xr)

        gridAction.CancelEdit()
        GridLoadAction(dtFrom.Value, dtTo.Value, cboLineID.Value, cboSubLine.Value, cboMachine.Value, cboPartID.Value, cboItem.Value)
        ShowMsgAction(MsgTypeEnum.Success, "Update data successful!", 1)
    End Sub

    Protected Sub gridAction_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
        e.Cancel = True
        Dim xr As New clsQCSXRAction
        xr.ActionDate = e.Values("Date")
        xr.LineID = e.Values("LineID")
        xr.SubLineID = e.Values("SubLineID")
        xr.ProcessID = e.Values("ProcessID")
        xr.PartID = e.Values("PartID")
        xr.XRCode = e.Values("XRCode")
        clsQCSXRActionDB.Delete(xr)

        gridAction.CancelEdit()
        GridLoadAction(dtFrom.Value, dtTo.Value, cboLineID.Value, cboSubLine.Value, cboMachine.Value, cboPartID.Value, cboItem.Value)
        ShowMsgAction(MsgTypeEnum.Success, "Delete data successful!", 1)
    End Sub

    Private Function ANull(value As String) As Object
        If value = "" Then
            Return Nothing
        Else
            Return Adbl(value)
        End If
    End Function
    Private Function Adbl(value As Object) As Double
        If IsDBNull(value) Then
            Return 0
        ElseIf IsNumeric(value) Then
            Return CDbl(value)
        Else
            Return 0
        End If
    End Function

    Private Sub cbkUpdate_Callback(source As Object, e As DevExpress.Web.ASPxCallback.CallbackEventArgs) Handles cbkUpdate.Callback
        Dim pXUCLAdj As String = Split(e.Parameter, "|")(0)
        Dim pXLCLAdj As String = Split(e.Parameter, "|")(1)
        Dim pRUCLAdj As String = Split(e.Parameter, "|")(2)
        Dim pPartID As String = Split(e.Parameter, "|")(3) & ""
        Dim pXRCode As String = Split(e.Parameter, "|")(4) & ""
        Dim pDate As String = Split(e.Parameter, "|")(5)
        Dim pXUCL As String = Split(e.Parameter, "|")(6)
        Dim pXLCL As String = Split(e.Parameter, "|")(7)
        Dim pRUCL As String = Split(e.Parameter, "|")(8)

        Dim xr As New clsQCSXRHistory
        xr.XUCLAdjusted = ANull(pXUCLAdj)
        xr.XLCLAdjusted = ANull(pXLCLAdj)
        xr.RUCLAdjusted = ANull(pRUCLAdj)
        xr.XUCL = Adbl(pXUCL)
        xr.XLCL = Adbl(pXLCL)
        xr.RUCL = Adbl(pRUCL)
        xr.Period = DateAdd(DateInterval.Month, -1, CDate(pDate))
        xr.Period = DateSerial(xr.Period.Year, xr.Period.Month, 1)
        xr.PartID = pPartID
        xr.XRCode = pXRCode
        xr.User = Session("user") & ""
        clsQCSXRHistoryDB.UpdateAdjustment(xr)

        cbkUpdate.JSProperties("cp_message") = "Update adjustment successful!"
        cbkUpdate.JSProperties("cp_type") = MsgTypeEnum.Success
    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        DownloadExcel()
    End Sub

    Private Sub DownloadExcel()
        Dim CatererID As String = Session("CatererID") & ""
        Dim XBar As String = Session("XBar") & ""
        Dim XUCL As Double = Val(txtXUCL.Text)
        Dim XLCL As Double = Val(txtXLCL.Text)
        Dim RUCL As Double = Val(txtRUCL.Text)
        If txtXUCLAdj.Text <> "" Then
            XUCL = Val(txtXUCLAdj.Text)
        End If
        If txtXLCLAdj.Text <> "" Then
            XLCL = Val(txtXLCLAdj.Text)
        End If
        If txtRUCLAdj.Text <> "" Then
            RUCL = Val(txtRUCLAdj.Text)
        End If

        Dim ds As DataSet = clsXRChartDB.GetDataset(dtFrom.Value, dtTo.Value, cboPartID.Value, cboLineID.Value, cboSubLine.Value, cboItem.Value)
        Dim dt0 As DataTable = ds.Tables(0)
        If dt0.Rows.Count = 0 Then
            Return
        End If
        Dim dtShift As DataTable = ds.Tables(3)
        Dim ps As New PrintingSystem()

        LoadChartX(XUCL, XLCL, XBar)
        Dim linkX As New PrintableComponentLink(ps)
        linkX.Component = (CType(chartX, IChartContainer)).Chart

        Dim RBar As String = Session("RBar") & ""
        LoadChartR(RUCL, RBar)
        Dim linkR As New PrintableComponentLink(ps)
        linkR.Component = (CType(chartR, IChartContainer)).Chart

        Dim compositeLink As New CompositeLink(ps)
        compositeLink.Links.AddRange(New Object() {linkX, linkR})
        compositeLink.CreateDocument()
        Dim Path As String = Server.MapPath("Download")
        Dim streamImg As New MemoryStream
        compositeLink.ExportToImage(streamImg)

        Using Pck As New ExcelPackage
            Dim ws As ExcelWorksheet = Pck.Workbook.Worksheets.Add("Sheet1")
            With ws
                Dim iDay As Integer = 2
                Dim iCol As Integer = 2
                Dim lastCol As Integer = 1
                Dim Seq As Integer = 0
                Dim SelDay As Date = dtFrom.Value
                .Cells(1, 1).Value = "Date"
                .Cells(2, 1).Value = "Shift"
                .Cells(3, 1).Value = "Sequence"
                Do Until iCol >= MaxCount
                    If dt0.Rows(0)(iDay) & "" = "" Then
                        Exit Do
                    End If
                    .Cells(1, iCol, 1, iCol + 2).Style.Numberformat.Format = "dd/MM/yy"
                    .Cells(1, iCol, 1, iCol + 2).Value = dt0.Rows(0)(iDay)
                    .Cells(1, iCol, 1, iCol + 2).Merge = True
                    .Cells(2, iCol).Value = 1
                    .Cells(2, iCol + 1).Value = 2
                    .Cells(2, iCol + 2).Value = 3
                    If Not IsDBNull(dtShift.Rows(0)(iCol - 2)) Then
                        Seq = Seq + 1
                        .Cells(3, iCol).Value = Seq
                        lastCol = iCol
                    End If
                    If Not IsDBNull(dtShift.Rows(0)(iCol - 1)) Then
                        Seq = Seq + 1
                        .Cells(3, iCol + 1).Value = Seq
                        lastCol = iCol + 1
                    End If
                    If Not IsDBNull(dtShift.Rows(0)(iCol)) Then
                        Seq = Seq + 1
                        .Cells(3, iCol + 2).Value = Seq
                        lastCol = iCol + 2
                    End If
                    iCol = iCol + 3
                    iDay = iDay + 1
                Loop
                iCol = iCol + 1
                .Cells(1, 1, 11, iCol).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                .Cells(1, 1, 11, iCol).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                .Cells(1, 1, 11, iCol).Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                .Cells(1, 1, 11, iCol).Style.Border.Left.Style = Style.ExcelBorderStyle.Thin

                Dim nCol As Integer = iCol

                Dim dt1 As DataTable = ds.Tables(1)
                For i = 0 To dt1.Rows.Count - 1
                    For iCol = 1 To MaxCount
                        .Cells(i + 4, iCol).Value = dt1.Rows(i)(iCol)
                    Next
                Next

                Dim n As Integer = dt1.Rows.Count + 3
                Dim dt5 As DataTable = ds.Tables(5)
                For iCol = 1 To MaxCount
                    .Cells(n, iCol).Value = dt5.Rows(0)(iCol)
                Next
                For iCol = MaxCount + 3 To 2 Step -1
                    If .Cells(n, iCol).Value <> "5" Then
                        .DeleteColumn(iCol)
                    End If
                Next
                For iCol = 1 To MaxCount + 2
                    If .Cells(n, iCol).Value & "" <> "" Then
                        lastCol = iCol
                    End If
                Next
                .Cells(1, 1, 3, lastCol).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                .Cells(1, 1, 3, lastCol).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                .Cells(1, 1, 3, lastCol).Style.Font.Color.SetColor(System.Drawing.Color.White)
                .Cells(1, 1, 3, lastCol).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                .Cells(1, 1, 3, lastCol).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                .Cells(1, 1, 3, lastCol).Style.WrapText = True

                .Cells(n, 1, n, MaxCount + 3).Clear()

                n = dt1.Rows.Count
                .Cells(n, 1, n, lastCol).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                .Cells(n, 1, n, lastCol).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)

                .Cells(n + 1, 1, n + 2, 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                .Cells(n + 1, 1, n + 2, 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)

                .Cells(13, 1).Value = "SUMMARY"
                .Cells(14, 1).Value = "Line"
                .Cells(14, 2).Value = "Part No."
                .Cells(14, 4).Value = "Part Name"
                .Cells(14, 6).Value = "Item Check"
                .Cells(14, 8).Value = "Standard"
                .Cells(14, 10).Value = "Frequency"
                .Cells(14, 12).Value = "Measuring Instrument"
                .Cells(14, 14).Value = "Machine No."
                .Cells(14, 15).Value = "CP"
                .Cells(14, 16).Value = "CPK"
                .Cells(14, 2, 14, 3).Merge = True
                .Cells(14, 4, 14, 5).Merge = True
                .Cells(14, 6, 14, 7).Merge = True
                .Cells(14, 8, 14, 9).Merge = True
                .Cells(14, 10, 14, 11).Merge = True
                .Cells(14, 12, 14, 13).Merge = True
                .Cells(14, 1, 15, 16).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                .Cells(14, 1, 15, 16).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                .Cells(14, 1, 15, 16).Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                .Cells(14, 1, 15, 16).Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                .Cells(14, 1, 14, 16).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                .Cells(14, 1, 14, 16).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                .Cells(14, 1, 14, 16).Style.Font.Color.SetColor(System.Drawing.Color.White)
                .Cells(14, 1, 14, 16).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                .Cells(14, 1, 14, 16).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                .Cells(14, 1, 14, 16).Style.WrapText = True
                Dim StartRow As Integer = 15
                Dim dtSum As DataTable = clsXRChartDB.GetSummary(dtFrom.Value, dtTo.Value, cboLineID.Value, cboSubLine.Value, cboPartID.Value, cboMachine.Value, cboItem.Value, XUCL, XLCL)
                For i = 0 To dtSum.Rows.Count - 1
                    Dim j As Integer = StartRow + i
                    .Cells(j, 1).Value = dtSum.Rows(i)("LineID")
                    .Cells(j, 2).Value = dtSum.Rows(i)("PartID")
                    .Cells(j, 4).Value = dtSum.Rows(i)("PartName")
                    .Cells(j, 6).Value = dtSum.Rows(i)("Item")
                    .Cells(j, 8).Value = dtSum.Rows(i)("Standard")
                    .Cells(j, 10).Value = dtSum.Rows(i)("Frequency")
                    .Cells(j, 12).Value = dtSum.Rows(i)("MeasuringInstrument")
                    .Cells(j, 14).Value = dtSum.Rows(i)("MachineNo")
                    .Cells(j, 15).Value = dtSum.Rows(i)("CP")
                    .Cells(j, 16).Value = dtSum.Rows(i)("CPK")

                    .Cells(j, 2, j, 3).Merge = True
                    .Cells(j, 4, j, 5).Merge = True
                    .Cells(j, 6, j, 7).Merge = True
                    .Cells(j, 8, j, 9).Merge = True
                    .Cells(j, 10, j, 11).Merge = True
                    .Cells(j, 12, j, 13).Merge = True
                Next

                .Cells(17, 1).Value = "COMMENT"
                .Cells(18, 1).Value = "Date"
                .Cells(18, 3).Value = "PIC"
                .Cells(18, 5).Value = "Action"
                .Cells(18, 7).Value = "Result"
                .Cells(18, 1, 18, 2).Merge = True
                .Cells(18, 3, 18, 4).Merge = True
                .Cells(18, 5, 18, 6).Merge = True
                .Cells(18, 7, 18, 8).Merge = True

                Dim dtAct As DataTable = clsQCSXRActionDB.GetTable(dtFrom.Value, dtTo.Value, cboLineID.Value, cboSubLine.Value, txtProcessID.Text, cboPartID.Value, txtXRCode.Text)
                .Cells(18, 1, 18 + dtAct.Rows.Count, 8).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                .Cells(18, 1, 18 + dtAct.Rows.Count, 8).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                .Cells(18, 1, 18 + dtAct.Rows.Count, 8).Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                .Cells(18, 1, 18 + dtAct.Rows.Count, 8).Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                .Cells(18, 1, 18, 8).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                .Cells(18, 1, 18, 8).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                .Cells(18, 1, 18, 8).Style.Font.Color.SetColor(System.Drawing.Color.White)
                .Cells(18, 1, 18, 8).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                .Cells(18, 1, 18, 8).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                StartRow = 19                
                .Cells(18, 1, 18 + dtAct.Rows.Count, 1).Style.Numberformat.Format = "dd MMM yyyy"
                .Cells(18, 1, 18 + dtAct.Rows.Count, 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                For i = 0 To dtAct.Rows.Count - 1
                    Dim j As Integer = StartRow + i
                    .Cells(j, 1).Value = dtAct.Rows(i)("Date")
                    .Cells(j, 3).Value = dtAct.Rows(i)("PIC") & ""
                    .Cells(j, 5).Value = dtAct.Rows(i)("Action") & ""
                    .Cells(j, 7).Value = dtAct.Rows(i)("Result") & ""
                    .Cells(j, 1, j, 2).Merge = True
                    .Cells(j, 3, j, 4).Merge = True
                    .Cells(j, 5, j, 6).Merge = True
                    .Cells(j, 7, j, 8).Merge = True
                Next
                .InsertRow(1, 7)
                .Cells(3, 1).Value = "Date"
                .Cells(4, 1).Value = "Machine No."
                .Cells(5, 1).Value = "Line No."
                .Cells(6, 1).Value = "Sub Line No."
                .Cells(3, 3).Value = ": " & Format(dtFrom.Value, "dd MMM yyyy") & " to " & Format(dtTo.Value, "dd MMM yyyy")
                .Cells(4, 3).Value = ": " & cboMachine.Text & " " & txtProcess.Text
                .Cells(5, 3).Value = ": " & cboLineID.Text
                .Cells(6, 3).Value = ": " & cboSubLine.Text

                .Cells(3, 8).Value = "Part No."
                .Cells(4, 8).Value = "Part Name"
                .Cells(5, 8).Value = "Item Check"
                .Cells(3, 10).Value = ": " & cboPartID.Text
                .Cells(4, 10).Value = ": " & txtPartName.Text
                .Cells(5, 10).Value = ": " & cboItem.Text

                .Cells(1, 1).Style.Font.Bold = True
                .Cells(1, 1).Style.Font.Size = 16
                .Cells(1, 1).Value = "SQC XR Chart"
                .Cells(1, 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                .Cells(1, 1, 1, 12).Merge = True
                Dim FinalRow As Integer = .Dimension.End.Row
                Dim ColumnString As String = "A2:M" & FinalRow
                .Cells(ColumnString).Style.Font.Name = "Segoe UI"
                .Cells(ColumnString).Style.Font.Size = 10

                .InsertRow(20, 22)
                Dim fi As New FileInfo(Path & "\chart.png")
                Dim Picture As OfficeOpenXml.Drawing.ExcelPicture
                Picture = .Drawings.AddPicture("chart", Image.FromStream(streamImg))
                Picture.SetPosition(20, 0, 0, 0)
            End With

            Dim stream As MemoryStream = New MemoryStream(Pck.GetAsByteArray())
            Response.AppendHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            Response.AppendHeader("Content-Disposition", "attachment; filename=SQCXRChart_" & Format(Date.Now, "yyyy-MM-dd") & ".xlsx")
            Response.BinaryWrite(stream.ToArray())
            Response.End()

        End Using
    End Sub

    Private Sub DownloadChart()
        Dim ps As New PrintingSystem()

        Dim XBar As String = Session("XBar") & ""
        LoadChartX(Val(txtXUCL.Text), Val(txtXLCL.Text), XBar)
        Dim linkX As New PrintableComponentLink(ps)
        linkX.Component = (CType(chartX, IChartContainer)).Chart

        Dim RBar As String = Session("RBar") & ""
        LoadChartR(Val(txtRUCL.Text), RBar)
        Dim linkR As New PrintableComponentLink(ps)
        linkR.Component = (CType(chartR, IChartContainer)).Chart

        Dim compositeLink As New CompositeLink(ps)
        compositeLink.Links.AddRange(New Object() {linkX, linkR})
        compositeLink.CreateDocument()
        Dim Path As String = Server.MapPath("Download")
        compositeLink.ExportToImage(Path & "\chart.png")
        Using stream As New MemoryStream()
            compositeLink.PrintingSystem.ExportToXlsx(stream)
            Response.Clear()
            Response.Buffer = False
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment; filename=QCS_XR_Chart.xlsx")
            Response.BinaryWrite(stream.ToArray())
            Response.End()
        End Using
        ps.Dispose()
    End Sub

    Private Sub grid_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles grid.HtmlRowPrepared
        e.Row.Visible = False
    End Sub
End Class