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
using HtmlAgilityPack;

/// <summary>
/// Summary description for DataSources
/// </summary>
public class DataSources
{
	public DataSources()
	{
	}
    public SortedList GetAppDataSources(Hashtable State)
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
        if (list == null || list.Count == 0)         
            return null;
  
        SortedList sorted_sources = new SortedList();
        foreach (XmlNode data_source in list)
        {
            sorted_sources[data_source.SelectSingleNode("data_source_id").InnerText] = data_source.SelectSingleNode("data_source_type").InnerText;
        }

        if (xml_changed)
        {
            Util util = new Util();
            util.UpdateStagingAppXml(State);
        }
        return sorted_sources;
    }
    public ArrayList GetDataSourceDatabaseTables(Hashtable State)
    {
        XmlDocument doc = GetStagingAppXml(State);
        if (State["DataSourceID"] == null)
            return null;
        XmlNode data_source_id = doc.SelectSingleNode("//application/data_sources/data_source/data_source_id[.='" + State["DataSourceID"].ToString() + "']");
        if (data_source_id == null)
            return null;

        XmlNode data_source = data_source_id.ParentNode;

        XmlNodeList table_list = data_source.SelectNodes("data_source_configuration/tables/table");
        ArrayList tables = new ArrayList();
        foreach (XmlNode table_node in table_list)
        {
            XmlNode table_name = table_node.SelectSingleNode("table_name");
            tables.Add(table_name.InnerText);
        }
        tables.Sort();
        return tables;
    }
    public string GetDataSourceDatabaseConnection(Hashtable State)
    {
        XmlDocument doc = GetStagingAppXml(State);
        if (State["DataSourceID"] == null)
            return null;
        XmlNode data_source_id = doc.SelectSingleNode("//application/data_sources/data_source/data_source_id[.='" + State["DataSourceID"].ToString() + "']");
        if (data_source_id == null)
        {
            return null;
        }

        XmlNode data_source = data_source_id.ParentNode;
        XmlNode connection = data_source.SelectSingleNode("data_source_configuration/connection_string");
        if (connection == null)
        {
            return null;
        }

        return connection.InnerText;
    }
    public ArrayList GetDataSourceDatabaseTableFields(Hashtable State)
    {
        XmlDocument doc = GetStagingAppXml(State);
        if (State["DataSourceID"] == null)
            return null;
        XmlNode data_source_id = doc.SelectSingleNode("//application/data_sources/data_source/data_source_id[.='" + State["DataSourceID"].ToString() + "']");
        if (data_source_id == null)
        {
            return null;
        }

        XmlNode data_source = data_source_id.ParentNode;
        if (State["SelectedDatabaseTable"] == null)
        {
            State["SpreadsheetError"] = "Query Error: No Table has been selected.";
            return null;
        }

        string table = State["SelectedDatabaseTable"].ToString();
        XmlNode table_name_node = data_source.SelectSingleNode("data_source_configuration/tables/table/table_name[.='" + table + "'] | data_source_configuration/tables/table/table_name[.='" + table.ToLower() + "']");

        if (table_name_node == null)
        {
            State["SpreadsheetError"] = "Query Error: A saved worksheet or table: " + table + " does not exist. All queries for this event have been deleted to avoid runtime errors.";
            State["SelectedDatabaseTable"] = null;
            return null;
        }
        XmlNodeList field_list = table_name_node.ParentNode.SelectNodes("fields/field/name");

        // init database fields
        ArrayList fields = new ArrayList();
        foreach (XmlNode field_node in field_list)
        {
            fields.Add(field_node.InnerText);
        }
        fields.Sort();
        return fields;
    }
    public SortedList GetPageDataSources(Hashtable State)
    {
        SortedList sorted_sources = new SortedList();
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNode page_name = xmlDoc.SelectSingleNode("//pages/page/name[.='" + State["SelectedAppPage"].ToString() + "']");
        XmlNode data_sources = page_name.ParentNode.SelectSingleNode("data_sources");
        if (data_sources == null)
        {
            return null;
        }
        XmlNodeList list = data_sources.SelectNodes("data_source");
        int index = 1;
        foreach (XmlNode data_source in list)
        {
            ArrayList source_info = new ArrayList();
            source_info.Add(data_source.SelectSingleNode("data_source_id").InnerText);

            XmlNodeList events = data_source.SelectNodes("event");
            ArrayList event_info = new ArrayList();
            foreach (XmlNode event_node in events)
            {
                XmlNode data_source_event_type_node = event_node.SelectSingleNode("data_source_event_type");
                if (data_source_event_type_node == null)
                    continue;
                string event_type = data_source_event_type_node.InnerText;
                event_info.Add(event_type);
                if (event_type == "field")
                    event_info.Add(event_node.SelectSingleNode("data_source_event_field").InnerText);
            }
            source_info.Add(event_info);
            sorted_sources[index.ToString()] = source_info;
            index++;
        }

        return sorted_sources;
    }
    public SortedList GetPageEventFields(Hashtable State)
    {
        SortedList sorted_sources = new SortedList();
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNode page_name = xmlDoc.SelectSingleNode("//pages/page/name[.='" + State["SelectedAppPage"].ToString() + "']");
        XmlNodeList list = page_name.ParentNode.SelectSingleNode("fields").ChildNodes;
        foreach (XmlNode field in list)
        {
            if (field.Name == "button" || field.Name == "iamge_button" || field.Name == "switch" || field.Name == "table")
                sorted_sources[field.SelectSingleNode("id").InnerText] = field.Name;
        }

        return sorted_sources;
    }
    public void AddDataSourceToPage(Hashtable State, string data_source_id)
    {
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNode data_source_id_node = xmlDoc.SelectSingleNode("//application/data_sources/data_source/data_source_id[.='" + data_source_id + "']");
        if (data_source_id == null)
        {
            return;
        }
        XmlNode page = xmlDoc.SelectSingleNode("//pages/page/name[.='" + State["SelectedAppPage"].ToString() + "']").ParentNode;
        XmlNode data_sources = page.SelectSingleNode("data_sources");
        if (data_sources == null)
            data_sources = CreateNode(xmlDoc, page, "data_sources");
        XmlNode page_data_source = CreateNode(xmlDoc, data_sources, "data_source");

        CreateNode(xmlDoc, page_data_source, "data_source_id", data_source_id);
        XmlNode event_node = CreateNode(xmlDoc, page_data_source, "event");
        CreateNode(xmlDoc, event_node, "data_source_event_type", "page");
        CreateNode(xmlDoc, event_node, "data_source_operations");
        State["AppXmlDoc"] = xmlDoc;
        Util util = new Util();
        util.UpdateStagingAppXml(State);
    }
    public void RemovePageDataSource(Hashtable State, int data_source_index)
    {
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNode page = xmlDoc.SelectSingleNode("//pages/page/name[.='" + State["SelectedAppPage"].ToString() + "']").ParentNode;
        XmlNodeList list = page.SelectNodes("data_sources/data_source");
        if (list == null || list.Count == 0)
        {
            return;
        }
        list[data_source_index].ParentNode.RemoveChild(list[data_source_index]);
        State["AppXmlDoc"] = xmlDoc;
        Util util = new Util();
        util.UpdateStagingAppXml(State);
    }
    public void SaveGoogleSpreadsheetDataSource(Hashtable State, string id, string DatabaseType, string DBConnectionString, Hashtable tables)
    {
        Util util = new Util();
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNode application = xmlDoc.SelectSingleNode("//application");
        XmlNode data_sources = application.SelectSingleNode("data_sources");
        if (data_sources == null)
        {
            data_sources = CreateNode(xmlDoc, application, "data_sources");
        }
        XmlNode data_source = null;
        XmlNode tables_node = null;
        XmlNode data_source_id = xmlDoc.SelectSingleNode("//application/data_sources/data_source/data_source_id[.='" + id + "']");
        if (data_source_id != null)
        {
            data_source = data_source_id.ParentNode;
            data_source.RemoveAll();
        }
        else
        {
            data_source = CreateNode(xmlDoc, data_sources, "data_source");
        }
        data_source_id = CreateNode(xmlDoc, data_source, "data_source_id", id);
        CreateNode(xmlDoc, data_source, "data_source_type", "google_spreadsheet");
        XmlNode data_source_configuration = CreateNode(xmlDoc, data_source, "data_source_configuration");
        CreateNode(xmlDoc, data_source_configuration, "connection_string", DBConnectionString);
        tables_node = CreateNode(xmlDoc, data_source_configuration, "tables");

        foreach (string table_name in tables.Keys)
        {
            XmlNode table_node = CreateNode(xmlDoc, tables_node, "table");
            CreateNode(xmlDoc, table_node, "table_name", table_name);
            ArrayList field_list = (ArrayList)tables[table_name];
            XmlNode fields_node = CreateNode(xmlDoc, table_node, "fields");
            foreach (Hashtable field in field_list)
            {
                XmlNode field_node = CreateNode(xmlDoc, fields_node, "field");
                CreateNode(xmlDoc, field_node, "name", field["name"].ToString());
                if (field["type"] != null)
                    CreateNode(xmlDoc, field_node, "type", field["type"].ToString());
                if (field["length"] != null)
                    CreateNode(xmlDoc, field_node, "length", field["length"].ToString());
            }
        }

        State["AppXmlDoc"] = xmlDoc;
        util.UpdateStagingAppXml(State);
    }
    public void DeleteAppDataSource(Hashtable State, string data_source_id)
    {
        XmlDocument xmlDoc = GetStagingAppXml(State);
        XmlNodeList data_sources = xmlDoc.SelectNodes("//data_source_id[.='" + data_source_id + "']");
        ArrayList tobe_deleted = new ArrayList();
        foreach (XmlNode data_source in data_sources)
        {
            tobe_deleted.Add(data_source.ParentNode);
        }
        foreach (XmlNode node in tobe_deleted)
        {
            node.ParentNode.RemoveChild(node);
        }
        //remove old version also
        XmlNode database_config = xmlDoc.SelectSingleNode("//database_config");
        if (database_config != null)
            database_config.ParentNode.RemoveChild(database_config);

        State["AppXmlDoc"] = xmlDoc;
        Util util = new Util();
        util.UpdateStagingAppXml(State);
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
    public string GetSpreadsheetLinkFromConnectionString(string connection_string)
    {
        string url = null;
        int index = connection_string.IndexOf("url='");
        if (index >= 0)
        {
            index += 5;
            int end = connection_string.IndexOf("'", index);
            if (end >= 0)
                url = connection_string.Substring(index, end - index);
        }
        return url;
    }
    public Hashtable ParseSqlCommand(XmlDocument doc, XmlNode sql_command)
    {
        Hashtable CommandEntry = new Hashtable();
        string command = sql_command.SelectSingleNode("command").InnerText.ToLower();
        CommandEntry["command"] = command;
        if (command == "if")
        {
            CommandEntry["command_condition_phone_field1"] = sql_command.SelectSingleNode("command_condition_phone_field1").InnerText;
            CommandEntry["command_condition_operation"] = sql_command.SelectSingleNode("command_condition_operation").InnerText;
            CommandEntry["command_condition_phone_field2"] = sql_command.SelectSingleNode("command_condition_phone_field2").InnerText;
        }
        else
        {
            if (command.EndsWith("go to page"))
            {
                CommandEntry["page"] = sql_command.SelectSingleNode("page").InnerText;
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
                            break;
                        Hashtable FieldEntry = new Hashtable();
                        if (field.SelectSingleNode("database_field") != null)
                            FieldEntry["database_field"] = field.SelectSingleNode("database_field").InnerText;
                        else
                            FieldEntry["database_field"] = GetFirstDatabaseField(doc, CommandEntry["table"].ToString());
                        XmlNode phone_field_node = field.SelectSingleNode("phone_field");
                        if (phone_field_node != null)
                            FieldEntry["phone_field"] = phone_field_node.InnerText;
                        else
                            FieldEntry["phone_field"] = "";

                        DBFields.Add(FieldEntry);
                    }
                    CommandEntry["database_fields"] = DBFields;

                }
            }
            XmlNodeList conditions = sql_command.SelectNodes("condition");
            if (conditions != null)
            {
                ArrayList DBConditions = new ArrayList();
                foreach (XmlNode condition in conditions)
                {
                    if (condition.InnerXml.Length == 0)
                        break;
                    Hashtable ConditionEntry = new Hashtable();
                    ConditionEntry["condition_operation"] = condition.SelectSingleNode("condition_operation").InnerText;
                    if (condition.SelectSingleNode("condition_1st_field") != null)
                        ConditionEntry["condition_1st_field"] = condition.SelectSingleNode("condition_1st_field").InnerText;
                    else
                        ConditionEntry["condition_1st_field"] = GetFirstDatabaseField(doc, CommandEntry["table"].ToString());

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
        return CommandEntry;
    }
    public bool DoesAppUseGoogleSpreadsheets(Hashtable State)
    {
         XmlDocument xmlDoc = GetStagingAppXml(State);
         XmlNode type = xmlDoc.SelectSingleNode("//data_source_type[.='google_spreadsheet']");
         return (type == null) ? false : true;
    }
     protected string GetFirstDatabaseField(XmlDocument doc,string table)
    {
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
}