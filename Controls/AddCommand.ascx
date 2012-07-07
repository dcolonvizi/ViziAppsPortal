<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddCommand.ascx.cs" Inherits="Controls_AddCommand" %>
<div>
<table><tr><td>
    <asp:ImageButton ID="AddIfCondition" runat="server" 
            ImageUrl="~/images/add_command_condition.png" 
             OnClientClick="add_command_condition_click(this,null)" />
</td><td>
 </td><td>
 <asp:ImageButton ID="AddCommand" runat="server" 
        ImageUrl="~/images/add_command.png" OnClientClick="add_command_click(this,null)" />
            </td></tr></table>
</div>