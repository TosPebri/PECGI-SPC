Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.Data
Imports OfficeOpenXml

Public Class TCCSMaster
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
#End Region

#Region "Procedure"
    Private Sub up_fillpart()
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSMasterDB.GetDataPart("")
        cbopartid.DataSource = dsMenu
        cbopartid.DataBind()
    End Sub

    Private Sub up_fillpartpopup()
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSMasterDB.GetDataPartPopup("")
        cbopartidpopup.DataSource = dsMenu
        cbopartidpopup.DataBind()
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        GridMenu.JSProperties("cp_message") = ErrMsg
        GridMenu.JSProperties("cp_type") = msgType
        GridMenu.JSProperties("cp_val") = pVal
    End Sub

    Private Sub up_GridLoadMenu(ByVal pMachineNo As String, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pPartID As String, ByVal pRevNo As String)
        Dim ErrMsg As String = ""
        Dim Menu As DataSet
        Menu = ClsTCCSMasterDetailDB.GetData(pMachineNo, pLineID, pSubLineID, pPartID, pRevNo, ErrMsg)
        If ErrMsg = "" Then
            GridMenu.DataSource = Menu
            GridMenu.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub Approval()
        Dim pErr As String = ""
        Dim TCCS As New ClsTCCSMaster
        TCCS.ApprovalPIC = pUser
        TCCS.MachineNo = cbomachineno.Value
        TCCS.LineID = txtlineid.Value
        TCCS.SubLineID = txtsublineid.Value
        TCCS.PartID = cbopartid.Value
        TCCS.RevNo = cborevno.Value
        ClsTCCSMasterDB.Approval(TCCS, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Approve successfully!", 1)
        End If
    End Sub
#End Region

    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not IsPostBack And Not IsCallback Then
            If Request.QueryString("PartID") Is Nothing Then
                Exit Sub
            Else
                Dim dsMachine As DataTable
                dsMachine = ClsTCCSMasterDB.GetDataMachine(pUser, "")
                cbomachineno.DataSource = dsMachine
                cbomachineno.DataBind()

                Dim dsRev As DataTable
                dsRev = ClsTCCSMasterDB.GetDataRev(Request.QueryString("MachineNo").ToString(), Request.QueryString("LineID").ToString(), Request.QueryString("SUbLineID").ToString(), Request.QueryString("PartID").ToString())
                cborevno.DataSource = dsRev
                cborevno.DataBind()

                cbopartid.Value = Request.QueryString("PartID").ToString()
                txtpartname.Value = Request.QueryString("PartName").ToString()
                cbomachineno.Value = Request.QueryString("MachineNo").ToString()
                txtlineid.Value = Request.QueryString("LineID").ToString()
                txtsublineid.Value = Request.QueryString("SubLineID").ToString()
                cborevno.Value = Request.QueryString("RevNo").ToString()
                up_GridLoadMenu(Request.QueryString("MachineNo").ToString(), Request.QueryString("LineID").ToString(), Request.QueryString("SubLineID").ToString(), Request.QueryString("PartID").ToString(), Request.QueryString("RevNo").ToString())
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "SelectRevNo();", True)
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "C010")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If
        sGlobal.getMenu("C010")
        Master.SiteTitle = sGlobal.menuName

        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "C010")
        Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
        commandColumn.Visible = False
        up_fillpart()
        up_fillpartpopup()
        If AuthUpdate = True Then
            btnNew.ClientEnabled = True
            commandColumn.Visible = True
        End If
    End Sub

#Region "GridMenu"
    Protected Sub GridMenu_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles GridMenu.AfterPerformCallback
        Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            If cborevno.Text = "--New--" Then
                cborevno.Value = 0
            End If
            up_GridLoadMenu(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value)
            'up_GridLoadMenu(IIf(cbolineid.Value = Nothing, "", cbolineid.Value), IIf(cbosublineid.Value = Nothing, "", cbosublineid.Value), IIf(cboprocessid.Value = Nothing, "", cboprocessid.Value), IIf(cbopartid.Value = Nothing, "", cbopartid.Value), IIf(cborevno.Value = Nothing, "", cborevno.Value))
        End If
    End Sub

    Private Sub GridMenu_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridMenu.BeforeGetCallbackResult
        If GridMenu.IsNewRowEditing Then
            GridMenu.SettingsCommandButton.UpdateButton.Text = "Save"
            If cbopartid.Text = "" Then
                GridMenu.JSProperties("cp_alertpartno") = 1
                GridMenu.CancelEdit()
            ElseIf cbomachineno.Text = "" Then
                GridMenu.JSProperties("cp_alertmachineno") = 1
                GridMenu.CancelEdit()
            ElseIf cborevno.Text = "" Then
                GridMenu.JSProperties("cp_alertrevno") = 1
                GridMenu.CancelEdit()
            ElseIf dtrevdate.Text = "" Then
                GridMenu.JSProperties("cp_alertrevdate") = 1
                GridMenu.CancelEdit()
            ElseIf txtrevisionhistory.Text = "" Then
                GridMenu.JSProperties("cp_alertrevhistory") = 1
                GridMenu.CancelEdit()
            ElseIf txtpreparedby.Text = "" Then
                GridMenu.JSProperties("cp_alertpreparedby") = 1
                GridMenu.CancelEdit()
            End If
        ElseIf Not GridMenu.IsNewRowEditing Then
        End If
    End Sub

    Protected Sub GridMenu_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs) Handles GridMenu.CellEditorInitialize
        If Not GridMenu.IsNewRowEditing Then

            If e.Column.FieldName = "ItemID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If

            If e.Column.FieldName = "ProcessID" Then
                If TypeOf e.Editor Is ASPxComboBox Then
                    Dim combo As ASPxComboBox = DirectCast(e.Editor, ASPxComboBox)
                    combo.DataSource = ClsTCCSMasterDB.GetDataProcess(cbomachineno.Value, txtlineid.Value, txtsublineid.Value)
                    combo.DataBindItems()
                End If
            End If

            If e.Column.FieldName = "ProcessName" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If

            If e.Column.FieldName = "ValueType" Then
                If e.Value = "T" Then
                    GridMenu.JSProperties("cp_valuetypet") = 1
                ElseIf e.Value = "N" Then
                    GridMenu.JSProperties("cp_valuetypen") = 1

                End If
            End If
        Else
            If e.Column.FieldName = "ItemID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke

                Dim ErrMsg As String = ""
                Dim Menu As DataSet
                Menu = ClsTCCSMasterDetailDB.GetLastItemID(IIf(cbomachineno.Value = Nothing, "", cbomachineno.Value), IIf(txtlineid.Value = Nothing, "", txtlineid.Value), IIf(txtsublineid.Value = Nothing, "", txtsublineid.Value), IIf(cbopartid.Value = Nothing, "", cbopartid.Value), IIf(cborevno.Text = Nothing Or cborevno.Text = "--New--", "", cborevno.Text), ErrMsg)
                If Menu.Tables(0).Rows(0)("NilMax").ToString = "" Then
                    e.Editor.Value = 1
                Else
                    e.Editor.Value = Val(Menu.Tables(0).Rows(0)("NilMax").ToString()) + 1
                End If

                If ErrMsg = "" Then
                End If
            End If

            If e.Column.FieldName = "ProcessID" Then
                If TypeOf e.Editor Is ASPxComboBox Then
                    Dim combo As ASPxComboBox = DirectCast(e.Editor, ASPxComboBox)
                    combo.DataSource = ClsTCCSMasterDB.GetDataProcess(cbomachineno.Value, txtlineid.Value, txtsublineid.Value)
                    combo.DataBindItems()
                End If
            End If

            If e.Column.FieldName = "ProcessName" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If
        End If
    End Sub

    Private Sub GridMenu_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridMenu.CustomCallback
        Dim pFunction As String = Split(e.Parameters, "|")(0)
        Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)

        Select Case pFunction
            Case "Approve"
                Approval()
                GridMenu.JSProperties("cp_approved") = 1

            Case "ApprovalStatusCheck"
                Dim ErrMsg As String = ""
                Dim Menu As DataSet
                Menu = ClsTCCSMasterDB.GetLastRevApproval(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, ErrMsg)

                If ErrMsg = "" Then
                    If Menu.Tables(0).Rows.Count = 0 Then
                        GridMenu.JSProperties("cp_clicknew") = 1
                        up_GridLoadMenu("", "", "", "", "")
                        commandColumn.Visible = True
                    Else
                        If Menu.Tables(0).Rows(0)("ApprovalStatus") = 0 Then
                            GridMenu.JSProperties("cp_alertunapproved") = 1
                            GridMenu.JSProperties("cp_revno") = Menu.Tables(0).Rows(0)("Revno").ToString
                            Exit Sub
                        Else
                            GridMenu.JSProperties("cp_clicknew") = 1
                            up_GridLoadMenu("", "", "", "", "")
                            commandColumn.Visible = True
                        End If
                    End If
                Else
                    show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
                    Exit Sub
                End If

            Case "Copy"
                Dim pErr As String = ""
                If cbomachinenopopup.Text = "" Then
                    show_error(MsgTypeEnum.Warning, "Please select Machine No!", 1)
                    Exit Sub
                ElseIf cbopartidpopup.Text = "" Then
                    show_error(MsgTypeEnum.Warning, "Please Fill Data Part No!", 1)
                    Exit Sub
                ElseIf cborevnopopup.Text = "" Then
                    show_error(MsgTypeEnum.Warning, "Please Fill Data Rev No!", 1)
                    Exit Sub
                End If

                If ClsTCCSMasterDB.IsExistTCCS(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value) = False Then
                    Dim Menu As DataSet
                    Dim MaxRevNo As Integer
                    Menu = ClsTCCSMasterDB.GetLastRev(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value)
                    If Menu.Tables(0).Rows(0)("NilMax").ToString() = "" Then
                        MaxRevNo = 1
                    Else
                        MaxRevNo = Val(Menu.Tables(0).Rows(0)("NilMax").ToString()) + 1
                    End If

                    Dim TCCS As New ClsTCCSMaster
                    TCCS.MachineNo = cbomachineno.Value
                    TCCS.LineID = txtlineid.Value
                    TCCS.SubLineID = txtsublineid.Value
                    TCCS.PartID = cbopartid.Value
                    TCCS.RevNo = MaxRevNo
                    TCCS.RevDate = dtrevdate.Value
                    TCCS.RevHistory = txtrevisionhistory.Value
                    TCCS.PreparedBy = txtpreparedby.Value
                    TCCS.Remark = txtremark.Value
                    TCCS.CreateUser = pUser

                    Dim TCCSItem As New ClsTCCSMasterDetail
                    TCCSItem.MachineNoCopy = cbomachinenopopup.Value
                    TCCSItem.LineIDCopy = txtlineidpopup.Value
                    TCCSItem.SubLineIDCopy = txtsublineidpopup.Value
                    TCCSItem.PartIDCopy = cbopartidpopup.Value
                    TCCSItem.RevNoCopy = cborevnopopup.Value

                    ClsTCCSMasterDB.InsertCopy(TCCS, TCCSItem, pErr)

                    If pErr <> "" Then
                        show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                    Else
                        show_error(MsgTypeEnum.Success, "Copy data successfully!", 1)
                        GridMenu.JSProperties("cp_aftersave") = "1"
                        GridMenu.JSProperties("cp_revno") = MaxRevNo
                        cborevno.Value = MaxRevNo
                        up_GridLoadMenu(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, MaxRevNo)
                    End If
                Else
                    Dim TCCSItemDelALL As New ClsTCCSMasterDetail
                    TCCSItemDelALL.MachineNo = cbomachineno.Value
                    TCCSItemDelALL.LineID = txtlineid.Value
                    TCCSItemDelALL.SubLineID = txtsublineid.Value
                    TCCSItemDelALL.PartID = cbopartid.Value
                    TCCSItemDelALL.RevNo = cborevno.Value
                    ClsTCCSMasterDetailDB.DeleteAllItem(TCCSItemDelALL, pErr)

                    If pErr <> "" Then
                        show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                    Else
                        Dim TCCSItemCopy As New ClsTCCSMasterDetail

                        TCCSItemCopy.MachineNo = cbomachineno.Value
                        TCCSItemCopy.MachineNoCopy = cbomachinenopopup.Value
                        TCCSItemCopy.PartID = cbopartid.Value
                        TCCSItemCopy.PartIDCopy = cbopartidpopup.Value
                        TCCSItemCopy.RevNo = cborevno.Value
                        TCCSItemCopy.RevNoCopy = cborevnopopup.Value
                        ClsTCCSMasterDetailDB.Copy(TCCSItemCopy, pErr)

                        If pErr <> "" Then
                            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                        Else
                            show_error(MsgTypeEnum.Success, "Copy data successfully!", 1)

                            show_error(MsgTypeEnum.Success, "Copy data successfully!", 1)
                            up_GridLoadMenu(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value)
                        End If
                    End If
                End If

            Case "Delete"
                Dim pErr As String = ""
                Dim TCCSItem As New ClsTCCSMasterDetail
                TCCSItem.MachineNo = cbomachineno.Value
                TCCSItem.LineID = txtlineid.Value
                TCCSItem.SubLineID = txtsublineid.Value
                TCCSItem.PartID = cbopartid.Value
                TCCSItem.RevNo = cborevno.Value
                ClsTCCSMasterDetailDB.DeleteAll(TCCSItem, "")

                If pErr <> "" Then
                    show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                Else
                    show_error(MsgTypeEnum.Success, "Delete data successfully!", 1)
                    GridMenu.JSProperties("cp_clearall") = 1
                    GridMenu.JSProperties("cp_allbtninit") = 1
                End If
                up_GridLoadMenu(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value)

            Case "Excel"

                up_Excel(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value, "")

            Case "GridClear"
                up_GridLoadMenu("", "", "", "", "")

            Case "NewGridVisibleFalse"
                commandColumn.Visible = False

            Case "NewGridVisibleTrue"
                commandColumn.Visible = True

            Case "SelectRevNo"
                Dim ErrMsg As String = ""
                Dim Menu As DataSet
                Menu = ClsTCCSMasterDB.GetData(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value, ErrMsg)

                If ErrMsg = "" Then
                    If Menu.Tables(0).Rows.Count = 0 Then
                    Else
                        GridMenu.JSProperties("cp_fillheader") = 1
                        GridMenu.JSProperties("cp_revdate") = Format(Menu.Tables(0).Rows(0)("RevDate"), "dd MMM yyyy")
                        GridMenu.JSProperties("cp_revhistory") = Menu.Tables(0).Rows(0)("RevHistory").ToString
                        GridMenu.JSProperties("cp_preparedby") = Menu.Tables(0).Rows(0)("PreparedBy").ToString
                        GridMenu.JSProperties("cp_remark") = Menu.Tables(0).Rows(0)("Remark").ToString

                        GridMenu.JSProperties("cp_approvalstatus") = Menu.Tables(0).Rows(0)("ApprovalStatus").ToString
                        If Menu.Tables(0).Rows(0)("ApprovalStatus") = 0 Then
                            GridMenu.JSProperties("cp_approvalpic") = "UNAPPROVED"
                            GridMenu.JSProperties("cp_approvaldate") = "UNAPPROVED"
                            If AuthUpdate = True Then
                                commandColumn.Visible = True
                                GridMenu.JSProperties("cp_authtrue") = 1
                            End If

                            If ClsTCCSMasterDB.GetStatusApproval(pUser, "") = True Then
                                GridMenu.JSProperties("cp_btnapprovetrue") = 1
                            Else
                                GridMenu.JSProperties("cp_btnapprovefalse") = 1
                            End If


                        Else
                            GridMenu.JSProperties("cp_approvalpic") = Menu.Tables(0).Rows(0)("FullName").ToString
                            GridMenu.JSProperties("cp_approvaldate") = Format(Menu.Tables(0).Rows(0)("ApprovalDate"), "dd MMM yyyy")
                            GridMenu.JSProperties("cp_btnapprovefalse") = 1
                            If AuthUpdate = True Then
                                commandColumn.Visible = False
                            End If
                        End If

                        GridMenu.JSProperties("cp_activestatus") = Menu.Tables(0).Rows(0)("ActiveStatus").ToString
                        If Menu.Tables(0).Rows(0)("ActiveStatus") = 0 Then
                            GridMenu.JSProperties("cp_status") = "Inactive"
                        Else
                            GridMenu.JSProperties("cp_status") = "Active"
                        End If

                    End If
                Else
                    show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
                    Exit Sub
                End If
                up_GridLoadMenu(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value)

            Case "Update"
                Dim pErr As String = ""
                If ClsTCCSMasterDB.IsExistTCCS(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value) = True Then
                    Dim TCCSUpdate As New ClsTCCSMaster
                    TCCSUpdate.MachineNo = cbomachineno.Value
                    TCCSUpdate.LineID = txtlineid.Value
                    TCCSUpdate.SubLineID = txtsublineid.Value
                    TCCSUpdate.PartID = cbopartid.Value
                    TCCSUpdate.RevNo = cborevno.Value
                    TCCSUpdate.RevDate = dtrevdate.Value
                    TCCSUpdate.RevHistory = txtrevisionhistory.Value
                    TCCSUpdate.PreparedBy = txtpreparedby.Value
                    TCCSUpdate.Remark = txtremark.Value
                    TCCSUpdate.UpdateUser = pUser

                    ClsTCCSMasterDB.Update(TCCSUpdate, pErr)

                    If pErr <> "" Then
                        show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
                    Else
                        show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
                    End If
                Else
                    show_error(MsgTypeEnum.Warning, "Update data unsuccessfully!", 1)
                End If
        End Select
    End Sub

    Protected Sub GridMenu_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles GridMenu.RowInserting
        e.Cancel = True
        Dim pErr As String = ""

        If ClsTCCSMasterDB.IsExistTCCS(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value) = False Then
            If cbopartid.Text = "" Then
                GridMenu.JSProperties("cp_alertpartno") = 1
                GridMenu.CancelEdit()
            ElseIf cbomachineno.Text = "" Then
                GridMenu.JSProperties("cp_alertmachineno") = 1
                GridMenu.CancelEdit()
            ElseIf cborevno.Text = "" Then
                GridMenu.JSProperties("cp_alertrevno") = 1
                GridMenu.CancelEdit()
            ElseIf dtrevdate.Text = "" Then
                GridMenu.JSProperties("cp_alertrevdate") = 1
                GridMenu.CancelEdit()
            ElseIf txtrevisionhistory.Text = "" Then
                GridMenu.JSProperties("cp_alertrevhistory") = 1
                GridMenu.CancelEdit()
            ElseIf txtpreparedby.Text = "" Then
                GridMenu.JSProperties("cp_alertpreparedby") = 1
                GridMenu.CancelEdit()
            End If

            Dim Menu As DataSet
            Dim MaxRevNo As Integer
            Menu = ClsTCCSMasterDB.GetLastRev(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value)
            If Menu.Tables(0).Rows(0)("NilMax").ToString() = "" Then
                MaxRevNo = 1
            Else
                MaxRevNo = Val(Menu.Tables(0).Rows(0)("NilMax").ToString()) + 1
            End If

            Dim TCCS As New ClsTCCSMaster
            TCCS.MachineNo = cbomachineno.Value
            TCCS.LineID = txtlineid.Value
            TCCS.SubLineID = txtsublineid.Value
            TCCS.PartID = cbopartid.Value
            TCCS.RevNo = MaxRevNo
            TCCS.RevDate = dtrevdate.Value
            TCCS.RevHistory = txtrevisionhistory.Value
            TCCS.PreparedBy = txtpreparedby.Value
            TCCS.Remark = txtremark.Value
            TCCS.CreateUser = pUser

            Dim TCCSDetail As New ClsTCCSMasterDetail With
                {.ItemID = e.NewValues("ItemID"),
                 .SeqNo = e.NewValues("SeqNo"),
                 .ProcessID = e.NewValues("ProcessID"),
                 .KPointStatus = e.NewValues("KPointStatus"),
                 .PICType = e.NewValues("PICType"),
                 .Item = e.NewValues("Item"),
                 .Tools = e.NewValues("Tools"),
                 .Standard = e.NewValues("Standard"),
                 .ValueType = e.NewValues("ValueType"),
                 .NumRangeStart = Val(e.NewValues("NumRangeStart")),
                 .NumRangeEnd = Val(e.NewValues("NumRangeEnd")),
                 .TextRange = e.NewValues("TextRange"),
                 .Remark = e.NewValues("Remark")}

            ClsTCCSMasterDB.InsertHeadDet(TCCS, TCCSDetail, pErr)

            If pErr <> "" Then
                show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
            Else

                show_error(MsgTypeEnum.Success, "Save data successfully!", 1)

                GridMenu.JSProperties("cp_aftersave") = "1"
                GridMenu.JSProperties("cp_revno") = MaxRevNo
                cborevno.Value = MaxRevNo
                up_GridLoadMenu(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, MaxRevNo)
                GridMenu.CancelEdit()
            End If
        ElseIf ClsTCCSMasterDB.IsExistTCCS(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value) = True Then
            Dim TCCSItem As New ClsTCCSMasterDetail With
                    {.MachineNo = cbomachineno.Value,
                     .LineID = txtlineid.Value,
                     .SubLineID = txtsublineid.Value,
                     .PartID = cbopartid.Value,
                     .RevNo = cborevno.Value,
                     .ItemID = e.NewValues("ItemID"),
                     .SeqNo = e.NewValues("SeqNo"),
                     .ProcessID = e.NewValues("ProcessID"),
                     .KPointStatus = e.NewValues("KPointStatus"),
                     .PICType = e.NewValues("PICType"),
                     .Item = e.NewValues("Item"),
                     .Tools = e.NewValues("Tools"),
                     .Standard = e.NewValues("Standard"),
                     .ValueType = e.NewValues("ValueType"),
                     .NumRangeStart = Val(e.NewValues("NumRangeStart")),
                     .NumRangeEnd = Val(e.NewValues("NumRangeEnd")),
                     .TextRange = e.NewValues("TextRange"),
                     .Remark = e.NewValues("Remark")}
            ClsTCCSMasterDetailDB.Insert(TCCSItem, pErr)

            If pErr <> "" Then
                show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
            Else
                GridMenu.CancelEdit()
                show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
                up_GridLoadMenu(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value)
            End If
        End If
    End Sub

    Private Sub GridMenu_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles GridMenu.RowUpdating
        e.Cancel = True
        Dim pErr As String = ""
        Dim TCCSItem As New ClsTCCSMasterDetail With
                    {.MachineNo = cbomachineno.Value,
                     .LineID = txtlineid.Value,
                     .SubLineID = txtsublineid.Value,
                     .PartID = cbopartid.Value,
                     .RevNo = cborevno.Value,
                     .ItemID = e.OldValues("ItemID"),
                     .SeqNo = e.NewValues("SeqNo"),
                     .ProcessID = e.NewValues("ProcessID"),
                     .KPointStatus = e.NewValues("KPointStatus"),
                     .PICType = e.NewValues("PICType"),
                     .Item = e.NewValues("Item"),
                     .Tools = e.NewValues("Tools"),
                     .Standard = e.NewValues("Standard"),
                     .ValueType = e.NewValues("ValueType"),
                     .NumRangeStart = Val(e.NewValues("NumRangeStart")),
                     .NumRangeEnd = Val(e.NewValues("NumRangeEnd")),
                     .TextRange = e.NewValues("TextRange"),
                     .Remark = e.NewValues("Remark")}
        ClsTCCSMasterDetailDB.Update(TCCSItem, pErr)

        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            GridMenu.CancelEdit()
            show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
            up_GridLoadMenu(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value)
        End If
    End Sub

    Protected Sub GridMenu_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles GridMenu.RowDeleting
        e.Cancel = True
        Dim pErr As String = ""
        Dim Item As New ClsTCCSMasterDetail With {.MachineNo = cbomachineno.Value,
                                                  .LineID = txtlineid.Value,
                                                  .SubLineID = txtsublineid.Value,
                                                  .PartID = cbopartid.Value,
                                                  .RevNo = cborevno.Value,
                                                  .ItemID = e.Values("ItemID")}
        ClsTCCSMasterDetailDB.Delete(Item, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            GridMenu.CancelEdit()
            show_error(MsgTypeEnum.Success, "Delete data successfully!", 1)
            up_GridLoadMenu(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value, cborevno.Value)
        End If
    End Sub

    Protected Sub GridMenu_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
        Dim GridData As DevExpress.Web.ASPxGridView.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView.ASPxGridView)

        For Each column As GridViewColumn In GridMenu.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            End If

            If dataColumn.FieldName = "ItemID" Then
                If IsNothing(e.NewValues("ItemID")) OrElse e.NewValues("ItemID").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Item ID!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                End If
            End If

            If dataColumn.FieldName = "SeqNo" Then
                If IsNothing(e.NewValues("SeqNo")) OrElse e.NewValues("SeqNo").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Seq No!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                End If
            End If

            If dataColumn.FieldName = "ProcessID" Then
                If IsNothing(e.NewValues("ProcessID")) OrElse e.NewValues("ProcessID").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Process ID!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                End If
            End If

            If dataColumn.FieldName = "PICType" Then
                If IsNothing(e.NewValues("PICType")) OrElse e.NewValues("PICType").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input PIC!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                End If
            End If

            If dataColumn.FieldName = "Item" Then
                If IsNothing(e.NewValues("Item")) OrElse e.NewValues("Item").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Check Point!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                End If
            End If

            If dataColumn.FieldName = "Tools" Then
                If IsNothing(e.NewValues("Tools")) OrElse e.NewValues("Tools").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Tools!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                End If
            End If

            If dataColumn.FieldName = "ValueType" Then
                If IsNothing(e.NewValues("ValueType")) OrElse e.NewValues("ValueType").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Value Type!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                ElseIf e.NewValues("ValueType").ToString.Trim = "N" Then
                    GridMenu.JSProperties("cp_valuetypen") = 1
                ElseIf e.NewValues("ValueType").ToString.Trim = "T" Then
                    GridMenu.JSProperties("cp_valuetypet") = 1
                End If
            End If

            If dataColumn.FieldName = "Standard" Then
                If IsNothing(e.NewValues("Standard")) OrElse e.NewValues("Standard").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Standard!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                End If
            End If

            If dataColumn.FieldName = "NumRangeEnd" Then
                If IsNothing(e.NewValues("ValueType")) OrElse e.NewValues("ValueType").ToString.Trim = "" Then
                ElseIf e.NewValues("ValueType").ToString.Trim = "N" Then
                    If e.NewValues("NumRangeStart") > e.NewValues("NumRangeEnd") Then
                        e.Errors(dataColumn) = "End Range cannot less than Start Range!"
                        show_error(MsgTypeEnum.Warning, "End Range cannot less than Start Range!", 1)

                    End If
                End If
            End If

            If dataColumn.FieldName = "TextRange" Then
                If IsNothing(e.NewValues("ValueType")) OrElse e.NewValues("ValueType").ToString.Trim = "" Then
                ElseIf e.NewValues("ValueType").ToString.Trim = "T" Then
                        If IsNothing(e.NewValues("TextRange")) OrElse e.NewValues("TextRange").ToString.Trim = "" Then
                        e.Errors(dataColumn) = "Please input Text Range!"
                        show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                        End If
                    End If
                End If
        Next column

        'For Each column As GridViewColumn In GridMenu.Columns
        '    Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
        '    If dataColumn Is Nothing Then
        '        Continue For
        '    End If

        '    If dataColumn.FieldName = "ItemID" Then
        '        If IsNothing(e.NewValues("ItemID")) OrElse e.NewValues("ItemID").ToString.Trim = "" Then
        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
        '            Exit Sub
        '        End If
        '    End If

        '    If dataColumn.FieldName = "ProcessID" Then
        '        If IsNothing(e.NewValues("ProcessID")) OrElse e.NewValues("ProcessID").ToString.Trim = "" Then
        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
        '            Exit Sub
        '        End If
        '    End If

        '    If dataColumn.FieldName = "PICType" Then
        '        If IsNothing(e.NewValues("PICType")) OrElse e.NewValues("PICType").ToString.Trim = "" Then
        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
        '            Exit Sub
        '        End If
        '    End If

        '    If dataColumn.FieldName = "Item" Then
        '        If IsNothing(e.NewValues("Item")) OrElse e.NewValues("Item").ToString.Trim = "" Then
        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
        '            Exit Sub
        '        End If
        '    End If

        '    If dataColumn.FieldName = "Tools" Then
        '        If IsNothing(e.NewValues("Tools")) OrElse e.NewValues("Tools").ToString.Trim = "" Then
        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
        '            Exit Sub
        '        End If
        '    End If

        '    If dataColumn.FieldName = "ValueType" Then
        '        If IsNothing(e.NewValues("ValueType")) OrElse e.NewValues("ValueType").ToString.Trim = "" Then
        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
        '        ElseIf e.NewValues("ValueType").ToString.Trim = "N" Then
        '            GridMenu.JSProperties("cp_valuetypen") = 1
        '        ElseIf e.NewValues("ValueType").ToString.Trim = "T" Then
        '            GridMenu.JSProperties("cp_valuetypet") = 1
        '            Exit Sub
        '        End If
        '    End If

        '    If dataColumn.FieldName = "Standard" Then
        '        If IsNothing(e.NewValues("Standard")) OrElse e.NewValues("Standard").ToString.Trim = "" Then
        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
        '            Exit Sub
        '        End If
        '    End If

        '    If dataColumn.FieldName = "NumRangeEnd" Then
        '        If e.NewValues("ValueType").ToString.Trim = "N" Then
        '            If IsNothing(e.NewValues("ValueType")) OrElse e.NewValues("ValueType").ToString.Trim = "" Then
        '            ElseIf e.NewValues("NumRangeStart") > e.NewValues("NumRangeEnd") Then
        '                show_error(MsgTypeEnum.Warning, "End Range cannot less than Start Range!", 1)
        '                Exit Sub
        '            End If
        '        End If
        '    End If

        '    If dataColumn.FieldName = "TextRange" Then
        '        If e.NewValues("ValueType").ToString.Trim = "T" Then
        '            If IsNothing(e.NewValues("ValueType")) OrElse e.NewValues("ValueType").ToString.Trim = "" Then
        '            ElseIf IsNothing(e.NewValues("TextRange")) OrElse e.NewValues("TextRange").ToString.Trim = "" Then
        '                show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
        '                Exit Sub
        '            End If
        '        End If
        '    End If
        'Next column
    End Sub

    Protected Sub GridMenu_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles GridMenu.StartRowEditing
        'If (Not GridMenu.IsNewRowEditing) Then
        '    GridMenu.DoRowValidation()
        'End If
        'show_error(MsgTypeEnum.Info, "", 0)
    End Sub
#End Region

#Region "Callback"
    Private Sub cbomachineno_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cbomachineno.Callback
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSMasterDB.GetDataMachine(pUser, "")
        cbomachineno.DataSource = dsMenu
        cbomachineno.DataBind()
    End Sub

    Private Sub cborevno_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cborevno.Callback
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSMasterDB.GetDataRev(cbomachineno.Value, txtlineid.Value, txtsublineid.Value, cbopartid.Value)
        cborevno.DataSource = dsMenu
        cborevno.DataBind()
    End Sub

    Private Sub cbopartid_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cbopartid.Callback
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSMasterDB.GetDataPart("")
        cbopartid.DataSource = dsMenu
        cbopartid.DataBind()
    End Sub

    Private Sub cbomachinenopopup_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cbomachinenopopup.Callback
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSMasterDB.GetDataMachinePopUp(cbopartidpopup.Value, "")
        cbomachinenopopup.DataSource = dsMenu
        cbomachinenopopup.DataBind()
    End Sub

    Private Sub cborevnopopup_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cborevnopopup.Callback
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSMasterDB.GetDataRevPopUp(cbomachinenopopup.Value, txtlineidpopup.Value, txtsublineidpopup.Value, cbopartidpopup.Value)
        cborevnopopup.DataSource = dsMenu
        cborevnopopup.DataBind()
    End Sub

    'Private Sub cbosublineidpopup_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cbosublineidpopup.Callback
    '    Dim dsMenu As DataTable
    '    dsMenu = ClsTCCSMasterDB.GetDataSubLine(cbolineidpopup.Value, "")
    '    cbosublineidpopup.DataSource = dsMenu
    '    cbosublineidpopup.DataBind()
    'End Sub

    'Private Sub cboprocessidpopup_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cboprocessidpopup.Callback
    '    Dim dsMenu As DataTable
    '    dsMenu = ClsTCCSMasterDB.GetDataProcess(cbolineidpopup.Value, cbosublineidpopup.Value, "")
    '    cboprocessidpopup.DataSource = dsMenu
    '    cboprocessidpopup.DataBind()
    'End Sub

    'Private Sub cbopartidpopup_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cbopartidpopup.Callback
    '    Dim dsMenu As DataTable
    '    dsMenu = ClsTCCSMasterDB.GetDataPart("")
    '    cbopartidpopup.DataSource = dsMenu
    '    cbopartidpopup.DataBind()
    'End Sub

    'Private Sub cborevnopopup_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cborevnopopup.Callback
    '    Dim dsMenu As DataTable
    '    dsMenu = ClsTCCSMasterDB.GetDataRev(cbolineidpopup.Value, cbosublineidpopup.Value, cboprocessidpopup.Value, cbopartidpopup.Value)
    '    cborevnopopup.DataSource = dsMenu
    '    cborevnopopup.DataBind()
    'End Sub
#End Region

#Region "Download To Excel"

    Private Sub up_Excel(ByVal pMachineNo As String, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef pErr As String = "")
        Try
            Dim fi As New FileInfo(Server.MapPath("~\Download\Type Change Check Sheet Master.xlsx"))
            If fi.Exists Then
                fi.Delete()
                fi = New FileInfo(Server.MapPath("~\Download\Type Change Check Sheet Master.xlsx"))
            End If
            Dim exl As New ExcelPackage(fi)
            Dim ws As ExcelWorksheet
            ws = exl.Workbook.Worksheets.Add("Sheet1")
            ws.View.ShowGridLines = False

            Dim Rpt As DataSet
            Rpt = ClsTCCSMasterDetailDB.GetData(pMachineNo, pLineID, pSubLineID, pPartID, pRevNo, pErr)

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

                .Cells(10, 6, 10, 6).Value = "PIC (OPR / QA)"
                .Cells(10, 6, 10, 7).Merge = True
                .Cells(10, 6, 10, 7).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 6, 10, 7).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 6, 10, 7).Style.Font.Size = 10
                .Cells(10, 6, 10, 7).Style.Font.Name = "Segoe UI"
                .Cells(10, 6, 10, 7).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 8, 10, 8).Value = "Check Point"
                .Cells(10, 8, 10, 9).Merge = True
                .Cells(10, 8, 10, 9).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 8, 10, 9).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 8, 10, 9).Style.Font.Size = 10
                .Cells(10, 8, 10, 9).Style.Font.Name = "Segoe UI"
                .Cells(10, 8, 10, 9).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 10, 10, 10).Value = "Tools"
                .Cells(10, 10, 10, 13).Merge = True
                .Cells(10, 10, 10, 13).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 10, 10, 13).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 10, 10, 13).Style.Font.Size = 10
                .Cells(10, 10, 10, 13).Style.Font.Name = "Segoe UI"
                .Cells(10, 10, 10, 13).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 14, 10, 14).Value = "Standard"
                .Cells(10, 14, 10, 17).Merge = True
                .Cells(10, 14, 10, 17).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 14, 10, 17).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 14, 10, 17).Style.Font.Size = 10
                .Cells(10, 14, 10, 17).Style.Font.Name = "Segoe UI"
                .Cells(10, 14, 10, 17).Style.Font.Color.SetColor(Color.White)

                '.Cells(10, 16, 10, 16).Value = "Standard"
                '.Cells(10, 16, 10, 17).Merge = True
                '.Cells(10, 16, 10, 17).Style.HorizontalAlignment = HorzAlignment.Far
                '.Cells(10, 16, 10, 17).Style.VerticalAlignment = VertAlignment.Center
                '.Cells(10, 16, 10, 17).Style.Font.Size = 10
                '.Cells(10, 16, 10, 17).Style.Font.Name = "Segoe UI"
                '.Cells(10, 16, 10, 17).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 18, 10, 18).Value = "Range"
                .Cells(10, 18, 10, 19).Merge = True
                .Cells(10, 18, 10, 19).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 18, 10, 19).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 18, 10, 19).Style.Font.Size = 10
                .Cells(10, 18, 10, 19).Style.Font.Name = "Segoe UI"
                .Cells(10, 18, 10, 19).Style.Font.Color.SetColor(Color.White)

                .Cells(10, 20, 10, 20).Value = "Remark"
                .Cells(10, 20, 10, 21).Merge = True
                .Cells(10, 20, 10, 21).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(10, 20, 10, 21).Style.VerticalAlignment = VertAlignment.Center
                .Cells(10, 20, 10, 21).Style.Font.Size = 10
                .Cells(10, 20, 10, 21).Style.Font.Name = "Segoe UI"
                .Cells(10, 20, 10, 21).Style.Font.Color.SetColor(Color.White)

                For i = 0 To Rpt.Tables(0).Rows.Count - 1
                    .Cells(i + 11, 1, i + 11, 1).Value = Rpt.Tables(0).Rows(i)("Number")
                    .Cells(i + 11, 1, i + 11, 1).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 11, 1, i + 11, 1).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 11, 1, i + 11, 1).Style.WrapText = True
                    .Cells(i + 11, 1, i + 11, 1).Style.Font.Size = 10
                    .Cells(i + 11, 1, i + 11, 1).Style.Font.Name = "Segoe UI"

                    .Cells(i + 11, 2, i + 11, 2).Value = Rpt.Tables(0).Rows(i)("ProcessName")
                    .Cells(i + 11, 2, i + 11, 3).Style.WrapText = True
                    .Cells(i + 11, 2, i + 11, 3).Merge = True
                    .Cells(i + 11, 2, i + 11, 3).Style.Font.Size = 10
                    .Cells(i + 11, 2, i + 11, 3).Style.Font.Name = "Segoe UI"

                    .Cells(i + 11, 4, i + 11, 4).Value = Rpt.Tables(0).Rows(i)("KPointStatus")
                    .Cells(i + 11, 4, i + 11, 5).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 11, 4, i + 11, 5).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 11, 4, i + 11, 5).Style.WrapText = True
                    .Cells(i + 11, 4, i + 11, 5).Merge = True
                    .Cells(i + 11, 4, i + 11, 5).Style.Font.Size = 10
                    .Cells(i + 11, 4, i + 11, 5).Style.Font.Name = "Segoe UI"

                    .Cells(i + 11, 6, i + 11, 6).Value = Rpt.Tables(0).Rows(i)("PICType")
                    .Cells(i + 11, 6, i + 11, 7).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 11, 6, i + 11, 7).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 11, 6, i + 11, 7).Style.WrapText = True
                    .Cells(i + 11, 6, i + 11, 7).Merge = True
                    .Cells(i + 11, 6, i + 11, 7).Style.Font.Size = 10
                    .Cells(i + 11, 6, i + 11, 7).Style.Font.Name = "Segoe UI"

                    .Cells(i + 11, 8, i + 11, 8).Value = Rpt.Tables(0).Rows(i)("Item")
                    .Cells(i + 11, 8, i + 11, 9).Style.WrapText = True
                    .Cells(i + 11, 8, i + 11, 9).Merge = True
                    .Cells(i + 11, 8, i + 11, 9).Style.Font.Size = 10
                    .Cells(i + 11, 8, i + 11, 9).Style.Font.Name = "Segoe UI"

                    .Cells(i + 11, 10, i + 11, 10).Value = Rpt.Tables(0).Rows(i)("Tools")
                    .Cells(i + 11, 10, i + 11, 13).Style.WrapText = True
                    .Cells(i + 11, 10, i + 11, 13).Merge = True
                    .Cells(i + 11, 10, i + 11, 13).Style.Font.Size = 10
                    .Cells(i + 11, 10, i + 11, 13).Style.Font.Name = "Segoe UI"

                    .Cells(i + 11, 14, i + 11, 14).Value = Rpt.Tables(0).Rows(i)("Standard")
                    .Cells(i + 11, 14, i + 11, 17).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 11, 14, i + 11, 17).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 11, 14, i + 11, 17).Style.WrapText = True
                    .Cells(i + 11, 14, i + 11, 17).Merge = True
                    .Cells(i + 11, 14, i + 11, 17).Style.Font.Size = 10
                    .Cells(i + 11, 14, i + 11, 17).Style.Font.Name = "Segoe UI"

                    '.Cells(i + 11, 16, i + 11, 16).Value = Rpt.Tables(0).Rows(i)("Standard")
                    '.Cells(i + 11, 16, i + 11, 17).Style.HorizontalAlignment = HorzAlignment.Far
                    '.Cells(i + 11, 16, i + 11, 17).Style.VerticalAlignment = VertAlignment.Center
                    '.Cells(i + 11, 16, i + 11, 17).Style.WrapText = True
                    '.Cells(i + 11, 16, i + 11, 17).Merge = True
                    '.Cells(i + 11, 16, i + 11, 17).Style.Font.Size = 10
                    '.Cells(i + 11, 16, i + 11, 17).Style.Font.Name = "Segoe UI"

                    .Cells(i + 11, 18, i + 11, 18).Value = Rpt.Tables(0).Rows(i)("Range")
                    .Cells(i + 11, 18, i + 11, 19).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 11, 18, i + 11, 19).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 11, 18, i + 11, 19).Style.WrapText = True
                    .Cells(i + 11, 18, i + 11, 19).Merge = True
                    .Cells(i + 11, 18, i + 11, 19).Style.Font.Size = 10
                    .Cells(i + 11, 18, i + 11, 19).Style.Font.Name = "Segoe UI"

                    .Cells(i + 11, 20, i + 11, 20).Value = Rpt.Tables(0).Rows(i)("Remark")
                    .Cells(i + 11, 20, i + 11, 21).Style.WrapText = True
                    .Cells(i + 11, 20, i + 11, 21).Merge = True
                    .Cells(i + 11, 20, i + 11, 21).Style.Font.Size = 10
                    .Cells(i + 11, 20, i + 11, 21).Style.Font.Name = "Segoe UI"
                Next

                FormatExcel(ws, Rpt)
                InsertHeader(ws)
                up_LastRev(ws)
                up_Approval(ws)
            End With

            exl.Save()
            DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("Download/" & fi.Name)
        Catch ex As Exception
            pErr = ex.Message
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        End Try
    End Sub

    Private Sub InsertHeader(ByVal pExl As ExcelWorksheet)
        With pExl
            .Cells(1, 1, 1, 1).Value = "Type Change Check Sheet Master"
            .Cells(1, 1, 1, 21).Merge = True
            .Cells(1, 1, 1, 21).Style.HorizontalAlignment = HorzAlignment.Far
            .Cells(1, 1, 1, 21).Style.VerticalAlignment = VertAlignment.Center
            .Cells(1, 1, 1, 21).Style.Font.Bold = True
            .Cells(1, 1, 1, 21).Style.Font.Size = 16
            .Cells(1, 1, 1, 21).Style.Font.Name = "Segoe UI"

            .Cells(3, 1, 3, 1).Value = "Part No"
            .Cells(3, 1, 3, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 1, 3, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 1, 3, 1).Style.Font.Size = 10
            .Cells(3, 1, 3, 1).Style.Font.Name = "Segoe UI"

            .Cells(3, 2, 3, 2).Value = ": " & cbopartid.Value
            .Cells(3, 2, 3, 2).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 2, 3, 2).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 2, 3, 2).Style.Font.Size = 10
            .Cells(3, 2, 3, 2).Style.Font.Name = "Segoe UI"

            .Cells(4, 1, 4, 1).Value = "Part Name"
            .Cells(4, 1, 4, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 1, 4, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 1, 4, 1).Style.Font.Size = 10
            .Cells(4, 1, 4, 1).Style.Font.Name = "Segoe UI"

            .Cells(4, 2, 4, 2).Value = ": " & txtpartname.Value
            .Cells(4, 2, 4, 2).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 2, 4, 2).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 2, 4, 2).Style.Font.Size = 10
            .Cells(4, 2, 4, 2).Style.Font.Name = "Segoe UI"

            .Cells(5, 1, 5, 1).Value = "Machine"
            .Cells(5, 1, 5, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(5, 1, 5, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(5, 1, 5, 1).Style.Font.Size = 10
            .Cells(5, 1, 5, 1).Style.Font.Name = "Segoe UI"

            .Cells(5, 2, 5, 2).Value = ": " & cbomachineno.Value
            .Cells(5, 2, 5, 2).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(5, 2, 5, 2).Style.VerticalAlignment = VertAlignment.Center
            .Cells(5, 2, 5, 2).Style.Font.Size = 10
            .Cells(5, 2, 5, 2).Style.Font.Name = "Segoe UI"

            .Cells(6, 1, 6, 1).Value = "Line"
            .Cells(6, 1, 6, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(6, 1, 6, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(6, 1, 6, 1).Style.Font.Size = 10
            .Cells(6, 1, 6, 1).Style.Font.Name = "Segoe UI"

            .Cells(6, 2, 6, 2).Value = ": " & txtlineid.Value
            .Cells(6, 2, 6, 2).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(6, 2, 6, 2).Style.VerticalAlignment = VertAlignment.Center
            .Cells(6, 2, 6, 2).Style.Font.Size = 10
            .Cells(6, 2, 6, 2).Style.Font.Name = "Segoe UI"

            .Cells(7, 1, 7, 1).Value = "Sub Line"
            .Cells(7, 1, 7, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(7, 1, 7, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(7, 1, 7, 1).Style.Font.Size = 10
            .Cells(7, 1, 7, 1).Style.Font.Name = "Segoe UI"

            .Cells(7, 2, 7, 2).Value = ": " & txtsublineid.Value
            .Cells(7, 2, 7, 2).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(7, 2, 7, 2).Style.VerticalAlignment = VertAlignment.Center
            .Cells(7, 2, 7, 2).Style.Font.Size = 10
            .Cells(7, 2, 7, 2).Style.Font.Name = "Segoe UI"

            .Cells(8, 1, 8, 1).Value = "Rev No"
            .Cells(8, 1, 8, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(8, 1, 8, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(8, 1, 8, 1).Style.Font.Size = 10
            .Cells(8, 1, 8, 1).Style.Font.Name = "Segoe UI"

            .Cells(8, 2, 8, 2).Value = ": " & cborevno.Value
            .Cells(8, 2, 8, 2).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(8, 2, 8, 2).Style.VerticalAlignment = VertAlignment.Center
            .Cells(8, 2, 8, 2).Style.Font.Size = 10
            .Cells(8, 2, 8, 2).Style.Font.Name = "Segoe UI"

        End With
    End Sub

    Private Sub up_LastRev(ByVal lastrev As ExcelWorksheet, Optional ByRef pErr As String = "")
        Try

            Dim Rpt As DataSet
            Rpt = ClsTCCSMasterDB.GetLast5Rev(cbopartid.Value, cbomachineno.Value, txtlineid.Value, txtsublineid.Value, pErr)

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
                Next
                FormatExcelRev(lastrev, Rpt)
            End With
        Catch ex As Exception
            pErr = ex.Message
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        End Try
    End Sub

    Private Sub up_Approval(ByVal approval As ExcelWorksheet, Optional ByRef pErr As String = "")
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

                .Cells(4, 15, 4, 15).Value = "Prod. Sec. Head"
                .Cells(4, 15, 4, 16).Merge = True
                .Cells(4, 15, 4, 16).Style.WrapText = True
                .Cells(4, 15, 4, 16).Style.Font.Size = 10
                .Cells(4, 15, 4, 16).Style.Font.Name = "Segoe UI"

                .Cells(3, 17, 3, 17).Value = "PIC"
                .Cells(3, 17, 3, 18).Merge = True
                .Cells(3, 17, 3, 18).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(3, 17, 3, 18).Style.VerticalAlignment = VertAlignment.Center
                .Cells(3, 17, 3, 18).Style.WrapText = True
                .Cells(3, 17, 3, 18).Style.Font.Size = 10
                .Cells(3, 17, 3, 18).Style.Font.Name = "Segoe UI"
                .Cells(3, 17, 3, 18).Style.Font.Color.SetColor(Color.White)

                .Cells(4, 17, 4, 17).Value = lblsectheadpic.Value
                .Cells(4, 17, 4, 18).Merge = True
                .Cells(4, 17, 4, 18).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(4, 17, 4, 18).Style.VerticalAlignment = VertAlignment.Center
                .Cells(4, 17, 4, 18).Style.WrapText = True
                .Cells(4, 17, 4, 18).Style.Font.Size = 10
                .Cells(4, 17, 4, 18).Style.Font.Name = "Segoe UI"

                .Cells(3, 19, 3, 19).Value = "Date"
                .Cells(3, 19, 3, 20).Merge = True
                .Cells(3, 19, 3, 20).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(3, 19, 3, 20).Style.VerticalAlignment = VertAlignment.Center
                .Cells(3, 19, 3, 20).Style.WrapText = True
                .Cells(3, 19, 3, 20).Style.Font.Size = 10
                .Cells(3, 19, 3, 20).Style.Font.Name = "Segoe UI"
                .Cells(3, 19, 3, 20).Style.Font.Color.SetColor(Color.White)

                .Cells(4, 19, 4, 19).Value = lblsectheaddate.Value
                .Cells(4, 19, 4, 20).Merge = True
                .Cells(4, 19, 4, 20).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(4, 19, 4, 20).Style.VerticalAlignment = VertAlignment.Center
                .Cells(4, 19, 4, 20).Style.WrapText = True
                .Cells(4, 19, 4, 20).Style.Font.Size = 10
                .Cells(4, 19, 4, 20).Style.Font.Name = "Segoe UI"

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
            Dim rgAll As ExcelRange = .Cells(3, 15, 1 + 3, 20)
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
End Class