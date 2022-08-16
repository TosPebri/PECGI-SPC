Imports DevExpress.Web

Public Class ProdSampleInput
    Inherits System.Web.UI.Page
    Dim pUser As String = ""
    Public AuthApprove As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public ValueType As String
    Dim GlobalPrm As String = ""

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        grid.JSProperties("cp_message") = ErrMsg
        grid.JSProperties("cp_type") = msgType
        grid.JSProperties("cp_val") = pVal
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GlobalPrm = Request.QueryString("Date") & ""
        sGlobal.getMenu("B020")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user") & ""
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B040")
        show_error(MsgTypeEnum.Info, "", 0)
        If Not IsPostBack And Not IsCallback Then
            up_FillCombo()
            If GlobalPrm <> "" Then
                dtDate.Value = CDate(Request.QueryString("Date"))
            Else
                dtDate.Value = Now.Date
            End If
        End If
    End Sub

    Private Sub up_FillCombo()
        cboFactory.DataSource = clsFactoryDB.GetList
        cboFactory.DataBind()

        cboType.DataSource = clsItemTypeDB.GetList
        cboType.DataBind()

        cboItemCheck.DataSource = clsItemCheckDB.GetList
        cboItemCheck.DataBind()

        cboLine.DataSource = ClsLineDB.GetList
        cboLine.DataBind()
    End Sub

    Protected Sub grid_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles grid.RowInserting
        e.Cancel = True
    End Sub

    Private Sub grid_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles grid.RowUpdating
        e.Cancel = True
    End Sub

    Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles grid.RowDeleting
        e.Cancel = True
    End Sub

    Protected Sub grid_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles grid.AfterPerformCallback
        Dim commandColumn = TryCast(grid.Columns(0), GridViewCommandColumn)
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then

        End If
    End Sub

    Private Sub GridLoad()
        Dim ErrMsg As String = ""


    End Sub
End Class