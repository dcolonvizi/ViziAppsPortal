<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadUserCredentials.aspx.cs" Inherits="Dialogs_UploadUserCredentials" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UpLoad ViziApps Credentials</title>
    <style type="text/css">

    .style3
    {
            height: 118px;
        }


        .label
{
   font-family:Verdana;
   font-size:12px;
   color:Black;
   height: 152px;
	width: 516px;
}

ul
{
  font-family: verdana; 
  font-size:11px;
  list-style-image: url(../../images/small_arrow.gif);
  list-style-type:square; 
  list-style-position:outside; 
  padding-left:30px; 
  margin-left:0px;
  margin-top:5px; 
  color:#666666;
  vertical-align:top;
}


        .style23
        {
            width: 308px;
            height: 118px;
        }


        .style9
        {
            width: 308px;
            height: 5px;
        }
        .style24
        {
            height: 5px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
            
                 <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="width: 21px; height: 35px">
                </td>
                <td colspan="3" style="font-weight: bold; font-size: 10pt; font-family: Arial; height: 35px;
                    text-decoration: underline">
                    <asp:Label ID="Title" runat="server" ForeColor="Maroon"></asp:Label></td>
            </tr>
            <tr style="color: #000000">
                <td class="style3">
                </td>
                <td  style="font-size: 10pt; color: black; font-family: Arial; background-color: #FFF4FF; "
                    valign="top"  class="style3">
                    <ul class="label">
                        <li style="padding-bottom:12px;">Click 
                            <b>Download Template</b> and save the Excel template file to your desktop. </li>
                        <li style="padding-bottom:12px;">Enter the ViziApps user credentials in the 
                            appropriate fields in the template file.
                        </li>
                        <li style="padding-bottom:12px;">Select 
                            <b>Add Upload to List</b> or <b>Replace List 
                            with Upload </b></li>
                        <li style="padding-bottom:12px;">Click 
                            <b>Browse</b> to find your Excel file on our desktop.</li>
                        <li style="height: 29px">Click <b>Upload Excel File </b>to import the ViziApps user 
                            credentials.</li>
                    </ul>
                </td>
                <td class="style23"></td>
                <td valign="top" class="style3">
                    <asp:Button ID="DownloadTemplate" runat="server" CausesValidation="False" Font-Names="Arial"
                        Font-Size="10pt" Height="22px" Text="Download Template"
                        Width="130px" onclick="DownloadTemplate_Click" /></td>
            </tr>
            <tr>
                <td class="style9">
                </td>
                <td colspan="3" style="font-size: 10pt; font-family: Arial; " 
                    valign="bottom" class="style24">
                    </td>
            </tr>
            <tr>
                <td style="width: 21px; height: 40px;">
                </td>
                <td  style="height: 40px" colspan="2" valign="middle">
                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Arial" Font-Size="10pt"
                        Width="604px" />
                    
                    </td>
                        <td align="center" valign="middle">
                    <asp:Button ID="UploadExcelFileButton" runat="server" CausesValidation="False" Font-Names="Arial"
                        Font-Size="10pt" Height="22px" OnClick="UploadExcelFileButton_Click" Text="Upload Excel File"
                        Width="128px" /></td>
            </tr>
            <tr>
                <td style="width: 21px; height: 25px">
                </td>
                <td colspan="3" style="height: 25px">
                    <asp:Label ID="ErrorMessage" runat="server" Font-Names="Arial" Font-Size="10pt" 
                        ForeColor="DarkRed" Font-Bold="True" Width="708px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 21px; height: 20px">
                </td>
                <td class="style7">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td style="width: 348px; height: 20px">
                </td>
            </tr>
        </table>
    
    </div>

    </form>
</body>
</html>
