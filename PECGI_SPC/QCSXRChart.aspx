<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="QCSXRChart.aspx.vb" Inherits="PECGI_SPC.QCSXRChart" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v14.1.Web, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" >
    function OnInit(s, e) {
        var today = new Date();
        dtFrom.SetDate(today);
        dtTo.SetDate(today);
    }

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
        var formula;
        if (s.cp_TotalX == null) {
            txtFormula2a.SetText('');
            txtFormula3a.SetText('');
        } else {
            formula = s.cp_TotalX + ':' + s.cp_TotalSeq;
            txtFormula2a.SetText(formula);
            txtFormula3a.SetText(s.cp_ResultX);
        }
        if (s.cp_TotalR == null) {
            txtFormula2b.SetText('');
            txtFormula3b.SetText('');
        } else {
            formula = s.cp_TotalR + ':' + s.cp_TotalSeq;
            txtFormula2b.SetText(formula);
            txtFormula3b.SetText(s.cp_ResultR);
        }
        if (s.cp_XUCL == null || s.cp_XLCL == null) {
            txtX2a.SetText('');
            txtXUCL.SetText('');
            txtX2b.SetText('');
            txtXLCL.SetText('');
            txtR2.SetText('');
            txtRUCL.SetText('');
        } else {
            formula = s.cp_ResultX + '+(' + s.cp_A2Value + 'x' + s.cp_ResultR + ')';
            txtX2a.SetText(formula);
            txtXUCL.SetText(s.cp_XUCL);
            formula = s.cp_ResultX + '-(' + s.cp_A2Value + 'x' + s.cp_ResultR + ')';
            txtX2b.SetText(formula);
            txtXLCL.SetText(s.cp_XLCL);

            formula = s.cp_D4Value + 'x' + s.cp_ResultR;
            txtR2.SetText(formula);
            txtRUCL.SetText(s.cp_RUCL);
        }
        txtXUCLAdj.SetText(s.cp_XUCLAdj);
        txtXLCLAdj.SetText(s.cp_XLCLAdj);
        txtRUCLAdj.SetText(s.cp_RUCLAdj);
        var xUCL = s.cp_XUCL;
        var xLCL = s.cp_XLCL;
        var rUCL = s.cp_RUCL;
        if (s.cp_XUCLAdj != '') { xUCL = s.cp_XUCLAdj; }
        if (s.cp_XLCLAdj != '') { xLCL = s.cp_XLCLAdj; }
        if (s.cp_RUCLAdj != '') { rUCL = s.cp_RUCLAdj; }
        chartX.PerformCallback(xUCL + '|' + xLCL + '|' + s.cp_XBar);
        chartR.PerformCallback(rUCL + '|' + s.cp_RBar);
        gridAction.PerformCallback(dtFrom.GetText() + '|' + dtTo.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + txtProcessID.GetValue() + '|' + cboPartID.GetValue() + '|' + txtXRCode.GetText());        
        gridSummary.PerformCallback(dtFrom.GetText() + '|' + dtTo.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboPartID.GetValue() + '|' + cboMachine.GetValue() + '|' + cboItem.GetValue() + '|' + xUCL + '|' + xLCL);
    }

    function ActionOnEndCallback(s, e) {
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

        if (s.IsEditing()) {
            var form = s.GetPopupEditForm();            
            form.PopUp.AddHandler(function (s, e) {
                var editor = gridAction.GetEditor('Date');
                editor.Focus();
            });
        }

        if (s.cp_dataexist == 1) {
            toastr.warning('Date is already exist!', 'Warning');
            toastr.options.closeButton = false;
            toastr.options.debug = false;
            toastr.options.newestOnTop = false;
            toastr.options.progressBar = false;
            toastr.options.preventDuplicates = true;
            toastr.options.onclick = null;

            e.processOnServer = false;
            s.cp_dataexist = null;
            return;
        } 
    }

    function OnBatchEditStartEditing(s, e) {
        var selectedDate = e.focusedColumn.name;
        var rowIndex = e.visibleIndex;
        var columnIndex = e.focusedColumn.index;
        var levelno = grid.batchEditApi.GetCellValue(rowIndex, 'Des');
        if (levelno == '') {
            window.open('QCSResultInquiry.aspx?Date=' + selectedDate + '&LineID=' + cboLineID.GetValue() + '&SubLineID=' + cboSubLine.GetText() + '&PartID=' + cboPartID.GetValue() + '&PartName=' + txtPartName.GetText(), "NewWindow");
        }
        e.cancel = true;
    }

    function FillCboLine(s, e) {
        cboLineID.PerformCallback(dtFrom.GetText() + '|' + dtTo.GetText());
        cboSubLine.ClearItems();
        cboPartID.ClearItems();
        txtPartName.SetText('');
        cboMachine.ClearItems();
        txtProcess.SetText('');
        txtProcessID.SetText('');
        cboItem.ClearItems();
    }

    function CboLineChanged(s, e) {
        cboSubLine.SetEnabled(false);
        cboSubLine.PerformCallback('load|' + cboLineID.GetValue());
        cboPartID.SetEnabled(false);
        cboPartID.PerformCallback(dtFrom.GetText() + '|' + dtTo.GetText() + '|' + cboLineID.GetValue());
        cboItem.SetEnabled(false);
        cboItem.PerformCallback('load|' + cboLineID.GetValue() + '|' + cboPartID.GetValue() + '|' + cboMachine.GetValue());
        cboMachine.SetEnabled(false);
        cboMachine.PerformCallback('load|' + cboLineID.GetValue() + '|');
        txtPartName.SetText('');
        txtProcess.SetText('');        
    }

    function cboMachineChanged(s, e) {
        cboItem.SetEnabled(false);
        txtProcess.SetText(cboMachine.GetSelectedItem().GetColumnText(1));
        txtProcessID.SetText(cboMachine.GetSelectedItem().GetColumnText(2));
        cboItem.PerformCallback('load|' + cboLineID.GetValue() + '|' + cboPartID.GetValue() + '|' + cboMachine.GetValue());
    }

    function FillCboMachine(s, e) {
        cboMachine.SetEnabled(false);
        cboMachine.PerformCallback('load|' + cboLineID.GetValue() + '|' + cboSubLine.GetValue());
        txtProcess.SetText('');
    }

    function GridLoad() {
        grid.PerformCallback('load|' + dtFrom.GetText() + '|' + dtTo.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboMachine.GetValue() + '|' + cboPartID.GetValue() + '|' + cboItem.GetValue() + '|' + txtProcess.GetText() + '|' + txtXRCode.GetText() );
    }

    function UpdateXUCL() {
        cbkUpdate.PerformCallback(txtXUCLAdj.GetText() + '|' + txtXLCLAdj.GetText() + '|' + txtRUCLAdj.GetText() + '|' + cboPartID.GetValue() + '|' + txtXRCode.GetValue() + '|' + dtFrom.GetText() + '|' + txtXUCL.GetText() + '|' + txtXLCL.GetText() + '|' + txtRUCL.GetText() );
    }

    function CboPartIDChanged(s, e) {
        cboItem.SetEnabled(false);
        txtPartName.SetText(cboPartID.GetSelectedItem().GetColumnText(1));
        cboItem.PerformCallback('load|' + cboLineID.GetValue() + '|' + cboPartID.GetValue() + '|' + cboMachine.GetValue());
    }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div style="padding: 0px 5px 5px 5px">
        <table>
        <tr>
            <td style="width:50px">
            
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Date" Font-Names="Segoe UI" 
                        Font-Size="9pt">
                    </dx:ASPxLabel>
            
            </td>
            <td style="width:110px">
            
                    <dx:ASPxDateEdit ID="dtFrom" runat="server" Theme="Office2010Black" 
                        Width="100px"
                            ClientInstanceName="dtFrom" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                            Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="5" 
                        EditFormat="Custom">
                            <CalendarProperties>
                                <HeaderStyle Font-Size="12pt" Paddings-Padding="5px" >
    <Paddings Padding="5px"></Paddings>
                                </HeaderStyle>
                                <DayStyle Font-Size="9pt" Paddings-Padding="5px" >
    <Paddings Padding="5px"></Paddings>
                                </DayStyle>
                                <WeekNumberStyle Font-Size="9pt" Paddings-Padding="5px">
    <Paddings Padding="5px"></Paddings>
                                </WeekNumberStyle>
                                <FooterStyle Font-Size="9pt" Paddings-Padding="10px" >
    <Paddings Padding="10px"></Paddings>
                                </FooterStyle>
                                <ButtonStyle Font-Size="9pt" Paddings-Padding="10px">
    <Paddings Padding="10px"></Paddings>
                                </ButtonStyle>
                            </CalendarProperties>
                            <ClientSideEvents Init="OnInit" />
                            <ButtonStyle Width="5px" Paddings-Padding="4px" >
    <Paddings Padding="4px"></Paddings>
                            </ButtonStyle>
                        </dx:ASPxDateEdit>
            
            </td>
            <td style="width:60px">
            
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="To" 
                        Font-Names="Segoe UI" Font-Size="9pt">
                    </dx:ASPxLabel>
            
            </td>
            <td style="width:110px">
            
                    <dx:ASPxDateEdit ID="dtTo" runat="server" Theme="Office2010Black" 
                        Width="100px"
                            ClientInstanceName="dtTo" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                            Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="5" 
                        EditFormat="Custom">
                            <CalendarProperties>
                                <HeaderStyle Font-Size="12pt" Paddings-Padding="5px" >
    <Paddings Padding="5px"></Paddings>
                                </HeaderStyle>
                                <DayStyle Font-Size="9pt" Paddings-Padding="5px" >
    <Paddings Padding="5px"></Paddings>
                                </DayStyle>
                                <WeekNumberStyle Font-Size="9pt" Paddings-Padding="5px">
    <Paddings Padding="5px"></Paddings>
                                </WeekNumberStyle>
                                <FooterStyle Font-Size="9pt" Paddings-Padding="10px" >
    <Paddings Padding="10px"></Paddings>
                                </FooterStyle>
                                <ButtonStyle Font-Size="9pt" Paddings-Padding="10px">
    <Paddings Padding="10px"></Paddings>
                                </ButtonStyle>
                            </CalendarProperties>
                            <ButtonStyle Width="5px" Paddings-Padding="4px" >
    <Paddings Padding="4px"></Paddings>
                            </ButtonStyle>
                        </dx:ASPxDateEdit>
            
            </td>
            <td style="width:60px">
            
                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Machine No." 
                        Font-Names="Segoe UI" Font-Size="9pt" Width="90px">
                    </dx:ASPxLabel>
            
            </td>
            <td style="width:110px">
            
    <dx:ASPxComboBox ID="cboMachine" runat="server" Theme="Office2010Black" TextField="MachineNo"
                        ClientInstanceName="cboMachine" ValueField="MachineNo" Font-Names="Segoe UI" 
                        Font-Size="9pt" Height="25px" 
                        IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                        Width="100px" TabIndex="2">
                        <ClientSideEvents SelectedIndexChanged="cboMachineChanged" EndCallback="function(s, e) {cboMachine.SetEnabled(true);}" />
                        <Columns>
                            <dx:ListBoxColumn Caption="Machine No." FieldName="MachineNo" Width="100px" />
                            <dx:ListBoxColumn Caption="Process" FieldName="ProcessName" Width="160px" />
                            <dx:ListBoxColumn Caption="ProcessID" FieldName="ProcessID" Width="60px" />
                        </Columns>
                        <ItemStyle Height="10px" Paddings-Padding="4px" >
    <Paddings Padding="4px"></Paddings>
                        </ItemStyle>
                        <ButtonStyle Paddings-Padding="4px" Width="5px">
    <Paddings Padding="4px"></Paddings>
                        </ButtonStyle>
                    </dx:ASPxComboBox>
                
            </td>
            <td style="width:60px">
            
                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Part No." 
                            Font-Names="Segoe UI" Font-Size="9pt" Width="60px">
                        </dx:ASPxLabel>                
            
            </td>
            <td style="width:135px">
            
    <dx:ASPxComboBox ID="cboPartID" runat="server" Theme="Office2010Black" TextField="PartName"
                        ClientInstanceName="cboPartID" ValueField="PartID" Font-Names="Segoe UI" 
                        Font-Size="9pt" Height="25px" 
                        IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                        Width="125px" TabIndex="2" EnableCallbackMode="true">
                        <ClientSideEvents SelectedIndexChanged="CboPartIDChanged" 
                        EndCallback="function(s, e) {cboPartID.SetEnabled(true);}"
                        />
                        <Columns>
                            <dx:ListBoxColumn Caption="Part ID" FieldName="PartID" Width="120px" />
                            <dx:ListBoxColumn Caption="Part Name" FieldName="PartName" Width="250px" />
                        </Columns>
                        <ItemStyle Height="10px" Paddings-Padding="4px" >
    <Paddings Padding="4px"></Paddings>
                        </ItemStyle>
                        <ButtonStyle Paddings-Padding="4px" Width="5px">
    <Paddings Padding="4px"></Paddings>
                        </ButtonStyle>
                    </dx:ASPxComboBox>
                
            </td>
            <td style="width:60px">
            
                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Item Check" 
                            Font-Names="Segoe UI" Font-Size="9pt" Width="80px" Height="16px">
                        </dx:ASPxLabel>                
            
            </td>
            <td style="width:160px">
                <dx:ASPxComboBox ID="cboItem" runat="server" Theme="Office2010Black" TextField="Item"
                                        ClientInstanceName="cboItem" ValueField="ItemID" Font-Names="Segoe UI" 
                                        Font-Size="9pt" Height="25px" 
                                        IncrementalFilteringMode="Contains" 
                    TextFormatString="{1}" DisplayFormatString="{1}"
                                        Width="150px" TabIndex="2">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                        txtXRCode.SetText(cboItem.GetSelectedItem().GetColumnText(2));
                                        }" 
                                        EndCallback="function(s, e) {cboItem.SetEnabled(true);}"
                                        />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Item ID" FieldName="ItemID" Width="0px" />
                                            <dx:ListBoxColumn Caption="Item Name" FieldName="Item" Width="200px" />
                                            <dx:ListBoxColumn Caption="XR Code" FieldName="XRCode" Width="0px" />
                                        </Columns>
                                        <ItemStyle Height="10px" Paddings-Padding="4px" >
                    <Paddings Padding="4px"></Paddings>
                                        </ItemStyle>
                                        <ButtonStyle Paddings-Padding="4px" Width="5px">
                    <Paddings Padding="4px"></Paddings>
                                        </ButtonStyle>
                                    </dx:ASPxComboBox>            
            </td>
        </tr>

        <tr>
            <td>
            
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Line No." 
                        Font-Names="Segoe UI" Font-Size="9pt" Width="60px">
                    </dx:ASPxLabel>
            
            </td>
            <td style="padding-top: 3px">
            
                    <dx:ASPxComboBox ID="cboLineID" runat="server" Theme="Office2010Black" TextField="LineName"
                        ClientInstanceName="cboLineID" ValueField="LineID" Font-Names="Segoe UI" 
                        Font-Size="9pt" Height="25px" 
                        IncrementalFilteringMode="Contains" TextFormatString="{0}-{1}" DisplayFormatString="{0}"
                        Width="100px" TabIndex="1">
                        <ClientSideEvents SelectedIndexChanged="CboLineChanged"/>
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
            <td>
            
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Sub Line No." 
                        Font-Names="Segoe UI" Font-Size="9pt" Width="80px">
                    </dx:ASPxLabel>
            
            </td>
            <td style="padding-top: 3px">
            
                    
                    <dx:ASPxComboBox ID="cboSubLine" runat="server" Theme="Office2010Black" 
                        ClientInstanceName="cboSubLine" ValueField="SubLineID" Font-Names="Segoe UI" 
                        Font-Size="9pt" Height="25px" 
                        IncrementalFilteringMode="Contains" TextFormatString="{0}" DisplayFormatString="{0}"
                        Width="100px" TabIndex="1">
                        <ClientSideEvents SelectedIndexChanged="FillCboMachine" 
                        EndCallback="function(s, e) {cboSubLine.SetEnabled(true);}"
                        />
                        <Columns>
                            <dx:ListBoxColumn Caption="Sub Line ID" FieldName="SubLineID" Width="70px" />
                        </Columns>
                        <ItemStyle Height="10px" Paddings-Padding="4px">
    <Paddings Padding="4px"></Paddings>
                        </ItemStyle>
                        <ButtonStyle Paddings-Padding="4px" Width="5px">
    <Paddings Padding="4px"></Paddings>
                        </ButtonStyle>
                    </dx:ASPxComboBox>
                
                    
            </td>
            <td>
            
                    <dx:ASPxLabel ID="lblqeleader0" runat="server" Text="Process" 
                        Font-Names="Segoe UI" Font-Size="9pt">
                    </dx:ASPxLabel>
            
            </td>
            <td style="padding-top: 3px">
            
                    <dx:ASPxTextBox ID="txtProcess" runat="server" BackColor="WhiteSmoke" 
                        ClientInstanceName="txtProcess" EnableTheming="True" Font-Names="Segoe UI" 
                        Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                        Theme="Office2010Black" Width="100px" ReadOnly="True" TabIndex="3">
                    </dx:ASPxTextBox>
            
            </td>
            <td>
            
                    <dx:ASPxLabel ID="lblqeleader1" runat="server" Text="Part Name" 
                        Font-Names="Segoe UI" Font-Size="9pt" Width="80px">
                    </dx:ASPxLabel>
            
            </td>
            <td style="padding-top: 3px">
            
                    <dx:ASPxTextBox ID="txtPartName" runat="server" BackColor="WhiteSmoke" 
                        ClientInstanceName="txtPartName" EnableTheming="True" Font-Names="Segoe UI" 
                        Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                        Theme="Office2010Black" Width="125px" ReadOnly="True" TabIndex="3">
                    </dx:ASPxTextBox>
            
            </td>
            <td>
            
                    &nbsp;</td>
            <td style="padding-top: 3px">
            
                    <dx:ASPxButton ID="btnSearch" runat="server" AutoPostBack="False" 
                        ClientInstanceName="btnSearch" Font-Names="Segoe UI" Font-Size="9pt" 
                        Height="25px" Text="Search" Theme="Default" UseSubmitBehavior="False" 
                        Width="90px" TabIndex="16">
                        <ClientSideEvents Click="function(s, e) {                                    
                                    var msg = '';
                                    if (cboLineID.GetText() == '') {
                                        cboLineID.Focus();
                                        msg = 'Please select Line!';
                                    } else if (cboSubLine.GetText() == '') {
                                        cboSubLine.Focus();
                                        msg = 'Please select Sub Line!';
                                    } else if (cboPartID.GetText() == '') {
                                        cboPartID.Focus();
                                        msg = 'Please select Part ID!';
                                    } else if (cboItem.GetText() == '') {
                                        cboItem.Focus();
                                        msg = 'Please select Item!';
                                    } 
                                    
                                    if (msg != '') {
                                        toastr.warning(msg, 'Warning');
                                        toastr.options.closeButton = false;
                                        toastr.options.debug = false;
                                        toastr.options.newestOnTop = false;
                                        toastr.options.progressBar = false;
                                        toastr.options.preventDuplicates = true;
                                        toastr.options.onclick = null;
                                        e.processOnServer = false;
                                        return;
                                    }

	                        GridLoad();	
                        }" />
                        <Paddings Padding="2px" />
                    </dx:ASPxButton>
            
            </td>
        </tr>
    </table>
    </div>
<div style="height:6px">
    <hr />
</div>
    
<div style="height:26px; " align="center">
    <dx:ASPxLabel ID="ASPxLabel29" runat="server" Text="X-R SHEET" 
        Font-Names="Segoe UI" Font-Size="12pt" Font-Bold="True" 
        Font-Underline="True" ></dx:ASPxLabel>
</div>
<div style="padding: 2px 5px 5px 5px">
        <dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
            EnableTheming="True" KeyFieldName="Seq" Theme="Office2010Black" 
            OnAfterPerformCallback="grid_AfterPerformCallback" 
            Width="100%" 
            Font-Names="Segoe UI" Font-Size="9pt">
            <ClientSideEvents EndCallback="OnEndCallback" 
                BatchEditStartEditing="OnBatchEditStartEditing" 
             />
            <Columns>
                <dx:GridViewBandColumn Caption="DATE" VisibleIndex="0">
                    <Columns>
                        <dx:GridViewBandColumn Caption="SHIFT" VisibleIndex="0">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="SEQUENCE" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:GridViewBandColumn>
                    </Columns>
                </dx:GridViewBandColumn>
            </Columns>
        <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" 
                AllowFocusedRow="True" AllowDragDrop="False" AllowSort="False" />
        <SettingsEditing Mode="Batch" >
            <BatchEditSettings ShowConfirmOnLosingChanges="False" />
            </SettingsEditing>
        <SettingsPager AlwaysShowPager="true" Mode="ShowAllRecords" PageSize="30">
        </SettingsPager>
        <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="250" 
            VerticalScrollBarMode="Auto" ShowStatusBar="Hidden" />
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="false" 
                VerticalAlign="TopSides" Width="320" VerticalOffset="0" Height="300" />
        </SettingsPopup>
            <SettingsDataSecurity AllowDelete="False" 
                AllowInsert="False" />
        <Styles EditFormColumnCaption-Paddings-PaddingLeft="10px" 
            EditFormColumnCaption-Paddings-PaddingRight="10px" 
            Header-Paddings-Padding="5px">
            <Header HorizontalAlign="Center" Wrap="True">
                <Paddings Padding="2px" />
            </Header>
            <EditFormColumnCaption Font-Names="Segoe UI" Font-Size="9pt">
                <Paddings PaddingBottom="5px" PaddingLeft="15px" PaddingRight="15px" 
                    PaddingTop="5px" />
            </EditFormColumnCaption>
            <CommandColumnItem ForeColor="SteelBlue">
            </CommandColumnItem>
        </Styles>
    </dx:ASPxGridView>
</div>
<div style="height:10px">
    <hr />
</div>
<div style="padding: 0px 5px 5px 5px; width: 100%;">
    <table style="width: 100%">
        <tr style="border: 1px">
            <td style="padding: 5px; width: 30%" valign="top">
                <table style="border: 1px solid silver; padding: 5px; width:100%">
                    <tr>
                        <td colspan="7" align="center" style="background-color: silver">
                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="FORMULA" Font-Names="Segoe UI" Font-Size="9pt" >
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">&nbsp;
                        </td>
                    </tr>
                    <tr style="height: 28px;">
                        <td style="">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="X" Font-Names="Segoe UI" Font-Size="9pt" >
                            </dx:ASPxLabel>
                        </td>
                        <td style="">
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="=" Font-Names="Segoe UI" Font-Size="9pt" >
                            </dx:ASPxLabel>
                        </td>
                        <td style="">
                            <dx:ASPxTextBox ID="txtFormula1a" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Text="Total X : Total Seq" Width="120px" 
                                ClientInstanceName="txtFormula1a" ReadOnly="True" BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="">
                                                    
                        </td>
                        <td style="">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="R" Font-Names="Segoe UI" Font-Size="9pt" ></dx:ASPxLabel>                        
                        </td>
                        <td style="">
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="=" Font-Names="Segoe UI" Font-Size="9pt" ></dx:ASPxLabel>
                        </td>
                        <td style="">
                            <dx:ASPxTextBox ID="txtFormula1b" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Text="Total R : Total Seq" Width="120px" 
                                ClientInstanceName="txtFormula1b" ReadOnly="True" BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>                        
                        </td>                        
                    </tr>
                    <tr style="height: 28px;">
                        <td style=""></td>
                        <td style="">
                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="=">
                            </dx:ASPxLabel>
                        </td>
                        <td style="">
                            <dx:ASPxTextBox ID="txtFormula2a" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="120px" 
                                ClientInstanceName="txtFormula2a" ReadOnly="True" BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>
                        </td>
                        <td style=""></td>
                        <td style=""></td>
                        <td style=""><dx:ASPxLabel ID="ASPxLabel25" runat="server" Text="=" Font-Names="Segoe UI" Font-Size="9pt" ></dx:ASPxLabel></td>
                        <td style="">
                            <dx:ASPxTextBox ID="txtFormula2b" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="120px" 
                                ClientInstanceName="txtFormula2b" ReadOnly="True" BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>                        
                        </td>
                    </tr>
                    <tr style="height: 28px;">
                        <td style=""></td>
                        <td style="">
                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="=">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtFormula3a" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="60px" ClientInstanceName="txtFormula3a" 
                                ReadOnly="True" BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td></td>
                        <td style=""><dx:ASPxLabel ID="ASPxLabel26" runat="server" Text="=" Font-Names="Segoe UI" Font-Size="9pt" ></dx:ASPxLabel></td>
                        <td>
                            <dx:ASPxTextBox ID="txtFormula3b" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="60px" ClientInstanceName="txtFormula3b" 
                                ReadOnly="True" BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="padding: 5px; width: 25%" valign="top">
                <table style="border: 1px solid silver; padding: 5px; width:100%">
                    <tr>
                        <td colspan="9" align="center" style="background-color: Silver">
                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="CONTROL X" Font-Names="Segoe UI" Font-Size="9pt" >
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9">&nbsp;
                        </td>
                    </tr>
                    <tr style="height: 28px">
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="UCL" Font-Names="Segoe UI" Font-Size="9pt" >
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="=" Font-Names="Segoe UI" Font-Size="9pt" >
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="2">
                            <dx:ASPxTextBox ID="txtX1a" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Text="X + A2R" Width="70px" ClientInstanceName="txtX1a" 
                                ReadOnly="True" BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td style="padding-left: 5px"><dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="LCL" Font-Names="Segoe UI" Font-Size="9pt" ></dx:ASPxLabel></td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="=" Font-Names="Segoe UI" Font-Size="9pt" >
                            </dx:ASPxLabel>                        
                        </td>
                        <td colspan="2">
                            <dx:ASPxTextBox ID="txtX1b" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Text="X - A2R" Width="70px" ClientInstanceName="txtX1b" 
                                ReadOnly="True" BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>                        
                        </td>                        
                    </tr>
                    <tr style="height: 28px">
                        <td></td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="=">
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="2">
                            <dx:ASPxTextBox ID="txtX2a" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="120px" 
                                ClientInstanceName="txtX2a" ReadOnly="True" BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td colspan="2">
                            <dx:ASPxTextBox ID="txtX2b" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="120px" ClientInstanceName="txtX2b" 
                                ReadOnly="True" BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>                        
                        </td>
                    </tr>
                    <tr style="height: 28px">
                        <td></td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="=">
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="2">
                            <dx:ASPxTextBox ID="txtXUCL" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="60px" ClientInstanceName="txtXUCL" ReadOnly="True" 
                                BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td colspan="2">
                            <dx:ASPxTextBox ID="txtXLCL" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="60px" ClientInstanceName="txtXLCL" ReadOnly="True" 
                                BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td colspan="2">
                            <dx:ASPxLabel ID="ASPxLabel30" runat="server" Text="Adjustment" 
                                Font-Names="Segoe UI" Font-Size="9pt" >
                            </dx:ASPxLabel>
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td colspan="2">
                            <dx:ASPxLabel ID="ASPxLabel31" runat="server" Text="Adjustment" 
                                Font-Names="Segoe UI" Font-Size="9pt" >
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <dx:ASPxTextBox ID="txtXUCLAdj" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="40px" ClientInstanceName="txtXUCLAdj">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <dx:ASPxTextBox ID="txtXLCLAdj" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="40px" ClientInstanceName="txtXLCLAdj">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="9" align="right">&nbsp;</td>
                    </tr>
                </table>            
            </td>
            <td style="padding: 5px; width: 25%" valign="top">
            
                <table style="border: 1px solid silver; padding: 5px; width:100%">
                    <tr>
                        <td colspan="4" align="center" style="background-color: Silver">
                            <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="CONTROL R" 
                                Font-Names="Segoe UI" Font-Size="9pt" >
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr style="height: 28px">
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="UCL" Font-Names="Segoe UI" 
                                Font-Size="9pt" >
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="=" Font-Names="Segoe UI" 
                                Font-Size="9pt" >
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="2">
                            <dx:ASPxTextBox ID="txtR1" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Text="D4 R" Width="120px" ClientInstanceName="txtR1" 
                                ReadOnly="True" BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>
                        </td>
                     
                    </tr>
                    <tr style="height: 28px">
                        <td></td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel27" runat="server" Text="=">
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="2">
                            <dx:ASPxTextBox ID="txtR2" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="120px" 
                                ClientInstanceName="txtR2" ReadOnly="True" BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr style="height: 28px">
                        <td></td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel28" runat="server" Text="=">
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="2">
                            <dx:ASPxTextBox ID="txtRUCL" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="60px" ClientInstanceName="txtRUCL" ReadOnly="True" 
                                BackColor="WhiteSmoke">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td><td></td><td colspan="2">
                            <dx:ASPxLabel ID="ASPxLabel32" runat="server" Text="Adjustment" 
                                Font-Names="Segoe UI" Font-Size="9pt" >
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td></td><td></td>
                        <td>
                            <dx:ASPxTextBox ID="txtRUCLAdj" runat="server" Font-Names="Segoe UI" 
                                Font-Size="9pt" Width="40px" ClientInstanceName="txtRUCLAdj">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr dir="ltr">
                        <td colspan="4">&nbsp;</td>
                    </tr>
                </table>            
            
            </td>
            <td style="padding: 5px; width: 20%" valign="top">
            
        <dx:ASPxGridView ID="gridDet" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridDet"
            EnableTheming="True" KeyFieldName="XID" Theme="Office2010Black" Width="100%" 
            Font-Names="Segoe UI" Font-Size="9pt">
            <Columns>
                <dx:GridViewBandColumn Caption="TABLE A2 &amp; D4" VisibleIndex="0">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="X" ShowInCustomizationForm="True" 
                            VisibleIndex="0" Width="40px" FieldName="XID">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="A2" ShowInCustomizationForm="True" 
                            VisibleIndex="1" Width="60px" FieldName="A2Value">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="D4" ShowInCustomizationForm="True" 
                            VisibleIndex="2" Width="60px" FieldName="D4Value">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
            </Columns>
        <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" 
                AllowFocusedRow="True" AllowDragDrop="False" AllowSort="False" />
        <SettingsEditing Mode="Batch" >
            <BatchEditSettings ShowConfirmOnLosingChanges="False" />
            </SettingsEditing>
        <SettingsPager AlwaysShowPager="true" Mode="ShowAllRecords" PageSize="30">
        </SettingsPager>
        <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="126" 
                ShowStatusBar="Hidden" />
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="false" 
                VerticalAlign="WindowCenter" Width="320" />
        </SettingsPopup>
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" 
                AllowInsert="False" />
        <Styles EditFormColumnCaption-Paddings-PaddingLeft="10px" 
            EditFormColumnCaption-Paddings-PaddingRight="10px" 
            Header-Paddings-Padding="5px">
            <Header HorizontalAlign="Center" Wrap="True">
                <Paddings Padding="2px" />
            </Header>
            <EditFormColumnCaption Font-Names="Segoe UI" Font-Size="9pt">
                <Paddings PaddingBottom="5px" PaddingLeft="15px" PaddingRight="15px" 
                    PaddingTop="5px" />
            </EditFormColumnCaption>
            <CommandColumnItem ForeColor="SteelBlue">
            </CommandColumnItem>
        </Styles>        
    </dx:ASPxGridView>
            
            </td>
        </tr>

    </table>
</div>

<div style="width: 100%;">
    <table width="100%">
        <tr style="height:20px">
            <td style="width: 30%" align="center">
            
                            &nbsp;</td>
            <td align="center">
            
                            <dx:ASPxButton ID="btnUpdateXUCL" runat="server" Text="Save" 
                                AutoPostBack="False" ClientInstanceName="btnUpdateXUCL" Width="90px">
                                <ClientSideEvents Click="function(s, e) {
                                    var msg = '';
                                    if (cboLineID.GetText() == '') {
                                        cboLineID.Focus();
                                        msg = 'Please select Line!';
                                    } else if (cboSubLine.GetText() == '') {
                                        cboSubLine.Focus();
                                        msg = 'Please select Sub Line!';
                                    } else if (cboPartID.GetText() == '') {
                                        cboPartID.Focus();
                                        msg = 'Please select Part ID!';
                                    } else if (cboItem.GetText() == '') {
                                        cboItem.Focus();
                                        msg = 'Please select Item!';
                                    } 
                                    
                                    if (msg != '') {
                                        toastr.warning(msg, 'Warning');
                                        toastr.options.closeButton = false;
                                        toastr.options.debug = false;
                                        toastr.options.newestOnTop = false;
                                        toastr.options.progressBar = false;
                                        toastr.options.preventDuplicates = true;
                                        toastr.options.onclick = null;
                                        e.processOnServer = false;
                                        return;
                                    }
                                    UpdateXUCL();
                                }" />
                            </dx:ASPxButton>
            
            </td>
            <td align="center">
            
                            &nbsp;</td>
        </tr>
    </table>
</div>
<div style="height:10px">
    <hr />
</div>
<div style="padding: 0px 5px 5px 5px; width: 100%;">
    <dxchartsui:WebChartControl ID="chartX" runat="server" ClientInstanceName="chartX"
        Height="200px" Width="1080px" AutoBindingSettingsEnabled="False"
        AutoLayoutSettingsEnabled="False" CrosshairEnabled="True">
        <DiagramSerializable>
            <cc1:XYDiagram>
            <AxisX VisibleInPanesSerializable="-1"></AxisX>
            <AxisY VisibleInPanesSerializable="-1"></AxisY>
            </cc1:XYDiagram>
            </DiagramSerializable>
        <seriesserializable>

            <cc1:Series ArgumentDataMember="Seq" Name="Warning" 
                        ValueDataMembersSerializable="Warning">
                <viewserializable>
                    <cc1:PointSeriesView>
                        <PointMarkerOptions size="4" kind="6"></PointMarkerOptions>
                    </cc1:PointSeriesView>
                </viewserializable>
            </cc1:Series>
        </seriesserializable>

        <seriestemplate>
            <viewserializable>
                <cc1:LineSeriesView>
                    <linemarkeroptions size="2"></linemarkeroptions>
                    <linestyle thickness="1" />
                </cc1:LineSeriesView>
            </viewserializable>
        </seriestemplate>  
        <legend alignmenthorizontal="Left" alignmentvertical="BottomOutside" 
            direction="LeftToRight"></legend> 
        <titles>
            <cc1:ChartTitle Font="Segoe UI, 12pt, style=Bold" Text="CONTROL X" />
        </titles>

    </dxchartsui:WebChartControl>


</div>
<div style="height:10px">&nbsp;</div>
<div style="padding: 0px 5px 5px 5px; width: 100%;">


    <dxchartsui:WebChartControl ID="chartR" runat="server" 
        CrosshairEnabled="True" Height="200px" Width="1080px" 
        ClientInstanceName="chartR">

        <DiagramSerializable>
            <cc1:XYDiagram>
                <AxisX visibleinpanesserializable="-1" title-font="Trebuchet MS, 8pt, style=Bold" title-text="Seq" title-visible="True"></AxisX>
                <AxisY visibleinpanesserializable="-1" title-font="Trebuchet MS, 8pt, style=Bold" title-text="" title-visible="True"></AxisY>
            </cc1:XYDiagram>
        </DiagramSerializable>  

        <legend alignmenthorizontal="Left" alignmentvertical="BottomOutside" 
            direction="LeftToRight"></legend>  

        <seriesserializable>

            <cc1:Series ArgumentDataMember="Seq" Name="Warning" 
                        ValueDataMembersSerializable="Warning">
                <viewserializable>
                    <cc1:PointSeriesView>
                        <PointMarkerOptions size="4" kind="6"></PointMarkerOptions>
                    </cc1:PointSeriesView>
                </viewserializable>
            </cc1:Series>
        </seriesserializable>

        <seriestemplate>
            <viewserializable>
                <cc1:LineSeriesView>
                    <linemarkeroptions size="2"></linemarkeroptions>
                    <linestyle thickness="1" />
                </cc1:LineSeriesView>
            </viewserializable>
        </seriestemplate> 

        <titles>
            <cc1:ChartTitle Font="Segoe UI, 12pt, style=Bold" Text="CONTROL R" />
        </titles>

    </dxchartsui:WebChartControl>


</div>

<div style="height:10px">&nbsp;<dx:ASPxGridViewExporter ID="GridExporter" 
        runat="server" GridViewID="grid">
    </dx:ASPxGridViewExporter>
    </div>
<div style="height:26px; " align="center">
    <dx:ASPxLabel ID="ASPxLabel33" runat="server" Text="SUMMARY" 
        Font-Names="Segoe UI" Font-Size="12pt" Font-Bold="True" 
        Font-Underline="True" ></dx:ASPxLabel>
</div>

<div style="padding: 0px 5px 5px 5px">

        <dx:ASPxGridView ID="gridSummary" runat="server" 
        AutoGenerateColumns="False" ClientInstanceName="gridSummary"
            EnableTheming="True" KeyFieldName="Seq" Theme="Office2010Black" 
            Width="100%" 
            Font-Names="Segoe UI" Font-Size="9pt">
            <Columns>
                <dx:GridViewDataTextColumn Caption="LINE" VisibleIndex="0" FieldName="LineID" 
                    Width="60px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="PART NO." VisibleIndex="1" 
                    FieldName="PartID" Width="130px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="PART NAME" VisibleIndex="2" 
                    FieldName="PartName" Width="200px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ITEM CHECK" VisibleIndex="3" 
                    FieldName="Item" Width="130px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="STANDARD" VisibleIndex="4" 
                    FieldName="Standard" Width="90px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="FREQUENCY" VisibleIndex="5" 
                    FieldName="Frequency">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="MEASURING INSTRUMENT" VisibleIndex="6" 
                    FieldName="MeasuringInstrument" Width="120px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="MACHINE NO." VisibleIndex="7" 
                    FieldName="MachineNo" Width="70px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CP" VisibleIndex="9" Width="60px">
                    <PropertiesTextEdit DisplayFormatString="#,0.00">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CPK" VisibleIndex="10" Width="60px">
                    <PropertiesTextEdit DisplayFormatString="#,0.00">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="n Data" FieldName="nData" VisibleIndex="8" 
                    Width="60px">
                </dx:GridViewDataTextColumn>
            </Columns>
        <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" 
                AllowFocusedRow="True" AllowDragDrop="False" AllowSort="False" />
        <SettingsEditing Mode="Batch" >
            <BatchEditSettings ShowConfirmOnLosingChanges="False" />
            </SettingsEditing>
        <SettingsPager AlwaysShowPager="true" Mode="ShowAllRecords" PageSize="30">
        </SettingsPager>
        <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="50" 
            VerticalScrollBarMode="Auto" ShowStatusBar="Hidden" />
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="False" Width="320"/>
        </SettingsPopup>
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" 
                AllowInsert="False" />
        <Styles EditFormColumnCaption-Paddings-PaddingLeft="10px" 
            EditFormColumnCaption-Paddings-PaddingRight="10px" 
            Header-Paddings-Padding="5px">
            <Header HorizontalAlign="Center" Wrap="True">
                <Paddings Padding="2px" />
            </Header>
            <EditFormColumnCaption Font-Names="Segoe UI" Font-Size="9pt">
                <Paddings PaddingBottom="5px" PaddingLeft="15px" PaddingRight="15px" 
                    PaddingTop="5px" />
            </EditFormColumnCaption>
            <CommandColumnItem ForeColor="SteelBlue">
            </CommandColumnItem>
        </Styles>        
    </dx:ASPxGridView>

</div>
<div style="height:6px">&nbsp;</div>
<div style="height:26px; " align="center">
    <dx:ASPxLabel ID="ASPxLabel34" runat="server" Text="COMMENT" 
        Font-Names="Segoe UI" Font-Size="12pt" Font-Bold="True" 
        Font-Underline="True" ></dx:ASPxLabel>
</div>

<div style="padding: 0px 5px 5px 5px">

        <dx:ASPxGridView ID="gridAction" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridAction"
            EnableTheming="True" KeyFieldName="Date;LineID;SubLineID;ProcessID;PartID;XRCode" Theme="Office2010Black" Width="100%"
            Font-Names="Segoe UI" Font-Size="9pt" 
            OnRowInserting="gridAction_RowInserting"
            OnRowUpdating="gridAction_RowUpdating"
            OnRowDeleting="gridAction_RowDeleting"
            OnStartRowEditing="gridAction_StartRowEditing"
            OnRowValidating="gridAction_RowValidating"
            >
            <ClientSideEvents EndCallback="ActionOnEndCallback" />
            <Columns>
                <dx:GridViewCommandColumn FixedStyle="Left"
                    VisibleIndex="0" ShowEditButton="true" ShowDeleteButton="true" 
                    ShowNewButtonInHeader="true">
                    <HeaderStyle Paddings-PaddingLeft="3px" HorizontalAlign="Center" 
                        VerticalAlign="Middle" >
                        <Paddings PaddingLeft="3px"></Paddings>
                    </HeaderStyle>
                </dx:GridViewCommandColumn>

                <dx:GridViewDataTextColumn FieldName="PartID"
                    VisibleIndex="9" Settings-AutoFilterCondition="Contains" Visible="False" 
                    >
<Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn FieldName="XRCode"
                    VisibleIndex="10" Settings-AutoFilterCondition="Contains" Visible="False" 
                    >
<Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PIC" 
                    VisibleIndex="11" Caption="PIC">
                    <PropertiesTextEdit MaxLength="25">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Action" VisibleIndex="12" Width="350px">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Result" VisibleIndex="13" Width="350px">
                    <PropertiesTextEdit MaxLength="50">
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataDateColumn FieldName="Date" VisibleIndex="2" Name="Date">
                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy" EditFormat="Custom" 
                        EditFormatString="dd MMM yyyy" MaxDate="2050-12-01" MinDate="2018-12-01" 
                        Width="90px">
                    </PropertiesDateEdit>
                    <Settings AutoFilterCondition="Contains" />
                    <EditFormSettings VisibleIndex="1" />
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataComboBoxColumn FieldName="LineID" Visible="False" 
                    VisibleIndex="4">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn FieldName="SubLineID" Visible="False" 
                    VisibleIndex="6">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn FieldName="ProcessID" Visible="False" 
                    VisibleIndex="8">
                </dx:GridViewDataComboBoxColumn>
            </Columns>

            <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="Control" />
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm"/>
            <Settings VerticalScrollBarMode="Auto" 
                VerticalScrollableHeight="250" HorizontalScrollBarMode="Auto" />
            <SettingsText ConfirmDelete="Are you sure want to delete ?"></SettingsText>
            <SettingsPopup>
                <EditForm Modal="False" Width="320" HorizontalAlign="WindowCenter" />
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

<div style="padding: 0px 5px 5px 5px">
    <table style="width: 100%">
        <tr >
            <td style="padding:5px 0px 5px 0px">
                
                    <dx:ASPxTextBox ID="txtProcessID" runat="server" BackColor="WhiteSmoke" 
                        ClientInstanceName="txtProcessID" EnableTheming="True" Font-Names="Segoe UI" 
                        Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                        Theme="Office2010Black" Width="100px" ReadOnly="True" TabIndex="3" 
                        ClientVisible="False">
                    </dx:ASPxTextBox>
            
            </td>
            <td style="padding:5px 0px 5px 0px">

                    <dx:ASPxTextBox ID="txtXRCode" runat="server" BackColor="WhiteSmoke" 
                        ClientInstanceName="txtXRCode" EnableTheming="True" Font-Names="Segoe UI" 
                        Font-Size="9pt" Height="25px" HorizontalAlign="Left" 
                        Theme="Office2010Black" Width="100px" ReadOnly="True" TabIndex="3" 
                        ClientVisible="False">
                    </dx:ASPxTextBox>
            
            </td>
            <td style="padding:5px 0px 5px 0px">
                
            </td>
            <td style="padding:5px 0px 5px 0px">
                &nbsp;</td>
            <td style="width:95px;">                
                <dx:ASPxCallback ID="cbkUpdate" runat="server" ClientInstanceName="cbkUpdate">
                    <ClientSideEvents EndCallback="function(s, e) {
                        grid.PerformCallback('load|' + dtFrom.GetText() + '|' + dtTo.GetText() + '|' + cboLineID.GetValue() + '|' + cboSubLine.GetText() + '|' + cboMachine.GetValue() + '|' + cboPartID.GetValue() + '|' + cboItem.GetValue() + '|' + txtProcess.GetText() + '|' + txtXRCode.GetText() );
                        if (s.cp_type == 'Success') {
                            toastr.success(s.cp_message, 'Success');
                            toastr.options.closeButton = false;
                            toastr.options.debug = false;
                            toastr.options.newestOnTop = false;
                            toastr.options.progressBar = false;
                            toastr.options.preventDuplicates = true;
                            toastr.options.onclick = null;	
                        } else if (s.cp_type == 'ErrorMsg') {
                            toastr.error(s.cp_message, 'Error');
                            toastr.options.closeButton = false;
                            toastr.options.debug = false;
                            toastr.options.newestOnTop = false;
                            toastr.options.progressBar = false;
                            toastr.options.preventDuplicates = true;
                            toastr.options.onclick = null;
                        }
                    }" />
                </dx:ASPxCallback>
                
            </td>
            <td style="width: 95px;">
<dx:ASPxButton ID="btnClear" runat="server" Text="Clear" UseSubmitBehavior="False"
                    Font-Names="Segoe UI" Font-Size="9pt" Width="90px" Height="25px" AutoPostBack="False"
                    ClientInstanceName="btnClear" Theme="Default" TabIndex="14">
                    <ClientSideEvents Click="function(s, e) {
	cboLineID.SetText('');
	cboSubLine.SetText('');
    cboMachine.SetText('');
    txtProcess.SetText('');
    txtProcessID.SetText('');
	cboPartID.SetText('');
	txtPartName.SetText('');
    cboItem.SetText('');
    GridLoad();
}" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>                
            </td>
            <td style="width:95px; padding:0px 0px 0px 0px;">
                <dx:ASPxButton ID="btnExcel" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnExcel" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Excel" Theme="Default" UseSubmitBehavior="False" 
                    Width="90px" TabIndex="16">
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
            </td>
        </tr>
        </table>
        <table>
            <tr>
                <td>
<dx:ASPxPopupControl ID="pcUpload" runat="server" CloseAction="CloseButton"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcUpload"
        HeaderText="Upload Attachment" AllowDragging="True" 
        PopupAnimationType="None" Height="130px" 
        Theme="Office2010Black" Width="360px" TabIndex="12">
        <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); }" />
        <ModalBackgroundStyle BackColor="White" CssClass="&quot;noBackground&quot;">
        </ModalBackgroundStyle>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK" Height="100px">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            
                            <table style="width: 100%; margin-top: 15px;">
                                <tr>
                                    <td style="width: 100px">
                                        &nbsp;
                                        <dx:ASPxLabel ID="lblqeleader3" runat="server" Font-Names="Segoe UI" 
                                            Font-Size="9pt" Text="File Name" Width="60px">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="width: 10px">
                                        &nbsp;
                                    </td>
                                    <td align="left" colspan="4">
                                        

                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td colspan="2">

                                    </td>
                                    <td colspan="2">
                                        &nbsp;</td>
                                </tr>
                                <tr style="height: 30px">
                                    <td align="center" colspan="5">
                                    
                                    </td>
                                </tr>
                                <tr style="height: 40px">
                                    <td align="right" colspan="3">
                                        &nbsp;</td>
                                    <td colspan="2" style="padding-left: 5px">
                                        &nbsp;</td>
                                    <td>
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
                </td>
            </tr>
        </table>
</div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT Rtrim(ProcessID) as ProcessID, ProcessName FROM Process">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server"
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
        SelectCommand="SELECT DISTINCT(RTRIM(MeasuringInstrument)) as MeasuringInstrument FROM QCSItem">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dsLine" runat="server"
    ConnectionString="<%$ ConnectionStrings:ApplicationServices %>" 
    SelectCommand="SELECT RTRIM(LineID) as LineID, RTRIM(LineName) LineName FROM [Line]">
    </asp:SqlDataSource>

</asp:Content>
