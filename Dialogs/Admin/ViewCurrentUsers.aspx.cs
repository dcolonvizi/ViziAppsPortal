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

public partial class ViewCurrentUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;
        try
        {
            string logout_user = Request.Form.Get("logout_user");
            if (logout_user != null && logout_user.Length > 0)
            {
                SessionLogout_Click(logout_user);
            }

            DataTable table = new DataTable("Current ViziApps Users");
            table.Columns.Add("Username");
            Hashtable UsersList = (Hashtable)HttpRuntime.Cache["UsersList"];
            foreach(string key in UsersList.Keys)
            {
                string[] row_data = new string[1];
                row_data[0]= key;
                table.Rows.Add(row_data);                
            }

            DataSet dsSrc = new DataSet();
            dsSrc.Tables.Add(table);
            CurrentUsers.DataSource = dsSrc;
            CurrentUsers.DataBind();
            
            foreach (GridViewRow row in CurrentUsers.Rows)
            {
                string username = row.Cells[0].Text;
 
                ImageButton logout = (ImageButton)row.Cells[1].Controls[0];
                logout.ID = "logout_" + username;
                logout.Attributes.Add("onclick", "setUsername('" + username + "'); return confirm('Are you sure you want to logout this user?');");
            } 
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions((Hashtable)HttpRuntime.Cache[Session.SessionID], Response, ex);
        }
    }
    protected void SessionLogout_Click(string username)
    {
        Hashtable UsersList = (Hashtable)HttpRuntime.Cache["UsersList"];
        if ( ((Hashtable)HttpRuntime.Cache[Session.SessionID])["Username"] != null)
        {
            UsersList.Remove(username);
            HttpRuntime.Cache["UsersList"] = UsersList;
         }

        Message.Text = "User is logged out of session.";
    }
}
