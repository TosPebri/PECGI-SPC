﻿Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Utils
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.Data
Imports OfficeOpenXml
Imports DevExpress.Web
Imports OfficeOpenXml.Style

Public Class ProdSampleVerification
    Inherits System.Web.UI.Page

#Region "Declaration"

    Dim pUser As String = ""
    Dim Factory_Sel As String = "1"
    Dim ItemType_Sel As String = "2"
    Dim Line_Sel As String = "3"
    Dim ItemCheck_Sel As String = "4"
    Dim Shift_Sel As String = "5"
    Dim Seq_Sel As String = "6"
    Dim User_Sel As String = "7"

    Dim GetHeader_ProdDate As String = "1"
    Dim GetHeader_ShifCode As String = "2"
    Dim GetHeader_Time As String = "3"
    Dim GetGridData As String = "4"
    Dim GetGridData_Activity As String = "5"
    Dim GetCharSetup As String = "6"
    Dim GetVerifyPrivilege As String = "7"

    Dim UCL As Decimal = 0
    Dim LCL As Decimal = 0
    Dim USL As Decimal = 0
    Dim LSL As Decimal = 0
    Dim ColumnBrowse As String = ""
    Dim VerifyStatus As String = "0"
    Dim DescIndex As String = ""

    Dim dt As DataTable
    Dim ds As DataSet


    'EXCEL DECLARE
    Dim row_GridTitle = 0
    Dim row_HeaderResult = 0
    Dim row_HeaderActivity = 0
    Dim row_CellResult = 0
    Dim row_CellActivity = 0

    Dim col_HeaderResult = 0
    Dim col_HeaderActivity = 0
    Dim col_CellResult = 0
    Dim col_CellActivity = 0

    Dim RowIndexName As String = ""
    '----------------'

    Dim prmFactoryCode = ""
    Dim prmItemType = ""
    Dim prmLineCode = ""
    Dim prmItemCheck = ""
    Dim prmProdDate = ""
    Dim prmShifCode = ""
    Dim prmSeqNo = ""

    Public AuthUpdate As Boolean = False
    Public AuthDelete As Boolean = False
    Public AuthAccess As Boolean = False

#End Region

#Region "StartForm"
    Private Sub Page_Init(ByVal sender As Object, ByVale As System.EventArgs) Handles Me.Init
        pUser = Session("user")
        AuthAccess = sGlobal.Auth_UserAccess(pUser, "B040")
        If AuthAccess = False Then
            Response.Redirect("~/Main.aspx")
        End If
        sGlobal.getMenu("B040")
        Master.SiteTitle = sGlobal.menuName
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("prm") IsNot Nothing Then
                Dim data = Request.QueryString("prm").ToString()
                prmFactoryCode = Session("prmFactoryCode")
                prmItemType = Session("prmItemType")
                prmLineCode = Session("prmLineCode")
                prmItemCheck = Session("prmItemCheck")
                prmProdDate = Session("prmProdDate")
                prmShifCode = Session("prmShiftCode")
                prmSeqNo = Session("prmSeqNo")

                dtProdDate.Value = Convert.ToDateTime(prmProdDate)
                dtProdDate.Enabled = False

                Dim ShiftCode = ""
                If prmShifCode = "1" Then
                    ShiftCode = "SH001"
                ElseIf prmShifCode = "2" Then
                    ShiftCode = "SH002"
                End If

                btnBrowse.Enabled = False
                btnClear.Enabled = False

                UpFillCombo()

                Up_GridLoad(prmFactoryCode, prmItemType, prmLineCode, prmItemCheck, Convert.ToDateTime(prmProdDate).ToString("yyyy-MM-dd"), ShiftCode, prmSeqNo)
                Up_GridLoadActivities(prmFactoryCode, prmItemType, prmLineCode, prmItemCheck, Convert.ToDateTime(prmProdDate).ToString("yyyy-MM-dd"), ShiftCode, prmSeqNo)

                'If VerifyStatus = 1 Then
                '    btnVerification.Enabled = True
                'End If
                Grid.JSProperties("cp_Verify") = VerifyStatus
            Else
                dtProdDate.Value = DateTime.Now
                UpFillCombo()
                Up_GridLoadActivities("", "", "", "", "", "", "")
                Grid.JSProperties("cp_Verify") = VerifyStatus
                btnBack.Visible = False
            End If

        End If
    End Sub
#End Region

#Region "Event"
    Private Sub Grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles Grid.CustomCallback
        Try
            Dim cls As New clsProdSampleVerification
            Dim msgErr As String = ""
            Dim pAction As String = Split(e.Parameters, "|")(0)

            If pAction = "Load" Then
                Dim Factory As String = HideValue.Get("FactoryCode")
                Dim Itemtype As String = HideValue.Get("ItemType_Code")
                Dim Line As String = HideValue.Get("LineCode")
                Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
                Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
                Dim Shift As String = HideValue.Get("ShiftCode")
                Dim Seq As String = HideValue.Get("Seq")

                Up_GridLoad(Factory, Itemtype, Line, ItemCheck, ProdDate, Shift, Seq)
                Grid.JSProperties("cp_Verify") = VerifyStatus
            ElseIf pAction = "Verify" Then
                Dim SpcResultID As String
                Dim Factory As String = HideValue.Get("FactoryCode")
                Dim Itemtype As String = HideValue.Get("ItemType_Code")
                Dim Line As String = HideValue.Get("LineCode")
                Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
                Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
                Dim Shift As String = HideValue.Get("ShiftCode")
                Dim Seq As String = HideValue.Get("Seq")

                cls.FactoryCode = Factory
                cls.ItemType_Code = Itemtype
                cls.LineCode = Line
                cls.ItemCheck_Code = ItemCheck
                cls.ProdDate = ProdDate
                cls.ShiftCode = Shift
                cls.Seq = Seq

                ds = clsProdSampleVerificationDB.GridLoad(GetCharSetup, cls)
                Dim dtColBrowse As DataTable = ds.Tables(0)
                If dtColBrowse.Rows.Count > 0 Then
                    SpcResultID = dtColBrowse.Rows(0)("SPCResultID")
                End If
                Verify(SpcResultID)
                Grid.JSProperties("cp_Verify") = VerifyStatus
            End If

        Catch ex As Exception
            show_errorGrid(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Sub GridMenu_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs) Handles GridMenu.CustomCallback
        Try
            Dim cls As New clsProdSampleVerification
            Dim msgErr As String = ""
            Dim pAction As String = Split(e.Parameters, "|")(0)

            If pAction = "Load" Then
                Dim Factory As String = HideValue.Get("FactoryCode")
                Dim Itemtype As String = HideValue.Get("ItemType_Code")
                Dim Line As String = HideValue.Get("LineCode")
                Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
                Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
                Dim Shift As String = HideValue.Get("ShiftCode")
                Dim Seq As String = HideValue.Get("Seq")

                Up_GridLoadActivities(Factory, Itemtype, Line, ItemCheck, ProdDate, Shift, Seq)
            ElseIf pAction = "Clear" Then
                'dt = clsProdSampleVerificationDB.LoadGrid(cls, msgErr)
                'GridMenu.DataSource = dt
                'GridMenu.DataBind()          
            End If

        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub
    Private Sub GridMenu_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridMenu.BeforeGetCallbackResult
        If GridMenu.IsNewRowEditing Then
            GridMenu.SettingsCommandButton.UpdateButton.Text = "Save"
        End If
    End Sub

    Private Sub Grid_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs) Handles Grid.HtmlDataCellPrepared
        Try
            If e.DataColumn.FieldName = "nDescIndex" Then
                DescIndex = e.CellValue
            ElseIf e.DataColumn.FieldName <> "nDesc" Then

                If DescIndex = "EachData" Then
                    If e.CellValue < LSL Or e.CellValue > USL Then
                        e.Cell.BackColor = Color.Red
                    ElseIf e.CellValue < LSL Or e.CellValue > UCL Then
                        e.Cell.BackColor = Color.Pink
                    End If
                ElseIf DescIndex = "XBar" Then
                    If e.CellValue < LSL Or e.CellValue > USL Then
                        e.Cell.BackColor = Color.Red
                    ElseIf e.CellValue < LSL Or e.CellValue > UCL Then
                        e.Cell.BackColor = Color.Yellow
                    End If
                ElseIf DescIndex = "Judgement" Then
                    If e.CellValue = "NG" Then
                        e.Cell.BackColor = Color.Red
                    End If
                ElseIf DescIndex = "Correction" Then
                    If e.CellValue = "C" Then
                        e.Cell.BackColor = Color.Orange
                    End If
                ElseIf DescIndex = "View" Then
                    e.Cell.ForeColor = Color.Blue
                End If

                If (e.DataColumn.FieldName = ColumnBrowse) Then
                    If DescIndex = "Verification" Then
                        If IsDBNull(e.CellValue) Then
                            e.Cell.BackColor = Color.Yellow
                        End If
                    End If

                End If

            End If
        Catch ex As Exception
            Throw New Exception("Error_EditingGrid !" & ex.Message)
        End Try
    End Sub
    Private Sub Grid_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles GridMenu.CellEditorInitialize
        If Not GridMenu.IsNewRowEditing Then
            If e.Column.FieldName = "FactoryCode" Or e.Column.FieldName = "ItemTypeCode" Or
                e.Column.FieldName = "LineCode" Or e.Column.FieldName = "ItemCheckCode" Or
                e.Column.FieldName = "ShiftCode" Or e.Column.FieldName = "ProdDate" Then
                e.Editor.ReadOnly = True
                e.Editor.ForeColor = Color.Silver
            End If
        End If

        Dim cls As New clsProdSampleVerification
        cls.User = pUser

        If e.Column.FieldName = "FactoryCode" Then
            Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
            up_FillcomboGrid(combo, Factory_Sel, cls)
            If GridMenu.IsEditing Then combo.Value = e.Value : HideValue.Set("FactoryCode", e.Value)
        ElseIf e.Column.FieldName = "ItemTypeCode" Then
            Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
            up_FillcomboGrid(combo, ItemType_Sel, cls)
            If GridMenu.IsEditing Then combo.Value = e.Value : HideValue.Set("ItemTypeCode", e.Value)
        ElseIf e.Column.FieldName = "LineCode" Then
            Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
            AddHandler combo.Callback, AddressOf GridChange_OnCallback
            If GridMenu.IsEditing Then
                cls.FactoryCode = HideValue.Get("FactoryCode")
                Call up_FillcomboGrid(combo, Line_Sel, cls)
                combo.Value = e.Value
            End If
        ElseIf e.Column.FieldName = "ItemCheckCode" Then
            Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
            AddHandler combo.Callback, AddressOf GridChange_OnCallback
            If GridMenu.IsEditing Then
                cls.FactoryCode = HideValue.Get("FactoryCode")
                cls.ItemType_Code = HideValue.Get("ItemTypeCode")
                cls.LineCode = HideValue.Get("LineCode")
                up_FillcomboGrid(combo, ItemCheck_Sel, cls)
                combo.Value = e.Value
                HideValue.Set("ItemCheckCode", e.Value)
            End If
        ElseIf e.Column.FieldName = "ShiftCode" Then
            Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
            AddHandler combo.Callback, AddressOf GridChange_OnCallback
            If GridMenu.IsEditing Then
                cls.FactoryCode = HideValue.Get("FactoryCode")
                cls.ItemType_Code = HideValue.Get("ItemTypeCode")
                cls.LineCode = HideValue.Get("LineCode")
                cls.ItemCheck_Code = HideValue.Get("ItemCheckCode")
                up_FillcomboGrid(combo, Shift_Sel, cls)
                combo.Value = e.Value
            End If
        ElseIf e.Column.FieldName = "PIC" Then
            Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
            up_FillcomboGrid(combo, User_Sel, cls)
        End If

    End Sub

    Private Sub GridChange_OnCallback(ByVal source As Object, ByVal e As CallbackEventArgsBase)
        Dim cmb As ASPxComboBox = source
        Dim cls As New clsProdSampleVerification
        Dim ItemTypeCode = ""
        Dim LineCode = ""
        Dim ItemCheckCode = ""

        Dim prm As String = Split(e.Parameter, "|")(0)
        Dim Sel As String = Split(e.Parameter, "|")(1)
        Dim FactoryCode As String = Split(e.Parameter, "|")(2)
        If prm = "ItemCheckCode" Then
            ItemTypeCode = Split(e.Parameter, "|")(3)
            LineCode = Split(e.Parameter, "|")(4)
        ElseIf prm = "ShiftCode" Then
            ItemTypeCode = Split(e.Parameter, "|")(3)
            LineCode = Split(e.Parameter, "|")(4)
            ItemCheckCode = Split(e.Parameter, "|")(5)
        End If


        With cls
            .User = pUser
            .FactoryCode = FactoryCode
            .ItemType_Code = ItemTypeCode
            .LineCode = LineCode
            .ItemCheck_Code = ItemCheckCode
        End With
        dt = clsProdSampleVerificationDB.FillCombo(Sel, cls)
        With cmb
            .Items.Clear() : .Columns.Clear()
            .DataSource = dt
            .DataBind()
        End With
    End Sub

    Protected Sub GridMenu_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles GridMenu.RowInserting
        e.Cancel = True

        Dim data As New clsProdSampleVerification With {
            .FactoryCode = e.NewValues("FactoryCode") & "",
            .ItemType_Code = e.NewValues("ItemTypeCode") & "",
            .LineCode = e.NewValues("LineCode") & "",
            .ItemCheck_Code = e.NewValues("ItemCheckCode") & "",
            .ProdDate = Convert.ToDateTime(e.NewValues("ProdDate")).ToString("yyyy-MM-dd"),
            .ShiftCode = e.NewValues("ShiftCode") & "",
            .Action = e.NewValues("Action") & "",
            .PIC = e.NewValues("PIC") & "",
            .Result = e.NewValues("Result") & "",
            .Remark = e.NewValues("Remark") & "",
            .User = pUser}
        Try
            Dim Insert = clsProdSampleVerificationDB.Activity_Insert("CREATE", data)
            If Insert = True Then
                show_error(MsgTypeEnum.Success, "Save data successfully!", 1)
                GridMenu.CancelEdit()
                'Up_GridLoadActivities(Factory, Itemtype, Line, ItemCheck, ProdDate, Shift, Seq)
                Return
            End If
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub
    Protected Sub GridMenu_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles GridMenu.RowUpdating
        e.Cancel = True
        'Dim Factory As String = HideValue.Get("FactoryCode")
        'Dim Itemtype As String = HideValue.Get("ItemType_Code")
        'Dim Line As String = HideValue.Get("LineCode")
        'Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
        'Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
        'Dim Shift As String = HideValue.Get("ShiftCode")
        'Dim Seq As String = HideValue.Get("Seq")

        Dim data As New clsProdSampleVerification With {
            .PIC = e.NewValues("PIC") & "",
            .ActivityID = e.NewValues("ActivityID") & "",
            .Action = e.NewValues("Action") & "",
            .Result = e.NewValues("Result") & "",
            .Remark = e.NewValues("Remark") & "",
            .User = pUser}
        Try
            Dim Update = clsProdSampleVerificationDB.Activity_Insert("UPDATE", data)
            If Update = True Then
                show_error(MsgTypeEnum.Success, "Update data successfully!", 1)
                GridMenu.CancelEdit()
                'Up_GridLoadActivities(Factory, Itemtype, Line, ItemCheck, ProdDate, Shift, Seq)
                Return
            End If
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Protected Sub GridMenu_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles GridMenu.RowDeleting
        e.Cancel = True
        Dim Factory As String = HideValue.Get("FactoryCode")
        Dim Itemtype As String = HideValue.Get("ItemType_Code")
        Dim Line As String = HideValue.Get("LineCode")
        Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
        Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
        Dim Shift As String = HideValue.Get("ShiftCode")
        Dim Seq As String = HideValue.Get("Seq")

        Dim data As New clsProdSampleVerification With {
            .ActivityID = e.Values("ActivityID")}
        Try
            Dim Delete = clsProdSampleVerificationDB.Activity_Insert("DELETE", data)
            show_error(MsgTypeEnum.Success, "Delete data successfully!", 1)
            GridMenu.CancelEdit()
            Up_GridLoadActivities(Factory, Itemtype, Line, ItemCheck, ProdDate, Shift, Seq)
        Catch ex As Exception
            show_error(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Sub cboLineID_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboLineID.Callback
        Try
            Dim data As New clsProdSampleVerification()
            data.User = pUser
            data.FactoryCode = e.Parameter

            dt = clsProdSampleVerificationDB.FillCombo(Line_Sel, data)
            With cboLineID
                .DataSource = dt
                .DataBind()
            End With
        Catch ex As Exception
            show_error(MsgTypeEnum.Info, ex.Message, 0)
        End Try
    End Sub

    Private Sub cboItemCheck_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboItemCheck.Callback
        Try
            Dim data As New clsProdSampleVerification()
            data.FactoryCode = Split(e.Parameter, "|")(0)
            data.ItemType_Code = Split(e.Parameter, "|")(1)
            data.LineCode = Split(e.Parameter, "|")(2)
            data.User = pUser

            dt = clsProdSampleVerificationDB.FillCombo(ItemCheck_Sel, data)
            With cboItemCheck
                .DataSource = dt
                .DataBind()
            End With
        Catch ex As Exception
            show_error(MsgTypeEnum.Info, ex.Message, 0)
        End Try
    End Sub

    Private Sub cboShift_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboShift.Callback
        Try
            Dim data As New clsProdSampleVerification()
            data.FactoryCode = Split(e.Parameter, "|")(0)
            data.ItemType_Code = Split(e.Parameter, "|")(1)
            data.LineCode = Split(e.Parameter, "|")(2)
            data.ItemCheck_Code = Split(e.Parameter, "|")(3)

            dt = clsProdSampleVerificationDB.FillCombo(Shift_Sel, data)
            With cboShift
                .DataSource = dt
                .DataBind()
            End With

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, ex.Message, 0)
        End Try
    End Sub

    Private Sub cboSeq_Callback(sender As Object, e As CallbackEventArgsBase) Handles cboSeq.Callback
        Try
            Dim data As New clsProdSampleVerification()
            data.FactoryCode = Split(e.Parameter, "|")(0)
            data.ItemType_Code = Split(e.Parameter, "|")(1)
            data.LineCode = Split(e.Parameter, "|")(2)
            data.ItemCheck_Code = Split(e.Parameter, "|")(3)
            data.ShiftCode = Split(e.Parameter, "|")(4)

            dt = clsProdSampleVerificationDB.FillCombo(Seq_Sel, data)
            With cboSeq
                .DataSource = dt
                .DataBind()
            End With

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, ex.Message, 0)
        End Try
    End Sub


    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Dim cls As New clsProdSampleVerification
        Dim Factory As String = cboFactory.Value
        Dim FactoryName As String = cboFactory.Text
        Dim Itemtype As String = cboItemType.Value
        Dim Itemtype_Name As String = cboItemType.Text
        Dim Line As String = cboLineID.Value
        Dim LineName As String = cboLineID.Text
        Dim ItemCheck As String = cboItemCheck.Value
        Dim ItemCheck_Name As String = cboItemCheck.Text
        Dim prodDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
        Dim Period As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy MMM dd")
        Dim Shift As String = cboShift.Value
        Dim ShiftName As String = cboShift.Text
        Dim Seq As String = cboSeq.Value

        cls.FactoryCode = Factory
        cls.FactoryName = FactoryName
        cls.ItemType_Code = Itemtype
        cls.ItemType_Name = Itemtype_Name
        cls.LineCode = Line
        cls.LineName = LineName
        cls.ItemCheck_Code = ItemCheck
        cls.ItemCheck_Name = ItemCheck_Name
        cls.ProdDate = prodDate
        cls.Period = Period
        cls.ShiftCode = Shift
        cls.ShiftName = ShiftName
        cls.Seq = Seq

        up_Excel(cls)
    End Sub

#End Region

#Region "Procedure"
    Private Sub show_error(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        GridMenu.JSProperties("cp_message") = ErrMsg
        GridMenu.JSProperties("cp_type") = msgType
        GridMenu.JSProperties("cp_val") = pVal
    End Sub

    Private Sub show_errorGrid(ByVal msgType As MsgTypeEnum, ByVal ErrMsg As String, ByVal pVal As Integer)
        Grid.JSProperties("cp_message") = ErrMsg
        Grid.JSProperties("cp_type") = msgType
        Grid.JSProperties("cp_val") = pVal
    End Sub
    Private Sub UpFillCombo()
        Try
            Dim data As New clsProdSampleVerification()
            data.User = pUser
            Dim a As String

            '============ FILL COMBO FACTORY CODE ================'
            dt = clsProdSampleVerificationDB.FillCombo(Factory_Sel, data)
            With cboFactory
                .DataSource = dt
                .DataBind()
            End With
            If prmFactoryCode <> "" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("CODE") = prmFactoryCode Then
                        cboFactory.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboFactory.Enabled = False
            End If
            If cboFactory.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboFactory.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("FactoryCode", a)
            data.FactoryCode = HideValue.Get("FactoryCode")
            '======================================================'

            '============== FILL COMBO ITEM TYPE =================='
            dt = clsProdSampleVerificationDB.FillCombo(ItemType_Sel, data)
            With cboItemType
                .DataSource = dt
                .DataBind()
            End With
            If prmItemType <> "" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("CODE") = prmItemType Then
                        cboItemType.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboItemType.Enabled = False
            End If
            If cboItemType.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboItemType.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("ItemType_Code", a)
            data.ItemType_Code = HideValue.Get("ItemType_Code")
            '======================================================'

            '============== FILL COMBO LINE CODE =================='
            dt = clsProdSampleVerificationDB.FillCombo(Line_Sel, data)
            With cboLineID
                .DataSource = dt
                .DataBind()
            End With
            If prmLineCode <> "" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("CODE") = prmLineCode Then
                        cboLineID.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboLineID.Enabled = False
            End If
            If cboLineID.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboLineID.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("LineCode", a)
            data.LineCode = HideValue.Get("LineCode")
            '======================================================'


            '============== FILL COMBO ITEM CHECK =================='
            dt = clsProdSampleVerificationDB.FillCombo(ItemCheck_Sel, data)
            With cboItemCheck
                .DataSource = dt
                .DataBind()
            End With
            If prmItemCheck <> "" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("CODE") = prmItemCheck Then
                        cboItemCheck.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboItemCheck.Enabled = False
            End If
            If cboItemCheck.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboItemCheck.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("ItemCheck_Code", a)
            data.ItemCheck_Code = HideValue.Get("ItemCheck_Code")
            '======================================================'

            '============== FILL COMBO SHIFY =================='
            dt = clsProdSampleVerificationDB.FillCombo(Shift_Sel, data)
            With cboShift
                .DataSource = dt
                .DataBind()
            End With
            If prmShifCode <> "" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("CODE") = prmShifCode Then
                        cboShift.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboShift.Enabled = False
            End If
            If cboShift.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboShift.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("ShiftCode", a)
            data.ShiftCode = HideValue.Get("ShiftCode")
            '======================================================'

            '============== FILL COMBO SEQ =================='
            dt = clsProdSampleVerificationDB.FillCombo(Seq_Sel, data)
            With cboSeq
                .DataSource = dt
                .DataBind()
            End With
            If prmSeqNo <> "" Then
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i)("CODE") = prmSeqNo Then
                        cboSeq.SelectedIndex = i
                        Exit For
                    End If
                Next
                cboSeq.Enabled = False
            End If
            If cboSeq.SelectedIndex < 0 Then
                a = ""
            Else
                a = cboSeq.SelectedItem.GetFieldValue("CODE")
            End If
            HideValue.Set("Seq", a)
            data.Seq = HideValue.Get("Seq")
            '======================================================'

        Catch ex As Exception
            show_error(MsgTypeEnum.Info, "", 0)
        End Try
    End Sub

    Private Sub Up_GridLoad(Factory As String, ItemType As String, Line As String, ItemCheck As String, ProdDate As String, Shift As String, Seq As String)
        Dim msgErr As String = ""

        Dim cls As New clsProdSampleVerification
        cls.FactoryCode = Factory
        cls.ItemType_Code = ItemType
        cls.LineCode = Line
        cls.ItemCheck_Code = ItemCheck
        cls.ProdDate = ProdDate
        cls.ShiftCode = Shift
        cls.Seq = Seq
        cls.User = pUser

        With Grid
            .Columns.Clear()
            Dim ColDescIndex As New GridViewDataTextColumn
            ColDescIndex.FieldName = "nDescIndex"
            ColDescIndex.Width = 0
            ColDescIndex.CellStyle.HorizontalAlign = HorizontalAlign.Center
            .Columns.Add(ColDescIndex)

            Dim Band1 As New GridViewBandColumn
            Band1.Caption = "Date"
            .Columns.Add(Band1)

            Dim Band2 As New GridViewBandColumn
            Band2.Caption = "Shift"
            Band1.Columns.Add(Band2)

            Dim ColDesc As New GridViewDataTextColumn
            ColDesc.FieldName = "nDesc"
            ColDesc.Caption = "Time"
            ColDesc.Width = 80
            ColDesc.CellStyle.HorizontalAlign = HorizontalAlign.Center
            Band2.Columns.Add(ColDesc)

            ds = clsProdSampleVerificationDB.GridLoad(GetHeader_ProdDate, cls)
            Dim dtDate As DataTable = ds.Tables(0)
            If dtDate.Rows.Count > 0 Then
                For i = 0 To dtDate.Rows.Count - 1
                    Dim Col_ProdDate As New GridViewBandColumn
                    Dim nProdDate = dtDate.Rows(i)("ProdDate")
                    Col_ProdDate.Caption = nProdDate
                    .Columns.Add(Col_ProdDate)

                    cls.ProdDate_Grid = Convert.ToDateTime(nProdDate).ToString("yyyy-MM-dd")
                    ds = clsProdSampleVerificationDB.GridLoad(GetHeader_ShifCode, cls)
                    Dim dtShift As DataTable = ds.Tables(0)
                    If dtShift.Rows.Count > 0 Then
                        For n = 0 To dtShift.Rows.Count - 1

                            Dim Col_Shift As New GridViewBandColumn
                            Dim nShiftCode = dtShift.Rows(n)("ShiftCode")
                            If nShiftCode = "SH001" Then
                                nShiftCode = "Shift 1"
                            ElseIf nShiftCode = "SH002" Then
                                nShiftCode = "Shift 2"
                            End If

                            Col_Shift.Caption = nShiftCode
                            Col_ProdDate.Columns.Add(Col_Shift)

                            cls.Shiftcode_Grid = dtShift.Rows(n)("ShiftCode")
                            ds = clsProdSampleVerificationDB.GridLoad(GetHeader_Time, cls)
                            Dim dtSeq As DataTable = ds.Tables(0)
                            If dtSeq.Rows.Count > 0 Then
                                For r = 0 To dtSeq.Rows.Count - 1
                                    Dim Col_Seq As New GridViewDataTextColumn
                                    Col_Seq.Width = 70
                                    Col_Seq.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                                    Col_Seq.CellStyle.HorizontalAlign = HorizontalAlign.Center
                                    Col_Seq.FieldName = dtSeq.Rows(r)("nTime")
                                    Col_Seq.Caption = dtSeq.Rows(r)("nTimeDesc")
                                    Col_Shift.Columns.Add(Col_Seq)
                                Next
                            End If
                        Next
                    End If
                Next

                ds = clsProdSampleVerificationDB.GridLoad(GetGridData, cls)
                Dim dtGrid As DataTable = ds.Tables(0)
                If dtGrid.Rows.Count > 0 Then
                    .KeyFieldName = "nDesc"
                    .DataSource = dtGrid
                    .DataBind()
                    .Styles.CommandColumn.BackColor = Color.White
                    .Styles.CommandColumn.ForeColor = Color.Black
                End If

                ds = clsProdSampleVerificationDB.GridLoad(GetCharSetup, cls)
                Dim dtColBrowse As DataTable = ds.Tables(0)
                If dtColBrowse.Rows.Count > 0 Then
                    UCL = dtColBrowse.Rows(0)("UCL")
                    LCL = dtColBrowse.Rows(0)("LCL")
                    USL = dtColBrowse.Rows(0)("USL")
                    LSL = dtColBrowse.Rows(0)("LSL")
                End If

                ColumnBrowse = Convert.ToDateTime(ProdDate).ToString("yyyyMMdd") & "_" & Shift & "_" & Seq
                ds = clsProdSampleVerificationDB.GridLoad(GetVerifyPrivilege, cls)
                Dim dtVerifyPrivilege As DataTable = ds.Tables(0)
                If dtVerifyPrivilege.Rows.Count > 0 Then
                    VerifyStatus = dtVerifyPrivilege.Rows(0)("VerifyPrivilege")
                End If
            Else
                show_errorGrid(MsgTypeEnum.Warning, "Data Not Found", 1)
            End If
        End With
    End Sub

    Private Sub Up_GridLoadActivities(Factory As String, ItemType As String, Line As String, ItemCheck As String, ProdDate As String, Shift As String, Seq As String)

        Dim cls As New clsProdSampleVerification
        cls.FactoryCode = Factory
        cls.ItemType_Code = ItemType
        cls.LineCode = Line
        cls.ItemCheck_Code = ItemCheck
        cls.ProdDate = ProdDate
        cls.ShiftCode = Shift
        cls.Seq = Seq

        With GridMenu
            ds = clsProdSampleVerificationDB.GridLoad(GetGridData_Activity, cls)
            Dim dtGridMenu As DataTable = ds.Tables(0)
            .DataSource = dtGridMenu
            .DataBind()
        End With
    End Sub

    Private Sub Verify(SpcResultId As String)
        Dim cls As New clsProdSampleVerification
        cls.SPCResultID = SpcResultId
        cls.User = pUser

        Dim Factory As String = HideValue.Get("FactoryCode")
        Dim Itemtype As String = HideValue.Get("ItemType_Code")
        Dim Line As String = HideValue.Get("LineCode")
        Dim ItemCheck As String = HideValue.Get("ItemCheck_Code")
        Dim ProdDate As String = Convert.ToDateTime(dtProdDate.Value).ToString("yyyy-MM-dd")
        Dim Shift As String = HideValue.Get("ShiftCode")
        Dim Seq As String = HideValue.Get("Seq")

        Try
            Dim Verify = clsProdSampleVerificationDB.Verify(cls)
            If Verify = "" Then
                show_errorGrid(MsgTypeEnum.Success, "Verify data successfully!", 1)
                Up_GridLoad(Factory, Itemtype, Line, ItemCheck, ProdDate, Shift, Seq)
                Return
            Else
                show_errorGrid(MsgTypeEnum.Warning, Verify, 1)
                Return
            End If
        Catch ex As Exception
            show_errorGrid(MsgTypeEnum.ErrorMsg, ex.Message, 1)
        End Try
    End Sub

    Private Sub up_FillcomboGrid(ByVal cmb As ASPxComboBox, Action As String, cls As clsProdSampleVerification)
        dt = clsProdSampleVerificationDB.FillCombo(Action, cls)
        With cmb
            .Items.Clear() : .Columns.Clear()
            .DataSource = dt
            .DataBind()
        End With
    End Sub


#Region "Download To Excel"
    Private Sub up_Excel(cls As clsProdSampleVerification)
        Try
            Using excel As New ExcelPackage
                Dim ws As ExcelWorksheet
                ws = excel.Workbook.Worksheets.Add("BO4 - Prod Sample Verifiaction")

                With ws
                    GridTitle(ws, cls)
                    HeaderResult(ws, cls)
                    CellResult(ws, cls)
                    HeaderActivity(ws, cls)
                    CellActivity(ws, cls)
                End With

                Response.Clear()
                Response.Buffer = True
                Response.Charset = ""
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=Production Sample Verification_" & Format(Date.Now, "yyyy-MM-dd_HHmmss") & ".xlsx")
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

    Private Sub GridTitle(ByVal pExl As ExcelWorksheet, cls As clsProdSampleVerification)
        With pExl
            Try
                .Cells(1, 1).Value = "Product Sample Verification"
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
                .Cells(4, 3).Value = ": " & cls.ItemType_Name

                .Cells(5, 1, 5, 2).Value = "Line Code"
                .Cells(5, 1, 5, 2).Merge = True
                .Cells(5, 3).Value = ": " & cls.LineName

                .Cells(6, 1, 6, 2).Value = "Item Check Code"
                .Cells(6, 1, 6, 2).Merge = True
                .Cells(6, 3).Value = ": " & cls.ItemCheck_Name

                .Cells(7, 1, 7, 2).Value = "Prod Date"
                .Cells(7, 1, 7, 2).Merge = True
                .Cells(7, 3).Value = ": " & cls.Period

                .Cells(8, 1, 8, 2).Value = "Shift Code"
                .Cells(8, 1, 8, 2).Merge = True
                .Cells(8, 3).Value = ": " & cls.ShiftName

                .Cells(9, 1, 9, 2).Value = "Sequence No"
                .Cells(9, 1, 9, 2).Merge = True
                .Cells(9, 3).Value = ": " & cls.Seq

                Dim rgHeader As ExcelRange = .Cells(3, 3, 9, 4)
                rgHeader.Style.HorizontalAlignment = HorzAlignment.Near
                rgHeader.Style.VerticalAlignment = VertAlignment.Center
                rgHeader.Style.Font.Size = 10
                rgHeader.Style.Font.Name = "Segoe UI"

                row_GridTitle = 9
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End With
    End Sub

    Private Sub HeaderResult(ByVal pExl As ExcelWorksheet, cls As clsProdSampleVerification)
        With pExl
            Try
                Dim irow = row_GridTitle + 4
                Dim nColDate = 2
                Dim nColSeq = 2
                Dim nColShift = 2

                .Cells(irow, 1).Value = "Date"
                .Cells(irow + 1, 1).Value = "Shift"
                .Cells(irow + 2, 1).Value = "Time"
                .Column(1).Width = 15

                row_HeaderResult = irow + 2

                ds = clsProdSampleVerificationDB.GridLoad(GetHeader_ProdDate, cls)
                Dim dtDate As DataTable = ds.Tables(0)
                If dtDate.Rows.Count > 0 Then
                    For i = 0 To dtDate.Rows.Count - 1
                        Dim nProdDate = dtDate.Rows(i)("ProdDate")
                        cls.ProdDate_Grid = Convert.ToDateTime(nProdDate).ToString("yyyy-MM-dd")

                        'GET COLUMN SHIFT
                        ds = clsProdSampleVerificationDB.GridLoad(GetHeader_ShifCode, cls)
                        Dim dtShift As DataTable = ds.Tables(0)
                        If dtShift.Rows.Count > 0 Then
                            For n = 0 To dtShift.Rows.Count - 1
                                cls.Shiftcode_Grid = dtShift.Rows(n)("ShiftCode")

                                'GET COLUMN SHIFT
                                ds = clsProdSampleVerificationDB.GridLoad(GetHeader_Time, cls)
                                Dim dtSeq As DataTable = ds.Tables(0)
                                Dim nSeq = dtSeq.Rows.Count
                                If dtSeq.Rows.Count > 0 Then
                                    For r = 0 To dtSeq.Rows.Count - 1
                                        .Cells(irow + 2, nColSeq).Value = dtSeq.Rows(r)("nTimeDesc")
                                        .Cells(irow + 2, nColSeq).Style.HorizontalAlignment = HorzAlignment.Center
                                        .Column(nColSeq).Width = 15
                                        nColSeq = nColSeq + 1
                                        col_HeaderResult = col_HeaderResult + 1
                                    Next
                                End If
                                '-----------------------------

                                Dim nShiftCode = dtShift.Rows(n)("ShiftCode")
                                If nShiftCode = "SH001" Then
                                    nShiftCode = "Shift 1"
                                ElseIf nShiftCode = "SH002" Then
                                    nShiftCode = "Shift 2"
                                End If

                                .Cells(irow + 1, nColShift, irow + 1, nColShift + nSeq - 1).Value = nShiftCode
                                .Cells(irow + 1, nColShift, irow + 1, nColShift + nSeq - 1).Merge = True
                                .Cells(irow + 1, nColShift, irow + 1, nColShift + nSeq - 1).Style.HorizontalAlignment = HorzAlignment.Center

                                nColShift = nColSeq
                            Next
                            '-----------------------------

                            .Cells(irow, nColDate, irow, nColShift - 1).Value = dtDate.Rows(i)("ProdDate")
                            .Cells(irow, nColDate, irow, nColShift - 1).Merge = True
                            .Cells(irow, nColDate, irow, nColShift - 1).Style.HorizontalAlignment = HorzAlignment.Center

                        End If
                    Next
                End If

                .Cells(irow - 1, 1, irow - 1, col_HeaderResult + 1).Value = "STATISTIC PRODUCT MONITORING"
                .Cells(irow - 1, 1, irow - 1, col_HeaderResult + 1).Merge = True
                .Cells(irow - 1, 1, irow - 1, col_HeaderResult + 1).Style.HorizontalAlignment = MenuItemAlignment.Center
                .Cells(irow - 1, 1, irow - 1, col_HeaderResult + 1).Style.Font.Bold = True

                Dim rgCell As ExcelRange = .Cells(irow, 1, row_HeaderResult, col_HeaderResult + 1)
                rgCell.Style.Font.Size = 10
                rgCell.Style.Font.Name = "Segoe UI"
                rgCell.Style.HorizontalAlignment = HorzAlignment.Center
                rgCell.Style.Font.Color.SetColor(Color.White)
                rgCell.Style.Fill.PatternType = ExcelFillStyle.Solid
                rgCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End With
    End Sub
    Private Sub CellResult(ByVal pExl As ExcelWorksheet, cls As clsProdSampleVerification)
        With pExl
            Try
                Dim irow = row_HeaderResult + 1
                ds = clsProdSampleVerificationDB.GridLoad(GetCharSetup, cls)
                Dim dtColBrowse As DataTable = ds.Tables(0)
                If dtColBrowse.Rows.Count > 0 Then
                    UCL = dtColBrowse.Rows(0)("UCL")
                    LCL = dtColBrowse.Rows(0)("LCL")
                    USL = dtColBrowse.Rows(0)("USL")
                    LSL = dtColBrowse.Rows(0)("LSL")
                End If

                ds = clsProdSampleVerificationDB.GridLoad(GetGridData, cls)
                Dim dtGrid As DataTable = ds.Tables(0)
                If dtGrid.Rows.Count > 0 Then
                    For i = 0 To dtGrid.Rows.Count - 2
                        For n = 1 To dtGrid.Columns.Count - 1
                            Try

                                Dim data = dtGrid.Rows(i)(n)
                                .Cells(irow + i, n).Value = dtGrid.Rows(i)(n)

                                If n > 2 Then
                                    Dim Color = "#FFFFFF"
                                    Dim RowIndex = Trim(dtGrid.Rows(i)(0))

                                    If RowIndex = "EachData" Then
                                        If data < LSL Or data > USL Then
                                            Color = "#ff0000"
                                        ElseIf data < LSL Or data > UCL Then
                                            Color = "#ffc0cb"
                                        End If
                                    ElseIf RowIndex = "XBar" Then
                                        If data < LSL Or data > USL Then
                                            Color = "#ff0000"
                                        ElseIf data < LSL Or data > UCL Then
                                            Color = "#fffb00"
                                        End If
                                    ElseIf RowIndex = "Judgement" Then
                                        If data = "NG" Then
                                            Color = "#ff0000"
                                        End If
                                    ElseIf RowIndex = "Correction" Then
                                        If data = "C" Then
                                            Color = "#FFA500"
                                        End If
                                    End If

                                    .Cells(irow + i, n).Style.Fill.PatternType = ExcelFillStyle.Solid
                                    .Cells(irow + i, n).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(Color))
                                End If

                            Catch ex As Exception
                                Throw New Exception(ex.Message)
                            End Try
                        Next
                    Next
                End If

                col_CellResult = dtGrid.Columns.Count
                row_CellResult = irow + dtGrid.Rows.Count

                Dim Border As ExcelRange = .Cells(row_GridTitle + 4, 1, row_CellResult - 2, col_CellResult - 1)
                Border.Style.Border.Top.Style = ExcelBorderStyle.Thin
                Border.Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                Border.Style.Border.Right.Style = ExcelBorderStyle.Thin
                Border.Style.Border.Left.Style = ExcelBorderStyle.Thin
                Border.Style.Font.Size = 10
                Border.Style.Font.Name = "Segoe UI"
                Border.Style.HorizontalAlignment = HorzAlignment.Center
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End With
    End Sub

    Private Sub HeaderActivity(ByVal pExl As ExcelWorksheet, cls As clsProdSampleVerification)
        Try
            Dim irow = row_CellResult + 2
            With pExl
                .Cells(irow, 1, irow, 7).Value = "ACTIVITY MONITORING"
                .Cells(irow, 1, irow, 7).Merge = True
                .Cells(irow, 1, irow, 7).Style.HorizontalAlignment = MenuItemAlignment.Center
                .Cells(irow, 1, irow, 7).Style.Font.Bold = True
                irow = irow + 1

                .Cells(irow, 1).Value = "Date"
                .Cells(irow, 2).Value = "PIC"

                .Cells(irow, 3, irow, 4).Value = "Action"
                .Cells(irow, 3, irow, 4).Style.HorizontalAlignment = HorzAlignment.Center
                .Cells(irow, 3, irow, 4).Merge = True
                .Cells(irow, 3, irow, 4).Style.WrapText = True

                .Cells(irow, 5).Value = "Result"

                .Cells(irow, 6).Value = "Remark"
                .Cells(irow, 6, irow, 7).Style.HorizontalAlignment = HorzAlignment.Center
                .Cells(irow, 6, irow, 7).Merge = True
                .Cells(irow, 6, irow, 7).Style.WrapText = True


                .Cells(irow, 8).Value = "Last User"
                .Cells(irow, 9).Value = "Last Update"

                .Column(1).Width = 15
                .Column(2).Width = 15
                .Column(3).Width = 15
                .Column(4).Width = 15

                .Column(5).Width = 15
                .Column(6).Width = 15
                .Column(7).Width = 15
                .Column(8).Width = 15
                .Column(9).Width = 15

                col_HeaderActivity = 9
                row_HeaderActivity = irow

                Dim rgCell As ExcelRange = .Cells(irow, 1, irow, col_HeaderActivity)
                rgCell.Style.Font.Size = 10
                rgCell.Style.Font.Name = "Segoe UI"
                rgCell.Style.HorizontalAlignment = HorzAlignment.Center
                rgCell.Style.Font.Color.SetColor(Color.White)
                rgCell.Style.Fill.PatternType = ExcelFillStyle.Solid
                rgCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DimGray)
            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CellActivity(ByVal pExl As ExcelWorksheet, cls As clsProdSampleVerification)
        Try
            Dim irow = row_HeaderActivity + 1
            With pExl
                ds = clsProdSampleVerificationDB.GridLoad(GetGridData_Activity, cls)
                Dim dtGridMenu As DataTable = ds.Tables(0)
                Dim nRow = dtGridMenu.Rows.Count
                If dtGridMenu.Rows.Count > 0 Then
                    For i = 0 To dtGridMenu.Rows.Count - 1
                        .Cells(irow + i, 1).Value = dtGridMenu.Rows(0)("ProdDate")
                        .Cells(irow + i, 1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

                        .Cells(irow + i, 2).Value = dtGridMenu.Rows(0)("PIC")
                        .Cells(irow + i, 2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left

                        .Cells(irow + i, 3, irow + i, 4).Value = dtGridMenu.Rows(0)("Action")
                        .Cells(irow + i, 3, irow + i, 4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left
                        .Cells(irow + i, 3, irow + i, 4).Merge = True
                        .Cells(irow + i, 3, irow + i, 4).Style.WrapText = True

                        .Cells(irow + i, 5).Value = If(dtGridMenu.Rows(0)("Result") = 0, "OK", "NG")
                        .Cells(irow + i, 5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

                        .Cells(irow + i, 6, irow + i, 7).Value = dtGridMenu.Rows(0)("Remark")
                        .Cells(irow + i, 6, irow + i, 7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left
                        .Cells(irow + i, 6, irow + i, 7).Merge = True
                        .Cells(irow + i, 6, irow + i, 7).Style.WrapText = True

                        .Cells(irow + i, 8).Value = dtGridMenu.Rows(0)("LastUser")
                        .Cells(irow + i, 8).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left

                        .Cells(irow + i, 9).Value = dtGridMenu.Rows(0)("LastDate")
                        .Cells(irow + i, 9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                    Next
                End If

                col_CellActivity = 9
                row_CellActivity = irow + nRow

                Dim Border As ExcelRange = .Cells(irow, 1, row_CellActivity - 1, col_CellActivity)
                Border.Style.Border.Top.Style = ExcelBorderStyle.Thin
                Border.Style.Border.Bottom.Style = ExcelBorderStyle.Thin
                Border.Style.Border.Right.Style = ExcelBorderStyle.Thin
                Border.Style.Border.Left.Style = ExcelBorderStyle.Thin
                Border.Style.Font.Size = 10
                Border.Style.Font.Name = "Segoe UI"

            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
#End Region

    'Private Sub UpFillCombo()
    '    Try
    '        Dim data As New clsProdSampleVerification()
    '        Dim ErrMsg As String = ""
    '        Dim a As String
    '        data.User = pUser

    '        '============ FILL COMBO FACTORY CODE ================'
    '        dt = clsProdSampleVerificationDB.FillCombo(Factory_Sel, data, ErrMsg)
    '        With cboFactory
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '        If prmFactoryCode <> "" Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("CODE") = prmFactoryCode Then
    '                    cboFactory.SelectedIndex = i
    '                    Exit For
    '                End If
    '            Next
    '            cboFactory.Enabled = False
    '        Else
    '            cboFactory.SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End If
    '        If cboFactory.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboFactory.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("FactoryCode", a)
    '        data.FactoryCode = HideValue.Get("FactoryCode")
    '        '======================================================'

    '        '============== FILL COMBO ITEM TYPE =================='
    '        dt = clsProdSampleVerificationDB.FillCombo(ItemType_Sel, data, ErrMsg)
    '        With cboItemType
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '        If prmItemType <> "" Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("CODE") = prmItemType Then
    '                    cboItemType.SelectedIndex = i
    '                    Exit For
    '                End If
    '            Next
    '            cboItemType.Enabled = False
    '        Else
    '            cboItemType.SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End If
    '        If cboItemType.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboItemType.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("ItemType_Code", a)
    '        data.ItemType_Code = HideValue.Get("ItemType_Code")
    '        '======================================================'

    '        '============== FILL COMBO LINE CODE =================='
    '        dt = clsProdSampleVerificationDB.FillCombo(Line_Sel, data, ErrMsg)
    '        With cboLineID
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '        If prmLineCode <> "" Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("CODE") = prmLineCode Then
    '                    cboLineID.SelectedIndex = i
    '                    Exit For
    '                End If
    '            Next
    '            cboLineID.Enabled = False
    '        Else
    '            cboLineID.SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End If
    '        If cboLineID.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboLineID.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("LineCode", a)
    '        data.LineCode = HideValue.Get("LineCode")
    '        '======================================================'


    '        '============== FILL COMBO ITEM CHECK =================='
    '        dt = clsProdSampleVerificationDB.FillCombo(ItemCheck_Sel, data, ErrMsg)
    '        With cboItemCheck
    '            .DataSource = dt
    '            .DataBind()
    '        End With
    '        If prmItemCheck <> "" Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("CODE") = prmItemCheck Then
    '                    cboItemCheck.SelectedIndex = i
    '                    Exit For
    '                End If
    '            Next
    '            cboItemCheck.Enabled = False
    '        Else
    '            cboItemCheck.SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End If
    '        If cboItemCheck.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboItemCheck.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("ItemCheck_Code", a)
    '        data.ItemCheck_Code = HideValue.Get("ItemCheck_Code")
    '        '======================================================'

    '        '============== FILL COMBO SHIFY =================='
    '        dt = clsProdSampleVerificationDB.FillCombo(Shift_Sel, data, ErrMsg)
    '        With cboShift
    '            .DataSource = dt
    '            .DataBind()
    '            .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End With
    '        If prmShifCode <> "" Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("CODE") = prmShifCode Then
    '                    cboShift.SelectedIndex = i
    '                    Exit For
    '                End If
    '            Next
    '            cboShift.Enabled = False
    '        Else
    '            cboShift.SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End If
    '        If cboShift.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboShift.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("ShiftCode", a)
    '        data.ShiftCode = HideValue.Get("ShiftCode")
    '        '======================================================'

    '        '============== FILL COMBO SEQ =================='
    '        dt = clsProdSampleVerificationDB.FillCombo(Seq_Sel, data, ErrMsg)
    '        With cboSeq
    '            .DataSource = dt
    '            .DataBind()
    '            .SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End With
    '        If prmSeqNo <> "" Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("CODE") = prmSeqNo Then
    '                    cboSeq.SelectedIndex = i
    '                    Exit For
    '                End If
    '            Next
    '            cboSeq.Enabled = False
    '        Else
    '            cboSeq.SelectedIndex = IIf(dt.Rows.Count > 0, 0, -1)
    '        End If
    '        If cboSeq.SelectedIndex < 0 Then
    '            a = ""
    '        Else
    '            a = cboSeq.SelectedItem.GetFieldValue("CODE")
    '        End If
    '        HideValue.Set("Seq", a)
    '        data.Seq = HideValue.Get("Seq")
    '        '======================================================'

    '    Catch ex As Exception
    '        show_error(MsgTypeEnum.Info, "", 0)
    '    End Try
    'End Sub

#End Region
End Class