<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Create a ViziApps Account</title>
		<LINK href="styles/baseStyle.css" type="text/css" rel="stylesheet"/>
		<LINK href="styles/callBackStyle.css" type="text/css" rel="stylesheet"/>
		<LINK href="styles/tabStyle.css" type="text/css" rel="stylesheet"/>
		<LINK href="styles/Calendar.css" type="text/css" rel="stylesheet"/>
		<LINK href="styles/gridStyle.css" type="text/css" rel="stylesheet"/>
<style type="text/css">

body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	
}
body,td,th,input {
	font-family: Verdana;
	font-size: 12px;
	color: #333333;
        margin-bottom: 0px;
    }
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
    #CreateAccountSubmit
    {
        width: 201px;
    }
    .style1
    {
        width: 9px;
        height: 41px;
    }
    .style2
    {
        width: 158px;
        height: 41px;
    }
    .style3
    {
        width: 413px;
        height: 30px;
    }
    .style4
    {
        width: 413px;
        height: 41px;
    }
    .style6
    {
        height: 10px;
        width: 158px;
    }
    .style7
    {
        width: 158px;
    }
    .style8
    {
        height: 34px;
        width: 158px;
    }
    .style9
    {
        height: 33px;
        width: 158px;
    }
    .style10
    {
        height: 65px;
        width: 158px;
    }
    .style11
    {
        height: 35px;
        width: 158px;
    }
    .style12
    {
        height: 32px;
        width: 158px;
    }
    .style13
    {
        width: 9px;
        height: 40px;
    }
    .style14
    {
        width: 413px;
        height: 40px;
    }
    .style15
    {
        height: 40px;
        width: 158px;
    }
    .style16
    {
        height: 23px;
    }
    .style17
    {
        width: 13px;
    }
-->
</style>
 <script  language="javascript" type="text/javascript" src="scripts/google_analytics_1.0.js"></script>
</head>
<body>
<form id="form1" runat="server">

<table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
  <tr valign="top">
    <td width="20%" style="height: 113px">&nbsp;</td>
    <td width="61%" style="height: 113px"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="22%" valign="top" style="background-repeat:no-repeat "></td>
        <td width="78%" valign="top"></td>
      </tr>
    </table>
      <table width="100%"  border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="20" align="right" valign="top" style="height: 526px">&nbsp;</td>
          <td valign="top" style="height: 526px">
            <table width="100%"  border="0" cellspacing="0" cellpadding="0">
              <tr valign="top">
                <td width="485" bgcolor="#FFFFFF" style="height: 155px;">
                   
                         
                                 <table bgcolor="#FFFFFF" border="0" cellpadding="0" cellspacing="0" width="100%" height="200">
                        <tr>
                            <td style="width: 14px; height: 10px">
                            </td>
                            <td style="width: 460px; height: 10px;background-color:#0054c2;">
                                <table><tr><td class="style17"></td><td><asp:Image ID="HeaderImage" runat="server" 
                                    ImageUrl="~/images/logo_header_300.png" /></td></tr></table></td>
                            <td style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 14px; height: 87px">
                            </td>
                            <td align="left" style="width: 460px; height: 87px">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
                                    <tr>
                                        <td bgcolor="white" colspan="2" style="height: 26px">
                                        </td>
                                        <td bgcolor="white" style="width: 9px; height: 26px">
                                        </td>
                                    </tr>
                                </table>
                                <div style="width: 600px; ">
                                <table align="left" border="0" cellpadding="0" cellspacing="0"
                                    frame="box" width="600">
                                    <tr>
                                        <td align="left" bgcolor="#999999" colspan="3" 
                                            valign="middle" class="style16">
                                            &nbsp;&nbsp;
                                            <asp:Label ID="Label13" runat="server" Font-Bold="True"  
                                                ForeColor="White" Text="Create My ViziApps Account" Width="200px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 9px; height: 10px">
                                        </td>
                                        <td align="left" style="  
                                            " valign="top" class="style6">
                                        </td>
                                        <td align="left" style="width: 413px; height: 10px" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 9px; height: 56px">
                                        </td>
                                        <td align="left" colspan="2" style="   height: 56px;"
                                            valign="top">
                                                <asp:Label ID="Label4" runat="server"    Width="428px" Font-Bold="True">Please 
                                                fill the fields below and then click on &#39;Create My ViziApps Account&#39;. Fields 
                                                with a <img src="images/red_border.gif" style="position: relative; top: 2pt"/> are required.</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 31px; width: 9px;">
                                        </td>
                                        <td align="left" style="  " 
                                            valign="top" class="style7">
                                            <asp:Label ID="Label1" runat="server"  
                                                Text="Username" Font-Italic="False"></asp:Label>
                                        </td>
                                        <td align="left" style="height: 31px; width: 413px;" valign="top">
                                            <asp:TextBox ID="UsernameBox" runat="server"  Width="170px" 
                                                Font-Italic="False" BorderColor="Salmon" BorderWidth="1px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 31px; width: 9px;">
                                        </td>
                                        <td align="left" style="  " 
                                            valign="top" class="style7">
                                            <asp:Label ID="Label2" runat="server"  
                                                Text="Password" Font-Italic="False"></asp:Label>
                                        </td>
                                        <td align="left" style="height: 31px; width: 413px;" valign="top">
                                            <asp:TextBox ID="PasswordTextBox" runat="server"  TextMode="Password"
                                                Width="170px" Font-Italic="False" BorderColor="Salmon" BorderWidth="1px"></asp:TextBox>
                                            &nbsp;&nbsp;
                                            <asp:Label ID="Label3" runat="server" style="font-size:11px;" 
                                                Text="(Password must be 6 to 30 characters)"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 35px; width: 9px;">
                                        </td>
                                        <td align="left" style="  " 
                                            valign="top" class="style7">
                                            <asp:Label ID="Label14" runat="server"  
                                                Text="Confirm Password" Font-Italic="False" Width="137px"></asp:Label>
                                        </td>
                                        <td align="left" style="height: 35px; width: 413px;" valign="top">
                                            <asp:TextBox ID="ConfirmPasswordBox" runat="server"  TextMode="Password"
                                                Width="170px" Font-Italic="False" BorderColor="Salmon" BorderWidth="1px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 34px; width: 9px;">
                                            &nbsp;</td>
                                        <td align="left" valign="top" class="style8">
                                            <div align="left" 
                                                style=" width: 111px;  ">
                                                <span class="style3">
                                                    <asp:Label ID="Label5" runat="server"  
                                                        Text="First Name" Font-Italic="False"></asp:Label></span>&nbsp;&nbsp;</div>
                                        </td>
                                        <td align="left" style="height: 34px; width: 413px;" valign="top">
                                            <asp:TextBox ID="FirstNameTextBox" runat="server"  
                                                Width="170px" Font-Italic="False" AutoCompleteType="FirstName" 
                                                BorderColor="Salmon" BorderWidth="1px"></asp:TextBox>
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 33px; width: 9px;">
                                            &nbsp;</td>
                                        <td align="left" valign="top" class="style9">
                                            <div align="left" style=" width: 82px;  ">
                                                <span class="style3">
                                                    <asp:Label ID="Label6" runat="server"  
                                                        Text="Last Name" Font-Italic="False"></asp:Label></span>&nbsp;&nbsp;</div>
                                        </td>
                                        <td align="left" style="height: 33px; width: 413px;" valign="top">
                                            <asp:TextBox ID="LastNameTextBox" runat="server"  Width="170px" 
                                                Font-Italic="False" AutoCompleteType="LastName" BorderColor="Salmon" 
                                                BorderWidth="1px"></asp:TextBox>
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 9px; height: 65px;">
                                        </td>
                                        <td align="left" style="   " 
                                            valign="top" class="style10">
                                            <asp:Label ID="Label12" runat="server"  
                                                Text="Email " Font-Italic="False"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" 
                                            style=" height: 65px; width: 413px;">
                                            
                                            <asp:TextBox ID="EmailTextBox" runat="server"  Width="365px" 
                                                Font-Italic="False" AutoCompleteType="Email" BorderColor="Salmon" 
                                                BorderWidth="1px"></asp:TextBox><br />
                                            Your email is verified when you reply to our registration email. If you don't get
                                            that email, check your anti-spam location.<br />
                                            </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 35px; width: 9px;">
                                        </td>
                                        <td align="left" style="  
                                            " valign="top" class="style11">
                                            <asp:Label ID="Label7" runat="server"  
                                                Text="Phone Number" Font-Italic="False"></asp:Label></td>
                                        <td align="left" style="height: 35px; width: 413px;" valign="top">
                                            <asp:TextBox ID="PhoneTextBox" runat="server"  Width="170px" Font-Italic="False" AutoCompleteType="BusinessPhone"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                        </td>
                                        <td align="left" style="  
                                            " valign="top" class="style2">
                                            <asp:Label ID="Label8" runat="server"  
                                                Text="How did you hear about us?" Font-Italic="False"></asp:Label></td>
                                        <td align="left" valign="top" class="style4">
                                            <asp:DropDownList ID="ReferralSourceList" runat="server" style="font-size:12px; font-family:Verdana; color:#222222"
                                                 >
                                                <asp:ListItem>Select -&gt;</asp:ListItem>
                                                <asp:ListItem>Web Search</asp:ListItem>
                                                 <asp:ListItem>Article</asp:ListItem>
                                                <asp:ListItem>Word of Mouth</asp:ListItem>
                                                 <asp:ListItem>Blog</asp:ListItem>
                                                <asp:ListItem>LinkedIn</asp:ListItem>
                                                <asp:ListItem>Newsletter</asp:ListItem>
                                                <asp:ListItem>Trade Show</asp:ListItem>
                                                <asp:ListItem>Facebook</asp:ListItem>
                                                <asp:ListItem>Twitter</asp:ListItem>
                                                 <asp:ListItem>Web Ad</asp:ListItem>
                                                <asp:ListItem>Magazine Ad</asp:ListItem>
                                                <asp:ListItem>Company Sales Dept.</asp:ListItem>
                                                 <asp:ListItem>Webinar</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                           </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="style13">
                                            </td>
                                        <td align="left" class="style15">
                                            <asp:Label ID="Label15" runat="server"  
                                                Text="In a phrase, describe the app you want to build?"></asp:Label>
                                        </td>
                                        <td align="left" valign="top" class="style14">
                                            <asp:TextBox ID="AppToBuild" runat="server"  Width="413px" 
                                                Font-Italic="False"></asp:TextBox>
                                                <asp:Label ID="Label9" runat="server"  
                                                Text="Example: An app to provide price and availibility of our products"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 32px; width: 9px;">
                                            &nbsp;</td>
                                        <td align="left" style="height: 32px;" colspan="2" valign="middle">
                                            <asp:Label ID="MessageLabel" runat="server"   ForeColor="Maroon"
                                                Width="582px" Font-Bold="True" Height="41px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 32px; width: 9px;">
                                        </td>
                                        <td align="left" class="style12">
                                        </td>
                                        <td align="left" valign="top" style="height: 32px; width: 413px;">
                                            <input id="CreateAccountSubmit" runat="server" name="CreateAccount" onserverclick="CreateAccountSubmit_ServerClick"
                                                type="submit" value="Create My ViziApps Account" /></td>
                                    </tr>
                                    </table>
                                </div>
                            </td>
                            <td style="height: 87px">
                            </td>
                        </tr>
                    </table>
                    
                   
                </td>
                <td width="254" style="height: 155px">
                </td>
              </tr>
            </table>

              </td></tr>
      </table></td>
      
    <td bordercolor="#FFFFFF" style="width: 55%; height: 113px;">&nbsp;</td>
  </tr>
</table>

</form>
</body>
</html>
