Imports System.Drawing
Imports DevExpress.Web
Imports DevExpress.XtraCharts
Imports DevExpress.XtraCharts.Web

Public Class SampleControlQuality
    Inherits System.Web.UI.Page
    Dim pUser As String = ""
    Public AuthApprove As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public ValueType As String
    Dim GlobalPrm As String = ""

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        gridX.JSProperties("cp_message") = ErrMsg
        gridX.JSProperties("cp_type") = msgType
        gridX.JSProperties("cp_val") = pVal
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GlobalPrm = Request.QueryString("FactoryCode") & ""
        sGlobal.getMenu("B060")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user") & ""
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B060")
        gridX.SettingsDataSecurity.AllowInsert = AuthUpdate
        gridX.SettingsDataSecurity.AllowEdit = AuthUpdate
        show_error(MsgTypeEnum.Info, "", 0)
        If Not IsPostBack And Not IsCallback Then
            up_FillCombo()
            If GlobalPrm <> "" Then
                dtDate.Value = CDate(Request.QueryString("ProdDate"))
                Dim FactoryCode As String = Request.QueryString("FactoryCode")
                Dim ItemTypeCode As String = Request.QueryString("ItemTypeCode")
                Dim Line As String = Request.QueryString("Line")
                Dim ItemCheckCode As String = Request.QueryString("ItemCheckCode")
                Dim ProdDate As String = Request.QueryString("ProdDate")
                Dim ProdDate2 As String = Request.QueryString("ProdDate2")
                Dim Shift As String = Request.QueryString("Shift")
                Dim Sequence As String = Request.QueryString("Sequence")

                InitCombo(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate, Shift, Sequence, ProdDate2)
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "GridLoad();", True)
            Else
                dtDate.Value = Now.Date
                pUser = Session("user") & ""
                dtDate.Value = Now.Date
                dtTo.Value = Now.Date
                If pUser <> "" Then
                    Dim User As clsUserSetup = clsUserSetupDB.GetData(pUser)
                    If User IsNot Nothing Then
                        cboFactory.Value = User.FactoryCode
                        cboType.DataSource = clsItemTypeDB.GetList(cboFactory.Value)
                        cboType.DataBind()
                    End If
                End If
                'InitCombo("F001", "TPMSBR011", "015", "IC021", "2022-08-03", "SH001", 1, "2022-09-01")
            End If
        End If
    End Sub

    Private Sub InitCombo(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String, ShiftCode As String, Sequence As String, ProdDate2 As String)
        dtDate.Value = CDate(ProdDate)
        dtTo.Value = CDate(ProdDate2)
        cboFactory.Value = FactoryCode

        cboType.DataSource = clsItemTypeDB.GetList(cboFactory.Value)
        cboType.DataBind()
        cboType.Value = ItemTypeCode

        cboLine.DataSource = ClsLineDB.GetList("admin", cboFactory.Value, cboType.Value)
        cboLine.DataBind()
        cboLine.Value = Line

        cboItemCheck.DataSource = clsItemCheckDB.GetList(cboFactory.Value, cboType.Value, cboLine.Value)
        cboItemCheck.DataBind()
        cboItemCheck.Value = ItemCheckCode
    End Sub

    Private Sub up_FillCombo()
        cboFactory.DataSource = clsFactoryDB.GetList
        cboFactory.DataBind()
    End Sub

    Dim dtXR As DataTable
    Dim dtLSL As DataTable
    Dim dtUSL As DataTable
    Dim dtLCL As DataTable
    Dim dtUCL As DataTable
    Dim dtChart As DataTable

    Private Sub GridXLoad(FactoryCode As String, ItemTypeCode As String, LineCode As String, ItemCheckCode As String, ProdDate As String, ProdDate2 As String, VerifiedOnly As Integer)
        With gridX
            .Columns.Clear()
            Dim Band1 As New GridViewBandColumn
            Band1.Caption = "DATE"
            Band1.HeaderStyle.Height = 90
            .Columns.Add(Band1)

            Dim Band2 As New GridViewBandColumn
            Band2.Caption = "SHIFT"
            Band1.HeaderStyle.Height = 40
            Band1.Columns.Add(Band2)

            Dim Col1 As New GridViewDataTextColumn
            Col1.FieldName = "Des"
            Col1.Caption = "TIME"
            Col1.Width = 90
            Col1.FixedStyle = GridViewColumnFixedStyle.Left
            Col1.CellStyle.HorizontalAlign = HorizontalAlign.Center
            Band2.Columns.Add(Col1)

            Dim ds As DataSet = clsSPCResultDetailDB.GetSampleByPeriod(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ProdDate, ProdDate2, VerifiedOnly)
            Dim dtDay As DataTable = ds.Tables(0)

            Dim PrevDay As String = ""
            Dim PrevShift As String = ""
            For iDay = 0 To dtDay.Rows.Count - 1
                Dim SelDay As Date = dtDay.Rows(iDay)("ProdDate")
                Dim dDay As String = Format(SelDay, "yyyy-MM-dd")

                Dim BandDay As GridViewBandColumn
                Dim BandShift As GridViewBandColumn

                If dDay <> PrevDay Then
                    BandDay = New GridViewBandColumn
                    BandDay.Caption = Format(SelDay, "dd MMM yyyy")
                    .Columns.Add(BandDay)

                End If

                Dim SelShift As String = dtDay.Rows(iDay)("ShiftCode")
                If SelShift <> PrevShift Or dDay <> PrevDay Then
                    BandShift = New GridViewBandColumn
                    BandShift.Caption = "S-" & SelShift
                    BandDay.Columns.Add(BandShift)
                End If

                Dim colTime As New GridViewDataTextColumn
                colTime.Caption = dtDay.Rows(iDay)("RegisterDate")
                colTime.FieldName = dtDay.Rows(iDay)("ColName")
                colTime.Width = 60
                colTime.CellStyle.HorizontalAlign = HorizontalAlign.Center
                BandShift.Columns.Add(colTime)

                PrevDay = dDay
                PrevShift = SelShift
            Next
            dtXR = ds.Tables(1)
            gridX.DataSource = dtXR
            gridX.DataBind()

            If ds.Tables.Count > 2 Then
                dtLSL = ds.Tables(2)
                dtUSL = ds.Tables(3)
                dtLCL = ds.Tables(4)
                dtUCL = ds.Tables(5)
                dtChart = ds.Tables(6)
            End If
        End With
    End Sub

    Private Sub gridX_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles gridX.CustomCallback
        Dim FactoryCode As String = Split(e.Parameters, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameters, "|")(1)
        Dim LineCode As String = Split(e.Parameters, "|")(2)
        Dim ItemCheckCode As String = Split(e.Parameters, "|")(3)
        Dim ProdDate As String = Split(e.Parameters, "|")(4)
        Dim ProdDate2 As String = Split(e.Parameters, "|")(5)
        Dim VerifiedOnly As Integer = Split(e.Parameters, "|")(6)
        GridXLoad(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ProdDate, ProdDate2, VerifiedOnly)
    End Sub

    Private Sub gridX_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles gridX.HtmlDataCellPrepared
        Dim LCL As Double
        Dim UCL As Double
        Dim LSL As Double
        Dim USL As Double

        Dim ColName As String = e.DataColumn.FieldName
        If Not IsDBNull(e.CellValue) AndAlso ColName <> "Seq" AndAlso ColName <> "Des" AndAlso (e.GetValue("Seq") = "1" Or e.GetValue("Seq") = "3" Or e.GetValue("Seq") = "4" Or e.GetValue("Seq") = "5") Then
            LSL = dtLSL.Rows(0)(ColName)
            USL = dtUSL.Rows(0)(ColName)
            LCL = dtLCL.Rows(0)(ColName)
            UCL = dtUCL.Rows(0)(ColName)
            Dim Value As Double = clsSPCResultDB.ADecimal(e.CellValue)
            If Value < LSL Or Value > USL Then
                e.Cell.BackColor = Color.Red
            ElseIf Value < LCL Or Value > UCL Then
                e.Cell.BackColor = Color.Pink
            End If
        End If
        If e.DataColumn.FieldName = "Des" Then
            If e.CellValue = "1" Then
                e.Cell.BackColor = Color.Red
            ElseIf e.CellValue = "2" Then
                e.Cell.BackColor = Color.Orange
            ElseIf e.CellValue = "3" Then
                e.Cell.BackColor = Color.LightGreen
            ElseIf e.CellValue = "4" Then
                e.Cell.BackColor = Color.DarkGreen
            ElseIf e.CellValue = "5" Then
                e.Cell.BackColor = Color.LightBlue
            End If
        End If
        If e.KeyValue = "-" Or e.KeyValue = "--" Then
            e.Cell.Text = ""
        End If
    End Sub

    Private Sub gridX_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs) Handles gridX.HtmlRowPrepared
        If e.KeyValue = "-" Or e.KeyValue = "--" Then
            e.Row.BackColor = System.Drawing.Color.FromArgb(112, 112, 112)
        End If
    End Sub

    Private Sub chartX_CustomDrawSeries(sender As Object, e As CustomDrawSeriesEventArgs) Handles chartX.CustomDrawSeries
        Dim s As String = e.Series.Name
        If s = "#1" Then
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.Kind = MarkerKind.Circle
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.BorderColor = Color.Red
            CType(e.SeriesDrawOptions, PointDrawOptions).Color = Color.Red
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.FillStyle.FillMode = FillMode.Solid
            e.LegendDrawOptions.Color = Color.Red
        ElseIf s = "#2" Then
            'CType(e.SeriesDrawOptions, PointDrawOptions).Marker.Kind = MarkerKind.Diamond
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.BorderColor = Color.Orange
            CType(e.SeriesDrawOptions, PointDrawOptions).Color = Color.Orange
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.FillStyle.FillMode = FillMode.Solid
            e.LegendDrawOptions.Color = Color.Orange
        ElseIf s = "#3" Then
            'CType(e.SeriesDrawOptions, PointDrawOptions).Marker.Kind = MarkerKind.Triangle
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.BorderColor = Color.Green
            CType(e.SeriesDrawOptions, PointDrawOptions).Color = Color.LightGreen
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.FillStyle.FillMode = FillMode.Solid
            e.LegendDrawOptions.Color = Color.LightGreen
        ElseIf s = "#4" Then
            'CType(e.SeriesDrawOptions, PointDrawOptions).Marker.Kind = MarkerKind.Square
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.BorderColor = Color.DarkGreen
            CType(e.SeriesDrawOptions, PointDrawOptions).Color = Color.DarkGreen
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.FillStyle.FillMode = FillMode.Solid
            e.LegendDrawOptions.Color = Color.DarkGreen
        ElseIf s = "#5" Then
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.Kind = MarkerKind.Circle
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.BorderColor = Color.Blue
            CType(e.SeriesDrawOptions, PointDrawOptions).Color = Color.LightBlue
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.FillStyle.FillMode = FillMode.Solid
            e.LegendDrawOptions.Color = Color.LightBlue
        End If
    End Sub

    Private Sub LoadChartX(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String, ProdDate2 As String)
        Dim xr As List(Of clsXRChart) = clsXRChartDB.GetChartXRMonthly(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate, ProdDate2)
        With chartX
            .DataSource = xr
            Dim diagram As XYDiagram = CType(.Diagram, XYDiagram)
            diagram.AxisX.WholeRange.MinValue = 0
            diagram.AxisX.WholeRange.MaxValue = 12

            diagram.AxisX.GridLines.LineStyle.DashStyle = DashStyle.Solid
            diagram.AxisX.GridLines.MinorVisible = True
            diagram.AxisX.MinorCount = 1
            diagram.AxisX.GridLines.Visible = False

            diagram.AxisY.NumericScaleOptions.CustomGridAlignment = 0.005
            diagram.AxisY.GridLines.MinorVisible = False

            Dim ChartType As String = clsXRChartDB.GetChartType(FactoryCode, ItemTypeCode, Line, ItemCheckCode)
            If ChartType = "1" Then
                .Titles(0).Text = "Chart X"
            Else
                .Titles(0).Text = "Graph Monitoring"
            End If

            Dim Setup As clsChartSetup = clsChartSetupDB.GetData(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
            diagram.AxisY.ConstantLines.Clear()
            If Setup IsNot Nothing Then
                Dim LCL As New ConstantLine("LCL")
                LCL.Color = Drawing.Color.Purple
                LCL.LineStyle.Thickness = 2
                LCL.LineStyle.DashStyle = DashStyle.DashDot
                diagram.AxisY.ConstantLines.Add(LCL)
                LCL.AxisValue = Setup.XBarLCL

                Dim UCL As New ConstantLine("UCL")
                UCL.Color = Drawing.Color.Purple
                UCL.LineStyle.Thickness = 2
                UCL.LineStyle.DashStyle = DashStyle.DashDot
                diagram.AxisY.ConstantLines.Add(UCL)
                UCL.AxisValue = Setup.XBarUCL

                Dim CL As New ConstantLine("CL")
                CL.Color = Drawing.Color.Black
                CL.LineStyle.Thickness = 2
                CL.LineStyle.DashStyle = DashStyle.Solid
                diagram.AxisY.ConstantLines.Add(CL)
                CL.AxisValue = Setup.XBarCL

                Dim LSL As New ConstantLine("LSL")
                LSL.Color = Drawing.Color.Red
                LSL.LineStyle.Thickness = 2
                LSL.LineStyle.DashStyle = DashStyle.Solid
                diagram.AxisY.ConstantLines.Add(LSL)
                LSL.AxisValue = Setup.SpecLSL

                Dim USL As New ConstantLine("USL")
                USL.Color = Drawing.Color.Red
                USL.LineStyle.Thickness = 2
                USL.LineStyle.DashStyle = DashStyle.Solid
                diagram.AxisY.ConstantLines.Add(USL)
                USL.AxisValue = Setup.SpecUSL

                diagram.AxisY.WholeRange.MinValue = Setup.SpecLSL
                diagram.AxisY.WholeRange.MaxValue = Setup.SpecUSL
                diagram.AxisY.WholeRange.EndSideMargin = Setup.SpecUSL + 1

                diagram.AxisY.VisualRange.MinValue = Setup.SpecLSL
                diagram.AxisY.VisualRange.MaxValue = Setup.SpecUSL

                CType(.Diagram, XYDiagram).SecondaryAxesY.Clear()
                Dim myAxisY As New SecondaryAxisY("my Y-Axis")
                myAxisY.Visibility = DevExpress.Utils.DefaultBoolean.False
                CType(.Diagram, XYDiagram).SecondaryAxesY.Add(myAxisY)
                CType(.Series("Rule").View, XYDiagramSeriesViewBase).AxisY = myAxisY
                CType(.Series("RuleYellow").View, XYDiagramSeriesViewBase).AxisY = myAxisY
            End If
            .DataBind()
            .Width = xr.Count * 12
        End With
    End Sub

    Private Sub chartX_CustomCallback(sender As Object, e As CustomCallbackEventArgs) Handles chartX.CustomCallback
        Dim Prm As String = e.Parameter
        If Prm = "" Then
            Prm = "F001|TPMSBR011|015|IC021|03 Aug 2022"
        End If
        Dim FactoryCode As String = Split(Prm, "|")(0)
        Dim ItemTypeCode As String = Split(Prm, "|")(1)
        Dim LineCode As String = Split(Prm, "|")(2)
        Dim ItemCheckCode As String = Split(Prm, "|")(3)
        Dim ProdDate As String = Split(Prm, "|")(4)
        Dim ProdDate2 As String = Split(Prm, "|")(5)
        LoadChartX(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ProdDate, ProdDate2)
    End Sub

    Private Sub chartX_BoundDataChanged(sender As Object, e As EventArgs) Handles chartX.BoundDataChanged
        'With chartX
        '    Dim view As FullStackedBarSeriesView = TryCast(.Series("Rule").View, FullStackedBarSeriesView)
        '    If view IsNot Nothing Then
        '        view.Color = Color.Red
        '        view.FillStyle.FillMode = FillMode.Solid
        '        view.Transparency = 200
        '        view.Border.Thickness = 1
        '    End If
        '    Dim view2 As FullStackedBarSeriesView = TryCast(.Series("RuleYellow").View, FullStackedBarSeriesView)
        '    If view2 IsNot Nothing Then
        '        view2.Color = Color.Yellow
        '        view2.FillStyle.FillMode = FillMode.Solid
        '        view2.Transparency = 200
        '        view2.Border.Thickness = 1
        '    End If
        'End With
    End Sub

    Private Sub cboType_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboType.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        cboType.DataSource = clsItemTypeDB.GetList(FactoryCode)
        cboType.DataBind()
    End Sub

    Private Sub cboLine_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboLine.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameter, "|")(1)
        Dim UserID As String = Session("user") & ""
        cboLine.DataSource = ClsLineDB.GetList(UserID, FactoryCode, ItemTypeCode)
        cboLine.DataBind()
    End Sub

    Private Sub cboItemCheck_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboItemCheck.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameter, "|")(1)
        Dim LineCode As String = Split(e.Parameter, "|")(2)
        cboItemCheck.DataSource = clsItemCheckDB.GetList(FactoryCode, ItemTypeCode, LineCode)
        cboItemCheck.DataBind()
    End Sub
End Class