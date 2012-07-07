using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_DatabaseCommand : System.Web.UI.UserControl
{
    public Controls_DatabaseCommand()
    {
    }
    public Controls_DatabaseCommand(string selected_table_name)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        ArrayList tables = (ArrayList)State["DataSourceDatabaseTables"];

        table.Items.Clear();
        foreach (string name in tables)
        {
            Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem(name, name); 
            table.Items.Add(item);
            if (name == selected_table_name)
                item.Selected = true;
        }
    }
}