<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToolHelp.aspx.cs" Inherits="Help_ToolHelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Help with Editor Tools</title>
    <style type="text/css">
        .style1
        {
            color: #000000;
        }
        .style2
        {
            color: #990033;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
    <div style="font-family:Verdana;font-size:14px;width:700px;">
        <p>
            &nbsp;To insert any field or widget:</p>
        <ul>
            <li style="height:40px;">Click on a <strong>field tool</strong> button to the left 
                of the canvas and its property sheet will pop up.
            </li>
            <li style="height:62px;">To insert the field on the canvas, fill out the properties 
                and click on the <strong>Insert </strong>button in the property sheet. Note that 
                every field has an internal name and that all internal names must be unique 
                within each app.</li>
            <li style="height:62px;">After the field appears on the canvas, you can select it, drag it, 
                resize it, bring it forward, push it back, view its properties and delete it.&nbsp; 
                Use the mouse to drag and resize any field or align one field with another.</li>
            <li style="height:50px;">To select a field just click it and it&#39;s border will turn 
                blue. To see its properties just double click it or click the properties 
                (pencil) icon.</li>
            <li style="height:50px;">Once you select a field, you can move it 1 pixel at a time 
                using the any of the four arrow keys on your keyboard.</li>
            <li style="height:49px;">You can delete a field by either hitting the delete key on 
                your keyboard or clicking on the <span class="style2">X</span><span 
                    class="style1"> in the last tool bar.</span></li>
            <li style="height:41px;">If the field has characters, you 
                can modify their size , color and style&nbsp; in the property view.</li>
            <li style="height:40px;"><strong>If you click on a field tool, while an existing 
                field is still selected, the selected field will be replaced by the newly 
                inserted field.</strong></li>
        </ul>
    </div>
    </form>
</body>
</html>
