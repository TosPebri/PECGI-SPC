<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SubLine.aspx.vb" Inherits="PECGI_SPC.SubLine" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>


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

        }
 }    

 function SelectLine(s, e) {
            LineName.SetText(LineID.GetSelectedItem().GetColumnText(1));
            LineName.inputElement.style.color = 'Silver';
            SubLineID.SetText('');
            Grid.PerformCallback('load|');
          }  

function SelectProcess(s, e) {
    ProcessName.SetText(ProcessID.GetSelectedItem().GetColumnText(1));
    ProcessName.inputElement.style.color = 'Silver';
        }  

var lastLineID = null;
function OnLineIDChanged(cmbLineID) {
    LineName.SetText(LineID.GetSelectedItem().GetColumnText(1));
    LineName.inputElement.style.color = 'Silver';
    if (Grid.GetEditor("SubLineID").InCallback() )
	    lastLineID = cmbLineID.GetValue().toString();
    else            
	    Grid.GetEditor("SubLineID").PerformCallback(cmbLineID.GetValue().toString());
	}

function OnEndCallbackSubLineID(s,e){
		if (lastLineID){
			Grid.GetEditor("SubLineID").PerformCallback(lastLineID);
			lastLineID = null;
		}
	}

var startTime;
function OnBeginCallbackMachineNo() {
	startTime = new Date();
}
function OnBeginCallbackMachineNo() {
	var result = new Date() - startTime;
	result /= 1000;
	result = result.toString();
	if(result.length > 4){
		result = result.substr(0, 4);
    }
}

	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

<div style="padding: 5px 5px 5px 5px">
         <div style="padding: 5px 5px 5px 5px">
        <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="Grid"
            EnableTheming="True" KeyFieldName="LineID;SubLineID;ProcessID" Theme="Office2010Black" 
             Width="100%"
            Font-Names="Segoe UI" Font-Size="9pt" >
            <ClientSideEvents EndCallback="OnEndCallback" />

            <Columns>
                <dx:GridViewCommandColumn FixedStyle="Left"
                    VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" 
                    ShowNewButtonInHeader="true" ShowClearFilterButton="true">
                    <HeaderStyle Paddings-PaddingLeft="3px" HorizontalAlign="Center" 
                        VerticalAlign="Middle" >
                        <Paddings PaddingLeft="3px"></Paddings>
                    </HeaderStyle>
                </dx:GridViewCommandColumn>

                <dx:GridViewDataComboBoxColumn Caption="Line ID" FieldName="LineID" 
                    VisibleIndex="1" Width="75px" Visible="False">
                    <EditFormSettings Visible="True" VisibleIndex="1" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="75px" 
                                    TextFormatString="{0}-{1}"
                                    DisplayFormatString="{0}" 
                                    IncrementalFilteringMode="Contains" 
                                    TextField="LineID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="LineID" ClientInstanceName="LineID">
                                    <%--<ClientSideEvents SelectedIndexChanged="SelectLine" />--%>
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) { OnLineIDChanged(s); }"></ClientSideEvents>
                                    <Columns>
                                        <dx:listboxcolumn Caption="Line ID" FieldName="LineID" Width="68px"/>
                                        <dx:listboxcolumn Caption="Line Name" FieldName="LineName" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="2px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataTextColumn Caption="LineID" FieldName="LineID1" VisibleIndex="2" Width="75px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>

                    <EditFormSettings Visible="False" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Line Name" FieldName="LineName"
                    VisibleIndex="3" Width="160px" Settings-AutoFilterCondition="Contains" 
                    ReadOnly="True" 
                    >
                    <PropertiesTextEdit MaxLength="35" Width="150px" ClientInstanceName="LineName">
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <EditFormSettings Visible="True" VisibleIndex="2" />
                    <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataComboBoxColumn Caption="Sub Line ID" FieldName="SubLineID" 
                    VisibleIndex="4" Width="80px" Visible="False">
                    <EditFormSettings VisibleIndex="3" Visible="True" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDown" 
                                    Width="80px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="SubLineID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="SubLineID"
                                    ClientInstanceName="SubLineID"
                                    MaxLength="50"
                                    >
                                    <ClientSideEvents EndCallback="OnEndCallbackSubLineID"/>
                                   <%-- <Columns>
                                        <dx:listboxcolumn Caption="Sub Line ID" FieldName="SubLineID" Width="68px"/>
                                    </Columns>--%>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>

                 <dx:GridViewDataTextColumn Caption="Sub Line ID" FieldName="SubLineID1" Width="80px"
                    VisibleIndex="5">
                    <EditFormSettings Visible="False" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataComboBoxColumn Caption="Process ID" FieldName="ProcessID" 
                    VisibleIndex="6" Width="80px" Visible="False">
                    <EditFormSettings Visible="True" VisibleIndex="4" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="80px" 
                                    TextFormatString="{0}-{1}"
                                    DisplayFormatString="{0}" 
                                    IncrementalFilteringMode="Contains" 
                                    TextField="ProcessID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="ProcessID"
                                    ClientInstanceName="ProcessID">
                                    <ClientSideEvents SelectedIndexChanged="SelectProcess"/>
                                    <Columns>
                                        <dx:listboxcolumn Caption="Process ID" FieldName="ProcessID" Width="68px"/>
                                        <dx:listboxcolumn Caption="Process Name" FieldName="ProcessName" Width="150px" />
                                        <dx:listboxcolumn Caption="Remark" FieldName="Remark" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>

                 <dx:GridViewDataTextColumn Caption="ProcessID" FieldName="ProcessID1"  
                    VisibleIndex="7" Width="80px">
                    <EditFormSettings Visible="False" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Process Name" FieldName="ProcessName"
                    VisibleIndex="8" Width="100px" Settings-AutoFilterCondition="Contains" 
                    >
                    <PropertiesTextEdit MaxLength="35" Width="100px" ClientInstanceName="ProcessName">
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <EditFormSettings Visible="True" VisibleIndex="5" />
                    <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataComboBoxColumn Caption="Machine No" FieldName="MachineNo" 
                    VisibleIndex="9" Width="95px" Visible="False">
                    <EditFormSettings Visible="True" VisibleIndex="6" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="95px" 
                                    TextFormatString="{0}-{1}"
                                    DisplayFormatString="{0}" 
                                    IncrementalFilteringMode="Contains" 
                                    TextField="MachineNo" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="MachineNo"
                                    ClientInstanceName="MachineNo"
                                    EnableCallbackMode="true" CallbackPageSize="10">
                                    <ClientSideEvents BeginCallback="function(s, e) { OnBeginCallbackMachineNo(); }" EndCallback="function(s, e) { OnEndCallbackMachineNo(); } "/>
                                    <Columns>
                                        <dx:listboxcolumn Caption="Machine No" FieldName="MachineNo" Width="68px"/>
                                        <dx:listboxcolumn Caption="Remark" FieldName="Remark" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="2px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>


                 <dx:GridViewDataTextColumn Caption="MachineNo" VisibleIndex="10" 
                    FieldName="MachineNo1" Width="95px">
                    <EditFormSettings Visible="False" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                </dx:GridViewDataTextColumn>

                <%--<dx:GridViewDataTextColumn Caption="Machine No" FieldName="MachineNo"
                    VisibleIndex="6" Width="130px" Settings-AutoFilterCondition="Contains" 
                    >
                    <PropertiesTextEdit MaxLength="15" Width="130px">
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>--%>

                <dx:GridViewDataTextColumn Caption="Description" FieldName="Description"
                    VisibleIndex="11" Width="160px" Settings-AutoFilterCondition="Contains" 
                    >
                    <PropertiesTextEdit MaxLength="100" Width="150px">
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <EditFormSettings VisibleIndex="7" />
                    <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True"></CellStyle>
                </dx:GridViewDataTextColumn>
                
                <dx:GridViewDataTextColumn Caption="Duplicate" FieldName="Duplicate" 
                    VisibleIndex="12">
                    <EditFormSettings Visible="False" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                
            </Columns>

            <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="Control" />
            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm"/>
            <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true" PageSizeItemSettings-Visible="true">
<PageSizeItemSettings Visible="True"></PageSizeItemSettings>
            </SettingsPager>
            <Settings ShowFilterRow="True" VerticalScrollBarMode="Auto" 
                VerticalScrollableHeight="250" HorizontalScrollBarMode="Auto" />
            <SettingsText ConfirmDelete="Are you sure want to delete ?"></SettingsText>

                                   <SettingsPopup>
                <EditForm Modal="false" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Width="320" />
            </SettingsPopup>

            <Styles EditFormColumnCaption-Paddings-PaddingLeft="10px" EditFormColumnCaption-Paddings-PaddingRight="10px" >
                <Header>
                    <Paddings Padding="2px"></Paddings>
                </Header>
                
                <EditFormColumnCaption Font-Size="9pt" Font-Names="Segoe UI">
                    <Paddings PaddingLeft="15px" PaddingTop="5px" PaddingBottom="5px" PaddingRight="5px"></Paddings>
                </EditFormColumnCaption>
            </Styles>
                    
            <Templates>
                <EditForm>
                    <div style="padding: 15px 15px 15px 15px">
                        <dx:ContentControl ID="ContentControl1" runat="server">
                            <dx:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors"
                                runat="server">
                            </dx:ASPxGridViewTemplateReplacement>
                        </dx:ContentControl>
                    </div>
                    <div style="text-align: left; padding: 5px 5px 5px 15px">                        
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                            runat="server" >
                        </dx:ASPxGridViewTemplateReplacement>
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                            runat="server">
                        </dx:ASPxGridViewTemplateReplacement>
                    </div>
                </EditForm>
            </Templates>
<%--            <SettingsPopup>
                <EditForm Modal="false" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Width="80" />
            </SettingsPopup>

            <Styles EditFormColumnCaption-Paddings-PaddingLeft="10px" EditFormColumnCaption-Paddings-PaddingRight="10px" >
                <Header>
                    <Paddings Padding="2px"></Paddings>
                </Header>

                <EditFormColumnCaption Font-Size="9pt" Font-Names="Segoe UI">
                    <Paddings PaddingLeft="5px" PaddingTop="5px" PaddingBottom="5px"></Paddings>
                </EditFormColumnCaption>
            </Styles>
                    
            <Templates>
                <EditForm>
                    <div style="padding: 15px 15px 15px 15px">
                        <dx:ContentControl ID="ContentControl1" runat="server">
                            <dx:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors"
                                runat="server">
                            </dx:ASPxGridViewTemplateReplacement>
                        </dx:ContentControl>
                    </div>
                    <div style="text-align: left; padding: 5px 5px 5px 15px">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                            runat="server">
                        </dx:ASPxGridViewTemplateReplacement>
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                            runat="server">
                        </dx:ASPxGridViewTemplateReplacement>
                    </div>
                </EditForm>
            </Templates>--%>
                    
         </dx:ASPxGridView>
         </div> 

            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT RTRIM(LineID) as LineID, RTRIM(LineName) LineName FROM [Line]">
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT RTRIM(ProcessID) as ProcessID, RTRIM(ProcessName) as ProcessName FROM [Process]">
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource3" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT DISTINCT(SubLineID) AS SubLineID FROM SubLine WHERE LineID = @LineID">
            <SelectParameters>
                <asp:Parameter Name="LineID"/>
            </SelectParameters>
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource4" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT MachineNo, Remark FROM Machine">
            </asp:SqlDataSource>

</div>

</asp:Content>