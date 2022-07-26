Imports System.Data.SqlClient
Imports System.IO

Public Class clsQCSResultShift
    Public Property QCSResultID As Integer
    Public Property QCSResultShiftID As Integer
    Public Property Shift As String
    Public Property ChangePoint As String
    Public Property Remark As String    
    Public Property ApprovalStatus As String
    Public Property ApprovalDate As Object
    Public Property ApprovalPIC As String
    Public Property RevNo As Integer
    Public Property RevDate As Object
    Public Property FileName As String
End Class

Public Class clsXRView
    Public Property QCSDate As Date
    Public Property Shift As String
End Class

Public Class clsQCSResultShiftDB
    Public Shared Function Insert(QCS As clsQCSResultShift, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim cmd As New SqlCommand("sp_QCSResultShift_Ins", Cn, Tr)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("QCSResultID", QCS.QCSResultID)
        cmd.Parameters.AddWithValue("Shift", QCS.Shift)
        cmd.Parameters.AddWithValue("ChangePoint", QCS.ChangePoint)
        cmd.Parameters.AddWithValue("Remark", QCS.Remark)
        cmd.Parameters.AddWithValue("ApprovalStatus", QCS.ApprovalStatus)
        cmd.Parameters.AddWithValue("ApprovalPIC", QCS.ApprovalPIC)
        Dim prm As New SqlParameter
        prm.ParameterName = "QCSResultShiftID"
        prm.Direction = ParameterDirection.Output
        prm.Size = 5
        cmd.Parameters.Add(prm)
        Dim i As Integer = cmd.ExecuteNonQuery()
        QCS.QCSResultShiftID = prm.Value
        Return i
    End Function

    Public Shared Function UpdateRemark(pDate As Date, pShift As Integer, pLineID As String, pSubLineID As String, pPartID As String, pRevNo As Integer, pChangePoint As String, pRemark As String) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "update QCSResultShift set ChangePoint = @ChangePoint, Remark = @Remark " & vbCrLf & _
                "from QCSResultShift S inner join QCSResult R on R.QCSResultID = S.QCSResultID " & vbCrLf & _
                "where R.Date = @Date and R.LineID = @LineID and R.SubLineID = @SubLineID and R.RevNo = @RevNo and S.Shift = @Shift " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Date", Format(pDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("LineID", pLineID)
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
            cmd.Parameters.AddWithValue("RevNo", pRevNo)
            cmd.Parameters.AddWithValue("Shift", pShift)
            cmd.Parameters.AddWithValue("ChangePoint", pChangePoint)
            cmd.Parameters.AddWithValue("Remark", pRemark)
            Dim i As Integer = cmd.ExecuteNonQuery
            Return i
        End Using
    End Function

    Public Shared Function UpdateAttachment(pDate As Date, pShift As Integer, pLineID As String, pSubLineID As String, pPartID As String, pRevNo As Integer, pFileName As String, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim q As String
        q = "update QCSResultShift set FileName = "
        If pFileName <> "" Then
            q = q & "@FileName " & vbCrLf
        Else
            q = q & "NULL " & vbCrLf
        End If
        q = q &
            "from QCSResultShift S inner join QCSResult R on R.QCSResultID = S.QCSResultID " & vbCrLf & _
            "where R.Date = @Date and R.LineID = @LineID and R.SubLineID = @SubLineID and R.RevNo = @RevNo and S.Shift = @Shift " & vbCrLf
        Dim cmd As New SqlCommand(q, Cn, Tr)
        cmd.Parameters.AddWithValue("Date", Format(pDate, "yyyy-MM-dd"))
        cmd.Parameters.AddWithValue("LineID", pLineID)
        cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
        cmd.Parameters.AddWithValue("RevNo", pRevNo)
        cmd.Parameters.AddWithValue("Shift", pShift)
        If pFileName <> "" Then
            cmd.Parameters.AddWithValue("FileName", pFileName)
        End If

        Dim i As Integer = cmd.ExecuteNonQuery
        Return i
    End Function

    Public Shared Function Approve(QCS As clsQCSResultShift) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim Q As String = "Update QCSResultShift set ApprovalStatus = @ApprovalStatus, " & vbCrLf & _
            "ApprovalDate = GetDate(), ApprovalPIC = @ApprovalPIC " & vbCrLf & _
            "where QCSResultShiftID = @QCSResultShiftID "
            Dim cmd As New SqlCommand(Q, Cn)
            cmd.Parameters.AddWithValue("QCSResultShiftID", QCS.QCSResultShiftID)
            cmd.Parameters.AddWithValue("ApprovalStatus", QCS.ApprovalStatus)
            cmd.Parameters.AddWithValue("ApprovalPIC", QCS.ApprovalPIC)
            Dim i As Integer = cmd.ExecuteNonQuery()
            Return i
        End Using
    End Function

    Public Shared Function Delete(QCSResultShiftID) As Integer
        Dim QCSPath As String = clsSetting.QCSPath
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim Q As String
            Q = "Delete QCSResultShift where QCSResultShiftID = @QCSResultShiftID "
            Dim cmd As New SqlCommand(Q, Cn)
            cmd.Parameters.AddWithValue("QCSResultShiftID", QCSResultShiftID)
            Dim i As Integer = cmd.ExecuteNonQuery()
            Return i
        End Using
    End Function

    Public Shared Function GetData(pDate As Date, pShift As String, pPartID As String, pLineID As String, pSubLineID As String, pRevNo As Integer) As clsQCSResultShift
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = _
                "Select top 1 R.RevNo, Q.RevDate, S.* from QCSResult R inner join QCSResultShift S on R.QCSResultID = S.QCSResultID " & vbCrLf & _
                "inner join QCS Q on R.LineID = Q.LineID and R.PartID = Q.PartID and R.RevNo = Q.RevNo " & vbCrLf & _
                "where R.Date = @Date and R.PartID = @PartID and R.LineID = @LineID and R.SubLineID = @SubLineID and S.Shift = @Shift and R.RevNo = @RevNo " & vbCrLf & _
                "order by R.RevNo desc " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Date", Format(pDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("Shift", Val(pShift & ""))
            cmd.Parameters.AddWithValue("PartID", pPartID & "")
            cmd.Parameters.AddWithValue("LineID", pLineID & "")
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID & "")
            cmd.Parameters.AddWithValue("RevNo", pRevNo)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return Nothing
            Else
                Dim Rs As New clsQCSResultShift
                With dt.Rows(0)
                    Rs.QCSResultID = .Item("QCSResultID")
                    Rs.QCSResultShiftID = .Item("QCSResultShiftID")
                    Rs.Shift = .Item("Shift")
                    Rs.Remark = .Item("Remark") & ""
                    Dim Cp As String = Trim(.Item("ChangePoint") & "")
                    If Cp = "" Then
                        Rs.ChangePoint = "0"
                    ElseIf Val(Cp) < 1 Or Val(Cp) > 14 Then
                        Rs.ChangePoint = "0"
                    Else
                        Rs.ChangePoint = .Item("ChangePoint") & ""
                    End If
                    Rs.ApprovalStatus = Val(.Item("ApprovalStatus") & "")
                    If IsDate(.Item("ApprovalDate")) Then
                        Rs.ApprovalDate = .Item("ApprovalDate")
                    Else
                        Rs.ApprovalDate = Nothing
                    End If
                    Rs.ApprovalPIC = RTrim(.Item("ApprovalPIC") & "")
                    Rs.RevNo = .Item("RevNo")
                    Rs.RevDate = .Item("RevDate")
                    Rs.FileName = .Item("FileName") & ""
                End With
                Return Rs
            End If
        End Using
    End Function

    Public Shared Function GetApproval(pUser As String) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String
            q = "select R.[Date], R.LineID, R.PartID, R.SubLineID, R.Shift, R.RevNo " & vbCrLf & _
                "from vw_QCSResult R inner join UserLine L on R.LineID = L.LineID " & vbCrLf & _
                "inner join (Select PartID, LineID, max(RevNo) RevNo from QCS Q where ActiveStatus = 1 group by PartID, LineID) X on R.PartID = X.PartID and R.LineID = X.LineID and R.RevNo = X.RevNo " & vbCrLf & _
                "where L.UserID = @UserID and ApprovalDate is null and Cycle = 5 and PIC <> '' " & vbCrLf & _
                "group by R.[Date], R.LineID, R.PartID, R.SubLineID, R.Shift, R.RevNo " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("UserID", pUser)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt.Rows.Count
        End Using
    End Function

    Public Shared Function GetApprovalList(pUser As String) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String
            q = "select R.[Date], R.LineID, R.PartID, R.PartName, R.SubLineID, R.Shift, R.RevNo " & vbCrLf & _
                "from vw_QCSResult R inner join UserLine L on R.LineID = L.LineID " & vbCrLf & _
                "inner join (Select PartID, LineID, max(RevNo) RevNo from QCS Q where ActiveStatus = 1 group by PartID, LineID) X on R.PartID = X.PartID and R.LineID = X.LineID and R.RevNo = X.RevNo " & vbCrLf & _
                "where L.UserID = @UserID and ApprovalDate is null and Cycle = 5 and PIC <> '' " & vbCrLf & _
                "group by R.[Date], R.LineID, R.PartID, R.PartName, R.SubLineID, R.Shift, R.RevNo " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("UserID", pUser)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Shared Function ValidatePIC(pDate As Date, pShift As String, pPartID As String, pLineID As String, pSubLineID As String, pRevNo As Integer) As String
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String
            q = _
                "select A.*, C.PIC " & vbCrLf & _
                "from ( " & vbCrLf & _
                "	select 1 Cycle union select 2 union select 3 union select 4 union select 5 " & vbCrLf & _
                ") A left join ( " & vbCrLf & _
                "	select Cycle, max(PIC) PIC " & vbCrLf & _
                "	from vw_QCSResult " & vbCrLf & _
                "	where Date = @Date " & vbCrLf & _
                "	and RevNo = @RevNo " & vbCrLf & _
                "	and LineID = @LineID " & vbCrLf & _
                "	and SubLineID = @SubLineID " & vbCrLf & _
                "	and Shift = @Shift " & vbCrLf & _
                "	group by Cycle " & vbCrLf & _
                ") C on A.Cycle = C.Cycle " & vbCrLf & _
                "where isnull(C.PIC, '') = ''"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Date", Format(pDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("Shift", Val(pShift & ""))
            cmd.Parameters.AddWithValue("PartID", pPartID & "")
            cmd.Parameters.AddWithValue("LineID", pLineID & "")
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID & "")
            cmd.Parameters.AddWithValue("RevNo", pRevNo)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return ""
            Else
                Dim s As String = ""
                For i = 0 To dt.Rows.Count - 1
                    s = s & dt.Rows(i)("Cycle") & ","
                Next
                If s <> "" Then
                    s = Mid(s, 1, Len(s) - 1)
                End If
                Return "Please input NRP for Cycle " & s & " and save data first!"
            End If
        End Using
    End Function

    Public Shared Function LastShiftApproved(pDate As Date, pShift As String, pPartID As String, pLineID As String, pSubLineID As String) As String
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String
            q = "select top 1 R.Date, R.LineID, R.SubLineID, R.PartID, R.RevNo, S.Shift, " & vbCrLf & _
                "isnull(S.ApprovalStatus, 0) ApprovalStatus, S.ApprovalDate, S.ApprovalPIC " & vbCrLf & _
                "from QCSResult R inner join QCSResultShift S on R.QCSResultID = S.QCSResultID " & vbCrLf
            q = q & "where PartID = @PartID and LineID = @LineID and SubLineID = @SubLineID " & vbCrLf
            q = q & "and (R.Date < @Date or R.Date = @Date and S.Shift < @Shift) " & vbCrLf

            q = q & "order by Date desc, Shift desc, RevNo desc " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Date", Format(pDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("Shift", Val(pShift & ""))
            cmd.Parameters.AddWithValue("PartID", pPartID & "")
            cmd.Parameters.AddWithValue("LineID", pLineID & "")
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID & "")
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return ""
            ElseIf dt.Rows(0)("ApprovalStatus") = 1 Then
                Return ""
            Else
                With dt.Rows(0)
                    Return "Previous shift has not been approved (" & Format(.Item("Date"), "dd MMM yyyy") & ", Shift " & .Item("Shift") & ")"
                End With
            End If
        End Using
    End Function
End Class