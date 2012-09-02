<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TabPublishOld.aspx.cs" Inherits="PublishOld" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        	 font-family:Arial;
        	 font-size:11px;
        }
        
    .style6
    {
        height: 275px;
    }
    .style7
    {
        width: 196px;
    }
    .style17
    {
        height: 275px;
        width: 36px;
    }
    .style32
    {
        height: 39px;
    }
    .style33
    {
        height: 40px;
    }
    .style34
    {
        height: 40px;
        width: 37px;
    }
    .style35
    {
        height: 39px;
        width: 37px;
    }
    .style40
    {
        height: 47px;
        width: 218px;
    }
    .style66
    {
        height: 52px;
    }
    .style77
    {
        height: 52px;
        width: 36px;
    }
    .style78
    {
        height: 52px;
        width: 213px;
    }
    .style79
    {
        height: 52px;
        width: 27px;
    }
    .style80
    {
        height: 24px;
        width: 36px;
    }
    .style81
    {
        height: 24px;
    }
    .style82
    {
        height: 24px;
        width: 27px;
    }
    .style83
    {
        height: 26px;
        width: 36px;
    }
    .style84
    {
        height: 26px;
    }
    .style85
    {
        height: 26px;
        width: 27px;
    }
          .style89
          {
              height: 23px;
              width: 36px;
          }
          .style90
          {
              height: 23px;
          }
          .style91
          {
              height: 23px;
              width: 27px;
          }
          .style92
          {
              height: 47px;
              width: 36px;
          }
          .style93
          {
              height: 47px;
          }
          .style94
          {
              height: 47px;
              width: 213px;
          }
          .style95
          {
              height: 47px;
              width: 27px;
          }
      </style>
       <script  language="javascript" type="text/javascript" src="scripts/google_analytics_1.0.js"></script>
<script  language="javascript" type="text/javascript" src="scripts/default_script_1.6.js"></script> 
<script type="text/javascript">
    function validateDateTime(source, arguments) {
        arguments.IsValid = !isNaN(Date.parse(arguments.Value));
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">		
	</telerik:RadScriptManager>
	
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="ProvisionApps">
            <UpdatedControls>
               <telerik:AjaxUpdatedControl ControlID="ProvisionApps" LoadingPanelID="ProvisionAppsLoadingPanel"></telerik:AjaxUpdatedControl>
               <telerik:AjaxUpdatedControl ControlID="SeePublishingFormButton" ></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="ProvisionToStagingButton" ></telerik:AjaxUpdatedControl>
                 <telerik:AjaxUpdatedControl ControlID="ProvisionMessage" ></telerik:AjaxUpdatedControl>
                 <telerik:AjaxUpdatedControl ControlID="UploadProductionCredentials" ></telerik:AjaxUpdatedControl>
                 <telerik:AjaxUpdatedControl ControlID="ViewProductionCredentials" ></telerik:AjaxUpdatedControl>
                 
                 
          </UpdatedControls>
        </telerik:AjaxSetting>
        
         <telerik:AjaxSetting AjaxControlID="ProvisionToStagingButton">
            <UpdatedControls>
                  <telerik:AjaxUpdatedControl ControlID="ProvisionMessage" LoadingPanelID="ProvisionAppsLoadingPanel" ></telerik:AjaxUpdatedControl>
                  <telerik:AjaxUpdatedControl ControlID="SeePublishingFormButton" ></telerik:AjaxUpdatedControl>
                 <telerik:AjaxUpdatedControl ControlID="UploadProductionCredentials" ></telerik:AjaxUpdatedControl>
          </UpdatedControls>
        </telerik:AjaxSetting>

         <telerik:AjaxSetting AjaxControlID="RefreshProductionServices">
            <UpdatedControls>
                  <telerik:AjaxUpdatedControl ControlID="ProductionServices" LoadingPanelID="ProvisionAppsLoadingPanel" ></telerik:AjaxUpdatedControl>
                 <telerik:AjaxUpdatedControl ControlID="UploadProductionCredentials" ></telerik:AjaxUpdatedControl>
                 <telerik:AjaxUpdatedControl ControlID="ViewProductionCredentials" ></telerik:AjaxUpdatedControl>
          </UpdatedControls>
        </telerik:AjaxSetting>
       
        <telerik:AjaxSetting AjaxControlID="ProductionServices">
            <UpdatedControls>
                  <telerik:AjaxUpdatedControl ControlID="ProductionServices" LoadingPanelID="ProvisionAppsLoadingPanel" ></telerik:AjaxUpdatedControl>
                 <telerik:AjaxUpdatedControl ControlID="UploadProductionCredentials" ></telerik:AjaxUpdatedControl>
                 <telerik:AjaxUpdatedControl ControlID="ViewProductionCredentials" ></telerik:AjaxUpdatedControl>
          </UpdatedControls>
        </telerik:AjaxSetting>

    </AjaxSettings>
	</telerik:RadAjaxManager>

     <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <telerik:RadWindow 
                    id="ProvisioningProcess" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                    title="How Your Phone Is Provisioned"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>
                
            <telerik:RadWindow 
                    id="PageHelp" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                     Top="200px"
                     Left="200px"
                    title="Help on Publishing" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
           </telerik:RadWindow>
            </Windows>
            </telerik:RadWindowManager>
       
      <telerik:RadAjaxLoadingPanel ID="ProvisionAppsLoadingPanel" runat="server" Skin="Default" 
                                Transparency="0" BackColor="LightGray"  
                               ></telerik:RadAjaxLoadingPanel>
                                 <div align="center" id="header" style="height:60px;  background-color:#0054c2;">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>                
               
                <td><a href="http://ViziApps.com" style="text-decoration:none">
                <asp:Image ID="HeaderImage" ImageUrl="~/images/logo_header_300.png" runat="server" style="border:0px">
                                                            </asp:Image></a>
                </td>
               
                <td class="style45">
                   
                   
                    <asp:Label ID="UserLabel" runat="server" style="color:White"></asp:Label>
                    
                    </td>
                     <td style="color:White;"></td>
               
                <td align="center">
                    <asp:ImageButton ID="SupportButton" runat="server"  
                        ImageUrl="~/images/SupportButton.png" TabIndex="1000"  style=""/>
                </td>
                <td style="color:White;"></td>
                <td class="heading" align="center">
                    <asp:ImageButton ID="LogoutButton" runat="server"  
                        ImageUrl="~/images/LogoutButton.png" onclick="LogoutButton_Click" 
                        TabIndex="2000" style=""/>
                </td>
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
                       
                      <telerik:RadMenu ID="TabMenu" runat="server" Skin=""
                                  onitemclick="TabMenu_ItemClick"                                    
                             style="border-width: 0px; margin: 0px; padding: 0px; vertical-align:top; z-index:100; top: 0px; left: 0px;" 
                             TabIndex="1100"  >
                         
                            <Items>
                             <telerik:RadMenuItem ImageUrl="~/images/MySolutionsButton.png" HoveredImageUrl="~/images/MySolutionsButton_hov.png"
                        SelectedImageUrl="~/images/MySolutionsButton_sel.png"  Value="MySolutions" 
                                    TabIndex="1200"/>
                              <telerik:RadMenuItem ImageUrl="~/images/DisplayDesignButton.png" HoveredImageUrl="~/images/DisplayDesignButton_hov.png"
                        SelectedImageUrl="~/images/DisplayDesignButton_sel.png"  Value="DesignNative" TabIndex="1300" ><Items>
                                <telerik:RadMenuItem  ImageUrl="~/images/DisplayNativeDesignButton.png" HoveredImageUrl="~/images/DisplayNativeDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayNativeDesignButton_sel.png" Value="DesignNative"/>
                                <telerik:RadMenuItem  ImageUrl="~/images/DisplayWebDesignButton.png" HoveredImageUrl="~/images/DisplayWebDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayWebDesignButton_sel.png" Value="DesignWeb"/>
                                 <telerik:RadMenuItem  ImageUrl="~/images/DisplayHybridDesignButton.png" HoveredImageUrl="~/images/DisplayHybridDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayHybridDesignButton_sel.png" Value="DesignHybrid"/>
                                 </Items>
                             </telerik:RadMenuItem>
                            <telerik:RadMenuItem ImageUrl="~/images/ProvisionButton.png" HoveredImageUrl="~/images/ProvisionButton_hov.png"
                        SelectedImageUrl="~/images/ProvisionButton_sel.png"  Value="PublishOld" Selected="true" TabIndex="1500"/>
                            <telerik:RadMenuItem ImageUrl="~/images/FAQButton.png" HoveredImageUrl="~/images/FAQButton_hov.png"
                        SelectedImageUrl="~/images/FAQButton_sel.png"  Value="FAQ" TabIndex="1600"/>
                       <telerik:RadMenuItem ImageUrl="~/images/MyProfileButton.png" HoveredImageUrl="~/images/MyProfileButton_hov.png"
                        SelectedImageUrl="~/images/MyProfileButton_sel.png"  Value="MyProfile" TabIndex="1700"/>
                       

                         </Items>
                          </telerik:RadMenu>
                      
                       
                          </td></tr>
                          </table>
                        
                    </div>
                </td></tr>
                </table>   
               
                </div>       
     <div align="center" style="width:100%">
             <div align="center" style=" background-color:#bcbcbc; width:900px; vertical-align:top;">
                <table border="0" cellpadding="0" cellspacing="0">
                 <tr style="height:16px">
                             <td style="height:16px;width:16px;  background-image:url(images/round_top_left.png); background-position:right bottom; background-repeat:no-repeat;">
                                   
                             </td>
                             <td style="background-image:url(images/white_square.png); width:868px; height:16px; background-repeat:repeat">
                             
                             </td>
                             <td style="height:16px;width:16px;background-image:url(images/round_top_right.png); background-position:left bottom; background-repeat:no-repeat; ">
                              </td>
                         </tr>
                </table>
                </div>
                <div style="width: 900px; height: 445px; background-color:White">
                    <table  bgcolor="White" style="width: 900px; height: 441px;" cellpadding="0" 
                        cellspacing="0">
                        <tr>
                            <td valign="middle" class="style80">
                               
                                </td>
                            <td valign="middle" class="style81" bgcolor="#0054c2" colspan="3" align="left">
                               
                            &nbsp;&nbsp;&nbsp;
    
     <asp:Label ID="Label1" runat="server" Text="Testing and Publishing" 
                Font-Bold="True"  ForeColor="White" 
                style="font-family:Verdana; font-size:12px;"></asp:Label>
                            </td>
                            <td class="style82" align="left">
                               
                                </td>
                            <td align="left" rowspan="6" valign="top">
                            <table style="height: 186px; width: 242px;">
                            <tr>
                            <td valign="top" class="style33">
                               
                               <asp:Button ID="SeeDataModel" runat="server" CausesValidation="False" 
                                   Font-Names="Arial" Font-Size="10pt" OnClientClick="var oWin=radopen('Help/Publish/ViewProvisioningProcess.htm', 'ProvisioningProcess'); 
                                    oWin.set_visibleTitlebar(true);
                       oWin.set_visibleStatusbar(false);
                       oWin.set_modal(true); 
                                   oWin.moveTo(100, 0); 
                                   oWin.setSize(850,875);
                                   return false;"
                                   Text="Testing and Publishing Your App" Width="210px" />
                               
                            </td>
                            <td valign="top" class="style34" align="center">
                               
            <asp:ImageButton ID="ProvisioningHelp" runat="server" ImageUrl="~/images/help.gif" 
                ToolTip="What is this page for?" OnClientClick="var oWin=radopen('Help/Publish/ProvisionHelp.aspx', 'PageHelp'); 
                oWin.setSize(800,750);
                         oWin.set_visibleTitlebar(true);
                       oWin.set_visibleStatusbar(false);
                       oWin.set_modal(true); 
                        oWin.moveTo(100, 0); 
                                       return false;"/>
                               
                            </td>
                            </tr>
                             <tr>
                            <td valign="top" class="style33">
                               
                                            <asp:Button ID="PurchaseButton" runat="server" 
                                                CausesValidation="False" style="font-family:Verdana; font-size:12px;"                                                
                                                Text="Purchase ViziApps Services" Width="210px" 
                                                onclick="PurchaseButton_Click" />
                               
                            </td>
                            <td valign="top" class="style34" align="center">
                               
            <asp:ImageButton ID="PaymentProcessHelp" runat="server" ImageUrl="~/images/help.gif" 
                ToolTip="What is the payment process for production?" OnClientClick="var oWin=radopen('Help/Publish/PaymentHelp.aspx', 'PageHelp'); 
                oWin.setSize(800,400);
                         oWin.set_visibleTitlebar(true);
                       oWin.set_visibleStatusbar(false);
                       oWin.set_modal(true); 
                        oWin.moveTo(100, 50); 
                                       return false;"/>
                               
                            </td>
                            </tr>
                             <tr>
                            <td class="style33" valign="top">
                               
                                            <asp:Button ID="ManageBillingButton" runat="server" 
                                                CausesValidation="False" style="font-family:Verdana; font-size:12px;"                                                
                                                Text="Manage Your Billing" Width="210px" 
                                                onclick="ManageBillingButton_Click" />
                               
                            </td>
                            <td class="style34" valign="top" align="center">
                               
            <asp:ImageButton ID="PaymentProcessHelp0" runat="server" ImageUrl="~/images/help.gif" 
                ToolTip="How to Change or Cancel your ViziApps Subscriptions?" OnClientClick="var oWin=radopen('Help/Publish/ChangingProductionService.aspx', 'PageHelp'); 
                oWin.setSize(800,430);
                         oWin.set_visibleTitlebar(true);
                       oWin.set_visibleStatusbar(false);
                       oWin.set_modal(true); 
                        oWin.moveTo(100, 100); 
                                       return false;"/>
                               
                                 </td>
                            </tr>
                             <tr>
                            <td valign="top" class="style32">
                                &nbsp;</td>
                            <td valign="top" class="style35">
                                &nbsp;</td>
                            </tr>
                             </table>
                               
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" class="style83">
                               
                                &nbsp;&nbsp;</td>
                            <td valign="middle" class="style84" colspan="3" align="right">
                               
                                <asp:Label ID="ProvisionMessage" runat="server" Font-Bold="True" 
                                    style="font-family:Verdana; font-size:12px;" ForeColor="Maroon" 
                                    Height="16px" Width="527px"></asp:Label>
                            </td>
                            <td class="style85" align="left" height="26">
                               
                                            &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="middle" class="style77">
                               
                                &nbsp;&nbsp; </td>
                            <td valign="middle" class="style66" colspan="2">
                               
                            <table><tr><td valign="middle" class="style7">
         <asp:Label ID="Label2" runat="server" Text="Current App" 
                                    ForeColor="#000099" Font-Bold="True" 
                                    style="font-family:Verdana; font-size:12px;" Width="100px"></asp:Label>
                                    </td><td align="right"><telerik:RadComboBox ID="ProvisionApps" runat="server" Font-Names="Arial" 
                                                    AutoPostBack="True" ForeColor="Black" Width="250px" style="font-family:Verdana; font-size:12px;"
                                                    onselectedindexchanged="ProvisionApps_SelectedIndexChanged" ></telerik:RadComboBox></td></tr></table>                                                
                            </td>
                            <td class="style78" align="right" style="font-family:Verdana; font-size:12px;">
                                
                                            <asp:Button ID="ProvisionToStagingButton" 
                runat="server" CausesValidation="False" 
                                                 OnClick="ProvisionToStagingButton_Click" 
                                                Text="Select App For Testing" 
                Width="195px" />
                            </td>
                            <td class="style79" align="left" height="52">
                               
                                            &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="middle" class="style92">
                               
                                </td>
                            <td valign="middle" class="style93" align="right" 
                                style="font-family:Verdana; font-size:12px;">
                               
                                            &nbsp;</td>
                            <td valign="middle" class="style40" align="right" style="font-family:Verdana; font-size:12px;">
                               
                                            &nbsp;</td>
                            <td class="style94" align="right" style="font-family:Verdana; font-size:12px;">
                                
                                            <asp:Button ID="SeePublishingFormButton" runat="server" 
                                                CausesValidation="False" 
                                                Text="See Publishing Form" Width="195px" />
                               
                            </td>
                            <td class="style95" align="left">
                               
                                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" class="style89">
                               
                                </td>
                            <td valign="middle" class="style90" align="left" 
                                style="font-family:Verdana; font-size:12px;" colspan="3" 
                                title="List of Paid ViziApps Services">
                               
         <asp:Label ID="Label3" runat="server" Text="List of Paid ViziApps Services in Your Account" 
                                    ForeColor="#000099" Font-Bold="True" 
                                    style="font-family:Verdana; font-size:12px;" Width="347px"></asp:Label>
                            </td>
                            <td class="style91" align="left">
                               
                                            </td>
                        </tr>
                        <tr>
                            <td class="style17" align="center">
                                </td>
                            <td class="style6" colspan="3" align="left" valign="top">
                               
                               <telerik:RadGrid ID="ProductionServices" 
                               OnSortCommand="ProductionServices_SortCommand" 
                               OnPageIndexChanged="ProductionServices_PageIndexChanged" 
                                ItemStyle-Font-Size="14px"  
                                 AlternatingItemStyle-Font-Size= "14px"
                                 SelectedItemStyle-Font-Size  = "14px"                          
                                Width="98%" 
                                 AllowSorting="True" 
                                 PageSize="8" 
                                 AllowPaging="True" 
                                 AllowMultiRowSelection="True" 
                                 runat="server"
                                 Skin="Telerik"
                                  HeaderStyle-Font-Size="16px"
                                  Gridlines="None" Height="271px" >
                             <PagerStyle Mode="NextPrevAndNumeric" />
                             <ClientSettings EnableRowHoverStyle="true"  >
                                <Selecting AllowRowSelect="true"   />
                            </ClientSettings>
                            <MasterTableView>
                               <Columns></Columns>
                            </MasterTableView>

                            </telerik:RadGrid>
                            
                            </td>
                            <td class="style6" align="left" height="275">
                               
                                            </td>
                        </tr>
                        </table>
    </div>
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
     <div align="center" id="CopyRight" runat="server"></div>
</body>
</html>
