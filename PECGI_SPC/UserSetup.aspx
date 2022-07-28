<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserSetup.aspx.vb" Inherits="PECGI_SPC.UserSetup" %>

<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ MasterType VirtualPath="~/Site.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script type="text/javascript" >
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
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<div style="padding: 5px 5px 5px 5px">
         <div style="padding: 5px 5px 5px 5px">
        <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="Grid"
            EnableTheming="True" KeyFieldName="UserID" Theme="Office2010Black" 
            OnRowValidating="Grid_RowValidating" OnStartRowEditing="Grid_StartRowEditing"
            OnRowInserting="Grid_RowInserting" OnRowDeleting="Grid_RowDeleting" 
            OnAfterPerformCallback="Grid_AfterPerformCallback" Width="100%"
            Font-Names="Segoe UI" Font-Size="9pt" >
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

                <dx:GridViewDataTextColumn Caption="User ID" FieldName="UserID"
                    VisibleIndex="7" Width="100px" Settings-AutoFilterCondition="Contains" 
                    FixedStyle="Left">
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

                <dx:GridViewDataTextColumn Caption="Full Name" FieldName="FullName"
                    VisibleIndex="8" Width="150px" Settings-AutoFilterCondition="Contains">
                    <PropertiesTextEdit MaxLength="30" Width="200px">
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>

<Settings AutoFilterCondition="Contains"></Settings>

                    <FilterCellStyle Paddings-PaddingRight="4px">
<Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="6px" HorizontalAlign="Center" VerticalAlign="Middle">
<Paddings PaddingLeft="6px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Description" FieldName="Description"
                    VisibleIndex="13" Width="150px" Settings-AutoFilterCondition="Contains">
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
                
                <dx:GridViewDataTextColumn Caption="Password" FieldName="Password" 
                    VisibleIndex="10" Visible="False">
                    <PropertiesTextEdit MaxLength="100" Width="200px">
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="True" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn Caption="Locked" FieldName="LockStatus" 
                    VisibleIndex="26" Width="60px">
                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Char" 
                        ValueUnchecked="0">
                    </PropertiesCheckEdit>
                    <Settings AllowSort="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn Caption="Admin" FieldName="AdminStatus" 
                    VisibleIndex="15" Width="60px">
                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Char" 
                        ValueUnchecked="0">
                    </PropertiesCheckEdit>
                    <Settings AllowSort="False" />
                </dx:GridViewDataCheckColumn>
                
                <dx:GridViewDataTextColumn Caption="Privileges" FieldName="Privileges" 
                    VisibleIndex="5" FixedStyle="Left" Width="70px">
                    <Settings AllowAutoFilter="False" AllowSort="False" />
                    <EditFormSettings Visible="False" />
                    <DataItemTemplate>
                        <dx:ASPxHyperLink ID="hyperlink02" Font-Names="Segoe UI" Font-Size="9pt" 
                            runat="server" Text=<%#Eval("Privileges")%> OnInit="PrivilegesLink_Init">
                        </dx:ASPxHyperLink>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                
                <dx:GridViewDataCheckColumn FieldName="LineLeaderStatus" VisibleIndex="17" 
                    Caption="Line Leader" Width="60px">
                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Char" 
                        ValueUnchecked="0">
                    </PropertiesCheckEdit>
                    <Settings AllowSort="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn FieldName="LineForemanStatus" VisibleIndex="19" 
                    Caption="Line Foreman" Width="60px">
                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Char" 
                        ValueUnchecked="0">
                    </PropertiesCheckEdit>
                    <Settings AllowSort="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn FieldName="ProdSectionHeadStatus" VisibleIndex="21" 
                    Caption="Prod Section Head" Width="60px">
                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Char" 
                        ValueUnchecked="0">
                    </PropertiesCheckEdit>
                    <Settings AllowSort="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn FieldName="QELeaderStatus" VisibleIndex="23" 
                    Caption="QE Leader" Width="60px">
                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Char" 
                        ValueUnchecked="0">
                    </PropertiesCheckEdit>
                    <Settings AllowSort="False" />
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn FieldName="QESectionHeadStatus" VisibleIndex="25" 
                    Caption="QE Section Head" Width="60px">
                    <PropertiesCheckEdit ValueChecked="1" ValueType="System.Char" 
                        ValueUnchecked="0">
                    </PropertiesCheckEdit>
                    <Settings AllowSort="False" />
                </dx:GridViewDataCheckColumn>
                
                <dx:GridViewDataTextColumn Caption="Lines" FieldName="LinePrivileges" 
                    FixedStyle="Left" VisibleIndex="6" Width="55px">
                    <Settings AllowAutoFilter="False" AllowSort="False" />
                    <EditFormSettings Visible="False" />
                    <DataItemTemplate>
                        <dx:ASPxHyperLink ID="hyperlink02" Font-Names="Segoe UI" Font-Size="9pt" 
                            runat="server" Text=<%#Eval("LinePrivileges")%> OnInit="LinePrivilegesLink_Init">
                        </dx:ASPxHyperLink>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                
            </Columns>

            <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="Control" />
            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm"/>
            <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true">
                <PageSizeItemSettings Visible="True">
                </PageSizeItemSettings>
            </SettingsPager>
            <Settings ShowFilterRow="True" VerticalScrollBarMode="Auto" 
                VerticalScrollableHeight="300" HorizontalScrollBarMode="Auto" />
            <SettingsText ConfirmDelete="Are you sure want to delete ?"></SettingsText>
            <SettingsPopup>
                <EditForm Modal="false" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Width="200" />
            </SettingsPopup>

            <Styles EditFormColumnCaption-Paddings-PaddingLeft="10px" EditFormColumnCaption-Paddings-PaddingRight="10px" >
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
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="User ID" Width="90px"></dx:ASPxLabel>
                                    </td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editUserID" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="UserID">
                                        </dx:ASPxGridViewTemplateReplacement>                                    
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>Full Name</td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editFullName" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="FullName">
                                        </dx:ASPxGridViewTemplateReplacement>                                    
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>Password</td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editPassword" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="Password">
                                        </dx:ASPxGridViewTemplateReplacement>                                    
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                    <td>Description</td>                                
                                    <td>
                                        <dx:ASPxGridViewTemplateReplacement ID="editDescription" ReplacementType="EditFormCellEditor"
                                            runat="server" ColumnID="Description">
                                        </dx:ASPxGridViewTemplateReplacement>                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Admin Status
                                    </td>
                                    <td>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxGridViewTemplateReplacement ID="editAdminStatus" ReplacementType="EditFormCellEditor"
                                                runat="server" ColumnID="AdminStatus">
                                            </dx:ASPxGridViewTemplateReplacement>                                            
                                        </dx:LayoutItemNestedControlContainer>                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Lock Status
                                    </td>
                                    <td>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxGridViewTemplateReplacement ID="editLockStatus" ReplacementType="EditFormCellEditor"
                                                runat="server" ColumnID="LockStatus">
                                            </dx:ASPxGridViewTemplateReplacement>                                            
                                        </dx:LayoutItemNestedControlContainer>                                    
                                    </td>
                                </tr>
                            </table>

                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Approval" SettingsItemHelpTexts-Position="Bottom" Width="100%">
                                    <Items>
                                        <dx:LayoutItem Caption="Line Leader Status">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxGridViewTemplateReplacement ID="editLineLeaderStatus" ReplacementType="EditFormCellEditor"
                                                        runat="server" ColumnID="LineLeaderStatus">
                                                    </dx:ASPxGridViewTemplateReplacement>                                            
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Line Foreman Status">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxGridViewTemplateReplacement ID="editLineForemanStatus" ReplacementType="EditFormCellEditor"
                                                        runat="server" ColumnID="LineForemanStatus">
                                                    </dx:ASPxGridViewTemplateReplacement>                                            
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Prod Section Head Status">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxGridViewTemplateReplacement ID="editProdSectionHeadStatus" ReplacementType="EditFormCellEditor"
                                                        runat="server" ColumnID="ProdSectionHeadStatus">
                                                    </dx:ASPxGridViewTemplateReplacement>                                            
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="QE Leader Status">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxGridViewTemplateReplacement ID="editQELeaderStatus" ReplacementType="EditFormCellEditor"
                                                        runat="server" ColumnID="QELeaderStatus">
                                                    </dx:ASPxGridViewTemplateReplacement>                                            
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="QE Section Head Status">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxGridViewTemplateReplacement ID="editQESectionHeadStatus" ReplacementType="EditFormCellEditor"
                                                        runat="server" ColumnID="QESectionHeadStatus">
                                                    </dx:ASPxGridViewTemplateReplacement>                                            
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>                                        
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                            </dx:ASPxFormLayout>
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
    </div> 
</div>
     </asp:Content>
