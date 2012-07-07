<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBWebService.aspx.cs" Inherits="Help_DBWebService" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ViziApps Database Web Service URL</title>
    <style type="text/css">

        .style5
        {
            height: 35px;
            width: 341px;
        }
        .style4
        {
            height: 44px;
            width: 341px;
        }
        .style8
        {
            width: 149px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    	<telerik:RadScriptManager ID="RadScriptManagerWS" runat="server"/>

<script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close(sender, eventArgs) {
           var customArg = document.getElementById("WebServiceURL").value;
           GetRadWindow().close(customArg);
        }
        </script>
	
     <telerik:RadProgressManager ID="RadprogressmanagerWS" runat="server" />
         <telerik:RadWindowManager ID="RadWindowManagerWS" runat="server">
            <Windows>
                <telerik:RadWindow 
                    id="HelpGetWebServiceInfoWindow" 
                    runat="server"
                    showcontentduringload="false"
                    title="Help on Getting WebService Info"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>
                
            </Windows>
            </telerik:RadWindowManager>

    <div style=" font-family:Verdana; font-size:12px;">
     <table style="width: 582px" width="582"><tr><td class="style5" valign="bottom">
			<span class="body">To download the 
            free ViziApps Database Web Service <a 
            href="../../Help/ManageData/MobiFlexDatabase.zip">click here</a></span></td></tr><tr><td class="style5" valign="bottom">
             Enter the URL of 
             the 
         MobiFlex
			<span class="body">Database </span>Web Service on your server:</td></tr><tr><td height="35">
        <asp:TextBox ID="WebServiceURL" runat="server" Width="560px"></asp:TextBox>
         </td></tr><tr><td class="style4">
         <table style="width: 328px; height: 67px;"><tr><td class="style8">
    <asp:Button runat="server" ID="SaveWebServiceInfo" Text="Save" onclick="SaveWebServiceInfo_Click"  
 /></td><td align="right">
                    <asp:Button ID="Cancel" runat="server" Text="Close" 
                     OnClientClick="Close();return false;"  />
                        </td><td>
                    &nbsp;</td></tr><tr><td colspan="3">
                <asp:Label ID="SaveWebServiceInfoMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="306px"></asp:Label>
                        </td></tr></table>
         </td></tr></table>
         </div>
    </form>  
</body>
</html>
