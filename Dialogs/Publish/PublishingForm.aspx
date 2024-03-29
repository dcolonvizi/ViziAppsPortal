﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublishingForm.aspx.cs" Inherits="ProvisionForm" %>

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
        .style46
        {
            width: 481px;
        }
        .style48
        {
            width: 220px;
            height: 80px;
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
        .style53
        {
            height: 72px;
        }
        .style58
        {
            height: 41px;
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
            height: 45px;
        }
        .style65
        {
            height: 14px;
        }
        .style66
        {
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
            height: 40px;
        }
        .style70
        {
            height: 45px;
        }
        .style71
        {
            height: 80px;
        }
        .style72
        {
            height: 37px;
        }
    </style>
    
         <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
</head>
<body>
    <form id="form1" runat="server">
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
                     <telerik:AjaxUpdatedControl ControlID="UploadPublishedUserCredentials" > </telerik:AjaxUpdatedControl> 
                      <telerik:AjaxUpdatedControl ControlID="ViewPublishedUserCredentials" > </telerik:AjaxUpdatedControl>                                                             
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

             <telerik:AjaxSetting AjaxControlID="SubmitForProvisioning">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ProvisioningMessage" > </telerik:AjaxUpdatedControl>                   
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
                    title="Screen Image" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
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
                
    <table width="100%" style="height: 303px"><tr>
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
               
                </td><td class="style70">
               
                <asp:Label ID="SaveProductionAppNameMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="270px"></asp:Label>
               
                </td></tr></table>
            
               
            </td>
            </tr><tr>
        <td class="style69" valign="top"> 
        <table><tr><td>
    <asp:Button runat="server" ID="CopyDesignToProduction" 
                Text="Save Test Design To Publishing Design" Width="246px" onclick="CopyDesignToProduction_Click" 
 /></td><td>
            
                <asp:Image ID="ProductionDesignExists" runat="server" ImageUrl="../../images/check.gif" 
                                AlternateText="Publication Design Exists" 
                 Visible="False" /></td></tr></table>
            </td>
            <td class="style69" valign="top">
                <asp:Label ID="CopyDesignMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="189px"></asp:Label>
                        </td></tr><tr>
        <td class="style31" colspan="2">
        <asp:Label ID="SplashUploadMessage1" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="368px">Application Icon ( large 512 X 512 pixels from .jpg file )</asp:Label>
            </td>
            </tr><tr>
        <td class="style31" colspan="2">
               
                <table style="width:454px;">
                    <tr>
                        <td class="style48">
        <telerik:RadUpload ID="UploadLargeIcon" runat="server" ControlObjectsVisibility="None" OverwriteExistingFiles ="true"
             TargetFolder="~/temp_files" Skin="Windows7" Width="224px" />

   
	                    </td>
                        <td style=" font-family:Tahoma; font-size:12px" class="style71">
             
                            </td>
                        <td style=" font-family:Tahoma; font-size:12px" class="style71">
             
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
                                                  return false;" Text="Large Icon" 
                Visible="false" Width="95px" />
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
                </table>
               
            </td>
            </tr></table>
                
            </td>
            <td valign="top" class="style68">
               <table><tr><td>
               <table style="width: 401px"><tr><td>
                <asp:Label ID="NumberOfUsersLabel" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="137px">If App Uses a Limited Number of Users</asp:Label>
                    </td><td>
                   <telerik:RadComboBox ID="NumberOfUsers" Runat="server" Width="250px" 
                           onselectedindexchanged="NumberOfUsers_SelectedIndexChanged" 
                           AutoPostBack="True">
                   <Items>
                   <telerik:RadComboBoxItem  Text="Select ->" Value="none" Selected="true"/>
                   <telerik:RadComboBoxItem  Text="Limited Users with 1 password for all" 
                           Value="limited_1_password_all" />
                   <telerik:RadComboBoxItem  Text="Limited Users with 1 password for each" 
                           Value="limited_1_password_each" />
                   </Items>
                   </telerik:RadComboBox>
                   </td></tr></table>
               </td></tr><tr>
               <td class="style65">
                   <asp:Panel ID="LimitedUsersPanel" runat="server" style="display:none" >
                   <table style="width: 353px; height: 113px">
                   <tr>
                   <td align="center">
                       <asp:Label ID="SplashUploadMessage7" runat="server" Font-Bold="True" 
                           Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="119px">Single Username</asp:Label>
                   </td>
                   <td align="center">
                       <asp:Label ID="SplashUploadMessage8" runat="server" Font-Bold="True" 
                           Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="118px">Single Password</asp:Label>
                   </td>
                   </tr>
                   <tr>
                   <td align="center">
                       <asp:TextBox ID="SingleUsername" runat="server"></asp:TextBox>
                   </td>
                   <td align="center">
                       <asp:TextBox ID="SinglePassword" runat="server"></asp:TextBox>
                   </td>
                   </tr>
                       <tr>
                           <td align="center">
                               <asp:Button ID="SaveUserCredentials" runat="server" 
                                    Text="Save Credentials" Width="120px" 
                                   onclick="SaveUserCredentials_Click" />
                           </td>
                           <td align="center">
                               <asp:Label ID="SaveCredentialsMessage" runat="server" Font-Bold="True" 
                                   Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="164px"></asp:Label>
                           </td>
                       </tr>
                   </table>
                      
                       </asp:Panel>  
                   </td></tr>
                   <tr><td>
                   <table style="width: 450px; height: 102px"><tr><td class="style72">
                               
                                            <asp:Button ID="UploadPublishedUserCredentials" runat="server" 
                                                CausesValidation="False" 
                                                Text="Upload Published User Credentials" Width="228px" style="display:none" />
                            </td><td class="style72"></td></tr><tr><td class="style66">
                               
                                            <asp:Button ID="ViewPublishedUserCredentials" runat="server" 
                                                CausesValidation="False" 
                                                Text="View Published User Credentials" Width="228px"  style="display:none"/>
                            </td><td>&nbsp;</td></tr><tr><td class="style66" colspan="2">
                               
                <asp:Label ID="SaveNumberOfUsersMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="388px" Height="16px"></asp:Label>
                            </td></tr></table>
                       </td></tr>
                   <tr><td class="style58" valign="bottom">
                       <asp:Label ID="SubmissionNotesLabel" runat="server" Font-Bold="True" 
                           Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="383px">Add any notes about your submission or update:</asp:Label>
                               
                    </td></tr><tr><td class="style53">
               
            <asp:TextBox ID="SubmissionNotes" runat="server" Height="59px" 
                TextMode="MultiLine" Width="399px"></asp:TextBox>
                
                    </td></tr><tr><td class="style59">
                <asp:Button ID="SubmitForProvisioning" runat="server" CausesValidation="False" 
                    Font-Names="Arial" Font-Size="10pt" onclick="SubmitForProvisioning_Click" 
                    Text="Submit For Publishing" Width="163px" />
                               
                                            &nbsp;&nbsp;&nbsp;
                               
                                            <asp:Button ID="PurchaseButton" runat="server" 
                                                CausesValidation="False" style="font-family:arial; font-size:12px;"                                                
                                                Text="Purchase ViziApps Services" Width="178px" />
                               
                    </td></tr><tr><td>
                    <asp:Label ID="ProvisioningMessage" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="386px" Height="62px"></asp:Label>
                    </td></tr></table>
               
            </td>
        </tr>
       
        <tr>
            <td class="style46">
               
                &nbsp;</td>
        </tr>
              <tr>
            <td class="style46">
               
    <table width="100%"><tr>
        <td class="style35">
        <asp:Label ID="SplashUploadMessage2" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="336px">Splash Image ( 320 X 460 pixels from .jpg file )</asp:Label>
            </td>
            </tr></table>
               
            </td>
        </tr>    
          <tr>
            <td class="style46">
               
                <table style="width:453px;">
                    <tr>
                        <td class="style49">
        <telerik:RadUpload ID="UploadScreenSplash" runat="server" ControlObjectsVisibility="None" OverwriteExistingFiles ="true"
             TargetFolder="~/temp_files" Skin="Windows7" Width="211px" />

   
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
            <asp:Button ID="ScreenSplashButton" runat="server" OnClientClick="var oWin=radopen('ScreenShot.aspx', 'ScreenShotBox');
                                                   oWin.set_visibleTitlebar(true);
                                                    oWin.set_visibleStatusbar(false);
                                                  oWin.set_modal(true); 
                                                  oWin.setSize(354,540);
                                                  oWin.moveTo(100, 100); 
                                                  return false;" Text="Screen Splash" 
                Visible="false" Width="105px"  /><asp:ImageButton ID="DeleteSplashImage" runat="server" 
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
               
            </td>
        </tr>    
          <tr>
            <td class="style46">
               
                &nbsp;</td>
            <td>
               
                &nbsp;</td>
        </tr>   
           </table>
	</div>

	</form>
</body>
</html>
