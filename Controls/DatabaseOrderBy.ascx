<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatabaseOrderBy.ascx.cs" Inherits="Controls_DatabaseOrderBy" %>
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
        <td style=" font-family:Verdana; font-size:12px; color:#333333">Order By database field:</td>
        <td>
    <asp:Image ID="sql_image0" runat="server" 
        ImageUrl="~/images/database_fields.png" ToolTip="Database Field Mapping" /></td>
<td class="style1">
    <telerik:RadComboBox ID="sort_field" runat="server" CssClass="body"
         OnClientSelectedIndexChanged="sort_field_SelectedIndexChanged"  />
    
</td>
<td>
 <telerik:RadComboBox ID="sort_direction" runat="server" CssClass="body"
         Width="150px" 
        OnClientSelectedIndexChanged="sort_direction_SelectedIndexChanged" >
        <Items>
            <telerik:RadComboBoxItem runat="server" Text="Ascending" Value="Ascending" 
                Owner="sort_direction" />
            <telerik:RadComboBoxItem runat="server" Text="Descending" Value="Descending" 
                Owner="sort_direction" />
            
        </Items>  
        </telerik:RadComboBox>
</td>
<td>
    <asp:ImageButton ID="delete_field" runat="server" 
        ImageUrl="~/images/delete_small.gif"  
        ToolTip="Delete This Sort" onclientclick="delete_order_by_click(this,null);"/></td>
</tr></table>
</div>
