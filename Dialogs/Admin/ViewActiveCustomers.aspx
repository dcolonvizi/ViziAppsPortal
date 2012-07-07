<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewActiveCustomers.aspx.cs" Inherits="ViewActiveCustomers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View Active ViziApps Customers</title>
		<link href="../../styles/baseStyle.css" type="text/css" rel="stylesheet"/>
		<link href="../../styles/callBackStyle.css" type="text/css" rel="stylesheet"/>
		<link href="../../styles/tabStyle.css" type="text/css" rel="stylesheet"/>
		<link href="../../styles/Calendar.css" type="text/css" rel="stylesheet"/>
		<link href="../../styles/gridStyle.css" type="text/css" rel="stylesheet"/>
    <style type="text/css">
        .style1
        {
            width: 87px;
        }
        .style2
        {
            width: 87px;
            height: 45px;
        }
        .style3
        {
            height: 45px;
        }
        .style4
        {
            height: 45px;
            width: 328px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Verdana;font-size:14px;">
   <table><tr><td class="style2">Min Logins:</td><td class="style3"> 
       <asp:TextBox ID="ActiveUsersMinNLogins" runat="server" Width="47px">6</asp:TextBox></td>
       <td align="right" class="style4">
       Number of Previous Days for Last login:&nbsp;&nbsp; </td><td class="style3"> 
           <asp:TextBox ID="ActiveUsersDaysLoggedIn" runat="server" Width="46px">7</asp:TextBox></td></tr><tr>
           <td class="style1">&nbsp;</td><td colspan="2"><asp:Button ID="GetActiveCustomers" runat="server" 
            onclick="GetActiveCustomers_Click" Text="View Active Customers" Width="154px" /></td><td></td></tr></table>
       
       
        
   
    </div>
    </form>
</body>
</html>
