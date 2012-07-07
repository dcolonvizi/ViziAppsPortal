using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class Dialogs_AccountIdentifier : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../../Default.aspx');", true); return; }

        Message.Text = "";

        if(!IsPostBack)
        {
        if( State["UrlAccountIdentifier"]!= null &&  State["UrlAccountIdentifier"].ToString().Length > 0)
            UrlAccountIdentifier.Value =  State["UrlAccountIdentifier"].ToString();
        }
    }
    private static bool IsStringAlphaNumericPeriod(string str)
    {
        Regex r = new Regex(@"^[a-zA-Z0-9\.]*$");
        return r.IsMatch(str);
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        string name = UrlAccountIdentifier.Value.Trim();
        if (name.Length == 0)
        {
            Message.Text = "Enter Account Identifier Name";
            return;
        }
        if (!IsStringAlphaNumericPeriod(name) || name == ".")
        {
            Message.Text = "Domain Name should only use alphanumeric characters or '.'";
            return;
        }
        if (util.DoesUrlAccountIdentifierExist((Hashtable)HttpRuntime.Cache[Session.SessionID], name))
            Message.Text = "Account Identifier " + name + " is not Available.";
        else
        {
            UrlAccountIdentifier.Value = name;
            util.SaveUrlAccountIdentifier((Hashtable)HttpRuntime.Cache[Session.SessionID], name);
             State["UrlAccountIdentifier"] = name;
            Message.Text = "Account Identifier " + name + " has been saved. ";
        }
    }
}
