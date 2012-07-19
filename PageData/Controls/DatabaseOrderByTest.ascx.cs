using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Controls_DatabaseOrderByTest : System.Web.UI.UserControl
{
    public Controls_DatabaseOrderByTest()
    {
    }
    public Controls_DatabaseOrderByTest(string selected_sort_field)
    {
        sort_field.Value = selected_sort_field;
    }
}