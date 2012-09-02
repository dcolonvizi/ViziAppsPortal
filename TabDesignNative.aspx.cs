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

public partial class DesignNative : System.Web.UI.Page
{
    string PagePanelStyle = "background-color:#EDF7FE; width: 452px";

    protected void Page_Load(object sender, EventArgs e)
    {
        Init init = new Init();
        Util util = new Util();
                    
        try
        {
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            if (util.CheckSessionTimeout(State,Response,"Default.aspx")) return;

            if ( HttpRuntime.Cache["TechSupportEmail"] != null)
            {
                util.AddEmailToButton(SupportButton,  HttpRuntime.Cache["TechSupportEmail"].ToString(), "Email To Tech Support");
            }

            util.UpdateSessionLog(State, "post", "TabDesign");

            ClearMessages();

            if (!IsPostBack)
            {
                CopyRight.InnerText = HttpRuntime.Cache["CopyRight"].ToString();
                string attr = "javascript: NamedPopUp('ManageData.aspx', 'ManageDataPopup','height=800, width=1000, left=50, top=50, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');return false;";
                ManageData.Attributes.Add("onclick", attr);

                UserLabel.Text = State["Username"].ToString();

                if (CurrentApp.Items.Count == 0 || CurrentApp.SelectedValue.Contains("->") ||
                              State["SelectedApp"] == null)
                {
                    init.InitAppsList(State, CurrentApp);
                }

                State["SelectedAppType"] = Constants.NATIVE_APP_TYPE;

                if (State["SelectedApp"] == null || !util.DoesAppExist(State) || CurrentApp.SelectedIndex == 0)
                {
                    InitCurrentApp("->");
                    State["SelectedDeviceType"] = Constants.IPHONE;
                    DeviceType.Text = State["SelectedDeviceType"].ToString();
                }
                else if (State["SelectedApp"] != null)
                {
                    InitCurrentApp(State["SelectedApp"].ToString());
                }
            }

            DeletePage.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this page?');");
 
            if (State["ResetConfigApps"] != null)
            {
                State["SelectedApp"] = null;
                init.InitAppsList(State, CurrentApp);
                State["ResetConfigApps"] = null;
            }

            State["WebServiceValidated"] = null;
            if (State["SelectedDeviceType"] == null)
            {
                State["SelectedDeviceType"] = Constants.IPHONE;
                DeviceType.Text = State["SelectedDeviceType"].ToString();
            }
            SetAllAppNames();
        }
        catch (Exception ex)
        {
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            util.ProcessMainExceptions(State, Response, ex);
        }
    }
    public void SetAllAppNames()
    {
        StringBuilder apps = new StringBuilder();
        foreach (RadComboBoxItem item in CurrentApp.Items)
        {
            if (item.Index == 0)
                continue;
            apps.Append(item.Value + ";");
        }
        if (apps.Length > 0)
        {
            apps.Remove(apps.Length - 1, 1);
            AllAppNames.Text = apps.ToString();
        }
        else
            AllAppNames.Text = "";
    }
    
    private void SetAppProperties()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if ( State["SelectedDeviceType"] == null)
            return;
        AppProperties.Items.Clear();
        AppProperties.Items.Add(new RadComboBoxItem("Select ->", ""));
        AppProperties.Items.Add(new RadComboBoxItem("App Description", "app_description"));
        AppProperties.Items.Add(new RadComboBoxItem("Set Main Device Type for App", "app_device"));

        if ( State["SelectedDeviceType"].ToString() == Constants.IPAD ||
            State["SelectedDeviceType"].ToString() == Constants.ANDROID_TABLET)
        {
            AppProperties.Items.Add(new RadComboBoxItem("App Background Color", "app_background_color"));
        }
        else
        {
            AppProperties.Items.Add(new RadComboBoxItem("App Background Image", "app_background_image"));
        }
        AppProperties.Items.Add(new RadComboBoxItem("Do Action on App Opening", "on_app_open"));
        AppProperties.Items.Add(new RadComboBoxItem("See Images Uploaded from Running App", "app_images"));
  
    }

    public void OnAppPagesChanged(Object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
 
        try
        {
            ClearMessages();
            if (SavedCanvasHtml.Text.Length > 0)
            {
                if (!SavePage())
                {
                     AppPages.Items.FindItemByValue(e.OldValue).Selected = true;
                    return;
                }
            }
            AppPagesChanged(Request.Form.Get("AppPages"));
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }

    public void AppPagesChanged(string page_name)
    {
        try
        {
            ClearMessages();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
             State["SelectedAppPage"] = page_name;
            AppPages.SelectedValue = page_name;
            ShowPage(page_name);
        }
        catch (Exception ex)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }

    public void ShowPage(string page_name)
    {
        try
        {
            XmlUtil x_util = new XmlUtil();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            if ( State["SelectedDeviceType"] == null /*||  State["SelectedDeviceView"] == null*/)
            {
                string device_type = x_util.GetAppDeviceType(State);
                 State["SelectedDeviceType"] = device_type;
                // State["SelectedDeviceView"] = device_type;
                 DeviceType.Text = State["SelectedDeviceType"].ToString();
            }

            if (page_name != null)
            {
                string html = x_util.GetAppPage(State, page_name);
                if(html.StartsWith("Error:"))
                {
                    Message.Text = html;
                    return;
                }
                State["PageHtml"] = html;
                Util util = new Util();
                if (State["DoSavePage"] != null)
                {
                    State["DoSavePage"] = null;
                    util.SavePageImage(State, page_name, html);
                }

                DefaultButtonImage.Text = util.GetDefaultButton(State);
            }
            else
            {
                 State["PageHtml"] = "";
                DefaultButtonImage.Text = ConfigurationManager.AppSettings["DefaultButtonImage"];
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.StartsWith("Error:"))
            {
                Message.Text = ex.Message;
                return;
            }
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    public void OnCurrentAppChanged(Object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
            if (SavedCanvasHtml.Text.Length > 0)
            {
                if (!SavePage())
                {
                    //check for no app
                    string selected_app = Request.Form.Get("CurrentApp");
                    if (selected_app.IndexOf("->") >= 0)
                    {
                        Message.Text = "Go back and edit the previous app. " + Message.Text;
                        InitCurrentApp(selected_app);
                        SavedCanvasHtml.Text = "";
                    }
                    else
                        InitCurrentApp(State["SelectedApp"].ToString());
                    return;
                }
            }
            string app = Request.Form.Get("CurrentApp");
            InitCurrentApp(app);
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    public void InitCurrentApp(string app)
    {
        try
        {
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            CurrentApp.SelectedValue = app;
            Util util = new Util();
            if (app.Contains("->"))
            {
                ResetAppStateVariables();
                StartMessage.Style.Value = "";
                State["SelectedDeviceType"] = Constants.IPHONE;
               // State["SelectedDeviceView"] = Constants.IPHONE;
                SetViewForDevice();     
                AppSelectedForTest.Visible = false;
                HideAppControls();
                return;
            }
            State["SelectedApp"] = app;
            State["SelectedAppType"] = util.GetAppType(State);
            switch (State["SelectedAppType"].ToString())
            {
                case Constants.NATIVE_APP_TYPE:
                    break;
                case Constants.WEB_APP_TYPE:
                    Response.Redirect("TabDesignWeb.aspx", false);
                    break;
                case Constants.HYBRID_APP_TYPE:
                    Response.Redirect("TabDesignHybrid.aspx", false);
                    break;
            }

            if (util.IsAppSelectedForTest(State))
                AppSelectedForTest.Visible = true;
            else
                AppSelectedForTest.Visible = false;

            XmlUtil x_util = new XmlUtil();
            util.GetStagingAppXml(State, app);
           // State["SelectedDeviceView"] = 
            State["SelectedDeviceType"] = x_util.GetAppDeviceType(State);
            if (State["SelectedDeviceType"] == null )
            {
                //State["SelectedDeviceView"] = 
                State["SelectedDeviceType"] = Constants.IPHONE;
            }
            DeviceType.Text = State["SelectedDeviceType"].ToString();
            SetViewForDevice();  
            InitAppPages();
            StartMessage.Style.Value = "display:none";

            string html = x_util.GetFirstAppPage(State);
            State["PageHtml"] = html;

            DefaultButtonImage.Text = util.GetDefaultButton(State);

            AppPages.SelectedValue = State["SelectedAppPage"].ToString();

            ShowAppControls();
        }
        catch (Exception ex)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    public void InitAppPages()
    {
        AppPages.Items.Clear();
        XmlUtil x_util = new XmlUtil();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string[] pages = x_util.GetAppPageNames(State,  State["SelectedApp"].ToString());

        foreach (string page in pages)
        {
            AppPages.Items.Add(new RadComboBoxItem(page, page));
        }
        if (pages.Length == 0)
             State["SelectedAppPage"] = null;

        if ( State["SelectedAppPage"] == null)
        {
            AppPages.SelectedIndex = 0;
             State["SelectedAppPage"] = AppPages.SelectedValue;
        }
        else
            AppPages.SelectedValue =  State["SelectedAppPage"].ToString();

    }
    private void ClearMessages()
    {
        Message.Text = "";
    }
    private string decodeUnicode(string inparam)
    {
        try
        {
            Encoding unicode = Encoding.Unicode;
            StringBuilder result = new StringBuilder();
            int i = 0;
            while (i < inparam.Length)
            {
                if (inparam[i] == '!')
                {
                    string hex = inparam.Substring(i + 1, 4);
                    byte[] hex_bytes = new byte[2];
                    hex_bytes[1] = Convert.ToByte(getHex(hex.Substring(0, 2)));
                    hex_bytes[0] = Convert.ToByte(getHex(hex.Substring(2)));
                    string one_char = Encoding.Unicode.GetString(hex_bytes);
                    result.Append(one_char);
                    i += 5;
                }
                else
                {
                    result.Append(inparam[i]);
                    i++;
                }
            }
            return result.ToString();
        }
        catch (Exception ex)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
            return "";
        }
    }
    private int hexValue(char c)
    {
        if (c >= '0' && c <= '9')
        {
            return (c - '0');
        }
        if (c >= 'a' && c <= 'f')
        {
            return (c - 'a') + 10;
        }
        if (c >= 'A' && c <= 'F')
        {
            return (c - 'A') + 10;
        }
        return -1;
    }
    private int getHex(string s)
    {
        int result = 0;
        for (int i = 0; i < 2; i++)
        {
            int j = hexValue(s[i]);
            result = (result * 16) + j;
        }
        return result;
    }
    private string UnicodeToAscii(string inparam)
    {
        if (inparam == null || inparam.Length == 0)
        {
            return "";
        }
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < inparam.Length; i++)
        {
            int ch = (int)inparam[i];
            if (ch == '!')
            {
                result.Append("!0021");
            }
            else if ((ch & 0xFF80) != 0)
            {
                result.Append("!");
                string hex = string.Format("{0:x4}", ch);
                result.Append(hex);
            }
            else
            {
                result.Append(inparam[i]);
            }
        }
        return result.ToString();
        
    }
    protected void DeleteApp_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
            string app = CurrentApp.SelectedValue;
            State["SelectedApp"] = app;
            util.DeleteApplication(State);

            ResetAppStateVariables();

            UpdateAppLists();
            CurrentApp.SelectedIndex = 0;
            StartMessage.Style.Value = "";
            AppPages.Items.Clear();
            Message.Text = app + " has been deleted.";
            util.SetDefaultBackgroundForView(State,Constants.IPHONE);
            State["SelectedDeviceType"] = Constants.IPHONE;
            AppSelectedForTest.Visible = false;
            State["SelectedApp"] = null;
            HideAppControls();
            SetViewForDevice();
            AppName.Text = "";

        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    protected bool CheckAppName(string app)
    {
        try
        {
            ClearMessages();
            if (app.Length == 0)
            {
                Message.Text = "Enter Application Name";
                return false;
            }

            //check for valid name
            if (!Check.ValidateObjectName(Message, app))
            {
                return false;
            }

            //check for previous name
            DB db = new DB();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            string sql = "SELECT * FROM applications WHERE customer_id='" + State["CustomerID"] + "' AND application_name='" + app + "'";
            string n_matches = db.ViziAppsExecuteScalar(State, sql);
            db.CloseViziAppsDatabase(State);
            if (n_matches != null && n_matches != "0")
            {
                Message.Text = "The app name " + app + " already exists.";
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
            return false;
        }
    }
    protected bool CheckPageName(string app, string page_name)
    {
        try
        {
            ClearMessages();
            if (page_name.Length == 0)
            {
                Message.Text = "Enter Page Name";
                return false;
            }

            //check for valid name
            if (!Check.ValidateObjectName(Message, page_name))
            {
                return false;
            }

            //check for previous name
            XmlUtil x_util = new XmlUtil();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            string[] page_names = x_util.GetAppPageNames(State, app);
            foreach (string name in page_names)
            {
                if (name == page_name)
                {
                    Message.Text = "Page " + page_name + " already exists.";
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
            return false;
        }
    }
    protected void SaveAppAs_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
             string app = AppName.Text.Trim();
            if (!CheckAppName(app))
                return;

            string page_name = PageName.Text.Trim().Replace(" ", "_");
            string designed_for = DeviceType.Text.Trim();
            string app_type = AppType.Text.Trim();

            if (IsNewApp.Text.Length > 0)
            {
                IsNewApp.Text = "";
                CurrentApp.SelectedIndex = 0;
                AppPages.Items.Clear();
                ResetAppStateVariables();
                SavedCanvasHtml.Text = "";
            }
            else
            {
                 if ( State["AppXmlDoc"] != null && !CheckPageName(app, page_name))
                    return;
            }

            State["SelectedApp"] = app;
            State["SelectedAppPage"] = page_name;
            State["SelectedDeviceType"] = designed_for;
           // State["SelectedDeviceView"] = designed_for;
            State["SelectedAppType"] = app_type;

            XmlUtil x_util = new XmlUtil();

            AppPages.SelectedValue = page_name;
            util.SetDefaultBackgroundForView(State,designed_for);
            
            if (!SaveAppInDatabase(app, page_name))
            {
                return;
            }

            switch (State["SelectedAppType"].ToString())
            {
                case Constants.NATIVE_APP_TYPE:
                    break;
                case Constants.WEB_APP_TYPE:
                    Response.Redirect("TabDesignWeb.aspx", false);
                    break;
                case Constants.HYBRID_APP_TYPE:
                      Response.Redirect("TabDesignHybrid.aspx", false);
                    break;
            }

            ShowAppControls();
            InitAppPages();

            Message.Text = app + " has been saved. ";
            AppName.Text = app;
            StartMessage.Style.Value = "display:none";

            if ( State["EncodeComputeWarnings"] != null)
            {
                Message.Text += " " +  State["EncodeComputeWarnings"].ToString();
                 State["EncodeComputeWarnings"] = null;
            }

            if ( State["CreatePageMessage"] != null)
            {
                StringBuilder sb = (StringBuilder) State["CreatePageMessage"];
                Message.Text += sb.ToString();
                 State["SelectedAppPage"] = page_name;
            }

            AppSelectedForTest.Visible = false;
            UpdateAppLists();
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    protected bool SaveAppInDatabase(string app, string page_name)
    {
        try
        {
            ClearMessages();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            State["CreatePageMessage"] = null;

            //save application
            Util util = new Util();
            XmlUtil x_util = new XmlUtil();

            State["PageHtml"] = x_util.FilterCanvasOutput(SavedCanvasHtml.Text);
            string html = State["PageHtml"].ToString();

            if (State["SelectedDeviceType"] == null /*||  State["SelectedDeviceView"] == null*/)
            {
                State["SelectedDeviceType"] = Constants.IPHONE;
                DeviceType.Text = State["SelectedDeviceType"].ToString();
            }

            util.CreateApp(State, page_name, State["SelectedDeviceType"].ToString(), AppDescription.Text);
            util.SetDefaultButton(State, DefaultButtonImage.Text);
            Hashtable duplicates = x_util.EncodeAppPageToAppXml(State, page_name, html);
            if (duplicates != null)
            {
                State["SelectedApp"] = app;

                util.DeleteApplication(State);

                ResetAppStateVariables();

                UpdateAppLists();
                AppPages.Items.Clear();

                StringBuilder errors = new StringBuilder();
                foreach (string duplicate in duplicates.Keys)
                {
                    errors.Append(duplicate + " internal name in current page also found on " + duplicates[duplicate].ToString() + " page; ");
                }
                Message.Text = errors.ToString() + " This application was not saved because duplicate identifiers are not allowed.";

                return false;
            }
            x_util.SetBackgroundImage(State);

            SetViewForDevice();
            ShowPage(page_name);
            CurrentApp.Items.Add(new RadComboBoxItem(app, app));
            CurrentApp.SelectedIndex = CurrentApp.Items.Count - 1;

            AppPages.Items.Clear();
            AppPages.Items.Add(new RadComboBoxItem(page_name, page_name));
            AppPages.SelectedIndex = 0;
            SavedCanvasHtml.Text = "";
            return true;
        }
        catch (Exception ex)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
            return false;
        }
    }
    protected void SaveApp_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
            if (SavedCanvasHtml.Text.Length > 0)
                SavePage();

            if (State["SelectedApp"] == null ||
                !util.DoesAppExist(State) ||
                CurrentApp.SelectedIndex == 0)
            {
                return;
            }

            if (State["CreatePageMessage"] != null)
            {
                StringBuilder sb = (StringBuilder)State["CreatePageMessage"];
                Message.Text += sb.ToString();
                InitAppPages();
            }
        }
        catch (Exception ex)
        {
             util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    protected void SetBackgroundPost_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ClearMessages();
        string background = Background.Text.Trim();
        if (State["SelectedDeviceType"] != null)
        {
            if ( State["SelectedDeviceType"].ToString() == Constants.ANDROID_PHONE)
                background = background.Replace("_iphone.", "_android.");
            if ( State["SelectedDeviceType"].ToString() == Constants.IPAD ||
                 State["SelectedDeviceType"].ToString() == Constants.ANDROID_TABLET)
                return;
        }
         State["BackgroundImageUrl"] = background;

        Background.Text = "";
        XmlUtil x_util = new XmlUtil();
        if ( State["SelectedAppPage"] != null)
        {
            x_util.SetBackgroundImage(State);
            ShowPage( State["SelectedAppPage"].ToString());
        }
        else
            ShowPage(null);
    }
    protected void SetViewForDeviceTypePost_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
        if (SavedCanvasHtml.Text.Length > 0)
             SavePage();

        SetViewForDevice();       
        ShowPage(State["SelectedAppPage"].ToString());
    }
    protected void SetViewForDevice()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State["SelectedDeviceType"] == null)
            State["SelectedDeviceType"] = Constants.IPHONE;
        switch (State["SelectedDeviceType"].ToString())
        {
            case Constants.IPHONE:
                DeviceMultiPage.SelectedIndex = 0;
                CanvasFrame.Attributes["style"] = "width:320px;height:460px;position:relative;left:33px;top:-592px;";
                if (State["BackgroundImageUrl"] == null)
                    State["BackgroundImageUrl"] = "https://s3.amazonaws.com/MobiFlexImages/apps/images/backgrounds/standard_w_header_iphone.jpg";
                else if (State["BackgroundImageUrl"].ToString().Contains("_android."))
                    State["BackgroundImageUrl"] = State["BackgroundImageUrl"].ToString().Replace("_android.", "_iphone.");
                break;
            case Constants.ANDROID_PHONE:
                DeviceMultiPage.SelectedIndex = 1;
                CanvasFrame.Attributes["style"] = "width:320px;height:508px;position:relative;left:28px;top:-591px;";
                if (State["BackgroundImageUrl"] == null)
                    State["BackgroundImageUrl"] = "https://s3.amazonaws.com/MobiFlexImages/apps/images/backgrounds/standard_w_header_android.jpg";
                else if (State["BackgroundImageUrl"].ToString().Contains("_iphone."))
                    State["BackgroundImageUrl"] = State["BackgroundImageUrl"].ToString().Replace("_iphone.", "_android.");
                break;
            case Constants.IPAD:
                DeviceMultiPage.SelectedIndex = 2;
                CanvasFrame.Attributes["style"] = "width:" + Constants.IPAD_SPLASH_PORTRAIT_WIDTH_S + "px;height:" + Constants.IPAD_SPLASH_PORTRAIT_HEIGHT_S + "px;position:relative;left:20px;top:-1089px;border:0px";
                State["BackgroundImageUrl"] = "Dialogs/CoverFlow/pics/standard_w_header_ipad.jpg";
                break;
            case Constants.ANDROID_TABLET:
                DeviceMultiPage.SelectedIndex = 3;
                CanvasFrame.Attributes["style"] = "width:" + Constants.ANDROID_TABLET_SPLASH_PORTRAIT_WIDTH_S + "px;height:" + Constants.ANDROID_TABLET_SPLASH_PORTRAIT_HEIGHT_S + "px;position:relative;left:12px;top:-1280px;border:0px";
                State["BackgroundImageUrl"] = "Dialogs/CoverFlow/pics/standard_w_header_android_tablet.jpg";
                break;
            default:
                DeviceMultiPage.SelectedIndex = 0;
                CanvasFrame.Attributes["style"] = "width:320px;height:460px;position:relative;left:33px;top:-592px;";
                if (State["BackgroundImageUrl"] == null)
                    State["BackgroundImageUrl"] = "https://s3.amazonaws.com/MobiFlexImages/apps/images/backgrounds/standard_w_header_iphone.jpg";
                else if (State["BackgroundImageUrl"].ToString().Contains("_android."))
                    State["BackgroundImageUrl"] = State["BackgroundImageUrl"].ToString().Replace("_android.", "_iphone.");
                break;

        }
        SetAppProperties();
    }
    protected void SaveAppPage_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
            string page_name = PageName.Text.Trim().Replace(" ", "_");
            State["SelectedAppPage"] = page_name;
            PageName.Text = "";
            if (SavedCanvasHtml.Text.Length > 0)
                SavePage();
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    protected bool SavePage()
    {
        try
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];

            util.SetDefaultButton(State, DefaultButtonImage.Text);
            XmlUtil x_util = new XmlUtil();
            //save application
            State["CreatePageMessage"] = null;

            string page_name = State["SelectedAppPage"].ToString();
            State["PageHtml"] = x_util.FilterCanvasOutput(SavedCanvasHtml.Text);
            string html = State["PageHtml"].ToString();

           Hashtable duplicates = x_util.EncodeAppPageToAppXml(State, page_name, html);
 
            Message.Text = "Page " + page_name + " has been saved. ";
            ShowPage(page_name);

            SavedCanvasHtml.Text = "";

            if (duplicates != null)
            {
                StringBuilder errors = new StringBuilder();
                foreach (string duplicate in duplicates.Keys)
                {
                    errors.Append(duplicate + " internal name in current page also found on " + duplicates[duplicate].ToString() + " page; ");
                }
                Message.Text += errors.ToString() + " Your app will not work with duplicate identifiers. Fix and then save this page again.";
            }


            if (State["EncodeComputeWarnings"] != null)
            {
                Message.Text += " " + State["EncodeComputeWarnings"].ToString();
                State["EncodeComputeWarnings"] = null;
            }

            InitAppPages();

            PagePanel.Style.Value = PagePanelStyle;
            return true;
        }
        catch (Exception ex)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
            return false;
        }
    }
    protected void AcceptRenameApp_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
            if (SavedCanvasHtml.Text.Length > 0)
                SavePage();
            AppName.Text = Request.Form.Get("AppName");
            string app = AppName.Text.Trim();
            if (!CheckAppName(app))
                return;
            util.RenameApplication(State, CurrentApp.SelectedValue, app);
            CurrentApp.Items[CurrentApp.SelectedIndex].Value = app;
            CurrentApp.Items[CurrentApp.SelectedIndex].Text = app;
            State["SelectedApp"] = app;
            SetAllAppNames();

            XmlUtil x_util = new XmlUtil();
            x_util.RenameApp(State);

            UpdateAppLists();
            AppName.Text = "";
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    protected void NewPage_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
            if (SavedCanvasHtml.Text.Length > 0)
            {
                if (!SavePage())
                    return;
            }
            string next_page = PageName.Text.Trim().Replace(" ", "_");
            PageName.Text = "";

            if (!CheckPageName(CurrentApp.SelectedValue, next_page))
            {
                return;
            }

            AppPages.SelectedValue = next_page;
            State["SelectedAppPage"] = next_page;
            XmlUtil x_util = new XmlUtil();

            AppPages.Items.Add(new RadComboBoxItem(next_page, next_page));
            AppPages.SelectedIndex = AppPages.Items.Count - 1;
            x_util.SaveAppPage(State, next_page, "");
            x_util.EncodeAppPageToAppXml(State, next_page, ""); 
            InitPage();
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }

    }
    protected void RenamePage_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ClearMessages();
        PagePanel.Style.Value = "display:none";
        PageName.Text = "";
    }
    protected void AcceptPageName_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
            if (SavedCanvasHtml.Text.Length > 0)
                SavePage();
            string next_page = PageName.Text.Trim().Replace(" ", "_");
            PageName.Text = "";

            if (!CheckPageName(CurrentApp.SelectedValue, next_page))
            {
                return;
            }
            XmlUtil x_util = new XmlUtil();

            x_util.RenameAppPage(State, State["SelectedAppPage"].ToString(), next_page);
            if (x_util.IsCurrentPageNameUsed(State))
            {
                x_util.ReplacePageNameUsedInSubmits(State, State["SelectedAppPage"].ToString(), next_page);
                Message.Text = "The new page name was changed for all actions that refer to it in your app.";
            }
            AppPages.SelectedValue = next_page;
            State["SelectedAppPage"] = next_page;
            State["DoSavePage"] = true;
            InitAppPages();

            PagePanel.Style.Value = PagePanelStyle;
            ShowPage(next_page);
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    protected void CancelRenamePage_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ClearMessages();
        PagePanel.Style.Value = PagePanelStyle;
        PageName.Text = "";
    }
    protected void DeletePage_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
            XmlUtil x_util = new XmlUtil();
            int n_pages = x_util.GetAppPageCount(State);
            if (n_pages == 1)
            {
                Message.Text = "A saved application must have at least 1 page. You can rename this page.";
                return;
            }
            if (x_util.IsCurrentPageNameUsed(State))
            {
                Message.Text = "The page was deleted but note that it was referred to, by an action on another page.";
            }
            x_util.DeleteAppPage(State, State["SelectedAppPage"].ToString());
            AppPages.SelectedIndex = 0;
            InitAppPages();

            State["SelectedAppPage"] = AppPages.SelectedValue;
            PageName.Text = State["SelectedAppPage"].ToString();

            string html = x_util.GetAppPage(State, State["SelectedAppPage"].ToString());
            if (html.StartsWith("Error:"))
            {
                Message.Text += html;
                return;
            }
            State["PageHtml"] = html;
            PageName.Text = "";
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    protected void InitPage()
    {
        PageName.Text = "";
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        State["PageHtml"] = "";
        DefaultButtonImage.Text = ConfigurationManager.AppSettings["DefaultButtonImage"];
    }
    protected void HideAppControls()
    {
        RenameApp.Style.Value = "display:none";
        DuplicateApp.Style.Value = "display:none";
        ConvertAppType.Style.Value = "display:none";
        DeleteApp.Style.Value = "display:none";
        PagePanel.Style.Value = "display:none";
        AppProperties.Style.Value = "display:none";
        AppPropertiesLabel.Style.Value = "display:none";
        SelectForTest.Style.Value = "display:none";
        ValidateFieldNames.Style.Value = "display:none";
        storyBoardPanel.Style.Value = "display:none";
    }
    protected void ShowAppControls()
    {
        RenameApp.Style.Value = "";
        DuplicateApp.Style.Value = "";
        ConvertAppType.Style.Value = "";
        DeleteApp.Style.Value = "";
        PagePanel.Style.Value = PagePanelStyle;
        AppProperties.Style.Value = "";
        AppPropertiesLabel.Style.Value = "";
        SelectForTest.Style.Value = "";
        ValidateFieldNames.Style.Value = "";
        storyBoardPanel.Style.Value = "height:1000px";
    }
  
    protected void ViewForDevice_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
            if (SavedCanvasHtml.Text.Length > 0)
                SavePage();
            XmlUtil x_util = new XmlUtil();
            string device_view = e.Value;
            if (State["SelectedDeviceType"].ToString() == Constants.IPAD)
                device_view = Constants.IPAD;
            else if (State["SelectedDeviceType"].ToString() == Constants.ANDROID_TABLET)
                device_view = Constants.ANDROID_TABLET;
            else if (device_view == Constants.IPAD && State["SelectedDeviceType"].ToString() != Constants.IPAD)
                device_view =  State["SelectedDeviceType"].ToString();

            State["SelectedDeviceType"] =  device_view;
            if ( State["SelectedApp"] == null ||  State["SelectedApp"].ToString().Contains("->"))
            {
                util.SetDefaultBackgroundForView(State,device_view);
            }
            else
                ShowPage( State["SelectedAppPage"].ToString());

        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    protected void DuplicateApp_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
            if (SavedCanvasHtml.Text.Length > 0)
                SavePage();
            State["SelectedApp"] = CurrentApp.SelectedValue;
            AppName.Text = Request.Form.Get("AppName");
            string new_name = AppName.Text.Trim();
            AppName.Text = "";
            util.CopyApp(State, new_name);
            Message.Text = new_name + " has been created. ";
            Init init = new Init();
             State["SelectedApp"] = new_name;
            UpdateAppLists();
            InitAppPages();
            AppSelectedForTest.Visible = false;
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    protected void DuplicatePage_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
            if (SavedCanvasHtml.Text.Length > 0)
            {
                if (!SavePage())
                    return;
            }
            string new_page_name = PageName.Text.Trim().Replace(" ", "_");
            PageName.Text = "";

            if (!CheckPageName(CurrentApp.SelectedValue, new_page_name))
            {
                return;
            }

            XmlUtil x_util = new XmlUtil();
            x_util.CopyAppPage(State, State["SelectedAppPage"].ToString(), new_page_name);

            Message.Text = new_page_name + " page has been created. ";
            State["SelectedAppPage"] = new_page_name;
            State["DoSavePage"] = true;
            InitAppPages();
            ShowPage(new_page_name);
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    protected void UpdateAppLists()
    {
        try
        {
            Init init = new Init();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            init.InitAppsList(State, CurrentApp);

            RadComboBox ProvisionApps = (RadComboBox) State["ProvisionApps"];
            init.InitAppsList(State, ProvisionApps);

            init.InitManageDataAppsList(State);
            SetAllAppNames();
        }
        catch (Exception ex)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    protected void NextPage_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ClearMessages();
        if (SavedCanvasHtml.Text.Length > 0)
        {
            if (!SavePage())
                return;
        }
        if (State["SelectedApp"] == null)
            return;

        int index = AppPages.SelectedIndex;
        if(index +1 < AppPages.Items.Count)
        {
            AppPages.SelectedIndex = index + 1;
             State["SelectedAppPage"] = AppPages.SelectedValue;
            ShowPage(AppPages.SelectedValue);
        }
    }
    protected void PreviousPage_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ClearMessages();
        if (SavedCanvasHtml.Text.Length > 0)
        {
            if (!SavePage())
                return;
        }
        if (State["SelectedApp"] == null)
            return;

        int index = AppPages.SelectedIndex;
        if (index - 1 >= 0)
        {
            AppPages.SelectedIndex = index - 1;
             State["SelectedAppPage"] = AppPages.SelectedValue;
            ShowPage(AppPages.SelectedValue);
        }
    }
    protected void MovePageDown_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ClearMessages();
         if (SavedCanvasHtml.Text.Length > 0)
         {
             if (!SavePage())
                 return;
         }
         if (State["SelectedApp"] == null)
            return;
        XmlUtil x_util = new XmlUtil();
        x_util.MovePageDown(State);
        InitAppPages();
    }
    protected void MovePageUp_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ClearMessages();
        if (SavedCanvasHtml.Text.Length > 0)
        {
            if (!SavePage())
                return;
        }
        if (State["SelectedApp"] == null)
            return;

        XmlUtil x_util = new XmlUtil();
        x_util.MovePageUp(State);
        InitAppPages();
    }
    public void ResetAppStateVariables()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        State["SelectedApp"] = null;
         State["SelectedAppPage"] = null;
         State["BackgroundImageUrl"] = null;
         State["AppXmlDoc"] = null;
         State["SelectedDeviceType"] = null;
        // State["SelectedDeviceView"] = null;
         DeviceType.Text = "";
         InitPage();
        AppName.Text = "";
        PageName.Text = "";
        AppType.Text = "";
    }
    protected void SelectForTest_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (SavedCanvasHtml.Text.Length > 0)
            SavePage();
        util.SelectForTesting(State);
        AppSelectedForTest.Visible = true;
    }
   /* protected void GoToWebAppDesign_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (SavedCanvasHtml.Text.Length > 0)
        {
            if (!SavePage())
                return;
        }
        if (State["SelectedApp"] != null &&
            !State["SelectedApp"].ToString().Contains("->") &&
            SaveBeforeChangingAppType.Text.Length > 0)
        {
            SaveBeforeChangingAppType.Text = "";
           // util.SetAppType(State, Constants.WEB_APP_TYPE);
        }
        else
        {
            InitCurrentApp("->");
        }
        Response.Redirect("TabDesignWeb.aspx", false);
    }*/
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
    protected void MenuButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (SavedCanvasHtml.Text.Length > 0)
            SavePage();

        string tab = MenuValue.Text;
        Session["MainMenu"] = tab;
        if (tab == "DesignHybrid" || tab == "DesignWeb")
            State["SelectedApp"] = null;

        Response.Redirect("Tab" + tab + ".aspx", false);
    }
    protected void GoToPage_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (SavedCanvasHtml.Text.Length > 0)
        {
            if (!SavePage())
                return;
        }
        AppPagesChanged(PageName.Text);
    }
    protected void ValidateFieldNames_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (SavedCanvasHtml.Text.Length > 0)
            SavePage();
        XmlUtil x_util = new XmlUtil();
        x_util.ValidateFieldNames(State, Message); 
    }
    protected void Undo_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        SavedCanvasHtml.Text = "";
        ShowPage(State["SelectedAppPage"].ToString());
    }
    protected void DisplayMode_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        ShowPage(State["SelectedAppPage"].ToString());
    } 
}