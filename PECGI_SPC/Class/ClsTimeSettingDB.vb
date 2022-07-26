Imports System.Data.SqlClient

Public Class ClsTimeSettingDB
    Public Shared Function GetList(Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSTime_Sel"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
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

    Public Shared Function Insert(ByVal pTime As ClsTimeSetting, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSTime_Ins"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("Shift1Cycle1", Format((pTime.Shift1Cycle1), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift1Cycle2", Format((pTime.Shift1Cycle2), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift1Cycle3", Format((pTime.Shift1Cycle3), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift1Cycle4", Format((pTime.Shift1Cycle4), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift1Cycle5", Format((pTime.Shift1Cycle5), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift2Cycle1", Format((pTime.Shift2Cycle1), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift2Cycle2", Format((pTime.Shift2Cycle2), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift2Cycle3", Format((pTime.Shift2Cycle3), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift2Cycle4", Format((pTime.Shift2Cycle4), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift2Cycle5", Format((pTime.Shift2Cycle5), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift3Cycle1", Format((pTime.Shift3Cycle1), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift3Cycle2", Format((pTime.Shift3Cycle2), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift3Cycle3", Format((pTime.Shift3Cycle3), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift3Cycle4", Format((pTime.Shift3Cycle4), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift3Cycle5", Format((pTime.Shift3Cycle5), "HH:mm"))
                cmd.Parameters.AddWithValue("CreateUser", pTime.CreateUser)


                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Update(ByVal pTime As ClsTimeSetting, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSTime_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("Shift1Cycle1", Format((pTime.Shift1Cycle1), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift1Cycle2", Format((pTime.Shift1Cycle2), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift1Cycle3", Format((pTime.Shift1Cycle3), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift1Cycle4", Format((pTime.Shift1Cycle4), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift1Cycle5", Format((pTime.Shift1Cycle5), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift2Cycle1", Format((pTime.Shift2Cycle1), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift2Cycle2", Format((pTime.Shift2Cycle2), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift2Cycle3", Format((pTime.Shift2Cycle3), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift2Cycle4", Format((pTime.Shift2Cycle4), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift2Cycle5", Format((pTime.Shift2Cycle5), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift3Cycle1", Format((pTime.Shift3Cycle1), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift3Cycle2", Format((pTime.Shift3Cycle2), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift3Cycle3", Format((pTime.Shift3Cycle3), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift3Cycle4", Format((pTime.Shift3Cycle4), "HH:mm"))
                cmd.Parameters.AddWithValue("Shift3Cycle5", Format((pTime.Shift3Cycle5), "HH:mm"))
                cmd.Parameters.AddWithValue("UpdateUser", pTime.UpdateUser)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function isExist(Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSTime_Exist"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
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
End Class
