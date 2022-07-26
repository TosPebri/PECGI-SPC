Imports System.Data.SqlClient

Public Class clsQCSXRHistory
    Public Property PartID As String
    Public Property XRCode As String
    Public Property Period As Date
    Public Property XUCL As Double
    Public Property XUCLAdjusted As Double?
    Public Property XLCL As Double
    Public Property XLCLAdjusted As Double?
    Public Property RUCL As Double
    Public Property RUCLAdjusted As Double?
    Public Property User As String
End Class

Public Class clsQCSXRHistoryDB
    Public Shared Function Insert(XR As clsQCSXRHistory) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Insert into QCSXRHistory (" & vbCrLf & _
                "PartID, XRCode, Period, " & vbCrLf & _
                "XUCL, XUCLAdjusted, XLCL, XLCLAdjusted, RUCL, RUCLAdjusted, " & vbCrLf & _
                "CreateDate, CreateUser ) values ( " & vbCrLf & _
                "@PartID, @XRCode, @Period, " & vbCrLf & _
                "@XUCL, @XUCLAdjusted, @XLCL, @XLCLAdjusted, @RUCL, @RUCLAdjusted, " & vbCrLf & _
                "GetDate(), @CreateUser ) "
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Period", Format(XR.Period, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("PartID", XR.PartID)
            cmd.Parameters.AddWithValue("XRCode", XR.XRCode)
            cmd.Parameters.AddWithValue("CreateUser", XR.User)
            Dim i As Integer = cmd.ExecuteNonQuery
            Return i
        End Using
    End Function

    Public Shared Function UpdateAdjustment(XR As clsQCSXRHistory) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Update QCSXRHistory set " & vbCrLf &
                "XUCL = @XUCL, XLCL = @XLCL, RUCL = @RUCL, " & vbCrLf &
                "XUCLAdjusted = @XUCLAdjusted, " & vbCrLf &
                "XLCLAdjusted = @XLCLAdjusted, " & vbCrLf &
                "RUCLAdjusted = @RUCLAdjusted, " & vbCrLf &
                "UpdateDate = GetDate(), UpdateUser = @UpdateUser " & vbCrLf &
                "where PartID = @PartID And XRCode = @XRCode And Period = @Period " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Period", Format(XR.Period, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("PartID", XR.PartID)
            cmd.Parameters.AddWithValue("XRCode", XR.XRCode)
            cmd.Parameters.AddWithValue("XUCL", XR.XUCL)
            cmd.Parameters.AddWithValue("XLCL", XR.XLCL)
            cmd.Parameters.AddWithValue("RUCL", XR.RUCL)
            cmd.Parameters.AddWithValue("XUCLAdjusted", IIf(XR.XUCLAdjusted.HasValue, XR.XUCLAdjusted, DBNull.Value))
            cmd.Parameters.AddWithValue("XLCLAdjusted", IIf(XR.XLCLAdjusted.HasValue, XR.XLCLAdjusted, DBNull.Value))
            cmd.Parameters.AddWithValue("RUCLAdjusted", IIf(XR.RUCLAdjusted.HasValue, XR.RUCLAdjusted, DBNull.Value))
            cmd.Parameters.AddWithValue("UpdateUser", XR.User)
            Dim i As Integer = cmd.ExecuteNonQuery
            If i = 0 Then
                q = "Insert into QCSXRHistory (" & vbCrLf & _
                    "   PartID, XRCode, Period, XUCL, XLCL, RUCL, XUCLAdjusted, XLCLAdjusted, RUCLAdjusted, " & vbCrLf & _
                    "   CreateDate, CreateUser " & vbCrLf & _
                    ") values ( " & vbCrLf
                q = q & _
                    "   @PartID, @XRCode, @Period, @XUCL, @XLCL, @RUCL, @XUCLAdjusted, @XLCLAdjusted, @RUCLAdjusted, " & vbCrLf & _
                    "   GetDate(), @UpdateUser ) " & vbCrLf
                cmd.CommandText = q
                cmd.ExecuteNonQuery()
            End If
            Return i
        End Using
    End Function

    Public Shared Function Delete(XR As clsQCSXRHistory) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Delete from QCSXRHistory " & vbCrLf & _
                "where PartID = @PartID and XRCode = @XRCode and Period = @Period " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("PartID", XR.PartID)
            cmd.Parameters.AddWithValue("XRCode", XR.XRCode)
            cmd.Parameters.AddWithValue("Period", Format(XR.Period, "yyyy-MM-dd"))
            Dim i As Integer = cmd.ExecuteNonQuery
            Return i
        End Using
    End Function

    Public Shared Function GetData(Period As Date, LineID As String, SubLineID As String, Process As String, PartID As String, XRCode As String) As clsQCSXRHistory
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = _
                "declare @MaxPeriod date = (Select max(Period) from QCSXRHistory where PartID = @PartID and XRCode = @XRCode and Period <= @Period) " & vbCrLf & _
                "if @MaxPeriod is null set @MaxPeriod = @Period " & vbCrLf & _
                "Select * from QCSXRHistory " & vbCrLf & _
                "where PartID = @PartID and XRCode = @XRCode and Period = @MaxPeriod " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("PartID", PartID)
            cmd.Parameters.AddWithValue("XRCode", XRCode)
            cmd.Parameters.AddWithValue("Period", Format(Period, "yyyy-MM-dd"))
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return Nothing
            Else
                With dt.Rows(0)
                    Dim XR As New clsQCSXRHistory
                    XR.Period = Period
                    XR.PartID = PartID
                    XR.XRCode = XRCode
                    If Not IsDBNull(dt.Rows(0)("XUCLAdjusted")) Then
                        XR.XUCLAdjusted = CDbl(dt.Rows(0)("XUCLAdjusted"))
                    End If
                    If Not IsDBNull(dt.Rows(0)("XLCLAdjusted")) Then
                        XR.XLCLAdjusted = CDbl(dt.Rows(0)("XLCLAdjusted"))
                    End If
                    If Not IsDBNull(dt.Rows(0)("RUCLAdjusted")) Then
                        XR.RUCLAdjusted = CDbl(dt.Rows(0)("RUCLAdjusted"))
                    End If
                    Return XR
                End With
            End If
        End Using
    End Function

    Public Shared Function GetTable(Period As Date, LineID As String, SubLineID As String, Process As String, PartID As String, XRCode As String) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Select * from QCSXRHistory " & vbCrLf & _
                "where PartID = @PartID and XRCode = @XRCode and Period = @Period " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("PartID", PartID)
            cmd.Parameters.AddWithValue("XRCode", XRCode)
            cmd.Parameters.AddWithValue("Period", Format(Period, "yyyy-MM-dd"))
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function
End Class
