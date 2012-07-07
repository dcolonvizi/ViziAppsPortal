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
using System.Xml;
using MobiFlex;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using Telerik.Web.UI;
using System.Drawing;

public partial class Dialogs_AppDevice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;
            XmlUtil x_util = new XmlUtil();
            State["SelectedDeviceType"] = x_util.GetAppDeviceType(State);
            DesignedForDevice.Items.FindItemByValue(State["SelectedDeviceType"].ToString()).Selected = true;
        }
    }
    protected void DesignedForDevice_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        try
        {
            ClearMessages();
            
            DesignedForDevice.Text = e.Text;
            string device_design = e.Value;
            DeviceType.Text = e.Text;
            XmlUtil x_util = new XmlUtil();
            string previous_device_design = x_util.GetAppDeviceType(State);
  
            State["SelectedDeviceView"] = device_design;
            State["SelectedDeviceType"] = device_design;
            if (State["SelectedApp"] == null || State["SelectedApp"].ToString().Contains("->"))
            {
                util.SetDefaultBackgroundForView(State,device_design);
            }
  
            x_util.SetAppDeviceType(State, previous_device_design, device_design);
            
            Message.Text = "Main device for App has been set.";
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
            Message.Text = "Internal Error: " + ex.Message + ": " + ex.StackTrace;
        }
    }
    private void ClearMessages()
    {
        Message.Text = "";
    }
}