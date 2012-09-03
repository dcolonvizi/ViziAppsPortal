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
using System.Text;
using System.Xml;
using Telerik.Web.UI;
using MySql.Data.MySqlClient;


public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null)
        {
            return;
        }
        else if (State["PreviousError"] != null)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('" + State["PreviousError"].ToString() + "');", true);           
            State.Remove("PreviousError");
        }

       ClientScriptManager CSM = Page.ClientScript;
        State["ClientScriptManager"] = CSM;

        if (Request == null || Request.ServerVariables == null)
            return;

        State["PageRequestIPAddress"] = Request.ServerVariables["REMOTE_ADDR"]; //used if user logs in again after quitting

        string gapps_email = Context.Request.QueryString.Get("gapps_email");
        if (State["LoggedInFromAdmin"] != null ||
              State["LoggedinFromEula"] != null ||
             State["LoggedInFromGoogleApps"] != null ||
            gapps_email != null)
        {
            if (gapps_email != null)
            {
                State["Username"] = gapps_email;
                State["LoggedInFromGoogleApps"] = gapps_email;
            }
            State["LoggedInFromAdmin"] = null;
            State["LoggedinFromEula"] = null;
            ViziAppsLogin_Click(null, null);
            return;
        }
 
        try
        {
            util.LogLastUsed(State);
            if (Request.Form.Get("Logout") != null) // check if user is logged out
            {
                //log out is actually done twice: once from the server and once from the client posting to the server
                //the second time around we throw an exception with the message "logged out"
                util.Logout(State);
                throw new Exception("logged out");
            }

        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            if (ex.Message.IndexOf("incorrect") >= 0)
            {
                util.Logout(State);
                FailureText.Text = ex.Message;
            }
            else if (ex.Message.IndexOf("timed out") >= 0)
            {
                util.Logout(State);
                FailureText.Text = "Your session has timed out.";
            }
            else if (ex.Message.IndexOf("in use") >= 0)
            {
                util.Logout(State);
                FailureText.Text = "Your account is already in use.";
            }
            else 
            {
                util.ProcessMainExceptions(State, Response, ex);
            }
        }
    }
    protected void ViziAppsLogin_Click(object sender, EventArgs e)
    {
        string username = null;
        string password = null;
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State["Username"] != null)
        {
            username =  State["Username"].ToString();
            if( State["Password"] != null)
                password =  State["Password"].ToString();

            Username.Text = "";
            Password.Text = "";
            FailureText.Text = "";
        }
        else
        {
            if (Username.Text.Length == 0)
            {
                FailureText.Text = "Enter your username.";
                return;
            }
            else if (Password.Text.Length == 0)
            {
                FailureText.Text = "Enter your password.";
                return;
            }

            username = Username.Text.Trim().ToLower();
            password = Password.Text.Trim();
        }

        //For Logins other than the main login, do actual login at the page that the user will be working on
        // to avoid messing with Session during a Redirect
        Util util = new Util();
        InitPortalSession();
         State["Password"] = password;
        switch (username)
        {
            case "admin":
                 State["Username"] = username;
                Response.Redirect("Admin.aspx", false);
                break;
            default:
                string status = null;
                 State["UserHostAddress"] = Request.UserHostAddress;

                if (Request.QueryString.Get("guid") != null)
                {
                     State["UserHostAddress"] = Request.UserHostAddress;
                     if (!CheckTemplatesAccountAccess(username))
                     {
                         FailureText.Text = "Access to account not allowed" ;
                         return;
                     }
                     status = util.LoginToViziApps(State, username,
                         State["Password"].ToString());

                    if (status != "OK" && status != "agree_to_EULA")
                    {
                        throw new Exception(status);
                    }
                }
                else
                {
                    if (State["LoggedInFromGoogleApps"] != null)
                    {
                        if (State["LoggedInFromGoogleApps"].ToString() != "true") //in contrast to the encrypted email
                        {
                            username = Util.Decrypt(username, "mobiflex1");
                            State["Username"] = username;
                        }
                        status = util.LoginToViziAppsFromGoogleApps(State, username);
                    }
                    else
                    {
                        if (!CheckTemplatesAccountAccess(username))
                        {
                            FailureText.Text = "Access to account not allowed";
                            return;
                        }
                        status = util.LoginToViziApps(State, username, password);
                    }

                    
                    if (status != "OK")
                    {
                        if (status != "agree_to_EULA")
                        {
                            FailureText.Text = status;
                            return;
                        }
                    }

                    InitUserSession();

                    if (status == "agree_to_EULA")
                    {
                         State["NeedsEULAAgreement"] = true;
                        Response.Redirect("EULAAgreement.aspx", false);
                        return;
                    }
                    else if ( State["LoggedInFromGoogleApps"] != null)
                    {
                         State["LoggedInFromGoogleApps"] = null;
                    }
                    Response.Redirect("TabMySolutions.aspx", false);
                }
                break;
        }
    }
    private bool CheckTemplatesAccountAccess(string username)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State["UserHostAddress"].ToString() == "::1")
            return true;
        if (username ==  HttpRuntime.Cache["TemplatesAccount"].ToString() &&
            State["UserHostAddress"].ToString() !=  HttpRuntime.Cache["ViziAppsClientIPAddressForTemplatesAccount"].ToString())
        {
            return false;
        }
        return true;
    }
    private void InitPortalSession()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        //set browser type
        State["Browser"] = Request.Browser.Browser;
        State["BrowserVersion"] = Request.Browser.Version;
        State["RequestUrlHost"] = Request.Url.Host;
 
        //delete any old temp files
        Util util = new Util();
        util.DeleteOldTempFiles(State);
    }

    protected void InitUserSession()
    {
        //make sure licenses are in place because a website publish by Visual Studio does not transfer licenses
        string path = MapPath(".") + @"\licenses";
        string bin_path = MapPath(".") + @"\bin";
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            string file_name = file.Substring(file.LastIndexOf(@"\") + 1);
            string bin_file_path = bin_path + @"\" + file_name.Remove(file_name.LastIndexOf("."));
            if (!File.Exists(bin_file_path))
            {
                File.Copy(file, bin_file_path);
            }
        }

        //make sure the image uploads folder exists
        string image_uploads_path = MapPath(".") + @"\images\image_archive\uploads";
        if (!Directory.Exists(image_uploads_path))
            Directory.CreateDirectory(image_uploads_path);

    }

    protected void ForgotPasswordButton_Click(object sender, EventArgs e)
    {
        LoginPages.SelectedIndex = 1;       
    }

    protected void SendPasswordButton_Click(object sender, EventArgs e)
    {
        string user_email = Email.Text.Trim().ToLower();
        string sql = "SELECT username,password FROM customers WHERE email='" + user_email + "'";
        DB db = new DB();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        if (rows.Length > 0)
        {
            DataRow row = rows[0];
            string username = row["username"].ToString();
            string body = "Your ViziApps credentials are:\nUsername: " + row["username"].ToString() + "\nPassword: " + row["password"].ToString();
            Email email = new Email();
            InitPortalSession(); //to init from email
            email.SendEmail(State,  HttpRuntime.Cache["TechSupportEmail"].ToString(), user_email, "", "", "ViziApps Credentials", body, "", false);
            LoginPages.SelectedIndex = 0;
            FailureText.Text = "An email has been sent to you with your credentials.";
        }
        else
        {
            Message.Text = "The email you entered is not registered.";
        }
        db.CloseViziAppsDatabase(State);
    }
}