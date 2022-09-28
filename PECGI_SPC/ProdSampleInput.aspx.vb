Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DevExpress.XtraCharts
Imports DevExpress.XtraCharts.Web
Imports System.Drawing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrintingLinks
Imports DevExpress.XtraCharts.Native
Imports OfficeOpenXml
Imports System.IO
Imports DevExpress.Utils
Imports OfficeOpenXml.Style

Public Class ProdSampleInput
    Inherits System.Web.UI.Page
    Dim pUser As String = ""
    Public AuthApprove As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public ValueType As String
    Dim GlobalPrm As String = ""
    Dim row_GridTitle = 0
    Dim row_HeaderResult = 0
    Dim row_HeaderActivity = 0
    Dim row_CellResult = 0
    Dim row_CellChart = 0
    Dim row_CellActivity = 0
    Dim col_HeaderResult = 0
    Dim col_HeaderActivity = 0
    Dim col_CellResult = 0
    Dim col_CellActivity = 0
    Dim RowIndexName As String = ""
    Dim CharacteristicSts As String = ""


    Private Class clsHeader
        Public Property FactoryCode As String
        Public Property FactoryName As String
        Public Property ItemTypeCode As String
        Public Property ItemTypeName As String
        Public Property LineCode As String
        Public Property LineName As String
        Public Property ItemCheckCode As String
        Public Property ItemCheckName As String

        Public Property ProdDate As String
        Public Property ShiftCode As String
        Public Property Shiftname As String
        Public Property Seq As Integer
        Public Property VerifiedOnly As String
    End Class

    Private Sub GridXLoad(Hdr As clsHeader)
        With gridX
            .Columns.Clear()
            Dim Band1 As New GridViewBandColumn
            Band1.Caption = "DATE"
            Band1.HeaderStyle.Height = 90
            .Columns.Add(Band1)

            Dim Band2 As New GridViewBandColumn
            Band2.Caption = "SHIFT"
            Band1.HeaderStyle.Height = 40
            Band1.Columns.Add(Band2)

            Dim Col1 As New GridViewDataTextColumn
            Col1.FieldName = "Des"
            Col1.Caption = "TIME"
            Col1.Width = 90
            Col1.FixedStyle = GridViewColumnFixedStyle.Left
            Col1.CellStyle.HorizontalAlign = HorizontalAlign.Center
            Band2.Columns.Add(Col1)

            Dim SelDay As Object = clsSPCResultDB.GetPrevDate(Hdr.FactoryCode, Hdr.ItemTypeCode, Hdr.LineCode, Hdr.ItemCheckCode, Hdr.ProdDate)
            For iDay = 1 To 2
                If Not IsDBNull(SelDay) Then
                    Dim dDay As String = Format(CDate(SelDay), "yyyy-MM-dd")
                    Dim BandDay As New GridViewBandColumn
                    BandDay.Caption = Format(SelDay, "dd MMM yyyy")
                    .Columns.Add(BandDay)

                    Dim Shiftlist As List(Of clsShift) = clsFrequencyDB.GetShift(Hdr.FactoryCode, Hdr.ItemTypeCode, Hdr.LineCode, Hdr.ItemCheckCode, dDay)

                    For Each Shift In Shiftlist
                        Dim BandShift As New GridViewBandColumn
                        BandShift.Caption = "S-" & Shift.ShiftName
                        BandDay.Columns.Add(BandShift)

                        Dim SeqList As List(Of clsSequenceNo) = clsFrequencyDB.GetSequence(Hdr.FactoryCode, Hdr.ItemTypeCode, Hdr.LineCode, Hdr.ItemCheckCode, Shift.ShiftCode, dDay)
                        Dim ColIndex As Integer = 1
                        For Each Seq In SeqList
                            Dim colTime As New GridViewDataTextColumn
                            colTime.Caption = Seq.StartTime
                            colTime.FieldName = iDay.ToString + "_" + Shift.ShiftName.ToString + "_" + Seq.SequenceNo.ToString
                            colTime.Width = 90
                            colTime.CellStyle.HorizontalAlign = HorizontalAlign.Center

                            BandShift.Columns.Add(colTime)
                            ColIndex = ColIndex + 1
                        Next

                        Dim colLCL As New GridViewDataTextColumn
                        colLCL.Caption = "LCL"
                        colLCL.FieldName = "XBarLCL" & iDay.ToString
                        colLCL.Visible = False
                        BandShift.Columns.Add(colLCL)

                        Dim colUCL As New GridViewDataTextColumn
                        colUCL.Caption = "UCL"
                        colUCL.FieldName = "XBarUCL" & iDay.ToString
                        colUCL.Visible = False
                        BandShift.Columns.Add(colUCL)

                        Dim colLSL As New GridViewDataTextColumn
                        colLSL.Caption = "LSL"
                        colLSL.FieldName = "SpecLSL" & iDay.ToString
                        colLSL.Visible = False
                        BandShift.Columns.Add(colLSL)

                        Dim colUSL As New GridViewDataTextColumn
                        colUSL.Caption = "USL"
                        colUSL.FieldName = "SpecUSL" & iDay.ToString
                        colUSL.Visible = False
                        BandShift.Columns.Add(colUSL)

                    Next
                End If
                SelDay = CDate(Hdr.ProdDate)
            Next
            Dim dt As DataTable = clsSPCResultDetailDB.GetTableXR(Hdr.FactoryCode, Hdr.ItemTypeCode, Hdr.LineCode, Hdr.ItemCheckCode, Hdr.ProdDate, Hdr.VerifiedOnly)
            gridX.DataSource = dt
            gridX.DataBind()
        End With
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        grid.JSProperties("cp_message") = ErrMsg
        grid.JSProperties("cp_type") = msgType
        grid.JSProperties("cp_val") = pVal
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GlobalPrm = Request.QueryString("FactoryCode") & ""
        sGlobal.getMenu("B020 ")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user") & ""
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B020 ")
        grid.SettingsDataSecurity.AllowInsert = AuthUpdate
        grid.SettingsDataSecurity.AllowEdit = AuthUpdate
        show_error(MsgTypeEnum.Info, "", 0)
        If Not IsPostBack And Not IsCallback Then
            up_FillCombo()
            If GlobalPrm <> "" Then
                dtDate.Value = CDate(Request.QueryString("ProdDate"))
                Dim FactoryCode As String = Request.QueryString("FactoryCode")
                Dim ItemTypeCode As String = Request.QueryString("ItemTypeCode")
                Dim Line As String = Request.QueryString("Line")
                Dim ItemCheckCode As String = Request.QueryString("ItemCheckCode")
                Dim ProdDate As String = Request.QueryString("ProdDate")
                Dim Shift As String = Request.QueryString("Shift")
                Dim Sequence As String = Request.QueryString("Sequence")

                InitCombo(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate, Shift, Sequence)
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "GridLoad();", True)
                'GridLoad(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate, Shift, Sequence, 0)
            Else
                pUser = Session("user") & ""
                dtDate.Value = Now.Date
                If pUser <> "" Then
                    Dim User As clsUserSetup = clsUserSetupDB.GetData(pUser)
                    If User IsNot Nothing Then
                        cboFactory.Value = User.FactoryCode
                        cboType.DataSource = clsItemTypeDB.GetList(cboFactory.Value)
                        cboType.DataBind()
                    End If
                End If
                'InitCombo(User.FactoryCode, "TPMSBR011", "015", "IC021", "2022-08-04", "SH001", 1)
            End If
        End If
    End Sub

    Private Sub InitCombo(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String, ShiftCode As String, Sequence As String)
        pUser = Session("user") & ""
        dtDate.Value = CDate(ProdDate)
        cboFactory.Value = FactoryCode

        cboType.DataSource = clsItemTypeDB.GetList(cboFactory.Value)
        cboType.DataBind()
        cboType.Value = ItemTypeCode

        cboLine.DataSource = ClsLineDB.GetList(pUser, cboFactory.Value, cboType.Value)
        cboLine.DataBind()
        cboLine.Value = Line

        cboItemCheck.DataSource = clsItemCheckDB.GetList(cboFactory.Value, cboType.Value, cboLine.Value)
        cboItemCheck.DataBind()
        cboItemCheck.Value = ItemCheckCode

        cboShift.DataSource = clsFrequencyDB.GetShift(cboFactory.Value, cboType.Value, cboLine.Value, cboItemCheck.Value)
        cboShift.DataBind()
        cboShift.Value = ShiftCode

        cboSeq.DataSource = clsFrequencyDB.GetSequence(cboFactory.Value, cboType.Value, cboLine.Value, cboItemCheck.Value)
        cboSeq.DataBind()
        cboSeq.Value = Sequence
    End Sub

    Private Sub up_FillCombo()
        cboFactory.DataSource = clsFactoryDB.GetList
        cboFactory.DataBind()
    End Sub

    Protected Sub grid_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles grid.RowInserting
        e.Cancel = True
        Dim Result As New clsSPCResult
        Result.FactoryCode = cboFactory.Value
        Result.ItemCheckCode = cboItemCheck.Value
        Result.ItemTypeCode = cboType.Value
        Result.LineCode = cboLine.Value
        Result.ProdDate = dtDate.Value
        Result.ShiftCode = cboShift.Value
        Result.SequenceNo = cboSeq.Value
        Result.SubLotNo = 0
        Result.Remark = ""
        Result.RegisterUser = Session("user") & ""
        clsSPCResultDB.Insert(Result)

        Dim SeqNo As Integer = clsSPCResultDetailDB.GetSeqNo(Result.FactoryCode, Result.ItemTypeCode, Result.LineCode, Result.ItemCheckCode, Format(Result.ProdDate, "yyyy-MM-dd"), Result.ShiftCode, Result.SequenceNo)
        Dim Detail As New clsSPCResultDetail
        Detail.SPCResultID = Result.SPCResultID
        Detail.SequenceNo = SeqNo
        Detail.DeleteStatus = e.NewValues("DeleteStatus")
        Detail.Value = e.NewValues("Value")
        Detail.Remark = e.NewValues("Remark")
        Detail.RegisterUser = Result.RegisterUser
        clsSPCResultDetailDB.Insert(Detail)
        grid.CancelEdit()

        show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
    End Sub

    Protected Sub grid_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles grid.RowDeleting
        e.Cancel = True
    End Sub

    Private Sub up_ClearJS()
        grid.JSProperties("cpUSL") = " "
        grid.JSProperties("cpLSL") = " "
        grid.JSProperties("cpUCL") = " "
        grid.JSProperties("cpLCL") = " "
        grid.JSProperties("cpMin") = ""
        grid.JSProperties("cpMax") = ""
        grid.JSProperties("cpAve") = ""
        grid.JSProperties("cpR") = ""
        grid.JSProperties("cpC") = " "
        grid.JSProperties("cpNG") = " "
        grid.JSProperties("cpMKUser") = " "
        grid.JSProperties("cpMKDate") = " "
        grid.JSProperties("cpQCUser") = " "
        grid.JSProperties("cpQCDate") = " "
        grid.JSProperties("cpSubLotNo") = ""
        grid.JSProperties("cpRemarks") = ""
        grid.JSProperties("cpRefresh") = ""
    End Sub

    Private Sub up_ClearGrid()
        grid.DataSource = Nothing
        grid.DataBind()
        up_ClearJS()
    End Sub
    Protected Sub grid_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles grid.AfterPerformCallback
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            Dim Hdr As New clsHeader
            Hdr.FactoryCode = cboFactory.Value
            Hdr.FactoryName = cboFactory.Text
            Hdr.ItemTypeCode = cboType.Value
            Hdr.ItemTypeName = cboType.Text
            Hdr.LineCode = cboLine.Value
            Hdr.LineName = cboLine.Text
            Hdr.ItemCheckCode = cboItemCheck.Value
            Hdr.ItemCheckName = cboItemCheck.Value
            Hdr.ProdDate = Convert.ToDateTime(dtDate.Value).ToString("yyyy-MM-dd")
            Hdr.ShiftCode = cboShift.Value
            Hdr.Shiftname = cboShift.Text
            Hdr.Seq = cboSeq.Value
            GridLoad(cboFactory.Value, cboType.Value, cboLine.Value, cboItemCheck.Value, Format(dtDate.Value, "yyyy-MM-dd"), cboShift.Value, cboSeq.Value, cboShow.Value)
            GridXLoad(Hdr)
        End If
    End Sub

    Private Sub GridLoad(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String, Shift As String, Sequence As Integer, VerifiedOnly As Integer)
        Dim ErrMsg As String = ""
        Dim dt As DataTable = clsSPCResultDetailDB.GetTable(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate, Shift, Sequence, VerifiedOnly)
        grid.DataSource = dt
        grid.DataBind()

        Dim UserID As String = Session("user")
        Dim AllowSkill As Boolean = clsIOT.AllowSkill(UserID, FactoryCode, Line, ItemTypeCode)

        Dim Verified As Boolean = False
        If dt.Rows.Count = 0 Then
            up_ClearJS()
            grid.JSProperties("cpRefresh") = "1"
            Dim Setup As clsChartSetup = clsChartSetupDB.GetData(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
            If Setup IsNot Nothing Then
                grid.JSProperties("cpUSL") = Setup.SpecUSL
                grid.JSProperties("cpLSL") = Setup.SpecLSL
                grid.JSProperties("cpUCL") = Setup.XBarUCL
                grid.JSProperties("cpLCL") = Setup.XBarLCL
            End If
        Else
            With dt.Rows(0)
                grid.JSProperties("cpUSL") = AFormat(.Item("SpecUSL"))
                grid.JSProperties("cpLSL") = AFormat(.Item("SpecLSL"))
                grid.JSProperties("cpUCL") = AFormat(.Item("XBarUCL"))
                grid.JSProperties("cpLCL") = AFormat(.Item("XBarLCL"))
                grid.JSProperties("cpMin") = AFormat(.Item("MinValue"))
                grid.JSProperties("cpMax") = AFormat(.Item("MaxValue"))
                grid.JSProperties("cpAve") = AFormat(.Item("AvgValue"))
                grid.JSProperties("cpR") = AFormat(.Item("RValue"))
                grid.JSProperties("cpC") = .Item("CValue")
                grid.JSProperties("cpNG") = .Item("NGValue")
                grid.JSProperties("cpMKDate") = .Item("MKDate")
                grid.JSProperties("cpMKUser") = .Item("MKUser")
                grid.JSProperties("cpQCDate") = .Item("QCDate")
                grid.JSProperties("cpQCUser") = .Item("QCUser")
                grid.JSProperties("cpSubLotNo") = .Item("SubLotNo")
                grid.JSProperties("cpRemarks") = .Item("Remarks")
                grid.JSProperties("cpRefresh") = "1"
                If .Item("QCDate") & "" <> "" Then
                    Verified = True
                End If
            End With
            Dim LastVerification As Integer = clsSPCResultDB.GetLastVerification(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
            grid.SettingsDataSecurity.AllowInsert = LastVerification = 1 And Not Verified And AuthUpdate
            grid.SettingsDataSecurity.AllowEdit = LastVerification = 1 And Not Verified And AuthUpdate
        End If
    End Sub

    Private Function AFormat(v As Object) As String
        If v Is Nothing OrElse IsDBNull(v) Then
            Return ""
        Else
            Return Format(v, "0.000")
        End If
    End Function


    Private Sub grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles grid.CustomCallback
        Dim pFunction As String = Split(e.Parameters, "|")(0)
        Select Case pFunction
            Case "clear"
                up_ClearGrid()
            Case "load", "save", "approve"
                Dim pFactory As String = Split(e.Parameters, "|")(1)
                Dim pItemType As String = Split(e.Parameters, "|")(2)
                Dim pLine As String = Split(e.Parameters, "|")(3)
                Dim pItemCheck As String = Split(e.Parameters, "|")(4)
                Dim pDate As String = Split(e.Parameters, "|")(5)
                Dim pShift As String = Split(e.Parameters, "|")(6)
                Dim pSeq As String = Split(e.Parameters, "|")(7)
                Dim pVerified As String = Split(e.Parameters, "|")(8)
                pSeq = Val(pSeq)
                GridLoad(pFactory, pItemType, pLine, pItemCheck, pDate, pShift, pSeq, pVerified)
        End Select
    End Sub

    Private Sub grid_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs) Handles grid.RowUpdating
        e.Cancel = True
        Dim Result As New clsSPCResult
        Result.FactoryCode = cboFactory.Value
        Result.ItemCheckCode = cboItemCheck.Value
        Result.ItemTypeCode = cboType.Value
        Result.LineCode = cboLine.Value
        Result.ProdDate = dtDate.Value
        Result.ShiftCode = cboShift.Value
        Result.SequenceNo = cboSeq.Value
        Result.SubLotNo = Val(txtSubLotNo.Text)
        Result.Remark = txtRemarks.Text
        Result.RegisterUser = Session("user") & ""
        clsSPCResultDB.Insert(Result)

        Dim SeqNo As Integer = e.Keys("SeqNo")
        Dim Detail As New clsSPCResultDetail
        Detail.SPCResultID = Result.SPCResultID
        Detail.SequenceNo = SeqNo
        Detail.DeleteStatus = e.NewValues("DeleteStatus")
        Detail.Value = e.NewValues("Value")
        Detail.Remark = e.NewValues("Remark")
        Detail.RegisterUser = Result.RegisterUser
        clsSPCResultDetailDB.Insert(Detail)
        grid.CancelEdit()

        show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
    End Sub

    Private Sub cbkRefresh_Callback(source As Object, e As CallbackEventArgs) Handles cbkRefresh.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameter, "|")(1)
        Dim LineCode As String = Split(e.Parameter, "|")(2)
        Dim ItemCheckCode As String = Split(e.Parameter, "|")(3)
        Dim ProdDate As String = Split(e.Parameter, "|")(4)

        Dim Setup As clsChartSetup = clsChartSetupDB.GetData(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ProdDate)
        If Setup Is Nothing Then
            cbkRefresh.JSProperties("cpUSL") = " "
            cbkRefresh.JSProperties("cpLSL") = " "
            cbkRefresh.JSProperties("cpUCL") = " "
            cbkRefresh.JSProperties("cpLCL") = " "
        Else
            cbkRefresh.JSProperties("cpUSL") = Setup.SpecUSL
            cbkRefresh.JSProperties("cpLSL") = Setup.SpecLSL
            cbkRefresh.JSProperties("cpUCL") = Setup.XBarUCL
            cbkRefresh.JSProperties("cpLCL") = Setup.XBarLCL
        End If
    End Sub

    Private Sub cboType_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboType.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        cboType.DataSource = clsItemTypeDB.GetList(FactoryCode)
        cboType.DataBind()
    End Sub

    Private Sub cboLine_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboLine.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameter, "|")(1)
        Dim UserID As String = Session("user") & ""
        cboLine.DataSource = ClsLineDB.GetList(UserID, FactoryCode, ItemTypeCode)
        cboLine.DataBind()
    End Sub

    Private Sub GridTitle(ByVal pExl As ExcelWorksheet, cls As clsHeader)
        With pExl
            Try
                .Cells(1, 1).Value = "Production Sample Input"
                .Cells(1, 1, 1, 13).Merge = True
                .Cells(1, 1, 1, 13).Style.HorizontalAlignment = HorzAlignment.Near
                .Cells(1, 1, 1, 13).Style.VerticalAlignment = VertAlignment.Center
                .Cells(1, 1, 1, 13).Style.Font.Bold = True
                .Cells(1, 1, 1, 13).Style.Font.Size = 16
                .Cells(1, 1, 1, 13).Style.Font.Name = "Segoe UI"

                .Cells(3, 1, 3, 2).Value = "Factory Code"
                .Cells(3, 1, 3, 2).Merge = True
                .Cells(3, 3).Value = ": " & cls.FactoryName

                .Cells(4, 1, 4, 2).Value = "Item Type Code"
                .Cells(4, 1, 4, 2).Merge = True
                .Cells(4, 3).Value = ": " & cls.ItemTypeName

                .Cells(5, 1, 5, 2).Value = "Line Code"
                .Cells(5, 1, 5, 2).Merge = True
                .Cells(5, 3).Value = ": " & cls.LineName

                .Cells(6, 1, 6, 2).Value = "Item Check Code"
                .Cells(6, 1, 6, 2).Merge = True
                .Cells(6, 3).Value = ": " & cls.ItemCheckName

                .Cells(7, 1, 7, 2).Value = "Prod Date"
                .Cells(7, 1, 7, 2).Merge = True
                .Cells(7, 3).Value = ": " & cls.ProdDate

                .Cells(8, 1, 8, 2).Value = "Shift Code"
                .Cells(8, 1, 8, 2).Merge = True
                .Cells(8, 3).Value = ": " & cls.Shiftname

                .Cells(9, 1, 9, 2).Value = "Sequence No"
                .Cells(9, 1, 9, 2).Merge = True
                .Cells(9, 3).Value = ": " & cls.Seq

                Dim rgHdr As ExcelRange = .Cells(3, 3, 9, 4)
                rgHdr.Style.HorizontalAlignment = HorzAlignment.Near
                rgHdr.Style.VerticalAlignment = VertAlignment.Center
                rgHdr.Style.Font.Size = 10
                rgHdr.Style.Font.Name = "Segoe UI"
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End With
    End Sub

    Private Sub cboItemCheck_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboItemCheck.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameter, "|")(1)
        Dim LineCode As String = Split(e.Parameter, "|")(2)
        cboItemCheck.DataSource = clsItemCheckDB.GetList(FactoryCode, ItemTypeCode, LineCode)
        cboItemCheck.DataBind()
    End Sub

    Protected Sub grid_BatchUpdate(sender As Object, e As ASPxDataBatchUpdateEventArgs)
        Dim Result As New clsSPCResult
        If e.UpdateValues.Count > 0 Then
            Result.FactoryCode = cboFactory.Value
            Result.ItemCheckCode = cboItemCheck.Value
            Result.ItemTypeCode = cboType.Value
            Result.LineCode = cboLine.Value
            Result.ProdDate = dtDate.Value
            Result.ShiftCode = cboShift.Value
            Result.SequenceNo = cboSeq.Value
            Result.SubLotNo = 0
            Result.Remark = ""
            Result.RegisterUser = Session("user") & ""

            clsSPCResultDB.Insert(Result)
            For i = 0 To e.UpdateValues.Count - 1
                Dim SeqNo As String = e.UpdateValues(i).Keys("SeqNo")
                Dim Detail As New clsSPCResultDetail
                Detail.SPCResultID = Result.SPCResultID
                Detail.SequenceNo = SeqNo
                Detail.DeleteStatus = e.UpdateValues(i).NewValues("DeleteStatus")
                Detail.Value = e.UpdateValues(i).NewValues("Value")
                Detail.Remark = e.UpdateValues(i).NewValues("Remark")
                Detail.RegisterUser = Result.RegisterUser
                clsSPCResultDetailDB.Insert(Detail)
            Next
        End If
    End Sub

    Private Sub DownloadExcel()
        Dim ps As New PrintingSystem()
        LoadChartX(cboFactory.Value, cboType.Value, cboLine.Value, cboItemCheck.Value, Format(dtDate.Value, "yyyy-MM-dd"))
        Dim linkX As New PrintableComponentLink(ps)
        linkX.Component = (CType(chartX, IChartContainer)).Chart

        Dim compositeLink As New CompositeLink(ps)
        compositeLink.Links.AddRange(New Object() {linkX})
        compositeLink.CreateDocument()
        Dim Path As String = Server.MapPath("Download")
        Dim streamImg As New MemoryStream
        compositeLink.ExportToImage(streamImg)

        Using Pck As New ExcelPackage
            Dim ws As ExcelWorksheet = Pck.Workbook.Worksheets.Add("Sheet1")
            With ws
                Dim iDay As Integer = 2
                Dim iCol As Integer = 2
                Dim lastCol As Integer = 1
                Dim Seq As Integer = 0
                Dim SelDay As Date = dtDate.Value

                Dim Hdr As New clsHeader
                Hdr.FactoryCode = cboFactory.Value
                Hdr.FactoryName = cboFactory.Text
                Hdr.ItemTypeCode = cboType.Value
                Hdr.ItemTypeName = cboType.Text
                Hdr.LineCode = cboLine.Value
                Hdr.LineName = cboLine.Text
                Hdr.ItemCheckCode = cboItemCheck.Value
                Hdr.ItemCheckName = cboItemCheck.Value
                Hdr.ProdDate = Convert.ToDateTime(dtDate.Value).ToString("yyyy-MM-dd")
                Hdr.ShiftCode = cboShift.Value
                Hdr.Shiftname = cboShift.Text
                Hdr.Seq = cboSeq.Value

                GridTitle(ws, Hdr)
                GridExcel(ws, Hdr)
                LastRow = 50
                .InsertRow(LastRow, 22)
                Dim fi As New FileInfo(Path & "\chart.png")
                Dim Picture As OfficeOpenXml.Drawing.ExcelPicture
                Picture = .Drawings.AddPicture("chart", Image.FromStream(streamImg))
                Picture.SetPosition(20, 0, 0, 0)
            End With

            Dim stream As MemoryStream = New MemoryStream(Pck.GetAsByteArray())
            Response.AppendHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            Response.AppendHeader("Content-Disposition", "attachment; filename=ProdSampleInput_" & Format(Date.Now, "yyyy-MM-dd") & ".xlsx")
            Response.BinaryWrite(stream.ToArray())
            Response.End()

        End Using
    End Sub

    Dim LastRow As Integer

    Private Sub GridExcel(pExl As ExcelWorksheet, Hdr As clsHeader)
        Dim dt As DataTable = clsSPCResultDetailDB.GetTable(Hdr.FactoryCode, Hdr.ItemTypeCode, Hdr.LineCode, Hdr.ItemCheckCode, Hdr.ProdDate, Hdr.ShiftCode, Hdr.Seq, Hdr.VerifiedOnly)
        Dim iRow As Integer = 12
        With pExl
            .Cells(iRow, 1).Value = "Data"
            .Cells(iRow, 2).Value = "Value"
            .Cells(iRow, 3).Value = "Judgement"
            .Cells(iRow, 4).Value = "Operator"
            .Cells(iRow, 5).Value = "Sample Date"
            .Cells(iRow, 6).Value = "Delete Status"
            .Cells(iRow, 7).Value = "Remarks"
            .Cells(iRow, 8).Value = "Last User"
            .Cells(iRow, 9).Value = "Last Update"
            .Cells(iRow, 1, iRow, 9).Style.Fill.PatternType = ExcelFillStyle.Solid
            .Cells(iRow, 1, iRow, 9).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#878787"))
            For i = 0 To dt.Rows.Count - 1
                iRow = iRow + 1
                .Cells(iRow, 1).Value = dt.Rows(i)("SeqNo")
                .Cells(iRow, 2).Value = dt.Rows(i)("Value")
                .Cells(iRow, 3).Value = dt.Rows(i)("Judgement")
                .Cells(iRow, 4).Value = dt.Rows(i)("RegisterUser")
                .Cells(iRow, 5).Value = dt.Rows(i)("RegisterDate")
            Next

            iRow = iRow + 1
            .Cells(iRow, 1).Value = "Sub Lot No"
            .Cells(iRow, 2).Value = "Sub Lot No"
            iRow = iRow + 1
            .Cells(iRow, 1).Value = "Remarks"
            .Cells(iRow, 2).Value = "Remarks"
            iRow = iRow + 2

            Dim SelDay As Object = clsSPCResultDB.GetPrevDate(Hdr.FactoryCode, Hdr.ItemTypeCode, Hdr.LineCode, Hdr.ItemCheckCode, Hdr.ProdDate)
            .Cells(iRow, 1).Value = "Date"
            .Cells(iRow + 1, 1).Value = "Shift"
            .Cells(iRow + 2, 1).Value = "Time"
            Dim StartRow As Integer = iRow
            Dim StartCol As Integer, EndCol As Integer
            Dim StartCol2 As Integer, EndCol2 As Integer
            Dim FieldNames As New List(Of String)
            For iDay = 1 To 2
                Dim iCol As Integer = 1
                If Not IsDBNull(SelDay) Then
                    iRow = StartRow
                    iCol = iCol + 1
                    Dim dDay As String = Format(CDate(SelDay), "yyyy-MM-dd")
                    .Cells(iRow, iCol).Value = Format(SelDay, "dd MMM yyyy")

                    StartCol = iCol
                    Dim Shiftlist As List(Of clsShift) = clsFrequencyDB.GetShift(Hdr.FactoryCode, Hdr.ItemTypeCode, Hdr.LineCode, Hdr.ItemCheckCode, dDay)
                    For Each Shift In Shiftlist
                        .Cells(iRow + 1, iCol).Value = "S-" & Shift.ShiftName
                        StartCol2 = iCol
                        Dim SeqList As List(Of clsSequenceNo) = clsFrequencyDB.GetSequence(Hdr.FactoryCode, Hdr.ItemTypeCode, Hdr.LineCode, Hdr.ItemCheckCode, Shift.ShiftCode, dDay)
                        For Each Seq In SeqList
                            .Cells(iRow + 2, iCol).Value = Seq.StartTime
                            FieldNames.Add(iDay.ToString + "_" + Shift.ShiftName.ToString + "_" + Seq.SequenceNo.ToString)
                            iCol = iCol + 1
                        Next
                        EndCol2 = iCol
                        If EndCol2 > StartCol2 Then
                            .Cells(iRow + 1, StartCol2, iRow + 1, EndCol2).Merge = True
                        End If
                    Next
                    EndCol = iCol - 1
                    .Cells(iRow, StartCol, iRow, EndCol).Merge = True
                End If
                SelDay = CDate(Hdr.ProdDate)
                dt = clsSPCResultDetailDB.GetTableXR(Hdr.FactoryCode, Hdr.ItemTypeCode, Hdr.LineCode, Hdr.ItemCheckCode, Hdr.ProdDate, Hdr.VerifiedOnly)
                iRow = StartRow
                'For j = 0 To dt.Rows.Count - 1
                '    iCol = 1
                '    For Each Fn In FieldNames
                '        iCol = iCol + 1
                '        .Cells(iRow, iCol).Value = dt.Rows(j)(Fn)
                '    Next
                '    iRow = iRow + 1
                'Next
            Next
            LastRow = iRow + 1
        End With
    End Sub

    Private Sub cboShift_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboShift.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameter, "|")(1)
        Dim LineCode As String = Split(e.Parameter, "|")(2)
        Dim ItemCheckCode As String = Split(e.Parameter, "|")(3)
        cboShift.DataSource = clsFrequencyDB.GetShift(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode)
        cboShift.DataBind()
    End Sub

    Private Sub grid_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs) Handles grid.CellEditorInitialize
        If e.Column.FieldName = "Value" Then
            If IsDBNull(e.Value) Or grid.IsNewRowEditing Then
                e.Editor.ReadOnly = False
            Else
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = System.Drawing.Color.Silver
            End If
        ElseIf (e.Column.FieldName = "Remark" Or e.Column.FieldName = "DeleteStatus") Then
            e.Editor.ReadOnly = False
        Else
            e.Editor.ReadOnly = True
            e.Editor.ForeColor = System.Drawing.Color.Silver
        End If
    End Sub

    Private Sub grid_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles grid.HtmlDataCellPrepared
        If e.DataColumn.FieldName <> "Value" And e.DataColumn.FieldName <> "Remark" And e.DataColumn.FieldName <> "DeleteStatus" Then
            e.Cell.Attributes.Add("onclick", "event.cancelBubble = true")
        End If
    End Sub

    Private Sub grid_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs) Handles grid.HtmlRowPrepared
        If e.GetValue("DeleteStatus") IsNot Nothing AndAlso e.GetValue("DeleteStatus").ToString = "1" Then
            e.Row.BackColor = System.Drawing.Color.Silver
        ElseIf e.GetValue("JudgementColor") IsNot Nothing AndAlso Not IsDBNull(e.GetValue("JudgementColor")) Then
            If e.GetValue("JudgementColor") = "1" Then
                e.Row.BackColor = System.Drawing.Color.Pink
            ElseIf e.GetValue("JudgementColor") = "2" Then
                e.Row.BackColor = System.Drawing.Color.Red
            End If
        End If
    End Sub

    Private Sub cboSeq_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboSeq.Callback
        Dim FactoryCode As String = Split(e.Parameter, "|")(0)
        Dim ItemTypeCode As String = Split(e.Parameter, "|")(1)
        Dim LineCode As String = Split(e.Parameter, "|")(2)
        Dim ItemCheckCode As String = Split(e.Parameter, "|")(3)
        Dim ShiftCode As String = Split(e.Parameter, "|")(4)
        Dim ProdDate As String = Split(e.Parameter, "|")(5)
        cboSeq.DataSource = clsFrequencyDB.GetSequence(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode)
        cboSeq.DataBind()
    End Sub

    Private Sub gridX_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles gridX.CustomCallback
        Dim Hdr As New clsHeader
        Hdr.FactoryCode = Split(e.Parameters, "|")(0)
        Hdr.ItemTypeCode = Split(e.Parameters, "|")(1)
        Hdr.LineCode = Split(e.Parameters, "|")(2)
        Hdr.ItemCheckCode = Split(e.Parameters, "|")(3)
        Hdr.ProdDate = Split(e.Parameters, "|")(4)
        Hdr.VerifiedOnly = Split(e.Parameters, "|")(5)
        GridXLoad(Hdr)
    End Sub

    Private Sub gridX_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs) Handles gridX.HtmlRowPrepared
        If e.KeyValue = "Min" Or e.KeyValue = "Max" Or e.KeyValue = "Avg" Or e.KeyValue = "R" Then
            'e.Row.BackColor = System.Drawing.Color.LightGray
            e.Row.BorderStyle = BorderStyle.Double
            e.Row.BorderWidth = 50
        End If
        If e.KeyValue = "-" Or e.KeyValue = "--" Then
            e.Row.BackColor = System.Drawing.Color.FromArgb(112, 112, 112)
            e.Row.Height = 5
        End If
    End Sub

    Private Sub LoadChartR(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String)
        Dim xr As List(Of clsXRChart) = clsXRChartDB.GetChartR(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
        If xr.Count = 0 Then
            chartR.JSProperties("cpShow") = "0"
        Else
            chartR.JSProperties("cpShow") = "1"
        End If
        With chartR
            .DataSource = xr
            Dim diagram As XYDiagram = CType(.Diagram, XYDiagram)
            diagram.AxisX.WholeRange.MinValue = 0
            diagram.AxisX.WholeRange.MaxValue = 12

            diagram.AxisX.GridLines.LineStyle.DashStyle = DashStyle.Solid
            diagram.AxisX.GridLines.MinorVisible = True
            diagram.AxisX.MinorCount = 1
            diagram.AxisX.GridLines.Visible = False



            Dim Setup As clsChartSetup = clsChartSetupDB.GetData(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
            diagram.AxisY.ConstantLines.Clear()
            If Setup IsNot Nothing Then
                Dim RCL As New ConstantLine("CL R")
                RCL.Color = System.Drawing.Color.Purple
                RCL.LineStyle.Thickness = 2
                RCL.LineStyle.DashStyle = DashStyle.DashDot
                diagram.AxisY.ConstantLines.Add(RCL)
                RCL.AxisValue = Setup.RCL

                Dim RUCL As New ConstantLine("UCL R")
                RUCL.Color = System.Drawing.Color.Purple
                RUCL.LineStyle.Thickness = 2
                RUCL.LineStyle.DashStyle = DashStyle.DashDot
                diagram.AxisY.ConstantLines.Add(RUCL)
                RUCL.AxisValue = Setup.RUCL

                Dim MaxValue As Double
                If xr.Count > 0 Then
                    If xr(0).MaxValue > Setup.RUCL Then
                        MaxValue = xr(0).MaxValue
                    Else
                        MaxValue = Setup.RUCL
                    End If
                End If
                diagram.AxisY.WholeRange.MaxValue = MaxValue
            End If
            .DataBind()
        End With
    End Sub

    Private Sub LoadChartX(FactoryCode As String, ItemTypeCode As String, Line As String, ItemCheckCode As String, ProdDate As String)
        Dim xr As List(Of clsXRChart) = clsXRChartDB.GetChartXR(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
        With chartX
            .DataSource = xr
            Dim diagram As XYDiagram = CType(.Diagram, XYDiagram)
            diagram.AxisX.WholeRange.MinValue = 0
            diagram.AxisX.WholeRange.MaxValue = 12

            diagram.AxisX.GridLines.LineStyle.DashStyle = DashStyle.Solid
            diagram.AxisX.GridLines.MinorVisible = True
            diagram.AxisX.MinorCount = 1
            diagram.AxisX.GridLines.Visible = False

            diagram.AxisY.NumericScaleOptions.CustomGridAlignment = 0.005
            diagram.AxisY.GridLines.MinorVisible = False
            Dim ChartType As String = clsXRChartDB.GetChartType(FactoryCode, ItemTypeCode, Line, ItemCheckCode)
            If ChartType = "1" Then
                .Titles(0).Text = "Chart X"
            Else
                .Titles(0).Text = "Graph Monitoring"
            End If


            Dim Setup As clsChartSetup = clsChartSetupDB.GetData(FactoryCode, ItemTypeCode, Line, ItemCheckCode, ProdDate)
            diagram.AxisY.ConstantLines.Clear()
            If Setup IsNot Nothing Then
                Dim LCL As New ConstantLine("LCL")
                LCL.Color = System.Drawing.Color.Purple
                LCL.LineStyle.Thickness = 2
                LCL.LineStyle.DashStyle = DashStyle.DashDot
                diagram.AxisY.ConstantLines.Add(LCL)
                LCL.AxisValue = Setup.XBarLCL

                Dim UCL As New ConstantLine("UCL")
                UCL.Color = System.Drawing.Color.Purple
                UCL.LineStyle.Thickness = 2
                UCL.LineStyle.DashStyle = DashStyle.DashDot
                diagram.AxisY.ConstantLines.Add(UCL)
                UCL.AxisValue = Setup.XBarUCL

                Dim CL As New ConstantLine("CL")
                CL.Color = System.Drawing.Color.Black
                CL.LineStyle.Thickness = 2
                CL.LineStyle.DashStyle = DashStyle.Solid
                diagram.AxisY.ConstantLines.Add(CL)
                CL.AxisValue = Setup.XBarCL

                Dim LSL As New ConstantLine("LSL")
                LSL.Color = System.Drawing.Color.Red
                LSL.LineStyle.Thickness = 2
                LSL.LineStyle.DashStyle = DashStyle.Solid
                diagram.AxisY.ConstantLines.Add(LSL)
                LSL.AxisValue = Setup.SpecLSL

                Dim USL As New ConstantLine("USL")
                USL.Color = System.Drawing.Color.Red
                USL.LineStyle.Thickness = 2
                USL.LineStyle.DashStyle = DashStyle.Solid
                diagram.AxisY.ConstantLines.Add(USL)
                USL.AxisValue = Setup.SpecUSL

                diagram.AxisY.WholeRange.MinValue = Setup.SpecLSL
                diagram.AxisY.WholeRange.MaxValue = Setup.SpecUSL
                diagram.AxisY.WholeRange.EndSideMargin = Setup.SpecUSL + 1

                CType(.Diagram, XYDiagram).SecondaryAxesY.Clear()
                Dim myAxisY As New SecondaryAxisY("my Y-Axis")
                myAxisY.Visibility = DevExpress.Utils.DefaultBoolean.False
                CType(.Diagram, XYDiagram).SecondaryAxesY.Add(myAxisY)
                CType(.Series("Rule").View, XYDiagramSeriesViewBase).AxisY = myAxisY
                CType(.Series("RuleYellow").View, XYDiagramSeriesViewBase).AxisY = myAxisY
            End If
            .DataBind()
            .Width = xr.Count * 20
        End With
    End Sub

    Private Sub chartX_CustomCallback(sender As Object, e As CustomCallbackEventArgs) Handles chartX.CustomCallback

        Dim Prm As String = e.Parameter
        If Prm = "" Then
            Prm = "F001|TPMSBR011|015|IC021|03 Aug 2022"
        End If
        Dim FactoryCode As String = Split(Prm, "|")(0)
        Dim ItemTypeCode As String = Split(Prm, "|")(1)
        Dim LineCode As String = Split(Prm, "|")(2)
        Dim ItemCheckCode As String = Split(Prm, "|")(3)
        Dim ProdDate As String = Split(Prm, "|")(4)

        LoadChartX(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ProdDate)
    End Sub

    Private Sub gridX_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles gridX.HtmlDataCellPrepared
        Dim LCL As Double
        Dim UCL As Double
        Dim LSL As Double
        Dim USL As Double
        Dim SetupFound As Boolean = False
        If Not IsDBNull(e.CellValue) AndAlso (e.DataColumn.FieldName.StartsWith("1") Or e.DataColumn.FieldName.StartsWith("2")) _
            And (e.GetValue("Seq") = "1" Or e.GetValue("Seq") = "3" Or e.GetValue("Seq") = "4" Or e.GetValue("Seq") = "5") Then
            If (e.DataColumn.FieldName.StartsWith("1")) Then
                If Not IsDBNull(e.GetValue("XBarLCL1")) Then
                    SetupFound = True
                    LCL = e.GetValue("XBarLCL1")
                    UCL = e.GetValue("XBarUCL1")
                    LSL = e.GetValue("SpecLSL1")
                    USL = e.GetValue("SpecUSL1")
                End If
            ElseIf (e.DataColumn.FieldName.StartsWith("2")) Then
                If Not IsDBNull(e.GetValue("XBarLCL2")) Then
                    SetupFound = True
                    LCL = e.GetValue("XBarLCL2")
                    UCL = e.GetValue("XBarUCL2")
                    LSL = e.GetValue("SpecLSL2")
                    USL = e.GetValue("SpecUSL2")
                End If
            End If
            If SetupFound Then
                Dim Value As Double = clsSPCResultDB.ADecimal(e.CellValue)
                If Value < LSL Or Value > USL Then
                    e.Cell.BackColor = Color.Red
                ElseIf Value < LCL Or Value > UCL Then
                    e.Cell.BackColor = Color.Pink
                End If
            End If
        End If
        If e.DataColumn.FieldName = "Des" Then
            If e.CellValue = "1" Then
                e.Cell.BackColor = Color.Red
            ElseIf e.CellValue = "2" Then
                e.Cell.BackColor = Color.Orange
            ElseIf e.CellValue = "3" Then
                e.Cell.BackColor = Color.LightGreen
            ElseIf e.CellValue = "4" Then
                e.Cell.BackColor = Color.DarkGreen
            ElseIf e.CellValue = "5" Then
                e.Cell.BackColor = Color.LightBlue
            End If
        End If
        If e.KeyValue = "-" Or e.KeyValue = "--" Then
            e.Cell.Text = ""
        End If
    End Sub

    Private Sub chartR_CustomCallback(sender As Object, e As CustomCallbackEventArgs) Handles chartR.CustomCallback
        Dim Prm As String = e.Parameter
        If Prm = "" Then
            Prm = "F001|TPMSBR011|015|IC021|03 Aug 2022"
        End If
        Dim FactoryCode As String = Split(Prm, "|")(0)
        Dim ItemTypeCode As String = Split(Prm, "|")(1)
        Dim LineCode As String = Split(Prm, "|")(2)
        Dim ItemCheckCode As String = Split(Prm, "|")(3)
        Dim ProdDate As String = Split(Prm, "|")(4)
        LoadChartR(FactoryCode, ItemTypeCode, LineCode, ItemCheckCode, ProdDate)
    End Sub

    Private Sub chartX_CustomDrawSeries(sender As Object, e As CustomDrawSeriesEventArgs) Handles chartX.CustomDrawSeries
        Dim s As String = e.Series.Name
        If s = "#1" Then
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.Kind = MarkerKind.Circle
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.BorderColor = Color.Red
            CType(e.SeriesDrawOptions, PointDrawOptions).Color = Color.Red
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.FillStyle.FillMode = FillMode.Solid
            e.LegendDrawOptions.Color = Color.Red
        ElseIf s = "#2" Then
            'CType(e.SeriesDrawOptions, PointDrawOptions).Marker.Kind = MarkerKind.Diamond
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.BorderColor = Color.Orange
            CType(e.SeriesDrawOptions, PointDrawOptions).Color = Color.Orange
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.FillStyle.FillMode = FillMode.Solid
            e.LegendDrawOptions.Color = Color.Orange
        ElseIf s = "#3" Then
            'CType(e.SeriesDrawOptions, PointDrawOptions).Marker.Kind = MarkerKind.Triangle
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.BorderColor = Color.Green
            CType(e.SeriesDrawOptions, PointDrawOptions).Color = Color.LightGreen
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.FillStyle.FillMode = FillMode.Solid
            e.LegendDrawOptions.Color = Color.LightGreen
        ElseIf s = "#4" Then
            'CType(e.SeriesDrawOptions, PointDrawOptions).Marker.Kind = MarkerKind.Square
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.BorderColor = Color.DarkGreen
            CType(e.SeriesDrawOptions, PointDrawOptions).Color = Color.DarkGreen
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.FillStyle.FillMode = FillMode.Solid
            e.LegendDrawOptions.Color = Color.DarkGreen
        ElseIf s = "#5" Then
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.Kind = MarkerKind.Circle
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.BorderColor = Color.Blue
            CType(e.SeriesDrawOptions, PointDrawOptions).Color = Color.LightBlue
            CType(e.SeriesDrawOptions, PointDrawOptions).Marker.FillStyle.FillMode = FillMode.Solid
            e.LegendDrawOptions.Color = Color.LightBlue
        End If
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        DownloadExcel()
    End Sub

    Private Sub grid_StartRowEditing(sender As Object, e As ASPxStartRowEditingEventArgs) Handles grid.StartRowEditing

    End Sub
End Class