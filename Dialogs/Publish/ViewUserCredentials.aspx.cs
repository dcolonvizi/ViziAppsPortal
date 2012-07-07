using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Configuration;
using MySql.Data.MySqlClient;

public partial class Dialogs_ViewUserCredentials : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../../Default.aspx');", true); return; }
        ShowCredentials();
    }
    protected void Credentials_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;
        ShowCredentials();
    }

    protected void Credentials_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;
        ShowCredentials();
    }
    protected void ShowCredentials()
    {
        Util util = new Util();
        ArrayList credentials = util.GetEndUserCredentials((Hashtable)HttpRuntime.Cache[Session.SessionID]);
        if (credentials == null || credentials.Count == 0)
            return;

        DataTable CredentialsTable = new DataTable();
        CredentialsTable.Columns.Add("Username");
        CredentialsTable.Columns.Add("Password");
        //CredentialsTable.Columns.Add(" ");
        foreach (string[] service in credentials)
        {
            DataRow row = CredentialsTable.NewRow();
            row.ItemArray = service;
            CredentialsTable.Rows.Add(row);
        }

        Credentials.DataSource = CredentialsTable;
        Credentials.DataBind();

        /*int index = 0;
        Init init = new Init();

        foreach (GridDataItem row in Credentials.Items)
        {
            string[] service = (string[])credentials[index];
            if (service[1] == null || service[1].Length == 0)
            {
                RadComboBox box = new RadComboBox();
                box.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(box_SelectedIndexChanged);
                box.AutoPostBack = true;
                box.ID = "ServiceApp." + index.ToString();
                box.Width = Unit.Pixel(200);
                init.InitAppsListNoDefault((Hashtable)HttpRuntime.Cache[Session.SessionID], box);
                row.Cells[3].Controls.Add(box);
            }
            else
            {
                ImageButton delete_button = new ImageButton();
                delete_button.ImageUrl = "~/images/delete_small.gif";
                delete_button.ID = "remove." + index.ToString();
                delete_button.ToolTip = "Remove this service from this app";
                delete_button.Click += new ImageClickEventHandler(delete_button_Click);
                delete_button.Attributes.Add("onclick", "return confirm('Are you sure you want to remove this app from this service?');");
                row.Cells[4].Controls.Add(delete_button);
            }
            index++;
        }*/
    }
}