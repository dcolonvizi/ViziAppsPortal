using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Controls_DeviceToDatabaseField : System.Web.UI.UserControl
{
   public Controls_DeviceToDatabaseField()
   {
   }
   public Controls_DeviceToDatabaseField(string selected_database_field)
   {
       Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
       ArrayList fields = (ArrayList)State["DataSourceDatabaseTableFields"];

        foreach (string name in fields)
        {
            RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem(name, name);
            database_field.Items.Add(item);
            if (selected_database_field == name)
                item.Selected = true;
        }
     }
}