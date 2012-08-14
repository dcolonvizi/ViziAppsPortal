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

public partial class ViewAllCustomers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;
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
            query = "SELECT * FROM customers ";

            DB db = new DB();
            DataTable table = db.ViziAppsQuery(State,query);
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
