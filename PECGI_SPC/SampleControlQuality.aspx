<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SampleControlQuality.aspx.vb" Inherits="PECGI_SPC.SampleControlQuality" %>
<%@ Register Assembly="DevExpress.XtraCharts.v20.2.Web, Version=20.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraCharts.v20.2, Version=20.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register assembly="DevExpress.Web.v20.2, Version=20.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <style type="text/css">
        .header {
            border: 1px solid silver; 
            background-color: #F0F0F0;
            text-align: center;
        }
        .body {
            border: 1px solid silver; 
        }
        .auto-style1 {
            height: 12px;
        }
    </style>
    <script type="text/javascript" >
        var rowIndex, columnIndex;
        function OnInit(s, e) {
            var d = new Date(2022, 8, 3);
            dtDate.SetDate(d);  
        }

        function isNumeric(n) {
            if (n == null || n == '') {
                return true;
            } else {
                return !isNaN(parseFloat(n)) && isFinite(n);
            }
        }

        function ValidateSave(s, e) {
            var rows = grid.GetVisibleRowsOnPage();
            var startIndex = 0;
            var i;
            var errmsg = '';      
            
            for (i = startIndex; i < rows - 1; i++) {
                if (isNumeric(grid.batchEditApi.GetCellValue(i, 'Value')) == false) {
                    errmsg = 'Please input valid numeric!';
                    grid.batchEditApi.StartEdit(i, 1);
                    break;
                }
            }                  
            if (errmsg != '') {
                e.cancel = true; 
                toastr.warning(errmsg, 'Warning');
                toastr.options.closeButton = false;
                toastr.options.debug = false;
                toastr.options.newestOnTop = false;
                toastr.options.progressBar = false;
                toastr.options.preventDuplicates = true;
                toastr.options.onclick = null;
                e.processOnServer = false;
                return;
            }
            grid.UpdateEdit();
        }

        function ClearGrid(s, e) {
            grid.CancelEdit();
	        grid.PerformCallback('clear');
        }

        function cboFactoryChanged(s, e) { 
            cboType.SetEnabled(false);
            cboType.PerformCallback(cboFactory.GetValue());
        }

        function cboTypeChanged(s, e) {
            cboLine.SetEnabled(false);   
            cboLine.PerformCallback(cboFactory.GetValue()  + '|' + cboType.GetValue());
        }

        function cboLineChanged(s, e) {    
            cboItemCheck.SetEnabled(false);
            cboItemCheck.PerformCallback(cboFactory.GetValue() + '|' + cboType.GetValue() + '|' + cboLine.GetValue());

            cboShift.PerformCallback(cboFactory.GetValue() + '|' + cboType.GetValue() + '|' + cboLine.GetValue() + '|' + cboItemCheck.GetValue());            
        }

        function cboItemCheckChanged(s, e) {
            cboShift.PerformCallback(cboFactory.GetValue() + '|' + cboType.GetValue() + '|' + cboLine.GetValue() + '|' + cboItemCheck.GetValue());            
            cboSeq.PerformCallback(cboFactory.GetValue() + '|' + cboType.GetValue() + '|' + cboLine.GetValue() + '|' + cboItemCheck.GetValue() + '|' + cboShift.GetValue() + '|' + dtDate.GetText());
        }

        function cboShiftChanged(s, e) {            
            
        }

        function OnBatchEditStartEditing(s, e) {
            if(cboSeq.GetValue() == '') {
                e.cancel = true;                
                toastr.warning('Please select Sequence!', 'Warning');
                toastr.options.closeButton = false;
                toastr.options.debug = false;
                toastr.options.newestOnTop = false;
                toastr.options.progressBar = false;
                toastr.options.preventDuplicates = true;
                toastr.options.onclick = null;
            }
            currentColumnName = e.focusedColumn.fieldName;            
            currentEditableVisibleIndex = e.visibleIndex;          
        }      

        function OnBatchEditEndEditing(s, e) {            
            rowIndex = null;
            columnIndex = null;            
        }

        function ChartREndCallBack(s, e) {
            var i = s.cpShow;
            var x = document.getElementById("chartRdiv");
            if (i=='1') {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
            
        }

        function OpenWindow(s, e) {
            var selectedDate = dtDate.GetText();
            window.open('ProdSampleInput.aspx?FactoryCode=' + cboFactory.GetValue() + '&ItemTypeCode=' + cboType.GetValue() + '&Line=' + cboLine.GetValue() + '&ItemCheckCode=' + cboItemCheck.GetValue() + '&Shift=' + cboShift.GetValue() + '&ProdDate=' + dtDate.GetText() + '&Sequence=' + cboSeq.GetValue(), "NewWindow");
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

            lblMKUser.SetText(s.cpMKUser);
            lblMKDate.SetText(s.cpMKDate);
            lblQCUser.SetText(s.cpQCUser);
            lblQCDate.SetText(s.cpQCDate);

            lblUSL.SetText(s.cpUSL);
            lblLSL.SetText(s.cpLSL);
            lblUCL.SetText(s.cpUCL);
            lblLCL.SetText(s.cpLCL);
            lblMin.SetText(s.cpMin);
            lblMax.SetText(s.cpMax);
            lblAve.SetText(s.cpAve);
            lblR.SetText(s.cpR);
            lblC.SetText(s.cpC);            
            txtRemarks.SetText(s.cpRemarks);
            txtSubLotNo.SetText(s.cpSubLotNo);
            
            if (s.cpNG == '2') {
                lblNG.SetText('NG');
                document.getElementById('NG').style.backgroundColor = 'Red';
            } else if (s.cpNG == '1') {
                lblNG.SetText('NG');
                document.getElementById('NG').style.backgroundColor = 'Pink';
            } else if (s.cpNG == '0') {
                lblNG.SetText('OK');
                document.getElementById('NG').style.backgroundColor = 'Green';
            } else {
                lblNG.SetText('');
                document.getElementById('NG').style.backgroundColor = 'White';
            }
            if (s.cpC == 'C') {
                document.getElementById('C').style.backgroundColor = 'Orange';
            } else {
                document.getElementById('C').style.backgroundColor = 'White';
            }      

            if(s.cpMin != '') {
                if (s.cpMin < s.cpLSL) {
                    document.getElementById('Min').style.backgroundColor = 'Red';
                } else if (s.cpMin < s.cpLCL) {
                    document.getElementById('Min').style.backgroundColor = 'Pink';
                } else {
                    document.getElementById('Min').style.backgroundColor = 'White';
                }
            } else {
                document.getElementById('Min').style.backgroundColor = 'White';
            }
            if (s.cpMax > s.cpUSL) {
                document.getElementById('Max').style.backgroundColor = 'Red';
            } else if (s.cpMax > s.cpUCL) {
                document.getElementById('Max').style.backgroundColor = 'Pink';
            } else {
                document.getElementById('Max').style.backgroundColor = 'White';
            }
            if (s.cpRefresh == '1') {
                gridX.PerformCallback(cboFactory.GetValue() + '|' + cboType.GetValue() + '|' + cboLine.GetValue() + '|' + cboItemCheck.GetValue() + '|' + dtDate.GetText());
                chartX.PerformCallback(cboFactory.GetValue() + '|' + cboType.GetValue() + '|' + cboLine.GetValue() + '|' + cboItemCheck.GetValue() + '|' + dtDate.GetText());
                chartR.PerformCallback(cboFactory.GetValue() + '|' + cboType.GetValue() + '|' + cboLine.GetValue() + '|' + cboItemCheck.GetValue() + '|' + dtDate.GetText());
            }            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div style="padding: 0px 5px 5px 5px">
        <table class="auto-style3" style="width: 100%">
        <tr >
            <td style="width:60px; padding:5px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Factory" Font-Names="Segoe UI" 
                    Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:130px; padding:5px 0px 0px 0px">
                <dx:ASPxComboBox ID="cboFactory" runat="server" Theme="Office2010Black" TextField="FactoryName"
                    ClientInstanceName="cboFactory" ValueField="FactoryCode" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    IncrementalFilteringMode="Contains" 
                    Width="100px" TabIndex="6" EnableCallbackMode="True">
                    <ClientSideEvents SelectedIndexChanged="cboFactoryChanged" />

                    <ItemStyle Height="10px" Paddings-Padding="4px" >
<Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style=" width:100px; padding:5px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Machine Process" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width:130px; padding:5px 0px 0px 0px">
                <dx:ASPxComboBox ID="cboLine" runat="server" Theme="Office2010Black" TextField="LineName"
                    ClientInstanceName="cboLine" ValueField="LineCode" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    Width="190px" TabIndex="4" NullValueItemDisplayText="{1}">
                    <ClientSideEvents SelectedIndexChanged="cboLineChanged" EndCallback="function(s, e) {cboLine.SetEnabled(true);}"/>
                    <ItemStyle Height="10px" Paddings-Padding="4px">
<Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style=" padding: 5px 0px 0px 10px; width:50px">
                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Date" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:10px">
                &nbsp;
                </td>
            <td style="padding: 5px 0px 0px 0px; width: 180px;" colspan="3">                
                                <dx:ASPxDateEdit ID="dtDate" runat="server" Theme="Office2010Black" 
                    Width="100px"
                        ClientInstanceName="dtDate" EditFormatString="dd MMM yyyy" DisplayFormatString="dd MMM yyyy"
                        Font-Names="Segoe UI" Font-Size="9pt" Height="25px" TabIndex="2" 
                    EditFormat="Custom">
                        <CalendarProperties ShowWeekNumbers="False">
                            <HeaderStyle Font-Size="12pt" Paddings-Padding="5px" ><Paddings Padding="5px"></Paddings>
                            </HeaderStyle>
                            <DayStyle Font-Size="9pt" Paddings-Padding="5px" ><Paddings Padding="5px"></Paddings>
                            </DayStyle>
                            <WeekNumberStyle Font-Size="9pt" Paddings-Padding="5px"><Paddings Padding="5px"></Paddings>
                            </WeekNumberStyle>
                            <FooterStyle Font-Size="9pt" Paddings-Padding="10px" ><Paddings Padding="10px"></Paddings>
                            </FooterStyle>
                            <ButtonStyle Font-Size="9pt" Paddings-Padding="10px"><Paddings Padding="10px"></Paddings>
                            </ButtonStyle>
                        </CalendarProperties>                        
                        <ButtonStyle Width="5px" Paddings-Padding="4px" >
<Paddings Padding="4px"></Paddings>
                        </ButtonStyle>
                    </dx:ASPxDateEdit>

                
                </td>            
           
                <td style=" width:110px">                
                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Show Verified Only" 
                        Font-Names="Segoe UI" Font-Size="9pt" Width="109px">
                    </dx:ASPxLabel>                
                </td>            
            <td colspan="2" style="width: 120px">                
                
                <dx:ASPxComboBox ID="cboShow" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboShow" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    Width="58px" TabIndex="9" SelectedIndex="0">
                    <Items>                        
                        <dx:ListEditItem Text="No" Value="0" Selected="true"/>
                        <dx:ListEditItem Text="Yes" Value="1" />
                    </Items>
                    <ItemStyle Height="10px" Paddings-Padding="4px">
<Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
                
            </td>                        
        </tr>

        <tr>
            <td style=" width:60px; padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Type" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:130px; padding:3px 0px 0px 0px">
                
                <dx:ASPxComboBox ID="cboType" runat="server" Theme="Office2010Black" TextField="Description"
                    ClientInstanceName="cboType" ValueField="ItemTypeCode" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    Width="100px" TabIndex="6" EnableCallbackMode="True">                    
                    <ClientSideEvents SelectedIndexChanged="cboTypeChanged" EndCallback="function(s, e) {cboType.SetEnabled(true);}"/>
                    <ItemStyle Height="10px" Paddings-Padding="4px" >
<Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
            </td>
            <td style=" padding:3px 0px 0px 0px">
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Item Check" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style="width:200px; padding:3px 0px 0px 0px">
                
                    
                <dx:ASPxComboBox ID="cboItemCheck" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboItemCheck" ValueField="ItemCheckCode" TextField="ItemCheck" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    Width="190px" TabIndex="5" >
                    <ClientSideEvents EndCallback="function(s, e) {
                            cboItemCheck.SetEnabled(true);                            
                       }"
                        SelectedIndexChanged="cboItemCheckChanged"/>

                    <ItemStyle Height="10px" Paddings-Padding="4px">
<Paddings Padding="4px"></Paddings>
                    </ItemStyle>
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
                
                    
            </td>
            <td style=" padding: 3px 0px 0px 10px; ">
                <dx:ASPxLabel ID="lblqeleader0" runat="server" Text="Shift" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>
            <td style=" width:10px">
                &nbsp;</td>
            <td style="width:100px; padding:3px 0px 0px 0px">
                
                <dx:ASPxComboBox ID="cboShift" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboShift" ValueField="ShiftCode" TextField="ShiftName" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    Width="100px" TabIndex="3">
                    <ClientSideEvents SelectedIndexChanged="cboShiftChanged" 
                        EndCallback="function(s, e) {cboShift.SetEnabled(true);}"
                        />
                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>                
            </td>
            <td style="width:30px; padding:3px 0px 0px 5px">
                <dx:ASPxLabel ID="lblqeleader2" runat="server" Text="Seq" 
                    Font-Names="Segoe UI" Font-Size="9pt">
                </dx:ASPxLabel>
            </td>            
            <td style="width:60px; padding:3px 0px 0px 0px">
                
                <dx:ASPxComboBox ID="cboSeq" runat="server" Theme="Office2010Black" 
                    ClientInstanceName="cboSeq" ValueField="SequenceNo" TextField="SequenceNo" Font-Names="Segoe UI" 
                    Font-Size="9pt" Height="25px" 
                    Width="60px" TabIndex="3">
                    <ClientSideEvents SelectedIndexChanged="ClearGrid" EndCallback="function(s, e) {cboSeq.SetEnabled(true);}"/>

                    <ButtonStyle Paddings-Padding="4px" Width="5px">
<Paddings Padding="4px"></Paddings>
                    </ButtonStyle>
                </dx:ASPxComboBox>
                
            </td>            
            <td>
                
                <dx:ASPxButton ID="btnOpen" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnOpen" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Prod Sample Input" Theme="Office2010Silver" UseSubmitBehavior="False" 
                    Width="90px" TabIndex="10">
                    <ClientSideEvents Click="OpenWindow" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
                
            </td>            
            <td align="left" style="width: 60px">
                
                <dx:ASPxButton ID="btnSearch" runat="server" AutoPostBack="False" 
                    ClientInstanceName="btnSearch" Font-Names="Segoe UI" Font-Size="9pt" 
                    Height="25px" Text="Browse" Theme="Office2010Silver" UseSubmitBehavior="False" 
                    Width="90px" TabIndex="10">
                    <ClientSideEvents Click="function(s, e) {
                        var errmsg = '';
                        if(cboFactory.GetText() == '') {
                            cboFactory.Focus();
                            errmsg = 'Please select Factory!';                                                                
	                    } else if(cboType.GetText() == '') {
                            cboType.Focus();
                            errmsg = 'Please select Type!';
	                    } else if(cboLine.GetText() == '') {
                            cboLine.Focus();
                            errmsg = 'Please select Machine Process!';
	                    } else if(cboItemCheck.GetText() == '') {
                            cboItemCheck.Focus();
                            errmsg = 'Please select Item Check!';
	                    } else if(cboShift.GetText() == '') {
                            cboShift.Focus();
                            errmsg = 'Please select Shift!';
	                    } else if(cboSeq.GetText() == '') {
                            cboSeq.Focus();
                            errmsg = 'Please select Sequence!';
	                    }

                        if(errmsg != '') {
                            toastr.warning(errmsg, 'Warning');
                            toastr.options.closeButton = false;
                            toastr.options.debug = false;
                            toastr.options.newestOnTop = false;
                            toastr.options.progressBar = false;
                            toastr.options.preventDuplicates = true;
                            toastr.options.onclick = null;		
		                    e.processOnServer = false;
		                    return;
                        }
                        grid.CancelEdit();
 	                    grid.PerformCallback('load' + '|' + cboFactory.GetValue() + '|' + cboType.GetValue() + '|' + cboLine.GetValue() + '|' + cboItemCheck.GetValue() + '|' + dtDate.GetText() + '|' + cboShift.GetValue() + '|' + cboSeq.GetValue() + '|' + cboShow.GetValue());
                    }" />
                    <Paddings Padding="2px" />
                </dx:ASPxButton>
                
            </td>   
            <td align="left">
                &nbsp;</td>   
                     
        </tr>
    </table>
    </div>
<div style="height:10px">
    <dx:ASPxHiddenField ID="hfRevNo" runat="server" ClientInstanceName="hfRevNo">
    </dx:ASPxHiddenField>
</div>
<hr style="border-color:darkgray; " class="auto-style1"/>


<div style="padding: 10px 5px 5px 5px">
<dx:ASPxGridView ID="gridX" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridX"
                                EnableTheming="True" KeyFieldName="Des" Theme="Office2010Black"            
                                Width="100%" 
                                Font-Names="Segoe UI" Font-Size="9pt">


                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>


                    <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />


<SettingsPopup>
<FilterControl AutoUpdatePosition="False"></FilterControl>
</SettingsPopup>
                            <Columns>
                                <dx:GridViewBandColumn Caption="DATE" VisibleIndex="0">
                                    <Columns>
                                        <dx:GridViewBandColumn Caption="SHIFT" VisibleIndex="0">
                                            <Columns>
                                                <dx:GridViewBandColumn Caption="TIME" VisibleIndex="0">
                                                </dx:GridViewBandColumn>
                                            </Columns>
                                        </dx:GridViewBandColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                    </Columns>
                            <Styles>
                                <Header HorizontalAlign="Center">
                                </Header>
                            </Styles>


                            </dx:ASPxGridView>    
</div>

</asp:Content>
