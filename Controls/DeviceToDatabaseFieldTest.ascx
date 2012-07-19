<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DeviceToDatabaseFieldTest.ascx.cs" Inherits="Controls_DeviceToDatabaseFieldTest" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .style1
    {
        width: 158px;
    }
    #device_field
    {
        width: 187px;
    }
</style>
<div style=" font-family:Verdana; font-size:12px">
<table style="width: 668px"><tr>

        <td style=" font-family:Verdana; font-size:11px; color:#333333">from device field 
            value</td>
            <td>
    <asp:Image ID="sql_image" runat="server" 
        ImageUrl="~/images/phone_field.png" ToolTip="Database Field Mapping" /></td>
<td class="style1">
   <input ID="device_field" runat="server"  />
    
</td>
<td style=" font-family:Verdana; font-size:11px; color:#333333" align="right">to database field</td>
<td>
    <asp:Image ID="sql_image0" runat="server" 
        ImageUrl="~/images/database_fields.png" ToolTip="Database Field Mapping" /></td>
<td width="150">
    
    <telerik:RadComboBox ID="database_field" runat="server" CssClass="body"
         OnClientSelectedIndexChanged="database_field_SelectedIndexChanged"  />
<td>
    &nbsp;</td>
</tr></table>
</div>


 