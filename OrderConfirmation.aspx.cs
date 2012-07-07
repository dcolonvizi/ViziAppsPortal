using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class OrderConfirmation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        
        if (!IsPostBack)
        {
            string username = Request.QueryString.Get("user");
            string email = null;
            if (username == null)
                email = Request.QueryString.Get("email");
            string confirm = Request.QueryString.Get("confirm");
            ConfirmationNumber.Text = confirm;
            string order = Request.QueryString.Get("order");
            string[] skus = order.Split("_".ToCharArray());       

            string customer_id = null;
            if (username != null)
            {
                customer_id = util.GetCustomerIDFromUsername(State, username);
            }
            else
            {
                customer_id = util.GetCustomerIDFromEmail(State, email);
            }
            if (customer_id != null)
            {
                 State["CustomerID"] = customer_id;
                util.SetPaidService(State, confirm, customer_id, skus);
            }
            else
            {
                Warning.Visible = true;
            }
        }
    }
    protected void LoginToUser_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
        
        if (State["CustomerID"] == null)
        {
            Warning.Visible = true;
            Warning.Text = "Unknown user credentials from email.";
            return;
        }
        DB db = new DB();
        string sql = "SELECT username,password FROM customers WHERE customer_id='" +  State["CustomerID"].ToString() + "'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        DataRow row = rows[0];
        db.CloseViziAppsDatabase(State);

        
         State["Username"] = row["username"].ToString();
         State["Password"] = row["password"].ToString();
         State["LoggedInFromAdmin"] = true;
        Response.Redirect("Default.aspx", false);
    }
}