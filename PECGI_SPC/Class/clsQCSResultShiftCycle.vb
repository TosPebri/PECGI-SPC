Imports System.Data.SqlClient

Public Class clsQCSResultShiftCycle
    Public Property QCSResultShiftID As Integer
    Public Property QCSResultShiftCycleID As Integer
    Public Property Cycle As Integer
    Public Property LotNo As String
    Public Property Notes As String
    Public Property PIC As String
    Public Property ProcessCls As String
End Class

Public Class clsQCSResultShiftCycleDB
    Public Shared Function Insert(QCS As clsQCSResultShiftCycle, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim cmd As New SqlCommand("sp_QCSResultShiftCycle_Ins", Cn, Tr)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("QCSResultShiftID", QCS.QCSResultShiftID)
        cmd.Parameters.AddWithValue("Cycle", QCS.Cycle)
        cmd.Parameters.AddWithValue("LotNo", QCS.LotNo)
        cmd.Parameters.AddWithValue("PIC", QCS.PIC)
        Dim prm As New SqlParameter
        prm.ParameterName = "QCSResultShiftCycleID"
        prm.Direction = ParameterDirection.Output
        prm.Size = 5
        cmd.Parameters.Add(prm)
        Dim i As Integer = cmd.ExecuteNonQuery()
        QCS.QCSResultShiftCycleID = prm.Value
        Return i
    End Function

    Public Shared Function Judge(pDate As Date, pLineID As String, pSubLineID As String, pPartID As String, pRevNo As String, pShift As Integer, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim cmd As New SqlCommand("sp_UpdateJudge", Cn, Tr)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("Date", Format(pDate, "yyyy-MM-dd"))
        cmd.Parameters.AddWithValue("LineID", pLineID)
        cmd.Parameters.AddWithValue("SubLineID", pSubLineID)
        cmd.Parameters.AddWithValue("PartID", pPartID)
        cmd.Parameters.AddWithValue("RevNo", pRevNo)
        cmd.Parameters.AddWithValue("Shift", pShift)
        Dim i As Integer = cmd.ExecuteNonQuery()
        Return i
    End Function

    Public Shared Function UpdatePIC(Rsc As clsQCSResultShiftCycle, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim Q As String = "Update QCSResultShiftCycle set LastUpdate = GetDate() " & vbCrLf & _
            "where QCSResultShiftID = @QCSResultShiftID and Cycle = @Cycle and isnull(PIC, '') <> @PIC " & vbCrLf & _
            "Update QCSResultShiftCycle set PIC = @PIC " & vbCrLf & _
            "where QCSResultShiftID = @QCSResultShiftID and Cycle = @Cycle "
        Dim cmd As New SqlCommand(Q, Cn, Tr)
        cmd.Parameters.AddWithValue("QCSResultShiftID", Rsc.QCSResultShiftID)
        cmd.Parameters.AddWithValue("PIC", Rsc.PIC)
        cmd.Parameters.AddWithValue("Cycle", Rsc.Cycle)
        Dim i As Integer = cmd.ExecuteNonQuery()
        Return i
    End Function

    Public Shared Function UpdateProcess(Rsc As clsQCSResultShiftCycle, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim Q As String = "Update QCSResultShiftCycle set ProcessCls = @ProcessCls, Notes = @ProcessCls " & vbCrLf & _
            "where QCSResultShiftID = @QCSResultShiftID and Cycle = @Cycle "
        Dim cmd As New SqlCommand(Q, Cn, Tr)
        Rsc.ProcessCls = Mid(Rsc.ProcessCls, 1, 1)
        cmd.Parameters.AddWithValue("QCSResultShiftID", Rsc.QCSResultShiftID)
        cmd.Parameters.AddWithValue("ProcessCls", Rsc.ProcessCls)
        cmd.Parameters.AddWithValue("Cycle", Rsc.Cycle)
        Dim i As Integer = cmd.ExecuteNonQuery()
        Return i
    End Function

    Public Shared Function UpdateNotes(Rsc As clsQCSResultShiftCycle, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim Q As String = "Update QCSResultShiftCycle set Notes = @Notes " & vbCrLf & _
            "where QCSResultShiftID = @QCSResultShiftID and Cycle = @Cycle "
        Dim cmd As New SqlCommand(Q, Cn, Tr)
        cmd.Parameters.AddWithValue("QCSResultShiftID", Rsc.QCSResultShiftID)
        cmd.Parameters.AddWithValue("Notes", Rsc.Notes)
        cmd.Parameters.AddWithValue("Cycle", Rsc.Cycle)
        Dim i As Integer = cmd.ExecuteNonQuery()
        Return i
    End Function

    Public Shared Function UpdateLotNo(Rsc As clsQCSResultShiftCycle, Cn As SqlConnection, Tr As SqlTransaction) As Integer
        Dim Q As String = "Update QCSResultShiftCycle set LotNo = @LotNo " & vbCrLf & _
            "where QCSResultShiftID = @QCSResultShiftID and Cycle = @Cycle "
        Dim cmd As New SqlCommand(Q, Cn, Tr)
        cmd.Parameters.AddWithValue("QCSResultShiftID", Rsc.QCSResultShiftID)
        cmd.Parameters.AddWithValue("LotNo", Rsc.LotNo)
        cmd.Parameters.AddWithValue("Cycle", Rsc.Cycle)
        Dim i As Integer = cmd.ExecuteNonQuery()
        Return i
    End Function
End Class