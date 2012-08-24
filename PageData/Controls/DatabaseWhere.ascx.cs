using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Controls_DatabaseWhere : System.Web.UI.UserControl
{
    public Controls_DatabaseWhere()
    {
    }
    public Controls_DatabaseWhere(string selected_condition_1st_field)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if ((bool)State["IsFirstCommandCondition"])
        {
            condition_operation.Items.Clear();
            condition_operation.Items.Add(new Telerik.Web.UI.RadComboBoxItem("Where", "Where"));
        }

        ArrayList fields = (ArrayList)State["DataSourceDatabaseTableFields"];

        bool is_found = false;
        foreach (string name in fields)
        {
            Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem(name, name);
            condition_1st_field.Items.Add(item);
            if (selected_condition_1st_field == name)
            {
                item.Selected = true;
                is_found = true;
            }
         }
        if (!is_found)
        {
            condition_1st_field.Items.Insert(0, new RadComboBoxItem("Select ->", "no_value"));
            condition_1st_field.SelectedIndex = 0;
        }
    }
}