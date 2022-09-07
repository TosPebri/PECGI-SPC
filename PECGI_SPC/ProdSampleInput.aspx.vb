Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DevExpress.XtraCharts
Imports DevExpress.XtraCharts.Web
Imports System.Drawing

Public Class ProdSampleInput
    Inherits System.Web.UI.Page
    Dim pUser As String = ""
    Public AuthApprove As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public ValueType As String
    Dim GlobalPrm As String = ""

    Private Sub GridXLoad(FactoryCode As String, ItemTypeCode As String, LineCode As String, ItemCheckCode As String, ProdDate As String)
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

            Dim SelDay As Object = clsSPCResultDB.GetPrevDate(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ProdDate)
            For iDay = 1 To 2
                If Not IsDBNull(SelDay) Then
                    Dim dDay As String = Format(CDate(SelDay), "yyyy-MM-dd")
                    Dim BandDay As New GridViewBandColumn
                    BandDay.Caption = Format(SelDay, "dd MMM yyyy")
                    .Columns.Add(BandDay)

                    Dim Shiftlist As List(Of clsShift) = clsFrequencyDB.GetShift(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, dDay)

                    For Each Shift In Shiftlist
                        Dim BandShift As New GridViewBandColumn
                        BandShift.Caption = "S-" & Shift.ShiftName
                        BandDay.Columns.Add(BandShift)

                        Dim SeqList As List(Of clsSequenceNo) = clsFrequencyDB.GetSequence(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, Shift.ShiftCode, dDay)
                        Dim ColIndex As Integer = 1
                        For Each Seq In SeqList
                            Dim colTime As New GridViewDataTextColumn
                            colTime.Caption = Seq.StartTime
                            colTime.FieldName = iDay.ToString + "_" + Shift.ShiftName.ToString + "_" + Seq.SequenceNo.ToString
                            colTime.Width = 90
                            colTime.CellStyle.HorizontalAlign = HorizontalAlign.Center

                            BandShift.Columns.Add(colTime)
                            ColIndex = ColIndex + 1
                        Next

                        Dim colLCL As New GridViewDataTextColumn
                        colLCL.Caption = "LCL"
                        colLCL.FieldName = "XBarLCL" & iDay.ToString
                        colLCL.Visible = False
                        BandShift.Columns.Add(colLCL)

                        Dim colUCL As New GridViewDataTextColumn
                        colUCL.Caption = "UCL"
                        colUCL.FieldName = "XBarUCL" & iDay.ToString
                        colUCL.Visible = False
                        BandShift.Columns.Add(colUCL)

                        Dim colLSL As New GridViewDataTextColumn
                        colLSL.Caption = "LSL"
                        colLSL.FieldName = "SpecLSL" & iDay.ToString
                        colLSL.Visible = False
                        BandShift.Columns.Add(colLSL)

                        Dim colUSL As New GridViewDataTextColumn
                        colUSL.Caption = "USL"
                        colUSL.FieldName = "SpecUSL" & iDay.ToString
                        colUSL.Visible = False
                        BandShift.Columns.Add(colUSL)

                    Next
                End If
                SelDay = CDate(ProdDate)
            Next
            Dim dt As DataTable = clsSPCResultDetailDB.GetTableXR(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ProdDate)
            gridX.DataSource = dt
            gridX.DataBind()
        End With
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        grid.JSProperties("cp_message") = ErrMsg
        grid.JSProperties("cp_type") = msgType
        grid.JSProperties("cp_val") = pVal
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GlobalPrm = Request.QueryString("FactoryCode") & ""
        sGlobal.getMenu("B020 ")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user") & ""
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B020 ")
        grid.SettingsDataSecurity.AllowInsert = AuthUpdate
        grid.SettingsDataSecurity.AllowEdit = AuthUpdate
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
                Dim Shift As String = Request.QueryString("Shift")
                Dim Sequence As String = Request.QueryString("Sequence")

                InitCombo(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate, Shift, Sequence)
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "GridLoad();", True)
                'GridLoad(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate, Shift, Sequence, 0)
            Else
                dtDate.Value = Now.Date
                InitCombo("F001", "TPMSBR011", "015", "IC021", "2022-08-03", "SH001", 1)
            End If
        End If
    End Sub

    Private Sub InitCombo(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String, ShiftCode As String, Sequence As String)
        dtDate.Value = CDate(ProdDate)
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

        cboShift.DataSource = clsFrequencyDB.GetShift(cboFactory.Value, cboType.Value, cboLine.Value, cboItemCheck.Value)
        cboShift.DataBind()
        cboShift.Value = ShiftCode

        cboSeq.DataSource = clsFrequencyDB.GetSequence(cboFactory.Value, cboType.Value, cboLine.Value, cboItemCheck.Value)
        cboSeq.DataBind()
        cboSeq.Value = Sequence
    End Sub

    Private Sub up_FillCombo()
        cboFactory.DataSource = clsFactoryDB.GetList
        cboFactory.DataBind()
    End Sub

    Protected Sub grid_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles grid.RowInserting
        e.Cancel = True
        Dim Result As New clsSPCResult
        Result.FactoryCode = cboFactory.Value
        Result.ItemCheckCode = cboItemCheck.Value
        Result.ItemTypeCode = cboType.Value
        Result.LineCode = cboLine.Value
        Result.ProdDate = dtDate.Value
        Result.ShiftCode = cboShift.Value
        Result.SequenceNo = cboSeq.Value
        Result.SubLotNo = 0
        Result.Remark = ""
        Result.RegisterUser = Session("user") & ""
        clsSPCResultDB.Insert(Result)

        Dim SeqNo As Integer = clsSPCResultDetailDB.GetSeqNo(Result.FactoryCode, Result.ItemTypeCode, Result.LineCode, Result.ItemCheckCode, Format(Result.ProdDate, "yyyy-MM-dd"), Result.ShiftCode, Result.SequenceNo)
        Dim Detail As New clsSPCResultDetail
        Detail.SPCResultID = Result.SPCResultID
        Detail.SequenceNo = SeqNo
        Detail.DeleteStatus = e.NewValues("DeleteStatus")
        Detail.Value = e.NewValues("Value")
        Detail.Remark = e.NewValues("Remark")
        Detail.RegisterUser = Result.RegisterUser
        clsSPCResultDetailDB.Insert(Detail)
        grid.CancelEdit()

        show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
    End Sub

    Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles grid.RowDeleting
        e.Cancel = True
    End Sub

    Private Sub up_ClearJS()
        grid.JSProperties("cpUSL") = " "
        grid.JSProperties("cpLSL") = " "
        grid.JSProperties("cpUCL") = " "
        grid.JSProperties("cpLCL") = " "
        grid.JSProperties("cpMin") = ""
        grid.JSProperties("cpMax") = ""
        grid.JSProperties("cpAve") = ""
        grid.JSProperties("cpR") = ""
        grid.JSProperties("cpC") = " "
        grid.JSProperties("cpNG") = " "
        grid.JSProperties("cpMKUser") = " "
        grid.JSProperties("cpMKDate") = " "
        grid.JSProperties("cpQCUser") = " "
        grid.JSProperties("cpQCDate") = " "
        grid.JSProperties("cpSubLotNo") = ""
        grid.JSProperties("cpRemarks") = ""
        grid.JSProperties("cpRefresh") = ""
    End Sub

    Private Sub up_ClearGrid()
        grid.DataSource = Nothing
        grid.DataBind()
        up_ClearJS()
    End Sub
    Protected Sub grid_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles grid.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            GridLoad(cboFactory.Value, cboType.Value, cboLine.Value, cboItemCheck.Value, Format(dtDate.Value, "yyyy-MM-dd"), cboShift.Value, cboSeq.Value, cboShow.Value)
            GridXLoad(cboFactory.Value, cboType.Value, cboLine.Value, cboItemCheck.Value, Format(dtDate.Value, "yyyy-MM-dd"))
        End If
    End Sub

    Private Sub GridLoad(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String, Shift As String, Sequence As Integer, VerifiedOnly As Integer)
        Dim ErrMsg As String = ""
        Dim dt As DataTable = clsSPCResultDetailDB.GetTable(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate, Shift, Sequence, VerifiedOnly)
        grid.DataSource = dt
        grid.DataBind()
        If dt.Rows.Count = 0 Then
            up_ClearJS()
            grid.JSProperties("cpRefresh") = "1"
            Dim Setup As clsChartSetup = clsChartSetupDB.GetData(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
            If Setup IsNot Nothing Then
                grid.JSProperties("cpUSL") = Setup.SpecUSL
                grid.JSProperties("cpLSL") = Setup.SpecLSL
                grid.JSProperties("cpUCL") = Setup.XBarUCL
                grid.JSProperties("cpLCL") = Setup.XBarLCL
            End If
        Else
            With dt.Rows(0)
                grid.JSProperties("cpUSL") = AFormat(.Item("SpecUSL"))
                grid.JSProperties("cpLSL") = AFormat(.Item("SpecLSL"))
                grid.JSProperties("cpUCL") = AFormat(.Item("XBarUCL"))
                grid.JSProperties("cpLCL") = AFormat(.Item("XBarLCL"))
                grid.JSProperties("cpMin") = AFormat(.Item("MinValue"))
                grid.JSProperties("cpMax") = AFormat(.Item("MaxValue"))
                grid.JSProperties("cpAve") = AFormat(.Item("AvgValue"))
                grid.JSProperties("cpR") = AFormat(.Item("RValue"))
                grid.JSProperties("cpC") = .Item("CValue")
                grid.JSProperties("cpNG") = .Item("NGValue")
                grid.JSProperties("cpMKDate") = .Item("MKDate")
                grid.JSProperties("cpMKUser") = .Item("MKUser")
                grid.JSProperties("cpQCDate") = .Item("QCDate")
                grid.JSProperties("cpQCUser") = .Item("QCUser")
                grid.JSProperties("cpSubLotNo") = .Item("SubLotNo")
                grid.JSProperties("cpRemarks") = .Item("Remarks")
                grid.JSProperties("cpRefresh") = "1"
            End With
        End If
    End Sub

    Private Function AFormat(v As Object) As String
        If v Is Nothing OrElse IsDBNull(v) Then
            Return ""
        Else
            Return Format(v, "0.000")
        End If
    End Function


    Private Sub grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles grid.CustomCallback
        Dim pFunction As String = Split(e.Parameters, "|")(0)
        Select Case pFunction
            Case "clear"
                up_ClearGrid()
            Case "load", "save", "approve"
                Dim pFactory As String = Split(e.Parameters, "|")(1)
                Dim pItemType As String = Split(e.Parameters, "|")(2)
                Dim pLine As String = Split(e.Parameters, "|")(3)
                Dim pItemCheck As String = Split(e.Parameters, "|")(4)
                Dim pDate As String = Split(e.Parameters, "|")(5)
                Dim pShift As String = Split(e.Parameters, "|")(6)
                Dim pSeq As String = Split(e.Parameters, "|")(7)
                Dim pVerified As String = Split(e.Parameters, "|")(8)
                pSeq = Val(pSeq)
                GridLoad(pFactory, pItemType, pLine, pItemCheck, pDate, pShift, pSeq, pVerified)
        End Select
    End Sub

    Private Sub grid_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles grid.RowUpdating
        e.Cancel = True
        Dim Result As New clsSPCResult
        Result.FactoryCode = cboFactory.Value
        Result.ItemCheckCode = cboItemCheck.Value
        Result.ItemTypeCode = cboType.Value
        Result.LineCode = cboLine.Value
        Result.ProdDate = dtDate.Value
        Result.ShiftCode = cboShift.Value
        Result.SequenceNo = cboSeq.Value
        Result.SubLotNo = Val(txtSubLotNo.Text)
        Result.Remark = txtRemarks.Text
        Result.RegisterUser = Session("user") & ""
        clsSPCResultDB.Insert(Result)

        Dim SeqNo As Integer = e.Keys("SeqNo")
        Dim Detail As New clsSPCResultDetail
        Detail.SPCResultID = Result.SPCResultID
        Detail.SequenceNo = SeqNo
        Detail.DeleteStatus = e.NewValues("DeleteStatus")
        Detail.Value = e.NewValues("Value")
        Detail.Remark = e.NewValues("Remark")
        Detail.RegisterUser = Result.RegisterUser
        clsSPCResultDetailDB.Insert(Detail)
        grid.CancelEdit()

        show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
    End Sub

    Private Sub cbkRefresh_Callback(source As Object, e As CallbackEventArgs) Handles cbkRefresh.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameter, "|")(1)
        Dim LineCode As String = Split(e.Parameter, "|")(2)
        Dim ItemCheckCode As String = Split(e.Parameter, "|")(3)
        Dim ProdDate As String = Split(e.Parameter, "|")(4)

        Dim Setup As clsChartSetup = clsChartSetupDB.GetData(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ProdDate)
        If Setup Is Nothing Then
            cbkRefresh.JSProperties("cpUSL") = " "
            cbkRefresh.JSProperties("cpLSL") = " "
            cbkRefresh.JSProperties("cpUCL") = " "
            cbkRefresh.JSProperties("cpLCL") = " "
        Else
            cbkRefresh.JSProperties("cpUSL") = Setup.SpecUSL
            cbkRefresh.JSProperties("cpLSL") = Setup.SpecLSL
            cbkRefresh.JSProperties("cpUCL") = Setup.XBarUCL
            cbkRefresh.JSProperties("cpLCL") = Setup.XBarLCL
        End If

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

    Protected Sub grid_BatchUpdate(sender As Object, e As ASPxDataBatchUpdateEventArgs)
        Dim Result As New clsSPCResult
        If e.UpdateValues.Count > 0 Then
            Result.FactoryCode = cboFactory.Value
            Result.ItemCheckCode = cboItemCheck.Value
            Result.ItemTypeCode = cboType.Value
            Result.LineCode = cboLine.Value
            Result.ProdDate = dtDate.Value
            Result.ShiftCode = cboShift.Value
            Result.SequenceNo = cboSeq.Value
            Result.SubLotNo = 0
            Result.Remark = ""
            Result.RegisterUser = Session("user") & ""

            clsSPCResultDB.Insert(Result)
            For i = 0 To e.UpdateValues.Count - 1
                Dim SeqNo As String = e.UpdateValues(i).Keys("SeqNo")
                Dim Detail As New clsSPCResultDetail
                Detail.SPCResultID = Result.SPCResultID
                Detail.SequenceNo = SeqNo
                Detail.DeleteStatus = e.UpdateValues(i).NewValues("DeleteStatus")
                Detail.Value = e.UpdateValues(i).NewValues("Value")
                Detail.Remark = e.UpdateValues(i).NewValues("Remark")
                Detail.RegisterUser = Result.RegisterUser
                clsSPCResultDetailDB.Insert(Detail)
            Next
        End If
    End Sub

    Private Sub cboShift_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboShift.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameter, "|")(1)
        Dim LineCode As String = Split(e.Parameter, "|")(2)
        Dim ItemCheckCode As String = Split(e.Parameter, "|")(3)
        cboShift.DataSource = clsFrequencyDB.GetShift(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode)
        cboShift.DataBind()
    End Sub

    Private Sub grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles grid.CellEditorInitialize
        If e.Column.FieldName = "Value" Then
            If IsDBNull(e.Value) Or grid.IsNewRowEditing Then
                e.Editor.ReadOnly = False
            Else
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = System.Drawing.Color.Silver
            End If
        ElseIf (e.Column.FieldName = "Remark" Or e.Column.FieldName = "DeleteStatus") Then
            e.Editor.ReadOnly = False
        Else
            e.Editor.ReadOnly = True
            e.Editor.ForeColor = System.Drawing.Color.Silver
        End If
    End Sub

    Private Sub grid_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles grid.HtmlDataCellPrepared
        If e.DataColumn.FieldName <> "Value" And e.DataColumn.FieldName <> "Remark" And e.DataColumn.FieldName <> "DeleteStatus" Then
            e.Cell.Attributes.Add("onclick", "event.cancelBubble = true")
        End If
    End Sub

    Private Sub grid_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs) Handles grid.HtmlRowPrepared
        If e.GetValue("DeleteStatus") IsNot Nothing AndAlso e.GetValue("DeleteStatus").ToString = "1" Then
            e.Row.BackColor = System.Drawing.Color.Silver
        ElseIf e.GetValue("JudgementColor") IsNot Nothing AndAlso Not IsDBNull(e.GetValue("JudgementColor")) Then
            If e.GetValue("JudgementColor") = "1" Then
                e.Row.BackColor = System.Drawing.Color.Pink
            ElseIf e.GetValue("JudgementColor") = "2" Then
                e.Row.BackColor = System.Drawing.Color.Red
            End If
        End If
    End Sub

    Private Sub cboSeq_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboSeq.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameter, "|")(1)
        Dim LineCode As String = Split(e.Parameter, "|")(2)
        Dim ItemCheckCode As String = Split(e.Parameter, "|")(3)
        Dim ShiftCode As String = Split(e.Parameter, "|")(4)
        Dim ProdDate As String = Split(e.Parameter, "|")(5)
        cboSeq.DataSource = clsFrequencyDB.GetSequence(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode)
        cboSeq.DataBind()
    End Sub

    Private Sub gridX_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles gridX.CustomCallback
        Dim FactoryCode As String = Split(e.Parameters, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameters, "|")(1)
        Dim LineCode As String = Split(e.Parameters, "|")(2)
        Dim ItemCheckCode As String = Split(e.Parameters, "|")(3)
        Dim ProdDate As String = Split(e.Parameters, "|")(4)
        GridXLoad(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ProdDate)
    End Sub

    Private Sub gridX_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs) Handles gridX.HtmlRowPrepared
        If e.KeyValue = "Min" Or e.KeyValue = "Max" Or e.KeyValue = "Avg" Or e.KeyValue = "R" Then
            'e.Row.BackColor = System.Drawing.Color.LightGray
            e.Row.BorderStyle = BorderStyle.Double
            e.Row.BorderWidth = 50
        End If
        If e.KeyValue = "-" Or e.KeyValue = "--" Then
            e.Row.BackColor = System.Drawing.Color.FromArgb(112, 112, 112)
            e.Row.Height = 5
        End If
    End Sub

    Private Sub LoadChartR(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String)
        Dim xr As List(Of clsXRChart) = clsXRChartDB.GetChartR(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
        If xr.Count = 0 Then
            chartR.JSProperties("cpShow") = "0"
        Else
            chartR.JSProperties("cpShow") = "1"
        End If
        With chartR
            .DataSource = xr
            Dim diagram As XYDiagram = CType(.Diagram, XYDiagram)
            diagram.AxisX.WholeRange.MinValue = 0
            diagram.AxisX.WholeRange.MaxValue = 12

            diagram.AxisX.GridLines.LineStyle.DashStyle = DashStyle.Solid
            diagram.AxisX.GridLines.MinorVisible = True
            diagram.AxisX.MinorCount = 1
            diagram.AxisX.GridLines.Visible = False



            Dim Setup As clsChartSetup = clsChartSetupDB.GetData(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
            diagram.AxisY.ConstantLines.Clear()
            If Setup IsNot Nothing Then
                Dim RCL As New ConstantLine("CL R")
                RCL.Color = Drawing.Color.Purple
                RCL.LineStyle.Thickness = 2
                RCL.LineStyle.DashStyle = DashStyle.DashDot
                diagram.AxisY.ConstantLines.Add(RCL)
                RCL.AxisValue = Setup.RCL

                Dim RUCL As New ConstantLine("UCL R")
                RUCL.Color = Drawing.Color.Purple
                RUCL.LineStyle.Thickness = 2
                RUCL.LineStyle.DashStyle = DashStyle.DashDot
                diagram.AxisY.ConstantLines.Add(RUCL)
                RUCL.AxisValue = Setup.RUCL

                Dim MaxValue As Double
                If xr.Count > 0 Then
                    If xr(0).MaxValue > Setup.RUCL Then
                        MaxValue = xr(0).MaxValue
                    Else
                        MaxValue = Setup.RUCL
                    End If
                End If
                diagram.AxisY.WholeRange.MaxValue = MaxValue
            End If
            .DataBind()
        End With
    End Sub

    Private Sub LoadChartX(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String)
        Dim xr As List(Of clsXRChart) = clsXRChartDB.GetChartXR(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
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

                diagram.AxisY.VisualRange.MinValue = Setup.SpecLSL
                diagram.AxisY.VisualRange.MaxValue = Setup.SpecUSL

                CType(.Diagram, XYDiagram).SecondaryAxesY.Clear()
                Dim myAxisY As New SecondaryAxisY("my Y-Axis")
                myAxisY.Visibility = DevExpress.Utils.DefaultBoolean.False
                CType(.Diagram, XYDiagram).SecondaryAxesY.Add(myAxisY)
                CType(.Series("Rule").View, XYDiagramSeriesViewBase).AxisY = myAxisY
            End If


            .DataBind()
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

        LoadChartX(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ProdDate)
    End Sub

    Private Sub gridX_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles gridX.HtmlDataCellPrepared
        Dim LCL As Double
        Dim UCL As Double
        Dim LSL As Double
        Dim USL As Double
        Dim SetupFound As Boolean = False
        If Not IsDBNull(e.CellValue) AndAlso (e.DataColumn.FieldName.StartsWith("1") Or e.DataColumn.FieldName.StartsWith("2")) _
            And (e.GetValue("Seq") = "1" Or e.GetValue("Seq") = "3" Or e.GetValue("Seq") = "4" Or e.GetValue("Seq") = "5") Then
            If (e.DataColumn.FieldName.StartsWith("1")) Then
                If Not IsDBNull(e.GetValue("XBarLCL1")) Then
                    SetupFound = True
                    LCL = e.GetValue("XBarLCL1")
                    UCL = e.GetValue("XBarUCL1")
                    LSL = e.GetValue("SpecLSL1")
                    USL = e.GetValue("SpecUSL1")
                End If
            ElseIf (e.DataColumn.FieldName.StartsWith("2")) Then
                If Not IsDBNull(e.GetValue("XBarLCL2")) Then
                    SetupFound = True
                    LCL = e.GetValue("XBarLCL2")
                    UCL = e.GetValue("XBarUCL2")
                    LSL = e.GetValue("SpecLSL2")
                    USL = e.GetValue("SpecUSL2")
                End If
            End If
            If SetupFound Then
                Dim Value As Double = clsSPCResultDB.ADecimal(e.CellValue)
                If Value < LSL Or Value > USL Then
                    e.Cell.BackColor = Color.Red
                ElseIf Value < LCL Or Value > UCL Then
                    e.Cell.BackColor = Color.Pink
                End If
            End If
        End If
        If e.KeyValue = "-" Or e.KeyValue = "--" Then
            e.Cell.Text = ""
        End If
    End Sub

    Private Sub chartR_CustomCallback(sender As Object, e As CustomCallbackEventArgs) Handles chartR.CustomCallback
        Dim Prm As String = e.Parameter
        If Prm = "" Then
            Prm = "F001|TPMSBR011|015|IC021|03 Aug 2022"
        End If
        Dim FactoryCode As String = Split(Prm, "|")(0)
        Dim ItemTypeCode As String = Split(Prm, "|")(1)
        Dim LineCode As String = Split(Prm, "|")(2)
        Dim ItemCheckCode As String = Split(Prm, "|")(3)
        Dim ProdDate As String = Split(Prm, "|")(4)
        LoadChartR(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ProdDate)
    End Sub

    Private Sub chartX_BoundDataChanged(sender As Object, e As EventArgs) Handles chartX.BoundDataChanged
        With chartX
            DirectCast(.Series("Rule").View, FullStackedBarSeriesView).Color = Color.Red
            DirectCast(.Series("Rule").View, FullStackedBarSeriesView).FillStyle.FillMode = FillMode.Solid
            DirectCast(.Series("Rule").View, FullStackedBarSeriesView).Transparency = 180
            DirectCast(.Series("Rule").View, FullStackedBarSeriesView).Border.Thickness = 1
        End With
    End Sub
End Class