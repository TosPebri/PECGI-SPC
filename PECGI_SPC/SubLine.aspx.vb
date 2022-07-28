Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data

Public Class SubLine
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
    Public DuplicateMachine As Boolean
    Dim pLineID As String = ""
#End Region

#Region "Procedure"
    Private Sub up_GridLoad()
        'Dim ErrMsg As String = ""
        'Dim Pro As New List(Of ClsSubLine)
        'Pro = ClsSubLineDB.GetList(ErrMsg)
        'If ErrMsg = "" Then
        '    Grid.DataSource = Pro
        '    Grid.DataBind()
        'Else
        '    show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
        'End If

        Dim ErrMsg As String = ""
        Dim Menu As DataSet
        Menu = ClsSubLineDB.GetList(ErrMsg)
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

    Protected Sub FillSubLineIDCombo(ByVal pCmb As ASPxComboBox, ByVal pLineID As String)
        If String.IsNullOrEmpty(pLineID) Then
            Return
        End If

        Dim cities As List(Of String) = GetSubLineIDs(pLineID)
        pCmb.Items.Clear()
        For Each city As String In cities
            pCmb.Items.Add(city)
        Next city
    End Sub

    Private Function GetSubLineIDs(ByVal pLineID As String) As List(Of String)
        Dim list As New List(Of String)()
        SqlDataSource3.SelectParameters(0).DefaultValue = pLineID
        Dim view As DataView = CType(SqlDataSource3.Select(DataSourceSelectArguments.Empty), DataView)
        For i As Integer = 0 To view.Count - 1
            list.Add(CStr(view(i)(0)))
        Next i
        Return list
    End Function

    Private Sub cmbSubLineID_OnCallback(ByVal source As Object, ByVal e As CallbackEventArgsBase)
        FillSubLineIDCombo(TryCast(source, ASPxComboBox), e.Parameter)
    End Sub

    'Private Sub LineLoad()
    '    If TypeOf e.Editor Is ASPxComboBox Then
    '        Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
    '        combo.DataSource = ClsSubLineDB.GetDataProcess()
    '        combo.TextField = "ProcessID"
    '        combo.ValueField = "ProcessID"
    '        combo.DataBindItems()
    '    End If
    'End Sub
#End Region

#Region "Initialization"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            up_GridLoad()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "A040")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If
        sGlobal.getMenu("A040")
        Master.SiteTitle = sGlobal.menuName

        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "A040")
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
        ElseIf e.CallbackName = "CANCELEDIT" Then
            up_GridLoad()
        End If
    End Sub

    Protected Sub Grid_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles Grid.RowInserting
        e.Cancel = True
        Dim pErr As String = ""
        Dim SubLine As New ClsSubLine With {.LineID = e.NewValues("LineID"),
                                      .SubLineID = e.NewValues("SubLineID"),
                                      .ProcessID = e.NewValues("ProcessID"),
                                      .MachineNo = e.NewValues("MachineNo"),
                                      .Description = e.NewValues("Description"),
                                      .CreateUser = pUser}
        ClsSubLineDB.Insert(SubLine, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            Grid.CancelEdit()
            If DuplicateMachine = True Then
                show_error(MsgTypeEnum.Warning, "Save data successfully with Machine No duplicated!", 1)
            Else
                show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
            End If

            up_GridLoad()
        End If
    End Sub

    Protected Sub Grid_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles Grid.RowUpdating
        e.Cancel = True
        Dim pErr As String = ""
        Dim line As New ClsSubLine With {.LineID = e.OldValues("LineID"),
                                      .SubLineID = e.OldValues("SubLineID"),
                                      .ProcessID = e.NewValues("ProcessID"),
                                      .MachineNo = e.NewValues("MachineNo"),
                                      .Description = e.NewValues("Description"),
                                         .UpdateUser = pUser}
        ClsSubLineDB.Update(line, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            Grid.CancelEdit()
            If DuplicateMachine = True Then
                show_error(MsgTypeEnum.Warning, "Save data successfully with Machine No duplicated!", 1)
            Else
                show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
            End If
            up_GridLoad()
        End If
    End Sub

    Protected Sub Grid_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles Grid.RowDeleting
        e.Cancel = True
        Dim pErr As String = ""
        Dim Line As New ClsSubLine With {.LineID = e.Values("LineID"),
                                         .SubLineID = e.Values("SubLineID"),
                                         .ProcessID = e.Values("ProcessID")}

        If ClsSubLineDB.isExistDel(e.Values("SubLineID"), e.Values("LineID"), "") Then
            show_error(MsgTypeEnum.Warning, "Sub Line ID Cannot be Deleted Because it is Being Used in the Part", 1)
        Else
            ClsSubLineDB.Delete(Line, pErr)
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

    Private Sub Grid_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles Grid.CellEditorInitialize

        If Not Grid.IsNewRowEditing Then
            If e.Column.FieldName = "LineID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If
            If e.Column.FieldName = "LineName" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If
            If e.Column.FieldName = "SubLineID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If
            If e.Column.FieldName = "ProcessID" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If
            If e.Column.FieldName = "ProcessName" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If

            If e.Column.FieldName = "MachineNo" Then
                If TypeOf e.Editor Is ASPxComboBox Then
                    Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
                    combo.DataSource = ClsSubLineDB.GetDataMachine()
                    combo.TextField = "MachineNo"
                    combo.ValueField = "MachineNo"
                    combo.DataBindItems()
                End If
            End If
        Else
            If e.Column.FieldName = "LineID" Then
                Dim val As Object = Grid.GetRowValuesByKeyValue(e.KeyValue, "LineID")
                Dim country As String = CStr(val)
                Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
                FillSubLineIDCombo(combo, country)
                AddHandler combo.Callback, AddressOf cmbSubLineID_OnCallback

                If TypeOf e.Editor Is ASPxComboBox Then
                    combo.DataSource = ClsSubLineDB.GetDataLine()
                    combo.TextField = "LineID"
                    combo.ValueField = "LineID"
                    combo.DataBindItems()
                End If

            End If
            If e.Column.FieldName = "LineName" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If

            If e.Column.FieldName = "ProcessID" Then
                If TypeOf e.Editor Is ASPxComboBox Then
                    Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
                    combo.DataSource = ClsSubLineDB.GetDataProcess()
                    combo.TextField = "ProcessID"
                    combo.ValueField = "ProcessID"
                    combo.DataBindItems()
                End If
            End If

            If e.Column.FieldName = "ProcessName" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Black
                e.Editor.BackColor = Color.WhiteSmoke
            End If

            If e.Column.FieldName = "MachineNo" Then
                If TypeOf e.Editor Is ASPxComboBox Then
                    Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
                    combo.DataSource = ClsSubLineDB.GetDataMachine()
                    combo.TextField = "MachineNo"
                    combo.ValueField = "MachineNo"
                    combo.DataBindItems()
                End If
            End If

            If e.Column.FieldName = "SubLineID" Then
                Dim val As Object = Grid.GetRowValuesByKeyValue(e.KeyValue, "LineID")
                Dim country As String = CStr(val)
                Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
                FillSubLineIDCombo(combo, country)
                AddHandler combo.Callback, AddressOf cmbSubLineID_OnCallback
                'up_GridLoad()
            End If
        End If


    End Sub

    Protected Sub Grid_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles Grid.RowValidating
        Dim GridData As DevExpress.Web.ASPxGridView = TryCast(sender, DevExpress.Web.ASPxGridView)

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
                        If ClsSubLineDB.isExist(e.NewValues("LineID"), e.NewValues("SubLineID"), e.NewValues("ProcessID"), "") Then
                            e.Errors(dataColumn) = "Data is already exist!"
                            show_error(MsgTypeEnum.Warning, e.Errors(dataColumn), 1)
                        End If
                    End If
                End If
            End If

            If dataColumn.FieldName = "SubLineID" Then
                If IsNothing(e.NewValues("SubLineID")) OrElse e.NewValues("SubLineID").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Sub Line ID!"
                Else
                    If e.IsNewRow Then
                        If ClsSubLineDB.isExist(e.NewValues("LineID"), e.NewValues("SubLineID"), e.NewValues("ProcessID"), "") Then
                            e.Errors(dataColumn) = "Data is already exist!"
                            show_error(MsgTypeEnum.Warning, e.Errors(dataColumn), 1)
                        End If
                    End If
                End If
            End If

            If dataColumn.FieldName = "ProcessID" Then
                If IsNothing(e.NewValues("ProcessID")) OrElse e.NewValues("SubLineID").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Process ID!"
                Else
                    If e.IsNewRow Then
                        If ClsSubLineDB.isExist(e.NewValues("LineID"), e.NewValues("SubLineID"), e.NewValues("ProcessID"), "") Then
                            e.Errors(dataColumn) = "Data is already exist!"
                            show_error(MsgTypeEnum.Warning, e.Errors(dataColumn), 1)
                        End If
                    End If
                End If
            End If

            If dataColumn.FieldName = "ProcessID" Then
                If IsNothing(e.NewValues("ProcessID")) OrElse e.NewValues("ProcessID").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input ProcessID!"
                End If
            End If

            If dataColumn.FieldName = "MachineNo" Then
                If IsNothing(e.NewValues("MachineNo")) OrElse e.NewValues("MachineNo").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input MachineNo!"
                Else
                    If ClsSubLineDB.isExistMachine(e.NewValues("MachineNo"), "") Then
                        DuplicateMachine = True
                    End If
                End If
            End If

            If dataColumn.FieldName = "Description" Then
                If IsNothing(e.NewValues("Description")) OrElse e.NewValues("Description").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please input Description!"
                End If
            End If
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

            If dataColumn.FieldName = "SubLineID" Then
                If IsNothing(e.NewValues("SubLineID")) OrElse e.NewValues("SubLineID").ToString.Trim = "" Then
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    Exit Sub
                End If
            End If

            If dataColumn.FieldName = "ProcessID" Then
                If IsNothing(e.NewValues("ProcessID")) OrElse e.NewValues("ProcessID").ToString.Trim = "" Then
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    Exit Sub
                End If
            End If

            If dataColumn.FieldName = "MachineNo" Then
                If IsNothing(e.NewValues("MachineNo")) OrElse e.NewValues("MachineNo").ToString.Trim = "" Then
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    Exit Sub
                Else
                    If ClsSubLineDB.isExistMachine(e.NewValues("MachineNo"), "") Then
                        DuplicateMachine = True
                        Exit Sub
                    End If
                End If
            End If

            If dataColumn.FieldName = "Description" Then
                If IsNothing(e.NewValues("Description")) OrElse e.NewValues("Description").ToString.Trim = "" Then
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