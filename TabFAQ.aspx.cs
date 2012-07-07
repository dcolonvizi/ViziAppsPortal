using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Configuration;
using MySql.Data.MySqlClient;

public partial class TabFAQ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State,Response,"Default.aspx")) return;
        try
        {
            if (!IsPostBack)
               UserLabel.Text = State["Username"].ToString();

            if (State["TechSupportEmail"] != null)
            {
                util.AddEmailToButton(SupportButton, State["TechSupportEmail"].ToString(), "Email To Tech Support");
            }
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }
    }
    protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        string account_type = util.GetAccountType(State);
        util.Logout(State);
        if (account_type != null && account_type.Contains("google_apps"))
            Response.Redirect("LogoutForGoogleApps.aspx", false);
        else
            Response.Redirect("Default.aspx", false);

    }
    protected void TabMenu_ItemClick(object sender, RadMenuEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        string tab = e.Item.Value;
        Session["MainMenu"] = tab;
        Response.Redirect("Tab" + tab + ".aspx", false);
    }
}