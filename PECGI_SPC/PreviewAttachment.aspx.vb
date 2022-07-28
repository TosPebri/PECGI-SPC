Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO

Public Class PreviewAttachment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim UserID As String = Trim(Session("user") & "")
        Dim GlobalPrm As String = ""
        If Request.QueryString("type") Is Nothing Then
            Exit Sub
        End If
        Dim Type As String = Request.QueryString("type").ToString()
        
        Dim ImgPath As String
        If Type = "TCCS" Then
            Dim ResultID As String = Request.QueryString("id").ToString()
            Dim ItemID As String = Request.QueryString("itemid").ToString()
            Dim FileName As String = Request.QueryString("name").ToString()
            Dim LineID As String = Request.QueryString("line") & ""
            Dim MachineNo As String = Request.QueryString("machine") & ""
            Dim PartNo As String = Request.QueryString("part") & ""
            Dim ResultDate As String = Request.QueryString("date") & ""
            txtResultID.Text = ResultID
            txtItemID.Text = ItemID
            txtName.Text = FileName
            Dim TCCSPath As String = clsSetting.TCCSPath
            If TCCSPath = "" Then
                ImgPath = Server.MapPath("~/TCCS/" & Format(CDate(ResultDate), "yyyyMMdd") & "/" & LineID & "/" & MachineNo & "/" & PartNo & "/" & ResultID & "/" & ItemID + "/" & FileName)
            Else
                ImgPath = TCCSPath & "\TCCS\" & Format(CDate(ResultDate), "yyyyMMdd") & "\" & LineID & "\" & MachineNo & "\" & PartNo & "\" & ResultID & "\" & ItemID + "\" & FileName
            End If
        Else            
            Dim LineID As String = Split(Type, "|")(1)
            Dim PartNo As String = Split(Type, "|")(2)
            Dim Shift As String = Split(Type, "|")(3)
            Dim FileName As String = Split(Type, "|")(4)
            Dim ResultDate As String = Split(Type, "|")(5)
            Dim QCSpath As String = clsSetting.QCSPath
            If QCSpath = "" Then
                ImgPath = Server.MapPath("~/QCS/" & Format(CDate(ResultDate), "yyyyMMdd") & "/" & LineID & "/" & PartNo & "/" & Shift & "/" & FileName)
            Else
                ImgPath = QCSpath & "\QCS\" & Format(CDate(ResultDate), "yyyyMMdd") & "\" & LineID & "\" & PartNo & "\" & Shift & "\" & FileName
            End If
        End If
        If System.IO.File.Exists(ImgPath) Then
            Dim byteImage As Byte() = File.ReadAllBytes(ImgPath)
            img2.ContentBytes = byteImage
            lblErr.Text = ""
        Else
            lblErr.Text = "File Not Found"
        End If
    End Sub

    Protected Sub DeleteAttachment(sender As Object, e As EventArgs)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Using Tr As SqlTransaction = Cn.BeginTransaction
                clsTCCSResultItemDB.UpdateAttachment(txtResultID.Text, txtItemID.Text, "", Cn, Tr)
                Tr.Commit()
            End Using
        End Using
    End Sub

    Protected Sub cbkClear_Callback(source As Object, e As DevExpress.Web.CallbackEventArgs) Handles cbkClear.Callback
        Dim ResultID As String = Split(e.Parameter, "|")(0)
        Dim ItemID As String = Split(e.Parameter, "|")(1)
        Using Cn As New SqlConnection(Sconn.Stringkoneksi)
            Cn.Open()
            Using Tr As SqlTransaction = Cn.BeginTransaction
                clsTCCSResultItemDB.UpdateAttachment(ResultID, ItemID, "", Cn, Tr)
                Tr.Commit()
            End Using
        End Using
    End Sub
End Class