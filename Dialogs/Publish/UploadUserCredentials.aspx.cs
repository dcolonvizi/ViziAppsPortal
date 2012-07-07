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
using Aspose.Excel;
using Telerik.Web.UI;

public partial class Dialogs_UploadUserCredentials : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../../Default.aspx');", true); return; }
        ClearMessages();
        UploadExcelFileButton.Attributes.Add("onclick", "return confirm('Large uploads make take a few minutes. Do you want to begin?');");

    }
    protected void UploadExcelFileButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;
        
        if (this.FileUpload1.PostedFile != null)
        {
            // get the file
            HttpPostedFile file = this.FileUpload1.PostedFile;

            // check the length of the file
            if (file.ContentLength > 0)
            {
                string app_name =  ((Hashtable)HttpRuntime.Cache[Session.SessionID])["SelectedApp"].ToString();
                string application_id = util.GetAppID((Hashtable)HttpRuntime.Cache[Session.SessionID]);
 
                // get the Excel tables
                ExcelUtil excel_util = new ExcelUtil();
                DataTable[] excel_tables = excel_util.GetDataTablesFromExcelStream(file.InputStream);
                if (excel_tables == null || excel_tables.Length == 0)
                {
                    ErrorMessage.Text = "The file: '" + file.FileName + "' could not be processed as an Excel file.";
                    return;
                }

                 DataTable excel_table = excel_tables[0];
                 //string update_type = UploadType.SelectedIndex == 0 ? "add" : "replace";
                 string update_type =  "replace";
                 
                //check on user limit
                 long max_users = util.GetMaxUsers((Hashtable)HttpRuntime.Cache[Session.SessionID]);
                if (max_users == 0 || max_users > 1000L)
                {
                    ErrorMessage.Text = "No paid ViziApps service allows any production credentials";
                    return;
                }

                if (excel_table.Rows.Count - 1 > max_users)
                {
                    ErrorMessage.Text = "The number of credentials in your Credentials File exceed the limit of the paid ViziApps service";
                    return;
                }

                StringBuilder errors = new StringBuilder("The file was successfully uploaded. Close this window.");


                util.UpdateUserCredentials((Hashtable)HttpRuntime.Cache[Session.SessionID], application_id, excel_table.Rows, update_type);

                ErrorMessage.Text = errors.ToString();
            }
            else
            {
                ErrorMessage.Text = "The file: '" + file.FileName + "' is empty.";
            }
        }
    }

    private void ClearMessages()
    {
        ErrorMessage.Text = "";
     }

    private string getMappedFieldName(string mappedFieldNames, string excelFieldName)
    {
        string returnValue = string.Empty;
        try
        {
            foreach (string fieldNameMapping in mappedFieldNames.Split(','))
            {
                //The mapping will be defined as BusinessStreet=street and NOT 'BusinessStreet=street or ''BusinessStreet=street
                if (fieldNameMapping.Split('=')[0].ToLower().Replace(" ", "") == excelFieldName.Replace("''", ""))
                    return fieldNameMapping.Split('=')[1];
            }
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError((Hashtable)HttpRuntime.Cache[Session.SessionID], ex);
            returnValue = "UNEXPECTED ERROR";  //We lost the actual error here but unfortunately we have no palce to log and we don't want any tech error displayed to the client
        }
        return returnValue;
    }

    private string getInternalErrorText()
    {
        return "Internal Error: The application ran into technical problems. Please email our support team at " +  ((Hashtable)HttpRuntime.Cache[Session.SessionID])["TechSupportEmail"].ToString() + ".";
    }
    protected void DownloadTemplate_Click(object sender, EventArgs e)
    {
        //Instantiate an instance of license and set the license file through its path
        Aspose.Excel.License license = new Aspose.Excel.License();
        license.SetLicense("Aspose.Excel.lic");

        Excel excel = new Excel();
        string path = MapPath("../..") + @"\templates\";
        string designerFile = path + "UserCredentialsTemplate.xls";
        excel.Open(designerFile);
        excel.Save("User_Credentials.xls", SaveType.OpenInExcel, FileFormatType.Default, this.Response);
    }
}