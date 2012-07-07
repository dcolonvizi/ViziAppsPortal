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
using Aspose.Excel;
using System.Xml;
using System.Drawing;
using Telerik.Web.UI;
using System.Net;
using System.IO;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.xml;
using iTextSharp.text.html;
using HtmlAgilityPack;

public partial class ManageData_GoogleSpreadsheetOperations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { SessionTimeOut.Text = "../../Default.aspx"; return; }

        if (State["SelectedAppType"] != null)
        {
            SelectedAppType.Value = State["SelectedAppType"].ToString();

            int thumbWidth = Constants.IPHONE_DISPLAY_WIDTH / 2;
            int thumbHeight = Convert.ToInt32(Constants.IPHONE_SCROLL_HEIGHT_S) / 2;

            if (State["SelectedDeviceType"] == null)
            {
                XScaleFactor.Value = (thumbWidth / Constants.IPHONE_SCROLL_WIDTH_D).ToString();
                YScaleFactor.Value = (thumbHeight / Constants.IPHONE_SCROLL_HEIGHT_D).ToString();
            }
            else
            {
                switch (State["SelectedDeviceType"].ToString())
                {
                    case Constants.IPHONE:
                    default:
                        XScaleFactor.Value = (thumbWidth / Constants.IPHONE_SCROLL_WIDTH_D).ToString();
                        YScaleFactor.Value = (thumbHeight / Constants.IPHONE_SCROLL_HEIGHT_D).ToString();
                        break;
                    case Constants.ANDROID_PHONE:
                        XScaleFactor.Value = (thumbWidth / Constants.ANDROID_PHONE_SCROLL_WIDTH_D).ToString();
                        YScaleFactor.Value = (thumbHeight / Constants.ANDROID_PHONE_SCROLL_HEIGHT_D).ToString();
                        break;
                    case Constants.IPAD:
                        XScaleFactor.Value = (thumbWidth / Constants.IPAD_SCROLL_WIDTH_D).ToString();
                        YScaleFactor.Value = (thumbHeight / Constants.IPAD_SCROLL_HEIGHT_D).ToString();
                        break;
                    case Constants.ANDROID_TABLET:
                        XScaleFactor.Value = (thumbWidth / Constants.ANDROID_TABLET_SCROLL_WIDTH_D).ToString();
                        YScaleFactor.Value = (thumbHeight / Constants.ANDROID_TABLET_SCROLL_HEIGHT_D).ToString();
                        break;
                }
            }
        }
        try
        {
            if (IsPostBack)
                Message.Text = " ";
            else
            {
                LoadCurrentStoryBoardPage();
                DataSources DS = new DataSources();
                State["DataSourceDatabaseTables"] = DS.GetDataSourceDatabaseTables(State);
                BuildDatabaseTrees();
             }
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
        }
    }
    protected void BuildDatabaseTrees()
    {
        InitDatabaseCommandsView(CreateSpreadsheetRootNode());
        SpreadsheetCommandsView.ExpandAllNodes();
    }
    private RadTreeNode CreateSpreadsheetRootNode()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        SpreadsheetCommandsView.Nodes.Clear();
        
        RadTreeNode DatabaseCommandRoot = null;
        if (State["DataSourceEventField"] == null)
            DatabaseCommandRoot = new RadTreeNode("Operations that will run before " + State["SelectedAppPage"].ToString() + " is shown");
        else
            DatabaseCommandRoot = new RadTreeNode("Operations that will run after " + State["DataSourceEventField"].ToString() + " is tapped");

        DatabaseCommandRoot.CssClass = "RadTreeView";
        DatabaseCommandRoot.Category = "event";

        SpreadsheetCommandsView.Nodes.Add(DatabaseCommandRoot);
        
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

    protected void InitDatabaseCommandsView(RadTreeNode DatabaseCommandRoot)
    {
        XmlUtil x_util = new XmlUtil();
        Util util = new Util();
        DataSources DS = new DataSources();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode page_name = doc.SelectSingleNode("//pages/page/name[.='" + State["SelectedAppPage"].ToString() + "']");
        XmlNode data_sources = page_name.ParentNode.SelectSingleNode("data_sources");
        if (data_sources == null)
        {
            return;
        }
        XmlNode event_node = null;
        if (State["DataSourceEventField"] == null)
        {
            XmlNodeList data_source_id_list = data_sources.SelectNodes("data_source/data_source_id[.='" + State["DataSourceID"].ToString() + "']");
            foreach (XmlNode node in data_source_id_list)
            {
                XmlNode possible = node.ParentNode.SelectSingleNode("event/data_source_event_type[.='page']");
                if (possible != null)
                {
                    event_node = possible.ParentNode;
                    break;
                }
            }
            if (event_node == null)
                return;
        }
        else
        {
            event_node = data_sources.SelectSingleNode("data_source/event/data_source_event_field[.='" + State["DataSourceEventField"].ToString() + "']").ParentNode;
        }

        XmlNode sql_commands = event_node.SelectSingleNode("data_source_operations/sql_commands");
        if (sql_commands == null)
            return;

        ArrayList DBCommands = new ArrayList();

        XmlNodeList sql_command_list = sql_commands.SelectNodes("sql_command");

        foreach (XmlNode sql_command in sql_command_list)
        {
            if (sql_command.InnerXml.Length == 0)
                continue;
            DBCommands.Add(DS.ParseSqlCommand(doc, sql_command));
        }

        State["DBCommands"] = DBCommands;

        RefreshCommandsView();
    }
    protected void AddDeviceAction_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { SessionTimeOut.Text = "../../Default.aspx"; return; }
        AddGoToPage(GetCommandRoot(), null);
    }
    protected void AddIfCondition_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { SessionTimeOut.Text = "../../Default.aspx"; return; }
        AddCommandCondition(GetCommandRoot());
    }
    protected void AddQuery_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { SessionTimeOut.Text = "../../Default.aspx"; return; }
        AddCommand(GetCommandRoot(), null);
    }
    protected void ResetDatabaseConfig_Click(object sender, EventArgs e)
    {
        XmlUtil x_util = new XmlUtil();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { SessionTimeOut.Text = "../../Default.aspx"; return; }

        Util util = new Util();
        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode page_name = doc.SelectSingleNode("//pages/page/name[.='" + State["SelectedAppPage"].ToString() + "']");
        XmlNode data_sources = page_name.ParentNode.SelectSingleNode("data_sources");
        if (data_sources == null)
        {
            Message.Text = "There was an internal error with your design. Please notify ViziApps: " + State["TechSupportEmail"].ToString();
            return;
        }
        XmlNodeList list = data_sources.SelectNodes("data_source");
        XmlNode data_source = list[(int)State["PageDataSourceIndex"]];
        XmlNode event_node = null;
        if (State["DataSourceEventField"] == null) //page type
        {
            XmlNode temp_data_source_event_type = data_source.SelectSingleNode("event/data_source_event_type[.='page']");
            if (temp_data_source_event_type != null)
                event_node = temp_data_source_event_type.ParentNode;
        }
        else //field type
        {
            XmlNode temp_data_source_event_field = data_source.SelectSingleNode("event/data_source_event_field[.='" + State["DataSourceEventField"].ToString() + "']");
            if (temp_data_source_event_field != null)
                event_node = temp_data_source_event_field.ParentNode;
        }
        if(event_node != null)
         data_source.RemoveChild(event_node);

        State["AppXmlDoc"] = doc;
        util.UpdateStagingAppXml(State);

        State["DBCommands"] = null;

        Message.Text = "All Query Commands for this event field have been deleted.";
        RefreshCommandsView();
        Message.ForeColor = Color.Maroon;
        SpreadsheetCommandsView.Nodes.Clear();
        CreateSpreadsheetRootNode();
    }
    protected void SaveDatabaseConfig_Click(object sender, EventArgs e)
    {
        XmlUtil x_util = new XmlUtil();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { SessionTimeOut.Text = "../../Default.aspx"; return; }

        Util util = new Util();
        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode page_name = doc.SelectSingleNode("//pages/page/name[.='" + State["SelectedAppPage"].ToString() + "']");
        XmlNode data_sources = page_name.ParentNode.SelectSingleNode("data_sources");
        if (data_sources == null)
        {
            Message.Text = "There was an internal error with your design. Please notify ViziApps: " + State["TechSupportEmail"].ToString();
            return;
        }
        XmlNodeList list = data_sources.SelectNodes("data_source");
        XmlNode data_source = list[(int)State["PageDataSourceIndex"] ];
        XmlNode event_node = null;
        if (State["DataSourceEventField"] == null) //page type
        {
            XmlNode temp_data_source_event_type = data_source.SelectSingleNode("event/data_source_event_type[.='page']");
            if (temp_data_source_event_type == null)
            {
                event_node = x_util.CreateNode(doc, data_source, "event");
                x_util.CreateNode(doc, event_node, "data_source_event_type", "page");
            }
            else
                event_node = temp_data_source_event_type.ParentNode;
        }
        else //field type
        {
            XmlNode temp_data_source_event_field = data_source.SelectSingleNode("event/data_source_event_field[.='" + State["DataSourceEventField"].ToString() + "']");
            if (temp_data_source_event_field == null)
            {
                event_node = x_util.CreateNode(doc, data_source, "event");
                x_util.CreateNode(doc, event_node, "data_source_event_type", "field");
                x_util.CreateNode(doc, event_node, "data_source_event_field", State["DataSourceEventField"].ToString());
            }
            else
            {
                event_node = temp_data_source_event_field.ParentNode;
            }
        }          

        XmlNode data_source_operations = event_node.SelectSingleNode("data_source_operations");
        if (data_source_operations != null)
            event_node.RemoveChild(data_source_operations);

        data_source_operations = x_util.CreateNode(doc, event_node, "data_source_operations");

        XmlNode sql_commands = x_util.CreateNode(doc, data_source_operations, "sql_commands");

        ArrayList DBCommands = (ArrayList)State["DBCommands"];
        if (DBCommands == null)
        {
            Message.Text = "There are no query Commands to save.";
            Message.ForeColor = Color.Maroon;
            return;
        }

        foreach (Hashtable CommandEntry in DBCommands)
        {
            XmlNode sql_command = x_util.CreateNode(doc, sql_commands, "sql_command");
            string command = CommandEntry["command"].ToString().ToLower();
            x_util.CreateNode(doc, sql_command, "command", command);
            if (command == "if")
            {
                x_util.CreateNode(doc, sql_command, "command_condition_phone_field1", CommandEntry["command_condition_phone_field1"].ToString());
                x_util.CreateNode(doc, sql_command, "command_condition_operation", CommandEntry["command_condition_operation"].ToString());
                x_util.CreateNode(doc, sql_command, "command_condition_phone_field2", CommandEntry["command_condition_phone_field2"].ToString());
            }
            else
            {
                if (command.EndsWith("go to page"))
                {
                    x_util.CreateNode(doc, sql_command, "page", CommandEntry["page"].ToString());
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
                            else
                                x_util.CreateNode(doc, field_item, "database_field", GetFirstDatabaseField(CommandEntry["table"].ToString()));

                            if (FieldEntry["phone_field"] != null && FieldEntry["phone_field"].ToString().Length > 0)
                            {
                                x_util.CreateNode(doc, field_item, "phone_field", FieldEntry["phone_field"].ToString());
                            }
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
        Message.Text = "Query Commands have been saved.";
        Message.ForeColor = Color.Maroon;
    }
    protected void RefreshCommandsView()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        ArrayList DBCommands = (ArrayList)State["DBCommands"];
        RadTreeNode DatabaseCommandRoot = CreateSpreadsheetRootNode();
        
        if (DBCommands == null)
            return;

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
            string selected_table = null;
            if (CommandEntry["table"] != null && CommandEntry["table"].ToString().Length > 0)
            {
                State["SelectedDatabaseTable"] = selected_table = CommandEntry["table"].ToString();
                DataSources DS = new DataSources();
                State["DataSourceDatabaseTableFields"] = DS.GetDataSourceDatabaseTableFields(State);
                if (State["DataSourceDatabaseTableFields"] == null)
                {
                    ResetDatabaseConfig_Click(null, null);
                    Message.Text = State["SpreadsheetError"].ToString();
                    State["SpreadsheetError"] = null;
                    State["DBCommands"] = null;
                    State["SelectedDatabaseTable"] = null;
                    State["SelectedSqlCommand"] = null;
                    return ;
                }
            }
 
            if (command == "if")
            {
                CommandControl = LoadControl("Controls/IfPhoneFieldThenDoCommand.ascx");
            }
            else if(command.EndsWith("go to page"))
            {
                string selected_page = null;
                if (CommandEntry["page"] != null && CommandEntry["page"].ToString().Length > 0)
                {
                    State["SelectedGoToPage"] = selected_page = CommandEntry["page"].ToString();
                }
                CommandControl = LoadControl("Controls/GoToPage.ascx", selected_page);
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
            else
            {
                CommandControl = LoadControl("Controls/DatabaseCommand.ascx", selected_table);
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
                if (CommandEntry["command_condition_phone_field1"] != null && CommandEntry["command_condition_phone_field1"].ToString().Length > 0)
                {
                    string command_condition_phone_field1 = CommandEntry["command_condition_phone_field1"].ToString();
                    ((HtmlInputText)CommandControl.FindControl("command_condition_phone_field1")).Value = command_condition_phone_field1;
                }
                else
                {
                    CommandEntry["command_condition_phone_field1"] = ((HtmlInputText)CommandControl.FindControl("command_condition_phone_field1")).Value;
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
                if (CommandEntry["command_condition_phone_field2"] != null && CommandEntry["command_condition_phone_field2"].ToString().Length > 0)
                {
                    string command_condition_phone_field2 = CommandEntry["command_condition_phone_field2"].ToString();
                    HtmlInputText command_condition_phone_field2_input = ((HtmlInputText)CommandControl.FindControl("command_condition_phone_field2"));
                    command_condition_phone_field2_input.Value = command_condition_phone_field2;
                }
                else
                {
                    CommandEntry["command_condition_phone_field2"] = ((HtmlInputText)CommandControl.FindControl("command_condition_phone_field2")).Value;
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

             ArrayList DBFields = (ArrayList)CommandEntry["database_fields"];
            if (DBFields != null && field_control_type != null)
            {
                foreach (Hashtable FieldEntry in DBFields)
                {
                    RadTreeNode field_node = new RadTreeNode();
                    field_node.CssClass = "RadTreeView";
                    field_node.Category = "field";
                    field_node.PostBack = false;
                    State["SelectedDatabaseTable"] = selected_table;
                    string control_file = (field_control_type == "from_database_to_phone") ? "DatabaseToPhoneField.ascx" : "PhoneToDatabaseField.ascx";
                    string selected_database_field = null;
                    if (FieldEntry["database_field"] != null && FieldEntry["database_field"].ToString().Length > 0)
                    {
                        selected_database_field = FieldEntry["database_field"].ToString();
                    }
                    else
                    {
                        ArrayList DataSourceDatabaseTableFields = (ArrayList)State["DataSourceDatabaseTableFields"];
                        selected_database_field = DataSourceDatabaseTableFields[0].ToString();
                    }

                    Control FieldControl = LoadControl("Controls/" + control_file, selected_database_field);
                    if (State["SpreadsheetError"] != null)
                    {
                        ResetDatabaseConfig_Click(null, null);
                        Message.Text = State["SpreadsheetError"].ToString();
                        State["SpreadsheetError"] = null;
                        State["DBCommands"] = null;
                        State["SelectedDatabaseTable"] = null;
                        State["SelectedSqlCommand"] = null;
                        return;
                    }
                    field_node.Controls.Add(FieldControl);
                    command_node.Nodes.Add(field_node);

                    if (FieldEntry["phone_field"] != null && FieldEntry["phone_field"].ToString().Length > 0)
                    {
                        string phone_field = FieldEntry["phone_field"].ToString();
                        HtmlInputText phone_field_input = (HtmlInputText)FieldControl.FindControl("phone_field");
                        phone_field_input.Value = phone_field;
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
                    State["SelectedDatabaseTable"] = selected_table;
                    string selected_condition_1st_field = null;
                    if (ConditionEntry["condition_1st_field"] != null && ConditionEntry["condition_1st_field"].ToString().Length > 0)
                    {
                        selected_condition_1st_field = ConditionEntry["condition_1st_field"].ToString();
                    }
                    else
                    {
                        ArrayList DataSourceDatabaseTableFields = (ArrayList)State["DataSourceDatabaseTableFields"];
                        selected_condition_1st_field = DataSourceDatabaseTableFields[0].ToString();
                    }
                    Control WhereControl = LoadControl("Controls/SpreadsheetWhere.ascx", selected_condition_1st_field);

                    if (State["SpreadsheetError"] != null)
                    {
                        ResetDatabaseConfig_Click(null, null);
                        Message.Text = State["SpreadsheetError"].ToString();
                        State["SpreadsheetError"] = null;
                        State["DBCommands"] = null;
                        State["SelectedDatabaseTable"] = null;
                        State["SelectedSqlCommand"] = null;
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
                        HtmlInputText condition_2nd_field_input = (HtmlInputText)WhereControl.FindControl("condition_2nd_field");
                        condition_2nd_field_input.Value = condition_2nd_field;
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
                State["SelectedDatabaseTable"] = selected_table;
                string selected_sort_field = null;
                if (DBOrderBy["sort_field"] != null && DBOrderBy["sort_field"].ToString().Length > 0)
                {
                    selected_sort_field = DBOrderBy["sort_field"].ToString();
                }
                else
                {
                    ArrayList DataSourceDatabaseTableFields = (ArrayList)State["DataSourceDatabaseTableFields"];
                    selected_sort_field = DataSourceDatabaseTableFields[0].ToString();
                }

                Control OrderByControl = LoadControl("Controls/DatabaseOrderBy.ascx", selected_sort_field);
                if (State["SpreadsheetError"] != null)
                {
                    ResetDatabaseConfig_Click(null, null);
                    Message.Text = State["SpreadsheetError"].ToString();
                    State["SpreadsheetError"] = null;
                    State["DBCommands"] = null;
                    State["SelectedDatabaseTable"] = null;
                    State["SelectedSqlCommand"] = null;
                    return;
                }

                orderBy_node.Controls.Add(OrderByControl);
                command_node.Nodes.Add(orderBy_node);

                if (DBOrderBy["sort_direction"] != null && DBOrderBy["sort_direction"].ToString().Length > 0)
                {
                    string sort_direction = DBOrderBy["sort_direction"].ToString();
                    ((RadComboBox)OrderByControl.FindControl("sort_direction")).SelectedValue = sort_direction;
                }
                sub_command_index++;
            }

            command_node.ExpandChildNodes();
            command_index++;
        } 
        SpreadsheetCommandsView.ExpandAllNodes();
    }
    private void LoadCurrentStoryBoardPage()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        try
        {
            State["PageViewAppID"] = util.GetAppID(State);

            XmlUtil x_util = new XmlUtil();
            XmlDocument doc = x_util.GetStagingAppXml(State);
            if (doc == null)
                return;
            RadTreeNode page_node = new RadTreeNode();
            page_node.CssClass = "RadTreeView";
            page_node.Category = "page";
            page_node.PostBack = false;
            PageTreeView.Nodes.Clear();
            Control PageControl = LoadControl("Controls/PageView.ascx", State["SelectedAppPage"].ToString());
            page_node.Controls.Add(PageControl);
            PageTreeView.Nodes.Add(page_node);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }

    private void LoadStoryBoard()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        try
        {
            State["PageViewAppID"] = util.GetAppID(State);

            XmlUtil x_util = new XmlUtil();
            XmlDocument doc = x_util.GetStagingAppXml(State);
            if (doc == null)
                return;

            PageTreeView.Nodes.Clear();
            string[] pages = x_util.GetAppPageNames(State, State["SelectedApp"].ToString());
            for (int i = 0; i < pages.Length; i++)
            {
                RadTreeNode page_node = new RadTreeNode();
                page_node.CssClass = "RadTreeView";
                page_node.Category = "page";
                page_node.PostBack = false;
                Control PageControl = LoadControl("Controls/PageView.ascx", pages[i]);
                page_node.Controls.Add(PageControl);
                PageTreeView.Nodes.Add(page_node);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
    private UserControl LoadControl(string UserControlPath, params object[] constructorParameters)
    {
        try
        {
            System.Collections.Generic.List<Type> constParamTypes = new System.Collections.Generic.List<Type>();
            foreach (object constParam in constructorParameters)
            {
                constParamTypes.Add(constParam.GetType());
            }

            UserControl ctl = Page.LoadControl(UserControlPath) as UserControl;

            // Find the relevant constructor
            ConstructorInfo constructor = ctl.GetType().BaseType.GetConstructor(constParamTypes.ToArray());

            //And then call the relevant constructor
            if (constructor == null)
            {
                throw new MemberAccessException("The requested constructor was not found on : " + ctl.GetType().BaseType.ToString());
            }
            else
            {
                try
                {
                    constructor.Invoke(ctl, constructorParameters);
                }
                catch (Exception ex)
                {
                    StringBuilder error = new StringBuilder("LoadControl in StoryBoard could not construct: ");
                    foreach (object constParam in constructorParameters)
                    {
                        error.Append(constParam.ToString());
                    }
                    throw new Exception(error.ToString() + " - " + ex.Message + ": " + ex.StackTrace);
                }
            }

            // Finally return the fully initialized UC
            return ctl;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
    protected void DBTreeClick_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { SessionTimeOut.Text = "../../Default.aspx"; return; }

        ArrayList DBCommands = (ArrayList)State["DBCommands"];
        Hashtable CommandEntry = null;
        string[] split = DBTreeInfo.Text.Split(";".ToCharArray());
        string info_text = DBTreeInfo.Text;
 
        //command_condition
        if (info_text.StartsWith("command_condition_phone_field1"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            CommandEntry["command_condition_phone_field1"] = split[2];
        }
        else if (info_text.StartsWith("command_condition_operation"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            CommandEntry["command_condition_operation"] = split[2];
        }
        else if (info_text.StartsWith("command_condition_phone_field2"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            CommandEntry["command_condition_phone_field2"] = split[2];
        }
        else if (info_text.StartsWith("delete_command_condition"))//this is order dependent because 2 items contain "delete_command"
        {
            DBCommands.RemoveAt(Convert.ToInt32(split[1]));
        }
        else if (info_text.StartsWith("command_condition_add_then_sql"))
        {
            AddCommand(GetCommandRoot(), "Then");
        }
        else if (info_text.StartsWith("command_condition_add_else_sql"))
        {
            AddCommand(GetCommandRoot(), "Else");
        }
        else if (info_text.StartsWith("command_condition_add_then_action"))
        {
            AddGoToPage(GetCommandRoot(), "Then");
        }
        else if (info_text.StartsWith("command_condition_add_else_action"))
        {
            AddGoToPage(GetCommandRoot(), "Else");
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
        else if (info_text.StartsWith("page"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            CommandEntry["page"] = split[2];
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
            Hashtable FieldEntry = (Hashtable)DBFields[0];
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
        else if (info_text.StartsWith("phone_field"))
        {
            CommandEntry = (Hashtable)DBCommands[Convert.ToInt32(split[1])];
            ArrayList DBFields = (ArrayList)CommandEntry["database_fields"];
            if (DBFields != null)
            {
                Hashtable FieldEntry = (Hashtable)DBFields[Convert.ToInt32(split[2])];
                FieldEntry["phone_field"] = split[3];
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
                Message.Text = "A command condition cannot follow another command condition.";
                Message.ForeColor = Color.Maroon;
                RefreshCommandsView();
                return;
            }
        }

        Hashtable newCommandEntry = new Hashtable();
        newCommandEntry["command"] = "If";
        newCommandEntry["command_condition_phone_field1"] = null;
        newCommandEntry["command_condition_operation"] = null;
        newCommandEntry["command_condition_phone_field2"] = null;


        DBCommands.Add(newCommandEntry);
        State["DBCommands"] = DBCommands;

        RefreshCommandsView();
    }
    protected void AddGoToPage(RadTreeNode DatabaseCommandRoot, string prefix)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        /*//Code to Stop double addition of command in production that cannot be seen in Debug
        if (State["AddedDeviceActionAlready"] != null)
        {
            if (DateTime.Now.AddSeconds(-3.0) < (DateTime)State["AddedDeviceActionAlready"])
            {
                RefreshCommandsView();
                return;
            }
        }

        State["AddedDeviceActionAlready"] = DateTime.Now;  //for next time
        //End code segment
        */
        ArrayList DBCommands = (ArrayList)State["DBCommands"];
        if (DBCommands == null)
            DBCommands = new ArrayList();

        if (DBCommands.Count > 0)
        {
            Hashtable prev_CommandEntry = (Hashtable)DBCommands[DBCommands.Count - 1];
            if (prefix == null)
            {
                if (prev_CommandEntry["command"].ToString().ToLower() == "if")
                {
                    Message.Text = "Only a 'Then' command can follow an 'If' condition.";
                    Message.ForeColor = Color.Maroon;
                    RefreshCommandsView();
                    return;
                }
            }
            else if (prefix == "Then")
            {
                if (prev_CommandEntry["command"].ToString().ToLower().StartsWith("then"))
                {
                    Message.Text = "Only one 'Then' command can follow an 'If' condition.";
                    Message.ForeColor = Color.Maroon;
                    RefreshCommandsView();
                    return;
                }
                else if (prev_CommandEntry["command"].ToString().ToLower() != "if")
                {
                    Message.Text = "A 'Then' command must follow an 'If' condition.";
                    Message.ForeColor = Color.Maroon;
                    RefreshCommandsView();
                    return;
                }
            }
            else if (prefix == "Else")
            {
                if (prev_CommandEntry["command"].ToString().ToLower().StartsWith("else"))
                {
                    Message.Text = "Only one 'Else' command can follow an 'If' condition.";
                    Message.ForeColor = Color.Maroon;
                    RefreshCommandsView();
                    return;
                }
                else if (!prev_CommandEntry["command"].ToString().ToLower().StartsWith("then"))
                {
                    Message.Text = "An 'Else' command must follow a 'Then' command.";
                    Message.ForeColor = Color.Maroon;
                    RefreshCommandsView();
                    return;
                }
            }
        }

        Hashtable newCommandEntry = new Hashtable();
        if (prefix == null)
            newCommandEntry["command"] = "Go To Page";
        else
            newCommandEntry["command"] = prefix + " Go To Page";

        XmlUtil x_util = new XmlUtil();
        string[] pages = x_util.GetAppPageNames(State, State["SelectedApp"].ToString());
        newCommandEntry["page"] = pages[0];
        if (newCommandEntry["page"] == null)
        {
            Message.Text = "You need to first initialize your Google Docs connection by clicking on 'Connect to Your Google Docs'";
            Message.ForeColor = Color.Maroon;
            return;
        }


        DBCommands.Add(newCommandEntry);

        State["DBCommands"] = DBCommands;

        RefreshCommandsView();
    }

    protected void AddCommand(RadTreeNode DatabaseCommandRoot, string prefix)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        /*//Code to Stop double addition of command in production that cannot be seen in Debug
       
       if (State["AddedCommandAlready"] != null)
       {
           if (DateTime.Now.AddSeconds(-3.0) < (DateTime)State["AddedCommandAlready"])
           {
               RefreshCommandsView();
               return;
           }
       }

       State["AddedCommandAlready"] = DateTime.Now;  //for next time        
        //End code segment
         * */

        ArrayList DBCommands = (ArrayList)State["DBCommands"];
        if (DBCommands == null)
            DBCommands = new ArrayList();

        if (DBCommands.Count > 0)
        {
            Hashtable prev_CommandEntry = (Hashtable)DBCommands[DBCommands.Count - 1];
            if (prefix == null)
            {
                if (prev_CommandEntry["command"].ToString().ToLower() == "if")
                {
                    Message.Text = "Only a 'Then' command can follow an 'If' condition.";
                    Message.ForeColor = Color.Maroon;
                    RefreshCommandsView();
                    return;
                }
            }
            else if (prefix == "Then")
            {
                if (prev_CommandEntry["command"].ToString().ToLower().StartsWith("then"))
                {
                    Message.Text = "Only one 'Then' command can follow an 'If' condition.";
                    Message.ForeColor = Color.Maroon;
                    RefreshCommandsView();
                    return;
                }
                else if (prev_CommandEntry["command"].ToString().ToLower() != "if")
                {
                    Message.Text = "A 'Then' command must follow an 'If' condition.";
                    Message.ForeColor = Color.Maroon;
                    RefreshCommandsView();
                    return;
                }
            }
            else if (prefix == "Else")
            {
                if (prev_CommandEntry["command"].ToString().ToLower().StartsWith("else"))
                {
                    Message.Text = "Only one 'Else' command can follow an 'If' condition.";
                    Message.ForeColor = Color.Maroon;
                    RefreshCommandsView();
                    return;
                }
                else if (!prev_CommandEntry["command"].ToString().ToLower().StartsWith("then"))
                {
                    Message.Text = "An 'Else' command must follow a 'Then' command.";
                    Message.ForeColor = Color.Maroon;
                    RefreshCommandsView();
                    return;
                }
            }
        }

        Hashtable newCommandEntry = new Hashtable();
        if (prefix == null)
            newCommandEntry["command"] = "Select From";
        else
            newCommandEntry["command"] = prefix + " Select From";

        newCommandEntry["table"] = GetFirstDatabaseTable();
        if (newCommandEntry["table"] == null)
        {

            Message.Text = "You need to first initialize your Google Docs connection by clicking on 'Connect to Your Google Docs'";
            Message.ForeColor = Color.Maroon;
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
    protected string GetFirstDatabaseField(string table)
    {
         Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode database_config = doc.SelectSingleNode("//application/data_sources/data_source"); //new config
        if (database_config == null)
        {
            return "";            
        }

        XmlNode table_name_node = database_config.SelectSingleNode("//tables/table/table_name[.='" + table + "'] | //tables/table/table_name[.='" + table.ToLower() + "']");
        XmlNodeList field_list = table_name_node.ParentNode.SelectNodes("fields/field/name");

        // init database fields
        ArrayList fields = new ArrayList();
        foreach (XmlNode field_node in field_list)
        {
            fields.Add(field_node.InnerText);
        }
        fields.Sort();
        return fields[0].ToString();
    }
    protected string GetFirstDatabaseTable()
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        XmlDocument doc = util.GetStagingAppXml(State);

        //get this page 
        XmlNode page_node = doc.SelectSingleNode("//pages/page/name[.='" + State["SelectedAppPage"].ToString() + "']").ParentNode;

        //get get data sources on this page
        XmlNodeList data_sources = page_node.SelectNodes("data_sources/data_source");
        if (data_sources == null)
        {
            return null;
        }
        int dataSourceIndex = (int)State["PageDataSourceIndex"];

        //get the correct data source by index
        XmlNode data_source = data_sources.Item(dataSourceIndex);
        string data_source_id = data_source.SelectSingleNode("data_source_id").InnerText;

        //get the data source detail from the app data sources
        XmlNode app_data_source = doc.SelectSingleNode("//application/data_sources/data_source/data_source_id[.='" + data_source_id + "']").ParentNode;

        //get all the tables
        XmlNodeList table_list = app_data_source.SelectNodes("data_source_configuration/tables/table");
        ArrayList tables = new ArrayList();
        foreach (XmlNode table_node in table_list)
        {
            XmlNode table_name = table_node.SelectSingleNode("table_name");
            tables.Add(table_name.InnerText);
        }
        tables.Sort();
        if (tables.Count == 0)
            return null;

        //get the first table
        return tables[0].ToString();
    }
    protected RadTreeNode GetCommandRoot()
    {
        if (SpreadsheetCommandsView.Nodes.Count == 0)
            return CreateSpreadsheetRootNode();
              
        return SpreadsheetCommandsView.Nodes[0];
    }
    protected void StoryBoardDisplayType_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { SessionTimeOut.Text = "../../Default.aspx"; return; }

        int toggle = ((RadButton)sender).SelectedToggleStateIndex;
        if (toggle == 0) //show current page
        {
            LoadCurrentStoryBoardPage();
        }
        else //show all pages
        {
            LoadStoryBoard();
        }
    }
    protected void AddJavascript(string code)
    {
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), code, true); 
    }
}