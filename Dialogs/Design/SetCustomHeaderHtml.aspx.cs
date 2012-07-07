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

public partial class Dialogs_SetCustomHeaderHtml : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;
        try
        {
            Message.Text = "";
            State["CustomHeaderHTML"]= Header.Text = util.GetCustomHeaderHTML(State);
            Upload.Attributes.Add("onclick", "NamedPopUp('UploadCustomHeaderHtml.aspx','UploadCustomHeaderHtmlPopup' ,'height=150, width=500, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');return false;");
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
        string html = Request.Form.Get("Header");
        util.SetCustomHeaderHTML(State,html);
        Message.Text = "Custom Header HTML Updated.";
        Header.Text = html;
    }

    protected void Clear_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        util.SetCustomHeaderHTML(State, "");
        Header.Text = "";
    }
}