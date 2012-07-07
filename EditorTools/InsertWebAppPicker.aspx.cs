using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class InsertWebAppPicker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true);
    }
    protected void pickerType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;
        
        pickerType.SelectedValue = e.Value;
        SectionMessage.Visible = true;
        section_names.Visible = true;
        switch (e.Value)
        {
            case "time":
            case "date":
                SectionMessage.Visible = false;
                section_names.Visible = false;
                SectionPages.SelectedIndex = 0;
                break;
            case "1_section":
                SectionMessage.Text = "Enter the Section Name";
                SectionPages.SelectedIndex = 1;
                break;
            case "2_sections":
                SectionMessage.Text = "Enter 2 Section Names separated by a comma";
                SectionPages.SelectedIndex = 2;
                break;
            case "3_sections":
                SectionMessage.Text = "Enter 3 Section Names separated by commas";
                SectionPages.SelectedIndex = 3;
                break;
            case "4_sections":
                SectionMessage.Text = "Enter 4 Section Names separated by commas";
                SectionPages.SelectedIndex = 4;
                break;
        }
    }
  
}
