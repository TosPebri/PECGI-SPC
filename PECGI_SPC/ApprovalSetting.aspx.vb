Imports DevExpress.Web

Public Class ApprovalSetting
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
#End Region

#Region "Initialization"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then


            UP_FillComboQELeader()
            UP_FillComboQESectionHead()
            UP_FillCombo()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sGlobal.getMenu("D020 ")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user")
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "D020 ")
        show_error(MsgTypeEnum.Info, "", 0)
        If AuthUpdate = False Then
            btnSave.Visible = False
        End If
        cboqeleader1.Items.Insert(0, New ListEditItem("", ""))
        cboqeleader2.Items.Insert(0, New ListEditItem("", ""))
        cbosectionhead1.Items.Insert(0, New ListEditItem("", ""))
        cbosectionhead2.Items.Insert(0, New ListEditItem("", ""))
    End Sub
#End Region

#Region "Procedure"
    Private Sub UP_FillComboQELeader()
        Dim ErrMsg As String = ""
        Dim dsline As DataTable
        dsline = ClsApprovalSettingDB.GetDataQELeader(ErrMsg)
        If ErrMsg = "" Then
            
            cboqeleader1.DataSource = dsline
            cboqeleader1.DataBind()
            cboqeleader2.DataSource = dsline
            cboqeleader2.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub UP_FillComboQESectionHead()
        Dim ErrMsg As String = ""
        Dim dsline As DataTable
        dsline = ClsApprovalSettingDB.GetDataQESectionHead(ErrMsg)
        If ErrMsg = "" Then
            cbosectionhead1.DataSource = dsline
            cbosectionhead1.DataBind()
            cbosectionhead2.DataSource = dsline
            cbosectionhead2.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub UP_FillCombo()
        Dim ErrMsg As String = ""
        Dim dsline As DataSet
        dsline = ClsApprovalSettingDB.GetData(ErrMsg)
        If ErrMsg = "" Then
            If IsNothing(dsline) Then
                cboqeleader1.Text = ""
                cboqeleader2.Text = ""
                cbosectionhead1.Text = ""
                cbosectionhead2.Text = ""
            Else
                cboqeleader1.Text = dsline.Tables(0).Rows(0)("QELeader1").ToString
                cboqeleader2.Text = dsline.Tables(0).Rows(0)("QELeader2").ToString
                cbosectionhead1.Text = dsline.Tables(0).Rows(0)("QESectionHead1").ToString
                cbosectionhead2.Text = dsline.Tables(0).Rows(0)("QESectionHead2").ToString
            End If
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, Optional ByVal pVal As Integer = 1)
        cbSave.JSProperties("cp_message") = ErrMsg
        cbSave.JSProperties("cp_type") = msgType
        cbSave.JSProperties("cp_val") = pVal
    End Sub
#End Region

#Region "Control Event"
    Private Sub cbSave_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbSave.Callback
        Dim pErr As String = ""

        If ClsApprovalSettingDB.isExist("") Then
            Dim ApprovalSetup As New ClsApprovalSetting
            ApprovalSetup.QELeader1 = cboqeleader1.Text
            ApprovalSetup.QELeader2 = cboqeleader2.Text
            ApprovalSetup.QESectionHead1 = cbosectionhead1.Text
            ApprovalSetup.QESectionHead2 = cbosectionhead2.Text
            ApprovalSetup.UpdateUser = pUser

            ClsApprovalSettingDB.Update(ApprovalSetup, pErr)

            If pErr <> "" Then
                show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
            Else
                show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
                UP_FillCombo()
            End If
        Else
            Dim ApprovalSetup As New ClsApprovalSetting
            ApprovalSetup.QELeader1 = cboqeleader1.Text
            ApprovalSetup.QELeader2 = cboqeleader2.Text
            ApprovalSetup.QESectionHead1 = cbosectionhead1.Text
            ApprovalSetup.QESectionHead2 = cbosectionhead2.Text
            ApprovalSetup.CreateUser = pUser

            ClsApprovalSettingDB.Insert(ApprovalSetup, pErr)

            If pErr <> "" Then
                show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
            Else
                show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
            End If
        End If
    End Sub


#End Region


End Class