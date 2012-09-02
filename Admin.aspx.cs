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
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        State["CustomersByAccount"] = CustomersByAccount;
        State["CustomersByEmail"] = CustomersByEmail;

        if (!IsPostBack)
        {
            if (State["Password"] == null)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }
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
                   // ViewAllCustomers.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                   //     "Dialogs/Admin/ViewAllCustomers.aspx", 10, 10, true, true, true, true));
                    ViewActiveCustomers.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                        "Dialogs/Admin/ViewActiveCustomers.aspx", 120, 550, true, true, true, true));
                    ViewCurrentUsers.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                        "Dialogs/Admin/ViewCurrentUsers.aspx", 480, 500, true, true, true, true));
                    this.EmailCustomers.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                        "Dialogs/Admin/AdminEmail.aspx", 900, 800, true, true, true, true));
                    ShowXmlDesign.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                        "Dialogs/Admin/ShowXmlDesign.aspx", 750, 750, true, true, true, true));

                    HideForCustomers();
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

        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (e.Text.IndexOf("->") > 0)
        {
            CustomersByEmail.Items[0].Selected = true;
             AdminMessage.Text = "Select a customer and try again.";
            return;
        }
 
        State["ServerAdminCustomerUsername"] = e.Text;
        string sql = "SELECT * FROM customers WHERE username='" + e.Text + "'";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        DataRow row = rows[0];
        string customer_id = row["customer_id"].ToString();
        string email = row["email"].ToString();
        CustomersByAccount.FindItemByText(row["username"].ToString()).Selected = true;
        CustomersByEmail.FindItemByText(email).Selected = true;
        State["ServerAdminCustomerID"] = customer_id;
        Util util = new Util();
        RegisteredDateTime.Text = "Signed Up: " + row["registration_date_time"].ToString();
        LastUsedDateTime.Text = "Last used: " + row["last_use_date_time"].ToString();

        Password.Text = util.DecodeMySql(row["password"].ToString());
        AccountTypes.Text = util.DecodeMySql(row["account_type"].ToString().Replace("type=","").Replace(";",""));
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

        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (e.Text.IndexOf("->") > 0)
        {
            CustomersByAccount.Items[0].Selected = true;
            AdminMessage.Text = "Select a customer and try again.";
            return;
        }
 
        State["ServerAdminCustomerUsername"] = e.Text;
        string sql = "SELECT * FROM customers WHERE email='" + e.Text + "'";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        DataRow row = rows[0];
        string username = row["username"].ToString();
        CustomersByAccount.FindItemByText(username).Selected = true;
        CustomersByEmail.FindItemByText(row["email"].ToString()).Selected = true;
        string customer_id = row["customer_id"].ToString();
        State["ServerAdminCustomerID"] = customer_id;
        Util util = new Util();
        RegisteredDateTime.Text = "Registered: " + row["registration_date_time"].ToString();
        LastUsedDateTime.Text = "Last used: " + row["last_use_date_time"].ToString();

        AccountTypes.Text = util.DecodeMySql(row["account_type"].ToString().Replace("type=", "").Replace(";", ""));
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
       AccountPanel.Style.Value = "display:block";
       LoginPanel.Style.Value = "display:block";
       AppPanel.Style.Value = "display:block";
    }
    protected void HideForCustomers()
    {
       AccountPanel.Style.Value = "display:none";
       LoginPanel.Style.Value = "display:none";
       AppPanel.Style.Value = "display:none";
       OneAppPanel.Style.Value = "display:none";
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
            AdminMessage.Text = "Admin Customer can not be deactivated.";
        }

        else
        {
            sql = "UPDATE customers SET status='inactive' WHERE customer_id='" + customer_id + "'";
            db.ViziAppsExecuteNonQuery(State, sql);
            db.CloseViziAppsDatabase(State);
            CustomerStatus.Text = "inactive";
            AdminMessage.Text = "Customer has been deactivated.";

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
            AdminMessage.Text = "Status of Admin Customer can not be changed.";
        }
        else
        {
            sql = "UPDATE customers SET status='active' WHERE customer_id='" + customer_id + "'";
            db.ViziAppsExecuteNonQuery(State, sql);
            db.CloseViziAppsDatabase(State);
            CustomerStatus.Text = "active";
            AdminMessage.Text = "Customer has been activated.";
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
            AdminMessage.Text = "Customer can only be removed after it has been deactivated.";
        }
        else
        {
            DoRemoveCustomer(username, customer_id);
            AdminMessage.Text = "Customer has been removed.";

            HideForCustomers();

            Init init = new Init();
            init.InitApplicationCustomers(State);
            CustomerStatus.Text = "";
        }

        db.CloseViziAppsDatabase(State);
        CustomersByAccount.Items[0].Selected = true;
        CustomersByEmail.Items[0].Selected = true; 
        HideForApplications();
    }
    private void DoRemoveCustomer(string username, string customer_id)
    {
        ClearMessages();
        DB db = new DB();
        string sql = "UPDATE customers SET status='inactive' WHERE customer_id='" + customer_id + "'";
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        db.ViziAppsExecuteNonQuery(State, sql);
  
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
        OneAppPanel.Style.Value = "display:block";
    }
    protected void HideForApplications()
    {
        OneAppPanel.Style.Value = "display:none";
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
        AdminMessage.Text = "";
     }

    protected void UpdatePassword_Click(object sender, EventArgs e)
    {
        DB db = new DB();
        Util util = new Util();
        if (Password.Text.Length < 6)
        {
            AdminMessage.Text = "Passwords must 6 characters or more.";
            return;
        }
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string sql = "UPDATE customers SET password='" + util.MySqlFilter(Password.Text) + "' WHERE customer_id='" + State["ServerAdminCustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);
        db.CloseViziAppsDatabase(State);
        AdminMessage.Text = "Password has been set.";
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
        Message.Text = "Image URLs have been updated in the database."; 
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
            string From =   HttpRuntime.Cache["TechSupportEmail"].ToString();
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
    protected void UpdateAccountTypes_Click(object sender, EventArgs e)
    {
        DB db = new DB();
        Util util = new Util();
        if (AccountTypes.Text.Length == 0)
        {
            AdminMessage.Text = "Account Types cannot be empty.";
            return;
        }
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string sql = "UPDATE customers SET account_type='type=" + util.MySqlFilter(AccountTypes.Text.Trim()) + ";' WHERE customer_id='" + State["ServerAdminCustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);
        db.CloseViziAppsDatabase(State);
        AdminMessage.Text = "Account Types have been set";
    }
    protected void ViewAllCustomers_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];

        Util util = new Util();
        try
        {
            //Instantiate an instance of license and set the license file through its path

            string query = "SELECT first_name, last_name, company, email, username, phone , registration_date_time FROM customers ";

            DB db = new DB();
            DataTable myDataTable = db.GetDataTable(query);

            Grid.DataSource = myDataTable;
            Grid.DataBind();
            Grid.MasterTableView.ExportToExcel();

        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }
    }
}
