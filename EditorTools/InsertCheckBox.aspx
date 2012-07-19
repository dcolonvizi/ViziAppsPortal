<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertCheckBox.aspx.cs" Inherits="EditorTools_InsertCheckBox" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert a CheckBox with an Action</title>
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
            width: 199px;
        }
        .style4
        {
            width: 77px;
        }
        .style5
        {
            width: 165px;
        }
        #compute
        {
            width: 416px;
        }
        .style38
        {
            width: 243px;
        }
        .style42
        {
            width: 154px;
        }
        .style53
        {
            width: 193px;
        }
        #sms_phone_field
        {
            width: 184px;
        }
        #foursquare_account_field
        {
            width: 188px;
        }
        .style56
        {
            width: 180px;
        }
        .style61
        {
            width: 199px;
        }        
        .style65
        {
            width: 176px;
        }
        #sms_phone_field2
        {
            margin-left: 0px;
        }
        #sms_phone_field3
        {
            margin-left: 0px;
        }
        .style67
        {
            width: 196px;
        }
        .style69
        {
            width: 173px;
        }
        .style70
        {
            width: 197px;
        }
        .style72
        {
            width: 177px;
        }
        .style73
        {
            width: 175px;
        }
        #sms_phone_field3
        {
            width: 184px;
        }
        .style74
        {
            width: 178px;
        }
        .style76
        {
            width: 200px;
        }
        .style77
        {
            width: 167px;
        }
        .style78
        {
            width: 179px;
        }
        .style79
        {
            width: 201px;
        }
               
        .style87
        {
            width: 164px;
        }
        .style84
        {
            width: 251px;
        }
        #compression0
        {
            width: 68px;
        }
        #image_field
        {
            width: 188px;
        }
        #compression
        {
            width: 64px;
        }
        #icon_field
        {
            width: 188px;
        }
        .style90
        {
            width: 252px;
        }
        .style100
        {
            width: 302px;
        }
        #shopping_cart_url
        {
            width: 480px;
        }
        .style33
        {
            width: 93px;
        }
        #next_page_condition
        {
            width: 257px;
        }
        .body
        {}
        .style23
        {
            width: 83px;
        }
          </style> 
         <script type="text/javascript" src="../jquery/js/jquery-1.5.1.min.js"></script>
         <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
         <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
         <script  language="javascript" type="text/javascript" src="js/insertCheckBox_1.8.min.js"></script>
</head>
<body>
    <form id="checkboxform" runat="server">
               <telerik:RadScriptManager ID="RadScriptManagercheckbox" runat="server">
                </telerik:RadScriptManager>
          <telerik:RadAjaxManager ID="RadAjaxManagercheckbox" runat="server">
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
            </Windows>
            </telerik:RadWindowManager>
    <div style="height:35px; width: 786px;">
    <table style="width: 741px; height: 25px;"><tr><td class="style87">
        Internal CheckBox Name:</td><td><input type="text" id="checkboxName" 
                size="30" /></td>
        <td align="right"><asp:TextBox ID="AccountType" runat="server" style="display:none"/>
         </td></tr></table>
    </div>
    <div style="height:38px; width: 787px;"><table><tr><td>
        Default Value:</td><td><telerik:RadButton ID="DefaultValue" 
                runat="server" ButtonType="StandardButton" Skin="Office2007" 
                    ToggleType="CustomToggle" OnClientLoad="onDefaultValueLoad" 
                AutoPostBack="False" >
                    <ToggleStates>
                        <telerik:RadButtonToggleState  Text="Checked" />
                        <telerik:RadButtonToggleState  Text="Unchecked" />
                    </ToggleStates>
                    
                </telerik:RadButton></td></tr></table>
    </div>
   
    <div style="height:20px; font-weight:bold; vertical-align:bottom; width: 784px;">
        When a user changes the 
        checkbox to checked :
     </div>
    <div>
        <telerik:RadComboBox ID="actions" runat="server" 
            OnClientSelectedIndexChanged="changeAction" Width="600px" >
        </telerik:RadComboBox>
    </div>
    <div style="height:10px; width: 783px;"></div>
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
           <telerik:RadComboBox ID="GoToPages" runat="server" AllowCustomText="true"  OnClientLoad="OnClientGoToPagesLoadHandler"
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
                    <table style="width:778px">
                        <tr>
                            <td width="20px">
                                If</td>
                            <td width="230px">
                                <input type="text" id="next_page_condition" />
                            </td>
                            <td align="right" width="108">
                                Then Go To Page:</td>
                            <td width="160">
                                <telerik:RadComboBox ID="page_if_true" runat="server" AllowCustomText="true"  OnClientLoad="OnClientpage_if_trueLoadHandler"
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
                                    OnClientTextChange="page_if_false_SelectedIndexChanged" Width="160px">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </telerik:RadPageView>
     <telerik:RadPageView ID="previous_page_view" runat="server">
               <div></div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="post_view" runat="server">
     <div>
         <table>
             <tr>
                 <td width="600px">
                 </td>
                 <td>
                     <img id="NetworkInterfaceHelp0" 
                    src="../images/help.gif"                     
                    alt="How does the device access web data sources?" onclick="PopUp('../Help/Design/NetworkInterfaceHelp.htm', 'height=380, width=700, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, titlebar=no,scrollbars=no, resizable=no');
                return false;" />
                 </td>
             </tr>
         </table>
         
     </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="call_view" runat="server">
                 <div style="height:32px;">
    <table style="width: 759px"><tr><td class="style53">
        Call Phone in field:</td>
        <td class="style56"><input type="text" id="device_field" size="25"/></td>
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
                 <div style="height:32px;">
                     <table style="width: 781px">
                         <tr>
                             <td class="style74" valign="top">
                                 Subject in field:</td>
                             <td class="style12" valign="top" width="200">
                                 <input type="text" id="share_subject_field" size="30"/>
                             </td>
                             <td class="style12">
                                 &nbsp;</td>
                             <td class="style12">
                                 &nbsp;</td>
                         </tr>
                     </table>
                 </div>
                 <div style="height:33px;">
                     <table style="width: 770px">
                         <tr>
                             <td class="style78">
                                 Message to share in field:</td>
                             <td class="style76">
                                 <input type="text" id="message_field" size="30"/>
                             </td>
                             <td align="right" class="style77">
                                 Hyperlink to photo in field:</td>
                             <td>
                                 <input type="text" id="media_link_field" size="30"/>
                             </td>
                         </tr>
                     </table>
                 </div>
                 <div style="height:33px;">
                     <table style="width: 768px">
                         <tr>
                             <td class="style74" >
                                 SMS phone(s) in field:</td>
                             <td class="style79" >
                                 <input type="text" id="sms_phone_field" 
                size="30"/>
                             </td>
                             <td  align="right" class="style77">
                                 Email(s) To, in field:</td>
                             <td >
                                 <input type="text" id="to_email_field" 
                size="30"/>
                             </td>
                         </tr>
                     </table>
                 </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="email_view" runat="server">
                 <div style="height:32px;">
    <table style="width: 781px; height: 28px;"><tr><td class="style65" valign="top">
        Email(s) To, in field:</td>
        <td class="style12" valign="top" width="200">
            <input type="text" id="to_email_field2" size="30"/></td>
        <td class="style12">
            delimit emails with semicolon in the field containing the values</td>
        </tr></table>
    </div>
                 <div style="height:33px;">
                     <table style="width: 770px">
                         <tr>
                             <td class="style65">
                                 Subject in field:</td>
                             <td class="style38">
                                 <input type="text" id="subject_field" size="30"/>
                             </td>
                             <td class="style42" align="right">
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                         </tr>
                     </table>
                 </div>
                 <div style="height: 33px;">
                     <table style="width: 770px">
                         <tr>
                             <td class="style65">
                                 Message to share in field:</td>
                             <td class="style67">
                                 <input type="text" id="message_field2" size="30"/>
                             </td>
                             <td align="right" class="style69">
                                 Hyperlink to photo in field:</td>
                             <td>
                                 <input type="text" id="media_link_field2" size="30"/>
                             </td>
                         </tr>
                     </table>
                 </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="sms_view" runat="server">
                 <div style="height:33px;">
    <table style="width: 776px"><tr><td class="style65" align="left" valign="top">
        SMS phone(s) in field:</td>
        <td class="style61" valign="top">
            <input type="text" id="sms_phone_field2" size="30"/></td>
        <td>
            delimit numbers with semicolon in the field containing the values</td>
        </tr></table>
    </div>
                 <div style="height:33px;">
                     <table style="width: 770px">
                         <tr>
                             <td class="style72">
                                 Message to share in field:</td>
                             <td class="style70">
                                 <input type="text" id="message_field3" size="30"/>
                             </td>
                             <td align="right" class="style73">
                                 Hyperlink to photo in field:</td>
                             <td>
                                 <input type="text" id="media_link_field3" size="30"/>
                             </td>
                         </tr>
                     </table>
                 </div>
            </telerik:RadPageView>
             <telerik:RadPageView ID="take_photo_view" runat="server">
               <div style="height: 32px">
                   <table style="width: 768px">
                       <tr>
                           <td align="left" class="style84">
                               Photo image URL is saved in<strong> hidden field</strong>:
                           </td>
                           <td>
                               <input 
                type="text" id="image_field" size="20"/>
                           </td>
                           <td align="right">
                               Compression Ratio (0.001 to 1.0)</td>
                           <td>
                               <input 
                type="text" id="compression" size="20"/>
                           </td>
                       </tr>
                   </table>
                 </div>
       
        <div style="height: 63px"><table style="width: 778px">
        <tr><td class="style90">Automated icon is saved in <strong>hidden field</strong>:</td>
            <td colspan="3"><input 
                type="text" id="icon_field" size="20"/></td>
            <td>
                (Icon is generated only if icon field name is filled)
            </td>
            </tr>
        <tr><td align="right" class="style90">Icon width:&nbsp;&nbsp; </td><td><input 
                type="text" id="icon_width" size="5" value="60"/></td><td align="right">&nbsp;Icon 
                height:&nbsp;&nbsp; </td><td><input 
                type="text" id="icon_height" size="5" value="80"/></td>
            <td>
                &nbsp;</td>
            </tr>
        </table>
                </div>
                
            </telerik:RadPageView>
            
            <telerik:RadPageView ID="capture_doc_view" runat="server">
            <div><table style="width: 776px"><tr><td  align="left" valign="top" 
                       class="style91">
                   Latest document case data are saved as XML in field:</td>
        <td class="style75" valign="top">
            <input type="text" id="doc_case_field" size="30"/></td>
        <td class="style69" valign="top">
            (This is usually a hidden field)</td>
        </tr></table></div>                
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
 
    <div style="height:10px; background-color:#ddeedd; vertical-align:middle;"></div>
    
     <div style="height:30px; background-color:#ddeedd; vertical-align:middle;" id="compute_container" runat="server">
     <table style="width: 743px"><tr><td>
     <input type="checkbox" id="docompute"/></td><td class="style5">And Compute Among Fields</td><td><input type="text" id="compute"  size="60"/></td>
         <td align="right" class="style4">
      <img id="ComputeHelp" src="../images/help.gif" 
        alt="How is the compute property used?" onclick="PopUp('../Help/Design/ComputeHelp.htm', 'height=600, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, titlebar=no,scrollbars=no, resizable=no');return false;" 
        /></td></tr></table>
    </div>
      
       
       <div style="height:7px;"></div>
     <div style="height:25px; vertical-align:bottom;">
        <input type="button" id="insertButtonID" onclick="insertButton();" 
             value="Insert CheckBox"/>
    </div>    
 

    </form>
</body>
</html>