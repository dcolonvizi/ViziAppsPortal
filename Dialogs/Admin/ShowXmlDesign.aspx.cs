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
using System.Text;
using System.IO;

public partial class Dialogs_Admin_ShowXmlDesign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;
        try
        {
            Message.Text = "";
            State["SelectedAdminAppType"] = GetApplicationTypeForAdmin(State);
            XmlDocument doc = GetAppXmlForAdmin(State);
           Design.Text = FormatXMLString(doc.InnerXml);
           State["AdminAppDesign"] = doc;
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions((Hashtable)HttpRuntime.Cache[Session.SessionID], Response, ex);
        }
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        XmlDocument doc = (XmlDocument)State["AdminAppDesign"];
        string design = Request.Form.Get("Design");
        doc.LoadXml(design);
        State["AdminAppDesign"] = doc;
        State["SelectedAdminAppType"] = GetApplicationTypeForAdmin(State);
        UpdateStagingAppXmlFromAdmin(State);
        Message.Text = "XML Design Updated.";
    }
    public XmlDocument GetAppXmlForAdmin(Hashtable State)
    {
        XmlDocument doc = new XmlDocument();
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder();
        b_sql.Append("SELECT  staging_app_xml FROM applications ");
        b_sql.Append("WHERE application_name='" + State["SelectedAdminApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["ServerAdminCustomerID"].ToString() + "'");
        DataRow[] rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
        DataRow row = rows[0];
        if (row["staging_app_xml"] == DBNull.Value || row["staging_app_xml"] == null)
        {
            State["AppXmlDoc"] = null;
            return null;
        }
        string xml = row["staging_app_xml"].ToString();
        Util util = new Util();
        doc.LoadXml(util.DecodeMySql(xml));
        db.CloseViziAppsDatabase(State);
        return doc;
    }
    public string GetApplicationTypeForAdmin(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT application_type FROM applications WHERE application_name='" + State["SelectedAdminApp"].ToString() + "' AND customer_id='" + State["ServerAdminCustomerID"].ToString() + "'";
        string application_type = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return application_type;
    }
    public void UpdateStagingAppXmlFromAdmin(Hashtable State)
    {
        try
        {
            string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", "");
            DB db = new DB();
            StringBuilder b_sql = new StringBuilder("UPDATE applications SET ");
            b_sql.Append("application_type='" + State["SelectedAdminAppType"].ToString() + "',");
            XmlDocument doc = (XmlDocument)State["AdminAppDesign"];
            Util util = new Util();
            b_sql.Append("staging_app_xml='" + util.MySqlFilter(doc.OuterXml) + "',");
            b_sql.Append("date_time_modified='" + NOW + "' ");
            b_sql.Append("WHERE application_name='" + State["SelectedAdminApp"].ToString() + "'");
            b_sql.Append(" AND customer_id='" + State["ServerAdminCustomerID"].ToString() + "'");
            db.ViziAppsExecuteNonQuery(State, b_sql.ToString());
            db.CloseViziAppsDatabase(State);
        }
        catch (Exception ex)
        {
            throw new Exception("Error in UpdateStagingAppXmlFromAdmin: " + ex.Message + ": " + ex.StackTrace);
        }
    }
    public static string FormatXMLString(string sUnformattedXML)
    {
        XmlDocument xd = new XmlDocument();
        xd.LoadXml(sUnformattedXML);
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        XmlTextWriter xtw = null;
        try
        {
            xtw = new XmlTextWriter(sw);
            xtw.Formatting = Formatting.Indented;
            xd.WriteTo(xtw);
        }
        finally
        {
            if (xtw != null)
                xtw.Close();
        }
        return sb.ToString();
    }


}