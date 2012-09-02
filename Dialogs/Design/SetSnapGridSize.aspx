﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetSnapGridSize.aspx.cs" Inherits="Dialogs_PageTransitionType" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Set Main Device for App</title>
    <style type="text/css">
    body
    {
        font-family:Arial;
        font-size:12px;
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
	background-image: url('mvwres://Telerik.Web.UI, Version=2011.3.1115.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbInputCellLeft
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox_Default .rcbInputCellLeft
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2011.3.1115.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbInputCellLeft
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox_Default .rcbInputCellLeft
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2011.3.1115.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbInputCellLeft
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox_Default .rcbInputCellLeft
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2011.3.1115.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
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
	background-image: url('mvwres://Telerik.Web.UI, Version=2011.3.1115.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbArrowCellRight
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox_Default .rcbArrowCellRight
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2011.3.1115.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbArrowCellRight
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox_Default .rcbArrowCellRight
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2011.3.1115.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbArrowCellRight
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox_Default .rcbArrowCellRight
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2011.3.1115.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbArrowCellRight
{
	background-color: transparent;
	background-repeat: no-repeat;
}

a
{
  color:#DD3409;
  text-decoration:none; 
}

        .style3
        {
            width: 154px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <telerik:RadScriptManager ID="RadScriptManager1" runat="server">		
	</telerik:RadScriptManager>
    <div>
    <table><tr><td class="style3">
                                         <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" 
                                             Font-Size="10pt" ForeColor="#000099" Text="Set Grid Size for Snap:" 
                                        Width="157px" Height="16px"></asp:Label>
                            </td><td>
                                             <telerik:RadComboBox ID="SnapGridSize" runat="server" 
                                                 Font-Names="Arial" Font-Size="10pt"                                                  
                                                 Width="50px"  OnClientSelectedIndexChanged="parent.window.setSnapGridSize">  
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="5" Value="5"  />
                                                    <telerik:RadComboBoxItem Text="6" Value="6"  />  
                                                     <telerik:RadComboBoxItem Text="7" Value="7"  />
                                                     <telerik:RadComboBoxItem Text="8" Value="8"  />      
                                                     <telerik:RadComboBoxItem Text="9" Value="9"  />
                                                     <telerik:RadComboBoxItem Text="10" Value="10" />
                                                     <telerik:RadComboBoxItem Text="12" Value="12"  />  
                                                     <telerik:RadComboBoxItem Text="15" Value="15"  />  
                                                     <telerik:RadComboBoxItem Text="18" Value="18"  />  
                                                     <telerik:RadComboBoxItem Text="20" Value="20" />    
                                                </Items>
                                             </telerik:RadComboBox>&nbsp; pixels</td></tr><tr><td colspan="2">
                                <asp:Label ID="Message" runat="server" Font-Bold="True" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="Maroon" Height="42px" 
                Width="498px"></asp:Label>
                               </td></tr></table>
    </div>
    <asp:TextBox ID="DeviceType" runat="server" style="display:none"></asp:TextBox>
    </form>
</body>
</html>
