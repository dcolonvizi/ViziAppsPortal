<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IconImage.aspx.cs" Inherits="Dialogs_IconImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Choose an Icon or Bar Image</title>
    <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close(sender, eventArgs) {
            GetRadWindow().close();
        }

        function CloseWithArg(url, width, height) {
            var arg = [];
            arg[0] = url;
            arg[1] = width;
            arg[2] = height;
            GetRadWindow().close(arg);
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">   
     <div>
       <asp:Repeater ID="DarkIconRepeater" runat="server">
            <HeaderTemplate><div style="height:10px;"></div>
                <div align="left" style="height:35px; background-color:#CCFFCC"><asp:Label 
                        ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                        ForeColor="#003399" 
                        Text="Click the icon image you want. You can also download an image from your browser, modify it and later, upload the modified image as your custom image." /></div></HeaderTemplate>
                     <ItemTemplate>
                     <div style="height:10px"></div>
                     <div style="width:100%;font-family:Verdana;font-size:12px;color:Navy;">
                     <table><tr>
                     <td>
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url0") %>'
                            id="Img0"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url0") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url1") %>'
                            id="Img1"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url1") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url2") %>'
                            id="Img2"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url2") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url3") %>'
                            id="Img3"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url3") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url4") %>'
                            id="Img4"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url4") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url5") %>'
                            id="Img5"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url5") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url6") %>'
                            id="Img6"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url6") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url7") %>'
                            id="Img7"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url7") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url8") %>'
                            id="Img8"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url8") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url9") %>'
                            id="Img9"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url9") %>',this.width,this.height);" />
                            </td>
                            
                            
                            </tr></table>
                    </div>
            </ItemTemplate>
       </asp:Repeater>
    </div>
  
    <div>
       <asp:Repeater ID="BarRepeater" runat="server">
            <HeaderTemplate><div style="height:10px;"></div>
                <div align="left" style="height:35px; background-color:#CCFFCC"><asp:Label 
                        ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                        ForeColor="#003399" 
                        Text="Click the bar image you want." /></div></HeaderTemplate>
                     <ItemTemplate>
                     <div style="height:10px"></div>
                     <div style="width:100%;font-family:Verdana;font-size:12px;color:Navy">
  <table><tr>
                     <td>
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url0") %>'
                            id="ImgA0"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url0") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td>
                             
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url1") %>'
                            id="ImgA1"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url1") %>',this.width,this.height);" />
                            </td>
                            </tr></table>                    </div>
            </ItemTemplate>
       </asp:Repeater>
    </div>
   </form>
</body>
</html>
