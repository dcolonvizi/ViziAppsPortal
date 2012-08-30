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
using System.IO;

public partial class ViewActiveCustomers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void  GetActiveCustomers_Click(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        string NoActiveUsersList = Server.MapPath("../../") + @"\App_Data\NoActiveUsersList.txt";
        string[] lines = File.ReadAllLines(NoActiveUsersList);
        Hashtable NoActiveUsersTable = new Hashtable();
        foreach (string line in lines)
        {
            NoActiveUsersTable[line] = true;

        }
 
        Util util = new Util();
        try
        {
            //Instantiate an instance of license and set the license file through its path
 
           string  query = "SELECT * FROM customers WHERE last_use_date_time>SUBDATE(NOW(),INTERVAL " + ActiveUsersDaysLoggedIn.Text + " DAY) AND n_logins>=" + ActiveUsersMinNLogins.Text;

            DB db = new DB();            
            DataTable myDataTable = db.GetDataTable(query);

            Grid.DataSource = myDataTable;
            Grid.DataBind();
            Grid.MasterTableView.ExportToExcel();

        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }

    }

}
