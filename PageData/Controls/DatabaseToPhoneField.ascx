<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatabaseToPhoneField.ascx.cs" Inherits="Controls_DatabaseToPhoneField" %>
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
<table style="width: 590px"><tr>

        <td style=" font-family:Verdana; font-size:11px; color:#333333" class="style3">from worksheet field</td>
        <td>
    <asp:Image ID="sql_image" runat="server" 
        ImageUrl="~/images/database_fields.png" ToolTip="Database Field Mapping" /></td>
<td class="style1">
    <telerik:RadComboBox ID="database_field" runat="server" CssClass="body"
         OnClientSelectedIndexChanged="database_field_SelectedIndexChanged"  />
    
</td>
<td style=" font-family:Verdana; font-size:11px; color:#333333">to device field</td>
<td>
    <asp:Image ID="sql_image0" runat="server" 
        ImageUrl="~/images/phone_field.png" ToolTip="Database Field Mapping" /></td>
<td class="style2">
<input id="phone_field" type="text" style="width: 135px;" onkeydown="phone_field_key_down(event);"
        runat="server" />
    </td>

<td>
    <asp:ImageButton ID="delete_field" runat="server" 
        ImageUrl="~/images/delete_small.gif"  
        ToolTip="Delete This Field" onclientclick="delete_field_click(this,null);"/></td>
</tr></table>
</div>