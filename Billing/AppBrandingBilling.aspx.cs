using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using System.Data;


public partial class AppBrandingBilling : System.Web.UI.Page
{
    // Data members

    public string BrandingType = "";
    public string AppID = "";
    public string AppType = "";
    public string AppCGCustomerCode = "";

    //private PlanCodeEnum  nativePlanCode = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];

        try
        {

            if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }
            {

                if (!Page.IsPostBack)
                {

                    Android.Visible = false;
                    ios.Visible = false;
                    SubmitButton.Visible = false;
                    USERINFO.Visible = false;


                    if (InitAppsList(State, RadComboAppSelector) == false)
                    {
                        //There are no Apps which can add a NEW Service at this moment.
                        RadNotification1.Title = "WARNING";
                        RadNotification1.Text = "There are no Apps that can be submitted for App Store Preparation.";
                        RadNotification1.Visible = true;
                        RadNotification1.Show();
                    }

                   /* if ((State["SelectedApp"] != null) && State["SelectedApp"].ToString() != "")
                    {
                        RadComboAppSelector.SelectedValue = State["SelectedApp"].ToString();
                    }*/

                    //if (RadComboAppSelector.Items.Count == 0 || RadComboAppSelector.SelectedValue.Contains("->") || State["SelectedApp"] == null)



                }


            }
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }

    }



    // Modified from original viziapps code with the additional checks so only valid ones are added to the ComboBox.
    private bool InitAppsList(Hashtable State, RadComboBox AppsList)
    {
        Util util = new Util();
        try
        {

            if (AppsList == null)
                return false;

           // string sql = "SELECT DISTINCT application_name FROM applications WHERE customer_id='" + State["CustomerID"].ToString() + "' ORDER BY application_name";

            //Get only native Apps to load onto this ComboBox.
            string sql = "SELECT DISTINCT application_name,application_id FROM applications WHERE customer_id='" + State["CustomerID"].ToString() + "' AND application_type <> 'web'" + " ORDER BY application_name";  

            DB db = new DB();
            DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
            AppsList.Items.Clear();
            foreach (DataRow row in rows)
            {
                string app_name = row["application_name"].ToString();
                string application_id = row["application_id"].ToString();

                BillingUtil billingutil = new BillingUtil();

                                                                                                //Inserting only Apps which meet all these criteria. 
                if ((billingutil.IsAppStoreSubmissionPaid(State, app_name) == false) &&          // + never submitted for App preparation as yet.
                     (billingutil.IsAppPaid(State, app_name) == false) &&                       // + not yet paid for anything 
                     (util.IsFreeProductionValid(State, application_id) == true) &&             // + completed the Production Form Submission
                     (billingutil.IsAppCancelled(State, app_name) == false))                    // + never cancelled any service.
                    {
                      AppsList.Items.Add(new RadComboBoxItem(app_name, app_name));
                    }

             
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


    protected void SubmitButton_Click(object sender, EventArgs e)
    {

        try
        {

            Page.Validate();

            if (Page.IsValid)
            {
                if (CG_CreateFreeCustomerAndInvoices() == true)   //INVOICES CREATED SUCCCESSFULLY
                {
                    //Done the CG part
                    if (String.IsNullOrEmpty(AppCGCustomerCode) == false)
                    {
                        //Flag to the AJAX client side.
                        CGResponseFlag.Text = AppCGCustomerCode;
                    }
                }

            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }



    }


    private Boolean CG_CreateFreeCustomerAndInvoices()
    {
      
        try
        {
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];

            //Create a new customer
            CustomerPost newCustomer = new CustomerPost();

            newCustomer.Company = CompanyTextBox.Text;
            newCustomer.FirstName = FirstNameTextBox.Text;
            newCustomer.LastName = LastNameTextBox.Text;
            newCustomer.Email = EmailTextBox.Text;
            newCustomer.CCFirstName = CCFirstNameTextbox.Text;
            newCustomer.CCLastName = CCLastNameTextBox.Text;
            newCustomer.CCNumber = CCNumberTextBox.Text;
            newCustomer.CCExpiration = CCExpirationTextBox.Text;
            newCustomer.CCZip = CCZipTextBox.Text;
            newCustomer.CCCardCode = CCCardCodeTextBox.Text;
            newCustomer.Code = State["application_id"].ToString();


           //For Branding purposes create appropriate branding code.
            if (State["SelectedDeviceType"].ToString() == Constants.ANDROID_PHONE)
            {
                newCustomer.PlanCode =  PlanCodeEnum.ANDROID_BRANDING;
            }
            else
            {
                if (State["SelectedDeviceType"].ToString() == Constants.IPHONE)
                {
                    newCustomer.PlanCode = PlanCodeEnum.IOS_BRANDING;
                }
                else
                {
                    RadNotification1.Title = "WARNING";
                    RadNotification1.Text = "This application has no device type.";
                    RadNotification1.Visible = true;
                    RadNotification1.Show();
                    return false;
                }
            }




            //Send it to the server
            CGError servererror = new CGError();
            Customer returnCustomer = CheddarGetter.CreateCustomer(newCustomer, servererror);


            //FAILURE
            if (String.IsNullOrEmpty(servererror.Code) == false)
            {

                //string clickHandler = "this.disabled = false; this.value=\'Submit\'; ";
                // SubmitButton.Attributes.Add("onclick", clickHandler);


                SubmitButton.Enabled = true;
                SubmitButton.Text = "Submit";

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
            }


            // IF SUCCESSFULLY ABLE TO CREATE THE FREE NATIVE CUSTOMER THEN KICK OFF A ONE TIME INVOICE FOR BRANDING 

            OneTimeInvoicePost myonetimeinvoice = new OneTimeInvoicePost();

            myonetimeinvoice.InvoiceCharges = new CustomChargePost[1];

            int number_of_charges = 0;
            CustomChargePost mycustom1 = new CustomChargePost();

            mycustom1.CustomerCode = AppCGCustomerCode;
            mycustom1.Description = "Branding";
            mycustom1.ChargeCode = "BRANDING ";
            mycustom1.EachAmount = 0;
            mycustom1.Quantity = 0;


            if ((State["SelectedDeviceType"].ToString() == Constants.ANDROID_PHONE || State["SelectedDeviceType"].ToString() == Constants.ANDROID_TABLET) && (String.IsNullOrEmpty(AppCGCustomerCode) == false))
            {
                mycustom1.ChargeCode += "- ANRDOID";
                mycustom1.CustomerCode = AppCGCustomerCode;
                mycustom1.Quantity = 1;
                mycustom1.EachAmount += 99;
                mycustom1.Description += "- Android";
            }

            if ((State["SelectedDeviceType"].ToString() == Constants.IPHONE || State["SelectedDeviceType"].ToString() == Constants.IPAD) && (String.IsNullOrEmpty(AppCGCustomerCode) == false))
            {
                mycustom1.ChargeCode += "- iOS";
                mycustom1.Quantity = 1;
                mycustom1.EachAmount += 99;
                mycustom1.Description += "- iOS";
            }

            myonetimeinvoice.InvoiceCharges[number_of_charges] = new CustomChargePost();
            myonetimeinvoice.InvoiceCharges[number_of_charges] = mycustom1;

            CGError servererror2 = new CGError();
            //SERVER CALL TO THE ONE TIME INVOICE for both charges at once.
            Customer returnCustomer2 = CheddarGetter.CreateOneTimeInvoice(myonetimeinvoice, servererror2);


            if (String.IsNullOrEmpty(servererror2.Code) == false)
            {
                //CG.InnerHtml += "<li>ERROR:" + servererror2.Message;

                SubmitButton.Enabled = true;
                SubmitButton.Text = "Submit";

                //string clickHandler = "this.disabled = false; this.value=\'Submit\'; ";
                //SubmitButton.Attributes.Add("onclick", clickHandler);


                RadNotification1.Title = "WARNING";
                RadNotification1.Text = servererror2.Message;
                RadNotification1.Visible = true;
                RadNotification1.Show();


                return false;
            }

            //Store in the PaidServicesDB
            BillingUtil billingutil = new BillingUtil();

            //Record the new SKU but do not turn on the 'paid' field as yet.

            string confirm = returnCustomer2.Subscriptions[0].Invoices[1].PaidTransactionId.ToString();

            string sku = returnCustomer2.Subscriptions[0].SubscriptionsPlans[0].Code.ToString();
            billingutil.StorePaidServicesDB(confirm,sku,State,false);

            
            //Enable 10 day grace for App store Approval.
            Util util = new Util();
            util.SetFreeProductionExpiration(State, DateTime.Now.ToUniversalTime().AddDays(10.0D));


            //Send Email to Support.
            System.Text.StringBuilder body = new System.Text.StringBuilder("Customer Username: " + State["Username"].ToString() + "\n");
            body.Append("App Name: " + State["SelectedApp"].ToString() + "\n");
            body.Append("\n-- ViziApps Support");
            Email email = new Email();
            string status = email.SendEmail(State, State["TechSupportEmail"].ToString(), State["TechSupportEmail"].ToString(),"", "", "Customer submitted App for App Store Preparation", body.ToString(), "", false);
            
            
            return true;

           
           
        }//end try

        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message.ToString() + ex.StackTrace.ToString());
            
        }

        return false;
    }


    protected void iosBrandingCheckBox_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void AndroidBrandingCheckBox_CheckedChanged(object sender, EventArgs e)
    {

    }


    private void PageRefresh()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();

        if ((State["SelectedAppType"].ToString() == Constants.NATIVE_APP_TYPE) || (State["SelectedAppType"].ToString() == Constants.HYBRID_APP_TYPE))
        {
            if (State["SelectedDeviceType"].ToString() == Constants.ANDROID_PHONE || State["SelectedDeviceType"].ToString() == Constants.ANDROID_TABLET)
            {
                Android.Visible = true;
                ios.Visible = false;
            }

            if (State["SelectedDeviceType"].ToString() == Constants.IPHONE || State["SelectedDeviceType"].ToString() == Constants.IPAD)
            {
                ios.Visible = true;
                Android.Visible = false;
            }


            SubmitButton.Visible = true;

            PreFillBillingFormDetails();
        }
        else
        {
            RadNotification1.Title = "WARNING";
            RadNotification1.Text = "Web Apps cannot be prepared for a App Store submission. Skip this step and directly select a Publishing Service.";
            RadNotification1.Visible = true;
            RadNotification1.Show();
        }
    }
    

    protected void RadComboAppSelector_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        BillingUtil billingutil = new BillingUtil();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        try
        {
            if (ApplicationInit(State,RadComboAppSelector,AppID,AppCGCustomerCode) == true)
            {

                //Secondary check with CheddarGettar -to check if this ApplicationID was already used to create a CGCustomer => atleast branding step was done.
                CGError servererror = new CGError();
                Customer native_customer_details = CheddarGetter.GetCustomer(AppID, servererror);
                if (String.Compare(native_customer_details.Code, AppID, false) == 0)
                {
                    USERINFO.Visible = false;
                    RadNotification1.Title = "WARNING";
                    RadNotification1.Text = "This application has already been submitted to the App Store.";
                    RadNotification1.Visible = true;
                    RadNotification1.Show();
                }
                else
                {
                    USERINFO.Visible = true;
                    PageRefresh();
                }
            }
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
        Session["MainMenu"] = tab;
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
        CCFirstNameTextbox.Text = util.DecodeMySql(row["first_name"].ToString());
        CCLastNameTextBox.Text = util.DecodeMySql(row["last_name"].ToString());
        CCZipTextBox.Text = row["postal_code"].ToString();

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