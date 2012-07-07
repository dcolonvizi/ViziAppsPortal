<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetCustomHeaderHtml.aspx.cs"  validateRequest="false" Inherits="Dialogs_SetCustomHeaderHtml" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Set Custom Header HTML</title>
      <script type="text/javascript">
          var MyPopUpWindow;
          function NamedPopUp(url, name, features) {
              try {
                  if (isMyPopUpWindowOpen()) {
                      closeMyPopUpWindow();
                  }
                  MyPopUpWindow = window.open(url, name, features);
                  if (MyPopUpWindow == null) {
                      alert('For correct operation, popups need to be allowed from this website.');
                      return;
                  }
              }
              catch (err) {
                  var txt = "There was an error on this page.\n\n";
                  txt += "Error description: " + err.description + "\n\n";
                  txt += "Click OK to continue.\n\n";
                  alert(txt);
              }
          }
          function isMyPopUpWindowOpen() {
              return (MyPopUpWindow && MyPopUpWindow.value != '' && !MyPopUpWindow.closed);
          }
          function closeMyPopUpWindow() {
              MyPopUpWindow.close();
          }
          function saveCustomHeaderHTML(html) {
              closeMyPopUpWindow();
              var Header = document.getElementById("Header");
              Header.innerHTML = html;
              var Update = document.getElementById("Update");
              Update.click();
          }  
          function customHeaderHelpPopup()
          {
              NamedPopUp('../../Help/Design/CustomHeaderHelp.htm', 'CustomHeaderHelpPopup', 'height=800, width=800, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
          }
          function customHeaderReferencePopup() {
              NamedPopUp('../../Help/Design/CustomHeaderReference.htm', 'CustomHeaderReferencePopup', 'height=800, width=800, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
          }
     </script>
    <style type="text/css">
        .style1
        {
            width: 152px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
	</telerik:RadScriptManager>
    
    	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>    
             <telerik:AjaxSetting AjaxControlID="UploadButton">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="UploadMessage" > </telerik:AjaxUpdatedControl>                   
                     <telerik:AjaxUpdatedControl ControlID="Header" > </telerik:AjaxUpdatedControl>                   
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="Update">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Message" > </telerik:AjaxUpdatedControl>                   
                    <telerik:AjaxUpdatedControl ControlID="Header" > </telerik:AjaxUpdatedControl>                   
                 </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="Clear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Message" > </telerik:AjaxUpdatedControl>                   
                    <telerik:AjaxUpdatedControl ControlID="Header" > </telerik:AjaxUpdatedControl>                   
                 </UpdatedControls>
            </telerik:AjaxSetting>

       </AjaxSettings>
	</telerik:RadAjaxManager>
    <div>
    <table style="width: 676px; height: 63px;"><tr><td colspan="3">
             <asp:Label ID="Message" runat="server" Font-Bold="True" Font-Names="Arial"
                 Font-Size="10pt" ForeColor="Maroon" Width="324px" Height="16px"></asp:Label>
                 </td><td align="right">
                                            <img alt="" id="CustomHeaderHelp" 
                src="../../images/help.gif"  onclick="customHeaderHelpPopup()"/></td></tr><tr>
            <td class="style1">
        <asp:Button ID="Update" runat="server" Text="Update Custom Header HTML" 
            Width="193px" onclick="Update_Click" /></td><td align="center"> 
            <asp:Button ID="Clear" 
                runat="server" Text="Clear Custom Header HTML" 
            Width="182px" onclick="Clear_Click" /></td><td align="right">
            <asp:Button ID="Upload" 
                runat="server" Text="Upload Custom Header HTML" 
            Width="193px"  /></td><td align="right">
            <asp:Button ID="CustomHeaderReference" onclientclick="customHeaderReferencePopup()"
                runat="server" Text="Reference" 
            Width="81px"  /></td></tr></table>
    </div>
    <div style="width: 700px">
        <asp:TextBox ID="Header" runat="server" TextMode="MultiLine" 
            style="width:670px;height:690px;"></asp:TextBox>
    </div>
     <telerik:RadToolTip runat="server" ID="HelpTip1" Width = "250px" Height = "25px" TargetControlId = "CustomHeaderHelp" 
                   IsClientID="true" Animation = "Fade" Position= "BottomLeft"><span style="color:Blue;font-size:14px;">How do I use Custom Header HTML?
                     </span></telerik:RadToolTip>
                     <telerik:RadToolTip runat="server" ID="RadToolTip1" Width = "250px" Height = "25px" TargetControlId = "CustomHeaderReference" 
                   IsClientID="true" Animation = "Fade" Position= "BottomLeft"><span style="color:Blue;font-size:14px;">What are the ViziApps functions that I can use?
                     </span></telerik:RadToolTip>
    </form>
</body>
</html>
