Imports System.Data.SqlClient

Public Class ClsTCCSMasterDB
    Public Shared Function GetDataPart(Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_GetPart"

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

    Public Shared Function GetDataMachine(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_GetMachine"

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

    Public Shared Function GetDataRev(ByVal pMachineNo As String, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pPartID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_GetRev"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
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

    Public Shared Function GetDataRevPopUp(ByVal pMachineNo As String, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pPartID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_GetRevPopup"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
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

    Public Shared Function GetData(ByVal pMachineNo As String, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_GetData"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
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

    Public Shared Function GetLastRevApproval(ByVal pMachineNo As String, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pPartID As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_GetLastRevApprove"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
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

    Public Shared Function GetDataProcess(ByVal pMachineNo As String, ByVal pLineID As String, ByVal pSubLineID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_GetProcess"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
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

    'Public Shared Function GetDataLine(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataTable
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_TCCS_GetLine"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("UserID", pUserID)
    '            Dim da As New SqlDataAdapter(cmd)
    '            Dim dt As New DataTable
    '            da.Fill(dt)

    '            Return dt
    '        End Using
    '    Catch ex As Exception
    '        pErr = ex.Message
    '        Return Nothing
    '    End Try
    'End Function

    'Public Shared Function GetDataSubLine(ByVal pLineID As String, Optional ByRef pErr As String = "") As DataTable
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_TCCS_GetSubLine"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("LineID", pLineID)
    '            Dim da As New SqlDataAdapter(cmd)
    '            Dim dt As New DataTable
    '            da.Fill(dt)

    '            Return dt
    '        End Using
    '    Catch ex As Exception
    '        pErr = ex.Message
    '        Return Nothing
    '    End Try
    'End Function
    

    Public Shared Function IsExistTCCS(ByVal pMachineNo As String, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_IsExist"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
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

    Public Shared Function GetLastRev(ByVal pMachineNo As String, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pPartID As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_GetLastRev"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
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

    Public Shared Function InsertHeadDet(ByVal pTCCS As ClsTCCSMaster, ByVal pTCCSDetail As ClsTCCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Dim a As Integer = 0
            Dim conn As New SqlConnection(Sconn.Stringkoneksi)

            conn.Open()
            Dim sql As String = ""
            sql = "SP_TCCS_Item_Ins"

            Using cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pTCCS.MachineNo)
                cmd.Parameters.AddWithValue("LineID", pTCCS.LineID)
                cmd.Parameters.AddWithValue("SubLineID", pTCCS.SubLineID)
                cmd.Parameters.AddWithValue("PartID", pTCCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pTCCS.RevNo)
                cmd.Parameters.AddWithValue("RevDate", pTCCS.RevDate)
                cmd.Parameters.AddWithValue("RevHistory", pTCCS.RevHistory)
                cmd.Parameters.AddWithValue("PreparedBy", pTCCS.PreparedBy)
                If pTCCS.Remark = Nothing Then
                    cmd.Parameters.AddWithValue("RemarkHead", "")
                Else
                    cmd.Parameters.AddWithValue("RemarkHead", pTCCS.Remark)
                End If
                cmd.Parameters.AddWithValue("CreateUser", pTCCS.CreateUser)

                cmd.Parameters.AddWithValue("ItemID", pTCCSDetail.ItemID)
                cmd.Parameters.AddWithValue("SeqNo", pTCCSDetail.SeqNo)
                cmd.Parameters.AddWithValue("ProcessID", pTCCSDetail.ProcessID)
                If pTCCSDetail.KPointStatus = Nothing Then
                    cmd.Parameters.AddWithValue("KPointStatus", "")
                Else
                    cmd.Parameters.AddWithValue("KPointStatus", pTCCSDetail.KPointStatus)
                End If
                cmd.Parameters.AddWithValue("PICType", pTCCSDetail.PICType)
                cmd.Parameters.AddWithValue("Item", pTCCSDetail.Item)
                cmd.Parameters.AddWithValue("Tools", pTCCSDetail.Tools)
                cmd.Parameters.AddWithValue("Standard", pTCCSDetail.Standard)
                cmd.Parameters.AddWithValue("ValueType", pTCCSDetail.ValueType)

                If pTCCSDetail.ValueType = "T" Then
                    cmd.Parameters.AddWithValue("NumRangeStart", DBNull.Value)
                    cmd.Parameters.AddWithValue("NumRangeEnd", DBNull.Value)
                    cmd.Parameters.AddWithValue("TextRange", pTCCSDetail.TextRange)
                ElseIf pTCCSDetail.ValueType = "N" Then
                    cmd.Parameters.AddWithValue("NumRangeStart", pTCCSDetail.NumRangeStart.Replace(",", "."))
                    cmd.Parameters.AddWithValue("NumRangeEnd", pTCCSDetail.NumRangeEnd.Replace(",", "."))
                    cmd.Parameters.AddWithValue("TextRange", DBNull.Value)
                End If
                If pTCCSDetail.Remark = Nothing Then
                    cmd.Parameters.AddWithValue("RemarkDet", "")
                Else
                    cmd.Parameters.AddWithValue("RemarkDet", pTCCSDetail.Remark)
                End If

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using

        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function InsertCopy(ByVal pTCCS As ClsTCCSMaster, ByVal pTCCSItem As ClsTCCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Dim a As Integer = 0
            Dim conn As New SqlConnection(Sconn.Stringkoneksi)

            conn.Open()
            Dim sql As String = ""
            sql = "SP_TCCS_Item_Copy"

            Using cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pTCCS.MachineNo)
                cmd.Parameters.AddWithValue("LineID", pTCCS.LineID)
                cmd.Parameters.AddWithValue("SubLineID", pTCCS.SubLineID)
                cmd.Parameters.AddWithValue("PartID", pTCCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pTCCS.RevNo)
                cmd.Parameters.AddWithValue("RevDate", pTCCS.RevDate)
                cmd.Parameters.AddWithValue("RevHistory", pTCCS.RevHistory)
                cmd.Parameters.AddWithValue("PreparedBy", pTCCS.PreparedBy)
                If pTCCS.Remark = Nothing Then
                    cmd.Parameters.AddWithValue("Remark", "")
                Else
                    cmd.Parameters.AddWithValue("Remark", pTCCS.Remark)
                End If
                cmd.Parameters.AddWithValue("CreateUser", pTCCS.CreateUser)

                cmd.Parameters.AddWithValue("MachineNoCopy", pTCCSItem.MachineNoCopy)
                cmd.Parameters.AddWithValue("LineIDCopy", pTCCSItem.LineIDCopy)
                cmd.Parameters.AddWithValue("SubLineIDCopy", pTCCSItem.SubLineIDCopy)
                cmd.Parameters.AddWithValue("PartIDCopy", pTCCSItem.PartIDCopy)
                cmd.Parameters.AddWithValue("RevNoCopy", pTCCSItem.RevNoCopy)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using

        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function GetStatusApproval(ByVal pUserID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_GetStatusApproval"

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

    Public Shared Function Approval(ByVal pTCCS As ClsTCCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_Approval"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pTCCS.MachineNo)
                cmd.Parameters.AddWithValue("LineID", pTCCS.LineID)
                cmd.Parameters.AddWithValue("SubLineID", pTCCS.SubLineID)
                cmd.Parameters.AddWithValue("PartID", pTCCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pTCCS.RevNo)
                cmd.Parameters.AddWithValue("ApprovalPIC", pTCCS.ApprovalPIC)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Update(ByVal pTCCS As ClsTCCSMaster, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pTCCS.MachineNo)
                cmd.Parameters.AddWithValue("LineID", pTCCS.LineID)
                cmd.Parameters.AddWithValue("SubLineID", pTCCS.SubLineID)
                cmd.Parameters.AddWithValue("PartID", pTCCS.PartID)
                cmd.Parameters.AddWithValue("RevNo", pTCCS.RevNo)
                cmd.Parameters.AddWithValue("RevDate", pTCCS.RevDate)
                cmd.Parameters.AddWithValue("RevHistory", pTCCS.RevHistory)
                cmd.Parameters.AddWithValue("PreparedBy", pTCCS.PreparedBy)
                If pTCCS.Remark = Nothing Then
                    cmd.Parameters.AddWithValue("Remark", "")
                Else
                    cmd.Parameters.AddWithValue("Remark", pTCCS.Remark)
                End If
                cmd.Parameters.AddWithValue("UpdateUser", pTCCS.UpdateUser)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    'Public Shared Function Delete(ByVal pTCCS As ClsTCCSMaster, Optional ByRef pErr As String = "") As Integer
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_TCCS_Del"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            'cmd.Parameters.AddWithValue("LineID", pTCCS.LineID)
    '            'cmd.Parameters.AddWithValue("SubLineID", pTCCS.SubLineID)
    '            'cmd.Parameters.AddWithValue("ProcessID", pTCCS.ProcessID)
    '            cmd.Parameters.AddWithValue("PartID", pTCCS.PartID)
    '            cmd.Parameters.AddWithValue("RevNo", pTCCS.RevNo)
    '            Dim rtn As Integer = cmd.ExecuteNonQuery
    '            Return rtn
    '        End Using
    '    Catch ex As Exception
    '        pErr = ex.Message
    '        Return 0
    '    End Try
    'End Function

    Public Shared Function GetDataPartPopup(Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_GetPartPopup"

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

    'Masih belum
    Public Shared Function GetDataMachinePopUp(ByVal pPartID As String, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_GetMachinePopup"

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

    Public Shared Function GetLast5Rev(ByVal pPartID As String, ByVal pMachineNo As String, ByVal pLineID As String, ByVal pSubLineID As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_GetLast5"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("PartID", pPartID)
                cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
                cmd.Parameters.AddWithValue("LineID", pLineID)
                cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
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
