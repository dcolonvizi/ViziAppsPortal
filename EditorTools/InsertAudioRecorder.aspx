<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertAudioRecorder.aspx.cs" Inherits="InsertAudioRecorder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert an Audio Recorder</title>
    <style type="text/css">
        body
        {
        	font-family: tahoma; font-size: 12px; 
        }
        .style1
        {
            color: #800000;
        }
    </style>
</head>
<body>
    <form id="buttonform" runat="server">
    <div style="height:50px;">
        Internal Audio Recorder Name:&nbsp;<input type="text" id="AudioRecorderName" size="40"/> 
        <span class="style1">This field cannot be currently 
        displayed on any phone. It will be available soon.</span>
    </div>
 
   
        <div style="height:25px;">
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Audio Recorder"/>
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
            var AudioRecorderName = document.getElementById("AudioRecorderName");
            AudioRecorderName.value = ArgList[0];

            if (ArgList[0].length > 0) {
                var insertButtonID = document.getElementById("insertButtonID");
                insertButtonID.value = "Update Audio Recorder";
            }

            style = ArgList[1];
        }

        function insertButton() //fires when the Insert Link button is clicked
        {
            var AudioRecorderName = document.getElementById("AudioRecorderName");
            if (AudioRecorderName.value == null || AudioRecorderName.value == '') {
                alert('AudioRecorder Name must be filled');
                return;
            }
            if (!IsValidObjectName(AudioRecorderName.value)) {
                alert('AudioRecorder Name can only contain either a letter, number, space or "_" and be 1 to 100 characters long');
                return;
            }
            AudioRecorderName.value = AudioRecorderName.value.replaceAll(" ", "_");


            ArgList[0] = AudioRecorderName.value;
            ArgList[1] = style;

            parent.window.InsertAudioRecorderCallback(ArgList);
        }
    </script>
    </form>
</body>
</html>
