<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertTextField.aspx.cs" Inherits="InsertTextField" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert a Text Field</title>
     <style type="text/css">
          body
        {
        	font-family: tahoma; font-size: 12px; 
        }
        .colorPickerWrapper
        {
            float: left;
            border: solid 1px #9c9c9c;
            padding: 5px 5px 5px 17px;
            width: 393px;
            background:#F1F1F1 url('../images/editor_images/client-side-api.gif') repeat-x 0 0;
        }

         #textFieldDefaultValue
         {
             width: 178px;
         }

    </style>
    <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
    <script  language="javascript" type="text/javascript" src="../scripts/color_picker_1.0.js"></script>
    <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
    <script  language="javascript" type="text/javascript" src="js/insertTextField_1.2.min.js"></script>
</head>
<body>
    <form id="textfieldform" runat="server">
    <telerik:RadScriptManager ID="RadScriptManagerTextField" runat="server">
                </telerik:RadScriptManager>
    <div style="height:38px;">
        Internal Text Field Name:&nbsp;<input type="text" id="textFieldName" />
    </div>
    <div style="height:38px;">
        Default Value (only 1 line)&nbsp;<input type="text" id="textFieldDefaultValue" />&nbsp;(This can be left blank)
    </div>
    <div style="height: 25px">
    A value from a data interface response will overwrite the field value. 
    </div>
   <div style="height:40px;">Select type of field&nbsp;&nbsp;
   <select id="field_type">
   <option value="text" selected="true">Text Field</option>
   <option value="password">Password Field</option>
   </select>  
    </div>
    <div style="height:42px;">Select type of keyboard&nbsp;&nbsp;
  <select id="keyboard">
   <option value="default" selected="true">General</option>
   <option value="numbersandpunctuation">Numbers & Punctuation</option>
   <option value="URL">URL</option>
    <option value="numbers">Numbers</option>
     <option value="phone">Phone Number</option>
      <option value="namephone">Name and Phone</option>
       <option value="email">Email</option>
   </select>
   </div>  
    <div style="height:42px;">Validation on page change&nbsp;&nbsp;
  <select id="validation">
   <option value="none" selected="true">None</option>
   <option value="required_field">Required Field</option>
   </select>
   </div>      
     <div style="height: 37px">
    <table><tr> <td>
               <telerik:RadComboBox ID="FontFamily" runat="server" Width="140px"  OnClientSelectedIndexChanged="OnFontFamilyClientSelectedIndexChanged"   OnClientLoad="OnFontFamilyClientLoad">
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
    <div style="height:58px;">
           <table><tr><td>
                    <div class="colorPickerWrapper">
        <div style="float: left;">
            <div style="line-height: 21px; float: left;">
                Select Font Color:&nbsp;</div>
            <telerik:RadColorPicker ShowIcon="true" ID="FontColor" runat="server" PaletteModes="All" SelectedColor="Black"
                OnClientColorChange="HandleColorChange" OnClientLoad="OnColorClientLoad" Style="float: left;" />
        </div>
        <div style="float: right">
            <div style="line-height: 21px; float: left;">
                Selected Font Color Value:&nbsp;</div>
            <input type="text" value="#000000" id="ColorPickerSelectedColor" style="width: 70px;" readonly="readonly" />
        </div>
    </div>


               </td></tr></table>
    </div>
      <div style="height:25px;">
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Text Field"/>
    </div>    
    

    </form>
</body>
</html>