using System;
using System.Collections;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewUserProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

    }
}