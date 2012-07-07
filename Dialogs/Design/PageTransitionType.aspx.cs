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
            if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
            XmlUtil x_util = new XmlUtil();
            State["PageTransitionType"] = x_util.GetPageTransitionType(State);
            PageTransitions.Items.FindItemByValue(State["PageTransitionType"].ToString()).Selected = true;
        }
    }
    protected void PageTransitions_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();

            PageTransitions.Text = e.Text;
            State["PageTransitionType"] = e.Value;
            XmlUtil x_util = new XmlUtil();
            x_util.SetPageTransitionType(State, e.Value);            
            Message.Text = "The Page Transition Type has been set.";
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    private void ClearMessages()
    {
        Message.Text = "";
    }
}