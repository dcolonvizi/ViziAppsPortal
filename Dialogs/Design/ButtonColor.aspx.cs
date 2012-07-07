using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dialogs_ButtonColor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        DB db = new DB();
        string sql = "SELECT * FROM stock_images WHERE ";
        if ( State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE)
            sql += "type='jquery_buttons' or type='blank_buttons'";
        else
            sql += "type='blank_buttons'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        DataSet paramsDS = new DataSet("ParameterDataSet");
        DataTable paramTable = paramsDS.Tables.Add("ParamTable");
        DataColumn paramCol = paramTable.Columns.Add("image_url", typeof(String));

        foreach (DataRow row in rows)
        {
            string type = row["type"].ToString();
            string url = row["image_url"].ToString();

            DataRow paramRow = paramTable.NewRow();
            string[] row_array = new string[1];
            row_array[0] = url;
            paramRow.ItemArray = row_array;
            paramTable.Rows.Add(paramRow);
        }

        ParamRepeater.DataSource = paramsDS;
        ParamRepeater.DataBind();
        db.CloseViziAppsDatabase((Hashtable)HttpRuntime.Cache[Session.SessionID]);

    }
}