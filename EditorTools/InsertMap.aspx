<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertMap.aspx.cs" Inherits="EditorTools_InsertMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Insert a Map</title>
    <style type="text/css">
         body
        {
        	font-family: tahoma; font-size: 12px; 
        }
        #Text1
        {
            width: 378px;
        }
        .style1
        {
            color: #800000;
        }
    </style>
</head>
<body>
    <form id="mapform" runat="server">
    <div style="height:50px;">
        Internal Map Name:&nbsp;<input type="text" id="MapViewName" />
        <span class="style1">This field cannot be currently displayed on any phone. It will be available soon.</span></div>
     <div style="height:30px;">
         KML
        Map File URL&nbsp; <input type="text" id="url" size="50" /></div>
     <div style="height:50px;">
         This can either be a static URL 
         (use full http format) or set by a network data response when the application runs. This URL will not display in the editor.&nbsp;&nbsp;     </div>
 
     <div style="height:25px;">
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Map"/>
    </div>
    

    <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
         <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>

    <script type="text/javascript">
        if (window.attachEvent) {
            window.attachEvent("onload", initDialog);
        }
        else if (window.addEventListener) {
            window.addEventListener("load", initDialog, false);
        }

        var ArgList = null;

        function initDialog() {
            ArgList = parent.window.getDialogInputArgs();
            for (var i = 0; i < ArgList.length; i++) {
                if (ArgList[i] == null)
                    ArgList[i] = '';
            }
            var MapViewName = document.getElementById("MapViewName");
            MapViewName.value = ArgList[0];

            if (ArgList[0].length > 0) {
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update Map";
            }


            var url = document.getElementById("url");

            url.value = ArgList[1];

            style = ArgList[2];

        }

        function insertButton() //fires when the Insert Link button is clicked
        {
            var MapViewName = document.getElementById("MapViewName");
            if (MapViewName.value == null || MapViewName.value == '') {
                alert('Map View Name must be filled');
                return;
            }

            if (!IsValidObjectName(MapViewName.value)) {
                alert('Map View Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            MapViewName.value = MapViewName.value.replaceAll(" ", "_");

            ArgList[0] = MapViewName.value;

            var url = document.getElementById("url");

            ArgList[1] = url.value;
            ArgList[2] = style;

            parent.window.InsertMapCallback(ArgList);
        }
    </script>
    </form>
</body>
</html>