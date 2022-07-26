<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ApprovalSetting.aspx.vb" Inherits="PECGI_SPC.ApprovalSetting" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script type="text/javascript">
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
            }

    function SelectQELeader1(s, e) {
        if (cboqeleader1.GetValue() == cboqeleader2.GetValue() && cboqeleader1.GetValue() != null) {

            toastr.warning('QE Leader 1 Cannot be Equal to QE Leader 2', 'Warning');
            toastr.options.closeButton = false;
            toastr.options.debug = false;
            toastr.options.newestOnTop = false;
            toastr.options.progressBar = false;
            toastr.options.preventDuplicates = true;
            toastr.options.onclick = null;

            e.processOnServer = false;

            cboqeleader1.SetText('');
            return;
        }

    
    }

    function SelectQELeader2(s, e) {
        if (cboqeleader2.GetValue() == cboqeleader1.GetValue() && cboqeleader2.GetValue() != null) {
            toastr.warning('QE Leader 2 Cannot be Equal to QE Leader 1', 'Warning');
            toastr.options.closeButton = false;
            toastr.options.debug = false;
            toastr.options.newestOnTop = false;
            toastr.options.progressBar = false;
            toastr.options.preventDuplicates = true;
            toastr.options.onclick = null;

            e.processOnServer = false;

            cboqeleader2.SetText('');
            return;
        }
    }

    function SelectSectionHead1(s, e) {
        if (cbosectionhead1.GetValue() == cbosectionhead2.GetValue() && cbosectionhead1.GetValue() != null) {
            toastr.warning('QE Section Head 1 Cannot be Equal to QE Section Head 2', 'Warning');
            toastr.options.closeButton = false;
            toastr.options.debug = false;
            toastr.options.newestOnTop = false;
            toastr.options.progressBar = false;
            toastr.options.preventDuplicates = true;
            toastr.options.onclick = null;

            e.processOnServer = false;

            cbosectionhead1.SetText('');
            return;
        }
    }

    function SelectSectionHead2(s, e) {
        if (cbosectionhead2.GetValue() == cbosectionhead1.GetValue() && cbosectionhead2.GetValue() != null) {
            toastr.warning('QE Section Head 2 Cannot be Equal to QE Section Head 1', 'Warning');
            toastr.options.closeButton = false;
            toastr.options.debug = false;
            toastr.options.newestOnTop = false;
            toastr.options.progressBar = false;
            toastr.options.preventDuplicates = true;
            toastr.options.onclick = null;

            e.processOnServer = false;

            cbosectionhead2.SetText('');
            return;
        }
    }

    function ClickSave(s, e) {
        if (cboqeleader1.GetValue() == null) {
            toastr.warning('Please Select QE Leader 1!', 'Warning');
            toastr.options.closeButton = false;
            toastr.options.debug = false;
            toastr.options.newestOnTop = false;
            toastr.options.progressBar = false;
            toastr.options.preventDuplicates = true;
            toastr.options.onclick = null;

            e.processOnServer = false;
            return;
        }
        if (cboqeleader2.GetValue() == null) {
            toastr.warning('Please Select QE Leader 2!', 'Warning');
            toastr.options.closeButton = false;
            toastr.options.debug = false;
            toastr.options.newestOnTop = false;
            toastr.options.progressBar = false;
            toastr.options.preventDuplicates = true;
            toastr.options.onclick = null;

            e.processOnServer = false;
            return;
        }
        if (cbosectionhead1.GetValue() == null) {
            toastr.warning('Please Select QE Section Head 1!', 'Warning');
            toastr.options.closeButton = false;
            toastr.options.debug = false;
            toastr.options.newestOnTop = false;
            toastr.options.progressBar = false;
            toastr.options.preventDuplicates = true;
            toastr.options.onclick = null;

            e.processOnServer = false;
            return;
        }
        if (cbosectionhead2.GetValue() == null) {
            toastr.warning('Please Select QE Section Head 2!', 'Warning');
            toastr.options.closeButton = false;
            toastr.options.debug = false;
            toastr.options.newestOnTop = false;
            toastr.options.progressBar = false;
            toastr.options.preventDuplicates = true;
            toastr.options.onclick = null;

            e.processOnServer = false;
            return;
        }

        cbSave.PerformCallback();
    }
        </script>
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <table style="width:100%;">
        <tr>
            <td style="padding: 5px 0px 0px 0px; width:100px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width:100px">
                &nbsp;</td>
            <td style="width:80px; padding:5px 0px 0px 0px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 160px">
                &nbsp;</td>
            <td class="style1" style="padding: 5px 0px 0px 0px; width: 100px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width:100px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="padding: 5px 0px 0px 0px;" class="style2">
                </td>
            <td style="padding: 5px 0px 0px 0px;" class="style2">
                </td>
            <td style="padding: 5px 0px 0px 0px;" class="style3">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="QE Leader 1" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding: 5px 0px 0px 0px;" class="style4">
               <dx:ASPxComboBox ID="cboqeleader1" runat="server" Theme="Office2010Black" TextField="UserName"
                    ClientInstanceName="cboqeleader1" DropDownStyle="DropDownList" ValueField="UserID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="150px">
                    <ClientSideEvents SelectedIndexChanged="SelectQELeader1"/>
                    <Columns>
                        <dx:ListBoxColumn Caption="User ID" FieldName="UserID" Width="100px" />
                        <dx:ListBoxColumn Caption="User Name" FieldName="UserName" Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td class="style2" style="padding: 5px 0px 0px 0px;">
                </td>
            <td style="padding: 5px 0px 0px 0px;" class="style2">
                </td>
        </tr>
        <tr>
            <td style="padding: 5px 0px 0px 0px; width:100px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width:100px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width:80px">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="QE Leader 2" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding: 5px 0px 0px 0px; width: 160px">
            <dx:ASPxComboBox ID="cboqeleader2" runat="server" Theme="Office2010Black" TextField="UserName"
                    ClientInstanceName="cboqeleader2" DropDownStyle="DropDownList" ValueField="UserID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="150px">
                    <Columns>
                        <dx:ListBoxColumn Caption="User ID" FieldName="UserID" Width="100px" />
                        <dx:ListBoxColumn Caption="User Name" FieldName="UserName" Width="250px" />
                    </Columns>
                    <ClientSideEvents SelectedIndexChanged="SelectQELeader2"/>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
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
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="QE Section Head 1" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding: 5px 0px 0px 0px; width: 160px">
               <dx:ASPxComboBox ID="cbosectionhead1" runat="server" Theme="Office2010Black" TextField="UserName"
                    ClientInstanceName="cbosectionhead1" DropDownStyle="DropDownList" ValueField="UserID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="150px">
                    <Columns>
                        <dx:ListBoxColumn Caption="User ID" FieldName="UserID" Width="100px" />
                        <dx:ListBoxColumn Caption="User Name" FieldName="UserName" Width="250px" />
                    </Columns>
                    <ClientSideEvents SelectedIndexChanged="SelectSectionHead1"/>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
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
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="QE Section Head 2" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding: 5px 0px 0px 0px; width: 160px">
               <dx:ASPxComboBox ID="cbosectionhead2" runat="server" Theme="Office2010Black" TextField="UserName"
                    ClientInstanceName="cbosectionhead2" DropDownStyle="DropDownList" ValueField="UserID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="150px">
                    <Columns>
                        <dx:ListBoxColumn Caption="User ID" FieldName="UserID" Width="100px" />
                        <dx:ListBoxColumn Caption="User Name" FieldName="UserName" Width="250px" />
                    </Columns>
                    <ClientSideEvents SelectedIndexChanged="SelectSectionHead2"/>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
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
            <td style="padding: 5px 0px 0px 0px; width: 160px">
                <dx:ASPxButton ID="btnSave" runat="server" Text="Save" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="100px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnSave" Theme="Default">
                    <ClientSideEvents Click="ClickSave"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td class="style1" style="padding: 5px 0px 0px 0px; width: 100px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 100px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="padding: 5px 0px 0px 0px; " class="style1">
                </td>
            <td style="padding: 5px 0px 0px 0px; " class="style1">
                </td>
            <td style="padding: 5px 0px 0px 0px; " class="style1">
                </td>
            <td style="padding: 5px 0px 0px 0px; " class="style1">
                &nbsp;</td>
            <td class="style1" style="padding: 5px 0px 0px 0px; ">
                </td>
            <td style="padding: 5px 0px 0px 0px; " class="style1">
                </td>
        </tr>
    </table>

<dx:ASPxCallback ID="cbSave" runat="server" ClientInstanceName="cbSave">
    <ClientSideEvents EndCallback="OnEndCallback" />                                           
</dx:ASPxCallback>

</asp:Content>
