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
using Newtonsoft.Json;


public partial class PageData_Dialogs_TestQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        Util util = new Util();
        XmlUtil x_util = new XmlUtil();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        string query_index = Context.Request.QueryString.Get("query_index");
        RefreshCommandsView(Convert.ToInt32(query_index));
    }
    private RadTreeNode CreateSpreadsheetRootNode()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        SpreadsheetCommandView.Nodes.Clear();

        RadTreeNode DatabaseCommandRoot = null;
        if (State["DataSourceEventField"] == null)
            DatabaseCommandRoot = new RadTreeNode("Commands that will run before " + State["SelectedAppPage"].ToString() + " is shown");
        else
            DatabaseCommandRoot = new RadTreeNode("Commands that will run after " + State["DataSourceEventField"].ToString() + " is tapped");

        DatabaseCommandRoot.CssClass = "RadTreeView";
        DatabaseCommandRoot.Category = "event";

        SpreadsheetCommandView.Nodes.Add(DatabaseCommandRoot);

        return DatabaseCommandRoot;
    }
    protected void RefreshCommandsView(int command_index)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        ArrayList DBCommands = (ArrayList)State["DBCommands"];
        RadTreeNode DatabaseCommandRoot = CreateSpreadsheetRootNode();

        if (DBCommands == null)
            return;

        //re add the previous controls
        Hashtable prev_CommandEntry = null;
        Hashtable CommandEntry = (Hashtable)DBCommands[command_index];
        State["TestCommandEntry"] = CommandEntry;
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
        }

        CommandControl = LoadControl("../Controls/DatabaseCommandTest.ascx", selected_table);
        if (command.StartsWith("then "))
        {
            command = command.Remove(0, 5);
            ((HtmlInputText)CommandControl.FindControl("command")).Value = command;
            ((Label)CommandControl.FindControl("command_prefix")).Text = "Then ";
        }
        else if (command.StartsWith("else "))
        {
            command = command.Remove(0, 5);
            ((HtmlInputText)CommandControl.FindControl("command")).Value = command;
            ((Label)CommandControl.FindControl("command_prefix")).Text = "Else ";
        }
        else
            ((HtmlInputText)CommandControl.FindControl("command")).Value = command;

        
        prev_CommandEntry = CommandEntry;

        command_node.Controls.Add(CommandControl);
        DatabaseCommandRoot.Nodes.Add(command_node);

        string field_control_type = null;

        if (command == "insert into")
        {
            field_control_type = "from_phone_to_database";
        }
        else if (command == "update")
        {
             field_control_type = "from_phone_to_database";
        }
        else if (command == "select from")
        {
            field_control_type = "from_database_to_phone";
        }
 
        ArrayList DBFields = (ArrayList)CommandEntry["database_fields"];
        if (DBFields != null && field_control_type != null)
        {
            Hashtable TestInputValueMap = new Hashtable();
            State["TestInputValueMap"] = TestInputValueMap;
            foreach (Hashtable FieldEntry in DBFields)
            {
                RadTreeNode field_node = new RadTreeNode();
                field_node.CssClass = "RadTreeView";
                field_node.Category = "field";
                field_node.PostBack = false;
                State["SelectedDatabaseTable"] = selected_table;
                string control_file = (field_control_type == "from_database_to_phone") ? "DatabaseToDeviceFieldTest.ascx" : "DeviceToDatabaseFieldTest.ascx";
                string selected_database_field = "";
                if (FieldEntry["database_field"] != null && FieldEntry["database_field"].ToString().Length > 0)
                {
                    selected_database_field = FieldEntry["database_field"].ToString();
                }
 
                Control FieldControl = LoadControl("../Controls/" + control_file, selected_database_field);
                
                if (State["SpreadsheetError"] != null)
                {
                    Message.Text = State["SpreadsheetError"].ToString();
                    State["SpreadsheetError"] = null;
                    State["DBCommands"] = null;
                    State["SelectedDatabaseTable"] = null;
                    State["SelectedSqlCommand"] = null;
                    return;
                }
                field_node.Controls.Add(FieldControl);
                command_node.Nodes.Add(field_node);

                if (field_control_type == "from_database_to_phone")
                {
                    if (FieldEntry["device_field"] != null && FieldEntry["device_field"].ToString().Length > 0)
                    {
                        string device_field = FieldEntry["device_field"].ToString();
                        HtmlInputText device_field_input = (HtmlInputText)FieldControl.FindControl("device_field");
                        device_field_input.Value = device_field;
                    }
                }
                else
                {
                    TestInputValueMap[FieldEntry["database_field"].ToString()] = (HtmlInputText)FieldControl.FindControl("device_field");
                }
               
                sub_command_index++;
            }
        }
        ArrayList DBWhere = (ArrayList)CommandEntry["conditions"];
        if (DBWhere != null)
        {
            int i_condition = 0;
            // Hashtable uniqueConditionIDs = new Hashtable();
            Hashtable TestWhereValueMap = new Hashtable();
            State["TestWhereValueMap"] = TestWhereValueMap;

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
                  Control WhereControl = LoadControl("../Controls/SpreadsheetWhereTest.ascx", selected_condition_1st_field);

                if (State["SpreadsheetError"] != null)
                {
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
                    ((HtmlInputText)WhereControl.FindControl("condition_operation")).Value = condition_operation;
                }
                
                if (ConditionEntry["field_operation"] != null && ConditionEntry["field_operation"].ToString().Length > 0)
                {
                    string field_operation = ConditionEntry["field_operation"].ToString();
                    ((HtmlInputText)WhereControl.FindControl("field_operation")).Value = field_operation;
                }

                TestWhereValueMap[i_condition] = (HtmlInputText)WhereControl.FindControl("condition_2nd_field");

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
 
            Control OrderByControl = LoadControl("../Controls/DatabaseOrderByTest.ascx", selected_sort_field);
            if (State["SpreadsheetError"] != null)
            {
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
                ((HtmlInputText)OrderByControl.FindControl("sort_direction")).Value = sort_direction;
            }
            sub_command_index++;
        }

        command_node.ExpandChildNodes();

        SpreadsheetCommandView.ExpandAllNodes();
    }

    protected void TestQueryButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        //default message
        Message.Text = "Enter test values for empty device fields in the Query and click on &quot;Test Spreadsheet Command&quot;";

        DataSources DS = new DataSources();
        string DBConnectionString = DS.GetDataSourceDatabaseConnection(State);
        
        bool doGoogleApps = false;
        try
        {
  
	        string[] parts = DBConnectionString.Split(";".ToCharArray());
            string spreadsheet = null;
            string username = null;
            string password = null;
            string consumer_key = null;
            string consumer_secret = null;
            string requestor_id = null;
            foreach(string part in parts)
            {
                string[] parms = part.Split("=".ToCharArray());
                switch(parms[0])
                {
                    case "spreadsheet":
            	        spreadsheet = parms[1];
                        break;
                    case "username":                       
                        username = parms[1];
                         break;
                    case "password":                       
                         password = parms[1];
                         break;
                    case "consumer_key":                        
                        consumer_key = parms[1];
                        break;
                    case "consumer_secret":                       
                        consumer_secret = parms[1];
                         break;
                    case "requestor_id":                       
                         requestor_id = parms[1];
                         break;
                }           
            }
            string initGoogleDocsParams = null;
            if (password != null) {
                initGoogleDocsParams = "?username=" + username + "&password=" + password + "&spreadsheet=" + HttpUtility.UrlEncode(spreadsheet);
                doGoogleApps = false;
            }
            else {
                initGoogleDocsParams = "?requestor_id=" + requestor_id + "&consumer_key=" + consumer_key + "&consumer_secret=" + consumer_secret + "&spreadsheet=" + HttpUtility.UrlEncode(spreadsheet);
                doGoogleApps = true;
            }
            doSqlCommand(State,doGoogleApps,initGoogleDocsParams);
         }
        catch(Exception ex)
        {
            string x = ex.Message + ": " + ex.StackTrace;
            return; //caused when use tries to build output before input is done 
        }
    }

    protected void  doSqlCommand(Hashtable State, bool doGoogleApps,string initGoogleDocsParams) 
    {  
        string SpreadsheetRequestUrl = doGoogleApps?  State["google_apps_spreadsheet_web_service_url"].ToString(): State["google_spreadsheet_web_service_url"].ToString();
        StringBuilder GoogleDocsParams = new StringBuilder(SpreadsheetRequestUrl + initGoogleDocsParams);
        Hashtable CommandEntry =  (Hashtable)State["TestCommandEntry"]  ;
        string table = CommandEntry["table"].ToString();
        GoogleDocsParams.Append("&table=" + HttpUtility.UrlEncode(table));
        string command = CommandEntry["command"].ToString().ToLower();
        GoogleDocsParams.Append("&query_type=" + HttpUtility.UrlEncode(command));

        ArrayList DBFields = (ArrayList)CommandEntry["database_fields"];
 
        
        ArrayList DBWhere = (ArrayList)CommandEntry["conditions"];
        if (DBWhere != null)
        {
            StringBuilder sb_conditions = new StringBuilder();
            int k = 0;
            foreach (Hashtable ConditionEntry in DBWhere)
            {
	            if(k>0)
	            {
	                string condition_op =ConditionEntry["condition_operation"].ToString();
	                sb_conditions.Append( " " + condition_op.ToLower() + " ");
	            }
 	            sb_conditions.Append(ConditionEntry["condition_1st_field"].ToString());
	            sb_conditions.Append(ConditionEntry["field_operation"].ToString());

                Hashtable TestWhereValueMap = (Hashtable) State["TestWhereValueMap"] ;
                HtmlInputText test_input = (HtmlInputText)TestWhereValueMap[k];
                string condition_2nd_field = test_input.Value;
                sb_conditions.Append("\"" + condition_2nd_field + "\"");
                k++;
	        }
	        GoogleDocsParams.Append("&conditions=" + HttpUtility.UrlEncode(sb_conditions.ToString()));            
        }
        Hashtable DBOrderBy = (Hashtable)CommandEntry["order_by"];
        if (DBOrderBy != null)
        {
             string    selected_sort_field = DBOrderBy["sort_field"].ToString();
             string sort_direction = DBOrderBy["sort_direction"].ToString();
 	         GoogleDocsParams.Append("&order_by=" + selected_sort_field + "&sort_direction=" + sort_direction.ToLower());
        }	
 
 	    if(command=="select from") {
	        googleSpreadsheetQuery(GoogleDocsParams.ToString()+"&callback=?");
	    }
	    else if(command == "insert into")
	    {
            StringBuilder insert = new StringBuilder();
	        foreach(Hashtable FieldEntry in DBFields)
	        {
                Hashtable TestInputValueMap = (Hashtable)State["TestInputValueMap"];
                HtmlInputText test_input = (HtmlInputText)TestInputValueMap[FieldEntry["database_field"].ToString()];
                string device_field = test_input.Value;
                if (device_field.Length == 0)
                    continue;
                insert.Append( FieldEntry["database_field"].ToString() + "=|" + HttpUtility.UrlEncode(device_field) + "|;");	//for some reason quotes do not encode correctly through url query                 
            }
	        GoogleDocsParams.Append("&insert=" + HttpUtility.UrlEncode(insert.ToString()));
 	        googleSpreadsheetQuery(GoogleDocsParams.ToString()+"&callback=?");	
	    }
	    else if(command == "update")
	    {
           StringBuilder update = new StringBuilder();
	        foreach(Hashtable FieldEntry in DBFields)
	        {
                Hashtable TestInputValueMap = (Hashtable)State["TestInputValueMap"];
                HtmlInputText test_input = (HtmlInputText)TestInputValueMap[FieldEntry["database_field"].ToString()];
                string device_field = test_input.Value;
                if (device_field.Length == 0)
                    continue;
                update.Append(FieldEntry["database_field"].ToString() + "=|" + HttpUtility.UrlEncode(device_field) + "|;");	//for some reason quotes do not encode correctly through url query                 
            }
	        GoogleDocsParams.Append("&update=" + HttpUtility.UrlEncode(update.ToString()));
 	        googleSpreadsheetQuery(GoogleDocsParams.ToString()+"&callback=?");	
	    }
	    else if(command == "delete from") {
	        googleSpreadsheetQuery(GoogleDocsParams.ToString()+"&callback=?");	
	    }     
    }


    protected void googleSpreadsheetQuery(string SpreadsheetURL)
    {
        Util util = new Util();
        string json = util.GetWebPage(SpreadsheetURL).Substring(2); //remove beginning "?(" and ending ")"
        XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(json.Remove(json.Length - 1));
        XmlNode status = doc.SelectSingleNode("//status");
        if (status.InnerText != "success")
        {
            Message.Text = status.InnerText;
            return;
        }
        XmlNodeList rows = doc.SelectNodes("//row");
        if (rows.Count == 0)
        {
            SpreadsheetResponseTreeView.Nodes.Clear();
            RadTreeNode ResponseRootEmpty = new RadTreeNode("Response was successful but there were no response entries from this Spreadsheet Query");
            ResponseRootEmpty.CssClass = "RadTreeView";
            ResponseRootEmpty.Category = "response";
            SpreadsheetResponseTreeView.Nodes.Add(ResponseRootEmpty);
            return;
        }
        XmlNode first_row = rows[0];
        XmlNodeList fieldNames = first_row.SelectNodes("field");
        Hashtable DeviceFields = new Hashtable();
        foreach (XmlNode field in fieldNames)
        {
            DeviceFields[field.InnerText] = new ArrayList();
         }

        SpreadsheetResponseTreeView.Nodes.Clear();
        RadTreeNode ResponseRoot = new RadTreeNode("Response from Spreadsheet Query:");
        ResponseRoot.CssClass = "RadTreeView";
        ResponseRoot.Category = "response";

        //get database to device field map
        Hashtable DatabaseToDeviceMap = new Hashtable();
        if (rows != null && rows.Count > 0)
        {
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            Hashtable CommandEntry = (Hashtable)State["TestCommandEntry"];
            ArrayList DBFields = (ArrayList)CommandEntry["database_fields"];
            foreach (Hashtable FieldEntry in DBFields)
            {
                DatabaseToDeviceMap[FieldEntry["database_field"].ToString()] = FieldEntry["device_field"].ToString();
            }

            //get all values for each database field
            foreach (XmlNode row in rows)
            {
                XmlNodeList fields = row.SelectNodes("field");
                XmlNodeList values = row.SelectNodes("value");
                int index = 0;
                foreach (XmlNode field in fields)
                {
                    ArrayList list = (ArrayList)DeviceFields[field.InnerText];
                    list.Add(values[index].InnerText);
                    index++;
                }
            }
        }
        foreach (String key in DeviceFields.Keys)
        {
            if (DatabaseToDeviceMap[key] == null)
                continue;
            StringBuilder string_array = new StringBuilder();
            ArrayList values = (ArrayList) DeviceFields[key];
            bool is_first = true;
            foreach(String value in values)
            {
                if(is_first)
                    is_first = false;
                else
                     string_array.Append("; ");

                if(values.Count > 1)
                     string_array.Append("\"" + value + "\"" );
                else
                    string_array.Append( value);
            }
            UserControl ResponseControl = null;
            string field_value = string_array.ToString();

            if (field_value.Length > 49)
                ResponseControl = LoadControl("../Controls/TestResponseLong.ascx", DatabaseToDeviceMap[key].ToString() + "=" + string_array.ToString());
            else
                ResponseControl = LoadControl("../Controls/TestResponse.ascx", DatabaseToDeviceMap[key].ToString() + "=" + string_array.ToString());

            RadTreeNode row_node = new RadTreeNode("row");
            ResponseRoot.CssClass = "RadTreeView";
            ResponseRoot.Category = "response";
            row_node.Controls.Add(ResponseControl);
            ResponseRoot.Nodes.Add(row_node);
        }
        SpreadsheetResponseTreeView.Nodes.Add(ResponseRoot);
        SpreadsheetResponseTreeView.ExpandAllNodes();
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
}