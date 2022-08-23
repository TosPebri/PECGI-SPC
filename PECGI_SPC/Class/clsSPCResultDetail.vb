Imports System.Data.SqlClient

Public Class clsSPCResultDetail
    Public Property SPCResultID As Integer
    Public Property SequenceNo As Integer
    Public Property Value As Double
    Public Property Remark As String
    Public Property DeleteStatus As String
    Public Property RegisterUser As String

End Class

Public Class clsSPCResultDetailDB
    Public Shared Function Insert(Detail As clsSPCResultDetail)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "sp_SPCResultDetail_Ins"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("SPCResultID", Detail.SPCResultID)
            cmd.Parameters.AddWithValue("SequenceNo", Detail.SequenceNo)
            cmd.Parameters.AddWithValue("Value", Detail.Value)
            cmd.Parameters.AddWithValue("Remark", Detail.Remark)
            cmd.Parameters.AddWithValue("DeleteStatus", Detail.DeleteStatus)
            cmd.Parameters.AddWithValue("RegisterUser", Detail.RegisterUser)
            Dim i As Integer = cmd.ExecuteNonQuery
            Return i
        End Using
    End Function

    Public Shared Function GetTable(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String, Shift As String, Sequence As Integer) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "sp_SPCResultDetail"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("FactoryCode", FactoryCode)
            cmd.Parameters.AddWithValue("ItemTypeCode", ItemTypeCode)
            cmd.Parameters.AddWithValue("Line", Line)
            cmd.Parameters.AddWithValue("ItemCheckCode", ItemCheckCode)
            cmd.Parameters.AddWithValue("ProdDate", ProdDate)
            cmd.Parameters.AddWithValue("ShiftCode", Shift)
            cmd.Parameters.AddWithValue("SequenceNo", Sequence)

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function
End Class