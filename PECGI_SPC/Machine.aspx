<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Machine.aspx.vb" Inherits="PECGI_SPC.Machine" %>
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
    }
}    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<div style="padding: 5px 5px 5px 5px">
    <dx:ASPxGridView ID="GridMenu" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridMenu"
        EnableTheming="True" KeyFieldName="MachineNo" Theme="Office2010Black" 
        Width="100%" OnAfterPerformCallback="GridMenu_AfterPerformCallback" 
        OnRowDeleting="GridMenu_RowDeleting" 
        OnRowInserting="GridMenu_RowInserting" 
        OnRowValidating="GridMenu_RowValidating" 
        OnStartRowEditing="GridMenu_StartRowEditing" 
        Font-Names="Segoe UI" Font-Size="9pt" >
        <ClientSideEvents EndCallback="OnEndCallback" />
        
        <Columns>
            <dx:GridViewCommandColumn FixedStyle="Left"
                VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" 
                ShowNewButtonInHeader="true" ShowClearFilterButton="true" 
                ShowInCustomizationForm="True">
                <HeaderStyle Paddings-PaddingLeft="3px" HorizontalAlign="Center" 
                    VerticalAlign="Middle" >
                    <Paddings PaddingLeft="3px"></Paddings>
                </HeaderStyle>
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="Machine No" FieldName="MachineNo"
                VisibleIndex="1" Width="100px" Settings-AutoFilterCondition="Contains" 
                FixedStyle="Left" ShowInCustomizationForm="True">
                <PropertiesTextEdit MaxLength="15" Width="100px">
                    <Style HorizontalAlign="Left"></Style>
                </PropertiesTextEdit>
                <Settings AutoFilterCondition="Contains"></Settings>
                <editformsettings visibleindex="0"/>
                <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                </FilterCellStyle>
                <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="5px"></Paddings>
                </HeaderStyle>
                <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="Remark" FieldName="Remark"
                VisibleIndex="2" Width="175px" Settings-AutoFilterCondition="Contains" 
                FixedStyle="Left" ShowInCustomizationForm="True">
                <PropertiesTextEdit MaxLength="35" Width="175px">
                    <Style HorizontalAlign="Left"></Style>
                </PropertiesTextEdit>
                <Settings AutoFilterCondition="Contains"></Settings>
                <editformsettings visibleindex="2" />
                <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                </FilterCellStyle>
                <HeaderStyle Paddings-PaddingLeft="6px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="6px"></Paddings>
                </HeaderStyle>
                <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
            </dx:GridViewDataTextColumn>
            </Columns>

            <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="Control" />
            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm"/>
            <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true" PageSizeItemSettings-Visible="true"></SettingsPager>
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
                    
         </dx:ASPxGridView>
         </div>
</asp:Content>
