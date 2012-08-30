using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

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
        bool is_found = false;
        foreach (string name in tables)
        {
            Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem(name, name); 
            table.Items.Add(item);
            if (selected_table_name == name)
            {
                item.Selected = true;
                is_found = true;
            }
        }
        if (!is_found)
        {
            table.Items.Insert(0, new RadComboBoxItem("Select ->", "no_value"));
            table.SelectedIndex = 0;
        }
    }
}