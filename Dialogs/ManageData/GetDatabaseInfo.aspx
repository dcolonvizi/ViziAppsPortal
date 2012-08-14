<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetDatabaseInfo.aspx.cs" Inherits="Dialogs_GetDatabaseInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Get Database Info for ViziApps</title>
    <style type="text/css">
        .style1
        {
            width: 338px;
        }
        .style2
        {
            width: 338px;
            height: 38px;
        }
        .style4
        {
            height: 44px;
            width: 341px;
        }
        .style5
        {
            height: 35px;
            width: 341px;
        }
        .style6
        {
            width: 123px;
        }
        .style7
        {
            width: 147px;
        }
        .style8
        {
            width: 48px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    	<telerik:RadScriptManager ID="RadScriptManagerDB" runat="server"/>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>        
              <telerik:AjaxSetting AjaxControlID="SaveDatabaseInfo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="SaveDatabaseInfoMessage"  />    
                        <telerik:AjaxUpdatedControl ControlID="Status" />                     
                    </UpdatedControls>
                </telerik:AjaxSetting>
                </AjaxSettings>
        </telerik:RadAjaxManager>

<script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close(sender, eventArgs) {
            var customArg = Array(2);
           customArg[0] =  document.getElementById("Status").value;
           customArg[1] = document.getElementById("DBConnectionString").value;
           GetRadWindow().close(customArg);
        }
        </script>
	
     <telerik:RadProgressManager ID="RadprogressmanagerDB" runat="server" />
         <telerik:RadWindowManager ID="RadWindowManagerDB" runat="server">
            <Windows>
                <telerik:RadWindow 
                    id="HelpGetDatabaseInfoWindow" 
                    runat="server"
                    showcontentduringload="false"
                    title="Help on Getting Database Info"
                    behaviors="Close" Skin="Web20">
                </telerik:RadWindow>
                
            </Windows>
            </telerik:RadWindowManager>

    <div style=" font-family:Verdana; font-size:12px;">
    <table style="width: 405px"><tr><td class="style7">Your Database Type</td>
        <td align="left">
        <telerik:RadComboBox ID="DatabaseType" runat="server" >
        <Items>
         <telerik:RadComboBoxItem  Value="MySql" Text="MySql"/>
         <telerik:RadComboBoxItem  Value="SqlServer" Text="SqlServer"/>
         <telerik:RadComboBoxItem  Value="Oracle" Text="Oracle"/>
         <telerik:RadComboBoxItem  Value="Sybase" Text="Sybase"/>
         <telerik:RadComboBoxItem  Value="DB2" Text="DB2"/>
         <telerik:RadComboBoxItem  Value="Informix" Text="Informix"/>
         <telerik:RadComboBoxItem  Value="TeraData" Text="TeraData"/>
        </Items>
        </telerik:RadComboBox>
        </td><td>
      <asp:ImageButton ID="GetDatabaseInfoHelp" runat="server" ImageUrl="~/images/help.gif" 
        ToolTip="How is this info used?" />

        </td></tr></table>
    </div>
    <div style=" font-family:Verdana; font-size:12px;">
     <table><tr><td class="style2" valign="bottom">Upload the .sql file of your database without data:</td>
         </tr><tr><td class="style1">
        <telerik:RadUpload ID="SqlFileUpload" runat="server" ControlObjectsVisibility="None" OverwriteExistingFiles ="True"
             TargetFolder="~/temp_files" Skin="Windows7" Width="315px" Height="34px" 
                 AllowedFileExtensions=".sql" MaxFileInputsCount="1">
        </telerik:RadUpload>
             </td></tr><tr><td class="style1">
             <table style="width: 353px"><tr><td class="style6">
    <asp:Button runat="server" ID="UploadSQLFile" Text="Upload File" onclick="UploadSQLFile_Click" 
 /></td><td>
                <asp:Label ID="SqlUploadMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="231px"></asp:Label>
                        </td></tr></table>
             </td></tr></table>
    </div>
    <div style=" font-family:Verdana; font-size:12px;">
     <table><tr><td class="style5" valign="bottom">Database Connection String Within Your 
         Server:</td></tr><tr><td height="35">
        <asp:TextBox ID="DBConnectionString" runat="server" Width="337px"></asp:TextBox>
         </td></tr><tr><td class="style4">
         <table style="width: 328px; height: 97px;"><tr><td class="style8">
    <asp:Button runat="server" ID="SaveDatabaseInfo" Text="Save" onclick="SaveDatabaseInfo_Click"  
 /></td><td align="center">
                    <asp:Button ID="Remove" runat="server" Text="Remove All Info" onclick="Remove_Click" 
                       />
                        </td><td align="right">
                    <asp:Button ID="Cancel" runat="server" Text="Close" 
                     OnClientClick="Close();return false;"  />
                        </td></tr><tr><td colspan="3">
                <asp:Label ID="SaveDatabaseInfoMessage" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon" Width="306px"></asp:Label>
                        </td></tr><tr><td colspan="3">
                    <asp:TextBox ID="Status" runat="server" style="display:none"/>
                        </td></tr></table>
         </td></tr></table>
         </div>
    </form>
</body>
</html>
