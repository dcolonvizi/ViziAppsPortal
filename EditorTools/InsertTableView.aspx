<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertTableView.aspx.cs" Inherits="InsertTableView" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert a Table</title>
    <style type="text/css">
        body
        {
        	font-family:Verdana;
        	font-size:11px;
        }
        #field_name
        {
            width: 475px;
        }
        .style8
        {
            width: 64px;
        }
        .style9
        {
            width: 182px;
        }
        .style10
        {
            width: 200px;
        }
        #compute
        {
            width: 455px;
        }
        .style36
        {
            width: 422px;
        }
                
        #foursquare_account_field
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
            width: 181px;
        }
        
        .style67
        {
            width: 179px;
        }
        
        .style68
        {
            width: 192px;
        }
        
        .style33
        {
            width: 93px;
        }
                
        #next_page_condition0
        {
            width: 235px;
        }
        #next_page_condition
        {
            width: 237px;
        }
                
        .style70
        {
            width: 164px;
        }
                
        </style>
         <script type="text/javascript" src="../jquery/js/jquery-1.5.1.min.js"></script>
         <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
         <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
         <script  language="javascript" type="text/javascript" src="js/insertTableView_1.20.min.js"></script>

</head>
<body>
    <form id="tableform" runat="server">
            <telerik:RadScriptManager ID="RadScriptManagerTable" runat="server">
                </telerik:RadScriptManager>
          <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>            
  
           <telerik:AjaxSetting AjaxControlID="rowType">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="rowType" > </telerik:AjaxUpdatedControl>
                      <telerik:AjaxUpdatedControl ControlID="FieldNameMessage" > </telerik:AjaxUpdatedControl>
                      <telerik:AjaxUpdatedControl ControlID="selectedType" > </telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="SectionPages" > </telerik:AjaxUpdatedControl>
                  </UpdatedControls>
            </telerik:AjaxSetting>

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
     
   
    <div style="height:30px;width:790px;font-family:Verdana;font-size:12px">
        Internal Table Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox runat="server" id="tableName" width="200"/>
    </div>
    <div style="height:30px;width:790px;font-family:Verdana;font-size:12px">
        Table Display Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;<asp:TextBox runat="server" id="tableDisplayName" width="200"/>
    &nbsp;This name does not have to be unique</div>
    
    <div style="height:35px;width:790px;vertical-align:middle;background-color:#ffeeee;">
    <table border="0" cellpadding="0" cellspacing="0" height="100%"><tr>
        <td class="style70">
    Select Table Row Type
    </td>
    <td style="font-family:Verdana; font-size:12px;">
    <telerik:RadComboBox ID="rowType" runat="server"  Width="600px" Height="170px" 
            onselectedindexchanged="rowType_SelectedIndexChanged"  AutoPostBack="true" 
            OnClientLoad="OnClientRowTypeLoadHandler" style="margin-left: 0px">
    <Items>
    <telerik:RadComboBoxItem  Selected="true" Value="1text|text"  Text="1 text field" ImageUrl="~/images/editor_images/tabletypewithoutimage.png"/>
    <telerik:RadComboBoxItem  Value="2texts|text,text" Text="2 text fields" ImageUrl="~/images/editor_images/TableTypeWith2TextFields.png"/>
    <telerik:RadComboBoxItem  Value="image1text|image,text" Text="1 image and 1 text field" ImageUrl="~/images/editor_images/tabletypewithimage.png"/>
    <telerik:RadComboBoxItem  Value="image2texts|image,text,text" Text="1 image and 2 texts fields" ImageUrl="~/images/editor_images/tabletypewithimage2texts.png"/>
    <telerik:RadComboBoxItem  Value="1texthidden|text,hidden"  Text="1 text and 1 hidden field" ImageUrl="~/images/editor_images/tabletypewithoutimage_w_hidden.png"/>
    <telerik:RadComboBoxItem  Value="2textshidden|text,text,hidden" Text="2 text fields and 1 hidden field" ImageUrl="~/images/editor_images/TableTypeWith2TextFields_w_hidden.png"/>
    <telerik:RadComboBoxItem  Value="image1texthidden|image,text,hidden" Text="1 image, 1 text field and 1 hidden field" ImageUrl="~/images/editor_images/tabletypewithimage_w_hidden.png"/>
    <telerik:RadComboBoxItem  Value="image2textshidden|image,text,text,hidden" Text="1 image, 2 text fields and 1 hidden field" ImageUrl="~/images/editor_images/tabletypewithimage2texts_w_hidden.png"/>
    </Items>
    
    </telerik:RadComboBox>
    </td></tr></table>
    </div>
 
 <div style="height:40px;width:790px; vertical-align:middle;background-color:#ffeeee;"> 
 <table style="height:100%; width: 775px;"><tr><td style="width: 160px">
 <asp:Label ID="FieldNameMessage" runat="server" Text="Enter the Field Name" 
         Width="159px"></asp:Label></td>
     <td><asp:TextBox id="field_names" runat="server" width="600px"/>
         </td></tr></table>
 </div>

    <div>
    <telerik:RadTabStrip ID="TableTabStrip" runat="server" MultiPageID="TableMultiPage"
                SelectedIndex="0" CssClass="tabStrip" 
            OnClientTabSelected="tabSelected" Width="830px">
                <Tabs>
                    <telerik:RadTab Text="Actions" Selected="True">
                    </telerik:RadTab>
                    <telerik:RadTab Text="Preloaded Lists">
                    </telerik:RadTab>                   
                </Tabs>
            </telerik:RadTabStrip>
            </div>
             <telerik:RadMultiPage ID="TableMultiPage" runat="server" SelectedIndex="0" 
                Width="790px" >
                <telerik:RadPageView ID="ActionsView" runat="server" Width="790px" >
    <div style="height:13px;"> </div>
   
    <div style="height:20px; font-weight:bold; vertical-align:bottom;">
        When a user taps a table row:
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
              <telerik:RadPageView ID="page_from_field_view" runat="server">
               <div></div>
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
             <telerik:RadPageView ID="select_view" runat="server">
                 <div style="height:32px;">
                     <table>
                         <tr>
                             <td class="style36">
                                 Field that will automatically contain the number of rows selected:</td>
                             <td class="style10">
                                 <input type="text" id="count_field" size="25"/>
                             </td>
                             <td class="style10">
                                 (optional)</td>
                         </tr>
                     </table>
                 </div>
            </telerik:RadPageView>
                 <telerik:RadPageView ID="post_view" runat="server">
                     <div>
                         <table>
                             <tr>
                                 <td width="600px">
                                 </td>
                                 <td>
                                     <asp:ImageButton ID="NetworkInterfaceHelp" runat="server" 
                                         ImageUrl="~/images/help.gif" 
                                         OnClientClick="PopUp('../Help/Design/NetworkInterfaceHelp.htm', 'height=380, width=700, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, titlebar=no,scrollbars=no, resizable=no');return false;" 
                                         ToolTip="How does the device access web data sources?" />
                                 </td>
                             </tr>
                         </table>
                     </div>
            </telerik:RadPageView>
 
            <telerik:RadPageView ID="call_view" runat="server">
                 <div style="height:32px;">
    <table style="width: 765px"><tr><td>
        Call Phone in field:</td>
        <td><input type="text" id="device_field" size="25"/></td>
        <td>
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
                             <td class="style67">
                                 Message to share in field:</td>
                             <td class="style38">
                                 <input type="text" id="message_field" size="30"/>
                             </td>
                             <td align="right" class="style42">
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
                             <td class="style63" >
                                 SMS phone(s) in field:</td>
                             <td class="style68" >
                                 <input type="text" id="sms_phone_field" 
                size="30"/>
                             </td>
                             <td  align="right" class="style42">
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
    <table style="width: 781px"><tr><td class="style63" valign="top">
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
                             <td class="style64">
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
                 <div style="height:33px;">
                     <table style="width: 770px">
                         <tr>
                             <td class="style63">
                                 Message to share in field:</td>
                             <td class="style38">
                                 <input type="text" id="message_field2" size="30"/>
                             </td>
                             <td align="right" class="style42">
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
    <table style="width: 776px"><tr><td class="style63" align="left" valign="top">
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
                             <td class="style64">
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
        </telerik:RadMultiPage>
    </div>
 
         <div style="height:10px; background-color:#ddeedd; vertical-align:middle;">        
         </div>
     <div style="height:30px; background-color:#ddeedd; vertical-align:middle;" id="compute_container" runat="server">
     <table><tr>
         <td>
     <input ID="docompute" type="checkbox" /></td>
         <td class="style9">And Compute Among Fields</td>
         <td>
             <input ID="compute" size="60" type="text" /></td>
         <td align="right" class="style8">
      <asp:ImageButton ID="ComputeHelp" runat="server" ImageUrl="~/images/help.gif" 
                 OnClientClick="PopUp('../Help/Design/ComputeHelp.htm', 'height=600, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, titlebar=no,scrollbars=no, resizable=no');return false;" 
                 ToolTip="How is the compute property used?" />
     </td></tr></table>
    </div>
     </telerik:RadPageView>
                <telerik:RadPageView ID="ListView" runat="server" Width="790px">
                <div>
       <telerik:RadMultiPage id="SectionPages" runat="server" SelectedIndex="0"   on="initSectionPages" >
   
                 <telerik:RadPageView id="section1" runat="server" Height="285px">
                  <div style="height:10px;"></div>
     <div style=" font-weight:bold;height:20px;">
         &nbsp;&nbsp; Item List: <span style="font-weight:normal">(If the table field values will be set from a web 
         data source, leave this blank)</span>
    </div>
    <div><table style="width:325px">
        <tr><td class="section1">???</td>
            </tr></table></div>
    <div style="height:250px;">
        <asp:TextBox id="section1Options" rows="15" width="320px" runat="server" 
            TextMode="MultiLine" Height="224px"></asp:TextBox>
    </div>
        </telerik:RadPageView>
                 <telerik:RadPageView id="sections2" runat="server" Height="284px">
    <div style="height:10px;"></div>
     <div style=" font-weight:bold;height:20px;">
         &nbsp;&nbsp; Item Lists: <span style="font-weight:normal">(If the table field values will be set from
         <span style="font-weight:normal">a web data source</span>, 
         leave this blank)</span>
    </div>
    <div ><table style="width:626px">
        <tr><td class="section1" style="width:310px">???</td>
            <td class="section2">???</td></tr></table></div>
    <div style="height:250px;">
        <table><tr>
            <td>
        <asp:TextBox id="sections2Options1" rows="15" width="300px" runat="server" 
               TextMode="MultiLine" Height="224px"></asp:TextBox>
        </td>
       <td>
        <asp:TextBox ID="sections2Options2" runat="server" Height="224px" rows="15" 
               TextMode="MultiLine" width="300px"></asp:TextBox>
        </td></tr></table>
    </div>
    </telerik:RadPageView>
                 <telerik:RadPageView id="sections3" runat="server" Height="285px">
    <div style="height:10px;"></div>
     <div style=" font-weight:bold;height:20px;">
         &nbsp;&nbsp; Item Lists: <span style="font-weight:normal">(If the table field values will be set from
         <span style="font-weight:normal">a web data source</span>, 
         leave this blank)</span>
    </div>
     <div ><table style="width:786px">
        <tr><td class="section1" style="width: 258px">???</td>
            <td class="section2" style="width: 258px">???</td>
            <td class="section3">???</td></tr></table></div>
    <div style="height:250px;">
      <table><tr>
          <td>
        <asp:TextBox id="sections3Options1" rows="15" width="250px" runat="server" 
               TextMode="MultiLine" Height="224px"></asp:TextBox>
        </td>
       <td>
        <asp:TextBox id="sections3Options2" rows="15" width="250px" runat="server" 
               TextMode="MultiLine" Height="224px"></asp:TextBox>
        </td>
       <td>
        <asp:TextBox ID="sections3Options3" runat="server" Height="224px" rows="15" 
               TextMode="MultiLine" width="250px"></asp:TextBox>
        </td>
        </tr></table>
            </div>
    </telerik:RadPageView>
                 <telerik:RadPageView id="sections4" runat="server" Height="284px">
    <div style="height:10px;"></div>
    <div style=" font-weight:bold;height:20px;">
         &nbsp;&nbsp; Item Lists: <span style="font-weight:normal">(If the table field values will be set from
         <span style="font-weight:normal">a web data source</span>, 
         leave this blank)</span>
    </div>
     <div ><table style="width:840px">
        <tr><td class="section1" style="width: 210px">???</td>
            <td class="section2" style="width: 208px">???</td>
            <td class="section3" style="width: 208px">???</td>
            <td class="section4">???</td></tr></table></div>
    <div style="height:250px;">
      <table><tr>
          <td>         
        <asp:TextBox id="sections4Options1" rows="15" width="200px" runat="server" 
               TextMode="MultiLine" Height="224px"></asp:TextBox></td>
          <td>
        <asp:TextBox id="sections4Options2" rows="15" width="200px" runat="server" 
                TextMode="MultiLine" Height="224px"></asp:TextBox>
        </td>
       <td>
        <asp:TextBox ID="sections4Options3" runat="server" Height="224px" rows="15" 
               TextMode="MultiLine" width="200px"></asp:TextBox>
        </td>
        <td>
        <asp:TextBox ID="sections4Options4" runat="server" Height="224px" rows="15" 
                TextMode="MultiLine" width="200px"></asp:TextBox>
        </td>
        </tr></table>
          </div>
    </telerik:RadPageView>
        </telerik:RadMultiPage>
        </div>
      </telerik:RadPageView>
      </telerik:RadMultiPage>
     <div style="height:10px;width:790px; "></div>
     <div style="height:28px;width:790px; vertical-align:bottom;">
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Table"/>
         <asp:TextBox ID="options" runat="server" style="display:none" />
         </div>         
         </form>
</body>
</html>