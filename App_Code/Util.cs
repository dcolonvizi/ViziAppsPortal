using System;
using System.Data;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Security.Cryptography; 
using System.Security.AccessControl;
using System.Text;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Telerik.Web.UI;
using HtmlAgilityPack;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for Util
/// </summary>
public class Util
{
    protected int cell_width = 75;
    protected int page_size = 20;

    public Util()
    {
    }
    public string GetCustomHeaderHTML(Hashtable State)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT custom_header_html FROM applications ");
        b_sql.Append("WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        string html = db.ViziAppsExecuteScalar(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
        if (html == null || html.Length == 0)
        {
            return "";
        }
        return html;
    }
    public void SetCustomHeaderHTML(Hashtable State,string html)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("UPDATE applications SET custom_header_html");
        if(html == null || html.Length == 0)
             b_sql.Append("=NULL ");
        else
            b_sql.Append("='" + MySqlFilter(html) + "' ");
        b_sql.Append("WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
    }
    public string GetUrlAccountIdentifier(Hashtable State)
    {
        string sql = "SELECT url_account_identifier FROM customers WHERE customer_id='" + State["CustomerID"].ToString() + "'";
        DB db = new DB();
        string account_identifier = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        if (account_identifier == null)
            account_identifier = "";
        return account_identifier;
    }
    public bool DoesUrlAccountIdentifierExist(Hashtable State, string account_identifier)
    {
        string sql = "SELECT COUNT(*) FROM customers WHERE url_account_identifier='" + account_identifier + "' AND customer_id!='" + State["CustomerID"].ToString() + "'";
        DB db = new DB();
        string count = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return (count != "0") ? true : false;
    }
    public void SaveUrlAccountIdentifier(Hashtable State, string account_identifier)
    {
        string sql = "UPDATE customers SET url_account_identifier='" + account_identifier + "' WHERE customer_id='" + State["CustomerID"].ToString() + "'";
        DB db = new DB();
        db.ViziAppsExecuteNonQuery(State, sql);
        db.CloseViziAppsDatabase(State); 
    }
    public bool IsAppSelectedForTest(Hashtable State)
    {
        string status = GetApplicationStatus(State, State["SelectedApp"].ToString());
        if (status != null && status.Contains("staging"))
        {
            return true;
        }
        else
            return false;

    }
    public bool GetUse1UserCredential(Hashtable State,string application_id)
    {
        DB db = new DB();
        string sql = "SELECT use_1_user_credential FROM applications WHERE application_id='" + application_id + "'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        db.CloseViziAppsDatabase(State);
        DataRow row = rows[0];
        bool use_1_user_credential = false;
        if (row["use_1_user_credential"] != DBNull.Value)
        {
            string s_use_1_user_credential = row["use_1_user_credential"].ToString();
            use_1_user_credential = (s_use_1_user_credential == "1") ? true : false;
        }
        return use_1_user_credential;
    }
    public bool CheckForFreeWebApp(Hashtable State,string html)
    {
        HtmlDocument HtmlDoc = new HtmlDocument();
        HtmlDoc.LoadHtml(html);
        HtmlNodeCollection nodes =  HtmlDoc.DocumentNode.SelectNodes("//a[@href]");
       
        foreach (HtmlNode node in nodes)
        {
            HtmlAttribute attr = node.Attributes["href"];
            if(attr.Value.StartsWith("http"))
                return false;
        }
        return true;
    }
   
    public bool IsAppStoreSubmissionPaid(Hashtable State, string app_name)
    {
        DB db = new DB();
        string sql = "SELECT COUNT(*) FROM paid_services WHERE (sku='" + State["iOSSubmitServiceSku"].ToString() +
            "' OR sku='" + State["AndroidSubmitServiceSku"].ToString() + "') AND app_name ='" + app_name + "' AND customer_id='" + State["CustomerID"].ToString() + "' AND status='paid'";
        string count = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return (count == "0") ? false : true;
    }
    public void SetFreeProductionExpiration(Hashtable State, DateTime expirationDateTime)
    {
        DB db = new DB();
        string expiration = expirationDateTime.ToString("s").Replace("T", " ");
        string sql = "UPDATE applications SET free_production_expiration_date_time='" + expiration + "' WHERE application_name ='" + State["SelectedApp"].ToString() + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);
        db.CloseViziAppsDatabase(State);    
    }

    public bool IsFreeProductionValid(Hashtable State, string application_id)
    {
        DB db = new DB();
        string sql = "SELECT free_production_expiration_date_time FROM applications WHERE application_id ='" + application_id + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        string expiration = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        if (expiration == null || expiration.Length == 0)
            return false;
        DateTime expirationDateTime = DateTime.Parse(expiration);
        return (DateTime.Now.ToUniversalTime() <= expirationDateTime) ? true : false;
    }
    public bool IsGoodApplicationName(string name)
    {
        if (name==null || name.Length == 0 || name.Length > 100)
            return false;

        bool first = true;
        foreach (char c in name)
        {
            if (first)
            {
                if (!Char.IsLetter(c))
                    return false;
                first = false;
            }
            if (!Char.IsLetter(c) && !Char.IsNumber(c) && c != ' ' && c != '_')
                return false;
        }
        return true;
    }
    public bool IsGoodObjectNameNoSpace(string name)
    {
        if (name == null || name.Length == 0 || name.Length > 100)
            return false;

        bool first = true;
        foreach (char c in name)
        {
            if (first)
            {
                if (!Char.IsLetter(c))
                    return false;
                first = false;
            }
            if (!Char.IsLetter(c) && !Char.IsNumber(c) && c != '_')
                return false;
        }
        return true;
    }
     public string GetDigits(string number)
     {
         if (number == null || number.Length == 0)
             return "";
         StringBuilder build_number = new StringBuilder();
         foreach (char x in number)
         {
             if (Char.IsDigit(x))
                 build_number.Append(x);
         }
 
         return build_number.ToString();
     }
    public string GetPhoneDigits(string phone)
    {
        if (phone == null || phone.Length == 0)
            return "";
        StringBuilder build_phone = new StringBuilder();
        foreach (char x in phone)
        {
            if (Char.IsDigit(x))
                build_phone.Append(x);
        }
        if (build_phone.Length>10 && build_phone[0] == '1')
            build_phone.Remove(0, 1);

        return build_phone.ToString();
    }
    public string FormatPhone(string phone)
    {
        if (phone == null || phone.Length == 0)
            return "";
        string f_phone = GetPhoneDigits(phone);
        if (f_phone.Length < 10)
            return "";
        if (f_phone.Length == 10)
            return f_phone.Insert(6, "-").Insert(3, "-");
        else
            return f_phone;
    }
    public string FilterWebFileName(string input)
    {
        string name = input.Replace("%20", "_");
        StringBuilder output = new StringBuilder();
        foreach (char c in name)
        {
            if (Char.IsLetterOrDigit(c) || c == '_' || c=='.')
            {
                output.Append(c);
            }
            else if(c == ' ')
            {
                output.Append("_");
            }
        }
        return output.ToString();
    }
    public void SaveXmlFile(XmlDocument doc, string file_path)
    {
        XmlTextWriter tw = new XmlTextWriter(file_path, Encoding.ASCII);
        tw.Formatting = Formatting.Indented;
        doc.Save(tw);
        tw.Flush();
        tw.Close();
    }
     // Adds an ACL entry on the specified directory for the specified account.
    public void AddDirectorySecurity(string DirectoryName, string Account, FileSystemRights Rights, AccessControlType ControlType)
    {
        // Create a new DirectoryInfo object.
        DirectoryInfo dInfo = new DirectoryInfo(DirectoryName);

        // Get a DirectorySecurity object that represents the 
        // current security settings.
        DirectorySecurity dSecurity = dInfo.GetAccessControl();

        // Add the FileSystemAccessRule to the security settings. 
        dSecurity.AddAccessRule(new FileSystemAccessRule(Account,Rights,ControlType));

        // Set the new access settings.
        dInfo.SetAccessControl(dSecurity);
    }

    public string ReplaceValueInTokens(string line, char[] separators, string search, string new_value)
    {
        if (line == null || line.Length == 0)
            return "";
        string[] tokens = line.Split(separators);
        foreach (string token in tokens)
        {
            if (token.ToLower().IndexOf(search) == 0)
            {
                string first = null;
                if (token.Length > search.Length)
                    first = token.Substring(0, search.Length);
                else
                    first = token;
                string new_token = first + new_value;
                return line.Replace(token, new_token);
            }
        }
        return "";
    }
    public string GetWebPage(string url)
    {
        // used to build entire input
        StringBuilder sb = new StringBuilder();

        // used on each read operation
        byte[] buf = new byte[8192];

        try
        {
            // prepare the web page we will be asking for
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create(url);

        // execute the request
           HttpWebResponse response = (HttpWebResponse)
                request.GetResponse();


            // we will read data via the response stream
            Stream resStream = response.GetResponseStream();

            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            // print out page source
            return sb.ToString();
        }
        catch
        {
            return null;
        }

    }
 
    public void SetSoapEnvelopeTemplatePHP(XmlDocument SoapEnv, string web_service_url, string method, string[] method_params)
    {
        XmlNode top = SoapEnv.ChildNodes[1];
        int end = web_service_url.LastIndexOf("/") + 1;
        string name_space_uri = web_service_url.Substring(0, end);
        top.Attributes[1].InnerText = name_space_uri;
        XmlNode body = top.ChildNodes[0];
        XmlNode method_node = SoapEnv.CreateElement("ns1", method, name_space_uri);
        foreach (string param in method_params)
        {
            XmlNode param_node = SoapEnv.CreateElement(param.Trim());
            method_node.AppendChild(param_node);
        }
        body.AppendChild(method_node);
    }
    public void SetSoapEnvelopeTemplateASMX(XmlDocument SoapEnv, string web_service_url, string method, string[] method_params)
    {
        XmlNode top = SoapEnv.ChildNodes[1];
        XmlNode body = top.ChildNodes[0];
        XmlNode method_node = SoapEnv.CreateElement( method);
        XmlAttribute xmlns = SoapEnv.CreateAttribute("xmlns");
        xmlns.InnerText = GetTargetNameSpace(web_service_url + "?WSDL");
        method_node.Attributes.Append(xmlns);
        foreach (string param in method_params)
        {
            XmlNode param_node = SoapEnv.CreateElement(param.Trim());
            method_node.AppendChild(param_node);
        }
        body.AppendChild(method_node);
    }
    public void SetSoapEnvelopePHP(XmlDocument SoapEnv, string web_service_url, string method, string inputs)
    {
        XmlNode top = SoapEnv.ChildNodes[1];
        int end = web_service_url.LastIndexOf("/") + 1;
        string name_space_uri = web_service_url.Substring(0, end);
        top.Attributes[1].InnerText = name_space_uri;
        XmlNode body = top.ChildNodes[0];
        XmlNode method_node = SoapEnv.CreateElement("ns1", method, name_space_uri);
        string[] param_list = inputs.Split("&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (string param in param_list)
        {
            string[] parts = param.Split("=".ToCharArray(), StringSplitOptions.None);
            XmlNode param_node = SoapEnv.CreateElement(parts[0].Trim());
            param_node.InnerText = parts[1];
            method_node.AppendChild(param_node);
        }
        body.AppendChild(method_node);
    }
    public void SetSoapEnvelopeASMX(XmlDocument SoapEnv, string web_service_url, string method, string inputs)
    {

        XmlNode top = SoapEnv.ChildNodes[1];
        XmlNode body = top.ChildNodes[0];
        XmlNode method_node = SoapEnv.CreateElement( method);
        XmlAttribute xmlns = SoapEnv.CreateAttribute("xmlns");
         xmlns.InnerText = GetTargetNameSpace(web_service_url + "?WSDL"); 
        method_node.Attributes.Append(xmlns);
        string[] param_list = inputs.Split("&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (string param in param_list)
        {
            string[] parts = param.Split("=".ToCharArray(), StringSplitOptions.None);
            XmlNode param_node = SoapEnv.CreateElement(parts[0].Trim());
            param_node.InnerText = parts[1];
            method_node.AppendChild(param_node);
        }
        body.AppendChild(method_node);
    }
    public string GetTargetNameSpace(string wsdl_url)
    {
        string xml = GetWebPage(wsdl_url);
        if (xml == null)
        {
            return "http://tempuri.org/";
        }
        XmlDocument WebDoc = new XmlDocument();
        WebDoc.LoadXml(xml);
        if (WebDoc.ChildNodes[1].Attributes["targetNamespace"] == null)
            return "http://tempuri.org/";
        else
            return WebDoc.ChildNodes[1].Attributes["targetNamespace"].InnerText;
    }
    public XmlDocument HttpSOAPRequest(Hashtable State, string url, XmlDocument soap_envelope, string proxy)
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        if (proxy != null)
            req.Proxy = new WebProxy(proxy, true);
        int start = url.IndexOf("://") + 3;
        int end = url.IndexOf("/", start);
        req.UserAgent = "PHP-SOAP/5.2.13";
        req.ContentType = "text/xml;charset=\"utf-8\"; action=" + url;
        req.Accept = "text/xml";
        req.Method = "POST";
        req.CookieContainer = new CookieContainer();
        if (State["cookie"] != null)
        {
            Cookie cookie = (Cookie)State["cookie"];
            req.CookieContainer.Add(cookie);
            req.Headers["Cookie"] = cookie.Name + "=" + cookie.Value + "; path=" + cookie.Path;
        }

        Stream stm = req.GetRequestStream();
        soap_envelope.Save(stm);
        stm.Close();
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

        // *** Save the cookies on the persistent object
        if (resp.Cookies.Count > 0)
        {
            Cookie cookie = resp.Cookies[0];
            State["cookie"] = cookie;
        }

        stm = resp.GetResponseStream();
        StreamReader r = new StreamReader(stm);
        XmlDocument TestDoc = new XmlDocument();
        TestDoc.LoadXml(r.ReadToEnd());
        return TestDoc;
    }

    public string HTTPPost(String url, String payload)
    {
        string xml = null;
        WebResponse result = null;

        try
        {
            WebRequest req = WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            StringBuilder UrlEncoded = new StringBuilder();
            Char[] reserved = { '?', '=', '&' };
            byte[] SomeBytes = null;

            if (payload != null)
            {
                int i = 0, j;
                while (i < payload.Length)
                {
                    j = payload.IndexOfAny(reserved, i);
                    if (j == -1)
                    {
                        UrlEncoded.Append(HttpUtility.UrlEncode(payload.Substring(i, payload.Length - i)));
                        break;
                    }
                    UrlEncoded.Append(HttpUtility.UrlEncode(payload.Substring(i, j - i)));
                    UrlEncoded.Append(payload.Substring(j, 1));
                    i = j + 1;
                }
                SomeBytes = Encoding.UTF8.GetBytes(UrlEncoded.ToString());
                req.ContentLength = SomeBytes.Length;
                Stream newStream = req.GetRequestStream();
                newStream.Write(SomeBytes, 0, SomeBytes.Length);
                newStream.Close();
            }
            else
            {
                req.ContentLength = 0;
            }

            result = req.GetResponse();
            Stream ReceiveStream = result.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader sr = new StreamReader(ReceiveStream, encode);
            //Console.WriteLine("\r\nResponse stream received");
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            //Console.WriteLine("HTML...\r\n");
            while (count > 0)
            {
                String str = new String(read, 0, count);
                xml += str;
                count = sr.Read(read, 0, 256);
            }
        }
        catch (Exception ex)
        {
            string message = ex.Message;
            return message;
        }
        finally
        {
            if (result != null)
            {
                result.Close();
            }
        }
        return xml;
    }

    public string LoginToViziAppsFromGoogleApps(Hashtable State, string username)
    {
        try
        {
            string sql = "SELECT * FROM customers WHERE username='" + username + "' AND account_type like '%google_apps%'";
            DB db = new DB();
            DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
            if (rows.Length == 0)
            {
                db.CloseViziAppsDatabase(State);
                return "The username is incorrect.";
            }
            DataRow row = rows[0];
            if (row["status"].ToString() == "inactive")
            {
                db.CloseViziAppsDatabase(State);
                return "Your account is inactive. Contact ViziApps to re-activate your account.";
            }

            //check expiration date
            string expiration_date = row["expiration_date"].ToString();
            if (expiration_date.Length > 0)
            {
                DateTime expiration = DateTime.Parse(expiration_date);
                if (expiration < DateTime.Now.ToUniversalTime())
                {
                    sql = "UPDATE customers SET status='inactive' WHERE customer_id='" + row["customer_id"].ToString() + "'";
                    db.ViziAppsExecuteNonQuery(State, sql);
                    db.CloseViziAppsDatabase(State);
                    return "Your account has expired.";
                }
            }

            State["CustomerID"] = row["customer_id"].ToString();

            string account_type = GetAccountType(row["account_type"].ToString());
            State["AccountType"] = account_type;
            State["CustomerEmail"] = row["email"].ToString();

            Hashtable UsersList = (Hashtable)HttpRuntime.Cache["UsersList"];
            if (UsersList == null)
            {
                //this shouldn't happen so report this now and go on
                String error = "Application Cache UsersList has been set to null";
                TimeZones tz = new TimeZones();
                string NOW = tz.GetCurrentDateTimeMySqlFormat(State);

                sql = "INSERT INTO error_log SET log_id=UUID(), timestamp='" + NOW + "',username='" +
                   username + "',app='no app selectred',error='" + error + "',stacktrace='no stack trace'";
                db.ViziAppsExecuteNonQuery(State, sql);
                db.CloseViziAppsDatabase(State);

                HttpRuntime.Cache["UsersList"] = new Hashtable();
                UsersList = (Hashtable)HttpRuntime.Cache["UsersList"];
            }

            string force_1_user_sessions = row["force_1_user_sessions"].ToString();
            bool one_user_allowed = force_1_user_sessions == "1" || force_1_user_sessions.ToLower() == "true";
            if (UsersList[username] != null)
            {
                Hashtable UserTable = (Hashtable)UsersList[username];
                //check if only 1 user is allowed
                if (one_user_allowed && State["PageRequestIPAddress"] != null && UserTable["PageRequestIPAddress"].ToString() != State["PageRequestIPAddress"].ToString())
                    return "The account is already in use.";
                UserTable["PageRequestIPAddress"] = State["PageRequestIPAddress"].ToString();
                UserTable["SessionID"] = State["SessionID"];
            }
            else
            {
                Hashtable UserTable = new Hashtable();
                UserTable["PageRequestIPAddress"] = State["PageRequestIPAddress"].ToString();
                UserTable["SessionID"] = State["SessionID"];
                UsersList[username] = UserTable;
            }        
 
            //initialize configurations
            State["CustomerStatus"] = row["status"].ToString();
            State["Password"] = "";
            State["Username"] = username;
            SetLoggedIn(State);

            TimeZones zone_util = new TimeZones();
            zone_util.GetDefaultTimeZone(State);

            IncrementNLogins(State);
            LogLastUsed(State);

            string agreed_to_eula = row["agreed_to_eula"].ToString();

            if (username.ToLower() == "admin")
                return "admin";

            else if (agreed_to_eula == "1" || agreed_to_eula.ToLower() == "true")
            {
                return "OK";
            }

            else
                return "agree_to_EULA";

        }
        catch (Exception ex)
        {
            LogError(State, ex);
            return "Internal error in login process.";
        }
    }
  
    public string LoginToViziApps(Hashtable State, string username, string password)
    {
        try
        {
            string sql = "SELECT * FROM customers WHERE username='" + username + "'";
            DB db = new DB();
            DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
            if (rows.Length == 0)
            {
                db.CloseViziAppsDatabase(State);
                return "Either the username or the password is incorrect.";
            }
            DataRow row = rows[0];
            if(row["status"].ToString()== "inactive")
            {
                db.CloseViziAppsDatabase(State);
                return "Your account is inactive. Contact ViziApps to re-activate your account.";
            }

            string db_password = row["password"].ToString();
            if (db_password == password)
            {
                //check expiration date
                string expiration_date = row["expiration_date"].ToString();
                if (expiration_date.Length > 0)
                {
                    DateTime expiration = DateTime.Parse(expiration_date);
                    if (expiration < DateTime.Now.ToUniversalTime())
                    {
                        sql = "UPDATE customers SET status='inactive' WHERE customer_id='" + row["customer_id"].ToString() + "'";
                        db.ViziAppsExecuteNonQuery(State, sql);
                        db.CloseViziAppsDatabase(State);
                        return "Your account has expired.";
                    }
                }

                State["CustomerID"] = row["customer_id"].ToString();
                Util util = new Util();

                string[] account_type_list = GetAccountType(row["account_type"].ToString()).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string AllowedAccountTypes = ConfigurationManager.AppSettings["AllowedAccountTypes"];
                string[] allowed_account_type_list = AllowedAccountTypes.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                bool isAccountTypeAllowed = false;
                foreach (string allowed_account_type in allowed_account_type_list)
                {
                    foreach (string account_type in account_type_list)
                    {
                        if (account_type == allowed_account_type)
                        {
                            isAccountTypeAllowed = true;
                            break;
                        }
                    }
                    if(isAccountTypeAllowed)
                        break;
                }
                if (!isAccountTypeAllowed)
                {
                    foreach (string account_type in account_type_list)
                    {
                        if (account_type == "google_apps")
                            return "If you created a ViziApps account from Google Apps Marketplace, you can only login into ViziApps from your Google account to maintain secure access to your data.";                          
                    }
                    return "Invalid Login";
                }
                State["AccountType"] = GetAccountType(row["account_type"].ToString());

                Hashtable UsersList = (Hashtable)HttpRuntime.Cache["UsersList"];
                if (UsersList == null)
                {
                    //this shouldn't happen so report this now and go on
                    String error = "Application Cache UsersList has been set to null";
                    TimeZones tz = new TimeZones();
                    string NOW = tz.GetCurrentDateTimeMySqlFormat(State);

                    sql = "INSERT INTO error_log SET log_id=UUID(), timestamp='" + NOW + "',username='" +
                       username + "',app='no app selectred',error='" + error + "',stacktrace='no stack trace'";
                    db.ViziAppsExecuteNonQuery(State, sql);
                    db.CloseViziAppsDatabase(State);

                    HttpRuntime.Cache["UsersList"] = new Hashtable();
                    UsersList = (Hashtable)HttpRuntime.Cache["UsersList"];
                }

                string force_1_user_sessions = row["force_1_user_sessions"].ToString();
                bool one_user_allowed = force_1_user_sessions == "1" || force_1_user_sessions.ToLower() == "true"; 
                if (UsersList[username] != null)
                {
                    Hashtable UserTable = (Hashtable)UsersList[username];
                    //check if only 1 user is allowed
                    if (one_user_allowed && State["PageRequestIPAddress"] != null && UserTable["PageRequestIPAddress"].ToString() != State["PageRequestIPAddress"].ToString())
                        return "The account is already in use.";
                    UserTable["PageRequestIPAddress"] = State["PageRequestIPAddress"].ToString();
                    UserTable["SessionID"] = State["SessionID"];
                }
                else
                {
                    Hashtable UserTable = new Hashtable();
                    UserTable["PageRequestIPAddress"] = State["PageRequestIPAddress"].ToString();
                    UserTable["SessionID"] = State["SessionID"];
                    UsersList[username] = UserTable;
                }                

                //initialize configurations
                State["CustomerStatus"] = row["status"].ToString();
                State["Password"] = password;
                State["Username"] = username;
                SetLoggedIn(State);

                TimeZones zone_util = new TimeZones();
                zone_util.GetDefaultTimeZone(State);

                IncrementNLogins(State);
                LogLastUsed(State);

                string agreed_to_eula = row["agreed_to_eula"].ToString();

                if (username.ToLower() == "admin")
                    return "admin";

                else if (agreed_to_eula == "1" || agreed_to_eula.ToLower() == "true")
                {
                    return "OK";
                }

                else
                    return "agree_to_EULA";
            }
            else
            {
                db.CloseViziAppsDatabase(State);
                return "Either the username or the password is incorrect.";
            }
        }
        catch (Exception ex)
        {
            LogError(State, ex);
            return "Internal error in login process.";
        }
    }
    public string GetAccountType(string account_entry)
    {
        string[] split = account_entry.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (string entry in split)
        {
            string[] parts = entry.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                if (parts[0] == "type")
                    return parts[1];
            }
        }
        return null;
    }
    public string GetAccountType(Hashtable State)
    {
        if (State == null || State["CustomerID"] == null)
            return "viziapps";
         DB db = new DB();
         string sql = "SELECT account_type FROM customers WHERE customer_id = '" + State["CustomerID"].ToString() + "'";
         string account_type = db.ViziAppsExecuteScalar(State, sql);
         return GetAccountType(account_type);
    }
    public void SetLoggedIn(Hashtable State)
    {
        if (ConfigurationManager.AppSettings["DebugSessions"] == "true")
        {
            StartSessionLog(State);
        }

        State["LoggedIn"] = true;
     }
    public bool HasAgreedToEula(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT agreed_to_eula FROM customers WHERE customer_id = '" + State["CustomerID"].ToString() + "'";
        string agreed_to_eula = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return (agreed_to_eula.ToLower() == "false" || agreed_to_eula == "0") ? false : true;
    }
    public Hashtable ParseAccountType(string account_type)
    {
        string[] tokens = account_type.Split(';');
        Hashtable table = new Hashtable();
        foreach (string token in tokens)
        {
            if (token.Length == 0)
                continue;
            string[] parse = token.Split('=');
            if (parse.Length == 2)
            {
                table[parse[0]] = parse[1];
            }
            else
                throw new Exception("Error: bad format in account type: " + account_type);
        }
        return table;
    }
    public bool ActivateCustomerAccount(Hashtable State, string customer_id)
    {
        DB db = new DB();
        string sql = "SELECT COUNT(*) FROM customers WHERE customer_id='" + customer_id + "'";
        string count = db.ViziAppsExecuteScalar(State,sql);
        if(count == "0")
            return false;

        sql = "UPDATE customers SET status='trial' WHERE customer_id='" + customer_id + "'";
        db.ViziAppsExecuteNonQuery(State,sql);

        db.CloseViziAppsDatabase(State);
        return true;
    }
    public string CreateMobiFlexAccount(Hashtable State, 
        string username, string password, string security_question, string security_answer, string first_name, string last_name,
        string email, string phone, string account_type, string referral_source, string app_to_build, string status)
    {
        StringBuilder sql = new StringBuilder("INSERT INTO customers SET ");
        string customer_id = Guid.NewGuid().ToString();
        sql.Append("customer_id='" + customer_id + "'");
        sql.Append(",username='" + username + "'");
        sql.Append(",password='" + MySqlFilter(password) + "'");
        if(security_question.Length > 0)
                sql.Append(",security_question='" + security_question.Replace("'","''") + "'");
        if (security_answer.Length > 0)
                 sql.Append(",security_answer='" + security_answer.Replace("'", "''") + "'");
        sql.Append(",first_name='" + MySqlFilter(first_name) + "'");
        sql.Append(",last_name='" + MySqlFilter(last_name) + "'");
        sql.Append(",email='" + email + "'");
        double DefaultTimeZoneDeltaHours = Convert.ToDouble(State["DefaultTimeZoneDeltaHours"].ToString());
        TimeZones zone_util = new TimeZones();
        string zone = Convert.ToString(DefaultTimeZoneDeltaHours + zone_util.GetDaylightSavingsTimeOffset(DateTime.Now.ToUniversalTime()));
        sql.Append(",default_time_zone_delta_hours='" + zone + "'");
        if (phone != null && phone.Length > 0)
        {
            sql.Append(",phone='" + phone + "'");
        }

        sql.Append(",account_type='"+ account_type + "'");
        if (referral_source != null && referral_source.IndexOf("->") < 0)
        {
            sql.Append(",referral_source='" + referral_source + "'");
        }
        if (app_to_build != null && app_to_build.Length > 0)
        {
            sql.Append(",app_to_build='" + MySqlFilter(app_to_build) + "'");
        }
        string NOW = DateTime.Now.ToUniversalTime().ToString("s").Replace("T", " ");

        sql.Append(",registration_date_time='" + NOW + "',status='" + status + "'");
        DB db = new DB();
        db.ViziAppsExecuteNonQuery(State,sql.ToString());
        db.CloseViziAppsDatabase(State);
        return customer_id;
    }
    public void LogLastUsed(Hashtable State)
    {
        try
        {
            string NOW = DateTime.Now.ToUniversalTime().ToString("s").Replace("T", " ");
            string sql = "UPDATE customers SET last_use_date_time='" + NOW + "',last_user_host_address='" + State["UserHostAddress"].ToString() + "' WHERE customer_id='" + State["CustomerID"].ToString() + "'";
            DB db = new DB();
            db.ViziAppsExecuteNonQuery(State,sql);
            db.CloseViziAppsDatabase(State);
        }
        catch { } // an exception may happen at logout because the State is undefined then. so let it go
    }
    public void IncrementNLogins(Hashtable State)
    {
            DB db = new DB();
            string sql = "UPDATE customers SET n_logins=n_logins+1 WHERE customer_id='" + State["CustomerID"].ToString() + "'";
            db.ViziAppsExecuteNonQuery(State, sql);
            db.CloseViziAppsDatabase(State);
     }
    public string GetLastUserHostAddress(Hashtable State)
    {
        if (State["CustomerID"] != null)
        {
            string sql = "SELECT last_user_host_address FROM customers WHERE customer_id='" + State["CustomerID"].ToString() + "'";
            DB db = new DB();
            string last_user_host_address = db.ViziAppsExecuteScalar(State, sql);
            db.CloseViziAppsDatabase(State);
            return last_user_host_address;
        }
        else
            return null;
    }
    public string FilterDataBaseFieldValue(string type, string raw_value)
    {
        if (type.ToLower() == "telephone" || type.ToLower() == "phone" || type.ToLower() == "us_phone")
        {
            return GetPhoneDigits(raw_value);
        }
        else if (type.ToLower() == "number")
        {
            return raw_value.Replace("'", "''").Replace(@"\", @"\\").Replace(",", "");
        }
        else if (type.ToLower() == "currency")
        {
            return raw_value.Replace("'", "''").Replace(@"\", @"\\").Replace(",", "").Replace("$", "");
        }
        else if (type.ToLower() == "name")
        {
            StringBuilder output = new StringBuilder();
            foreach (char c in raw_value)
            {
              if(c.Equals('~') || 
				c.Equals('!') || 
				c.Equals('?') ||
				c.Equals('$') ||
				c.Equals('%') ||
				c.Equals('^') ||
				c.Equals('*') ||
				c.Equals('.') ||
				c.Equals(',') ||
				c.Equals('-') ||
				c.Equals('_') ||
				c.Equals('=') ||
				c.Equals('(') ||
				c.Equals(')') ||
				c.Equals('[') ||
				c.Equals(']') ||
				c.Equals('{') ||
				c.Equals('}') ||
				c.Equals('|') ||
				c.Equals('\\')||
				c.Equals(';') ||
				c.Equals(':') ||
				c.Equals('"') ||
				c.Equals('/') ||
				c.Equals('<') ||
				c.Equals('>') )
                     output.Append(' ');
              else if(c.Equals('`') || 
                      c.Equals('\'') )
                  continue;
              else if(c.Equals('@'))
                  output.Append(" at ");
              else if(c.Equals('&') || c.Equals('+'))
                  output.Append(" and ");
              else if(c.Equals('#'))
                  output.Append(" number ");
              else if(Convert.ToUInt16(c) < 32)
                  continue;
             else
                 output.Append(c);
            }
            return output.ToString();
        }
 
        else  
            return raw_value.Trim().Replace("'", "''").Replace(@"\", @"\\");
    }
    public void UpdateUserCredentials(Hashtable State,
        string application_id, DataRowCollection data_rows, string update_type)
    {
        string sql = null;
        DB db = new DB();
        if (update_type == "replace")
        {
            sql = "DELETE FROM users WHERE application_id='" + application_id + "'";
            db.ViziAppsExecuteNonQuery(State, sql);
        }
        StringBuilder sb = new StringBuilder("INSERT INTO users (user_id,customer_id,application_id,username,password,status) VALUES ");
        string customer_id = State["CustomerID"].ToString();

        bool is_first = true;
        foreach (DataRow row in data_rows)
        {
            if (is_first)
            {
                is_first = false;
                continue;
            }
            sb.Append("(UUID(),'" +
            customer_id +
            "','" + application_id +
            "','" + row[0].ToString() +
            "','" + row[1].ToString() +
            "','active'),");
        }
        sb.Remove(sb.Length - 1, 1); //remove last comma
        db.ViziAppsExecuteNonQuery(State, sb.ToString());
        db.CloseViziAppsDatabase(State);
    }
    public void UpdateUserCredentials(Hashtable State,
       string application_id, string[] credential)
    {
        DB db = new DB();
        string sql = "DELETE FROM users WHERE application_id='" + application_id + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        StringBuilder sb = new StringBuilder("INSERT INTO users (user_id,customer_id,application_id,username,password,status) VALUES ");
        string customer_id = State["CustomerID"].ToString();

        sb.Append("(UUID(),'" +
        customer_id +
        "','" + application_id +
        "','" + credential[0] +
        "','" + credential[1] +
        "','active')");

        db.ViziAppsExecuteNonQuery(State, sb.ToString());
        db.CloseViziAppsDatabase(State);
    }
    public void DeleteUserCredentials(Hashtable State, string application_id)
    {
        DB db = new DB();
        string sql = "DELETE FROM users WHERE application_id='" + application_id + "'";
        db.ViziAppsExecuteNonQuery(State, sql);
        db.CloseViziAppsDatabase(State);
    }
    public ArrayList GetEndUserCredentials(Hashtable State)
    {
        if(State["SelectedApp"] == null)
            return new ArrayList();

        string application_id = GetAppID(State);
        
        DB db = new DB();
        string sql = "SELECT username,password FROM users WHERE application_id='" + application_id + "'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        ArrayList credentials = new ArrayList();
        foreach (DataRow row in rows)
        {
            string[] credential = new string[2];
            credential[0] = row["username"].ToString();
            credential[1] = row["password"].ToString();
            credentials.Add(credential);
        }
        db.CloseViziAppsDatabase(State);
        return credentials;
    }
    protected string AlphaNumericFilter(string input)
    {
        StringBuilder sb = new StringBuilder();
        foreach (Char c in input)
        {
            if(Char.IsLetterOrDigit(c) || c == ' ')
                sb.Append(c);
        }
        return sb.ToString();
    }
    public void SelectForTesting(Hashtable State)
    {
        DB db = new DB();

        //clear any previous staging
        string sql = "UPDATE applications SET in_staging=0, status='configuration' WHERE status='staging' AND customer_id='" + State["CustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        sql = "UPDATE applications SET in_staging=0, status='production' WHERE status='staging/production' AND customer_id='" + State["CustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        sql = "UPDATE applications SET in_staging=1,status='staging' WHERE status='configuration' AND application_name='" + State["SelectedApp"].ToString() +
            "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        sql = "UPDATE applications SET in_staging=1,status='staging/production' WHERE status='production' AND application_name='" + State["SelectedApp"].ToString() +
            "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        db.CloseViziAppsDatabase(State);
    }
    public bool IsAppSelectedForTesting(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT status FROM applications WHERE application_name='" + State["SelectedApp"].ToString() +
                    "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        string status = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return (status.Contains("staging")) ? true : false;
    }
    public string GetAppID(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT application_id FROM applications WHERE application_name='" + State["SelectedApp"].ToString() + 
                    "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        string application_id = db.ViziAppsExecuteScalar(State,sql);
        db.CloseViziAppsDatabase(State);
        return application_id;
    }
    public string GetAppIDFromProductionAppName(Hashtable State, string app_name)
    {
        DB db = new DB();
        if (State["CustomerID"] == null)
            State["CustomerID"] = GetCustomerIDFromUsername(State, State["Username"].ToString());

        string sql = "SELECT application_id FROM applications WHERE production_app_name='" + app_name +
                    "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        string application_id = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return application_id;
    }
    public string GetAppIDFromAppName(Hashtable State, string application_name)
    {
        DB db = new DB();
        if (State["CustomerID"] == null)
            State["CustomerID"] = GetCustomerIDFromUsername(State, State["Username"].ToString());

        string sql = "SELECT application_id FROM applications WHERE application_name='" + application_name + "' AND customer_id='" + State["CustomerID"].ToString() + "'";

        string application_id = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return application_id;

    }
    public ArrayList GetCustomerAppNames(Hashtable State)
    {
        ArrayList list = new ArrayList();
        DB db = new DB();
        string sql = "SELECT application_name FROM applications WHERE customer_id='" + State["CustomerID"].ToString() + "'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        foreach (DataRow row in rows)
        {
            list.Add(row["application_name"].ToString());
        }
        db.CloseViziAppsDatabase(State);
        return list;
    }
    public string GetAppName(Hashtable State, string application_id)
    {
        DB db = new DB();
        string sql = "SELECT application_name FROM applications WHERE application_id='" + application_id + "'";
        string application_name = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return application_name;

    }
    public string GetAppType(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT application_type FROM applications WHERE application_name='" + State["SelectedApp"].ToString() + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        string application_type = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return application_type;
    }
    public void SetAppType(Hashtable State, string app_type)
    {
        XmlUtil x_util = new XmlUtil();
        x_util.SetAppType(State, app_type);
        DB db = new DB();
        string sql = "SELECT application_type FROM applications WHERE application_name='" + State["SelectedApp"].ToString() + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        string previous_app_type = db.ViziAppsExecuteScalar(State, sql);
        if (previous_app_type != app_type)
        {
            if (app_type == Constants.WEB_APP_TYPE)
            {
               
                x_util.ConvertNativeAppToWebApp(State);
            }
            sql = "UPDATE applications SET application_type='" + app_type + "' WHERE application_name='" + State["SelectedApp"].ToString() + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
            db.ViziAppsExecuteNonQuery(State, sql);
        }
        db.CloseViziAppsDatabase(State);
    }
    public string MySqlFilter(string input)
    {
        return input.Replace(@"\", @"\\").Replace("’", "'").Replace("'", @"\'").Replace("\"", "\\\"");
    }
 
    public string DecodeMySql(string input)
    {
        if (input == null)
            return null;
        return input.Replace("''", "'").Replace("\\\"", "\"").Replace(@"\\", @"\");
    }
    public bool DoesAppExist(Hashtable State)
    {
       return DoesAppExist( State, State["SelectedApp"].ToString());
    }
    public bool DoesAppExist(Hashtable State,string app_name)
    {
        DB db = new DB();
        string sql = "SELECT COUNT(*) FROM applications WHERE customer_id='" + State["CustomerID"] +
            "' AND application_name='" + app_name + "'";
        string n_matches = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return (n_matches == null || n_matches == "0") ? false : true;
    }
    public void CreateApp(Hashtable State, string page_name, string primary_device_type,String app_description)
    {
        string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", "");

        //create basic app data
        string application_name = State["SelectedApp"].ToString();
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("INSERT into applications SET ");
        string application_id = Guid.NewGuid().ToString();
        b_sql.Append("application_id='" + application_id + "',");
        b_sql.Append("customer_id='" + State["CustomerID"].ToString() + "',");
        b_sql.Append("username='" + State["Username"].ToString() + "',");
        b_sql.Append("application_name='" + application_name + "',");
        b_sql.Append("application_type='" + State["SelectedAppType"].ToString() + "',");
        if (State["DefaultButtonURL"] != null)
            b_sql.Append("default_button_image='" + State["DefaultButtonURL"].ToString() + "',");
        b_sql.Append("description='" + app_description.Replace("'", "''").Replace(@"\", @"\\") + "',");
        XmlUtil x_util = new XmlUtil();
 
        b_sql.Append("date_time_modified='" + NOW + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());

        db.CloseViziAppsDatabase(State);

        //create stage app xml in database
        x_util.CreateStagingAppXml(State, primary_device_type, application_name, application_id, page_name);

    }
    public bool DoesAppExistInAccount(Hashtable State, string new_app_name)
    {
        DB db = new DB();

        //does the app already exist
        string sql = "SELECT COUNT(*) FROM applications WHERE customer_id='" + State["CustomerID"] +
            "' AND application_name='" + new_app_name + "'";
        string n_matches = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        if (n_matches != null && n_matches != "0")    
            return true;
        
        return false;
    }
    public bool CopyTemplateApp(Hashtable State,
                string template_app_name,string new_app_name)
    {
        DB db = new DB();

        //This function assumes that the new_app_name is unique;
        string sql = "SELECT customer_id FROM customers WHERE username='" + State["TemplatesAccount"].ToString() + "'";
        string customer_id = db.ViziAppsExecuteScalar(State, sql);

        StringBuilder b_sql = new StringBuilder("SELECT * FROM applications ");
        b_sql.Append("WHERE application_name='" + template_app_name + "'");
        b_sql.Append(" AND customer_id='" + customer_id + "'");
        DataRow[] rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
        DataRow row = rows[0];

        string previous_application_id = row["application_id"].ToString();

        string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", "");

        b_sql = new StringBuilder("INSERT into applications SET ");
        string application_id = Guid.NewGuid().ToString();
        b_sql.Append("application_id='"+ application_id + "',");
        b_sql.Append("customer_id='" + State["CustomerID"] + "',");
        b_sql.Append("username='" + State["Username"].ToString() + "',");

        XmlUtil x_util = new XmlUtil();
        string new_xml = x_util.RenameAppXmlWithID(State, row["staging_app_xml"].ToString(), new_app_name, application_id);

        b_sql.Append("staging_app_xml='" + MySqlFilter(new_xml) + "',");
        if (row["custom_header_html"] != null)
             b_sql.Append("custom_header_html='" + MySqlFilter(row["custom_header_html"].ToString()) + "',");
        b_sql.Append("application_name='" + new_app_name + "',");
        b_sql.Append("application_type='" + row["application_type"].ToString() + "',");
 
        if (row["default_button_image"] != null)
             b_sql.Append("default_button_image='" + row["default_button_image"].ToString() + "',");        
        b_sql.Append("description='" + row["description"].ToString().Replace("'", "''").Replace(@"\", @"\\") + "',");
        b_sql.Append("date_time_modified='" + NOW + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());
        
        //get all the pages
        sql = "SELECT * FROM application_pages WHERE application_id='" + previous_application_id + "'";
        rows = db.ViziAppsExecuteSql(State, sql);

        //insert all the pages into the new app
        foreach (DataRow page_row in rows)
        {
             sql = "INSERT INTO application_pages (application_page_id,application_id,page_name,page_image_url,date_time_modified) VALUES (UUID(),'" +
             application_id + "','" +
             page_row["page_name"].ToString() + "','" +
             page_row["page_image_url"].ToString() + "','" + NOW + "')";
             db.ViziAppsExecuteNonQuery(State, sql);
        }
        db.CloseViziAppsDatabase(State);
        //reset
        State["AppXmlDoc"] = null;
        return true;
    }
    public void CopyAppToAccount(Hashtable State, string application_name)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT * FROM applications ");
        b_sql.Append("WHERE application_name='" + application_name + "'");
        b_sql.Append(" AND customer_id='" + State["CopyApplicationFromCustomerID"].ToString() + "'");
        DataRow[] rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
        DataRow row = rows[0];

        string previous_application_id = row["application_id"].ToString();
        string application_id = Guid.NewGuid().ToString();

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(DecodeMySql(row["staging_app_xml"].ToString()));
 
        //delete any app with the same name
        db.ViziAppsExecuteNonQuery(State, "DELETE FROM applications WHERE application_name='" + application_name + "' AND customer_id='" + State["CopyApplicationToCustomerID"].ToString()  + "'");

        string username = db.ViziAppsExecuteScalar(State, "SELECT username FROM customers WHERE customer_id='" + State["CopyApplicationToCustomerID"].ToString()  + "'");

        b_sql = new StringBuilder("INSERT into applications SET ");
        b_sql.Append("application_id='" + application_id  + "',");
        b_sql.Append("customer_id='" + State["CopyApplicationToCustomerID"].ToString() + "',");
        b_sql.Append("username='" + username + "',");

        XmlUtil x_util = new XmlUtil();
        string new_xml = x_util.RenameAppXmlWithID(State, row["staging_app_xml"].ToString(), application_name, application_id);

        b_sql.Append("staging_app_xml='" + MySqlFilter(new_xml) + "',");
        if (row["custom_header_html"] != null)
            b_sql.Append("custom_header_html='" + MySqlFilter(row["custom_header_html"].ToString()) + "',");
        b_sql.Append("application_name='" + application_name + "',");
        b_sql.Append("application_type='" + row["application_type"].ToString() + "',");
         
        if (row["default_button_image"] != null)
            b_sql.Append("default_button_image='" + row["default_button_image"].ToString() + "',");        

        b_sql.Append("description='" + row["description"].ToString().Replace("'", "''").Replace(@"\", @"\\") + "',");
        string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", "");
        b_sql.Append("date_time_modified='" + NOW + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());

        //get all the pages
        string sql = "SELECT * FROM application_pages WHERE application_id='" + previous_application_id + "'";
        rows = db.ViziAppsExecuteSql(State, sql);

        //insert all the pages into the new app
        foreach (DataRow page_row in rows)
        {
           sql = "INSERT INTO application_pages (application_page_id,application_id,page_name,page_image_url,date_time_modified) VALUES (UUID(),'" +
           application_id + "','" +
           page_row["page_name"].ToString() + "','" +
           page_row["page_image_url"].ToString() + "','" + NOW + "')";
           db.ViziAppsExecuteNonQuery(State, sql);
        }
        db.CloseViziAppsDatabase(State);
        //reset
        State["AppXmlDoc"] = null;
    }
    public void CopyApp(Hashtable State, string new_application_name)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT * FROM applications ");
        b_sql.Append("WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        DataRow[] rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
        DataRow row = rows[0];

        string previous_application_id = row["application_id"].ToString();

        b_sql = new StringBuilder("INSERT into applications SET ");
        string application_id = Guid.NewGuid().ToString();
        b_sql.Append("application_id='" + application_id + "',");
        b_sql.Append("customer_id='" + State["CustomerID"] + "',");
        b_sql.Append("username='" + State["Username"].ToString() + "',");

        XmlUtil x_util = new XmlUtil();
        string new_xml = x_util.RenameAppXmlWithID(State, row["staging_app_xml"].ToString(), new_application_name, application_id);

        b_sql.Append("staging_app_xml='" + MySqlFilter(new_xml) + "',");
        if (row["custom_header_html"] != null)
            b_sql.Append("custom_header_html='" + MySqlFilter(row["custom_header_html"].ToString()) + "',");
        b_sql.Append("application_name='" + new_application_name + "',");
        b_sql.Append("application_type='" + row["application_type"].ToString() + "',");
 
        if (row["default_button_image"] != null)
            b_sql.Append("default_button_image='" + row["default_button_image"].ToString() + "',");        

        b_sql.Append("description='" + row["description"].ToString().Replace("'", "''").Replace(@"\", @"\\") + "',");
        string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", "");
        b_sql.Append("date_time_modified='" + NOW + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());

        //get all the pages
        string sql = "SELECT * FROM application_pages WHERE application_id='" + previous_application_id + "'";
        rows = db.ViziAppsExecuteSql(State, sql);

        //insert all the pages into the new app
        foreach (DataRow page_row in rows)
        {
           sql = "INSERT INTO application_pages (application_page_id,application_id,page_name,page_image_url,date_time_modified) VALUES (UUID(),'" +
           application_id + "','" +
           page_row["page_name"].ToString() + "','" +
           page_row["page_image_url"].ToString() + "','" + NOW + "')";
           db.ViziAppsExecuteNonQuery(State, sql);
        }
         db.CloseViziAppsDatabase(State);
        //reset
         State["AppXmlDoc"] = null;
    }
    public string ScaleYValues(Hashtable State, string html)
    {
        double from_display_height = Constants.IPHONE_DISPLAY_HEIGHT_D;
        switch (State["SelectedDeviceType"].ToString())
        {
            case Constants.ANDROID_PHONE:
                from_display_height = Constants.ANDROID_PHONE_DISPLAY_HEIGHT_D;
                break;
        }

        double to_display_height = Constants.IPHONE_DISPLAY_HEIGHT_D;
        switch (State["SelectedDeviceType"].ToString())
        {
            case Constants.ANDROID_PHONE:
                to_display_height = Constants.ANDROID_PHONE_DISPLAY_HEIGHT_D;
                break;
        }
        double y_factor = to_display_height / from_display_height;

        HtmlDocument HtmlDoc = new HtmlDocument();
        HtmlDoc.LoadHtml(html);
       
        foreach (HtmlNode node in HtmlDoc.DocumentNode.ChildNodes)
        {
            if (node.Attributes["style"] != null)
            {
                node.Attributes["style"].Value = RescaleYStyleValues(node.Attributes["style"].Value, y_factor);
            }
        }
        return HtmlDoc.DocumentNode.InnerHtml;

    }
    public double GetYFactor(Hashtable State)
    {
        double from_display_height = Constants.IPHONE_DISPLAY_HEIGHT_D;
        switch (State["SelectedDeviceType"].ToString())
        {
            case Constants.ANDROID_PHONE:
                from_display_height = Constants.ANDROID_PHONE_DISPLAY_HEIGHT_D;
                break;
            case Constants.BLACKBERRY_TORCH:
                from_display_height = Constants.BLACKBERRY_TORCH_DISPLAY_HEIGHT_D;
                break;
            case Constants.BLACKBERRY_BOLD:
                from_display_height = Constants.BLACKBERRY_BOLD_DISPLAY_HEIGHT_D;
                break;
        }

        double to_display_height = Constants.IPHONE_DISPLAY_HEIGHT_D;
        switch (State["SelectedDeviceType"].ToString())
        {
            case Constants.ANDROID_PHONE:
                to_display_height = Constants.ANDROID_PHONE_DISPLAY_HEIGHT_D;
                break;
            case Constants.BLACKBERRY_TORCH:
                to_display_height = Constants.BLACKBERRY_TORCH_DISPLAY_HEIGHT_D;
                break;
            case Constants.BLACKBERRY_BOLD:
                to_display_height = Constants.BLACKBERRY_BOLD_DISPLAY_HEIGHT_D;
                break;
        }
        return to_display_height / from_display_height;
    }
    public string UnScaleYValues(Hashtable State, string html)
    {
        double from_display_height = Constants.IPHONE_DISPLAY_HEIGHT_D;
        switch (State["SelectedDeviceType"].ToString())
        {
            case Constants.ANDROID_PHONE:
                from_display_height = Constants.ANDROID_PHONE_DISPLAY_HEIGHT_D;
                break;
            case Constants.BLACKBERRY_TORCH:
                from_display_height = Constants.BLACKBERRY_TORCH_DISPLAY_HEIGHT_D;
                break;
            case Constants.BLACKBERRY_BOLD:
                from_display_height = Constants.BLACKBERRY_BOLD_DISPLAY_HEIGHT_D;
                break;
        }

        double to_display_height = Constants.IPHONE_DISPLAY_HEIGHT_D;
        switch (State["SelectedDeviceType"].ToString())
        {
            case Constants.ANDROID_PHONE:
                to_display_height = Constants.ANDROID_PHONE_DISPLAY_HEIGHT_D;
                break;
            case Constants.BLACKBERRY_TORCH:
                to_display_height = Constants.BLACKBERRY_TORCH_DISPLAY_HEIGHT_D;
                break;
            case Constants.BLACKBERRY_BOLD:
                to_display_height = Constants.BLACKBERRY_BOLD_DISPLAY_HEIGHT_D;
                break;
        }
        double y_factor = to_display_height / from_display_height;

        HtmlDocument HtmlDoc = new HtmlDocument();
        HtmlDoc.LoadHtml(html);

        foreach (HtmlNode node in HtmlDoc.DocumentNode.ChildNodes)
        {
            if (node.Attributes["style"] != null)
            {
                node.Attributes["style"].Value = RescaleYStyleValues(node.Attributes["style"].Value, y_factor);
            }
        }
        return HtmlDoc.DocumentNode.InnerHtml;

    }
    public string RescaleYStyleValues(string style_in, double y_factor)
    {
        if(!style_in.Contains("top:") && !style_in.Contains("height:"))
            return style_in;
        string style_out = ScaleStylePxAttribute(style_in, "top:", y_factor);
        return ScaleStylePxAttribute(style_out, "height:", y_factor); ;
    }
    public string ScaleStylePxAttribute(String style, string attribute, double factor)
    {
        int start = style.IndexOf(attribute);
        if (start < 0)
            return style;

        int first = start + attribute.Length;
        int second = style.IndexOf("px", first);
        double value = Convert.ToDouble(style.Substring(first, second - first).Trim());
        value *= factor;
        string change = (Convert.ToInt32(value)).ToString();
        return style.Substring(0, first) + change + style.Substring(second);
    }
    private string AddToStyleTop(string style_in, int top)
    {
        int start = style_in.IndexOf("top:");
        if(start < 0)
            return style_in;

        int first = start + 4;
        int second = style_in.IndexOf(";",first);
        string change = (Convert.ToInt32(style_in.Substring(first,second-first).Replace("px","").Trim()) + top).ToString() + "px";
        string style_out = style_in.Substring(0, first) + change + style_in.Substring(second);
        return style_out;
    }
    public Hashtable AddToStyleAttribute(Hashtable style_attr, string style)
    {
        string[] parts = style.Split(";".ToCharArray());
        
        for (int i = 0;i<parts.Length;i++)
        {
            if (parts[i].Trim().Length == 0)            
                continue;
            
            string style_part = parts[i].Trim();
            string[] sub = style_part.Split(":".ToCharArray());
            if (sub[1].Trim().StartsWith("url"))
            {
                if ((i + 1) >= parts.Length || (i + 2) >= parts.Length)
                    continue;
                sub[1] += ";" + parts[i + 1] + ";" + parts[i + 2];
                parts[i + 1] = "";
                parts[i + 2] = "";
            }
            style_attr[sub[0].Trim()] = sub[1].Trim();  
     
        }
        return style_attr;
    }
    public byte[] GetApplicationIcon(Hashtable State)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT icon FROM applications ");
        b_sql.Append("WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        DataRow[] rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
        DataRow row = rows[0];
        db.CloseViziAppsDatabase(State);
        return (byte[])row["icon"];
    }
    public void SetApplicationIcon(Hashtable State,byte[] icon)
    {
        string connect = ConfigurationManager.AppSettings["ViziAppsAdminConnectionString"];
        MySqlConnection myConn = new MySqlConnection(connect);
        MySqlCommand nonqueryCommand = myConn.CreateCommand();

        StringBuilder b_sql = new StringBuilder("UPDATE applications SET icon=@ImageFile ");
        b_sql.Append("WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");

        nonqueryCommand.CommandText = b_sql.ToString();
        MySqlParameter DocumentFileParameter = new MySqlParameter("@ImageFile", MySqlDbType.Binary);
        DocumentFileParameter.Value = icon;
        nonqueryCommand.Parameters.Add(DocumentFileParameter);
        myConn.Open();
        nonqueryCommand.ExecuteNonQuery();
        myConn.Close(); 
    }
    /*public void SetApplicationLongDescription(Hashtable State, string long_description)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("UPDATE applications SET appstore_description='" + MySqlFilter(long_description) + "'");
        b_sql.Append(" WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
    }
    public void SetApplicationKeywords(Hashtable State, string keywords)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("UPDATE applications SET keywords='" + MySqlFilter(keywords) + "'");
        b_sql.Append(" WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
    }
    public void SetApplicationPricing(Hashtable State, string price)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("UPDATE applications SET price='" + price + "'");
        b_sql.Append(" WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
    }*/
    public void SetUnlimitedUsers(Hashtable State)
    {
        DB db = new DB();
        String sql = "UPDATE applications SET has_unlimited_users=1,use_1_user_credential=0 WHERE application_name='" +
            State["SelectedApp"].ToString() + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);
        db.CloseViziAppsDatabase(State);
        string application_id = GetAppID(State);
        DeleteUserCredentials(State, application_id);
    }
    public void SetLimitedUsersCredentialMethod(Hashtable State, bool UseSingleCredential, string username, string password)
    {
        DB db = new DB();
        string use_1_user_credential = UseSingleCredential ? "true" : "false";
        StringBuilder b_sql = new StringBuilder("UPDATE applications SET has_unlimited_users=0,use_1_user_credential=" + use_1_user_credential + " ");
        b_sql.Append(" WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());

        string application_id = GetAppID(State);
        if (UseSingleCredential)
        {
            string[] credential = new string[2];
            credential[0] = username;
            credential[1] = password;
            UpdateUserCredentials(State, application_id, credential);
         }
        else
            DeleteUserCredentials(State, application_id);


        db.CloseViziAppsDatabase(State);
    }
    public void SetProductionAppName(Hashtable State, string production_app_name)
    {
        DB db = new DB();
        string sql = "UPDATE applications SET production_app_name='" + production_app_name + "' " +
        "WHERE application_name='" + State["SelectedApp"].ToString() + "'" +
        " AND customer_id='" + State["CustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);
        db.CloseViziAppsDatabase(State);
    }
    public string GetProductionAppName(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT production_app_name FROM applications WHERE application_name='" + State["SelectedApp"].ToString() + "'" +
        " AND customer_id='" + State["CustomerID"].ToString() + "'";
        string production_app_name =  db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return production_app_name;
    }
    public String GetApplicationLargeIcon(Hashtable State,string application_id)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT url FROM branding_images ");
        b_sql.Append("WHERE application_id='" + application_id + "' AND type='icon' AND width=512");
        string large_icon_url = db.ViziAppsExecuteScalar(State, b_sql.ToString());       
        db.CloseViziAppsDatabase(State);
        return large_icon_url;
    }
    public String GetApplicationIcon(Hashtable State, string application_id, string size)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT url FROM branding_images ");
        b_sql.Append("WHERE application_id='" + application_id + "' AND type='icon' AND width=" + size);
        string icon_url = db.ViziAppsExecuteScalar(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
        return icon_url;
    }
    public System.Drawing.Image ResizeImage(System.Drawing.Image imgToResize, Size size)
    {
        int sourceWidth = imgToResize.Width;
        int sourceHeight = imgToResize.Height;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)size.Width / (float)sourceWidth);
        nPercentH = ((float)size.Height / (float)sourceHeight);

        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap b = new Bitmap(destWidth, destHeight);
        Graphics g = Graphics.FromImage((System.Drawing.Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();

        return (System.Drawing.Image)b;
    }
    public void SetApplicationLargeIcon(Hashtable State, string application_id, Bitmap bitmap, string file_name, string file_path)
    {
        DeleteLargeIcon(State, application_id);

        AmazonS3 s3 = new AmazonS3();
 
        string url = s3.UploadFile(State, file_name, file_path);
        if (!url.StartsWith("http"))
            throw new Exception("Error in SetApplicationLargeIcon: s3.UploadFile failed.");

        if (File.Exists(file_path))
            File.Delete(file_path);

        string[] icon_sizes = {"114","72","58","57","48","36","29"};

        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("INSERT INTO branding_images SET ");
        b_sql.Append("branding_image_id='" + Guid.NewGuid().ToString() + "'");
        b_sql.Append(",application_id='" + application_id + "'");
        b_sql.Append(",width=512,height=512,type='icon'");
        b_sql.Append(",url='" + url + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());

        //do all the other sizes
        foreach (string icon_size in icon_sizes)
        {
            int size = Convert.ToInt32(icon_size);
            Bitmap resized_bitmap = (Bitmap)ResizeImage(bitmap, new Size(size,size));
            resized_bitmap.Save(file_path);
            url = s3.UploadFile(State, file_name.Insert(file_name.Length-4,"_" +icon_size), file_path);
            if (!url.StartsWith("http"))
                throw new Exception("Error in SetApplicationLargeIcon: s3.UploadFile failed.");

            if (File.Exists(file_path))
                File.Delete(file_path);

            b_sql = new StringBuilder("INSERT INTO branding_images SET ");
            b_sql.Append("branding_image_id='" + Guid.NewGuid().ToString() + "'");
            b_sql.Append(",application_id='" + application_id + "'");
            b_sql.Append(",width=" + icon_size + ",height=" + icon_size + ",type='icon'");
            b_sql.Append(",url='" + url + "'");
            db.ViziAppsExecuteNonQuery(State, b_sql.ToString());
        }
        db.CloseViziAppsDatabase(State);
    }
    public void DeleteLargeIcon(Hashtable State, string application_id)
    {
        DB db = new DB();
        string sql = "DELETE FROM branding_images WHERE application_id='" + application_id + "' AND type='icon'";
        db.ViziAppsExecuteNonQuery(State, sql);
        db.CloseViziAppsDatabase(State);
    }
    public string GetApplicationSplashImage(Hashtable State, string application_id)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT url FROM branding_images ");
        b_sql.Append("WHERE application_id='" + application_id + "' AND type='splash' ");
        string splash_image_url = db.ViziAppsExecuteScalar(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
        return splash_image_url;
    }
    public void SetApplicationSplashImage(Hashtable State, string application_id, string splash_image_url)
    {
        DeleteSplashImage(State, application_id);

        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("INSERT INTO branding_images SET ");
        b_sql.Append("branding_image_id='" + Guid.NewGuid().ToString() + "'");
        b_sql.Append(",application_id='" + application_id + "'");
        b_sql.Append(",width=320,height=460,type='splash'");
        b_sql.Append(",url='" + splash_image_url + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
    }
    public void DeleteSplashImage(Hashtable State, string application_id)
    {
        DB db = new DB();
        string sql = "DELETE FROM branding_images WHERE application_id='" + application_id + "' AND type='splash'";
        db.ViziAppsExecuteNonQuery(State, sql);
        db.CloseViziAppsDatabase(State);
    }
    public string GetApplicationDescription(Hashtable State,string application)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT description FROM applications ");
        b_sql.Append("WHERE application_name='" + application + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        string description = db.ViziAppsExecuteScalar(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
        return description;
    }
 
    public string GetTemplateApplicationDescription(Hashtable State, string application)
    {
        DB db = new DB();
        string template_account = null;
        if (State["Username"].ToString() == State["TemplatesAccount"].ToString())
        {
            template_account = State["DevAccount"].ToString();
            State["TemplatesAccount"] = template_account;
        }
        else
            template_account = State["TemplatesAccount"].ToString();

        string sql = "SELECT customer_id FROM customers WHERE username='" + template_account + "'";
        string customer_id = db.ViziAppsExecuteScalar(State, sql);

        StringBuilder b_sql = new StringBuilder("SELECT description FROM applications ");
        b_sql.Append("WHERE application_name='" + application + "'");
        b_sql.Append(" AND customer_id='" + customer_id + "'");
        string description = db.ViziAppsExecuteScalar(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
        return description;
    }
 
    public XmlDocument GetProductionAppXml(Hashtable State)
    {
        if (State["SelectedApp"] == null)
            return null;
        return GetProductionAppXml(State, State["SelectedApp"].ToString());
    }
    public XmlDocument GetStagingAppXml(Hashtable State)
    {
        if (State["SelectedApp"] == null)
            return null;
        return GetStagingAppXml(State, State["SelectedApp"].ToString());
    }
    public XmlDocument GetStagingAppXml(Hashtable State, string application_name)
    {
        XmlDocument doc = new XmlDocument();
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT staging_app_xml,application_type FROM applications ");
        b_sql.Append("WHERE application_name='" + application_name + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        DataRow[] rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
        if (rows.Length == 0)
        {
            State["AppXmlDoc"] = null;
            return null;
        } 
        DataRow row = rows[0];
        if (row["staging_app_xml"] == DBNull.Value || row["staging_app_xml"] == null)
        {
            State["AppXmlDoc"] = null;
            return null;
        }
        string xml = row["staging_app_xml"].ToString();
        doc.LoadXml(DecodeMySql(xml));

        //check for app type
        XmlNode application_node = doc.SelectSingleNode("//application");
        XmlNode type_node = application_node.SelectSingleNode("type");
        if (type_node == null)
        {
            XmlUtil x_util = new XmlUtil();
            x_util.CreateNode(doc, application_node, "type", row["application_type"].ToString());
        }
        else
            type_node.InnerText = row["application_type"].ToString();

        db.CloseViziAppsDatabase(State);
        State["AppXmlDoc"] = doc;

        return doc;
    }
    public XmlDocument GetProductionAppXml(Hashtable State, string application_name)
    {
        XmlDocument doc = new XmlDocument();
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT production_app_xml,application_type FROM applications ");
        b_sql.Append("WHERE application_name='" + application_name + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        DataRow[] rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
        DataRow row = rows[0];
        if (row["production_app_xml"] == DBNull.Value || row["production_app_xml"] == null)
        {
            State["AppXmlDoc"] = null;
            return null;
        }
        string xml = row["production_app_xml"].ToString();
        doc.LoadXml(DecodeMySql(xml));

        //check for app type
        XmlNode application_node = doc.SelectSingleNode("//application");
        XmlNode type_node = application_node.SelectSingleNode("type");
        if (type_node == null)
        {
            XmlUtil x_util = new XmlUtil();
            x_util.CreateNode(doc, application_node, "type", row["application_type"].ToString());
        }
        else
            type_node.InnerText = row["application_type"].ToString();

        db.CloseViziAppsDatabase(State);
        State["AppXmlDoc"] = doc;
        return doc;
    }
    public void CopyStagingDesignToProduction(Hashtable State)
    {
        XmlDocument doc = new XmlDocument();
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("UPDATE applications SET production_app_xml = staging_app_xml,");
        string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", ""); 
        b_sql.Append("production_date_time='" + NOW + "' ");
        b_sql.Append("WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);    
    }
    public string GetStagingAppTimeStamp(Hashtable State, string application_id)
    {
        XmlDocument doc = new XmlDocument();
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT date_time_modified FROM applications ");
        b_sql.Append("WHERE application_id='" + application_id + "'");
        string date_time_modified = db.ViziAppsExecuteScalar(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
        if (date_time_modified != null && date_time_modified.Length > 0)
        {
            DateTime modified = DateTime.Parse(date_time_modified);
            return modified.ToString("u").Replace("Z", "");
        }
        else
            return "";

    }
    public string GetProductionAppTimeStamp(Hashtable State, string application_id)
    {
        XmlDocument doc = new XmlDocument();
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT production_date_time FROM applications ");
        b_sql.Append("WHERE application_id='" + application_id + "'");
        string date_time_modified = db.ViziAppsExecuteScalar(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
        if (date_time_modified != null && date_time_modified.Length > 0)
        {
            DateTime modified = DateTime.Parse(date_time_modified);
            return modified.ToString("u").Replace("Z", "");
        }
        else
            return "";

    }
    public void UpdateStagingAppXml(Hashtable State)
    {
        UpdateStagingAppXml(State,State["SelectedApp"].ToString());
    }
    public void UpdateStagingAppXml(Hashtable State, string application_name)
    {
        try
        {
            string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", "");
            XmlDocument doc = (XmlDocument)State["AppXmlDoc"];
            DB db = new DB();
            StringBuilder b_sql = new StringBuilder("UPDATE applications SET ");
            if (State["SelectedAppType"] == null)
                 State["SelectedAppType"]= Constants.NATIVE_APP_TYPE;
            b_sql.Append("application_type='" + State["SelectedAppType"].ToString() + "',");
            b_sql.Append("staging_app_xml='" + MySqlFilter(doc.OuterXml) + "',");
            b_sql.Append("date_time_modified='" + NOW + "' ");
            b_sql.Append("WHERE application_name='" + application_name + "'");
            b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
            db.ViziAppsExecuteNonQuery(State, b_sql.ToString());
            db.CloseViziAppsDatabase(State);
        }
        catch(Exception ex)
        {
            throw new Exception("Error in UpdateStagingAppXml: " + ex.Message + ": " + ex.StackTrace);
        }
    }
    public void SaveAppPageImage(Hashtable State,string image_url)
    {
        string application_id = GetAppID(State);
         DB db = new DB();
        string sql = "SELECT application_page_id FROM application_pages WHERE application_id='" + application_id +
            "' AND page_name='" + State["SelectedAppPage"].ToString() + "'";
        string application_page_id = db.ViziAppsExecuteScalar(State, sql);
        string query_type = (application_page_id == null || application_page_id.Length == 0) ? "insert" : "update";

        string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", "");
        if (query_type == "insert")
        {
            sql = "INSERT INTO application_pages (application_page_id,application_id,page_name,page_image_url,date_time_modified) VALUES (UUID(),'" +
             application_id + "','" + 
             State["SelectedAppPage"].ToString() + "','" + 
             image_url + "','" + NOW + "')";
        }
        else
        {
            sql = "UPDATE application_pages SET page_image_url='" + image_url + 
                "',date_time_modified='" + NOW + 
                "' WHERE application_page_id='" + application_page_id + "'";
        }
        db.ViziAppsExecuteNonQuery(State, sql);
        db.CloseViziAppsDatabase(State);

    }
    public string GetAppPageImage(Hashtable State,string application_id, string page_name )
    {
        DB db = new DB();
        string sql = "SELECT page_image_url FROM application_pages WHERE application_id='" + application_id +
            "' AND page_name='" + page_name + "'";
        string page_image_url = db.ViziAppsExecuteScalar(State, sql);
        if (page_image_url == null || page_image_url.Length == 0)
            page_image_url = "../images/page_not_saved.jpg";
        else
            page_image_url += "?" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss"); //this avoids caching when the image url is updated

        db.CloseViziAppsDatabase(State);
        return page_image_url;
    }
    public byte[] ConvertBitmapToByteArray(Bitmap bitmap)
    {
        using (System.IO.MemoryStream sampleStream = new System.IO.MemoryStream())
        {
            //save to stream.
            bitmap.Save(sampleStream, System.Drawing.Imaging.ImageFormat.Bmp);
            //the byte array
            return sampleStream.ToArray();
        }
    }
    public void DeleteAppPageImage(Hashtable State, string page_name)
    {
        DB db = new DB();
        string application_id = GetAppID(State);

       //delete image file
        string sql = "SELECT page_image_url FROM application_pages WHERE application_id='" + application_id +
            "' AND page_name='" + page_name + "'";
        string page_image_url = db.ViziAppsExecuteScalar(State, sql);
        if (page_image_url != null && page_image_url.Length > 0 && page_image_url != "../images/page_not_saved.jpg")
        {
            HtmlToImage util = new HtmlToImage();
            util.DeleteImageFromUrl(State,page_image_url);
        }

        //delete DB entry
        sql = "DELETE FROM application_pages WHERE application_id='" + application_id + "' AND page_name='" + page_name + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        db.CloseViziAppsDatabase(State);
 
    }
    public string SetAppDescription(Hashtable State, string app_description)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("UPDATE applications SET description='" + app_description.Replace("'", "''").Replace(@"\", @"\\") + "' ");
        b_sql.Append("WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        string description = db.ViziAppsExecuteScalar(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
        return description;
    }
    public void SaveDatabaseSchema(Hashtable State, string DatabaseType, string DBConnectionString, Hashtable tables)
    {
        XmlUtil x_util = new XmlUtil();
        XmlDocument doc = GetStagingAppXml(State);
        XmlNode root = doc.SelectSingleNode("//mobiflex_project");
        XmlNode database_config = doc.SelectSingleNode("//mobiflex_project/database_config");
        if (database_config == null)
        {
            database_config = x_util.CreateNode(doc, root, "database_config");
        }
        XmlNode database_type = database_config.SelectSingleNode("database_type");
        if (database_type == null)
            database_type = x_util.CreateNode(doc, database_config, "database_type", DatabaseType);
        else
            database_type.InnerText = DatabaseType;

        XmlNode connection_string = database_config.SelectSingleNode("connection_string");
        if (connection_string == null)
            connection_string = x_util.CreateNode(doc, database_config, "connection_string", DBConnectionString);
        else
            connection_string.InnerText = DBConnectionString;

        XmlNode tables_node = database_config.SelectSingleNode("tables");
        if (tables_node != null)
            tables_node.RemoveAll();
        else
            tables_node = x_util.CreateNode(doc, database_config, "tables");

        foreach (string table_name in tables.Keys)
        {
            XmlNode table_node = x_util.CreateNode(doc, tables_node, "table");
            x_util.CreateNode(doc, table_node, "table_name", table_name);
            ArrayList field_list = (ArrayList)tables[table_name];
            XmlNode fields_node = x_util.CreateNode(doc, table_node, "fields");
            foreach (Hashtable field in field_list)
            {
                XmlNode field_node = x_util.CreateNode(doc, fields_node, "field");
                x_util.CreateNode(doc, field_node, "name", field["name"].ToString());
                if (field["type"] != null)
                    x_util.CreateNode(doc, field_node, "type", field["type"].ToString());
                if (field["length"] != null)
                    x_util.CreateNode(doc, field_node, "length", field["length"].ToString());
            }
        }

        State["AppXmlDoc"] = doc;
        UpdateStagingAppXml(State);
    }
    public void RemoveDatabaseInfo(Hashtable State)
    {
        XmlUtil x_util = new XmlUtil();
        XmlDocument doc = GetStagingAppXml(State);
        XmlNode root = doc.SelectSingleNode("//mobiflex_project");
        XmlNode database_config = doc.SelectSingleNode("//mobiflex_project/database_config");
        if (database_config != null)
        {
           root.RemoveChild(database_config);
        }
        UpdateStagingAppXml(State);
    }
    public bool DoesDatabaseInfoExist(Hashtable State)
    {
        XmlUtil x_util = new XmlUtil();
        XmlDocument doc = GetStagingAppXml(State);
        XmlNode root = doc.SelectSingleNode("//mobiflex_project");
        XmlNode database_config = doc.SelectSingleNode("//mobiflex_project/database_config");
        return (database_config != null) ? true : false;
    }
    public string GetCustomerIDFromEmail(Hashtable State, string email)
    {
        DB db = new DB();
        string sql = "SELECT customer_id,username FROM customers WHERE email='" + email + "'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        if (rows.Length > 0)
        {
            DataRow row = rows[0];
            string customer_id = row["customer_id"].ToString();
            State["CustomerID"] = row["customer_id"].ToString();
            State["Username"] = row["username"].ToString();
            db.CloseViziAppsDatabase(State);
            return customer_id;
        }
        else
            return null;
    }
    public string GetCustomerIDFromUsername(Hashtable State, string username)
    {
        DB db = new DB();
        string sql = "SELECT customer_id FROM customers WHERE username='" + username + "'";
        string customer_id = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return customer_id;
    }
    public string GetUsernameFromCustomerID(Hashtable State, string customer_id)
    {
        DB db = new DB();
        string sql = "SELECT username  FROM customers WHERE customer_id='" + customer_id + "'";
        string username = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return username;
    }
    public string GetCustomerStatus(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT status FROM customers WHERE customer_id='" + State["CustomerID"].ToString() + "'";
        string status = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return status;
    } 
    public void SetPaidService(Hashtable State, string confirm,
            string customer_id, string[] skus)
    {
        string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", "");
        string purchase_date = DateTime.Now.ToUniversalTime().ToString("d");

        DB db = new DB();
        int index = 0;
        foreach (string sku in skus)
        {
            StringBuilder b_sql = new StringBuilder("INSERT INTO paid_services SET ");
            b_sql.Append("purchase_date='" + purchase_date + "',");
            b_sql.Append("sku='" + sku + "',");
            b_sql.Append("confirmation='" + confirm + "',");
            string username = GetUsernameFromCustomerID(State, customer_id);
            b_sql.Append("username='" + username + "',");
            b_sql.Append("customer_id='" + customer_id + "',");
            b_sql.Append("purchase_date_time='" + NOW + "',");
            b_sql.Append("status='paid'");
            try
            {
                db.ViziAppsExecuteNonQuery(State, b_sql.ToString());
            }
            catch (Exception ex)
            {
                LogError(State, ex);

                if (!ex.Message.ToLower().Contains("duplicate"))
                    throw new Exception(ex.Message);
            }
            index++;
        }

        string sql = "UPDATE customers SET status='active' WHERE status!='active' AND customer_id='" + customer_id + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        db.CloseViziAppsDatabase(State);
    }
    public string GetServiceNameFromSku(Hashtable State, string sku)
    {
        DB db = new DB();
        string sql = "SELECT service FROM sku_list WHERE sku='" + sku + "'";
        string service = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return service;
    }
    public string GetSkuFromService(Hashtable State, string service)
    {
        DB db = new DB();
        string sql = "SELECT sku FROM sku_list WHERE service='" + service + "'";
        string sku = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return sku;
    }
    public ArrayList GetPaidServices(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT sku,app_name FROM paid_services WHERE customer_id='" + State["CustomerID"].ToString() + "' AND sku!='10' AND status='paid'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        db.CloseViziAppsDatabase(State);
        ArrayList PaidServices = new ArrayList();
        foreach (DataRow row in rows)
        {
            string[] values = new string[2];
            values[0] = GetServiceNameFromSku(State, row["sku"].ToString());
            values[1] = row["app_name"].ToString();
            PaidServices.Add(values);      
        }
        return PaidServices;
    }
    public void MapAppToProductionService(Hashtable State, string app_name, string sku)
    {
        DB db = new DB();
        String application_id = GetAppIDFromAppName(State, app_name);
        string sql = "UPDATE paid_services SET app_name='" + app_name + "', application_id='" + application_id +
            "' WHERE application_id IS NULL AND sku='" + sku + "' AND customer_id='" + State["CustomerID"].ToString() + "' AND status='paid' LIMIT 1";
        db.ViziAppsExecuteNonQuery(State, sql);

        sql = "SELECT status FROM applications WHERE application_name='" + app_name + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        string status = db.ViziAppsExecuteScalar(State, sql);
        
        if(!status.Contains("production"))
            status += "/production";

        string has_unlimited_users = "0";
        sql = "SELECT sku FROM paid_services WHERE app_name='" + app_name + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        if (rows.Length > 0)
        {
            foreach (DataRow row in rows)
            {
                sku = row["sku"].ToString();
                sql = "SELECT max_users FROM sku_list WHERE sku='" + sku + "'";
                string s_max_users = db.ViziAppsExecuteScalar(State, sql);
                db.CloseViziAppsDatabase(State);
                if (s_max_users != null && s_max_users.Length > 0)
                {
                    long n_users = Convert.ToInt64(s_max_users);
                    if (n_users > 1000)
                    {
                        has_unlimited_users = "1";
                        break;
                    }
                }
                
            }
        }
 
        long max_users = GetMaxUsers(State, app_name);
        sql = "UPDATE applications SET status='" + status + "'" +
        ",has_unlimited_users='" + has_unlimited_users + 
        "' WHERE application_name='" + app_name + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        db.CloseViziAppsDatabase(State);
    }
    public void RemoveAppFromProductionService(Hashtable State, string app_name,string sku)
    {
        DB db = new DB();
         string sql = "UPDATE paid_services SET app_name=NULL, application_id=NULL " +
            "WHERE app_name='" + app_name + "' AND sku='" + sku + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        sql = "SELECT status FROM applications WHERE application_name='" + app_name + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        string status = db.ViziAppsExecuteScalar(State, sql);

        status = status.Replace("/production", "");

        sql = "UPDATE applications SET status='" + status + "' WHERE application_name='" + app_name + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        db.CloseViziAppsDatabase(State);
    }
    public void CancelPaidService(Hashtable State, string purchase_date,string sku)
    {
        DB db = new DB();
        string sql = "SELECT app_name FROM paid_services WHERE sku='" + sku + "' AND purchase_date='" + purchase_date + "'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        if (rows.Length > 0)
        {
            DataRow row = rows[0];
            string app_name = row["app_name"].ToString();
            if (app_name != null && app_name.Length != 0)
                RemoveAppFromProductionService(State, app_name,sku);
        }

        string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", "");
        int day_of_month = DateTime.Parse(purchase_date).Day;
        string expiration = DateTime.Now.ToUniversalTime()
            .AddDays(-Convert.ToDouble(DateTime.Now.ToUniversalTime().Day))
            .AddMonths(1)
            .AddDays(Convert.ToDouble(day_of_month))
            .ToString("u").Replace("Z", "");
        StringBuilder b_sql = new StringBuilder("UPDATE paid_services SET ");
         b_sql.Append("cancellation_date_time='" + NOW + "', ");
         b_sql.Append("expiration_date_time='" + expiration + "', ");
         b_sql.Append("app_name='NULL', ");
         b_sql.Append("application_id='NULL', ");
         b_sql.Append("status='cancelled' ");
         b_sql.Append("WHERE sku='" + sku + "' ");
         b_sql.Append("AND purchase_date='" + purchase_date + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());
        db.CloseViziAppsDatabase(State);
    }
    public bool IsPaidProductionApp(Hashtable State, string app_name)
    {
        DB db = new DB();
        string sql = "SELECT status FROM applications WHERE application_name='" + app_name + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        string status = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return status.Contains("production") ? true : false;
    }
    public Hashtable IsProductionAppPaid(Hashtable State)
    {
        string application_id = GetAppIDFromAppName(State, State["SelectedApp"].ToString());
        return IsProductionAppPaid(State, application_id);
    }
    public Hashtable IsProductionAppPaid(Hashtable State,string application_id)
    {
        DB db = new DB();
        string sql = "SELECT status,sku FROM paid_services WHERE status='paid' AND sku != '" +
             State["iOSSubmitServiceSku"].ToString() + "' AND  sku != '" +
             State["AndroidSubmitServiceSku"].ToString() + "' AND application_id='" + application_id + "'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        if (rows.Length > 0)
        {
            DataRow row = rows[0];
            sql = "SELECT max_users,max_pages, allow_network_data FROM sku_list WHERE sku='" + row["sku"].ToString() + "'";
            rows = db.ViziAppsExecuteSql(State, sql);
            if (rows.Length > 0)
            {
                row = rows[0];
                Hashtable features = new Hashtable();
                if(row["max_users"] == null || row["max_users"] == DBNull.Value)
                    features["max_users"] = 0;
                else
                    features["max_users"] = Convert.ToInt32(row["max_users"].ToString());

                if (row["max_pages"] == null || row["max_pages"] == DBNull.Value)
                    features["max_pages"] = 0;
                else
                     features["max_pages"] = Convert.ToInt32(row["max_pages"].ToString());

                string allow_network_data = row["allow_network_data"].ToString();
                features["allow_network_data"] = (allow_network_data.ToLower() == "true" || allow_network_data == "1") ? true : false;
                db.CloseViziAppsDatabase(State);
                return features;
            }
            else
            {
                db.CloseViziAppsDatabase(State);
                return null;
            }
        }
        else
        {
            db.CloseViziAppsDatabase(State);
            return null;
        }
    }
    public bool IsFreeTrialDone(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT COUNT(*) FROM customers WHERE customer_id='" + State["CustomerID"].ToString() +
            "' AND n_logins>3 AND registration_date_time < DATE_SUB(CURDATE(),INTERVAL " + ConfigurationManager.AppSettings["DaysForFreeTrial"] + " DAY) ";
        string count = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        return (count == "1") ? true : false;
    }
    
    public long GetMaxUsers(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT sku FROM paid_services WHERE (sku!='" + State["iOSSubmitServiceSku"].ToString() +
            "' AND sku!='" + State["AndroidSubmitServiceSku"].ToString() + "') AND status='paid' AND app_name='" + 
            State["SelectedApp"].ToString() + 
            "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        if (rows.Length == 0)
            return 0;
        foreach (DataRow row in rows)
        {
            string sku = row["sku"].ToString();
            sql = "SELECT max_users FROM sku_list WHERE sku='" + sku + "'";
            string s_max_users = db.ViziAppsExecuteScalar(State, sql);
            db.CloseViziAppsDatabase(State);
            if (s_max_users != null && s_max_users.Length > 0)
                return Convert.ToInt64(s_max_users);
            else
                return 0;
        }
        db.CloseViziAppsDatabase(State);
        return 0;
    }
    public long GetMaxPages(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT sku FROM paid_services WHERE status='paid' AND app_name='" + State["SelectedApp"].ToString() + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
        if (rows.Length == 0)
            return 0;
        foreach (DataRow row in rows)
        {
            string sku = row["sku"].ToString();
            sql = "SELECT max_pages FROM sku_list WHERE sku='" + sku + "'";
            string s_max_pages = db.ViziAppsExecuteScalar(State, sql);
            db.CloseViziAppsDatabase(State);
            if (s_max_pages != null && s_max_pages.Length > 0)
                return Convert.ToInt64(s_max_pages);
            else
                return 0;
        }
        db.CloseViziAppsDatabase(State);
        return 0;
    }
    public long GetMaxUsers(Hashtable State, string sku)
    {
        DB db = new DB();
        string sql = "SELECT max_users FROM sku_list WHERE sku='" + sku + "'";
        string s_max_users = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        if (s_max_users != null)
            return Convert.ToInt64(s_max_users);
        else
            return 0;
    }
    public long GetMaxPages(Hashtable State, string sku)
    {
        DB db = new DB();
        string sql = "SELECT max_pages FROM sku_list WHERE sku='" + sku + "'";
        string s_max_pages = db.ViziAppsExecuteScalar(State, sql);
        db.CloseViziAppsDatabase(State);
        if (s_max_pages != null)
            return Convert.ToInt64(s_max_pages);
        else
            return 0;
    }
   
    public void AddDatabaseValidations(Hashtable State,string table_name, ArrayList UserDataTable)
    {
        XmlDocument doc = (XmlDocument)State["AppXmlDoc"];
        XmlNode data_validation_list = doc.SelectSingleNode("//data_validation_list");
        XmlUtil x_util = new XmlUtil();
        XmlNode table_validation = x_util.CreateNode(doc, data_validation_list, "table_validation");
        x_util.CreateNode(doc, table_validation, "table_name", table_name);
        XmlNode fields = x_util.CreateNode(doc, table_validation, "fields");
        

        foreach (object o in UserDataTable)
        {
            string[] set = (string[])o;
            string type = null;
            switch (set[1])
            {
                case "string":
                case "email":
                case "currency":
                case "US_phone":
                case "International_phone":
                case "zipcode":
                case "social_security_number":
                case "credit_card_number":
                case "URL":
                    type = "varchar (255)";
                    break;
                case "text":
                    type = "text";
                    break;
                case "date":
                    type = "date";
                    break;
                case "datetime":
                    type = "datetime";
                    break;
                case "time":
                    type = "time";
                    break;
                case "image":
                    type = "mediumblob";
                    break;
                case "number":
                    type = "float";
                    break;
            }
            x_util.CreateNode(doc, fields, "field",set[0].ToLower() );
            x_util.CreateNode(doc, fields, "type", type);
        }
        UpdateStagingAppXml(State);
    }
    public void RenameApplication(Hashtable State, string prev_app_name, string new_app_name)
    {
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("UPDATE applications SET ");
        b_sql.Append("application_name='" + new_app_name + "' ");
        b_sql.Append("WHERE customer_id='" + State["CustomerID"] + "' AND ");
        b_sql.Append("application_name='" + prev_app_name + "'");
        db.ViziAppsExecuteNonQuery(State, b_sql.ToString());

        //change the name in the XML
        XmlUtil x_util = new XmlUtil();
        x_util.RenameApp(State,new_app_name);

         db.CloseViziAppsDatabase(State);
     }
    public void DeleteApplication(Hashtable State)
    {
        string application_name = State["SelectedApp"].ToString();

        DB db = new DB();
        string sql = "SELECT application_id FROM applications WHERE application_name='" + application_name + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        string application_id = db.ViziAppsExecuteScalar(State, sql);

        sql = "DELETE FROM applications WHERE application_id='" + application_id + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        sql = "DELETE FROM application_pages WHERE application_id='" + application_id + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        sql = "DELETE FROM branding_images WHERE application_id='" + application_id + "'";
        db.ViziAppsExecuteNonQuery(State, sql);

        db.CloseViziAppsDatabase(State);

        if (State["SelectedAppType"] != null && (State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE || State["SelectedAppType"].ToString() == Constants.HYBRID_APP_TYPE) && 
            State["UrlAccountIdentifier"] != null)
        {
            AmazonS3 s3 = new AmazonS3();
            string Bucket = ConfigurationManager.AppSettings["WebAppBucket"];
            string file_name = State["SelectedApp"].ToString().Replace(" ", "_") + Constants.WEB_APP_TEST_SUFFIX + "/index.html"; 
            string key = State["UrlAccountIdentifier"].ToString() + "/" + file_name; 
            s3.DeleteS3Object(Bucket,key);

            file_name = State["SelectedApp"].ToString().Replace(" ", "_") + "/index.html";
            key = State["UrlAccountIdentifier"].ToString() + "/" + file_name;
            if (s3.S3ObjectExists(Bucket, key))
                s3.DeleteS3Object(Bucket, key);
        }
    }

    public string GetUniqueTimeID()
    {
        return DateTime.Now.ToUniversalTime().Ticks.ToString().Remove(17,1).Remove(0,1); //accurate to 1 microsec
    }
    public string HTTPUploadFile(string url, string local_file_path)
    {
        string xml = null;

        try
        {
            WebClient Client = new WebClient();
            Client.UploadFile(url, local_file_path);
        }
        catch (Exception ex)
        {
            string message = ex.Message;
            return message;
        }
        return xml;
    }
 
    public string GetApplicationStatus(Hashtable State,string application_name)
    {
        DB db = new DB();
        string sql = "SELECT status FROM applications WHERE application_name='" + application_name + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        string status = db.ViziAppsExecuteScalar(State,sql);
        return status;
    }
    public void LogSQLError(Hashtable State, Exception ex,string sql_used)
    {
        try
        {
            string username = "";
            if (State["Username"] != null)
                username = State["Username"].ToString();
            else
                username = "username_not_set";

            string err = "ViziApps error: username: " + username + "; exception: " + ex.Message + "; stack trace: " + ex.StackTrace + "; sql: " + sql_used;
            LogWindowsEvent(err);
            if (username != "username_not_set")
            {
                DB db = new DB();
                string error = MySqlFilter(ex.Message + "; sql: " + sql_used);
                string stacktrace = MySqlFilter(ex.StackTrace);

                TimeZones tz = new TimeZones();
                string NOW = tz.GetCurrentDateTimeMySqlFormat(State);

                string app = "No app selected";
                if (State["SelectedApp"] != null)
                    app = State["SelectedApp"].ToString();
 
                string sql = "INSERT INTO error_log SET log_id=UUID(), timestamp='" + NOW + "',username='" +
                    username + "',app='" + app + "',error='" + error + "',stacktrace='" + stacktrace + "'";
                db.ViziAppsExecuteNonQuery(State, sql);
                db.CloseViziAppsDatabase(State);
            }
        }
        catch { }
    }

    public void LogError(Hashtable State, Exception ex)
    {
        try
        {
            string username = "";
            if (State["Username"] != null)
                username = State["Username"].ToString();
            else
                username = "username_not_set";

            string err = "ViziApps error: username: " + username + "; exception: " + ex.Message + "; stack trace: " + ex.StackTrace;
            LogWindowsEvent(err);
            if (username != "username_not_set")
            {
                DB db = new DB();
                string error = MySqlFilter(ex.Message);
                string stacktrace =MySqlFilter(ex.StackTrace);

                TimeZones tz = new TimeZones();
                string NOW = tz.GetCurrentDateTimeMySqlFormat(State);

                string app = "No app selected";
                if (State["SelectedApp"] != null)
                    app = State["SelectedApp"].ToString();

                string sql = "INSERT INTO error_log SET log_id=UUID(), timestamp='" + NOW + "',username='" +
                    username + "',app='" + app + "',error='" + error + "',stacktrace='" + stacktrace + "'";
                db.ViziAppsExecuteNonQuery(State, sql);
                db.CloseViziAppsDatabase(State);
            }
        }
        catch{}
    }
    public void StartSessionLog(Hashtable State)
    {
        DB db = new DB();
        //does State already exist?
        string sql = "SELECT COUNT(*) FROM sessions WHERE session_id='" + State["SessionID"].ToString() + "'";
        string count = db.ViziAppsExecuteScalar(State, sql);
        if (count != "0")
        {
            UpdateSessionLog(State, "login", "Default");
            return;
        }

        string timestamp = DateTime.Now.ToString("u").Replace("Z", "");
        string trace = "time: " + timestamp + ", type: login, page: Default";
        sql = "INSERT INTO sessions SET " +
             "session_id='" + State["SessionID"].ToString() + "'," +
             "username='" + State["Username"].ToString() + "'," +
             "first_session_date_time='" + timestamp + "'," +
             "last_session_date_time='" + timestamp + "'," +
             "session_duration='0'," +
             "trace='" + trace + "'";
        try
        {
            db.ViziAppsExecuteNonQuery(State, sql);
        }
        catch (Exception ex) { }; //exception for duplicate State ids on login in debug
        db.CloseViziAppsDatabase(State);
    }
    public void UpdateSessionLog(Hashtable State,string type, string page)
    {
        try
        {
            DB db = new DB();
            string sql = "SELECT first_session_date_time,last_session_date_time FROM sessions WHERE session_id='" + State["SessionID"].ToString() + "'";
            DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
            if (rows.Length == 0)
                return; // call is before login
            DataRow row = rows[0];
            string first_session_date_time = row["first_session_date_time"].ToString();
            if (first_session_date_time == null)
                return; // call is before login

            if (type.Contains("timeout:"))
            {
                sql = "UPDATE sessions SET n_timeouts=n_timeouts+1 WHERE session_id='" + State["SessionID"].ToString() + "'";
                db.ViziAppsExecuteNonQuery(State, sql);
            }
            string last_session_date_time = row["last_session_date_time"].ToString();
            DateTime first = DateTime.Parse(first_session_date_time);
            DateTime last = DateTime.Parse(last_session_date_time);
            DateTime now = DateTime.Now;
            string timestamp = now.ToString("u").Replace("Z", "");
            TimeSpan total_duration = now - first;
            TimeSpan step_duration = now - last;
            string trace = "; time: " + timestamp + ", step_duration: " + step_duration.TotalMinutes.ToString() + ", type: " + type + ", page: " + page;
            sql = "UPDATE sessions SET " +
                "last_session_date_time='" + timestamp + "'," +
                "session_duration='" + total_duration.TotalMinutes.ToString() + "'," +
                "trace=concat(trace,'" + trace + "') WHERE session_id='" + State["SessionID"].ToString() + "'";
            db.ViziAppsExecuteNonQuery(State, sql);
            db.CloseViziAppsDatabase(State);
        }
        catch { } //if there is an error keep going
    }
    public void LogSessionTimeOut(Hashtable State,string page)
    {
        if (State == null)
        {
            throw new Exception("Timeout with no State or Session");
        }
        StackTrace stackTrace = new StackTrace();           // get call stack
        StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)

        // write call stack method names
        StringBuilder stack = new StringBuilder();
        bool isFirst = true;
        foreach (StackFrame stackFrame in stackFrames)
        {
            if (isFirst)
                isFirst = false;
            else
                stack.Append("<-");
            stack.Append(stackFrame.GetMethod().Name);   // write method name
        }

        StringBuilder values = new StringBuilder();
        if (State.Keys.Count > 0)
        {
            isFirst = true;
            foreach (string key in State.Keys)
            {
                if (isFirst)
                    isFirst = false;
                else
                    values.Append(", ");
                if( State[key] != null)
                    values.Append(key + "=" + State[key].ToString());
                else
                    values.Append(key + "=null");
            }
        }
        string info = "cache values: " + values.ToString();

        string trace = "timeout: " + info + ", stack: " + stack.ToString();
        UpdateSessionLog(State, trace, page);
    }
    public void LogWindowsEvent(string event_message)
    {
        try
        {
            // Create the source, if it does not already exist.
            if (!EventLog.SourceExists("ViziApps"))
            {
                EventLog.CreateEventSource("ViziApps", "Application");
            }

            // Create an EventLog instance and assign its source.
            EventLog myLog = new EventLog();
            myLog.Source = "ViziApps";

            // Write an informational entry to the event log.   
            if (event_message.Length < 32701)
                myLog.WriteEntry(event_message, EventLogEntryType.Error);
            else
                myLog.WriteEntry(event_message.Remove(32700), EventLogEntryType.Error);
        }
        catch
        {
            //on server 2008 requires admin rights
        }
    }
 
    public void AddEmailToButton(Button button, string to_email,string type)
    {
        string popupURL = "EmailForm.aspx?email=" + HttpUtility.UrlEncode(to_email) + "&type=" + HttpUtility.UrlEncode(type);

        button.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
            popupURL,  700, 600));
    }
    public void AddEmailToButton(ImageButton button, string to_email, string type)
    {
        string popupURL = "EmailForm.aspx?email=" + HttpUtility.UrlEncode(to_email) + "&type=" + HttpUtility.UrlEncode(type);

        button.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
            popupURL, 700, 600));
    }
    public void Logout(Hashtable State)
    {
        UpdateSessionLog(State, "logout","Util");
        Hashtable UsersList = (Hashtable)HttpRuntime.Cache["UsersList"];
        if (State != null && State["Username"] != null)
        {
            UsersList.Remove(State["Username"].ToString());
            State["Username"] = null;
            State["CustomerID"] = null;
        }
    }
    public bool CheckSessionTimeout(Hashtable State, HttpResponse Response,string URL)
    {
        if (State == null || State.Count <= 2)
        {
            if (State != null)
                State["PreviousError"] = "Your session has timed out.";
            Response.Redirect(URL, false);
            return true;
        }
        return false;
    }

    public void ProcessMainExceptions(Hashtable State, HttpResponse Response, Exception ex)
    {
        if (State != null)
        {
            string error = ex.Message + "- " + ex.StackTrace;
            try
            {
                UpdateSessionLog(State, "error: " + error, "Util");
            }
            catch { }//if there is an error in the DB keep going
            LogError(State, ex);
            Logout(State);
        }
        Response.Redirect("Default.aspx", false);
    }
    public void CheckDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            try
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not create directory: " + path);
            }
        }
    }
    public void RemoveApplicationProjectAndDatabases(string application_name)
    {
            string DatabaseUsername = ConfigurationManager.AppSettings["DatabaseUsername"];
            string DatabasePassword = ConfigurationManager.AppSettings["DatabasePassword"];

            //remove app database if linked
            DB db = new DB();
            db.MySqlDropDatabase(application_name, DatabaseUsername, DatabasePassword); 
    }
    public string Encrypt(string input)
    {
        StringBuilder output = new StringBuilder();
        foreach (char c in input)
        {
            output.Append(Convert.ToChar(c + 10));
        }
        return output.ToString();
    }
    public string Decrypt( string input)
    {
        StringBuilder output = new StringBuilder();
        foreach (char c in input)
        {
            output.Append(Convert.ToChar(c - 10));
        }
        return output.ToString();
    }

    // C# to convert a string to a byte array.
    public byte[] StringToByteArray(string str)
    {
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        return encoding.GetBytes(str);
    }
    public string ByteArrayToString(byte[] bytes)
    {
        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        return enc.GetString(bytes);
    }
    public string CombineStringArray(string[] array)
    {
        StringBuilder CallListValues = new StringBuilder();
        foreach (string val in array)
        {
            if(val==null)
                CallListValues.Append("|");
            else
                CallListValues.Append(val + "|");
        }
        if (CallListValues.Length > 0)
            CallListValues.Remove(CallListValues.Length - 1, 1); //remove last '|'
        return CallListValues.ToString();
    }
    public string CombineStringArraySemicolon(string[] array)
    {
        StringBuilder CallListValues = new StringBuilder();
        foreach (string val in array)
        {
            if (val == null)
                CallListValues.Append(";");
            else
                CallListValues.Append(val + ";");
        }
        if (CallListValues.Length > 0)
            CallListValues.Remove(CallListValues.Length - 1, 1); //remove last '|'
        return CallListValues.ToString();
    }

    public string CombineStringArrays(string[,] array)
    {
        StringBuilder CallListValues = new StringBuilder();
        for (int row = 0; row < array.GetLength(0); row++)
        {
            for (int col = 0; col < array.GetLength(1); col++)
            {
                if(array[row, col]==null)
                    CallListValues.Append("|");
                else
                    CallListValues.Append(array[row, col] + "|");
            }
            if (CallListValues.Length > 0)
                CallListValues.Remove(CallListValues.Length - 1, 1); //remove last '|'
            CallListValues.Append("~");
        }
        if (CallListValues.Length > 0)
            CallListValues.Remove(CallListValues.Length - 1, 1); //remove last '~'
        return CallListValues.ToString();
    }
    public string CombineIntArray(int[] array)
    {
        StringBuilder CallListValues = new StringBuilder();
        foreach (int val in array)
        {
            CallListValues.Append(val.ToString() + "|");
        }
        if (CallListValues.Length > 0)
            CallListValues.Remove(CallListValues.Length - 1, 1); //remove last '|'
        return CallListValues.ToString();
    }


     public void CopyDropDownList(DropDownList source, DropDownList target)
    {
        target.Items.Clear();
        foreach (ListItem item in source.Items)
        {
            target.Items.Add(item.Value);
        }
        target.SelectedIndex = source.SelectedIndex;
    }

    // KRIS. 03/24/2008. CHANGE START
    /// <summary>
    /// This is a helper method to call a webservice without having to create a WebReference. This uses util.HTTPPost to post
    /// the webservice inputs to a URL, which is the Webservice URL.
    /// </summary>
    /// <param name="strWebserviceURL">Eg : http://localhost/Webservice.asmx/Webmethod</param>
    /// <param name="strInputs">Eg : XmlInputs=<input>test</input></param>
    /// <returns></returns>
    public XmlDocument CallWebservice(string strWebserviceURL, string method, string strInputs)
    {
        Util util = new Util();
        XmlDocument doc = new XmlDocument();
        string response = util.HTTPPost(strWebserviceURL+"/" + method, strInputs);
        doc.LoadXml(response);
        XmlNode root = doc.FirstChild.NextSibling;
        if (root.Name == "string")
        {
            string response2 = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"  + root.InnerText;
            doc = new XmlDocument();
            doc.LoadXml(response2);
        }
        return doc;
    }
    // KRIS. 03/24/2008. CHANGE END

    public void PredefinedRange_SelectedIndexChanged(Hashtable State,string range, string start_date_time, string end_date_time)
    {
        double time_zone_delta_hours = Convert.ToDouble(State["TimeZoneDeltaHours"].ToString());
        DateTime now = DateTime.Now.ToUniversalTime().AddHours(time_zone_delta_hours);
        DateTime Start = DateTime.Now.ToUniversalTime();
        DateTime End = DateTime.Now.ToUniversalTime();

        if (range.StartsWith("Custom"))
        {

            if (!DateTime.TryParse(start_date_time, out Start))
            {
                return;
            }
            else //convert to universal time
            {
                Start = Start.AddHours(-time_zone_delta_hours);
            }
            if (!DateTime.TryParse(end_date_time, out End))
            {
                return;
            }
            else //convert to universal time
            {
                End = End.AddHours(-time_zone_delta_hours);
            }
        }
        else if (range == "This Hour")
        {
            string this_hour = now.ToString("yyyy-MM-dd HH:00:00");
            Start = DateTime.Parse(this_hour);
            End = now;
        }
        else if (range == "Last Hour")
        {
            string last_hour = now.AddHours(-1.0).ToString("yyyy-MM-dd HH:00:00");
            Start = DateTime.Parse(last_hour);
            End = Start.AddHours(1.0).AddMinutes(-1.0);
        }
        else if (range == "Today")
        {
            string this_day = now.ToString("yyyy-MM-dd 00:00:00");
            Start = DateTime.Parse(this_day);
            End = now;
        }
        else if (range == "Yesterday")
        {
            string yesterday = now.AddDays(-1.0).ToString("yyyy-MM-dd 00:00:00");
            Start = DateTime.Parse(yesterday);
            End = Start.AddDays(1.0).AddMinutes(-1.0);
        }
        else if (range == "Week to Date")
        {
            string yesterday = now.AddDays(-Convert.ToDouble(now.DayOfWeek)).ToString("yyyy-MM-dd 00:00:00");
            Start = DateTime.Parse(yesterday);
            End = now;
        }
        else if (range == "Last Week")
        {
            string last_week = now.AddDays(-7.0).AddDays(-Convert.ToDouble(now.DayOfWeek)).ToString("yyyy-MM-dd 00:00:00");
            Start = DateTime.Parse(last_week);
            End = Start.AddDays(7.0).AddMinutes(-1.0);
        }
        else if (range == "Month to Date")
        {
            string this_month = now.ToString("yyyy-MM-01 00:00:00");
            Start = DateTime.Parse(this_month);
            End = now;
        }
        else if (range == "Last Month")
        {
            string last_month = now.AddMonths(-1).ToString("yyyy-MM-01 00:00:00");
            Start = DateTime.Parse(last_month);
            End = Start.AddMonths(1).AddMinutes(-1.0);
        }
        else if (range == "Quarter to Date")
        {
            int this_quarter = (now.Month - 1) / 3;
            int month = this_quarter * 3 + 1;
            string s_month = month.ToString();
            if (s_month.Length == 1)
                s_month = "0" + s_month;
            string s_quarter = now.ToString("yyyy-" + s_month + "-01 00:00:00");
            Start = DateTime.Parse(s_quarter);
            End = now;
        }
        else if (range == "Last Quarter")
        {
            int last_quarter = (now.AddMonths(-3).Month - 1) / 3;
            int lq_month = last_quarter * 3 + 1;
            string lqs_month = lq_month.ToString();
            if (lqs_month.Length == 1)
                lqs_month = "0" + lqs_month;
            string lss_quarter = now.ToString(now.AddMonths(-3).Year.ToString() + "-" + lqs_month + "-01 00:00:00");
            Start = DateTime.Parse(lss_quarter);
            End = Start.AddMonths(3).AddMinutes(-1.0);
        }
        else if (range == "Year to Date")
        {
            string this_year = now.ToString("yyyy-01-01 00:00:00");
            Start = DateTime.Parse(this_year);
            End = now;
        }
        else if (range == "Last Year")
        {
            string this_year = now.AddYears(-1).ToString("yyyy-01-01 00:00:00");
            Start = DateTime.Parse(this_year);
            End = Start.AddYears(1).AddMinutes(-1.0);
        }
        State["report_start_time"] = Start;
        State["report_end_time"] = End;
    }
    public RadTreeNode CreateFieldNode(RadTreeNode PageRoot, string field, string field_type)
    {
        RadTreeNode id = new RadTreeNode(field);
        id.CssClass = "RadTreeView";

        switch (field_type)
        {
            case "alert":
                id.ImageUrl = "~/images/editor_images/alert_icon.png";
                break;
            case "switch":
                id.ImageUrl = "~/images/editor_images/switch_icon.png";
                break;
            case "audio_recorder":
                id.ImageUrl = "~/images/editor_images/audio_recorder_icon.png";
                break;
            case "gps_field":
            case "gps":
                id.ImageUrl = "~/images/editor_images/gps_icon.jpg";
                break;
            case "hidden_field":
                id.ImageUrl = "~/images/editor_images/hidden_field_icon.jpg";
                break;
            case "photo":
                id.ImageUrl = "~/images/editor_images/photo.jpg";
                break;
            case "speech_reco":
                id.ImageUrl = "~/images/editor_images/speech_recognition_icon.gif";
                break;
            case "slider":
                id.ImageUrl = "~/images/editor_images/slider.png";
                break;
            case "text_field":
                id.ImageUrl = "~/images/editor_images/textfield_icon.png";
                break;
            case "text_area":
                id.ImageUrl = "~/images/editor_images/textarea_icon.png";
                break;
            case "table_field":
            case "table":
                id.ImageUrl = "~/images/editor_images/tableview.png";
                break;
            case "web_view":
                id.ImageUrl = "~/images/editor_images/browser.png";
                break;
            case "button":
                id.ImageUrl = "~/images/editor_images/button.png";
                break;
            case "select":
                id.ImageUrl = "~/images/editor_images/select.png";
                break;
            case "picker_field":
            case "picker":
                id.ImageUrl = "~/images/editor_images/picker_view_icon.png";
                break;
            case "label":
                id.ImageUrl = "~/images/editor_images/label.gif";
                break;
            case "image_button":
                id.ImageUrl = "~/images/editor_images/image_button.png";
                break;
            case "image":
                id.ImageUrl = "~/images/editor_images/image_icon.png";
                break;
            case "audio":
                id.ImageUrl = "~/images/editor_images/audio_icon.png";
                break;
            default:
                id.ImageUrl = "~/images/MCM_application.gif";
                break;

        }
        //id.Style = "background-color:#99ffb9;";
        //id.BackColor = Color.FromArgb(153, 255, 185); //LIGHT GREEN
        id.PostBack = false;
        PageRoot.Nodes.Add(id);

        return id;

    }
    public string CapitalizeWords(string value)
    {
        if (value == null)
            throw new ArgumentNullException("value");
        if (value.Length == 0)
            return value;

        StringBuilder sb = new StringBuilder(value.Length);
        // Upper the first char.
        sb.Append(char.ToUpper(value[0]));
        for (int i = 1; i < value.Length; i++)
        {
            // Get the current char.
            char c = value[i];

            // Upper if after a space.
            if (char.IsWhiteSpace(value[i - 1]))
                c = char.ToUpper(c);
            else
                c = char.ToLower(c);

            sb.Append(c);
        }

        return sb.ToString();
    }
  
    public string GetDefaultButton(Hashtable State)
    {
        DB db = new DB();
        string sql = "SELECT default_button_image FROM applications WHERE application_name='" + State["SelectedApp"].ToString() +  "' AND customer_id='" + State["CustomerID"].ToString() + "'";
        string url = db.ViziAppsExecuteScalar(State, sql);
        if (url == null || url.Length == 0)
        {
            if (State["SelectedAppType"] != null && (State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE || State["SelectedAppType"].ToString() == Constants.HYBRID_APP_TYPE))
                url = ConfigurationManager.AppSettings["DefaultWebAppButtonImage"];
            else
                url = ConfigurationManager.AppSettings["DefaultButtonImage"];
        }
        db.CloseViziAppsDatabase(State);
        return url;
    }
    public void SetDefaultBackgroundForView(Hashtable State,string device_view)
    {
        switch (device_view)
        {
            default:
            case Constants.IPHONE:
                State["BackgroundImageUrl"] = "https://s3.amazonaws.com/MobiFlexImages/apps/images/backgrounds/standard_w_header_iphone.jpg";
                break;
            case Constants.IPAD:
                State["BackgroundImageUrl"] = "https://s3.amazonaws.com/MobiFlexImages/apps/images/backgrounds/standard_w_header_ipad.jpg";
                break;
            case Constants.ANDROID_PHONE:
                State["BackgroundImageUrl"] = "https://s3.amazonaws.com/MobiFlexImages/apps/images/backgrounds/standard_w_header_android.jpg";
                break;
            case Constants.ANDROID_TABLET:
                State["BackgroundImageUrl"] = "https://s3.amazonaws.com/MobiFlexImages/apps/images/backgrounds/standard_w_header_android_tablet.jpg";
                break;
        }
        State["BackgroundHtml"] = "<img id=\"background_image\" src=\"" + State["BackgroundImageUrl"].ToString() + "\" style=\"position:absolute;top:0px;left:0px;\"/>";
    }
    public void SetDefaultButton(Hashtable State,string default_button_url)
    {
        if (State == null || State["SelectedApp"] == null || State["CustomerID"] == null)
            return;
        try
        {
            DB db = new DB();
            if (default_button_url == null || default_button_url.Length == 0)
            {
                if (State["SelectedAppType"] != null && (State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE || State["SelectedAppType"].ToString() == Constants.HYBRID_APP_TYPE))
                    default_button_url = ConfigurationManager.AppSettings["DefaultWebAppButtonImage"];
                else
                    default_button_url = ConfigurationManager.AppSettings["DefaultButtonImage"];
            }
            string sql = "UPDATE applications SET default_button_image='" + default_button_url + "' WHERE application_name='" + State["SelectedApp"].ToString() + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
            db.ViziAppsExecuteNonQuery(State, sql);
            db.CloseViziAppsDatabase(State);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
    public void SavePageImage(Hashtable State, string page_name, string html)
    {
        //save half size image of page
        HtmlToImage thumb = new HtmlToImage();
        if (State["SelectedDeviceType"].ToString() == Constants.IPAD ||
            State["SelectedDeviceType"].ToString() == Constants.ANDROID_TABLET)
        {
            if (State["BackgroundColor"] == null)
                State["BackgroundColor"] = "#cccccc";

            string background_color_div_prefix = "<div style=\"width:" + Constants.IPAD_SPLASH_PORTRAIT_WIDTH_S + "px;height:" + Constants.IPAD_SPLASH_PORTRAIT_HEIGHT_S + "px;vertical-align:top;background-color:" + State["BackgroundColor"].ToString() + "\" >";
            if (State["SelectedDeviceType"].ToString() == Constants.ANDROID_TABLET)
                background_color_div_prefix = "<div style=\"width:" + Constants.ANDROID_TABLET_PORTRAIT_WIDTH_S + "px;height:" + Constants.ANDROID_TABLET_SPLASH_PORTRAIT_HEIGHT_S + "px;vertical-align:top;background-color:" + State["BackgroundColor"].ToString() + "\" >";
            string background_color_div_suffix = "</div>";
            html = background_color_div_prefix + html + background_color_div_suffix;
        }
        string page_image_url = thumb.ConvertToImageLink(State, page_name, html);
        if (page_image_url != null)
        {
            SaveAppPageImage(State, page_image_url);
        }
    }
    public Size GetImageSize(string url)
    {
        System.Drawing.Image img = DownloadImage(url);
        Bitmap bitmap = new Bitmap(img);
        return bitmap.Size;
    }
    /// <summary>
    /// Function to download Image from website
    /// </summary>
    /// <param name="_URL">URL address to download image</param>
    /// <returns>Image</returns>
    public System.Drawing.Image DownloadImage(string _URL)
    {
        System.Drawing.Image _tmpImage = null;

        try
        {
            // Open a connection
            System.Net.HttpWebRequest _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);

            _HttpWebRequest.AllowWriteStreamBuffering = true;

            // You can also specify additional header values like the user agent or the referer: (Optional)
            _HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
            _HttpWebRequest.Referer = "http://www.google.com/";

            // set timeout for 20 seconds (Optional)
            _HttpWebRequest.Timeout = 20000;

            // Request response:
            System.Net.WebResponse _WebResponse = _HttpWebRequest.GetResponse();

            // Open data stream:
            System.IO.Stream _WebStream = _WebResponse.GetResponseStream();

            // convert webstream to image
            _tmpImage = System.Drawing.Image.FromStream(_WebStream);

            // Cleanup
            _WebResponse.Close();
            _WebResponse.Close();
        }
        catch (Exception _Exception)
        {
            // Error
            Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            return null;
        }

        return _tmpImage;
    }
    public void ResetAppStateVariables(Hashtable State)
    {
        State["SelectedApp"] = null;
        State["SelectedAppPage"] = null;
        State["BackgroundImageUrl"] = null;
        State["AppXmlDoc"] = null;
        State["SelectedDeviceType"] = null;
    }
    public void DeleteOldTempFiles(Hashtable State)
    {
        string[] files = Directory.GetFiles(State["TempFilesPath"].ToString());
        DateTime now = DateTime.UtcNow;
        foreach (string file in files)
        {
            if (file.Contains("folder_placeholder.txt")) //place holder to prevent folder from being deleted
                continue;
            FileInfo fileInfo = new FileInfo(file);
            TimeSpan age = now - fileInfo.LastWriteTimeUtc;
            if (age.TotalMinutes > 5.0D)
                File.Delete(file);
        }
    }
    public static string Encrypt(string clearText, string Password)
    {
        // First we need to turn the input string into a byte array. 
        byte[] clearBytes =
          System.Text.Encoding.Unicode.GetBytes(clearText);

        // Then, we need to turn the password into Key and IV 
        // We are using salt to make it harder to guess our key
        // using a dictionary attack - 
        // trying to guess a password by enumerating all possible words. 
        PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
            new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

        // Now get the key/IV and do the encryption using the
        // function that accepts byte arrays. 
        // Using PasswordDeriveBytes object we are first getting
        // 32 bytes for the Key 
        // (the default Rijndael key length is 256bit = 32bytes)
        // and then 16 bytes for the IV. 
        // IV should always be the block size, which is by default
        // 16 bytes (128 bit) for Rijndael. 
        // If you are using DES/TripleDES/RC2 the block size is
        // 8 bytes and so should be the IV size. 
        // You can also read KeySize/BlockSize properties off
        // the algorithm to find out the sizes. 
        byte[] encryptedData = Encrypt(clearBytes,
                 pdb.GetBytes(32), pdb.GetBytes(16));

        // Now we need to turn the resulting byte array into a string. 
        // A common mistake would be to use an Encoding class for that.
        //It does not work because not all byte values can be
        // represented by characters. 
        // We are going to be using Base64 encoding that is designed
        //exactly for what we are trying to do. 
        return Convert.ToBase64String(encryptedData);

    }
    public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
    {
        // Create a MemoryStream to accept the encrypted bytes 
        MemoryStream ms = new MemoryStream();

        // Create a symmetric algorithm. 
        // We are going to use Rijndael because it is strong and
        // available on all platforms. 
        // You can use other algorithms, to do so substitute the
        // next line with something like 
        //      TripleDES alg = TripleDES.Create(); 
        Rijndael alg = Rijndael.Create();

        // Now set the key and the IV. 
        // We need the IV (Initialization Vector) because
        // the algorithm is operating in its default 
        // mode called CBC (Cipher Block Chaining).
        // The IV is XORed with the first block (8 byte) 
        // of the data before it is encrypted, and then each
        // encrypted block is XORed with the 
        // following block of plaintext.
        // This is done to make encryption more secure. 

        // There is also a mode called ECB which does not need an IV,
        // but it is much less secure. 
        alg.Key = Key;
        alg.IV = IV;

        // Create a CryptoStream through which we are going to be
        // pumping our data. 
        // CryptoStreamMode.Write means that we are going to be
        // writing data to the stream and the output will be written
        // in the MemoryStream we have provided. 
        CryptoStream cs = new CryptoStream(ms,
           alg.CreateEncryptor(), CryptoStreamMode.Write);

        // Write the data and make it do the encryption 
        cs.Write(clearData, 0, clearData.Length);

        // Close the crypto stream (or do FlushFinalBlock). 
        // This will tell it that we have done our encryption and
        // there is no more data coming in, 
        // and it is now a good time to apply the padding and
        // finalize the encryption process. 
        cs.Close();

        // Now get the encrypted data from the MemoryStream.
        // Some people make a mistake of using GetBuffer() here,
        // which is not the right way. 
        byte[] encryptedData = ms.ToArray();

        return encryptedData;
    } 
    public static string Decrypt(string cipherText, string Password)
    {
        // First we need to turn the input string into a byte array. 
        // We presume that Base64 encoding was used 
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        // Then, we need to turn the password into Key and IV 
        // We are using salt to make it harder to guess our key
        // using a dictionary attack - 
        // trying to guess a password by enumerating all possible words. 
        PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
            new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 
            0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

        // Now get the key/IV and do the decryption using
        // the function that accepts byte arrays. 
        // Using PasswordDeriveBytes object we are first
        // getting 32 bytes for the Key 
        // (the default Rijndael key length is 256bit = 32bytes)
        // and then 16 bytes for the IV. 
        // IV should always be the block size, which is by
        // default 16 bytes (128 bit) for Rijndael. 
        // If you are using DES/TripleDES/RC2 the block size is
        // 8 bytes and so should be the IV size. 
        // You can also read KeySize/BlockSize properties off
        // the algorithm to find out the sizes. 
        byte[] decryptedData = Decrypt(cipherBytes,
            pdb.GetBytes(32), pdb.GetBytes(16));

        // Now we need to turn the resulting byte array into a string. 
        // A common mistake would be to use an Encoding class for that.
        // It does not work 
        // because not all byte values can be represented by characters. 
        // We are going to be using Base64 encoding that is 
        // designed exactly for what we are trying to do. 
        return System.Text.Encoding.Unicode.GetString(decryptedData);
    }
    // Decrypt a byte array into a byte array using a key and an IV 
    public static byte[] Decrypt(byte[] cipherData,
                                byte[] Key, byte[] IV)
    {
        // Create a MemoryStream that is going to accept the
        // decrypted bytes 
        MemoryStream ms = new MemoryStream();

        // Create a symmetric algorithm. 
        // We are going to use Rijndael because it is strong and
        // available on all platforms. 
        // You can use other algorithms, to do so substitute the next
        // line with something like 
        //     TripleDES alg = TripleDES.Create(); 
        Rijndael alg = Rijndael.Create();

        // Now set the key and the IV. 
        // We need the IV (Initialization Vector) because the algorithm
        // is operating in its default 
        // mode called CBC (Cipher Block Chaining). The IV is XORed with
        // the first block (8 byte) 
        // of the data after it is decrypted, and then each decrypted
        // block is XORed with the previous 
        // cipher block. This is done to make encryption more secure. 
        // There is also a mode called ECB which does not need an IV,
        // but it is much less secure. 
        alg.Key = Key;
        alg.IV = IV;

        // Create a CryptoStream through which we are going to be
        // pumping our data. 
        // CryptoStreamMode.Write means that we are going to be
        // writing data to the stream 
        // and the output will be written in the MemoryStream
        // we have provided. 
        CryptoStream cs = new CryptoStream(ms,
            alg.CreateDecryptor(), CryptoStreamMode.Write);

        // Write the data and make it do the decryption 
        cs.Write(cipherData, 0, cipherData.Length);

        // Close the crypto stream (or do FlushFinalBlock). 
        // This will tell it that we have done our decryption
        // and there is no more data coming in, 
        // and it is now a good time to remove the padding
        // and finalize the decryption process. 
        cs.Close();

        // Now get the decrypted data from the MemoryStream. 
        // Some people make a mistake of using GetBuffer() here,
        // which is not the right way. 
        byte[] decryptedData = ms.ToArray();

        return decryptedData;
    }
}
