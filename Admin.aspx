<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ViziApps Admin</title>
    		<link href="styles/baseStyle.css" type="text/css" rel="stylesheet"/>
            <style type="text/css">
                 body
                {
        	         background-color:#bcbcbc;
                }
                .style1
                {
                    width: 95px;
                }
                .style2
                {
                    height: 33px;
                }
                .style3
                {
                    height: 34px;
                }
                .style4
                {
                    height: 35px;
                }
                .style5
                {
                    height: 37px;
                }
                .style7
                {
                    width: 212px;
                }
            </style>
      <script  language="javascript" type="text/javascript" src="scripts/default_script_1.6.js"></script>
</head>
<body>
    <form id="form1" runat="server">
 	<telerik:RadScriptManager ID="RadScriptManagerAdmin" runat="server">
	</telerik:RadScriptManager>
        <telerik:RadWindowManager ID="MySolutionsRadWindowManager" runat="server">
            <Windows>          

                <telerik:RadWindow 
                    id="CopyApplicationBox" 
                    runat="server"
                    Modal="true"
                    showcontentduringload="false"
                     DestroyOnClose="true"
                      VisibleStatusbar="false"
                      VisibleTitlebar="true"
                    title="Copy Application" 
                   behaviors="Close" Skin="Web20">
                </telerik:RadWindow>
                
            </Windows>
            </telerik:RadWindowManager>
    
      <div align="center">
	              <div align="center" id="header" style="height:60px;  background-color:#0054c2;">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                
               
                <td>
                <asp:Image ID="HeaderImage" ImageUrl="~/images/logo_header_300.png" runat="server">
                                                            </asp:Image>
                </td>
               
                <td class="style45">
                   
                   
                    <asp:Label ID="UserLabel" runat="server"></asp:Label>
                    
                    </td>
                     <td style="color:White;"></td>
               
                <td align="center">
                </td>
                <td style="color:White;"></td>
                <td class="heading" align="center">
                    <asp:ImageButton ID="LogoutButton" runat="server"  
                        ImageUrl="images/LogoutButton.png" onclick="LogoutButton_Click"/>
                </td>
                <td>
                </td>
                </tr>
                </table>
                </div>  
 
    <div align="center" style=" background-color:#bcbcbc; width:900px; vertical-align:top;">
                <table border="0" cellpadding="0" cellspacing="0">
                 <tr style="height:16px">
                             <td style="height:16px;width:16px;  background-image:url(images/round_top_left.png); background-position:right bottom; background-repeat:no-repeat;">
                                   
                             </td>
                             <td style="background-image:url(images/white_square.png); height:16px; width:868px;background-repeat:repeat">
                             
                             </td>
                             <td style="height:16px;width:16px;background-image:url(images/round_top_right.png); background-position:left bottom; background-repeat:no-repeat; ">
                              </td>
                         </tr>
                </table>
                </div>
 <table style="width: 900px;background-color: #ffffff;" border=0 cellpadding="0" cellspacing="0">
    <tr>
        <td colspan="5" style="height: 20px" valign="middle" align="left" 
            bgcolor="#680792">
            &nbsp;
            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                ForeColor="White" Text="All Customers and Applications"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="5" align="right" class="style20">
            </td>
    </tr>
    <tr>
    <td align="left" valign="top" colspan="5">
            &nbsp;<asp:Label ID="Message" runat="server" Font-Bold="True" Font-Italic="False" 
                Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Height="23px" 
                Width="812px"></asp:Label>
            &nbsp;&nbsp;
            </td>
    </tr>
    <tr>
        <td valign="top" class="style21" colspan="5" >
            <table style="width: 861px; height: 80px;">
            <tr>
            <td class="style5" align="left">
                <asp:Button ID="ViewCurrentUsers" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" Text="View Current Users" 
                    Width="132px" />
                </td>
            <td class="style5">
                <asp:Button ID="ViewAllCustomers" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" Text="View All Customers" 
                    Width="143px" />
                </td>
            <td class="style5">
                <asp:Button ID="EmailUpgradeNotice" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" Text="Email Maintenance Notice" 
                    Width="167px" onclick="EmailUpgradeNotice_Click"  />
                </td>
            <td class="style5">
                &nbsp;</td>
            <td class="style5">
                <asp:Button ID="EmailCustomers" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" Text="Email Customers" Width="140px" />
                </td>
            </tr>
            <tr>
            <td class="style18" align="left" colspan="2">
                <asp:Button ID="ViewCurrentUsers0" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" Text="Copy Apps Across Accounts" 
                    Width="191px"  OnClientClick="var oWin=radopen('Dialogs/Admin/CopyApplication.aspx', 'CopyApplicationBox');
                       oWin.set_visibleTitlebar(true);
                       oWin.set_visibleStatusbar(false);
                       oWin.set_modal(true); 
                       oWin.moveTo(20, 200);
                       oWin.setSize(800,200);
                       return false;"/>
                </td>
            <td class="style18">
                <asp:Button ID="ViewActiveCustomers" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt"  
                    Text="View Active Users" Width="127px" 
                      />
                </td>
            <td class="style18">
                &nbsp;</td>
            <td class="style18">
                <asp:Button ID="UpdateImageListing" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" Text="Update Image Listing" 
                    Width="148px" onclick="UpdateImageListing_Click" />
                </td>
            </tr>
             </table></td>
    </tr>
     <tr>
         <td class="style14" colspan="5" valign="top">
             &nbsp;</td>
     </tr>
     <tr>
         <td bgcolor="#D97540" class="style14" colspan="5" valign="top">
             &nbsp;&nbsp;
             <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Arial" 
                 Font-Size="10pt" ForeColor="White" Text="Each Customer"></asp:Label>
         </td>
     </tr>
     <tr>
         <td class="style15" colspan="5" valign="top">
             <asp:Label ID="AdminMessage" runat="server" Font-Bold="True" 
                 Font-Italic="False" Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" 
                 Height="23px" Width="833px"></asp:Label>
         </td>
     </tr>
     <tr>
         <td valign="top" class="style4" align="right">
             <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10pt" 
                 ForeColor="Black" Text="Customer by Account"></asp:Label>
         &nbsp;
         </td>
         <td valign="top" class="style4">
             <telerik:RadComboBox ID="CustomersByAccount" runat="server" AutoPostBack="True" 
                 Font-Names="Arial" Font-Size="10pt" 
                  Width="155px" 
                 MarkFirstMatch="True" 
                 onselectedindexchanged="CustomersByAccount_SelectedIndexChanged">
             </telerik:RadComboBox>
         </td>
         <td class="style4" valign="top" colspan="3">
             <table style="width: 560px">
             <tr>
             <td class="style29" align="left" valign="top">
                 <asp:Button ID="LoginToUserbyAccount" runat="server" CausesValidation="False" 
                     Font-Names="Arial" Font-Size="10pt" Text="Login To User" Visible="False" 
                     Width="126px" onclick="LoginToUserbyAccount_Click"  />
             </td>
             <td class="style30">
                 <asp:Label ID="RegisteredDateTime" runat="server" Font-Names="Arial" 
                     Font-Size="9pt" ForeColor="Black" Visible="False" Width="200px"></asp:Label>
             </td>
             </tr>
             </table>
             </td>
     </tr>
     <tr>
         <td valign="top" class="style4" align="right">
             <asp:Label ID="Label25" runat="server" Font-Names="Arial" Font-Size="10pt" 
                 ForeColor="Black" Text="Customer by Email"></asp:Label>
         &nbsp;
         </td>
         <td valign="top" class="style4">
             <telerik:RadComboBox ID="CustomersByEmail" runat="server" AutoPostBack="True" 
                 Font-Names="Arial" Font-Size="10pt" 
                  Width="155px" 
                 MarkFirstMatch="True" 
                 onselectedindexchanged="CustomersByEmail_SelectedIndexChanged">
             </telerik:RadComboBox>
         </td>
         <td class="style4" valign="top" colspan="3">
             <table style="width: 557px">
             <tr>
             <td class="style7" align="left" valign="top">
                 <asp:Button ID="LoginToUserbyEmail" runat="server" CausesValidation="False" 
                     Font-Names="Arial" Font-Size="10pt" Text="Login To User" Visible="False" 
                     Width="126px" onclick="LoginToUserbyEmail_Click" />
             </td>
             <td>
                 <asp:Label ID="LastUsedDateTime" runat="server" Font-Names="Arial" 
                     Font-Size="9pt" ForeColor="Black" style="margin-left: 0px" Visible="False" 
                     Width="200px"></asp:Label>
             </td>
             </tr>
             </table>
             </td>
     </tr>
    <tr height="20">
        <td style="width: 404px; height: 35px" valign="top" align="right">
            <asp:Label ID="PasswordLabel" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                Text="Password" Visible="False"></asp:Label>&nbsp; </td>
        <td style="width: 295px; height: 35px" valign="top">
            <asp:TextBox ID="Password" runat="server" Font-Names="Arial" Font-Size="10pt" Visible="False"></asp:TextBox></td>
        <td align="left" valign="top" class="style2">
            <asp:Button ID="UpdatePassword" runat="server" CausesValidation="False" Font-Names="Arial"
                Font-Size="10pt" Text="Update Password"
                Visible="False" Width="130px" OnClick="UpdatePassword_Click" /></td>
        <td align="left" valign="top" class="style1">
                 <asp:Button ID="ViewUserProfile" runat="server" CausesValidation="False" 
                     Font-Names="Arial" Font-Size="10pt" Text="View User Profile" Visible="False" 
                     Width="126px" />
             </td>
        <td style="height: 35px" valign="top">
        <asp:Label ID="PasswordMessage" runat="server" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Maroon" Width="164px" Font-Bold="True"></asp:Label></td>
    </tr>
     <tr height="20">
         <td style="width: 404px; height: 35px" valign="top" align="right"><asp:DropDownList ID="ExpirationDateMode" runat="server" Font-Names="Arial" Font-Size="10pt" AutoPostBack="True"  Visible="False" Width="112px" OnSelectedIndexChanged="ExpirationDateMode_SelectedIndexChanged">
             <asp:ListItem Selected="True">Never Expires</asp:ListItem>
             <asp:ListItem>Expires</asp:ListItem>
         </asp:DropDownList></td>
         <td style="width: 295px; height: 35px" valign="top">
             <asp:TextBox ID="ExpirationDate" runat="server" Font-Names="Arial" Font-Size="10pt"
                 Visible="False"></asp:TextBox></td>
         <td align="left" colspan="2" valign="top" class="style2">
             <asp:Button ID="UpdateExpirationDate" runat="server" CausesValidation="False" Font-Names="Arial"
                Font-Size="10pt"  Text="Update Expiration Date"
                Visible="False" Width="156px" OnClick="UpdateExpirationDate_Click" /></td>
         <td style="height: 35px;" valign="top">
             <asp:Label ID="ExpirationMessage" runat="server" Font-Names="Arial" Font-Size="10pt"
                 ForeColor="Maroon" Width="157px" Font-Bold="True"></asp:Label></td>
     </tr>
     <tr height="20">
         <td style="width: 404px; height: 40px" valign="top" align="right">
             <asp:Label ID="CustomerStatusLabel" runat="server" Font-Names="Arial" Font-Size="10pt"
                 ForeColor="Black" Text="Customer Status: " Visible="False"></asp:Label> </td>
         <td style="width: 295px; height: 40px" valign="top" align="center">
             <asp:Label ID="CustomerStatus" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                 Visible="False"></asp:Label></td>
         <td colspan="3" valign="top" style="height: 40px">
            <table width="100%">
                <tr>
                    <td align="left" class="style31">
            <asp:Button ID="DeactivateCustomer" runat="server" CausesValidation="False" Font-Names="Arial"
                Font-Size="10pt" OnClick="DeactivateCustomer_Click" Text="Deactivate Customer"
                Visible="False" Width="150px" /></td>
                    <td align="center" class="style32">
                        <asp:Button ID="ActivateCustomer" runat="server" CausesValidation="False"
                Font-Names="Arial" Font-Size="10pt" Text="Activate Customer"
                Visible="False" Width="137px" OnClick="ActivateCustomer_Click" /></td>
                    <td class="style22">
            <asp:Button ID="RemoveCustomer" runat="server" CausesValidation="False" Font-Names="Arial"
                Font-Size="10pt" OnClick="RemoveCustomer_Click" Text="Remove Customer" Visible="False"
                Width="137px" Height="24px" /></td>
                    <td align="right">
                        <asp:Button ID="EmailCustomer" runat="server" CausesValidation="False" 
                            Font-Names="Arial" Font-Size="10pt"  
                            Text="Email This Customer" Visible="False" Width="150px" />
                    </td>
                </tr>
            </table>        
             <asp:Label ID="ActivationMessage" runat="server" Font-Bold="True" Font-Names="Arial"
                 Font-Size="10pt" ForeColor="Maroon" Width="511px"></asp:Label></td>
     </tr>
     <tr height="20">
         <td bgcolor="#CE5378" colspan="5" style="height: 20px" valign="middle">
             &nbsp;
             <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                 ForeColor="White" Text="Each Application"></asp:Label></td>
     </tr>
     <tr height="20">
         <td style="width: 404px; height: 20px" valign="top">
         </td>
         <td style="width: 295px; height: 20px" valign="top">
         </td>
         <td valign="top" class="style3" colspan="2">
         </td>
         <td style="height: 20px" valign="top">
         </td>
     </tr>
    <tr height=20>
        <td style="width: 404px; height: 34px;" valign="top" align="center">
            <asp:Label ID="ApplicationLabel" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Application" Visible="False" ForeColor="Black"></asp:Label></td>
        <td colspan="3" style="height: 34px" valign="top" align="left">
            <telerik:RadComboBox ID="Applications" runat="server" Font-Names="Arial" 
                Font-Size="10pt" AutoPostBack="True" 
                 Visible="False" 
                Width="289px" MarkFirstMatch="True" 
                onselectedindexchanged="Applications_SelectedIndexChanged">            </telerik:RadComboBox></td>
        <td valign="top" style="height: 34px;">
            &nbsp;</td>
    </tr>
    <tr>
        <td  style="width: 404px; height: 35px" valign="top" align="center">        
            <asp:Label ID="ApplicationStatusLabel" runat="server" Font-Names="Arial" Font-Size="10pt"
                ForeColor="Black" Text="Application Status" Visible="False" Width="117px"></asp:Label></td>
        <td  style="width: 295px; height: 35px" valign="top" align="center">        
            <asp:Label ID="ApplicationStatus" runat="server" Font-Names="Arial" Font-Size="10pt"
                 Visible="False" ForeColor="Black"></asp:Label></td>
        <td valign="top" align="left" class="style2" colspan="2">
                <asp:Button ID="ShowXmlDesign" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" Text="Show XML Design" Visible="False"
                    Width="132px" />
                </td>
        <td valign="top" style="height: 35px">
            &nbsp;</td>
    </tr>
     </table>

 <div align="center" style=" background-color:#bcbcbc; width:900px; vertical-align:top;">
                <table border="0" cellpadding="0" cellspacing="0">
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
</div>
    </form>
</body>
</html>
