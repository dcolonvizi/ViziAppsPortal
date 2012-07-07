using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dialogs_IconImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }
        
        DB db = new DB();
        string sql = "SELECT * FROM stock_images WHERE type='bw_dark_icons'";
        DataRow[] rows = db.ViziAppsExecuteSql((Hashtable)HttpRuntime.Cache[Session.SessionID], sql);
        DataSet paramsDS = new DataSet("DarkIconRepeaterDataSet");
        DataTable paramTable = paramsDS.Tables.Add("DarkIconRepeaterTable");
        int n_columns = 10;
        for (int i = 0; i < n_columns; i++)
        {
            paramTable.Columns.Add("image_url" + i.ToString(), typeof(String));
        }

        int index = 0;
        int max_index = rows.Length;
        while (index < max_index)
        {
            string[] row_array = new string[n_columns];
            for (int i = 0; i < n_columns; i++)
                row_array[i] = "";

            int count = 0;
            while (count < n_columns)
            {
                DataRow row = rows[index++];
                if (index >= max_index)
                    break;
                string url = row["image_url"].ToString();

                row_array[count++] = url;
            }
            DataRow paramRow = paramTable.NewRow();
            paramRow.ItemArray = row_array;
            paramTable.Rows.Add(paramRow);
        }

        DarkIconRepeater.DataSource = paramsDS;
        DarkIconRepeater.DataBind();

        n_columns = 2;
        sql = "SELECT * FROM stock_images WHERE type='bars'";
        rows = db.ViziAppsExecuteSql((Hashtable)HttpRuntime.Cache[Session.SessionID], sql);
        paramsDS = new DataSet("BarRepeaterDataSet");
        paramTable = paramsDS.Tables.Add("BarRepeaterTable");
        for (int i = 0; i < n_columns; i++)
        {
            paramTable.Columns.Add("image_url" + i.ToString(), typeof(String));
        }

        index = 0;
        max_index = rows.Length;
        while (index < max_index)
        {
            string[] row_array = new string[n_columns];
            for (int i = 0; i < n_columns; i++)
                row_array[i] = "";

            int count = 0;
            while (count < n_columns)
            {
                DataRow row = rows[index++];
                if (index >= max_index)
                    break;
                string url = row["image_url"].ToString();

                row_array[count++] = url;
            }
            DataRow paramRow = paramTable.NewRow();
            paramRow.ItemArray = row_array;
            paramTable.Rows.Add(paramRow);
        }

        BarRepeater.DataSource = paramsDS;
        BarRepeater.DataBind();
        db.CloseViziAppsDatabase((Hashtable)HttpRuntime.Cache[Session.SessionID]);
    }
}