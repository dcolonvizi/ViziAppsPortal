using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_DatabaseCommandTest : System.Web.UI.UserControl
{
    public Controls_DatabaseCommandTest()
    {
    }
    public Controls_DatabaseCommandTest(string selected_table_name)
    {
        table.Value = selected_table_name;
    }
}