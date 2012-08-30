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
using Telerik.Web.UI;

public partial class MyProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State,Response,"Default.aspx")) return;
        try
        {
            if (!IsPostBack)
            {
                UserLabel.Text = State["Username"].ToString();
            }

            if (State["TechSupportEmail"] != null)
            {
                util.AddEmailToButton(SupportButton, State["TechSupportEmail"].ToString(), "Email To Tech Support");
            }

            util.UpdateSessionLog(State, "post", "TabMyProfile");
  
            if (State["ServerAdminCustomerUsername"] != null)
                UsernameLabel.Text = State["ServerAdminCustomerUsername"].ToString();
            else
                UsernameLabel.Text = State["Username"].ToString();

            Message.Text = "";

            string sql = null;
            if (State["Username"].ToString() != "admin")
            {
                sql = "SELECT * FROM customers WHERE customer_id='" + State["CustomerID"].ToString() + "'";
            }
            else
            {
                sql = "SELECT * FROM customers WHERE customer_id='" + State["ServerAdminCustomerID"].ToString() + "'";
            }
            DB db = new DB();
            DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
            DataRow row = rows[0];

            PasswordTextBox.Text = "";
            ConfirmPasswordBox.Text = "";
            CompanyTextBox.Text = util.DecodeMySql(row["company"].ToString());
            RoleTextBox.Text = util.DecodeMySql(row["role"].ToString());
            FirstNameTextBox.Text = util.DecodeMySql(row["first_name"].ToString());
            LastNameTextBox.Text = util.DecodeMySql(row["last_name"].ToString());
            StreetTextBox.Text = util.DecodeMySql(row["street_address"].ToString());
            CityTextBox.Text = util.DecodeMySql(row["city"].ToString());


            if (row["state"] != null && row["state"].ToString().Length > 0)
                StateList.Text = row["state"].ToString();

            PostalCodeTextBox.Text = row["postal_code"].ToString();
            CountryTextBox.Text = util.DecodeMySql(row["country"].ToString());

            PhoneTextbox.Text = row["phone"].ToString();
            EmailTextBox.Text = row["email"].ToString();
            string status = row["status"].ToString();

            
            //Additions for the CC fields 
            if (!IsPostBack)
            {
                CCFirstNameTextbox.Text = util.DecodeMySql(row["first_name"].ToString());
                CCLastNameTextBox.Text = util.DecodeMySql(row["last_name"].ToString());
                CCZipTextBox.Text = row["postal_code"].ToString();
            }


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
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        Message.Text = "";

        PasswordTextBox.Text = Request.Form.Get("PasswordTextBox");
        ConfirmPasswordBox.Text = Request.Form.Get("ConfirmPasswordBox");
        CompanyTextBox.Text = Request.Form.Get("CompanyTextBox");
        RoleTextBox.Text = Request.Form.Get("RoleTextBox");
        FirstNameTextBox.Text = Request.Form.Get("FirstNameTextBox");
        LastNameTextBox.Text = Request.Form.Get("LastNameTextBox");
        StreetTextBox.Text = Request.Form.Get("StreetTextBox");
        CityTextBox.Text = Request.Form.Get("CityTextBox");
        StateList.Text = Request.Form.Get("StateList");
        PostalCodeTextBox.Text = Request.Form.Get("PostalCodeTextBox");
        CountryTextBox.Text = Request.Form.Get("CountryTextBox");
        PhoneTextbox.Text = Request.Form.Get("PhoneTextbox");
        EmailTextBox.Text = Request.Form.Get("EmailTextBox");
        string force_1_user_sessions = Request.Form.Get("Force1UserSessions");
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
        if (State["Username"].ToString() != "admin")
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

                email.SendEmail(State,  State["TechSupportEmail"].ToString(), to_email, "", "", "ViziApps Notice", body.ToString(), "",false);
            }
            else
            {
                Message.Text = "New password and confirmation password do not match. Your account information has not been updated";
                return;
            }
        }

        sql = new StringBuilder("UPDATE customers SET ");
        sql.Append("company='" + util.MySqlFilter(CompanyTextBox.Text) + "'");
        sql.Append(",role='" + util.MySqlFilter(RoleTextBox.Text) + "'");
        sql.Append(",first_name='" + util.MySqlFilter(FirstNameTextBox.Text) + "'");
        sql.Append(",last_name='" + util.MySqlFilter(LastNameTextBox.Text) + "'");
        sql.Append(",street_address='" + util.MySqlFilter(StreetTextBox.Text) + "'");
        sql.Append(",city='" + util.MySqlFilter(CityTextBox.Text) + "'");
        if (StateList.SelectedValue.IndexOf("->") < 0)
            sql.Append(",state='" + StateList.SelectedValue + "'");
        else
            sql.Append(",state=''");

        sql.Append(",postal_code='" + PostalCodeTextBox.Text + "'");
        sql.Append(",country='" + util.MySqlFilter(CountryTextBox.Text) + "'");
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

        //Update with CheddarGetter the CreditCardDetails if the Checkbox for CreditCardUpdate is checked.
        if (Update_CC_Details_CheckBox.Checked)
        {
            if (UpdateCheddarGetterWithCC() == true)
                Message.Text = "Your account profile has been updated. ";
            else
                Message.Text = "There was a problem updating your credit card info. Please contact support@viziapps.com for assistance.";
        }
        else
            Message.Text = "Your account profile has been updated. ";
        //End CC Update
        
    }



    private bool UpdateCheddarGetterWithCC()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        
        //Get all paid_apps from paid_services table directly.
        string sql = "SELECT application_id FROM paid_services WHERE customer_id='" + State["CustomerID"].ToString() + "' AND status='paid' ORDER BY app_name";

        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);

        bool status = false;
        foreach (DataRow row in rows)
        {
            string AppID = row["application_id"].ToString();
            status = UpdateCheddarGetterPerApp(AppID);
        }
        return status;
}



    private bool UpdateCheddarGetterPerApp(string AppID)
    {

        try
        {
            CGError servererror = new CGError();
            Customer customer_details = CheddarGetter.GetCustomer(AppID, servererror);

            if (String.IsNullOrEmpty(servererror.Code) == false)
            {
                RadNotification1.Title = "WARNING";
                RadNotification1.Text = servererror.Message + " . Please contact support@viziapps.com for assistance";
                RadNotification1.Visible = true;
                RadNotification1.Show();
                return false;
            }


            CustomerPost updateCustomer = new CustomerPost();

            //Copy over the plan since we are not changing that.
            updateCustomer.strPlanCode = customer_details.Subscriptions[0].SubscriptionsPlans[0].Code;
            

            updateCustomer.Company = CompanyTextBox.Text;
            updateCustomer.FirstName = FirstNameTextBox.Text;
            updateCustomer.LastName = LastNameTextBox.Text;
            updateCustomer.Email = EmailTextBox.Text;

            //Extra fields required for nonFREE plans
            updateCustomer.CCFirstName = CCFirstNameTextbox.Text;
            updateCustomer.CCLastName = CCLastNameTextBox.Text;
            updateCustomer.CCNumber = CCNumberTextBox.Text;
            updateCustomer.CCExpiration = CCExpirationTextBox.Text;
            updateCustomer.CCZip = CCZipTextBox.Text;
            updateCustomer.CCCardCode = CCCardCodeTextBox.Text;

            updateCustomer.Code = AppID;
            System.Diagnostics.Debug.WriteLine("Updating Customerinfo for AppID=" + AppID);

            //Send it to the server
            Customer returnCustomer = CheddarGetter.UpdateCustomerAndSubscription(updateCustomer, servererror);
            //FAILURE
            if (String.IsNullOrEmpty(servererror.Code) == false)
            {
                RadNotification1.Title = "WARNING";
                RadNotification1.Text = servererror.Message + " . Please contact support@viziapps.com for assistance";
                RadNotification1.Visible = true;
                RadNotification1.Show();
                return false;
            }

            //SUCCESS
            if (String.IsNullOrEmpty(returnCustomer.Code) == false)
                return true;

        }

        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            throw;
        }
        return false;
    }


     protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        string account_type = util.GetAccountType(State);
        util.Logout(State);
        if (account_type != null && account_type.Contains("google_apps"))
            Response.Redirect("LogoutForGoogleApps.aspx", false);
        else
            Response.Redirect("Default.aspx", false);

    }
    protected void TabMenu_ItemClick(object sender, RadMenuEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        string tab = e.Item.Value;
        Session["MainMenu"] = tab;
         Response.Redirect("Tab" + tab + ".aspx", false);
    }
            
}