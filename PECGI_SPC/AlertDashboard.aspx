<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AlertDashboard.aspx.vb" Inherits="PECGI_SPC.AlertDashboard" MasterPageFile="~/Site.Master" %>

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
            Grid.PerformCallback('Load');
            GridNG.PerformCallback('Load');
        }

        <%--window.onload=function(){

            var interval = document.getElementById("<%=hdInterval.ClientID %>").value;

            //var interval = document.getElementById("hdInterval").value;
            setInterval(() => {
                Grid.PerformCallback('Load');
                GridNG.PerformCallback('Load');
            }, interval);

        }--%>
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div style="padding: 0px 5px 5px 5px">
        <%--<table>
            <tr style="height: 30px">
                <td style="width: 120px">
                    &nbsp;<dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="Factory">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="cboFactory" runat="server" Theme="Office2010Black" Width="200px" Height="25px" ClientInstanceName="cboFactory">
                        <ItemStyle Height="10px" Paddings-Padding="4px" />
                        <ButtonStyle Width="5px" Paddings-Padding="4px" />
                    </dx:ASPxComboBox>                            
                </td>
                <td >
                    <dx:ASPxButton ID="btnSearch" runat="server" AutoPostBack="False" ClientInstanceName="btnBrowse" Theme="Office2010Silver" Height="28px"
                        Text="Browse" style="margin-left: 30px">
                        <ClientSideEvents Click="up_Browse" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>--%>
        <table style="width:100%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="width:60px; padding:1px 0px 0px 0px">
                                &nbsp;<dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Names="Segoe UI" Font-Size="8pt" Text="Factory">
                                </dx:ASPxLabel>
                            </td>
                            <td style=" width:130px; padding:1px 0px 0px 0px">
                                <%--<dx:ASPxComboBox ID="cboFactory" runat="server" Theme="Office2010Black" Width="100px" Height="25px" ClientInstanceName="cboFactory">
                                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" />
                                </dx:ASPxComboBox>--%>
                                <dx:ASPxComboBox ID="cboFactory" runat="server" Theme="Office2010Black" TextField="FactoryName" ClientInstanceName="cboFactory" ValueField="FactoryCode" Font-Names="Segoe UI" 
                                    Font-Size="9pt" Height="25px" IncrementalFilteringMode="Contains" Width="100px" TabIndex="6">
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </dx:ASPxComboBox>
                            </td>
                            <td style=" width:100px;">
                                <dx:ASPxButton ID="btnSearch" runat="server" AutoPostBack="False" ClientInstanceName="btnBrowse" Theme="Office2010Silver" Height="28px"
                                    Text="Browse" style="margin-left: 30px">
                                    <ClientSideEvents Click="up_Browse" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>

                <td>
                </td>

                <td>
                    <table style="width: 100%; height: 50px">
                        <tr>
                            <td style="width: 10%;" align="left">
                                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Date" CssClass="text" />
                            </td>
                            <td style="width: 10%;" align="left">
                                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text=":" CssClass="text" />
                                &nbsp
                                <dx:ASPxLabel ID="lblDateNow" ClientInstanceName="lblOK" runat="server" Text="" CssClass="text" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;" align="left">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Time" CssClass="text" />
                            </td>
                            <td style="width: 10%;" align="left">
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text=":" CssClass="text" />
                                &nbsp
                                <%--<dx:ASPxLabel ID="lblTimeNow" runat="server" Text="" CssClass="text" />--%>
                                <label id="lblTimeNow"></label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
        <asp:SqlDataSource ID="dsFactory" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            SelectCommand="Exec sp_SPCAlertDashboard_FillCombo '1' "></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsType" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            SelectCommand="Exec sp_SPCAlertDashboard_FillCombo '2' "></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsMachine" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            SelectCommand="Exec sp_SPCAlertDashboard_FillCombo '3' "></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsItemCheck" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            SelectCommand="Exec sp_SPCAlertDashboard_FillCombo '5' "></asp:SqlDataSource>
    
        <asp:SqlDataSource ID="dsShiftCode" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            SelectCommand="Exec sp_SPCAlertDashboard_FillCombo '6' "></asp:SqlDataSource>

        <asp:SqlDataSource ID="dsSequence" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
            SelectCommand="Exec sp_SPCAlertDashboard_FillCombo '7' "></asp:SqlDataSource>


    <div style="padding: 20px 5px 5px 5px">

        <div class="bg-color-grayDark" style="width: 100%;height: 25px">
            <center>
                <label style="color: white; margin-top: 5px" >Production Sample - Delay Input</label>
            </center>
        </div>

        <div id="ScrollList" style="height: 150px; overflow: auto">
            <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="Grid"
                EnableTheming="True" KeyFieldName="ItemTypeName;LineCode;ItemCheck" Theme="Office2010Black" Width="100%"
                Font-Names="Segoe UI" Font-Size="9pt"
                OnRowValidating="Grid_RowValidating" OnStartRowEditing="Grid_StartRowEditing"
                OnRowInserting="Grid_RowInserting" OnRowDeleting="Grid_RowDeleting"
                OnAfterPerformCallback="Grid_AfterPerformCallback" SettingsContextMenu-EnableScrolling="false" >
                <ClientSideEvents EndCallback="OnEndCallback" />
                <Columns>
                    <%--<dx:GridViewCommandColumn Caption="Action" FixedStyle="Left"
                        VisibleIndex="0" ShowEditButton="true" ShowClearFilterButton="true" Width="80px">
                        <HeaderStyle Paddings-PaddingLeft="3px" HorizontalAlign="Center" 
                            VerticalAlign="Middle" >
                            <Paddings PaddingLeft="3px"></Paddings>
                        </HeaderStyle>
                    </dx:GridViewCommandColumn>--%>
                
                    <dx:GridViewDataTextColumn Caption="Action" VisibleIndex="0" FixedStyle="Left" Width="80px" Settings-AutoFilterCondition="Contains">
                        <HeaderStyle Paddings-PaddingLeft="3px" HorizontalAlign="Center" 
                            VerticalAlign="Middle" >
                            <Paddings PaddingLeft="3px"></Paddings>
                        </HeaderStyle>
                        <DataItemTemplate>
                            <center>
                                <a href="https://www.google.com" >
                                    <label class="fa fa-edit" id="lblEdit2"></label>
                                    <%--<dx:ASPxLabel ID="lblEdit" runat="server" CssClass="fa fa-edit"></dx:ASPxLabel>--%>
                                </a>
                            </center>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Factory Code" FieldName="FactoryCode" VisibleIndex="0"
                        Width="200px" Settings-AutoFilterCondition="Contains" Visible="false">
                        <PropertiesComboBox DataSourceID="dsFactory" DropDownStyle="DropDownList" TextFormatString="{0}"
                            IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="195px"
                            TextField="FactoryCode" ValueField="FactoryCode" ClientInstanceName="FactoryCode">
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

                    <dx:GridViewDataComboBoxColumn Caption="Type" FieldName="ItemTypeCode" VisibleIndex="1"
                        Width="70px" Settings-AutoFilterCondition="Contains">
                        <PropertiesComboBox DataSourceID="dsType" DropDownStyle="DropDownList" TextFormatString="{0}"
                            IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="60px"
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
                
                    <dx:GridViewDataComboBoxColumn Caption="Item Check" FieldName="ItemCheckCode" VisibleIndex="3"
                        Width="250px" Settings-AutoFilterCondition="Contains">
                        <PropertiesComboBox DataSourceID="dsItemCheck" DropDownStyle="DropDownList" TextFormatString="{0}"
                            IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="245px"
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

                    <dx:GridViewDataTextColumn Caption="Date" FieldName="Date"
                        VisibleIndex="4" Width="100px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                        </FilterCellStyle>
                        <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Shift" FieldName="ShiftCode" VisibleIndex="5"
                        Width="60px" Settings-AutoFilterCondition="Contains">
                        <PropertiesComboBox DataSourceID="dsShiftCode" DropDownStyle="DropDownList" TextFormatString="{0}"
                            IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="60px"
                            TextField="ShiftCode" ValueField="ShiftCode" ClientInstanceName="ShiftCode">
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
                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Seq" FieldName="SequenceNo" VisibleIndex="6"
                        Width="40px" Settings-AutoFilterCondition="Contains">
                        <PropertiesComboBox DataSourceID="dsSequence" DropDownStyle="DropDownList" TextFormatString="{0}"
                            IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="40px"
                            TextField="SequenceNo" ValueField="SequenceNo" ClientInstanceName="SequenceNo">
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
                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn Caption="Schedule Start" FieldName="StartTime"
                        VisibleIndex="7" Width="100px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                        </FilterCellStyle>
                        <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Schedule End" FieldName="EndTime"
                        VisibleIndex="8" Width="100px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                        </FilterCellStyle>
                        <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Delay (Minute)" FieldName="Delay"
                        VisibleIndex="8" Width="60px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                        </FilterCellStyle>
                        <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <%--<dx:GridViewDataTextColumn Caption="Alert" FieldName=""
                        VisibleIndex="9" Width="100px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
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
                
                    <dx:GridViewDataTextColumn Caption="Alert" VisibleIndex="9" Width="80px" Settings-AutoFilterCondition="Contains">
                        <HeaderStyle Paddings-PaddingLeft="3px" HorizontalAlign="Center" 
                            VerticalAlign="Middle" >
                            <Paddings PaddingLeft="3px"></Paddings>
                        </HeaderStyle>
                        <DataItemTemplate>
                            <center>
                                <a href="https://www.google.com" >
                                    <label> Send Email</label>
                                </a>
                            </center>
                        </DataItemTemplate>
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


            </dx:ASPxGridView>
            
            <br />
            <button disabled="disabled" style="background-color:yellow;width: 30px;height:20px"></button> <label> Delay < 60 Minutes</label>
            <button disabled="disabled" style="background-color:red;width: 30px;height:20px"></button> <label> Delay > 60 Minutes</label>
        </div>
    </div>
    <br />
    <div style="padding: 20px 5px 5px 5px">

        
        <div class="bg-color-grayDark" style="width: 100%;height: 25px">
            <center>
                <label style="color: white; margin-top: 5px" >Production Sample - NG Result</label>
            </center>
        </div>
        
        <div id="ScrollList" style="height: 150px; overflow: auto">
            <dx:ASPxGridView ID="GridNG" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridNG"
                EnableTheming="True" KeyFieldName="ItemTypeName;LineCode;ItemCheck" Theme="Office2010Black" Width="100%"
                Font-Names="Segoe UI" Font-Size="9pt"
                OnRowValidating="GridNG_RowValidating" OnStartRowEditing="GridNG_StartRowEditing"
                OnRowInserting="GridNG_RowInserting" OnRowDeleting="GridNG_RowDeleting"
                OnAfterPerformCallback="GridNG_AfterPerformCallback">
                <ClientSideEvents EndCallback="OnEndCallback" />
                <Columns>               
                
                    <dx:GridViewDataTextColumn Caption="Action" VisibleIndex="0" Width="80px" Settings-AutoFilterCondition="Contains">
                        <HeaderStyle Paddings-PaddingLeft="3px" HorizontalAlign="Center" 
                            VerticalAlign="Middle" >
                            <Paddings PaddingLeft="3px"></Paddings>
                        </HeaderStyle>
                        <DataItemTemplate>
                            <center>
                                <a href="https://www.google.com" >
                                    <label class="fa fa-edit"></label>
                                </a>
                            </center>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Factory Code" FieldName="FactoryCode" VisibleIndex="0"
                        Width="200px" Settings-AutoFilterCondition="Contains" Visible="false">
                        <PropertiesComboBox DataSourceID="dsFactory" DropDownStyle="DropDownList" TextFormatString="{0}"
                            IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="195px"
                            TextField="FactoryCode" ValueField="FactoryCode" ClientInstanceName="FactoryCode">
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
                        Width="70px" Settings-AutoFilterCondition="Contains">
                        <PropertiesComboBox DataSourceID="dsType" DropDownStyle="DropDownList" TextFormatString="{0}"
                            IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="60px"
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
                        Width="250px" Settings-AutoFilterCondition="Contains">
                        <PropertiesComboBox DataSourceID="dsItemCheck" DropDownStyle="DropDownList" TextFormatString="{0}"
                            IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="245px"
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

                    <dx:GridViewDataTextColumn Caption="Date" FieldName="Date"
                        VisibleIndex="4" Width="100px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                        </FilterCellStyle>
                        <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Shift" FieldName="ShiftCode" VisibleIndex="5"
                        Width="60px" Settings-AutoFilterCondition="Contains">
                        <PropertiesComboBox DataSourceID="dsShiftCode" DropDownStyle="DropDownList" TextFormatString="{0}"
                            IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="60px"
                            TextField="ShiftCode" ValueField="ShiftCode" ClientInstanceName="ShiftCode">
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
                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn Caption="Seq" FieldName="SequenceNo" VisibleIndex="6"
                        Width="40px" Settings-AutoFilterCondition="Contains">
                        <PropertiesComboBox DataSourceID="dsSequence" DropDownStyle="DropDownList" TextFormatString="{0}"
                            IncrementalFilteringMode="Contains" DisplayFormatInEditMode="true" Width="40px"
                            TextField="SequenceNo" ValueField="SequenceNo" ClientInstanceName="SequenceNo">
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
                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn Caption="USL" FieldName="USL"
                        VisibleIndex="7" Width="100px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                        </FilterCellStyle>
                        <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="LSL" FieldName="LSL"
                        VisibleIndex="8" Width="100px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                        </FilterCellStyle>
                        <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="UCL" FieldName="UCL"
                        VisibleIndex="8" Width="60px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                        </FilterCellStyle>
                        <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="LCL" FieldName="LCL"
                        VisibleIndex="9" Width="60px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                        </FilterCellStyle>
                        <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Min" FieldName="MinValue"
                        VisibleIndex="10" Width="60px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                        </FilterCellStyle>
                        <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Max" FieldName="MaxValue"
                        VisibleIndex="11" Width="60px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                        </FilterCellStyle>
                        <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Ave" FieldName="Average"
                        VisibleIndex="12" Width="60px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                        <Settings AutoFilterCondition="Contains"></Settings>
                        <FilterCellStyle Paddings-PaddingRight="4px">
                        <Paddings PaddingRight="4px"></Paddings>
                        </FilterCellStyle>
                        <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                        <Paddings PaddingLeft="5px"></Paddings>
                        </HeaderStyle>
                        <CellStyle HorizontalAlign="Right" VerticalAlign="Middle"></CellStyle>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption="Operator" FieldName="Operator"
                        VisibleIndex="13" Width="100px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
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

                    <dx:GridViewDataTextColumn Caption="MK" FieldName="MK"
                        VisibleIndex="14" Width="100px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
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

                    <dx:GridViewDataTextColumn Caption="QC" FieldName="QC"
                        VisibleIndex="15" Width="100px" Settings-AutoFilterCondition="Contains">
                        <PropertiesTextEdit MaxLength="25" Width="100px">
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

                    <dx:GridViewDataTextColumn Caption="Alert" VisibleIndex="16" Width="80px" Settings-AutoFilterCondition="Contains">
                        <HeaderStyle Paddings-PaddingLeft="3px" HorizontalAlign="Center" 
                            VerticalAlign="Middle" >
                            <Paddings PaddingLeft="3px"></Paddings>
                        </HeaderStyle>
                        <DataItemTemplate>
                            <center>
                                <a href="https://www.google.com" >
                                    <label> Send Email</label>
                                </a>
                            </center>
                        </DataItemTemplate>
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

            </dx:ASPxGridView>
            <br />
            <button disabled="disabled" style="background-color:yellow;width: 30px;height:20px"></button> <label> Out Of Control Value</label>
            <button disabled="disabled" style="background-color:red;width: 30px;height:20px"></button> <label> Out Of Specification Value</label>
        </div>
        
    </div>
    <div style="height:10px">
        <input type="hidden" runat="server" id="hdInterval" value="<%=hdInterval %>" />  
    </div>
    
    <script>
        window.onload = function () {

            var interval = document.getElementById("<%=hdInterval.ClientID %>").value;

            setTimer0 = setInterval(function () {

                Grid.PerformCallback('Load');
                GridNG.PerformCallback('Load');
            }, interval, (0));

            setTimer1 = setInterval(function () {
                var today = new Date();
                document.getElementById('lblTimeNow').innerHTML = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
            }, 1000, (1));
        }
    </script>
</asp:Content>
