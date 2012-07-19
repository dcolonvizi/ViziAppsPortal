<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DeviceToDatabaseFieldTest.ascx.cs" Inherits="Controls_DeviceToDatabaseFieldTest" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .style1
    {
        width: 158px;
    }
</style>
<div style=" font-family:Verdana; font-size:12px">
<table style="width: 662px"><tr>

        <td style=" font-family:Verdana; font-size:11px; color:#333333">from device field</td>
            <td>
    <asp:Image ID="sql_image" runat="server" 
        ImageUrl="~/images/phone_field.png" ToolTip="Database Field Mapping" /></td>
<td class="style1">
<input id="device_field" type="text" style="width: 184px;" 
        runat="server" onkeydown="phone_field_key_down(event);"   /></td>
<td style=" font-family:Verdana; font-size:11px; color:#333333" align="right">to worksheet field</td>
<td>
    <asp:Image ID="sql_image0" runat="server" 
        ImageUrl="~/images/database_fields.png" ToolTip="Database Field Mapping" /></td>
<td width="150">
    
<input id="database_field" type="text" style="width: 184px;" 
        runat="server" 
        readonly="readonly"   /></td>
</tr></table>
</div>


 