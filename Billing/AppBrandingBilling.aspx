<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppBrandingBilling.aspx.cs" Inherits="AppBrandingBilling" %>
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




    <body class="body">
    <form runat="server" id="mainForm" method="post">

    <telerik:RadScriptManager ID="RadScriptManager" runat="server"> </telerik:RadScriptManager>

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






    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnResponseEnd="OnAjaxResponse" ></ClientEvents>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="AndroidBrandingCheckBox"> 
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="iosBrandingCheckBox">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="SubmitButton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CGResponseFlag" />
                    <telerik:AjaxUpdatedControl ControlID="ButtonReset" />
                    <telerik:AjaxUpdatedControl ControlID="RadNotification1" />
                    <telerik:AjaxUpdatedControl ControlID="SubmitButton" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        </telerik:RadAjaxManager>

     

     <!-- OUTERTABLE -->    
     <table width=100%>
     <tr><td valign="top">

      <asp:Button ID="BackButton" runat="server" onclick="BackButton_Click"   class="bluebutton"  Text="Back" Height="47px" Width="73px" />      
 
     </td>
     
     
     
     
     <td>  
     <table id="Table1"  class="newbrandingtable" runat="server" >
    
     <!-- BRANDING TABLE -->    
    <tr><td colspan=5>
    <center>
    <h3> App Store Preparation</h3>
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
                 </telerik:RadComboBox>
    </center>
    
    </td>
    </tr>

            <tr>
            <td colspan=5>
             <div id="Android" runat="server">
                    <table class="brandingitem" >
                    <tr>
                    <td>
                    
                    <img class="branding_icon" height="80px" src="images/billing_images/android_logo.png" />
                    <br />
                    <p class="branding_desc">One-time $99.00 fee to prepare the App for the Google Play Store.</p>    
                    <br />
                            
                     </td>
                    </tr>
                    </table>
             </div>      
             </td>
             </tr>
             

             <!-- td class="small_gap">&nbsp;</td -->
             
             
             <tr>
             <td colspan=5>
                    <div id="ios" runat="server">
                    <table class="brandingitem">
                        <tr>
                        <td>
                        <img class="branding_icon" height="80px" src="images/billing_images/ios_logo.png" />
                        <br />
                        <p class="branding_desc">One-time $99.00 fee to prepare the App for the Apple Store.</p>
                        <br />
                       
                        </td>
                        </tr>
                    </table>
                    </div>
             </td>
             </tr>
    </table>
     <!-- END BRANDING TABLE -->    
     <br />
   
     <!-- USER INFO -->
     <div id="USERINFO" runat="server">
    <table class="newbrandingtable" >  
    <tr>
    <td>
    <!-- INNER TABLE -->
    <table align="center">
    
    <tr><td colspan=5 class="table_headrow"> User Information</td></tr>
    <tr><td  colspan =5>&nbsp;</td>   </tr>
    <tr>
    	    <td class="subscriptionlabel_title">Company</td>
            <td><asp:textbox id="CompanyTextBox"  CssClass="textbox"  runat="server" 
                    Width="220px" AutoCompleteType="Company" CausesValidation="True" ></asp:textbox>
            <br />
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"
                                        ControlToValidate="CompanyTextBox"
                                        SetFocusOnError="True" ValidationGroup="formdata">
                  </asp:RequiredFieldValidator>    
             </td>
            
            
            <td class="small_gap">&nbsp;</td>
            
            
            <td class="subscriptionlabel_title">Email</td>



             <td class="style2">
             <asp:TextBox id="EmailTextBox" runat="server" Width="215px" Font-Size="10pt"  EmptyMessage="email" AutoCompleteType="Email" CssClass="textbox" Font-Names="Arial" CausesValidation="True">@</asp:TextBox>

             <br />

             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue="@"
                                        CssClass="validatortext"  
                                        ControlToValidate="EmailTextBox"
                                        SetFocusOnError="True" ValidationGroup="formdata"></asp:RequiredFieldValidator>
                         
              <asp:RegularExpressionValidator  ID="RegularExpressionValidator14" runat="server"  
                                        Display="Dynamic"
                                        Enabled="true"
                                        EnableClientScript="true"
                                        ErrorMessage="Not a Valid Email Address"
                                        CssClass="validatortext"
                                        ControlToValidate="EmailTextBox"
                                        InitialValue="@"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        SetFocusOnError="True" ValidationGroup="formdata"></asp:RegularExpressionValidator>
             
             </td>
        </tr>
    <tr><td class="subscriptionlabel_title">First Name</td>


            <td>
            
            <asp:TextBox ID="FirstNameTextBox" runat="server" CssClass="textbox"  Width="221px" AutoCompleteType="FirstName" CausesValidation="True"></asp:TextBox><br />
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
                <asp:textbox id="LastNameTextBox" runat="server" CssClass="textbox"  Width="216px" AutoCompleteType="LastName" CausesValidation="True"></asp:textbox><br />
                    
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
                <td><asp:TextBox ID="StreetTextBox" runat="server" Height="60px" CssClass="textbox" 
                        TextMode="MultiLine" Width="220px" 
                        AutoCompleteType="BusinessStreetAddress" 
			CausesValidation="True"></asp:TextBox><br /> 
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  
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

            <td class="style2"><asp:textbox id="CityTextBox" 
                                        runat="server" 
                                        CssClass="textbox" 
                                    	Font-Size="10pt" AutoCompleteType="BusinessCity" 
                                    	Font-Names="Arial" Width="217px" 
					                    CausesValidation="True"></asp:textbox> <br />
                		<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"  
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
                                     Skin="Web20">

                 <Items>
                     <telerik:RadComboBoxItem runat="server"  Text=""  Value="" />
                     <telerik:RadComboBoxItem runat="server"  Text="Alabama"  Value="AL" />
                     <telerik:RadComboBoxItem runat="server"  Text="Alaska"  Value="AL"  />
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
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"  
                                        ControlToValidate="StateList"
                                        SetFocusOnError="True" ValidationGroup="formdata">
				</asp:RequiredFieldValidator>        


             </td>
        

             <td class="small_gap">&nbsp;</td>
        

             <td class="subscriptionlabel_title">Postal Code<br /></td>
             <td class="style2"> <asp:textbox id="PostalCodeTextBox" CssClass="textbox" runat="server" 
                                        Font-Size="10pt" AutoCompleteType="BusinessZipCode" Font-Names="Arial"  
                                        CausesValidation="True" Width="65px"></asp:textbox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"
                                        ControlToValidate="PostalCodeTextBox"
                                        SetFocusOnError="True" ValidationGroup="formdata">
                                        </asp:RequiredFieldValidator>
              </td>
          </tr>
	<tr>
        <td class="subscriptionlabel_title">Country<br /></td>
        <td class="style4"> 
                <asp:textbox id="CountryTextBox" runat="server"  CssClass="textbox" 
				Font-Size="10pt" 
				AutoCompleteType="BusinessCountryRegion" Font-Names="Arial"  
				CausesValidation="True"></asp:textbox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator777" runat="server"  
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
            <asp:TextBox ID="PhoneTextbox" runat="server" Font-Size="10pt" CssClass="textbox" 
                                        AutoCompleteType="BusinessPhone" Font-Names="Arial" 
                                        CausesValidation="True" Width="108px"></asp:TextBox>

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
        <tr><td  colspan =5>&nbsp;</td>   </tr>
    <tr><td colspan=5 class="table_headrow"> Credit Card Details </td></tr>
    <tr><td  colspan =5>&nbsp;</td>   </tr>
    <tr><td class="subscriptionlabel_title">Cardholder&#39;s First Name<br /></td>
         <td class="style4"> 
            <asp:textbox id="CCFirstNameTextbox" runat="server" Font-Size="10pt" CssClass="textbox" 
			Width="200px" AutoCompleteType="FirstName"  
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
                    <asp:TextBox ID="CCLastNameTextBox" runat="server" CssClass="textbox" 
			Font-Size="10pt" Width="200px" 
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
                   <asp:textbox id="CCNumberTextBox" runat="server" CssClass="textbox" 
				Font-Size="10pt" Width="200px" 
				CausesValidation="True"></asp:textbox>
                   <br />
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Required"
                                        InitialValue=""
                                        CssClass="validatortext"  
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
				runat="server" Font-Size="10pt" Width="46px" CssClass="textbox" 
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
			Font-Size="10pt" 
	                Width="84px" 
                    CssClass="textbox" 
        	        CausesValidation="True"></asp:TextBox>

        <br />

        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"  
                                        Display="Dynamic"
                                        EnableClientScript="true"
                                        Enabled="true"
                                        ErrorMessage="Not in (mm/yyyy) format !!"
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
                         Font-Size="10pt" 
                         Width="72px" 
                         CssClass="textbox" 
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

        <tr><td  colspan =5>&nbsp;</td>   </tr>
    <tr> 
        <td ><asp:TextBox ID="CGResponseFlag" runat='server' style="display:none"></asp:TextBox></td>
        <td valign="middle"> 
        <center>

        
        <asp:Button ID="SubmitButton" runat="server" CssClass="bluebutton"  OnClientClick = 'submitclientclicked();'
                    Text="Submit" 
                    CausesValidation="true" 
                    validationGroup="formdata" 
                    OnClick="SubmitButton_Click" />

            
         </center>
         </td>
          <td></td> 
          <td valign="middle">
            <center>
            <asp:Button ID="ButtonReset" runat="server" 
                       CssClass="bluebutton" 
                       Text="Reset" 
                       CausesValidation="true" 
                       ValidationGroup="formdata" 
                       OnClientClick="theForm.reset();return false;" />
            </center>
           </td>
           
        </tr>
    
    </table>
    <!-- INNER TABLE -->
    </td>
    </tr>   

    
     
    </table>
    </div>
     <!-- END USER INFO -->
    
    
    
    </td></tr>   
    </table>
    <!-- END OUTERTABLE -->    






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
