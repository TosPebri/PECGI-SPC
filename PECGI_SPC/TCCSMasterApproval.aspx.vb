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

Public Class TCCSMasterApproval
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
    Public StatusApproval1 As String
#End Region

#Region "Procedure"
    Private Sub up_fillline()
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSApprovalDB.GetDataLine(pUser, "")
        cbolineid.DataSource = dsMenu
        cbolineid.DataBind()
    End Sub

    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        GridMenu.JSProperties("cp_message") = ErrMsg
        GridMenu.JSProperties("cp_type") = msgType
        GridMenu.JSProperties("cp_val") = pVal
    End Sub

    Private Sub up_GridLoadMenu(ByVal pLineID As String, ByVal pSubLineID As String, ByVal pMachineNo As String, ByVal pPartID As String, ByVal pRevNo As String, ByVal pApprovalStatus As String, ByVal pStartDate As String, ByVal pEndDate As String)
        Dim ErrMsg As String = ""
        Dim Menu As DataSet
        Menu = ClsTCCSApprovalDB.GetData(pUser, pLineID, pSubLineID, pMachineNo, pPartID, pRevNo, pApprovalStatus, pStartDate, pEndDate, ErrMsg)
        If ErrMsg = "" Then
            GridMenu.DataSource = Menu
            GridMenu.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub Approval(ByVal pMachineNo As String, ByVal pPartID As String, ByVal pRevNo As String, ByVal pLineID As String, ByVal pSubLineID As String)
        Dim pErr As String = ""
        Dim TCCSAproval As New ClsTCCSApproval
        TCCSAproval.ApprovalPIC = pUser
        TCCSAproval.MachineNo = pMachineNo
        TCCSAproval.PartID = pPartID
        TCCSAproval.RevNo = pRevNo
        TCCSAproval.LineID = pLineID
        TCCSAproval.SubLineID = pSubLineID
        ClsTCCSApprovalDB.Approval(TCCSAproval, pErr)
        If pErr <> "" Then
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        Else
            show_error(MsgTypeEnum.Success, "Approve successfully!", 1)
        End If
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "C020")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If

        sGlobal.getMenu("C020")
        Master.SiteTitle = sGlobal.menuName

        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "C020")
        Dim commandColumn = TryCast(GridMenu.Columns(0), GridViewCommandColumn)
        up_fillline()
        If ClsTCCSApprovalDB.CekStatusSectionHead(pUser) = True Then
            btnApprove.ClientEnabled = True
        Else
            btnApprove.ClientEnabled = False
        End If

        'notif TCCS Approval
        If ClsTCCSApprovalDB.CekStatusSectionHead(pUser) = True Then
            StatusApproval1 = 1
        End If

        Dim Menu As DataSet
        Menu = ClsTCCSApprovalDB.GetStartDate(IIf(pUser = Nothing, "", pUser), IIf(StatusApproval1 = Nothing, "", StatusApproval1))
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
            up_GridLoadMenu(cbolineid.Value, cbosublineid.Value, cbomachineno.Value, cbopartid.Value, cborevno.Value, cboapprovalstatus.Value, dtstart.Value, dtend.Value)
        ElseIf e.CallbackName = "PAGERONCLICK" Then
            up_GridLoadMenu(cbolineid.Value, cbosublineid.Value, cbomachineno.Value, cbopartid.Value, cborevno.Value, cboapprovalstatus.Value, dtstart.Value, dtend.Value)
        End If
    End Sub

    Private Sub GridMenu_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridMenu.BeforeGetCallbackResult

    End Sub

    Private Sub GridMenu_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles GridMenu.CellEditorInitialize

    End Sub

    Private Sub GridMenu_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridMenu.CustomCallback
        Dim pFunction As String = Split(e.Parameters, "|")(0)

        Select Case pFunction
            Case "Approve"
                Dim table As DataTable = Nothing
                table = DirectCast(Session("table"), DataTable)
                Dim selectItems As List(Of Object) = GridMenu.GetSelectedFieldValues("MachineNo;PartID;RevNo;LineID;SubLineID")
                If selectItems.Count = 0 Then
                    show_error(MsgTypeEnum.Warning, "Please select data!", 1)
                Else
                    For Each selectItemId As Object In selectItems
                        Dim pMachineNo As String = Split(selectItemId, "|")(0)
                        Dim pPartID As String = Split(selectItemId, "|")(1)
                        Dim pRevNo As String = Split(selectItemId, "|")(2)
                        Dim pLineID As String = Split(selectItemId, "|")(3)
                        Dim pSubLineID As String = Split(selectItemId, "|")(4)
                        Dim Menu As DataSet
                        Menu = ClsTCCSApprovalDB.CekStatusApproval(pMachineNo, pPartID, pRevNo)

                        If ClsTCCSApprovalDB.CekStatusSectionHead(pUser, "") = True And Menu.Tables(0).Rows(0)("ApprovalStatus").ToString <> "1" Then
                            Approval(pMachineNo, pPartID, pRevNo, pLineID, pSubLineID)
                            up_GridLoadMenu(cbolineid.Value, cbosublineid.Value, cbomachineno.Value, cbopartid.Value, cborevno.Value, cboapprovalstatus.Value, dtstart.Value, dtend.Value)
                        Else
                            GridMenu.JSProperties("cp_messageapprove") = "1"
                        End If
                    Next selectItemId
                    up_GridLoadMenu(cbolineid.Value, cbosublineid.Value, cbomachineno.Value, cbopartid.Value, cborevno.Value, cboapprovalstatus.Value, dtstart.Value, dtend.Value)
                End If

            Case "Excel"
                up_Excel(cbolineid.Value, cbosublineid.Value, cbomachineno.Value, cbopartid.Value, txtpartname.Value, cborevno.Value, cboapprovalstatus.Value, "")

            Case "Refresh"
                up_GridLoadMenu(cbolineid.Value, cbosublineid.Value, cbomachineno.Value, cbopartid.Value, cborevno.Value, cboapprovalstatus.Value, dtstart.Value, dtend.Value)

            Case "Clear"
                up_GridLoadMenu("", "", "", "", "", "", "", "")

            Case "ViewQCS"
                GridMenu.JSProperties("cp_viewtccs") = "1"
                GridMenu.JSProperties("cp_partid") = RTrim(GridMenu.GetSelectedFieldValues("PartID")(0).ToString)
                GridMenu.JSProperties("cp_partname") = RTrim(GridMenu.GetSelectedFieldValues("PartName")(0).ToString)
                GridMenu.JSProperties("cp_machineno") = RTrim(GridMenu.GetSelectedFieldValues("MachineNo")(0).ToString)
                GridMenu.JSProperties("cp_lineid") = RTrim(GridMenu.GetSelectedFieldValues("LineID")(0).ToString)
                GridMenu.JSProperties("cp_sublineid") = RTrim(GridMenu.GetSelectedFieldValues("SubLineID")(0).ToString)
                GridMenu.JSProperties("cp_revno") = GridMenu.GetSelectedFieldValues("RevNo")(0).ToString
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

    Private Sub GridMenu_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles GridMenu.HtmlDataCellPrepared
        If (e.DataColumn.FieldName = "ApprovalDate") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            End If
        End If
        If (e.DataColumn.FieldName = "ApprovalPIC") Then
            If IsDBNull(e.CellValue) Then
                e.Cell.BackColor = Color.Red
            End If
        End If
    End Sub
#End Region

#Region "CallBack"
    Private Sub cbosublineid_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbosublineid.Callback
        Dim pLineID As String = Split(e.Parameter, "|")(0)
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSApprovalDB.GetDataSubLine(pLineID, pUser, "")
        cbosublineid.DataSource = dsMenu
        cbosublineid.DataBind()
    End Sub

    Private Sub cbomachineno_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbomachineno.Callback
        Dim pLineID As String = Split(e.Parameter, "|")(0)
        Dim pSubLineID As String = Split(e.Parameter, "|")(1)
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSApprovalDB.GetDataMachine(pLineID, pSubLineID, pUser, "")
        cbomachineno.DataSource = dsMenu
        cbomachineno.DataBind()
    End Sub

    Private Sub cbopartid_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbopartid.Callback
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSApprovalDB.GetDataPart("")
        cbopartid.DataSource = dsMenu
        cbopartid.DataBind()
    End Sub

    Private Sub cborevno_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cborevno.Callback
        Dim pLineID As String = Split(e.Parameter, "|")(0)
        Dim pSubLineID As String = Split(e.Parameter, "|")(1)
        Dim pMachineNo As String = Split(e.Parameter, "|")(2)
        Dim pPartID As String = Split(e.Parameter, "|")(3)
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSApprovalDB.GetDataRev(pLineID, pSubLineID, pMachineNo, pPartID)
        cborevno.DataSource = dsMenu
        cborevno.DataBind()
    End Sub

    Private Sub cboapprovalstatus_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cboapprovalstatus.Callback
        Dim dsMenu As DataTable
        dsMenu = ClsTCCSApprovalDB.GetStatusApproval("")
        cboapprovalstatus.DataSource = dsMenu
        cboapprovalstatus.DataBind()
    End Sub
#End Region

#Region "Download To Excel"
    Private Sub up_Excel(ByVal pLineID As String, ByVal pSubLineID As String, ByVal pMachineNo As String, ByVal pPartID As String, ByVal pPartName As String, ByVal pRevNo As String, ByVal pApprovalStatus As String, Optional ByRef pErr As String = "")
        Try
            Dim fi As New FileInfo(Server.MapPath("~\Download\Change Item Check Sheet Approval.xlsx"))
            If fi.Exists Then
                fi.Delete()
                fi = New FileInfo(Server.MapPath("~\Download\Change Item Check Sheet Approval.xlsx"))
            End If
            Dim exl As New ExcelPackage(fi)
            Dim ws As ExcelWorksheet
            ws = exl.Workbook.Worksheets.Add("Sheet1")
            ws.View.ShowGridLines = False

            Dim Rpt As DataSet
            Rpt = ClsTCCSApprovalDB.GetData(pUser, pLineID, pSubLineID, pMachineNo, pPartID, pRevNo, pApprovalStatus, dtstart.Value, dtend.Value, pErr)

            With ws
                .Cells(6, 1, 8, 1).Value = "Line"
                .Cells(6, 1, 8, 1).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 1, 8, 1).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 1, 8, 1).Merge = True
                .Cells(6, 1, 8, 1).Style.Font.Size = 10
                .Cells(6, 1, 8, 1).Style.Font.Name = "Segoe UI"
                .Cells(6, 1, 8, 1).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 2, 8, 2).Value = "Sub Line"
                .Cells(6, 2, 8, 2).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 2, 8, 2).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 2, 8, 2).Merge = True
                .Cells(6, 2, 8, 2).Style.Font.Size = 10
                .Cells(6, 2, 8, 2).Style.Font.Name = "Segoe UI"
                .Cells(6, 2, 8, 2).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 3, 8, 3).Value = "Machine No"
                .Cells(6, 3, 8, 3).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 3, 8, 3).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 3, 8, 3).Merge = True
                .Cells(6, 3, 8, 3).Style.Font.Size = 10
                .Cells(6, 3, 8, 3).Style.Font.Name = "Segoe UI"
                .Cells(6, 3, 8, 3).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 4, 8, 4).Value = "Part No"
                .Cells(6, 4, 8, 4).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 4, 8, 4).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 4, 8, 4).Merge = True
                .Cells(6, 4, 8, 4).Style.Font.Size = 10
                .Cells(6, 4, 8, 4).Style.Font.Name = "Segoe UI"
                .Cells(6, 4, 8, 4).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 5, 8, 5).Value = "Part Name"
                .Cells(6, 5, 8, 5).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 5, 8, 5).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 5, 8, 5).Merge = True
                .Cells(6, 5, 8, 5).Style.Font.Size = 10
                .Cells(6, 5, 8, 5).Style.Font.Name = "Segoe UI"
                .Cells(6, 5, 8, 5).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 6, 8, 6).Value = "Rev No"
                .Cells(6, 6, 8, 6).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 6, 8, 6).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 6, 8, 6).Style.WrapText = True
                .Cells(6, 6, 8, 6).Merge = True
                .Cells(6, 6, 8, 6).Style.Font.Size = 10
                .Cells(6, 6, 8, 6).Style.Font.Name = "Segoe UI"
                .Cells(6, 6, 8, 6).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 7, 8, 7).Value = "Rev Date"
                .Cells(6, 7, 8, 7).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 7, 8, 7).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 7, 8, 7).Style.WrapText = True
                .Cells(6, 7, 8, 7).Merge = True
                .Cells(6, 7, 8, 7).Style.Font.Size = 10
                .Cells(6, 7, 8, 7).Style.Font.Name = "Segoe UI"
                .Cells(6, 7, 8, 7).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 8, 8, 8).Value = "Rev History"
                .Cells(6, 8, 8, 8).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 8, 8, 8).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 8, 8, 8).Style.WrapText = True
                .Cells(6, 8, 8, 8).Merge = True
                .Cells(6, 8, 8, 8).Style.Font.Size = 10
                .Cells(6, 8, 8, 8).Style.Font.Name = "Segoe UI"
                .Cells(6, 8, 8, 8).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 9, 8, 9).Value = "Preared By"
                .Cells(6, 9, 8, 9).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 9, 8, 9).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 9, 8, 9).Style.WrapText = True
                .Cells(6, 9, 8, 9).Merge = True
                .Cells(6, 9, 8, 9).Style.Font.Size = 10
                .Cells(6, 9, 8, 9).Style.Font.Name = "Segoe UI"
                .Cells(6, 9, 8, 9).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 10, 6, 11).Value = "Approval"
                .Cells(6, 10, 6, 11).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 10, 6, 11).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 10, 6, 11).Merge = True
                .Cells(6, 10, 6, 11).Style.Font.Size = 10
                .Cells(6, 10, 6, 11).Style.Font.Name = "Segoe UI"
                .Cells(6, 10, 6, 11).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 10, 7, 11).Value = "Prod. Sec. Head"
                .Cells(7, 10, 7, 11).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 10, 7, 11).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 10, 7, 11).Merge = True
                .Cells(7, 10, 7, 11).Style.Font.Size = 10
                .Cells(7, 10, 7, 11).Style.Font.Name = "Segoe UI"
                .Cells(7, 10, 7, 11).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 10, 8, 10).Value = "Date"
                .Cells(8, 10, 8, 10).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 10, 8, 10).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 10, 8, 10).Merge = True
                .Cells(8, 10, 8, 10).Style.Font.Size = 10
                .Cells(8, 10, 8, 10).Style.Font.Name = "Segoe UI"
                .Cells(8, 10, 8, 10).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 11, 8, 11).Value = "PIC"
                .Cells(8, 11, 8, 11).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 11, 8, 11).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 11, 8, 11).Merge = True
                .Cells(8, 11, 8, 11).Style.Font.Size = 10
                .Cells(8, 11, 8, 11).Style.Font.Name = "Segoe UI"
                .Cells(8, 11, 8, 11).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 12, 8, 12).Value = "Remark"
                .Cells(6, 12, 8, 12).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 12, 8, 12).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 12, 8, 12).Style.WrapText = True
                .Cells(6, 12, 8, 12).Merge = True
                .Cells(6, 12, 8, 12).Style.Font.Size = 10
                .Cells(6, 12, 8, 12).Style.Font.Name = "Segoe UI"
                .Cells(6, 12, 8, 12).Style.Font.Color.SetColor(Color.White)

                For i = 0 To Rpt.Tables(0).Rows.Count - 1
                    .Cells(i + 9, 1, i + 9, 1).Value = Rpt.Tables(0).Rows(i)("LineID")
                    .Cells(i + 9, 1, i + 9, 1).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 1, i + 9, 1).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 1, i + 9, 1).Style.WrapText = True
                    .Cells(i + 9, 1, i + 9, 1).Style.Font.Size = 10
                    .Cells(i + 9, 1, i + 9, 1).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 2, i + 9, 2).Value = Rpt.Tables(0).Rows(i)("SubLineID")
                    .Cells(i + 9, 2, i + 9, 2).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 2, i + 9, 2).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 2, i + 9, 2).Style.WrapText = True
                    .Cells(i + 9, 2, i + 9, 2).Style.Font.Size = 10
                    .Cells(i + 9, 2, i + 9, 2).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 3, i + 9, 3).Value = Rpt.Tables(0).Rows(i)("MachineNo")
                    .Cells(i + 9, 3, i + 9, 3).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 3, i + 9, 3).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 3, i + 9, 3).Style.WrapText = True
                    .Cells(i + 9, 3, i + 9, 3).Style.Font.Size = 10
                    .Cells(i + 9, 3, i + 9, 3).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 4, i + 9, 4).Value = Rpt.Tables(0).Rows(i)("PartID")
                    .Cells(i + 9, 4, i + 9, 4).Style.WrapText = True
                    .Cells(i + 9, 4, i + 9, 4).Style.Font.Size = 10
                    .Cells(i + 9, 4, i + 9, 4).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 5, i + 9, 5).Value = Rpt.Tables(0).Rows(i)("PartName")
                    .Cells(i + 9, 5, i + 9, 5).Style.WrapText = True
                    .Cells(i + 9, 5, i + 9, 5).Style.Font.Size = 10
                    .Cells(i + 9, 5, i + 9, 5).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 6, i + 9, 6).Value = Rpt.Tables(0).Rows(i)("RevNo")
                    .Cells(i + 9, 6, i + 9, 6).Style.WrapText = True
                    .Cells(i + 9, 6, i + 9, 6).Style.Font.Size = 10
                    .Cells(i + 9, 6, i + 9, 6).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 7, i + 9, 7).Value = Format(Rpt.Tables(0).Rows(i)("RevDate"), "dd-MM-yyyy")
                    .Cells(i + 9, 7, i + 9, 7).Style.WrapText = True
                    .Cells(i + 9, 7, i + 9, 7).Style.Font.Size = 10
                    .Cells(i + 9, 7, i + 9, 7).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 8, i + 9, 8).Value = Rpt.Tables(0).Rows(i)("RevHistory")
                    .Cells(i + 9, 8, i + 9, 8).Style.WrapText = True
                    .Cells(i + 9, 8, i + 9, 8).Style.Font.Size = 10
                    .Cells(i + 9, 8, i + 9, 8).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 9, i + 9, 9).Value = Rpt.Tables(0).Rows(i)("PreparedBy")
                    .Cells(i + 9, 9, i + 9, 9).Style.WrapText = True
                    .Cells(i + 9, 9, i + 9, 9).Style.Font.Size = 10
                    .Cells(i + 9, 9, i + 9, 9).Style.Font.Name = "Segoe UI"


                    If IsDBNull(Rpt.Tables(0).Rows(i)("ApprovalDate")) Then
                        .Cells(i + 9, 10, i + 9, 10).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 10, i + 9, 10).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))

                        .Cells(i + 9, 10, i + 9, 10).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 10, i + 9, 10).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    End If

                    .Cells(i + 9, 10, i + 9, 10).Value = Rpt.Tables(0).Rows(i)("ApprovalDate")
                    .Cells(i + 9, 10, i + 9, 10).Style.Numberformat.Format = "dd-MM-yyyy"
                    .Cells(i + 9, 10, i + 9, 10).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 10, i + 9, 10).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 10, i + 9, 10).Style.WrapText = True
                    .Cells(i + 9, 10, i + 9, 10).Style.Font.Size = 10
                    .Cells(i + 9, 10, i + 9, 10).Style.Font.Name = "Segoe UI"

                    If IsDBNull(Rpt.Tables(0).Rows(i)("ApprovalPIC")) Then
                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))

                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                    End If

                    .Cells(i + 9, 11, i + 9, 11).Value = Rpt.Tables(0).Rows(i)("ApprovalPIC")
                    .Cells(i + 9, 11, i + 9, 11).Style.HorizontalAlignment = HorzAlignment.Far
                    .Cells(i + 9, 11, i + 9, 11).Style.VerticalAlignment = VertAlignment.Center
                    .Cells(i + 9, 11, i + 9, 11).Style.WrapText = True
                    .Cells(i + 9, 11, i + 9, 11).Style.Font.Size = 10
                    .Cells(i + 9, 11, i + 9, 11).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 12, i + 9, 12).Value = Rpt.Tables(0).Rows(i)("Remark")
                    .Cells(i + 9, 12, i + 9, 12).Style.WrapText = True
                    .Cells(i + 9, 12, i + 9, 12).Style.Font.Size = 10
                    .Cells(i + 9, 12, i + 9, 12).Style.Font.Name = "Segoe UI"

                Next
                FormatExcel(ws, Rpt)
                InsertHeader(ws, pLineID, pSubLineID, pMachineNo, pPartID, pPartName, pRevNo, pApprovalStatus)
            End With

            exl.Save()
            DevExpress.Web.ASPxWebControl.RedirectOnCallback("Download/" & fi.Name)
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        End Try
    End Sub

    Private Sub InsertHeader(ByVal pExl As ExcelWorksheet, ByVal pLineID As String, ByVal pSubLineID As String, ByVal pMachineNo As String, ByVal pPartID As String, ByVal pPartName As String, ByVal pRevNo As String, ByVal pApprovalStatus As String)
        With pExl
            .Cells(1, 1, 1, 1).Value = "Change Item Check Sheet Approval"
            .Cells(1, 1, 1, 13).Merge = True
            .Cells(1, 1, 1, 13).Style.HorizontalAlignment = HorzAlignment.Far
            .Cells(1, 1, 1, 13).Style.VerticalAlignment = VertAlignment.Center
            .Cells(1, 1, 1, 13).Style.Font.Bold = True
            .Cells(1, 1, 1, 13).Style.Font.Size = 16
            .Cells(1, 1, 1, 13).Style.Font.Name = "Segoe UI"

            .Cells(3, 1, 3, 1).Value = "Line No"
            .Cells(3, 1, 3, 1).Style.Font.Size = 10
            .Cells(3, 1, 3, 1).Style.Font.Name = "Segoe UI"

            .Cells(3, 3, 3, 3).Value = ": " & pLineID
            .Cells(3, 3, 3, 3).Merge = True
            .Cells(3, 3, 3, 3).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 3, 3, 3).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 3, 3, 3).Style.Font.Size = 10
            .Cells(3, 3, 3, 3).Style.Font.Name = "Segoe UI"

            .Cells(4, 1, 4, 1).Value = "Sub Line"
            .Cells(4, 1, 4, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 1, 4, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 1, 4, 1).Style.Font.Size = 10
            .Cells(4, 1, 4, 1).Style.Font.Name = "Segoe UI"

            .Cells(4, 3, 4, 3).Value = ": " & pSubLineID
            .Cells(4, 3, 4, 3).Merge = True
            .Cells(4, 3, 4, 3).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 3, 4, 3).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 3, 4, 3).Style.Font.Size = 10
            .Cells(4, 3, 4, 3).Style.Font.Name = "Segoe UI"

            .Cells(3, 4, 3, 4).Value = "Part No"
            .Cells(3, 4, 3, 4).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 4, 3, 4).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 4, 3, 4).Style.Font.Size = 10
            .Cells(3, 4, 3, 4).Style.Font.Name = "Segoe UI"

            .Cells(3, 5, 3, 5).Value = ": " & pPartID
            .Cells(3, 5, 3, 5).Merge = True
            .Cells(3, 5, 3, 5).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 5, 3, 5).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 5, 3, 5).Style.Font.Size = 10
            .Cells(3, 5, 3, 5).Style.Font.Name = "Segoe UI"

            .Cells(4, 4, 4, 4).Value = "Part Name"
            .Cells(4, 4, 4, 4).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 4, 4, 4).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 4, 4, 4).Style.Font.Size = 10
            .Cells(4, 4, 4, 4).Style.Font.Name = "Segoe UI"

            .Cells(4, 5, 4, 5).Value = ": " & pPartName
            .Cells(4, 5, 4, 5).Merge = True
            .Cells(4, 5, 4, 5).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 5, 4, 5).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 5, 4, 5).Style.Font.Size = 10
            .Cells(4, 5, 4, 5).Style.Font.Name = "Segoe UI"


            .Cells(3, 6, 3, 6).Value = "Rev No"
            .Cells(3, 6, 3, 6).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 6, 3, 6).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 6, 3, 6).Style.Font.Size = 10
            .Cells(3, 6, 3, 6).Style.Font.Name = "Segoe UI"

            .Cells(3, 8, 3, 8).Value = ": " & pRevNo
            .Cells(3, 8, 3, 8).Merge = True
            .Cells(3, 8, 3, 8).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 8, 3, 8).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 8, 3, 8).Style.Font.Size = 10
            .Cells(3, 8, 3, 8).Style.Font.Name = "Segoe UI"

            .Cells(4, 6, 4, 6).Value = "Approval Status"
            .Cells(4, 6, 4, 6).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 6, 4, 6).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 6, 4, 6).Style.Font.Size = 10
            .Cells(4, 6, 4, 6).Style.Font.Name = "Segoe UI"

            .Cells(4, 8, 4, 8).Value = ": " & pApprovalStatus
            .Cells(4, 8, 4, 8).Merge = True
            .Cells(4, 8, 4, 8).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 8, 4, 8).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 8, 4, 8).Style.Font.Size = 10
            .Cells(4, 8, 4, 8).Style.Font.Name = "Segoe UI"
        End With
    End Sub

    Private Sub FormatExcel(ByVal pExl As ExcelWorksheet, ByVal pRpt As DataSet)
        With pExl
            .Column(1).Width = 7 'Line ID
            .Column(2).Width = 10 'Sub LineID
            .Column(3).Width = 11 'Machine No
            .Column(4).Width = 20 'Part ID
            .Column(5).Width = 20 'Part Name
            .Column(6).Width = 8 'Rev No
            .Column(7).Width = 13 'Rev Date

            .Column(8).Width = 15 'Rev His
            .Column(9).Width = 15 '
            .Column(10).Width = 12
            .Column(11).Width = 15
            .Column(12).Width = 35

            Dim rgAll As ExcelRange = .Cells(6, 1, pRpt.Tables(0).Rows.Count + 8, 12)
            DrawAllBorders(rgAll)

            Dim rgHeader As ExcelRange = .Cells(6, 1, 8, 12)
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