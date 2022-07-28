<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="QCSXRData.aspx.vb" Inherits="PECGI_SPC.QCSXRData" %>
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

function OnBatchEditStartEditing(s, e) {
    currentColumnName = e.focusedColumn.fieldName;
    if (currentColumnName == "XID") 
        {
            e.cancel = true;
        }
    currentEditableVisibleIndex = e.visibleIndex;
}

 function SelectLineID(s, e) {
//        cbopartid.SetText('');
//        txtpartname.SetText('');
//        cborevno.SetValue('');
//        dtrevdate.SetText('');
//        txtrevisionhistory.SetText('');
//        txtpreparedby.SetText('');

//        GridMenu.PerformCallback('GridClear|');
        cbopartid.PerformCallback('load|' + cbolineid.GetValue());
}

function SelectPartID(s, e) {
        txtpartname.SetText(cbopartid.GetSelectedItem().GetColumnText(1));
//        cborevno.SetValue('');
//        dtrevdate.SetText('');
//        txtrevisionhistory.SetText('');
//        txtpreparedby.SetText('');
//        lblActive.SetText('Inactive');
//        lblActive.GetMainElement().style.color = 'Red';

        cborevno.PerformCallback('|' + cbopartid.GetValue());
        cborevnopopup.PerformCallback('|' + cbopartid.GetValue());
//        GridMenu.PerformCallback('ClearGridPart|');
}

function SelectRevNo(s, e) {
        txtpartname.SetText(cbopartid.GetSelectedItem().GetColumnText(1));
//        cborevno.SetValue('');
//        dtrevdate.SetText('');
//        txtrevisionhistory.SetText('');
//        txtpreparedby.SetText('');
//        lblActive.SetText('Inactive');
//        lblActive.GetMainElement().style.color = 'Red';

        cboprocessid.PerformCallback('|' + cbopartid.GetValue() + '|' + cborevno.GetValue());
//        GridMenu.PerformCallback('ClearGridPart|');
}

function SelectProcessID(s, e) {
        cbocheckitem.PerformCallback('|' + cbopartid.GetValue() + '|' + cborevno.GetValue() + '|' + cboprocessid.GetValue());
//        GridMenu.PerformCallback('ClearGridPart|');
}

function SelectCheckItem(s, e) {
    GridMenu.PerformCallback();
}

function ClickSave(s, e) {
    GridMenu.PerformCallback();
}

    function ClickCopyFromOtherPart(s, e) {        
        var txt;
        var r = confirm("Are you sure to copy XR Data from other revision? Previous data will be delete!");
        if (r == true) {
//            cbolineidpopup.SetText('');
//            cbopartidpopup.SetText('');
//            txtpartnamepopup.SetText('');
//            cborevnopopup.SetText('');
            pcLogin.Show();
        }
}

function SelectRevNoPopUp(s, e) {
    cbocheckitempopup.PerformCallback('|' + cbopartid.GetValue() + '|' + cborevnopopup.GetValue() + '|' + cboprocessid.GetValue());
}

function ClickCopyPopUp(s, e) {
    GridMenu.PerformCallback('Copy|');
    pcLogin.Hide();
}

	</script>
    <style type="text/css">
        .style1
        {
            width: 25px;
        }
        .style2
        {
            height: 18px;
        }
        .style4
        {
            height: 18px;
            width: 4px;
        }
        .style5
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<div style="padding: 5px 5px 5px 5px">
         <div style="padding: 5px 5px 5px 5px">
        <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="Grid"
            EnableTheming="True" KeyFieldName="XID" Theme="Office2010Black" 
             Width="100%" OnAfterPerformCallback="Grid_AfterPerformCallback" 
                        OnRowDeleting="Grid_RowDeleting" 
                        OnRowInserting="Grid_RowInserting" 
                        OnRowValidating="Grid_RowValidating" 
                        OnStartRowEditing="Grid_StartRowEditing" 
            Font-Names="Segoe UI" Font-Size="9pt" >
            <ClientSideEvents EndCallback="OnEndCallback" />

            <Columns>
                <dx:GridViewCommandColumn FixedStyle="Left"
                    VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="false" 
                    ShowNewButtonInHeader="false" ShowClearFilterButton="true" 
                    ShowInCustomizationForm="True">
                    <HeaderStyle Paddings-PaddingLeft="3px" HorizontalAlign="Center" 
                        VerticalAlign="Middle" >
                        <Paddings PaddingLeft="3px"></Paddings>
                    </HeaderStyle>
                </dx:GridViewCommandColumn>

                <dx:GridViewDataTextColumn Caption="XID" FieldName="XID"
                    VisibleIndex="1" Width="75px" Settings-AutoFilterCondition="Contains" 
                    FixedStyle="Left" ShowInCustomizationForm="True">
                    <PropertiesTextEdit MaxLength="15" Width="75px">
                        <Style HorizontalAlign="Left"></Style>
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>
                    <EditFormSettings visibleindex="0"/>
                    <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="5px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="5px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="A2" FieldName="A2Value"
                    VisibleIndex="2" Width="75px" Settings-AutoFilterCondition="Contains" 
                    FixedStyle="Left" ShowInCustomizationForm="True">
                    <PropertiesTextEdit MaxLength="35" Width="75px">
                        <Style HorizontalAlign="Left"></Style>
                        <MaskSettings Mask="<0..99999999999999g>.<00..999>"/>
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>

                    <EditFormSettings visibleindex="2" />

                    <FilterCellStyle Paddings-PaddingRight="4px">
                    <Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="6px" HorizontalAlign="Center" VerticalAlign="Middle">
                    <Paddings PaddingLeft="6px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle"></CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="D4" FieldName="D4Value"
                    VisibleIndex="3" Width="75px" Settings-AutoFilterCondition="Contains" 
                    FixedStyle="Left" ShowInCustomizationForm="True">
                    <PropertiesTextEdit MaxLength="35" Width="75px">
                        <Style HorizontalAlign="Left"></Style>
                        <MaskSettings Mask="<0..99999999999999g>.<00..999>"/>
                    </PropertiesTextEdit>
                    <Settings AutoFilterCondition="Contains"></Settings>

                    <EditFormSettings visibleindex="3" />

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
            <SettingsPager Mode="ShowPager" PageSize="20" AlwaysShowPager="true" PageSizeItemSettings-Visible="true">
<PageSizeItemSettings Visible="True"></PageSizeItemSettings>
            </SettingsPager>
            <Settings ShowFilterRow="True" VerticalScrollBarMode="Auto" 
                VerticalScrollableHeight="250" HorizontalScrollBarMode="Auto" />
            <SettingsText ConfirmDelete="Are you sure want to delete ?"></SettingsText>

                        <SettingsPopup>
                <EditForm Modal="false" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Width="150" />
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
</div>

<%--<div style="padding: 0px 5px 5px 5px">
    <table style="width: 100%;">
        <tr>
            <td style="padding: 5px 0px 0px 0px; width: 50px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Line No." Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 100px">
               <dx:ASPxComboBox ID="cbolineid" runat="server" Theme="Office2010Black" TextField="LineName"
                    ClientInstanceName="cbolineid" DropDownStyle="DropDownList" ValueField="LineID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="1">
                    <ClientSideEvents SelectedIndexChanged="SelectLineID" />
                    <Columns>
                        <dx:ListBoxColumn Caption="Line ID" FieldName="LineID" Width="70px" />
                        <dx:ListBoxColumn Caption="Line Name" FieldName="LineName" 
                            Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px"/>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style="width: 30px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 65px">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Part No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 150px">
                <dx:ASPxComboBox ID="cbopartid" runat="server" Theme="Office2010Black" TextField="PartName"
                    ClientInstanceName="cbopartid" DropDownStyle="DropDownList" ValueField="PartID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                    Width="150px" TabIndex="2">
                    <ClientSideEvents SelectedIndexChanged="SelectPartID"/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Part ID" FieldName="PartID" Width="120px" />
                        <dx:ListBoxColumn Caption="Part Name" FieldName="PartName" Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style="width: 30px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 65px">
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Rev No." 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 100px">
                <dx:ASPxComboBox ID="cborevno" runat="server" Theme="Office2010Black" TextField="RevNo"
                    ClientInstanceName="cborevno" DropDownStyle="DropDownList" ValueField="RevNo"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="4">
                    <ClientSideEvents SelectedIndexChanged="SelectRevNo"/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Rev No" FieldName="RevNo" Width="70px"/>
                        <dx:ListBoxColumn Caption="Rev Date" FieldName="RevDate" Width="70px"/>
                        <dx:ListBoxColumn Caption="Rev History" FieldName="RevHistory" Width="200px"/>
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Width="5px" Paddings-Padding="4px" ></ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style="width: 30px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 65px">
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Check Item" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 150px;">
                <dx:ASPxComboBox ID="cbocheckitem" runat="server" Theme="Office2010Black" TextField="Item"
                    ClientInstanceName="cbocheckitem" DropDownStyle="DropDownList" ValueField="ItemID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0} - {1}" DisplayFormatString="{0}"
                    Width="50px" TabIndex="2"> 
                    <ClientSideEvents SelectedIndexChanged="SelectCheckItem"/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Item ID" FieldName="ItemID" Width="40px" />
                        <dx:ListBoxColumn Caption="Item Name" FieldName="Item" Width="150px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px" />
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style="padding: 5px 0px 0px 0px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="padding: 5px 0px 0px 0px; width: 50px">
                &nbsp;</td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 100px">
                &nbsp;</td>
            <td style="width: 30px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 65px">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Part Name" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 150px">
                <dx:ASPxTextBox ID="txtpartname" runat="server" BackColor="WhiteSmoke" 
                    ClientInstanceName="txtpartname" EnableTheming="True" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                    Theme="Office2010Black" Width="150px" ReadOnly="True" TabIndex="3">
                </dx:ASPxTextBox>
            </td>
            <td style="width: 30px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 65px">
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Process" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 100px">
               <dx:ASPxComboBox ID="cboprocessid" runat="server" Theme="Office2010Black" TextField="ProcessID"
                    ClientInstanceName="cboprocessid" DropDownStyle="DropDownList" ValueField="ProcessID"
                    EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                    Width="100px" TabIndex="1">
                    <ClientSideEvents SelectedIndexChanged="SelectProcessID"/>
                    <Columns>
                        <dx:ListBoxColumn Caption="Process ID" FieldName="ProcessID" Width="70px" />
                        <dx:ListBoxColumn Caption="Process Name" FieldName="ProcessName" 
                            Width="250px" />
                    </Columns>
                    <ItemStyle Height="10px" Paddings-Padding="4px"/>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style="width: 30px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 65px">
                &nbsp;</td>
            <td style="width: 20px">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width: 150px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
</div>--%>

<%--<div style="padding: 5px 5px 5px 5px">
         <div style="padding: 5px 5px 5px 5px">
        <dx:ASPxGridView ID="GridMenu" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridMenu"
            EnableTheming="True" KeyFieldName="XID" Theme="Office2010Black" 
            Width="100%" Font-Names="Segoe UI" Font-Size="9pt" OnRowValidating="GridMenu_RowValidating" OnStartRowEditing="GridMenu_StartRowEditing"
            OnRowInserting="GridMenu_RowInserting" OnRowDeleting="GridMenu_RowDeleting" 
            OnAfterPerformCallback="GridMenu_AfterPerformCallback">
            <ClientSideEvents EndCallback="OnEndCallback" BatchEditStartEditing="OnBatchEditStartEditing"/>
            <Columns>

                <dx:GridViewDataTextColumn Caption="X" FieldName="XID" ReadOnly="true"
                    VisibleIndex="1" Width="50px" Settings-AutoFilterCondition="Contains">
                    <PropertiesTextEdit MaxLength="15" Width="50px">
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

                <dx:GridViewDataTextColumn Caption="A2" FieldName="A2Value"
                    VisibleIndex="2" Width="100px" Settings-AutoFilterCondition="Contains">
                    <PropertiesTextEdit MaxLength="35" Width="100px">
                        <Style HorizontalAlign="Left"></Style>
                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..9999g>.<00..9999>"/>
                    </PropertiesTextEdit>

<Settings AutoFilterCondition="Contains"></Settings>

                    <FilterCellStyle Paddings-PaddingRight="4px">
<Paddings PaddingRight="4px"></Paddings>
                    </FilterCellStyle>
                    <HeaderStyle Paddings-PaddingLeft="6px" HorizontalAlign="Center" VerticalAlign="Middle">
<Paddings PaddingLeft="6px"></Paddings>
                    </HeaderStyle>
                    <CellStyle HorizontalAlign="Right"></CellStyle>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn Caption="D4" FieldName="D4Value"
                    VisibleIndex="3" Width="100px" Settings-AutoFilterCondition="Contains">
                    <PropertiesTextEdit MaxLength="50" Width="100px">
                        <Style HorizontalAlign="Left"></Style>
                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..9999g>.<00..9999>"/>
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
                
            </Columns>

            <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="Control" AllowFocusedRow="true"/>
            <SettingsEditing Mode="Batch"/>
            <SettingsPager Mode="ShowPager" PageSize="10" AlwaysShowPager="true" PageSizeItemSettings-Visible="true">
<PageSizeItemSettings Visible="True"></PageSizeItemSettings>
            </SettingsPager>
            <Settings ShowFilterRow="True" VerticalScrollBarMode="Auto" 
                VerticalScrollableHeight="250" HorizontalScrollBarMode="Auto" />
            <SettingsText ConfirmDelete="Are you sure want to delete ?"></SettingsText>
            <SettingsPopup>
                <EditForm Modal="false" HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" Width="200" />
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
</div>--%>

<%--<div style="padding: 0px 5px 5px 5px">
    <table class="auto-style3" style="width: 100%">
        <tr >
            <td style="width:10px; padding:5px 0px 0px 0px; border-top: 1px solid silver">
            </td>
            <td style=" border-top: 1px solid silver">
                &nbsp;</td>
            <td style=" border-top: 1px solid silver">
                &nbsp;</td>
            <td style=" border-top: 1px solid silver">
                &nbsp;</td>
            <td style=" border-top: 1px solid silver">
                &nbsp;</td>
            <td style="border-top: 1px solid silver">
                &nbsp;</td>
            <td style=" border-top: 1px solid silver">
                &nbsp;</td>
            <td style="border-top: 1px solid silver">
                &nbsp;</td>
            <td class="style1" 
                style="border-top: 1px solid silver; ">
                &nbsp;</td>
            <td class="style1" 
                style="border-top: 1px solid silver; ">
                &nbsp;</td>
            <td class="style1" 
                style="border-top: 1px solid silver; ">
                &nbsp;</td>
            <td style="border-top: 1px solid silver">
                &nbsp;</td>
            <td style="border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width:10px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnClear" runat="server" Text="Clear" UseSubmitBehavior="false"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="false"
                    ClientInstanceName="btnClear" Theme="Default" TabIndex="13">
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
            <td style="width:10px; border-top: 1px solid silver">
                &nbsp;</td>
            <td style="padding: 5px 0px 0px 0px; width:10px; border-top: 1px solid silver">
                <dx:ASPxButton ID="btnSave" runat="server" AutoPostBack="false" 
                    ClientInstanceName="btnSave" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Save" Theme="Default" UseSubmitBehavior="false" 
                    Width="90px" TabIndex="15" ClientEnabled="true">
                    <ClientSideEvents Click="function(s,e){GridMenu.UpdateEdit();}"/>
                  
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
        </tr>
        </table>
</div>--%>

<%--<div>
   <dx:ASPxPopupControl ID="pcLogin" runat="server" CloseAction="CloseButton"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcLogin"
        HeaderText="Copy data XR from other revision" AllowDragging="True" 
        PopupAnimationType="None" Height="260px" 
        Theme="Office2010Black" Width="350px">
        <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />
        <ModalBackgroundStyle BackColor="White" CssClass="&quot;noBackground&quot;">
        </ModalBackgroundStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK" Height="220px">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width: 60px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td class="style5" style="padding: 5px 0px 0px 0px; width: 50px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width: 60px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td class="style5" style="padding: 5px 0px 0px 0px; width: 50px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                    </td>
                                    <td class="style2">
                                        &nbsp;</td>
                                    <td class="style2" style="padding: 5px 0px 0px 0px; width: 60px">
                                        &nbsp;</td>
                                    <td class="style2" style="width: 10px">
                                        &nbsp;</td>
                                    <td class="style4" style="padding: 5px 0px 0px 0px; width: 50px">
                                        &nbsp;</td>
                                    <td class="style2">
                                        &nbsp;</td>
                                    <td class="style2">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width: 60px">
                                        <dx:ASPxLabel ID="ASPxLabel18" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Rev No.">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td class="style5" style="padding: 5px 0px 0px 0px; width: 50px">
                                        <dx:ASPxComboBox ID="cborevnopopup" runat="server" 
                                            ClientInstanceName="cborevnopopup" DisplayFormatString="{0}" 
                                            EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="25px" IncrementalFilteringMode="StartsWith" TabIndex="20" 
                                            TextField="RevNo" TextFormatString="{0}" Theme="Office2010Black" 
                                            ValueField="RevNo" Width="100px">
                                            <ClientSideEvents SelectedIndexChanged="SelectRevNoPopUp"/>
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Rev No" FieldName="RevNo" Width="70px" />
                                                <dx:ListBoxColumn Caption="Rev Date" FieldName="RevDate" Width="70px" />
                                                <dx:ListBoxColumn Caption="Rev History" FieldName="RevHistory" Width="200px" />
                                            </Columns>
                                            <ItemStyle Height="10px">
                                            <Paddings Padding="4px" />
                                            </ItemStyle>
                                            <ButtonStyle Width="5px">
                                                <Paddings Padding="4px" />
                                            </ButtonStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width: 60px">
                                        <dx:ASPxLabel ID="ASPxLabel19" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="Check Item">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td class="style5" style="padding: 5px 0px 0px 0px; width: 50px">
                                        <dx:ASPxComboBox ID="cbocheckitempopup" runat="server" 
                                            ClientInstanceName="cbocheckitempopup" DisplayFormatString="{1}" 
                                            EnableIncrementalFiltering="True" Font-Names="Segoe UI" Font-Size="9pt" 
                                            Height="25px" IncrementalFilteringMode="StartsWith" TabIndex="2" 
                                            TextField="Item" TextFormatString="{0} - {1}" Theme="Office2010Black" 
                                            ValueField="ItemID" Width="150px">
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Item ID" FieldName="ItemID" Width="120px" />
                                                <dx:ListBoxColumn Caption="Item Name" FieldName="Item" Width="250px" />
                                            </Columns>
                                            <ItemStyle Height="10px">
                                            <Paddings Padding="4px" />
                                            </ItemStyle>
                                            <ButtonStyle Width="5px">
                                                <Paddings Padding="4px" />
                                            </ButtonStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width: 60px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td class="style5" style="padding: 5px 0px 0px 0px; " colspan="3">
                                        <dx:ASPxButton ID="btnCopyPopUp" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="btnCopyPopUp" Height="25px" 
                                            style="float: left; margin-right: 8px" Text="Copy" Width="70px">
                                            <ClientSideEvents Click="ClickCopyPopUp" />
                                        </dx:ASPxButton>
                                        <dx:ASPxButton ID="btCancel0" runat="server" AutoPostBack="False" Height="25px" 
                                            style="float: left; margin-right: 8px" Text="Cancel" Width="70px">
                                            <ClientSideEvents Click="function(s, e) { pcLogin.Hide(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width: 60px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td class="style5" style="padding: 5px 0px 0px 0px; width: 50px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td style="padding: 5px 0px 0px 0px; width: 60px">
                                        &nbsp;</td>
                                    <td style="width: 10px">
                                        &nbsp;</td>
                                    <td class="style5" style="padding: 5px 0px 0px 0px; width: 50px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                    </td>
                                    <td class="style1">
                                        &nbsp;</td>
                                    <td class="style1" style="padding: 5px 0px 0px 0px; width: 60px">
                                        &nbsp;</td>
                                    <td class="style1" style="width: 10px">
                                        &nbsp;</td>
                                    <td class="style5" style="padding: 5px 0px 0px 0px; width: 50px">
                                        &nbsp;</td>
                                    <td class="style1">
                                        &nbsp;</td>
                                    <td class="style1">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>    
</div>--%>
</asp:Content>
