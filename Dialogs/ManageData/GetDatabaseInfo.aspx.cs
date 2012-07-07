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
using Parser;

public partial class Dialogs_GetDatabaseInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../../Default.aspx');", true); return; }

        GetDatabaseInfoHelp.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
    "../../Help/ManageData/GetDatabaseInfoHelp.htm", 600, 800, false, false, false, true));

    }
    protected void UploadSQLFile_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        ClearMessages();
        if (SqlFileUpload.UploadedFiles.Count > 0)
        {
            string targetFolder = Server.MapPath(SqlFileUpload.TargetFolder);
            foreach (UploadedFile file in SqlFileUpload.UploadedFiles)
            {
                string name = file.GetName();
                if (!Directory.Exists(targetFolder))
                    Directory.CreateDirectory(targetFolder);
                string file_path = targetFolder + @"\" + name;
                State["SqlFilePath"] = file_path;
                SqlUploadMessage.Text = name + " Uploaded.";
             }
        }
        else
        {
           SqlUploadMessage.Text = "Browse for a file";
        }
    }
    protected void ParseSqlFile(string sql_file)
    {
        SqlParser myParser = new SqlParser();
        string database_name = null;
        Hashtable tables = new Hashtable();

        XmlDocument doc = myParser.Parse(sql_file);
        XmlNodeList create_list = doc.SelectNodes("//Text[@Value='CREATE'] | //Text[@Value='create']");
        foreach (XmlNode create_node in create_list)
        {
            XmlNode next = create_node.NextSibling;
            if (next.Attributes[0].InnerText.ToLower() == "database")
            {
                for (int i = 0; i < 5; i++)
                {
                    next = next.NextSibling;
                    if (next.Attributes[0].InnerText.ToLower() != "if" &&
                       next.Attributes[0].InnerText.ToLower() != "not" &&
                       next.Attributes[0].InnerText.ToLower() != "exists" &&
                       next.Attributes[0].InnerText.ToLower() != "`")
                    {
                        database_name = next.Attributes[0].InnerText;
                    }
                }
            }
            else if (next.Attributes[0].InnerText.ToLower() == "table")
            {
                next = next.NextSibling.NextSibling;
                string table = next.Attributes[0].InnerText;
               
                //get first braces
                XmlNode brace_node = null;
                for (int i = 0; i < 5; i++)
                {
                    next = next.NextSibling;
                    if (next.Attributes[0].InnerText.ToLower() == "braces" )
                    {
                       brace_node = next;
                        break;
                    }
                }
                //get fields
                bool quote_toggle = true;
                ArrayList field_list = new ArrayList();

                foreach (XmlNode node in brace_node.ChildNodes)
                {
                    if (node.Attributes[0].InnerText == "`" && quote_toggle)
                    {
                        quote_toggle = false;
                        next = node.NextSibling;
                        Hashtable field = new Hashtable();
                        string field_name = next.Attributes[0].InnerText;
                        field["name"] = field_name;
                        next = next.NextSibling.NextSibling;
                        string field_type = next.Attributes[0].InnerText;
                        field["type"] = field_type;
                        field_list.Add(field);
                        next = next.NextSibling;
                        if (next.Attributes[0].InnerText == "BRACES")
                        {
                            string field_length = next.ChildNodes[0].Attributes[0].InnerText;
                            field["length"] = field_length;
                        }
                    }
                    else if (node.Attributes[0].InnerText == "`" && !quote_toggle)
                    {
                        quote_toggle = true;
                    } 
                }


                tables[table] = field_list;
            }
        }

        //save in app xml
        Util util = new Util();
        util.SaveDatabaseSchema((Hashtable)HttpRuntime.Cache[Session.SessionID], DatabaseType.SelectedValue, DBConnectionString.Text, tables);
    }
    protected void SaveDatabaseInfo_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        ClearMessages();
        if ( State["SqlFilePath"] == null)
        {
            SaveDatabaseInfoMessage.Text = "Upload .sql file of your database.";
            return;
        }
        if (DBConnectionString.Text.Length == 0)
        {
            SaveDatabaseInfoMessage.Text = "Enter Database connection string";
            return;
        }
        string file_path =  State["SqlFilePath"].ToString();
        if (System.IO.File.Exists(file_path))
        {
            string sql_file = File.ReadAllText(file_path);
            ParseSqlFile(sql_file);
            File.Delete(file_path);
            SaveDatabaseInfoMessage.Text = "Your Database Info has been saved.";
            Status.Text = "saved";
        }
        else
        {
            SaveDatabaseInfoMessage.Text = "Uploaded sql file could not be accessed.";
        }
    }
    protected void Remove_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;
        util.RemoveDatabaseInfo(State);
        SaveDatabaseInfoMessage.Text = "All Info has been removed.";
    }
    protected void ClearMessages()
    {
        Status.Text = "";
        SaveDatabaseInfoMessage.Text = "";
        SqlUploadMessage.Text = "";
    }
}