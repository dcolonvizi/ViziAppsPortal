<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoogleSpreadsheetDataSourceWithLink.aspx.cs" Inherits="Dialogs_GoogleSpreadsheetDataSourceWithLink" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ViziApps Interface to Google Docs</title>
    <style type="text/css">
    body
    {
        font-family:Verdana;
        font-size:12px;
    }
        .style5
        {
            height: 35px;
            width: 690px;
        }
        .style8
        {
            width: 149px;
        }
        .style9
        {
            height: 35px;
            width: 13px;
        }
        .style12
        {
            width: 13px;
        }
            
.RadComboBox_Default
{
	font: 12px "Segoe UI", Arial, sans-serif;
	color: #333;
}

.RadComboBox
{
	vertical-align: middle;
}

.RadComboBox
{
	text-align: left;
}

.RadComboBox_Default
{
	font: 12px "Segoe UI", Arial, sans-serif;
	color: #333;
}

.RadComboBox
{
	vertical-align: middle;
}

.RadComboBox
{
	text-align: left;
}

.RadComboBox_Default
{
	font: 12px "Segoe UI", Arial, sans-serif;
	color: #333;
}

.RadComboBox
{
	vertical-align: middle;
}

.RadComboBox
{
	text-align: left;
}

.RadComboBox *
{
	margin: 0;
	padding: 0;
}

.RadComboBox *
{
	margin: 0;
	padding: 0;
}

.RadComboBox *
{
	margin: 0;
	padding: 0;
}

.RadComboBox_Default .rcbInputCellLeft
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2010.1.519.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbInputCellLeft
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox_Default .rcbInputCellLeft
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2010.1.519.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbInputCellLeft
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox_Default .rcbInputCellLeft
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2010.1.519.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbInputCellLeft
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox .rcbReadOnly .rcbInput
{
	cursor: default;
}

.RadComboBox .rcbReadOnly .rcbInput
{
	cursor: default;
}

.RadComboBox .rcbReadOnly .rcbInput
{
	cursor: default;
}

.RadComboBox_Default .rcbInput
{
	font: 12px "Segoe UI", Arial, sans-serif;
	color: #333;
}

.RadComboBox .rcbInput
{
	text-align: left;
}

.RadComboBox_Default .rcbInput
{
	font: 12px "Segoe UI", Arial, sans-serif;
	color: #333;
}

.RadComboBox .rcbInput
{
	text-align: left;
}

.RadComboBox_Default .rcbInput
{
	font: 12px "Segoe UI", Arial, sans-serif;
	color: #333;
}

.RadComboBox .rcbInput
{
	text-align: left;
}

.RadComboBox_Default .rcbArrowCellRight
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2010.1.519.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbArrowCellRight
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox_Default .rcbArrowCellRight
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2010.1.519.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbArrowCellRight
{
	background-color: transparent;
	background-repeat: no-repeat;
}

.RadComboBox_Default .rcbArrowCellRight
{
	background-image: url('mvwres://Telerik.Web.UI, Version=2010.1.519.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
}

.RadComboBox .rcbArrowCellRight
{
	background-color: transparent;
	background-repeat: no-repeat;
}

        .style14
        {
        }

        .style15
        {
            width: 104px;
        }
        .style16
        {
            width: 71px;
        }
        
        .style18
        {
            width: 166px;
        }
        
        </style>
</head>
<body style="width: 855px; height: 441px">
    <form id="form2" runat="server">
    	<telerik:RadScriptManager ID="RadScriptManagerDB" runat="server"/>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>        
              <telerik:AjaxSetting AjaxControlID="SaveDatabaseInfo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="SaveGoogleDocsInfoMessage" LoadingPanelID="GDocsLoadingPanel"  />    
                        <telerik:AjaxUpdatedControl ControlID="Status" />                     
                    </UpdatedControls>
                </telerik:AjaxSetting>
                </AjaxSettings>
        </telerik:RadAjaxManager>

<script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close(sender, eventArgs) {
            var Status = document.getElementById("Status");
           var customArg =  Status.value;
           GetRadWindow().close(customArg);
        }
        </script>
	
         <telerik:RadWindowManager ID="RadWindowManagerDB" runat="server">
            <Windows>
                <telerik:RadWindow 
                    id="HelpGetDatabaseInfoWindow" 
                    runat="server"
                    showcontentduringload="false"
                    title="Help on Getting Database Info"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>
                
            </Windows>
            </telerik:RadWindowManager>
             <telerik:RadAjaxLoadingPanel ID="GDocsLoadingPanel" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
             <telerik:RadMultiPage ID="ContentMultiPage" runat="server" 
            SelectedIndex="0" Width="110px">
            <telerik:RadPageView ID="MobiFlexAccountType" runat="server">
    <div style=" font-family:Verdana; font-size:12px;">
   
     <table width="850"><tr><td   valign="bottom" colspan="4"><table><tr>
         <td class="style15">Data Source ID</td><td>
         <asp:TextBox ID="DataSourceID" runat="server" Width="194px"></asp:TextBox>
         </td></tr></table></td>
         <td  valign="top">
             <asp:ImageButton ID="GetGoogleDocsInfoHelp" runat="server" 
                 ImageUrl="~/images/help.gif" ToolTip="How is this info used?" />
         </td></tr>
         <tr>
             <td colspan="4" valign="bottom">
                 Enter your Google Docs account <strong>Username</strong> and <strong>Password</strong> 
                 and then click <strong>Get Spreadsheets</strong>. Select a spreadsheet for this 
                 app.</td>
             <td valign="top">
                 &nbsp;</td>
         </tr>
         <tr><td height="35" >
         <table ><tr><td>Username:&nbsp; </td><td>
        <asp:TextBox ID="Username" runat="server" Width="140px"></asp:TextBox></td><td>Password:</td>
             <td width="150px">
        <asp:TextBox ID="Password" runat="server" Width="106px" TextMode="Password"></asp:TextBox>
             </td></tr></table>
         </td>
             <td height="35">
                 <asp:Button ID="GetSpreadsheets" runat="server" 
                     Text="Get Spreadsheets" Width="134px" onclick="GetSpreadsheets_Click" />
             </td>
             <td height="35">
                 spreadsheet:</td>
             <td height="35">
                 <telerik:RadComboBox ID="AccountSpreadsheets" runat="server" 
                     AutoPostBack="True" />
             </td>
             <td height="35" >
                </td></tr>
                 
                 </table>
                 </div>
                  </telerik:RadPageView>
         <telerik:RadPageView ID="GoogleAppsAccountType" runat="server">
    <div style=" font-family:Verdana; font-size:12px;">
     <table style="width: 690px"><tr><td  class="style5" valign="bottom">Select your 
         Google Docs 
         <strong>Spreadsheet</strong> for this app. Select a spreadsheet for this app<br /> and 
         then click <strong>Save</strong>. Wait for a success message before closing.</td>
         <td class="style9" valign="top">
      <asp:ImageButton ID="GetGoogleDocsInfoHelp2" runat="server" ImageUrl="~/images/help.gif" 
        ToolTip="How is this info used?" />

         </td></tr><tr><td height="35">
         <table style="width: 655px"><tr><td align="right">Spreadsheet:</td><td style="width:5px;"></td><td>
             <telerik:RadComboBox ID="Spreadsheets" runat="server" AutoPostBack="True" 
                 Width="250px" />                
             </td><td>&nbsp; </td><td>
             &nbsp;</td><td>&nbsp;</td><td>
             &nbsp;</td></tr></table>
         </td><td height="35" class="style12">
                 &nbsp;</td></tr></table>
         </div>
         </telerik:RadPageView>
         </telerik:RadMultiPage>
         <div>
   
     <table width="850">
         <tr>
             <td valign="bottom">
                 Go to your Google Docs account, open the spreadsheet you want to use, click on 
                 the &quot;Share&quot; button and copy the URL to the field below. Then click 
                 <strong>Save</strong>. Wait for a success message before 
                 closing.</td>
         </tr>
         <tr><td height="35" >
         <table style="width: 828px" ><tr>
             <td class="style18">Google Spreadsheet Link&nbsp; </td>
             <td>
        <asp:TextBox ID="SpreadsheetLink" runat="server" Width="644px"></asp:TextBox></td></tr></table>
         </td>
             </tr>
                 
                 </table>
                 </div>
         <div>
                 <table style="width: 849px; height: 43px;"><tr><td class="style16">
                     &nbsp;</td><td class="style16">
    <asp:Button runat="server" ID="SaveDatabaseInfo" Text="Save" onclick="SaveDatabaseInfo_Click"  
 /></td><td class="style14">
                <asp:Label ID="SaveGoogleDocsInfoMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="306px" 
                     style="margin-bottom: 0px"></asp:Label>
                        </td><td class="style8">
                         &nbsp;</td><td>
        
                    <asp:TextBox ID="Status" runat="server" style="display:none" />
             </td></tr></table>
         </div>        
    </form>
</body>
</html>
