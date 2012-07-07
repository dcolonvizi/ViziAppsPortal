<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppImages.aspx.cs" Inherits="Dialogs_AppImages" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>App Images</title>
     <style type="text/css">
    body
    {
       font-family:Verdana;
       font-size:12px;
       color:Black;
	    margin-bottom: 0px;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
  <telerik:RadScriptManager ID="manager1" runat="server">
	</telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
  <AjaxSettings>   
           <telerik:AjaxSetting AjaxControlID="ImageList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SelectedImage" LoadingPanelID="load"/>  
                     <telerik:AjaxUpdatedControl ControlID="Zoom"/>  
                </UpdatedControls>
            </telerik:AjaxSetting>            

             <telerik:AjaxSetting AjaxControlID="Zoom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="SelectedImage" LoadingPanelID="load"/>  
                     <telerik:AjaxUpdatedControl ControlID="Zoom"/>  
                </UpdatedControls>
            </telerik:AjaxSetting>  
            </AjaxSettings>

            </telerik:RadAjaxManager>
    <div>
    <table><tr><td>Image File Name</td><td>
    <telerik:RadAjaxLoadingPanel id="load" runat="server"></telerik:RadAjaxLoadingPanel>
    <telerik:RadComboBox ID="ImageList" runat="server" AutoPostBack="True" 
                         
                        Width="700px" MarkFirstMatch="True" 
            onselectedindexchanged="imageList_SelectedIndexChanged">
                    </telerik:RadComboBox></td><td>                                 
                                 <telerik:RadButton ID="Zoom" runat="server" ButtonType="ToggleButton"  ToggleType="CheckBox"
                     AutoPostBack="true"     Checked="true" onclick="Zoom_Click" style="display:none" Width="120px"
                                        >
                    <ToggleStates>
                        <telerik:RadButtonToggleState  Text="Zoom In" />
                        <telerik:RadButtonToggleState   Text="Zoom Out" />
                    </ToggleStates>                    
                </telerik:RadButton>   
                 </td></tr></table>
    </div>
   
    <div>
        <asp:Image ID="SelectedImage" runat="server" />
    </div>
    </form>
</body>
</html>
