Imports System.Data.SqlClient

Public Class clsUserLineDB
    Public Shared Function InsertUpdate(UL As clsUserLine) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String
            If UL.Allow = "1" Then
                q = "if not exists (select top 1 * from UserLine where AppID = 'QCS' " & vbCrLf & _
                    "And UserID = @UserID and LineID = @LineID) " & vbCrLf & _
                    "Insert into UserLine (AppID, UserID, LineID) values (" & vbCrLf & _
                    "'QCS', @UserID, @LineID )"
            Else
                q = "Delete from UserLine where AppID = 'QCS' And UserID = @UserID and LineID = @LineID"
            End If
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("UserID", UL.UserID)
            cmd.Parameters.AddWithValue("LineID", UL.LineID)
            Dim i As Integer = cmd.ExecuteNonQuery
            Return i
        End Using
    End Function

    Public Shared Function Delete(UserID As String) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Delete UserLine where " & vbCrLf & _
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
            Dim q As String = "Select * from UserLine where " & vbCrLf & _
                "UserID = @UserID and LineID = @LineID and AppID = 'QCS' "
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("UserID", UserID)
            cmd.Parameters.AddWithValue("LineID", LineID)
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
        Using cn As New SqlConnection(Sconn.Stringkoneksi)
            Dim sql As String = _
                "select L.LineID, L.LineName, case when U.UserID is Null then 0 else 1 end Allow " & vbCrLf & _
                "from Line L left join UserLine U on U.LineID = L.LineID " & vbCrLf & _
                "and U.UserID = '" & pUserID & "' and U.AppID='QCS' " & vbCrLf & _
                "ORDER BY L.LineID "
            Dim Cmd As New SqlCommand(sql, cn)
            Dim da As New SqlDataAdapter(Cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Dim Lines As New List(Of clsUserLine)
            For i = 0 To dt.Rows.Count - 1
                Dim Menu As New clsUserLine
                Menu.LineID = dt.Rows(i)("LineID")
                Menu.LineName = dt.Rows(i)("LineName")
                Menu.Allow = dt.Rows(i)("Allow")
                Lines.Add(Menu)
            Next
            Return Lines
        End Using
    End Function

    Public Shared Function Copy(FromUserID As String, ToUserID As String, CreateUser As String) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "delete from UserLine where UserID = @ToUserID and AppID = 'QCS' "
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("ToUserID", ToUserID)

            Dim i As Integer = cmd.ExecuteNonQuery
            q = "Insert into UserLine (AppID, UserID, LineID) " & vbCrLf & _
                "select AppID, @ToUserID, LineID " & vbCrLf & _
                "from UserLine where UserID = @FromUserID and AppID = 'QCS' "
            cmd = New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("FromUserID", FromUserID)
            cmd.Parameters.AddWithValue("ToUserID", ToUserID)
            cmd.Parameters.AddWithValue("CreateUser", CreateUser)
            i = cmd.ExecuteNonQuery()
            Return i
        End Using
    End Function
End Class
