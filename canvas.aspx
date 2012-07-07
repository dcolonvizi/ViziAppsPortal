<%@ Page Language="C#" AutoEventWireup="true" CodeFile="canvas.aspx.cs" Inherits="canvas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="jquery/css/cupertino/jquery.ui.resizable.css"/>
    <link rel="stylesheet" href="jquery/css/cupertino/jquery-ui.css"/>
    <link rel="stylesheet" href="styles/canvas.css"/>
   <style type="text/css">
     body
    {
    	 font-family:Verdana;
         font-size:14pt;
         color: black;
         margin-top:0 0 0 0;
         padding: 0 0 0 0; 
         border-width:0;
         position:static;
         top:0px;
         left:0px;
    }
  </style>

   <script type="text/javascript" src="jquery/js/jquery-1.4.4.min.js"></script>
   <script type="text/javascript" src="jquery/js/jquery.ui.core.js"></script>
   <script type="text/javascript" src="jquery/js/jquery.ui.widget.js"></script>
   <script type="text/javascript" src="jquery/js/jquery.ui.mouse.js"></script>
   <script type="text/javascript" src="jquery/js/jquery.ui.selectable.js"></script>
   <script type="text/javascript" src="jquery/js/jquery.ui.resizable.js"></script>
   <script type="text/javascript" src="jquery/js/jquery.ui.draggable.js"></script>
   <script language="javascript" type="text/javascript" src="scripts/browser_1.4.js"></script>
   <script  language="javascript" type="text/javascript" src="scripts/default_script_1.6.js"></script>
   <script type="text/javascript" src="scripts/canvas_1.14.min.js"></script> 
</head>
<body>
    <form id="form1" runat="server" style="vertical-align:top" >     
    <asp:Literal ID="html_content" runat="server" ></asp:Literal>      
    <asp:TextBox ID="prev_selected_html" runat="server" style="display:none;"></asp:TextBox>   
    </form>
</body>
</html>
