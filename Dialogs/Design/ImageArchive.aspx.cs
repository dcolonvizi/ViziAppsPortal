using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dialogs_ImageArchive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        AmazonS3 S3 = new AmazonS3();
        ArrayList images = S3.GetAccountImageArchiveUrls(State);
        DataSet paramsDS = new DataSet("ParameterDataSet");
        DataTable paramTable = paramsDS.Tables.Add("ParamTable");
        DataColumn paramCol0 = paramTable.Columns.Add("image_url", typeof(String));
        DataColumn paramCol1 = paramTable.Columns.Add("id", typeof(String));

         int index = 0;
        foreach (string url in images)
        {
            //filter out pages and camera images
            string file = url.Substring(url.LastIndexOf("/")+1);
            if (file.Length > 30 && file[13] == '-' && file[18] == '-' && file[23] == '-' && file[28] == '-') //this is a camera image
                continue;
            int n_periods = 0;
            foreach (char c in file)
            {
                if (c == '.')
                    n_periods++;
            }
            if (n_periods == 2) // these are storyboard images
                continue;
            if(file.EndsWith("qrcode.png"))//these are qrcode images
                continue;
            //string prefix = file.Substring(0, file.IndexOf("."));
            //if (apps.Contains(prefix)) //storyboard prefix
            //    continue;
            DataRow paramRow = paramTable.NewRow();
            string[] row_array = new string[2];
            row_array[0] = url;
            row_array[1] = "image" + index.ToString();
            paramRow.ItemArray = row_array;
            paramTable.Rows.Add(paramRow);
            index++;
        }

        ParamRepeater.DataSource = paramsDS;
        ParamRepeater.DataBind();
    }
}