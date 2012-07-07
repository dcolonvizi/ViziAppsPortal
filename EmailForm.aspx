<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailForm.aspx.cs" Inherits="EmailForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Email Form</title>
		<LINK href="styles/baseStyle.css" type="text/css" rel="stylesheet"/>
		<LINK href="styles/callBackStyle.css" type="text/css" rel="stylesheet"/>
		<LINK href="styles/tabStyle.css" type="text/css" rel="stylesheet"/>
		<LINK href="styles/Calendar.css" type="text/css" rel="stylesheet"/>
		<LINK href="styles/gridStyle.css" type="text/css" rel="stylesheet"/>
    <style type="text/css">
        .style1
        {
            width: 15px;
            height: 23px;
        }
        .style2
        {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 555px; height: 436px">
                <tr>
                    <td colspan="4">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td bgcolor="#0054C2" class="style1">
                    </td>
                    <td bgcolor="#0054C2" colspan="2" class="style2">
                        <asp:Label ID="EmailType" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                            ForeColor="White" Text="Sending Emails to Customers"></asp:Label></td>
                    <td bgcolor="#0054C2" class="style2">
                    </td>
                </tr>
                <tr>
                    <td style="width: 15px">
                        &nbsp;
                    </td>
                    <td style="width: 94px">
                    </td>
                    <td style="width: 379px" valign="middle">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15px; height: 30px">
                    </td>
                    <td style="width: 94px; height: 30px">
                        <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                            Text="From Email"></asp:Label></td>
                    <td style="width: 379px; height: 30px" valign="middle">
                        <asp:TextBox ID="FromEmail" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
                            ForeColor="Black" Width="417px"></asp:TextBox></td>
                    <td style="height: 30px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 15px; height: 30px;">
                    </td>
                    <td style="width: 94px; height: 30px;">
                        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                            Text="To Email"></asp:Label></td>
                    <td style="width: 379px; height: 30px;" valign="middle">
                        <asp:Label ID="ToEmail" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
                            ForeColor="Black" Width="417px"></asp:Label></td>
                    <td style="height: 30px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 15px; height: 30px">
                    </td>
                    <td style="width: 94px; height: 30px">
                        <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                            Text="Re: Category"></asp:Label></td>
                    <td style="width: 379px; height: 30px" valign="middle">
                        <asp:DropDownList ID="Category" runat="server"  Font-Names="Arial"
                            Font-Size="10pt"  Width="417px">
                            <asp:ListItem  Selected=True>WebSite issue</asp:ListItem>
                            <asp:ListItem>Configuration issue</asp:ListItem>
                            <asp:ListItem>Deployment issue</asp:ListItem>
                            <asp:ListItem>Call session issue</asp:ListItem>
                            <asp:ListItem>Billing issue</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="height: 30px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 15px; height: 30px">
                    </td>
                    <td style="width: 94px; height: 30px">
                        <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                            Text="Re: Application"></asp:Label></td>
                    <td style="width: 379px; height: 30px" valign="middle">
                        <asp:DropDownList ID="ApplicationList" runat="server"  Font-Names="Arial"
                            Font-Size="10pt"  Width="417px">
                        </asp:DropDownList></td>
                    <td style="height: 30px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 15px; height: 30px">
                    </td>
                    <td style="width: 94px; height: 30px">
                        <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                            Text="Email Subject"></asp:Label></td>
                    <td style="width: 379px; height: 30px" valign="middle">
                        <asp:TextBox ID="EmailSubject" runat="server" Font-Names="Arial" Font-Size="10pt"
                            Width="412px"></asp:TextBox></td>
                    <td style="height: 30px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 15px">
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="EmailBody" runat="server" Height="350px" TextMode="MultiLine" Width="505px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15px; height: 55px">
                    </td>
                    <td colspan="2" style="height: 55px">
                        <asp:Button ID="SendEmails" runat="server" CausesValidation="False" Font-Names="Arial"
                            Font-Size="10pt" OnClick="SendEmails_Click" Text="Send Email" Width="103px" />&nbsp;
                        <asp:Label ID="Message" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                            ForeColor="Maroon" Width="399px"></asp:Label></td>
                    <td style="height: 55px">
                    </td>
                </tr>
            </table>
        </div>
    
    </div>
    </form>
</body>
</html>
