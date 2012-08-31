<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublishingForm.aspx.cs" Inherits="ProvisionForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>App Publishing Form</title>
    <link rel="stylesheet" type="text/css" href="CSS/BillingStylesheet.css" />


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
    <script type="text/javascript" >
        function showSplashScreen() {
            var oWin = radopen('../Dialogs/Publish/ScreenShot.aspx', 'ScreenShotBox');
            oWin.set_visibleTitlebar(true);
            oWin.set_visibleStatusbar(false);
            oWin.set_modal(true);

            var SelectedDeviceType = document.getElementById('SelectedDeviceType');
            switch (SelectedDeviceType.value) {
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

	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">	</telerik:RadScriptManager>


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
            </Windows>
            </telerik:RadWindowManager>
      <telerik:RadAjaxLoadingPanel ID="WholePageLoadingPanel" runat="server" Skin="Default" 
                                Transparency="0" BackColor="LightGray"  IsSticky="true"
	                            CssClass="MyModalPanel"></telerik:RadAjaxLoadingPanel>
      <div align="center" id="header" style="height:80px;width:100%;  background-color:#0054c2;">
                   <div style="height:10px;"></div>
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%" >
                <tr>                
               
                <td><a href="http://ViziApps.com" style="text-decoration:none">
                <asp:Image ID="HeaderImage" ImageUrl="~/images/logo_header_300.png" runat="server" style="border:0px">
                                                            </asp:Image></a>
                    </td>
                    <td> <table style="width: 335px"><tr><td>
                        &nbsp;</td><td>
                            &nbsp;</td><td>
                            &nbsp;</td></tr></table></td>
               
                <td class="style25">
                   
                   
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
               <table border="0" cellpadding="0" cellspacing="0" id="tabs"  
                   style="width:100%;height:30px; border:0px; padding:0px;  vertical-align:top;  margin:0px; background-image:url('../images/tabs_section.gif'); background-repeat:repeat-x">
                    <tr><td align="center" valign="top">
                     <div align="center">
                      
                     <table border="0" cellpadding="0" cellspacing="0" style="height:30px;" ><tr>
                     <td >
                       
                      <telerik:RadMenu ID="TabMenu" runat="server" Skin=""
                                  onitemclick="TabMenu_ItemClick"                                    
                             style="border-width: 0px; margin: 0px; padding: 0px; vertical-align:top; z-index:100; top: 0px; left: 0px;" 
                             TabIndex="1100">
                         
                            <Items>
                             <telerik:RadMenuItem ImageUrl="~/images/MySolutionsButton.png" HoveredImageUrl="~/images/MySolutionsButton_hov.png"
                        SelectedImageUrl="~/images/MySolutionsButton_sel.png"  Value="MySolutions" 
                                    TabIndex="1200"/>
                               <telerik:RadMenuItem ImageUrl="~/images/DisplayDesignButton.png" HoveredImageUrl="~/images/DisplayDesignButton_hov.png"
                        SelectedImageUrl="~/images/DisplayDesignButton_sel.png"  Value="DesignNative" 
                                    TabIndex="1300" ><Items>
                                 <telerik:RadMenuItem  ImageUrl="~/images/DisplayNativeDesignButton_sel.png" HoveredImageUrl="~/images/DisplayNativeDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayNativeDesignButton_sel.png" Value="DesignNative"/>
                               <telerik:RadMenuItem  ImageUrl="~/images/DisplayWebDesignButton.png" HoveredImageUrl="~/images/DisplayWebDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayWebDesignButton_sel.png" Value="DesignWeb"/>
                                 <telerik:RadMenuItem  ImageUrl="~/images/DisplayHybridDesignButton.png" HoveredImageUrl="~/images/DisplayHybridDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayHybridDesignButton_sel.png" Value="DesignHybrid"/>
                                 </Items>
                             </telerik:RadMenuItem>

                            <telerik:RadMenuItem ImageUrl="~/images/ProvisionButton_sel.png" HoveredImageUrl="~/images/ProvisionButton_hov.png"
                        SelectedImageUrl="~/images/ProvisionButton_sel.png"  Value="Publish" TabIndex="1500"/>
                            <telerik:RadMenuItem ImageUrl="~/images/FAQButton.png" HoveredImageUrl="~/images/FAQButton_hov.png"
                        SelectedImageUrl="~/images/FAQButton_sel.png"  Value="FAQ" TabIndex="1600"/>
                       <telerik:RadMenuItem ImageUrl="~/images/MyProfileButton.png" HoveredImageUrl="~/images/MyProfileButton_hov.png"
                        SelectedImageUrl="~/images/MyProfileButton_sel.png"  Value="MyProfile" TabIndex="1700"/>
                       

                         </Items>

<WebServiceSettings>
<ODataSettings InitialContainerName=""></ODataSettings>
</WebServiceSettings>
                          </telerik:RadMenu>
                      
                       
                          </td></tr>
                          </table>
                        
                    </div>
                </td></tr>
                </table>   
               
                </div>  







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


    <table width=100%>
    <tr>
    <td valign="top">
    <asp:Button ID="BackButton" runat="server" onclick="BackButton_Click" class="bluebutton"  Text="Back" Height="47px" Width="73px" />
    </td>
     
    
    <!-- Right Section -->
    <td>  


    <center>
    <h3>Submit Publishing Form</h3>
            <telerik:RadComboBox ID="RadComboAppSelector" 
                                    runat="server" 
                                    AutoPostBack="True" 
                                     Font-Names="Arial" 
                                     Font-Size="12pt" 
                                     Width="350px" 
                                     MarkFirstMatch="True"  
                                     Skin="Web20" 
                                     Label="Select your App"
                                     CssClass="combolabel"
                                     onselectedindexchanged="RadComboAppSelector_SelectedIndexChanged">

                 <Items>
                 </Items>

                <WebServiceSettings>
                <ODataSettings InitialContainerName=""></ODataSettings>
                </WebServiceSettings>
                 </telerik:RadComboBox>
            <br />
            <br />
            <asp:TextBox ID="CGResponseFlag" runat='server' style="display:none"></asp:TextBox>
            <br />







        <div>
        <asp:Label ID="App" runat="server" Font-Bold="True"  Font-Names="Arial" Font-Size="14pt" ForeColor="#003399"     Width="620px"></asp:Label>
        <br />
        </div>



        <div id="NATIVE" runat="server">

        <table style="width: 900px;">
        <tr>
            <td class="style67" valign="top">                
            <table width="100%" ><tr>
            <td class="style61" valign="top" colspan="2"> 
            <table style="height: 93px; width: 472px;">
                <tr>
                    <td class="style63">
                    <asp:Label ID="ProductionAppNameLabel" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="143px" Height="16px">Publishing App Name</asp:Label>            
                </td>
                <td class="style62">
                       <asp:TextBox ID="ProductionAppName" runat="server" Width="276px"></asp:TextBox>
                </td>
                </tr>
                
                <tr><td class="style64">
                <asp:Button runat="server" ID="SaveProductionAppNameButton" Text="Save Publishing App Name" Width="176px" onclick="SaveProductionAppNameButton_Click"  class="bluesmallbutton" />
               
                </td><td class="style70">
               
                <asp:Label ID="SaveProductionAppNameMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="270px"></asp:Label>
               
                </td></tr></table>
            
               
            </td>
            </tr><tr>
        <td class="style69" valign="top"> 
        <table><tr><td>
    <asp:Button runat="server" ID="CopyDesignToProduction" Text="Save Test Design To Publishing Design" Width="246px" onclick="CopyDesignToProduction_Click" class="bluesmallbutton" /></td>
    <td>
            
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
        <telerik:RadUpload ID="UploadLargeIcon" runat="server" ControlObjectsVisibility="None" 
                                OverwriteExistingFiles ="true" TargetFolder="~/temp_files" Skin="Web20" 
                                Width="224px" />

   
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
                        <asp:Button runat="server" ID="UploadLargeIconButton" Text="Upload File" onclick="UploadLargeIconButton_Click" class="bluesmallbutton" />
                    </td>
                    
                    <td class="style34">     &nbsp;</td>

                   <td class="style34">
                     <asp:Button ID="LargeIconButton" runat="server" OnClientClick="var oWin=radopen('../Dialogs/Publish/LargeIcon.aspx', 'LargeIconBox');
                                                                                   oWin.set_visibleTitlebar(true);
                                                                                    oWin.set_visibleStatusbar(false);
                                                                                  oWin.set_modal(true); 
                                                                                  oWin.setSize(550,600);
                                                                                  oWin.moveTo(100, 100); 
                                                                                  return false;" Text="Large Icon" 
                                                                    Visible="false" 
                                                                    Width="95px" 
                                                                    class="bluesmallbutton" />


                       <asp:ImageButton    ID="DeleteIcon" 
                                                runat="server" 
                                                ImageUrl="../images/delete_small.gif" 
                                                AlternateText="Delete Icon" 
                                                onclick="DeleteIcon_Click" 
                                                Visible="False" />
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
            </tr>
            </table>
            <br />   

            <table width="100%"><tr>
        <td class="style35">
        <asp:Label ID="SplashUploadMessage2" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="336px">Splash Image ( 320 X 460 pixels from .jpg file )</asp:Label>
            </td>
            </tr></table>
               
                <table style="width:453px;">
                    <tr>
                        <td class="style49">
                      <telerik:RadUpload ID="UploadScreenSplash" runat="server" ControlObjectsVisibility="None" OverwriteExistingFiles ="true"
                                  TargetFolder="~/temp_files" Skin="Web20" Width="211px" />

   
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
    <asp:Button runat="server" ID="UploadScreenSplashButton" Text="Upload File" onclick="UploadScreenSplashButton_Click" class="bluesmallbutton" />
                        </td>
                        <td class="style34">
                            &nbsp;</td>
                        <td class="style34">
            <asp:Button ID="ScreenSplashButton" runat="server" OnClientClick="var oWin=radopen('../Dialogs/Publish/ScreenShot.aspx', 'ScreenShotBox');
                                                   oWin.set_visibleTitlebar(true);
                                                    oWin.set_visibleStatusbar(false);
                                                  oWin.set_modal(true); 
                                                  oWin.setSize(354,540);
                                                  oWin.moveTo(100, 100); 
                                                  return false;" Text="See Splash Screen" 
                                                  Visible="false" Width="126px"   
                                class="bluesmallbutton" />
                
                <asp:ImageButton ID="DeleteSplashImage" runat="server" 
                                ImageUrl="../images/delete_small.gif" AlternateText="Delete Splash Image" 
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


            <td class="small_gap">
            </td>
            <td valign="top" class="style68">
               <table><tr><td>
               <table style="width: 401px"><tr><td>
                     <asp:CheckBox  Text="Limited Users with 1 password for all" ID="NumberOfUsersCheckbox" 
                         runat="server" oncheckedchanged="NumberOfUsersCheckbox_CheckedChanged"  />
                    <!-- Value="limited_1_password_all" / -->
                    </td>
                    
                    <td>







                   </td>
                   </tr>
                   </table>
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
                               <asp:Button ID="SaveUserCredentials" runat="server" Text="Save Credentials" Width="120px" onclick="SaveUserCredentials_Click" class="bluesmallbutton"/>
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
                   <table style="width: 450px; height: 102px">
                   <tr><td class="style72">                              
                                    <asp:Button ID="UploadPublishedUserCredentials" runat="server"  CausesValidation="False" Text="Upload Published User Credentials" Width="228px" style="display:none" class="bluesmallbutton" />  
                        </td>
                        <td class="style72"></td>
                    </tr>



                    <tr><td class="style66">
                               
                             <asp:Button ID="ViewPublishedUserCredentials" runat="server" CausesValidation="False" Text="View Published User Credentials" Width="228px"  style="display:none" class="bluesmallbutton"/>
                            </td><td>&nbsp;</td></tr>
                            
                            <tr><td class="style66" colspan="2">
                               
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
                <asp:Button ID="SubmitPublishingForm" runat="server" CausesValidation="False" onclick="SubmitPublishingFormClicked"  Text="Submit Publishing Form"  class="bluebutton" />
                               
                               
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
               
                &nbsp;</td>
        </tr>    
 
           </table>
	</div>
    






        <div ID="WEB" runat="server">
        <table style="width: 900px;">
        <tr>
        <td class="style67" valign="top">
                
	    <table width="100%">
	    <tr>
	    <td class="style61" valign="top" colspan="2"> 
	      
		    <table style="height: 93px; width: 472px;">

		    <tr>
			    <td class="style63">
	        	        <asp:Label ID="Web_ProductionAppNameLabel" 
				    runat="server" 
				    Font-Bold="True"  
				    Font-Names="Arial" 
				    Font-Size="10pt" 
				    ForeColor="#003399" 
				    Width="143px" 
				    Height="16px">Publishing App Name</asp:Label>
	                    </td>
			    <td class="style62"> <asp:TextBox ID="Web_ProductionAppName" runat="server" Width="276px"></asp:TextBox> </td>
		    </tr>
	
		    <tr>
		    <td class="style64">
                	    <asp:Button runat="server" ID="Web_SaveProductionAppNameButton" Text="Save Publishing App Name"  Width="176px" onclick="Web_SaveProductionAppNameButton_Click"  class="bluesmallbutton" />
	            </td>

		    <td>
               		    <asp:Label ID="Web_SaveProductionAppNameMessage" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="270px"></asp:Label>
	            </td>
		    </tr>
		    </table>
            
              
        </td>
        </tr>
	

	    <tr>
        <td class="style56" valign="top"> 
        

		<table>
		<tr>
		<td>
		    <asp:Button runat="server" ID="Web_CopyDesignToProduction"   Text="Save Test Design To Publishing Design" Width="252px" onclick="Web_CopyDesignToProduction_Click"  class="bluesmallbutton" />
		</td>
		
		<td>
	            <asp:Image ID="Web_ProductionDesignExists" runat="server" ImageUrl="../../images/check.gif" AlternateText="Publication Design Exists" Visible="False" />
		</td>
		</tr>
		</table>

        </td>

        <td class="style57" valign="top">
                <asp:Label ID="Web_CopyDesignMessage" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="189px"></asp:Label>
        </td>
	</tr>

	<tr>
        <td class="style69" colspan="2">
        <asp:Label ID="Web_IconUploadLabel" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="386px">Application Icon ( large 512 X 512 pixels from .jpg file )</asp:Label>
        </td>
        </tr>


	<tr>
        <td class="style31" colspan="2">
              <table style="width:454px; height: 237px;">
                    <tr>
                        <td class="style48">
		        <telerik:RadUpload ID="Web_UploadLargeIcon" 
				runat="server" 
				ControlObjectsVisibility="None" 
				OverwriteExistingFiles ="true"   
				TargetFolder="~/temp_files" 
				Skin="Web20" 
				Width="224px" />
 
	                </td>
                        <td style=" font-family:Tahoma; font-size:12px">  &nbsp;</td>
                        <td style=" font-family:Tahoma; font-size:12px">
             
                            <asp:Repeater ID="Web_repeaterResultsLargeIcon" runat="server" Visible="False"> 
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
			    <asp:Button runat="server" ID="Web_UploadLargeIconButton" Text="Upload File" onclick="Web_UploadLargeIconButton_Click" class="bluesmallbutton"  />
                        </td>

                        <td class="style34"> &nbsp;</td>
                        <td class="style34">
			        <asp:Button ID="Web_LargeIconButton" runat="server" 
				     OnClientClick="var oWin=radopen('../Dialogs/Publish/LargeIcon.aspx', 'LargeIconBox');  
							            oWin.set_visibleTitlebar(true);  
							            oWin.set_visibleStatusbar(false);  
							            oWin.set_modal(true); 
	                                    oWin.setSize(550,600);
                                        oWin.moveTo(100, 100); 
                                        return false;" 
                                        Text="See Large Icon" 
					                    Visible="false" 
                                        Width="107px" 
                                        class="bluesmallbutton" />
		
		                <asp:ImageButton ID="Web_DeleteIcon" runat="server" ImageUrl="../images/delete_small.gif" 
                                AlternateText="Delete Icon" onclick="Web_DeleteIcon_Click" Visible="False" />
                            </td>
                    </tr>
                    <tr>
                        <td class="style40" colspan="3">

	                <asp:Label ID="Web_IconUploadMessage" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="315px">
			</asp:Label>
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
        <asp:Label ID="Web_SplashUploadLabel" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt" ForeColor="#003399" Width="326px">Splash Image ( 320 X 460 pixels from .jpg file )</asp:Label>
            </td>
            </tr></table>
               
               </td></tr><tr>
               <td class="style65">
               
                <table style="width:453px;">
                    <tr>
                        <td class="style49">

	        <telerik:RadUpload ID="Web_UploadScreenSplash" runat="server" ControlObjectsVisibility="None" OverwriteExistingFiles ="true"
        	     TargetFolder="~/temp_files" Skin="Web20" Width="211px" />

   
	                    </td>
                        <td style=" font-family:Tahoma; font-size:12px">
             
                            &nbsp;</td>
                        <td style=" font-family:Tahoma; font-size:12px">
             
                            <asp:Repeater ID="Web_repeaterResultsScreenSplash" runat="server" Visible="False">
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
    			<asp:Button runat="server" ID="Web_UploadScreenSplashButton" Text="Upload File" onclick="Web_UploadScreenSplashButton_Click" class="bluesmallbutton"  />
                        </td>
                        <td class="style34">
                            &nbsp;</td>
                        <td class="style34">
	
			        <asp:Button ID="Web_ScreenSplashButton" runat="server" Text="See Screen Splash" 
                                Visible="false" Width="126px" class="bluesmallbutton" 
                                />




				<asp:ImageButton ID="Web_DeleteSplashImage" runat="server" 
					ImageUrl="../images/delete_small.gif" AlternateText="Delete Splash Image" 
	                                onclick="Web_DeleteSplashImage_Click" Visible="False" />
	
                           </td>
                    	</tr>
                    	<tr>
                        <td class="style39" colspan="3">

	                	<asp:Label ID="Web_SplashUploadMessage" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="272px">
				</asp:Label>
                        </td>
                    	</tr>
                    </table>
               
                   </td>
		</tr>
                   <tr><td class="style70">
               
                    </td></tr><tr><td class="style59">

                <asp:Button ID="Web_SubmitPublishingForm" runat="server" CausesValidation="False" onclick="Web_SubmitPublishingForm_Click" 
                    Text="Submit Publishing Form"  class="bluebutton" />
                        
                               
                    </td></tr><tr><td>
                    <asp:Label ID="Web_PublishMessage" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="410px" Height="62px"></asp:Label>
                    </td></tr><tr><td class="style71">
                       <table cellpadding="0" cellspacing="0" style="height: 61px" width="100%">
                           <tr>
                               <td align="left" class="style18" valign="top">
                                   <table style="width: 286px; height: 20px">
                                       <tr>
                                           <td>
                                               <asp:Label ID="Web_QRCodeLabel" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="#000099" style="display:none" Width="350px"></asp:Label>
                                           </td>
                                       </tr>

                                       <tr>
                                        <td>
                                               <img id="Web_QRCode" runat="server" alt="" height="150" src="" style="display:none" width="150" />
                                        </td>
                                       </tr>
                                    </table>
                               </td>
                           </tr>
                           <tr>
                               <td align="left" class="style19" valign="top">
                                   <asp:Label ID="Web_PublishedAppURL" runat="server" Font-Bold="True" Font-Names="Arial" 
                                       Font-Size="10pt" ForeColor="#000099" Width="436px"></asp:Label>
                               </td>
                           </tr>
                       </table>
                                                    
                                                    
                    </td></tr></table>
               
            </td>
        </tr>
       
           </table>
	    </div>



  
  
  
  
      

    </center>
    </td>
    </tr>
    </table>

          <telerik:RadNotification ID="RadNotification1"  runat="server"  
                        Width="300px" 
                        Height="150px"
                        EnableRoundedCorners="True"
                        ContentIcon="images/billing_images/warning.png"
                        Animation="Fade" 
                        AnimationDuration="1000"
                        EnableShadow="True" 
                        Position="Center"
                        Title="Notification Title" 
                        Text="Notification"
                        Style="z-index: 35000" 
                        AutoCloseDelay="5000" 
                        ForeColor="Red"
                        Font-Bold="True" 
                        BorderStyle="Groove"
                        BorderColor="#5370A6"
                        TitleIcon="images/billing_images/warning_title.png" 
                        VisibleOnPageLoad="false" >
        </telerik:RadNotification>   

	</form>
</body>
</html>
