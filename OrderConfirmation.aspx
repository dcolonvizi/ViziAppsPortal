<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderConfirmation.aspx.cs" Inherits="OrderConfirmation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ViziApps Store Order Confirmation</title>
    		<link href="styles/baseStyle.css" type="text/css" rel="stylesheet"/>
            <style type="text/css">BODY { MARGIN-TOP: 0px;  MARGIN-LEFT: 0px }
                </style>
</head>
<body>
    <form id="form1" runat="server">
    
      <div align="left">
	              <div align="center" id="header" style="height:60px;  background-color:#000099;">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                
               
                <td>
                <asp:Image ID="HeaderImage" ImageUrl="~/images/logo_header_300.png" runat="server">
                                                            </asp:Image>
                </td>
               
                <td class="style45">
                   
                   
                    
                    
                    </td>
                     <td style="color:White;"></td>
               
                <td align="center">
                </td>
                <td style="color:White;"></td>
                <td class="heading" align="center">
                    &nbsp;</td>
                <td>
                </td>
                </tr>
                </table>
                </div>  
          <div ID="AdminContainer"  style="height:1000px; padding-left:100px; background-color: #ffffff;" runat="server">
          
       <p><font face="Arial, Helvetica, sans-serif" color="black" size="3"><b>Order Confirmation</b></font></p>
 
<font face="Arial, Helvetica, sans-serif" color="black" size="2">
<p><b>Thanks for your order! We really appreciate your business.</b></p>
              <p>
 
<font face="Arial, Helvetica, sans-serif" color="black" size="2">
    <asp:Label ID="Warning" runat="server" Font-Bold="True" Font-Size="10pt" 
        ForeColor="Red" 
        Text="Your Billing email does not match your ViziApps account email. Please report this to support@viziapps.com right away." 
        Visible="False"></asp:Label>
 
 
 
</font>
 

    
              </p>
 
 
	
 
 
 
 
 
 
<p>
	Please make note of your confirmation number: <asp:Label ID="ConfirmationNumber" runat="server"
        ></asp:Label>&nbsp;for your order:
    </p>
              <p>
 
<font face="Arial, Helvetica, sans-serif" color="black" size="2">
                  <asp:Label ID="Service" runat="server"
        ></asp:Label>
 
 
 </font>

 

    
</p>
 
 <font face="Arial, Helvetica, sans-serif" color="black" size="2">
	<p>You can review the status of your order at any time by:</p>
              <ul>
                  <li>Going to the Publish Page </li>
                  <li>Clicking <b>Manage Your Billing</b></li>
                  <li>Clicking on <strong>Account History</strong>.</li>
              </ul>
 
 </font>
 
<p>If you have any questions about your order, please send an e-mail to <a href='mailto:sales@viziapps.com'>sales@viziapps.com</a>.</p>
              <p>
                 <asp:Button ID="LoginToUser" runat="server" CausesValidation="False" 
                     Font-Names="Arial" Font-Size="10pt" Text="Go To My ViziApps Portal" 
                     Width="193px" onclick="LoginToUser_Click" />
              </p>
 
</font>
</div>
</div>
    </form>
</body>
</html>