<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertImage.aspx.cs" Inherits="InsertImage" %>

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
        .style4
        {
            width: 515px;
            height: 40px;
        }
        #imageform
        {
            height: 327px;
        }
        .style35
        {
            width: 110px;
        }
        </style>
     <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
     <script  language="javascript" type="text/javascript" src="../scripts/font_1.6.min.js"></script>
     <script  language="javascript" type="text/javascript" src="js/insertImage_1.2.min.js"></script>
</head>
<body style="height: 325px">
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
               <telerik:RadWindow 
                    id="imageArchive" 
                    runat="server"
                    showcontentduringload="true"
                    title="Choose an Icon or Bar Image" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>
                </Windows>
     </telerik:RadWindowManager>
      
      <telerik:RadDialogOpener runat="server" id="DialogOpener1"></telerik:RadDialogOpener> 
        <div style="height:35px;">
        <table style="width: 700px"><tr><td>
        Internal Image Name:&nbsp;<input id="imageName"  type="text" size="30"/>
            </td><td align="right">
                &nbsp;</td></tr></table>
        </div>
    
     <div style="height:39px;">

       <div style="height:199px; background-color: #FFFFEC;">
       <table style="width: 716px">
       <tr>
       <td class="style34">Choose one of the 5 ways to load an image: load a stock icon, 
           upload your own image, use a previous image, use an existing image URL or use an image URL from a network 
           response.</td>
           </tr>
       <tr>
       <td class="style32">
       <table style="width: 703px"><tr><td>
         <input type="button" onclick="javascript:showSetImageClient();" 
                value="Choose From Our Stock Icons and Navigation Bars" 
               style="width: 328px"/></td><td>
        <input type="button" onclick="javascript:showUploadImageClient();" 
                value="Upload Your Custom Image" style="width: 187px"/></td><td>
        <input type="button" onclick="javascript:showPreviousImageClient();" 
                value="Use a Previous Image" style="width: 155px"/></td></tr></table>
                </td>
           </tr>
       <tr><td> <table><tr><td class="style35">
       Enter Image URL:</td><td><asp:TextBox ID="imagePath" 
                   runat="server" Width="581px"></asp:TextBox>      
          </td></tr></table>
                         <asp:HiddenField ID="ImageWidth" runat="server" />
               <asp:HiddenField ID="ImageHeight" runat="server" /></td>
           </tr>
       <tr><td class="style4">If the image will be downloaded from 
           your back-end database leave the URL 
           field blank. A downloaded URL will overwrite the field value.</td>
           </tr>
       </table> 
    </div>
         <div>
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Image"/></div>
    </div>
    </form>
</body>
</html>
