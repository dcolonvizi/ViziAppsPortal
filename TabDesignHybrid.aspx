<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TabDesignHybrid.aspx.cs" Inherits="TabDesignHybrid" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ViziApps: Build Web Mobile Apps Online</title>
    		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
        <meta http-equiv="Pragma" content="no-cache"/>
        <meta http-equiv="Expires" content="-1"/>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>

		<link href="styles/baseStyle.css" type="text/css" rel="stylesheet"/>
 		<link href="jquery/css/cupertino/jquery-ui-1.8.13.custom.css" type="text/css" rel="stylesheet"/>
       <style type="text/css">
        body
        {
        	 background-color:#ffffff;
        }
        .MyModalPanel
        {
	        position:absolute;
	        top:0;
	        left:0;
	        width:100%;
	        height:100%;
        }
        .edit_tool_text
        {
            font-family:verdana;
            font-size:11px;
            width:113px;
            font-weight:bold;
        }
        .edit_tool_button
        {
            width:135px;
            height:26px;
            text-align:left;
            vertical-align:top;
        }
            .style2
           {
               height: 26px;
           }
            .style3
           {
               height: 26px;
               width: 251px;
           }
            .style12
           {
               width: 119px;
               height: 34px;
           }
           .style13
           {
               height: 34px;
           }
           .style14
           {
               width: 32px;
               height: 34px;
           }
           .style15
           {
               width: 30px;
               height: 34px;
           }
            .style16
           {
               height: 9px;
           }
           .style17
           {
               height: 9px;
               width: 251px;
           }
            .style18
           {
               height: 19px;
           }
            .style19
           {
               height: 18px;
           }
            .style20
           {
               height: 25px;
           }
           .style21
           {
               height: 25px;
               width: 251px;
           }
            .style22
           {
               height: 8px;
           }
           .style23
           {
               width: 87px;
               height: 41px;
           }
           .style24
           {
               height: 41px;
           }
            .style25
           {
               height: 34px;
               width: 40px;
           }
            .style26
           {
               height: 34px;
               width: 66px;
           }
           .style27
           {
               height: 34px;
               width: 125px;
           }
            </style>
             <script  language="javascript" type="text/javascript" src="scripts/google_analytics_1.0.js"/>
        <script  language="javascript" type="text/javascript" src="scripts/default_script_1.6.js"/>
        <script  language="javascript" type="text/javascript" src="scripts/storyboard_1.4.js"></script>          
        <script language="javascript" type="text/javascript" src="scripts/browser_1.4.js"></script> 
        <script  language="javascript" type="text/javascript" src="jquery/js/jquery-1.5.1.min.js"></script>  
        <script type="text/javascript" src="jquery/js/jquery-ui-1.8.13.custom.min.js"></script>
        <script  language="javascript" type="text/javascript" src="scripts/dialogs_1.26.min.js"></script>
        <script language="javascript" type="text/javascript" src="EditorTools/js/editor_custom_tools_2.41.min.js"></script>
        <script  language="javascript" type="text/javascript">
            function PopUp(url, features) {
                var PUtest = window.open(url, '_blank', features);
                if (PUtest == null) {
                    alert('For correct operation, popups need to be allowed from this website.');
                }
            }
        </script>          
         
 </head>
<body onload="onCurrentAppChanged();">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">		
	</telerik:RadScriptManager>
	
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <ClientEvents OnResponseEnd="onWebAppsDesignAjaxResponseEnd" OnRequestStart="onWebAppsDesignAjaxRequestStart" />
  <AjaxSettings> 
  
         <telerik:AjaxSetting AjaxControlID="Undo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>   
                    <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>     
                    <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>   
                     <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                     <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                     <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                     <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml" />
                     <telerik:AjaxUpdatedControl ControlID="Message"/>  
                     <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>   
                </UpdatedControls>
            </telerik:AjaxSetting>  
            
          <telerik:AjaxSetting AjaxControlID="ValidateFieldNames">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="Message"/>  
                  </UpdatedControls>
            </telerik:AjaxSetting>   
            
           <telerik:AjaxSetting AjaxControlID="AppProperties">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="AppProperties"/>  
                  </UpdatedControls>
            </telerik:AjaxSetting>    

            <telerik:AjaxSetting AjaxControlID="SaveUrlAccountIdentifier">
                <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>  
                     <telerik:AjaxUpdatedControl ControlID="EditorTools"/>
                         <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                
                     <telerik:AjaxUpdatedControl ControlID="Message"/>  
                      <telerik:AjaxUpdatedControl ControlID="PagePanel"/>                      
                      <telerik:AjaxUpdatedControl ControlID="DesignMessage" /> 
                      <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>    
                      <telerik:AjaxUpdatedControl ControlID="CurrentAppContainer" /> 
                       <telerik:AjaxUpdatedControl ControlID="PreviewNote" />   
                                                            
              </UpdatedControls>
            </telerik:AjaxSetting>       
            <telerik:AjaxSetting AjaxControlID="DisplayMode">
                <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>   
                     <telerik:AjaxUpdatedControl ControlID="EditorTools"/>
                         <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                   
                     <telerik:AjaxUpdatedControl ControlID="Message"/>  
                     <telerik:AjaxUpdatedControl ControlID="PagePanel"/>                      
                      <telerik:AjaxUpdatedControl ControlID="DesignMessage" /> 
                      <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>    
                       <telerik:AjaxUpdatedControl ControlID="CurrentAppContainer" /> 
                       <telerik:AjaxUpdatedControl ControlID="PreviewNote" />  
                        <telerik:AjaxUpdatedControl ControlID="storyBoardPanelWebApp" />                        
                         <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/> 
                          <telerik:AjaxUpdatedControl ControlID="DisplayModeButton"/>                                                             
              </UpdatedControls>
            </telerik:AjaxSetting>    
              
            <telerik:AjaxSetting AjaxControlID="NewApplication" >
                <UpdatedControls>                           
                        <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>  
                         <telerik:AjaxUpdatedControl ControlID="AppProperties"/>  
                         <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                  
                      <telerik:AjaxUpdatedControl ControlID="PagePanel"/>  
                       <telerik:AjaxUpdatedControl ControlID="CurrentApp" />                       
                     <telerik:AjaxUpdatedControl ControlID="NewApplication" />
                     <telerik:AjaxUpdatedControl ControlID="RenameApp" />
                    <telerik:AjaxUpdatedControl ControlID="DuplicateApp"/>  
                    <telerik:AjaxUpdatedControl ControlID="ConvertAppType" /> 
                     <telerik:AjaxUpdatedControl ControlID="DeleteApp" />
                     <telerik:AjaxUpdatedControl ControlID="DisplayModeButton" />
                     <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                     <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                     <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                   <telerik:AjaxUpdatedControl ControlID="StoryBoardTip" />                    
                     <telerik:AjaxUpdatedControl ControlID="Message" />
                     <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>   
                     <telerik:AjaxUpdatedControl ControlID="AppName" />   
                      <telerik:AjaxUpdatedControl ControlID="AllAppNames" />                       
                      <telerik:AjaxUpdatedControl ControlID="AppPropertiesLabel"/>                             
                         <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
           </UpdatedControls>
            </telerik:AjaxSetting>

           <telerik:AjaxSetting AjaxControlID="SelectForTest" >
                <UpdatedControls>                           
                        <telerik:AjaxUpdatedControl ControlID="AppSelectedForTest" LoadingPanelID="ConfigureLoadingPanel"/>   
                 </UpdatedControls>
            </telerik:AjaxSetting>

            
            <telerik:AjaxSetting AjaxControlID="CurrentApp">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppProperties"/>  
                      <telerik:AjaxUpdatedControl ControlID="AppPropertiesLabel"/>                             
                     <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                 
                     <telerik:AjaxUpdatedControl ControlID="PagePanel" />
                     <telerik:AjaxUpdatedControl ControlID="RenameApp" />
                     <telerik:AjaxUpdatedControl ControlID="DuplicateApp"/>   
                     <telerik:AjaxUpdatedControl ControlID="ConvertAppType" /> 
                     <telerik:AjaxUpdatedControl ControlID="DeleteApp" />
                       <telerik:AjaxUpdatedControl ControlID="DisplayModeButton" />
                     <telerik:AjaxUpdatedControl ControlID="StoryBoardTip" />                    
                     <telerik:AjaxUpdatedControl ControlID="Message" />
                     <telerik:AjaxUpdatedControl ControlID="StartMessage" />
                     <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>   
                     <telerik:AjaxUpdatedControl ControlID="AppName" />   
                      <telerik:AjaxUpdatedControl ControlID="AllAppNames" /> 
                     <telerik:AjaxUpdatedControl ControlID="PageName" />                                   
                     <telerik:AjaxUpdatedControl ControlID="CurrentApp" />  
                     <telerik:AjaxUpdatedControl ControlID="DeviceType" />                                          
                     <telerik:AjaxUpdatedControl ControlID="DefaultButtonImage"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                     <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                     <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>    
                     <telerik:AjaxUpdatedControl ControlID="storyBoardPanelWebApp" LoadingPanelID="ConfigureLoadingPanel"/>   
                     <telerik:AjaxUpdatedControl ControlID="DesignMessage" />                     
                     <telerik:AjaxUpdatedControl ControlID="SetBackgroundImage"/>  
                     <telerik:AjaxUpdatedControl ControlID="SetBackgroundColor"/>                    
                     <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
                     <telerik:AjaxUpdatedControl ControlID="DisplayModeButton"/>  
                     <telerik:AjaxUpdatedControl ControlID="SelectForTest"/>  
                      <telerik:AjaxUpdatedControl ControlID="ValidateFieldNames"/>                    
                      <telerik:AjaxUpdatedControl ControlID="AppSelectedForTest"/>                                                              
                </UpdatedControls>
            </telerik:AjaxSetting>
              
          <telerik:AjaxSetting AjaxControlID="ViewForDevice">
                <UpdatedControls>
                       <telerik:AjaxUpdatedControl ControlID="ViewForDevice"/>                             
                        <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>  
                         <telerik:AjaxUpdatedControl ControlID="AppProperties"/>      
                     <telerik:AjaxUpdatedControl ControlID="EditorTools"/>
                         <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                  
                     <telerik:AjaxUpdatedControl ControlID="Message"/> 
                     <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>    
                       <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>     
                      <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
             </UpdatedControls>
            </telerik:AjaxSetting>

               <telerik:AjaxSetting AjaxControlID="DeleteApp">
                <UpdatedControls>                      
                       <telerik:AjaxUpdatedControl ControlID="DeviceType" />                                           
                       <telerik:AjaxUpdatedControl ControlID="CurrentApp" />                       
                      <telerik:AjaxUpdatedControl ControlID="Message"/> 
                      <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>     
                      <telerik:AjaxUpdatedControl ControlID="StartMessage" />
                      <telerik:AjaxUpdatedControl ControlID="DeployToStagingButton"/>                          
                     <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/> 
                     <telerik:AjaxUpdatedControl ControlID="AppProperties"/>       
                     <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                               
                      <telerik:AjaxUpdatedControl ControlID="PagePanel" />
                     <telerik:AjaxUpdatedControl ControlID="AppsMultiPage" />
                      <telerik:AjaxUpdatedControl ControlID="DuplicateApp"/> 
                      <telerik:AjaxUpdatedControl ControlID="ConvertAppType" />   
                      <telerik:AjaxUpdatedControl ControlID="DeleteApp"/>   
                      <telerik:AjaxUpdatedControl ControlID="RenameApp"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                     <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                     <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                    <telerik:AjaxUpdatedControl ControlID="StoryBoardTip" />                    
                    <telerik:AjaxUpdatedControl ControlID="DefaultButtonImage"/>    
                      <telerik:AjaxUpdatedControl ControlID="AppName" />   
                      <telerik:AjaxUpdatedControl ControlID="AllAppNames" /> 
                    <telerik:AjaxUpdatedControl ControlID="DesignMessage" />   
                    <telerik:AjaxUpdatedControl ControlID="storyBoardPanelWebApp" />                         
                      <telerik:AjaxUpdatedControl ControlID="AppPropertiesLabel"/>                             
                         <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/> 
                          <telerik:AjaxUpdatedControl ControlID="DisplayModeButton"/>  
                     <telerik:AjaxUpdatedControl ControlID="SelectForTest"/>  
                      <telerik:AjaxUpdatedControl ControlID="ValidateFieldNames"/>  
                      <telerik:AjaxUpdatedControl ControlID="AppSelectedForTest"/>    
        </UpdatedControls>
            </telerik:AjaxSetting>
            
            
         <telerik:AjaxSetting AjaxControlID="SaveAppPost">
                <UpdatedControls>
                     
                       <telerik:AjaxUpdatedControl ControlID="CurrentApp" />   
                        <telerik:AjaxUpdatedControl ControlID="DeviceType" />                                           
                        <telerik:AjaxUpdatedControl ControlID="Message"/>  
                        <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>                   
                        <telerik:AjaxUpdatedControl ControlID="PagePanel" />                          
                        <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>   
                         <telerik:AjaxUpdatedControl ControlID="AppProperties"/>
                        <telerik:AjaxUpdatedControl ControlID="storyBoardPanelWebApp" LoadingPanelID="ConfigureLoadingPanel"/> 
                       <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                        
                        <telerik:AjaxUpdatedControl ControlID="DuplicateApp"/>  
                        <telerik:AjaxUpdatedControl ControlID="ConvertAppType" />  
                        <telerik:AjaxUpdatedControl ControlID="DeleteApp"/>              
                        <telerik:AjaxUpdatedControl ControlID="DisplayModeButton" />
                        <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                        <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                        <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                         <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                         <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                        <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                         <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                         <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                         <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                        <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                         <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                        <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                         <telerik:AjaxUpdatedControl ControlID="StoryBoardTip" />                    
                        <telerik:AjaxUpdatedControl ControlID="RenameApp"/>              
                        <telerik:AjaxUpdatedControl ControlID="AppDescription" />  
                         <telerik:AjaxUpdatedControl ControlID="PageName" />  
                         <telerik:AjaxUpdatedControl ControlID="DesignedFor" />  
                         <telerik:AjaxUpdatedControl ControlID="AppName" />   
                        <telerik:AjaxUpdatedControl ControlID="AllAppNames" />        
                        <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>      
                        <telerik:AjaxUpdatedControl ControlID="AppProperties"/>                      
                         <telerik:AjaxUpdatedControl ControlID="AppPropertiesLabel"/> 
          </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="SaveAppAsPost">                    
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CurrentApp" />                       
                     <telerik:AjaxUpdatedControl ControlID="AppProperties" /> 
                       <telerik:AjaxUpdatedControl ControlID="DeviceType" />                                           
                     <telerik:AjaxUpdatedControl ControlID="Message"/>   
                     <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>     
                    <telerik:AjaxUpdatedControl ControlID="StartMessage" />
                      <telerik:AjaxUpdatedControl ControlID="PagePanel" />                          
                    <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage" LoadingPanelID="WholePageLoadingPanel"/>  
                     <telerik:AjaxUpdatedControl ControlID="storyBoardPanelWebApp" />      
                    <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                    
                    <telerik:AjaxUpdatedControl ControlID="DuplicateApp"/> 
                    <telerik:AjaxUpdatedControl ControlID="ConvertAppType" />   
                    <telerik:AjaxUpdatedControl ControlID="DeleteApp"/>              
                      <telerik:AjaxUpdatedControl ControlID="DisplayModeButton" />
                     <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                     <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                     <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                   <telerik:AjaxUpdatedControl ControlID="StoryBoardTip" />                    
                         <telerik:AjaxUpdatedControl ControlID="RenameApp"/>              
                        <telerik:AjaxUpdatedControl ControlID="AppDescription" />  
                         <telerik:AjaxUpdatedControl ControlID="PageName" />  
                         <telerik:AjaxUpdatedControl ControlID="AppName" />                    
                      <telerik:AjaxUpdatedControl ControlID="AllAppNames" /> 
                        <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
                       <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>  
                       <telerik:AjaxUpdatedControl ControlID="AppProperties"/>   
                     <telerik:AjaxUpdatedControl ControlID="AppPropertiesLabel"/>  
                      <telerik:AjaxUpdatedControl ControlID="AppSelectedForTest"/>   
                     <telerik:AjaxUpdatedControl ControlID="SelectForTest"/> 
                      <telerik:AjaxUpdatedControl ControlID="ValidateFieldNames"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppSelectedForTest"/>     
             </UpdatedControls>
            </telerik:AjaxSetting>    

                     <telerik:AjaxSetting AjaxControlID="SaveApp">
                <UpdatedControls>
                       <telerik:AjaxUpdatedControl ControlID="CurrentApp" />                       
                     <telerik:AjaxUpdatedControl ControlID="Message"/>  
                     <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>                   
                   <telerik:AjaxUpdatedControl ControlID="PagePanel" />                          
                         <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>      
                      <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>     
                         <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                   
                      <telerik:AjaxUpdatedControl ControlID="DuplicateApp"/>   
                      <telerik:AjaxUpdatedControl ControlID="ConvertAppType" /> 
                         <telerik:AjaxUpdatedControl ControlID="DeleteApp"/>              
                      <telerik:AjaxUpdatedControl ControlID="DisplayModeButton" />
                     <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                     <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                    <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                    <telerik:AjaxUpdatedControl ControlID="StoryBoardTip" />                    
                         <telerik:AjaxUpdatedControl ControlID="RenameApp"/>              
                        <telerik:AjaxUpdatedControl ControlID="AppDescription" />  
                         <telerik:AjaxUpdatedControl ControlID="PageName" />  
                         <telerik:AjaxUpdatedControl ControlID="AppName" />                    
                      <telerik:AjaxUpdatedControl ControlID="AllAppNames" /> 
                        <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/> 
                  <telerik:AjaxUpdatedControl ControlID="AppProperties"/> 
                 <telerik:AjaxUpdatedControl ControlID="AppPropertiesLabel"/>     
              </UpdatedControls>
            </telerik:AjaxSetting>   
            
            <telerik:AjaxSetting AjaxControlID="SaveAppPagePost">
                <UpdatedControls>
                       <telerik:AjaxUpdatedControl ControlID="CurrentApp" />                       
                     <telerik:AjaxUpdatedControl ControlID="Message"/>  
                     <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>                   
                   <telerik:AjaxUpdatedControl ControlID="PagePanel" />                          
                        <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>  
                      <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>     
                          <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                      
                      <telerik:AjaxUpdatedControl ControlID="DuplicateApp"/>  
                      <telerik:AjaxUpdatedControl ControlID="ConvertAppType" />  
                         <telerik:AjaxUpdatedControl ControlID="DeleteApp"/>              
                     <telerik:AjaxUpdatedControl ControlID="DisplayModeButton" />
                     <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                     <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                     <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                   <telerik:AjaxUpdatedControl ControlID="StoryBoardTip" />                    
                         <telerik:AjaxUpdatedControl ControlID="RenameApp"/>              
                        <telerik:AjaxUpdatedControl ControlID="AppDescription" />  
                         <telerik:AjaxUpdatedControl ControlID="PageName" />  
                         <telerik:AjaxUpdatedControl ControlID="AppName" />          
                      <telerik:AjaxUpdatedControl ControlID="AllAppNames" /> 
                        <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
          </UpdatedControls>
            </telerik:AjaxSetting> 
              <telerik:AjaxSetting AjaxControlID="SetViewForDeviceTypePost">
                <UpdatedControls>
                       <telerik:AjaxUpdatedControl ControlID="CurrentApp" />                       
                     <telerik:AjaxUpdatedControl ControlID="Message"/>  
                     <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>                   
                   <telerik:AjaxUpdatedControl ControlID="PagePanel" />                          
                        <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/> 
                         <telerik:AjaxUpdatedControl ControlID="AppProperties"/>      
                          <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>   
                      <telerik:AjaxUpdatedControl ControlID="RadScriptBlock1"/>                             
                      <telerik:AjaxUpdatedControl ControlID="DuplicateApp"/>   
                      <telerik:AjaxUpdatedControl ControlID="ConvertAppType" /> 
                         <telerik:AjaxUpdatedControl ControlID="DeleteApp"/>              
                     <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                     <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                     <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                         <telerik:AjaxUpdatedControl ControlID="RenameApp"/>              
                        <telerik:AjaxUpdatedControl ControlID="AppDescription" />  
                         <telerik:AjaxUpdatedControl ControlID="PageName" />  
                         <telerik:AjaxUpdatedControl ControlID="AppName" />          
                      <telerik:AjaxUpdatedControl ControlID="AllAppNames" /> 
                    <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>  
                     <telerik:AjaxUpdatedControl ControlID="SelectForTest" /> 
                      <telerik:AjaxUpdatedControl ControlID="ValidateFieldNames"/>    
                      <telerik:AjaxUpdatedControl ControlID="AppSelectedForTest" />   
                         <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
        </UpdatedControls>
            </telerik:AjaxSetting> 


             <telerik:AjaxSetting AjaxControlID="SetBackgroundPost">
                <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="PagePanel" />                          
                        <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/> 
                         <telerik:AjaxUpdatedControl ControlID="AppProperties"/>      
                         <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                           
                       <telerik:AjaxUpdatedControl ControlID="Background"/>                                                    
            </UpdatedControls>
            </telerik:AjaxSetting>             

             <telerik:AjaxSetting AjaxControlID="DuplicateAppPost">
                <UpdatedControls>
                       <telerik:AjaxUpdatedControl ControlID="CurrentApp" />                       
                     <telerik:AjaxUpdatedControl ControlID="Message"/>    
                     <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>                 
                   <telerik:AjaxUpdatedControl ControlID="PagePanel" />                          
                         <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>  
                         <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                 
                        <telerik:AjaxUpdatedControl ControlID="AppPages"/>  
                        <telerik:AjaxUpdatedControl ControlID="PageName"/> 
                       <telerik:AjaxUpdatedControl ControlID="DuplicateApp"/> 
                       <telerik:AjaxUpdatedControl ControlID="ConvertAppType" />   
                        <telerik:AjaxUpdatedControl ControlID="DeleteApp"/>              
                     <telerik:AjaxUpdatedControl ControlID="DisplayModeButton" />
                      <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                     <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                     <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                  <telerik:AjaxUpdatedControl ControlID="StoryBoardTip" />                    
                         <telerik:AjaxUpdatedControl ControlID="RenameApp"/>              
                     <telerik:AjaxUpdatedControl ControlID="AppName" />   
                      <telerik:AjaxUpdatedControl ControlID="AllAppNames" /> 
                          <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
                        <telerik:AjaxUpdatedControl ControlID="AppSelectedForTest"/>                          
         </UpdatedControls>
            </telerik:AjaxSetting>
  
        <telerik:AjaxSetting AjaxControlID="DuplicatePagePost">
                <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/> 
                         <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>           
                         <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                
                         <telerik:AjaxUpdatedControl ControlID="AppPages"/>                                         
                         <telerik:AjaxUpdatedControl ControlID="PageName"/>               
                      <telerik:AjaxUpdatedControl ControlID="Message"/> 
                      <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>               
                      <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                       <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                   <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                        <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
          </UpdatedControls>
            </telerik:AjaxSetting>
            
           <telerik:AjaxSetting AjaxControlID="NewPagePost">
                <UpdatedControls>                          
                         <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/> 
                          <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>           
                         <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                   
                         <telerik:AjaxUpdatedControl ControlID="AppPages"/>                                       
                         <telerik:AjaxUpdatedControl ControlID="PageName"/>               
                      <telerik:AjaxUpdatedControl ControlID="Message"/>    
                      <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>             
                      <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                      <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                    <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                         <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
           </UpdatedControls>
            </telerik:AjaxSetting>                  
                   
              <telerik:AjaxSetting AjaxControlID="RenameAppPost">
                <UpdatedControls>                          
                           <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage" />   
                          <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                               
                      <telerik:AjaxUpdatedControl ControlID="Message"/> 
                      <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>   
                       <telerik:AjaxUpdatedControl ControlID="CurrentApp" LoadingPanelID="ConfigureLoadingPanel"/>                   
                     <telerik:AjaxUpdatedControl ControlID="AppName" />   
                      <telerik:AjaxUpdatedControl ControlID="AllAppNames" /> 
                      <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                     <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                     <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                        <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
            </UpdatedControls>
            </telerik:AjaxSetting>    

             <telerik:AjaxSetting AjaxControlID="RenamePagePost">
                <UpdatedControls>                          
                          <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>  
                       <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>     
                        <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                
                         <telerik:AjaxUpdatedControl ControlID="AppPages"/>
                          <telerik:AjaxUpdatedControl ControlID="PageName"/>               
                      <telerik:AjaxUpdatedControl ControlID="Message"/>  
                      <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>               
                     <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                     <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                     <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                        <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
            </UpdatedControls>
            </telerik:AjaxSetting>    
             <telerik:AjaxSetting AjaxControlID="NextPage">
                <UpdatedControls>                         
                          <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/> 
                          <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                 
                         <telerik:AjaxUpdatedControl ControlID="AppPages"/>                                                       
                      <telerik:AjaxUpdatedControl ControlID="PageName"/>   
                    <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                      <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                    <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                      <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>     
                       <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
            </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="PreviousAppPage">
                <UpdatedControls>                         
                          <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>   
                          <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                               
                         <telerik:AjaxUpdatedControl ControlID="AppPages"/>                                                       
                      <telerik:AjaxUpdatedControl ControlID="PageName"/>                   
                     <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                       <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                   <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                      <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>     
                        <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
           </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="MovePageDown">
                <UpdatedControls>                         
                          <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>  
                      <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>     
                          <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                
                         <telerik:AjaxUpdatedControl ControlID="AppPages"/>                                                       
                     <telerik:AjaxUpdatedControl ControlID="PageName"/>                     
                    <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                      <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                    <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                         <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
           </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="MovePageUp">
                <UpdatedControls>                          
                          <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>       
                       <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>     
                        <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                                  
                         <telerik:AjaxUpdatedControl ControlID="AppPages"/>                                                       
                    <telerik:AjaxUpdatedControl ControlID="PageName"/>                       
                   <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                      <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                    <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                        <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                         <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
        </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="DeletePage">
                <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="PagePanel" />                          
                           <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>       
                        <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>     
                        <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                                             
                      <telerik:AjaxUpdatedControl ControlID="Message"/>   
                      <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>              
                         <telerik:AjaxUpdatedControl ControlID="AppPages"/>  
                         <telerik:AjaxUpdatedControl ControlID="PageName"/>                           
                      <telerik:AjaxUpdatedControl ControlID="DisplayModeButton" />
                     <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                       <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                   <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                        <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                 <telerik:AjaxUpdatedControl ControlID="StoryBoardTip" />                    
                         <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
           </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="AppPages">
                <UpdatedControls>
                          <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>       
                         <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                            
                        <telerik:AjaxUpdatedControl ControlID="PageName"/>                     
                     <telerik:AjaxUpdatedControl ControlID="Message"/>     
                     <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>   
                         <telerik:AjaxUpdatedControl ControlID="AppPages" />     
                    <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                      <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                    <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                      <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>     
                        <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
            </UpdatedControls>
            </telerik:AjaxSetting>   
            
             <telerik:AjaxSetting AjaxControlID="GoToPage">
                <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="PagePanel" />                          
                          <telerik:AjaxUpdatedControl ControlID="DeviceMultiPage"/>       
                         <telerik:AjaxUpdatedControl ControlID="CanvasFrame"/>                           
                        <telerik:AjaxUpdatedControl ControlID="PageName"/>                     
                     <telerik:AjaxUpdatedControl ControlID="Message"/>   
                     <telerik:AjaxUpdatedControl ControlID="GenerateMessage"/>     
                         <telerik:AjaxUpdatedControl ControlID="AppPages" />  
                     <telerik:AjaxUpdatedControl ControlID="AppTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="AppTip3"/>   
                      <telerik:AjaxUpdatedControl ControlID="AppTip4"/>                            
                       <telerik:AjaxUpdatedControl ControlID="AppTip5"/>          
                   <telerik:AjaxUpdatedControl ControlID="PageTip1"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip2"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip3"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip4"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip5"/>                         
                      <telerik:AjaxUpdatedControl ControlID="PageTip6"/>                         
                     <telerik:AjaxUpdatedControl ControlID="PageTip7"/>                         
                     <telerik:AjaxUpdatedControl ControlID="storyBoardPanel" LoadingPanelID="ConfigureLoadingPanel"/>     
                         <telerik:AjaxUpdatedControl ControlID="SavedCanvasHtml"/>   
            </UpdatedControls>
            </telerik:AjaxSetting>              
            
    </AjaxSettings>
  	</telerik:RadAjaxManager>
     <telerik:RadWindowManager ID="ConfigureRadWindowManager" runat="server">
            <Windows>
               
                 <telerik:RadWindow 
                    id="HelpEditorTools" 
                    runat="server"
                    showcontentduringload="false"
                    VisibleStatusbar="false"
                     VisibleTitlebar="true"
                     DestroyOnClose="true"
                    behaviors="Default"
                      CssClass="EditorTool"
                      Skin="Web20">
                </telerik:RadWindow>
                
                <telerik:RadWindow 
                    id="PageHelp" 
                    runat="server"
                    showcontentduringload="false"
                    title="Help on Visual Design & Flow" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="false"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>
                
                <telerik:RadWindow 
                    id="RenameAppBox" 
                    runat="server"
                    DestroyOnClose="true"
                    showcontentduringload="false"
                   title="Rename Application" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="false"
                    behaviors="None" Skin="Web20">
                </telerik:RadWindow>

                 <telerik:RadWindow 
                    id="NewAppBox" 
                    runat="server"
                    DestroyOnClose="true"
                    showcontentduringload="false"
                   title="New Application" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="false"
                    behaviors="None" Skin="Web20">
                </telerik:RadWindow>
                
                <telerik:RadWindow 
                    id="NamePageBox" 
                    runat="server"
                     DestroyOnClose="true"
                    showcontentduringload="false"
                    title="New Page Name" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="false"
                    behaviors="None" Skin="Web20">
                </telerik:RadWindow>
                
                 <telerik:RadWindow 
                    id="RenamePageBox" 
                    runat="server"
                     DestroyOnClose="true"
                    showcontentduringload="false"
                    title="Rename Page" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="false"
                    behaviors="None" Skin="Web20">
                </telerik:RadWindow>
                 <telerik:RadWindow 
                    id="SetBackgroundBox" 
                    runat="server"
                     DestroyOnClose="true"                   
                     ShowContentDuringLoad="true"
                   title="Set Stock Background" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="false"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>
                
                 <telerik:RadWindow 
                    id="SetOwnBackgroundBox" 
                    runat="server"
                     DestroyOnClose="true"                   
                     ShowContentDuringLoad="true"
                   title="Set Custom Background" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="false"
                    behaviors="None" Skin="Web20">
                </telerik:RadWindow>  
                <telerik:RadWindow 
                    id="setUrlAccountIdentifierBox" 
                    runat="server"
                     DestroyOnClose="true"
                    showcontentduringload="false"
                    title="Set Account Identifier" 
                    VisibleStatusbar="false"
                     VisibleTitlebar="false"
                    behaviors="None" Skin="Web20">
                </telerik:RadWindow>
                            
            </Windows>
            </telerik:RadWindowManager>
            <telerik:RadAjaxLoadingPanel ID="WholePageLoadingPanel" runat="server" Skin="Default" 
                                Transparency="0" BackColor="LightGray"  IsSticky="true"
	                            CssClass="MyModalPanel"></telerik:RadAjaxLoadingPanel>
                                 <telerik:RadAjaxLoadingPanel ID="ConfigureLoadingPanel" runat="server" Skin="Default" 
                                Transparency="0" BackColor="LightGray"  
                               ></telerik:RadAjaxLoadingPanel>    
     <div align="center" id="header" style="height:80px;  background-color:#0054c2;">
                 <div style="height:10px"></div>
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%" >
                <tr>                
               
                <td><a href="http://ViziApps.com" style="text-decoration:none">
                <asp:Image ID="HeaderImage" ImageUrl="~/images/logo_header_300.png" runat="server" style="border:0px">
                                                            </asp:Image></a>
                    </td>
                    <td> <table style="width: 335px"><tr><td>
                   <asp:ImageButton ID="LayoutVideo" runat="server" CausesValidation="False" 
                       Height="55px" Width="75px"                        
                        CssClass="config_menu" ImageUrl="~/images/layout_video_button.png"  
                         OnClientClick= "PopUp('Help/Design/LayoutVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');return false;"

                        />
                   </td><td>
                   <asp:ImageButton ID="BasicFieldsVideo" runat="server" CausesValidation="False"                        
                        CssClass="config_menu" Height="55px" Width="75px" ImageUrl="~/images/basic_fields_video_button.png" 
                         OnClientClick= "PopUp('Help/Design/BasicFieldsVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');return false;"

                        />
                   </td><td>
                   <asp:ImageButton ID="SeeAllFields" runat="server" CausesValidation="False" 
                       Height="55px" Width="75px"                       
                        CssClass="config_menu" 
                       ImageUrl="~/images/all_fields_button.png" 
                       OnClientClick= "PopUp('Help/Design/ViewAllWebAppFields.htm', 'height=650, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');return false;"
/>
                   </td></tr></table></td>
               
                <td class="style27">
                   
                   
                    <asp:Label ID="UserLabel" runat="server" style="color:White"></asp:Label>
                    
                    </td>
                     <td style="color:White;"></td>
               
                <td align="center" class="style26">
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
                             style="border-width: 0px; margin: 0px; padding: 0px; vertical-align:top; z-index:100;" 
                             TabIndex="1100"  >
                         
                            <Items>
                             <telerik:RadMenuItem ImageUrl="~/images/MySolutionsButton.png" HoveredImageUrl="~/images/MySolutionsButton_hov.png"
                        SelectedImageUrl="~/images/MySolutionsButton_sel.png"  Value="MySolutions" 
                                    TabIndex="1200"/>
                              <telerik:RadMenuItem ImageUrl="~/images/DisplayHybridDesignButton_sel.png" HoveredImageUrl="~/images/DisplayHybridDesignButton_hov.png"
                        SelectedImageUrl="~/images/DisplayDesignButton_sel.png"  Value="DesignNative" TabIndex="1300" ><Items>
                                <telerik:RadMenuItem  ImageUrl="~/images/DisplayNativeDesignButton.png" HoveredImageUrl="~/images/DisplayNativeDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayNativeDesignButton_sel.png" Value="DesignNative"/>
                                <telerik:RadMenuItem  ImageUrl="~/images/DisplayWebDesignButton.png" HoveredImageUrl="~/images/DisplayWebDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayWebDesignButton_sel.png" Value="DesignWeb"/>
                                 <telerik:RadMenuItem  ImageUrl="~/images/DisplayHybridDesignButton_sel.png" HoveredImageUrl="~/images/DisplayHybridDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayHybridDesignButton_sel.png" Value="DesignHybrid"/>
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
      <div align="center" style=" background-color:#ffffff; width:100%; vertical-align:top;height:15px;">&nbsp;</div>
              <div align="center" style=" background-color:#ffffff; width:100%; vertical-align:top;">
             
            
 <table border="0" cellpadding="0" cellspacing="0" style="width: 684px">
    <tr>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 849px">
                <tr>              
                    <td style="height: 10px;" valign="top">                                
                            <table cellpadding="0" cellspacing="0">
                            <tr>
                            <td colspan="3">
                            
                            <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                            <td valign="top" style="width:135px;">
                            <table> <tr>
                              
                              <td><span id="HelpClick" onclick="ToolHelp();"> <img alt="" 
                                                src="images/help.gif" /></span>
                                                   
                                                                                </td>
                               </tr></table>
                              <table id="edit_tools" style="width:135px;font-size:11px; font-weight:bold;display:none" cellspacing="0" cellpadding="0" runat="server">
                             
                              <tr>
                              <td>
                              <div id="EditClick" onclick="doEditProperties();" style="cursor:pointer;padding:5px 0px 0px 10px; background-image:url('images/editor_images/blank_button.png'); background-repeat:no-repeat;width:135px;height:26px;text-align:left; vertical-align:top;">
                              <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Label Field" src="images/editor_images/edit.png" />
                              </td><td style="font-family:verdana;font-size:11px;width:113px;font-weight:bold">Edit Field</td>
                              </tr></table></div></td>
                              </tr>
                                                           <tr>
                              <td>
                              <div id="CopyClick" onclick="copySelectedElement();" style="cursor:pointer;padding:5px 0px 0px 10px; background-image:url('images/editor_images/blank_button.png'); background-repeat:no-repeat;width:135px;height:26px;text-align:left; vertical-align:top;">
                              <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img id="CopyClickImage" alt="Label Field" src="images/editor_images/copy.gif" />
                              </td><td style="font-family:verdana;font-size:11px;width:113px;font-weight:bold">Copy Field</td>
                              </tr></table></div>
                              </td>
                              </tr>
                              <tr>
                              <td>
                              <div id="PasteClick" onclick="pasteSelectedElement();" style="cursor:pointer;padding:5px 0px 0px 10px; background-image:url('images/editor_images/blank_button.png'); background-repeat:no-repeat;width:135px;height:26px;text-align:left; vertical-align:top;">
                              <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Label Field" src="images/editor_images/paste.gif" />
                              </td><td style="font-family:verdana;font-size:11px;width:113px;font-weight:bold">Paste Field</td>
                              </tr></table></div>
                              </td>
                              </tr>

                              <tr>
                              <td>
                              <div id="DeleteClick" onclick="deleteSelectedElement();" style="cursor:pointer;padding:5px 0px 0px 10px; background-image:url('images/editor_images/blank_button.png'); background-repeat:no-repeat;width:135px;height:26px;text-align:left; vertical-align:top;">
                              <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Label Field" src="images/editor_images/delete.gif" />
                              </td><td style="font-family:verdana;font-size:11px;width:113px;font-weight:bold">Delete Field</td>
                              </tr></table></div></td>
                              </tr>
                              <tr>
                              <td>
                              <div id="html_panel" class="edit_tool_button">
                              <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Html Panel Field" src="images/editor_images/panel.gif" />
                              </td><td class="edit_tool_text">Html Panel</td>
                              </tr></table></div></td>
                              </tr>
                              <tr>
                              <td>
                              <div id="label" class="edit_tool_button">
                              <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Label Field" src="images/editor_images/label.gif" />
                              </td><td class="edit_tool_text">Label</td>
                              </tr></table></div></td>
                              </tr>
                              <tr>
                              <td>
                              <div id="text_field" class="edit_tool_button">
                                          <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Text Field" src="images/editor_images/textfield_icon.png" />
                              </td><td class="edit_tool_text">Text Field</td>
                              </tr></table></div></td>
                              </tr>
                              <tr>
                              <td>
                              <div id="text_area" class="edit_tool_button">
                                         <table cellpadding="0" cellspacing="0"><tr><td style="width:22px">
                                             <img alt="Text Area" src="images/editor_images/textarea_icon.png" />
                              </td><td class="edit_tool_text">Text Area</td>
                              </tr></table></div></td>
                             </tr>
                              <tr>
                              <td>
                              <div id="image" class="edit_tool_button">
                                         <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Image" src="images/editor_images/image_icon.png" />
                              </td><td  class="edit_tool_text">Image</td>
                              </tr></table></div></td>
                             </tr>
                              <tr>
                              <td>
                              <div id="button" class="edit_tool_button">
                                         <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Button" src="images/editor_images/button.png" />
                              </td><td class="edit_tool_text">Button</td>
                              </tr></table></div></td>
                             </tr>
                              <tr>
                              <td>
                              <div id="image_button" class="edit_tool_button">
                                         <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Image Button" src="images/editor_images/image_button.png" />
                              </td><td class="edit_tool_text">Image Button</td>
                              </tr></table></div></td>
                             </tr>
                              <tr>
                              <td>
                              <div id="switch" class="edit_tool_button">
                                         <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Switch" src="images/editor_images/switch_icon.png" />
                              </td><td class="edit_tool_text">Switch</td>
                              </tr></table></div></td>
                             </tr>
                             <tr>
                              <td>
                              <div id="checkbox" style="width:135px;height:26px;text-align:left; vertical-align:top;">
                                         <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="CheckBox" src="images/editor_images/checkbox_icon.png" />
                              </td><td style="font-family:verdana;font-size:11px;width:113px">CheckBox</td>
                              </tr></table></div></td>
                             </tr>
                              <tr>
                              <td>
                              <div id="table" class="edit_tool_button">
                                          <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Table" src="images/editor_images/tableview.png" />
                               </td><td class="edit_tool_text">Table</td>
                              </tr></table></div></td>
                             </tr>
                              <tr>
                              <td>
                             <div id="picker" class="edit_tool_button">
                                          <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Picker" src="images/editor_images/picker_view_icon_web.png" />
                               </td><td class="edit_tool_text">Picker</td>
                              </tr></table></div></td>
                              </tr>
                              
                              <tr>
                              <td>
                              <div id="slider" class="edit_tool_button">
                                         <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Slider" src="images/editor_images/slider.png" />
                               </td><td class="edit_tool_text">Slider</td>
                              </tr></table></div></td>
                            </tr>
                             <tr>
                              <td>
                              <div id="web_view" class="edit_tool_button">
                                         <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Web View" src="images/editor_images/browser.png" />
                              </td><td class="edit_tool_text">Web View</td>
                              </tr></table></div></td>
                             </tr>
                             
                              <tr>
                              <td>
                              <div id="alert" class="edit_tool_button">
                                        <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Alert" src="images/editor_images/alert_icon.png" />
                               </td><td class="edit_tool_text">Alert</td>
                              </tr></table></div></td>
                            </tr>
                             
                              <tr>
                              <td>  
                              <div id="hidden_field" class="edit_tool_button">
                                         <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Hidden Field" src="images/editor_images/hidden_field_icon.jpg" /> 
                               </td><td class="edit_tool_text">Hidden Field</td>
                              </tr></table></div></td>
                            </tr>
                              <tr>
                              <td>
                               <div id="BringToMostFront" onclick="BringToMostFront();" style=" cursor:pointer;padding:5px 0px 0px 10px; background-image:url('images/editor_images/blank_button.png'); background-repeat:no-repeat;width:135px;height:26px;text-align:left; vertical-align:top;">
                              <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Label Field" src="images/editor_images/send_to_front.png" />
                              </td><td style="font-family:verdana;font-size:11px;width:113px;font-weight:bold">Bring To Front</td>
                              </tr></table></div></td>
                              </tr>
                              <tr>
                              <td>
                                <div id="SendToMostBack" onclick="SendToMostBack();" style="cursor:pointer;padding:5px 0px 0px 10px; background-image:url('images/editor_images/blank_button.png'); background-repeat:no-repeat;width:135px;height:26px;text-align:left; vertical-align:top;">
                              <table cellpadding="0" cellspacing="0"><tr><td style="width:22px"><img alt="Label Field" src="images/editor_images/send_to_back.png" />
                              </td><td style="font-family:verdana;font-size:11px;width:113px;font-weight:bold">Send To Back</td>
                              </tr></table></div></td>
                              </tr>                              
                              </table>
                             
                              <asp:TextBox ID="DefaultButtonImage" runat="server" style="display:none;" AutoPostBack="True" />   
                              <asp:TextBox ID="SavedCanvasHtml" runat="server" Width="1px" style="display:none;"/>
                              <asp:TextBox ID="AllAppNames" runat="server" Width="1px" style="display:none;"/>
                              <asp:TextBox ID="SaveBeforeChangingAppType" runat="server" Width="1px" style="display:none;"/>
                              <asp:TextBox ID="DeviceType" runat="server" Width="1px" style="display:none;"/>
                              <asp:TextBox ID="AppType" runat="server" Width="1px" style="display:none;"/>
                              </td>
                            <td valign="top">                            
                            <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                            <td style="vertical-align:top;" valign="top">                                                   
                                <telerik:RadMultiPage runat="server" ID="DeviceMultiPage" SelectedIndex="0"   >
                                 <telerik:RadPageView runat="server" ID="iPhoneView">
                                     <div style="width:382px;height:746px;vertical-align:top; background-image:url(images/editor_images/iphone4_skin.gif);background-repeat:no-repeat"></div></telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="AndroidView">
                                        <div style="width:374px;height:696px;vertical-align:top; background-image:url(images/editor_images/galaxy_android_skin.gif);background-repeat:no-repeat"></div></telerik:RadPageView>
                                     <telerik:RadPageView runat="server" ID="iPadPortraitView">
                                         <div style="border:0px;width:808px;height:1109px;vertical-align:top; background-image:url(images/editor_images/ipad_portrait_skin.gif);background-repeat:no-repeat"></div></telerik:RadPageView>
                                 <telerik:RadPageView runat="server" ID="AndroidTabletView">
                                         <div style="width:824px;height:1292px;vertical-align:top; background-image:url(images/editor_images/android_tablet_portrait_skin.gif);background-repeat:no-repeat"></div></telerik:RadPageView> </telerik:RadMultiPage>
                                 
                                 <asp:Panel ID="CanvasFrame" runat="server" BackColor="#aaaaaa" style="width:320px;height:460px;position:relative;left:33px;top:-592px;" >                   
                                 <iframe id="canvas" width="100%" height="100%" marginheight="0" marginwidth="0" src="canvas.aspx" scrolling="no" style=" border-width:0;" runat="server"></iframe>
                                </asp:Panel>
                            </td>
                             </tr>
                             </table>
                             
                             
                             </td>
                            <td  valign="top" style="text-align:left; vertical-align:top;">
                               <table style="height: 326px">
                               <tr>
                               <td>
                                    <table border="0" cellpadding="0" cellspacing = "0"  style="width: 455px;height:20px">
                                    <tr style="height:20px">
                                        <td style="height:20px">
                                                <asp:Label ID="StartMessage0" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                    Font-Size="12pt" ForeColor="#000099" Height="20px" Width="217px">Hybrid App Design (BETA)</asp:Label>
                                        </td>
                                       
                                        <td style="height:20px" align="right">
                                                    &nbsp;</td>
                                       
                                        <td style="height:20px" align="right">
                                                    &nbsp;</td>
                                        <td align="right">       
                                            <img alt="" id="DesignHelp" src="images/help.gif"  onclick="DesignPageHelp()"/>
                                      </td>
                                      </tr>
                                    
                                      <tr style="height:20px">
                                        <td style="height:20px" colspan="5">
                                                <asp:Label ID="DesignMessage" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                    Font-Size="10pt" ForeColor="Maroon" Height="21px" Width="448px">Select an App or Click the New App Icon</asp:Label>
                                        </td>
                                       
                                        </tr>
                                       </table>
                               </td>
                               </tr>
                                 <tr>
                               <td valign="top">
                            <div id="CurrentAppContainer" runat="server" style="background-color:#FFFAEA; width: 452px;">
                             <div align="left">
                            <table style="width: 449px"><tr><td>
                                <asp:Label ID="Label2" runat="server" Text="Current App" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" Width="81px"></asp:Label>
                            </td><td colspan="2">
                                    <telerik:RadComboBox ID="CurrentApp" runat="server" AutoPostBack="True" OnClientLoad="onCurrentAppLoad"
                                    Font-Names="Arial" Font-Size="10pt" OnClientSelectedIndexChanged="onCurrentAppChanged"
                                    OnSelectedIndexChanged="OnCurrentAppChanged" Width="250px">
                                </telerik:RadComboBox>
                            </td><td >
                                    <asp:ImageButton ID="NewApplication" runat="server" ImageUrl="~/images/new.gif" 
                                        OnClientClick="SaveNewApp();return false;" />
                                </td><td class="style19" >
                                    <asp:ImageButton ID="RenameApp" runat="server" ImageUrl="~/images/css.gif" 
                                        OnClientClick="showRenameAppBox()"
                                          />
                                </td>
                                <td >
                                    <asp:ImageButton ID="DuplicateApp" runat="server" ImageUrl="~/images/duplicate.gif" 
                                         style="height: 16px"  
                                        Width="16px" OnClientClick="showDuplicateAppBox()" />
                                </td>
                                 <td >
                                    <asp:ImageButton ID="ConvertAppType" runat="server" ImageUrl="~/images/convert.png" 
                                         style="height: 16px"  
                                        Width="16px" OnClientClick="convertApp()" />
                                </td>
                                <td >
                                    <asp:ImageButton ID="DeleteApp" runat="server" ImageUrl="~/images/delete.gif" 
                                        onclick="DeleteApp_Click" style="height: 16px" 
                                        Width="16px" />
                                </td>
                                </tr></table>
                            </div>
                            <div align="left">
                            <table style=" vertical-align: top;">
                                <tr><td class="style20" 
                                    valign="top">
                                <asp:Label ID="AppPropertiesLabel" runat="server" Text="App Properties" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" Width="103px" 
                                        Height="16px"></asp:Label>
                            </td><td align="left" class="style21" valign="top" >
                                    <telerik:RadComboBox ID="AppProperties" runat="server" 
                                    Font-Names="Arial" Font-Size="10pt" OnClientSelectedIndexChanged="onChangeAppProperties"
                                     Width="250px">
                                     <Items>
                                     </Items>
                                </telerik:RadComboBox>
                                         </td><td class="style20" valign="top" >
                                </td><td class="style20" valign="top" >
                                </td>
                                <td class="style20" valign="top" >
                                </td>
                                <td class="style20" valign="top" >
                                </td>
                                </tr>
                                <tr><td class="style16" 
                                    valign="top">
                                    &nbsp;</td><td align="right" class="style17" valign="top" >
                                        <asp:Button ID="SelectForTest" runat="server" 
                                               Text="Select This App To Test On Device" Width="224px" onclick="SelectForTest_Click"  
                                        />                                         
                                                                           
                                         </td><td class="style16" valign="top" >
                <asp:Image ID="AppSelectedForTest" runat="server" ImageUrl="images/check.gif" 
                                AlternateText="App Selected For Test" 
                 Visible="False" />
                                        </td><td class="style16" valign="top" >
                                        &nbsp;</td>
                                <td class="style16" valign="top" >
                                    &nbsp;</td>
                                <td class="style16" valign="top" >
                                    &nbsp;</td>
                                </tr>
                                <tr><td class="style16" 
                                    valign="top">
                                    &nbsp;</td><td align="right" class="style17" valign="top" >
                                     <asp:Button ID="ValidateFieldNames" runat="server" 
                                             onclick="ValidateFieldNames_Click" Text="Validate Field References" Width="224px"  
                                        />                                        
                                                                           
                                         </td><td class="style16" valign="top" >
                                        &nbsp;</td><td class="style16" valign="top" >
                                        &nbsp;</td>
                                <td class="style16" valign="top" >
                                    &nbsp;</td>
                                <td class="style16" valign="top" >
                                    &nbsp;</td>
                                </tr>
                                </table></div>
                                </div>
                                </td>
                                </tr>
                               <tr>
                               <td>
                                <table>
                                <tr><td colspan="6">
                                <table width="100%" cellpadding="0" cellspacing="0" style="height: 61px"><tr>
                                <td align="left" valign="top">
                                <asp:Button ID="DisplayMode" runat="server" onclick="DisplayMode_Click"   Width="1px" Height="1px" BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="0" />
                                 <telerik:RadButton ID="DisplayModeButton" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"  
                    Width="150px" Height="30px" AutoPostBack="False"     Checked="true"  OnClientClicked="checkURLIdentifier"
                                        >
                    <ToggleStates>
                        <telerik:RadButtonToggleState   ImageUrl="~/images/show_app_running.png" HoveredImageUrl="~/images/show_app_running_hover.png"/>
                        <telerik:RadButtonToggleState  ImageUrl="~/images/show_app_design.png" HoveredImageUrl="~/images/show_app_design_hover.png"  />
                    </ToggleStates>                    
                </telerik:RadButton>   
                 </td><td align="right" >
                                                    <asp:TextBox ID="DisplayModeValue" runat="server" style="display:none"/>
                                                     <asp:Button ID="SaveWebDataSources" runat="server"                                                      
                                            CausesValidation="False" UseSubmitBehavior="False" 
                                            onclick="SaveWebDataSources_Click" style="display:none"/>
                                                     <asp:TextBox ID="UrlAccountIdentifier" runat="server" style="display:none"/>
                                                      <asp:TextBox ID="WebDataSource" runat="server" style="display:none"/>
                                                      <asp:Button ID="SaveUrlAccountIdentifier" runat="server"                                                      
                                            CausesValidation="False" UseSubmitBehavior="False" onclick="SaveUrlAccountIdentifier_Click" style="display:none" />
                                                    </td></tr><tr>
                                <td align="left" valign="top" class="style18" colspan="2">
                                   <asp:Label ID="PreviewNote" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                Font-Size="10pt" ForeColor="#000099" 
                                                        Width="436px" ></asp:Label>
                                         </td>
                                         </tr>
                                         </table>                               
                                                
                               </td>
                               </tr>
                              <tr>
                               <td>                                       

                               <div id="PagePanel" runat="server" align="left" 
                                       style="background-color:#EDF7FE; width: 452px; height: 107px;">        
                                <table>
                                    <tr>
                                        <td align="left" class="style12">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" 
                                                Font-Names="Arial" Font-Size="10pt" ForeColor="#000099" Text="App Page" 
                                                Width="77px"></asp:Label>
                                        </td>
                                        <td align="left" class="style13">
                                            <telerik:RadComboBox ID="AppPages" runat="server" AutoPostBack="True" 
                                                Font-Names="Arial" Font-Size="10pt" 
                                                onselectedindexchanged="OnAppPagesChanged" Width="175px">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td align="center" class="style14">
                                            <asp:ImageButton ID="NewPage" runat="server" ImageUrl="~/images/new.gif" 
                                                OnClientClick="showNewPageBox()" />
                                        </td>                                        
                                        <td class="style15">
                                            <asp:ImageButton ID="RenamePage" runat="server" ImageUrl="~/images/css.gif" 
                                                OnClientClick="showRenamePageBox()" style="height: 16px" />
                                        </td>
                                        <td>
                                    <asp:ImageButton ID="DuplicatePage" runat="server" ImageUrl="~/images/duplicate.gif" 
                                         style="height: 16px"  
                                        Width="16px" OnClientClick="showDuplicatePageBox()" 
                                                 />
                                        </td>
                                        <td class="style25">
                                            <asp:ImageButton ID="NextPage" runat="server" ImageUrl="~/images/downarrow.gif" 
                                                onclick="NextPage_Click" OnClientClick="checkStoryBoardRefresh()" />
                                        </td>
                                        <td class="style15">
                                            <asp:ImageButton ID="PreviousAppPage" runat="server" 
                                                ImageUrl="~/images/uparrow.gif" onclick="PreviousPage_Click" OnClientClick="checkStoryBoardRefresh()" Width="16px" />
                                        </td>
                                        <td class="style15">
                                            <asp:ImageButton ID="MovePageDown" runat="server" 
                                                ImageUrl="~/images/movedown_arrow.gif" onclick="MovePageDown_Click" OnClientClick="checkStoryBoardRefresh()" />
                                        </td>
                                        <td class="style15">
                                            <asp:ImageButton ID="MovePageUp" runat="server" 
                                                ImageUrl="~/images/moveup_arrow.gif" onclick="MovePageUp_Click" OnClientClick="checkStoryBoardRefresh()" Width="16px" />
                                        </td>
                                        
                                        <td class="style13">
                                            <asp:ImageButton ID="DeletePage" runat="server" ImageUrl="~/images/delete.gif" 
                                                onclick="DeletePage_Click" style="height: 16px" />
                                        </td>
                                    </tr>
                                    </table>
                                    <table style="width: 453px">
                                    <tr>
                                        <td align="left" class="style23">
                                                    <asp:Button ID="SaveApp" runat="server"  Text="Save"
                                                    Width="70px" OnClientClick="SaveAppPage(); return false;"  CausesValidation="False" UseSubmitBehavior="False" />
                                        </td>
                                        <td align="left" class="style24">
                                             <asp:Button ID="Undo" runat="server"  Text="Undo Edits After Last Save"
                                                    Width="180px"  
                                                 CausesValidation="False" UseSubmitBehavior="False" onclick="Undo_Click" />
                                            </td>
                                        <td align="left" class="style24">
                                             <asp:Button ID="ManagePageData" runat="server"  Text="Manage Page Data "
                                                    Width="139px"  OnClientClick="managePageData();" UseSubmitBehavior="False"
                                                  />
                                            </td>
                                             
                                        <td align="left" class="style24">
      <asp:ImageButton ID="MySolutionsHelp" runat="server" ImageUrl="~/images/help.gif" 
        ToolTip="How Your Device Gets Web Data?" OnClientClick="var oWin=radopen('Help/PageData/ViewDataModel.htm', 'ViewDataModel'); 
                                       oWin.set_visibleTitlebar(true);
                                     oWin.set_visibleStatusbar(false);
                                   oWin.setSize(1060,900);
                                   oWin.moveTo(0,0);
                                   return false;"/>

                                            </td>
                                             
                                    </tr>
                                </table>
                                </div>
                               </td>
                               </tr>
                            <tr>
                               <td align="left" class="style22" >                                                  
                                   &nbsp;</td>
                               </tr>
                            <tr>
                               <td >
                                <asp:Label ID="Message" runat="server" Font-Bold="True" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="Maroon" Height="42px" Width="444px"></asp:Label>
                               </td>
                               </tr>
                            
                            <tr>
                               <td align="left" valign="top" style="height:1000px" >
                                
                               <div runat="server" id="storyBoardPanelWebApp" style="height:1000px">
                                   <iframe id="storyBoard" width="100%" height="100%" marginheight="0" marginwidth="0" scrolling="no" style=" border-width:0;" src="Dialogs/Design/StoryBoard.aspx" ></iframe>
                                </div>
                                   </td>
                               </tr>
                            <tr>
                               <td >
                                <asp:Button ID="RenameAppPost" runat="server" CausesValidation="False" 
                                        onclick="AcceptRenameApp_Click" 
                                Width="1px" style="display:none" />
                                 <asp:Button ID="DuplicateAppPost" runat="server" CausesValidation="False" 
                                        onclick="DuplicateApp_Click" 
                                Width="1px" style="display:none" />
                                
                                <asp:Button ID="RenamePagePost" runat="server" CausesValidation="False" 
                                        onclick="AcceptPageName_Click"  style="display:none" 
                                Width="1px" Height="1px" />

                                  <asp:Button ID="DuplicatePagePost" runat="server" CausesValidation="False" 
                                        onclick="DuplicatePage_Click" 
                                Width="1px" style="display:none" />

                               <asp:Button ID="NewPagePost" runat="server" CausesValidation="False" 
                                        onclick="NewPage_Click"  style="display:none" 
                                Width="1px" Height="1px" />
                               
                                <asp:Button ID="SaveAppAsPost" runat="server" CausesValidation="False" 
                                        onclick="SaveAppAs_Click"  style="display:none" 
                                Width="1px" Height="1px" UseSubmitBehavior="False" />

                                <asp:Button ID="SaveAppPost" runat="server" CausesValidation="False" 
                                        onclick="SaveApp_Click"  style="display:none" 
                                Width="1px" Height="1px" UseSubmitBehavior="False" />

                                 <asp:Button ID="SaveAppPagePost" runat="server" CausesValidation="False" 
                                        onclick="SaveAppPage_Click"  style="display:none" 
                                Width="1px" Height="1px" UseSubmitBehavior="False" />

                                 <asp:Button ID="SetViewForDeviceTypePost" runat="server" CausesValidation="False" 
                                        onclick="SetViewForDeviceTypePost_Click"  style="display:none"
                                Width="1px" Height="1px" UseSubmitBehavior="False" />

                                 <asp:Button ID="SetBackgroundPost" runat="server" CausesValidation="False" 
                                        onclick="SetBackgroundPost_Click"  style="display:none" 
                                Width="1px" Height="1px" UseSubmitBehavior="False" />

                             <asp:TextBox ID="AppName" runat="server"  style="display:none"  AutoPostBack="true"
                                 Width="1px"  Height="1px"></asp:TextBox>
                                <asp:TextBox ID="AppDescription" runat="server"  AutoPostBack="true"
                                style="display:none"></asp:TextBox>
                              <asp:TextBox ID="PageName" runat="server"  style="display:none"   AutoPostBack="true"
                                 Width="1px"  Height="1px"></asp:TextBox>
                                 <asp:Button ID="GoToPage" runat="server" onclick="GoToPage_Click" style="display:none" />
                                 <asp:TextBox ID="DesignedFor" runat="server"  style="display:none"   AutoPostBack="true" />                              
                                 <asp:TextBox ID="Background" runat="server"  style="display:none"  AutoPostBack="true"
                                Width="1px"  Height="1px"></asp:TextBox>
                                
                                <asp:TextBox ID="IsNewApp" runat="server"  AutoPostBack="true"
                                Width="1px"  style="display:none"></asp:TextBox>                                
                               
                              
                                       
                                                             
                                </td>
                               </tr>
                               </table>                               
                                
                                </td>
                            </tr>
                            </table>
                          
                            
                            </td>
                                </tr>
                            </table>
                           
   
                    </td>
               </tr>
           </table>
           </td>
           </tr>
           </table>
          
               
                </div>
                 <telerik:RadToolTip runat="server" ID="HelpTip2" Width = "175px" Height = "25px" TargetControlId = "HelpClick" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Help on the Editor Tools
                     </span></telerik:RadToolTip>
                <telerik:RadToolTip runat="server" ID="HelpTip1" Width = "150px" Height = "25px" TargetControlId = "DesignHelp" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">What is this page for?
                     </span></telerik:RadToolTip>
                <telerik:RadToolTip runat="server" ID="AppTip1" Width = "100px" Height = "25px" TargetControlId = "RenameApp" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Rename App
                     </span></telerik:RadToolTip>
                <telerik:RadToolTip runat="server" ID="AppTip2" Width = "100px" Height = "25px" TargetControlId = "DuplicateApp" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Duplicate App
                     </span></telerik:RadToolTip>
                 <telerik:RadToolTip runat="server" ID="AppTip5" Width = "130px" Height = "25px" TargetControlId = "ConvertAppType" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Convert App Type
                     </span></telerik:RadToolTip>
                <telerik:RadToolTip runat="server" ID="AppTip3" Width = "90px" Height = "25px" TargetControlId = "DeleteApp" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Delete App
                     </span></telerik:RadToolTip>
                 <telerik:RadToolTip runat="server" ID="AppTip4" Width = "120px" Height = "25px" TargetControlId = "NewApplication" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Create New App
                     </span></telerik:RadToolTip>
               <telerik:RadToolTip runat="server" ID="PageTip1" Width = "80px" Height = "25px" TargetControlId = "NewPage" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">New Page
                     </span></telerik:RadToolTip>
                <telerik:RadToolTip runat="server" ID="PageTip2" Width = "100px" Height = "25px" TargetControlId = "RenamePage" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Rename Page
                     </span></telerik:RadToolTip>
                 <telerik:RadToolTip runat="server" ID="RadToolTip3" Width = "100px" Height = "25px" TargetControlId = "DuplicatePage" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Duplicate Page
                     </span></telerik:RadToolTip>
                 <telerik:RadToolTip runat="server" ID="PageTip3" Width = "110px" Height = "25px" TargetControlId = "NextPage" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Show Next Page
                     </span></telerik:RadToolTip>
                <telerik:RadToolTip runat="server" ID="PageTip4" Width = "130px" Height = "25px" TargetControlId = "PreviousAppPage" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Show Previous Page
                     </span></telerik:RadToolTip>
                <telerik:RadToolTip runat="server" ID="PageTip6" Width = "180px" Height = "25px" TargetControlId = "MovePageDown" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Move Selected Page Down
                     </span></telerik:RadToolTip>
                <telerik:RadToolTip runat="server" ID="PageTip7" Width = "160px" Height = "25px" TargetControlId = "MovePageUp" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Move Selected Page Up
                     </span></telerik:RadToolTip>
                <telerik:RadToolTip runat="server" ID="PageTip8" Width = "90px" Height = "25px" TargetControlId = "DeletePage" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Delete Page
                     </span></telerik:RadToolTip>
               <telerik:RadToolTip runat="server" ID="RadToolTip1" Width = "230px" Height = "25px" TargetControlId = "LayoutVideo" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Tutorial Video on Layout Design
                     </span></telerik:RadToolTip>
               <telerik:RadToolTip runat="server" ID="RadToolTip2" Width = "280px" Height = "25px" TargetControlId = "BasicFieldsVideo" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">Tutorial Video on Form and Button Fields
                     </span></telerik:RadToolTip>

    </form>
    <div id="new_app_dialog" title="New App"></div>
    <div id="rename_app_dialog" title="Rename App"></div>
    <div id="duplicate_app_dialog" title="Duplicate App"></div>
    <div id="convert_app_dialog" title="Convert App from one type to another"></div>
    <div id="new_page_dialog" title="New Page"></div>
    <div id="rename_page_dialog" title="Rename Page"></div>
    <div id="duplicate_page_dialog" title="Duplicate Page"></div>
    <div id="tool_help_dialog" title="Tools Help"></div>
    <div id="design_page_help_dialog" title="Design Page Help"></div> 
    <div id="web_data_sources_dialog" title="Web Data Source"></div>
    <div id="google_spreadsheet_data_source_dialog" title="Google Spreadsheet Data Source"></div>
    <div id="rss_data_surce_dialog" title="RSS Data Source"></div>
    <div id="rest_web_service_data_source_dialog" title="REST Web Service Data Source"></div>
    <div id="soap_web_service_data_source_dialog" title="SOAP Web Service Data Source"></div>
    <div id="sql_database_data_source_dialog" title="SQL Database Data Source"></div>
    <div id="manage_page_data_dialog" title="Page Data" runat="server"></div>
    <div id="app_images_dialog" title="App Images"></div>
    <div id="on_app_open_dialog" title="On App Open"></div>
    <div id="custom_header_html_dialog" title="Set Custom Header HTML"></div>
    <div id="account_identifier_dialog" title="Set Account Identifier"></div>
    <div id="app_description_dialog" title="Set App Description"></div>
    <div id="page_transition_type_dialog" title="Set Page Transition Type for App"></div>
    <div id="app_device_dialog" title="Set Main Device Type for App"></div>
    <div id="app_background_color_dialog" title="Set App Background Color"></div>
    <div id="html_panel_dialog" title="Html Panel Properties"></div>
    <div id="label_dialog" title="Label Properties"></div>
    <div id="text_field_dialog" title="Text Field Properties"></div>
    <div id="text_area_dialog" title="Text Area Properties"></div>
    <div id="image_dialog" title="Image Properties"></div>
    <div id="button_dialog" title="Button Properties"></div>
    <div id="image_button_dialog" title="Image Button Properties"></div>
    <div id="switch_dialog" title="Switch Properties"></div>
    <div id="checkbox_dialog" title="CheckBox Properties"></div>
    <div id="table_dialog" title="Table View Properties"></div>
    <div id="picker_dialog" title="Picker Properties"></div>
    <div id="slider_dialog" title="Slider Properties"></div>
    <div id="web_view_dialog" title="Web View Properties"></div>
    <div id="alert_dialog" title="Alert Properties"></div>
    <div id="map_dialog" title="Map Properties"></div>
    <div id="hidden_field_dialog" title="Hidden Field Properties"></div>
</body>
</html>
