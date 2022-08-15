Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Drawing

Public Class ItemCheck
    Inherits System.Web.UI.Page

#Region "Declare"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
#End Region

#Region "Events"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            up_GridLoad()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sGlobal.getMenu("A010")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user")
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "A010")
        show_error(MsgTypeEnum.Info, "", 0)
        If AuthUpdate = False Then
            Dim commandColumn = TryCast(Grid.Columns(0), GridViewCommandColumn)
            commandColumn.Visible = False
        End If
    End Sub

    Protected Sub Grid_AfterPerformCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles Grid.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            up_GridLoad()
        End If
    End Sub

    Protected Sub Grid_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles Grid.RowInserting
        e.Cancel = True
        Dim pErr As String = ""
        Dim User As New ClsSPCItemCheckMaster With {
            .ItemCheckCode = e.NewValues("ItemCheckCode"),
            .ItemCheck = e.NewValues("ItemCheck"),
            .UnitMeasurement = e.NewValues("UnitMeasurement"),
            .Description = e.NewValues("Description"),
            .ActiveStatus = e.NewValues("ActiveStatus"),
            .UpdateUser = pUser,
            .CreateUser = pUser
        }
        Try
            Dim CheckUser As ClsSPCItemCheckMaster = ClsSPCItemCheckMasterDB.GetData(User.ItemCheckCode)
            If CheckUser IsNot Nothing Then
                show_error(MsgTypeEnum.ErrorMsg, "Item already exists!", 1)
                Return
            End If
            ClsSPCItemCheckMasterDB.Insert(User)
            Grid.CancelEdit()
            up_GridLoad()
            show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Protected Sub Grid_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles Grid.RowUpdating
        e.Cancel = True
        Dim User As New ClsSPCItemCheckMaster With {
            .ItemCheckCode = e.NewValues("ItemCheckCode"),
            .ItemCheck = e.NewValues("ItemCheck"),
            .UnitMeasurement = e.NewValues("UnitMeasurement"),
            .Description = e.NewValues("Description"),
            .ActiveStatus = e.NewValues("ActiveStatus"),
            .UpdateUser = pUser
        }
        Try
            ClsSPCItemCheckMasterDB.Update(User)
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
            Dim ItemCheckCode As String = e.Values("ItemCheckCode")
            Dim ItemCheck As String = e.Values("ItemCheck")
            Dim Description As String = e.Values("Description")
            Dim ActiveStatus As String = e.Values("ActiveStatus")
            Dim ValidationDelete As ClsSPCItemCheckMaster = ClsSPCItemCheckMasterDB.ValidationDelete(ItemCheckCode)
            If ValidationDelete IsNot Nothing Then
                show_error(MsgTypeEnum.ErrorMsg, "Can't Delete, Item already used!", 1)
                Return
            End If
            ClsSPCItemCheckMasterDB.Delete(ItemCheckCode)
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
            If e.Column.FieldName = "ItemCheckCode" Then
                e.Editor.ReadOnly = True
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

#End Region

#Region "Functions"
    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        Grid.JSProperties("cp_message") = ErrMsg
        Grid.JSProperties("cp_type") = msgType
        Grid.JSProperties("cp_val") = pVal
    End Sub

    Private Sub up_GridLoad()
        Dim Users As List(Of ClsSPCItemCheckMaster)
        Try
            Users = ClsSPCItemCheckMasterDB.GetList()
            Grid.DataSource = Users
            Grid.DataBind()
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

#End Region

End Class