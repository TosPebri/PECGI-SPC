Imports System.Data.SqlClient

Public Class ClsTCCSResultApprovalDB
    Public Shared Function GetDataLine(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_GetLine"

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

    Public Shared Function GetDataSubLine(ByVal pLineID As String, ByVal pUserID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_GetSubLine"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
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

    Public Shared Function GetDataMachine(ByVal pLineID As String, ByVal pSubLineID As String, ByVal pUserID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_GetMachine"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
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

    Public Shared Function GetDataJudgement(Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_GetJudgement"

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

    Public Shared Function GetDataPart(Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_GetPart"

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

    Public Shared Function GetDataApprovalStatus(Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_GetStatusApproval"

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

    Public Shared Function GetData(ByVal pUserID As String, ByVal pStartDate As String, ByVal pEndDate As String, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pMachineNo As String, ByVal pJudgement As String, ByVal pPartID As String, ByVal pApprovalStatus As String, ByVal pApprovalStatus1 As String, ByVal pApprovalStatus2 As String, ByVal pApprovalStatus3 As String, ByVal pApprovalStatus4 As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_GetData"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
                cmd.Parameters.AddWithValue("StartDate", pStartDate)
                cmd.Parameters.AddWithValue("EndDate", pEndDate)
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
                cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
                cmd.Parameters.AddWithValue("Judgement", pJudgement)
                cmd.Parameters.AddWithValue("PartID", pPartID)
                cmd.Parameters.AddWithValue("ApprovalStatus", pApprovalStatus)
                cmd.Parameters.AddWithValue("ApprovalStatus1", pApprovalStatus1)
                cmd.Parameters.AddWithValue("ApprovalStatus2", pApprovalStatus2)
                cmd.Parameters.AddWithValue("ApprovalStatus3", pApprovalStatus3)
                cmd.Parameters.AddWithValue("ApprovalStatus4", pApprovalStatus4)
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

    Public Shared Function CekUser(ByVal pUserID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_CekUser"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
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

    Public Shared Function GetDataQESecHead(ByVal pUserID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_CekQESecHead"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
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

    Public Shared Function GetDataProdSecHead(ByVal pUserID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_CekProdSecHead"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
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

    Public Shared Function GetDataLineLeader(ByVal pUserID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_CekLineLeader"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
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

    Public Shared Function GetDataQELeader(ByVal pUserID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_CekQELeader"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
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

    Public Shared Function GetDataCount4(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_Count4"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)
                Return ds

            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetDataCount3(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_Count3"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)
                Return ds

            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetDataCount2(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_Count2"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)
                Return ds

            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetDataCount1(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_Count1"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)
                Return ds

            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetDataCount(ByVal pUserID As String, ByVal pApprovalStatus1 As String, ByVal pApprovalStatus2 As String, ByVal pApprovalStatus3 As String, ByVal pApprovalStatus4 As String, Optional ByRef pErr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_Count"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
                cmd.Parameters.AddWithValue("ApprovalStatus1", pApprovalStatus1)
                cmd.Parameters.AddWithValue("ApprovalStatus2", pApprovalStatus2)
                cmd.Parameters.AddWithValue("ApprovalStatus3", pApprovalStatus3)
                cmd.Parameters.AddWithValue("ApprovalStatus4", pApprovalStatus4)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)
                Return ds

            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function CekStatusApproval(ByVal pTCCSResultID As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_CekApprovalStatus"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("TCCSResultID", pTCCSResultID)
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

    Public Shared Function Approve1(ByVal pTCCSResultApproval As ClsTCCSResultApproval, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_Approval1"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("TCCSResultID", pTCCSResultApproval.TCCSResultID)
                cmd.Parameters.AddWithValue("ApprovalPIC1", pTCCSResultApproval.ApprovalPIC1)
                cmd.Parameters.AddWithValue("ApprovalJudgement1", pTCCSResultApproval.ApprovalJudgement1)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Approve2(ByVal pTCCSResultApproval As ClsTCCSResultApproval, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_Approval2"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("TCCSResultID", pTCCSResultApproval.TCCSResultID)
                cmd.Parameters.AddWithValue("ApprovalPIC2", pTCCSResultApproval.ApprovalPIC2)
                cmd.Parameters.AddWithValue("ApprovalJudgement2", pTCCSResultApproval.ApprovalJudgement2)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Approve3(ByVal pTCCSResultApproval As ClsTCCSResultApproval, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_Approval3"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("TCCSResultID", pTCCSResultApproval.TCCSResultID)
                cmd.Parameters.AddWithValue("ApprovalPIC3", pTCCSResultApproval.ApprovalPIC3)
                cmd.Parameters.AddWithValue("ApprovalJudgement3", pTCCSResultApproval.ApprovalJudgement3)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Approve4(ByVal pTCCSResultApproval As ClsTCCSResultApproval, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_Approval4"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("TCCSResultID", pTCCSResultApproval.TCCSResultID)
                cmd.Parameters.AddWithValue("ApprovalPIC4", pTCCSResultApproval.ApprovalPIC4)
                cmd.Parameters.AddWithValue("ApprovalJudgement4", pTCCSResultApproval.ApprovalJudgement4)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function GetStartDate(ByVal pUserID As String, ByVal pApprovalStatus1 As String, ByVal pApprovalStatus2 As String, ByVal pApprovalStatus3 As String, ByVal pApprovalStatus4 As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSResultApproval_GetDate"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
                cmd.Parameters.AddWithValue("ApprovalStatus1", pApprovalStatus1)
                cmd.Parameters.AddWithValue("ApprovalStatus2", pApprovalStatus2)
                cmd.Parameters.AddWithValue("ApprovalStatus3", pApprovalStatus3)
                cmd.Parameters.AddWithValue("ApprovalStatus4", pApprovalStatus4)
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
