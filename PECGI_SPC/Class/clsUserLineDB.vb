Imports System.Data.SqlClient

Public Class clsUserLineDB
    Public Shared Function InsertUpdate(UL As clsUserLine) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String
            q = "if not exists (select top 1 * from dbo.spc_UserLine where AppID = 'SPC' " & vbCrLf &
                "And UserID = @UserID and LineCode = @LineCode) " & vbCrLf &
                "Insert into dbo.spc_UserLine (AppID, UserID, LineCode, AllowShow, AllowUpdate, AllowVerify, RegisterDate, RegisterUser, UpdateDate, UpdateUser) values (" & vbCrLf &
                "'SPC', @UserID, @LineCode, @AllowShow, @AllowUpdate, @AllowVerify, GETDATE(), @UserID,GETDATE(), @UserID)" & vbCrLf &
                "ELSE" & vbCrLf &
                "BEGIN"

            q = q + " UPDATE dbo.spc_UserLine " & vbCrLf &
                      " SET AllowShow=@AllowShow, " & vbCrLf &
                      " AllowUpdate=@AllowUpdate ," & vbCrLf &
                      " AllowVerify=@AllowVerify ," & vbCrLf &
                      " UpdateDate=GetDate()," & vbCrLf &
                      " UpdateUser=@UserID " & vbCrLf &
                      " WHERE AppID='SPC' AND UserID =@UserID AND LineCode=@LineCode " & vbCrLf &
                      " END "
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("UserID", UL.UserID)
            cmd.Parameters.AddWithValue("LineCode", UL.LineID)
            cmd.Parameters.AddWithValue("AllowShow", UL.AllowShow)
            cmd.Parameters.AddWithValue("AllowUpdate", UL.AllowUpdate)
            cmd.Parameters.AddWithValue("AllowVerify", UL.AllowVerify)

            Dim i As Integer = cmd.ExecuteNonQuery
            Return i
        End Using
    End Function

    Public Shared Function Delete(UserID As String) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Delete dbo.spc_UserLine where " & vbCrLf &
                "UserID = @UserID and AppID = 'QCS' "
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("UserID", UserID)
            Dim i As Integer = cmd.ExecuteNonQuery
            Return i
        End Using
    End Function

    Public Shared Function GetData(UserID As String, LineID As String) As clsUserLine
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Select * from dbo.spc_UserLine where " & vbCrLf &
                "UserID = @UserID and LineCode = @LineCode and AppID = 'SPC' "
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("UserID", UserID)
            cmd.Parameters.AddWithValue("LineCode", LineID)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return Nothing
            Else
                Dim Ul As New clsUserLine
                Ul.UserID = UserID
                Ul.LineID = LineID
                Return Ul
            End If
        End Using
    End Function

    Public Shared Function GetList(pUserID As String) As List(Of clsUserLine)
        Try
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                Dim sql As String =
                    "select A.LineCode, B.LineName, A.AllowShow, A.AllowUpdate, A.AllowVerify " & vbCrLf &
                    "from dbo.spc_UserLine A left join dbo.MS_Line B on A.LineCode = B.LineCode " & vbCrLf &
                    "Where A.UserID = '" & pUserID & "' and A.AppID='SPC' " & vbCrLf &
                    "ORDER BY A.LineCode "
                Dim Cmd As New SqlCommand(sql, cn)
                Dim da As New SqlDataAdapter(Cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                Dim Lines As New List(Of clsUserLine)
                For i = 0 To dt.Rows.Count - 1
                    Dim Menu As New clsUserLine
                    Menu.LineID = dt.Rows(i)("LineCode")
                    Menu.LineName = dt.Rows(i)("LineName")
                    Menu.AllowShow = dt.Rows(i)("AllowShow")
                    Menu.AllowUpdate = dt.Rows(i)("AllowUpdate")
                    Menu.AllowVerify = dt.Rows(i)("AllowVerify")
                    Lines.Add(Menu)
                Next
                Return Lines
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Copy(FromUserID As String, ToUserID As String, CreateUser As String) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "delete from dbo.spc_UserLine where UserID = @ToUserID and AppID = 'SPC' "
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("ToUserID", ToUserID)

            Dim i As Integer = cmd.ExecuteNonQuery
            q = "Insert into dbo.spc_UserLine (AppID, UserID, LineCode, AllowShow, AllowUpdate, AllowVerify, RegisterDate, RegisterUser, UpdateDate, UpdateUser) " & vbCrLf &
                "select AppID, @ToUserID, LineCode, AllowShow, AllowUpdate, AllowVerify, GETDATE(), @CreateUser, GETDATE(), @CreateUser " & vbCrLf &
                "from dbo.spc_UserLine where UserID = @FromUserID and AppID = 'SPC' "
            cmd = New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("FromUserID", FromUserID)
            cmd.Parameters.AddWithValue("ToUserID", ToUserID)
            cmd.Parameters.AddWithValue("CreateUser", CreateUser)
            i = cmd.ExecuteNonQuery()
            Return i
        End Using
    End Function
End Class
