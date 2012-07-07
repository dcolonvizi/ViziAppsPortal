using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ValidateEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
 
        string customer_id = Request.QueryString.Get("id");
        if (!IsPostBack)
        {
            bool is_active = util.ActivateCustomerAccount((Hashtable)HttpRuntime.Cache[Session.SessionID], customer_id);
            if (is_active)
            {
                Message.Text = "Your account has been activated. You can now login to the ViziApps Studio.";
            }
            else
            {
                Message.Text = "Your email is not recognized.";
                LoginButton.Visible = false;
            }
        }
    }
}