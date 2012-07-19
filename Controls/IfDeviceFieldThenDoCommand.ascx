<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IfDeviceFieldThenDoCommand.ascx.cs" Inherits="Controls_IfDeviceFieldThenDoCommand" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .style1
    {
        width: 60px;
    }
</style>
<div style=" font-family:Verdana; font-size:12px">
<table><tr>
<td>
    <asp:Image ID="sql_image" runat="server" ImageUrl="~/images/condition.png" 
        ToolTip="Where Condition" /></td>
<td>
    If 
    device field</td>
    <td>
    <asp:Image ID="sql_image0" runat="server" 
        ImageUrl="~/images/phone_field.png" ToolTip="Database Field Mapping" />
    </td>
<td><telerik:RadComboBox ID="command_condition_device_field1" runat="server" CssClass="body"
        OnClientSelectedIndexChanged="command_condition_device_field1_SelectedIndexChanged"  />
    </td>
    <td><telerik:RadComboBox ID="command_condition_operation" runat="server"  CssClass="body"
            Width="60px" 
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
    <td width="170">
     <telerik:RadComboBox ID="command_condition_device_field2" runat="server"  CssClass="body"
        OnClientSelectedIndexChanged="command_condition_device_field2_SelectedIndexChanged" 
        OnClientTextChange="command_condition_device_field2_SelectedIndexChanged" 
        EmptyMessage="Choose Field or Enter Text"  
        AllowCustomText="true">     </telerik:RadComboBox>        
   </td>
   
    <td>
    <telerik:RadButton ID="add_then_sql" runat="server" Text="then" OnClientClicked="command_condition_add_then_sql_click" Skin="Office2007"
         ToolTip="Add Then Command" Width="65px" >     
<Icon PrimaryIconUrl="~/images/add.gif" SecondaryIconUrl="~/images/database.gif"></Icon>
        </telerik:RadButton>
           
   </td>
   
    <td >
    <telerik:RadButton ID="add_else_command" runat="server"   ToolTip="Add Else Command" Text="else" Skin="Office2007"
            OnClientClicked="command_condition_add_else_sql_click" Width="65px">  
            <Icon PrimaryIconUrl="~/images/add.gif" SecondaryIconUrl="~/images/database.gif"></Icon>      
        </telerik:RadButton>      
   </td>
   
<td>
    <asp:ImageButton ID="delete_condition" runat="server" 
        ImageUrl="~/images/delete_small.gif"  
        ToolTip="Delete this Condition" onclientclick="delete_command_condition_click(this,null);"/></td>
</tr></table>
</div>


 