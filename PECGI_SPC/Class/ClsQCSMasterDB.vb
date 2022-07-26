Imports System.Data.SqlClient
Public Class ClsQCSMasterDB
    'Public Shared Function GetDataApproval(ByVal pUserID As String, Optional ByRef pErr As String = "") As Boolean
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_QCS_GetApproval"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("UserID", pUserID)
    '            Dim da As New SqlDataAdapter(cmd)
    '            Dim ds As New DataSet
    '            da.Fill(ds)

    '            If ds.Tables(0).Rows.Count <> 0 Then
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        End Using
    '    Catch ex As Exception
    '        pErr = ex.Message
    '        Return Nothing
    '    End Try
    'End Function

    Public Shared Function GetDataQE(ByVal pUserID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_GetQE"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("UserID", pUserID)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)

                If ds.Tables(0).Rows(0)("QELeaderStatus").ToString = 1 Or ds.Tables(0).Rows(0)("QESectionHeadStatus").ToString = 1 Then
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

    Public Shared Function GetDataLine(ByVal pStatus As String, ByVal pUserID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_GetLine"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("Status", pStatus)
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

    Public Shared Function GetDataLineCopy(ByVal pPartID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_GetLineCopy"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
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

    Public Shared Function GetDataPart(ByVal pLineID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_GetPart"

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

    Public Shared Function GetDataPartCopy(Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_GetPartCopy"

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

    Public Shared Function GetDataRev(ByVal pPartID As String, ByVal pLineID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_GetRev"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("PartID", pPartID)
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

    Public Shared Function GetDataRevPopUp(ByVal pPartID As String, ByVal pLineID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_GetRevPopup"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("PartID", pPartID)
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

    Public Shared Function ExistQCS(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_Exist"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
                cmd.Parameters.AddWithValue("RevNo", pRevNo)
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

    Public Shared Function GetApprovalName(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_GetApprovalName"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
                cmd.Parameters.AddWithValue("RevNo", pRevNo)
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


    '===========================================
    Public Shared Function GetDataLineLeader(ByVal pUserID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_CekLineLeader"

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

    Public Shared Function GetDataForemanLeader(ByVal pUserID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_CekForemanLeader"

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
                sql = "SP_QCS_CekQE"

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

    Public Shared Function GetDataApprovalLine(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_ApprovalLine"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
                cmd.Parameters.AddWithValue("RevNo", pRevNo)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)

                If ds.Tables(0).Rows(0)("ApprovalStatus1").ToString = 0 Then
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

    Public Shared Function GetDataApprovalForeman(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_ApprovalForeman"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
                cmd.Parameters.AddWithValue("RevNo", pRevNo)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)

                If ds.Tables(0).Rows(0)("ApprovalStatus2").ToString = 0 Then
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

    Public Shared Function GetDataApprovalQE(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_ApprovalQE"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
                cmd.Parameters.AddWithValue("RevNo", pRevNo)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)

                If ds.Tables(0).Rows(0)("ApprovalStatus3").ToString = 0 Then
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

    Public Shared Function Approve1(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_Approval1"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("ApprovalPIC1", pQCS.ApprovalPIC1)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Unapprove1(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_Unapproval1"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Approve2(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_Approval2"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("ApprovalPIC2", pQCS.ApprovalPIC2)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Unapprove2(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_Unapproval2"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Approve3(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_Approval3"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("ApprovalPIC3", pQCS.ApprovalPIC3)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function UpdStatus(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_UpdActive"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    'Public Shared Function UpdApproveBy(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_QCS_UpdApproveBy"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
    '            cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
    '            'cmd.Parameters.AddWithValue("ApprovedBy", pQCS.ApprovedBy)

    '            Dim rtn As Integer = cmd.ExecuteNonQuery
    '            Return rtn
    '        End Using
    '    Catch ex As Exception
    '        pErr = ex.Message
    '        Return 0
    '    End Try
    'End Function

    Public Shared Function isExistQCS(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_Exist"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
                cmd.Parameters.AddWithValue("RevNo", pRevNo)
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

    'Public Shared Function isExistQCS1(ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef perr As String = "") As DataSet
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_QCS_Exist"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
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

    Public Shared Function isExistItemID(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSItem_ItemID"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
                cmd.Parameters.AddWithValue("RevNo", pRevNo)
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

    Public Shared Function isExistRevNo(ByVal pLineID As String, ByVal pPartID As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSItem_RevNo"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)

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

    'Public Shared Function Insert(ByVal pSCI As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
    '    Try
    '        Dim a As Integer = 0
    '        Dim conn As New SqlConnection(Sconn.Stringkoneksi)

    '        conn.Open()
    '        Dim sql As String = ""
    '        sql = "SP_QCS_Ins"


    '        Using cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("LineID", pSCI.LineID)
    '            cmd.Parameters.AddWithValue("PartID", pSCI.PartID)
    '            cmd.Parameters.AddWithValue("RevNo", pSCI.RevNo)
    '            cmd.Parameters.AddWithValue("RevDate", pSCI.RevDate)
    '            cmd.Parameters.AddWithValue("RevHistory", pSCI.RevHistory)
    '            cmd.Parameters.AddWithValue("PreparedBy", pSCI.PreparedBy)
    '            If pSCI.SafetySymbol = Nothing Then
    '                cmd.Parameters.AddWithValue("SafetySymbol", "")
    '            Else
    '                cmd.Parameters.AddWithValue("SafetySymbol", pSCI.SafetySymbol)
    '            End If
    '            'cmd.Parameters.AddWithValue("ApprovedBy", pSCI.ApprovedBy)
    '            cmd.Parameters.AddWithValue("CreateUser", pSCI.CreateUser)

    '            Dim rtn As Integer = cmd.ExecuteNonQuery
    '            Return rtn
    '        End Using

    '    Catch ex As Exception
    '        pErr = ex.Message
    '        Return 0
    '    End Try
    'End Function

    Public Shared Function Update(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("RevDate", pQCS.RevDate)
                cmd.Parameters.AddWithValue("RevHistory", pQCS.RevHistory)
                cmd.Parameters.AddWithValue("PreparedBy", pQCS.PreparedBy)
                'If pQCS.SafetySymbol = Nothing Then
                '    cmd.Parameters.AddWithValue("SafetySymbol", "")
                'Else
                '    cmd.Parameters.AddWithValue("SafetySymbol", pQCS.SafetySymbol)
                'End If
                cmd.Parameters.AddWithValue("RequiredAttachmentStatus", pQCS.RequiredAttachmentStatus)
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

    Public Shared Function Delete(ByVal pQCSItem As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCSItem.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCSItem.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCSItem.RevNo)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function GetLast5Rev(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_GetLast5"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
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

    Public Shared Function GetLastRevApproval(ByVal pLineID As String, ByVal pPartID As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_GetLastRevApprove"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
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

    Public Shared Function GetLastRevApprovalB(ByVal pLineID As String, ByVal pPartID As String, Optional ByRef perr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_GetLastRevApprove"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("PartID", pPartID)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds)
                If ds.Tables(0).Rows(0)("ApprovalStatus3").ToString = 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            perr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function InsertHeadDet(ByVal pQCS As ClsQCSMaster, ByVal pQCSDetail As ClsQCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Dim a As Integer = 0
            Dim conn As New SqlConnection(Sconn.Stringkoneksi)

            conn.Open()
            Dim sql As String = ""
            sql = "SP_QCS_Item_Ins"

            Using cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("RevDate", pQCS.RevDate)
                cmd.Parameters.AddWithValue("RevHistory", pQCS.RevHistory)
                cmd.Parameters.AddWithValue("PreparedBy", pQCS.PreparedBy)
                'If pQCS.SafetySymbol = Nothing Then
                '    cmd.Parameters.AddWithValue("SafetySymbol", "")
                'Else
                '    cmd.Parameters.AddWithValue("SafetySymbol", pQCS.SafetySymbol)
                'End If
                cmd.Parameters.AddWithValue("RequiredAttachmentStatus", pQCS.RequiredAttachmentStatus)
                cmd.Parameters.AddWithValue("CreateUser", pQCS.CreateUser)

                cmd.Parameters.AddWithValue("ItemID", pQCSDetail.ItemID)
                cmd.Parameters.AddWithValue("SeqNo", pQCSDetail.SeqNo)
                cmd.Parameters.AddWithValue("ProcessID", pQCSDetail.ProcessID)
                If pQCSDetail.KPointStatus = Nothing Then
                    cmd.Parameters.AddWithValue("KPointStatus", "")
                ElseIf pQCSDetail.KPointStatus = "B" Then
                    cmd.Parameters.AddWithValue("KPointStatus", "")
                Else
                    cmd.Parameters.AddWithValue("KPointStatus", pQCSDetail.KPointStatus)
                End If
                cmd.Parameters.AddWithValue("Item", pQCSDetail.Item)
                cmd.Parameters.AddWithValue("Standard", pQCSDetail.Standard)
                cmd.Parameters.AddWithValue("ValueType", pQCSDetail.ValueType)

                If pQCSDetail.ValueType = "T" Then
                    cmd.Parameters.AddWithValue("NumRangeStart", DBNull.Value)
                    cmd.Parameters.AddWithValue("NumRangeEnd", DBNull.Value)
                    cmd.Parameters.AddWithValue("TextRange", pQCSDetail.TextRange)
                ElseIf pQCSDetail.ValueType = "N" Then
                    cmd.Parameters.AddWithValue("NumRangeStart", pQCSDetail.NumRangeStart.Replace(",", "."))
                    cmd.Parameters.AddWithValue("NumRangeEnd", pQCSDetail.NumRangeEnd.Replace(",", "."))
                    cmd.Parameters.AddWithValue("TextRange", DBNull.Value)
                End If
                cmd.Parameters.AddWithValue("MeasuringInstrument", pQCSDetail.MeasuringInstrument)
                If pQCSDetail.XRCode = Nothing Then
                    cmd.Parameters.AddWithValue("XRCode", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("XRCode", pQCSDetail.XRCode)
                End If
                cmd.Parameters.AddWithValue("FrequencyType", pQCSDetail.FrequencyType)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using

        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function InsertCopy(ByVal pQCS As ClsQCSMaster, ByVal pQCSItem As ClsQCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Dim a As Integer = 0
            Dim conn As New SqlConnection(Sconn.Stringkoneksi)

            conn.Open()
            Dim sql As String = ""
            sql = "SP_QCS_Item_Copy"

            Using cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("RevDate", pQCS.RevDate)
                cmd.Parameters.AddWithValue("RevHistory", pQCS.RevHistory)
                cmd.Parameters.AddWithValue("PreparedBy", pQCS.PreparedBy)
                'If pQCS.SafetySymbol = Nothing Then
                '    cmd.Parameters.AddWithValue("SafetySymbol", "")
                'Else
                '    cmd.Parameters.AddWithValue("SafetySymbol", pQCS.SafetySymbol)
                'End If
                cmd.Parameters.AddWithValue("RequiredAttachmentStatus", pQCS.RequiredAttachmentStatus)
                cmd.Parameters.AddWithValue("CreateUser", pQCS.CreateUser)

                cmd.Parameters.AddWithValue("LineIDCopy", pQCSItem.LineIDCopy)
                cmd.Parameters.AddWithValue("PartIDCopy", pQCSItem.PartIDCopy)
                cmd.Parameters.AddWithValue("RevNoCopy", pQCSItem.RevNoCopy)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using

        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function UpdateSymbol1(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_UpdSymbol1"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("SafetySymbol1", pQCS.SafetySymbol1)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function UpdateSymbol2(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_UpdSymbol2"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("SafetySymbol2", pQCS.SafetySymbol2)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function UpdateSymbol3(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_UpdSymbol3"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("SafetySymbol3", pQCS.SafetySymbol3)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function UpdateSymbol4(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_UpdSymbol4"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("SafetySymbol4", pQCS.SafetySymbol4)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function UpdateSymbol5(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_UpdSymbol5"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("SafetySymbol5", pQCS.SafetySymbol5)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function UpdateSymbol6(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_UpdSymbol6"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                cmd.Parameters.AddWithValue("SafetySymbol6", pQCS.SafetySymbol6)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function DeleteSymbol1(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_DelSymbol1"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function DeleteSymbol2(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_DelSymbol2"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function DeleteSymbol3(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_DelSymbol3"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function DeleteSymbol4(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_DelSymbol4"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function DeleteSymbol5(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_DelSymbol5"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function DeleteSymbol6(ByVal pQCS As ClsQCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCS_DelSymbol6"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCS.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCS.RevNo)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function
End Class
