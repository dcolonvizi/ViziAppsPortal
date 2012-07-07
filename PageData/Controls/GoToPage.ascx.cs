using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_GoToPage : System.Web.UI.UserControl
{
    public Controls_GoToPage()
    {
    }
    public Controls_GoToPage(string selected_page)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        XmlUtil x_util = new XmlUtil();
        string[] pages = x_util.GetAppPageNames(State, State["SelectedApp"].ToString());

        gotopage.Items.Clear();
        foreach (string name in pages)
        {
            Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem(name, name);
            gotopage.Items.Add(item);
            if (name == selected_page)
                item.Selected = true;
        }
    }
}