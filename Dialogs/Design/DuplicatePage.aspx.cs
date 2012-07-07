using System;
using System.Collections;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dialogs_DuplicatePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2)  Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); 

    }
    protected void Save_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (PageName.Value.Length == 0)
        {
            Message.Text = "Enter an Page Name";
            return;
        }
        else if (!Check.ValidateObjectNameNoSpace(Message, PageName.Value))
        {
            return;
        }
        XmlUtil x_util = new XmlUtil();
        string[] pages = x_util.GetAppPageNames(State, State["SelectedApp"].ToString());
        foreach (string page in pages)
        {
            if (PageName.Value == page)
            {
                Message.Text = "The New Page Name has already been used. Try another name";
                return;
            }
        }
        Message.Text = "Saved.";
    }
}
