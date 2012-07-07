using System;
using System.Collections.Generic;
using System.Collections;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dialogs_CoverFlow_TemplateAndroidDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../../Default.aspx');", true);
            return;
        }

        Image1.ImageUrl = Request.QueryString.Get("src");
         State["SelectedTemplateApp"] = Request.QueryString.Get("app");
    }
    protected void AddTemplateApp_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        if (AppName.Text.Length == 0)
        {
            Message.Text = "Enter your own name for this app.";
            return;
        }
        string new_app_name = AppName.Text.Trim();
        if (!Check.ValidateObjectName(Message, new_app_name))
        {
            return; 
        }

        if(util.DoesAppExistInAccount(State,new_app_name))
        {
            Message.Text = "The app name " + new_app_name + " already exists. Enter a unique app name.";
            return;
        }

         State["NameForTemplateApp"] = new_app_name;
        Ready.Text = "OK";
    }
}
