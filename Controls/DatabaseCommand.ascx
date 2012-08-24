<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatabaseCommand.ascx.cs" Inherits="Controls_DatabaseCommand" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .style2
    {
        width: 30px;
    }
</style>
<div>
<table style="width: 490px"><tr>
<td>
    <asp:Image ID="sql_image" runat="server" ImageUrl="~/images/sql.png" 
        ToolTip="Sql Command" /></td>
<td style="font-family:Verdana;font-size:12px">
    <asp:Label ID="command_prefix" runat="server" Text=""></asp:Label>
</td>
<td>
    <telerik:RadComboBox ID="command" runat="server" 
         Width="90px" OnClientSelectedIndexChanged="command_SelectedIndexChanged" >
        <Items>
           <telerik:RadComboBoxItem runat="server" Text="Select From" 
                Value="select from" />
            <telerik:RadComboBoxItem runat="server" Text="Insert Into" 
                Value="insert into" />
            <telerik:RadComboBoxItem runat="server" Text="Update" Value="update" />
             <telerik:RadComboBoxItem runat="server" Text="Delete From" 
                Value="delete from" />
        </Items>
    </telerik:RadComboBox>
</td>
<td width="175" align="left">
    <telerik:RadComboBox ID="table" runat="server" 
         OnClientSelectedIndexChanged="table_SelectedIndexChanged" Width="200px" >
    </telerik:RadComboBox></td>
<td class="style2">
    <asp:ImageButton ID="add_field" runat="server" ImageUrl="~/images/add_field.png" 
        OnClientClick="add_field_click(this,null);" ToolTip="Add a Field"/></td>
<td class="style2">
    <asp:ImageButton ID="add_condition" runat="server" 
        ImageUrl="~/images/add_filter.png" OnClientClick="add_condition_click(this,null);"
        ToolTip="Add a Where Condition" Width="18px" /></td>
        <td class="style2">
         <asp:ImageButton ID="add_order_by" runat="server" 
        ImageUrl="~/images/add_order_by.png" OnClientClick="add_order_by_click(this,null);"
        ToolTip="Add Order By" Width="18px" />
        </td>
        <td class="style2">
         <asp:ImageButton ID="test_query" runat="server" 
        ImageUrl="~/images/check.png" OnClientClick="test_query_click(this,null);"
        ToolTip="Test Command" Width="19px" />
        </td>
<td>
    <asp:ImageButton ID="delete_command" runat="server" 
        ImageUrl="~/images/delete_small.gif"  
        ToolTip="Delete this Command" OnClientClick="delete_command_click(this,null);" /></td>
</tr></table>
</div>