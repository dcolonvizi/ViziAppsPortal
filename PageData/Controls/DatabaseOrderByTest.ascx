<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatabaseOrderByTest.ascx.cs" Inherits="Controls_DatabaseOrderByTest" %>
<style type="text/css">

    .style1
    {
        width: 158px;
    }
</style>
<div style=" font-family:Verdana; font-size:12px">
<table><tr>
<td>
    <asp:Image ID="sql_image" runat="server" 
        ImageUrl="~/images/order_by.png" ToolTip="Database Field" /></td>
        <td style=" font-family:Verdana; font-size:11px; color:#333333">Order By worksheet field:</td>
        <td>
    <asp:Image ID="sql_image0" runat="server" 
        ImageUrl="~/images/database_fields.png" ToolTip="Database Field Mapping" /></td>
<td class="style1">
    
<input id="sort_field" type="text" style="width: 184px;" 
        runat="server" 
        readonly="readonly"   /></td>
<td>
    
<input id="sort_direction" type="text" style="width: 184px;" 
        runat="server" 
        readonly="readonly"   /></td>
<td>
    &nbsp;</td>
</tr></table>
</div>
