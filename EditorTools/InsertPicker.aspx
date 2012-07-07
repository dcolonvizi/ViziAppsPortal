<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertPicker.aspx.cs" Inherits="InsertPicker" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert a Picker</title>
    <style type="text/css">
         body
        {
        	font-family: tahoma; font-size: 12px; 
        }
        .style1
        {
            width: 168px;
        }
        .style2
        {
            width: 123px;
        }
        .style3
        {
            width: 138px;
        }
    </style>
        <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
    <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
    <script  language="javascript" type="text/javascript" src="js/insertPicker_1.3.min.js"></script>

</head>
<body>
    <form id="pickerform" runat="server">
               <telerik:RadScriptManager ID="RadScriptManagerPicker" runat="server">
                </telerik:RadScriptManager>
          <telerik:RadAjaxManager ID="RadAjaxManager178" runat="server">
        <AjaxSettings>  
            
           <telerik:AjaxSetting AjaxControlID="pickerType">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="pickerType" > </telerik:AjaxUpdatedControl>
                      <telerik:AjaxUpdatedControl ControlID="SectionMessage" > </telerik:AjaxUpdatedControl>  
                      <telerik:AjaxUpdatedControl ControlID="section_names" > </telerik:AjaxUpdatedControl>                       
                       <telerik:AjaxUpdatedControl ControlID="SectionPages" > </telerik:AjaxUpdatedControl>    
                        <telerik:AjaxUpdatedControl ControlID="SaveOptions" > </telerik:AjaxUpdatedControl>    
                     
                 </UpdatedControls>
            </telerik:AjaxSetting>
            
                     <telerik:AjaxSetting AjaxControlID="SaveOptions">
                <UpdatedControls>
                       <telerik:AjaxUpdatedControl ControlID="SaveListMessage" > </telerik:AjaxUpdatedControl>                                        
                       <telerik:AjaxUpdatedControl ControlID="options" > </telerik:AjaxUpdatedControl>                                        
                 </UpdatedControls>
            </telerik:AjaxSetting>
            
     </AjaxSettings>
     </telerik:RadAjaxManager>
    <div style="height:36px;">
    <table><tr><td class="style2">
        Internal Picker Name:</td><td><asp:TextBox id="PickerName" runat="server" Width="200"/>&nbsp;&nbsp;&nbsp;&nbsp; </td><td>
        Note that Apple does not allow the picker height to be changed.</td></tr></table>
    </div>
        <div style=" background-color:#ffeeee">
    <div style="height:40px;vertical-align:middle;">
    <table border="0" cellpadding="0" cellspacing="0" height="100%"><tr><td>
    Select Picker Type&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </td>
    <td style="font-family:Verdana; font-size:12px;">
    <telerik:RadComboBox ID="pickerType" runat="server"  Width="200px" Height="170px" 
            onselectedindexchanged="pickerType_SelectedIndexChanged"  
            AutoPostBack="true" OnClientLoad="OnClientPickerTypeLoadHandler">
    <Items>
    <telerik:RadComboBoxItem  Selected="true" Value="1_section"  Text="1 list section" />
    <telerik:RadComboBoxItem  Value="2_sections" Text="2 list sections" />
    <telerik:RadComboBoxItem  Value="3_sections" Text="3 list sections" />
    <telerik:RadComboBoxItem  Value="4_sections" Text="4 list sections" />
    <telerik:RadComboBoxItem  Value="date" Text="date" />
    <telerik:RadComboBoxItem  Value="time" Text="time" />
    </Items>
    
    </telerik:RadComboBox>
    </td></tr></table>
    </div>
 
 <div style="height:40px; vertical-align:middle;"> 
 <table style="height:40px; width: 738px;"><tr><td class="style3">
 <asp:Label ID="SectionMessage" runat="server" Text="Enter the Section Name" 
         Width="143px"></asp:Label></td>
     <td><asp:TextBox id="section_names" width="564px" runat="server"/>
         </td></tr></table>
 </div>
 </div> 
 <telerik:RadMultiPage id="SectionPages" runat="server" SelectedIndex="1" >
    <telerik:RadPageView id="nosection" runat="server">
    <div></div>
    </telerik:RadPageView>
     <telerik:RadPageView id="section1" runat="server">
     <div style=" font-weight:bold;height:20px;">
        Item List:
    </div>
    <div style="height:250px;">
        <asp:TextBox id="section1Options" rows="15" width="320" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
    </telerik:RadPageView>
     <telerik:RadPageView id="sections2" runat="server">
     <div style=" font-weight:bold;height:20px;">
        Item Lists:
    </div>
    <div style="height:250px;">
        <table><tr><td>
        <asp:TextBox id="sections2Options1" rows="15" width="300" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
       <td>
        <asp:TextBox id="sections2Options2" rows="15" width="300" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td></tr></table>
    </div>
    </telerik:RadPageView>
     <telerik:RadPageView id="sections3" runat="server">
     <div style=" font-weight:bold;height:20px;">
        Item Lists:
    </div>
    <div style="height:250px;">
      <table><tr><td>
        <asp:TextBox id="sections3Options1" rows="15" width="250" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
       <td>
        <asp:TextBox id="sections3Options2" rows="15" width="250" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
       <td>
        <asp:TextBox id="sections3Options3" rows="15" width="250" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
        </tr></table>
            </div>
    </telerik:RadPageView>
     <telerik:RadPageView id="sections4" runat="server">
     <div style=" font-weight:bold;height:20px;">
        Item Lists:
    </div>
    <div style="height:250px;">
      <table><tr><td>
        <asp:TextBox id="sections4Options1" rows="15" width="200" runat="server" TextMode="MultiLine"></asp:TextBox></td><td>
        <asp:TextBox id="sections4Options2" rows="15" width="200" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
       <td>
        <asp:TextBox id="sections4Options3" rows="15" width="200" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
        <td>
        <asp:TextBox id="sections4Options4" rows="15" width="200" runat="server" TextMode="MultiLine"></asp:TextBox>
        </td>
        </tr></table>
          </div>
    </telerik:RadPageView>
 </telerik:RadMultiPage>
 <div style="height:20px;"></div>
     <div style="height:38px;">
     <table>
     <tr>
     <td class="style1">
             <input type="button" id="insertButtonID" onclick="insertPicker();" value="Insert Picker"/></td>
     <td class="style1">
         &nbsp;</td>
     <td>
             &nbsp;</td>
      <td>
      <asp:TextBox ID="options" runat="server" style="display:none" />
      </td>
      </tr></table>
    </div>
    </form></body></html>