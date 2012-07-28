<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertImageButton.aspx.cs" Inherits="InsertImageButton" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert An Image Button with an Action</title>
    <style type="text/css">
        body
        {
        	font-family:Verdana;
        	font-size:11px;
        	color:#000000;
        }
        .style8
        {
            width: 80px;
        }
        .style9
        {
            width: 424px;
        }
        .style26
        {
            width: 135px;
        }
        .style27
        {
            width: 123px;
        }
        .style28
        {
            width: 156px;
        }
        .style32
        {
            width: 620px;
            height: 34px;
        }
        #compute
        {
            width: 427px;
        }
        .style41
        {
            width: 618px;
        }
        .style53
        {
            width: 180px;
        }
        .style54
        {
            width: 620px;
            height: 29px;
        }
        .style55
        {
            width: 620px;
            height: 37px;
        }
        .style61
        {
            width: 192px;
        }
        #foursquare_account_field
        {
            width: 186px;
        }
        .style63
        {
            width: 177px;
        }
        .style38
        {
            width: 195px;
        }
        .style42
        {
            width: 154px;
        }
        .style66
        {
            width: 161px;
        }
        .style69
        {
            width: 160px;
        }
        .style70
        {
            width: 165px;
        }
        .style71
        {
            width: 176px;
        }
        .style72
        {
            width: 162px;
        }
        .style74
        {
            width: 198px;
        }
        .style75
        {
            width: 159px;
        }
        .style82
        {
            width: 147px;
        }
        #image_field
        {
            width: 188px;
        }
        #compression
        {
            width: 70px;
        }
        #icon_field
        {
            width: 188px;
            margin-left: 0px;
        }
        .style84
        {
            width: 251px;
        }
        .style86
        {
            width: 210px;
        }
        .style88
        {
            width: 67px;
        }
        .style90
        {
            width: 250px;
        }
        .style91
        {
            width: 42px;
        }
        .style92
        {
            width: 87px;
        }
        .style96
        {
            width: 333px;
        }
        .style97
        {
            width: 199px;
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
        #next_page_condition0
        {
            width: 239px;
        }
        .style23
        {
            width: 83px;
        }
          .style102
        {
            width: 215px;
        }
          .style103
        {
            width: 150px;
        }
          .style104
        {
            width: 140px;
        }
        .style105
        {
            width: 139px;
        }
          </style>
         <script type="text/javascript" src="../jquery/js/jquery-1.5.1.min.js"></script>
         <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
         <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
         <script  language="javascript" type="text/javascript" src="js/insertImageButton_1.40.min.js"></script>
</head>
<body>
    <form id="imageform" runat="server">
     <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
                </telerik:RadScriptManager>
     <telerik:RadWindowManager ID="RadWindowManagerImage" runat="server" Skin="Telerik">
            <Windows>
                <telerik:RadWindow 
                    id="UploadImageHelp" 
                    runat="server"
                    showcontentduringload="false"
                    width="600px"
                    height="350px"
                    title="How To Upload an Image"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>

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
                    id="buttonImageWindow" 
                    runat="server"
                    showcontentduringload="true"
                    title="Choose Button Image" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>

             <telerik:RadWindow 
                    id="imageArchive" 
                    runat="server"
                    showcontentduringload="true"
                    title="Choose an Icon or Bar Image" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>
                </Windows>
     </telerik:RadWindowManager>
                         
      <telerik:RadDialogOpener runat="server" id="DialogOpener1"></telerik:RadDialogOpener> 
            <div style="height:34px;">
    <table style="width: 717px"><tr><td class="style26"> Internal Button Name:</td>
        <td class="style28"><input type="text" id="buttonName" size="30"/></td>
        <td align="center" class="style27">
            &nbsp;</td><td>
          <asp:HiddenField ID="ImageWidth" runat="server" />
               <asp:HiddenField ID="ImageHeight" runat="server"/><asp:HiddenField ID="AccountType" runat="server" /></td></tr></table>
      
    </div>

       <div style="height:144px; background-color: #FFFFEC;">
       <table style="width: 716px">
       <tr>
       <td class="style54">Choose one of 4 ways to set the button image: load a stock image,  
           upload your own image, use previous image or use an existing image URL</td>
           </tr>
       <tr>
       <td class="style32">
       <table style="width: 643px"><tr><td>
         <input type="button" onclick="javascript:showSetButtonImageClient();" 
                value="Choose From Our Stock Button Images" style="width: 265px"/></td><td>
        <input type="button" onclick="javascript:showUploadButtonImageClient();" 
                value="Upload Your Custom Image" style="width: 182px"/></td><td>
        <input type="button" onclick="javascript:showPreviousImageClient();" 
                value="Use a Previous Image" style="width: 155px"/></td></tr></table>
                </td>
           </tr>
       <tr>
       <td class="style55">
        <table style="width: 702px; height: 31px;"><tr><td>
       Enter Image URL:</td><td>
               <asp:TextBox ID="imagePath" 
               runat="server" Width="578px"></asp:TextBox> </td></tr></table>     
           </td>
           </tr>
       <tr><td> &nbsp;</td>
           </tr>
       </table> 
    </div>
    
  <div style="height:20px; font-weight:bold; vertical-align:bottom;">
        When a user taps the button:
     </div>
    <div>
        <telerik:RadComboBox ID="actions" runat="server" 
            OnClientSelectedIndexChanged="changeAction" Width="600px" >
        </telerik:RadComboBox>
    </div>
    <div style="height:10px;"></div>
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
           <table style="width:778px"><tr>
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
          </tr></table>

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
                                <img id="NetworkInterfaceHelp" 
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
                     <table style="width: 745px">
                         <tr>
                             <td class="style53">
                                 Call Phone in field:</td>
                             <td class="style63">
                                 <input type="text" id="device_field" size="25"/>
                             </td>
                             <td class="style10">
                                 (name of the field that will contain the actual phone number)</td>
                         </tr>
                     </table>
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
                             <td class="style63" valign="top">
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
                             <td class="style63">
                                 Message to share in field:</td>
                             <td class="style38">
                                 <input type="text" id="message_field" size="30"/>
                             </td>
                             <td align="right" class="style72">
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
                             <td class="style71" >
                                 SMS phone(s) in field:</td>
                             <td class="style74" >
                                 <input type="text" id="sms_phone_field" 
                size="30"/>
                             </td>
                             <td  align="right" class="style75">
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
    <table style="width: 781px"><tr><td class="style66" valign="top">
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
                             <td class="style69">
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
                             <td class="style69">
                                 Message to share in field:</td>
                             <td class="style38">
                                 <input type="text" id="message_field2" size="30"/>
                             </td>
                             <td align="right" class="style70">
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
    <table style="width: 776px"><tr><td class="style66" align="left" valign="top">
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
                             <td class="style66">
                                 Message to share in field:</td>
                             <td class="style38">
                                 <input type="text" id="message_field3" size="30"/>
                             </td>
                             <td align="right" class="style42">
                                 Hyperlink to photo in field:</td>
                             <td>
                                 <input type="text" id="media_link_field3" size="30"/>
                             </td>
                         </tr>
                     </table>
                 </div>
            </telerik:RadPageView>
             <telerik:RadPageView ID="take_photo_view" runat="server">
               <div style="height: 32px"><table style="width: 768px"><tr><td align="left" 
                       class="style84">Photo image URL is saved in<strong> hidden field</strong>: </td>
                   <td class="style82">
                       <input 
                type="text" id="image_field" size="20"/>
                   </td>
                   <td align="right" class="style86">
                       Compression Ratio (0.001 to 1.0)</td>
                   <td><input 
                type="text" id="compression" size="20"/></td></tr></table></div>
       
        <div style="height: 63px"><table style="width: 778px">
        <tr><td class="style90">Automated icon is saved in <strong>hidden field</strong>:</td>
            <td colspan="3" class="style3"><input 
                type="text" id="icon_field" size="20"/></td>
            <td class="style3">
                (Icon is generated only if icon field name is filled)
            </td>
            </tr>
        <tr><td align="right" class="style90">Icon width:&nbsp;&nbsp;&nbsp;&nbsp; </td><td class="style91"><input 
                type="text" id="icon_width" size="5" value="60"/></td>
            <td align="right" 
                class="style92">&nbsp;Icon height:&nbsp;&nbsp; </td><td class="style88"><input 
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
                            <td class="style102" align="left">
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
         <td class="style105" align="right">
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
         <td class="style105" align="right">
             Login alert field:</td>
         <td>
             <input type="text" id="mcommerce_login_alert_field" size="30" class="mcommerce"/>
         </td>
         </tr>
         </table></div>  
         <div style="height:33px;">
     <table style="width: 770px"><tr><td class="style103">
         Login page name:</td><td class="style38"> 
             <input type="text" id="mcommerce_login_page_name" size="30" class="mcommerce"/>
         </td>
         <td class="style104" align="right">
             Login info button name:</td>
         <td>
             <input type="text" id="mcommerce_login_info_button_name" size="30" 
                 class="mcommerce"/>
         </td>
         </tr>
         </table></div>             
            </telerik:RadPageView>
            <telerik:RadPageView ID="init_card_swiper_view" runat="server">
                <div id="addSwiperPropertyDiv">
                    <table style="width: 400px; height: 30px;">
                        <tr>
                            <td align="left" class="style23" valign="top">
                                <select onchange="addPropertySelected(this,'addSwiperPropertyDiv');" 
                                    style="width: 173px; font-size:12px">
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
                             </select> </td>
                            <td valign="top" class="style100">
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
            <div>
    <table style="width: 784px"><tr>
        <td  align="left" valign="top" 
                       class="style96">
                   Latest document case data are saved as XML in field:</td>
        <td class="style97" valign="top">
            <input type="text" id="doc_case_field" size="30"/></td>
        <td valign="top">
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
     <div style="height:10px; background-color:#ddeedd; vertical-align:middle;">
         </div>
    <div style="height:30px;  background-color:#ddeedd" id="compute_container" runat="server">
    <table style="width: 736px"><tr><td>
     <input type="checkbox" id="docompute"/></td><td class="style9">And Compute Among Fields</td>
        <td class="style41"><input type="text" id="compute"  size="60"/></td>
        <td align="right" class="style8">
      <img id="ComputeHelp" src="../images/help.gif" 
        alt="How is the compute property used?" onclick="PopUp('../Help/Design/ComputeHelp.htm', 'height=600, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, titlebar=no,scrollbars=no, resizable=no');return false;" 
        /></td></tr></table>
    </div>
    
      <div style="height:16px;"></div>
     <div style="height:28px; vertical-align:bottom;">
         <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Button"/>
    </div>
    </form>
</body>
</html>
