Imports System.Data.SqlClient

Public Class clsQCSXRAction
    Public Property ActionDate As Date
    Public Property LineID As String
    Public Property SubLineID As String
    Public Property ProcessID As String
    Public Property PartID As String
    Public Property XRCode As String
    Public Property PIC As String
    Public Property Action As String
    Public Property Result As String
    Public Property User As String
End Class

Public Class clsQCSXRActionDB
    Public Shared Function Insert(XR As clsQCSXRAction) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String
            q = "Update QCSXRAction set " & vbCrLf & _
                "PIC = @PIC, Action = @Action, Result = @Result, CreateDate = GetDate(), CreateUser = @CreateUser " & vbCrLf & _
                "where Date = @Date and LineID = @LineID and SubLineID = @SubLineID and ProcessID = @ProcessID and PartID = @PartID and XRCode = @XRCode " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Date", Format(XR.ActionDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("LineID", XR.LineID)
            cmd.Parameters.AddWithValue("SubLineID", XR.SubLineID)
            cmd.Parameters.AddWithValue("ProcessID", XR.ProcessID)
            cmd.Parameters.AddWithValue("PartID", XR.PartID)
            cmd.Parameters.AddWithValue("XRCode", XR.XRCode)
            cmd.Parameters.AddWithValue("PIC", XR.PIC & "")
            cmd.Parameters.AddWithValue("Action", XR.Action)
            cmd.Parameters.AddWithValue("Result", XR.Result)
            cmd.Parameters.AddWithValue("CreateUser", XR.User)
            Dim i As Integer = cmd.ExecuteNonQuery
            If i = 0 Then
                q = "Insert into QCSXRAction (" & vbCrLf & _
                    "Date, LineID, SubLineID, ProcessID, PartID, XRCode, " & vbCrLf & _
                    "PIC, Action, Result, CreateDate, CreateUser ) values ( " & vbCrLf & _
                    "@Date, @LineID, @SubLineID, @ProcessID, @PartID, @XRCode, " & vbCrLf & _
                    "@PIC, @Action, @Result, GetDate(), @CreateUser ) "
                cmd.CommandText = q
                cmd.ExecuteNonQuery()
            End If
            Return i
        End Using
    End Function

    Public Shared Function Update(XR As clsQCSXRAction) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Update QCSXRAction set " & vbCrLf & _
                "PIC = @PIC, Action = @Action, Result = @Result, UpdateDate = GetDate(), CreateUser = @CreateUser " & vbCrLf & _
                "where Date = @Date and LineID = @LineID and SubLine = @SubLine " & vbCrLf & _
                "and Process = @Process and PartID = @PartID and XRCode = @XRCode " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Date", Format(XR.ActionDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("LineID", XR.LineID)
            cmd.Parameters.AddWithValue("SubLine", XR.SubLineID)
            cmd.Parameters.AddWithValue("Process", XR.ProcessID)
            cmd.Parameters.AddWithValue("PartID", XR.PartID)
            cmd.Parameters.AddWithValue("XRCode", XR.XRCode)
            cmd.Parameters.AddWithValue("PIC", XR.PIC)
            cmd.Parameters.AddWithValue("Action", XR.Action)
            cmd.Parameters.AddWithValue("Result", XR.Result)
            cmd.Parameters.AddWithValue("UpdateUser", XR.User)
            Dim i As Integer = cmd.ExecuteNonQuery
            Return i
        End Using
    End Function

    Public Shared Function Delete(XR As clsQCSXRAction) As Integer
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Delete from QCSXRAction " & vbCrLf & _
                "where Date = @Date and LineID = @LineID and SubLineID = @SubLineID " & vbCrLf & _
                "and ProcessID = @ProcessID and PartID = @PartID and XRCode = @XRCode " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Date", Format(XR.ActionDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("LineID", XR.LineID)
            cmd.Parameters.AddWithValue("SubLineID", XR.SubLineID)
            cmd.Parameters.AddWithValue("ProcessID", XR.ProcessID)
            cmd.Parameters.AddWithValue("PartID", XR.PartID)
            cmd.Parameters.AddWithValue("XRCode", XR.XRCode & "")
            Dim i As Integer = cmd.ExecuteNonQuery
            Return i
        End Using
    End Function

    Public Shared Function GetData(ActionDate As Date, LineID As String, SubLineID As String, Process As String, PartID As String, XRCode As String) As clsQCSXRAction
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Select * from QCSXRAction " & vbCrLf & _
                "where Date = @Date and LineID = @LineID and SubLine = @SubLine " & vbCrLf & _
                "and Process = @Process and PartID = @PartID and XRCode = @XRCode " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Date", Format(ActionDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("LineID", LineID)
            cmd.Parameters.AddWithValue("SubLine", SubLineID)
            cmd.Parameters.AddWithValue("Process", Process)
            cmd.Parameters.AddWithValue("PartID", PartID)
            cmd.Parameters.AddWithValue("XRCode", XRCode)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return Nothing
            Else
                With dt.Rows(0)
                    Dim XR As New clsQCSXRAction
                    XR.ActionDate = ActionDate
                    XR.LineID = LineID
                    XR.SubLineID = SubLineID
                    XR.ProcessID = Process
                    XR.PartID = PartID
                    XR.XRCode = XRCode
                    XR.PIC = .Item("PIC") & ""
                    XR.Action = .Item("Action") & ""
                    XR.Result = .Item("Result") & ""
                    Return XR
                End With
            End If
        End Using
    End Function

    Public Shared Function GetExistData(ActionDate As Date, LineID As String, SubLineID As String, Process As String, PartID As String, XRCode As String) As Boolean
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Select * from QCSXRAction " & vbCrLf & _
                "where Date = @Date and LineID = @LineID and SubLineID = @SubLine " & vbCrLf & _
                "and ProcessID = @Process and PartID = @PartID and XRCode = @XRCode " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("Date", Format(ActionDate, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("LineID", LineID)
            cmd.Parameters.AddWithValue("SubLine", SubLineID)
            cmd.Parameters.AddWithValue("Process", Process)
            cmd.Parameters.AddWithValue("PartID", PartID)
            cmd.Parameters.AddWithValue("XRCode", XRCode)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            If dt.Rows.Count = 0 Then
                Return False
            Else
                Return True
            End If
        End Using
    End Function

    Public Shared Function GetTable(DateFrom As Date, DateTo As Date, LineID As String, SubLineID As String, Process As String, PartID As String, XRCode As String) As DataTable
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Dim q As String = "Select * from QCSXRAction " & vbCrLf
            q = q & _
                "where Date between @DateFrom and @DateTo " & vbCrLf & _
                "and LineID = @LineID and SubLineID = @SubLineID " & vbCrLf & _
                "and ProcessID = @ProcessID and PartID = @PartID and XRCode = @XRCode " & vbCrLf
            Dim cmd As New SqlCommand(q, Cn)
            cmd.Parameters.AddWithValue("DateFrom", Format(DateFrom, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("DateTo", Format(DateTo, "yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("LineID", LineID & "")
            cmd.Parameters.AddWithValue("SubLineID", SubLineID & "")
            cmd.Parameters.AddWithValue("ProcessID", Process & "")
            cmd.Parameters.AddWithValue("PartID", PartID & "")
            cmd.Parameters.AddWithValue("XRCode", XRCode & "")
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Return dt
        End Using
    End Function
End Class
