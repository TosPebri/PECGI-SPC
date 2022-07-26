Imports System.Data.SqlClient

Public Class clsTCCSResult
    Public Property TCCSResultID As Integer
    Public Property TCCSDate As Date
    Public Property MachineNo As String
    Public Property PartID As String
    Public Property LineID As String
    Public Property SubLineID As String
    Public Property RevNo As Integer
    Public Property OldPartID As String
    Public Property LotNo As String
    Public Property Remark As String

    Public Property ApprovalStatus1 As Integer
    Public Property ApprovalDate1 As Nullable(Of Date)
    Public Property ApprovalPIC1 As String
    Public Property ApprovalJudgement1 As String

    Public Property ApprovalStatus2 As Integer
    Public Property ApprovalDate2 As Nullable(Of Date)
    Public Property ApprovalPIC2 As String
    Public Property ApprovalJudgement2 As String

    Public Property ApprovalStatus3 As Integer
    Public Property ApprovalDate3 As Nullable(Of Date)
    Public Property ApprovalPIC3 As String
    Public Property ApprovalJudgement3 As String

    Public Property ApprovalStatus4 As Integer
    Public Property ApprovalDate4 As Nullable(Of Date)
    Public Property ApprovalPIC4 As String
    Public Property ApprovalJudgement4 As String

    Public Property Judgement As String
    Public Property CreateUser As String
    Public Property LastUpdate As Nullable(Of Date)
End Class

Public Class clsTCCSResultDB
    Public Shared Function GetTable(pDate As Date, pMachineNo As String, pLineID As String, pSubLineID As String, pPartID As String) As DataSet
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "sp_TCCSResult_Sel"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 60
            cmd.Parameters.AddWithValue("Date", Format(pDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
            cmd.Parameters.AddWithValue("LineID", pLineID)
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
            cmd.Parameters.AddWithValue("PartID", pPartID)
            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds)
            Return ds
        End Using
    End Function

    Public Shared Function GetMachine(UserID As String) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select M.MachineNo, rtrim(S.SubLineID) SubLineID, rtrim(S.LineID) LineID, P.ProcessName, S.ProcessID " & vbCrLf & _
                "from Machine M inner join SubLine S on M.MachineNo = S.MachineNo inner join Process P on S.ProcessID = P.ProcessID " & vbCrLf & _
                "inner join UserLine U on S.LineID = U.LineID " & vbCrLf & _
                "where U.UserID = @UserID " & vbCrLf & _
                "group by M.MachineNo, S.SubLineID, S.LineID, P.ProcessName, S.ProcessID " & vbCrLf & _
                "order by S.LineID, S.SubLineID, S.ProcessID, M.MachineNo "
            Dim cmd As New SqlCommand(q, Cn)
            Dim da As New SqlDataAdapter(cmd)
            cmd.Parameters.AddWithValue("UserID", UserID)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Shared Function GetPart(pMachineNo As String) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "SELECT rtrim(P.PartID) AS PartID, RTRIM(P.PartName) AS PartName " & vbCrLf & _
                "From Part P inner join TCCS Q on P.PartID = Q.PartID " & vbCrLf & _
                "inner join ( " & vbCrLf & _
                "	select PartID, MachineNo, max(RevNo) RevNo from TCCS where ApprovalStatus = 1 group by PartID, MachineNo " & vbCrLf & _
                ") X on P.PartID = X.PartID and Q.MachineNo = X.MachineNo and Q.RevNo = X.RevNo " & vbCrLf & _
                "where Q.MachineNo = case when @MachineNo = '' then Q.MachineNo else @MachineNo end " & vbCrLf & _
                "and P.ActiveStatus = 1 " & vbCrLf & _
                "group by P.PartID, P.PartName " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("MachineNo", pMachineNo)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Shared Function Insert(Result As clsTCCSResult, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim q As String = "sp_TCCSResult_Ins"
        Dim cmd As New SqlCommand(q, Cn, Tr)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("Date", Format(Result.TCCSDate, "yyyy-MM-dd"))
        cmd.Parameters.AddWithValue("MachineNo", Result.MachineNo)
        cmd.Parameters.AddWithValue("PartID", Result.PartID)
        cmd.Parameters.AddWithValue("LineID", Result.LineID)
        cmd.Parameters.AddWithValue("SubLineID", Result.SubLineID)
        cmd.Parameters.AddWithValue("OldPartID", Result.OldPartID)
        cmd.Parameters.AddWithValue("LotNo", Result.LotNo)
        cmd.Parameters.AddWithValue("Remark", Result.Remark)
        cmd.Parameters.AddWithValue("CreateUser", Result.CreateUser)
        Dim prm As New SqlParameter
        prm.ParameterName = "TCCSResultID"
        prm.Direction = ParameterDirection.Output
        prm.Size = 5
        cmd.Parameters.Add(prm)
        Dim i As Integer = cmd.ExecuteNonQuery()
        Result.TCCSResultID = prm.Value
        Return i
    End Function

    Public Shared Function GetData(pDate As Date, pMachineNo As String, pPartID As String, pLineID As String, pSubLineID As String) As clsTCCSResult
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = _
                "Select top 1 R.*, J.Judgement, " & vbCrLf & _
                "coalesce(R.UpdateDate, R.CreateDate) LastUpdate, coalesce(R.UpdateUser, R.CreateUser) UserUpdate " & vbCrLf & _
                "from TCCSResult R left join (" & vbCrLf & _
                "   select TCCSResultID, min(isnull(Judgement, 'NG')) Judgement from TCCSResultItem group by TCCSResultID " & vbCrLf & _
                ") J on R.TCCSResultID = J.TCCSResultID " & vbCrLf & _
                "where R.Date = @Date and R.PartID = @PartID and R.MachineNo = @MachineNo " & vbCrLf & _
                "and R.LineID = @LineID and R.SubLineID = @SubLineID " & vbCrLf & _
                "order by R.RevNo desc " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Date", Format(pDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("MachineNo", Val(pMachineNo & ""))
            cmd.Parameters.AddWithValue("PartID", pPartID & "")
            cmd.Parameters.AddWithValue("LineID", pLineID & "")
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID & "")
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return Nothing
            Else
                Dim Rs As New clsTCCSResult
                With dt.Rows(0)
                    Rs.TCCSResultID = .Item("TCCSResultID")
                    Rs.MachineNo = .Item("MachineNo")
                    Rs.PartID = RTrim(.Item("PartID"))
                    Rs.LineID = RTrim(.Item("LineID"))
                    Rs.SubLineID = RTrim(.Item("SubLineID"))
                    Rs.RevNo = .Item("RevNo")
                    Rs.OldPartID = RTrim(.Item("OldPartID") & "")

                    Rs.ApprovalStatus1 = Val(.Item("ApprovalStatus1") & "")
                    If IsDate(.Item("ApprovalDate1")) Then
                        Rs.ApprovalDate1 = .Item("ApprovalDate1")
                    Else
                        Rs.ApprovalDate1 = Nothing
                    End If
                    Rs.ApprovalPIC1 = RTrim(.Item("ApprovalPIC1") & "")
                    Rs.ApprovalJudgement1 = .Item("ApprovalJudgement1") & ""

                    Rs.ApprovalStatus2 = Val(.Item("ApprovalStatus2") & "")
                    If IsDate(.Item("ApprovalDate2")) Then
                        Rs.ApprovalDate2 = .Item("ApprovalDate2")
                    Else
                        Rs.ApprovalDate2 = Nothing
                    End If
                    Rs.ApprovalPIC2 = RTrim(.Item("ApprovalPIC2") & "")
                    Rs.ApprovalJudgement2 = RTrim(.Item("ApprovalJudgement2") & "")

                    Rs.ApprovalStatus3 = Val(.Item("ApprovalStatus3") & "")
                    If IsDate(.Item("ApprovalDate3")) Then
                        Rs.ApprovalDate3 = .Item("ApprovalDate3")
                    Else
                        Rs.ApprovalDate3 = Nothing
                    End If
                    Rs.ApprovalPIC3 = RTrim(.Item("ApprovalPIC3") & "")
                    Rs.ApprovalJudgement3 = RTrim(.Item("ApprovalJudgement3") & "")

                    Rs.ApprovalStatus4 = Val(.Item("ApprovalStatus4") & "")
                    If IsDate(.Item("ApprovalDate4")) Then
                        Rs.ApprovalDate4 = .Item("ApprovalDate4")
                    Else
                        Rs.ApprovalDate4 = Nothing
                    End If
                    Rs.ApprovalPIC4 = RTrim(.Item("ApprovalPIC4") & "")
                    Rs.ApprovalJudgement4 = RTrim(.Item("ApprovalJudgement4") & "")
                    Rs.Judgement = RTrim(.Item("Judgement") & "")
                    If IsDate(.Item("LastUpdate")) Then
                        Rs.LastUpdate = .Item("LastUpdate")
                    End If
                    Rs.CreateUser = .Item("UserUpdate").ToString.Trim
                End With
                Return Rs
            End If
        End Using
    End Function

    Public Shared Function Approve(ApproveLevel As String, Result As clsTCCSResult) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim a As Integer = Val(ApproveLevel)
            Dim Q As String = "Update TCCSResult set ApprovalStatus" & a & " = @ApprovalStatus, " & vbCrLf & _
            "ApprovalDate" & a & " = GetDate(), ApprovalPIC" & a & " = @ApprovalPIC, ApprovalJudgement" & a & " = @ApprovalJudgement " & vbCrLf & _
            "where TCCSResultID = @TCCSResultID "
            Dim cmd As New SqlCommand(Q, Cn)
            cmd.Parameters.AddWithValue("TCCSResultID", Result.TCCSResultID)
            cmd.Parameters.AddWithValue("ApprovalStatus", Result.ApprovalStatus1)
            cmd.Parameters.AddWithValue("ApprovalPIC", Result.ApprovalPIC1)
            cmd.Parameters.AddWithValue("ApprovalJudgement", Result.ApprovalJudgement1)
            Dim i As Integer = cmd.ExecuteNonQuery()
            Return i
        End Using
    End Function

    Public Shared Function Delete(TCCSResultID As Integer) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim Q As String = "Delete TCCSResult " & vbCrLf & _
            "where TCCSResultID = @TCCSResultID " & vbCrLf & _
            "delete TCCSResultAttachment where TCCSResultID = @TCCSResultID "
            Dim cmd As New SqlCommand(Q, Cn)
            cmd.Parameters.AddWithValue("TCCSResultID", TCCSResultID)
            Dim i As Integer = cmd.ExecuteNonQuery()
            Return i
        End Using
    End Function
End Class