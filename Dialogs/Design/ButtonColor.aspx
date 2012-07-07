<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ButtonColor.aspx.cs" Inherits="Dialogs_ButtonColor" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Choose Button Color</title>
    <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close(sender, eventArgs) {
            GetRadWindow().close();
        }

        function CloseWithArg(url) {
            GetRadWindow().close(url);
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="ParamRepeater" runat="server">
            <HeaderTemplate><div style="height:10px;"></div>
                <div align="left" style="height:35px; background-color:#CCFFCC"><asp:Label 
                        ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                        ForeColor="#003399" 
                        Text="Click the button you want" /></div></HeaderTemplate>
                     <ItemTemplate>
                     <div style="height:10px"></div>
                     <div style="width:150px;font-family:Verdana;font-size:12px;color:Navy">
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url") %>'
                            id="ButtonImage"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url") %>');" />
                    </div>
            </ItemTemplate>
       </asp:Repeater>
    </div>
    </form>
</body>
</html>
