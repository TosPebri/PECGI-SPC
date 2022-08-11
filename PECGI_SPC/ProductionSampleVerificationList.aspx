<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProductionSampleVerificationList.aspx.vb" Inherits="PECGI_SPC.ProductionSampleVerificationList" %>

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
    <div style="padding: 5px 5px 5px 5px; padding-bottom:20px; border-bottom:groove">
        <table class="auto-style3">
            <tr style="height:40px">
                <td> 
                    <dx:ASPxLabel ID="lblFactory" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="Factory">
                    </dx:ASPxLabel>
                </td>
                <td style="width:20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboFactory" runat="server" Font-Names="Segoe UI"
                        Font-Size="8pt" Theme="Office2010Black" EnableTheming="True" Height="25px"
                        TextField="CODE" ValueField="CODENAME" ClientInstanceName="cboFactory">
                        <Columns>
                            <dx:ListBoxColumn Caption="CODE" FieldName="CODE" Width="60px" />
                            <dx:ListBoxColumn Caption="CODE NAME" FieldName="CODENAME" Width="120px" />
                        </Columns>
                    </dx:ASPxComboBox>
                </td>

                <td style="width:10px">&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblLineID" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="Machine Process">
                    </dx:ASPxLabel>
                </td>
                <td style="width:20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboLineID" runat="server" Font-Names="Segoe UI"
                        Font-Size="8pt" Theme="Office2010Black" EnableTheming="True" Height="25px"
                        TextField="CODENAME" ValueField="CODE" ClientInstanceName="cboLineID">
                        <Columns>
                            <dx:ListBoxColumn Caption="CODE" FieldName="CODE" Width="60px" />
                            <dx:ListBoxColumn Caption="CODE NAME" FieldName="CODENAME" Width="120px" />
                        </Columns>
                    </dx:ASPxComboBox>
                </td>

                <td style="width:10px">&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblFromDate" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="Date">
                    </dx:ASPxLabel>
                </td>
                   <td style="width:20px">&nbsp;</td>
                <td colspan="3">
                     <dx:ASPxDateEdit ID="dtFromDate" runat="server" Theme="Office2010Black" AutoPostBack="false"
                        ClientInstanceName="dtFromDate" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="5">
                        <ClientSideEvents Init="OnInitDate" />
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

                 <td style="width:20px">&nbsp;</td>
                <td style="width:60px" align="center">
                    <dx:ASPxLabel ID="lblToDate" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="To">
                    </dx:ASPxLabel>
                </td>

                 <td style="width:20px">&nbsp;</td>
                <td colspan="3">
                    <dx:ASPxDateEdit ID="dtToDate" runat="server" Theme="Office2010Black" AutoPostBack="false"
                        ClientInstanceName="dtToDate" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="5">
                        <ClientSideEvents Init="OnInitDate" />
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
                    <dx:ASPxLabel ID="lblItemType" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="Type">
                    </dx:ASPxLabel>
                </td>
                <td style="width:20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboItemType" runat="server" Font-Names="Segoe UI"
                        Font-Size="8pt" Theme="Office2010Black" EnableTheming="True" Height="25px"
                        TextField="CODENAME" ValueField="CODE" ClientInstanceName="cboItemType">
                        <Columns>
                            <dx:ListBoxColumn Caption="CODE" FieldName="CODE" Width="60px" />
                            <dx:ListBoxColumn Caption="CODE NAME" FieldName="CODENAME" Width="120px" />
                        </Columns>
                    </dx:ASPxComboBox>
                </td>

                 <td style="width:10px">&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblItemCheck" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="Item Check">
                    </dx:ASPxLabel>
                </td>
                <td style="width:20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboItemCheck" runat="server" Font-Names="Segoe UI"
                        Font-Size="8pt" Theme="Office2010Black" EnableTheming="True" Height="25px"
                        TextField="CODENAME" ValueField="CODE" ClientInstanceName="cboItemCheck">
                        <Columns>
                            <dx:ListBoxColumn Caption="CODE" FieldName="CODE" Width="60px" />
                            <dx:ListBoxColumn Caption="CODE NAME" FieldName="CODENAME" Width="120px" />
                        </Columns>
                    </dx:ASPxComboBox>
                </td>

                 <td style="width:10px">&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblMK" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="MK Verification">
                    </dx:ASPxLabel>
                </td>
                   <td style="width:20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboMK" runat="server" Font-Names="Segoe UI" Height="25px"
                        Font-Size="8pt" Theme="Office2010Black" EnableTheming="True" Width="80px">
                         <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="Yes" Value="1" />
                            <dx:ListEditItem Text="No" Value="0" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>

                <td style="width:5px">&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblQC" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="QC Verification">
                    </dx:ASPxLabel>
                </td>

                <td style="width:20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboQC" runat="server" Font-Names="Segoe UI" Height="25px"
                        Font-Size="8pt" Theme="Office2010Black" EnableTheming="True"  Width="80px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="Yes" Value="1" />
                            <dx:ListEditItem Text="No" Value="0" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>

                <td style="width:10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnBrowse" runat="server" AutoPostBack="False" ClientInstanceName="btnBrowse"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="Browse" Theme="Office2010Silver" Width="80px">
                        <ClientSideEvents Click="Browse" />
                    </dx:ASPxButton>
                </td>
                 <td style="width:10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnClear" runat="server" AutoPostBack="False" ClientInstanceName="btnClear"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="Clear" Theme="Office2010Silver" Width="80px">
                        <ClientSideEvents Click="Clear" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding: 5px 5px 5px 5px; padding-top:20px; padding-bottom:20px">
        <table>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnVerification" runat="server" AutoPostBack="False" ClientInstanceName="btnVerification"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="Verification" Theme="Office2010Silver" Width="100px">
                        <ClientSideEvents Click="Verification" />
                    </dx:ASPxButton>
                </td>
                <td style="width:10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnViewSampleInput" runat="server" AutoPostBack="False" ClientInstanceName="btnViewSampleInput"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="View Sample Input" Theme="Office2010Silver" Width="100px">
                        <ClientSideEvents Click="ViewSampleInput" />
                    </dx:ASPxButton>
                </td>
                <td style="width:10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnExcel" runat="server" AutoPostBack="False" ClientInstanceName="btnExcel"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="Excel" Theme="Office2010Silver" Width="100px">
                        <ClientSideEvents Click="Excel" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding: 5px 5px 5px 5px">
        <dx:ASPxGridView ID="GridMenu" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridMenu"
            EnableTheming="True" KeyFieldName="PartID;SubLineID;LineID" Theme="Office2010Black"
            Width="100%"
            Font-Names="Segoe UI" Font-Size="9pt">
            <ClientSideEvents EndCallback="OnEndCallback" Init="function grid_Init(s, e) {scheduleGridUpdate(s);}"
                BeginCallback="function grid_BeginCallback(s, e) {window.clearTimeout(timeout);}" />
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True"
                    ShowClearFilterButton="true" VisibleIndex="1" SelectAllCheckboxMode="Page"
                    Width="30px" FixedStyle="Left">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </dx:GridViewCommandColumn>

                <dx:GridViewDataTextColumn Caption="Date" VisibleIndex="2" Width="55px"
                    FieldName="ProdDate">
                    <PropertiesTextEdit ClientInstanceName="ProdDate">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Shift" VisibleIndex="3" Width="55px"
                    FieldName="Shift">
                    <PropertiesTextEdit ClientInstanceName="Shift">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Seq" VisibleIndex="4" Width="55px"
                    FieldName="Seq">
                    <PropertiesTextEdit ClientInstanceName="Seq">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Item Check" VisibleIndex="5" Width="55px"
                    FieldName="ItemCheck">
                    <PropertiesTextEdit ClientInstanceName="ItemCheck">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Min" VisibleIndex="6" Width="55px"
                    FieldName="Min">
                    <PropertiesTextEdit ClientInstanceName="Min">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Avg" VisibleIndex="7" Width="55px"
                    FieldName="Avg">
                    <PropertiesTextEdit ClientInstanceName="Avg">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="R" VisibleIndex="8" Width="55px"
                    FieldName="R">
                    <PropertiesTextEdit ClientInstanceName="R">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Correction Status" VisibleIndex="9" Width="55px"
                    FieldName="Correction_Sts">
                    <PropertiesTextEdit ClientInstanceName="Correction_Sts">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Result" VisibleIndex="10" Width="55px"
                    FieldName="Result">
                    <PropertiesTextEdit ClientInstanceName="Result">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="SampleTime" VisibleIndex="11" Width="55px"
                    FieldName="SampleTime">
                    <PropertiesTextEdit ClientInstanceName="SampleTime">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Operator" VisibleIndex="12" Width="55px"
                    FieldName="Operator">
                    <PropertiesTextEdit ClientInstanceName="Operator">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewBandColumn Caption="Verification by MK" VisibleIndex="13" HeaderStyle-HorizontalAlign="Center">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="PIC" VisibleIndex="0" Width="50px"
                            FieldName="MK_PIC">
                            <PropertiesTextEdit ClientInstanceName="MK_PIC"></PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn Caption="Time" VisibleIndex="0" Width="50px"
                            FieldName="MK_Time">
                            <PropertiesTextEdit ClientInstanceName="MK_Time"></PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>

                <dx:GridViewBandColumn Caption="Verification by QC" VisibleIndex="14" HeaderStyle-HorizontalAlign="Center">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="PIC" VisibleIndex="0" Width="50px"
                            FieldName="QC_PIC">
                            <PropertiesTextEdit ClientInstanceName="QC_PIC"></PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn Caption="Time" VisibleIndex="0" Width="50px"
                            FieldName="QC_Time">
                            <PropertiesTextEdit ClientInstanceName="QC_Time"></PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
            </Columns>
        </dx:ASPxGridView>
    </div>
</asp:Content>
