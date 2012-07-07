<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateAndroidDetail.aspx.cs" Inherits="Dialogs_CoverFlow_TemplateAndroidDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ViziApps Template App Selection</title>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
        }
        .style2
        {
            height: 50px;
        }
        .style3
        {
            height: 50px;
            width: 354px;
        }
    </style>
    <link href="../../jquery/css/cupertino/jquery-ui-1.8.13.custom.css" type="text/css" rel="stylesheet"/>

     <script type="text/javascript" src="../../jquery/js/jquery-1.5.1.min.js"></script>
    <script type="text/javascript" src="../../jquery/js/jquery-ui-1.8.13.custom.min.js"></script>
     <script language="JavaScript" type="text/javascript">
         $(function () {
             var Message = $('.check');
             if (Message[0].value.length > 0)
                 parent.window.closeAddTemplateAppClient();
         });
        </script>
</head>
<body style="background-color:#000000;font-family:Verdana;font-size:12pt;">
    <form id="form1" runat="server">
   <div align="center">
<table width="100%">
<tr><td colspan="4" align="center">
    <asp:Image ID="Image1" runat="server" />  
</td></tr>
<tr><td align="right" valign="middle" style="height:50px" class="style1">
    Your Name for this App</td><td align="center" valign="middle" class="style3">
        <asp:TextBox ID="AppName" runat="server" Width="290px"></asp:TextBox>
        </td><td align="left" valign="middle" style="height:50px">
            <asp:Button ID="AddTemplateApp" runat="server" Text="Add To My Account" 
                Width="145px" onclick="AddTemplateApp_Click" /></td>
            <td align="center" valign="middle" style="height:50px">
    <input id="Button1" type="button" value="Back" onclick="window.location.href = 'TemplateAndroidIndex.html';" />
</td></tr>
<tr><td align="center" valign="middle" style="height:50px">
    &nbsp;</td><td align="center" valign="middle" class="style2" 
        colspan="2">
       <asp:Label   ID="Message" runat="server" ForeColor="White" Text=""></asp:Label>
    </td>
            <td align="center" valign="middle" style="height:50px">
                 <asp:TextBox CssClass="check" ID="Ready" runat="server" style="display:none" ></asp:TextBox></td></tr>
</table>
</div>
    </form>
</body>
</html>
