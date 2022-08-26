Imports System.Data.SqlClient

Public Class clsItemType
    Public Property ItemTypeCode As String
    Public Property Description As String
End Class

Public Class clsItemTypeDB
    Public Shared Function GetList(FactoryCode As String) As List(Of clsItemType)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select distinct I.ItemTypeCode, M.Description " & vbCrLf &
                "From spc_ItemCheckByType I inner Join MS_ItemType M on I.ItemTypeCode = M.ItemTypeCode  " & vbCrLf &
                "Where I.FactoryCode = @FactoryCode "
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("FactoryCode", FactoryCode)
            Dim rd As SqlDataReader = cmd.ExecuteReader
            Dim ItemTypeList As New List(Of clsItemType)
            Do While rd.Read
                Dim ItemType As New clsItemType
                ItemType.ItemTypeCode = rd("ItemTypeCode") & ""
                ItemType.Description = rd("Description") & ""
                ItemTypeList.Add(ItemType)
            Loop
            rd.Close()
            Return ItemTypeList
        End Using
    End Function
End Class