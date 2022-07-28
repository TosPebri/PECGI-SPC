Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports OfficeOpenXml


Public Class QCSXRData
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
        Dim Menu As DataSet
        Menu = ClsQCSXRDataDB.GetList(ErrMsg)
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "B060")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If
        sGlobal.getMenu("B060")
        Master.SiteTitle = sGlobal.menuName

        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B060")
        show_error(MsgTypeEnum.Info, "", 0)
        If AuthUpdate = False Then
            Dim commandColumn = TryCast(Grid.Columns(0), GridViewCommandColumn)
            commandColumn.Visible = False
        End If
        up_GridLoad()
    End Sub

    '#Region "Grid"
    '    Protected Sub GridMenu_AfterPerformCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles GridMenu.AfterPerformCallback
    '        If e.CallbackName <> "CANCELEDIT" Then
    '            'If ClsQCSXRDataDB.Exist(cbopartid.Value, cborevno.Value, cbocheckitem.Value, "") = True Then
    '            '    up_GridLoad(cbopartid.Value, cborevno.Value, cbocheckitem.Value, 0)
    '            'Else
    '            '    up_GridLoad(cbopartid.Value, cborevno.Value, cbocheckitem.Value, 1)
    '            'End If
    '        End If
    '    End Sub

    '    Protected Sub GridMenu_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles GridMenu.RowInserting
    '        e.Cancel = True
    '    End Sub

    '    Protected Sub GridMenu_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles GridMenu.RowUpdating
    '        e.Cancel = True
    '    End Sub

    '    Protected Sub GridMenu_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles GridMenu.RowDeleting
    '        e.Cancel = True
    '    End Sub

    '    Private Sub GridMenu_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridMenu.BeforeGetCallbackResult
    '        If GridMenu.IsNewRowEditing Then
    '            GridMenu.SettingsCommandButton.UpdateButton.Text = "Save"
    '            GridMenu.SettingsCommandButton.NewButton.Text = "#"
    '        End If
    '    End Sub

    '    Private Sub GridMenu_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles GridMenu.CellEditorInitialize
    '        'If Not Grid.IsNewRowEditing Then
    '        '    If e.Column.FieldName = "LineID" Then
    '        '        e.Editor.ReadOnly = True
    '        '        e.Editor.ForeColor = Color.Silver
    '        '    End If
    '        'End If

    '        'Dim tab1 As Int16 = 1
    '        'Dim tab2 As Int16 = 2
    '        'Dim tab3 As Int16 = 3
    '        ''Dim tab4 As Int16 = 4
    '        ''Dim tab5 As Int16 = 5
    '        ''Dim tab6 As Int16 = 6
    '        ''Dim tab7 As Int16 = 7
    '        ''Dim tab8 As Int16 = 8
    '        ''Dim tab9 As Int16 = 9
    '        ''Dim tab10 As Int16 = 10
    '        ''Dim tab11 As Int16 = 11


    '        'If e.Column.FieldName = "LineID" Then
    '        '    e.Editor.TabIndex = tab1
    '        'End If
    '        'If e.Column.FieldName = "LineName" Then
    '        '    e.Editor.TabIndex = tab2
    '        'End If
    '        'If e.Column.FieldName = "EZRLineID" Then
    '        '    e.Editor.TabIndex = tab3
    '        'End If
    '        ''If e.Column.FieldName = "Leader1" Then
    '        ''    e.Editor.TabIndex = tab4
    '        ''End If
    '        ''If e.Column.FieldName = "Leader2" Then
    '        ''    e.Editor.TabIndex = tab5
    '        ''End If
    '        ''If e.Column.FieldName = "Leader3" Then
    '        ''    e.Editor.TabIndex = tab6
    '        ''End If
    '        ''If e.Column.FieldName = "Foreman1" Then
    '        ''    e.Editor.TabIndex = tab7
    '        ''End If
    '        ''If e.Column.FieldName = "Foreman2" Then
    '        ''    e.Editor.TabIndex = tab8
    '        ''End If
    '        ''If e.Column.FieldName = "Foreman3" Then
    '        ''    e.Editor.TabIndex = tab9
    '        ''End If
    '        ''If e.Column.FieldName = "SectionHead1" Then
    '        ''    e.Editor.TabIndex = tab10
    '        ''End If
    '        ''If e.Column.FieldName = "SectionHead2" Then
    '        ''    e.Editor.TabIndex = tab11
    '        ''End If

    '    End Sub

    '    Protected Sub GridMenu_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles GridMenu.RowValidating
    '        'Dim GridData As DevExpress.Web.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView)

    '        'For Each column As GridViewColumn In Grid.Columns
    '        '    Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
    '        '    If dataColumn Is Nothing Then
    '        '        Continue For
    '        '    End If

    '        '    If dataColumn.FieldName = "LineID" Then
    '        '        If IsNothing(e.NewValues("LineID")) OrElse e.NewValues("LineID").ToString.Trim = "" Then
    '        '            e.Errors(dataColumn) = "Please input Line ID!"
    '        '        Else
    '        '            If e.IsNewRow Then
    '        '                If ClsLineDB.isExist(e.NewValues("LineID"), "") Then
    '        '                    e.Errors(dataColumn) = "Line ID is already exist!"
    '        '                    show_error(MsgTypeEnum.Warning, e.Errors(dataColumn), 1)
    '        '                    Exit Sub
    '        '                End If
    '        '            End If
    '        '        End If
    '        '    End If

    '        '    If dataColumn.FieldName = "LineName" Then
    '        '        If IsNothing(e.NewValues("LineName")) OrElse e.NewValues("LineName").ToString.Trim = "" Then
    '        '            e.Errors(dataColumn) = "Please input Line Name!"
    '        '        End If
    '        '    End If
    '        '    If dataColumn.FieldName = "EZRLineID" Then
    '        '        If IsNothing(e.NewValues("EZRLineID")) OrElse e.NewValues("EZRLineID").ToString.Trim = "" Then
    '        '            e.Errors(dataColumn) = "Please input 'EZ Runner ID'!"
    '        '        End If
    '        '    End If

    '        '    'If dataColumn.FieldName = "Leader1" Then
    '        '    '    If IsNothing(e.NewValues("Leader1")) OrElse e.NewValues("Leader1").ToString.Trim = "" Then
    '        '    '        e.Errors(dataColumn) = "Please input Leader1 Status!"
    '        '    '        'show_error(MsgTypeEnum.Warning, e.Errors(dataColumn), 1)
    '        '    '        'Exit Sub
    '        '    '    End If
    '        '    'End If

    '        '    'If dataColumn.FieldName = "Foreman1" Then
    '        '    '    If IsNothing(e.NewValues("Foreman1")) OrElse e.NewValues("Foreman1").ToString.Trim = "" Then
    '        '    '        e.Errors(dataColumn) = "Please input Foreman1 Status!"
    '        '    '    End If
    '        '    'End If

    '        '    'If dataColumn.FieldName = "SectionHead1" Then
    '        '    '    If IsNothing(e.NewValues("SectionHead1")) OrElse e.NewValues("SectionHead1").ToString.Trim = "" Then
    '        '    '        e.Errors(dataColumn) = "Please input SectionHead1 Status!"
    '        '    '    End If
    '        '    'End If
    '        'Next column

    '        'For Each column As GridViewColumn In Grid.Columns
    '        '    Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
    '        '    If dataColumn Is Nothing Then
    '        '        Continue For
    '        '    End If

    '        '    If dataColumn.FieldName = "LineID" Then
    '        '        If IsNothing(e.NewValues("LineID")) OrElse e.NewValues("LineID").ToString.Trim = "" Then
    '        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
    '        '            Exit Sub
    '        '        End If
    '        '    End If

    '        '    If dataColumn.FieldName = "LineName" Then
    '        '        If IsNothing(e.NewValues("LineName")) OrElse e.NewValues("LineName").ToString.Trim = "" Then
    '        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
    '        '            Exit Sub
    '        '        End If
    '        '    End If

    '        '    If dataColumn.FieldName = "EZRLineID" Then
    '        '        If IsNothing(e.NewValues("EZRLineID")) OrElse e.NewValues("EZRLineID").ToString.Trim = "" Then
    '        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
    '        '            Exit Sub
    '        '        End If
    '        '    End If

    '        '    'If dataColumn.FieldName = "Leader1" Then
    '        '    '    If IsNothing(e.NewValues("Leader1")) OrElse e.NewValues("Leader1").ToString.Trim = "" Then
    '        '    '        show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
    '        '    '        Exit Sub
    '        '    '    End If
    '        '    'End If

    '        '    'If dataColumn.FieldName = "Foreman1" Then
    '        '    '    If IsNothing(e.NewValues("Foreman1")) OrElse e.NewValues("Foreman1").ToString.Trim = "" Then
    '        '    '        show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
    '        '    '        Exit Sub
    '        '    '    End If
    '        '    'End If

    '        '    'If dataColumn.FieldName = "SectionHead1" Then
    '        '    '    If IsNothing(e.NewValues("SectionHead1")) OrElse e.NewValues("SectionHead1").ToString.Trim = "" Then
    '        '    '        show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
    '        '    '        Exit Sub
    '        '    '    End If
    '        '    'End If
    '        'Next column
    '    End Sub

    '    Private Sub GridMenu_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles GridMenu.CustomCallback
    '        'Dim pFunction As String = Split(e.Parameters, "|")(0)
    '        'Dim pErr As String = ""
    '        'Select Case pFunction
    '        '    Case "Copy"
    '        '        Dim QCSXRData As New ClsQCSXRData
    '        '        QCSXRData.PartID = cbopartid.Value
    '        '        QCSXRData.RevNo = cborevno.Value
    '        '        QCSXRData.RevNoCopy = cborevnopopup.Value
    '        '        QCSXRData.ItemID = cbocheckitem.Value
    '        '        QCSXRData.ItemIDCopy = cbocheckitempopup.Value

    '        '        ClsQCSXRDataDB.Copy(QCSXRData, pErr)

    '        '        If pErr <> "" Then
    '        '            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
    '        '        Else
    '        '            show_error(MsgTypeEnum.Success, "Copy data successfully!", 1)
    '        '        End If
    '        'End Select
    '    End Sub

    '    Protected Sub GridMenu_StartRowEditing(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles GridMenu.StartRowEditing
    '        If (Not GridMenu.IsNewRowEditing) Then
    '            GridMenu.DoRowValidation()
    '        End If
    '        show_error(MsgTypeEnum.Info, "", 0)
    '    End Sub

    '    Private Sub GridMenu_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs) Handles GridMenu.BatchUpdate
    '        Dim pErr As String = ""
    '        Dim QCSXRData As New ClsQCSXRData
    '        QCSXRData.PartID = cbopartid.Value
    '        QCSXRData.RevNo = cborevno.Value
    '        QCSXRData.ItemID = cbocheckitem.Value

    '        For i As Integer = 0 To e.UpdateValues.Count - 1
    '            QCSXRData.XID = e.UpdateValues(i).OldValues("XID")
    '            QCSXRData.A2Value = e.UpdateValues(i).NewValues("A2Value")
    '            QCSXRData.D4Value = e.UpdateValues(i).NewValues("D4Value")
    '            ClsQCSXRDataDB.Update(QCSXRData, pErr)
    '        Next

    '        If pErr <> "" Then
    '            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
    '        Else
    '            show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
    '        End If
    '    End Sub
    '#End Region


#Region "Control Event"
    Protected Sub Grid_AfterPerformCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles Grid.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" Then
            up_GridLoad()
        End If
    End Sub

    Protected Sub Grid_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles Grid.RowInserting
        e.Cancel = True
        Dim pErr As String = ""
        Dim XRData As New ClsQCSXRData With {.XID = e.NewValues("XID"),
                                      .A2Value = e.NewValues("A2Value"),
                                      .D4Value = e.NewValues("D4Value")}
        ClsQCSXRDataDB.Insert(XRData, pErr)
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
        Dim XRData As New ClsQCSXRData With {.XID = e.OldValues("XID"),
                                      .A2Value = e.NewValues("A2Value"),
                                      .D4Value = e.NewValues("D4Value")}
        ClsQCSXRDataDB.Update(XRData, pErr)
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
        Dim XRData As New ClsQCSXRData With {.XID = e.Values("XID")}
        ClsQCSXRDataDB.Delete(XRData, pErr)
            If pErr <> "" Then
                show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
            Else
                Grid.CancelEdit()
                show_error(MsgTypeEnum.Success, "Delete data successfully!", 1)
                up_GridLoad()
            End If
    End Sub

    Private Sub Grid_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.BeforeGetCallbackResult
        If Grid.IsNewRowEditing Then
            Grid.SettingsCommandButton.UpdateButton.Text = "Save"
        End If
    End Sub

    Private Sub Grid_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles Grid.CellEditorInitialize
        If Not Grid.IsNewRowEditing Then

            If e.Column.FieldName = "XID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If
        Else
            If e.Column.FieldName = "XID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke

                Dim ErrMsg As String = ""
                Dim Menu As DataSet
                Menu = ClsQCSXRDataDB.isExistXID(ErrMsg)
                If Menu.Tables(0).Rows(0)("NilMax").ToString() = "" Then
                    e.Editor.Value = 1
                Else
                    e.Editor.Value = Val(Menu.Tables(0).Rows(0)("NilMax").ToString()) + 1
                End If

                If ErrMsg = "" Then
                End If
            End If
        End If
    End Sub

    Protected Sub Grid_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles Grid.RowValidating
        'Dim GridData As DevExpress.Web.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView)

        'For Each column As GridViewColumn In Grid.Columns
        '    Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
        '    If dataColumn Is Nothing Then
        '        Continue For
        '    End If

        '    If dataColumn.FieldName = "LineID" Then
        '        If IsNothing(e.NewValues("LineID")) OrElse e.NewValues("LineID").ToString.Trim = "" Then
        '            e.Errors(dataColumn) = "Please input Line ID!"
        '        Else
        '            If e.IsNewRow Then
        '                If ClsLineDB.isExist(e.NewValues("LineID"), "") Then
        '                    e.Errors(dataColumn) = "Line ID is already exist!"
        '                    show_error(MsgTypeEnum.Warning, e.Errors(dataColumn), 1)
        '                    Exit Sub
        '                End If
        '            End If
        '        End If
        '    End If

        '    If dataColumn.FieldName = "LineName" Then
        '        If IsNothing(e.NewValues("LineName")) OrElse e.NewValues("LineName").ToString.Trim = "" Then
        '            e.Errors(dataColumn) = "Please input Line Name!"
        '        End If
        '    End If
        '    If dataColumn.FieldName = "EZRLineID" Then
        '        If IsNothing(e.NewValues("EZRLineID")) OrElse e.NewValues("EZRLineID").ToString.Trim = "" Then
        '            e.Errors(dataColumn) = "Please input 'EZ Runner ID'!"
        '        End If
        '    End If
        'Next column

        'For Each column As GridViewColumn In Grid.Columns
        '    Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
        '    If dataColumn Is Nothing Then
        '        Continue For
        '    End If

        '    If dataColumn.FieldName = "LineID" Then
        '        If IsNothing(e.NewValues("LineID")) OrElse e.NewValues("LineID").ToString.Trim = "" Then
        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
        '            Exit Sub
        '        End If
        '    End If

        '    If dataColumn.FieldName = "LineName" Then
        '        If IsNothing(e.NewValues("LineName")) OrElse e.NewValues("LineName").ToString.Trim = "" Then
        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
        '            Exit Sub
        '        End If
        '    End If

        '    If dataColumn.FieldName = "EZRLineID" Then
        '        If IsNothing(e.NewValues("EZRLineID")) OrElse e.NewValues("EZRLineID").ToString.Trim = "" Then
        '            show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
        '            Exit Sub
        '        End If
        '    End If
        'Next column
    End Sub

    Protected Sub Grid_StartRowEditing(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles Grid.StartRowEditing
        If (Not Grid.IsNewRowEditing) Then
            Grid.DoRowValidation()
        End If
        show_error(MsgTypeEnum.Info, "", 0)
    End Sub

#End Region

#Region "Callback"
    'Private Sub cbopartid_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbopartid.Callback
    '    Dim pParam As String = Split(e.Parameter, "|")(1)

    '    Dim dsMenu As DataTable
    '    dsMenu = ClsQCSMasterDB.GetDataPart(pParam, "")
    '    cbopartid.DataSource = dsMenu
    '    cbopartid.DataBind()

    'End Sub

    'Private Sub cborevno_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cborevno.Callback
    '    Dim pParam As String = Split(e.Parameter, "|")(1)

    '    Dim dsMenu As DataTable
    '    dsMenu = ClsQCSMasterDB.GetDataRev(pParam, "")
    '    cborevno.DataSource = dsMenu
    '    cborevno.DataBind()
    'End Sub

    'Private Sub cborevnopopup_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cborevnopopup.Callback
    '    Dim pParam As String = Split(e.Parameter, "|")(1)

    '    Dim dsMenu As DataTable
    '    dsMenu = ClsQCSMasterDB.GetDataRev(pParam, "")

    '    cborevnopopup.DataSource = dsMenu
    '    cborevnopopup.DataBind()
    'End Sub

    'Private Sub cboprocessid_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cboprocessid.Callback
    '    Dim pPartID As String = Split(e.Parameter, "|")(1)
    '    Dim pRevNo As String = Split(e.Parameter, "|")(2)

    '    Dim dsMenu As DataTable
    '    dsMenu = ClsQCSXRDataDB.GetDataProcess(pPartID, pRevNo, "")
    '    cboprocessid.DataSource = dsMenu
    '    cboprocessid.DataBind()
    'End Sub

    'Private Sub cbocheckitem_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbocheckitem.Callback
    '    Dim pPartID As String = Split(e.Parameter, "|")(1)
    '    Dim pRevNo As String = Split(e.Parameter, "|")(2)
    '    Dim pProcessID As String = Split(e.Parameter, "|")(3)

    '    Dim dsMenu As DataTable
    '    dsMenu = ClsQCSXRDataDB.GetDataCheckItem(pPartID, pRevNo, pProcessID, "")
    '    cbocheckitem.DataSource = dsMenu
    '    cbocheckitem.DataBind()
    'End Sub

    'Private Sub cbocheckitempopup_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbocheckitempopup.Callback
    '    Dim pPartID As String = Split(e.Parameter, "|")(1)
    '    Dim pRevNo As String = Split(e.Parameter, "|")(2)
    '    Dim pProcessID As String = Split(e.Parameter, "|")(3)

    '    Dim dsMenu As DataTable
    '    dsMenu = ClsQCSXRDataDB.GetDataCheckItem(pPartID, pRevNo, pProcessID, "")
    '    cbocheckitempopup.DataSource = dsMenu
    '    cbocheckitempopup.DataBind()
    'End Sub
#End Region
End Class