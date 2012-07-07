using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Help_ViewConnectionString : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../../Default.aspx');", true);
            return;
        }
        if ( State["DBConnectionString"] != null)
            ConnectionString.Text =  State["DBConnectionString"].ToString();
        else
            ConnectionString.Text = "No Connection String has been entered. Click on <b>Upload Database Info</b> to enter the Connection String.";

        if ( State["DBWebServiceURL"] != null)
            DBWebServiceURL.Text =  State["DBWebServiceURL"].ToString();
        else
            DBWebServiceURL.Text = "No ViziApps Web Service URL has been entered. Click on <b>DB Web Service</b> to enter the URL.";
    }
}