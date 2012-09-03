using System;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Collections;
using System.Data;


public partial class ModifyPublishingService : System.Web.UI.Page
{
    // Data members
    public string AppType = "";
    public string AppID = "";
    public string AppSKU = "";
    public string AppCGCustomerCode = "";
    private static PlanCodeEnum SelectedPlanCode = 0;


   


    protected void Page_Load(object sender, System.EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        

        Util util = new Util();
        Init init = new Init();

        try
        {

         

            if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }
            {

                if (!Page.IsPostBack)
                {
                    USERINFO.Visible = false;


                    if (InitAppsList(State, RadComboAppSelector) == false)
                    {
                        //There are no Apps which can add a NEW Service at this moment.
                        RadMultiPageMaster.Visible = false;
                        RadNotification1.Title = "WARNING";
                        RadNotification1.Text = "There are no Apps to modify publishing service for.";
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

    }




    private bool InitAppsList(Hashtable State, RadComboBox AppsList)
    {
        Util util = new Util();
        try
        {

            if (AppsList == null)
                return false;



            //Get only paid_apps from paid_services table directly.
            string sql = "SELECT app_name FROM paid_services WHERE customer_id='" + State["CustomerID"].ToString() + "' AND status='paid' ORDER BY app_name"; 

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
            HideAllNativeTicks();
            HideAllWebTicks();

            AppID = State["application_id"].ToString();

            if (String.IsNullOrEmpty(AppID) == false)
            {
                CGError servererror = new CGError();
                Customer customer_details = CheddarGetter.GetCustomer(AppID, servererror);

                if (String.IsNullOrEmpty(servererror.Code) == false)
                {
                    USERINFO.Visible = false;
                    RadMultiPageMaster.Visible = false;

                    RadNotification1.Title = "WARNING";

                    if (servererror.Code.IndexOf("NotFound") >= 0)
                        RadNotification1.Text = "No publishing service found for the App";
                    else
                        RadNotification1.Text = servererror.Message;

                    RadNotification1.Visible = true;
                    RadNotification1.Show();

                    return;
                }





                if (customer_details.Subscriptions[0].CanceledDateTime != null)
                {
                    USERINFO.Visible = false;
                    RadMultiPageMaster.Visible = false;

                    RadNotification1.Title = "WARNING";
                    RadNotification1.Text = "Cannot modify a cancelled publishing service. The most recent publishing service for this App was cancelled on " + customer_details.Subscriptions[0].CanceledDateTime.ToString() + ". Please create a new publishing service.";

                    RadNotification1.Visible = true;
                    RadNotification1.Show();

                    return;
                }



                if (String.Compare(customer_details.Code, AppID, false) == 0)
                {
                    CompanyTextBox.Text = customer_details.Company;
                    EmailTextBox.Text = customer_details.Email;
                    FirstNameTextBox.Text = customer_details.FirstName;
                    LastNameTextBox.Text = customer_details.LastName;

                    CCFirstNameTextbox.Text = customer_details.Subscriptions[0].CCFirstName;
                    CCLastNameTextBox.Text = customer_details.Subscriptions[0].CCLastName;
                    CCZipTextBox.Text = customer_details.Subscriptions[0].CCZip;

                }

                AppType = State["SelectedAppType"].ToString();
                AppID = State["application_id"].ToString();
                TickAndSelect_CurrentPlan();
            }


            USERINFO.Visible = true;

            if (State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE)
            {
                RadMultiPageMaster.Visible = true;
                RadMultiPageMaster.SelectedIndex = 1;
                RadPageNativeSection.Visible = false;
                RadPageWebSection.Visible = true;
                WebPricingMultiPage.SelectedIndex = 0;
                RadTabStrip1.SelectedIndex = 0;
            }
            else
            {
                RadMultiPageMaster.Visible = true;
                RadMultiPageMaster.SelectedIndex = 0;
                RadPageNativeSection.Visible = true;
                RadPageWebSection.Visible = false;
                NativePricingMultiPage.SelectedIndex = 0;
                RadTabStripNative.SelectedIndex = 0;
            }



            PreFillBillingFormDetails();
        }
        catch (Exception)
        {

            throw;
        }
    }



    protected void RadTabStrip1_TabClick(object sender, RadTabStripEventArgs e)
    {

    }

    private void TickAndSelect_CurrentPlan()
    {

        try
        {
            if ((AppType.Contains("native") || AppType.Contains("hybrid")) && (String.IsNullOrEmpty(AppID) == false))
            {
                CGError servererror = new CGError();
                //Get the Native and Web Plan Code for the Customer from CG first.
                Customer customer_details = CheddarGetter.GetCustomer(AppID, servererror);
                if (customer_details != null)
                {
                    //Split the code which is number_NATIVE_MONTHLY_BASIC
                    string[] words = customer_details.Subscriptions[0].SubscriptionsPlans[0].Code.Split(new char[] { '_' }, 3);


                    if (words.Length < 2)
                        return;

                    HideAllNativeTicks();


                    if (String.Compare("MONTHLY", words[1], true) == 0)
                    {
                        NativePricingMultiPage.SelectedIndex = 0;
                        RadTabStripNative.SelectedIndex = 0;

                        if (String.Compare("BASIC", words[2], true) == 0)
                        {
                            Tick1.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.NATIVE_MONTHLY_BASIC;
                            System.Diagnostics.Debug.WriteLine("NATIVE_MONTHLY_BASIC");
                        }

                        if (String.Compare("PRO", words[2], true) == 0)
                        {
                            Tick2.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.NATIVE_MONTHLY_PRO;
                            System.Diagnostics.Debug.WriteLine("NATIVE_MONTHLY_PRO");
                        }

                        if (String.Compare("PREMIUM", words[2], true) == 0)
                        {
                            Tick3.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.NATIVE_MONTHLY_PREMIUM;
                            System.Diagnostics.Debug.WriteLine("NATIVE_MONTHLY_PREMIUM");
                        }

                    }

                    if (String.Compare("YEARLY", words[1], true) == 0)
                    {
                        NativePricingMultiPage.SelectedIndex = 1;
                        RadTabStripNative.SelectedIndex = 1;
                        

                        if (String.Compare("BASIC", words[2], true) == 0)
                        {
                            Tick4.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.NATIVE_YEARLY_BASIC;
                            System.Diagnostics.Debug.WriteLine("NATIVE_YEARLY_BASIC");
                        }

                        if (String.Compare("PRO", words[2], true) == 0)
                        {
                            Tick5.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.NATIVE_YEARLY_PRO;
                            System.Diagnostics.Debug.WriteLine("NATIVE_YEARLY_PRO");
                        }

                        if (String.Compare("PREMIUM", words[2], true) == 0)
                        {
                            Tick6.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.NATIVE_YEARLY_PREMIUM;
                            System.Diagnostics.Debug.WriteLine("NATIVE_YEARLY_PREMIUM");
                        }

                    }

                    if (String.Compare("NONPROFIT", words[1], true) == 0)
                    {
                        NativePricingMultiPage.SelectedIndex = 2;
                        RadTabStripNative.SelectedIndex = 2;


                        if (String.Compare("BASIC", words[2], true) == 0)
                        {
                            Tick7.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.NATIVE_NONPROFIT_BASIC;
                            System.Diagnostics.Debug.WriteLine("NATIVE_NONPROFIT_BASIC");
                        }

                        if (String.Compare("PRO", words[2], true) == 0)
                        {
                            Tick8.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.NATIVE_NONPROFIT_PRO;
                            System.Diagnostics.Debug.WriteLine("NATIVE_NONPROFIT_PRO");
                        }

                        if (String.Compare("PREMIUM", words[2], true) == 0)
                        {
                            Tick9.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.NATIVE_NONPROFIT_PREMIUM;
                            System.Diagnostics.Debug.WriteLine("NATIVE_NONPROFIT_PREMIUM");
                        }

                    }
                } //(customer_details != null)
            }

            if (AppType.Contains("web") && (String.IsNullOrEmpty(AppID) == false))
            {
                CGError servererror = new CGError();
                Customer web_customer_details = CheddarGetter.GetCustomer(AppID, servererror);
                if (web_customer_details != null)
                {
                    string[] words = web_customer_details.Subscriptions[0].SubscriptionsPlans[0].Code.Split(new char[] { '_' }, 3);

                    if (words.Length < 2)
                        return;


                    HideAllWebTicks();


                    if (String.Compare("MONTHLY", words[1], true) == 0)
                    {
                        WebPricingMultiPage.SelectedIndex = 0;
                        RadTabStrip1.SelectedIndex = 0;

                        if (String.Compare("BASIC", words[2], true) == 0)
                        {
                            WTick1.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.WEB_MONTHLY_BASIC;
                            System.Diagnostics.Debug.WriteLine("WEB_MONTHLY_BASIC");
                        }

                        if (String.Compare("PRO", words[2], true) == 0)
                        {
                            WTick2.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.WEB_MONTHLY_PRO;
                            System.Diagnostics.Debug.WriteLine("WEB_MONTHLY_PRO");
                        }

                        if (String.Compare("PREMIUM", words[2], true) == 0)
                        {
                            WTick3.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.WEB_MONTHLY_PREMIUM;
                            System.Diagnostics.Debug.WriteLine("WEB_MONTHLY_PREMIUM");
                        }
                    }

                    if (String.Compare("YEARLY", words[1], true) == 0)
                    {
                        WebPricingMultiPage.SelectedIndex = 1;
                        RadTabStrip1.SelectedIndex = 1;

                        if (String.Compare("BASIC", words[2], true) == 0)
                        {
                            WTick4.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.WEB_YEARLY_BASIC;
                            System.Diagnostics.Debug.WriteLine("WEB_YEARLY_BASIC");
                        }

                        if (String.Compare("PRO", words[2], true) == 0)
                        {
                            WTick5.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.WEB_YEARLY_PRO;
                            System.Diagnostics.Debug.WriteLine("WEB_YEARLY_PRO");
                        }

                        if (String.Compare("PREMIUM", words[2], true) == 0)
                        {
                            WTick6.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.WEB_YEARLY_PREMIUM;
                            System.Diagnostics.Debug.WriteLine("WEB_YEARLY_PREMIUM");
                        }

                    }

                    if (String.Compare("NONPROFIT", words[1], true) == 0)
                    {
                        WebPricingMultiPage.SelectedIndex = 2;
                        RadTabStrip1.SelectedIndex = 2;

                        if (String.Compare("BASIC", words[2], true) == 0)
                        {
                            WTick7.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.WEB_NONPROFIT_BASIC;
                            System.Diagnostics.Debug.WriteLine("WEB_NONPROFIT_BASIC");
                        }

                        if (String.Compare("PRO", words[2], true) == 0)
                        {
                            WTick8.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.WEB_NONPROFIT_PRO;
                            System.Diagnostics.Debug.WriteLine("WEB_NONPROFIT_PRO");
                        }

                        if (String.Compare("PREMIUM", words[2], true) == 0)
                        {
                            WTick9.Visible = true;
                            SelectedPlanCode = PlanCodeEnum.WEB_NONPROFIT_PREMIUM;
                            System.Diagnostics.Debug.WriteLine("WEB_NONPROFIT_PREM");
                        }

                    }

                }
            }


        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void MonthlyBasicButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = Tick1.Visible;
            HideAllNativeTicks();
            Tick1.Visible = !tmp;

            if (Tick1.Visible)
                SelectedPlanCode = PlanCodeEnum.NATIVE_MONTHLY_BASIC;

            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void MonthlyProButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = Tick2.Visible;
            HideAllNativeTicks();
            Tick2.Visible = !tmp;

            if (Tick2.Visible)
                SelectedPlanCode = PlanCodeEnum.NATIVE_MONTHLY_PRO;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void MonthlyPremiumButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = Tick3.Visible;
            HideAllNativeTicks();
            Tick3.Visible = !tmp;
            if (Tick3.Visible)
                SelectedPlanCode = PlanCodeEnum.NATIVE_MONTHLY_PREMIUM;

            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void YearlyBasicButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = Tick4.Visible;
            HideAllNativeTicks();
            Tick4.Visible = !tmp;
            if (Tick4.Visible)
                SelectedPlanCode = PlanCodeEnum.NATIVE_YEARLY_BASIC;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void YearlyProButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = Tick5.Visible;
            HideAllNativeTicks();
            Tick5.Visible = !tmp;
            if (Tick5.Visible)
                SelectedPlanCode = PlanCodeEnum.NATIVE_YEARLY_PRO;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void YearlyPremiumButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = Tick6.Visible;
            HideAllNativeTicks();
            Tick6.Visible = !tmp;
            if (Tick6.Visible)
                SelectedPlanCode = PlanCodeEnum.NATIVE_YEARLY_PREMIUM;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void NonProfitBasicButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = Tick7.Visible;
            HideAllNativeTicks();
            Tick7.Visible = !tmp;
            if (Tick7.Visible)
                SelectedPlanCode = PlanCodeEnum.NATIVE_NONPROFIT_BASIC;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void NonProfitProButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = Tick8.Visible;
            HideAllNativeTicks();
            Tick8.Visible = !tmp;
            if (Tick8.Visible)
                SelectedPlanCode = PlanCodeEnum.NATIVE_NONPROFIT_PRO;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void NonProfitPremiumButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = Tick9.Visible;
            HideAllNativeTicks();
            Tick9.Visible = !tmp;
            if (Tick9.Visible)
                SelectedPlanCode = PlanCodeEnum.NATIVE_NONPROFIT_PREMIUM;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void WebBasicMonthlyButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = WTick1.Visible;
            HideAllWebTicks();
            WTick1.Visible = !tmp;
            if (WTick1.Visible)
                SelectedPlanCode = PlanCodeEnum.WEB_MONTHLY_BASIC;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void WebProMonthlyButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = WTick2.Visible;
            HideAllWebTicks();
            WTick2.Visible = !tmp;
            if (WTick2.Visible)
                SelectedPlanCode = PlanCodeEnum.WEB_MONTHLY_PRO;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }
    }


    protected void WebPremiumMonthlyButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = WTick3.Visible;
            HideAllWebTicks();
            WTick3.Visible = !tmp;
            if (WTick3.Visible)
                SelectedPlanCode = PlanCodeEnum.WEB_MONTHLY_PREMIUM;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }
    }


    protected void WebYearlyBasicButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = WTick4.Visible;
            HideAllWebTicks();
            WTick4.Visible = !tmp;
            if (WTick4.Visible)
                SelectedPlanCode = PlanCodeEnum.WEB_YEARLY_BASIC;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void WebYearlyProButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = WTick5.Visible;
            HideAllWebTicks();
            WTick5.Visible = !tmp;
            if (WTick5.Visible)
                SelectedPlanCode = PlanCodeEnum.WEB_YEARLY_PRO;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void WebYearlyPremiumButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = WTick6.Visible;
            HideAllWebTicks();
            WTick6.Visible = !tmp;
            if (WTick6.Visible)
                SelectedPlanCode = PlanCodeEnum.WEB_YEARLY_PREMIUM;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }
    }


    protected void WebNonProfitBasicButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = WTick7.Visible;
            HideAllWebTicks();
            WTick7.Visible = !tmp;
            if (WTick7.Visible)
                SelectedPlanCode = PlanCodeEnum.WEB_NONPROFIT_BASIC;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void WebNonProfitProButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = WTick8.Visible;
            HideAllWebTicks();
            WTick8.Visible = !tmp;
            if (WTick8.Visible)
                SelectedPlanCode = PlanCodeEnum.WEB_NONPROFIT_PRO;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void WebNonProfitPremiumButton_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean tmp = WTick9.Visible;
            HideAllWebTicks();
            WTick9.Visible = !tmp;
            if (WTick9.Visible)
                SelectedPlanCode = PlanCodeEnum.WEB_NONPROFIT_PREMIUM;
            System.Diagnostics.Debug.WriteLine("{0}", SelectedPlanCode);
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];

        try
        {
            System.Diagnostics.Debug.WriteLine("SelectedPlanCode={0} ", SelectedPlanCode);

            System.Diagnostics.Debug.WriteLine("AppCGCustomerCode={0} , AppID={1}", AppCGCustomerCode, AppID);



            if (SelectedPlanCode == 0)  //Nothing selected at all
            {
                RadNotification1.Title = "WARNING";
                RadNotification1.Text = "Please select atleast one publishing service";
                RadNotification1.Visible = true;
                RadNotification1.Show();
            }
            else
            {
                Page.Validate();
                if (Page.IsValid) //ALL VALIDATORS VALIDATED FINE
                {

                    AppID = State["application_id"].ToString();

                    //Connect to CG and Modify the Customer 
                    if (CG_ModifyCustomer() == true)
                    {
                        //If successful
                        if (String.IsNullOrEmpty(AppCGCustomerCode) == false)
                        {
                                                   
                            //Flag to the AJAX client side.
                            CGResponseFlag.Text = AppCGCustomerCode;

                        }
                    }
                }
            }

        }
        catch (Exception)
        {

            throw;
        }

    }


    private void HideAllNativeTicks()
    {
        try
        {
            SelectedPlanCode = 0;
            Tick1.Visible = false;
            Tick2.Visible = false;
            Tick3.Visible = false;
            Tick4.Visible = false;
            Tick5.Visible = false;
            Tick6.Visible = false;
            Tick7.Visible = false;
            Tick8.Visible = false;
            Tick9.Visible = false;
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void HideAllWebTicks()
    {
        try
        {
            SelectedPlanCode = 0;
            WTick1.Visible = false;
            WTick2.Visible = false;
            WTick3.Visible = false;
            WTick4.Visible = false;
            WTick5.Visible = false;
            WTick6.Visible = false;
            WTick7.Visible = false;
            WTick8.Visible = false;
            WTick9.Visible = false;
        }
        catch (Exception)
        {

            throw;
        }
    }


    private void StorePublishingServiceInDB(Customer returnCustomer)
    {

        //Write all the right details into the paid_services table in the DB.

    }


    private Boolean CG_ModifyCustomer()
    {
        try
        {
            //Create a new customer
            CustomerPost newCustomer = new CustomerPost();

            newCustomer.Company = CompanyTextBox.Text;
            newCustomer.FirstName = FirstNameTextBox.Text;
            newCustomer.LastName = LastNameTextBox.Text;
            newCustomer.Email = EmailTextBox.Text;


            //Extra fields required for nonFREE plans
            newCustomer.CCFirstName = CCFirstNameTextbox.Text;
            newCustomer.CCLastName = CCLastNameTextBox.Text;
            newCustomer.CCNumber = CCNumberTextBox.Text;
            newCustomer.CCExpiration = CCExpirationTextBox.Text;
            newCustomer.CCZip = CCZipTextBox.Text;
            newCustomer.CCCardCode = CCCardCodeTextBox.Text;

            if (SelectedPlanCode == 0)
            {
                //CG.InnerHtml = "<strong>ERROR <br>ATLEAST ONE PUBLISHING IS REQUIRED !!</strong>";
                RadNotification1.Title = "WARNING";
                RadNotification1.Text = "Atleast one publishing service is required !!";
                RadNotification1.Visible = true;
                RadNotification1.Show();
                return false;
            }
            else
            {
                //CG.InnerHtml = "<strong>BILLING SERVER RESPONSES</strong><br><ul>";

                if (SelectedPlanCode != 0)
                {


                    newCustomer.PlanCode = SelectedPlanCode;                 //The updated plan code goes here
                    newCustomer.Code = AppID;                        //existing customer code
                    System.Diagnostics.Debug.WriteLine("using customer code" + newCustomer.Code + "To modifyplan to " + newCustomer.PlanCode);

                    //Send it to the server
                    CGError servererror = new CGError();
                    Customer returnCustomer = CheddarGetter.UpdateCustomerAndSubscription(newCustomer, servererror);


                    //FAILURE
                    if (String.IsNullOrEmpty(servererror.Code) == false)
                    {
                        //CG.InnerHtml += "<li>ERROR:" + servererror.Message;
                        RadNotification1.Title = "WARNING";
                        RadNotification1.Text = servererror.Message;
                        RadNotification1.Visible = true;
                        RadNotification1.Show();
                        return false;
                    }

                    //SUCCESS
                    if (String.IsNullOrEmpty(returnCustomer.Code) == false)
                    {

                        AppCGCustomerCode = returnCustomer.Code;
                        CGResponseFlag.Text = AppCGCustomerCode;

                        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
                        BillingUtil billingutil = new BillingUtil();
                        string confirm = returnCustomer.Subscriptions[0].Invoices[1].PaidTransactionId.ToString();

                        string sku = returnCustomer.Subscriptions[0].SubscriptionsPlans[0].Code.ToString();

                        billingutil.UpdatePaidServicesDB(confirm,sku, State);


                    }
                }

            }

            return true;

        }//end try
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            throw;
        }

    }




    protected void RadComboAppSelector_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        BillingUtil billingutil = new BillingUtil();

        try
        {
            if (ApplicationInit(State, RadComboAppSelector, AppID, AppCGCustomerCode))
                page_refresh();
            else
            {
                RadNotification1.Title = "WARNING";
                RadNotification1.Text = "Problem initializing the App.";
                RadNotification1.Visible = true;
                RadNotification1.Show();
                return;
            }
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




    private void PreFillBillingFormDetails()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (State["ServerAdminCustomerUsername"] != null)
            UserLabel.Text = State["ServerAdminCustomerUsername"].ToString();
        else
            UserLabel.Text = State["Username"].ToString();

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
        db.CloseViziAppsDatabase(State);

        DataRow row = rows[0];

        CompanyTextBox.Text = util.DecodeMySql(row["company"].ToString());
        EmailTextBox.Text = row["email"].ToString();
        FirstNameTextBox.Text = util.DecodeMySql(row["first_name"].ToString());
        LastNameTextBox.Text = util.DecodeMySql(row["last_name"].ToString());
        StreetTextBox.Text = util.DecodeMySql(row["street_address"].ToString());
        CityTextBox.Text = util.DecodeMySql(row["city"].ToString());

        if (row["state"] != null && row["state"].ToString().Length > 0)
            StateList.SelectedValue = row["state"].ToString();


        //StateList.Text = row["state"].ToString();

        PostalCodeTextBox.Text = row["postal_code"].ToString();
        CountryTextBox.Text = util.DecodeMySql(row["country"].ToString());
        PhoneTextbox.Text = row["phone"].ToString();

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //CCFirstNameTextbox.Text = util.DecodeMySql(row["first_name"].ToString());
        //CCLastNameTextBox.Text = util.DecodeMySql(row["last_name"].ToString());
        //CCZipTextBox.Text = row["postal_code"].ToString();

        //++++++++++++++ To be Removed at the end ++++++++++++++++++++++++++
        //CCNumberTextBox.Text = "4111111111111111";
        //CCExpirationTextBox.Text = "12/2012";
        //CCCardCodeTextBox.Text = "222";
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
