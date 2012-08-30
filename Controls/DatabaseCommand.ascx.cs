using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_DatabaseCommand : System.Web.UI.UserControl
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

        XmlNodeList table_list = database_config.SelectNodes("//tables/table");
        table.Items.Clear();
        ArrayList tables = new ArrayList();
        foreach (XmlNode table_node in table_list)
        {
            XmlNode table_name = table_node.SelectSingleNode("table_name");
            tables.Add(table_name.InnerText);
        }
        tables.Sort();

        foreach(string name in tables)
        {
            table.Items.Add(new Telerik.Web.UI.RadComboBoxItem(name, name));
        }
    }
}