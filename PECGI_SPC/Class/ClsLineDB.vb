Imports System.Data.SqlClient

Public Class ClsLineDB
    Public Shared Function GetList(Optional ByRef pErr As String = "") As List(Of ClsLine)
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Line_GetList"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                Dim list As New List(Of ClsLine)
                For i = 0 To dt.Rows.Count - 1
                    Dim line As New ClsLine With {.LineID = Trim(dt.Rows(i).Item("LineID") & ""),
                                                    .LineName = Trim(dt.Rows(i).Item("LineName") & "")}

                    list.Add(line)
                Next
                Return list
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function Insert(ByVal pLine As ClsLine, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Line_Ins"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLine.LineID)
                cmd.Parameters.AddWithValue("LineName", pLine.LineName)
                'cmd.Parameters.AddWithValue("EZRLineID", pLine.EZRLineID)
                'cmd.Parameters.AddWithValue("Leader1", pLine.Leader1)

                'If IsNothing(pLine.Leader2) Then
                '    cmd.Parameters.AddWithValue("Leader2", DBNull.Value)
                'Else
                '    cmd.Parameters.AddWithValue("Leader2", pLine.Leader2)
                'End If

                'If IsNothing(pLine.Leader3) Then
                '    cmd.Parameters.AddWithValue("Leader3", DBNull.Value)
                'Else
                '    cmd.Parameters.AddWithValue("Leader3", pLine.Leader3)
                'End If

                'cmd.Parameters.AddWithValue("Foreman1", pLine.Foreman1)

                'If IsNothing(pLine.Foreman2) Then
                '    cmd.Parameters.AddWithValue("Foreman2", DBNull.Value)
                'Else
                '    cmd.Parameters.AddWithValue("Foreman2", pLine.Foreman2)
                'End If

                'If IsNothing(pLine.Foreman3) Then
                '    cmd.Parameters.AddWithValue("Foreman3", DBNull.Value)
                'Else
                '    cmd.Parameters.AddWithValue("Foreman3", pLine.Foreman3)
                'End If

                'cmd.Parameters.AddWithValue("SectionHead1", pLine.SectionHead1)

                'If IsNothing(pLine.SectionHead2) Then
                '    cmd.Parameters.AddWithValue("SectionHead2", DBNull.Value)
                'Else
                '    cmd.Parameters.AddWithValue("SectionHead2", pLine.SectionHead2)
                'End If

                cmd.Parameters.AddWithValue("User", pLine.CreateUser)


                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Update(ByVal pLine As ClsLine, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Line_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLine.LineID)
                cmd.Parameters.AddWithValue("LineName", pLine.LineName)
                'cmd.Parameters.AddWithValue("EZRLineID", pLine.EZRLineID)
                'cmd.Parameters.AddWithValue("Leader1", pLine.Leader1)

                'If IsNothing(pLine.Leader2) Then
                '    cmd.Parameters.AddWithValue("Leader2", DBNull.Value)
                'Else
                '    cmd.Parameters.AddWithValue("Leader2", pLine.Leader2)
                'End If

                'If IsNothing(pLine.Leader3) Then
                '    cmd.Parameters.AddWithValue("Leader3", DBNull.Value)
                'Else
                '    cmd.Parameters.AddWithValue("Leader3", pLine.Leader3)
                'End If

                'cmd.Parameters.AddWithValue("Foreman1", pLine.Foreman1)

                'If IsNothing(pLine.Foreman2) Then
                '    cmd.Parameters.AddWithValue("Foreman2", DBNull.Value)
                'Else
                '    cmd.Parameters.AddWithValue("Foreman2", pLine.Foreman2)
                'End If

                'If IsNothing(pLine.Foreman3) Then
                '    cmd.Parameters.AddWithValue("Foreman3", DBNull.Value)
                'Else
                '    cmd.Parameters.AddWithValue("Foreman3", pLine.Foreman3)
                'End If

                'cmd.Parameters.AddWithValue("SectionHead1", pLine.SectionHead1)

                'If IsNothing(pLine.SectionHead2) Then
                '    cmd.Parameters.AddWithValue("SectionHead2", DBNull.Value)
                'Else
                '    cmd.Parameters.AddWithValue("SectionHead2", pLine.SectionHead2)
                'End If

                'cmd.Parameters.AddWithValue("Leader1", pLine.Leader1)
                'cmd.Parameters.AddWithValue("Leader2", pLine.Leader2)
                'cmd.Parameters.AddWithValue("Leader3", pLine.Leader3)
                'cmd.Parameters.AddWithValue("Foreman1", pLine.Foreman1)
                'cmd.Parameters.AddWithValue("Foreman2", pLine.Foreman2)
                'cmd.Parameters.AddWithValue("Foreman3", pLine.Foreman3)
                'cmd.Parameters.AddWithValue("SectionHead1", pLine.SectionHead1)
                'cmd.Parameters.AddWithValue("SectionHead2", pLine.SectionHead2)
                cmd.Parameters.AddWithValue("User", pLine.UpdateUser)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Delete(ByVal pLine As ClsLine, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Line_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLine.LineID)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function isExist(ByVal pLineID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Line_Exist"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
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

    Public Shared Function isExistDel(ByVal pLineID As String, Optional ByRef pErr As String = "") As Boolean
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_Line_Exist_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("LineID", pLineID)
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
