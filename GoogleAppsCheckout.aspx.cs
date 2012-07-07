using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GCheckout.Checkout;
using GCheckout.Util;
using System.Xml;
using System.IO;
using System.Net;

public partial class GoogleAppsCheckout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
        
        string purchaseToken = Request.QueryString.Get("appsmarket.purchaseToken");
        if (purchaseToken != null)
        {
             ((Hashtable)HttpRuntime.Cache[Session.SessionID])["PurchaseToken"] = purchaseToken;
            /*AppsMarketService service = new AppsMarketService();
            service.appId = YOUR_APPLICATION_ID;
            service.consumerSecret = YOUR_CONSUMER_SECRET;

            // Assumes you are connecting to the sandbox. Use http://www.googleapis.com/appsmarket/v2/ to connect to production.
            service.endpoint = "https://www.googleapis.com/appsmarket/v2sandbox/";

            service.appName = YOUR_APP_NAME;
            service.consumerKey = service.appId + ".apps.googleusercontent.com";
            service.authorize();

            // Constructs a subscription request. In a complete application, this would be based on details the
            // customer has supplied on a product purchasing page.
            Subscription sub = new Subscription();
            sub.applicationId = service.appId;
            sub.customerId = customerId;
            sub.purchaseToken = purchaseToken;
            sub.name = "SaaSy App Subscription Update";
            sub.description = "SaaSy App Subscription Update";
            sub.purchaseToken = purchaseToken;
            sub.currencyCode = "USD";

            // Sets up the information to immediately charge the buyer a prorated upgrade amount.
            sub.initialCart = new InitialCart();
            sub.initialCart.cart = new Cart();
            sub.initialCart.cart.receiptName = "Prorated upgrade fee";
            sub.initialCart.cart.receiptDescription = "15 days remaining until next regularly scheduled subscription payment";
            LineItem item = new LineItem();
            item.name = "My Prorated Upgrade Fee";
            item.description = "Mid-term upgrade for SaaSy App";
            item.editionId = "premium"
            item.developerSku = "premium";
            item.price = 3000000L;
            item.seatCount = 3;
            sub.initialCart.cart.items = new ArrayList();
            sub.initialCart.cart.items.add(item);

            // Sets up the information for recurring charges to the buyer.
            // In this example, the first future billing occurs 30 days from now
            // and repeated billings will occur every month thereafter.
            sub.recurringCart = new RecurringCart();
            sub.recurringCart.cart = new Cart();
            sub.recurringCart.cart.receiptName = "Monthly subscription";
            sub.recurringCart.cart.receiptDescription = "Recurring charge to maintain subscription";
            item = new LineItem();
            item.name = "My Subscription";
            item.description = "Subscription billing for SaaSy App";
            item.editionId = "premium";
            item.developerSku = "premium";
            item.seatCount = 3; // Set this to -1 for a site license.
            item.price = 6000000L;
            sub.recurringCart.cart.items = new ArrayList();
            sub.recurringCart.cart.items.add(item);

            Subscription response = service.insertSubscription(sub);




            String customerId = "example.com";
            String purchaseToken = "AB-123";
            String edition = "premium";
            String developerSku = "premium";

            // Configure the base subscription.
            Subscription sub = new Subscription();
            sub.applicationId = service.appId;
            sub.customerId = customerId;
            sub.purchaseToken = purchaseToken;
            sub.name = service.appName + " Subscription";
            sub.description = "Example subscription description";
            sub.currencyCode = "USD";

            // Configure the initial cart (up front fees).
            sub.initialCart = new InitialCart();
            sub.initialCart.cart = new Cart();
            sub.initialCart.cart.receiptName = "Initial cart";
            sub.initialCart.cart.receiptDescription = "Initial cart description";
            LineItem item = new LineItem();
            item.name = "Setup fee";
            item.description = "Setup fee description";
            item.developerSku = "setupfee";
            item.price = 99000000L; // 99 USD, in micro-dollars
            item.seatCount = 1;
            sub.initialCart.cart.items = new ArrayList();
            sub.initialCart.cart.items.add(item);

            // Configure the recurring cart (recurring subscription).
            sub.recurringCart = new RecurringCart();
            sub.recurringCart.cart = new Cart();
            sub.recurringCart.cart.receiptName = "Recurring subscription";
            sub.recurringCart.cart.receiptDescription = "Recurring cart description";
            item = new LineItem();
            item.name = edition + " edition";
            item.description = edition + " edition description";
            item.editionId = edition;
            item.developerSku = developerSku;
            item.seatCount = 5;
            item.price = 250000000L; // 250 USD (50/seat), in micro-dollars
            sub.recurringCart.cart.items = new ArrayList();
            sub.recurringCart.cart.items.add(item);
            sub.recurringCart.firstChargeDays = 0; // Begin charging immediately
            sub.recurringCart.frequency = "MONTHLY"; // Charge once a month

            // Send the subscription to Google.
            Subscription response = service.insertSubscription(sub);*/
        }
    }
    protected void GCheckoutButton1_Click(object sender, ImageClickEventArgs e)
    {
        string url = "http://google.com/enterprise/marketplace/redirectWithSignature?productListingId=" +
            ConfigurationManager.AppSettings["GoogleAppsProductListingID"] +
            "&domain=" +  ((Hashtable)HttpRuntime.Cache[Session.SessionID])["Username"].ToString() +
            "&supportLink=" + HttpUtility.UrlEncode(ConfigurationManager.AppSettings["GoogleAppsCheckout2"]);
        Response.Redirect(url, false);
    }
    protected void RejectButton_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
       
        Response.Redirect("Default.aspx", true);
    }
}