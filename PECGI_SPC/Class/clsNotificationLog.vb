Imports System.Data.SqlClient

Public Class clsNotificationLog
    Public Property FactoryCode As String
    Public Property ItemTypeCode As String
    Public Property LineCode As String
    Public Property ItemCheckCode As String
    Public Property ProdDate As Date
    Public Property ShiftCode As String
    Public Property SequenceNo As Integer
    Public Property NotificationCategory As String
    Public Property EmailDate As String
    Public Property LastUpdate As Date
    Public Property LastUser As String
End Class

Public Class clsNotificationLogDB
    Public Shared Function Insert(Notif As clsNotificationLog)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "sp_SPC_NotificationLog_Ins"
            Dim cmd As New SqlCommand(q, Cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("FactoryCode", Notif.FactoryCode)
            cmd.Parameters.AddWithValue("ItemTypeCode", Notif.ItemTypeCode)
            cmd.Parameters.AddWithValue("LineCode", Notif.LineCode)
            cmd.Parameters.AddWithValue("ItemCheckCode", Notif.ItemCheckCode)
            cmd.Parameters.AddWithValue("ProdDate", Notif.ProdDate)
            cmd.Parameters.AddWithValue("ShiftCode", Notif.ShiftCode)
            cmd.Parameters.AddWithValue("SequenceNo", Notif.SequenceNo)
            cmd.Parameters.AddWithValue("NotificationCategory", Notif.NotificationCategory)
            cmd.Parameters.AddWithValue("LastUser", Notif.LastUser)

        End Using
    End Function
End Class
