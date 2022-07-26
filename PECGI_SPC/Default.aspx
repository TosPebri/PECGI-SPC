<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="PECGI_SPC._Default" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>Statistics Quality Control</title>
        <link href="~/Styles/SiteLogin.css" type="text/css" rel="Stylesheet" runat="server" />
        <link href="~/Styles/images/icon.ico" rel="SHORTCUT ICON" type="image/icon" />
        <style type="text/css">
            .style1
            {
                width: 4px;
            }
        </style>
    </head>

    <body onload="document.getElementById('txtusername').focus()" >
        
        <div class="form-area" style="background-color: #333333; width: 420px; height: 400px;">
            <form id="form1" runat="server">
                <table align="center" width="100%" border="0">
                    <tr>                        
                        <td align="center" style="width: 900px" >
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Statistics Quality Control" 
                                Font-Names="Candara" ForeColor="#99CC00" Font-Bold="True" 
                                Font-Size="30px">
                            </dx:ASPxLabel>    
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" valign="bottom">
                            <table align="center" border="0" class="content-box bg-default" style="background-color: #C0C0C0;">                                                                
                                <tr>
                                    <td>
                                        <%--<img alt="PGM" src="Styles/images/logo2.png" style="margin-bottom: 20px" />--%>
                                    </td>
                                </tr>
                                           
                                <tr>
                                    <td align="center" style="width:350px">
                                        <table id="tblLogin" border="0">
                                            <tr>
                                                <td align="left">
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="USER ID" 
                                                        Font-Names="Segoe UI" Font-Bold="true"
                                                        ForeColor="Black" Font-Size="12px">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr style="height:35px">
                                                <td align="left" colspan="3">
                                                    <dx:ASPxTextBox ID="txtusername" runat="server" Border-BorderStyle="None"
                                                        Width="250px" Height="30px" Font-Names="Segoe UI" Font-Size="14px" 
                                                        ClientInstanceName="txtusername" >
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left">
                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="PASSWORD" 
                                                        Font-Names="Segoe UI" Font-Bold="true"
                                                        ForeColor="Black" Font-Size="12px">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td align="center">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>                                        

                                            <tr style="height:35px">
                                                <td align="left" colspan="3">
                                                    <dx:ASPxTextBox ID="txtpassword" runat="server" Password="True" Border-BorderStyle="None"
                                                        Width="250px" Height="30px" Font-Names="Segoe UI" Font-Size="14px">
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>                                        
                                            <tr>
                                                <td align="right" class="errMsg" colspan="3">&nbsp;                                                                                                                              
                                                    <dx:ASPxLabel ID="lblInfo" runat="server" Text="" Font-Names="Segoe UI" 
                                                        ForeColor="Red" CssClass="errMsg" Font-Bold="True">
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="errMsg" colspan="3">
                                                    <dx:ASPxHyperLink ID="linkForgot" runat="server" 
                                                        ClientInstanceName="linkForgot" ClientVisible="False" 
                                                        Text="Forgot password?" >
                                                        <ClientSideEvents Click="function (s,e) {{location.href = 'ChangePassword.aspx';}}" />
                                                    </dx:ASPxHyperLink>
                                                </td>
                                            </tr>
                                            <tr> 
                                                <td colspan="3" align="center">
                                                    <dx:ASPxButton ID="btnLogin" runat="server" Text="LOGIN"
                                                        Width="100px" Height="30px" Font-Names="Segoe UI" Font-Size="10pt"
                                                        CssFilePath="~/css/bootstrap-theme.css"
                                                        CssClass="btn-warning">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>                                        
                                            <tr>
                                                <td align="right" colspan="3" style="font-size:10pt; font-family:Tahoma">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="3">
                                                    <dx:ASPxButton ID="btnVersion" runat="server" Text="Version" 
                                                        ClientInstanceName="btnVersion" RenderMode="Link" ForeColor="Black"
                                                        UseSubmitBehavior="False" AutoPostBack="False">
                                                        <ClientSideEvents Click="function(s, e) {           			                  	
			                                                var w = 500;
                                                            var h = 500;
                                                            var left = (screen.width / 2) - (w / 2);
                                                            var top = (screen.height / 2) - (h / 2);
                                                            var winseting = 'height=' + h + ',width=' + w + ',left=' + left + ',top=' + top;
                                                            var arg = window.open('PopUpVersion.aspx', 'ModalPopUp', winseting,scrollbars=true);
		    	                                            }"/>           
                                                    </dx:ASPxButton>                                                                                                
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>                            
                            </table>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" class="version" CssClass="version" 
                                ForeColor="White" Font-Size="10pt"
                                Text="© 2018 - Developed by PT. TOS Information Systems Indonesia">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>  
            </form>
        </div>
        <%-- <div class="footer-area">
            <table style="width:100%">
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td align="center" style="height: 40px">
                            &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>--%>
    </body>
</html>