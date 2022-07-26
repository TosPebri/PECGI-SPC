Imports System.Data.SqlClient

Public Class ClsProcessDB
    Public Shared Function GetList(Optional ByRef pErr As String = "") As List(Of ClsProcess)
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Process_GetList"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                Dim list As New List(Of ClsProcess)
                For i = 0 To dt.Rows.Count - 1
                    Dim pro As New ClsProcess With {.ProcessID = Trim(dt.Rows(i).Item("ProcessID") & ""),
                                                    .ProcessName = Trim(dt.Rows(i).Item("ProcessName") & ""),
                                                    .Remark = Trim(dt.Rows(i).Item("Remark") & "")}

                    list.Add(pro)
                Next
                Return list
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function Insert(ByVal pPro As ClsProcess, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Process_Ins"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("ProcessID", pPro.ProcessID)
                cmd.Parameters.AddWithValue("ProcessName", pPro.ProcessName)
                cmd.Parameters.AddWithValue("Remark", pPro.Remark)
                cmd.Parameters.AddWithValue("User", pPro.CreateUser)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Update(ByVal pPro As ClsProcess, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Process_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("ProcessID", pPro.ProcessID)
                cmd.Parameters.AddWithValue("ProcessName", pPro.ProcessName)
                cmd.Parameters.AddWithValue("Remark", pPro.Remark)
                cmd.Parameters.AddWithValue("User", pPro.UpdateUser)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Delete(ByVal pPro As ClsProcess, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Process_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("ProcessID", pPro.ProcessID)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function isExist(ByVal pProcessID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Process_Exist"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("ProcessID", pProcessID)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function isExistDel(ByVal pProcessID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Process_Exist_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("ProcessID", pProcessID)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)

                If ds.Tables(0).Rows.Count <> 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function
End Class
