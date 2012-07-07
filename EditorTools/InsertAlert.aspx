<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertAlert.aspx.cs" Inherits="InsertAlert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert an Alert</title>
    <style type="text/css">
        body
        {
        	font-family: tahoma; font-size: 12px; 
        }
        #alertMessage
        {
            width: 344px;
        }
        #alertName
        {
            width: 188px;
        }
    </style>
</head>
<body>
    <form id="alertform" runat="server">
    <div style="height:54px;">
    <table style="width: 365px"><tr><td>
        Internal Alert Name:</td><td><input type="text" id="alertName" /></td></tr></table>
    <div>
    The alert triangle symbol will appear on the 
        design canvas, but not on the phone.
    </div>
    </div>
    <div style="height: 38px; width: 537px;">The alert is a popup. The popup occurs when a network data response contains a message assigned to the alert. When the user confirms the alert, it disappears.</div>
    <div>
    </div>
    <div style="height:32px;">
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Alert"/>
    </div>
    
    
        <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
     <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
     <script  language="javascript" type="text/javascript" src="js/insertAlert_1.0.min.js"></script>
    </form>
</body>
</html>