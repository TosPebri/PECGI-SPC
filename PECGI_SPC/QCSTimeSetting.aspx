<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="QCSTimeSetting.aspx.vb" Inherits="PECGI_SPC.QCSTimeSetting" %>
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
    }

    function ClickSave(s, e) {
        GridMenu.UpdateEdit();
    }

	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <dx:ASPxGridView ID="GridMenu" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridMenu"
            EnableTheming="True" KeyFieldName="Shift1;Shift2;Shift3" Theme="Office2010Black" 
            Width="100%" 
            OnRowInserting="GridMenu_RowInserting" OnRowDeleting="GridMenu_RowDeleting" 
            Font-Names="Segoe UI" Font-Size="9pt">
            <ClientSideEvents EndCallback="OnEndCallback" />
        <Columns>

            <dx:GridViewCommandColumn FixedStyle="Left" ShowDeleteButton="true" 
            ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0" 
                Width="100px">
            </dx:GridViewCommandColumn>

              <dx:GridViewBandColumn VisibleIndex="1" Caption="Shift 1">
                  <Columns>
                      <dx:GridViewDataTextColumn Caption="1" FieldName="Shift1Cycle1" VisibleIndex="1" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                          <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                      </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn Caption="2" FieldName="Shift1Cycle2" VisibleIndex="2" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                           <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                      </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn Caption="4" FieldName="Shift1Cycle4" VisibleIndex="4" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                           <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                      </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn Caption="5" FieldName="Shift1Cycle5" VisibleIndex="5" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                           <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                      </dx:GridViewDataTextColumn>
                      <dx:GridViewDataTextColumn Caption="3" FieldName="Shift1Cycle3" VisibleIndex="3" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                           <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                      </dx:GridViewDataTextColumn>
                  </Columns>
                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewBandColumn>

            <dx:GridViewBandColumn Caption="Shift 2" VisibleIndex="2">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="1" FieldName="Shift2Cycle1" VisibleIndex="1" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="2" FieldName="Shift2Cycle2" VisibleIndex="2" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="3" FieldName="Shift2Cycle3" VisibleIndex="3" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="4" FieldName="Shift2Cycle4" VisibleIndex="4" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="5" FieldName="Shift2Cycle5" VisibleIndex="5" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn VisibleIndex="3" Caption="Shift 3">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="1" FieldName="Shift3Cycle1" VisibleIndex="1" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="2" FieldName="Shift3Cycle2" VisibleIndex="2" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="3" FieldName="Shift3Cycle3" VisibleIndex="3" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="4" FieldName="Shift3Cycle4" VisibleIndex="4" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="5" FieldName="Shift3Cycle5" VisibleIndex="5" PropertiesTextEdit-MaskSettings-Mask="<00..23>:<00..59>" Width="65px">
<PropertiesTextEdit>
</PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                         <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                          </CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewBandColumn>

        </Columns>
        <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" AllowSort="false"/>
        <SettingsEditing EditFormColumnCount="1" Mode="Batch" />
        <SettingsPager AlwaysShowPager="true" Mode="ShowPager" PageSize="30">
        </SettingsPager>
        <Settings HorizontalScrollBarMode="Auto" 
            VerticalScrollBarMode="Auto" />
        <SettingsText ConfirmDelete="Are you sure want to delete ?" />
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="false" 
                VerticalAlign="WindowCenter" Width="320" />
        </SettingsPopup>
            <SettingsDataSecurity AllowDelete="False" />
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

  <div style="padding: 10px 5px 5px 5px">
   <table style="width: 100%;">
        <tr>
            <td style=" width:100px">
                &nbsp;</td>
            <td style=" width:100px">&nbsp;</td>
            <td style=" width:100px">&nbsp;</td>
            <td style=" width:100px">&nbsp;</td>
            <td style=" width:100px">&nbsp;</td>
            <td style=" width:100px">&nbsp;</td>
            <td style=" width:100px">&nbsp;</td>
            <td style=" width:100px">&nbsp;</td>
            <td style=" width:100px">&nbsp;</td>
            <td style=" width:100px">&nbsp;</td>
            <td style=" width:100px">
                &nbsp;</td>
            <td style=" width:100px">
                <dx:ASPxButton ID="btnSave" runat="server" Text="Save" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnSave" Theme="Default">
                    <ClientSideEvents Click="ClickSave"/>
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
        </tr>
        </table>
  </div>
    
</asp:Content>
