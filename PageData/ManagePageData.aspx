<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePageData.aspx.cs" Inherits="ManagePageData" %>
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

		<link href="../styles/baseStyle.css" type="text/css" rel="stylesheet"/>
       <style type="text/css">
        body
        {
            width:100%;
            height:100%;
        	 background-color:#ffffff;
        	 font-family:Verdana;
        	 font-size:12px;
        }
 
           .style6
           {
               width: 303px;
           }
           .style8
           {
               width: 183px;
           }
           .style14
           {
               width: 20px;
           }
           .style20
           {
               width: 136px;
           }
           #form1
           {
               height: 800px;
           }
           .style21
           {
               width: 22px;
           }
           .style24
           {
               width: 23px;
           }
           .style25
           {
               width: 276px;
           }
           .style26
           {
               width: 277px;
           }
           .style27
           {
               width: 228px;
           }
           .style28
           {
               width: 18px;
           }
           </style>
      <script  language="javascript" type="text/javascript" src="../scripts/google_analytics_1.0.js"></script>
         <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script> 
       <script  language="javascript" type="text/javascript" src="../scripts/dialogs_1.26.min.js"></script>
       <script  language="javascript" type="text/javascript" src="../scripts/manage_data_1.3.min.js"></script>
         
</head>
<body onload="onManagePageDataLoad()">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">		
	</telerik:RadScriptManager>
	
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
    <ClientEvents  OnResponseEnd="reloadManageDataURL"/>    
         <AjaxSettings > 
                   <telerik:AjaxSetting AjaxControlID="AppDataSources">
          
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="AppDataSources" />
                    </UpdatedControls>
                    
                </telerik:AjaxSetting> 

            <telerik:AjaxSetting AjaxControlID="EventField">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="EventField" />
                         <telerik:AjaxUpdatedControl ControlID="ManageDataPanel"  />
                    </UpdatedControls>
                </telerik:AjaxSetting> 
          
           <telerik:AjaxSetting AjaxControlID="DeleteAppDataSource" >
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="AppDataSources" />
                         <telerik:AjaxUpdatedControl ControlID="SelectAppDataSource" />   
                         <telerik:AjaxUpdatedControl ControlID="EventField"  />
                         <telerik:AjaxUpdatedControl ControlID="ManageDataPanel"  />
                         <telerik:AjaxUpdatedControl ControlID="DeleteAppDataSource" />     
                        <telerik:AjaxUpdatedControl ControlID="EditAppDataSource" />     
                  </UpdatedControls>
                </telerik:AjaxSetting>                  
 
        <telerik:AjaxSetting AjaxControlID="RemovePageDataSource"  >
                    <UpdatedControls>
                         <telerik:AjaxUpdatedControl ControlID="RemovePageDataSource" />
                       <telerik:AjaxUpdatedControl ControlID="PageDataSources" />
                         <telerik:AjaxUpdatedControl ControlID="EventField"  />
                         <telerik:AjaxUpdatedControl ControlID="ManageDataPanel"  />
                    </UpdatedControls>
                </telerik:AjaxSetting> 
                
      <telerik:AjaxSetting AjaxControlID="PageDataSources">
                    <UpdatedControls>
                          <telerik:AjaxUpdatedControl ControlID="EventField"  />
                         <telerik:AjaxUpdatedControl ControlID="ManageDataPanel"  />
                           <telerik:AjaxUpdatedControl ControlID="PageDataSources" />
                    </UpdatedControls>
        </telerik:AjaxSetting> 

         <telerik:AjaxSetting AjaxControlID="SelectAppDataSource">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="SelectAppDataSource" />
                          <telerik:AjaxUpdatedControl ControlID="EventField"  />
                         <telerik:AjaxUpdatedControl ControlID="ManageDataPanel"  />
                           <telerik:AjaxUpdatedControl ControlID="PageDataSources" />
                    </UpdatedControls>
                </telerik:AjaxSetting> 
                
                <telerik:AjaxSetting AjaxControlID="AppDataSourcesPost">
          
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="AppDataSources" />
                         <telerik:AjaxUpdatedControl ControlID="SelectAppDataSource" />                        
                        <telerik:AjaxUpdatedControl ControlID="DeleteAppDataSource" />     
                        <telerik:AjaxUpdatedControl ControlID="EditAppDataSource" />     
                                           
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

            </Windows>
            </telerik:RadWindowManager>
    <asp:TextBox ID="SelectedPageDataSource" runat="server" style="display:none"></asp:TextBox>  
                <div  style="width:100%;background-color:white; vertical-align:top; text-align:left; height: 100%;">
                <table style="width:100%;">
                <tr><td>
                
                <div style="height: 35px">               
                <table style="width: 527px" align="left">
                <tr><td style="color:#000099;" align="left" class="style20">
                                All App Data Sources</td>
                  <td class="style6">
                      <asp:Button ID="AppDataSourcesPost" runat="server" Text="" 
                          onclick="AppDataSourcesPost_Click" style="display:none"/>
            <telerik:RadComboBox ID="AppDataSources" runat="server"   
                 Width="300px"  OnClientLoad="setDataSource" OnClientSelectedIndexChanged="onSelectAppDataSource"
                         >
                <Items>
                    
                </Items>
            </telerik:RadComboBox></td>
                    <td class="style21" > 
                                    <img alt="" id="NewAppDataSource" src="../images/new.gif" 
                                         onclick="newAppDataSource()" />
                    </td>
                    <td class="style24" >
                                  
                                   <asp:ImageButton id="EditAppDataSource" ImageUrl="~/images/edit.png" runat="server"
                                         onclientclick="editAppDataSource()" />
                    </td><td >
                                    <asp:ImageButton ID="DeleteAppDataSource" runat="server" ImageUrl="~/images/delete.gif" 
                                            onclick="DeleteAppDataSource_Click"  
                                         />
                        </td>
                        </tr>
                        </table>
                        <table><tr><td>
                        <div style="width:342px">
                        <div id="NewAppDataSourceContainer" style="text-align: left;display:none" 
                        align="left">
                        <table style="width: 342px" align="left">
                        <tr><td style="color: #000099" align="right">New Data Source Type</td>
                        <td>
            <telerik:RadComboBox ID="RadComboBox1" runat="server" Width="160px" 
                                OnClientSelectedIndexChanged="addAppDataSource" style="margin-left: 0px">
            <Items>
            <telerik:RadComboBoxItem Text="Select ->" Value="" />
            <telerik:RadComboBoxItem Text="Google Spreadsheet" Value="google_spreadsheet" />
            <telerik:RadComboBoxItem Text="RSS Feed" Value="rss_feed" />
            <telerik:RadComboBoxItem Text="Rest Web Service" Value="rest_web_service" />
            <telerik:RadComboBoxItem Text="Soap Web Service" Value="soap_web_service" />
            <telerik:RadComboBoxItem Text="SQL Database" Value="sql_database" />
            </Items>
            </telerik:RadComboBox>
        </td><td>
                                    <img id="cancel" src="../images/cancel.png" alt=""
                                          onclick="cancelNewAppDataSource();"/>
              </td></tr>
              </table>
                        </div>
                        </div>
                        </td>
                        <td>
      <asp:ImageButton ID="MySolutionsHelp" runat="server" ImageUrl="~/images/help.gif" 
        ToolTip="What is this page for?" OnClientClick="var oWin=radopen('../Help/PageData/PageDataHelp.htm', 'PageHelp'); 
        oWin.setSize(600,400);
                 oWin.set_visibleTitlebar(true);
                       oWin.set_visibleStatusbar(false);
                       oWin.set_modal(true); 
                        oWin.moveTo(100, 100); 
        return false;"/>

                   </td>
                   <td style="width:10px"></td>
                   <td>
                                <asp:Button ID="Button1" runat="server" Text="Spreadsheet User Guide" 
                           Width="162px" OnClientClick="PopUp('../Help/Guide/Default.htm', ' menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');return false;" />
                       </td>
                   </tr>
                   </table>
                       </div> 
                       <div style="height: 35px">               
                <table style="width: 832px" align="left">
                <tr><td style="color:#000099" 
                        class="style25">
                    <asp:Label ID="PageDataSourcesLabel" runat="server" 
                        Text="Select a Data Source to Use for this Page" />
                                </td>
                  <td class="style27">                    
                     
            <telerik:RadComboBox ID="PageDataSources" runat="server" Width="225px"  
                    style="margin-left: 0px"  
                          onselectedindexchanged="PageDataSources_SelectedIndexChanged" 
                          AutoPostBack="True"  >
            </telerik:RadComboBox></td>
                    <td class="style14"> 
                                    <img alt="" id="AddPageDataSource" src="../images/add.gif" 
                                         onclick="showSelectDataSource()" />
                    </td><td class="style28">
                                    <asp:ImageButton ID="RemovePageDataSource" runat="server" ImageUrl="~/images/delete_small.gif" 
                                         style="height: 16px" 
                                        Width="16px" onclick="RemovePageDataSource_Click"  />
                        </td><td>
                                    <asp:Label ID="Message" runat="server" ForeColor="Maroon"></asp:Label>
                        </td></tr>
                        </table>
                                                <div id="SelectDataSourceContainer" style="text-align: left;display:none" 
                        align="left">
                        <table style="width: 388px" align="left">
                        <tr><td style="color: #000099" align="right">Add App Data Source</td>
                        <td class="style8">
            <telerik:RadComboBox ID="SelectAppDataSource" runat="server" Width="225px" 
                                 style="margin-left: 0px" 
                                AutoPostBack="True"  OnClientSelectedIndexChanged="cancelShowPageDataSource"
                                onselectedindexchanged="SelectAppDataSource_SelectedIndexChanged">
            <Items>
             </Items>
            </telerik:RadComboBox>
        </td><td>
                                    <img id="Img1" src="../images/cancel.png" alt=""
                                          onclick="cancelShowPageDataSource();"/>
              </td></tr>
              </table>
                        </div>

                       </div>
                       <div id="EventFieldContainer" runat="server" style="height:36px; display:inline">
                <table style="width: 100%; height: 32px;" align="left">
                <tr><td style="color:#000099" 
                        class="style26">
                                Select a Page or Field to Start Operations <br /> 
                                on the Selected Data Source</td>
                  <td>
            <telerik:RadComboBox ID="EventField" runat="server" Width="225px" 
                          style="margin-left: 0px" AutoPostBack="True" 
                          onselectedindexchanged="EventField_SelectedIndexChanged" >                          
                <Items>                    
                </Items>
            </telerik:RadComboBox></td>
                    </tr>
                        </table>
                                                </div>
                  </td></tr>
                  <tr><td>  
                                                                                   
   <telerik:RadAjaxPanel id="ManageDataPanel" align="left" runat="server"  style="width:100%;height:840px; border:1px solid #cccccc; vertical-align:top;display:block" >
         <iframe id="ManageData" src="" width="100%" height="100%" frameborder="0"  runat="server"/>           
   </telerik:RadAjaxPanel>  
    </td>
    </tr>
    </table>
    </div>
     <telerik:RadToolTip runat="server" ID="HelpTip1" Width = "175px" Height = "25px" TargetControlId = "NewAppDataSource" 
        IsClientID="true" Animation = "Fade" Position="BottomCenter"><span style="color:Blue;font-size:14px;">New App Data Source
            </span></telerik:RadToolTip>
            <telerik:RadToolTip runat="server" ID="RadToolTip1" Width = "175px" Height = "25px" TargetControlId = "EditAppDataSource" 
        IsClientID="true" Animation = "Fade" Position= "BottomCenter"><span style="color:Blue;font-size:14px;">Edit App Data Source
            </span></telerik:RadToolTip>
            <telerik:RadToolTip runat="server" ID="RadToolTip2" Width = "175px" Height = "25px" TargetControlId = "DeleteAppDataSource" 
        IsClientID="true" Animation = "Fade" Position= "BottomCenter"><span style="color:Blue;font-size:14px;">Delete App Data Source
            </span></telerik:RadToolTip>
            <telerik:RadToolTip runat="server" ID="RadToolTip3" Width = "175px" Height = "25px" TargetControlId = "AddPageDataSource" 
        IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Add Page Data Source
            </span></telerik:RadToolTip>
            <telerik:RadToolTip runat="server" ID="RadToolTip4" Width = "400px" Height = "25px" TargetControlId = "RemovePageDataSource" 
        IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Remove Data Source From Page But Not From App
            </span></telerik:RadToolTip>
    </form>
</body>
</html>
