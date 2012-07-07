using System;
using System.Collections;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Help_ProvisionHelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2)
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../../Default.aspx');", true);

        Pricing.Attributes.Add("onclick", "javascript: PopUp('http://viziapps.com/Pricing.aspx', 'height=750, width=1000, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');return false;");
        PaymentHelp.Attributes.Add("onclick", "javascript: PopUp('PaymentHelp.aspx', 'height=450, width=800, left=200, top=200, menubar=no, status=no, location=no, toolbar=no, scrollbars=yes, resizable=yes');return false;");

    }
}