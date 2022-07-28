<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="QCSMasterApproval.aspx.vb" Inherits="PECGI_SPC.QCSMasterApproval" %>
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

//        if (s.cp_viewqcs == 1){
//        txtlineidview.SetText(s.cp_lineid);
//        txtpartidview.SetText(s.cp_partid);
//        txtpartnameview.SetText(s.cp_partname);
//        txtrevnoview.SetText(s.cp_revno);
//        txtrevdateview.SetText(s.cp_revdate);
//        txtrevhistoryview.SetText(s.cp_revhistory);
//        txtpreparedbyview.SetText(s.cp_preparedby);
//        s.cp_viewqcs = null;  
//        }

        if (s.cp_callgrid == 1){
        Grid.PerformCallback('ViewQCS|');  
        s.cp_callgrid = null;
        }

        if (s.cp_viewqcs == 1){
//        alert(s.cp_lineid); 
        window.open('QCSMaster.aspx?LineID=' + s.cp_lineid + '&PartID=' + s.cp_partid + '&PartName=' + s.cp_partname + '&RevNo=' + s.cp_revno, "NewWindow");
        s.cp_viewqcs = null;
        }
}

//function OnEndCallbackGrid(s, e) {
//            if (s.cp_message != "" && s.cp_val == 1) {
//               
//                if (s.cp_type == "Success" && s.cp_val == 1) {
//                    toastr.success(s.cp_message, 'Success');
//                    toastr.options.closeButton = false;
//                    toastr.options.debug = false;
//                    toastr.options.newestOnTop = false;
//                    toastr.options.progressBar = false;
//                    toastr.options.preventDuplicates = true;
//                    toastr.options.onclick = null;
//                    s.cp_val = 0;
//                    s.cp_message = "";
//                }
//                else if (s.cp_type == "Warning" && s.cp_val == 1) {
//                
//                    toastr.warning(s.cp_message, 'Warning');
//                    toastr.options.closeButton = false;
//                    toastr.options.debug = false;
//                    toastr.options.newestOnTop = false;
//                    toastr.options.progressBar = false;
//                    toastr.options.preventDuplicates = true;
//                    toastr.options.onclick = null;
//                    ss.cp_val = 0;
//                    s.cp_message = "";
//                }
//                else if (s.cp_type == "ErrorMsg" && s.cp_val == 1) {
//                    toastr.error(s.cp_message, 'Error');
//                    toastr.options.closeButton = false;
//                    toastr.options.debug = false;
//                    toastr.options.newestOnTop = false;
//                    toastr.options.progressBar = false;
//                    toastr.options.preventDuplicates = true;
//                    toastr.options.onclick = null;
//                    s.cp_val = 0;
//                    s.cp_message = "";
//                }
//            }
//            else if (s.cp_message == "" && s.cp_val == 0) {
//                toastr.options.closeButton = false;
//                toastr.options.debug = false;
//                toastr.options.newestOnTop = false;
//                toastr.options.progressBar = false;
//                toastr.options.preventDuplicates = true;
//                toastr.options.onclick = null;
//            }

//            if (s.cp_viewqcs == 1){
//            txtlineidview.SetText(s.cp_lineid);
//            txtpartidview.SetText(s.cp_partid);
//            txtpartnameview.SetText(s.cp_partname);
//            txtrevnoview.SetText(s.cp_revno);
//            txtrevdateview.SetText(s.cp_revdate);
//            txtrevhistoryview.SetText(s.cp_revhistory);
//            txtpreparedbyview.SetText(s.cp_preparedby);
//            s.cp_viewqcs = null;
//            Grid.PerformCallback('ViewQCS|');  
//        }
//}

function SelectLineID(s, e) {
    var getrev = cborevno.GetText();
//    cbopartid.PerformCallback('Select|' + cbolineid.GetValue());
    cborevno.PerformCallback('Select|' + cbopartid.GetValue() + '|' + cbolineid.GetValue());
//    cbopartid.SetText('');
//    txtpartname.SetText('');
    
    if (getrev == 'ALL' || getrev == 'Latest Rev') {
        cborevno.SetValue(getrev);
    }else{
        cborevno.SetValue('');
    }
}
  
function SelectPartID(s, e) {
    var getrev = cborevno.GetText();
    cbolineid.PerformCallback('Select|' + cbopartid.GetValue());
    txtpartname.SetText(cbopartid.GetSelectedItem().GetColumnText(1));
    

    if (getrev == 'ALL' || getrev == 'Latest Rev') {
        cborevno.SetValue(getrev);
    }else{
        cborevno.SetValue('');
    }
}
function SelectRevNo(s, e) {
//    cboapprovalstatus.PerformCallback('|');
//    var getrev = cborevno.GetText();
//    var getapp = cboapprovalstatus.GetText();
//    cborevno.SetValue(getrev);
//    cboapprovalstatus.SetValue(getapp);
    }

function ClickRefresh(s, e) {
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
        toastr.warning('Please select Part ID!', 'Warning');
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

        if (cboapprovalstatus.GetText() == '' ) {
        toastr.warning('Please select Approval Status!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
        }
        
        GridMenu.PerformCallback('Refresh|' + cbolineid.GetValue() + '|' + cbopartid.GetValue() + '|' + cborevno.GetValue() + '|' + cboapprovalstatus.GetValue());
        btnExcel.SetEnabled(true);
    }

function BtnApproveClick(s, e) {
    var r = confirm("Are you sure want to approve?");
    if (r == true) {
        GridMenu.PerformCallback('Approve|');
    }     
}

function ViewQCS(s, e) {
    var count = GridMenu.GetSelectedRowCount();
    if (count == 0){
        toastr.warning('Please select data!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    }else if(count > 1){
        toastr.warning('Please select only one data!', 'Warning');
        toastr.options.closeButton = false;
        toastr.options.debug = false;
        toastr.options.newestOnTop = false;
        toastr.options.progressBar = false;
        toastr.options.preventDuplicates = true;
        toastr.options.onclick = null;

        e.processOnServer = false;
        return;
    }else{
        GridMenu.PerformCallback('ViewQCS|');  
//        ViewQCS.close();
//        pcLogin.Show();
//    window.open('QCSMaster.aspx?LineID=MGS', "NewWindow");
    } 
    
}

function BtnClearClick(s, e) {
    cbopartid.SetValue('ALL');
    txtpartname.SetValue('ALL');
    cbolineid.SetValue('ALL');
    cborevno.SetValue('Latest Rev');
    cboapprovalstatus.SetValue('Unapproved');
    GridMenu.PerformCallback('ClearGrid|');
//    cbopartid.PerformCallback('Select|' + cbolineid.GetText());
//    cborevno.PerformCallback('Select|' + cbopartid.GetText());
    }

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
//		time.SetText(result.toString() + " sec");
//		label.SetText("Time to retrieve the last data:");
}

function OnInitLineID(s, e) {
    cbolineid.PerformCallback('|' + 'ALL');    
    cbolineid.SetValue('ALL');
    cborevno.PerformCallback('Select|' + 'ALL' + '|' + 'ALL');
}

function OnInitPartID(s, e) {
    cbopartid.SetValue('ALL');
    txtpartname.SetValue('ALL');     
//           
}

function OnInitRevNo(s, e) {
    cborevno.SetValue('Latest Rev');     
    cboapprovalstatus.PerformCallback('|'); 
    cboapprovalstatus.SetValue('Unapproved');
}

function OnInitApprovalStatus(s, e) {
    cboapprovalstatus.SetValue('Unapproved');     
}

	</script>
    <style type="text/css">
        .style1
        {
            width: 151px;
        }
        .style2
        {
            width: 168px;
        }
        .style3
        {
            width: 127px;
        }
        .style5
        {
            width: 69px;
        }
        .style7
        {
            width: 72px;
        }
        .style8
        {
            width: 4px;
        }
        .style9
        {
            width: 62px;
        }
        .style10
        {
            width: 65px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<div style="padding: 0px 5px 5px 5px">
    <table class="auto-style3" style="width: 100%">
        <tr >
            <td style="padding:5px 0px 5px 0px; width: 80px;" class="style7">
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="Date From" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 10px;" class="style7" width="10px">
                &nbsp;</td>
            <td style="padding:5px 0px 5px 0px" class="style7">
                <dx:ASPxDateEdit ID="dtstart" runat="server" Theme="Office2010Black" 
                    Width="100px" AutoPostBack="false"
                        ClientInstanceName="dtstart" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="7">
                        <%--<ClientSideEvents Init="function(s, e){var today = new Date(); dtstart.SetDate(today);}" />--%>
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
            <td style="width: 20px;" class="style7">
                &nbsp;</td>
            <td style="padding:5px 0px 5px 0px; width: 90px;" class="style7">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Part No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width:10px; ">
                &nbsp;</td>
            <td style=" width:100px; padding:5px 0px 0px 0px">
                <dx:ASPxComboBox ID="cbopartid" runat="server" Theme="Office2010Black" TextField="PartName"
                    ClientInstanceName="cbopartid" DropDownStyle="DropDownList" ValueField="PartID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                    Width="150px" TabIndex="2" EnableCallbackMode="true" CallbackPageSize="10">
                    <ClientSideEvents SelectedIndexChanged="SelectPartID" Init="OnInitPartID" BeginCallback="function(s, e) { OnBeginCallbackPartID(); }" EndCallback="function(s, e) { OnEndCallbackPartID(); } "/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Part ID" FieldName="PartID" Width="110px" />
                        <dx:ListBoxColumn Caption="Part Name" FieldName="PartName" Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style=" width:20px; ">
                &nbsp;</td>
            <td style=" width:70px; padding:5px 0px 5px 0px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Line No." Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:10px; ">
                &nbsp;</td>
            <td style="width:100px; padding:5px 0px 0px 0px">
               <dx:ASPxComboBox ID="cbolineid" runat="server" Theme="Office2010Black" TextField="LineName"
                    ClientInstanceName="cbolineid" DropDownStyle="DropDownList" ValueField="LineID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="1">
                    <ClientSideEvents SelectedIndexChanged="SelectLineID" Init="OnInitLineID"/>
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
            <td style=" width:20px">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width: 110px;">
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Approval Status" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
                </td>
            <td style="width: 10px;">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 150px;">
                <dx:ASPxComboBox ID="cboapprovalstatus" runat="server" Theme="Office2010Black" TextField="Status"
                    ClientInstanceName="cboapprovalstatus" DropDownStyle="DropDown" ValueField="Status"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="150px" TabIndex="4">
                    <ClientSideEvents Init="OnInitApprovalStatus" />
                    <Columns>
                        <dx:ListBoxColumn Caption="Status" FieldName="Status" Width="70px"/>
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Width="5px" Paddings-Padding="4px" ></ButtonStyle>
                </dx:ASPxComboBox>
                </td>
            <td style="width: 20px;">
                &nbsp;</td>
            <td style="padding: 3px 0px 0px 0px; width: 150px;">
                &nbsp;</td>
            <td style="padding: 3px 0px 0px 0px; width: 150px;">
                &nbsp;</td>
        </tr>

        <tr>
            <td style=" padding:3px 0px 5px 0px; width: 80px;" class="style7">
                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="To" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width: 10px;" class="style7" width="10px">
                &nbsp;</td>
            <td style=" padding:3px 0px 5px 0px" class="style7">
                <dx:ASPxDateEdit ID="dtend" runat="server" Theme="Office2010Black" 
                    Width="100px" AutoPostBack="false"
                        ClientInstanceName="dtend" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="7">
                        <ClientSideEvents Init="function(s, e){var today = new Date(); dtend.SetDate(today);}" />
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
            <td style=" width: 20px;" class="style7">
                &nbsp;</td>
            <td style=" padding:3px 0px 5px 0px; width: 90px;" class="style7">
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Part Name" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:10px; ">
                &nbsp;</td>
            <td style=" width:100px; padding:3px 0px 5px 0px">
                <dx:ASPxTextBox ID="txtpartname" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtpartname" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="150px" ReadOnly="True" TabIndex="3">
                </dx:ASPxTextBox>
                    
            </td>
            <td style=" width:20px; ">
                &nbsp;</td>
            <td style=" width:70px; padding:3px 0px 5px 0px">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Rev. No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:10px; ">
                &nbsp;</td>
            <td style="width:100px; padding:3px 0px 5px 0px">
                <dx:ASPxComboBox ID="cborevno" runat="server" Theme="Office2010Black" TextField="RevNo"
                    ClientInstanceName="cborevno" DropDownStyle="DropDown" ValueField="RevNo"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="4">
                    <ClientSideEvents SelectedIndexChanged="SelectRevNo" Init="OnInitRevNo"/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Rev No" FieldName="RevNo" Width="70px"/>
                        <dx:ListBoxColumn Caption="Rev Date" FieldName="RevDate" Width="70px"/>
                        <dx:ListBoxColumn Caption="Rev History" FieldName="RevHistory" Width="200px"/>
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Width="5px" Paddings-Padding="4px" ></ButtonStyle>
                </dx:ASPxComboBox>
                    
            </td>
            <td style=" width:20px">
                &nbsp;</td>
            <td style="width:110px; padding:3px 0px 5px 0px">
                &nbsp;</td>
            <td style="width:10px; ">
                &nbsp;</td>
            <td style="width:150px; padding:3px 0px 5px 0px">
                &nbsp;</td>
            <td style="width:20px; ">
                &nbsp;</td>
            <td style="width:150px; padding:3px 0px 5px 0px">
                <dx:ASPxButton ID="btnRefresh" runat="server" Text="Refresh" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnRefresh" Theme="Default" TabIndex="12">
                    <ClientSideEvents Click="ClickRefresh" Init="ClickRefresh"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:150px; padding:3px 0px 0px 0px">
                &nbsp;</td>
        </tr>

        </table>
</div>

<div style="padding: 2px 5px 5px 5px">
        <dx:ASPxGridView ID="GridMenu" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridMenu"
            EnableTheming="True" KeyFieldName="PartID;LineID;RevNo" Theme="Office2010Black" 
            OnStartRowEditing="GridMenu_StartRowEditing" OnRowValidating="GridMenu_RowValidating"
            OnRowInserting="GridMenu_RowInserting" OnRowDeleting="GridMenu_RowDeleting" 
            OnAfterPerformCallback="GridMenu_AfterPerformCallback" Width="100%" 
            Font-Names="Segoe UI" Font-Size="9pt">
            <ClientSideEvents EndCallback="OnEndCallback" />
            <Columns>
                <%--<dx:GridViewCommandColumn VisibleIndex="1" ShowSelectButton="true">
                    </dx:GridViewCommandColumn>--%>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" 
                    ShowClearFilterButton="true" VisibleIndex="1" SelectAllCheckboxMode="Page" 
                    Width="30px" FixedStyle="Left" />
                <dx:GridViewDataTextColumn Caption="Part No" FieldName="PartID" 
                    VisibleIndex="2" Width="110px" FixedStyle="Left">
                    <PropertiesTextEdit ClientInstanceName="PartID">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Part Name" FieldName="PartName" 
                    VisibleIndex="3" Width="145px" FixedStyle="Left">
                    <PropertiesTextEdit ClientInstanceName="PartName">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Line No" FieldName="LineID" 
                    VisibleIndex="4" Width="60px" FixedStyle="Left">
                    <PropertiesTextEdit ClientInstanceName="LineID">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Rev No" FieldName="RevNo" VisibleIndex="5" 
                    Width="50px" FixedStyle="Left">
                    <PropertiesTextEdit ClientInstanceName="RevNo">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Rev Date" FieldName="RevDate" 
                    VisibleIndex="6" Width="90px">
                    <PropertiesTextEdit DisplayFormatString="dd-MMM-yyyy" ClientInstanceName="RevDate">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Revision History" FieldName="RevHistory" 
                    VisibleIndex="7" Width="200px">
                    <PropertiesTextEdit ClientInstanceName="RevHistory"></PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Prepared By" FieldName="PreparedBy" 
                    VisibleIndex="8" Width="75px">
                    <PropertiesTextEdit ClientInstanceName="PreparedBy"></PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewBandColumn Caption="Approval" VisibleIndex="11">
                    <Columns>
                        <dx:GridViewBandColumn Caption="Line Leader" VisibleIndex="0">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Date" FieldName="ApprovalDate1" 
                                    VisibleIndex="0" Width="85px">
                                    <PropertiesTextEdit DisplayFormatString="dd-MMM-yyyy" ClientInstanceName="ApprovalDate1">
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="PIC" FieldName="ApprovalPIC1" 
                                    VisibleIndex="1" Width="85px">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="Line Foreman" VisibleIndex="1">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Date" FieldName="ApprovalDate2" 
                                    VisibleIndex="0" Width="85px">
                                    <PropertiesTextEdit DisplayFormatString="dd-MMM-yyyy" ClientInstanceName="ApprovalDate2">
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="PIC" FieldName="ApprovalPIC2" 
                                    VisibleIndex="1" Width="85px">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="QE Leader" VisibleIndex="2">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Date" FieldName="ApprovalDate3" 
                                    VisibleIndex="0" Width="85px">
                                    <PropertiesTextEdit DisplayFormatString="dd-MMM-yyyy" ClientInstanceName="ApprovalDate3">
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="PIC" FieldName="ApprovalPIC3" 
                                    VisibleIndex="1" Width="85px">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </dx:GridViewBandColumn>
                
            </Columns>
        <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" />
        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
        <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true" PageSizeItemSettings-Visible="true">
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
            <CommandColumnItem ForeColor="SteelBlue">
            </CommandColumnItem>
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
            <td style="width:250px; padding:5px 0px 5px 0px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style=" padding: 5px 0px 5px 0px; width:250px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style=" padding: 5px 0px 5px 0px; width:250px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style=" padding: 5px 0px 5px 0px; width:250px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style=" padding: 5px 0px 5px 0px; width:250px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="width:10px; padding:5px 0px 5px 0px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style=" padding: 5px 0px 5px 0px; width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td class="style1" 
                style="padding: 5px 0px 5px 0px; border-top: 1px solid silver; width: 10px;">
                &nbsp;</td>
            <td class="style1" 
                style="padding: 5px 0px 5px 0px; border-top: 1px solid silver; width: 10px;">
                &nbsp;</td>
            <td class="style1" 
                style="padding: 5px 0px 5px 0px; border-top: 1px solid silver; width: 10px;">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:10px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnClear" runat="server" Text="Clear" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnClear" Theme="Default" TabIndex="13">
                    <ClientSideEvents Click="BtnClearClick"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:10px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnExcel" Theme="Default" TabIndex="14" 
                    ClientEnabled="False">
                    <ClientSideEvents Click="function(s, e) {
                            GridMenu.PerformCallback('excel|' + cbolineid.GetValue() + '|' + cbopartid.GetValue() + '|' + txtpartname.GetValue() + '|' + cborevno.GetValue() + '|' + cboapprovalstatus.GetValue());
                        }" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:10px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnViewQCS" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnViewQCS" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="View SQC Master" Theme="Default" UseSubmitBehavior="false" 
                    Width="90px" TabIndex="15" ClientEnabled="true">
                    <ClientSideEvents Click="ViewQCS" /> <%--CheckedChanged="function(s,e){Grid.PerformCallback('ViewQCS|');}"--%>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="width: 10px; border-top: 1px solid silver">
            </td>
            <td style="width:10px; padding:5px 0px 5px 0px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnApprove" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnApprove" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Approve" Theme="Default" UseSubmitBehavior="false" 
                    Width="90px" TabIndex="16">
                    <ClientSideEvents Click="BtnApproveClick"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
        </tr>
        </table>
</div>
<div>
   <dx:ASPxPopupControl ID="pcLogin" runat="server" CloseAction="CloseButton" ClientInstanceName="pcLogin"
        HeaderText="View QCS" AllowDragging="True" 
        PopupAnimationType="Fade" Height="500px" 
        Theme="Office2010Black" Width="977px" Modal="True" 
        PopupHorizontalOffset="295" PopupVerticalOffset="110">
        <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />
        <ModalBackgroundStyle BackColor="White" CssClass="&quot;noBackground&quot;">
        </ModalBackgroundStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK" Height="220px">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <div style="padding: 0px 5px 5px 5px">
    <table class="auto-style3" style="width: 100%">
        <tr >
            <td style="width:60px; padding:5px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Line No." Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" padding:5px 0px 0px 0px" class="style2">
                <dx:ASPxTextBox ID="txtlineidview" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtlineidview" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" ReadOnly="false" 
                    TabIndex="3" Theme="Office2010Black" Width="100px">
                </dx:ASPxTextBox>
            </td>
            <td style=" padding:5px 0px 0px 0px" class="style5">
                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Rev. No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding:5px 0px 0px 0px" class="style3">
                <dx:ASPxTextBox ID="txtrevnoview" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtrevnoview" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" ReadOnly="True" 
                    TabIndex="3" Theme="Office2010Black" Width="100px">
                </dx:ASPxTextBox>
            </td>
            <td style=" padding: 5px 0px 0px 0px; " class="style7">
                <dx:ASPxLabel ID="lblpreparedby" runat="server" Text="Prepared By" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" padding: 5px 0px 0px 0px; " class="style8">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; " class="style12">
                <dx:ASPxTextBox ID="txtpreparedbyview" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtpreparedbyview" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="176px" TabIndex="7" ReadOnly="True">
                </dx:ASPxTextBox>
                </td>
            <td style="padding: 5px 0px 0px 0px;
                align="center" class="style9">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px;"center" class="style10">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 75px; align="center">
                &nbsp;</td>
        </tr>

        <tr>
            <td style=" width:60px; padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Part No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" padding:3px 0px 0px 0px" class="style2">
                <dx:ASPxTextBox ID="txtpartidview" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtpartidview" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" ReadOnly="True" 
                    TabIndex="3" Theme="Office2010Black" Width="150px">
                </dx:ASPxTextBox>
            </td>
            <td style=" padding:3px 0px 0px 0px" class="style5">
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Rev. Date" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding:3px 0px 0px 0px" class="style3">
                <dx:ASPxTextBox ID="txtrevdateview" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtrevdateview" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" ReadOnly="True" 
                    TabIndex="3" Theme="Office2010Black" Width="100px">
                </dx:ASPxTextBox>
                    
            </td>
            <td style=" padding: 3px 0px 0px 0px; " class="style7">
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
                &nbsp;</td>
            <td style=" padding: 3px 0px 0px 0px; " class="style8">
                &nbsp;</td>
            <td style="padding:3px 0px 0px 0px" class="style12" xml:lang="100px">
                <%--<dx:ASPxTextBox ID="txtapprovedby" runat="server" BackColor="White" 
                    ClientInstanceName="txtapprovedby" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="150px" TabIndex="8" ReadOnly="True">
                </dx:ASPxTextBox>--%>
                &nbsp;</td>
            <td style="padding: 3px 0px 0px 5px; " class="style9">
                &nbsp;</td>
            <td style="padding: 3px 0px 0px 0px; " align="center" 
                class="style10">
                &nbsp;</td>
            <td style="padding: 3px 0px 0px 0px; width: 10px;" align="center">
                &nbsp;</td>
        </tr>

        <tr>
            <td style=" width:60px; padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Part Name" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" padding:3px 0px 0px 0px" class="style2">
                <dx:ASPxTextBox ID="txtpartnameview" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtpartnameview" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="150px" ReadOnly="True" TabIndex="3">
                </dx:ASPxTextBox>
            </td>
            <td style=" padding:3px 0px 0px 0px" class="style5">
                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Rev. History" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="padding:3px 0px 0px 0px" colspan="4">
                <dx:ASPxTextBox ID="txtrevhistoryview" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtrevhistoryview" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="195px" TabIndex="6" ReadOnly="True">
                </dx:ASPxTextBox>
            </td>
            <td style="padding: 3px 0px 0px 5px; " class="style9">
                &nbsp;</td>
            <td style="padding: 3px 0px 0px 0px; " align="center" 
                class="style10">
                &nbsp;</td>
            <td style="padding: 3px 0px 0px 0px; width: 10px;" align="center">
                &nbsp;</td>
        </tr>

        <tr>
            <td style=" width:60px; padding:3px 0px 5px 0px">
                &nbsp;</td>
            <td style=" padding:3px 0px 5px 0px" class="style2">
                &nbsp;</td>
            <td style=" padding:3px 0px 5px 0px" class="style5">
                &nbsp;</td>
            <td style="padding:3px 0px 5px 0px" class="style3">
                &nbsp;</td>
            <td style=" padding: 3px 0px 5px 0px; " class="style7">
                &nbsp;</td>
            <td style=" padding: 3px 0px 5px 0px; " class="style8">
                &nbsp;</td>
            <td style="padding:3px 0px 5px 0px" class="style12">
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
                            <div>
                                <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" 
                                    ClientInstanceName="Grid" EnableTheming="True" Font-Names="Segoe UI" 
                                    Font-Size="9pt" KeyFieldName="ItemID" 
                                   Theme="Office2010Black" OnAfterPerformCallback="Grid_AfterPerformCallback"
                                    Width="100%">
                                    <ClientSideEvents EndCallback="OnEndCallback" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="No" FieldName="ItemID" 
                                            ShowInCustomizationForm="True" VisibleIndex="1" Width="50px">
                                            <PropertiesTextEdit ClientInstanceName="ItemID" MaxLength="15" Width="50px">
                                                <Style HorizontalAlign="Left">
                                                </Style>
                                            </PropertiesTextEdit>
                                            <EditFormSettings VisibleIndex="1" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            <Paddings PaddingLeft="6px" />
                                            </HeaderStyle>
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Process ID" FieldName="ProcessID" 
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="2" Width="100px">
                                            <PropertiesComboBox ClientInstanceName="ProcessID" 
                                                IncrementalFilteringMode="StartsWith" 
                                                TextField="ProcessID" TextFormatString="{0}" ValueField="ProcessID" 
                                                Width="100px">
                                                <ClientSideEvents SelectedIndexChanged="function(){ProcessName.SetText(ProcessID.GetSelectedItem().GetColumnText(1));}" />
                                                <Columns>
                                                    <dx:ListBoxColumn Caption="Process ID" FieldName="ProcessID" Width="62px" />
                                                    <dx:ListBoxColumn Caption="Process Name" FieldName="ProcessName" 
                                                        Width="200px" />
                                                </Columns>
                                                <ItemStyle Height="10px">
                                                <Paddings Padding="4px" />
                                                </ItemStyle>
                                                <ButtonStyle Width="5px">
                                                    <Paddings Padding="2px" />
                                                </ButtonStyle>
                                            </PropertiesComboBox>
                                            <EditFormSettings Visible="True" VisibleIndex="2" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            <Paddings PaddingLeft="5px" />
                                            </HeaderStyle>
                                            <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                                            </CellStyle>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="K Point" FieldName="KPointStatus" 
                                            ShowInCustomizationForm="True" VisibleIndex="4" Width="60px">
                                            <PropertiesComboBox ClientInstanceName="KPointStatus" 
                                                DisplayFormatInEditMode="True" IncrementalFilteringMode="StartsWith" 
                                                TextField="KPointStatus" TextFormatString="{0}" ValueField="KPointStatus" 
                                                Width="60px">
                                                <Items>
                                                    <dx:ListEditItem Text="B" Value="B" />
                                                    <dx:ListEditItem Text="K" Value="K" />
                                                </Items>
                                                <ItemStyle Height="10px">
                                                <Paddings Padding="4px" />
                                                </ItemStyle>
                                                <ButtonStyle Width="5px">
                                                    <Paddings Padding="2px" />
                                                </ButtonStyle>
                                            </PropertiesComboBox>
                                            <EditFormSettings VisibleIndex="4" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                                            <Paddings PaddingLeft="2px" />
                                            </HeaderStyle>
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            </CellStyle>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataTextColumn Caption="Item" FieldName="Item" 
                                            ShowInCustomizationForm="True" VisibleIndex="5" Width="150px">
                                            <PropertiesTextEdit ClientInstanceName="Item" MaxLength="50" Width="150px">
                                                <Style HorizontalAlign="Left">
                                                </Style>
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" />
                                            <EditFormSettings VisibleIndex="5" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Standard" FieldName="Standard" 
                                            ShowInCustomizationForm="True" VisibleIndex="7" Width="150px">
                                            <PropertiesTextEdit ClientInstanceName="Standard" MaxLength="50" Width="150px">
                                                <Style HorizontalAlign="Left">
                                                </Style>
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" />
                                            <EditFormSettings VisibleIndex="7" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Range" FieldName="Range" 
                                            ShowInCustomizationForm="True" VisibleIndex="8" Width="150px">
                                            <PropertiesTextEdit ClientInstanceName="Range" MaxLength="15" Width="150px">
                                                <Style HorizontalAlign="Left">
                                                </Style>
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" />
                                            <EditFormSettings Visible="False" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="ValueType" FieldName="ValueType" 
                                            ShowInCustomizationForm="True" VisibleIndex="6" Width="75px">
                                            <PropertiesComboBox ClientInstanceName="ValueType" 
                                                DisplayFormatInEditMode="True" IncrementalFilteringMode="StartsWith" 
                                                TextField="ValueType" TextFormatString="{0}" ValueField="ValueType" 
                                                Width="70px">
                                                <ClientSideEvents Init="SelectValueTypeInit" 
                                                    SelectedIndexChanged="SelectValueType1" />
                                                <Items>
                                                    <dx:ListEditItem Text="Numeric" Value="N" />
                                                    <dx:ListEditItem Text="Text" Value="T" />
                                                </Items>
                                                <ItemStyle Height="10px">
                                                <Paddings Padding="4px" />
                                                </ItemStyle>
                                                <ButtonStyle Width="5px">
                                                    <Paddings Padding="2px" />
                                                </ButtonStyle>
                                            </PropertiesComboBox>
                                            <EditFormSettings VisibleIndex="6" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                                            <Paddings PaddingLeft="2px" />
                                            </HeaderStyle>
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            </CellStyle>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataTextColumn Caption="NumRangeStart" FieldName="NumRangeStart" 
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="9" Width="150px">
                                            <PropertiesTextEdit ClientInstanceName="NumRangeStart" DisplayFormatString="d">
                                                <MaskSettings Mask="&lt;0..999999g&gt;.&lt;00..9999&gt;" />
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" />
                                            <EditFormSettings Visible="True" VisibleIndex="9" />
                                            <EditCellStyle CssClass="NumRangeStart">
                                            </EditCellStyle>
                                            <EditFormCaptionStyle CssClass="NumRangeStart">
                                            </EditFormCaptionStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="NumRangeEnd" FieldName="NumRangeEnd" 
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="10" Width="150px">
                                            <PropertiesTextEdit ClientInstanceName="NumRangeEnd" DisplayFormatString="c">
                                                <MaskSettings Mask="&lt;0..999999g&gt;.&lt;00..9999&gt;" />
                                            </PropertiesTextEdit>
                                            <EditFormSettings Visible="True" VisibleIndex="10" />
                                            <EditCellStyle CssClass="NumRangeEnd">
                                            </EditCellStyle>
                                            <EditFormCaptionStyle CssClass="NumRangeEnd">
                                            </EditFormCaptionStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="TextRange" FieldName="TextRange" 
                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="11" Width="150px">
                                            <PropertiesTextEdit ClientInstanceName="TextRange" MaxLength="15" Width="150px">
                                                <Style HorizontalAlign="Left">
                                                </Style>
                                            </PropertiesTextEdit>
                                            <Settings AutoFilterCondition="Contains" />
                                            <EditFormSettings Caption="TextRange" Visible="True" VisibleIndex="11" />
                                            <EditCellStyle CssClass="TextRange">
                                            </EditCellStyle>
                                            <EditFormCaptionStyle CssClass="TextRange">
                                            </EditFormCaptionStyle>
                                            <HeaderStyle CssClass="TextRange" HorizontalAlign="Center" 
                                                VerticalAlign="Middle">
                                            <Paddings PaddingLeft="5px" />
                                            </HeaderStyle>
                                            <CellStyle CssClass="TextRange" HorizontalAlign="Left" VerticalAlign="Middle">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Measuring Instrument" 
                                            FieldName="MeasuringInstrument" ShowInCustomizationForm="True" 
                                            VisibleIndex="12" Width="150px">
                                            <PropertiesComboBox ClientInstanceName="MeasuringInstrument" 
                                                 DisplayFormatInEditMode="True" 
                                                DropDownStyle="DropDown" IncrementalFilteringMode="Contains" 
                                                TextField="MeasuringInstrument" TextFormatString="{0}" 
                                                ValueField="MeasuringInstrument" Width="150px">
                                                <Columns>
                                                    <dx:ListBoxColumn FieldName="MeasuringInstrument" Width="68px" />
                                                </Columns>
                                                <ItemStyle Height="10px">
                                                <Paddings Padding="4px" />
                                                </ItemStyle>
                                                <ButtonStyle Width="5px">
                                                    <Paddings Padding="4px" />
                                                </ButtonStyle>
                                            </PropertiesComboBox>
                                            <EditFormSettings VisibleIndex="12" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                            </CellStyle>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataTextColumn Caption="Process" FieldName="ProcessName" 
                                            ShowInCustomizationForm="True" VisibleIndex="3">
                                            <PropertiesTextEdit ClientInstanceName="ProcessName">
                                            </PropertiesTextEdit>
                                            <EditFormSettings VisibleIndex="3" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" />
                                    <SettingsPager AlwaysShowPager="True">
                                        <PageSizeItemSettings Visible="True">
                                        </PageSizeItemSettings>
                                    </SettingsPager>
                                    <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm">
                                    </SettingsEditing>
                                    <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="250" 
                                        VerticalScrollBarMode="Auto" />
                                    <SettingsText ConfirmDelete="Are you sure want to delete ?" />
                                    <SettingsPopup>
                                        <EditForm HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" 
                                            Width="320px" />
                                    </SettingsPopup>
                                    <Styles>
                                        <Header>
                                            <Paddings Padding="2px" />
                                        </Header>
                                        <CommandColumnItem ForeColor="SteelBlue">
                                        </CommandColumnItem>
                                        <EditFormColumnCaption Font-Names="Segoe UI" Font-Size="9pt">
                                            <Paddings PaddingBottom="5px" PaddingLeft="15px" PaddingRight="15px" 
                                                PaddingTop="5px" />
                                        </EditFormColumnCaption>
                                    </Styles>
                                    <Templates>
                                        <EditForm>
                                            <div style="padding: 15px 15px 15px 15px">
                                                <dx:ContentControl ID="ContentControl2" runat="server">
                                                    <dx:ASPxGridViewTemplateReplacement ID="Editors0" runat="server" 
                                                        ReplacementType="EditFormEditors" />
                                                </dx:ContentControl>
                                            </div>
                                            <div style="text-align: left; padding: 5px 5px 5px 15px">
                                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton0" runat="server" 
                                                    ReplacementType="EditFormUpdateButton" />
                                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton0" runat="server" 
                                                    ReplacementType="EditFormCancelButton" />
                                            </div>
                                        </EditForm>
                                    </Templates>
                                </dx:ASPxGridView>
                            
                            </div>
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
</asp:Content>
