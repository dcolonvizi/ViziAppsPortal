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
using MySql.Data.MySqlClient;
using Telerik.Web.UI;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            if (State["Password"] == null)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }
            Util util = new Util();
            string status = util.LoginToViziApps(State, State["Username"].ToString(),
                  State["Password"].ToString());

            if (status != "admin")
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            ClearMessages();
            try
            {
                 State["CustomersByAccount"] = CustomersByAccount;
                 State["CustomersByEmail"] = CustomersByEmail;

                if ( State["ServerAdminCustomerID"] == null ||  State["ServerAdminCustomerID"].ToString() == "0")
                {
                     State["ServerAdminCustomerID"] = "0";
                    Init init = new Init();
                    init.InitApplicationCustomers(State);
                }
                if (!Page.IsPostBack)
                {
                    ViewUserProfile.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                        "Dialogs/Admin/ViewUserProfile.aspx", 750, 750, true, true, true, true));
                    ViewAllCustomers.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                        "Dialogs/Admin/ViewAllCustomers.aspx", 10, 10, true, true, true, true));
                    ViewActiveCustomers.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                        "Dialogs/Admin/ViewActiveCustomers.aspx", 120, 550, true, true, true, true));
                    ViewCurrentUsers.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                        "Dialogs/Admin/ViewCurrentUsers.aspx", 480, 500, true, true, true, true));
                    this.EmailCustomers.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                        "Dialogs/Admin/AdminEmail.aspx", 900, 800, true, true, true, true));
                    ShowXmlDesign.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                        "Dialogs/Admin/ShowXmlDesign.aspx", 750, 750, true, true, true, true));

                    RemoveCustomer.Attributes.Add("onclick", "return confirm('Are you sure you want to remove this customer?');");
                    DeactivateCustomer.Attributes.Add("onclick", "return confirm('Are you sure you want to deactivate this customer?');");
                    ActivateCustomer.Attributes.Add("onclick", "return confirm('Are you sure you want to activate this customer?');");
                }
            }

            catch (Exception ex)
            {
                util.ProcessMainExceptions(State, Response, ex);
            }
        }
    }

    protected void CustomersByAccount_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ClearMessages();
        HideForCustomers();

        if (e.Text.IndexOf("->") > 0)
        {
            AdminMessage.Text = "Select a customer and try again.";
            return;
        }
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];

        State["ServerAdminCustomerUsername"] = e.Text;
        string sql = "SELECT * FROM customers WHERE username='" + e.Text + "'";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        DataRow row = rows[0];
        string customer_id = row["customer_id"].ToString();
        string email = row["email"].ToString();
        CustomersByEmail.FindItemByText(email).Selected = true;
        State["ServerAdminCustomerID"] = customer_id;
        Util util = new Util();
        RegisteredDateTime.Text = "Registered: " + row["registration_date_time"].ToString();
        LastUsedDateTime.Text = "Last used: " + row["last_use_date_time"].ToString();

        string expiration_date = row["expiration_date"].ToString();
        if (expiration_date != null && expiration_date.Length > 0)
        {
            ExpirationDateMode.SelectedIndex = 1;
            DateTime expires = DateTime.Parse(expiration_date);
            this.ExpirationDate.Text = expires.ToString("d");
        }
        else ExpirationDateMode.SelectedIndex = 0;

        Password.Text = util.DecodeMySql(row["password"].ToString());
         CustomerStatus.Text = row["status"].ToString();
        if (row["email"] != null && row["email"].ToString().Length > 0)
        {
            util.AddEmailToButton(EmailCustomer, row["email"].ToString(), "Customer Email");
        }

        sql = "SELECT application_name FROM applications WHERE customer_id='" + customer_id + "' ORDER BY application_name";
        rows = db.ViziAppsExecuteSql(State, sql);
        Applications.Items.Clear();
        foreach (DataRow row1 in rows)
        {

            Applications.Items.Add(new RadComboBoxItem(row1["application_name"].ToString()));
        }
        Applications.Items.Insert(0, new RadComboBoxItem("Select ViziApps App ->"));


        db.CloseViziAppsDatabase(State);

        ShowForCustomers();
    }
    protected void CustomersByEmail_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ClearMessages();
        HideForCustomers();

        if (e.Text.IndexOf("->") > 0)
        {
            AdminMessage.Text = "Select a customer and try again.";
            return;
        }
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];

        State["ServerAdminCustomerUsername"] = e.Text;
        string sql = "SELECT * FROM customers WHERE email='" + e.Text + "'";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        DataRow row = rows[0];
        string username = row["username"].ToString();
        CustomersByAccount.FindItemByText(username).Selected = true;
        string customer_id = row["customer_id"].ToString();
        State["ServerAdminCustomerID"] = customer_id;
        Util util = new Util();
        RegisteredDateTime.Text = "Registered: " + row["registration_date_time"].ToString();
        LastUsedDateTime.Text = "Last used: " + row["last_use_date_time"].ToString();

        string expiration_date = row["expiration_date"].ToString();
        if (expiration_date != null && expiration_date.Length > 0)
        {
            ExpirationDateMode.SelectedIndex = 1;
            DateTime expires = DateTime.Parse(expiration_date);
            this.ExpirationDate.Text = expires.ToString("d");
        }
        else ExpirationDateMode.SelectedIndex = 0;

        Password.Text = util.DecodeMySql(row["password"].ToString());
        CustomerStatus.Text = row["status"].ToString();
        if (row["email"] != null && row["email"].ToString().Length > 0)
        {
            util.AddEmailToButton(EmailCustomer, row["email"].ToString(), "Customer Email");
        }

        sql = "SELECT application_name FROM applications WHERE customer_id='" + customer_id + "' ORDER BY application_name";
        rows = db.ViziAppsExecuteSql(State, sql);
        Applications.Items.Clear();
        foreach (DataRow row1 in rows)
        {

            Applications.Items.Add(new RadComboBoxItem(row1["application_name"].ToString()));
        }
        Applications.Items.Insert(0, new RadComboBoxItem("Select ViziApps App ->"));


        db.CloseViziAppsDatabase(State);

        ShowForCustomers();
    }

    protected void ShowForCustomers()
    {
        LoginToUserbyAccount.Visible = true;
        ViewUserProfile.Visible = true;
        RegisteredDateTime.Visible = true;
        LastUsedDateTime.Visible = true;
        PasswordLabel.Visible = true;
        Password.Visible = true;
        UpdatePassword.Visible = true;
        PasswordMessage.Visible = true;

        ExpirationDateMode.Visible = true;
        if (ExpirationDateMode.SelectedIndex == 1)
        {
            ExpirationDate.Visible = true;
            UpdateExpirationDate.Visible = true;
            ExpirationMessage.Visible = true;
        }
 
        DeactivateCustomer.Visible = true;
        RemoveCustomer.Visible = true;
        EmailCustomer.Visible = true;
        ActivateCustomer.Visible = true;
        ApplicationLabel.Visible = true;
        Applications.Visible = true;
        CustomerStatus.Visible = true;
        CustomerStatusLabel.Visible = true;
    }
    protected void HideForCustomers()
    {
        LoginToUserbyAccount.Visible = false;
        ViewUserProfile.Visible = false;
        RegisteredDateTime.Visible = false;
        LastUsedDateTime.Visible = false;
        PasswordLabel.Visible = false;
        Password.Visible = false;
        UpdatePassword.Visible = false;
        PasswordMessage.Visible = false;
        ExpirationDateMode.Visible = false;
        ExpirationDate.Visible = false;
        UpdateExpirationDate.Visible = false;
        ExpirationMessage.Visible = false; ;
 
        DeactivateCustomer.Visible = false;
        RemoveCustomer.Visible = false;
        EmailCustomer.Visible = false;
        ActivateCustomer.Visible = false;
        ApplicationLabel.Visible = false;
        Applications.Visible = false;
        CustomerStatus.Visible = false;
        CustomerStatusLabel.Visible = false;
        HideForApplications();
    }
    protected void DeactivateCustomer_Click(object sender, EventArgs e)
    {
        ClearMessages();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string customer_id = State["ServerAdminCustomerID"].ToString();
        if (customer_id == "0")
        {
            AdminMessage.Text = "Select a customer and try again.";
            return;
        }

        //check if admin
        string sql = "SELECT status FROM customers WHERE customer_id='" + customer_id + "'";
        DB db = new DB();
        string status = db.ViziAppsExecuteScalar(State, sql);
        if (status == "admin")
        {
            db.CloseViziAppsDatabase(State);
            ActivationMessage.Text = "Admin Customer can not be deactivated.";
        }

        else
        {
            sql = "UPDATE customers SET status='inactive' WHERE customer_id='" + customer_id + "'";
            db.ViziAppsExecuteNonQuery(State, sql);
            db.CloseViziAppsDatabase(State);
            CustomerStatus.Text = "inactive";
            ActivationMessage.Text = "Customer has been deactivated.";

        }
    }
    protected void ActivateCustomer_Click(object sender, EventArgs e)
    {
        ClearMessages();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string customer_id = State["ServerAdminCustomerID"].ToString();
        if (customer_id == "0")
        {
            AdminMessage.Text = "Select a customer and try again.";
            return;
        }

        //check if admin
        string sql = "SELECT status FROM customers WHERE customer_id='" + customer_id + "'";
        DB db = new DB();
        string status = db.ViziAppsExecuteScalar(State, sql);
        if (status == "admin")
        {
            db.CloseViziAppsDatabase(State);
            ActivationMessage.Text = "Status of Admin Customer can not be changed.";
        }
        else
        {
            sql = "UPDATE customers SET status='active' WHERE customer_id='" + customer_id + "'";
            db.ViziAppsExecuteNonQuery(State, sql);
            db.CloseViziAppsDatabase(State);
            CustomerStatus.Text = "active";
            ActivationMessage.Text = "Customer has been activated.";
        }
    }

    protected void RemoveCustomer_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string customer_id = State["ServerAdminCustomerID"].ToString();
        if (customer_id == "0")
        {
            AdminMessage.Text = "Select a customer and try again.";
            return;
        }

        string sql = "SELECT status,username FROM customers WHERE customer_id='" + customer_id + "'";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        DataRow row = rows[0];

        string status = row["status"].ToString();
        string username = row["username"].ToString();

        if (status != "inactive")
        {
            ActivationMessage.Text = "Customer can only be removed after it has been deactivated.";
        }
        else
        {
            DoRemoveCustomer(username, customer_id);
            ActivationMessage.Text = "Customer has been removed.";

            HideForCustomers();

            Init init = new Init();
            init.InitApplicationCustomers(State);
            CustomerStatus.Text = "";
        }

        db.CloseViziAppsDatabase(State);
        HideForApplications();
    }
    private void DoRemoveCustomer(string username, string customer_id)
    {
        ClearMessages();
        DB db = new DB();
        string sql = "UPDATE customers SET status='inactive' WHERE customer_id='" + customer_id + "'";
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        db.ViziAppsExecuteNonQuery(State, sql);
  
        //remove files
        Util util = new Util();
        string home = util.GetApplicationHome(State);

        //remove all apps in application production folder
        string app_directory = home + @"\customer_media\" +username;
        if (Directory.Exists(app_directory))
            Directory.Delete(app_directory, true);


        sql = "SELECT application_id FROM applications WHERE customer_id='" + customer_id + "'";
        DataRow[] rows3 = db.ViziAppsExecuteSql(State, sql);
        foreach (DataRow row3 in rows3)
        {
            string application_id = row3["application_id"].ToString();
            sql = "DELETE FROM application_pages WHERE application_id='" + application_id + "'";
            db.ViziAppsExecuteNonQuery(State, sql);
        }
        sql = "DELETE FROM applications WHERE customer_id='" + customer_id + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        sql = "DELETE FROM customers WHERE status='inactive' AND customer_id='" + customer_id + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        db.CloseViziAppsDatabase(State);
        CustomersByAccount.SelectedIndex = 0;
    }
    protected void Applications_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ClearMessages();
        //get initial values
        if (e.Text.IndexOf("->") > 0)
        {
            HideForApplications();
            return;
        }

        ShowForApplications();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string customer_id = State["ServerAdminCustomerID"].ToString();
        Util util = new Util();

        State["SelectedAdminApp"] = e.Text;
        string sql = "SELECT * FROM applications WHERE customer_id='" + customer_id + "' AND application_name='" + e.Text + "'";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        string status = "";
        DataRow row = rows[0];
        string application_id = row["application_id"].ToString();

         State["application_id"] = application_id;

        status = row["status"].ToString();
        ApplicationStatus.Text = status;
        db.CloseViziAppsDatabase(State);
    }

    protected void ShowForApplications()
    {
        ApplicationStatus.Visible = true;
        ApplicationStatusLabel.Visible = true;
        ShowXmlDesign.Visible = true;
    }
    protected void HideForApplications()
    {
        ApplicationStatus.Visible = false;
        ApplicationStatusLabel.Visible = false;
        ShowXmlDesign.Visible = false;
     }

 
    private bool DatabaseExists(string server, string database_name, string database_user, string database_password)
    {
        try
        {
            MySqlConnection CallListDB = new MySqlConnection();
            CallListDB.ConnectionString = "SERVER=" + server + ";USER ID=" + database_user + ";PASSWORD=" + database_password + ";DATABASE=" + database_name + ";";
            CallListDB.Open();
            CallListDB.Close();
            return true;
        }
        catch
        {
            return false;
        }

    }

    private string GetXmlNodeInnerText(XmlNode parentNode, string xPath)
    {

        XmlNode valueNode = parentNode.SelectSingleNode(xPath);

        if (valueNode != null)
        {
            return valueNode.InnerText;
        }
        else
        {
            return string.Empty;
        }
    }

 

    private void ClearMessages()
    {
        Message.Text = "";
        ActivationMessage.Text = "";
        AdminMessage.Text = "";
        PasswordMessage.Text = "";
        ExpirationMessage.Text = "";
    }

    protected void UpdatePassword_Click(object sender, EventArgs e)
    {
        DB db = new DB();
        Util util = new Util();
        if (Password.Text.Length < 6)
        {
            PasswordMessage.Text = "Passwords must 6 characters or more.";
            return;
        }
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string sql = "UPDATE customers SET password='" + util.MySqlFilter(Password.Text) + "' WHERE customer_id='" + State["ServerAdminCustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);
        db.CloseViziAppsDatabase(State);
        PasswordMessage.Text = "Password has been set.";
    }
    protected void ExpirationDateMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ExpirationDateMode.SelectedIndex == 1)
        {
            ExpirationDate.Visible = true;
            UpdateExpirationDate.Visible = true;
        }
        else
        {
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            string sql = "UPDATE customers SET expiration_date=NULL WHERE customer_id='" + State["ServerAdminCustomerID"].ToString() + "'";
            DB db = new DB();
            db.ViziAppsExecuteNonQuery(State, sql);
            db.CloseViziAppsDatabase(State);
            ExpirationMessage.Text = "Customer account has been set to never expire.";
            ExpirationDate.Visible = false;
            UpdateExpirationDate.Visible = false;
        }
    }
    protected void UpdateExpirationDate_Click(object sender, EventArgs e)
    {
        DB db = new DB();

        if (!Check.ValidateDateTime(ExpirationMessage, ExpirationDate.Text))
        {
            ExpirationMessage.Text = "Expiration date is not valid. " + ExpirationMessage.Text;
            return;
        }
        DateTime expires = DateTime.Parse(ExpirationDate.Text);
        if (expires <= DateTime.Now.ToUniversalTime())
        {
            ExpirationMessage.Text = "Expiration date must be in the future.";
            return;
        }
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string sql = "UPDATE customers SET expiration_date='" + expires.ToString("s").Replace("T", " ") + "' WHERE customer_id='" + State["ServerAdminCustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);
        db.CloseViziAppsDatabase(State);
        ExpirationMessage.Text = "Expiration date has been set.";
    }
    protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Default.aspx", false);
        //Response.End();
    }
    protected void LoginToUserbyAccount_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (Password.Text.Length > 0)
        {
             State["Username"] = CustomersByAccount.Text;
             State["Password"] = Password.Text;
        }
        else
             State["LoggedInFromGoogleApps"] = Util.Encrypt(CustomersByAccount.Text,"mobiflex1");

         State["LoggedInFromAdmin"] = true;
        Response.Redirect("Default.aspx", false);
    }
    protected void LoginToUserbyEmail_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (Password.Text.Length > 0)
        {
            State["Username"] = CustomersByEmail.Text;
            State["Password"] = Password.Text;
        }
        else
            State["LoggedInFromGoogleApps"] = Util.Encrypt(CustomersByAccount.Text, "mobiflex1");

        State["LoggedInFromAdmin"] = true;
        Response.Redirect("Default.aspx", false);
    }
    protected void UpdateImageListing_Click(object sender, EventArgs e)
    {
        AmazonS3 util = new AmazonS3();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        ArrayList image_list = util.GetStockImageUrls(State);
        DB db = new DB();
        db.ViziAppsExecuteNonQuery(State, "DELETE FROM stock_images");

        foreach (String url in image_list)
        {
            int start = url.IndexOf("apps/images/") + 12;
            string type = url.Substring(start,url.IndexOf("/",start) - start);
            string sql = "INSERT INTO stock_images (image_url,type) VALUES ('" + url + "','" + type + "')";
            db.ViziAppsExecuteNonQuery(State, sql);

        }
        db.CloseViziAppsDatabase(State);
        AdminMessage.Text = "Image URLs have been updated in the database."; 
    }
    protected void EmailUpgradeNotice_Click(object sender, EventArgs e)
    {
        Hashtable UsersList = (Hashtable)HttpRuntime.Cache["UsersList"];
        DB db = new DB();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        foreach (string username in UsersList.Keys)
        {
            string To = db.ViziAppsExecuteScalar(State, "SELECT email FROM customers WHERE username='" + username + "'");
            Email email = new Email();
            string From =  State["TechSupportEmail"].ToString();
            string Body = "The ViziApps Studio will be down in 1 minute for 5 minutes for an upgrade maintenance.\n\nSorry for the inconvenience.\n\n--ViziApps Support";
            string status = email.SendEmail(State, From, To, "", "", "ViziApps Studio Maintenance Notice", Body, "",false);
            if (status.IndexOf("OK") < 0)
            {
                Message.Text = "There was a problem sending the emails: " + status;
                db.CloseViziAppsDatabase(State);
                return;
            }
        }
        db.CloseViziAppsDatabase(State);
        Message.Text = "Maintenance notice has been emailed to " + UsersList.Keys.Count.ToString() + " current users";
    }  
    
}
