using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using System.Net;
using System.IO;
using Telerik.Web;
using HtmlAgilityPack;
using System.Drawing;
using Telerik.Web.UI;

/// <summary>
/// Summary description for XmlUtil
/// </summary>
public class XmlUtil
{
	public XmlUtil()
	{
	}
    public XmlDocument GenerateHybridAppXml(Hashtable State, XmlDocument XmlDoc, string design_width, string design_height,string html)
    {
        WebAppsUtil w_util = new WebAppsUtil();
        XmlUtil x_util = new XmlUtil();
        XmlDocument hybridXmlDoc = new XmlDocument();
        hybridXmlDoc.LoadXml(State["NewHybridAppXml"].ToString());
        XmlNode app_project = hybridXmlDoc.SelectSingleNode("//app_project");

        XmlNode configuration = app_project.SelectSingleNode("configuration");

        XmlNode device_type_node = configuration.SelectSingleNode("device_type");
        device_type_node.InnerText = XmlDoc.SelectSingleNode("//configuration/device_type").InnerText; ;

        XmlNode device_design_width_node = configuration.SelectSingleNode("device_design_width");
        device_design_width_node.InnerText = design_width;

        XmlNode device_design_height_node = configuration.SelectSingleNode("device_design_height");
        device_design_height_node.InnerText = design_height;
 
        XmlNode application = app_project.SelectSingleNode("application");

        XmlNode name = application.SelectSingleNode("name");
        name.InnerText = XmlDoc.SelectSingleNode("//application/name").InnerText;

        XmlNode ID = application.SelectSingleNode("id");
        ID.InnerText =  XmlDoc.SelectSingleNode("//application/id").InnerText;

        XmlNode html_node = application.SelectSingleNode("html");
        html_node.InnerText = HttpUtility.HtmlEncode(html);

        return hybridXmlDoc;
     }

    public void ConvertNativeAppToWebApp(Hashtable State)
    {
        bool xml_changed = false;
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNode application = xmlDoc.SelectSingleNode("//application");
        XmlNode data_sources = application.SelectSingleNode("data_sources");
        if (data_sources == null)
        {
            data_sources = CreateNode(xmlDoc, application, "data_sources");
            xml_changed = true;
        }

        XmlNodeList list = data_sources.SelectNodes("data_source");
        if (list == null || list.Count == 0)  //find old versions
        {
            //prepare advance page and field info
            XmlNode data_source_id_node = null;

            //check on data type
            XmlNode database_config = xmlDoc.SelectSingleNode("//database_config");
            XmlNode phone_data_requests = xmlDoc.SelectSingleNode("//phone_data_requests");
            if (database_config != null && database_config.ChildNodes.Count > 0)
            {
                XmlNode data_source = CreateNode(xmlDoc, data_sources, "data_source");
                XmlNode database_type = database_config.SelectSingleNode("database_type");
                XmlNode data_source_configuration = CreateNode(xmlDoc, data_source, "data_source_configuration");
                if (database_type.InnerText == "GoogleDocs")
                {
                    data_source_id_node = CreateNode(xmlDoc, data_source, "data_source_id", "Google Spreadsheet");
                    CreateNode(xmlDoc, data_source, "data_source_type", "google_spreadsheet");
                    data_source_configuration.AppendChild(database_config.SelectSingleNode("connection_string").CloneNode(true));
                    data_source_configuration.AppendChild(database_config.SelectSingleNode("tables").CloneNode(true));
                }
                else
                {
                    data_source_id_node = CreateNode(xmlDoc, data_source, "data_source_id", "SQL Database");
                    CreateNode(xmlDoc, data_source, "data_source_type", "sql_database");
                    data_source_configuration.AppendChild(database_config.SelectSingleNode("connection_string").CloneNode(true));
                }

                // transfer old event data to page data
                XmlNodeList sql_commands_list = xmlDoc.SelectNodes("//event/sql_commands");
                string goto_page_name = null;
                foreach (XmlNode sql_commands in sql_commands_list)
                {
                    //find which page contains the last phone_field
                    XmlNodeList phone_field_list = sql_commands.SelectNodes("sql_command/field_item/phone_field");
                    if (phone_field_list == null || phone_field_list.Count == 0)
                        continue;
                    string phone_field = phone_field_list[phone_field_list.Count - 1].InnerText;

                    XmlNode phone_field_node = xmlDoc.SelectSingleNode("//id[.  ='" + phone_field + "']");
                    if (phone_field_node == null)
                        phone_field_node = xmlDoc.SelectSingleNode("//name[.  ='" + phone_field + "']");

                    if (phone_field_node == null)
                        continue;

                    XmlNode page = phone_field_node.ParentNode.ParentNode.ParentNode;
                    if (page.Name != "page")
                    {
                        page = page.ParentNode.ParentNode;
                    }
                    if (goto_page_name == null) //only set this page name for the first one
                    {
                        goto_page_name = page.SelectSingleNode("name").InnerText;
                    }

                    XmlNode page_data_sources = page.SelectSingleNode("data_sources");
                    if (page_data_sources == null)
                        page_data_sources = CreateNode(xmlDoc, page, "data_sources");

                    XmlNode page_data_source = CreateNode(xmlDoc, page_data_sources, "data_source");
                    CreateNode(xmlDoc, page_data_source, "data_source_id", data_source_id_node.InnerText);
                    XmlNode event_node = CreateNode(xmlDoc, page_data_source, "event");

                    if (phone_field_node.ParentNode.Name == "alert")
                    {
                        CreateNode(xmlDoc, event_node, "data_source_event_type", "field");
                        CreateNode(xmlDoc, event_node, "data_source_event_field", sql_commands.ParentNode.SelectSingleNode("event_name").InnerText);
                    }
                    else
                    {
                        CreateNode(xmlDoc, event_node, "data_source_event_type", "page");

                        string event_name = sql_commands.ParentNode.SelectSingleNode("event_name").InnerText;
                        XmlNode event_field_node = xmlDoc.SelectSingleNode("//id[.  ='" + event_name + "']");
                        if (event_field_node == null)
                            event_field_node = xmlDoc.SelectSingleNode("//name[.  ='" + event_name + "']");
                        else
                        {
                            XmlNode submit = event_field_node.ParentNode.SelectSingleNode("submit");

                            if (submit != null)
                            {
                                XmlNode next_page_node = null;
                                XmlNode post_page = submit.SelectSingleNode("post");
                                if (post_page != null || (submit.InnerText.Length > 0 && submit.InnerText.StartsWith("post")))
                                {
                                    bool doesReponsePageExist = false;
                                    if (post_page != null)
                                    {
                                        XmlNode response_page = post_page.SelectSingleNode("response_page");
                                        if (response_page != null)
                                        {
                                            next_page_node = CreateNode(xmlDoc, submit, "next_page");
                                            CreateNode(xmlDoc, next_page_node, "page", response_page.InnerText);
                                            doesReponsePageExist = true;
                                            submit.RemoveChild(post_page);
                                        }
                                        else
                                            submit.RemoveChild(post_page);
                                    }
                                    if (submit.InnerText.Length > 0 && submit.InnerText.StartsWith("post"))
                                    {
                                        if (submit.InnerText.IndexOf("response_page") >= 0)
                                        {
                                            int start = submit.InnerText.IndexOf("response_page") + 14;
                                            next_page_node = CreateNode(xmlDoc, submit, "next_page");
                                            int end = submit.InnerText.IndexOf(";", start);
                                            if (end < 0)
                                                end = submit.InnerText.Length;
                                            string page_name = submit.InnerText.Substring(start, end - start);
                                            CreateNode(xmlDoc, next_page_node, "page", page_name);
                                            doesReponsePageExist = true;
                                            submit.FirstChild.InnerText = "next_page:page~" + goto_page_name + ";";
                                        }
                                        else
                                        {
                                            //find last select device field in SQL
                                            next_page_node = CreateNode(xmlDoc, submit, "next_page");
                                            CreateNode(xmlDoc, next_page_node, "page", goto_page_name);
                                            submit.FirstChild.InnerText = "next_page:page~" + goto_page_name + ";";
                                        }
                                    }

                                    if (!doesReponsePageExist && goto_page_name != null)
                                    {
                                        next_page_node = CreateNode(xmlDoc, submit, "next_page");
                                        CreateNode(xmlDoc, next_page_node, "page", goto_page_name);
                                    }

                                   
                                }
                            }
                        }
                    }

                    XmlNode data_source_operations = CreateNode(xmlDoc, event_node, "data_source_operations");
                    data_source_operations.AppendChild(sql_commands.CloneNode(true));
                }
            }
            else if (phone_data_requests != null && phone_data_requests.ChildNodes.Count > 0)
            {
                XmlNode data_source = CreateNode(xmlDoc, data_sources, "data_source");
                XmlNode data_source_configuration = CreateNode(xmlDoc, data_source, "data_source_configuration");
                CreateNode(xmlDoc, data_source, "data_source_id", "Web Service");
                CreateNode(xmlDoc, data_source, "data_source_type", "soap_web_service");
                CreateNode(xmlDoc, data_source_configuration, "web_service_url", xmlDoc.SelectSingleNode("//phone_data_request/web_service").InnerText);
                XmlNodeList methods = xmlDoc.SelectNodes("//phone_data_request/method");
                foreach (XmlNode method in methods)
                    CreateNode(xmlDoc, data_source_configuration, "method", method.InnerText);
            }
            else
                return ;

            xml_changed = true;
        }
        if (xml_changed)
        {
            Util util = new Util();
            State["AppXmlDoc"] = xmlDoc;
            util.UpdateStagingAppXml(State);
        }
    }

    public void RenameApp(Hashtable State)
    {
        RenameApp(State,State["SelectedApp"].ToString());
    }
    public void RenameApp(Hashtable State,string new_app_name)
    {
        XmlDocument doc = GetStagingAppXml(State);
        //set name
        XmlNode name = doc.SelectSingleNode("//application/name");
        name.InnerText = new_app_name;
        Util util = new Util();
        State["AppXmlDoc"] = doc;
        util.UpdateStagingAppXml(State);
    }
   
    public void CreateStagingAppXml(Hashtable State, string device_type, 
        string application_name, string application_id, string page_name)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(State["NewProjectPath"].ToString());

        //set app type
        XmlNode application_node = doc.SelectSingleNode("//application");
        XmlNode type_node = application_node.SelectSingleNode("type");
        if (type_node == null)
            CreateNode(doc, application_node, "type", State["SelectedAppType"].ToString());
        else
            type_node.InnerText = State["SelectedAppType"].ToString();

        //set device_type
        XmlNode configuration = doc.SelectSingleNode("//configuration");
        XmlNode device_type_node = configuration.SelectSingleNode("device_type");
        XmlNode device_design_width_node = configuration.SelectSingleNode("device_design_width");
        XmlNode device_design_height_node = configuration.SelectSingleNode("device_design_height");

        switch (device_type)
        {
            case Constants.IPHONE:
                device_type_node.InnerText = Constants.IPHONE;
                device_design_width_node.InnerText = Constants.IPHONE_DISPLAY_WIDTH_S;
                device_design_height_node.InnerText = Constants.IPHONE_DISPLAY_HEIGHT_S;
                break;
            case Constants.ANDROID_PHONE:
                device_type_node.InnerText = Constants.ANDROID_PHONE;
                device_design_width_node.InnerText = Constants.ANDROID_PHONE_DISPLAY_WIDTH_S;
                device_design_height_node.InnerText = Constants.ANDROID_PHONE_DISPLAY_HEIGHT_S;
                break;
            case Constants.IPAD:
                device_type_node.InnerText = Constants.IPAD;
                device_design_width_node.InnerText = Constants.IPAD_DISPLAY_WIDTH_S;
                device_design_height_node.InnerText = Constants.IPAD_DISPLAY_HEIGHT_S;
                break;
            case Constants.ANDROID_TABLET:
                device_type_node.InnerText = Constants.ANDROID_TABLET;
                device_design_width_node.InnerText = Constants.ANDROID_TABLET_DISPLAY_WIDTH_S;
                device_design_height_node.InnerText = Constants.ANDROID_TABLET_DISPLAY_HEIGHT_S;
                break;
         } 
        
        //set name
        XmlNode name = doc.SelectSingleNode("//application/name");
        name.InnerText = application_name;

        XmlNode id = doc.SelectSingleNode("//application/id");
        id.InnerText = application_id;

        XmlNode first_page_name = doc.SelectSingleNode("//pages/first_page_name");
        first_page_name.InnerText = page_name;  

        Util util = new Util();
        State["AppXmlDoc"] = doc;
        util.UpdateStagingAppXml(State);
    }
   
    public XmlDocument GetStagingAppXml(Hashtable State)
    {
        if (State["AppXmlDoc"] != null)
            return (XmlDocument)State["AppXmlDoc"];
        else
        {
            Util util = new Util();
            return util.GetStagingAppXml(State);
        }
    }
    public XmlDocument GetProductionAppXml(Hashtable State)
    {
        if (State["ProductionAppXmlDoc"] != null)
            return (XmlDocument)State["ProductionAppXmlDoc"];
        else
        {
            Util util = new Util();
            return util.GetProductionAppXml(State);
        }
    }
    public XmlNodeList GetPageNodes(Hashtable State)
    {
        XmlDocument xmlDoc = GetStagingAppXml(State);
        return xmlDoc.SelectNodes("//pages/page");
    }
    public XmlNodeList GetProductionPageNodes(Hashtable State)
    {
        XmlDocument xmlDoc = GetProductionAppXml(State);
        return xmlDoc.SelectNodes("//pages/page");
    }
    public string GetFirstAppPageName(Hashtable State)
    {
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNode first_page_name = xmlDoc.SelectSingleNode("//pages/first_page_name");
        if (first_page_name != null)
        {
            return first_page_name.InnerText;
        }
        else
        {
            first_page_name = xmlDoc.SelectSingleNode("//pages/page/name");
            if (first_page_name != null)
                return first_page_name.InnerText;
            else
                return null;
        }
    }
    public void SetFirstPageName(Hashtable State,string first_page_name)
    {
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNode first_page_name_node = xmlDoc.SelectSingleNode("//pages/first_page_name");
        if (first_page_name_node != null)
        {
            first_page_name_node.InnerText = first_page_name;
         }
        else
        {
            XmlNode pages_node = xmlDoc.SelectSingleNode("//pages");
            CreateNode(xmlDoc, pages_node, "first_page_name", first_page_name);
        }
        Util util = new Util();
        State["AppXmlDoc"] = xmlDoc;
        util.UpdateStagingAppXml(State);
    }
    public bool IsCurrentPageNameUsed(Hashtable State)
    {
        XmlDocument xmlDoc = GetStagingAppXml(State);
        if (xmlDoc == null)
            return false;
        XmlNodeList list = xmlDoc.SelectNodes("//submit");
        string search = State["SelectedAppPage"].ToString();
        foreach (XmlNode node in list)
        {
            if (node.InnerText.Contains(search))
            {
                XmlNode name = node.ParentNode.ParentNode.ParentNode.SelectSingleNode("name");
                if (name == null)
                    return false;
                if(name.InnerText != search)
                    return true;
            }
        }       
        return false;       
    }
    public void ReplacePageNameUsedInSubmits(Hashtable State, string old_page_name, string new_page_name)
    {
        XmlDocument xmlDoc = GetStagingAppXml(State);
        if (xmlDoc == null)
            return;
        XmlNodeList list = xmlDoc.SelectNodes("//submit");
        bool do_update = false;
        foreach (XmlNode node in list)
        {
            if (node.InnerText.Contains(old_page_name))
            {
                do_update = true;
                XmlNode text = node.FirstChild;//text node
                text.InnerText = text.InnerText.Replace(old_page_name, new_page_name);
                XmlNode nextPage = node.SelectSingleNode("next_page/page");
                if (nextPage != null)
                    nextPage.InnerText = new_page_name;
            }
        }
        if (do_update)
        {
            Util util = new Util();
            State["AppXmlDoc"] = xmlDoc;
            util.UpdateStagingAppXml(State);
        }
    }
    public bool IsFirstAppPage(Hashtable State, string page_name)
    {
        XmlDocument xmlDoc = GetStagingAppXml(State);
        if (xmlDoc == null)
            return true;
        XmlNode first_page_name = xmlDoc.SelectSingleNode("//pages/first_page_name");
        if (first_page_name != null)
        {
            return (first_page_name.InnerText==page_name) ? true : false;
        }
        else
        {
            first_page_name = xmlDoc.SelectSingleNode("//pages/page/name");
            if (first_page_name != null)
                return (first_page_name.InnerText == page_name) ? true : false;
            else
                return false;
        }
    }
    public string[] GetAppPageNames(Hashtable State, string application_name)
    {
        SortedList page_list = new SortedList();
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNodeList pages = xmlDoc.SelectNodes("//pages/page");
        bool do_update = false;
         foreach (XmlNode page in pages)
        {
            string name = page.SelectSingleNode("name").InnerText;
            XmlNode order_node = page.SelectSingleNode("order");
            if (order_node == null)
            {
                order_node = CreateNode(xmlDoc, page, "order", GetNextPageOrder(xmlDoc));
                do_update = true;
            }
            int order = Convert.ToInt32(order_node.InnerText);
            page_list[order] = name;

           
        }
        string[] names = new string[page_list.Count];
        int i=0;
        foreach (int key in page_list.Keys)
        {
            names[i++] = page_list[key].ToString();           
        }
        if (do_update)
        {
            Util util = new Util();
            State["AppXmlDoc"] = xmlDoc;
            util.UpdateStagingAppXml(State);
        }
        return names;
    }
    public string[] GetAppPageFields(Hashtable State, string application_name,string page_name)
    {
        XmlDocument doc = (XmlDocument)State["AppXmlDoc"];
        XmlNode page = doc.SelectSingleNode("//pages/page/name[.  ='" + page_name + "']").ParentNode;
        XmlNode fields = page.SelectSingleNode("fields");
        if (fields == null)
            return new string[0];
        string[] field_names = new string[fields.ChildNodes.Count];
        int index = 0;
        foreach (XmlNode child in fields.ChildNodes)
        {
            field_names[index++] = child.SelectSingleNode("id").InnerText + " (" + child.Name + ")" ;
        }
        return field_names;
    }
    public int GetProductionAppPageCount(Hashtable State)
    {
        XmlNodeList page_list = GetProductionPageNodes(State);
        return page_list.Count;
    }
    public int GetAppPageCount(Hashtable State)
    {
        XmlNodeList page_list = GetPageNodes(State);
        return page_list.Count;
    }
    public void CopyAppPage(Hashtable State, string page_to_copy, string new_page_name)
    {
        XmlDocument xmlDoc = GetStagingAppXml(State);

        XmlNode prev_page_name_node = xmlDoc.SelectSingleNode("//pages/page/name[.  ='" + page_to_copy + "']");
        if (prev_page_name_node != null)
        {
            XmlNode prev_page_node = prev_page_name_node.ParentNode;
            XmlNode new_page_node = prev_page_node.CloneNode(true);

            //modify prev node with new names
            new_page_node.SelectSingleNode("name").InnerText = new_page_name;
            new_page_node.SelectSingleNode("order").InnerText =  GetNextPageOrder(xmlDoc);
 
            XmlNodeList fields = new_page_node.SelectSingleNode("fields").ChildNodes;
            foreach (XmlNode field in fields)
            {
                string id = field.SelectSingleNode("id").InnerText;
                field.SelectSingleNode("id").InnerText = new_page_name.Replace(" ","_") + "_" + id;
                if(field.Name == "table")
                {
                  XmlNodeList table_field_names = field.SelectNodes("table_fields/table_field/name");
                  foreach (XmlNode table_field_name in table_field_names)
                  {
                      string name = table_field_name.InnerText;
                      table_field_name.InnerText = new_page_name.Replace(" ", "_") + "_" + name;
                  }
                }
                else if (field.Name == "picker")
                {
                    XmlNodeList picker_field_names = field.SelectNodes("picker_fields/picker_field/name");
                    foreach (XmlNode picker_field_name in picker_field_names)
                    {
                        string name = picker_field_name.InnerText;
                        picker_field_name.InnerText = new_page_name.Replace(" ", "_") + "_" + name;
                    }
                }
            }

            prev_page_node.ParentNode.AppendChild(new_page_node);


            Util util = new Util();
            State["AppXmlDoc"] = xmlDoc;
            util.UpdateStagingAppXml(State);

            HtmlToImage thumb = new HtmlToImage();
            string page_image_url = thumb.ConvertToImageLink(State, new_page_name, State["BackgroundHtml"].ToString());
            if (page_image_url != null)
                util.SaveAppPageImage(State, page_image_url);

        }
    }    

    public void RenameAppPage(Hashtable State, string prev_page_name, string new_page_name)
    {
        XmlDocument xmlDoc = GetStagingAppXml(State);

        //get first page first to see if that needs to be changed
        XmlNode first_page_name = xmlDoc.SelectSingleNode("//pages/first_page_name");
        if (first_page_name == null)
        {
            XmlNode pages = xmlDoc.SelectSingleNode("//pages");
            first_page_name = CreateNode(xmlDoc, pages, "first_page_name", new_page_name);
        }
        else if(prev_page_name ==  first_page_name.InnerText)
            first_page_name.InnerText = new_page_name;  

         XmlNode prev_page_name_node = xmlDoc.SelectSingleNode("//pages/page/name[.  ='" + prev_page_name + "']");
        if (prev_page_name_node != null)
        {
            prev_page_name_node.InnerText = new_page_name;
            Util util = new Util();
            State["AppXmlDoc"] = xmlDoc;
            util.UpdateStagingAppXml(State);
        }
    }
    public void MovePageDown(Hashtable State)
    {
        if(State["SelectedAppPage"] ==null)
            return;
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNode page_name_node = xmlDoc.SelectSingleNode("//pages/page/name[.  ='" + State["SelectedAppPage"].ToString() + "']");
        if (page_name_node != null)
        {
            SortedList sorted_list = new SortedList();
            XmlNodeList order_node_list = xmlDoc.SelectNodes("//pages/page/order");
            foreach (XmlNode order_node in order_node_list)
            {
                int order = Convert.ToInt32(order_node.InnerText);
                sorted_list[order] = order_node;
            }

            XmlNode page_node = page_name_node.ParentNode;
            XmlNode current_order_node = page_node.SelectSingleNode("order");
            string current_order = current_order_node.InnerText;

            bool take_next_order = false;
            string intended_order = null;
            foreach (DictionaryEntry entry in sorted_list)
            {
                if (take_next_order)
                {
                    intended_order = entry.Key.ToString();
                    break;
                }
                if (entry.Key.ToString() == current_order)
                {
                    take_next_order = true;
                }
            }
            if (intended_order == null)
                return; // the current is already at the end

            XmlNode exchange_node_order = page_node.SelectSingleNode("//pages/page/order[.  ='" + intended_order + "']");
            if (exchange_node_order == null) 
                return;

            exchange_node_order.InnerText = current_order;
            current_order_node.InnerText = intended_order;
            foreach (DictionaryEntry entry in sorted_list)
            {
                if (current_order == entry.Key.ToString())
                {
                    SetFirstPageName(State, exchange_node_order.ParentNode.SelectSingleNode("name").InnerText);
                }
                break;
            }

            Util util = new Util();
            State["AppXmlDoc"] = xmlDoc;
            util.UpdateStagingAppXml(State);
        }
    }
    public void MovePageUp(Hashtable State)
    {
        if (State["SelectedAppPage"] == null)
            return;
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNode page_name_node = xmlDoc.SelectSingleNode("//pages/page/name[.  ='" + State["SelectedAppPage"].ToString() + "']");
        if (page_name_node != null)
        {
            SortedList sorted_list = new SortedList();
            XmlNodeList order_node_list = xmlDoc.SelectNodes("//pages/page/order");
            foreach (XmlNode order_node in order_node_list)
            {
                int order = Convert.ToInt32(order_node.InnerText);
                sorted_list[order] = order_node;
            }

            XmlNode page_node = page_name_node.ParentNode;
            XmlNode current_order_node = page_node.SelectSingleNode("order");
            string current_order = current_order_node.InnerText;

            string previous_order = null;
            string intended_order = null;
            foreach (DictionaryEntry entry in sorted_list)
            { 
                if (entry.Key.ToString() == current_order)
                {
                    intended_order = previous_order;
                    break;
                }
                previous_order = entry.Key.ToString();

            }
            if (intended_order == null)
                return; // the current is already at the beginning

            XmlNode exchange_node_order = page_node.SelectSingleNode("//pages/page/order[.  ='" + intended_order + "']");
            exchange_node_order.InnerText = current_order;
            current_order_node.InnerText = intended_order;

            Util util = new Util();
            State["AppXmlDoc"] = xmlDoc;
            util.UpdateStagingAppXml(State);

            foreach (DictionaryEntry entry in sorted_list)
            {
                if (intended_order == entry.Key.ToString())
                {
                    SetFirstPageName(State, State["SelectedAppPage"].ToString());
                }
                break;
            }
         }
    }
    public void DeleteAppPage(Hashtable State, string page_name)
    {
        Util util = new Util();
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNode page_name_node = xmlDoc.SelectSingleNode("//pages/page/name[.  ='" + page_name + "']");
        if (page_name_node != null)
        {
            XmlNode page_node = page_name_node.ParentNode;
            XmlNode pages_node = page_node.ParentNode;
            pages_node.RemoveChild(page_node);
            State["AppXmlDoc"] = xmlDoc;
            util.UpdateStagingAppXml(State);
            util.DeleteAppPageImage(State, page_name);
        }

        //get first page first to see if that needs to be changed
        XmlNode first_page_name = xmlDoc.SelectSingleNode("//pages/first_page_name");
        if (first_page_name == null)
        {
            XmlNode pages = xmlDoc.SelectSingleNode("//pages");
            first_page_name = CreateNode(xmlDoc, pages, "first_page_name", "");
        }
        if (page_name == first_page_name.InnerText || first_page_name.InnerText == "")
        {
            //take first page we see
            XmlNode first_page_name_node = xmlDoc.SelectSingleNode("//pages/page/name");
            if (first_page_name_node != null)
            {
                first_page_name.InnerText = first_page_name_node.InnerText;
                State["AppXmlDoc"] = xmlDoc;
                util.UpdateStagingAppXml(State);
            }
        }
    }
    public string FilterCanvasOutput(string canvas_output)
    {
        try
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(UnescapeXml(canvas_output));
            StringBuilder clean_html = new StringBuilder();
            HtmlNodeCollection list = htmlDoc.DocumentNode.SelectNodes("//div[@title]");
            if (list == null)
                return "";
            foreach (HtmlNode node in list)
            {
                //remove incidental divs
                HtmlNodeCollection bad_nodes = new HtmlNodeCollection(node);
                foreach (HtmlNode child in node.ChildNodes)
                {
                    if (child.Name == "div" && child.Attributes["class"] != null && child.Attributes["class"].Value.Contains("resizable"))
                        bad_nodes.Append(child);
                }
                for (int i = 0; i < bad_nodes.Count; i++)
                {
                    bad_nodes[i].Remove();
                }

                clean_html.Append(node.OuterHtml);
            }
            return clean_html.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
    public string GetAppDeviceType(Hashtable State)
    {
        XmlDocument doc = null;
        if (State["AppXmlDoc"] != null)
            doc = (XmlDocument)State["AppXmlDoc"];
        else
        {
            Util util = new Util();
            doc = util.GetStagingAppXml(State);
            if (doc == null)
                return Constants.IPHONE;
        }

        XmlNode device_type = doc.SelectSingleNode("//device_type");
        if (device_type == null)
        {
            XmlNode configuration = doc.SelectSingleNode("//configuration");
            CreateNode(doc, configuration, "device_type", Constants.IPHONE);
            State["AppXmlDoc"] = doc;
            Util util = new Util();
            util.UpdateStagingAppXml(State);
            return Constants.IPHONE;
        }
        else
        {
            return device_type.InnerText;
        }
    }
    public string GetPageTransitionType(Hashtable State)
    {
        XmlDocument doc = null;
        if (State["AppXmlDoc"] != null)
            doc = (XmlDocument)State["AppXmlDoc"];
        else
        {
            Util util = new Util();
            doc = util.GetStagingAppXml(State);
            if (doc == null)
                return "slide";
        }

        XmlNode page_transition_type = doc.SelectSingleNode("//page_transition_type");
        if (page_transition_type == null)
        {
            XmlNode application = doc.SelectSingleNode("//application");
            CreateNode(doc, application, "page_transition_type", "slide");
            State["AppXmlDoc"] = doc;
            Util util = new Util();
            util.UpdateStagingAppXml(State);
            return "slide";
        }
        else
        {
            return page_transition_type.InnerText;
        }
    }
   
    public string GetFirstAppPage(Hashtable State)
    {
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNode first_page_name_node = xmlDoc.SelectSingleNode("//pages/first_page_name");
        string first_page_name = null;
        if (first_page_name_node != null)
        {
            first_page_name = first_page_name_node.InnerText;
        }
        else
        {
            first_page_name_node = xmlDoc.SelectSingleNode("//pages/page/name");
            if (first_page_name_node != null)
                first_page_name = first_page_name_node.InnerText;
            else
                return null;
        }
        State["SelectedAppPage"] = first_page_name;
        return GetAppPage(State, first_page_name);
    }
    public string GetAppPage(Hashtable State, string page_name)
    {
        try
        {
            XmlDocument xmlDoc = GetStagingAppXml(State);

            //get background image
            XmlNode configuration_node = xmlDoc.SelectSingleNode("//configuration");
            XmlNode background_node = configuration_node.SelectSingleNode("background");
            if (background_node == null)
            {
                background_node = CreateNode(xmlDoc, configuration_node, "background");
            }
            XmlNode background_image_node = background_node.SelectSingleNode("image_source");
            if (background_image_node == null)
            {
                background_image_node = CreateNode(xmlDoc, background_node, "image_source", "https://s3.amazonaws.com/MobiFlexImages/apps/images/backgrounds/standard_w_header_iphone.jpg");
            }
            string background_image = background_image_node.InnerText;
            if (background_image.Contains("customer_media") || background_image.Contains("s3.amazonaws.com"))
            {
                if (State["SelectedDeviceView"].ToString() == Constants.ANDROID_PHONE)
                    background_image = background_image.Replace("_iphone.", "_android.");
                State["BackgroundImageUrl"] = background_image;
            }
            else
            {
                background_image = background_image.Substring(background_image.LastIndexOf("/") + 1);
                if (State["SelectedDeviceView"].ToString() == Constants.ANDROID_PHONE)
                    background_image = background_image.Replace("_iphone.", "_android.");

                State["BackgroundImageUrl"] = "https://s3.amazonaws.com/MobiFlexImages/apps/images/backgrounds/" + background_image;
            }

            XmlNode page_name_node = xmlDoc.SelectSingleNode("//pages/page/name[.  ='" + page_name + "']");
            if (page_name_node == null)
                return "";

            double y_factor = 1.0D;
            Util util = new Util();
            if (State["SelectedDeviceType"].ToString() != State["SelectedDeviceView"].ToString())
            {
                y_factor = util.GetYFactor(State);
            }

            XmlNode page_node = page_name_node.ParentNode;
            XmlNode fields_node = page_node.SelectSingleNode("fields");
            if(fields_node == null)
                return "";

            XmlNodeList field_list = page_node.SelectSingleNode("fields").ChildNodes;
            HtmlDocument htmlDoc = new HtmlDocument();
            HtmlNode root = htmlDoc.DocumentNode;
            foreach (XmlNode field in field_list)
            {
                try
                {
                    //restrict certain cross use fields
                    if (State["SelectedAppType"].ToString() == Constants.NATIVE_APP_TYPE)
                    {
                        if (field.Name == "html_panel")
                            continue;
                    }

                    HtmlNode new_node = htmlDoc.CreateElement("div");
                    Hashtable field_map = ParseXmlToHtml(field);
                    SetCommonHtmlAttributes(htmlDoc, new_node, field_map, y_factor, field.Name);
                    HtmlAttribute title_attr = htmlDoc.CreateAttribute("title");
                    switch (field.Name)
                    {
                        case "image"://"<div title=\"MobiFlex Image\" id=\"{0}\"  type=\"get\" style=\"{2}\" ><img src=\"{1}\" style=\"height:100%;width:100%;\"/></div>"
                            title_attr.Value = "MobiFlex Image";
                            string image_url = field_map["image_source"].ToString();
                            if (!field_map.ContainsKey("width")) //the xml does not have the width and height so use the width and height of the actual image
                            {
                                Size size = util.GetImageSize(image_url);
                                if (size != null)
                                {
                                    new_node.Attributes["style"].Value += "width:" + size.Width.ToString() + "px;height:" + size.Height.ToString() + "px;";
                                }
                            }
                            AddImageNode(htmlDoc, new_node, image_url);
                            break;
                        case "audio"://<div title=\"MobiFlex Audio\" id=\"{0}\" source=\"{1}\" style=\"{2}\"  ><img src=\"images/editor_images/audio_field.png\" style=\"height:100%;width:100%;\"/></div>"
                            title_attr.Value = "MobiFlex Audio";
                            HtmlAttribute audio_source_attr = htmlDoc.CreateAttribute("source", field_map["audio_source"].ToString());
                            new_node.Attributes.Append(audio_source_attr);
                            AddImageNode(htmlDoc, new_node, "images/editor_images/audio_field.png");
                            break;
                        case "label": //"<div title=\"MobiFlex Label\" id=\"{0}\" style=\"{2}\" >{1}<img class=\"spacer\" src=\"images/spacer.gif\" style=\"position:relative;top:-16px;width:100%;height:100%\" /></div>"
                            title_attr.Value = "MobiFlex Label";
                            new_node.InnerHtml = field_map["text"].ToString();

                            //<img class=\"spacer\" src=\"images/spacer.gif\" style=\"position:relative;top:-16px;width:100%;height:100%\" />
                            HtmlNode label_img_node = htmlDoc.CreateElement("img");
                            HtmlAttribute label_img_src_attr = htmlDoc.CreateAttribute("src", "images/spacer.gif");
                            label_img_node.Attributes.Append(label_img_src_attr);

                            int label_font_size = -Convert.ToInt32(field_map["font_size"].ToString());
                            HtmlAttribute label_img_style_attr = htmlDoc.CreateAttribute("style", "position:relative;top:" + label_font_size.ToString() + "px;width:100%;height:100%");
                            label_img_node.Attributes.Append(label_img_style_attr);
                            new_node.AppendChild(label_img_node);
                            break;
                        case "text_field": //"<div title=\"MobiFlex TextField\" id=\"{0}\" type=\"{1}\" alt=\"{2}\"  style=\"{3}\" ><img src=\"images/editor_images/text_field.png\" style=\"height:100%;width:100%;\"/></div>"
                            title_attr.Value = "MobiFlex TextField";
                            AddImageNode(htmlDoc, new_node, "images/editor_images/text_field.png");
                            string alt = "keyboard:";
                            if (field_map.ContainsKey("keyboard"))
                                alt +=  field_map["keyboard"].ToString() + ";";
                            else
                                alt +=  "default;";

                            if (field_map.ContainsKey("validation"))
                                alt += "validation:" + field_map["validation"].ToString();
                            else
                                alt += "validation:none";
                            HtmlAttribute alt_attr = htmlDoc.CreateAttribute("alt", alt);
                            new_node.Attributes.Append(alt_attr);
                            HtmlAttribute type_attr = htmlDoc.CreateAttribute("type", field_map["type"].ToString());
                            new_node.Attributes.Append(type_attr);
                            if (field_map["text"] != null)
                            {
                                HtmlAttribute text_attr = htmlDoc.CreateAttribute("text", EncodeStudioHtml(UnescapeXml(field_map["text"].ToString())));
                                new_node.Attributes.Append(text_attr);
                            }
                            break;
                        case "html_panel": //"<div title=\"MobiFlex HtmlPanel\" id=\"{0}\" style=\"{2}\" ><div style=\"width:98%;height:98%;overflow:hidden;background-color:#ffffff\">{1}</div></div>"
                            title_attr.Value = "MobiFlex HtmlPanel";
                            new_node.InnerHtml = HttpUtility.HtmlDecode(field_map["html"].ToString());
                            /* HtmlNode container_node = htmlDoc.CreateElement("div");
                             HtmlAttribute panel_style_attr = htmlDoc.CreateAttribute("style", " width:98%;height:98%;overflow:hidden;background-color:#ffffff");
                             container_node.Attributes.Append(panel_style_attr);
                              container_node.InnerHtml = HttpUtility.HtmlDecode(field_map["html"].ToString());
                            new_node.AppendChild(container_node);*/
                            break;
                        case "web_view": //<div title=\"MobiFlex WebView\" alt=\"MobiFlex WebView\" id=\"{0}\" style=\"position:absolute;z-index:{2};top:44px;left:0px;height:416px;width:320px;\" url=\"{1}\" ><iframe src='" + src + "' width='99%' height='99.4%' marginheight='0' marginwidth='0' scrolling='no' style='border-width:0;' /></div>"
                            title_attr.Value = "MobiFlex WebView";
                            if (field_map["url"] == null)
                                field_map["url"] = "";
                            HtmlAttribute url_attr = htmlDoc.CreateAttribute("url", field_map["url"].ToString());
                            new_node.Attributes.Append(url_attr);
                            if (field_map["url"].ToString().Length > 0)
                            {
                                HtmlNode iframe_node = htmlDoc.CreateElement("iframe");
                                iframe_node.Attributes.Append(htmlDoc.CreateAttribute("src", field_map["url"].ToString()));
                                iframe_node.Attributes.Append(htmlDoc.CreateAttribute("width", "99%"));
                                iframe_node.Attributes.Append(htmlDoc.CreateAttribute("height", "99.4%"));
                                iframe_node.Attributes.Append(htmlDoc.CreateAttribute("marginheight", "0"));
                                iframe_node.Attributes.Append(htmlDoc.CreateAttribute("marginwidth", "0"));
                                iframe_node.Attributes.Append(htmlDoc.CreateAttribute("scrolling", "no"));
                                iframe_node.Attributes.Append(htmlDoc.CreateAttribute("style", "border-width:0;"));
                                new_node.AppendChild(iframe_node);
                            }
                            else
                            {
                                HtmlNode div_node = htmlDoc.CreateElement("div");
                                String webview_url = "images/editor_images/WebView.jpg";
                                if (State["SelectedDeviceType"].ToString() == Constants.IPAD ||
                                   State["SelectedDeviceType"].ToString() == Constants.ANDROID_TABLET)
                                    webview_url = "images/editor_images/WebView_ipad.jpg";
                                HtmlAttribute style2_attr = htmlDoc.CreateAttribute("style", "width:100%;height:100%;background-image:url('" + webview_url + "');background-repeat:no-repeat");
                                div_node.Attributes.Append(style2_attr);
                                new_node.AppendChild(div_node);
                            }
                            break;
                        case "button": //"<div title=\"MobiFlex Button\" style=\"{3}\" id=\"{0}\" align=\"center\"  submit=\"{1}\"><img src=\"https://s3.amazonaws.com/MobiFlexImages/apps/images/blank_buttons/medium_green_button.png\" style=\"width:100%;height:100%\" /><p style=\"position:relative;top:-38px;\">{2}</p></div>"
                            title_attr.Value = "MobiFlex Button";

                            //there are 2 coding schemes here: 
                            //1 css type coding
                            //2 xml type coding - the new one
                            //maintain backward comptibility
                            string submit = ";";
                            if (field_map.ContainsKey("submit"))
                            {
                                XmlNode submit_node = field.SelectSingleNode("submit");
                                if (submit_node.ChildNodes.Count > 1)
                                {
                                    StringBuilder sb_submit = new StringBuilder();
                                    sb_submit.Append(submit_node.LastChild.Name + ":");
                                    bool is_first = true;
                                    foreach (XmlNode submit_attribute in submit_node.LastChild.ChildNodes)
                                    {
                                        if (is_first)
                                            is_first = false;
                                        else
                                            sb_submit.Append(",");
                                        sb_submit.Append(submit_attribute.Name + "~" + submit_attribute.InnerText);
                                    }
                                    submit = sb_submit.ToString() + ";";
                                }
                                else
                                {
                                    submit = field_map["submit"].ToString() + ";";
                                    //bring to forward compatibility
                                    if (submit.StartsWith("post"))
                                    {
                                        if (submit.Length >= 19)
                                            //submit = submit.Remove(0, 19).Insert(0, "post:response_page~");
                                            submit = submit.Remove(0, 19).Insert(0, "post:");
                                    }
                                    else if (submit.Contains("next_page:"))
                                        submit = submit.Replace("next_page:", "next_page:page~");
                                    else if (submit.Contains("call:"))
                                        submit = submit.Replace("call:", "call:phone_field~");
                                }
                            }

                            if (field_map.ContainsKey("compute"))
                            {
                                string decode = DecodeComputeNode(field_map["compute"].ToString());
                                if (decode == null)
                                {
                                    return "Error: There may be an error in the compute field";
                                }
                                submit += "compute:" + decode + ";";
                            }

                            HtmlAttribute submit_attr = htmlDoc.CreateAttribute("submit", submit);
                            new_node.Attributes.Append(submit_attr);

                            //align=\"center\" 
                            HtmlAttribute align_attr = htmlDoc.CreateAttribute("align", "center");
                            new_node.Attributes.Append(align_attr);

                            //<img src=\"https://s3.amazonaws.com/MobiFlexImages/apps/images/blank_buttons/medium_green_button.png\" style=\"width:100%;height:100%\" />
                            HtmlNode img_node = htmlDoc.CreateElement("img");
                            HtmlAttribute img_src_attr = null;
                            if (field_map["image_source"] != null)
                                img_src_attr = htmlDoc.CreateAttribute("src", field_map["image_source"].ToString());
                            else
                                img_src_attr = htmlDoc.CreateAttribute("src", "https://s3.amazonaws.com/MobiFlexImages/apps/images/blank_buttons/medium_green_button.png");

                            img_node.Attributes.Append(img_src_attr);

                            HtmlAttribute img_style_attr = htmlDoc.CreateAttribute("style", "width:100%;height:100%");
                            img_node.Attributes.Append(img_style_attr);
                            new_node.AppendChild(img_node);

                            //<p style=\"position:relative;top:-38px;\">{2}</p>
                            HtmlNode p_node = htmlDoc.CreateElement("p");
                            HtmlNode text_node = null;
                            if (field_map["text"] != null)
                                text_node = htmlDoc.CreateTextNode(field_map["text"].ToString());
                            else
                                text_node = htmlDoc.CreateTextNode("");

                            p_node.AppendChild(text_node);
                            //top = (-ph/2) -(3*ah/2);
                            int height = Convert.ToInt32(field_map["height"].ToString());
                            int font_size = Convert.ToInt32(field_map["font_size"].ToString());
                            int top = (-height / 2) - (3 * font_size / 2);
                            HtmlAttribute style_attr = htmlDoc.CreateAttribute("style", "position:relative;top:" + top.ToString() + "px;");
                            p_node.Attributes.Append(style_attr);
                            new_node.AppendChild(p_node);
                            break;
                        case "image_button": //"<div title=\"MobiFlex ImageButton\"  style=\"{3}\" id=\"{0}\"  submit=\"{2}\"><img src=\"{1}\" style=\"height:100%;width:100%;\"/></div>"
                            title_attr.Value = "MobiFlex ImageButton";
                            string image_button_url = field_map["image_source"].ToString();
                            if (!field_map.ContainsKey("width")) //the xml does not have the width and height so use the width and height of the actual image
                            {
                                Size size = util.GetImageSize(image_button_url);
                                if (size != null)
                                {
                                    new_node.Attributes["style"].Value += "width:" + size.Width.ToString() + "px;height:" + size.Height.ToString() + "px;";
                                }
                            }
                            AddImageNode(htmlDoc, new_node, image_button_url);

                            //there are 2 coding schemes here: 
                            //1 css type coding
                            //2 xml type coding - the new one
                            //maintain backward comptibility
                            string image_button_submit = ";";
                            if (field_map.ContainsKey("submit"))
                            {
                                XmlNode submit_node = field.SelectSingleNode("submit");
                                if (submit_node.ChildNodes.Count > 1)
                                {
                                    StringBuilder sb_submit = new StringBuilder();
                                    sb_submit.Append(submit_node.LastChild.Name + ":");
                                    bool is_first = true;
                                    foreach (XmlNode submit_attribute in submit_node.LastChild.ChildNodes)
                                    {
                                        if (is_first)
                                            is_first = false;
                                        else
                                            sb_submit.Append(",");
                                        sb_submit.Append(submit_attribute.Name + "~" + submit_attribute.InnerText);
                                    }
                                    image_button_submit = sb_submit.ToString() + ";";
                                }
                                else
                                {
                                    image_button_submit = field_map["submit"].ToString() + ";";
                                    //bring to forward compatibility
                                    if (image_button_submit.StartsWith("post"))
                                    {
                                        if (image_button_submit.Length >= 19)
                                           // image_button_submit = image_button_submit.Remove(0, 19).Insert(0, "post:response_page~");
                                            image_button_submit = image_button_submit.Remove(0, 19).Insert(0, "post:");
                                    }
                                    else if (image_button_submit.Contains("next_page:"))
                                        image_button_submit = image_button_submit.Replace("next_page:", "next_page:page~");
                                    else if (image_button_submit.Contains("call:"))
                                        image_button_submit = image_button_submit.Replace("call:", "call:phone_field~");
                                }
                            }
                            if (field_map.ContainsKey("compute"))
                            {
                                string decode = DecodeComputeNode(field_map["compute"].ToString());
                                if (decode == null)
                                {
                                    return "Error: There may be an error in the compute field";
                                }
                                image_button_submit += "compute:" + decode + ";";
                              }

                            HtmlAttribute image_button_submit_attr = htmlDoc.CreateAttribute("submit", image_button_submit);
                            new_node.Attributes.Append(image_button_submit_attr);
                            break;
                        case "picker": //"<div title=\"MobiFlex Picker\" style=\"position:absolute;z-index:{3};top:44px;left:0px;width:320px; height:216px;background-image:url('images/editor_images/datepicker.jpg');background-repeat:no-repeat\" id=\"{0}\"  type=\"{1}\" options=\"\"  />"
                            title_attr.Value = "MobiFlex Picker";
                            string picker_type = field_map["picker_type"].ToString();
                            if (State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE &&
                                picker_type != "date" &&
                                picker_type != "time" &&
                                picker_type != "1_section")
                            {
                                picker_type = "1_section";
                            }
                            HtmlAttribute picker_type_attr = htmlDoc.CreateAttribute("type", picker_type);
                            new_node.Attributes.Append(picker_type_attr);
                            if((State["SelectedDeviceView"] != null && State["SelectedDeviceView"].ToString() == Constants.ANDROID_PHONE) ||
                                (State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE))
                            {
                                switch (picker_type)
                                {
                                    case "date":
                                    case "time":
                                    case "1_section":
                                        new_node.Attributes["style"].Value += "background-image:url('images/editor_images/spinner.jpg');background-repeat:no-repeat;";
                                        new_node.Attributes["style"].Value = new_node.Attributes["style"].Value.Replace("height:216px", "height:40px");
                                        break;
                                    case "2_sections":
                                    case "3_sections":
                                    case "4_sections":
                                        new_node.Attributes["style"].Value += "background-image:url('images/editor_images/2section_spinner.jpg');background-repeat:no-repeat;";
                                        break;
                                }
                            }
                            else
                            {
                                switch (picker_type)
                                {
                                    case "date":
                                        new_node.Attributes["style"].Value += "background-image:url('images/editor_images/datepicker.jpg');background-repeat:no-repeat;";
                                        break;
                                    case "time":
                                        new_node.Attributes["style"].Value += "background-image:url('images/editor_images/time_picker.jpg');background-repeat:no-repeat;";
                                        break;
                                    case "1_section":
                                        new_node.Attributes["style"].Value += "background-image:url('images/editor_images/1section_picker.jpg');background-repeat:no-repeat;";
                                        break;
                                    case "2_sections":
                                        new_node.Attributes["style"].Value += "background-image:url('images/editor_images/2section_picker.jpg');background-repeat:no-repeat;";
                                        break;
                                    case "3_sections":
                                        new_node.Attributes["style"].Value += "background-image:url('images/editor_images/3section_picker.jpg');background-repeat:no-repeat;";
                                        break;
                                    case "4_sections":
                                        new_node.Attributes["style"].Value += "background-image:url('images/editor_images/4section_picker.jpg');background-repeat:no-repeat;";
                                        break;
                                }
                            }
                            StringBuilder name_list = new StringBuilder();

                            if (picker_type.Contains("section"))
                            {
                                XmlNodeList picker_fields = field.SelectNodes("picker_fields/picker_field");
                                StringBuilder option_list = new StringBuilder();
                                bool isFirst = true;
                                foreach (XmlNode picker_field in picker_fields)
                                {
                                    XmlNode name_node = picker_field.SelectSingleNode("name");
                                    if (isFirst)
                                        isFirst = false;
                                    else
                                    {
                                        name_list.Append(",");
                                        option_list.Append("|");
                                    }
                                    name_list.Append(name_node.InnerText);
                                    XmlNode options_node = picker_field.SelectSingleNode("options");
                                    if (options_node != null)
                                        option_list.Append(options_node.InnerText);
                                    if (State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE)
                                        break;
                                }                               
 
                                HtmlAttribute picker_options = htmlDoc.CreateAttribute("options", EncodeStudioHtml(UnescapeXml(option_list.ToString())));
                                new_node.Attributes.Append(picker_options);

                                picker_type_attr.Value += ":" + name_list.ToString();
                            }
                            break;
                        case "text_area": //"position:absolute;z-index:{0};top:44px;left:10px;width:250px;height:300px;font-family:Verdana;font-size:16px;color:#000000;font-style:normal;font-weight:normal;text-decoration:none;padding:5px;background-image:url('images/editor_images/text_area.png');background-repeat:no-repeat;background-size:100% 100%;overflow:hidden"
                            title_attr.Value = "MobiFlex TextArea";
                            new_node.Attributes["style"].Value += "background-image:url('images/editor_images/text_area.png');background-repeat:no-repeat;background-size:100% 100%;overflow:hidden;";
                            if (field_map.ContainsKey("text"))
                            {
                                HtmlAttribute text_attr = htmlDoc.CreateAttribute("text", EncodeStudioHtml(UnescapeXml(field_map["text"].ToString())));
                                new_node.Attributes.Append(text_attr);
                                new_node.InnerHtml = "<div style=\"padding:5px\">" + field_map["text"].ToString().Replace(@"\n", "<br/>") + "</div>";
                            }
                            string edit_type = "non_editable";
                            if (field_map.ContainsKey("edit_type"))
                            {
                                edit_type = field_map["edit_type"].ToString();
                            }
                            HtmlAttribute edit_type_attr = htmlDoc.CreateAttribute("type", edit_type);
                            new_node.Attributes.Append(edit_type_attr);
                            break;
                        case "switch": //"<div title=\"MobiFlex Switch\" id=\"{0}\" style=\"{4}\" type=\"{1}\" value=\"{2}\" submit=\"{3}\" ><img src=\"" + image_url + "\" style=\"height:100%;width:100%;\"/></div>"
                            title_attr.Value = "MobiFlex Switch";
                            string switch_value = field_map["default_value"].ToString();
                            switch (switch_value)
                            {
                                case "on":
                                    AddImageNode(htmlDoc, new_node, "images/editor_images/switch_on.png");
                                     break;
                                case "off":
                                    AddImageNode(htmlDoc, new_node, "images/editor_images/switch_off.png");
                                    break;
                                case "yes":
                                    AddImageNode(htmlDoc, new_node, "images/editor_images/switch_yes.png");
                                    break;
                                case "no":
                                    AddImageNode(htmlDoc, new_node, "images/editor_images/switch_no.png");
                                    break;
                                case "true":
                                    AddImageNode(htmlDoc, new_node, "images/editor_images/switch_true.png");
                                    break;
                                case "false":
                                    AddImageNode(htmlDoc, new_node, "images/editor_images/switch_false.png");
                                    break;
                            }

                            //there are 2 coding schemes here: 
                            //1 css type coding
                            //2 xml type coding - the new one
                            //maintain backward comptibility
                            string switch_submit = ";";
                            if (field_map.ContainsKey("submit"))
                            {
                                XmlNode submit_node = field.SelectSingleNode("submit");
                                if (submit_node.ChildNodes.Count > 1)
                                {
                                    StringBuilder sb_submit = new StringBuilder();
                                    sb_submit.Append(submit_node.LastChild.Name + ":");
                                    bool is_first = true;
                                    foreach (XmlNode submit_attribute in submit_node.LastChild.ChildNodes)
                                    {
                                        if (is_first)
                                            is_first = false;
                                        else
                                            sb_submit.Append(",");
                                        sb_submit.Append(submit_attribute.Name + "~" + submit_attribute.InnerText);
                                    }
                                    switch_submit = sb_submit.ToString() + ";";
                                }
                                else
                                {
                                    switch_submit = field_map["submit"].ToString() + ";";
                                    //bring to forward compatibility
                                    if (switch_submit.StartsWith("post"))
                                    {
                                        if (switch_submit.Length >= 19)
                                            //switch_submit = switch_submit.Remove(0, 19).Insert(0, "post:response_page~");
                                             switch_submit = switch_submit.Remove(0, 19).Insert(0, "post:");
                                    }
                                    else if (switch_submit.Contains("next_page:"))
                                        switch_submit = switch_submit.Replace("next_page:", "next_page:page~");
                                    else if (switch_submit.Contains("call:"))
                                        switch_submit = switch_submit.Replace("call:", "call:phone_field~");
                                }
                            }
                            if (field_map.ContainsKey("compute"))
                            {
                                string decode = DecodeComputeNode(field_map["compute"].ToString());
                                if (decode == null)
                                {
                                    return "Error: There may be an error in the compute field";
                                }
                                switch_submit += "compute:" + decode + ";";
                            }

                            HtmlAttribute switch_type_attr = null;
                            if (field_map["type"] != null)                            
                                switch_type_attr = htmlDoc.CreateAttribute("type", field_map["type"].ToString());
                            
                            else
                                switch_type_attr = htmlDoc.CreateAttribute("type", "on_off");
                           
                            new_node.Attributes.Append(switch_type_attr);
 
                            HtmlAttribute value_attr = htmlDoc.CreateAttribute("value", field_map["default_value"].ToString());
                            new_node.Attributes.Append(value_attr);

                            HtmlAttribute switch_submit_attr = htmlDoc.CreateAttribute("submit", switch_submit);
                            new_node.Attributes.Append(switch_submit_attr);
                            break;
                        case "checkbox": //"<div title=\"MobiFlex Switch\" id=\"{0}\" style=\"{4}\" type=\"{1}\" value=\"{2}\" submit=\"{3}\" ><img src=\"" + image_url + "\" style=\"height:100%;width:100%;\"/></div>"
                            title_attr.Value = "MobiFlex CheckBox";
                            string checkbox_value = field_map["default_value"].ToString();
                            switch (checkbox_value)
                            {
                                case "checked":
                                    AddImageNode(htmlDoc, new_node, "images/editor_images/checkbox_on.png");
                                    break;
                                case "unchecked":
                                    AddImageNode(htmlDoc, new_node, "images/editor_images/checkbox_off.png");
                                    break;
                            }

                            //there are 2 coding schemes here: 
                            //1 css type coding
                            //2 xml type coding - the new one
                            //maintain backward comptibility
                            string checkbox_submit = ";";
                            if (field_map.ContainsKey("submit"))
                            {
                                XmlNode submit_node = field.SelectSingleNode("submit");
                                if (submit_node.ChildNodes.Count > 1)
                                {
                                    StringBuilder sb_submit = new StringBuilder();
                                    sb_submit.Append(submit_node.LastChild.Name + ":");
                                    bool is_first = true;
                                    foreach (XmlNode submit_attribute in submit_node.LastChild.ChildNodes)
                                    {
                                        if (is_first)
                                            is_first = false;
                                        else
                                            sb_submit.Append(",");
                                        sb_submit.Append(submit_attribute.Name + "~" + submit_attribute.InnerText);
                                    }
                                    checkbox_submit = sb_submit.ToString() + ";";
                                }
                                else
                                {
                                    checkbox_submit = field_map["submit"].ToString() + ";";
                                    //bring to forward compatibility
                                    if (checkbox_submit.StartsWith("post"))
                                    {
                                        if (checkbox_submit.Length >= 19)
                                            //checkbox_submit = checkbox_submit.Remove(0, 19).Insert(0, "post:response_page~");
                                            checkbox_submit = checkbox_submit.Remove(0, 19).Insert(0, "post:");
                                    }
                                    else if (checkbox_submit.Contains("next_page:"))
                                        checkbox_submit = checkbox_submit.Replace("next_page:", "next_page:page~");
                                    else if (checkbox_submit.Contains("call:"))
                                        checkbox_submit = checkbox_submit.Replace("call:", "call:phone_field~");
                                }
                            }
                            if (field_map.ContainsKey("compute"))
                            {
                                string decode = DecodeComputeNode(field_map["compute"].ToString());
                                if (decode == null)
                                {
                                    return "Error: There may be an error in the compute field";
                                }
                                checkbox_submit += "compute:" + decode + ";";
                            }

                            HtmlAttribute checkbox_value_attr = htmlDoc.CreateAttribute("value", field_map["default_value"].ToString());
                            new_node.Attributes.Append(checkbox_value_attr);

                            HtmlAttribute checkbox_submit_attr = htmlDoc.CreateAttribute("submit", checkbox_submit);
                            new_node.Attributes.Append(checkbox_submit_attr);
                            break;
                        case "slider": //"<div title=\"MobiFlex Slider\" id=\"{0}\" value=\"{2}\" style=\"z-index:{3};top:160px;left:10px;height:57px;width:283px;\" type=\"{1}\" ><img src=\"images/editor_images/horizontal_slider.png\" style=\"height:100%;width:100%;\"/></div>"
                            title_attr.Value = "MobiFlex Slider";
                            string slider_type = field_map["type"].ToString();
                            HtmlAttribute slider_type_attr = htmlDoc.CreateAttribute("type", slider_type);
                            new_node.Attributes.Append(slider_type_attr);
                            switch (slider_type)
                            {
                                case "horizontal":
                                    AddImageNode(htmlDoc, new_node, "images/editor_images/horizontal_slider.png");
                                    break;
                                case "vertical":
                                    AddImageNode(htmlDoc, new_node, "images/editor_images/vertical_slider.png");
                                    break;
                            }
                            string min_value = field_map["min_value"].ToString();
                            string max_value = field_map["max_value"].ToString();
                            HtmlAttribute slider_value_attr = htmlDoc.CreateAttribute("value", min_value + ":" + max_value);
                            new_node.Attributes.Append(slider_value_attr);
                            break;
                        case "table": //"<div title=\"MobiFlex Table\" id=\"{0}\" name=\"{1}\" alt=\"MobiFlex Table\"  style=\"position:absolute;z-index:{5};top:44px;left:0px;height:416px;width:320px;background-image:url('images/editor_images/largetableview1textfield.jpg');background-repeat:no-repeat\"  fields=\"{2}\" options=\"{4}\" submit=\"{3}\" />"
                            title_attr.Value = "MobiFlex Table";
                            string display_name = field_map["id"].ToString();
                            if (field_map["display_name"] != null)
                            {
                                display_name = field_map["display_name"].ToString();
                            }
                            HtmlAttribute table_display_name_attr = htmlDoc.CreateAttribute("name", display_name);
                            new_node.Attributes.Append(table_display_name_attr);

                            string table_type = field_map["table_type"].ToString();
                            if (table_type.Contains("image1text") || table_type.Contains("imagetext") || table_type.Contains("image1texthidden"))
                                new_node.Attributes["style"].Value += "background-image:url('images/editor_images/LargeTableView1Image1textField.jpg');background-repeat:no-repeat;";
                            else if (table_type.Contains("image2texts") || table_type.Contains("image2textshidden") )
                                new_node.Attributes["style"].Value += "background-image:url('images/editor_images/LargeTableView1Image2TextFields.jpg');background-repeat:no-repeat;";
                            else if (table_type.Contains("1text") || table_type.Contains("1texthidden"))
                                new_node.Attributes["style"].Value += "background-image:url('images/editor_images/LargeTableView1TextField.jpg');background-repeat:no-repeat;";
                            else if (table_type.Contains("2texts") || table_type.Contains("2textshidden"))
                                new_node.Attributes["style"].Value += "background-image:url('images/editor_images/LargeTableView2TextFields.jpg');background-repeat:no-repeat;";

                            XmlNodeList table_fields = field.SelectNodes("table_fields/table_field");
                            StringBuilder type_list = new StringBuilder();
                            StringBuilder field_name_list = new StringBuilder();
                            StringBuilder table_option_list = new StringBuilder();
                           bool is_first_field = true;
                            foreach (XmlNode table_field in table_fields)
                            {
                                if (is_first_field)
                                    is_first_field = false;
                                else
                                {
                                    field_name_list.Append(",");
                                    type_list.Append( ",");
                                }
                                XmlNode field_name_node = table_field.SelectSingleNode("name");
                                field_name_list.Append(field_name_node.InnerText );
                                XmlNode type_node = table_field.SelectSingleNode("type");
                                type_list.Append(type_node.InnerText );
                                XmlNode options_node = table_field.SelectSingleNode("options");
                                if (options_node != null)
                                    table_option_list.Append(options_node.InnerText + "|");
                            }
                            if (table_option_list.Length > 0)
                                table_option_list.Remove(table_option_list.Length - 1, 1);

                            string fields = table_type + "|" + type_list.ToString() + ":" + field_name_list.ToString();
                            HtmlAttribute fields_attr = htmlDoc.CreateAttribute("fields", fields);
                            new_node.Attributes.Append(fields_attr);

                            //there are 2 coding schemes here: 
                            //1 css type coding
                            //2 xml type coding - the new one
                            //maintain backward comptibility
                            string table_submit = ";";
                            if (field_map.ContainsKey("submit"))
                            {
                                XmlNode submit_node = field.SelectSingleNode("submit");
                                if (submit_node.ChildNodes.Count > 1)
                                {
                                    StringBuilder sb_submit = new StringBuilder();
                                    sb_submit.Append(submit_node.LastChild.Name + ":");
                                    bool is_first = true;
                                    foreach (XmlNode submit_attribute in submit_node.LastChild.ChildNodes)
                                    {
                                        if (is_first)
                                            is_first = false;
                                        else
                                            sb_submit.Append(",");
                                        sb_submit.Append(submit_attribute.Name + "~" + submit_attribute.InnerText);
                                    }
                                    table_submit = sb_submit.ToString() + ";";
                                }
                                else
                                {
                                    table_submit = field_map["submit"].ToString() + ";";
                                    //bring to forward compatibility
                                    if (table_submit.StartsWith("post"))
                                    {
                                        if (table_submit.Length >= 19)
                                           // table_submit = table_submit.Remove(0, 19).Insert(0, "post:response_page~");
                                             table_submit = table_submit.Remove(0, 19).Insert(0, "post:");
                                    }
                                    else if (table_submit.Contains("next_page:"))
                                        table_submit = table_submit.Replace("next_page:", "next_page:page~");
                                    else if (table_submit.Contains("call:"))
                                        table_submit = table_submit.Replace("call:", "call:phone_field~");
                                }
                            }
                            if (field_map.ContainsKey("compute"))
                            {
                                string decode = DecodeComputeNode(field_map["compute"].ToString());
                                if (decode == null)
                                {
                                    return "Error: There may be an error in the compute field";
                                }
                                table_submit += "compute:" + decode + ";";
                            }

                            HtmlAttribute table_submit_attr = htmlDoc.CreateAttribute("submit", table_submit);
                            new_node.Attributes.Append(table_submit_attr);
                            if (table_option_list.Length > 0)
                            {
                                HtmlAttribute table_options = htmlDoc.CreateAttribute("options", EncodeStudioHtml(UnescapeXml(table_option_list.ToString())));
                                new_node.Attributes.Append(table_options);
                            }
                            break;
                        case "alert": //"<div title=\"MobiFlex Alert\" id=\"{0}\" style=\"z-index:{1};top:410px;left:270px;height:50px;width:50px;\"  ><img src=\"images/editor_images/alert.png\" style=\"height:100%;width:100%;\"/></div>"
                            title_attr.Value = "MobiFlex Alert";
                            AddImageNode(htmlDoc, new_node, "images/editor_images/alert.png");
                            break;
                        case "hidden_field": //"<div title=\"MobiFlex HiddenField\" id=\"{0}\" value=\"{1}\" style=\"position:absolute;z-index:{2};top:410px;left:0px;height:30px;width:30px;\"  ><img src=\"images/editor_images/hidden_field.png\" style=\"height:100%;width:100%;\"
                            title_attr.Value = "MobiFlex HiddenField";
                            if (!field_map.ContainsKey("value"))
                            {
                                HtmlAttribute hidden_value_attr = htmlDoc.CreateAttribute("value", "");
                                new_node.Attributes.Append(hidden_value_attr);
                            }
                            AddImageNode(htmlDoc, new_node, "images/editor_images/hidden_field.png");
                            break;
                        case "gps": //"<div title=\"MobiFlex GPS\" id=\"GPS\" alt=\"{0}\" style=\"z-index:{1};top:160px;left:10px;height:58px;width:56px;\"  ><img src=\"images/editor_images/gps.png\" style=\"height:100%;width:100%;\"/></div>"
                            title_attr.Value = "MobiFlex GPS";
                            AddImageNode(htmlDoc, new_node, "images/editor_images/gps.png");
                            string latitude = field_map["latitude"].ToString();
                            string longitude = field_map["longitude"].ToString();
                            HtmlAttribute gps_attr = htmlDoc.CreateAttribute("alt", latitude + ";" + longitude);
                            new_node.Attributes.Append(gps_attr);
                            break;
                        case "map": //"<div title=\"MobiFlex Map\" alt=\"MobiFlex Map\" id=\"{0}\" style=\"position:absolute;z-index:{2};top:44px;left:0px;height:416px;width:320px;background-image:url('images/editor_images/map.jpg');background-repeat:no-repeat\"  url=\"{1}\" />"
                            title_attr.Value = "MobiFlex Map";
                            new_node.Attributes["style"].Value += "background-image:url('images/editor_images/map.jpg');background-repeat:no-repeat;";
                            break;
                        case "photo": //"<div title=\"MobiFlex Photo\" id=\"{0}\" compression=\"{1}\" icon_field=\"{2}\" style=\"z-index:{3};top:160px;left:10px;height:48px;width:48px;\"  ><img src=\"images/editor_images/picture_taker.gif\" style=\"height:100%;width:100%;\"/></div>"
                            title_attr.Value = "MobiFlex Photo";
                            AddImageNode(htmlDoc, new_node, "images/editor_images/picture_taker.gif");
                            break;
                        case "audio_recorder": //"<div title=\"MobiFlex AudioRecorder\" id=\"{0}\" style=\"z-index:{1};top:160px;left:10px;height:84px;width:198px;\"  ><img src=\"images/editor_images/audio_recorder.png\" style=\"height:100%;width:100%;\"/></div>"
                            title_attr.Value = "MobiFlex AudioRecorder";
                            AddImageNode(htmlDoc, new_node, "images/editor_images/audio_recorder.png");
                            break;
                        case "speech_recognition": // "<div title=\"MobiFlex SpeechRecognition\" alt=\"MobiFlex Speech Recognition\" id=\"{0}\" style=\"z-index:{2};top:48px;left:0px;height:412px;width:320px;\" choice_title=\"{1}\"  ><img src=\"images/editor_images/speech_recognition.png\" style=\"height:100%;width:100%;\"/></div>"
                            title_attr.Value = "MobiFlex SpeechRecognition";
                            AddImageNode(htmlDoc, new_node, "images/editor_images/speech_recognition.png");
                            break;


                    }
                    new_node.Attributes.Append(title_attr);

                    root.AppendChild(new_node);
                }
                catch (Exception ex)
                {
                    util.LogError(State, ex);
                    throw new Exception(ex.Message + ": " + ex.StackTrace);
                }
            }
            try
            {
                return htmlDoc.DocumentNode.WriteContentTo();
            }
            catch //work around bug in 3rd party package
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
   
    public Hashtable ParseXmlToHtml(XmlNode field)
    {
        Hashtable field_map = new Hashtable();
        XmlNodeList attributes = field.ChildNodes;
        foreach (XmlNode attribute in attributes)
        {
            field_map[attribute.Name.ToLower()] = attribute.InnerText;
        }
        return field_map;
    }
    public void SetCommonHtmlAttributes(HtmlDocument htmlDoc,  HtmlNode new_node, Hashtable field_map,double y_factor,string field_type)
    {
        //init style
        HtmlAttribute style = htmlDoc.CreateAttribute("style","position:absolute;");
        new_node.Attributes.Append(style);
        HtmlAttribute icon_field = null;

        //set default z-index
        if(!field_map.ContainsKey("z_index"))
            field_map["z_index"] = "50";
        if (field_type == "text_field" || field_type == "text_area" || field_type == "label" || field_type == "button")
        {
            if (!field_map.ContainsKey("font_style"))
                field_map["font_style"] = "normal";
            if (!field_map.ContainsKey("font_weight"))
                field_map["font_weight"] = "normal";
            if (!field_map.ContainsKey("text_decoration"))
                field_map["text_decoration"] = "none";
        }
       
        foreach (string key in field_map.Keys)
        {
            switch (key)
            {
                case "icon_field":
                    if (new_node.Attributes["icon_field"] == null)
                    {
                        icon_field = htmlDoc.CreateAttribute("icon_field", "field:" + field_map[key].ToString() + ";");
                        new_node.Attributes.Append(icon_field);
                    }
                    else
                        new_node.Attributes["icon_field"].Value += "field:" + field_map[key].ToString() + ";";
                    break;
                case "icon_width":
                    if (new_node.Attributes["icon_field"] == null)
                    {
                        icon_field = htmlDoc.CreateAttribute("icon_field", "width:" + field_map[key].ToString() + ";");
                        new_node.Attributes.Append(icon_field);
                    }
                    else
                         new_node.Attributes["icon_field"].Value += "width:" + field_map[key].ToString() + ";";
                    break;
                case "icon_height":
                    if (new_node.Attributes["icon_field"] == null)
                    {
                        icon_field = htmlDoc.CreateAttribute("icon_field", "height:" + field_map[key].ToString() + ";");
                        new_node.Attributes.Append(icon_field);
                    }
                    else
                         new_node.Attributes["icon_field"].Value += "height:" + field_map[key].ToString() + ";";
                    break;
                case "compression":
                case "url":
                case "alt":
                case "title":
                case "id":
                case "class":
                case "type":
                case "src":
                    HtmlAttribute attr = htmlDoc.CreateAttribute(key, field_map[key].ToString());
                    new_node.Attributes.Append(attr);
                    break;
                case "value":
                    HtmlAttribute val_attr = htmlDoc.CreateAttribute(key, EncodeStudioHtml(UnescapeXml(field_map[key].ToString())));
                    new_node.Attributes.Append(val_attr);
                    break;
                case "top":
                case "height":
                    if (y_factor == 1.0D)
                        new_node.Attributes["style"].Value += key + ":" + field_map[key].ToString() + "px;";
                    else
                    {
                        double y = Math.Round(Convert.ToDouble(field_map[key].ToString()) * y_factor);
                        new_node.Attributes["style"].Value += key + ":" + y.ToString() + "px;";
                    }
                    break;
                case "left":
                case "width":
                case "font_size":
                    new_node.Attributes["style"].Value +=  key.Replace("_", "-") + ":" + field_map[key].ToString() + "px;";
                    break;
                case "z_index":
                case "font_family":
                case "color":
                case "font_style":
                case "font_weight":
                case "text_decoration":
                case "background_color":
                case "overflow":
                    new_node.Attributes["style"].Value += key.Replace("_","-") + ":" + field_map[key].ToString() + ";";
                   break;
             }
        }
    }
   
    public void AddImageNode(HtmlDocument htmlDoc,HtmlNode new_node,string image_source)
    {
        //<img src=\"{1}\" style=\"height:100%;width:100%;\"/>
        HtmlNode image_node = htmlDoc.CreateElement("img");
        HtmlAttribute src_attr = htmlDoc.CreateAttribute("src", image_source);
        image_node.Attributes.Append(src_attr);
        HtmlAttribute style_attr = htmlDoc.CreateAttribute("style", "height:100%;width:100%;");
        image_node.Attributes.Append(style_attr);
        new_node.AppendChild(image_node);
    }
    public void AddImageConstantHeightNode(HtmlDocument htmlDoc, HtmlNode new_node, string image_source)
    {
        //<img src=\"{1}\" style=\"height:100%;width:100%;\"/>
        HtmlNode image_node = htmlDoc.CreateElement("img");
        HtmlAttribute src_attr = htmlDoc.CreateAttribute("src", image_source);
        image_node.Attributes.Append(src_attr);
        HtmlAttribute style_attr = htmlDoc.CreateAttribute("style", "width:100%;");
        image_node.Attributes.Append(style_attr);
        new_node.AppendChild(image_node);
    }
    public void SetAppType(Hashtable State,string app_type)
    {
        XmlDocument doc = GetStagingAppXml(State);
        State["AppXmlDoc"] = doc;
        XmlNode application_node = doc.SelectSingleNode("//application");
        XmlNode type_node = application_node.SelectSingleNode("type");
        if (type_node == null)
            CreateNode(doc, application_node, "type", app_type);
        else
            type_node.InnerText = app_type;

        Util util = new Util();
        util.UpdateStagingAppXml(State);
    }
    public void SetPageTransitionType(Hashtable State, string page_transition_type)
    {
        XmlDocument doc = GetStagingAppXml(State);
        State["AppXmlDoc"] = doc;

        XmlNode application = doc.SelectSingleNode("//application");

        //set scale
        XmlNode device_type_node = application.SelectSingleNode("page_transition_type");
        if (device_type_node == null)
            device_type_node = CreateNode(doc, device_type_node, "page_transition_type");

        device_type_node.InnerText = page_transition_type;
        Util util = new Util();
        util.UpdateStagingAppXml(State);
    }

    public void SetAppDeviceType(Hashtable State,
        string previous_device_design, string device_design)
    {
        XmlDocument doc = GetStagingAppXml(State);
        State["AppXmlDoc"] = doc;

        XmlNode configuration_node = doc.SelectSingleNode("//configuration");

        //set scale
        XmlNode device_type_node = configuration_node.SelectSingleNode("device_type");
        if (device_type_node == null)
            device_type_node = CreateNode(doc, configuration_node, "device_type");

        device_type_node.InnerText = device_design;

        XmlNode device_design_height_node = configuration_node.SelectSingleNode("device_design_height");
        if (device_design_height_node == null)
            device_design_height_node = CreateNode(doc, configuration_node, "device_design_height", Constants.IPHONE_DISPLAY_HEIGHT_S);

        XmlNode device_design_width_node = configuration_node.SelectSingleNode("device_design_width");
        if (device_design_width_node == null)
            device_design_width_node = CreateNode(doc, configuration_node, "device_design_width", Constants.IPHONE_DISPLAY_WIDTH_S);

        switch (device_design)
        {
            case Constants.IPHONE:
                device_type_node.InnerText = Constants.IPHONE;
                device_design_width_node.InnerText = Constants.IPHONE_DISPLAY_WIDTH_S;
                device_design_height_node.InnerText = Constants.IPHONE_DISPLAY_HEIGHT_S;
                break;
            case Constants.ANDROID_PHONE:
                device_type_node.InnerText = Constants.ANDROID_PHONE;
                device_design_width_node.InnerText = Constants.ANDROID_PHONE_DISPLAY_WIDTH_S;
                device_design_height_node.InnerText = Constants.ANDROID_PHONE_DISPLAY_HEIGHT_S;
                break;
            case Constants.IPAD:
                device_type_node.InnerText = Constants.IPAD;
                device_design_width_node.InnerText = Constants.IPAD_DISPLAY_WIDTH_S;
                device_design_height_node.InnerText = Constants.IPAD_DISPLAY_HEIGHT_S;
                break;
            case Constants.ANDROID_TABLET:
                device_type_node.InnerText = Constants.ANDROID_TABLET;
                device_design_width_node.InnerText = Constants.ANDROID_TABLET_DISPLAY_WIDTH_S;
                device_design_height_node.InnerText = Constants.ANDROID_TABLET_DISPLAY_HEIGHT_S;
                break;
        }

        //rescale all pages
         double from_display_height = Constants.IPHONE_DISPLAY_HEIGHT_D;
         if (previous_device_design != null)
         {
             switch (previous_device_design)
             {
                 case Constants.ANDROID_PHONE:
                     from_display_height = Constants.ANDROID_PHONE_DISPLAY_HEIGHT_D;
                     break;
             }

             double to_display_height = Constants.IPHONE_DISPLAY_HEIGHT_D;
             switch (device_design)
             {
                 case Constants.ANDROID_PHONE:
                     to_display_height = Constants.ANDROID_PHONE_DISPLAY_HEIGHT_D;
                     break;
             }
             double y_factor = to_display_height / from_display_height;

             XmlNodeList top_list = doc.SelectNodes("//top");
             foreach (XmlNode top in top_list)
             {
                 top.InnerText = Math.Round(Convert.ToDouble(top.InnerText) * y_factor).ToString();
             }
             XmlNodeList height_list = doc.SelectNodes("//height");
             foreach (XmlNode height in height_list)
             {
                 height.InnerText = Math.Round(Convert.ToDouble(height.InnerText) * y_factor).ToString();
             }
         }
        Util util = new Util();
        util.UpdateStagingAppXml(State);
    }
    public void SaveAppPage(Hashtable State, string page_name, string html)
    {
        EncodeAppPageToAppXml(State, page_name, html);
    }
    public Hashtable EncodeAppPageToAppXml(Hashtable State, string page_name, string html)
    {
       XmlDocument doc = GetStagingAppXml(State);
       State["AppXmlDoc"] = doc;

       XmlNode configuration_node = doc.SelectSingleNode("//configuration");

        //save background
       if (State["BackgroundImageUrl"] != null)
       {
           string background_image = State["BackgroundImageUrl"].ToString();
           XmlNode background_image_node = configuration_node.SelectSingleNode("background_image");
           if (background_image_node == null)
               background_image_node = CreateNode(doc, configuration_node, "background_image", background_image);
           else
               background_image_node.InnerText = background_image;
       }
        
        //find page if it exists
        XmlNode page_name_node = doc.SelectSingleNode("//pages/page/name[.  ='" + page_name  + "']");
        XmlNode page_node = null;
        XmlNode fields_node = null;
        if (page_name_node == null) //no - create a new page
        {
            XmlNode pages_node = doc.SelectSingleNode("//pages");
            page_node = CreateNode(doc, pages_node, "page");
            CreateNode(doc, page_node, "name", page_name);
            CreateNode(doc, page_node, "order", GetNextPageOrder(doc));
        }
        else
        {
            page_node = page_name_node.ParentNode;
            fields_node = page_node.SelectSingleNode("fields");
            if (fields_node != null)
                page_node.RemoveChild(fields_node);
        }
         fields_node = CreateNode(doc, page_node, "fields");

         State["ComputeSymbols"] = new ArrayList();
         if (html.Length > 0)
         {
             //encode design
             HtmlDocument HtmlDoc = new HtmlDocument();
             if (State["Browser"].ToString() == "Opera")
                 html = FilterOperaHtml(html);

             HtmlDoc.LoadHtml(html);
             HtmlNodeCollection div_list = HtmlDoc.DocumentNode.SelectNodes("//div[starts-with(@title,'MobiFlex')]");
             string ret = ProcessHtmlNodes(State, doc, page_name, fields_node, div_list);
             if (ret != "OK")
             {
                 throw new Exception(ret);
             }
         }
         else //new page with only a backbground
         {
            State["BackgroundHtml"] =  html = "<img id=\"background_image\" src=\"" + State["BackgroundImageUrl"].ToString() + "\" style=\"position:absolute;top:0px;left:0px;\"/>";          
         }
        Util util = new Util();
        util.UpdateStagingAppXml(State);

        Hashtable duplicate_IDs = CheckForDuplicateIDs(page_name, doc);
        if (duplicate_IDs != null)
            return duplicate_IDs;

        ArrayList ComputeSymbols = (ArrayList)State["ComputeSymbols"];
        if (ComputeSymbols.Count > 0)
        {
            CheckComputeSymbols(doc,State);
        }

        util.SavePageImage(State, page_name,html);
        return null;
    }
 
    public void ValidateFieldNames(Hashtable State, Label Message)
    {
        //validate goto page, compute statements, all property sheets with field names, 
       XmlDocument doc = GetStagingAppXml(State);
       Hashtable distinctIDs = new Hashtable();
       XmlNodeList XmlIDs = doc.SelectNodes("//id");
       //get all ids first
       foreach (XmlNode XmlID in XmlIDs)
       {
            distinctIDs[XmlID.InnerText] = true;
       }

       XmlIDs = doc.SelectNodes("//picker/picker_fields/picker_field/name | //table/table_fields/table_field/name");
       foreach (XmlNode XmlID in XmlIDs)
       {
           distinctIDs[XmlID.InnerText] = true;
       }

        StringBuilder errors = new StringBuilder();
        string field_name = null;
        string page_name = null;
        XmlNode field_node = null;
        //get all property sheet field names
        XmlNodeList fields = doc.SelectNodes("//phone_field | //subject_field | //message_field | //email_field | //media_link_field | //sms_phone_field | //to_email_field | //subject_field | //icon_field | //doc_selection_field");
        foreach (XmlNode field in fields)
        {
            if(field.Name == "icon_field")
                field_node = field.ParentNode;
            else
                field_node = field.ParentNode.ParentNode.ParentNode;

            if (field_node.Name == "sql_commands")
                continue;
            if (!distinctIDs.ContainsKey(field.InnerText))
            {
                field_name = field_node.SelectSingleNode("id").InnerText;
                page_name = field_node.ParentNode.ParentNode.SelectSingleNode("name").InnerText;
                errors.Append("The property field name " + field.InnerText + " of field " + field_name + " on page " + page_name + "; ");
            }
        }
        //get all compute statements
        XmlNodeList computes = doc.SelectNodes("//compute");
        foreach (XmlNode compute in computes)
        {
            field_node = compute.ParentNode;
            field_name = field_node.SelectSingleNode("id").InnerText;
            page_name = field_node.ParentNode.ParentNode.SelectSingleNode("name").InnerText;

            string[] statements = compute.InnerText.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string statement in statements)
            {
                string[] parts = statement.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    string[] sections = part.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (sections[0] == "assign" || sections[0] == "first")
                    {
                        try
                        {
                            string word = sections[1];
                            if (word.ToLower() != "now()" && 
                                !word.ToLower().StartsWith("showwebpage(") &&
                                 !word.ToLower().StartsWith("playaudio(") && 
                                word.ToLower() != "true" && 
                                word.ToLower() != "false" && 
                                !distinctIDs.ContainsKey(word))
                            {
                                 errors.Append("The compute field name " + word + " of field " + field_name + " on page " + page_name + "; ");
                            }
                        }
                        catch(Exception ex)
                        {
                             errors.Append("Error in the compute property of field " + field_name + " on page " + page_name + "; ");
                        }
                    }
                }
            }
        }
        if (errors.Length == 0)
            Message.Text = "All fields have been validated";
        else
        {
            errors.Insert(0,"The following fields do not match:\n");
            Message.Text = errors.ToString();
        }
    }
    public string GetNextPageOrder(XmlDocument doc)
    {
        XmlNodeList page_orders = doc.SelectNodes("//page/order");
        if (page_orders.Count == 0)
            return "0";
        int max_page_order = 0;
        foreach (XmlNode order_node in page_orders)
        {
            int order = Convert.ToInt32( order_node.InnerText);
            if (order > max_page_order)
                max_page_order = order;
        }
        return (max_page_order + 1).ToString();
    }
    public string[] GetMobileCommerce(Hashtable State, string application)
    {
        XmlDocument doc = GetStagingAppXml(State);
        XmlNode configuration_node = doc.SelectSingleNode("//configuration");
        XmlNode mobile_commerce_node = configuration_node.SelectSingleNode("mobile_commerce");
        if (mobile_commerce_node == null)
            return null;

        string[] Values = new string[2];

        XmlNode username_node = mobile_commerce_node.SelectSingleNode("username");
        Values[0] = username_node.InnerText;
        XmlNode password_node = mobile_commerce_node.SelectSingleNode("password");
        Values[1] = password_node.InnerText;
        return Values; 
    }
    public bool DoesAppHaveMobileCommerce(Hashtable State, string application)
    {
        XmlDocument doc = GetStagingAppXml(State);
        XmlNode configuration_node = doc.SelectSingleNode("//configuration");
        XmlNode mobile_commerce_node = configuration_node.SelectSingleNode("mobile_commerce");
        return (mobile_commerce_node == null) ? false : true;
    }
    public string SetMobileCommerce(Hashtable State, string username, string password)
    {
        XmlDocument doc = GetStagingAppXml(State);
        XmlNode configuration_node = doc.SelectSingleNode("//configuration");
        XmlNode mobile_commerce_node = configuration_node.SelectSingleNode("mobile_commerce");
        if (mobile_commerce_node == null)
            mobile_commerce_node = CreateNode(doc, configuration_node, "mobile_commerce");

        CreateNode(doc, mobile_commerce_node, "username", username);
        CreateNode(doc, mobile_commerce_node, "password", password);

        Util util = new Util();
        State["AppXmlDoc"] = doc;
        util.UpdateStagingAppXml(State);
        return "OK";
    }
    public void ClearMobileCommerce(Hashtable State)
    {
        XmlDocument doc = GetStagingAppXml(State);
        XmlNode configuration_node = doc.SelectSingleNode("//configuration");
        XmlNode mobile_commerce_node = configuration_node.SelectSingleNode("mobile_commerce");
        if (mobile_commerce_node != null)
        {
            configuration_node.RemoveChild(mobile_commerce_node);
        }

        Util util = new Util();
        State["AppXmlDoc"] = doc;
        util.UpdateStagingAppXml(State);
       
    }
    public void SetBackgroundColor(Hashtable State, string background_color)
    {
        XmlDocument doc = GetStagingAppXml(State);
        XmlNode configuration_node = doc.SelectSingleNode("//configuration");
        XmlNode background_color_node = configuration_node.SelectSingleNode("background_color");
        if (background_color_node == null)
            background_color_node = CreateNode(doc, configuration_node, "background_color");

        background_color_node.InnerText = background_color;

        Util util = new Util();
        State["AppXmlDoc"] = doc;
        util.UpdateStagingAppXml(State);
    }
    public string GetBackgroundColor(Hashtable State )
    {
        XmlDocument doc = GetStagingAppXml(State);
        XmlNode configuration_node = doc.SelectSingleNode("//configuration");
        XmlNode background_color_node = configuration_node.SelectSingleNode("background_color");
        if (background_color_node == null)
            return "#f0f0f0";

        return background_color_node.InnerText;
    }
    public void SetBackgroundImage(Hashtable State)
    {
        XmlDocument doc = GetStagingAppXml(State);
        XmlNode configuration_node = doc.SelectSingleNode("//configuration");

        if (State["BackgroundImageUrl"] != null)
        {
            string background_image = State["BackgroundImageUrl"].ToString();
            
            XmlNode background_node = configuration_node.SelectSingleNode("background");
            if (background_node == null)
                background_node = CreateNode(doc, configuration_node, "background");

            XmlNode background_image_node = background_node.SelectSingleNode("image_source");
            if (background_image_node == null)
            {
                background_image_node = CreateNode(doc, background_node, "image_source", background_image);
            }
            else
                background_image_node.InnerText = background_image;

            Util util = new Util();
            State["AppXmlDoc"] = doc;
            util.UpdateStagingAppXml(State);
        }
    }
    private string FilterOperaHtml(string html)
    {
        StringBuilder filtered_html = new StringBuilder();
        int start = 0;
        int end = 0;
        do
        {
            end = html.IndexOf("font-family: \"",start);
            if(end >= 0)
            {
                end += 13;
                filtered_html.Append(html.Substring(start,end-start));
                start = end + 1;
                end = html.IndexOf("\"",start);
                if(end >=0)
                {
                    filtered_html.Append(html.Substring(start,end-start));
                }
                start = end + 1;
            }
            else
                filtered_html.Append(html.Substring(start));

        } while (end > 0);

        return filtered_html.ToString();
    }
    private class mySortClass : IComparer
    {
        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer.Compare(Object x, Object y)
        {
            Hashtable x_dict = (Hashtable)x;
            int x_z_index = (int)x_dict["z-index"];
            Hashtable y_dict = (Hashtable)y; 
            int y_z_index = (int)y_dict["z-index"];
            if (x_z_index > y_z_index)
                return 1;
            else if (x_z_index < y_z_index)
                return -1;
            else
                return 0;
        }
    }


    private string ProcessHtmlNodes(Hashtable State, XmlDocument doc, string page_name, XmlNode fields_node, HtmlNodeCollection childNodes)
    {
        try
        {
            XmlNode field_node = null;
            HttpServerUtility Server = (HttpServerUtility)State["Server"];
            Util util = new Util();

            ArrayList sortedNodeList = new ArrayList();

            foreach (HtmlNode node in childNodes)
            {
                Hashtable dict = new Hashtable();
                Hashtable attribute_list = new Hashtable();
                foreach (HtmlAttribute attribute in node.Attributes)
                {
                    attribute_list[attribute.Name.ToLower()] = attribute.Value.Replace("&amp;", "&");
                }
                HtmlNode child = node.FirstChild;
                if (child != null)
                {
                    foreach (HtmlAttribute attribute in child.Attributes)
                    {
                        if (attribute.Name.ToLower() == "src")
                            attribute_list[attribute.Name.ToLower()] = attribute.Value;
                    }
                }

                dict["node"] = node;
                dict["attribute_list"] = attribute_list;

                //just get z-index for now
                string style_string = attribute_list["style"].ToString();
                if (style_string.Contains("z-index"))
                {
                    int start = style_string.IndexOf("z-index:");
                    start += 8;
                    int end = style_string.IndexOf(";", start);
                    string s_zindex = style_string.Substring(start, end - start);
                    dict["z-index"] = Convert.ToInt32(s_zindex.Trim());
                }
                else
                    dict["z-index"] = 0;

                sortedNodeList.Add(dict);
            }

            sortedNodeList.Sort(new mySortClass());//sort by z-index to get overlapping fields

            foreach (Hashtable dict in sortedNodeList)
            {
                HtmlNode node = (HtmlNode)dict["node"];
                string text = node.InnerText;
                Hashtable attribute_list = (Hashtable)dict["attribute_list"];

                switch (attribute_list["title"].ToString())
                {
                    case "MobiFlex Button":
                        field_node = CreateNode(doc, fields_node, "button");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        if (attribute_list["src"] != null)
                            CreateNode(doc, field_node, "image_source", attribute_list["src"].ToString());

                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));

                        //there are 2 coding schemes here: 
                        //1 css type coding
                        //2 xml type coding - the new one
                        //maintain backward comptibility
                        if (attribute_list["submit"] != null)
                        {
                            string submit = attribute_list["submit"].ToString().Replace("_this_page", page_name).Replace(";;", ";");
                            XmlNode submit_node = null;
                            if (submit.Contains("compute:"))
                            {
                                XmlNode compute_node = CreateNode(doc, field_node, "compute");
                                if (!EncodeCompute(State, doc, compute_node, submit.Substring(submit.IndexOf("compute:"))))
                                {
                                    return "There is an error in the compute field: " + submit.Substring(submit.IndexOf("compute:"));
                                }
                                string submit_part = submit.Substring(0, submit.IndexOf("compute:"));
                                if (submit_part != ";") //it needs to be more than just a ';'
                                    submit_node = CreateNode(doc, field_node, "submit", submit_part);
                            }
                            else
                                submit_node = CreateNode(doc, field_node, "submit", submit);

                            if (submit_node != null)
                            {
                                EncodeSubmit(State, doc, submit_node);
                                CheckCreatePage(State, doc, submit_node);
                                //ensure backward compatiblity
                                FilterSubmitBackwards(submit_node);
                            }
                        }
                        string button_text = node.InnerText.Replace("\r", "").Replace("\n", "").Trim();
                        CreateNode(doc, field_node, "text", button_text);
                        break;
                    case "MobiFlex Label":
                        field_node = CreateNode(doc, fields_node, "label");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        string label_text2 = node.InnerText.Replace("\r", "").Replace("\n", "");
                        CreateNode(doc, field_node, "text", label_text2);
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;

                    case "MobiFlex TextField":
                        field_node = CreateNode(doc, fields_node, "text_field");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        CreateNode(doc, field_node, "type", attribute_list["type"].ToString());
                        if (attribute_list["text"] != null)
                        {
                            CreateNode(doc, field_node, "text", DecodeStudioHtml(attribute_list["text"].ToString()));
                        }
                        if (attribute_list["alt"] != null)
                        {
                            string[] parts = attribute_list["alt"].ToString().Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length == 1)
                            {
                                CreateNode(doc, field_node, "keyboard", attribute_list["alt"].ToString());
                                CreateNode(doc, field_node, "validation", "none");
                            }
                            else if(parts.Length == 2)
                            {
                                string[] sub_parts = parts[0].Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                if(sub_parts.Length ==2)
                                     CreateNode(doc, field_node, "keyboard", sub_parts[1]);

                                sub_parts = parts[1].Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                if (sub_parts.Length == 2)
                                     CreateNode(doc, field_node, "validation", sub_parts[1]);
                            }
                        }
                        else
                        {
                            CreateNode(doc, field_node, "keyboard", "default");
                            CreateNode(doc, field_node, "validation", "none");
                        }
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));

                        break;
                    case "MobiFlex TextArea":
                        field_node = CreateNode(doc, fields_node, "text_area");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        if(attribute_list["type"] != null)
                             CreateNode(doc, field_node, "edit_type", attribute_list["type"].ToString());
                        else
                            CreateNode(doc, field_node, "edit_type", "non_editable");
 
                        if (attribute_list["text"] != null)
                            CreateNode(doc, field_node, "text", DecodeStudioHtml(attribute_list["text"].ToString()));
                        else
                            CreateNode(doc, field_node, "text", "");

                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));

                        break;

                    case "MobiFlex HtmlPanel":
                        field_node = CreateNode(doc, fields_node, "html_panel");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        CreateNode(doc, field_node, "html", HttpUtility.HtmlEncode(node.InnerHtml)); 
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;

                    case "MobiFlex ImageButton":
                        field_node = CreateNode(doc, fields_node, "image_button");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        CreateNode(doc, field_node, "image_source", attribute_list["src"].ToString());
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));

                        //there are 2 coding schemes here: 
                        //1 css type coding
                        //2 xml type coding - the new one
                        //maintain backward comptibility
                        if (attribute_list["submit"] != null)
                        {
                            string ImageButton_submit = attribute_list["submit"].ToString().Replace("_this_page", page_name).Replace(";;", ";"); 
                            XmlNode submit_node = null;
                            if (ImageButton_submit.Contains("compute:"))
                            {
                                XmlNode compute_node = CreateNode(doc, field_node, "compute");
                                if (!EncodeCompute(State, doc, compute_node, ImageButton_submit.Substring(ImageButton_submit.IndexOf("compute:"))))
                                {
                                    return "There is an error in the compute field: " + ImageButton_submit.Substring(ImageButton_submit.IndexOf("compute:"));
                                }

                                string ImageButton_submit_part = ImageButton_submit.Substring(0, ImageButton_submit.IndexOf("compute:"));
                                if (ImageButton_submit_part != ";") //it needs to be more than just a ';'
                                    submit_node = CreateNode(doc, field_node, "submit", ImageButton_submit_part);
                            }
                            else
                                submit_node = CreateNode(doc, field_node, "submit", ImageButton_submit);

                            if (submit_node != null)
                            {
                                EncodeSubmit(State, doc, submit_node);
                                CheckCreatePage(State, doc, submit_node);
                                //ensure backward compatiblity
                                FilterSubmitBackwards(submit_node);
                            }
                        }
                        break;
                    case "MobiFlex Image":
                        field_node = CreateNode(doc, fields_node, "image");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        CreateNode(doc, field_node, "image_source", attribute_list["src"].ToString());
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;
                    case "MobiFlex Audio":
                        field_node = CreateNode(doc, fields_node, "audio");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        CreateNode(doc, field_node, "audio_source", attribute_list["source"].ToString());
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;
                    case "MobiFlex Table":
                        field_node = CreateNode(doc, fields_node, "table");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        if (attribute_list["name"] == null)                        
                            CreateNode(doc, field_node, "display_name", attribute_list["id"].ToString());
                        
                        else
                           CreateNode(doc, field_node, "display_name", attribute_list["name"].ToString());

                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));

                        //get table type and fields
                        string fields_value = attribute_list["fields"].ToString();
                        string[] parts2 = fields_value.Split(":".ToCharArray());
                        string[] format = parts2[0].Split("|".ToCharArray());
                        if (format.Length != 2)
                        {
                            return attribute_list["id"].ToString() + " table format is wrong. Re-insert the table.";

                        }
                        CreateNode(doc, field_node, "table_type", format[0]);
                        XmlNode table_fields = CreateNode(doc, field_node, "table_fields");
                        string[] field_types = format[1].Split(",".ToCharArray());
                        string[] field_names = parts2[1].Split(",".ToCharArray());
                        int i_field = 0;

                        //prepare options if they exist
                        string[] option_sections = null;
                        if (attribute_list.ContainsKey("options"))
                        {
                            string options = attribute_list["options"].ToString();
                            if (options.Length > 0)
                            {
                                option_sections = options.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                if (option_sections.Length > 0 && option_sections.Length != field_names.Length)
                                    return "The number of table fields (" + field_names.Length + ") does not equal the number of option section(s) (" + option_sections.Length + ")";
                            }
                        }
                        int i_section = 0;
                        foreach (string field_name in field_names)
                        {
                            XmlNode table_field = CreateNode(doc, table_fields, "table_field");
                            CreateNode(doc, table_field, "name", field_name.Trim());
                            CreateNode(doc, table_field, "type", field_types[i_field]);
                            if (attribute_list.ContainsKey("options") && option_sections != null && option_sections.Length > 0)
                                CreateNode(doc, table_field, "options", DecodeStudioHtml(option_sections[i_section++]));
                            i_field++;
                        }
                        //there are 2 coding schemes here: 
                        //1 css type coding
                        //2 xml type coding - the new one
                        //maintain backward comptibility
                        if (attribute_list.ContainsKey("submit"))
                        {
                            string table_submit = attribute_list["submit"].ToString().Replace("_this_page", page_name).Replace(";;", ";");
                            XmlNode submit_node = null;
                            if (table_submit.Contains("compute:"))
                            {
                                XmlNode compute_node = CreateNode(doc, field_node, "compute");
                                if (!EncodeCompute(State, doc, compute_node, table_submit.Substring(table_submit.IndexOf("compute:"))))
                                {
                                    return "There is an error in the compute field: " + table_submit.Substring(table_submit.IndexOf("compute:"));
                                }

                                string table_submit_part = table_submit.Substring(0, table_submit.IndexOf("compute:"));
                                if (table_submit_part != ";") //it needs to be more than just a ';'
                                    submit_node = CreateNode(doc, field_node, "submit", table_submit_part);
                            }
                            else
                                submit_node = CreateNode(doc, field_node, "submit", table_submit);

                            if (submit_node != null)
                            {
                                EncodeSubmit(State, doc, submit_node);
                                CheckCreatePage(State, doc, submit_node);
                                //ensure backward compatiblity
                                FilterSubmitBackwards(submit_node);
                            }
                        }

                        break;
                    case "MobiFlex Slider":
                        field_node = CreateNode(doc, fields_node, "slider");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());                      
                         CreateNode(doc, field_node, "type", "horizontal");
                        string value = attribute_list["value"].ToString();
                        string[] values = value.Split(":".ToCharArray());
                        CreateNode(doc, field_node, "min_value", values[0]);
                        CreateNode(doc, field_node, "max_value", values[1]);
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;
                    case "MobiFlex Picker":
                        field_node = CreateNode(doc, fields_node, "picker");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        //get picker fields
                        string type_value = attribute_list["type"].ToString();
                        string[] picker_parts = type_value.Split(":".ToCharArray());
                        string picker_type = picker_parts[0].Trim();
                        CreateNode(doc, field_node, "picker_type", picker_type);
                        bool pickerHasOptions = true;
                        if (picker_type == "date" || picker_type == "time")
                            pickerHasOptions = false;

                        string picker_options = null;
                        string[] picker_option_sections = null;
                        if (pickerHasOptions)
                        {
                            if (attribute_list.ContainsKey("options"))
                            {
                                picker_options = attribute_list["options"].ToString();
                                picker_option_sections = picker_options.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            }
                            else
                            {
                                return "Options are missing for picker " + attribute_list["id"].ToString();
                            }
                        }

                        if (picker_type != "date" && picker_type != "time")
                        {
                            XmlNode picker_fields = CreateNode(doc, field_node, "picker_fields");
                            string[] picker_field_names = picker_parts[1].Split(",".ToCharArray());

                            int i_picker_section = 0;
                            foreach (string picker_field_name in picker_field_names)
                            {
                                XmlNode picker_field = CreateNode(doc, picker_fields, "picker_field");
                                CreateNode(doc, picker_field, "name", picker_field_name.Trim());
                                if (pickerHasOptions && picker_option_sections != null && picker_option_sections.Length > i_picker_section)
                                    CreateNode(doc, picker_field, "options", DecodeStudioHtml(picker_option_sections[i_picker_section++]));
                            }
                        }

                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;
                    case "MobiFlex WebView":
                        field_node = CreateNode(doc, fields_node, "web_view");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        if (attribute_list.ContainsKey("url") && attribute_list["url"].ToString().Length > 0)
                            CreateNode(doc, field_node, "url", attribute_list["url"].ToString());
                       
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;
                    case "MobiFlex Alert":
                        field_node = CreateNode(doc, fields_node, "alert");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;
                    case "MobiFlex Switch":
                        field_node = CreateNode(doc, fields_node, "switch");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        CreateNode(doc, field_node, "default_value", attribute_list["value"].ToString());
                        CreateNode(doc, field_node, "type", attribute_list["type"].ToString());
                       AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));

                        //there are 2 coding schemes here: 
                        //1 css type coding
                        //2 xml type coding - the new one
                        //maintain backward comptibility
                        if (attribute_list["submit"] != null)
                        {
                            string switch_submit = attribute_list["submit"].ToString().Replace("_this_page", page_name).Replace(";;", ";");
                            XmlNode submit_node = null;
                            if (switch_submit.Contains("compute:"))
                            {
                                XmlNode compute_node = CreateNode(doc, field_node, "compute");
                                if (!EncodeCompute(State, doc, compute_node, switch_submit.Substring(switch_submit.IndexOf("compute:"))))
                                {
                                    return "There is an error in the compute field: " + switch_submit.Substring(switch_submit.IndexOf("compute:"));
                                }
                                string switch_submit_part = switch_submit.Substring(0, switch_submit.IndexOf("compute:"));
                                if (switch_submit_part != ";") //it needs to be more than just a ';'
                                    submit_node = CreateNode(doc, field_node, "submit", switch_submit_part);
                            }
                            else
                                submit_node = CreateNode(doc, field_node, "submit", switch_submit);

                            if (submit_node != null)
                            {
                                EncodeSubmit(State, doc, submit_node);
                                CheckCreatePage(State, doc, submit_node);
                                //ensure backward compatiblity
                                FilterSubmitBackwards(submit_node);
                            }
                        }
                        break;
                    case "MobiFlex CheckBox":
                        field_node = CreateNode(doc, fields_node, "checkbox");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        CreateNode(doc, field_node, "default_value", attribute_list["value"].ToString());
                       
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));

                        //there are 2 coding schemes here: 
                        //1 css type coding
                        //2 xml type coding - the new one
                        //maintain backward comptibility
                        if (attribute_list["submit"] != null)
                        {
                            string checkbox_submit = attribute_list["submit"].ToString().Replace("_this_page", page_name).Replace(";;", ";");
                            XmlNode submit_node = null;
                            if (checkbox_submit.Contains("compute:"))
                            {
                                XmlNode compute_node = CreateNode(doc, field_node, "compute");
                                if (!EncodeCompute(State, doc, compute_node, checkbox_submit.Substring(checkbox_submit.IndexOf("compute:"))))
                                {
                                    return "There is an error in the compute field: " + checkbox_submit.Substring(checkbox_submit.IndexOf("compute:"));
                                }
                                string switch_submit_part = checkbox_submit.Substring(0, checkbox_submit.IndexOf("compute:"));
                                if (switch_submit_part != ";") //it needs to be more than just a ';'
                                    submit_node = CreateNode(doc, field_node, "submit", switch_submit_part);
                            }
                            else
                                submit_node = CreateNode(doc, field_node, "submit", checkbox_submit);

                            if (submit_node != null)
                            {
                                EncodeSubmit(State, doc, submit_node);
                                CheckCreatePage(State, doc, submit_node);
                                //ensure backward compatiblity
                                FilterSubmitBackwards(submit_node);
                            }
                        }
                        break;
                    case "MobiFlex HiddenField":
                        field_node = CreateNode(doc, fields_node, "hidden_field");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        if(attribute_list["value"].ToString().Length > 0)
                            CreateNode(doc, field_node, "value", DecodeStudioHtml(attribute_list["value"].ToString()));
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;
                    case "MobiFlex AudioRecorder":
                        field_node = CreateNode(doc, fields_node, "audio_recorder");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;
                    case "MobiFlex Photo":
                        field_node = CreateNode(doc, fields_node, "photo");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        CreateNode(doc, field_node, "compression", attribute_list["compression"].ToString());
                        if (attribute_list["icon_field"] != null && attribute_list["icon_field"].ToString().Length > 0)
                        {
                            string[] parse_list = attribute_list["icon_field"].ToString().Split(";".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
                            foreach (string parse in parse_list)
                            {
                                string[] icon_parts = parse.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                switch (icon_parts[0])
                                {
                                    case "field":
                                        CreateNode(doc, field_node, "icon_field", icon_parts[1]);
                                        break;
                                    case "width":
                                        CreateNode(doc, field_node, "icon_width", icon_parts[1]);
                                        break;
                                    case "height":
                                        CreateNode(doc, field_node, "icon_height", icon_parts[1]);
                                        break;
                                }
                            }
                        }
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;
                    case "MobiFlex GPS":
                        field_node = CreateNode(doc, fields_node, "gps");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        string names = attribute_list["alt"].ToString();
                        string[] gps_parts = names.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        CreateNode(doc, field_node, "latitude", gps_parts[0]);
                        CreateNode(doc, field_node, "longitude", gps_parts[1]);
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;
                    case "MobiFlex Map":
                        field_node = CreateNode(doc, fields_node, "map");
                        CreateNode(doc, field_node, "id", attribute_list["id"].ToString());
                        CreateNode(doc, field_node, "url", attribute_list["url"].ToString());
                        AddStyleNodes(doc, field_node, util.AddToStyleAttribute(new Hashtable(), attribute_list["style"].ToString()));
                        break;
                }
            }
            return "OK";
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);

            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }

    private void EncodeSubmit(Hashtable State, XmlDocument doc, XmlNode submit_node)
    {
        string submit_string = submit_node.InnerText;
        string[] parts = submit_string.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (string part in parts)
        {
            string[] sub_parts = part.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string[] pairs = null;
            switch (sub_parts[0])
            {
                case "post":
                    CreateNode(doc, submit_node, "post");
                    break;
                case "next_page":
                    XmlNode next_page_node = CreateNode(doc, submit_node, "next_page");
                     pairs = sub_parts[1].Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                     if (pairs.Length >= 2)
                         CreateNode(doc, next_page_node, pairs[0], pairs[1]);
                  break;
                case "if_then_next_page":
                  XmlNode if_then_next_page_node = CreateNode(doc, submit_node, "if_then_next_page");
                  pairs = sub_parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                  foreach (string pair in pairs)
                  {
                      string[] subparts = pair.Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                      CreateNode(doc, if_then_next_page_node, subparts[0], subparts[1]);
                  }
                  break;
                case "previous_page":
                  CreateNode(doc, submit_node, "previous_page");
                  break;
                case "select":
                    XmlNode select_node = CreateNode(doc, submit_node, "select");
                    if (sub_parts.Length == 2)
                    {
                        pairs = sub_parts[1].Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (pairs != null && pairs.Length == 2)
                            CreateNode(doc, select_node, pairs[0], pairs[1]);
                    }
                     break;
                case "call":
                    XmlNode call_node = CreateNode(doc, submit_node, "call");
                      pairs = sub_parts[1].Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                      if (pairs.Length >= 2)
                          CreateNode(doc, call_node, pairs[0], pairs[1]);
                    break;
                case "share":
                    XmlNode share_node = CreateNode(doc, submit_node, "share");
                    if (sub_parts.Length > 1 && sub_parts[1].Length > 0)
                    {
                        string[] pieces = sub_parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        foreach (string piece in pieces)
                        {
                            pairs = piece.Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (pairs.Length == 2)
                                CreateNode(doc, share_node, pairs[0], pairs[1]);
                        }
                    }
                    break;
                case "email":
                    XmlNode email_node = CreateNode(doc, submit_node, "email");
                    if (sub_parts.Length > 1 && sub_parts[1].Length > 0)
                    {
                        string[] email_pieces = sub_parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        foreach (string piece in email_pieces)
                        {
                            pairs = piece.Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (pairs.Length == 2)
                                CreateNode(doc, email_node, pairs[0], pairs[1]);
                        }
                    }
                    break;
                case "sms":
                    XmlNode sms_node = CreateNode(doc, submit_node, "sms");
                    if (sub_parts.Length > 1 && sub_parts[1].Length > 0)
                    {
                        string[] sms_pieces = sub_parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        foreach (string piece in sms_pieces)
                        {
                            pairs = piece.Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (pairs.Length == 2)
                                CreateNode(doc, sms_node, pairs[0], pairs[1]);
                        }
                    }
                    break;
                case "capture_doc":
                    XmlNode capture_doc = CreateNode(doc, submit_node, "capture_doc");
                     pairs = sub_parts[1].Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                     if (pairs.Length >= 2)
                         CreateNode(doc, capture_doc, pairs[0], pairs[1]);
                     break;
                case "capture_process_document":
                case "manage_document_case":
                case "login_to_mcommerce":
                     XmlNode login_to_mcommerce = CreateNode(doc, submit_node, sub_parts[0]);
                     if (sub_parts.Length > 1 && sub_parts[1].Length > 0)
                     {
                         string[] pieces = sub_parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                         foreach (string piece in pieces)
                         {
                             pairs = piece.Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                             if (pairs.Length == 2)
                                 CreateNode(doc, login_to_mcommerce, pairs[0], pairs[1]);
                         }
                     }
                     break;
                case "init_card_swiper":
                     XmlNode mobile_commerce = CreateNode(doc, submit_node, "init_card_swiper");
                    if (sub_parts.Length > 1 && sub_parts[1].Length > 0)
                    {
                        string[] pieces = sub_parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        foreach (string piece in pieces)
                        {
                            pairs = piece.Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (pairs.Length == 2)
                                CreateNode(doc, mobile_commerce, pairs[0], pairs[1]);
                        }
                    }                     
                    break;
                case "void_charge":
                    XmlNode void_charge = CreateNode(doc, submit_node, "void_charge");
                    if (sub_parts.Length > 1 && sub_parts[1].Length > 0)
                    {
                        string[] pieces = sub_parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        foreach (string piece in pieces)
                        {
                            pairs = piece.Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (pairs.Length == 2)
                                CreateNode(doc, void_charge, pairs[0], pairs[1]);
                        }
                    }                     
                  break;
                case "manual_card_charge":
                    CreateNode(doc, submit_node, "manual_card_charge");
                    break;
                case "page_from_field":
                     XmlNode page_from_field = CreateNode(doc, submit_node, "page_from_field");
                    pairs = sub_parts[1].Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                     if (pairs.Length >= 2)
                         CreateNode(doc, page_from_field, pairs[0], pairs[1]);
                     break;
                case "take_photo":
                     XmlNode take_photo = CreateNode(doc, submit_node, "take_photo");
                     if (sub_parts.Length > 1 && sub_parts[1].Length > 0)
                     {
                         string[] take_photo_pieces = sub_parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                         foreach (string piece in take_photo_pieces)
                         {
                             pairs = piece.Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                             if (pairs.Length == 2)
                                 CreateNode(doc, take_photo, pairs[0], pairs[1]);
                         }
                     }
                     break;
                case "capture_signature":
                     XmlNode capture_signature_node = CreateNode(doc, submit_node, "capture_signature");
                     pairs = sub_parts[1].Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                     if (pairs.Length >= 2)
                         CreateNode(doc, capture_signature_node, pairs[0], pairs[1]);
                     break;
 
               default:
                    break;
  
            }
         }
    }
    private bool EncodeCompute(Hashtable State, XmlDocument doc, XmlNode compute_node, string compute_string)
    {
        ArrayList ComputeSymbols = (ArrayList)State["ComputeSymbols"];
        if (ComputeSymbols == null)
            ComputeSymbols = new ArrayList();

        StringBuilder warnings = new StringBuilder();
        try
        {
            //separate compute label from value
            string[] parts = compute_string.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            StringBuilder operations = new StringBuilder();

            //separate different computes
            string[] computes = parts[1].Split("|;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string compute in computes)
            {
                // separate assignment
                string[] split = compute.Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string assign_symbol = split[0].Trim();
                operations.Append("assign:" + assign_symbol + ";");
                ComputeSymbols.Add(assign_symbol);
                bool in_symbol = false;
                StringBuilder sb_symbol = new StringBuilder();
                string symbol = null;
                bool is_first_symbol = true;

                if (split.Length > 1)
                {
                    foreach (Char c in split[1].Trim())
                    {
                        if (in_symbol)
                        {
                            if (!Char.IsLetterOrDigit(c) && c != '_' && c != '.' && c != '(' && c != ')')
                            {
                                in_symbol = false;
                                symbol = sb_symbol.ToString();
                                sb_symbol = new StringBuilder();
                                if (is_first_symbol)
                                {
                                    is_first_symbol = false;
                                    operations.Append("first:" + symbol + ";");
                                    ComputeSymbols.Add(symbol);
                                }
                                else
                                {
                                    operations.Append(symbol + ";");
                                    ComputeSymbols.Add(symbol);
                                }
                                switch (c)
                                {
                                    case ' ':
                                        break;
                                    case '+':
                                    case '-':
                                    case '*':
                                    case '/':
                                        operations.Append("op:" + c + ",");
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                sb_symbol.Append(c);
                            }
                        }
                        else
                        {
                            if (Char.IsLetterOrDigit(c) || c == '_' || c == '.')
                            {
                                in_symbol = true;
                                sb_symbol.Append(c);
                            }
                            else
                            {
                                switch (c)
                                {
                                    case ' ':
                                        break;
                                    case '+':
                                    case '-':
                                    case '*':
                                    case '/':
                                        operations.Append("op:" + c + ",");
                                        break;
                                    default:
                                        break;
                                }

                            }
                        }
                    }
                    if (is_first_symbol)
                    {
                        operations.Append("first:" + sb_symbol.ToString() + ";|");
                        ComputeSymbols.Add(sb_symbol.ToString());
                    }
                    else
                    {
                        operations.Append(sb_symbol.ToString() + ";|");
                        ComputeSymbols.Add(sb_symbol.ToString());
                    }
                }
                else
                    operations.Append("|");
            }
            compute_node.InnerText = operations.ToString();
        }
        catch
        {
            State["ComputeSymbols"] = ComputeSymbols;
            return false;
        }
 
        State["ComputeSymbols"] = ComputeSymbols;
        return true;
    }
    private void FilterSubmitBackwards(XmlNode submit_node)
    {
       // if (submit_node.FirstChild.InnerText.StartsWith("post"))
        //    submit_node.FirstChild.InnerText = submit_node.FirstChild.InnerText.Replace("response_page~", "response_page:");
         if (submit_node.FirstChild.InnerText.StartsWith("next_page"))
            submit_node.FirstChild.InnerText = submit_node.FirstChild.InnerText.Replace("page~", "");
        else if (submit_node.FirstChild.InnerText.StartsWith("call"))
            submit_node.FirstChild.InnerText = submit_node.FirstChild.InnerText.Replace("phone_field~", "");
    }
    private string DecodeComputeNode(string compute)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            string[] statements = compute.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string statement in statements)
            {
                string[] steps = statement.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string step in steps)
                {
                    string[] parts = step.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    switch (parts[0].ToLower())
                    {
                        case "assign":
                            if (parts.Length > 1)
                            {
                                sb.Append(parts[1]);
                                if (!parts[1].ToLower().StartsWith("playaudio(") && 
                                    !parts[1].ToLower().StartsWith("playvideo(") &&
                                    !parts[1].ToLower().StartsWith("gotopage(") &&
                                    !parts[1].ToLower().StartsWith("gotopageif(") &&
                                    !parts[1].ToLower().StartsWith("if(") &&
                                    !parts[1].ToLower().StartsWith("showwebpage("))
                                    sb.Append("~");
                            }
                            break;
                        case "first":
                            sb.Append(parts[1] + " ");
                            break;
                        case "op":
                            string[] op_split = parts[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            sb.Append(op_split[0] + " " + op_split[1] + " ");
                            break;
                    }
                }
                sb.Append("|");
            }

            return sb.Replace("||", "|").ToString();
        }
        catch (Exception ex)
        {
            return null; //we give up and let the caller know there may be a user syntax error
           //Util util = new Util();
           //throw new Exception(ex.Message + ": " + ex.StackTrace);
        }

    }
    private void CheckComputeSymbols(XmlDocument doc, Hashtable State)
    {
        State["EncodeComputeWarnings"] = null;

        ArrayList ComputeSymbols = (ArrayList)State["ComputeSymbols"];
        ArrayList UndefinedComputeSymbols = new ArrayList();
 
        foreach (string sym in ComputeSymbols)
        {
            if (sym == ";")
                continue;
            string symbol = sym;
            string keyword = symbol.ToLower();
            if (keyword == "now()" ||
                keyword.StartsWith("playaudio(") ||
                keyword.StartsWith("playvideo(") ||
                keyword.StartsWith("gotopage(") ||
                 keyword.StartsWith("gotopageif(") ||
                   keyword.StartsWith("if(") ||
                keyword.StartsWith("showwebpage(") ||
                keyword == "true" || keyword == "false")
            {
                int start = symbol.IndexOf("(");
                start++;
                int end = symbol.IndexOf(")", start);
                if (end > start)
                {
                    if (keyword.StartsWith("gotopageif(") || keyword.StartsWith("if("))
                    {
                        string parts_string = symbol.Substring(start, end - start);
                        string[] part_list = parts_string.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        foreach(string part in part_list)
                        {
                            //This code is unfinished. You need to keep parsing inside the () and checking the symbols
                            //CheckSymbol(doc, UndefinedComputeSymbols, part);
                        }
                    }
                    else
                    {
                        symbol = symbol.Substring(start, end - start);
                        CheckSymbol( doc,  UndefinedComputeSymbols,  symbol);
                    }
                }
                else
                    continue;
            }
  
        }

        if(UndefinedComputeSymbols.Count > 0)
        {
            if (UndefinedComputeSymbols.Count == 1)
            {
               State["EncodeComputeWarnings"] = UndefinedComputeSymbols[0] + " is not yet defined.";
            }
            else
            {
                StringBuilder warning = new StringBuilder("The following symbols are not yet defined: ");
                bool is_first = true;
                foreach (string symbol in UndefinedComputeSymbols)
                {
                    if (is_first)
                        is_first = false;
                    else
                        warning.Append(", ");

                    if (symbol != "NOW()")
                        warning.Append(symbol);
                }
                State["EncodeComputeWarnings"] = warning.ToString();
            }             
        }
    }
    private void CheckSymbol(XmlDocument doc, ArrayList UndefinedComputeSymbols, string symbol)
    {
        XmlNode ID = doc.SelectSingleNode("//id[.  ='" + symbol + "']");
        if (ID != null)
            return;

        ID = doc.SelectSingleNode("//name[.  ='" + symbol + "']");
        if (ID == null)
        {
            double Num;
            bool isNum = double.TryParse(symbol, out Num);
            if (!isNum)
                UndefinedComputeSymbols.Add(symbol);
        }
    }
    private void AddStyleNodes(XmlDocument doc, XmlNode field_node, string style_string)
    {
        Hashtable label_style_list = ParseHtmlStyle(style_string);
        AddStyleNodes(doc,field_node,label_style_list);
    }
    private void AddStyleNodes(XmlDocument doc, XmlNode field_node, Hashtable label_style_list)
    {
        foreach (string raw_key in label_style_list.Keys)
        {
            string key = raw_key.ToLower();
            switch (key)
            {
                case "top":
                case "left":
                case "width":
                case "height":
                case "font-size":
                case "font-weight":
                case "line-height":
                   string units =  label_style_list[raw_key].ToString().Replace("px", "");
                    if (units.Contains("."))
                        units = units.Substring(0, units.IndexOf("."));
                    CreateNode(doc, field_node, key.Replace("-", "_"), units);
                    break;
                case "background-image":
                case "background-repeat":
                case "-moz-background-size":
                case "-o-background-size":
                case "-webkit-background-size":
                case "background-size":
                case "border":
                case "position":
                case "cursor":
                case "border-left-width":
                case "border-top-width":
                case "border-right-width":
                case "border-bottom-width":
                case "border-left-style":
                case "border-top-style":
                case "border-right-style":
                case "border-bottom-style":
                case "border-left-color":
                case "border-top-color":
                case "border-right-color":
                case "border-bottom-color":
                case "border-bottom":
                case "border-top":
                case "border-left":
                case "border-right":
                    break;
                case "background-color":
                case "color":
                    string raw_color_string = label_style_list[raw_key].ToString().ToLower();
                    if (raw_color_string.StartsWith("rgb"))
                    {
                        string[] rgb = raw_color_string.Substring(4).Split("),".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        int r = Convert.ToInt32(rgb[0]);
                        int g =  Convert.ToInt32(rgb[1]);
                        int b =  Convert.ToInt32(rgb[2]);
                        System.Drawing.Color color = System.Drawing.Color.FromArgb(r, g, b);
                        string s_color = string.Concat("#", (color.ToArgb() & 0x00FFFFFF).ToString("X6"));
                        CreateNode(doc, field_node, key.Replace("-", "_"), s_color);
                    }
                    else
                        CreateNode(doc, field_node, key.Replace("-", "_"), raw_color_string);
                    break;
                default:
                    CreateNode(doc, field_node, key.Replace("-", "_"), label_style_list[raw_key].ToString());
                    break;
            }
        }
    }
    private Hashtable CheckForDuplicateIDs(string current_page, XmlDocument doc)
    {

        Hashtable distinctIDs = new Hashtable();
        Hashtable duplicates = new Hashtable();
        XmlNodeList XmlIDs = doc.SelectNodes("//id");
        foreach (XmlNode XmlID in XmlIDs)
        {
            XmlNode id_node = XmlID.ParentNode.ParentNode.ParentNode.SelectSingleNode("name");
            if (id_node == null)
                continue;
            string page_name = id_node.InnerText;
            if (distinctIDs.ContainsKey(XmlID.InnerText))
            {
                if (page_name != current_page)
                    duplicates[XmlID.InnerText] = page_name;
                else
                    duplicates[XmlID.InnerText] = distinctIDs[XmlID.InnerText].ToString();
            }
            else
                distinctIDs[XmlID.InnerText] = page_name;
        }

        XmlIDs = doc.SelectNodes("//picker/picker_fields/picker_field/name | //table/table_fields/table_field/name");
        foreach (XmlNode XmlID in XmlIDs)
        {
            XmlNode name_node = XmlID.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.SelectSingleNode("name");
            if (name_node == null)
                continue;
            string page_name = name_node.InnerText;
            if (distinctIDs.ContainsKey(XmlID.InnerText))
            {
                if (page_name != current_page)
                    duplicates[XmlID.InnerText] = page_name;
                else
                    duplicates[XmlID.InnerText] = distinctIDs[XmlID.InnerText].ToString();
            }
            else
                distinctIDs[XmlID.InnerText] = page_name;
        }

        if (duplicates.Count > 0)
            return duplicates;
        else
            return null;
    }
    private void CheckCreatePage(Hashtable State, XmlDocument doc, XmlNode submit_node)
    {
        XmlNode node = submit_node.SelectSingleNode("next_page");
        if (node == null)
            return;
        string page_name = null;
 
        node = node.SelectSingleNode("page");
        if (node == null)
            return;
        
        page_name = node.InnerText;
         
        //find page if it exists
        XmlNode page_name_node = doc.SelectSingleNode("//pages/page/name[.  ='" + page_name  + "']");
        if (page_name_node == null && page_name != "_this_page") //no - create a new page . "_this_page" is reserved
        {
           XmlNode pages_node = doc.SelectSingleNode("//pages");
            XmlNode page_node = CreateNode(doc, pages_node, "page");
            CreateNode(doc, page_node, "name", page_name);
            CreateNode(doc, page_node, "order", GetNextPageOrder(doc));

            if (State["CreatePageMessage"] == null)
            {
                StringBuilder sb_new = new StringBuilder();
                State["CreatePageMessage"] = sb_new;
            }
            StringBuilder sb = (StringBuilder)State["CreatePageMessage"];
            sb.Append(page_name + " page was created. ");
        }
    }
    public Hashtable GetAppOpenAction(Hashtable State)
    {
        XmlDocument doc = GetStagingAppXml(State);
        XmlNode configuration_node = doc.SelectSingleNode("//configuration");

        XmlNode on_app_open_node = configuration_node.SelectSingleNode("on_app_open");
        if (on_app_open_node == null)
            return null;

        Hashtable AppOpenAction = new Hashtable();
        XmlNode submit_node = on_app_open_node.SelectSingleNode("submit");
        if (submit_node != null)
        {

            XmlNode action_node = submit_node.FirstChild;
            if (action_node == null)
                return null;

            switch (action_node.Name)
            {
                case "post":
                    AppOpenAction[action_node.Name] = true;
                    break;
                case "capture_process_document":
                case "manage_document_case":
                case "capture_doc":
                    XmlNode child_node = action_node.FirstChild;
                    if (child_node != null)
                        AppOpenAction[action_node.Name] = child_node.InnerText;
                    else
                        AppOpenAction[action_node.Name] = true;
                    break;

                default:
                    return null;
            }
        }

        XmlNode compute_node = on_app_open_node.SelectSingleNode("compute");
        if (compute_node != null)
            AppOpenAction["compute"] = DecodeComputeNode(compute_node.InnerText).Replace("~","=").Replace("|",";");

        return AppOpenAction;
    }
    public void SetAppOpenAction(Hashtable State, Hashtable AppOpenAction)
    {
        XmlDocument doc = GetStagingAppXml(State);
        XmlNode configuration_node = doc.SelectSingleNode("//configuration");
        XmlNode on_app_open_node = null;
        if (AppOpenAction == null)
        { 
            on_app_open_node = configuration_node.SelectSingleNode("on_app_open");
            if (on_app_open_node == null)
                return;
            else
                configuration_node.RemoveChild(on_app_open_node);
            return;
        }
        on_app_open_node = configuration_node.SelectSingleNode("on_app_open");
        if (on_app_open_node == null)
            on_app_open_node = CreateNode(doc, configuration_node, "on_app_open");
        else
        {
            configuration_node.RemoveChild(on_app_open_node);
            on_app_open_node = CreateNode(doc, configuration_node, "on_app_open");
        }

        foreach (string key in AppOpenAction.Keys)
        {
            XmlNode submit_node = null;
            switch (key)
            {
                case "post":
                    submit_node = on_app_open_node.SelectSingleNode("submit");
                    if (submit_node == null)                    
                        submit_node = CreateNode(doc, on_app_open_node, "submit");
                    
                    CreateNode(doc, submit_node, key);
                    break;
                case "capture_process_document":
                case "manage_document_case":
                case "capture_doc":
                    submit_node = on_app_open_node.SelectSingleNode("submit");
                    if (submit_node == null)                    
                        submit_node = CreateNode(doc, on_app_open_node, "submit");
                    
                    CreateNode(doc, submit_node, key, AppOpenAction[key].ToString());
                    break;
                case "compute":
                    XmlNode compute_node = CreateNode(doc, on_app_open_node, "compute");
                    EncodeCompute(State, doc, compute_node, "compute:" + AppOpenAction[key].ToString().Replace("=","~").Replace(";","|"));
                    break;
            }
        }

        Util util = new Util();
        State["AppXmlDoc"] = doc;
        util.UpdateStagingAppXml(State);
    }


    private string ReplaceStyleItemsWithList(string style_string, Hashtable new_style_list)
    {
        string[] style_values = style_string.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        Hashtable style_list = new Hashtable();
        foreach (string style_value in style_values)
        {
            string[] parts = style_value.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            style_list[parts[0].Trim()] = parts[1].Trim();
        }
        foreach (DictionaryEntry item in new_style_list)
        {
            style_list[item.Key] = item.Value;
        }
        StringBuilder style = new StringBuilder();
        foreach (DictionaryEntry x in style_list)
        {
            style.Append(x.Key + ":" + x.Value + ";");
        }

        return style.ToString() ;
    }
    private int GetStyleFontSize(Hashtable attribute_list)
    {
        int size = 11;
        string style_string = attribute_list["style"].ToString();
        string[] style_values = style_string.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (string style_value in style_values)
        {
            string[] parts = style_value.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string type = parts[0].Trim();
            if (type == "font-size")
            {
                string s_size = parts[1].Trim().Replace("px", "");
                size = Convert.ToInt32(s_size);
            }
            break;
        }
        return size;
    }
    private string GetStyleFontFamily(Hashtable attribute_list)
    {
        string font_family = "helvetica";
        string style_string = attribute_list["style"].ToString();
        string[] style_values = style_string.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (string style_value in style_values)
        {
            string[] parts = style_value.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string type = parts[0].Trim();
            if (type == "font-family")
            {
                font_family = parts[1].Trim();
            }
            break;
        }
        return font_family;
    }
    private string GetStyleImageUrl(string style_string)
    {
        string[] style_values = style_string.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (string style_value in style_values)
        {
            string[] parts = style_value.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string type = parts[0].Trim();
            if (type == "background")
            {
                int start = parts[1].IndexOf("url(");
                start += 4;
                int end = parts[1].IndexOf(")", start);
                return parts[1].Substring(start, end - start);
            }
        }
        return "";
    }
    private Size GetStyleWidthAndHeight(string style_string, Size size)
    {
        string[] style_values = style_string.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (string style_value in style_values)
        {
            string[] parts = style_value.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string type = parts[0].Trim();
            if (type == "width")
            {
                string w_val = parts[1].Trim().Replace("px", "");
                size.Width = Convert.ToInt32(w_val);
            }
            else if (type == "height")
            {
                string h_val = parts[1].Trim().Replace("px", "");
                size.Height = Convert.ToInt32(h_val);
            }
        }
        return size;
    }

    public XmlDocument LoadXmlFile(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        return doc;
    }
    private int GetRowOfTextPosition(string input,int start_row, int start_position, int text_position)
    {
        int row = start_row;
        int end = 0;
        int start = start_position;
        while (end >= 0)
        {
            end =input.IndexOf("\n",start);
            if(end >=0)
            {
                if (end > text_position)
                    return row;

                row++;
                start = end + 1;
                if (start > input.Length - 1)
                {
                    return row;
                }
            }
        }
        return row;
    }
    private int BackwardsGetRowOfTextPosition(string input, int start_row, int start_position, int text_position)
    {
        int row = start_row;
        int end = 0;
        int start = start_position;
        while (end >= 0)
        {
            end = input.LastIndexOf("\n", start);
            if (end >= 0)
            {
                if (end < text_position)
                    return row;

                row--;
                start = end - 1;
                if (start < 0)
                {
                    return row;
                }
            }
        }
        return row;
    }
     public string RemoveHtmlTag(string input, string tag)
    {
        string search = "<" + tag;
        int start = 0;
        string output = input;
        while (start >= 0)
        {
            start = output.IndexOf(search, start);
            if (start >= 0)
            {
                int start2 = start + search.Length;
                int end = output.IndexOf(">", start2);
                if (end >= 0)
                {
                    output = output.Remove(start, end +1 - start);
                    start = 0;
                }
                else
                {
                    break;
                }
            }
        }
        search = "</" + tag + ">";
        return output.Replace(search,"");
    }
    public TableCell CreateTableCell(string text, string ID, string style_class)
    {
        TableCell cell = new TableCell();
        cell.Text = text;
        cell.ID = ID;
        if(text.Length>0)
            cell.CssClass = style_class;
        return cell;
    }
    public TableCell CreateTableCellTextBox(string text, string ID, int width, string style_class)
    {
        TableCell cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        TextBox box = new TextBox();
        box.Text = text;
        box.ID = ID;
        box.CssClass = style_class;
        box.Width = width;
        cell.Controls.Add(box);
        return cell;
    }
    public bool AreTextBoxCellsDifferent(TableCell cell1, TableCell cell2)
    {
        TextBox box1 = (TextBox)cell1.Controls[0];
        TextBox box2 = (TextBox)cell2.Controls[0];
        return box1.Text != box2.Text;
    }
    public TableCell CreateTableCellLabel(string text, string ID, string style_class)
    {
        TableCell cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        Label label = new Label();
        label.Text = text;
        label.ID = ID;
        label.CssClass = style_class;
        cell.Controls.Add(label);
        return cell;
    }
    public TableCell CreateTableCellRadioButton(string text,string ID, string group_name)
    {
        TableCell cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        RadioButton button = new RadioButton();
        //button.Text = text;
        button.ID = ID;
        //button.CssClass = "InvisibleRadioButtonText";
        button.GroupName = group_name;
        cell.Controls.Add(button);
        return cell;
    }
    public TableCell CreateTableCellDropDownList(string text, string ID, DropDownList list, string style_class)
    {
        TableCell cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Center;
        list.SelectedValue = text;
        list.ID = ID;
        list.CssClass = style_class;
        cell.Controls.Add(list);
        return cell;
    }
    public bool AreDropDownListSelectionsDifferent(TableCell cell1, TableCell cell2)
    {
        DropDownList box1 = (DropDownList)cell1.Controls[0];
        DropDownList box2 = (DropDownList)cell2.Controls[0];
        return box1.SelectedValue != box2.SelectedValue;
    }
 
    private string GetXmlNodeInnerText(XmlNode parentNode, string xPath)
    {

        XmlNode valueNode = parentNode.SelectSingleNode(xPath);

        if (valueNode != null)
        {
            return valueNode.InnerText;
        }
        else
        {
            return string.Empty;
        }
    }
    public bool DidRadioListChange(RadioButtonList Current, RadioButtonList Before)
    {
        if (Before.Items.Count != Current.Items.Count)
            return true;
        int i = 0;
        foreach (ListItem item in Current.Items)
        {
            if (item.Value != Before.Items[i++].Value)
                return true;
        }
        return false;
    }
 
    public XmlNode CreateTopNode(XmlDocument doc, string node_label)
    {
        XmlNode node = doc.CreateElement(node_label);
        doc.AppendChild(node);
        return node;
    }
    public XmlNode CreateNode(XmlDocument doc, XmlNode parent, string node_label, string node_value)
    {
        XmlNode node = doc.CreateElement(node_label);
        node.InnerText = node_value;
        parent.AppendChild(node);
        return node;
    }
    public XmlNode CreateNode(XmlDocument doc, XmlNode parent, string node_label)
    {
        XmlNode node = doc.CreateElement(node_label);
        parent.AppendChild(node);
        return node;
    }
    public XmlAttribute AddAttribute(XmlDocument doc, XmlNode node, string name, string value)
    {
        XmlAttribute attribute = doc.CreateAttribute(name);
        attribute.Value = value;
        node.Attributes.Append(attribute);
        return attribute;
    }
    public Hashtable ParseHtmlStyle(string style)
    {
        Hashtable styles = new Hashtable();
        string[] parts = style.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (string part in parts)
        {
            string[] subs = part.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (subs.Length == 1)
                continue;
            else if(subs.Length ==3)
                subs[1] += ":" + subs[2];
            styles[subs[0].Trim()] = subs[1].Trim();
        }

        //make sure this has a top left
        if (styles["left"] == null)
            styles["left"] = "0";
        if (styles["top"] == null)
            styles["top"] = "0";

        return styles;
    }
    public string RenameAppXmlWithID(Hashtable State, string current_xml, string new_app_name,string new_app_id )
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(current_xml.Substring(current_xml.IndexOf("<mobiflex")));
        XmlNode app_node = doc.SelectSingleNode("//application");
        XmlNode app_name_node = app_node.SelectSingleNode("name");
        if (app_name_node == null)
            app_name_node = CreateNode(doc, app_node, "name", new_app_name);
        else
            app_name_node.InnerText = new_app_name;

        XmlNode app_id_node = app_node.SelectSingleNode("id");
        if (app_id_node == null)
            app_id_node = CreateNode(doc, app_node, "id", new_app_id);
        else
            app_id_node.InnerText = new_app_id;
        return doc.OuterXml;
    }

    public string EscapeXml(string s)
    {
         if (!string.IsNullOrEmpty(s))
        {
            // replace literal values with entities
            return  s.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
         }
        return "";
    }

    public string UnescapeXml(string s)
    {
        if (!string.IsNullOrEmpty(s))
        {
            // replace literal values with entities
            return s.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'").Replace("&amp;", "&");
        }
        return "";
    }
    public string EncodeStudioHtml(string s)
    {
        if (!string.IsNullOrEmpty(s))
        {
            // replace literal values with entities
            return s.Replace("<", "&lt;").Replace(">", "&gt;").Replace( "\"","``").Replace( "=","~~");
        }
        return "";
    }
    public string DecodeStudioHtml(string s)
    {
        if (!string.IsNullOrEmpty(s))
        {
            // replace literal values with entities
            return s.Replace("&lt;", "<").Replace("&gt;", ">").Replace("``", "\"").Replace("~~", "=");
        }
        return "";
    }
    private class PageValues
    {
        public PageValues(string font_type, int font_size, string font_weight)
        {
            this.FontSize = font_size;
            this.FontWeight = font_weight;
            this.FontType = font_type;
            Message = "OK";
        }

        public string FontType;
        public int FontSize;
        public string FontWeight;
        public string Message;
    }
}


