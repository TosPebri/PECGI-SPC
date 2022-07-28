<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PreviewPDF.aspx.vb" Inherits="PECGI_SPC.PreviewPDF" %>
<%@ Register assembly="DevExpress.Web.v20.2, Version=20.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>





<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>Preview PDF</title>
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
        <td align="center">
            <dx:ASPxLabel ID="lblErr" runat="server" ClientInstanceName="lblErr" 
                Font-Bold="True" Font-Size="12pt">
            </dx:ASPxLabel>
        </td>
    </tr>   
    </table>        
        </div> 
    <div style="padding: 5px 5px 5px 5px">
        
    </div>
    </form>
</body>
</html>
