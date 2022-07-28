<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="QCSResultMonitoring.aspx.vb" Inherits="PECGI_SPC.QCSResultMonitoring" %>
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
        else if (s.cp_type == "Warning" && s.cp_val == 1) {
            toastr.warning(s.cp_message, 'Warning');
            toastr.options.closeButton = false;
            toastr.options.debug = false;
            toastr.options.newestOnTop = false;
            toastr.options.progressBar = false;
            toastr.options.preventDuplicates = true;
            toastr.options.onclick = null;
            s.cp_val = 0;
            s.cp_message = "";
            }
        else if (s.cp_message == "" && s.cp_val == 0) {
            toastr.options.closeButton = false;
            toastr.options.debug = false;
            toastr.options.newestOnTop = false;
            toastr.options.progressBar = false;
            toastr.options.preventDuplicates = true;
            toastr.options.onclick = null;
            }
}//end callback

if (s.cp_viewqcs == 1){ 
        window.open('QCSResultInquiry.aspx?Date=' + s.cp_date + '&LineID=' + s.cp_lineid + '&SubLineID=' + s.cp_sublineid + '&PartID=' + s.cp_partid + '&PartName=' + s.cp_partname, "NewWindow");
        s.cp_viewqcs = null;
    }

if(s.cp_clearsum == 1){
  lblng.SetText('0');
  lblnp.SetText('0');
  lblok.SetText('0');
  lblincomplete.SetText('0');
  lbltotal.SetText('0');
  s.cp_clearsum = 0;
}

if(s.cp_lblng == 1){
  lblng.SetText(s.cp_sumng);
  s.cp_lblng = 0;
}

if(s.cp_lblnp == 1){
  lblnp.SetText(s.cp_sumnp);
  s.cp_lblnp = 0;
}

if(s.cp_lblok == 1){
  lblok.SetText(s.cp_sumok);
  s.cp_lblok = 0;
}

if(s.cp_lblincomplete == 1){
  lblincomplete.SetText(s.cp_sumincomplete);
  s.cp_lblincomplete = 0;
}

if(s.cp_lbltotal == 1){
  lbltotal.SetText(s.cp_sumtotal);
  s.cp_lbltotal = 0;
}

scheduleGridUpdate(s);
}//End Callback   

 var timeout;
 function scheduleGridUpdate(grid) {
    if (cbautorefresh.GetValue() == true){
    window.clearTimeout(timeout);
    timeout = window.setTimeout( 
    function() { grid.Refresh(); },
    60000
    //30000
    );
    }
}

function OnInitDate(s, e) {
    var today = new Date();
    dtdate.SetDate(today);            
}

function OnInitLine(s, e) {
    cbolineid.SetValue('ALL');
    cbopartid.PerformCallback('|' + 'ALL');
    cbopartid.SetValue('ALL');   
    txtpartname.SetValue('ALL');            
}


function OnInitPartID(s, e) {
    cboqcsstatus.PerformCallback('|');      
    cboqcsstatus.SetValue('ALL');  
}

function SelectLineID(s, e) {
    cbopartid.PerformCallback('|' + cbolineid.GetValue());
    cbopartid.SetValue('ALL');
    cboqcsstatus.SetValue('ALL');
//    txtpartname.SetText('');
//    cboqcsstatus.SetText('');
}

function SelectPartID(s, e) {
    txtpartname.SetText(cbopartid.GetSelectedItem().GetColumnText(1));

}

function SelectApprovalStatus(s, e) {
    cboqcsstatus.PerformCallback('|');
}

function ClickRefresh(s, e) {
    GridMenu.PerformCallback('Refresh|' + dtdate.GetValue() + '|' + cbolineid.GetValue() + '|' + cbopartid.GetValue() + '|' + cboqcsstatus.GetValue());
}

function ClickAutoRefresh(s, e) {
    GridMenu.PerformCallback('Refresh|' + dtdate.GetValue() + '|' + cbolineid.GetValue() + '|' + cbopartid.GetValue() + '|' + cboqcsstatus.GetValue());
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
    } 
    
}

function BtnClearClick(s, e){
    dtdate.SetText('');
    cbolineid.SetValue('');
    cbopartid.SetValue('');
    txtpartname.SetValue('');
    cboqcsstatus.SetValue('');
    lblok.SetValue(0);
    lblng.SetValue(0);
    lblincomplete.SetValue(0);
    lbltotal.SetValue(0);
    GridMenu.PerformCallback();
}

</script>
    <style type="text/css">
        .style1
        {
            width: 117px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div style="padding: 0px 5px 5px 5px">
    <table style="width: 100%;">
        <tr>
            <td style="padding: 5px 0px 0px 0px; width: 45px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Date" Font-Names="Segoe UI" 
                    Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 5px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 100px">
                <dx:ASPxDateEdit ID="dtdate" runat="server" Theme="Office2010Black" 
                    Width="100px" AutoPostBack="false"
                        ClientInstanceName="dtdate" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="5">
                        <ClientSideEvents Init="OnInitDate"/>
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
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 65px">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Part No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 5px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 150px">
                <dx:ASPxComboBox ID="cbopartid" runat="server" Theme="Office2010Black" TextField="PartName"
                    ClientInstanceName="cbopartid" DropDownStyle="DropDownList" ValueField="PartID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                    Width="150px" TabIndex="2">
                    <ClientSideEvents SelectedIndexChanged="SelectPartID" Init="OnInitPartID"/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Part ID" FieldName="PartID" Width="120px" />
                        <dx:ListBoxColumn Caption="Part Name" FieldName="PartName" Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 90px">
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="SQC Status" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 5px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 150px">
                <dx:ASPxComboBox ID="cboqcsstatus" runat="server" Theme="Office2010Black" TextField="Status"
                    ClientInstanceName="cboqcsstatus" DropDownStyle="DropDown" ValueField="Status"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="150px" TabIndex="4">
                    <Columns>
                        <dx:ListBoxColumn Caption="Status" FieldName="Status" Width="70px"/>
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Width="5px" Paddings-Padding="4px" ></ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 100px" class="style1">
                <dx:ASPxCheckBox ID="cbautorefresh" ClientInstanceName="cbautorefresh" runat="server" Height="19px" 
                    Text="Auto Refresh" Font-Names="Segoe UI" Font-Size="9pt">
                    <ClientSideEvents CheckedChanged="ClickAutoRefresh" />
                </dx:ASPxCheckBox>
            </td>
            <td style="padding: 5px 0px 0px 0px; width: 30px" class="style1">
                &nbsp;</td>
            <td style="width: 50px; border: 1px solid silver; background-color:#808080" class="style1" align="center">
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="OK"  ForeColor="White"
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 50px; border: 1px solid silver; background-color:#808080" class="style1" align="center">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="NG" ForeColor="White"
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 50px; border: 1px solid silver; background-color:#808080" class="style1" align="center">
                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="NP" ForeColor="White"
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 75px; border: 1px solid silver; background-color:#808080" class="style1" align="center">
                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Incomplete"  ForeColor="White"
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 50px; border: 1px solid silver; background-color:#808080" class="style1" align="center">
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="Total" ForeColor="White"
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td style="padding: 3px 5px 0px 0px; width: 45px">
                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Line No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 5px">
                &nbsp;</td>
            <td style="padding: 3px 5px 0px 0px; width: 100px">
               <dx:ASPxComboBox ID="cbolineid" runat="server" Theme="Office2010Black" TextField="LineName"
                    ClientInstanceName="cbolineid" DropDownStyle="DropDownList" ValueField="LineID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="1">
                    <ClientSideEvents SelectedIndexChanged="SelectLineID" Init="OnInitLine"/>
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
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; width: 65px">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Part Name" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 5px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; width: 150px">
                <dx:ASPxTextBox ID="txtpartname" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtpartname" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="150px" ReadOnly="True" TabIndex="3">
                </dx:ASPxTextBox>
            </td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; width: 90px">
                &nbsp;</td>
            <td style="width: 5px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; width: 150px">
                &nbsp;</td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; width: 100px" class="style1">
                <dx:ASPxButton ID="btnRefresh" runat="server" Text="Refresh" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnRefresh" Theme="Default" TabIndex="12">
                    <ClientSideEvents Click="ClickRefresh"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="padding: 3px 0px 5px 0px; width: 30px" class="style1">
                &nbsp;</td>
            <td style="width: 50px; border: 1px solid silver" class="style1" align="center">
                <dx:ASPxLabel ID="lblok" runat="server" Text="0" ClientInstanceName="lblok" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 50px; border: 1px solid silver" class="style1" align="center">
                <dx:ASPxLabel ID="lblng" runat="server" Text="0" ClientInstanceName="lblng" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 50px; border: 1px solid silver" class="style1" align="center">
                <dx:ASPxLabel ID="lblnp" runat="server" Text="0" ClientInstanceName="lblnp" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 50px; border: 1px solid silver" class="style1" align="center">
                <dx:ASPxLabel ID="lblincomplete" runat="server" Text="0" ClientInstanceName="lblincomplete"
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 50px; border: 1px solid silver" class="style1" align="center">
                <dx:ASPxLabel ID="lbltotal" runat="server" Text="0" ClientInstanceName="lbltotal"
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td style="padding: 3px 5px 0px 0px; width: 45px">
                &nbsp;</td>
            <td style="width: 5px">
                &nbsp;</td>
            <td style="padding: 3px 5px 0px 0px; width: 100px">
                &nbsp;</td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; width: 65px">
                &nbsp;</td>
            <td style="width: 5px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; width: 150px">
                &nbsp;</td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; width: 90px">
                &nbsp;</td>
            <td style="width: 5px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; width: 150px">
                &nbsp;</td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; width: 100px" class="style1">
                &nbsp;</td>
            <td style="padding: 3px 0px 5px 0px; width: 30px" class="style1">
                &nbsp;</td>
            <td style="width: 50px" class="style1">
                &nbsp;</td>
            <td style="width: 50px" class="style1">
                &nbsp;</td>
            <td style="width: 50px" class="style1">
                &nbsp;</td>
            <td style="width: 50px" class="style1">
                &nbsp;</td>
            <td style="width: 50px" class="style1">
                &nbsp;</td>
        </tr>
        </table>
</div>

<div style="padding: 2px 5px 5px 5px">
        <dx:ASPxGridView ID="GridMenu" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridMenu"
            EnableTheming="True" KeyFieldName="PartID;SubLineID;LineID" Theme="Office2010Black" 
            Width="100%" 
            Font-Names="Segoe UI" Font-Size="9pt">
            <ClientSideEvents EndCallback="OnEndCallback" Init="function grid_Init(s, e) {scheduleGridUpdate(s);}" BeginCallback="function grid_BeginCallback(s, e) {window.clearTimeout(timeout);}"/>
            <%--OnStartRowEditing="GridMenu_StartRowEditing" OnRowValidating="GridMenu_RowValidating"
            OnRowInserting="GridMenu_RowInserting" OnRowDeleting="GridMenu_RowDeleting" 
            OnAfterPerformCallback="GridMenu_AfterPerformCallback"--%>
            <Columns>
                <%--<dx:GridViewCommandColumn VisibleIndex="1" ShowSelectButton="true">
                    </dx:GridViewCommandColumn>--%>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" 
                    ShowClearFilterButton="true" VisibleIndex="1" SelectAllCheckboxMode="Page" 
                    Width="30px" FixedStyle="Left" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="Sub Line" VisibleIndex="2" Width="55px" 
                    FieldName="SubLineID">
                     <PropertiesTextEdit ClientInstanceName="SubLineID">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Part No." VisibleIndex="3" Width="110px" 
                    FieldName="PartID">
                    <PropertiesTextEdit ClientInstanceName="PartID">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Part Name" VisibleIndex="4" Width="120px"
                    FieldName="PartName">
                    <PropertiesTextEdit ClientInstanceName="PartName">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewBandColumn Caption="Shift 1" VisibleIndex="5">
                    <Columns>
                        <dx:GridViewBandColumn Caption="1" VisibleIndex="0">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift1Cycle1">
                                    <PropertiesTextEdit ClientInstanceName="Shift1Cycle1"> </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="2" VisibleIndex="1">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift1Cycle2">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="3" VisibleIndex="2">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift1Cycle3">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="4" VisibleIndex="3">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift1Cycle4">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="5" VisibleIndex="4">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift1Cycle5">
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
                
                <dx:GridViewBandColumn Caption="Shift 2" VisibleIndex="6">
                    <Columns>
                        <dx:GridViewBandColumn Caption="1" VisibleIndex="0">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift2Cycle1">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="2" VisibleIndex="1">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift2Cycle2">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="3" VisibleIndex="2">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift2Cycle3">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="4" VisibleIndex="3">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift2Cycle4">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn Caption="5" VisibleIndex="4">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift2Cycle5">
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
                <dx:GridViewBandColumn Caption="Shift 3" VisibleIndex="7">
                    <Columns>
                        <dx:GridViewBandColumn VisibleIndex="0" Caption="1">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift3Cycle1">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn VisibleIndex="1" Caption="2">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift3Cycle2">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn VisibleIndex="2" Caption="3">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift3Cycle3">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn VisibleIndex="3" Caption="4">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift3Cycle4">
                                    <EditCellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </EditCellStyle>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </dx:GridViewBandColumn>
                        <dx:GridViewBandColumn VisibleIndex="4" Caption="5">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="(00 : 00)" VisibleIndex="0" Width="50px" 
                                    FieldName="Shift3Cycle5">
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
                
                <dx:GridViewDataTextColumn Caption="LineID" VisibleIndex="8"
                FieldName="LineID" Visible="False">
                 <PropertiesTextEdit ClientInstanceName="LineID">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                
            </Columns>
        <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" />
        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
        <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true" PageSizeItemSettings-Visible="true">
<PageSizeItemSettings Visible="True"></PageSizeItemSettings>
        </SettingsPager>
        <Settings HorizontalScrollBarMode="Auto" 
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
                
                    <dx:ASPxLabel ID="lblHint" ClientInstanceName="lblHint" runat="server" 
                    Font-Names="Segoe UI" Font-Size="9pt" 
                    
                    Text="No Process : [TC] = Tool Change, [D] = Dandori, [MT] = Machine Trouble, [O] = Other" 
                    Width="550px"></dx:ASPxLabel>                
                </td>
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
                    ClientInstanceName="btnExcel" Theme="Default" TabIndex="14">
                    <ClientSideEvents Click="function(s, e) {
                            GridMenu.PerformCallback('Excel|' + cbolineid.GetValue() + '|' + cbopartid.GetValue() + '|' + txtpartname.GetValue() + '|' + cboqcsstatus.GetValue());
                            }" />
                            <%--GridMenu.PerformCallback('Excel|' + cbolineid.GetValue() + '|' + cbopartid.GetValue() + '|' + txtpartname.GetValue() + '|' + cboqcsstatus.GetValue());--%>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 5px 0px; width:10px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnViewQCS" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnViewQCS" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="View SQC Result" Theme="Default" UseSubmitBehavior="false" 
                    Width="90px" TabIndex="15" ClientEnabled="true">
                    <ClientSideEvents Click="ViewQCS"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
        </tr>
        </table>
</div>
</asp:Content>
