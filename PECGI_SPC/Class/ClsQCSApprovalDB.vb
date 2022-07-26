Imports System.Data.SqlClient
Public Class ClsQCSApprovalDB
    Public Shared Function GetDataQE(ByVal pUserID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_Approval_GetQE"

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

    Public Shared Function GetDataLine(ByVal pUserID As String, ByVal pPartID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSApproval_GetLine"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
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

    Public Shared Function GetDataPart(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSApproval_GetPart"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
                'cmd.Parameters.AddWithValue("LineID", pLineID)
                'cmd.Parameters.AddWithValue("Status", pStatus)
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

    Public Shared Function GetDataRev(ByVal pPartID As String, ByVal pLineID As String, ByVal pStatus As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSApproval_GetRev"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("PartID", pPartID)
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("Status", pStatus)
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

    Public Shared Function GetStatus(Optional ByRef perr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSApproval_GetStatus"
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

    'Public Shared Function GetListAll(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, ByVal pApprovalStatus As String, Optional ByRef perr As String = "") As DataSet
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_QCSApproval_SelAll"
    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("LineID", pLineID)
    '            cmd.Parameters.AddWithValue("PartID", pPartID)
    '            cmd.Parameters.AddWithValue("RevNo", pRevNo)
    '            Dim da As New SqlDataAdapter(cmd)
    '            Dim ds As New DataSet
    '            da.Fill(ds)
    '            Return ds
    '        End Using
    '    Catch ex As Exception
    '        perr = ex.Message
    '        Return Nothing
    '    End Try
    'End Function

    'Public Shared Function GetListApproved(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, ByVal pApprovalStatus As String, Optional ByRef perr As String = "") As DataSet
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_QCSApproval_SelApproved"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("LineID", pLineID)
    '            cmd.Parameters.AddWithValue("PartID", pPartID)
    '            cmd.Parameters.AddWithValue("RevNo", pRevNo)
    '            Dim da As New SqlDataAdapter(cmd)
    '            Dim ds As New DataSet
    '            da.Fill(ds)
    '            Return ds
    '        End Using
    '    Catch ex As Exception
    '        perr = ex.Message
    '        Return Nothing
    '    End Try
    'End Function

    'Public Shared Function GetListUnApproved(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, ByVal pApprovalStatus As String, Optional ByRef perr As String = "") As DataSet
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_QCSApproval_SelUnApproved"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("LineID", pLineID)
    '            cmd.Parameters.AddWithValue("PartID", pPartID)
    '            cmd.Parameters.AddWithValue("RevNo", pRevNo)
    '            Dim da As New SqlDataAdapter(cmd)
    '            Dim ds As New DataSet
    '            da.Fill(ds)
    '            Return ds
    '        End Using
    '    Catch ex As Exception
    '        perr = ex.Message
    '        Return Nothing
    '    End Try
    'End Function

    Public Shared Function GetListList(ByVal pUserID As String, ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, ByVal pApprovalStatus As String, ByVal pStatus As String, ByVal pStartDate As String, ByVal pEndDate As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSApproval_GetList"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
                cmd.Parameters.AddWithValue("RevNo", pRevNo)
                cmd.Parameters.AddWithValue("Approval", pApprovalStatus)
                cmd.Parameters.AddWithValue("Status", pStatus)
                cmd.Parameters.AddWithValue("StartDate", pStartDate)
                cmd.Parameters.AddWithValue("EndDate", pEndDate)
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

    Public Shared Function Update(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("RevDate", pQCS.RevDate)
                cmd.Parameters.AddWithValue("RevHistory", pQCS.RevHistory)
                cmd.Parameters.AddWithValue("PreparedBy", pQCS.PreparedBy)
                'cmd.Parameters.AddWithValue("ApprovedBy", pQCS.ApprovedBy)
                cmd.Parameters.AddWithValue("UpdateUser", pQCS.UpdateUser)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function GetDataCountQEApproval(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSApproval_Count3"

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

    Public Shared Function GetDataCountForemanApproval(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSApproval_Count2"

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

    Public Shared Function GetDataCountLineApproval(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSApproval_Count1"

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

    Public Shared Function GetStartDate(ByVal pUserID As String, ByVal pApprovalStatus1 As String, ByVal pApprovalStatus2 As String, ByVal pApprovalStatus3 As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSApproval_GetDate"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
                cmd.Parameters.AddWithValue("ApprovalStatus1", pApprovalStatus1)
                cmd.Parameters.AddWithValue("ApprovalStatus2", pApprovalStatus2)
                cmd.Parameters.AddWithValue("ApprovalStatus3", pApprovalStatus3)
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
