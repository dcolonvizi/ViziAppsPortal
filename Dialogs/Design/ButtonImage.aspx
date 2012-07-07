<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ButtonImage.aspx.cs" Inherits="Dialogs_ButtonImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Choose Button Image</title>
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
       <asp:Repeater ID="LightButtonRepeater" runat="server">
            <HeaderTemplate><div style="height:10px;"></div>
                <div align="left" style="height:35px; background-color:#CCFFCC"><asp:Label 
                        ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                        ForeColor="#003399" 
                        Text="Click the button image you want. This first set of images are white with transparent backgrounds. They are good for tab buttons when there is a dark image bar behind the buttons." /></div></HeaderTemplate>
                     <ItemTemplate>
                     <div style="height:10px"></div>
                     <div style="width:100%;font-family:Verdana;font-size:12px;color:Navy; background-color:#000000">
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
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url10") %>'
                            id="Img10"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url10") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url11") %>'
                            id="Img11"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url11") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url12") %>'
                            id="Img12"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url12") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url13") %>'
                            id="Img13"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url13") %>',this.width,this.height);" />
                            </td>
                            </tr></table>
                    </div>
            </ItemTemplate>
       </asp:Repeater>
    </div>

      <div>
       <asp:Repeater ID="NavButtonRepeater" runat="server">
            <HeaderTemplate><div style="height:10px;"></div>
                <div align="left" style="height:35px; background-color:#CCFFCC"><asp:Label 
                        ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                        ForeColor="#003399" 
                        Text="Click the button image you want." /></div></HeaderTemplate>
                     <ItemTemplate>
                     <div style="height:10px"></div>
                     <div style="width:100%;font-family:Verdana;font-size:12px;color:Navy; background-color:#aaaaaa">
  <table><tr>
                     <td>
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url0") %>'
                            id="ImgA0"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url0") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td>
                             
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url1") %>'
                            id="ImgA1"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url1") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url2") %>'
                            id="ImgA2"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url2") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url3") %>'
                            id="ImgA3"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url3") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td>
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url4") %>'
                            id="ImgA4"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url4") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url5") %>'
                            id="ImgA5"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url5") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url6") %>'
                            id="ImgA6"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url6") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url7") %>'
                            id="ImgA7"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url7") %>',this.width,this.height);" />
                            </td>
                            </tr></table>                    </div>
            </ItemTemplate>
       </asp:Repeater>
    </div>
  
    <div>
       <asp:Repeater ID="ColorButtonRepeater" runat="server">
            <HeaderTemplate><div style="height:10px;"></div>
                <div align="left" style="height:35px; background-color:#CCFFCC"><asp:Label 
                        ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                        ForeColor="#003399" 
                        Text="Click the button image you want." /></div></HeaderTemplate>
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
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url2") %>'
                            id="ImgA2"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url2") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url3") %>'
                            id="ImgA3"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url3") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td>
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url4") %>'
                            id="ImgA4"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url4") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url5") %>'
                            id="ImgA5"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url5") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url6") %>'
                            id="ImgA6"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url6") %>',this.width,this.height);" />
                            </td><td style="width:20px"></td>
                            <td> 
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url7") %>'
                            id="ImgA7"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url7") %>',this.width,this.height);" />
                            </td>
                            </tr></table>                    </div>
            </ItemTemplate>
       </asp:Repeater>
    </div>
   </form>
</body>
</html>
