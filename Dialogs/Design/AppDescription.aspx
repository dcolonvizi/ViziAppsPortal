<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppDescription.aspx.cs" Inherits="Dialogs_AppDescription" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ViziApps App Description</title>
    <style type="text/css">
        .style1
        {
            height: 126px;
        }
        #AppDescription
        {            width: 300px;
        }
    
        .message
        {
        	font-family:Verdana;
        	font-size:12px;
        	color:Maroon;
        	}
        </style>
</head>
<body>
     <form id="form1" runat="server">
     <telerik:RadScriptManager ID="RadScriptManagerEditAppProperties" runat="server">
                </telerik:RadScriptManager>

    <div style="width:580px;height:190px;">
    
        <table style="width:100%; height: 76px;">
            <tr>
                <td class="style1">
                    </td>
                <td class="style1">
                                <asp:Label ID="Label4" runat="server" 
                        Text="App Description" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" 
                        Width="113px"></asp:Label>
                            </td>
                <td style="font-family:Verdana;font-size:12px; width: 423px; height: 107px; margin:10px;padding:10px; text-align:justify">
                    <textarea id="AppDescription" runat="server" rows="8"
                        ></textarea></td>
            </tr>
            <tr>
                <td >
                    &nbsp;</td>
                <td class="style5" >
                    <asp:Button ID="Save" runat="server" Text="Save" Width="59px" onclick="Save_Click" 
                    />
                </td>
                <td >
                   
                </td>
            </tr>
            <tr>
                <td >
                    &nbsp;</td>
                <td class="style5" colspan="2" >
                    <asp:Label ID="Message" runat="server"  Width="546px" CssClass="message"></asp:Label>
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
