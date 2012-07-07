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
using Telerik.Web.UI;


public partial class Dialogs_GetGoogleDocsInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../../Default.aspx');", true); return; }

        GetGoogleDocsInfoHelp.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                "../../Help/ManageData/GetGoogleDocsInfoHelp.htm", 350, 700, false, false, false, true));
        GetGoogleDocsInfoHelp2.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                 "../../Help/ManageData/GetGoogleDocsInfoHelp.htm", 350, 700, false, false, false, true));

        if (!IsPostBack)
        {
            if (!State["AccountType"].ToString().Contains("google_apps"))
            {
                if ( State["DBConnectionString"] != null)
                {
                    string[] parts =  State["DBConnectionString"].ToString().Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        string[] parms = part.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        switch (parms[0])
                        {
                            case "username":
                                Username.Text = parms[1];
                                break;
                            case "password":
                                Password.Text = parms[1];
                                break;
                            case "spreadsheet":
                                AccountSpreadsheets.Items.Clear();
                                AccountSpreadsheets.Items.Add(new RadComboBoxItem("Select Spreadsheet ->", "->"));
                                AccountSpreadsheets.Items.Add(new RadComboBoxItem(parms[1], parms[1]));
                                AccountSpreadsheets.SelectedIndex = 1;
                                break;
                        }
                    }
                }
            }

            else if ( State["AccountType"].ToString().Contains("google_apps"))
            {
                ContentMultiPage.SelectedIndex = 1;
                GDocs gDocs = new GDocs();
                string ret = gDocs.GetSpreadsheets(State, Spreadsheets);
                if (ret != "OK")
                {
                    SaveGoogleDocsInfoMessage.Text = "There was an error in getting spreadsheet info from your Google Apps: " + ret;
                    return;
                }

                if ( State["DBConnectionString"] != null)
                {
                    string[] parts =  State["DBConnectionString"].ToString().Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        string[] parms = part.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        switch (parms[0])
                        {
                            case "spreadsheet":
                                Spreadsheets.SelectedValue = parms[0];
                                break;
                        }
                    }
                }

            }
        }
    }
    protected void SaveDatabaseInfo_Click(object sender, EventArgs e)
    {
        ClearMessages();
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        GDocs gDocs = new GDocs();
        if (! State["AccountType"].ToString().Contains("google_apps"))
        {
            if (AccountSpreadsheets.SelectedIndex == 0)
            {
                SaveGoogleDocsInfoMessage.Text = "Select a spreadsheet for this app.";
                return;
            }
            if (Username.Text.Length == 0)
            {
                SaveGoogleDocsInfoMessage.Text = "Enter your Google Docs account username.";
                return;
            }
            if ( State["GDocsPassword"] == null)
            {
                SaveGoogleDocsInfoMessage.Text = "Click 'Get Spreadsheets' first";
                return;
            }
 

            string status = gDocs.ParseGoogleDocsSpreadsheet(State, AccountSpreadsheets.SelectedValue, Username.Text.Trim(),  State["GDocsPassword"].ToString());
            if (status == "OK")
            {
                 State["GDocsPassword"] = null;
                SaveGoogleDocsInfoMessage.Text = "Your Database Info has been saved.";
                Status.Text = "saved";
            }
            else
            {
                SaveGoogleDocsInfoMessage.Text = status;
                Status.Text = "";
            }
        }
        else if ( State["AccountType"].ToString().Contains("google_apps"))
        {
            if (Spreadsheets.SelectedIndex == 0)
            {
                SaveGoogleDocsInfoMessage.Text = "Select a spreadsheet for this app.";
                return;
            }
 
            string status2 = gDocs.ParseGoogleDocsSpreadsheet(State, Spreadsheets.SelectedValue);
            if (status2 == "OK")
            {
                SaveGoogleDocsInfoMessage.Text = "Your Database Info has been saved.";
                Status.Text = "saved";
            }
            else
            {
                SaveGoogleDocsInfoMessage.Text = status2;
                Status.Text = "";
            }
        }
    }
 
    protected void ClearMessages()
    {
        Status.Text = "";
        SaveGoogleDocsInfoMessage.Text = "";
    }
    protected void Remove_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        util.RemoveDatabaseInfo(State);
        SaveGoogleDocsInfoMessage.Text = "All info has been removed.";
    }
    protected void GetSpreadsheets_Click(object sender, EventArgs e)
    {
        ClearMessages();
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        if (!State["AccountType"].ToString().Contains("google_apps"))
        {
            if (Username.Text.Length == 0)
            {
                SaveGoogleDocsInfoMessage.Text = "Enter your Google Docs account username.";
                return;
            }
            if (Password.Text.Length == 0)
            {
                SaveGoogleDocsInfoMessage.Text = "Enter your Google Docs account password.";
                return;
            }
            else
                 State["GDocsPassword"] = Password.Text.Trim();
        }
        GDocs gDocs = new GDocs(); 
        String ret = gDocs.GetSpreadsheets(State, AccountSpreadsheets, Username.Text.Trim(),Password.Text.Trim());
        if (ret != "OK")
            SaveGoogleDocsInfoMessage.Text = "Either the username or password was not valid: " + ret;
    }
}