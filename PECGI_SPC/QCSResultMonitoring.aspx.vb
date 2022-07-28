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

Public Class QCSResultMonitoring
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Public AuthInsert As Boolean = False
    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
    Public OK As Integer = 0
    Public NG As Integer = 0
    Public NP As Integer = 0
    Public Incomplete As Integer = 0
    Public Total As Integer = 0
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "B050")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If
        sGlobal.getMenu("B050")
        Master.SiteTitle = sGlobal.menuName

        AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B050")
        show_error(MsgTypeEnum.Info, "", 0)

        If ClsQCSMasterDB.GetDataQE(pUser, "") = True Then
            up_fillcomboline(1, pUser)
        Else
            up_fillcomboline(0, pUser)
        End If
        up_GridLoadShiftCycle()
        up_GridLoad()
    End Sub

#Region "Procedure"
    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        GridMenu.JSProperties("cp_message") = ErrMsg
        GridMenu.JSProperties("cp_type") = msgType
        GridMenu.JSProperties("cp_val") = pVal
    End Sub

    Private Sub SumNG()
        GridMenu.JSProperties("cp_lblng") = 1
        GridMenu.JSProperties("cp_sumng") = NG
    End Sub

    Private Sub SumNP()
        GridMenu.JSProperties("cp_lblnp") = 1
        GridMenu.JSProperties("cp_sumnp") = NP
    End Sub

    Private Sub SumOK()
        GridMenu.JSProperties("cp_lblok") = 1
        GridMenu.JSProperties("cp_sumok") = OK
    End Sub

    Private Sub SumIncomplete()
        GridMenu.JSProperties("cp_lblincomplete") = 1
        GridMenu.JSProperties("cp_sumincomplete") = Incomplete
    End Sub

    Private Sub SumTotal()
        GridMenu.JSProperties("cp_lbltotal") = 1
        GridMenu.JSProperties("cp_sumtotal") = NG + OK + Incomplete + NP
    End Sub

    Private Sub up_fillcomboline(ByVal pStstus As String, ByVal pUser As String)
        Dim ErrMsg As String = ""
        Dim dsline As DataTable
        dsline = ClsQCSResultMonitoringDB.GetDataLine(pUser, ErrMsg)
        If ErrMsg = "" Then
            cbolineid.DataSource = dsline
            cbolineid.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub up_GridLoadShiftCycle()
        Dim ErrMsg As String = ""
        Dim Menu As DataSet
        Menu = ClsQCSResultMonitoringDB.GetTime(ErrMsg)
        GridMenu.Columns("Shift1Cycle1").Caption = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle1").ToString & ")"
        GridMenu.Columns("Shift1Cycle2").Caption = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle2").ToString & ")"
        GridMenu.Columns("Shift1Cycle3").Caption = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle3").ToString & ")"
        GridMenu.Columns("Shift1Cycle4").Caption = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle4").ToString & ")"
        GridMenu.Columns("Shift1Cycle5").Caption = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle5").ToString & ")"

        GridMenu.Columns("Shift2Cycle1").Caption = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle1").ToString & ")"
        GridMenu.Columns("Shift2Cycle2").Caption = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle2").ToString & ")"
        GridMenu.Columns("Shift2Cycle3").Caption = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle3").ToString & ")"
        GridMenu.Columns("Shift2Cycle4").Caption = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle4").ToString & ")"
        GridMenu.Columns("Shift2Cycle5").Caption = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle5").ToString & ")"

        GridMenu.Columns("Shift3Cycle1").Caption = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle1").ToString & ")"
        GridMenu.Columns("Shift3Cycle2").Caption = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle2").ToString & ")"
        GridMenu.Columns("Shift3Cycle3").Caption = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle3").ToString & ")"
        GridMenu.Columns("Shift3Cycle4").Caption = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle4").ToString & ")"
        GridMenu.Columns("Shift3Cycle5").Caption = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle5").ToString & ")"
    End Sub

    Private Sub up_GridLoad()
        Dim ErrMsg As String = ""
        Dim Menu As DataSet
        Menu = ClsQCSResultMonitoringDB.GetList(IIf(dtdate.Value = Nothing, "", Format(dtdate.Value, "yyyy-MM-dd")), IIf(cbolineid.Value = Nothing, "", cbolineid.Value), IIf(cbopartid.Value = Nothing, "", cbopartid.Value), IIf(cboqcsstatus.Value = Nothing, "ALL", cboqcsstatus.Value), pUser, ErrMsg)
        If ErrMsg = "" Then
            GridMenu.DataSource = Menu
            GridMenu.DataBind()
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If

    End Sub
#End Region

#Region "Grid"
    Private Sub GridMenu_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles GridMenu.CustomCallback
        Dim pFunction As String = Split(e.Parameters, "|")(0)

        Select Case pFunction
            Case "Refresh"
                Dim pDate As String = Split(e.Parameters, "|")(1)
                Dim pLine As String = Split(e.Parameters, "|")(2)
                Dim pPart As String = Split(e.Parameters, "|")(3)
                Dim pQCSStatus As String = Split(e.Parameters, "|")(4)
                GridMenu.JSProperties("cp_clearsum") = "1"
                up_GridLoad()
            Case "Excel"
                Dim pDate As Date = Format(dtdate.Value, "yyyy-MM-dd")
                Dim pLineID As String = Split(e.Parameters, "|")(1)
                Dim pPartID As String = Split(e.Parameters, "|")(2)
                Dim pPartName As String = Split(e.Parameters, "|")(3)
                Dim pQCSStatus As String = Split(e.Parameters, "|")(4)
                up_Excel(pDate, pLineID, pPartID, pPartName, pQCSStatus, "")

            Case "ViewQCS"
                GridMenu.JSProperties("cp_viewqcs") = "1"
                GridMenu.JSProperties("cp_date") = Format(dtdate.Value, "yyyy-MM-dd")
                GridMenu.JSProperties("cp_lineid") = RTrim(GridMenu.GetSelectedFieldValues("LineID")(0).ToString)
                GridMenu.JSProperties("cp_sublineid") = RTrim(GridMenu.GetSelectedFieldValues("SubLineID")(0).ToString)
                GridMenu.JSProperties("cp_partid") = RTrim(GridMenu.GetSelectedFieldValues("PartID")(0).ToString)
                GridMenu.JSProperties("cp_partname") = RTrim(GridMenu.GetSelectedFieldValues("PartName")(0).ToString)
        End Select
    End Sub

    Private Sub GridMenu_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles GridMenu.HtmlDataCellPrepared
        If (e.DataColumn.FieldName = "Shift1Cycle1") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift1Cycle2") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift1Cycle3") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift1Cycle4") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift1Cycle5") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift2Cycle1") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift2Cycle2") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift2Cycle3") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift2Cycle4") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift2Cycle5") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift3Cycle1") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift3Cycle2") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift3Cycle3") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift3Cycle4") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If

        If (e.DataColumn.FieldName = "Shift3Cycle5") Then
            If e.CellValue = "NG" Then
                e.Cell.ForeColor = Color.Yellow
                e.Cell.BackColor = Color.Red
                NG = NG + 1
                SumNG()
            ElseIf e.CellValue = "TC" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "D" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "MT" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "O" Then
                e.Cell.ForeColor = Color.Black
                e.Cell.BackColor = Color.Yellow
                NP = NP + 1
                SumNP()
            ElseIf e.CellValue = "-" Then
                e.Cell.ForeColor = Color.Red
                e.Cell.BackColor = Color.Red
                Incomplete = Incomplete + 1
                SumIncomplete()
            ElseIf e.CellValue = "OK" Then
                OK = OK + 1
                SumOK()
            End If
        End If
        SumTotal()
    End Sub

#End Region

#Region "Other"
    Private Sub cbopartid_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbopartid.Callback
        Dim pParam As String = Split(e.Parameter, "|")(1)
        Dim dsMenu As DataTable
        'If pParam = "ALL" Then
        '    If ClsQCSMasterDB.GetDataQE(pUser, "") = True Then
        '        dsMenu = ClsQCSApprovalDB.GetDataPart(pUser, pParam, 2, "")
        '    Else
        '        dsMenu = ClsQCSApprovalDB.GetDataPart(pUser, pParam, 1, "")
        '    End If
        'Else
        '    dsMenu = ClsQCSApprovalDB.GetDataPart(pUser, pParam, 0, "")
        'End If
        dsMenu = ClsQCSResultMonitoringDB.GetDataPart(pUser, cbolineid.Value, "")
        cbopartid.DataSource = dsMenu
        cbopartid.DataBind()
    End Sub

    'Private Sub cboapprovalstatus_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cboapprovalstatus.Callback
    '    Dim pSelect As String = Split(e.Parameter, "|")(0)

    '    Dim dsMenu As DataTable

    '    dsMenu = ClsQCSApprovalDB.GetStatus("")
    '    cboapprovalstatus.DataSource = dsMenu
    '    cboapprovalstatus.DataBind()
    'End Sub

    Private Sub cboqcsstatus_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cboqcsstatus.Callback
        Dim pSelect As String = Split(e.Parameter, "|")(0)

        Dim dsMenu As DataTable

        dsMenu = ClsQCSResultMonitoringDB.GetProcess("")
        cboqcsstatus.DataSource = dsMenu
        cboqcsstatus.DataBind()
    End Sub
#End Region

#Region "Download To Excel"
    Private Sub up_Excel(ByVal pDate As Date, ByVal pLineID As String, ByVal pPartID As String, ByVal pPartName As String, ByVal pQCSStatus As String, Optional ByRef pErr As String = "")
        Try
            Dim fi As New FileInfo(Server.MapPath("~\Download\Statistics Quality Control Result Monitoring.xlsx"))
            If fi.Exists Then
                fi.Delete()
                fi = New FileInfo(Server.MapPath("~\Download\Statistics Quality Control Result Monitoring.xlsx"))
            End If
            Dim exl As New ExcelPackage(fi)
            Dim ws As ExcelWorksheet
            ws = exl.Workbook.Worksheets.Add("Sheet1")
            ws.View.ShowGridLines = False

            Dim ErrMsg As String = ""
            Dim Menu As DataSet
            Menu = ClsQCSResultMonitoringDB.GetTime(ErrMsg)

            Dim Rpt As DataSet
            Rpt = ClsQCSResultMonitoringDB.GetList(pDate, pLineID, pPartID, pQCSStatus, pUser, pErr)

            With ws
                .Cells(6, 1, 8, 1).Value = "Sub Line"
                .Cells(6, 1, 8, 1).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 1, 8, 1).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 1, 8, 1).Merge = True
                .Cells(6, 1, 8, 1).Style.Font.Size = 10
                .Cells(6, 1, 8, 1).Style.Font.Name = "Segoe UI"
                .Cells(6, 1, 8, 1).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 2, 8, 2).Value = "Part No"
                .Cells(6, 2, 8, 2).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 2, 8, 2).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 2, 8, 2).Merge = True
                .Cells(6, 2, 8, 2).Style.Font.Size = 10
                .Cells(6, 2, 8, 2).Style.Font.Name = "Segoe UI"
                .Cells(6, 2, 8, 2).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 3, 8, 3).Value = "Part Name"
                .Cells(6, 3, 8, 3).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 3, 8, 3).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 3, 8, 3).Merge = True
                .Cells(6, 3, 8, 3).Style.Font.Size = 10
                .Cells(6, 3, 8, 3).Style.Font.Name = "Segoe UI"
                .Cells(6, 3, 8, 3).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 4, 6, 8).Value = "Shift1"
                .Cells(6, 4, 6, 8).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 4, 6, 8).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 4, 6, 8).Merge = True
                .Cells(6, 4, 6, 8).Style.Font.Size = 10
                .Cells(6, 4, 6, 8).Style.Font.Name = "Segoe UI"
                .Cells(6, 4, 6, 8).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 4, 7, 4).Value = "1"
                .Cells(7, 4, 7, 4).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 4, 7, 4).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 4, 7, 4).Merge = True
                .Cells(7, 4, 7, 4).Style.Font.Size = 10
                .Cells(7, 4, 7, 4).Style.Font.Name = "Segoe UI"
                .Cells(7, 4, 7, 4).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 5, 7, 5).Value = "2"
                .Cells(7, 5, 7, 5).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 5, 7, 5).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 5, 7, 5).Merge = True
                .Cells(7, 5, 7, 5).Style.Font.Size = 10
                .Cells(7, 5, 7, 5).Style.Font.Name = "Segoe UI"
                .Cells(7, 5, 7, 5).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 6, 7, 6).Value = "3"
                .Cells(7, 6, 7, 6).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 6, 7, 6).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 6, 7, 6).Merge = True
                .Cells(7, 6, 7, 6).Style.Font.Size = 10
                .Cells(7, 6, 7, 6).Style.Font.Name = "Segoe UI"
                .Cells(7, 6, 7, 6).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 7, 7, 7).Value = "4"
                .Cells(7, 7, 7, 7).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 7, 7, 7).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 7, 7, 7).Merge = True
                .Cells(7, 7, 7, 7).Style.Font.Size = 10
                .Cells(7, 7, 7, 7).Style.Font.Name = "Segoe UI"
                .Cells(7, 7, 7, 7).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 8, 7, 8).Value = "5"
                .Cells(7, 8, 7, 8).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 8, 7, 8).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 8, 7, 8).Merge = True
                .Cells(7, 8, 7, 8).Style.Font.Size = 10
                .Cells(7, 8, 7, 8).Style.Font.Name = "Segoe UI"
                .Cells(7, 8, 7, 8).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 4, 8, 4).Value = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle1").ToString & ")"
                .Cells(8, 4, 8, 4).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 4, 8, 4).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 4, 8, 4).Merge = True
                .Cells(8, 4, 8, 4).Style.Font.Size = 10
                .Cells(8, 4, 8, 4).Style.Font.Name = "Segoe UI"
                .Cells(8, 4, 8, 4).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 5, 8, 5).Value = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle2").ToString & ")"
                .Cells(8, 5, 8, 5).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 5, 8, 5).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 5, 8, 5).Merge = True
                .Cells(8, 5, 8, 5).Style.Font.Size = 10
                .Cells(8, 5, 8, 5).Style.Font.Name = "Segoe UI"
                .Cells(8, 5, 8, 5).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 6, 8, 6).Value = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle3").ToString & ")"
                .Cells(8, 6, 8, 6).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 6, 8, 6).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 6, 8, 6).Merge = True
                .Cells(8, 6, 8, 6).Style.Font.Size = 10
                .Cells(8, 6, 8, 6).Style.Font.Name = "Segoe UI"
                .Cells(8, 6, 8, 6).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 7, 8, 7).Value = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle4").ToString & ")"
                .Cells(8, 7, 8, 7).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 7, 8, 7).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 7, 8, 7).Merge = True
                .Cells(8, 7, 8, 7).Style.Font.Size = 10
                .Cells(8, 7, 8, 7).Style.Font.Name = "Segoe UI"
                .Cells(8, 7, 8, 7).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 8, 8, 8).Value = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle5").ToString & ")"
                .Cells(8, 8, 8, 8).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 8, 8, 8).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 8, 8, 8).Merge = True
                .Cells(8, 8, 8, 8).Style.Font.Size = 10
                .Cells(8, 8, 8, 8).Style.Font.Name = "Segoe UI"
                .Cells(7, 8, 8, 8).Style.Font.Color.SetColor(Color.White)


                .Cells(6, 9, 6, 13).Value = "Shift2"
                .Cells(6, 9, 6, 13).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 9, 6, 13).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 9, 6, 13).Merge = True
                .Cells(6, 9, 6, 13).Style.Font.Size = 10
                .Cells(6, 9, 6, 13).Style.Font.Name = "Segoe UI"
                .Cells(6, 9, 6, 13).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 9, 7, 9).Value = "1"
                .Cells(7, 9, 7, 9).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 9, 7, 9).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 9, 7, 9).Merge = True
                .Cells(7, 9, 7, 9).Style.Font.Size = 10
                .Cells(7, 9, 7, 9).Style.Font.Name = "Segoe UI"
                .Cells(7, 9, 7, 9).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 10, 7, 10).Value = "2"
                .Cells(7, 10, 7, 10).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 10, 7, 10).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 10, 7, 10).Merge = True
                .Cells(7, 10, 7, 10).Style.Font.Size = 10
                .Cells(7, 10, 7, 10).Style.Font.Name = "Segoe UI"
                .Cells(7, 10, 7, 10).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 11, 7, 11).Value = "3"
                .Cells(7, 11, 7, 11).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 11, 7, 11).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 11, 7, 11).Merge = True
                .Cells(7, 11, 7, 11).Style.Font.Size = 10
                .Cells(7, 11, 7, 11).Style.Font.Name = "Segoe UI"
                .Cells(7, 11, 7, 11).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 12, 7, 12).Value = "4"
                .Cells(7, 12, 7, 12).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 12, 7, 12).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 12, 7, 12).Merge = True
                .Cells(7, 12, 7, 12).Style.Font.Size = 10
                .Cells(7, 12, 7, 12).Style.Font.Name = "Segoe UI"
                .Cells(7, 12, 7, 12).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 13, 7, 13).Value = "5"
                .Cells(7, 13, 7, 13).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 13, 7, 13).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 13, 7, 13).Merge = True
                .Cells(7, 13, 7, 13).Style.Font.Size = 10
                .Cells(7, 13, 7, 13).Style.Font.Name = "Segoe UI"
                .Cells(7, 13, 7, 13).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 9, 8, 9).Value = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle1").ToString & ")"
                .Cells(8, 9, 8, 9).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 9, 8, 9).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 9, 8, 9).Merge = True
                .Cells(8, 9, 8, 9).Style.Font.Size = 10
                .Cells(8, 9, 8, 9).Style.Font.Name = "Segoe UI"
                .Cells(8, 9, 8, 9).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 10, 8, 10).Value = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle2").ToString & ")"
                .Cells(8, 10, 8, 10).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 10, 8, 10).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 10, 8, 10).Merge = True
                .Cells(8, 10, 8, 10).Style.Font.Size = 10
                .Cells(8, 10, 8, 10).Style.Font.Name = "Segoe UI"
                .Cells(8, 10, 8, 10).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 11, 8, 11).Value = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle3").ToString & ")"
                .Cells(8, 11, 8, 11).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 11, 8, 11).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 11, 8, 11).Merge = True
                .Cells(8, 11, 8, 11).Style.Font.Size = 10
                .Cells(8, 11, 8, 11).Style.Font.Name = "Segoe UI"
                .Cells(8, 11, 8, 11).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 12, 8, 12).Value = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle4").ToString & ")"
                .Cells(8, 12, 8, 12).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 12, 8, 12).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 12, 8, 12).Merge = True
                .Cells(8, 12, 8, 12).Style.Font.Size = 10
                .Cells(8, 12, 8, 12).Style.Font.Name = "Segoe UI"
                .Cells(8, 12, 8, 12).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 13, 8, 13).Value = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle5").ToString & ")"
                .Cells(8, 13, 8, 13).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 13, 8, 13).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 13, 8, 13).Merge = True
                .Cells(8, 13, 8, 13).Style.Font.Size = 10
                .Cells(8, 13, 8, 13).Style.Font.Name = "Segoe UI"
                .Cells(8, 13, 8, 13).Style.Font.Color.SetColor(Color.White)

                .Cells(6, 14, 6, 18).Value = "Shift3"
                .Cells(6, 14, 6, 18).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(6, 14, 6, 18).Style.VerticalAlignment = VertAlignment.Center
                .Cells(6, 14, 6, 18).Merge = True
                .Cells(6, 14, 6, 18).Style.Font.Size = 10
                .Cells(6, 14, 6, 18).Style.Font.Name = "Segoe UI"
                .Cells(6, 14, 6, 18).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 14, 7, 14).Value = "1"
                .Cells(7, 14, 7, 14).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 14, 7, 14).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 14, 7, 14).Merge = True
                .Cells(7, 14, 7, 14).Style.Font.Size = 10
                .Cells(7, 14, 7, 14).Style.Font.Name = "Segoe UI"
                .Cells(7, 14, 7, 14).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 15, 7, 15).Value = "2"
                .Cells(7, 15, 7, 15).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 15, 7, 15).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 15, 7, 15).Merge = True
                .Cells(7, 15, 7, 15).Style.Font.Size = 10
                .Cells(7, 15, 7, 15).Style.Font.Name = "Segoe UI"
                .Cells(7, 15, 7, 15).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 16, 7, 16).Value = "3"
                .Cells(7, 16, 7, 16).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 16, 7, 16).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 16, 7, 16).Merge = True
                .Cells(7, 16, 7, 16).Style.Font.Size = 10
                .Cells(7, 16, 7, 16).Style.Font.Name = "Segoe UI"
                .Cells(7, 16, 7, 16).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 17, 7, 17).Value = "4"
                .Cells(7, 17, 7, 17).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 17, 7, 17).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 17, 7, 17).Merge = True
                .Cells(7, 17, 7, 17).Style.Font.Size = 10
                .Cells(7, 17, 7, 17).Style.Font.Name = "Segoe UI"
                .Cells(7, 17, 7, 17).Style.Font.Color.SetColor(Color.White)

                .Cells(7, 18, 7, 18).Value = "5"
                .Cells(7, 18, 7, 18).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(7, 18, 7, 18).Style.VerticalAlignment = VertAlignment.Center
                .Cells(7, 18, 7, 18).Merge = True
                .Cells(7, 18, 7, 18).Style.Font.Size = 10
                .Cells(7, 18, 7, 18).Style.Font.Name = "Segoe UI"
                .Cells(7, 18, 7, 18).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 14, 8, 14).Value = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle1").ToString & ")"
                .Cells(8, 14, 8, 14).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 14, 8, 14).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 14, 8, 14).Merge = True
                .Cells(8, 14, 8, 14).Style.Font.Size = 10
                .Cells(8, 14, 8, 14).Style.Font.Name = "Segoe UI"
                .Cells(8, 14, 8, 14).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 15, 8, 15).Value = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle2").ToString & ")"
                .Cells(8, 15, 8, 15).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 15, 8, 15).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 15, 8, 15).Merge = True
                .Cells(8, 15, 8, 15).Style.Font.Size = 10
                .Cells(8, 15, 8, 15).Style.Font.Name = "Segoe UI"
                .Cells(8, 15, 8, 15).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 16, 8, 16).Value = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle3").ToString & ")"
                .Cells(8, 16, 8, 16).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 16, 8, 16).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 16, 8, 16).Merge = True
                .Cells(8, 16, 8, 16).Style.Font.Size = 10
                .Cells(8, 16, 8, 16).Style.Font.Name = "Segoe UI"
                .Cells(8, 16, 8, 16).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 17, 8, 17).Value = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle4").ToString & ")"
                .Cells(8, 17, 8, 17).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 17, 8, 17).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 17, 8, 17).Merge = True
                .Cells(8, 17, 8, 17).Style.Font.Size = 10
                .Cells(8, 17, 8, 17).Style.Font.Name = "Segoe UI"
                .Cells(8, 17, 8, 17).Style.Font.Color.SetColor(Color.White)

                .Cells(8, 18, 8, 18).Value = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle5").ToString & ")"
                .Cells(8, 18, 8, 18).Style.HorizontalAlignment = HorzAlignment.Far
                .Cells(8, 18, 8, 18).Style.VerticalAlignment = VertAlignment.Center
                .Cells(8, 18, 8, 18).Merge = True
                .Cells(8, 18, 8, 18).Style.Font.Size = 10
                .Cells(8, 18, 8, 18).Style.Font.Name = "Segoe UI"
                .Cells(8, 18, 8, 18).Style.Font.Color.SetColor(Color.White)

                For i = 0 To Rpt.Tables(0).Rows.Count - 1
                    .Cells(i + 9, 1, i + 9, 1).Value = Rpt.Tables(0).Rows(i)("SubLineID")
                    .Cells(i + 9, 1, i + 9, 1).Style.WrapText = True
                    .Cells(i + 9, 1, i + 9, 1).Style.Font.Size = 10
                    .Cells(i + 9, 1, i + 9, 1).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 2, i + 9, 2).Value = Rpt.Tables(0).Rows(i)("PartID")
                    .Cells(i + 9, 2, i + 9, 2).Style.WrapText = True
                    .Cells(i + 9, 2, i + 9, 2).Style.Font.Size = 10
                    .Cells(i + 9, 2, i + 9, 2).Style.Font.Name = "Segoe UI"

                    .Cells(i + 9, 3, i + 9, 3).Value = Rpt.Tables(0).Rows(i)("PartName")
                    .Cells(i + 9, 3, i + 9, 3).Style.WrapText = True
                    .Cells(i + 9, 3, i + 9, 3).Style.Font.Size = 10
                    .Cells(i + 9, 3, i + 9, 3).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift1Cycle1") = "NG" Then
                        .Cells(i + 9, 4, i + 9, 4).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 4, i + 9, 4).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 4, i + 9, 4).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift1Cycle1") = "TC" Or Rpt.Tables(0).Rows(i)("Shift1Cycle1") = "D" Or Rpt.Tables(0).Rows(i)("Shift1Cycle1") = "MT" Or Rpt.Tables(0).Rows(i)("Shift1Cycle1") = "O" Then
                        .Cells(i + 9, 4, i + 9, 4).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 4, i + 9, 4).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift1Cycle1") = "-" Then
                        .Cells(i + 9, 4, i + 9, 4).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 4, i + 9, 4).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 4, i + 9, 4).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 4, i + 9, 4).Value = Rpt.Tables(0).Rows(i)("Shift1Cycle1")
                    .Cells(i + 9, 4, i + 9, 4).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 4, i + 9, 4).Style.WrapText = True
                    .Cells(i + 9, 4, i + 9, 4).Style.Font.Size = 10
                    .Cells(i + 9, 4, i + 9, 4).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift1Cycle2") = "NG" Then
                        .Cells(i + 9, 5, i + 9, 5).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 5, i + 9, 5).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 5, i + 9, 5).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift1Cycle2") = "TC" Or Rpt.Tables(0).Rows(i)("Shift1Cycle2") = "D" Or Rpt.Tables(0).Rows(i)("Shift1Cycle2") = "MT" Or Rpt.Tables(0).Rows(i)("Shift1Cycle2") = "O" Then
                        .Cells(i + 9, 5, i + 9, 5).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 5, i + 9, 5).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift1Cycle2") = "-" Then
                        .Cells(i + 9, 5, i + 9, 5).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 5, i + 9, 5).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 5, i + 9, 5).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 5, i + 9, 5).Value = Rpt.Tables(0).Rows(i)("Shift1Cycle2")
                    .Cells(i + 9, 5, i + 9, 5).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 5, i + 9, 5).Style.WrapText = True
                    .Cells(i + 9, 5, i + 9, 5).Style.Font.Size = 10
                    .Cells(i + 9, 5, i + 9, 5).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift1Cycle3") = "NG" Then
                        .Cells(i + 9, 6, i + 9, 6).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 6, i + 9, 6).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 6, i + 9, 6).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift1Cycle3") = "TC" Or Rpt.Tables(0).Rows(i)("Shift1Cycle3") = "D" Or Rpt.Tables(0).Rows(i)("Shift1Cycle3") = "MT" Or Rpt.Tables(0).Rows(i)("Shift1Cycle3") = "O" Then
                        .Cells(i + 9, 6, i + 9, 6).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 6, i + 9, 6).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift1Cycle3") = "-" Then
                        .Cells(i + 9, 6, i + 9, 6).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 6, i + 9, 6).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 6, i + 9, 6).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 6, i + 9, 6).Value = Rpt.Tables(0).Rows(i)("Shift1Cycle3")
                    .Cells(i + 9, 6, i + 9, 6).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 6, i + 9, 6).Style.WrapText = True
                    .Cells(i + 9, 6, i + 9, 6).Style.Font.Size = 10
                    .Cells(i + 9, 6, i + 9, 6).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift1Cycle4") = "NG" Then
                        .Cells(i + 9, 7, i + 9, 7).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 7, i + 9, 7).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 7, i + 9, 7).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift1Cycle4") = "TC" Or Rpt.Tables(0).Rows(i)("Shift1Cycle4") = "D" Or Rpt.Tables(0).Rows(i)("Shift1Cycle4") = "MT" Or Rpt.Tables(0).Rows(i)("Shift1Cycle4") = "O" Then
                        .Cells(i + 9, 7, i + 9, 7).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 7, i + 9, 7).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift1Cycle4") = "-" Then
                        .Cells(i + 9, 7, i + 9, 7).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 7, i + 9, 7).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 7, i + 9, 7).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 7, i + 9, 7).Value = Rpt.Tables(0).Rows(i)("Shift1Cycle4")
                    .Cells(i + 9, 7, i + 9, 7).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 7, i + 9, 7).Style.WrapText = True
                    .Cells(i + 9, 7, i + 9, 7).Style.Font.Size = 10
                    .Cells(i + 9, 7, i + 9, 7).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift1Cycle5") = "NG" Then
                        .Cells(i + 9, 8, i + 9, 8).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 8, i + 9, 8).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 8, i + 9, 8).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift1Cycle5") = "TC" Or Rpt.Tables(0).Rows(i)("Shift1Cycle5") = "D" Or Rpt.Tables(0).Rows(i)("Shift1Cycle5") = "MT" Or Rpt.Tables(0).Rows(i)("Shift1Cycle5") = "O" Then
                        .Cells(i + 9, 8, i + 9, 8).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 8, i + 9, 8).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift1Cycle5") = "-" Then
                        .Cells(i + 9, 8, i + 9, 8).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 8, i + 9, 8).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 8, i + 9, 8).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 8, i + 9, 8).Value = Rpt.Tables(0).Rows(i)("Shift1Cycle5")
                    .Cells(i + 9, 8, i + 9, 8).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 8, i + 9, 8).Style.WrapText = True
                    .Cells(i + 9, 8, i + 9, 8).Style.Font.Size = 10
                    .Cells(i + 9, 8, i + 9, 8).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift2Cycle1") = "NG" Then
                        .Cells(i + 9, 9, i + 9, 9).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 9, i + 9, 9).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 9, i + 9, 9).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift2Cycle1") = "TC" Or Rpt.Tables(0).Rows(i)("Shift2Cycle1") = "D" Or Rpt.Tables(0).Rows(i)("Shift2Cycle1") = "MT" Or Rpt.Tables(0).Rows(i)("Shift2Cycle1") = "O" Then
                        .Cells(i + 9, 9, i + 9, 9).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 9, i + 9, 9).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift2Cycle1") = "-" Then
                        .Cells(i + 9, 9, i + 9, 9).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 9, i + 9, 9).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 9, i + 9, 9).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 9, i + 9, 9).Value = Rpt.Tables(0).Rows(i)("Shift2Cycle1")
                    .Cells(i + 9, 9, i + 9, 9).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 9, i + 9, 9).Style.WrapText = True
                    .Cells(i + 9, 9, i + 9, 9).Style.Font.Size = 10
                    .Cells(i + 9, 9, i + 9, 9).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift2Cycle2") = "NG" Then
                        .Cells(i + 9, 10, i + 9, 10).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 10, i + 9, 10).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 10, i + 9, 10).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift2Cycle2") = "TC" Or Rpt.Tables(0).Rows(i)("Shift2Cycle2") = "D" Or Rpt.Tables(0).Rows(i)("Shift2Cycle2") = "MT" Or Rpt.Tables(0).Rows(i)("Shift2Cycle2") = "O" Then
                        .Cells(i + 9, 10, i + 9, 10).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 10, i + 9, 10).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift2Cycle2") = "-" Then
                        .Cells(i + 9, 10, i + 9, 10).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 10, i + 9, 10).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 10, i + 9, 10).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 10, i + 9, 10).Value = Rpt.Tables(0).Rows(i)("Shift2Cycle2")
                    .Cells(i + 9, 10, i + 9, 10).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 10, i + 9, 10).Style.WrapText = True
                    .Cells(i + 9, 10, i + 9, 10).Style.Font.Size = 10
                    .Cells(i + 9, 10, i + 9, 10).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift2Cycle3") = "NG" Then
                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 11, i + 9, 11).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift2Cycle3") = "TC" Or Rpt.Tables(0).Rows(i)("Shift2Cycle3") = "D" Or Rpt.Tables(0).Rows(i)("Shift2Cycle3") = "MT" Or Rpt.Tables(0).Rows(i)("Shift2Cycle3") = "O" Then
                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift2Cycle3") = "-" Then
                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 11, i + 9, 11).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 11, i + 9, 11).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 11, i + 9, 11).Value = Rpt.Tables(0).Rows(i)("Shift2Cycle3")
                    .Cells(i + 9, 11, i + 9, 11).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 11, i + 9, 11).Style.WrapText = True
                    .Cells(i + 9, 11, i + 9, 11).Style.Font.Size = 10
                    .Cells(i + 9, 11, i + 9, 11).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift2Cycle4") = "NG" Then
                        .Cells(i + 9, 12, i + 9, 12).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 12, i + 9, 12).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 12, i + 9, 12).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift2Cycle4") = "TC" Or Rpt.Tables(0).Rows(i)("Shift2Cycle4") = "D" Or Rpt.Tables(0).Rows(i)("Shift2Cycle4") = "MT" Or Rpt.Tables(0).Rows(i)("Shift2Cycle4") = "O" Then
                        .Cells(i + 9, 12, i + 9, 12).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 12, i + 9, 12).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift2Cycle4") = "-" Then
                        .Cells(i + 9, 12, i + 9, 12).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 12, i + 9, 12).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 12, i + 9, 12).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 12, i + 9, 12).Value = Rpt.Tables(0).Rows(i)("Shift2Cycle4")
                    .Cells(i + 9, 12, i + 9, 12).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 12, i + 9, 12).Style.WrapText = True
                    .Cells(i + 9, 12, i + 9, 12).Style.Font.Size = 10
                    .Cells(i + 9, 12, i + 9, 12).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift2Cycle5") = "NG" Then
                        .Cells(i + 9, 13, i + 9, 13).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 13, i + 9, 13).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 13, i + 9, 13).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift2Cycle5") = "TC" Or Rpt.Tables(0).Rows(i)("Shift2Cycle5") = "D" Or Rpt.Tables(0).Rows(i)("Shift2Cycle5") = "MT" Or Rpt.Tables(0).Rows(i)("Shift2Cycle5") = "O" Then
                        .Cells(i + 9, 13, i + 9, 13).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 13, i + 9, 13).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift2Cycle5") = "-" Then
                        .Cells(i + 9, 13, i + 9, 13).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 13, i + 9, 13).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 13, i + 9, 13).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 13, i + 9, 13).Value = Rpt.Tables(0).Rows(i)("Shift2Cycle5")
                    .Cells(i + 9, 13, i + 9, 13).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 13, i + 9, 13).Style.WrapText = True
                    .Cells(i + 9, 13, i + 9, 13).Style.Font.Size = 10
                    .Cells(i + 9, 13, i + 9, 13).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift3Cycle1") = "NG" Then
                        .Cells(i + 9, 14, i + 9, 14).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 14, i + 9, 14).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 14, i + 9, 14).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift3Cycle1") = "TC" Or Rpt.Tables(0).Rows(i)("Shift3Cycle1") = "D" Or Rpt.Tables(0).Rows(i)("Shift3Cycle1") = "MT" Or Rpt.Tables(0).Rows(i)("Shift3Cycle1") = "O" Then
                        .Cells(i + 9, 14, i + 9, 14).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 14, i + 9, 14).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift3Cycle1") = "-" Then
                        .Cells(i + 9, 14, i + 9, 14).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 14, i + 9, 14).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 14, i + 9, 14).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 14, i + 9, 14).Value = Rpt.Tables(0).Rows(i)("Shift3Cycle1")
                    .Cells(i + 9, 14, i + 9, 14).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 14, i + 9, 14).Style.WrapText = True
                    .Cells(i + 9, 14, i + 9, 14).Style.Font.Size = 10
                    .Cells(i + 9, 14, i + 9, 14).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift3Cycle2") = "NG" Then
                        .Cells(i + 9, 15, i + 9, 15).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 15, i + 9, 15).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 15, i + 9, 15).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift3Cycle2") = "TC" Or Rpt.Tables(0).Rows(i)("Shift3Cycle2") = "D" Or Rpt.Tables(0).Rows(i)("Shift3Cycle2") = "MT" Or Rpt.Tables(0).Rows(i)("Shift3Cycle2") = "O" Then
                        .Cells(i + 9, 15, i + 9, 15).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 15, i + 9, 15).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift3Cycle2") = "-" Then
                        .Cells(i + 9, 15, i + 9, 15).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 15, i + 9, 15).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 15, i + 9, 15).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 15, i + 9, 15).Value = Rpt.Tables(0).Rows(i)("Shift3Cycle2")
                    .Cells(i + 9, 15, i + 9, 15).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 15, i + 9, 15).Style.WrapText = True
                    .Cells(i + 9, 15, i + 9, 15).Style.Font.Size = 10
                    .Cells(i + 9, 15, i + 9, 15).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift3Cycle3") = "NG" Then
                        .Cells(i + 9, 16, i + 9, 16).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 16, i + 9, 16).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 16, i + 9, 16).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift3Cycle3") = "TC" Or Rpt.Tables(0).Rows(i)("Shift3Cycle3") = "D" Or Rpt.Tables(0).Rows(i)("Shift3Cycle3") = "MT" Or Rpt.Tables(0).Rows(i)("Shift3Cycle3") = "O" Then
                        .Cells(i + 9, 16, i + 9, 16).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 16, i + 9, 16).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift3Cycle3") = "-" Then
                        .Cells(i + 9, 16, i + 9, 16).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 16, i + 9, 16).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 16, i + 9, 16).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 16, i + 9, 16).Value = Rpt.Tables(0).Rows(i)("Shift3Cycle3")
                    .Cells(i + 9, 16, i + 9, 16).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 16, i + 9, 16).Style.WrapText = True
                    .Cells(i + 9, 16, i + 9, 16).Style.Font.Size = 10
                    .Cells(i + 9, 16, i + 9, 16).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift3Cycle4") = "NG" Then
                        .Cells(i + 9, 17, i + 9, 17).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 17, i + 9, 17).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 17, i + 9, 17).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift3Cycle4") = "TC" Or Rpt.Tables(0).Rows(i)("Shift3Cycle4") = "D" Or Rpt.Tables(0).Rows(i)("Shift3Cycle4") = "MT" Or Rpt.Tables(0).Rows(i)("Shift3Cycle4") = "O" Then
                        .Cells(i + 9, 17, i + 9, 17).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 17, i + 9, 17).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift3Cycle4") = "-" Then
                        .Cells(i + 9, 17, i + 9, 17).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 17, i + 9, 17).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 17, i + 9, 17).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 17, i + 9, 17).Value = Rpt.Tables(0).Rows(i)("Shift3Cycle4")
                    .Cells(i + 9, 17, i + 9, 17).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 17, i + 9, 17).Style.WrapText = True
                    .Cells(i + 9, 17, i + 9, 17).Style.Font.Size = 10
                    .Cells(i + 9, 17, i + 9, 17).Style.Font.Name = "Segoe UI"

                    If Rpt.Tables(0).Rows(i)("Shift3Cycle5") = "NG" Then
                        .Cells(i + 9, 18, i + 9, 18).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 18, i + 9, 18).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 18, i + 9, 18).Style.Font.Color.SetColor(Color.Yellow)
                    ElseIf Rpt.Tables(0).Rows(i)("Shift3Cycle5") = "TC" Or Rpt.Tables(0).Rows(i)("Shift3Cycle5") = "D" Or Rpt.Tables(0).Rows(i)("Shift3Cycle5") = "MT" Or Rpt.Tables(0).Rows(i)("Shift3Cycle5") = "O" Then
                        .Cells(i + 9, 18, i + 9, 18).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 18, i + 9, 18).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffff00"))
                    ElseIf Rpt.Tables(0).Rows(i)("Shift3Cycle5") = "-" Then
                        .Cells(i + 9, 18, i + 9, 18).Style.Fill.PatternType = Style.ExcelFillStyle.Solid
                        .Cells(i + 9, 18, i + 9, 18).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FF0000"))
                        .Cells(i + 9, 17, i + 9, 18).Style.Font.Color.SetColor(Color.Red)
                    End If

                    .Cells(i + 9, 18, i + 9, 18).Value = Rpt.Tables(0).Rows(i)("Shift3Cycle5")
                    .Cells(i + 9, 18, i + 9, 18).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                    .Cells(i + 9, 18, i + 9, 18).Style.WrapText = True
                    .Cells(i + 9, 18, i + 9, 18).Style.Font.Size = 10
                    .Cells(i + 9, 18, i + 9, 18).Style.Font.Name = "Segoe UI"
                Next
                FormatExcel(ws, Rpt)
                InsertHeader(ws, pDate, pLineID, pPartID, pPartName, pQCSStatus)
            End With

            exl.Save()
            DevExpress.Web.ASPxWebControl.RedirectOnCallback("Download/" & fi.Name)
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, pErr, 1)
        End Try
    End Sub

    Private Sub InsertHeader(ByVal pExl As ExcelWorksheet, ByVal pDate As Date, ByVal pLineID As String, ByVal pPartID As String, ByVal pPartName As String, ByVal pQCSStatus As String)
        With pExl
            .Cells(1, 1, 1, 1).Value = "Statistics Quality Control Result Monitoring"
            .Cells(1, 1, 1, 18).Merge = True
            .Cells(1, 1, 1, 18).Style.HorizontalAlignment = HorzAlignment.Far
            .Cells(1, 1, 1, 18).Style.VerticalAlignment = VertAlignment.Center
            .Cells(1, 1, 1, 18).Style.Font.Bold = True
            .Cells(1, 1, 1, 18).Style.Font.Size = 16
            .Cells(1, 1, 1, 18).Style.Font.Name = "Segoe UI"

            .Cells(3, 1, 3, 1).Value = "Date"
            .Cells(3, 1, 3, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 1, 3, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 1, 3, 1).Style.Font.Size = 10
            .Cells(3, 1, 3, 1).Style.Font.Name = "Segoe UI"

            .Cells(3, 3, 3, 3).Value = ": " & Format(pDate, "dd-MM-yyyy")
            .Cells(3, 3, 3, 3).Merge = True
            .Cells(3, 3, 3, 3).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 3, 3, 3).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 3, 3, 3).Style.Font.Size = 10
            .Cells(3, 3, 3, 3).Style.Font.Name = "Segoe UI"

            .Cells(4, 1, 4, 1).Value = "Line"
            .Cells(4, 1, 4, 1).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 1, 4, 1).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 1, 4, 1).Style.Font.Size = 10
            .Cells(4, 1, 4, 1).Style.Font.Name = "Segoe UI"

            .Cells(4, 3, 4, 3).Value = ": " & pLineID
            .Cells(4, 3, 4, 3).Merge = True
            .Cells(4, 3, 4, 3).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 3, 4, 3).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 3, 4, 3).Style.Font.Size = 10
            .Cells(4, 3, 4, 3).Style.Font.Name = "Segoe UI"

            .Cells(3, 5, 3, 5).Value = "Part No"
            .Cells(3, 5, 3, 5).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 5, 3, 5).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 5, 3, 5).Style.Font.Size = 10
            .Cells(3, 5, 3, 5).Style.Font.Name = "Segoe UI"

            .Cells(3, 9, 3, 9).Value = ": " & pPartID
            .Cells(3, 9, 3, 9).Merge = True
            .Cells(3, 9, 3, 9).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 9, 3, 9).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 9, 3, 9).Style.Font.Size = 10
            .Cells(3, 9, 3, 9).Style.Font.Name = "Segoe UI"

            .Cells(4, 5, 4, 5).Value = "Part Name"
            .Cells(4, 5, 4, 5).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 5, 4, 5).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 5, 4, 5).Style.Font.Size = 10
            .Cells(4, 5, 4, 5).Style.Font.Name = "Segoe UI"

            .Cells(4, 9, 4, 9).Value = ": " & pPartName
            .Cells(4, 9, 4, 9).Merge = True
            .Cells(4, 9, 4, 9).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(4, 9, 4, 9).Style.VerticalAlignment = VertAlignment.Center
            .Cells(4, 9, 4, 9).Style.Font.Size = 10
            .Cells(4, 9, 4, 9).Style.Font.Name = "Segoe UI"

            .Cells(3, 13, 3, 13).Value = "QCS Status"
            .Cells(3, 13, 3, 13).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 13, 3, 13).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 13, 3, 13).Style.Font.Size = 10
            .Cells(3, 13, 3, 13).Style.Font.Name = "Segoe UI"

            .Cells(3, 17, 3, 17).Value = ": " & pQCSStatus
            .Cells(3, 17, 3, 17).Merge = True
            .Cells(3, 17, 3, 17).Style.HorizontalAlignment = HorzAlignment.Near
            .Cells(3, 17, 3, 17).Style.VerticalAlignment = VertAlignment.Center
            .Cells(3, 17, 3, 17).Style.Font.Size = 10
            .Cells(3, 17, 3, 17).Style.Font.Name = "Segoe UI"

            '.Cells(4, 13, 4, 13).Value = "QCS Status"
            '.Cells(4, 13, 4, 13).Style.HorizontalAlignment = HorzAlignment.Near
            '.Cells(4, 13, 4, 13).Style.VerticalAlignment = VertAlignment.Center
            '.Cells(4, 13, 4, 13).Style.Font.Size = 10
            '.Cells(4, 13, 4, 13).Style.Font.Name = "Segoe UI"

            '.Cells(4, 17, 4, 17).Value = ": " & pQCSStatus
            '.Cells(4, 17, 4, 17).Merge = True
            '.Cells(4, 17, 4, 17).Style.HorizontalAlignment = HorzAlignment.Near
            '.Cells(4, 17, 4, 17).Style.VerticalAlignment = VertAlignment.Center
            '.Cells(4, 17, 4, 17).Style.Font.Size = 10
            '.Cells(4, 17, 4, 17).Style.Font.Name = "Segoe UI"

        End With
    End Sub

    Private Sub FormatExcel(ByVal pExl As ExcelWorksheet, ByVal pRpt As DataSet)
        With pExl
            .Column(1).Width = 8
            .Column(2).Width = 17
            .Column(3).Width = 28
            .Column(4).Width = 7
            .Column(5).Width = 7
            .Column(6).Width = 7
            .Column(7).Width = 7

            .Column(8).Width = 7
            .Column(9).Width = 7
            .Column(10).Width = 7
            .Column(11).Width = 7
            .Column(12).Width = 7
            .Column(13).Width = 7
            .Column(14).Width = 7
            .Column(15).Width = 7
            .Column(16).Width = 7
            .Column(17).Width = 7
            .Column(18).Width = 7

            Dim rgAll As ExcelRange = .Cells(6, 1, pRpt.Tables(0).Rows.Count + 8, 18)
            DrawAllBorders(rgAll)

            Dim rgHeader As ExcelRange = .Cells(6, 1, 8, 18)
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