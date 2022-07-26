Imports System.Data.SqlClient

Public Class clsSetting
    Public Shared Function QCSPath() As String
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select * from Setting"
            Dim cmd As New SqlCommand(q, Cn)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return ""
            Else
                Dim path As String = dt.Rows(0)("QCSPath") & ""
                If path.EndsWith("\") Then
                    path = Mid(path, 1, Len(path) - 1)
                End If
                Return path
            End If
        End Using
    End Function

    Public Shared Function TCCSPath() As String
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select * from Setting"
            Dim cmd As New SqlCommand(q, Cn)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return ""
            Else
                Dim path As String = dt.Rows(0)("TCCSPath") & ""
                If path.EndsWith("\") Then
                    path = Mid(path, 1, Len(path) - 1)
                End If
                Return path
            End If
        End Using
    End Function
End Class
