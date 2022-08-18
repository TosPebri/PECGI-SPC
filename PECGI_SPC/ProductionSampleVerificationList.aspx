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

        function OnInitToDate(s, e) {
            var today = new Date();
            dtToDate.SetDate(today);
        }

        function OnInitFromDate(s, e) {
            var today = new Date();
            dtFromDate.SetDate(today);
        }

        function Browse() {
            GridMenu.PerformCallback('Load');
        }

        function Clear() {
            var today = new Date();
            dtFromDate.SetDate(today);
            dtToDate.SetDate(today);

            cboFactory.SetSelectedIndex(0);
            cboItemType.SetSelectedIndex(0);
            cboMK.SetSelectedIndex(0);
            cboQC.SetSelectedIndex(0);
            ChangeFactory()

            GridMenu.PerformCallback('Clear');
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
            var ItemCheck_Code = cboItemCheck.GetValue();
            HideValue.Set('ItemCheck_Code', ItemCheck_Code);
            GridMenu.PerformCallback('Kosong');
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
                          <ClientSideEvents SelectedIndexChanged="ChangeFactory"/>
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
                        <ClientSideEvents SelectedIndexChanged="ChangeLine"/>
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
                <td colspan="3">
                    <dx:ASPxDateEdit ID="dtFromDate" runat="server" Theme="Office2010Black" AutoPostBack="false"
                        ClientInstanceName="dtFromDate" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="5">
                        <ClientSideEvents Init="OnInitFromDate" />
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

                <td style="width: 20px">&nbsp;</td>
                <td style="width: 60px" align="center">
                    <dx:ASPxLabel ID="lblToDate" runat="server" Font-Names="Segoe UI" Font-Size="9pt" Text="To">
                    </dx:ASPxLabel>
                </td>

                <td style="width: 20px">&nbsp;</td>
                <td colspan="3">
                    <dx:ASPxDateEdit ID="dtToDate" runat="server" Theme="Office2010Black" AutoPostBack="false"
                        ClientInstanceName="dtToDate" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="5">
                        <ClientSideEvents Init="OnInitToDate" />
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
                         <ClientSideEvents SelectedIndexChanged="ChangeItemType"/>
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
                        Font-Size="9pt" Theme="Office2010Black" EnableTheming="True" Height="25px" EnableIncrementalFiltering="True" SelectedIndex="0" TextFormatString="{1}"
                        TextField="CODENAME" ValueField="CODE" ClientInstanceName="cboItemCheck">
                         <ClientSideEvents SelectedIndexChanged="ChangeItemCheck"/>
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
                    <dx:ASPxLabel ID="lblMK" runat="server" Font-Names="Segoe UI" Font-Size="9pt" Text="MK Verification">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboMK" runat="server" Font-Names="Segoe UI" Height="25px" DropDownStyle="DropDownList" IncrementalFilteringMode="Contains" ClientInstanceName="cboMK"
                        Font-Size="9pt" Theme="Office2010Black" EnableTheming="True" Width="80px" EnableIncrementalFiltering="True" TextFormatString="{0}" SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="YES" Value="1" />
                            <dx:ListEditItem Text="NO" Value="0" />
                        </Items>
                        <ItemStyle Height="10px" Paddings-Padding="4px" Font-Size="11px" />
                        <ButtonStyle Paddings-Padding="4px" Width="5px">
                        </ButtonStyle>
                    </dx:ASPxComboBox>
                </td>

                <td style="width: 5px">&nbsp;</td>
                <td>
                    <dx:ASPxLabel ID="lblQC" runat="server" Font-Names="Segoe UI" Font-Size="9pt" Text="QC Verification">
                    </dx:ASPxLabel>
                </td>

                <td style="width: 20px">&nbsp;</td>
                <td>
                    <dx:ASPxComboBox ID="cboQC" runat="server" Font-Names="Segoe UI" Height="25px" DropDownStyle="DropDownList" IncrementalFilteringMode="Contains" ClientInstanceName="cboQC"
                        Font-Size="9pt" Theme="Office2010Black" EnableTheming="True" Width="80px" EnableIncrementalFiltering="True" TextFormatString="{0}" SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="YES" Value="1" />
                            <dx:ListEditItem Text="NO" Value="0" />
                        </Items>
                        <ItemStyle Height="10px" Paddings-Padding="4px" Font-Size="11px" />
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
            </tr>
        </table>
    </div>
    <div style="padding: 5px 5px 5px 5px; padding-top: 20px; padding-bottom: 20px">
        <table>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnVerification" runat="server" AutoPostBack="False" ClientInstanceName="btnVerification"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="Verification" Theme="Office2010Silver" Width="100px">
                        <ClientSideEvents Click="Verification" />
                    </dx:ASPxButton>
                </td>
                <td style="width: 10px">&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnViewSampleInput" runat="server" AutoPostBack="False" ClientInstanceName="btnViewSampleInput"
                        Font-Names="Segoe UI" Font-Size="10pt" Text="View Sample Input" Theme="Office2010Silver" Width="100px">
                        <ClientSideEvents Click="ViewSampleInput" />
                    </dx:ASPxButton>
                </td>
                <td style="width: 10px">&nbsp;</td>
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
            <ClientSideEvents EndCallback="OnEndCallback" />
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="1" Width="30px" FixedStyle="Left" Caption=" ">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </dx:GridViewCommandColumn>

                <dx:GridViewDataTextColumn Caption="Date" VisibleIndex="2" Width="100px"
                    FieldName="ProdDate">
                    <PropertiesTextEdit ClientInstanceName="ProdDate">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Shift" VisibleIndex="3" Width="55px"
                    FieldName="ShiftCode">
                    <PropertiesTextEdit ClientInstanceName="ShiftCode">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Seq" VisibleIndex="4" Width="55px"
                    FieldName="SequenceNo">
                    <PropertiesTextEdit ClientInstanceName="SequenceNo">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Item Check" VisibleIndex="5" Width="200px"
                    FieldName="ItemCheck">
                    <PropertiesTextEdit ClientInstanceName="ItemCheck">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Min" VisibleIndex="6" Width="55px"
                    FieldName="nMin">
                    <PropertiesTextEdit ClientInstanceName="nMin">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                 <dx:GridViewDataTextColumn Caption="Max" VisibleIndex="7" Width="55px"
                    FieldName="nMax">
                    <PropertiesTextEdit ClientInstanceName="nMax">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Avg" VisibleIndex="8" Width="55px"
                    FieldName="nAvg">
                    <PropertiesTextEdit ClientInstanceName="nAvg">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="R" VisibleIndex="9" Width="55px"
                    FieldName="nR">
                    <PropertiesTextEdit ClientInstanceName="nR">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Correction Status" VisibleIndex="10" Width="70px" 
                    FieldName="Cor_Sts">
                    <PropertiesTextEdit ClientInstanceName="Cor_Sts">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"/>
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" > 
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Result" VisibleIndex="11" Width="55px"
                    FieldName="Result">
                    <PropertiesTextEdit ClientInstanceName="Result">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="SampleTime" VisibleIndex="12" Width="150px"
                    FieldName="SampleTime">
                    <PropertiesTextEdit ClientInstanceName="SampleTime">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="Operator" VisibleIndex="13" Width="100px"
                    FieldName="Operator">
                    <PropertiesTextEdit ClientInstanceName="Operator">
                    </PropertiesTextEdit>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewBandColumn Caption="Verification by MK" VisibleIndex="14" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Paddings-PaddingBottom="3px" HeaderStyle-Paddings-PaddingTop="3px">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="PIC" VisibleIndex="1" Width="80px"
                            FieldName="MK_PIC" HeaderStyle-Paddings-PaddingBottom="2px" HeaderStyle-Paddings-PaddingTop="2px">
                            <PropertiesTextEdit ClientInstanceName="MK_PIC"></PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn Caption="Time" VisibleIndex="2" Width="150px"
                            FieldName="MK_Time" HeaderStyle-Paddings-PaddingBottom="2px" HeaderStyle-Paddings-PaddingTop="2px">
                            <PropertiesTextEdit ClientInstanceName="MK_Time"></PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>

                <dx:GridViewBandColumn Caption="Verification by QC" VisibleIndex="15" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Paddings-PaddingBottom="3px" HeaderStyle-Paddings-PaddingTop="3px">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="PIC" VisibleIndex="1" Width="80px"
                            FieldName="QC_PIC" HeaderStyle-Paddings-PaddingBottom="2px" HeaderStyle-Paddings-PaddingTop="2px">
                            <PropertiesTextEdit ClientInstanceName="QC_PIC"></PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn Caption="Time" VisibleIndex="2" Width="150px"
                            FieldName="QC_Time" HeaderStyle-Paddings-PaddingBottom="2px" HeaderStyle-Paddings-PaddingTop="2px">
                            <PropertiesTextEdit ClientInstanceName="QC_Time"></PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
            </Columns>
            <SettingsBehavior ColumnResizeMode="control" />
            <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true" PageSizeItemSettings-Visible="true">
                <PageSizeItemSettings Visible="True"></PageSizeItemSettings>
            </SettingsPager>
            <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto" VerticalScrollableHeight="300" />
            
        </dx:ASPxGridView>
    </div>

    <dx:ASPxHiddenField ID="HideValue" runat="server" ClientInstanceName="HideValue"></dx:ASPxHiddenField>
</asp:Content>
