Imports System.Data.SqlClient

Public Class ClsLine
    Public Property FactoryCode As String
    Public Property ProcessCode As String
    Public Property LineCode As String
    Public Property VisualProcessCode As String
    Public Property LineName As String
End Class


Public Class ClsLineDB
    Public Shared Function GetList(FactoryCode As String, ItemTypeCode As String) As List(Of ClsLine)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select distinct L.FactoryCode, L.ProcessCode, L.LineCode, L.LineCode + ' - ' + L.LineName as LineName " & vbCrLf &
                "from MS_Line L inner join spc_ItemCheckByType I " & vbCrLf &
                "on L.FactoryCode = I.FactoryCode and L.LineCode = I.LineCode " & vbCrLf &
                "where L.LineCode is not Null " & vbCrLf
            If FactoryCode <> "" Then
                q = q & "and L.FactoryCode = @FactoryCode "
            End If
            If ItemTypeCode <> "" Then
                q = q & "and I.ItemTypeCode = @ItemTypeCode "
            End If
            q = q & "order by LineCode"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("FactoryCode", FactoryCode)
            cmd.Parameters.AddWithValue("ItemTypeCode", ItemTypeCode)
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