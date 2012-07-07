<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountIdentifier.aspx.cs" Inherits="Dialogs_AccountIdentifier" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Set Account Identifier</title>
    <style type="text/css">
        #AppName
        {
            width: 250px;
        }
        #UrlAccountIdentifier
        {
            margin-left: 0px;
            width: 320px;
        }
        .style11
        {
            width: 16px;
        }
    </style>
    <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
 <script  language="javascript" type="text/javascript" src="../../scripts/dialogs_1.26.min.js"></script>
  <script type="text/javascript">
      function close() {
          var UrlAccountIdentifier = document.getElementById("UrlAccountIdentifier");
          parent.window.setUrlAccountIdentifierCallback(UrlAccountIdentifier.value);
      }    
        </script>
</head>
<body style="height:201px; width:374px" onunload="close();">
    <form id="form1" runat="server">
    
        <div style="height:201px; width:372px">
        <table style="width: 368px; font-family: verdana; font-size: 12px; height: 197px;"><tr>
        <td class="style11"></td><td>
                                <asp:Label ID="Label2" runat="server" 
                Text="Set an Account Identifier for Your Web Apps URL" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" 
                        Width="315px"></asp:Label>
                            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
        <td class="style11"></td><td>Examples:
                                mydomain, mycompany, myproduct</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
        <td class="style11"></td><td colspan="2">This needs to be unique within ViziApps 
            accounts</td>
        </tr>
        <tr>
        <td class="style11"></td><td colspan="2">
                <input name="UrlAccountIdentifier" type="text" id="UrlAccountIdentifier" size="40" runat="server" /></td>
        </tr>
        <tr>
        <td class="style11"></td><td>
                    <asp:Button ID="SaveUrlAccountIdentifier" runat="server" Text="Save" 
                        Width="59px" onclick="Save_Click" Height="26px" 
                    />
                </td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
        <td class="style11"></td><td colspan="2">
                                <asp:Label ID="Message" runat="server" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#660033" Font-Bold="True" 
                        Width="317px" Height="45px"></asp:Label>
                </td>
        </tr></table>
    
    </div>
    </form>
</body>
</html>
