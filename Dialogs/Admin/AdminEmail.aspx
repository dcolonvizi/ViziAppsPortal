<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminEmail.aspx.cs" Inherits="AdminEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Email Customers</title>
		<LINK href="../../styles/baseStyle.css" type="text/css" rel="stylesheet"/>
		<LINK href="../../styles/callBackStyle.css" type="text/css" rel="stylesheet"/>
		<LINK href="../../styles/tabStyle.css" type="text/css" rel="stylesheet"/>
		<LINK href="../../styles/Calendar.css" type="text/css" rel="stylesheet"/>
		<LINK href="../../styles/gridStyle.css" type="text/css" rel="stylesheet"/>
    <style type="text/css">
        .style1
        {
            width: 16px;
            height: 22px;
        }
        .style2
        {
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 771px; height: 436px">
            <tr>
                <td colspan="4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td bgcolor="#0054C2" class="style1">
                </td>
                <td bgcolor="#0054C2" colspan="2" class="style2">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        ForeColor="White" Text="Sending Emails to Customers"></asp:Label></td>
                <td bgcolor="#0054C2" class="style2">
                </td>
            </tr>
            <tr>
                <td style="width: 16px">
                </td>
                <td style="width: 94px">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                        Text="Email Type"></asp:Label></td>
                <td style="width: 648px" valign="middle">
                    <asp:RadioButtonList ID="EmailType" runat="server" Font-Names="Arial" Font-Size="10pt"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Production Customers</asp:ListItem>
                        <asp:ListItem Value="All ViziApps Users">All ViziApps Users</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 16px; height: 55px">
                </td>
                <td style="width: 94px; height: 55px">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                        Text="Email Subject"></asp:Label></td>
                <td style="width: 648px; height: 55px" valign="middle">
                    <asp:TextBox ID="EmailSubject" runat="server" Font-Names="Arial" Font-Size="10pt"
                        Width="597px"></asp:TextBox></td>
                <td style="height: 55px">
                </td>
            </tr>
            <tr>
                <td style="width: 16px">
                </td>
                <td colspan="2">
                    <asp:TextBox ID="EmailBody" runat="server" Height="410px" Width="697px" TextMode="MultiLine"></asp:TextBox></td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 16px; height: 55px;">
                </td>
                <td style="height: 55px;" colspan="2">
                    <asp:Button ID="SendEmails" runat="server" CausesValidation="False" Font-Names="Arial"
                        Font-Size="10pt"  Text="Send Emails" Width="103px" OnClick="SendEmails_Click"  />&nbsp;
                    <asp:Label ID="Message" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        ForeColor="Maroon" Width="590px"></asp:Label></td>
                <td style="height: 55px">
                </td>
            </tr>
            <tr>
                <td style="width: 16px; height: 55px">
                </td>
                <td valign="top">
                    <asp:Label ID="SentEmails" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                        Text="Users that were sent email"></asp:Label></td>
                <td  style="height: 55px">
                    <asp:TextBox ID="SentUsers" runat="server" Height="210px" TextMode="MultiLine" Width="300px"></asp:TextBox></td>
                <td style="height: 55px">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
