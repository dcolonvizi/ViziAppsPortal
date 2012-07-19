<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatabaseToDeviceFieldTest.ascx.cs" Inherits="Controls_DatabaseToDeviceFieldTest" %>
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
        width: 119px;
    }
</style>
<div style=" font-family:Verdana; font-size:12px">
<table style="width: 634px"><tr>

        <td style=" font-family:Verdana; font-size:11px; color:#333333" class="style3">from worksheet field</td>
        <td>
    <asp:Image ID="sql_image" runat="server" 
        ImageUrl="~/images/database_fields.png" ToolTip="Database Field Mapping" /></td>
<td class="style1">
<input id="database_field" type="text" style="width: 177px;" 
        runat="server" readonly="readonly" /></td>
<td style=" font-family:Verdana; font-size:11px; color:#333333" align="right">to device field</td>
<td>
    <asp:Image ID="sql_image0" runat="server" 
        ImageUrl="~/images/phone_field.png" ToolTip="Database Field Mapping" /></td>
<td class="style2">
<input id="device_field" type="text" style="width: 177px;"
        runat="server" readonly="readonly" />
    </td>

</tr></table>
</div>