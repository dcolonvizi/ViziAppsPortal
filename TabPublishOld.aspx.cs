using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;

<<<<<<< HEAD:TabPublish.aspx.cs

public partial class TabPublish : System.Web.UI.Page
=======
public partial class PublishOld : System.Web.UI.Page
>>>>>>> 282fcd8b95fa0aad979c8f1657ab542a20d084b7:TabPublishOld.aspx.cs
{

    // Data members

    protected void Page_Load(object sender, EventArgs e)
    {
        Init init = new Init();
        Util util = new Util();

        try
        {
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            
            if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;


            if (State["TechSupportEmail"] != null)
            {
                util.AddEmailToButton(SupportButton, State["TechSupportEmail"].ToString(), "Email To Tech Support");
            }

            util.UpdateSessionLog(State, "post", "TabPublishOld");

            if (!IsPostBack)
            {
                UserLabel.Text = State["Username"].ToString();
            }

         //   SeeAllFields.Attributes.Add("onclick", "javascript: PopUp('../Help/Design/ViewAllNativeFields.htm', 'height=800, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');return false;");
         //   LayoutVideo.Attributes.Add("onclick", "javascript: PopUp('../Help/Design/LayoutVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');return false;");
         //   BasicFieldsVideo.Attributes.Add("onclick", "javascript: PopUp('../Help/Design/BasicFieldsVideo.htm', 'height=325, width=570, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=no, resizable=no');return false;");

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            throw;
        }
    }

    protected void AppBrandingButton_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Billing/AppBrandingBilling.aspx", false);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            throw;
        }
    }

    protected void NewSubscriptionButton_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Billing/NewPublishingService.aspx", false);
            //Response.Redirect("~/Billing/AppBrandingBilling.aspx", false);
        }


        catch (ArgumentNullException ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.StackTrace.ToString() + ex.Message.ToString());
            throw;
        }

        catch (ArgumentException ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.StackTrace.ToString() + ex.Message.ToString());
            throw;
        }

        catch (HttpException ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.StackTrace.ToString() + ex.Message.ToString());
            throw;
        }

        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            throw;
        }

    }

    protected void ModifySubscriptionButton_Click(object sender, EventArgs e)
    {

        try
        {
            Response.Redirect("~/Billing/ModifyPublishingService.aspx", false);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            throw;
        }

    }

    protected void CancelSubscriptionButton_Click(object sender, EventArgs e)
    {

        try
        {
            Response.Redirect("~/Billing/CancelPublishingService.aspx", false);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            throw;
        }
    }

    protected void BillingHistoryButton_Click(object sender, EventArgs e)
    {

        try
        {
            Response.Redirect("~/Billing/ShowBillingHistory.aspx",false);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            throw;
        }
    }



    protected void PublishingFormButton_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Billing/PublishingForm.aspx", false);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
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
            Response.Redirect("LogoutForGoogleApps.aspx", false);
        else
            Response.Redirect("Default.aspx", false);

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

        Response.Redirect("Tab" + tab + ".aspx", false);
    }


  
}
