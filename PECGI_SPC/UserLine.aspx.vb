Public Class UserLine
    Inherits System.Web.UI.Page
    Dim UserID As String

    Private Sub UserLine_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        If Request.QueryString("prm") Is Nothing Then
            Exit Sub
        End If
        UserID = Request.QueryString("prm").ToString()
        txtUser.Text = UserID
        up_GridLoad(UserID)
    End Sub

    Private Sub up_GridLoad(ByVal pUserID As String)
        Dim dsMenu As List(Of clsUserLine)
        dsMenu = clsUserLineDB.GetList(pUserID)
        gridMenu.DataSource = dsMenu
        gridMenu.DataBind()
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, Optional ByVal pVal As Integer = 1)
        gridMenu.JSProperties("cp_message") = ErrMsg
        gridMenu.JSProperties("cp_type") = msgType
        gridMenu.JSProperties("cp_val") = pVal
    End Sub

    Private Sub gridMenu_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs) Handles gridMenu.BatchUpdate
        Dim ls_Allow As String = ""
        Dim LineID As String = ""

        Dim a As Integer = e.UpdateValues.Count
        For iLoop = 0 To a - 1
            ls_Allow = (e.UpdateValues(iLoop).NewValues("Allow").ToString())
            If ls_Allow = True Then ls_Allow = "1" Else ls_Allow = "0"

            LineID = Trim(e.UpdateValues(iLoop).NewValues("LineID").ToString())
            Dim UserLine As New clsUserLine With {
                .UserID = UserID,
                .LineID = LineID,
                .Allow = ls_Allow
            }
            Dim pErr As String = ""
            Dim iUpd As Integer = clsUserLineDB.InsertUpdate(UserLine)
            If pErr <> "" Then
                Exit For
            End If
        Next iLoop
        gridMenu.EndUpdate()
    End Sub

    Private Sub gridMenu_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles gridMenu.CustomCallback
        Dim pAction As String = Split(e.Parameters, "|")(0)
        Dim pUserID As String = Split(e.Parameters, "|")(1)
        up_GridLoad(pUserID)
        If pAction = "save" Then
            show_error(MsgTypeEnum.Success, "Update data successful", 1)
        End If
    End Sub

    Private Sub cbkValid_Callback(source As Object, e As DevExpress.Web.ASPxCallback.CallbackEventArgs) Handles cbkValid.Callback
        Dim pAction As String = Split(e.Parameter, "|")(0)
        Dim FromUserID As String = Split(e.Parameter, "|")(1)
        Dim TouserID As String = Split(e.Parameter, "|")(2)
        Dim CreateUser As String = Session("user") & ""
        If FromUserID <> "" Then
            clsUserLineDB.Copy(FromUserID, TouserID, CreateUser)
        End If
    End Sub
End Class