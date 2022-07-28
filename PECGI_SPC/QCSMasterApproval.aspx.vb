Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports OfficeOpenXml

Public Class QCSMasterApproval
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
    Public StatusApproval1 As String
    Public StatusApproval2 As String
    Public StatusApproval3 As String
    'Public ValueType As String
#End Region

#Region "Procedure"
    Private Sub up_fillcombopart()
        Dim ErrMsg As String = ""
        Dim dsline As DataTable
        dsline = ClsQCSApprovalDB.GetDataPart(pUser, ErrMsg)
        If ErrMsg = "" Then
            cbopartid.DataSource = dsline
            cbopartid.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    'Private Sub up_fillcomboline(ByVal pStstus As String, ByVal pUser As String)
    '    Dim ErrMsg As String = ""
    '    Dim dsline As DataTable
    '    dsline = ClsQCSApprovalDB.GetDataLine(pUser, pStstus, ErrMsg)
    '    If ErrMsg = "" Then
    '        cbolineid.DataSource = dsline
    '        cbolineid.DataBind()
    '    Else
    '        show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
    '        Exit Sub
    '    End If
    'End Sub


    Private Sub up_GridLoadMenu(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String, ByVal pStatusApp As String, ByVal pStatus As String, ByVal pStartDate As String, ByVal pEndDate As String)

        Dim ErrMsg As String = ""
        Dim Menu As DataSet
        Menu = ClsQCSApprovalDB.GetListList(pUser, pLineID, pPartID, pRevNo, pStatusApp, pStatus, pStartDate, pEndDate, ErrMsg)
        If ErrMsg = "" Then
            GridMenu.DataSource = Menu
            GridMenu.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    'Private Sub up_GridLoadMenuView(ByVal pPartID As String, ByVal pRevNo As String)
    '    Dim ErrMsg As String = ""
    '    Dim Menu As DataSet
    '    Menu = ClsQCSMasterDetailDB.GetList(pPartID, pRevNo, ErrMsg)
    '    If ErrMsg = "" Then
    '        Grid.DataSource = Menu
    '        Grid.DataBind()
    '    Else
    '        show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
    '        Exit Sub
    '    End If
    'End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        GridMenu.JSProperties("cp_message") = ErrMsg
        GridMenu.JSProperties("cp_type") = msgType
        GridMenu.JSProperties("cp_val") = pVal
    End Sub

    Private Sub Approval1(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String)
        Dim pErr As String = ""
        Dim QCSApprove As New ClsQCSMaster
        QCSApprove.ApprovalPIC1 = pUser
        QCSApprove.LineID = pLineID
        QCSApprove.PartID = pPartID
        QCSApprove.RevNo = pRevNo
        ClsQCSMasterDB.Approve1(QCSApprove, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Approve successfully!", 1)
        End If
    End Sub

    Private Sub Approval2(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String)
        Dim pErr As String = ""
        Dim QCSApprove As New ClsQCSMaster
        QCSApprove.ApprovalPIC2 = pUser
        QCSApprove.LineID = pLineID
        QCSApprove.PartID = pPartID
        QCSApprove.RevNo = pRevNo
        ClsQCSMasterDB.Approve2(QCSApprove, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Approve successfully!", 1)
        End If
    End Sub

    Private Sub Approval3(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String)
        Dim pErr As String = ""
        Dim QCSApprove As New ClsQCSMaster
        QCSApprove.ApprovalPIC3 = pUser
        QCSApprove.LineID = pLineID
        QCSApprove.PartID = pPartID
        QCSApprove.RevNo = pRevNo
        ClsQCSMasterDB.Approve3(QCSApprove, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Approve successfully!", 1)
        End If
    End Sub

    Private Sub UpdateStatus(ByVal pLineID As String, ByVal pPartID As String, ByVal pRevNo As String)
        Dim pErr As String = ""

        Dim QCSActive As New ClsQCSMaster
        QCSActive.LineID = pLineID
        QCSActive.PartID = pPartID
        QCSActive.RevNo = pRevNo
        ClsQCSMasterDB.UpdStatus(QCSActive, pErr)

        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        End If
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "B020")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If
        sGlobal.getMenu("B020 ")
        Master.SiteTitle = sGlobal.menuName
        pUser = Session("user")
        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B020 ")
        show_error(MsgTypeEnum.Info, "", 0)

        If ClsQCSMasterDB.GetDataQE(pUser, "") = True Or ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True Or ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True Then
            btnApprove.ClientEnabled = True
        Else
            btnApprove.ClientEnabled = False
        End If
        up_fillcombopart()
        up_GridLoadMenu(cbolineid.Value, cbopartid.Value, cborevno.Value, cboapprovalstatus.Value, 0, dtstart.Value, dtend.Value)


        If ClsQCSMasterDB.GetDataQELeader(pUser) = True Then
            StatusApproval1 = 0
            StatusApproval2 = 0
            StatusApproval3 = 1
        ElseIf ClsQCSMasterDB.GetDataForemanLeader(pUser) = True Then
            StatusApproval1 = 0
            StatusApproval2 = 1
            StatusApproval3 = 0
        ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser) = True Then
            StatusApproval1 = 1
            StatusApproval2 = 0
            StatusApproval3 = 0
        End If
        Dim Menu As DataSet
        Menu = ClsQCSApprovalDB.GetStartDate(IIf(pUser = Nothing, "", pUser), IIf(StatusApproval1 = Nothing, "", StatusApproval1), IIf(StatusApproval2 = Nothing, "", StatusApproval2), IIf(StatusApproval3 = Nothing, "", StatusApproval3))
        If Menu.Tables(0).Rows.Count = 1 And Not IsCallback Then
            dtstart.Value = Menu.Tables(0).Rows(0)("RevDate")
        ElseIf Menu.Tables(0).Rows.Count <> 1 And Not IsCallback Then
            dtstart.Value = Date.Now()
        End If
    End Sub


#Region "GridMenu"
    Protected Sub GridMenu_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles GridMenu.AfterPerformCallback
        Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            up_GridLoadMenu(cbolineid.Value, cbopartid.Value, cborevno.Value, cboapprovalstatus.Value, 0, dtstart.Value, dtend.Value)
        End If
    End Sub

    Private Sub GridMenu_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridMenu.BeforeGetCallbackResult
        up_GridLoadMenu(cbolineid.Value, cbopartid.Value, cborevno.Value, cboapprovalstatus.Value, 0, dtstart.Value, dtend.Value)
    End Sub

    Private Sub GridMenu_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles GridMenu.CellEditorInitialize

    End Sub

    Private Sub GridMenu_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridMenu.CustomCallback
        Dim pFunction As String = Split(e.Parameters, "|")(0)

        Select Case pFunction
            Case "Refresh"
                Dim pLine As String = Split(e.Parameters, "|")(1)
                Dim pPart As String = Split(e.Parameters, "|")(2)
                Dim pRev As String = Split(e.Parameters, "|")(3)
                Dim pStatus As String = Split(e.Parameters, "|")(4)
                'If ClsQCSMasterDB.GetDataQE(pUser, "") = True Then
                '    up_GridLoadMenu(cbolineid.Text, cbopartid.Text, cborevno.Text, cboapprovalstatus.Text, 1)
                'Else
                '    up_GridLoadMenu(cbolineid.Text, cbopartid.Text, cborevno.Text, cboapprovalstatus.Text, 0)
                'End If
                up_GridLoadMenu(cbolineid.Value, cbopartid.Value, cborevno.Value, cboapprovalstatus.Value, 0, dtstart.Value, dtend.Value)
                'up_GridLoadMenu(pLine, pPart, pRev, pStatus)

            Case "ClearGrid"
                Dim dsMenu As DataTable

                dsMenu = ClsQCSApprovalDB.GetDataPart(pUser, "")
                cbopartid.DataSource = dsMenu
                cbopartid.DataBind()
                up_GridLoadMenu("", "", "", "", "", "", "")

            Case "Approve"
                Dim table As DataTable = Nothing
                table = DirectCast(Session("table"), DataTable)
                Dim selectItems As List(Of Object) = GridMenu.GetSelectedFieldValues("PartID;LineID;RevNo")
                If selectItems.Count = 0 Then
                    show_error(MsgTypeEnum.Warning, "Please select data!", 1)
                Else
                    For Each selectItemId As Object In selectItems
                        Dim pPart As String = Split(selectItemId, "|")(0)
                        Dim pLineID As String = Split(selectItemId, "|")(1)
                        Dim pRevNo As String = Split(selectItemId, "|")(2)

                        If ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataQELeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(pLineID, pPart, pRevNo, "") = True And ClsQCSMasterDB.GetDataApprovalForeman(pLineID, pPart, pRevNo, "") = True And ClsQCSMasterDB.GetDataApprovalQE(pLineID, pPart, pRevNo, "") = True Then
                            UpdateStatus(pLineID, pPart, pRevNo)
                            Approval1(pLineID, pPart, pRevNo)
                            Approval2(pLineID, pPart, pRevNo)
                            Approval3(pLineID, pPart, pRevNo)
                        ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataQELeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(pLineID, pPart, pRevNo, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(pLineID, pPart, pRevNo, "") = True And ClsQCSMasterDB.GetDataApprovalQE(pLineID, pPart, pRevNo, "") = True Then
                            UpdateStatus(pLineID, pPart, pRevNo)
                            Approval2(pLineID, pPart, pRevNo)
                            Approval3(pLineID, pPart, pRevNo)
                        ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataQELeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(pLineID, pPart, pRevNo, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(pLineID, pPart, pRevNo, "") = False And ClsQCSMasterDB.GetDataApprovalQE(pLineID, pPart, pRevNo, "") = True Then
                            UpdateStatus(pLineID, pPart, pRevNo)
                            Approval3(pLineID, pPart, pRevNo)

                        ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(pLineID, pPart, pRevNo, "") = True And ClsQCSMasterDB.GetDataApprovalForeman(pLineID, pPart, pRevNo, "") = True And ClsQCSMasterDB.GetDataApprovalQE(pLineID, pPart, pRevNo, "") = True Then
                            Approval1(pLineID, pPart, pRevNo)
                            Approval2(pLineID, pPart, pRevNo)
                        ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(pLineID, pPart, pRevNo, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(pLineID, pPart, pRevNo, "") = True And ClsQCSMasterDB.GetDataApprovalQE(pLineID, pPart, pRevNo, "") = True Then
                            Approval2(pLineID, pPart, pRevNo)

                        ElseIf ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataQELeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(pLineID, pPart, pRevNo, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(pLineID, pPart, pRevNo, "") = True And ClsQCSMasterDB.GetDataApprovalQE(pLineID, pPart, pRevNo, "") = True Then
                            UpdateStatus(pLineID, pPart, pRevNo)
                            Approval2(pLineID, pPart, pRevNo)
                            Approval3(pLineID, pPart, pRevNo)
                        ElseIf ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataQELeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(pLineID, pPart, pRevNo, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(pLineID, pPart, pRevNo, "") = False And ClsQCSMasterDB.GetDataApprovalQE(pLineID, pPart, pRevNo, "") = True Then
                            UpdateStatus(pLineID, pPart, pRevNo)
                            Approval3(pLineID, pPart, pRevNo)

                        ElseIf ClsQCSMasterDB.GetDataLineLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(pLineID, pPart, pRevNo, "") = True And ClsQCSMasterDB.GetDataApprovalForeman(pLineID, pPart, pRevNo, "") = True And ClsQCSMasterDB.GetDataApprovalQE(pLineID, pPart, pRevNo, "") = True Then
                            Approval1(pLineID, pPart, pRevNo)

                        ElseIf ClsQCSMasterDB.GetDataForemanLeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(pLineID, pPart, pRevNo, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(pLineID, pPart, pRevNo, "") = True And ClsQCSMasterDB.GetDataApprovalQE(pLineID, pPart, pRevNo, "") = True Then
                            Approval2(pLineID, pPart, pRevNo)

                        ElseIf ClsQCSMasterDB.GetDataQELeader(pUser, "") = True And ClsQCSMasterDB.GetDataApprovalLine(pLineID, pPart, pRevNo, "") = False And ClsQCSMasterDB.GetDataApprovalForeman(pLineID, pPart, pRevNo, "") = False And ClsQCSMasterDB.GetDataApprovalQE(pLineID, pPart, pRevNo, "") = True Then
                            UpdateStatus(pLineID, pPart, pRevNo)
                            Approval3(pLineID, pPart, pRevNo)
                        End If
                    Next selectItemId
                    'up_GridLoadMenu()
                End If
            Case "ViewQCS"
                '    GridMenu.JSProperties("cp_viewqcs") = 1
                GridMenu.JSProperties("cp_viewqcs") = "1"
                GridMenu.JSProperties("cp_lineid") = GridMenu.GetSelectedFieldValues("LineID")(0).ToString
                GridMenu.JSProperties("cp_partid") = GridMenu.GetSelectedFieldValues("PartID")(0).ToString
                GridMenu.JSProperties("cp_partname") = GridMenu.GetSelectedFieldValues("PartName")(0).ToString
                GridMenu.JSProperties("cp_revno") = GridMenu.GetSelectedFieldValues("RevNo")(0).ToString
                '    GridMenu.JSProperties("cp_revdate") = Format(GridMenu.GetSelectedFieldValues("RevDate")(0), "dd MMM yyyy")
                '    GridMenu.JSProperties("cp_revhistory") = GridMenu.GetSelectedFieldValues("RevHistory")(0).ToString
                '    GridMenu.JSProperties("cp_preparedby") = GridMenu.GetSelectedFieldValues("PreparedBy")(0).ToString

                '    GridMenu.JSProperties("cp_callgrid") = 1

                'up_GridLoadMenuView(GridMenu.GetSelectedFieldValues("PartID")(0).ToString, GridMenu.GetSelectedFieldValues("RevNo")(0).ToString)


            Case "excel"
                Dim pLineID As String = Split(e.Parameters, "|")(1)
                Dim pPartID As String = Split(e.Parameters, "|")(2)
                Dim pPartName As String = Split(e.Parameters, "|")(3)
                Dim pRevNo As String = Split(e.Parameters, "|")(4)
                Dim pApprovalStatus As String = Split(e.Parameters, "|")(5)
                Dim pStatus As String
                If ClsQCSMasterDB.GetDataQE(pUser, "") = True Then
                    pStatus = 1
                Else
                    pStatus = 0
                End If
                up_Excel(pLineID, pPartID, pPartName, pRevNo, pApprovalStatus, pStatus, "")
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


#End Region

#Region "Grid"
    Protected Sub Grid_AfterPerformCallback(sender As Object, e As DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs) Handles Grid.AfterPerformCallback
        Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
        If e.CallbackName <> "CANCELEDIT" And e.CallbackName <> "CUSTOMCALLBACK" Then
            'up_GridLoadMenuView(txtpartidview.Text, txtrevnoview.Text)
        End If
    End Sub

    Private Sub Grid_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.BeforeGetCallbackResult
        'up_GridLoadMenuView(txtpartidview.Text, txtrevnoview.Text)
    End Sub

    Private Sub Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Grid.CustomCallback
        Dim pFunction As String = Split(e.Parameters, "|")(0)

        Select Case pFunction
            Case "ViewQCS"
                'up_GridLoadMenuView(txtpartidview.Text, txtrevnoview.Text)
        End Select
    End Sub
#End Region

#Region "Other"
    Private Sub cbopartid_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbopartid.Callback
        Dim pParam As String = Split(e.Parameter, "|")(1)
        Dim dsMenu As DataTable
        'If pParam = "ALL" Then
        '    'If ClsQCSMasterDB.GetDataQE(pUser, "") = True Then
        '    '    dsMenu = ClsQCSApprovalDB.GetDataPart(pUser, pParam, 2, "")
        '    'Else
        '    '    dsMenu = ClsQCSApprovalDB.GetDataPart(pUser, pParam, 1, "")
        '    'End If
        'Else
        '    dsMenu = ClsQCSApprovalDB.GetDataPart(pUser, pParam, 0, "")
        'End If
        dsMenu = ClsQCSApprovalDB.GetDataPart(pUser, "")
        cbopartid.DataSource = dsMenu
        cbopartid.DataBind()
    End Sub

    Private Sub cbolineid_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbolineid.Callback
        Dim pParam As String = Split(e.Parameter, "|")(1)
        Dim dsMenu As DataTable
        dsMenu = ClsQCSApprovalDB.GetDataLine(pUser, pParam, "")
        cbolineid.DataSource = dsMenu
        cbolineid.DataBind()
    End Sub

    Private Sub cborevno_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cborevno.Callback
        Dim pPartID As String = Split(e.Parameter, "|")(1)
        Dim pLineID As String = Split(e.Parameter, "|")(2)
        Dim dsMenu As DataTable
        If pPartID = "ALL" Or pLineID = "ALL" Then
            dsMenu = ClsQCSApprovalDB.GetDataRev(pPartID, pLineID, 1, "")
        Else
            dsMenu = ClsQCSApprovalDB.GetDataRev(pPartID, pLineID, 0, "")
        End If
        cborevno.DataSource = dsMenu
        cborevno.DataBind()
    End Sub

    Private Sub cboapprovalstatus_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cboapprovalstatus.Callback
        Dim pSelect As String = Split(e.Parameter, "|")(0)

        Dim dsMenu As DataTable
        If pSelect = "Select" Then
            dsMenu = Nothing
            cboapprovalstatus.DataSource = dsMenu
            cboapprovalstatus.DataBind()
        Else
            dsMenu = ClsQCSApprovalDB.GetStatus("")
            cboapprovalstatus.DataSource = dsMenu
            cboapprovalstatus.DataBind()
        End If
    End Sub
#End Region


    Private Sub GridMenu_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles GridMenu.HtmlDataCellPrepared
        If (e.DataColumn.FieldName = "ApprovalDate1") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            End If
        End If
        If (e.DataColumn.FieldName = "ApprovalPIC1") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            End If
        End If
        If (e.DataColumn.FieldName = "ApprovalDate2") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            End If
        End If
        If (e.DataColumn.FieldName = "ApprovalPIC2") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            End If
        End If
        If (e.DataColumn.FieldName = "ApprovalDate3") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            End If
        End If
        If (e.DataColumn.FieldName = "ApprovalPIC3") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            End If
        End If
    End Sub

#Region "Download To Excel"
    Private Sub up_Excel(ByVal pLineID As String, ByVal pPartID As String, ByVal pPartName As String, ByVal pRevNo As String, ByVal pApprovalStatus As String, ByVal pStatus As String, Optional ByRef pErr As String = "")
        Try
            Dim fi As New FileInfo(Server.MapPath("~\Download\Statistics Quality Control Approval.xlsx"))
            If fi.Exists Then
                fi.Delete()
                fi = New FileInfo(Server.MapPath("~\Download\Statistics Quality Control Approval.xlsx"))
            End If
            Dim exl As New ExcelPackage(fi)
            Dim ws As ExcelWorksheet
            ws = exl.Workbook.Worksheets.Add("Sheet1")
            ws.View.ShowGridLines = False

            Dim Rpt As DataSet
            Rpt = ClsQCSApprovalDB.GetListList(pUser, pLineID, pPartID, pRevNo, pApprovalStatus, pStatus, dtstart.Value, dtend.Value, pErr)

            With ws
                .Cells(6, 1, 8, 1).Value = "Part No"
                .Cells(6, 1, 8, 1).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 1, 8, 1).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 1, 8, 1).Merge = True
                .Cells(6, 1, 8, 1).Style.Font.Size = 10
                .Cells(6, 1, 8, 1).Style.Font.Name = "Segoe UI"
                .Cells(6, 1, 8, 1).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 2, 8, 2).Value = "Part Name"
                .Cells(6, 2, 8, 2).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 2, 8, 2).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 2, 8, 2).Merge = True
                .Cells(6, 2, 8, 2).Style.Font.Size = 10
                .Cells(6, 2, 8, 2).Style.Font.Name = "Segoe UI"
                .Cells(6, 2, 8, 2).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 3, 8, 3).Value = "Line No"
                .Cells(6, 3, 8, 3).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 3, 8, 3).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 3, 8, 3).Merge = True
                .Cells(6, 3, 8, 3).Style.Font.Size = 10
                .Cells(6, 3, 8, 3).Style.Font.Name = "Segoe UI"
                .Cells(6, 3, 8, 3).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 4, 8, 4).Value = "Rev No"
                .Cells(6, 4, 8, 4).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 4, 8, 4).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 4, 8, 4).Merge = True
                .Cells(6, 4, 8, 4).Style.Font.Size = 10
                .Cells(6, 4, 8, 4).Style.Font.Name = "Segoe UI"
                .Cells(6, 4, 8, 4).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 5, 8, 5).Value = "Rev Date"
                .Cells(6, 5, 8, 5).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 5, 8, 5).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 5, 8, 5).Merge = True
                .Cells(6, 5, 8, 5).Style.Font.Size = 10
                .Cells(6, 5, 8, 5).Style.Font.Name = "Segoe UI"
                .Cells(6, 5, 8, 5).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 6, 8, 6).Value = "Rev History"
                .Cells(6, 6, 8, 6).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 6, 8, 6).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 6, 8, 6).Style.WrapText = True
                .Cells(6, 6, 8, 6).Merge = True
                .Cells(6, 6, 8, 6).Style.Font.Size = 10
                .Cells(6, 6, 8, 6).Style.Font.Name = "Segoe UI"
                .Cells(6, 6, 8, 6).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 7, 8, 7).Value = "Prepared By"
                .Cells(6, 7, 8, 7).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 7, 8, 7).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 7, 8, 7).Style.WrapText = True
                .Cells(6, 7, 8, 7).Merge = True
                .Cells(6, 7, 8, 7).Style.Font.Size = 10
                .Cells(6, 7, 8, 7).Style.Font.Name = "Segoe UI"
                .Cells(6, 7, 8, 7).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 8, 6, 13).Value = "Approval"
                .Cells(6, 8, 6, 13).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 8, 6, 13).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 8, 6, 13).Merge = True
                .Cells(6, 8, 6, 13).Style.Font.Size = 10
                .Cells(6, 8, 6, 13).Style.Font.Name = "Segoe UI"
                .Cells(6, 8, 6, 13).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 8, 7, 9).Value = "Line Leader"
                .Cells(7, 8, 7, 9).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 8, 7, 9).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 8, 7, 9).Merge = True
                .Cells(7, 8, 7, 9).Style.Font.Size = 10
                .Cells(7, 8, 7, 9).Style.Font.Name = "Segoe UI"
                .Cells(7, 8, 7, 9).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 10, 7, 11).Value = "Line Foreman"
                .Cells(7, 10, 7, 11).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 10, 7, 11).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 10, 7, 11).Merge = True
                .Cells(7, 10, 7, 11).Style.Font.Size = 10
                .Cells(7, 10, 7, 11).Style.Font.Name = "Segoe UI"
                .Cells(7, 10, 7, 11).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 12, 7, 13).Value = "QE Foreman"
                .Cells(7, 12, 7, 13).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 12, 7, 13).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 12, 7, 13).Merge = True
                .Cells(7, 12, 7, 13).Style.Font.Size = 10
                .Cells(7, 12, 7, 13).Style.Font.Name = "Segoe UI"
                .Cells(7, 12, 7, 13).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 8, 8, 8).Value = "Date"
                .Cells(8, 8, 8, 8).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 8, 8, 8).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 8, 8, 8).Style.Font.Size = 10
                .Cells(8, 8, 8, 8).Style.Font.Name = "Segoe UI"
                .Cells(8, 8, 8, 8).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 9, 8, 9).Value = "PIC"
                .Cells(8, 9, 8, 9).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 9, 8, 9).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 9, 8, 9).Style.Font.Size = 10
                .Cells(8, 9, 8, 9).Style.Font.Name = "Segoe UI"
                .Cells(8, 9, 8, 9).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 10, 8, 10).Value = "Date"
                .Cells(8, 10, 8, 10).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 10, 8, 10).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 10, 8, 10).Style.Font.Size = 10
                .Cells(8, 10, 8, 10).Style.Font.Name = "Segoe UI"
                .Cells(8, 10, 8, 10).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 11, 8, 11).Value = "PIC"
                .Cells(8, 11, 8, 11).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 11, 8, 11).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 11, 8, 11).Style.Font.Size = 10
                .Cells(8, 11, 8, 11).Style.Font.Name = "Segoe UI"
                .Cells(8, 11, 8, 11).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 12, 8, 12).Value = "Date"
                .Cells(8, 12, 8, 12).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 12, 8, 12).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 12, 8, 12).Style.Font.Size = 10
                .Cells(8, 12, 8, 12).Style.Font.Name = "Segoe UI"
                .Cells(8, 12, 8, 12).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 13, 8, 13).Value = "PIC"
                .Cells(8, 13, 8, 13).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 13, 8, 13).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 13, 8, 13).Style.Font.Size = 10
                .Cells(8, 13, 8, 13).Style.Font.Name = "Segoe UI"
                .Cells(8, 13, 8, 13).Style.Font.Color.SetColor(Color.White)

                For i = 0 To Rpt.Tables(0).Rows.Count - 1
                    .Cells(i + 9, 1, i + 9, 1).Value = Rpt.Tables(0).Rows(i)("PartID")
                    .Cells(i + 9, 1, i + 9, 1).Style.WrapText = True
                    .Cells(i + 9, 1, i + 9, 1).Style.Font.Size = 10
                    .Cells(i + 9, 1, i + 9, 1).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 2, i + 9, 2).Value = Rpt.Tables(0).Rows(i)("PartName")
                    .Cells(i + 9, 2, i + 9, 2).Style.WrapText = True
                    .Cells(i + 9, 2, i + 9, 2).Style.Font.Size = 10
                    .Cells(i + 9, 2, i + 9, 2).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 3, i + 9, 3).Value = Rpt.Tables(0).Rows(i)("LineID")
                    .Cells(i + 9, 3, i + 9, 3).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 3, i + 9, 3).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 3, i + 9, 3).Style.WrapText = True
                    .Cells(i + 9, 3, i + 9, 3).Style.Font.Size = 10
                    .Cells(i + 9, 3, i + 9, 3).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 4, i + 9, 4).Value = Rpt.Tables(0).Rows(i)("RevNo")
                    .Cells(i + 9, 4, i + 9, 4).Style.WrapText = True
                    .Cells(i + 9, 4, i + 9, 4).Style.Font.Size = 10
                    .Cells(i + 9, 4, i + 9, 4).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 5, i + 9, 5).Value = Format(Rpt.Tables(0).Rows(i)("RevDate"), "dd-MM-yyyy")
                    .Cells(i + 9, 5, i + 9, 5).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 5, i + 9, 5).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 5, i + 9, 5).Style.WrapText = True
                    .Cells(i + 9, 5, i + 9, 5).Style.Font.Size = 10
                    .Cells(i + 9, 5, i + 9, 5).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 6, i + 9, 6).Value = Rpt.Tables(0).Rows(i)("RevHistory")
                    .Cells(i + 9, 6, i + 9, 6).Style.WrapText = True
                    .Cells(i + 9, 6, i + 9, 6).Style.Font.Size = 10
                    .Cells(i + 9, 6, i + 9, 6).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 7, i + 9, 7).Value = Rpt.Tables(0).Rows(i)("PreparedBy")
                    .Cells(i + 9, 7, i + 9, 7).Style.WrapText = True
                    .Cells(i + 9, 7, i + 9, 7).Style.Font.Size = 10
                    .Cells(i + 9, 7, i + 9, 7).Style.Font.Name = "Segoe UI"

                    If IsDBNull(Rpt.Tables(0).Rows(i)("ApprovalDate1")) Then
                        .Cells(i + 9, 8, i + 9, 8).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 8, i + 9, 8).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))

                        .Cells(i + 9, 9, i + 9, 9).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 9, i + 9, 9).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    End If

                    .Cells(i + 9, 8, i + 9, 8).Value = Rpt.Tables(0).Rows(i)("ApprovalDate1")
                    .Cells(i + 9, 8, i + 9, 8).Style.Numberformat.Format = "dd-MM-yyyy"
                    .Cells(i + 9, 8, i + 9, 8).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 8, i + 9, 8).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 8, i + 9, 8).Style.WrapText = True
                    .Cells(i + 9, 8, i + 9, 8).Style.Font.Size = 10
                    .Cells(i + 9, 8, i + 9, 8).Style.Font.Name = "Segoe UI"
                    

                    .Cells(i + 9, 9, i + 9, 9).Value = Rpt.Tables(0).Rows(i)("ApprovalPIC1")
                    .Cells(i + 9, 9, i + 9, 9).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 9, i + 9, 9).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 9, i + 9, 9).Style.WrapText = True
                    .Cells(i + 9, 9, i + 9, 9).Style.Font.Size = 10
                    .Cells(i + 9, 9, i + 9, 9).Style.Font.Name = "Segoe UI"

                    If IsDBNull(Rpt.Tables(0).Rows(i)("ApprovalDate2")) Then
                        .Cells(i + 9, 10, i + 9, 10).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 10, i + 9, 10).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))

                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    End If

                    .Cells(i + 9, 10, i + 9, 10).Value = Rpt.Tables(0).Rows(i)("ApprovalDate2")
                    .Cells(i + 9, 10, i + 9, 10).Style.Numberformat.Format = "dd-MM-yyyy"
                    .Cells(i + 9, 10, i + 9, 10).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 10, i + 9, 10).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 10, i + 9, 10).Style.WrapText = True
                    .Cells(i + 9, 10, i + 9, 10).Style.Font.Size = 10
                    .Cells(i + 9, 10, i + 9, 10).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 11, i + 9, 11).Value = Rpt.Tables(0).Rows(i)("ApprovalPIC2")
                    .Cells(i + 9, 11, i + 9, 11).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 11, i + 9, 11).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 11, i + 9, 11).Style.WrapText = True
                    .Cells(i + 9, 11, i + 9, 11).Style.Font.Size = 10
                    .Cells(i + 9, 11, i + 9, 11).Style.Font.Name = "Segoe UI"

                    If IsDBNull(Rpt.Tables(0).Rows(i)("ApprovalDate3")) Then
                        .Cells(i + 9, 12, i + 9, 12).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 12, i + 9, 12).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))

                        .Cells(i + 9, 13, i + 9, 13).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 13, i + 9, 13).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    End If

                    .Cells(i + 9, 12, i + 9, 12).Value = Rpt.Tables(0).Rows(i)("ApprovalDate3")
                    .Cells(i + 9, 12, i + 9, 12).Style.Numberformat.Format = "dd-MM-yyyy"
                    .Cells(i + 9, 12, i + 9, 12).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 12, i + 9, 12).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 12, i + 9, 12).Style.WrapText = True
                    .Cells(i + 9, 12, i + 9, 12).Style.Font.Size = 10
                    .Cells(i + 9, 12, i + 9, 12).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 13, i + 9, 13).Value = Rpt.Tables(0).Rows(i)("ApprovalPIC3")
                    .Cells(i + 9, 13, i + 9, 13).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 13, i + 9, 13).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 13, i + 9, 13).Style.WrapText = True
                    .Cells(i + 9, 13, i + 9, 13).Style.Font.Size = 10
                    .Cells(i + 9, 13, i + 9, 13).Style.Font.Name = "Segoe UI"
                Next
                FormatExcel(ws, Rpt)
                InsertHeader(ws, pLineID, pPartID, pPartName, pRevNo, pApprovalStatus)
            End With

            exl.Save()
            DevExpress.Web.ASPxWebControl.RedirectOnCallback("Download/" & fi.Name)
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        End Try
    End Sub

    Private Sub InsertHeader(ByVal pExl As ExcelWorksheet, ByVal pLineID As String, ByVal pPartID As String, ByVal pPartName As String, ByVal pRevNo As String, ByVal pApprovalStatus As String)
        With pExl
            .Cells(1, 1, 1, 1).Value = "Statistics Quality Control Approval"
            .Cells(1, 1, 1, 13).Merge = True
            .Cells(1, 1, 1, 13).Style.HorizontalAlignment = HorzAlignment.Far
            .Cells(1, 1, 1, 13).Style.VerticalAlignment = VertAlignment.Center
            .Cells(1, 1, 1, 13).Style.Font.Bold = True
            .Cells(1, 1, 1, 13).Style.Font.Size = 16
            .Cells(1, 1, 1, 13).Style.Font.Name = "Segoe UI"

            .Cells(3, 1, 3, 1).Value = "Line No"
            .Cells(3, 1, 3, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 1, 3, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 1, 3, 1).Style.Font.Size = 10
            .Cells(3, 1, 3, 1).Style.Font.Name = "Segoe UI"

            .Cells(3, 2, 3, 2).Value = ": " & pLineID
            .Cells(3, 2, 3, 2).Merge = True
            .Cells(3, 2, 3, 2).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 2, 3, 2).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 2, 3, 2).Style.Font.Size = 10
            .Cells(3, 2, 3, 2).Style.Font.Name = "Segoe UI"

            .Cells(3, 3, 3, 3).Value = "Part No"
            .Cells(3, 3, 3, 5).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 3, 3, 5).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 3, 3, 5).Style.Font.Size = 10
            .Cells(3, 3, 3, 5).Style.Font.Name = "Segoe UI"

            .Cells(3, 6, 3, 6).Value = ": " & pPartID
            .Cells(3, 6, 3, 6).Merge = True
            .Cells(3, 6, 3, 6).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 6, 3, 6).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 6, 3, 6).Style.Font.Size = 10
            .Cells(3, 6, 3, 6).Style.Font.Name = "Segoe UI"

            .Cells(4, 3, 4, 3).Value = "Part Name"
            .Cells(4, 3, 4, 5).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 3, 4, 5).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 3, 4, 5).Style.Font.Size = 10
            .Cells(4, 3, 4, 5).Style.Font.Name = "Segoe UI"

            .Cells(4, 6, 4, 6).Value = ": " & pPartName
            .Cells(4, 6, 4, 6).Merge = True
            .Cells(4, 6, 4, 6).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 6, 4, 6).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 6, 4, 6).Style.Font.Size = 10
            .Cells(4, 6, 4, 6).Style.Font.Name = "Segoe UI"

            .Cells(3, 7, 3, 7).Value = "Rev No"
            .Cells(3, 7, 3, 8).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 7, 3, 8).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 7, 3, 8).Style.Font.Size = 10
            .Cells(3, 7, 3, 8).Style.Font.Name = "Segoe UI"

            .Cells(3, 9, 3, 9).Value = ": " & pRevNo
            .Cells(3, 9, 3, 9).Merge = True
            .Cells(3, 9, 3, 9).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 9, 3, 9).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 9, 3, 9).Style.Font.Size = 10
            .Cells(3, 9, 3, 9).Style.Font.Name = "Segoe UI"

            .Cells(4, 7, 4, 7).Value = "Approval Status"
            .Cells(4, 7, 4, 8).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 7, 4, 8).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 7, 4, 8).Style.Font.Size = 10
            .Cells(4, 7, 4, 8).Style.Font.Name = "Segoe UI"

            .Cells(4, 9, 4, 9).Value = ": " & pApprovalStatus
            .Cells(4, 9, 4, 9).Merge = True
            .Cells(4, 9, 4, 9).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 9, 4, 9).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 9, 4, 9).Style.Font.Size = 10
            .Cells(4, 9, 4, 9).Style.Font.Name = "Segoe UI"
        End With
    End Sub

    Private Sub FormatExcel(ByVal pExl As ExcelWorksheet, ByVal pRpt As DataSet)
        With pExl
            .Column(1).Width = 18
            .Column(2).Width = 24
            .Column(3).Width = 7
            .Column(4).Width = 7
            .Column(5).Width = 12
            .Column(6).Width = 35
            .Column(7).Width = 15

            .Column(8).Width = 12
            .Column(9).Width = 15
            .Column(10).Width = 12
            .Column(11).Width = 15
            .Column(12).Width = 12
            .Column(13).Width = 15

            Dim rgAll As ExcelRange = .Cells(6, 1, pRpt.Tables(0).Rows.Count + 8, 13)
            DrawAllBorders(rgAll)

            Dim rgHeader As ExcelRange = .Cells(6, 1, 8, 13)
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