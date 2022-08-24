Imports System.Data.SqlClient

Public Class clsFrequency
    Public Property FrequencyCode As String
End Class

Public Class clsShift
    Public Property ShiftCode As String
End Class

Public Class clsSequenceNo
    Public Property SequenceNo As Integer
End Class

Public Class clsFrequencyDB
    Public Shared Function GetShift(FactoryCode As String, ItemTypeCode As String, LineCode As String, ItemCheckCode As String) As List(Of clsShift)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select distinct F.ShiftCode " & vbCrLf &
                "From spc_ItemCheckByType T inner Join spc_MS_Frequency F on T.FrequencyCode = F.FrequencyCode " & vbCrLf &
                "where FactoryCode = @FactoryCode and ItemTypeCode = @ItemTypeCode and LineCode = @LineCode and ItemCheckCode = @ItemCheckCode  " & vbCrLf &
                "and T.ActiveStatus = 1 and F.ActiveStatus = 1  " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("FactoryCode", FactoryCode)
            cmd.Parameters.AddWithValue("ItemTypeCode", ItemTypeCode)
            cmd.Parameters.AddWithValue("LineCode", LineCode)
            cmd.Parameters.AddWithValue("ItemCheckCode", ItemCheckCode)
            Dim rd As SqlDataReader = cmd.ExecuteReader
            Dim ShiftList As New List(Of clsShift)
            Do While rd.Read
                Dim Shift As New clsShift
                Shift.ShiftCode = rd("ShiftCode")
                ShiftList.Add(Shift)
            Loop
            rd.Close()
            Return ShiftList
        End Using
    End Function

    Public Shared Function GetSequence(FactoryCode As String, ItemTypeCode As String, LineCode As String, ItemCheckCode As String, ShiftCode As String) As List(Of clsSequenceNo)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "select distinct SequenceNo " & vbCrLf &
                "From spc_ItemCheckByType T inner Join spc_MS_Frequency F on T.FrequencyCode = F.FrequencyCode " & vbCrLf &
                "where FactoryCode = @FactoryCode and ItemTypeCode = @ItemTypeCode and LineCode = @LineCode and ItemCheckCode = @ItemCheckCode and ShiftCode = @ShiftCode " & vbCrLf &
                "and T.ActiveStatus = 1 and F.ActiveStatus = 1  " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("FactoryCode", FactoryCode)
            cmd.Parameters.AddWithValue("ItemTypeCode", ItemTypeCode)
            cmd.Parameters.AddWithValue("LineCode", LineCode)
            cmd.Parameters.AddWithValue("ItemCheckCode", ItemCheckCode)
            cmd.Parameters.AddWithValue("ShiftCode", ShiftCode)
            Dim rd As SqlDataReader = cmd.ExecuteReader
            Dim SequenceList As New List(Of clsSequenceNo)
            Do While rd.Read
                Dim Shift As New clsSequenceNo
                Shift.SequenceNo = rd("SequenceNo")
                SequenceList.Add(Shift)
            Loop
            rd.Close()
            Return SequenceList
        End Using
    End Function
End Class