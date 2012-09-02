using System;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Text;
using System.Collections;

/// <summary>
/// Summary description for DB
/// Perforce location $Id: //depot/Dev/MobiFlex/Core/Source/App_Code/DB.cs#5 $
/// Changelist number $Change: 157 $
/// Last modified $DateTime: 2008/04/21 16:05:50 $
/// </summary>
public class DB
{
	public DB()
	{

	}
     /*public void CloseUserDatabase(Hashtable State)
    {
        MySqlConnection myConn = (MySqlConnection)State["UserDatabaseConnection"];
        myConn.Close();
    }*/
 
    /// <summary>
    /// Added by AGoel Changelist #10
    /// </summary>
    /// <param name="State"></param>
    /// <param name="storedProcName"></param>
    /// <param name="inputParamNames"></param>
    /// <param name="inputParamTypes"></param>
    /// <returns>DataTable</returns
    public DataTable ViziAppsStoredProcQueryDataTable(Hashtable State, string storedProcName, string[] inputParamNames, ArrayList inputParamTypes, ArrayList inputParamValues)
    {
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        MySqlConnection ViziAppsDB = GetViziAppsDatabase();
        adapter.SelectCommand = new MySqlCommand(storedProcName, ViziAppsDB);
        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        for (int count=0; count < inputParamNames.Length; count++)
        {
            MySqlParameter param = new MySqlParameter("?" + inputParamNames[count], inputParamTypes[count]);
            param.Direction = ParameterDirection.Input;
            param.Value = inputParamValues[count];
            adapter.SelectCommand.Parameters.Add(param);
        }

        DataTable table = new DataTable();
        //TODO:  Error handling to be coded later
        adapter.Fill(table);
        return table;
    }

    /// <summary>
    /// Added by AGoel Changelist #10
    /// </summary>
    /// <param name="State"></param>
    /// <param name="storedProcName"></param>
    /// <param name="inputParamNames"></param>
    /// <param name="inputParamTypes"></param>
    /// <returns>DataSet that can contain multiple datatables</returns
    public DataSet ViziAppsStoredProcQueryDataSet(Hashtable State, string storedProcName, string[] inputParamNames, ArrayList inputParamTypes, ArrayList inputParamValues)
    {
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        MySqlConnection ViziAppsDB = GetViziAppsDatabase();
        adapter.SelectCommand = new MySqlCommand(storedProcName, ViziAppsDB);
        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        for (int count = 0; count < inputParamNames.Length; count++)
        {
            MySqlParameter param = new MySqlParameter("?" + inputParamNames[count], inputParamTypes[count]);
            param.Direction = ParameterDirection.Input;
            param.Value = inputParamValues[count];
            adapter.SelectCommand.Parameters.Add(param);
        }

        DataSet table = new DataSet();
        //TODO:  Error handling to be coded later
        adapter.Fill(table);
        return table;
    }

    public DataRow[] ViziAppsExecuteSql(Hashtable State, string sql)
    {
        try
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlConnection ViziAppsDB = (MySqlConnection)State["ViziAppsDB"];
            if (ViziAppsDB == null || ViziAppsDB.State != ConnectionState.Open)
            {
                OpenViziAppsDatabase(State);
                ViziAppsDB = (MySqlConnection)State["ViziAppsDB"];
            }
            adapter.SelectCommand = new MySqlCommand(sql, ViziAppsDB);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table.Select(null, null, DataViewRowState.CurrentRows);
        }
        catch (MySqlException ex)
        {
            Util util = new Util();
            util.LogSQLError(State, ex,sql);
            throw new Exception("Error in ViziAppsExecuteSql: sql: " + sql);
        }
    }
    public DataTable ViziAppsQuery(Hashtable State, string sql)
    {
        try
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlConnection ViziAppsDB = (MySqlConnection)State["ViziAppsDB"];
            if (ViziAppsDB == null || ViziAppsDB.State != ConnectionState.Open)
            {
                OpenViziAppsDatabase(State);
                ViziAppsDB = (MySqlConnection)State["ViziAppsDB"];
            }
            adapter.SelectCommand = new MySqlCommand(sql, ViziAppsDB);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        catch (MySqlException ex)
        {
            Util util = new Util();
            util.LogSQLError(State, ex,sql);
            throw new Exception("Error in ViziAppsQuery: sql: " + sql);
        }
    }
    public string ViziAppsExecuteNonQuery(Hashtable State, string sql)
    {
        try
        {
            MySqlConnection ViziAppsDB = (MySqlConnection)State["ViziAppsDB"];
            if (ViziAppsDB == null || ViziAppsDB.State != ConnectionState.Open)
            {
                OpenViziAppsDatabase(State);
                ViziAppsDB = (MySqlConnection)State["ViziAppsDB"];
            }
            MySqlCommand cmd = new MySqlCommand(sql, ViziAppsDB);
            cmd.ExecuteNonQuery();
            return null;
        }
        catch (MySqlException ex)
        {
            Util util = new Util();
            util.LogSQLError(State, ex,sql);
            throw new Exception("Error in ViziAppsExecuteNonQuery: sql: " + sql);
        }
    }
    public string ViziAppsExecuteScalar(Hashtable State, string sql)
    {
        try
        {
            MySqlConnection ViziAppsDB = (MySqlConnection)State["ViziAppsDB"];
            if (ViziAppsDB == null || ViziAppsDB.State != ConnectionState.Open)
            {
                OpenViziAppsDatabase(State);
                ViziAppsDB = (MySqlConnection)State["ViziAppsDB"];
            }
            MySqlCommand cmd = new MySqlCommand(sql, ViziAppsDB);
            object result = cmd.ExecuteScalar();
            if (result == null)
                return null;
            else
                return result.ToString();
        }
        catch (MySqlException ex)
        {
            Util util = new Util();
            util.LogSQLError(State, ex,sql);
            throw new Exception("Error in ViziAppsExecuteScalar: sql: " + sql);
        }
    }
    public bool OpenViziAppsDatabase(Hashtable State)
    {
        try
        {
            string connect = GetConnectionString();
            MySqlConnection ViziAppsDB = new MySqlConnection(connect);
            try
            {
                ViziAppsDB.Open();
                State["ViziAppsDB"] = ViziAppsDB;
            }
            catch(Exception ex)
            {
                string error = ex.Message ;
                return false;
            }
            return true;
        }
        catch (MySqlException ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            throw new Exception("Error in OpenViziAppsDatabase");
        }
    }   
    /// <summary>
    /// This function stores the Connection in State["ViziAppsDB"] without opening it
    /// Added by AGoel Changelist #10
    /// </summary>
    /// <param name="State"></param>
    /// <returns></returns>
    private MySqlConnection GetViziAppsDatabase()
    {
        //string connect = ConfigurationManager.AppSettings["ViziAppsAdminConnectionString"];
        string connect = GetConnectionString();
        return new MySqlConnection(connect);
    }
    public void CloseViziAppsDatabase(Hashtable State)
    {
        try
        {
            MySqlConnection ViziAppsDB = (MySqlConnection)State["ViziAppsDB"];
            if (ViziAppsDB == null)
                return;
            ViziAppsDB.Close();
        }
        catch (MySqlException ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            throw new Exception("Error in CloseViziAppsDatabase");
        }
    }
 
    /// <summary>
    /// This function checks for any sleeping connections beyond a reasonable time and kills them.
    /// Since .NET appears to have a bug with how pooling MySQL connections are handled and leaves
    /// too many sleeping connections without closing them, we will kill them here.
    /// </summary>
    /// iMinSecondsToExpire - all connections sleeping more than this amount in seconds will be killed.
    /// <returns>integer - number of connections killed</returns>

    public string MySqlFilterConnectionString(string in_connect)
    {
        if (in_connect.ToLower().IndexOf("driver=") < 0)
        {
            if (in_connect.ToLower().IndexOf("pooling=false") < 0)
            {
                return in_connect + "pooling=false;";
            }
            else
                return in_connect;
        }
        string[] tokens = in_connect.Split(';');
        StringBuilder out_connect = new StringBuilder();
        foreach (string token in tokens)
        {
            if (token.ToLower().IndexOf("driver=") >= 0 ||
                token.ToLower().IndexOf("option=") >= 0 ||
                token.Length == 0)
            {
                continue;
            }
            else if (token.ToLower().IndexOf("user=") >= 0)
            {
                string user = token.ToLower();
                out_connect.Append(user.Replace("user", "user id") + ";");
            }
            else
                out_connect.Append(token + ";");
        }
        string output = out_connect.ToString();
        if (output.ToLower().IndexOf("pooling=false") < 0)
            return output + "pooling=false;";

        else
            return output;
    }
    public string OdbcFilterConnectionString(string in_connect)
    {
        if (in_connect.ToLower().IndexOf("driver=") >= 0)
            return in_connect;
        string[] tokens = in_connect.Split(';');
        StringBuilder out_connect = new StringBuilder("DRIVER={MySQL Odbc 3.51 Driver};option=3;");
        foreach (string token in tokens)
        {
             if (token.ToLower().IndexOf("user id=") >= 0)
            {
                string user = token.ToLower();
                out_connect.Append(user.Replace("user id", "user") + ";");
            }
            else if (token.Length > 0)
                out_connect.Append(token + ";");
        }
        return out_connect.ToString();
    }
    public void MySqlDropDatabase(string database_name, string database_username, string database_password)
    {
        try
        {
            string strDROP = "DROP DATABASE " + database_name + ";";
            MySqlConnection conDatabase = new MySqlConnection("Data Source=localhost;" +
                                                  "Persist Security Info=yes;" +
                                                  "User Id=" + database_username + "; PWD=" + database_password + ";");
            MySqlCommand cmdDatabase = new MySqlCommand(strDROP, conDatabase);

            conDatabase.Open();
            cmdDatabase.ExecuteNonQuery();
            conDatabase.Close();
        }
        catch (Exception ex)
        {
            string message = ex.Message + ": " + ex.StackTrace;
        }
    }
    public string GetMySQL5Home()
    {
        string MYSQL5_REGISTRY_PATH = "Software\\MySQL AB\\MySQL Server 5.0";
        Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(MYSQL5_REGISTRY_PATH);
        return key.GetValue("Location").ToString();
    }
    public string GetMySQL5_1_Home()
    {
        string MYSQL5_1_REGISTRY_PATH = "Software\\MySQL AB\\MySQL Server 5.1";
        Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(MYSQL5_1_REGISTRY_PATH);
        return key.GetValue("Location").ToString();
    }
    public DataTable GetDataTable(string query)
    {
        //string connect = ConfigurationManager.AppSettings["ViziAppsAdminConnectionString"];
        string connect = GetConnectionString();
        MySqlConnection conn = new MySqlConnection(connect);
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        adapter.SelectCommand = new MySqlCommand(query, conn);

        DataTable myDataTable = new DataTable();

        conn.Open();
        try
        {
            adapter.Fill(myDataTable);
        }
        finally
        {
            conn.Close();
        }

        return myDataTable;
    }
    public static string GetConnectionString()
    {
        string stagingURL = ConfigurationManager.AppSettings["StagingURL"];
        string connectionString = ConfigurationManager.AppSettings["ViziAppsAdminConnectionString"];

        try
        {
            HttpContext context = HttpContext.Current;
            string baseUrl = context.Request.Url.Authority.TrimEnd('/').Replace("www.", "");

            if (stagingURL == baseUrl)
            {
                connectionString = ConfigurationManager.AppSettings["ViziAppsAdminStagingConnectionString"];
            }
        }
        catch { } //during the initialization in Global.asax.cs there is no Request so an exeption occurs which will be ignored

        return connectionString;
    }
}
