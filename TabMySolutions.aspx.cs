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
using System.IO;
using System.Diagnostics;
using System.Threading;
using MySql.Data.MySqlClient;
using Telerik.Web.UI;

public partial class TabMySolutions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State,Response,"Default.aspx")) return;
        try
        {
            if (State["TechSupportEmail"] != null)
            {
                util.AddEmailToButton(SupportButton, State["TechSupportEmail"].ToString(), "Email To Tech Support");
            }

            util.UpdateSessionLog(State, "post", "TabMySolutions");
 
            ClearMessages();

            DemoVideo.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                 "Help/MySolutions/DemoVideo.htm", 400, 655, false, false, false, true));
            DemoVideo1.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                 "Help/MySolutions/DemoVideo.htm", 400, 655, false, false, false, true));
            QuickStart.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                 "Help/MySolutions/QuickStart.aspx", 940, 750, false, false, false, true));
            QuickStart1.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                  "Help/MySolutions/QuickStart.aspx", 940, 750, false, false, false, true));
            YourFirstApp.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                 "Help/MySolutions/YourFirstApp.htm", 350, 650, false, false, false, true));
            YourFirstApp1.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                 "Help/MySolutions/YourFirstApp.htm", 350, 650, false, false, false, true));
            Overview.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                 "Help/MySolutions/Overview.aspx", 800, 800, false, false, false, true));
            Overview1.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                 "Help/MySolutions/Overview.aspx", 800, 800, false, false, false, true));

            if (IsPostBack)
            {
                if (Request.Form.AllKeys.Length > 0)
                {
                    int last = Request.Form.AllKeys.Length - 1;

                    for (int i = last; i > last - 3; i--)
                    {
                        if (Request.Form.AllKeys[i] != null && Request.Form.AllKeys[i].Contains("$delete."))
                        {
                            int start = Request.Form.AllKeys[i].IndexOf("$delete.") + 8;
                            string[] split = Request.Form.AllKeys[i].Substring(start).Split(".".ToCharArray());
                            int row = Convert.ToInt32(split[0]);
                            string app = MySolutions.Items[row].Cells[2].Text;
                            //delete app
                            State["SelectedApp"] = app;
                            util.DeleteApplication(State);
                            util.ResetAppStateVariables(State);
                            LoadData();
                            return;
                        }
                    }
                }
            }
            else
            {
                UserLabel.Text = State["Username"].ToString();
                LoadData();
            }

        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }
    }
    protected void ClearMessages()
    {
        Message.Text = "";
    }
 
    protected void MySolutions_ItemDeleted(object source, GridDeletedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        int row = e.Item.ItemIndex;
       // RadGrid MySolutions = (RadGrid) State["MySolutions"];
        string app = MySolutions.Items[row].Cells[2].Text;
        //delete app
        State["SelectedApp"] = app;
        util.DeleteApplication(State);
        util.ResetAppStateVariables(State);
        LoadData();
    }
    private void LoadData()
    {
        try
        {
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            DataTable myDataTable = GetDataTable("SELECT application_name, application_type, date_time_modified, production_date_time, number_of_users,number_of_uses, status FROM applications WHERE customer_id='" + State["CustomerID"].ToString() +
                "' ORDER BY application_name");
            if (myDataTable.Rows.Count == 0)
            {
                FirstView.Style.Value = "text-align:-moz-center";
                NormalView.Style.Value = "display:none";
                InfoTable.Style.Value = "display:none";
                MySolutions.DataSourceID = String.Empty;
                MySolutions.DataBind();
                return;
            }
            Util util = new Util();
            foreach (DataColumn column in myDataTable.Columns)
            {
                if(column.ColumnName.StartsWith("application"))
                        column.ColumnName = column.ColumnName.Replace("application", "app");
                column.ColumnName = util.CapitalizeWords(column.ColumnName.Replace("_", " "));
            }

            //modify status values
           foreach(DataRow row in myDataTable.Rows)
           {
               row[myDataTable.Columns.Count - 1] = row[myDataTable.Columns.Count - 1].ToString().Replace("/", " / ");
           }

            //add delete column with each row getting an event
            myDataTable.Columns.Add(new DataColumn("x"));
 
            MySolutions.DataSource = myDataTable;
            MySolutions.DataBind();
  
            int index = 0;
            foreach (GridDataItem row in MySolutions.Items)
            {
                ImageButton delete_button = new ImageButton();
                delete_button.ImageUrl = "~/images/delete_small.gif";
                delete_button.ID = "delete." + index.ToString();
                delete_button.ToolTip = "Delete this App";
                index++;
                delete_button.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this app?');");
                int delete_cell_index = row.Cells.Count - 1;
                row.Cells[delete_cell_index].Controls.Add(delete_button);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
    protected void RowSelected_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (SelectedRowIndex.Text.Length == 0)
            return;
        int index = Convert.ToInt32(SelectedRowIndex.Text);
        ToggleRowSelection(index);
    }
    protected void ToggleRowSelection(int index)
    {
        TableCell cell = MySolutions.Items[index].Cells[2];
        string app = cell.Text;
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        State["SelectedApp"] = app;
         State["MainMenu"] = "Design";
        Util util = new Util();
         State["SelectedAppType"] = util.GetAppType(State);
         switch (State["SelectedAppType"].ToString())
         {
             case Constants.NATIVE_APP_TYPE:
                 Response.Redirect("TabDesignNative.aspx", false);
                 break;
             case Constants.WEB_APP_TYPE:
                 Response.Redirect("TabDesignWeb.aspx", false);
                 break;
             case Constants.HYBRID_APP_TYPE:
                 Response.Redirect("TabDesignHybrid.aspx", false);
                 break;
         }
    }

    protected void MySolutions_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        
        LoadData();
    }

    protected void MySolutions_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

       
        LoadData();
    }

    protected void MySolutions_PageSizeChanged(object source, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

       
        LoadData();
    }  
    public DataTable GetDataTable(string query)
    {
        string connect = ConfigurationManager.AppSettings["ViziAppsAdminConnectionString"];
        MySqlConnection conn = new MySqlConnection(connect);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        adapter.SelectCommand = new MySqlCommand(query, conn);

        DataTable myDataTable = new DataTable();

        conn.Open();
        try
        {
            adapter.Fill(myDataTable);
        }
        finally
        {
            conn.Close();
        }

        return myDataTable;
    }

    protected void CopyTemplateAppToAccount_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (State == null) return;
        if (State["CustomerID"] == null) // check for lost or end of Session
        {
            util.Logout(State);
            return;
        }
        string selected_template =  State["SelectedTemplateApp"].ToString();
        util.CopyTemplateApp(State, selected_template,  State["NameForTemplateApp"].ToString());
        LoadData();
    }
  
    private void InitPortalSession()
    {
        Init init = new Init();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        init.InitSiteConfigurations(State);
        State["NewProjectPath"] = MapPath(".") + @"\App_data\NewViziAppsNativeApp.xml";
         State["CanvasHtml"] = File.ReadAllText(MapPath(".") + @"\App_Data\Canvas.txt");
         State["NewWebAppHtml"] = File.ReadAllText(MapPath(".") + @"\App_Data\NewViziAppsWebApp.txt");
         State["NewHybridAppXml"] = File.ReadAllText(MapPath(".") + @"\App_Data\NewViziAppsHybridApp.xml");
         State["ShareThisScripts"] = File.ReadAllText(MapPath(".") + @"\App_Data\ShareThisScripts.txt");
         State["Server"] = Server;

        //set browser type
         State["Browser"] = Request.Browser.Browser;
         State["BrowserVersion"] = Request.Browser.Version;
         State["UserHostAddress"] = Request.UserHostAddress;
         State["TempFilesPath"] = MapPath(".") + @"\temp_files\";
   }
    protected void InitUserSession()
    {
        //make sure licenses are in place because a website publish by Visual Studio does not transfer licenses
        string path = MapPath(".") + @"\licenses";
        string bin_path = MapPath(".") + @"\bin";
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            string file_name = file.Substring(file.LastIndexOf(@"\") + 1);
            string bin_file_path = bin_path + @"\" + file_name.Remove(file_name.LastIndexOf("."));
            if (!File.Exists(bin_file_path))
            {
                File.Copy(file, bin_file_path);
            }
        }

        //make sure the image uploads folder exists
        string image_uploads_path = MapPath(".") + @"\images\image_archive\uploads";
        if (!Directory.Exists(image_uploads_path))
            Directory.CreateDirectory(image_uploads_path);

    }
    protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        string account_type = util.GetAccountType(State);
        util.Logout(State);
        if (account_type != null && account_type.Contains("google_apps"))
            Response.Redirect("LogoutForGoogleApps.aspx", false);
        else
            Response.Redirect("Default.aspx", false);

    }
    protected void TabMenu_ItemClick(object sender, RadMenuEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        string tab = e.Item.Value;
        Session["MainMenu"] = tab;
        Response.Redirect("Tab" + tab + ".aspx", false);
    }
    protected void Close_Click(object sender, ImageClickEventArgs e)
    {
        FirstView.Style.Value = "display:none";
        NormalView.Style.Value = "text-align:-moz-center";
        InfoTable.Style.Value = "width: 410px";
    }
}