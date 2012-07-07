using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Collections;
using System.Text;
using Telerik.Web.UI;

/// <summary>
/// Summary description for Init
/// </summary>
public class Init
{

    public Init()
	{
	}
    public void InitAccountList(Hashtable State, RadComboBox Accounts, bool Initialize)
    {
        string sql = "SELECT username FROM customers ORDER BY username";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        Accounts.Items.Clear();
        int index = 0;
        foreach (DataRow row in rows)
        {
            string username = row["username"].ToString();
            Accounts.Items.Add(new RadComboBoxItem(username,username));
            if (Initialize)
            {
                if (username == State["Username"].ToString())
                    Accounts.SelectedIndex = index;
                index++;
            }
        }
        if (!Initialize)
            Accounts.Items.Insert(0, new RadComboBoxItem("Select Account ->","Select Account ->"));

        db.CloseViziAppsDatabase(State);
    }

    public void InitSiteConfigurations(HttpRequest Request, Hashtable State)
    {
        State["RequestUrlHost"] = Request.Url.Host;
        InitSiteConfigurations(State);
    }
    public void InitSiteConfigurations(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT configuration,value FROM site_configuration";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        foreach (DataRow row in rows)
        {
            string configuration = row["configuration"].ToString();
            string value = row["value"].ToString();
            State[configuration] = value;
        }
        db.CloseViziAppsDatabase(State);

        if (State["OEMConfigs"] != null)
        {
            DataRow oem = (DataRow)State["OEMConfigs"];
            string support_email = oem["support_email"].ToString();
            if (support_email != null && support_email.Length > 0)
                State["TechSupportEmail"] = support_email;
            string sales_email = oem["sales_email"].ToString();
            if (sales_email != null && sales_email.Length > 0)
                State["SalesEmail"] = sales_email;
       }
     }
    public void InitSkuConfigurations(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT configuration,value FROM site_configuration WHERE configuration='AndroidSubmitServiceSku' OR configuration='iOSSubmitServiceSku'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        foreach (DataRow row in rows)
        {
            string configuration = row["configuration"].ToString();
            string value = row["value"].ToString();
            State[configuration] = value;
        }
        db.CloseViziAppsDatabase(State);

    }
    public void InitApplicationCustomers(Hashtable State)
    {
        RadComboBox CustomersByAccount = (RadComboBox)State["CustomersByAccount"];
        if (CustomersByAccount == null)
            return;

        string sql = "SELECT username FROM customers ORDER BY username";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        CustomersByAccount.Items.Clear();
        foreach (DataRow row in rows)
        {
            CustomersByAccount.Items.Add(new RadComboBoxItem(row["username"].ToString()));
        }
        CustomersByAccount.Items.Insert(0, new RadComboBoxItem("Select Customer ->"));

        RadComboBox CustomersByEmail = (RadComboBox)State["CustomersByEmail"];

        sql = "SELECT email FROM customers ORDER BY email";
        rows = db.ViziAppsExecuteSql(State, sql);
        CustomersByEmail.Items.Clear();
        foreach (DataRow row in rows)
        {
            CustomersByEmail.Items.Add(new RadComboBoxItem(row["email"].ToString()));
        }
        CustomersByEmail.Items.Insert(0, new RadComboBoxItem("Select Customer ->"));
        db.CloseViziAppsDatabase(State);
    }
    public void InitAppsList(Hashtable State, RadComboBox AppsList)
    {
        if (AppsList == null)
            return;

        string sql = "SELECT DISTINCT application_name FROM applications WHERE customer_id='" + State["CustomerID"].ToString() + "' ORDER BY application_name";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        AppsList.Items.Clear();
        foreach (DataRow row in rows)
        {
            string app_name = row["application_name"].ToString();
            AppsList.Items.Add(new RadComboBoxItem(app_name, app_name));
            if (State["SelectedApp"] != null && app_name == State["SelectedApp"].ToString())
            {
                AppsList.Items[AppsList.Items.Count-1].Selected = true;
            }
        }
        AppsList.Items.Insert(0, new RadComboBoxItem("Select App ->", "Select App ->"));
    }
    public void InitManageDataAppsList(Hashtable State)
    {
        RadComboBox AppsList = (RadComboBox)State["ManageDataApps"];

        if (AppsList == null)
            return;

        string sql = "SELECT DISTINCT application_name FROM applications WHERE application_type!='" + Constants.WEB_APP_TYPE + "' AND customer_id='" + State["CustomerID"].ToString() + "' ORDER BY application_name";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        AppsList.Items.Clear();
        foreach (DataRow row in rows)
        {
            string app_name = row["application_name"].ToString();
            AppsList.Items.Add(new RadComboBoxItem(app_name, app_name));
            if (State["SelectedApp"] != null && app_name == State["SelectedApp"].ToString())
            {
                AppsList.Items[AppsList.Items.Count - 1].Selected = true;
            }
        }
        AppsList.Items.Insert(0, new RadComboBoxItem("Select App ->", "Select App ->"));
    }
    public void InitAppsList(Hashtable State, RadComboBox AppsList, string customer_id)
    {
        if (AppsList == null)
            return;

        string sql = "SELECT DISTINCT application_name FROM applications WHERE customer_id='" + customer_id + "' ORDER BY application_name";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        AppsList.Items.Clear();
        foreach (DataRow row in rows)
        {
            string app_name = row["application_name"].ToString();
            AppsList.Items.Add(new RadComboBoxItem(app_name, app_name));
        }
        AppsList.Items.Insert(0, new RadComboBoxItem("Select App ->", "Select App ->"));
        AppsList.Items[0].Selected = true;
    }
    public void InitAppsListNoDefault(Hashtable State, RadComboBox AppsList)
    {
        if (AppsList == null)
            return;

        string sql = "SELECT DISTINCT application_name FROM applications WHERE customer_id='" + State["CustomerID"].ToString() + "' ORDER BY application_name";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        AppsList.Items.Clear();
        foreach (DataRow row in rows)
        {
            string app_name = row["application_name"].ToString();
            AppsList.Items.Add(new RadComboBoxItem(app_name, app_name));
         }
        AppsList.Items.Insert(0, new RadComboBoxItem("Select App ->", "Select App ->"));
    }
    
    public void InitTemplateList(Hashtable State, RadComboBox TemplateAppsList, Label Description)
    {
        if (TemplateAppsList == null)
            return;

        DB db = new DB();
        string template_account = null;
        if (State["Username"].ToString() == State["TemplatesAccount"].ToString())
        {
            template_account = State["DevAccount"].ToString();
            State["TemplatesAccount"] = template_account;
        }
        else
            template_account = State["TemplatesAccount"].ToString();

        string sql = "SELECT customer_id FROM customers WHERE username='" + template_account + "'";
        string customer_id = db.ViziAppsExecuteScalar(State, sql);

        sql = "SELECT application_name,description FROM applications WHERE customer_id='" + customer_id + "' ORDER BY application_name";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        TemplateAppsList.Items.Clear();
        bool is_first = true;
        foreach (DataRow row in rows)
        {
            string app_name = row["application_name"].ToString();
            TemplateAppsList.Items.Add(new RadComboBoxItem(app_name, app_name));
            if (is_first)
            {
                is_first = false;
                TemplateAppsList.SelectedIndex = 0;
                //State["SelectedTemplateApp"] = TemplateAppsList.SelectedValue;
                Description.Text = row["description"].ToString();
            }
        }
    }
    public void InitAppsList(Hashtable State, DropDownList AppsList)
    {
        if (AppsList == null)
            return;
                
        string sql = "SELECT DISTINCT application_name FROM applications WHERE customer_id='" + State["CustomerID"].ToString() + "' ORDER BY application_name";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        AppsList.Items.Clear();
        foreach (DataRow row in rows)
        {
            AppsList.Items.Add(row["application_name"].ToString());
        }
        AppsList.Items.Insert(0, "Select Application ->");

        if (State["SelectedApp"] != null)
        {
            AppsList.SelectedValue = State["SelectedApp"].ToString();
        }
    }
 

    public void InitAppPages(Hashtable State, RadComboBox AppPages, bool includeCurrentPage)
    {
        AppPages.Items.Clear();

        if(State["SelectedApp"] == null || State["SelectedApp"].ToString().Contains("->"))
            return;

        XmlUtil x_util = new XmlUtil();
        string[] pages = x_util.GetAppPageNames(State, State["SelectedApp"].ToString());

        foreach (string page in pages)
        {
            if (!includeCurrentPage && State["SelectedAppPage"] != null && State["SelectedAppPage"].ToString() == page)
                continue;

            AppPages.Items.Add(new RadComboBoxItem(page, page));
        }
        AppPages.SelectedIndex = 0;
    }
    public void InitAppPagesAndCustom(Hashtable State, RadComboBox AppPages, bool includeCurrentPage)
    {
        AppPages.Items.Clear();
        AppPages.EmptyMessage = "Choose Field or Enter Page Name";

        if(State["SelectedApp"] == null || State["SelectedApp"].ToString().Contains("->"))
            return;

        XmlUtil x_util = new XmlUtil();
        string[] pages = x_util.GetAppPageNames(State, State["SelectedApp"].ToString());

        foreach (string page in pages)
        {
            if (!includeCurrentPage && State["SelectedAppPage"] != null && State["SelectedAppPage"].ToString() == page)
                continue;

            AppPages.Items.Add(new RadComboBoxItem(page, page));
        }
       
    }

    

    public void InitReportApps(Hashtable State)
    {
        DropDownList ReportApps = (DropDownList)State["ReportApps"];
        if (ReportApps == null)
            return;

        string sql = "SELECT DISTINCT application_name FROM applications WHERE status!='configuration' AND customer_id='" + State["CustomerID"].ToString() + "' ORDER BY application_name";
        DB db = new DB();
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        ReportApps.Items.Clear();
        foreach (DataRow row in rows)
        {
            ReportApps.Items.Add(row["application_name"].ToString());
        }
        ReportApps.Items.Insert(0, "Select ViziApps App ->");
        db.CloseViziAppsDatabase(State);

        if (State["SelectedReportsApp"] != null)
        {
            ReportApps.SelectedValue = State["SelectedReportsApp"].ToString();
        }

    }
}
