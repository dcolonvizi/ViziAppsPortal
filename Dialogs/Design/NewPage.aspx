<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewPage.aspx.cs" Inherits="Dialogs_NamePage" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style4
        {
            width: 17px;
        }
        .style2
        {
            width: 17px;
            height: 47px;
        }
        .style3
        {
            height: 47px;
        }
        .style1
        {
        }
    </style>
    <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
    <script type="text/javascript">
        function onAjaxResponseEnd(sender, args) {
            var Message = document.getElementById("Message");
           
            if (Message.innerHTML == "Saved.") {
                var arg = document.getElementById("PageName").value;
                parent.window.onNewPageClientClose(arg);
            }
            else
                alert(Message.innerHTML)
        }
        </script>
</head>
<body>
<form id="form1" runat="server">
 <telerik:RadScriptManager ID="RadScriptManagerSaveAs2" runat="server">
                </telerik:RadScriptManager>
 <telerik:RadAjaxManager ID="AjaxtManagerSaveAs2" runat="server">
      <ClientEvents  OnResponseEnd="onAjaxResponseEnd"/>
    <AjaxSettings>
     <telerik:AjaxSetting AjaxControlID="Save">
          <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Message"/>   
               </UpdatedControls>
          </telerik:AjaxSetting>
    </AjaxSettings>
    </telerik:RadAjaxManager>
    
 
        <table style="width:100%; height: 104px;">
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td>
                                <asp:Label ID="Label2" runat="server" Text="New Page Name" Font-Names="Arial" 
                                    Font-Size="10pt" ForeColor="#000099" Font-Bold="True" 
                        Width="280px"></asp:Label>
                            </td>
            </tr>
            <tr>
                <td class="style2">
                    </td>
                <td class="style3">
                    <input name="PageName" type="text" id="PageName" size="40" runat="server"/></td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Button ID="Save" runat="server" Text="Save" Width="59px" onclick="Save_Click"
                     />
                &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Message" runat="server" style="color: #800000"></asp:Label>
                </td>
            </tr>
        </table>
    
    <div>
    
    </div>
    </form>
</body>
</html>
