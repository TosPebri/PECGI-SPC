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

Public Class Line
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
        Dim Pro As New List(Of ClsLine)
        Pro = ClsLineDB.GetList(ErrMsg)
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
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "A020")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If

        sGlobal.getMenu("A020")
        Master.SiteTitle = sGlobal.menuName
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "A020")
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
        Dim Line As New ClsLine With {.LineID = e.NewValues("LineID"),
                                      .LineName = e.NewValues("LineName"),
                                      .EZRLineID = e.NewValues("EZRLineID"),
                                         .CreateUser = pUser}
        ClsLineDB.Insert(Line, pErr)
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
        Dim line As New ClsLine With {.LineID = e.OldValues("LineID"),
                                      .LineName = e.NewValues("LineName"),
                                      .EZRLineID = e.NewValues("EZRLineID"),
                                         .UpdateUser = pUser}
        ClsLineDB.Update(line, pErr)
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
        Dim Line As New ClsLine With {.LineID = e.Values("LineID")}
        If ClsLineDB.isExistDel(e.Values("LineID"), "") Then
            show_error(MsgTypeEnum.Warning, "Line ID cannot be deleted because it is being used in the other process", 1)
        Else
            ClsLineDB.Delete(Line, pErr)
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
            If e.Column.FieldName = "LineID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Silver
            End If
        End If

        Dim tab1 As Int16 = 1
        Dim tab2 As Int16 = 2
        Dim tab3 As Int16 = 3
        'Dim tab4 As Int16 = 4
        'Dim tab5 As Int16 = 5
        'Dim tab6 As Int16 = 6
        'Dim tab7 As Int16 = 7
        'Dim tab8 As Int16 = 8
        'Dim tab9 As Int16 = 9
        'Dim tab10 As Int16 = 10
        'Dim tab11 As Int16 = 11


        If e.Column.FieldName = "LineID" Then
            e.Editor.TabIndex = tab1
        End If
        If e.Column.FieldName = "LineName" Then
            e.Editor.TabIndex = tab2
        End If
        If e.Column.FieldName = "EZRLineID" Then
            e.Editor.TabIndex = tab3
        End If
        'If e.Column.FieldName = "Leader1" Then
        '    e.Editor.TabIndex = tab4
        'End If
        'If e.Column.FieldName = "Leader2" Then
        '    e.Editor.TabIndex = tab5
        'End If
        'If e.Column.FieldName = "Leader3" Then
        '    e.Editor.TabIndex = tab6
        'End If
        'If e.Column.FieldName = "Foreman1" Then
        '    e.Editor.TabIndex = tab7
        'End If
        'If e.Column.FieldName = "Foreman2" Then
        '    e.Editor.TabIndex = tab8
        'End If
        'If e.Column.FieldName = "Foreman3" Then
        '    e.Editor.TabIndex = tab9
        'End If
        'If e.Column.FieldName = "SectionHead1" Then
        '    e.Editor.TabIndex = tab10
        'End If
        'If e.Column.FieldName = "SectionHead2" Then
        '    e.Editor.TabIndex = tab11
        'End If
        
    End Sub

    Protected Sub Grid_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles Grid.RowValidating
        Dim GridData As DevExpress.Web.ASPxGridView.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView.ASPxGridView)

        For Each column As GridViewColumn In Grid.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            End If

            If dataColumn.FieldName = "LineID" Then
                If IsNothing(e.NewValues("LineID")) OrElse e.NewValues("LineID").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Line ID!"
                Else
                    If e.IsNewRow Then
                        If ClsLineDB.isExist(e.NewValues("LineID"), "") Then
                            e.Errors(dataColumn) = "Line ID is already exist!"
                            show_error(MsgTypeEnum.Warning, e.Errors(dataColumn), 1)
                            Exit Sub
                        End If
                    End If
                End If
            End If

            If dataColumn.FieldName = "LineName" Then
                If IsNothing(e.NewValues("LineName")) OrElse e.NewValues("LineName").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Line Name!"
                End If
            End If
            If dataColumn.FieldName = "EZRLineID" Then
                If IsNothing(e.NewValues("EZRLineID")) OrElse e.NewValues("EZRLineID").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input 'EZ Runner ID'!"
                End If
            End If

            'If dataColumn.FieldName = "Leader1" Then
            '    If IsNothing(e.NewValues("Leader1")) OrElse e.NewValues("Leader1").ToString.Trim = "" Then
            '        e.Errors(dataColumn) = "Please input Leader1 Status!"
            '        'show_error(MsgTypeEnum.Warning, e.Errors(dataColumn), 1)
            '        'Exit Sub
            '    End If
            'End If

            'If dataColumn.FieldName = "Foreman1" Then
            '    If IsNothing(e.NewValues("Foreman1")) OrElse e.NewValues("Foreman1").ToString.Trim = "" Then
            '        e.Errors(dataColumn) = "Please input Foreman1 Status!"
            '    End If
            'End If

            'If dataColumn.FieldName = "SectionHead1" Then
            '    If IsNothing(e.NewValues("SectionHead1")) OrElse e.NewValues("SectionHead1").ToString.Trim = "" Then
            '        e.Errors(dataColumn) = "Please input SectionHead1 Status!"
            '    End If
            'End If
        Next column

        For Each column As GridViewColumn In Grid.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            End If

            If dataColumn.FieldName = "LineID" Then
                If IsNothing(e.NewValues("LineID")) OrElse e.NewValues("LineID").ToString.Trim = "" Then
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    Exit Sub
                End If
            End If

            If dataColumn.FieldName = "LineName" Then
                If IsNothing(e.NewValues("LineName")) OrElse e.NewValues("LineName").ToString.Trim = "" Then
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    Exit Sub
                End If
            End If

            If dataColumn.FieldName = "EZRLineID" Then
                If IsNothing(e.NewValues("EZRLineID")) OrElse e.NewValues("EZRLineID").ToString.Trim = "" Then
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    Exit Sub
                End If
            End If

            'If dataColumn.FieldName = "Leader1" Then
            '    If IsNothing(e.NewValues("Leader1")) OrElse e.NewValues("Leader1").ToString.Trim = "" Then
            '        show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
            '        Exit Sub
            '    End If
            'End If

            'If dataColumn.FieldName = "Foreman1" Then
            '    If IsNothing(e.NewValues("Foreman1")) OrElse e.NewValues("Foreman1").ToString.Trim = "" Then
            '        show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
            '        Exit Sub
            '    End If
            'End If

            'If dataColumn.FieldName = "SectionHead1" Then
            '    If IsNothing(e.NewValues("SectionHead1")) OrElse e.NewValues("SectionHead1").ToString.Trim = "" Then
            '        show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
            '        Exit Sub
            '    End If
            'End If
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