<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoogleSpreadsheetOperations.aspx.cs" Inherits="ManageData_GoogleSpreadsheetOperations" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

td
{
	MARGIN-TOP: 0px;  
	MARGIN-LEFT: 0px; 
	font-family:Verdana; 
	font-size:12px ;
	color:#333333;
}

           #form1
        {
            width: 873px;
        }
           .style13
        {
            width: 185px;
        }
        .style14
        {
            width: 74px;
        }
        .style15
        {
            width: 87px;
        }
        .style16
        {
            width: 158px;
        }
           </style>
   <script type="text/javascript" src="../jquery/js/jquery-1.5.1.min.js"></script>
   <script type="text/javascript" src="../jquery/js/jquery-ui-1.8.13.custom.min.js"></script>
   <script language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
   <script language="javascript" type="text/javascript" src="../scripts/browser_1.4.js"></script>
   <script  language="javascript" type="text/javascript" src="../scripts/storyboard_popup_1.5.min.js"></script> 
   <script  language="javascript" type="text/javascript" src="../scripts/google_analytics_1.0.js"></script>
   <script  language="javascript" type="text/javascript" src="../scripts/dialogs_1.26.min.js"></script>
    <script  language="javascript" type="text/javascript" src="../scripts/database_script_1.13.min.js"></script>
    <script  language="javascript" type="text/javascript" src="../scripts/manage_data_1.3.min.js"></script>
   <script  language="javascript" type="text/javascript">
       function checkError() {
           var Message = document.getElementById("Message");
           if (Message.innerHTML.length > 0 && Message.innerHTML.indexOf("Query Error:") >=0 ) {
               alert(Message.innerHTML);
               Message.innerHTML = '';
           }
       }
   </script>
    
</head>
<body onload="checkError()">
    <form id="form1" runat="server">                          
     	<telerik:RadScriptManager ID="RadScriptManagerSpreadsheet" runat="server"/>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnResponseEnd="checkSessionTimeOut"/>

  <AjaxSettings>    
  
   <telerik:AjaxSetting AjaxControlID="AddDeviceAction">
                    <UpdatedControls>
                       <telerik:AjaxUpdatedControl ControlID="SpreadsheetCommandsView"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="SessionTimeOut"></telerik:AjaxUpdatedControl>                      
                    </UpdatedControls>
                </telerik:AjaxSetting> 

   <telerik:AjaxSetting AjaxControlID="StoryBoardDisplayType">
                    <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="SessionTimeOut"></telerik:AjaxUpdatedControl>                      
                       <telerik:AjaxUpdatedControl ControlID="PageTreeView" />
                    </UpdatedControls>
                </telerik:AjaxSetting> 
  
          <telerik:AjaxSetting AjaxControlID="AddIfCondition">
                    <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="SessionTimeOut"></telerik:AjaxUpdatedControl>                      
                       <telerik:AjaxUpdatedControl ControlID="Message" />
                       <telerik:AjaxUpdatedControl ControlID="SpreadsheetCommandsView"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting> 

     <telerik:AjaxSetting AjaxControlID="AddQuery">
                    <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="SessionTimeOut"></telerik:AjaxUpdatedControl>                      
                       <telerik:AjaxUpdatedControl ControlID="Message" />
                       <telerik:AjaxUpdatedControl ControlID="SpreadsheetCommandsView"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting> 
          
           <telerik:AjaxSetting AjaxControlID="SaveDatabaseConfig">
                    <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="SessionTimeOut"></telerik:AjaxUpdatedControl>                      
                       <telerik:AjaxUpdatedControl ControlID="Message" />
                       <telerik:AjaxUpdatedControl ControlID="SpreadsheetCommandsView"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting> 

                 <telerik:AjaxSetting AjaxControlID="ResetDatabaseConfig">
                    <UpdatedControls>
                          <telerik:AjaxUpdatedControl ControlID="SessionTimeOut"></telerik:AjaxUpdatedControl>                      
                      <telerik:AjaxUpdatedControl ControlID="Message" />
                         <telerik:AjaxUpdatedControl ControlID="SpreadsheetCommandsView"></telerik:AjaxUpdatedControl>
                  </UpdatedControls>
                </telerik:AjaxSetting> 

                <telerik:AjaxSetting AjaxControlID="DBTreeClick">
                    <UpdatedControls>
                          <telerik:AjaxUpdatedControl ControlID="SessionTimeOut"></telerik:AjaxUpdatedControl>                      
                      <telerik:AjaxUpdatedControl ControlID="Message" />
                        <telerik:AjaxUpdatedControl ControlID="SpreadsheetCommandsView"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>       

    </AjaxSettings>
    	</telerik:RadAjaxManager>
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Telerik">
            <Windows>
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

            </Windows>
            </telerik:RadWindowManager>
   
                <div style="width:100%; text-align: left; ">
                    <table style="width:100%;"><tr>
                    <td style="width:400px" align="left" valign="top">
                    <table><tr><td>
                    <asp:Label ID="Label25" runat="server" 
                            Font-Bold="True" Font-Names="Arial" 
            Font-Size="11pt" ForeColor="#003399" Text="StoryBoard" Width="178px"></asp:Label></td></tr><tr><td>
            <telerik:RadButton ID="StoryBoardDisplayType" runat="server" ButtonType="StandardButton" Skin="Office2007" 
                    ToggleType="CustomToggle" AutoPostBack="true" onclick="StoryBoardDisplayType_Click">
                    <ToggleStates>
                        <telerik:RadButtonToggleState  Text="Show All Pages" />
                        <telerik:RadButtonToggleState  Text="Show Only Current Page" />
                    </ToggleStates>
                </telerik:RadButton>
                            </td></tr><tr><td>
                    <telerik:RadTreeView ID="PageTreeView" Runat="server" 
                             Skin="Web20" 
                             BorderColor="#CCCCFF"
                              BorderStyle="Dashed"
                              BorderWidth="1px"
                              ForeColor="Navy"
                              Width="400px"
                              Height="850px"
                              OnClientNodeClicked="PageTreeViewNode_Clicked"
                              ></telerik:RadTreeView>
                              </td></tr></table>
                    </td>
                        <td class="style94">&#160;&#160;</td>
                        <td valign="top">
                        <table>
                        <tr><td>
                        <table width="100%"><tr><td class="style13">
                    <asp:Label ID="Label24" runat="server" 
                            Font-Bold="True" Font-Names="Arial" 
            Font-Size="11pt" ForeColor="#003399" Text="Spreadsheet Commands" Width="178px"></asp:Label>
            </td><td class="style14">
                           </td><td class="style15">
                           </td><td class="style16">
                    <asp:Button ID="Button9" runat="server" CausesValidation="False" 
    Font-Names="Arial" Font-Size="10pt" OnClientClick="PopUp('../Help/PageData/GoogleDocsGuide.htm', 'height=800, width=820, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');return false;" 
    Text="Interface Users Guide" Width="145px" /></td><td>
                        <asp:ImageButton ID="GoogleDocsHelpButton" runat="server" 
    ImageUrl="~/images/help.gif" OnClientClick="var oWin=radopen('../Help/PageData/GoogleDocsHelp.htm', 'DatabaseHelp'); 
                        oWin.setSize(850,450);
                                    oWin.set_visibleTitlebar(true);
                                        oWin.set_visibleStatusbar(false);
                                        oWin.set_modal(true); 
                                        oWin.moveTo(0, 0); 
                        return false;" 
    ToolTip="How do I manage data with Google Docs spreadsheets?" /></td></tr></table>
            </td></tr><tr><td bgcolor="#CCFFCC">
                            <asp:Label ID="Message" runat="server" 
                                Font-Bold="False" Font-Names="Arial" 
        Font-Size="10pt" ForeColor="#003399" 
        Text="To set device fields in the command tree, drag fields from storyboard and drop in nodes below. " 
        Width="546px"></asp:Label></td></tr>
        <tr><td><table><tr><td>
    <telerik:RadButton ID="AddIfCondition" runat="server" Text="Add Condition" Skin="Office2007" 
      Icon-PrimaryIconUrl="~/images/decision.gif" onclick="AddIfCondition_Click" >
<Icon PrimaryIconUrl="~/images/decision.gif"></Icon>
    </telerik:RadButton>
</td><td>
 </td><td>
     <telerik:RadButton ID="AddQuery" runat="server" Text="Add Query" Skin="Office2007"
       onclick="AddQuery_Click" >
<Icon PrimaryIconUrl="~/images/database.gif"></Icon>
     </telerik:RadButton>
            </td>
             <td>
     <telerik:RadButton ID="AddDeviceAction" runat="server" Text="Add Device Action" Skin="Office2007"
        onclick="AddDeviceAction_Click" >
<Icon PrimaryIconUrl="~/images/goto_page.gif"></Icon>
     </telerik:RadButton>
            </td>
            <td>
     <telerik:RadButton ID="SaveDatabaseConfig" runat="server" Text="Save" Skin="Office2007"
     onclick="SaveDatabaseConfig_Click"  >
<Icon PrimaryIconUrl="~/images/save.png"></Icon>
     </telerik:RadButton>
            </td>
            <td>
     <telerik:RadButton ID="ResetDatabaseConfig" runat="server" Text="Clear All" Skin="Office2007"
        onclick="ResetDatabaseConfig_Click" >
<Icon PrimaryIconUrl="~/images/delete.gif"></Icon>
     </telerik:RadButton>
            </td></tr></table></td></tr>
        
        <tr><td align="left" valign="top">
                <telerik:RadTreeView ID="SpreadsheetCommandsView" Runat="server" 
            BorderColor="#CCCCFF" BorderStyle="Dashed" 
            BorderWidth="1px" EnableDragAndDrop="false" ForeColor="Navy" Height="1000px" 
            OnclientLoad="SpreadsheetCommandsView_load" 
            Skin="Web20" Width="800px"></telerik:RadTreeView></td></tr></table></td>
                        </tr></table></div>
                                          
                     <input id="SelectedAppType" type="hidden" runat="server" />
         <input id="XScaleFactor" type="hidden" runat="server" />
         <input id="YScaleFactor" type="hidden" runat="server" />
         <asp:TextBox ID="DBTreeInfo" runat="server" style="display:none" Width="4px"></asp:TextBox>
          <asp:TextBox ID="SessionTimeOut" runat="server" style="display:none" Width="4px"></asp:TextBox>         
   <asp:Button ID="DBTreeClick" runat="server" onclick="DBTreeClick_Click" style="display:none" Width="1px" />
    </form>
</body>
</html>
