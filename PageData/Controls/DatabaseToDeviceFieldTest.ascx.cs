using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Controls_DatabaseToDeviceFieldTest : System.Web.UI.UserControl
{
    public Controls_DatabaseToDeviceFieldTest()
    {
    }
    public Controls_DatabaseToDeviceFieldTest(string selected_database_field)
    {
        database_field.Value = selected_database_field;
    }
}