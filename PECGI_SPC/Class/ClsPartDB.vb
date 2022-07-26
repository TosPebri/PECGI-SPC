Imports System.Data.SqlClient

Public Class ClsPartDB

    Public Shared Function GetList(Optional ByRef pErr As String = "") As List(Of ClsPart)
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Part_GetList"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                Dim list As New List(Of ClsPart)
                For i = 0 To dt.Rows.Count - 1
                    Dim subline As New ClsPart With {.PartID = Trim(dt.Rows(i).Item("PartID") & ""),
                                                     .PartName = Trim(dt.Rows(i).Item("PartName") & ""),
                                                     .Remark = Trim(dt.Rows(i).Item("Remark") & "")}

                    list.Add(subline)
                Next
                Return list
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function GetList1(Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Part_GetList"

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

    Public Shared Function Insert(ByVal pLine As ClsPart, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Part_Ins"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("PartID", pLine.PartID)
                cmd.Parameters.AddWithValue("PartName", pLine.PartName)
                If IsNothing(pLine.Remark) Then
                    cmd.Parameters.AddWithValue("Remark", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("Remark", pLine.Remark)
                End If
                cmd.Parameters.AddWithValue("ActiveStatus", pLine.ActiveStatus)
                cmd.Parameters.AddWithValue("User", pLine.CreateUser)


                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Update(ByVal pLine As ClsPart, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Part_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("PartID", pLine.PartID)
                cmd.Parameters.AddWithValue("PartName", pLine.PartName)
                If IsNothing(pLine.Remark) Then
                    cmd.Parameters.AddWithValue("Remark", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("Remark", pLine.Remark)
                End If
                cmd.Parameters.AddWithValue("ActiveStatus", pLine.ActiveStatus)

                cmd.Parameters.AddWithValue("User", pLine.UpdateUser)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Delete(ByVal pLine As ClsPart, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Part_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("PartID", pLine.PartID)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function isExist(ByVal pPartID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Part_Exist"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("PartID", pPartID)
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

    Public Shared Function isExistDel(ByVal pPartID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Part_Exist_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("PartID", pPartID)
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
