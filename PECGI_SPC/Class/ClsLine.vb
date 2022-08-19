Imports System.Data.SqlClient

Public Class ClsLine
    Public Property FactoryCode As String
    Public Property ProcessCode As String
    Public Property LineCode As String
    Public Property VisualProcessCode As String
    Public Property LineName As String
End Class


Public Class ClsLineDB
    Public Shared Function GetList(Optional FactoryCode As String = "", Optional ProcessCode As String = "") As List(Of ClsLine)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select FactoryCode, ProcessCode, LineCode, LineCode + ' - ' + LineName as LineName " & vbCrLf &
                "from MS_Line where LineCode is not Null " & vbCrLf
            If FactoryCode <> "" Then
                q = q & "and FactoryCode = @FactoryCode "
            End If
            If ProcessCode <> "" Then
                q = q & "and ProcessCode = @ProcessCode "
            End If
            q = q & "order by LineCode"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("FactoryCode", FactoryCode)
            cmd.Parameters.AddWithValue("ProcessCode", ProcessCode)
            Dim rd As SqlDataReader = cmd.ExecuteReader
            Dim FactoryList As New List(Of ClsLine)
            Do While rd.Read
                Dim Factory As New ClsLine
                Factory.FactoryCode = rd("FactoryCode")
                Factory.ProcessCode = rd("ProcessCode")
                Factory.LineCode = rd("LineCode")
                Factory.LineName = rd("LineName")
                FactoryList.Add(Factory)
            Loop
            rd.Close()
            Return FactoryList
        End Using
    End Function
End Class