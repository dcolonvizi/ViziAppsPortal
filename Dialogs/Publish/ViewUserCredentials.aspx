<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewUserCredentials.aspx.cs" Inherits="Dialogs_ViewUserCredentials" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View End User Credentials</title>
</head>
<body>
    <form id="form1" runat="server">
     <telerik:RadScriptManager ID="RadScriptManager11" runat="server">
	</telerik:RadScriptManager>
                               <telerik:RadGrid ID="Credentials" 
                               OnSortCommand="Credentials_SortCommand" 
                               OnPageIndexChanged="Credentials_PageIndexChanged" 
                                ItemStyle-Font-Size="14px"  
                                 AlternatingItemStyle-Font-Size= "14px"
                                 SelectedItemStyle-Font-Size  = "14px"                          
                                Width="98%" 
                                 AllowSorting="True" 
                                 PageSize="30" 
                                 AllowPaging="True" 
                                 AllowMultiRowSelection="True" 
                                 runat="server"
                                 Skin="Telerik"
                                  HeaderStyle-Font-Size="16px"
                                  Gridlines="None" >
                             <PagerStyle Mode="NextPrevAndNumeric" />
                             <ClientSettings EnableRowHoverStyle="true"
                              >
                                <Selecting AllowRowSelect="false"   />
                            </ClientSettings>
                            <MasterTableView>
                               <Columns></Columns>
                            </MasterTableView>

                            </telerik:RadGrid>
                            
    <div>
    
    </div>
    </form>
</body>
</html>
