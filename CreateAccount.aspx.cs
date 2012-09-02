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
using System.IO;
using System.Xml;

public partial class CreateAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageLabel.Text = "";
    }
    protected void CreateAccountSubmit_ServerClick(object sender, EventArgs e)
    {
        //check for competitors
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string address = EmailTextBox.Text.ToLower();
        string bad_domains = Server.MapPath(".") + @"\App_Data\BadDomains.txt";
        string[] lines = File.ReadAllLines(bad_domains);
        foreach(string line in lines)
        {
            if (address.EndsWith(line))
            {
                MessageLabel.Text = "An email has been sent to you to complete your registration. Please follow the directions in the email.";
                return;
            }
        }
        
        Util util = new Util();
        DB db = new DB();

        Label Error = new Label();
        StringBuilder err = new StringBuilder();
        string username = UsernameBox.Text.Trim().ToLower();
        if (!Check.ValidateUsername(Error, username))
        {
            err.Append(Error.Text.Clone() + "<BR>");
        }
        else
        {
            string query = "SELECT username FROM customers WHERE username='" + username + "'";
            string prev_username = db.ViziAppsExecuteScalar(State,query);
            if (username == prev_username)
            {
               /* query = "SELECT password FROM customers WHERE username='" + username + "'";
                string password = db.ViziAppsExecuteScalar(State, query);
                if(password != PasswordTextBox.Text)*/
                     err.Append("The " + username + " account already exists.<BR>");
            }
            if (address.Length> 0 && address.ToLower() != "michael@viziapps.com") //for every email not for testing
            {
                query = "SELECT email FROM customers WHERE email='" + address + "'";
                string email = db.ViziAppsExecuteScalar(State, query);
                if (email == this.EmailTextBox.Text)
                {
                    err.Append("An account already exists with the same email.<BR>");
                }
            }
        }
        if (!Check.ValidatePassword(Error, PasswordTextBox.Text))
        {
            err.Append("Enter Password: " +Error.Text.Clone() + "<BR>");
        }
        if (!Check.ValidateEmail(Error, EmailTextBox.Text))
        {
            err.Append(Error.Text.Clone() + "<BR>");
        }
        if (PasswordTextBox.Text != ConfirmPasswordBox.Text)
        {
            err.Append("The password and confirming password do not match. Try again.<BR>");
        }
        if (!Check.ValidateName(Error,FirstNameTextBox.Text))
        {
            err.Append("Enter First Name: " + Error.Text.Clone() + "<BR>");
        }
        if (!Check.ValidateName(Error, LastNameTextBox.Text))
        {
            err.Append("Enter Last Name: " + Error.Text.Clone() + "<BR>");
        }
 
        string phone = PhoneTextBox.Text.Trim ();
        if (PhoneTextBox.Text.Length > 0) //optional field
        {
            if (!Check.ValidatePhone(Error, PhoneTextBox.Text))
            {
                err.Append("Enter a valid phone number: " + Error.Text.Clone() + "<BR>");
            }
        }
        if (err.Length > 0)
        {
            MessageLabel.Text = "The following input(s) are required:<BR>" + err.ToString();
            db.CloseViziAppsDatabase(State);
            return;
        }
        try
        {
            
            string account_type = "type=viziapps;"; //set default for now
            string security_question = "";
            string security_answer = "";

            string customer_id = util.CreateMobiFlexAccount(State, username, PasswordTextBox.Text.Trim(), security_question, security_answer, FirstNameTextBox.Text.Trim(), LastNameTextBox.Text.Trim(),
                    EmailTextBox.Text.ToLower().Trim(), phone, account_type, ReferralSourceList.SelectedValue,AppToBuild.Text, "inactive");

            string email_template_path = Server.MapPath(".") + @"\templates\EmailValidation.txt";
            string url =   HttpRuntime.Cache["PublicViziAppsUrl"].ToString() + "/ValidateEmail.aspx?id=" + customer_id;
            string from =   HttpRuntime.Cache["TechSupportEmail"].ToString();
            string body = File.ReadAllText(email_template_path)
                    .Replace("[NAME]", FirstNameTextBox.Text.Trim())
                    .Replace("[LINK]",url)
                    .Replace("[SUPPORT]",from);

            Email email = new Email(); 
            string status = email.SendEmail(State, from, EmailTextBox.Text, "", "", "ViziApps Registration", body, "",true);
            if (status.IndexOf("OK") >= 0)
            {
                MessageLabel.Text = "An email has been sent to you to complete your registration. Please follow the directions in the email.";
            }
            else
            {
                MessageLabel.Text = status;
                //problem with email : delete account
                string sql = "DELETE FROM customers WHERE username='" + username + "'";
                db.ViziAppsExecuteNonQuery(State, sql);
            }
            db.CloseViziAppsDatabase(State);
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            MessageLabel.Text = ex.Message + ": " + ex.StackTrace;
            db.CloseViziAppsDatabase(State);
            return;
        }
    }
    
}
