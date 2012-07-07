using System;
using System.Collections;
using Telerik.Web.UI;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dialogs_OnAppOpen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Message.Text = "";
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        AccountType.Value = State["AccountType"].ToString();

        if (!IsPostBack)
        {
             InitActions();
        }
    }
    protected void InitActions()
    {
        XmlUtil x_util = new XmlUtil();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        actions.Items.Add(new RadComboBoxItem("Select action ->", ""));
        if (State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE)
        {
             actions.Items.Add(new RadComboBoxItem("Get or send device data via a web data source", "post"));
        }
        else
        {
            actions.Items.Add(new RadComboBoxItem("Get or send device data via a web data source", "post"));
 
            if (State["AccountType"].ToString().Contains("kofax"))
            {
                actions.Items.Add(new RadComboBoxItem("Capture documents from camera photos", "capture_doc"));
            }
        }
        Hashtable AppOpenAction = x_util.GetAppOpenAction(State);
        if (AppOpenAction != null)
        {
            foreach (String key in AppOpenAction.Keys)
            {
                switch (key)
                {
                    case "post":
                        actions.Items.FindItemByValue(key).Selected = true;
                        actionsMultiPage.PageViews[1].Selected = true;
                        break;
                    case "capture_doc":
                        actions.Items.FindItemByValue(key).Selected = true;
                        actionsMultiPage.PageViews[2].Selected = true;
                        if (AppOpenAction[key].ToString().Length > 0)
                            doc_case_field.Value = AppOpenAction[key].ToString();                       
                        break;
                    case "compute":
                        docompute.Checked = true;
                        compute.Value = AppOpenAction[key].ToString();
                        break;
                }
            }
            Clear.Style.Value = "";
        }
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        Hashtable AppOpenAction = new Hashtable();
        switch (actions.SelectedValue)
        {
            case "post":
                AppOpenAction["post"] = true;
                break;
            case "capture_doc":
                AppOpenAction["capture_doc"] = doc_case_field.Value;
                break;
            default:
                if (!docompute.Checked)
                {
                    Message.Text = "Select an action before saving.";
                    return;
                }
                break;
        }

        XmlUtil x_util = new XmlUtil();
        if (docompute.Checked && compute.Value.Length > 0)
        {
            AppOpenAction["compute"] = compute.Value;
        }

        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID]; 
        x_util.SetAppOpenAction(State, AppOpenAction);
        Message.Text = "Saved.";
        Clear.Style.Value = "";
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
        XmlUtil x_util = new XmlUtil();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        x_util.SetAppOpenAction(State, null);
        actions.Items[0].Selected = true;
        actionsMultiPage.PageViews[0].Selected = true;
        docompute.Checked = false;
        compute.Value = "";
        Message.Text = "Cleared.";
        Clear.Style.Value = "display:none";
    }
}