<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TabPublish.aspx.cs" Inherits="TabPublish" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>ViziApps: Build Mobile Apps Online</title>
    <link rel="stylesheet" type="text/css" href="~/Billing/CSS/BillingStylesheet.css" /> <!--CSS-->
           

</head>
<body class="body">
    <form id="form1" runat="server">
    

      <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>

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
      <div align="left" id="header" style="height:80px;width:100%;  background-color:#0054c2;">
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
                   
                   
                    <asp:Label ID="UserLabel" runat="server" style="color:White" Font-Size="Medium"></asp:Label>
                    
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
                             style="border-width: 0px; margin: 0px; padding: 0px; vertical-align:top; z-index:100; top: 0px; left: 0px;" 
                             TabIndex="1100"  >
                         
                            <Items>
                             <telerik:RadMenuItem ImageUrl="~/images/MySolutionsButton.png" HoveredImageUrl="~/images/MySolutionsButton_hov.png"
                        SelectedImageUrl="~/images/MySolutionsButton_sel.png"  Value="MySolutions" 
                                    TabIndex="1200"/>
                               <telerik:RadMenuItem ImageUrl="~/images/DisplayDesignButton.png" HoveredImageUrl="~/images/DisplayDesignButton_hov.png"
                        SelectedImageUrl="~/images/DisplayDesignButton_sel.png"  Value="DesignNative" TabIndex="1300" ><Items>
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

    

     <table class="billingblock" >
     <tr>
     
     <!-- Links Table -->
     <td valign="top" >
     <table style="width: 300px">
          <tr><td  > 
             <asp:Button ID="PublishingFormButton" 
                 runat="server"  Text="1. Submit Publishing Form" CssClass="bluebutton" 
                 onclick="PublishingFormButton_Click" Width="285px" /> </td> </tr>


         <tr><td  > 
             <asp:Button ID="AppBrandingButton" 
                 runat="server"  Text="2. App Store Preparation" CssClass="bluebutton" 
                 onclick="AppBrandingButton_Click" Width="285px" /> </td> </tr>
         
         <tr><td > 
             <asp:Button ID="NewSubscriptionButton" 
                 runat="server"  CssClass="bluebutton" Text="3. Add Publishing Service"  
                 onclick="NewSubscriptionButton_Click" Width="285px" /> </td></tr>
         
         <tr><td > 
             <asp:Button ID="ModifySubscriptionButton" 
                runat="server" CssClass="bluebutton" Text="4. Modify Publishing Service" 
                onclick="ModifySubscriptionButton_Click" Width="285px" /> </td></tr>
         

         <tr><td > 
             <asp:Button ID="CancelSubscriptionButton" 
                runat="server" CssClass="bluebutton" Text="5. Cancel Publishing Service" 
                onclick="CancelSubscriptionButton_Click" Width="285px" /> </td></tr>


         <tr><td > 
             <asp:Button ID="BillingHistoryButton" 
                runat="server" CssClass="bluebutton" Text="6. Show Billing History" 
                Width="285px" onclick="BillingHistoryButton_Click" /> </td></tr>

       </table>
     </td>
     <!-- Links Table -->

     


     <!-- Native & Web Content Table -->
     <td valign="top"> 
         <center><h3>App Submission and Publishing Steps</h3> </center>


     <table>
     <tr><td ><p class="billinginstructions"><strong> 1. Submit Publishing Form</strong>
     Submit the publishing form along with the splash screen and the icon for the App. </p>
         <p class="billinginstructions">For Web Apps, ViziApps Engineering team will host 
             your published web app but if you fail to add a publishing service (step3 below) 
             your app will stop functioning after 14 days. </p>
     </td></tr>


     <tr><td >
         <p class="billinginstructions"><strong> 2. App Store Preparation (Native and 
             Hybrid Apps only):</strong>
     ViziApps Engineering team will take the App that you have designed and create the final 
             store ready version of your application and will work with you through the store 
             submission process.</p>
         <p class="billinginstructions">Note: If you are planning to create mulitple Apps 
             for different target platforms from one Design then 
             this step needs to be done separately for each of App.</p>
     </td></tr>
         
     

     
     <tr><td><p class="billinginstructions"><strong>3. Add Publishing Service </strong>
             </p>
         <p class="billinginstructions">In order for your App to be usable after the initial 
             14 days, you have to purchase a ViziApps Publishing Service.&nbsp; Details of 
             the&nbsp; different services offered are available on the 
             <a href="http://www.viziapps.com/features-pricing/" target="new"> ViziApps website. </a>When you are ready to purchase a 
             publishing service click on the button 
             <em>&quot;</em>3.Add Publishing Service&quot; on the left<em>. </em> <br />
             </p>
         <p class="billinginstructions">Note: If you are planning to create mulitple Apps 
             for different target platforms from one Design then 
             this step needs to be done separately for each of App.<br />
             </p>
          </td>
          </tr>

        
     <tr><td><p class="billinginstructions"> <strong>4. Modifying Publishing Service </strong>
      Select this link to change your current App Publishing Service.</p>
     </td></tr>

     <tr><td><p class="billinginstructions"><strong>5. Cancel Publishing Service </strong>
     Select this link to cancel your current App Publishing Service.</p>
     <td></tr>

     <tr><td><p class="billinginstructions"><strong>6.Show Billing History</strong>
     Select this link to see your Billing History.</p>
     </td>
     </tr>
     </table>

     </td>
     <!-- Web Content Table -->

     </tr>
     </table>

    </form>
</body>
</html>
