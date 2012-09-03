<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewApp.aspx.cs" Inherits="Dialogs_NewApp" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style5
        {
            width: 149px;
        }
        .style7
        {
            width: 17px;
            height: 44px;
        }
        .style8
        {
            height: 44px;
            width: 149px;
        }
        .style9
        {
            height: 44px;
            font-family:arial;
            font-size:12px;
        }
        #AppDescription
        {
            width: 370px;
        }
        #PageName
        {
            width: 194px;
        }
        #AppName
        {
            width: 237px;
        }
        .style10
        {
            width: 187px;
        }
        .style11
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
        }
        .style12
        {
            font-family: Arial, Helvetica, sans-serif;
        }
        .style16
        {
            width: 17px;
            height: 36px;
        }
        .style17
        {
            height: 36px;
            width: 149px;
        }
        .style18
        {
            height: 36px;
            font-family: arial;
            font-size: 12px;
        }
        .style19
        {
            width: 17px;
            height: 35px;
        }
        .style20
        {
            height: 35px;
            width: 149px;
        }
        .style21
        {
            height: 35px;
            font-family: arial;
            font-size: 12px;
        }
    </style>
  <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
   <script type="text/javascript">
       function GetRadWindow() {
           var oWindow = null;
           if (window.radWindow) oWindow = window.radWindow;
           else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
           return oWindow;
       }

       var device_type;
       function OnDesignedForDeviceLoadHandler(sender) {
           device_type = sender;
       }

       var app_type;
       function OnAppTypeLoadHandler(sender) {
           app_type = sender;
       }

       function onAjaxResponseEnd(sender, args) {
           var Message = document.getElementById("Message");
           if (Message.innerHTML == "Saved.") {
               var args = new Array(5);
               args[0] = document.getElementById("AppName").value;
               args[1] = device_type.get_value(); //Device Type
               args[2] = app_type.get_value(); //Device Type
               args[3] = document.getElementById("AppDescription").value;
               args[4] = document.getElementById("PageName").value;
               parent.window.onNewAppClientClose(args);
           }
       }
        </script>
 </head>
<body>
    <form id="form1" runat="server">
     <telerik:RadScriptManager ID="RadScriptManagerSaveAs" runat="server">
                </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="AjaxtManagerSaveAs" runat="server">
      <ClientEvents  OnResponseEnd="onAjaxResponseEnd"/>
    <AjaxSettings>
     <telerik:AjaxSetting AjaxControlID="Save">
          <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Message"/>   
               </UpdatedControls>
          </telerik:AjaxSetting>
    </AjaxSettings>
    </telerik:RadAjaxManager>

    <div style="width:580px;height:297px;">
    
        <table style="width:100%; height: 76px;">
            <tr>
                <td class="style16">
                    </td>
                <td class="style17">
                                <asp:Label ID="Label2" runat="server" Text="Application Name" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" 
                        Width="129px"></asp:Label>
                            </td>
                <td class="style18">
                    <input name="AppeName" type="text" id="AppName" size="40" runat="server"/>&nbsp; </td>
            </tr>
            <tr>
                <td class="style19">
                    </td>
                <td class="style20">
                                <asp:Label ID="Label5" runat="server" Text="Designed For" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" 
                        Width="129px"></asp:Label>
                            </td>
                <td class="style21">
                   <telerik:RadComboBox ID="DesignedForDevice" runat="server" OnClientLoad="OnDesignedForDeviceLoadHandler" 
                                                Font-Names="Arial" Font-Size="10pt" 
                                                 Width="175px"  >
                                                <Items>
                                                <telerik:RadComboBoxItem Text="iPhone" Value="iphone" />
                                                 <telerik:RadComboBoxItem Text="Android Phone"  Value="android_phone"/>
                                                  <telerik:RadComboBoxItem Text="iPad"  Value="ipad"/>  
                                                   <telerik:RadComboBoxItem Text="Android Tablet"  Value="android_tablet"/> 
                                                </Items>
                                            </telerik:RadComboBox>&nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td class="style19">
                    </td>
                <td class="style20">
                                <asp:Label ID="Label6" runat="server" Text="App Type" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" 
                        Width="129px"></asp:Label>
                            </td>
                <td class="style21">
                <table style="width: 386px"><tr><td class="style10">
                   <telerik:RadComboBox ID="AppType" runat="server" OnClientLoad="OnAppTypeLoadHandler"
                                                Font-Names="Arial" Font-Size="10pt" 
                                                 Width="175px"  >
                                                <Items>
                                                <telerik:RadComboBoxItem Text="Native App" Value="native"  />
                                                 <telerik:RadComboBoxItem Text="Web App"  Value="web" />                                                   
                                                 <telerik:RadComboBoxItem Text="Hybrid App"  Value="hybrid" /> 
                                                </Items>

                                            </telerik:RadComboBox></td><td><span id="HelpClick" runat="server"> 
                        <img  alt="" 
                                                src="../../images/help.gif" /></span></td><td>&nbsp;&nbsp; </td></tr></table> </td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;</td>
                <td class="style8">
                                <asp:Label ID="Label4" runat="server" 
                        Text="Application Description" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" 
                        Width="159px"></asp:Label>
                            </td>
                <td class="style9">
                    <textarea id="AppDescription" rows="4" cols="40"></textarea></td>
            </tr>
            <tr>
                <td class="style19">
                    </td>
                <td class="style20">
                                <asp:Label ID="Label3" runat="server" Text="First Page Name" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" 
                        Width="129px"></asp:Label>
                            </td>
                <td class="style21">
                    <input name="PageName" type="text" id="PageName" size="40" runat="server"/>&nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td class="style19">
                    &nbsp;</td>
                <td class="style20">
                                &nbsp;</td>
                <td class="style21">
                    All values can be changed later</td>
            </tr>
            <tr>
                <td >
                    &nbsp;</td>
                <td class="style5" >
                    <asp:Button ID="Save" runat="server" Text="Save" Width="59px" onclick="Save_Click" 
                   />
                </td>
                <td class="style11" >
                    <asp:Label ID="Message" runat="server" style="color: #800000"></asp:Label>
                </td>
            </tr>
            </table>
    
    </div>
     <telerik:RadToolTip runat="server" ID="RadToolTip2" Width = "280px" Height = "25px" TargetControlId = "HelpClick" 
                   IsClientID="true" Animation = "Fade" Position= "TopCenter"><span style="color:Blue;font-size:14px;">
         <span class="style12">What do the different app types mean?</span>
                     </span></telerik:RadToolTip> 
    </form>
</body>
</html>
