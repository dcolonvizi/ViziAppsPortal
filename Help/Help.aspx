<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="Help_Help" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ViziApps Help</title>
     <style type="text/css">
        body
        {
        	 background-color:#efefef;
        	 font-family:Arial;
        	 font-size:12px;
        }
        </style>
    <script  language="javascript" type="text/javascript" src="../scripts/default_script_1.6.js"></script>
    <script type="text/javascript">

        function onLoad(sender, args) {
            
        }

        function onNodeClicking(sender, args) {
            var select = args.get_node().get_text();
            switch (select) {
               case 'Quick Start':
                    PopUp('MySolutions/QuickStart.aspx', 'height=940, width=750, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                 case 'Layout Video':
                    PopUp('Design/LayoutVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');
                    break;
                 case 'App Properties':
                   PopUp('Design/AppPropertiesHelp.htm', 'height=800, width=720, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');
                    break;
                case 'See All Native Fields':
                    PopUp('Design/ViewAllNativeFields.htm', 'height=800, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'See All Web and Hybrid Fields':
                    PopUp('Design/ViewAllWebAppFields.htm', 'height=650, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
               case 'Inserting Fields on a Page':
                    PopUp('Design/ToolHelp.aspx', 'height=500, width=750, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no'); 
                    break;
                case 'Form and Button Fields Video':
                    PopUp('Design/FormAndButtonFieldsVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Hidden Fields':
                   PopUp('Design/HiddenFieldHelp.htm', 'height=300, width=500, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');
                    break;
                case 'Compute Action':
                    PopUp('Design/ComputeHelp.htm', 'height=600, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'StoryBoard':
                    PopUp('Design/StoryBoardHelp.htm', 'height=750, width=750, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Audio and Video':
                    PopUp('Design/WebViewAudioAndVideoVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Web Views Video':
                    PopUp('Design/WebViewAudioAndVideoVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Tables Video':
                    PopUp('Design/TablesVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Picker Field Video':
                    PopUp('Design/PickerVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Mobile Payments Video':
                    PopUp('Design/MobilePaymentsVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'HTML Panel Video':
                    PopUp('Design/HtmlPanelVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Custom Header HTML':
                    PopUp('Design/CustomHeaderHelp.htm', 'height=800, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                 case 'Accessing Fields in Javascript':
                    PopUp('Design/CustomHeaderReference.htm', 'height=800, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Manage Data Overview':
                    PopUp('ManageData/ManageDataHelp.htm', 'height=400, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Google SpreadSheet Guide':
                    PopUp('Guide/Default.htm', 'left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Web Services':
                    PopUp('ManageData/WebServiceSpecs.htm', 'height=650, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Database Access':
                    PopUp('ManageData/DatabaseHelp.htm', 'height=650, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Publishing Process':
                    PopUp('Publish/ProvisionHelp.aspx', 'height=650, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'Fees':
                    PopUp('http://www.viziapps.com/features-pricing/', 'height=1000, width=1000, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
                case 'FAQs':
                    PopUp('../TabFAQ.aspx', 'height=1000, width=900, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');
                    break;
            }
            return false;
        }  

        function onMouseOver(sender, args) {
        }

        function onMouseOut(sender, args) {
        }

        function onNodeExpanded(sender, args) {
        }

        function onNodeCollapsed(sender, args) {
        }
            
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <telerik:RadScriptManager ID="RadScriptManager1" runat="server">		
	</telerik:RadScriptManager>
    <div style="width:240px;">
    <div style="font-size:14px;font-weight:bold;height:25px; color:Navy;">
        <table style="width: 240px"><tr><td align="left">Help</td><td align="right">
            <img src="../images/close_dialog.png" alt="" onclick='parent.window.closeHelp();'/></td></tr></table></div>
        <telerik:RadTreeView ID="HelpTree" runat="server" DataSourceID="XmlDataSource1"
            OnClientNodeClicking="onNodeClicking" OnClientMouseOver="onMouseOver" OnClientMouseOut="onMouseOut" 
            OnClientNodeExpanded="onNodeExpanded" OnClientNodeCollapsed="onNodeCollapsed" OnClientLoad="onLoad" >
        <DataBindings>
                        <telerik:RadTreeNodeBinding DataMember="Node" TextField="Text" ImageUrlField="ImageUrl" ExpandedField="Expanded" />
                    </DataBindings>
        </telerik:RadTreeView>
         <asp:XmlDataSource runat="server" ID="XmlDataSource1" DataFile="Help.xml" XPath="/Tree/Node" />
    </div>
    </form>
</body>
</html>
