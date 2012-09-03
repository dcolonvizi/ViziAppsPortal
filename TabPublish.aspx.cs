using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;


public partial class TabPublish : System.Web.UI.Page
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


            if ( HttpRuntime.Cache["TechSupportEmail"] != null)
            {
                util.AddEmailToButton(SupportButton,  HttpRuntime.Cache["TechSupportEmail"].ToString(), "Email To Tech Support");
            }

            util.UpdateSessionLog(State, "post", "TabPublish");

            if (!IsPostBack)
            {
                CopyRight.InnerText = HttpRuntime.Cache["CopyRight"].ToString();
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
        
        if (tab == "TabDesignHybrid.aspx" || tab == "TabDesignWeb.aspx")
            State["SelectedApp"] = null;

        Response.Redirect(tab, false);
    }



    protected void PublishMenu_ItemClick(object sender, RadMenuEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        string tab = e.Item.Value;
        Response.Redirect(tab, false);
    }
}
