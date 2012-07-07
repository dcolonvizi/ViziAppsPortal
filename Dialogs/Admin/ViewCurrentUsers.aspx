<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCurrentUsers.aspx.cs" Inherits="ViewCurrentUsers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View Current Users</title>
		<link href="../../styles/baseStyle.css" type="text/css" rel="stylesheet"/>
		<link href="../../styles/callBackStyle.css" type="text/css" rel="stylesheet"/>
		<link href="../../styles/tabStyle.css" type="text/css" rel="stylesheet"/>
		<link href="../../styles/Calendar.css" type="text/css" rel="stylesheet"/>
		<link href="../../styles/gridStyle.css" type="text/css" rel="stylesheet"/>
        <script language="javascript" type="text/javascript">
            function setUsername(username) {
                var logout_user = document.getElementById("logout_user");
                logout_user.value = username;
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="logout_user" runat="server" style="display:none"></asp:TextBox>
        <asp:GridView ID="CurrentUsers" runat="server" AutoGenerateColumns="False" BorderColor="Silver"
            BorderStyle="Solid" BorderWidth="1px" CellPadding="2" Font-Names="Arial" Font-Size="9pt"
            PageSize="20" Width="310px">
            <Columns>
                <asp:BoundField DataField="username" HeaderText="Current ViziApps Users" NullDisplayText="null"
                    ReadOnly="True">
                    <ItemStyle Font-Names="Arial" Font-Size="9pt" ForeColor="#404040" />
                    <HeaderStyle Width="300px" />
                </asp:BoundField>
                <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/delete.gif" SelectText="Logout this user"
                    ShowSelectButton="True" >
                    <HeaderStyle Width="50px" />
                </asp:CommandField>
            </Columns>
            <SelectedRowStyle BackColor="#FFFFC0" />
            <HeaderStyle BackColor="Teal" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"
                ForeColor="White" />
            <AlternatingRowStyle BackColor="FloralWhite" />
        </asp:GridView>
    <asp:Label ID="Message" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon"
            Visible="False" Font-Bold="True" Width="844px"></asp:Label>
    </div>
    </form>
</body>
</html>
