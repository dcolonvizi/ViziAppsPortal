<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowXmlDesign.aspx.cs"  validateRequest="false" Inherits="Dialogs_Admin_ShowXmlDesign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ViziApps XML App Design</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="Design" runat="server" TextMode="MultiLine" style="width:700px;height:700px;"></asp:TextBox>
    </div>
    <div>
    <table><tr><td>
        <asp:Button ID="Update" runat="server" Text="Update Design" 
            Width="109px" onclick="Update_Click" /></td><td>
             &nbsp;</td><td>
             <asp:Label ID="Message" runat="server" Font-Bold="True" Font-Names="Arial"
                 Font-Size="10pt" ForeColor="Maroon" Width="326px"></asp:Label>
                 </td></tr></table>
    </div>
    </form>
</body>
</html>
