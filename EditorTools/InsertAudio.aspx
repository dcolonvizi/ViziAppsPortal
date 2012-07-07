<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertAudio.aspx.cs" Inherits="EditorTools_InsertAudio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Insert An Audio File</title>
    <style type="text/css">
            body
        {
        	font-family: tahoma; font-size: 12px; height: 124px;width:537px;
        }

        .style34
        {
            width: 515px;
            height: 37px;
        }
        .style36
        {
            height: 32px;
        }
        #audioform
        {
            width: 622px;
        }
        .style37
        {
            width: 515px;
            height: 41px;
        }
        </style>
     <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>   
     <script  language="javascript" type="text/javascript" src="js/insertAudio_1.1.min.js"></script>

</head>
<body>
    <form id="audioform" runat="server">
     <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
                </telerik:RadScriptManager>
     <telerik:RadWindowManager ID="RadWindowManagerImage" runat="server" Skin="Telerik">
            <Windows>
                <telerik:RadWindow 
                    id="audioWindow" 
                    runat="server"
                    showcontentduringload="true"
                    title="Upload Audio File" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
             </telerik:RadWindow>
                </Windows>
     </telerik:RadWindowManager>
      <div style="width:625px; height:264px; font-family: tahoma; font-size: 12px; ">
      <telerik:RadDialogOpener runat="server" id="DialogOpener1"></telerik:RadDialogOpener> 
        <div style="height:35px;">
        <table style="width: 602px"><tr><td>
        Internal Audio Name:&nbsp;<asp:TextBox ID="audioName" runat="server" Width="179px"></asp:TextBox>
            &nbsp;This is a hidden field on the device</td><td align="right">
      
<asp:ImageButton ID="ComputeHelp" runat="server" ImageUrl="~/images/help.gif" 
        ToolTip="How is an audio file played on the device?" OnClientClick="PopUp('../Help/Design/AudioHelp.htm', 'height=325, width=500, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');return false;"/>
            </td></tr></table>
        </div>
    
     <div style="height:43px;">
       <div style="height:191px; background-color: #FFFFEC;">
       <table style="width: 609px; height: 190px;">
       <tr>
       <td class="style37">Choose one of the 3 ways to load your audio URL: upload audio 
           file, existing audio URL 
           or dynamic audio URL from the web</td>
           </tr>
       <tr>
       <td class="style34">
        <input type="button" onclick="javascript:showUploadAudioClient();" 
                value="Upload Your Custom Audio" style="width: 290px"/>           
           </td>
           </tr>
       <tr><td class="style36"> 
       Enter Audio URL:</td>
       </tr>
       <tr><td><asp:TextBox ID="audioPath" runat="server" Width="572px"></asp:TextBox>   
       
       &nbsp;<span onclick="playAudio()"><asp:Image ToolTip="Play audio file" ImageUrl="../images/play.gif" runat="server"/>  </span>
                        </td>
           </tr>
       <tr><td style="height:40px;">If the audio will be downloaded from 
           your back-end database leave the URL 
           field blank. A downloaded URL will overwrite the field value.</td>
           </tr>
       </table> 
        
         </div>
         </div>
         
         </div>
    <div>
        <input type="button" id="insertButtonID" onclick="insertButton();" value="Insert Audio"/>
    </div>
    </form>
</body>
</html>
