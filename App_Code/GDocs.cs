using System;
using System.Data;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Telerik.Web.UI;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Google.Spreadsheets;
using Google.GData.Spreadsheets;
using Google.Documents;
using Google.GData.Documents;
using Google.GData.Extensions;
using Google.GData.Client;
/// <summary>
/// Summary description for GDocs
/// </summary>
public class GDocs
{
	public GDocs()
	{
	}
    public string ParseGoogleDocsSpreadsheet(Hashtable State, string data_source_id, string use_spreadsheet_name, string username, string password)
    {
        try
        {
            string connection_string = null;
            DataSources DS = new DataSources();
            if (password == null)
            { //get password from DB
                State["DataSourceID"] = data_source_id;
                connection_string = DS.GetDataSourceDatabaseConnection(State);
                string[] splits = connection_string.Split(';');
                foreach (string split in splits)
                {
                    string[] parts = split.Split('=');
                    if (parts[0] == "password")
                    {
                        password = parts[1];
                        break;
                    }
                }
                if (password == null)
                    return null;
            }
            SpreadsheetsService service = new SpreadsheetsService(State["SelectedApp"].ToString());
            service.setUserCredentials(username, password);

            //get all spreadsheets
            Google.GData.Spreadsheets.SpreadsheetQuery query = new Google.GData.Spreadsheets.SpreadsheetQuery();

            SpreadsheetFeed feed = service.Query(query);

            bool found_spreadsheet = false;
            Hashtable tables = new Hashtable();
            foreach (SpreadsheetEntry entry in feed.Entries)
            {
                string spreadsheet_name = entry.Title.Text;
                if (spreadsheet_name.ToLower() == use_spreadsheet_name.ToLower())
                {
                    //Use this spreadsheet
                    found_spreadsheet = true;
                    AtomLink link = entry.Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null);

                    //get all worksheets
                    WorksheetQuery wk_query = new WorksheetQuery(link.HRef.ToString());
                    WorksheetFeed wk_feed = service.Query(wk_query);

                    foreach (WorksheetEntry worksheet in wk_feed.Entries)
                    {
                        string table_name = worksheet.Title.Text;

                        AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

                        ListQuery list_query = new ListQuery(listFeedLink.HRef.ToString());
                        ListFeed list_feed = service.Query(list_query);

                        //get field names
                        if (list_feed.Entries.Count == 0)
                        {
                            return "The worksheet " + table_name + " has no values.";
                        }

                        ListEntry fieldRow = (ListEntry)list_feed.Entries[0];
                        ArrayList field_list = new ArrayList();
                        foreach (ListEntry.Custom column in fieldRow.Elements)
                        {
                            Hashtable field = new Hashtable();
                            field["name"] = column.LocalName;
                            field_list.Add(field);
                        }
                        tables[table_name] = field_list;
                    }
                    break;
                }
            }
            if (!found_spreadsheet)
            {
                return use_spreadsheet_name + " could not be found";
            }

            connection_string = "spreadsheet=" + use_spreadsheet_name + ";username=" + username + ";password=" + password + "'";
            State["DBConnectionString"] = connection_string;
            DS.SaveGoogleSpreadsheetDataSource(State, data_source_id, "GoogleDocs", connection_string, tables);

            return "OK";
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            if (ex.Message.Contains("Execution of request failed"))
                return "Credentials to your Google Docs Account or spreadsheet name are not valid.";
            else
                return "There was an internal error with access to your Google Docs account.";
        }
    }
 
    public string ParseGoogleDocsSpreadsheet(Hashtable State, string data_source_id, string use_spreadsheet_name, string username, string password, string url)
    {
        try
        {
            string connection_string = null;
           DataSources DS = new DataSources();
           if (password == null)
           { //get password from DB
               State["DataSourceID"] = data_source_id;
               connection_string = DS.GetDataSourceDatabaseConnection(State);
               string[] splits = connection_string.Split(';');
               foreach (string split in splits)
               {
                   string[] parts = split.Split('=');
                   if (parts[0] == "password")
                   {
                       password = parts[1];
                       break;
                   }
               }
               if (password == null)
                   return null;
           }
            SpreadsheetsService service = new SpreadsheetsService(State["SelectedApp"].ToString());
            service.setUserCredentials(username, password);

            //get all spreadsheets
            Google.GData.Spreadsheets.SpreadsheetQuery query = new Google.GData.Spreadsheets.SpreadsheetQuery();

            SpreadsheetFeed feed = service.Query(query);

            bool found_spreadsheet = false;
            Hashtable tables = new Hashtable();
            foreach (SpreadsheetEntry entry in feed.Entries)
            {
                string spreadsheet_name = entry.Title.Text;
                if (spreadsheet_name.ToLower() == use_spreadsheet_name.ToLower())
                {
                    //Use this spreadsheet
                    found_spreadsheet = true;
                    AtomLink link = entry.Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null);

                    //get all worksheets
                    WorksheetQuery wk_query = new WorksheetQuery(link.HRef.ToString());
                    WorksheetFeed wk_feed = service.Query(wk_query);

                    foreach (WorksheetEntry worksheet in wk_feed.Entries)
                    {
                        string table_name = worksheet.Title.Text;

                        AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

                        ListQuery list_query = new ListQuery(listFeedLink.HRef.ToString());
                        ListFeed list_feed = service.Query(list_query);

                        //get field names
                        if (list_feed.Entries.Count == 0)
                        {
                            return "The worksheet " + table_name + " has no values.";
                        }

                        ListEntry fieldRow = (ListEntry)list_feed.Entries[0];
                        ArrayList field_list = new ArrayList();
                        foreach (ListEntry.Custom column in fieldRow.Elements)
                        {
                            Hashtable field = new Hashtable();
                            field["name"] = column.LocalName;
                            field_list.Add(field);
                        }
                        tables[table_name] = field_list;
                    }
                    break;
                }
            }
            if (!found_spreadsheet)
            {
                return use_spreadsheet_name + " could not be found";
            }

             connection_string = "spreadsheet=" + use_spreadsheet_name + ";username=" + username + ";password=" + password + ";url='" + url + "'";
            State["DBConnectionString"] = connection_string;
            DS.SaveGoogleSpreadsheetDataSource(State, data_source_id, "GoogleDocs", connection_string, tables);

            return "OK";
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            if (ex.Message.Contains("Execution of request failed"))
                return "Credentials to your Google Docs Account or spreadsheet name are not valid.";
            else
                return "There was an internal error with access to your Google Docs account.";
        }
    }
    public string ParseGoogleDocsSpreadsheet(Hashtable State, string data_source_id, string use_spreadsheet_name)
    {
        try
        {
            SpreadsheetsService service = new SpreadsheetsService(State["SelectedApp"].ToString());
            GOAuthRequestFactory requestFactory = new GOAuthRequestFactory("wise", "MobiFlex");
            requestFactory.ConsumerKey = ConfigurationManager.AppSettings["GoogleAppsKey"];
            requestFactory.ConsumerSecret = ConfigurationManager.AppSettings["GoogleAppsSecret"];
            service.RequestFactory = requestFactory;

            //get all spreadsheets
            Google.GData.Spreadsheets.SpreadsheetQuery query = new Google.GData.Spreadsheets.SpreadsheetQuery();
            query.OAuthRequestorId = State["CustomerEmail"].ToString();
            query.Uri = new Uri("https://spreadsheets.google.com/feeds/spreadsheets/private/full?xoauth_requestor_id=" + State["CustomerEmail"].ToString());

            SpreadsheetFeed feed = service.Query(query);

            bool found_spreadsheet = false;
            Hashtable tables = new Hashtable();
            foreach (SpreadsheetEntry entry in feed.Entries)
            {
                string spreadsheet_name = entry.Title.Text;
                if (spreadsheet_name.ToLower() == use_spreadsheet_name.ToLower())
                {
                    //Use this spreadsheet
                    found_spreadsheet = true;
                    AtomLink link = entry.Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null);

                    //get all worksheets
                    WorksheetQuery wk_query = new WorksheetQuery(link.HRef.ToString());
                    WorksheetFeed wk_feed = service.Query(wk_query);

                    foreach (WorksheetEntry worksheet in wk_feed.Entries)
                    {
                        string table_name = worksheet.Title.Text;

                        AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

                        ListQuery list_query = new ListQuery(listFeedLink.HRef.ToString());
                        ListFeed list_feed = service.Query(list_query);

                        //get field names
                        if (list_feed.Entries.Count == 0)
                        {
                            return "The worksheet " + table_name + " has no values.";
                        }
                        ListEntry fieldRow = (ListEntry)list_feed.Entries[0];
                        ArrayList field_list = new ArrayList();
                        foreach (ListEntry.Custom column in fieldRow.Elements)
                        {
                            Hashtable field = new Hashtable();
                            field["name"] = column.LocalName;
                            field_list.Add(field);
                        }
                        tables[table_name] = field_list;
                    }
                    break;
                }
            }
            if (!found_spreadsheet)
            {
                return use_spreadsheet_name + " could not be found";
            }

            DataSources DS = new DataSources();
            string connection_string = "spreadsheet=" + use_spreadsheet_name +
                ";consumer_key=" + ConfigurationManager.AppSettings["GoogleAppsKey"] +
                ";consumer_secret=" + ConfigurationManager.AppSettings["GoogleAppsSecret"] +
                ";requestor_id=" + State["Username"].ToString();
            State["DBConnectionString"] = connection_string;
            DS.SaveGoogleSpreadsheetDataSource(State, data_source_id, "GoogleDocs", connection_string, tables);

            return "OK";
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            if (ex.Message.Contains("Execution of request failed"))
                return "Credentials to your Google Docs Account or spreadsheet name are not valid.";
            else
                return "There was an internal error with access to your Google Docs account.";
        }
    }
    public string ParseGoogleDocsSpreadsheet(Hashtable State,string use_spreadsheet_name, string username, string password)
    {
        try
        {
            SpreadsheetsService service = new SpreadsheetsService(State["SelectedApp"].ToString());
            service.setUserCredentials(username, password);

            //get all spreadsheets
            Google.GData.Spreadsheets.SpreadsheetQuery query = new Google.GData.Spreadsheets.SpreadsheetQuery();

            SpreadsheetFeed feed = service.Query(query);

            bool found_spreadsheet = false;
            Hashtable tables = new Hashtable();
            foreach (SpreadsheetEntry entry in feed.Entries)
            {
                string spreadsheet_name = entry.Title.Text;
                if (spreadsheet_name.ToLower() == use_spreadsheet_name.ToLower())
                {
                    //Use this spreadsheet
                    found_spreadsheet = true;
                    AtomLink link = entry.Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null);

                    //get all worksheets
                    WorksheetQuery wk_query = new WorksheetQuery(link.HRef.ToString());
                    WorksheetFeed wk_feed = service.Query(wk_query);

                    foreach (WorksheetEntry worksheet in wk_feed.Entries)
                    {
                        string table_name = worksheet.Title.Text;

                        AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

                        ListQuery list_query = new ListQuery(listFeedLink.HRef.ToString());
                        ListFeed list_feed = service.Query(list_query);

                        //get field names
                        if (list_feed.Entries.Count == 0)
                        {
                            return "The worksheet " + table_name + " has no values.";
                        }

                        ListEntry fieldRow = (ListEntry)list_feed.Entries[0];
                        ArrayList field_list = new ArrayList();
                        foreach (ListEntry.Custom column in fieldRow.Elements)
                        {
                            Hashtable field = new Hashtable();
                            field["name"] = column.LocalName;
                            field_list.Add(field);
                        }
                        tables[table_name] = field_list;
                    }
                    break;
                }
            }
            if (!found_spreadsheet)
            {
                return use_spreadsheet_name + " could not be found";
            }

            Util util = new Util();
            string connection_string = "spreadsheet=" + use_spreadsheet_name + ";username=" + username + ";password=" + password;
            State["DBConnectionString"] = connection_string;
            util.SaveDatabaseSchema(State, "GoogleDocs", connection_string, tables);

            return "OK";
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            if (ex.Message.Contains("Execution of request failed"))
                return "Credentials to your Google Docs Account or spreadsheet name are not valid.";
            else
                return "There was an internal error with access to your Google Docs account.";
        }
    }
    public string ParseGoogleDocsSpreadsheet(Hashtable State, string use_spreadsheet_name)
    {
        try
        {
            SpreadsheetsService service = new SpreadsheetsService(State["SelectedApp"].ToString());
            GOAuthRequestFactory requestFactory = new GOAuthRequestFactory("wise", "MobiFlex");
            requestFactory.ConsumerKey = ConfigurationManager.AppSettings["GoogleAppsKey"];
            requestFactory.ConsumerSecret = ConfigurationManager.AppSettings["GoogleAppsSecret"];
            service.RequestFactory = requestFactory;
 
            //get all spreadsheets
            Google.GData.Spreadsheets.SpreadsheetQuery query = new Google.GData.Spreadsheets.SpreadsheetQuery();
            query.OAuthRequestorId = State["CustomerEmail"].ToString();
            query.Uri = new Uri("https://spreadsheets.google.com/feeds/spreadsheets/private/full?xoauth_requestor_id=" + State["CustomerEmail"].ToString());

            SpreadsheetFeed feed = service.Query(query);

            bool found_spreadsheet = false;
            Hashtable tables = new Hashtable();
            foreach (SpreadsheetEntry entry in feed.Entries)
            {
                string spreadsheet_name = entry.Title.Text;
                if (spreadsheet_name.ToLower() == use_spreadsheet_name.ToLower())
                {
                    //Use this spreadsheet
                    found_spreadsheet = true;
                    AtomLink link = entry.Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null);

                    //get all worksheets
                    WorksheetQuery wk_query = new WorksheetQuery(link.HRef.ToString());
                    WorksheetFeed wk_feed = service.Query(wk_query);

                    foreach (WorksheetEntry worksheet in wk_feed.Entries)
                    {
                        string table_name = worksheet.Title.Text;

                        AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

                        ListQuery list_query = new ListQuery(listFeedLink.HRef.ToString());
                        ListFeed list_feed = service.Query(list_query);

                        //get field names
                        if (list_feed.Entries.Count == 0)
                        {
                            return "The worksheet " + table_name + " has no values.";
                        }
                        ListEntry fieldRow = (ListEntry)list_feed.Entries[0];
                        ArrayList field_list = new ArrayList();
                        foreach (ListEntry.Custom column in fieldRow.Elements)
                        {
                            Hashtable field = new Hashtable();
                            field["name"] = column.LocalName;
                            field_list.Add(field);
                        }
                        tables[table_name] = field_list;
                    }
                    break;
                }
            }
            if (!found_spreadsheet)
            {
                return use_spreadsheet_name + " could not be found";
            }

            Util util = new Util();
            string connection_string = "spreadsheet=" + use_spreadsheet_name +
                ";consumer_key=" + ConfigurationManager.AppSettings["GoogleAppsKey"] +
                ";consumer_secret=" + ConfigurationManager.AppSettings["GoogleAppsSecret"] +
                ";requestor_id=" + State["Username"].ToString();
            State["DBConnectionString"] = connection_string;
            util.SaveDatabaseSchema(State, "GoogleDocs", connection_string, tables);

            return "OK";
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            if (ex.Message.Contains("Execution of request failed"))
                return "Credentials to your Google Docs Account or spreadsheet name are not valid.";
            else
                return "There was an internal error with access to your Google Docs account.";
        }
    }
    public string UploadImageFile(Hashtable State, string local_file_path)
    {
        DocumentsService service = new DocumentsService("MobiFlexDocs");
        service.setUserCredentials(State["MobiFlexGDocsUsername"].ToString(), State["MobiFlexGDocsPassword"].ToString());
        string name = local_file_path.Substring(local_file_path.IndexOf(@"media\") + 6).Replace(@"\", ".");
        DocumentEntry newEntry = service.UploadFile(local_file_path, name,"text/html",true);
        return newEntry.AlternateUri.ToString();
    }
    public void DeleteImageFile(Hashtable State,string url)
    {
        DocumentsService service = new DocumentsService("MobiFlexDocs");
        service.setUserCredentials(State["MobiFlexGDocsUsername"].ToString(), State["MobiFlexGDocsPassword"].ToString());
        AtomEntry entry = service.Get(url);
        entry.Delete();
    }
    public string GetSpreadsheets(Hashtable State, RadComboBox Spreadsheets)
    {
        try
        {
            SpreadsheetsService service = new SpreadsheetsService(State["SelectedApp"].ToString());

            GOAuthRequestFactory requestFactory = new GOAuthRequestFactory("wise", "MobiFlex");
            requestFactory.ConsumerKey = ConfigurationManager.AppSettings["GoogleAppsKey"];
            requestFactory.ConsumerSecret = ConfigurationManager.AppSettings["GoogleAppsSecret"];
            service.RequestFactory = requestFactory;
            //get all spreadsheets
            Google.GData.Spreadsheets.SpreadsheetQuery query = new Google.GData.Spreadsheets.SpreadsheetQuery();
            query.OAuthRequestorId = State["CustomerEmail"].ToString();
            query.Uri = new Uri("https://spreadsheets.google.com/feeds/spreadsheets/private/full?xoauth_requestor_id=" + State["CustomerEmail"].ToString());

            SpreadsheetFeed feed = service.Query(query);

            Spreadsheets.Items.Clear();
            Spreadsheets.Items.Add(new RadComboBoxItem("Select Spreadsheet ->", "->"));
            foreach (SpreadsheetEntry entry in feed.Entries)
            {
                string spreadsheet_name = entry.Title.Text;
                Spreadsheets.Items.Add(new RadComboBoxItem(spreadsheet_name, spreadsheet_name));
            }
            return "OK";
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            return ex.Message;
        }
    }
    public string GetSpreadsheets(Hashtable State, RadComboBox Spreadsheets, string username, string password)
    {
        try
        {
            SpreadsheetsService service = new SpreadsheetsService(State["SelectedApp"].ToString());

            service.setUserCredentials(username, password);

            //get all spreadsheets
            Google.GData.Spreadsheets.SpreadsheetQuery query = new Google.GData.Spreadsheets.SpreadsheetQuery();
            SpreadsheetFeed feed = service.Query(query);

            Spreadsheets.Items.Clear();
            Spreadsheets.Items.Add(new RadComboBoxItem("Select Spreadsheet ->", "->"));
            foreach (SpreadsheetEntry entry in feed.Entries)
            {
                string spreadsheet_name = entry.Title.Text;
                Spreadsheets.Items.Add(new RadComboBoxItem(spreadsheet_name, spreadsheet_name));
            }
            return "OK";
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            return ex.Message;
        }
    }
}