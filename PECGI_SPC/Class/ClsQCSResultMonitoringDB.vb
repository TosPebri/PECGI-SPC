Imports System.Data.SqlClient
Public Class ClsQCSResultMonitoringDB
    Public Shared Function GetDataLine(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSResultMonitoring_GetLine"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
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

    Public Shared Function GetDataPart(ByVal pUserID As String, ByVal pLineID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSResultMonitoring_GetPart"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
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

    Public Shared Function GetTime(Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSResultMonitoring_GetTime"

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

    Public Shared Function GetProcess(Optional ByRef perr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSResultMonitoring_GetProcess"
                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataTable
                da.Fill(ds)
                Return ds
            End Using
        Catch ex As Exception
            perr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetList(ByVal pDate As String, ByVal pLineID As String, ByVal pPartID As String, ByVal pQCSStatus As String, ByVal pUser As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSResultMonitoring_GetList"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("Date", pDate)
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
                'If pApprovalStatus = "ALL" Then
                '    cmd.Parameters.AddWithValue("ApprovalStatus", 2)
                'ElseIf pApprovalStatus = "Approved" Then
                '    cmd.Parameters.AddWithValue("ApprovalStatus", 1)
                'ElseIf pApprovalStatus = "Unapproved" Then
                '    cmd.Parameters.AddWithValue("ApprovalStatus", 0)
                'End If

                If pQCSStatus = "ALL" Then
                    cmd.Parameters.AddWithValue("pQCSStatus", 2)
                ElseIf pQCSStatus = "OK" Then
                    cmd.Parameters.AddWithValue("pQCSStatus", 1)
                ElseIf pQCSStatus = "NG" Then
                    cmd.Parameters.AddWithValue("pQCSStatus", 0)
                End If
                cmd.Parameters.AddWithValue("UserID", pUser)
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
End Class
