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
using System.Text;
using Telerik.Web.UI;

public partial class EULAAgreement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
        if (!IsPostBack)
        {
            Instructions.Text = "Please review the following End User License Agreement and click on the button that says “I Understand and Agree” to continue or on the button that says “I Do Not Accept”.";
            string EULAFile =  State["EULAFile"].ToString();
            string path = Server.MapPath(".") + @"\" + EULAFile;

            StreamReader sr = File.OpenText(path);
            AgreementTextBox.Text = sr.ReadToEnd();
            sr.Close();
        }
    }
    protected void AgreeButton_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
 
        DB db = new DB();
        string sql = "SELECT agreed_to_eula FROM customers WHERE customer_id = '" +  State["CustomerID"].ToString() + "'";
        string agreed_to_eula = db.ViziAppsExecuteScalar((Hashtable)HttpRuntime.Cache[Session.SessionID], sql);
        if (agreed_to_eula.ToLower() == "false" || agreed_to_eula == "0")
        {
            sql = "UPDATE customers SET agreed_to_eula=true WHERE customer_id = '" +  State["CustomerID"].ToString() + "'";
            db.ViziAppsExecuteNonQuery((Hashtable)HttpRuntime.Cache[Session.SessionID], sql);
            SendEmailToSalesandCustomer(db);
        }
        db.CloseViziAppsDatabase(State);
         State["LoggedinFromEula"] = true;
        Response.Redirect("Default.aspx", false);
    }
    private void SendEmailToSalesandCustomer(DB db)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        //send an email with all the stuff
        string sql = "SELECT first_name, last_name, email, phone,referral_source,app_to_build,account_type,password FROM customers WHERE customer_id='" +  State["CustomerID"].ToString() + "'";
        DataRow[] rows = db.ViziAppsExecuteSql((Hashtable)HttpRuntime.Cache[Session.SessionID], sql);
        DataRow row = rows[0];
        string email_template_path = null;
        string body = null;

        string subject = null;
        if (!row["account_type"].ToString().Contains("google_apps"))
        {
            subject = "New Account Signup for ViziApps";
            email_template_path = Server.MapPath(".") + @"\templates\NewViziAppsSignupEmail.txt";
            body = File.ReadAllText(email_template_path)
                     .Replace("[USERNAME]", State["Username"].ToString())
                     .Replace("[FIRST_NAME]", row["first_name"].ToString())
                     .Replace("[LAST_NAME]", row["last_name"].ToString())
                     .Replace("[EMAIL]", row["email"].ToString());

            if (row["phone"] != null && row["phone"] != DBNull.Value && row["phone"].ToString().Length > 0)
                 body = body.Replace("[PHONE]", row["phone"].ToString());
            else 
                body = body.Replace("[PHONE]", "Unknown");

            if (row["referral_source"] != null && row["referral_source"] != DBNull.Value && row["referral_source"].ToString() != "unknown")
                body = body.Replace("[FOUND_BY]", row["referral_source"].ToString());
            else
                body = body.Replace("[FOUND_BY]", "Unknown");

            if (row["app_to_build"] != null && row["app_to_build"] != DBNull.Value && row["app_to_build"].ToString().Length > 0)
                body = body.Replace("[APP_TO_BUILD]", row["app_to_build"].ToString());
            else
                body = body.Replace("[APP_TO_BUILD]", "Unknown");
        }
        else if (row["account_type"].ToString().Contains("google_apps"))
        {
            subject = "New Account Signup for ViziApps From Google Apps";
            email_template_path = Server.MapPath(".") + @"\templates\NewGoogleAppsViziAppsSignupEmail.txt";
            body = File.ReadAllText(email_template_path)
                    .Replace("[USERNAME]", State["Username"].ToString())
                    .Replace("[FIRST_NAME]", row["first_name"].ToString())
                    .Replace("[LAST_NAME]", row["last_name"].ToString())
                    .Replace("[EMAIL]", row["email"].ToString());
        }

        Email email = new Email();
        email.SendEmail((Hashtable)HttpRuntime.Cache[Session.SessionID],  State["TechSupportEmail"].ToString(),  State["SalesEmail"].ToString(), "", "", subject, body.ToString(), "",true);

        string welcome_body = null;
        if ( State["LoggedInFromGoogleApps"] != null)
        {
            email_template_path = Server.MapPath(".") + @"\templates\GoogleAppsCustomerWelcomeEmail.txt";
             State["LoggedInFromGoogleApps"] = "true";
            welcome_body = File.ReadAllText(email_template_path).Replace("[NAME]", row["first_name"].ToString()).Replace("[PASSWORD]", row["password"].ToString());
        }
        else
        {
            email_template_path = Server.MapPath(".") + @"\templates\CustomerWelcomeEmail.txt";
            welcome_body = File.ReadAllText(email_template_path).Replace("[NAME]", row["first_name"].ToString());
        }
        email.SendEmail((Hashtable)HttpRuntime.Cache[Session.SessionID],  State["SalesEmail"].ToString(), row["email"].ToString(), "", "", "Welcome to ViziApps", welcome_body, "",true);
    }
    protected void RejectButton_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
        State["LoggedInFromGoogleApps"] = null;
        Response.Redirect("Default.aspx", true);

        //string javascript = "<script type='text/javascript'>CloseWindow();</script>";
        //AddJavascript(javascript);
    }
    public void AddJavascript(string javascript)
    {
        if (!ClientScript.IsStartupScriptRegistered("JSScript"))
        {
            ClientScript.RegisterStartupScript(GetType(), "JSScript", javascript);
        }
    }
}
