<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Line.aspx.vb" Inherits="PECGI_SPC.Line" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<%-- <script type="text/javascript">
     function OnEndCallback(s, e) {
         if (s.cp_message) {
             lblMessage.SetText(s.cp_type + ' : ' + s.cp_message);
             var NAME = document.getElementById("msgpanel")
             if (s.cp_type === "Info") {
                 NAME.className = "alert alert-info fade in"
             }
             else if (s.cp_type === "Success") {
                 NAME.className = "alert alert-success fade in"
             }
             else if (s.cp_type === "Warning") {
                 NAME.className = "alert alert-warning fade in"
             }
             else if (s.cp_type === "ErrorMsg") {
                 NAME.className = "alert alert-danger fade in"
             }
         }
         else {
             lblMessage.SetText('')
             var NAME = document.getElementById("msgpanel")
             NAME.className = ""
         }
     }
	</script>--%>

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

var startTime;
function OnBeginCallbackEZR() {
	startTime = new Date();
}
function OnEndCallbackEZR() {
	var result = new Date() - startTime;
	result /= 1000;
	result = result.toString();
	if(result.length > 4)
		result = result.substr(0, 4);
//		time.SetText(result.toString() + " sec");
//		label.SetText("Time to retrieve the last data:");
}
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<div style="padding: 5px 5px 5px 5px">
         <div style="padding: 5px 5px 5px 5px">
        <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="Grid"
            EnableTheming="True" KeyFieldName="LineID" Theme="Office2010Black" 
             Width="100%" OnAfterPerformCallback="Grid_AfterPerformCallback" 
                        OnRowDeleting="Grid_RowDeleting" 
                        OnRowInserting="Grid_RowInserting" 
                        OnRowValidating="Grid_RowValidating" 
                        OnStartRowEditing="Grid_StartRowEditing" 
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

                <dx:GridViewDataTextColumn Caption="Line & Process" FieldName="LineID"
                    VisibleIndex="1" Width="100px" Settings-AutoFilterCondition="Contains" 
                    FixedStyle="Left" ShowInCustomizationForm="True">
                    <PropertiesTextEdit MaxLength="15" Width="75px">
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

                <dx:GridViewDataTextColumn Caption="Line Name" FieldName="LineName"
                    VisibleIndex="2" Width="150px" Settings-AutoFilterCondition="Contains" 
                    FixedStyle="Left" ShowInCustomizationForm="True">
                    <PropertiesTextEdit MaxLength="35" Width="150px">
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

                <%--<dx:GridViewDataComboBoxColumn Caption="Leader1" FieldName="Leader1" 
                    VisibleIndex="4" Width="100px" ShowInCustomizationForm="True">
                    <editformsettings visibleindex="4"/>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="100px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="UserID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="UserID"
                                    DataSourceID="SqlDataSource1"
                                    >
                                    
                                    <Columns>
                                        <dx:listboxcolumn Caption="User ID" FieldName="UserID" Width="68px"/>
                                        <dx:listboxcolumn Caption="Full Name" FieldName="FullName" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataComboBoxColumn Caption="Leader2" FieldName="Leader2" 
                    VisibleIndex="5" Width="100px" ShowInCustomizationForm="True">
                    <editformsettings visibleindex="6" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="100px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="UserID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="UserID"
                                    DataSourceID="SqlDataSource1">
                                    
                                    <Columns>
                                        <dx:listboxcolumn Caption="User ID" FieldName="UserID" Width="68px"/>
                                        <dx:listboxcolumn Caption="Full Name" FieldName="FullName" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataComboBoxColumn Caption="Leader3" FieldName="Leader3" 
                    VisibleIndex="6" Width="100px" ShowInCustomizationForm="True">
                    <editformsettings visibleindex="8" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="100px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="UserID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="UserID"
                                    DataSourceID="SqlDataSource1">
                                    
                                    <Columns>
                                        <dx:listboxcolumn Caption="User ID" FieldName="UserID" Width="68px"/>
                                        <dx:listboxcolumn Caption="Full Name" FieldName="FullName" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataComboBoxColumn Caption="Foreman1" FieldName="Foreman1" 
                    VisibleIndex="7" Width="100px" ShowInCustomizationForm="True">
                    <editformsettings visibleindex="1" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="100px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="UserID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="UserID"
                                    DataSourceID="SqlDataSource2">
                                    
                                    <Columns>
                                        <dx:listboxcolumn Caption="User ID" FieldName="UserID" Width="68px"/>
                                        <dx:listboxcolumn Caption="Full Name" FieldName="FullName" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataComboBoxColumn Caption="Foreman2" FieldName="Foreman2" 
                    VisibleIndex="8" Width="100px" ShowInCustomizationForm="True">
                    <editformsettings visibleindex="3" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="100px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="UserID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="UserID"
                                    DataSourceID="SqlDataSource2">
                                    
                                    <Columns>
                                        <dx:listboxcolumn Caption="User ID" FieldName="UserID" Width="68px"/>
                                        <dx:listboxcolumn Caption="Full Name" FieldName="FullName" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataComboBoxColumn Caption="Foreman3" FieldName="Foreman3" 
                    VisibleIndex="9" Width="100px" ShowInCustomizationForm="True">
                    <editformsettings visibleindex="5" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="100px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="UserID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="UserID"
                                    DataSourceID="SqlDataSource2">
                                    
                                    <Columns>
                                        <dx:listboxcolumn Caption="User ID" FieldName="UserID" Width="68px"/>
                                        <dx:listboxcolumn Caption="Full Name" FieldName="FullName" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>

                <dx:GridViewDataComboBoxColumn Caption="SectionHead1" FieldName="SectionHead1" 
                    VisibleIndex="10" Width="100px" ShowInCustomizationForm="True">
                    <editformsettings visibleindex="7" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="100px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="UserID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="UserID"
                                    DataSourceID="SqlDataSource3">
                                    
                                    <Columns>
                                        <dx:listboxcolumn Caption="User ID" FieldName="UserID" Width="68px"/>
                                        <dx:listboxcolumn Caption="Full Name" FieldName="FullName" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                
                <dx:GridViewDataComboBoxColumn Caption="SectionHead2" FieldName="SectionHead2" 
                    VisibleIndex="11" Width="100px" ShowInCustomizationForm="True">
                    <editformsettings visibleindex="9" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    DropDownStyle="DropDownList" 
                                    Width="100px" 
                                    TextFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="UserID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="UserID"
                                    DataSourceID="SqlDataSource3">
                                    
                                    <Columns>
                                        <dx:listboxcolumn Caption="User ID" FieldName="UserID" Width="68px"/>
                                        <dx:listboxcolumn Caption="Full Name" FieldName="FullName" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>--%>

                <%--<dx:GridViewDataComboBoxColumn Caption="EZ Runner Line ID" FieldName="EZRLineID" 
                    VisibleIndex="3" Width="150px">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True"></HeaderStyle>
                     <PropertiesComboBox 
                                    MaxLength="15"
                                    DropDownStyle="DropDownList" 
                                    Width="150px" 
                                    TextFormatString="{0}-{1}-{2}"
                                    DisplayFormatString="{0}"
                                    IncrementalFilteringMode="Contains" 
                                    TextField="EZRLineID" 
                                    DisplayFormatInEditMode="true"
                                    ValueField="EZRLineID"
                                    DataSourceID="SqlDataSource1" ClientInstanceName="EZRLineID"
                                    EnableCallbackMode="true" CallbackPageSize="10">
                                    <ClientSideEvents BeginCallback="function(s, e) { OnBeginCallbackEZR(); }" EndCallback="function(s, e) { OnEndCallbackEZR(); } "/>
                    
                                    <Columns>
                                        <dx:listboxcolumn Caption="Line Code" FieldName="EZRLineID" Width="68px"/>
                                        <dx:listboxcolumn Caption="Manufacture Code" FieldName="Manufacture_Code" Width="150px" />
                                        <dx:listboxcolumn Caption="Line Name" FieldName="Line_Name" Width="150px" />
                                    </Columns>
                                    <ItemStyle Height="10px" Paddings-Padding="4px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ItemStyle>
                                    <ButtonStyle Width="5px" Paddings-Padding="2px" >
                                        <Paddings Padding="4px"></Paddings>
                                    </ButtonStyle>
                                </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>--%>
                
                <%--<dx:GridViewDataTextColumn Caption="EZ Runner Line ID" FieldName="EZRLineID" 
                    VisibleIndex="3" Width="150px" Settings-AutoFilterCondition="Contains" 
                    FixedStyle="Left" ShowInCustomizationForm="True">
                    <PropertiesTextEdit MaxLength="35" Width="150px">
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>

                    <editformsettings visibleindex="3" />

                    <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="6px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="6px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>--%>

                <%--<dx:GridViewDataTextColumn Caption="Line Name" FieldName="LineName"
                    VisibleIndex="2" Width="150px" Settings-AutoFilterCondition="Contains" 
                    FixedStyle="Left" ShowInCustomizationForm="True">
                    <PropertiesTextEdit MaxLength="35" Width="150px">
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
                </dx:GridViewDataTextColumn>--%>
                
            </Columns>

            <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="Control" />
            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm"/>
            <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true" PageSizeItemSettings-Visible="true"></SettingsPager>
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
         <asp:SqlDataSource ID="SqlDataSource1" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="select distinct(Line_Code) AS EZRLineID, Manufacture_Code, Line_Name from EZRPROD.EZR_Production_Live.dbo.Manufacture_Line">
         </asp:SqlDataSource>
            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT UserID='', FullName='' UNION SELECT RTRIM(UserID) AS UserID, RTRIM(FullName) AS FullName FROM [UserSetup] WHERE LineLeaderStatus=1">
            </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT UserID='', FullName='' UNION SELECT RTRIM(UserID) AS UserID, RTRIM(FullName) AS FullName FROM [UserSetup] WHERE LineForemanStatus=1">
            </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource3" runat="server"
            ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
            SelectCommand="SELECT UserID='', FullName='' UNION SELECT RTRIM(UserID) AS UserID, RTRIM(FullName) AS FullName FROM [UserSetup] WHERE LineSectionHeadStatus=1">
            </asp:SqlDataSource>--%>

</div>
    
</asp:Content>
