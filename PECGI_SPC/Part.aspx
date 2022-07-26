<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Part.aspx.vb" Inherits="PECGI_SPC.Part" %>
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
            if (s.IsEditing()) {
            var form = s.GetPopupEditForm();
            form.PopUp.AddHandler(function(s,e) {
            var editor = Grid.GetEditor('PartID');
            editor.Focus();
            });
            }

        }

           function SelectLine(s, e) {
                LineName.SetText(LineID.GetSelectedItem().GetColumnText(1));
                LineName.inputElement.style.color = 'Silver';
//               Grid.PerformCallback('load|');
        }  
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<div style="padding: 5px 5px 5px 5px">
         <div style="padding: 5px 5px 5px 5px">
        <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="Grid"
            EnableTheming="True" KeyFieldName="PartID" Theme="Office2010Black" 
             Width="100%"
            Font-Names="Segoe UI" Font-Size="9pt" >
            <ClientSideEvents EndCallback="OnEndCallback" />

            <Columns>
                <dx:GridViewCommandColumn FixedStyle="Left"
                    VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" 
                    ShowNewButtonInHeader="true" ShowClearFilterButton="True">
                    <HeaderStyle Paddings-PaddingLeft="3px" HorizontalAlign="Center" 
                        VerticalAlign="Middle" >
                        <Paddings PaddingLeft="3px"></Paddings>
                    </HeaderStyle>
                </dx:GridViewCommandColumn>

                <dx:GridViewDataTextColumn Caption="Part ID" FieldName="PartID"
                    VisibleIndex="1" Width="200px" Settings-AutoFilterCondition="Contains" 
                    >
                    <PropertiesTextEdit MaxLength="25" Width="200px">
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <EditFormSettings VisibleIndex="1" />
                    <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Part Name" FieldName="PartName"
                    VisibleIndex="2" Width="350px" Settings-AutoFilterCondition="Contains" 
                    >
                    <PropertiesTextEdit MaxLength="35" Width="350px">
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <EditFormSettings VisibleIndex="2" />
                    <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>

                <%--<dx:GridViewDataComboBoxColumn Caption="LineID" FieldName="LineID" 
                    VisibleIndex="3" Width="75px" ReadOnly="False">
                    <EditFormSettings VisibleIndex="3" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="75px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="StartsWith" 
                                    TextField="LineID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="LineID"
                                    DataSourceID="SqlDataSource1"
                                    ClientInstanceName="LineID">
                                    <ClientSideEvents SelectedIndexChanged="SelectLine" />
                                    <Columns>
                                        <dx:listboxcolumn Caption="Line ID" FieldName="LineID" Width="68px"/>
                                        <dx:listboxcolumn Caption="Line Name" FieldName="LineName" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataTextColumn Caption="Line Name" FieldName="LineName"
                    VisibleIndex="4" Width="150px" Settings-AutoFilterCondition="Contains" 
                    >
                    <PropertiesTextEdit MaxLength="35" Width="140px" ClientInstanceName="LineName">
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <EditFormSettings VisibleIndex="4" />
                    <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>--%>
                
                <%--<dx:GridViewDataComboBoxColumn Caption="SubLineID" FieldName="SubLineID" 
                    VisibleIndex="5" Width="80px">
                    <EditFormSettings VisibleIndex="5" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="80px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="StartsWith" 
                                    TextField="SubLineID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="SubLineID"
                                    ClientInstanceName="SubLineID">

                                    <Columns>
                                        <dx:listboxcolumn Caption="Sub Line ID" FieldName="SubLineID" Width="68px"/>
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>--%>

                <dx:GridViewDataTextColumn Caption="Remark" FieldName="Remark"
                    VisibleIndex="6" Width="200px" Settings-AutoFilterCondition="Contains" 
                    >
                    <PropertiesTextEdit MaxLength="25" Width="200px" ClientInstanceName="Remark">
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <EditFormSettings VisibleIndex="3" />
                    <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataComboBoxColumn Caption="Active Status" FieldName="ActiveStatus" 
                    VisibleIndex="7" Visible="False">
                    <PropertiesComboBox DropDownStyle="DropDown" Width="70px" TextFormatString="{0}"
                    IncrementalFilteringMode="StartsWith" TextField="ActiveStatus" DisplayFormatInEditMode="true"
                    ValueField="ActiveStatus" ClientInstanceName="ActiveStatus" >
                    <Items>
                        <dx:ListEditItem Text="Active" Value="1" />
                        <dx:ListEditItem Text="Inactive" Value="0"/>
                    </Items>
                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                        <Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Width="5px" Paddings-Padding="2px" >
                        <Paddings Padding="2px"></Paddings>
                    </ButtonStyle>
                </PropertiesComboBox>
                    <EditFormSettings Visible="True" VisibleIndex="4" />
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataTextColumn Caption="Active Status" FieldName="ActiveStatus1"
                    VisibleIndex="8" Width="85px" Settings-AutoFilterCondition="Contains" >
<Settings AutoFilterCondition="Contains"></Settings>

                    <EditFormSettings Visible="False" />
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>
            </Columns>

            <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="Control" />
            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm"/>
            <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true" PageSizeItemSettings-Visible="true">
<PageSizeItemSettings Visible="True"></PageSizeItemSettings>
            </SettingsPager>
            <Settings ShowFilterRow="True" VerticalScrollBarMode="Auto" 
                VerticalScrollableHeight="300" HorizontalScrollBarMode="Auto" />
            <SettingsText ConfirmDelete="Are you sure want to delete ?"></SettingsText>
            <SettingsPopup>
                <EditForm Modal="false" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Width="320" />
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
            </Templates>
                    
         </dx:ASPxGridView>
         </div> 

          <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT RTRIM(LineID) AS LineID, RTRIM(LineName) AS LineName FROM Line">

    </asp:SqlDataSource>

   <%--         <asp:SqlDataSource ID="SqlDataSource2" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT RTRIM(SubLineID) AS SubLineID, RTRIM(LineID) AS LineID FROM [SubLine]">
            </asp:SqlDataSource>--%>

</div>
</asp:Content>
