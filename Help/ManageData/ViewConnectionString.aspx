<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewConnectionString.aspx.cs" Inherits="Help_ViewConnectionString" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Database Connection String</title>
    <style type="text/css">

     .style92
     {
         height: 34px;
         width: 25px;
     }
     .style93
     {
         width: 588px;
         height: 34px;
     }
        .style94
        {
            height: 6px;
        }
        .style95
        {
            height: 6px;
            width: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 856px; height: 72px;"><tr><td class="style95"></td>
                                     <td style=" font-family:Verdana; font-size:12;" 
                class="style94">
                                                 <asp:Label 
                                  ID="Label24" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                                  ForeColor="#003399" Text="Database Connection String:"></asp:Label>&nbsp;</td></tr>
                                     <tr>
                                         <td class="style92">
                                         </td>
                                         <td class="style93">
                                                 <asp:Label 
                                  ID="ConnectionString" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                                  ForeColor="#003399" BackColor="#FFFFCC" Width="811px"></asp:Label>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td class="style92">
                                             &nbsp;</td>
                                         <td class="style93">
                                                 <asp:Label 
                                  ID="Label25" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                                  ForeColor="#003399" Text="URL to Database Web Service:"></asp:Label>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td class="style92">
                                             &nbsp;</td>
                                         <td class="style93">
                                                 <asp:Label 
                                  ID="DBWebServiceURL" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                                  ForeColor="#003399" BackColor="#FFFFCC" Width="811px"></asp:Label>
                                         </td>
                                     </tr>
                                     </table>
    
    </div>
    </form>
</body>
</html>
