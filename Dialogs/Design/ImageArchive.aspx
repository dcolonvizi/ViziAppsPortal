<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageArchive.aspx.cs" Inherits="Dialogs_ImageArchive" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Choose From Your Image Archive</title>
     <script  language="javascript" type="text/javascript" src="../../scripts/default_script_1.6.js"></script>
          <script  language="javascript" type="text/javascript" src="../../jquery/js/jquery-1.5.1.min.js"></script>  
       <script type="text/javascript" src="../../jquery/js/jquery-ui-1.8.13.custom.min.js"></script>
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

        function CloseWithArg(url, id) {
            var args = new Array(3);
            args[0] = url;
            $(function () {
                args[1] = $('#' + id).width();
                args[2] = $('#' + id).height();
            });
             GetRadWindow().close(args);
        }
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="ParamRepeater" runat="server">
            <HeaderTemplate><div style="height:10px;"></div>
                <div align="left" style="height:35px; background-color:#CCFFCC"><asp:Label 
                        ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                        ForeColor="#003399" 
                        Text="Click the image you want" /></div></HeaderTemplate>
                     <ItemTemplate>
                     <div style="height:10px"></div>
                     <div style="width:150px;font-family:Verdana;font-size:12px;color:Navy">
                       <img src='<%#DataBinder.Eval(Container.DataItem,"image_url") %>' alt="" style="border:1px solid #cccccc"
                            id="<%#DataBinder.Eval(Container.DataItem,"id") %>"  onclick="CloseWithArg('<%#DataBinder.Eval(Container.DataItem,"image_url") %>','<%#DataBinder.Eval(Container.DataItem,"id") %>');" />
                    </div>
            </ItemTemplate>
       </asp:Repeater>
    </div>
    </form>
</body>
</html>
