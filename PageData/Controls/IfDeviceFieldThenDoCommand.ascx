<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IfDeviceFieldThenDoCommand.ascx.cs" Inherits="Controls_IfDeviceFieldThenDoCommand" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .style2
    {
        width: 144px;
    }
    .style3
    {
        width: 77px;
    }
</style>
<div style=" font-family:Verdana; font-size:12px">
<table style="width: 607px"><tr>
<td>
    <asp:Image ID="sql_image" runat="server" ImageUrl="~/images/decision.gif" 
        ToolTip="Where Condition" /></td>
<td class="style3">
    If</td>
    <td>
    <asp:Image ID="sql_image0" runat="server" 
        ImageUrl="~/images/phone_field.png" ToolTip="Database Field Mapping" />
    </td>
<td>
<input id="command_condition_device_field1" type="text" style="width: 135px;" onkeydown="command_condition_device_field1_key_down(event);"
        runat="server"  
           />       
    </td>
    <td><telerik:RadComboBox ID="command_condition_operation" runat="server"  CssClass="body"
            Width="40px" 
             OnClientSelectedIndexChanged="command_condition_operation_SelectedIndexChanged" 
            >
       <Items>
            <telerik:RadComboBoxItem runat="server" Text="=" Value="=" 
                Owner="field_operation" />
            <telerik:RadComboBoxItem runat="server" Text="!=" Value="!=" 
                Owner="field_operation" />
       </Items>
    </telerik:RadComboBox></td>
    <td>
    <asp:Image ID="sql_image1" runat="server" 
        ImageUrl="~/images/phone_field.png" ToolTip="Database Field Mapping" /></td>
    <td class="style2">
    <input id="command_condition_device_field2" type="text" style="width: 135px;" 
        runat="server"  onkeydown="command_condition_device_field2_key_down(event);" />          
   </td>
   
    <td >       
    <telerik:RadButton ID="add_then_sql" runat="server" Text="then" OnClientClicked="command_condition_add_then_sql_click" Skin="Office2007"
         ToolTip="Add Then Command" Width="65px" >     
<Icon PrimaryIconUrl="~/images/add.gif" SecondaryIconUrl="~/images/database.gif"></Icon>
        </telerik:RadButton>
   </td>
     <td>       
    <telerik:RadButton ID="add_then_action" runat="server" Text="then" OnClientClicked="command_condition_add_then_action_click"
         ToolTip="Add Then Goto Page" Skin="Office2007" Width="65px" >        
<Icon PrimaryIconUrl="~/images/add.gif" SecondaryIconUrl="~/images/goto_page.gif"></Icon>
         </telerik:RadButton>
   </td>
   
    <td >
    <telerik:RadButton ID="add_else_command" runat="server"   ToolTip="Add Else Command" Text="else" Skin="Office2007"
            OnClientClicked="command_condition_add_else_sql_click" Width="65px">  
            <Icon PrimaryIconUrl="~/images/add.gif" SecondaryIconUrl="~/images/database.gif"></Icon>      
        </telerik:RadButton>
   </td>
   <td >
    <telerik:RadButton ID="add_else_action" runat="server"   ToolTip="Add Else Goto Page" Text="else" Skin="Office2007"
            OnClientClicked="command_condition_add_else_action_click" Width="65px">        
<Icon PrimaryIconUrl="~/images/add.gif" SecondaryIconUrl="~/images/goto_page.gif"></Icon>
       </telerik:RadButton>
   </td>
   
<td>
    <asp:ImageButton ID="delete_condition" runat="server" 
        ImageUrl="~/images/delete_small.gif"  
        ToolTip="Delete this Condition" onclientclick="delete_command_condition_click(this,null);"/></td>
</tr></table>
</div>


 