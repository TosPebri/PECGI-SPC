Imports System.Data.SqlClient

Public Class clsItemCheck
    Public Property ItemCheckCode As String
    Public Property ItemCheck As String
End Class

Public Class clsItemCheckDB
    Public Shared Function GetList() As List(Of clsItemCheck)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select * from spc_ItemCheckMaster"
            Dim cmd As New SqlCommand(q, Cn)
            Dim rd As SqlDataReader = cmd.ExecuteReader
            Dim ItemCheckList As New List(Of clsItemCheck)
            Do While rd.Read
                Dim ItemCheck As New clsItemCheck
                ItemCheck.ItemCheckCode = rd("ItemCheckCode") & ""
                ItemCheck.ItemCheck = rd("ItemCheck") & ""
                ItemCheckList.Add(ItemCheck)
            Loop
            rd.Close()
            Return ItemCheckList
        End Using
    End Function
End Class
