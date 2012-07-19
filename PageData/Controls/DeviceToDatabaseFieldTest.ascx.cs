using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Controls_DeviceToDatabaseFieldTest : System.Web.UI.UserControl
{
    public Controls_DeviceToDatabaseFieldTest()
    {
    }
    public Controls_DeviceToDatabaseFieldTest(string selected_database_field)
    {
        database_field.Value = selected_database_field;
    }
}