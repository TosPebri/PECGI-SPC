Imports System.Data.SqlClient

Public Class clsItemType
    Public Property ItemTypeCode As String
    Public Property Description As String
End Class

Public Class clsItemTypeDB
    Public Shared Function GetList() As List(Of clsItemType)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select * from MS_ItemType"
            Dim cmd As New SqlCommand(q, Cn)
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