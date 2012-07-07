using System;
using System.Collections.Generic;
using System.Collections;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class InsertTableView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
 if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }
        if (!IsPostBack)
        {
            if (GoToPages.Items.Count == 0)
            {
                Init init = new Init();
                init.InitAppPagesAndCustom(State, GoToPages, false);
                init.InitAppPagesAndCustom(State, page_if_true, true);
                init.InitAppPagesAndCustom(State, page_if_false, true);
            }
            InitActions();
        }
    }
    protected void InitActions()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        actions.Items.Add(new RadComboBoxItem("Select action ->", ""));
        if ( State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE)
        {
            actions.Items.Add(new RadComboBoxItem("Go to a page for any row selected", "next_page"));
            actions.Items.Add(new RadComboBoxItem("Go to the page = value of the hidden field of the selected row", "page_from_field"));
            actions.Items.Add(new RadComboBoxItem("If/Then Go to page ", "if_then_next_page"));
            actions.Items.Add(new RadComboBoxItem("Go to the previous page", "previous_page"));
            actions.Items.Add(new RadComboBoxItem("Get or send device data via a web data source", "post"));
        }
        else
        {
            actions.Items.Add(new RadComboBoxItem("Go to a page for any row selected", "next_page"));
            actions.Items.Add(new RadComboBoxItem("Go to the page = value of the hidden field of the selected row", "page_from_field"));
            actions.Items.Add(new RadComboBoxItem("If/Then Go to page ", "if_then_next_page"));
            actions.Items.Add(new RadComboBoxItem("Go to the previous page", "previous_page"));
            actions.Items.Add(new RadComboBoxItem("Select multiple rows", "select"));
            actions.Items.Add(new RadComboBoxItem("Get or send device data via a web data source", "post"));
            actions.Items.Add(new RadComboBoxItem("Call phone", "call"));
            actions.Items.Add(new RadComboBoxItem("Share a message through Facebook, Texting or Email using a popup menu", "share"));
            actions.Items.Add(new RadComboBoxItem("Email message", "email"));
            actions.Items.Add(new RadComboBoxItem("Text message", "sms"));
        }
    }

    protected void rowType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        rowType.SelectedValue = e.Value;
        switch (e.Value)
        {
            case "image1text|image,text":
            case "imagetext|image,text":
            case "2texts|text,text":
            case "1texthidden|text,hidden":
                FieldNameMessage.Text = "Enter 2 Field Names separated by a comma";
                 SectionPages.SelectedIndex = 1;
               break;
            case "image1texthidden|image,text,hidden":
            case "2textshidden|text,text,hidden":
            case "image2texts|image,text,text":
               FieldNameMessage.Text = "Enter 3 Field Names separated by a comma";
               SectionPages.SelectedIndex = 2;
               break;
            case "image2textshidden|image,text,text,hidden":
               FieldNameMessage.Text = "Enter 4 Field Names separated by a comma";
               SectionPages.SelectedIndex = 3;
               break;
            default:
                FieldNameMessage.Text = "Enter the Field Name";
                SectionPages.SelectedIndex = 0;
                break;
        }
    }
}
