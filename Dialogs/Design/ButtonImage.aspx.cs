using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dialogs_ButtonImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DB db = new DB();
        string sql = "SELECT * FROM stock_images WHERE type='bw_light_icons'";
        DataRow[] rows = db.ViziAppsExecuteSql((Hashtable)HttpRuntime.Cache[Session.SessionID], sql);
        DataSet paramsDS = new DataSet("LightButtonRepeaterDataSet");
        DataTable paramTable = paramsDS.Tables.Add("LightButtonRepeaterTable");
        int n_columns = 14;
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

        LightButtonRepeater.DataSource = paramsDS;
        LightButtonRepeater.DataBind();

        n_columns = 8;
        sql = "SELECT * FROM stock_images WHERE type='navigation_buttons'";
        rows = db.ViziAppsExecuteSql((Hashtable)HttpRuntime.Cache[Session.SessionID], sql);
        paramsDS = new DataSet("NavButtonRepeaterDataSet");
        paramTable = paramsDS.Tables.Add("NavButtonRepeaterTable");
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

        NavButtonRepeater.DataSource = paramsDS;
        NavButtonRepeater.DataBind();

        n_columns = 8;
        sql = "SELECT * FROM stock_images WHERE type='icon_buttons'";
        rows = db.ViziAppsExecuteSql((Hashtable)HttpRuntime.Cache[Session.SessionID], sql);
        paramsDS = new DataSet("ColorButtonRepeaterDataSet");
        paramTable = paramsDS.Tables.Add("ColorButtonRepeaterTable");
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

        ColorButtonRepeater.DataSource = paramsDS;
        ColorButtonRepeater.DataBind();
        db.CloseViziAppsDatabase((Hashtable)HttpRuntime.Cache[Session.SessionID]);
    }
}