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
using System.IO;
using System.Xml;
using Telerik.Web.UI;

public partial class CopyApplication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        Message.Text = "";
        if (!IsPostBack)
        {
            Init init = new Init();
            init.InitAccountList(State, FromAccounts, true);
            init.InitAccountList(State, ToAccounts, false);
            CopyApplicationButton.Visible = false;
        }
    }
    protected void FromAccounts_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        if (e.Text.IndexOf("->") > 0)
        {
            Applications.Visible = false;
            CopyApplicationButton.Visible = false;
            return;
        }
        Applications.Visible = true;

        DB db = new DB();
        string sql = "SELECT customer_id FROM customers WHERE username='" + e.Text + "'";
        string customer_id = db.ViziAppsExecuteScalar((Hashtable)HttpRuntime.Cache[Session.SessionID], sql);
         ((Hashtable)HttpRuntime.Cache[Session.SessionID])["CopyApplicationFromCustomerID"] = customer_id;

        Init init = new Init();
        init.InitAppsList((Hashtable)HttpRuntime.Cache[Session.SessionID], Applications, customer_id);

        db.CloseViziAppsDatabase((Hashtable)HttpRuntime.Cache[Session.SessionID]);
    }
    protected void ToAccounts_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        if (e.Text.IndexOf("->") > 0)
        {
            CopyApplicationButton.Visible = false;
            return;
        }

        CopyApplicationButton.Visible = true;
 
        DB db = new DB();
        string sql = "SELECT customer_id FROM customers WHERE username='" + e.Text + "'";
        string customer_id = db.ViziAppsExecuteScalar((Hashtable)HttpRuntime.Cache[Session.SessionID], sql);
         ((Hashtable)HttpRuntime.Cache[Session.SessionID])["CopyApplicationToCustomerID"] = customer_id;
        db.CloseViziAppsDatabase((Hashtable)HttpRuntime.Cache[Session.SessionID]);
    }
    protected void CopyApplicationButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        if (Applications.Text.IndexOf("->") > 0)
        {
            Message.Text = "Select Application.";
            return;
        }
        if (ToAccounts.Text.IndexOf("->") > 0)
        {
            Message.Text = "Select Destination Account.";
            return;
        }
        if (ToAccounts.Text == FromAccounts.Text)
        {
            Message.Text = "Select a Different Destination Account.";
            return;
        }

        string app_name = Applications.SelectedItem.Text;

        //copy application
        util.CopyAppToAccount((Hashtable)HttpRuntime.Cache[Session.SessionID], app_name);
        Message.Text = "Application Copy is Successful.";
    }

}