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
        	         background-color:#ffffff;
                }
                .style12
                {
                    width: 138px;
                }
                .style13
                {
                    width: 317px;
                }
                .style27
                {
                    height: 33px;
                }
                .style28
                {
                    height: 34px;
                }
                .style30
                {
                    width: 130px;
                }
                .style31
                {
                    width: 135px;
                }
                .style37
                {
                    width: 160px;
                    height: 35px;
                }
                .style38
                {
                    width: 217px;
                    height: 35px;
                }
                .style39
                {
                    width: 184px;
                }
                .style40
                {
                    width: 217px;
                }
                .style42
                {
                    width: 160px;
                     height: 35px;
                }
                .style43
                {
                    width: 133px;
                }
                .style44
                {
                    width: 159px;
                    height: 35px;
                }
                .style45
                {
                    height: 31px;
                }
                .style46
                {
                    width: 174px;
                    height: 35px;
                }
                .style47
                {
                    height: 35px;
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
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings> 
  
            <telerik:AjaxSetting AjaxControlID="EmailUpgradeNotice">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Message"/>                       
                </UpdatedControls>
            </telerik:AjaxSetting>  

           <telerik:AjaxSetting AjaxControlID="UpdateImageListing">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Message"/>                       
                </UpdatedControls>
            </telerik:AjaxSetting>  

            <telerik:AjaxSetting AjaxControlID="UpdatePassword">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="AdminMessage"/>                       
                </UpdatedControls>
            </telerik:AjaxSetting>  

          <telerik:AjaxSetting AjaxControlID="UpdateExpirationDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="AdminMessage"/>                       
                </UpdatedControls>
            </telerik:AjaxSetting>  

          <telerik:AjaxSetting AjaxControlID="UpdateAccountTypes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="AdminMessage"/>                       
                </UpdatedControls>
            </telerik:AjaxSetting>  
            
         <telerik:AjaxSetting AjaxControlID="DeactivateCustomer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="AdminMessage"/>   
                                        
                </UpdatedControls>
            </telerik:AjaxSetting>  

        <telerik:AjaxSetting AjaxControlID="ActivateCustomer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="AdminMessage"/>                       
                </UpdatedControls>
            </telerik:AjaxSetting>  

       <telerik:AjaxSetting AjaxControlID="RemoveCustomer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="AdminMessage"/>     
                  <telerik:AjaxUpdatedControl ControlID="LoginPanel"/>                       
                    <telerik:AjaxUpdatedControl ControlID="AccountPanel"/>  
                     <telerik:AjaxUpdatedControl ControlID="AppPanel"/>                     
                   <telerik:AjaxUpdatedControl ControlID="CustomersByEmail"/>                       
                      <telerik:AjaxUpdatedControl ControlID="CustomersByAccount"/>                       
               </UpdatedControls>
            </telerik:AjaxSetting>  
            
      <telerik:AjaxSetting AjaxControlID="CustomersByAccount">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="CustomersByEmail"/>                       
                      <telerik:AjaxUpdatedControl ControlID="CustomersByAccount"/>                       
                  <telerik:AjaxUpdatedControl ControlID="LoginPanel"/>                       
                    <telerik:AjaxUpdatedControl ControlID="AccountPanel"/>  
                     <telerik:AjaxUpdatedControl ControlID="AppPanel"/>    
                     <telerik:AjaxUpdatedControl ControlID="RegisteredDateTime"/> 
                      <telerik:AjaxUpdatedControl ControlID="LastUsedDateTime"/>    
                       <telerik:AjaxUpdatedControl ControlID="AdminMessage"/>                                        
                </UpdatedControls>
       </telerik:AjaxSetting>  
 
       <telerik:AjaxSetting AjaxControlID="CustomersByEmail">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="CustomersByEmail"/>                       
                     <telerik:AjaxUpdatedControl ControlID="CustomersByAccount"/>                       
                   <telerik:AjaxUpdatedControl ControlID="LoginPanel"/>                       
                    <telerik:AjaxUpdatedControl ControlID="AccountPanel"/>  
                     <telerik:AjaxUpdatedControl ControlID="AppPanel"/>                        
                     <telerik:AjaxUpdatedControl ControlID="RegisteredDateTime"/> 
                      <telerik:AjaxUpdatedControl ControlID="LastUsedDateTime"/>   
                       <telerik:AjaxUpdatedControl ControlID="AdminMessage"/>                                         
                </UpdatedControls>
       </telerik:AjaxSetting>  
 
       <telerik:AjaxSetting AjaxControlID="Applications">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Applications"/>                       
                    <telerik:AjaxUpdatedControl ControlID="OneAppPanel"/>                       
                </UpdatedControls>
       </telerik:AjaxSetting>  
            
        </AjaxSettings>
        </telerik:RadAjaxManager>
            
     
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
 
    <div  style="height:10px">
                </div>
 
    <div style="height: 20px; background-color:#680792; text-align:left" >
           
            &nbsp;&nbsp;&nbsp;&nbsp;
           
            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                ForeColor="White" Text="All Acounts and Applications"></asp:Label>
    </div>
    <div style="height:10px"></div>
    <div align="center">
 
            <table style="width: 861px; ">
            <tr>
            <td class="style27" align="left">
                <asp:Button ID="ViewCurrentUsers" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" Text="View Current Users" 
                    Width="192px" />
                </td>
            <td align="center">
                <asp:Button ID="ViewActiveCustomers" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt"  
                    Text="View Active Users" Width="148px" 
                      />
                </td>
            <td class="style27">
                <asp:Button ID="EmailUpgradeNotice" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" Text="Email Maintenance Notice" 
                    Width="167px" onclick="EmailUpgradeNotice_Click"  />
                </td>
            <td class="style27">
                <asp:Button ID="EmailCustomers" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" Text="Email Customers" Width="140px" 
                     />
                </td>
            </tr>
            <tr>
            <td class="style28" align="left">
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
            <td class="style28" align="center">
                <asp:Button ID="UpdateImageListing" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" Text="Update Image Listing" 
                    Width="148px" onclick="UpdateImageListing_Click" />
                </td>
            <td class="style28">
                &nbsp;</td>
            <td class="style28">
                </td>
            </tr>
            </table>
           
            </div>
            <div align="center">            
                <asp:Label ID="Message" runat="server" Font-Bold="True" Font-Italic="False" 
                Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Height="23px" 
                Width="812px"></asp:Label>
               </div>
  
     <div style=" background-color:#D97540; text-align:left">
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Arial" 
                 Font-Size="10pt" ForeColor="White" Text="Each Account"></asp:Label>
         </div>
          <div style="height:10px"></div>
          <div align="center" style="height: 25px">
              <asp:Label ID="AdminMessage" runat="server" Font-Bold="True" 
                 Font-Italic="False" Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" 
                 Height="16px" Width="672px"></asp:Label>
          </div>
          <div align="center" >
         
              <table style="width:920px;">
                  <tr>
                      <td align="left" class="style37">
                         
                      <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10pt" 
                 ForeColor="Black" Text="Customer by Account" Width="131px" Height="16px"></asp:Label>
                      </td>
                      <td class="style38" align="left">
             <telerik:RadComboBox ID="CustomersByAccount" runat="server" AutoPostBack="True" 
                 Font-Names="Arial" Font-Size="10pt" 
                  Width="200px" 
                 MarkFirstMatch="True" 
                 onselectedindexchanged="CustomersByAccount_SelectedIndexChanged">
             </telerik:RadComboBox>
                         
                      </td>
                      <td rowspan="2" valign="top" align="left">
                          <div ID="LoginPanel" runat="server"  style="display:block">            
            
                      <table style="height: 70px; width: 524px;" align="left">
                 <tr>
                     <td align="left" class="style43" valign="top">
                 <asp:Button ID="LoginToUserbyAccount" runat="server" CausesValidation="False" 
                     Font-Names="Arial" Font-Size="10pt" Text="Login To User"  
                     Width="126px" onclick="LoginToUserbyAccount_Click"  />
                     </td>
                     <td align="left" class="style39">
                 <asp:Label ID="RegisteredDateTime" runat="server"  style="font-size:10px"
                     ></asp:Label>
                     </td>
                     <td align="left">
                 <asp:Label ID="LastUsedDateTime" runat="server" style="font-size:10px"
                     ></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td align="left" class="style43" valign="top">
                 <asp:Button ID="LoginToUserbyEmail" runat="server" CausesValidation="False" 
                     Font-Names="Arial" Font-Size="10pt" Text="Login To User"  
                     Width="126px" onclick="LoginToUserbyEmail_Click" />
                     </td>
                     <td align="right" class="style39" valign="top">
                        <asp:Button ID="EmailCustomer" runat="server" CausesValidation="False" 
                            Font-Names="Arial" Font-Size="10pt"  
                            Text="Email This Customer"  Width="150px" />
                     </td>
                     <td>
                         </td>
                 </tr>
             </table>
              </div> 
                      </td>
                  </tr>
                  <tr>
                      <td align="left" class="style42">
                          
                      <asp:Label ID="Label25" runat="server" Font-Names="Arial" Font-Size="10pt" 
                 ForeColor="Black" Text="Customer by Email" Width="131px"></asp:Label>
                      </td>
                      <td class="style40" align="left">
             <telerik:RadComboBox ID="CustomersByEmail" runat="server" AutoPostBack="True" 
                 Font-Names="Arial" Font-Size="10pt" 
                  Width="200px" 
                 MarkFirstMatch="True" 
                 onselectedindexchanged="CustomersByEmail_SelectedIndexChanged">
             </telerik:RadComboBox>
                          &nbsp;
                      </td>
                  </tr>
                  </table>
             
              </div>
    <div id="AccountPanel" runat="server"  style="display:block" 
        align="center">
             <table style="width:920px;">
                 <tr>
                     <td class="style44" align="left">
            <asp:Label ID="PasswordLabel" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Black"
                Text="Password" ></asp:Label>
                     </td>
                     <td align="left" class="style47">
            <asp:TextBox ID="Password" runat="server" Font-Names="Arial" Font-Size="10pt" 
                 Width="200px"></asp:TextBox>
                     </td>
                     <td align="left" class="style47">
            <asp:Button ID="UpdatePassword" runat="server" CausesValidation="False" Font-Names="Arial"
                Font-Size="10pt" Text="Update Password"
                 Width="156px" OnClick="UpdatePassword_Click" />
                     </td>
                      <td class="style46">
                 <asp:Button ID="ViewUserProfile" runat="server" CausesValidation="False" 
                     Font-Names="Arial" Font-Size="10pt" Text="View User Profile"  
                     Width="156px" />
                     </td>
                     <td align="left" colspan="2" class="style47">
                        
                        </td>
                 </tr>
                 <tr>
                     <td class="style44" align="left">
             <asp:Label ID="AccountTypesLabel" runat="server" Font-Names="Arial" Font-Size="10pt"
                 ForeColor="Black" Text="Customer Account Types " ></asp:Label> </td>
                     <td align="left" class="style47">
             <asp:TextBox ID="AccountTypes" runat="server" Font-Names="Arial" Font-Size="10pt"
                  Width="200px"></asp:TextBox></td>
                     <td align="left" class="style47">
            <asp:Button ID="UpdateAccountTypes" runat="server" CausesValidation="False" Font-Names="Arial"
                Font-Size="10pt" Text="Update Account Types"
                 Width="156px" onclick="UpdateAccountTypes_Click"   />
                     </td>
                     <td align="left" class="style46">
             <asp:Label ID="AccountTypesNote" runat="server" Font-Names="Arial" Font-Size="10pt"
                 ForeColor="Black" Text="(list separated by commas)" ></asp:Label> </td>
                     <td align="left" colspan="2" class="style47">
                         </td>
                 </tr>
                 <tr>
                     <td class="style44" align="left">
             <asp:Label ID="CustomerStatusLabel" runat="server" Font-Names="Arial" Font-Size="10pt"
                 ForeColor="Black" Text="Customer Status: " ></asp:Label> </td>
                     <td align="left" class="style47">
             <asp:TextBox ID="CustomerStatus" runat="server" Font-Names="Arial" Font-Size="10pt"
                  Width="200px"></asp:TextBox></td>
                     <td align="left" class="style47">
            <asp:Button ID="DeactivateCustomer" runat="server" CausesValidation="False" Font-Names="Arial"
                Font-Size="10pt" OnClick="DeactivateCustomer_Click" Text="Deactivate Customer"
                 Width="156px" /></td>
                     <td class="style46">
                        <asp:Button ID="ActivateCustomer" runat="server" CausesValidation="False"
                Font-Names="Arial" Font-Size="10pt" Text="Activate Customer"
                 Width="156px" OnClick="ActivateCustomer_Click" /></td>
                     <td class="style47">
            <asp:Button ID="RemoveCustomer" runat="server" CausesValidation="False" Font-Names="Arial"
                Font-Size="10pt" OnClick="RemoveCustomer_Click" Text="Remove Customer" 
                Width="157px" Height="24px" /></td>
                     <td class="style47">
                         </td>
                 </tr>
             </table>
             </div>
     <div style=" background-color:#CE5378; text-align:left" >
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                 ForeColor="White" Text="Each Application"></asp:Label></div>
                  <div style="height:10px"></div>
                  <div align="center">
     <table style="width: 963px">
     <tr height="20">
         <td style="height: 20px" valign="top">
             <div id="AppPanel" runat="server" style="display:block" align="center">
                 <table style="width: 920px;">
                     <tr>
                         <td class="style12" align="left">
                             &nbsp;
                         <asp:Label ID="ApplicationLabel" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Application"  ForeColor="Black"></asp:Label>
                         </td>
                         <td align="left" class="style13">
            <telerik:RadComboBox ID="Applications" runat="server" Font-Names="Arial" 
                Font-Size="10pt" AutoPostBack="True" 
                  
                Width="289px" MarkFirstMatch="True" 
                onselectedindexchanged="Applications_SelectedIndexChanged">            </telerik:RadComboBox>&nbsp;
                         </td>
                         <td>
                             &nbsp;
                         </td>
                     </tr>
                     </table>
                     <div id="OneAppPanel" runat="server" style="display:block">
                     <table style="width: 920px"><tr><td align="left" class="style30">                          
                         <asp:Label ID="ApplicationStatusLabel" runat="server" Font-Names="Arial" Font-Size="10pt"
                ForeColor="Black" Text="Application Status"  Width="117px"></asp:Label>
                         </td>
                         <td align="left" class="style31">
            <asp:Label ID="ApplicationStatus" runat="server" Font-Names="Arial" Font-Size="10pt"
                  ForeColor="Black"></asp:Label>&nbsp;
                         </td>
                         <td align="left">
                             &nbsp;
                         <asp:Button ID="ShowXmlDesign" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" Text="Show XML Design" 
                    Width="132px" />
                         </td>
                     </tr>
                     </table>
                     </div>
             </div></td>
     </tr>
     </table>

</div>
    </form>
</body>
</html>
