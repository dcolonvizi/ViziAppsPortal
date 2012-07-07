using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Collections;
using System.Text;
using System.IO;
using Telerik.Web;
using HtmlAgilityPack;
using System.Drawing;
using Telerik.Web.UI;

public partial class Controls_PageView : System.Web.UI.UserControl
{
    public Controls_PageView()
    {
    }
    public Controls_PageView(string PageName)
    {
        XmlUtil x_util = new XmlUtil();
        Util util = new Util();
 
        //get page image
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State["SelectedAppPage"].ToString() == PageName)
            PageViewContainer.Style.Value = "background-color:#FFFFCC";
        PageImage.ImageUrl = util.GetAppPageImage(State, State["PageViewAppID"].ToString(), PageName);
        PageImage.ID = PageName + "_PageImage";
        PageImage.Attributes.Add("onclick", "goToPage('" + PageName + "');");
        PageImage.Attributes.Add("onmouseover", "this.style.cursor='pointer';");
        PageImage.Attributes.Add("onmouseout", "this.style.cursor='arrow';");
        if (State["UseFullPageImage"] != null)
        {
            PageImage.Width = 320;
            PageImage.Height = 460;
        }
        //get page fields
        XmlDocument doc = x_util.GetStagingAppXml(State);
        RadTreeNode PageRoot = new RadTreeNode(PageName);
        PageRoot.CssClass = "RadTreeView";
        PageRoot.ImageUrl = "../../images/ascx.gif";
        PageRoot.Category = "page";
        PageRoot.Font.Size = FontUnit.Point(12);
        OnePageView.Nodes.Add(PageRoot);

        //do all fields 
        XmlNode page = doc.SelectSingleNode("//pages/page/name[.  ='" + PageName + "']").ParentNode;
        XmlNode fields = page.SelectSingleNode("fields");
      
        if (fields != null)
        {
            //sort fields first
            SortedList list = new SortedList();
            SortableList<StoryBoardField> nameList = new SortableList<StoryBoardField>();


            foreach (XmlNode child in fields.ChildNodes)
            {
                Hashtable dict = new Hashtable();
                dict["field_type"] = child.Name;
                XmlNode id_node = child.SelectSingleNode("id");
                dict["id"] = id_node;
                string input_field = id_node.InnerText.Trim();
                dict["left"] = child.SelectSingleNode("left").InnerText;
                dict["top"] = child.SelectSingleNode("top").InnerText;
                dict["width"] = child.SelectSingleNode("width").InnerText;
                dict["height"] = child.SelectSingleNode("height").InnerText;
                string field_type = dict["field_type"].ToString();
                if (field_type == "button" ||
                    field_type == "image_button" ||
                    field_type == "table" ||
                    field_type == "switch")
                {
                    if(child.SelectSingleNode("submit") != null)
                        dict["submit"] = child.SelectSingleNode("submit").InnerText;
                }
                if (field_type == "table" )
                {
                    XmlNodeList sub_fields = child.SelectNodes("table_fields/table_field/name");
                    ArrayList table_list = new ArrayList();
                    foreach (XmlNode sub_field in sub_fields)
                    {
                        table_list.Add(sub_field.InnerText);
                    }
                    dict["sub_fields"] = table_list;
                }
                else if (field_type == "picker")
                {
                    XmlNodeList sub_fields = child.SelectNodes("picker_fields/picker_field/name");
                    ArrayList picker_list = new ArrayList();
                    foreach (XmlNode sub_field in sub_fields)
                    {
                        picker_list.Add(sub_field.InnerText);
                    }
                    dict["sub_fields"] = picker_list;
                }
                list[input_field] = dict;
                nameList.Add(new StoryBoardField(id_node.InnerText.Trim(), Convert.ToInt32(child.SelectSingleNode("top").InnerText), Convert.ToInt32(child.SelectSingleNode("left").InnerText)));
            }

            nameList.Sort("Top", true);

            foreach (StoryBoardField input_field in nameList)
            {
                Hashtable dict = (Hashtable)list[input_field.FieldName];
                string field_type = dict["field_type"].ToString();
                RadTreeNode field_node = util.CreateFieldNode(PageRoot, input_field.FieldName, field_type);
                field_node.Value = "left:" +  dict["left"].ToString() + ";top:" + dict["top"].ToString() + ";width:" + dict["width"].ToString() + ";height:" + dict["height"].ToString() ;
                if (dict["submit"] != null && dict["submit"].ToString().Length > 0 && dict["submit"].ToString() != ";")
                {
                    field_node.BackColor = Color.PeachPuff;
                    string[] submit = dict["submit"].ToString().Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    string target_page_name = null;
                    //this code is compatible with both old and new syntax
                    if (submit[0].StartsWith("post") && (submit[0].Contains("response_page:") || submit[0].Contains("response_page~")))
                    {
                        target_page_name = submit[0].Substring(19);                        
                        field_node.Value += ";next_page:" + target_page_name;
                    }
                   /* else if (submit.Length > 1 && (submit[1].StartsWith("response_page:") || submit[1].StartsWith("response_page~")))
                    {
                        target_page_name = submit[1].Substring(14);
                        field_node.Value += ";next_page:" + target_page_name;
                    }*/
                    else if (submit[0].StartsWith("next_page"))
                    {
                        if (submit[0].Contains("next_page:page~"))
                            target_page_name = submit[0].Substring(15);
                        else
                            target_page_name = submit[0].Substring(10);

                        field_node.Value += ";next_page:" + target_page_name;
                    }

                 }
                if (field_type == "table" ||
                    field_type == "picker")
                {
                    ArrayList sub_field_list = (ArrayList)dict["sub_fields"];
                    foreach (string sub_field_name in sub_field_list)
                    {
                        RadTreeNode sub_field_node = util.CreateFieldNode(field_node, sub_field_name, field_type);
                    }
                }
            }
        }
        OnePageView.ExpandAllNodes();
        OnePageView.OnClientMouseOver = "onPageViewMouseOver";
        OnePageView.OnClientMouseOut = "onPageViewMouseOut";
    }
}