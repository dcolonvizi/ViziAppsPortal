<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FreeTrialEnds.aspx.cs" Inherits="FreeTrialEnds" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Checkout for Design & Test Service</title>
 <style type="text/css">
BODY { MARGIN-TOP: 0px;  MARGIN-LEFT: 0px }
form{
margin:0px;padding:0px;
}
a:link {
	color: #2C4DC2;
}
a:visited {
	color: #2C4DC2;
}
a:hover {
	color: #2C4DC2;
}
a:active {
	color: #2C4DC2;
}

.heading
{
   font-family:Verdana;
   font-size:12px;
  
}

.style1
{
    font-family:Verdana;
    font-size:16px;
    color:#444444;
            height: 97px;
        }

        .style2
        {
            width: 69px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
</div>
<div style="height:30px"></div>
 <div align="center"> 
<table width="100%"  border="0" cellpadding="0" cellspacing="0" style="background-color:white" align="center">
  
                          <tr valign="top">
    <td class="style2" ></td>
    <td >
      <table width="100%"  border="0" cellspacing="0" cellpadding="0" 
            >
        <tr>
          <td valign="top">
            <table width="100%"  border="0" cellspacing="0" cellpadding="0" 
                  >
              <tr valign="top">
                <td width="485" bgcolor="#FFFFFF"  align="center">
                    <table bgcolor="#FFFFFF" border="0" cellpadding="0" cellspacing="0" 
                        style="width: 751px">
                        <tr>
                            <td align="left" valign="top" class="style1">
    
        Your 
                                free trial period is over. To continue designing and testing ViziApps 
                                there is a service fee of $9.99 per month. To buy this service, click the 
                                Buy button. 
                                <br />
                                <br />
                                After your purchase, you can cancel at an time and your service will remain 
                                active 
                                until the end of the monthly billing cycle.</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 444px; height: 55px" valign="middle">
                                &nbsp; 
                                &nbsp; &nbsp;&nbsp;<asp:Button ID="BuyButton" runat="server" 
                                     Text="Buy ViziApps Design &amp; Test Service" 
                                    Width="233px" onclick="BuyButton_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
                                <asp:Button ID="RejectButton" runat="server" 
                                     Text="Logout" OnClick="RejectButton_Click" /></td>
                        </tr>
                    </table>
                </td>
              </tr>
            </table>

              </td></tr>
      </table></td>
  </tr>
</table>
</div>

    <div>
    
        </div>
    </form>
</body>
</html>
