Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.IO
Imports OfficeOpenXml
Imports System.Drawing

Public Class TCCSResultInput
    Inherits System.Web.UI.Page
    Dim LastCycle As Integer
    Dim pUser As String = ""
    Public AuthApprove As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public ValueType As String
    Public SelUser As clsUserSetup
    Dim ApproveStatus1 As Integer
    Dim ApproveStatus2 As Integer
    Dim ApproveStatus3 As Integer
    Dim ApproveStatus4 As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sGlobal.getMenu("C030")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user") & ""
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "C030")
        AuthDelete = AuthUpdate
        btnSave.Enabled = AuthUpdate
        btnUpload.Enabled = AuthUpdate
        SelUser = clsUserSetupDB.GetData(pUser)
        If SelUser Is Nothing Then
            AuthApprove = False
            btnApprove.Enabled = False
        ElseIf SelUser.LineLeaderStatus = "1" Or SelUser.QELeaderStatus = "1" Or SelUser.ProdSectionHeadStatus = "1" Or SelUser.QESectionHeadStatus = "1" Then
            AuthApprove = True
            btnApprove.Enabled = True
        Else
            AuthApprove = False
            btnApprove.Enabled = False
        End If
        If Not IsPostBack And Not IsCallback Then
            dtDate.Value = Now.Date
        End If
        show_error(MsgTypeEnum.Info, "", 0)
        up_FillCboMachine(pUser)
        up_FillCboOldPartID()
        If Not IsPostBack And Not IsCallback Then
            Dim ResultDate As String
            Dim MachineNo As String
            Dim LineID As String
            Dim SubLineID As String
            Dim PartID As String

            If Request.QueryString("Date") Is Nothing Then
                If Session("tccsDate") & "" <> "" And Session("tccsMachineNo") & "" <> "" And Session("tccsLineID") & "" <> "" And Session("tccsPartID") & "" <> "" Then
                    ResultDate = Session("tccsDate")
                    MachineNo = Session("tccsMachineNo")
                    LineID = Session("tccsLineID")
                    SubLineID = Session("tccsSubLineID")
                    PartID = Session("tccsPartID")
                Else
                    Exit Sub
                End If
            Else
                ResultDate = Request.QueryString("Date") & ""
                MachineNo = Trim(Request.QueryString("MachineNo") & "")
                LineID = Trim(Request.QueryString("LineID") & "")
                SubLineID = Trim(Request.QueryString("SubLineID") & "")
                PartID = Trim(Request.QueryString("PartID") & "")
            End If
            dtDate.Value = CDate(ResultDate)
            cboMachine.Value = MachineNo

            Dim dt As DataTable
            dt = clsTCCSResultDB.GetPart(MachineNo)
            cboPartID.DataSource = dt
            cboPartID.DataBind()

            cboPartID.Value = PartID
            txtLineID.Text = LineID
            txtSubLine.Text = SubLineID

            txtpartname.Text = cboPartID.SelectedItem.GetValue("PartName")

            Dim ds As DataSet = clsTCCSResultDB.GetTable(ResultDate, MachineNo, LineID, SubLineID, PartID)
            Dim dt2 As DataTable = ds.Tables(0)
            If dt2.Rows.Count > 0 Then
                txtID.Text = dt2.Rows(0)("TCCSResultID") & ""
                txtpartname.Text = dt2.Rows(0)("PartName") & ""
                cboOldPartID.Value = dt2.Rows(0)("OldPartID") & ""
                txtOldPartName.Text = dt2.Rows(0)("OldPartName") & ""
                txtLotNo.Text = dt2.Rows(0)("LotNo") & ""
                txtNotes.Text = dt2.Rows(0)("Remark") & ""
                If Not IsDBNull(dt2.Rows(0)("LastUpdate")) Then
                    txtLastUpdate.Text = Format(dt2.Rows(0)("LastUpdate"), "dd MMM yyyy")
                End If
                txtUserUpdate.Text = dt2.Rows(0)("UpdateUser") & ""
                txtRevNo.Text = dt2.Rows(0)("RevNo") & ""
                If Not IsDBNull(dt2.Rows(0)("RevDate")) Then
                    txtRevDate.Text = Format(dt2.Rows(0)("RevDate"), "dd MMM yyyy")
                End If

                cbkAttach.JSProperties("cpLoadData") = "1"
                cbkAttach.JSProperties("cpOldPartID") = dt2.Rows(0)("OldPartID") & ""
                cbkAttach.JSProperties("cpOldPartName") = dt2.Rows(0)("OldPartName") & ""
                cbkAttach.JSProperties("cpLotNo") = dt2.Rows(0)("LotNo") & ""
                cbkAttach.JSProperties("cpRemark") = dt2.Rows(0)("Remark") & ""
                cbkAttach.JSProperties("cpTCCSResultID") = dt2.Rows(0)("TCCSResultID") & ""
                If Not IsDBNull(dt2.Rows(0)("LastUpdate")) Then
                    cbkAttach.JSProperties("cpLastUpdate") = Format(dt2.Rows(0)("LastUpdate"), "dd MMM yyyy")
                End If
                cbkAttach.JSProperties("cpUserUpdate") = dt2.Rows(0)("UpdateUser") & ""
                cbkAttach.JSProperties("cpRevNo") = dt2.Rows(0)("RevNo") & ""
                If Not IsDBNull(dt2.Rows(0)("RevDate")) Then
                    cbkAttach.JSProperties("cpRevDate") = Format(dt2.Rows(0)("RevDate"), "dd MMM yyyy")
                End If

                cbkAttach.JSProperties("cpEnableSave") = ""
                cbkAttach.JSProperties("cpEnableDelete") = ""
                cbkAttach.JSProperties("cpEnableApprove") = ""
                cbkAttach.JSProperties("cpEnableApproveNG") = ""
                Dim R As clsTCCSResult = clsTCCSResultDB.GetData(ResultDate, MachineNo, PartID, LineID, SubLineID)
                If R IsNot Nothing Then
                    cbkAttach.JSProperties("cpDate1") = Format(R.ApprovalDate1, "dd MMM yyyy")
                    cbkAttach.JSProperties("cpDate2") = Format(R.ApprovalDate2, "dd MMM yyyy")
                    cbkAttach.JSProperties("cpDate3") = Format(R.ApprovalDate3, "dd MMM yyyy")
                    cbkAttach.JSProperties("cpDate4") = Format(R.ApprovalDate4, "dd MMM yyyy")

                    cbkAttach.JSProperties("cpPIC1") = R.ApprovalPIC1
                    cbkAttach.JSProperties("cpPIC2") = R.ApprovalPIC2
                    cbkAttach.JSProperties("cpPIC3") = R.ApprovalPIC3
                    cbkAttach.JSProperties("cpPIC4") = R.ApprovalPIC4

                    cbkAttach.JSProperties("cpJudge1") = R.ApprovalJudgement1
                    cbkAttach.JSProperties("cpJudge2") = R.ApprovalJudgement2
                    cbkAttach.JSProperties("cpJudge3") = R.ApprovalJudgement3
                    cbkAttach.JSProperties("cpJudge4") = R.ApprovalJudgement4

                    If AuthUpdate = True Then
                        If R.ApprovalStatus1 = 0 And R.ApprovalStatus2 = 0 And R.ApprovalStatus3 = 0 And R.ApprovalStatus4 = 0 Then
                            cbkAttach.JSProperties("cpEnableSave") = "1"
                            cbkAttach.JSProperties("cpEnableDelete") = "1"
                        End If
                        If SelUser.QELeaderStatus = "1" And R.ApprovalStatus1 = 0 Then
                            cbkAttach.JSProperties("cpEnableApprove") = "1"
                            cbkAttach.JSProperties("cpEnableApproveNG") = "1"
                        ElseIf SelUser.LineLeaderStatus = "1" And R.ApprovalStatus2 = 0 Then
                            cbkAttach.JSProperties("cpEnableApprove") = "1"
                            cbkAttach.JSProperties("cpEnableApproveNG") = "1"
                        ElseIf SelUser.ProdSectionHeadStatus = "1" And R.ApprovalStatus3 = 0 And R.ApprovalStatus2 = 1 And R.Judgement = "NG" Then
                            cbkAttach.JSProperties("cpEnableApprove") = "1"
                            cbkAttach.JSProperties("cpEnableApproveNG") = "1"
                        ElseIf SelUser.QESectionHeadStatus = "1" And R.ApprovalStatus4 = 0 And R.ApprovalStatus3 = 1 And R.Judgement = "NG" Then
                            cbkAttach.JSProperties("cpEnableApprove") = "1"
                            cbkAttach.JSProperties("cpEnableApproveNG") = "1"
                        End If
                        ApproveStatus1 = R.ApprovalStatus1
                        ApproveStatus2 = R.ApprovalStatus2
                        ApproveStatus3 = R.ApprovalStatus3
                        ApproveStatus4 = R.ApprovalStatus4
                    End If
                End If
            End If
            GridLoad(ResultDate, MachineNo, LineID, SubLineID, PartID)
        End If        
    End Sub

    Private Sub up_FillCboMachine(UserID As String)
        Dim dt As DataTable = clsTCCSResultDB.GetMachine(UserID)
        cboMachine.DataSource = dt
        cboMachine.DataBind()
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        grid.JSProperties("cp_message") = ErrMsg
        grid.JSProperties("cp_type") = msgType
        grid.JSProperties("cp_val") = pVal
    End Sub

    Private Sub show_attach(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        cbkAttach.JSProperties("cp_message") = ErrMsg
        cbkAttach.JSProperties("cp_type") = msgType
        cbkAttach.JSProperties("cp_val") = pVal
    End Sub

    Protected Sub grid_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles grid.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            GridLoad(dtDate.Value, cboMachine.Value, txtLineID.Text, txtSubLine.Text, cboPartID.Value)
        End If
    End Sub

    Private Sub GridLoad(pDate As Date, pMachineNo As String, pLineID As String, pSubLineID As String, pPartID As String)
        Dim ds As DataSet = clsTCCSResultDB.GetTable(pDate, pMachineNo, pLineID, pSubLineID, pPartID)
        Dim dt As DataTable = ds.Tables(1)
        grid.DataSource = dt
        grid.DataBind()

        If pPartID = "" Then
            Session("tccsDate") = ""
        Else
            Session("tccsDate") = Format(pDate, "yyyy-MM-dd")
        End If
        Session("tccsMachineNo") = pMachineNo
        Session("tccsLineID") = pLineID
        Session("tccsSubLineID") = pSubLineID
        Session("tccsPartID") = pPartID

        If dt.Rows.Count = 0 Then
            cbkAttach.JSProperties("cpEnableDelete") = ""
            cbkAttach.JSProperties("cpEnableSave") = ""
            cbkAttach.JSProperties("cpEnableApprove") = ""
            cbkAttach.JSProperties("cpEnableApproveNG") = ""
            cbkAttach.JSProperties("cpLastUpdate") = ""
            cbkAttach.JSProperties("cpUserUpdate") = ""
        Else
            If ApproveStatus1 + ApproveStatus2 + ApproveStatus3 + ApproveStatus4 > 0 Then
                cbkAttach.JSProperties("cpEnableSave") = ""
            ElseIf AuthUpdate = True Then
                cbkAttach.JSProperties("cpEnableSave") = "1"
            Else
                cbkAttach.JSProperties("cpEnableSave") = ""
            End If
            Dim Rs As clsTCCSResult = clsTCCSResultDB.GetData(pDate, pMachineNo, pPartID, pLineID, pSubLineID)
            If Rs Is Nothing Then
                cbkAttach.JSProperties("cpLastUpdate") = ""
                cbkAttach.JSProperties("cpUserUpdate") = ""
            Else
                cbkAttach.JSProperties("cpLastUpdate") = Format(Rs.LastUpdate, "dd MMM yyyy")
                cbkAttach.JSProperties("cpUserUpdate") = Rs.CreateUser
            End If
            If AuthApprove = True Then
                If Rs Is Nothing Then
                    cbkAttach.JSProperties("cpEnableApprove") = ""
                    cbkAttach.JSProperties("cpEnableApproveNG") = ""
                Else
                    If SelUser.QELeaderStatus = "1" And Rs.ApprovalStatus1 = 0 Then
                        cbkAttach.JSProperties("cpEnableApprove") = "1"
                        cbkAttach.JSProperties("cpEnableApproveNG") = "1"
                    ElseIf SelUser.LineLeaderStatus = "1" And Rs.ApprovalStatus2 = 0 Then
                        cbkAttach.JSProperties("cpEnableApprove") = "1"
                        cbkAttach.JSProperties("cpEnableApproveNG") = "1"
                    ElseIf SelUser.ProdSectionHeadStatus = "1" And Rs.ApprovalStatus3 = 0 And Rs.ApprovalStatus2 = 1 And Rs.Judgement = "NG" Then
                        cbkAttach.JSProperties("cpEnableApprove") = "1"
                        cbkAttach.JSProperties("cpEnableApproveNG") = "1"
                    ElseIf SelUser.QESectionHeadStatus = "1" And Rs.ApprovalStatus4 = 0 And Rs.ApprovalStatus3 = 1 And Rs.Judgement = "NG" Then
                        cbkAttach.JSProperties("cpEnableApprove") = "1"
                        cbkAttach.JSProperties("cpEnableApproveNG") = "1"
                    End If
                End If
            Else
                cbkAttach.JSProperties("cpEnableApprove") = ""
                cbkAttach.JSProperties("cpEnableApproveNG") = ""
            End If
            If AuthDelete = True Then
                If Rs Is Nothing Or ApproveStatus1 = 1 Or ApproveStatus2 = 1 Or ApproveStatus3 = 1 Or ApproveStatus4 = 1 Then
                    cbkAttach.JSProperties("cpEnableDelete") = ""
                Else
                    cbkAttach.JSProperties("cpEnableDelete") = "1"
                End If
            Else
                cbkAttach.JSProperties("cpEnableDelete") = ""
            End If
        End If
    End Sub

    Private Sub GetHeader(R As clsTCCSResult)
        R.TCCSDate = Format(dtDate.Value, "yyyy-MM-dd")
        R.MachineNo = cboMachine.Value
        R.PartID = cboPartID.Value
        R.LineID = txtLineID.Text
        R.SubLineID = txtSubLine.Text
        R.OldPartID = cboOldPartID.Value
        R.LotNo = txtLotNo.Text
        R.Remark = txtNotes.Text
        R.CreateUser = Session("User") & ""
    End Sub

    Protected Sub grid_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs) Handles grid.BatchUpdate
        Try
            With grid
                Session("batcherror") = ""
                Dim n As Integer = e.UpdateValues.Count
                If n = 0 Then
                    Session("batcherror") = "Please input result!"
                    Return
                End If
                Dim R As New clsTCCSResult
                GetHeader(R)

                Dim Tr As SqlTransaction
                Using Cn As New SqlConnection(Sconn.Stringkoneksi)
                    Cn.Open()
                    Tr = Cn.BeginTransaction(IsolationLevel.ReadCommitted)
                    clsTCCSResultDB.Insert(R, Cn, Tr)
                    For i = 0 To n - 1
                        Dim Ri As New clsTCCSResultItem
                        Ri.TCCSResultID = R.TCCSResultID
                        Ri.ItemID = e.UpdateValues(i).NewValues("ItemID")
                        Ri.ValueType = UCase(e.UpdateValues(i).NewValues("ValueType") & "")
                        If Ri.ValueType = "N" Then
                            If IsDBNull(e.UpdateValues(i).NewValues("Result")) Or e.UpdateValues(i).NewValues("Result") Is Nothing Then
                                Ri.NumValue = Nothing
                            Else
                                Ri.NumValue = Val(e.UpdateValues(i).NewValues("Result"))
                            End If
                        Else
                            Ri.TextValue = e.UpdateValues(i).NewValues("Result") & ""
                        End If
                        Ri.Judgement = e.UpdateValues(i).NewValues("Judgement") & ""
                        Ri.Attachment = e.UpdateValues(i).NewValues("Attachment") & ""
                        clsTCCSResultItemDB.Insert(Ri, Cn, Tr)
                    Next
                    Tr.Commit()
                End Using
            End With
            e.Handled = True
        Catch ex As Exception
            Session("batcherror") = ex.Message
        End Try
    End Sub

    Protected Sub grid_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles grid.CustomCallback
        Dim pFunction As String = Split(e.Parameters, "|")(0)
        Select Case pFunction
            Case "load", "save", "delete"
                Dim ErrMsg As String = Session("batcherror") & ""
                Dim pDate As String = Split(e.Parameters, "|")(1)
                Dim pMachineNo As String = Split(e.Parameters, "|")(2)
                Dim pPartID As String = Split(e.Parameters, "|")(3)
                Dim pLineID As String = Split(e.Parameters, "|")(4)
                Dim pSubLineID As String = Split(e.Parameters, "|")(5)
                Dim ds As DataSet = clsTCCSResultDB.GetTable(pDate, pMachineNo, pLineID, pSubLineID, pPartID)
                Dim dt0 As DataTable
                Dim Judgement As String = ""
                If ds.Tables.Count > 0 Then
                    dt0 = ds.Tables(0)
                    If dt0.Rows.Count > 0 Then
                        Session("tccsDate") = pDate
                        Session("tccsMachineNo") = pMachineNo
                        Session("tccsLineID") = pLineID
                        Session("tccsSubLineID") = pSubLineID
                        Session("tccsPartID") = pPartID                        

                        grid.JSProperties("cpTCCSResultID") = dt0.Rows(0)("TCCSResultID") & ""
                        grid.JSProperties("cpLotNo") = RTrim(dt0.Rows(0)("LotNo") & "")
                        grid.JSProperties("cpOldPartID") = RTrim(dt0.Rows(0)("OldPartID") & "")
                        grid.JSProperties("cpOldPartName") = RTrim(dt0.Rows(0)("OldPartName") & "")
                        grid.JSProperties("cpRemark") = RTrim(dt0.Rows(0)("Remark") & "")
                        grid.JSProperties("cpRevNo") = RTrim(dt0.Rows(0)("RevNo") & "")
                        grid.JSProperties("cpRevDate") = Format(dt0.Rows(0)("RevDate"), "dd MMM yyyy")

                        grid.JSProperties("cpApproval1") = dt0.Rows(0)("ApprovalStatus1") & ""
                        grid.JSProperties("cpPIC1") = RTrim(dt0.Rows(0)("ApprovalPIC1") & "")
                        If IsDBNull(dt0.Rows(0)("ApprovalDate1")) Then
                            grid.JSProperties("cpDate1") = ""
                        Else
                            grid.JSProperties("cpDate1") = Format(dt0.Rows(0)("ApprovalDate1"), "dd MMM yyyy")
                        End If
                        grid.JSProperties("cpJudge1") = dt0.Rows(0)("ApprovalJudgement1") & ""

                        grid.JSProperties("cpApproval2") = dt0.Rows(0)("ApprovalStatus2") & ""
                        grid.JSProperties("cpPIC2") = RTrim(dt0.Rows(0)("ApprovalPIC2") & "")
                        If IsDBNull(dt0.Rows(0)("ApprovalDate2")) Then
                            grid.JSProperties("cpDate2") = ""
                        Else
                            grid.JSProperties("cpDate2") = Format(dt0.Rows(0)("ApprovalDate2"), "dd MMM yyyy")
                        End If
                        grid.JSProperties("cpJudge2") = dt0.Rows(0)("ApprovalJudgement2") & ""

                        grid.JSProperties("cpApproval3") = dt0.Rows(0)("ApprovalStatus3") & ""
                        grid.JSProperties("cpPIC3") = RTrim(dt0.Rows(0)("ApprovalPIC3") & "")
                        If IsDBNull(dt0.Rows(0)("ApprovalDate3")) Then
                            grid.JSProperties("cpDate3") = ""
                        Else
                            grid.JSProperties("cpDate3") = Format(dt0.Rows(0)("ApprovalDate3"), "dd MMM yyyy")
                        End If
                        grid.JSProperties("cpJudge3") = dt0.Rows(0)("ApprovalJudgement3") & ""

                        grid.JSProperties("cpApproval4") = dt0.Rows(0)("ApprovalStatus4") & ""
                        grid.JSProperties("cpPIC4") = RTrim(dt0.Rows(0)("ApprovalPIC4") & "")
                        If IsDBNull(dt0.Rows(0)("ApprovalDate4")) Then
                            grid.JSProperties("cpDate4") = ""
                        Else
                            grid.JSProperties("cpDate4") = Format(dt0.Rows(0)("ApprovalDate4"), "dd MMM yyyy")
                        End If
                        grid.JSProperties("cpJudge4") = dt0.Rows(0)("ApprovalJudgement4") & ""
                        Judgement = dt0.Rows(0)("Judgement") & ""
                        ApproveStatus1 = dt0.Rows(0)("ApprovalStatus1")
                        ApproveStatus2 = dt0.Rows(0)("ApprovalStatus2")
                        ApproveStatus3 = dt0.Rows(0)("ApprovalStatus3")
                        ApproveStatus4 = dt0.Rows(0)("ApprovalStatus4")
                    Else
                        grid.JSProperties("cpTCCSResultID") = ""
                        grid.JSProperties("cpLotNo") = ""
                        grid.JSProperties("cpOldPartID") = ""
                        grid.JSProperties("cpOldPartName") = ""
                        grid.JSProperties("cpRemark") = ""
                        grid.JSProperties("cpRevNo") = ""
                        grid.JSProperties("cpRevDate") = ""

                        grid.JSProperties("cpApproval1") = ""
                        grid.JSProperties("cpPIC1") = ""
                        grid.JSProperties("cpDate1") = ""
                        grid.JSProperties("cpJudge1") = ""

                        grid.JSProperties("cpApproval2") = ""
                        grid.JSProperties("cpPIC2") = ""
                        grid.JSProperties("cpDate2") = ""
                        grid.JSProperties("cpJudge2") = ""

                        grid.JSProperties("cpApproval3") = ""
                        grid.JSProperties("cpPIC3") = ""
                        grid.JSProperties("cpDate3") = ""
                        grid.JSProperties("cpJudge3") = ""

                        grid.JSProperties("cpApproval4") = ""
                        grid.JSProperties("cpPIC4") = ""
                        grid.JSProperties("cpDate4") = ""
                        grid.JSProperties("cpJudge4") = ""

                        grid.JSProperties("cpEnableApprove") = ""
                        grid.JSProperties("cpEnableSave") = ""
                        ApproveStatus1 = 0
                        ApproveStatus2 = 0
                        ApproveStatus3 = 0
                        ApproveStatus4 = 0
                        Judgement = ""
                    End If
                End If
                Dim dt As DataTable
                If ds.Tables.Count > 1 Then
                    dt = ds.Tables(1)
                    grid.DataSource = dt
                    grid.DataBind()
                    If dt.Rows.Count = 0 Then
                        grid.JSProperties("cpEnableDelete") = ""
                        grid.JSProperties("cpEnableSave") = ""
                        grid.JSProperties("cpEnableApprove") = ""
                        grid.JSProperties("cpEnableApproveNG") = ""
                        grid.JSProperties("cpLastUpdate") = ""
                        grid.JSProperties("cpUserUpdate") = ""
                    Else
                        If ApproveStatus1 + ApproveStatus2 + ApproveStatus3 + ApproveStatus4 > 0 Then
                            grid.JSProperties("cpEnableSave") = ""
                        ElseIf AuthUpdate = True Then
                            grid.JSProperties("cpEnableSave") = "1"
                        Else
                            grid.JSProperties("cpEnableSave") = ""
                        End If
                        Dim Rs As clsTCCSResult = clsTCCSResultDB.GetData(pDate, pMachineNo, pPartID, pLineID, pSubLineID)
                        If Rs Is Nothing Then
                            grid.JSProperties("cpLastUpdate") = ""
                            grid.JSProperties("cpUserUpdate") = ""
                        Else
                            grid.JSProperties("cpLastUpdate") = Format(Rs.LastUpdate, "dd MMM yyyy")
                            grid.JSProperties("cpUserUpdate") = Rs.CreateUser
                        End If
                        If AuthApprove = True Then
                            If Rs Is Nothing Then
                                grid.JSProperties("cpEnableApprove") = ""
                                grid.JSProperties("cpEnableApproveNG") = ""
                            Else
                                If SelUser.QELeaderStatus = "1" And Rs.ApprovalStatus1 = 0 Then
                                    grid.JSProperties("cpEnableApprove") = "1"
                                    grid.JSProperties("cpEnableApproveNG") = "1"
                                ElseIf SelUser.LineLeaderStatus = "1" And Rs.ApprovalStatus2 = 0 Then
                                    grid.JSProperties("cpEnableApprove") = "1"
                                    grid.JSProperties("cpEnableApproveNG") = "1"
                                ElseIf SelUser.ProdSectionHeadStatus = "1" And Rs.ApprovalStatus3 = 0 And Rs.ApprovalStatus2 = 1 And Rs.Judgement = "NG" Then
                                    grid.JSProperties("cpEnableApprove") = "1"
                                    grid.JSProperties("cpEnableApproveNG") = "1"
                                ElseIf SelUser.QESectionHeadStatus = "1" And Rs.ApprovalStatus4 = 0 And Rs.ApprovalStatus3 = 1 And Rs.Judgement = "NG" Then
                                    grid.JSProperties("cpEnableApprove") = "1"
                                    grid.JSProperties("cpEnableApproveNG") = "1"
                                End If
                            End If
                        Else
                            grid.JSProperties("cpEnableApprove") = ""
                            grid.JSProperties("cpEnableApproveNG") = ""
                        End If
                        If AuthDelete = True Then
                            If Rs Is Nothing Or ApproveStatus1 = 1 Or ApproveStatus2 = 1 Or ApproveStatus3 = 1 Or ApproveStatus4 = 1 Then
                                grid.JSProperties("cpEnableDelete") = ""
                            Else
                                grid.JSProperties("cpEnableDelete") = "1"
                            End If
                        Else
                            grid.JSProperties("cpEnableDelete") = ""
                        End If
                    End If
                End If
                If pFunction = "load" Then
                    grid.JSProperties("cpLoadData") = "1"
                ElseIf pFunction = "delete" Then
                    grid.JSProperties("cpLoadData") = "1"
                    show_error(MsgTypeEnum.Success, "Attachment deleted", 1)
                ElseIf pFunction = "save" Then
                    grid.JSProperties("cpLoadData") = "1"
                    If ErrMsg = "" Then
                        show_error(MsgTypeEnum.Success, "Save data successful", 1)
                    ElseIf ErrMsg = "Please input result!" Then
                        Dim R As clsTCCSResult = clsTCCSResultDB.GetData(pDate, pMachineNo, pPartID, pLineID, pSubLineID)
                        If R Is Nothing Then
                            show_error(MsgTypeEnum.Warning, ErrMsg, 1)
                            Return
                        End If
                        GetHeader(R)
                        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
                            Cn.Open()
                            Using Tr As SqlTransaction = Cn.BeginTransaction
                                Dim i As Integer = clsTCCSResultDB.Insert(R, Cn, Tr)
                                If i = 0 Then
                                    show_error(MsgTypeEnum.Warning, "No data changes!", 1)
                                Else
                                    grid.JSProperties("cpTCCSResultID") = R.TCCSResultID
                                    grid.JSProperties("cpLotNo") = R.LotNo
                                    grid.JSProperties("cpOldPartID") = R.OldPartID
                                    grid.JSProperties("cpOldPartName") = txtOldPartName.Text
                                    grid.JSProperties("cpRemark") = R.Remark

                                    Tr.Commit()
                                    show_error(MsgTypeEnum.Success, "Update data successful", 1)
                                End If
                            End Using
                        End Using
                    Else
                        show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
                    End If
                End If
        End Select
    End Sub

    Protected Sub grid_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles grid.RowInserting
        e.Cancel = True
        grid.CancelEdit()
    End Sub

    Protected Sub grid_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles grid.RowUpdating
        e.Cancel = True
    End Sub

    Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles grid.RowDeleting
        e.Cancel = True
        grid.CancelEdit()
    End Sub

    Private Sub cboPartID_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cboPartID.Callback
        Dim pMachineNo As String = Split(e.Parameter, "|")(0)
        Dim dt As DataTable
        dt = clsTCCSResultDB.GetPart(pMachineNo)
        cboPartID.DataSource = dt
        cboPartID.DataBind()
    End Sub

    Private Sub up_FillCboOldPartID()
        Dim PartList As List(Of ClsPart) = ClsPartDB.GetList
        cboOldPartID.DataSource = PartList
        cboOldPartID.DataBind()
    End Sub

    Private Sub cbkValid_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkValid.Callback
        Dim pDate As String = Split(e.Parameter, "|")(0)
        Dim pMachineNo As String = Split(e.Parameter, "|")(1)
        Dim pPartID As String = Split(e.Parameter, "|")(2)
        Dim pLotNo As String = Split(e.Parameter, "|")(3)
        Dim pRemark As String = Split(e.Parameter, "|")(4)
        cbkValid.JSProperties("cpValidationMsg") = ""
        Session("batcherror") = "Please input result!"
    End Sub

    Protected Sub AttachmentLink_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim link As DevExpress.Web.ASPxHyperLink = CType(sender, DevExpress.Web.ASPxHyperLink)
        If AuthUpdate = False Or ApproveStatus1 = 1 Or ApproveStatus2 = 1 Or ApproveStatus3 = 1 Or ApproveStatus4 = 1 Then
            link.Text = ""
            Return
        End If
        link.Text = "Attach"
        Dim templatecontainer As GridViewDataItemTemplateContainer = CType(link.NamingContainer, GridViewDataItemTemplateContainer)
        link.NavigateUrl = "javascript:void(0)"
        Dim ItemID As String = ""
        Dim i As Integer = templatecontainer.VisibleIndex
        If i >= 0 Then
            ItemID = templatecontainer.Grid.GetRowValues(i, "ItemID") & ""
            If ItemID <> "" Then
                link.ClientSideEvents.Click = "function (s,e) { " +
                        "for (var i = 0; i < grid.GetVisibleRowsOnPage(); i++) { " +
                        "   var result = true; " +
                        "   var valuetype = grid.batchEditApi.GetCellValue(i, 'ValueType'); " +
                        "   if (valuetype == 'N') {" +
                        "       if (grid.batchEditApi.GetCellValue(i, 'Result') == null || grid.batchEditApi.GetCellValue(i, 'Result').trim() == '') { " +
                        "           result = false; break; " +
                        "       } " +
                        "   } else if (valuetype = 'T') { " +
                        "       if (grid.batchEditApi.GetCellValue(i, 'Judgement') == null || grid.batchEditApi.GetCellValue(i, 'Judgement').trim() == '') { " +
                        "           result = false; break; " +
                        "       } " +
                        "   } " +
                        "} " +
                        "result = true; " +
                        "if (lblDate1.GetText() != '') { " +
                        "   " +
                        "} else if (result == false) { " +
                        "   toastr.warning('Please input all result first!', 'Warning');" +
                        "   toastr.options.closeButton = False;" +
                        "   toastr.options.debug = False;" +
                        "   toastr.options.newestOnTop = False;" +
                        "   toastr.options.progressBar = False;" +
                        "   toastr.options.preventDuplicates = True;" +
                        "   toastr.options.onclick = null;" +
                        "} else { " +
                        "   txtItemID.SetText('" + ItemID + "'); pcUpload.Show();" +
                        "}} "
            End If
        End If
    End Sub

    Protected Sub ViewLink_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim link As DevExpress.Web.ASPxHyperLink = CType(sender, DevExpress.Web.ASPxHyperLink)
        Dim templatecontainer As GridViewDataItemTemplateContainer = CType(link.NamingContainer, GridViewDataItemTemplateContainer)
        link.NavigateUrl = "javascript: Void(0)"
        If templatecontainer.VisibleIndex >= 0 Then
            Dim ItemID As String = templatecontainer.Grid.GetRowValues(templatecontainer.VisibleIndex, "ItemID") & ""
            If ItemID <> "" Then
                Dim TCCSResultID As String = templatecontainer.Grid.GetRowValues(templatecontainer.VisibleIndex, "TCCSResultID") & ""
                Dim Attachment As String = templatecontainer.Grid.GetRowValues(templatecontainer.VisibleIndex, "Attachment") & ""
                Dim LineID As String = txtLineID.Text.Trim
                Dim MachineNo As String = Trim(cboMachine.Value & "")
                Dim PartNo As String = Trim(cboPartID.Value & "")
                Dim TCCSDate As String = Format(dtDate.Value, "yyyy-MM-dd")
                If Attachment.ToLower.EndsWith(".pdf") Then
                    link.ClientSideEvents.Click = "function (s,e) { " +
                            "window.open('PreviewPDF.aspx?type=TCCS&id=" + TCCSResultID + "&itemid=" & ItemID + "&name=" + Attachment + "&machine=" + MachineNo + "&line=" + LineID + "&part=" + PartNo + "&date=" + TCCSDate +
                            "', 'ModalPopUp', " +
                            "'height=600,width=850,left=200,top=10'); }"
                Else
                    link.ClientSideEvents.Click = "function (s,e) {window.open('PreviewAttachment.aspx?type=TCCS&id=" +
                        TCCSResultID + "&itemid=" & ItemID + "&name=" + Attachment + "&machine=" + MachineNo + "&line=" + LineID + "&part=" + PartNo + "&date=" + TCCSDate +
                        "', 'ModalPopUp', 'height=600,width=850,left=200,top=10'); }"
                End If
            End If
        End If
    End Sub

    Protected Sub RemoveLink_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim link As DevExpress.Web.ASPxHyperLink = CType(sender, DevExpress.Web.ASPxHyperLink)
        If AuthUpdate = False Or ApproveStatus1 = 1 Or ApproveStatus2 = 1 Or ApproveStatus3 = 1 Or ApproveStatus4 = 1 Then
            link.Text = ""
            Return
        End If
        Dim templatecontainer As GridViewDataItemTemplateContainer = CType(link.NamingContainer, GridViewDataItemTemplateContainer)
        link.NavigateUrl = "javascript: Void(0)"
        If templatecontainer.VisibleIndex >= 0 Then
            Dim ItemID As String = templatecontainer.Grid.GetRowValues(templatecontainer.VisibleIndex, "ItemID") & ""
            If ItemID <> "" Then
                Dim TCCSResultID As String = templatecontainer.Grid.GetRowValues(templatecontainer.VisibleIndex, "TCCSResultID") & ""
                Dim Attachment As String = templatecontainer.Grid.GetRowValues(templatecontainer.VisibleIndex, "Attachment") & ""
                Dim LineID As String = txtLineID.Text.Trim
                Dim FileName As String = templatecontainer.Grid.GetRowValues(templatecontainer.VisibleIndex, "Attachment") & ""
                Dim MachineNo As String = Trim(cboMachine.Value & "")
                Dim PartNo As String = Trim(cboPartID.Value & "")
                Dim TCCSDate As Date = Format(dtDate.Value, "yyyy-MM-dd")
                link.ClientSideEvents.Click = "function (s,e) { " +
                    "if (lblDate1.GetText() == '') { " +
                    "   if(confirm('Are you sure you want to delete this attachment?')) { " +
                    "       cbkRemove.PerformCallback('" + TCCSResultID + "|" + ItemID + "|" + MachineNo + "|" + LineID + "|" + PartNo + "|" + FileName + "|" + TCCSDate + "'); " +
                    "   } " +
                    "} " +
                    "} "
            End If
        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim lotno As String = txtLastUpdate.Text
        Dim PostedFile As HttpPostedFile = uploader1.PostedFile
        Dim FileName As String = Path.GetFileName(PostedFile.FileName)
        Dim FileExtension As String = Path.GetExtension(FileName)
        Dim FileSize As Single = PostedFile.ContentLength / 1024 / 1024
        If FileSize = 0 Or FileName = "" Then
            show_attach(MsgTypeEnum.Warning, "No file to upload!", 1)
            Exit Sub
        End If
        Dim ds As DataSet = clsTCCSResultDB.GetTable(dtDate.Value, cboMachine.Value, txtLineID.Text, txtSubLine.Text, cboPartID.Value)
        Dim dt As DataTable = ds.Tables(1)
        Dim R As New clsTCCSResult
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Using Tr As SqlTransaction = Cn.BeginTransaction
                GetHeader(R)
                clsTCCSResultDB.Insert(R, Cn, Tr)
                Dim TCCSResultID As String = R.TCCSResultID
                Dim ItemID As String = Val(txtItemID.Text)
                Dim LineID As String = txtLineID.Text.Trim
                Dim MachineNo As String = cboMachine.Value.ToString.Trim
                Dim PartNo As String = cboPartID.Value.ToString.Trim
                Dim DestDir As String
                Dim TCCSPath As String = clsSetting.TCCSPath
                If TCCSPath = "" Then
                    DestDir = Server.MapPath("~/TCCS/" & Format(R.TCCSDate, "yyyyMMdd") & "/" & LineID & "/" & MachineNo & "/" & PartNo & "/" & TCCSResultID & "/" & ItemID)
                Else
                    DestDir = TCCSPath & "\TCCS\" & Format(R.TCCSDate, "yyyyMMdd") & "\" & LineID & "\" & MachineNo & "\" & PartNo & "\" & TCCSResultID & "\" & ItemID
                End If
                If Not Directory.Exists(DestDir) Then
                    Directory.CreateDirectory(DestDir)
                End If
                Dim Dest As String = DestDir & "\" & FileName
                uploader1.SaveAs(Dest)

                For i = 0 To dt.Rows.Count - 1
                    Dim Ri As New clsTCCSResultItem
                    With dt.Rows(i)
                        Ri.TCCSResultID = R.TCCSResultID
                        Ri.ItemID = .Item("ItemID")
                        Ri.ValueType = UCase(.Item("ValueType") & "")
                        If Not IsDBNull(.Item("Result")) Then
                            Ri.NullValue = False
                            If Ri.ValueType = "N" Then
                                Ri.NumValue = Val(.Item("Result"))
                            Else
                                Ri.TextValue = .Item("Result") & ""
                            End If
                        Else
                            Ri.NullValue = True
                        End If
                        Ri.Judgement = .Item("Judgement") & ""
                        If Ri.ItemID = ItemID Then
                            Ri.Attachment = FileName
                        Else
                            Ri.Attachment = .Item("Attachment") & ""
                        End If
                        clsTCCSResultItemDB.Insert(Ri, Cn, Tr)
                    End With
                Next
                clsTCCSResultItemDB.UpdateAttachment(R.TCCSResultID, ItemID, FileName, Cn, Tr)
                Tr.Commit()
            End Using
        End Using
        GridLoad(dtDate.Value, cboMachine.Value, txtLineID.Text, txtSubLine.Text, cboPartID.Value)

        cbkAttach.JSProperties("cpLoadData") = "1"
        cbkAttach.JSProperties("cpOldPartID") = Trim(cboOldPartID.Value & "")
        cbkAttach.JSProperties("cpOldPartName") = txtOldPartName.Text
        cbkAttach.JSProperties("cpLotNo") = txtLotNo.Text
        cbkAttach.JSProperties("cpRemark") = txtNotes.Text
        cbkAttach.JSProperties("cpEnableSave") = IIf(btnSave.ClientEnabled, "1", "")
        cbkAttach.JSProperties("cpEnableDelete") = IIf(btnDelete.ClientEnabled, "1", "")
        cbkAttach.JSProperties("cpEnableApprove") = IIf(btnApprove.ClientEnabled, "1", "")
        cbkAttach.JSProperties("cpEnableApproveNG") = IIf(btnApproveNG.ClientEnabled, "1", "")
        cbkAttach.JSProperties("cpTCCSResultID") = R.TCCSResultID
        cbkAttach.JSProperties("cpLastUpdate") = Format(Now.Date, "dd MMM yyyy")
        cbkAttach.JSProperties("cpUserUpdate") = Session("user") & ""
        cbkAttach.JSProperties("cpRevNo") = txtRevNo.Text
        cbkAttach.JSProperties("cpRevDate") = txtRevDate.Text
        show_attach(MsgTypeEnum.Success, "Upload successful", 1)
        btnSave.ClientEnabled = True
    End Sub

    Private Sub cbkApprove_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkApprove.Callback
        Dim pApprove As String = Split(e.Parameter, "|")(0)
        Dim pDate As String = Split(e.Parameter, "|")(1)
        Dim pMachineNo As String = Split(e.Parameter, "|")(2)
        Dim pPartID As String = Split(e.Parameter, "|")(3)
        Dim pLineID As String = Split(e.Parameter, "|")(4)
        Dim pSubLineID As String = Split(e.Parameter, "|")(5)
        Dim pUser As String = Session("User") & ""
        Dim Rs As clsTCCSResult = clsTCCSResultDB.GetData(pDate, pMachineNo, pPartID, pLineID, pSubLineID)
        Dim ApproveLevel As Integer
        If SelUser.QELeaderStatus = "1" And Rs.ApprovalStatus1 = 0 Then
            ApproveLevel = 1
        ElseIf SelUser.LineLeaderStatus = "1" And Rs.ApprovalStatus2 = 0 Then
            ApproveLevel = 2
        ElseIf SelUser.ProdSectionHeadStatus = "1" And Rs.ApprovalStatus2 = 1 And Rs.ApprovalStatus3 = 0 And Rs.Judgement = "NG" Then
            ApproveLevel = 3
        ElseIf SelUser.QESectionHeadStatus = "1" And Rs.ApprovalStatus3 = 1 And Rs.ApprovalStatus4 = 0 And Rs.Judgement = "NG" Then
            ApproveLevel = 4
        End If
        Rs.ApprovalStatus1 = "1"
        Rs.ApprovalPIC1 = pUser
        Rs.ApprovalJudgement1 = pApprove
        clsTCCSResultDB.Approve(ApproveLevel, Rs)
        cbkApprove.JSProperties("cpValidationMsg") = ""
    End Sub

    Private Sub cbkValidateApprove_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkValidateApprove.Callback
        Dim pDate As String = Split(e.Parameter, "|")(1)
        Dim pMachineNo As String = Split(e.Parameter, "|")(2)
        Dim pPartID As String = Split(e.Parameter, "|")(3)
        Dim pLineID As String = Split(e.Parameter, "|")(4)
        Dim pSubLineID As String = Split(e.Parameter, "|")(5)
        Dim Rs As clsTCCSResult = clsTCCSResultDB.GetData(pDate, pMachineNo, pPartID, pLineID, pSubLineID)
        If Rs Is Nothing Then
            cbkValidateApprove.JSProperties("cpValidationMsg") = "Data is not found!"
            Return
        End If
        Dim ApproveLevel As Integer
        If SelUser.QELeaderStatus = "1" And Rs.ApprovalStatus1 = 0 Then
            ApproveLevel = 1
        ElseIf SelUser.LineLeaderStatus = "1" And Rs.ApprovalStatus2 = 0 Then
            ApproveLevel = 2
        ElseIf SelUser.ProdSectionHeadStatus = "1" And Rs.ApprovalStatus2 = 1 And Rs.ApprovalStatus3 = 0 And Rs.Judgement = "NG" Then
            ApproveLevel = 3
        ElseIf SelUser.QESectionHeadStatus = "1" And Rs.ApprovalStatus3 = 1 And Rs.ApprovalStatus4 = 0 And Rs.Judgement = "NG" Then
            ApproveLevel = 4
        End If
        If ApproveLevel = 0 Then
            cbkValidateApprove.JSProperties("cpValidationMsg") = "You cannot approve this data!"
            Return
        End If
        cbkValidateApprove.JSProperties("cpApproveLevel") = ApproveLevel
        cbkValidateApprove.JSProperties("cpValidationMsg") = ""
    End Sub

    Private Sub cbkRefresh_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkRefresh.Callback
        Dim pDate As String = Split(e.Parameter, "|")(1)
        Dim pMachineNo As String = Split(e.Parameter, "|")(2)
        Dim pPartID As String = Split(e.Parameter, "|")(3)
        Dim pLineID As String = Split(e.Parameter, "|")(4)
        Dim pSubLineID As String = Split(e.Parameter, "|")(5)
        cbkRefresh.JSProperties("cpApproveMsg") = ""
        Dim Rs As clsTCCSResult = clsTCCSResultDB.GetData(pDate, pMachineNo, pPartID, pLineID, pSubLineID)
        If Rs Is Nothing Then
            cbkRefresh.JSProperties("cpApproval1") = ""
            cbkRefresh.JSProperties("cpPIC1") = ""
            cbkRefresh.JSProperties("cpDate1") = ""
            cbkRefresh.JSProperties("cpJudge1") = ""

            cbkRefresh.JSProperties("cpApproval2") = ""
            cbkRefresh.JSProperties("cpPIC2") = ""
            cbkRefresh.JSProperties("cpDate2") = ""
            cbkRefresh.JSProperties("cpJudge2") = ""

            cbkRefresh.JSProperties("cpApproval3") = ""
            cbkRefresh.JSProperties("cpPIC3") = ""
            cbkRefresh.JSProperties("cpDate3") = ""
            cbkRefresh.JSProperties("cpJudge3") = ""

            cbkRefresh.JSProperties("cpApproval4") = ""
            cbkRefresh.JSProperties("cpPIC4") = ""
            cbkRefresh.JSProperties("cpDate4") = ""
            cbkRefresh.JSProperties("cpJudge4") = ""

            cbkRefresh.JSProperties("cpEnableApprove") = ""
            cbkRefresh.JSProperties("cpEnableSave") = ""
            cbkRefresh.JSProperties("cpEnableDelete") = ""
        Else
            cbkRefresh.JSProperties("cpApproval1") = Rs.ApprovalStatus1
            cbkRefresh.JSProperties("cpPIC1") = Rs.ApprovalPIC1
            cbkRefresh.JSProperties("cpDate1") = Format(Rs.ApprovalDate1, "dd MMM yyyy")
            cbkRefresh.JSProperties("cpJudge1") = Rs.ApprovalJudgement1

            cbkRefresh.JSProperties("cpApproval2") = Rs.ApprovalStatus2
            cbkRefresh.JSProperties("cpPIC2") = Rs.ApprovalPIC2
            cbkRefresh.JSProperties("cpDate2") = Format(Rs.ApprovalDate2, "dd MMM yyyy")
            cbkRefresh.JSProperties("cpJudge2") = Rs.ApprovalJudgement2

            cbkRefresh.JSProperties("cpApproval3") = Rs.ApprovalStatus3
            cbkRefresh.JSProperties("cpPIC3") = Rs.ApprovalPIC3
            cbkRefresh.JSProperties("cpDate3") = Format(Rs.ApprovalDate3, "dd MMM yyyy")
            cbkRefresh.JSProperties("cpJudge3") = Rs.ApprovalJudgement3

            cbkRefresh.JSProperties("cpApproval4") = Rs.ApprovalStatus4
            cbkRefresh.JSProperties("cpPIC4") = Rs.ApprovalPIC4
            cbkRefresh.JSProperties("cpDate4") = Format(Rs.ApprovalDate4, "dd MMM yyyy")
            cbkRefresh.JSProperties("cpJudge4") = Rs.ApprovalJudgement4

            If SelUser.QELeaderStatus = "1" And Rs.ApprovalStatus1 = 0 Then
                cbkRefresh.JSProperties("cpEnableApprove") = "1"
                cbkRefresh.JSProperties("cpEnableApproveNG") = "1"
            ElseIf SelUser.LineLeaderStatus = "1" And Rs.ApprovalStatus2 = 0 Then
                cbkRefresh.JSProperties("cpEnableApprove") = "1"
                cbkRefresh.JSProperties("cpEnableApproveNG") = "1"
            ElseIf SelUser.ProdSectionHeadStatus = "1" And Rs.ApprovalStatus2 = 1 And Rs.ApprovalStatus3 = 0 And Rs.Judgement = "NG" Then
                cbkRefresh.JSProperties("cpEnableApprove") = "1"
                cbkRefresh.JSProperties("cpEnableApproveNG") = "1"
            ElseIf SelUser.QESectionHeadStatus = "1" And Rs.ApprovalStatus3 = 1 And Rs.ApprovalStatus4 = 0 And Rs.Judgement = "NG" Then
                cbkRefresh.JSProperties("cpEnableApprove") = "1"
                cbkRefresh.JSProperties("cpEnableApproveNG") = "1"
            Else
                cbkRefresh.JSProperties("cpEnableApprove") = ""
                cbkRefresh.JSProperties("cpEnableApproveNG") = ""
            End If
            cbkRefresh.JSProperties("cpEnableSave") = ""
        End If
        If btnSave.Enabled = False Then
            cbkRefresh.JSProperties("cpEnableSave") = ""
        End If
        If btnApprove.Enabled = False Then
            cbkRefresh.JSProperties("cpEnableApprove") = ""
        End If
        If AuthDelete = True Then
            If Rs Is Nothing Or Rs.ApprovalStatus1 = 1 Or Rs.ApprovalStatus2 = 1 Or Rs.ApprovalStatus3 = 1 Or Rs.ApprovalStatus4 = 1 Then
                cbkRefresh.JSProperties("cpEnableDelete") = ""
            Else
                cbkRefresh.JSProperties("cpEnableDelete") = "1"
            End If
        Else
            cbkRefresh.JSProperties("cpEnableDelete") = ""
        End If
    End Sub

    Private Sub cbkValidateNG_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkValidateNG.Callback
        Dim pDate As String = Split(e.Parameter, "|")(1)
        Dim pMachineNo As String = Split(e.Parameter, "|")(2)
        Dim pPartID As String = Split(e.Parameter, "|")(3)
        Dim pLineID As String = Split(e.Parameter, "|")(4)
        Dim pSubLineID As String = Split(e.Parameter, "|")(5)
        Dim Rs As clsTCCSResult = clsTCCSResultDB.GetData(pDate, pMachineNo, pPartID, pLineID, pSubLineID)
        If Rs Is Nothing Then
            cbkValidateNG.JSProperties("cpValidationMsg") = "Data is not found!"
            Return
        End If
        Dim ApproveLevel As Integer
        If SelUser.QELeaderStatus = "1" And Rs.ApprovalStatus1 = 0 Then
            ApproveLevel = 1
        ElseIf SelUser.LineLeaderStatus = "1" And Rs.ApprovalStatus1 = 1 And Rs.ApprovalStatus2 = 0 Then
            ApproveLevel = 2
        ElseIf SelUser.ProdSectionHeadStatus = "1" And Rs.ApprovalStatus2 = 1 And Rs.ApprovalStatus3 = 0 And Rs.Judgement = "NG" Then
            ApproveLevel = 3
        ElseIf SelUser.QESectionHeadStatus = "1" And Rs.ApprovalStatus3 = 1 And Rs.ApprovalStatus4 = 0 And Rs.Judgement = "NG" Then
            ApproveLevel = 4
        End If
        If ApproveLevel = 0 Then
            cbkValidateNG.JSProperties("cpValidationMsg") = "You cannot approve this data!"
            Return
        End If
        cbkValidateNG.JSProperties("cpApproveLevel") = ApproveLevel
        cbkValidateNG.JSProperties("cpValidationMsg") = ""
    End Sub

    Protected Sub cbkDelete_Callback(source As Object, e As CallbackEventArgs) Handles cbkDelete.Callback
        Dim pDate As String = Split(e.Parameter, "|")(0)
        Dim pMachineNo As String = Split(e.Parameter, "|")(1)
        Dim pPartID As String = Split(e.Parameter, "|")(2)
        Dim pLineID As String = Split(e.Parameter, "|")(3)
        Dim pSubLineID As String = Split(e.Parameter, "|")(4)
        Dim R As clsTCCSResult = clsTCCSResultDB.GetData(pDate, pMachineNo, pPartID, pLineID, pSubLineID)
        If R Is Nothing Then
            cbkDelete.JSProperties("cpValidationMsg") = "Data is not found!"
        Else
            Dim i As Integer = clsTCCSResultDB.Delete(R.TCCSResultID)
            cbkDelete.JSProperties("cpValidationMsg") = ""
        End If
    End Sub

    Protected Sub cbkRemove_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkRemove.Callback
        Dim TCCSResultID As String = Split(e.Parameter, "|")(0)
        Dim ItemID As String = Split(e.Parameter, "|")(1)
        Dim MachineNo As String = Split(e.Parameter, "|")(2)
        Dim LineID As String = Split(e.Parameter, "|")(3)
        Dim PartNo As String = Split(e.Parameter, "|")(4)
        Dim FileName As String = Split(e.Parameter, "|")(5)
        Dim TCCSDate As String = Split(e.Parameter, "|")(6)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Using Tr As SqlTransaction = Cn.BeginTransaction
                Dim i As Integer = clsTCCSResultItemDB.UpdateAttachment(TCCSResultID, ItemID, "", Cn, Tr)

                Dim FilePath As String
                Dim TCCSPath As String = clsSetting.TCCSPath
                If TCCSPath = "" Then
                    FilePath = Server.MapPath("~/TCCS/" & Format(CDate(TCCSDate), "yyyyMMdd") & "/" & LineID + "/" & MachineNo & "/" & PartNo & "/" & TCCSResultID & "/" & ItemID + "/" & FileName)
                Else
                    FilePath = TCCSPath & "\TCCS\" & Format(CDate(TCCSDate), "yyyyMMdd") & "\" & LineID & "\" & MachineNo & "\" & PartNo & "\" & TCCSResultID & "\" & ItemID + "\" & FileName
                End If                
                If File.Exists(FilePath) Then
                    File.Delete(FilePath)
                End If
                Tr.Commit()
            End Using
        End Using
        cbkRemove.JSProperties("cpValidationMsg") = ""
    End Sub

    Private Sub DownloadExcel()
        Dim ds As DataSet = clsTCCSResultDB.GetTable(Format(dtDate.Value, "yyyy-MM-dd"), cboMachine.Value, txtLineID.Text, txtSubLine.Text, cboPartID.Value)
        Dim dt As DataTable = ds.Tables(1)
        If dt.Rows.Count = 0 Then
            Return
        End If
        Dim dt2 As DataTable = ds.Tables(0)
        Using Pck As New ExcelPackage
            Dim ws As ExcelWorksheet = Pck.Workbook.Worksheets.Add("Sheet1")
            With ws
                .Cells(1, 1, 1, 1).Value = "No"
                .Cells(1, 2, 1, 2).Value = "Process"
                .Cells(1, 3, 1, 3).Value = "KPoint Status"
                .Cells(1, 4, 1, 4).Value = "PIC (OPR/QA)"
                .Cells(1, 5, 1, 5).Value = "Check Point"
                .Cells(1, 6, 1, 6).Value = "Tools"
                .Cells(1, 7, 1, 7).Value = "Value Type"
                .Cells(1, 8, 1, 8).Value = "Standard"
                .Cells(1, 9, 1, 9).Value = "Range"
                .Cells(1, 10, 1, 10).Value = "Result"
                .Cells(1, 11, 1, 11).Value = "Judgment"

                Dim n As Integer = dt.Rows.Count + 2
                .Cells(1, 1, 2, 11).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                .Cells(1, 1, 2, 11).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                .Cells(1, 1, 2, 11).Style.Font.Color.SetColor(System.Drawing.Color.White)

                .Cells(1, 1, 2, 11).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                .Cells(1, 1, 2, 11).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                .Cells(1, 1, 2, 11).Style.WrapText = True
                .Column(1).Width = 4
                .Column(2).Width = 16
                .Column(3).Width = 6
                .Column(4).Width = 10
                .Column(5).Width = 12
                .Column(6).Width = 20
                .Column(7).Width = 7
                .Column(8).Width = 20
                .Column(9).Width = 18
                .Column(11).Width = 6
                .Column(12).Width = 4

                For i = 1 To 11
                    .Cells(1, i, 2, i).Merge = True
                Next
                .Row(1).Height = 15
                For iRow = 0 To dt.Rows.Count - 1
                    .Cells(iRow + 3, 1).Value = dt.Rows(iRow)("SeqNo")
                    .Cells(iRow + 3, 2).Value = dt.Rows(iRow)("ProcessName")
                    .Cells(iRow + 3, 3).Value = dt.Rows(iRow)("KPointStatus")
                    .Cells(iRow + 3, 4).Value = dt.Rows(iRow)("PICType")
                    .Cells(iRow + 3, 5).Value = dt.Rows(iRow)("CheckPoint")
                    .Cells(iRow + 3, 6).Value = dt.Rows(iRow)("Tools")
                    .Cells(iRow + 3, 7).Value = dt.Rows(iRow)("ValueType")
                    .Cells(iRow + 3, 8).Value = dt.Rows(iRow)("Standard")
                    .Cells(iRow + 3, 9).Value = dt.Rows(iRow)("Range")
                    .Cells(iRow + 3, 10).Value = dt.Rows(iRow)("Result")
                    .Cells(iRow + 3, 11).Value = dt.Rows(iRow)("Judgement")
                    If dt.Rows(iRow)("Judgement") & "" = "NG" Then
                        .Cells(iRow + 3, 11).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(iRow + 3, 11).Style.Fill.BackgroundColor.SetColor(Color.Red)
                    End If
                Next                
                .InsertColumn(6, 2)
                .InsertColumn(9, 2)
                .InsertColumn(13, 1)
                .InsertColumn(15, 1)
                .Cells(1, 5, 2, 7).Merge = True
                .Cells(1, 8, 2, 10).Merge = True
                .Cells(1, 12, 2, 13).Merge = True
                .Cells(1, 14, 2, 15).Merge = True

                .Cells(1, 1, n, 17).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                .Cells(1, 1, n, 17).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                .Cells(1, 1, n, 17).Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                .Cells(1, 1, n, 1).Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                .Cells(n - 2, 7, n, 17).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left


                .InsertRow(1, 8)
                .Cells(3, 1).Value = "Date"
                .Cells(4, 1).Value = "Machine No."
                .Cells(5, 1).Value = "Line No."
                .Cells(6, 1).Value = "Sub Line No."
                .Cells(7, 1).Value = "Catatan Kelainan"
                .Cells(3, 3).Value = ": " & Format(dtDate.Value, "dd MMM yyyy")
                .Cells(4, 3).Value = ": " & cboMachine.Text
                .Cells(5, 3).Value = ": " & txtLineID.Text
                .Cells(6, 3).Value = ": " & txtSubLine.Text
                .Cells(7, 3).Value = ": " & txtNotes.Text

                .Cells(3, 6).Value = "Part No."
                .Cells(4, 6).Value = "Part Name"
                .Cells(5, 6).Value = "Old Part No."
                .Cells(6, 6).Value = "Old Part Name"
                .Cells(3, 7).Value = ": " & cboPartID.Value
                .Cells(4, 7).Value = ": " & txtpartname.Text
                .Cells(5, 7).Value = ": " & cboOldPartID.Value
                .Cells(6, 7).Value = ": " & txtOldPartName.Text

                .Cells(3, 9).Value = "Lot No."
                .Cells(4, 9).Value = "Last Update"
                .Cells(5, 9).Value = "User Update"
                .Cells(6, 9).Value = "Rev No/Date"
                .Cells(3, 11).Value = ": " & txtLotNo.Text
                .Cells(4, 11).Value = ": " & txtLastUpdate.Text
                .Cells(5, 11).Value = ": " & txtUserUpdate.Text
                .Cells(6, 11).Value = ": " & txtRevNo.Text & " / " & txtRevDate.Text

                .Cells(3, 13).Value = "Condition"
                .Cells(3, 14).Value = "Approval"
                .Cells(3, 15).Value = "Date"
                .Cells(3, 16).Value = "PIC"
                .Cells(3, 17).Value = "Judgement"

                .Cells(4, 15, 7, 15).Style.Numberformat.Format = "dd MMM yyyy"
                .Cells(3, 13, 7, 17).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                .Cells(3, 13, 7, 17).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                .Cells(3, 13, 7, 17).Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                .Cells(3, 13, 7, 17).Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                
                .Cells(3, 13, 3, 17).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                .Cells(3, 13, 3, 17).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                .Cells(3, 13, 3, 17).Style.Font.Color.SetColor(System.Drawing.Color.White)

                .Cells(4, 13).Value = "Normal"
                .Cells(4, 14).Value = "QE Leader"
                .Cells(4, 15).Value = dt2.Rows(0)("ApprovalDate1")
                .Cells(4, 16).Value = dt2.Rows(0)("ApprovalPIC1")
                .Cells(4, 17).Value = dt2.Rows(0)("ApprovalJudgement1")

                .Cells(5, 13).Value = "Normal"
                .Cells(5, 14).Value = "Line Leader"
                .Cells(5, 15).Value = dt2.Rows(0)("ApprovalDate2")
                .Cells(5, 16).Value = dt2.Rows(0)("ApprovalPIC2")
                .Cells(5, 17).Value = dt2.Rows(0)("ApprovalJudgement2")

                .Cells(6, 13).Value = "Abnormal"
                .Cells(6, 14).Value = "Prod Sec. Head"
                .Cells(6, 15).Value = dt2.Rows(0)("ApprovalDate3")
                .Cells(6, 16).Value = dt2.Rows(0)("ApprovalPIC3")
                .Cells(6, 17).Value = dt2.Rows(0)("ApprovalJudgement3")

                .Cells(7, 13).Value = "Abnormal"
                .Cells(7, 14).Value = "QE Sec. Head"
                .Cells(7, 15).Value = dt2.Rows(0)("ApprovalDate4")
                .Cells(7, 16).Value = dt2.Rows(0)("ApprovalPIC4")
                .Cells(7, 17).Value = dt2.Rows(0)("ApprovalJudgement4")

                .Cells(4, 13, 5, 13).Merge = True
                .Cells(6, 13, 7, 13).Merge = True
                .Cells(4, 13, 7, 13).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center

                .Cells(1, 1).Style.Font.Bold = True
                .Cells(1, 1).Style.Font.Size = 16
                .Cells(1, 1).Value = "CICS Result Input"
                .Cells(1, 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                .Cells(1, 1, 1, 17).Merge = True

                .Column(5).Width = 11
                .Column(6).Width = 15
                .Column(7).Width = 5
                .Column(9).Width = 13
                .Column(13).Width = 10
                .Column(14).Width = 15
                .Column(15).Width = 12
                .Column(16).Width = 15
                .DeleteColumn(10)

                .Cells(11, 5, n + 10, 6).Style.Border.Right.Style = Style.ExcelBorderStyle.None
                .Cells(11, 8, n + 10, 8).Style.Border.Right.Style = Style.ExcelBorderStyle.None
                .Cells(11, 11, n + 10, 11).Style.Border.Right.Style = Style.ExcelBorderStyle.None
                .Cells(11, 13, n + 10, 13).Style.Border.Right.Style = Style.ExcelBorderStyle.None

                Dim FinalRow As Integer = .Dimension.End.Row
                Dim ColumnString As String = "A2:S" & FinalRow
                .Cells(ColumnString).Style.Font.Name = "Segoe UI"
                .Cells(ColumnString).Style.Font.Size = 10
            End With

            Dim stream As MemoryStream = New MemoryStream(Pck.GetAsByteArray())
            Response.AppendHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            Response.AppendHeader("Content-Disposition", "attachment; filename=CICSResult_" & Format(Date.Now, "yyyy-MM-dd") & ".xlsx")
            Response.BinaryWrite(stream.ToArray())
            Response.End()

        End Using
    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        DownloadExcel()
    End Sub

    Protected Sub grid_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles grid.HtmlDataCellPrepared
        If (e.DataColumn.FieldName = "Judgement") Then
            If e.CellValue & "" = "NG" Then
                e.Cell.BackColor = Color.Red
            End If
        End If
    End Sub
End Class