Imports System.Net
Imports System.IO

Public Class PreviewPDF
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = "Preview PDF"
        If Not IsPostBack Then
            Dim UserID As String = Trim(Session("user") & "")
            Dim GlobalPrm As String = ""
            Dim pdfPath As String = ""

            If Request.QueryString("type") Is Nothing Then
                Exit Sub
            End If
            Dim Type As String = Request.QueryString("type") & ""
            If Type = "TCCS" Then
                Dim TCCSResultID As String = Request.QueryString("id") & ""
                Dim ItemID As String = Request.QueryString("itemid") & ""
                Dim FileName As String = Request.QueryString("name") & ""
                Dim MachineNo As String = Request.QueryString("machine") & ""
                Dim LineID As String = Request.QueryString("line") & ""
                Dim PartNo As String = Request.QueryString("part") & ""
                Dim ResultDate As String = Request.QueryString("date") & ""

                Dim TCCSPath As String = clsSetting.TCCSPath
                If TCCSPath = "" Then
                    pdfPath = Server.MapPath("~/TCCS/" & Format(CDate(ResultDate), "yyyyMMdd") & "/" & LineID & "/" & MachineNo & "/" & PartNo & "/" & TCCSResultID & "/" & ItemID + "/" + FileName)
                Else
                    pdfPath = TCCSPath & "\TCCS\" & Format(CDate(ResultDate), "yyyyMMdd") & "\" & LineID & "\" & MachineNo & "\" & PartNo & "\" & TCCSResultID & "\" & ItemID + "\" + FileName
                End If
            Else
                Dim LineID As String = Split(Type, "|")(1)
                Dim PartNo As String = Split(Type, "|")(2)
                Dim Shift As String = Split(Type, "|")(3)
                Dim FileName As String = Split(Type, "|")(4)
                Dim ResultDate As String = Split(Type, "|")(5)

                Dim QCSPath As String = clsSetting.QCSPath
                If QCSPath = "" Then
                    pdfPath = Server.MapPath("~/QCS/" & Format(CDate(ResultDate), "yyyyMMdd") & "/" & LineID & "/" & PartNo & "/" & Shift & "/" & FileName)
                Else
                    pdfPath = QCSPath & "\QCS\" & Format(CDate(ResultDate), "yyyyMMdd") & "\" & LineID & "\" & PartNo & "\" & Shift & "\" & FileName
                End If                
            End If
            
            Dim client As WebClient = New WebClient()
            Try
                If File.Exists(pdfPath) Then
                    Dim buffer As Byte() = client.DownloadData(pdfPath)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-length", buffer.Length.ToString())
                    Response.BinaryWrite(buffer)
                    lblErr.Text = " "
                Else
                    lblErr.Text = "File Not Found"
                End If
            Catch ex As Exception
                lblErr.Text = ex.Message
                lblErr.ForeColor = Drawing.Color.Red
            End Try
        End If
    End Sub

End Class