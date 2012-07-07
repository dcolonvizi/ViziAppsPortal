using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Dialogs_AppBackgroundColor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        if (!IsPostBack)
        {
            if ( State["BackgroundColor"] != null)
            {
                BackgroundColor.SelectedColor = System.Drawing.ColorTranslator.FromHtml( State["BackgroundColor"].ToString());
                ColorPickerSelectedColor.Value =  State["BackgroundColor"].ToString();
            }
        }
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;
        
        State["BackgroundColor"] = System.Drawing.ColorTranslator.ToHtml(BackgroundColor.SelectedColor);
        XmlUtil x_util = new XmlUtil();
        x_util.SetBackgroundColor((Hashtable)HttpRuntime.Cache[Session.SessionID],  State["BackgroundColor"].ToString());
        Message.Text = "App Background Color has been saved.";
    }
}