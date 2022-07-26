Imports System.Data.SqlClient
Public Class ClsTCCSMasterDetailDB
    Public Shared Function GetData(ByVal pMachineNo As String, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSItem_GetData"

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

    Public Shared Function GetLastItemID(ByVal pMachineNo As String, ByVal pLIneID As String, ByVal pSubLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSItem_ItemID"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
                cmd.Parameters.AddWithValue("LineID", pLIneID)
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

    Public Shared Function Insert(ByVal pTCCSDetail As ClsTCCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSItem_Ins"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pTCCSDetail.MachineNo)
                cmd.Parameters.AddWithValue("LineID", pTCCSDetail.LineID)
                cmd.Parameters.AddWithValue("SubLineID", pTCCSDetail.SubLineID)
                cmd.Parameters.AddWithValue("PartID", pTCCSDetail.PartID)
                cmd.Parameters.AddWithValue("RevNo", pTCCSDetail.RevNo)
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
                    cmd.Parameters.AddWithValue("Remark", "")
                Else
                    cmd.Parameters.AddWithValue("Remark", pTCCSDetail.Remark)
                End If

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Update(ByVal pTCCSDetail As ClsTCCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSItem_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pTCCSDetail.MachineNo)
                cmd.Parameters.AddWithValue("LineID", pTCCSDetail.LineID)
                cmd.Parameters.AddWithValue("SubLineID", pTCCSDetail.SubLineID)
                cmd.Parameters.AddWithValue("PartID", pTCCSDetail.PartID)
                cmd.Parameters.AddWithValue("RevNo", pTCCSDetail.RevNo)
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
                    cmd.Parameters.AddWithValue("Remark", "")
                Else
                    cmd.Parameters.AddWithValue("Remark", pTCCSDetail.Remark)
                End If

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Delete(ByVal pTCCSDetail As ClsTCCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSItem_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pTCCSDetail.MachineNo)
                cmd.Parameters.AddWithValue("LineID", pTCCSDetail.LineID)
                cmd.Parameters.AddWithValue("SubLineID", pTCCSDetail.SubLineID)
                cmd.Parameters.AddWithValue("PartID", pTCCSDetail.PartID)
                cmd.Parameters.AddWithValue("RevNo", pTCCSDetail.RevNo)
                cmd.Parameters.AddWithValue("ItemID", pTCCSDetail.ItemID)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function DeleteAll(ByVal pTCCSDetail As ClsTCCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCS_Item_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pTCCSDetail.MachineNo)
                cmd.Parameters.AddWithValue("LineID", pTCCSDetail.LineID)
                cmd.Parameters.AddWithValue("SubLineID", pTCCSDetail.SubLineID)
                cmd.Parameters.AddWithValue("PartID", pTCCSDetail.PartID)
                cmd.Parameters.AddWithValue("RevNo", pTCCSDetail.RevNo)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function DeleteAllItem(ByVal pTCCSDetail As ClsTCCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSItem_DelAll"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pTCCSDetail.MachineNo)
                cmd.Parameters.AddWithValue("LineID", pTCCSDetail.LineID)
                cmd.Parameters.AddWithValue("SubLineID", pTCCSDetail.SubLineID)
                cmd.Parameters.AddWithValue("PartID", pTCCSDetail.PartID)
                cmd.Parameters.AddWithValue("RevNo", pTCCSDetail.RevNo)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Copy(ByVal pTCCSItem As ClsTCCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_TCCSItem_Copy"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("MachineNo", pTCCSItem.MachineNo)
                cmd.Parameters.AddWithValue("MachineNoCopy", pTCCSItem.MachineNoCopy)
                cmd.Parameters.AddWithValue("LineID", pTCCSItem.LineID)
                cmd.Parameters.AddWithValue("LineIDCopy", pTCCSItem.LineIDCopy)
                cmd.Parameters.AddWithValue("SubLineID", pTCCSItem.SubLineID)
                cmd.Parameters.AddWithValue("SubLineIDCopy", pTCCSItem.SubLineIDCopy)
                cmd.Parameters.AddWithValue("PartID", pTCCSItem.PartID)
                cmd.Parameters.AddWithValue("RevNo", pTCCSItem.RevNo)
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
End Class
