using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Net;
using System.Data;
using System.Drawing;
using Amazon.DynamoDB.DocumentModel;

/// <summary>
/// Summary description for PhoneWebService
/// </summary>
[WebService(Namespace = "http://viziapps.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class PhoneWebService : System.Web.Services.WebService
{
    public PhoneWebService()
    {
    }

    private class Styles
    {
        public Styles(string style_string)
        {
            string[] split = style_string.Split(";".ToCharArray());
            foreach (string style_field in split)
            {
                string[] parts = style_field.Split(":".ToCharArray());
                switch (parts[0].Trim())
                {
                    case "width":
                        width = parts[1].Trim();
                        break;
                    case "height":
                        height = parts[1].Trim();
                        break;
                    case "font-family":
                        font_family = parts[1].Trim();
                        break;
                    case "font-size":
                        font_size = parts[1].Trim();
                        break;
                    case "font-weight":
                        font_weight = parts[1].Trim();
                        break;
                }
            }
        }
        public string width;
        public string height;
        public string font_family;
        public string font_size;
        public string font_weight;
    }

    [WebMethod(EnableSession = true)]
    public XmlDocument Login()
    {
        Init init = new Init();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        init.InitSkuConfigurations(State);
         HttpRuntime.Cache["TempFilesPath"] = Server.MapPath(".") + @"\temp_files\";
        Util util = new Util();
        XmlUtil x_util = new XmlUtil();
        XmlNode status = null;
        XmlDocument Design = null;

        try
        {
            DB db = new DB();

            HttpRequest request = Context.Request;

            string viziapps_version = request.QueryString.Get("viziapps_version");
            if (viziapps_version == null)
                viziapps_version = request.QueryString.Get("mobiflex_version");

            string device_id = request.QueryString.Get("deviceid");
            string device_model = request.QueryString.Get("device_model");
            string customer_username = request.QueryString.Get("customer");
            string app_status = (customer_username != null && customer_username.Length > 0) ? "production" : "staging";
            string application_name = request.QueryString.Get("app");
            string application_id = request.QueryString.Get("app_id");
            string unlimited = request.QueryString.Get("unlimited");
            string device_version = request.QueryString.Get("device_version");
            if (application_id == null)
                application_id = "";

            string sql = null;
            DataRow[] rows = null;
            string customer_id = null;
            string user_id = null;
            string user = request.QueryString.Get("user");
            string password = request.QueryString.Get("pwd");

            string display_width = request.QueryString.Get("display_width");
            if (display_width == null)
                display_width = "320";

            string display_height = request.QueryString.Get("display_height");
            if (display_height == null)
                display_height = "480";

            if (device_model == null)
                State["SelectedDeviceType"] = Constants.IPHONE;
            else if (device_model.ToLower().Contains("iphone") || device_model.ToLower().Contains("ipod"))
                State["SelectedDeviceType"] = Constants.IPHONE;
            else if (device_model.ToLower().Contains("ipad"))
                State["SelectedDeviceType"] = Constants.IPAD;
            else if (Convert.ToInt32(display_width) > 600)
                State["SelectedDeviceType"] = Constants.ANDROID_TABLET;
            else
                State["SelectedDeviceType"] = Constants.ANDROID_PHONE;

            if (unlimited == null || unlimited != "true")
            {
                if (user == null || password == null)
                {
                    Design = new XmlDocument();
                    XmlNode root2 = Design.CreateElement("login_response");
                    Design.AppendChild(root2);
                    status = x_util.CreateNode(Design, root2, "status", "Either the username or the password: " + password + " is incorrect.");
                    SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, null, null, "app login: bad credentials");
                    return Design;
                }
            }

            if (app_status == "production")
            {
                util.GetProductionAccountInfo(State, customer_username);
                if (customer_id == null)
                    customer_id = State["CustomerID"].ToString();
                //State["Username"] = customer_username;
                //customer_id = util.GetCustomerIDFromUsername(State, customer_username);
                //State["CustomerID"] = customer_id;
                //string account_status = util.GetCustomerStatus(State);
                // if (account_status == "inactive")
                if (State["AccountStatus"].ToString() == "inactive")
                {
                    SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, null, null, "app login: account inactive");
                    throw new System.InvalidOperationException("Your customer account is inactive.");
                }
                util.GetProductionAppInfo(State, application_name);
                application_id = State["AppID"].ToString();

                if (State["IsProductionAppPaid"] != null && State["IsProductionAppPaid"].ToString() != "true")
                {
                    //if (!util.IsFreeProductionValid(State, application_id))
                    if (State["IsFreeProductionValid"] != null && State["IsFreeProductionValid"].ToString() != "true")
                    {
                        SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, null, null, "app login: publishing service expired");
                        throw new System.InvalidOperationException("The publishing service for your app has expired.");
                    }
                }

                if (unlimited == null || unlimited != "true")
                {
                    //check username and password
                    // sql = "SELECT * FROM users WHERE username='" + user.ToLower() + "' AND password='" + util.MySqlFilter(password) +
                    //     "' AND application_id='" + application_id + "'";

                    //rows = db.ViziAppsExecuteSql(State, sql);
                    //if (rows.Length == 0)
                    if (State["Password"] == null)
                    {
                        //db.CloseViziAppsDatabase(State);
                        SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, null, null, "app login: bad credentials");
                        throw new System.InvalidOperationException("Either the username or the password: " + password + " is incorrect.");
                    }

                    //check number of users -- unlimited use never needs a login
                    //bool use_1_user_credential = util.GetUse1UserCredential(State, application_id);
                    //if (use_1_user_credential)
                    if (State["Use1UserCredential"] != null && State["Use1UserCredential"].ToString() == "true")
                    {
                        Hashtable features = util.IsProductionAppPaid(State, application_id);
                        DataRow row = rows[0];
                        sql = "SELECT COUNT(*) FROM users_device_ids WHERE user_id='" + row["user_id"].ToString() + "'";
                        int device_count = Convert.ToInt32(db.ViziAppsExecuteScalar(State, sql));

                        sql = "SELECT COUNT(*) FROM users_device_ids WHERE device_id='" + device_id + "'";
                        string device_exists = db.ViziAppsExecuteScalar(State, sql);

                        if (device_exists == "0")
                        {
                            if (device_count >= (int)features["max_users"])
                            {
                                db.CloseViziAppsDatabase(State);
                                SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, null, null, "app login: reached limit of users");
                                throw new System.InvalidOperationException("Cannot download app: reached limit of users.");
                            }
                            else
                            {
                                sql = "INSERT INTO users_device_ids SET device_id='" + device_id + "',user_id='" + row["user_id"].ToString() + "'";
                                db.ViziAppsExecuteNonQuery(State, sql);
                            }
                        }
                        //else app is allowed
                    }
                }
            }
            else //staging
            {
                sql = "SELECT * FROM customers WHERE username='" + user.ToLower() + "'";
                rows = db.ViziAppsExecuteSql(State, sql);
                if (rows.Length == 0)
                {
                    db.CloseViziAppsDatabase(State);
                    SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, null, null, "app login: user not registered");
                    throw new Exception("The username " + user.ToLower() + " is not registered. Go to www.viziapps.com and create a free account.");
                }

                DataRow row = rows[0];
                if (row["password"].ToString() != password)
                {
                    db.CloseViziAppsDatabase(State);
                    SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, null, null, "app login: bad credentials");
                    throw new Exception("Either the username or the password: " + password + " is incorrect.");
                }
                if (row["status"].ToString() == "inactive")
                {
                    db.CloseViziAppsDatabase(State);
                    SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, null, null, "app login: account is inactive");
                    throw new Exception("Your account is inactive. Contact ViziApps to re-activate your account.");
                }
                customer_id = row["customer_id"].ToString();
                State["CustomerID"] = customer_id;
            }


            //user is now logged in

            if (app_status == "staging")
            {
                sql = "SELECT application_id FROM applications WHERE " +
                   "in_staging=1 AND customer_id='" + customer_id + "'";

                application_id = db.ViziAppsExecuteScalar(State, sql);
                if (application_id == null)
                {
                    db.CloseViziAppsDatabase(State);
                    SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, null, null, "app login: no app selected");
                    throw new System.InvalidOperationException("You need to select an app to test, on the design page of your ViziApps Studio account.");
                }
            }

            db.CloseViziAppsDatabase(State);

            //get design
            if (State["AppDesignURL"] == null)
            {
                Design = GetDesign(application_id, user_id, customer_id, Convert.ToInt32(display_width), Convert.ToInt32(display_height), app_status, null);
                //save design in a file if production
                if (app_status == "production")
                {
                    util.SaveProductionAppInfo(State, application_name, Design);
                }
            }
            else
            {
                Design = new XmlDocument();
                Design.LoadXml(util.GetWebPage(State["AppDesignURL"].ToString()));
            }
            if (Design == null)
            {
                Design = new XmlDocument();
                XmlNode root2 = Design.CreateElement("login_response");
                Design.AppendChild(root2);
                SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, null, null, "app login: no app selected");
                status = x_util.CreateNode(Design, root2, "status", "You need to select an app to test, on the design page of your ViziApps Studio account.");
            }
            else
                SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, null, null, "app login: design downloaded");

        }
        catch (System.Exception SE)
        {
            util.LogError(State, SE);

            if (status == null)
            {
                Design = new XmlDocument();
                XmlNode root2 = Design.CreateElement("login_response");
                Design.AppendChild(root2);
                status = x_util.CreateNode(Design, root2, "status");

            }
            status.InnerText = SE.Message;
            util.LogError(State, SE);
        }
        return Design;
    }
    [WebMethod(EnableSession = true)]
    public XmlDocument Report()
    {
        Init init = new Init();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        init.InitSkuConfigurations(State);
         HttpRuntime.Cache["TempFilesPath"] = Server.MapPath(".") + @"\temp_files\";
        Util util = new Util();
        XmlUtil x_util = new XmlUtil();
        XmlNode status_node = null;
        XmlDocument Report = new XmlDocument();
        XmlNode root = Report.CreateElement("report_response");
        Report.AppendChild(root);
        try
        {
            DB db = new DB();

            HttpRequest request = Context.Request;
            string application_id = request.QueryString.Get("appid");
            string application_name = request.QueryString.Get("app");
            string isproduction = request.QueryString.Get("isproduction");
            string username = request.QueryString.Get("customer");
            string user_id = request.QueryString.Get("userid");
            string device_id = request.QueryString.Get("deviceid");
            string device_version = request.QueryString.Get("device_version");
            string device_model = request.QueryString.Get("device_model");

            string viziapps_version = request.QueryString.Get("viziapps_version");
            if (viziapps_version == null)
                viziapps_version = request.QueryString.Get("mobiflex_version");

            string latitude = request.QueryString.Get("latitude");
            string longitude = request.QueryString.Get("longitude");

            string app_status = "staging";
            if (isproduction == "yes")
            {
                app_status = "production";
            }

            string customer_id = request.QueryString.Get("custid");
            if (app_status == "production")
            {
                util.GetProductionAccountInfo(State, username);
                util.GetProductionAppInfo(State, application_name);
                application_id = State["AppID"].ToString();

                if (State["IsProductionAppPaid"] != null && State["IsProductionAppPaid"].ToString() != "true")
                {
                    //if (!util.IsFreeProductionValid(State, application_id))
                    if (State["IsFreeProductionValid"] != null && State["IsFreeProductionValid"].ToString() != "true")
                    {
                        x_util.CreateNode(Report, root, "status", "kill");
                        x_util.CreateNode(Report, root, "status_message", "The account for this app is inactive. Contact ViziApps to re-activate your account.");
                        SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, latitude, longitude, "app killed due to inactive account");
                        throw new System.InvalidOperationException("The publishing service for your app has expired.");
                    }
                }
                if (State["AccountStatus"].ToString() == "inactive")
                {
                    x_util.CreateNode(Report, root, "status", "kill");
                    x_util.CreateNode(Report, root, "status_message", "The account for this app is inactive. Contact ViziApps to re-activate your account.");
                    SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, latitude, longitude, "app killed due to inactive account");
                    return Report;
                }
            }
            //else app is staging
            else if (customer_id != null && customer_id.Length > 0)
            {
                State["CustomerID"] = customer_id;
                string active_sql = "SELECT COUNT(*) FROM customers where customer_id='" + customer_id + "' AND status!='inactive'";
                string active_count = db.ViziAppsExecuteScalar(State, active_sql);
                if (active_count == "0")
                {
                    x_util.CreateNode(Report, root, "status", "kill");
                    x_util.CreateNode(Report, root, "status_message", "The account for this app is inactive. Contact ViziApps to re-activate your account.");
                    SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, latitude, longitude, "app killed due to inactive account");
                    return Report;
                }
            }

            string display_width = request.QueryString.Get("display_width");
            if (display_width == null)
                display_width = "320";

            string display_height = request.QueryString.Get("display_height");
            if (display_height == null)
                display_height = "480";

            if (device_model == null)
                State["SelectedDeviceType"] = Constants.IPHONE;
            else if (device_model.ToLower().Contains("iphone") || device_model.ToLower().Contains("ipod"))
                State["SelectedDeviceType"] = Constants.IPHONE;
            else if (device_model.ToLower().Contains("ipad"))
                State["SelectedDeviceType"] = Constants.IPAD;
            else if (Convert.ToInt32(display_width) > 600)
                State["SelectedDeviceType"] = Constants.ANDROID_TABLET;
            else
                State["SelectedDeviceType"] = Constants.ANDROID_PHONE;

            if (application_id != null && application_id.Length > 0)
            {
                string sql = null;
                if (app_status == "staging")
                {
                    sql = "SELECT status FROM applications WHERE application_id='" + application_id + "'";
                    string staging_status = db.ViziAppsExecuteScalar(State, sql);
                    if (staging_status == null || (!staging_status.Contains("staging") && customer_id != null))
                    {
                        sql = "SELECT application_id FROM applications WHERE customer_id='" + customer_id + "' AND status LIKE '%staging%'";
                        string new_application_id = db.ViziAppsExecuteScalar(State, sql);
                        if (new_application_id != null)
                        {
                            XmlDocument Design = GetDesign(new_application_id, user_id, customer_id, Convert.ToInt32(display_width), Convert.ToInt32(display_height), app_status, null);
                            if (Design != null)
                            {
                                Design.SelectSingleNode("//status").InnerText = "update_app";
                                SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, latitude, longitude, "app updated");
                            }
                            else
                            {
                                Design = new XmlDocument();
                                XmlNode root2 = Design.CreateElement("report_response");
                                Design.AppendChild(root2);
                                x_util.CreateNode(Design, root2, "status", "kill");
                                x_util.CreateNode(Design, root2, "status_message", "Application no longer exists.");
                                SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, latitude, longitude, "app does not exist");
                            }
                            return Design;
                        }
                    }
                    db.CloseViziAppsDatabase(State);
                }
            }

            string app_time_stamp = request.QueryString.Get("app_time_stamp");
            if (app_time_stamp != null && app_time_stamp.Length > 0)
            {
                string date_time_modified = null;
                if (app_status == "staging")
                    date_time_modified = util.GetStagingAppTimeStamp(State, application_id);
                else
                {
                    date_time_modified = State["DateTimeModified"].ToString();
                }
                DateTime AppDateTime;
                bool isGoodAppDateTime= DateTime.TryParse(app_time_stamp, out AppDateTime);
                DateTime DateTimeModified;
                bool isGoodDateTimeModified = DateTime.TryParse(date_time_modified, out DateTimeModified);
                if (isGoodAppDateTime && isGoodDateTimeModified && AppDateTime != DateTimeModified)
                { // assuming that there is a newer version
                    XmlDocument Design = null;
                    if (app_status == "staging")
                    {
                        Design = GetDesign(application_id, user_id, customer_id, Convert.ToInt32(display_width), Convert.ToInt32(display_height), app_status, date_time_modified);
                    }
                    else
                    {
                        Design = new XmlDocument();
                        Design.LoadXml(util.GetWebPage(State["AppDesignURL"].ToString()));
                    }
                    if (Design != null)
                    {
                        Design.SelectSingleNode("//status").InnerText = "update_app";
                        SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, latitude, longitude, "app updated");
                    }
                    else
                    {
                        Design = new XmlDocument();
                        XmlNode root2 = Design.CreateElement("report_response");
                        Design.AppendChild(root2);
                        x_util.CreateNode(Design, root2, "status", "kill");
                        x_util.CreateNode(Design, root2, "status_message", "Application no longer exists.");
                        SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, latitude, longitude, "app does not exist");
                    }

                    return Design;
                }
                else
                    SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, latitude, longitude, "app opened");
            }
            else
                SaveReport(State, application_id, app_status, customer_id, user_id, device_id, device_model, device_version, viziapps_version, latitude, longitude, "app opened");


            string status = "OK";

            //check for unlimited use
            if (app_status == "production")
            {
                if (State["HasUnlimitedUsers"].ToString() == "true")
                    status += " unlimited";
            }

            status_node = x_util.CreateNode(Report, root, "status", status);
        }
        catch (System.Exception SE)
        {
            util.LogError(State, SE);
            if (status_node == null)
            {
                Report = new XmlDocument();
                XmlNode root2 = Report.CreateElement("report_response");
                Report.AppendChild(root2);
                status_node = x_util.CreateNode(Report, root2, "status");

            }
            status_node.InnerText = SE.Message + ": " + SE.StackTrace;
        }

        return Report;
    }
    private void SaveReport(Hashtable State, String application_id, String app_status, String customer_id, String user_id,
        String device_id, String device_model, String device_version, String viziapps_version,
        String latitude, String longitude, String app_use)
    {
        if (device_id == null)
            return;

        Document DDBDoc = new Document();
        DDBDoc["report_id"] = Guid.NewGuid().ToString();
        DDBDoc["app_use"] = app_use;
        DDBDoc["report_date_time"] = DateTime.UtcNow.ToString("s") + "Z";
        DDBDoc["app_id"] = application_id;
        DDBDoc["app_status"] = app_status;
        DDBDoc["customer_id"] = customer_id;
        DDBDoc["user_id"] = user_id;
        DDBDoc["device_id"] = device_id;
        DDBDoc["device_model"] = device_model;
        DDBDoc["device_version"] = device_version;
        DDBDoc["viziapps_version"] = viziapps_version;
        if (latitude != null && latitude.Length > 0)
            DDBDoc["gps_latitude"] = latitude;
        if (longitude != null && longitude.Length > 0)
            DDBDoc["gps_longitude"] = longitude;
        DynamoDB ddb = new DynamoDB();
        if (device_id != null && device_id.Length > 0)
            ddb.PutItem(State, "mobile_app_usage", DDBDoc);
        if (customer_id != null && customer_id.Length > 0)
            ddb.PutItem(State, "customer_usage", DDBDoc);
        if (application_id != null && application_id.Length > 0)
            ddb.PutItem(State, "app_usage", DDBDoc);

        /*StringBuilder sb_sql = new StringBuilder("INSERT INTO reports SET ");
        sb_sql.Append("report_id='" + Guid.NewGuid().ToString() + "'");
        DateTime now = DateTime.Now.ToUniversalTime();
        sb_sql.Append(",report_date_time='" + now.ToString("u").Replace("Z", "") + "'");
        sb_sql.Append(",application_id='" + application_id + "'");
        sb_sql.Append(",app_status='" + app_status + "'");
        sb_sql.Append(",customer_id='" + customer_id + "'");
        sb_sql.Append(",user_id='" + user_id + "'");
        sb_sql.Append(",device_id='" + device_id + "'");
        sb_sql.Append(",device_model='" + device_model + "'");
        sb_sql.Append(",device_version='" + device_version + "'");
        sb_sql.Append(",mobiflex_version='" + mobiflex_version + "'");
        if (latitude != null && latitude.Length > 0)
            sb_sql.Append(",gps_latitude='" + latitude + "'");
        if (longitude != null && longitude.Length > 0)
            sb_sql.Append(",gps_longitude='" + longitude + "'");

        db.ViziAppsExecuteNonQuery(State, sb_sql.ToString());
        db.CloseViziAppsDatabase(State);*/
    }

    [WebMethod(EnableSession = true)]
    public XmlDocument GetCustomerInfo()
    {
        XmlUtil x_util = new XmlUtil();
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        XmlNode status = null;
        XmlDocument Response = new XmlDocument();
        XmlNode root = Response.CreateElement("response");
        Response.AppendChild(root);
        try
        {
            DB db = new DB();
            String sql = "SELECT COUNT(*) FROM customers WHERE status!='inactive'";
            String count = db.ViziAppsExecuteScalar(State, sql);
            x_util.CreateNode(Response, root, "customer_count", count);
            db.CloseViziAppsDatabase(State);
            x_util.CreateNode(Response, root, "status", "success");
        }
        catch (System.Exception SE)
        {
            util.LogError(State, SE);

            if (status == null)
            {
                Response = new XmlDocument();
                XmlNode root2 = Response.CreateElement("response");
                Response.AppendChild(root2);
                status = x_util.CreateNode(Response, root2, "status");

            }
            status.InnerText = SE.Message;
            util.LogError(State, SE);
        }
        return Response;
    }
    protected XmlDocument GetDesign(string application_id, string user_id, string customer_id,
        int device_display_width, int device_display_height, string app_status, string time_stamp)
    {
        XmlUtil x_util = new XmlUtil();
        Util util = new Util();
        DB db = new DB();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string sql = "SELECT application_name,application_type FROM applications WHERE application_id='" + application_id + "'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        DataRow row = rows[0];
        string application_name = row["application_name"].ToString();
        string application_type = row["application_type"].ToString();
        State["SelectedApp"] = application_name;

        XmlDocument Design = null;
        if (app_status == "staging")
        {
            Design = util.GetStagingAppXml(State, application_name);
        }
        else
        {
            Design = util.GetProductionAppXml(State, application_name);
        }

        if (Design == null)
            return null;

        if (application_type == Constants.HYBRID_APP_TYPE)
        {
            WebAppsUtil w_util = new WebAppsUtil();
            State["SelectedAppType"] = Constants.HYBRID_APP_TYPE;
             HttpRuntime.Cache["NewWebAppHtml"] = File.ReadAllText(Server.MapPath(".") + @"\App_Data\NewViziAppsWebApp.txt");
             HttpRuntime.Cache["NewHybridAppXml"] = File.ReadAllText(Server.MapPath(".") + @"\App_Data\NewViziAppsHybridApp.xml");
             HttpRuntime.Cache["ShareThisScripts"] = File.ReadAllText(Server.MapPath(".") + @"\App_Data\ShareThisScripts.txt");
             HttpRuntime.Cache["TempFilesPath"] = Server.MapPath(".") + @"\temp_files\";

            State["Username"] = util.GetUsernameFromCustomerID(State, customer_id);
            //get original design display width and height
            string device_design_width = Design.SelectSingleNode("//configuration/device_design_width").InnerText;
            string device_design_height = Design.SelectSingleNode("//configuration/device_design_height").InnerText;
            double x_size_factor = 1.0D;
            double y_size_factor = 1.0D;
            if (device_display_width > 600)
            {
                x_size_factor = Convert.ToDouble(device_display_width) / Convert.ToDouble(device_design_width);
                y_size_factor = Convert.ToDouble(device_display_height) / Convert.ToDouble(device_design_height);
            }
            if (app_status == "production")
                State["IsProduction"] = true;
            else
                State["IsProduction"] = false;
            string html = w_util.GetWebApp(State, Design, x_size_factor, y_size_factor);
            Design = x_util.GenerateHybridAppXml(State, Design, device_display_width.ToString(), device_display_height.ToString(), html);
        }
        XmlNode configuration = Design.SelectSingleNode("//configuration");

        if (user_id != null && user_id.Length > 0)
            x_util.CreateNode(Design, configuration, "user_id", user_id);
        x_util.CreateNode(Design, configuration, "customer_id", customer_id);
        XmlNode app_node = Design.SelectSingleNode("//application");
        if (time_stamp == null)
        {
            if (app_status == "staging")
            {
                time_stamp = util.GetStagingAppTimeStamp(State, application_id);
            }
            else
            {
                time_stamp = util.GetProductionAppTimeStamp(State, application_id);
            }
        }

        x_util.CreateNode(Design, app_node, "time_stamp", time_stamp);
        XmlNode id_node = app_node.SelectSingleNode("id");
        if (id_node == null)
            x_util.CreateNode(Design, app_node, "id", application_id);
        else
            id_node.InnerText = application_id;

        XmlNode root = Design.SelectSingleNode("app_project");
        if (root == null)
            root = Design.SelectSingleNode("mobiflex_project");

        XmlNode status_node = x_util.CreateNode(Design, root, "status", "OK");

        return Design;
    }
    protected string DecodeMySql(string input)
    {
        return input.Replace("\\\"", "\"").Replace(@"\\", @"\");
    }



}

