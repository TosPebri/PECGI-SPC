Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Drawing

Public Class ControlChartSetup
    Inherits System.Web.UI.Page

#Region "Declarations"
    Dim pUser As String = ""
    Dim pMenuID As String = "A040"
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Private dt As DataTable
#End Region

#Region "Events"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            up_Fillcombo()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sGlobal.getMenu(pMenuID)
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user")
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, pMenuID)
        show_error(MsgTypeEnum.Info, "", 0)
    End Sub

    Protected Sub Grid_AfterPerformCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles Grid.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
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
            If e.Column.FieldName = "Factory" Or e.Column.FieldName = "TypeEditGrid" Or e.Column.FieldName = "MachineEditGrid" Or e.Column.FieldName = "ItemCheckEditGrid" Or e.Column.FieldName = "Start" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Silver
            End If
        End If

        If e.Column.FieldName = "Factory" Then
            Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
            up_FillcomboGrid(combo, "0")
            If Grid.IsEditing Then combo.Value = e.Value : HF.Set("FactoryEdit", e.Value)
        ElseIf e.Column.FieldName = "MachineEditGrid" Then
            Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
            AddHandler combo.Callback, AddressOf cmbGridMachine_OnCallback
            If Grid.IsEditing Then Call up_FillcomboGrid(combo, "3", HF.Get("FactoryEdit")) : combo.Value = e.Value
        ElseIf e.Column.FieldName = "ItemCheckEditGrid" Then
            Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
            up_FillcomboGrid(combo, "5")
            If Grid.IsEditing Then combo.Value = e.Value
        ElseIf e.Column.FieldName = "TypeEditGrid" Then
            Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
            up_FillcomboGrid(combo, "1")
            If Grid.IsEditing Then combo.Value = e.Value
        End If

        If e.Column.FieldName = "Factory" Or e.Column.FieldName = "TypeEditGrid" Or e.Column.FieldName = "Start" Then
            e.Editor.Width = "125"
        ElseIf e.Column.FieldName = "MachineEditGrid" Or e.Column.FieldName = "ItemCheckEditGrid" Then
            e.Editor.Width = "200"
        ElseIf e.Column.FieldName = "SpecUSL" Or e.Column.FieldName = "SpecLSL" _
            Or e.Column.FieldName = "XCL" Or e.Column.FieldName = "XUCL" Or e.Column.FieldName = "XLCL" _
            Or e.Column.FieldName = "RCL" Or e.Column.FieldName = "RUCL" Or e.Column.FieldName = "RLCL" Then
            e.Editor.Width = "75"
        End If
    End Sub

    Private Sub Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Grid.CustomCallback
        Try
            Dim pAction As String = Split(e.Parameters, "|")(0)

            If pAction = "Load" Then
                up_GridLoad()
            ElseIf pAction = "Kosong" Then
                Dim cls As New clsControlChartSetup With {
                    .Factory = "",
                    .Machine = "",
                    .Type = "",
                    .Period = ""
                }
                dt = clsControlChartSetupDB.GetList(cls)
                Grid.DataSource = dt
                Grid.DataBind()
            End If

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Protected Sub Grid_StartRowEditing(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles Grid.StartRowEditing
        If (Not Grid.IsNewRowEditing) Then
            Grid.DoRowValidation()
        End If
        show_error(MsgTypeEnum.Info, "", 0)
    End Sub

    Protected Sub Grid_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles Grid.RowInserting
        e.Cancel = True
        Try
            Dim StTime As DateTime = Convert.ToDateTime(e.NewValues("Start"))

            Call up_InsUpd("0", _
                e.NewValues("Factory"), _
                e.NewValues("TypeEditGrid"), _
                e.NewValues("MachineEditGrid"), _
                e.NewValues("ItemCheckEditGrid"), _
                StTime.ToString("yyyy-MM-dd"),
                e.NewValues("SpecUSL"), e.NewValues("SpecLSL"), _
                e.NewValues("XCL"), e.NewValues("XUCL"), e.NewValues("XLCL"), _
                e.NewValues("RCL"), e.NewValues("RLCL"), e.NewValues("RUCL"), _
                pUser)
            Grid.CancelEdit()
            up_GridLoad()
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Protected Sub Grid_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles Grid.RowUpdating
        e.Cancel = True
        Try
            Dim StTime As DateTime = Convert.ToDateTime(e.NewValues("Start"))

            Call up_InsUpd("1", _
                e.NewValues("Factory"), _
                e.NewValues("TypeEditGrid"), _
                e.NewValues("MachineEditGrid"), _
                e.NewValues("ItemCheckEditGrid"), _
                StTime.ToString("yyyy-MM-dd"),
                e.NewValues("SpecUSL"), e.NewValues("SpecLSL"), _
                e.NewValues("XCL"), e.NewValues("XUCL"), e.NewValues("XLCL"), _
                e.NewValues("RCL"), e.NewValues("RLCL"), e.NewValues("RUCL"), _
                pUser)
            Grid.CancelEdit()
            up_GridLoad()
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Protected Sub Grid_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles Grid.RowDeleting
        e.Cancel = True
        Try
            Dim StTime As DateTime = Convert.ToDateTime(e.Values("Start"))

            Dim cls As New clsControlChartSetup With
            {
                .Factory = e.Values("Factory"),
                .ItemType = e.Values("TypeEditGrid"),
                .Machine = e.Values("MachineEditGrid"),
                .ItemCheck = e.Values("ItemCheckEditGrid"),
                .StartTime = StTime.ToString("yyyy-MM-dd")
            }

            clsControlChartSetupDB.Delete(cls)
            Grid.CancelEdit()
            up_GridLoad()
            show_error(MsgTypeEnum.Success, "Delete data successfully!", 1)
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Protected Sub Grid_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles Grid.RowValidating
        Dim dataCol As New GridViewDataColumn
        Dim AdaError As Boolean = False

        For Each column As GridViewColumn In Grid.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            End If

            If dataColumn.FieldName = "Factory" Then
                If IsNothing(e.NewValues("Factory")) OrElse e.NewValues("Factory").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please Choose Factory!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "MachineEditGrid" Then
                If IsNothing(e.NewValues("MachineEditGrid")) OrElse e.NewValues("MachineEditGrid").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please Choose Machine!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "ItemCheckEditGrid" Then
                If IsNothing(e.NewValues("ItemCheckEditGrid")) OrElse e.NewValues("ItemCheckEditGrid").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please Choose Item Check!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "TypeEditGrid" Then
                If IsNothing(e.NewValues("TypeEditGrid")) OrElse e.NewValues("TypeEditGrid").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please Choose Type!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "Start" Then
                If IsNothing(e.NewValues("Start")) OrElse e.NewValues("Start").ToString.Trim = "" Then
                    e.Errors(dataColumn) = "Please Choose Period Start!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "SpecUSL" Then
                If IsNothing(e.NewValues("SpecUSL")) OrElse e.NewValues("SpecUSL").ToString.Trim = "" OrElse e.NewValues("SpecUSL").ToString.Trim = "0" Then
                    e.Errors(dataColumn) = "Please Input Minimum 0.001!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "SpecLSL" Then
                If IsNothing(e.NewValues("SpecLSL")) OrElse e.NewValues("SpecLSL").ToString.Trim = "" OrElse e.NewValues("SpecLSL").ToString.Trim = "0" Then
                    e.Errors(dataColumn) = "Please Input Minimum 0.001!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "XCL" Then
                If IsNothing(e.NewValues("XCL")) OrElse e.NewValues("XCL").ToString.Trim = "" OrElse e.NewValues("XCL").ToString.Trim = "0" Then
                    e.Errors(dataColumn) = "Please Input Minimum 0.001!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "XUCL" Then
                If IsNothing(e.NewValues("XUCL")) OrElse e.NewValues("XUCL").ToString.Trim = "" OrElse e.NewValues("XUCL").ToString.Trim = "0" Then
                    e.Errors(dataColumn) = "Please Input Minimum 0.001!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "XLCL" Then
                If IsNothing(e.NewValues("XLCL")) OrElse e.NewValues("XLCL").ToString.Trim = "" OrElse e.NewValues("XLCL").ToString.Trim = "0" Then
                    e.Errors(dataColumn) = "Please Input Minimum 0.001!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "RCL" Then
                If IsNothing(e.NewValues("RCL")) OrElse e.NewValues("RCL").ToString.Trim = "" OrElse e.NewValues("RCL").ToString.Trim = "0" Then
                    e.Errors(dataColumn) = "Please Input Minimum 0.001!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "RUCL" Then
                If IsNothing(e.NewValues("RUCL")) OrElse e.NewValues("RUCL").ToString.Trim = "" OrElse e.NewValues("RUCL").ToString.Trim = "0" Then
                    e.Errors(dataColumn) = "Please Input Minimum 0.001!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "RLCL" Then
                If IsNothing(e.NewValues("RLCL")) OrElse e.NewValues("RLCL").ToString.Trim = "" OrElse e.NewValues("RLCL").ToString.Trim = "0" Then
                    e.Errors(dataColumn) = "Please Input Minimum 0.001!"
                    show_error(MsgTypeEnum.Warning, "Please fill in all required fields!", 1)
                    AdaError = True
                End If
            End If

            If dataColumn.FieldName = "LastUser" Then
                dataCol = dataColumn
            End If
        Next column

        If e.IsNewRow Then
            Dim pErr As String = ""
            Dim StTime As DateTime = Convert.ToDateTime(e.NewValues("Start"))

            Dim cls As New clsControlChartSetup With
            {
                .Factory = e.NewValues("Factory"),
                .ItemType = e.NewValues("TypeEditGrid"),
                .Machine = e.NewValues("MachineEditGrid"),
                .ItemCheck = e.NewValues("ItemCheckEditGrid"),
                .StartTime = StTime.ToString("yyyy-MM-dd")
            }

            clsControlChartSetupDB.Check(cls, pErr)
            If pErr <> "" Then show_error(MsgTypeEnum.Warning, pErr, 1) : e.Errors(dataCol) = ""
        End If
    End Sub

    Private Sub cboMachine_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboMachine.Callback
        Dim FactoryCode As String = e.Parameter

        Try
            dt = clsControlChartSetupDB.FillCombo("2", FactoryCode)
            With cboMachine
                .Items.Clear() : .Columns.Clear()
                .DataSource = dt
                .Columns.Add("Code") : .Columns(0).Visible = False
                .Columns.Add("Description") : .Columns(1).Width = 225

                .TextField = "Description"
                .ValueField = "Code"
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
        Catch ex As Exception
            show_error(MsgTypeEnum.Info, "", 0)
        End Try
    End Sub

    Private Sub cmbGridMachine_OnCallback(ByVal source As Object, ByVal e As CallbackEventArgsBase)
        Dim cmb As ASPxComboBox = source
        Dim Param As String = e.Parameter

        dt = clsControlChartSetupDB.FillCombo("3", Param)
        With cmb
            .Items.Clear() : .Columns.Clear()
            .DataSource = dt
            .Columns.Add("Code") : .Columns(0).Visible = False
            .Columns.Add("Description") : .Columns(1).Width = 100

            .TextField = "Description"
            .ValueField = "Code"
            .DataBind()
            .SelectedIndex = -1
        End With
    End Sub
#End Region

#Region "Functions"
    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        Grid.JSProperties("cp_message") = ErrMsg
        Grid.JSProperties("cp_type") = msgType
        Grid.JSProperties("cp_val") = pVal
    End Sub

    Private Sub up_Fillcombo()
        Try
            Dim a As String = ""
            dt = clsControlChartSetupDB.FillCombo("0")
            With cboFactory
                .Items.Clear() : .Columns.Clear()
                .DataSource = dt
                .Columns.Add("Code") : .Columns(0).Visible = False
                .Columns.Add("Description") : .Columns(1).Width = 100

                .TextField = "Description"
                .ValueField = "Code"
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If cboFactory.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboFactory.SelectedItem.GetFieldValue("Code")
            End If
            HF.Set("FactoryCode", a)

            dt = clsControlChartSetupDB.FillCombo("1")
            With cboType
                .Items.Clear() : .Columns.Clear()
                .DataSource = dt
                .Columns.Add("Code") : .Columns(0).Visible = False
                .Columns.Add("Description") : .Columns(1).Width = 100

                .TextField = "Description"
                .ValueField = "Code"
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If cboType.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboType.SelectedItem.GetFieldValue("Code")
            End If
            HF.Set("Type", a)

            dt = clsControlChartSetupDB.FillCombo("2", HF.Get("FactoryCode"))
            With cboMachine
                .Items.Clear() : .Columns.Clear()
                .DataSource = dt
                .Columns.Add("Code") : .Columns(0).Visible = False
                .Columns.Add("Description") : .Columns(1).Width = 100

                .TextField = "Description"
                .ValueField = "Code"
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
            End With
            If cboMachine.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboMachine.SelectedItem.GetFieldValue("Code")
            End If
            HF.Set("MachineCode", a)

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, "", 0)
        End Try
    End Sub

    Private Sub up_FillcomboGrid(ByVal cmb As ASPxComboBox, Type As String, Optional Param As String = "")
        dt = clsControlChartSetupDB.FillCombo(Type, Param)
        With cmb
            .Items.Clear() : .Columns.Clear()
            .DataSource = dt
            .Columns.Add("Code") : .Columns(0).Visible = False
            .Columns.Add("Description") : .Columns(1).Width = 100

            .TextField = "Description"
            .ValueField = "Code"
            .DataBind()
            .SelectedIndex = -1
        End With
    End Sub

    Private Sub up_GridLoad()
        Try
            Dim Factory As String = HF.Get("FactoryCode")
            Dim Machine As String = HF.Get("MachineCode")
            Dim Type As String = HF.Get("Type")
            Dim Period As String = Convert.ToDateTime(dtPeriod.Value).ToString("yyyy-MM-dd")

            Dim cls As New clsControlChartSetup With {
                .Factory = Factory,
                .Machine = Machine,
                .Type = Type,
                .Period = Period
            }
            dt = clsControlChartSetupDB.GetList(cls)
            Grid.DataSource = dt
            Grid.DataBind()
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Function up_InsUpd(Type As String, Factory As String, ItemType As String, Machine As String, ItemCheck As String, Start As String, _
                               SpecUSL As String, SpecLSL As String, _
                               XBarCL As String, XBarUCL As String, XBarLCL As String, _
                               RCL As String, RLCL As String, RUCL As String, User As String) As Boolean
        Dim message As String = IIf(Type = "0", "Save data successfully!", "Update data successfully!") '0 Save | 1 Update
        Try
            Dim cls As New clsControlChartSetup With
            {
                .Factory = Factory,
                .ItemType = ItemType,
                .ItemCheck = ItemCheck,
                .Machine = Machine,
                .StartTime = Start,
                .SpecUSL = SpecUSL, .SpecLSL = SpecLSL,
                .XBarCL = XBarCL, .XBarUCL = XBarUCL, .XBarLCL = XBarLCL,
                .RCL = RCL, .RLCL = RLCL, .RUCL = RUCL,
                .User = User
            }
            clsControlChartSetupDB.InsertUpdate(cls)
            show_error(MsgTypeEnum.Success, message, 1)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
    
End Class