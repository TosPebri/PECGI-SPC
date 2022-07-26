Imports System.Data.SqlClient
Public Class ClsApprovalSettingDB
    Public Shared Function GetDataQELeader(Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QEApprover_GetQELeader"

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

    Public Shared Function GetDataQESectionHead(Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QEApprover_GetQESectionHead"

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

    Public Shared Function GetData(Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QEApprover_GetData"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)
                If ds.Tables(0).Rows.Count <> 0 Then
                    Return ds
                Else
                    Return Nothing
                End If

            End Using
        Catch ex As Exception
            perr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function Insert(ByVal pApprovalSetup As ClsApprovalSetting, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QEApprover_Ins"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("QELeader1", pApprovalSetup.QELeader1)
                cmd.Parameters.AddWithValue("QELeader2", pApprovalSetup.QELeader2)
                cmd.Parameters.AddWithValue("QESectionHead1", pApprovalSetup.QESectionHead1)
                cmd.Parameters.AddWithValue("QESectionHead2", pApprovalSetup.QESectionHead2)
                cmd.Parameters.AddWithValue("CreateUser", pApprovalSetup.CreateUser)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Update(ByVal pApr As ClsApprovalSetting, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QEApprover_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("QELeader1", pApr.QELeader1)
                cmd.Parameters.AddWithValue("QELeader2", pApr.QELeader2)
                cmd.Parameters.AddWithValue("QESectionHead1", pApr.QESectionHead1)
                cmd.Parameters.AddWithValue("QESectionHead2", pApr.QESectionHead2)
                cmd.Parameters.AddWithValue("UpdateUser", pApr.UpdateUser)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function isExist(Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QEApprover_Exist"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
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
