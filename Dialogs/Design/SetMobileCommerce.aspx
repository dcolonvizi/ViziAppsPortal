<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetMobileCommerce.aspx.cs" Inherits="Dialogs_SetMobileCommerce" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Set ViziApps Mobile Commerce</title>
    <style type="text/css">
        .style1
        {
            width: 56px;
            height: 36px;
        }
        .style3
        {
            height: 36px;
        }
        .style4
        {
            width: 423px;
            height: 36px;
        }
        .message
        {}
        .style5
        {
            width: 116px;
        }
        .style6
        {
            width: 56px;
        }
    </style>
</head>
<body>
     <form id="form1" runat="server">
     <telerik:RadScriptManager ID="RadScriptManagerEditAppProperties" runat="server">
                </telerik:RadScriptManager>

    <div style="width:580px;height:147px;">
    
        <table style="width:100%; height: 76px;">
            <tr>
                <td class="style1">
                    </td>
                <td class="style3">
                                <asp:Label ID="Label4" runat="server" 
                        Text="MyRoamData Username" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" 
                        Width="161px"></asp:Label>
                            </td>
                <td style="font-family:Verdana;font-size:12px; margin:10px;padding:10px; text-align:justify" 
                    class="style4">
                    <asp:TextBox ID="MobileCommerceUsername" runat="server" Width="246px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                                <asp:Label ID="Label5" runat="server" 
                        Text="MyRoamData Password" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" 
                        Width="161px"></asp:Label>
                            </td>
                <td style="font-family:Verdana;font-size:12px; margin:10px;padding:10px; text-align:justify" 
                    class="style4">
                    <asp:TextBox ID="MobileCommercePassword" runat="server" Width="246px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3" colspan="2">
                <table><tr><td style="width:100px;">
                    <asp:Button ID="Save" runat="server" Text="Save" Width="59px" onclick="Save_Click" 
                    />
                   </td><td style="width:100px;"><asp:Button ID="Clear" runat="server" Text="Clear" Width="59px" onclick="Clear_Click" 
                    /></td><td>
                    <asp:Label ID="Message" runat="server"  Width="345px" CssClass="message" style="font-family:Verdana;font-size:12px"
                        ForeColor="Maroon"></asp:Label>
                   </td></tr></table>
                            </td>
            </tr>
            <tr>
                <td class="style6" >
                    &nbsp;</td>
                <td class="style5" colspan="2" >
                    &nbsp;</td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
