Imports System.Data.SqlClient

Public Class clsQCSTime
    Public Property Shift As Integer
    Public Property Cycle As Integer
    Public Property Time As String
End Class

Public Class clsQCSTimeCycle
    Public Property Shift As Integer
    Public Property Cycle1Time As String
    Public Property Cycle2Time As String
    Public Property Cycle3Time As String
    Public Property Cycle4Time As String
    Public Property Cycle5Time As String
End Class

Public Class clsQCSLastUpdate
    Public Property LastUpdate11 As String
    Public Property LastUpdate12 As String
    Public Property LastUpdate13 As String
    Public Property LastUpdate14 As String
    Public Property LastUpdate15 As String

    Public Property LastUpdate21 As String
    Public Property LastUpdate22 As String
    Public Property LastUpdate23 As String
    Public Property LastUpdate24 As String
    Public Property LastUpdate25 As String

    Public Property LastUpdate31 As String
    Public Property LastUpdate32 As String
    Public Property LastUpdate33 As String
    Public Property LastUpdate34 As String
    Public Property LastUpdate35 As String
End Class

Public Class clsQCSTimeDB
    Public Shared Function GetList(pShift As String) As List(Of clsQCSTime)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Select * from QCSTime where Shift = @Shift"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Shift", pShift)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Dim qtList As New List(Of clsQCSTime)
            For i = 0 To dt.Rows.Count - 1
                Dim qt As New clsQCSTime With {
                    .Shift = dt.Rows(i)("Shift"),
                    .Cycle = dt.Rows(i)("Cycle"),
                    .Time = dt.Rows(i)("Time")
                }
                qtList.Add(qt)
            Next
            Return qtList
        End Using
    End Function

    Public Shared Function GetLastUpdate(pDate As Date, pLineID As String, pSubLineID As String, pPartID As String, pRevNo As Integer, Optional pShift As Integer = 0) As clsQCSLastUpdate
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String
            If pShift = 0 Then
                q = _
                "select  " & vbCrLf & _
                "  max(case when Shift = 1 and Cycle = 1 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate11 " & vbCrLf & _
                ", max(case when Shift = 1 and Cycle = 2 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate12 " & vbCrLf & _
                ", max(case when Shift = 1 and Cycle = 3 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate13 " & vbCrLf & _
                ", max(case when Shift = 1 and Cycle = 4 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate14 " & vbCrLf & _
                ", max(case when Shift = 1 and Cycle = 5 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate15 " & vbCrLf & _
                ", max(case when Shift = 2 and Cycle = 1 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate21 " & vbCrLf & _
                ", max(case when Shift = 2 and Cycle = 2 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate22 " & vbCrLf & _
                ", max(case when Shift = 2 and Cycle = 3 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate23 " & vbCrLf & _
                ", max(case when Shift = 2 and Cycle = 4 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate24 " & vbCrLf & _
                ", max(case when Shift = 2 and Cycle = 5 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate25 " & vbCrLf & _
                ", max(case when Shift = 3 and Cycle = 1 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate31 " & vbCrLf & _
                ", max(case when Shift = 3 and Cycle = 3 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate32 " & vbCrLf & _
                ", max(case when Shift = 3 and Cycle = 3 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate33 " & vbCrLf & _
                ", max(case when Shift = 3 and Cycle = 4 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate34 " & vbCrLf & _
                ", max(case when Shift = 3 and Cycle = 5 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate35 " & vbCrLf & _
                "from vw_QCSResult  " & vbCrLf
                q = q & _
                    "where [Date] = @Date and LineID = @LineID and SubLineID = @SubLineID " & vbCrLf & _
                    "and PartID = @PartID and RevNo = @RevNo "
            Else
                q = _
                "select  " & vbCrLf & _
                "  max(case when Cycle = 1 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate11 " & vbCrLf & _
                ", max(case when Cycle = 2 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate12 " & vbCrLf & _
                ", max(case when Cycle = 3 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate13 " & vbCrLf & _
                ", max(case when Cycle = 4 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate14 " & vbCrLf & _
                ", max(case when Cycle = 5 then convert(char(5), LastUpdate, 14) else '' end) LastUpdate15 " & vbCrLf & _
                "from vw_QCSResult  " & vbCrLf
                q = q & _
                    "where [Date] = @Date and LineID = @LineID and SubLineID = @SubLineID " & vbCrLf & _
                    "and PartID = @PartID and RevNo = @RevNo and Shift = " & pShift
            End If
            
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Date", Format(pDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("LineID", pLineID & "")
            cmd.Parameters.AddWithValue("SubLineID", pSubLineID & "")
            cmd.Parameters.AddWithValue("PartID", pPartID & "")
            cmd.Parameters.AddWithValue("RevNo", pRevNo)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return Nothing
            Else
                With dt.Rows(0)
                    Dim qt As New clsQCSLastUpdate
                    qt.LastUpdate11 = dt.Rows(0)("LastUpdate11") & ""
                    qt.LastUpdate12 = dt.Rows(0)("LastUpdate12") & ""
                    qt.LastUpdate13 = dt.Rows(0)("LastUpdate13") & ""
                    qt.LastUpdate14 = dt.Rows(0)("LastUpdate14") & ""
                    qt.LastUpdate15 = dt.Rows(0)("LastUpdate15") & ""

                    If pShift = 0 Then
                        qt.LastUpdate21 = dt.Rows(0)("LastUpdate21") & ""
                        qt.LastUpdate22 = dt.Rows(0)("LastUpdate22") & ""
                        qt.LastUpdate23 = dt.Rows(0)("LastUpdate23") & ""
                        qt.LastUpdate24 = dt.Rows(0)("LastUpdate24") & ""
                        qt.LastUpdate25 = dt.Rows(0)("LastUpdate25") & ""

                        qt.LastUpdate21 = dt.Rows(0)("LastUpdate31") & ""
                        qt.LastUpdate22 = dt.Rows(0)("LastUpdate32") & ""
                        qt.LastUpdate23 = dt.Rows(0)("LastUpdate33") & ""
                        qt.LastUpdate24 = dt.Rows(0)("LastUpdate34") & ""
                        qt.LastUpdate25 = dt.Rows(0)("LastUpdate35") & ""
                    End If
                    If qt.LastUpdate11 = "" Then qt.LastUpdate11 = "-"
                    If qt.LastUpdate12 = "" Then qt.LastUpdate12 = "-"
                    If qt.LastUpdate13 = "" Then qt.LastUpdate13 = "-"
                    If qt.LastUpdate14 = "" Then qt.LastUpdate14 = "-"
                    If qt.LastUpdate15 = "" Then qt.LastUpdate15 = "-"
                    If qt.LastUpdate21 = "" Then qt.LastUpdate21 = "-"
                    If qt.LastUpdate22 = "" Then qt.LastUpdate22 = "-"
                    If qt.LastUpdate23 = "" Then qt.LastUpdate23 = "-"
                    If qt.LastUpdate24 = "" Then qt.LastUpdate24 = "-"
                    If qt.LastUpdate25 = "" Then qt.LastUpdate25 = "-"
                    If qt.LastUpdate31 = "" Then qt.LastUpdate31 = "-"
                    If qt.LastUpdate32 = "" Then qt.LastUpdate32 = "-"
                    If qt.LastUpdate33 = "" Then qt.LastUpdate33 = "-"
                    If qt.LastUpdate34 = "" Then qt.LastUpdate34 = "-"
                    If qt.LastUpdate35 = "" Then qt.LastUpdate35 = "-"
                    Return qt
                End With
            End If
        End Using
    End Function

    Public Shared Function GetCycle(pShift As String) As clsQCSTimeCycle
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = ""
            For i = 1 To 5
                q = q & "Select " & i & " [Shift], " & vbCrLf & _
                    "case when isnull(convert(char(5), max(QCSTime), 14), '') = '' then ' ' else isnull(convert(char(5), max(QCSTime), 14), '') end QCSTime " & vbCrLf & _
                    "from QCSTime where Shift = @Shift and Cycle = " & i & vbCrLf
                If i < 5 Then
                    q = q & "union all " & vbCrLf
                End If
            Next
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Shift", pShift)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Dim qt As New clsQCSTimeCycle
            qt.Cycle1Time = dt.Rows(0)("QCSTime")
            qt.Cycle2Time = dt.Rows(1)("QCSTime")
            qt.Cycle3Time = dt.Rows(2)("QCSTime")
            qt.Cycle4Time = dt.Rows(3)("QCSTime")
            qt.Cycle5Time = dt.Rows(4)("QCSTime")
            Return qt
        End Using
    End Function

    Public Shared Function StartHour() As Integer
        Return 7
    End Function
End Class
