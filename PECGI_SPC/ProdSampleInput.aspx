<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProdSampleInput.aspx.vb" Inherits="PECGI_SPC.ProdSampleInput" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register assembly="DevExpress.Web.v20.2, Version=20.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" >
        function OnBatchEditStartEditing(s, e) {
            e.cancel = true;
            currentEditableVisibleIndex = e.visibleIndex;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div style="padding: 0px 5px 5px 5px">
        <table class="auto-style3" style="width: 100%">
        <tr >
            <td style="width:60px; padding:5px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Factory" Font-Names="Segoe UI" 
                    Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:130px; padding:5px 0px 0px 0px">
                <dx:ASPxComboBox ID="cboFactory" runat="server" Theme="Office2010Black" TextField="FactoryName"
                    ClientInstanceName="cboFactory" ValueField="FactoryCode" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{1}" DisplayFormatString="{1}"
                    Width="100px" TabIndex="6" EnableCallbackMode="True">
                    <Columns>
                        <dx:ListBoxColumn Caption="Factory Code" FieldName="FactoryCode" Width="90px" />
                        <dx:ListBoxColumn Caption="Factory Name" FieldName="FactoryName" Width="200px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" >
<Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style=" width:100px; padding:5px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Machine Process" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width:130px; padding:5px 0px 0px 0px">
                <dx:ASPxComboBox ID="cboLine" runat="server" Theme="Office2010Black" TextField="LineName"
                    ClientInstanceName="cboLine" ValueField="LineCode" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{1}" DisplayFormatString="{1}"
                    Width="190px" TabIndex="4">
                    <Columns>
                        <dx:ListBoxColumn Caption="Line Code" FieldName="LineCode" Width="70px" />
                        <dx:ListBoxColumn Caption="Line Name" FieldName="LineName" Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px"/>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style=" padding: 5px 0px 0px 0px; width:50px">
                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Date" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:10px">
                &nbsp;
                </td>
            <td style="padding: 5px 0px 0px 0px; width: 180px;" colspan="3">                
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
           
                <td style=" width:110px">                
                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Show Verified Only" 
                        Font-Names="Segoe UI" Font-Size="9pt" Width="109px">
                    </dx:ASPxLabel>                
                </td>            
            <td colspan="2" style="width: 120px">                
                
                <dx:ASPxComboBox ID="cboShow" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboShow" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    Width="58px" TabIndex="9">
                    <Items>                        
                        <dx:ListEditItem Text="No" Value="0" Selected="true"/>
                        <dx:ListEditItem Text="Yes" Value="1" />
                    </Items>
                    <ItemStyle Height="10px" Paddings-Padding="4px">
<Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
                
            </td>                        
        </tr>

        <tr>
            <td style=" width:60px; padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Type" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:130px; padding:3px 0px 0px 0px">
                
                <dx:ASPxComboBox ID="cboType" runat="server" Theme="Office2010Black" TextField="Description"
                    ClientInstanceName="cboType" ValueField="ItemTypeCode" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" TextFormatString="{1}" DisplayFormatString="{1}"
                    Width="100px" TabIndex="6" EnableCallbackMode="True" NullValueItemDisplayText="{0}-{1}">
                    <Columns>
                        <dx:ListBoxColumn Caption="Item Type" FieldName="ItemTypeCode" Width="90px" />
                        <dx:ListBoxColumn Caption="Description" FieldName="Description" Width="200px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" >
<Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style=" padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Item Check" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width:200px; padding:3px 0px 0px 0px">
                
                    
                <dx:ASPxComboBox ID="cboItemCheck" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboItemCheck" ValueField="ItemCheckCode" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" TextFormatString="{1}" DisplayFormatString="{1}"
                    Width="190px" TabIndex="5" NullValueItemDisplayText="{1}">
                    <Columns>
                        <dx:ListBoxColumn Caption="Code" FieldName="ItemCheckCode" Width="70px" />
                        <dx:ListBoxColumn Caption="Item Check" FieldName="ItemCheck" Width="250px">
                        </dx:ListBoxColumn>
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px">
<Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
                
                    
            </td>
            <td style=" padding: 3px 0px 0px 0px; ">
                <dx:ASPxLabel ID="lblqeleader0" runat="server" Text="Shift" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:10px">
                &nbsp;</td>
            <td style="width:70px; padding:3px 0px 0px 0px">
                
                <dx:ASPxComboBox ID="cboShift" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboShift" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    Width="60px" TabIndex="3" SelectedIndex="0">
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
            <td style="width:30px; padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="lblqeleader2" runat="server" Text="Seq" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>            
            <td style="width:60px; padding:3px 0px 0px 0px">
                
                <dx:ASPxComboBox ID="cboSeq" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboSeq" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    Width="60px" TabIndex="3" SelectedIndex="0">
                    <Items>
                        <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                        <dx:ListEditItem Text="1" Value="1" />
                        <dx:ListEditItem Text="2" Value="2" />
                        <dx:ListEditItem Text="3" Value="3" />
                        <dx:ListEditItem Text="4" Value="4" />
                        <dx:ListEditItem Text="5" Value="5" />
                    </Items>
                    <ItemStyle Height="10px" Paddings-Padding="4px">
<Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
                
            </td>            
            <td>
                &nbsp;</td>            
            <td align="left" style="width: 60px">
                
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
	                        } else if(cboFactory.GetText() == '') {
                                cboFactory.Focus();
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
	                    grid.PerformCallback('load|' + dtDate.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboFactory.GetValue() + '|' + cboShift.GetValue() + '|' + cboRevNo.GetValue() + '|' + txtLotNo.GetText());
                    }" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
                
            </td>   
            <td align="left">
                &nbsp;</td>   
                     
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
            OnRowInserting="grid_RowInserting" 
            OnRowDeleting="grid_RowDeleting" 
            OnAfterPerformCallback="grid_AfterPerformCallback" 
            Width="100%" 
            Font-Names="Segoe UI" Font-Size="9pt" TabIndex="11">
            <ClientSideEvents EndCallback="OnEndCallback" 
                BatchEditStartEditing="OnBatchEditStartEditing" 
             />
        <Columns>

            <dx:GridViewCommandColumn Caption="Action" VisibleIndex="0">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Data#" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Value" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Judgement" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Operator" VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Sample Time" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Remarks" VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Delete Status" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Last User" VisibleIndex="8">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Last Update" VisibleIndex="9">
            </dx:GridViewDataTextColumn>

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

<FilterControl AutoUpdatePosition="False"></FilterControl>
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

<div style="height:10px">


</div>    
</asp:Content>
