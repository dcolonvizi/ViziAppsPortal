<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ViziApps: Build Mobile Apps Online</title>
    		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <meta http-equiv="Expires" content="-1"/>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>

		<link href="styles/baseStyle.css" type="text/css" rel="stylesheet"/>
    <style type="text/css">
        body
        {
        	 background-color:#bcbcbc;
        }
        .style2
        {
            height: 36px;
        }
        .style4
        {
            height: 33px;
        }
        .style5
        {
            height: 35px;
        }
    </style>
   <script  language="javascript" type="text/javascript" src="scripts/google_analytics_1.0.js"></script>
      <script type="text/javascript" src="jquery/js/jquery-1.4.4.min.js"></script>
    <script  language="javascript" type="text/javascript" src="scripts/default_script_1.6.js"></script>  
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">		
	</telerik:RadScriptManager>
	
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
    <telerik:AjaxSetting AjaxControlID="ForgotPasswordButton">
    <UpdatedControls>
    <telerik:AjaxUpdatedControl ControlID="LoginPages" />
    </UpdatedControls>
    </telerik:AjaxSetting>
    </AjaxSettings>
	</telerik:RadAjaxManager>

     <div align="center" id="header" style="height:60px; width:100%;  background-color:#0054c2;">
                <table cellpadding="0" cellspacing="0" border="0">
                <tr>                
               
                <td><a href="http://ViziApps.com" style="text-decoration:none">
                <asp:Image ID="HeaderImage" ImageUrl="~/images/logo_header_300.png" runat="server" style="border:0px">
                                                            </asp:Image></a>
                </td>
               
                <td class="style45">
                   
                   
                    &nbsp;</td>
                     <td style="color:White;"></td>
               
                <td align="center">
                    &nbsp;</td>
                <td style="color:White;"></td>
                <td class="heading" align="center">
                    &nbsp;</td>
                <td>               
                </td>
                </tr>
                </table>
                </div>  
                  <div align="center" style="width:100%;height: 30px">
               <table border="0" cellpadding="0" cellspacing="0" id="tabs"  style="width:100%;height:30px; border:0px; padding:0px;  vertical-align:top;  margin:0px; background-image:url(images/tabs_section.gif); background-repeat:repeat-x  ">
                    <tr><td align="center" valign="top">
                     <div align="center">
                      
                     <table border="0" cellpadding="0" cellspacing="0" style="height:30px;" ><tr>
                     <td >
                       
                         &nbsp;</td></tr>
                          </table>
                        
                    </div>
                </td></tr>
                </table>   
               
                </div>   
                <telerik:RadMultiPage ID="LoginPages" runat="server" 
        SelectedIndex="0">
        <telerik:RadPageView ID="Login" runat="server">
           
    <div align="center" style="vertical-align:top;height:260px; ">
       
        <table style="width: 300px;  background-image:url('images/login_background.png'); background-repeat:no-repeat; height: 260px; font-family:Arial;color:#333333;font-size:12pt;">
       
                            
            <tr>
                <td align="center"  colspan="2" class="style2">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/login_button2.png" /></td>
            </tr>
            <tr>
                <td align="center" class="style4" >
                    Username</td>
                <td align="left" class="style4" >
                    <asp:TextBox ID="Username" runat="server" 
                        Width="130px" Height="22px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="center"  
                    class="style4">
                    Password</td>
                <td align="left" class="style4">
                    <asp:TextBox ID="Password" runat="server" 
                        TextMode="Password" 
                        Width="130px" Height="22px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="style5" ></td>
                <td  align="left" class="style5">
                    <asp:Button ID="ViziAppsLogin" runat="server" CausesValidation="False" 
                         Text="Login" onclick="ViziAppsLogin_Click" 
                        /></td>
            </tr>
            <tr>
                <td class="style5" >
                    </td>
                <td  align="left" class="style5">
    <asp:Button ID="ForgotPasswordButton" runat="server" CausesValidation="False"
        onclick="ForgotPasswordButton_Click" Text="I forgot my password" Width="144px" 
                        Font-Names="Arial" Font-Size="10pt" ForeColor="#333333" /></td>
            </tr>
            <tr>
                <td colspan="2" align="center" valign="top" >
                    <asp:Label ID="FailureText" runat="server" Font-Names="Arial" Font-Size="12px" 
                        ForeColor="Maroon"></asp:Label></td>
            </tr>
            </table>
        
        </div>
           </telerik:RadPageView>
           <telerik:RadPageView ID="ForgotLogin" runat="server" Height="16px">
           
    <div align="center" style="vertical-align:top;height:150px; ">
       
        <table style="width: 300px;  background-image:url('images/forgot_login_background.png'); background-repeat:no-repeat; height: 152px; font-family:Arial;color:#333333;font-size:12pt;">
       
                            
            <tr>
                <td align="center"  colspan="2" class="style2">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/login_button2.png" /></td>
            </tr>
            <tr>
                <td align="center" class="style4" >
                    email</td>
                <td align="left" class="style4" >
                    <asp:TextBox ID="Email" runat="server" 
                        Width="197px" Height="22px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="style5" >
                    </td>
                <td  align="left" class="style5">
    <asp:Button ID="SendPasswordButton" runat="server" CausesValidation="False"
        Text="Send my credentials" Width="144px" 
                        Font-Names="Arial" Font-Size="10pt" ForeColor="#333333" 
                        onclick="SendPasswordButton_Click" /></td>
            </tr>
            <tr>
                <td colspan="2" align="center" valign="top" >
                    <asp:Label ID="Message" runat="server" Font-Names="Arial" Font-Size="12px" 
                        ForeColor="Maroon"></asp:Label></td>
            </tr>
            </table>
        
        </div>
           </telerik:RadPageView>
    </telerik:RadMultiPage>      

    </form>
</body>
</html>
