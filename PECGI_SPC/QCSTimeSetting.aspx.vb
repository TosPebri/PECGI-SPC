Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DevExpress.Data

Public Class QCSTimeSetting
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "D010")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If

        sGlobal.getMenu("D010")
        Master.SiteTitle = sGlobal.menuName

        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "D010")
        show_error(MsgTypeEnum.Info, "", 0)
        If AuthUpdate = False Then
            btnSave.Visible = False
            Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
            commandColumn.Visible = False
        ElseIf ClsTimeSettingDB.isExist("") Then
            Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
            commandColumn.Visible = False
        End If
        GridMenu.Settings.ShowStatusBar = GridViewStatusBarMode.Hidden
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            up_GridLoadMenu()
        End If
    End Sub

    Private Sub up_GridLoadMenu()
        Dim ErrMsg As String = ""
        Dim Menu As DataSet
        Menu = ClsTimeSettingDB.GetList(ErrMsg)
        If ErrMsg = "" Then
            GridMenu.DataSource = Menu
            GridMenu.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If

    End Sub

    Private Sub DisableEdit()

         

    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        GridMenu.JSProperties("cp_message") = ErrMsg
        GridMenu.JSProperties("cp_type") = msgType
        GridMenu.JSProperties("cp_val") = pVal
    End Sub

    Protected Sub GridMenu_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs) Handles GridMenu.RowInserting
        e.Cancel = True
        Dim pErr As String = ""
        Dim Time As New ClsTimeSetting With {.Shift1Cycle1 = e.NewValues("Shift1Cycle1"),
                                            .Shift1Cycle2 = e.NewValues("Shift1Cycle2"),
                                            .Shift1Cycle3 = e.NewValues("Shift1Cycle3"),
                                            .Shift1Cycle4 = e.NewValues("Shift1Cycle4"),
                                            .Shift1Cycle5 = e.NewValues("Shift1Cycle5"),
                                            .Shift2Cycle1 = e.NewValues("Shift2Cycle1"),
                                            .Shift2Cycle2 = e.NewValues("Shift2Cycle2"),
                                            .Shift2Cycle3 = e.NewValues("Shift2Cycle3"),
                                            .Shift2Cycle4 = e.NewValues("Shift2Cycle4"),
                                            .Shift2Cycle5 = e.NewValues("Shift2Cycle5"),
                                            .Shift3Cycle1 = e.NewValues("Shift3Cycle1"),
                                            .Shift3Cycle2 = e.NewValues("Shift3Cycle2"),
                                            .Shift3Cycle3 = e.NewValues("Shift3Cycle3"),
                                            .Shift3Cycle4 = e.NewValues("Shift3Cycle4"),
                                            .Shift3Cycle5 = e.NewValues("Shift3Cycle5"),
                                            .CreateUser = pUser}
        ClsTimeSettingDB.Insert(Time, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            GridMenu.CancelEdit()
            show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
            up_GridLoadMenu()
            Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
            commandColumn.Visible = False
        End If
    End Sub

    Protected Sub GridMenu_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles GridMenu.RowUpdating
        e.Cancel = True
        Dim pErr As String = ""
        Dim loc As New ClsTimeSetting With {.Shift1Cycle1 = e.NewValues("Shift1Cycle1"),
                                            .Shift1Cycle2 = e.NewValues("Shift1Cycle2"),
                                            .Shift1Cycle3 = e.NewValues("Shift1Cycle3"),
                                            .Shift1Cycle4 = e.NewValues("Shift1Cycle4"),
                                            .Shift1Cycle5 = e.NewValues("Shift1Cycle5"),
                                            .Shift2Cycle1 = e.NewValues("Shift2Cycle1"),
                                            .Shift2Cycle2 = e.NewValues("Shift2Cycle2"),
                                            .Shift2Cycle3 = e.NewValues("Shift2Cycle3"),
                                            .Shift2Cycle4 = e.NewValues("Shift2Cycle4"),
                                            .Shift2Cycle5 = e.NewValues("Shift2Cycle5"),
                                            .Shift3Cycle1 = e.NewValues("Shift3Cycle1"),
                                            .Shift3Cycle2 = e.NewValues("Shift3Cycle2"),
                                            .Shift3Cycle3 = e.NewValues("Shift3Cycle3"),
                                            .Shift3Cycle4 = e.NewValues("Shift3Cycle4"),
                                            .Shift3Cycle5 = e.NewValues("Shift3Cycle5"),
                                            .UpdateUser = pUser}
        ClsTimeSettingDB.Update(loc, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            GridMenu.CancelEdit()
            show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
            up_GridLoadMenu()
        End If
    End Sub

    Protected Sub GridMenu_RowDeleting(sender As Object, e As ASPxDataDeletingEventArgs) Handles GridMenu.RowDeleting
        e.Cancel = True
    End Sub
End Class