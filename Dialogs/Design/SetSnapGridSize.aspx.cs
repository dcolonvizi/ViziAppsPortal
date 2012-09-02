using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using Telerik.Web.UI;
using System.Drawing;

public partial class Dialogs_PageTransitionType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;
            String grid_size = Request.QueryString.Get("grid_size");
            SnapGridSize.FindItemByText(grid_size).Selected = true;
         }
        ClearMessages();
    }
   
    private void ClearMessages()
    {
        Message.Text = "";
    }
}