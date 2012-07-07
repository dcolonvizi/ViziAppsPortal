using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Aspose.Excel;
using System.IO;

/// <summary>
/// Summary description for ExcelUtil
/// </summary>
public class ExcelUtil
{
	public ExcelUtil()
	{
	}
    public DataTable[] GetDataTablesFromExcelStream(Stream ExcelStream)
    {
        Aspose.Excel.License license = new Aspose.Excel.License();
        license.SetLicense("Aspose.Excel.lic");

        try
        {
            Excel excel = new Excel();
            excel.Open(ExcelStream);
            Worksheets sheets = excel.Worksheets;
            DataTable[] tables = new DataTable[sheets.Count];
            int index = 0;
            foreach (Worksheet sheet in sheets)
            {
                Cells cells = sheet.Cells;
                tables[index] = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1);
                tables[index++].TableName = sheet.Name;
            }
            return tables;
        }
        catch
        {
            return null;
        }
    }

}
