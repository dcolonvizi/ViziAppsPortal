using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class EditorTools_InsertSwitch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }
        AccountType.Text =  State["AccountType"].ToString();

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
            actions.Items.Add(new RadComboBoxItem("Go to page", "next_page"));
            actions.Items.Add(new RadComboBoxItem("If/Then Go to page ", "if_then_next_page"));
            actions.Items.Add(new RadComboBoxItem("Go to previous page", "previous_page"));
            actions.Items.Add(new RadComboBoxItem("Get or send device data via a web data source", "post"));
        }
        else
        {
            actions.Items.Add(new RadComboBoxItem("Go to page", "next_page"));
            actions.Items.Add(new RadComboBoxItem("If/Then Go to page ", "if_then_next_page"));
            actions.Items.Add(new RadComboBoxItem("Go to previous page", "previous_page"));
            actions.Items.Add(new RadComboBoxItem("Get or send device data via a web data source", "post"));
            actions.Items.Add(new RadComboBoxItem("Call phone", "call"));
            actions.Items.Add(new RadComboBoxItem("Share a message through Facebook, Texting or Email using a popup menu", "share"));
            actions.Items.Add(new RadComboBoxItem("Email message", "email"));
            actions.Items.Add(new RadComboBoxItem("Text message", "sms"));
            actions.Items.Add(new RadComboBoxItem("Take a photo", "take_photo"));
        }
    }

    protected void SwitchType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
         ((RadComboBox)sender).SelectedItem.Selected = true ;
         switch (e.Value)
         {
             case "yes_no":
                 DefaultValue.ToggleStates[0].Text = "Yes";
                 DefaultValue.ToggleStates[1].Text = "No";
                 break;
             case "on_off":
                 DefaultValue.ToggleStates[0].Text = "On";
                 DefaultValue.ToggleStates[1].Text = "Off";
                 break;
             case "true_false":
                   DefaultValue.ToggleStates[0].Text = "True";
                 DefaultValue.ToggleStates[1].Text = "False";
               break;
         }
         DefaultValue.SelectedToggleStateIndex = 1;
    }
}