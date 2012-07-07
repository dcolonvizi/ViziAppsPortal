using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using Aspose.Excel;
using Telerik.Web.UI;

public partial class Help_DBWebService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2)  Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../../Default.aspx.aspx');", true); 

    }
    protected void SaveWebServiceInfo_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx.aspx")) return;

        ClearMessages();

        if (WebServiceURL.Text.Length == 0)
        {
            SaveWebServiceInfoMessage.Text = "Enter Web Service URL";
            return;
        }
        //save in app xml
        XmlUtil x_util = new XmlUtil();
        XmlDocument doc = util.GetStagingAppXml(State);
        XmlNode root = doc.SelectSingleNode("//mobiflex_project");
        XmlNode database_config = doc.SelectSingleNode("//mobiflex_project/database_config");
        if (database_config == null)
        {
            database_config = x_util.CreateNode(doc, root, "database_config");
        }

        XmlNode database_webservice_url = doc.SelectSingleNode("database_webservice_url");
        if (database_webservice_url == null)
            x_util.CreateNode(doc, database_config, "database_webservice_url", WebServiceURL.Text);
        else
            database_webservice_url.InnerText = WebServiceURL.Text;

        util.UpdateStagingAppXml(State);


         ((Hashtable)HttpRuntime.Cache[Session.SessionID])["DBWebServiceURL"] = WebServiceURL.Text;

        SaveWebServiceInfoMessage.Text = "Your Web Service URL has been saved.";
    }
    protected void ClearMessages()
    {
        SaveWebServiceInfoMessage.Text = "";
    }
}