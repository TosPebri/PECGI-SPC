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

        function OnInit(s, e) {
            if (s.cp_Verify == "0") {
                btnVerification.SetEnabled(true);
            }
            else {
                btnVerification.SetEnabled(false);
            }
        }

        function GridOnEndCallback(s, e) {
            console.log(s.cp_message);
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
            if (s.cp_Verify == "0") {
                btnVerification.SetEnabled(true);
            }
        }

        function Browse() {
            Grid.PerformCallback('Load');
            GridMenu.PerformCallback('Load');
        }

        function Clear() {
            var today = new Date();
            dtProdDate.SetDate(today);
            GridMenu.PerformCallback('Clear');
        }

        function Verify() {
            Grid.PerformCallback('Verify');
        }

        function ChangeFactory() {
            var FactoryCode = cboFactory.GetValue();
            var ItemType_Code = cboItemType.GetValue();
            var LineCode = cboLineID.GetValue();
            HideValue.Set('FactoryCode', FactoryCode);
            HideValue.Set('ItemType_Code', ItemType_Code);
            HideValue.Set('LineCode', LineCode);
            cboLineID.PerformCallback(FactoryCode);
            cboItemCheck.PerformCallback(FactoryCode + '|' + ItemType_Code + '|' + LineCode);
            GridMenu.PerformCallback('Kosong');
        }

        function ChangeItemType() {
            var FactoryCode = cboFactory.GetValue();
            var ItemType_Code = cboItemType.GetValue();
            var LineCode = cboLineID.GetValue();
            HideValue.Set('FactoryCode', FactoryCode);
            HideValue.Set('ItemType_Code', ItemType_Code);
            HideValue.Set('LineCode', LineCode);
            cboItemCheck.PerformCallback(FactoryCode + '|' + ItemType_Code + '|' + LineCode);
            GridMenu.PerformCallback('Kosong');
        }

        function ChangeLine() {
            var FactoryCode = cboFactory.GetValue();
            var LineCode = cboLineID.GetValue();
            var ItemType_Code = cboItemType.GetValue();
            HideValue.Set('FactoryCode', FactoryCode);
            HideValue.Set('ItemType_Code', ItemType_Code);
            HideValue.Set('LineCode', LineCode);
            cboItemCheck.PerformCallback(FactoryCode + '|' + ItemType_Code + '|' + LineCode);
            GridMenu.PerformCallback('Kosong');
        }

        function ChangeItemCheck() {
            var FactoryCode = cboFactory.GetValue();
            var LineCode = cboLineID.GetValue();
            var ItemType_Code = cboItemType.GetValue();
            var ItemCheck_Code = cboItemCheck.GetValue();
            HideValue.Set('FactoryCode', FactoryCode);
            HideValue.Set('ItemType_Code', ItemType_Code);
            HideValue.Set('LineCode', LineCode);
            HideValue.Set('ItemCheck_Code', ItemCheck_Code);
            cboShift.PerformCallback(FactoryCode + '|' + ItemType_Code + '|' + LineCode + '|' + ItemCheck_Code);
            GridMenu.PerformCallback('Kosong');
        }

        function ChangeShift() {
            var FactoryCode = cboFactory.GetValue();
            var LineCode = cboLineID.GetValue();
            var ItemType_Code = cboItemType.GetValue();
            var ItemCheck_Code = cboItemCheck.GetValue();
            var ShiftCode = cboShift.GetValue();
            HideValue.Set('FactoryCode', FactoryCode);
            HideValue.Set('ItemType_Code', ItemType_Code);
            HideValue.Set('LineCode', LineCode);
            HideValue.Set('ItemCheck_Code', ItemCheck_Code);
            HideValue.Set('ShiftCode', ShiftCode);
            cboSeq.PerformCallback(FactoryCode + '|' + ItemType_Code + '|' + LineCode + '|' + ItemCheck_Code + '|' + ShiftCode);
            GridMenu.PerformCallback('Kosong');
        }

        function ChangeSeq() {
            var Seq = cboSeq.GetValue();
            HideValue.Set('Seq', Seq);
            GridMenu.PerformCallback('Kosong');
        }

        function Back() {
            window.open('ProductionSampleVerificationList.aspx?prm=Verify', '_self');
        }

        function OnBatchEditStartEditing(s, e) {
            var rowIndex = e.visibleIndex;
            var levelno = grid.batchEditApi.GetCellValue(rowIndex, 'View');
            if (levelno == 'View') {
                window.open('ProdSampleVerification.aspx?', '_self');
            }
            e.cancel = true;
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
                        Theme="Office2010Black" EnableTheming="True" Height="25px" EnableIncrementalFiltering="True" Width="120px"
                        TextField="CODENAME" ValueField="CODE" ClientInstanceName="cboFactory">
                        <ClientSideEvents SelectedIndexChanged="ChangeFactory" />
                        <ItemStyle Height="10px" Paddings-Padding="4px" />
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
                        Theme="Office2010Black" EnableTheming="True" Height="25px" EnableIncrementalFiltering="True"
                        TextField="CODENAME" ValueField="CODE" ClientInstanceName="cboLineID">
                        <ClientSideEvents SelectedIndexChanged="ChangeLine" />
                        <ItemStyle Height="10px" Paddings-Padding="4px" />
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
                        Theme="Office2010Black" EnableTheming="True" Height="25px" EnableIncrementalFiltering="True" Width="120px"
                        TextField="CODENAME" ValueField="CODE" ClientInstanceName="cboItemType">
                        <ClientSideEvents SelectedIndexChanged="ChangeItemType" />
                        <ItemStyle Height="10px" Paddings-Padding="4px" />
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
                        Font-Size="9pt" Theme="Office2010Black" EnableTheming="True" Height="25px" EnableIncrementalFiltering="True"
                        TextField="CODENAME" ValueField="CODE" ClientInstanceName="cboItemCheck">
                        <ClientSideEvents SelectedIndexChanged="ChangeItemCheck" />
                        <ItemStyle Height="10px" Paddings-Padding="4px" />
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
                        ClientInstanceName="cboShift" Theme="Office2010Black" TextField="CODENAME" ValueField="CODE" EnableTheming="True" Width="60px"
                        EnableIncrementalFiltering="True">
                        <ClientSideEvents SelectedIndexChanged="ChangeShift" />
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
                        ClientInstanceName="cboSeq" Theme="Office2010Black" EnableTheming="True" Width="60px" EnableIncrementalFiltering="True"
                        TextField="CODENAME" ValueField="CODE">
                        <ClientSideEvents SelectedIndexChanged="ChangeSeq" />
                        <ItemStyle Height="10px" Paddings-Padding="4px" />
                        <ButtonStyle Paddings-Padding="4px" Width="5px">
                        </ButtonStyle>
                    </dx:ASPxComboBox>
                </td>

                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnBrowse" runat="server" AutoPostBack="False" ClientInstanceName="btnBrowse"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="Browse" Theme="Office2010Silver" Width="80px">
                        <ClientSideEvents Click="Browse" />
                    </dx:ASPxButton>
                </td>
                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnClear" runat="server" AutoPostBack="False" ClientInstanceName="btnClear"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="Clear" Theme="Office2010Silver" Width="80px">
                        <ClientSideEvents Click="Clear" />
                    </dx:ASPxButton>
                </td>
                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnBack" runat="server" AutoPostBack="False" ClientInstanceName="btnBack" Height="25px"
                        Font-Names="Segoe UI" Font-Size="9pt" Text="Back" Theme="Office2010Silver" Width="80px">
                        <ClientSideEvents Click="Back" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding: 5px 5px 5px 5px; padding-top: 20px; padding-bottom: 10px">
        <table>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnVerification" runat="server" AutoPostBack="False" ClientInstanceName="btnVerification"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="Verify" Theme="Office2010Silver" Width="100px">
                        <ClientSideEvents Click="Verify" Init="OnInit" />
                    </dx:ASPxButton>
                </td>
                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnSPCSample" runat="server" AutoPostBack="False" ClientInstanceName="btnSPCSample"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="SPC Sample" Theme="Office2010Silver" Width="100px">
                        <%--<ClientSideEvents Click="SPCSample" />--%>
                    </dx:ASPxButton>
                </td>
                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnIOTProcess" runat="server" AutoPostBack="False" ClientInstanceName="btnIOTProcess"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="View IOT Process Table" Theme="Office2010Silver" Width="100px">
                        <%--    <ClientSideEvents Click="IOTProcess" />--%>
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
            <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="Grid"
                EnableTheming="True" KeyFieldName="nDesc" Theme="Office2010Black"
                Width="100%" Font-Names="Segoe UI" Font-Size="9pt">
                <ClientSideEvents EndCallback="GridOnEndCallback" Init="OnInit" BatchEditStartEditing="OnBatchEditStartEditing" />
                <Columns>
                    <dx:GridViewBandColumn Caption="Date" VisibleIndex="0">
                        <Columns>
                            <dx:GridViewBandColumn Caption="Shift" VisibleIndex="0">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Time" FieldName="nDesc" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:GridViewBandColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                </Columns>
                <SettingsBehavior ColumnResizeMode="Control" />
                <SettingsPager AlwaysShowPager="true" Mode="ShowAllRecords" PageSize="30">
                </SettingsPager>
                <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="400"
                    VerticalScrollBarMode="Auto" ShowStatusBar="Hidden" />
                <Styles Header-Paddings-Padding="5px">
                    <Header HorizontalAlign="Center" Wrap="True">
                        <Paddings Padding="2px" />
                    </Header>
                </Styles>
            </dx:ASPxGridView>
        </table>
    </div>
    <div style="height: 26px; padding-top: 30px; padding-bottom: 5px; padding-left: 360px" align="Left">
        <dx:ASPxLabel ID="lblGridMenu" runat="server" Text="ACTIVITY MONITORING"
            Font-Names="Segoe UI" Font-Size="12pt" Font-Bold="True"
            Font-Underline="True">
        </dx:ASPxLabel>
    </div>
    <div style="padding: 5px 5px 5px 5px; padding-bottom: 20px">
        <table>
            <dx:ASPxGridView ID="GridMenu" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridMenu"
                EnableTheming="True" KeyFieldName="ActivityID" Theme="Office2010Black"
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


                    <dx:GridViewDataTextColumn Caption="Acitivity ID" VisibleIndex="1" Width="10px"
                        FieldName="ActivityID" Visible="false">
                        <PropertiesTextEdit ClientInstanceName="ActivityID" Width="60px">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Date" VisibleIndex="2" Width="100px"
                        FieldName="ProdDate">
                        <PropertiesTextEdit ClientInstanceName="ProdDate">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="PIC" VisibleIndex="3" Width="120px"
                        FieldName="PIC">
                        <PropertiesTextEdit ClientInstanceName="PIC">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Action" VisibleIndex="4" Width="200px"
                        FieldName="Action">
                        <PropertiesTextEdit ClientInstanceName="Action" Width="300px">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Result" FieldName="Result"
                        VisibleIndex="4" Width="50px" Settings-AutoFilterCondition="Contains">
                        <PropertiesComboBox DropDownStyle="DropDownList" Width="80px" TextFormatString="{0}"
                            IncrementalFilteringMode="StartsWith" DisplayFormatInEditMode="true">
                            <Items>
                                <dx:ListEditItem Text="OK" Value="0" />
                                <dx:ListEditItem Text="NG" Value="1" />
                            </Items>
                            <ItemStyle Height="10px" Paddings-Padding="4px">
                                <Paddings Padding="4px"></Paddings>
                            </ItemStyle>
                            <ButtonStyle Width="5px" Paddings-Padding="2px">
                                <Paddings Padding="2px"></Paddings>
                            </ButtonStyle>
                        </PropertiesComboBox>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <HeaderStyle Paddings-PaddingLeft="2px" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                            <Paddings PaddingLeft="2px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn Caption="Remark" VisibleIndex="6" Width="200px"
                        FieldName="Remark">
                        <PropertiesTextEdit ClientInstanceName="Remark" Width="300px">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Last User" VisibleIndex="7" Width="120px"
                        FieldName="LastUser">
                        <PropertiesTextEdit ClientInstanceName="LastUser">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Last Update" VisibleIndex="8" Width="100px"
                        FieldName="LastDate">
                        <PropertiesTextEdit ClientInstanceName="LastUpdate">
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataComboBoxColumn Caption="" FieldName="FactoryCode" Visible="false">
                        <PropertiesComboBox DropDownStyle="DropDownList" IncrementalFilteringMode="Contains" 
                            DisplayFormatInEditMode="true" Width="95px" TextField="CODE" 
                            ValueField="CODENAME" ClientInstanceName="FactoryCode">
                            <ClientSideEvents ValueChanged="GridFactoryChange" />
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
                        <CellStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </dx:GridViewDataComboBoxColumn>

                     <dx:GridViewDataComboBoxColumn Caption="" FieldName="ItemTypeCode" Visible="false">
                        <PropertiesComboBox DropDownStyle="DropDownList" IncrementalFilteringMode="Contains" 
                            DisplayFormatInEditMode="true" Width="95px" TextField="CODE" 
                            ValueField="CODENAME" ClientInstanceName="ItemTypeCode">
                            <ClientSideEvents ValueChanged="GridItemTypeChange" />
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
                        <CellStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </dx:GridViewDataComboBoxColumn>

                     <dx:GridViewDataComboBoxColumn Caption="" FieldName="LineCode" Visible="false">
                        <PropertiesComboBox DropDownStyle="DropDownList" IncrementalFilteringMode="Contains" 
                            DisplayFormatInEditMode="true" Width="95px" TextField="CODE" 
                            ValueField="CODENAME" ClientInstanceName="LineCode">
                            <ClientSideEvents ValueChanged="GridLineChange" />
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
                        <CellStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </dx:GridViewDataComboBoxColumn>

                     <dx:GridViewDataComboBoxColumn Caption="" FieldName="ItemCheckCode" Visible="false">
                        <PropertiesComboBox DropDownStyle="DropDownList" IncrementalFilteringMode="Contains" 
                            DisplayFormatInEditMode="true" Width="95px" TextField="CODE" 
                            ValueField="CODENAME" ClientInstanceName="ItemCheckCode">
                            <ClientSideEvents ValueChanged="GridItemCheckChange" />
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
                        <CellStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </dx:GridViewDataComboBoxColumn>

                     <dx:GridViewDataComboBoxColumn Caption="" FieldName="ShiftCode" Visible="false">
                        <PropertiesComboBox DropDownStyle="DropDownList" IncrementalFilteringMode="Contains" 
                            DisplayFormatInEditMode="true" Width="95px" TextField="CODE" 
                            ValueField="CODENAME" ClientInstanceName="ShiftCode">
                            <ClientSideEvents ValueChanged="GridShiftChange" />
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
                        <CellStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </dx:GridViewDataComboBoxColumn>

                </Columns>

                <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="Control" />
                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
                <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true">
                    <PageSizeItemSettings Visible="True">
                    </PageSizeItemSettings>
                </SettingsPager>
                <Settings ShowFilterRow="false" VerticalScrollBarMode="Auto"
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
                        <div style="padding: 15px 15px 15px 15px; width: 380px">
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <table align="center">
                                    <tr style="height: 30px">
                                        <td>
                                            <dx:ASPxLabel ID="lblAction" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text=" Action" Width="60px"></dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxGridViewTemplateReplacement ID="editAction" ReplacementType="EditFormCellEditor"
                                                runat="server" ColumnID="Action"></dx:ASPxGridViewTemplateReplacement>
                                        </td>
                                        <td style="visibility: hidden">
                                            <dx:ASPxGridViewTemplateReplacement ID="editActivityID" ReplacementType="EditFormCellEditor"
                                                runat="server" ColumnID="ActivityID"></dx:ASPxGridViewTemplateReplacement>
                                        </td>
                                    </tr>

                                    <tr style="height: 30px">
                                        <td>Remark</td>
                                        <td>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxGridViewTemplateReplacement ID="editRemark" ReplacementType="EditFormCellEditor"
                                                    runat="server" ColumnID="Remark"></dx:ASPxGridViewTemplateReplacement>
                                                <dx:LayoutItemNestedControlContainer>
                                        </td>
                                    </tr>

                                    <tr style="height: 30px">
                                        <td>Result</td>
                                        <td>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxGridViewTemplateReplacement ID="editResult" ReplacementType="EditFormCellEditor"
                                                    runat="server" ColumnID="Result"></dx:ASPxGridViewTemplateReplacement>
                                                <dx:LayoutItemNestedControlContainer>
                                        </td>
                                    </tr>
                                </table>
                            </dx:ContentControl>
                        </div>
                        <div style="text-align: left; padding: 5px 5px 5px 15px">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                        </div>
                    </EditForm>
                </Templates>
            </dx:ASPxGridView>
        </table>
    </div>
    <dx:ASPxHiddenField ID="HideValue" runat="server" ClientInstanceName="HideValue"></dx:ASPxHiddenField>
</asp:Content>
