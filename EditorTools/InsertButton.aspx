<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertButton.aspx.cs" Inherits="InsertButton" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert a Button with an Action</title>
    <style type="text/css">
        body
        {
        	font-family:Verdana;
        	font-size:11px;
        	color:#000000;
        }
        #nextPage
        {
            width: 218px;
        }
        #nextPage3
        {
            width: 269px;
        }
        #label
        {
            width: 289px;
        }
        .style4
        {
            width: 77px;
        }
        .style5
        {
            width: 315px;
        }
        .style23
        {
            width: 83px;
        }
          .colorPickerWrapper
        {
            float: left;
            border: solid 1px #9c9c9c;
            padding: 5px 5px 5px 17px;
            width: 393px;
            background:#F1F1F1 url('../images/editor_images/client-side-api.gif') repeat-x 0 0;
        }
        .style24
        {
            width: 123px;
        }
        .style26
        {
            width: 144px;
        }
        #buttonName
        {
            width: 219px;
        }
        .style33
        {
            width: 93px;
        }
        #compute
        {
            width: 464px;
        }
        .style36
        {
            width: 528px;
        }
        .style38
        {
            width: 195px;
        }
        .style42
        {
            width: 154px;
        }
        .style53
        {
            width: 193px;
        }
        #foursquare_account_field
        {
            width: 177px;
        }
        .style55
        {
            width: 177px;
        }
        .style59
        {
            width: 181px;
        }
        .style61
        {
            width: 199px;
        }
        .style63
        {
            width: 180px;
        }
        .style64
        {
            width: 158px;
        }
        .style65
        {
            width: 194px;
        }
        .style66
        {
            width: 160px;
        }
        .style69
        {
            width: 242px;
        }
        #Text1
        {
            width: 230px;
        }
        #Text2
        {
            width: 230px;
        }
        .style75
        {
            width: 200px;
        }
        .style84
        {
            width: 251px;
        }
        .style82
        {
            width: 147px;
        }
        .style86
        {
            width: 210px;
        }
        .style90
        {
            width: 250px;
        }
        .style77
        {
            width: 46px;
        }
        .style88
        {
            width: 67px;
        }
        #image_field
        {
            width: 186px;
        }
        #compression
        {
            width: 59px;
        }
        #icon_field
        {
            width: 186px;
        }
        .style91
        {
            width: 394px;
        }
        .style100
        {
            width: 302px;
        }
        #shopping_cart_url0
        {
            width: 480px;
        }
        #label0
        {
            width: 289px;
        }
        #condition
        {
            width: 251px;
        }
        #next_page_condition
        {
            width: 250px;
        }
        .body
        {}
        #next_page_condition0
        {
            width: 250px;
        }
        .style104
        {
            width: 215px;
        }
        #mcommerce_password_field
        {
            margin-left: 0px;
        }
          .style103
        {
            width: 150px;
        }
          #mcommerce_password_field0
        {
            margin-left: 0px;
        }
        .style105
        {
            width: 143px;
        }
        </style>
         <script type="text/javascript" src="../jquery/js/jquery-1.5.1.min.js"></script>        
         <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
         <script  language="javascript" type="text/javascript" src="../scripts/color_picker_1.0.js"></script>
         <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
         <script  language="javascript" type="text/javascript" src="js/insertButton_1.40.min.js"></script>
</head>
<body>
    <form id="buttonform" runat="server">
               <telerik:RadScriptManager ID="RadScriptManagerButton" runat="server">
                </telerik:RadScriptManager>
          <telerik:RadAjaxManager ID="RadAjaxManagerButton" runat="server">
        <AjaxSettings>            

           <telerik:AjaxSetting AjaxControlID="ResponsePages">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="ResponsePages" > </telerik:AjaxUpdatedControl>                    
                  </UpdatedControls>
            </telerik:AjaxSetting>
            
          <telerik:AjaxSetting AjaxControlID="GoToPages">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="GoToPages" > </telerik:AjaxUpdatedControl>                    
                  </UpdatedControls>
            </telerik:AjaxSetting>
            
                </AjaxSettings>
     </telerik:RadAjaxManager>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Telerik">
            <Windows>
               
               <telerik:RadWindow 
                    id="PageHelp" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                    title="How the Compute Statement is Used" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>

             <telerik:RadWindow 
                    id="buttonColor" 
                    runat="server"
                    showcontentduringload="true"
                    width="300px"
                    height="800px"
                    title="Choose Button Color" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>
            </Windows>
            </telerik:RadWindowManager>
    
    <div style="height:36px; width: 792px;">
    <table style="width: 652px"><tr><td class="style26"> Internal Button Name:</td><td><input type="text" id="buttonName" /></td>
        <td align="center" class="style24">
        <asp:Image ID="ButtonImage" style="width:72px;height:15px" 
                ImageUrl="" runat="server" />
        </td><td>
         <input type="button" onclick="showSetButtonColorClient();" 
                value="Choose Button Color" style="width: 154px"/>
            </td></tr></table>
      
    </div>
       <div style="height:32px; width: 794px;">
       <table style="width: 552px"><tr><td class="style23">
        Button Label:</td><td><input type="text" id="label" /></td><td>
            <asp:HiddenField ID="AccountType" runat="server" /></td></table>        
    </div>
      <div style="height: 37px; width: 784px;">
    <table><tr> <td>
               <telerik:RadComboBox ID="FontFamily" runat="server" Width="140px" OnClientSelectedIndexChanged="OnFontFamilyClientSelectedIndexChanged"   OnClientLoad="OnFontFamilyClientLoad">
               <Items>
                       <telerik:RadComboBoxItem runat="server" Text="Antiqua" Value="Antiqua" />
                     <telerik:RadComboBoxItem runat="server" Text="Arial" Value="Arial" />
                     <telerik:RadComboBoxItem runat="server" Text="Avqest" Value="Avqest" />
                     <telerik:RadComboBoxItem runat="server" Text="Blackletter" Value="Blackletter" />
                     <telerik:RadComboBoxItem runat="server" Text="Calibri" Value="Calibri" />
                     <telerik:RadComboBoxItem runat="server" Text="Comic Sans" Value="Comic Sans" />
                     <telerik:RadComboBoxItem runat="server" Text="Courier" Value="Courier" />
                     <telerik:RadComboBoxItem runat="server" Text="Decorative" Value="Decorative" />
                     <telerik:RadComboBoxItem runat="server" Text="Fraktur" Value="Fraktur" />
                     <telerik:RadComboBoxItem runat="server" Text="Frosty" Value="Frosty" />
                     <telerik:RadComboBoxItem runat="server" Text="Garamond" Value="Garamond" />
                     <telerik:RadComboBoxItem runat="server" Text="Georgia" Value="Georgia" />
                     <telerik:RadComboBoxItem runat="server" Text="Helvetica" Value="Helvetica" />
                     <telerik:RadComboBoxItem runat="server" Text="Impact" Value="Impact" />
                     <telerik:RadComboBoxItem runat="server" Text="Minion" Value="Minion" />
                     <telerik:RadComboBoxItem runat="server" Text="Modern" Value="Modern" />
                     <telerik:RadComboBoxItem runat="server" Text="Monospace" Value="Monospace" />
                     <telerik:RadComboBoxItem runat="server" Text="Palatino" Value="Palatino" />
                     <telerik:RadComboBoxItem runat="server" Text="Roman" Value="Roman" />
                     <telerik:RadComboBoxItem runat="server" Text="Script" Value="Script" />
                     <telerik:RadComboBoxItem runat="server" Text="Swiss" Value="Swiss" />
                     <telerik:RadComboBoxItem runat="server" Text="Tahoma" Value="Tahoma" />
                     <telerik:RadComboBoxItem runat="server" Text="Times New Roman" Value="Times New Roman" />
                     <telerik:RadComboBoxItem runat="server" Text="Verdana" Value="Verdana"  Selected="true"/>
                       </Items>
               </telerik:RadComboBox>
               </td>
               <td><telerik:RadComboBox ID="FontSize" runat="server" Width="80px" OnClientSelectedIndexChanged="OnFontSizeClientSelectedIndexChanged" OnClientLoad="OnFontSizeClientLoad">
                   <Items>
                       <telerik:RadComboBoxItem runat="server" Text="6px" Value="6px" />
                       <telerik:RadComboBoxItem runat="server" Text="8px" Value="8px" />
                       <telerik:RadComboBoxItem runat="server" Text="10px" Value="10px" />
                       <telerik:RadComboBoxItem runat="server" Text="11px" Value="11px" />
                       <telerik:RadComboBoxItem runat="server" Text="12px" Value="12px" />
                       <telerik:RadComboBoxItem runat="server" Text="14px" Value="14px" />
                       <telerik:RadComboBoxItem runat="server" Text="16px" Value="16px" Selected="true"/>
                       <telerik:RadComboBoxItem runat="server" Text="18px" Value="18px" />
                       <telerik:RadComboBoxItem runat="server" Text="20px" Value="20px" />
                       <telerik:RadComboBoxItem runat="server" Text="22px" Value="22px" />
                       <telerik:RadComboBoxItem runat="server" Text="24px" Value="24px" />
                       <telerik:RadComboBoxItem runat="server" Text="26px" Value="26px" />
                       <telerik:RadComboBoxItem runat="server" Text="28px" Value="28px" />
                       <telerik:RadComboBoxItem runat="server" Text="30px" Value="30px" />
                       <telerik:RadComboBoxItem runat="server" Text="32px" Value="32px" />
                       <telerik:RadComboBoxItem runat="server" Text="34px" Value="34px" />
                       <telerik:RadComboBoxItem runat="server" Text="36px" Value="36px" />
                       <telerik:RadComboBoxItem runat="server" Text="38px" Value="38px" />
                       <telerik:RadComboBoxItem runat="server" Text="40px" Value="40px" />
                       <telerik:RadComboBoxItem runat="server" Text="42px" Value="42px" />
                       <telerik:RadComboBoxItem runat="server" Text="44px" Value="44px" />
                       <telerik:RadComboBoxItem runat="server" Text="46px" Value="46px" />
                       <telerik:RadComboBoxItem runat="server" Text="50px" Value="50px" />
                       <telerik:RadComboBoxItem runat="server" Text="54px" Value="54px" />
                       <telerik:RadComboBoxItem runat="server" Text="60px" Value="60px" />
                       <telerik:RadComboBoxItem runat="server" Text="64px" Value="64px" />
                       <telerik:RadComboBoxItem runat="server" Text="70px" Value="70px" />
                       <telerik:RadComboBoxItem runat="server" Text="74px" Value="74px" />
                       <telerik:RadComboBoxItem runat="server" Text="80px" Value="80px" />
                       <telerik:RadComboBoxItem runat="server" Text="90px" Value="90px" />
                       <telerik:RadComboBoxItem runat="server" Text="100px" Value="100px" />
                   </Items>
               </telerik:RadComboBox>
               </td><td valign="top">
                   &nbsp;</td>
                   
                           <td valign="top">
                               &nbsp;</td></tr></table>
    </div>
    <div style="height:43px; width: 790px;">
           <table><tr><td>
                    <div class="colorPickerWrapper">
        <div style="float: left;">
            <div style="line-height: 21px; float: left;">
                Select Font Color:&nbsp;</div>
            <telerik:RadColorPicker ShowIcon="true" ID="FontColor" runat="server" PaletteModes="All" SelectedColor="White"
                OnClientColorChange="HandleColorChange" OnClientLoad="OnColorClientLoad" Style="float: left;"   />
        </div>
        <div style="float: right">
            <div style="line-height: 21px; float: left;">
                Selected Font Color Value:&nbsp;</div>
            <input type="text" value="#ffffff" id="ColorPickerSelectedColor" style="width: 70px;" readonly="readonly" />
        </div>
    </div>


               </td></tr></table>
    </div>
      <div style="height:13px; width: 792px;"> </div>
   
    <div style="height:20px; font-weight:bold; vertical-align:bottom; width: 786px;">
        When a user taps the button:
     </div>
    <div>
        <telerik:RadComboBox ID="actions" runat="server" 
            OnClientSelectedIndexChanged="changeAction" Width="600px" >       
        </telerik:RadComboBox>
    </div>
    <div style="height:10px; width: 788px;"></div>
    <div>
        <telerik:RadMultiPage ID="actionsMultiPage" runat="server"  SelectedIndex="0" 
            Width="790px">
            <telerik:RadPageView ID="no_view" runat="server">
               <div></div>
            </telerik:RadPageView>
            
            <telerik:RadPageView ID="next_page_view" runat="server">
                  <div style="height:33px;">
           <table style="width:595px"><tr>
            <td class="style33">
             Go to Page:
           </td>
           <td>
           <telerik:RadComboBox ID="GoToPages" runat="server" AllowCustomText="true" OnClientLoad="OnClientGoToPagesLoadHandler"
                   CssClass="body" EmptyMessage="Choose Field or Enter Page" 
                   OnClientSelectedIndexChanged="GoToPages_SelectedIndexChanged" 
                   OnClientTextChange="GoToPages_SelectedIndexChanged">
                       
    
    </telerik:RadComboBox>
               
           </td>
           <td>
               &nbsp;</td>
          </tr></table>

    </div>
            </telerik:RadPageView>
             <telerik:RadPageView ID="if_then_next_page_view" runat="server">
                  <div style="height:33px;">
           <table style="width:778px"><tr>
            <td width="20px">
                If</td>
               <td width="230px">
                   <input type="text" id="next_page_condition" onchange="next_page_condition_Changed(this,this.value);" />
               </td>
               <td align="right" width="108">
                   Then Go To Page:</td>
           <td width="160">
           <telerik:RadComboBox ID="page_if_true" runat="server" AllowCustomText="true" OnClientLoad="OnClientpage_if_trueLoadHandler"
                   CssClass="body" EmptyMessage="Choose Field or Enter Page" 
                   OnClientSelectedIndexChanged="page_if_true_SelectedIndexChanged" 
                   OnClientTextChange="page_if_true_SelectedIndexChanged">
                       
    
    </telerik:RadComboBox>
               
           </td>
           <td align="right" width="30px">
               Else:
           </td>
           <td width="160px">
               <telerik:RadComboBox ID="page_if_false" runat="server" AllowCustomText="true" OnClientLoad="OnClientpage_if_falseLoadHandler"
                   CssClass="body" EmptyMessage="Choose Field or Enter Page" 
                   OnClientSelectedIndexChanged="page_if_false_SelectedIndexChanged" 
                   OnClientTextChange="page_if_false_SelectedIndexChanged" 
                   Width="160px">
               </telerik:RadComboBox>
               </td>
          </tr></table>

    </div>
            </telerik:RadPageView>
             <telerik:RadPageView ID="previous_page_view" runat="server">
               <div></div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="post_view" runat="server">
            <div>
            <table><tr><td width="600px"></td><td>
                <img id="NetworkInterfaceHelp" 
                    src="../images/help.gif"                     
                    alt="How does the device access web data sources?" 
                    onclick="PopUp('../Help/Design/NetworkInterfaceHelp.htm', 'height=380, width=700, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, titlebar=no,scrollbars=no, resizable=no');
                return false;"/></td></tr></table>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="call_view" runat="server">
                 <div style="height:32px;">
    <table style="width: 774px"><tr><td class="style53">
        Call Phone in field:</td>
        <td class="style55"><input type="text" id="device_field" size="25"/></td>
        <td class="style10">
            (name of the field that will contain the actual phone number)</td>
        </tr></table>
    </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="share_view" runat="server">
            <div style="height:33px;">
                All fields are optional. Set these entries with the names of the fields that 
                will contain the actual values. For multiple recipients,
                <br />
                separate the values with semicolons.</div>
                <div style="height:32px;"><table style="width: 781px"><tr>
    <td class="style63" valign="top">
        Subject in field:</td>
        <td class="style12" width="200" valign="top">
            <input type="text" id="share_subject_field" size="30"/>
                    </td>
                    <td class="style12">
                        &nbsp;</td>
                    <td class="style12">
                        &nbsp;</td>
                    </tr></table></div>
             <div style="height:33px;">
     <table style="width: 770px"><tr><td class="style63">
         Message to share in field:</td><td class="style38"> 
             <input type="text" id="message_field" size="30"/></td>
         <td class="style64" align="right">
             Hyperlink to photo in field:</td>
         <td>
             <input type="text" id="media_link_field" size="30"/>
         </td>
         </tr></table></div>                
                 <div style="height:33px;">
     <table style="width: 768px"><tr><td class="style63" >
         SMS phone(s) in field:</td><td class="style65" > 
             <input type="text" id="sms_phone_field" size="30"/></td>
         <td  align="right" class="style66">
             Email(s) To, in field:</td>
         <td >
             <input type="text" id="to_email_field" 
                size="30"/>
         </td>
         </tr></table></div>
                
                </telerik:RadPageView>
                <telerik:RadPageView ID="email_view" runat="server">
                <div style="height:32px;">
    <table style="width: 781px"><tr><td class="style63" valign="top">
        Email(s) To, in field:</td>
        <td class="style12" valign="top" width="200">
            <input type="text" id="to_email_field2" size="30"/></td>
        <td class="style12">
            delimit emails with semicolon in the field containing the values</td>
        </tr></table>
    </div>
                  <div style="height:33px;">
     <table style="width: 770px"><tr><td class="style59">
         Subject in field:</td><td class="style38"> 
             <input type="text" id="subject_field" size="30"/></td>
         <td class="style42" align="right">
             &nbsp;</td>
         <td>
             &nbsp;</td>
         </tr></table></div>  
          <div style="height:33px;">
     <table style="width: 770px"><tr><td class="style59">
         Message to share in field:</td><td class="style38"> 
             <input type="text" id="message_field2" size="30"/></td>
         <td class="style42" align="right">
             Hyperlink to photo in field:</td>
         <td>
             <input type="text" id="media_link_field2" size="30"/>
         </td>
         </tr></table></div>        
                 
            </telerik:RadPageView>
            <telerik:RadPageView ID="sms_view" runat="server">
            <div style="height:33px;">
    <table style="width: 776px"><tr><td class="style63" align="left" valign="top">
        SMS phone(s) in field:</td>
        <td class="style61" valign="top">
            <input type="text" id="sms_phone_field2" size="30"/></td>
        <td>
            delimit numbers with semicolon in the field containing the values</td>
        </tr></table>
    </div>
              <div style="height:33px;">
     <table style="width: 770px"><tr><td class="style59">
         Message to share in field:</td><td class="style38"> 
             <input type="text" id="message_field3" size="30"/></td>
         <td class="style42" align="right">
             Hyperlink to photo in field:</td>
         <td>
             <input type="text" id="media_link_field3" size="30"/>
         </td>
         </tr></table></div>                 
            </telerik:RadPageView>
             <telerik:RadPageView ID="take_photo_view" runat="server">
               <div style="height: 32px">
                   <table style="width: 768px">
                       <tr>
                           <td align="left" class="style84">
                               Photo image URL is saved in<strong> hidden field</strong>:
                           </td>
                           <td class="style82">
                               <input 
                type="text" id="image_field" size="20"/>
                           </td>
                           <td align="right" class="style86">
                               Compression Ratio (0.001 to 1.0)</td>
                           <td>
                               <input 
                type="text" id="compression" size="20"/>
                           </td>
                       </tr>
                   </table>
                 </div>
       
        <div style="height: 63px"><table style="width: 778px">
        <tr><td class="style90" >Automated icon is saved in <strong>hidden field</strong>:</td>
            <td colspan="3" class="style3"><input 
                type="text" id="icon_field" size="20"/></td>
            <td class="style3">
                (Icon is generated only if icon field name is filled)
            </td>
            </tr>
        <tr><td align="right" class="style90" >Icon width:&nbsp;&nbsp;&nbsp;&nbsp; </td><td class="style77"><input 
                type="text" id="icon_width" size="5" value="60"/></td><td align="right" 
                >&nbsp;Icon height:&nbsp;&nbsp; </td><td class="style88"><input 
                type="text" id="icon_height" size="5" value="80"/></td>
            <td>
                &nbsp;</td>
            </tr>
        </table>
                </div>
                
            </telerik:RadPageView>    
            <telerik:RadPageView ID="capture_signature_view" runat="server">
                
                <div style="height:33px;">
                    <table style="width: 770px">
                        <tr>
                            <td align="left" class="style104">
                                Store the signature image in field:</td>
                            <td>
                                <input type="text" id="signature_image_field" size="30"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="login_to_mcommerce_view" runat="server">
              <div style="height:33px;">
     <table style="width: 770px"><tr><td class="style103">
         Username field:</td><td class="style38"> 
             <input type="text" id="mcommerce_username_field" size="30" class="mcommerce"/></td>
         <td class="style26" align="right">
             Password field:</td>
         <td>
             <input type="text" id="mcommerce_password_field" size="30" class="mcommerce"/>
         </td>
         </tr>
         </table></div>   
         <div style="height:33px;">
     <table style="width: 770px"><tr><td class="style103">
         Activation code field:</td><td class="style38"> 
             <input type="text" id="mcommerce_activation_code_field" size="30" class="mcommerce"/></td>
         <td class="style26" align="right">
             Login alert field:</td>
         <td>
             <input type="text" id="mcommerce_login_alert_field" size="30" class="mcommerce"/>
         </td>
         </tr>
         </table></div>               
                <div style="height:33px;">
                    <table style="width: 770px">
                        <tr>
                            <td class="style103">
                                Login page name:</td>
                            <td class="style38">
                                <input type="text" id="mcommerce_login_page_name" size="30" class="mcommerce"/>
                            </td>
                            <td align="right" class="style105">
                                Login info button name:</td>
                            <td>
                                <input type="text" id="mcommerce_login_info_button_name" size="30" class="mcommerce"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </telerik:RadPageView>
                  <telerik:RadPageView ID="init_card_swiper_view" runat="server">                 
                  <div id="addSwiperPropertyDiv">
                     <table style="width: 400px; height: 30px;">
                         <tr>
                             <td align="left" class="style23" valign="top">
                                 <select style="width: 173px; font-size:12px" 
                                     onchange="addPropertySelected(this,'addSwiperPropertyDiv');">
                                 <option>Add a property -&gt;</option>
                                 <option value="NAME">Amount field name</option>
                                 <option value="NAME">Credit card field name</option>
                                 <option value="NAME">Expiration field name</option>                                
                                 <option value="NAME">Security code field name</option>
                                 <option value="NAME">Full name field name</option>
                                 <option value="NAME">Organization field name</option>
                                 <option value="NAME">Occupation field name</option>
                                 <option value="NAME">Transaction label field name</option>
                                 <option value="NAME">Email field name</option>
                                 <option value="NAME">Street address1 field name</option>
                                 <option value="NAME">Street address2 field name</option>
                                 <option value="NAME">City field name</option>
                                 <option value="NAME">State or province field name</option>
                                 <option value="NAME">Zipcode field name</option>
                                 <option value="NAME">Phone field name</option>
                                 <option value="NAME">Order field name</option>
                                 <option value="NAME">Notes field name</option>
                                 <option value="NAME">Confirmation page name</option>
                                 <option value="NAME">Confirmation message field name</option> 
                                 <option value="NAME">Transaction ID field name</option>
                                 <option value="NAME">Transaction log button name</option>   
                                 <option value="NAME">Signature field name</option>  
                                 <option value="NAME">User agree1 field name</option>  
                                 <option value="NAME">User agree2 field name</option> 
                                 <option value="NAME">User agree3 field name</option>                              
                                 </select>
                                 </td>
                             <td class="style100" valign="top">
                                 &nbsp;</td>
                         </tr>
                     </table>
                 </div>
                 
            </telerik:RadPageView>
             <telerik:RadPageView ID="manual_card_charge_view" runat="server">
               <div>Charge the credit card using the fields in the init card swiper action. </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="void_charge_view" runat="server">
              <div id="addVoidPropertyDiv">
                    <table style="width: 400px; height: 30px;">
                         <tr>
                             <td align="left" class="style23" valign="top">
                                 <select style="width: 173px; font-size:12px" 
                                     onchange="addPropertySelected(this,'addVoidPropertyDiv');">
                                 <option>Add a property -&gt;</option>
                                 <option value="NAME">Transaction ID field name</option>
                                 <option value="NAME">Confirmation page name</option>
                                 <option value="NAME">Confirmation message field name</option>  
                                 <option value="NAME">Transaction log button name</option>                                 
                                 </select>
                                 </td>
                             <td class="style100" valign="top">
                                 &nbsp;</td>
                         </tr>
                     </table>
    </div>              
            </telerik:RadPageView>
            <!-- <telerik:RadPageView ID="capture_doc_view" runat="server">
               <div> <table style="width: 776px"><tr><td  align="left" valign="top" 
                       class="style91">
                   Latest document case data are saved as XML in field:</td>
        <td class="style75" valign="top">
            <input type="text" id="doc_case_field" size="30"/></td>
        <td class="style69" valign="top">
            (This is usually a hidden field)</td>
        </tr></table></div>
            </telerik:RadPageView>-->
             <telerik:RadPageView ID="capture_process_document_view" runat="server">
                <div id="addCaptureProcessDocumentPropertyDiv">
                   <table style="width: 400px; height: 30px;">
                       <tr>
                           <td align="left" class="style23" valign="top">
                               <select name="D1" onchange="addPropertySelected(this,'addCaptureProcessDocumentPropertyDiv');" 
                                   style="width: 173px; font-size:12px">
                                   <option>Add a property -&gt;</option>
                                   <option value="NAME">Name of field containing image processing string</option>
                                   <option value="true,false">Auto process image</option>
                                   <option value="true,false">Show image quality feedback</option>
                                   <option value="true,false">Use stability feature</option>
                                   <option value="0-100">Stability sensitivity </option>
                                   <option value="true,false">Do review image</option>
                                   <option value="true,false">Do process only</option>
                                   <option value="camera,photo_album">Document image source</option>
                                   <option value="NAME">Name of field containing photo file paths</option>
                                   <option value="NAME">Name of field containing processed image file paths</option>
                                   <option value="true,false">Show capture and process settings</option>
                               </select> </td>
                           <td class="style100" valign="top">
                               &nbsp;</td>
                       </tr>
                   </table>
                 </div>
       
            </telerik:RadPageView>
             <telerik:RadPageView ID="manage_document_case_view" runat="server">
                <div id="addManageDocumentCasePropertyDiv">
                   <table style="width: 400px; height: 30px;">
                       <tr>
                           <td align="left" class="style23" valign="top">
                               <select name="D2" onchange="addPropertySelected(this,'addManageDocumentCasePropertyDiv');" 
                                   style="width: 173px; font-size:12px">
                                   <option>Add a property -&gt;</option>
                                   <option value="kofax, user_defined_cases_kofax_submission, user_defined_cases_user_defined_submission">Data flow type</option>
                                   <option value="NAME">Name of field containing case definition</option>
                                   <option value="NAME">Name of field containing case data</option>
                                   <option value="URL">Case management logo url</option>
                                   <option>About box text</option>
                                   <option value="NAME">Name of field containing email body template</option>
                                   <option value="true,false">Show settings screen</option>
                                   <option>Replacement for case label name </option>
                                   <option value="NAME">Name of field containing do capture and process images</option>
                                    <option value="NAME">Name of field containing case management server url</option>
                               </select> </td>
                           <td class="style100" valign="top">
                               &nbsp;</td>
                       </tr>
                   </table>
                 </div>
       
            </telerik:RadPageView>
               </telerik:RadMultiPage>
                </div>
    <div style="height:10px; background-color:#ddeedd; vertical-align:middle; width: 791px;"></div>
    
     <div style="height:30px; background-color:#ddeedd; vertical-align:middle; width: 790px;" 
                   id="compute_container" runat="server">
     <table style="width: 741px"><tr><td>
     <input type="checkbox" id="docompute"/></td><td class="style5">And Compute Among Fields</td>
         <td class="style36"><input type="text" id="compute"  size="70"/></td>
         <td align="right" class="style4">
      <img id="ComputeHelp" src="../images/help.gif" 
        alt="How is the compute property used?" onclick="PopUp('../Help/Design/ComputeHelp.htm', 'height=600, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, titlebar=no,scrollbars=no, resizable=no');return false;" 
        />

     </td></tr></table>
    </div>
      
       
       <div style="height:16px; width: 789px;"></div>
     <div style="height:25px; vertical-align:bottom; width: 788px;">
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Button"/>
    </div>  
    </form>
</body>
</html>