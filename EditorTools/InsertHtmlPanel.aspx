<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertHtmlPanel.aspx.cs" Inherits="EditorTools_InsertHtmlPanel" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert an Html Panel</title>
    <style type="text/css">
         body
        {
        	font-family: tahoma; font-size: 12px; 
        }
        #htmlPanelName
        {
            width: 176px;
        }
        .colorPickerWrapper
        {
            float: left;
            border: solid 1px #9c9c9c;
            padding: 5px 5px 5px 17px;
            width: 393px;
            background:#F1F1F1 url('../images/editor_images/client-side-api.gif') repeat-x 0 0;
        }
        #htmlPanelform
        {
            height: 900px;
            width: 380px;
        }

    </style>
        <script  language="javascript" type="text/javascript" src="../jquery/js/jquery-1.5.1.min.js"></script>  
        <script type="text/javascript" src="../jquery/js/jquery-ui-1.8.13.custom.min.js"></script>
       <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
       <script  language="javascript" type="text/javascript" src="../scripts/color_picker_1.0.js"></script>
       <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
       <script  language="javascript" type="text/javascript" src="js/insertHtmlPanel_1.2.min.js"></script>

</head>
<body>
    <form id="htmlPanelform" runat="server">
    <telerik:RadScriptManager ID="RadScriptManagerhtmlPanel" runat="server">
                </telerik:RadScriptManager>
       <div style="width:380px;">
    <div style="height:36px;">
    <table style="width: 344px"><tr><td>Internal Panel Name:</td><td><input type="text" id="htmlPanelName" /></td><td>
      <asp:ImageButton ID="HtmlFieldHelp" runat="server" ImageUrl="~/images/help.gif" 
        ToolTip="How is an Html Panel used?" OnClientClick="PopUp('../Help/Design/HtmlPanelHelp.htm', 'height=420, width=530, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');return false;"/>

        </td></tr></table>
        
    </div>
    <div style="height: 795px; width: 100%">
        
         <telerik:RadEditor ID="editor" runat="server" Width="346px" Height="789px" Skin="Default">
          <Tools>
         <telerik:EditorToolGroup>
            <telerik:EditorTool Name="PageProperties" />
            <telerik:EditorTool Name="FontName" />
            <telerik:EditorTool Name="RealFontSize" />
                        <telerik:EditorSeparator />
            <telerik:EditorTool Name="Bold" />
            <telerik:EditorTool Name="Italic" />
            <telerik:EditorTool Name="Underline" />
            <telerik:EditorTool Name="StrikeThrough" />
             <telerik:EditorSeparator />
            <telerik:EditorTool Name="Subscript" />
            <telerik:EditorTool Name="Superscript" />
             </telerik:EditorToolGroup>
              <telerik:EditorToolGroup>
            <telerik:EditorTool Name="ConvertToLower" />
            <telerik:EditorTool Name="ConvertToUpper" />
          <telerik:EditorTool Name="InsertSymbol" />
            <telerik:EditorSeparator />
             <telerik:EditorTool Name="ForeColor" />
            <telerik:EditorTool Name="BackColor" />
                 <telerik:EditorSeparator />
            <telerik:EditorTool Name="JustifyLeft" />
            <telerik:EditorTool Name="JustifyCenter" />
            <telerik:EditorTool Name="JustifyRight" />
                            <telerik:EditorSeparator />
            <telerik:EditorTool Name="StyleBuilder" />
           <telerik:EditorTool Name="FormatStripper" />
  
        </telerik:EditorToolGroup>
             
         <telerik:EditorToolGroup>
            <telerik:EditorTool Name="Indent" />
            <telerik:EditorTool Name="Outdent" />
                 <telerik:EditorSeparator />
            <telerik:EditorTool Name="InsertParagraph" />
            <telerik:EditorTool Name="InsertGroupbox" />
            <telerik:EditorTool Name="InsertHorizontalRule" />
            <telerik:EditorTool Name="InsertOrderedList" />
            <telerik:EditorTool Name="InsertUnorderedList" />
 
        </telerik:EditorToolGroup>
         <telerik:EditorToolGroup>
              <telerik:EditorTool Name="InsertMyImage" Text="Insert Image" />
            <telerik:EditorTool Name="TableWizard" />
             <telerik:EditorTool Name="InsertFormElement" />
                     <telerik:EditorSeparator />
            <telerik:EditorTool Name="InsertDate" />
           <telerik:EditorTool Name="InsertTime" />
         </telerik:EditorToolGroup>
        <telerik:EditorToolGroup>
              <telerik:EditorTool Name="InsertLink" />
              <telerik:EditorTool Name="Unlink" />
       </telerik:EditorToolGroup>
         <telerik:EditorToolGroup>
            <telerik:EditorTool Name="FindAndReplace" />
            <telerik:EditorTool Name="Copy" />
            <telerik:EditorTool Name="Paste" />
            <telerik:EditorTool Name="Undo" />
             <telerik:EditorTool Name="Redo" />
                  <telerik:EditorSeparator />
             <telerik:EditorTool Name="AjaxSpellCheck" />
             <telerik:EditorTool Name="XhtmlValidator" />
             <telerik:EditorTool Name="Print" />
        </telerik:EditorToolGroup>
  </Tools>        
   </telerik:RadEditor>
   <script type="text/javascript">
       var editorForInsertImage;

       Telerik.Web.UI.Editor.CommandList["InsertMyImage"] = function (commandName, editor, args) {
           editorForInsertImage = editor;
           InsertHtmlPanelImageOpen(null);
        };

       function InsertImageInHtmlPanelCallback(image_url) {
           editorForInsertImage.pasteHtml('<img alt="" src="' + image_url + '"/>');
       }
</script>
   
   </div>
     
     </div>
       <div style="height:12px;"></div>
       <div style="height:70px;">
       <table style="width: 375px"><tr><td>Field Location:</td><td>left x</td>
           <td><input type="text" id="left" class="viziapps-field-location" size="10" /></td><td>top y</td><td>
           <input type="text" id="top" class="viziapps-field-location" size="10" /></td>           
           <td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>width</td>
           <td><input type="text" id="width" class="viziapps-field-location" size="10" /></td><td>height</td><td>
           <input type="text" id="height" class="viziapps-field-location" size="10" /></td>           
           <td>&nbsp;</td></tr></table>
    </div>
     <div style="height:25px;">
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Html Panel"/>
    </div>
    

    </form>
       <div id="html_panel_insert_image_dialog" title="Image Properties"></div>

</body>
</html>
