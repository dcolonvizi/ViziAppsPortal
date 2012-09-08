using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Web.Caching;
using System.IO;

namespace MobiFlex 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
        public Hashtable UsersList;
  
		public Global()
		{
			InitializeComponent();
 		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
            HttpRuntime.Cache["UsersList"] = new Hashtable();

            Init init = new Init();
            init.InitSiteConfigurations();
            HttpRuntime.Cache["NewProjectPath"] = Server.MapPath(".") + @"\App_data\NewViziAppsNativeApp.xml";
            HttpRuntime.Cache["CanvasHtml"] = File.ReadAllText(Server.MapPath(".") + @"\App_Data\Canvas.txt");
            HttpRuntime.Cache["NewWebAppHtml"] = File.ReadAllText(Server.MapPath(".") + @"\App_Data\NewViziAppsWebApp.txt");
            HttpRuntime.Cache["NewHybridAppXml"] = File.ReadAllText(Server.MapPath(".") + @"\App_Data\NewViziAppsHybridApp.xml");
            HttpRuntime.Cache["ShareThisScripts"] = File.ReadAllText(Server.MapPath(".") + @"\App_Data\ShareThisScripts.txt");
            HttpRuntime.Cache["TempFilesPath"] = Server.MapPath(".") + @"\temp_files\";
            HttpRuntime.Cache["Server"] = Server;

		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{
            //init cache hashtable with 30 minute sliding timeout
            HttpRuntime.Cache.Insert(Session.SessionID, new Hashtable());
            ((Hashtable)HttpRuntime.Cache[Session.SessionID])["SessionID"] = Session.SessionID;
		}
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("p3p", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
        } 

        protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{
            Hashtable UsersList = (Hashtable)HttpRuntime.Cache["UsersList"];
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            if (State !=  null && State["Username"] != null && UsersList != null)
            {
                UsersList.Remove(State["Username"].ToString());
                HttpRuntime.Cache["UsersList"] = UsersList;
             }
            State = null;
        }

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

