Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data

Public Class Machine
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
        Dim Machine As New List(Of ClsMachine)
        Machine = ClsMachineDB.GetData(ErrMsg)
        If ErrMsg = "" Then
            GridMenu.DataSource = Machine
            GridMenu.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
        End If
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        GridMenu.JSProperties("cp_message") = ErrMsg
        GridMenu.JSProperties("cp_type") = msgType
        GridMenu.JSProperties("cp_val") = pVal
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "A030")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If
        sGlobal.getMenu("A030")
        Master.SiteTitle = sGlobal.menuName
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "A030")
        show_error(MsgTypeEnum.Info, "", 0)
        If AuthUpdate = False Then
            Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
            commandColumn.Visible = False
        End If
        up_GridLoad()
    End Sub

#Region "Control Event"
    Protected Sub GridMenu_AfterPerformCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles GridMenu.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" Then
            up_GridLoad()
        End If
    End Sub

    Protected Sub GridMenu_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles GridMenu.RowInserting
        e.Cancel = True
        Dim pErr As String = ""
        Dim Machine As New ClsMachine With {.MachineNo = e.NewValues("MachineNo"),
                                            .Remark = e.NewValues("Remark"),
                                            .CreateUser = pUser}
        ClsMachineDB.Insert(Machine, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            GridMenu.CancelEdit()
            show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
            up_GridLoad()
        End If
    End Sub

    Protected Sub GridMenu_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles GridMenu.RowUpdating
        e.Cancel = True
        Dim pErr As String = ""
        Dim Machine As New ClsMachine With {.MachineNo = e.OldValues("MachineNo"),
                                            .Remark = e.NewValues("Remark"),
                                            .CreateUser = pUser}
        ClsMachineDB.Update(Machine, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            GridMenu.CancelEdit()
            show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
            up_GridLoad()
        End If
    End Sub

    Protected Sub GridMenu_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles GridMenu.RowDeleting
        e.Cancel = True
        Dim pErr As String = ""
        Dim Machine As New ClsMachine With {.MachineNo = e.Values("MachineNo")}
        If ClsMachineDB.IsExistDel(e.Values("MachineNo"), "") Then
            show_error(MsgTypeEnum.Warning, "Machine No cannot be deleted because it is being used in the other process", 1)
        Else
            ClsMachineDB.Delete(Machine, pErr)
            If pErr <> "" Then
                show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
            Else
                GridMenu.CancelEdit()
                show_error(MsgTypeEnum.Success, "Delete data successfully!", 1)
                up_GridLoad()
            End If
        End If

    End Sub

    Private Sub GridMenu_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridMenu.BeforeGetCallbackResult
        If GridMenu.IsNewRowEditing Then
            GridMenu.SettingsCommandButton.UpdateButton.Text = "Save"
        End If
    End Sub

    Private Sub GridMenu_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles GridMenu.CellEditorInitialize
        If Not GridMenu.IsNewRowEditing Then
            If e.Column.FieldName = "MachineNo" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Silver
            End If
        End If
    End Sub

    Protected Sub GridMenu_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles GridMenu.RowValidating
        Dim GridData As DevExpress.Web.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView)

        For Each column As GridViewColumn In GridMenu.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            End If

            If dataColumn.FieldName = "MachineNo" Then
                If IsNothing(e.NewValues("MachineNo")) OrElse e.NewValues("MachineNo").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Machine No!"
                    show_error(MsgTypeEnum.Warning, "Please input Machine No!", 1)
                Else
                    If e.IsNewRow Then
                        If ClsMachineDB.IsExist(e.NewValues("MachineNo"), "") Then
                            e.Errors(dataColumn) = "Machine No is already exist!"
                            show_error(MsgTypeEnum.Warning, "Machine No is already exist!", 1)
                            Exit Sub
                        End If
                    End If
                End If
            End If
        Next column
    End Sub

    Protected Sub GridMenu_StartRowEditing(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles GridMenu.StartRowEditing
        'If (Not Grid.IsNewRowEditing) Then
        '    Grid.DoRowValidation()
        'End If
        'show_error(MsgTypeEnum.Info, "", 0)
    End Sub

#End Region

End Class