<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertGPS.aspx.cs" Inherits="InsertGPS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert a GPS</title>
     <style type="text/css">
        body
        {
        	font-family: tahoma; font-size: 12px; 
        }
        </style>
    </head>
<body style="height: 178px">
    <form id="buttonform" runat="server">
    <div style="width:390px; height:230px;">
    <div style="height:100px;">
        
        The Internal Name of this field is GPS. This is a hidden field on the device. 
        When inserted once in your app design on any page, it provides the latitude and 
        longitude of the user&#39;s current position. The default internal names of the 
        2 subfields are provided, but you can change them.</div>
          <div style="height:39px;">
              Latitude Name:&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<input type="text" id="latitude" value="latitude" size="40"/> </div>
          <div style="height:41px;">
              Longitude Name:&nbsp;&nbsp; &nbsp;<input type="text" id="longitude" value="longitude" size="40"/> </div>
          
   
   
        <div style="height:25px;">
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert GPS"/>
           </div>
    
    </div>
    <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
         <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
     <script  language="javascript" type="text/javascript" src="js/insertGPS_1.0.min.js"></script>

    </form>
</body>
</html>