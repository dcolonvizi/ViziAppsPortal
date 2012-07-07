using System;
using System.Collections;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GoogleAppsCancelService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

    }
    protected void RejectButton_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        Response.Redirect("Default.aspx", true);
    }
}