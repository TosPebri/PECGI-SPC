Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Drawing
Imports OfficeOpenXml

Public Class ProdSampleQCSummary
    Inherits System.Web.UI.Page

#Region "Declarations"
    Dim pUser As String = ""
    Dim pMenuID As String = "B050"
    Public AuthAccess As Boolean = False
    Private dt As DataTable
#End Region

#Region "Events"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            HF.Set("Excel", "0")
            up_Fillcombo()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sGlobal.getMenu(pMenuID)
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, pMenuID)
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If
    End Sub

    Private Sub cboMachine_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboMachine.Callback
        Try
            up_FillcomboMachine()
        Catch ex As Exception
            show_error(MsgTypeEnum.Info, "Machine Callback Error " & ex.Message, 1)
        End Try
    End Sub

    Private Sub cboFrequency_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboFrequency.Callback
        Try
            up_FillcomboFrequency()
        Catch ex As Exception
            show_error(MsgTypeEnum.Info, "Frequency Callback Error " & ex.Message, 1)
        End Try
    End Sub

    Private Sub cboSequence_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboSequence.Callback
        Try
            up_FillcomboSequence()
        Catch ex As Exception
            show_error(MsgTypeEnum.Info, "Sequence Callback Error " & ex.Message, 1)
        End Try
    End Sub

    Protected Sub Grid_AfterPerformCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles Grid.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            Dim cls As New clsProdSampleQCSummary
            Dim dTime As DateTime = dtPeriod.Value
            With cls
                .FactoryCode = HF.Get("FactoryCode")
                .ItemTypeCode = HF.Get("TypeCode")
                .MachineCode = HF.Get("MachineCode")
                .Frequency = HF.Get("FrequencyCode")
                .Sequence = cboSequence.Value
                .Period = dTime.ToString("yyyy-MM-dd")
            End With
            up_GridLoad(cls)
        End If
    End Sub

    Private Sub Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Grid.CustomCallback
        Try
            Dim pAction As String = Split(e.Parameters, "|")(0)
            Dim cls As New clsProdSampleQCSummary
            Dim dTime As DateTime = dtPeriod.Value

            If pAction = "Load" Then
                With cls
                    .FactoryCode = HF.Get("FactoryCode")
                    .ItemTypeCode = HF.Get("TypeCode")
                    .MachineCode = HF.Get("MachineCode")
                    .Frequency = HF.Get("FrequencyCode")
                    .Sequence = cboSequence.Value
                    .Period = dTime.ToString("yyyy-MM-dd")
                    .UserID = pUser
                End With

                up_GridLoad(cls)
            ElseIf pAction = "Kosong" Then
                With cls
                    .FactoryCode = ""
                    .ItemTypeCode = ""
                    .MachineCode = ""
                    .Frequency = ""
                    .Sequence = "0"
                    .Period = dTime.ToString("yyyy-MM-dd")
                    .UserID = pUser
                End With

                up_GridLoad(cls)
            End If

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Sub Grid_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles Grid.HtmlDataCellPrepared
        Dim Link As New HyperLink()
        If (e.DataColumn.FieldName <> "Type") Then
            If e.CellValue.ToString.Contains("NG") Then
                If Split(e.CellValue, "||").Count = 3 Then
                    e.Cell.Text = ""

                    Link.ForeColor = Color.Black
                    Link.Text = "NG"
                    Link.NavigateUrl = Split(e.CellValue, "||")(2)

                    e.Cell.Controls.Add(Link)
                    e.Cell.BackColor = ColorTranslator.FromHtml(Split(e.CellValue, "||")(1))
                End If
            ElseIf e.CellValue.ToString.Contains("NoProd") Or e.CellValue.ToString.Contains("NoResult") Then
                If Split(e.CellValue, "||").Count = 2 Then
                    e.Cell.Text = ""
                    e.Cell.BackColor = ColorTranslator.FromHtml(Split(e.CellValue, "||")(1))
                    If Split(e.CellValue, "||")(1) = "#515151" Then e.Cell.BorderColor = ColorTranslator.FromHtml("#515151")
                End If
            ElseIf e.CellValue.ToString.Contains("NOK") Then
                e.Cell.Text = ""
            Else
                e.Cell.Text = ""

                Link.ForeColor = Color.Black
                Link.Text = "OK"
                Link.NavigateUrl = Split(e.CellValue, "||")(1)

                e.Cell.Controls.Add(Link)
            End If
        End If
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        DownloadExcel()
    End Sub
#End Region

#Region "Functions"
    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        Grid.JSProperties("cp_message") = ErrMsg
        Grid.JSProperties("cp_type") = msgType
        Grid.JSProperties("cp_val") = pVal
    End Sub

    Private Sub up_Fillcombo()
        Try
            up_FillcomboFactory()
            up_FillcomboType()
            up_FillcomboMachine()
            up_FillcomboFrequency()
            up_FillcomboSequence()
        Catch ex As Exception
            show_error(MsgTypeEnum.Info, "", 1)
        End Try
    End Sub

    Private Sub up_FillcomboFactory()
        If HF.Get("Excel") = "0" Then
            Dim a As String = ""
            dt = clsProdSampleQCSummaryDB.FillCombo("0")
            With cboFactory
                .Items.Clear() : .Columns.Clear()
                .DataSource = dt
                .Columns.Add("Code") : .Columns(0).Visible = False
                .Columns.Add("Description") : .Columns(1).Width = 100

                .TextField = "Description"
                .ValueField = "Code"
                .DataBind()
                .SelectedIndex = -1 'IIf(dt.Rows.Count > 0, 0, -1)

                If .SelectedIndex < 0 Then
                    a = ""
                Else
                    a = .SelectedItem.GetFieldValue("Code")
                End If
            End With
            HF.Set("FactoryCode", a)
        End If
    End Sub

    Private Sub up_FillcomboType()
        If HF.Get("Excel") = "0" Then
            Dim a As String = ""
            dt = clsProdSampleQCSummaryDB.FillCombo("1")
            With cboType
                .Items.Clear() : .Columns.Clear()
                .DataSource = dt
                .Columns.Add("Code") : .Columns(0).Visible = False
                .Columns.Add("Description") : .Columns(1).Width = 100

                .TextField = "Description"
                .ValueField = "Code"
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)

                If .SelectedIndex < 0 Then
                    a = ""
                Else
                    a = .SelectedItem.GetFieldValue("Code")
                End If
            End With
            HF.Set("TypeCode", a)
        End If
    End Sub

    Private Sub up_FillcomboMachine()
        If HF.Get("Excel") = "0" Then
            Dim a As String = ""
            dt = clsProdSampleQCSummaryDB.FillCombo("2", HF.Get("FactoryCode"), pUser)
            With cboMachine
                .Items.Clear() : .Columns.Clear()
                .DataSource = dt
                .Columns.Add("Code") : .Columns(0).Visible = False
                .Columns.Add("Description") : .Columns(1).Width = 100

                .TextField = "Description"
                .ValueField = "Code"
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)

                If .SelectedIndex < 0 Then
                    a = ""
                Else
                    a = .SelectedItem.GetFieldValue("Code")
                End If
            End With
            HF.Set("MachineCode", a)
        End If
    End Sub

    Private Sub up_FillcomboFrequency()
        If HF.Get("Excel") = "0" Then
            Dim a As String = ""
            dt = clsProdSampleQCSummaryDB.FillCombo("3", HF.Get("FactoryCode"), HF.Get("TypeCode"), HF.Get("MachineCode"), pUser)
            With cboFrequency
                .Items.Clear() : .Columns.Clear()
                .DataSource = dt
                .Columns.Add("Code") : .Columns(0).Visible = False
                .Columns.Add("Description") : .Columns(1).Width = 100

                .TextField = "Description"
                .ValueField = "Code"
                .DataBind()
                .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)

                If .SelectedIndex < 0 Then
                    a = ""
                Else
                    a = .SelectedItem.GetFieldValue("Code")
                End If
            End With
            HF.Set("FrequencyCode", a)
        End If
    End Sub

    Private Sub up_FillcomboSequence()
        If HF.Get("Excel") = "0" Then
            dt = clsProdSampleQCSummaryDB.FillCombo("4", HF.Get("FactoryCode"), HF.Get("FrequencyCode"), HF.Get("TypeCode"), HF.Get("MachineCode"), pUser)
            With cboSequence
                .Items.Clear() : .Columns.Clear()
                .DataSource = dt
                .Columns.Add("Code") : .Columns(0).Visible = False
                .Columns.Add("Description") : .Columns(1).Width = 100

                .TextField = "Description"
                .ValueField = "Code"
                .DataBind()
                .SelectedIndex = -1 'IIf(dt.Rows.Count > 0, 0, -1)
            End With
        End If
    End Sub

    Private Sub up_GridLoad(cls As clsProdSampleQCSummary)
        Dim OK As Integer = 0, NG As Integer = 0, no As Integer = 0, sampletime As String = "", ds As New DataSet
        With Grid
            .Columns.Clear()

            Dim Col0 As New GridViewDataTextColumn
            Col0.FieldName = "Type"
            Col0.Caption = "Type"
            Col0.Width = 75

            Col0.FixedStyle = GridViewColumnFixedStyle.Left
            Col0.HeaderStyle.Wrap = DefaultBoolean.True
            Col0.HeaderStyle.VerticalAlign = VerticalAlign.Middle
            Col0.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            Col0.CellStyle.VerticalAlign = VerticalAlign.Middle
            Col0.CellStyle.HorizontalAlign = HorizontalAlign.Center
            .Columns.Add(Col0)

            ds = clsProdSampleQCSummaryDB.GetList(cls)
            dt = ds.Tables(0)

            If dt.Rows.Count > 0 Then
                For i = 1 To dt.Columns.Count - 1
                    OK = OK + dt.Select("[" + dt.Columns(i).ColumnName + "] = 'OK'").Length
                    NG = NG + dt.Select("[" + dt.Columns(i).ColumnName + "] Like '%NG%'").Length
                    no = no + dt.Select("[" + dt.Columns(i).ColumnName + "] Like '%NoResult%'").Length

                    Dim Col As New GridViewDataTextColumn
                    'Dim Col As New GridViewDataHyperLinkColumn()
                    'Col.PropertiesHyperLinkEdit.NavigateUrlFormatString = "~/details.aspx?Device={0}"
                    Col.FieldName = dt.Columns(i).ColumnName
                    Col.Caption = dt.Columns(i).ColumnName
                    Col.Width = 100

                    Col.HeaderStyle.Wrap = DefaultBoolean.True
                    Col.HeaderStyle.VerticalAlign = VerticalAlign.Middle
                    Col.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                    Col.CellStyle.VerticalAlign = VerticalAlign.Middle
                    Col.CellStyle.HorizontalAlign = HorizontalAlign.Center

                    .Columns.Add(Col)
                Next

                If ds.Tables(1).Rows.Count = 0 Then
                    sampletime = ""
                Else
                    sampletime = ds.Tables(1).Rows(0)(0)
                End If
                If cls.Frequency = "ALL" Then sampletime = "ALL"

                .JSProperties("cp_header") = "Yes"
                .JSProperties("cp_sampletime") = sampletime
                .JSProperties("cp_ok") = OK
                .JSProperties("cp_ng") = NG
                .JSProperties("cp_no") = no
                .JSProperties("cp_total") = dt.Rows.Count * (dt.Columns.Count - 1)
            Else
                .JSProperties("cp_header") = "No"
            End If
            .DataSource = dt
            .DataBind()

        End With
    End Sub

    Private Sub DownloadExcel()
        Try
            Dim cls As New clsProdSampleQCSummary
            Dim dTime As DateTime = dtPeriod.Value

            With cls
                .FactoryCode = HF.Get("FactoryCode")
                .ItemTypeCode = HF.Get("TypeCode")
                .MachineCode = HF.Get("MachineCode")
                .Frequency = HF.Get("FrequencyCode")
                .Sequence = cboSequence.Value
                .Period = dTime.ToString("yyyy-MM-dd")
                .UserID = pUser
            End With
            dt = clsProdSampleQCSummaryDB.GetList(cls).Tables(0)

            Using excel As New ExcelPackage
                Dim ws As ExcelWorksheet = excel.Workbook.Worksheets.Add("B05 - Sample Control Quality Summary")
                Dim rowsExcel As Integer = 1
                Dim lastCol As Integer = dt.Columns.Count - 1

                With ws
                    'Start Filter Title
                    .Cells(1, 1).Value = "Factory"
                    .Cells(1, 1).Style.Font.Size = 12
                    .Cells(1, 1).Style.Font.Bold = True

                    .Cells(1, 2).Value = ": " + cboFactory.Text
                    .Cells(1, 2).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    .Cells(1, 2).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    .Cells(1, 2).Style.Font.Size = 12
                    .Cells(1, 2).Style.Font.Bold = True

                    .Cells(1, 3).Value = "Machine Process"
                    .Cells(1, 3).Style.Font.Size = 12
                    .Cells(1, 3).Style.Font.Bold = True

                    .Cells(1, 4).Value = ": " + cboMachine.Text
                    .Cells(1, 4).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    .Cells(1, 4).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    .Cells(1, 4).Style.Font.Size = 12
                    .Cells(1, 4).Style.Font.Bold = True

                    .Cells(1, 5).Value = "Frequency"
                    .Cells(1, 5).Style.Font.Size = 12
                    .Cells(1, 5).Style.Font.Bold = True

                    .Cells(1, 6).Value = ": " + cboFrequency.Text
                    .Cells(1, 6).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    .Cells(1, 6).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    .Cells(1, 6).Style.Font.Size = 12
                    .Cells(1, 6).Style.Font.Bold = True

                    '----------------Moving Rows--------------------'

                    .Cells(2, 1).Value = "Type"
                    .Cells(2, 1).Style.Font.Size = 12
                    .Cells(2, 1).Style.Font.Bold = True

                    .Cells(2, 2).Value = ": " + cboType.Text
                    .Cells(2, 2).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    .Cells(2, 2).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    .Cells(2, 2).Style.Font.Size = 12
                    .Cells(2, 2).Style.Font.Bold = True

                    .Cells(2, 3).Value = "Period"
                    .Cells(2, 3).Style.Font.Size = 12
                    .Cells(2, 3).Style.Font.Bold = True

                    .Cells(2, 4).Value = ": " + dTime.ToString("dd MMMM yyyy")
                    .Cells(2, 4).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    .Cells(2, 4).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    .Cells(2, 4).Style.Font.Size = 12
                    .Cells(2, 4).Style.Font.Bold = True

                    .Cells(2, 5).Value = "Sequence"
                    .Cells(2, 5).Style.Font.Size = 12
                    .Cells(2, 5).Style.Font.Bold = True

                    .Cells(2, 6).Value = ": " + cboSequence.Text
                    .Cells(2, 6).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    .Cells(2, 6).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    .Cells(2, 6).Style.Font.Size = 12
                    .Cells(2, 6).Style.Font.Bold = True
                    .Cells(2, 6).Style.Numberformat.Format = "@"

                    'End Filter Title

                    'Start Info Title

                    .Cells(4, 1).Value = "Sample Time"
                    .Cells(4, 1).Style.Font.Size = 12

                    .Cells(4, 2).Value = ": " & HF.Get("sampletime")
                    .Cells(4, 2).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    .Cells(4, 2).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    .Cells(4, 2).Style.Font.Size = 12

                    .Cells(4, 3).Value = "OK Result"
                    .Cells(4, 3).Style.Font.Size = 12

                    .Cells(4, 4).Value = ": " + HF.Get("ok").ToString()
                    .Cells(4, 4).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    .Cells(4, 4).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    .Cells(4, 4).Style.Font.Size = 12

                    '----------------Moving Rows--------------------'

                    .Cells(5, 1).Value = "Total Sample"
                    .Cells(5, 1).Style.Font.Size = 12

                    .Cells(5, 2).Value = ": " + HF.Get("total").ToString()
                    .Cells(5, 2).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    .Cells(5, 2).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    .Cells(5, 2).Style.Font.Size = 12

                    .Cells(5, 3).Value = "NG Result"
                    .Cells(5, 3).Style.Font.Size = 12

                    .Cells(5, 4).Value = ": " + HF.Get("ng").ToString()
                    .Cells(5, 4).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    .Cells(5, 4).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    .Cells(5, 4).Style.Font.Size = 12
                    .Cells(5, 4).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    .Cells(5, 4).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)

                    '----------------Moving Rows--------------------'

                    .Cells(6, 3).Value = "Delay"
                    .Cells(6, 3).Style.Font.Size = 12

                    .Cells(6, 4).Value = ": " + HF.Get("NOK").ToString()
                    .Cells(6, 4).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    .Cells(6, 4).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    .Cells(6, 4).Style.Font.Size = 12
                    .Cells(6, 4).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    .Cells(6, 4).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)

                    'End Info Title
                    rowsExcel = 9
                    'Start Header FieldName

                    For i = 0 To lastCol
                        .Cells(rowsExcel, i + 1).Value = dt.Columns(i).ColumnName
                        .Cells(rowsExcel, i + 1).Style.Font.Bold = True
                        .Cells(rowsExcel, i + 1).Style.Font.Size = 12
                        .Cells(rowsExcel, i + 1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                        .Cells(rowsExcel, i + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    Next

                    rowsExcel += 1
                    If dt.Rows.Count > 1 Then
                        For i = 0 To dt.Rows.Count - 1
                            'dataCount = ""
                            For j = 0 To lastCol
                                .Cells(rowsExcel, j + 1).Value = dt.Rows(i)(j).ToString()
                                .Cells(rowsExcel, j + 1).Style.Font.Size = 10
                                .Cells(rowsExcel, j + 1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                                .Cells(rowsExcel, j + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                                If j = 0 Then
                                    .Column(j + 1).Width = 13
                                ElseIf j <> 0 Then
                                    .Column(j + 1).Width = 17
                                    If dt.Rows(i)(j).ToString.Contains("NG") Then
                                        .Cells(rowsExcel, j + 1).Value = "NG"
                                        If Split(dt.Rows(i)(j).ToString, "||").Count = 2 Then
                                            .Cells(rowsExcel, j + 1, rowsExcel, j + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                                            .Cells(rowsExcel, j + 1, rowsExcel, j + 1).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(Split(dt.Rows(i)(j).ToString, "||")(1)))
                                        End If
                                    ElseIf dt.Rows(i)(j).ToString.Contains("NoProd") Or dt.Rows(i)(j).ToString.Contains("NoResult") Then
                                        .Cells(rowsExcel, j + 1).Value = ""
                                        If Split(dt.Rows(i)(j).ToString, "||").Count = 2 Then
                                            .Cells(rowsExcel, j + 1, rowsExcel, j + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                                            .Cells(rowsExcel, j + 1, rowsExcel, j + 1).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(Split(dt.Rows(i)(j).ToString, "||")(1)))
                                        End If
                                    ElseIf dt.Rows(i)(j).ToString.Contains("NOK") Then
                                        .Cells(rowsExcel, j + 1).Value = ""
                                    Else
                                        .Cells(rowsExcel, j + 1).Value = "OK"
                                    End If
                                End If
                            Next
                            rowsExcel += 1
                        Next

                        .View.FreezePanes(10, 2)
                        .Cells(9, 2, 9, lastCol + 1).Style.WrapText = True
                        .Cells(9, 1, rowsExcel - 1, lastCol + 1).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                        .Cells(9, 1, rowsExcel - 1, lastCol + 1).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                        .Cells(9, 1, rowsExcel - 1, lastCol + 1).Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                        .Cells(9, 1, rowsExcel - 1, lastCol + 1).Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                    End If
                End With

                Response.Clear()
                Response.Buffer = True
                Response.Charset = ""
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=Sample Control Quality Summary_" & Format(Date.Now, "yyyy-MM-dd_HHmmss") & ".xlsx")
                Using MyMemoryStream As New MemoryStream()
                    excel.SaveAs(MyMemoryStream)
                    MyMemoryStream.WriteTo(Response.OutputStream)
                    Response.Flush()
                    Response.End()
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
#End Region
    
End Class