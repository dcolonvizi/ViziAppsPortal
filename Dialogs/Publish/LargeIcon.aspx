<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LargeIcon.aspx.cs" Inherits="Dialogs_NamePage" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>App Large Icon</title>
   
</head>
<body>
    <form id="form1" runat="server">
 <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
    <script type="text/javascript">
        function GetRadWindow()
        {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close(sender, eventArgs)
        {
           GetRadWindow().close();
        }
        
       
        </script>
       
    <telerik:RadBinaryImage ID="LargeIcon" runat="server" />
    <div>
    
    </div>
    </form>
</body>
</html>
