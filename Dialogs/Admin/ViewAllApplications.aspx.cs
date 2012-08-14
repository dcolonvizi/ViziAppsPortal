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
using Aspose.Excel;
using System.Text;

public partial class ViewAllApplications : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
        try
        {
            //Instantiate an instance of license and set the license file through its path
            Aspose.Excel.License license = new Aspose.Excel.License();
            license.SetLicense("Aspose.Excel.lic");
            string error = "The following applications had errors:<br>";
            StringBuilder error_list = new StringBuilder();

            Excel excel = new Excel();
            string path = MapPath(".") + @"\templates\";
            string designerFile = path + "ViewTemplate.XLS";
            excel.Open(designerFile);
            DB db = new DB();

            //get all app and staging servers in Hashtable
            string sql = "SELECT app_server_id,server_name FROM app_servers WHERE use_type='production' OR use_type='staging'";
            DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
            Hashtable serverID_to_name = new Hashtable();
            foreach (DataRow row in rows)
            {
                serverID_to_name[row["app_server_id"].ToString()] = row["server_name"].ToString();
            }

            //get all customer names in Hashtable
            sql = "SELECT customer_id,username FROM customers ";
            rows = db.ViziAppsExecuteSql(State, sql);
            Hashtable customerID_to_username = new Hashtable();
            foreach (DataRow row in rows)
            {
                customerID_to_username[row["customer_id"].ToString()] = row["username"].ToString();
            }

            //get all customer names in Hashtable
            sql = "SELECT application_id,app_server_id,use_type FROM application_to_server_mappings ";
            rows = db.ViziAppsExecuteSql(State, sql);
            Hashtable applicationID_to_app_serverID = new Hashtable();
            foreach (DataRow row in rows)
            {
                if(row["app_server_id"] != null && row["app_server_id"].ToString().Length>0)
                    applicationID_to_app_serverID[row["application_id"].ToString()+row["use_type"].ToString()] = row["app_server_id"].ToString();
            }
            
            DataTable table = new DataTable();
            table.Columns.Add("username");
            table.Columns.Add("application_name");
            table.Columns.Add("staging_server_name");
            table.Columns.Add("date_time_modified");
            table.Columns.Add("production_server_name");
            table.Columns.Add("production_date_time");
            table.Columns.Add("status");

            string production_app_server_id = null;
            string application_id = null;
            Hashtable bad_customerID_list = new Hashtable();

            //get all application information
            sql = "SELECT * FROM applications WHERE (status='staging' OR status='staging/production' or status='production')";
            rows = db.ViziAppsExecuteSql(State, sql);
            foreach (DataRow row in rows)
            {
                try
                {
                    application_id = row["application_id"].ToString();
                    string status = row["status"].ToString();
                    DataRow data_row = table.NewRow();
                    string[] items = new string[10];
                    string customer_id = row["customer_id"].ToString();
                    if (customerID_to_username.Contains(customer_id))
                        items[0] = customerID_to_username[customer_id].ToString();
                    else
                    {
                        bad_customerID_list[customer_id] = true;
                    }
                    items[1] = row["application_name"].ToString();
                    string staging_app_server_id = null;
                    items[2] = "";
                    if (status.IndexOf("staging") >= 0)
                    {
                        string key = application_id + "staging";
                        if (applicationID_to_app_serverID.ContainsKey(key))
                        {
                            staging_app_server_id = applicationID_to_app_serverID[key].ToString();
                            items[2] = serverID_to_name[staging_app_server_id].ToString();
                        }
                    }
                    items[3] = row["date_time_modified"].ToString();
                    production_app_server_id = null;
                    items[4] = "";
                    if (status.IndexOf("production") >= 0)
                    {
                        production_app_server_id = applicationID_to_app_serverID[application_id + "production"].ToString();
                        items[4] = serverID_to_name[production_app_server_id].ToString();
                    }
                    items[5] = row["production_date_time"].ToString();
                    items[6] = status;
                    data_row.ItemArray = items;
                    table.Rows.Add(data_row);
                }
                catch (Exception ex)
                {
                    util.LogError(State, ex);
                    error_list.Append(ex.Message + ": " + ex.StackTrace + "<br>");
                    continue;
                } 
            }
            if (error_list.Length > 0)
            {
                Message.Visible = true;
                Message.Text = error + error_list.ToString();
                return;
            }
            //get rid of applications with bad customer_id's
            foreach (string customer_id in bad_customerID_list.Keys)
            {
                sql = "DELETE FROM applications where customer_id='" + customer_id + "'";
                db.ViziAppsExecuteNonQuery(State, sql);
            }

            Worksheet sheet = excel.Worksheets[0];
            string[] column_names = new string[table.Columns.Count];
            int col = 0;
            foreach (DataColumn column in table.Columns)
            {
                column_names[col++] = column.ColumnName.Replace("_"," ");
            }
            sheet.Cells.ImportArray(column_names, 0, 0, false);
            sheet.Cells.ImportDataTable(table, false, 1, 0);
            
            col = 0;
            foreach (DataColumn column in table.Columns)
            {
                sheet.AutoFitColumn(col++);
            }
            int index = excel.Styles.Add();
            Aspose.Excel.Style style = excel.Styles[index];
            style.Number = 22;

            //set date time columns
            col = 0;
            foreach (DataColumn column in table.Columns)
            {
                string name = column.ColumnName.ToLower();
                if (name.IndexOf("date") >= 0 || name.IndexOf("time") >= 0)
                {
                    Range range = sheet.Cells.CreateRange(1,col,table.Rows.Count,1);
                    range.Style = style;
                }

                col++;
            }
            sheet.Name = "Application List";

            excel.Save("Application_List.xls", SaveType.OpenInExcel, FileFormatType.Default, this.Response);
            db.CloseViziAppsDatabase(State);
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }

    }
}
