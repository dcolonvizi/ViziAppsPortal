<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CancelPublishingService.aspx.cs" Inherits="CancelPublishingService" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

<title>ViziApps:App Billing</title>

    <link rel="stylesheet" type="text/css" href="CSS/BillingStylesheet.css" />
    <link rel="stylesheet" type="text/css" href="CSS/jquery.toastmessage.css" />


    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js"></script>
    <script type="text/javascript"  src="Javascript/jquery.toastmessage.js"></script>


    <script type="text/javascript">

        $(document).ready(function () {

        });


        function redirect(url) {
            window.location = url;
        }


        function OnAjaxResponse(sender, args) {

            console.log("OnAjaxResponse");


            var cgresp = null;

            if (cgresp == null)
                cgresp = document.getElementById("<%= CGResponseFlag.ClientID %>").value;



            if (cgresp != "") {

                $().toastmessage('showToast', {
                    text: '<h4>Your request to cancel has been processed. You will get an email shortly. </h4> <br><br><br> <small>Redirecting page shortly.... ',
                    sticky: true,
                    position: 'middle-center',
                    type: 'success',
                    closeText: 'OK',
                    close: function () {
                        console.log("toast is closed ...");
                    }
                });

                var location = '../TabPublish.aspx';

                setTimeout(function () { redirect(location); }, 6000);
            }


        }
     </script>
     
    </telerik:RadCodeBlock>

</head>
<body>

       

    <form id="CancelBilling" runat="server">


    
    
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

      <telerik:RadScriptManager ID="RadScriptManager" runat="server"> </telerik:RadScriptManager>

      <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
         <AjaxSettings>
             <telerik:AjaxSetting AjaxControlID="RadComboAppSelector">
                 <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="RadComboAppSelector" />
                     <telerik:AjaxUpdatedControl ControlID="CancelSubscriptionsButton" />
                     <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                     <telerik:AjaxUpdatedControl ControlID="cgbilling" />
                 </UpdatedControls>
             </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="CancelSubscriptionsButton">
                 <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="CGResponseFlag" />
                     <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                 </UpdatedControls>
             </telerik:AjaxSetting>
         </AjaxSettings>
        <ClientEvents OnResponseEnd="OnAjaxResponse" ></ClientEvents>
        
          

        </telerik:RadAjaxManager>



    


 <!-- OUTERTABLE -->    
    <table width=100%>
    <tr>
    <td valign="top">
    <asp:Button ID="Button1" runat="server" onclick="BackButton_Click" class="bluebutton"  Text="Back" Height="47px" Width="73px" />
    </td>
     
    <td>  
    <td>
        <center>
        <table class="cancelblock"> 
    <tr><td colspan="2"> 
    
     <center> <h3>Cancel Publishing Service</h3> <br />
     
        
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

                 <Items></Items>

                <WebServiceSettings>
                <ODataSettings InitialContainerName=""></ODataSettings>
                </WebServiceSettings>
                 </telerik:RadComboBox>
            <br />
            <br />


                <asp:TextBox ID="CGResponseFlag" runat='server' style="display:none"></asp:TextBox>
                <br /><br />
                </center>
    </td>
    </tr>


    <tr> <td class="billing info"> <div id="cgbilling" class="canceldetails" runat="server"></div></td></tr>         
    </table>
        
        <asp:Button ID="CancelSubscriptionsButton" runat="server" onclick="CancelSubscriptionsButton_Click" Text="Submit"  CssClass="bluebutton"/> <br />
        </center>
    </td>
    </tr>
    </table>
           
       <telerik:RadNotification ID="RadNotification1"  runat="server"  
                        VisibleOnPageLoad="false"  
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
                        TitleIcon="images/billing_images/warning_title.png">

     </telerik:RadNotification>

</center>
</form>
</body>
</html>
