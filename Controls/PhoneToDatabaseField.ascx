<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PhoneToDatabaseField.ascx.cs" Inherits="Controls_PhoneToDatabaseField" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .style1
    {
        width: 158px;
    }
</style>
<div style=" font-family:Verdana; font-size:12px">
<table><tr>

        <td style=" font-family:Verdana; font-size:11px; color:#333333">from device field or 
            custom text</td>
            <td>
    <asp:Image ID="sql_image" runat="server" 
        ImageUrl="~/images/phone_field.png" ToolTip="Database Field Mapping" /></td>
<td class="style1">
   <telerik:RadComboBox ID="phone_field" runat="server" CssClass="body"
         OnClientSelectedIndexChanged="phone_field_SelectedIndexChanged"  
         OnClientTextChange="phone_field_SelectedIndexChanged"   
         AllowCustomText="true" />
    
</td>
<td style=" font-family:Verdana; font-size:11px; color:#333333">to database field</td>
<td>
    <asp:Image ID="sql_image0" runat="server" 
        ImageUrl="~/images/database_fields.png" ToolTip="Database Field Mapping" /></td>
<td width="150">
    
    <telerik:RadComboBox ID="database_field" runat="server" CssClass="body"
         OnClientSelectedIndexChanged="database_field_SelectedIndexChanged"  />
<td>
    <asp:ImageButton ID="delete_field" runat="server" 
        ImageUrl="~/images/delete_small.gif"  
        ToolTip="Delete This Field" onclientclick="delete_field_click(this,null);"/></td>
</tr></table>
</div>


 