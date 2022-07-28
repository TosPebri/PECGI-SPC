Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports OfficeOpenXml
Imports OfficeOpenXml.Drawing

Public Class QCSMaster
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
    Public ValueType As String
#End Region

#Region "Initialization"

    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            up_GridLoadMenu(cbolineid.Value, cbopartid.Value, cborevno.Text)
            If Request.QueryString("LineID") Is Nothing Then
                Exit Sub
            Else
                cbolineid.Value = Request.QueryString("LineID").ToString()
                cbopartid.Value = Request.QueryString("PartID").ToString()
                txtpartname.Value = Request.QueryString("PartName").ToString()
                cborevno.Value = Request.QueryString("RevNo").ToString()
                up_GridLoadMenu(cbolineid.Value, cbopartid.Value, cborevno.Value)
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "B010")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If
        sGlobal.getMenu("B010")
        Master.SiteTitle = sGlobal.menuName

        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B010")
        show_error(MsgTypeEnum.Info, "", 0)
        Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
        If AuthUpdate = False Then
            commandColumn.Visible = False
            btnCopy.ClientEnabled = False
            btnInput.ClientEnabled = False
            btnNew.ClientEnabled = False
            btnSave.ClientEnabled = False
            btnApprove.ClientEnabled = False
            btnExcel.ClientEnabled = False
            btnDelete.ClientEnabled = False
            btnCancel.ClientEnabled = False
        End If
        'If ClsQCSMasterDB.GetDataQE(pUser, "") = True Then
        '    up_fillcomboline(1, pUser)
        'Else
        '    up_fillcomboline(0, pUser)
        'End If

        'up_fillcombolinecopy(1, pUser)
        up_fillpart()
        up_fillpartcopy()

        btnInput.ClientVisible = False
    End Sub
#End Region

#Region "Procedure"
    Private Sub up_fillpart()
        Dim dsMenu As DataTable
        dsMenu = ClsQCSMasterDB.GetDataPart("", "")
        cbopartid.DataSource = dsMenu
        cbopartid.DataBind()
    End Sub

    Private Sub up_fillpartcopy()
        Dim ErrMsg As String = ""
        Dim dsline As DataTable
        dsline = ClsQCSMasterDB.GetDataPartCopy(ErrMsg)
        If ErrMsg = "" Then
            cbopartidpopup.DataSource = dsline
            cbopartidpopup.DataBind()

            'cbolineidpopup.DataSource = dsline
            'cbolineidpopup.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub up_fillcomboline(ByVal pStatus As String, ByVal pUserID As String)
        Dim ErrMsg As String = ""
        Dim dsline As DataTable
        dsline = ClsQCSMasterDB.GetDataLine(pStatus, pUserID, ErrMsg)
        If ErrMsg = "" Then
            cbolineid.DataSource = dsline
            cbolineid.DataBind()

            'cbolineidpopup.DataSource = dsline
            'cbolineidpopup.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub up_fillcombolinecopy(ByVal pStatus As String, ByVal pUserID As String)
        Dim ErrMsg As String = ""
        Dim dsline As DataTable
        dsline = ClsQCSMasterDB.GetDataLine(pStatus, pUserID, ErrMsg)
        If ErrMsg = "" Then
            cbolineidpopup.DataSource = dsline
            cbolineidpopup.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub up_GridLoadMenu(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String)
        Dim ErrMsg As String = ""
        Dim Menu As DataSet
        Menu = ClsQCSMasterDetailDB.GetList(pLineID, pPartID, pRevNo, ErrMsg)
        If ErrMsg = "" Then
            GridMenu.DataSource = Menu
            GridMenu.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        GridMenu.JSProperties("cp_message") = ErrMsg
        GridMenu.JSProperties("cp_type") = msgType
        GridMenu.JSProperties("cp_val") = pVal
    End Sub

    Private Sub upload_message(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        ASPxCallback1.JSProperties("cp_message") = ErrMsg
        ASPxCallback1.JSProperties("cp_type") = msgType
        ASPxCallback1.JSProperties("cp_val") = pVal
    End Sub

    Private Sub Approval1()
        Dim pErr As String = ""
        Dim QCSApprove As New ClsQCSMaster
        QCSApprove.ApprovalPIC1 = pUser
        QCSApprove.LineID = cbolineid.Value
        QCSApprove.PartID = cbopartid.Value
        QCSApprove.RevNo = cborevno.Text
        ClsQCSMasterDB.Approve1(QCSApprove, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Approve successfully!", 1)
        End If
    End Sub

    Private Sub Approval2()
        Dim pErr As String = ""
        Dim QCSApprove As New ClsQCSMaster
        QCSApprove.ApprovalPIC2 = pUser
        QCSApprove.LineID = cbolineid.Value
        QCSApprove.PartID = cbopartid.Value
        QCSApprove.RevNo = cborevno.Text
        ClsQCSMasterDB.Approve2(QCSApprove, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Approve successfully!", 1)
        End If
    End Sub

    Private Sub Approval3()
        Dim pErr As String = ""
        Dim QCSApprove As New ClsQCSMaster
        QCSApprove.ApprovalPIC3 = pUser
        QCSApprove.LineID = cbolineid.Value
        QCSApprove.PartID = cbopartid.Value
        QCSApprove.RevNo = cborevno.Text
        ClsQCSMasterDB.Approve3(QCSApprove, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Approve successfully!", 1)
        End If
    End Sub

    Private Sub Unapprove1()
        Dim pErr As String = ""
        Dim QCSApprove As New ClsQCSMaster
        QCSApprove.LineID = cbolineid.Value
        QCSApprove.PartID = cbopartid.Value
        QCSApprove.RevNo = cborevno.Text
        ClsQCSMasterDB.Unapprove1(QCSApprove, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Unapprove successfully!", 1)
        End If
    End Sub

    Private Sub Unapprove2()
        Dim pErr As String = ""
        Dim QCSApprove As New ClsQCSMaster
        QCSApprove.LineID = cbolineid.Value
        QCSApprove.PartID = cbopartid.Value
        QCSApprove.RevNo = cborevno.Text
        ClsQCSMasterDB.Unapprove2(QCSApprove, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Unapprove successfully!", 1)
        End If
    End Sub

    Private Sub Unapprove3()
        Dim pErr As String = ""
        Dim QCSApprove As New ClsQCSMaster
        QCSApprove.PartID = cbopartid.Value
        QCSApprove.RevNo = cborevno.Text
        ClsQCSMasterDB.Unapprove1(QCSApprove, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Unapprove successfully!", 1)
        End If
    End Sub

    Private Sub UpdateStatus()
        Dim pErr As String = ""
        Dim QCSApprove As New ClsQCSMaster
        QCSApprove.ApprovalPIC3 = pUser
        QCSApprove.LineID = cbolineid.Value
        QCSApprove.PartID = cbopartid.Value
        QCSApprove.RevNo = cborevno.Text
        ClsQCSMasterDB.Approve3(QCSApprove, pErr)

        Dim QCSActive As New ClsQCSMaster
        QCSActive.LineID = cbolineid.Value
        QCSActive.PartID = cbopartid.Value
        QCSActive.RevNo = cborevno.Value
        ClsQCSMasterDB.UpdStatus(QCSActive, pErr)

        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        End If
    End Sub

    Private Sub LoadApprovalInformation()
        Dim ErrMsg As String = ""
        Dim MenuName As DataSet
        MenuName = ClsQCSMasterDB.GetApprovalName(cbolineid.Value, cbopartid.Value, cborevno.Value, ErrMsg)
        Dim Menu As DataSet
        Menu = ClsQCSMasterDB.ExistQCS(cbolineid.Value, cbopartid.Value, cborevno.Value, ErrMsg)
        If ErrMsg = "" Then
            If Menu.Tables(0).Rows.Count = 0 Then
            Else
                GridMenu.JSProperties("cp_fillapprovalinformation") = 1
                GridMenu.JSProperties("cp_activestatus") = Menu.Tables(0).Rows(0)("ActiveStatus").ToString
                GridMenu.JSProperties("cp_approvalstatus1") = Menu.Tables(0).Rows(0)("ApprovalStatus1").ToString
                GridMenu.JSProperties("cp_approvalstatus2") = Menu.Tables(0).Rows(0)("ApprovalStatus2").ToString
                GridMenu.JSProperties("cp_approvalstatus3") = Menu.Tables(0).Rows(0)("ApprovalStatus3").ToString

                If Menu.Tables(0).Rows(0)("ApprovalStatus1") = 0 Then
                    GridMenu.JSProperties("cp_approvalpic1") = "UNAPPROVED"
                    GridMenu.JSProperties("cp_approvaldate1") = "UNAPPROVED"
                Else
                    'GridMenu.JSProperties("cp_approvalpic1") = Menu.Tables(0).Rows(0)("ApprovalPIC1").ToString
                    If MenuName.Tables(0).Rows(0)("FullName").ToString = "" Then
                        GridMenu.JSProperties("cp_approvalpic1") = MenuName.Tables(0).Rows(0)("ApprovalPIC").ToString
                    Else
                        GridMenu.JSProperties("cp_approvalpic1") = MenuName.Tables(0).Rows(0)("FullName").ToString
                    End If

                    GridMenu.JSProperties("cp_approvaldate1") = Format(Menu.Tables(0).Rows(0)("ApprovalDate1"), "dd MMM yyyy")
                End If

                If Menu.Tables(0).Rows(0)("ApprovalStatus2") = 0 Then
                    GridMenu.JSProperties("cp_approvalpic2") = "UNAPPROVED"
                    GridMenu.JSProperties("cp_approvaldate2") = "UNAPPROVED"
                Else
                    'GridMenu.JSProperties("cp_approvalpic2") = Menu.Tables(0).Rows(0)("ApprovalPIC2").ToString
                    If MenuName.Tables(0).Rows(1)("FullName").ToString = "" Then
                        GridMenu.JSProperties("cp_approvalpic2") = MenuName.Tables(0).Rows(1)("ApprovalPIC").ToString
                    Else
                        GridMenu.JSProperties("cp_approvalpic2") = MenuName.Tables(0).Rows(1)("FullName").ToString
                    End If

                    GridMenu.JSProperties("cp_approvaldate2") = Format(Menu.Tables(0).Rows(0)("ApprovalDate2"), "dd MMM yyyy")
                End If

                If Menu.Tables(0).Rows(0)("ApprovalStatus3") = 0 Then
                    GridMenu.JSProperties("cp_approvalpic3") = "UNAPPROVED"
                    GridMenu.JSProperties("cp_approvaldate3") = "UNAPPROVED"
                Else
                    'GridMenu.JSProperties("cp_approvalpic3") = Menu.Tables(0).Rows(0)("ApprovalPIC3").ToString
                    If MenuName.Tables(0).Rows(2)("FullName").ToString = "" Then
                        GridMenu.JSProperties("cp_approvalpic3") = MenuName.Tables(0).Rows(2)("ApprovalPIC").ToString
                    Else
                        GridMenu.JSProperties("cp_approvalpic3") = MenuName.Tables(0).Rows(2)("FullName").ToString
                    End If

                    GridMenu.JSProperties("cp_approvaldate3") = Format(Menu.Tables(0).Rows(0)("ApprovalDate3"), "dd MMM yyyy")
                End If
            End If
        End If
    End Sub

    Private Sub ApprovedTrue(ByVal pVal As Integer)
        GridMenu.JSProperties("cp_btnapprovedtrue") = pVal
        If AuthUpdate = True Then
            GridMenu.JSProperties("cp_btncopy") = False
            GridMenu.JSProperties("cp_btninput") = False
            GridMenu.JSProperties("cp_btnnew") = True
            GridMenu.JSProperties("cp_btndelete") = False
            GridMenu.JSProperties("cp_btncancel") = False
            GridMenu.JSProperties("cp_btnsave") = False
            Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
            commandColumn.Visible = False
        Else
            GridMenu.JSProperties("cp_btncopy") = False
            GridMenu.JSProperties("cp_btninput") = False
            GridMenu.JSProperties("cp_btnnew") = False
            GridMenu.JSProperties("cp_btndelete") = False
            GridMenu.JSProperties("cp_btncancel") = False
            GridMenu.JSProperties("cp_btnsave") = False
        End If
        GridMenu.JSProperties("cp_btnexcel") = True
    End Sub

    Private Sub ApprovedFalse(ByVal pVal As Integer)
        GridMenu.JSProperties("cp_btnapprovedfalse") = pVal
        If AuthUpdate = True Then
            GridMenu.JSProperties("cp_btncopy") = True
            GridMenu.JSProperties("cp_btninput") = False
            GridMenu.JSProperties("cp_btnnew") = True
            GridMenu.JSProperties("cp_btndelete") = True
            GridMenu.JSProperties("cp_btncancel") = False
            GridMenu.JSProperties("cp_btnsave") = True
            Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
            commandColumn.Visible = True
        Else
            GridMenu.JSProperties("cp_btncopy") = False
            GridMenu.JSProperties("cp_btninput") = False
            GridMenu.JSProperties("cp_btnnew") = False
            GridMenu.JSProperties("cp_btndelete") = False
            GridMenu.JSProperties("cp_btncancel") = False
            GridMenu.JSProperties("cp_btnsave") = False
        End If
        GridMenu.JSProperties("cp_btnexcel") = True
    End Sub

    Private Sub ButtonUnApproved()
        'btnCopy.ClientEnabled = True
        'btnInput.ClientEnabled = False
        'btnExcel.ClientEnabled = True
        'btnExcel.ClientEnabled = True
        'btnDelete.ClientEnabled = True
        'btnCancel.ClientEnabled = False
        'btnSave.ClientEnabled = True
        'Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
        'commandColumn.Visible = True
    End Sub

    Private Sub show_data(ByVal pCek As Integer, ByVal pRevDate As String, ByVal pRevHis As String, ByVal pPre As String, ByVal pStatus As String, ByVal pLineStatus As String, ByVal pForeStatus As String, ByVal pQeStatus As String, ByVal pLinePIC As String, ByVal pForePIC As String, ByVal pQePIC As String, ByVal pLineDate As String, ByVal pForDate As String, ByVal pQeDate As String)
        GridMenu.JSProperties("cp_cek") = pCek
        GridMenu.JSProperties("cp_revdate") = pRevDate
        GridMenu.JSProperties("cp_revhistory") = pRevHis
        GridMenu.JSProperties("cp_preparedby") = pPre
        'GridMenu.JSProperties("cp_approvedby") = pApr
        GridMenu.JSProperties("cp_Status") = pStatus
        GridMenu.JSProperties("cp_leaderstatus") = pLineStatus
        GridMenu.JSProperties("cp_foremanstatus") = pForeStatus
        GridMenu.JSProperties("cp_qestatus") = pQeStatus
        GridMenu.JSProperties("cp_leaderpic") = pLinePIC
        GridMenu.JSProperties("cp_foremanpic") = pForePIC
        GridMenu.JSProperties("cp_qepic") = pQePIC
        GridMenu.JSProperties("cp_leaderdate") = pLineDate
        GridMenu.JSProperties("cp_foremandate") = pForDate
        GridMenu.JSProperties("cp_qedate") = pQeDate
    End Sub

    Private Sub show_StatusApproval(ByVal pStatusApproval As String, ByVal pLineStatus As String, ByVal pForeStatus As String, ByVal pQeStatus As String, ByVal pLinePIC As String, ByVal pForePIC As String, ByVal pQePIC As String, ByVal pLineDate As String, ByVal pForDate As String, ByVal pQeDate As String)
        GridMenu.JSProperties("cp_statusapproval") = pStatusApproval
        GridMenu.JSProperties("cp_leaderstatus") = pLineStatus
        GridMenu.JSProperties("cp_foremanstatus") = pForeStatus
        GridMenu.JSProperties("cp_qestatus") = pQeStatus

        GridMenu.JSProperties("cp_leaderpic") = pLinePIC
        GridMenu.JSProperties("cp_foremanpic") = pForePIC
        GridMenu.JSProperties("cp_qepic") = pQePIC
        GridMenu.JSProperties("cp_leaderdate") = pLineDate
        GridMenu.JSProperties("cp_foremandate") = pForDate
        GridMenu.JSProperties("cp_qedate") = pQeDate
    End Sub

    Private Sub show_StatusApprovalFinal(ByVal pStatusApproval As String, ByVal pStatus As String, ByVal pLineStatus As String, ByVal pForeStatus As String, ByVal pQeStatus As String, ByVal pLinePIC As String, ByVal pForePIC As String, ByVal pQePIC As String, ByVal pLineDate As String, ByVal pForDate As String, ByVal pQeDate As String)
        GridMenu.JSProperties("cp_statusapproval") = pStatusApproval
        'GridMenu.JSProperties("cp_approvedby") = pApr
        GridMenu.JSProperties("cp_Status") = pStatus
        GridMenu.JSProperties("cp_leaderstatus") = pLineStatus
        GridMenu.JSProperties("cp_foremanstatus") = pForeStatus
        GridMenu.JSProperties("cp_qestatus") = pQeStatus

        GridMenu.JSProperties("cp_leaderpic") = pLinePIC
        GridMenu.JSProperties("cp_foremanpic") = pForePIC
        GridMenu.JSProperties("cp_qepic") = pQePIC
        GridMenu.JSProperties("cp_leaderdate") = pLineDate
        GridMenu.JSProperties("cp_foremandate") = pForDate
        GridMenu.JSProperties("cp_qedate") = pQeDate
    End Sub
#End Region

#Region "Control Event"
#Region "GridMenu"
    Protected Sub GridMenu_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles GridMenu.AfterPerformCallback
        Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            If cborevno.Text = "--New--" Then
                cborevno.Value = 0
            End If
            up_GridLoadMenu(cbolineid.Value, cbopartid.Value, cborevno.Value)
        End If

        'If Request.QueryString("LineID") Is Nothing Then
        'Else
        '    GridMenu.JSProperties("cp_parsing") = 1
        '    GridMenu.JSProperties("cp_lineid") = Request.QueryString("LineID").ToString()
        '    GridMenu.JSProperties("cp_partid") = Request.QueryString("PartID").ToString()
        '    GridMenu.JSProperties("cp_partname") = Request.QueryString("PartName").ToString()
        '    GridMenu.JSProperties("cp_revno") = Request.QueryString("RevNo").ToString()
        '    up_GridLoadMenu(cbopartid.Value, cborevno.Value)
        'End If
    End Sub

    Private Sub GridMenu_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridMenu.BeforeGetCallbackResult
        If GridMenu.IsNewRowEditing Then
            GridMenu.SettingsCommandButton.UpdateButton.Text = "Save"
            If cbopartid.Text = "" Then
                show_error(MsgTypeEnum.Warning, "Please Select Part No", 1)
                GridMenu.CancelEdit()
            ElseIf cbolineid.Text = "" Then
                show_error(MsgTypeEnum.Warning, "Please Select Line No", 1)
                GridMenu.CancelEdit()
            ElseIf cborevno.Text = "" Then
                show_error(MsgTypeEnum.Warning, "Please Select Rev No", 1)
                GridMenu.CancelEdit()
            ElseIf dtrevdate.Text = "" Then
                show_error(MsgTypeEnum.Warning, "Please Select Rev Date", 1)
                GridMenu.CancelEdit()
            ElseIf txtrevisionhistory.Text = "" Then
                show_error(MsgTypeEnum.Warning, "Please Fill Data Rev History", 1)
                GridMenu.CancelEdit()
            ElseIf txtpreparedby.Text = "" Then
                show_error(MsgTypeEnum.Warning, "Please Fill Data Prepared By", 1)
                GridMenu.CancelEdit()
            ElseIf cboattachment.Text = "" Then
                show_error(MsgTypeEnum.Warning, "Please Select Attachment", 1)
                GridMenu.CancelEdit()
            End If

            GridMenu.JSProperties("cp_valuetype") = 1
            GridMenu.JSProperties("cp_valuetypevalue") = ValueType
        ElseIf Not GridMenu.IsNewRowEditing Then
            GridMenu.JSProperties("cp_valuetypeedit") = 1
            GridMenu.JSProperties("cp_valuetypevalue") = ValueType
            'Dim NumRangeStart = TryCast(GridMenu.Columns(8), GridViewDataTextColumn)
            'Dim NumRangeEnd = TryCast(GridMenu.Columns(9), GridViewDataTextColumn)
            'Dim CharRange = TryCast(GridMenu.Columns(10), GridViewDataTextColumn)
            'If ValueType = "N" Then
            '    NumRangeStart.EditFormSettings.Visible = False
            '    NumRangeEnd.EditFormSettings.Visible = False
            '    CharRange.EditFormSettings.Visible = True
            '    GridMenu.JSProperties("cp_edit") = "1"
            '    GridMenu.JSProperties("cp_btntextapprove1") = "Approve"
            'ElseIf ValueType = "T" Then
            '    CharRange.EditFormSettings.Visible = False
            '    NumRangeStart.EditFormSettings.Visible = True
            '    NumRangeEnd.EditFormSettings.Visible = True
            'End If
            'GridMenu.JSProperties("cp_edit") = "1"
            'GridMenu.JSProperties("cp_value") = ValueType
        End If
    End Sub

    Protected Sub GridMenu_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles GridMenu.CellEditorInitialize
        If Not GridMenu.IsNewRowEditing Then

            If e.Column.FieldName = "ItemID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If
            If e.Column.FieldName = "ProcessName" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If


            If e.Column.FieldName = "KPointStatus" Then
                If TypeOf e.Editor Is ASPxComboBox Then
                    Dim combo As ASPxComboBox = DirectCast(e.Editor, ASPxComboBox)
                    If combo.Value = Nothing Then
                        combo.Value = "B"
                    End If
                End If
            End If

            If e.Column.FieldName = "ValueType" Then
                ValueType = e.Editor.Value
                GridMenu.JSProperties("cp_valuetype") = 1
                GridMenu.JSProperties("cp_valuetypevalue") = ValueType
            End If

            If e.Column.FieldName = "ProcessName" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If

            Dim NumRangeStart = TryCast(GridMenu.Columns(8), GridViewDataTextColumn)
            Dim NumRangeEnd = TryCast(GridMenu.Columns(9), GridViewDataTextColumn)
            Dim CharRange = TryCast(GridMenu.Columns(10), GridViewDataTextColumn)

            If e.Column.FieldName = "ProcessID" Then
                If TypeOf e.Editor Is ASPxComboBox Then
                    Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
                    combo.DataSource = ClsSubLineDB.GetDataProcess()
                    combo.TextField = "ProcessID"
                    combo.ValueField = "ProcessID"
                    combo.DataBindItems()
                End If
            End If

        Else
            If e.Column.FieldName = "ItemID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke

                Dim ErrMsg As String = ""
                Dim Menu As DataSet
                Menu = ClsQCSMasterDB.isExistItemID(cbolineid.Value, cbopartid.Value, IIf(cborevno.Text = "--New--", "", cborevno.Text), ErrMsg)
                If Menu.Tables(0).Rows(0)("NilMax").ToString() = "" Then
                    e.Editor.Value = 1
                Else
                    e.Editor.Value = Val(Menu.Tables(0).Rows(0)("NilMax").ToString()) + 1
                End If

                If ErrMsg = "" Then
                End If
            End If

            If e.Column.FieldName = "ProcessName" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If
            If e.Column.FieldName = "ValueType" Then
                'ValueType = GridMenu.GetFocusedRowCellValue
                GridMenu.JSProperties("cp_valuetypeedit") = 1
                GridMenu.JSProperties("cp_valuetypevalue") = ValueType
            End If
            'If e.Column.FieldName = "NumRangeStart" Then

            'End If

            If e.Column.FieldName = "ProcessID" Then
                If TypeOf e.Editor Is ASPxComboBox Then
                    Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
                    combo.DataSource = ClsSubLineDB.GetDataProcess()
                    combo.TextField = "ProcessID"
                    combo.ValueField = "ProcessID"
                    combo.DataBindItems()
                End If
            End If


        End If
    End Sub

    Private Sub GridMenu_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridMenu.CustomCallback

        Dim pFunction As String = Split(e.Parameters, "|")(0)

        Select Case pFunction
            Case "excel"
                Dim pPart As String = Split(e.Parameters, "|")(1)
                Dim pRev As String = Split(e.Parameters, "|")(2)
                Dim pLine As String = Split(e.Parameters, "|")(3)
                Dim pPartName As String = Split(e.Parameters, "|")(4)
                Dim pLeaderPIC As String = Split(e.Parameters, "|")(5)
                Dim pForemanPIC As String = Split(e.Parameters, "|")(6)
                Dim pQEPIC As String = Split(e.Parameters, "|")(7)
                Dim pLeaderDate As String = Split(e.Parameters, "|")(8)
                Dim pForemanDate As String = Split(e.Parameters, "|")(9)
                Dim pQEDate As String = Split(e.Parameters, "|")(10)
                up_Excel(pPart, pRev, pLine, pPartName, pLeaderPIC, pForemanPIC, pQEPIC, pLeaderDate, pForemanDate, pQEDate, "")

            Case "ClearGrid"
                up_GridLoadMenu("", "", "")
                GridMenu.JSProperties("cp_cleargrid") = 1

            Case "ClearGridPart"
                up_GridLoadMenu("", "", "")
                GridMenu.JSProperties("cp_cleargridpart") = 1

            Case "ClearAll"
                up_GridLoadMenu("", "", "")
                GridMenu.JSProperties("cp_clearall") = 1

            Case "SelectValueType"
                ValueType = Split(e.Parameters, "|")(1)
                If ValueType = "N" Then
                    Dim NumRangeStart = TryCast(GridMenu.Columns(8), GridViewDataTextColumn)
                    Dim NumRangeEnd = TryCast(GridMenu.Columns(9), GridViewDataTextColumn)
                    Dim CharRange = TryCast(GridMenu.Columns(10), GridViewDataTextColumn)

                    NumRangeStart.EditFormSettings.Visible = False
                    NumRangeEnd.EditFormSettings.Visible = False
                    NumRangeStart.ReadOnly = False
                    NumRangeEnd.ReadOnly = False

                    CharRange.EditFormSettings.Visible = True
                    CharRange.ReadOnly = True

                ElseIf ValueType = "T" Then
                    Dim NumRangeStart = TryCast(GridMenu.Columns(8), GridViewDataTextColumn)
                    Dim NumRangeEnd = TryCast(GridMenu.Columns(9), GridViewDataTextColumn)
                    Dim CharRange = TryCast(GridMenu.Columns(10), GridViewDataTextColumn)

                    CharRange.EditFormSettings.Visible = False
                    NumRangeStart.ReadOnly = False

                    NumRangeStart.EditFormSettings.Visible = True
                    NumRangeEnd.EditFormSettings.Visible = True

                    NumRangeStart.ReadOnly = True
                    NumRangeEnd.ReadOnly = True
                End If

            Case "Update"
                Dim pPartID As String = Split(e.Parameters, "|")(1)
                Dim pRevNo As String = Split(e.Parameters, "|")(2)
                Dim pErr As String = ""
                If ClsQCSMasterDB.isExistQCS(cbolineid.Value, pPartID, pRevNo, "") = True Then
                    Dim QCSUpdate As New ClsQCSMaster
                    QCSUpdate.LineID = cbolineid.Value
                    QCSUpdate.PartID = pPartID
                    QCSUpdate.RevNo = pRevNo
                    QCSUpdate.RevDate = dtrevdate.Text
                    QCSUpdate.RevHistory = txtrevisionhistory.Text
                    QCSUpdate.PreparedBy = txtpreparedby.Text
                    'QCSUpdate.SafetySymbol = txtsafety.Text
                    QCSUpdate.RequiredAttachmentStatus = cboattachment.Value
                    'QCSUpdate.ApprovedBy = txtapprovedby.Text
                    QCSUpdate.UpdateUser = pUser

                    ClsQCSMasterDB.Update(QCSUpdate, pErr)

                    If pErr <> "" Then
                        show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                    Else
                        show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
                    End If
                Else
                    show_error(MsgTypeEnum.Warning, "Update data unsuccessfully!", 1)
                End If

            Case "Copy"
                Dim pLineID As String = Split(e.Parameters, "|")(1)
                Dim pPartID As String = Split(e.Parameters, "|")(2)
                Dim pRevNo As String = Split(e.Parameters, "|")(3)

                Dim pErr As String = ""
                If cbolineidpopup.Value = "" Then
                    show_error(MsgTypeEnum.Warning, "Please Fill Data Line No!", 1)
                    Exit Sub
                ElseIf cbopartid.Text = "" Then
                    show_error(MsgTypeEnum.Warning, "Please Fill Data Part No!", 1)
                    Exit Sub
                ElseIf cborevnopopup.Text = "" Then
                    show_error(MsgTypeEnum.Warning, "Please Fill Data Rev No!", 1)
                    Exit Sub
                End If

                If ClsQCSMasterDB.isExistQCS(cbolineid.Value, cbopartid.Value, cborevno.Value) = False Then
                    Dim Menu As DataSet
                    Dim MaxRevNo As Integer
                    Menu = ClsQCSMasterDB.isExistRevNo(cbolineid.Value, cbopartid.Value, "")
                    If Menu.Tables(0).Rows(0)("NilMax").ToString() = "" Then
                        MaxRevNo = 1
                    Else
                        MaxRevNo = Val(Menu.Tables(0).Rows(0)("NilMax").ToString()) + 1
                    End If

                    Dim StandardCheckItem As New ClsQCSMaster
                    StandardCheckItem.LineID = cbolineid.Value
                    StandardCheckItem.PartID = cbopartid.Value
                    StandardCheckItem.RevNo = MaxRevNo
                    StandardCheckItem.RevDate = dtrevdate.Value
                    StandardCheckItem.RevHistory = txtrevisionhistory.Value
                    StandardCheckItem.PreparedBy = txtpreparedby.Value
                    'StandardCheckItem.SafetySymbol = txtsafety.Value
                    StandardCheckItem.RequiredAttachmentStatus = cboattachment.Value
                    StandardCheckItem.CreateUser = pUser

                    'ClsQCSMasterDB.Insert(StandardCheckItem)

                    If pErr <> "" Then
                        show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                    Else
                        Dim StandardCheckItemDetail As New ClsQCSMasterDetail
                        StandardCheckItemDetail.LineIDCopy = cbolineidpopup.Value
                        StandardCheckItemDetail.PartIDCopy = cbopartidpopup.Text
                        StandardCheckItemDetail.RevNoCopy = cborevnopopup.Value

                        'ClsQCSMasterDetailDB.Copy(StandardCheckItemDetail, pErr)
                        ClsQCSMasterDB.InsertCopy(StandardCheckItem, StandardCheckItemDetail, pErr)

                        If pErr <> "" Then
                            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                        Else
                            show_error(MsgTypeEnum.Success, "Copy data successfully!", 1)
                            'up_GridLoadMenu(cbopartid.Value, cborevno.Value)
                            GridMenu.JSProperties("cp_aftersave") = "1"
                            GridMenu.JSProperties("cp_cborevno") = MaxRevNo

                            cborevno.Value = MaxRevNo
                            up_GridLoadMenu(cbolineid.Value, cbopartid.Value, MaxRevNo)
                        End If
                    End If
                Else
                    Dim Item As New ClsQCSMasterDetail
                    Item.LineID = cbolineid.Value
                    Item.PartID = cbopartid.Value
                    Item.RevNo = cborevno.Text
                    ClsQCSMasterDetailDB.DeleteAll(Item, pErr)

                    If pErr <> "" Then
                        show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                    Else
                        Dim StandardCheckItemDetail As New ClsQCSMasterDetail
                        StandardCheckItemDetail.LineID = cbolineid.Value
                        StandardCheckItemDetail.LineIDCopy = cbolineidpopup.Value
                        StandardCheckItemDetail.PartID = cbopartid.Value
                        StandardCheckItemDetail.RevNo = cborevno.Text
                        StandardCheckItemDetail.PartIDCopy = cbopartidpopup.Value
                        StandardCheckItemDetail.RevNoCopy = cborevnopopup.Value

                        ClsQCSMasterDetailDB.Copy(StandardCheckItemDetail, pErr)

                        If pErr <> "" Then
                            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                        Else
                            show_error(MsgTypeEnum.Success, "Copy data successfully!", 1)
                            'up_GridLoadMenu(cbopartid.Value, cborevno.Value)
                            GridMenu.JSProperties("cp_loadsave") = "1"
                            GridMenu.JSProperties("cp_revno") = cborevno.Text
                            cborevno.Value = cborevno.Text
                            up_GridLoadMenu(cbolineid.Value, cbopartid.Value, cborevno.Text)
                        End If
                    End If
                End If

            Case "Delete"
                Dim pErr As String = ""
                Dim Item As New ClsQCSMasterDetail
                Item.LineID = cbolineid.Value
                Item.PartID = cbopartid.Value
                Item.RevNo = cborevno.Text
                ClsQCSMasterDetailDB.DeleteAll(Item, "")
                If pErr <> "" Then
                    show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                Else
                    Dim QCS As New ClsQCSMaster
                    QCS.LineID = cbolineid.Value
                    QCS.PartID = cbopartid.Value
                    QCS.RevNo = cborevno.Value
                    ClsQCSMasterDB.Delete(QCS, "")
                    If pErr <> "" Then
                        show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                    Else
                        show_error(MsgTypeEnum.Success, "Delete data successfully!", 1)
                        GridMenu.JSProperties("cp_clearheaderdelete") = 1
                        GridMenu.JSProperties("cp_clearapprovalinformation") = 1
                    End If
                    up_GridLoadMenu(cbolineid.Text, cbopartid.Value, cborevno.Text)
                End If
                '=========================================================================================================================================================
            Case "GridClear"
                up_GridLoadMenu("", "", "")
                GridMenu.JSProperties("cp_clearapprovalinformation") = 1

            Case "ClickNew"
                up_GridLoadMenu("", "", "")
                GridMenu.JSProperties("cp_clearapprovalinformation") = 1
                Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
                commandColumn.Visible = True

            Case "GridGet"
                Dim pPart As String = Split(e.Parameters, "|")(1)
                Dim pRev As String = Split(e.Parameters, "|")(2)

                Dim ErrMsg As String = ""
                Dim Menu As DataSet
                Dim MenuName As DataSet
                Menu = ClsQCSMasterDB.ExistQCS(cbolineid.Value, pPart, pRev, ErrMsg)
                MenuName = ClsQCSMasterDB.GetApprovalName(cbolineid.Value, pPart, pRev, ErrMsg)
                If ErrMsg = "" Then
                    If Menu.Tables(0).Rows.Count = 0 Then
                    Else
                        GridMenu.JSProperties("cp_fillheader") = 1
                        GridMenu.JSProperties("cp_revdate") = Format(Menu.Tables(0).Rows(0)("RevDate"), "dd MMM yyyy")
                        GridMenu.JSProperties("cp_revhistory") = Menu.Tables(0).Rows(0)("RevHistory").ToString
                        GridMenu.JSProperties("cp_preparedby") = Menu.Tables(0).Rows(0)("PreparedBy").ToString
                        GridMenu.JSProperties("cp_activestatus") = Menu.Tables(0).Rows(0)("ActiveStatus").ToString
                        'GridMenu.JSProperties("cp_safetysymbol") = Menu.Tables(0).Rows(0)("SafetySymbol").ToString

                        If IsDBNull(Menu.Tables(0).Rows(0)("SafetySymbol1")) Then
                            GridMenu.JSProperties("cp_safetysymbol1") = "img/musashi/ImagePreview1.gif"
                        Else
                            GridMenu.JSProperties("cp_safetysymbol1") = "data:image/png;base64," + Convert.ToBase64String(Menu.Tables(0).Rows(0)("SafetySymbol1"))
                        End If

                        If IsDBNull(Menu.Tables(0).Rows(0)("SafetySymbol2")) Then
                            GridMenu.JSProperties("cp_safetysymbol2") = "img/musashi/ImagePreview1.gif"
                        Else
                            GridMenu.JSProperties("cp_safetysymbol2") = "data:image/png;base64," + Convert.ToBase64String(Menu.Tables(0).Rows(0)("SafetySymbol2"))
                        End If

                        If IsDBNull(Menu.Tables(0).Rows(0)("SafetySymbol3")) Then
                            GridMenu.JSProperties("cp_safetysymbol3") = "img/musashi/ImagePreview1.gif"
                        Else
                            GridMenu.JSProperties("cp_safetysymbol3") = "data:image/png;base64," + Convert.ToBase64String(Menu.Tables(0).Rows(0)("SafetySymbol3"))
                        End If

                        If IsDBNull(Menu.Tables(0).Rows(0)("SafetySymbol4")) Then
                            GridMenu.JSProperties("cp_safetysymbol4") = "img/musashi/ImagePreview1.gif"
                        Else
                            GridMenu.JSProperties("cp_safetysymbol4") = "data:image/png;base64," + Convert.ToBase64String(Menu.Tables(0).Rows(0)("SafetySymbol4"))
                        End If

                        If IsDBNull(Menu.Tables(0).Rows(0)("SafetySymbol5")) Then
                            GridMenu.JSProperties("cp_safetysymbol5") = "img/musashi/ImagePreview1.gif"
                        Else
                            GridMenu.JSProperties("cp_safetysymbol5") = "data:image/png;base64," + Convert.ToBase64String(Menu.Tables(0).Rows(0)("SafetySymbol5"))
                        End If

                        If IsDBNull(Menu.Tables(0).Rows(0)("SafetySymbol6")) Then
                            GridMenu.JSProperties("cp_safetysymbol6") = "img/musashi/ImagePreview1.gif"
                        Else
                            GridMenu.JSProperties("cp_safetysymbol6") = "data:image/png;base64," + Convert.ToBase64String(Menu.Tables(0).Rows(0)("SafetySymbol6"))
                        End If

                        'GridMenu.JSProperties("cp_safetysymbol2") = "data:image/png;base64," + Convert.ToBase64String(Menu.Tables(0).Rows(0)("SafetySymbol2"))
                        'GridMenu.JSProperties("cp_safetysymbol3") = "data:image/png;base64," + Convert.ToBase64String(Menu.Tables(0).Rows(0)("SafetySymbol3"))
                        'GridMenu.JSProperties("cp_safetysymbol4") = "data:image/png;base64," + Convert.ToBase64String(Menu.Tables(0).Rows(0)("SafetySymbol4"))
                        'GridMenu.JSProperties("cp_safetysymbol5") = "data:image/png;base64," + Convert.ToBase64String(Menu.Tables(0).Rows(0)("SafetySymbol5"))
                        'GridMenu.JSProperties("cp_safetysymbol6") = "data:image/png;base64," + Convert.ToBase64String(Menu.Tables(0).Rows(0)("SafetySymbol6"))
                        GridMenu.JSProperties("cp_attachemnt") = Menu.Tables(0).Rows(0)("RequiredAttachmentStatus").ToString

                        GridMenu.JSProperties("cp_fillapprovalinformation") = 1
                        GridMenu.JSProperties("cp_approvalstatus1") = Menu.Tables(0).Rows(0)("ApprovalStatus1").ToString
                        GridMenu.JSProperties("cp_approvalstatus2") = Menu.Tables(0).Rows(0)("ApprovalStatus2").ToString
                        GridMenu.JSProperties("cp_approvalstatus3") = Menu.Tables(0).Rows(0)("ApprovalStatus3").ToString

                        If Menu.Tables(0).Rows(0)("ApprovalStatus1") = 0 Then
                            GridMenu.JSProperties("cp_approvalpic1") = "UNAPPROVED"
                            GridMenu.JSProperties("cp_approvaldate1") = "UNAPPROVED"
                        Else
                            If MenuName.Tables(0).Rows(0)("FullName").ToString = "" Then
                                GridMenu.JSProperties("cp_approvalpic1") = MenuName.Tables(0).Rows(0)("ApprovalPIC").ToString
                            Else
                                GridMenu.JSProperties("cp_approvalpic1") = MenuName.Tables(0).Rows(0)("FullName").ToString
                            End If

                            GridMenu.JSProperties("cp_approvaldate1") = Format(Menu.Tables(0).Rows(0)("ApprovalDate1"), "dd MMM yyyy")
                        End If

                        If Menu.Tables(0).Rows(0)("ApprovalStatus2") = 0 Then
                            GridMenu.JSProperties("cp_approvalpic2") = "UNAPPROVED"
                            GridMenu.JSProperties("cp_approvaldate2") = "UNAPPROVED"
                        Else
                            'GridMenu.JSProperties("cp_approvalpic2") = Menu.Tables(0).Rows(0)("ApprovalPIC2").ToString
                            If MenuName.Tables(0).Rows(1)("FullName").ToString = "" Then
                                GridMenu.JSProperties("cp_approvalpic2") = MenuName.Tables(0).Rows(1)("ApprovalPIC").ToString
                            Else
                                GridMenu.JSProperties("cp_approvalpic2") = MenuName.Tables(0).Rows(1)("FullName").ToString
                            End If

                            GridMenu.JSProperties("cp_approvaldate2") = Format(Menu.Tables(0).Rows(0)("ApprovalDate2"), "dd MMM yyyy")
                        End If

                        If Menu.Tables(0).Rows(0)("ApprovalStatus3") = 0 Then
                            GridMenu.JSProperties("cp_approvalpic3") = "UNAPPROVED"
                            GridMenu.JSProperties("cp_approvaldate3") = "UNAPPROVED"
                        Else
                            'GridMenu.JSProperties("cp_approvalpic3") = Menu.Tables(0).Rows(0)("ApprovalPIC3").ToString
                            If MenuName.Tables(0).Rows(2)("FullName").ToString = "" Then
                                GridMenu.JSProperties("cp_approvalpic3") = MenuName.Tables(0).Rows(2)("ApprovalPIC").ToString
                            Else
                                GridMenu.JSProperties("cp_approvalpic3") = MenuName.Tables(0).Rows(2)("FullName").ToString
                            End If

                            GridMenu.JSProperties("cp_approvaldate3") = Format(Menu.Tables(0).Rows(0)("ApprovalDate3"), "dd MMM yyyy")
                        End If

                        If ClsQCSMasterDB.GetDataQELeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False Then
                            LoadApprovalInformation()
                            GridMenu.JSProperties("cp_btnapprove") = "1"
                            GridMenu.JSProperties("cp_txtapprove") = "Approve"
                            ApprovedTrue(1)
                        ElseIf ClsQCSMasterDB.GetDataQELeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                            LoadApprovalInformation()
                            GridMenu.JSProperties("cp_btnapprove") = "0"
                            GridMenu.JSProperties("cp_txtapprove") = "Approve"
                            ApprovedTrue(1)

                        ElseIf ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                            LoadApprovalInformation()
                            GridMenu.JSProperties("cp_btnapprove") = "0"
                            GridMenu.JSProperties("cp_txtapprove") = "Approve"
                            ApprovedTrue(1)

                        ElseIf ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                            LoadApprovalInformation()
                            GridMenu.JSProperties("cp_btnapprove") = "0"
                            GridMenu.JSProperties("cp_txtapprove") = "Unapprove"
                            ApprovedTrue(1)

                        ElseIf ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False Then
                            LoadApprovalInformation()
                            GridMenu.JSProperties("cp_btnapprove") = "1"
                            GridMenu.JSProperties("cp_txtapprove") = "Unapprove"
                            ApprovedTrue(1)

                        ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                            LoadApprovalInformation()
                            GridMenu.JSProperties("cp_btnapprove") = "0"
                            GridMenu.JSProperties("cp_txtapprove") = "Approve"
                            ApprovedFalse(1)

                        ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                            LoadApprovalInformation()
                            GridMenu.JSProperties("cp_btnapprove") = "0"
                            GridMenu.JSProperties("cp_txtapprove") = "Unapprove"
                            ApprovedTrue(1)

                        ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False Then
                            LoadApprovalInformation()
                            GridMenu.JSProperties("cp_btnapprove") = "1"
                            GridMenu.JSProperties("cp_txtapprove") = "Unapprove"
                            ApprovedTrue(1)
                        ElseIf AuthUpdate = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False Then
                            ApprovedTrue(1)
                        ElseIf AuthUpdate = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                            ApprovedFalse(1)
                        End If

                    End If
                Else
                    show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
                    Exit Sub
                End If
                up_GridLoadMenu(cbolineid.Value, pPart, pRev)

            Case "ClickApprove"
                Dim pErr As String = ""
                Dim Menu As DataSet
                Dim pGetButton As String = Split(e.Parameters, "|")(1)

                'APPROVE
                If pGetButton = "Approve" Then
                    If ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataQELeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                        UpdateStatus()
                        Approval1()
                        Approval2()
                        Approval3()
                        LoadApprovalInformation()
                        GridMenu.JSProperties("cp_btnapprove") = "1"
                        GridMenu.JSProperties("cp_txtapprove") = "Approve"
                        ApprovedTrue(1)
                    ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                        Approval1()
                        Approval2()
                        LoadApprovalInformation()
                        GridMenu.JSProperties("cp_btnapprove") = "0"
                        GridMenu.JSProperties("cp_txtapprove") = "Unapprove"
                        ApprovedTrue(1)
                    ElseIf ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataQELeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                        UpdateStatus()
                        Approval2()
                        Approval3()
                        LoadApprovalInformation()
                        GridMenu.JSProperties("cp_btnapprove") = "1"
                        GridMenu.JSProperties("cp_txtapprove") = "Approve"
                        ApprovedTrue(1)
                    ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                        Approval1()
                        LoadApprovalInformation()
                        GridMenu.JSProperties("cp_btnapprove") = "0"
                        GridMenu.JSProperties("cp_txtapprove") = "Unapprove"
                        ApprovedTrue(1)
                    ElseIf ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                        Approval2()
                        LoadApprovalInformation()
                        GridMenu.JSProperties("cp_btnapprove") = "0"
                        GridMenu.JSProperties("cp_txtapprove") = "Unapprove"
                        ApprovedTrue(1)
                    ElseIf ClsQCSMasterDB.GetDataQELeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                        UpdateStatus()
                        Approval3()
                        LoadApprovalInformation()
                        GridMenu.JSProperties("cp_btnapprove") = "1"
                        GridMenu.JSProperties("cp_txtapprove") = "Approve"
                        ApprovedTrue(1)
                    End If

                    'UNAPPROVE
                ElseIf pGetButton = "Unapprove" Then
                    'Cek Line User
                    If ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                        Unapprove2()
                        Unapprove1()
                        LoadApprovalInformation()
                        GridMenu.JSProperties("cp_btnapprove") = "0"
                        GridMenu.JSProperties("cp_txtapprove") = "Approve"
                        ApprovedFalse(1)
                    ElseIf ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                        Unapprove2()
                        LoadApprovalInformation()
                        GridMenu.JSProperties("cp_btnapprove") = "0"
                        GridMenu.JSProperties("cp_txtapprove") = "Approve"
                        ApprovedTrue(1)
                    ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True And ClsQCSMasterDB.GetDataApprovalQE(cbolineid.Value, cbopartid.Value, cborevno.Value, "") = True Then
                        Unapprove1()
                        LoadApprovalInformation()
                        GridMenu.JSProperties("cp_btnapprove") = "0"
                        GridMenu.JSProperties("cp_txtapprove") = "Approve"
                        ApprovedFalse(1)
                    End If
                End If
            Case "ButtonNewClick"
                Dim pErr As String = ""
                Dim Menu As DataSet
                Menu = ClsQCSMasterDB.GetLastRevApproval(cbolineid.Value, cbopartid.Value, "")
                If ClsQCSMasterDB.GetLastRevApprovalB(cbolineid.Value, cbopartid.Value, "") = True Then
                    show_error(MsgTypeEnum.Warning, "Part ID : " & cbopartid.Value & ", " & "Line ID : " & cbolineid.Value & ", " & "Rev No : " & Menu.Tables(0).Rows(0)("RevNo").ToString() & " has not been approved", 1)
                Else
                    GridMenu.JSProperties("cp_newstatusapproved") = "1"
                End If

            Case "SelectNewClick"
                Dim pErr As String = ""
                Dim Menu As DataSet
                Menu = ClsQCSMasterDB.GetLastRevApproval(cbolineid.Value, cbopartid.Value, "")
                If ClsQCSMasterDB.GetLastRevApprovalB(cbolineid.Value, cbopartid.Value, "") = True Then
                    GridMenu.JSProperties("cp_newstatusunapproved") = "1"
                    GridMenu.JSProperties("cp_partid") = cbopartid.Value
                    GridMenu.JSProperties("cp_lineid") = cbolineid.Value
                    GridMenu.JSProperties("cp_revno") = Menu.Tables(0).Rows(0)("RevNo").ToString()
                    'show_error(MsgTypeEnum.Warning, "Part ID : " & cbopartid.Value & ", " & "Line ID : " & cbolineid.Value & ", " & "Rev No : " & Menu.Tables(0).Rows(0)("RevNo").ToString() & " has not been approved", 1)
                Else
                    GridMenu.JSProperties("cp_newstatusapproved") = "1"
                End If

            Case "DeleteSymbol1"
                Dim QCS As New ClsQCSMaster
                QCS.LineID = cbolineid.Value
                QCS.PartID = cbopartid.Value
                QCS.RevNo = cborevno.Value
                ClsQCSMasterDB.DeleteSymbol1(QCS, "")
                GridMenu.JSProperties("cp_delsafetysymbol1") = 1
                GridMenu.JSProperties("cp_safetysymbol1") = "img/musashi/ImagePreview1.gif"

            Case "DeleteSymbol2"
                Dim QCS As New ClsQCSMaster
                QCS.LineID = cbolineid.Value
                QCS.PartID = cbopartid.Value
                QCS.RevNo = cborevno.Value
                ClsQCSMasterDB.DeleteSymbol2(QCS, "")
                GridMenu.JSProperties("cp_delsafetysymbol2") = 1
                GridMenu.JSProperties("cp_safetysymbol2") = "img/musashi/ImagePreview1.gif"

            Case "DeleteSymbol3"
                Dim QCS As New ClsQCSMaster
                QCS.LineID = cbolineid.Value
                QCS.PartID = cbopartid.Value
                QCS.RevNo = cborevno.Value
                ClsQCSMasterDB.DeleteSymbol3(QCS, "")
                GridMenu.JSProperties("cp_delsafetysymbol3") = 1
                GridMenu.JSProperties("cp_safetysymbol3") = "img/musashi/ImagePreview1.gif"

            Case "DeleteSymbol4"
                Dim QCS As New ClsQCSMaster
                QCS.LineID = cbolineid.Value
                QCS.PartID = cbopartid.Value
                QCS.RevNo = cborevno.Value
                ClsQCSMasterDB.DeleteSymbol4(QCS, "")
                GridMenu.JSProperties("cp_delsafetysymbol4") = 1
                GridMenu.JSProperties("cp_safetysymbol4") = "img/musashi/ImagePreview1.gif"

            Case "DeleteSymbol5"
                Dim QCS As New ClsQCSMaster
                QCS.LineID = cbolineid.Value
                QCS.PartID = cbopartid.Value
                QCS.RevNo = cborevno.Value
                ClsQCSMasterDB.DeleteSymbol5(QCS, "")
                GridMenu.JSProperties("cp_delsafetysymbol5") = 1
                GridMenu.JSProperties("cp_safetysymbol5") = "img/musashi/ImagePreview1.gif"

            Case "DeleteSymbol6"
                Dim QCS As New ClsQCSMaster
                QCS.LineID = cbolineid.Value
                QCS.PartID = cbopartid.Value
                QCS.RevNo = cborevno.Value
                ClsQCSMasterDB.DeleteSymbol6(QCS, "")
                GridMenu.JSProperties("cp_delsafetysymbol6") = 1
                GridMenu.JSProperties("cp_safetysymbol6") = "img/musashi/ImagePreview1.gif"
        End Select
    End Sub

    Protected Sub GridMenu_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles GridMenu.RowInserting
        e.Cancel = True
        Dim pErr As String = ""


        If ClsQCSMasterDB.isExistQCS(cbolineid.Value, cbopartid.Value, cborevno.Value) = False Then
            If cborevno.Value = "" Then
                show_error(MsgTypeEnum.Warning, "Please Fill Data Rev No!", 1)
                Exit Sub
            ElseIf dtrevdate.Text = "" Then
                show_error(MsgTypeEnum.Warning, "Please Fill Data Rev Date!", 1)
                Exit Sub
            ElseIf txtrevisionhistory.Text = "" Then
                show_error(MsgTypeEnum.Warning, "Please Fill Data Rev History!", 1)
                Exit Sub
            ElseIf txtpreparedby.Text = "" Then
                show_error(MsgTypeEnum.Warning, "Please Fill Data Prepared By!", 1)
                Exit Sub
            End If

            Dim Menu As DataSet
            Dim MaxRevNo As Integer
            Menu = ClsQCSMasterDB.isExistRevNo(cbolineid.Value, cbopartid.Value, "")
            If Menu.Tables(0).Rows(0)("NilMax").ToString() = "" Then
                MaxRevNo = 1
            Else
                MaxRevNo = Val(Menu.Tables(0).Rows(0)("NilMax").ToString()) + 1
            End If

            Dim StandardCheckItem As New ClsQCSMaster
            StandardCheckItem.LineID = cbolineid.Value
            StandardCheckItem.PartID = cbopartid.Value
            StandardCheckItem.RevNo = MaxRevNo
            StandardCheckItem.RevDate = dtrevdate.Text
            StandardCheckItem.RevHistory = txtrevisionhistory.Text
            StandardCheckItem.PreparedBy = txtpreparedby.Text
            'StandardCheckItem.SafetySymbol = txtsafety.Text
            StandardCheckItem.RequiredAttachmentStatus = cboattachment.Value
            StandardCheckItem.CreateUser = pUser

            'ClsQCSMasterDB.Insert(StandardCheckItem)

            If pErr <> "" Then
                show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
            Else
                Dim StandardCheckItemDetail As New ClsQCSMasterDetail With
                    {.ItemID = e.NewValues("ItemID"),
                     .SeqNo = e.NewValues("SeqNo"),
                     .ProcessID = e.NewValues("ProcessID"),
                     .KPointStatus = e.NewValues("KPointStatus"),
                     .Item = e.NewValues("Item"),
                     .Standard = e.NewValues("Standard"),
                     .ValueType = e.NewValues("ValueType"),
                     .NumRangeStart = Val(e.NewValues("NumRangeStart")),
                     .NumRangeEnd = Val(e.NewValues("NumRangeEnd")),
                     .TextRange = e.NewValues("TextRange"),
                     .MeasuringInstrument = e.NewValues("MeasuringInstrument"),
                     .XRCode = e.NewValues("XRCode"),
                     .FrequencyType = e.NewValues("FrequencyType")}
                ClsQCSMasterDB.InsertHeadDet(StandardCheckItem, StandardCheckItemDetail, pErr)
                'ClsQCSMasterDetailDB.Insert(StandardCheckItemDetail, pErr)

                If pErr <> "" Then
                    show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                Else

                    show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
                    Dim dsMenu As DataTable
                    dsMenu = ClsQCSMasterDB.GetDataRev(cbopartid.Value, "")

                    GridMenu.JSProperties("cp_aftersave") = "1"
                    GridMenu.JSProperties("cp_cborevno") = MaxRevNo
                    GridMenu.JSProperties("cp_aftersavegridget") = "1"
                    cborevno.Value = MaxRevNo
                    up_GridLoadMenu(cbolineid.Value, cbopartid.Value, MaxRevNo)
                    GridMenu.CancelEdit()
                End If
            End If
        ElseIf ClsQCSMasterDB.isExistQCS(cbolineid.Value, cbopartid.Value, cborevno.Value) = True Then
            Dim StandardCheckItemDetail As New ClsQCSMasterDetail With
                    {.LineID = cbolineid.Value,
                     .PartID = cbopartid.Value,
                     .RevNo = cborevno.Value,
                     .ItemID = e.NewValues("ItemID"),
                     .SeqNo = e.NewValues("SeqNo"),
                     .ProcessID = e.NewValues("ProcessID"),
                     .KPointStatus = e.NewValues("KPointStatus"),
                     .Item = e.NewValues("Item"),
                     .Standard = e.NewValues("Standard"),
                     .ValueType = e.NewValues("ValueType"),
                     .NumRangeStart = Val(e.NewValues("NumRangeStart")),
                     .NumRangeEnd = Val(e.NewValues("NumRangeEnd")),
                     .TextRange = e.NewValues("TextRange"),
                     .MeasuringInstrument = e.NewValues("MeasuringInstrument"),
                     .XRCode = e.NewValues("XRCode"),
                     .FrequencyType = e.NewValues("FrequencyType")}
            ClsQCSMasterDetailDB.Insert(StandardCheckItemDetail, pErr)

            If pErr <> "" Then
                show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
            Else
                GridMenu.CancelEdit()
                show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
                up_GridLoadMenu(cbolineid.Value, cbopartid.Value, cborevno.Value)
            End If
        End If
    End Sub

    Private Sub GridMenu_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles GridMenu.RowUpdating
        e.Cancel = True
        Dim pErr As String = ""
        Dim SetMenu As New ClsQCSMasterDetail With {.LineID = cbolineid.Value,
                                                    .PartID = cbopartid.Value,
                                                    .RevNo = cborevno.Value,
                                                    .ItemID = e.NewValues("ItemID"),
                                                    .ProcessID = e.NewValues("ProcessID"),
                                                    .KPointStatus = e.NewValues("KPointStatus"),
                                                    .Item = e.NewValues("Item"),
                                                    .SeqNo = e.NewValues("SeqNo"),
                                                    .Standard = e.NewValues("Standard"),
                                                    .ValueType = e.NewValues("ValueType"),
                                                    .NumRangeStart = Val(e.NewValues("NumRangeStart")),
                                                    .NumRangeEnd = Val(e.NewValues("NumRangeEnd")),
                                                    .TextRange = e.NewValues("TextRange"),
                                                    .MeasuringInstrument = e.NewValues("MeasuringInstrument"),
                                                    .XRCode = e.NewValues("XRCode"),
                                                    .FrequencyType = e.NewValues("FrequencyType")
                                            }

        ClsQCSMasterDetailDB.Update(SetMenu, pErr)

        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            GridMenu.CancelEdit()
            show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
            up_GridLoadMenu(cbolineid.Value, cbopartid.Value, cborevno.Value)
        End If
    End Sub

    Protected Sub GridMenu_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles GridMenu.RowDeleting
        e.Cancel = True
        Dim pErr As String = ""
        Dim Item As New ClsQCSMasterDetail With {.LineID = cbolineid.Value,
                                                 .ItemID = e.Values("ItemID"),
                                                 .PartID = cbopartid.Value,
                                                 .RevNo = cborevno.Text}
        ClsQCSMasterDetailDB.Delete(Item, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            GridMenu.CancelEdit()
            show_error(MsgTypeEnum.Success, "Delete data successfully!", 1)
            up_GridLoadMenu(cbolineid.Value, cbopartid.Value, cborevno.Value)
        End If

    End Sub

    Protected Sub GridMenu_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
        Dim GridData As DevExpress.Web.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView)

        For Each column As GridViewColumn In GridMenu.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            End If

            If dataColumn.FieldName = "ItemID" Then
                If IsNothing(e.NewValues("ItemID")) OrElse e.NewValues("ItemID").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Item ID!"
                Else
                    If e.IsNewRow Then
                        If ClsSubLineDB.isExist(cbopartid.Value, cborevno.Value, e.NewValues("ItemID"), "") Then
                            e.Errors(dataColumn) = "Data is already exist!"
                            show_error(MsgTypeEnum.Warning, e.Errors(dataColumn), 1)
                            show_error(MsgTypeEnum.Warning, "Data is already exist!", 1)
                            Exit Sub
                        End If
                    End If
                End If
            End If

            If dataColumn.FieldName = "ProcessName" Then
                If IsNothing(e.NewValues("ProcessName")) OrElse e.NewValues("ProcessName").ToString.Trim = "" Then
                    GridMenu.JSProperties("cp_message") = 1
                    e.Errors(dataColumn) = "Please input Process!"
                End If
            End If

            'If dataColumn.FieldName = "KPointStatus" Then
            '    If IIf(e.NewValues("KPointStatus") = Nothing, e.NewValues("KPointStatus") = "", e.NewValues("KPointStatus")) Then
            '        'GridMenu.JSProperties("cp_message") = 1
            '        'e.Errors(dataColumn) = "Please input K Point!"
            '    End If
            'End If

            If dataColumn.FieldName = "Item" Then
                If IsNothing(e.NewValues("Item")) OrElse e.NewValues("Item").ToString.Trim = "" Then
                    GridMenu.JSProperties("cp_message") = 1
                    e.Errors(dataColumn) = "Please input Item!"
                End If
            End If

            If dataColumn.FieldName = "ValueType" Then
                If IsNothing(e.NewValues("ValueType")) OrElse e.NewValues("ValueType").ToString.Trim = "" Then
                    GridMenu.JSProperties("cp_message") = 1
                    e.Errors(dataColumn) = "Please input Value Type!"
                End If
            End If

            If dataColumn.FieldName = "TextRange" Then
                If IsNothing(e.NewValues("ValueType")) And IsNothing(e.NewValues("TextRange")) Then
                    If e.NewValues("ValueType").ToString = "T" Then
                        GridMenu.JSProperties("cp_message") = 1
                        e.Errors(dataColumn) = "Please input Text Range!"
                        ValueType = e.NewValues("ValueType").ToString
                        GridMenu.JSProperties("cp_valuetypeedit") = 1
                        GridMenu.JSProperties("cp_valuetypevalue") = ValueType

                    End If
                End If
            End If

            If dataColumn.FieldName = "NumRangeStart" Then
                'If e.NewValues("NumRangeStart") = 0.0 Then
                '    If e.NewValues("ValueType").ToString = "N" Then
                '        GridMenu.JSProperties("cp_message") = 1
                '        e.Errors(dataColumn) = "Please input Num Range Start!"
                '        ValueType = e.NewValues("ValueType").ToString
                '        GridMenu.JSProperties("cp_valuetypeedit") = 1
                '        GridMenu.JSProperties("cp_valuetypevalue") = ValueType
                '    End If
                'Else
                If e.NewValues("NumRangeStart") > e.NewValues("NumRangeEnd") Then
                    GridMenu.JSProperties("cp_messagelessthan") = 1
                    'e.Errors(dataColumn) = "End Range cannot less than Start Range!"
                    ValueType = e.NewValues("ValueType").ToString
                    GridMenu.JSProperties("cp_valuetypeedit") = 1
                    GridMenu.JSProperties("cp_valuetypevalue") = ValueType
                End If
            End If

            If dataColumn.FieldName = "NumRangeEnd" Then
                'If e.NewValues("NumRangeEnd") = 0.0 Then
                '    If e.NewValues("ValueType").ToString = "N" Then
                '        GridMenu.JSProperties("cp_message") = 1
                '        e.Errors(dataColumn) = "Please input Num Range End!"
                '        ValueType = e.NewValues("ValueType").ToString
                '        GridMenu.JSProperties("cp_valuetypeedit") = 1
                '        GridMenu.JSProperties("cp_valuetypevalue") = ValueType
                '    End If
                'Else
                If e.NewValues("NumRangeStart") > e.NewValues("NumRangeEnd") Then
                    GridMenu.JSProperties("cp_messagelessthan") = 1
                    e.Errors(dataColumn) = "End Range cannot less than Start Range!"
                    ValueType = e.NewValues("ValueType").ToString
                    GridMenu.JSProperties("cp_valuetypeedit") = 1
                    GridMenu.JSProperties("cp_valuetypevalue") = ValueType
                End If
            End If

            'If dataColumn.FieldName = "NumRangeStart" Then
            '    If IsNothing(e.NewValues("ValueType")) Then
            '    ElseIf e.NewValues("ValueType").ToString = "N" Then
            '        If e.NewValues("NumRangeStart") = 0.0 Then
            '            ValueType = e.NewValues("ValueType").ToString
            '            GridMenu.JSProperties("cp_valuetypeedit") = 1
            '            GridMenu.JSProperties("cp_valuetypevalue") = ValueType
            '            e.Errors(dataColumn) = "Please input Num Range Start!"
            '        End If
            '    End If
            'End If

            'If dataColumn.FieldName = "NumRangeEnd" Then
            '    If IsNothing(e.NewValues("ValueType")) Then
            '    ElseIf e.NewValues("ValueType").ToString = "N" Then
            '        If e.NewValues("NumRangeEnd") = 0.0 Then
            '            ValueType = e.NewValues("ValueType").ToString
            '            GridMenu.JSProperties("cp_valuetypeedit") = 1
            '            GridMenu.JSProperties("cp_valuetypevalue") = ValueType
            '            e.Errors(dataColumn) = "Please input Num Range End!"
            '        End If
            '    Else
            '    End If
            'End If


            If dataColumn.FieldName = "Standard" Then
                If IsNothing(e.NewValues("Standard")) OrElse e.NewValues("Standard").ToString.Trim = "" Then
                    GridMenu.JSProperties("cp_message") = 1
                    e.Errors(dataColumn) = "Please input Standard!"
                End If
            End If

            If dataColumn.FieldName = "MeasuringInstrument" Then
                If IsNothing(e.NewValues("MeasuringInstrument")) OrElse e.NewValues("MeasuringInstrument").ToString.Trim = "" Then
                    GridMenu.JSProperties("cp_message") = 1
                    e.Errors(dataColumn) = "Please input Measuring Instrument!"
                End If
            End If

            If dataColumn.FieldName = "FrequencyType" Then
                If IsNothing(e.NewValues("FrequencyType")) OrElse e.NewValues("FrequencyType").ToString.Trim = "" Then
                    GridMenu.JSProperties("cp_message") = 1
                    e.Errors(dataColumn) = "Please input Frequency Type!"
                End If
            End If
        Next column
    End Sub

    Protected Sub GridMenu_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles GridMenu.StartRowEditing

    End Sub
#End Region

#Region "Other"
    Private Sub cbolineid_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbolineid.Callback
        If ClsQCSMasterDB.GetDataQE(pUser, "") = True Then
            up_fillcomboline(1, pUser)
        Else
            up_fillcomboline(0, pUser)
        End If
    End Sub

    Private Sub cbopartid_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbopartid.Callback
        'Dim pParam As String = Split(e.Parameter, "|")(1)

        'Dim dsMenu As DataTable
        'dsMenu = ClsQCSMasterDB.GetDataPart(pParam, "")
        'cbopartid.DataSource = dsMenu
        'cbopartid.DataBind()
    End Sub

    Private Sub cbopartid_Init(sender As Object, e As System.EventArgs) Handles cbopartid.Init
        If Request.QueryString("LineID") Is Nothing Then
            Exit Sub
        Else
            Dim pParam As String = Request.QueryString("LineID").ToString()

            Dim dsMenu As DataTable
            dsMenu = ClsQCSMasterDB.GetDataPart(pParam, "")
            cbopartid.DataSource = dsMenu
            cbopartid.DataBind()
        End If
    End Sub

    Private Sub cborevno_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cborevno.Callback
        Dim pPartID As String = Split(e.Parameter, "|")(1)
        Dim pLineID As String = Split(e.Parameter, "|")(2)

        Dim dsMenu As DataTable
        dsMenu = ClsQCSMasterDB.GetDataRev(pPartID, pLineID, "")
        cborevno.DataSource = dsMenu
        cborevno.DataBind()
    End Sub

    Private Sub cborevno_Init(sender As Object, e As System.EventArgs) Handles cborevno.Init
        If Request.QueryString("PartID") Is Nothing Then
            Exit Sub
        Else
            Dim pParam As String = Request.QueryString("PartID").ToString()

            Dim dsMenu As DataTable
            dsMenu = ClsQCSMasterDB.GetDataRev(pParam, "")
            cborevno.DataSource = dsMenu
            cborevno.DataBind()
        End If
    End Sub

    Private Sub cbolineidpopup_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbolineidpopup.Callback
        Dim pPartID As String = Split(e.Parameter, "|")(1)

        Dim dsMenu As DataTable
        dsMenu = ClsQCSMasterDB.GetDataLineCopy(pPartID, "")
        cbolineidpopup.DataSource = dsMenu
        cbolineidpopup.DataBind()

    End Sub

    Private Sub cbopartidpopup_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbopartidpopup.Callback
        Dim pParam As String = Split(e.Parameter, "|")(1)

        Dim dsMenu As DataTable
        dsMenu = ClsQCSMasterDB.GetDataPart(pParam, "")
        cbopartidpopup.DataSource = dsMenu
        cbopartidpopup.DataBind()

    End Sub

    Private Sub cborevnopopup_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cborevnopopup.Callback
        Dim pPartID As String = Split(e.Parameter, "|")(1)
        Dim pLineID As String = Split(e.Parameter, "|")(2)

        Dim dsMenu As DataTable
        dsMenu = ClsQCSMasterDB.GetDataRevPopUp(pPartID, pLineID, "")
        cborevnopopup.DataSource = dsMenu
        cborevnopopup.DataBind()
        'cborevno.Style.
        'cborevno.Properties.DropDownStyle = DropDownStyle.DropDownList
    End Sub
#End Region
#End Region

#Region "Download To Excel"

    Private Sub up_Excel(ByVal pPart As String, ByVal pPrev As String, ByVal pLine As String, ByVal pPartName As String, ByVal pLeaderPIC As String, ByVal pForemanPIC As String, ByVal pQEPIC As String, ByVal pLeaderDate As String, ByVal pForemanDate As String, ByVal pQEDate As String, Optional ByRef pErr As String = "")
        Try
            Dim fi As New FileInfo(Server.MapPath("~\Download\SQC" & "_" & pPart & "_" & pPrev & "_" & Format(dtrevdate.Value, "yyyyMMdd") & ".xlsx"))
            If fi.Exists Then
                fi.Delete()
                fi = New FileInfo(Server.MapPath("~\Download\SQC" & "_" & pPart & "_" & pPrev & "_" & Format(dtrevdate.Value, "yyyyMMdd") & ".xlsx"))
            End If
            Dim exl As New ExcelPackage(fi)
            Dim ws As ExcelWorksheet
            ws = exl.Workbook.Worksheets.Add("Sheet1")
            ws.View.ShowGridLines = False

            Dim Rpt As DataSet
            Rpt = ClsQCSMasterDetailDB.GetList(cbolineid.Value, pPart, pPrev, pErr)

            With ws
                .Cells(10, 1, 10, 1).Value = "No"
                .Cells(10, 1, 10, 1).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 1, 10, 1).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 1, 10, 1).Style.Font.Size = 10
                .Cells(10, 1, 10, 1).Style.Font.Name = "Segoe UI"
                .Cells(10, 1, 10, 1).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 2, 10, 2).Value = "Process"
                .Cells(10, 2, 10, 3).Merge = True
                .Cells(10, 2, 10, 3).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 2, 10, 3).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 2, 10, 3).Style.Font.Size = 10
                .Cells(10, 2, 10, 3).Style.Font.Name = "Segoe UI"
                .Cells(10, 2, 10, 3).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 4, 10, 4).Value = "K-Point"
                .Cells(10, 4, 10, 5).Merge = True
                .Cells(10, 4, 10, 5).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 4, 10, 5).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 4, 10, 5).Style.Font.Size = 10
                .Cells(10, 4, 10, 5).Style.Font.Name = "Segoe UI"
                .Cells(10, 4, 10, 5).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 6, 10, 6).Value = "Item"
                .Cells(10, 6, 10, 8).Merge = True
                .Cells(10, 6, 10, 8).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 6, 10, 8).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 6, 10, 8).Style.Font.Size = 10
                .Cells(10, 6, 10, 8).Style.Font.Name = "Segoe UI"
                .Cells(10, 6, 10, 8).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 9, 10, 9).Value = "Value Type"
                .Cells(10, 9, 10, 10).Merge = True
                .Cells(10, 9, 10, 10).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 9, 10, 10).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 9, 10, 10).Style.Font.Size = 10
                .Cells(10, 9, 10, 10).Style.Font.Name = "Segoe UI"
                .Cells(10, 9, 10, 10).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 11, 10, 11).Value = "Standard"
                .Cells(10, 11, 10, 13).Merge = True
                .Cells(10, 11, 10, 13).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 11, 10, 13).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 11, 10, 13).Style.Font.Size = 10
                .Cells(10, 11, 10, 13).Style.Font.Name = "Segoe UI"
                .Cells(10, 11, 10, 13).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 14, 10, 14).Value = "Range"
                .Cells(10, 14, 10, 17).Merge = True
                .Cells(10, 14, 10, 17).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 14, 10, 17).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 14, 10, 17).Style.Font.Size = 10
                .Cells(10, 14, 10, 17).Style.Font.Name = "Segoe UI"
                .Cells(10, 14, 10, 17).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 18, 10, 18).Value = "Measuring Instrument"
                .Cells(10, 18, 10, 20).Merge = True
                .Cells(10, 18, 10, 20).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 18, 10, 20).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 18, 10, 20).Style.Font.Size = 10
                .Cells(10, 18, 10, 20).Style.Font.Name = "Segoe UI"
                .Cells(10, 18, 10, 20).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 21, 10, 21).Value = "XR Code"
                .Cells(10, 21, 10, 21).Merge = True
                .Cells(10, 21, 10, 21).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 21, 10, 21).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 21, 10, 21).Style.Font.Size = 10
                .Cells(10, 21, 10, 21).Style.Font.Name = "Segoe UI"
                .Cells(10, 21, 10, 21).Style.Font.Color.SetColor(Color.White)

                For i = 0 To Rpt.Tables(0).Rows.Count - 1
                    .Cells(i + 11, 1, i + 11, 1).Value = Rpt.Tables(0).Rows(i)("Number")
                    .Cells(i + 11, 1, i + 11, 1).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 11, 1, i + 11, 1).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 11, 1, i + 11, 1).Style.WrapText = True
                    .Cells(i + 11, 1, i + 11, 1).Style.Font.Size = 10
                    .Cells(i + 11, 1, i + 11, 1).Style.Font.Name = "Segoe UI"

                    .Cells(i + 11, 2, i + 11, 2).Value = Rpt.Tables(0).Rows(i)("ProcessName")
                    .Cells(i + 11, 2, i + 11, 3).Merge = True
                    .Cells(i + 11, 2, i + 11, 3).Style.WrapText = True
                    .Cells(i + 11, 2, i + 11, 3).Style.Font.Size = 10
                    .Cells(i + 11, 2, i + 11, 3).Style.Font.Name = "Segoe UI"

                    .Cells(i + 11, 4, i + 11, 4).Value = Rpt.Tables(0).Rows(i)("KPointStatus")
                    .Cells(i + 11, 4, i + 11, 5).Merge = True
                    .Cells(i + 11, 4, i + 11, 5).Style.WrapText = True
                    .Cells(i + 11, 4, i + 11, 5).Style.Font.Size = 10
                    .Cells(i + 11, 4, i + 11, 5).Style.Font.Name = "Segoe UI"
                    .Cells(i + 11, 4, i + 11, 5).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 11, 4, i + 11, 5).Style.VerticalAlignment = VertAlignment.Center

                    .Cells(i + 11, 6, i + 11, 6).Value = Rpt.Tables(0).Rows(i)("Item")
                    .Cells(i + 11, 6, i + 11, 8).Merge = True
                    .Cells(i + 11, 6, i + 11, 8).Style.WrapText = True
                    .Cells(i + 11, 6, i + 11, 8).Style.Font.Size = 10
                    .Cells(i + 11, 6, i + 11, 8).Style.Font.Name = "Segoe UI"

                    '    .Cells(i + 12, 4, i + 12, 4).Value = Rpt.Tables(0).Rows(i)("Item")

                    If Rpt.Tables(0).Rows(i)("ValueType") = "N" Then
                        .Cells(i + 11, 9, i + 11, 9).Value = "Numeric"
                    ElseIf Rpt.Tables(0).Rows(i)("ValueType") = "T" Then
                        .Cells(i + 11, 9, i + 11, 9).Value = "Text"
                    End If
                    .Cells(i + 11, 9, i + 11, 10).Merge = True
                    .Cells(i + 11, 9, i + 11, 10).Style.WrapText = True
                    .Cells(i + 11, 9, i + 11, 10).Style.Font.Size = 10
                    .Cells(i + 11, 9, i + 11, 10).Style.Font.Name = "Segoe UI"
                    .Cells(i + 11, 9, i + 11, 10).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 11, 9, i + 11, 10).Style.VerticalAlignment = VertAlignment.Center

                    .Cells(i + 11, 11, i + 11, 11).Value = Rpt.Tables(0).Rows(i)("Standard")
                    .Cells(i + 11, 11, i + 11, 13).Merge = True
                    .Cells(i + 11, 11, i + 11, 13).Style.WrapText = True
                    .Cells(i + 11, 11, i + 11, 13).Style.Font.Size = 10
                    .Cells(i + 11, 11, i + 11, 13).Style.Font.Name = "Segoe UI"
                    .Cells(i + 11, 11, i + 11, 13).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 11, 11, i + 11, 13).Style.VerticalAlignment = VertAlignment.Center

                    .Cells(i + 11, 14, i + 11, 14).Value = Rpt.Tables(0).Rows(i)("Range")
                    .Cells(i + 11, 14, i + 11, 17).Merge = True
                    .Cells(i + 11, 14, i + 11, 17).Style.WrapText = True
                    .Cells(i + 11, 14, i + 11, 17).Style.Font.Size = 10
                    .Cells(i + 11, 14, i + 11, 17).Style.Font.Name = "Segoe UI"
                    .Cells(i + 11, 14, i + 11, 17).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 11, 14, i + 11, 17).Style.VerticalAlignment = VertAlignment.Center

                    .Cells(i + 11, 18, i + 11, 20).Value = Rpt.Tables(0).Rows(i)("MeasuringInstrument")
                    .Cells(i + 11, 18, i + 11, 20).Merge = True
                    .Cells(i + 11, 18, i + 11, 20).Style.WrapText = True
                    .Cells(i + 11, 18, i + 11, 20).Style.Font.Size = 10
                    .Cells(i + 11, 18, i + 11, 20).Style.Font.Name = "Segoe UI"
                    .Cells(i + 11, 18, i + 11, 20).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 11, 18, i + 11, 20).Style.VerticalAlignment = VertAlignment.Center

                    .Cells(i + 11, 21, i + 11, 21).Value = Rpt.Tables(0).Rows(i)("XRCode")
                    .Cells(i + 11, 21, i + 11, 21).Merge = True
                    .Cells(i + 11, 21, i + 11, 21).Style.WrapText = True
                    .Cells(i + 11, 21, i + 11, 21).Style.Font.Size = 10
                    .Cells(i + 11, 21, i + 11, 21).Style.Font.Name = "Segoe UI"
                    .Cells(i + 11, 21, i + 11, 21).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 11, 21, i + 11, 21).Style.VerticalAlignment = VertAlignment.Center
                Next

                FormatExcel(ws, Rpt)
                InsertHeader(ws, pLine, pPart, pPartName, pPrev)
                up_LastRev(ws, pPart)
                up_Approval(ws, pLeaderPIC, pForemanPIC, pQEPIC, pLeaderDate, pForemanDate, pQEDate)
            End With

            exl.Save()
            DevExpress.Web.ASPxWebControl.RedirectOnCallback("Download/" & fi.Name)
        Catch ex As Exception
            pErr = ex.Message
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        End Try
    End Sub

    Private Sub InsertHeader(ByVal pExl As ExcelWorksheet, ByVal pLine As String, ByVal pPart As String, ByVal pPartName As String, ByVal prev As String)
        With pExl
            .Cells(1, 1, 1, 1).Value = "Statistics Quality Control"
            .Cells(1, 1, 1, 21).Merge = True
            .Cells(1, 1, 1, 21).Style.HorizontalAlignment = HorzAlignment.Far
            .Cells(1, 1, 1, 21).Style.VerticalAlignment = VertAlignment.Center
            .Cells(1, 1, 1, 21).Style.Font.Bold = True
            .Cells(1, 1, 1, 21).Style.Font.Size = 16
            .Cells(1, 1, 1, 21).Style.Font.Name = "Segoe UI"

            .Cells(3, 1, 3, 1).Value = "Part"
            .Cells(3, 1, 3, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 1, 3, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 1, 3, 1).Style.Font.Size = 10
            .Cells(3, 1, 3, 1).Style.Font.Name = "Segoe UI"

            .Cells(3, 2, 3, 2).Value = ": " & pPart
            .Cells(3, 2, 3, 2).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 2, 3, 2).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 2, 3, 2).Style.Font.Size = 10
            .Cells(3, 2, 3, 2).Style.Font.Name = "Segoe UI"

            .Cells(4, 1, 4, 1).Value = "Part Name"
            .Cells(4, 1, 4, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 1, 4, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 1, 4, 1).Style.Font.Size = 10
            .Cells(4, 1, 4, 1).Style.Font.Name = "Segoe UI"

            .Cells(4, 2, 4, 2).Value = ": " & pPartName
            .Cells(4, 2, 4, 2).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 2, 4, 2).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 2, 4, 2).Style.Font.Size = 10
            .Cells(4, 2, 4, 2).Style.Font.Name = "Segoe UI"

            .Cells(5, 1, 5, 1).Value = "Line"
            .Cells(5, 1, 5, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(5, 1, 5, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(5, 1, 5, 1).Style.Font.Size = 10
            .Cells(5, 1, 5, 1).Style.Font.Name = "Segoe UI"

            .Cells(5, 2, 5, 2).Value = ": " & pLine
            .Cells(5, 2, 5, 2).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(5, 2, 5, 2).Style.VerticalAlignment = VertAlignment.Center
            .Cells(5, 2, 5, 2).Style.Font.Size = 10
            .Cells(5, 2, 5, 2).Style.Font.Name = "Segoe UI"

            .Cells(6, 1, 6, 1).Value = "Rev. No"
            .Cells(6, 1, 6, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(6, 1, 6, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(6, 1, 6, 1).Style.Font.Size = 10
            .Cells(6, 1, 6, 1).Style.Font.Name = "Segoe UI"

            .Cells(6, 2, 6, 2).Value = ": " & prev
            .Cells(6, 2, 6, 2).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(6, 2, 6, 2).Style.VerticalAlignment = VertAlignment.Center
            .Cells(6, 2, 6, 2).Style.Font.Size = 10
            .Cells(6, 2, 6, 2).Style.Font.Name = "Segoe UI"

            '.Cells(7, 15, 7, 15).Value = "SAFETY SYMBOL : " & txtsafety.Value
            '.Cells(7, 15, 7, 20).Merge = True
            '.Cells(7, 15, 7, 20).Style.HorizontalAlignment = HorzAlignment.Far
            '.Cells(7, 15, 7, 20).Style.VerticalAlignment = VertAlignment.Center
            '.Cells(7, 15, 7, 20).Style.Font.Bold = True
            '.Cells(7, 15, 7, 20).Style.Font.Size = 10
            '.Cells(7, 15, 7, 20).Style.Font.Name = "Segoe UI"
            .Cells(8, 15, 8, 20).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
            .Cells(8, 15, 8, 20).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
            .Cells(8, 15, 8, 20).Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
            .Cells(8, 15, 8, 20).Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
            .Cells(8, 15, 8, 20).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin

            Dim Gambar As DataSet
            Gambar = ClsQCSMasterDB.ExistQCS(cbolineid.Value, cbopartid.Value, cborevno.Value, "")

            If IsDBNull(Gambar.Tables(0).Rows(0)("SafetySymbol1")) Then
            Else
                Dim bytes1 As [Byte]() = Gambar.Tables(0).Rows(0)("SafetySymbol1")
                Dim image1 As Image
                Dim ms1 As New MemoryStream(bytes1)
                image1 = Image.FromStream(ms1)
                Dim excelpicture1 As ExcelPicture
                excelpicture1 = pExl.Drawings.AddPicture("Symbol1", image1)
                excelpicture1.SetSize(30, 30)
                excelpicture1.SetPosition(153, 805)

            End If

            If IsDBNull(Gambar.Tables(0).Rows(0)("SafetySymbol2")) Then
            Else
                Dim bytes2 As [Byte]() = Gambar.Tables(0).Rows(0)("SafetySymbol2")
                Dim ms2 As New MemoryStream(bytes2)
                Dim image2 As Image
                image2 = Image.FromStream(ms2)
                Dim excelpicture2 As ExcelPicture
                excelpicture2 = pExl.Drawings.AddPicture("Symbol2", image2)
                excelpicture2.SetSize(30, 30)
                excelpicture2.SetPosition(153, 862)
            End If

            If IsDBNull(Gambar.Tables(0).Rows(0)("SafetySymbol3")) Then
            Else
                Dim bytes3 As [Byte]() = Gambar.Tables(0).Rows(0)("SafetySymbol3")
                Dim ms3 As New MemoryStream(bytes3)
                Dim image3 As Image
                image3 = Image.FromStream(ms3)
                Dim excelpicture3 As ExcelPicture
                excelpicture3 = pExl.Drawings.AddPicture("Symbol3", image3)
                excelpicture3.SetSize(30, 30)
                excelpicture3.SetPosition(153, 917)
            End If

            If IsDBNull(Gambar.Tables(0).Rows(0)("SafetySymbol4")) Then
            Else
                Dim bytes4 As [Byte]() = Gambar.Tables(0).Rows(0)("SafetySymbol4")
                Dim ms4 As New MemoryStream(bytes4)
                Dim image4 As Image
                image4 = Image.FromStream(ms4)
                Dim excelpicture4 As ExcelPicture
                excelpicture4 = pExl.Drawings.AddPicture("Symbol4", image4)
                excelpicture4.SetSize(30, 30)
                excelpicture4.SetPosition(153, 972)
            End If

            If IsDBNull(Gambar.Tables(0).Rows(0)("SafetySymbol5")) Then
            Else
                Dim bytes5 As [Byte]() = Gambar.Tables(0).Rows(0)("SafetySymbol5")
                Dim ms5 As New MemoryStream(bytes5)
                Dim image5 As Image
                image5 = Image.FromStream(ms5)
                Dim excelpicture5 As ExcelPicture
                excelpicture5 = pExl.Drawings.AddPicture("Symbol5", image5)
                excelpicture5.SetSize(30, 30)
                excelpicture5.SetPosition(153, 1027)
            End If

            If IsDBNull(Gambar.Tables(0).Rows(0)("SafetySymbol6")) Then
            Else
                Dim bytes6 As [Byte]() = Gambar.Tables(0).Rows(0)("SafetySymbol6")
                Dim ms6 As New MemoryStream(bytes6)
                Dim image6 As Image
                image6 = Image.FromStream(ms6)
                Dim excelpicture6 As ExcelPicture
                excelpicture6 = pExl.Drawings.AddPicture("Symbol6", image6)
                excelpicture6.SetSize(30, 30)
                excelpicture6.SetPosition(153, 1082)
            End If

        End With
    End Sub

    Private Sub up_LastRev(ByVal lastrev As ExcelWorksheet, ByVal pPart As String, Optional ByRef pErr As String = "")
        Try

            Dim Rpt As DataSet
            Rpt = ClsQCSMasterDB.GetLast5Rev(cbolineid.Value, pPart, pErr)

            With lastrev
                .Cells(3, 4, 3, 4).Value = "Rev"
                .Cells(3, 4, 3, 4).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(3, 4, 3, 4).Style.VerticalAlignment = VertAlignment.Center
                .Cells(3, 4, 3, 4).Style.Font.Size = 10
                .Cells(3, 4, 3, 4).Style.Font.Name = "Segoe UI"
                .Cells(3, 4, 3, 4).Style.Font.Color.SetColor(Color.White)

                .Cells(3, 5, 3, 5).Value = "Date"
                .Cells(3, 5, 3, 6).Merge = True
                .Cells(3, 5, 3, 6).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(3, 5, 3, 6).Style.VerticalAlignment = VertAlignment.Center
                .Cells(3, 5, 3, 6).Style.Font.Size = 10
                .Cells(3, 5, 3, 6).Style.Font.Name = "Segoe UI"
                .Cells(3, 5, 3, 6).Style.Font.Color.SetColor(Color.White)

                .Cells(3, 7, 3, 7).Value = "Revision History"
                .Cells(3, 7, 3, 9).Merge = True
                .Cells(3, 7, 3, 9).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(3, 7, 3, 9).Style.VerticalAlignment = VertAlignment.Center
                .Cells(3, 7, 3, 9).Style.Font.Size = 10
                .Cells(3, 7, 3, 9).Style.Font.Name = "Segoe UI"
                .Cells(3, 7, 3, 9).Style.Font.Color.SetColor(Color.White)

                .Cells(3, 10, 3, 10).Value = "Prepared By"
                .Cells(3, 10, 3, 11).Merge = True
                .Cells(3, 10, 3, 11).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(3, 10, 3, 11).Style.VerticalAlignment = VertAlignment.Center
                .Cells(3, 10, 3, 11).Style.Font.Size = 10
                .Cells(3, 10, 3, 11).Style.Font.Name = "Segoe UI"
                .Cells(3, 10, 3, 11).Style.Font.Color.SetColor(Color.White)

                '.Cells(3, 12, 3, 12).Value = "Approved By"
                '.Cells(3, 12, 3, 13).Merge = True
                '.Cells(3, 12, 3, 13).Style.HorizontalAlignment = HorzAlignment.Far
                '.Cells(3, 12, 3, 13).Style.VerticalAlignment = VertAlignment.Center
                '.Cells(3, 12, 3, 13).Style.Font.Size = 10
                '.Cells(3, 12, 3, 13).Style.Font.Name = "Segoe UI"
                '.Cells(3, 12, 3, 13).Style.Font.Color.SetColor(Color.White)

                For i = 0 To Rpt.Tables(0).Rows.Count - 1
                    .Cells(i + 4, 4, i + 4, 4).Value = Rpt.Tables(0).Rows(i)("RevNo")
                    .Cells(i + 4, 4, i + 4, 4).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 4, 4, i + 4, 4).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 4, 4, i + 4, 4).Style.WrapText = True
                    .Cells(i + 4, 4, i + 4, 4).Style.Font.Size = 10
                    .Cells(i + 4, 4, i + 4, 4).Style.Font.Name = "Segoe UI"

                    .Cells(i + 4, 5, i + 4, 5).Value = Format(Rpt.Tables(0).Rows(i)("RevDate"), "dd-MM-yyyy")
                    .Cells(i + 4, 5, i + 4, 6).Merge = True
                    .Cells(i + 4, 5, i + 4, 6).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 4, 5, i + 4, 6).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 4, 5, i + 4, 6).Style.WrapText = True
                    .Cells(i + 4, 5, i + 4, 6).Style.Font.Size = 10
                    .Cells(i + 4, 5, i + 4, 6).Style.Font.Name = "Segoe UI"

                    .Cells(i + 4, 7, i + 4, 7).Value = Rpt.Tables(0).Rows(i)("RevHistory")
                    .Cells(i + 4, 7, i + 4, 9).Merge = True
                    .Cells(i + 4, 7, i + 4, 9).Style.WrapText = True
                    .Cells(i + 4, 7, i + 4, 9).Style.Font.Size = 10
                    .Cells(i + 4, 7, i + 4, 9).Style.Font.Name = "Segoe UI"

                    .Cells(i + 4, 10, i + 4, 10).Value = Rpt.Tables(0).Rows(i)("PreparedBy")
                    .Cells(i + 4, 10, i + 4, 11).Merge = True
                    .Cells(i + 4, 10, i + 4, 11).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 4, 10, i + 4, 11).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 4, 10, i + 4, 11).Style.WrapText = True
                    .Cells(i + 4, 10, i + 4, 11).Style.Font.Size = 10
                    .Cells(i + 4, 10, i + 4, 11).Style.Font.Name = "Segoe UI"

                    '.Cells(i + 4, 12, i + 4, 12).Value = Rpt.Tables(0).Rows(i)("ApprovedBy")
                    '.Cells(i + 4, 12, i + 4, 13).Merge = True
                    '.Cells(i + 4, 12, i + 4, 13).Style.HorizontalAlignment = HorzAlignment.Far
                    '.Cells(i + 4, 12, i + 4, 13).Style.VerticalAlignment = VertAlignment.Center
                    '.Cells(i + 4, 12, i + 4, 13).Style.WrapText = True
                    '.Cells(i + 4, 12, i + 4, 13).Style.Font.Size = 10
                    '.Cells(i + 4, 12, i + 4, 13).Style.Font.Name = "Segoe UI"
                Next
                FormatExcelRev(lastrev, Rpt)
            End With
        Catch ex As Exception
            pErr = ex.Message
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        End Try
    End Sub

    Private Sub up_Approval(ByVal approval As ExcelWorksheet, ByVal pLeaderPIC As String, ByVal pForemanPIC As String, ByVal pQEPIC As String, ByVal pLeaderDate As String, ByVal pForemanDate As String, ByVal pQEDate As String, Optional ByRef pErr As String = "")
        Try
            With approval
                .Cells(3, 15, 3, 15).Value = "Approval"
                .Cells(3, 15, 3, 16).Merge = True
                .Cells(3, 15, 3, 16).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(3, 15, 3, 16).Style.VerticalAlignment = VertAlignment.Center
                .Cells(3, 15, 3, 16).Style.WrapText = True
                .Cells(3, 15, 3, 16).Style.Font.Size = 10
                .Cells(3, 15, 3, 16).Style.Font.Name = "Segoe UI"
                .Cells(3, 15, 3, 16).Style.Font.Color.SetColor(Color.White)

                .Cells(4, 15, 4, 15).Value = "Line Leader"
                .Cells(4, 15, 4, 16).Merge = True
                .Cells(4, 15, 4, 16).Style.WrapText = True
                .Cells(4, 15, 4, 16).Style.Font.Size = 10
                .Cells(4, 15, 4, 16).Style.Font.Name = "Segoe UI"

                .Cells(5, 15, 5, 15).Value = "Line Foreman"
                .Cells(5, 15, 5, 16).Merge = True
                .Cells(5, 15, 5, 16).Style.WrapText = True
                .Cells(5, 15, 5, 16).Style.Font.Size = 10
                .Cells(5, 15, 5, 16).Style.Font.Name = "Segoe UI"

                .Cells(6, 15, 6, 15).Value = "QE Leader"
                .Cells(6, 15, 6, 16).Merge = True
                .Cells(6, 15, 6, 16).Style.WrapText = True
                .Cells(6, 15, 6, 16).Style.Font.Size = 10
                .Cells(6, 15, 6, 16).Style.Font.Name = "Segoe UI"

                .Cells(3, 17, 3, 17).Value = "PIC"
                .Cells(3, 17, 3, 18).Merge = True
                .Cells(3, 17, 3, 18).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(3, 17, 3, 18).Style.VerticalAlignment = VertAlignment.Center
                .Cells(3, 17, 3, 18).Style.WrapText = True
                .Cells(3, 17, 3, 18).Style.Font.Size = 10
                .Cells(3, 17, 3, 18).Style.Font.Name = "Segoe UI"
                .Cells(3, 17, 3, 18).Style.Font.Color.SetColor(Color.White)

                .Cells(4, 17, 4, 17).Value = pLeaderPIC
                .Cells(4, 17, 4, 18).Merge = True
                .Cells(4, 17, 4, 18).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(4, 17, 4, 18).Style.VerticalAlignment = VertAlignment.Center
                .Cells(4, 17, 4, 18).Style.WrapText = True
                .Cells(4, 17, 4, 18).Style.Font.Size = 10
                .Cells(4, 17, 4, 18).Style.Font.Name = "Segoe UI"

                .Cells(5, 17, 5, 17).Value = pForemanPIC
                .Cells(5, 17, 5, 18).Merge = True
                .Cells(5, 17, 5, 18).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(5, 17, 5, 18).Style.VerticalAlignment = VertAlignment.Center
                .Cells(5, 17, 5, 18).Style.WrapText = True
                .Cells(5, 17, 5, 18).Style.Font.Size = 10
                .Cells(5, 17, 5, 18).Style.Font.Name = "Segoe UI"

                .Cells(6, 17, 6, 17).Value = pQEPIC
                .Cells(6, 17, 6, 18).Merge = True
                .Cells(6, 17, 6, 18).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 17, 6, 18).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 17, 6, 18).Style.WrapText = True
                .Cells(6, 17, 6, 18).Style.Font.Size = 10
                .Cells(6, 17, 6, 18).Style.Font.Name = "Segoe UI"

                .Cells(3, 19, 3, 19).Value = "Date"
                .Cells(3, 19, 3, 20).Merge = True
                .Cells(3, 19, 3, 20).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(3, 19, 3, 20).Style.VerticalAlignment = VertAlignment.Center
                .Cells(3, 19, 3, 20).Style.WrapText = True
                .Cells(3, 19, 3, 20).Style.Font.Size = 10
                .Cells(3, 19, 3, 20).Style.Font.Name = "Segoe UI"
                .Cells(3, 19, 3, 20).Style.Font.Color.SetColor(Color.White)

                .Cells(4, 19, 4, 19).Value = pLeaderDate
                .Cells(4, 19, 4, 20).Merge = True
                .Cells(4, 19, 4, 20).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(4, 19, 4, 20).Style.VerticalAlignment = VertAlignment.Center
                .Cells(4, 19, 4, 20).Style.WrapText = True
                .Cells(4, 19, 4, 20).Style.Font.Size = 10
                .Cells(4, 19, 4, 20).Style.Font.Name = "Segoe UI"

                .Cells(5, 19, 5, 19).Value = pForemanDate
                .Cells(5, 19, 5, 20).Merge = True
                .Cells(5, 19, 5, 20).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(5, 19, 5, 20).Style.VerticalAlignment = VertAlignment.Center
                .Cells(5, 19, 5, 20).Style.WrapText = True
                .Cells(5, 19, 5, 20).Style.Font.Size = 10
                .Cells(5, 19, 5, 20).Style.Font.Name = "Segoe UI"

                .Cells(6, 19, 6, 19).Value = pQEDate
                .Cells(6, 19, 6, 20).Merge = True
                .Cells(6, 19, 6, 20).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 19, 6, 20).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 19, 6, 20).Style.WrapText = True
                .Cells(6, 19, 6, 20).Style.Font.Size = 10
                .Cells(6, 19, 6, 20).Style.Font.Name = "Segoe UI"

                FormatExcelApproval(approval)
            End With
        Catch ex As Exception
            pErr = ex.Message
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        End Try
    End Sub

    Private Sub FormatExcel(ByVal pExl As ExcelWorksheet, ByVal pRpt As DataSet)
        With pExl
            .Column(1).Width = 10
            .Column(2).Width = 14
            .Column(3).Width = 6
            .Column(4).Width = 6
            .Column(5).Width = 5
            .Column(6).Width = 8
            .Column(7).Width = 8
            .Column(8).Width = 13
            .Column(9).Width = 15
            .Column(10).Width = 5
            .Column(11).Width = 8
            .Column(12).Width = 5
            .Column(13).Width = 8
            .Column(14).Width = 2
            .Column(15).Width = 8
            .Column(16).Width = 8
            .Column(17).Width = 8
            .Column(18).Width = 8
            .Column(19).Width = 8
            .Column(20).Width = 8
            .Column(21).Width = 10

            .Row(7).Height = 10
            .Row(8).Height = 30

            Dim rgAll As ExcelRange = .Cells(10, 1, pRpt.Tables(0).Rows.Count + 10, 21)
            DrawAllBorders(rgAll)

            Dim rgHeader As ExcelRange = .Cells(10, 1, 10, 21)
            rgHeader.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            rgHeader.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
            rgHeader.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            rgHeader.Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
        End With
    End Sub

    Private Sub FormatExcelRev(ByVal pExl As ExcelWorksheet, ByVal pRpt As DataSet)
        With pExl
            Dim rgAll As ExcelRange = .Cells(3, 4, pRpt.Tables(0).Rows.Count + 3, 11)
            DrawAllBorders(rgAll)

            Dim rgHeader As ExcelRange = .Cells(3, 4, 3, 11)
            rgHeader.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            rgHeader.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
            rgHeader.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            rgHeader.Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
        End With
    End Sub

    Private Sub FormatExcelApproval(ByVal pExl As ExcelWorksheet)
        With pExl
            Dim rgAll As ExcelRange = .Cells(3, 15, 3 + 3, 20)
            DrawAllBorders(rgAll)

            Dim rgHeader As ExcelRange = .Cells(3, 15, 3, 20)
            rgHeader.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            rgHeader.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
            rgHeader.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            rgHeader.Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
        End With
    End Sub

    Private Sub DrawAllBorders(ByVal Rg As ExcelRange)
        With Rg
            .Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
            .Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
            .Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
            .Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
            .Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
        End With
    End Sub

#End Region

    Protected Sub btnuploadpopup_Click(sender As Object, e As EventArgs) Handles btnuploadpopup.Click
        Dim QCS As New ClsQCSMaster
        Dim PostedFile1 As HttpPostedFile = uploader1.PostedFile
        Dim PostedFile2 As HttpPostedFile = uploader2.PostedFile
        Dim PostedFile3 As HttpPostedFile = uploader3.PostedFile
        Dim PostedFile4 As HttpPostedFile = uploader4.PostedFile
        Dim PostedFile5 As HttpPostedFile = uploader5.PostedFile
        Dim PostedFile6 As HttpPostedFile = uploader6.PostedFile
        Dim FileName1 As String = ""
        Dim FileName2 As String = ""
        Dim FileName3 As String = ""
        Dim FileName4 As String = ""
        Dim FileName5 As String = ""
        Dim FileName6 As String = ""
        Dim FileExtension1 As String = ""
        Dim FileExtension2 As String = ""
        Dim FileExtension3 As String = ""
        Dim FileExtension4 As String = ""
        Dim FileExtension5 As String = ""
        Dim FileExtension6 As String = ""
        Dim Stream1 As Stream
        Dim Stream2 As Stream
        Dim Stream3 As Stream
        Dim Stream4 As Stream
        Dim Stream5 As Stream
        Dim Stream6 As Stream
        Dim Br1 As BinaryReader
        Dim Br2 As BinaryReader
        Dim Br3 As BinaryReader
        Dim Br4 As BinaryReader
        Dim Br5 As BinaryReader
        Dim Br6 As BinaryReader
        Dim Bytes1 As Byte()
        Dim Bytes2 As Byte()
        Dim Bytes3 As Byte()
        Dim Bytes4 As Byte()
        Dim Bytes5 As Byte()
        Dim Bytes6 As Byte()

        QCS.LineID = cbolineid.Value
        QCS.PartID = cbopartid.Value
        QCS.RevNo = cborevno.Value

        If Not IsNothing(PostedFile1) Then
            FileName1 = Path.GetFileName(PostedFile1.FileName)
            FileExtension1 = Path.GetExtension(FileName1)
            Stream1 = PostedFile1.InputStream
            Br1 = New BinaryReader(Stream1)
            Bytes1 = Br1.ReadBytes(Stream1.Length)

            If Bytes1.Length Then

                QCS.SafetySymbol1 = Bytes1
            End If
            ClsQCSMasterDB.UpdateSymbol1(QCS, "")
        End If

        If Not IsNothing(PostedFile2) Then
            FileName2 = Path.GetFileName(PostedFile2.FileName)
            FileExtension2 = Path.GetExtension(FileName2)
            Stream2 = PostedFile2.InputStream
            Br2 = New BinaryReader(Stream2)
            Bytes2 = Br2.ReadBytes(Stream2.Length)

            If Bytes2.Length Then

                QCS.SafetySymbol2 = Bytes2
            End If
            ClsQCSMasterDB.UpdateSymbol2(QCS, "")
        End If

        If Not IsNothing(PostedFile3) Then
            FileName3 = Path.GetFileName(PostedFile3.FileName)
            FileExtension3 = Path.GetExtension(FileName3)
            Stream3 = PostedFile3.InputStream
            Br3 = New BinaryReader(Stream3)
            Bytes3 = Br3.ReadBytes(Stream3.Length)

            If Bytes3.Length Then

                QCS.SafetySymbol3 = Bytes3
            End If
            ClsQCSMasterDB.UpdateSymbol3(QCS, "")
        End If

        If Not IsNothing(PostedFile4) Then
            FileName4 = Path.GetFileName(PostedFile4.FileName)
            FileExtension4 = Path.GetExtension(FileName4)
            Stream4 = PostedFile4.InputStream
            Br4 = New BinaryReader(Stream4)
            Bytes4 = Br4.ReadBytes(Stream4.Length)

            If Bytes4.Length Then

                QCS.SafetySymbol4 = Bytes4
            End If
            ClsQCSMasterDB.UpdateSymbol4(QCS, "")
        End If

        If Not IsNothing(PostedFile5) Then
            FileName5 = Path.GetFileName(PostedFile5.FileName)
            FileExtension5 = Path.GetExtension(FileName5)
            Stream5 = PostedFile5.InputStream
            Br5 = New BinaryReader(Stream5)
            Bytes5 = Br5.ReadBytes(Stream5.Length)

            If Bytes5.Length Then

                QCS.SafetySymbol5 = Bytes5
            End If
            ClsQCSMasterDB.UpdateSymbol5(QCS, "")
        End If

        If Not IsNothing(PostedFile6) Then
            FileName6 = Path.GetFileName(PostedFile6.FileName)
            FileExtension6 = Path.GetExtension(FileName6)
            Stream6 = PostedFile6.InputStream
            Br6 = New BinaryReader(Stream6)
            Bytes6 = Br6.ReadBytes(Stream6.Length)

            If Bytes6.Length Then

                QCS.SafetySymbol6 = Bytes6
            End If
            ClsQCSMasterDB.UpdateSymbol6(QCS, "")
        End If

        Dim Gambar As DataSet
        Gambar = ClsQCSMasterDB.ExistQCS(cbolineid.Value, cbopartid.Value, cborevno.Value, "")
        If IsDBNull(Gambar.Tables(0).Rows(0)("SafetySymbol1")) Then
        Else
            ASPxImage1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(Gambar.Tables(0).Rows(0)("SafetySymbol1"))
        End If

        If IsDBNull(Gambar.Tables(0).Rows(0)("SafetySymbol2")) Then
        Else
            ASPxImage2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(Gambar.Tables(0).Rows(0)("SafetySymbol2"))
        End If

        If IsDBNull(Gambar.Tables(0).Rows(0)("SafetySymbol3")) Then
        Else
            ASPxImage3.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(Gambar.Tables(0).Rows(0)("SafetySymbol3"))
        End If

        If IsDBNull(Gambar.Tables(0).Rows(0)("SafetySymbol4")) Then
        Else
            ASPxImage4.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(Gambar.Tables(0).Rows(0)("SafetySymbol4"))
        End If

        If IsDBNull(Gambar.Tables(0).Rows(0)("SafetySymbol5")) Then
        Else
            ASPxImage5.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(Gambar.Tables(0).Rows(0)("SafetySymbol5"))
        End If

        If IsDBNull(Gambar.Tables(0).Rows(0)("SafetySymbol6")) Then
        Else
            ASPxImage6.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(Gambar.Tables(0).Rows(0)("SafetySymbol6"))
        End If

        upload_message(MsgTypeEnum.Success, "Upload symbol successful", 1)
    End Sub

    'Protected Sub btnDeleteSymbol1_Click(sender As Object, e As EventArgs) Handles btnDeleteSymbol1.Click
    '    Dim QCS As New ClsQCSMaster
    '    QCS.LineID = cbolineid.Value
    '    QCS.PartID = cbopartid.Value
    '    QCS.RevNo = cborevno.Value
    '    ClsQCSMasterDB.DeleteSymbol1(QCS, "")
    '    upload_message(MsgTypeEnum.Success, "Delete symbol 1 successful", 1)
    'End Sub
End Class