<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublishingFormWebApps.aspx.cs" Inherits="PublishingFormWebApps" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>App Publishing Form</title>
 	<telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
    <style type="text/css">
        .style31
        {
        }


        .style34
        {
            height: 30px;
        }
        .style35
        {
        }
        .style39
        {
            height: 17px;
        }
        .style40
        {
        }
        .style41
        {
            height: 30px;
            width: 220px;
        }
        .style48
        {
            width: 220px;
        }
        .style49
        {
            width: 218px;
        }
        .style50
        {
            height: 30px;
            width: 218px;
        }
        .style56
        {
            height: 56px;
            width: 302px;
        }
        .style57
        {
            height: 56px;
        }
        .style59
        {
            height: 52px;
        }
        .style61
        {
            height: 66px;
        }
        .style62
        {
            height: 22px;
        }
        .style63
        {
            height: 22px;
            width: 185px;
        }
        .style64
        {
            width: 185px;
        }
        .style65
        {
            height: 14px;
        }
        .style67
        {
            width: 481px;
            height: 140px;
        }
        .style68
        {
            height: 140px;
        }
        .style69
        {
        }
        .style70
        {
            height: 21px;
        }
        .style71
        {
            height: 99px;
        }
    </style>
    
         <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
         <script type="text/javascript" >
             function showSplashScreen() {
                var oWin=radopen('ScreenShot.aspx', 'ScreenShotBox');
                oWin.set_visibleTitlebar(true);
                oWin.set_visibleStatusbar(false);
                oWin.set_modal(true); 

                 var SelectedDeviceType = document.getElementById('SelectedDeviceType');
                 switch(SelectedDeviceType.value)
                 {
                     default: //case 'iphone':
                         oWin.setSize(354, 540);
                         oWin.moveTo(100, 100);
                         break;
                     case 'android_phone':
                         oWin.setSize(354, 555);
                         oWin.moveTo(100, 100);
                         break;
                     case 'ipad':
                         oWin.setSize(802, 1084);
                         oWin.moveTo(5, 5);
                         break;
                 }
                return false;
             }
         </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:TextBox ID="SelectedDeviceType" runat="server" style="display:none"></asp:TextBox>
	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
	</telerik:RadScriptManager>
	<script type="text/javascript">
	    //Put your JavaScript code here.
    </script>
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>         
              
         <telerik:AjaxSetting AjaxControlID="NumberOfUsers">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="NumberOfUsers" > </telerik:AjaxUpdatedControl>   
                     <telerik:AjaxUpdatedControl ControlID="LimitedUsersPanel" > </telerik:AjaxUpdatedControl>   
                     <telerik:AjaxUpdatedControl ControlID="SaveNumberOfUsersMessage" > </telerik:AjaxUpdatedControl>                                        
                 </UpdatedControls>
            </telerik:AjaxSetting>        

           <telerik:AjaxSetting AjaxControlID="SaveUserCredentials">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="SaveCredentialsMessage" > </telerik:AjaxUpdatedControl> 
                      <telerik:AjaxUpdatedControl ControlID="SaveNumberOfUsersMessage" > </telerik:AjaxUpdatedControl>                                                             
                 </UpdatedControls>
            </telerik:AjaxSetting>        

          <telerik:AjaxSetting AjaxControlID="UploadAppLargeIcon">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="IconUploadMessage" > </telerik:AjaxUpdatedControl>   
                     <telerik:AjaxUpdatedControl ControlID="repeaterResultsLargeIcon" > </telerik:AjaxUpdatedControl>    
                  </UpdatedControls>
            </telerik:AjaxSetting>
            
             <telerik:AjaxSetting AjaxControlID="UploadAppSplash">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SplashUploadMessage" > </telerik:AjaxUpdatedControl>                   
                     <telerik:AjaxUpdatedControl ControlID="repeaterResultsSplashImage" > </telerik:AjaxUpdatedControl>                    
                 </UpdatedControls>
            </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="UploadScreenShots">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ScreenShotsUploadMessage" > </telerik:AjaxUpdatedControl>                   
                     <telerik:AjaxUpdatedControl ControlID="repeaterResultsScreenShots" > </telerik:AjaxUpdatedControl>                    
                 </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="Publish">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PublishMessage" > </telerik:AjaxUpdatedControl>                   
                    <telerik:AjaxUpdatedControl ControlID="QRCodeLabel" > </telerik:AjaxUpdatedControl>                   
                    <telerik:AjaxUpdatedControl ControlID="QRCode" > </telerik:AjaxUpdatedControl>      
                   <telerik:AjaxUpdatedControl ControlID="PublishedAppURL" > </telerik:AjaxUpdatedControl>    
                 </UpdatedControls>
            </telerik:AjaxSetting>

       </AjaxSettings>
	</telerik:RadAjaxManager>
     <telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />
         <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <telerik:RadWindow 
                    id="LargeIconBox" 
                    runat="server"
                    showcontentduringload="false"
                    title="Large App Icon"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>
                
            <telerik:RadWindow 
                    id="ScreenShotBox" 
                    runat="server"
                    showcontentduringload="false"
                    title="Screen Image" 
                    behaviors="Default" Skin="Web20">
           </telerik:RadWindow>
            </Windows>
            </telerik:RadWindowManager>
<div>
 <asp:Label ID="App" runat="server" Font-Bold="True" 
                Font-Names="Arial" Font-Size="14pt" ForeColor="#003399" 
        Width="620px"></asp:Label>
</div>
<div>
    <table style="width: 900px;">
        <tr>
            <td class="style67" valign="top">
                
    <table width="100%"><tr>
        <td class="style61" valign="top" colspan="2"> 
        <table style="height: 93px; width: 472px;"><tr><td class="style63">
                <asp:Label ID="ProductionAppNameLabel" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="143px" 
                Height="16px">Publishing App Name</asp:Label>
                       </td><td class="style62">
                       <asp:TextBox ID="ProductionAppName" runat="server" Width="276px"></asp:TextBox>
            </td></tr><tr><td class="style64">
                <asp:Button runat="server" ID="SaveProductionAppNameButton" Text="Save Publishing App Name" 
                     Width="176px" onclick="SaveProductionAppNameButton_Click"  />
               
                </td><td>
               
                <asp:Label ID="SaveProductionAppNameMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="270px"></asp:Label>
               
                </td></tr></table>
            
               
            </td>
            </tr><tr>
        <td class="style56" valign="top"> 
        <table><tr><td>
    <asp:Button runat="server" ID="CopyDesignToProduction" 
                Text="Save Test Design To Publishing Design" Width="252px" onclick="CopyDesignToProduction_Click" 
 /></td><td>
            <asp:Image ID="ProductionDesignExists" runat="server" ImageUrl="../../images/check.gif" 
                                AlternateText="Publication Design Exists" 
                 Visible="False" /></td></tr></table>
            </td>
            <td class="style57" valign="top">
                <asp:Label ID="CopyDesignMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="189px"></asp:Label>
                        </td></tr><tr>
        <td class="style69" colspan="2">
        <asp:Label ID="IconUploadLabel" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="386px">Application Icon ( large 512 X 512 pixels from .jpg file )</asp:Label>
            </td>
            </tr><tr>
        <td class="style31" colspan="2">
               
                <table style="width:454px; height: 237px;">
                    <tr>
                        <td class="style48">
        <telerik:RadUpload ID="UploadLargeIcon" runat="server" ControlObjectsVisibility="None" OverwriteExistingFiles ="true"
             TargetFolder="~/uploaded_files" Skin="Windows7" Width="224px" />

   
	                    </td>
                        <td style=" font-family:Tahoma; font-size:12px">
             
                            &nbsp;</td>
                        <td style=" font-family:Tahoma; font-size:12px">
             
                            <asp:Repeater ID="repeaterResultsLargeIcon" runat="server" Visible="False">
                                <HeaderTemplate>
                                    <div class="title">Uploaded file :</div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "FileName")%>
                                    <%#DataBinder.Eval(Container.DataItem, "ContentLength").ToString() + " bytes"%>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td class="style41">
    <asp:Button runat="server" ID="UploadLargeIconButton" Text="Upload File" onclick="UploadLargeIconButton_Click" 
 />
                        </td>
                        <td class="style34">
                            &nbsp;</td>
                        <td class="style34">
            <asp:Button ID="LargeIconButton" runat="server" OnClientClick="var oWin=radopen('LargeIcon.aspx', 'LargeIconBox');
                                                   oWin.set_visibleTitlebar(true);
                                                    oWin.set_visibleStatusbar(false);
                                                  oWin.set_modal(true); 
                                                  oWin.setSize(550,600);
                                                  oWin.moveTo(100, 100); 
                                                  return false;" Text="See Large Icon" 
                Visible="false" Width="107px" />
                <asp:ImageButton ID="DeleteIcon" runat="server" ImageUrl="../../images/delete_small.gif" 
                                AlternateText="Delete Icon" onclick="DeleteIcon_Click" Visible="False" />
                            </td>
                    </tr>
                    <tr>
                        <td class="style40" colspan="3">
                <asp:Label ID="IconUploadMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="315px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style40" colspan="3" style="height: 100px;">
                            &nbsp;</td>
                    </tr>
                </table>
               
            </td>
            </tr></table>
                
            </td>
            <td valign="top" class="style68">
               <table><tr><td>
               
    <table width="100%"><tr>
        <td class="style35">
        <asp:Label ID="SplashUploadLabel" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="326px">Splash Image ( 320 X 460 pixels from .jpg file )</asp:Label>
            </td>
            </tr></table>
               
               </td></tr><tr>
               <td class="style65">
               
                <table style="width:453px;">
                    <tr>
                        <td class="style49">
        <telerik:RadUpload ID="UploadScreenSplash" runat="server" ControlObjectsVisibility="None" OverwriteExistingFiles ="true"
             TargetFolder="~/uploaded_files" Skin="Windows7" Width="211px" />

   
	                    </td>
                        <td style=" font-family:Tahoma; font-size:12px">
             
                            &nbsp;</td>
                        <td style=" font-family:Tahoma; font-size:12px">
             
                            <asp:Repeater ID="repeaterResultsScreenSplash" runat="server" Visible="False">
                                <HeaderTemplate>
                                    <div class="title">Uploaded file :</div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "FileName")%>
                                    <%#DataBinder.Eval(Container.DataItem, "ContentLength").ToString() + " bytes"%>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td class="style50">
    <asp:Button runat="server" ID="UploadScreenSplashButton" Text="Upload File" onclick="UploadScreenSplashButton_Click" 
 />
                        </td>
                        <td class="style34">
                            &nbsp;</td>
                        <td class="style34">
            <asp:Button ID="ScreenSplashButton" runat="server" Text="See Screen Splash" 
                Visible="false" Width="126px"  /><asp:ImageButton ID="DeleteSplashImage" runat="server" 
                                ImageUrl="../../images/delete_small.gif" AlternateText="Delete Splash Image" 
                                onclick="DeleteSplashImage_Click" Visible="False" />
                            </td>
                    </tr>
                    <tr>
                        <td class="style39" colspan="3">
                <asp:Label ID="SplashUploadMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="272px"></asp:Label>
                            </td>
                    </tr>
                </table>
               
                   </td></tr>
                   <tr><td class="style70">
               
                    </td></tr><tr><td class="style59">
                <asp:Button ID="Publish" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="14pt" onclick="Publish_Click" 
                    Text="Publish" Width="110px" />
                               
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                               
                                            <asp:Button ID="PurchaseButton" runat="server" 
                                                CausesValidation="False" style="font-family:arial; font-size:12px;"                                                
                                                Text="Purchase ViziApps Services" Width="178px" />
                               
                    </td></tr><tr><td>
                    <asp:Label ID="PublishMessage" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="410px" Height="62px"></asp:Label>
                    </td></tr><tr><td class="style71">
                       <table cellpadding="0" cellspacing="0" style="height: 61px" width="100%">
                           <tr>
                               <td align="left" class="style18" valign="top">
                                   <table style="width: 286px; height: 20px">
                                       <tr>
                                           <td>
                                               <asp:Label ID="QRCodeLabel" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                   Font-Size="10pt" ForeColor="#000099" style="display:none" Width="350px"></asp:Label>
                                           </td>
                                           <tr>
                                           <td>
                                               <img id="QRCode" runat="server" alt="" height="150" src="" style="display:none" 
                                                   width="150" /></td></tr>
                                       </tr>
                                   </table>
                               </td>
                           </tr>
                           <tr>
                               <td align="left" class="style19" valign="top">
                                   <asp:Label ID="PublishedAppURL" runat="server" Font-Bold="True" Font-Names="Arial" 
                                       Font-Size="10pt" ForeColor="#000099" Width="436px"></asp:Label>
                               </td>
                           </tr>
                       </table>
                                                    
                                                    
                    </td></tr></table>
               
            </td>
        </tr>
       
           </table>
	</div>

	</form>
</body>
</html>
