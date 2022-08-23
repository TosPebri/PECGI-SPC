Imports System.Data.SqlClient
Public Class clsProdSampleVerificationDB
    Public Shared Function FillCombo(ByVal Status As String, data As clsProdSampleVerification, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SPC_ProdSampleVerification_FillCombo"
                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("Status", Status)
                cmd.Parameters.AddWithValue("User", If(data.User, ""))
                cmd.Parameters.AddWithValue("FactoryCode", If(data.FactoryCode, ""))
                cmd.Parameters.AddWithValue("LineCode", If(data.LineCode, ""))
                cmd.Parameters.AddWithValue("ItemTypeCode", If(data.ItemType_Code, ""))
                cmd.Parameters.AddWithValue("ItemCheckCode", If(data.ItemCheck_Code, ""))
                cmd.Parameters.AddWithValue("ShiftCode", If(data.ShiftCode, ""))
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


    Public Shared Function GridLoad(ActionGrid As String, data As clsProdSampleVerification, Optional ByRef pErr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SPC_ProdSampleVerification_Grid"
                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("FactoryCode", If(data.FactoryCode, ""))
                cmd.Parameters.AddWithValue("LineCode", If(data.LineCode, ""))
                cmd.Parameters.AddWithValue("ItemTypeCode", If(data.ItemType_Code, ""))
                cmd.Parameters.AddWithValue("ItemCheckCode", If(data.ItemCheck_Code, ""))
                cmd.Parameters.AddWithValue("ShiftCode", If(data.ShiftCode, ""))
                cmd.Parameters.AddWithValue("Seq", If(data.Seq, ""))
                cmd.Parameters.AddWithValue("ProdDate", If(data.ProdDate, ""))
                cmd.Parameters.AddWithValue("ActionGrid", If(ActionGrid, ""))
                cmd.Parameters.AddWithValue("Shiftcode_Grid", If(data.Shiftcode_Grid, ""))
                cmd.Parameters.AddWithValue("ProdDate_Grid", If(data.ProdDate_Grid, ""))
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
