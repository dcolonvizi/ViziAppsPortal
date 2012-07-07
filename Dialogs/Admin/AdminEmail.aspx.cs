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
using System.Text;

public partial class AdminEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Message.Text = "";
        SentUsers.Text = "";
    }

    protected void SendEmails_Click(object sender, EventArgs e)
    {
        if (EmailBody.Text.Length == 0)
        {
            Message.Text = "The email body has no text";
            return;
        }
        if (EmailSubject.Text.Length == 0)
        {
            Message.Text = "The email subject has no text";
            return;
        }
        string type = EmailType.SelectedValue;
        string sql = "";
        DB db = new DB();
        DataRow[] rows = null;
        if (type == "Production Customers")
        {
            sql = "SELECT username,email FROM customers WHERE status='active'";
            rows = db.ViziAppsExecuteSql((Hashtable)HttpRuntime.Cache[Session.SessionID], sql);
        }
        else
        {
            sql = "SELECT username,email FROM customers WHERE status!='inactive'";
            rows = db.ViziAppsExecuteSql((Hashtable)HttpRuntime.Cache[Session.SessionID], sql);
        }
        StringBuilder no_emails = new StringBuilder();
        StringBuilder sent_users = new StringBuilder();
        Email email = new Email();
        foreach (DataRow row in rows)
        {
            string username = row["username"].ToString();
            string to_email = row["email"].ToString();
            if (to_email.Length > 0)
            {
                email.SendEmail((Hashtable)HttpRuntime.Cache[Session.SessionID], ((Hashtable)HttpRuntime.Cache[Session.SessionID])["TechSupportEmail"].ToString(), to_email, "", "", EmailSubject.Text, EmailBody.Text, "",false);
                sent_users.Append(username + "\n");
            }
            else if(username!="admin" && username != "prompts")
            {
                no_emails.Append(username + "; ");
            }
        }
        if (no_emails.Length > 0)
        {
            Message.Text = "The emails were sent successfully, except for the following users: " + no_emails.ToString();
        }
        else
            Message.Text = "The emails were all sent successfully.";

        SentUsers.Text = sent_users.ToString(); 

    }
}
