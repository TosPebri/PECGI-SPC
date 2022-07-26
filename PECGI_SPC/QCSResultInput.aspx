<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="QCSResultInput.aspx.vb" Inherits="PECGI_SPC.QCSResultInput" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxHiddenField" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" >
        var rowIndex, colIndex;

        function isNumeric(n) {
            if (n == null || n == '') {
                return true;
            } else {
                return !isNaN(parseFloat(n)) && isFinite(n);
            }
        }

        function DeleteData(s, e) {
            if (lblPIC.GetText() != '') {
                toastr.warning('Cannot delete. Data is already approved.', 'Warning');
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
                cbkDelete.PerformCallback('delete|' + dtrevdate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue() + '|' + cboRevNo.GetValue());
            }
        }

        function ClearScreen() {
            cboShift.SetText('');
            cboLineID.SetText('');
            cboSubLine.SetText('');
            cboCP.SetValue(0);
            txtRemark.SetText('');
            txtFileName.SetText('');
            cboPartID.SetText('');
            txtpartname.SetText('');
            txtRevDate.SetText('');
            lblPIC.SetText('');
            lblDate.SetText('');
            cboRevNo.ClearItems();
            cboRevNo.SetText('');

            cboCP.SetEnabled(false);
            cboCP.GetMainElement().style.backgroundColor = 'WhiteSmoke';
            cboCP.GetInputElement().style.backgroundColor = 'WhiteSmoke';
            cboCP.inputElement.style.color = 'Black';

            txtRemark.SetEnabled(false);
            txtRemark.GetMainElement().style.backgroundColor = 'WhiteSmoke';
            txtRemark.GetInputElement().style.backgroundColor = 'WhiteSmoke';
            txtRemark.inputElement.style.color = 'Black';

            btnSave.SetEnabled(false);
            btnDelete.SetEnabled(false);
            btnApprove.SetEnabled(false);
        }

        function OnInit(s, e) {
            btnAttach.SetEnabled(false);
            btnView.SetEnabled(false);
            btnRemove.SetEnabled(false);

            cboCP.SetEnabled(false);
            cboCP.GetMainElement().style.backgroundColor = 'WhiteSmoke';
            cboCP.GetInputElement().style.backgroundColor = 'WhiteSmoke';
            cboCP.inputElement.style.color = 'Black';

            txtRemark.SetEnabled(false);
            txtRemark.GetMainElement().style.backgroundColor = 'WhiteSmoke';
            txtRemark.GetInputElement().style.backgroundColor = 'WhiteSmoke';
            txtRemark.inputElement.style.color = 'Black';

            cboRevNo.SetEnabled(false);
            cboRevNo.GetMainElement().style.backgroundColor = 'WhiteSmoke';
            cboRevNo.GetInputElement().style.backgroundColor = 'WhiteSmoke';
            cboRevNo.inputElement.style.color = 'Black';

            var readOnlyIndexes = grid.cpReadOnlyColumns;
            ASPxClientUtils.AttachEventToElement(s.GetMainElement(), "keydown", function (event) {
                if (event.keyCode == 9 || event.keyCode == 13 || event.keyCode == 40) {
                    if (ASPxClientUtils.IsExists(columnIndex) && ASPxClientUtils.IsExists(rowIndex)) {
                        ASPxClientUtils.PreventEventAndBubble(event);
                        if (rowIndex < grid.GetVisibleRowsOnPage() + grid.GetTopVisibleIndex() - 1) {
                            rowIndex++;
                            grid.batchEditApi.StartEdit(rowIndex, columnIndex);
                        } else {
                            btnSave.Focus();
                        }
                    }
                }

                if (event.keyCode == 38) {
                    if (ASPxClientUtils.IsExists(columnIndex) && ASPxClientUtils.IsExists(rowIndex)) {
                        ASPxClientUtils.PreventEventAndBubble(event);
                        if (rowIndex > 0) {
                            rowIndex--;
                            grid.batchEditApi.StartEdit(rowIndex, columnIndex);
                        }
                    }
                }
            });                                            
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

            if (s.cp_loadgrid == 1) {
                grid.PerformCallback('load|' + dtrevdate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue() + '|' + cboRevNo.GetValue());
                s.cp_loadgrid = 0;
            }

            if (s.cpEnableSave == '1') {
                btnAttach.SetEnabled(true);
            }
            if (s.cpAttach == '1') {
                if (s.cpEnableSave == '1') {                    
                    btnAttach.SetEnabled(true);

                    cboCP.SetEnabled(true);
                    cboCP.GetMainElement().style.backgroundColor = 'White';
                    cboCP.GetInputElement().style.backgroundColor = 'White';
                    cboCP.inputElement.style.color = 'Black';

                    txtRemark.SetEnabled(true);
                    txtRemark.GetMainElement().style.backgroundColor = 'White';
                    txtRemark.GetInputElement().style.backgroundColor = 'White';
                    txtRemark.inputElement.style.color = 'Black';

                    if (s.cpFileName == '') {
                        btnView.SetEnabled(false);
                        btnRemove.SetEnabled(false);
                    } else {
                        btnView.SetEnabled(true);
                        btnRemove.SetEnabled(true);
                    }
                    btnSave.SetEnabled(true);
                    if (s.cpEnableApprove == '1') {
                        btnDelete.SetEnabled(true);
                    } else {
                        btnDelete.SetEnabled(false);
                    }
                } else {
                    cboCP.SetEnabled(false);
                    cboCP.GetMainElement().style.backgroundColor = 'WhiteSmoke';
                    cboCP.GetInputElement().style.backgroundColor = 'WhiteSmoke';
                    cboCP.inputElement.style.color = 'Black';

                    txtRemark.SetEnabled(false);
                    txtRemark.GetMainElement().style.backgroundColor = 'WhiteSmoke';
                    txtRemark.GetInputElement().style.backgroundColor = 'WhiteSmoke';
                    txtRemark.inputElement.style.color = 'Black';

                    btnSave.SetEnabled(false);
                    btnDelete.SetEnabled(false);
                    btnAttach.SetEnabled(false);
                    btnView.SetEnabled(false);
                    btnRemove.SetEnabled(false);
                };
                if (s.cpEnableApprove == '1') {
                    btnApprove.SetEnabled(true);
                } else {
                    btnApprove.SetEnabled(false);
                };
                txtFileName.SetText(s.cpFileName);
                cboRevNo.SetValue(s.cpRevNo);
            }
            lblNRP2.SetText(s.cpNRP2);
            lblNRP3.SetText(s.cpNRP3);
            lblNRP4.SetText(s.cpNRP4);
            lblNRP5.SetText(s.cpNRP5);
            lblRequired.SetText(s.cpRequired);
            var rows = grid.GetVisibleRowsOnPage();
            if (rows == 0) {
                lblHint.SetVisible(true);
            } else {
                lblHint.SetVisible(true);
            }
        }
        var currentColumnName;
        function OnBatchEditStartEditing(s, e) {
            currentColumnName = e.focusedColumn.fieldName;
            if (currentColumnName == "SeqNo" || currentColumnName == "LineID" || currentColumnName == "PartID"
                || currentColumnName == "ProcessID" || currentColumnName == "ProcessName" || currentColumnName == "KPointStatus"
                || currentColumnName == "ItemID" || currentColumnName == "Item" || currentColumnName == "Standard"
                || currentColumnName == "TextRange" || currentColumnName == "NumRangeStart" || currentColumnName == "NumRangeEnd"
                || currentColumnName == "Range" || currentColumnName == "TextValue" || currentColumnName == "NumRangeEnd"
                || currentColumnName == "MeasuringInstrument" || currentColumnName == "FrequencyType"
                || btnSave.GetEnabled() == false
            ) {
                e.cancel = true;
            } else {
                currentEditableVisibleIndex = e.visibleIndex;
                rowIndex = e.visibleIndex;
                columnIndex = e.focusedColumn.index;
                var freqtype = grid.batchEditApi.GetCellValue(rowIndex, 'FrequencyType');
                freqtype = 'A';
                var errmsg = '';
                if (freqtype == 'B') {
                    if (columnIndex == 20 || columnIndex == 22 || columnIndex == 24 || columnIndex == 26) {
                        e.cancel = true;
                        return;
                    }
                }                
                if (columnIndex == 20 && lblNRP2.GetText() == '') {
                    errmsg = "Please input NRP for cycle 1 and save data first!";
                } else if (columnIndex == 22 && lblNRP3.GetText() == '') {
                    errmsg = "Please input NRP for cycle 2 and save data first!";
                } else if (columnIndex == 24 && lblNRP4.GetText() == '') {
                    errmsg = "Please input NRP for cycle 3 and save data first!";
                } else if (columnIndex == 26 && lblNRP5.GetText() == '') {
                    errmsg = "Please input NRP for cycle 4 and save data first!";
                } else if ((columnIndex == 22 || columnIndex == 24 || columnIndex == 26) && lblRequired.GetText() == '1' && txtFileName.GetText() == '') {
                    errmsg = "Please upload attachment first!";
                } else if (columnIndex == 18 || columnIndex == 20 || columnIndex == 22 || columnIndex == 24 || columnIndex == 26) {
                    var levelno = grid.batchEditApi.GetCellValue(rowIndex, 'LevelNo');
                    var rowno;
                    if (levelno == 999) {
                        e.cancel = true;
                    } else if (levelno == 1000) {
                        var noprocess = grid.batchEditApi.GetCellValue(rowIndex + 1, columnIndex);                        
                        if (noprocess == null || noprocess.trim() == '') {
                            for (rowno = 0; rowno < rowIndex - 2; rowno++) {                                
                                freqtype = grid.batchEditApi.GetCellValue(rowno, 'FrequencyType');
                                if (freqtype == '5/Shift') {
                                    var value = grid.batchEditApi.GetCellValue(rowno, columnIndex);
                                    if (value == null || value == '') {
                                        levelno = grid.batchEditApi.GetCellValue(rowno, 'LevelNo');
                                        e.cancel = true;
                                        errmsg = 'Please input value';
                                        grid.batchEditApi.StartEdit(rowno, columnIndex);
                                        break;
                                    }
                                }
                            }
                        }
                    } else if (levelno < 1000) {
                        var rowProcess = grid.GetVisibleRowsOnPage() - 1;
                        var processCls = grid.batchEditApi.GetCellValue(rowProcess, columnIndex);
                        if (processCls == '1' || processCls == '2' || processCls == '3' || processCls == '4') {
                            e.cancel = true;
                            return;
                        }
                    }
                }
                
                if (errmsg != '') {
                    e.cancel = true;                    
                    toastr.warning(errmsg, 'Warning');
                    toastr.options.closeButton = false;
                    toastr.options.debug = false;
                    toastr.options.newestOnTop = false;
                    toastr.options.progressBar = false;
                    toastr.options.preventDuplicates = true;
                    toastr.options.onclick = null;
                    e.processOnServer = false;
                    return;
                }
            }
        }

        function OnBatchEditEndEditing(s, e) {
            rowIndex = null;
            columnIndex = null;
        }

        function cboLineChanged(s, e) {
            cboSubLine.SetEnabled(false);
            cboPartID.SetEnabled(false);
            cboSubLine.PerformCallback('load|' + cboLineID.GetValue());            
            cboPartID.PerformCallback('load|' + cboLineID.GetValue());
            txtpartname.SetText('');
            GridLoad();
        }

        function GridLoad() {
            cbkRefresh.PerformCallback('load|' + dtrevdate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue() + '|0');
        }

        function GridLoadRev() {
            cbkRefresh.PerformCallback('load|' + dtrevdate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue() + '|' + cboRevNo.GetValue());
        }

        function ViewInquiry(s, e) {
            var errmsg = '';
            if (cboLineID.GetValue() == null) {
                cboLineID.Focus()
                errmsg = 'Please select Line ID!';
            } else if (cboSubLine.GetValue() == null) {
                cboSubLine.Focus();
                errmsg = 'Please select Sub Line ID!';
            } else if (cboPartID.GetValue() == null) {
                cboPartID.Focus();
                errmsg = 'Please select Part ID!';
            }
            if (errmsg != '') {
                e.cancel = true;
                toastr.warning(errmsg, 'Warning');
                toastr.options.closeButton = false;
                toastr.options.debug = false;
                toastr.options.newestOnTop = false;
                toastr.options.progressBar = false;
                toastr.options.preventDuplicates = true;
                toastr.options.onclick = null;
                e.processOnServer = false;
                return;
            }
            window.open('QCSResultInquiry.aspx?Date=' + dtrevdate.GetText() + '&LineID=' + cboLineID.GetValue() + '&SubLineID=' + cboSubLine.GetText() + '&PartID=' + cboPartID.GetValue() + '&PartName=' + txtpartname.GetText(), "NewWindow");
        }

        function GetRevNo(s, e) {
            if (s.cpRemark == null) {
                return;
            }
            if (s.cpNotes == null || s.cpNotes == '') {
                cboCP.SetValue(0);
            }
            else {
                cboCP.SetValue(s.cpNotes);
            }            
            txtRemark.SetText(s.cpRemark);
            lblPIC.SetText(s.cpPIC);
            lblDate.SetText(s.cpDate);
            txtFileName.SetText(s.cpFileName);
            var revno = s.cpRevNo;          
            if(revno != '-')
            {
                cboRevNo.SetEnabled(false);
                cboRevNo.ClearItems();
                var revnolist = s.cpRevNoList;
                var index, len;
                len = revnolist.length;
				for(index = 0; index < len; ++index) {
				    var item = revnolist[index];
				    cboRevNo.AddItem(item);
				    if (index == 0) {
				        hfRevNo.Set('revno', item);
				    }
                }
	            cboRevNo.SetSelectedIndex(0);
	            if (cboRevNo.GetItemCount() <= 1) {
	                cboRevNo.SetEnabled(false);
	                cboRevNo.GetMainElement().style.backgroundColor = 'WhiteSmoke';
	                cboRevNo.GetInputElement().style.backgroundColor = 'WhiteSmoke';
	                cboRevNo.inputElement.style.color = 'Black';
	            } else {
	                cboRevNo.SetEnabled(true);
	                cboRevNo.GetMainElement().style.backgroundColor = 'White';
	                cboRevNo.GetInputElement().style.backgroundColor = 'White';
	                cboRevNo.inputElement.style.color = 'Black';
	            }
	        }
	        grid.PerformCallback('load|' + dtrevdate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue() + '|' + cboRevNo.GetValue());
	        txtRevDate.SetText(s.cpRevDate);
	        if (s.cpRevDate == '') {
	            btnSave.SetEnabled(false);
	            btnDelete.SetEnabled(false);
	            btnAttach.SetEnabled(false);
	            btnRemove.SetEnabled(false);
	            btnApprove.SetEnabled(false);
	            btnView.SetEnabled(false);

	            cboCP.SetEnabled(false);
	            cboCP.GetMainElement().style.backgroundColor = 'WhiteSmoke';
	            cboCP.GetInputElement().style.backgroundColor = 'WhiteSmoke';
	            cboCP.inputElement.style.color = 'Black';

	            txtRemark.SetEnabled(false);
	            txtRemark.GetMainElement().style.backgroundColor = 'WhiteSmoke';
	            txtRemark.GetInputElement().style.backgroundColor = 'WhiteSmoke';
	            txtRemark.inputElement.style.color = 'Black';
	        }
	        else {
	            if (s.cpEnableSave == '1') {
	                btnAttach.SetEnabled(true);

	                if (s.cpFileName == '') {
	                    btnView.SetEnabled(false);
	                    btnRemove.SetEnabled(false);
	                } else {
	                    btnView.SetEnabled(true);
	                    btnRemove.SetEnabled(true);	                    
	                }
	                btnSave.SetEnabled(true);
	                if (s.cpEnableApprove == '1') {
	                    btnDelete.SetEnabled(true);
	                } else {
	                    btnDelete.SetEnabled(false);
	                }
	                cboCP.SetEnabled(true);
	                cboCP.GetMainElement().style.backgroundColor = 'White';
	                cboCP.GetInputElement().style.backgroundColor = 'White';
	                cboCP.inputElement.style.color = 'Black';

	                txtRemark.SetEnabled(true);
	                txtRemark.GetMainElement().style.backgroundColor = 'White';
	                txtRemark.GetInputElement().style.backgroundColor = 'White';
	                txtRemark.inputElement.style.color = 'Black';
	            } else {
	                btnSave.SetEnabled(false);
	                btnDelete.SetEnabled(false);
	                btnAttach.SetEnabled(false);
	                btnRemove.SetEnabled(false);

	                if (s.cpFileName == '') {
	                    btnView.SetEnabled(false);
	                } else {
	                    btnView.SetEnabled(true);
	                }

	                cboCP.SetEnabled(false);
	                cboCP.GetMainElement().style.backgroundColor = 'WhiteSmoke';
	                cboCP.GetInputElement().style.backgroundColor = 'WhiteSmoke';
	                cboCP.inputElement.style.color = 'Black';

	                txtRemark.SetEnabled(false);
	                txtRemark.GetMainElement().style.backgroundColor = 'WhiteSmoke';
	                txtRemark.GetInputElement().style.backgroundColor = 'WhiteSmoke';
	                txtRemark.inputElement.style.color = 'Black';
	            };
	            if (s.cpEnableApprove == '1') {
	                btnApprove.SetEnabled(true);
	            } else {
	                btnApprove.SetEnabled(false);
	            };
	        }
            if(s.cpApproveMsg != '') {
                toastr.warning(s.cpApproveMsg, 'Warning');
                toastr.options.closeButton = false;
                toastr.options.debug = false;
                toastr.options.newestOnTop = false;
                toastr.options.progressBar = false;
                toastr.options.preventDuplicates = true;
                toastr.options.onclick = null;
            };
        }

        function SelectPartID(s, e) {
            txtpartname.SetText(cboPartID.GetSelectedItem().GetColumnText(1));
            GridLoad();
        }

        function ValidateSave(s, e) {
            var rows = grid.GetVisibleRowsOnPage();
            var errmsg = '';
            var rowNRP = rows - 2;
            var rowCls = rows - 1;
            var c1 = grid.batchEditApi.GetCellValue(rowCls, 'Cycle1')
            var c2 = grid.batchEditApi.GetCellValue(rowCls, 'Cycle2')
            var c3 = grid.batchEditApi.GetCellValue(rowCls, 'Cycle3')
            var c4 = grid.batchEditApi.GetCellValue(rowCls, 'Cycle4')
            var c5 = grid.batchEditApi.GetCellValue(rowCls, 'Cycle5')
            if (rows == 0) {
                errmsg = 'Please select data!';
            } else if (cboSubLine.GetText() == '') {
                errmsg = 'Please select Sub Line!';
            } else if (isNumeric(grid.batchEditApi.GetCellValue(rowNRP, 'Cycle1')) == false) {
                errmsg = 'Please input numeric for NRP!';
                grid.batchEditApi.StartEdit(rowNRP, 18);
            } else if (isNumeric(grid.batchEditApi.GetCellValue(rowNRP, 'Cycle2')) == false) {
                errmsg = 'Please input numeric for NRP!';
                grid.batchEditApi.StartEdit(rowNRP, 20);
            } else if (isNumeric(grid.batchEditApi.GetCellValue(rowNRP, 'Cycle3')) == false) {
                errmsg = 'Please input numeric for NRP!';
                grid.batchEditApi.StartEdit(rowNRP, 22);
            } else if (isNumeric(grid.batchEditApi.GetCellValue(rowNRP, 'Cycle4')) == false) {
                errmsg = 'Please input numeric for NRP!';
                grid.batchEditApi.StartEdit(rowNRP, 24);
            } else if (isNumeric(grid.batchEditApi.GetCellValue(rowNRP, 'Cycle5')) == false) {
                errmsg = 'Please input numeric for NRP!';
                grid.batchEditApi.StartEdit(rowNRP, 26);
            } else if (c1 != '1' && c1 != '2' && c1 != '3' && c1 != '4' && c1 != null && c1.trim() != '') {
                errmsg = 'Please input 1/2/3/4!';
                grid.batchEditApi.StartEdit(rowCls, 18);
            } else if (c2 != '1' && c2 != '2' && c2 != '3' && c2 != '4' && c2 != null && c2.trim() != '') {
                errmsg = 'Please input 1/2/3/4!';
                grid.batchEditApi.StartEdit(rowCls, 20);
            } else if (c3 != '1' && c3 != '2' && c3 != '3' && c3 != '4' && c3 != null && c3.trim() != '') {
                errmsg = 'Please input 1/2/3/4!';
                grid.batchEditApi.StartEdit(rowCls, 22);
            } else if (c4 != '1' && c4 != '2' && c4 != '3' && c4 != '4' && c4 != null && c4.trim() != '') {
                errmsg = 'Please input 1/2/3/4!';
                grid.batchEditApi.StartEdit(rowCls, 24);
            } else if (c5 != '1' && c5 != '2' && c5 != '3' && c5 != '4' && c5 != null && c5.trim() != '') {
                errmsg = 'Please input 1/2/3/4!';
                grid.batchEditApi.StartEdit(rowCls, 26);
            } else {
                var startIndex = 0;
                var i;
                var processCls = '';
                if (c1 == null) c1 = '';
                if (c2 == null) c2 = '';
                if (c3 == null) c3 = '';
                if (c4 == null) c4 = '';
                if (c5 == null) c5 = '';
                for (i = startIndex; i < startIndex + rows - 3; i++) {
                    var valuetype = grid.batchEditApi.GetCellValue(i, 'ValueType');
                    if (valuetype == 'N') {
                        if (c1 == '' && isNumeric(grid.batchEditApi.GetCellValue(i, 'Cycle1')) == false) {
                            errmsg = 'Please input valid numeric!';
                            grid.batchEditApi.StartEdit(i, 18);
                            break;
                        }
                        if (c2 == '' && isNumeric(grid.batchEditApi.GetCellValue(i, 'Cycle2')) == false) {
                            errmsg = 'Please input valid numeric!';
                            grid.batchEditApi.StartEdit(i, 20);
                            break;
                        }
                        if (c3 == '' && isNumeric(grid.batchEditApi.GetCellValue(i, 'Cycle3')) == false) {
                            errmsg = 'Please input valid numeric!';
                            grid.batchEditApi.StartEdit(i, 22);
                            break;
                        }
                        if (c4 == '' && isNumeric(grid.batchEditApi.GetCellValue(i, 'Cycle4')) == false) {
                            errmsg = 'Please input valid numeric!';
                            grid.batchEditApi.StartEdit(i, 24);
                            break;
                        }
                        if (c5 == '' && isNumeric(grid.batchEditApi.GetCellValue(i, 'Cycle5')) == false) {
                            errmsg = 'Please input valid numeric!';
                            grid.batchEditApi.StartEdit(i, 26);
                            break;
                        }
                    }
                }
            }
            if (errmsg != '') {
                toastr.warning(errmsg, 'Warning');
                toastr.options.closeButton = false;
                toastr.options.debug = false;
                toastr.options.newestOnTop = false;
                toastr.options.progressBar = false;
                toastr.options.preventDuplicates = true;
                toastr.options.onclick = null;
                e.processOnServer = false;
                return;
            }

            cbkValid.PerformCallback('valid|' + cboShift.GetText() + '|' + cboLineID.GetText());
        }

        function ApproveData(s, e) {
            if (grid.GetVisibleRowsOnPage() == 0) {
                toastr.warning('Please select data!', 'Warning');
                toastr.options.closeButton = false;
                toastr.options.debug = false;
                toastr.options.newestOnTop = false;
                toastr.options.progressBar = false;
                toastr.options.preventDuplicates = true;
                toastr.options.onclick = null;
                e.processOnServer = false;
                return;
            }

            if (confirm('If you approve this shift data you can no longer update the data.\nAre you sure you want to approve this data?')) {
                cbkApprove.PerformCallback('approve|' + dtrevdate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue() + '|' + cboRevNo.GetValue());
            }
        }

        function pad(num, size) {
            var s = "00" + num;
            return s.substr(s.length - size);
        }

        function OnGridFocusedRowChanged() {
            gridApproval.GetRowValues(gridApproval.GetFocusedRowIndex(), 'Date;LineID;SubLineID;PartID;PartName;Shift;RevNo', OnGetRowValuesMenu);
        }
        function OnGetRowValuesMenu(values) {
            var date2 = values[0];
            var dd = date2.getDate();
            var mm = date2.getMonth() + 1;
            var yy = date2.getFullYear();
            lblDate2.SetText(yy + '-' + pad(mm, 2) + '-' + pad(dd, 2));
            lblLineID.SetText(values[1].trim());
            lblSubLineID.SetText(values[2].trim());
            lblPartID.SetText(values[3].trim());
            lblPartName.SetText(values[4].trim());   
            lblShift.SetText(values[5]);
            lblRevNo.SetText(values[6]);
        }

        function SelectApproval() {
            if (lblDate2.GetText() == '') {
                toastr.warning('Please select data', 'Warning');
                toastr.options.closeButton = false;
                toastr.options.debug = false;
                toastr.options.newestOnTop = false;
                toastr.options.progressBar = false;
                toastr.options.preventDuplicates = true;
                toastr.options.onclick = null;
                return;
            }
            var date2 = new Date(lblDate2.GetText());
            dtrevdate.SetDate(date2);
            cboShift.SetValue(lblShift.GetText());
            cboLineID.SetValue(lblLineID.GetText());

            cboSubLine.PerformCallback('load|' + cboLineID.GetValue());
            cboPartID.PerformCallback('list|' + cboLineID.GetValue());
            
            pcApproval.Hide();
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
<div style="padding: 0px 5px 5px 5px">
    <table class="auto-style3" style="width: 100%">
        <tr >
            <td style="width:60px; padding:5px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Date" Font-Names="Segoe UI" 
                    Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:140px; padding:5px 0px 0px 0px">
                <dx:ASPxDateEdit ID="dtrevdate" runat="server" Theme="Office2010Black" 
                    Width="100px"
                        ClientInstanceName="dtrevdate" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
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
            <td style=" width:70px; padding:5px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Line No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width:220px; padding:5px 0px 0px 0px">
                <dx:ASPxComboBox ID="cboLineID" runat="server" Theme="Office2010Black" TextField="LineName"
                    ClientInstanceName="cboLineID" ValueField="LineID" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="4">
                    <ClientSideEvents SelectedIndexChanged="cboLineChanged"/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Line ID" FieldName="LineID" Width="70px" />
                        <dx:ListBoxColumn Caption="Line Name" FieldName="LineName" 
                            Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px"/>
                    <ButtonStyle Paddings-Padding="4px" Width="5px"></ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style=" padding: 5px 0px 0px 0px; width:60px" colspan="2">
                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Part No." 
                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px">
                </dx:ASPxLabel>
            </td>
            <td style=" width:10px" colspan="1">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 160px;" colspan="3">                
                
                <dx:ASPxComboBox ID="cboPartID" runat="server" Theme="Office2010Black" TextField="PartName"
                    ClientInstanceName="cboPartID" DropDownStyle="DropDownList" ValueField="PartID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                    Width="150px" TabIndex="6" EnableCallbackMode="true" CallbackPageSize="10" 
                    EnableTheming="True">

                    <ClientSideEvents SelectedIndexChanged="SelectPartID" EndCallback="function(s, e) {
                        cboPartID.SetEnabled(true);
                        var action = s.cpAction;   
                        if(action == 'list') {
                            cboSubLine.SetValue(lblSubLineID.GetText());
                            cboPartID.SetValue(lblPartID.GetText());
                            txtpartname.SetText(lblPartName.GetText());
                            GridLoad();
                        }
                     }" />
                    <Columns>
                        <dx:ListBoxColumn Caption="Part ID" FieldName="PartID" Width="120px" />
                        <dx:ListBoxColumn Caption="Part Name" FieldName="PartName" Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
                
                </td>
            <td style="padding: 5px 0px 0px 0px; width: 75px; border: 1px solid silver; background-color:#808080" align="center">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Approval" Width="90px"
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="White">
                </dx:ASPxLabel>
            </td>
            <td style="padding: 5px 0px 0px 0px; width: 75px; border: 1px solid silver; background-color:#808080" align="center">
                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="PIC" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="White">
                </dx:ASPxLabel>
            </td>
            <td style="padding: 5px 0px 0px 0px; width: 75px; border: 1px solid silver; background-color:#808080" align="center">
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="Date" 
                    Font-Names="Segoe UI" Font-Size="9pt" style="text-align: center" 
                    ForeColor="White">
                </dx:ASPxLabel>
            </td>
        </tr>

        <tr>
            <td style=" width:60px; padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Shift" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:100px; padding:3px 0px 0px 0px">
                
                <dx:ASPxComboBox ID="cboShift" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboShift" Font-Names="Segoe UI" 
                    IncrementalFilteringMode="Contains" 
                    Font-Size="9pt" Height="25px" 
                    Width="100px" TabIndex="2">
                    <ClientSideEvents SelectedIndexChanged="GridLoad" />
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
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Sub Line No." 
                    Font-Names="Segoe UI" Font-Size="9pt" Width="70px">
                </dx:ASPxLabel>
            </td>
            <td style="width:220px; padding:3px 0px 0px 0px">
                
                    
                <dx:ASPxComboBox ID="cboSubLine" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboSubLine" ValueField="SubLineID" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="180px" TabIndex="5">
                    <ClientSideEvents SelectedIndexChanged="GridLoad" EndCallback="function(s, e) {cboSubLine.SetEnabled(true);}" />
                    <Columns>
                        <dx:ListBoxColumn Caption="Sub Line ID" FieldName="SubLineID" Width="70px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px">
<Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
                
                    
            </td>
            <td style=" padding: 3px 0px 0px 0px; width:60px" colspan="2">
                <dx:ASPxLabel ID="lblqeleader0" runat="server" Text="Part Name" 
                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px">
                </dx:ASPxLabel>
            </td>
            <td style=" width:10px" colspan="1">
                &nbsp;</td>
            <td style="width:150px; padding:3px 0px 0px 0px" colspan="3">
                <dx:ASPxTextBox ID="txtpartname" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtpartname" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="150px" ReadOnly="True" TabIndex="-1">
                </dx:ASPxTextBox>
            </td>
            <td style="border: 1px solid silver; padding: 3px 0px 0px 5px; width: 10px;">
                <dx:ASPxLabel ID="lbllineleaderstatus" runat="server" Text="Line Leader" 
                    Font-Names="Segoe UI" Font-Size="9pt" 
                    ClientInstanceName="lbllineleaderstatus">
                </dx:ASPxLabel>
                </td>
            <td style="border: 1px solid silver; padding: 3px 0px 0px 0px; width: 10px;" align="center">
                <dx:ASPxLabel ID="lblPIC" runat="server" 
                    Font-Names="Segoe UI" Font-Size="9pt" Width="100px"
                    ClientInstanceName="lblPIC">
                </dx:ASPxLabel>
                </td>
            <td style="border: 1px solid silver; padding: 3px 0px 0px 0px; width: 10px;" align="center">
                <dx:ASPxLabel ID="lblDate" runat="server" 
                    Font-Names="Segoe UI" Font-Size="9pt" Width="100px"
                    ClientInstanceName="lblDate">
                </dx:ASPxLabel>
                </td>
        </tr>

        <tr>
            <td style=" width:60px; padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Change Point" 
                    Font-Names="Segoe UI" Font-Size="9pt" Width="80px">
                </dx:ASPxLabel>
            </td>
            <td style=" padding:3px 0px 0px 0px" colspan="3">
                <dx:ASPxComboBox ID="cboCP" runat="server" Theme="Office2010Black" ClientInstanceName="cboCP" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" Width="350px" TabIndex="8">
                    <Items>
                        <dx:ListEditItem Value="0" Text="" Selected="True" />
                        <dx:ListEditItem Value="1" Text="Perubahan Desain" />
                        <dx:ListEditItem Value="2" Text="Supplier/Customer Baru" />
                        <dx:ListEditItem Value="3" Text="Perubahan Material" />
                        <dx:ListEditItem Value="4" Text="Perubahan Flow Proses" />
                        <dx:ListEditItem Value="5" Text="Perubahan Syarat Proses" />
                        <dx:ListEditItem Value="6" Text="Perubahan Mesin" />
                        <dx:ListEditItem Value="7" Text="Perubahan Jig Tool" />
                        <dx:ListEditItem Value="8" Text="Perubahan Die" />
                        <dx:ListEditItem Value="9" Text="Perubahan Inspeksi" />
                        <dx:ListEditItem Value="10" Text="Perubahan Packaging/Delivery" />
                        <dx:ListEditItem Value="11" Text="Perubahan Pekerja" />
                        <dx:ListEditItem Value="12" Text="Proses Barang Pertama" />
                        <dx:ListEditItem Value="13" Text="Trouble" />
                        <dx:ListEditItem Value="14" Text="Lain-lain" />
                    </Items>
                    <ItemStyle Height="10px" Paddings-Padding="4px"><Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px"><Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style=" padding:3px 0px 0px 0px; width: 60px;" colspan="2">
                <dx:ASPxLabel ID="lblqeleader1" runat="server" Text="SQC RevNo/Date" 
                    Font-Names="Segoe UI" Font-Size="9pt" Width="100px">
                </dx:ASPxLabel>
            </td>
            <td style=" width: 10px;">
                &nbsp;</td>
            <td style=" width: 60px; padding-top: 3px;">
                
                <dx:ASPxComboBox ID="cboRevNo" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboRevNo" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    Width="58px" TabIndex="8">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                        if(cboRevNo.GetEnabled() == true)
	                        {
		                        GridLoadRev();
	                        }
                            hfRevNo.Set('revno', cboRevNo.GetText());
                        }" />
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
            <td style=" width: 100px; padding-top: 3px;">
                <dx:ASPxTextBox ID="txtRevDate" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtRevDate" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="80px" ReadOnly="True" TabIndex="-1">
                </dx:ASPxTextBox>
            </td>
            <td style=" padding:3px 0px 0px 0px; width: 300px;" colspan="3">

            </td>
        </tr>   
        <tr>
            <td style=" width:60px; padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Remarks" 
                    Font-Names="Segoe UI" Font-Size="9pt" Width="80px">
                </dx:ASPxLabel>            
            </td>
            <td style=" padding:3px 0px 0px 0px" colspan="3">
                <dx:ASPxTextBox ID="txtRemark" runat="server" BackColor="White" 
                    ClientInstanceName="txtRemark" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="350px" TabIndex="3" MaxLength="100" 
                    AutoCompleteType="Disabled">
                </dx:ASPxTextBox>
            </td>
            <td style=" padding:3px 0px 0px 0px; width: 60px;" colspan="2">
            </td>        
        </tr>     
    </table>
    <table style="width:100%">
        <tr style="padding-top: 3px; height: 36px;">
            <td style="width: 80px">
            
                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="Attachment" 
                    Font-Names="Segoe UI" Font-Size="9pt" Width="80px">
                </dx:ASPxLabel>
            
            </td>
            <td style="width: 200px">
            
                <dx:ASPxTextBox ID="txtFileName" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtFileName" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="196px" ReadOnly="True" TabIndex="-1">
                </dx:ASPxTextBox>
            
            </td>
            <td style="width: 65px" valign="middle">
            
                <dx:ASPxButton ID="btnAttach" runat="server" Text="Upload" UseSubmitBehavior="False"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px" Height="25px" AutoPostBack="False"
                    ClientInstanceName="btnAttach" Theme="Default" TabIndex="13">
                    <ClientSideEvents Click="function(s, e) {
	                    pcUpload.Show();
                    }" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            
            </td>
            <td style="width: 65px" valign="middle">
            
                <dx:ASPxButton ID="btnView" runat="server" Text="View" UseSubmitBehavior="False"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px" Height="25px" AutoPostBack="False"
                    ClientInstanceName="btnView" Theme="Default" TabIndex="13">
                    <ClientSideEvents Click="function (s,e) {
                        var resultdate = dtrevdate.GetText();
                        var lineID = cboLineID.GetValue();
                        var partID = cboPartID.GetValue();
                        var shift = cboShift.GetValue();
                        var filename = txtFileName.GetText();
                        var w = 850;
                        var h = 600;
                        var left = 200;
                        var top = 10;
                        var winseting = 'height=' + h + ',width=' + w + ',left=' + left + ',top=' + top;
                        if(filename.toLowerCase().endsWith('pdf')) {
                            var arg = window.open('PreviewPDF.aspx?type=QCS' + '|' + lineID + '|' + partID + '|' + shift + '|' + filename + '|' + resultdate, 'ModalPopUp', winseting);
                        } else {
                            var arg = window.open('PreviewAttachment.aspx?type=QCS' + '|' + lineID + '|' + partID + '|' + shift + '|' + filename + '|' + resultdate, 'ModalPopUp', winseting);
                        }   
                    }" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            
            </td>
            <td valign="middle" style="width: 68px">
            
                <dx:ASPxButton ID="btnRemove" runat="server" Text="Remove" UseSubmitBehavior="False"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px" Height="25px" AutoPostBack="False"
                    ClientInstanceName="btnRemove" Theme="Default" TabIndex="13">                    
                    <ClientSideEvents Click="function(s, e) {
	                        if (confirm('Are you sure you want to delete this attachment?')) {
                                cbkRemove.PerformCallback('remove|' + dtrevdate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue() + '|' + cboRevNo.GetValue() + '|' + txtFileName.GetText() );
                            }
                        }" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            
            </td>
            <td valign="middle" style="width: 95px">
            
                <dx:ASPxButton ID="btnClear" runat="server" Text="Clear" UseSubmitBehavior="False"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="60px" Height="25px" AutoPostBack="False"
                    ClientInstanceName="btnClear" Theme="Default" TabIndex="13">
                    <ClientSideEvents Click="function(s, e) {
                        if (confirm('Are you sure you want to clear this screen?')) {
                            ClearScreen();
                        }                        
                    }" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            
            </td>
            <td>
                    <dx:ASPxButton ID="btnList" runat="server" Text="Approval List" UseSubmitBehavior="False"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="120px" Height="25px" AutoPostBack="False"
                    ClientInstanceName="btnList" Theme="Default" TabIndex="13">
                    <ClientSideEvents Click="function(s, e) {                        
	                    pcApproval.Show();
                        gridApproval.PerformCallback('load');
                    }" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>            
            </td>
        </tr>
    </table>
</div>
<div style="padding: 2px 5px 5px 5px">
        <dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
            EnableTheming="True" KeyFieldName="LevelNo;KeyField" Theme="Office2010Black"
            OnCustomJSProperties="Grid_CustomJSProperties"
            OnStartRowEditing="grid_StartRowEditing" 
            OnRowValidating="grid_RowValidating"
            OnRowInserting="grid_RowInserting" 
            OnRowDeleting="grid_RowDeleting" 
            OnAfterPerformCallback="grid_AfterPerformCallback"  
            OnBatchUpdate="grid_BatchUpdate"          
            Width="100%" 
            Font-Names="Segoe UI" Font-Size="9pt">
            <ClientSideEvents 
                EndCallback="OnEndCallback" 
                BatchEditStartEditing="OnBatchEditStartEditing" 
                BatchEditEndEditing="OnBatchEditEndEditing" 
                Init="OnInit" 
             />
        <Columns>

          <%--  <dx:GridViewDataTextColumn Caption="No" FieldName="SeqNo" Settings-AutoFilterCondition="Contains" 
            VisibleIndex="1" FixedStyle="Left" Width="50px" 
                ShowInCustomizationForm="True" >
                <PropertiesTextEdit MaxLength="3" Width="50px" ClientInstanceName="SeqNo">
                    <Style HorizontalAlign="Left"></Style>
                </PropertiesTextEdit>
                <Settings AutoFilterCondition="Contains"></Settings>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                </CellStyle>
            </dx:GridViewDataTextColumn>--%>

              <dx:GridViewDataTextColumn Caption="No" FieldName="SeqNo"
                    VisibleIndex="2" Width="35px">
                    <PropertiesTextEdit MaxLength="15" Width="50px" ClientInstanceName="ItemID">
                        <%--<ClientSideEvents Validation="SessionIDValidation" />--%>
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>
                    <EditFormSettings VisibleIndex="1" />
                    <HeaderStyle Paddings-PaddingLeft="6px" HorizontalAlign="Center" VerticalAlign="Middle">
<Paddings PaddingLeft="6px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>

            <%--<dx:GridViewDataComboBoxColumn FieldName="ProcessName" VisibleIndex="2" 
                Width="100px" Caption="Process" ShowInCustomizationForm="True" 
                Visible="true">
                <PropertiesComboBox TextField="ProcessName" ValueField="ProcessID" 
                Width="100px" TextFormatString="{0}" ClientInstanceName="ProcessName" DropDownStyle="DropDownList" 
                IncrementalFilteringMode="StartsWith">
                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                    <Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Width="5px" Paddings-Padding="2px" >
                    <Paddings Padding="2px"></Paddings>
                    </ButtonStyle>
                    </PropertiesComboBox>
                    <EditFormSettings VisibleIndex="2" Visible="false" />
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle" >
                        <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </dx:GridViewDataComboBoxColumn>--%>

            <dx:GridViewDataTextColumn Caption="Item" FieldName="Item" Settings-AutoFilterCondition="Contains" 
            VisibleIndex="11" Width="150px" >
                <PropertiesTextEdit MaxLength="50" Width="150px" ClientInstanceName="Item">
                    <Style HorizontalAlign="Left"></Style>
                </PropertiesTextEdit>
                <Settings AutoFilterCondition="Contains"></Settings>
                <EditFormSettings VisibleIndex="5" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Process" VisibleIndex="7" 
                FieldName="ProcessName" Width="100px">
            <PropertiesTextEdit ClientInstanceName="ProcessName"></PropertiesTextEdit>
                <EditFormSettings VisibleIndex="3" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="LineID" VisibleIndex="4" Width="0px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PartID" VisibleIndex="5" Width="110px" 
                Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ProcessID" Visible="False" 
                VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="KPointStatus" VisibleIndex="9" 
                Width="50px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ItemID" VisibleIndex="10" Width="0px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Standard" VisibleIndex="12" Width="140px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Range" FieldName="Range" VisibleIndex="13" 
                Width="120px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="LevelNo" VisibleIndex="1" Width="0px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Status1" FieldName="Status1" 
                Visible="False" VisibleIndex="31" Width="50px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status2" Visible="False" 
                VisibleIndex="32" Width="50px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status3" Visible="False" 
                VisibleIndex="33" Width="50px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status4" Visible="False" 
                VisibleIndex="34" Width="50px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status5" Visible="False" 
                VisibleIndex="35" Width="50px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="KeyField" Visible="False" 
                VisibleIndex="36" Width="50px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn Caption="1" VisibleIndex="18">
                <Columns>
                    <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle1" VisibleIndex="0" 
                        Width="76px">
                        <PropertiesTextEdit MaxLength="15" Width="58px"></PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="2" VisibleIndex="21">
                <Columns>
                    <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle2" VisibleIndex="0" 
                        Width="76px">
                        <PropertiesTextEdit MaxLength="15" Width="58px">
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="3" VisibleIndex="24">
                <Columns>
                    <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle3" VisibleIndex="0" 
                        Width="76px">
                        <PropertiesTextEdit MaxLength="15" Width="58px">
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="4" VisibleIndex="26">
                <Columns>
                    <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle4" VisibleIndex="0" 
                        Width="76px">
                        <PropertiesTextEdit MaxLength="15" Width="58px">
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="5" VisibleIndex="28">
                <Columns>
                    <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle5" VisibleIndex="0" 
                        Width="76px">
                        <PropertiesTextEdit MaxLength="15" Width="58px">
                        </PropertiesTextEdit>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>

            <dx:GridViewDataTextColumn FieldName="ValueType" VisibleIndex="30" Width="0px">
                <PropertiesTextEdit>
                    <Style Wrap="False">
                    </Style>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="MeasuringInstrument" VisibleIndex="16" 
                Width="90px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="FrequencyType" VisibleIndex="17" 
                Width="90px">
                <PropertiesTextEdit><Style Wrap="False"></Style></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>

        </Columns>
        <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" 
                AllowFocusedRow="True" AllowDragDrop="False" AllowSort="False" />
        <SettingsEditing Mode="Batch" >
            <BatchEditSettings ShowConfirmOnLosingChanges="False" />
            </SettingsEditing>
        <SettingsPager AlwaysShowPager="true" Mode="ShowAllRecords" PageSize="30">
        </SettingsPager>
        <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="400" 
            VerticalScrollBarMode="Auto" ShowStatusBar="Hidden" />
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="false" 
                VerticalAlign="WindowCenter" Width="320" />
        </SettingsPopup>
        <Styles EditFormColumnCaption-Paddings-PaddingLeft="10px" 
            EditFormColumnCaption-Paddings-PaddingRight="10px" 
            Header-Paddings-Padding="5px">
            <Header HorizontalAlign="Center" Wrap="True">
                <Paddings Padding="2px" />
            </Header>
            <EditFormColumnCaption Font-Names="Segoe UI" Font-Size="9pt">
                <Paddings PaddingBottom="5px" PaddingLeft="15px" PaddingRight="15px" 
                    PaddingTop="5px" />
            </EditFormColumnCaption>
            <CommandColumnItem ForeColor="SteelBlue">
            </CommandColumnItem>
            <BatchEditModifiedCell BackColor="White" ForeColor="Black">
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
<div style="height:10px">
<table>
    <tr>
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
                                    ApproveData();
                                }" />
                            </dx:ASPxCallback>
        </td>
        <td>
                <dx:ASPxCallback ID="cbkDelete" runat="server" 
                ClientInstanceName="cbkDelete">
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
                                    ClearScreen();
                                    toastr.success('Delete data successful', 'Success');
                                    toastr.options.closeButton = false;
                                    toastr.options.debug = false;
                                    toastr.options.newestOnTop = false;
                                    toastr.options.progressBar = false;
                                    toastr.options.preventDuplicates = true;
                                    toastr.options.onclick = null;
                                }" />
                            </dx:ASPxCallback>
        </td>
        <td>
            <dx:ASPxLabel ID="lblNRP2" runat="server" ClientInstanceName="lblNRP2" ClientVisible="False"></dx:ASPxLabel>
        </td>
        <td>        
            <dx:ASPxLabel ID="lblNRP3" runat="server" ClientInstanceName="lblNRP3" ClientVisible="False"></dx:ASPxLabel>        
        </td>
        <td>
            <dx:ASPxLabel ID="lblNRP4" runat="server" ClientInstanceName="lblNRP4" ClientVisible="False"></dx:ASPxLabel>        
        </td>
        <td>
            <dx:ASPxLabel ID="lblNRP5" runat="server" ClientInstanceName="lblNRP5" ClientVisible="False"></dx:ASPxLabel>        
            <dx:ASPxLabel ID="lblRequired" runat="server" ClientInstanceName="lblRequired" ClientVisible="False"></dx:ASPxLabel>                
        </td>
    </tr>
</table>
                
            </div>
<div style="padding: 0px 0px 0px 0px">
    <table style="width: 100%">
        <tr >
            <td style="padding:0px">
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
                                        grid.PerformCallback('save|' + dtrevdate.GetText('') + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetValue() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue() + '|' + cboRevNo.GetValue() + '|' + cboCP.GetValue() + '|' + txtRemark.GetText());
                                    }, millisecondsToWait);
                                }" />
                            </dx:ASPxCallback>
                <dx:ASPxCallback ID="cbkRefresh" runat="server" ClientInstanceName="cbkRefresh">
                                <ClientSideEvents EndCallback="GetRevNo" Init="GetRevNo" />
                            </dx:ASPxCallback>
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
                                    var millisecondsToWait = 300;
                                    setTimeout(function () {                                        
                                        cbkRefresh.PerformCallback('approve|' + dtrevdate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue() + '|' + cboRevNo.GetValue());
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
            <td style="padding:0px"; align="left">
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
                
                    <dx:ASPxLabel ID="lblHint" ClientInstanceName="lblHint" runat="server" Font-Names="Segoe UI" Font-Size="9pt" 
                    Text="No Process = 1:Tool Change 2:Dandori 3:Machine Trouble 4:Other" Width="350px"></dx:ASPxLabel>                
                </td>
            <td style="width:120px;">
                <dx:ASPxButton ID="btnGetData" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnGetData" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" TabIndex="13" Text="Read QA Machine" Theme="Default" 
                    UseSubmitBehavior="False" Width="110px">
                    <ClientSideEvents Click="function(s, e) {grid.PerformCallback('GetData');}" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:95px;">
                 <dx:ASPxButton ID="btnDelete" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnDelete" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Delete" Theme="Default" UseSubmitBehavior="False" 
                    Width="90px" TabIndex="10">
                    <ClientSideEvents Click="DeleteData" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>               
                </td>
            <td style="width: 60px;">                
                &nbsp;
            </td>
            <td style="width: 150px;">
                <dx:ASPxButton ID="btnSave" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnSave" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Save" Theme="Default" UseSubmitBehavior="False" 
                    Width="90px" TabIndex="10">
                    <ClientSideEvents Click="ValidateSave" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:95px; padding:0px 0px 0px 0px;">
                <dx:ASPxButton ID="btnApprove" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnApprove" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Approve" Theme="Default" UseSubmitBehavior="False" 
                    Width="90px" TabIndex="11">
                    <ClientSideEvents Click="function(s, e) {
                        var errmsg = '';
                        if (lblRequired.GetText() == '1' && txtFileName.GetText() == '') {
                            errmsg = 'Please upload attachment first!';
                        }
                
                        if (errmsg != '') {
                            e.cancel = true;                    
                            toastr.warning(errmsg, 'Warning');
                            toastr.options.closeButton = false;
                            toastr.options.debug = false;
                            toastr.options.newestOnTop = false;
                            toastr.options.progressBar = false;
                            toastr.options.preventDuplicates = true;
                            toastr.options.onclick = null;
                            e.processOnServer = false;
                            return;
                        }

	                    cbkValidateApprove.PerformCallback('approve|' + dtrevdate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue() + '|' + cboRevNo.GetValue());
                    }" 
                     />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:95px; padding:0px 0px 0px 0px;">
                <dx:ASPxButton ID="btnInquiry" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnInquiry" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="SQC Result Inquiry" Theme="Default" UseSubmitBehavior="False" 
                    Width="120px" TabIndex="11">
                    <ClientSideEvents Click="ViewInquiry"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
        </tr>
        </table>

        <table style="width:100%">
            <tr>
                <td>
                
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
                                        <dx:ASPxCallback ID="cbkAttach" runat="server" ClientInstanceName="cbkAttach">
                                            <ClientSideEvents Init="OnEndCallback" />
                                        </dx:ASPxCallback>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;</td>
                                </tr>
                                <tr style="height: 30px">
                                    <td align="center" colspan="5">
                                    
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ControlToValidate="uploader1" ErrorMessage="Please select file!" 
                                            Font-Names="Segoe UI" Font-Size="12px" ForeColor="#FF3300" 
                                            ValidationGroup="FileValidationGroup"></asp:RequiredFieldValidator>
                                    
                                    </td>
                                </tr>
                                <tr style="height: 40px">
                                    <td align="right" colspan="3">
                                        <dx:ASPxButton ID="btnUpload" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="btnUpload" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="25px" Text="Upload" Width="80px" TabIndex="13">
                                            <ClientSideEvents Click="function(s, e) {     
                                                Page_ClientValidate('FileValidationGroup');
                                                if (Page_IsValid) {
                                                    var msg = '';
                                                    if (cboShift.GetValue() == '') {
                                                        msg = 'Please select Shift!';
                                                    } else if (cboPartID.GetValue() == '') {
                                                        cboPartID.Focus();
                                                        msg = 'Please select Part ID!';
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
                
                </td>
                <td>

<dx:ASPxPopupControl ID="pcApproval" runat="server" CloseAction="CloseButton"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcApproval"
        HeaderText="Approval List" AllowDragging="True" 
        PopupAnimationType="None" Height="360px" 
        Theme="Office2010Black" Width="860px" TabIndex="12">
        <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); }" />
        <ModalBackgroundStyle BackColor="White" CssClass="&quot;noBackground&quot;">
        </ModalBackgroundStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK" Height="100px">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            
                            <table style="width: 100%; margin-top: 15px;">
                                <tr>
                                    <td colspan="6">
                                        
                <dx:ASPxGridView ID="gridApproval" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridApproval"
                    Font-Names="Segoe UI" Font-Size="8pt" KeyFieldName="Date;LineID;PartID;SubLineID;Shift;RevNo" Theme="Office2010Black"
                    Width="100%">                    
                    <ClientSideEvents FocusedRowChanged="function(s, e) {OnGridFocusedRowChanged();}" 
                        RowClick="function(s, e) {OnGridFocusedRowChanged();}" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="LineID" ShowInCustomizationForm="True" 
                            VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SubLineID" ShowInCustomizationForm="True" 
                            VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PartID" ShowInCustomizationForm="True" 
                            VisibleIndex="4" Width="120px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Shift" ShowInCustomizationForm="True" 
                            VisibleIndex="6" Width="40px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="RevNo" ShowInCustomizationForm="True" 
                            VisibleIndex="7" Width="40px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="Date" ShowInCustomizationForm="True" 
                            VisibleIndex="1">
                            <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="PartName" ShowInCustomizationForm="True" 
                            VisibleIndex="5" Width="200px">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" AllowSort="False" ColumnResizeMode="Control" />
                    <SettingsPager Mode="ShowAllRecords" NumericButtonCount="10">
                    </SettingsPager>
                    <SettingsEditing Mode="Batch" NewItemRowPosition="Bottom">
                        <BatchEditSettings ShowConfirmOnLosingChanges="False" />
                    </SettingsEditing>
                    <Settings HorizontalScrollBarMode="Visible" ShowStatusBar="Hidden" ShowVerticalScrollBar="True"
                        VerticalScrollableHeight="220" VerticalScrollBarMode="Visible" />
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" 
                        AllowInsert="False" />
                                        <Styles>
                                            <Header HorizontalAlign="Center">
                                                <Paddings PaddingBottom="5px" PaddingTop="5px" />
                                            </Header>
                                        </Styles>
                    <StylesEditors ButtonEditCellSpacing="0">
                        <ProgressBar Height="21px">
                        </ProgressBar>
                    </StylesEditors>
                </dx:ASPxGridView>  


                                    </td>
                                </tr>
                                <tr style="height: 40px">
                                    <td align="right" colspan="3">
                                        <dx:ASPxButton ID="btnListOK" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="btnListOK" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="25px" Text="Select" Width="80px" TabIndex="13">
                                            
                                            <ClientSideEvents Click="SelectApproval" />
                                            
                                        </dx:ASPxButton>
                                    </td>
                                    <td colspan="2" style="padding-left: 5px">
                                        <dx:ASPxButton ID="btnListCancel" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="btnListCancel" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="25px" style="float: left; margin-right: 8px" Text="Cancel" 
                                            Width="70px" TabIndex="14">
                                            <ClientSideEvents Click="function(s, e) { pcApproval.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <div>                                        
                                            <dx:ASPxLabel ID="lblDate2" runat="server" Text="" Font-Names="Segoe UI" Font-Size="9pt" ClientInstanceName="lblDate2" ClientVisible="False"></dx:ASPxLabel>
                                            <dx:ASPxLabel ID="lblLineID" runat="server" Text="" Font-Names="Segoe UI" Font-Size="9pt" ClientInstanceName="lblLineID" ClientVisible="false"></dx:ASPxLabel>
                                            <dx:ASPxLabel ID="lblSubLineID" runat="server" Text="" Font-Names="Segoe UI" Font-Size="9pt" ClientInstanceName="lblSubLineID" ClientVisible="false"></dx:ASPxLabel>
                                            <dx:ASPxLabel ID="lblPartID" runat="server" Text="" Font-Names="Segoe UI" Font-Size="9pt" ClientInstanceName="lblPartID" ClientVisible="false"></dx:ASPxLabel>
                                            <dx:ASPxLabel ID="lblPartName" runat="server" Text="" Font-Names="Segoe UI" Font-Size="9pt" ClientInstanceName="lblPartName" ClientVisible="false"></dx:ASPxLabel>
                                            <dx:ASPxLabel ID="lblShift" runat="server" Text="" Font-Names="Segoe UI" Font-Size="9pt" ClientInstanceName="lblShift" ClientVisible="false"></dx:ASPxLabel>
                                            <dx:ASPxLabel ID="lblRevNo" runat="server" Text="" Font-Names="Segoe UI" Font-Size="9pt" ClientInstanceName="lblRevNo" ClientVisible="false"></dx:ASPxLabel>
                                        </div>
                                        </td>
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
                
                </td>
            </tr>
        </table>
</div>
<asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT Rtrim(ProcessID) as ProcessID, ProcessName FROM Process">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT DISTINCT(RTRIM(MeasuringInstrument)) as MeasuringInstrument FROM QCSItem">
    </asp:SqlDataSource>
    <dx:ASPxHiddenField ID="hfRevNo" runat="server" ClientInstanceName="hfRevNo">
    </dx:ASPxHiddenField>
</asp:Content>
