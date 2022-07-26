Imports System.Data.SqlClient

Public Class ClsSubLineDB

    'Public Shared Function GetList(Optional ByRef pErr As String = "") As List(Of ClsSubLine)
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_SubLine_GetList"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            Dim da As New SqlDataAdapter(cmd)
    '            Dim dt As New DataTable
    '            da.Fill(dt)

    '            Dim list As New List(Of ClsSubLine)
    '            For i = 0 To dt.Rows.Count - 1
    '                Dim subline As New ClsSubLine With {.LineID = Trim(dt.Rows(i).Item("LineID") & ""),
    '                                                    .LineName = Trim(dt.Rows(i).Item("LineName") & ""),
    '                                                    .SubLineID = Trim(dt.Rows(i).Item("SubLineID") & ""),
    '                                                    .ProcessID = Trim(dt.Rows(i).Item("ProcessID") & ""),
    '                                                    .ProcessName = Trim(dt.Rows(i).Item("ProcessName") & ""),
    '                                                    .MachineNo = Trim(dt.Rows(i).Item("MachineNo") & ""),
    '                                                    .Description = Trim(dt.Rows(i).Item("Description") & "")}

    '                list.Add(subline)
    '            Next
    '            Return list
    '        End Using
    '    Catch ex As Exception
    '        pErr = ex.Message
    '        Return Nothing
    '    End Try
    'End Function

    Public Shared Function GetList(Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SubLine_GetList"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)
                Return ds
            End Using
        Catch ex As Exception
            perr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetDataLine(Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SubLine_GetLine"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                Return dt
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetDataProcess(Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SubLine_GetProcess"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                Return dt
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetDataMachine(Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SubLine_GetMachine"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                Return dt
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetDataSubLine(ByVal pLineID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SubLine_GetSubLine"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                Return dt
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function Insert(ByVal pLine As ClsSubLine, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SubLine_Ins"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLine.LineID)
                cmd.Parameters.AddWithValue("SubLineID", pLine.SubLineID)
                cmd.Parameters.AddWithValue("ProcessID", pLine.ProcessID)
                cmd.Parameters.AddWithValue("MachineNo", pLine.MachineNo)
                cmd.Parameters.AddWithValue("Description", pLine.Description)
                cmd.Parameters.AddWithValue("User", pLine.CreateUser)


                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Update(ByVal pLine As ClsSubLine, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SubLine_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLine.LineID)
                cmd.Parameters.AddWithValue("SubLineID", pLine.SubLineID)
                cmd.Parameters.AddWithValue("ProcessID", pLine.ProcessID)
                cmd.Parameters.AddWithValue("MachineNo", pLine.MachineNo)
                cmd.Parameters.AddWithValue("Description", pLine.Description)
                cmd.Parameters.AddWithValue("User", pLine.UpdateUser)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Delete(ByVal pLine As ClsSubLine, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SubLine_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLine.LineID)
                cmd.Parameters.AddWithValue("SubLineID", pLine.SubLineID)
                cmd.Parameters.AddWithValue("ProcessID", pLine.ProcessID)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function isExist(ByVal pLineID As String, ByVal pSubLineID As String, ByVal pProcessID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SubLine_Exist"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
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

    Public Shared Function isExistDel(ByVal pSubLineID As String, ByVal pLineID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SubLine_Exist_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
                cmd.Parameters.AddWithValue("LineID", pLineID)
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

    Public Shared Function isExistMachine(ByVal pMachineNo As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SubLine_ExistMachine"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
                'cmd.Parameters.AddWithValue("LineID", pLineID)
                'cmd.Parameters.AddWithValue("ProcessID", pProcessID)
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
