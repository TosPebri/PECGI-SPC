﻿Imports System.Data.SqlClient

Public Class Cls_ss_UserPrivilegeDB
    Public Shared Function GetList(Optional ByRef pErr As String = "") As List(Of Cls_ss_UserPrivilege)
        Try
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                Dim sql As String = "SELECT * FROM dbo.spc_UserPrivilege"
                Dim Cmd As New SqlCommand(sql, cn)
                Dim da As New SqlDataAdapter(Cmd)
                Dim dt As New DataTable
                da.Fill(dt)
                Dim UPs As New List(Of Cls_ss_UserPrivilege)
                For i = 0 To dt.Rows.Count - 1
                    Dim UP As New Cls_ss_UserPrivilege With {.AppID = "P01", .UserID = dt.Rows(i)("UserID"),
                            .MenuID = Trim(dt.Rows(i)("MenuID") & ""), .AllowAccess = dt.Rows(i)("AllowAccess"), .AllowUpdate = dt.Rows(i)("AllowUpdate"), .AllowPrint = dt.Rows(i)("AllowPrint")}
                    UPs.Add(UP)
                Next
                Return UPs
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try

    End Function

    Public Shared Function GetValidate(ByVal pUserID As String, Optional ByRef pErr As String = "") As DataSet
        Try
            pErr = ""
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                cn.Open()
                Dim sql As String = ""

                sql = "SELECT * FROM dbo.spc_UserSetup WHERE AppID='spc' AND UserID='" & pUserID & "' "


                Dim ds As New DataSet
                Dim cmd As New SqlCommand(sql, cn)
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(ds)
                Return ds

            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return Nothing
        End Try
    End Function

    Public Shared Function Copy(FromUserID As String, ToUserID As String, CreateUser As String) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "delete from dbo.spc_UserPrivilege where UserID = @ToUserID"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("ToUserID", ToUserID)

            Dim i As Integer = cmd.ExecuteNonQuery
            q = "Insert into spc_UserPrivilege (" & vbCrLf &
                "AppID, UserID, MenuID, AllowAccess, AllowUpdate, AllowSpecial, CreateDate, CreateUser ) " & vbCrLf &
                "select AppID, @ToUserID, MenuID, AllowAccess, AllowUpdate, AllowSpecial, GetDate(), @CreateUser " & vbCrLf &
                "from spc_UserPrivilege where UserID = @FromUserID"
            cmd = New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("FromUserID", FromUserID)
            cmd.Parameters.AddWithValue("ToUserID", ToUserID)
            cmd.Parameters.AddWithValue("CreateUser", CreateUser)
            i = cmd.ExecuteNonQuery()
            Return i
        End Using
    End Function

    Public Shared Function Save(ByVal pUserP As Cls_ss_UserPrivilege, Optional ByRef pErr As String = "") As Integer
        Try
            pErr = ""
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                cn.Open()
                Dim sql As String = " IF NOT EXISTS (SELECT * FROM dbo.SPC_UserPrivilege WHERE UserID =@UserID AND MenuID=@MenuID)" & vbCrLf &
                      " BEGIN " & vbCrLf &
                    " INSERT INTO dbo.SPC_UserPrivilege ( AppID ,UserID ,MenuID ,AllowAccess ,AllowUpdate) " & vbCrLf &
                    " VALUES( @AppID ," & vbCrLf &
                    " @UserID ," & vbCrLf &
                    " @MenuID ," & vbCrLf &
                    " @AllowAccess ," & vbCrLf &
                    " @AllowUpdate )" & vbCrLf &
                    " END " & vbCrLf &
                    " ELSE " & vbCrLf &
                    " BEGIN "
                sql = sql + " UPDATE dbo.SPC_UserPrivilege " & vbCrLf &
                      " SET AllowAccess=@AllowAccess, " & vbCrLf &
                      " AllowUpdate=@AllowUpdate " & vbCrLf &
                      " WHERE AppID=@AppID AND UserID =@UserID AND MenuID=@MenuID " & vbCrLf &
                      " END "
                Dim Cmd As New SqlCommand(sql, cn)
                With Cmd.Parameters
                    .AddWithValue("AppID", pUserP.AppID)
                    .AddWithValue("UserID", pUserP.UserID)
                    .AddWithValue("MenuID", pUserP.MenuID)
                    .AddWithValue("AllowAccess", pUserP.AllowAccess)
                    .AddWithValue("AllowUpdate", pUserP.AllowUpdate)
                End With
                Return Cmd.ExecuteNonQuery
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try

    End Function

    Public Shared Function Delete(ByVal pUserP As Cls_ss_UserPrivilege, Optional ByRef pErr As String = "") As Integer
        pErr = ""
        Try
            Using cn As New SqlConnection(Sconn.Stringkoneksi)
                cn.Open()
                Dim sql As String = "DELETE spc_UserPrivilege WHERE UserID=@UserID"
                Dim Cmd As New SqlCommand(sql, cn)
                With Cmd.Parameters
                    .AddWithValue("UserID", pUserP.UserID)
                End With
                Return Cmd.ExecuteNonQuery
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try

    End Function
End Class
