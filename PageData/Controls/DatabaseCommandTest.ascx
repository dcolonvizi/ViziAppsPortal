<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatabaseCommandTest.ascx.cs" Inherits="Controls_DatabaseCommandTest" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div>
<table><tr>
<td>
    <asp:Image ID="sql_image" runat="server" ImageUrl="~/images/database.gif" 
        ToolTip="Sql Command" /></td>
<td style="font-family:Verdana;font-size:12px" >
    <asp:Label ID="command_prefix" runat="server" Text=""></asp:Label>
</td>
<td>
    
<input id="command" type="text" style="width: 120px;" 
        runat="server"  
        readonly="readonly"   /></td>
<td style="font-family:Verdana;font-size:11px">worksheet</td>
<td width="175">
    
<input id="table" type="text" style="width: 184px;" 
        runat="server" 
        readonly="readonly"   /></td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
        <td>
            &nbsp;</td>
         <td>
             &nbsp;</td>
<td>
    &nbsp;</td>
</tr></table>
</div>