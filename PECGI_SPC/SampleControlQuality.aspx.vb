Public Class SampleControlQuality
    Inherits System.Web.UI.Page
    Dim pUser As String = ""
    Public AuthApprove As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public ValueType As String
    Dim GlobalPrm As String = ""

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        gridX.JSProperties("cp_message") = ErrMsg
        gridX.JSProperties("cp_type") = msgType
        gridX.JSProperties("cp_val") = pVal
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GlobalPrm = Request.QueryString("Date") & ""
        sGlobal.getMenu("B060")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user") & ""
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B060")
        show_error(MsgTypeEnum.Info, "", 0)
        If Not IsPostBack And Not IsCallback Then
            up_FillCombo()
            If GlobalPrm <> "" Then
                dtDate.Value = CDate(Request.QueryString("Date"))
                Dim FactoryCode As String = Request.QueryString("FactoryCode")
                Dim ItemTypeCode As String = Request.QueryString("ItemTypeCode")
                Dim Line As String = Request.QueryString("Line")
                Dim ItemCheckCode As String = Request.QueryString("ItemCheckCode")
                Dim ProdDate As String = Request.QueryString("ProdDate")
                Dim Shift As String = Request.QueryString("Shift")
                Dim Sequence As String = Request.QueryString("Sequence")

            Else
                dtDate.Value = Now.Date
                InitCombo()
            End If
        End If
    End Sub

    Private Sub InitCombo()
        dtDate.Value = CDate("2022-08-03")
        cboFactory.Value = "F001"
        cboType.DataSource = clsItemTypeDB.GetList(cboFactory.Value)
        cboType.DataBind()
        cboType.Value = "TPMSBR011"

        cboLine.DataSource = ClsLineDB.GetList("admin", cboFactory.Value, cboType.Value)
        cboLine.DataBind()
        cboLine.Value = "015"

        cboItemCheck.DataSource = clsItemCheckDB.GetList(cboFactory.Value, cboType.Value, cboLine.Value)
        cboItemCheck.DataBind()
        cboItemCheck.Value = "IC021"

        cboShift.DataSource = clsFrequencyDB.GetShift(cboFactory.Value, cboType.Value, cboLine.Value, cboItemCheck.Value)
        cboShift.DataBind()
        cboShift.Value = "SH001"

        cboSeq.Value = "1"
        cboSeq.DataSource = clsFrequencyDB.GetSequence(cboFactory.Value, cboType.Value, cboLine.Value, cboItemCheck.Value)
        cboSeq.DataBind()
    End Sub

    Private Sub up_FillCombo()
        cboFactory.DataSource = clsFactoryDB.GetList
        cboFactory.DataBind()
    End Sub

End Class