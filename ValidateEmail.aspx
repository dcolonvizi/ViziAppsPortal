<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ValidateEmail.aspx.cs" Inherits="ValidateEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
			<title>ViziApps: Build a Mobile App Online</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="styles/baseStyle.css" type="text/css" rel="stylesheet"/>
		
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<style type="text/css">BODY { MARGIN-TOP: 0px;  MARGIN-LEFT: 0px }
		    .style45
            {
                font-family: Verdana;
                font-size: 12px;
                color: White;
                width: 123px;
            }
            .menu
            {
            	text-align:center;
            }
		    .style46
            {
                width: 596px;
            }
		    .style47
            {
                width: 53px;
            }
            .style48
            {
                width: 135px;
            }
		</style>
	<telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
</head>
  <body style=" background-color:#bcbcbc;">
	<form id="Form1" runat="server">
	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
	</telerik:RadScriptManager> 

	<telerik:RadSkinManager ID="RadSkinManager1" Runat="server" Skin="Telerik">
	</telerik:RadSkinManager>

   
	              <div align="center" id="header" style="height:60px;  background-color:#000099;">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                
               
                <td>
                <asp:Image ID="HeaderImage" ImageUrl="~/images/logo_header_300.png" runat="server">
                                                            </asp:Image>
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


                <div align="center">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tabs"  style="height:38px; border:0px; padding:0px;  vertical-align:top;  margin:0px; background-image:url(images/tabs_section.png); ">
                    <tr><td align="center" valign="top">
                     <div align="center">
                    </div>
                </td></tr>
                </table>   
                </div>
                 <div align="center" id="page"  style="background-color:#bcbcbc;">
                     <table  cellpadding="0" cellspacing = "0"  border="0" style="width: 900px;" >
                         <tr style="height:16px">
                             <td style="height:16px;width:16px;  background-image:url(images/round_top_left.png); background-position:right bottom; background-repeat:no-repeat;">
                                   
                             </td>
                             <td style="background-image:url(images/white_square.png); height:16px; width:868px;background-repeat:repeat">
                             
                             </td>
                             <td style="height:16px;width:16px;background-image:url(images/round_top_right.png); background-position:left bottom; background-repeat:no-repeat; ">
                              </td>
                         </tr>
                         </table>
                        
                        <div align="center" style="background-color:#bcbcbc;">
                         <table  cellpadding="0" cellspacing = "0"  border="0" style="background-color:white;width: 900px;" >
                         <tr><td class="style47">
                             &nbsp;</td>
                                <td class="style46" align="right">
                            <asp:Label ID="Message" runat="server" 
                                style=" font-family:Verdana;font-size:14px" ForeColor="#4B4B4B" Width="549px"></asp:Label>
                                </td>
                                <td align="center" class="style48">
                            <asp:Button ID="LoginButton" runat="server" PostBackUrl="Default.aspx" 
                                Text="Login" />
                                </td>
                                <td align="center">
                                    &nbsp;</td>
                                </tr></table>
                        
               </div>
               <div align="center" id="Div1"  style="background-color:#bcbcbc;">
                     <table  cellpadding="0" cellspacing = "0"  border="0" style="width: 900px;" >
                         <tr style="height:16px">
                             <td style="height:16px;width:16px;  background-image:url(images/round_bottom_left.png); background-position:right bottom; background-repeat:no-repeat;">
                                   
                             </td>
                             <td style="background-image:url(images/white_square.png); height:16px; width:868px;background-repeat:repeat">
                             
                             </td>
                             <td style="height:16px;width:16px;background-image:url(images/round_bottom_right.png); background-position:left bottom; background-repeat:no-repeat; ">
                              </td>
                         </tr>
                          </table>
                        
   
                </div>              
	</form>
</body>
</html>
