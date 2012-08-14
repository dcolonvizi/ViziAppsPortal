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

public partial class EmailForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
        
       try
        {
            Message.Text = "";
            ToEmail.Text = Request.QueryString.Get("email");
            EmailType.Text = Request.QueryString.Get("type");

            //fill in customers applications
            string sql = "SELECT application_name FROM applications WHERE customer_id='" +  State["CustomerID"].ToString() + "' ORDER BY application_name";
            DB db = new DB();
            DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
            ApplicationList.Items.Clear();
            if (rows != null && rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    ApplicationList.Items.Add(row["application_name"].ToString());
                }
            }
            ApplicationList.Items.Insert(0, "No Application Issue");

            sql = "SELECT email FROM customers WHERE customer_id='" +  State["CustomerID"].ToString() + "'";
            string from = db.ViziAppsExecuteScalar(State, sql);
            if (EmailType.Text == "Customer Email")
            {
                FromEmail.Text =  State["TechSupportEmail"].ToString();
            }
            else if (from == null)
            {
                FromEmail.Text = "";
            }
            else
            {
                FromEmail.Text = from;
            }
            db.CloseViziAppsDatabase(State);
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }
    }

    protected void SendEmails_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

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

        Email email = new Email();
        FromEmail.Text = Request.Form.Get("FromEmail");
        if (FromEmail.Text.Trim().Length == 0)
        {
            Message.Text = "Set the From Email address.";
            return;
        }

        StringBuilder user_info = new StringBuilder();
        string CC = "";
        if (EmailType.Text != "Customer Email")
        {
            //get user info
            string sql = "SELECT * FROM customers WHERE username='" +  State["Username"].ToString() + "'";
            DB db = new DB();
            DataRow[] rows = db.ViziAppsExecuteSql(State, sql.ToString());
            DataRow row = rows[0];
            user_info.Append("User Information" + "\r\n");
            user_info.Append("\tCustomer Username: " +  State["Username"].ToString() + "\r\n");
            user_info.Append("\tCustomer Company: " + row["company"] + "\r\n");
            user_info.Append("\tCustomer First Name: " + row["first_name"] + "\r\n");
            user_info.Append("\tCustomer Last Name: " + row["last_name"] + "\r\n");
            user_info.Append("\tCustomer Email: " + FromEmail.Text + "\r\n\r\n");
            user_info.Append("\tCustomer Phone: " + row["phone"] + "\r\n\r\n");
            user_info.Append("Issue Information" + "\r\n");
            user_info.Append("\tIssue Category: " + Category.SelectedValue + "\r\n");

            ApplicationList.SelectedValue = Request.Form.Get("ApplicationList");
            user_info.Append("\tApplication: " + ApplicationList.SelectedValue + "\r\n");
            user_info.Append("\r\n");
        }
        else
            CC =  State["TechSupportEmail"].ToString();

        user_info.Append(EmailBody.Text);
        try
        {
            string status = email.SendEmail(State,  State["TechSupportEmail"].ToString(), ToEmail.Text, CC, "", EmailSubject.Text, user_info.ToString(), "",false);
            if (status == "OK")
            {
                if (EmailType.Text == "Customer Email")
                {
                    Message.Text = "The email was sent successfully. Close this window now.";
                }
                else
                {
                    Message.Text = "The email was sent successfully. Our ViziApps Support team will respond to you shortly. Close this window now.";
                }
            }
            else
                Message.Text = "Error: " + status;
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Error: " + ex.Message;
        }

    }
}
