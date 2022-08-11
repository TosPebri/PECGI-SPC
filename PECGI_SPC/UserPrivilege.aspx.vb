Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data
Public Class UserPrivilege
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim RegisterUser As String = ""
    Dim UserID As String = ""

    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
#End Region

#Region "Procedure"
    Private Sub up_GridLoad(ByVal pUserID As String)
        Dim pErr As String = ""
        Dim dsMenu As List(Of Cls_ss_UserMenu)
        dsMenu = clsUserSetupDB.GetListMenu(pUserID, pErr)
        If pErr = "" Then
            gridMenu.DataSource = dsMenu
            gridMenu.DataBind()
            If dsMenu Is Nothing Then
                show_error(MsgTypeEnum.Warning, "Data is not found!")
            End If
        Else
            show_error(MsgTypeEnum.ErrorMsg, pErr)
        End If
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, Optional ByVal pVal As Integer = 1)
        gridMenu.JSProperties("cp_message") = ErrMsg
        gridMenu.JSProperties("cp_type") = msgType
        gridMenu.JSProperties("cp_val") = pVal
    End Sub
#End Region

#Region "Initialization"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        sGlobal.getMenu("Z020")
        Master.SiteTitle = sGlobal.menuName
        show_error(MsgTypeEnum.Info, "", 0)
        RegisterUser = Session("user")

        AuthAccess = sGlobal.Auth_UserAccess(RegisterUser, "Z020")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If

        AuthUpdate = sGlobal.Auth_UserUpdate(RegisterUser, "Z020")
        If AuthUpdate = False Then
            btnSave.Enabled = False
        End If

        AuthDelete = sGlobal.Auth_UserDelete(RegisterUser, "Z020")
        If AuthDelete = False Then
            btnSave.Enabled = False
        End If

        If Request.QueryString("prm") Is Nothing Then
            UserID = RegisterUser
            btnCancel.Visible = False
            Exit Sub
        Else
            btnCancel.Visible = True
            UserID = Request.QueryString("prm").ToString()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            up_GridLoad(UserID)
        End If
        txtUser.Text = UserID
    End Sub
#End Region

#Region "Control Event"
    Private Sub gridMenu_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs) Handles gridMenu.BatchUpdate
        Dim ls_AllowAccess As String = "", ls_AllowUpdate As String = "", ls_AllowSpecial As String = "", ls_Active As String = ""
        Dim MenuID As String = "", ls_AllowDelete As String = ""

        Dim a As Integer = e.UpdateValues.Count
        For iLoop = 0 To a - 1
            Try
                ls_AllowAccess = (e.UpdateValues(iLoop).NewValues("AllowAccess").ToString())
                ls_AllowUpdate = (e.UpdateValues(iLoop).NewValues("AllowUpdate").ToString())
                ls_AllowDelete = (e.UpdateValues(iLoop).NewValues("AllowDelete").ToString())

                If ls_AllowAccess = True Then ls_AllowAccess = "1" Else ls_AllowAccess = "0"
                If ls_AllowUpdate = True Then ls_AllowUpdate = "1" Else ls_AllowUpdate = "0"
                If ls_AllowDelete = True Then ls_AllowDelete = "1" Else ls_AllowDelete = "0"

                MenuID = Trim(e.UpdateValues(iLoop).NewValues("MenuID").ToString())
                Dim UserPrevilege As New Cls_ss_UserPrivilege With {
                    .AppID = "SPC",
                    .UserID = UserID,
                    .MenuID = MenuID,
                    .AllowAccess = ls_AllowAccess,
                    .AllowUpdate = ls_AllowUpdate,
                    .AllowDelete = ls_AllowDelete,
                    .RegisterUser = RegisterUser
                }
                Dim pErr As String = ""
                pErr  = Cls_ss_UserPrivilegeDB.Save(UserPrevilege, pErr)
                If pErr <> "" Then
                    Exit For
                End If
            Catch ex As Exception
                Throw ex
            End Try
        Next iLoop
        gridMenu.EndUpdate()
    End Sub

    Private Sub gridMenu_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles gridMenu.CustomCallback
        Dim pAction As String = Split(e.Parameters, "|")(0)
        Dim sUserID As String = Split(e.Parameters, "|")(1)
        'Dim pAction As String = e.Parameters
        'Dim stxtUser As String = txtUser.Text
        'Dim scboUser As String = cboUser.Text

        up_GridLoad(sUserID)
        If pAction = "save" Then
            show_error(MsgTypeEnum.Success, "Update data successful", 1)
        End If

        'If pAction = "save" Then
        '    show_error(MsgTypeEnum.Success, "Update data successful", 1)
        'ElseIf pAction = "loadtxtUser" Then
        '    up_GridLoad(stxtUser)
        'ElseIf pAction = "loadcboUser" Then
        '    up_GridLoad(scboUser)
        'End If
    End Sub

    Private Sub cbkValid_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkValid.Callback
        Dim pAction As String = Split(e.Parameter, "|")(0)
        Dim FromUserID As String = Split(e.Parameter, "|")(1)
        Dim TouserID As String = Split(e.Parameter, "|")(2)
        'Dim pAction As String = e.Parameter
        'Dim FromUserID As String = cboUser.Text
        'Dim TouserID As String = txtUser.Text
        If FromUserID <> "" Then
            Cls_ss_UserPrivilegeDB.Copy(FromUserID, TouserID, RegisterUser)
        End If
    End Sub
#End Region
End Class