using System.Collections;
using System;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.Caching;

/// <summary>
/// 
/// Author: Shyam Vaidhyanathan
/// 
/// Description: Util functions for Billing. In some cases existing util 
/// functionalities need a wrapper or if the functionality does not exist.
/// 
/// </summary>


    public class BillingUtil
    {
        public BillingUtil()
        {
        }


        //Get the SKU from paid_services table for the AppID.
        public String getAppPaidSKU(Hashtable State)
        {
            DB db = new DB();
            string sql = "SELECT sku FROM paid_services WHERE application_id='" + State["application_id"].ToString() + "' AND status='paid'";
            string sku = db.ViziAppsExecuteScalar(State, sql);
            db.CloseViziAppsDatabase(State);
            State["SelectedAppSKU"] = sku;
            string AppSKU = sku;

            System.Diagnostics.Debug.WriteLine("AppSKU =" + AppSKU);
            return AppSKU;
        }


        /*  
        public bool ApplicationInit(Hashtable State, RadComboBox AppsList, string AppID, string AppCGCustomerCode)
        {
            Util util = new Util();
            string app = AppsList.SelectedValue;

            if (app.Contains("->"))
                return false;
        

            State["SelectedApp"] = app;

            State["SelectedAppType"] = util.GetAppType(State);
        

            State["application_id"] = util.GetAppID(State);
            AppID = State["application_id"].ToString();

            AppCGCustomerCode = getAppPaidSKU(State);

            if (State["SelectedDeviceType"] == null)
            {
                State["SelectedDeviceView"] = State["SelectedDeviceType"] = Constants.IPHONE;
            }


            XmlUtil x_util = new XmlUtil();
            State["SelectedDeviceView"] = State["SelectedDeviceType"] = x_util.GetAppDeviceType(State);

            string username = State["Username"].ToString();
            State["customer_id"] = util.GetCustomerIDFromUsername(State,username);

            return true;
        }
    */



        //public void StorePaidServicesDB(Customer returnCustomer, Hashtable State, bool paid)
        public void StorePaidServicesDB(string confirm, string sku, Hashtable State, bool paid)
        {
            try
            {
                Util util = new Util();
        
                string customer_id = "";

                if (State["customer_id"] != null)
                    customer_id = State["customer_id"].ToString();

                string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", "");
                string purchase_date = DateTime.Now.ToUniversalTime().ToString("d");
                string application_id = State["application_id"].ToString();
                string app_name = State["SelectedApp"].ToString();

                DB db = new DB();

                string sql = "UPDATE customers SET status='active'    WHERE status!='active' AND customer_id='" + customer_id + "'";
                try
                {
                    db.ViziAppsExecuteNonQuery(State, sql);
                }
                catch (Exception ex)
                {
                    util.LogError(State, ex);

                    if (!ex.Message.ToLower().Contains("duplicate"))
                        throw new Exception(ex.Message);
                }



                StringBuilder b_sql = new StringBuilder("INSERT INTO paid_services SET ");
                b_sql.Append("purchase_date='" + purchase_date + "',");
                b_sql.Append("sku='" + sku + "',");
                b_sql.Append("confirmation='" + confirm + "',");
                string username = util.GetUsernameFromCustomerID(State, customer_id);
                b_sql.Append("username='" + username + "',");
                b_sql.Append("customer_id='" + customer_id + "',");
                b_sql.Append("purchase_date_time='" + NOW + "',");
                b_sql.Append("application_id='" + application_id + "'");
                b_sql.Append(", app_name='" + app_name + "'");

                if (paid)
                    b_sql.Append(", status='paid'");

                System.Diagnostics.Debug.WriteLine("b_sql =" + b_sql.ToString());
                string sql_string = b_sql.ToString();

                try
                {
                    db.ViziAppsExecuteNonQuery(State, sql_string);
                }
                catch (Exception ex)
                {
                    util.LogError(State, ex);

                    if (!ex.Message.ToLower().Contains("duplicate"))
                        throw new Exception(ex.Message);
                }



                db.CloseViziAppsDatabase(State);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString() + ex.StackTrace.ToString());

            }
        }

        public void CancelPaidServicesDB(Hashtable State)
        {
            try
            {
                Util util = new Util();

                string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", "");
                string application_id = State["application_id"].ToString();

                DB db = new DB();


                StringBuilder b_sql = new StringBuilder("UPDATE paid_services SET ");
                b_sql.Append("cancellation_date_time='" + NOW + "',");
                b_sql.Append("status='cancelled'");
                b_sql.Append("WHERE application_id='" + application_id + "'");

                System.Diagnostics.Debug.WriteLine("b_sql =" + b_sql.ToString());
                string sql_string = b_sql.ToString();

                try
                {
                    db.ViziAppsExecuteNonQuery(State, sql_string);
                }
                catch (Exception ex)
                {
                    util.LogError(State, ex);

                    if (!ex.Message.ToLower().Contains("duplicate"))
                        throw new Exception(ex.Message);
                }


                db.CloseViziAppsDatabase(State);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString() + ex.StackTrace.ToString());

            }
        }

//        public void UpdatePaidServicesDB(Customer returnCustomer, Hashtable State)
        public void UpdatePaidServicesDB(string confirm, string sku,Hashtable State)
        {
            try
            {
                Util util = new Util();
                string customer_id = "";

                if (State["customer_id"] != null)
                    customer_id = State["customer_id"].ToString();

                string NOW = DateTime.Now.ToUniversalTime().ToString("u").Replace("Z", "");
                string purchase_date = DateTime.Now.ToUniversalTime().ToString("d");
                string application_id = State["application_id"].ToString();
                string app_name = State["SelectedApp"].ToString();

                DB db = new DB();

                string sql = "UPDATE customers SET status='active'   WHERE status!='active' AND customer_id='" + customer_id + "'";
                try
                {
                    db.ViziAppsExecuteNonQuery(State, sql);
                }
                catch (Exception ex)
                {
                    util.LogError(State, ex);

                    if (!ex.Message.ToLower().Contains("duplicate"))
                        throw new Exception(ex.Message);
                }



                StringBuilder b_sql = new StringBuilder("UPDATE paid_services SET ");
                b_sql.Append("purchase_date='" + purchase_date + "',");
                b_sql.Append("sku='" + sku + "',");
                b_sql.Append("confirmation='" + confirm + "',");
                b_sql.Append("purchase_date_time='" + NOW + "',");
                b_sql.Append("status='paid'");
                b_sql.Append("   WHERE application_id='" + application_id + "'");

                System.Diagnostics.Debug.WriteLine("b_sql =" + b_sql.ToString());
                string sql_string = b_sql.ToString();

                try
                {
                    db.ViziAppsExecuteNonQuery(State, sql_string);
                }
                catch (Exception ex)
                {
                    util.LogError(State, ex);

                    if (!ex.Message.ToLower().Contains("duplicate"))
                        throw new Exception(ex.Message);
                }



                db.CloseViziAppsDatabase(State);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString() + ex.StackTrace.ToString());

            }
        }



        //App Store (App Preparation step) is considered done if there is a branding SKU but the status field is not yet set to paid
        public bool IsAppStoreSubmissionPaid(Hashtable State, string app_name)
        {
            DB db = new DB();
            string sql = "SELECT COUNT(*) FROM paid_services WHERE (sku='" +  HttpRuntime.Cache["iOSSubmitServiceSku"].ToString() +
                "' OR sku='" +  HttpRuntime.Cache["AndroidSubmitServiceSku"].ToString() + "') AND app_name ='" + app_name + "' AND customer_id='" + State["CustomerID"].ToString() + "'";
            string count = db.ViziAppsExecuteScalar(State, sql);
            db.CloseViziAppsDatabase(State);
            return (count == "0") ? false : true;
        }

        //Any kind of paid
        public bool IsAppPaid(Hashtable State, string app_name)
        {
            DB db = new DB();
            string sql = "SELECT COUNT(*) FROM paid_services WHERE app_name ='" + app_name + "' AND customer_id='" + State["CustomerID"].ToString() + "' AND status='paid'";
            string count = db.ViziAppsExecuteScalar(State, sql);
            db.CloseViziAppsDatabase(State);
            return (count == "0") ? false : true;
        }


        //Any kind of paid
        public bool IsAppCancelled(Hashtable State, string app_name)
        {
            DB db = new DB();
            string sql = "SELECT COUNT(*) FROM paid_services WHERE app_name ='" + app_name + "' AND customer_id='" + State["CustomerID"].ToString() + "' AND status='cancelled'";
            string count = db.ViziAppsExecuteScalar(State, sql);
            db.CloseViziAppsDatabase(State);
            return (count == "0") ? false : true;
        }

    }
