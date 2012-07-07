<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatabaseWhere.ascx.cs" Inherits="Controls_DatabaseWhere" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div>
<table style="width: 558px"><tr>
<td>
    <asp:Image ID="sql_image" runat="server" ImageUrl="~/images/filter.png" 
        ToolTip="Where Condition" /></td>
<td>
    <telerik:RadComboBox ID="condition_operation" runat="server" CssClass="body"
         Width="100px" 
        OnClientSelectedIndexChanged="condition_operation_SelectedIndexChanged" >
        <Items>
            <telerik:RadComboBoxItem runat="server" Text="And" Value="And" 
                Owner="where_operation" />
            <telerik:RadComboBoxItem runat="server" Text="Or" Value="Or" 
                Owner="where_operation" />
            <telerik:RadComboBoxItem runat="server" Text="And Not" Value="And Not" 
                Owner="where_operation" />
            <telerik:RadComboBoxItem runat="server" Text="Or Not" Value="Or Not" 
                Owner="where_operation" />
        </Items>
    </telerik:RadComboBox>
</td>
<td>
    <asp:Image ID="sql_image1" runat="server" 
        ImageUrl="~/images/database_fields.png" ToolTip="Database Field Mapping" /></td>
<td><telerik:RadComboBox ID="condition_1st_field" runat="server" CssClass="body"
        OnClientSelectedIndexChanged="condition_1st_field_SelectedIndexChanged"  />
    </td>
    <td><telerik:RadComboBox ID="field_operation" runat="server"  CssClass="body"
            Width="60px" 
             OnClientSelectedIndexChanged="field_operation_SelectedIndexChanged" 
            >
       <Items>
            <telerik:RadComboBoxItem runat="server" Text="=" Value="=" />
            <telerik:RadComboBoxItem runat="server" Text="!=" Value="!=" />
            <telerik:RadComboBoxItem runat="server" Text="<" Value="<" />
            <telerik:RadComboBoxItem runat="server" Text="&lt;=" Value="&lt;=" />
            <telerik:RadComboBoxItem runat="server" Text=">" Value=">" />
            <telerik:RadComboBoxItem runat="server" Text="&gt;=" Value="&gt;=" />
            <telerik:RadComboBoxItem runat="server" Text="like" Value="like" />
            <telerik:RadComboBoxItem runat="server" Text="not like" Value="not like" />
        </Items>
    </telerik:RadComboBox></td>
    <td>
    <asp:Image ID="sql_image0" runat="server" 
        ImageUrl="~/images/phone_field.png" ToolTip="Database Field Mapping" /></td>
    <td width="170">
    <input id="condition_2nd_field" type="text" style="width: 135px;" onkeydown="condition_2nd_field_key_down(event);"
        runat="server"   />      
   </td>
<td>
    <asp:ImageButton ID="delete_condition" runat="server" 
        ImageUrl="~/images/delete_small.gif"  
        ToolTip="Delete this Condition" onclientclick="delete_condition_click(this,null);"/></td>
</tr></table>
</div>