using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using MobiFlex;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using Telerik.Web.UI;
using System.Drawing;

public partial class Dialogs_ConvertApp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HelpClick.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                       "../../Help/Design/AppTypeHelp.htm", 260, 530, false, false, false, false));
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
            XmlUtil x_util = new XmlUtil();
            AppType.Items.Clear();
            AppType.Items.Add(new RadComboBoxItem("Select ->", "->"));
            switch (State["SelectedAppType"].ToString())
            {
                case Constants.NATIVE_APP_TYPE:
                    AppType.Items.Add(new RadComboBoxItem("Web App Type (no accecss to native device resources)", Constants.WEB_APP_TYPE));
                    AppType.Items.Add(new RadComboBoxItem("Hybrid App Type", Constants.HYBRID_APP_TYPE));
                    break;
                case Constants.WEB_APP_TYPE:
                    AppType.Items.Add(new RadComboBoxItem("Native App Type", Constants.NATIVE_APP_TYPE));
                    AppType.Items.Add(new RadComboBoxItem("Hybrid App Type", Constants.HYBRID_APP_TYPE));
                     break;
                case Constants.HYBRID_APP_TYPE:
                     AppType.Items.Add(new RadComboBoxItem("Web App Type (no accecss to native device resources)", Constants.WEB_APP_TYPE));
                     AppType.Items.Add(new RadComboBoxItem("Native App Type", Constants.NATIVE_APP_TYPE));
                    break;
            }
        }
    }
    protected void AppType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        XmlUtil x_util = new XmlUtil();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();

            AppType.Text = e.Text;
            util.SetAppType(State, e.Value);
            string prev_app_type = State["SelectedAppType"].ToString();
            State["SelectedAppType"] = e.Value;
            switch (State["SelectedAppType"].ToString())
            {
                case Constants.NATIVE_APP_TYPE:
                    RedirectPage.Text = "TabDesignNative.aspx";
                    break;
                case Constants.WEB_APP_TYPE:
                    if (prev_app_type == Constants.NATIVE_APP_TYPE) 
                        x_util.ConvertNativeAppToWebApp(State);
                     RedirectPage.Text = "TabDesignWeb.aspx";
                    break;
                case Constants.HYBRID_APP_TYPE:
                   if (prev_app_type == Constants.NATIVE_APP_TYPE) 
                        x_util.ConvertNativeAppToWebApp(State);

                    RedirectPage.Text = "TabDesignHybrid.aspx";
                    break;
            }
             Message.Text = "The App Type has been changed...";
            //ajax return will close this dialog box and change page windows
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    private void ClearMessages()
    {
        Message.Text = "";
    }
}