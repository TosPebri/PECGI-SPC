Imports DevExpress.Web
Imports DevExpress.Web.Data

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
                Dim FactoryCode As String = Request.QueryString("FactoryCode")
                Dim ItemTypeCode As String = Request.QueryString("ItemTypeCode")
                Dim Line As String = Request.QueryString("Line")
                Dim ItemCheckCode As String = Request.QueryString("ItemCheckCode")
                Dim ProdDate As String = Request.QueryString("ProdDaet")
                GridLoad(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
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
    End Sub

    Protected Sub grid_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles grid.RowInserting
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

    Private Sub GridLoad(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String)
        Dim ErrMsg As String = ""
        Dim dt As DataTable = clsSPCResultDetailDB.GetTable(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
        grid.DataSource = dt
        grid.DataBind()
    End Sub

    Private Sub grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles grid.CustomCallback
        Dim pFunction As String = Split(e.Parameters, "|")(0)
        Dim pFactory As String = Split(e.Parameters, "|")(1)
        Dim pItemType As String = Split(e.Parameters, "|")(2)
        Dim pLine As String = Split(e.Parameters, "|")(3)
        Dim pItemCheck As String = Split(e.Parameters, "|")(4)
        Dim pDate As String = Split(e.Parameters, "|")(5)
        Select Case pFunction
            Case "clear"

            Case "load", "save", "approve"
                GridLoad(pFactory, pItemType, pLine, pItemCheck, pDate)
        End Select
    End Sub

    Private Sub grid_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles grid.RowUpdating
        e.Cancel = True
    End Sub

    Private Sub cbkRefresh_Callback(source As Object, e As CallbackEventArgs) Handles cbkRefresh.Callback
        cbkRefresh.JSProperties("cpUSL") = 1
        cbkRefresh.JSProperties("cpLSL") = 2
        cbkRefresh.JSProperties("cpUCL") = 3
        cbkRefresh.JSProperties("cpLCL") = 4
        cbkRefresh.JSProperties("cpMin") = 5
        cbkRefresh.JSProperties("cpMax") = 6
        cbkRefresh.JSProperties("cpAve") = 7
        cbkRefresh.JSProperties("cpR") = 8
        cbkRefresh.JSProperties("cpNG") = 9
    End Sub

    Private Sub cboType_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboType.Callback

    End Sub

    Private Sub cboLine_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboLine.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        cboLine.DataSource = ClsLineDB.GetList(FactoryCode)
        cboLine.DataBind()
    End Sub

    Private Sub cboItemCheck_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboItemCheck.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameter, "|")(1)
        Dim LineCode As String = Split(e.Parameter, "|")(2)
        cboItemCheck.DataSource = clsItemCheckDB.GetList(FactoryCode, ItemTypeCode, LineCode)
        cboItemCheck.DataBind()
    End Sub
End Class