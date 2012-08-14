using System;
using System.Data;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Security.AccessControl;
using System.Text;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.Services;

public partial class WebAppService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

    }

    [WebMethod(EnableSession = true)]
    public string Report(string app_id, string customer_id, string is_production)
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
        string xml_prefix = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
        try
        {
            DB db = new DB();

            if (is_production == "yes")
            {
                //is payment current
                Hashtable features = util.IsProductionAppPaid(State, app_id);
                if (features == null)
                {
                    if (!util.IsFreeProductionValid(State, app_id))
                    {
                        x_util.CreateNode(Report, root, "status", "kill");
                        x_util.CreateNode(Report, root, "status_message", "The account for this app is inactive. Contact ViziApps to re-activate your account.");
                        return (xml_prefix + Report.OuterXml);
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
                    return (xml_prefix + Report.OuterXml);
                }
            }

            string status = "OK";

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

        return (xml_prefix + Report.OuterXml);
    }
}