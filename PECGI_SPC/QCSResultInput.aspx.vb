Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.IO

Public Class QCSResultInput
    Inherits System.Web.UI.Page
    Dim LastCycle As Integer
    Dim pUser As String = ""
    Public AuthApprove As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthInquiry As Boolean = False
    Public ValueType As String

    Dim NoProcess1 As String
    Dim NoProcess2 As String
    Dim NoProcess3 As String
    Dim NoProcess4 As String
    Dim NoProcess5 As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sGlobal.getMenu("B030")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user") & ""
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B030")
        btnSave.Enabled = AuthUpdate
        AuthInquiry = sGlobal.Auth_UserUpdate(pUser, "B040")
        btnInquiry.Enabled = AuthUpdate
        Dim User As clsUserSetup = clsUserSetupDB.GetData(pUser)
        If User Is Nothing Then
            btnApprove.Enabled = False
            btnList.Enabled = False
        ElseIf User.LineLeaderStatus = "1" Then
            btnApprove.Enabled = True
            btnList.Enabled = True
        Else
            btnApprove.Enabled = False
            btnList.Enabled = False
        End If
        show_error(MsgTypeEnum.Info, "", 0)
        up_FillComboLine()
        If Not IsPostBack And Not IsCallback Then
            If Request.Cookies("qcsDate") IsNot Nothing Then
                Session("qcsDate") = Request.Cookies("qcsDate").Value
                Session("qcsLineID") = Request.Cookies("qcsLineID").Value
                Session("qcsSubLineID") = Request.Cookies("qcsSubLineID").Value
                Session("qcsPartID") = Request.Cookies("qcsPartID").Value
                Session("qcsShift") = Request.Cookies("qcsShift").Value
            End If
            If Session("qcsDate") & "" <> "" And Session("qcsLineID") & "" <> "" And Session("qcsPartID") & "" <> "" Then
                dtrevdate.Value = CDate(Session("qcsDate"))
                cboShift.Value = Session("qcsShift")

                Dim pLineID As String = Session("qcsLineID")
                Dim ul As clsUserLine = clsUserLineDB.GetData(pUser, pLineID)
                Dim dt As DataTable
                If ul IsNot Nothing Then
                    dt = ClsSubLineDB.GetDataSubLine(pLineID, "")
                    cboSubLine.DataSource = dt
                    cboSubLine.DataBind()
                    cboLineID.Value = pLineID
                    cboSubLine.Value = Session("qcsSubLineID")

                    dt = clsQCSResultDB.GetPart(pLineID)
                    cboPartID.DataSource = dt
                    cboPartID.DataBind()

                    cboPartID.Value = Session("qcsPartID")
                    txtpartname.Text = cboPartID.SelectedItem.GetValue("PartName")
                    LoadResult(Format(dtrevdate.Value, "yyyy-MM-dd"), cboLineID.Value, cboSubLine.Value, cboPartID.Value, cboShift.Value, "0", True)
                End If
                'cboRevNo.Value = Session("qcsRevNo")
                'up_GridLoad(Format(dtrevdate.Value, "yyyy-MM-dd"), cboLineID.Value, cboSubLine.Value, cboPartID.Value, cboShift.Value, cboRevNo.Value)
            Else
                dtrevdate.Value = Now.Date
                btnDelete.ClientEnabled = False
                btnSave.ClientEnabled = False
                btnApprove.ClientEnabled = False
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
        dsline = ClsQCSMasterDB.GetDataLine(1, pUser, ErrMsg)
        If ErrMsg = "" Then
            cboLineID.DataSource = dsline
            cboLineID.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub SetHeader(pShift As String)
        Dim qt As clsQCSTimeCycle = clsQCSTimeDB.GetCycle(pShift)
        Dim qs As clsQCSLastUpdate = clsQCSTimeDB.GetLastUpdate(dtrevdate.Value, cboLineID.Value, cboSubLine.Value, cboPartID.Value, cboRevNo.Value, cboShift.Value)
        With grid
            .Columns("Cycle1").Caption = "STD " & qt.Cycle1Time & vbCrLf & "ACT " & qs.LastUpdate11
            .Columns("Cycle2").Caption = "STD " & qt.Cycle2Time & vbCrLf & "ACT " & qs.LastUpdate12
            .Columns("Cycle3").Caption = "STD " & qt.Cycle3Time & vbCrLf & "ACT " & qs.LastUpdate13
            .Columns("Cycle4").Caption = "STD " & qt.Cycle4Time & vbCrLf & "ACT " & qs.LastUpdate14
            .Columns("Cycle5").Caption = "STD " & qt.Cycle5Time & vbCrLf & "ACT " & qs.LastUpdate15
        End With
    End Sub

    Private Sub Grid_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles grid.HtmlDataCellPrepared
        If e.DataColumn.FieldName = "SeqNo" Or e.DataColumn.FieldName = "ProcessName" Or e.DataColumn.FieldName = "KPointStatus" _
        Or e.DataColumn.FieldName = "Item" Or e.DataColumn.FieldName = "Standard" Then
            Dim KeyValue As Integer = 0
            If e.KeyValue IsNot Nothing Then
                KeyValue = e.KeyValue.ToString.Split("|")(0)
            End If
            If KeyValue >= 998 Then
                e.Cell.BackColor = Drawing.Color.Silver
            End If
        End If        
        For i = 1 To 5
            Dim fn As String = "Cycle" & i
            Dim NoProcess As String = ""
            If i = 1 Then
                NoProcess = NoProcess1
            ElseIf i = 2 Then
                NoProcess = NoProcess2
            ElseIf i = 3 Then
                NoProcess = NoProcess3
            ElseIf i = 4 Then
                NoProcess = NoProcess4
            ElseIf i = 5 Then
                NoProcess = NoProcess5
            End If
            If e.DataColumn.FieldName = fn Then
                If NoProcess = "1" Or NoProcess = "2" Or NoProcess = "3" Or NoProcess = "4" Then
                    Dim KeyValue As Integer = 0
                    If e.KeyValue IsNot Nothing Then
                        KeyValue = e.KeyValue.ToString.Split("|")(0)
                    End If
                    If KeyValue < 1000 Then
                        e.Cell.BackColor = Drawing.Color.Yellow
                    End If
                ElseIf e.GetValue("Status" & i) & "" = "NG" Then
                    e.Cell.BackColor = Drawing.Color.Red
                End If
            End If
        Next
        'Dim FreqType As String = e.GetValue("FrequencyType") & ""
        'If FreqType = "B" Then
        '    Dim colStart As Integer
        '    If cboShift.Text = "1" Then
        '        colStart = 2
        '    Else
        '        colStart = 1
        '    End If
        '    For i = colStart To 5
        '        If e.DataColumn.FieldName = "Cycle" & i Then
        '            e.Cell.BackColor = Drawing.Color.Silver
        '        End If
        '    Next
        'End If
        Return        
    End Sub

    Private Sub cbkValid_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkValid.Callback
        Dim pFunction As String = Split(e.Parameter, "|")(0)
        Dim pLineID As String = Split(e.Parameter, "|")(1)
        Dim pPartID As String = Split(e.Parameter, "|")(2)
        Dim strMsg As String = ""
        cbkValid.JSProperties("cpValidationMsg") = strMsg
    End Sub

    Private Sub up_GridLoadApproval()
        Dim dt As DataTable = clsQCSResultShiftDB.GetApprovalList(pUser)
        gridApproval.DataSource = dt
        gridApproval.DataBind()
        If btnList.Enabled Then
            btnList.ClientEnabled = dt.Rows.Count > 0
        End If
    End Sub

    Private Sub up_GridLoad(pDate As Date, pLineID As String, pSubLineID As String, ByVal pPartID As String, ByVal pShift As String, pRevNo As Integer)
        Dim ErrMsg As String = ""
        If pLineID Is Nothing Then
            Return
        End If
        If pLineID = "null" Then
            pLineID = ""
        End If
        If pSubLineID = "null" Then
            pSubLineID = ""
        End If
        If pPartID = "null" Then
            pPartID = ""
        End If
        If pShift = "null" Then
            pShift = ""
        End If
        If pPartID = "" Then
            Session("qcsDate") = ""
            Response.Cookies("qcsDate").Value = ""
            Response.Cookies("qcsDate").Expires = Now.AddDays(3)
        Else
            Session("qcsDate") = Format(pDate, "yyyy-MM-dd")
            Response.Cookies("qcsDate").Value = Format(pDate, "yyyy-MM-dd")
            Response.Cookies("qcsDate").Expires = Now.AddDays(3)
        End If
        If pLineID <> "" Then
            Session("qcsLineID") = pLineID
            Session("qcsSubLineID") = pSubLineID
            Response.Cookies("qcsLineID").Value = pLineID
            Response.Cookies("qcsSubLineID").Value = pSubLineID
        End If
        If pPartID <> "" Then
            Session("qcsPartID") = pPartID
            Response.Cookies("qcsPartID").Value = pPartID
        End If
        Session("qcsShift") = pShift
        Session("qcsRevNo") = pRevNo
        Response.Cookies("qcsShift").Value = pShift
        Response.Cookies("qcsRevNo").Value = pRevNo

        Response.Cookies("qcsLineID").Expires = Now.AddDays(3)
        Response.Cookies("qcsSubLineID").Expires = Now.AddDays(3)
        Response.Cookies("qcsPartID").Expires = Now.AddDays(3)
        Response.Cookies("qcsShift").Expires = Now.AddDays(3)
        Response.Cookies("qcsRevNo").Expires = Now.AddDays(3)

        Dim ds As DataSet = clsQCSResultDB.GetDataSet(Format(pDate, "yyyy-MM-dd"), pLineID, pSubLineID, pPartID, pShift, pRevNo)
        Dim dt As DataTable = ds.Tables(0)
        If dt.Rows.Count = 0 Then
            NoProcess1 = ""
            NoProcess2 = ""
            NoProcess3 = ""
            NoProcess4 = ""
            NoProcess5 = ""
        Else
            Dim iRow As Integer = 0
            For iRow = 0 To dt.Rows.Count - 1
                If dt.Rows(iRow)("LevelNo") = 1001 Then
                    Exit For
                End If
            Next
            If iRow > 0 Then
                NoProcess1 = Val(dt.Rows(iRow)("Cycle1") & "")
                NoProcess2 = Val(dt.Rows(iRow)("Cycle2") & "")
                NoProcess3 = Val(dt.Rows(iRow)("Cycle3") & "")
                NoProcess4 = Val(dt.Rows(iRow)("Cycle4") & "")
                NoProcess5 = Val(dt.Rows(iRow)("Cycle5") & "")
            End If
        End If
        Dim dt2 As DataTable = ds.Tables(1)
        If ErrMsg = "" Then
            LastCycle = 0
            grid.DataSource = Nothing
            grid.DataBind()
            grid.DataSource = dt
            grid.DataBind()
            SetHeader(pShift)
            grid.JSProperties("cpEnableApprove") = ""
            grid.JSProperties("cpEnableSave") = ""
            grid.JSProperties("cpRequired") = ""
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("RequiredAttachmentStatus") = "1" Then
                    grid.JSProperties("cpRequired") = "1"
                End If
            End If
            If dt2.Rows.Count > 0 Then
                With dt2.Rows(0)
                    grid.JSProperties("cpNRP2") = .Item("PIC2") & ""
                    grid.JSProperties("cpNRP3") = .Item("PIC3") & ""
                    grid.JSProperties("cpNRP4") = .Item("PIC4") & ""
                    grid.JSProperties("cpNRP5") = .Item("PIC5") & ""
                End With
            Else
                grid.JSProperties("cpNRP2") = ""
                grid.JSProperties("cpNRP3") = ""
                grid.JSProperties("cpNRP4") = ""
                grid.JSProperties("cpNRP5") = ""
            End If

        Else
            grid.JSProperties("cpNRP2") = ""
            grid.JSProperties("cpNRP3") = ""
            grid.JSProperties("cpNRP4") = ""
            grid.JSProperties("cpNRP5") = ""
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Protected Sub grid_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs)
        Try
            Session("batcherror") = ""
            Dim R As New clsQCSResult
            R.QCSDate = dtrevdate.Value
            R.RevNo = cboRevNo.Value
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
                Rs.Remark = txtRemark.Text
                Rs.ChangePoint = cboCP.Value
                clsQCSResultShiftDB.Insert(Rs, Cn, Tr)

                For iCycle = 1 To 5
                    Dim Rsc As New clsQCSResultShiftCycle
                    Rsc.QCSResultShiftID = Rs.QCSResultShiftID
                    Rsc.Cycle = iCycle
                    Rsc.LotNo = ""
                    Rsc.PIC = ""
                    Rsc.ProcessCls = ""
                    clsQCSResultShiftCycleDB.Insert(Rsc, Cn, Tr)

                    For i As Integer = 0 To e.UpdateValues.Count - 1
                        If e.UpdateValues(i).Keys("LevelNo") = 998 Then
                            Rsc.LotNo = e.UpdateValues(i).NewValues("Cycle" & iCycle) & ""
                            clsQCSResultShiftCycleDB.UpdateLotNo(Rsc, Cn, Tr)
                        ElseIf e.UpdateValues(i).Keys("LevelNo") = 999 Then
                            Rsc.Notes = e.UpdateValues(i).NewValues("Cycle" & iCycle) & ""
                            clsQCSResultShiftCycleDB.UpdateNotes(Rsc, Cn, Tr)
                        ElseIf e.UpdateValues(i).Keys("LevelNo") = 1000 Then
                            Rsc.PIC = e.UpdateValues(i).NewValues("Cycle" & iCycle) & ""
                            clsQCSResultShiftCycleDB.UpdatePIC(Rsc, Cn, Tr)
                        ElseIf e.UpdateValues(i).Keys("LevelNo") = 1001 Then
                            Rsc.ProcessCls = e.UpdateValues(i).NewValues("Cycle" & iCycle) & ""
                            clsQCSResultShiftCycleDB.UpdateProcess(Rsc, Cn, Tr)
                        Else
                            Dim LineID As String = e.UpdateValues(i).OldValues("LineID")
                            Dim ItemID As String = e.UpdateValues(i).OldValues("ItemID")
                            Dim Cycle As String = e.UpdateValues(i).NewValues.Keys.Count

                            Dim Rsci As New clsQCSResultShiftCycleItem
                            Rsci.QCSResultShiftCycleID = Rsc.QCSResultShiftCycleID
                            Rsci.ItemID = ItemID
                            Dim Value As String = e.UpdateValues(i).NewValues("Cycle" & iCycle)
                            Rsci.ValueType = e.UpdateValues(i).NewValues("ValueType")
                            If IsDBNull(e.UpdateValues(i).NewValues("Cycle" & iCycle)) Or e.UpdateValues(i).NewValues("Cycle" & iCycle) Is Nothing Then
                                Rsci.NumValue = Nothing
                            ElseIf Rsci.ValueType = "N" Then
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
        Catch ex As Exception
            Session("batcherror") = ex.Message
        Finally
            e.Handled = True
        End Try
    End Sub

    Protected Sub Grid_CustomJSProperties(ByVal sender As Object, ByVal e As ASPxGridViewClientJSPropertiesEventArgs)
        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
        Dim readOnlyColumns As New List(Of Integer)()
        For Each column In grid.Columns
            If TypeOf (column) Is GridViewDataColumn Then
                If Not column.FieldName.StartsWith("Cycle") Then
                    readOnlyColumns.Add(column.Index)
                End If
            End If
        Next column
        e.Properties("cpReadOnlyColumns") = readOnlyColumns
    End Sub

    Private Sub grid_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles grid.CustomCallback
        Dim pFunction As String = Split(e.Parameters, "|")(0)
        Select Case pFunction
            Case "GetData"
                Try
                    Session("batcherror") = ""
                    Dim R As New clsQCSResult
                    R.QCSDate = dtrevdate.Value
                    R.RevNo = cboRevNo.Value
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
                        Rs.Remark = txtRemark.Text
                        Rs.ChangePoint = cboCP.Value
                        clsQCSResultShiftDB.Insert(Rs, Cn, Tr)

                        For iCycle = 1 To 5
                            Dim Rsc As New clsQCSResultShiftCycle
                            Rsc.QCSResultShiftID = Rs.QCSResultShiftID
                            Rsc.Cycle = iCycle
                            Rsc.LotNo = ""
                            Rsc.PIC = ""
                            Rsc.ProcessCls = ""
                            clsQCSResultShiftCycleDB.Insert(Rsc, Cn, Tr)

                            Dim AmbilData As DataSet
                            AmbilData = clsQCSResultShiftCycleItemDB.GetData(cboShift.Value, iCycle)
                            Dim AmbilDataCount As Integer = AmbilData.Tables(0).Rows.Count
                            If AmbilData.Tables(0).Rows.Count = 0 Then
                            Else
                                For item = 0 To AmbilDataCount - 1
                                    Dim LineID As String = cboLineID.Value
                                    Dim ItemID As String = AmbilData.Tables(0).Rows(item)("ItemID").ToString()
                                    Dim Rsci As New clsQCSResultShiftCycleItem
                                    Rsci.QCSResultShiftCycleID = Rsc.QCSResultShiftCycleID
                                    Rsci.ItemID = ItemID
                                    Dim Value As String = AmbilData.Tables(0).Rows(item)("Value").ToString()
                                    Rsci.NumValue = Value
                                    clsQCSResultShiftCycleItemDB.InsertGetData(Rsci, Cn, Tr)
                                Next
                            End If
                        Next                        
                        Tr.Commit()
                    End Using
                Catch ex As Exception
                    Session("batcherror") = ex.Message
                Finally
                    'e.Handled = True
                End Try
                grid.JSProperties("cp_loadgrid") = 1
            Case "clear"
                up_GridLoad(Now.Date, "", "", "", "", 0)
                grid.JSProperties("cp_cleargrid") = 1
            Case "load", "save", "approve"
                Dim pDate As String = Split(e.Parameters, "|")(1)
                Dim pLineID As String = Split(e.Parameters, "|")(2)
                Dim pSubLineID As String = Split(e.Parameters, "|")(3)
                Dim pPartID As String = Split(e.Parameters, "|")(4)
                Dim pShift As String = Val(Split(e.Parameters, "|")(5))
                Dim pRevNo As String = Val(Split(e.Parameters, "|")(6))
                If Session("batcherror") & "" <> "" Then
                    SetHeader(pShift)
                    show_error(MsgTypeEnum.ErrorMsg, Session("batcherror"), 1)
                    Session("batcherror") = ""
                    Return
                End If
                If pFunction = "save" Then
                    Dim pChangePoint As String = Split(e.Parameters, "|")(7)
                    If pChangePoint = "null" Or pChangePoint = "" Then
                        pChangePoint = "0"
                    End If
                    Dim pRemark As String = Split(e.Parameters, "|")(8)
                    clsQCSResultShiftDB.UpdateRemark(pDate, pShift, pLineID, pSubLineID, pPartID, pRevNo, pChangePoint, pRemark)
                    grid.JSProperties("cpEnableSave") = "1"
                    Using Cn As New SqlConnection(Sconn.Stringkoneksi)
                        Cn.Open()
                        Using Tr As SqlTransaction = Cn.BeginTransaction
                            clsQCSResultShiftCycleDB.Judge(pDate, pLineID, pSubLineID, pPartID, pRevNo, pShift, Cn, Tr)
                            Tr.Commit()
                        End Using
                    End Using                    
                End If
                up_GridLoad(pDate, pLineID, pSubLineID, pPartID, pShift, pRevNo)
                If pFunction = "save" Then
                    grid.JSProperties("cpEnableSave") = "1"
                End If
        End Select
        If pFunction = "save" Then
            show_error(MsgTypeEnum.Success, "Save data successful", 1)
        ElseIf pFunction = "approve" Then
            show_error(MsgTypeEnum.Success, "Approve data successful", 1)
        End If
        Session("batcherror") = ""
    End Sub

    Protected Sub grid_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles grid.RowInserting
        e.Cancel = True
        grid.CancelEdit()
    End Sub

    Private Sub grid_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles grid.RowUpdating
        e.Cancel = True
        grid.CancelEdit()
    End Sub

    Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles grid.RowDeleting
        e.Cancel = True
        grid.CancelEdit()
    End Sub

    Protected Sub grid_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles grid.StartRowEditing

    End Sub

    Protected Sub grid_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs)

    End Sub

    Protected Sub grid_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles grid.AfterPerformCallback
        Dim commandColumn = TryCast(grid.Columns(0), GridViewCommandColumn)
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            up_GridLoad(Format(dtrevdate.Value, "yyyy-MM-dd"), cboLineID.Value, cboSubLine.Value, cboPartID.Value, cboShift.Value, cboRevNo.Value)
        End If
    End Sub

    Private Sub cbopartid_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cboPartID.Callback
        Dim pAction As String = Split(e.Parameter, "|")(0)
        Dim pLineID As String = Split(e.Parameter, "|")(1)
        Dim dt As DataTable
        dt = clsQCSResultDB.GetPart(pLineID)
        cboPartID.DataSource = dt
        cboPartID.DataBind()
        cboPartID.JSProperties("cpAction") = pAction
    End Sub

    Private Sub cboSubLine_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cboSubLine.Callback
        Dim pLineID As String = Split(e.Parameter, "|")(1)
        Dim dt As DataTable
        dt = ClsSubLineDB.GetDataSubLine(pLineID, "")
        cboSubLine.DataSource = dt
        cboSubLine.DataBind()
    End Sub

    Private Sub LoadResult(pDate As String, pLineID As String, pSubLineID As String, pPartID As String, pShift As String, pRevNo As String, Optional IsInit As Boolean = False)
        cbkRefresh.JSProperties("cpApproveMsg") = ""
        If pRevNo = 0 Then
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
                pRevNo = Rv(0).RevNo
                cbkRefresh.JSProperties("cpRevDate") = Format(Rv(0).RevDate, "dd MMM yyyy")
            End If
        Else
            cbkRefresh.JSProperties("cpRevNo") = "-"
            cbkRefresh.JSProperties("cpRevDate") = clsQCSResultDB.GetRevDate(pDate, pPartID, pLineID, pSubLineID, pRevNo)
        End If

        Dim Approved As Boolean = False
        Dim Rs As clsQCSResultShift = clsQCSResultShiftDB.GetData(pDate, pShift, pPartID, pLineID, pSubLineID, Val(pRevNo))
        If Rs Is Nothing Then
            cbkRefresh.JSProperties("cpNotes") = "0"
            cbkRefresh.JSProperties("cpRemark") = ""
            cbkRefresh.JSProperties("cpApproval") = ""
            cbkRefresh.JSProperties("cpPIC") = ""
            cbkRefresh.JSProperties("cpDate") = ""
            cbkRefresh.JSProperties("cpFileName") = ""
        Else
            cbkRefresh.JSProperties("cpNotes") = Rs.ChangePoint
            cbkRefresh.JSProperties("cpRemark") = Rs.Remark
            cbkRefresh.JSProperties("cpApproval") = Rs.ApprovalStatus
            cbkRefresh.JSProperties("cpPIC") = Rs.ApprovalPIC
            cbkRefresh.JSProperties("cpDate") = Format(Rs.ApprovalDate, "dd MMM yyyy")
            cbkRefresh.JSProperties("cpFileName") = Rs.FileName
            If Rs.ApprovalStatus = "1" Then
                Approved = True
                cbkRefresh.JSProperties("cpEnableApprove") = ""
                cbkRefresh.JSProperties("cpEnableSave") = ""
            End If
        End If
        If Approved = False Then
            Dim strMsg As String = "" 'clsQCSResultShiftDB.LastShiftApproved(pDate, pShift, pPartID, pLineID, pSubLineID)            
            If strMsg = "" Then
                Dim LastRevNo As Integer = clsQCSResultDB.GetLastRevNo(pDate, pPartID, pLineID, pSubLineID)
                If Val(pRevNo) <> LastRevNo Then
                    cbkRefresh.JSProperties("cpEnableApprove") = ""
                    cbkRefresh.JSProperties("cpEnableSave") = ""
                Else
                    cbkRefresh.JSProperties("cpEnableApprove") = "1"
                    cbkRefresh.JSProperties("cpEnableSave") = "1"
                End If
            Else
                cbkRefresh.JSProperties("cpEnableApprove") = ""
                cbkRefresh.JSProperties("cpEnableSave") = ""
                cbkRefresh.JSProperties("cpApproveMsg") = strMsg
            End If
        End If
        If IsInit = False Then
            If btnSave.Enabled = False Then
                cbkRefresh.JSProperties("cpEnableSave") = ""
            End If
            If btnApprove.Enabled = False Then
                cbkRefresh.JSProperties("cpEnableApprove") = ""
            End If
        End If
    End Sub

    Private Sub cbkRefresh_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkRefresh.Callback
        Dim pDate As String = Split(e.Parameter, "|")(1)
        Dim pLineID As String = Split(e.Parameter, "|")(2)
        Dim pSubLineID As String = Split(e.Parameter, "|")(3)
        Dim pPartID As String = Split(e.Parameter, "|")(4)
        Dim pShift As String = Split(e.Parameter, "|")(5)
        Dim pRevNo As String = Split(e.Parameter, "|")(6)

        LoadResult(pDate, pLineID, pSubLineID, pPartID, pShift, pRevNo)
    End Sub

    Private Sub cbkApprove_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkApprove.Callback
        Dim pDate As String = Split(e.Parameter, "|")(1)
        Dim pLineID As String = Split(e.Parameter, "|")(2)
        Dim pSubLineID As String = Split(e.Parameter, "|")(3)
        Dim pPartID As String = Split(e.Parameter, "|")(4)
        Dim pShift As String = Split(e.Parameter, "|")(5)
        Dim pRevNo As String = Split(e.Parameter, "|")(6)
        Dim pUser As String = Session("User") & ""
        Dim Rs As clsQCSResultShift = clsQCSResultShiftDB.GetData(pDate, pShift, pPartID, pLineID, pSubLineID, pRevNo)
        If Rs IsNot Nothing Then
            Rs.ApprovalStatus = "1"
            Rs.ApprovalPIC = pUser
            clsQCSResultShiftDB.Approve(Rs)
            cbkApprove.JSProperties("cpValidationMsg") = ""
        Else
            cbkApprove.JSProperties("cpValidationMsg") = "Data is not found!"
        End If
    End Sub

    Private Sub cbkValidateApprove_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkValidateApprove.Callback
        Dim pDate As String = Split(e.Parameter, "|")(1)
        Dim pLineID As String = Split(e.Parameter, "|")(2)
        Dim pSubLineID As String = Split(e.Parameter, "|")(3)
        Dim pPartID As String = Split(e.Parameter, "|")(4)
        Dim pShift As String = Split(e.Parameter, "|")(5)
        Dim pRevNo As String = Split(e.Parameter, "|")(6)

        cbkValidateApprove.JSProperties("cpValidationMsg") = ""
        Dim ErrMsg As String = clsQCSResultShiftDB.ValidatePIC(pDate, pShift, pPartID, pLineID, pSubLineID, pRevNo)
        cbkValidateApprove.JSProperties("cpValidationMsg") = ErrMsg
    End Sub

    Private Sub show_attach(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        cbkAttach.JSProperties("cp_message") = ErrMsg
        cbkAttach.JSProperties("cp_type") = msgType
        cbkAttach.JSProperties("cp_val") = pVal
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim PostedFile As HttpPostedFile = uploader1.PostedFile
        Dim FileName As String = Path.GetFileName(PostedFile.FileName)
        Dim FileExtension As String = Path.GetExtension(FileName)
        Dim FileSize As Single = PostedFile.ContentLength / 1024 / 1024
        If FileSize = 0 Or FileName = "" Then
            show_attach(MsgTypeEnum.Warning, "No file to upload!", 1)
            Exit Sub
        End If
        Dim R As New clsQCSResult
        R.QCSDate = dtrevdate.Value
        R.RevNo = Val(hfRevNo("revno"))
        R.LineID = cboLineID.Value
        R.SubLineID = cboSubLine.Value
        R.PartID = cboPartID.Value
        R.CreateUser = Session("user") & ""
        Dim QCSPath As String = ""
        Try
            Using Cn As New SqlConnection(Sconn.Stringkoneksi)
                Cn.Open()
                Using Tr As SqlTransaction = Cn.BeginTransaction(IsolationLevel.ReadCommitted)
                    Dim DestDir As String
                    QCSPath = clsSetting.QCSPath
                    If QCSPath = "" Then
                        DestDir = Server.MapPath("~/QCS/" & Format(R.QCSDate, "yyyyMMdd") & "/" & R.LineID & "/" & R.PartID & "/" & cboShift.Value)
                    Else
                        DestDir = QCSPath & "\QCS\" & Format(R.QCSDate, "yyyyMMdd") & "\" & R.LineID & "\" & R.PartID & "\" & cboShift.Value
                    End If
                    If Not Directory.Exists(DestDir) Then
                        Directory.CreateDirectory(DestDir)
                    End If
                    Dim Dest As String = DestDir & "\" & FileName
                    uploader1.SaveAs(Dest)

                    clsQCSResultDB.Insert(R, Cn, Tr)
                    Dim Rs As New clsQCSResultShift
                    Rs.QCSResultID = R.QCSResultID
                    Rs.Shift = cboShift.Value
                    Rs.Remark = txtRemark.Text
                    Rs.ChangePoint = cboCP.Value
                    clsQCSResultShiftDB.Insert(Rs, Cn, Tr)

                    clsQCSResultShiftDB.UpdateAttachment(R.QCSDate, Rs.Shift, R.LineID, R.SubLineID, R.PartID, R.RevNo, FileName, Cn, Tr)
                    Tr.Commit()
                End Using
            End Using
            cbkAttach.JSProperties("cpAttach") = "1"
            cbkAttach.JSProperties("cpRemark") = txtRemark.Text
            cbkAttach.JSProperties("cpNotes") = cboCP.Value
            cbkAttach.JSProperties("cpFileName") = FileName
            cbkAttach.JSProperties("cpEnableSave") = IIf(btnSave.ClientEnabled, "1", "")
            cbkAttach.JSProperties("cpEnableApprove") = IIf(btnApprove.ClientEnabled, "1", "")
            cbkAttach.JSProperties("cpRevNo") = Val(hfRevNo("revno"))
            up_GridLoad(R.QCSDate, R.LineID, R.SubLineID, R.PartID, cboShift.Value, R.RevNo)
            cbkAttach.JSProperties("cpNRP2") = grid.JSProperties("cpNRP2")
            cbkAttach.JSProperties("cpNRP3") = grid.JSProperties("cpNRP3")
            cbkAttach.JSProperties("cpNRP4") = grid.JSProperties("cpNRP4")
            cbkAttach.JSProperties("cpNRP5") = grid.JSProperties("cpNRP5")

            show_attach(MsgTypeEnum.Success, "Upload successful", 1)
            btnSave.ClientEnabled = True
        Catch ex As Exception
            cbkAttach.JSProperties("cpAttach") = "1"
            cbkAttach.JSProperties("cpRemark") = txtRemark.Text
            cbkAttach.JSProperties("cpNotes") = cboCP.Value
            cbkAttach.JSProperties("cpFileName") = FileName
            cbkAttach.JSProperties("cpEnableSave") = IIf(btnSave.ClientEnabled, "1", "")
            cbkAttach.JSProperties("cpEnableApprove") = IIf(btnApprove.ClientEnabled, "1", "")
            cbkAttach.JSProperties("cpRevNo") = Val(hfRevNo("revno"))
            up_GridLoad(R.QCSDate, R.LineID, R.SubLineID, R.PartID, cboShift.Value, R.RevNo)
            cbkAttach.JSProperties("cpNRP2") = grid.JSProperties("cpNRP2")
            cbkAttach.JSProperties("cpNRP3") = grid.JSProperties("cpNRP3")
            cbkAttach.JSProperties("cpNRP4") = grid.JSProperties("cpNRP4")
            cbkAttach.JSProperties("cpNRP5") = grid.JSProperties("cpNRP5")
            show_attach(MsgTypeEnum.ErrorMsg, "Could not access QCS Path " & QCSPath, 1)
            btnSave.ClientEnabled = True
        End Try
    End Sub

    Private Sub cbkRemove_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkRemove.Callback
        Dim pDate As String = Split(e.Parameter, "|")(1)
        Dim pLineID As String = Split(e.Parameter, "|")(2)
        Dim pSubLineID As String = Split(e.Parameter, "|")(3)
        Dim pPartID As String = Split(e.Parameter, "|")(4)
        Dim pShift As String = Val(Split(e.Parameter, "|")(5))
        Dim pRevNo As String = Val(Split(e.Parameter, "|")(6))
        Dim pFile As String = Split(e.Parameter, "|")(7)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Using Tr As SqlTransaction = Cn.BeginTransaction
                Dim i As Integer = clsQCSResultShiftDB.UpdateAttachment(pDate, pShift, pLineID, pSubLineID, pPartID, pRevNo, "", Cn, Tr)
                Dim FilePath As String
                Dim QCSPath As String = clsSetting.QCSPath
                If QCSPath = "" Then
                    FilePath = Server.MapPath("~/QCS/" & Format(CDate(pDate), "yyyyMMdd") & "/" & pLineID & "/" & pPartID & "/" & pShift & "/" & pFile)
                Else
                    FilePath = QCSPath & "\QCS\" & Format(CDate(pDate), "yyyyMMdd") & "\" & pLineID & "\" & pPartID & "\" & pShift & "\" & pFile
                End If
                If File.Exists(FilePath) Then
                    File.Delete(FilePath)
                    Tr.Commit()
                Else
                    cbkRemove.JSProperties("cpValidationMsg") = "File is not found!"
                    Return
                End If
            End Using
        End Using
        cbkRemove.JSProperties("cpValidationMsg") = ""
    End Sub

    Protected Sub cbkDelete_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkDelete.Callback
        Dim pDate As String = Split(e.Parameter, "|")(1)
        Dim pLineID As String = Split(e.Parameter, "|")(2)
        Dim pSubLineID As String = Split(e.Parameter, "|")(3)
        Dim pPartID As String = Split(e.Parameter, "|")(4)
        Dim pShift As String = Split(e.Parameter, "|")(5)
        Dim pRevNo As String = Split(e.Parameter, "|")(6)
        Dim pUser As String = Session("User") & ""
        Dim Rs As clsQCSResultShift = clsQCSResultShiftDB.GetData(pDate, pShift, pPartID, pLineID, pSubLineID, pRevNo)
        If Rs IsNot Nothing Then
            clsQCSResultShiftDB.Delete(Rs.QCSResultShiftID)
            Dim pFile As String = Rs.FileName
            If pFile <> "" Then
                Dim QCSPath As String = clsSetting.QCSPath
                Dim FilePath As String
                If QCSPath = "" Then
                    FilePath = Server.MapPath("~/QCS/" & Format(CDate(pDate), "yyyyMMdd") & "/" & pLineID & "/" & pPartID & "/" & pShift & "/" & pFile)
                Else
                    FilePath = QCSPath & "\QCS\" & Format(CDate(pDate), "yyyyMMdd") & "\" & pLineID & "\" & pPartID & "\" & pShift & "\" & pFile
                End If
                If File.Exists(FilePath) Then
                    File.Delete(FilePath)
                End If
            End If
            cbkDelete.JSProperties("cpValidationMsg") = ""
        Else
            cbkDelete.JSProperties("cpValidationMsg") = "Data is not found!"
        End If
    End Sub

    Private Sub gridApproval_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles gridApproval.CustomCallback
        up_GridLoadApproval()
    End Sub
End Class