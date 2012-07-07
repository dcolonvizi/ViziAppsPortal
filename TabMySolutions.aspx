<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TabMySolutions.aspx.cs" Inherits="TabMySolutions" %>
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
        }
        
    .style4
    {
        height: 37px;
    }
     .config_menu
    {
        height: 55px;
         width: 75px;
         white-space:pre-wrap;
         overflow:visible;
    }
          .style5
          {
              width: 20px;
              height: 22px;
          }
          .style6
          {
              height: 22px;
          }
          .style16
          {
              width: 19px;
              height: 44px;
          }
          .style17
          {
              width: 19px;
          }
          .style18
          {
              width: 301px;
          }
          .style19
          {
              width: 65px;
          }
          .style20
          {
              width: 98px;
          }
          </style>
     <script  language="javascript" type="text/javascript" src="scripts/google_analytics_1.0.js"></script>
     <script  language="javascript" type="text/javascript" src="scripts/default_script_1.6.js"></script> 
        <script  language="javascript" type="text/javascript" src="scripts/dialogs_1.26.min.js""></script>
        <script  language="javascript" type="text/javascript" src="scripts/browser_1.4.js"></script>
<script type="text/javascript">
    var oldgridSelectedColor;

    function setMouseOverColor(element) {

        oldgridSelectedColor = element.style.backgroundColor;
        element.style.backgroundColor = '#ffff90';
        element.style.cursor = 'hand';
    }

    function setMouseOutColor(element) {

        element.style.backgroundColor = oldgridSelectedColor;
        element.style.textDecoration = 'none';
    }

    function onDeleteProductionApplication(item) {
        if (confirm('Are you sure you want to delete this production application?')) {
            Form1.deleting_production_application.value = item;
            return true;
        }
        else {
            Form1.deleting_production_application.value = '';
            return false;
        }
    }
    function OnRowSelected(sender, eventArgs) {
        var button = document.getElementById('RowSelected');
        var row_index = eventArgs.get_itemIndexHierarchical();
        var index = document.getElementById('SelectedRowIndex');
        index.value = row_index;
        button.click();
    }
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">		
	</telerik:RadScriptManager>
	
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
     <AjaxSettings>
            
        <telerik:AjaxSetting AjaxControlID="CopyTemplateAppToAccount">
         <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="MySolutions" LoadingPanelID="MySolutionsLoadingPanel"/>
             <telerik:AjaxUpdatedControl ControlID="Message" />
         </UpdatedControls>
       </telerik:AjaxSetting>

    <telerik:AjaxSetting AjaxControlID="MySolutions">
         <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="MySolutions" LoadingPanelID="MySolutionsLoadingPanel"/>
            <telerik:AjaxUpdatedControl ControlID="Message" />

         </UpdatedControls>
       </telerik:AjaxSetting>

     <telerik:AjaxSetting AjaxControlID="RowSelected">
         <UpdatedControls>
            <telerik:AjaxUpdatedControl ControlID="MultiPage1" LoadingPanelID="MySolutionsLoadingPanel"/>
             <telerik:AjaxUpdatedControl ControlID="TabMenu"/>
         </UpdatedControls>
       </telerik:AjaxSetting>    
       </AjaxSettings>   
	</telerik:RadAjaxManager>

    
    <telerik:RadWindowManager ID="MySolutionsRadWindowManager" runat="server">
            <Windows>
             <telerik:RadWindow 
                    id="AddTemplateAppBox" 
                    runat="server"
                     DestroyOnClose="true"                   
                     ShowContentDuringLoad="true"
                   title="Add Template App" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="false"
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
                    title="Help on MyApps" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>

                 <telerik:RadWindow 
                    id="QuickStartBox" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                     Top="200px"
                     Left="200px"
                    title="Get Started in 1 Minute" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>

                 <telerik:RadWindow 
                    id="YourFirstAppBox" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                     Top="200px"
                     Left="200px"
                    title="Your First App" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>
               
                <telerik:RadWindow 
                    id="NameTemplateApp" 
                    runat="server"
                    Modal="true"
                   DestroyOnClose="true"
                    showcontentduringload="false"
                    title="Your Application Name" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="false"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>
                
            </Windows>
            </telerik:RadWindowManager>
    <div align="center" id="header" style="height:80px;width:100%;  background-color:#0054c2;">
    <div style="height:10px;"></div>
                <table cellpadding="0" cellspacing="0" border="0" style="width: 1122px">
                <tr>                
               
                <td style="width:372px"><a href="http://ViziApps.com" style="text-decoration:none">
                <asp:Image ID="HeaderImage" ImageUrl="~/images/logo_header_300.png" runat="server" style="border:0px">
                                                            </asp:Image></a>
                   
                    </td>
               <td> <table style="width: 410px" id="InfoTable" runat="server" ><tr><td>
                   <asp:ImageButton ID="DemoVideo" runat="server" CausesValidation="False" 
                       Height="55px" Width="75px"                        
                        CssClass="config_menu" ImageUrl="~/images/demo_video_button.png"  
                        />
                   </td><td>
                   <asp:ImageButton ID="Overview" runat="server" CausesValidation="False"                        
                        CssClass="config_menu" Height="55px" Width="75px" ImageUrl="~/images/viziapps_overview_button.png" 
                        />
                   </td><td>
                   <asp:ImageButton ID="QuickStart" runat="server" CausesValidation="False" 
                       Height="55px" Width="75px"                       
                        CssClass="config_menu" 
                       ImageUrl="~/images/1_minute_quick_start_button.png" />
                   </td><td>
                   <asp:ImageButton ID="YourFirstApp" runat="server" CausesValidation="False" 
                       Height="55px" Width="75px"                       
                        CssClass="config_menu" ImageUrl="~/images/your_first_app_button.png" />
                   </td></tr></table></td>
                <td class="style20">
                   
                   
                    <asp:Label ID="UserLabel" runat="server" style="color:White"></asp:Label>
                    
                    </td>
                     <td style="color:White;"></td>
               
                <td align="center" class="style19">
                    <asp:ImageButton ID="SupportButton" runat="server"  
                        ImageUrl="~/images/SupportButton.png" TabIndex="1000"  style=""/>
                </td>
                <td style="color:White;"></td>
                <td class="heading" align="center">
                    <asp:ImageButton ID="LogoutButton" runat="server"  
                        ImageUrl="~/images/LogoutButton.png" onclick="LogoutButton_Click" 
                        TabIndex="2000" style="height: 18px"/>
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
                             style="border-width: 0px; margin: 0px; padding: 0px; vertical-align:top; z-index:100;" 
                             TabIndex="1100"  >
                         
                            <Items>
                             <telerik:RadMenuItem ImageUrl="~/images/MySolutionsButton.png" HoveredImageUrl="~/images/MySolutionsButton_hov.png"
                        SelectedImageUrl="~/images/MySolutionsButton_sel.png"  Value="MySolutions" Selected="true"
                                    TabIndex="1200"/>
                             <telerik:RadMenuItem ImageUrl="~/images/DisplayDesignButton.png" HoveredImageUrl="~/images/DisplayDesignButton_hov.png"
                        SelectedImageUrl="~/images/DisplayDesignButton_sel.png"  Value="DesignNative" TabIndex="1300" ><Items>
                                <telerik:RadMenuItem  ImageUrl="~/images/DisplayNativeDesignButton.png" HoveredImageUrl="~/images/DisplayNativeDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayNativeDesignButton_sel.png" Value="DesignNative"/>
                                <telerik:RadMenuItem  ImageUrl="~/images/DisplayWebDesignButton.png" HoveredImageUrl="~/images/DisplayWebDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayWebDesignButton_sel.png" Value="DesignWeb"/>
                                 <telerik:RadMenuItem  ImageUrl="~/images/DisplayHybridDesignButton.png" HoveredImageUrl="~/images/DisplayHybridDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayHybridDesignButton_sel.png" Value="DesignHybrid"/>
                                 </Items>
                             </telerik:RadMenuItem>
                           
                            <telerik:RadMenuItem ImageUrl="~/images/ProvisionButton.png" HoveredImageUrl="~/images/ProvisionButton_hov.png"
                        SelectedImageUrl="~/images/ProvisionButton_sel.png"  Value="Publish" TabIndex="1500"/>
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
              
                          
                <div ID="NormalView" runat="server"  align="center" style="text-align:-moz-center">

<table><tr><td valign="top">
        <div align="center" style=" background-color:#bcbcbc; width:900px; vertical-align:top;">
                <table border="0" cellpadding="0" cellspacing="0">
                 <tr style="height:16px">
                             <td style="height:16px;width:16px;  background-image:url(images/round_top_left.png); background-position:right bottom; background-repeat:no-repeat;" 
                                 valign="top">
                                   
                             </td>
                             <td style="background-image:url(images/white_square.png); width:868px; height:16px; background-repeat:repeat">
                             
                             </td>
                             <td style="height:16px;width:16px;background-image:url(images/round_top_right.png); background-position:left bottom; background-repeat:no-repeat; ">
                              </td>
                         </tr>
                </table>
                </div>

<table style="width: 900px" border="0" cellpadding="0" cellspacing="0" 
    bgcolor="White">
    <tr>
    <td class="style5">
    </td>
     <td bgcolor="#0054c2" colspan="4" align="left" class="style6">
            
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                ForeColor="#ffffff" Text="My Current Mobile Apps" Width="216px"></asp:Label>
                   
    </td>
     <td align="center" class="style6">
            
                   </td>
     <td align="right" class="style6">
            <asp:ImageButton ID="MySolutionsHelp" runat="server" ImageUrl="~/images/help.gif" 
                ToolTip="What is this page for?" OnClientClick="var oWin=radopen('Help/MySolutions/MySolutionsHelp.htm', 'PageHelp'); 
                oWin.setSize(400,200);
                 oWin.set_visibleTitlebar(true);
                       oWin.set_visibleStatusbar(false);
                       oWin.set_modal(true); 
                        oWin.moveTo(400, 200); 
                return false;"/>
    </td>
     <td class="style5">
    </tr>
     <tr>
    <td class="style4">
    </td>
     <td class="style4" align="left" colspan="4">
                            <asp:Label ID="Message" runat="server" Font-Bold="True" 
             Font-Names="Arial" Font-Size="10pt"
                                Font-Underline="False" ForeColor="Maroon" Width="672px"></asp:Label>
    </td>
     <td class="style4">
         </td>
     <td class="style4">
    </td>
     <td class="style4">
         </td>
    </tr>
     <tr>
    <td>
    </td>
     <td colspan="6" style=" font-family:Verdana; font-size:14px; color:#222222">
                          
                            <telerik:RadAjaxLoadingPanel ID="MySolutionsLoadingPanel" runat="server" />
                              
                               <telerik:RadGrid ID="MySolutions" 
                               CssClass="MySolutionsClass"
                                ActiveItemStyle-CssClass="MySolutionsClass"
                                 MasterTableView-CssClass="MySolutionsClass"                                  
                               OnSortCommand="MySolutions_SortCommand" 
                               OnPageIndexChanged="MySolutions_PageIndexChanged" 
                                ItemStyle-CssClass="MySolutionsClass"
                                 AlternatingItemStyle-CssClass="MySolutionsClass"
                                 SelectedItemStyle-CssClass="MySolutionsClass"     
                                 OnItemDeleted="MySolutions_ItemDeleted"                    
                                Width="96%" 
                                 AllowSorting="True" 
                                 PageSize="50" 
                                 OnPageSizeChanged="MySolutions_PageSizeChanged"
                                 AllowPaging="True" 
                                 AllowMultiRowSelection="True" 
                                 runat="server"
                                   HeaderStyle-Font-Size="16px"
                                   Font-Names="Verdana"
                                    Font-Size="14px"
                                  Gridlines="None" 
                                style=" font-family:Verdana; font-size:14px; color:#222222">
                             <PagerStyle Mode="NextPrevAndNumeric" />
                             <ClientSettings EnableRowHoverStyle="true"  ClientEvents-OnRowSelected="OnRowSelected">
                                <Selecting AllowRowSelect="true"   />
                            </ClientSettings>
                            <MasterTableView>
                               <Columns>      
                               </Columns>
                            </MasterTableView>

                            </telerik:RadGrid>
                            
    </td>
     <td>
         &nbsp;</td>
    </tr>
     <tr>
    <td>
        &nbsp;</td>
     <td colspan="6">
                          
                            &nbsp;</td>
     <td>
         &nbsp;</td>
    </tr>
     <tr>
    <td>
    </td>
     <td align="left" class="style1">
                           
                    <asp:Button ID="AddTemplate" runat="server" CausesValidation="False" 
                       Font-Names="Arial" Font-Size="10pt" 
                       Text="Add Template App" Width="166px" 
                       OnClientClick="showAddTemplateAppClient(); return false;" />
    </td>
     <td align="right" class="style2">
                            <asp:TextBox runat="server" ID="SelectedRowIndex" Width="38px" style="display:none"/>
                   <asp:Button ID="RowSelected" runat="server" CausesValidation="False" 
                       Font-Names="Arial" Font-Size="10pt" Width="31px" onclick="RowSelected_Click" style="display:none"/>                      
         <asp:Button ID="CopyTemplateAppToAccount" runat="server" onclick="CopyTemplateAppToAccount_Click"  style="display:none"/> 
</td>
     <td align="left">
            &nbsp;</td>
     <td>
                    &nbsp;</td>
     <td>
                   &nbsp; &nbsp;</td>
     <td>
                             &nbsp;</td>
     <td>
         &nbsp;</td>
    </tr>
    
     </table>
<div align="center" style=" background-color:#bcbcbc; width:900px; vertical-align:top;">
                <table border="0" cellpadding="0" cellspacing="0">
                 <tr style="height:16px">
                             <td style="height:16px;width:16px;  background-image:url(images/round_bottom_left.png); background-position:right bottom; background-repeat:no-repeat;">
                                   
                             </td>
                             <td style="background-image:url(images/white_square.png); width:868px; height:16px; background-repeat:repeat">
                             
                             </td>
                             <td style="height:16px;width:16px;background-image:url(images/round_bottom_right.png); background-position:left bottom; background-repeat:no-repeat; ">
                              </td>
                         </tr>
                </table>
                </div>
</td>
</tr>
</table>

              </div>
               <div ID="FirstView" runat="server" style="display:none" >
               <div align="center" style="height:20px;width:auto"></div>
               <div align="center" style="width:auto; height:150px;">
               <table style="width:500px;height:100%; background-image:url(images/first_view_background.png)"><tr>
                   <td class="style16">
                       &nbsp;</td><td>
                   </td><td>
                   </td><td>
                   </td><td align="right">
                       <asp:ImageButton ID="Close" runat="server" ImageUrl="~/images/close_dialog.png" 
                           onclick="Close_Click" />
                   </td><td align="right">
                       &nbsp;</td></tr><tr><td class="style17" valign="top">
                       &nbsp;</td><td valign="top">
                   <asp:ImageButton ID="DemoVideo1" runat="server" CausesValidation="False" 
                       CssClass="config_menu" Height="55px" ImageUrl="~/images/demo_video_button.png" 
                       Width="75px" />
                   </td><td valign="top">
                       <asp:ImageButton ID="Overview1" runat="server" CausesValidation="False" 
                           CssClass="config_menu" Height="55px" 
                           ImageUrl="~/images/viziapps_overview_button.png" Width="75px" />
                   </td><td valign="top">
                       <asp:ImageButton ID="QuickStart1" runat="server" CausesValidation="False" 
                           CssClass="config_menu" Height="55px" 
                           ImageUrl="~/images/1_minute_quick_start_button.png" Width="75px" />
                   </td><td valign="top">
                       <asp:ImageButton ID="YourFirstApp1" runat="server" CausesValidation="False" 
                           CssClass="config_menu" Height="55px" 
                           ImageUrl="~/images/your_first_app_button.png" Width="75px" />
                   </td><td valign="top">
                           &nbsp;</td></tr></table>
               </div>
               </div>
           
           
    </form>
</body>
</html>
