<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SQLDatabaseDataSource.aspx.cs" Inherits="Dialogs_SQLDatabaseDataSource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New SQL Database Data Source Configuration</title>
    <style type="text/css">

td
{
	MARGIN-TOP: 0px;  
	MARGIN-LEFT: 0px; 
	font-family:Verdana; 
	font-size:12px ;
	color:#333333;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">   
                     <div><table style="font-family:verdana;width: 768px; height: 43px;">
                     <tr><td colspan="7"> Support for SQL Database Web Services coming in a few weeks.</td></tr>
                     <tr>
                         <td align="right" 
                                 class="style96"><div>
                 </div></td><td>&#160;&#160;</td><td align="left">
                 &nbsp;</td><td>
                     <asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                           Font-Names="Arial" Font-Size="10pt" OnClientClick="getDatabaseInfo(this)"
                                   Text="Upload Database Info" Width="145px" /></td>
                             <td align="right">
                                 <asp:Button ID="ViewConnectionString" 
                                     runat="server" CausesValidation="False" 
                                           Font-Names="Arial" Font-Size="10pt" OnClientClick="showViewConnections();" 
                                           Text="DB Connections" Visible="False" Width="118px" /></td>
                             <td 
                                       align="right">
                                 <asp:Button ID="DBWebService" 
                                     runat="server" CausesValidation="False" 
                                           Font-Names="Arial" Font-Size="10pt" OnClientClick="var oWin=radopen('Dialogs/ManageData/DBWebService.aspx', 'WebserviceURLHelp'); 
                                                            oWin.setSize(650,250);
                                                             oWin.set_visibleTitlebar(false);
                                                                   oWin.set_visibleStatusbar(false);
                                                                   oWin.set_modal(true); 
                                                                    oWin.moveTo(300, 200); 
                                                                     oWin.add_close(onWebServiceURLClientClose);
                                                                return false;" Text="DB Web Service" 
                                     Width="116px" /></td>
                             <td 
                                       align="right">
                                 <asp:ImageButton ID="DatabaseHelpButton" runat="server" 
                                           ImageUrl="~/images/help.gif" OnClientClick="var oWin=radopen('Help/PageData/DatabaseHelp.htm', 'DatabaseHelp'); 
                                                        oWin.setSize(850,500);
                                                                 oWin.set_visibleTitlebar(true);
                                                                       oWin.set_visibleStatusbar(false);
                                                                       oWin.set_modal(true); 
                                                                        oWin.moveTo(400, 200); 
                                                        return false;" 
                                           ToolTip="How do I manage data with my database?" /></td></tr></table></div>
    </form>
</body>
</html>
