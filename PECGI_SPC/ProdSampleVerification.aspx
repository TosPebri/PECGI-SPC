<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProdSampleVerification.aspx.vb" Inherits="PECGI_SPC.ProdSampleVerification" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div style="padding: 5px 5px 5px 5px; padding-bottom: 20px; border-bottom: groove">
        <table class="auto-style3">
            <tr style="height: 40px">
                <td>
                    <dx:ASPxLabel ID="lblFactory" runat="server" Font-Names="Segoe UI" Font-Size="9pt" Text="Factory">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboFactory" runat="server" Font-Names="Segoe UI" DropDownStyle="DropDownList" IncrementalFilteringMode="Contains"
                        Font-Size="9pt" Theme="Office2010Black" EnableTheming="True" Height="25px" EnableIncrementalFiltering="True" TextFormatString="{1}"
                        TextField="CODENAME" ValueField="CODE" ClientInstanceName="cboFactory" SelectedIndex="0">
                        <%--<ClientSideEvents SelectedIndexChanged="ChangeFactory" />--%>
                        <Columns>
                            <dx:ListBoxColumn Caption="CODE" FieldName="CODE" Width="60px" />
                            <dx:ListBoxColumn Caption="CODE NAME" FieldName="CODENAME" Width="120px" />
                        </Columns>
                        <ItemStyle Height="10px" Paddings-Padding="4px" Font-Size="11px" />
                        <ButtonStyle Paddings-Padding="4px" Width="5px">
                        </ButtonStyle>
                    </dx:ASPxComboBox>
                </td>

                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblLineID" runat="server" Font-Names="Segoe UI" Font-Size="9pt" Text="Machine Process">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboLineID" runat="server" Font-Names="Segoe UI" DropDownStyle="DropDownList" IncrementalFilteringMode="Contains"
                        Font-Size="9pt" Theme="Office2010Black" EnableTheming="True" Height="25px" EnableIncrementalFiltering="True" TextFormatString="{1}"
                        TextField="CODENAME" ValueField="CODE" ClientInstanceName="cboLineID" SelectedIndex="0">
                        <%--<ClientSideEvents SelectedIndexChanged="ChangeLine" />--%>
                        <Columns>
                            <dx:ListBoxColumn Caption="CODE" FieldName="CODE" Width="60px" />
                            <dx:ListBoxColumn Caption="CODE NAME" FieldName="CODENAME" Width="120px" />
                        </Columns>
                        <ItemStyle Height="10px" Paddings-Padding="4px" Font-Size="11px" />
                        <ButtonStyle Paddings-Padding="4px" Width="5px">
                        </ButtonStyle>
                    </dx:ASPxComboBox>
                </td>

                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblFromDate" runat="server" Font-Names="Segoe UI" Font-Size="9pt" Text="Date">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 20px">&nbsp;</td>
                <td colspan="5">
                    <dx:ASPxDateEdit ID="dtProdDate" runat="server" Theme="Office2010Black" AutoPostBack="false" Width="155px"
                        ClientInstanceName="dtProdDate" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="5">
                        <%--<ClientSideEvents Init="OnInitProdDate" />--%>
                        <CalendarProperties>
                            <HeaderStyle Font-Size="12pt" Paddings-Padding="5px" />
                            <DayStyle Font-Size="9pt" Paddings-Padding="5px" />
                            <WeekNumberStyle Font-Size="9pt" Paddings-Padding="5px"></WeekNumberStyle>
                            <FooterStyle Font-Size="9pt" Paddings-Padding="10px" />
                            <ButtonStyle Font-Size="9pt" Paddings-Padding="10px"></ButtonStyle>
                        </CalendarProperties>
                        <ButtonStyle Width="5px" Paddings-Padding="4px"></ButtonStyle>
                    </dx:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    <dx:ASPxLabel ID="lblItemType" runat="server" Font-Names="Segoe UI" Font-Size="9pt" Text="Type">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboItemType" runat="server" Font-Names="Segoe UI" DropDownStyle="DropDownList" IncrementalFilteringMode="Contains"
                        Font-Size="9pt" Theme="Office2010Black" EnableTheming="True" Height="25px" EnableIncrementalFiltering="True" SelectedIndex="0" TextFormatString="{1}"
                        TextField="CODENAME" ValueField="CODE" ClientInstanceName="cboItemType">
                        <%--<ClientSideEvents SelectedIndexChanged="ChangeItemType" />--%>
                        <Columns>
                            <dx:ListBoxColumn Caption="CODE" FieldName="CODE" Width="60px" />
                            <dx:ListBoxColumn Caption="CODE NAME" FieldName="CODENAME" Width="120px" />
                        </Columns>
                        <ItemStyle Height="10px" Paddings-Padding="4px" Font-Size="11px" />
                        <ButtonStyle Paddings-Padding="4px" Width="5px">
                        </ButtonStyle>
                    </dx:ASPxComboBox>
                </td>

                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblItemCheck" runat="server" Font-Names="Segoe UI" Font-Size="9pt" Text="Item Check">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboItemCheck" runat="server" Font-Names="Segoe UI" DropDownStyle="DropDownList" IncrementalFilteringMode="Contains"
                        Font-Size="9pt" Theme="Office2010Black" EnableTheming="True" Height="25px" EnableIncrementalFiltering="True" SelectedIndex="0"
                        TextFormatString="{1}" TextField="CODENAME" ValueField="CODE" ClientInstanceName="cboItemCheck">
                        <%--<ClientSideEvents SelectedIndexChanged="ChangeItemCheck" />--%>
                        <Columns>
                            <dx:ListBoxColumn Caption="CODE" FieldName="CODE" Width="60px" />
                            <dx:ListBoxColumn Caption="CODE NAME" FieldName="CODENAME" Width="120px" />
                        </Columns>
                        <ItemStyle Height="10px" Paddings-Padding="4px" Font-Size="11px" />
                        <ButtonStyle Paddings-Padding="4px" Width="5px">
                        </ButtonStyle>
                    </dx:ASPxComboBox>
                </td>

                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblShift" runat="server" Font-Names="Segoe UI" Font-Size="9pt" Text="Shift">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboShift" runat="server" Font-Names="Segoe UI" Height="25px" DropDownStyle="DropDownList" IncrementalFilteringMode="Contains"
                        ClientInstanceName="cboShift" Font-Size="9pt" Theme="Office2010Black" TextField="CODENAME" ValueField="CODE" EnableTheming="True" Width="60px"
                        EnableIncrementalFiltering="True" TextFormatString="{0}" SelectedIndex="0">
                        <Columns>
                            <dx:ListBoxColumn Caption="CODE" FieldName="CODE" Width="60px" />
                            <dx:ListBoxColumn Caption="CODE NAME" FieldName="CODENAME" Width="120px" />
                        </Columns>
                        <ItemStyle Height="10px" Paddings-Padding="4px" Font-Size="11px" />
                        <ButtonStyle Paddings-Padding="4px" Width="5px">
                        </ButtonStyle>
                    </dx:ASPxComboBox>
                </td>

                <td style="width: 5px">&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblSeq" runat="server" Font-Names="Segoe UI" Font-Size="9pt" Text="Seq">
                    </dx:ASPxLabel>
                </td>

                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboSeq" runat="server" Font-Names="Segoe UI" Height="25px" DropDownStyle="DropDownList" IncrementalFilteringMode="Contains"
                        ClientInstanceName="cboSeq" Font-Size="9pt" Theme="Office2010Black" EnableTheming="True" Width="60px" EnableIncrementalFiltering="True"
                        TextFormatString="{0}" SelectedIndex="0" TextField="CODENAME" ValueField="CODE">
                        <Columns>
                            <dx:ListBoxColumn Caption="CODE" FieldName="CODE" Width="60px" />
                            <dx:ListBoxColumn Caption="CODE NAME" FieldName="CODENAME" Width="120px" />
                        </Columns>
                        <ItemStyle Height="10px" Paddings-Padding="4px" Font-Size="11px" />
                        <ButtonStyle Paddings-Padding="4px" Width="5px">
                        </ButtonStyle>
                    </dx:ASPxComboBox>
                </td>

                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnBrowse" runat="server" AutoPostBack="False" ClientInstanceName="btnBrowse"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="Browse" Theme="Office2010Silver" Width="80px">
                        <%--<ClientSideEvents Click="Browse" />--%>
                    </dx:ASPxButton>
                </td>
                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnClear" runat="server" AutoPostBack="False" ClientInstanceName="btnClear"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="Clear" Theme="Office2010Silver" Width="80px">
                      <%--  <ClientSideEvents Click="Clear" />--%>
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding: 5px 5px 5px 5px; padding-top: 20px; padding-bottom: 20px">
        <table>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnVerification" runat="server" AutoPostBack="False" ClientInstanceName="btnVerification"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="Verify" Theme="Office2010Silver" Width="100px">
                        <%--<ClientSideEvents Click="Verification" />--%>
                    </dx:ASPxButton>
                </td>
                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnSPCSample" runat="server" AutoPostBack="False" ClientInstanceName="btnSPCSample"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="SPC Sample" Theme="Office2010Silver" Width="100px">
                      <%--  <ClientSideEvents Click="SPCSample" />--%>
                    </dx:ASPxButton>
                </td>
                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnIOTProcess" runat="server" AutoPostBack="False" ClientInstanceName="btnIOTProcess"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="View IOT Process Table" Theme="Office2010Silver" Width="100px">
                      <%--  <ClientSideEvents Click="IOTProcess" />--%>
                    </dx:ASPxButton>
                </td>
                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnIOTTraceability" runat="server" AutoPostBack="False" ClientInstanceName="btnIOTTraceability"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="View IOT Traceability" Theme="Office2010Silver" Width="100px">
                      <%--  <ClientSideEvents Click="IOTTraceability" />--%>
                    </dx:ASPxButton>
                </td>
                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnExcel" runat="server" AutoPostBack="False" ClientInstanceName="btnExcel"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="Excel" Theme="Office2010Silver" Width="100px">
                      <%--  <ClientSideEvents Click="Excel" />--%>
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding: 5px 5px 5px 5px;">
        <table>
            <tr>
                <td style="width: 50%">
                    
                </td>
                <td style="width: 50%"></td>
            </tr>
        </table>
    </div>
    <div style="padding: 5px 5px 5px 5px; padding-top: 20px; padding-bottom: 20px">
        <table>
            <dx:ASPxGridView ID="GridMenu2" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridMenu2"
                EnableTheming="True" KeyFieldName="PartID;SubLineID;LineID" Theme="Office2010Black"
                Width="100%"
                Font-Names="Segoe UI" Font-Size="9pt">
                <ClientSideEvents EndCallback="OnEndCallback" />
                <Columns>
                    <dx:GridViewCommandColumn FixedStyle="Left"
                        VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true"
                        ShowNewButtonInHeader="true" ShowClearFilterButton="true" Width="80px">
                        <HeaderStyle Paddings-PaddingLeft="3px" HorizontalAlign="Center"
                            VerticalAlign="Middle">
                            <Paddings PaddingLeft="3px"></Paddings>
                        </HeaderStyle>
                    </dx:GridViewCommandColumn>

                    <dx:GridViewDataTextColumn Caption="Date" VisibleIndex="2" Width="100px"
                        FieldName="ProdDate">
                        <PropertiesTextEdit ClientInstanceName="ProdDate">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="PIC" VisibleIndex="3" Width="55px"
                        FieldName="PIC">
                        <PropertiesTextEdit ClientInstanceName="PIC">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Action" VisibleIndex="4" Width="55px"
                        FieldName="Action">
                        <PropertiesTextEdit ClientInstanceName="Action">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Result" VisibleIndex="5" Width="200px"
                        FieldName="Result">
                        <PropertiesTextEdit ClientInstanceName="Result">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Remark" VisibleIndex="6" Width="55px"
                        FieldName="Remark">
                        <PropertiesTextEdit ClientInstanceName="Remark">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="LastUser" VisibleIndex="7" Width="55px"
                        FieldName="LastUser">
                        <PropertiesTextEdit ClientInstanceName="LastUser">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="LastUpdate" VisibleIndex="8" Width="55px"
                        FieldName="LastUpdate">
                        <PropertiesTextEdit ClientInstanceName="LastUpdate">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior ColumnResizeMode="control" />
                <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true" PageSizeItemSettings-Visible="true">
                    <PageSizeItemSettings Visible="True"></PageSizeItemSettings>
                </SettingsPager>
                <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto" VerticalScrollableHeight="300" />
            </dx:ASPxGridView>
        </table>
    </div>
</asp:Content>
