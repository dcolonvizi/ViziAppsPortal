<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageData.aspx.cs" Inherits="ManageData" %>
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
 
           .style1
           {
               width: 96px;
           }
           .style2
           {
               width: 280px;
           }
           .style3
           {
               width: 214px;
           }
           .style4
           {
               width: 295px;
           }
           .style5
           {
               width: 16px;
           }
           .style6
           {
               width: 29px;
           }
           .style8
           {
               width: 76px;
           }
           .style9
           {
               width: 149px;
           }
           .style10
           {
               width: 121px;
           }
           .style11
           {
               height: 20px;
               width: 737px;
           }
           .style12
           {
               width: 135px;
           }
     </style>
      <script  language="javascript" type="text/javascript" src="scripts/google_analytics_1.0.js"></script>
      <script  language="javascript" type="text/javascript" src="scripts/default_script_1.6.js"></script> 
      <script  language="javascript" type="text/javascript" src="scripts/storyboard_1.4.js"></script>  
      <script  language="javascript" type="text/javascript" src="scripts/tree_view_script_1.1.min.js"></script>
      <script  language="javascript" type="text/javascript" src="scripts/dialogs_1.26.min.js"></script>
      <script  language="javascript" type="text/javascript" src="scripts/database_script_1.14.min.js"></script>
      <script  language="javascript" type="text/javascript">
          //global static
          var storyBoardWindow = null;
          var TestQueryURL = 'PageData/Dialogs/TestQuery.aspx'; 
       </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">		
	</telerik:RadScriptManager>
	
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
  <AjaxSettings>    
          
              <telerik:AjaxSetting AjaxControlID="ClearTest">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DataMultiPage" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
 
              <telerik:AjaxSetting AjaxControlID="WebServiceEvents">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="WebServiceEvents" LoadingPanelID="ManageDataControlLoadingPanel"  />
                         <telerik:AjaxUpdatedControl ControlID="DataMultiPage" />
                          <telerik:AjaxUpdatedControl ControlID="DataTypeTabStrip" />                         
                   <telerik:AjaxUpdatedControl ControlID="WebServiceInputTreeView"/>
                     <telerik:AjaxUpdatedControl ControlID="PhoneRequestTreeView" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="TestWebServiceButton">
                 <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataMultiPage" LoadingPanelID="DataResponseTreeLoadingPanel"/>
                     <telerik:AjaxUpdatedControl ControlID="TestWebServiceMessage"/>
                     <telerik:AjaxUpdatedControl ControlID="WebServiceResponseTreeView"/>
                     
                 </UpdatedControls>
                </telerik:AjaxSetting>
                
               <telerik:AjaxSetting AjaxControlID="ManageDataApps">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ContentMultiPage" />
                  <telerik:AjaxUpdatedControl ControlID="ViewStoryBoard" />                   
                    <telerik:AjaxUpdatedControl ControlID="ManageDataApps" LoadingPanelID="ManageDataControlLoadingPanel" ></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ManageDataTypeLabel" />   
                    <telerik:AjaxUpdatedControl ControlID="ManageDataType" />                                         
               </UpdatedControls>
            </telerik:AjaxSetting>
                       
             <telerik:AjaxSetting AjaxControlID="ManageDataType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ManageDataType" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ManageTypeMultiPage" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
               </UpdatedControls>
            </telerik:AjaxSetting>
                                                      
            <telerik:AjaxSetting AjaxControlID="WebServiceResponseTreeView">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="WebServiceResponseTreeView" LoadingPanelID="DataResponseTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="PhoneResponseTreeView" LoadingPanelID="DataResponseTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                   <telerik:AjaxUpdatedControl ControlID="DataMultiPage"/>
                     <telerik:AjaxUpdatedControl ControlID="ClearTest"/>
                      <telerik:AjaxUpdatedControl ControlID="TestWebServiceButton"/>
              </UpdatedControls>
            </telerik:AjaxSetting>   
    
            <telerik:AjaxSetting AjaxControlID="WebServiceInputTreeView">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="WebServiceInputTreeView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="PhoneRequestTreeView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="SaveRequestMessage"/>  
                </UpdatedControls>
            </telerik:AjaxSetting>   
            
            <telerik:AjaxSetting AjaxControlID="PhoneRequestTreeView">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="WebServiceInputTreeView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
               </UpdatedControls>
            </telerik:AjaxSetting>   

            <telerik:AjaxSetting AjaxControlID="ViewConnectionString">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DatabaseCommandsView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
               </UpdatedControls>
            </telerik:AjaxSetting>             
           

            <telerik:AjaxSetting AjaxControlID="DatabaseCommandsView">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DatabaseCommandsView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="DatabaseConfigMessage"/> 
               </UpdatedControls>
            </telerik:AjaxSetting>                           

          <telerik:AjaxSetting AjaxControlID="DBTreeClick">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DatabaseCommandsView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="SpreadsheetCommandsView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
               </UpdatedControls>
            </telerik:AjaxSetting>             
           
             <telerik:AjaxSetting AjaxControlID="ResetDataRequestMap">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="WebServiceInputTreeView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="PhoneRequestTreeView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
               </UpdatedControls>
            </telerik:AjaxSetting>   

             <telerik:AjaxSetting AjaxControlID="SaveMethodCall">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="WebServiceInputTreeView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RequestTreeEdits" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>    
                     <telerik:AjaxUpdatedControl ControlID="SaveRequestMessage"/>         
               </UpdatedControls>
            </telerik:AjaxSetting>   

            <telerik:AjaxSetting AjaxControlID="SeeAllPages">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="WebServiceInputTreeView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="PhoneRequestTreeView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                      <telerik:AjaxUpdatedControl ControlID="SeeAllPages"/>         
               </UpdatedControls>
            </telerik:AjaxSetting>   

           <telerik:AjaxSetting AjaxControlID="ResetMethodCall">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="WebServiceInputTreeView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RequestTreeEdits" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>  
                     <telerik:AjaxUpdatedControl ControlID="SaveRequestMessage"/>           
               </UpdatedControls>
            </telerik:AjaxSetting>   

            <telerik:AjaxSetting AjaxControlID="ResetDataResponseMap">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="WebServiceResponseTreeView" LoadingPanelID="DataResponseTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="PhoneResponseTreeView" LoadingPanelID="DataResponseTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ResponseTreeEdits" ></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="ResponseMessage"/>     
               </UpdatedControls>
            </telerik:AjaxSetting>   

          <telerik:AjaxSetting AjaxControlID="SaveDataResponseMap">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PhoneResponseTreeView" LoadingPanelID="DataResponseTreeLoadingPanel"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="ResponseTreeEdits" ></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="ResponseMessage"/>     
               </UpdatedControls>
            </telerik:AjaxSetting>   

            <telerik:AjaxSetting AjaxControlID="SaveGoogleDocsConfig">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="SpreadsheetCommandsView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
               </UpdatedControls>
            </telerik:AjaxSetting>   

             <telerik:AjaxSetting AjaxControlID="ResetGoogleDocsConfig">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="SpreadsheetCommandsView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
               </UpdatedControls>
            </telerik:AjaxSetting>   
          
           <telerik:AjaxSetting AjaxControlID="SaveDatabaseConfig">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="DatabaseCommandsView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
               </UpdatedControls>
            </telerik:AjaxSetting>   

             <telerik:AjaxSetting AjaxControlID="ResetDatabaseConfig">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="DatabaseCommandsView" LoadingPanelID="DataRequestTreeLoadingPanel"></telerik:AjaxUpdatedControl>
               </UpdatedControls>
            </telerik:AjaxSetting>   

    </AjaxSettings>
    	</telerik:RadAjaxManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Telerik">
            <Windows>
                <telerik:RadWindow 
                    id="ViewDataModel" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                    title="How Your Device Gets Data"
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
                    title="Help on Managing Data" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>

                          <telerik:RadWindow 
                    id="DatabaseHelp" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                     Top="200px"
                     Left="200px"
                    title="Help on Interfacing to your Database" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>

            <telerik:RadWindow 
                    id="ViewConnectionStringHelp" 
                    runat="server"
                    showcontentduringload="false"
                    width="800px"
                    height="100px"
                     Top="200px"
                     Left="300px"
                    title="Help on Managing Data" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="true"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>
             
              <telerik:RadWindow 
                    id="WebserviceURLHelp" 
                    runat="server"
                    showcontentduringload="false"
                    width="800px"
                    height="100px"
                     Top="200px"
                     Left="300px"
                    title="Help on Managing Data" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="true"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>

            <telerik:RadWindow 
                    id="WebServiceSpecsWindow" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                     Top="200px"
                     Left="200px"
                    title="Web Service Specifications" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>

             <telerik:RadWindow 
                    id="DatabaseSpecsWindow" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                     Top="200px"
                     Left="200px"
                    title="Web Service Specifications" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>            

            <telerik:RadWindow 
                    id="EventMappingStatusWindow" 
                    runat="server"
                    showcontentduringload="false"
                     Top="200px"
                     Left="200px"
                    title="Event Mapping Status" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>
             

            </Windows>
            </telerik:RadWindowManager>

 <telerik:RadWindowManager ID="RadWindowManager2" runat="server" Skin="Telerik">
            <Windows>
                <telerik:RadWindow 
                    id="RadWindow1" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                    title="How Your Device Gets Data"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>
                
               <telerik:RadWindow 
                    id="RadWindow2" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                     Top="200px"
                     Left="200px"
                    title="Help on Managing Data" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>

                          <telerik:RadWindow 
                    id="RadWindow3" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                     Top="200px"
                     Left="200px"
                    title="Help on Interfacing to your Database" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>

            <telerik:RadWindow 
                    id="RadWindow4" 
                    runat="server"
                    showcontentduringload="false"
                    width="800px"
                    height="100px"
                     Top="200px"
                     Left="300px"
                    title="Help on Managing Data" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="true"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>
             
              <telerik:RadWindow 
                    id="RadWindow5" 
                    runat="server"
                    showcontentduringload="false"
                    width="800px"
                    height="100px"
                     Top="200px"
                     Left="300px"
                    title="Help on Managing Data" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="true"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>

            <telerik:RadWindow 
                    id="RadWindow6" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                     Top="200px"
                     Left="200px"
                    title="Web Service Specifications" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>

             <telerik:RadWindow 
                    id="RadWindow7" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                     Top="200px"
                     Left="200px"
                    title="Web Service Specifications" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>            

            <telerik:RadWindow 
                    id="RadWindow8" 
                    runat="server"
                    showcontentduringload="false"
                     Top="200px"
                     Left="200px"
                    title="Event Mapping Status" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>
             

            </Windows>
            </telerik:RadWindowManager>
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
                    &nbsp;</td>
                <td>               
                </td>
                </tr>
                </table>
                </div>  
                  
      <div align="center" style="width:100%">
            <div align="center" style=" background-color:#bcbcbc; width:1000px; vertical-align:top;">
                <table border="0" cellpadding="0" cellspacing="0">
                 <tr style="height:16px">
                             <td style="height:16px;width:16px;  background-image:url(images/round_top_left.png); background-position:right bottom; background-repeat:no-repeat;">
                                   
                             </td>
                             <td style="background-image:url(images/white_square.png); height:16px; width:968px;background-repeat:repeat">
                             
                             </td>
                             <td style="height:16px;width:16px;background-image:url(images/round_top_right.png); background-position:left bottom; background-repeat:no-repeat; ">
                              </td>
                         </tr>
                </table>
                </div>
                <div  style="width:1000px;background-color:white; vertical-align:top">
                <table style="width:1000px;background-color:white; vertical-align:top">
                <tr>
                <td style="width:16px;" ></td>
                <td style="width:968px;" >
                <table>
                <tr>
                <td align="left">
                <table style="width: 954px">
                <tr>
   <td style="background-color:#0054c2; text-align:left;" align="left" class="style11">
  &nbsp;&nbsp;
  <asp:Label ID="ManageDataHeader" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="10pt" ForeColor="White" 
           
           Text="   Application Data"></asp:Label>   </td>
                <td align="left">
                    &nbsp;</td>
                    <td class="style12">
                                <asp:Button ID="Button10" runat="server" Text="Spreadsheet User Guide" 
                           Width="162px" 
                            OnClientClick="PopUp('Help/Guide/Default.htm', ' menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');return false;" />
                            </td>
                 <td align="center">
      <asp:ImageButton ID="MySolutionsHelp" runat="server" ImageUrl="~/images/help.gif" 
        ToolTip="What is this page for?" OnClientClick="var oWin=radopen('Help/ManageData/ManageDataHelp.htm', 'PageHelp'); 
        oWin.setSize(600,400);
                 oWin.set_visibleTitlebar(true);
                       oWin.set_visibleStatusbar(false);
                       oWin.set_modal(true); 
                        oWin.moveTo(400, 200); 
        return false;"/>

                </td>
                </tr></table>
                </td>
                </tr>
                <tr>
                 <td>
                 <table style="width: 866px">
                 <tr>
                 <td class="style8">
                                <asp:Label ID="Label2" runat="server" Text="Current App" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True"></asp:Label>
                 </td>
                  <td class="style10">
            <telerik:RadComboBox ID="ManageDataApps" runat="server" Font-Names="Arial" AutoPostBack="True" 
                Font-Size="10pt" OnSelectedIndexChanged="ManageDataApps_SelectedIndexChanged" Width="250px" >
            </telerik:RadComboBox>
                 </td>
                  <td class="style9">

                                                    <asp:Button ID="ViewStoryBoard" runat="server"  Text="View Storyboard"
                                                    Width="115px"   
                                       CausesValidation="False" UseSubmitBehavior="False" />
                 </td>
                  <td class="style6">
                 </td>
                  <td style="width: 250px">
                                     
            <asp:Button ID="SeeDataModel" runat="server" CausesValidation="False" 
                                   Font-Names="Arial" Font-Size="10pt" OnClientClick="var oWin=radopen('Help/ManageData/ViewDataModel.htm', 'ViewDataModel'); 
                                       oWin.set_visibleTitlebar(true);
                                     oWin.set_visibleStatusbar(false);
                                   oWin.setSize(1060,900);
                                   oWin.moveTo(50,0);
                                   return false;"
                                   Text="How Your Device Gets Web Data" Width="215px" />
                                     
                 </td>
                 </tr>
                     </table>
                 </td>
                </tr>
                <tr>
                 <td>
                 <table style="width: 866px">
                 <tr>
                 <td class="style1">
                                <asp:Label ID="ManageDataTypeLabel" runat="server" Text="Type of Data Management" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" style="display:none"></asp:Label>
                                             </td>
                  <td class="style2">
            <telerik:RadComboBox ID="ManageDataType" runat="server" Font-Names="Arial" AutoPostBack="True" 
                Font-Size="10pt" Width="250px" style="display:none" onselectedindexchanged="ManageDataType_SelectedIndexChanged">
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="Select -&gt;" 
                        Value="Select -&gt;" />
                    <telerik:RadComboBoxItem runat="server" Text="Google Spreadsheet" 
                        Value="Google Spreadsheet" />
                    <telerik:RadComboBoxItem runat="server" Text="Database" Value="Database" />
                    <telerik:RadComboBoxItem runat="server" Text="WebService" Value="WebService" />
                </Items>
            </telerik:RadComboBox></td>
                  <td class="style4">
                                     
                                      <telerik:RadAjaxLoadingPanel ID="ManageDataControlLoadingPanel" runat="server" 
                                          Skin="Default" Width="16px"></telerik:RadAjaxLoadingPanel>
                     </td>
                  <td class="style3">

                                      <telerik:RadAjaxLoadingPanel ID="DataRequestTreeLoadingPanel" runat="server" 
                                          Skin="Default" Width="16px"></telerik:RadAjaxLoadingPanel>
                                     
                     </td>
                  <td style="width:200px">
                                     
                                      <telerik:RadAjaxLoadingPanel ID="DataResponseTreeLoadingPanel" runat="server" 
                                          Skin="Default" Width="16px">
                                 </telerik:RadAjaxLoadingPanel>
                                     
                     </td>
                 </tr></table>
                </td>
                </tr>
                <tr>
                <td>
          <telerik:RadMultiPage ID="ContentMultiPage" runat="server" 
            SelectedIndex="0">
            <telerik:RadPageView ID="NoView" runat="server"><div></div></telerik:RadPageView>
             <telerik:RadPageView ID="ContentView" runat="server" BackColor="White">
             <div style="height:10px"></div><telerik:RadMultiPage 
                  ID="ManageTypeMultiPage" runat="server" SelectedIndex="3"><telerik:RadPageView ID="WebServiceView" runat="server"><div><table style="width: 868px; "><tr><td class="style79"><asp:Label ID="Label15" runat="server" Font-Names="Verdana" 
                    Font-Size="10pt" ForeColor="#333333" 
                    Text="Event for Mapping Device Data to Web Services" Width="350px"></asp:Label></td><td align="left">
                         <telerik:RadComboBox ID="WebServiceEvents" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="WebServiceEvents_SelectedIndexChanged" Width="250px"></telerik:RadComboBox></td><td><asp:Button ID="WebServiceEventMappingStatus" runat="server" 
                        CausesValidation="False" Font-Names="Arial" Font-Size="10pt" 
                        Text="Event Mapping Status" Width="164px" /></td><td align="right"><asp:Button ID="WebServiceSpecs" runat="server" 
                        CausesValidation="False" Font-Names="Arial" Font-Size="10pt" OnClientClick="var oWin=radopen('Help/ManageData/WebServiceSpecs.htm', 'WebServiceSpecsWindow'); 
                                       oWin.setSize(800,500);
                                     oWin.set_visibleTitlebar(true);
                                           oWin.set_visibleStatusbar(false);
                                           oWin.set_modal(true); 
                                            oWin.moveTo(400, 200); 
                                   return false;" Text="Web Service Specs" Width="137px" /></td></tr></table><table style="width: 879px"><tr><td class="style76"><telerik:RadTabStrip ID="DataTypeTabStrip" runat="server" 
                    MultiPageID="DataMultiPage" SelectedIndex="0" Skin="Telerik"><tabs><telerik:RadTab runat="server" Font-Bold="true" Font-Size="14px" 
                    ForeColor="Gray" ImageUrl="~/images/forward_nav.gif" PageViewID="RequestsView" 
                    Selected="True" Text="Data Requests From Device" /><telerik:RadTab runat="server" Font-Bold="true" Font-Size="14px" 
                    ForeColor="Gray" ImageUrl="~/images/backward_nav.gif" 
                    PageViewID="ResponsesView" Text="Data Responses Back To Device" /></tabs></telerik:RadTabStrip></td><td align="right" width="150"><asp:Button ID="DBTreeClick" runat="server" onclick="DBTreeClick_Click" style="display:none" Width="1px" /></td></tr></table></div><telerik:RadMultiPage 
                  ID="DataMultiPage" runat="server"  SelectedIndex="0" Width="868px"><telerik:RadPageView ID="RequestsView" runat="server" 
                  Width="868px"><div style="width: 949px;" align="left"><table style="width: 856px"><tr><td class="style77"></td><td class="style78"></td><td rowspan="5" width="880"><img alt="" src="images/data_request_legend.png" /></td></tr><tr><td class="style71"></td><td bgcolor="#CCFFCC" class="style75"><asp:Label ID="help4" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="10pt" ForeColor="#003399" 
                        Text="First double click a Web Service node on the right to enter your URL" 
                        Width="608px"></asp:Label></td></tr><tr><td class="style84"></td><td bgcolor="#CCFFCC" class="style85">
                                     <asp:Label ID="help1" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="10pt" ForeColor="#003399" 
                        Text="Drag highlighted device data-fields  from the left to  Web Service inputs ("></asp:Label><img src="images/dot.gif" style="vertical-align: bottom;" /><asp:Label ID="help3" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="10pt" ForeColor="#003399" Text=") on the right."></asp:Label></td></tr><tr><td class="style82"></td><td bgcolor="#CCFFCC" class="style83"><asp:Label 
                    ID="help5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                    ForeColor="#003399" 
                    Text="Click Save below the web service method when you are done."></asp:Label></td></tr><tr><td class="style71">&#160;</td><td align="center" class="style75"><asp:Label ID="SaveRequestMessage" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon"  
                        Width="537px"></asp:Label></td></tr></table></div><div><table style="width: 877px"><tr><td class="style31"><table><tr><td><asp:Label ID="Label5" runat="server" Font-Bold="True" 
                              Font-Names="Arial" Font-Size="11pt" ForeColor="#003399" 
                              Text="Data From Device" Width="200px"></asp:Label></td></tr><tr><td align="left"><asp:CheckBox ID="SeeAllPages" runat="server" AutoPostBack="True" Checked="false" 
                                      Font-Names="Verdana" Font-Size="10pt" ForeColor="Black" 
                                      oncheckedchanged="SeeAllPages_CheckedChanged" Text="See All Pages" /></td></tr></table></td><td class="style5">&#160;</td><td align="right" class="style81" valign="top"><asp:Label 
                    ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" 
                    ForeColor="#003399" Text="Inputs To WebService" Width="200px"></asp:Label></td><td align="center" class="style80"><asp:Button ID="SaveMethodCall" runat="server" CausesValidation="False" 
                        Font-Names="Arial" Font-Size="10pt" onclick="SaveDataRequestMap_Click" 
                        style="display:none" Width="1px" /><asp:Button ID="ResetMethodCall" runat="server" CausesValidation="False" 
                        Font-Names="Arial" Font-Size="10pt" onclick="ResetMethodCall_Click" 
                        style="display:none" Width="1px" /></td><td align="center">&#160;</td><td align="right" valign="bottom" width="130"><asp:Button ID="ResetDataRequestMap" runat="server" CausesValidation="False" 
                                                 Font-Names="Arial" Font-Size="10pt" onclick="ResetDataRequestMap_Click" 
                                                 Text="Reset All" Width="80px" /></td></tr></table></div><div style="width:868px; text-align: left;"><table style="width: 100%"><tr><td style=" vertical-align:top;"><telerik:RadTreeView ID="PhoneRequestTreeView" Runat="server" 
                    BorderColor="#CCCCFF" BorderStyle="Dashed" BorderWidth="1px" 
                    EnableDragAndDrop="true" ForeColor="Navy" Height="500px" 
                    OnClientNodeDragStart="onPhoneRequestTreeViewDragStart" 
                    OnClientNodeDropping="onPhoneRequestTreeViewNodeDropping" Skin="Web20" 
                    Width="350px"></telerik:RadTreeView></td><td>&#160;&#160;</td><td style="width:400px;" valign="top"><telerik:RadTreeView 
                    ID="WebServiceInputTreeView" Runat="server" AllowNodeEditing="true" 
                    AutoPostBack="true" BorderColor="#CCCCFF" BorderStyle="Dashed" 
                    BorderWidth="1px" EnableDragAndDrop="true" ForeColor="Navy" Height="500px" 
                    OnClientNodeClicked="WebServiceInputTreeViewClicked" 
                    OnClientNodeDragStart="ClientNoNodeDragStart" 
                    OnClientNodeDropping="onPhoneRequestTreeViewNodeDropping" 
                    OnNodeEdit="OnWebServiceInputTreeNodeEdit" Skin="Web20" Width="500px"></telerik:RadTreeView></td><td align="right" style="width:197px;" valign="top">&#160;</td></tr></table></div></telerik:RadPageView><telerik:RadPageView ID="ResponsesView" runat="server"><div style="height: 11px"></div><div align="left"><table style="width: 900px"><tr><td bgcolor="#CCFFCC" class="style68">
                         <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="10pt" ForeColor="#003399" 
                        Text="To get the web service outputs to the device, first call the live Web Service by clicking 'Test Web Service'." 
                        Width="696px"></asp:Label></td><td rowspan="6"><img alt="" src="images/data_response_legend.png" /></td></tr><tr><td bgcolor="#CCFFCC" class="style68"><asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="10pt" ForeColor="#003399" 
                        Text="After you call the live Web Service, expand the green nodes on the right to see the outputs." 
                        Width="696px"></asp:Label></td></tr><tr><td bgcolor="#CCFFCC" class="style66"><asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="10pt" ForeColor="#003399" 
                        Text="Drag highlighted Web Service responses (" Width="273px"></asp:Label><img src="images/dot.gif" style="vertical-align: bottom" />
                             <asp:Label 
                        ID="Label14" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="10pt" ForeColor="#003399" 
                        Text=") from the right to highlighted device fields on the left." Width="343px"></asp:Label></td></tr><tr><td bgcolor="#CCFFCC" class="style68"><asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="10pt" ForeColor="#003399" Text="Click 'Save' when you are done." 
                        Width="301px"></asp:Label></td></tr><tr><td align="center" class="style65"><asp:Label ID="ResponseMessage" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Height="16px" 
                        Width="500px"></asp:Label></td></tr></table></div><div><table style="width: 887px"><tr><td width="150"><asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="11pt" ForeColor="#003399" Text="Data To Device" Width="152px"></asp:Label></td><td align="center" class="style90">
                                 <asp:Button ID="TestWebService" runat="server" onclick="TestWebService_Click" 
                        style="margin-left: 0px" Text="Test Web Service" Width="135px" /></td><td align="center" width="80"><asp:Button ID="SaveDataResponseMap" runat="server" CausesValidation="False" 
                        Font-Names="Arial" Font-Size="10pt" onclick="SaveDataResponseMap_Click" 
                        Text="Save" Width="50px" /></td><td width="80"><asp:Button ID="ResetDataResponseMap" runat="server" CausesValidation="False" 
                        Font-Names="Arial" Font-Size="10pt" onclick="ResetDataResponseMap_Click" 
                        Text="Reset" Width="53px" /></td><td class="style91">&#160;</td><td class="style88" width="100">&#160;</td><td align="left"><asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="11pt" ForeColor="#003399" Text="Outputs From WebService" 
                        Width="230px"></asp:Label></td></tr></table></div><div style="width:868px; text-align: left;"><table style="width:100%;"><tr><td style=" vertical-align:top;"><telerik:RadTreeView ID="PhoneResponseTreeView" Runat="server" 
                    AllowNodeEditing="true" BorderColor="#CCCCFF" BorderStyle="Dashed" 
                    BorderWidth="1px" EnableDragAndDrop="true" ForeColor="Navy" Height="500px" 
                    OnClientNodeClicked="PhoneResponseTreeViewNodeClicked" 
                    OnClientNodeDragStart="ClientNoNodeDragStart" 
                    OnClientNodeDropping="onWebServiceResponseTreeViewNodeDropping" Skin="Web20" 
                    Width="400px"></telerik:RadTreeView></td><td>&#160;&#160;</td><td style="width:600px;" valign="top"><telerik:RadTreeView ID="WebServiceResponseTreeView" Runat="server" 
                    BorderColor="#CCCCFF" BorderStyle="Dashed" BorderWidth="1px" 
                    EnableDragAndDrop="true" ForeColor="Navy" Height="500px" 
                    OnClientNodeDragStart="onWebServiceResponseTreeViewDragStart" 
                    OnClientNodeDropping="onWebServiceResponseTreeViewNodeDropping" Skin="Web20" 
                    Width="500px"></telerik:RadTreeView></td><td align="right" style="width:205px;" valign="top">&#160;</td></tr></table></div></telerik:RadPageView><telerik:RadPageView ID="TestWebServiceView" runat="server"><div style="text-align:left;"><asp:Label ID="TestWebServiceMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="525px"></asp:Label></div><div><asp:Repeater ID="ParamRepeater" runat="server"><HeaderTemplate><div style="height:10px;"></div><div align="left" style="height:25px; background-color:#CCFFCC"><asp:Label 
                        ID="Label25" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                        ForeColor="#003399" 
                        
                                 Text="To get live web service responses enter valid inputs, if any, and click 'Call Web Service'" /></div></HeaderTemplate><ItemTemplate><div><table><tr><td style="width:150px;font-family:Verdana;font-size:12px;color:Navy"><asp:Label 
                            ID="Label7" runat="server" 
                            Text='<%#DataBinder.Eval(Container.DataItem,"Param") %>'></asp:Label></td><td><asp:TextBox ID="paramInput" runat="server" Width="400"></asp:TextBox></td></tr></table></div></ItemTemplate></asp:Repeater><div style="height:20px;"><div 
                style="height:50px; vertical-align:middle; text-align:left;"><table style="width: 571px; text-align: left;"><tr><td align="left" style="width: 150px;">&#160;&#160;</td><td align="right" class="style43"><asp:Button ID="TestWebServiceButton" runat="server" 
                                onclick="TestWebServiceButton_Click" style="margin-left: 0px" 
                                Text="Call Web Service" Width="136px" /></td><td style="text-align: right;"><asp:Button ID="ClearTest" runat="server" onclick="ClearTest_Click" 
                                Text="Close This Test" Width="136px" /></td></tr></table></div></div></div></telerik:RadPageView></telerik:RadMultiPage></telerik:RadPageView><telerik:RadPageView ID="BlankView" runat="server"><div></div></telerik:RadPageView><telerik:RadPageView ID="DatabaseView" runat="server"><div><table style="width: 868px; height: 43px;"><tr><td align="right" class="style96"><div>
                 <asp:Label ID="DatabaseEventsLabel" runat="server" Font-Names="Verdana" Font-Size="10pt" 
                                               ForeColor="#333333" 
                     Text="Tap Event for Mapping Device Data to Your Database"></asp:Label></div></td><td>&#160;&#160;</td><td align="left">
                 <telerik:RadComboBox ID="DatabaseEvents" runat="server" AutoPostBack="True" 
                                           
                     onselectedindexchanged="DatabaseEvents_SelectedIndexChanged" Width="250px"></telerik:RadComboBox></td><td>
                     <asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                           Font-Names="Arial" Font-Size="10pt" OnClientClick="getDatabaseInfo(this)"
                                   Text="Upload Database Info" Width="145px" /></td><td align="right"><asp:Button ID="ViewConnectionString" runat="server" CausesValidation="False" 
                                           Font-Names="Arial" Font-Size="10pt" OnClientClick="showViewConnections();" 
                                           Text="DB Connections" Visible="False" Width="118px" /></td><td 
                                       align="right"><asp:Button ID="DBWebService" runat="server" CausesValidation="False" 
                                           Font-Names="Arial" Font-Size="10pt" OnClientClick="var oWin=radopen('Dialogs/ManageData/DBWebService.aspx', 'WebserviceURLHelp'); 
                                                            oWin.setSize(650,250);
                                                             oWin.set_visibleTitlebar(false);
                                                                   oWin.set_visibleStatusbar(false);
                                                                   oWin.set_modal(true); 
                                                                    oWin.moveTo(300, 200); 
                                                                     oWin.add_close(onWebServiceURLClientClose);
                                                                return false;" Text="DB Web Service" Width="116px" /></td><td 
                                       align="right"><asp:ImageButton ID="DatabaseHelpButton" runat="server" 
                                           ImageUrl="~/images/help.gif" OnClientClick="var oWin=radopen('Help/ManageData/DatabaseHelp.htm', 'DatabaseHelp'); 
                                                        oWin.setSize(850,500);
                                                                 oWin.set_visibleTitlebar(true);
                                                                       oWin.set_visibleStatusbar(false);
                                                                       oWin.set_modal(true); 
                                                                        oWin.moveTo(400, 200); 
                                                        return false;" 
                                           ToolTip="How do I manage data with my database?" /></td></tr></table></div><div align="left" style="width: 949px; "><table style="width: 856px; height: 100px;"><tr><td class="style92"></td><td bgcolor="#CCFFCC" class="style93"><asp:Label 
                                       ID="Label17" runat="server" Font-Bold="True" Font-Names="Arial" 
                                       Font-Size="10pt" ForeColor="#003399" 
                                       Text="Select the device tap event above and then create a SQL Command by clicking on the green '+' below" 
                                       Width="800px"></asp:Label></td></tr><tr><td class="style92"></td><td bgcolor="#CCFFCC" class="style93"><asp:Label 
                                       ID="Label19" runat="server" Font-Bold="True" Font-Names="Arial" 
                                       Font-Size="10pt" ForeColor="#003399" Text="Click Save when you are done."></asp:Label></td></tr><tr><td class="style71">&#160;</td><td align="center" class="style75"><asp:Label ID="DatabaseConfigMessage" runat="server" Font-Bold="True" 
                                       Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Height="17px" 
                                       Width="665px"></asp:Label></td></tr></table></div><div><table style="width: 877px; height: 29px;"><tr><td class="style31"><asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Arial" 
                                           Font-Size="11pt" ForeColor="#003399" Text="Database Commands" Width="200px"></asp:Label></td><td class="style5"></td><td align="right" class="style95"><asp:Button ID="SaveDatabaseConfig" runat="server" CausesValidation="False" 
                                           Font-Names="Arial" Font-Size="10pt" onclick="SaveDatabaseConfig_Click" 
                                           Text="Save" Width="65px" /></td><td align="center"><asp:Button ID="ResetDatabaseConfig" runat="server" CausesValidation="False" 
                                           Font-Names="Arial" Font-Size="10pt" onclick="ResetDatabaseConfig_Click" 
                                           Text="Reset All Sql Commands For All Events" Width="253px" /></td><td align="right" width="130"><asp:TextBox ID="ConnectionString" runat="server" style="display:none;"></asp:TextBox><asp:TextBox ID="WebServiceURLString" runat="server" style="display:none;"></asp:TextBox></td></tr></table></div><div style="width:868px; text-align: left;"><table style="width:100%;"><tr><td class="style94">&#160;&#160;</td><td style="width:400px;" valign="top"><telerik:RadTreeView 
                                           ID="DatabaseCommandsView" Runat="server" AutoPostBack="true" 
                                           BorderColor="#CCCCFF" BorderStyle="Dashed" BorderWidth="1px" 
                                           EnableDragAndDrop="false" ForeColor="Navy" Height="500px" 
                                           OnclientLoad="DatabaseCommandsView_load" 
                                           Skin="Web20" Width="800px"></telerik:RadTreeView></td><td align="right" style="width:197px;" valign="top"><asp:Button 
                                           ID="UpdateDatabaseTree" runat="server" CausesValidation="False" 
                                           onclick="UpdateDatabaseTree_Click" style="display:none" Width="1px" /><asp:TextBox ID="DBTreeInfo" runat="server" style="display:none" Width="4px"></asp:TextBox></td></tr></table></div></telerik:RadPageView>
                                           <telerik:RadPageView ID="GoogleDocsView" runat="server">
                                           <div>
                                           <table style="width: 868px; height: 43px;">
                                           <tr><td align="right" class="style97"><div><asp:Label ID="SpreadSheetEventsLabel" 
                                                   runat="server" Font-Names="Verdana" Font-Size="10pt" 
                                       ForeColor="#333333" 
                                       Text="Tap Event for Mapping Device Data to your Google Docs Spreadsheet"></asp:Label></div</td><td align="right" class="style98">
                                                   <telerik:RadComboBox ID="SpreadSheetEvents" runat="server" AutoPostBack="True" 
                                   onselectedindexchanged="DatabaseEvents_SelectedIndexChanged" Width="250px"></telerik:RadComboBox></td><td align="right">
                                                   <asp:Button ID="Button1" runat="server" CausesValidation="False" Font-Names="Arial" 
                                           Font-Size="10pt" OnClientClick="getGoogleDocsInfo(this)"
                                   Text="Connect to Google Docs" Width="165px" /></td><td align="right">
                                                   <asp:Button ID="Button9" runat="server" CausesValidation="False" 
                                   Font-Names="Arial" Font-Size="10pt" OnClientClick="PopUp('Help/ManageData/GoogleDocsGuide.htm', 'height=800, width=820, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');return false;" 
                                   Text="Short Guide" Width="145px" /></td><td align="right"><asp:ImageButton ID="GoogleDocsHelpButton" runat="server" 
                                   ImageUrl="~/images/help.gif" OnClientClick="var oWin=radopen('Help/ManageData/GoogleDocsHelp.htm', 'DatabaseHelp'); 
                                                        oWin.setSize(850,450);
                                                                 oWin.set_visibleTitlebar(true);
                                                                       oWin.set_visibleStatusbar(false);
                                                                       oWin.set_modal(true); 
                                                                        oWin.moveTo(400, 200); 
                                                        return false;" 
                                   ToolTip="How do I manage data with Google Docs spreadsheets?" /></td>
                                   </tr></table>
                                   </div>
                                   <div align="left" style="width: 949px; "><table style="width: 856px; height: 100px;"><tr><td class="style92"></td><td bgcolor="#CCFFCC" class="style93"><asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Arial" 
                                       Font-Size="10pt" ForeColor="#003399" 
                                       Text="Select a device tap event above and then create a Spreadsheet Command with the 2 buttons below." 
                                       Width="800px"></asp:Label></td></tr><tr><td class="style92"></td><td bgcolor="#CCFFCC" class="style93"><asp:Label 
                                       ID="Label21" runat="server" Font-Bold="True" Font-Names="Arial" 
                                       Font-Size="10pt" ForeColor="#003399" Text="Click Save when you are done."></asp:Label></td></tr><tr><td class="style71">&#160;</td><td align="center" class="style75"><asp:Label ID="GoogleDocsConfigMessage" runat="server" Font-Bold="True" 
                                       Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Height="17px" 
                                       Width="660px"></asp:Label></td></tr></table></div><div><table style="width: 877px; height: 29px;"><tr><td class="style31"><asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Arial" 
                                           Font-Size="11pt" ForeColor="#003399" Text="Spreadsheet Commands" Width="200px"></asp:Label></td><td class="style5"></td><td align="right" class="style95"><asp:Button ID="SaveGoogleDocsConfig" runat="server" CausesValidation="False" 
                                           Font-Names="Arial" Font-Size="10pt" onclick="SaveDatabaseConfig_Click" 
                                           Text="Save" Width="65px" /></td><td align="center"><asp:Button ID="ResetGoogleDocsConfig" runat="server" CausesValidation="False" 
                                           Font-Names="Arial" Font-Size="10pt" onclick="ResetDatabaseConfig_Click" 
                                           Text="Reset All Commands For All Events" Width="253px" /></td><td align="right" width="130"><asp:TextBox ID="TextBox1" runat="server" style="display:none;"></asp:TextBox><asp:TextBox ID="TextBox2" runat="server" style="display:none;"></asp:TextBox></td></tr></table></div><div style="width:868px; text-align: left;"><table style="width:100%;"><tr><td class="style94">&#160;&#160;</td><td style="width:400px;" valign="top">
                                               <telerik:RadTreeView ID="SpreadsheetCommandsView" Runat="server" 
                                           BorderColor="#CCCCFF" BorderStyle="Dashed" 
                                           BorderWidth="1px" EnableDragAndDrop="false" ForeColor="Navy" Height="1000px" 
                                           OnclientLoad="SpreadsheetCommandsView_load" 
                                           Skin="Web20" Width="800px"></telerik:RadTreeView></td><td align="right" style="width:197px;" valign="top">
                                           <asp:Button ID="Button8" runat="server" CausesValidation="False" 
                                           onclick="UpdateDatabaseTree_Click" style="display:none" Width="1px" /><asp:TextBox ID="SpreadsheetTreeInfo" runat="server" style="display:none" 
                                           Width="4px"></asp:TextBox></td></tr></table></div>
                                           </telerik:RadPageView>
                                           </telerik:RadMultiPage></telerik:RadPageView>
                        </telerik:RadMultiPage>
    
                </td>
                </tr>
                </table>
                
                </td>
                <td style="width:16px;" ></td>
                </tr></table>
                </div>
              <div align="center" style=" background-color:#bcbcbc; width:1000px; vertical-align:top;">
                <table border="0" cellpadding="0" cellspacing="0">
                 <tr style="height:16px">
                             <td style="height:16px;width:16px;  background-image:url(images/round_bottom_left.png); background-position:right bottom; background-repeat:no-repeat;">
                                   
                             </td>
                             <td style="background-image:url(images/white_square.png); height:16px; width:968px;background-repeat:repeat">
                             
                             </td>
                             <td style="height:16px;width:16px;background-image:url(images/round_bottom_right.png); background-position:left bottom; background-repeat:no-repeat; ">
                              </td>
                         </tr>
                </table>
                </div>
                 <div>
            <asp:TextBox ID="RequestTreeEdits" runat="server" style="display:none;"
                                 Width="10px" BorderStyle="None" BorderWidth="0px" 
                     ></asp:TextBox>
           <asp:TextBox ID="ResponseTreeEdits" runat="server"  AutoPostBack="true" style="display:none;"
                        Height="35px" Width="10px" BorderStyle="None" BorderWidth="0px" 
                     ></asp:TextBox>
            <asp:TextBox ID="ClickedNodeInfo" runat="server" style="display:none;"
                                 Width="10px" BorderStyle="None" BorderWidth="0px" 
                        ></asp:TextBox>
                         <asp:TextBox ID="ShouldRefreshStoryBoard" runat="server"  AutoPostBack="true"
                                Width="1px"  style="display:none"></asp:TextBox>                                
                               <input id="storyBoardWindow" type="hidden"/>
 
    </div>
    </div>
    </form>
</body>
</html>
