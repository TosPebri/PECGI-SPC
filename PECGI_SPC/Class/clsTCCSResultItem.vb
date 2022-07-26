Imports System.Data.SqlClient

Public Class clsTCCSResultItem
    Public Property TCCSResultID As Integer
    Public Property ItemID As String
    Public Property NumValue As Double?
    Public Property TextValue As String
    Public Property Judgement As String
    Public Property Attachment As String
    Public Property ValueType As String
    Public Property NullValue As Boolean
End Class

Public Class clsTCCSResultItemDB
    Public Shared Function Insert(Item As clsTCCSResultItem, Cn As SqlConnection, Tr As SqlTransaction)
        Dim cmd As New SqlCommand("sp_TCCSResultItem_Ins", Cn, Tr)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("TCCSResultID", Item.TCCSResultID)
        cmd.Parameters.AddWithValue("ItemID", Item.ItemID)
        If Item.NullValue Then

        ElseIf Item.ValueType = "N" Then
            cmd.Parameters.AddWithValue("NumValue", Item.NumValue)
        Else
            cmd.Parameters.AddWithValue("TextValue", Item.TextValue)
        End If
        cmd.Parameters.AddWithValue("Judgement", Item.Judgement)
        cmd.Parameters.AddWithValue("Attachment", Item.Attachment)
        Dim i As Integer = cmd.ExecuteNonQuery()
        Return i
    End Function

    Public Shared Function UpdateAttachment(TCCSResultID As Integer, ItemID As Integer, Attachment As String, Cn As SqlConnection, Tr As SqlTransaction)
        Dim q As String = _
            "Delete from TCCSResultAttachment where TCCSResultID = @TCCSResultID and ItemID = @ItemID " & vbCrLf
        If Attachment <> "" Then
            q = q & "Insert into TCCSResultAttachment (TCCSResultID, ItemID, Attachment) values (@TCCSResultID, @ItemID, @Attachment) " & vbCrLf
        End If
        q = q & _
            "update TCCSResultItem set Attachment = A.Attachment " & vbCrLf & _
            "from TCCSResultItem T left join TCCSResultAttachment A on T.TCCSResultID = A.TCCSResultID and T.ItemID = A.ItemID " & vbCrLf & _
            "where T.TCCSResultID = @TCCSResultID and T.ItemID = @ItemID " & vbCrLf
        Dim cmd As New SqlCommand(q, Cn, Tr)
        cmd.Parameters.AddWithValue("TCCSResultID", TCCSResultID)
        cmd.Parameters.AddWithValue("Attachment", Attachment)
        cmd.Parameters.AddWithValue("ItemID", ItemID)
        Dim i As Integer = cmd.ExecuteNonQuery()
        Return i
    End Function
End Class
