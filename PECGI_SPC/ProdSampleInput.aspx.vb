Imports DevExpress.Web
Imports DevExpress.Web.Data

Public Class ProdSampleInput
    Inherits System.Web.UI.Page
    Dim pUser As String = ""
    Public AuthApprove As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public ValueType As String
    Dim GlobalPrm As String = ""

    Private Sub GridXLoad()
        With gridX
            .Columns.Clear()
            Dim Band1 As New GridViewBandColumn
            Band1.Caption = "DATE"
            .Columns.Add(Band1)

            Dim Band2 As New GridViewBandColumn
            Band2.Caption = "SHIFT"
            Band1.Columns.Add(Band2)

            Dim Col1 As New GridViewDataTextColumn
            Col1.FieldName = "Des"
            Col1.Caption = "TIME"
            Col1.Width = 90
            Col1.FixedStyle = GridViewColumnFixedStyle.Left
            Band2.Columns.Add(Col1)

            Dim SelDay As Date = Now.Date.AddDays(-1)
            For iDay = 1 To 2
                Dim BandDay As New GridViewBandColumn
                BandDay.Caption = Format(SelDay, "dd MMM yyyy")
                .Columns.Add(BandDay)

                For iShift = 1 To 2
                    Dim BandShift As New GridViewBandColumn
                    BandShift.Caption = "Shift " & iShift
                    BandDay.Columns.Add(BandShift)

                    For iTime = 1 To 5
                        Dim colTime As New GridViewDataTextColumn
                        colTime.Caption = iTime
                        BandShift.Columns.Add(colTime)
                    Next

                Next

                SelDay = SelDay.AddDays(1)
            Next
        End With
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        grid.JSProperties("cp_message") = ErrMsg
        grid.JSProperties("cp_type") = msgType
        grid.JSProperties("cp_val") = pVal
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GlobalPrm = Request.QueryString("Date") & ""
        sGlobal.getMenu("B020")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user") & ""
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B040")
        show_error(MsgTypeEnum.Info, "", 0)
        If Not IsPostBack And Not IsCallback Then
            up_FillCombo()
            If GlobalPrm <> "" Then
                dtDate.Value = CDate(Request.QueryString("Date"))
                Dim FactoryCode As String = Request.QueryString("FactoryCode")
                Dim ItemTypeCode As String = Request.QueryString("ItemTypeCode")
                Dim Line As String = Request.QueryString("Line")
                Dim ItemCheckCode As String = Request.QueryString("ItemCheckCode")
                Dim ProdDate As String = Request.QueryString("ProdDate")
                Dim Shift As String = Request.QueryString("Shift")
                Dim Sequence As String = Request.QueryString("Sequence")

                GridLoad(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate, Shift, Sequence)
            Else
                dtDate.Value = Now.Date
            End If
        End If
        GridXLoad()
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

        Dim SeqNo As Integer
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

    Private Sub up_ClearGrid()
        grid.DataSource = Nothing
        grid.DataBind()
        grid.JSProperties("cpUSL") = " "
        grid.JSProperties("cpLSL") = " "
        grid.JSProperties("cpUCL") = " "
        grid.JSProperties("cpLCL") = " "
        grid.JSProperties("cpMin") = " "
        grid.JSProperties("cpMax") = " "
        grid.JSProperties("cpAve") = " "
        grid.JSProperties("cpR") = " "
        grid.JSProperties("cpC") = " "
        grid.JSProperties("cpNG") = " "
    End Sub
    Protected Sub grid_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles grid.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            GridLoad(cboFactory.Value, cboType.Value, cboLine.Value, cboItemCheck.Value, Format(dtDate.Value, "yyyy-MM-dd"), cboShift.Value, cboSeq.Value)
        End If
    End Sub

    Private Sub GridLoad(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String, Shift As String, Sequence As Integer)
        Dim ErrMsg As String = ""
        Dim dt As DataTable = clsSPCResultDetailDB.GetTable(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate, Shift, Sequence)
        grid.DataSource = dt
        grid.DataBind()
        If dt.Rows.Count = 0 Then
            grid.JSProperties("cpUSL") = " "
            grid.JSProperties("cpLSL") = " "
            grid.JSProperties("cpUCL") = " "
            grid.JSProperties("cpLCL") = " "
            grid.JSProperties("cpMin") = " "
            grid.JSProperties("cpMax") = " "
            grid.JSProperties("cpAve") = " "
            grid.JSProperties("cpR") = " "
            grid.JSProperties("cpC") = " "
            grid.JSProperties("cpNG") = " "
        Else
            With dt.Rows(0)
                grid.JSProperties("cpUSL") = .Item("SpecUSL")
                grid.JSProperties("cpLSL") = .Item("SpecLSL")
                grid.JSProperties("cpUCL") = .Item("XBarUCL")
                grid.JSProperties("cpLCL") = .Item("XBarLCL")
                grid.JSProperties("cpMin") = .Item("MinValue")
                grid.JSProperties("cpMax") = .Item("MaxValue")
                grid.JSProperties("cpAve") = .Item("AvgValue")
                grid.JSProperties("cpR") = .Item("RValue")
                grid.JSProperties("cpC") = .Item("CValue")
                grid.JSProperties("cpNG") = .Item("NGValue")
            End With
        End If

    End Sub

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
                pSeq = Val(pSeq)
                GridLoad(pFactory, pItemType, pLine, pItemCheck, pDate, pShift, pSeq)
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
        Result.SubLotNo = 0
        Result.Remark = ""
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
            cbkRefresh.JSProperties("cpUSL") = ""
            cbkRefresh.JSProperties("cpLSL") = ""
            cbkRefresh.JSProperties("cpUCL") = ""
            cbkRefresh.JSProperties("cpLCL") = ""
            cbkRefresh.JSProperties("cpMin") = ""
            cbkRefresh.JSProperties("cpMax") = ""
            cbkRefresh.JSProperties("cpAve") = ""
            cbkRefresh.JSProperties("cpR") = ""
            cbkRefresh.JSProperties("cpNG") = ""
        Else
            cbkRefresh.JSProperties("cpUSL") = Setup.SpecUSL
            cbkRefresh.JSProperties("cpLSL") = Setup.SpecLSL
            cbkRefresh.JSProperties("cpUCL") = Setup.XBarUCL
            cbkRefresh.JSProperties("cpLCL") = Setup.XBarLCL
            cbkRefresh.JSProperties("cpMin") = Setup.Min
            cbkRefresh.JSProperties("cpMax") = Setup.Max
            cbkRefresh.JSProperties("cpAve") = Setup.Avg
            cbkRefresh.JSProperties("cpR") = Setup.R
            cbkRefresh.JSProperties("cpNG") = Setup.NG
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
        cboLine.DataSource = ClsLineDB.GetList(FactoryCode, ItemTypeCode)
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
            If IsDBNull(e.Value) Then
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
        ElseIf e.GetValue("Judgement") IsNot Nothing AndAlso e.GetValue("Judgement").ToString = "NG" Then
            e.Row.BackColor = System.Drawing.Color.Red
        End If
    End Sub

    Private Sub cboSeq_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboSeq.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameter, "|")(1)
        Dim LineCode As String = Split(e.Parameter, "|")(2)
        Dim ItemCheckCode As String = Split(e.Parameter, "|")(3)
        Dim ShiftCode As String = Split(e.Parameter, "|")(4)
        cboSeq.DataSource = clsFrequencyDB.GetSequence(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ShiftCode)
        cboSeq.DataBind()
    End Sub
End Class