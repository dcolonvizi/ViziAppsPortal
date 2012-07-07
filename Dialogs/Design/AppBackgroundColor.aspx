<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppBackgroundColor.aspx.cs" Inherits="Dialogs_AppBackgroundColor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ViziApps App Description</title>
    <style type="text/css">
        .style1
        {
            height: 84px;
        }
        #AppDescription
        {            width: 300px;
        }
    
        .message
        {
        	font-family:Verdana;
        	font-size:12px;
        	color:Maroon;
        	}
         .colorPickerWrapper
        {
            float: left;
            border: solid 1px #9c9c9c;
            padding: 5px 5px 5px 17px;
            width: 513px;
            background:#F1F1F1 url('../../images/editor_images/client-side-api.gif') repeat-x 0 0;
        }

        </style>
          <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
          <script  language="javascript" type="text/javascript" src="../../scripts/color_picker_1.0.js"></script>
          <script  language="javascript" type="text/javascript" src="../../scripts/font_1.6.min.js"></script>
</head>
<body onunload="parent.window.setAppBackgroundColorCallback();">
     <form id="form1" runat="server" >
     <telerik:RadScriptManager ID="RadScriptManagerEditAppBackgroundColor" runat="server">
                </telerik:RadScriptManager>

    <div style="width:580px;height:190px;">
    
        <table style="width:100%; height: 76px;">
            <tr>
                <td class="style1">
                    </td>
                <td class="style1" colspan="2">
                    <div class="colorPickerWrapper" style="font-family:verdana;font-size:12px">
        <div style="float: left;">
            <div style="line-height: 21px; float: left;">
                Select Background Color:&nbsp;</div>
            <telerik:RadColorPicker ShowIcon="true" ID="BackgroundColor" runat="server" PaletteModes="All" SelectedColor="White"
                OnClientColorChange="HandleColorChange" OnClientLoad="OnColorClientLoad" Style="float: left;" />
        </div>
        <div style="float: right;">
            <div style="line-height: 21px; float: left;">
                Selected 
                Background Color Value:&nbsp;</div>
            <input type="text" value="#f0f0f0" id="ColorPickerSelectedColor" style="width: 70px;" readonly="readonly" runat="server"/>
        </div>
    </div>


                            </td>
            </tr>
            <tr>
                <td >
                    &nbsp;</td>
                <td class="style5" >
                    <asp:Button ID="SaveBackgroundColor" runat="server" Text="Save" Width="59px" onclick="Save_Click" 
                    />
                </td>
                <td >
                   
                </td>
            </tr>
            <tr>
                <td >
                    &nbsp;</td>
                <td class="style5" colspan="2" >
                    <asp:Label ID="Message" runat="server"  Width="546px" CssClass="message"></asp:Label>
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
