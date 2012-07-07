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
    public Controls_DatabaseOrderBy()
    {
    }
    public Controls_DatabaseOrderBy(string selected_sort_field)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        ArrayList fields = (ArrayList)State["DataSourceDatabaseTableFields"];

        sort_field.Items.Clear();
        foreach (string name in fields)
        {
            RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem(name, name);
            sort_field.Items.Add(item);
            if (selected_sort_field == name)
                item.Selected = true;
        }
    }
}