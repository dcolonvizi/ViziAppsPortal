using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Xml;
using System.Drawing;
using Telerik.Web.UI;
using System.Net;
using System.IO;

public partial class ManageData : System.Web.UI.Page
{
    protected int cell_width = 75;
    protected int page_size = 20;

    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State,Response,"Default.aspx")) return;
         try
        {
            if (!IsPostBack)
               UserLabel.Text = State["Username"].ToString();

            if (State["TechSupportEmail"] != null)
            {
                util.AddEmailToButton(SupportButton, State["TechSupportEmail"].ToString(), "Email To Tech Support");
            }

            util.UpdateSessionLog(State, "post", "TabManageData");

            State["DatabaseEvents"] = DatabaseEvents;
            State["SpreadSheetEvents"] = SpreadSheetEvents;
            State["WebServiceEvents"] = WebServiceEvents;
            State["ManageDataApps"] = ManageDataApps;
            ManageDataType.Attributes.Add("onclick", "checkChangingManageDataType(this);");
            string attr = "javascript: NamedPopUp('Dialogs/Design/StoryBoard.aspx', 'StoryBoardPopup','height=900, width=460, left=0, top=400, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');return false;";
            ViewStoryBoard.Attributes.Add("onclick", attr);


            ClearMessages();

            if (DataMultiPage.SelectedIndex == 2)
                return;

            WebServiceEventMappingStatus.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                "Dialogs/ManageData/EventMappingStatus.aspx", 500, 500, false, false, false, true));

            Init init = new Init();
            if (State["ResetManageDataApps"] != null)
            {
                State["SelectedApp"] = null;
                init.InitManageDataAppsList(State);
                State["ResetManageDataApps"] = null;
            }

            if (ManageDataApps.Items.Count == 0 || ManageDataApps.SelectedValue.Contains("->"))
            {
                init.InitManageDataAppsList(State);
                ManageDataType.Style.Value = "display:none";
                ManageDataTypeLabel.Style.Value = "display:none";
                ViewStoryBoard.Style.Value = "display:none";
                ShouldRefreshStoryBoard.Text = "close";
                ManageTypeMultiPage.SelectedIndex = Constants.BLANK_PAGE;
                State["ManageDataType"] = null;
            }

            if (ManageDataApps.SelectedIndex > 0)
            {
                ViewStoryBoard.Style.Value = "";
                ManageDataType.Style.Value = "";
                ManageDataTypeLabel.Style.Value = "";

                string target = Request.Form.Get("__EVENTTARGET");
                if (target != "SaveDataRequestMap" &&
                    target != "SaveDataResponseMap" &&
                    target != "WebServiceResponseTreeView")
                    PrepareAppDisplay(target);
                if (target == "ViewConnectionString")
                {
                    DatabaseCommandsView.Nodes.Clear();
                    DatabaseEvents.SelectedIndex = 0;
                    SpreadsheetCommandsView.Nodes.Clear();
                    SpreadSheetEvents.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }
    }
    /***************************************** Common Functions *******************************/
    public void ManageDataApps_SelectedIndexChanged(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

 
        ClearMessages();

         State["ManageDataType"] = null;
        string app_name = Request.Form.Get("ManageDataApps");
        if (!app_name.Contains("->"))
        {
             State["SelectedApp"] = app_name;
            ManageDataApps.SelectedValue = app_name;
            InitDataTrees(app_name);
            ViewStoryBoard.Style.Value = "";
            ManageDataType.Style.Value = "";
            ManageDataTypeLabel.Style.Value = "";
        }
        else
        {
            ManageDataType.Style.Value = "display:none";
            ManageDataTypeLabel.Style.Value = "display:none";
            ViewStoryBoard.Style.Value = "display:none";
            util.ResetAppStateVariables(State);
            ContentMultiPage.SelectedIndex = 0;
            ShouldRefreshStoryBoard.Text = "close";
            Init init = new Init();
            init.InitManageDataAppsList(State);
            DataMultiPage.SelectedIndex = 3;
        }
    }
    protected void ManageDataType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        RemoveAllConfigs();

        switch (ManageDataType.SelectedIndex)
        {
            case 0:
                ManageTypeMultiPage.SelectedIndex = Constants.BLANK_PAGE;
                 State["ManageDataType"] = null;
                break;
            case Constants.GOOGLE_DOCS_INDEX:
                ManageTypeMultiPage.SelectedIndex = Constants.GOOGLE_DOCS_PAGE;
                 State["ManageDataType"] = "google_spreadsheet";
                GoogleDocsConfigMessage.Text = "To start, click 'Connect to your Google Docs'";
                SpreadSheetEvents.Style.Value = "display:none";
                SpreadSheetEventsLabel.Style.Value = "display:none";
                break;
            case Constants.DATABASE_INDEX:
                ManageTypeMultiPage.SelectedIndex = Constants.DATABASE_PAGE;
                 State["ManageDataType"] = "database";
                DatabaseConfigMessage.Text = "To start, click 'Upload Database Info'";
                DatabaseEvents.Style.Value = "display:none";
                DatabaseEventsLabel.Style.Value = "display:none";
                break;
            case Constants.WEB_SERVICE_INDEX:
                ManageTypeMultiPage.SelectedIndex = Constants.WEB_SERVICE_PAGE;
                 State["ManageDataType"] = "web_service";
                WebServiceEvents.SelectedIndex = 0;
                break;
        }
        InitDataTrees(ManageDataApps.SelectedValue);
    }
    public void PrepareAppDisplay(string target)
    {
        ContentMultiPage.SelectedIndex = 1;
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State["SelectedApp"] == null)
            return;
        string app_name =  State["SelectedApp"].ToString();
        ManageDataApps.SelectedValue = app_name;

        if (WebServiceEvents.Items.Count == 0 || target == "TabMenu")
        {
            InitDataTrees(app_name);
            DataMultiPage.SelectedIndex = 3;
        }
    }
    protected void WebServiceEvents_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        DataTypeTabStrip.SelectedIndex = 0;
        DataMultiPage.SelectedIndex = 0;
        BuildWebServiceDataTrees(e.Text);
    }
    protected void InitDataTrees(string app)
    {
        try
        {
            WebServiceEvents.Items.Clear();
            WebServiceEvents.Items.Add(new RadComboBoxItem("Select ->", ""));

            DatabaseEvents.Items.Clear();
            DatabaseEvents.Items.Add(new RadComboBoxItem("Select ->", ""));

            SpreadSheetEvents.Items.Clear();
            SpreadSheetEvents.Items.Add(new RadComboBoxItem("Select ->", ""));

            PhoneRequestTreeView.Nodes.Clear();
            WebServiceInputTreeView.Nodes.Clear();
            PhoneResponseTreeView.Nodes.Clear();
            WebServiceResponseTreeView.Nodes.Clear();
            DatabaseCommandsView.Nodes.Clear();
            SpreadsheetCommandsView.Nodes.Clear();

            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            XmlDocument doc = util.GetStagingAppXml(State, app);
            if (doc == null)
                return;

            //do events from phone 
            XmlNodeList buttons = doc.SelectNodes("//fields/button | //fields/image_button | //fields/table | //fields/switch | //fields/checkbox");
            foreach (XmlNode button in buttons)
            {
                XmlNode page_node = button.ParentNode.ParentNode.SelectSingleNode("name");
                XmlNode id_node = button.SelectSingleNode("id");
                string event_field = id_node.InnerText.Trim();

                string button_id = button.SelectSingleNode("id").InnerText;
                XmlNode submit = button.SelectSingleNode("submit");
                if (submit != null)
                {
                    string page_name = null;
                    XmlNode post_node = submit.SelectSingleNode("post");
                    if (post_node != null) //new syntax
                    {
                        XmlNode response_page_node = post_node.SelectSingleNode("response_page");
                        if (response_page_node != null)
                            page_name = response_page_node.InnerText;
                        else
                            page_name = page_node.InnerText;
                    }
                    else //old syntax
                    {
                        string action = submit.InnerText;
                        //this code is compatible with old syntax
                        if (action.StartsWith("post") ) 
                        {
                            if (action.Contains("response_page:") || action.Contains("response_page~"))
                            {
                                string[] split = action.Split(";".ToCharArray());
                                if (split[0].Length > 19)
                                    page_name = split[0].Substring(19);
                                else
                                    page_name = split[1].Substring(14);
                            }
                            else
                                page_name = page_node.InnerText;
                        }
                        
                    }
                    if (page_name != null)
                    {
                        WebServiceEvents.Items.Add(new RadComboBoxItem(event_field, page_node.InnerText + ":" + page_name + ":" + event_field));
                        DatabaseEvents.Items.Add(new RadComboBoxItem(event_field, page_node.InnerText + ":" + page_name + ":" + event_field));
                        SpreadSheetEvents.Items.Add(new RadComboBoxItem(event_field, page_node.InnerText + ":" + page_name + ":" + event_field));
                    }
                }
            }

            //set default first
            ManageDataType.SelectedIndex = 0;
            ManageTypeMultiPage.SelectedIndex = Constants.BLANK_PAGE;

            if ( State["ManageDataType"] != null)
            {
                switch ( State["ManageDataType"].ToString())
                {
                    case "google_spreadsheet":
                        ManageDataType.SelectedIndex = Constants.GOOGLE_DOCS_INDEX;
                        ManageTypeMultiPage.SelectedIndex = Constants.GOOGLE_DOCS_PAGE;
                        break;
                    case "database":
                        ManageDataType.SelectedIndex = Constants.DATABASE_INDEX;
                        ManageTypeMultiPage.SelectedIndex = Constants.DATABASE_PAGE;
                        break;
                    case "web_service":
                        ManageDataType.SelectedIndex = Constants.WEB_SERVICE_INDEX;
                        ManageTypeMultiPage.SelectedIndex = Constants.WEB_SERVICE_PAGE;
                        break;
                }
            }
            else
            {
                //set defaut data manage type
                XmlNode database_type = doc.SelectSingleNode("//mobiflex_project/database_config/database_type");
                if (database_type != null)
                {
                    XmlNode database_config = database_type.ParentNode;
                    //check if GoogleDocs type
                    if (database_type.InnerText == "GoogleDocs")
                    {
                        ManageDataType.SelectedIndex = Constants.GOOGLE_DOCS_INDEX;
                        ManageTypeMultiPage.SelectedIndex = Constants.GOOGLE_DOCS_PAGE;
                         State["ManageDataType"] = "google_spreadsheet";
                        SpreadSheetEvents.Style.Value = "";
                        SpreadSheetEventsLabel.Style.Value = "";
                    }
                    else{
                         ManageDataType.SelectedIndex = Constants.DATABASE_INDEX;
                        ManageTypeMultiPage.SelectedIndex = Constants.DATABASE_PAGE;
                         State["ManageDataType"] = "database";
                        DatabaseEvents.Style.Value = "";
                        DatabaseEvents.Style.Value = "";
                    }

                    XmlNode connection_string = database_config.SelectSingleNode("connection_string");
                    if (connection_string != null)
                    {
                         State["DBConnectionString"] = connection_string.InnerText;
                        ViewConnectionString.Visible = true;
                    }
                    else
                    {
                        ViewConnectionString.Visible = false;
                        if (ManageDataType.SelectedIndex == Constants.DATABASE_INDEX)
                            DatabaseConfigMessage.Text = "First click 'Upload Database Info'";
                        else
                            GoogleDocsConfigMessage.Text = "First click 'Connect to your Google Docs'";
                    }

                    XmlNode database_webservice_url = database_config.SelectSingleNode("database_webservice_url");
                    if (database_webservice_url != null)
                         State["DBWebServiceURL"] = database_webservice_url.InnerText;
                }
                else
                {
                    XmlNode webservice_config = doc.SelectSingleNode("//mobiflex_project/phone_data_requests");
                    if (webservice_config != null && webservice_config.ChildNodes.Count != 0)
                    {
                        ManageDataType.SelectedIndex = Constants.WEB_SERVICE_INDEX;
                        ManageTypeMultiPage.SelectedIndex = Constants.WEB_SERVICE_PAGE;
                         State["ManageDataType"] = "web_service";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            util.LogError(State, ex);
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
    /******************************************* Web Service Functions *************************************/
    protected void BuildWebServiceDataTrees(string event_name)
    {
        string event_field = event_name;
        string button_id = event_name;
        RadComboBoxItem item = WebServiceEvents.Items.FindItemByText(event_name);
        WebServiceEvents.SelectedIndex = item.Index;
        if (event_name.Contains("->"))
        {
            DataMultiPage.SelectedIndex = 3;
            return;
        }
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string app = State["SelectedApp"].ToString();

        string[] page_names = item.Value.Split(":".ToCharArray());
        string from_page = page_names[0];
        string to_page = page_names[1];
        if (to_page == "_this_page")
            to_page = from_page;
        PhoneRequestTreeView.Nodes.Clear();
        WebServiceInputTreeView.Nodes.Clear();
        PhoneResponseTreeView.Nodes.Clear();
        WebServiceResponseTreeView.Nodes.Clear();

        RadTreeNode WebServiceInputRoot = new RadTreeNode(event_field);
        WebServiceInputRoot.CssClass = "RadTreeView";
        WebServiceInputRoot.ImageUrl = "~/images/editor_images/button.png";
        WebServiceInputTreeView.Nodes.Add(WebServiceInputRoot);

        RadTreeNode WebServiceResponseRoot = new RadTreeNode(event_field);
        WebServiceResponseRoot.CssClass = "RadTreeView";
        WebServiceResponseRoot.ImageUrl = "~/images/editor_images/button.png";
        WebServiceResponseTreeView.Nodes.Add(WebServiceResponseRoot);

        Util util = new Util();
        XmlUtil x_util = new XmlUtil();
        XmlDocument doc = util.GetStagingAppXml(State, app);
        XmlNode project_node = doc.SelectSingleNode("//mobiflex_project");
        XmlNode phone_data_requests = project_node.SelectSingleNode("phone_data_requests");
        bool update_xml = false;
        if (phone_data_requests == null)
        {
             phone_data_requests = x_util.CreateNode(doc, project_node, "phone_data_requests");
             update_xml = true;
        }

        XmlNode web_service_data_responses = doc.SelectSingleNode("//mobiflex_project/web_service_data_responses");
        if (web_service_data_responses == null)
        {
            web_service_data_responses = x_util.CreateNode(doc, project_node, "web_service_data_responses");
            update_xml = true;
        }
        if (update_xml)
        {
            State["AppXmlDoc"] = doc;
            util.UpdateStagingAppXml(State);
        }

        Hashtable input_to_event_map = new Hashtable();

        XmlNodeList pages = doc.SelectNodes("//pages/page");
        foreach (XmlNode page in pages)
        {
            XmlNode page_name_node = page.SelectSingleNode("name");
            string page_name = page_name_node.InnerText;
            RadTreeNode PhoneRequestRoot = null;
            RadTreeNode PhoneResponseRoot = null;
            if (page_name == from_page || SeeAllPages.Checked)
            {
                PhoneRequestRoot = new RadTreeNode(page_name);
                PhoneRequestRoot.CssClass = "RadTreeView";
                PhoneRequestRoot.ImageUrl = "~/images/ascx.gif";
                PhoneRequestTreeView.Nodes.Add(PhoneRequestRoot);
            }
            if (page_name == to_page || SeeAllPages.Checked)
            {
                PhoneResponseRoot = new RadTreeNode(page_name);
                PhoneResponseRoot.CssClass = "RadTreeView";
                PhoneResponseRoot.ImageUrl = "~/images/ascx.gif";
                PhoneResponseTreeView.Nodes.Add(PhoneResponseRoot);
                PhoneResponseRoot.Category = "page";
            }

            if (page_name != from_page && page_name != to_page && !SeeAllPages.Checked)
                continue;

            //do inputs from phone 
            XmlNodeList input_nodes = page.SelectNodes("fields/text_area | fields/label | fields/text_field | fields/hidden_field");

            foreach (XmlNode input_node in input_nodes)
            {
                // phone input field
                XmlNode id_node = input_node.SelectSingleNode("id");
                if (page_name == from_page || SeeAllPages.Checked)
                {
                    string input_field = id_node.InnerText.Trim();
                    CreateInputFieldNode(PhoneRequestRoot, input_field, input_node.Name, phone_data_requests, input_to_event_map);
                }
                else if (page_name == to_page || SeeAllPages.Checked)
                {
                    //phone output field
                    RadTreeNode id = util.CreateFieldNode(PhoneResponseRoot, id_node.InnerText, input_node.Name);
                    id.Category = "output";
                    id.BackColor = Color.FromArgb(250, 252, 156); //yellow

                    CreateWebResponseFieldNode(id, phone_data_requests);

                    AddStatusNodes(id, phone_data_requests, id.Text + "_status");
                }
            }


            if (page_name == from_page || SeeAllPages.Checked)
            {

                XmlNodeList gps_fields = page.SelectNodes("fields/gps");

                foreach (XmlNode gps_field in gps_fields)
                {
                    XmlNode id_node = gps_field.SelectSingleNode("id");
                    string input_field = id_node.InnerText.Trim();
                    RadTreeNode gps_node = CreateInputFieldNode(PhoneRequestRoot, input_field, "gps", phone_data_requests, input_to_event_map);

                    XmlNode part_node = gps_field.SelectSingleNode("latitude");
                    CreateInputFieldNode(gps_node, part_node.InnerText, "gps_field", phone_data_requests, input_to_event_map);
                    part_node = gps_field.SelectSingleNode("longitude");
                    CreateInputFieldNode(gps_node, part_node.InnerText, "gps_field", phone_data_requests, input_to_event_map);
                }
            }

            XmlNodeList pickers = page.SelectNodes("fields/picker");
            foreach (XmlNode picker in pickers)
            {
                XmlNode id_node = picker.SelectSingleNode("id");
                XmlNode fields = picker.SelectSingleNode("picker_fields");

                //picker fields are both inputs and outputs
                if (page_name == from_page || SeeAllPages.Checked)
                {
                    string input_field = id_node.InnerText.Trim();
                    RadTreeNode picker_node = CreateInputFieldNode(PhoneRequestRoot, input_field, "picker", phone_data_requests, input_to_event_map);
                    //picker itself is not an input - the fields are
                    picker_node.BackColor = Color.White;
                    picker_node.Category = "";

                    if (fields != null)
                    {
                        XmlNodeList field_list = fields.SelectNodes("picker_field");
                        foreach (XmlNode field_item in field_list)
                        {
                            XmlNode field_name = field_item.SelectSingleNode("name");

                            //table fields can be inputs too
                            input_field = field_name.InnerText.Trim();
                            CreateInputFieldNode(picker_node, input_field, "picker_field", phone_data_requests, input_to_event_map);
                        }
                    }
                    else // for time and date types
                    {
                             input_field = id_node.InnerText.Trim();
                            CreateInputFieldNode(picker_node, input_field, "picker_field", phone_data_requests, input_to_event_map);                       
                    }
                }
                else if (page_name == to_page || SeeAllPages.Checked)
                {
                    RadTreeNode id = util.CreateFieldNode(PhoneResponseRoot, id_node.InnerText, "picker");
                    id.Category = "picker";
                    if (fields != null)
                    {
                        XmlNodeList field_list = fields.SelectNodes("picker_field");
                        foreach (XmlNode field_item in field_list)
                        {
                            XmlNode field_name = field_item.SelectSingleNode("name");

                            RadTreeNode name_node = new RadTreeNode(field_name.InnerText);
                            name_node.CssClass = "RadTreeView";
                            name_node.ImageUrl = "~/images/target.gif";
                            name_node.Category = "output";
                            name_node.BackColor = Color.FromArgb(250, 252, 156); //yellow
                            id.Nodes.Add(name_node);

                            CreateWebResponseFieldNode(name_node, phone_data_requests);
                        }
                    }
                    else // for time and date types
                    {
                        RadTreeNode name_node = new RadTreeNode(id_node.InnerText);
                        name_node.CssClass = "RadTreeView";
                        name_node.ImageUrl = "~/images/target.gif";
                        name_node.Category = "output";
                        name_node.BackColor = Color.FromArgb(250, 252, 156); //yellow
                        id.Nodes.Add(name_node);

                        CreateWebResponseFieldNode(name_node, phone_data_requests);
                    }
                    AddStatusNodes(id, phone_data_requests, id.Text + "_status");
                }
            }


            //do responses to phone 
            XmlNodeList tables = page.SelectNodes("fields/table");
            foreach (XmlNode table in tables)
            {
                XmlNode id_node = table.SelectSingleNode("id");
                XmlNode fields = table.SelectSingleNode("table_fields");
                XmlNodeList field_list = fields.SelectNodes("table_field");

                //table fields are both inputs and outputs
                if (page_name == from_page || SeeAllPages.Checked)
                {
                    string input_field = id_node.InnerText.Trim();
                    RadTreeNode table_node = CreateInputFieldNode(PhoneRequestRoot, input_field, "table", phone_data_requests, input_to_event_map);
                    //table itself is not an input
                    table_node.BackColor = Color.White;
                    table_node.Category = "";
                    foreach (XmlNode field_item in field_list)
                    {
                        XmlNode field_name = field_item.SelectSingleNode("name");

                        //table fields can be inputs too
                        input_field = field_name.InnerText.Trim();
                        CreateInputFieldNode(table_node, input_field, "table_field", phone_data_requests, input_to_event_map);
                    }
                }
                if (page_name == to_page || SeeAllPages.Checked)
                {
                    RadTreeNode id = util.CreateFieldNode(PhoneResponseRoot, id_node.InnerText, "table");
                    foreach (XmlNode field_item in field_list)
                    {
                        XmlNode field_name = field_item.SelectSingleNode("name");

                        RadTreeNode name_node = new RadTreeNode(field_name.InnerText);
                        name_node.CssClass = "RadTreeView";
                        name_node.ImageUrl = "~/images/target.gif";
                        name_node.Category = "output";
                        name_node.BackColor = Color.FromArgb(250, 252, 156); //yellow
                        id.Nodes.Add(name_node);

                        CreateWebResponseFieldNode(name_node, phone_data_requests);
                    }

                    AddStatusNodes(id, phone_data_requests, id.Text + "_status");
                }
            }

            if (page_name == to_page || SeeAllPages.Checked)
            {
                XmlNodeList field_list2 = page.SelectNodes("fields/alert | fields/web_view | fields/image");
                foreach (XmlNode field2 in field_list2)
                {
                    XmlNode id_node = field2.SelectSingleNode("id");

                    RadTreeNode id = util.CreateFieldNode(PhoneResponseRoot, id_node.InnerText, field2.Name);
                    id.Category = "output";
                    id.BackColor = Color.FromArgb(250, 252, 156); //yellow

                    CreateWebResponseFieldNode(id, phone_data_requests);

                    AddStatusNodes(id, phone_data_requests, id.Text + "_status");
                }
            }
        }

        string node_value = app + "~" + event_field + "~" + button_id;
        RadTreeNode url_node = new RadTreeNode();
        url_node.CssClass = "RadTreeView";
        url_node.ImageUrl = "~/images/function_call.gif";
        url_node.AllowEdit = true;
        url_node.Value = node_value;
        WebServiceInputRoot.Nodes.Add(url_node);
        if ( State[node_value] == null)
        {
            string default_label = "Click here twice to set the Web Service URL";
            string mapped_url = null;
            XmlNodeList mapped_page_requests = phone_data_requests.SelectNodes("//phone_data_request/event_field[.='" + event_field + "']");
            if (mapped_page_requests != null)
            {
                foreach (XmlNode mapped_page_request in mapped_page_requests)
                {
                    XmlNode mapped_event_field = mapped_page_request.ParentNode.SelectSingleNode("event_field[.='" + button_id + "']");
                    if (mapped_event_field != null)
                    {
                        XmlNode web_service = mapped_event_field.ParentNode.SelectSingleNode("web_service");
                        XmlNode method_node = mapped_event_field.ParentNode.SelectSingleNode("method");
                        if (web_service != null && method_node != null)
                        {
                            url_node.Text = web_service.InnerText;
                            mapped_url = web_service.InnerText;
                        }
                        else
                        {
                            XmlNode web_service_method = mapped_event_field.ParentNode.SelectSingleNode("web_service_method");
                            mapped_url = web_service_method.InnerText;
                            url_node.Text = mapped_url.Substring(0, mapped_url.LastIndexOf("/"));
                        }
                        InitWebServiceNodes(doc, event_field, url_node, WebServiceResponseRoot, phone_data_requests);
                        break;
                    }

                    if (mapped_url != null)
                        break;
                }
                if (mapped_url == null)
                    url_node.Text = default_label;
            }
            else
                url_node.Text = default_label;
        }
        else
        {
            url_node.Text =  State[node_value].ToString();
            InitWebServiceNodes(doc, event_field, url_node, WebServiceResponseRoot, phone_data_requests);
        }

        PhoneRequestTreeView.ExpandAllNodes();
        WebServiceInputTreeView.ExpandAllNodes();
        PhoneResponseTreeView.ExpandAllNodes();
    }
    protected RadTreeNode CreateInputFieldNode(RadTreeNode PhoneRequestRoot, string input_field, string field_type, XmlNode phone_data_requests, Hashtable input_to_event_map)
    {
        bool is_field_mapped = false;
        Util util = new Util();
        XmlNode mapped_node = phone_data_requests.SelectSingleNode("//phone_data_request/event_field[.='" + WebServiceEvents.SelectedItem.Text + "']");
        if (mapped_node != null)
        {
            mapped_node = mapped_node.ParentNode.SelectSingleNode("input_mapping/input_field[.='" + input_field + "']");
            if (mapped_node != null)
                is_field_mapped = true;
        }

        if (!is_field_mapped)
        {
            RadTreeNode id = util.CreateFieldNode(PhoneRequestRoot, input_field, field_type);
            if (input_to_event_map.ContainsKey(input_field))
            {
                id.Value = "page:" + PhoneRequestRoot.Text +
                     ";event_field:" + input_to_event_map[input_field].ToString() +
                     ";input_field:" + input_field;
            }
            else
            {
                id.Value = "page:" + PhoneRequestRoot.Text +
                    ";input_field:" + input_field;
            }
            //id.Style = "background-color:#99ffb9;";
            if (field_type == "gps_field" ||
                field_type == "table_field" ||
                field_type == "text_field" ||
                field_type == "label" ||
                 field_type == "speech_reco" ||
                  field_type == "hidden_field" ||
                 field_type == "text_area" ||
                field_type == "picker_field")
            {
                id.BackColor = Color.FromArgb(153, 255, 185); //LIGHT GREEN
                id.Category = "request";
            }

            return id;
        }
        else
        {
            return null;
        }
    }
    protected void InitWebServiceNodes(XmlDocument doc, string event_field, RadTreeNode url_node,
        RadTreeNode WebServiceResponseRoot, XmlNode phone_data_requests)
    {
        string url = url_node.Text;
        url_node.ExpandParentNodes();
        url_node.CollapseChildNodes();
        Hashtable WebServiceMethodInputs = new Hashtable();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        State["WebServiceMethodInputs"] = WebServiceMethodInputs;
        RadTreeNode out_url = new RadTreeNode(url);
        out_url.CssClass = "RadTreeView";
        out_url.ImageUrl = "~/images/dialog.gif";
        WebServiceResponseRoot.Nodes.Add(out_url);

        Util util = new Util();

        if (!url.EndsWith("?WSDL"))
            url += "?WSDL";
        string xml = util.GetWebPage(url);
        if (xml == null)
        {
            SaveRequestMessage.Text = "Accessing " + url + " resulted in an error";
            return;
        }
        XmlDocument WebDoc = new XmlDocument();
        WebDoc.LoadXml(xml);
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(WebDoc.NameTable);
        nsmgr.AddNamespace("s", "http://www.w3.org/2001/XMLSchema");

        XmlNodeList web_methods = WebDoc.SelectNodes("//s:schema/s:element", nsmgr);
        if (web_methods.Count == 0)
        {
            nsmgr.AddNamespace("wsdl", "http://schemas.xmlsoap.org/wsdl/");
            web_methods = WebDoc.SelectNodes("//wsdl:operation", nsmgr);
            if (web_methods.Count == 0)
            {
                SaveRequestMessage.Text = "Could not find any methods with URL: " + url;
                return;
            }
        }
        Hashtable used_names = new Hashtable();
        foreach (XmlNode web_method in web_methods)
        {
            //throw out certain methods with <s:any /> 
            if (web_method.InnerXml.Contains("s:any"))
                continue;
            string web_method_name = web_method.Attributes["name"].Value;
            if (used_names.ContainsKey(web_method_name))
                continue;
            used_names[web_method_name] = true;
            ArrayList MethodInputs = new ArrayList();
            string service_url = out_url.Text;
            if (out_url.Text.ToLower().EndsWith("?wsdl"))
                service_url = service_url.Remove(url.Length - 5);
            WebServiceMethodInputs[service_url + "/" + web_method_name] = MethodInputs;
            RadTreeNode in_method_node = new RadTreeNode(web_method_name);
            in_method_node.CssClass = "RadTreeView";
            in_method_node.ImageUrl = "~/images/forward_nav.gif";
            url_node.Nodes.Add(in_method_node);

            //response web methods
            XmlNode mapped_node = phone_data_requests.SelectSingleNode("//event_field[.='" + event_field + "']");
            RadTreeNode out_method_node = null;
            if (mapped_node != null &&  State["WebServiceTestDoc"] != null &&  State["WebServiceResponseTreeViewNodeText"] != null &&
                  State["WebServiceResponseTreeViewNodeText"].ToString() == web_method_name)
            {
                //make sure we are in the right event
                XmlNode method_node = mapped_node.ParentNode.SelectSingleNode("method");
                if (method_node != null && method_node.InnerText == web_method_name)
                {
                    XmlDocument TestDoc = (XmlDocument) State["WebServiceTestDoc"];
                    out_method_node = new RadTreeNode(web_method_name);
                     State["WebServiceResponseTreeViewMethodText"] = web_method_name;
                    out_method_node.CssClass = "RadTreeView";
                    out_method_node.ImageUrl = "~/images/backward_nav.gif";
                    out_method_node.Category = "method";
                    out_method_node.Value = service_url + ";" + web_method_name + ";";
                    out_url.Nodes.Add(out_method_node);
                    out_method_node.Value.Remove(out_method_node.Value.Length - 1, 1);
                    WebServiceResponseTreeView.ExpandAllNodes();

                    XmlToTreeViewNode(TestDoc.FirstChild.NextSibling, out_method_node, web_method_name);

                    out_method_node.ExpandParentNodes();

                    Session.Remove("WebServiceTestDoc");
                }
            }
            else if (mapped_node != null)
            {
                XmlNode web_service_node = mapped_node.ParentNode.SelectSingleNode("web_service[.='" + service_url + "']");
                XmlNode method_node = mapped_node.ParentNode.SelectSingleNode("method[.='" + web_method_name + "']");
                if (web_service_node != null && method_node != null)
                {
                    in_method_node.BackColor = Color.LightCoral;

                    out_method_node = new RadTreeNode(web_method_name);
                    out_method_node.CssClass = "RadTreeView";
                    out_method_node.ImageUrl = "~/images/backward_nav.gif";
                    out_method_node.Category = "method";
                    out_method_node.Value = service_url + ";" + web_method_name + ";";
                    out_url.Nodes.Add(out_method_node);
                    out_method_node.Value.Remove(out_method_node.Value.Length - 1, 1);
                    WebServiceResponseTreeView.ExpandAllNodes();
                }
            }

            XmlNodeList parms = web_method.SelectNodes("s:complexType/s:sequence/s:element", nsmgr);
            foreach (XmlNode parm in parms)
            {
                string parm_name = parm.Attributes["name"].Value;
                MethodInputs.Add(parm_name);
                if (out_method_node != null)
                    out_method_node.Value += parm_name + ",";
                RadTreeNode parm_node = new RadTreeNode(parm_name);
                parm_node.CssClass = "RadTreeView";
                parm_node.ImageUrl = "~/images/dot.gif";
                parm_node.Category = "input";
                parm_node.BackColor = Color.FromArgb(250, 252, 156);
                in_method_node.Nodes.Add(parm_node);

                if (mapped_node != null)
                {
                    XmlNode web_service_node = mapped_node.ParentNode.SelectSingleNode("web_service[.='" + service_url + "']");
                    XmlNode method_node = mapped_node.ParentNode.SelectSingleNode("method[.='" + web_method_name + "']");
                    if (web_service_node != null && method_node != null)
                    {
                        XmlNode web_service_method_input_node = mapped_node.ParentNode.SelectSingleNode("input_mapping/web_service_method_input[.='" + parm_name + "']");
                        if (web_service_method_input_node != null)
                        {
                            XmlNode input_field_node = web_service_method_input_node.ParentNode.SelectSingleNode("input_field");
                            if (input_field_node != null)
                            {
                                string request_name = input_field_node.InnerText;
                                XmlNode field_node = doc.SelectSingleNode("//id[.='" + request_name + "']");
                                RadTreeNode request_node = null;
                                if (field_node == null)

                                    request_node = util.CreateFieldNode(parm_node, request_name, "");
                                else
                                    request_node = util.CreateFieldNode(parm_node, request_name, field_node.ParentNode.Name);

                                request_node.Text = request_name;
                                request_node.Category = "request";
                                request_node.BackColor = Color.FromArgb(153, 255, 185); //LIGHT GREEN
                            }
                        }
                    }
                }
            }
            RadTreeNode save_method_node = new RadTreeNode("Save calling this method with 0 or more inputs mapped");
            save_method_node.CssClass = "RadTreeView";
            save_method_node.ImageUrl = "~/images/save.gif";
            save_method_node.Category = "save";
            in_method_node.Nodes.Add(save_method_node);

            RadTreeNode undo_method_inputs_node = new RadTreeNode("Undo mapping of device fields to this method");
            undo_method_inputs_node.CssClass = "RadTreeView";
            undo_method_inputs_node.ImageUrl = "~/images/cancel.png";
            undo_method_inputs_node.Category = "delete";
            in_method_node.Nodes.Add(undo_method_inputs_node);
            in_method_node.ExpandChildNodes();
        }
    }
    protected void OnWebServiceInputTreeNodeEdit(object sender, Telerik.Web.UI.RadTreeNodeEditEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        string url = e.Text;
        if (url.ToLower().EndsWith("?wsdl"))
            url = url.Remove(url.Length - 5, 5);

        e.Node.Text = url;
        State[e.Node.Value] = url;
        BuildWebServiceDataTrees(WebServiceEvents.SelectedItem.Text);
        e.Node.ExpandChildNodes();
    }
    protected void ClearMessages()
    {
        TestWebServiceMessage.Text = "";
        SaveRequestMessage.Text = "";
        ResponseMessage.Text = "";
        DatabaseConfigMessage.Text = "";
        GoogleDocsConfigMessage.Text = "";
    }
    protected void DeleteWebServiceInputTreeViewMethod(RadTreeNode view_method_node)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        XmlDocument doc = util.GetStagingAppXml(State);
         State["AppXmlDoc"] = doc;
        XmlNode phone_data_requests = doc.SelectSingleNode("//mobiflex_project/phone_data_requests");
        if (phone_data_requests == null)
            return;
        string method = view_method_node.Text;

        XmlNode xml_method_node = phone_data_requests.SelectSingleNode("phone_data_request/method[.='" + method + "']");
        if (xml_method_node != null)
        {
            XmlNode phone_data_request = xml_method_node.ParentNode;
            phone_data_request.ParentNode.RemoveChild(phone_data_request);

            XmlNode web_service_data_responses = doc.SelectSingleNode("//mobiflex_project/web_service_data_responses");
            xml_method_node = web_service_data_responses.SelectSingleNode("web_service_data_response/method[.='" + method + "']");
            if (xml_method_node != null)
            {
                XmlNode phone_data_response = xml_method_node.ParentNode;
                web_service_data_responses.RemoveChild(phone_data_response);
            }

            util.UpdateStagingAppXml(State);
        }

        BuildWebServiceDataTrees(WebServiceEvents.SelectedItem.Text);
    }
    protected void TestWebService_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            RadTreeNode method_node = WebServiceResponseTreeView.Nodes[0].Nodes[0].Nodes[0]; //causes exception when use tries to build output before input is done
             State["WebServiceResponseTreeViewNodeValue"] = method_node.Value;
             State["WebServiceResponseTreeViewNodeText"] = method_node.Text;

            DataSet paramsDS = new DataSet("ParameterDataSet");

            DataTable paramTable = paramsDS.Tables.Add("ParamTable");

            DataColumn paramCol = paramTable.Columns.Add("param", typeof(String));

            string[] node_values = method_node.Value.Split(";".ToCharArray());

             State["WebServiceURL"] = node_values[0];
             State["WebServiceMethod"] = node_values[1];

            string[] param_values = node_values[2].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
             State["WebServiceMethodInputValues"] = param_values;

            foreach (string param_value in param_values)
            {
                DataRow row = paramTable.NewRow();
                string[] row_array = new string[1];
                row_array[0] = param_value;
                row.ItemArray = row_array;
                paramTable.Rows.Add(row);
            }

            ParamRepeater.DataSource = paramsDS;
            ParamRepeater.DataBind();

            SaveDataResponses();
            DataMultiPage.SelectedIndex = 2;
        }
        catch
        {
            return; //caused when use tries to build output before input is done 
        }
    }
    protected string GetSoap(string web_service_url, string method, string[] method_params)
    {
        XmlDocument SoapEnv = new XmlDocument();
        Util util = new Util();

        if (web_service_url.ToLower().Contains(".php/") || web_service_url.ToLower().EndsWith(".php"))
        {
            SoapEnv.Load(MapPath(".") + @"\templates\SoapEnvelopeTemplatePHP.xml");
            util.SetSoapEnvelopeTemplatePHP(SoapEnv, web_service_url, method, method_params);
        }
        else if (web_service_url.ToLower().Contains(".asmx/") || web_service_url.ToLower().EndsWith(".asmx"))
        {
            SoapEnv.Load(MapPath(".") + @"\templates\SoapEnvelopeTemplateASMX.xml");
            util.SetSoapEnvelopeTemplateASMX(SoapEnv, web_service_url, method, method_params);
        }
        else
            throw new Exception("Cannot recognize web service type");


        return SoapEnv.InnerXml;
    }
    protected void TestWebServiceButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        string[] param_values = (string[]) State["WebServiceMethodInputValues"];
        string web_service_url =  State["WebServiceURL"].ToString();
        string method =  State["WebServiceMethod"].ToString();
        XmlDocument TestDoc = null;
        try
        {
            StringBuilder inputs = new StringBuilder();
            for (int i = 1; i <= ParamRepeater.Items.Count; i++)
            {
                string index = i.ToString();
                if (i < 10)
                    index = "0" + index;
                string text = Request.Form.Get("ParamRepeater$ctl" + index + "$paramInput");

                inputs.Append("&" + param_values[i - 1] + "=" + text);
            }

            if (ParamRepeater.Items.Count > 0 && inputs[0] == '?')
                inputs.Remove(0, 1);

            XmlDocument SoapEnv = new XmlDocument();
            if (web_service_url.ToLower().Contains(".php/") || web_service_url.ToLower().EndsWith(".php"))
            {
                SoapEnv.Load(MapPath(".") + @"\templates\SoapEnvelopeTemplatePHP.xml");
                util.SetSoapEnvelopePHP(SoapEnv, web_service_url, method, inputs.ToString());
                TestDoc = util.HttpSOAPRequest(State, web_service_url, SoapEnv, null);
            }
            else if (web_service_url.ToLower().Contains(".asmx/") || web_service_url.ToLower().EndsWith(".asmx"))
            {
                SoapEnv.Load(MapPath(".") + @"\templates\SoapEnvelopeTemplateASMX.xml");
                util.SetSoapEnvelopeASMX(SoapEnv, web_service_url, method, inputs.ToString());
                TestDoc = util.HttpSOAPRequest(State, web_service_url, SoapEnv, null);
            }
            else
                throw new Exception("Cannot recognize web service type");
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            TestWebServiceMessage.Text = "Calling the web service return an error: " + ex.Message;
            return;
        }

         State["WebServiceTestDoc"] = TestDoc;

        BuildWebServiceDataTrees(WebServiceEvents.SelectedItem.Text);

        ResponseMessage.Text = "Expand the nodes for " +  State["WebServiceResponseTreeViewNodeText"].ToString();

        DataMultiPage.SelectedIndex = 1;
    }
    protected void ClearTest_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        DataMultiPage.SelectedIndex = 1;
    }
    protected void XmlToTreeViewNode(XmlNode from, RadTreeNode to, string web_method_name)
    {
        bool is_text = false;
        string node_text = from.Name;
        if (node_text == "#text")
        {
            node_text = from.InnerText;
            is_text = true;
        }
        RadTreeNode node = new RadTreeNode(node_text);
        node.CssClass = "RadTreeView";
        if (!is_text)
        {
            node.ImageUrl = "~/images/dot.gif";
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            node.Value = "web_service:url(" + State["WebServiceURL"].ToString() + ");method:" + web_method_name;
            node.Category = "response";
            node.BackColor = Color.FromArgb(153, 255, 185); //LIGHT GREEN
        }
        to.Nodes.Add(node);

        foreach (XmlNode xml_node in from.ChildNodes)
        {
            XmlToTreeViewNode(xml_node, node, web_method_name);
        }
    }
    protected void SaveDataRequestMap_Click(object sender, EventArgs e)
    {

        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ClearMessages();
        XmlUtil x_util = new XmlUtil();
        XmlNodeList phone_data_request_list = null;
        string[] event_method = ClickedNodeInfo.Text.Split(";".ToCharArray());
        string event_field = event_method[0];
        string method = event_method[1];

        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode project_node = doc.SelectSingleNode("//mobiflex_project");
        XmlNode phone_data_requests = project_node.SelectSingleNode("phone_data_requests");
        if (phone_data_requests == null)
        {
            phone_data_requests = x_util.CreateNode(doc, project_node, "phone_data_requests");
        }
        State["AppXmlDoc"] = doc;

        string edit_text = RequestTreeEdits.Text;
        RequestTreeEdits.Text = "";
        if (edit_text.Length == 0) //save web service method call with no inputs
        {
            RadTreeNode method_node = WebServiceInputTreeView.FindNodeByText(method);
            RadTreeNode web_service_node = method_node.ParentNode;
            RadTreeNode event_node = web_service_node.ParentNode;

            // find the node in the xml
            XmlNode mapped_node = phone_data_requests.SelectSingleNode("phone_data_request/event_field[.='" + event_field + "']");
            if (mapped_node != null)
            {
                XmlNode deletephone_data_request = mapped_node.ParentNode;
                deletephone_data_request.ParentNode.RemoveChild(deletephone_data_request);
            }

            XmlNode phone_data_request = x_util.CreateNode(doc, phone_data_requests, "phone_data_request");
            x_util.CreateNode(doc, phone_data_request, "event_field", event_node.Text);
            x_util.CreateNode(doc, phone_data_request, "web_service", web_service_node.Text);
            x_util.CreateNode(doc, phone_data_request, "method", method_node.Text);
            phone_data_request_list = phone_data_requests.SelectNodes("phone_data_request");
        }
        else
        {
            string[] edits = edit_text.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            //get list 
            ArrayList edit_array = new ArrayList();

            foreach (string edit in edits)
            {
                Hashtable edit_list = new Hashtable();
                string[] requests = edit.Split(";".ToCharArray());
                foreach (string request in requests)
                {
                    string[] parts = request.Split(":".ToCharArray());
                    switch (parts[0])
                    {
                        case "event_field":
                            edit_list["event_field"] = parts[1];
                            break;
                        case "input_field":
                            edit_list["input_field"] = parts[1];
                            break;
                        case "web_service":
                            if (parts.Length == 3)
                                parts[1] += ":" + parts[2];
                            string[] url_parts = parts[1].Split("()".ToCharArray());
                            edit_list["web_service"] = url_parts[1];
                            break;
                        case "method":
                            edit_list["method"] = parts[1];
                            break;
                        case "web_service_method_input":
                            edit_list["web_service_method_input"] = parts[1];
                            break;
                    }
                }

                edit_array.Add(edit_list);
            }
            // first create phone_data_request for any place where node has been dropped
            foreach (Hashtable list in edit_array)
            {
                if (list["event_field"] == null)
                    continue;
                string event_item = list["event_field"].ToString();

                //does event already exist? if yes remove it first, then create a new one
                XmlNode event_field_node = phone_data_requests.SelectSingleNode("phone_data_request/event_field[.='" + event_item + "']");
                if (event_field_node != null)
                {
                    XmlNode deletephone_data_request = event_field_node.ParentNode;
                    deletephone_data_request.ParentNode.RemoveChild(deletephone_data_request);
                }

                //create new data_request
                if (method == list["method"].ToString())
                {
                    XmlNode phone_data_request = x_util.CreateNode(doc, phone_data_requests, "phone_data_request");
                    x_util.CreateNode(doc, phone_data_request, "event_field", event_item);
                    x_util.CreateNode(doc, phone_data_request, "web_service", list["web_service"].ToString());
                    x_util.CreateNode(doc, phone_data_request, "method", list["method"].ToString());
                }

            }

            //fill in all the web_service_method_inputs whether they are matched to inputs or not, because
            //if they are not matched we still need to set the inputs to empty
            XmlNode input_mapping = null;
            phone_data_request_list = phone_data_requests.SelectNodes("phone_data_request");
            foreach (XmlNode phone_data_request in phone_data_request_list)
            {
                Hashtable WebServiceMethodInputs = (Hashtable) State["WebServiceMethodInputs"];

                ArrayList MethodInputs = null;
                //get inputs
                XmlNode web_service = phone_data_request.SelectSingleNode("web_service");
                XmlNode method_node = phone_data_request.SelectSingleNode("method");
                string combine = web_service.InnerText + "/" + method_node.InnerText;
                MethodInputs = (ArrayList)WebServiceMethodInputs[combine];

                foreach (string param in MethodInputs)
                {
                    XmlNode param_node = phone_data_request.SelectSingleNode("input_mapping/web_service_method_input[.='" + param + "']");
                    if (param_node == null)
                    {
                        input_mapping = x_util.CreateNode(doc, phone_data_request, "input_mapping");
                        x_util.CreateNode(doc, input_mapping, "web_service_method_input", param);
                    }
                }
            }

            //fill in the matching input_field mapping
            foreach (Hashtable list in edit_array)
            {
                string list_method = list["method"].ToString();
                XmlNode method_node = phone_data_requests.SelectSingleNode("phone_data_request/method[.='" + list_method + "']");
                string web_service = list["web_service"].ToString();
                XmlNode web_service_node = method_node.ParentNode.SelectSingleNode("web_service[.='" + web_service + "']");

                //does event already exist?
                if (web_service_node != null && method_node != null)
                {
                    XmlNode web_service_method_input =
                        web_service_node.ParentNode.SelectSingleNode("input_mapping/web_service_method_input[.='" +
                        list["web_service_method_input"].ToString() + "']");
                    if (web_service_method_input != null)
                    {
                        x_util.CreateNode(doc, web_service_method_input.ParentNode, "input_field", list["input_field"].ToString());
                    }
                }
            }
        }

        //get the soap envelope for each request
        foreach (XmlNode phone_data_request in phone_data_request_list)
        {
            XmlNode web_service = phone_data_request.SelectSingleNode("web_service");
            string web_service_url = web_service.InnerText;
            // if (web_service_url.ToLower().Contains(".php/") || web_service_url.ToLower().EndsWith(".php"))
            //{
            XmlNode soap_node = phone_data_request.SelectSingleNode("soap");
            if (soap_node == null)
            {
                XmlNode method_node = phone_data_request.SelectSingleNode("method");
                XmlNodeList mappings = phone_data_request.SelectNodes("input_mapping");
                string[] method_params = new string[mappings.Count];
                int i = 0;
                foreach (XmlNode mapping in mappings)
                {
                    XmlNode input_node = mapping.SelectSingleNode("web_service_method_input");
                    method_params[i++] = input_node.InnerText;
                }
                string soap = GetSoap(web_service.InnerText, method_node.InnerText, method_params);
                x_util.CreateNode(doc, phone_data_request, "soap", soap);
            }
            // }
        }


        util.UpdateStagingAppXml(State);
        BuildWebServiceDataTrees(WebServiceEvents.SelectedItem.Text);
        RequestTreeEdits.Text = "";
        SaveRequestMessage.Text = "Call to " + method + " has been saved.";
    }
    protected void ResetMethodCall_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        string[] event_method = ClickedNodeInfo.Text.Split(";".ToCharArray());
        string event_field = event_method[0];
        string method = event_method[1];
        RadTreeNode node = WebServiceInputTreeView.FindNodeByText(method);

        // find the node in the xml
        XmlDocument doc = util.GetStagingAppXml(State);
         State["AppXmlDoc"] = doc;
        XmlNode phone_data_requests = doc.SelectSingleNode("//mobiflex_project/phone_data_requests");
        if (phone_data_requests == null)
            return;
        XmlNode mapped_node = phone_data_requests.SelectSingleNode("phone_data_request/event_field[.='" + event_field + "']");
        if (mapped_node != null)
        {
            XmlNode phone_data_request = mapped_node.ParentNode;
            phone_data_requests.RemoveChild(phone_data_request);
            util.UpdateStagingAppXml(State);
        }

        XmlNode web_service_data_responses = doc.SelectSingleNode("//mobiflex_project/web_service_data_responses");
        mapped_node = web_service_data_responses.SelectSingleNode("//web_service_data_response/method[.='" + method + "']");
        if (mapped_node != null)
        {
            XmlNode web_service_data_response = mapped_node.ParentNode;
            web_service_data_responses.RemoveChild(web_service_data_response);
            util.UpdateStagingAppXml(State);
        }
        BuildWebServiceDataTrees(WebServiceEvents.SelectedItem.Text);

        SaveRequestMessage.Text = "Method Call Map Reset";
        RequestTreeEdits.Text = "";
    }
    protected void ResetDataRequestMap_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ClearMessages();
         XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode phone_data_requests = doc.SelectSingleNode("//mobiflex_project/phone_data_requests");
        if (phone_data_requests == null)
            return;
        phone_data_requests.RemoveAll();
        util.UpdateStagingAppXml(State);

        ResetDataResponseMap_Click(sender, e);

        SaveRequestMessage.Text = "Request Map Reset.";
        RequestTreeEdits.Text = "";
    }
    protected void SaveDataResponseMap_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        SaveDataResponses();
        BuildWebServiceDataTrees(WebServiceEvents.SelectedItem.Text);
        ResponseMessage.Text = "Response Map Saved.";
    }
    protected void SaveDataResponses()
    {
        ClearMessages();
        XmlUtil x_util = new XmlUtil();
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        XmlDocument doc = util.GetStagingAppXml(State);
         State["AppXmlDoc"] = doc;
        string edit_text = ResponseTreeEdits.Text;
        ResponseTreeEdits.Text = "";
        string[] edits = edit_text.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

        //get list in a hashtable
        ArrayList edit_array = new ArrayList();
        foreach (string edit in edits)
        {
            Hashtable edit_list = new Hashtable();
            string[] responses = edit.Split(";".ToCharArray());
            foreach (string response in responses)
            {
                string[] parts = response.Split(":".ToCharArray());
                switch (parts[0])
                {
                    case "output_field":
                        edit_list["output_field"] = parts[1].Trim();
                        break;
                    case "table":
                        edit_list["table"] = parts[1].Trim();
                        break;
                    case "web_service":
                        if (parts.Length == 3)
                            parts[1] += ":" + parts[2];
                        string[] url_parts = parts[1].Split("()".ToCharArray());
                        edit_list["web_service"] = url_parts[1].Trim();
                        break;
                    case "method":
                        edit_list["method"] = parts[1].Trim();
                        break;
                    case "web_service_response":
                        edit_list["web_service_response"] = parts[1].Trim();
                        break;
                    case "status_success_phrase":
                        edit_list["status_success_phrase"] = parts[1].Trim();
                        break;
                    case "status_error_alert":
                        edit_list["status_error_alert"] = parts[1].Trim();
                        break;
                }
            }
            edit_array.Add(edit_list);
        }
        XmlNode output_mapping = null;
        XmlNode web_service_data_responses = doc.SelectSingleNode("//mobiflex_project/web_service_data_responses");
        if (web_service_data_responses != null) //old encoding
        {
            foreach (Hashtable list in edit_array)
            {
                XmlNode web_service_data_response = null;
                string method = list["method"].ToString();
                string web_service = list["web_service"].ToString();
                //does method already exist?
                XmlNode method_node = web_service_data_responses.SelectSingleNode("web_service_data_response/method[.='" + method + "']");
                if (method_node == null)
                {
                    //create new web_service_data_response
                    web_service_data_response = x_util.CreateNode(doc, web_service_data_responses, "web_service_data_response");
                    x_util.CreateNode(doc, web_service_data_response, "web_service", web_service);
                    method_node = x_util.CreateNode(doc, web_service_data_response, "method", method);
                    if (list["table"] != null)
                        x_util.CreateNode(doc, web_service_data_response, "table", list["table"].ToString());
                    output_mapping = x_util.CreateNode(doc, web_service_data_response, "output_mapping");
                    x_util.CreateNode(doc, output_mapping, "output_field", list["output_field"].ToString());
                    x_util.CreateNode(doc, output_mapping, "web_service_response", list["web_service_response"].ToString());
                }
                else
                {
                    web_service_data_response = method_node.ParentNode;
                    XmlNode web_service_response = web_service_data_responses.SelectSingleNode("web_service_data_response/output_mapping/web_service_response[.='" + list["web_service_response"].ToString() + "']");
                    if (web_service_response != null)
                    {
                        output_mapping = web_service_response.ParentNode;
                        XmlNode output_field = output_mapping.SelectSingleNode("output_field");
                        output_field.InnerText = list["output_field"].ToString();
                    }
                    else
                    {
                        output_mapping = x_util.CreateNode(doc, web_service_data_response, "output_mapping");
                        x_util.CreateNode(doc, output_mapping, "output_field", list["output_field"].ToString());
                        x_util.CreateNode(doc, output_mapping, "web_service_response", list["web_service_response"].ToString());
                    }
                }
                if (list.ContainsKey("status_success_phrase"))
                {
                    XmlNode status_success_phrase = web_service_data_responses.SelectSingleNode("web_service_data_response/status_success_phrase");
                    if (status_success_phrase != null)
                        status_success_phrase.InnerText = list["status_success_phrase"].ToString();
                    else
                        x_util.CreateNode(doc, web_service_data_response, "status_success_phrase", list["status_success_phrase"].ToString());
                }
                if (list.ContainsKey("status_error_alert"))
                {
                    XmlNode status_error_alert = web_service_data_responses.SelectSingleNode("web_service_data_response/status_error_alert");
                    if (status_error_alert != null)
                        status_error_alert.InnerText = list["status_error_alert"].ToString();
                    else
                        x_util.CreateNode(doc, web_service_data_response, "status_error_alert", list["status_error_alert"].ToString());
                }
            }
        }

        //new encoding
        XmlNode project_node = doc.SelectSingleNode("//mobiflex_project");
        XmlNode phone_data_requests = project_node.SelectSingleNode("phone_data_requests");
        if (phone_data_requests == null)
        {
            phone_data_requests = x_util.CreateNode(doc, project_node, "phone_data_requests");
        }
        XmlNode event_field = phone_data_requests.SelectSingleNode("phone_data_request/event_field[.='" + WebServiceEvents.SelectedItem.Text + "']");
        XmlNode phone_data_request = event_field.ParentNode;

        output_mapping = null;
        foreach (Hashtable list in edit_array)
        {
            XmlNode web_service_response = phone_data_request.SelectSingleNode("output_mapping/web_service_response[.='" + list["web_service_response"].ToString() + "']");
            if (web_service_response != null)
            {
                output_mapping = web_service_response.ParentNode;
                XmlNode output_field = output_mapping.SelectSingleNode("output_field");
                output_field.InnerText = list["output_field"].ToString();
            }
            else
            {
                output_mapping = x_util.CreateNode(doc, phone_data_request, "output_mapping");
                x_util.CreateNode(doc, output_mapping, "output_field", list["output_field"].ToString());
                x_util.CreateNode(doc, output_mapping, "web_service_response", list["web_service_response"].ToString());
            }

            if (list.ContainsKey("status_success_phrase"))
            {
                XmlNode status_success_phrase = phone_data_request.SelectSingleNode("phone_data_request/status_success_phrase");
                if (status_success_phrase != null)
                    status_success_phrase.InnerText = list["status_success_phrase"].ToString();
                else
                    x_util.CreateNode(doc, phone_data_request, "status_success_phrase", list["status_success_phrase"].ToString());
            }
            if (list.ContainsKey("status_error_alert"))
            {
                XmlNode status_error_alert = phone_data_request.SelectSingleNode("phone_data_request/status_error_alert");
                if (status_error_alert != null)
                    status_error_alert.InnerText = list["status_error_alert"].ToString();
                else
                    x_util.CreateNode(doc, phone_data_request, "status_error_alert", list["status_error_alert"].ToString());
            }
        }

        util.UpdateStagingAppXml(State);
    }

    public void ResetDataResponseMap_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ClearMessages();
        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode web_service_data_responses = doc.SelectSingleNode("//mobiflex_project/web_service_data_responses");
        if (web_service_data_responses != null)
        {
            web_service_data_responses.RemoveAll();
        }

        XmlNode phone_data_requests = doc.SelectSingleNode("//mobiflex_project/phone_data_requests");
        if (phone_data_requests == null)
            return;
        XmlNodeList event_fields = phone_data_requests.SelectNodes("phone_data_request/event_field");
        foreach (XmlNode event_field in event_fields)
        {
            XmlNode phone_data_request = event_field.ParentNode;
            XmlNodeList output_mappings = phone_data_request.SelectNodes("output_mapping");
            foreach (XmlNode output_mapping in output_mappings)
            {
                phone_data_request.RemoveChild(output_mapping);
            }
        }

        util.UpdateStagingAppXml(State);

        BuildWebServiceDataTrees(WebServiceEvents.SelectedItem.Text);
        ResponseMessage.Text = "Response Map Reset.";
        ResponseTreeEdits.Text = "";
    }
    private bool CreateWebResponseFieldNode(RadTreeNode phone_field_node, XmlNode phone_data_requests)
    {
        string phone_field_name = phone_field_node.Text;
        bool is_field_mapped = false;
        XmlNode mapped_node = phone_data_requests.SelectSingleNode("//phone_data_request/event_field[.='" + WebServiceEvents.SelectedItem.Text + "']");
        if (mapped_node != null)
        {
            mapped_node = mapped_node.ParentNode.SelectSingleNode("output_mapping/output_field[.='" + phone_field_name + "']");
            if (mapped_node != null)
                is_field_mapped = true;
        }
        if (is_field_mapped)
        {
            XmlNode field_node = mapped_node.ParentNode.SelectSingleNode("web_service_response");
            RadTreeNode response_node = new RadTreeNode(field_node.InnerText);
            response_node.CssClass = "RadTreeView";
            response_node.ImageUrl = "~/images/dot.gif";
            response_node.Category = "response";
            response_node.BackColor = Color.FromArgb(153, 255, 185); //LIGHT GREEN
            phone_field_node.Nodes.Add(response_node);
        }
        return is_field_mapped;
    }
    private void AddStatusNodes(RadTreeNode phone_field_node, XmlNode phone_data_requests, string status_name)
    {
        RadTreeNode status = new RadTreeNode(status_name);
        status.CssClass = "RadTreeView";
        status.ImageUrl = "~/images/status.png";
        status.Category = "output";
        status.Value = "status";
        status.BackColor = Color.FromArgb(250, 252, 156); //yellow
        phone_field_node.Nodes.Add(status);

        string phone_field_name = phone_field_node.Text;
        bool is_field_mapped = false;
        XmlNode mapped_node = phone_data_requests.SelectSingleNode("//phone_data_request/event_field[.='" + WebServiceEvents.SelectedItem.Text + "']");
        if (mapped_node != null)
        {
            mapped_node = mapped_node.ParentNode.SelectSingleNode("output_mapping/output_field[.='" + status_name + "']");
            if (mapped_node != null)
                is_field_mapped = true;
        }

        if (is_field_mapped)
        {
            XmlNode output_mapping = mapped_node.ParentNode;
            XmlNode phone_data_request = output_mapping.ParentNode;
            XmlNode web_service_response = output_mapping.SelectSingleNode("web_service_response");
            RadTreeNode web_service_response_node = new RadTreeNode(web_service_response.InnerText);
            web_service_response_node.CssClass = "RadTreeView";
            web_service_response_node.ImageUrl = "~/images/dot.gif";
            web_service_response_node.BackColor = Color.FromArgb(153, 255, 185); //LIGHT GREEN
            status.Nodes.Add(web_service_response_node);

            XmlNode success_phrase = phone_data_request.SelectSingleNode("status_success_phrase");
            if (success_phrase != null)
            {
                RadTreeNode success_phrase_node = new RadTreeNode("success phrase");
                success_phrase_node.CssClass = "RadTreeView";
                success_phrase_node.BackColor = Color.FromArgb(153, 255, 185); //LIGHT GREEN
                web_service_response_node.Nodes.Add(success_phrase_node);

                RadTreeNode success_phrase_node2 = new RadTreeNode(success_phrase.InnerText);
                success_phrase_node2.CssClass = "RadTreeView";
                success_phrase_node2.AllowEdit = true;
                success_phrase_node2.BackColor = Color.FromArgb(153, 255, 185); //LIGHT GREEN
                success_phrase_node.Nodes.Add(success_phrase_node2);
            }

            XmlNode error_alert = phone_data_request.SelectSingleNode("status_error_alert");
            if (error_alert != null)
            {
                RadTreeNode error_alert_node = new RadTreeNode("error alert");
                error_alert_node.CssClass = "RadTreeView";
                error_alert_node.BackColor = Color.FromArgb(153, 255, 185); //LIGHT GREEN
                web_service_response_node.Nodes.Add(error_alert_node);

                RadTreeNode error_alert_node2 = new RadTreeNode(error_alert.InnerText);
                error_alert_node2.CssClass = "RadTreeView";
                error_alert_node2.AllowEdit = true;
                error_alert_node2.BackColor = Color.FromArgb(153, 255, 185); //LIGHT GREEN
                error_alert_node.Nodes.Add(error_alert_node2);
            }
        }
    }
    protected void SeeAllPages_CheckedChanged(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (WebServiceEvents.SelectedIndex != 0)
            BuildWebServiceDataTrees(WebServiceEvents.SelectedItem.Text);
    }
    /************************************************ Database and Google Docs Functions ********************************/
    protected void BuildDatabaseTrees(string event_name)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State["ManageDataType"].ToString() == "database")
        {
            if ( State["DBConnectionString"] != null)
                ViewConnectionString.Visible = true;
            else
                ViewConnectionString.Visible = false;
        }

         State["SelectedDBEvent"] = event_name;
        string event_field = event_name;
        string button_id = event_name;
        RadComboBoxItem item = null;
        if ( State["ManageDataType"].ToString() == "database")
        {
            item = DatabaseEvents.Items.FindItemByText(event_name);
            DatabaseEvents.SelectedIndex = item.Index;
        }
        else
        {
            item = SpreadSheetEvents.Items.FindItemByText(event_name);
            SpreadSheetEvents.SelectedIndex = item.Index;
        }
        if (event_name.Contains("->"))
        {
            DataMultiPage.SelectedIndex = 3;
            return;
        }
        string app =  State["SelectedApp"].ToString();

        string[] page_names = item.Value.Split(":".ToCharArray());
        string from_page = page_names[0];
        string to_page = page_names[1];
        RadTreeNode DatabaseCommandRoot = CreateDatabaseRootNode(event_field);       

        Util util = new Util();
        XmlDocument doc = util.GetStagingAppXml(State, app);
        XmlNode database_config = doc.SelectSingleNode("//mobiflex_project/database_config");
        if (database_config == null)
        {
            database_config = doc.CreateElement("database_config");
            doc.FirstChild.NextSibling.AppendChild(database_config);
            util.UpdateStagingAppXml(State, app);
            if ( State["ManageDataType"].ToString() == "database")
                DatabaseConfigMessage.Text = "Click on 'Get Database Info'";
            else
                GoogleDocsConfigMessage.Text = "Click on 'Connect to Your Google Docs'";

            return;
        }

        InitDatabaseCommandsView(DatabaseCommandRoot, event_field);

        if ( State["ManageDataType"].ToString() == "database")
        {
            DatabaseCommandsView.ExpandAllNodes();
        }
        else
        {
            SpreadsheetCommandsView.ExpandAllNodes();
        }
    }
    private RadTreeNode CreateDatabaseRootNode(string event_field)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State["ManageDataType"].ToString() == "database")
        {
            DatabaseCommandsView.Nodes.Clear();
        }
        else
        {
            SpreadsheetCommandsView.Nodes.Clear();
        }

        RadTreeNode DatabaseCommandRoot = new RadTreeNode(event_field);
        DatabaseCommandRoot.CssClass = "RadTreeView";
        DatabaseCommandRoot.Category = "event";
        Control CommandControl = LoadControl("Controls/AddCommand.ascx");
        DatabaseCommandRoot.Controls.Add(CommandControl);

        if (State["ManageDataType"].ToString() == "database")
        {
            DatabaseCommandsView.Nodes.Add(DatabaseCommandRoot);
        }
        else
        {
            SpreadsheetCommandsView.Nodes.Add(DatabaseCommandRoot);
        }
        return DatabaseCommandRoot;
    }
    protected RadTreeNode CreateDeviceDataFieldNode(RadTreeNode PhoneDataFieldsRoot, string input_field, string field_type, Hashtable input_to_event_map)
    {
        Util util = new Util();
        RadTreeNode id = util.CreateFieldNode(PhoneDataFieldsRoot, input_field, field_type);
        if (input_to_event_map.ContainsKey(input_field))
        {
            id.Value = "page:" + PhoneDataFieldsRoot.Text +
                 ";event_field:" + input_to_event_map[input_field].ToString() +
                 ";input_field:" + input_field;
        }
        else
        {
            id.Value = "page:" + PhoneDataFieldsRoot.Text +
                ";input_field:" + input_field;
        }
        if (field_type == "gps_field" ||
            field_type == "table_field" ||
            field_type == "text_field" ||
            field_type == "label" ||
             field_type == "speech_reco" ||
              field_type == "hidden_field" ||
             field_type == "text_area" ||
            field_type == "picker_field")
        {
            id.BackColor = Color.FromArgb(153, 255, 185); //LIGHT GREEN
            id.Category = "request";
        }

        return id;
    }

    protected void InitDatabaseCommandsView(RadTreeNode DatabaseCommandRoot, string event_field)
    {
        XmlUtil x_util = new XmlUtil();
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode database_config = doc.SelectSingleNode("//mobiflex_project/database_config");

        XmlNode events = database_config.SelectSingleNode("events");
        if (events == null)
            return;

        XmlNode event_name_node = events.SelectSingleNode("event/event_name[.='" + event_field + "']");
        if (event_name_node == null)
            return;

        XmlNode event_node = event_name_node.ParentNode;

        XmlNode sql_commands = event_node.SelectSingleNode("sql_commands");
        if (sql_commands == null)
            return;

        ArrayList DBCommands = new ArrayList();

        XmlNodeList sql_command_list = sql_commands.SelectNodes("sql_command");

        foreach (XmlNode sql_command in sql_command_list)
        {
            if (sql_command.InnerXml.Length == 0)
                continue;
            Hashtable CommandEntry = new Hashtable();
            string command = sql_command.SelectSingleNode("command").InnerText.ToLower();
            CommandEntry["command"] = command;
            if (command == "if")
            {
                CommandEntry["command_condition_device_field1"] = sql_command.SelectSingleNode("command_condition_phone_field1").InnerText;
                CommandEntry["command_condition_operation"] = sql_command.SelectSingleNode("command_condition_operation").InnerText;
                CommandEntry["command_condition_device_field2"] = sql_command.SelectSingleNode("command_condition_phone_field2").InnerText;
            }
            else
            {

                CommandEntry["table"] = sql_command.SelectSingleNode("table").InnerText;

                XmlNodeList database_fields = sql_command.SelectNodes("field_item");
                if (database_fields != null)
                {
                    ArrayList DBFields = new ArrayList();
                    foreach (XmlNode field in database_fields)
                    {
                        if (field.InnerXml.Length == 0)
                            continue;
                        Hashtable FieldEntry = new Hashtable();
                        FieldEntry["database_field"] = field.SelectSingleNode("database_field").InnerText;
                        XmlNode phone_field_node = field.SelectSingleNode("phone_field");
                        if (phone_field_node != null)
                            FieldEntry["device_field"] = phone_field_node.InnerText;
                        else
                            FieldEntry["device_field"] = "";

                        DBFields.Add(FieldEntry);
                    }
                    CommandEntry["database_fields"] = DBFields;

                }
                XmlNodeList conditions = sql_command.SelectNodes("condition");
                if (conditions != null)
                {
                    ArrayList DBConditions = new ArrayList();
                    foreach (XmlNode condition in conditions)
                    {
                        if (condition.InnerXml.Length == 0)
                            continue;
                        Hashtable ConditionEntry = new Hashtable();
                        ConditionEntry["condition_operation"] = condition.SelectSingleNode("condition_operation").InnerText;
                        ConditionEntry["condition_1st_field"] = condition.SelectSingleNode("condition_1st_field").InnerText;
                        ConditionEntry["field_operation"] = condition.SelectSingleNode("field_operation").InnerText;
                        XmlNode node = condition.SelectSingleNode("condition_2nd_field");
                        if (node != null)
                            ConditionEntry["condition_2nd_field"] = node.InnerText;
                        else
                            ConditionEntry["condition_2nd_field"] = "";
                        DBConditions.Add(ConditionEntry);
                    }
                    CommandEntry["conditions"] = DBConditions;

                }
                XmlNode order_by = sql_command.SelectSingleNode("order_by");
                if (order_by != null)
                {
                    Hashtable OrderBy = new Hashtable();
                    if (order_by.InnerXml.Length > 0)
                    {
                        OrderBy["sort_field"] = order_by.SelectSingleNode("sort_field").InnerText;
                        OrderBy["sort_direction"] = order_by.SelectSingleNode("sort_direction").InnerText;
                        CommandEntry["order_by"] = OrderBy;
                    }
                }
            }
            DBCommands.Add(CommandEntry);
        }

         State["DBCommands"] = DBCommands;

        RefreshCommandsView();
    }


    protected void SaveDatabaseConfig_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        XmlUtil x_util = new XmlUtil();
         XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode database_config = doc.SelectSingleNode("//mobiflex_project/database_config");

        XmlNode events = database_config.SelectSingleNode("events");
        if (events == null)
            events = x_util.CreateNode(doc, database_config, "events");

        XmlNode event_name_node = events.SelectSingleNode("event/event_name[.='" +  State["SelectedDBEvent"].ToString() + "']");
        XmlNode event_node = null;
        if (event_name_node == null)
        {
            event_node = x_util.CreateNode(doc, events, "event");
        }
        else
        {
            event_node = event_name_node.ParentNode;
            event_node.RemoveAll();
        }
        x_util.CreateNode(doc, event_node, "event_name",  State["SelectedDBEvent"].ToString());


        XmlNode sql_commands = x_util.CreateNode(doc, event_node, "sql_commands");

        ArrayList DBCommands = (ArrayList) State["DBCommands"];

        foreach (Hashtable CommandEntry in DBCommands)
        {
            XmlNode sql_command = x_util.CreateNode(doc, sql_commands, "sql_command");
            string command = CommandEntry["command"].ToString().ToLower();
            x_util.CreateNode(doc, sql_command, "command", command);
            if (command == "if")
            {
                x_util.CreateNode(doc, sql_command, "command_condition_phone_field1", CommandEntry["command_condition_device_field1"].ToString());
                x_util.CreateNode(doc, sql_command, "command_condition_operation", CommandEntry["command_condition_operation"].ToString());
                x_util.CreateNode(doc, sql_command, "command_condition_phone_field2", CommandEntry["command_condition_device_field2"].ToString());
            }
            else
            {
                x_util.CreateNode(doc, sql_command, "table", CommandEntry["table"].ToString());

                ArrayList DBFields = (ArrayList)CommandEntry["database_fields"];
                if (DBFields != null)
                {
                    foreach (Hashtable FieldEntry in DBFields)
                    {
                        XmlNode field_item = x_util.CreateNode(doc, sql_command, "field_item");
                        if (FieldEntry["database_field"] != null && FieldEntry["database_field"].ToString().Length > 0)
                        {
                            x_util.CreateNode(doc, field_item, "database_field", FieldEntry["database_field"].ToString());
                        }
                        if (FieldEntry["device_field"] != null && FieldEntry["device_field"].ToString().Length > 0)
                        {
                            x_util.CreateNode(doc, field_item, "phone_field", FieldEntry["device_field"].ToString());
                        }
                    }
                }
                ArrayList DBWhere = (ArrayList)CommandEntry["conditions"];
                if (DBWhere != null)
                {
                    foreach (Hashtable ConditionEntry in DBWhere)
                    {
                        XmlNode condition = x_util.CreateNode(doc, sql_command, "condition");
                        if (ConditionEntry["condition_operation"] != null && ConditionEntry["condition_operation"].ToString().Length > 0)
                        {
                            x_util.CreateNode(doc, condition, "condition_operation", ConditionEntry["condition_operation"].ToString());
                        }
                        if (ConditionEntry["condition_1st_field"] != null && ConditionEntry["condition_1st_field"].ToString().Length > 0)
                        {
                            x_util.CreateNode(doc, condition, "condition_1st_field", ConditionEntry["condition_1st_field"].ToString());
                        }
                        if (ConditionEntry["field_operation"] != null && ConditionEntry["field_operation"].ToString().Length > 0)
                        {
                            x_util.CreateNode(doc, condition, "field_operation", ConditionEntry["field_operation"].ToString());
                        }
                        if (ConditionEntry["condition_2nd_field"] != null && ConditionEntry["condition_2nd_field"].ToString().Length > 0)
                        {
                            x_util.CreateNode(doc, condition, "condition_2nd_field", ConditionEntry["condition_2nd_field"].ToString());
                        }
                    }
                }
                Hashtable DBOrderBy = (Hashtable)CommandEntry["order_by"];
                if (DBOrderBy != null)
                {
                    XmlNode order_by = x_util.CreateNode(doc, sql_command, "order_by");

                    if (DBOrderBy["sort_field"] != null && DBOrderBy["sort_field"].ToString().Length > 0)
                    {
                        x_util.CreateNode(doc, order_by, "sort_field", DBOrderBy["sort_field"].ToString());
                    }
                    if (DBOrderBy["sort_direction"] != null && DBOrderBy["sort_direction"].ToString().Length > 0)
                    {
                        x_util.CreateNode(doc, order_by, "sort_direction", DBOrderBy["sort_direction"].ToString());
                    }
                }
            }
        }
         State["AppXmlDoc"] = doc;
        util.UpdateStagingAppXml(State);

        RefreshCommandsView();
        if ( State["ManageDataType"].ToString() == "database")
            DatabaseConfigMessage.Text = "Query Commands for this event have been saved.";
        else
            GoogleDocsConfigMessage.Text = "Query Commands for this event have been saved.";
    }
    protected void RemoveAllConfigs()
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode database_config = doc.SelectSingleNode("//mobiflex_project/database_config");
        if (database_config != null)        
            database_config.RemoveAll();        

        XmlNode phone_data_requests = doc.SelectSingleNode("//mobiflex_project/phone_data_requests");
        if(phone_data_requests != null)
            phone_data_requests.RemoveAll();

         State["AppXmlDoc"] = doc;
        util.UpdateStagingAppXml(State);

         State["DBCommands"] = null;
    }
    protected void ResetDatabaseConfig_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        XmlUtil x_util = new XmlUtil();
        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode database_config = doc.SelectSingleNode("//mobiflex_project/database_config");
        XmlNode events = database_config.SelectSingleNode("events");
        if (events != null)
            events.RemoveAll();

         State["AppXmlDoc"] = doc;
        util.UpdateStagingAppXml(State);

         State["DBCommands"] = null;
        // RefreshCommandsView();

        if ( State["ManageDataType"].ToString() == "database")
        {
            DatabaseConfigMessage.Text = "All Sql Commands have been deleted for all events.";
            DatabaseEvents.SelectedIndex = 0;
            DatabaseCommandsView.Nodes.Clear();
        }
        else
        {
            GoogleDocsConfigMessage.Text = "All Query Commands have been deleted for all events.";
            SpreadSheetEvents.SelectedIndex = 0;
            SpreadsheetCommandsView.Nodes.Clear();
        }

    }
    protected void DatabaseEvents_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (State["ManageDataType"].ToString() == "database")
        {
            if (!util.DoesDatabaseInfoExist(State))
            {
                DatabaseConfigMessage.Text = "To start, click 'Upload Database Info'";
                return;
            }

            if (DatabaseEvents.SelectedIndex == 0)
            {
                DatabaseCommandsView.Nodes.Clear();
            }
            else
            {
                 State["DBCommands"] = null;
                BuildDatabaseTrees(e.Text);
            }
        }
        else
        {
            if (!util.DoesDatabaseInfoExist(State))
            {
                GoogleDocsConfigMessage.Text = "To start, click 'Connect to your Google Docs'";
                return;
            }
            if (SpreadSheetEvents.SelectedIndex == 0)
            {
                SpreadsheetCommandsView.Nodes.Clear();
            }
            else
            {
                 State["DBCommands"] = null;
                BuildDatabaseTrees(e.Text);
            }
        }
    }
    protected void UpdateDatabaseTree_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (ConnectionString.Text.Length > 0)
        {
             State["DBConnectionString"] = ConnectionString.Text;
            ConnectionString.Text = ""; //reset
            ViewConnectionString.Visible = true;
            DatabaseEvents.Style.Value = "";
            DatabaseEventsLabel.Style.Value = "";
        }
        if (WebServiceURLString.Text.Length > 0)
        {
             State["DBWebServiceURL"] = WebServiceURLString.Text;
            WebServiceURLString.Text = ""; //reset
            ViewConnectionString.Visible = true;
        }
        else // is Google docs
        {
            SpreadSheetEvents.Style.Value = "";
            SpreadSheetEventsLabel.Style.Value = "";
        }
        if ( State["ManageDataType"].ToString() == "database")
            BuildDatabaseTrees(DatabaseEvents.SelectedItem.Text);
        else
            BuildDatabaseTrees(SpreadSheetEvents.SelectedItem.Text);
    }
    protected string GetFirstDatabaseTable()
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode database_config = doc.SelectSingleNode("//mobiflex_project/database_config");
        if (database_config == null)
        {
            return null;
        }

        XmlNodeList table_list = database_config.SelectNodes("//tables/table");
        ArrayList tables = new ArrayList();
        foreach (XmlNode table_node in table_list)
        {
            XmlNode table_name = table_node.SelectSingleNode("table_name");
            tables.Add(table_name.InnerText);
        }
        tables.Sort();
        if (tables.Count == 0)
            return null;
        return tables[0].ToString();
    }
    protected void DBTreeClick_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ArrayList DBCommands = (ArrayList)State["DBCommands"];
        Hashtable CommandEntry = null;
        string[] split = DBTreeInfo.Text.Split(";".ToCharArray());
        string info_text = DBTreeInfo.Text;

        if (info_text.StartsWith("add_command_condition")) //this is order dependent because 2 items contain "add_command"
        {
            AddCommandCondition(GetCommandRoot());
            return;
        }
        else if (info_text.StartsWith("add_command"))
        {
            AddCommand(GetCommandRoot(), null);
            return;
        }
        //command_condition
        else if (info_text.StartsWith("command_condition_device_field1"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            CommandEntry["command_condition_device_field1"] = split[2];
        }
        else if (info_text.StartsWith("command_condition_operation"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            CommandEntry["command_condition_operation"] = split[2];
        }
        else if (info_text.StartsWith("command_condition_device_field2"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            CommandEntry["command_condition_device_field2"] = split[2];
        }
        else if (info_text.StartsWith("delete_command_condition"))//this is order dependent because 2 items contain "delete_command"
        {
            DBCommands.RemoveAt(Convert.ToInt32(split[1]));
        }
        else if (info_text.StartsWith("command_condition_add_then"))
        {
            AddCommand(GetCommandRoot(), "Then");
        }
        else if (info_text.StartsWith("command_condition_add_else"))
        {
            AddCommand(GetCommandRoot(), "Else");
        }
        //commands
        else if (info_text.StartsWith("delete_command"))
        {
            DBCommands.RemoveAt(Convert.ToInt32(split[1]));
        }
        else if (info_text.StartsWith("command"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            if (CommandEntry["command"] != null)
            {
                if (CommandEntry["command"].ToString().ToLower().StartsWith("then"))
                {
                    CommandEntry["command"] = "Then " + split[2];
                }
                else if (CommandEntry["command"].ToString().ToLower().StartsWith("else"))
                {
                    CommandEntry["command"] = "Else " + split[2];
                }
                else
                    CommandEntry["command"] = split[2];
            }
            else
                CommandEntry["command"] = split[2];
        }
        else if (info_text.StartsWith("table"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            CommandEntry["table"] = split[2];
            CommandEntry["database_fields"] = null;
            CommandEntry["conditions"] = null;
        }
        else if (info_text.StartsWith("add_field"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            ArrayList DBFields = (ArrayList)CommandEntry["database_fields"];
            if (DBFields == null)
                DBFields = new ArrayList();
            Hashtable field = new Hashtable();
            DBFields.Add(field);
            CommandEntry["database_fields"] = DBFields;
        }
        else if (info_text.StartsWith("add_condition"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            ArrayList DBWhere = (ArrayList)CommandEntry["conditions"];
            if (DBWhere == null)
                DBWhere = new ArrayList();
            Hashtable condition = new Hashtable();
            DBWhere.Add(condition);
            CommandEntry["conditions"] = DBWhere;
        }
        else if (info_text.StartsWith("add_order_by"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            if (CommandEntry["order_by"] == null)
                CommandEntry["order_by"] = new Hashtable();
        }

        //Do Field
        else if (info_text.StartsWith("database_field"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            ArrayList DBFields = (ArrayList)CommandEntry["database_fields"];
            if (DBFields != null)
            {
                Hashtable FieldEntry = (Hashtable)DBFields[Convert.ToInt32(split[2])];
                FieldEntry["database_field"] = split[3];
            }
        }
        else if (info_text.StartsWith("device_field"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            ArrayList DBFields = (ArrayList)CommandEntry["database_fields"];
            if (DBFields != null)
            {
                Hashtable FieldEntry = (Hashtable)DBFields[Convert.ToInt32(split[2])];
                FieldEntry["device_field"] = split[3];
            }
        }
        else if (info_text.StartsWith("delete_field"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            ArrayList DBFields = (ArrayList)CommandEntry["database_fields"];
            if (DBFields != null)
            {
                DBFields.RemoveAt(Convert.ToInt32(split[2]));
                if (DBFields.Count == 0)
                    CommandEntry["database_fields"] = null;
                else
                    CommandEntry["database_fields"] = DBFields;
            }
        }

            //Do Condition
        else if (info_text.StartsWith("condition_operation"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            ArrayList DBConditions = (ArrayList)CommandEntry["conditions"];
            if (DBConditions != null)
            {
                Hashtable ConditionEntry = (Hashtable)DBConditions[Convert.ToInt32(split[2]) - GetFieldCount(CommandEntry)];
                ConditionEntry["condition_operation"] = split[3];
            }
        }
        else if (info_text.StartsWith("condition_1st_field"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            ArrayList DBConditions = (ArrayList)CommandEntry["conditions"];
            if (DBConditions != null)
            {
                Hashtable ConditionEntry = (Hashtable)DBConditions[Convert.ToInt32(split[2]) - GetFieldCount(CommandEntry)];
                ConditionEntry["condition_1st_field"] = split[3];
            }
        }
        else if (info_text.StartsWith("field_operation"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            ArrayList DBConditions = (ArrayList)CommandEntry["conditions"];
            if (DBConditions != null)
            {
                Hashtable ConditionEntry = (Hashtable)DBConditions[Convert.ToInt32(split[2]) - GetFieldCount(CommandEntry)];
                ConditionEntry["field_operation"] = split[3];
            }
        }
        else if (info_text.StartsWith("condition_2nd_field"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            ArrayList DBConditions = (ArrayList)CommandEntry["conditions"];
            if (DBConditions != null)
            {
                Hashtable ConditionEntry = (Hashtable)DBConditions[Convert.ToInt32(split[2]) - GetFieldCount(CommandEntry)];
                ConditionEntry["condition_2nd_field"] = split[3];
            }
        }
        else if (info_text.StartsWith("delete_condition"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];

            ArrayList DBConditions = (ArrayList)CommandEntry["conditions"];
            if (DBConditions != null)
            {
                //2nd index is counted together with the number of fields
                //we need to subtract the number of fields before getting the real condition index
                DBConditions.RemoveAt(Convert.ToInt32(split[2]) - GetFieldCount(CommandEntry));
                if (DBConditions.Count == 0)
                    CommandEntry["conditions"] = null;
                else
                    CommandEntry["conditions"] = DBConditions;
            }
        }

        //ORDER BY
        else if (info_text.StartsWith("sort_field"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            Hashtable DBOrderBy = (Hashtable)CommandEntry["order_by"];
            if (DBOrderBy != null)
            {
                DBOrderBy["sort_field"] = split[2];
            }
        }
        else if (info_text.StartsWith("sort_direction"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            Hashtable DBOrderBy = (Hashtable)CommandEntry["order_by"];
            if (DBOrderBy != null)
            {
                DBOrderBy["sort_direction"] = split[2];
            }
        }
        else if (info_text.StartsWith("delete_order_by"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            Hashtable DBOrderBy = (Hashtable)CommandEntry["order_by"];
            if (DBOrderBy != null)
            {
                CommandEntry["order_by"] = null;
            }
        }

         State["DBCommands"] = DBCommands;
        RefreshCommandsView();
    }
    protected void AddCommandCondition(RadTreeNode DatabaseCommandRoot)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        ArrayList DBCommands = (ArrayList)State["DBCommands"];
        if (DBCommands == null)
            DBCommands = new ArrayList();

        if (DBCommands.Count > 0)
        {
            Hashtable prev_CommandEntry = (Hashtable)DBCommands[DBCommands.Count - 1];
            if (prev_CommandEntry["command"].ToString().ToLower() == "if")
            {
                GoogleDocsConfigMessage.Text = "A command condition cannot follow another command condition.";
                RefreshCommandsView();
                return;
            }
        }

        Hashtable newCommandEntry = new Hashtable();
        newCommandEntry["command"] = "If";
        newCommandEntry["command_condition_device_field1"] = null;
        newCommandEntry["command_condition_operation"] = null;
        newCommandEntry["command_condition_device_field2"] = null;


        DBCommands.Add(newCommandEntry);
         State["DBCommands"] = DBCommands;

        RefreshCommandsView();
    }

    protected void AddCommand(RadTreeNode DatabaseCommandRoot,string prefix)
    {
        //Code to Stop double addition of command in production that cannot be seen in Debug
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State["AddedCommandAlready"] != null)
        {
            if (DateTime.Now.AddSeconds(-3.0) < (DateTime) State["AddedCommandAlready"])
            {
                RefreshCommandsView();
                return;
            }
        }

         State["AddedCommandAlready"] = DateTime.Now;  //for next time
        //End code segment

        ArrayList DBCommands = (ArrayList) State["DBCommands"];
        if (DBCommands == null)
            DBCommands = new ArrayList();

        if (DBCommands.Count > 0)
        {
            Hashtable prev_CommandEntry = (Hashtable)DBCommands[DBCommands.Count - 1];
            if (prefix == null)
            {
                if (prev_CommandEntry["command"].ToString().ToLower() == "if")
                {
                    GoogleDocsConfigMessage.Text = "Only a 'Then' command can follow an 'If' condition.";
                    RefreshCommandsView();
                    return;
                }
            }
            else if (prefix == "Then")
            {
                if (prev_CommandEntry["command"].ToString().ToLower().StartsWith("then"))
                {
                    GoogleDocsConfigMessage.Text = "Only one 'Then' command can follow an 'If' condition.";
                    RefreshCommandsView();
                    return;
                }
                else if (prev_CommandEntry["command"].ToString().ToLower() != "if")
                {
                    GoogleDocsConfigMessage.Text = "A 'Then' command must follow an 'If' condition.";
                    RefreshCommandsView();
                    return;
                }
            }
            else if (prefix == "Else")
            {
                if (prev_CommandEntry["command"].ToString().ToLower().StartsWith("else"))
                {
                    GoogleDocsConfigMessage.Text = "Only one 'Else' command can follow an 'If' condition.";
                    RefreshCommandsView();
                    return;
                }
                else if (!prev_CommandEntry["command"].ToString().ToLower().StartsWith("then"))
                {
                    GoogleDocsConfigMessage.Text = "An 'Else' command must follow a 'Then' command.";
                    RefreshCommandsView();
                    return;
                }
            }
        }

        Hashtable newCommandEntry = new Hashtable();
        if(prefix == null)
            newCommandEntry["command"] = "Select From";
        else
            newCommandEntry["command"] = prefix + " Select From";

        newCommandEntry["table"] = GetFirstDatabaseTable();
        if (newCommandEntry["table"] == null)
        {
            if ( State["ManageDataType"].ToString() == "database")
            {
                DatabaseConfigMessage.Text = "You need to first initialize your database connection by clicking on 'Upload Database Info'";

            }
            else
            {
                GoogleDocsConfigMessage.Text = "You need to first initialize your Google Docs connection by clicking on 'Connect to Your Google Docs'";
            }
            return;
        }


        DBCommands.Add(newCommandEntry);

         State["DBCommands"] = DBCommands;

        RefreshCommandsView();
    }

    protected int GetFieldCount(Hashtable CommandEntry)
    {
        ArrayList DBFields = (ArrayList)CommandEntry["database_fields"];
        if (DBFields == null)
            return 0;
        return DBFields.Count;
    }
   
    protected RadTreeNode GetCommandRoot()
    {
        RadTreeNode DatabaseCommandRoot = null;
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State["ManageDataType"].ToString() == "database")
        {
            DatabaseCommandRoot = DatabaseCommandsView.Nodes[0];
        }
        else
        {
            DatabaseCommandRoot = SpreadsheetCommandsView.Nodes[0];
        }
        return DatabaseCommandRoot;
    }
    protected void RefreshCommandsView()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        ArrayList DBCommands = (ArrayList)State["DBCommands"];
        if (DBCommands == null)
            return;

        RadTreeNode DatabaseCommandRoot = CreateDatabaseRootNode(DatabaseEvents.SelectedValue);       

        //re add the previous controls
        int command_index = 0;
        Hashtable prev_CommandEntry = null;
        foreach (Hashtable CommandEntry in DBCommands)
        {
            int sub_command_index = 0;
            RadTreeNode command_node = new RadTreeNode();
            command_node.CssClass = "RadTreeView";
            command_node.Category = "command";
            command_node.PostBack = false;

            string orig_command = CommandEntry["command"].ToString();
            string command = orig_command.ToLower();
             State["SelectedSqlCommand"] = command;

            Control CommandControl = null;
            if (command == "if")
            {
                CommandControl = LoadControl("Controls/IfDeviceFieldThenDoCommand.ascx");               
            }
            else
            {
                CommandControl = LoadControl("Controls/DatabaseCommand.ascx");
                if (command.StartsWith("then "))
                {
                    command = command.Remove(0, 5);
                    ((RadComboBox)CommandControl.FindControl("command")).SelectedValue = command;
                    ((Label)CommandControl.FindControl("command_prefix")).Text = "Then ";
                }
                else if (command.StartsWith("else "))
                {
                    command = command.Remove(0, 5);
                    ((RadComboBox)CommandControl.FindControl("command")).SelectedValue = command;
                    ((Label)CommandControl.FindControl("command_prefix")).Text = "Else ";
                }
                else
                    ((RadComboBox)CommandControl.FindControl("command")).SelectedValue = command;

            }
            prev_CommandEntry = CommandEntry;

            command_node.Controls.Add(CommandControl);
            DatabaseCommandRoot.Nodes.Add(command_node);

            string field_control_type = null;

            if (command == "if")
            {
                if (CommandEntry["command_condition_device_field1"] != null && CommandEntry["command_condition_device_field1"].ToString().Length > 0)
                {
                    string command_condition_device_field1 = CommandEntry["command_condition_device_field1"].ToString();
                    ((RadComboBox)CommandControl.FindControl("command_condition_device_field1")).SelectedValue = command_condition_device_field1;
                }
                else
                {
                    CommandEntry["command_condition_device_field1"] = ((RadComboBox)CommandControl.FindControl("command_condition_device_field1")).SelectedValue;
                }
                if (CommandEntry["command_condition_operation"] != null && CommandEntry["command_condition_operation"].ToString().Length > 0)
                {
                    string command_condition_operation = CommandEntry["command_condition_operation"].ToString();
                    ((RadComboBox)CommandControl.FindControl("command_condition_operation")).SelectedValue = command_condition_operation;
                }
                else
                {
                    CommandEntry["command_condition_operation"] = ((RadComboBox)CommandControl.FindControl("command_condition_operation")).SelectedValue;
                }
                if (CommandEntry["command_condition_device_field2"] != null && CommandEntry["command_condition_device_field2"].ToString().Length > 0)
                {
                    string command_condition_device_field2 = CommandEntry["command_condition_device_field2"].ToString();
                    RadComboBox command_condition_device_field2_combo = ((RadComboBox)CommandControl.FindControl("command_condition_device_field2"));
                    command_condition_device_field2_combo.SelectedIndex = -1;
                    command_condition_device_field2_combo.Text = command_condition_device_field2;
                }
                else
                {
                    CommandEntry["command_condition_device_field2"] = ((RadComboBox)CommandControl.FindControl("command_condition_device_field2")).SelectedValue;
                }
                continue;
            }

            else if (command == "insert into")
            {
                ((ImageButton)CommandControl.FindControl("add_condition")).Visible = false;
                ((ImageButton)CommandControl.FindControl("add_order_by")).Visible = false;
                field_control_type = "from_phone_to_database";
            }
            else if (command == "update")
            {
                ((ImageButton)CommandControl.FindControl("add_order_by")).Visible = false;
                field_control_type = "from_phone_to_database";
            }
            else if (command == "select from")
            {
                field_control_type = "from_database_to_phone";
            }
            else if (command == "delete from")
            {
                ((ImageButton)CommandControl.FindControl("add_field")).Visible = false;
                ((ImageButton)CommandControl.FindControl("add_order_by")).Visible = false;
            }

            string table = CommandEntry["table"].ToString();
            //check if database_field is in combobox
            RadComboBox table_select = (RadComboBox)CommandControl.FindControl("table");
            ArrayList DBFields = null;
            if (table_select.Items.FindItemByValue(table) != null)
            {
                table_select.SelectedValue = table;
                DBFields = (ArrayList)CommandEntry["database_fields"];
            }
            else
            {
                table_select.Items.Insert(0, new RadComboBoxItem("Select ->", "no_value"));
                table_select.SelectedIndex = 0;
                GoogleDocsConfigMessage.Text = "The saved table name is no longer valid. All fields have been cleared.";
                CommandEntry["conditions"] = null;
                CommandEntry["order_by"] = null;
            }
             if (DBFields != null && field_control_type != null)
            {
                foreach (Hashtable FieldEntry in DBFields)
                {
                    RadTreeNode field_node = new RadTreeNode();
                    field_node.CssClass = "RadTreeView";
                    field_node.Category = "field";
                    field_node.PostBack = false;
                     State["SelectedDatabaseTable"] = table;
                    string control_file = (field_control_type == "from_database_to_phone") ? "DatabaseToDeviceField.ascx" : "DeviceToDatabaseField.ascx";
                    Control FieldControl = LoadControl("Controls/" +control_file);
                    if ( State["SpreadsheetError"] != null)
                    {
                        GoogleDocsConfigMessage.Text =  State["SpreadsheetError"].ToString();
                        return;
                    }
                    field_node.Controls.Add(FieldControl);
                    command_node.Nodes.Add(field_node);

                    if (FieldEntry["database_field"] != null && FieldEntry["database_field"].ToString().Length > 0)
                    {
                        string database_field = FieldEntry["database_field"].ToString();
                        //check if database_field is in combobox
                        RadComboBox database_field_select = (RadComboBox)FieldControl.FindControl("database_field");
                        if (database_field_select.Items.FindItemByValue(database_field) != null)
                            database_field_select.SelectedValue = database_field;
                        else //add select item
                        {
                            database_field_select.Items.Insert(0,new RadComboBoxItem("Select ->","no_value"));
                            database_field_select.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        FieldEntry["database_field"] = ((RadComboBox)FieldControl.FindControl("database_field")).SelectedValue;
                    }
                    if (FieldEntry["device_field"] != null && FieldEntry["device_field"].ToString().Length > 0)
                    {
                        string phone_field = FieldEntry["device_field"].ToString();
                        RadComboBox phone_field_combo = (RadComboBox)FieldControl.FindControl("device_field");
                        phone_field_combo.SelectedIndex = -1;
                        phone_field_combo.Text = phone_field;
                    }
                    else
                    {
                        string phone_field_text = ((RadComboBox)FieldControl.FindControl("device_field")).Text;
                        if (phone_field_text.Length > 0)
                            FieldEntry["device_field"] = phone_field_text;
                        else
                            FieldEntry["device_field"] = ((RadComboBox)FieldControl.FindControl("device_field")).SelectedValue;
                    }
                    sub_command_index++;
                }
            }
            ArrayList DBWhere = (ArrayList)CommandEntry["conditions"];
            if (DBWhere != null)
            {
                int i_condition = 0;
                // Hashtable uniqueConditionIDs = new Hashtable();
                foreach (Hashtable ConditionEntry in DBWhere)
                {
                    if (i_condition == 0)
                         State["IsFirstCommandCondition"] = true;
                    else
                         State["IsFirstCommandCondition"] = false;

                    RadTreeNode where_node = new RadTreeNode();
                    where_node.CssClass = "RadTreeView";
                    where_node.Category = "condition";
                    where_node.PostBack = false;
                     State["SelectedDatabaseTable"] = table;
                    Control WhereControl = LoadControl("Controls/DatabaseWhere.ascx");
                    if ( State["SpreadsheetError"] != null)
                    {
                        GoogleDocsConfigMessage.Text =  State["SpreadsheetError"].ToString();
                        return;
                    }

                    where_node.Controls.Add(WhereControl);

                    command_node.Nodes.Add(where_node);

                    if (ConditionEntry["condition_operation"] != null && ConditionEntry["condition_operation"].ToString().Length > 0)
                    {
                        string condition_operation = ConditionEntry["condition_operation"].ToString();
                        ((RadComboBox)WhereControl.FindControl("condition_operation")).SelectedValue = condition_operation;
                    }
                    else
                    {
                        ConditionEntry["condition_operation"] = ((RadComboBox)WhereControl.FindControl("condition_operation")).SelectedValue;
                    }
                    if (ConditionEntry["condition_1st_field"] != null && ConditionEntry["condition_1st_field"].ToString().Length > 0)
                    {
                        string condition_1st_field = ConditionEntry["condition_1st_field"].ToString();
                        //check if database field is in combobox
                        RadComboBox condition_1st_field_select = (RadComboBox)WhereControl.FindControl("condition_1st_field");
                        if (condition_1st_field_select.Items.FindItemByValue(condition_1st_field) != null)
                            condition_1st_field_select.SelectedValue = condition_1st_field;
                        else //add select item
                        {
                            condition_1st_field_select.Items.Insert(0,new RadComboBoxItem("Select ->","no_value"));
                            condition_1st_field_select.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        ConditionEntry["condition_1st_field"] = ((RadComboBox)WhereControl.FindControl("condition_1st_field")).SelectedValue;
                    }
                    if (ConditionEntry["field_operation"] != null && ConditionEntry["field_operation"].ToString().Length > 0)
                    {
                        string field_operation = ConditionEntry["field_operation"].ToString();
                        ((RadComboBox)WhereControl.FindControl("field_operation")).SelectedValue = field_operation;
                    }
                    else
                    {
                        ConditionEntry["field_operation"] = ((RadComboBox)WhereControl.FindControl("field_operation")).SelectedValue;
                    }
                    if (ConditionEntry["condition_2nd_field"] != null && ConditionEntry["condition_2nd_field"].ToString().Length > 0)
                    {
                        string condition_2nd_field = ConditionEntry["condition_2nd_field"].ToString();
                        RadComboBox condition_2nd_field_combo = (RadComboBox)WhereControl.FindControl("condition_2nd_field");
                        condition_2nd_field_combo.SelectedIndex = -1;
                        condition_2nd_field_combo.Text = condition_2nd_field;
                    }
                    else
                    {
                        ConditionEntry["condition_2nd_field"] = ((RadComboBox)WhereControl.FindControl("condition_2nd_field")).Text;
                    }
                    i_condition++;
                    sub_command_index++;
                }
            }
            Hashtable DBOrderBy = (Hashtable)CommandEntry["order_by"];
            if (DBOrderBy != null)
            {
                RadTreeNode orderBy_node = new RadTreeNode();
                orderBy_node.CssClass = "RadTreeView";
                orderBy_node.Category = "order_by";
                orderBy_node.PostBack = false;
                 State["SelectedDatabaseTable"] = table;
                Control OrderByControl = LoadControl("Controls/DatabaseOrderBy.ascx");
                if ( State["SpreadsheetError"] != null)
                {
                    GoogleDocsConfigMessage.Text =  State["SpreadsheetError"].ToString();
                    return;
                }

                orderBy_node.Controls.Add(OrderByControl);
                command_node.Nodes.Add(orderBy_node);

                if (DBOrderBy["sort_field"] != null && DBOrderBy["sort_field"].ToString().Length > 0)
                {
                    string sort_field = DBOrderBy["sort_field"].ToString();
                    //check if database_field is in combobox
                    RadComboBox sort_field_select = (RadComboBox)OrderByControl.FindControl("sort_field");
                    if (sort_field_select.Items.FindItemByValue(sort_field) != null)
                        sort_field_select.SelectedValue = sort_field;
                    else //add select item
                    {
                        sort_field_select.Items.Insert(0, new RadComboBoxItem("Select ->", "no_value"));
                        sort_field_select.SelectedIndex = 0;
                    }
                }
                else
                {
                    DBOrderBy["sort_field"] = ((RadComboBox)OrderByControl.FindControl("sort_field")).SelectedValue;
                }
                if (DBOrderBy["sort_direction"] != null && DBOrderBy["sort_direction"].ToString().Length > 0)
                {
                    string sort_direction = DBOrderBy["sort_direction"].ToString();
                    ((RadComboBox)OrderByControl.FindControl("sort_direction")).SelectedValue = sort_direction;
                }
                else
                {
                    DBOrderBy["sort_direction"] = ((RadComboBox)OrderByControl.FindControl("sort_direction")).SelectedValue;
                }
                sub_command_index++;
            }

            command_node.ExpandChildNodes();
            command_index++;
        }
        if ( State["ManageDataType"].ToString() == "database")        
            DatabaseCommandsView.ExpandAllNodes();
        
        else        
            SpreadsheetCommandsView.ExpandAllNodes();
    }
    protected void condition_2nd_field_ItemsRequested(object o, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ArrayList fields = (ArrayList)State["ConditionPhoneFields"];
        RadComboBox condition_2nd_field = null;
        foreach (string name in fields)
        {
            condition_2nd_field.Items.Add(new Telerik.Web.UI.RadComboBoxItem(name, name));
        }
    }
    protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        string account_type = util.GetAccountType(State);
        util.Logout(State);
        if (account_type != null && account_type.Contains("google_apps"))
            Response.Redirect("LogoutForGoogleApps.aspx", false);
        else
            Response.Redirect("Default.aspx", false);

    }
}