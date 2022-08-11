Imports System.Data.SqlClient
Public Class clsProductionSampleVerificationListDB
    Public Shared Function FillCombo(ByVal Status As String, data As clsProductionSampleVerificationList, Optional ByRef pErr As String = "") As DataTable
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_SPC_ProdSampleVerificationList_FillCombo"
                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("Status", Status)
                cmd.Parameters.AddWithValue("FactoryCode", If(data.FactoryCode, ""))
                cmd.Parameters.AddWithValue("LineCode", If(data.LineCode, ""))
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
