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
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
    Dim dataCount As String = ""
    Private dt As DataTable
#End Region

#Region "Events"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
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
                End With

                up_GridLoad(cls)
            End If

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Sub Grid_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles Grid.HtmlDataCellPrepared
        If (e.DataColumn.FieldName <> "Type") Then
            If e.DataColumn.FieldName = "DataCount" Then
                dataCount = e.CellValue
            Else
                If e.CellValue = "NG" Then
                    e.Cell.BackColor = Color.Red
                    e.Cell.ForeColor = Color.White
                ElseIf e.CellValue = "" Then
                    If dataCount = "1" Then
                        e.Cell.BackColor = Color.Yellow
                    Else
                        e.Cell.BackColor = Color.Gray
                        e.Cell.BorderColor = Color.Gray
                    End If
                End If
            End If
        End If
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
    End Sub

    Private Sub up_FillcomboType()
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
    End Sub

    Private Sub up_FillcomboMachine()
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
    End Sub

    Private Sub up_FillcomboFrequency()
        Dim a As String = ""
        dt = clsProdSampleQCSummaryDB.FillCombo("3", HF.Get("FactoryCode"), HF.Get("TypeCode"), HF.Get("MachineCode"))
        With cboFrequency
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
        HF.Set("FrequencyCode", a)
    End Sub

    Private Sub up_FillcomboSequence()
        Dim a As String = ""
        dt = clsProdSampleQCSummaryDB.FillCombo("4", HF.Get("FactoryCode"), HF.Get("FrequencyCode"), HF.Get("TypeCode"), HF.Get("MachineCode"))
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

            Dim Col1 As New GridViewDataTextColumn
            Col1.FieldName = "DataCount"
            Col1.Caption = "DataCount"
            Col1.Width = 0

            Col1.FixedStyle = GridViewColumnFixedStyle.Left
            Col1.HeaderStyle.Wrap = DefaultBoolean.True
            Col1.HeaderStyle.VerticalAlign = VerticalAlign.Middle
            Col1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
            Col1.CellStyle.VerticalAlign = VerticalAlign.Middle
            Col1.CellStyle.HorizontalAlign = HorizontalAlign.Center
            .Columns.Add(Col1)

            ds = clsProdSampleQCSummaryDB.GetList(cls)
            dt = ds.Tables(0)

            If dt.Rows.Count > 0 Then
                For i = 1 To dt.Columns.Count - 1
                    OK = OK + dt.Select("[" + dt.Columns(i).ColumnName + "] = 'OK'").Length
                    NG = NG + dt.Select("[" + dt.Columns(i).ColumnName + "] = 'NG'").Length
                    no = no + dt.Select("[" + dt.Columns(i).ColumnName + "] = ''").Length

                    Dim Col As New GridViewDataTextColumn
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
                dt.Columns.Add("DataCount")

                For i = 0 To dt.Rows.Count - 1
                    For kol = 1 To dt.Columns.Count - 1
                        If dt.Columns(kol).ColumnName <> "DataCount" Then
                            If dt.Rows(i)(kol).ToString() = "" And dt.Rows(i)("DataCount").ToString() = "" Then
                                dt.Rows(i)("DataCount") = ""
                            Else
                                dt.Rows(i)("DataCount") = "1"
                            End If
                        End If
                    Next
                Next


                If ds.Tables(1).Rows.Count = 0 Then
                    sampletime = ""
                Else
                    sampletime = ds.Tables(1).Rows(0)(0)
                End If

                .JSProperties("cp_header") = "Yes"
                .JSProperties("cp_sampletime") = sampletime
                .JSProperties("cp_ok") = OK
                .JSProperties("cp_ng") = NG
                .JSProperties("cp_no") = no
                .JSProperties("cp_total") = dt.Rows.Count * (dt.Columns.Count - 2)
            Else
                .JSProperties("cp_header") = "No"
            End If
            .DataSource = dt
            .DataBind()

        End With
    End Sub

#End Region

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
            End With
            dt = clsProdSampleQCSummaryDB.GetList(cls).Tables(0)

            If dt.Rows.Count = 0 Then
                show_error(MsgTypeEnum.Warning, "Data is Not Found", 1)
                Return
            End If

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
                    .Cells(5, 4).Style.Font.Color.SetColor(System.Drawing.Color.White)

                    '----------------Moving Rows--------------------'

                    .Cells(6, 3).Value = "Incomplete"
                    .Cells(6, 3).Style.Font.Size = 12

                    .Cells(6, 4).Value = ": " + HF.Get("NOK").ToString()
                    .Cells(6, 4).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    .Cells(6, 4).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                    .Cells(6, 4).Style.Font.Size = 12
                    .Cells(6, 4).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    .Cells(6, 4).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gray)
                    .Cells(6, 4).Style.Font.Color.SetColor(System.Drawing.Color.White)

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
                    For i = 0 To dt.Rows.Count - 1
                        dataCount = ""
                        For j = 0 To lastCol
                            .Cells(rowsExcel, j + 1).Value = dt.Rows(i)(j).ToString()
                            .Cells(rowsExcel, j + 1).Style.Font.Size = 10
                            .Cells(rowsExcel, j + 1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                            .Cells(rowsExcel, j + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                            If j = 0 Then
                                .Column(j + 1).Width = 13
                            ElseIf j <> 0 Then
                                .Column(j + 1).Width = 17
                                '.Cells(1, 1, 3, lastCol).Style.WrapText = True
                                If dt.Rows(i)(j).ToString() = "NG" Or dt.Rows(i)(j).ToString() = "OK" Then dataCount = "1"
                                If dt.Rows(i)(j).ToString() = "NG" Then
                                    .Cells(rowsExcel, j + 1, rowsExcel, j + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                                    .Cells(rowsExcel, j + 1, rowsExcel, j + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red)
                                    .Cells(rowsExcel, j + 1, rowsExcel, j + 1).Style.Font.Color.SetColor(System.Drawing.Color.White)
                                ElseIf dt.Rows(i)(j).ToString() = "" Then
                                    .Cells(rowsExcel, j + 1, rowsExcel, j + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                                    .Cells(rowsExcel, j + 1, rowsExcel, j + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow)
                                End If
                            End If
                        Next
                        If dataCount = "" Then
                            .Cells(rowsExcel, 2, rowsExcel, lastCol + 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                            .Cells(rowsExcel, 2, rowsExcel, lastCol + 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gray)
                        End If
                        rowsExcel += 1
                    Next

                    .View.FreezePanes(10, 2)
                    .Cells(9, 2, 9, lastCol + 1).Style.WrapText = True
                    .Cells(9, 1, rowsExcel - 1, lastCol + 1).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                    .Cells(9, 1, rowsExcel - 1, lastCol + 1).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                    .Cells(9, 1, rowsExcel - 1, lastCol + 1).Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                    .Cells(9, 1, rowsExcel - 1, lastCol + 1).Style.Border.Left.Style = Style.ExcelBorderStyle.Thin


                    'Dim iDay As Integer = 2
                    'Dim iCol As Integer = 2

                    'Dim Seq As Integer = 0
                    'Dim SelDay As Date = dtFrom.Value
                    '.Cells(1, 1).Value = "Date"
                    '.Cells(2, 1).Value = "Shift"
                    '.Cells(3, 1).Value = "Sequence"
                    'Do Until iCol >= MaxCount
                    '    If dt0.Rows(0)(iDay) & "" = "" Then
                    '        Exit Do
                    '    End If
                    '    .Cells(1, iCol, 1, iCol + 2).Style.Numberformat.Format = "dd/MM/yy"
                    '    .Cells(1, iCol, 1, iCol + 2).Value = dt0.Rows(0)(iDay)
                    '    .Cells(1, iCol, 1, iCol + 2).Merge = True
                    '    .Cells(2, iCol).Value = 1
                    '    .Cells(2, iCol + 1).Value = 2
                    '    .Cells(2, iCol + 2).Value = 3
                    '    If Not IsDBNull(dtShift.Rows(0)(iCol - 2)) Then
                    '        Seq = Seq + 1
                    '        .Cells(3, iCol).Value = Seq
                    '        lastCol = iCol
                    '    End If
                    '    If Not IsDBNull(dtShift.Rows(0)(iCol - 1)) Then
                    '        Seq = Seq + 1
                    '        .Cells(3, iCol + 1).Value = Seq
                    '        lastCol = iCol + 1
                    '    End If
                    '    If Not IsDBNull(dtShift.Rows(0)(iCol)) Then
                    '        Seq = Seq + 1
                    '        .Cells(3, iCol + 2).Value = Seq
                    '        lastCol = iCol + 2
                    '    End If
                    '    iCol = iCol + 3
                    '    iDay = iDay + 1
                    'Loop
                    'iCol = iCol + 1
                    '.Cells(1, 1, 11, iCol).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                    '.Cells(1, 1, 11, iCol).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                    '.Cells(1, 1, 11, iCol).Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                    '.Cells(1, 1, 11, iCol).Style.Border.Left.Style = Style.ExcelBorderStyle.Thin

                    'Dim nCol As Integer = iCol

                    'Dim dt1 As DataTable = ds.Tables(1)
                    'For i = 0 To dt1.Rows.Count - 1
                    '    For iCol = 1 To MaxCount
                    '        .Cells(i + 4, iCol).Value = dt1.Rows(i)(iCol)
                    '    Next
                    'Next

                    'Dim n As Integer = dt1.Rows.Count + 3
                    'Dim dt5 As DataTable = ds.Tables(5)
                    'For iCol = 1 To MaxCount
                    '    .Cells(n, iCol).Value = dt5.Rows(0)(iCol)
                    'Next
                    'For iCol = MaxCount + 3 To 2 Step -1
                    '    If .Cells(n, iCol).Value <> "5" Then
                    '        .DeleteColumn(iCol)
                    '    End If
                    'Next
                    'For iCol = 1 To MaxCount + 2
                    '    If .Cells(n, iCol).Value & "" <> "" Then
                    '        lastCol = iCol
                    '    End If
                    'Next
                    '.Cells(1, 1, 3, lastCol).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    '.Cells(1, 1, 3, lastCol).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                    '.Cells(1, 1, 3, lastCol).Style.Font.Color.SetColor(System.Drawing.Color.White)
                    '.Cells(1, 1, 3, lastCol).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    '.Cells(1, 1, 3, lastCol).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    '.Cells(1, 1, 3, lastCol).Style.WrapText = True

                    '.Cells(n, 1, n, MaxCount + 3).Clear()

                    'n = dt1.Rows.Count
                    '.Cells(n, 1, n, lastCol).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    '.Cells(n, 1, n, lastCol).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)

                    '.Cells(n + 1, 1, n + 2, 1).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    '.Cells(n + 1, 1, n + 2, 1).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)

                    '.Cells(13, 1).Value = "SUMMARY"
                    '.Cells(14, 1).Value = "Line"
                    '.Cells(14, 2).Value = "Part No."
                    '.Cells(14, 4).Value = "Part Name"
                    '.Cells(14, 6).Value = "Item Check"
                    '.Cells(14, 8).Value = "Standard"
                    '.Cells(14, 10).Value = "Frequency"
                    '.Cells(14, 12).Value = "Measuring Instrument"
                    '.Cells(14, 14).Value = "Machine No."
                    '.Cells(14, 15).Value = "CP"
                    '.Cells(14, 16).Value = "CPK"
                    '.Cells(14, 2, 14, 3).Merge = True
                    '.Cells(14, 4, 14, 5).Merge = True
                    '.Cells(14, 6, 14, 7).Merge = True
                    '.Cells(14, 8, 14, 9).Merge = True
                    '.Cells(14, 10, 14, 11).Merge = True
                    '.Cells(14, 12, 14, 13).Merge = True
                    '.Cells(14, 1, 15, 16).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                    '.Cells(14, 1, 15, 16).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                    '.Cells(14, 1, 15, 16).Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                    '.Cells(14, 1, 15, 16).Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                    '.Cells(14, 1, 14, 16).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    '.Cells(14, 1, 14, 16).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                    '.Cells(14, 1, 14, 16).Style.Font.Color.SetColor(System.Drawing.Color.White)
                    '.Cells(14, 1, 14, 16).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    '.Cells(14, 1, 14, 16).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    '.Cells(14, 1, 14, 16).Style.WrapText = True
                    'Dim StartRow As Integer = 15
                    'Dim dtSum As DataTable = clsXRChartDB.GetSummary(dtFrom.Value, dtTo.Value, cboLineID.Value, cboSubLine.Value, cboPartID.Value, cboMachine.Value, cboItem.Value, XUCL, XLCL)
                    'For i = 0 To dtSum.Rows.Count - 1
                    '    Dim j As Integer = StartRow + i
                    '    .Cells(j, 1).Value = dtSum.Rows(i)("LineID")
                    '    .Cells(j, 2).Value = dtSum.Rows(i)("PartID")
                    '    .Cells(j, 4).Value = dtSum.Rows(i)("PartName")
                    '    .Cells(j, 6).Value = dtSum.Rows(i)("Item")
                    '    .Cells(j, 8).Value = dtSum.Rows(i)("Standard")
                    '    .Cells(j, 10).Value = dtSum.Rows(i)("Frequency")
                    '    .Cells(j, 12).Value = dtSum.Rows(i)("MeasuringInstrument")
                    '    .Cells(j, 14).Value = dtSum.Rows(i)("MachineNo")
                    '    .Cells(j, 15).Value = dtSum.Rows(i)("CP")
                    '    .Cells(j, 16).Value = dtSum.Rows(i)("CPK")

                    '    .Cells(j, 2, j, 3).Merge = True
                    '    .Cells(j, 4, j, 5).Merge = True
                    '    .Cells(j, 6, j, 7).Merge = True
                    '    .Cells(j, 8, j, 9).Merge = True
                    '    .Cells(j, 10, j, 11).Merge = True
                    '    .Cells(j, 12, j, 13).Merge = True
                    'Next

                    '.Cells(17, 1).Value = "COMMENT"
                    '.Cells(18, 1).Value = "Date"
                    '.Cells(18, 3).Value = "PIC"
                    '.Cells(18, 5).Value = "Action"
                    '.Cells(18, 7).Value = "Result"
                    '.Cells(18, 1, 18, 2).Merge = True
                    '.Cells(18, 3, 18, 4).Merge = True
                    '.Cells(18, 5, 18, 6).Merge = True
                    '.Cells(18, 7, 18, 8).Merge = True

                    'Dim dtAct As DataTable = clsQCSXRActionDB.GetTable(dtFrom.Value, dtTo.Value, cboLineID.Value, cboSubLine.Value, txtProcessID.Text, cboPartID.Value, txtXRCode.Text)
                    '.Cells(18, 1, 18 + dtAct.Rows.Count, 8).Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                    '.Cells(18, 1, 18 + dtAct.Rows.Count, 8).Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                    '.Cells(18, 1, 18 + dtAct.Rows.Count, 8).Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
                    '.Cells(18, 1, 18 + dtAct.Rows.Count, 8).Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
                    '.Cells(18, 1, 18, 8).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                    '.Cells(18, 1, 18, 8).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                    '.Cells(18, 1, 18, 8).Style.Font.Color.SetColor(System.Drawing.Color.White)
                    '.Cells(18, 1, 18, 8).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    '.Cells(18, 1, 18, 8).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
                    'StartRow = 19
                    '.Cells(18, 1, 18 + dtAct.Rows.Count, 1).Style.Numberformat.Format = "dd MMM yyyy"
                    '.Cells(18, 1, 18 + dtAct.Rows.Count, 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    'For i = 0 To dtAct.Rows.Count - 1
                    '    Dim j As Integer = StartRow + i
                    '    .Cells(j, 1).Value = dtAct.Rows(i)("Date")
                    '    .Cells(j, 3).Value = dtAct.Rows(i)("PIC") & ""
                    '    .Cells(j, 5).Value = dtAct.Rows(i)("Action") & ""
                    '    .Cells(j, 7).Value = dtAct.Rows(i)("Result") & ""
                    '    .Cells(j, 1, j, 2).Merge = True
                    '    .Cells(j, 3, j, 4).Merge = True
                    '    .Cells(j, 5, j, 6).Merge = True
                    '    .Cells(j, 7, j, 8).Merge = True
                    'Next
                    '.InsertRow(1, 7)
                    '.Cells(3, 1).Value = "Date"
                    '.Cells(4, 1).Value = "Machine No."
                    '.Cells(5, 1).Value = "Line No."
                    '.Cells(6, 1).Value = "Sub Line No."
                    '.Cells(3, 3).Value = ": " & Format(dtFrom.Value, "dd MMM yyyy") & " to " & Format(dtTo.Value, "dd MMM yyyy")
                    '.Cells(4, 3).Value = ": " & cboMachine.Text & " " & txtProcess.Text
                    '.Cells(5, 3).Value = ": " & cboLineID.Text
                    '.Cells(6, 3).Value = ": " & cboSubLine.Text

                    '.Cells(3, 8).Value = "Part No."
                    '.Cells(4, 8).Value = "Part Name"
                    '.Cells(5, 8).Value = "Item Check"
                    '.Cells(3, 10).Value = ": " & cboPartID.Text
                    '.Cells(4, 10).Value = ": " & txtPartName.Text
                    '.Cells(5, 10).Value = ": " & cboItem.Text

                    '.Cells(1, 1).Style.Font.Bold = True
                    '.Cells(1, 1).Style.Font.Size = 16
                    '.Cells(1, 1).Value = "SQC XR Chart"
                    '.Cells(1, 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    '.Cells(1, 1, 1, 12).Merge = True
                    'Dim FinalRow As Integer = .Dimension.End.Row
                    'Dim ColumnString As String = "A2:M" & FinalRow
                    '.Cells(ColumnString).Style.Font.Name = "Segoe UI"
                    '.Cells(ColumnString).Style.Font.Size = 10

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

                'Dim stream As MemoryStream = New MemoryStream(excel.GetAsByteArray())
                'Response.AppendHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                'Response.AppendHeader("Content-Disposition", "attachment; filename=Sample Control Quality Summary_" & Format(Date.Now, "yyyy-MM-dd_HHmmss") & ".xlsx")
                'Response.BinaryWrite(stream.ToArray())
                'Response.End()

            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        DownloadExcel()
    End Sub
End Class