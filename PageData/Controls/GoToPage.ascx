<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GoToPage.ascx.cs" Inherits="Controls_GoToPage" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .style1
    {
        width: 4px;
    }
</style>
<div>
<table><tr>
<td>
    <asp:Image ID="sql_image" runat="server" ImageUrl="~/images/goto_page.gif" 
        ToolTip="Sql Command" /></td>
<td style="font-family:Verdana;font-size:12px">
    <asp:Label ID="command_prefix" runat="server" Text=""></asp:Label>
</td>
<td>
    <telerik:RadComboBox ID="command" runat="server" 
         Width="85px" OnClientSelectedIndexChanged="command_SelectedIndexChanged" >
        <Items>
           <telerik:RadComboBoxItem runat="server" Text="Go To Page" Value="go to page" />    
        </Items>
    </telerik:RadComboBox>
</td>
<td width="175">
    <telerik:RadComboBox ID="gotopage" runat="server" 
         OnClientSelectedIndexChanged="page_SelectedIndexChanged" Width="200px" >
    </telerik:RadComboBox></td>
<td class="style1">
    <asp:ImageButton ID="delete_command" runat="server" 
        ImageUrl="~/images/delete_small.gif"  
        ToolTip="Delete this Command" OnClientClick="delete_command_click(this,null);" /></td>
</tr></table>
</div>