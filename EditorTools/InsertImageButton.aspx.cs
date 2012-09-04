using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Editor.DialogControls;

public partial class InsertImageButton : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }
        State["AccountType"].ToString();

        if (!IsPostBack)
        {
            if (GoToPages.Items.Count == 0)
            {
                Init init = new Init();
                init.InitAppPagesAndCustom(State, GoToPages, false);
                init.InitAppPagesAndCustom(State, page_if_true, true);
                init.InitAppPagesAndCustom(State, page_if_false, true);
            }
            InitActions();
        }
    }
    protected void InitActions()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        actions.Items.Add(new RadComboBoxItem("Select action ->", ""));
        XmlUtil x_util = new XmlUtil();
        if (State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE)
        {
            actions.Items.Add(new RadComboBoxItem("Go to page", "next_page"));
            actions.Items.Add(new RadComboBoxItem("If/Then Go to page ", "if_then_next_page"));
            actions.Items.Add(new RadComboBoxItem("Go to previous page", "previous_page"));
            actions.Items.Add(new RadComboBoxItem("Get or send device data via a web data source", "post"));
        }
        else if (State["SelectedAppType"].ToString() == Constants.NATIVE_APP_TYPE)
        {
            actions.Items.Add(new RadComboBoxItem("Go to page", "next_page"));
            actions.Items.Add(new RadComboBoxItem("If/Then Go to page ", "if_then_next_page"));
            actions.Items.Add(new RadComboBoxItem("Go to previous page", "previous_page"));
            actions.Items.Add(new RadComboBoxItem("Get or send device data via a web data source", "post"));
            actions.Items.Add(new RadComboBoxItem("Call phone", "call"));
            actions.Items.Add(new RadComboBoxItem("Share a message through Facebook, Texting or Email using a popup menu", "share"));
            actions.Items.Add(new RadComboBoxItem("Email message", "email"));
            actions.Items.Add(new RadComboBoxItem("Text message", "sms"));
            actions.Items.Add(new RadComboBoxItem("Take a photo", "take_photo"));
            actions.Items.Add(new RadComboBoxItem("Capture a signature", "capture_signature"));
            actions.Items.Add(new RadComboBoxItem("Login to mobile commerce", "login_to_mcommerce"));
            actions.Items.Add(new RadComboBoxItem("Initialize card swiper for charge", "init_card_swiper"));
            actions.Items.Add(new RadComboBoxItem("Manually charge credit card", "manual_card_charge"));
            actions.Items.Add(new RadComboBoxItem("Void credit card charge", "void_charge"));

            if (State["AccountType"].ToString().Contains("kofax"))
            {
                //actions.Items.Add(new RadComboBoxItem("Capture documents from camera photos", "capture_doc"));
                actions.Items.Add(new RadComboBoxItem("Capture and Process documents from photos", "capture_process_document"));
                actions.Items.Add(new RadComboBoxItem("Manage document case", "manage_document_case"));
            }
            if (State["AccountType"].ToString().Contains("intuit"))
            {
                actions.Items.Add(new RadComboBoxItem("Pay with Intuit GoPayment App", "call_intuit_gopayment"));
            }

        }
        else if (State["SelectedAppType"].ToString() == Constants.HYBRID_APP_TYPE)
        {
            actions.Items.Add(new RadComboBoxItem("Go to page", "next_page"));
            actions.Items.Add(new RadComboBoxItem("If/Then Go to page ", "if_then_next_page"));
            actions.Items.Add(new RadComboBoxItem("Go to previous page", "previous_page"));
            actions.Items.Add(new RadComboBoxItem("Get or send device data via a web data source", "post"));
        }
    }
}
