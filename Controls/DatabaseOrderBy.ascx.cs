using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Controls_DatabaseOrderBy : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode database_config = doc.SelectSingleNode("//application/data_sources/data_source"); //new config
        if (database_config == null)
        {
            database_config = doc.SelectSingleNode("//database_config"); //old config
            if (database_config == null)
            {
                return;
            }
        }

        string table =  State["SelectedDatabaseTable"].ToString();
        XmlNode table_name_node = database_config.SelectSingleNode("//tables/table/table_name[.='" + table + "'] | //tables/table/table_name[.='" + table.ToLower() + "']");
        if (table_name_node == null)
        {
            State["SpreadsheetError"] = "Query Error: A saved worksheet or table: " + table + " does not exist. All queries for this event have been deleted to avoid runtime errors.";
            State["SelectedDatabaseTable"] = null;
            return;
        }
        XmlNodeList field_list = table_name_node.ParentNode.SelectNodes("fields/field/name");

        // init database fields
        sort_field.Items.Clear();
        ArrayList fields = new ArrayList();
        foreach (XmlNode field_node in field_list)
        {
            fields.Add(field_node.InnerText);
        }
        fields.Sort();

        foreach (string name in fields)
        {
            sort_field.Items.Add(new Telerik.Web.UI.RadComboBoxItem(name, name));
        }
    }
}