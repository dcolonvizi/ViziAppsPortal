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
using SpeechReco;

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
        Util util = new Util();
        XmlUtil x_util = new XmlUtil();
        XmlNode status = null;
        XmlDocument Design = null;
        try
        {
            DB db = new DB();

            HttpRequest request = Context.Request;

            string customer_username = request.QueryString.Get("customer");

            string app_status = (customer_username != null && customer_username.Length > 0)? "production" : "staging";

            string application_name = request.QueryString.Get("app");

            string application_id = request.QueryString.Get("app_id");
            if (application_id == null)
                application_id = "";

            string sql = null;
            DataRow[] rows = null;
            string customer_id = null;
            string user_id = "";
            string user = null;
            string password = null;

            string display_width = request.QueryString.Get("display_width");
            if (display_width == null)
                display_width = "320";

            string display_height = request.QueryString.Get("display_height");
            if (display_height == null)
                display_height = "480";

            string device_model = request.QueryString.Get("device_model");
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
            
            string unlimited = request.QueryString.Get("unlimited");
            if (unlimited == null || unlimited != "true")
            {
                user = request.QueryString.Get("user");
                password = request.QueryString.Get("pwd");
                if (user == null || password == null)
                {
                    Design = new XmlDocument();
                    XmlNode root2 = Design.CreateElement("mobiflex_project");
                    Design.AppendChild(root2);
                    status = x_util.CreateNode(Design, root2, "status","Either the username or the password: " + password + " is incorrect.");
                    return Design;
                }
            }

            if (app_status == "production")
            {
                State["Username"] = customer_username;
                customer_id = util.GetCustomerIDFromUsername(State, customer_username);
                State["CustomerID"] = customer_id;

                string account_status = util.GetCustomerStatus(State);
                if (account_status == "inactive")
                {
                     throw new System.InvalidOperationException("Your customer account is inactive.");
                }
                application_id = util.GetAppIDFromProductionAppName(State, application_name);

                //is payment current
                Hashtable features = util.IsProductionAppPaid(State, application_id);
                if (features == null)
                {
                    if (!util.IsFreeProductionValid(State, application_id))
                           throw new System.InvalidOperationException("The production service for your app has expired. Purchase a production service to re-activate your app.");
                }

                if (unlimited == null || unlimited != "true")
                {
                    //check username and password
                    sql = "SELECT * FROM users WHERE username='" + user.ToLower() + "' AND password='" + util.MySqlFilter(password) +
                         "' AND application_id='" + application_id + "'";

                    rows = db.ViziAppsExecuteSql(State, sql);
                    if (rows.Length == 0)
                    {
                        db.CloseViziAppsDatabase(State);
                        throw new System.InvalidOperationException("Either the username or the password: " + password + " is incorrect.");
                    }

                    //check number of users -- unlimited use never needs a login
                    bool use_1_user_credential = util.GetUse1UserCredential(State, application_id);
                    if (use_1_user_credential)
                    {
                        DataRow row = rows[0];
                        sql = "SELECT COUNT(*) FROM users_device_ids WHERE user_id='" + row["user_id"].ToString() + "'";
                        int device_count = Convert.ToInt32(db.ViziAppsExecuteScalar(State, sql));

                        string device_id = request.QueryString.Get("deviceid");
                        sql = "SELECT COUNT(*) FROM users_device_ids WHERE device_id='" + device_id + "'";
                        string device_exists = db.ViziAppsExecuteScalar(State, sql);

                        if (device_exists == "0")
                        {
                            if (device_count >= (int)features["max_users"])
                            {
                                db.CloseViziAppsDatabase(State);
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
                    throw new Exception("The username " + user.ToLower() + " is not registered. Go to www.viziapps.com and create a free account.");
                }
 
                DataRow row = rows[0];
                if (row["password"].ToString() != password)
                {
                    db.CloseViziAppsDatabase(State);
                    throw new Exception("Either the username or the password: " + password + " is incorrect.");
                }
                if (row["status"].ToString() == "inactive")
                {
                    db.CloseViziAppsDatabase(State);
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
                    throw new System.InvalidOperationException("You need to select an app to test, on the design page of your ViziApps Studio account.");
                }
            }

            db.CloseViziAppsDatabase(State);

            //get design
            Design = GetDesign(application_id, user_id, customer_id, Convert.ToInt32(display_width),  Convert.ToInt32(display_height), app_status,null);
            if (Design == null)
            {
                Design = new XmlDocument();
                XmlNode root2 = Design.CreateElement("mobiflex_project");
                Design.AppendChild(root2);
                status = x_util.CreateNode(Design, root2, "status", "No app has been selected for testing in your ViziApps account.");
            }
        }
        catch (System.Exception SE)
        {
            util.LogError(State, SE);

            if (status == null)
            {
                Design = new XmlDocument();
                XmlNode root2 = Design.CreateElement("mobiflex_project");
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
        Util util = new Util();
        XmlUtil x_util = new XmlUtil();
        XmlNode status_node = null;
        XmlDocument Report = new XmlDocument();
        XmlNode root = Report.CreateElement("mobiflex_report");
        Report.AppendChild(root);
        try
        {
            DB db = new DB();

            HttpRequest request = Context.Request;
            string application_id = request.QueryString.Get("appid");
            string application_name = request.QueryString.Get("app");
            string isproduction = request.QueryString.Get("isproduction");
            string username = request.QueryString.Get("customer");

            string app_status = "staging";
            if (isproduction == "yes")
            {
                app_status = "production";
             }

            string customer_id = request.QueryString.Get("custid");
            if(app_status == "production")
            {
                if(customer_id != null && customer_id.Length > 0)
                    State["CustomerID"] = customer_id;
                else
                    State["Username"] = username;

                application_id = util.GetAppIDFromProductionAppName(State, application_name);
               
                //is payment current
                Hashtable features = util.IsProductionAppPaid(State, application_id);
                if (features == null)
                {
                    if (!util.IsFreeProductionValid(State, application_id))
                    {
                        x_util.CreateNode(Report, root, "status", "kill");
                        x_util.CreateNode(Report, root, "status_message", "The account for this app is inactive. Contact ViziApps to re-activate your account.");
                        return Report;
                    }
                }
            }

            if (customer_id != null && customer_id.Length > 0)
            {
                State["CustomerID"] = customer_id;
                string active_sql = "SELECT COUNT(*) FROM customers where customer_id='" + customer_id + "' AND status!='inactive'";
                string active_count = db.ViziAppsExecuteScalar(State, active_sql);
                if (active_count == "0")
                {
                    x_util.CreateNode(Report, root, "status", "kill");
                    x_util.CreateNode(Report, root, "status_message", "The account for this app is inactive. Contact ViziApps to re-activate your account.");
                    return Report;
                }
            }

            string user_id = request.QueryString.Get("userid");
            string device_id = request.QueryString.Get("deviceid");
            string device_version = request.QueryString.Get("device_version");
            string mobiflex_version = request.QueryString.Get("mobiflex_version");
            string latitude = request.QueryString.Get("latitude");
            string longitude = request.QueryString.Get("longitude");

            string display_width = request.QueryString.Get("display_width");
            if (display_width == null)
                display_width = "320";

            string display_height = request.QueryString.Get("display_height");
            if (display_height == null)
                display_height = "480";

            string device_model = request.QueryString.Get("device_model");
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
            
            StringBuilder sb_sql = new StringBuilder("INSERT INTO reports SET ");
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

            db.ReportsExecuteNonQuery(State, sb_sql.ToString());
            db.CloseReportsDatabase(State);

            if (application_id != null && application_id.Length > 0)
            {
                string sql = null;
                if (app_status == "production")
                {
                    sql = "SELECT DISTINCT device_id FROM reports  WHERE application_id='" + application_id + "' GROUP BY device_id";
                    DataRow[] users_rows = db.ReportsExecuteSql(State, sql);
                    string number_of_users = users_rows.Length.ToString();
                    sql = "UPDATE applications SET number_of_uses=number_of_uses+1,number_of_users=" + number_of_users + " WHERE application_id='" +
                        application_id + "'";
                    db.ViziAppsExecuteNonQuery(State, sql);
                }
                else // staging app
                {                    
                    sql = "SELECT status FROM applications WHERE application_id='" + application_id + "'";
                    string staging_status = db.ViziAppsExecuteScalar(State, sql);
                    if (staging_status == null || (!staging_status.Contains("staging") && customer_id != null))
                    {
                        sql = "SELECT application_id FROM applications WHERE customer_id='" + customer_id + "' AND status LIKE '%staging%'";
                        string new_application_id = db.ViziAppsExecuteScalar(State, sql);
                        if (new_application_id != null)
                        {
                            XmlDocument Design = GetDesign(new_application_id, user_id, customer_id,  Convert.ToInt32(display_width), Convert.ToInt32( display_height), app_status,null);
                            if (Design != null)
                            {
                                Design.SelectSingleNode("//status").InnerText = "update_app";
                            }
                            else
                            {
                                Design = new XmlDocument();
                                XmlNode root2 = Design.CreateElement("mobiflex_project");
                                Design.AppendChild(root2);
                                x_util.CreateNode(Design, root2, "status", "kill");
                                x_util.CreateNode(Design, root2, "status_message", "Application no longer exists.");
                            }
                            return Design;
                        }                       
                    }
                    /*//check total testing uses per customer per week
                    DateTime last_week = now.AddDays(-7.0D);
                    sql = "SELECT COUNT(*) FROM reports WHERE app_status='staging' AND report_date_time > '" +
                        last_week.ToString("u").Replace("Z", "") + "' AND customer_id='" + customer_id + "'";
                    string s_count = db.ReportsExecuteScalar(State, sql);
                    db.CloseReportsDatabase(State);

                    int count = Convert.ToInt32(s_count);

                    if (count > Convert.ToInt32(ConfigurationManager.AppSettings["MaxStagingUsesPerWeek"]))
                    {
                        x_util.CreateNode(Report, root, "status", "kill");
                        x_util.CreateNode(Report, root, "status_message", "Your account has too many test uses of ViziApps per week.");
                        return Report;
                    }

                    //check on a new device_id
                    sql = "SELECT COUNT(*) FROM customer_device_ids where device_id='" + device_id + "'";
                    s_count = db.ViziAppsExecuteScalar(State, sql);
                    if (s_count == "0")
                    {

                        sql = "INSERT INTO customer_device_ids SET device_id='" + device_id + "',customer_id='" + customer_id + "'";
                        db.ViziAppsExecuteNonQuery(State, sql);
                    }

                    //check total devices per customer for testing
                    sql = "SELECT COUNT(*) FROM customer_device_ids where customer_id='" + customer_id + "'";
                    s_count = db.ViziAppsExecuteScalar(State, sql);
                    count = Convert.ToInt32(s_count);

                    if (count > Convert.ToInt32(ConfigurationManager.AppSettings["MaxCustomerTestingDevices"]))
                    {
                        x_util.CreateNode(Report, root, "status", "kill");
                        x_util.CreateNode(Report, root, "status_message", "Your account has too many devices using ViziApps.");
                        return Report;
                    }*/

                }
                db.CloseViziAppsDatabase(State);
            }

            string app_time_stamp = request.QueryString.Get("app_time_stamp");
            if (app_time_stamp != null && app_time_stamp.Length > 0)
            {
                string date_time_modified = null;
                if( app_status == "staging")
                    date_time_modified = util.GetStagingAppTimeStamp(State, application_id);
                else
                    date_time_modified = util.GetProductionAppTimeStamp(State, application_id);

                if (app_time_stamp != date_time_modified)
                { // assuming that there is a newer version
                    XmlDocument Design = GetDesign(application_id, user_id, customer_id,  Convert.ToInt32(display_width),  Convert.ToInt32(display_height), app_status, date_time_modified);
                    if (Design != null)
                    {
                        Design.SelectSingleNode("//status").InnerText = "update_app";
                    }
                    else
                    {
                        Design = new XmlDocument();
                        XmlNode root2 = Design.CreateElement("mobiflex_project");
                        Design.AppendChild(root2);
                        x_util.CreateNode(Design, root2, "status", "kill");
                        x_util.CreateNode(Design, root2, "status_message", "Application no longer exists.");
                    }

                    return Design;
                }
            }
            
            string status = "OK";

            //check for unlimited use
            if (app_status == "production")
            {
                application_name = request.QueryString.Get("app");
                string sql2 = "SELECT has_unlimited_users FROM applications WHERE application_id='" + application_id + "'";
                string has_unlimited_users = db.ViziAppsExecuteScalar(State, sql2);
                if (has_unlimited_users != null && has_unlimited_users.ToLower() == "true")
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
                XmlNode root2 = Report.CreateElement("mobiflex_project");
                Report.AppendChild(root2);
                status_node = x_util.CreateNode(Report, root2, "status");

            }
            status_node.InnerText = SE.Message + ": " + SE.StackTrace;
        }

        return Report;
    }

    protected XmlDocument GetDesign(string application_id, string user_id, string customer_id,
        int device_display_width, int device_display_height,string app_status,string time_stamp)
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
            State["NewWebAppHtml"] = File.ReadAllText(Server.MapPath(".") + @"\App_Data\NewViziAppsWebApp.txt");
            State["NewHybridAppXml"] = File.ReadAllText(Server.MapPath(".") + @"\App_Data\NewViziAppsHybridApp.xml");
            State["ShareThisScripts"] = File.ReadAllText(Server.MapPath(".") + @"\App_Data\ShareThisScripts.txt");
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
         XmlNode root = Design.SelectSingleNode("mobiflex_project");
        if(root == null)
            root = Design.SelectSingleNode("app_project");

        if (user_id != null && user_id.Length > 0)
            x_util.CreateNode(Design, root, "user_id", user_id);
        x_util.CreateNode(Design, root, "customer_id", customer_id);
        XmlNode app_node = root.SelectSingleNode("//application");
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

        XmlNode status_node = x_util.CreateNode(Design, root, "status", "OK");

        return Design;
    }
    protected string DecodeMySql(string input)
    {
        return input.Replace("\\\"", "\"").Replace(@"\\", @"\");
    }

}

