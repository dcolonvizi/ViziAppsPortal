using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dialogs_SetMobileCommerce : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlUtil x_util = new XmlUtil();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        if (!IsPostBack)
        {
            String[] Values = x_util.GetMobileCommerce(State, State["SelectedApp"].ToString());
            if (Values != null)
            {
                MobileCommerceUsername.Text = Values[0];
                MobileCommercePassword.Text = Values[1];
            }
        }
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        XmlUtil x_util = new XmlUtil();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        if (MobileCommerceUsername.Text.Trim().Length == 0)
        {
            Message.Text = "Set the Username.";
            return;
        }
        if (MobileCommercePassword.Text.Trim().Length == 0)
        {
            Message.Text = "Set the Password.";
            return;
        }
        x_util.SetMobileCommerce(State, MobileCommerceUsername.Text.Trim(), MobileCommercePassword.Text.Trim());
        Message.Text = "Mobile Commerce has been set.";
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
        XmlUtil x_util = new XmlUtil();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        x_util.ClearMobileCommerce(State);

        MobileCommerceUsername.Text = "";
        MobileCommercePassword.Text = "";
        Message.Text = "Mobile Commerce has been cleared.";
    }
}