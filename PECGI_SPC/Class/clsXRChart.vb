Imports System.Data.SqlClient

Public Class clsXRChart
    Public Property Seq As Integer
    Public Property Description As String
    Public Property Value As Single?
    Public Property Warning As Single?
End Class

Public Class clsXRChartDB
    Public Shared Function GetDataset(DateFrom As Date, DateTo As Date, PartID As String, LineID As String, SubLineID As String, ItemID As Integer) As DataSet
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = ""
            Dim cmd As New SqlCommand("sp_XRChart_Sel", Cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 120
            cmd.Parameters.AddWithValue("DateFrom", Format(DateFrom, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("DateTo", Format(DateTo, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("PartID", PartID)
            cmd.Parameters.AddWithValue("LineID", LineID)
            cmd.Parameters.AddWithValue("SubLineID", SubLineID)
            cmd.Parameters.AddWithValue("ItemID", ItemID)
            Dim da As New SqlDataAdapter(cmd)
            Dim ds As New DataSet
            da.Fill(ds)
            Return ds
        End Using
    End Function

    Public Shared Function GetChartX(UCL As Single, LCL As Single, XBar As String) As List(Of clsXRChart)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim xrList As New List(Of clsXRChart)
            Dim n As Integer = UBound(Split(XBar, "/"))
            Dim xr As clsXRChart
            For i = 1 To n
                Dim diff As Single?
                xr = New clsXRChart
                xr.Seq = i
                xr.Description = "XBar"
                If Split(XBar, "/")(i - 1) <> "" Then
                    xr.Value = CSng(Split(XBar, "/")(i - 1))
                    If xr.Value > UCL Then
                        diff = xr.Value
                    ElseIf xr.Value < LCL Then
                        diff = xr.Value
                    Else
                        diff = Nothing
                    End If
                Else
                    xr.Value = Nothing
                    diff = Nothing
                End If
                xr.Warning = diff
                
                xrList.Add(xr)

                xr = New clsXRChart
                xr.Seq = i
                xr.Description = "UCL"
                xr.Value = UCL
                xr.Warning = diff
                If diff IsNot Nothing Then

                End If
                xrList.Add(xr)

                xr = New clsXRChart
                xr.Seq = i
                xr.Description = "LCL"
                xr.Value = LCL
                xr.Warning = diff
                If diff IsNot Nothing Then

                End If
                xrList.Add(xr)
            Next
            Return xrList
        End Using
    End Function

    Public Shared Function GetChartR(UCL As Single, RBar As String) As List(Of clsXRChart)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim xrList As New List(Of clsXRChart)
            Dim xr As clsXRChart
            For i = 1 To UBound(Split(RBar, "/"))
                xr = New clsXRChart
                xr.Seq = i
                xr.Description = "RBar"
                Dim diff As Single?
                If Split(RBar, "/")(i - 1) <> "" Then                    
                    xr.Value = CSng(Split(RBar, "/")(i - 1))
                    If xr.Value > UCL Then
                        diff = xr.Value
                    Else
                        diff = Nothing
                    End If
                Else
                    xr.Value = Nothing
                    diff = Nothing
                End If
                xr.Warning = diff
                If diff IsNot Nothing Then

                End If
                xrList.Add(xr)

                xr = New clsXRChart
                xr.Seq = i
                xr.Description = "UCL"
                xr.Value = UCL
                xr.Warning = diff
                If diff IsNot Nothing Then

                End If
                xrList.Add(xr)
            Next
            Return xrList
        End Using
    End Function

    Public Shared Function GetXRCode(PartID As String, LineID As String, MachineNo As String) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select distinct I.ItemID, I.Item, I.XRCode " & vbCrLf & _
                "from QCSItem I inner join QCS Q on I.PartID = Q.PartID and I.RevNo = Q.RevNo and I.LineID = Q.LineID " & vbCrLf & _
                "inner join SubLine S on Q.LineID = S.LineID and S.ProcessID = I.ProcessID " & vbCrLf & _
                "where Q.ActiveStatus = 1 " & vbCrLf & _
                "and isnull(I.XRCode, '') <> '' " & vbCrLf & _
                "and I.PartID = @PartID and I.LineID = @LineID and S.MachineNo = @MachineNo "
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("PartID", PartID)
            cmd.Parameters.AddWithValue("LineID", LineID)
            cmd.Parameters.AddWithValue("MachineNo", MachineNo)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Shared Function GetSummary(DateFrom As Date, DateTo As Date, LineID As String, SubLineID As String, PartID As String, MachineNo As String, ItemID As String, UCL As Double, LCL As Double) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "sp_XRChartSummary"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("DateFrom", Format(DateFrom, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("DateTo", Format(DateTo, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("LineID", LineID)
            cmd.Parameters.AddWithValue("SubLineID", SubLineID)
            cmd.Parameters.AddWithValue("PartID", PartID)
            cmd.Parameters.AddWithValue("MachineNo", MachineNo)
            cmd.Parameters.AddWithValue("ItemID", ItemID)
            cmd.Parameters.AddWithValue("UCL", UCL)
            cmd.Parameters.AddWithValue("LCL", LCL)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function
End Class
