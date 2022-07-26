<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PreviewAttachment.aspx.vb" Inherits="PECGI_SPC.PreviewAttachment" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function Close() {
            if (window.opener != null && !window.opener.closed) {
                try {
                    window.opener.GridLoad();
                    alert('load');
                }
                catch (err) {
                    alert(err.message);
                }
            }
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 5px 5px 5px 5px">
<table style="width:100%;">
    <tr>
        <td style="width:40%" align="left">
            &nbsp;</td>
        <td align="left">
            <dx:ASPxTextBox ID="txtResultID" runat="server" 
                ClientInstanceName="txtResultID" Width="90px" ClientVisible="False">
            </dx:ASPxTextBox>
        </td>
        <td align="left">
            <dx:ASPxTextBox ID="txtItemID" runat="server" ClientInstanceName="txtItemID" 
                Width="90px" ClientVisible="False">
            </dx:ASPxTextBox>
        </td>
        <td align="left">
            <dx:ASPxTextBox ID="txtName" runat="server" ClientInstanceName="txtName" 
                Width="90px" ClientVisible="False">
            </dx:ASPxTextBox>
        </td>
        <td align="left">
            <dx:ASPxCallback ID="cbkClear" runat="server" ClientInstanceName="cbkClear">
                <clientsideevents endcallback="function(s, e) {
                    if (window.opener != null && !window.opener.closed) {
                            try {
                                window.opener.AfterDelete();
                            }
                            catch (err) {
                                alert(err.message);
                            }
                        }
                        window.close();	
                    }" />
            </dx:ASPxCallback>
        </td>
    </tr>
    
    <tr>
        <td colspan="5" align="center">
        
            <dx:ASPxLabel ID="lblErr" runat="server" ClientInstanceName="lblErr" 
                Font-Bold="True" Font-Size="12pt">
            </dx:ASPxLabel>
        
        </td>
    </tr>
    <tr>
        <td colspan="5" align="center" valign="middle">               
            <dx:ASPxBinaryImage ID="img2" runat="server" ClientInstanceName="img2" 
                ImageAlign="Middle">
            </dx:ASPxBinaryImage>
            <dx:ASPxImage ID="imgPreview" runat="server" ClientInstanceName="imgPreview" 
                ImageAlign="Middle">
            </dx:ASPxImage>
        </td>        
    </tr>
    </table>        
        </div> 
    <div style="padding: 5px 5px 5px 5px">
        
    </div>
    </form>
</body>
</html>
