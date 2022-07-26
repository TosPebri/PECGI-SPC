Imports System.Data.SqlClient
Public Class ClsMachineDB
    Public Shared Function GetData(Optional ByRef pErr As String = "") As List(Of ClsMachine)
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Machine_GetData"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                Dim list As New List(Of ClsMachine)
                For i = 0 To dt.Rows.Count - 1
                    Dim line As New ClsMachine With {.MachineNo = Trim(dt.Rows(i).Item("MachineNo") & ""),
                                                    .Remark = Trim(dt.Rows(i).Item("Remark") & "")}

                    list.Add(line)
                Next
                Return list
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function Insert(ByVal pMachine As ClsMachine, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Machine_Ins"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("MachineNo", pMachine.MachineNo)
                If IsNothing(pMachine.Remark) Then
                    cmd.Parameters.AddWithValue("Remark", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("Remark", pMachine.Remark)
                End If
                cmd.Parameters.AddWithValue("User", pMachine.CreateUser)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Update(ByVal pMachine As ClsMachine, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Machine_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachine.MachineNo)
                If IsNothing(pMachine.Remark) Then
                    cmd.Parameters.AddWithValue("Remark", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("Remark", pMachine.Remark)
                End If
                cmd.Parameters.AddWithValue("User", pMachine.CreateUser)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Delete(ByVal pMachine As ClsMachine, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Machine_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachine.MachineNo)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function IsExistDel(ByVal pMachine As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Machine_Exist_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachine)
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

    Public Shared Function IsExist(ByVal pMachineNo As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Machine_IsExist"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
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
