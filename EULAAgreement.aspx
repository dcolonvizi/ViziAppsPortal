<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EULAAgreement.aspx.cs" Inherits="EULAAgreement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>End User License Agreement</title>
    
<style type="text/css">
BODY { MARGIN-TOP: 0px;  MARGIN-LEFT: 0px }
form{
margin:0px;padding:0px;
}
a:link {
	color: #2C4DC2;
}
a:visited {
	color: #2C4DC2;
}
a:hover {
	color: #2C4DC2;
}
a:active {
	color: #2C4DC2;
}

.heading
{
   font-family:Verdana;
   font-size:12px;
   color:White;
}

</style>
 <script  language="javascript" type="text/javascript" src="scripts/google_analytics_1.0.js"></script>
 <script type="text/javascript">
     function GetRadWindow() {
         var oWindow = null;
         if (window.radWindow) oWindow = window.radWindow;
         else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
         return oWindow;
     }
     function CloseWindow() {
         GetRadWindow().close();
     }
     function AgreedToEula() {
         GetRadWindow().close('agreed');
     }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="AgreeButton">
            <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="UserLabel" > </telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="SupportButton" > </telerik:AjaxUpdatedControl>
                   <telerik:AjaxUpdatedControl ControlID="MultiPage1" LoadingPanelID="MultiPage1LoadingPanel"> </telerik:AjaxUpdatedControl>
           </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="RejectButton">
            <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="UserLabel" > </telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="SupportButton" > </telerik:AjaxUpdatedControl>
                   <telerik:AjaxUpdatedControl ControlID="MultiPage1" LoadingPanelID="MultiPage1LoadingPanel"> </telerik:AjaxUpdatedControl>
         </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<div>
	              <div align="center" id="header" style="height:60px;  background-color:#000099;">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                
               
                <td>
                <asp:Image ID="HeaderImage" ImageUrl="~/images/logo_header_300.png" runat="server">
                                                            </asp:Image>
                </td>
               
                <td class="style45">
                   
                   
                    
                    
                    </td>
                     <td style="color:White;"></td>
               
                <td align="center">
                </td>
                <td style="color:White;"></td>
                <td class="heading" align="center">
                    &nbsp;</td>
                <td>
                </td>
                </tr>
                </table>
                </div>  
</div>
<div style="height:10px">
</div>
   <div> 
<table width="100%" height="420"  border="0" cellpadding="0" cellspacing="0" style="background-color:white">
  
                          <tr valign="top">
    <td width="20%" height="400">&nbsp;</td>
    <td width="61%">
      <table width="100%"  border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td valign="top">
            <table width="100%"  border="0" cellspacing="0" cellpadding="0" 
                  style="height: 417px">
              <tr valign="top">
                <td width="485" bgcolor="#FFFFFF" style="height: 384px" align="center">
                    <table bgcolor="#FFFFFF" border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                        <tr>
                            <td align="left" style="width: 444px; height: 4px" valign="top">
                                <asp:Label ID="Instructions" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Black" Width="785px"></asp:Label><br />
                                <br />
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 444px; height: 115px" valign="top">
                                <asp:TextBox ID="AgreementTextBox" runat="server" Height="315px" TextMode="MultiLine" Width="784px" Font-Names="Arial" Font-Size="10pt" ForeColor="#404040"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 444px; height: 55px" valign="middle">
                                <asp:Button ID="AgreeButton" runat="server" 
                                    Text="I Understand and Accept" Width="163px" OnClick="AgreeButton_Click" />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                <asp:Button ID="RejectButton" runat="server" 
                                     Text="I Do Not Accept" OnClick="RejectButton_Click" /></td>
                        </tr>
                    </table>
                </td>
              </tr>
            </table>

              </td></tr>
      </table></td>
    <td bordercolor="#FFFFFF" style="width: 45%">&nbsp;</td>
  </tr>
</table>
</div>

    </form>
</body>
</html>
