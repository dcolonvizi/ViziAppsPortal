<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestQuery.aspx.cs" Inherits="PageData_Dialogs_TestQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


td
{
	MARGIN-TOP: 0px;  
	MARGIN-LEFT: 0px; 
	font-family:Verdana; 
	font-size:12px ;
	color:#333333;
}

           </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManagerSpreadsheet" runat="server"/>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        
      <AjaxSettings>      
          <telerik:AjaxSetting AjaxControlID="TestQueryButton">
                    <UpdatedControls>
                       <telerik:AjaxUpdatedControl ControlID="Message"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="SpreadsheetResponseTreeView" LoadingPanelID="LoadingPanel"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
          </telerik:AjaxSetting>
    </AjaxSettings>
  </telerik:RadAjaxManager>
    <div style="text-align:left;background-color:#CCFFCC">
                            <asp:Label ID="Message" runat="server" 
                                Font-Bold="False" Font-Names="Arial" 
        Font-Size="10pt" ForeColor="Maroon" 
        Text="Enter test values for empty device fields in the Query and click on &quot;Test Spreadsheet Command&quot;" 
        Width="578px"></asp:Label></div>
    <div style="height:10px"></div>     
    <div>
     <asp:Button ID="TestQueryButton" runat="server" 
                                 style="margin-left: 0px" 
                                Text="Test Spreadsheet Command" Width="196px" 
            onclick="TestQueryButton_Click" />
         </div>
          
     <div style="height:10px;">
         &nbsp;&nbsp; </div>
        <div>
        <telerik:RadTreeView ID="SpreadsheetCommandView" Runat="server" 
            BorderColor="#CCCCFF" BorderStyle="Dashed" 
            BorderWidth="1px" ForeColor="Navy" Height="400px"              
            Skin="Web20" Width="703px"></telerik:RadTreeView>               
                                </div>
        <div style="height:10px;">
         &nbsp;&nbsp; </div>
        <div style="text-align:left;background-color:#CCFFCC">
                            <asp:Label ID="Label1" runat="server" 
                                Font-Bold="False" Font-Names="Arial" 
        Font-Size="10pt" ForeColor="Maroon" 
        Text="Test Response: " 
        Width="546px"></asp:Label></div>
       <div>
       <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server" Skin="Default">
       </telerik:RadAjaxLoadingPanel>
        <telerik:RadTreeView ID="SpreadsheetResponseTreeView" Runat="server" 
                    BorderColor="#CCCCFF" BorderStyle="Dashed" BorderWidth="1px" 
                    EnableDragAndDrop="true" ForeColor="Navy" Height="400px" 
                    Skin="Web20" 
                    Width="803px" Font-Size="11"></telerik:RadTreeView></div>                        
    </form>
</body>
</html>
