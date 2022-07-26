Option Strict On
Imports System.Data.SqlClient
Public Class SiteHome
    Inherits System.Web.UI.MasterPage

#Region "DECLARATION"
    Dim sqlstring As String
    Dim dtView As DataTable
    Dim constr As New SqlClient.SqlConnection(Sconn.Stringkoneksi)
    Public SiteTitle As String
    Public MenuIndex As Integer
    Public IdMenu As String
    Dim script As String
    Dim Title As String
    Public notifQCSA As String
    Public notifTCCSA As String
    Public notifTCCSRA As String
    Public notifQCSResult As String
    Dim pUser As String = ""
    Public StatusApproval1 As String
    Public StatusApproval2 As String
    Public StatusApproval3 As String
    Public StatusApproval4 As String
#End Region

#Region "CONTROL EVENTS"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        MenuIndex = sGlobal.indexMenu
        IdMenu = sGlobal.idMenu.Trim()
        If SiteTitle = "" Then
            Title = ""
        Else
            Title = SiteTitle & " - "
        End If
        PageTitle.InnerText = Title & "Statistics Quality Control"

        ''notif approve
        If IsNothing(Session("user")) Then
            pUser = ""
        Else
            pUser = Session("user").ToString
        End If

        Dim menu As DataSet
        'notif QCS Approval
        If ClsQCSMasterDB.GetDataQELeader(pUser) = True Then
            menu = ClsQCSApprovalDB.GetDataCountQEApproval(pUser)
            notifQCSA = menu.Tables(0).Rows(0)("ApprovalStatus3").ToString()
        ElseIf ClsQCSMasterDB.GetDataForemanLeader(pUser) = True Then
            menu = ClsQCSApprovalDB.GetDataCountForemanApproval(pUser)
            notifQCSA = menu.Tables(0).Rows(0)("ApprovalStatus2").ToString()
        ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser) = True Then
            menu = ClsQCSApprovalDB.GetDataCountLineApproval(pUser)
            notifQCSA = menu.Tables(0).Rows(0)("ApprovalStatus1").ToString()
        End If

        'notif TCCS Approval
        If ClsTCCSApprovalDB.CekStatusSectionHead(pUser) = True Then
            menu = ClsTCCSApprovalDB.GetDataCountApproval(pUser)
            notifTCCSA = menu.Tables(0).Rows(0)("ApprovalStatus").ToString()
        End If

        'notif TCCS Result Approval
        'If ClsTCCSResultApprovalDB.GetDataQESecHead(pUser) = True Then
        '    menu = ClsTCCSResultApprovalDB.GetDataCount4(pUser)
        '    notifTCCSRA = menu.Tables(0).Rows(0)("ApprovalStatus4").ToString()
        'ElseIf ClsTCCSResultApprovalDB.GetDataProdSecHead(pUser) = True Then
        '    menu = ClsTCCSResultApprovalDB.GetDataCount3(pUser)
        '    notifTCCSRA = menu.Tables(0).Rows(0)("ApprovalStatus3").ToString()
        'ElseIf ClsTCCSResultApprovalDB.GetDataLineLeader(pUser) = True Then
        '    menu = ClsTCCSResultApprovalDB.GetDataCount2(pUser)
        '    notifTCCSRA = menu.Tables(0).Rows(0)("ApprovalStatus2").ToString()
        'ElseIf ClsTCCSResultApprovalDB.GetDataQELeader(pUser) = True Then
        '    menu = ClsTCCSResultApprovalDB.GetDataCount1(pUser)
        '    notifTCCSRA = menu.Tables(0).Rows(0)("ApprovalStatus1").ToString()
        'End If

        If ClsTCCSResultApprovalDB.GetDataQESecHead(pUser) = True Then
            StatusApproval4 = "1"
        Else
            StatusApproval4 = "0"
        End If

        If ClsTCCSResultApprovalDB.GetDataProdSecHead(pUser) = True Then
            StatusApproval3 = "1"
        Else
            StatusApproval3 = "0"
        End If

        If ClsTCCSResultApprovalDB.GetDataLineLeader(pUser) = True Then
            StatusApproval2 = "1"
        Else
            StatusApproval2 = "0"
        End If

        If ClsTCCSResultApprovalDB.GetDataQELeader(pUser) = True Then
            StatusApproval1 = "1"
        Else
            StatusApproval1 = "0"
        End If

        menu = ClsTCCSResultApprovalDB.GetDataCount(pUser, StatusApproval1, StatusApproval2, StatusApproval3, StatusApproval4)
        notifTCCSRA = menu.Tables(0).Rows(0)("Approved").ToString()

        If pUser <> "" Then
            Dim User As clsUserSetup = clsUserSetupDB.GetData(pUser)
            If User Is Nothing Then
                notifQCSResult = "''"
            ElseIf User.LineLeaderStatus = "1" Then
                notifQCSResult = clsQCSResultShiftDB.GetApproval(pUser).ToString
            Else
                notifQCSResult = "''"
            End If
        End If        

        If IsNothing(Session("user")) Then
            If Page.IsCallback Then
                DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("~/Default.aspx")
            Else
                Response.Redirect("~/Default.aspx")
            End If
            Exit Sub
        End If

        'If checkPrivilege() = False Then
        '    If Page.IsCallback Then
        '        DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("~/need_authorization.aspx")
        '    Else
        '        Response.Redirect("~/need_authorization.aspx")
        '    End If
        '    Exit Sub
        'End If

        With Me
            .BindMenu()
            lblUser.Text = Session("user").ToString
            lblAdmin.Text = Session("AdminStatus").ToString
        End With

        Session.Timeout = 600 'in minutes
    End Sub

    Public Sub rptMenu_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptMenu.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rptSubMenu As Repeater = TryCast(e.Item.FindControl("rptChildMenu"), Repeater)
            Dim test As String = DirectCast(e.Item.DataItem, System.Data.DataRowView).Row(0).ToString
            Dim q As String
            If Session("AdminStatus").ToString = "1" Then
                q = "SELECT GroupID,RTRIM(MenuDesc) MenuDesc, MenuName, '' HelpName, MenuIndex, '" & notifQCSA & "' NotifQCSA, '" & notifTCCSA & "' NotifTCCSA, '" & notifTCCSRA & "' NotifTCCSRA," & notifQCSResult & " NotifQCSResult, " & vbCrLf &
                "CASE WHEN USM.MenuID = '" & IdMenu & "' THEN 'active' ELSE '' END StatusActiveChild," & vbCrLf &
                "CASE WHEN MenuIndex = '" & MenuIndex & "' THEN 'active' ELSE '' END StatusActiveParent " & vbCrLf &
                "FROM dbo.UserMenu USM " & vbCrLf &
                "WHERE GroupID = '" & test & "' " &
                "and ActiveStatus = '1' " &
                "order by GroupIndex, MenuIndex "
            Else
                q = "SELECT GroupID,RTRIM(MenuDesc) MenuDesc, MenuName, '' HelpName, MenuIndex, '" & notifQCSA & "' NotifQCSA, '" & notifTCCSA & "' NotifTCCSA,'" & notifTCCSRA & "' NotifTCCSRA," & notifQCSResult & " NotifQCSResult, " & vbCrLf &
                "CASE WHEN USM.MenuID = '" & IdMenu & "' THEN 'active' ELSE '' END StatusActiveChild," & vbCrLf &
                "CASE WHEN MenuIndex = '" & MenuIndex & "' THEN 'active' ELSE '' END StatusActiveParent " & vbCrLf &
                "FROM dbo.UserMenu USM LEFT JOIN dbo.UserPrivilege UP ON USM.AppID = UP.AppID AND USM.MenuID = UP.MenuID  " & vbCrLf &
                "WHERE UP.AllowAccess = '1' AND UP.UserID='" & Session("User").ToString & "' " & vbCrLf &
                "and GroupID = '" & test & "' " &
                "and ActiveStatus = '1' AND USM.MenuID <> 'Z010 '" &
                "order by GroupIndex, MenuIndex "
            End If
            rptSubMenu.DataSource = GetData(q)
            rptSubMenu.DataBind()
        End If
    End Sub
#End Region

#Region "FUNCTION"

    Private Function checkPrivilege() As Boolean
        Dim page As String = Server.HtmlEncode(Request.Path)

        Dim resultSplit() As String = page.Split(CChar("/"))
        Dim sqlstring As String = " SELECT * FROM dbo.UserPrivilege UP " & _
                                  " JOIN dbo.UserMenu UM ON UP.MenuID = UM.MenuID " & _
                                  " WHERE UP.AllowAccess = 1 AND UserID='" & Session("User").ToString & "' "
        Dim dt As DataTable = sGlobal.GetData(sqlstring)
        If dt.Rows.Count > 0 Then
            Return True
        End If

        Return False
    End Function

    Private Sub BindMenu()
        Dim q As String
        If Session("AdminStatus").ToString = "1" Then
            q = "select USM.GroupID, USM.GroupIndex, USM.GroupID MenuDesc, USM.GroupID MenuName, " & vbCrLf &
            "max(GroupIcon) HelpName, 0 MenuIndex,'' StatusActiveChild, '' StatusActiveParent  " & vbCrLf &
            "FROM dbo.UserMenu USM " & vbCrLf &
            "where ActiveStatus = '1' " & vbCrLf &
            "group by USM.GroupID, USM.GroupIndex, USM.GroupID, USM.GroupID " & vbCrLf &
            "order by USM.GroupIndex "
        Else
            q = "select USM.GroupID, USM.GroupIndex, USM.GroupID MenuDesc, USM.GroupID MenuName, " & vbCrLf &
                "max(GroupIcon) HelpName, 0 MenuIndex,'' StatusActiveChild, '' StatusActiveParent  " & vbCrLf &
                "FROM dbo.UserMenu USM INNER JOIN dbo.UserPrivilege UP ON USM.AppID = UP.AppID AND USM.MenuID = UP.MenuID  " & vbCrLf &
                "WHERE UP.AllowAccess = '1' " & vbCrLf &
                "AND UP.UserID='" & Session("User").ToString & "' " & vbCrLf &
                "AND USM.MenuID <> 'Z010 ' " & vbCrLf &
                "and ActiveStatus = '1' " & vbCrLf &
                "group by USM.GroupID, USM.GroupIndex, USM.GroupID, USM.GroupID " & vbCrLf &
                "order by USM.GroupIndex "
        End If
        Me.rptMenu.DataSource = GetData(q)
        Me.rptMenu.DataBind()
    End Sub

#End Region

End Class