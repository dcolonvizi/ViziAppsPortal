<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnAppOpen.aspx.cs" Inherits="Dialogs_OnAppOpen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Set Action On App Open</title>
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
          .colorPickerWrapper
        {
            float: left;
            border: solid 1px #9c9c9c;
            padding: 5px 5px 5px 17px;
            width: 393px;
            background:#F1F1F1 url('../../images/editor_images/client-side-api.gif') repeat-x 0 0;
        }
        #buttonName
        {
            width: 219px;
        }
        #compute
        {
            width: 464px;
        }
        .style36
        {
            width: 528px;
        }
        #foursquare_account_field
        {
            width: 177px;
        }
        #Text1
        {
            width: 230px;
        }
        #Text2
        {
            width: 176px;
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
        .style93
        {
            width: 52px;
        }
        .style94
        {
            width: 106px;
        }
        #shopping_cart_url
        {
            width: 480px;
        }
        .style23
        {
            width: 83px;
        }
          .style100
        {
            width: 302px;
        }
        .style101
        {
            width: 32px;
        }
        </style>
         <script  language="javascript" type="text/javascript">
            if (window.attachEvent) {
                window.attachEvent("onload", initDialog);
            }
            else if (window.addEventListener) {
                window.addEventListener("load", initDialog, false);
            }

            var ArgList = null;

            function changeAction(sender, args) {
                var view_name = null;
                switch (sender.get_value()) {
                    case 'post':
                    case 'capture_process_document':
                    case 'manage_document_case':
                        view_name = sender.get_value() + '_view';
                        break;
                    default:
                        view_name = 'no_view';
                        break;
                }
                var actionsMultiPage = document.getElementById("actionsMultiPage");
                for (var i = 0; i < actionsMultiPage.control._pageViewData.length; i++) {
                    if (actionsMultiPage.control._pageViewData[i].id == view_name) {
                        actionsMultiPage.control._selectPageViewByIndex(i);
                        //clear all param class selections
                        $('.param').each(function (index, element) {
                            $(this).remove();
                        });
                        return;
                    }
                }
                actionsMultiPage.control._selectPageViewByIndex(0);
                //clear all param class selections
                $('.param').each(function (index, element) {
                    $(this).remove();
                });
            }

            function setComboValue(comboBox, value) {
                if (comboBox.control) {
                    comboBox.control.trackChanges();
                    var item = comboBox.control.findItemByValue(value);
                    if (item) {
                        item.select();
                    }
                    comboBox.control.commitChanges();
                }
                else {
                    comboBox.trackChanges();
                    var item = comboBox.findItemByValue(value);
                    if (item) {
                        item.select();
                    }
                    comboBox.commitChanges();
                }
            }

            function initDialog() {
            }
            function PopUp(url, features) {
                var PUtest = window.open(url, '_blank', features);
                if (PUtest == null) {
                    alert('For correct operation, popups need to be allowed from this website.');
                }
            }
            </script>
</head>
<body>
    <form id="buttonform" runat="server">
               <telerik:RadScriptManager ID="RadScriptManagerButton" runat="server">
                </telerik:RadScriptManager>
          <telerik:RadAjaxManager ID="RadAjaxManagerButton" runat="server">
        <AjaxSettings>            

                       
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
    
    <div style="height:20px; font-weight:bold; vertical-align:bottom;">
       <table><tr><td>When this app opens:</td><td><asp:HiddenField ID="AccountType" runat="server" /></td></tr></table>             
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
            
             <telerik:RadPageView ID="post_view" runat="server">
               <div>
                 
                 </div>
            </telerik:RadPageView>
             
           <!-- <telerik:RadPageView ID="capture_doc_view" runat="server">
               <div>
                   <table style="width: 776px">
                       <tr>
                           <td align="left" class="style91" valign="top">
                               Latest document case data are saved as XML in field:</td>
                           <td class="style75" valign="top">
                               <input runat="server"
                type="text" id="doc_case_field" size="30"/>
                           </td>
                           <td class="style69" valign="top">
                               (This is usually a hidden field)</td>
                       </tr>
                   </table>
                 </div>
       
            </telerik:RadPageView>-->
             <telerik:RadPageView ID="capture_process_document_view" runat="server">
                <div id="addCaptureProcessDocumentPropertyDiv">
                   <table style="width: 400px; height: 30px;">
                       <tr>
                           <td align="left" class="style23" valign="top">
                               <select name="D1" onchange="addPropertySelected(this,'addCaptureProcessDocumentPropertyDiv');" 
                                   style="width: 173px; font-size:12px">
                                   <option>Add a property -&gt;</option>
                                   <option>Name of field containing image processing string</option>
                                   <option>Auto process image</option>
                                   <option>Show image quality feedback</option>
                                   <option>Use stability feature</option>
                                   <option>Stability sensitivity </option>
                                   <option>Do review image</option>
                                   <option>Do process only</option>
                                   <option>Image source</option>
                                   <option>Name of field containing photo file paths</option>
                                   <option>Name of field containing processed image file paths</option>
                                   <option>Show capture and process settings</option>
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
                                   <option>Data flow type</option>
                                   <option>Name of field containing case definition</option>
                                   <option>Name of field containing case data</option>
                                   <option>Case management logo url</option>
                                   <option>About box text</option>
                                   <option>Name of field containing email body template</option>
                                   <option>Show settings screen</option>
                                   <option>Replacement for case label name </option>
                                   <option>Do capture and process images</option>
                                   <option>Case management server url</option>
                               </select> </td>
                           <td class="style100" valign="top">
                               &nbsp;</td>
                       </tr>
                   </table>
                 </div>
       
            </telerik:RadPageView>
                </telerik:RadMultiPage>
                </div>
    <div style="height:10px; background-color:#ddeedd; vertical-align:middle;"></div>
    
     <div style="height:30px; background-color:#ddeedd; vertical-align:middle;" id="compute_container" runat="server">
     <table style="width: 741px"><tr><td class="style101">
         <asp:CheckBox ID="docompute" runat="server" /></td><td class="style5">And Compute Among Fields</td>
         <td class="style36"><input type="text" id="compute"  size="70" runat="server"/></td>
         <td align="right" class="style4">
      <asp:ImageButton ID="ComputeHelp" runat="server" ImageUrl="~/images/help.gif" 
        ToolTip="How is the compute property used?" OnClientClick="PopUp('../../Help/Design/ComputeHelp.htm', 'height=600, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, titlebar=no,scrollbars=no, resizable=no');return false;" 
        />

     </td></tr></table>
    </div>
      
       
       <div style="height:16px;"></div>
     <div style="height:25px; vertical-align:bottom;">
       <table style="width: 741px"><tr><td class="style93"><asp:Button ID="Save" runat="server" onclick="Save_Click" Text="Save" /></td>
           <td class="style94"><asp:Button ID="Clear" runat="server" 
                   Text=" Clear Action" Width="95px" style="display:none" 
                   onclick="Clear_Click"/></td><td>
                                <asp:Label ID="Message" runat="server" Font-Bold="True" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="Maroon"  Width="260px"></asp:Label></td></tr></table>
    </div>  
    </form>
</body>
</html>