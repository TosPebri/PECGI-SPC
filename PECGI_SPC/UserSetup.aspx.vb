Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data

Public Class UserSetup
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
#End Region

#Region "Procedure"
    Private Sub up_GridLoad()
        Dim Users As List(Of clsUserSetup)
        Try
            Users = clsUserSetupDB.GetList()
            Grid.DataSource = Users
            Grid.DataBind()
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        Grid.JSProperties("cp_message") = ErrMsg
        Grid.JSProperties("cp_type") = msgType
        Grid.JSProperties("cp_val") = pVal
    End Sub
#End Region

#Region "Initialization"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            up_GridLoad()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sGlobal.getMenu("Z010")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user")
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "A010")
        show_error(MsgTypeEnum.Info, "", 0)
        If AuthUpdate = False Then
            Dim commandColumn = TryCast(Grid.Columns(0), GridViewCommandColumn)
            commandColumn.Visible = False
        End If
    End Sub
#End Region

#Region "Control Event"
    Protected Sub Grid_AfterPerformCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles Grid.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" Then
            up_GridLoad()
        End If
    End Sub

    Protected Sub Grid_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles Grid.RowInserting
        e.Cancel = True
        Dim pErr As String = ""
        Dim User As New clsUserSetup With {
            .UserID = e.NewValues("UserID") & "",
            .FullName = e.NewValues("FullName") & "",
            .Password = e.NewValues("Password") & "",
            .AdminStatus = If(e.NewValues("AdminStatus") = "Yes", "1", "0"),
            .Description = e.NewValues("Description") & "",
            .FactoryCode = e.NewValues("FactoryCode"),
            .JobPosition = e.NewValues("JobPosition"),
            .Email = e.NewValues("Email"),
            .LockStatus = e.NewValues("LockStatus"),
            .CreateUser = pUser
        }
        Try
            Dim CheckUser As clsUserSetup = clsUserSetupDB.GetData(User.UserID)
            If CheckUser IsNot Nothing Then
                show_error(MsgTypeEnum.ErrorMsg, "User already exists!", 1)
                Return
            End If
            pErr = clsUserSetupDB.Insert(User)
            If pErr = "" Then
                show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
                Grid.CancelEdit()
                up_GridLoad()
            Else
                show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
            End If
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Protected Sub Grid_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles Grid.RowUpdating
        e.Cancel = True
        Dim User As New clsUserSetup With {
            .UserID = e.OldValues("UserID"),
            .FullName = e.NewValues("FullName") & "",
            .Password = e.NewValues("Password"),
            .AdminStatus = If(e.NewValues("AdminStatus") = "Yes", "1", "0"),
            .Description = e.NewValues("Description") & "",
            .FactoryCode = e.NewValues("FactoryCode"),
            .JobPosition = e.NewValues("JobPosition") & "",
            .Email = e.NewValues("Email"),
            .LockStatus = e.NewValues("LockStatus"),
            .UpdateUser = pUser
        }
        Try
            clsUserSetupDB.Update(User)
            Grid.CancelEdit()
            up_GridLoad()
            show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Protected Sub Grid_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles Grid.RowDeleting
        e.Cancel = True
        Try
            Dim UserID As String = e.Values("UserID")
            clsUserSetupDB.Delete(UserID)
            Grid.CancelEdit()
            up_GridLoad()
            show_error(MsgTypeEnum.Success, "Delete data successfully!", 1)            
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Sub Grid_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.BeforeGetCallbackResult
        If Grid.IsNewRowEditing Then
            Grid.SettingsCommandButton.UpdateButton.Text = "Save"
        End If
    End Sub

    Private Sub Grid_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles Grid.CellEditorInitialize
        If Not Grid.IsNewRowEditing Then
            If e.Column.FieldName = "UserID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Silver
            End If
        End If
    End Sub

    Protected Sub Grid_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles Grid.RowValidating
        Dim GridData As DevExpress.Web.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView)
        Dim AdaError As Boolean = False
        Dim Password As String = ""
        Dim ConfirmPassword As String = ""
        For Each column As GridViewColumn In Grid.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            End If

            If dataColumn.FieldName = "UserID" Then
                If IsNothing(e.NewValues("UserID")) OrElse e.NewValues("UserID").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input User ID!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                Else
                    If e.IsNewRow Then
                        Dim User As clsUserSetup = clsUserSetupDB.GetData(e.NewValues("UserID"))
                        If User IsNot Nothing Then
                            e.Errors(dataColumn) = "User ID already exists!"
                            show_error(MsgTypeEnum.Warning, e.Errors(dataColumn), 1)
                            AdaError = True
                        End If
                    End If
                End If
            End If

            If dataColumn.FieldName = "Password" Then
                If IsNothing(e.NewValues("Password")) OrElse e.NewValues("Password").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Password!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If
            Password = e.NewValues("Password")
            If dataColumn.FieldName = "ConfirmPassword" Then
                If IsNothing(e.NewValues("ConfirmPassword")) OrElse e.NewValues("ConfirmPassword").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Confirm Password!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If
            ConfirmPassword = e.NewValues("Password")
        Next column

        If Not AdaError And Password <> ConfirmPassword Then
            show_error(MsgTypeEnum.Warning, "Password does not match!", 1)
        End If
    End Sub

    Protected Sub Grid_StartRowEditing(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles Grid.StartRowEditing
        If (Not Grid.IsNewRowEditing) Then
            Grid.DoRowValidation()
        End If        
        show_error(MsgTypeEnum.Info, "", 0)
    End Sub

    Protected Sub PrivilegesLink_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim link As DevExpress.Web.ASPxHyperLink = CType(sender, DevExpress.Web.ASPxHyperLink)
        Dim templatecontainer As GridViewDataItemTemplateContainer = CType(link.NamingContainer, GridViewDataItemTemplateContainer)
        link.NavigateUrl = "javascript:void(0)"
        link.ForeColor = Color.SteelBlue

        Dim UserID As String = ""
        UserID = templatecontainer.Grid.GetRowValues(templatecontainer.VisibleIndex, "UserID") & ""
        If UserID <> "" Then
            link.ClientSideEvents.Click = "function (s,e) {window.open('UserPrivilege.aspx?prm=" + UserID + "', '_self'); }"
        End If
    End Sub

    Protected Sub LinePrivilegesLink_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim link As DevExpress.Web.ASPxHyperLink = CType(sender, DevExpress.Web.ASPxHyperLink)
        Dim templatecontainer As GridViewDataItemTemplateContainer = CType(link.NamingContainer, GridViewDataItemTemplateContainer)
        link.NavigateUrl = "javascript:void(0)"
        link.ForeColor = Color.SteelBlue

        Dim UserID As String = ""
        UserID = templatecontainer.Grid.GetRowValues(templatecontainer.VisibleIndex, "UserID") & ""
        If UserID <> "" Then
            link.ClientSideEvents.Click = "function (s,e) {window.open('UserLine.aspx?prm=" + UserID + "', 'ModalPopUp', 'height=600,width=960,left=200,top=10'); }"
        End If
    End Sub
#End Region
End Class