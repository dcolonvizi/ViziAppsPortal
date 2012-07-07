<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventMappingStatus.aspx.cs" Inherits="Dialogs_EventMappingStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Event Mapping Status</title>
      		<link href="../../styles/baseStyle.css" type="text/css" rel="stylesheet"/>
		<link href="../../styles/tabStyle.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
	</telerik:RadScriptManager>
    <div>
                              
                               <telerik:RadGrid ID="MappingStatus" 
                                OnPageIndexChanged="PageIndexChanged" 
                                ItemStyle-Font-Size="14px"  
                                 AlternatingItemStyle-Font-Size= "14px"
                                 SelectedItemStyle-Font-Size  = "14px"                          
                                Width="98%" 
                                 AllowSorting="True" 
                                 PageSize="8" 
                                 AllowPaging="True" 
                                 AllowMultiRowSelection="True" 
                                 runat="server"
                                 Skin="Telerik"
                                  HeaderStyle-Font-Size="16px"
                                  Gridlines="None" >
                             <PagerStyle Mode="NextPrevAndNumeric" />
                             <ClientSettings EnableRowHoverStyle="true" >                                
                            </ClientSettings>
                            <MasterTableView>
                               <Columns></Columns>
                            </MasterTableView>

                            </telerik:RadGrid>
                            
    </div>
    </form>
</body>
</html>
