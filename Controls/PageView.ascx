<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageView.ascx.cs" Inherits="Controls_PageView" %>
<table>
<tr>
<td valign="top" align="left">
    <div>
    <asp:Image ID="PageImage" runat="server" BorderStyle="Groove" BorderWidth="2" BorderColor="Gray" Height="230" Width="160" ></asp:Image>
   </div>
</td>
<td valign="top" align="left">
  <telerik:RadTreeView ID="OnePageView" Runat="server" 
                             Skin="Web20"                             
                              ForeColor="Navy"  
                             ></telerik:RadTreeView>
</td></tr></table>