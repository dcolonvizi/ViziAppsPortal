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
using System.Text;
using Telerik.Web.UI;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Diagnostics;
using System.Reflection;
using System.IO;

/// <summary>
/// Summary description for WebServiceProperties
/// </summary>
public class WebServiceProperties
{
	public WebServiceProperties()
	{
		//
		// TODO: Add constructor logic here
		//
	}
  
    /// <summary>
    /// Retrieves the list of web methods from the web service.
    /// </summary>
    /// <param name="service">Node containing the web service inputs.</param>
    /// <returns>The WebMethods of the WebService.</returns>
    public ArrayList GetWebServiceWebMethods(XmlNode service)
    {
        ArrayList _return = new ArrayList();

        // get the list of methods
        XmlNodeList _methods = service.SelectNodes("web_methods/web_method");
        if (_methods.Count == 0)
        {
            _methods = service.SelectNodes("web_method");
        }

        // iterate the node list
        foreach (XmlNode _curMethod in _methods)
        {
            // grab the name
            string name = _curMethod.SelectSingleNode("web_method_name").InnerText;
            if (name == "get_Result")
                continue;

            // grab the type
            string type = _curMethod.SelectSingleNode("web_method_type").InnerText;
            if (type.ToLower().IndexOf("void") >= 0)
                continue;

            // get the params
            ArrayList inputs = new ArrayList();
            XmlNodeList _params = _curMethod.SelectNodes("web_method_args/web_method_arg");

            // iterate the list
            foreach (XmlNode _param in _params)
            {
                string _pName = _param.SelectSingleNode("web_method_arg_name").InnerText;
                string _pType = _param.SelectSingleNode("web_method_arg_type").InnerText;
                WebServiceInput input = new WebServiceInput(_pName, _pType, _pName + " ( " + _pType + " )");
                inputs.Add(input);
            }

            WebMethod method = new WebMethod(name, type, inputs);
            _return.Add(method);
        }
        return _return;
    }
 
}
