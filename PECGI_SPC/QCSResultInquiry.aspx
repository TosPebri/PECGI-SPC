<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="QCSResultInquiry.aspx.vb" Inherits="PECGI_SPC.QCSResultInquiry" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register assembly="DevExpress.Web.v20.2, Version=20.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>










<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" >
        function OnInit(s, e) {
            var today = new Date();

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

            if (s.cp_Date != null) {
                dtDate.SetDate(s.cp_Date);
                cboShift.SetValue(0);
                cboLineID.SetValue(s.cp_LineID);
                cboSubLine.SetValue(s.cp_SubLineID);
                cboPartID.SetValue(s.cp_PartID);
                txtPartName.SetText(s.cp_PartName);
                cboRevNo.SetValue(s.cp_RevNo);
                hfRevNo.Set('revno', s.cp_RevNo);
            }
            s.cp_Date = null;
        }

        function OnBatchEditStartEditing(s, e) {
            e.cancel = true;
            currentEditableVisibleIndex = e.visibleIndex;
        }

        function SelectLineID(s, e) {
            cboSubLine.PerformCallback('load|' + cboLineID.GetValue());            
            cboPartID.PerformCallback('load|' + cboLineID.GetValue());
            txtPartName.SetText('');
            FillCboRev();
        }

        function GetRevNo(s, e) {
            var revno = s.cpRevNo;
            if (revno != '-') {
                cboRevNo.SetEnabled(false);
                cboRevNo.ClearItems();
                var revnolist = s.cpRevNoList;
                var index, len;
                len = revnolist.length;
                for (index = 0; index < len; ++index) {
                    var item = revnolist[index];
                    cboRevNo.AddItem(item);
                    if (index == 0) {
                        hfRevNo.Set('revno', item);
                    }
                }
                cboRevNo.SetSelectedIndex(0);                
                if (cboRevNo.GetItemCount() <= 1) {
                    cboRevNo.SetEnabled(true);
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
        }

        function SelectPartID(s, e) {
            txtPartName.SetText(cboPartID.GetSelectedItem().GetColumnText(1));
            FillCboRev();
        }

        function FillCboRev(s, e) {
            cbkRefresh.PerformCallback('load|' + dtDate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue() + '|' + txtLotNo.GetText());
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

            cbkApprove.PerformCallback('approve|' + dtDate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue());
        }
    </script>
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
            <td style=" width:130px; padding:5px 0px 0px 0px">
                <dx:ASPxDateEdit ID="dtDate" runat="server" Theme="Office2010Black" 
                    Width="100px"
                        ClientInstanceName="dtDate" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="2" 
                    EditFormat="Custom">
                        <CalendarProperties>
                            <HeaderStyle Font-Size="12pt" Paddings-Padding="5px" ><Paddings Padding="5px"></Paddings>
                            </HeaderStyle>
                            <DayStyle Font-Size="9pt" Paddings-Padding="5px" ><Paddings Padding="5px"></Paddings>
                            </DayStyle>
                            <WeekNumberStyle Font-Size="9pt" Paddings-Padding="5px"><Paddings Padding="5px"></Paddings>
                            </WeekNumberStyle>
                            <FooterStyle Font-Size="9pt" Paddings-Padding="10px" ><Paddings Padding="10px"></Paddings>
                            </FooterStyle>
                            <ButtonStyle Font-Size="9pt" Paddings-Padding="10px"><Paddings Padding="10px"></Paddings>
                            </ButtonStyle>
                        </CalendarProperties>
                        <ClientSideEvents Init="OnInit" />
                        <ButtonStyle Width="5px" Paddings-Padding="4px" >
<Paddings Padding="4px"></Paddings>
                        </ButtonStyle>
                    </dx:ASPxDateEdit>
            </td>
            <td style=" width:90px; padding:5px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Line No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width:130px; padding:5px 0px 0px 0px">
                <dx:ASPxComboBox ID="cboLineID" runat="server" Theme="Office2010Black" TextField="LineName"
                    ClientInstanceName="cboLineID" ValueField="LineID" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="4">
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
            <td style=" padding: 5px 0px 0px 0px; width:60px">
                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Part No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:10px">
                &nbsp;
                </td>
            <td style="padding: 5px 0px 0px 0px; width: 180px;">                
                
                <dx:ASPxComboBox ID="cboPartID" runat="server" Theme="Office2010Black" TextField="PartName"
                    ClientInstanceName="cboPartID" ValueField="PartID" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                    Width="150px" TabIndex="6" EnableCallbackMode="True">
                    <ClientSideEvents SelectedIndexChanged="SelectPartID" />
                    <Columns>
                        <dx:ListBoxColumn Caption="Part ID" FieldName="PartID" Width="120px" />
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
           
                <td style=" width:65px">                
                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Lot No." 
                        Font-Names="Segoe UI" Font-Size="9pt" Width="60px">
                    </dx:ASPxLabel>                
                </td>            
            <td colspan="2" style="width: 120px">                
                
                <dx:ASPxTextBox ID="txtLotNo" runat="server" ClientInstanceName="txtLotNo" 
                    MaxLength="10" Width="90px" TabIndex="8">
                    <ClientSideEvents TextChanged="function(s, e) {
	if(txtLotNo.GetText() == '') {
		dtDate.SetEnabled(true);
		cboShift.SetEnabled(true);
		cboLineID.SetEnabled(true);
		cboSubLine.SetEnabled(true);
		cboPartID.SetEnabled(true);
	}
	else
	{
		dtDate.SetEnabled(false);
		cboShift.SetEnabled(false);
		cboLineID.SetEnabled(false);
		cboSubLine.SetEnabled(false);
		cboPartID.SetEnabled(false);		
	}
}" KeyUp="function(s, e) {
	if(txtLotNo.GetText() == '') {
		dtDate.SetEnabled(true);
		cboShift.SetEnabled(true);
		cboLineID.SetEnabled(true);
		cboSubLine.SetEnabled(true);
		cboPartID.SetEnabled(true);		
	}
	else
	{
		dtDate.SetEnabled(false);
		cboShift.SetEnabled(false);
		cboLineID.SetEnabled(false);
		cboSubLine.SetEnabled(false);
		cboPartID.SetEnabled(false);
	}	
}" />
                </dx:ASPxTextBox>
            </td>            
            <td colspan="1" style="width: 100px">                
                
                &nbsp;</td>            
        </tr>

        <tr>
            <td style=" width:60px; padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Shift" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:130px; padding:3px 0px 0px 0px">
                
                <dx:ASPxComboBox ID="cboShift" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboShift" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    Width="100px" TabIndex="3" SelectedIndex="0">
                    <ClientSideEvents SelectedIndexChanged="FillCboRev" />
                    <Items>
                        <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
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
            <td style=" padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Sub Line No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width:130px; padding:3px 0px 0px 0px">
                
                    
                <dx:ASPxComboBox ID="cboSubLine" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboSubLine" ValueField="SubLineID" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="5">
                    <ClientSideEvents SelectedIndexChanged="FillCboRev" />
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
            <td style=" padding: 3px 0px 0px 0px; width:60px">
                <dx:ASPxLabel ID="lblqeleader0" runat="server" Text="Part Name" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:10px">
                &nbsp;</td>
            <td style="width:150px; padding:3px 0px 0px 0px" colspan="1">
                <dx:ASPxTextBox ID="txtPartName" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtPartName" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="150px" ReadOnly="True" TabIndex="-1">
                </dx:ASPxTextBox>
            </td>            
            <td>
                <dx:ASPxLabel ID="lblqeleader1" runat="server" Text="SQC Rev No." 
                    Font-Names="Segoe UI" Font-Size="9pt" Width="80px">
                </dx:ASPxLabel>
            </td>            
            <td align="left" style="width: 60px">
                
                <dx:ASPxComboBox ID="cboRevNo" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboRevNo" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    Width="58px" TabIndex="9">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	hfRevNo.Set('revno', cboRevNo.GetText());
}" />
                    <ItemStyle Height="10px" Paddings-Padding="4px">
<Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
                
            </td>   
            <td align="left">
                &nbsp;</td>   
            <td align="left">
                <dx:ASPxButton ID="btnSearch" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnSearch" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Search" Theme="Default" UseSubmitBehavior="False" 
                    Width="90px" TabIndex="10">
                    <ClientSideEvents Click="function(s, e) {
                        var errmsg = '';
                        if(txtLotNo.GetText() == '') {
	                        if(cboLineID.GetText() == '') {
                                cboLineID.Focus();
                                errmsg = 'Please select Line No!';                                                                
	                        } else if(cboSubLine.GetText() == '') {
                                cboSubLine.Focus();
                                errmsg = 'Please select Sub Line No!';
	                        } else if(cboShift.GetText() == '') {
                                cboShift.Focus();
                                errmsg = 'Please select Shift!';
	                        } else if(cboPartID.GetText() == '') {
                                cboPartID.Focus();
                                errmsg = 'Please select Part No!';
	                        }

                            if(errmsg != '') {
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
	                    grid.PerformCallback('load|' + dtDate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboShift.GetValue() + '|' + cboRevNo.GetValue() + '|' + txtLotNo.GetText());
                    }" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>         
        </tr>
    </table>
</div>

<div style="height:10px">
    <dx:ASPxHiddenField ID="hfRevNo" runat="server" ClientInstanceName="hfRevNo">
    </dx:ASPxHiddenField>
    </div>
<div style="padding: 2px 5px 5px 5px">
        <dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
            EnableTheming="True" KeyFieldName="LevelNo" Theme="Office2010Black" 
            OnStartRowEditing="grid_StartRowEditing" OnRowValidating="grid_RowValidating"
            OnRowInserting="grid_RowInserting" OnRowDeleting="grid_RowDeleting" 
            OnAfterPerformCallback="grid_AfterPerformCallback" 
            OnCellEditorInitialize="grid_CellEditorInitialize" Width="100%" 
            Font-Names="Segoe UI" Font-Size="9pt" TabIndex="11">
            <ClientSideEvents EndCallback="OnEndCallback" 
                BatchEditStartEditing="OnBatchEditStartEditing" 
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
                    VisibleIndex="2" Width="50px">
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
                Width="60px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ItemID" VisibleIndex="10" Width="0px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Standard" VisibleIndex="12" Width="150px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Range" FieldName="Range" VisibleIndex="13" 
                Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="MeasuringInstrument" VisibleIndex="14" Width="90px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FrequencyType" VisibleIndex="15" Width="90px"></dx:GridViewDataTextColumn>            

            <dx:GridViewBandColumn Caption="SHIFT 1" VisibleIndex="16">
                <Columns>
                    <dx:GridViewBandColumn Caption="5" VisibleIndex="4">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle15" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="4" VisibleIndex="3">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle14" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="39px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="3" VisibleIndex="2">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle13" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="2" VisibleIndex="1">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle12" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="1" VisibleIndex="0">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle11" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="SHIFT 2" VisibleIndex="17">
                <Columns>
                    <dx:GridViewBandColumn Caption="5" VisibleIndex="5">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle25" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="4" VisibleIndex="4">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle24" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="3" VisibleIndex="3">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle23" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="2" VisibleIndex="2">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle22" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="1" VisibleIndex="1">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle21" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="SHIFT 3" VisibleIndex="18">
                <Columns>
                    <dx:GridViewBandColumn Caption="5" VisibleIndex="4">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle35" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="4" VisibleIndex="3">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle34" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="3" VisibleIndex="2">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle33" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="2" VisibleIndex="1">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle32" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewBandColumn Caption="1" VisibleIndex="0">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption=" " FieldName="Cycle31" VisibleIndex="0" 
                                Width="76px">
                                <PropertiesTextEdit MaxLength="5" Width="72px">
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                </Columns>
            </dx:GridViewBandColumn>

            <dx:GridViewDataTextColumn FieldName="LevelNo" VisibleIndex="1" Width="0px">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Status11" VisibleIndex="18" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status12" VisibleIndex="19" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status13" VisibleIndex="20" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status14" VisibleIndex="21" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status15" VisibleIndex="22" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Status21" VisibleIndex="23" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status22" VisibleIndex="24" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status23" VisibleIndex="25" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status24" VisibleIndex="26" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status25" VisibleIndex="27" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn FieldName="Status31" VisibleIndex="28" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status32" VisibleIndex="29" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status33" VisibleIndex="30" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status34" VisibleIndex="31" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Status35" VisibleIndex="32" Width="40px" 
                Visible="False"></dx:GridViewDataTextColumn>            
        </Columns>
        <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" 
                AllowFocusedRow="True" AllowDragDrop="False" AllowSort="False" />
        <SettingsEditing Mode="Batch" >
            <BatchEditSettings ShowConfirmOnLosingChanges="False" />
            </SettingsEditing>
        <SettingsPager AlwaysShowPager="true" Mode="ShowAllRecords" PageSize="30">
        </SettingsPager>
        <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="250" 
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
<div style="height:10px"></div>
<div style="padding: 0px 5px 5px 5px">
    <table style="width: 100%">
        <tr >
            <td style="padding:5px 0px 5px 0px">
                
                <dx:ASPxCallback ID="cbkRefresh" runat="server" ClientInstanceName="cbkRefresh">
                                <ClientSideEvents EndCallback="GetRevNo"/>
                            </dx:ASPxCallback>
                
            </td>
            <td style="padding:5px 0px 5px 0px">

            </td>
            <td style="padding:5px 0px 5px 0px">
                
            </td>
            <td style="padding:5px 0px 5px 0px">
                &nbsp;
                
            </td>
            <td style="width:95px;">
                
            </td>
            <td style="width: 95px;">
<dx:ASPxButton ID="btnClear" runat="server" Text="Clear" UseSubmitBehavior="False"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="False"
                    ClientInstanceName="btnClear" Theme="Default" TabIndex="1">
                    <ClientSideEvents Click="function(s, e) {
	cboShift.SetText('');
	cboLineID.SetText('');
	cboSubLine.SetText('');
	cboPartID.SetText('');
	txtPartName.SetText('');
    txtLotNo.SetText('');
    cboRevNo.SetText('');
    cboShift.SetValue(0);

		dtDate.SetEnabled(true);
		cboShift.SetEnabled(true);
		cboLineID.SetEnabled(true);
		cboSubLine.SetEnabled(true);
		cboPartID.SetEnabled(true);

    grid.CollapseAll();
}" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>                
            </td>
            <td style="width:95px; padding:0px 0px 0px 0px;">
                <dx:ASPxButton ID="btnExcel" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnExcel" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Excel" Theme="Default" UseSubmitBehavior="False" 
                    Width="90px" TabIndex="12">
                    <Paddings Padding="2px" />
                    <ClientSideEvents Click="function(s, e) {
                        if(txtLotNo.GetText() == '') {
                            var errmsg = '';
	                        if(cboLineID.GetText() == '') {
                                cboLineID.Focus();
                                errmsg = 'Please select Line No!';                                                                
	                        } else if(cboSubLine.GetText() == '') {
                                cboSubLine.Focus();
                                errmsg = 'Please select Sub Line No!';
	                        } else if(cboShift.GetText() == '') {
                                cboShift.Focus();
                                errmsg = 'Please select Shift!';
	                        } else if(cboPartID.GetText() == '') {
                                cboPartID.Focus();
                                errmsg = 'Please select Part No!';
	                        }

                            if(errmsg != '') {
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
                    }" />
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

</asp:Content>
