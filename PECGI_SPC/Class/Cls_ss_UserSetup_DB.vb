Imports System.Data.SqlClient
Public Class Cls_ss_UserSetup_DB
    Public Shared Function GetList(Optional ByRef pErr As String = "") As List(Of Cls_ss_UserSetup)
        Try
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                Dim sql As String
                Dim clsDESEncryption As New clsDESEncryption("TOS")
                sql = " SELECT * FROM dbo.UserSetup " & vbCrLf

                Dim Cmd As New SqlCommand(sql, cn)
                Dim da As New SqlDataAdapter(Cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                Dim Users As New List(Of Cls_ss_UserSetup)
                For i = 0 To dt.Rows.Count - 1
                    Dim User As New Cls_ss_UserSetup With {
                            .AppID = dt.Rows(i)("AppID"),
                            .UserID = Trim(dt.Rows(i)("UserID")),
                            .UserName = Trim(dt.Rows(i)("UserName")),
                            .Password = clsDESEncryption.DecryptData(dt.Rows(i)("Password")),
                            .Description = Trim(dt.Rows(i)("Description") & ""),
                            .Locked = dt.Rows(i)("Locked"),
                            .AdminStatus = dt.Rows(i)("AdminStatus") & "",
                            .RegisterDate = dt.Rows(i)("CreateDate") & "",                            
                            .LastUpdate = dt.Rows(i)("UpdateDate") & ""}
                    Users.Add(User)
                Next
                Return Users
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetUserID(ByVal pUserID As String, ByVal pPassword As String, Optional ByRef pErr As String = "") As Cls_ss_UserSetup
        Try
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                Dim sql As String = "SELECT * FROM UserSetup WHERE userID='" & Replace(pUserID, "'", "''") & "' AND Password='" & Replace(pPassword, "'", "''") & "'"
                Dim Cmd As New SqlCommand(sql, cn)
                Dim da As New SqlDataAdapter(Cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    Dim User As New Cls_ss_UserSetup With {.AppID = dt.Rows(0)("AppID"),
                                                           .UserID = dt.Rows(0)("UserID"),
                                                           .UserName = dt.Rows(0)("UserName"),
                                                           .UserType = dt.Rows(0)("UserType"),
                                                           .Password = dt.Rows(0)("Password"),
                                                           .Locked = dt.Rows(0)("Locked") & "",
                                                           .AdminStatus = dt.Rows(0)("AdminStatus"),
                                                           .Description = dt.Rows(0)("Description"),
                                                           .SecurityQuestion = dt.Rows(0)("SecurityQuestion"),
                                                           .SecurityAnswer = dt.Rows(0)("SecurityAnswer"),
                                                           .FirstLoginFlag = dt.Rows(0)("FirstLoginFlag"),
                                                           .LastLogin = dt.Rows(0)("LastLogin"),
                                                           .FailedLogin = dt.Rows(0)("FailedLogin"),
                                                           .PasswordHint = dt.Rows(0)("PasswordHint")}
                    Return User
                End If
                Return Nothing
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetUserLock(ByVal pUserID As String, ByVal pPassword As String,Optional ByRef pErr As String = "") As Boolean
        Dim Locked As Boolean = False
        Try
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                Dim sql As String = "SELECT * FROM UserSetup WHERE userID='" & Replace(pUserID, "'", "''") & "' AND Password='" & Replace(pPassword, "'", "''") & "' and Locked='0'"
                Dim Cmd As New SqlCommand(sql, cn)
                Dim da As New SqlDataAdapter(Cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    Locked = True
                End If
                Return Locked
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Locked
        End Try
    End Function

    Public Shared Function GetListMenu(ByVal pUserID As String, Optional ByRef pErr As String = "") As List(Of Cls_ss_UserMenu)
        Try
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                Dim sql As String = "  SELECT GroupID, USM.MenuID,   " & vbCrLf & _
                  "  MenuDesc, " & vbCrLf & _
                  "  ISNULL(AllowAccess,'0') AS AllowAccess,  " & vbCrLf & _
                  "  ISNULL(AllowUpdate,'0') AS AllowUpdate  " & vbCrLf & _
                  "  FROM UserMenu USM " & vbCrLf & _
                  "  LEFT JOIN (SELECT * FROM UserPrivilege WHERE UserID='" & pUserID & "' ) UP   " & vbCrLf & _
                  "  ON USM.AppID = UP.AppID AND USM.MenuID=UP.MenuID    " & vbCrLf & _
                  "  WHERE USM.AppID='QCS' " & vbCrLf & _
                  "  ORDER BY USM.MenuID  "
                Dim Cmd As New SqlCommand(sql, cn)
                Dim da As New SqlDataAdapter(Cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                Dim Menus As New List(Of Cls_ss_UserMenu)
                For i = 0 To dt.Rows.Count - 1
                    Dim Menu As New Cls_ss_UserMenu
                    Menu.GroupID = dt.Rows(i)("GroupID")
                    Menu.MenuID = dt.Rows(i)("MenuID")
                    Menu.MenuDesc = dt.Rows(i)("MenuDesc")                    
                    Menu.AllowAccess = dt.Rows(i)("AllowAccess")
                    Menu.AllowUpdate = dt.Rows(i)("AllowUpdate")
                    Menus.Add(Menu)
                Next
                Return Menus
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetMenuDS(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataSet
        Try
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                Dim sql As String = "SELECT GroupID, USM.MenuID, MenuDesc,   " & vbCrLf & _
                  "  ISNULL(AllowAccess,'0') AS AllowAccess,  " & vbCrLf & _
                  "  ISNULL(AllowUpdate,'0') AS AllowUpdate  " & vbCrLf & _
                  "  FROM UserMenu USM " & vbCrLf & _
                  "  LEFT JOIN (SELECT * FROM UserPrivilege WHERE UserID='" & pUserID & "' ) UP   " & vbCrLf & _
                  "  ON USM.AppID = UP.AppID AND USM.MenuID=UP.MenuID    " & vbCrLf & _
                  "  WHERE USM.AppID='P01' AND GroupID <> 'Security System' " & vbCrLf & _
                  "  ORDER BY USM.MenuID  "

                Dim dsMenu As New DataSet
                Dim cmd As New SqlCommand(sql, cn)
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dsMenu)
                Return dsMenu
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetValidate(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataSet
        Try
            pErr = ""
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                cn.Open()
                Dim sql As String = ""
                sql = "SELECT * FROM dbo.UserSetup WHERE AppID='P01' AND UserID='" & pUserID & "' "

                Dim ds As New DataSet
                Dim cmd As New SqlCommand(sql, cn)
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(ds)
                Return ds

            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function Insert(ByVal pUser As Cls_ss_UserSetup, Optional ByRef pErr As String = "") As Integer
        Try
            pErr = ""
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                cn.Open()
                Dim sql As String = "INSERT INTO UserSetup ([AppID],[UserID],[Name],[Password],[Description],[LockedCls],[StatusAdminCls],[Company]" & _
                                    ",[RegisterDate]) VALUES (@AppID,@UserID,@Name,@Password,@Description,@LockedCls,@StatusAdminCls,@Company" & _
                                    ",GETDATE())"
                Dim Cmd As New SqlCommand(sql, cn)
                With Cmd.Parameters
                    .AddWithValue("AppID", "P01")
                    .AddWithValue("UserID", pUser.UserID)
                    .AddWithValue("Name", pUser.Name)
                    .AddWithValue("Password", pUser.Password)
                    .AddWithValue("Description", pUser.Description)
                    .AddWithValue("LockedCls", pUser.LockedCls)
                    .AddWithValue("StatusAdminCls", pUser.StatusAdminCls)
                    .AddWithValue("Company", "PEMI")
                End With
                Return Cmd.ExecuteNonQuery
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try

    End Function

    Public Shared Function Update(ByVal pUser As Cls_ss_UserSetup, Optional ByRef pErr As String = "") As Integer
        Try
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                cn.Open()
                Dim sql As String = "UPDATE UserSetup SET Name=@Name, Password=@Password," & _
                                    "Description=@Description, LockedCls=@LockedCls, StatusAdminCls=@StatusAdminCls, Company=@Company, LastUpdate=GETDATE() WHERE UserID=@UserID"
                Dim Cmd As New SqlCommand(sql, cn)
                With Cmd.Parameters
                    .AddWithValue("UserID", pUser.UserID)
                    .AddWithValue("Name", pUser.Name)
                    .AddWithValue("Password", pUser.Password)
                    .AddWithValue("Description", pUser.Description)
                    .AddWithValue("LockedCls", pUser.LockedCls)
                    .AddWithValue("StatusAdminCls", pUser.StatusAdminCls)
                    .AddWithValue("Company", "PEMI")
                End With
                Return Cmd.ExecuteNonQuery
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try

    End Function

    Public Shared Function Delete(ByVal pUser As Cls_ss_UserSetup, Optional ByRef pErr As String = "") As Integer
        pErr = ""
        Try
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                cn.Open()
                Dim sql As String = "DELETE UserSetup WHERE UserID=@UserID"
                Dim Cmd As New SqlCommand(sql, cn)
                With Cmd.Parameters
                    .AddWithValue("UserID", pUser.UserID)
                End With
                Return Cmd.ExecuteNonQuery
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function
End Class
