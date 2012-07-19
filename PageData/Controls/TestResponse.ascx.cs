using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Controls_TestResponse : System.Web.UI.UserControl
{
    public Controls_TestResponse()
    {
    }
    public Controls_TestResponse(string parameter)
    {
        string field = parameter.Remove(parameter.IndexOf("="));
        device_field.Value = field;
        string value = parameter.Substring(parameter.IndexOf("=")+1);
        device_field_value.Value = value;
    }
}