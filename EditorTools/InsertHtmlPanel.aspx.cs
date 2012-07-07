using System;
using System.Collections.Generic;
using System.Collections;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditorTools_InsertHtmlPanel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
 if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }
        int border = 30;
        if( State["SelectedDeviceType"].ToString() == Constants.IPAD ||
            State["SelectedDeviceType"].ToString() == Constants.ANDROID_TABLET)
            editor.Width = 808 - border;
        else
            editor.Width = 380 - border;
    }
}