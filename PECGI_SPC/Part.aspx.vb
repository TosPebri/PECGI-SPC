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

Public Class Part
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
    Dim pLineID As String = ""
#End Region

#Region "Procedure"
    Private Sub up_GridLoad()
        'Dim ErrMsg As String = ""
        'Dim Pro As New List(Of ClsPart)
        'Pro = ClsPartDB.GetList(ErrMsg)
        'If ErrMsg = "" Then
        '    Grid.DataSource = Pro
        '    Grid.Columns().
        '    Grid.DataBind()
        'Else
        '    show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
        'End If

        Dim ErrMsg As String = ""
        Dim Menu As DataSet
        Menu = ClsPartDB.GetList1(ErrMsg)
        If ErrMsg = "" Then
            Grid.DataSource = Menu
            Grid.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
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
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "A050")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If

        sGlobal.getMenu("A050")
        Master.SiteTitle = sGlobal.menuName
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "A050")
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
        Dim SubLine As New ClsPart With {.PartID = e.NewValues("PartID"),
                                      .PartName = e.NewValues("PartName"),
                                      .LineID = e.NewValues("LineID"),
                                      .Remark = e.NewValues("Remark"),
                                      .ActiveStatus = e.NewValues("ActiveStatus"),
                                      .CreateUser = pUser}
        ClsPartDB.Insert(SubLine, pErr)
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
        Dim line As New ClsPart With {.PartID = e.OldValues("PartID"),
                                      .PartName = e.NewValues("PartName"),
                                      .LineID = e.NewValues("LineID"),
                                      .Remark = e.NewValues("Remark"),
                                      .ActiveStatus = e.NewValues("ActiveStatus"),
                                      .UpdateUser = pUser}
        ClsPartDB.Update(line, pErr)
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
        Dim Line As New ClsPart With {.PartID = e.Values("PartID")}
        If ClsPartDB.isExistDel(e.Values("PartID"), "") Then
            show_error(MsgTypeEnum.Warning, "Part ID cannot be deleted because it is being used in the other process", 1)
        Else
            ClsPartDB.Delete(Line, pErr)
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
            Grid.FocusedRowIndex = 1
        End If
    End Sub

    Private Sub Grid_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs) Handles Grid.CellEditorInitialize
        If Not Grid.IsNewRowEditing Then
            If e.Column.FieldName = "PartID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Silver
            End If
            If e.Column.FieldName = "LineName" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
                'e.Editor.SetClientSideEventHandler("Init", "function FocusQuantity() {If (Grid.IsNewRowEditing()) Then Grid.FocusEditor('LineName');}")
            End If

            'If e.Column.FieldName = "LineID" Then
            '    pLineID = e.Value
            'End If
            'If e.Column.FieldName = "SubLineID" Then
            '    If TypeOf e.Editor Is ASPxComboBox Then
            '        Dim combo As ASPxComboBox = DirectCast(e.Editor, ASPxComboBox)
            '        'Dim comboline = TryCast(Grid.Columns("LineID"), GridViewCommandColumn)
            '        combo.DataSource = ClsSubLineDB.GetDataSubLine(pLineID)
            '        combo.TextField = "SubLineID"
            '        combo.ValueField = "SubLineID"
            '        combo.DataBindItems()
            '    End If
            'End If
        Else

            If e.Column.FieldName = "PartID" Then
                'e.Editor.SetClientSideEventHandler("Init", "function FocusQuantity() {If (Grid.IsNewRowEditing()) Then Grid.FocusEditor('PartID');}")
            End If

            If e.Column.FieldName = "LineName" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If
            'If e.Column.FieldName = "LineID" Then
            '    pLineID = e.Value
            'End If
            'If e.Column.FieldName = "SubLineID" Then
            '    If TypeOf e.Editor Is ASPxComboBox Then
            '        Dim combo As ASPxComboBox = DirectCast(e.Editor, ASPxComboBox)
            '        'Dim comboline = TryCast(Grid.Columns("LineID"), GridViewCommandColumn)
            '        combo.DataSource = ClsSubLineDB.GetDataSubLine(pLineID)
            '        combo.TextField = "SubLineID"
            '        combo.ValueField = "SubLineID"
            '        combo.DataBindItems()
            '    End If
            'End If
        End If
    End Sub

    Protected Sub Grid_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles Grid.RowValidating
        Dim GridData As DevExpress.Web.ASPxGridView.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView.ASPxGridView)

        For Each column As GridViewColumn In Grid.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            End If

            If dataColumn.FieldName = "PartID" Then
                If IsNothing(e.NewValues("PartID")) OrElse e.NewValues("PartID").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input PartID!"
                Else
                    If e.IsNewRow Then
                        If ClsPartDB.isExist(e.NewValues("PartID"), "") Then
                            e.Errors(dataColumn) = "PartID is already exist!"
                            show_error(MsgTypeEnum.Warning, e.Errors(dataColumn), 1)
                            Exit Sub
                        End If
                    End If
                End If
            End If

            If dataColumn.FieldName = "PartName" Then
                If IsNothing(e.NewValues("PartName")) OrElse e.NewValues("PartName").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input PartName!"
                End If
            End If

            If dataColumn.FieldName = "ActiveStatus" Then
                If IsNothing(e.NewValues("ActiveStatus")) OrElse e.NewValues("ActiveStatus").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Active Status!"
                End If
            End If
            'If dataColumn.FieldName = "SubLineID" Then
            '    If IsNothing(e.NewValues("SubLineID")) OrElse e.NewValues("SubLineID").ToString.Trim = "" Then
            '        e.Errors(dataColumn) = "Please input SubLineID!"
            '    End If
            'End If

            'If dataColumn.FieldName = "Remark" Then
            '    If IsNothing(e.NewValues("Remark")) OrElse e.NewValues("Remark").ToString.Trim = "" Then
            '        e.Errors(dataColumn) = "Please input Remark!"
            '    End If
            'End If
        Next column

        For Each column As GridViewColumn In Grid.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            End If

            If dataColumn.FieldName = "PartID" Then
                If IsNothing(e.NewValues("PartID")) OrElse e.NewValues("PartID").ToString.Trim = "" Then
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    Exit Sub
                End If
            End If

            If dataColumn.FieldName = "PartName" Then
                If IsNothing(e.NewValues("PartName")) OrElse e.NewValues("PartName").ToString.Trim = "" Then
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    Exit Sub
                End If
            End If

            If dataColumn.FieldName = "ActiveStatus" Then
                If IsNothing(e.NewValues("ActiveStatus")) OrElse e.NewValues("ActiveStatus").ToString.Trim = "" Then
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    Exit Sub
                End If
            End If

            'If dataColumn.FieldName = "SubLineID" Then
            '    If IsNothing(e.NewValues("SubLineID")) OrElse e.NewValues("SubLineID").ToString.Trim = "" Then
            '        show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
            '        Exit Sub
            '    End If
            'End If

            'If dataColumn.FieldName = "Remark" Then
            '    If IsNothing(e.NewValues("Remark")) OrElse e.NewValues("Remark").ToString.Trim = "" Then
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
        Dim nil As Integer = 1
        Grid.JSProperties("cp_LineID") = nil
    End Sub
#End Region


    'Private Sub Grid_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles Grid.InitNewRow
    '    Dim nil As Integer = 1
    '    Grid.JSProperties("cp_LineID") = nil
    'End Sub

    'Private Sub Grid_CancelRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles Grid.CancelRowEditing
    '    Dim nil As Integer = 0
    '    Grid.JSProperties("cp_LineID") = nil
    'End Sub
End Class