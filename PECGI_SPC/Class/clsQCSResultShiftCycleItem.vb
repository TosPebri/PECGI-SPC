Imports System.Data.SqlClient

Public Class clsQCSResultShiftCycleItem
    Public Property QCSResultShiftCycleID As Integer
    Public Property ItemID As Integer
    Public Property NumValue As Object
    Public Property TextValue As String
    Public Property ValueType As String
End Class

Public Class clsQCSResultShiftCycleItemDB
    Public Shared Function Insert(RSCI As clsQCSResultShiftCycleItem, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim cmd As New SqlCommand("sp_QCSResultShiftCycleItem_Ins", Cn, Tr)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("QCSResultShiftCycleID", RSCI.QCSResultShiftCycleID)
        cmd.Parameters.AddWithValue("ItemID", RSCI.ItemID)
        If RSCI.ValueType = "N" And RSCI.NumValue IsNot Nothing Then
            cmd.Parameters.AddWithValue("NumValue", Val(RSCI.NumValue))
        End If
        cmd.Parameters.AddWithValue("TextValue", RSCI.TextValue)
        Dim i As Integer = cmd.ExecuteNonQuery()
        Return i
    End Function

    Public Shared Function InsertGetData(RSCI As clsQCSResultShiftCycleItem, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim cmd As New SqlCommand("sp_QCSResultShiftCycleItem_Ins", Cn, Tr)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("QCSResultShiftCycleID", RSCI.QCSResultShiftCycleID)
        cmd.Parameters.AddWithValue("ItemID", RSCI.ItemID)
        cmd.Parameters.AddWithValue("NumValue", Val(RSCI.NumValue))
        Dim i As Integer = cmd.ExecuteNonQuery()
        Return i
    End Function

    Public Shared Function GetData(ByVal pShitf As String, ByVal pCycle As String, Optional ByRef pErr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSResult_GetData_Testing"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("Shift", pShitf)
                cmd.Parameters.AddWithValue("Cycle", pCycle)
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
End Class
