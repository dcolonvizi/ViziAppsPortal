using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Data;


public partial class ShowBillingHistory : System.Web.UI.Page
{
    // Data members
    public string AppID = "";
    public string AppCGCustomerCode = "";
   
    public class BillingHistoryData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string CCFirstName { get; set; }
        public string CCLastName { get; set; }
        public string CCNumber { get; set; }
        public string CCZip { get; set; }
        public string CCType { get; set; }
        public int? CCLastFour { get; set; }
        public SortedList Invoices { get; set; }
        public SortedList Subscriptions { get; set; }
    }

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

                    if (InitAppsList(State, RadComboAppSelector) == false)
                    {
                        //There are no Apps which can add a NEW Service at this moment.
                        RadNotification1.Title = "WARNING";
                        RadNotification1.Text = "There are no Apps with Billing History";
                        RadNotification1.Visible = true;
                        RadNotification1.Show();
                    }
                    
                    /*if ((State["SelectedApp"] != null) && State["SelectedApp"].ToString() != "")
                    {
                        RadComboAppSelector.SelectedValue = State["SelectedApp"].ToString();
                    }*/
                }
            }
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }

    }

    private bool InitAppsList(Hashtable State, RadComboBox AppsList)
    {
        Util util = new Util();
        try
        {

            if (AppsList == null)
            {
                showhistory.Visible = false;
                return false;
            }
            else
                showhistory.Visible = true;


            //Get only apps from paid_services table directly (even if status is not paid => Just App Preparation done it should have a record there.
            string sql = "SELECT app_name FROM paid_services WHERE customer_id='" + State["CustomerID"].ToString() + "' ORDER BY app_name"; 

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
            //All three are Null then nothing to show.
            if (State["application_id"] == null)
            {
                showhistory.Visible = false;
            }
            else
            {
                BillingHistoryData BillingHistory = new BillingHistoryData();
                BillingHistory.Subscriptions = new SortedList();
                BillingHistory.Invoices = new SortedList();


                

                    showhistory.Visible = false;
                    AppID = State["application_id"].ToString();
                    CGError servererror = new CGError();
                    Customer customer_details = CheddarGetter.GetCustomer(AppID, servererror);

                    if (String.IsNullOrEmpty(servererror.Code) == false)
                    {
                        RadNotification1.Title = "WARNING";

                        if (servererror.Code.IndexOf("NotFound") >= 0)
                            RadNotification1.Text = "No billing history found for the App";
                        else
                            RadNotification1.Text = servererror.Message;

                        RadNotification1.Visible = true;
                        RadNotification1.Show();

                        return;
                    }


                    if ((DateTime.Compare(customer_details.CreatedDateTime, BillingHistory.CreatedDateTime) > 0))
                    {
                        BillingHistory.CreatedDateTime = customer_details.CreatedDateTime;
                    }

                    BillingHistory.FirstName = customer_details.FirstName;
                    BillingHistory.LastName = customer_details.LastName;
                    BillingHistory.CreatedDateTime = customer_details.CreatedDateTime;
                    BillingHistory.ModifiedDateTime = customer_details.ModifiedDateTime;
                    BillingHistory.Email = customer_details.Email;

                    if ((DateTime.Compare(customer_details.CreatedDateTime, BillingHistory.CreatedDateTime) > 0))
                    {
                        BillingHistory.CreatedDateTime = customer_details.CreatedDateTime;
                    }

                    BillingHistory.ModifiedDateTime = customer_details.ModifiedDateTime;
                    BillingHistory.Email = customer_details.Email;
                    int sub = 0;
                    int delta_time = 30;

                    foreach (Subscription s in customer_details.Subscriptions)
                    {
                        if (sub == 0)
                        {
                            BillingHistory.CCType = s.CCType;
                            BillingHistory.CCLastFour = s.CCLastFour;
                            BillingHistory.CCFirstName = s.CCFirstName;
                            BillingHistory.CCLastName = s.CCLastName;
                            BillingHistory.CCZip = s.CCZip;
                        }

                        if (BillingHistory.Subscriptions.Contains(s.CreatedDateTime) == false)
                            BillingHistory.Subscriptions.Add(s.CreatedDateTime, s);
                        else
                        {
                            s.CreatedDateTime = s.CreatedDateTime.AddSeconds(delta_time);
                            BillingHistory.Subscriptions.Add(s.CreatedDateTime, s);
                        }


                        foreach (Invoice i in s.Invoices)
                        {
                            if (BillingHistory.Invoices.Contains(i.CreatedDateTime) == false)
                                BillingHistory.Invoices.Add(i.CreatedDateTime, i);
                            else
                            {
                                i.CreatedDateTime = i.CreatedDateTime.AddSeconds(delta_time++);
                                BillingHistory.Invoices.Add(i.CreatedDateTime, i);
                            }

                        }

                        sub++;
                    }
                





                //Start acinfo table
                string tempInnerHTML = "";
                tempInnerHTML += "<tr><td colspan=4 class=\"table_headrow\">Billing History for " + BillingHistory.FirstName + "  " + BillingHistory.LastName + "</td></tr>";
                tempInnerHTML += "<tr><td class=\"table_itemtitle\">Customer Since:</td><td class=\"table_item\">" + BillingHistory.CreatedDateTime + "</td>";
                tempInnerHTML += "<td class=\"table_itemtitle\">Last Modified:</td><td class=\"table_item\">" + BillingHistory.ModifiedDateTime + "</td></tr>";
                acinfo.InnerHtml = tempInnerHTML;
                //End acinfo table

                //Build the CCDetails table 
                tempInnerHTML = "";
                tempInnerHTML += "<tr><td colspan=5 class=\"table_headrow\">Credit Card on File</td></tr>";
                tempInnerHTML += "<tr class=\"table_itemtitle\"><td>Name</td><td>Type<td>Last 4 Digits</td><td>Zip Code </td><td>Most Recent Bill</td></tr>";
                tempInnerHTML += "<tr class=\"table_item\"><td>" + BillingHistory.CCFirstName + " " + BillingHistory.CCLastName + "</td>";
                tempInnerHTML += "<td>" + BillingHistory.CCType.ToUpper() + "</td>";
                tempInnerHTML += "<td>" + BillingHistory.CCLastFour + "</td>";
                tempInnerHTML += "<td>" + BillingHistory.CCZip + "</td>";
                tempInnerHTML += "<td >" + BillingHistory.CreatedDateTime + "</td></tr>";
                ccdetails.InnerHtml = tempInnerHTML;
                //End the CCDetails table 


                //Build the Invoices Table
                tempInnerHTML = "";
                tempInnerHTML += "<tr><td colspan=6 class=\"table_headrow\">Invoices</td></tr>";
                tempInnerHTML += "<tr class=\"table_itemtitle\"><td>Invoice No.</td><td>Created Date</td><td>Billing Date</td><td>Type</td><td>Transaction Id</td><td>Total Charges</td></tr>";

                for (int c = 0; c < BillingHistory.Invoices.Count; c++)
                {
                    Boolean freeinvoice = false;
                    float totalcharge = 0;
                    string mycharges = "";
                    string str_item = "";

                    Invoice inv = (Invoice)BillingHistory.Invoices.GetByIndex(c);


                    mycharges = "<table class=\"singleinvoicetable\"><tr><td><h2> INVOICE " + inv.Number + "</h2></td><td class=\"small_gap\"></td><td class=\"small_gap\"></td><td class=\"small_gap\"></td><td>Billed on " + inv.BillingDateTime + "<br></td></tr></table>";
                    mycharges += "<table class=\"singleinvoicetable\">";
                    mycharges += "<caption> Charges </caption>";
                    mycharges += "<tr class=\"table_headrow\"><td>Item</td><td>Type</td><td>Code</td><td>Date</td><td>Quantity</td><td>Each</td><td>Amount</td></tr>";

                    foreach (Charge ch in inv.Charges)
                    {
                        
                        
                        if (ch.Code.Contains("BRANDING")  && ch.EachAmount == 0.00)
                            freeinvoice = true;

                        if (!freeinvoice)
                        {
                            if (ch.EachAmount < 0)
                                str_item = "Credit";
                            else
                                str_item = "Subscription";

                            mycharges += "<tr><td class=\"singleinvoicetableitem\">" + str_item + "<td class=\"singleinvoicetableitem\">" + ch.Type + "</td><td class=\"singleinvoicetableitem\">" + ch.Code + "</td><td class=\"singleinvoicetableitem\">" + ch.CreatedDateTime + "</td><td class=\"singleinvoicetableitem\">" + ch.Quantity + "</td><td class=\"singleinvoicetableitem\">" + ch.EachAmount + "</td><td class=\"singleinvoicetableitem\">" + (ch.EachAmount * ch.Quantity) + "</td></tr>";

                            if (String.IsNullOrEmpty(ch.Description) == false)
                                mycharges += "<tr class=\"singleinvoicedescription\"><td colspan=6>" + ch.Description + "</td></tr>";

                            totalcharge += ch.EachAmount;
                        }
                    }


                    if (!freeinvoice)
                    {
                        mycharges += "<tr><td colspan=7><hr></td></tr>";
                        mycharges += "<tr><td></td><td></td><td></td><td></td><td></td><td class=\"singleinvoicetableitem\">Total </td><td class=\"singleinvoicetableitem\">" + totalcharge + "</td>";
                        mycharges += "</table>";


                        if (inv.Transactions.Count > 0)
                        {
                            mycharges += "<table class=\"singleinvoicetable\">";
                            mycharges += "<caption> Transactions</caption>";
                            mycharges += "<tr class=\"table_headrow\"><td>Transaction ID</td><td>Type</td><td>Time</td><td>Response</td><td>Amount</td></tr>";
                            //Transactions
                            string tr_type = "";
                            foreach (Transaction tr in inv.Transactions)
                            {
                                if (tr.amount < 0)
                                {
                                    tr_type = "Refund";
                                    mycharges += "<tr><td class=\"singleinvoicetableitem\">" + tr.ID + "<td class=\"cancelwarning\">" + tr_type + "<td class=\"singleinvoicetableitem\">" + tr.transactedDatetime + "</td><td class=\"singleinvoicetableitem\">" + tr.response + "</td><td class=\"singleinvoicetableitem\">" + tr.amount + "</td></tr>";
                                }
                                else
                                {
                                    tr_type = "Charge";
                                    mycharges += "<tr><td class=\"singleinvoicetableitem\">" + tr.ID + "<td class=\"singleinvoicetableitem\">" + tr_type + "<td class=\"singleinvoicetableitem\">" + tr.transactedDatetime + "</td><td class=\"singleinvoicetableitem\">" + tr.response + "</td><td class=\"singleinvoicetableitem\">" + tr.amount + "</td></tr>";
                                }
                            }
                            mycharges += "</table>";
                        }

                        tempInnerHTML += "<tr class=\"table_item\"><td> <a href=\"#\" id=\"trigger" + inv.Number + "\">" + inv.Number + "</a></td><td>" + inv.CreatedDateTime.ToString("MM/dd/yyyy hh:mm tt") + "</td><td>" + inv.BillingDateTime.ToString("MM/dd/yyyy hh:mm tt") + "</td><td>" + inv.Type + "</td><td>" + inv.PaidTransactionId + "</td><td>" + totalcharge.ToString("0.00") + "</td><td><div class=\"invoicepopup\"  id=\"popup" + inv.Number + "\">" + mycharges + "</div>";
                        tempInnerHTML += "<script type=\"text/javascript\"> $(function () {  var moveLeft = 30; var moveDown = -120; $('a#trigger" + inv.Number + "').hover(function (e) {  $('div#popup" + inv.Number + "').show();}, function () { $('div#popup" + inv.Number + "').hide(); }); $('a#trigger" + inv.Number + "').mousemove(function (e) {$(\"div#popup" + inv.Number + "\").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft); }); }); </script>";
                    }

                }
                invoices.InnerHtml = tempInnerHTML;
                //End the Invoices Table



                //Build Subscriptions Table
                tempInnerHTML = "";
                tempInnerHTML += "<tr><td colspan=5 class=\"table_headrow\">Subscriptions</td></tr>";
                tempInnerHTML += "<tr class=\"table_itemtitle\"><td>Created On</td><td>Plan Name<td>Description</td><td>Recurring Charge</td><td>Status</td></tr>";
                string SubStatus = "";
                for (int s = 0; s < BillingHistory.Subscriptions.Count; s++)
                {
                    Subscription subs = (Subscription)BillingHistory.Subscriptions.GetByIndex(s);

                    SubStatus = "-";

                    if ((s == (BillingHistory.Subscriptions.Count - 1)))
                        SubStatus = "Active";

                    if (subs.CanceledDateTime.HasValue)
                        SubStatus = "Cancelled on " + subs.CanceledDateTime.ToString();


                    //if (subs.SubscriptionsPlans[0].Name.IndexOf("FREE") < 0)

                    if (subs.SubscriptionsPlans[0].RecurringChargeCode.Contains("BRANDING") == false)
                        tempInnerHTML += "<tr class=\"table_item\"><td >" + subs.CreatedDateTime + "</td><td>" + subs.SubscriptionsPlans[0].Name + "</td><td>" + subs.SubscriptionsPlans[0].Description + "</td><td>" + subs.SubscriptionsPlans[0].RecurringChargeAmount + "</td><td>" + SubStatus + "</td></tr>";

                }
                subscriptions.InnerHtml = tempInnerHTML;
                //End Subscriptions Table    

            }//else


            //Make Visible the history DIV
            showhistory.Visible = true;

        }//try
        catch (ArgumentException argex)
        {
            //Ignore it
        }
        catch (global::System.Exception)
        {
            throw;
        }

    }//pagerefresh

    protected void RadComboAppSelector_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        try
        {
            BillingUtil billingutil = new BillingUtil();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];

            if (ApplicationInit(State, RadComboAppSelector, AppID, AppCGCustomerCode) == true)
                page_refresh();
            else
                showhistory.Visible = false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ": " + ex.StackTrace);
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
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;
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
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        string tab = e.Item.Value;
        Session["MainMenu"] = tab;
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
