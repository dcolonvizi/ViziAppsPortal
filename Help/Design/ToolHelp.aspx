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
            <li style="height:62px;">After the field appears on the canvas, you can select it 
                with a click, drag it and resize it.&nbsp; 
                Use the mouse to drag and resize any field or align one field with another using 
                the alignment lines along the field edges.</li>
            <li style="height:54px;">To select a field just click it and it&#39;s border will turn 
                blue. To see its properties just double click it or click the <strong>Edit Field</strong>.</li>
            <li style="height:47px;">When two field overlap you can bring the selected field 
                forward with <strong>Bring to Front or</strong> push it back with <strong>Send 
                to Back</strong>,</li>
            <li style="height:62px;">Normally you can move any field to 1 pixel resolution. But 
                if you want to snap the position of a field to a grid location, click on the 
                toggle <strong>Snap to Grid</strong> button. If you want to change the grid size click 
                on the edit icon inside that button.</li>
            <li style="height:50px;">Once you select a field, you can move it 1 pixel at a time 
                using the any of the four arrow keys on your keyboard.</li>
            <li style="height:50px;">You can copy and paste fields in 2 ways either using the 
                <strong>Copy Field</strong> and <strong>Paste Field</strong> or pressing the 
                Ctrl-C and Ctrl-V keys on a PC or Cmd-C and Cmd-V on a Mac</li>
            <li style="height:49px;">You can delete a field by either hitting the delete key on 
                your keyboard or clicking on <span 
                    class="style1"> <strong>Delete Field</strong>.</span></li>
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
