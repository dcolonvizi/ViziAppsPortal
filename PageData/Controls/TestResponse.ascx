<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestResponse.ascx.cs" Inherits="Controls_TestResponse" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .style1
    {
        width: 158px;
    }
    .style2
    {
        width: 143px;
    }
    .style3
    {
        width: 69px;
    }
    .style4
    {
        width: 48px;
    }
    .style5
    {
        width: 18px;
    }
    </style>
<div style=" font-family:Verdana; font-size:12px">
<table style="width: 750px"><tr>

        <td style=" font-family:Verdana; font-size:11px; color:#333333" class="style3" 
            valign="top">
            Device Field</td>
        <td class="style5" valign="top">
    <asp:Image ID="sql_image1" runat="server" 
        ImageUrl="~/images/phone_field.png" ToolTip="Database Field Mapping" /></td>
<td class="style1" valign="top">
<input id="device_field" type="text" style="width: 150px;" 
        runat="server" readonly="readonly" /></td>
<td style=" font-family:Verdana; font-size:11px; color:#333333" align="right" 
            class="style4" valign="top">Value(s) </td>
<td class="style2">
<input id="device_field_value" type="text" style="width: 420px;" 
        runat="server" readonly="readonly" />
    </td>

</tr></table>
</div>