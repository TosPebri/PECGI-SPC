Imports System.Data.SqlClient
Public Class ClsQCSXRDataDB
    'Public Shared Function GetDataProcess(ByVal pPartID As String, ByVal pRevNo As String, Optional ByRef pErr As String = "") As DataTable
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_QCSXRData_GetProcess"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("PartID", pPartID)
    '            cmd.Parameters.AddWithValue("Revno", pRevNo)
    '            Dim da As New SqlDataAdapter(cmd)
    '            Dim dt As New DataTable
    '            da.Fill(dt)
    '            Return dt
    '        End Using
    '    Catch ex As Exception
    '        pErr = ex.Message
    '        Return Nothing
    '    End Try
    'End Function

    'Public Shared Function GetDataCheckItem(ByVal pPartID As String, ByVal pRevNo As String, ByVal pProcessID As String, Optional ByRef pErr As String = "") As DataTable
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_QCSXRData_GetItem"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("PartID", pPartID)
    '            cmd.Parameters.AddWithValue("Revno", pRevNo)
    '            cmd.Parameters.AddWithValue("ProcessID", pProcessID)
    '            Dim da As New SqlDataAdapter(cmd)
    '            Dim dt As New DataTable
    '            da.Fill(dt)
    '            Return dt
    '        End Using
    '    Catch ex As Exception
    '        pErr = ex.Message
    '        Return Nothing
    '    End Try
    'End Function

    'Public Shared Function Exist(ByVal pPartID As String, ByVal pRevNo As String, ByVal pItemID As String, Optional ByRef pErr As String = "") As Boolean
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_QCSXRData_Exist"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("PartID", pPartID)
    '            cmd.Parameters.AddWithValue("RevNo", pRevNo)
    '            cmd.Parameters.AddWithValue("ItemID", pItemID)
    '            Dim da As New SqlDataAdapter(cmd)
    '            Dim ds As New DataSet
    '            da.Fill(ds)

    '            If ds.Tables(0).Rows.Count = 0 Then
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

    Public Shared Function GetList(Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSXRData_GetList"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                'cmd.Parameters.AddWithValue("PartID", pPartID)
                'cmd.Parameters.AddWithValue("RevNo", pRevNo)
                'cmd.Parameters.AddWithValue("ItemID", pItemID)
                'cmd.Parameters.AddWithValue("Condition", pCondition)
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

    Public Shared Function Insert(ByVal pLine As ClsQCSXRData, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSXRData_Ins"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("XID", pLine.XID)
                cmd.Parameters.AddWithValue("A2Value", pLine.A2Value)
                cmd.Parameters.AddWithValue("D4Value", pLine.D4Value)
                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Update(ByVal pQCS As ClsQCSXRData, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSXRData_Upd"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("XID", pQCS.XID)
                cmd.Parameters.AddWithValue("A2Value", pQCS.A2Value)
                cmd.Parameters.AddWithValue("D4Value", pQCS.D4Value)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    Public Shared Function Delete(ByVal pXID As ClsQCSXRData, Optional ByRef pErr As String = "") As Integer
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSXRData_Del"

                Dim cmd As New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("XID", pXID.XID)

                Dim rtn As Integer = cmd.ExecuteNonQuery
                Return rtn
            End Using
        Catch ex As Exception
            pErr = ex.Message
            Return 0
        End Try
    End Function

    'Public Shared Function Copy(ByVal pSCIDetail As ClsQCSXRData, Optional ByRef pErr As String = "") As Integer
    '    Try
    '        Using conn As New SqlConnection(Sconn.Stringkoneksi)
    '            conn.Open()
    '            Dim sql As String = ""
    '            sql = "SP_QCSXRData_InsCopy"

    '            Dim cmd As New SqlCommand(sql, conn)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("PartID", pSCIDetail.PartID)
    '            cmd.Parameters.AddWithValue("RevNo", pSCIDetail.RevNo)
    '            cmd.Parameters.AddWithValue("RevNoCopy", pSCIDetail.RevNoCopy)
    '            cmd.Parameters.AddWithValue("ItemID", pSCIDetail.ItemID)
    '            cmd.Parameters.AddWithValue("ItemIDCopy", pSCIDetail.ItemIDCopy)

    '            Dim rtn As Integer = cmd.ExecuteNonQuery
    '            Return rtn
    '        End Using
    '    Catch ex As Exception
    '        pErr = ex.Message
    '        Return 0
    '    End Try
    'End Function

    Public Shared Function isExistXID(Optional ByRef perr As String = "") As DataSet
        Try
            Using conn As New SqlConnection(Sconn.Stringkoneksi)
                conn.Open()
                Dim sql As String = ""
                sql = "SP_QCSXRData_XID"

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
End Class
