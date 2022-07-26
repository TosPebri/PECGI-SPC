Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.Data
Imports OfficeOpenXml

Public Class TCCSResultApproval
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
    Public Result As String
    Public StatusApproval1 As String
    Public StatusApproval2 As String
    Public StatusApproval3 As String
    Public StatusApproval4 As String
#End Region

#Region "Procedure"
    Private Sub up_fillline()
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSResultApprovalDB.GetDataLine(pUser, "")
        cbolineid.DataSource = dsMenu
        cbolineid.DataBind()
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        GridMenu.JSProperties("cp_message") = ErrMsg
        GridMenu.JSProperties("cp_type") = msgType
        GridMenu.JSProperties("cp_val") = pVal
    End Sub

    Private Sub up_GridLoadMenu(ByVal pStartDate As String, ByVal pEndDate As String, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pMachineNo As String, ByVal pJudgement As String, ByVal pPartID As String, ByVal pApprovalStatus As String, ByVal pApprovalStatus1 As String, ByVal pApprovalStatus2 As String, ByVal pApprovalStatus3 As String, ByVal pApprovalStatus4 As String)
        Dim ErrMsg As String = ""
        Dim Menu As DataSet
        Menu = ClsTCCSResultApprovalDB.GetData(pUser, pStartDate, pEndDate, pLineID, pSubLineID, pMachineNo, pJudgement, pPartID, pApprovalStatus, pApprovalStatus1, pApprovalStatus2, pApprovalStatus3, pApprovalStatus4, ErrMsg)
        If ErrMsg = "" Then
            GridMenu.DataSource = Menu
            GridMenu.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub Approval1(ByVal pTCCSResultID As String, ByVal pJudgementApproval As String)
        Dim pErr As String = ""
        Dim TCCSResultApproval As New ClsTCCSResultApproval
        TCCSResultApproval.ApprovalPIC1 = pUser
        TCCSResultApproval.TCCSResultID = pTCCSResultID
        TCCSResultApproval.ApprovalJudgement1 = pJudgementApproval
        ClsTCCSResultApprovalDB.Approve1(TCCSResultApproval, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Approve successfully!", 1)
        End If
    End Sub

    Private Sub Approval2(ByVal pTCCSResultID As String, ByVal pJudgementApproval As String)
        Dim pErr As String = ""
        Dim TCCSResultApproval As New ClsTCCSResultApproval
        TCCSResultApproval.ApprovalPIC2 = pUser
        TCCSResultApproval.TCCSResultID = pTCCSResultID
        TCCSResultApproval.ApprovalJudgement2 = pJudgementApproval
        ClsTCCSResultApprovalDB.Approve2(TCCSResultApproval, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Approve successfully!", 1)
        End If
    End Sub

    Private Sub Approval3(ByVal pTCCSResultID As String, ByVal pJudgementApproval As String)
        Dim pErr As String = ""
        Dim TCCSResultApproval As New ClsTCCSResultApproval
        TCCSResultApproval.ApprovalPIC3 = pUser
        TCCSResultApproval.TCCSResultID = pTCCSResultID
        TCCSResultApproval.ApprovalJudgement3 = pJudgementApproval
        ClsTCCSResultApprovalDB.Approve3(TCCSResultApproval, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Approve successfully!", 1)
        End If
    End Sub

    Private Sub Approval4(ByVal pTCCSResultID As String, ByVal pJudgementApproval As String)
        Dim pErr As String = ""
        Dim TCCSResultApproval As New ClsTCCSResultApproval
        TCCSResultApproval.ApprovalPIC4 = pUser
        TCCSResultApproval.TCCSResultID = pTCCSResultID
        TCCSResultApproval.ApprovalJudgement4 = pJudgementApproval
        ClsTCCSResultApprovalDB.Approve4(TCCSResultApproval, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Approve successfully!", 1)
        End If
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "C040")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If

        sGlobal.getMenu("C040")
        Master.SiteTitle = sGlobal.menuName
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "C040")
        Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
        up_fillline()
        'If ClsTCCSResultApprovalDB.CekUser(pUser) = True Then
        '    btnApproveOK.ClientEnabled = True
        '    btnApproveNG.ClientEnabled = True
        'Else
        '    btnApproveOK.ClientEnabled = False
        '    btnApproveNG.ClientEnabled = False
        'End If
        If ClsTCCSResultApprovalDB.GetDataQESecHead(pUser) = True Then
            StatusApproval4 = 1
        Else
            StatusApproval4 = 0
        End If

        If ClsTCCSResultApprovalDB.GetDataProdSecHead(pUser) = True Then
            StatusApproval3 = 1
        Else
            StatusApproval3 = 0
        End If

        If ClsTCCSResultApprovalDB.GetDataLineLeader(pUser) = True Then
            StatusApproval2 = 1
        Else
            StatusApproval2 = 0
        End If

        If ClsTCCSResultApprovalDB.GetDataQELeader(pUser) = True Then
            StatusApproval1 = 1
        Else
            StatusApproval1 = 0
        End If

        Dim Menu As DataSet
        Menu = ClsTCCSResultApprovalDB.GetStartDate(IIf(pUser = Nothing, "", pUser), IIf(StatusApproval1 = Nothing, "", StatusApproval1), IIf(StatusApproval2 = Nothing, "", StatusApproval2), IIf(StatusApproval3 = Nothing, "", StatusApproval3), IIf(StatusApproval4 = Nothing, "", StatusApproval4))
        If Menu.Tables(0).Rows.Count = 1 And Not IsCallback Then
            dtstart.Value = Menu.Tables(0).Rows(0)("Date")
        ElseIf Menu.Tables(0).Rows.Count <> 1 And Not IsCallback Then
            dtstart.Value = Date.Now()
        End If
    End Sub

#Region "GridMenu"
    Protected Sub GridMenu_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs) Handles GridMenu.AfterPerformCallback
        Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            up_GridLoadMenu(dtstart.Value, dtend.Value, cbolineid.Value, cbosublineid.Value, cbomachineno.Value, cbojudgement.Value, cbopartid.Value, cboapprovalstatus.Value, StatusApproval1, StatusApproval2, StatusApproval3, StatusApproval4)
        End If
    End Sub

    Private Sub GridMenu_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridMenu.BeforeGetCallbackResult
        up_GridLoadMenu(dtstart.Value, dtend.Value, cbolineid.Value, cbosublineid.Value, cbomachineno.Value, cbojudgement.Value, cbopartid.Value, cboapprovalstatus.Value, StatusApproval1, StatusApproval2, StatusApproval3, StatusApproval4)
    End Sub

    Private Sub GridMenu_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs) Handles GridMenu.CellEditorInitialize

    End Sub

    Private Sub GridMenu_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridMenu.CustomCallback
        Dim pFunction As String = Split(e.Parameters, "|")(0)

        Select Case pFunction
            'Case "Approve"
            '    Dim pJudgementApproval As String = Split(e.Parameters, "|")(1)
            '    Dim table As DataTable = Nothing
            '    table = DirectCast(Session("table"), DataTable)
            '    Dim selectItems As List(Of Object) = GridMenu.GetSelectedFieldValues("TCCSResultID;Judgement")
            '    If selectItems.Count = 0 Then
            '        show_error(MsgTypeEnum.Warning, "Please select data!", 1)
            '    Else
            '        For Each selectItemId As Object In selectItems
            '            Dim pTCCSResultID As String = Split(selectItemId, "|")(0)
            '            Dim pJudgement As String = Split(selectItemId, "|")(1)
            '            Dim Menu As DataSet
            '            Menu = ClsTCCSResultApprovalDB.CekStatusApproval(pTCCSResultID)

            '            If ClsTCCSResultApprovalDB.GetDataQELeader(pUser) = True And ClsTCCSResultApprovalDB.GetDataLineLeader(pUser) = True And ClsTCCSResultApprovalDB.GetDataProdSecHead(pUser) = True And ClsTCCSResultApprovalDB.GetDataQESecHead(pUser) = True And Menu.Tables(0).Rows(0)("ApprovalStatus1").ToString <> "1" Then
            '                If pJudgement = "OK" Then
            '                    Approval1(pTCCSResultID, pJudgementApproval)
            '                    Approval2(pTCCSResultID, pJudgementApproval)
            '                ElseIf pJudgement = "NG" Then
            '                    Approval1(pTCCSResultID, pJudgementApproval)
            '                    Approval2(pTCCSResultID, pJudgementApproval)
            '                    Approval3(pTCCSResultID, pJudgementApproval)
            '                    Approval4(pTCCSResultID, pJudgementApproval)
            '                End If

            '            ElseIf ClsTCCSResultApprovalDB.GetDataQELeader(pUser) = True And ClsTCCSResultApprovalDB.GetDataLineLeader(pUser) = True And ClsTCCSResultApprovalDB.GetDataProdSecHead(pUser) = True And Menu.Tables(0).Rows(0)("ApprovalStatus1").ToString <> "1" Then
            '                If pJudgement = "OK" Then
            '                    Approval1(pTCCSResultID, pJudgementApproval)
            '                    Approval2(pTCCSResultID, pJudgementApproval)
            '                ElseIf pJudgement = "NG" Then
            '                    Approval1(pTCCSResultID, pJudgementApproval)
            '                    Approval2(pTCCSResultID, pJudgementApproval)
            '                    Approval3(pTCCSResultID, pJudgementApproval)
            '                End If

            '            ElseIf ClsTCCSResultApprovalDB.GetDataQELeader(pUser) = True And ClsTCCSResultApprovalDB.GetDataLineLeader(pUser) = True And Menu.Tables(0).Rows(0)("ApprovalStatus1").ToString <> "1" Then
            '                Approval1(pTCCSResultID, pJudgementApproval)
            '                Approval2(pTCCSResultID, pJudgementApproval)

            '            ElseIf ClsTCCSResultApprovalDB.GetDataQELeader(pUser) = True And Menu.Tables(0).Rows(0)("ApprovalStatus1").ToString <> "1" Then
            '                Approval1(pTCCSResultID, pJudgementApproval)

            '            ElseIf ClsTCCSResultApprovalDB.GetDataLineLeader(pUser) = True And ClsTCCSResultApprovalDB.GetDataProdSecHead(pUser) = True And ClsTCCSResultApprovalDB.GetDataQESecHead(pUser) = True And Menu.Tables(0).Rows(0)("ApprovalStatus1").ToString = "1" And Menu.Tables(0).Rows(0)("ApprovalStatus2").ToString <> "1" Then
            '                If pJudgement = "OK" Then
            '                    Approval2(pTCCSResultID, pJudgementApproval)
            '                ElseIf pJudgement = "NG" Then
            '                    Approval2(pTCCSResultID, pJudgementApproval)
            '                    Approval3(pTCCSResultID, pJudgementApproval)
            '                    Approval4(pTCCSResultID, pJudgementApproval)
            '                End If

            '            ElseIf ClsTCCSResultApprovalDB.GetDataProdSecHead(pUser) = True And ClsTCCSResultApprovalDB.GetDataQESecHead(pUser) = True And Menu.Tables(0).Rows(0)("ApprovalStatus2").ToString = "1" And Menu.Tables(0).Rows(0)("ApprovalStatus3").ToString <> "1" And pJudgement = "NG" Then
            '                Approval3(pTCCSResultID, pJudgementApproval)
            '                Approval4(pTCCSResultID, pJudgementApproval)

            '            ElseIf ClsTCCSResultApprovalDB.GetDataQESecHead(pUser) = True And Menu.Tables(0).Rows(0)("ApprovalStatus3").ToString = "1" And Menu.Tables(0).Rows(0)("ApprovalStatus4").ToString <> "1" And pJudgement = "NG" Then
            '                Approval4(pTCCSResultID, pJudgementApproval)

            '            ElseIf ClsTCCSResultApprovalDB.GetDataLineLeader(pUser) = True And Menu.Tables(0).Rows(0)("ApprovalStatus1").ToString = "1" And Menu.Tables(0).Rows(0)("ApprovalStatus2").ToString <> "1" Then
            '                Approval2(pTCCSResultID, pJudgementApproval)

            '            ElseIf ClsTCCSResultApprovalDB.GetDataProdSecHead(pUser) = True And Menu.Tables(0).Rows(0)("ApprovalStatus2").ToString = "1" And Menu.Tables(0).Rows(0)("ApprovalStatus3").ToString <> "1" And pJudgement = "NG" Then
            '                Approval3(pTCCSResultID, pJudgementApproval)

            '            Else
            '                GridMenu.JSProperties("cp_messageapprove") = "1"
            '            End If
            '        Next selectItemId
            '        'up_GridLoadMenu()
            '    End If

            Case "Excel"
                up_Excel(dtstart.Value, dtend.Value, cbolineid.Value, cbosublineid.Value, cbomachineno.Value, cbojudgement.Value, cbopartid.Value, cboapprovalstatus.Value)

            Case "Refresh"
                up_GridLoadMenu(dtstart.Value, dtend.Value, cbolineid.Value, cbosublineid.Value, cbomachineno.Value, cbojudgement.Value, cbopartid.Value, cboapprovalstatus.Value, StatusApproval1, StatusApproval2, StatusApproval3, StatusApproval4)

            Case "ViewTCCSResult"
                GridMenu.JSProperties("cp_viewtccsresult") = "1"
                GridMenu.JSProperties("cp_date") = Format(GridMenu.GetSelectedFieldValues("Date")(0), "yyyy-MM-dd")
                GridMenu.JSProperties("cp_machineno") = RTrim(GridMenu.GetSelectedFieldValues("MachineNo")(0).ToString)
                GridMenu.JSProperties("cp_lineid") = RTrim(GridMenu.GetSelectedFieldValues("LineID")(0).ToString)
                GridMenu.JSProperties("cp_sublineid") = RTrim(GridMenu.GetSelectedFieldValues("SubLineID")(0).ToString)
                GridMenu.JSProperties("cp_partid") = RTrim(GridMenu.GetSelectedFieldValues("PartID")(0).ToString)
        End Select
    End Sub

    Protected Sub GridMenu_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles GridMenu.RowInserting

    End Sub

    Private Sub GridMenu_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles GridMenu.RowUpdating

    End Sub

    Protected Sub GridMenu_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles GridMenu.RowDeleting

    End Sub

    Protected Sub GridMenu_RowValidating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataValidationEventArgs)

    End Sub

    Protected Sub GridMenu_StartRowEditing(sender As Object, e As DevExpress.Web.Data.ASPxStartRowEditingEventArgs) Handles GridMenu.StartRowEditing

    End Sub

    Private Sub GridMenu_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs) Handles GridMenu.HtmlDataCellPrepared

        If (e.DataColumn.FieldName = "Judgement") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Red
                Result = "NG"
            Else
                Result = "OK"
            End If
        End If

        If (e.DataColumn.FieldName = "ApprovalPIC1") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            End If
        End If

        If (e.DataColumn.FieldName = "ApprovalJudgement1") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            ElseIf e.CellValue = "NG" Then
                e.Cell.BackColor = Color.Red
                e.Cell.ForeColor = Color.White
            End If
        End If

        If (e.DataColumn.FieldName = "ApprovalDate1") Then
           If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            End If
        End If

        If (e.DataColumn.FieldName = "ApprovalPIC2") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            End If
        End If

        If (e.DataColumn.FieldName = "ApprovalJudgement2") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            ElseIf e.CellValue = "NG" Then
                e.Cell.BackColor = Color.Red
                e.Cell.ForeColor = Color.White
            End If
        End If

        If (e.DataColumn.FieldName = "ApprovalDate2") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            End If
        End If

        If (e.DataColumn.FieldName = "ApprovalPIC3") Then
            If IsDBNull(e.CellValue) Then
                If Result = "NG" Then
                    e.Cell.BackColor = Color.Red
                ElseIf Result = "OK" Then
                    e.Cell.BackColor = Color.Gray
                End If
            End If
        End If

        If (e.DataColumn.FieldName = "ApprovalJudgement3") Then
            If IsDBNull(e.CellValue) Then
                If Result = "NG" Then
                    e.Cell.BackColor = Color.Red
                ElseIf Result = "OK" Then
                    e.Cell.BackColor = Color.Gray
                End If
            ElseIf e.CellValue = "NG" Then
                e.Cell.BackColor = Color.Red
                e.Cell.ForeColor = Color.White
            End If
        End If

        If (e.DataColumn.FieldName = "ApprovalDate3") Then
            If IsDBNull(e.CellValue) Then
                If Result = "NG" Then
                    e.Cell.BackColor = Color.Red
                ElseIf Result = "OK" Then
                    e.Cell.BackColor = Color.Gray
                End If
            End If
        End If

        If (e.DataColumn.FieldName = "ApprovalPIC4") Then
            If IsDBNull(e.CellValue) Then
                If Result = "NG" Then
                    e.Cell.BackColor = Color.Red
                ElseIf Result = "OK" Then
                    e.Cell.BackColor = Color.Gray
                End If
            End If
        End If

        If (e.DataColumn.FieldName = "ApprovalJudgement4") Then
            If IsDBNull(e.CellValue) Then
                If Result = "NG" Then
                    e.Cell.BackColor = Color.Red
                ElseIf Result = "OK" Then
                    e.Cell.BackColor = Color.Gray
                End If
            ElseIf e.CellValue = "NG" Then
                e.Cell.BackColor = Color.Red
                e.Cell.ForeColor = Color.White
            End If
        End If

        If (e.DataColumn.FieldName = "ApprovalDate4") Then
            If IsDBNull(e.CellValue) Then
                If Result = "NG" Then
                    e.Cell.BackColor = Color.Red
                ElseIf Result = "OK" Then
                    e.Cell.BackColor = Color.Gray
                End If
            End If
        End If
    End Sub
#End Region

#Region "CallBack"
    Private Sub cbosublineid_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cbosublineid.Callback
        Dim pLineID As String = Split(e.Parameter, "|")(0)
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSResultApprovalDB.GetDataSubLine(pLineID, pUser, "")
        cbosublineid.DataSource = dsMenu
        cbosublineid.DataBind()
    End Sub

    Private Sub cbomachineno_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cbomachineno.Callback
        Dim pLineID As String = Split(e.Parameter, "|")(0)
        Dim pSubLineID As String = Split(e.Parameter, "|")(1)
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSResultApprovalDB.GetDataMachine(pLineID, pSubLineID, pUser, "")
        cbomachineno.DataSource = dsMenu
        cbomachineno.DataBind()
    End Sub

    Private Sub cbojudgement_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cbojudgement.Callback
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSResultApprovalDB.GetDataJudgement("")
        cbojudgement.DataSource = dsMenu
        cbojudgement.DataBind()
    End Sub

    Private Sub cbopartid_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cbopartid.Callback
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSResultApprovalDB.GetDataPart("")
        cbopartid.DataSource = dsMenu
        cbopartid.DataBind()
    End Sub

    Private Sub cboapprovalstatus_Callback(sender As Object, e As DevExpress.Web.ASPxClasses.CallbackEventArgsBase) Handles cboapprovalstatus.Callback
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSResultApprovalDB.GetDataApprovalStatus("")
        cboapprovalstatus.DataSource = dsMenu
        cboapprovalstatus.DataBind()
    End Sub
#End Region

#Region "Download To Excel"
    Private Sub up_Excel(ByVal pStartDate As String, ByVal pEndDate As String, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pMachineNo As String, ByVal pJudgement As String, ByVal pPartID As String, ByVal pApprovalStatus As String, Optional ByRef pErr As String = "")
        Try
            Dim fi As New FileInfo(Server.MapPath("~\Download\Change Item Check Sheet Result Approval.xlsx"))
            If fi.Exists Then
                fi.Delete()
                fi = New FileInfo(Server.MapPath("~\Download\Change Item Check Sheet Result Approval.xlsx"))
            End If
            Dim exl As New ExcelPackage(fi)
            Dim ws As ExcelWorksheet
            ws = exl.Workbook.Worksheets.Add("Sheet1")
            ws.View.ShowGridLines = False

            Dim Rpt As DataSet
            Rpt = ClsTCCSResultApprovalDB.GetData(pUser, pStartDate, pEndDate, pLineID, pSubLineID, pMachineNo, pJudgement, pPartID, pApprovalStatus, StatusApproval1, StatusApproval2, StatusApproval3, StatusApproval4, pErr)

            With ws
                .Cells(6, 1, 8, 1).Value = "Date"
                .Cells(6, 1, 8, 1).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 1, 8, 1).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 1, 8, 1).Merge = True
                .Cells(6, 1, 8, 1).Style.Font.Size = 10
                .Cells(6, 1, 8, 1).Style.Font.Name = "Segoe UI"
                .Cells(6, 1, 8, 1).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 2, 8, 2).Value = "Line No"
                .Cells(6, 2, 8, 2).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 2, 8, 2).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 2, 8, 2).Merge = True
                .Cells(6, 2, 8, 2).Style.Font.Size = 10
                .Cells(6, 2, 8, 2).Style.Font.Name = "Segoe UI"
                .Cells(6, 2, 8, 2).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 3, 8, 3).Value = "Sub Line No"
                .Cells(6, 3, 8, 3).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 3, 8, 3).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 3, 8, 3).Merge = True
                .Cells(6, 3, 8, 3).Style.Font.Size = 10
                .Cells(6, 3, 8, 3).Style.Font.Name = "Segoe UI"
                .Cells(6, 3, 8, 3).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 4, 8, 4).Value = "Machine No"
                .Cells(6, 4, 8, 4).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 4, 8, 4).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 4, 8, 4).Merge = True
                .Cells(6, 4, 8, 4).Style.Font.Size = 10
                .Cells(6, 4, 8, 4).Style.Font.Name = "Segoe UI"
                .Cells(6, 4, 8, 4).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 5, 8, 5).Value = "Result"
                .Cells(6, 5, 8, 5).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 5, 8, 5).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 5, 8, 5).Merge = True
                .Cells(6, 5, 8, 5).Style.Font.Size = 10
                .Cells(6, 5, 8, 5).Style.Font.Name = "Segoe UI"
                .Cells(6, 5, 8, 5).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 6, 8, 6).Value = "Part No"
                .Cells(6, 6, 8, 6).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 6, 8, 6).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 6, 8, 6).Style.WrapText = True
                .Cells(6, 6, 8, 6).Merge = True
                .Cells(6, 6, 8, 6).Style.Font.Size = 10
                .Cells(6, 6, 8, 6).Style.Font.Name = "Segoe UI"
                .Cells(6, 6, 8, 6).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 7, 8, 7).Value = "Part Name"
                .Cells(6, 7, 8, 7).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 7, 8, 7).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 7, 8, 7).Style.WrapText = True
                .Cells(6, 7, 8, 7).Merge = True
                .Cells(6, 7, 8, 7).Style.Font.Size = 10
                .Cells(6, 7, 8, 7).Style.Font.Name = "Segoe UI"
                .Cells(6, 7, 8, 7).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 8, 8, 8).Value = "Old Part No"
                .Cells(6, 8, 8, 8).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 8, 8, 8).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 8, 8, 8).Style.WrapText = True
                .Cells(6, 8, 8, 8).Merge = True
                .Cells(6, 8, 8, 8).Style.Font.Size = 10
                .Cells(6, 8, 8, 8).Style.Font.Name = "Segoe UI"
                .Cells(6, 8, 8, 8).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 9, 8, 9).Value = "Old Part Name"
                .Cells(6, 9, 8, 9).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 9, 8, 9).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 9, 8, 9).Style.WrapText = True
                .Cells(6, 9, 8, 9).Merge = True
                .Cells(6, 9, 8, 9).Style.Font.Size = 10
                .Cells(6, 9, 8, 9).Style.Font.Name = "Segoe UI"
                .Cells(6, 9, 8, 9).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 10, 8, 10).Value = "Lot No"
                .Cells(6, 10, 8, 10).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 10, 8, 10).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 10, 8, 10).Style.WrapText = True
                .Cells(6, 10, 8, 10).Merge = True
                .Cells(6, 10, 8, 10).Style.Font.Size = 10
                .Cells(6, 10, 8, 10).Style.Font.Name = "Segoe UI"
                .Cells(6, 10, 8, 10).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 11, 6, 16).Value = "Normal Condition"
                .Cells(6, 11, 6, 16).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 11, 6, 16).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 11, 6, 16).Merge = True
                .Cells(6, 11, 6, 16).Style.Font.Size = 10
                .Cells(6, 11, 6, 16).Style.Font.Name = "Segoe UI"
                .Cells(6, 11, 6, 16).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 11, 7, 13).Value = "Quality Engineerig Leader"
                .Cells(7, 11, 7, 13).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 11, 7, 13).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 11, 7, 13).Merge = True
                .Cells(7, 11, 7, 13).Style.Font.Size = 10
                .Cells(7, 11, 7, 13).Style.Font.Name = "Segoe UI"
                .Cells(7, 11, 7, 13).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 11, 8, 11).Value = "PIC"
                .Cells(8, 11, 8, 11).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 11, 8, 11).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 11, 8, 11).Merge = True
                .Cells(8, 11, 8, 11).Style.Font.Size = 10
                .Cells(8, 11, 8, 11).Style.Font.Name = "Segoe UI"
                .Cells(8, 11, 8, 11).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 12, 8, 12).Value = "Judgement"
                .Cells(8, 12, 8, 12).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 12, 8, 12).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 12, 8, 12).Merge = True
                .Cells(8, 12, 8, 12).Style.Font.Size = 10
                .Cells(8, 12, 8, 12).Style.Font.Name = "Segoe UI"
                .Cells(8, 12, 8, 12).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 13, 8, 13).Value = "Date"
                .Cells(8, 13, 8, 13).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 13, 8, 13).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 13, 8, 13).Merge = True
                .Cells(8, 13, 8, 13).Style.Font.Size = 10
                .Cells(8, 13, 8, 13).Style.Font.Name = "Segoe UI"
                .Cells(8, 13, 8, 13).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 14, 7, 16).Value = "Line Leader"
                .Cells(7, 14, 7, 16).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 14, 7, 16).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 14, 7, 16).Merge = True
                .Cells(7, 14, 7, 16).Style.Font.Size = 10
                .Cells(7, 14, 7, 16).Style.Font.Name = "Segoe UI"
                .Cells(7, 14, 7, 16).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 14, 8, 14).Value = "PIC"
                .Cells(8, 14, 8, 14).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 14, 8, 14).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 14, 8, 14).Merge = True
                .Cells(8, 14, 8, 14).Style.Font.Size = 10
                .Cells(8, 14, 8, 14).Style.Font.Name = "Segoe UI"
                .Cells(8, 14, 8, 14).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 15, 8, 15).Value = "Judgement"
                .Cells(8, 15, 8, 15).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 15, 8, 15).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 15, 8, 15).Merge = True
                .Cells(8, 15, 8, 15).Style.Font.Size = 10
                .Cells(8, 15, 8, 15).Style.Font.Name = "Segoe UI"
                .Cells(8, 15, 8, 15).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 16, 8, 16).Value = "Date"
                .Cells(8, 16, 8, 16).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 16, 8, 16).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 16, 8, 16).Merge = True
                .Cells(8, 16, 8, 16).Style.Font.Size = 10
                .Cells(8, 16, 8, 16).Style.Font.Name = "Segoe UI"
                .Cells(8, 16, 8, 16).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 17, 6, 22).Value = "Abnormal Condition"
                .Cells(6, 17, 6, 22).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 17, 6, 22).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 17, 6, 22).Merge = True
                .Cells(6, 17, 6, 22).Style.Font.Size = 10
                .Cells(6, 17, 6, 22).Style.Font.Name = "Segoe UI"
                .Cells(6, 17, 6, 22).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 17, 7, 19).Value = "Prod. Section Head"
                .Cells(7, 17, 7, 19).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 17, 7, 19).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 17, 7, 19).Merge = True
                .Cells(7, 17, 7, 19).Style.Font.Size = 10
                .Cells(7, 17, 7, 19).Style.Font.Name = "Segoe UI"
                .Cells(7, 17, 7, 19).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 17, 8, 17).Value = "PIC"
                .Cells(8, 17, 8, 17).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 17, 8, 17).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 17, 8, 17).Merge = True
                .Cells(8, 17, 8, 17).Style.Font.Size = 10
                .Cells(8, 17, 8, 17).Style.Font.Name = "Segoe UI"
                .Cells(8, 17, 8, 17).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 18, 8, 18).Value = "Judgement"
                .Cells(8, 18, 8, 18).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 18, 8, 18).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 18, 8, 18).Merge = True
                .Cells(8, 18, 8, 18).Style.Font.Size = 10
                .Cells(8, 18, 8, 18).Style.Font.Name = "Segoe UI"
                .Cells(8, 18, 8, 18).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 19, 8, 19).Value = "Date"
                .Cells(8, 19, 8, 19).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 19, 8, 19).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 19, 8, 19).Merge = True
                .Cells(8, 19, 8, 19).Style.Font.Size = 10
                .Cells(8, 19, 8, 19).Style.Font.Name = "Segoe UI"
                .Cells(8, 19, 8, 19).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 20, 7, 22).Value = "QE Section Head"
                .Cells(7, 20, 7, 22).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 20, 7, 22).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 20, 7, 22).Merge = True
                .Cells(7, 20, 7, 22).Style.Font.Size = 10
                .Cells(7, 20, 7, 22).Style.Font.Name = "Segoe UI"
                .Cells(7, 20, 7, 22).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 20, 8, 20).Value = "PIC"
                .Cells(8, 20, 8, 20).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 20, 8, 20).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 20, 8, 20).Merge = True
                .Cells(8, 20, 8, 20).Style.Font.Size = 10
                .Cells(8, 20, 8, 20).Style.Font.Name = "Segoe UI"
                .Cells(8, 20, 8, 20).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 21, 8, 21).Value = "Judgement"
                .Cells(8, 21, 8, 21).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 21, 8, 21).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 21, 8, 21).Merge = True
                .Cells(8, 21, 8, 21).Style.Font.Size = 10
                .Cells(8, 21, 8, 21).Style.Font.Name = "Segoe UI"
                .Cells(8, 21, 8, 21).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 22, 8, 22).Value = "Date"
                .Cells(8, 22, 8, 22).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 22, 8, 22).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 22, 8, 22).Merge = True
                .Cells(8, 22, 8, 22).Style.Font.Size = 10
                .Cells(8, 22, 8, 22).Style.Font.Name = "Segoe UI"
                .Cells(8, 22, 8, 22).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 23, 8, 23).Value = "Remark"
                .Cells(6, 23, 8, 23).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 23, 8, 23).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 23, 8, 23).Merge = True
                .Cells(6, 23, 8, 23).Style.Font.Size = 10
                .Cells(6, 23, 8, 23).Style.Font.Name = "Segoe UI"
                .Cells(6, 23, 8, 23).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 24, 8, 24).Value = "Create User"
                .Cells(6, 24, 8, 24).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 24, 8, 24).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 24, 8, 24).Merge = True
                .Cells(6, 24, 8, 24).Style.Font.Size = 10
                .Cells(6, 24, 8, 24).Style.Font.Name = "Segoe UI"
                .Cells(6, 24, 8, 24).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 25, 8, 25).Value = "Create Date"
                .Cells(6, 25, 8, 25).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 25, 8, 25).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 25, 8, 25).Merge = True
                .Cells(6, 25, 8, 25).Style.Font.Size = 10
                .Cells(6, 25, 8, 25).Style.Font.Name = "Segoe UI"
                .Cells(6, 25, 8, 25).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 26, 8, 26).Value = "Update User"
                .Cells(6, 26, 8, 26).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 26, 8, 26).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 26, 8, 26).Merge = True
                .Cells(6, 26, 8, 26).Style.Font.Size = 10
                .Cells(6, 26, 8, 26).Style.Font.Name = "Segoe UI"
                .Cells(6, 26, 8, 26).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 27, 8, 27).Value = "Update Date"
                .Cells(6, 27, 8, 27).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 27, 8, 27).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 27, 8, 27).Merge = True
                .Cells(6, 27, 8, 27).Style.Font.Size = 10
                .Cells(6, 27, 8, 27).Style.Font.Name = "Segoe UI"
                .Cells(6, 27, 8, 27).Style.Font.Color.SetColor(Color.White)


                For i = 0 To Rpt.Tables(0).Rows.Count - 1
                    .Cells(i + 9, 1, i + 9, 1).Value = Format(Rpt.Tables(0).Rows(i)("Date"), "dd-MM-yyyy")
                    .Cells(i + 9, 1, i + 9, 1).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 1, i + 9, 1).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 1, i + 9, 1).Style.WrapText = True
                    .Cells(i + 9, 1, i + 9, 1).Style.Font.Size = 10
                    .Cells(i + 9, 1, i + 9, 1).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 2, i + 9, 2).Value = Rpt.Tables(0).Rows(i)("LineID")
                    .Cells(i + 9, 2, i + 9, 2).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 2, i + 9, 2).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 2, i + 9, 2).Style.WrapText = True
                    .Cells(i + 9, 2, i + 9, 2).Style.Font.Size = 10
                    .Cells(i + 9, 2, i + 9, 2).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 3, i + 9, 3).Value = Rpt.Tables(0).Rows(i)("SubLineID")
                    .Cells(i + 9, 3, i + 9, 3).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 3, i + 9, 3).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 3, i + 9, 3).Style.WrapText = True
                    .Cells(i + 9, 3, i + 9, 3).Style.Font.Size = 10
                    .Cells(i + 9, 3, i + 9, 3).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 4, i + 9, 4).Value = Rpt.Tables(0).Rows(i)("MachineNo")
                    .Cells(i + 9, 4, i + 9, 4).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 4, i + 9, 4).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 4, i + 9, 4).Style.WrapText = True
                    .Cells(i + 9, 4, i + 9, 4).Style.Font.Size = 10
                    .Cells(i + 9, 4, i + 9, 4).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 5, i + 9, 5).Value = Rpt.Tables(0).Rows(i)("Judgement")
                    .Cells(i + 9, 5, i + 9, 5).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 5, i + 9, 5).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 5, i + 9, 5).Style.WrapText = True
                    .Cells(i + 9, 5, i + 9, 5).Style.Font.Size = 10
                    .Cells(i + 9, 5, i + 9, 5).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Judgement") = "NG" Then
                        .Cells(i + 9, 5, i + 9, 5).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 6, i + 9, 6).Value = Rpt.Tables(0).Rows(i)("PartID")
                    .Cells(i + 9, 6, i + 9, 6).Style.WrapText = True
                    .Cells(i + 9, 6, i + 9, 6).Style.Font.Size = 10
                    .Cells(i + 9, 6, i + 9, 6).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 7, i + 9, 7).Value = Rpt.Tables(0).Rows(i)("PartName")
                    .Cells(i + 9, 7, i + 9, 7).Style.WrapText = True
                    .Cells(i + 9, 7, i + 9, 7).Style.Font.Size = 10
                    .Cells(i + 9, 7, i + 9, 7).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 8, i + 9, 8).Value = Rpt.Tables(0).Rows(i)("OldPartID")
                    .Cells(i + 9, 8, i + 9, 8).Style.WrapText = True
                    .Cells(i + 9, 8, i + 9, 8).Style.Font.Size = 10
                    .Cells(i + 9, 8, i + 9, 8).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 9, i + 9, 9).Value = Rpt.Tables(0).Rows(i)("OldPartName")
                    .Cells(i + 9, 9, i + 9, 9).Style.WrapText = True
                    .Cells(i + 9, 9, i + 9, 9).Style.Font.Size = 10
                    .Cells(i + 9, 9, i + 9, 9).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 10, i + 9, 10).Value = Rpt.Tables(0).Rows(i)("LotNo")
                    .Cells(i + 9, 10, i + 9, 10).Style.WrapText = True
                    .Cells(i + 9, 10, i + 9, 10).Style.Font.Size = 10
                    .Cells(i + 9, 10, i + 9, 10).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 11, i + 9, 11).Value = Rpt.Tables(0).Rows(i)("ApprovalPIC1")
                    .Cells(i + 9, 11, i + 9, 11).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 11, i + 9, 11).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 11, i + 9, 11).Style.WrapText = True
                    .Cells(i + 9, 11, i + 9, 11).Style.Font.Size = 10
                    .Cells(i + 9, 11, i + 9, 11).Style.Font.Name = "Segoe UI"

                    If IsDBNull(Rpt.Tables(0).Rows(i)("ApprovalPIC1")) Then
                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    End If

                    .Cells(i + 9, 12, i + 9, 12).Value = Rpt.Tables(0).Rows(i)("ApprovalJudgement1")
                    .Cells(i + 9, 12, i + 9, 12).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 12, i + 9, 12).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 12, i + 9, 12).Style.WrapText = True
                    .Cells(i + 9, 12, i + 9, 12).Style.Font.Size = 10
                    .Cells(i + 9, 12, i + 9, 12).Style.Font.Name = "Segoe UI"

                    If IsDBNull(Rpt.Tables(0).Rows(i)("ApprovalJudgement1")) Then
                        .Cells(i + 9, 12, i + 9, 12).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 12, i + 9, 12).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    End If

                    .Cells(i + 9, 13, i + 9, 13).Value = Rpt.Tables(0).Rows(i)("ApprovalDate1")
                    .Cells(i + 9, 13, i + 9, 13).Style.Numberformat.Format = "dd-MM-yyyy"
                    .Cells(i + 9, 13, i + 9, 13).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 13, i + 9, 13).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 13, i + 9, 13).Style.WrapText = True
                    .Cells(i + 9, 13, i + 9, 13).Style.Font.Size = 10
                    .Cells(i + 9, 13, i + 9, 13).Style.Font.Name = "Segoe UI"

                    If IsDBNull(Rpt.Tables(0).Rows(i)("ApprovalDate1")) Then
                        .Cells(i + 9, 13, i + 9, 13).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 13, i + 9, 13).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    End If

                    .Cells(i + 9, 14, i + 9, 14).Value = Rpt.Tables(0).Rows(i)("ApprovalPIC2")
                    .Cells(i + 9, 14, i + 9, 14).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 14, i + 9, 14).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 14, i + 9, 14).Style.WrapText = True
                    .Cells(i + 9, 14, i + 9, 14).Style.Font.Size = 10
                    .Cells(i + 9, 14, i + 9, 14).Style.Font.Name = "Segoe UI"

                    If IsDBNull(Rpt.Tables(0).Rows(i)("ApprovalPIC2")) Then
                        .Cells(i + 9, 14, i + 9, 14).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 14, i + 9, 14).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    End If

                    .Cells(i + 9, 15, i + 9, 15).Value = Rpt.Tables(0).Rows(i)("ApprovalJudgement2")
                    .Cells(i + 9, 15, i + 9, 15).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 15, i + 9, 15).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 15, i + 9, 15).Style.WrapText = True
                    .Cells(i + 9, 15, i + 9, 15).Style.Font.Size = 10
                    .Cells(i + 9, 15, i + 9, 15).Style.Font.Name = "Segoe UI"

                    If IsDBNull(Rpt.Tables(0).Rows(i)("ApprovalJudgement2")) Then
                        .Cells(i + 9, 15, i + 9, 15).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 15, i + 9, 15).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    End If

                    .Cells(i + 9, 16, i + 9, 16).Value = Rpt.Tables(0).Rows(i)("ApprovalDate2")
                    .Cells(i + 9, 16, i + 9, 16).Style.Numberformat.Format = "dd-MM-yyyy"
                    .Cells(i + 9, 16, i + 9, 16).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 16, i + 9, 16).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 16, i + 9, 16).Style.WrapText = True
                    .Cells(i + 9, 16, i + 9, 16).Style.Font.Size = 10
                    .Cells(i + 9, 16, i + 9, 16).Style.Font.Name = "Segoe UI"

                    If IsDBNull(Rpt.Tables(0).Rows(i)("ApprovalDate2")) Then
                        .Cells(i + 9, 16, i + 9, 16).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 16, i + 9, 16).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    End If

                    .Cells(i + 9, 17, i + 9, 17).Value = Rpt.Tables(0).Rows(i)("ApprovalPIC3")
                    .Cells(i + 9, 17, i + 9, 17).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 17, i + 9, 17).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 17, i + 9, 17).Style.WrapText = True
                    .Cells(i + 9, 17, i + 9, 17).Style.Font.Size = 10
                    .Cells(i + 9, 17, i + 9, 17).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Judgement") = "NG" Then
                        .Cells(i + 9, 17, i + 9, 17).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 17, i + 9, 17).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    Else
                        .Cells(i + 9, 17, i + 9, 17).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 17, i + 9, 17).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                    End If

                    .Cells(i + 9, 18, i + 9, 18).Value = Rpt.Tables(0).Rows(i)("ApprovalJudgement3")
                    .Cells(i + 9, 18, i + 9, 18).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 18, i + 9, 18).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 18, i + 9, 18).Style.WrapText = True
                    .Cells(i + 9, 18, i + 9, 18).Style.Font.Size = 10
                    .Cells(i + 9, 18, i + 9, 18).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Judgement") = "NG" Then
                        .Cells(i + 9, 18, i + 9, 18).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 18, i + 9, 18).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    Else
                        .Cells(i + 9, 18, i + 9, 18).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 18, i + 9, 18).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                    End If

                    .Cells(i + 9, 19, i + 9, 19).Value = Rpt.Tables(0).Rows(i)("ApprovalDate3")
                    .Cells(i + 9, 19, i + 9, 19).Style.Numberformat.Format = "dd-MM-yyyy"
                    .Cells(i + 9, 19, i + 9, 19).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 19, i + 9, 19).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 19, i + 9, 19).Style.WrapText = True
                    .Cells(i + 9, 19, i + 9, 19).Style.Font.Size = 10
                    .Cells(i + 9, 19, i + 9, 19).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Judgement") = "NG" Then
                        .Cells(i + 9, 19, i + 9, 19).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 19, i + 9, 19).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    Else
                        .Cells(i + 9, 19, i + 9, 19).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 19, i + 9, 19).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                    End If

                    .Cells(i + 9, 20, i + 9, 20).Value = Rpt.Tables(0).Rows(i)("ApprovalPIC4")
                    .Cells(i + 9, 20, i + 9, 20).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 20, i + 9, 20).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 20, i + 9, 20).Style.WrapText = True
                    .Cells(i + 9, 20, i + 9, 20).Style.Font.Size = 10
                    .Cells(i + 9, 20, i + 9, 20).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Judgement") = "NG" Then
                        .Cells(i + 9, 20, i + 9, 20).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 20, i + 9, 20).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    Else
                        .Cells(i + 9, 20, i + 9, 20).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 20, i + 9, 20).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                    End If

                    .Cells(i + 9, 21, i + 9, 21).Value = Rpt.Tables(0).Rows(i)("ApprovalJudgement4")
                    .Cells(i + 9, 21, i + 9, 21).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 21, i + 9, 21).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 21, i + 9, 21).Style.WrapText = True
                    .Cells(i + 9, 21, i + 9, 21).Style.Font.Size = 10
                    .Cells(i + 9, 21, i + 9, 21).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Judgement") = "NG" Then
                        .Cells(i + 9, 21, i + 9, 21).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 21, i + 9, 21).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    Else
                        .Cells(i + 9, 21, i + 9, 21).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 21, i + 9, 21).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                    End If

                    .Cells(i + 9, 22, i + 9, 22).Value = Rpt.Tables(0).Rows(i)("ApprovalDate4")
                    .Cells(i + 9, 22, i + 9, 22).Style.Numberformat.Format = "dd-MM-yyyy"
                    .Cells(i + 9, 22, i + 9, 22).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 22, i + 9, 22).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 22, i + 9, 22).Style.WrapText = True
                    .Cells(i + 9, 22, i + 9, 22).Style.Font.Size = 10
                    .Cells(i + 9, 22, i + 9, 22).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Judgement") = "NG" Then
                        .Cells(i + 9, 22, i + 9, 22).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 22, i + 9, 22).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    Else
                        .Cells(i + 9, 22, i + 9, 22).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 22, i + 9, 22).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
                    End If

                    .Cells(i + 9, 23, i + 9, 23).Value = Rpt.Tables(0).Rows(i)("Remark")
                    .Cells(i + 9, 23, i + 9, 23).Style.WrapText = True
                    .Cells(i + 9, 23, i + 9, 23).Style.Font.Size = 10
                    .Cells(i + 9, 23, i + 9, 23).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 24, i + 9, 24).Value = Rpt.Tables(0).Rows(i)("CreateUser")
                    .Cells(i + 9, 24, i + 9, 24).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 24, i + 9, 24).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 24, i + 9, 24).Style.WrapText = True
                    .Cells(i + 9, 24, i + 9, 24).Style.Font.Size = 10
                    .Cells(i + 9, 24, i + 9, 24).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 25, i + 9, 25).Value = Rpt.Tables(0).Rows(i)("CreateDate")
                    .Cells(i + 9, 25, i + 9, 25).Style.Numberformat.Format = "dd-MM-yyyy"
                    .Cells(i + 9, 25, i + 9, 25).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 25, i + 9, 25).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 25, i + 9, 25).Style.WrapText = True
                    .Cells(i + 9, 25, i + 9, 25).Style.Font.Size = 10
                    .Cells(i + 9, 25, i + 9, 25).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 26, i + 9, 26).Value = Rpt.Tables(0).Rows(i)("UpdateUser")
                    .Cells(i + 9, 26, i + 9, 26).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 26, i + 9, 26).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 26, i + 9, 26).Style.WrapText = True
                    .Cells(i + 9, 26, i + 9, 26).Style.Font.Size = 10
                    .Cells(i + 9, 26, i + 9, 26).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 27, i + 9, 27).Value = Rpt.Tables(0).Rows(i)("UpdateDate")
                    .Cells(i + 9, 27, i + 9, 27).Style.Numberformat.Format = "dd-MM-yyyy"
                    .Cells(i + 9, 27, i + 9, 27).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 27, i + 9, 27).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 27, i + 9, 27).Style.WrapText = True
                    .Cells(i + 9, 27, i + 9, 27).Style.Font.Size = 10
                    .Cells(i + 9, 27, i + 9, 27).Style.Font.Name = "Segoe UI"

                Next
                FormatExcel(ws, Rpt)
                InsertHeader(ws)
            End With

            exl.Save()
            DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("Download/" & fi.Name)
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        End Try
    End Sub

    Private Sub InsertHeader(ByVal pExl As ExcelWorksheet)
        With pExl
            .Cells(1, 1, 1, 1).Value = "Change Item Check Sheet Result Approval"
            .Cells(1, 1, 1, 27).Merge = True
            .Cells(1, 1, 1, 27).Style.HorizontalAlignment = HorzAlignment.Far
            .Cells(1, 1, 1, 27).Style.VerticalAlignment = VertAlignment.Center
            .Cells(1, 1, 1, 27).Style.Font.Bold = True
            .Cells(1, 1, 1, 27).Style.Font.Size = 16
            .Cells(1, 1, 1, 27).Style.Font.Name = "Segoe UI"

            .Cells(2, 1, 2, 1).Value = "Date From"
            .Cells(2, 1, 2, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(2, 1, 2, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(2, 1, 2, 1).Style.Font.Size = 10
            .Cells(2, 1, 2, 1).Style.Font.Name = "Segoe UI"

            .Cells(2, 3, 2, 3).Value = ": " & Format(dtstart.Value, "dd-MMM-yyyy")
            .Cells(2, 3, 2, 3).Merge = True
            .Cells(2, 3, 2, 3).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(2, 3, 2, 3).Style.VerticalAlignment = VertAlignment.Center
            .Cells(2, 3, 2, 3).Style.Font.Size = 10
            .Cells(2, 3, 2, 3).Style.Font.Name = "Segoe UI"

            .Cells(3, 1, 3, 1).Value = "To"
            .Cells(3, 1, 3, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 1, 3, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 1, 3, 1).Style.Font.Size = 10
            .Cells(3, 1, 3, 1).Style.Font.Name = "Segoe UI"

            .Cells(3, 3, 3, 3).Value = ": " & Format(dtend.Value, "dd-MMM-yyyy")
            .Cells(3, 3, 3, 3).Merge = True
            .Cells(3, 3, 3, 3).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 3, 3, 3).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 3, 3, 3).Style.Font.Size = 10
            .Cells(3, 3, 3, 3).Style.Font.Name = "Segoe UI"

            .Cells(2, 5, 2, 5).Value = "Line No"
            .Cells(2, 5, 2, 5).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(2, 5, 2, 5).Style.VerticalAlignment = VertAlignment.Center
            .Cells(2, 5, 2, 5).Style.Font.Size = 10
            .Cells(2, 5, 2, 5).Style.Font.Name = "Segoe UI"

            .Cells(2, 7, 2, 7).Value = ": " & cbolineid.Value
            .Cells(2, 7, 2, 7).Merge = True
            .Cells(2, 7, 2, 7).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(2, 7, 2, 7).Style.VerticalAlignment = VertAlignment.Center
            .Cells(2, 7, 2, 7).Style.Font.Size = 10
            .Cells(2, 7, 2, 7).Style.Font.Name = "Segoe UI"

            .Cells(3, 5, 3, 5).Value = "Sub Line No"
            .Cells(3, 5, 3, 5).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 5, 3, 5).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 5, 3, 5).Style.Font.Size = 10
            .Cells(3, 5, 3, 5).Style.Font.Name = "Segoe UI"

            .Cells(3, 7, 3, 7).Value = ": " & cbosublineid.Value
            .Cells(3, 7, 3, 7).Merge = True
            .Cells(3, 7, 3, 7).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 7, 3, 7).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 7, 3, 7).Style.Font.Size = 10
            .Cells(3, 7, 3, 7).Style.Font.Name = "Segoe UI"

            .Cells(2, 8, 2, 8).Value = "Machine No"
            .Cells(2, 8, 2, 8).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(2, 8, 2, 8).Style.VerticalAlignment = VertAlignment.Center
            .Cells(2, 8, 2, 8).Style.Font.Size = 10
            .Cells(2, 8, 2, 8).Style.Font.Name = "Segoe UI"

            .Cells(2, 9, 2, 9).Value = ": " & cbomachineno.Value
            .Cells(2, 9, 2, 9).Merge = True
            .Cells(2, 9, 2, 9).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(2, 9, 2, 9).Style.VerticalAlignment = VertAlignment.Center
            .Cells(2, 9, 2, 9).Style.Font.Size = 10
            .Cells(2, 9, 2, 9).Style.Font.Name = "Segoe UI"

            .Cells(3, 8, 3, 8).Value = "Judgement"
            .Cells(3, 8, 3, 8).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 8, 3, 8).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 8, 3, 8).Style.Font.Size = 10
            .Cells(3, 8, 3, 8).Style.Font.Name = "Segoe UI"

            .Cells(3, 9, 3, 9).Value = ": " & cbojudgement.Value
            .Cells(3, 9, 3, 9).Merge = True
            .Cells(3, 9, 3, 9).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 9, 3, 9).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 9, 3, 9).Style.Font.Size = 10
            .Cells(3, 9, 3, 9).Style.Font.Name = "Segoe UI"

            .Cells(2, 10, 2, 10).Value = "Part No"
            .Cells(2, 10, 2, 10).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(2, 10, 2, 10).Style.VerticalAlignment = VertAlignment.Center
            .Cells(2, 10, 2, 10).Style.Font.Size = 10
            .Cells(2, 10, 2, 10).Style.Font.Name = "Segoe UI"

            .Cells(2, 12, 2, 12).Value = ": " & cbopartid.Value
            .Cells(2, 12, 2, 12).Merge = True
            .Cells(2, 12, 2, 12).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(2, 12, 2, 12).Style.VerticalAlignment = VertAlignment.Center
            .Cells(2, 12, 2, 12).Style.Font.Size = 10
            .Cells(2, 12, 2, 12).Style.Font.Name = "Segoe UI"

            .Cells(3, 10, 3, 10).Value = "Part Name"
            .Cells(3, 10, 3, 10).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 10, 3, 10).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 10, 3, 10).Style.Font.Size = 10
            .Cells(3, 10, 3, 10).Style.Font.Name = "Segoe UI"

            .Cells(3, 12, 3, 12).Value = ": " & txtpartname.Value
            .Cells(3, 12, 3, 12).Merge = True
            .Cells(3, 12, 3, 12).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 12, 3, 12).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 12, 3, 12).Style.Font.Size = 10
            .Cells(3, 12, 3, 12).Style.Font.Name = "Segoe UI"
        End With
    End Sub

    Private Sub FormatExcel(ByVal pExl As ExcelWorksheet, ByVal pRpt As DataSet)
        With pExl
            .Column(1).Width = 13 'Date
            .Column(2).Width = 8 'Line ID
            .Column(3).Width = 12 'Sub Line ID
            .Column(4).Width = 12 'Machine No
            .Column(5).Width = 8 'Result
            .Column(6).Width = 15 'Part No
            .Column(7).Width = 25 'Part Name
            .Column(8).Width = 15 'Old Part No
            .Column(9).Width = 25 'Old Part Name
            .Column(10).Width = 10 'Lot No

            .Column(11).Width = 13 'PIC 1
            .Column(12).Width = 13 'Judgement 1
            .Column(13).Width = 13 'Date 1

            .Column(14).Width = 13 'PIC 2
            .Column(15).Width = 13 'Judgement 2
            .Column(16).Width = 13 'Date 2

            .Column(17).Width = 13 'PIC 3
            .Column(18).Width = 13 'Judgement 3
            .Column(19).Width = 13 'Date 3

            .Column(20).Width = 13 'PIC 4
            .Column(21).Width = 13 'Judgement 4
            .Column(22).Width = 13 'Date 4

            .Column(23).Width = 30 'Remark

            .Column(24).Width = 13 'Create User
            .Column(25).Width = 13 'Create Date
            .Column(26).Width = 13 'Update User
            .Column(27).Width = 13 'Update Date

            Dim rgAll As ExcelRange = .Cells(6, 1, pRpt.Tables(0).Rows.Count + 8, 27)
            DrawAllBorders(rgAll)

            Dim rgHeader As ExcelRange = .Cells(6, 1, 8, 27)
            rgHeader.Style.Fill.PatternType = Style.ExcelFillStyle.Solid
            rgHeader.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
            rgHeader.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
            rgHeader.Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center
        End With
    End Sub

    Private Sub DrawAllBorders(ByVal Rg As ExcelRange)
        With Rg
            .Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
            .Style.Border.Bottom.Style = Style.ExcelBorderStyle.Thin
            .Style.Border.Left.Style = Style.ExcelBorderStyle.Thin
            .Style.Border.Right.Style = Style.ExcelBorderStyle.Thin
            .Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
        End With
    End Sub
#End Region

    
End Class