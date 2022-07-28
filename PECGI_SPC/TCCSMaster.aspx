<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TCCSMaster.aspx.vb" Inherits="PECGI_SPC.TCCSMaster" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register assembly="DevExpress.Web.v20.2, Version=20.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript" >
        <%--Function for Message--%>
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
//=============================================================================================================================================================================================================
if(s.cp_fillheader == 1){
    dtrevdate.SetText(s.cp_revdate);
    txtrevisionhistory.SetText(s.cp_revhistory);
    txtpreparedby.SetText(s.cp_preparedby);
    txtremark.SetText(s.cp_remark);
    lblsectheadpic.SetText(s.cp_approvalpic);
    lblsectheaddate.SetText(s.cp_approvaldate);
    lblstatus.SetText(s.cp_status);

    if (s.cp_approvalstatus == 1){
        lblsectheadpic.GetMainElement().style.color = 'Black';
        lblsectheaddate.GetMainElement().style.color = 'Black';
    }
    else{
        lblsectheadpic.GetMainElement().style.color = 'Red';
        lblsectheaddate.GetMainElement().style.color = 'Red';
    }

    if (s.cp_activestatus == 1){
        lblstatus.GetMainElement().style.color = 'Blue';
    }
    else{
        lblstatus.GetMainElement().style.color = 'Red';
    }
    
    s.cp_fillheader = null;
}

if(s.cp_clicknew == 1){
    cborevno.SetValue('--New--');
    cborevno.SetEnabled(false);
    var today = new Date();
    dtrevdate.SetDate(today);
    txtrevisionhistory.SetText('');
    txtpreparedby.SetText('');
    txtremark.SetText('');
    lblsectheadpic.SetText('UNAPPROVED');
    lblsectheaddate.SetText('UNAPPROVED');
    lblstatus.SetText('Inactive');
    lblsectheadpic.GetMainElement().style.color = 'Red';
    lblsectheaddate.GetMainElement().style.color = 'Red';
    lblstatus.GetMainElement().style.color = 'Red';

    btnCopy.SetEnabled(true);
    btnExcel.SetEnabled(true);
    btnNew.SetEnabled(false);
    btnDelete.SetEnabled(false);
    btnCancel.SetEnabled(true);
    btnSave.SetEnabled(false);
    btnApprove.SetEnabled(false);

    s.cp_clicknew = null;
}

if(s.cp_alertunapproved == 1){
    GridMenu.PerformCallback('GridClear');

    cborevno.SetText('');
    cborevno.SetEnabled(true);
    dtrevdate.SetText('');
    txtrevisionhistory.SetText('');
    txtpreparedby.SetText('');
    txtremark.SetText('');
    lblsectheadpic.SetText('UNAPPROVED');
    lblsectheaddate.SetText('UNAPPROVED');
    lblstatus.SetText('Inactive');
    lblsectheadpic.GetMainElement().style.color = 'Red';
    lblsectheaddate.GetMainElement().style.color = 'Red';
    lblstatus.GetMainElement().style.color = 'Red';

    btnCopy.SetEnabled(false);
    btnExcel.SetEnabled(true);
    btnNew.SetEnabled(true);
    btnDelete.SetEnabled(false);
    btnCancel.SetEnabled(false);
    btnSave.SetEnabled(false);
    btnApprove.SetEnabled(false);

    ClickNewStatus = true;

    toastr.warning('Part ID : ' + cbopartid.GetValue() + ', Machine No : ' + cbomachineno.GetValue() + ', Line ID : ' + txtlineid.GetValue() + ', Sub Line ID : ' + txtsublineid.GetValue() + ', Rev No : ' + s.cp_revno + ', has not been approved', 'Warning');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;
    e.processOnServer = false;
    s.cp_alertunapproved = null;
    return;   
}

if(s.cp_alertlineid == 1){
    
    toastr.warning('Please select Line No', 'Warning');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;
    e.processOnServer = false;
    s.cp_alertlineid = null;
    return;   
}

if(s.cp_alertsublineid == 1){
    
    toastr.warning('Please select Sub Line No', 'Warning');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;
    e.processOnServer = false;
    s.cp_alertsublineid = null;
    return;   
}

if(s.cp_alertmachineno == 1){
    
    toastr.warning('Please select Machine No', 'Warning');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;
    e.processOnServer = false;
    s.cp_alertmachineno = null;
    return;   
}

if(s.cp_alertpartno == 1){
    
    toastr.warning('Please select Part No', 'Warning');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;
    e.processOnServer = false;
    s.cp_alertpartno = null;
    return;   
}

if(s.cp_alertrevno == 1){
    
    toastr.warning('Please select Rev No', 'Warning');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;
    e.processOnServer = false;
    s.cp_alertrevno = null;
    return;   
}

if(s.cp_alertrevdate == 1){
    
    toastr.warning('Please select Rev Date', 'Warning');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;
    e.processOnServer = false;
    s.cp_alertrevdate = null;
    return;   
}

if(s.cp_alertrevhistory == 1){
    
    toastr.warning('Please fill Rev History', 'Warning');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;
    e.processOnServer = false;
    s.cp_alertrevhistory = null;
    return;   
}

if(s.cp_alertpreparedby == 1){
    
    toastr.warning('Please fill Prepared By', 'Warning');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;
    e.processOnServer = false;
    s.cp_alertpreparedby = null;
    return;   
}

if (s.cp_aftersave == 1){
    cborevno.PerformCallback();
    cborevno.SetText(s.cp_revno);
    cborevno.SetValue(s.cp_revno);
    cborevno.SetEnabled(true);

    btnCopy.SetEnabled(true);
    btnExcel.SetEnabled(true);
    btnNew.SetEnabled(true);
    btnCancel.SetEnabled(false);
    btnDelete.SetEnabled(true);
    btnSave.SetEnabled(true);
    ClickNewStatus = true;

    GridMenu.PerformCallback('SelectRevNo');

    s.cp_aftersave = null;
}

if (s.cp_btnapprovetrue == 1){
    btnApprove.SetEnabled(true);
//    btnCopy.SetEnabled(true);
//    btnExcel.SetEnabled(true);
//    btnNew.SetEnabled(true);
//    btnCancel.SetEnabled(false);
//    btnDelete.SetEnabled(true);
//    btnSave.SetEnabled(true);
    s.cp_btnapprovetrue = null;
}

if (s.cp_btnapprovefalse == 1){
    btnApprove.SetEnabled(false);
    btnCopy.SetEnabled(false);
    btnExcel.SetEnabled(true);
//    btnNew.SetEnabled(true);
//    btnCancel.SetEnabled(false);
    btnDelete.SetEnabled(false);
    btnSave.SetEnabled(false);
    s.cp_btnapprovefalse = null;
}

if (s.cp_authtrue == 1){
    btnCopy.SetEnabled(true);
    btnDelete.SetEnabled(true);
    btnSave.SetEnabled(true);
    s.cp_authtrue = null;
}

if (s.cp_approved == 1){
    btnApprove.SetEnabled(false);
    btnCopy.SetEnabled(false);
    btnExcel.SetEnabled(true);
    btnNew.SetEnabled(true);
    btnCancel.SetEnabled(false);
    btnDelete.SetEnabled(false);
    btnSave.SetEnabled(false);
    GridMenu.PerformCallback('SelectRevNo');
    s.cp_approved = null;
}

if (s.cp_valuetypet == 1){
    SetTextRangeVisibility(true);
    SetNumRangeStartVisibility(false);
    SetNumRangeEndVisibility(false);
    s.cp_valuetypet = null;
}

if (s.cp_valuetypen == 1){
    SetTextRangeVisibility(false);
    SetNumRangeStartVisibility(true);
    SetNumRangeEndVisibility(true);
    s.cp_valuetypen = null;
}

if (s.cp_allbtninit == 1){
    btnCopy.SetEnabled(false);
    btnExcel.SetEnabled(true);
    btnNew.SetEnabled(true);
    btnCancel.SetEnabled(false);
    btnDelete.SetEnabled(false);
    btnSave.SetEnabled(false);
    btnApprove.SetEnabled(false);
    s.cp_allbtninit = null;
}

if (s.cp_clearall == 1){
    cbopartid.PerformCallback();
    txtpartname.SetValue('');
    cbomachineno.PerformCallback();
    txtlineid.SetValue('');
    txtsublineid.SetValue('');
    cborevno.PerformCallback();
    dtrevdate.SetText('');
    txtrevisionhistory.SetValue('');
    txtpreparedby.SetValue('');
    txtremark.SetValue('');
    lblsectheadpic.SetValue('UNAPPROVED');
    lblsectheaddate.SetValue('UNAPPROVED');
    lblstatus.SetValue('Inactive');
    lblsectheadpic.GetMainElement().style.color = 'Red';
    lblsectheaddate.GetMainElement().style.color = 'Red';
    lblstatus.GetMainElement().style.color = 'Red';
    GridMenu.PerformCallback('GridClear');
    s.cp_clearall = null;
}
}//End Callback

function SelectPartID(s, e) {
    txtpartname.SetText(cbopartid.GetSelectedItem().GetColumnText(1));
    cbomachineno.PerformCallback();
    txtlineid.SetText('');
    txtsublineid.SetText('');
    cborevno.SetText('');
    dtrevdate.SetText('');
    txtrevisionhistory.SetText('');
    txtpreparedby.SetText('');
    txtremark.SetText('');
    lblsectheadpic.SetText('UNAPPROVED');
    lblsectheaddate.SetText('UNAPPROVED');
    lblstatus.SetText('Inactive');
    lblsectheadpic.GetMainElement().style.color = 'Red';
    lblsectheaddate.GetMainElement().style.color = 'Red';
    lblstatus.GetMainElement().style.color = 'Red';
    GridMenu.PerformCallback('GridClear');

    btnCopy.SetEnabled(false);
    btnCancel.SetEnabled(false);
    btnDelete.SetEnabled(false);
    btnSave.SetEnabled(false);
    btnApprove.SetEnabled(false);

    if (ClickNewStatus == false){
        cborevno.SetValue('--New--');
        cborevno.SetEnabled(false);
        var today = new Date();
        dtrevdate.SetDate(today);
        btnNew.SetEnabled(false);
        btnCancel.SetEnabled(true);
    }
}

function SelectMachineNo(s, e) {
    txtlineid.SetValue(cbomachineno.GetSelectedItem().GetColumnText(1));
    txtsublineid.SetValue(cbomachineno.GetSelectedItem().GetColumnText(2));
    cborevno.PerformCallback();
    dtrevdate.SetText('');
    txtrevisionhistory.SetText('');
    txtpreparedby.SetText('');
    txtremark.SetText('');
    lblsectheadpic.SetText('UNAPPROVED');
    lblsectheaddate.SetText('UNAPPROVED');
    lblstatus.SetText('Inactive');
    lblsectheadpic.GetMainElement().style.color = 'Red';
    lblsectheaddate.GetMainElement().style.color = 'Red';
    lblstatus.GetMainElement().style.color = 'Red';
    GridMenu.PerformCallback('GridClear');

    btnCopy.SetEnabled(false);
    btnCancel.SetEnabled(false);
    btnDelete.SetEnabled(false);
    btnSave.SetEnabled(false);
    btnApprove.SetEnabled(false);

    if (ClickNewStatus == false){
        GridMenu.PerformCallback('ApprovalStatusCheck');
    }

}

function SelectRevNo(s, e) {
    GridMenu.PerformCallback('SelectRevNo');
}

var startTime;
function OnBeginCallbackPartID() {
	startTime = new Date();
}
function OnEndCallbackPartID() {
	var result = new Date() - startTime;
	result /= 1000;
	result = result.toString();
	if(result.length > 4){
		result = result.substr(0, 4);
    }
}

var ClickNewStatus = true;
function ClickNew(s, e) {
    if (cbomachineno.GetValue() == null){
        cborevno.SetValue('--New--');
        cborevno.SetEnabled(false);
        var today = new Date();
        dtrevdate.SetDate(today);
        txtrevisionhistory.SetText('');
        txtpreparedby.SetText('');
        txtremark.SetText('');
        lblsectheadpic.SetText('UNAPPROVED');
        lblsectheaddate.SetText('UNAPPROVED');
        lblstatus.SetText('Inactive');
        lblsectheadpic.GetMainElement().style.color = 'Red';
        lblsectheaddate.GetMainElement().style.color = 'Red';
        lblstatus.GetMainElement().style.color = 'Red';

        btnCopy.SetEnabled(true);
        btnExcel.SetEnabled(true);
        btnNew.SetEnabled(false);
        btnDelete.SetEnabled(false);
        btnCancel.SetEnabled(true);
        btnSave.SetEnabled(false);
        btnApprove.SetEnabled(false);

        GridMenu.PerformCallback('NewGridVisibleTrue');
        ClickNewStatus = false;
    }
    else{
        GridMenu.PerformCallback('ApprovalStatusCheck');
        ClickNewStatus = false;
    }
} 

function ClickCancel(s, e) {
    cborevno.SetText('');
    cborevno.SetEnabled(true);
    dtrevdate.SetText('');
    txtrevisionhistory.SetText('');
    txtpreparedby.SetText('');
    txtremark.SetText('');
    lblsectheadpic.SetText('UNAPPROVED');
    lblsectheaddate.SetText('UNAPPROVED');
    lblstatus.SetText('Inactive');
    lblsectheadpic.GetMainElement().style.color = 'Red';
    lblsectheaddate.GetMainElement().style.color = 'Red';
    lblstatus.GetMainElement().style.color = 'Red';

    btnCopy.SetEnabled(false);
    btnExcel.SetEnabled(true);
    btnNew.SetEnabled(true);
    btnDelete.SetEnabled(false);
    btnCancel.SetEnabled(false);
    btnSave.SetEnabled(false);
    btnApprove.SetEnabled(false);

    ClickNewStatus = true;

    GridMenu.PerformCallback('NewGridVisibleFalse');
} 

function ClickSave(s, e) {
    var revno = cborevno.GetValue();
    GridMenu.PerformCallback('Update');
    cborevno.PerformCallback();
    cborevno.SetValue(revno);
} 

function ClickDelete(s, e) {  
    var r = confirm("Are you sure want to delete?");
    if (r == true) {
        GridMenu.PerformCallback('Delete|');
    } 
} 

function ClickApprove(s, e) {
    GridMenu.PerformCallback('Approve');
} 

function SelectValueTypeInit(s,e){
    SetTextRangeVisibility(false);
    SetNumRangeStartVisibility(false);
    SetNumRangeEndVisibility(false);
}

function SelectValueType(s,e){
    if (ValueType.GetValue() == 'T'){
        SetTextRangeVisibility(true);
        SetNumRangeStartVisibility(false);
        SetNumRangeEndVisibility(false);
    }
    if (ValueType.GetValue() == 'N'){
        SetTextRangeVisibility(false);
        SetNumRangeStartVisibility(true);
        SetNumRangeEndVisibility(true);
    }
}

var TextRangeVisible = true;
function SetTextRangeVisibility(visible) {
    TextRangeVisible = visible;
    var disp = visible ? 'table-cell' : 'none';
    $('td.TextRange').css('display', disp);
}

var NumRangeStartVisible = true;
function SetNumRangeStartVisibility(visible) {
    NumRangeStartVisible = visible
    var disp = visible ? 'table-cell' : 'none';
    $('td.NumRangeStart').css('display', disp);
}

var NumRangeEndVisible = true;
function SetNumRangeEndVisibility(visible) {
    NumRangeEndVisible = visible
    var disp = visible ? 'table-cell' : 'none';
    $('td.NumRangeEnd').css('display', disp);
}

function ClickCopy(s, e) {
    if (cbopartid.GetText() == '' ) {
        toastr.warning('Please select Part No!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    } 

    if (cbomachineno.GetText() == '' ) {
        toastr.warning('Please select Machine No!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    } 

    if (cborevno.GetText() == '' ) {
        toastr.warning('Please select Rev No!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    } 

    if (dtrevdate.GetText() == '' ) {
        toastr.warning('Please select Rev Date!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    } 

    if (txtrevisionhistory.GetText() == '' ) {
        toastr.warning('Please Fill Revision History!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    } 

    if (txtpreparedby.GetText() == '' ) {
        toastr.warning('Please Fill Prepared By!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    }

    var rowcount = GridMenu.GetVisibleRowsOnPage(); 
    if (rowcount > 0){
        var txt;
        var r = confirm("Are you sure to Copy From Other Part? Previous data will be delete!");
        if (r == true) {
            cbomachinenopopup.PerformCallback();
            cbopartidpopup.PerformCallback();
            txtpartnamepopup.SetValue('');
            cborevnopopup.PerformCallback();
            txtlineidpopup.SetValue('');
            txtsublineidpopup.SetValue('');
            pcLogin.Show();
        }
        else{
            pcLogin.Hide();
        }
    }
    else {
        cbomachinenopopup.PerformCallback();
        cbopartidpopup.PerformCallback();
        txtpartnamepopup.SetValue('');
        cborevnopopup.PerformCallback();
        txtlineidpopup.SetValue('');
        txtsublineidpopup.SetValue('');
        pcLogin.Show();
    }
}


function ClickCopyPopUp(s, e) {
    if (cbopartidpopup.GetText() == '' ) {
        toastr.warning('Please select Part No!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
        }

        if (cbomachinenopopup.GetText() == '' ) {
        toastr.warning('Please select Machine No!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
        }

        if (cborevnopopup.GetText() == '' ) {
        toastr.warning('Please select Rev No!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
        }

    GridMenu.PerformCallback('Copy|' + cbomachinenopopup.GetValue()  + '|' + cbopartidpopup.GetValue() + '|' + cborevnopopup.GetValue());
    pcLogin.Hide();
}

function SelectProcess(s, e) {
    ProcessName.SetText(ProcessID.GetSelectedItem().GetColumnText(1));
}  

function SelectPartIDPopup(s, e) {
    txtpartnamepopup.SetText(cbopartidpopup.GetSelectedItem().GetColumnText(1));
    cbomachinenopopup.PerformCallback();
}  

function SelectMachineNoPopup(s, e) {
    txtlineidpopup.SetValue(cbomachinenopopup.GetSelectedItem().GetColumnText(1));
    txtsublineidpopup.SetValue(cbomachinenopopup.GetSelectedItem().GetColumnText(2));
    cborevnopopup.PerformCallback();
}  

function ClickExcel(s, e) {
    if (cbopartid.GetText() == '' ) {
        toastr.warning('Please select Part No!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    } 

    if (cbomachineno.GetText() == '' ) {
        toastr.warning('Please select Machine No!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    } 

    if (cborevno.GetText() == '' ) {
        toastr.warning('Please select Rev No!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    } 

    if (dtrevdate.GetText() == '' ) {
        toastr.warning('Please select Rev Date!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    } 

    if (txtrevisionhistory.GetText() == '' ) {
        toastr.warning('Please Fill Revision History!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    } 

    if (txtpreparedby.GetText() == '' ) {
        toastr.warning('Please Fill Prepared By!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    }
    GridMenu.PerformCallback('Excel|');
}

function ClickClear(s, e) {
    cbopartid.SetText('');
    txtpartname.SetText('');
    cbomachineno.SetText('');
    txtlineid.SetText('');
    txtsublineid.SetText('');
    cborevno.SetText('');
    cborevno.SetEnabled(true);
    dtrevdate.SetText('');
    txtrevisionhistory.SetText('');
    txtpreparedby.SetText('');
    txtremark.SetText('');
    lblsectheadpic.SetText('UNAPPROVED');
    lblsectheaddate.SetText('UNAPPROVED');
    lblstatus.SetText('Inactive');
    lblsectheadpic.GetMainElement().style.color = 'Red';
    lblsectheaddate.GetMainElement().style.color = 'Red';
    lblstatus.GetMainElement().style.color = 'Red';
    GridMenu.PerformCallback('GridClear');

    btnCopy.SetEnabled(false);
    btnDelete.SetEnabled(false);
    btnCancel.SetEnabled(false);
    btnSave.SetEnabled(false);
    btnApprove.SetEnabled(false);
}  
</script>
    <style type="text/css">
        .style1
        {
            width: 83px;
        }
        .style3
        {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<div style="padding: 0px 5px 5px 5px">
    <table style="width: 100%;">
        <tr>
            <td style="padding: 5px 0px 5px 0px" width="75px">
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Part No" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td width="10px">
                &nbsp;</td>
            <td style="padding: 5px 0px 3px 0px" width="100px">
                <dx:ASPxComboBox ID="cbopartid" runat="server" Theme="Office2010Black" TextField="PartName"
                    ClientInstanceName="cbopartid" DropDownStyle="DropDownList" ValueField="PartID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                    Width="150px" TabIndex="1" EnableCallbackMode="true" CallbackPageSize="10">
                    <ClientSideEvents SelectedIndexChanged="SelectPartID" BeginCallback="function(s, e) { OnBeginCallbackPartID(); }" EndCallback="function(s, e) { OnEndCallbackPartID(); } "/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Part ID" FieldName="PartID" Width="110px" />
                        <dx:ListBoxColumn Caption="Part Name" FieldName="PartName" Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td width="15px">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px" width="75px">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Line No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td width="10px">
                &nbsp;
            </td>
            <td style="padding: 5px 0px 3px 0px" width="100px">
               <%--<dx:ASPxComboBox ID="cbolineid" runat="server" Theme="Office2010Black" TextField="LineName"
                    ClientInstanceName="cbolineid" DropDownStyle="DropDownList" ValueField="LineID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="4" ReadOnly="true" BackColor="WhiteSmoke" >
                    <Columns>
                        <dx:ListBoxColumn Caption="Line ID" FieldName="LineID" Width="70px" />
                        <dx:ListBoxColumn Caption="Line Name" FieldName="LineName" 
                            Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px"/>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>--%>
                <dx:ASPxTextBox ID="txtlineid" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtlineid" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="100px" ReadOnly="True" TabIndex="4">
                </dx:ASPxTextBox>
                    
            </td>
            <td width="15px">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px" width="75px">
                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Rev. No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td width="10px">
                &nbsp;</td>
            <td style="padding: 5px 0px 3px 0px" width="190px">
                <dx:ASPxComboBox ID="cborevno" runat="server" Theme="Office2010Black" TextField="RevNo"
                    ClientInstanceName="cborevno" DropDownStyle="DropDownList" ValueField="RevNo"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="StartsWith" TextFormatString="{0}-{1}-{2}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="6">
                    <ClientSideEvents SelectedIndexChanged="SelectRevNo"/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Rev No" FieldName="RevNo" Width="70px"/>
                        <dx:ListBoxColumn Caption="Rev Date" FieldName="RevDate" Width="70px"/>
                        <dx:ListBoxColumn Caption="Rev History" FieldName="RevHistory" Width="200px"/>
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Width="5px" Paddings-Padding="4px" ></ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td width="10px">
                &nbsp;</td>
            <td width="100px" 
                style="border: 1px solid silver; padding: 5px 0px 5px 0px; background-color: #808080;" 
                align="center">
                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Approval" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="White">
                </dx:ASPxLabel>
                &nbsp;
            </td>
            <td style="border: 1px solid silver; padding: 5px 0px 3px 0px; background-color: #808080;" 
                width="100px" align="center">
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="PIC" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="White">
                </dx:ASPxLabel>
            </td>
            <td style="border: 1px solid silver; padding: 5px 0px 3px 0px; background-color: #808080;" 
                width="100px" align="center">
                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="Date" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="White">
                </dx:ASPxLabel>
            </td>
            <td style="border: 1px solid silver; padding: 5px 0px 3px 0px; background-color: #808080;" 
                width="50px" align="center">
                <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="Status" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="White">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td style="padding: 3px 0px 5px 0px" width="75px">
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Part Name" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td width="10px">
                &nbsp;</td>
            <td style="padding: 0px 0px 3px 0px" width="100px">
                <dx:ASPxTextBox ID="txtpartname" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtpartname" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="150px" ReadOnly="True" TabIndex="2">
                </dx:ASPxTextBox>
                    
            </td>
            <td width="15px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px" width="75px">
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Sub Line No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td width="10px">
                &nbsp;
            </td>
            <td style="padding: 0px 0px 3px 0px" width="100px">
               <%--<dx:ASPxComboBox ID="cbosublineid" runat="server" Theme="Office2010Black" TextField="SubLineID"
                    ClientInstanceName="cbosublineid" DropDownStyle="DropDownList" ValueField="SubLineID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="5" ReadOnly="true" BackColor="WhiteSmoke" >
                    <Columns>
                        <dx:ListBoxColumn Caption="Sub Line ID" FieldName="SubLineID" Width="70px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px"/>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>--%>
                    
                <dx:ASPxTextBox ID="txtsublineid" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtsublineid" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="100px" ReadOnly="True" TabIndex="5">
                </dx:ASPxTextBox>
                    
            </td>
            <td width="15px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px" width="75px">
                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Rev. Date" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td width="10px">
                &nbsp;</td>
            <td style="padding: 0px 0px 3px 0px" width="190px">
                <dx:ASPxDateEdit ID="dtrevdate" runat="server" Theme="Office2010Black" 
                    Width="100px" AutoPostBack="false"
                        ClientInstanceName="dtrevdate" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="7">
                        <CalendarProperties>
                            <HeaderStyle Font-Size="12pt" Paddings-Padding="5px" />
                            <DayStyle Font-Size="9pt" Paddings-Padding="5px" />
                            <WeekNumberStyle Font-Size="9pt" Paddings-Padding="5px"></WeekNumberStyle>
                            <FooterStyle Font-Size="9pt" Paddings-Padding="10px" />
                            <ButtonStyle Font-Size="9pt" Paddings-Padding="10px"></ButtonStyle>
                        </CalendarProperties>
                        <ButtonStyle Width="5px" Paddings-Padding="4px" ></ButtonStyle>
                    </dx:ASPxDateEdit>
                    
            </td>
            <td width="10px">
                &nbsp;</td>
            <td width="100px" 
                style="padding: 3px 0px 0px 0px; border: 1px solid silver; " 
                align="center">
                <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Prod. Sect. Head" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding: 3px 0px 0px 0px; border: 1px solid silver; " 
                width="100px" align="center">
                <dx:ASPxLabel ID="lblsectheadpic" runat="server" Text="UNAPPROVED" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="Red"
                    ClientInstanceName="lblsectheadpic">
                </dx:ASPxLabel>
                </td>
            <td style="padding: 3px 0px 0px 0px; border: 1px solid silver; " width="100px" 
                align="center">
                <dx:ASPxLabel ID="lblsectheaddate" runat="server" Text="UNAPPROVED" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="Red" 
                    ClientInstanceName="lblsectheaddate">
                </dx:ASPxLabel>
            </td>
            <td style="border: 1px solid silver; padding: 3px 0px 0px 0px;" width="50px" 
                align="center">
                <dx:ASPxLabel ID="lblstatus" runat="server" Text="Inactive" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="Red" 
                    ClientInstanceName="lblstatus">
                </dx:ASPxLabel>
                </td>
        </tr>
        <tr>
            <td style="padding: 3px 0px 5px 0px" width="75px">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Machine No" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td width="10px">
                &nbsp;</td>
            <td style="padding: 0px 0px 3px 0px" width="100px">
               <dx:ASPxComboBox ID="cbomachineno" runat="server" Theme="Office2010Black" TextField="MachineNo"
                    ClientInstanceName="cbomachineno" DropDownStyle="DropDownList" ValueField="MachineNo"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}-{2}-{3}-{4}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="3">
                    <ClientSideEvents SelectedIndexChanged="SelectMachineNo"/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Machine No" FieldName="MachineNo" Width="70px" />
                        <dx:ListBoxColumn Caption="Line ID" FieldName="LineID" Width="70px" />
                        <dx:ListBoxColumn Caption="Sub Line ID" FieldName="SubLineID" Width="80px" />
                        <dx:ListBoxColumn Caption="Process ID" FieldName="ProcessID" Width="80px" />
                        <dx:ListBoxColumn Caption="Process Name" FieldName="ProcessName" Width="80px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px"/>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td width="15px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px" width="75px">
                &nbsp;</td>
            <td width="10px">
                &nbsp;</td>
            <td style="padding: 0px 0px 3px 0px" width="100px">
                &nbsp;</td>
            <td width="15px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px" width="75px">
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Rev. History" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td width="10px">
                &nbsp;</td>
            <td style="padding: 0px 0px 3px 0px" width="190px">
                <dx:ASPxTextBox ID="txtrevisionhistory" runat="server" BackColor="White" 
                    ClientInstanceName="txtrevisionhistory" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="175px" TabIndex="8">
                </dx:ASPxTextBox>
                    
            </td>
            <td width="10px">
                &nbsp;</td>
            <td width="100px" 
                style="padding: 3px 0px 0px 0px; " align="center">
                &nbsp;</td>
            <td style="padding: 3px 0px 0px 0px; " 
                width="100px" align="center">
                &nbsp;</td>
            <td style="padding: 3px 0px 0px 0px; " width="100px" align="center">
                &nbsp;</td>
            <td style="padding: 3px 0px 0px 0px" width="50px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="padding: 3px 0px 5px 0px" width="75px">
                <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="Remark" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td width="10px">
                &nbsp;</td>
            <td style="padding: 0px 0px 5px 0px" colspan="5">
                <dx:ASPxTextBox ID="txtremark" runat="server" BackColor="White" 
                    ClientInstanceName="txtremark" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="350px" TabIndex="10">
                </dx:ASPxTextBox>
                    
            </td>
            <td width="15px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px" width="75px">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Prepared By" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td width="10px">
                &nbsp;</td>
            <td style="padding: 0px 0px 5px 0px" width="190px">
                <dx:ASPxTextBox ID="txtpreparedby" runat="server" BackColor="White" 
                    ClientInstanceName="txtpreparedby" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="175px" TabIndex="9">
                </dx:ASPxTextBox>
            </td>
            <td width="10px">
                &nbsp;</td>
            <td width="100px" 
                style="padding: 3px 0px 5px 0px; " align="center">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; " width="100px" align="center">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; " width="100px" align="center">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px" width="50px">
                &nbsp;</td>
        </tr>
    </table>
</div>

<div style="padding: 2px 5px 5px 5px">

<dx:ASPxGridView ID="GridMenu" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridMenu"
    EnableTheming="True" KeyFieldName="ItemID" Theme="Office2010Black" 
    OnStartRowEditing="GridMenu_StartRowEditing" OnRowValidating="GridMenu_RowValidating"
    OnRowInserting="GridMenu_RowInserting" OnRowDeleting="GridMenu_RowDeleting" 
    OnAfterPerformCallback="GridMenu_AfterPerformCallback" OnCellEditorInitialize="GridMenu_CellEditorInitialize" Width="100%" 
    Font-Names="Segoe UI" Font-Size="9pt">
    <ClientSideEvents EndCallback="OnEndCallback" />
    <Columns>
        <dx:GridViewCommandColumn FixedStyle="Left" ShowDeleteButton="true" 
                ShowEditButton="true" ShowNewButtonInHeader="true" 
                VisibleIndex="0" Width="100px">
            </dx:GridViewCommandColumn>

        <dx:GridViewDataTextColumn Caption="ID" FieldName="ItemID" VisibleIndex="1" 
            Width="50px" Visible="False">
            <PropertiesTextEdit MaxLength="15" Width="50px" ClientInstanceName="ItemID">
                <Style HorizontalAlign="Left"></Style>
            </PropertiesTextEdit>
            <EditFormSettings VisibleIndex="1" Visible="True" />

<EditFormSettings VisibleIndex="1"></EditFormSettings>

            <HeaderStyle Paddings-PaddingLeft="6px" HorizontalAlign="Center" VerticalAlign="Middle">
                <Paddings PaddingLeft="6px"></Paddings>
            </HeaderStyle>
            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn Caption="Sequence No." FieldName="SeqNo" VisibleIndex="16" 
            Width="95px">
            <PropertiesTextEdit MaxLength="15" Width="50px" ClientInstanceName="SeqNo">
                <Style HorizontalAlign="Left"></Style>
            </PropertiesTextEdit>
            <EditFormSettings VisibleIndex="2" />

<EditFormSettings VisibleIndex="1"></EditFormSettings>

            <HeaderStyle Paddings-PaddingLeft="6px" HorizontalAlign="Center" VerticalAlign="Middle">
                <Paddings PaddingLeft="6px"></Paddings>
            </HeaderStyle>
            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataComboBoxColumn Caption="Process ID" FieldName="ProcessID" 
            VisibleIndex="3" Width="100px" Visible="False">

<EditFormSettings VisibleIndex="3" Visible="True"></EditFormSettings>

            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
            <PropertiesComboBox DropDownStyle="DropDownList" Width="80px" TextFormatString="{0}" IncrementalFilteringMode="Contains" 
                TextField="ProcessID" DisplayFormatInEditMode="true" ValueField="ProcessID" ClientInstanceName="ProcessID">
                <ClientSideEvents SelectedIndexChanged="SelectProcess"/>
                <Columns>
                    <dx:listboxcolumn Caption="Process ID" FieldName="ProcessID" Width="68px"/>
                    <dx:listboxcolumn Caption="Process Name" FieldName="ProcessName" Width="150px" />
                 </Columns>
                 <ItemStyle Height="10px" Paddings-Padding="4px" >
                    <Paddings Padding="4px"></Paddings>
                 </ItemStyle>
                 <ButtonStyle Width="5px" Paddings-Padding="4px" >
                    <Paddings Padding="4px"></Paddings>
                 </ButtonStyle>
            </PropertiesComboBox>
            <EditFormSettings VisibleIndex="2" />
        </dx:GridViewDataComboBoxColumn>

        <dx:GridViewDataTextColumn Caption="Process" FieldName="ProcessName"
            VisibleIndex="4" Width="100px" Settings-AutoFilterCondition="Contains" 
            ReadOnly="True">
            <PropertiesTextEdit MaxLength="35" Width="100px" ClientInstanceName="ProcessName">
                <Style HorizontalAlign="Left"></Style>
            </PropertiesTextEdit>
            <Settings AutoFilterCondition="Contains"></Settings>
            <EditFormSettings Visible="True" VisibleIndex="4" Caption="Process Name"/>

<EditFormSettings VisibleIndex="3"></EditFormSettings>

            <FilterCellStyle Paddings-PaddingRight="4px">
                <Paddings PaddingRight="4px"></Paddings>
            </FilterCellStyle>
            <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                <Paddings PaddingLeft="5px"></Paddings>
            </HeaderStyle>
            <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataComboBoxColumn Caption="K Point" FieldName="KPointStatus" 
            VisibleIndex="5" Width="60px">
            <PropertiesComboBox DropDownStyle="DropDownList" Width="60px" TextFormatString="{0}" 
                IncrementalFilteringMode="StartsWith" TextField="KPointStatus" DisplayFormatInEditMode="true" 
                ValueField="KPointStatus" ClientInstanceName="KPointStatus" >
                <Items>
                    <dx:ListEditItem Text="" Value="B" />
                    <dx:ListEditItem Text="Ⓚ" Value="Ⓚ"/>
                    </Items>
                <ItemStyle Height="10px" Paddings-Padding="4px" >
                    <Paddings Padding="4px"></Paddings>
                </ItemStyle>
                <ButtonStyle Width="5px" Paddings-Padding="2px" >
                    <Paddings Padding="2px"></Paddings>
                </ButtonStyle>
            </PropertiesComboBox>
            <EditFormSettings VisibleIndex="5" />

<EditFormSettings VisibleIndex="4"></EditFormSettings>

            <HeaderStyle Paddings-PaddingLeft="2px" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                <Paddings PaddingLeft="2px"></Paddings>
            </HeaderStyle>
            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
        </dx:GridViewDataComboBoxColumn>

        <dx:GridViewDataComboBoxColumn Caption="PIC (OPR / QA)" FieldName="PICType" 
            VisibleIndex="6">
            <PropertiesComboBox DropDownStyle="DropDownList" Width="60px" TextFormatString="{0}" 
                IncrementalFilteringMode="StartsWith" TextField="PICType" DisplayFormatInEditMode="true" 
                ValueField="PICType" ClientInstanceName="PICType" >
                <Items>
                    <dx:ListEditItem Text="OPR" Value="OPR" />
                    <dx:ListEditItem Text="QA" Value="QA"/>
                    </Items>
                <ItemStyle Height="10px" Paddings-Padding="4px" >
                    <Paddings Padding="4px"></Paddings>
                </ItemStyle>
                <ButtonStyle Width="5px" Paddings-Padding="2px" >
                    <Paddings Padding="2px"></Paddings>
                </ButtonStyle>
            </PropertiesComboBox>
            <EditFormSettings VisibleIndex="6" />

<EditFormSettings VisibleIndex="5"></EditFormSettings>

            <HeaderStyle Paddings-PaddingLeft="2px" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                <Paddings PaddingLeft="2px"></Paddings>
            </HeaderStyle>
            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
            </CellStyle>
        </dx:GridViewDataComboBoxColumn>

        <%--<dx:GridViewDataTextColumn Caption="Check Point" FieldName="Item" Settings-AutoFilterCondition="Contains" 
            VisibleIndex="6" Width="150px" >
            <PropertiesTextEdit MaxLength="50" Width="150px" ClientInstanceName="Item">
                <Style HorizontalAlign="Left"></Style>
            </PropertiesTextEdit>
            <Settings AutoFilterCondition="Contains"></Settings>
            <EditFormSettings VisibleIndex="4" />

<EditFormSettings VisibleIndex="6"></EditFormSettings>

            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        </dx:GridViewDataTextColumn>--%>

        <dx:GridViewDataComboBoxColumn Caption="Check Point" FieldName="Item" 
                    VisibleIndex="7" Width="150px">
                    <EditFormSettings VisibleIndex="7" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox
                                    ClientInstanceName="Item" 
                                    DropDownStyle="DropDown" 
                                    Width="150px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="Item" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="Item"
                                    DataSourceID="SqlDataSource1"
                                    >
                                    <Columns>
                                        <dx:listboxcolumn Caption="Check Point" FieldName="Item" Width="68px"/>
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataComboBoxColumn>

        <%--<dx:GridViewDataTextColumn Caption="Tools" FieldName="Tools" Settings-AutoFilterCondition="Contains" 
            VisibleIndex="7" Width="150px" >
            <PropertiesTextEdit MaxLength="50" Width="150px" ClientInstanceName="Tools">
                <Style HorizontalAlign="Left"></Style>
            </PropertiesTextEdit>
            <Settings AutoFilterCondition="Contains"></Settings>
            <EditFormSettings VisibleIndex="5" />

<EditFormSettings VisibleIndex="7"></EditFormSettings>

            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        </dx:GridViewDataTextColumn>--%>

        <dx:GridViewDataComboBoxColumn Caption="Tools" FieldName="Tools" 
                    VisibleIndex="8" Width="150px">
                    <EditFormSettings VisibleIndex="8" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox
                                    ClientInstanceName="Tools" 
                                    DropDownStyle="DropDown" 
                                    Width="150px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="Tools" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="Tools"
                                    DataSourceID="SqlDataSource2"
                                    >
                                    <Columns>
                                        <dx:listboxcolumn Caption="Tools" FieldName="Tools" Width="68px"/>
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataComboBoxColumn>

        <dx:GridViewDataComboBoxColumn Caption="ValueType" FieldName="ValueType"
            VisibleIndex="9" Width="75px">
            <PropertiesComboBox DropDownStyle="DropDownList" Width="70px" TextFormatString="{0}"
                IncrementalFilteringMode="StartsWith" TextField="ValueType" DisplayFormatInEditMode="true"
                ValueField="ValueType" ClientInstanceName="ValueType" >
                <ClientSideEvents SelectedIndexChanged="SelectValueType" Init="SelectValueTypeInit"/>
                <Items>
                    <dx:ListEditItem Text="Numeric" Value="N" />
                    <dx:ListEditItem Text="Text" Value="T" Selected="false"/>
                </Items>
                <ItemStyle Height="10px" Paddings-Padding="4px" >
                    <Paddings Padding="4px"></Paddings>
                </ItemStyle>
                <ButtonStyle Width="5px" Paddings-Padding="2px" >
                    <Paddings Padding="2px"></Paddings>
                </ButtonStyle>
            </PropertiesComboBox>
            <EditFormSettings VisibleIndex="9" />

<EditFormSettings VisibleIndex="8"></EditFormSettings>

            <HeaderStyle Paddings-PaddingLeft="2px" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                <Paddings PaddingLeft="2px"></Paddings>
            </HeaderStyle>
            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
        </dx:GridViewDataComboBoxColumn>

        <dx:GridViewDataTextColumn Caption="Standard" FieldName="Standard" Settings-AutoFilterCondition="Contains" 
            VisibleIndex="10" Width="150px" >
            <PropertiesTextEdit MaxLength="50" Width="150px" ClientInstanceName="Standard">
                <Style HorizontalAlign="Left"></Style>
            </PropertiesTextEdit>
            <Settings AutoFilterCondition="Contains"></Settings>
            <EditFormSettings VisibleIndex="10" />

<EditFormSettings VisibleIndex="9"></EditFormSettings>

            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
            </CellStyle>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn Caption="Range" FieldName="Range" Settings-AutoFilterCondition="Contains" 
            VisibleIndex="11" Width="150px" >
            <PropertiesTextEdit MaxLength="15" Width="150px" ClientInstanceName="Range">
                <Style HorizontalAlign="Left"></Style>
            </PropertiesTextEdit>
            <Settings AutoFilterCondition="Contains"></Settings>
            <EditFormSettings Visible="False" />

<EditFormSettings Visible="False"></EditFormSettings>

            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
            </CellStyle>
        </dx:GridViewDataTextColumn>
            
        <dx:GridViewDataTextColumn Caption="NumRangeStart" FieldName="NumRangeStart" 
            Settings-AutoFilterCondition="Contains" VisibleIndex="12" Width="150px" 
            Visible="False" EditFormSettings-Caption="">
            <PropertiesTextEdit DisplayFormatString="d" ClientInstanceName="NumRangeStart" >
                <MaskSettings Mask="<0..999999g>.<00..999>"/>
            </PropertiesTextEdit>
            <Settings AutoFilterCondition="Contains"></Settings>
            <EditFormSettings VisibleIndex="12" Visible="True" />
            <EditCellStyle CssClass="NumRangeStart" />

<EditFormSettings Visible="True" VisibleIndex="11"></EditFormSettings>

<EditCellStyle CssClass="NumRangeStart"></EditCellStyle>

            <EditFormCaptionStyle CssClass="NumRangeStart"></EditFormCaptionStyle>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn Caption="NumRangeEnd" FieldName="NumRangeEnd" 
            VisibleIndex="13" Width="150px" Visible="False">
            <PropertiesTextEdit DisplayFormatString="c" ClientInstanceName="NumRangeEnd">
                <MaskSettings Mask="<0..999999g>.<00..999>"/>
            </PropertiesTextEdit>
            <EditFormSettings VisibleIndex="13" Visible="True" />
            <EditCellStyle CssClass="NumRangeEnd" />

<EditFormSettings Visible="True" VisibleIndex="12"></EditFormSettings>

<EditCellStyle CssClass="NumRangeEnd"></EditCellStyle>

            <EditFormCaptionStyle CssClass="NumRangeEnd"></EditFormCaptionStyle>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn Caption="TextRange" FieldName="TextRange"
            VisibleIndex="14" Width="150px" Settings-AutoFilterCondition="Contains" 
            Visible="False" >
            <PropertiesTextEdit MaxLength="35" Width="150px" ClientInstanceName="TextRange">
                <Style HorizontalAlign="Left"></Style>
            </PropertiesTextEdit>
            <Settings AutoFilterCondition="Contains"></Settings>
            <EditFormSettings VisibleIndex="14" Caption="TextRange" Visible="True" />
            <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="TextRange">
                <Paddings PaddingLeft="5px"></Paddings>
            </HeaderStyle>
            <CellStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="TextRange"></CellStyle>
            <EditCellStyle CssClass="TextRange" />

<EditFormSettings Visible="True" VisibleIndex="13" Caption="TextRange"></EditFormSettings>

<EditCellStyle CssClass="TextRange"></EditCellStyle>

            <EditFormCaptionStyle CssClass="TextRange"></EditFormCaptionStyle>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn Caption="Remark" FieldName="Remark" Settings-AutoFilterCondition="Contains" 
            VisibleIndex="15" Width="150px" >
            <PropertiesTextEdit MaxLength="50" Width="150px" ClientInstanceName="Remark">
                <Style HorizontalAlign="Left"></Style>
            </PropertiesTextEdit>
            <Settings AutoFilterCondition="Contains"></Settings>
            <EditFormSettings VisibleIndex="15" />

<EditFormSettings VisibleIndex="14"></EditFormSettings>

            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
            </CellStyle>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn Caption="No" FieldName="Number" VisibleIndex="2" 
            Width="35px">
            <EditFormSettings Visible="False" />
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
            </CellStyle>
        </dx:GridViewDataTextColumn>

        </Columns>
    <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" />
    <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />

<SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>

    <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true" PageSizeItemSettings-Visible="true">
        <PageSizeItemSettings Visible="True"></PageSizeItemSettings>
    </SettingsPager>
    <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="200" VerticalScrollBarMode="Auto" />
    <SettingsText ConfirmDelete="Are you sure want to delete ?" />

<SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>

<Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto"></Settings>

<SettingsText ConfirmDelete="Are you sure want to delete ?"></SettingsText>

    <SettingsPopup>
        <EditForm HorizontalAlign="WindowCenter" Modal="false" VerticalAlign="WindowCenter" Width="320" />
<EditForm Width="320px" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter"></EditForm>
    </SettingsPopup>
    <Styles EditFormColumnCaption-Paddings-PaddingLeft="10px" EditFormColumnCaption-Paddings-PaddingRight="10px" Header-Paddings-Padding="5px">
        <Header>
            <Paddings Padding="2px" />
<Paddings Padding="2px"></Paddings>
        </Header>
        <EditFormColumnCaption Font-Names="Segoe UI" Font-Size="9pt">
            <Paddings PaddingBottom="5px" PaddingLeft="15px" PaddingRight="15px" PaddingTop="5px" />
<Paddings PaddingLeft="15px" PaddingTop="5px" PaddingRight="15px" PaddingBottom="5px"></Paddings>
        </EditFormColumnCaption>
    </Styles>
    <Templates>
        <EditForm>
            <div style="padding: 15px 15px 15px 15px">
                <dx:ContentControl ID="ContentControl1" runat="server">
                    <dx:ASPxGridViewTemplateReplacement ID="Editors" runat="server" ReplacementType="EditFormEditors" />
                </dx:ContentControl>
            </div>
            <div style="text-align: left; padding: 5px 5px 5px 15px">
                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" ReplacementType="EditFormUpdateButton" />
                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" ReplacementType="EditFormCancelButton" />
            </div>
        </EditForm>
    </Templates>
</dx:ASPxGridView>
</div>

<div style="padding: 0px 5px 5px 5px">
    <table class="auto-style3" style="width: 100%">
        <tr >
            <td style="width:150px; padding:5px 0px 5px 0px; border-top: 1px solid silver"">
                <dx:ASPxButton ID="btnCopy" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnCopy" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Copy From Other Part" Theme="Default" UseSubmitBehavior="false" 
                    Width="150px" TabIndex="11" ClientEnabled="False">
                    <ClientSideEvents Click="ClickCopy" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style=" width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="width:50px; padding:5px 0px 5px 0px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnExcel" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnExcel" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Excel" Theme="Default" UseSubmitBehavior="false" 
                    Width="90px" TabIndex="12">
                    <ClientSideEvents Click="ClickExcel" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style=" width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:50px; border-top: 1px solid silver">
                &nbsp;</td>
            <td class="style1" style="border-top: 1px solid silver">
                &nbsp;</td>
            <td class="style1" style="border-top: 1px solid silver">
                &nbsp;</td>
            <td class="style1" style="border-top: 1px solid silver; width: 50px;">
                <dx:ASPxButton ID="btnClear" runat="server" Text="Clear" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnClear" Theme="Default" TabIndex="13">
                    <ClientSideEvents Click="ClickClear"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td class="style1" style="border-top: 1px solid silver; width: 10px;" 
                width="10px">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:50px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnNew" runat="server" Text="New" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnNew" Theme="Default" TabIndex="14" 
                    ClientEnabled="False">
                    <ClientSideEvents Click="ClickNew"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:50px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnDelete" runat="server" Text="Delete" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnDelete" Theme="Default" TabIndex="15" 
                    ClientEnabled="False">
                    <ClientSideEvents Click="ClickDelete"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:50px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnCancel" runat="server" Text="Cancel" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnCancel" Theme="Default" TabIndex="16" 
                    ClientEnabled="False">
                    <ClientSideEvents Click="ClickCancel"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:50px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnSave" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnSave" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Save" Theme="Default" UseSubmitBehavior="false" 
                    Width="90px" TabIndex="17" ClientEnabled="False">
                    <ClientSideEvents Click="ClickSave"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="width: 10px; border-top: 1px solid silver">
            </td>
            <td style="width:50px; padding:5px 0px 5px 0px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnApprove" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnApprove" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Approve" Theme="Default" UseSubmitBehavior="false" 
                    Width="90px" TabIndex="18" ClientEnabled="False">
                    <ClientSideEvents Click="ClickApprove"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
        </tr>
        </table>
</div>

<div>
   <dx:ASPxPopupControl ID="pcLogin" runat="server" CloseAction="CloseButton"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcLogin"
        HeaderText="Copy From Other Part" AllowDragging="True" 
        PopupAnimationType="None" Height="290px" 
        Theme="Office2010Black" Width="360px">
        <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />
        <ModalBackgroundStyle BackColor="White" CssClass="&quot;noBackground&quot;">
        </ModalBackgroundStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK" Height="220px">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="70px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="150px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="70px">
                                        <dx:ASPxLabel ID="ASPxLabel18" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Part No.">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 3px 0px 0px 0px;" width="150px">
                                        <dx:ASPxComboBox ID="cbopartidpopup" runat="server" Theme="Office2010Black" TextField="PartName"
                                            ClientInstanceName="cbopartidpopup" DropDownStyle="DropDownList" ValueField="PartID"
                                            EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                                            IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                                            Width="150px" TabIndex="19" EnableCallbackMode="true" 
                                            CallbackPageSize="10">
                                            <ClientSideEvents SelectedIndexChanged="SelectPartIDPopup" BeginCallback="function(s, e) { OnBeginCallbackPartID(); }" EndCallback="function(s, e) { OnEndCallbackPartID(); } "/>
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Part ID" FieldName="PartID" Width="110px" />
                                                <dx:ListBoxColumn Caption="Part Name" FieldName="PartName" Width="250px" />
                                            </Columns>
                                            <ItemStyle Height="10px" Paddings-Padding="4px" >
<Paddings Padding="4px"></Paddings>
                                            </ItemStyle>
                                            <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                                            </ButtonStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="70px">
                                        <dx:ASPxLabel ID="ASPxLabel19" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Part Name">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 3px 0px 0px 0px;" width="150px">
                                        <dx:ASPxTextBox ID="txtpartnamepopup" runat="server" BackColor="WhiteSmoke" 
                                            ClientInstanceName="txtpartnamepopup" EnableTheming="True" 
                                            Font-Names="Segoe UI" Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                                            ReadOnly="True" TabIndex="20" Theme="Office2010Black" Width="150px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="70px">
                                        <dx:ASPxLabel ID="ASPxLabel23" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Machine No">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 3px 0px 0px 0px;" width="150px">
                                        <dx:ASPxComboBox ID="cbomachinenopopup" runat="server" 
                                            ClientInstanceName="cbomachinenopopup" DisplayFormatString="{0}" 
                                            EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="25px" IncrementalFilteringMode="Contains" TabIndex="21" 
                                            TextField="MachineNo" TextFormatString="{0}-{1}-{2}-{3}-{4}" Theme="Office2010Black" 
                                            ValueField="MachineNo" Width="100px">
                                            <ClientSideEvents SelectedIndexChanged="SelectMachineNoPopup" />
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Machine No" FieldName="MachineNo" Width="70px" />
                                                <dx:ListBoxColumn Caption="Line ID" FieldName="LineID" Width="70px" />
                                                <dx:ListBoxColumn Caption="Sub Line ID" FieldName="SubLineID" Width="80px" />
                                                <dx:ListBoxColumn Caption="Process ID" FieldName="ProcessID" Width="80px" />
                                                <dx:ListBoxColumn Caption="Process Name" FieldName="ProcessName" Width="80px" />
                                            </Columns>
                                            <ItemStyle Height="10px">
                                            <Paddings Padding="4px" />
                                            </ItemStyle>
                                            <ButtonStyle Width="5px">
                                                <Paddings Padding="4px" />
                                            </ButtonStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="70px">
                                        <dx:ASPxLabel ID="ASPxLabel24" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Line No.">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 3px 0px 0px 0px;" width="150px">
                                        <dx:ASPxTextBox ID="txtlineidpopup" runat="server" BackColor="WhiteSmoke" 
                                            ClientInstanceName="txtlineidpopup" EnableTheming="True" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Height="25px" HorizontalAlign="Left" ReadOnly="True" 
                                            TabIndex="4" Theme="Office2010Black" Width="100px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="70px">
                                        <dx:ASPxLabel ID="ASPxLabel25" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Sub Line No.">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 3px 0px 0px 0px;" width="150px">
                                        <dx:ASPxTextBox ID="txtsublineidpopup" runat="server" BackColor="WhiteSmoke" 
                                            ClientInstanceName="txtsublineidpopup" EnableTheming="True" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Height="25px" HorizontalAlign="Left" ReadOnly="True" 
                                            TabIndex="5" Theme="Office2010Black" Width="100px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style3" style="padding: 5px 0px 0px 0px; " width="10px">
                                    </td>
                                    <td class="style3" style="padding: 5px 0px 0px 0px; " width="10px">
                                    </td>
                                    <td class="style3" style="padding: 5px 0px 0px 0px; " width="70px">
                                        <dx:ASPxLabel ID="ASPxLabel20" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Rev No.">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="style3" style="padding: 5px 0px 0px 0px; " width="10px">
                                    </td>
                                    <td class="style3" style="padding: 3px 0px 0px 0px;" width="150px">
                                        <dx:ASPxComboBox ID="cborevnopopup" runat="server" 
                                            ClientInstanceName="cborevnopopup" DisplayFormatString="{0}" 
                                            EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="25px" IncrementalFilteringMode="Contains" TabIndex="22" 
                                            TextField="RevNo" TextFormatString="{0}-{1}-{2}" Theme="Office2010Black" 
                                            ValueField="RevNo" Width="100px">
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Rev No" FieldName="RevNo" Width="70px" />
                                                <dx:ListBoxColumn Caption="Rev Date" FieldName="RevDate" Width="70px" />
                                                <dx:ListBoxColumn Caption="Rev History" FieldName="RevHistory" Width="200px" />
                                            </Columns>
                                            <ItemStyle Height="10px">
                                            <Paddings Padding="4px" />
<Paddings Padding="4px"></Paddings>
                                            </ItemStyle>
                                            <ButtonStyle Width="5px">
                                                <Paddings Padding="4px" />
<Paddings Padding="4px"></Paddings>
                                            </ButtonStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="style3" style="padding: 5px 0px 0px 0px; " width="10px">
                                    </td>
                                    <td class="style3" style="padding: 5px 0px 0px 0px; " width="10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="70px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="150px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="70px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="150px">
                                        <dx:ASPxButton ID="btnCopyPopUp" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="btnCopyPopUp" Height="25px" 
                                            style="float: left; margin-right: 8px" Text="Copy" Width="70px" 
                                            TabIndex="23">
                                            <ClientSideEvents Click="ClickCopyPopUp" />
                                        </dx:ASPxButton>
                                        <dx:ASPxButton ID="btCancel0" runat="server" AutoPostBack="False" Height="25px" 
                                            style="float: left; margin-right: 8px" Text="Cancel" Width="70px" 
                                            TabIndex="24">
                                            <ClientSideEvents Click="function(s, e) { pcLogin.Hide(); }" />
<ClientSideEvents Click="function(s, e) { pcLogin.Hide(); }"></ClientSideEvents>
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="70px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="150px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="70px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="150px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="10px">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
<ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup(&#39;entryGroup&#39;); tbLogin.Focus(); }"></ClientSideEvents>

        <ContentStyle>
            <Paddings PaddingBottom="5px" />
<Paddings PaddingBottom="5px"></Paddings>
        </ContentStyle>
    </dx:ASPxPopupControl>    
</div>


<asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT DISTINCT(RTRIM(Item)) as Item FROM TCCSItem">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT DISTINCT(RTRIM(Tools)) as Tools FROM TCCSItem">
    </asp:SqlDataSource>
</asp:Content>
