Imports System.Data.SqlClient

Public Class clsQCSResult
    Public Property QCSResultID As Integer
    Public Property QCSDate As Date
    Public LineID As String
    Public SubLineID As String
    Public PartID As String
    Public RevNo As Integer
    Public CreateUser As String
End Class

Public Class clsQCSResultDB
    Public Shared Function GetPart(pLineID As String) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "SELECT rtrim(P.PartID) AS PartID, RTRIM(P.PartName) AS PartName " & vbCrLf & _
                "From Part P inner join QCS Q on P.PartID = Q.PartID " & vbCrLf & _
                "inner join ( " & vbCrLf & _
                "	select PartID, LineID, max(RevNo) RevNo from QCS group by PartID, LineID " & vbCrLf & _
                ") X on P.PartID = X.PartID and Q.LineID = X.LineID and Q.RevNo = X.RevNo " & vbCrLf & _
                "where Q.LineID = case when @LineID = '' then Q.LineID else @LineID end " & vbCrLf & _
                "and P.ActiveStatus = 1 " & vbCrLf & _
                "group by P.PartID, P.PartName, Q.RevNo " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("LineID", pLineID)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Shared Function GetPartResult(pDateFrom As Date, pDateTo As Date, pLineID As String) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "SELECT rtrim(P.PartID) AS PartID, RTRIM(P.PartName) AS PartName " & vbCrLf & _
                "From Part P inner join QCS Q on P.PartID = Q.PartID " & vbCrLf & _
                "inner join ( " & vbCrLf & _
                "	select PartID, LineID, max(RevNo) RevNo from QCS group by PartID, LineID " & vbCrLf & _
                ") X on P.PartID = X.PartID and Q.LineID = X.LineID and Q.RevNo = X.RevNo " & vbCrLf & _
                "inner join QCSResult R on P.PartID = R.PartID and X.LineID = R.LineID " & vbCrLf & _
                "where Q.LineID = case when @LineID = '' then Q.LineID else @LineID end " & vbCrLf & _
                "and R.Date between @DateFrom and @DateTo " & vbCrLf & _
                "group by P.PartID, P.PartName, Q.RevNo " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("DateFrom", Format(pDateFrom, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("DateTo", Format(pDateTo, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("LineID", pLineID)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Shared Function GetLineID(pDateFrom As Date, pDateTo As Date, pUser As String) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select rtrim(Q.LineID) LineID, rtrim(L.LineName) LineName " & vbCrLf & _
                "from QCSResult Q inner join Line L on Q.LineID = L.LineID " & vbCrLf & _
                "inner join UserLine U on L.LineID = U.LineID " & vbCrLf & _
                "where Q.Date between @DateFrom and @DateTo and U.UserID = @UserID " & vbCrLf & _
                "group by Q.LineID, L.LineName " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("DateFrom", Format(pDateFrom, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("DateTo", Format(pDateTo, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("UserID", pUser)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Shared Function GetMachine(pLineID As String, pSubLineID As String) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select S.MachineNo, P.ProcessName, S.ProcessID " & vbCrLf & _
                "from SubLine S inner join Process P on S.ProcessID = P.ProcessID " & vbCrLf & _
                "where LineID = @LineID and SubLineID = @SubLineID " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("LineID", pLineID)
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Shared Function GetTable(pDate As String, pLineID As String, pSubLineID As String, pPartID As String, pShift As String, pRevNo As Integer) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "sp_QCSResult_Sel"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("Date", pDate)
            cmd.Parameters.AddWithValue("LineID", pLineID)
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
            cmd.Parameters.AddWithValue("PartID", pPartID)
            cmd.Parameters.AddWithValue("Shift", Val(pShift))
            cmd.Parameters.AddWithValue("RevNo", Val(pRevNo))
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Shared Function GetDataSet(pDate As String, pLineID As String, pSubLineID As String, pPartID As String, pShift As String, pRevNo As Integer) As DataSet
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "sp_QCSResult_Sel"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("Date", pDate)
            cmd.Parameters.AddWithValue("LineID", pLineID)
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
            cmd.Parameters.AddWithValue("PartID", pPartID)
            cmd.Parameters.AddWithValue("Shift", Val(pShift))
            cmd.Parameters.AddWithValue("RevNo", Val(pRevNo))
            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds)
            Return ds
        End Using
    End Function

    Public Shared Function GetRevDate(pDate As Date, pPartID As String, pLineID As String, pSubLineID As String, pRevNo As String) As String
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String
            q = "Select top 1 Q.RevNo, Q.RevDate from QCS Q " & vbCrLf & _
                "where Q.PartID = @PartID and Q.LineID = @LineID and Q.RevNo = @RevNo " & vbCrLf & _
                "order by Q.RevNo desc "
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("PartID", pPartID & "")
            cmd.Parameters.AddWithValue("LineID", pLineID & "")
            cmd.Parameters.AddWithValue("RevNo", pRevNo & "")
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return ""
            Else
                Return Format(dt.Rows(0)("RevDate"), "dd MMM yyyy")
            End If
        End Using
    End Function

    Public Shared Function GetLastRevNo(pDate As Date, pPartID As String, pLineID As String, pSubLineID As String) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Select max(R.RevNo) RevNo " & vbCrLf & _
                "from QCS R where R.PartID = @PartID and R.LineID = @LineID and R.ActiveStatus = 1 " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Date", Format(pDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("PartID", pPartID & "")
            cmd.Parameters.AddWithValue("LineID", pLineID & "")
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID & "")
            Dim RevNo As String = cmd.ExecuteScalar & ""
            Return Val(RevNo)
        End Using
    End Function

    Public Shared Function GetRevNo(pDate As Date, pPartID As String, pLineID As String, pSubLineID As String, pShift As String) As List(Of clsQCSResultShift)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String
            q = "Select R.RevNo, Q.RevDate from QCSResult R inner join QCS Q " & vbCrLf & _
                "on R.PartID = Q.PartID and R.LineID = Q.LineID and R.RevNo = Q.RevNo " & vbCrLf & _
                "inner join QCSResultShift S on R.QCSResultID = S.QCSResultID " & vbCrLf & _
                "where R.Date = @Date and R.PartID = @PartID and R.LineID = @LineID and R.SubLineID = @SubLineID " & vbCrLf & _
                "and isnull(S.Shift, 0) = case when @Shift = 0 then isnull(S.Shift, 0) else @Shift end " & vbCrLf & _
                "union " & vbCrLf & _
                "select top 1 RevNo, RevDate from QCS " & vbCrLf & _
                "where PartID = @PartID and LineID = @LineID and ActiveStatus = 1 " & vbCrLf & _
                "and cast(ApprovalDate3 as date) <= @Date " & vbCrLf & _
                "order by RevNo desc "
            Dim cmd As New SqlCommand(q, Cn)
            Dim ResultDate As String = Format(pDate, "yyyy-MM-dd")
            cmd.Parameters.AddWithValue("Date", ResultDate)
            cmd.Parameters.AddWithValue("PartID", pPartID & "")
            cmd.Parameters.AddWithValue("LineID", pLineID & "")
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID & "")
            cmd.Parameters.AddWithValue("Shift", Val(pShift))
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Dim Result As New List(Of clsQCSResultShift)
            If dt.Rows.Count = 0 Then
                q = "select top 1 RevNo, RevDate from QCS where PartID = @PartID and LineID = @LineID " & vbCrLf & _
                    "and ActiveStatus = 1  " & vbCrLf & _
                    "order by RevNo Desc "
                cmd = New SqlCommand(q, Cn)
                cmd.Parameters.AddWithValue("PartID", pPartID & "")
                cmd.Parameters.AddWithValue("LineID", pLineID & "")
                da = New SqlDataAdapter(cmd)
                dt = New DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    With dt.Rows(0)
                        Dim Qcs As New clsQCSResultShift
                        Qcs.RevNo = .Item("RevNo")
                        Qcs.RevDate = .Item("RevDate")
                        Result.Add(Qcs)
                    End With
                End If
            Else
                For i = 0 To dt.Rows.Count - 1
                    With dt.Rows(i)
                        Dim Qcs As New clsQCSResultShift
                        Qcs.RevNo = .Item("RevNo")
                        Qcs.RevDate = .Item("RevDate")
                        Result.Add(Qcs)
                    End With
                Next
            End If
            Return Result
        End Using
    End Function

    Public Shared Function GetTableInquiry(pDate As String, pLineID As String, pSubLineID As String, pPartID As String, pShift As Integer, pRevNo As Integer) As DataSet
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "sp_QCSInquiry_Sel"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("Date", pDate)
            cmd.Parameters.AddWithValue("LineID", pLineID)
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
            cmd.Parameters.AddWithValue("PartID", pPartID)
            cmd.Parameters.AddWithValue("Shift", pShift)
            cmd.Parameters.AddWithValue("RevNo", pRevNo)
            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds)
            Return ds
        End Using
    End Function

    Public Shared Function GetPartName(PartID As String) As ClsPart
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Select * from Part where PartID = @PartID"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("PartID", PartID)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return Nothing
            Else
                Dim Part As New ClsPart
                Part.PartID = PartID
                Part.PartName = dt.Rows(0)("PartName") & ""
                Return Part
            End If
        End Using
    End Function

    Public Shared Function GetTableInquiry(pLotNo As String) As DataSet
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "sp_QCSInquiry_Sel"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("LotNo", pLotNo)
            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds)
            Return ds
        End Using
    End Function

    Public Shared Function Insert(QCS As clsQCSResult, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim cmd As New SqlCommand("sp_QCSResult_Ins", Cn, Tr)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("Date", QCS.QCSDate)
        cmd.Parameters.AddWithValue("RevNo", QCS.RevNo)
        cmd.Parameters.AddWithValue("LineID", QCS.LineID)
        cmd.Parameters.AddWithValue("SubLineID", QCS.SubLineID)
        cmd.Parameters.AddWithValue("PartID", QCS.PartID)
        cmd.Parameters.AddWithValue("CreateUser", QCS.CreateUser)
        Dim prm As New SqlParameter
        prm.ParameterName = "QCSResultID"
        prm.Direction = ParameterDirection.Output
        prm.Size = 5
        cmd.Parameters.Add(prm)
        Dim i As Integer = cmd.ExecuteNonQuery()
        QCS.QCSResultID = prm.Value
        Return i
    End Function

    Public Shared Function Update(QCS As clsQCSResult, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim cmd As New SqlCommand("sp_QCSResult_Upd", Cn, Tr)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("Date", QCS.QCSDate)
        cmd.Parameters.AddWithValue("LineID", QCS.LineID)
        cmd.Parameters.AddWithValue("SubLineID", QCS.SubLineID)
        cmd.Parameters.AddWithValue("PartID", QCS.PartID)
        cmd.Parameters.AddWithValue("CreateUser", QCS.CreateUser)
        cmd.Parameters.AddWithValue("QCSResultID", QCS.QCSResultID)
        Dim i As Integer = cmd.ExecuteNonQuery()
        Return i
    End Function
End Class