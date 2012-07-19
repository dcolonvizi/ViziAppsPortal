<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpreadsheetWhereTest.ascx.cs" Inherits="Controls_SpreadsheetWhereTest" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div>
<table style="width: 558px"><tr>
<td>
    <asp:Image ID="sql_image" runat="server" ImageUrl="~/images/filter.png" 
        ToolTip="Where Condition" /></td>
<td>
    <input id="condition_operation" type="text" style="width: 110px;" 
        runat="server" readonly="readonly"   /></td>
<td>
    <asp:Image ID="sql_image1" runat="server" 
        ImageUrl="~/images/database_fields.png" ToolTip="Database Field Mapping" /></td>
<td>
    <input id="condition_1st_field" type="text" style="width: 172px;" 
        runat="server" readonly="readonly"   /></td>
    <td>
    <input id="field_operation" type="text" style="width: 44px;" 
        runat="server" readonly="readonly"   /></td>
    <td>
    <asp:Image ID="sql_image0" runat="server" 
        ImageUrl="~/images/phone_field.png" ToolTip="Database Field Mapping" /></td>
    <td width="170">
    <input id="condition_2nd_field" type="text" style="width: 172px;" 
        runat="server"   />      
   </td>
</tr></table>
</div>