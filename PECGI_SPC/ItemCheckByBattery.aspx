<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ItemCheckByBattery.aspx.vb" Inherits="PECGI_SPC.ItemCheckByBattery" MasterPageFile="~/Site.Master" %>

<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ButtonStyleNo * {
            background-color: #003fbe;
            color: #fff;
        }
    </style>

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

        function up_Browse() {
            var Freq = cboFreq.GetSelectedItem().value;
            Grid.PerformCallback('Load|' + Freq);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div style="padding: 5px 5px 5px 5px; border-bottom-color: #a9a9a9; border-bottom-style: inset; border-bottom-width: 1px">
        <table>
            <tr style="height: 30px">
                <td style="width: 120px">
                    &nbsp;<dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="Factory">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="cboFactory" runat="server" Theme="Office2010Black" Width="200px" Height="25px" ClientInstanceName="cboFactory">
                        <ItemStyle Height="10px" Paddings-Padding="4px" />
                        <ButtonStyle Width="5px" Paddings-Padding="4px" />
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                var FactoryCode = cboFactory.GetValue();
                                HF.Set('FactoryCode', FactoryCode);
                                Grid.PerformCallback('');
                            }" />
                    </dx:ASPxComboBox>                            
                </td>
                <td style="width: 120px">
                    &nbsp;<dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="Machine Proccess">
                    </dx:ASPxLabel>
                </td>
                <td style="padding-right: 1em">
                    <dx:ASPxComboBox ID="cboMachineProccess" runat="server" Theme="Office2010Black" Width="200px" Height="25px" ClientInstanceName="cboMachineProccess">
                        <ItemStyle Height="10px" Paddings-Padding="4px" />
                        <ButtonStyle Width="5px" Paddings-Padding="4px" />
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                var LineCode = cboMachineProccess.GetValue();
                                HF.Set('LineCode', LineCode);
                                Grid.PerformCallback('');
                            }" />
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                    &nbsp;<dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Names="Segoe UI" 
                        Font-Size="8pt" Text="Type">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="cboTypeCode" runat="server" Theme="Office2010Black" Width="200px" Height="25px" ClientInstanceName="cboTypeCode">
                        <ItemStyle Height="10px" Paddings-Padding="4px" />
                        <ButtonStyle Width="5px" Paddings-Padding="4px" />
                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                var ItemTypeCode = cboTypeCode.GetValue();
                                HF.Set('ItemTypeCode', ItemTypeCode);
                                Grid.PerformCallback('');
                            }" />
                    </dx:ASPxComboBox>        
                </td>
                <td></td>
                <td >
                    <dx:ASPxButton runat="server" ID="btnSearch" Text="Browse" OnClick="btnSearch_Click"></dx:ASPxButton>
                    &nbsp;&nbsp;
                    <dx:ASPxButton runat="server" ID="btnReset" Text="Reset" ></dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    
        <asp:SqlDataSource ID="dsFactory" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            SelectCommand="Exec sp_ItemCheckByBattery_FillCombo '1' "></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsType" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            SelectCommand="Exec sp_ItemCheckByBattery_FillCombo '2' "></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsMachine" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            SelectCommand="Exec sp_ItemCheckByBattery_FillCombo '3' "></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsFrequency" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            SelectCommand="Exec sp_ItemCheckByBattery_FillCombo '4' "></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsItemCheck" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            SelectCommand="Exec sp_ItemCheckByBattery_FillCombo '5' "></asp:SqlDataSource>


    <div style="padding: 20px 5px 5px 5px">

        <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="Grid"
            EnableTheming="True" KeyFieldName="ItemTypeName;LineCode;ItemCheck" Theme="Office2010Black" Width="100%"
            Font-Names="Segoe UI" Font-Size="9pt"
            OnRowValidating="Grid_RowValidating" OnStartRowEditing="Grid_StartRowEditing"
            OnRowInserting="Grid_RowInserting" OnRowDeleting="Grid_RowDeleting"
            OnAfterPerformCallback="Grid_AfterPerformCallback">
            <ClientSideEvents EndCallback="OnEndCallback" />
            <Columns>
                <dx:GridViewCommandColumn FixedStyle="Left"
                    VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" 
                    ShowNewButtonInHeader="true" ShowClearFilterButton="true" Width="80px">
                    <HeaderStyle Paddings-PaddingLeft="3px" HorizontalAlign="Center" 
                        VerticalAlign="Middle" >
                        <Paddings PaddingLeft="3px"></Paddings>
                    </HeaderStyle>
                </dx:GridViewCommandColumn>

                <dx:GridViewDataTextColumn Caption="Factory Code" FieldName="FactoryCode"
                    VisibleIndex="0" Width="100px" Settings-AutoFilterCondition="Contains" 
                    FixedStyle="Left" Visible="false">
                    <PropertiesTextEdit MaxLength="15" Width="120px">
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
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Item Type Code" FieldName="ItemTypeCode"
                    VisibleIndex="0" Width="100px" Settings-AutoFilterCondition="Contains" 
                    FixedStyle="Left" Visible="false">
                    <PropertiesTextEdit MaxLength="15" Width="120px">
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
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataComboBoxColumn Caption="Type" FieldName="ItemTypeName" VisibleIndex="1"
                    Width="200px" Settings-AutoFilterCondition="Contains">
                    <PropertiesComboBox DataSourceID="dsType" DropDownStyle="DropDownList" TextFormatString="{0}"
                        IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="195px"
                        TextField="ItemTypeName" ValueField="ItemTypeCode" ClientInstanceName="ItemTypeCode">
                        <ItemStyle Height="10px" Paddings-Padding="4px">
                            <Paddings Padding="4px"></Paddings>
                        </ItemStyle>
                        <ButtonStyle Width="5px" Paddings-Padding="2px">
                            <Paddings Padding="2px"></Paddings>
                        </ButtonStyle>
                    </PropertiesComboBox>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"/>
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataComboBoxColumn Caption="Machine Process" FieldName="LineCode" VisibleIndex="2"
                    Width="200px" Settings-AutoFilterCondition="Contains">
                    <PropertiesComboBox DataSourceID="dsMachine" DropDownStyle="DropDownList" TextFormatString="{0}"
                        IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="195px"
                        TextField="LineName" ValueField="LineCode" ClientInstanceName="LineCode">
                        <ItemStyle Height="10px" Paddings-Padding="4px">
                            <Paddings Padding="4px"></Paddings>
                        </ItemStyle>
                        <ButtonStyle Width="5px" Paddings-Padding="2px">
                            <Paddings Padding="2px"></Paddings>
                        </ButtonStyle>
                    </PropertiesComboBox>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"/>
                </dx:GridViewDataComboBoxColumn>
                
                <dx:GridViewDataComboBoxColumn Caption="Item Check" FieldName="ItemCheck" VisibleIndex="3"
                    Width="200px" Settings-AutoFilterCondition="Contains">
                    <PropertiesComboBox DataSourceID="dsItemCheck" DropDownStyle="DropDownList" TextFormatString="{0}"
                        IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="195px"
                        TextField="ItemCheck" ValueField="ItemCheckCode" ClientInstanceName="ItemCheckCode">
                        <ItemStyle Height="10px" Paddings-Padding="4px">
                            <Paddings Padding="4px"></Paddings>
                        </ItemStyle>
                        <ButtonStyle Width="5px" Paddings-Padding="2px">
                            <Paddings Padding="2px"></Paddings>
                        </ButtonStyle>
                    </PropertiesComboBox>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"/>
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataComboBoxColumn Caption="Frequency" FieldName="FrequencyCode" VisibleIndex="4"
                    Width="200px" Settings-AutoFilterCondition="Contains">
                    <PropertiesComboBox DataSourceID="dsFrequency" DropDownStyle="DropDownList" TextFormatString="{0}"
                        IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="195px"
                        TextField="FrequencyName" ValueField="FrequencyCode" ClientInstanceName="FrequencyCode">
                        <ItemStyle Height="10px" Paddings-Padding="4px">
                            <Paddings Padding="4px"></Paddings>
                        </ItemStyle>
                        <ButtonStyle Width="5px" Paddings-Padding="2px">
                            <Paddings Padding="2px"></Paddings>
                        </ButtonStyle>
                    </PropertiesComboBox>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"/>
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataTextColumn Caption="Registratrion No" FieldName="RegistrationNo"
                    VisibleIndex="5" Width="150px" Settings-AutoFilterCondition="Contains">
                    <PropertiesTextEdit MaxLength="50" Width="200px">
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
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Sample Size" FieldName="SampleSize"
                    VisibleIndex="6" Width="150px" Settings-AutoFilterCondition="Contains">
                    <PropertiesTextEdit MaxLength="50" Width="200px">
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
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Remark" FieldName="Remark"
                    VisibleIndex="7" Width="150px" Settings-AutoFilterCondition="Contains">
                    <PropertiesTextEdit MaxLength="50" Width="200px">
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
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Evaluation" FieldName="Evaluation"
                    VisibleIndex="8" Width="150px" Settings-AutoFilterCondition="Contains">
                    <PropertiesTextEdit MaxLength="50" Width="200px">
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
                </dx:GridViewDataTextColumn>
                                
                <dx:GridViewDataCheckColumn Caption="Characteristic Item" FieldName="CharacteristicStatus" 
                    VisibleIndex="9" Width="60px">
                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Char" 
                        ValueUnchecked="0">
                    </PropertiesCheckEdit>
                    <Settings AllowSort="False" />
                </dx:GridViewDataCheckColumn>
                
                <dx:GridViewDataCheckColumn Caption="Active Status" FieldName="ActiveStatus" 
                    VisibleIndex="10" Width="60px">
                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Char" 
                        ValueUnchecked="0">
                    </PropertiesCheckEdit>
                    <Settings AllowSort="False" />
                </dx:GridViewDataCheckColumn>

                <dx:GridViewDataTextColumn Caption="Last User" FieldName="UpdateUser"
                    VisibleIndex="11" Width="150px" Settings-AutoFilterCondition="Contains">
                    <PropertiesTextEdit MaxLength="50" Width="200px">
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
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Last Update" FieldName="UpdateDate"
                    VisibleIndex="12" Width="150px" Settings-AutoFilterCondition="Contains">
                    <PropertiesTextEdit MaxLength="50" Width="200px">
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
                </dx:GridViewDataTextColumn>
                                
            </Columns>

            <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="Control" />
            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
            <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true">
                <PageSizeItemSettings Visible="True" />
            </SettingsPager>
            <Settings ShowFilterRow="True" VerticalScrollBarMode="Auto"
                VerticalScrollableHeight="300" HorizontalScrollBarMode="Auto" />
            <SettingsText ConfirmDelete="Are you sure want to delete ?"></SettingsText>
            <SettingsPopup>
                <EditForm Modal="false" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Width="200" />
            </SettingsPopup>

            <Styles EditFormColumnCaption-Paddings-PaddingLeft="10px" EditFormColumnCaption-Paddings-PaddingRight="10px">
                <Header Wrap="True">
                    <Paddings Padding="2px"></Paddings>
                </Header>

                <EditFormColumnCaption Font-Size="9pt" Font-Names="Segoe UI">
                    <Paddings PaddingLeft="5px" PaddingTop="5px" PaddingBottom="5px"></Paddings>
                </EditFormColumnCaption>
            </Styles>

            <Templates>
                <EditForm>
                    <div style="padding: 15px 15px 15px 15px; width: 300px">
                        <dx:ContentControl ID="ContentControl1" runat="server">
                            <table align="center">
                                <tr style="height:25px">
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="Factory Code" Width="90px"></dx:ASPxLabel>
                                    </td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editFactoryCode" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="FactoryCode" >
                                        </dx:ASPxGridViewTemplateReplacement> 
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>
                                        <%--<dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="Type Code" Width="90px"></dx:ASPxLabel>--%>
                                    </td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editItemTypeCode" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="ItemTypeCode" >
                                        </dx:ASPxGridViewTemplateReplacement> 
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="Type Name" Width="90px"></dx:ASPxLabel>
                                    </td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editItemTypeName" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="ItemTypeName">
                                        </dx:ASPxGridViewTemplateReplacement>                                    
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>Machine Process</td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editLineCode" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="LineCode">
                                        </dx:ASPxGridViewTemplateReplacement>   
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>Item Check</td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editItemCheck" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="ItemCheck">
                                        </dx:ASPxGridViewTemplateReplacement>                                    
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>Frequency</td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editFrequencyCode" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="FrequencyCode">
                                        </dx:ASPxGridViewTemplateReplacement>                                    
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>Registration No</td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editRegistrationNo" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="RegistrationNo">
                                        </dx:ASPxGridViewTemplateReplacement>                                    
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>Sample Size</td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editSampleSize" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="SampleSize">
                                        </dx:ASPxGridViewTemplateReplacement>                                    
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>Remark</td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editRemark" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="Remark">
                                        </dx:ASPxGridViewTemplateReplacement>                                    
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>Evaluation</td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editEvaluation" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="Evaluation">
                                        </dx:ASPxGridViewTemplateReplacement>                                    
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>Characteristic Item</td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editCharacteristicStatus" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="CharacteristicStatus">
                                        </dx:ASPxGridViewTemplateReplacement>                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Active Status
                                    </td>
                                    <td>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxGridViewTemplateReplacement ID="editActiveStatus" ReplacementType="EditFormCellEditor"
                                                runat="server" ColumnID="ActiveStatus">
                                            </dx:ASPxGridViewTemplateReplacement>       
                                        </dx:LayoutItemNestedControlContainer>                                    
                                    </td>
                                </tr>
                            </table>
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
            </Templates>

        </dx:ASPxGridView>
        <dx:ASPxHiddenField ID="HF" runat="server" ClientInstanceName="HF"></dx:ASPxHiddenField>
    </div>
</asp:Content>
