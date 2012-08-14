<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadAudio.aspx.cs" Inherits="Dialogs_UploadAudio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Upload Audio</title>
    <style type="text/css">

        .RadUpload_Windows7
{
    font: normal 11px/11px "Segoe UI", Arial, sans-serif;
}

.RadUpload
{
	width:430px; /*default*/
	text-align: left;
}

.RadUpload_Windows7
{
    font: normal 11px/11px "Segoe UI", Arial, sans-serif;
}

.RadUpload
{
	/*default*/
	text-align: left;
}

.RadUpload .ruInputs
{
	list-style:none;
	margin:0;
	padding:0;
}

.RadUpload .ruInputs
{
	zoom:1;/*IE fix - removing items on the client*/
}

.RadUpload .ruInputs
{
	list-style:none;
	margin:0;
	padding:0;
}

.RadUpload .ruInputs
{
	zoom:1;/*IE fix - removing items on the client*/
}

.RadUpload .ruInputs li
{
    margin: 0 0 5px;
}

.RadUpload .ruInputs li
{
    margin: 0 0 5px;
}

.RadUpload_Windows7 .ruFakeInput
{
    border-color: #abadb3 #dbdfe6 #e3e9ef #e2e3ea;
    color: #000;
}

.RadUpload .ruFakeInput
{
    height: 16px;
    margin-right: -1px;
    vertical-align: middle;
    background-position: 0 -93px;
    background-repeat: repeat-x;
    background-color: #fff;
    
    line-height /*\**/: 20px\9; /* IE8 Standards still broken + old hacks don't work */
    height /*\**/: 20px\9;
    padding-top /*\**/: 0\9;
}

.RadUpload .ruFakeInput
{
	float: none;
	vertical-align:top;
}

.RadUpload .ruFakeInput
{
    border-width: 1px;
    border-style: solid;
    line-height: 18px;
    padding: 4px 4px 0 4px;

    -moz-box-sizing: content-box; /* Quirksmode height fix */
    -webkit-box-sizing: content-box;
    box-sizing: content-box;
}

.RadUpload_Windows7 .ruFakeInput
{
    border-color: #abadb3 #dbdfe6 #e3e9ef #e2e3ea;
    color: #000;
}

.RadUpload .ruFakeInput
{
    height: 16px;
    margin-right: -1px;
    vertical-align: middle;
    background-position: 0 -93px;
    background-repeat: repeat-x;
    background-color: #fff;
    
    line-height /*\**/: 20px\9; /* IE8 Standards still broken + old hacks don't work */
    height /*\**/: 20px\9;
    padding-top /*\**/: 0\9;
}

.RadUpload .ruFakeInput
{
	float: none;
	vertical-align:top;
}

.RadUpload .ruFakeInput
{
    border-width: 1px;
    border-style: solid;
    line-height: 18px;
    padding: 4px 4px 0 4px;

    -moz-box-sizing: content-box; /* Quirksmode height fix */
    -webkit-box-sizing: content-box;
    box-sizing: content-box;
}

.RadUpload_Windows7 input
{
    font: normal 11px/11px "Segoe UI", Arial, sans-serif;
}

.RadUpload_Windows7 input
{
    font: normal 11px/11px "Segoe UI", Arial, sans-serif;
}

.RadUpload_Windows7 .ruButton
{
    background-image: url('mvwres://Telerik.Web.UI, Version=2010.1.519.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Windows7.Upload.ruSprite.png');
    color: #1e395b;
}

.RadUpload .ruBrowse
{
    width: 65px;
    margin-left: 4px;
    background-position: 0 0;
}

.RadUpload .ruButton
{
    width: 79px;
    height: 22px;
    border: 0;
    padding-bottom: 2px;
    background-position: 0 -23px;
    background-repeat: no-repeat;
    background-color: transparent;
    text-align: center;
}

.RadUpload .ruButton
{
	float: none;
	vertical-align:top;
}

.RadUpload_Windows7 .ruButton
{
    background-image: url('mvwres://Telerik.Web.UI, Version=2010.1.519.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Windows7.Upload.ruSprite.png');
    color: #1e395b;
}

.RadUpload .ruBrowse
{
    width: 65px;
    margin-left: 4px;
    background-position: 0 0;
}

.RadUpload .ruButton
{
    width: 79px;
    height: 22px;
    border: 0;
    padding-bottom: 2px;
    background-position: 0 -23px;
    background-repeat: no-repeat;
    background-color: transparent;
    text-align: center;
}

.RadUpload .ruButton
{
	float: none;
	vertical-align:top;
}

.RadUpload .ruInputs li.ruActions
{
    margin: 1.4em 0 0;
}

.RadUpload .ruInputs li.ruActions
{
    margin: 1.4em 0 0;
}

        .style50
        {
            height: 30px;
            width: 376px;
        }
        

        .style34
        {
            height: 30px;
        }
        .style39
        {
            height: 17px;
        }
        .style51
        {
            height: 24px;
        }
        </style>
          <script type="text/javascript">
              function GetRadWindow() {
                  var oWindow = null;
                  if (window.radWindow) oWindow = window.radWindow;
                  else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                  return oWindow;
              }

              function CloseSetBackgroundWithArg() {
                  var arg = [];
                  var image_source = document.getElementById("ImageSource");
                  arg[0] = image_source.value;
                  var ImageWidth = document.getElementById("ImageWidth");
                  arg[1] = ImageWidth.value;
                  var ImageHeight = document.getElementById("ImageHeight");
                  arg[2] = ImageHeight.value;
                  GetRadWindow().close(arg);
                  image_source.value = '';
              }
     </script>
</head>
<body>
    <form id="form1" runat="server">
               <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
	</telerik:RadScriptManager>
    	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>    
             <telerik:AjaxSetting AjaxControlID="UploadBackground">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="UploadMessage" > </telerik:AjaxUpdatedControl>                   
                 </UpdatedControls>
            </telerik:AjaxSetting>
       </AjaxSettings>
	</telerik:RadAjaxManager>
     <telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />
         <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <telerik:RadWindow 
                    id="LargeIconBox" 
                    runat="server"
                    showcontentduringload="false"
                    title="Large App Icon"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>
                
            <telerik:RadWindow 
                    id="ScreenShotBox" 
                    runat="server"
                    showcontentduringload="false"
                    title="Screen Image" VisibleStatusbar="false"
                    behaviors="Close" Skin="Web20">
           </telerik:RadWindow>
            </Windows>
            </telerik:RadWindowManager>

                <table style="width:453px;">
                    <tr>
                        <td colspan="2" style="font-family:Verdana; font-size:12px;color:#444444" 
                            class="style51">
                            Upload an 
                            Audio for Your App (.mp3 or .wav files)</td>
                    </tr>
                    <tr>
                        <td colspan="2">
        <telerik:RadUpload ID="UploadBackground" runat="server" ControlObjectsVisibility="None" 
                                OverwriteExistingFiles ="true" InputSize="58"
             TargetFolder="~/temp_files" Skin="Windows7" Width="443px" />

   
	                    </td>
                    </tr>
                    <tr>
                        <td class="style50">
    <asp:Button runat="server" ID="UploadButton" Text="Upload File" onclick="UploadButton_Click" 
 /> <asp:HiddenField ID="ImageWidth" runat="server"/>
               <asp:HiddenField ID="ImageHeight" runat="server" />
                <asp:HiddenField ID="ImageSource" runat="server" /></asp:TextBox>
                             </td>
                        <td class="style34">
    <asp:Button runat="server" ID="Close" Text="Close" OnClientClick="CloseSetBackgroundWithArg()"
 />
                        </td>
                    </tr>
                    <tr>
                        <td class="style39" colspan="2">
                <asp:Label ID="UploadMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="406px"></asp:Label>
                            </td>
                    </tr>
                </table>
               
    <div>
    
    </div>
    </form>
</body>
</html>
