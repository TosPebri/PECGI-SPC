Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Drawing

Public Class AlertDashboard
    Inherits System.Web.UI.Page

#Region "Declare"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public dt As DataTable
#End Region

#Region "Events"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            'up_GridLoad()
            GetComboBoxData()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sGlobal.getMenu("A020")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user")
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "A020")
        show_error(MsgTypeEnum.Info, "", 0)
        If AuthUpdate = False Then
            Dim commandColumn = TryCast(Grid.Columns(0), GridViewCommandColumn)
            commandColumn.Visible = False
        End If
    End Sub

    Protected Sub Grid_AfterPerformCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles Grid.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" Then
            up_GridLoad(cboFactory.Value, cboTypeCode.Text, cboMachineProccess.Text)
        End If
    End Sub

    Protected Sub Grid_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles Grid.RowInserting
        e.Cancel = True
        Dim pErr As String = ""
        Dim User As New ClsSPCItemCheckByType With {
            .FactoryCode = e.NewValues("FactoryCode"),
            .FactoryName = cboFactory.Text,
            .ItemTypeCode = e.NewValues("ItemTypeName"),
            .LineCode = e.NewValues("LineCode"),
            .ItemCheck = e.NewValues("ItemCheck"),
            .FrequencyCode = e.NewValues("FrequencyCode"),
            .RegistrationNo = e.NewValues("RegistrationNo"),
            .SampleSize = e.NewValues("SampleSize"),
            .Remark = e.NewValues("Remark"),
            .Evaluation = e.NewValues("Evaluation"),
            .CharacteristicItem = e.NewValues("CharacteristicItem"),
            .ActiveStatus = e.NewValues("ActiveStatus"),
            .UpdateUser = pUser,
            .CreateUser = pUser
        }
        Try
            ClsSPCItemCheckByTypeDB.Insert(User)
            Grid.CancelEdit()
            up_GridLoad(cboFactory.Value, cboTypeCode.Text, cboMachineProccess.Text)
            show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Protected Sub Grid_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles Grid.RowUpdating
        e.Cancel = True
        Dim pErr As String = ""
        Dim LineCode As String = ""
        Dim ItemCheck As String = ""
        LineCode = e.NewValues("LineCode")
        ItemCheck = e.NewValues("ItemCheck")
        Dim User As New ClsSPCItemCheckByType With {
            .FactoryCode = e.NewValues("FactoryCode"),
            .ItemTypeCode = e.NewValues("ItemTypeCode"),
            .LineCode = LineCode.Substring(0, LineCode.IndexOf(" -")),
            .ItemCheck = ItemCheck.Substring(0, ItemCheck.IndexOf(" -")),
            .FrequencyCode = e.NewValues("FrequencyCode"),
            .RegistrationNo = e.NewValues("RegistrationNo"),
            .SampleSize = e.NewValues("SampleSize"),
            .Remark = e.NewValues("Remark"),
            .Evaluation = e.NewValues("Evaluation"),
            .CharacteristicItem = e.NewValues("CharacteristicStatus"),
            .ActiveStatus = e.NewValues("ActiveStatus"),
            .UpdateUser = pUser,
            .CreateUser = pUser
        }
        Try
            ClsSPCItemCheckByTypeDB.Update(User)
            Grid.CancelEdit()
            up_GridLoad(cboFactory.Value, cboTypeCode.Text, cboMachineProccess.Text)
            show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Protected Sub Grid_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles Grid.RowDeleting
        e.Cancel = True
        Try
            Dim FactoryCode As String = e.Values("FactoryCode")
            Dim ItemTypeCode As String = e.Values("ItemTypeCode")
            Dim LineCode As String = e.Values("LineCode")
            Dim ItemCheck As String = e.Values("ItemCheck")

            LineCode = LineCode.Substring(0, LineCode.IndexOf(" -"))
            ItemCheck = ItemCheck.Substring(0, ItemCheck.IndexOf(" -"))

            ClsSPCItemCheckByTypeDB.Delete(FactoryCode, ItemTypeCode, LineCode, ItemCheck)
            Grid.CancelEdit()
            up_GridLoad(cboFactory.Value, cboTypeCode.Text, cboMachineProccess.Text)
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
            If e.Column.FieldName = "ItemCheckCode" Or e.Column.FieldName = "FactoryCode" Or e.Column.FieldName = "ItemTypeCode" Or e.Column.FieldName = "LineCode" Or e.Column.FieldName = "ItemTypeName" Or e.Column.FieldName = "ItemCheck" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Silver
                e.Editor.Visible = True
            End If
        End If

        If Grid.IsNewRowEditing Then
            If e.Column.FieldName = "FactoryCode" Then
                e.Editor.Value = cboFactory.Value
            End If
            If e.Column.FieldName = "ItemTypeCode" Then
                e.Editor.Visible = False
                e.Editor.ForeColor = Color.Silver
            End If
        End If
    End Sub
    Protected Sub Grid_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs) Handles Grid.RowValidating
    End Sub

    Protected Sub Grid_StartRowEditing(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles Grid.StartRowEditing
        If (Not Grid.IsNewRowEditing) Then
            Grid.DoRowValidation()
        End If
        show_error(MsgTypeEnum.Info, "", 0)
    End Sub
    Private Sub GetComboBoxData()
        GetTypeCode()
        GetFactoryCode()
        GetMachineProccess()
    End Sub
    Private Sub GetTypeCode()
        Dim a As String = ""
        dt = ClsSPCItemCheckByTypeDB.GetItemTypeCode()
        With cboTypeCode
            .Items.Clear() : .Columns.Clear()
            .DataSource = dt
            .Columns.Add("ItemTypeCode") : .Columns(0).Visible = False
            .Columns.Add("ItemTypeName") : .Columns(1).Width = 100

            .TextField = "ItemTypeName"
            .ValueField = "ItemTypeCode"
            .DataBind()
            .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
        End With
        If cboTypeCode.SelectedIndex < 0 Then
            a = ""
        Else
            a = cboTypeCode.SelectedItem.GetFieldValue("ItemTypeCode")
        End If
        HF.Set("ItemTypeCode", a)
    End Sub
    Private Sub GetFactoryCode()
        Dim a As String = ""
        dt = ClsSPCItemCheckByTypeDB.GetFactoryCode()
        With cboFactory
            .Items.Clear() : .Columns.Clear()
            .DataSource = dt
            .Columns.Add("FactoryCode") : .Columns(0).Visible = False
            .Columns.Add("FactoryName") : .Columns(1).Width = 100

            .TextField = "FactoryName"
            .ValueField = "FactoryCode"
            .DataBind()
            .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
        End With
    End Sub
    Private Sub GetMachineProccess()
        Dim a As String = ""
        dt = ClsSPCItemCheckByTypeDB.GetMachineProccess()
        With cboMachineProccess
            .Items.Clear() : .Columns.Clear()
            .DataSource = dt
            .Columns.Add("LineCode") : .Columns(0).Visible = False
            .Columns.Add("LineName") : .Columns(1).Width = 100

            .TextField = "LineName"
            .ValueField = "LineCode"
            .DataBind()
            .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
        End With
        If cboMachineProccess.SelectedIndex < 0 Then
            a = ""
        Else
            a = cboMachineProccess.SelectedItem.GetFieldValue("LineCode")
        End If
        HF.Set("LineCode", a)
    End Sub

#End Region

#Region "Functions"
    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        Grid.JSProperties("cp_message") = ErrMsg
        Grid.JSProperties("cp_type") = msgType
        Grid.JSProperties("cp_val") = pVal
    End Sub

    Private Sub up_GridLoad(FactoryCode As String, ItemTypeDescription As String, MachineProccess As String)
        Dim dtItemCheckByType As DataTable
        Try
            dtItemCheckByType = ClsSPCItemCheckByTypeDB.GetList(FactoryCode, ItemTypeDescription, MachineProccess)
            Grid.DataSource = dtItemCheckByType
            Grid.DataBind()
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub
    Private Sub Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Grid.CustomCallback
        Try
            Dim pAction As String = Split(e.Parameters, "|")(0)

            If pAction = "Load" Then
                up_GridLoad(cboFactory.Value, cboTypeCode.Text, cboMachineProccess.Text)
            End If

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub
#End Region

End Class