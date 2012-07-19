using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InsertTextField : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true);
        if (State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE || State["SelectedAppType"].ToString() == Constants.HYBRID_APP_TYPE)
            Response.Redirect("InsertWebAppTextField.aspx", false);
        // Bold.Attributes.Add("onclick", "onBoldClick();");
        //Italic.Attributes.Add("onclick", "onItalicClick();");
    }
}
