using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Configuration;
using MySql.Data.MySqlClient;

public partial class PublishOld : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State,Response,"Default.aspx")) return;
         try
        {
            if (!IsPostBack)
              UserLabel.Text = State["Username"].ToString();

            if (State["TechSupportEmail"] != null)
            {
                util.AddEmailToButton(SupportButton, State["TechSupportEmail"].ToString(), "Email To Tech Support");
            }

            util.UpdateSessionLog(State, "post", "TabPublishOld");

            /*PurchaseButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
               "http://stores.homestead.com/MobiFlexStore/StoreFront.bok", 700, 900, false, false, false, true));
           ManageBillingButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
               "http://stores.homestead.com/MobiFlexStore/StoreFront.bok", 700, 900, false, false, false, true));*/

 
            ShowProductionServices();

            ClearMessages();

            Init init = new Init();
            if (State["ResetProvisionApps"] != null)
            {
                State["SelectedApp"] = null;
                init.InitAppsList(State, ProvisionApps);
                State["ResetProvisionApps"] = null;
            }

            if (ProvisionApps.Items.Count == 0)
            {
                init.InitAppsList(State, ProvisionApps);

            }
            SetProvisionButtons(ProvisionApps.SelectedValue);
            SetProvisionFormPopup();
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }
    }
    protected void SetProvisionFormPopup()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State["SelectedApp"] != null)
        {
            Util util = new Util();
             State["SelectedAppType"] = util.GetAppType(State);
            if ( State["SelectedAppType"] == null ||  State["SelectedAppType"].ToString() == Constants.NATIVE_APP_TYPE)
            {
                SeePublishingFormButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                    "Dialogs/Publish/PublishingForm.aspx", 900, 950, false, false, false, true));
            }
            else
            {
                SeePublishingFormButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                     "Dialogs/Publish/PublishingFormWebApps.aspx", 700, 950, false, false, false, true));
            }
        }
    }
 
    protected void ProvisionToStagingButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        util.SelectForTesting(State);
        ProvisionMessage.Text =  State["SelectedApp"].ToString() + " has been selected for testing";
        ProvisionToStagingButton.Style.Value = "display:none";
    }

    protected void ProvisionApps_SelectedIndexChanged(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        string app_name = Request.Form.Get("ProvisionApps");
        State["SelectedApp"] = app_name;
        ProvisionApps.SelectedValue = app_name;
        SetProvisionButtons(app_name);
    }
    protected void SetProvisionButtons(string app_name)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (State["SelectedApp"] == null)
        {
            util.ResetAppStateVariables(State);
            ProvisionApps.SelectedIndex = 0;
            ProvisionToStagingButton.Style.Value = "display:none";
            SeePublishingFormButton.Style.Value = "display:none"; ;

            return;
        }

         

        ClearMessages();

        if (!app_name.Contains("->"))
        {
            if (util.IsAppSelectedForTest(State))
            {
                ProvisionMessage.Text =  State["SelectedApp"].ToString() + " has been provisioned for testing";
                ProvisionToStagingButton.Style.Value = "display:none";
            }
            else 
                ProvisionToStagingButton.Style.Value = "";

             SeePublishingFormButton.Style.Value = "";
             SetProvisionFormPopup();
        }
        else
        {
             State["SelectedApp"] = null;
             State["SelectedAppType"] = null;
            util.ResetAppStateVariables(State);
            ProvisionApps.SelectedIndex = 0;
            ProvisionToStagingButton.Style.Value = "display:none";
            SeePublishingFormButton.Style.Value = "display:none";; 
        }
    }

    public void ClearMessages()
    {
        ProvisionMessage.Text = "";
    }
    protected void RefreshProductionServices_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ShowProductionServices();
    }
    protected void ProductionServices_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ShowProductionServices();
    }

    protected void ProductionServices_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ShowProductionServices();
    }

    protected void ShowProductionServices()
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        ArrayList PaidServices = util.GetPaidServices(State);
        State["PaidServices"] = PaidServices;
        if (PaidServices == null || PaidServices.Count == 0)
            return;

        DataTable ServiceTable = new DataTable();
        ServiceTable.Columns.Add("Paid Service");
        ServiceTable.Columns.Add("Associated App");
        ServiceTable.Columns.Add(" ");
        foreach (string[] PaidService in PaidServices)
        {
            DataRow row = ServiceTable.NewRow();

            row.ItemArray = PaidService;
            ServiceTable.Rows.Add(row);
        }
 
        ProductionServices.DataSource = ServiceTable;
        ProductionServices.DataBind();

        int index = 0;
        Init init = new Init();
 
         foreach (GridDataItem row in ProductionServices.Items)
        {
            string[] service = (string[])PaidServices[index];
             if (service[1] == null || service[1].Length == 0)
             {
                 RadComboBox box = new RadComboBox();
                 box.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(box_SelectedIndexChanged);
                 box.AutoPostBack = true;
                 box.ID = "ServiceApp." + index.ToString();
                 box.Width = Unit.Pixel(200);
                 init.InitAppsListNoDefault(State, box);
                 row.Cells[3].Controls.Add(box);
             }
             else
             {
                 ImageButton delete_button = new ImageButton();
                 delete_button.ImageUrl = "~/images/delete_small.gif";
                 delete_button.ID = "remove." + index.ToString();
                 delete_button.ToolTip = "Remove this service from this app";
                 delete_button.Click += new ImageClickEventHandler(delete_button_Click);
                 delete_button.Attributes.Add("onclick", "return confirm('Are you sure you want to remove this app from this service?');");
                 row.Cells[4].Controls.Add(delete_button);
             }
             index++;
        }

    }

    void delete_button_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ImageButton delete_button = (ImageButton)sender;
        int index = Convert.ToInt32(delete_button.ID.Substring(delete_button.ID.IndexOf(".") + 1));
        ArrayList services = (ArrayList)State["PaidServices"];
        string[] service = (string[])services[index];
        string app_name = service[1];
        String sku = util.GetSkuFromService(State,service[0]);
        util.RemoveAppFromProductionService(State, app_name, sku);
        ShowProductionServices();
    }

    void box_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (e.Value.Contains("->"))
            return;
        RadComboBox box =  (RadComboBox) o;
        box.SelectedValue = e.Value;
        box.Text = e.Value;
        int index = Convert.ToInt32(box.ID.Substring(box.ID.IndexOf(".") + 1));
        string app_name = e.Value;
        ArrayList services = (ArrayList)State["PaidServices"];
        string[] service = (string[])services[index];
        util.MapAppToProductionService(State, e.Value, util.GetSkuFromService(State,service[0]));
        ShowProductionServices();
    }

    protected void PurchaseButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        Response.Redirect("http://stores.homestead.com/MobiFlexStore/StoreFront.bok?user=" + State["Username"].ToString(), true);
    }
    protected void ManageBillingButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        Response.Redirect("http://stores.homestead.com/MobiFlexStore/StoreFront.bok?user=" + State["Username"].ToString(), true);
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