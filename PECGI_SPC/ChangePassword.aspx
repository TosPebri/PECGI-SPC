<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ChangePassword.aspx.vb" Inherits="PECGI_SPC.ChangePassword" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script type="text/javascript">
            function MessageError(s, e) {
                if (s.cpMessage == "Update data successful.") {
                    toastr.success(s.cpMessage, 'Success');
                    toastr.options.closeButton = false;
                    toastr.options.debug = false;
                    toastr.options.newestOnTop = false;
                    toastr.options.progressBar = false;
                    toastr.options.preventDuplicates = true;
                    toastr.options.onclick = null;
                } else {
                    toastr.warning(s.cpMessage, 'Warning');
                    toastr.options.closeButton = false;
                    toastr.options.debug = false;
                    toastr.options.newestOnTop = false;
                    toastr.options.progressBar = false;
                    toastr.options.preventDuplicates = true;
                    toastr.options.onclick = null;
                }
            }
        </script>
        <style type="text/css">
            .style1
            {
                height: 25px;
            }
        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <table style="width: 100%;">
        <tr style="height:25px">
            <td style="width:160px">
                &nbsp;</td>
            <td style="width:160px">
                &nbsp;</td>
            <td style="width:160px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Names="Segoe UI" Font-Size="10pt" Text="Current Password">
                </dx:ASPxLabel>
            </td>
            <td style="width:20px"> &nbsp;</td>
            <td style="width:50px"> 
                <dx:ASPxTextBox ID="txtCurrentPassword" runat="server" Font-Names="Verdana" Width="200px" ClientInstanceName="txtCurrentPassword" Password="True">
                </dx:ASPxTextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr style="height:25px">
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Names="Segoe UI" Font-Size="10pt" Text="New Password">
                </dx:ASPxLabel>
            </td>
            <td>&nbsp;</td>
            <td>
                <dx:ASPxTextBox ID="txtNewPassword" runat="server" Font-Names="Verdana" Width="200px" ClientInstanceName="txtNewPassword" Password="True">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxCallback ID="cbProgress" runat="server" ClientInstanceName="cbProgress">
                    <ClientSideEvents CallbackComplete="MessageError" />                                           
                </dx:ASPxCallback>
            </td>
        </tr>
        <tr style="height:25px">
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Names="Segoe UI" Font-Size="10pt" Text="Confirm New Password">
                </dx:ASPxLabel>
            </td>
            <td>&nbsp;</td>
            <td>
                <dx:ASPxTextBox ID="txtConfirmPassword" runat="server" Font-Names="Verdana" Width="200px" ClientInstanceName="txtConfirmPassword" Password="True">
                </dx:ASPxTextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>

        <tr style="height:25px">
            <td colspan="6"></td>
        </tr>



        <tr>
            <td style="width:100px">
                &nbsp;</td>
            <td style="width:100px">
                &nbsp;</td>
            <td style="width:100px">
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width:100px">
                &nbsp;</td>
            <td style="width:100px">
                &nbsp;</td>
            <td style="width:100px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                <dx:ASPxButton ID="btnClear" runat="server" Text="Clear" AutoPostBack="False" Height="16px" Width="80px" Font-Names="Segoe UI" Font-Size="10pt" UseSubmitBehavior="False">
                    <ClientSideEvents Click="function(s, e) {
	                    txtCurrentPassword.SetText('');
	                    txtNewPassword.SetText('');
	                    txtConfirmPassword.SetText('');
                        txtCurrentPassword.Focus();
                    }" />                
                </dx:ASPxButton>
                <dx:ASPxButton ID="btnSubmit" runat="server" Text="Submit" Height="16px" Width="80px" Font-Names="Segoe UI" Font-Size="10pt" AutoPostBack="False"
                    UseSubmitBehavior="False" ClientInstanceName="btnSubmit">
                    <ClientSideEvents Click="function(s, e) {
                    
		                if (txtCurrentPassword.GetText() == ''){
                            toastr.warning('Please Insert Current Password!', 'Warning');
                            toastr.options.closeButton = false;
                            toastr.options.debug = false;
                            toastr.options.newestOnTop = false;
                            toastr.options.progressBar = false;
                            toastr.options.preventDuplicates = true;
                            toastr.options.onclick = null;
		                    e.processOnServer = false;
                            return;
	                    }          
                        if (txtNewPassword.GetText() == ''){
                            toastr.warning('Please Insert New Password!', 'Warning');
                            toastr.options.closeButton = false;
                            toastr.options.debug = false;
                            toastr.options.newestOnTop = false;
                            toastr.options.progressBar = false;
                            toastr.options.preventDuplicates = true;
                            toastr.options.onclick = null;
		                    e.processOnServer = false;
                            return;
	                    }      
                        if (txtConfirmPassword.GetText() == ''){
                            toastr.warning('Please Confirm New Password!', 'Warning');
                            toastr.options.closeButton = false;
                            toastr.options.debug = false;
                            toastr.options.newestOnTop = false;
                            toastr.options.progressBar = false;
                            toastr.options.preventDuplicates = true;
                            toastr.options.onclick = null;
		                    e.processOnServer = false;
                            return;
	                    }
                        if (txtNewPassword.GetText() != txtConfirmPassword.GetText()){
                            toastr.warning('New Password and Confirm Password do not match!', 'Warning');
                            toastr.options.closeButton = false;
                            toastr.options.debug = false;
                            toastr.options.newestOnTop = false;
                            toastr.options.progressBar = false;
                            toastr.options.preventDuplicates = true;
                            toastr.options.onclick = null;
		                    e.processOnServer = false;
                            return;
	                    }
                         cbProgress.PerformCallback();                             
                        }" />
                </dx:ASPxButton>
            </td>
            <td style="width:100px">
                &nbsp;</td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width:100px">
                &nbsp;</td>
            <td style="width:100px">
                &nbsp;</td>
            <td style="width:100px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
