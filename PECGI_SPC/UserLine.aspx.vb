Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data

Public Class UserLine
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim RegisterUser As String = ""
    Dim UserID As String
#End Region

#Region "Procedure"
    Private Sub up_GridLoad(ByVal pUserID As String)
        Dim dsMenu As List(Of clsUserLine)
        dsMenu = clsUserLineDB.GetList(pUserID)
        gridMenu.DataSource = dsMenu
        gridMenu.DataBind()
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, Optional ByVal pVal As Integer = 1)
        gridMenu.JSProperties("cp_message") = ErrMsg
        gridMenu.JSProperties("cp_type") = msgType
        gridMenu.JSProperties("cp_val") = pVal
    End Sub
#End Region

#Region "Initialization"
    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        sGlobal.getMenu("Z040")
        Master.SiteTitle = sGlobal.menuName
        show_error(MsgTypeEnum.Info, "", 0)
        RegisterUser = Session("user")
        If Request.QueryString("prm") Is Nothing Then
            UserID = RegisterUser
            btnCancel.Visible = False
            Exit Sub
        Else
            btnCancel.Visible = True
            UserID = Request.QueryString("prm").ToString()
        End If

        If Request.QueryString("prm") Is Nothing Then
            Exit Sub
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
        Dim ls_AllowShow As String = ""
        Dim ls_AllowUpdate As String = ""
        Dim ls_AllowVerify As String = ""
        Dim LineID As String = ""

        Dim a As Integer = e.UpdateValues.Count
        For iLoop = 0 To a - 1
            ls_AllowShow = If(e.UpdateValues(iLoop).NewValues("AllowShow").ToString() = True, "1", "0")
            ls_AllowUpdate = If(e.UpdateValues(iLoop).NewValues("AllowUpdate").ToString() = True, "1", "0")
            ls_AllowVerify = If(e.UpdateValues(iLoop).NewValues("AllowVerify").ToString() = True, "1", "0")

            LineID = Trim(e.UpdateValues(iLoop).NewValues("LineID").ToString())
            Dim UserLine As New clsUserLine With {
                .UserID = UserID,
                .LineID = LineID,
                .AllowShow = ls_AllowShow,
                .AllowUpdate = ls_AllowUpdate,
                .AllowVerify = ls_AllowVerify,
                .RegisterUser = RegisterUser
            }
            Dim pErr As String = ""
            Dim iUpd As Integer = clsUserLineDB.InsertUpdate(UserLine)
            If pErr <> "" Then
                Exit For
            End If
        Next iLoop
        gridMenu.EndUpdate()
    End Sub

    Private Sub gridMenu_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles gridMenu.CustomCallback
        Dim pAction As String = Split(e.Parameters, "|")(0)
        Dim pUserID As String = Split(e.Parameters, "|")(1)
        up_GridLoad(pUserID)
        If pAction = "save" Then
            show_error(MsgTypeEnum.Success, "Update data successful", 1)
        End If
    End Sub

    Private Sub cbkValid_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkValid.Callback
        Dim pAction As String = Split(e.Parameter, "|")(0)
        Dim FromUserID As String = Split(e.Parameter, "|")(1)
        Dim TouserID As String = Split(e.Parameter, "|")(2)
        If FromUserID <> "" Then
            clsUserLineDB.Copy(FromUserID, TouserID, RegisterUser)
        End If
    End Sub
#End Region
End Class