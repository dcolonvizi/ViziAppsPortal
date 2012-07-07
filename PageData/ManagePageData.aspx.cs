using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Aspose.Excel;
using System.Xml;
using System.Drawing;
using Telerik.Web.UI;
using System.Net;
using System.IO;

public partial class ManagePageData : System.Web.UI.Page
{
    protected int cell_width = 75;
    protected int page_size = 20;

    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        try
        {
            if (util.CheckSessionTimeout(State,Response,"../Default.aspx")) return;

            DeleteAppDataSource.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this data source and all data operations referring to this data source?');");
            RemovePageDataSource.Attributes.Add("onclick", "return confirm('Are you sure you want to remove this page data source from this page? This will reorder the page data sources.');");

            LoadData();

            if (!IsPostBack)
            {
                //set the first item as selected to start
                if(AppDataSources.Items.Count > 0)
                    AppDataSources.Items[0].Selected = true;

                if (EventField.Items.Count > 0)
                    EventField.Items[0].Selected = true;  
            }
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }
    }
    protected void PageDataSources_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        PageDataSources.FindItemByText(e.Text).Selected = true;       

        try{
            State["DBCommands"] = null; //reset previous commands
            
            string selected_page_data_source = e.Text;

            SelectedPageDataSource.Text = "";
            string selected_key = selected_page_data_source.Substring(selected_page_data_source.IndexOf(")") + 2);
            DataSources DS = new DataSources();
            SortedList page_list = DS.GetPageDataSources(State);
            string index = selected_page_data_source.Substring(1).Substring(0, selected_page_data_source.IndexOf(")") - 1);
            State["PageDataSourceIndex"] = Convert.ToInt32(index)-1;
            State["DataSourceEventType"] = ((ArrayList)page_list[index])[1].ToString();

            SortedList sorted_list = DS.GetAppDataSources(State);
            switch (sorted_list[selected_key].ToString())
            {
                case "google_spreadsheet":
                    ManageData.Attributes["src"] = "GoogleSpreadsheetOperations.aspx" ;
                    break;
                case "rss_feed":
                    ManageData.Attributes["src"] = "RSSDataSource.aspx";
                    break;
                case "rest_web_service":
                    ManageData.Attributes["src"] = "RESTWebServiceDataSource.aspx";
                    break;
                case "soap_web_service":
                    ManageData.Attributes["src"] = "SOAPWebServiceDataSource.aspx";
                    break;
                case "sql_database":
                    ManageData.Attributes["src"] = "SQLDatabaseDataSource.aspx";
                    break;
            }
 
            string event_field = null;
            if (((ArrayList)page_list[index])[1].ToString() == "field")
            {
                State["DataSourceEventField"] = event_field = ((ArrayList)page_list[index])[2].ToString();                
            }
            else
                State["DataSourceEventField"] = null;

            //get all event fields for this page
            SortedList event_list = DS.GetPageEventFields(State);
            EventField.Items.Clear();
            EventField.Items.Add(new RadComboBoxItem("Select ->", "->"));
            EventField.Items.Add(new RadComboBoxItem(State["SelectedAppPage"].ToString() + " (before it shows)", "page"));
            foreach (string field in event_list.Keys)
            {
                RadComboBoxItem item2 = new RadComboBoxItem(field, field);
                EventField.Items.Add(item2);
                if (event_field != null && event_field == field)
                    item2.Selected = true;
            }
            if (event_field == null)
            {
                RadComboBoxItem first_item = EventField.Items[0];
                first_item.Selected = true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
    protected void EventField_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        string selected_event_field = e.Text;
        if (selected_event_field.Contains("->"))
        {
            State["DataSourceEventType"] = null;
            State["DataSourceEventField"] = null;
        }
        else if (selected_event_field.Contains("("))
        {
            State["DataSourceEventType"] = "page";
            State["DataSourceEventField"] = null;
        }
        else
        {
            State["DataSourceEventType"] = "field";
            State["DataSourceEventField"] = selected_event_field;
        }
        State["DBCommands"] = null;

        EventField.FindItemByText(selected_event_field).Selected = true;

         if (selected_event_field.Contains("->"))
             ManageDataPanel.Style.Value = ManageDataPanel.Style.Value.Replace("block", "none");
         else
             ManageDataPanel.Style.Value = ManageDataPanel.Style.Value.Replace("none", "block");

    }
    private void LoadData()
    {
        try
        {
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            Util util = new Util();
            if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;
        

            //get app data sources
            AppDataSources.Items.Clear();
            SelectAppDataSource.Items.Clear();
            DataSources DS = new DataSources(); 
            SortedList sorted_list = DS.GetAppDataSources(State);
            if (sorted_list == null || sorted_list.Count == 0)
            {
                EditAppDataSource.Style.Value = "display:none";
                DeleteAppDataSource.Style.Value = "display:none";
                return;
            }

            SelectAppDataSource.Items.Add(new RadComboBoxItem("Select ->", ""));
            if (sorted_list.Count > 1)
            {
                AppDataSources.Items.Add(new RadComboBoxItem("Select ->", ""));
                EditAppDataSource.Style.Value = "display:none";
                DeleteAppDataSource.Style.Value = "display:none";
            }
            else // sorted_list.Count ==1
            {
                EditAppDataSource.Style.Value = "display:inline";
                DeleteAppDataSource.Style.Value = "display:inline";
            }

            foreach (string key in sorted_list.Keys)
            {
                AppDataSources.Items.Add(new RadComboBoxItem(key + " (" + sorted_list[key].ToString() + ")", key + ":" + sorted_list[key].ToString()));
                SelectAppDataSource.Items.Add(new RadComboBoxItem(key, key));
            }

            //get page data sources
            SortedList page_list = DS.GetPageDataSources(State);
            if (page_list == null || page_list.Count == 0)
            {
                RemovePageDataSource.Style.Value = "display:none";
                ManageDataPanel.Style.Value = ManageDataPanel.Style.Value.Replace("block", "none"); 
                return;
            }
            RemovePageDataSource.Style.Value = "display:inline";

            bool is_first = true;
            string index = null;
            PageDataSources.Items.Clear();
            string selected_key = null;
            foreach (string key in page_list.Keys)
            {
                ArrayList values = (ArrayList)page_list[key];
                string value = "(" + key + ") " + values[0].ToString();
                PageDataSources.Items.Add(new RadComboBoxItem(value,value));
                if (is_first)
                {
                    is_first = false;
                    index = key;
                    selected_key = values[0].ToString();
                }
            }

            ManageDataPanel.Style.Value = ManageDataPanel.Style.Value.Replace("block", "none");                     
            State["DataSourceID"] = selected_key;
            State["DataSourceType"] = sorted_list[selected_key].ToString();
            State["DataSourceEventType"] = ((ArrayList)page_list[index])[1].ToString();
            State["PageDataSourceIndex"] = Convert.ToInt32(index)-1;
            switch (sorted_list[selected_key].ToString())
            {
                case "google_spreadsheet":
                    ManageData.Attributes["src"] = "GoogleSpreadsheetOperations.aspx";
                    break;
                case "rss_feed":
                    ManageData.Attributes["src"] = "RSSDataSource.aspx";
                    break;
                case "rest_web_service":
                    ManageData.Attributes["src"] = "RESTWebServiceDataSource.aspx";
                    break;
                case "soap_web_service":
                    ManageData.Attributes["src"] = "SOAPWebServiceDataSource.aspx";
                    break;
                case "sql_database":
                    ManageData.Attributes["src"] = "SQLDatabaseDataSource.aspx";
                    break;
            }
            

             string event_field = null;
            if (((ArrayList)page_list[index])[1].ToString() == "field")
            {
                State["DataSourceEventField"] = event_field = ((ArrayList)page_list[index])[2].ToString();
            }
            else
                State["DataSourceEventField"] = null;

            //get all event fields for this page
            SortedList event_list = DS.GetPageEventFields(State);
            EventField.Items.Clear();
            EventField.Items.Add(new RadComboBoxItem("Select ->", "->")); 
            EventField.Items.Add(new RadComboBoxItem(State["SelectedAppPage"].ToString() + " (before it shows)", "page"));
            foreach (string field in event_list.Keys)
            {
                RadComboBoxItem item = new RadComboBoxItem(field, field);
                EventField.Items.Add(item);
                if (event_field != null && event_field == field)
                    item.Selected = true;
            }
            if (event_field == null)
            {
                RadComboBoxItem first_item = EventField.Items[0];
                first_item.Selected = true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
    protected void DeleteAppDataSource_Click(object sender, ImageClickEventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        DataSources DS = new DataSources();
        string data_source_id = AppDataSources.Text.Substring(0,AppDataSources.Text.IndexOf(" ("));
        DS.DeleteAppDataSource(State, data_source_id);
        LoadData();
    }
    protected void SelectAppDataSource_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        SelectAppDataSource.FindItemByText(e.Text).Selected = true;

        DataSources DS = new DataSources();
        DS.AddDataSourceToPage(State, e.Text);
        LoadData();
    }
    protected void RemovePageDataSource_Click(object sender, ImageClickEventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        string selected_raw_text = PageDataSources.SelectedValue;
        int index = Convert.ToInt32(selected_raw_text.Substring( 1,selected_raw_text.IndexOf(")")-1));
        DataSources DS = new DataSources(); 
        DS.RemovePageDataSource(State, index - 1);
 
        if (PageDataSources.Items.Count == 1)
        {
             RemovePageDataSource.Style.Value = "display:none";
             ManageDataPanel.Style.Value = ManageDataPanel.Style.Value.Replace("block", "none"); 
             PageDataSources.Items.Clear();
             EventField.Items.Clear();
             return;
        }
        else
         LoadData();
    }
    protected void AppDataSourcesPost_Click(object sender, EventArgs e)
    {
         LoadData();
    }
}