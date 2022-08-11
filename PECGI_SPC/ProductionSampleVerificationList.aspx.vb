Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.Data
Imports OfficeOpenXml

Public Class ProductionSampleVerificationList
    Inherits System.Web.UI.Page

#Region "Declaration"
    Dim pUser As String = ""
    Dim Factory_Sel As String = "1"
    Dim ItemType_Sel As String = "2"
    Dim Line_Sel As String = "3"
    Dim ItemCheck_Sel As String = "4"

    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "B040")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If

        sGlobal.getMenu("B040")
        Master.SiteTitle = sGlobal.menuName
        'AuthUpdate = sGlobal.Auth_UserUpdate(pUser, "B040")
        show_error(MsgTypeEnum.Info, "", 0)
        Dim data As New clsProductionSampleVerificationList()

        up_fillcombo(Factory_Sel, data, pUser)
        up_fillcombo(ItemType_Sel, data, pUser)
        up_fillcombo(Line_Sel, data, pUser)
        up_fillcombo(ItemCheck_Sel, data, pUser)
        'up_GridLoadShiftCycle()
        'up_GridLoad()
    End Sub

#Region "Procedure"
    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        GridMenu.JSProperties("cp_message") = ErrMsg
        GridMenu.JSProperties("cp_type") = msgType
        GridMenu.JSProperties("cp_val") = pVal
    End Sub

    Private Sub up_fillcombo(ByVal pStstus As String, data As clsProductionSampleVerificationList, ByVal pUser As String)
        Dim ErrMsg As String = ""
        Dim dsline As DataTable
        dsline = clsProductionSampleVerificationListDB.FillCombo(pStstus, data, ErrMsg)
        If ErrMsg = "" Then
            If pStstus = "1" Then
                cboFactory.DataSource = dsline
                cboFactory.DataBind()
            ElseIf pStstus = "2" Then
                cboItemType.DataSource = dsline
                cboItemType.DataBind()
            ElseIf pStstus = "3" Then
                cboLineID.DataSource = dsline
                cboLineID.DataBind()
            ElseIf pStstus = "4" Then
                cboItemCheck.DataSource = dsline
                cboItemCheck.DataBind()
            End If
        Else
            show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
            Exit Sub
        End If
    End Sub

    Private Sub up_GridLoadShiftCycle()
        'Dim ErrMsg As String = ""
        'Dim Menu As DataSet
        'Menu = ClsQCSResultMonitoringDB.GetTime(ErrMsg)
        'GridMenu.Columns("Shift1Cycle1").Caption = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle1").ToString & ")"
        'GridMenu.Columns("Shift1Cycle2").Caption = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle2").ToString & ")"
        'GridMenu.Columns("Shift1Cycle3").Caption = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle3").ToString & ")"
        'GridMenu.Columns("Shift1Cycle4").Caption = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle4").ToString & ")"
        'GridMenu.Columns("Shift1Cycle5").Caption = "(" & Menu.Tables(0).Rows(0)("Shift1Cycle5").ToString & ")"

        'GridMenu.Columns("Shift2Cycle1").Caption = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle1").ToString & ")"
        'GridMenu.Columns("Shift2Cycle2").Caption = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle2").ToString & ")"
        'GridMenu.Columns("Shift2Cycle3").Caption = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle3").ToString & ")"
        'GridMenu.Columns("Shift2Cycle4").Caption = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle4").ToString & ")"
        'GridMenu.Columns("Shift2Cycle5").Caption = "(" & Menu.Tables(0).Rows(0)("Shift2Cycle5").ToString & ")"

        'GridMenu.Columns("Shift3Cycle1").Caption = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle1").ToString & ")"
        'GridMenu.Columns("Shift3Cycle2").Caption = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle2").ToString & ")"
        'GridMenu.Columns("Shift3Cycle3").Caption = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle3").ToString & ")"
        'GridMenu.Columns("Shift3Cycle4").Caption = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle4").ToString & ")"
        'GridMenu.Columns("Shift3Cycle5").Caption = "(" & Menu.Tables(0).Rows(0)("Shift3Cycle5").ToString & ")"
    End Sub

    'Private Sub up_GridLoad()
    '    Dim ErrMsg As String = ""
    '    Dim Menu As DataSet
    '    Menu = ClsQCSResultMonitoringDB.GetList(IIf(dtdate.Value = Nothing, "", Format(dtdate.Value, "yyyy-MM-dd")), IIf(cboLineID.Value = Nothing, "", cboLineID.Value), IIf(cbopartid.Value = Nothing, "", cbopartid.Value), IIf(cboqcsstatus.Value = Nothing, "ALL", cboqcsstatus.Value), pUser, ErrMsg)
    '    If ErrMsg = "" Then
    '        GridMenu.DataSource = Menu
    '        GridMenu.DataBind()
    '    Else
    '        show_error(MsgTypeEnum.ErrorMsg, ErrMsg, 1)
    '        Exit Sub
    '    End If

    'End Sub
#End Region

End Class