<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertPhotoTaker.aspx.cs" Inherits="InsertPhotoTaker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert a Photo Taker</title>
    <style type="text/css">
         body
        {
        	font-family: tahoma; font-size: 12px; 
        }
        #compression
        {
            width: 81px;
        }
        .style1
        {
            width: 259px;
        }
        .style2
        {
            width: 259px;
            height: 32px;
        }
        .style3
        {
            height: 32px;
        }
        .style4
        {
            width: 33px;
        }
        .style5
        {
            width: 36px;
        }
        #insertButtonID
        {
            width: 148px;
        }
    </style>
</head>
<body>
    <form id="buttonform" runat="server">
    <div style="height:32px;">
        Internal Photo Name:&nbsp;<input type="text" id="PhotoTakerName" size="40"/>
        </div>
  
        <div style="height: 32px">Compression Ratio (0.001 to 1.0)&nbsp;&nbsp;&nbsp;<input 
                type="text" id="compression" size="20"/></div>
        <div style="height:122px;">
        <div style="height: 63px"><table style="width: 455px">
        <tr><td class="style2">Automated icon saved to <strong>hidden field</strong> name:</td>
            <td colspan="3" class="style3"><input 
                type="text" id="icon_field" size="20"/></td></tr>
        <tr><td align="right" class="style1">Icon W:</td><td class="style4"><input 
                type="text" id="icon_width" size="5" value="60"/></td><td align="right" 
                class="style5">&nbsp;H:&nbsp;&nbsp; </td><td><input 
                type="text" id="icon_height" size="5" value="80"/></td></tr>
        </table>
                </div>
                <div style="height: 25px">(Icon is generated only if icon field name is filled)
                </div>
        <div style="height:25px;">
        <input type="button" id="insertButtonID" onclick="insertButton();" 
                value="Insert Camera Field"/>
           </div>
    

    <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
    <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
    <script  language="javascript" type="text/javascript" src="js/insertPhotoTaker_1.0.min.js"></script>

    </form>
</body>
</html>