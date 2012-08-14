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
using System.IO;

public partial class ViewActiveCustomers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void  GetActiveCustomers_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string NoActiveUsersList = Server.MapPath("../../") + @"\App_Data\NoActiveUsersList.txt";
        string[] lines = File.ReadAllLines(NoActiveUsersList);
        Hashtable NoActiveUsersTable = new Hashtable();
        foreach (string line in lines)
        {
            NoActiveUsersTable[line] = true;

        }
 
        Util util = new Util();
        try
        {
            //Instantiate an instance of license and set the license file through its path
            Aspose.Excel.License license = new Aspose.Excel.License();
            license.SetLicense("Aspose.Excel.lic");

            Excel excel = new Excel();
            string path = MapPath("../../") + @"\templates\";
            string query = "";
            string designerFile = path + "ViewTemplate.XLS";
            excel.Open(designerFile);
            query = "SELECT * FROM customers WHERE last_use_date_time>SUBDATE(NOW(),INTERVAL " + ActiveUsersDaysLoggedIn.Text + " DAY) AND n_logins>=" + ActiveUsersMinNLogins.Text;

            DB db = new DB();
            DataRow[] rows = db.ViziAppsExecuteSql(State,query);
            DataTable table = db.ViziAppsQuery(State, query);
            table.Rows.Clear();
 
            foreach(DataRow row in rows)
            {
                 if(!NoActiveUsersTable.ContainsKey(row["username"].ToString()))
                 {
                     table.ImportRow(row);
                 }
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
            sheet.Name = "Customer List";

            excel.Save("Customer_List.xls", SaveType.OpenInExcel, FileFormatType.Default, this.Response);
            db.CloseViziAppsDatabase(State);
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }

    }
}
