<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertSlider.aspx.cs" Inherits="InsertSlider" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert a Slider</title>
    <style type="text/css">
         body
        {
        	font-family: tahoma; font-size: 12px; 
        }
        #max_value
        {
            width: 115px;
        }
        #min_value
        {
            width: 113px;
        }
        #sliderName
        {
            width: 183px;
        }
    </style>
</head>
<body>
    <form id="buttonform" runat="server">
    <div style="height:40px;">
        Internal Slider Name:&nbsp; &nbsp;<input type="text" id="sliderName" size="40"/>
    </div>
   <div style="height:30px; ">     
        Slider type is horizontal</div>
 
   <div style="height:37px;">
        Minimum Value: &nbsp; <input type="text" id="min_value" size="40" value="0.0" />
    </div>
    <div style="height:40px;">
        Maximum Value:&nbsp; <input type="text" id="max_value" size="40" value="100.0" />
    </div>
        <div style="height:25px;">
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Slider"/>
           </div>
    

    <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
    <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
   <script  language="javascript" type="text/javascript" src="js/insertSlider_1.1.min.js"></script>

    </form>
</body>
</html>