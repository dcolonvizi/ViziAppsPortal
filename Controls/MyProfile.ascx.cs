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
using System.Xml;

public partial class Controls_MyProfile : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        util.UpdateSessionLog(State, "post", "MyProfile");
 
         try
        {
            if( State["ServerAdminCustomerUsername"] != null)
                UsernameLabel.Text =  State["ServerAdminCustomerUsername"].ToString();
            else
                UsernameLabel.Text =  State["Username"].ToString();

            Message.Text = "";

            string sql = null;
            if ( State["Username"].ToString() != "admin")
            {
                sql = "SELECT * FROM customers WHERE customer_id='" +  State["CustomerID"].ToString() + "'";
            }
            else
            {
                sql = "SELECT * FROM customers WHERE customer_id='" +  State["ServerAdminCustomerID"].ToString() + "'";
            }
            DB db = new DB();
            DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
            DataRow row = rows[0];

            PasswordTextBox.Text = "";
            ConfirmPasswordBox.Text = "";
            CompanyTextBox.Text = SQLDecode(row["company"].ToString());
            RoleTextBox.Text = SQLDecode(row["role"].ToString());
            FirstNameTextBox.Text = SQLDecode(row["first_name"].ToString());
            LastNameTextBox.Text = SQLDecode(row["last_name"].ToString());
            StreetTextBox.Text = row["street_address"].ToString();
            CityTextBox.Text = SQLDecode(row["city"].ToString());

            if (row["state"] != null && row["state"].ToString().Length > 0)
                StateList.Text = row["state"].ToString();

            PostalCodeTextBox.Text = row["postal_code"].ToString();
            CountryTextBox.Text = SQLDecode(row["country"].ToString());

            PhoneTextbox.Text = row["phone"].ToString();
            EmailTextBox.Text = row["email"].ToString();
            string status = row["status"].ToString();

            db.CloseViziAppsDatabase(State);

            TimeZones zone_util = new TimeZones();
            string default_time_zone_delta_hours = row["default_time_zone_delta_hours"].ToString();
            zone_util.InitTimeZones(State, DateTime.Now.ToUniversalTime(), TimeZoneList, default_time_zone_delta_hours);

            string force_1_user_sessions = row["force_1_user_sessions"].ToString();
            Force1UserSessions.Checked = force_1_user_sessions == "1" || force_1_user_sessions.ToLower() == "true";
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }

    }
 
    protected void UpdateProfile_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        Message.Text = "";

        PasswordTextBox.Text = Request.Form.Get("UserAccount1$PasswordTextBox");
        ConfirmPasswordBox.Text = Request.Form.Get("UserAccount1$ConfirmPasswordBox");
        CompanyTextBox.Text = Request.Form.Get("UserAccount1$CompanyTextBox");
        RoleTextBox.Text = Request.Form.Get("UserAccount1$RoleTextBox");
        FirstNameTextBox.Text = Request.Form.Get("UserAccount1$FirstNameTextBox");
        LastNameTextBox.Text = Request.Form.Get("UserAccount1$LastNameTextBox");
        StreetTextBox.Text = Request.Form.Get("UserAccount1$StreetTextBox");
        CityTextBox.Text = Request.Form.Get("UserAccount1$CityTextBox");
        StateList.Text = Request.Form.Get("UserAccount1$StateList");
        PostalCodeTextBox.Text = Request.Form.Get("UserAccount1$PostalCodeTextBox");
        CountryTextBox.Text = Request.Form.Get("UserAccount1$CountryTextBox");
        PhoneTextbox.Text = Request.Form.Get("UserAccount1$PhoneTextbox");
        EmailTextBox.Text = Request.Form.Get("UserAccount1$EmailTextBox");
        string force_1_user_sessions = Request.Form.Get("UserAccount1$Force1UserSessions");
        Force1UserSessions.Checked = force_1_user_sessions == "on" ? true : false; 

        //validation
        if (CompanyTextBox.Text.Length > 0 && !Check.ValidateName(Message, CompanyTextBox.Text))
        {
            return;
        }
        if (RoleTextBox.Text.Length > 0 && !Check.ValidateString(Message, RoleTextBox.Text))
        {
            return;
        }
        if (FirstNameTextBox.Text.Length > 0 && !Check.ValidateName(Message, FirstNameTextBox.Text))
        {
            return;
        }
        if (LastNameTextBox.Text.Length > 0 && !Check.ValidateName(Message, LastNameTextBox.Text))
        {
            return;
        }
        if (StreetTextBox.Text.Length > 0 && !Check.ValidateText(Message, StreetTextBox.Text))
        {
            return;
        }
        if (CityTextBox.Text.Length > 0 && !Check.ValidateName(Message, CityTextBox.Text))
        {
            return;
        }
        if (PostalCodeTextBox.Text.Length > 0 && !Check.ValidateZipcode(Message, PostalCodeTextBox.Text))
        {
            return;
        }
        if (CountryTextBox.Text.Length > 0 && !Check.ValidateName(Message, CountryTextBox.Text))
        {
            return;
        }
        if (!Check.ValidatePhone(Message, PhoneTextbox.Text))
        {
            return;
        }
        if (!Check.ValidateEmail(Message, EmailTextBox.Text))
        {
            return;
        }

        StringBuilder sql = null;
        DB db = new DB();
        string username = null;
        if ( State["Username"].ToString() != "admin")
        {
            username =  State["Username"].ToString();
        }
        else
        {
            username =  State["ServerAdminUsername"].ToString();
        }

        if (PasswordTextBox.Text.Length > 0 || ConfirmPasswordBox.Text.Length > 0)
        {
            if (PasswordTextBox.Text == ConfirmPasswordBox.Text)
            {
                if (!Check.ValidatePassword(Message, PasswordTextBox.Text))
                {
                    return;
                }
                sql = new StringBuilder("UPDATE customers SET password='" + util.MySqlFilter(PasswordTextBox.Text) + "'");
                sql.Append(" WHERE username='" + username + "'");
                db.ViziAppsExecuteNonQuery(State, sql.ToString());

                sql = new StringBuilder("SELECT email from customers WHERE username='" + username + "'");
                string to_email = db.ViziAppsExecuteScalar(State, sql.ToString());

                Email email = new Email();
                StringBuilder body = new StringBuilder("\nYour ViziApps password has been changed.\n\n");

                body.Append("If you did not change it, contact our support team at support@viziapps.com right away. ");
                body.Append("\n\n - The ViziApps Team \n");

                email.SendEmail(State,  HttpRuntime.Cache["TechSupportEmail"].ToString(), to_email, "", "", "ViziApps Notice", body.ToString(), "",false);
            }
            else
            {
                Message.Text = "New password and confirmation password do not match. Your account information has not been updated";
                return;
            }
        }

        sql = new StringBuilder("UPDATE customers SET ");
        sql.Append("company='" + SQLEncode(CompanyTextBox.Text) + "'");
        sql.Append(",role='" + SQLEncode(RoleTextBox.Text) + "'");
        sql.Append(",first_name='" + SQLEncode(FirstNameTextBox.Text) + "'");
        sql.Append(",last_name='" + SQLEncode(LastNameTextBox.Text) + "'");
        sql.Append(",street_address='" + StreetTextBox.Text + "'");
        sql.Append(",city='" + SQLEncode(CityTextBox.Text) + "'");
        if (StateList.SelectedValue.IndexOf("->") < 0)
            sql.Append(",state='" + StateList.SelectedValue + "'");
        else
            sql.Append(",state=''");

        sql.Append(",postal_code='" + PostalCodeTextBox.Text + "'");
        sql.Append(",country='" + SQLEncode(CountryTextBox.Text) + "'");
        sql.Append(",phone='" + PhoneTextbox.Text + "'");
        sql.Append(",email='" + EmailTextBox.Text + "'");
        sql.Append(",default_time_zone_delta_hours='" + TimeZoneList.SelectedValue + "'");
        force_1_user_sessions = force_1_user_sessions == "on" ? "1" : "0";
        sql.Append(",force_1_user_sessions=" + force_1_user_sessions);
        sql.Append(" WHERE username='" + username + "'");
        db.ViziAppsExecuteNonQuery(State, sql.ToString());
        db.CloseViziAppsDatabase(State);

        TimeZones zone_util = new TimeZones();
        zone_util.GetDefaultTimeZone(State);

        Message.Text = "Your account profile has been updated. ";
    }
    protected string SQLEncode(string name)
    {
        return name.Replace("'", "''");
    }
    protected string SQLDecode(string name)
    {
        return name.Replace("''", "'");
    }
}
