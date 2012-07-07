<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertHIddenField.aspx.cs" Inherits="EditorTools_InsertHiddenField" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert Hidden Field</title>
    <style type="text/css">
         body
        {
        	font-family: tahoma; font-size: 12px; 
        }
        .style1
        {
            width: 256px;
        }
        .style2
        {
            width: 164px;
        }
    
.RadComboBox_Default
{
	font: 12px "Segoe UI", Arial, sans-serif;
	color: #333;
}

.RadComboBox
{
	vertical-align: middle;
    display: -moz-inline-stack;
    display: inline-block;
}

.RadComboBox
{
	text-align: left;
}

.RadComboBox_Default
{
	font: 12px "Segoe UI", Arial, sans-serif;
	color: #333;
}

.RadComboBox
{
	vertical-align: middle;
    display: -moz-inline-stack;
    display: inline-block;
}

.RadComboBox
{
	text-align: left;
}

.RadComboBox *
{
	margin: 0;
	padding: 0;
}

.RadComboBox *
{
	margin: 0;
	padding: 0;
}

.RadComboBox_Default .rcbInputCellLeft
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2011.1.413.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbInputCellLeft
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox_Default .rcbInputCellLeft
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2011.1.413.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbInputCellLeft
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox .rcbReadOnly .rcbInput
{
	cursor: default;
}

.RadComboBox .rcbReadOnly .rcbInput
{
	cursor: default;
}

.RadComboBox_Default .rcbInput
{
	font: 12px "Segoe UI", Arial, sans-serif;
	color: #333;
}

.RadComboBox .rcbInput
{
	text-align: left;
}

.RadComboBox_Default .rcbInput
{
	font: 12px "Segoe UI", Arial, sans-serif;
	color: #333;
}

.RadComboBox .rcbInput
{
	text-align: left;
}

.RadComboBox_Default .rcbArrowCellRight
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2011.1.413.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbArrowCellRight
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox_Default .rcbArrowCellRight
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2011.1.413.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbArrowCellRight
{
	background-color: transparent;
	background-repeat: no-repeat;
}

        .style5
        {
            width: 88px;
        }
    </style>
</head>
<body>
    <form id="buttonform" runat="server">
      <telerik:RadScriptManager ID="RadScriptManagerHiddenField" runat="server">
                </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Telerik">
            <Windows>
               
               <telerik:RadWindow 
                    id="PageHelp" 
                    runat="server"
                    showcontentduringload="false"
                    width="770px"
                    height="800px"
                     Top="200px"
                     Left="200px"
                    title="How the Hidden Field is Used" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>
            </Windows>
            </telerik:RadWindowManager>
    <div style="height:33px;">
    <table style="width: 493px">
    <tr><td class="style2">
        Internal Hidden Field Name:</td><td class="style1"><input type="text" id="HiddenName" size="40"/></td>
        <td align="right">
      <asp:ImageButton ID="HiddenFieldHelp" runat="server" ImageUrl="~/images/help.gif" 
        ToolTip="How is a hidden field used?" OnClientClick="PopUp('../Help/Design/HiddenFieldHelp.htm', 'height=300, width=500, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');return false;"/>

        </td></tr></table>
    </div>
   <div style="height:32px;">
   <table><tr><td class="style5">
   Set Value To:
   </td><td>
    <telerik:RadComboBox ID="value_type" runat="server"  Width="150px" OnClientLoad="OnClientValueTypeLoadHandler">
    <Items>
    <telerik:RadComboBoxItem  Selected="true" Value="default_value"  Text="Default Value Below" />
    <telerik:RadComboBoxItem  Value="mobile_device_id" Text="Device ID" />
    <telerik:RadComboBoxItem  Value="mobile_device_model" Text="Device Model" />
     <telerik:RadComboBoxItem  Value="mobile_system_version" Text="Device System Version" />
    </Items>
    
    </telerik:RadComboBox>
       </td></tr></table>
   </div>
    <div style="height:32px;">
   <table><tr><td class="style5">
  Default Value:
   </td><td>
    <input type="text" id="default_value" size="40"/> This field can be left empty
       </td></tr></table>
   </div>
   
        <div style="height:33px;">
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Hidden Field"/>
           </div>
    

    <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
    <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
   <script  language="javascript" type="text/javascript" src="js/insertHiddenField_1.0.min.js"></script>

    </form>
</body>
</html>
