using System;
using System.Collections;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Dialogs_NewApp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2)  Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true);

        HelpClick.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                       "../../Help/Design/AppTypeHelp.htm", 260, 530, false, false, false, false));
 
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if(AppName.Value.Length == 0)
        {
            Message.Text = "Enter an App Name";
            return;
        }
        else if (PageName.Value.Length == 0)
        {
            Message.Text = "Enter an Page Name";
            return;
        }
        else if (!Check.ValidateObjectName(Message, AppName.Value.Trim()))
        {
            return;
        }
        else if (!Check.ValidateObjectName(Message, PageName.Value.Trim()))
        {
            return;
        }
        Util util = new Util();
        if(util.DoesAppExist(State,AppName.Value))
        {
            Message.Text = "The New App Name has already been used. Try another name";
            return;
        }
        Message.Text = "Saved.";
    }
}
