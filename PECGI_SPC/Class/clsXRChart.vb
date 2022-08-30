Imports System.Data.SqlClient

Public Class clsXRChart
    Public Property Seq As Integer
    Public Property Description As String
    Public Property Value As Single?
    Public Property Warning As Single?
End Class

Public Class clsXRChartDB
    Public Shared Function GetChartX() As List(Of clsXRChart)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim xrList As New List(Of clsXRChart)
            Dim xr As clsXRChart
            xr = New clsXRChart
            xr.Seq = 1
            xr.Description = "Min"
            xr.Value = 10
            xrList.Add(xr)

            xr = New clsXRChart
            xr.Seq = 2
            xr.Description = "Min"
            xr.Value = 9
            xrList.Add(xr)

            xr = New clsXRChart
            xr.Seq = 3
            xr.Description = "Min"
            xr.Value = 6
            xrList.Add(xr)

            xr = New clsXRChart
            xr.Seq = 4
            xr.Description = "Min"
            xr.Value = 11
            xrList.Add(xr)

            xr = New clsXRChart
            xr.Seq = 1
            xr.Description = "Max"
            xr.Value = 8
            xrList.Add(xr)

            xr = New clsXRChart
            xr.Seq = 2
            xr.Description = "Max"
            xr.Value = 11
            xrList.Add(xr)

            Return xrList
        End Using
    End Function
End Class
