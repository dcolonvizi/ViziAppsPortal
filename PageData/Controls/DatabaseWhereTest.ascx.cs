using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Controls_DatabaseWhereTest : System.Web.UI.UserControl
{
    public Controls_DatabaseWhereTest()
    {
    }
    public Controls_DatabaseWhereTest(string selected_condition_1st_field)
    {
        condition_1st_field.Value = selected_condition_1st_field;
    }
}