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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Telerik.Web.UI;

public partial class UploadCustomHeaderHtml : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        UploadMessage.Text = "";
    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        if (UploadFile1.UploadedFiles.Count > 0)
        {
            foreach (UploadedFile validFile in UploadFile1.UploadedFiles)
            {
                // open the text file 
                using (StreamReader reader = new StreamReader(validFile.InputStream))
                data.Text = reader.ReadToEnd(); // set value to text files text 

                UploadMessage.Text = "Upload Successful...";
            }
        }
        else
        {
            UploadMessage.Text = "Browse for a file";
        }
    }
}