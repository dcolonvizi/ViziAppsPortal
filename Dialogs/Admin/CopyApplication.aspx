<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CopyApplication.aspx.cs" Inherits="CopyApplication" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


.label
{
   font-family:Verdana;
   font-size:12px;
   color:Black;
	margin-bottom: 0px;
}

        .textBox
        {}

        .message
        {
        	font-family:Verdana;
        	font-size:12px;
        	color:Maroon;
        	}
        .style1
        {
            height: 24px;
            width: 278px;
        }

    </style>
       <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
</head>
<body>
    <form id="form1" runat="server"> 
            <telerik:RadScriptManager ID="RadScriptManager12" runat="server"/>
            <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
       
     <telerik:AjaxSetting AjaxControlID="FromAccounts">
         <UpdatedControls>
           <telerik:AjaxUpdatedControl ControlID="FromAccounts" LoadingPanelID="CopyAppLoadingPanel"/>
             <telerik:AjaxUpdatedControl ControlID="Applications"/>
         </UpdatedControls>
       </telerik:AjaxSetting>
       </AjaxSettings>
       </telerik:RadAjaxManagerProxy>
       
		 <telerik:RadAjaxLoadingPanel ID="CopyAppLoadingPanel" runat="server" />

         <div>
        <table style="width: 708px">
            <tr>
                <td style="width: 5px; height: 24px;">
                    &nbsp;&nbsp;
                </td>
                <td align="center" class="label" style="width: 106px; height: 24px">
                    <strong>From:</strong></td>
                <td class="style1">
                    <telerik:RadComboBox ID="FromAccounts" runat="server" AutoPostBack="True" 
                        CssClass="label" OnSelectedIndexChanged="FromAccounts_SelectedIndexChanged" 
                        Width="155px" MarkFirstMatch="True">
                    </telerik:RadComboBox>
                </td>
                <td style="width: 317px; height: 24px">
                    &nbsp;</td>
                <td style="width: 317px; height: 24px">
                    <telerik:RadComboBox ID="Applications" runat="server" CssClass="label" MarkFirstMatch="True"
                        Width="250px">
                    </telerik:RadComboBox>
                </td>
                <td style="height: 24px">
                    &nbsp;&nbsp;
                </td>
                <td class="label" style="height: 24px">
                    <strong>To:</strong></td>
                <td style="width: 282px; height: 24px">
                    <telerik:RadComboBox ID="ToAccounts" runat="server" CssClass="label" Width="155px" MarkFirstMatch="True"
                    AutoPostBack="True" OnSelectedIndexChanged="ToAccounts_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td style="width: 5px; height: 24px;">
                    &nbsp;</td>
                <td align="center" class="label" style="width: 106px; height: 24px">
                    &nbsp;</td>
                <td class="style1">
                    &nbsp;</td>
                <td style="width: 317px; height: 24px">
                    &nbsp;</td>
                <td style="width: 317px; height: 24px">
                    &nbsp;</td>
                <td style="height: 24px">
                    &nbsp;</td>
                <td class="label" style="height: 24px">
                    &nbsp;</td>
                <td style="width: 282px; height: 24px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 5px; height: 43px;">
                </td>
                <td style="width: 106px; height: 43px">
                    <asp:Button ID="CopyApplicationButton" runat="server" CssClass="textBox" 
                        Text="Copy Application" 
                        ToolTip="Copy application from account to account" Visible="False" 
                        Width="115px" onclick="CopyApplicationButton_Click" />
                </td>
                <td align="center" colspan="6" style="height: 43px">
                    <asp:Label ID="Message" runat="server"  Width="546px" CssClass="message"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
