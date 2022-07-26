Imports System.Data.SqlClient

Public Class ClsQCSMasterDetailDB
    Public Shared Function GetList(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSItem_Sel"

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

    Public Shared Function Insert(ByVal pSCIDetail As ClsQCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSItem_Ins"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pSCIDetail.LineID)
                cmd.Parameters.AddWithValue("PartID", pSCIDetail.PartID)
                cmd.Parameters.AddWithValue("RevNo", pSCIDetail.RevNo)
                cmd.Parameters.AddWithValue("ItemID", pSCIDetail.ItemID)
                cmd.Parameters.AddWithValue("SeqNo", pSCIDetail.SeqNo)
                cmd.Parameters.AddWithValue("ProcessID", pSCIDetail.ProcessID)
                If pSCIDetail.KPointStatus = Nothing Then
                    cmd.Parameters.AddWithValue("KPointStatus", "")
                ElseIf pSCIDetail.KPointStatus = "B" Then
                    cmd.Parameters.AddWithValue("KPointStatus", "")
                Else
                    cmd.Parameters.AddWithValue("KPointStatus", pSCIDetail.KPointStatus)
                End If
                cmd.Parameters.AddWithValue("Item", pSCIDetail.Item)
                cmd.Parameters.AddWithValue("Standard", pSCIDetail.Standard)
                cmd.Parameters.AddWithValue("ValueType", pSCIDetail.ValueType)

                If pSCIDetail.ValueType = "T" Then
                    cmd.Parameters.AddWithValue("NumRangeStart", DBNull.Value)
                    cmd.Parameters.AddWithValue("NumRangeEnd", DBNull.Value)
                    cmd.Parameters.AddWithValue("TextRange", pSCIDetail.TextRange)
                ElseIf pSCIDetail.ValueType = "N" Then
                    cmd.Parameters.AddWithValue("NumRangeStart", pSCIDetail.NumRangeStart.Replace(",", "."))
                    cmd.Parameters.AddWithValue("NumRangeEnd", pSCIDetail.NumRangeEnd.Replace(",", "."))
                    cmd.Parameters.AddWithValue("TextRange", DBNull.Value)
                End If
                cmd.Parameters.AddWithValue("MeasuringInstrument", pSCIDetail.MeasuringInstrument)
                If pSCIDetail.XRCode = Nothing Then
                    cmd.Parameters.AddWithValue("XRCode", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("XRCode", pSCIDetail.XRCode)
                End If
                cmd.Parameters.AddWithValue("FrequencyType", pSCIDetail.FrequencyType)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    'Public Shared Function isExistItem(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, ByVal pItemID As String, Optional ByRef pErr As String = "") As Boolean
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_QCSItem_Exist"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("LineID", pLineID)
    '            cmd.Parameters.AddWithValue("PartID", pPartID)
    '            cmd.Parameters.AddWithValue("RevNo", pRevNo)
    '            cmd.Parameters.AddWithValue("ItemID", pItemID)
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

    Public Shared Function Delete(ByVal pQCSItem As ClsQCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSItem_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pQCSItem.LineID)
                cmd.Parameters.AddWithValue("PartID", pQCSItem.PartID)
                cmd.Parameters.AddWithValue("RevNo", pQCSItem.RevNo)
                cmd.Parameters.AddWithValue("ItemID", pQCSItem.ItemID)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function DeleteAll(ByVal pQCSItem As ClsQCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSItem_DelAll"

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

    Public Shared Function Update(ByVal pSCIDetail As ClsQCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSItem_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pSCIDetail.LineID)
                cmd.Parameters.AddWithValue("PartID", pSCIDetail.PartID)
                cmd.Parameters.AddWithValue("RevNo", pSCIDetail.RevNo)
                cmd.Parameters.AddWithValue("ItemID", pSCIDetail.ItemID)
                cmd.Parameters.AddWithValue("SeqNo", pSCIDetail.SeqNo)
                cmd.Parameters.AddWithValue("ProcessID", pSCIDetail.ProcessID)
                If pSCIDetail.KPointStatus = Nothing Then
                    cmd.Parameters.AddWithValue("KPointStatus", "")
                ElseIf pSCIDetail.KPointStatus = "B" Then
                    cmd.Parameters.AddWithValue("KPointStatus", "")
                Else
                    cmd.Parameters.AddWithValue("KPointStatus", pSCIDetail.KPointStatus)
                End If
                cmd.Parameters.AddWithValue("Item", pSCIDetail.Item)
                cmd.Parameters.AddWithValue("Standard", pSCIDetail.Standard)
                cmd.Parameters.AddWithValue("ValueType", pSCIDetail.ValueType)

                If pSCIDetail.ValueType = "T" Then
                    cmd.Parameters.AddWithValue("NumRangeStart", DBNull.Value)
                    cmd.Parameters.AddWithValue("NumRangeEnd", DBNull.Value)
                    cmd.Parameters.AddWithValue("TextRange", pSCIDetail.TextRange)
                ElseIf pSCIDetail.ValueType = "N" Then
                    cmd.Parameters.AddWithValue("NumRangeStart", pSCIDetail.NumRangeStart.Replace(",", "."))
                    cmd.Parameters.AddWithValue("NumRangeEnd", pSCIDetail.NumRangeEnd.Replace(",", "."))
                    cmd.Parameters.AddWithValue("TextRange", DBNull.Value)
                End If
                cmd.Parameters.AddWithValue("MeasuringInstrument", pSCIDetail.MeasuringInstrument)
                If pSCIDetail.XRCode = Nothing Then
                    cmd.Parameters.AddWithValue("XRCode", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("XRCode", pSCIDetail.XRCode)
                End If
                cmd.Parameters.AddWithValue("FrequencyType", pSCIDetail.FrequencyType)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Copy(ByVal pSCIDetail As ClsQCSMasterDetail, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSItem_InsCopy"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pSCIDetail.LineID)
                cmd.Parameters.AddWithValue("LineIDCopy", pSCIDetail.LineIDCopy)
                cmd.Parameters.AddWithValue("PartID", pSCIDetail.PartID)
                cmd.Parameters.AddWithValue("RevNo", pSCIDetail.RevNo)
                cmd.Parameters.AddWithValue("PartIDCopy", pSCIDetail.PartIDCopy)
                cmd.Parameters.AddWithValue("RevNoCopy", pSCIDetail.RevNoCopy)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function GetDataProcess(Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSItem_GetProcess"

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
End Class
