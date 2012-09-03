using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Data;

public partial class CancelPublishingService : System.Web.UI.Page
{
       
        // Data members
        private string AppType = "";
        private string AppID = "";
        private string AppSKU = "";
        private static String AppCGCustomerCode = "";
        private bool plancancelled = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            Util util = new Util();
            Init init = new Init();
            
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];

            try
            {

                if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }
                {

                    if (!Page.IsPostBack)
                    {
                        cgbilling.Visible = false;
                        CancelSubscriptionsButton.Visible = false;


                        if (InitAppsList(State, RadComboAppSelector) == false)
                        {
                            //There are no Apps to cancel
                            RadNotification1.Title = "WARNING";
                            RadNotification1.Text = "There are no App Subscriptions which can be cancelled.";
                            RadNotification1.Visible = true;
                            RadNotification1.Show();
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                util.ProcessMainExceptions(State, Response, ex);
            }
        }//PageLoad

        //Init the ComboBox with the specific check required for BillingHistory
        private bool InitAppsList(Hashtable State, RadComboBox AppsList)
        {
            Util util = new Util();
            try
            {

                if (AppsList == null)
                    return false;



                //Get only paid_apps from paid_services table directly.
                string sql = "SELECT app_name FROM paid_services WHERE customer_id='" + State["CustomerID"].ToString() + "' AND status='paid' AND sku != '" +  HttpRuntime.Cache["iOSSubmitServiceSku"].ToString() + "' AND  sku != '" +  HttpRuntime.Cache["AndroidSubmitServiceSku"].ToString() + "' ORDER BY app_name"; 
                //string sql = "SELECT app_name FROM paid_services WHERE customer_id='" + State["CustomerID"].ToString() + "' AND status='paid' ORDER BY app_name";


                DB db = new DB();
                DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
                AppsList.Items.Clear();
                foreach (DataRow row in rows)
                {
                    string app_name = row["app_name"].ToString();
                    AppsList.Items.Add(new RadComboBoxItem(app_name, app_name));
                }

                if (AppsList.IsEmpty)
                    return false;

                AppsList.Items.Insert(0, new RadComboBoxItem("Select App ->", "Select App ->"));
                AppsList.Items[0].Selected = true;

                return true;
            }
            catch (Exception ex)
            {
                util.ProcessMainExceptions(State, Response, ex);
            }
            return false;
        }


        private void page_refresh()
        {
            try
            {
                Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
                string tempInnerHTML = "";
                string CancelInfo = "";

                AppID = State["application_id"].ToString();

                if (String.IsNullOrEmpty(AppID) == false)
                {
                    CGError servererror = new CGError();
                    Customer customer_details = CheddarGetter.GetCustomer(AppID, servererror);

                    if (String.IsNullOrEmpty(servererror.Code) == false)
                    {
                        cgbilling.Visible = false;
                        CancelSubscriptionsButton.Visible = false;

                        RadNotification1.Title = "WARNING";

                        if (servererror.Code.IndexOf("NotFound") >= 0)
                            RadNotification1.Text = "No publishing service found for the App";
                        else
                            RadNotification1.Text = servererror.Message;

                        RadNotification1.Visible = true;
                        RadNotification1.Show();

                        return;
                    }

                    cgbilling.Visible = true;


                    AppCGCustomerCode = customer_details.Code;

                    int sub = 0;
                    foreach (Subscription s in customer_details.Subscriptions)
                    {
                        if (sub == 0)
                        {
                            if (s.CanceledDateTime != null)
                            {
                                plancancelled = true;
                                CancelInfo = s.CanceledDateTime.ToString();
                            }

                            tempInnerHTML += "<table class=\"cancelpagemaintable\">";
                            foreach (SubscriptionPlan p in s.SubscriptionsPlans)
                            {
                                if (p.Name.IndexOf("FREE") < 0)    //Only if not FREE
                                {
                                    tempInnerHTML += "<tr><td colspan=2 class=\"table_headrow\">Current Publishing Service</td></tr>";
                                    tempInnerHTML += "<tr><td class=\"table_itemtitle\">Publishing Service: </td><td class=\"table_item\">" + p.Name + "</td></tr>";
                                    tempInnerHTML += "<tr><td class=\"table_itemtitle\">Recurring Charge: </td><td class=\"table_item\">" + p.RecurringChargeAmount + "</td></tr>";
                                }
                                else
                                {
                                    cgbilling.Visible = false;
                                    CancelSubscriptionsButton.Visible = false;

                                    RadNotification1.Title = "WARNING";
                                    RadNotification1.Text = "No publishing service found for the App";
                                    RadNotification1.Visible = true;
                                    RadNotification1.Show();
                                    return;
                                }

                            }
                            sub++;


                            if (plancancelled == true)
                            {
                                tempInnerHTML += "<tr><td colspan=4 class=\"cancelwarning\">Cancelled on:" + CancelInfo + "<br><br></td></tr>";

                                CancelSubscriptionsButton.Visible = false;
                            }
                            else
                                CancelSubscriptionsButton.Visible = true;

                            tempInnerHTML += "</table>";
                        }
                        cgbilling.InnerHtml = tempInnerHTML;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void CancelSubscriptionsButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool cancelconfirmation = false;
              

                if (String.IsNullOrEmpty(AppCGCustomerCode) == false)
                {

                    //Cancel native
                    CGError servererror = new CGError();
                    cancelconfirmation = CheddarGetter.CancelSubscription(AppCGCustomerCode, servererror);

                    //FAILURE
                    if (String.IsNullOrEmpty(servererror.Code) == false)
                    {
                        //CG.InnerHtml += "<li>ERROR:" + servererror.Message;
                        RadNotification1.Title = "WARNING";
                        RadNotification1.Text = servererror.Message;
                        RadNotification1.Visible = true;
                        RadNotification1.Show();

                        return;
                    }

                }


                if (cancelconfirmation)    //SUCCESSFUL CANCELLATION
                {
                    Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
                    BillingUtil billingutil = new BillingUtil();
                    billingutil.CancelPaidServicesDB(State);

                    CGResponseFlag.Text = cancelconfirmation.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected void RadComboAppSelector_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            BillingUtil billingutil = new BillingUtil();
            try
            {

                if (ApplicationInit(State, RadComboAppSelector, AppID, AppCGCustomerCode))
                    page_refresh();
            }
            catch (Exception)
            {

                throw;
            }


        }


        protected void BackButton_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("../TabPublish.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }



        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
            string account_type = util.GetAccountType(State);
            util.Logout(State);
            if (account_type != null && account_type.Contains("google_apps"))
                Response.Redirect("../LogoutForGoogleApps.aspx", false);
            else
                Response.Redirect("../Default.aspx", false);

        }

        protected void TabMenu_ItemClick(object sender, RadMenuEventArgs e)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

            //if (SavedCanvasHtml.Text.Length > 0)
            //     SavePage();

            string tab = e.Item.Value;
            
            if (tab == "DesignHybrid" || tab == "DesignWeb")
                State["SelectedApp"] = null;

            Response.Redirect("../Tab" + tab + ".aspx", false);
        }


        private bool ApplicationInit(Hashtable State, RadComboBox AppsList, string AppID, string AppCGCustomerCode)
        {
            Util util = new Util();
            string app = AppsList.SelectedValue;

            if (app.Contains("->"))
                return false;


            State["SelectedApp"] = app;

            State["SelectedAppType"] = util.GetAppType(State);


            State["application_id"] = util.GetAppID(State);
            AppID = State["application_id"].ToString();

            BillingUtil billingutil = new BillingUtil();
            AppCGCustomerCode = billingutil.getAppPaidSKU(State);

            if (State["SelectedDeviceType"] == null)
            {
                State["SelectedDeviceView"] = State["SelectedDeviceType"] = Constants.IPHONE;
            }


            XmlUtil x_util = new XmlUtil();
            State["SelectedDeviceView"] = State["SelectedDeviceType"] = x_util.GetAppDeviceType(State);

            string username = State["Username"].ToString();
            State["customer_id"] = util.GetCustomerIDFromUsername(State, username);

            return true;
        }
    }

