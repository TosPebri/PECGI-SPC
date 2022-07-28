<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TCCSResultInput.aspx.vb" Inherits="PECGI_SPC.TCCSResultInput" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register assembly="DevExpress.Web.v20.2, Version=20.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>








<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" >
        var rowIndex, colIndex, rowEdit;
        var colResult = 9;
        var colJudgement = 11;

        function isNumeric(n) {
            if (n == null) {
                return true;
            } else {
                return !isNaN(parseFloat(n)) && isFinite(n);
            }
        }

        function OnInit(s, e) {
            var today = new Date();
            

            lblPIC1.SetText('');
            lblDate1.SetText('');
            lblJudge1.SetText('');

            lblPIC2.SetText('');
            lblDate2.SetText('');
            lblJudge2.SetText('');

            lblPIC3.SetText('');
            lblDate3.SetText('');
            lblJudge3.SetText('');

            lblPIC4.SetText('');
            lblDate4.SetText('');
            lblJudge4.SetText('');

            txtLastUpdate.SetText('');
            txtUserUpdate.SetText('');
            txtRevNo.SetText('');
            txtRevDate.SetText('');
            var readOnlyIndexes = grid.cpReadOnlyColumns;
            ASPxClientUtils.AttachEventToElement(s.GetMainElement(), "keydown", function (event) {
                if (event.keyCode == 9 || event.keyCode == 13 || event.keyCode == 40) {
                    if (ASPxClientUtils.IsExists(columnIndex) && ASPxClientUtils.IsExists(rowIndex)) {
                        ASPxClientUtils.PreventEventAndBubble(event);
                        if (rowIndex < grid.GetVisibleRowsOnPage() + grid.GetTopVisibleIndex() - 1)
                            rowIndex++;
                        else {
                            rowIndex = grid.GetTopVisibleIndex();
                            columnIndex = 0;                                
                        }
                        grid.batchEditApi.StartEdit(rowIndex, columnIndex);
                    }
                }

                if (event.keyCode == 38) {
                    if (ASPxClientUtils.IsExists(columnIndex) && ASPxClientUtils.IsExists(rowIndex)) {
                        ASPxClientUtils.PreventEventAndBubble(event);
                        if (rowIndex > 0)
                            rowIndex--;
                        else {
                            rowIndex = grid.GetTopVisibleIndex();
                            if (columnIndex < grid.GetColumnCount() - 1)
                                columnIndex--;
                            else
                                columnIndex = 0;
                            while (readOnlyIndexes.indexOf(columnIndex) > -1)
                                columnIndex--;
                        }
                        grid.batchEditApi.StartEdit(rowIndex, columnIndex);
                    }
                }
            });
        }

        function ClearData() {
            txtJudge.SetText('');
            cboMachine.SetText('');
            txtLineID.SetText('');
            txtSubLine.SetText('');
            txtNotes.SetText('');
            cboPartID.SetText('');
            txtpartname.SetText('');
            cboOldPartID.SetText('');
            txtOldPartName.SetText('');
            txtLotNo.SetText('');
            txtID.SetText('');

            lblPIC1.SetText('');
            lblDate1.SetText('');
            lblJudge1.SetText('');

            lblPIC2.SetText('');
            lblDate2.SetText('');
            lblJudge2.SetText('');

            lblPIC3.SetText('');
            lblDate3.SetText('');
            lblJudge3.SetText('');

            lblPIC4.SetText('');
            lblDate4.SetText('');
            lblJudge4.SetText('');

            txtLastUpdate.SetText('');
            txtUserUpdate.SetText('');
            txtRevNo.SetText('');
            txtRevDate.SetText('');

            GridLoad();            
        }

        function LoadApproval(s, e) {
            lblPIC1.SetText(s.cpPIC1);
            lblDate1.SetText(s.cpDate1);
            lblJudge1.SetText(s.cpJudge1);

            lblPIC2.SetText(s.cpPIC2);
            lblDate2.SetText(s.cpDate2);
            lblJudge2.SetText(s.cpJudge2);

            lblPIC3.SetText(s.cpPIC3);
            lblDate3.SetText(s.cpDate3);
            lblJudge3.SetText(s.cpJudge3);

            lblPIC4.SetText(s.cpPIC4);
            lblDate4.SetText(s.cpDate4);
            lblJudge4.SetText(s.cpJudge4);
            
            if (s.cpEnableSave == '1') {
                btnSave.SetEnabled(true);
                btnUpload.SetEnabled(true);
            } else {
                btnSave.SetEnabled(false);
                btnUpload.SetEnabled(false);
            };
            if (s.cpEnableApprove == '1') {
                btnApprove.SetEnabled(true);
            } else {
                btnApprove.SetEnabled(false);
            };
            if (s.cpEnableApproveNG == '1') {
                btnApproveNG.SetEnabled(true);
            } else {
                btnApproveNG.SetEnabled(false);
            };
        }

        function OnEndCallback(s, e) {
            if (s.cp_message != "" && s.cp_val == 1) {
                if (s.cp_type == "Success" && s.cp_val == 1) {
                    toastr.success(s.cp_message, 'Success');
                    toastr.options.closeButton = false;
                    toastr.options.debug = false;
                    toastr.options.newestOnTop = false;
                    toastr.options.progressBar = false;
                    toastr.options.preventDuplicates = true;
                    toastr.options.onclick = null;
                    s.cp_val = 0;
                    s.cp_message = "";
                }
                else if (s.cp_type == "Warning" && s.cp_val == 1) {

                    toastr.warning(s.cp_message, 'Warning');
                    toastr.options.closeButton = false;
                    toastr.options.debug = false;
                    toastr.options.newestOnTop = false;
                    toastr.options.progressBar = false;
                    toastr.options.preventDuplicates = true;
                    toastr.options.onclick = null;
                    ss.cp_val = 0;
                    s.cp_message = "";
                }
                else if (s.cp_type == "ErrorMsg" && s.cp_val == 1) {
                    toastr.error(s.cp_message, 'Error');
                    toastr.options.closeButton = false;
                    toastr.options.debug = false;
                    toastr.options.newestOnTop = false;
                    toastr.options.progressBar = false;
                    toastr.options.preventDuplicates = true;
                    toastr.options.onclick = null;
                    s.cp_val = 0;
                    s.cp_message = "";
                }
            }
            else if (s.cp_message == "" && s.cp_val == 0) {
                toastr.options.closeButton = false;
                toastr.options.debug = false;
                toastr.options.newestOnTop = false;
                toastr.options.progressBar = false;
                toastr.options.preventDuplicates = true;
                toastr.options.onclick = null;
            }

            if (s.cpEnableSave == '1') {
                btnSave.SetEnabled(true);
                btnUpload.SetEnabled(true);        
            } else {
                btnSave.SetEnabled(false);
                btnUpload.SetEnabled(false);
                btnDelete.SetEnabled(false);             
            }
            if (s.cpEnableApprove == '1') {
                btnApprove.SetEnabled(true);
                btnApproveNG.SetEnabled(true);
            } else {
                btnApprove.SetEnabled(false);
                btnApproveNG.SetEnabled(false);
            }
            if (s.cpLoadData == '1') {
                txtID.SetText(s.cpTCCSResultID);
                cboOldPartID.SetValue(s.cpOldPartID);
                txtOldPartName.SetText(s.cpOldPartName);
                txtLotNo.SetText(s.cpLotNo);
                txtNotes.SetText(s.cpRemark);                
                txtLastUpdate.SetText(s.cpLastUpdate);
                txtUserUpdate.SetText(s.cpUserUpdate);
                txtRevNo.SetText(s.cpRevNo);
                txtRevDate.SetText(s.cpRevDate);
                if (s.cpEnableDelete == '1') {
                    btnDelete.SetEnabled(true);
                } else {
                    btnDelete.SetEnabled(false);
                }
            }
            LoadApproval(s, e);
        }

        var currentColumnName;
        var rowNull = -1;
        function OnBatchEditStartEditing(s, e) {
            currentColumnName = e.focusedColumn.fieldName;
            if (currentColumnName != "Result" && currentColumnName != "Judgement") {
                e.cancel = true;
            } else if (btnSave.GetEnabled() == false) {
                e.cancel = true;
            }
            columnIndex = e.focusedColumn.index;
            currentEditableVisibleIndex = e.visibleIndex;
            rowIndex = e.visibleIndex;
            rowEdit = e.visibleIndex;
            if (currentColumnName == 'Judgement') {
                var valuetype = grid.batchEditApi.GetCellValue(rowIndex, 'ValueType');
                if (valuetype == 'N') {
                    e.cancel = true;
                }
            }            
        }

        function OnBatchEditEndEditing(s, e) {
            btnApprove.SetEnabled(false);
            btnApproveNG.SetEnabled(false);
            rowIndex = null;
            columnIndex = null;
        }

        function UpdateJudgement() {
            var startIndex = 0;
            var result = 0;
            var minvalue = 0;
            var maxvalue = 0;
            var i;
            result = parseFloat(result);
            minvalue = parseFloat(minvalue);
            maxvalue = parseFloat(maxvalue);

            var judgement = 'OK';
            var valuetype = '';
            for (i = startIndex; i < startIndex + grid.GetVisibleRowsOnPage(); i++) {
                valuetype = grid.batchEditApi.GetCellValue(i, 'ValueType');
                if (grid.batchEditApi.GetCellValue(i, 'Result') != null) {
                    if (valuetype == 'N') {
                        result = parseFloat(grid.batchEditApi.GetCellValue(i, 'Result'));
                        minvalue = parseFloat(grid.batchEditApi.GetCellValue(i, 'NumRangeStart'));
                        maxvalue = parseFloat(grid.batchEditApi.GetCellValue(i, 'NumRangeEnd'));
                        if (grid.batchEditApi.GetCellValue(i, 'Result') == null) {
                            grid.batchEditApi.SetCellValue(i, 'Judgement', null);
                        } else if (result >= minvalue && result <= maxvalue) {
                            grid.batchEditApi.SetCellValue(i, 'Judgement', 'OK');
                        } else {
                            grid.batchEditApi.SetCellValue(i, 'Judgement', 'NG');
                            judgement = 'NG';
                        }
                    } else if (valuetype == 'T') {
                        var textresult = grid.batchEditApi.GetCellValue(i, 'Result').trim().toUpperCase();
                        if (textresult == 'OK' || textresult == 'NG') {
                            grid.batchEditApi.SetCellValue(i, 'Judgement', textresult);
                        }
                    }
                }
            }

            for (i = 0; i < grid.GetVisibleRowsOnPage(); i++) {
                valuetype = grid.batchEditApi.GetCellValue(i, 'ValueType');
                if (valuetype == 'N') {
                    if (grid.batchEditApi.GetCellValue(i, 'Result') == null) {
                        judgement = '-';
                        rowNull = i;
                        break;
                    }
                } else if (valuetype == 'T') {
                    if (grid.batchEditApi.GetCellValue(i, 'Judgement') == null || grid.batchEditApi.GetCellValue(i, 'Judgement').trim() == '') {
                        judgement = '-';
                        rowNull = i;
                        break; 
                    }
                }
            }
            txtJudge.SetText(judgement);
        }

        function GridLoad() {
            txtJudge.SetText('');
            grid.PerformCallback('load|' + dtDate.GetText() + '|' + cboMachine.GetValue() + '|' + cboPartID.GetValue() + '|' + txtLineID.GetText() + '|' + txtSubLine.GetText());
        }

        function AfterDelete() {
            grid.PerformCallback('delete|' + dtDate.GetText() + '|' + cboMachine.GetValue() + '|' + cboPartID.GetValue() + '|' + txtLineID.GetText() + '|' + txtSubLine.GetText());
        }

        function cboPartIDChanged(s, e) {
            txtpartname.SetText(cboPartID.GetSelectedItem().GetColumnText(1));
            GridLoad();
        }
        function SelectOldPartID(s, e) {
            txtOldPartName.SetText(cboOldPartID.GetSelectedItem().GetColumnText(1));
        }

        function cboMachineChanged(s, e) {
            txtLineID.SetText(cboMachine.GetSelectedItem().GetColumnText(1));
            txtSubLine.SetText(cboMachine.GetSelectedItem().GetColumnText(2));
            txtpartname.SetText('');
            cboPartID.PerformCallback(cboMachine.GetValue());
            GridLoad();
        }

        function ValidateSave(s, e) {
            var msg = '';
            if (grid.GetVisibleRowsOnPage() == 0) {
                msg = 'Please select data!';
            } else if (cboMachine.GetValue() == '') {
                cboMachine.Focus();
                msg = 'Please select Machine No!';
            } else {

                var startIndex = 0;
                var result = 0;
                var minvalue = 0;
                var maxvalue = 0;
                var i;
                var rowseq = 0;
                result = parseFloat(result);
                minvalue = parseFloat(minvalue);
                maxvalue = parseFloat(maxvalue);

                var judgement = 'OK';
                var valuetype = '';
                for (i = startIndex; i < startIndex + grid.GetVisibleRowsOnPage(); i++) {
                    valuetype = grid.batchEditApi.GetCellValue(i, 'ValueType');
                    if (valuetype == 'N') {
                        if(isNumeric(grid.batchEditApi.GetCellValue(i, 'Result')) == false) {
                            msg = 'Please input valid numeric!';
                            judgement = '-';
                            grid.batchEditApi.StartEdit(i, colResult);
                            break;
                        }
                        result = parseFloat(grid.batchEditApi.GetCellValue(i, 'Result'));
                        minvalue = parseFloat(grid.batchEditApi.GetCellValue(i, 'NumRangeStart'));
                        maxvalue = parseFloat(grid.batchEditApi.GetCellValue(i, 'NumRangeEnd'));
                        if (grid.batchEditApi.GetCellValue(i, 'Result') == null) {
                            grid.batchEditApi.SetCellValue(i, 'Judgement', null);
                        } else if (result >= minvalue && result <= maxvalue) {
                            grid.batchEditApi.SetCellValue(i, 'Judgement', 'OK');
                        } else {
                            grid.batchEditApi.SetCellValue(i, 'Judgement', 'NG');
                            judgement = 'NG';
                        }
                    } else if (valuetype == 'T') {
                        if (grid.batchEditApi.GetCellValue(i, 'Result') != null) {
                            var textresult = grid.batchEditApi.GetCellValue(i, 'Result').trim().toUpperCase();
                            if (textresult == 'OK' || textresult == 'NG') {
                                grid.batchEditApi.SetCellValue(i, 'Judgement', textresult);
                            }
                        }
                    }
                }

                /*
                for (i = 0; i < grid.GetVisibleRowsOnPage(); i++) {
                    valuetype = grid.batchEditApi.GetCellValue(i, 'ValueType');
                    rowseq = grid.batchEditApi.GetCellValue(i, 'SeqNo');
                    if (valuetype == 'N') {
                        if (grid.batchEditApi.GetCellValue(i, 'Result') == null) {
                            judgement = '-';
                            msg = 'Please input result at row ' + rowseq;
                            grid.batchEditApi.StartEdit(rowseq, colResult);
                            break;
                        }
                    } else if (valuetype == 'T') {
                        if (grid.batchEditApi.GetCellValue(i, 'Judgement') == null || grid.batchEditApi.GetCellValue(i, 'Judgement').trim() == '') {
                            judgement = '-';
                            msg = 'Please input judgement at row ' + rowseq;
                            grid.batchEditApi.StartEdit(rowseq, colJudgement);
                            break;
                        }
                    }
                }
                */

                if (judgement == 'NG' && txtNotes.GetText().trim() == '') {
                    txtNotes.Focus();
                    msg = 'Please input Catatan Kelainan!';
                }
            }

            if (msg != '') {
                toastr.warning(msg, 'Warning');
                toastr.options.closeButton = false;
                toastr.options.debug = false;
                toastr.options.newestOnTop = false;
                toastr.options.progressBar = false;
                toastr.options.preventDuplicates = true;
                toastr.options.onclick = null;
                e.processOnServer = false;
                return;
            }
            cbkValid.PerformCallback(dtDate.GetText() + '|' + cboMachine.GetValue() + '|' + cboPartID.GetValue() + '|' + cboOldPartID.GetValue() + '|' + txtLotNo.GetText() + '|' + txtNotes.GetText());
        }

        function ValidateDelete(s, e) {
            var msg = '';
            if (grid.GetVisibleRowsOnPage() == 0) {
                msg = 'Please select data!';
            } else if (cboMachine.GetValue() == '') {
                cboMachine.Focus();
                msg = 'Please select Machine No!';
            } else if (cboPartID.GetValue() == '') {
                cboPartID.Focus();
                msg = 'Please input New Part No!';
            }

            if (msg != '') {
                toastr.warning(msg, 'Warning');
                toastr.options.closeButton = false;
                toastr.options.debug = false;
                toastr.options.newestOnTop = false;
                toastr.options.progressBar = false;
                toastr.options.preventDuplicates = true;
                toastr.options.onclick = null;
                e.processOnServer = false;
                return;
            }

            if (confirm('Are you sure you want to delete this data?')) {
                cbkDelete.PerformCallback(dtDate.GetText() + '|' + cboMachine.GetValue() + '|' + cboPartID.GetValue() + '|' + txtLineID.GetText() + '|' + txtSubLine.GetText());
            }
        }

        function ApproveData(s, e) {
            cbkApprove.PerformCallback('OK|' + dtDate.GetText() + '|' + cboMachine.GetValue() + '|' + cboPartID.GetValue() + '|' + txtLineID.GetText() + '|' + txtSubLine.GetText());            
        }

        function ApproveNG(s, e) {
            cbkApprove.PerformCallback('NG|' + dtDate.GetText() + '|' + cboMachine.GetValue() + '|' + cboPartID.GetValue() + '|' + txtLineID.GetText() + '|' + txtSubLine.GetText());
        }
    </script>
    <style type="text/css">
        .DisabledStyle
        {
            background-color:red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <table style="width:100%">
        <tr>
            <td>
                <div style="padding: 0px 5px 5px 5px">
                    <table class="auto-style3" style="width: 100%">
                        <tr >
                            <td style="width:60px; padding:5px 0px 0px 0px">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Date" Font-Names="Segoe UI" 
                                    Font-Size="9pt">
                                </dx:ASPxLabel>
                            </td>
                            <td style=" width:110px; padding:5px 0px 0px 0px">
                                <dx:ASPxDateEdit ID="dtDate" runat="server" Theme="Office2010Black" 
                                    Width="100px"
                                        ClientInstanceName="dtDate" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="1" 
                                    EditFormat="Custom">
                                        <CalendarProperties>
                                            <HeaderStyle Font-Size="12pt" Paddings-Padding="5px" >
                <Paddings Padding="5px"></Paddings>
                                            </HeaderStyle>
                                            <DayStyle Font-Size="9pt" Paddings-Padding="5px" >
                <Paddings Padding="5px"></Paddings>
                                            </DayStyle>
                                            <WeekNumberStyle Font-Size="9pt" Paddings-Padding="5px">
                <Paddings Padding="5px"></Paddings>
                                            </WeekNumberStyle>
                                            <FooterStyle Font-Size="9pt" Paddings-Padding="10px" >
                <Paddings Padding="10px"></Paddings>
                                            </FooterStyle>
                                            <ButtonStyle Font-Size="9pt" Paddings-Padding="10px">
                <Paddings Padding="10px"></Paddings>
                                            </ButtonStyle>
                                        </CalendarProperties>
                                        <ClientSideEvents Init="OnInit" ValueChanged="GridLoad" />
                                        <ButtonStyle Width="5px" Paddings-Padding="4px" >
                <Paddings Padding="4px"></Paddings>
                                        </ButtonStyle>
                                    </dx:ASPxDateEdit>
                            </td>
                            <td style=" width:90px; padding:5px 0px 0px 0px">
                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="New Part No." 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width:150px; padding:5px 0px 0px 0px">
                
                                <dx:ASPxComboBox ID="cboPartID" runat="server" Theme="Office2010Black" TextField="PartName"
                                    ClientInstanceName="cboPartID" ValueField="PartID" Font-Names="Segoe UI" 
                                    Font-Size="9pt" Height="25px" 
                                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                                    Width="150px" TabIndex="4" CallbackPageSize="40" 
                                    EnableTheming="True">

                                    <ClientSideEvents SelectedIndexChanged="cboPartIDChanged"/>
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Part ID" FieldName="PartID" Width="120px" />
                                        <dx:ListBoxColumn Caption="Part Name" FieldName="PartName" Width="250px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                                    </ButtonStyle>
                                </dx:ASPxComboBox>
                
                            </td>
                            <td style="width: 12px">
                                &nbsp;</td>
                            <td style=" width:10px">
                                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Lot No." 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px">
                                </dx:ASPxLabel>
                            </td>
                            <td style="padding: 5px 0px 0px 0px; width: 120px;" colspan="2">                
                
                                <dx:ASPxTextBox ID="txtLotNo" runat="server" ClientInstanceName="txtLotNo" 
                                    Width="115px" TabIndex="6" Height="25px" AutoCompleteType="Disabled">
                                </dx:ASPxTextBox>
                
                                </td>            
                        </tr>

                        <tr>
                            <td style=" width:60px; padding:3px 0px 0px 0px">
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Machine No." 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px">
                                </dx:ASPxLabel>
                            </td>
                            <td style=" padding:3px 0px 0px 0px">
                
                                <dx:ASPxComboBox ID="cboMachine" runat="server" Theme="Office2010Black" 
                                    ClientInstanceName="cboMachine" Font-Names="Segoe UI" 
                                    IncrementalFilteringMode="Contains" 
                                    Font-Size="9pt" Height="25px" 
                                    Width="100px" TabIndex="2"
                                    TextFormatString="{0}-{1}-{2}-{3}" DisplayFormatString="{0}" ValueField="MachineNo"
                                    >
                                    <ClientSideEvents SelectedIndexChanged="cboMachineChanged" />
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Machine No" FieldName="MachineNo" Width="90px" />
                                        <dx:ListBoxColumn Caption="Line ID" FieldName="LineID" Width="60px" />
                                        <dx:ListBoxColumn Caption="Sub Line ID" FieldName="SubLineID" Width="60px" />
                                        <dx:ListBoxColumn Caption="Process" FieldName="ProcessName" Width="60px" />
                                    </Columns>
                                    <Items>
                                        <dx:ListEditItem Text="1" Value="1" />
                                        <dx:ListEditItem Text="2" Value="2" />
                                        <dx:ListEditItem Text="3" Value="3" />
                                    </Items>
                                    <ItemStyle Height="10px" Paddings-Padding="4px">
                <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </dx:ASPxComboBox>
                
                            </td>
                            <td style=" width:90px; padding:3px 0px 0px 0px">
                                <dx:ASPxLabel ID="lblqeleader0" runat="server" Text="Part Name" 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width:150px; padding:3px 0px 0px 0px">
                
                    
                                <dx:ASPxTextBox ID="txtpartname" runat="server" BackColor="WhiteSmoke" 
                                    ClientInstanceName="txtpartname" EnableTheming="True" Font-Names="Segoe UI" 
                                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                                    Theme="Office2010Black" Width="150px" ReadOnly="True" TabIndex="-1">
                                </dx:ASPxTextBox>
                
                    
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Last Update" 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="80px">
                                </dx:ASPxLabel>
                            </td>
                            <td colspan="2" style="padding-top: 5px">
                
                    
                                <dx:ASPxTextBox ID="txtLastUpdate" runat="server" BackColor="WhiteSmoke" 
                                    ClientInstanceName="txtLastUpdate" EnableTheming="True" Font-Names="Segoe UI" 
                                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                                    Theme="Office2010Black" Width="115px" ReadOnly="True" TabIndex="-1">
                                </dx:ASPxTextBox>
                
                    
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Line No." 
                                    Font-Names="Segoe UI" Font-Size="9pt">
                                </dx:ASPxLabel>
                            </td>
                            <td style="padding-top: 3px" width="90px">
                
                    
                                <dx:ASPxTextBox ID="txtLineID" runat="server" BackColor="WhiteSmoke" 
                                    ClientInstanceName="txtLineID" EnableTheming="True" Font-Names="Segoe UI" 
                                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                                    Theme="Office2010Black" Width="100px" ReadOnly="True" TabIndex="-1">
                                </dx:ASPxTextBox>
                
                    
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Old Part No." 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="80px">
                                </dx:ASPxLabel>
                            </td>
                            <td style="padding-top: 3px">                
                
                                <dx:ASPxComboBox ID="cboOldPartID" runat="server" Theme="Office2010Black" TextField="PartName"
                                    ClientInstanceName="cboOldPartID" ValueField="PartID" 
                                    Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                                    Width="150px" TabIndex="5" EnableCallbackMode="True" CallbackPageSize="10" 
                                    EnableTheming="True">

                                    <ClientSideEvents SelectedIndexChanged="SelectOldPartID"/>
                                    <Columns>
                                        <dx:ListBoxColumn Caption="Part ID" FieldName="PartID" Width="120px" />
                                        <dx:ListBoxColumn Caption="Part Name" FieldName="PartName" Width="250px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                                    </ButtonStyle>
                                </dx:ASPxComboBox>
                
                                </td>
                            <td></td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="User Update" 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="70px">
                                </dx:ASPxLabel>
                            </td>
                            <td valign="top" colspan="2" style="padding-top: 5px">
                
                    
                                <dx:ASPxTextBox ID="txtUserUpdate" runat="server" BackColor="WhiteSmoke" 
                                    ClientInstanceName="txtUserUpdate" EnableTheming="True" Font-Names="Segoe UI" 
                                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                                    Theme="Office2010Black" Width="115px" ReadOnly="True" TabIndex="-1">
                                </dx:ASPxTextBox>
                
                    
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Sub Line No." 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px">
                                </dx:ASPxLabel>
                            </td>
                            <td style="padding-top: 3px">
                
                    
                                <dx:ASPxTextBox ID="txtSubLine" runat="server" BackColor="WhiteSmoke" 
                                    ClientInstanceName="txtSubLine" EnableTheming="True" Font-Names="Segoe UI" 
                                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                                    Theme="Office2010Black" Width="100px" ReadOnly="True" TabIndex="-1">
                                </dx:ASPxTextBox>
                
                    
                            </td>
                            <td>
                                <dx:ASPxLabel ID="lblqeleader2" runat="server" Text="Part Name" 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px">
                                </dx:ASPxLabel>
                            </td>
                            <td style="padding-top: 3px">
                                <dx:ASPxTextBox ID="txtOldPartName" runat="server" BackColor="WhiteSmoke" 
                                    ClientInstanceName="txtOldPartName" EnableTheming="True" Font-Names="Segoe UI" 
                                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                                    Theme="Office2010Black" Width="150px" ReadOnly="True" TabIndex="-1">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="Revision" 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="70px">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 25px; padding-top: 5px;">
                                <dx:ASPxTextBox ID="txtRevNo" runat="server" BackColor="WhiteSmoke" 
                                    ClientInstanceName="txtRevNo" EnableTheming="True" Font-Names="Segoe UI" 
                                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                                    Theme="Office2010Black" Width="24px" ReadOnly="True" TabIndex="-1">
                                </dx:ASPxTextBox>
                            </td>
                            <td style="padding-top: 5px; padding-left: 1px;">
                                <dx:ASPxTextBox ID="txtRevDate" runat="server" BackColor="WhiteSmoke" 
                                    ClientInstanceName="txtRevDate" EnableTheming="True" Font-Names="Segoe UI" 
                                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                                    Theme="Office2010Black" Width="89px" ReadOnly="True" TabIndex="-1">
                                </dx:ASPxTextBox>
                                </td>
                        </tr>
                        <tr style="height: 10px">
                            <td style=" width:60px; padding:3px 0px 0px 0px">
                                <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="Catatan Kelainan" 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="100px">
                                </dx:ASPxLabel>
                            </td>
                            <td style=" padding:3px 0px 0px 0px" colspan="7">
                                <dx:ASPxTextBox ID="txtNotes" runat="server" BackColor="White" 
                                    ClientInstanceName="txtNotes" EnableTheming="True" Font-Names="Segoe UI" 
                                    Font-Size="9pt" HorizontalAlign="Left" 
                                    Theme="Office2010Black" Width="557px" TabIndex="3">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>       
                </div>
            </td>
            <td valign="top" style="padding-left: 10px">
                <table style="border-style: solid; border-width: 1px">
                    <tr style="border-style: solid; border-width: 1px;">
                        <td style="border-style: solid; border-width: 1px; padding: 5px 0px 0px 0px; width: 70px; background-color:#808080" 
                            align="center">
                        
                                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="Condition" 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px" ForeColor="White">
                                </dx:ASPxLabel>
                        
                        </td>
                        <td style="border-style: solid; border-width: 1px; padding: 5px 0px 0px 0px; width: 75px; background-color:#808080" 
                            align="center">
                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Approval" 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px" ForeColor="White">
                                </dx:ASPxLabel>                        
                        </td>
                        <td style="border-style: solid; border-width: 1px; padding: 5px 0px 0px 0px; width: 75px; background-color:#808080" 
                            align="center">
                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Date" 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px" ForeColor="White">
                                </dx:ASPxLabel>                        
                        </td>
                        <td style="border-style: solid; border-width: 1px; padding: 5px 0px 0px 0px; width: 75px; background-color:#808080" 
                            align="center">
                                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="PIC" 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px" ForeColor="White">
                                </dx:ASPxLabel>                        
                        </td>
                        <td style="border-style: solid; border-width: 1px; padding: 5px 0px 0px 0px; width: 30px; background-color:#808080" 
                            align="center">
                                <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Judge ment" 
                                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px" ForeColor="White">
                                </dx:ASPxLabel>                        
                        </td>
                    </tr>
                    <tr style="border-style: solid; border-width: 1px; background-color: #CCFFCC;">
                        <td rowspan="2" 
                            style="padding-left: 6px; border-bottom-style: solid; border-bottom-width: 1px;">
                        
                                <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="Normal" Font-Names="Segoe UI" 
                                    Font-Size="9pt">
                                </dx:ASPxLabel>
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;">
                        
                                <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="QE Leader" Font-Names="Segoe UI" 
                                    Font-Size="9pt">
                                </dx:ASPxLabel>
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;">
                        
                                <dx:ASPxLabel ID="lblDate1" runat="server" Font-Names="Segoe UI" 
                                    Font-Size="9pt" ClientInstanceName="lblDate1">
                                </dx:ASPxLabel>
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;">
                        
                                <dx:ASPxLabel ID="lblPIC1" runat="server" Text="" Font-Names="Segoe UI" 
                                    Font-Size="9pt" ClientInstanceName="lblPIC1">
                                </dx:ASPxLabel>
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;" 
                            align="center">
                        
                                <dx:ASPxLabel ID="lblJudge1" runat="server" Text="" Font-Names="Segoe UI" 
                                    Font-Size="9pt" ClientInstanceName="lblJudge1">
                                </dx:ASPxLabel>
                        
                        </td>
                    </tr>
                    <tr style="background-color: #CCFFCC">
                        <td style="padding-left: 6px">
                        
                                <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="Line Leader" Font-Names="Segoe UI" 
                                    Font-Size="9pt">
                                </dx:ASPxLabel>
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;">
                        
                                <dx:ASPxLabel ID="lblDate2" runat="server" Font-Names="Segoe UI" 
                                    Font-Size="9pt" ClientInstanceName="lblDate2">
                                </dx:ASPxLabel>
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;">
                                <dx:ASPxLabel ID="lblPIC2" runat="server" Text="" Font-Names="Segoe UI" 
                                    Font-Size="9pt" ClientInstanceName="lblPIC2">
                                </dx:ASPxLabel>                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;" 
                            align="center">
                        
                                <dx:ASPxLabel ID="lblJudge2" runat="server" Text="" Font-Names="Segoe UI" 
                                    Font-Size="9pt" ClientInstanceName="lblJudge2">
                                </dx:ASPxLabel>
                        
                        </td>
                    </tr>
                    <tr style="border-style: solid; border-width: 1px; background-color: #FFCCFF;" 
                        id="trNG1">
                        <td rowspan="2" style="padding-left: 6px">
                        
                                <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="Abnormal" Font-Names="Segoe UI" 
                                    Font-Size="9pt">
                                </dx:ASPxLabel>
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;">
                        
                                <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="Prod Sec. Head" Font-Names="Segoe UI" 
                                    Font-Size="9pt" Width="90px">
                                </dx:ASPxLabel>
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;">
                        
                                <dx:ASPxLabel ID="lblDate3" runat="server" Font-Names="Segoe UI" 
                                    Font-Size="9pt" ClientInstanceName="lblDate3">
                                </dx:ASPxLabel>
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;">
                        
                                <dx:ASPxLabel ID="lblPIC3" runat="server" Text="" Font-Names="Segoe UI" 
                                    Font-Size="9pt" ClientInstanceName="lblPIC3">
                                </dx:ASPxLabel>                        
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;" 
                            align="center">
                        
                                <dx:ASPxLabel ID="lblJudge3" runat="server" Text="" Font-Names="Segoe UI" 
                                    Font-Size="9pt" ClientInstanceName="lblJudge3">
                                </dx:ASPxLabel>
                        
                        </td>
                    </tr>
                    <tr style="background-color: #FFCCFF">
                        <td style="padding-left: 6px">
                        
                                <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="QE Sec. Head" Font-Names="Segoe UI" 
                                    Font-Size="9pt" Width="90px">
                                </dx:ASPxLabel>
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;">
                        
                                <dx:ASPxLabel ID="lblDate4" runat="server" Font-Names="Segoe UI" 
                                    Font-Size="9pt" ClientInstanceName="lblDate4">
                                </dx:ASPxLabel>
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;">
                        
                                <dx:ASPxLabel ID="lblPIC4" runat="server" Text="" Font-Names="Segoe UI" 
                                    Font-Size="9pt" ClientInstanceName="lblPIC4">
                                </dx:ASPxLabel>                        
                        
                        </td>
                        <td style="border-left-style: solid; border-left-width: 1px; padding-left: 6px;" 
                            align="center">
                        
                                <dx:ASPxLabel ID="lblJudge4" runat="server" Text="" Font-Names="Segoe UI" 
                                    Font-Size="9pt" ClientInstanceName="lblJudge4">
                                </dx:ASPxLabel>
                        
                        </td>
                    </tr>                                                                                
                </table>
            </td>
        </tr>
    </table>
    

<div style="height:3px">&nbsp;</div>
<div style="padding: 2px 5px 5px 5px">
        <dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
            EnableTheming="True" Theme="Office2010Black"  
            KeyFieldName="SeqNo"               
            Width="100%" 
            Font-Names="Segoe UI" Font-Size="9pt"
            OnCustomCallback="grid_CustomCallback" TabIndex="-1"
            >
            <ClientSideEvents 
                EndCallback="OnEndCallback" 
                BatchEditStartEditing="OnBatchEditStartEditing" 
                BatchEditEndEditing="OnBatchEditEndEditing" 
                Init="OnInit" 
             />
        <Columns>
              <dx:GridViewDataTextColumn Caption="No" FieldName="SeqNo"
                    VisibleIndex="0" Width="40px">
                </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Process" FieldName="ProcessName" Settings-AutoFilterCondition="Contains" 
            VisibleIndex="1" Width="90px" >
<Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="K Point" VisibleIndex="2" 
                FieldName="KPointStatus" Width="40px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="PICType" VisibleIndex="3" Width="60px" 
                Caption="PIC (OPR/QA)">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CheckPoint" VisibleIndex="5" 
                Width="100px" Caption="Check Point">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Tools" 
                VisibleIndex="6" Caption="Tools" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ValueType" VisibleIndex="7" 
                Width="40px" Caption="Value Type">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Standard" VisibleIndex="8" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Range" VisibleIndex="9" Width="120px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Result" VisibleIndex="10" 
                Width="60px">
                <PropertiesTextEdit Width="59px">
                    <ClientSideEvents TextChanged="UpdateJudgement" />
                    <Style HorizontalAlign="Right">
                    </Style>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="ItemID" FieldName="ItemID" VisibleIndex="4" 
                Width="0px">
            </dx:GridViewDataTextColumn>

              <dx:GridViewDataComboBoxColumn FieldName="Judgement" VisibleIndex="16" 
                  Width="60px" Caption="Judgmnt">
                  <PropertiesComboBox>
                      <Items>
                          <dx:ListEditItem />
                          <dx:ListEditItem Text="OK" Value="OK" />
                          <dx:ListEditItem Text="NG" Value="NG" />
                      </Items>
                  </PropertiesComboBox>
              </dx:GridViewDataComboBoxColumn>

              <dx:GridViewDataTextColumn FieldName="Attachment" VisibleIndex="21" 
                  Width="0px">
                  <CellStyle Wrap="False">
                  </CellStyle>
              </dx:GridViewDataTextColumn>

              <dx:GridViewDataTextColumn FieldName="TCCSResultID" VisibleIndex="22" 
                  Width="0px">
                  <CellStyle Wrap="False">
                  </CellStyle>
              </dx:GridViewDataTextColumn>

              <dx:GridViewDataTextColumn FieldName="NumRangeStart" VisibleIndex="23" 
                  Width="0px">
                  <CellStyle Wrap="False">
                  </CellStyle>
              </dx:GridViewDataTextColumn>
              <dx:GridViewDataTextColumn FieldName="NumRangeEnd" VisibleIndex="24" 
                  Width="0px">
                  <CellStyle Wrap="False">
                  </CellStyle>
              </dx:GridViewDataTextColumn>

              <dx:GridViewBandColumn Caption="Attachment" VisibleIndex="17">
                  <Columns>
                      <dx:GridViewDataTextColumn Caption="Upload" FieldName="LblAttachment" 
                          VisibleIndex="0" Width="60px">
                          <EditFormSettings Visible="False" />
                          <DataItemTemplate>
                              <dx:ASPxHyperLink ID="hyperlink01" runat="server" Font-Names="Segoe UI" 
                                  Font-Size="9pt" OnInit="AttachmentLink_Init" Text="">
                              </dx:ASPxHyperLink>
                          </DataItemTemplate>
                          <CellStyle HorizontalAlign="Center">
                          </CellStyle>
                      </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn FieldName="View" 
                          VisibleIndex="1" Width="60px">
                          <EditFormSettings Visible="False" />
                          <DataItemTemplate>
                              <dx:ASPxHyperLink ID="hyperlink02" runat="server" Font-Names="Segoe UI" 
                                  Font-Size="9pt" OnInit="ViewLink_Init" Text='<%#Eval("View")%>'>
                              </dx:ASPxHyperLink>
                          </DataItemTemplate>
                          <CellStyle HorizontalAlign="Center">
                          </CellStyle>
                      </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn Caption="Remove" FieldName="Remove" VisibleIndex="2" 
                          Width="60px">
                          <EditFormSettings Visible="False" />
                          <DataItemTemplate>
                              <dx:ASPxHyperLink ID="hyperlink03" runat="server" Font-Names="Segoe UI" 
                                  Font-Size="9pt" OnInit="RemoveLink_Init" Text='<%#Eval("Remove")%>'>
                              </dx:ASPxHyperLink>
                          </DataItemTemplate>
                      </dx:GridViewDataTextColumn>
                  </Columns>
              </dx:GridViewBandColumn>

        </Columns>
        <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" 
                AllowFocusedRow="True" AllowDragDrop="False" AllowSort="False" />
        <SettingsEditing Mode="Batch" >
            <BatchEditSettings ShowConfirmOnLosingChanges="False" />
<BatchEditSettings ShowConfirmOnLosingChanges="False"></BatchEditSettings>
            </SettingsEditing>

<SettingsBehavior AllowDragDrop="False" AllowSort="False" AllowFocusedRow="True" 
                ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>

        <SettingsPager AlwaysShowPager="true" Mode="ShowAllRecords" PageSize="30">
        </SettingsPager>
        <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="220" 
            VerticalScrollBarMode="Auto" ShowStatusBar="Hidden" />

<Settings VerticalScrollableHeight="250" ShowStatusBar="Hidden" 
                HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto"></Settings>

        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="false" 
                VerticalAlign="WindowCenter" Width="320" />
<EditForm Width="320px" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter"></EditForm>
        </SettingsPopup>
        <Styles EditFormColumnCaption-Paddings-PaddingLeft="10px" 
            EditFormColumnCaption-Paddings-PaddingRight="10px" 
            Header-Paddings-Padding="5px">
            <Header HorizontalAlign="Center" Wrap="True">
                <Paddings Padding="2px" />
<Paddings Padding="2px"></Paddings>
            </Header>
            <EditFormColumnCaption Font-Names="Segoe UI" Font-Size="9pt">
                <Paddings PaddingBottom="5px" PaddingLeft="15px" PaddingRight="15px" 
                    PaddingTop="5px" />
<Paddings PaddingLeft="15px" PaddingTop="5px" PaddingRight="15px" PaddingBottom="5px"></Paddings>
            </EditFormColumnCaption>
            <CommandColumnItem ForeColor="SteelBlue">
            </CommandColumnItem>
            <BatchEditModifiedCell BackColor="White">
            </BatchEditModifiedCell>
        </Styles>
        <Templates>
            <EditForm>
                <div style="padding: 15px 15px 15px 15px">
                    <dx:ContentControl ID="ContentControl1" runat="server">
                        <dx:ASPxGridViewTemplateReplacement ID="Editors" runat="server" 
                            ReplacementType="EditFormEditors" />
                    </dx:ContentControl>
                </div>
                <div style="text-align: left; padding: 5px 5px 5px 15px">
                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" 
                        ReplacementType="EditFormUpdateButton" />
                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" 
                        ReplacementType="EditFormCancelButton" />
                </div>
            </EditForm>
        </Templates>
    </dx:ASPxGridView>
</div>
<div style="height:5px">

            </div>
<div style="padding: 0px 5px 5px 5px">
    <table style="width: 100%">
        <tr >
            <td style="padding:5px 0px 5px 0px">
                <dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" UseSubmitBehavior="False"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="False"
                    ClientInstanceName="btnExcel" Theme="Default" TabIndex="11">
                    <Paddings Padding="2px" />

                    <ClientSideEvents Click="function(s, e) {     
                            var msg = '';
                            if (cboMachine.GetValue() == '' || cboMachine.GetValue() == null) {
                                cboMachine.Focus();
                                msg = 'Please select Machine!';
                            } else if (cboPartID.GetValue() == '' || cboPartID.GetValue() == null) {
                                cboPartID.Focus();
                                msg = 'Please select New Part No!';
                            } 
                            if (msg != '') {
                                toastr.warning(msg, 'Warning');
                                toastr.options.closeButton = false;
                                toastr.options.debug = false;
                                toastr.options.newestOnTop = false;
                                toastr.options.progressBar = false;
                                toastr.options.preventDuplicates = true;
                                toastr.options.onclick = null;
                                e.processOnServer = false;
                                return;
                            }       
                    }" />
                </dx:ASPxButton>
            </td>
            <td style="padding:5px 0px 5px 0px">
                            
                    <dx:ASPxTextBox runat="server" Width="100px" 
                    HorizontalAlign="Left" Height="25px" ReadOnly="True" ClientInstanceName="txtID" 
                    Theme="Office2010Black" BackColor="WhiteSmoke" EnableTheming="True" 
                    Font-Names="Segoe UI" Font-Size="9pt" TabIndex="7" ID="txtID" 
                        ClientVisible="False"></dx:ASPxTextBox>

                            </td>
            <td style="padding:5px 0px 5px 0px">
                            
<dx:ASPxTextBox runat="server" Width="100px" 
                                    HorizontalAlign="Left" Height="25px" ReadOnly="True" ClientInstanceName="txtJudge" 
                                    Theme="Office2010Black" BackColor="WhiteSmoke" EnableTheming="True" 
                                    Font-Names="Segoe UI" Font-Size="9pt" TabIndex="7" ID="txtJudge" 
                                    ClientVisible="False"></dx:ASPxTextBox>                            
                            
                            </td>
            <td style="padding:5px 0px 5px 0px">
                &nbsp;</td>
            <td style="width:100px;">
                <dx:ASPxButton ID="btnClear" runat="server" Text="Clear" UseSubmitBehavior="False"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="False"
                    ClientInstanceName="btnClear" Theme="Default" TabIndex="10">
                    <ClientSideEvents Click="ClearData" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width: 100px;">
                <dx:ASPxButton ID="btnDelete" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnDelete" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Delete" Theme="Default" UseSubmitBehavior="False" 
                    Width="90px" TabIndex="11">
                    <ClientSideEvents Click="ValidateDelete" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width: 140px;">
                <dx:ASPxButton ID="btnSave" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnSave" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Save" Theme="Default" UseSubmitBehavior="False" 
                    Width="90px" TabIndex="7">
                    <ClientSideEvents Click="ValidateSave" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:100px; padding:0px 0px 0px 0px;">
                <dx:ASPxButton ID="btnApprove" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnApprove" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Approve OK" Theme="Default" UseSubmitBehavior="False" 
                    Width="90px" TabIndex="8">
                    <ClientSideEvents Click="function(s, e) {
	                    cbkValidateApprove.PerformCallback('approve|' + dtDate.GetText() + '|' + cboMachine.GetValue() + '|' + cboPartID.GetValue() + '|' + txtLineID.GetText() + '|' + txtSubLine.GetText());
                    }" 
                    Init="function(s, e) {
	                    btnApprove.SetEnabled(false);
                    }" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:100px; padding:0px 0px 0px 0px;">
                <dx:ASPxButton ID="btnApproveNG" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnApproveNG" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Approve NG" Theme="Default" UseSubmitBehavior="False" 
                    Width="90px" TabIndex="9">
                    <ClientSideEvents 
                    Click="function(s, e) {
	                    cbkValidateNG.PerformCallback('2|' + dtDate.GetText() + '|' + cboMachine.GetValue() + '|' + cboPartID.GetValue() + '|' + txtLineID.GetText() + '|' + txtSubLine.GetText());
                    }"
                    Init="function(s, e) {
	                    btnApproveNG.SetEnabled(false);
                    }" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
        </tr>

        

        </table>
        <table>
        <tr>
            <td>
                <dx:ASPxCallback ID="cbkValid" runat="server" ClientInstanceName="cbkValid">
                                <ClientSideEvents EndCallback="function(s, e) {
                                    var errMsg = s.cpValidationMsg;
			                        if (errMsg != '') {
                                        toastr.warning(errMsg, 'Warning');
                                        toastr.options.closeButton = false;
                                        toastr.options.debug = false;
                                        toastr.options.newestOnTop = false;
                                        toastr.options.progressBar = false;
                                        toastr.options.preventDuplicates = true;
                                        toastr.options.onclick = null;
                                        e.processOnServer = false;
                                        return;
                                    }

                                    grid.UpdateEdit();
                                    var millisecondsToWait = 600;
                                    setTimeout(function () {
                                        grid.PerformCallback('save|' + dtDate.GetText('') + '|' + cboMachine.GetValue() + '|' + cboPartID.GetValue() + '|' + txtLineID.GetText() + '|' + txtSubLine.GetText());
                                    }, millisecondsToWait);
                                }" />
                            </dx:ASPxCallback>
            </td>
            <td>
                <dx:ASPxCallback ID="cbkRefresh" runat="server" ClientInstanceName="cbkRefresh">
                                <ClientSideEvents EndCallback="LoadApproval"/>
                            </dx:ASPxCallback>
            </td>
            <td>
                <dx:ASPxCallback ID="cbkApprove" runat="server" ClientInstanceName="cbkApprove">
                                <ClientSideEvents EndCallback="function(s, e) {
                                    var errMsg = s.cpValidationMsg;
			                        if (errMsg != '') {
                                        toastr.warning(errMsg, 'Warning');
                                        toastr.options.closeButton = false;
                                        toastr.options.debug = false;
                                        toastr.options.newestOnTop = false;
                                        toastr.options.progressBar = false;
                                        toastr.options.preventDuplicates = true;
                                        toastr.options.onclick = null;
                                        e.processOnServer = false;
                                        return;
                                    }
                                    btnUpload.SetEnabled(false);
                                    btnDelete.SetEnabled(false);
                                    var millisecondsToWait = 300;
                                    setTimeout(function () {                                        
                                        cbkRefresh.PerformCallback('approve|' + dtDate.GetText() + '|' + cboMachine.GetValue() + '|' + cboPartID.GetValue() + '|' + txtLineID.GetText() + '|' + txtSubLine.GetText());
                                        toastr.success('Approve data successful', 'Success');
                                        toastr.options.closeButton = false;
                                        toastr.options.debug = false;
                                        toastr.options.newestOnTop = false;
                                        toastr.options.progressBar = false;
                                        toastr.options.preventDuplicates = true;
                                        toastr.options.onclick = null;                                        
                                    }, millisecondsToWait);
                                }" />
                            </dx:ASPxCallback>
            </td>
            <td>
                            
                <dx:ASPxCallback ID="cbkValidateApprove" runat="server" 
        ClientInstanceName="cbkValidateApprove">
                                <ClientSideEvents EndCallback="function(s, e) {
                                    var errMsg = s.cpValidationMsg;
			                        if (errMsg != '') {
                                        toastr.warning(errMsg, 'Warning');
                                        toastr.options.closeButton = false;
                                        toastr.options.debug = false;
                                        toastr.options.newestOnTop = false;
                                        toastr.options.progressBar = false;
                                        toastr.options.preventDuplicates = true;
                                        toastr.options.onclick = null;
                                        e.processOnServer = false;
                                        return;
                                    }

                                    var approveLevel = s.cpApproveLevel;
                                    if (approveLevel == '1') {
                                        if (confirm('If you approve this shift data you can no longer update the data.\nAre you sure you want to approve this data?')) {
                                            ApproveData();
                                        }
                                    } else {
                                        ApproveData();
                                    }
                                }" />
                            </dx:ASPxCallback>
                            
                            </td>
            <td>
                            
                <dx:ASPxCallback ID="cbkDelete" runat="server" ClientInstanceName="cbkDelete">
                                <ClientSideEvents EndCallback="function(s, e) {
                                    var errMsg = s.cpValidationMsg;
			                        if (errMsg != '') {
                                        toastr.warning(errMsg, 'Warning');
                                        toastr.options.closeButton = false;
                                        toastr.options.debug = false;
                                        toastr.options.newestOnTop = false;
                                        toastr.options.progressBar = false;
                                        toastr.options.preventDuplicates = true;
                                        toastr.options.onclick = null;
                                        e.processOnServer = false;
                                        return;
                                    }
                                    ClearData();
                                    toastr.success('Delete data successful!', 'Success');
                                    toastr.options.closeButton = false;
                                    toastr.options.debug = false;
                                    toastr.options.newestOnTop = false;
                                    toastr.options.progressBar = false;
                                    toastr.options.preventDuplicates = true;
                                    toastr.options.onclick = null;
                                    e.processOnServer = false;
                                }" />
                            </dx:ASPxCallback>
                            
                            </td>
            <td>
                            
                <dx:ASPxCallback ID="cbkAttach" runat="server" ClientInstanceName="cbkAttach">
                    <ClientSideEvents Init="OnEndCallback" />
                </dx:ASPxCallback>
                            
                            </td>
            <td>
                            
                <dx:ASPxCallback ID="cbkValidateNG" runat="server" 
        ClientInstanceName="cbkValidateNG">
                                <ClientSideEvents EndCallback="function(s, e) {
                                    var errMsg = s.cpValidationMsg;
			                        if (errMsg != '') {
                                        toastr.warning(errMsg, 'Warning');
                                        toastr.options.closeButton = false;
                                        toastr.options.debug = false;
                                        toastr.options.newestOnTop = false;
                                        toastr.options.progressBar = false;
                                        toastr.options.preventDuplicates = true;
                                        toastr.options.onclick = null;
                                        e.processOnServer = false;
                                        return;
                                    }
                                    var approveLevel = s.cpApproveLevel;
                                    if (approveLevel == '1') {
                                        if (confirm('If you approve this shift data you can no longer update the data.\nAre you sure you want to approve this data?')) {
                                            ApproveNG();
                                        }
                                    } else {
                                        ApproveNG();
                                    }
                                }" />
                            </dx:ASPxCallback>
                            
                            </td>

            <td>
            
                                <dx:ASPxCallback ID="cbkRemove" runat="server" ClientInstanceName="cbkRemove">
                                <ClientSideEvents EndCallback="function(s, e) {
                                    var errMsg = s.cpValidationMsg;
			                        if (errMsg != '') {
                                        toastr.warning(errMsg, 'Warning');
                                        toastr.options.closeButton = false;
                                        toastr.options.debug = false;
                                        toastr.options.newestOnTop = false;
                                        toastr.options.progressBar = false;
                                        toastr.options.preventDuplicates = true;
                                        toastr.options.onclick = null;
                                        e.processOnServer = false;
                                        return;
                                    }
                                    GridLoad();
                                    toastr.success('Remove attachment successful!', 'Success');
                                    toastr.options.closeButton = false;
                                    toastr.options.debug = false;
                                    toastr.options.newestOnTop = false;
                                    toastr.options.progressBar = false;
                                    toastr.options.preventDuplicates = true;
                                    toastr.options.onclick = null;
                                    e.processOnServer = false;
                                }" />
                            </dx:ASPxCallback>
            
            </td>
            <td>
                            &nbsp;</td>                            
        </tr>
        </table>
</div>

<dx:ASPxPopupControl ID="pcUpload" runat="server" CloseAction="CloseButton"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcUpload"
        HeaderText="Upload Attachment" AllowDragging="True" 
        PopupAnimationType="None" Height="130px" 
        Theme="Office2010Black" Width="360px" TabIndex="12">
        <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); }" />
        <ModalBackgroundStyle BackColor="White" CssClass="&quot;noBackground&quot;">
        </ModalBackgroundStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK" Height="100px">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            
                            <table style="width: 100%; margin-top: 15px;">
                                <tr>
                                    <td style="width: 100px">
                                        &nbsp;
                                        <dx:ASPxLabel ID="lblqeleader3" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="File Name" Width="60px">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="width: 10px">
                                        &nbsp;
                                    </td>
                                    <td align="left" colspan="4">
                                        <asp:FileUpload ID="uploader1" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Height="20px" Width="200px" TabIndex="12" 
                                            Accept=".jpg,.jpeg,.png,.gif,.pdf" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="uploader1" ErrorMessage="Please select file!"
                                                ValidationGroup="FileValidationGroup">
                                            </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td colspan="2">
                                        <dx:ASPxTextBox ID="txtItemID" runat="server" BackColor="WhiteSmoke" 
                                            ClientInstanceName="txtItemID" EnableTheming="True" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Height="25px" HorizontalAlign="Left" ReadOnly="True" 
                                            TabIndex="-1" Theme="Office2010Black" Width="100px" ClientVisible="False">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;</td>
                                </tr>
                                <tr style="height: 40px">
                                    <td align="right" colspan="3">
                                        <dx:ASPxButton ID="btnUpload" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="btnUpload" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="25px" Text="Upload" Width="80px" TabIndex="13">
                                            <ClientSideEvents Click="function(s, e) {                                                
                                                Page_ClientValidate('FileValidationGroup');
                                                if (Page_IsValid) {
                                                    grid.UpdateEdit();
                                                    pcUpload.Hide();
                                                }                                                
                                            }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td colspan="2" style="padding-left: 5px">
                                        <dx:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="btnCancel" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="25px" style="float: left; margin-right: 8px" Text="Cancel" 
                                            Width="70px" TabIndex="14">
                                            <ClientSideEvents Click="function(s, e) { pcUpload.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
<asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT Rtrim(ProcessID) as ProcessID, ProcessName FROM Process">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT DISTINCT(RTRIM(MeasuringInstrument)) as MeasuringInstrument FROM QCSItem">
    </asp:SqlDataSource>

</asp:Content>

