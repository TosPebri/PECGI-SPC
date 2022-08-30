Imports System.Data.SqlClient

Public Class clsSPCResult
    Public Property SPCResultID As Integer
    Public Property FactoryCode As String
    Public Property ItemTypeCode As String
    Public Property LineCode As String
    Public Property ItemCheckCode As String
    Public Property ProdDate As Date
    Public Property ShiftCode As String
    Public Property SequenceNo As Integer
    Public Property SubLotNo As Integer
    Public Property Remark As String
    Public Property RegisterUser As String
End Class

Public Class clsSPCResultDB
    Public Shared Function GetTable() As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "sp_SPCResult"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Shared Function GetPrevDate(FactoryCode As String, ItemTypeCode As String, LineCode As String, ItemCheckCode As String, ProdDate As String) As Object
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "sp_SPCResult_GetPrevDate"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("FactoryCode", FactoryCode)
            cmd.Parameters.AddWithValue("ItemTypeCode", ItemTypeCode)
            cmd.Parameters.AddWithValue("LineCode", LineCode)
            cmd.Parameters.AddWithValue("ItemCheckCode", ItemCheckCode)
            cmd.Parameters.AddWithValue("ProdDate", ProdDate)
            cmd.CommandType = CommandType.StoredProcedure
            Dim PrevDate As Object = cmd.ExecuteScalar
            Return PrevDate
        End Using
    End Function

    Public Shared Function Insert(Result As clsSPCResult) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "sp_SPCResult_Ins"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("FactoryCode", Result.FactoryCode)
            cmd.Parameters.AddWithValue("ItemTypeCode", Result.ItemTypeCode)
            cmd.Parameters.AddWithValue("LineCode", Result.LineCode)
            cmd.Parameters.AddWithValue("ItemCheckCode", Result.ItemCheckCode)
            cmd.Parameters.AddWithValue("ProdDate", Result.ProdDate)
            cmd.Parameters.AddWithValue("ShiftCode", Result.ShiftCode)
            cmd.Parameters.AddWithValue("SequenceNo", Result.SequenceNo)
            cmd.Parameters.AddWithValue("SubLotNo", Result.SubLotNo)
            cmd.Parameters.AddWithValue("RegisterUser", Result.RegisterUser)

            cmd.Parameters.Add("SPCResultID", SqlDbType.Int)
            cmd.Parameters("SPCResultID").Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()

            Result.SPCResultID = CInt(cmd.Parameters("SPCResultID").Value)
            Return Result.SPCResultID
        End Using
    End Function
End Class