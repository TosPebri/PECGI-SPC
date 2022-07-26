<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="QCSMaster.aspx.vb" Inherits="PECGI_SPC.QCSMaster" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>


<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>


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
//=============================================================================================================================================================================

if (s.cp_delsafetysymbol1 == 1){
    ASPxImage1.SetImageUrl(s.cp_safetysymbol1);
    s.cp_delsafetysymbol1 = 0;

    toastr.success('Delete safety symbol 1 success!', 'Success');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;

    e.processOnServer = false;
    return;
}

if (s.cp_delsafetysymbol2 == 1){
    ASPxImage2.SetImageUrl(s.cp_safetysymbol2);
    s.cp_delsafetysymbol2 = 0;

    toastr.success('Delete safety symbol 2 success!', 'Success');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;

    e.processOnServer = false;
    return;
}

if (s.cp_delsafetysymbol3 == 1){
    ASPxImage3.SetImageUrl(s.cp_safetysymbol3);
    s.cp_delsafetysymbol3 = 0;

    toastr.success('Delete safety symbol 3 success!', 'Success');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;

    e.processOnServer = false;
    return;
}

if (s.cp_delsafetysymbol4 == 1){
    ASPxImage4.SetImageUrl(s.cp_safetysymbol4);
    s.cp_delsafetysymbol4 = 0;

    toastr.success('Delete safety symbol 4 success!', 'Success');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;

    e.processOnServer = false;
    return;
}

if (s.cp_delsafetysymbol5 == 1){
    ASPxImage5.SetImageUrl(s.cp_safetysymbol5);
    s.cp_delsafetysymbol5 = 0;

    toastr.success('Delete safety symbol 5 success!', 'Success');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;

    e.processOnServer = false;
    return;
}

if (s.cp_delsafetysymbol6 == 1){
    ASPxImage6.SetImageUrl(s.cp_safetysymbol6);
    s.cp_delsafetysymbol6 = 0;

    toastr.success('Delete safety symbol 6 success!', 'Success');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;

    e.processOnServer = false;
    return;
}

if(s.cp_newstatusunapproved == 1){
    cborevno.SetText('');
    cborevno.SetEnabled(true);
    dtrevdate.SetText('');
    dtrevdate.SetEnabled(true);

    toastr.warning('Line ID : ' +s.cp_lineid + ', Part ID : ' + s.cp_partid + ', Rev No : ' + s.cp_revno + ', has not been approved', 'Warning');
    toastr.options.closeButton = false;
    toastr.options.debug = false;
    toastr.options.newestOnTop = false;
    toastr.options.progressBar = false;
    toastr.options.preventDuplicates = true;
    toastr.options.onclick = null;

    e.processOnServer = false;
    s.cp_newstatusunapproved = null;
    s.cp_newstatusunapproved = 0;
    return;

    
}

if(s.cp_newstatusapproved == 1){
          cborevno.SetValue('--New--');
          cborevno.SetEnabled(false);
          var today = new Date();
          dtrevdate.SetDate(today);
          txtrevisionhistory.SetText('');
          txtpreparedby.SetText('');
//          txtsafety.SetText('');
          lblActive.SetText('Inactive');
          lblActive.GetMainElement().style.color = 'Red';
          GridMenu.PerformCallback('ClickNew|');

          btnCopy.SetEnabled(true);
          btnInput.SetEnabled(false);
          btnExcel.SetEnabled(false);
          btnNew.SetEnabled(false);
          btnDelete.SetEnabled(false);
          btnCancel.SetEnabled(true);
          btnSave.SetEnabled(false);
          btnApprove.SetEnabled(false);
          s.cp_newstatusapproved = 0;
}

if(s.cp_clearheader == 1){
  cbopartid.SetText('');
  txtpartname.SetText('');
  cborevno.SetValue('');
  dtrevdate.SetText('');
  txtrevisionhistory.SetText('');
  txtpreparedby.SetText('');
  lblActive.SetText('Inactive');
  lblActive.GetMainElement().style.color = 'Red';
  s.cp_clearheader = 0;
}

if(s.cp_clearheaderdelete == 1){
  cbolineid.SetText('');
  cbopartid.SetText('');
  txtpartname.SetText('');
  cborevno.SetValue('');
  dtrevdate.SetText('');
  txtrevisionhistory.SetText('');
  txtpreparedby.SetText('');
//  txtsafety.SetText('');
  lblActive.SetText('Inactive');
  lblActive.GetMainElement().style.color = 'Red';
  btnApprove.SetEnabled(false);
  s.cp_clearheaderdelete = 0;
}

if(s.cp_clearapprovalinformation == 1){
  lbllineleaderpic.SetText('UNAPPROVED');
  lbllineforemanpic.SetText('UNAPPROVED');
  lblqeleaderpic.SetText('UNAPPROVED');
  lbllineleaderdate.SetText('UNAPPROVED');
  lbllineforemandate.SetText('UNAPPROVED');
  lblqeleaderdate.SetText('UNAPPROVED');
  
  lbllineleaderpic.GetMainElement().style.color = 'Red';
  lbllineforemanpic.GetMainElement().style.color = 'Red';
  lblqeleaderpic.GetMainElement().style.color = 'Red';
  lbllineleaderdate.GetMainElement().style.color = 'Red';
  lbllineforemandate.GetMainElement().style.color = 'Red';
  lblqeleaderdate.GetMainElement().style.color = 'Red';
  s.cp_clearapprovalinformation = 0;
}

if(s.cp_clearafterselectrevno == 1){
  dtrevdate.SetText('');
  txtrevisionhistory.SetText('');
  txtpreparedby.SetText('');
  lblActive.SetText('Inactive');
  lblActive.GetMainElement().style.color = 'Red';
  s.cp_clearafterselectrevno = 0;
}

if(s.cp_fillheader == 1){
  dtrevdate.SetText(s.cp_revdate);
  txtrevisionhistory.SetText(s.cp_revhistory);
  txtpreparedby.SetText(s.cp_preparedby);
  ASPxImage1.SetImageUrl(s.cp_safetysymbol1);
  ASPxImage2.SetImageUrl(s.cp_safetysymbol2);
  ASPxImage3.SetImageUrl(s.cp_safetysymbol3);
  ASPxImage4.SetImageUrl(s.cp_safetysymbol4);
  ASPxImage5.SetImageUrl(s.cp_safetysymbol5);
  ASPxImage6.SetImageUrl(s.cp_safetysymbol6);
//  txtsafety.SetText(s.cp_safetysymbol);
  cboattachment.SetValue(s.cp_attachemnt);
  if (s.cp_activestatus == 0){
    lblActive.SetText('Inactive');
    lblActive.GetMainElement().style.color = 'Red';
  }
  else{
    lblActive.SetText('Active');
    lblActive.GetMainElement().style.color = 'Blue';
  }
  s.cp_fillheader = 0;
}

if(s.cp_fillapprovalinformation == 1){
  if (s.cp_approvalstatus1 == 0){
    lbllineleaderpic.SetText(s.cp_approvalpic1);
    lbllineleaderdate.SetText(s.cp_approvaldate1);
    lbllineleaderpic.GetMainElement().style.color = 'Red';
    lbllineleaderdate.GetMainElement().style.color = 'Red';
    btnDeleteSymbol1.SetVisible(true);
    btnDeleteSymbol2.SetVisible(true);
    btnDeleteSymbol3.SetVisible(true);
    btnDeleteSymbol4.SetVisible(true);
    btnDeleteSymbol5.SetVisible(true);
    btnDeleteSymbol6.SetVisible(true);
    btnUpload.SetEnabled(true);
  }
  else{
    lbllineleaderpic.SetText(s.cp_approvalpic1);
    lbllineleaderdate.SetText(s.cp_approvaldate1);
    lbllineleaderpic.GetMainElement().style.color = 'Black';
    lbllineleaderdate.GetMainElement().style.color = 'Black';
    btnDeleteSymbol1.SetVisible(false);
    btnDeleteSymbol2.SetVisible(false);
    btnDeleteSymbol3.SetVisible(false);
    btnDeleteSymbol4.SetVisible(false);
    btnDeleteSymbol5.SetVisible(false);
    btnDeleteSymbol6.SetVisible(false);
    btnUpload.SetEnabled(false);
  }

  if (s.cp_approvalstatus2 == 0){
    lbllineforemanpic.SetText(s.cp_approvalpic2);
    lbllineforemandate.SetText(s.cp_approvaldate2);
    lbllineforemanpic.GetMainElement().style.color = 'Red';
    lbllineforemandate.GetMainElement().style.color = 'Red';
  }
  else{
    lbllineforemanpic.SetText(s.cp_approvalpic2);
    lbllineforemandate.SetText(s.cp_approvaldate2);
    lbllineforemanpic.GetMainElement().style.color = 'Black';
    lbllineforemandate.GetMainElement().style.color = 'Black';
  }

  if(s.cp_approvalstatus3 == 0){
    lblqeleaderpic.SetText(s.cp_approvalpic3);
    lblqeleaderdate.SetText(s.cp_approvaldate3);
    lblqeleaderpic.GetMainElement().style.color = 'Red';
    lblqeleaderdate.GetMainElement().style.color = 'Red';
  }
  else{
    lblqeleaderpic.SetText(s.cp_approvalpic3);
    lblqeleaderdate.SetText(s.cp_approvaldate3);
    lblqeleaderpic.GetMainElement().style.color = 'Black';
    lblqeleaderdate.GetMainElement().style.color = 'Black';
  }

  if(s.cp_activestatus == 0){
    lblActive.SetText('Inactive');
    lblActive.GetMainElement().style.color = 'Red';
  }
  else{
    lblActive.SetText('Active');
    lblActive.GetMainElement().style.color = 'Blue';
  }
  
  s.cp_fillapprovalinformation = 0;
}

if (s.cp_btnapprove == 0){
    btnApprove.SetText(s.cp_txtapprove);
    btnApprove.SetEnabled(true);
    s.cp_btnapprove = null;
}
if (s.cp_btnapprove == 1){
    btnApprove.SetText(s.cp_txtapprove);
    btnApprove.SetEnabled(false);
    s.cp_btnapprove = null;
}

if (s.cp_btnapprovedtrue == 1){
    btnCopy.SetEnabled(s.cp_btncopy);
    btnInput.SetEnabled(s.cp_btninput);
    btnNew.SetEnabled(s.cp_btnnew);
    btnDelete.SetEnabled(s.cp_btndelete);
    btnCancel.SetEnabled(s.cp_btncancel);
    btnSave.SetEnabled(s.cp_btnsave);
    btnExcel.SetEnabled(s.cp_btnexcel);
    s.cp_btnapprovedtrue = null;
}

if (s.cp_btnapprovedfalse == 1){
    btnCopy.SetEnabled(s.cp_btncopy);
    btnInput.SetEnabled(s.cp_btninput);
    btnNew.SetEnabled(s.cp_btnnew);
    btnDelete.SetEnabled(s.cp_btndelete);
    btnCancel.SetEnabled(s.cp_btncancel);
    btnSave.SetEnabled(s.cp_btnsave);
    btnExcel.SetEnabled(s.cp_btnexcel);
    s.cp_btnapprovedfalse = null;
}

if (s.cp_aftersave == 1){
    cborevno.PerformCallback('|' + cbopartid.GetValue() + '|' + cbolineid.GetValue());
    cborevno.SetText(s.cp_cborevno);
    cborevno.SetValue(s.cp_cborevno);
    cborevno.SetEnabled(true);
    s.cp_aftersave = null;
    btnNew.SetEnabled(true);
    btnCancel.SetEnabled(false);
    btnCopy.SetEnabled(true);
    btnExcel.SetEnabled(true);
    btnDelete.SetEnabled(true);
    btnSave.SetEnabled(true);
    ClickNewStatus = true;
    GridMenu.PerformCallback('GridGet|'+ cbopartid.GetValue() + '|' + cborevno.GetValue());
    s.cp_aftersave = null;
}

if (s.cp_aftersavegridget == 1){
    GridMenu.PerformCallback('GridGet|'+ cbopartid.GetValue() + '|' + cborevno.GetValue());
    s.cp_aftersavegridget = null;
}

if(s.cp_valuetype == 1){
//    alert(s.cp_valuetypevalue);
    if (s.cp_valuetypevalue == 'T'){
        SetTextRangeVisibility(true);
        SetNumRangeStartVisibility(false);
        SetNumRangeEndVisibility(false);
        }
    if (s.cp_valuetypevalue == 'N'){
        SetTextRangeVisibility(false);
        SetNumRangeStartVisibility(true);
        SetNumRangeEndVisibility(true);
        }
    s.cp_valuetype=null;
}

if(s.cp_valuetypeedit == 1){
    if (s.cp_valuetypevalue == 'T'){
        SetTextRangeVisibility(true);
        SetNumRangeStartVisibility(false);
        SetNumRangeEndVisibility(false);

        }
    if (s.cp_valuetypevalue == 'N'){
        SetTextRangeVisibility(false);
        SetNumRangeStartVisibility(true);
        SetNumRangeEndVisibility(true);
        }
    s.cp_valuetypeedit=null;
}

if (s.cp_message == 1 ) {
        toastr.warning('Please fill in all required fields!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        s.cp_message = null;
        return;
        } 
if (s.cp_messagelessthan == 1 ) {
        toastr.warning('End Range cannot less than Start Range!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        s.cp_messagelessthan = null;
        return;
        }

//if (s.cp_viewqcs == 1){        
//    cbolineid.SetValue(s.cp_lineid);
//    cbopartid.SetValue(s.cp_partid);
//    txtpartname.SetValue(s.cp_partname);
//    cborevno.SetValue(s.);
//}
}//End Callback
//            if(s.cp_cek == 1){
//                dtrevdate.SetText(s.cp_revdate);
//                txtrevisionhistory.SetText(s.cp_revhistory);
//                txtpreparedby.SetText(s.cp_preparedby);
//                txtpreparedby.SetText(s.cp_preparedby);
////                txtapprovedby.SetText(s.cp_approvedby);

//                if (s.cp_Status == 1){
//                    lblActive.SetText('Active');
//                    lblActive.GetMainElement().style.color = 'Blue';
//                }
//                else{
//                    lblActive.SetText('Inactive');
//                    lblActive.GetMainElement().style.color = 'Red';
//                }


//                if (s.cp_leaderstatus == 0){
//                    lbllineleaderpic.SetText('UNAPPROVED');
//                    lbllineleaderdate.SetText('UNAPPROVED');
//                    lbllineleaderpic.GetMainElement().style.color = 'Red';
//                    lbllineleaderdate.GetMainElement().style.color = 'Red';
//                }
//                else if (s.cp_leaderstatus == 1){
//                    lbllineleaderpic.SetText(s.cp_leaderpic);
//                    lbllineleaderdate.SetText(s.cp_leaderdate);
//                    lbllineleaderpic.GetMainElement().style.color = 'Black';
//                    lbllineleaderdate.GetMainElement().style.color = 'Black';
//                }

//                if (s.cp_foremanstatus == 0){
//                    lbllineforemanpic.SetText('UNAPPROVED');
//                    lbllineforemandate.SetText('UNAPPROVED');
//                    lbllineforemanpic.GetMainElement().style.color = 'Red';
//                    lbllineforemandate.GetMainElement().style.color = 'Red';
//                }
//                else if (s.cp_foremanstatus == 1){
//                    lbllineforemanpic.SetText(s.cp_foremanpic);
//                    lbllineforemandate.SetText(s.cp_foremandate);
//                    lbllineforemanpic.GetMainElement().style.color = 'Black';
//                    lbllineforemandate.GetMainElement().style.color = 'Black';
//                }

//                if (s.cp_qestatus == 0){
//                    lblqeleaderpic.SetText('UNAPPROVED');
//                    lblqeleaderpic.GetMainElement().style.color = 'Red';
//                    lblqeleaderdate.SetText('UNAPPROVED');
//                    lblqeleaderdate.GetMainElement().style.color = 'Red';
//                }
//                else if (s.cp_qestatus == 1){
//                    lblqeleaderpic.SetText(s.cp_qepic);
//                    lblqeleaderpic.GetMainElement().style.color = 'Black';
//                    lblqeleaderdate.SetText(s.cp_qedate);
//                    lblqeleaderdate.GetMainElement().style.color = 'Black';
//                }

//                s.cp_cek = 0;
////                //btnapprove1
////                if (s.cp_btnapprove1 == 1){
////                    btnApprove.SetText(s.cp_btntextapprove1);
////                    btnApprove.SetEnabled(true);
////                }
////                if (s.cp_btnapprove1 == 2){
////                    btnApprove.SetText(s.cp_btntextapprove1);
////                    btnApprove.SetEnabled(true);
////                }
////                if (s.cp_btnapprove1 == 3){
////                    btnApprove.SetText(s.cp_btntextapprove1);
////                    btnApprove.SetEnabled(false);
////                }
//            }

//            //btnapprove1
//                if (s.cp_btnapprove1 == 1){
//                    btnApprove.SetText(s.cp_btntextapprove1);
//                    btnApprove.SetEnabled(true);
//                }
//                if (s.cp_btnapprove1 == 2){
//                    btnApprove.SetText(s.cp_btntextapprove1);
//                    btnApprove.SetEnabled(true);
//                }
//                if (s.cp_btnapprove1 == 3){
//                    btnApprove.SetText(s.cp_btntextapprove1);
//                    btnApprove.SetEnabled(false);
//                }

//                if (s.cp_approveline == 1){
//                    btnSave.SetEnabled(false);
//                    btnDelete.SetEnabled(false);
//                    BtnCopy.SetEnabled(false);
//                }
//                if (s.cp_approveline == 0){
//                    btnSave.SetEnabled(true);
//                    btnDelete.SetEnabled(true);
//                    BtnCopy.SetEnabled(true);
//                }

//            if(s.cp_statusapproval == 1){
//                if (s.cp_leaderstatus == 0){
//                    lbllineleaderpic.SetText('UNAPPROVED');
//                    lbllineleaderdate.SetText('UNAPPROVED');
//                    lbllineleaderpic.GetMainElement().style.color = 'Red';
//                    lbllineleaderdate.GetMainElement().style.color = 'Red';
//                }
//                else if (s.cp_leaderstatus == 1){
//                    lbllineleaderpic.SetText(s.cp_leaderpic);
//                    lbllineleaderdate.SetText(s.cp_leaderdate);
//                    lbllineleaderpic.GetMainElement().style.color = 'Black';
//                    lbllineleaderdate.GetMainElement().style.color = 'Black';
//                }

//                if (s.cp_foremanstatus == 0){
//                    lbllineforemanpic.SetText('UNAPPROVED');
//                    lbllineforemandate.SetText('UNAPPROVED');
//                    lbllineforemanpic.GetMainElement().style.color = 'Red';
//                    lbllineforemandate.GetMainElement().style.color = 'Red';
//                }
//                else if (s.cp_foremanstatus == 1){
//                    lbllineforemanpic.SetText(s.cp_foremanpic);
//                    lbllineforemandate.SetText(s.cp_foremandate);
//                    lbllineforemanpic.GetMainElement().style.color = 'Black';
//                    lbllineforemandate.GetMainElement().style.color = 'Black';
//                }

//                if (s.cp_qestatus == 0){
//                    lblqeleaderpic.SetText('UNAPPROVED');
//                    lblqeleaderpic.GetMainElement().style.color = 'Red';
//                    lblqeleaderdate.SetText('UNAPPROVED');
//                    lblqeleaderdate.GetMainElement().style.color = 'Red';
//                }
//                else if (s.cp_qestatus == 1){
//                    lblqeleaderpic.SetText(s.cp_qepic);
//                    lblqeleaderpic.GetMainElement().style.color = 'Black';
//                    lblqeleaderdate.SetText(s.cp_qedate);
//                    lblqeleaderdate.GetMainElement().style.color = 'Black';
//                }
//            }

//            if(s.cp_statusapproval == 2){
//                if (s.cp_leaderstatus == 0){
//                    lbllineleaderpic.SetText('UNAPPROVED');
//                    lbllineleaderdate.SetText('UNAPPROVED');
//                    lbllineleaderpic.GetMainElement().style.color = 'Red';
//                    lbllineleaderdate.GetMainElement().style.color = 'Red';
//                }
//                else if (s.cp_leaderstatus == 1){
//                    lbllineleaderpic.SetText(s.cp_leaderpic);
//                    lbllineleaderdate.SetText(s.cp_leaderdate);
//                    lbllineleaderpic.GetMainElement().style.color = 'Black';
//                    lbllineleaderdate.GetMainElement().style.color = 'Black';
//                }

//                if (s.cp_foremanstatus == 0){
//                    lbllineforemanpic.SetText('UNAPPROVED');
//                    lbllineforemandate.SetText('UNAPPROVED');
//                    lbllineforemanpic.GetMainElement().style.color = 'Red';
//                    lbllineforemandate.GetMainElement().style.color = 'Red';
//                }
//                else if (s.cp_foremanstatus == 1){
//                    lbllineforemanpic.SetText(s.cp_foremanpic);
//                    lbllineforemandate.SetText(s.cp_foremandate);
//                    lbllineforemanpic.GetMainElement().style.color = 'Black';
//                    lbllineforemandate.GetMainElement().style.color = 'Black';
//                }

//                if (s.cp_qestatus == 0){
//                    lblqeleaderpic.SetText('UNAPPROVED');
//                    lblqeleaderpic.GetMainElement().style.color = 'Red';
//                    lblqeleaderdate.SetText('UNAPPROVED');
//                    lblqeleaderdate.GetMainElement().style.color = 'Red';
//                }
//                else if (s.cp_qestatus == 1){
//                    lblqeleaderpic.SetText(s.cp_qepic);
//                    lblqeleaderpic.GetMainElement().style.color = 'Black';
//                    lblqeleaderdate.SetText(s.cp_qedate);
//                    lblqeleaderdate.GetMainElement().style.color = 'Black';
//                }

////                txtapprovedby.SetText(s.cp_approvedby);

//                if (s.cp_Status == 1){
//                    lblActive.SetText('Active');
//                    lblActive.GetMainElement().style.color = 'Blue';
//                }
//                else{
//                    lblActive.SetText('Inactive');
//                    lblActive.GetMainElement().style.color = 'Red';
//                }
//            }


//            if(s.cp_cleargrid == 1){
//                    if(btnNew.GetEnabled() == false){
//                       var today = new Date();
//                        dtrevdate.SetDate(today);
//                        dtrevdate.SetEnabled(false);
//                       }
//                    else{
//                        dtrevdate.SetText(null);
//                    }
////                    dtrevdate.SetText(null);
//                    txtrevisionhistory.SetText(null);
//                    txtpreparedby.SetText(null);
////                    txtapprovedby.SetText(null);
//                    lblActive.SetText('Inactive');
//                    lbllineleaderpic.SetText('UNAPPROVED');
//                    lbllineforemanpic.SetText('UNAPPROVED');
//                    lblqeleaderpic.SetText('UNAPPROVED');
//                    lbllineleaderdate.SetText('UNAPPROVED');
//                    lbllineforemandate.SetText('UNAPPROVED');
//                    lblqeleaderdate.SetText('UNAPPROVED');
//                    lblActive.GetMainElement().style.color = 'Red';
//                    lbllineleaderpic.GetMainElement().style.color = 'Red';
//                    lbllineforemanpic.GetMainElement().style.color = 'Red';
//                    lblqeleaderpic.GetMainElement().style.color = 'Red';
//                    lbllineleaderdate.GetMainElement().style.color = 'Red';
//                    lbllineforemandate.GetMainElement().style.color = 'Red';
//                    lblqeleaderdate.GetMainElement().style.color = 'Red';
//                    s.cp_cleargrid = 0;
//            }

//            if(s.cp_cleargridpart == 1){
//                    dtrevdate.SetText(null);
//                    txtrevisionhistory.SetText(null);
//                    txtpreparedby.SetText(null);
////                    txtapprovedby.SetText(null);
//                    lblActive.SetText('Inactive');
//                    lbllineleaderpic.SetText('UNAPPROVED');
//                    lbllineforemanpic.SetText('UNAPPROVED');
//                    lblqeleaderpic.SetText('UNAPPROVED');
//                    lbllineleaderdate.SetText('UNAPPROVED');
//                    lbllineforemandate.SetText('UNAPPROVED');
//                    lblqeleaderdate.SetText('UNAPPROVED');
//                    lblActive.GetMainElement().style.color = 'Red';
//                    lbllineleaderpic.GetMainElement().style.color = 'Red';
//                    lbllineforemanpic.GetMainElement().style.color = 'Red';
//                    lblqeleaderpic.GetMainElement().style.color = 'Red';
//                    lbllineleaderdate.GetMainElement().style.color = 'Red';
//                    lbllineforemandate.GetMainElement().style.color = 'Red';
//                    lblqeleaderdate.GetMainElement().style.color = 'Red';
//                    s.cp_cleargridpart = 0;
//            }

//            if(s.cp_clearall == 1){
//                    cbopartid.SetText('');
//                    cbolineid.SetText('');
//                    txtpartname.SetText('');
//                    cborevno.SetText('');
//                    dtrevdate.SetText(null);
//                    txtrevisionhistory.SetText(null);
//                    txtpreparedby.SetText(null);
////                    txtapprovedby.SetText(null);
//                    lblActive.SetText('Inactive');
//                    lbllineleaderpic.SetText('UNAPPROVED');
//                    lbllineforemanpic.SetText('UNAPPROVED');
//                    lblqeleaderpic.SetText('UNAPPROVED');
//                    lbllineleaderdate.SetText('UNAPPROVED');
//                    lbllineforemandate.SetText('UNAPPROVED');
//                    lblqeleaderdate.SetText('UNAPPROVED');
//                    lblActive.GetMainElement().style.color = 'Red';
//                    lbllineleaderpic.GetMainElement().style.color = 'Red';
//                    lbllineforemanpic.GetMainElement().style.color = 'Red';
//                    lblqeleaderpic.GetMainElement().style.color = 'Red';
//                    lbllineleaderdate.GetMainElement().style.color = 'Red';
//                    lbllineforemandate.GetMainElement().style.color = 'Red';
//                    lblqeleaderdate.GetMainElement().style.color = 'Red';
//                    s.cp_clearall = 0;
//            }

//            if(s.cp_loadsave == 1){
//                cborevno.SetValue(s.cp_revno);
//                cborevno.SetEnabled(true);
//                btnExcel.SetEnabled(true);
//                btnExcel.SetEnabled(true);
//                btnNew.SetEnabled(true);
//                btnDelete.SetEnabled(true);
//                btnCancle.SetEnabled(false);
//                btnSave.SetEnabled(true);
//                s.cp_loadsave = 0;
//            }

//            if(s.cp_edit == 1){
//                if(s.cp_value == 'N'){
//                    SetTextRangeVisibility(false);
//                    SetNumRangeStartVisibility(true);
//                    SetNumRangeEndVisibility(true);
//                }
//                if(s.cp_value == 'T'){
//                    SetTextRangeVisibility(true);
//                    SetNumRangeStartVisibility(false);
//                    SetNumRangeEndVisibility(false);
//                }
//                 
//                s.cp_edit = 0;
//            }

//}//End Callback
  

//        window.onbeforeunload = confirmExit;
//  function confirmExit()
//  {
//    return "Do you want to leave this page without saving?";
//  }

//function SelectLineID(s, e) {
//    if(btnNew.GetEnabled() == true){
//        cbopartid.PerformCallback('load|' + cbolineid.GetValue());
//        cbopartid.SetText('');
//        txtpartname.SetText('');
//        cborevno.SetValue('');
//        btnDelete.SetEnabled(false);
//        btnSave.SetEnabled(false);
//        btnExcel.SetEnabled(false);
//        btnCopy.SetEnabled(false);
//        GridMenu.PerformCallback('ClearGrid|');
//      }
//    if(btnNew.GetEnabled() == false){
//        cborevno.SetText('--New--');
//        cbopartid.PerformCallback('load|' + cbolineid.GetValue());
//        cbopartid.SetText('');
//        txtpartname.SetText('');
//        GridMenu.PerformCallback('ClearGrid|');
//      }

////    cbopartid.PerformCallback('load|' + cbolineid.GetValue());
////    cbopartid.SetText('');
////    txtpartname.SetText('');
////    cborevno.SetValue('');
////    GridMenu.PerformCallback('ClearGrid|');
////       GridMenu.PerformCallback('GetGrid|'+ cbopartid.GetValue() + '|' + cborevno.GetValue());
//    }

function SelectLineIDPopUp(s, e) {
//    cbopartidpopup.PerformCallback('load|' + cbolineidpopup.GetValue());
//    cbopartidpopup.SetText('');
//    txtpartnamepopup.SetText('');
    cborevnopopup.PerformCallback('|' + cbopartidpopup.GetValue() + '|' + cbolineidpopup.GetValue());
    cborevnopopup.SetValue('');
    }

//function SelectPartID(s, e) {
//    if(btnNew.GetEnabled() == true){
//        txtpartname.SetText(cbopartid.GetSelectedItem().GetColumnText(1));
//        cborevno.PerformCallback('|' + cbopartid.GetValue());
//        GridMenu.PerformCallback('ClearGridPart|');
//        btnDelete.SetEnabled(false);
//        btnSave.SetEnabled(false);
//        btnExcel.SetEnabled(false);
//        btnCopy.SetEnabled(false);
//      }
//    if(btnNew.GetEnabled() == false){
//        cborevno.SetText('--New--');
//        txtpartname.SetText(cbopartid.GetSelectedItem().GetColumnText(1));
//      }
//    }

function SelectPartIDPopUp(s, e) {
    txtpartnamepopup.SetText(cbopartidpopup.GetSelectedItem().GetColumnText(1));
    cbolineidpopup.PerformCallback('|' + cbopartidpopup.GetValue());
//    cborevnopopup.PerformCallback('|' + cbopartidpopup.GetValue());
    }

function CallFunction(s, e) {
    alert('test');
    }

//function SelectRevNo(s, e) {
//    GridMenu.PerformCallback('GetGrid|'+ cbopartid.GetValue() + '|' + cborevno.GetValue() + '|' + 'RevNo');
//    btnDelete.SetEnabled(true);
//    btnSave.SetEnabled(true);
//    btnCopy.SetEnabled(true);
//    btnExcel.SetEnabled(true);
////    GridMenu.PerformCallback('CekStatusApprove|');
//    }

//function ClickRefresh(s, e) {
//    if (cbolineid.GetText() == '' ) {
//        toastr.warning('Please select Line No!', 'Warning');
//        toastr.options.closeButton = false;
//        toastr.options.debug = false;
//        toastr.options.newestOnTop = false;
//        toastr.options.progressBar = false;
//        toastr.options.preventDuplicates = true;
//        toastr.options.onclick = null;

//        e.processOnServer = false;
//        return;
//        } 

//    if (cbopartid.GetText() == '' ) {
//        toastr.warning('Please select Part No!', 'Warning');
//        toastr.options.closeButton = false;
//        toastr.options.debug = false;
//        toastr.options.newestOnTop = false;
//        toastr.options.progressBar = false;
//        toastr.options.preventDuplicates = true;
//        toastr.options.onclick = null;

//        e.processOnServer = false;
//        return;
//        } 

//    if (cborevno.GetText() == '' ) {
//        toastr.warning('Please select Rev No ID!', 'Warning');
//        toastr.options.closeButton = false;
//        toastr.options.debug = false;
//        toastr.options.newestOnTop = false;
//        toastr.options.progressBar = false;
//        toastr.options.preventDuplicates = true;
//        toastr.options.onclick = null;

//        e.processOnServer = false;
//        return;

//        }
//    
//    GridMenu.PerformCallback('RefreshClick|'+ cbopartid.GetValue() + '|' + cborevno.GetValue());
//    }

//function ClickNew(s, e) {
////        var count = cborevno.GetItemCount();  
//        cborevno.SetText('--New--');
//        cborevno.SetEnabled(false);
//        cbolineid.SetText('');
//        cbopartid.SetText('');
//        txtpartname.SetText('');
//        GridMenu.PerformCallback('GetGrid|'+ '' + '|' + '') //+ '|' + btnNew.GetText());
//        btnNew.SetEnabled(false);
////        btnDelete.SetEnable(false);
//        btnCancle.SetEnabled(true);
//        btnCopy.SetEnabled(true);
//        btnDelete.SetEnabled(false);
//        btnSave.SetEnabled(false);
//        btnExcel.SetEnabled(false);
////        var today = new Date();
////        dtrevdate.SetDate(today);
//        dtrevdate.SetEnabled(false);
////        GridMenu.PerformCallback('ClearGrid|');
////        GridMenu.PerformCallback('GetGrid|' + cbopartid.GetValue() + '|' + cborevno.GetValue());
////        GridMenu.PerformCallback('ClearGrid|');
//    } 

//function ClickCancle(s, e) {  
//        cborevno.SetText('');
//        cborevno.SetEnabled(true);
//        cbolineid.SetText('');
//        cbopartid.SetText('');
//        txtpartname.SetText('');
//        dtrevdate.SetText('');
//        txtrevisionhistory.SetText('');
//        txtpreparedby.SetText('');
//        btnNew.SetEnabled(true);
//        btnCancle.SetEnabled(false);
//        dtrevdate.SetEnabled(true);
//    } 

function ClickDelete(s, e) {  
        var r = confirm("Are you sure want to delete?");
        if (r == true) {
            GridMenu.PerformCallback('Delete|');
            GridMenu.PerformCallback('ClearAll|');
            btnDelete.SetEnabled(false);
            btnSave.SetEnabled(false);
            btnExcel.SetEnabled(false);
            btnCopy.SetEnabled(false);
        } 
        
//        cbopartid.SetText('');
//        cbolineid.SetText('');
//        txtpartname.SetText('');
//        cborevno.SetText('');
    } 

function SelectValueType(s, e) {
//    if (ValueType.GetValue() == "N")
//    {
//        alert("N");
//        NumRangeStart.SetEnabled(false);
//        NumRangeEnd.SetEnabled(false);
//        TextRange.SetEnabled(true);
//    }
//    else
//    {
//        alert("T");
//        NumRangeStart.SetEnabled(true);
//        NumRangeEnd.SetEnabled(true);
//        TextRange.SetEnabled(false);
//    }
    GridMenu.PerformCallback('SelectValueType|'+ ValueType.GetValue());
//GridMenu.PerformCallback();


    }

function SelectEndRange(s, e) {
    if (NumRangeEnd.GetValue() < NumRangeStart.GetValue()) {
            NumRangeEnd.SetValue(null);
            toastr.warning('End Range cannot less then Start Range', 'Warning');
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

    function SelectStartRange(s, e) {
        if(NumRangeEnd.GetValue() != 0.0000){
            if (NumRangeStart.GetValue() > NumRangeEnd.GetValue()) {
            NumRangeStart.SetValue(null);
            toastr.warning('End Range cannot less then Start Range', 'Warning');
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

    function ClickSave(s, e) {
        GridMenu.PerformCallback('Update|'+ cbopartid.GetValue() + '|' + cborevno.GetValue());
        GridMenu.PerformCallback('GetGrid|' + cbopartid.GetValue() + '|' + cborevno.GetValue() + '|' + btnSave.GetText());
    }

    function Key(s, e) {
        cborevno.SetValue(null);
    }

//     function BtnApproveClick(s, e) {
////        GridMenu.PerformCallback(btnApprove.GetText());
////        GridMenu.PerformCallback('GetGrid|'+ cbopartid.GetValue() + '|' + cborevno.GetValue());
////        GridMenu.PerformCallback('CekStatusApprove|');
//          GridMenu.PerformCallback('ClickApprove|' + btnApprove.GetText());
//    }


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

        if (cbolineidpopup.GetText() == '' ) {
        toastr.warning('Please select Line No!', 'Warning');
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

        GridMenu.PerformCallback('Copy|' + cbolineidpopup.GetValue()  + '|' + cbopartidpopup.GetValue() + '|' + cborevnopopup.GetValue());
        pcLogin.Hide();
    }

    function ClickCopyFromOtherPart(s, e) {
        if (cbolineid.GetText() == '' ) {
        toastr.warning('Please select Line No!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
        } 

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
                cbolineidpopup.SetText('');
                cbopartidpopup.SetText('');
                txtpartnamepopup.SetText('');
                cborevnopopup.SetText('');
                pcLogin.Show();
            }
            else{
            pcLogin.Hide();
            }
        }
        else{
                cbolineidpopup.SetText('');
                cbopartidpopup.SetText('');
                txtpartnamepopup.SetText('');
                cborevnopopup.SetText('');
                pcLogin.Show();
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



    function SelectValueType1(s,e){
        
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

     function SelectValueTypeInit(s,e){
            SetTextRangeVisibility(false);
            SetNumRangeStartVisibility(false);
            SetNumRangeEndVisibility(false);
     }

//=========================================================================================================================================================================//

function SelectLineID(s, e) {
    if(ClickNewStatus == true){
        cborevno.SetValue('');
        dtrevdate.SetText('');
        txtrevisionhistory.SetText('');
        txtpreparedby.SetText('');
        ASPxImage1.SetImageUrl('img/musashi/ImagePreview1.gif');
        ASPxImage2.SetImageUrl('img/musashi/ImagePreview1.gif');
        ASPxImage3.SetImageUrl('img/musashi/ImagePreview1.gif');
        ASPxImage4.SetImageUrl('img/musashi/ImagePreview1.gif');
        ASPxImage5.SetImageUrl('img/musashi/ImagePreview1.gif');
        ASPxImage6.SetImageUrl('img/musashi/ImagePreview1.gif');
//        txtsafety.SetText('');
        cboattachment.SetText('');
        lblActive.SetText('Inactive');
        lblActive.GetMainElement().style.color = 'Red';

        GridMenu.PerformCallback('ClearGridPart|');
        GridMenu.PerformCallback('GridClear|');
        cborevno.PerformCallback('|' + cbopartid.GetValue() + '|' + cbolineid.GetValue());
      }
    if(ClickNewStatus == false){
//        cborevno.SetText('--New--');
//        var today = new Date();
//        dtrevdate.SetDate(today);
//        txtrevisionhistory.SetText('');
//        txtpreparedby.SetText('');

//        GridMenu.PerformCallback('ClearGridPart|');
//        GridMenu.PerformCallback('GridClear|');
          
          GridMenu.PerformCallback('SelectNewClick|');
      }
}

//function InitLineID(s, e) {
//    cbopartid.PerformCallback('load|' + cbolineid.GetValue());
//    alert('myParam');
////    const urlParams = new URLSearchParams(window.location.search);
//    var myParam = urlParams.get('PartID');
//    alert(myParam);
//}

function SelectPartID(s, e) {
    if(ClickNewStatus == true){
        txtpartname.SetText(cbopartid.GetSelectedItem().GetColumnText(1));
        cborevno.SetValue('');
        dtrevdate.SetText('');
        txtrevisionhistory.SetText('');
        txtpreparedby.SetText('');
//        txtsafety.SetText('');
        ASPxImage1.SetImageUrl('img/musashi/ImagePreview1.gif');
        ASPxImage2.SetImageUrl('img/musashi/ImagePreview1.gif');
        ASPxImage3.SetImageUrl('img/musashi/ImagePreview1.gif');
        ASPxImage4.SetImageUrl('img/musashi/ImagePreview1.gif');
        ASPxImage5.SetImageUrl('img/musashi/ImagePreview1.gif');
        ASPxImage6.SetImageUrl('img/musashi/ImagePreview1.gif');
        cboattachment.SetText('');
        lblActive.SetText('Inactive');
        lblActive.GetMainElement().style.color = 'Red';

        cbolineid.PerformCallback('|');
        GridMenu.PerformCallback('ClearGridPart|');
        GridMenu.PerformCallback('GridClear|');
      }
    if(ClickNewStatus == false){
        txtpartname.SetText(cbopartid.GetSelectedItem().GetColumnText(1));

        cborevno.SetText('--New--');
        var today = new Date();
        dtrevdate.SetDate(today);
        txtrevisionhistory.SetText('');
        txtpreparedby.SetText('');
        lblActive.SetText('Inactive');

        cbolineid.PerformCallback('|');
      }
}

function SelectRevNo(s, e) {
    GridMenu.PerformCallback('GridGet|'+ cbopartid.GetValue() + '|' + cborevno.GetValue());
}

var ClickNewStatus = true;
function ClickNew(s, e) {
    if (cbolineid.GetValue() == null){
          cborevno.SetValue('--New--');
          cborevno.SetEnabled(false);
          var today = new Date();
          dtrevdate.SetDate(today);
          txtrevisionhistory.SetText('');
          txtpreparedby.SetText('');
//          txtsafety.SetText('');
          lblActive.SetText('Inactive');
          lblActive.GetMainElement().style.color = 'Red';
          GridMenu.PerformCallback('ClickNew|');

          btnCopy.SetEnabled(true);
          btnInput.SetEnabled(false);
          btnExcel.SetEnabled(false);
          btnNew.SetEnabled(false);
          btnDelete.SetEnabled(false);
          btnCancel.SetEnabled(true);
          btnSave.SetEnabled(false);
          btnApprove.SetEnabled(false);

          ClickNewStatus = false;
    }
    else{
        GridMenu.PerformCallback('ButtonNewClick|');
        ClickNewStatus = false;
    }
  
//  cborevno.SetValue('--New--');
//  cborevno.SetEnabled(false);
//  var today = new Date();
//  dtrevdate.SetDate(today);
//  txtrevisionhistory.SetText('');
//  txtpreparedby.SetText('');
//  lblActive.SetText('Inactive');
//  lblActive.GetMainElement().style.color = 'Red';
//  GridMenu.PerformCallback('ClickNew|');

//  btnCopy.SetEnabled(true);
//  btnInput.SetEnabled(false);
//  btnExcel.SetEnabled(false);
//  btnNew.SetEnabled(false);
//  btnDelete.SetEnabled(false);
//  btnCancel.SetEnabled(true);
//  btnSave.SetEnabled(false);
//  btnApprove.SetEnabled(false);

//  ClickNewStatus = false;
} 

function ClickCancel(s, e) {  
   cbolineid.SetText('');
   cbopartid.SetText('');
   txtpartname.SetText('');
   cborevno.SetText('');
   cborevno.SetEnabled(true);
   dtrevdate.SetText('');
   dtrevdate.SetEnabled(true);
   txtrevisionhistory.SetText('');
   txtpreparedby.SetText('');
//   txtsafety.SetText('');
   lblActive.SetText('Inactive');
   lblActive.GetMainElement().style.color = 'Red';

   btnCopy.SetEnabled(false);
   btnInput.SetEnabled(false);
   btnExcel.SetEnabled(false);
   btnInput.SetEnabled(false);
   btnNew.SetEnabled(true);
   btnDelete.SetEnabled(false);
   btnCancel.SetEnabled(false);
   btnSave.SetEnabled(false);
   btnApprove.SetEnabled(false);

   GridMenu.PerformCallback('GridClear|');

   ClickNewStatus = true;
   cbopartid.PerformCallback('|' + '');
} 

function BtnApproveClick(s, e) {
    if (btnApprove.GetText() == 'Approve'){
        var r = confirm("Are you sure want to approve?");
        if (r == true) {
            GridMenu.PerformCallback('ClickApprove|' + btnApprove.GetText());
        } 
    }
    if (btnApprove.GetText() == 'Unapprove'){
        var r = confirm("Are you sure want to unapprove?");
        if (r == true) {
            GridMenu.PerformCallback('ClickApprove|' + btnApprove.GetText());
        } 
    }
//   GridMenu.PerformCallback('ClickApprove|' + btnApprove.GetText());
}

function RevNoInit(s, e) {
    if (cbopartid.GetValue() == null){
//        alert(cbopartid.GetValue());
    }else{
        GridMenu.PerformCallback('GridGet|'+ cbopartid.GetValue() + '|' + cborevno.GetValue());
    }
}

//function OnInitPartID(s, e) {
////    cbopartid.SetValue('ALL');
////    txtpartname.SetValue('ALL');     
//    cbopartid.PerformCallback('|' + '');       
//}

var startTime;
function OnBeginCallbackPartID() {
	startTime = new Date();
}
function OnEndCallbackPartID() {
	var result = new Date() - startTime;
	result /= 1000;
	result = result.toString();
	if(result.length > 4)
		result = result.substr(0, 4);
}

function ClickUploadSymbol(s, e) {
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

        if (cbolineid.GetText() == '' ) {
        toastr.warning('Please select Line No!', 'Warning');
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

        if (cborevno.GetText() == '--New--' ) {
        toastr.warning('Please save data before upload safety symbol!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
        }

        pcuploadsymbol.Show();
    }

function ClickDeleteSymbol1(s, e) {  
    var r = confirm("Are you sure want to delete safety symbol 1?");
    if (r == true) {
        GridMenu.PerformCallback('DeleteSymbol1');
    } 
} 

function ClickDeleteSymbol2(s, e) {  
    var r = confirm("Are you sure want to delete safety symbol 2?");
    if (r == true) {
        GridMenu.PerformCallback('DeleteSymbol2');
    } 
} 

function ClickDeleteSymbol3(s, e) {  
    var r = confirm("Are you sure want to delete safety symbol 3?");
    if (r == true) {
        GridMenu.PerformCallback('DeleteSymbol3');
    } 
} 

function ClickDeleteSymbol4(s, e) {  
    var r = confirm("Are you sure want to delete safety symbol 4?");
    if (r == true) {
        GridMenu.PerformCallback('DeleteSymbol4');
    } 
} 

function ClickDeleteSymbol5(s, e) {  
    var r = confirm("Are you sure want to delete safety symbol 5?");
    if (r == true) {
        GridMenu.PerformCallback('DeleteSymbol5');
    } 
} 

function ClickDeleteSymbol6(s, e) {  
    var r = confirm("Are you sure want to delete safety symbol 6?");
    if (r == true) {
        GridMenu.PerformCallback('DeleteSymbol6');
    } 
} 
	</script>
    <style type="text/css">
        .style1
        {
            width: 162px;
        }
        .style2
        {
        }
        .style3
        {
            width: 97px;
        }
        .style4
        {
            width: 46px;
        }
        .style9
        {
            width: 73px;
        }
        .style10
        {
            width: 124px;
        }
        .style12
        {
            width: 63px;
        }
        .style13
        {
            width: 77px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

<div style="padding: 0px 5px 5px 5px">
    <table class="auto-style3" style="width: 100%">
        <tr >
            <td style="width:60px; padding:5px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Part No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:150px; padding:5px 0px 0px 0px">
                <dx:ASPxComboBox ID="cbopartid" runat="server" Theme="Office2010Black" TextField="PartName"
                    ClientInstanceName="cbopartid" DropDownStyle="DropDownList" ValueField="PartID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                    Width="150px" TabIndex="1" EnableCallbackMode="true" CallbackPageSize="10">
                    <ClientSideEvents SelectedIndexChanged="SelectPartID" BeginCallback="function(s, e) { OnBeginCallbackPartID(); }" EndCallback="function(s, e) { OnEndCallbackPartID(); } "/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Part ID" FieldName="PartID" Width="120px" />
                        <dx:ListBoxColumn Caption="Part Name" FieldName="PartName" Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style=" width:60px; padding:5px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Rev. No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding:5px 0px 0px 0px; width: 20px;" class="style13">
                <dx:ASPxComboBox ID="cborevno" runat="server" Theme="Office2010Black" TextField="RevNo"
                    ClientInstanceName="cborevno" DropDownStyle="DropDownList" ValueField="RevNo"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="StartsWith" TextFormatString="{0}-{1}-{2}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="4">
                    <ClientSideEvents SelectedIndexChanged="SelectRevNo" Init="RevNoInit"/> <%--KeyDown="Key" KeyPress="Key" KeyUp="Key"--%>
                    <Columns>
                        <dx:ListBoxColumn Caption="Rev No" FieldName="RevNo" Width="70px"/>
                        <dx:ListBoxColumn Caption="Rev Date" FieldName="RevDate" Width="70px"/>
                        <dx:ListBoxColumn Caption="Rev History" FieldName="RevHistory" Width="200px"/>
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Width="5px" Paddings-Padding="4px" ></ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style="padding:5px 0px 0px 0px; width: 20px;" class="style13">
                &nbsp;</td>
            <td style=" padding: 5px 0px 0px 0px; width:75px">
                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Prepared By" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding: 5px 0px 0px 0px; width: 100px;" class="style12" colspan="2">
                <dx:ASPxTextBox ID="txtpreparedby" runat="server" BackColor="White" 
                    ClientInstanceName="txtpreparedby" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="120px" TabIndex="7">
                </dx:ASPxTextBox>
                </td>
            <td style="padding: 5px 0px 0px 0px; border: 1px solid silver; background-color:#808080" 
                align="center" class="style9">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Approval" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="White">
                </dx:ASPxLabel>
            </td>
            <td style="padding: 5px 0px 0px 0px; border: 1px solid silver; background-color:#808080" 
                align="center" class="style10">
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
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Part Name" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:150px; padding:3px 0px 0px 0px">
                <dx:ASPxTextBox ID="txtpartname" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtpartname" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="150px" ReadOnly="True" TabIndex="2">
                </dx:ASPxTextBox>
            </td>
            <td style=" width:60px; padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Rev. Date" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding:3px 0px 0px 0px; width: 20px;" class="style13">
                <dx:ASPxDateEdit ID="dtrevdate" runat="server" Theme="Office2010Black" 
                    Width="100px" AutoPostBack="false"
                        ClientInstanceName="dtrevdate" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="5">
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
            <td style="padding:3px 0px 0px 0px; width: 20px;" class="style13">
                &nbsp;</td>
            <td style=" padding: 3px 0px 0px 0px; width:75px">
                <%--<ClientSideEvents SelectedIndexChanged="function SelectValueType1(s,e){
                        alert(ValueType.GetText());
                        if (ValueType.GetText() == 'T') {
                            alert('You Select T');
                            SetUnitPriceColumnVisibility(true);
                        }
                        else {
                            alert('You Select N');
                            SetUnitPriceColumnVisibility(false);
                        }
                    }"/>--%>
                <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="Attachment" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding:3px 0px 0px 0px; width: 100px;" class="style12" 
                xml:lang="100px" colspan="2">
                <%--<dx:ASPxTextBox ID="txtapprovedby" runat="server" BackColor="White" 
                    ClientInstanceName="txtapprovedby" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="150px" TabIndex="8" ReadOnly="True">
                </dx:ASPxTextBox>--%>
               <dx:ASPxComboBox ID="cboattachment" runat="server" Theme="Office2010Black" TextField="cboattachment"
                    ClientInstanceName="cboattachment" DropDownStyle="DropDownList"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="120px" TabIndex="3">
                     <Items>
                        <dx:ListEditItem Text="Required" Value="1"/>
                        <dx:ListEditItem Text="Not Required" Value="0"/>
                    </Items>
                    <ItemStyle Height="10px" Paddings-Padding="4px"/>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style="border: 1px solid silver; padding: 3px 0px 0px 5px; " class="style9">
                <dx:ASPxLabel ID="lbllineleaderstatus" runat="server" Text="Line Leader" 
                    Font-Names="Segoe UI" Font-Size="9pt" 
                    ClientInstanceName="lbllineleaderstatus">
                </dx:ASPxLabel>
                </td>
            <td style="border: 1px solid silver; padding: 3px 0px 0px 0px; " align="center" 
                class="style10">
                <dx:ASPxLabel ID="lbllineleaderpic" runat="server" Text="UNAPPROVED" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="Red" 
                    ClientInstanceName="lbllineleaderpic">
                </dx:ASPxLabel>
                </td>
            <td style="border: 1px solid silver; padding: 3px 0px 0px 0px; width: 10px;" align="center">
                <dx:ASPxLabel ID="lbllineleaderdate" runat="server" Text="UNAPPROVED" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="Red" 
                    ClientInstanceName="lbllineleaderdate">
                </dx:ASPxLabel>
                </td>
        </tr>

        <tr>
            <td style=" width:60px; padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Line &amp; Process" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:150px; padding:3px 0px 0px 0px">
               <dx:ASPxComboBox ID="cbolineid" runat="server" Theme="Office2010Black" TextField="LineName"
                    ClientInstanceName="cbolineid" DropDownStyle="DropDownList" ValueField="LineID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                    Width="150px" TabIndex="3">
                    <ClientSideEvents SelectedIndexChanged="SelectLineID"/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Line ID" FieldName="LineID" Width="70px" />
                        <dx:ListBoxColumn Caption="Line Name" FieldName="LineName" 
                            Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px"/>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style=" width:70px; padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Rev. History" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding:3px 0px 0px 0px; " colspan="2">
                <dx:ASPxTextBox ID="txtrevisionhistory" runat="server" BackColor="White" 
                    ClientInstanceName="txtrevisionhistory" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="190px" TabIndex="6">
                </dx:ASPxTextBox>
            </td>
            <td style="padding:3px 0px 0px 0px; width: 75px;">
                <dx:ASPxLabel ID="lblqeleader0" runat="server" Text="Active Status" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding:0px 0px 0px 0px; " colspan="2">
                <%--<dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="Safety Symbol" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>--%>
                <%--<dx:ASPxTextBox ID="txtsafety" runat="server" BackColor="White" 
                    ClientInstanceName="txtsafety" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="25pt" Height="60px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="135px" TabIndex="8">
                </dx:ASPxTextBox>--%>
                <dx:ASPxLabel ID="lblActive" runat="server" Text="Inactive" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="Red" Font-Bold="true" 
                    ClientInstanceName="lblActive">
                </dx:ASPxLabel>
            </td>
            <td style="border: 1px solid silver; padding: 3px 0px 0px 5px; " class="style9">
                <dx:ASPxLabel ID="lbllineforemanstatus" runat="server" Text="Line Foreman" 
                    Font-Names="Segoe UI" Font-Size="9pt" 
                    ClientInstanceName="lbllineforemanstatus">
                </dx:ASPxLabel>
                </td>
            <td style="border: 1px solid silver; padding: 3px 0px 0px 0px; " align="center" 
                class="style10">
                <dx:ASPxLabel ID="lbllineforemanpic" runat="server" Text="UNAPPROVED" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="Red" 
                    ClientInstanceName="lbllineforemanpic">
                </dx:ASPxLabel>
                </td>
            <td style="border: 1px solid silver; padding: 3px 0px 0px 0px; width: 10px;" align="center">
                <dx:ASPxLabel ID="lbllineforemandate" runat="server" Text="UNAPPROVED" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="Red" 
                    ClientInstanceName="lbllineforemandate">
                </dx:ASPxLabel>
                </td>
        </tr>

        <tr>
            <td style=" width:60px; padding:3px 0px 0px 0px">
                &nbsp;</td>
            <td style=" width:150px; padding:3px 0px 0px 0px">
                &nbsp;</td>
            <td style=" width:60px; padding:3px 0px 0px 0px">
                &nbsp;</td>
            <td style="padding:3px 0px 5px 0px; width: 20px;" class="style13">
            </td>
            <td style="padding:3px 0px 5px 0px; width: 20px;" class="style13">
                &nbsp;</td>
            <td style=" padding: 3px 0px 5px 0px; width:20px">
                &nbsp;</td>
            <td style="padding:0px 0px 0px 0px; width: 20px;">
                &nbsp;</td>
            <td style="padding:3px 0px 0px 0px; width: 80px;">
                &nbsp;</td>
            <td style="border: 1px solid silver; padding: 3px 0px 5px 5px; " class="style9">
                <dx:ASPxLabel ID="lblqeleaderstatus" runat="server" Text="QE Leader" 
                    Font-Names="Segoe UI" Font-Size="9pt" 
                    ClientInstanceName="lblqeleaderstatus">
                </dx:ASPxLabel>
                </td>
            <td style="border: 1px solid silver; padding: 3px 0px 5px 0px; " align="center" 
                class="style10">
                <dx:ASPxLabel ID="lblqeleaderpic" runat="server" Text="UNAPPROVED" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="Red" 
                    ClientInstanceName="lblqeleaderpic">
                </dx:ASPxLabel>
                </td>
            <td style="border: 1px solid silver; padding: 3px 0px 5px 0px; width: 10px;" align="center">
                <dx:ASPxLabel ID="lblqeleaderdate" runat="server" Text="UNAPPROVED" 
                    Font-Names="Segoe UI" Font-Size="9pt" ForeColor="Red" 
                    ClientInstanceName="lblqeleaderdate">
                </dx:ASPxLabel>
                </td>
        </tr>

        <tr>
            <td style=" padding:3px 0px 0px 0px" colspan="3">
                <table style="width:100%;">
                    <tr>
                        <td width="85px">
                <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="Safety Symbol" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
                        </td>
                        <td width="30px" align="center">
                            <dx:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" ClientInstanceName="ASPxImage1"
                                Width="30px" Height="30px" Border-BorderWidth="1px" Border-BorderColor="Black" 
                                ImageUrl="img/musashi/ImagePreview1.gif">
                            </dx:ASPxImage>
                        </td>
                        <td width="30px" align="center">
                            <dx:ASPxImage ID="ASPxImage2" runat="server" ShowLoadingImage="true" ClientInstanceName="ASPxImage2"
                                Width="30px" Height="30px" Border-BorderWidth="1px" Border-BorderColor="Black" 
                                ImageUrl="img/musashi/ImagePreview1.gif">
                            </dx:ASPxImage>
                        </td>
                        <td width="30px" align="center">
                            <dx:ASPxImage ID="ASPxImage3" runat="server" ShowLoadingImage="true" ClientInstanceName="ASPxImage3"
                                Width="30px" Height="30px" Border-BorderWidth="1px" Border-BorderColor="Black" 
                                ImageUrl="img/musashi/ImagePreview1.gif">
                            </dx:ASPxImage>
                        </td>
                        <td width="30px" align="center">
                            <dx:ASPxImage ID="ASPxImage4" runat="server" ShowLoadingImage="true" ClientInstanceName="ASPxImage4"
                                Width="30px" Height="30px" Border-BorderWidth="1px" Border-BorderColor="Black" 
                                ImageUrl="img/musashi/ImagePreview1.gif">
                            </dx:ASPxImage>
                        </td>
                        <td width="30px" align="center">
                            <dx:ASPxImage ID="ASPxImage5" runat="server" ShowLoadingImage="true" ClientInstanceName="ASPxImage5"
                                Width="30px" Height="30px" Border-BorderWidth="1px" Border-BorderColor="Black" 
                                ImageUrl="img/musashi/ImagePreview1.gif">
                            </dx:ASPxImage>
                        </td>
                        <td width="30px" align="center">
                            <dx:ASPxImage ID="ASPxImage6" runat="server" ShowLoadingImage="true" ClientInstanceName="ASPxImage6"
                                Width="30px" Height="30px" Border-BorderWidth="1px" Border-BorderColor="Black" 
                                ImageUrl="img/musashi/ImagePreview1.gif">
                            </dx:ASPxImage>
                        </td>
                    </tr>
                    <tr>
                        <td width="85px">
                            &nbsp;</td>
                        <td width="30px" align="center" rowspan="2">
                <dx:ASPxButton ID="btnDeleteSymbol1" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnDeleteSymbol1" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="16px" Theme="Default" UseSubmitBehavior="false" 
                    Width="16px" TabIndex="10" ClientEnabled="true" RenderMode="Link" ClientVisible="false">
                    <ClientSideEvents Click="ClickDeleteSymbol1" />
                    <Image IconID="actions_cancel_16x16gray">
                    </Image>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
                        </td>
                        <td width="30px" align="center" rowspan="2">
                <dx:ASPxButton ID="btnDeleteSymbol2" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnDeleteSymbol2" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="16px" Theme="Default" UseSubmitBehavior="false" 
                    Width="16px" TabIndex="10" ClientEnabled="true" RenderMode="Link" ClientVisible="false">
                    <ClientSideEvents Click="ClickDeleteSymbol2" />
                    <Image IconID="actions_cancel_16x16gray">
                    </Image>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
                        </td>
                        <td width="30px" align="center" rowspan="2">
                <dx:ASPxButton ID="btnDeleteSymbol3" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnDeleteSymbol3" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="16px" Theme="Default" UseSubmitBehavior="false" 
                    Width="16px" TabIndex="10" ClientEnabled="true" RenderMode="Link" ClientVisible="false">
                    <ClientSideEvents Click="ClickDeleteSymbol3" />
                    <Image IconID="actions_cancel_16x16gray">
                    </Image>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
                        </td>
                        <td width="30px" align="center" rowspan="2">
                <dx:ASPxButton ID="btnDeleteSymbol4" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnDeleteSymbol4" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="16px" Theme="Default" UseSubmitBehavior="false" 
                    Width="16px" TabIndex="10" ClientEnabled="true" RenderMode="Link" ClientVisible="false">
                    <ClientSideEvents Click="ClickDeleteSymbol4" />
                    <Image IconID="actions_cancel_16x16gray">
                    </Image>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
                        </td>
                        <td width="30px" align="center" rowspan="2">
                <dx:ASPxButton ID="btnDeleteSymbol5" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnDeleteSymbol5" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="16px" Theme="Default" UseSubmitBehavior="false" 
                    Width="16px" TabIndex="10" ClientEnabled="true" RenderMode="Link" ClientVisible="false">
                    <ClientSideEvents Click="ClickDeleteSymbol5" />
                    <Image IconID="actions_cancel_16x16gray">
                    </Image>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
                        </td>
                        <td width="30px" align="center" rowspan="2">
                <dx:ASPxButton ID="btnDeleteSymbol6" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnDeleteSymbol6" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="16px" Theme="Default" UseSubmitBehavior="false" 
                    Width="16px" TabIndex="10" ClientEnabled="true" RenderMode="Link" ClientVisible="false">
                    <ClientSideEvents Click="ClickDeleteSymbol6" />
                    <Image IconID="actions_cancel_16x16gray">
                    </Image>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td width="85px">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td style="padding:3px 0px 5px 0px; width: 20px;" class="style13">
                &nbsp;</td>
            <td style="padding:3px 0px 5px 0px; width: 20px;" class="style13">
                &nbsp;</td>
            <td style=" padding: 3px 0px 5px 0px; width:20px">
                &nbsp;</td>
            <td style="padding:0px 0px 0px 0px; width: 20px;">
                &nbsp;</td>
            <td style="padding:3px 0px 0px 0px; width: 80px;">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 5px; " class="style9">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; " align="center" 
                class="style10">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; width: 10px;" align="center">
                &nbsp;</td>
        </tr>

        </table>
</div>

<div style="padding: 2px 5px 5px 5px">
   <%-- <dx:ASPxGridView ID="GridMenu" runat="server" AutoGenerateColumns="False" 
        ClientInstanceName="GridMenu" EnableTheming="True" Font-Names="Segoe UI" 
        Font-Size="9pt" KeyFieldName="SeqNo" OnRowDeleting="GridMenu_RowDeleting" 
        OnRowInserting="GridMenu_RowInserting" OnRowValidating="GridMenu_RowValidating" 
        OnStartRowEditing="GridMenu_StartRowEditing"
        Theme="Office2010Black" 
        Width="100%">
        <ClientSideEvents EndCallback="OnEndCallback" />--%>

        <dx:ASPxGridView ID="GridMenu" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridMenu"
            EnableTheming="True" KeyFieldName="ItemID" Theme="Office2010Black" 
            OnStartRowEditing="GridMenu_StartRowEditing" OnRowValidating="GridMenu_RowValidating"
            OnRowInserting="GridMenu_RowInserting" OnRowDeleting="GridMenu_RowDeleting" 
            OnAfterPerformCallback="GridMenu_AfterPerformCallback" OnCellEditorInitialize="GridMenu_CellEditorInitialize" Width="100%" 
            Font-Names="Segoe UI" Font-Size="9pt">
            <ClientSideEvents EndCallback="OnEndCallback" />
        <Columns>

            <dx:GridViewCommandColumn FixedStyle="Left" ShowDeleteButton="true" 
            ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" 
                Width="100px">
            </dx:GridViewCommandColumn>

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

              <dx:GridViewDataTextColumn Caption="ID" FieldName="ItemID"
                    VisibleIndex="1" Width="50px" Visible="False">
                    <PropertiesTextEdit MaxLength="15" Width="50px" ClientInstanceName="ItemID">
                        <%--<ClientSideEvents Validation="SessionIDValidation" />--%>
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>
                    <EditFormSettings VisibleIndex="1" Visible="True" />
                    <HeaderStyle Paddings-PaddingLeft="6px" HorizontalAlign="Center" VerticalAlign="Middle">
<Paddings PaddingLeft="6px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="No" FieldName="Number" VisibleIndex="2" 
                    Width="35px">
                    <EditFormSettings Visible="False" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
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

            <dx:GridViewDataComboBoxColumn FieldName="ProcessID" VisibleIndex="3" 
                Width="100px" Caption="Process ID" 
                Visible="False">
                <PropertiesComboBox TextField="ProcessID" ValueField="ProcessID" 
                Width="100px" TextFormatString="{0}-{1}" DisplayFormatString="{0}" ClientInstanceName="ProcessID" DropDownStyle="DropDownList" 
                IncrementalFilteringMode="Contains">
                   <ClientSideEvents SelectedIndexChanged="function(){ProcessName.SetText(ProcessID.GetSelectedItem().GetColumnText(1));}"/>
                    <Columns>
                        <dx:listboxcolumn Caption="Process ID" FieldName="ProcessID" Width="62px"/>
                        <dx:listboxcolumn Caption="Process Name" FieldName="ProcessName" Width="200px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                    <Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Width="5px" Paddings-Padding="2px" >
                    <Paddings Padding="2px"></Paddings>
                    </ButtonStyle>
                    </PropertiesComboBox>
                    <EditFormSettings VisibleIndex="3" Visible="True" />
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle" >
                        <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </dx:GridViewDataComboBoxColumn>

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
                <HeaderStyle Paddings-PaddingLeft="2px" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                <Paddings PaddingLeft="2px"></Paddings>
                </HeaderStyle>
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn Caption="Item" FieldName="Item" Settings-AutoFilterCondition="Contains" 
            VisibleIndex="6" Width="150px" >
                <PropertiesTextEdit MaxLength="50" Width="150px" ClientInstanceName="Item">
                    <Style HorizontalAlign="Left"></Style>
                </PropertiesTextEdit>
                <Settings AutoFilterCondition="Contains"></Settings>
                <EditFormSettings VisibleIndex="6" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Standard" FieldName="Standard" Settings-AutoFilterCondition="Contains" 
            VisibleIndex="8" Width="150px" >
                <PropertiesTextEdit MaxLength="50" Width="150px" ClientInstanceName="Standard">
                    <Style HorizontalAlign="Left"></Style>
                </PropertiesTextEdit>
                <Settings AutoFilterCondition="Contains"></Settings>
                <EditFormSettings VisibleIndex="8" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                </CellStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Range" FieldName="Range" Settings-AutoFilterCondition="Contains" 
            VisibleIndex="9" Width="150px" >
                <PropertiesTextEdit MaxLength="15" Width="150px" ClientInstanceName="Range">
                    <Style HorizontalAlign="Left"></Style>
                </PropertiesTextEdit>
                <Settings AutoFilterCondition="Contains"></Settings>
                <EditFormSettings Visible="False" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataComboBoxColumn Caption="ValueType" FieldName="ValueType"
                VisibleIndex="7" Width="75px">
                <PropertiesComboBox DropDownStyle="DropDownList" Width="70px" TextFormatString="{0}"
                    IncrementalFilteringMode="StartsWith" TextField="ValueType" DisplayFormatInEditMode="true"
                    ValueField="ValueType" ClientInstanceName="ValueType" >
                    <ClientSideEvents SelectedIndexChanged="SelectValueType1" Init="SelectValueTypeInit"/>
                    <%--<ClientSideEvents SelectedIndexChanged="function SelectValueType1(s,e){
                        alert(ValueType.GetText());
                        if (ValueType.GetText() == 'T') {
                            alert('You Select T');
                            SetUnitPriceColumnVisibility(true);
                        }
                        else {
                            alert('You Select N');
                            SetUnitPriceColumnVisibility(false);
                        }
                    }"/>--%>
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
                <EditFormSettings VisibleIndex="7" />
                <HeaderStyle Paddings-PaddingLeft="2px" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
<Paddings PaddingLeft="2px"></Paddings>
                </HeaderStyle>
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
            </dx:GridViewDataComboBoxColumn>

            <dx:GridViewDataTextColumn Caption="NumRangeStart" FieldName="NumRangeStart" 
                Settings-AutoFilterCondition="Contains" VisibleIndex="10" Width="150px" 
                Visible="False" EditFormSettings-Caption="">
                <PropertiesTextEdit DisplayFormatString="d" ClientInstanceName="NumRangeStart" >
                <%--<ClientSideEvents TextChanged="SelectStartRange"/>--%>
          <MaskSettings Mask="<0..999999g>.<00..999>"/>
    </PropertiesTextEdit>
    
<Settings AutoFilterCondition="Contains"></Settings>
                <EditFormSettings VisibleIndex="9" Visible="True" />
                <EditCellStyle CssClass="NumRangeStart" />
                    <EditFormCaptionStyle CssClass="NumRangeStart"></EditFormCaptionStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="NumRangeEnd" FieldName="NumRangeEnd" 
                VisibleIndex="11" Width="150px" Visible="False">
                <PropertiesTextEdit DisplayFormatString="c" ClientInstanceName="NumRangeEnd" NullText="">
                <%--<ClientSideEvents TextChanged="SelectEndRange"/>--%>
          <MaskSettings Mask="<0..999999g>.<00..999>"/>
    </PropertiesTextEdit>
    
                <EditFormSettings VisibleIndex="10" Visible="True" />
                <EditCellStyle CssClass="NumRangeEnd" />
                    <EditFormCaptionStyle CssClass="NumRangeEnd"></EditFormCaptionStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="TextRange" FieldName="TextRange"
                VisibleIndex="12" Width="150px" Settings-AutoFilterCondition="Contains" 
                Visible="False" >
                <PropertiesTextEdit MaxLength="15" Width="150px" ClientInstanceName="TextRange">
                    <Style HorizontalAlign="Left"></Style>
                </PropertiesTextEdit>

<Settings AutoFilterCondition="Contains"></Settings>

                <EditFormSettings VisibleIndex="11" Caption="TextRange" Visible="True" />
                <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="TextRange">
<Paddings PaddingLeft="5px"></Paddings>
                </HeaderStyle>
                <CellStyle HorizontalAlign="Left" VerticalAlign="Middle" CssClass="TextRange"></CellStyle>

                    <EditCellStyle CssClass="TextRange" />
                    <EditFormCaptionStyle CssClass="TextRange"></EditFormCaptionStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataComboBoxColumn Caption="Measuring Instrument" FieldName="MeasuringInstrument" 
                    VisibleIndex="13" Width="150px">
                    <EditFormSettings VisibleIndex="12" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox
                                    ClientInstanceName="MeasuringInstrument" 
                                    DropDownStyle="DropDown" 
                                    Width="150px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="MeasuringInstrument" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="MeasuringInstrument"
                                    DataSourceID="SqlDataSource2"
                                    MaxLength = "100"
                                    >
                                    <Columns>
                                        <dx:listboxcolumn Caption="" FieldName="MeasuringInstrument" Width="68px"/>
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

            <dx:GridViewDataTextColumn Caption="Process" VisibleIndex="4" 
                FieldName="ProcessName">
            <PropertiesTextEdit ClientInstanceName="ProcessName"></PropertiesTextEdit>
                <EditFormSettings VisibleIndex="4" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="XR Code" VisibleIndex="14" 
                FieldName="XRCode" Width="60px">
            <PropertiesTextEdit ClientInstanceName="XRCode" MaxLength="3" Width="50px"></PropertiesTextEdit>
                <EditFormSettings VisibleIndex="13"/>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Sequence No." FieldName="SeqNo" VisibleIndex="15" 
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
           <%-- <dx:GridViewDataComboBoxColumn Caption="Frequency Type" 
                FieldName="FrequencyType" VisibleIndex="16">
            </dx:GridViewDataComboBoxColumn>--%>

            <dx:GridViewDataComboBoxColumn Caption="Frequency Type" FieldName="FrequencyType" 
                VisibleIndex="16" Width="150px">
                <PropertiesComboBox DropDownStyle="DropDownList" Width="60px" TextFormatString="{0}"
                    IncrementalFilteringMode="StartsWith" TextField="FrequencyType" DisplayFormatInEditMode="true"
                    ValueField="FrequencyType" ClientInstanceName="FrequencyType" >
                    <Items>
                        <dx:ListEditItem Text="5/Shift" Value="A" />
                        <dx:ListEditItem Text="1/Day" Value="B"/>
                        <dx:ListEditItem Text="1/Shift" Value="C"/>
                    </Items>
                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                    <Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Width="5px" Paddings-Padding="2px" >
                    <Paddings Padding="2px"></Paddings>
                    </ButtonStyle>
                </PropertiesComboBox>
                <EditFormSettings VisibleIndex="16" />
                <HeaderStyle Paddings-PaddingLeft="2px" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                <Paddings PaddingLeft="2px"></Paddings>
                </HeaderStyle>
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
            </dx:GridViewDataComboBoxColumn>

        </Columns>
        <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" />
        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
        <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true" PageSizeItemSettings-Visible="true">
<PageSizeItemSettings Visible="True"></PageSizeItemSettings>
        </SettingsPager>
        <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="250" 
            VerticalScrollBarMode="Auto" />
        <SettingsText ConfirmDelete="Are you sure want to delete ?" />
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="false" 
                VerticalAlign="WindowCenter" Width="320" />
        </SettingsPopup>
        <Styles EditFormColumnCaption-Paddings-PaddingLeft="10px" 
            EditFormColumnCaption-Paddings-PaddingRight="10px" 
            Header-Paddings-Padding="5px">
            <Header>
                <Paddings Padding="2px" />
            </Header>
            <EditFormColumnCaption Font-Names="Segoe UI" Font-Size="9pt">
                <Paddings PaddingBottom="5px" PaddingLeft="15px" PaddingRight="15px" 
                    PaddingTop="5px" />
            </EditFormColumnCaption>
            <%--<CommandColumnItem ForeColor="SteelBlue">
            </CommandColumnItem>--%>
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

<div style="padding: 0px 5px 5px 5px">
    <table class="auto-style3" style="width: 100%">
        <tr >
            <td style="width:150px; padding:5px 0px 5px 0px; border-top: 1px solid silver"">
                <dx:ASPxButton ID="btnCopy" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnCopy" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Copy From Other Part" Theme="Default" UseSubmitBehavior="false" 
                    Width="150px" TabIndex="9" ClientEnabled="False">
                    <ClientSideEvents Click="ClickCopyFromOtherPart" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style=" width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="width:50px; padding:5px 0px 5px 0px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnExcel" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnExcel" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Excel" Theme="Default" UseSubmitBehavior="false" 
                    Width="90px" TabIndex="10" ClientEnabled="False">
                    <ClientSideEvents Click="function(s, e) {
                            GridMenu.PerformCallback('excel|' + cbopartid.GetValue() + '|' + cborevno.GetValue() + '|' + cbolineid.GetValue() + '|' + txtpartname.GetValue() + '|' + lbllineleaderpic.GetText() + '|' + lbllineforemanpic.GetText() + '|' + lblqeleaderpic.GetText() + '|' + lbllineleaderdate.GetText() + '|' + lbllineforemandate.GetText() + '|' + lblqeleaderdate.GetText());
                        }" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style=" width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style=" width:10px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnUpload" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnUpload" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Upload Safety Symbol" Theme="Default" UseSubmitBehavior="false" 
                    Width="90px" TabIndex="10" ClientEnabled="false">
                    <ClientSideEvents Click="ClickUploadSymbol" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style=" width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:50px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnInput" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnInput" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Input Data XR" Theme="Default" UseSubmitBehavior="false" 
                    Width="90px" TabIndex="11" ClientEnabled="False">
                    <ClientSideEvents Click="function(s, e) {
                       
                    }"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td class="style1" style="border-top: 1px solid silver">
                &nbsp;</td>
            <td class="style1" style="border-top: 1px solid silver">
                &nbsp;</td>
            <td class="style1" style="border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:50px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnNew" runat="server" Text="New" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnNew" Theme="Default" TabIndex="12">
                    <ClientSideEvents Click="ClickNew"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:50px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnDelete" runat="server" Text="Delete" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnDelete" Theme="Default" TabIndex="13" 
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
                    ClientInstanceName="btnCancel" Theme="Default" TabIndex="14" 
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
                    Width="90px" TabIndex="15" ClientEnabled="False">
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
                    Width="90px" TabIndex="16" ClientEnabled="False">
                    <ClientSideEvents Click="BtnApproveClick"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
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

<div>
   <dx:ASPxPopupControl ID="pcLogin" runat="server" CloseAction="CloseButton"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcLogin"
        HeaderText="Copy From Other Part" AllowDragging="True" 
        PopupAnimationType="None" Height="260px" 
        Theme="Office2010Black" Width="350px">
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
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                    <td style="width:80px; padding:5px 0px 0px 0px">
                                        &nbsp;</td>
                                    <td style="width:80px; padding:5px 0px 0px 0px">
                                        &nbsp;</td>
                                    <td style="width:80px; padding:5px 0px 0px 0px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " class="style2">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width: 160px">
                                        &nbsp;</td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; width: 100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style2" style="padding: 5px 0px 0px 0px;">
                                    </td>
                                    <td class="style2" style="padding: 5px 0px 0px 0px;">
                                    </td>
                                    <td class="style3" style="padding: 5px 0px 0px 0px;" colspan="2">
                                        <dx:ASPxLabel ID="ASPxLabel14" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Part No.">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="style3" style="padding: 5px 0px 0px 0px;">
                                        &nbsp;</td>
                                    <td class="style4" style="padding: 5px 0px 0px 0px;" colspan="2">
                                        <dx:ASPxComboBox ID="cbopartidpopup" runat="server" 
                                            ClientInstanceName="cbopartidpopup" DisplayFormatString="{0}" 
                                            EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="25px" IncrementalFilteringMode="Contains" TabIndex="17" 
                                            TextField="PartName" TextFormatString="{0}-{1}" Theme="Office2010Black" 
                                            ValueField="PartID" Width="150px">
                                            <ClientSideEvents SelectedIndexChanged="SelectPartIDPopUp" />
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Part ID" FieldName="PartID" Width="130px" />
                                                <dx:ListBoxColumn Caption="Part Name" FieldName="PartName" Width="250px" />
                                            </Columns>
                                            <ItemStyle Height="10px">
                                            <Paddings Padding="4px" />
                                            </ItemStyle>
                                            <ButtonStyle Width="5px">
                                                <Paddings Padding="4px" />
                                            </ButtonStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="style2" style="padding: 5px 0px 0px 0px;">
                                    </td>
                                    <td class="style2" style="padding: 5px 0px 0px 0px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " colspan="2">
                                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Part Name">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; width:80px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " colspan="2">
                                        <dx:ASPxTextBox ID="txtpartnamepopup" runat="server" BackColor="WhiteSmoke" 
                                            ClientInstanceName="txtpartnamepopup" EnableTheming="True" 
                                            Font-Names="Segoe UI" Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                                            ReadOnly="True" TabIndex="18" Theme="Office2010Black" Width="150px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; width: 100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " colspan="2">
                                        <dx:ASPxLabel ID="ASPxLabel13" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Line No.">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; width:80px">
                                        &nbsp;</td>
                                    <td colspan="2" style="padding: 5px 0px 0px 0px; ">
                                        <dx:ASPxComboBox ID="cbolineidpopup" runat="server" 
                                            ClientInstanceName="cbolineidpopup" DisplayFormatString="{0}" 
                                            EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="25px" IncrementalFilteringMode="StartsWith" TabIndex="19" 
                                            TextField="LineName" TextFormatString="{0}-{1}" Theme="Office2010Black" 
                                            ValueField="LineID" Width="100px">
                                            <ClientSideEvents SelectedIndexChanged="SelectLineIDPopUp" />
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Line ID" FieldName="LineID" Width="70px" />
                                                <dx:ListBoxColumn Caption="Line Name" FieldName="LineName" Width="250px" />
                                            </Columns>
                                            <ItemStyle Height="10px">
                                            <Paddings Padding="4px" />
                                            </ItemStyle>
                                            <ButtonStyle Width="5px">
                                                <Paddings Padding="4px" />
                                            </ButtonStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; width: 100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " colspan="2">
                                        <dx:ASPxLabel ID="ASPxLabel15" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Rev No.">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; width:80px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " colspan="2">
                                        <dx:ASPxComboBox ID="cborevnopopup" runat="server" 
                                            ClientInstanceName="cborevnopopup" DisplayFormatString="{0}" 
                                            DropDownStyle="DropDownList" EnableIncrementalFiltering="True" 
                                            Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                                            IncrementalFilteringMode="Contains" TabIndex="20" TextField="RevNo" 
                                            TextFormatString="{0}" Theme="Office2010Black" ValueField="RevNo" 
                                            Width="100px">
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Rev No" FieldName="RevNo" Width="70px" />
                                                <dx:ListBoxColumn Caption="Rev Date" FieldName="RevDate" Width="70px"/>
                                                <dx:ListBoxColumn Caption="Rev History" FieldName="RevHistory" Width="200px"/>
                                            </Columns>
                                            <ItemStyle Height="10px">
                                            <Paddings Padding="4px" />
                                            </ItemStyle>
                                            <ButtonStyle Width="5px">
                                                <Paddings Padding="4px" />
                                            </ButtonStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; width: 100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:80px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:80px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:80px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " class="style2">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width: 160px">
                                        &nbsp;</td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; width: 100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width: 100px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:100px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:80px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:80px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width:80px">
                                        &nbsp;</td>
                                    <td style="padding: 0px 0px 0px 0px; " class="style2" colspan="3">
                                        <dx:ASPxButton ID="btnCopyPopUp" runat="server" AutoPostBack="False" 
                                            style="float: left; margin-right: 8px" Text="Copy" Width="70px" 
                                            Height="25px" ClientInstanceName="btnCopyPopUp" TabIndex="21">
                                            <ClientSideEvents Click="ClickCopyPopUp" />
                                        </dx:ASPxButton>
                                        <dx:ASPxButton ID="btCancel0" runat="server" AutoPostBack="False" 
                                            style="float: left; margin-right: 8px" Text="Cancel" Width="70px" 
                                            Height="25px" TabIndex="22">
                                            <ClientSideEvents Click="function(s, e) { pcLogin.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="padding: 5px 0px 0px 0px; width: 100px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; ">
                                    </td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; ">
                                    </td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; ">
                                        &nbsp;</td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; ">
                                        &nbsp;</td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; ">
                                        &nbsp;</td>
                                    <td class="style2" style="padding: 5px 0px 0px 0px; ">
                                        &nbsp;</td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; ">
                                        &nbsp;</td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; ">
                                    </td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; ">
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
</div>

    <br />
    <dx:ASPxPopupControl ID="pcuploadsymbol" runat="server" AllowDragging="True" 
        ClientInstanceName="pcuploadsymbol" CloseAction="CloseButton" 
        HeaderText="Upload Safety Symbol" Height="300px" PopupAnimationType="None" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        Theme="Office2010Black" Width="400px">
        <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />
        <ModalBackgroundStyle BackColor="White" CssClass="&quot;noBackground&quot;">
        </ModalBackgroundStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <dx:ASPxPanel ID="Panel2" runat="server" DefaultButton="btOK" Height="200px">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; " width="50px">
                                        &nbsp;</td>
                                    <td style="padding:5px 0px 0px 0px" width="65px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="175px">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; " width="50px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style14" style="padding: 5px 0px 0px 0px;">
                                        &nbsp;</td>
                                    <td class="style14" style="padding: 8px 0px 0px 0px;">
                                        <dx:ASPxLabel ID="ASPxLabel23" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Symbol 1">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="style14" colspan="2" style="padding: 8px 0px 0px 0px;">
                                        <asp:FileUpload ID="uploader1" runat="server" 
                                            Accept=".jpg,.jpeg,.png,.gif,.pdf" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="20px" TabIndex="12" Width="200px" />
                                    </td>
                                    <td class="style14" style="padding: 5px 0px 0px 0px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style14" style="padding: 8px 0px 0px 0px;">
                                        &nbsp;</td>
                                    <td class="style14" style="padding: 8px 0px 0px 0px;">
                                        <dx:ASPxLabel ID="ASPxLabel24" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Symbol 2">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="style14" colspan="2" style="padding: 8px 0px 0px 0px;">
                                        <asp:FileUpload ID="uploader2" runat="server" 
                                            Accept=".jpg,.jpeg,.png,.gif,.pdf" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="20px" TabIndex="12" Width="200px" />
                                    </td>
                                    <td class="style14" style="padding: 5px 0px 0px 0px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style14" style="padding: 8px 0px 0px 0px;">
                                        &nbsp;</td>
                                    <td class="style14" style="padding: 8px 0px 0px 0px;">
                                        <dx:ASPxLabel ID="ASPxLabel25" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Symbol 3">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="style14" colspan="2" style="padding: 8px 0px 0px 0px;">
                                        <asp:FileUpload ID="uploader3" runat="server" 
                                            Accept=".jpg,.jpeg,.png,.gif,.pdf" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="20px" TabIndex="12" Width="200px" />
                                    </td>
                                    <td class="style14" style="padding: 5px 0px 0px 0px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style14" style="padding: 8px 0px 0px 0px;">
                                        &nbsp;</td>
                                    <td class="style14" style="padding: 8px 0px 0px 0px;">
                                        <dx:ASPxLabel ID="ASPxLabel26" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Symbol 4">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="style14" colspan="2" style="padding: 8px 0px 0px 0px;">
                                        <asp:FileUpload ID="uploader4" runat="server" 
                                            Accept=".jpg,.jpeg,.png,.gif,.pdf" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="20px" TabIndex="12" Width="200px" />
                                    </td>
                                    <td class="style14" style="padding: 5px 0px 0px 0px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style14" style="padding: 8px 0px 0px 0px;">
                                        &nbsp;</td>
                                    <td class="style14" style="padding: 8px 0px 0px 0px;">
                                        <dx:ASPxLabel ID="ASPxLabel27" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Symbol 5">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="style14" colspan="2" style="padding: 8px 0px 0px 0px;">
                                        <asp:FileUpload ID="uploader5" runat="server" 
                                            Accept=".jpg,.jpeg,.png,.gif,.pdf" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="20px" TabIndex="12" Width="200px" />
                                    </td>
                                    <td class="style14" style="padding: 5px 0px 0px 0px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style14" style="padding: 8px 0px 0px 0px;">
                                        &nbsp;</td>
                                    <td class="style14" style="padding: 8px 0px 0px 0px;">
                                        <dx:ASPxLabel ID="ASPxLabel28" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Symbol 6">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="style14" colspan="2" style="padding: 8px 0px 0px 0px;">
                                        <asp:FileUpload ID="uploader6" runat="server" 
                                            Accept=".jpg,.jpeg,.png,.gif,.pdf" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="20px" TabIndex="12" Width="200px" />
                                    </td>
                                    <td class="style14" style="padding: 5px 0px 0px 0px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style14" style="padding: 5px 0px 0px 0px;">
                                        &nbsp;</td>
                                    <td class="style14" style="padding: 5px 0px 0px 0px;">
                                        &nbsp;</td>
                                    <td class="style14" colspan="2" style="padding: 5px 0px 0px 0px;">
                                        <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="ASPxCallback1">
                                            <ClientSideEvents Init="OnEndCallback"/>
                                        </dx:ASPxCallback>
                                    </td>
                                    <td class="style14" style="padding: 5px 0px 0px 0px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 0px 0px 0px; ">
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; ">
                                        &nbsp;</td>
                                    <td class="style2" colspan="3" style="padding: 0px 0px 0px 0px; ">
                                        <dx:ASPxButton ID="btnuploadpopup" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="btnuploadpopup" Height="20px" 
                                            style="float: left; margin-right: 8px" TabIndex="21" Text="Upload" Width="70px">
                                            <ClientSideEvents Click="function(s,e){pcuploadsymbol.Hide();}" />
                                        </dx:ASPxButton>
                                        <dx:ASPxButton ID="btncancleupload" runat="server" AutoPostBack="False" 
                                            Height="20px" style="float: left; margin-right: 8px" TabIndex="22" 
                                            Text="Cancel" Width="70px">
                                            <ClientSideEvents Click="function(s, e) { pcuploadsymbol.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td class="style2" style="padding: 0px 0px 0px 0px; ">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; ">
                                    </td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; ">
                                        &nbsp;</td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; ">
                                    </td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; ">
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

</asp:Content>
