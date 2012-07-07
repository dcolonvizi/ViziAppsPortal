<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoryBoard.aspx.cs" Inherits="Dialogs_StoryBoard" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ViziApps Story Board</title>

    <link href="../../jquery/css/cupertino/jquery-ui-1.8.13.custom.css" type="text/css" rel="stylesheet"/>

   <script type="text/javascript" src="../../jquery/js/jquery-1.5.1.min.js"></script>
    <script type="text/javascript" src="../../jquery/js/jquery-ui-1.8.13.custom.min.js"></script>
   <script language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
   <script language="javascript" type="text/javascript" src="../../scripts/browser_1.4.js"></script>
   <script  language="javascript" type="text/javascript" src="../../scripts/storyboard_popup_1.5.min.js"></script> 
    <script type="text/javascript">
        function responseEnd(sender, eventArgs) {
            doPopup = document.getElementById("doPopup");
            if (doPopup.value.length > 0) {
                window.open(doPopup.value, 'pdf', 'top=50,left=100,height=800,width=600,resizable=yes,scrollbars=yes,toolbar=no,status=no');
                doPopup.value = '';
            }
        }
   </script> 
</head>
<body>
    <form id="form1" runat="server">
   <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
	</telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="ChangeScale">
            <UpdatedControls>
               <telerik:AjaxUpdatedControl ControlID="PageTreeView" LoadingPanelID="PageTreeViewLoadingPanel"></telerik:AjaxUpdatedControl>     
                <telerik:AjaxUpdatedControl ControlID="ChangeScale" LoadingPanelID="PageTreeViewLoadingPanel"></telerik:AjaxUpdatedControl>                 
          </UpdatedControls>
        </telerik:AjaxSetting>        
         <telerik:AjaxSetting AjaxControlID="ExportDesign">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="doPopup"></telerik:AjaxUpdatedControl>                 
          </UpdatedControls>
           </telerik:AjaxSetting>
        </AjaxSettings>
         <ClientEvents OnResponseEnd="responseEnd" />
        </telerik:RadAjaxManager>
         <telerik:RadAjaxLoadingPanel ID="PageTreeViewLoadingPanel" runat="server" Skin="Default" 
                                Transparency="0" BackColor="LightGray"  
                               ></telerik:RadAjaxLoadingPanel>     
    <div>
    <table style="width:400px;font-family:Arial;font-size:12px"><tr><td class="style1">
        <asp:Button ID="ExportDesign" runat="server" Text="Download App Design as PDF File"                 
                Width="225px" onclick="ExportDesign_Click" />
        <asp:Button ID="ChangeScale" runat="server" Text="Scale Up Images" 
                Width="140px" onclick="ChangeScale_Click" style="display:none"/>
         <input id="SelectedAppType" type="hidden" runat="server" />
         <input id="XScaleFactor" type="hidden" runat="server" />
         <input id="YScaleFactor" type="hidden" runat="server" />
        </td>
        
        </tr></table>
    </div>
    <div id="treeview_container">
    
                <telerik:RadTreeView ID="PageTreeView" Runat="server" 
                             Skin="Web20" 
                             BorderColor="#CCCCFF"
                              BorderStyle="Dashed"
                              BorderWidth="1px"
                              ForeColor="Navy"
                              Width="440px"
                              Height="850px"
                              OnClientNodeClicked="PageTreeViewNode_Clicked"
                              ></telerik:RadTreeView>
                               
    </div>
    <asp:TextBox ID="storyboard_html" runat="server" style="display:none"/>
     <asp:TextBox ID="doPopup" runat="server" style="display:none"/>
    
    </form>
</body>
</html>
