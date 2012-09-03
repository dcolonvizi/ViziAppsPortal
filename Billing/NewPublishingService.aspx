<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewPublishingService.aspx.cs" Inherits="NewPublishingService" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

<title>ViziApps Billing</title>

    <link rel="stylesheet" type="text/css" href="CSS/BillingStylesheet.css" />
    <link rel="stylesheet" type="text/css" href="CSS/jquery.toastmessage.css" />


    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js"></script>
    <script type="text/javascript"  src="Javascript/jquery.toastmessage.js"></script>
    
    <script type="text/javascript">

        $(document).ready(function () {

        });

        function submitclientclicked() {

            if (typeof (Page_ClientValidate) == 'function') {

                if (Page_ClientValidate() == false) {
                    return false;
                }
            }


            var mySubmitButton = document.getElementById('SubmitButton');
            mySubmitButton.disabled = true; 
            mySubmitButton.value = "Submitting.."; 
            
        }

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
                    text: '<h4>Your order has been processed. You will get an email shortly. </h4> <br><br><br> <small>Redirecting page shortly.... ',
                    sticky: true,
                    position: 'middle-center',
                    type: 'success',
                    closeText: 'OK',
                    close: function () {
                        console.log("toast is closed ...");
                    }
                });

                var location = '../TabPublish.aspx';

                setTimeout(function () { redirect(location); }, 5000);
            }


        }
     </script>
     
    </telerik:RadCodeBlock>

</head>



<body class="BODY">
    <form runat="server" id="mainForm" method="post">

     <telerik:RadScriptManager ID="RadScriptManager" runat="server"> </telerik:RadScriptManager>

     <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnResponseEnd="OnAjaxResponse" ></ClientEvents>
        
          <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="SubmitButton">
                  <UpdatedControls>
                      <telerik:AjaxUpdatedControl ControlID="CGResponseFlag" />
                      <telerik:AjaxUpdatedControl ControlID="SubmitButton" />
                      <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
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
                        SelectedImageUrl="~/images/MySolutionsButton_sel.png"  Value="TabMySolutions.aspx" 
                                    TabIndex="1200"/>
                               <telerik:RadMenuItem ImageUrl="~/images/DisplayDesignButton.png" HoveredImageUrl="~/images/DisplayDesignButton_hov.png"
                        SelectedImageUrl="~/images/DisplayDesignButton_sel.png"  Value="TabDesignNative.aspx" TabIndex="1300" ><Items>
                                 <telerik:RadMenuItem  ImageUrl="~/images/DisplayNativeDesignButton_sel.png" HoveredImageUrl="~/images/DisplayNativeDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayNativeDesignButton_sel.png" Value="TabDesignNative.aspx"/>
                               <telerik:RadMenuItem  ImageUrl="~/images/DisplayWebDesignButton.png" HoveredImageUrl="~/images/DisplayWebDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayWebDesignButton_sel.png" Value="TabDesignWeb.aspx"/>
                                 <telerik:RadMenuItem  ImageUrl="~/images/DisplayHybridDesignButton.png" HoveredImageUrl="~/images/DisplayHybridDesignButton_hov.png"  SelectedImageUrl="~/images/DisplayHybridDesignButton_sel.png" Value="TabDesignHybrid.aspx"/>
                                 </Items>
                             </telerik:RadMenuItem>

                            <telerik:RadMenuItem ImageUrl="~/images/ProvisionButton_sel.png" HoveredImageUrl="~/images/ProvisionButton_hov.png"
                        SelectedImageUrl="~/images/ProvisionButton_sel.png"  Value="Publish" TabIndex="1500"/>
                            <telerik:RadMenuItem ImageUrl="~/images/FAQButton.png" HoveredImageUrl="~/images/FAQButton_hov.png"
                        SelectedImageUrl="~/images/FAQButton_sel.png"  Value="TabFAQ.aspx" TabIndex="1600"/>
                       <telerik:RadMenuItem ImageUrl="~/images/MyProfileButton.png" HoveredImageUrl="~/images/MyProfileButton_hov.png"
                        SelectedImageUrl="~/images/MyProfileButton_sel.png"  Value="TabMyProfile.aspx" TabIndex="1700"/>
                       

                         </Items>

                          </telerik:RadMenu>
                      
                       
                          </td></tr>
                          </table>
                        
                    </div>
                </td></tr>
                </table>   
               
                </div>  








     <div style="height:15px"></div>
    
     <div align="center" class="billinglabel">New Publishing Service</div>


   
    
    
    <div align="center">
    <table style="width:800px">
    <tr>
    <td valign="top" class="branding_icon">
    <asp:Button ID="BackButton" runat="server" onclick="BackButton_Click" 
            class="bluebutton"  Text="Back" Height="30px" Width="73px" />
    </td>
     
     
     
     
     <td>  
    
    <!-- TABGRID  TABLE -->
    <table id="Table1" class="tabgridpage" runat="server">
    <tr>
    <td>
    
    
    

    <table align="center">
    <tr>
    
    <td>
           
             <telerik:RadComboBox ID="RadComboAppSelector" 
                                    runat="server" 
                                    AutoPostBack="True" 
                                     Font-Names="Arial" 
                                     Font-Size="12pt" 
                                     Width="350px" 
                                     MarkFirstMatch="True"  
                                     Skin="Vista" 
                                     Label="Select your App   "
                                     CssClass="combolabel"
                                     
                 onselectedindexchanged="RadComboAppSelector_SelectedIndexChanged">

                 </telerik:RadComboBox>
            <br />
            <br />

    </td>
    
    
    </tr>
    
    <tr>
    <td>

        <telerik:RadMultiPage ID="RadMultiPageMaster" runat="server" SelectedIndex="0" CssClass="multiPage" Visible="false">
        <telerik:RadPageView ID="RadPageNativeSection" runat="server">
        <center><asp:Label ID="Label1" runat="server">Native / Hybrid Apps</asp:Label></center>
        <br />

        <telerik:RadTabStrip ID="RadTabStripNative" 
                             runat="server" 
                             MultiPageID="NativePricingMultiPage" 
                             EnableTheming="True"
                             Font-Size="12pt" 
                             Width="450px" 
                             Skin="Web20" >

         
               
                <Tabs>
                    <telerik:RadTab Text="Standard Pricing"  TabIndex="0" Font-Size="12pt"  ></telerik:RadTab>
                    <telerik:RadTab Text="Pre-Pay Yearly" TabIndex="1"    Font-Size="12pt" ></telerik:RadTab>
                    <telerik:RadTab Text="Non-Profits" TabIndex="2"       Font-Size="12pt"  ></telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="NativePricingMultiPage" runat="server" SelectedIndex="0" 
                CssClass="multiPage" >
           

                <telerik:RadPageView ID="RadPageView1" runat="server">
                <table>
                <tr>
                <td valign="top">
                
                   <table class="plan">
                       <tr><th class="ticktext">Selected</th></tr>
                       <tr><th class="plan">Plan</th></tr>
                       <tr><td class="plan">Service Fee</td></tr>
                       <tr><td class="plan">Number of end users</td></tr>
                       <tr><td class="plan">Number of Pages</td></tr> 
                   </table>
                 </td>
                 
                <td valign="top">

                   <table class="basicproprem" onmouseover=""  onmouseout="clearhelp" >
                           <tr><td class="tick">
                               <asp:Image ID="Tick1" runat="server" 
                                   ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                           <tr><th class="basicproprem"><asp:Button ID="MonthlyBasicButton" runat="server" CssClass="tabgridbutton" onclick="MonthlyBasicButton_Click" Text="Basic" />                               </th></tr>
                           <tr><td class="basicproprem"><span class="highlight">$49/mo.</span></td></tr>
                           <tr><td class="basicproprem">Up to 100</td></tr>
                           <tr><td class="basicproprem">Up to 5</td></tr>
                           
                    </table>
                  </td>


                  <td valign="top">


                    <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                            <tr><td class="tick">
                                <asp:Image ID="Tick2" runat="server" 
                                    ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                            <tr> <th class="basicproprem"><asp:Button ID="MonthlyProButton" runat="server" CssClass="tabgridbutton" onclick="MonthlyProButton_Click" Text="Pro" />                                </th></tr>
                            <tr> <td class="basicproprem"><span class="highlight">$89/mo.</span></td></tr>          
                            <tr> <td class="basicproprem">Up to 500</td></tr>
                            <tr> <td class="basicproprem">Up to 10</td></tr>                     
                            
                    </table>
                    </td>


                    <td valign="top">
                    <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                             <tr><td class="tick">
                                 <asp:Image ID="Tick3" runat="server" 
                                     ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                             <tr><th class="basicproprem"><asp:Button ID="MonthlyPremiumButton" runat="server" CssClass="tabgridbutton" onclick="MonthlyPremiumButton_Click" Text="Premium" />                                 </th></tr>
                             <tr><td class="basicproprem"><span class="highlight">$149/mo.</span></td></tr>
                             <tr><td class="basicproprem">Unlimited</td></tr>
                             <tr><td class="basicproprem">Unlimited</td></tr>
                             
                     </table> 
                </td>
                </tr>
                </table>          
                </telerik:RadPageView>


                <telerik:RadPageView ID="RadPageView2" runat="server">
                <table>
                <tr>
                  <td valign="top">
                
                   <table class="plan">
                       <tr><th class="ticktext">Selected</th></tr>
                       <tr><th class="plan">Plan</th></tr>
                       <tr><td class="plan">Service Fee</td></tr>
                       <tr><td class="plan">Number of end users</td></tr>
                       <tr><td class="plan">Number of Pages</td></tr> 
                       <tr><td></td></tr>
                       </table>
                 </td>
                 
                  <td valign="top">
                   <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                           <tr><td class="tick">
                               <asp:Image ID="Tick4" runat="server" 
                                   ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                           <tr><th class="basicproprem"><asp:Button ID="YearlyBasicButton" runat="server" CssClass="tabgridbutton" onclick="YearlyBasicButton_Click" Text="Basic" /></th></tr>
                           <tr><td class="basicproprem"><span class="highlight">$499/yr.</span></td></tr>
                           <tr><td class="basicproprem">Up to 100</td></tr>
                           <tr><td class="basicproprem">Up to 5</td></tr>
                           
                    </table>
                  </td>


                  <td valign="top">
                  <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                            <tr><td class="tick">
                                <asp:Image ID="Tick5" runat="server"  
                                    ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                            <tr> <th class="basicproprem"><asp:Button ID="YearlyProButton" runat="server" CssClass="tabgridbutton" onclick="YearlyProButton_Click" Text="Pro" /></th></tr>
                            <tr> <td class="basicproprem"><span class="highlight">$908/yr.</span></td></tr>          
                            <tr> <td class="basicproprem">Up to 500</td></tr>
                            <tr> <td class="basicproprem">Up to 10</td></tr>                     
                            
                    </table>
                    </td>


                    <td valign="top">
                    <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                             <tr><td class="tick">
                                 <asp:Image ID="Tick6" runat="server" 
                                     ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                             <tr><th class="basicproprem"><asp:Button ID="YearlyPremiumButton" runat="server" CssClass="tabgridbutton" onclick="YearlyPremiumButton_Click" Text="Premium" /> </th></tr>
                             <tr><td class="basicproprem"><span class="highlight">$1520/yr.</span></td></tr>
                             <tr><td class="basicproprem">Unlimited</td></tr>
                             <tr><td class="basicproprem">Unlimited</td></tr>
                             
                     </table> 
                </td>
                </tr>
                </table>          
                </telerik:RadPageView>

                <telerik:RadPageView ID="RadPageView3" runat="server">
                <table>
                <tr>
                   <td valign="top">
                
                   <table class="plan">
                       <tr><th class="ticktext">Selected</th></tr>
                       <tr><th class="plan">Plan</th></tr>
                       <tr><td class="plan">Service Fee</td></tr>
                       <tr><td class="plan">Number of end users</td></tr>
                       <tr><td class="plan">Number of Pages</td></tr> 
                       <tr><td></td></tr>
                   </table>
                 </td>
                 
                  <td valign="top">
                       <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                           <tr><td class="tick">
                               <asp:Image ID="Tick7" runat="server" 
                                   ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                           <tr><th class="basicproprem"><asp:Button ID="NonProfitBasicButton" runat="server" CssClass="tabgridbutton" onclick="NonProfitBasicButton_Click" Text="Basic" /></th></tr>
                           <tr><td class="basicproprem"><span class="highlight">$469/yr.</span></td></tr>
                           <tr><td class="basicproprem">Up to 100</td></tr>
                           <tr><td class="basicproprem">Up to 5</td></tr>
                          
                    </table>
                  </td>


                  <td valign="top">


                    <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                            <tr><td class="tick">
                                <asp:Image ID="Tick8" runat="server" 
                                    ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                            <tr> <th class="basicproprem"><asp:Button ID="NonProfitProButton" runat="server" CssClass="tabgridbutton" onclick="NonProfitProButton_Click" Text="Pro" /></th></tr>
                            <tr> <td class="basicproprem"><span class="highlight">$854/yr.</span></td></tr>          
                            <tr> <td class="basicproprem">Up to 500</td></tr>
                            <tr> <td class="basicproprem">Up to 10</td></tr>                     
                           
                    </table>
                    </td>


                  <td valign="top">
                    <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                             <tr><td class="tick">
                                 <asp:Image ID="Tick9" runat="server" 
                                     ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                             <tr><th class="basicproprem"><asp:Button ID="NonProfitPremiumButton" runat="server" CssClass="tabgridbutton" onclick="NonProfitPremiumButton_Click" Text="Premium" /> </th></tr>
                             <tr><td class="basicproprem"><span class="highlight">$1430/yr.</span></td></tr>
                             <tr><td class="basicproprem">Unlimited</td></tr>
                             <tr><td class="basicproprem">Unlimited</td></tr>
                             
                     </table> 
                </td>
                </tr>
                </table>          
                </telerik:RadPageView>

            </telerik:RadMultiPage>
        </telerik:RadPageView>

        <telerik:RadPageView ID="RadPageWebSection" runat="server">
        <center><asp:Label ID="Label2" runat="server">Web Apps</asp:Label></center>
        <br />    
        <telerik:RadTabStrip ID="RadTabStrip1" 
                             runat="server" 
                             MultiPageID="WebPricingMultiPage" 
                             EnableTheming="True"
                             Font-Size="12pt" 
                             Width="450px" 
                             Skin="Web20" >
        

        <Tabs>
                    <telerik:RadTab Text="Standard Pricing" TabIndex="0"    Font-Size="12pt"></telerik:RadTab>
                    <telerik:RadTab Text="Pre-Pay Yearly" TabIndex="1"      Font-Size="12pt"></telerik:RadTab>
                    <telerik:RadTab Text="Non-Profits" TabIndex="2"         Font-Size="12pt" ></telerik:RadTab>
        </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="WebPricingMultiPage" runat="server" SelectedIndex="2" 
                CssClass="multiPage" >


                <telerik:RadPageView ID="RadPageView4" runat="server">
                <table>
                <tr>
                  <td valign="top">
                
                   <table class="plan">
                       <tr><th class="ticktext">Selected</th></tr>
                       <tr><th class="plan">Plan</th></tr>
                       <tr><td class="plan">Service Fee</td></tr>
                       <tr><td class="plan">Number of end users</td></tr>
                       <tr><td class="plan">Number of Pages</td></tr> 
                       <tr><td></td></tr>
                   </table>
                 </td>
                 
                  <td valign="top">
                   <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                           <tr><td class="tick">
                               <asp:Image ID="WTick1" runat="server"  
                                   ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                           <tr><th class="basicproprem"><asp:Button ID="WebBasicMonthlyButton" runat="server" CssClass="tabgridbutton" onclick="WebBasicMonthlyButton_Click" Text="Basic" /> </th></tr>
                           <tr><td class="basicproprem"><span class="highlight">$29/mo.</span></td></tr>
                           <tr><td class="basicproprem">Up to 100</td></tr>
                           <tr><td class="basicproprem">Up to 5</td></tr>
                          
                    </table>
                  </td>


                  <td valign="top">


                    <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                            <tr><td class="tick">
                                <asp:Image ID="WTick2" runat="server" 
                                    ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                            <tr> <th class="basicproprem"><asp:Button ID="WebProMonthlyButton" runat="server" CssClass="tabgridbutton" onclick="WebProMonthlyButton_Click" Text="Pro" /></th></tr>
                            <tr> <td class="basicproprem"><span class="highlight">$89/mo.</span></td></tr>          
                            <tr> <td class="basicproprem">Up to 500</td></tr>
                            <tr> <td class="basicproprem">Up to 10</td></tr>                     
                    </table>
                    </td>


                    <td valign="top">
                    <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                             <tr><td class="tick">
                                 <asp:Image ID="WTick3" runat="server" 
                                     ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                             <tr><th class="basicproprem"><asp:Button ID="WebPremiumMonthlyButton" runat="server" CssClass="tabgridbutton" onclick="WebPremiumMonthlyButton_Click" Text="Premium" /> </th></tr>
                             <tr><td class="basicproprem"><span class="highlight">$149/mo.</span></td></tr>
                             <tr><td class="basicproprem">Unlimited</td></tr>
                             <tr><td class="basicproprem">Unlimited</td></tr>
                             
                     </table> 
                </td>
                </tr>
                </table>          
                </telerik:RadPageView>


                <telerik:RadPageView ID="RadPageView5" runat="server">
                <table>
                <tr>
                  <td valign="top">
                
                   <table class="plan">
                       <tr><th class="ticktext">Selected</th></tr>
                       <tr><th class="plan">Plan</th></tr>
                       <tr><td class="plan">Service Fee</td></tr>
                       <tr><td class="plan">Number of end users</td></tr>
                       <tr><td class="plan">Number of Pages</td></tr> 
                       <tr><td></td></tr>
                   </table>
                 </td>
                 
                  <td valign="top">
                   <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                           <tr><td class="tick">
                               <asp:Image ID="WTick4" runat="server" 
                                   ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                           <tr><th class="basicproprem"> <asp:Button ID="WebYearlyBasicButton" runat="server" Text="Basic" CssClass="tabgridbutton" onclick="WebYearlyBasicButton_Click" /></th></tr>
                           <tr><td class="basicproprem"><span class="highlight">$499/yr.</span></td></tr>
                           <tr><td class="basicproprem">Up to 100</td></tr>
                           <tr><td class="basicproprem">Up to 5</td></tr>
                   </table>
                  </td>


                  <td valign="top">


                    <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                            <tr><td class="tick">
                                <asp:Image ID="WTick5" runat="server" 
                                    ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                            <tr> <th class="basicproprem"><asp:Button ID="WebYearlyProButton" runat="server" Text="Pro" CssClass="tabgridbutton" onclick="WebYearlyProButton_Click" /></th></tr>
                            <tr> <td class="basicproprem"><span class="highlight">$908/yr.</span></td></tr>          
                            <tr> <td class="basicproprem">Up to 500</td></tr>
                            <tr> <td class="basicproprem">Up to 10</td></tr>                     
                    </table>
                    </td>


                    <td valign="top">
                    <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                             <tr><td class="tick">
                                 <asp:Image ID="WTick6" runat="server" 
                                     ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                             <tr><th class="basicproprem"><asp:Button ID="WebYearlyPremiumButton" runat="server" Text="Premium" CssClass="tabgridbutton" onclick="WebYearlyPremiumButton_Click" /></th></tr>
                             <tr><td class="basicproprem"><span class="highlight">$1520/yr.</span></td></tr>
                             <tr><td class="basicproprem">Unlimited</td></tr>
                             <tr><td class="basicproprem">Unlimited</td></tr>
                     </table> 
                </td>
                </tr>
                </table>          
                </telerik:RadPageView>

                <telerik:RadPageView ID="RadPageView6" runat="server">
                <table>
                <tr>
                <td valign="top">
                
                   <table class="plan">
                       <tr><th class="ticktext">Selected</th></tr>
                       <tr><th class="plan">Plan</th></tr>
                       <tr><td class="plan">Service Fee</td></tr>
                       <tr><td class="plan">Number of end users</td></tr>
                       <tr><td class="plan">Number of Pages</td></tr> 
                       <tr><td></td></tr>
                   </table>
                 </td>
                 
                  <td valign="top">
                   <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                           <tr><td class="tick">
                               <asp:Image ID="WTick7" runat="server" 
                                   ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                           <tr><th class="basicproprem"> <asp:Button ID="WebNonProfitBasicButton" runat="server" Text="Basic"  CssClass="tabgridbutton" onclick="WebNonProfitBasicButton_Click" />   </th></tr>
                           <tr><td class="basicproprem"><span class="highlight">$469/yr.</span></td></tr>
                           <tr><td class="basicproprem">Up to 100</td></tr>
                           <tr><td class="basicproprem">Up to 5</td></tr>
                           
                    </table>
                  </td>


                  <td valign="top">


                    <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                            <tr><td class="tick">
                                <asp:Image ID="WTick8" runat="server" 
                                    ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                            <tr> <th class="basicproprem"><asp:Button ID="WebNonProfitProButton" runat="server" Text="Pro" CssClass="tabgridbutton" onclick="WebNonProfitProButton_Click" /></th></tr>
                            <tr> <td class="basicproprem"><span class="highlight">$854/yr.</span></td></tr>          
                            <tr> <td class="basicproprem">Up to 500</td></tr>
                            <tr> <td class="basicproprem">Up to 10</td></tr>                     

                    </table>
                    </td>


                  <td valign="top">
                    <table class="basicproprem" onmouseover=""  onmouseout="clearhelp">
                             <tr><td class="tick">
                                 <asp:Image ID="WTick9" runat="server" 
                                     ImageUrl="~/Billing/images/billing_images/tick.png" /> </td> </tr>
                             <tr><th class="basicproprem"><asp:Button ID="WebNonProfitPremiumButton" runat="server" Text="Premium" CssClass="tabgridbutton" onclick="WebNonProfitPremiumButton_Click" /></th></tr>
                             <tr><td class="basicproprem"><span class="highlight">$1430/yr.</span></td></tr>
                             <tr><td class="basicproprem">Unlimited</td></tr>
                             <tr><td class="basicproprem">Unlimited</td></tr>
                     </table> 
                </td>
                </tr>
                </table>          
                </telerik:RadPageView>

            </telerik:RadMultiPage>
        </telerik:RadPageView>
        </telerik:RadMultiPage>
    </td>
    </tr>
    </table>
    </td>
    </tr>
    </table>
    <!-- END TABGRID  TABLE -->    

  

    <!-- USER INFO -->
    <div id="USERINFO" runat="server">
    <table class="tabgridpage" > 
    <tr>
    <td>
    <table align="center">

   <tr><td colspan=5 class="table_headrow">User Information</td></tr>
   	<tr>        <td colspan=5>&nbsp;</td> 	</tr>
   <tr>
    	    <td class="subscriptionlabel_title">Company</td>
            <td><asp:textbox id="CompanyTextBox" runat="server" Width="220px" AutoCompleteType="Company"  CssClass="textbox" CausesValidation="True"> </asp:textbox>
            <br />
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ControlToValidate="CompanyTextBox"
                                        SetFocusOnError="True" ValidationGroup="formdata"></asp:RequiredFieldValidator>    
             </td>
            
            
            <td class="small_gap">&nbsp;</td>
            
            
            <td class="subscriptionlabel_title">Email</td>
             <td class="style2"><asp:textbox id="EmailTextBox" runat="server" Width="215px" CssClass="textbox" Font-Size="10pt" AutoCompleteType="Email" Font-Names="Arial" CausesValidation="True">@</asp:textbox>
             <br />
              <asp:RegularExpressionValidator  ID="RegularExpressionValidator14" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Not a Valid Email Address"
                                        CssClass="validatortext"
                                        ControlToValidate="EmailTextBox"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        SetFocusOnError="True" ValidationGroup="formdata"></asp:RegularExpressionValidator>
             
             <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        CssClass="validatortext" 
                                        ControlToValidate="EmailTextBox"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>  
             
             </td>
        </tr>
    
    <tr><td class="subscriptionlabel_title">First Name</td>
            <td><asp:TextBox ID="FirstNameTextBox" runat="server" CssClass="textbox" Width="221px" AutoCompleteType="FirstName" CausesValidation="True"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ControlToValidate="FirstNameTextBox"
                                        SetFocusOnError="True" ValidationGroup="formdata"></asp:RequiredFieldValidator>
            </td>
                   

            <td class="small_gap">&nbsp;</td>

            <td class="subscriptionlabel_title">Last Name<br /> </td>
            <td class="style2">
                <asp:textbox id="LastNameTextBox" runat="server" CssClass="textbox" Width="216px" AutoCompleteType="LastName" CausesValidation="True"></asp:textbox><br />
                    
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext" 
                                        ControlToValidate="LastNameTextBox"
                                        SetFocusOnError="True" ValidationGroup="formdata" ></asp:RequiredFieldValidator>
                    
                </td>
                </tr>

    
<tr>
		<td class="subscriptionlabel_title">Street Address<br /></td>
                <td><asp:TextBox ID="StreetTextBox" runat="server" Height="60px" 
                        CssClass="textbox" TextMode="MultiLine" Width="220px" 
                        AutoCompleteType="BusinessStreetAddress" 
			CausesValidation="True"></asp:TextBox><br /> 
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext" 
                                        ControlToValidate="StreetTextBox"
                                        SetFocusOnError="True" ValidationGroup="formdata">
		   </asp:RequiredFieldValidator>
            </td>
                
            <td class="small_gap">&nbsp;</td>
                
            <td class="subscriptionlabel_title">City  <br /></td>

            <td class="style2"><asp:textbox id="CityTextBox" runat="server" 
                                    	CssClass="textbox" Font-Size="10pt" AutoCompleteType="BusinessCity" 
                                    	Font-Names="Arial" Width="217px" 
					CausesValidation="True"></asp:textbox> <br />
                		<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext" 
                                        ControlToValidate="CityTextBox"
                                        SetFocusOnError="True" ValidationGroup="formdata">
				</asp:RequiredFieldValidator>        
            </td>
    </tr>




    <tr> <td class="subscriptionlabel_title">State<br /></td>
         <td style="height: 30px; width: 43px;" valign="top">
                    

             <telerik:RadComboBox ID="StateList" 
                                    runat="server" 
                                    AutoPostBack="True" 
                                     Font-Names="Arial" 
                                     Font-Size="10pt" 
                                     Width="155px" 
                                     MarkFirstMatch="True"  
                                     Skin="Vista">

                 <Items>
                    <telerik:RadComboBoxItem runat="server"  Text="Select State"  Value="" />
                    <telerik:RadComboBoxItem runat="server"  Text="Alabama"  Value="AL" />
                     <telerik:RadComboBoxItem runat="server"  Text="Alaska"  Value="AK"  />
                    <telerik:RadComboBoxItem runat="server"  Text="Arizona" Value="AZ" />
                    <telerik:RadComboBoxItem runat="server"  Text="Arkansas" Value="AR"/>
                    <telerik:RadComboBoxItem runat="server"  Text="California" Value="CA"/>
                    <telerik:RadComboBoxItem runat="server"  Text="Colorado" Value="CO"/>
                    <telerik:RadComboBoxItem runat="server"  Text="Connecticut" Value="CT"/>
                    <telerik:RadComboBoxItem runat="server"  Text="District of Columbia" Value="DC" />
                    <telerik:RadComboBoxItem runat="server" Value="DE" Text="Delaware" />
                    <telerik:RadComboBoxItem runat="server" Value="FL" Text="Florida" />
                    <telerik:RadComboBoxItem runat="server" Value="GA" Text="Georgia" />
                    <telerik:RadComboBoxItem runat="server" Value="HI" Text="Hawaii" />
                    <telerik:RadComboBoxItem runat="server" Value="ID" Text="Idaho" />
                    <telerik:RadComboBoxItem runat="server" Value="IL" Text="Illinois" />
                    <telerik:RadComboBoxItem runat="server" Value="IN" Text="Indiana" />
                    <telerik:RadComboBoxItem runat="server" Value="IA" Text="Iowa" />
                    <telerik:RadComboBoxItem runat="server" Value="KS" Text="Kansas" />
                    <telerik:RadComboBoxItem runat="server" Value="KY" Text="Kentucky" />
                    <telerik:RadComboBoxItem runat="server" Value="LA" Text="Louisiana" />
                    <telerik:RadComboBoxItem runat="server" Value="ME" Text="Maine" />
                    <telerik:RadComboBoxItem runat="server" Value="MD" Text="Maryland" />
                    <telerik:RadComboBoxItem runat="server" Value="MA" Text="Massachusetts" />
                    <telerik:RadComboBoxItem runat="server" Value="MI" Text="Michigan" />
                    <telerik:RadComboBoxItem runat="server" Value="MN" Text="Minnesota" />
                    <telerik:RadComboBoxItem runat="server" Value="MS" Text="Mississippi" />
                    <telerik:RadComboBoxItem runat="server" Value="MO" Text="Missouri" />
                    <telerik:RadComboBoxItem runat="server" Value="MT" Text="Montana" />
                    <telerik:RadComboBoxItem runat="server" Value="NE" Text="Nebraska" />
                    <telerik:RadComboBoxItem runat="server" Value="NV" Text="Nevada" />
                    <telerik:RadComboBoxItem runat="server" Value="NH" Text="New Hampshire" />
                    <telerik:RadComboBoxItem runat="server" Value="NJ" Text="New Jersey" />
                    <telerik:RadComboBoxItem runat="server" Value="NM" Text="New Mexico" />
                    <telerik:RadComboBoxItem runat="server" Value="NY" Text="New York" />
                    <telerik:RadComboBoxItem runat="server" Value="NC" Text="North Carolina" />
                    <telerik:RadComboBoxItem runat="server" Value="ND" Text="North Dakota" />
                    <telerik:RadComboBoxItem runat="server" Value="OH" Text="Ohio" />
                    <telerik:RadComboBoxItem runat="server" Value="OK" Text="Oklahoma" />
                    <telerik:RadComboBoxItem runat="server" Value="OR" Text="Oregon" />
                    <telerik:RadComboBoxItem runat="server" Value="PA" Text="Pennsylvania" />
                    <telerik:RadComboBoxItem runat="server" Value="RI" Text="Rhode Island" />
                    <telerik:RadComboBoxItem runat="server" Value="SC" Text="South Carolina" />
                    <telerik:RadComboBoxItem runat="server" Value="SD" Text="South Dakota" />
                    <telerik:RadComboBoxItem runat="server" Value="TN" Text="Tennessee" />
                    <telerik:RadComboBoxItem runat="server" Value="TX" Text="Texas" />
                    <telerik:RadComboBoxItem runat="server" Value="UT" Text="Utah" />
                    <telerik:RadComboBoxItem runat="server" Value="VT" Text="Vermont" />
                    <telerik:RadComboBoxItem runat="server" Value="VA" Text="Virginia" />
                    <telerik:RadComboBoxItem runat="server" Value="WA" Text="Washington" />
                    <telerik:RadComboBoxItem runat="server" Value="WV" Text="West Virginia" />
                    <telerik:RadComboBoxItem runat="server" Value="WI" Text="Wisconsin" />
                    <telerik:RadComboBoxItem runat="server" Value="WY" Text="Wyoming" />
                    <telerik:RadComboBoxItem runat="server" Value="AB" Text="Alberta" />
                    <telerik:RadComboBoxItem runat="server" Value="BC" Text="British Columbia" />
                    <telerik:RadComboBoxItem runat="server" Value="MB" Text="Manitoba" />
                    <telerik:RadComboBoxItem runat="server" Value="NB" Text="New Brunswick" />
                    <telerik:RadComboBoxItem runat="server" Value="NL" Text="Newfoundland and Labrador" />
                    <telerik:RadComboBoxItem runat="server" Value="NT" Text="Northwest Territories" />
                    <telerik:RadComboBoxItem runat="server" Value="NS" Text="Nova Scotia" />
                    <telerik:RadComboBoxItem runat="server" Value="NU" Text="Nunavut" />
                    <telerik:RadComboBoxItem runat="server" Value="ON" Text="Ontario" />
                    <telerik:RadComboBoxItem runat="server" Value="PE" Text="Prince Edward Island" />
                    <telerik:RadComboBoxItem runat="server" Value="QC" Text="Quebec" />
                    <telerik:RadComboBoxItem runat="server" Value="SK" Text="Saskatchewan" />
                    <telerik:RadComboBoxItem runat="server" Value="YT" Text="Yukon" />
                    <telerik:RadComboBoxItem runat="server" Value="OTHER" Text="Other" />
                 </Items>
                 </telerik:RadComboBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Select State"
                                        InitialValue="Select State"
                                        CssClass="validatortext" 
                                        ControlToValidate="StateList"
                                        SetFocusOnError="True" ValidationGroup="formdata"></asp:RequiredFieldValidator>    
                </td>
        

             <td class="small_gap">&nbsp;</td>
        

             <td class="subscriptionlabel_title">Postal Code<br /></td>
             <td class="style2"> <asp:textbox id="PostalCodeTextBox" runat="server" 
                                        CssClass="textbox" Font-Size="10pt" AutoCompleteType="BusinessZipCode" Font-Names="Arial"  
                                        CausesValidation="True" Width="65px"></asp:textbox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ControlToValidate="PostalCodeTextBox"
                                        SetFocusOnError="True" ValidationGroup="formdata"></asp:RequiredFieldValidator>
              </td>
          </tr>






	<tr>
        <td class="subscriptionlabel_title">Country<br /></td>
        <td class="style4"> 
                <asp:textbox id="CountryTextBox" runat="server"  
				Font-Size="10pt" 
				CssClass="textbox"  AutoCompleteType="BusinessCountryRegion" Font-Names="Arial"  
				CausesValidation="True"></asp:textbox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        CssClass="validatortext"
                                        InitialValue=""  
                                        ControlToValidate="CountryTextBox"
                                        SetFocusOnError="True" ValidationGroup="formdata">
		</asp:RequiredFieldValidator>
                
        </td>
        <td class="style9">&nbsp;</td>
        <td class="subscriptionlabel_title">Phone<br /> </td>

        <td class="style2"> 
            <asp:TextBox ID="PhoneTextbox" runat="server" Font-Size="10pt" 
                                        AutoCompleteType="BusinessPhone" Font-Names="Arial" 
                                        CssClass="textbox" CausesValidation="True" Width="108px"></asp:TextBox>

                        <br />

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Phone number cannot have alphabets"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ControlToValidate="PhoneTextbox"
                                        ValidationExpression="^[0-9]+$"
                                        SetFocusOnError="True" 
                                        ValidationGroup="formdata" 
                                        ViewStateMode="Enabled"></asp:RegularExpressionValidator>

                           <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        InitialValue=""
                                        ErrorMessage="Required"
                                        CssClass="validatortext"  
                                        ControlToValidate="PhoneTextbox"
                                        SetFocusOnError="True" ValidationGroup="formdata">
			</asp:RequiredFieldValidator>
                
        </td>
        </tr>

        <tr>        <td colspan=5>&nbsp;</td> 	</tr>







            <tr><td colspan=5 class="table_headrow"> Credit Card Details </td></tr>
            	<tr>        <td colspan=5>&nbsp;</td> 	</tr>
     <tr><td class="subscriptionlabel_title">Cardholder&#39;s First Name<br /></td>
         <td class="style4"> 
            <asp:textbox id="CCFirstNameTextbox" runat="server" Font-Size="10pt" 
			CssClass="textbox" Width="200px" AutoCompleteType="FirstName"  
			CausesValidation="True"></asp:textbox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext" 
                                        ControlToValidate="CCFirstNameTextbox"
                                        SetFocusOnError="True" ValidationGroup="formdata">
		</asp:RequiredFieldValidator>             
        </td>
        <td class="style9">&nbsp;</td>
        <td class="subscriptionlabel_title">Cardholder&#39;s Last Name </td>
        <td class="style2"> 
                    <asp:TextBox ID="CCLastNameTextBox" runat="server" 
			CssClass="textbox" Font-Size="10pt" Width="200px" 
			AutoCompleteType="LastName" CausesValidation="True"></asp:TextBox>
                    <br />
            
                    	<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ControlToValidate="CCLastNameTextbox"
                                        SetFocusOnError="True" ValidationGroup="formdata">
			</asp:RequiredFieldValidator>                   
                        
        </td>
        </tr>






        

     <tr>
        <td class="subscriptionlabel_title">Credit Card Number<br /></td>
        <td class="style4"> 
                   <asp:textbox id="CCNumberTextBox" runat="server" 
				CssClass="textbox" Font-Size="10pt" Width="200px" 
				CausesValidation="True"></asp:textbox>
                   <br />
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"
                                        Font-Size="Smaller"  
                                        ControlToValidate="CCNumberTextbox"
                                        SetFocusOnError="True" 
					ValidationGroup="formdata">
			</asp:RequiredFieldValidator>  
        
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Credit card has to be numeric"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ControlToValidate="CCNumberTextbox"
                                        ValidationExpression="^[0-9]+$"
                                        SetFocusOnError="True" 
					ValidationGroup="formdata">
			</asp:RegularExpressionValidator>
        
        </td>
        <td></td>
        <td class="subscriptionlabel_title">CVV Code<br /> </td>
        <td class="style2"> <asp:TextBox ID="CCCardCodeTextBox" 
				CssClass="textbox" runat="server" Font-Size="10pt" Width="46px" 
				CausesValidation="True"></asp:TextBox>
        			<br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ControlToValidate="CCCardCodeTextbox"
                                        SetFocusOnError="True" 
					ValidationGroup="formdata">
				</asp:RequiredFieldValidator>  
                            
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
					runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="CVV Code has to be numeric"
                                        InitialValue=""
                                        CssClass="validatortext" 
                                        ControlToValidate="CCCardCodeTextbox"
                                        ValidationExpression="^[0-9]+$"
                                        SetFocusOnError="True" 
					ValidationGroup="formdata"></asp:RegularExpressionValidator>

        
     </td>
     </tr>







           <tr>
        <td class="subscriptionlabel_title">Expiration <br /><small><small>(mm/yyyy)</small></small></td>


        <td class="style4"> 
		<asp:TextBox ID="CCExpirationTextBox" 
			runat="server" 
			CssClass="textbox" Font-Size="10pt" 
	                Width="84px" 
        	        CausesValidation="True"></asp:TextBox>

        <br />

        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Not (mm/yyyy) format!!"
                                        InitialValue=""
                                        CssClass="validatortext" 
                                        ValidationExpression="(0[1-9]|1[012])[- /.](19|20)\d\d"
					ControlToValidate="CCExpirationTextbox"
                                        SetFocusOnError="True" 
					ValidationGroup="formdata">
			</asp:RegularExpressionValidator>  

            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext" 
                                        ControlToValidate="CCExpirationTextbox"
                                        SetFocusOnError="True" 
       					ValidationGroup="formdata">
		    </asp:RequiredFieldValidator>

        </td>

                <td></td>

        <td class="subscriptionlabel_title">Cardholder's Postal Code<br /> </td>

        <td class="style2"> 
            <asp:TextBox ID="CCZipTextBox" 
                         runat="server"  
                         CssClass="textbox" 
                         Font-Size="10pt" 
                         Width="72px" 
                         CausesValidation="True" 
                         ValidationGroup="formdata"></asp:TextBox>

            <br />
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"
                                        ControlToValidate="CCZipTextbox"
                                        SetFocusOnError="True" 
					                    ValidationGroup="formdata">
		    </asp:RequiredFieldValidator>
        
            
            
        </td>
        </tr>
        <tr>        <td colspan=5>&nbsp;</td> 	</tr>

               <tr>

        <td colspan=5>
        <center>
         <asp:TextBox ID="CGResponseFlag" runat='server' style="display:none"></asp:TextBox>

 

            <table style="width: 363px">
            <tr>
            <td>
           <asp:Button ID="SubmitButton"  
                        runat="server" 
                        CssClass="bluebutton"  
                        OnClientClick = 'submitclientclicked();'
                        Text="Submit" 
                        CausesValidation="true" 
                        validationGroup="formdata" 
                        OnClick="SubmitButton_Click" />
            </td>
            

            <td>
            <input type="reset" id='resetButton' class="bluebutton"  value='Reset'  
                onclick='this.form.reset();return false;' />
            </td>
            </tr>
            </table>

            <br />
            

        </center>
        </td>

        </tr>


       </table>



    </td>
    </tr>
    <!-- END USER INFO -->

   </table>
    </div>
    <!-- END USER INFO -->    

    </td>
    </table>


    </div>


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

    <!--  STATUS DIVS -->
    <br /><br />
    <div runat="server" id="CG"></div>
    <!--  STATUS DIVS -->


   </form>

</body>
</html>
