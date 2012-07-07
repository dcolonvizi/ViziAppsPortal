<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertImageInHtmlPanel.aspx.cs" Inherits="InsertImageInHtmlPanel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert An Image</title>
    <style type="text/css">
         body
        {
        	font-family: tahoma; font-size: 12px; 
        }
        .style32
        {
            width: 515px;
            height: 34px;
        }
        .style34
        {
            width: 515px;
            height: 37px;
        }
        #imageform
        {
            height: 327px;
        }
        </style>
</head>
<body style="height: 325px; width: 718px;">
    <form id="imageform" runat="server">
     <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
                </telerik:RadScriptManager>
     <telerik:RadWindowManager ID="RadWindowManagerImage" runat="server" Skin="Telerik">
            <Windows>
                <telerik:RadWindow 
                    id="UploadImageHelp" 
                    runat="server"
                    showcontentduringload="false"
                    width="600px"
                    height="350px"
                    title="How To Upload an Image"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>

                <telerik:RadWindow 
                    id="imageWindow" 
                    runat="server"
                    showcontentduringload="true"
                    title="Choose an Icon or Bar Image" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>
                </Windows>
     </telerik:RadWindowManager>
      
      <telerik:RadDialogOpener runat="server" id="DialogOpener1"></telerik:RadDialogOpener> 
    
     <div style="height:39px;">

       <div style="height:199px; background-color: #FFFFEC;">
       <table style="width: 716px">
       <tr>
       <td class="style34">Choose one of the 3 ways to load an image: load a stock image, 
           upload your own image, or enter an existing image URL.</td>
           </tr>
       <tr>
       <td class="style32">
         <input type="button" onclick="javascript:showSetImageClient();" 
                value="Choose From Our Stock Icons and Navigation Bars" 
               style="width: 349px"/>
                </td>
           </tr>
       <tr>
       <td class="style34">
        <input type="button" onclick="javascript:showUploadImageClient();" 
                value="Upload Your Custom Image" style="width: 349px"/>           
           </td>
           </tr>
       <tr><td> <table><tr><td>
       Enter Image URL:</td><td><asp:TextBox ID="imagePath" runat="server" Width="603px"></asp:TextBox>      
          </td></tr></table>
                         <asp:HiddenField ID="ImageWidth" runat="server" />
               <asp:HiddenField ID="ImageHeight" runat="server" /></td>
           </tr>
       </table> 
    </div>
         <div>
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Image"/></div>
    </div>
    

    <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
     <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
       <script  language="javascript" type="text/javascript" src="js/insertImageInHtmlPanel_1.1.min.js"></script>

    </form>
</body>
</html>
