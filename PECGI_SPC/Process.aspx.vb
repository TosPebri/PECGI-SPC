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

Public Class Process
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
#End Region

#Region "Procedure"
    Private Sub up_GridLoad()
        Dim ErrMsg As String = ""
        Dim Pro As New List(Of ClsProcess)
        Pro = ClsProcessDB.GetList(ErrMsg)
        If ErrMsg = "" Then
            Grid.DataSource = Pro
            Grid.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
        End If
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
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "A010")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If
        sGlobal.getMenu("A010")
        Master.SiteTitle = sGlobal.menuName
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "A010")
        show_error(MsgTypeEnum.Info, "", 0)
        If AuthUpdate = False Then
            Dim commandColumn = TryCast(Grid.Columns(0), GridViewCommandColumn)
            commandColumn.Visible = False
        End If
    End Sub
#End Region

#Region "Control Event"
    Protected Sub Grid_AfterPerformCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles Grid.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" Then
            up_GridLoad()
        End If
    End Sub

    Protected Sub Grid_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles Grid.RowInserting
        e.Cancel = True
        Dim pErr As String = ""
        Dim Pro As New ClsProcess With {.ProcessID = e.NewValues("ProcessID"),
                                         .ProcessName = e.NewValues("ProcessName"),
                                         .Remark = e.NewValues("Remark"),
                                         .CreateUser = pUser}
        ClsProcessDB.Insert(Pro, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            Grid.CancelEdit()
            show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
            up_GridLoad()
        End If
    End Sub

    Protected Sub Grid_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles Grid.RowUpdating
        e.Cancel = True
        Dim pErr As String = ""
        Dim loc As New ClsProcess With {.ProcessID = e.OldValues("ProcessID"),
                                         .ProcessName = e.NewValues("ProcessName"),
                                         .Remark = e.NewValues("Remark"),
                                         .UpdateUser = pUser}
        ClsProcessDB.Update(loc, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            Grid.CancelEdit()
            show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
            up_GridLoad()
        End If
    End Sub

    Protected Sub Grid_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles Grid.RowDeleting
        e.Cancel = True
        Dim pErr As String = ""
        Dim staff As New ClsProcess With {.ProcessID = e.Values("ProcessID")}
        If ClsProcessDB.isExistDel(e.Values("ProcessID"), "") Then
            show_error(MsgTypeEnum.Warning, "Process ID cannot be delete because it is being used in the Sub Line", 1)
        Else
            ClsProcessDB.Delete(staff, pErr)
            If pErr <> "" Then
                show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
            Else
                Grid.CancelEdit()
                show_error(MsgTypeEnum.Success, "Delete data successfully!", 1)
                up_GridLoad()
            End If
        End If
    End Sub

    Private Sub Grid_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.BeforeGetCallbackResult
        If Grid.IsNewRowEditing Then
            Grid.SettingsCommandButton.UpdateButton.Text = "Save"
        End If
    End Sub

    Private Sub Grid_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs) Handles Grid.CellEditorInitialize
        If Not Grid.IsNewRowEditing Then
            If e.Column.FieldName = "ProcessID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Silver
            End If
        End If
    End Sub

    Protected Sub Grid_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles Grid.RowValidating
        Dim GridData As DevExpress.Web.ASPxGridView.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView.ASPxGridView)

        For Each column As GridViewColumn In Grid.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            End If

            If dataColumn.FieldName = "ProcessID" Then
                If IsNothing(e.NewValues("ProcessID")) OrElse e.NewValues("ProcessID").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Process ID!"
                Else
                    If e.IsNewRow Then
                        If ClsProcessDB.isExist(e.NewValues("ProcessID"), "") Then
                            e.Errors(dataColumn) = "Process ID is already exist!"
                            show_error(MsgTypeEnum.Warning, e.Errors(dataColumn), 1)
                            Exit Sub
                        End If
                    End If
                End If
            End If

            If dataColumn.FieldName = "ProcessName" Then
                If IsNothing(e.NewValues("ProcessName")) OrElse e.NewValues("ProcessName").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Process Name!"
                    'Exit Sub
                End If
            End If

            If dataColumn.FieldName = "Remark" Then
                If IsNothing(e.NewValues("Remark")) OrElse e.NewValues("Remark").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Remark!"
                    'Exit Sub
                End If
            End If
        Next column

        For Each column As GridViewColumn In Grid.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            End If

            If dataColumn.FieldName = "ProcessID" Then
                If IsNothing(e.NewValues("ProcessID")) OrElse e.NewValues("ProcessID").ToString.Trim = "" Then
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    Exit Sub
                End If
            End If

            If dataColumn.FieldName = "ProcessName" Then
                If IsNothing(e.NewValues("ProcessName")) OrElse e.NewValues("ProcessName").ToString.Trim = "" Then
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    Exit Sub
                End If
            End If

            If dataColumn.FieldName = "Remark" Then
                If IsNothing(e.NewValues("Remark")) OrElse e.NewValues("Remark").ToString.Trim = "" Then
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    Exit Sub
                End If
            End If
        Next column

    End Sub

    Protected Sub Grid_StartRowEditing(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles Grid.StartRowEditing
        If (Not Grid.IsNewRowEditing) Then
            Grid.DoRowValidation()
        End If
        show_error(MsgTypeEnum.Info, "", 0)
    End Sub
#End Region
End Class