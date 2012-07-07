<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertWebView.aspx.cs" Inherits="InsertWebView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert a Web View</title>
    <style type="text/css">
         body
        {
        	font-family: tahoma; font-size: 12px;
            width: 579px;
        }
        #Text1
        {
            width: 378px;
        }
        #webViewName
        {
            width: 156px;
        }
        #url
        {
            width: 476px;
        }
        #buttonform
        {
            height: 210px;
            width: 578px;
        }
        .style1
        {
            width: 45px;
        }
    </style>
    <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
   <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
    <script  language="javascript" type="text/javascript" src="js/insertWebView_1.1.min.js"></script> 
</head>
<body>
    <form id="buttonform" runat="server">
    <div style="width:580px; height:210px">
    <div style="height:40px;">
        Internal Web View Name:&nbsp;<input type="text" id="webViewName" /></div>
    
     <div style="height:30px;">
        Web URL&nbsp; <input type="text" id="url" size="50" /></div>
     <div style="height:53px;">
         This can either be a static URL (use full http format) or set by a network data response when the application 
         runs. If this is blank, a sample web page will be displayed in the editor. A 
         badly formed URL<br />
         will display an error.</div>
 <div style="height:40px;">
       <table style="width: 565px"><tr><td>Field Location:</td><td>left x</td>
           <td class="style1"><input type="text" id="left" size="5" /></td><td>top y</td><td>
           <input type="text" id="top" size="5" /></td>           
           <td>
           width</td><td><input type="text" id="width" size="5" /></td><td>height</td><td>
           <input type="text" id="height" size="5" /></td></tr></table>
    </div>
     <div style="height:30px;">
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Web View"/>
    </div>
     <div>After you insert this webview, to edit it's properties double click on any edge.</div>
    </div>


    </form>
</body>
</html>