using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Drawing;

/// <summary>
/// Summary description for WebAppsUtil
/// </summary>
public class WebAppsUtil
{
	public WebAppsUtil()
	{
	}
    public string GetWebApp(Hashtable State,XmlDocument xmlDoc, double x_size_factor,double y_size_factor )
    {
        try
        {
            XmlUtil x_util = new XmlUtil();
            Util util = new Util();
            DataSources DS = new DataSources();
            HtmlDocument htmlDoc = new HtmlDocument();

            //Load App Foundation
            StringBuilder NewWebAppHtml = new StringBuilder(State["NewWebAppHtml"].ToString());
            if (State["IsProduction"] == null)
                State["IsProduction"] = false;
            if ((bool)State["IsProduction"]== true)
            {
                string production_app_name = util.GetProductionAppName(State);
                NewWebAppHtml.Replace("CUSTOM_TITLE", production_app_name);
            }
            else
            {
                NewWebAppHtml.Replace("CUSTOM_TITLE", State["SelectedApp"].ToString());
            }

            /*if(false)
            {
                 NewWebAppHtml.Replace("ADDON_VIZIAPPS_SCRIPTS", State["ShareThisScripts"].ToString());
            }
            else*/
                NewWebAppHtml.Replace("ADDON_VIZIAPPS_SCRIPTS", "");

                        //add custom header html
            NewWebAppHtml.Replace("CUSTOM_ACCOUNT_HEADER",util.GetCustomHeaderHTML(State));

            htmlDoc.LoadHtml(NewWebAppHtml.ToString());
            HtmlNode root = htmlDoc.DocumentNode;

            //Load Custom App Init
            StringBuilder customInitScript = new StringBuilder();
            string application_id = util.GetAppID(State);
            customInitScript.Append("var app_id = '" + application_id + "';\n");

            string app_time_stamp = null;
            if ((bool)State["IsProduction"] == false)
                app_time_stamp = util.GetStagingAppTimeStamp(State, application_id);
            else
                app_time_stamp = util.GetProductionAppTimeStamp(State, application_id);

            if (State["SelectedDeviceType"] == null)
                State["SelectedDeviceType"] = Constants.IPHONE;
 
            customInitScript.Append("\tvar app_time_stamp = '" + app_time_stamp + "';\n");
            customInitScript.Append("\tvar customer_id = '" + State["CustomerID"].ToString() + "';\n");
            customInitScript.Append("\tvar customer = '" + State["Username"].ToString() + "';\n");
            customInitScript.Append("\tvar app_name = '" + State["SelectedApp"].ToString() + "';\n");
            customInitScript.Append("\tvar design_device_type = '" + State["SelectedDeviceType"].ToString().ToLower() + "';\n");
            string viziapps_transition_type = null;
            if(State["PageTransitionType"] != null)
                viziapps_transition_type = State["PageTransitionType"].ToString();
            else
                 viziapps_transition_type ="slide";
            customInitScript.Append("\tvar viziapps_transition_type = '" + viziapps_transition_type + "';\n");
          
            switch (State["SelectedDeviceType"].ToString())
            {
                case Constants.IPHONE:
                case Constants.ANDROID_PHONE:
                default:
                    customInitScript.Append("\tvar ios_landscape_width_factor = " + Constants.IPHONE_LANDSCAPE_WIDTH_FACTOR + ";\n");
                    customInitScript.Append("\tvar ios_landscape_height_factor = " + Constants.IPHONE_LANDSCAPE_HEIGHT_FACTOR + ";\n");
                    customInitScript.Append("\tvar android_landscape_width_factor = " + Constants.ANDROID_PHONE_LANDSCAPE_WIDTH_FACTOR + ";\n");
                    customInitScript.Append("\tvar android_landscape_height_factor = " + Constants.ANDROID_PHONE_LANDSCAPE_HEIGHT_FACTOR + ";\n");
                    break;
                case Constants.ANDROID_TABLET:
                case Constants.IPAD:
                    customInitScript.Append("\tvar ios_landscape_width_factor = " + Constants.IPAD_LANDSCAPE_WIDTH_FACTOR + ";\n");
                    customInitScript.Append("\tvar ios_landscape_height_factor = " + Constants.IPAD_LANDSCAPE_HEIGHT_FACTOR + ";\n");
                    customInitScript.Append("\tvar android_landscape_width_factor = " + Constants.ANDROID_TABLET_LANDSCAPE_WIDTH_FACTOR + ";\n");
                    customInitScript.Append("\tvar android_landscape_height_factor = " + Constants.ANDROID_TABLET_LANDSCAPE_HEIGHT_FACTOR + ";\n");
                    break;               
                }

            if((bool)State["IsProduction"])
                 customInitScript.Append("\tvar is_production = true;\n"); 
            else
                customInitScript.Append("\tvar is_production = false;\n"); 

            string device_type = x_util.GetAppDeviceType(State);
            if (device_type == Constants.IPAD || device_type == Constants.ANDROID_TABLET)
                customInitScript.Append("\tvar does_background_image_exist = false;\n");
            else
                customInitScript.Append("\tvar does_background_image_exist = true;\n");

            if (DS.DoesAppUseGoogleSpreadsheets(State))
            {
                customInitScript.Append("\tvar doesAppUseGoogleSpreadsheets = true;\n");
                customInitScript.Append("\tvar isGoogleDataLoaded = false;\n");
                customInitScript.Append("google.load('gdata', '2.x');\n");
                customInitScript.Append("google.setOnLoadCallback(onGoogleDataLoad);\n");
                customInitScript.Append("function onGoogleDataLoad() {isGoogleDataLoaded=true;}\n");
            }
            else
            {
                customInitScript.Append("\tvar doesAppUseGoogleSpreadsheets = false;\n");
                customInitScript.Append("\tvar isGoogleDataLoaded = false;\n");
            }
 
            customInitScript.Append("\tvar latitude;\n");
            customInitScript.Append("\tvar longitude;\n");
 
            //get app icon
            HtmlNode head_node = root.SelectSingleNode("//head");
            HtmlNode icon_node = htmlDoc.CreateElement("link");
            icon_node.Attributes.Append(htmlDoc.CreateAttribute("rel", "apple-touch-icon"));
            if (device_type == Constants.IPAD || device_type == Constants.ANDROID_TABLET)
            {
                if ((bool)State["IsProduction"] == false)
                {
                    icon_node.Attributes.Append(htmlDoc.CreateAttribute("href", "http://viziapps.s3-website-us-east-1.amazonaws.com/apps/viziapps_icon_ipad.png"));
                }
                else
                {
                    icon_node.Attributes.Append(htmlDoc.CreateAttribute("href", util.GetApplicationIcon(State, application_id, "72")));
                }
            }   
            else
            {
                if ((bool)State["IsProduction"] == false)
                {
                    icon_node.Attributes.Append(htmlDoc.CreateAttribute("href", "http://viziapps.s3-website-us-east-1.amazonaws.com/apps/viziapps_icon.jpg"));
                }
                else
                {
                    icon_node.Attributes.Append(htmlDoc.CreateAttribute("href", util.GetApplicationIcon(State, application_id, "57")));
                }
            }

            head_node.AppendChild(icon_node);

            if (device_type == Constants.IPAD || device_type == Constants.ANDROID_TABLET)
            {
                HtmlNode ipad_splash_node = htmlDoc.CreateElement("link");
                ipad_splash_node.Attributes.Append(htmlDoc.CreateAttribute("rel", "apple-touch-startup-image"));
                ipad_splash_node.Attributes.Append(htmlDoc.CreateAttribute("media", "screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:landscape)"));
                if ((bool)State["IsProduction"] == false)
                {
                    ipad_splash_node.Attributes.Append(htmlDoc.CreateAttribute("href", "http://viziapps.s3-website-us-east-1.amazonaws.com/apps/viziapps_splash_ipad_landscape.jpg"));
                }
                else
                {
                    ipad_splash_node.Attributes.Append(htmlDoc.CreateAttribute("href", util.GetApplicationSplashImage(State, application_id)));
                }
                head_node.AppendChild(ipad_splash_node);

                HtmlNode ipad_splash_node2 = htmlDoc.CreateElement("link");
                ipad_splash_node2.Attributes.Append(htmlDoc.CreateAttribute("rel", "apple-touch-startup-image"));
                ipad_splash_node2.Attributes.Append(htmlDoc.CreateAttribute("media", "screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:portrait)"));
                if ((bool)State["IsProduction"] == false)
                {
                    ipad_splash_node2.Attributes.Append(htmlDoc.CreateAttribute("href", "http://viziapps.s3-website-us-east-1.amazonaws.com/apps/viziapps_splash_ipad_portrait.jpg"));
                }
                else
                {
                    ipad_splash_node2.Attributes.Append(htmlDoc.CreateAttribute("href", util.GetApplicationSplashImage(State, application_id)));
                }
                head_node.AppendChild(ipad_splash_node2);
            }
            else  //<link rel="apple-touch-startup-image" href="/startup.png">
            {
                HtmlNode splash_node = htmlDoc.CreateElement("link");
                splash_node.Attributes.Append(htmlDoc.CreateAttribute("rel", "apple-touch-startup-image"));
                if ((bool)State["IsProduction"] == false)
                {
                    splash_node.Attributes.Append(htmlDoc.CreateAttribute("href", "http://viziapps.s3-website-us-east-1.amazonaws.com/apps/viziapps_splash.jpg"));
                }
                else
                {
                    splash_node.Attributes.Append(htmlDoc.CreateAttribute("href", util.GetApplicationSplashImage(State, application_id)));
                }
                head_node.AppendChild(splash_node);
            }

            string backgroundUrl = null;
            if (device_type != Constants.IPAD)
            {
                //get background image
                XmlNode configuration_node = xmlDoc.SelectSingleNode("//configuration");
                XmlNode background_node = configuration_node.SelectSingleNode("background");
                if (background_node == null)
                {
                    background_node = x_util.CreateNode(xmlDoc, configuration_node, "background");
                }
                XmlNode background_image_node = background_node.SelectSingleNode("image_source");
                if (background_image_node == null)
                {
                    background_image_node = x_util.CreateNode(xmlDoc, background_node, "image_source", "https://s3.amazonaws.com/MobiFlexImages/apps/images/backgrounds/standard_w_header_iphone.jpg");
                }
                string background_image = background_image_node.InnerText;
                if (background_image.Contains("customer_media") || background_image.Contains("s3.amazonaws.com"))
                {
                    if (State["SelectedDeviceType"].ToString() == Constants.ANDROID_PHONE)
                        background_image = background_image.Replace("_iphone.", "_android.");
                    backgroundUrl = background_image;
                }
                else
                {
                    background_image = background_image.Substring(background_image.LastIndexOf("/") + 1);
                    if (State["SelectedDeviceType"].ToString() == Constants.ANDROID_PHONE)
                        background_image = background_image.Replace("_iphone.", "_android.");

                    backgroundUrl = "https://s3.amazonaws.com/MobiFlexImages/apps/images/backgrounds/" + background_image;
                }
            }

            //Load App Pages
            XmlNodeList page_nodes = xmlDoc.SelectNodes("//pages/page");
            if (page_nodes == null)
                return "";

            //get first page
            XmlNode first_page_name_node = xmlDoc.SelectSingleNode("//pages/first_page_name");
            ArrayList page_node_list = new ArrayList();
            string first_page_name = "";
            if (first_page_name_node != null)
            {
                first_page_name = first_page_name_node.InnerText;
                foreach (XmlNode page_node in page_nodes)
                {
                    if (page_node.SelectSingleNode("name").InnerText == first_page_name)
                    {
                        page_node_list.Add(page_node);
                        break;
                    }
                }
            }
            
            foreach (XmlNode page_node in page_nodes)
            {
                if (page_node.SelectSingleNode("name").InnerText != first_page_name)
                {
                    page_node_list.Add(page_node);
                }
            }

            HtmlNode body_node = root.SelectSingleNode("//body");
 
            StringBuilder functions = new StringBuilder();

            if ((bool)State["IsProduction"] == false && State["SelectedAppType"].ToString() == Constants.HYBRID_APP_TYPE)
            {
                //add first function to go to login page
                StringBuilder settings_actions = new StringBuilder("$('#viziapps_login_button').bind('tap',function(event){\n");
                settings_actions.Append("\t\twindow.plugins.vsettings.login(null,null);\n");
                settings_actions.Append("\t\tevent.stopPropagation();\n");
                settings_actions.Append("\t});\n");
                functions.Append(settings_actions.ToString());
            }

            bool isFirstPage = true;
            foreach (XmlNode page_node in page_node_list)
            {
                ArrayList dialogs_in_page = new ArrayList();
                HtmlNode html_page_node = htmlDoc.CreateElement("div");
                html_page_node.Attributes.Append(htmlDoc.CreateAttribute("data-role", "page"));
                html_page_node.Attributes.Append(htmlDoc.CreateAttribute("id", page_node.SelectSingleNode("name").InnerText));
                if (device_type == Constants.IPAD || device_type == Constants.ANDROID_TABLET)
                    html_page_node.Attributes.Append(htmlDoc.CreateAttribute("style", "background-image: none; background-color:" + x_util.GetBackgroundColor(State)));

                HtmlNode content_node = htmlDoc.CreateElement("div");
                content_node.Attributes.Append(htmlDoc.CreateAttribute("data-role", "content"));
                content_node.Attributes.Append(htmlDoc.CreateAttribute("style", "background-image:none;background-color:transparent"));
                html_page_node.AppendChild(content_node);

                //create separate background image for phones
                if (device_type != Constants.IPAD)
                {
                    HtmlNode field_node = htmlDoc.CreateElement("div");
                    Size size = util.GetImageSize(backgroundUrl);
                    if (size != null)
                    {
                        double background_width = Convert.ToDouble(size.Width) * x_size_factor;
                        double background_height = Convert.ToDouble(size.Height) * y_size_factor;
                        field_node.Attributes.Append(htmlDoc.CreateAttribute("style", "z-index:1;position:absolute;left:0px;top:0px; width:" + background_width.ToString() + "px;height:" + background_height.ToString() + "px;"));
                    }

                    x_util.AddImageNode(htmlDoc, field_node, backgroundUrl);
                    content_node.AppendChild(field_node);
                }

                XmlNode fields_node = page_node.SelectSingleNode("fields");
                if (fields_node == null)
                    continue;

                XmlNodeList field_list = page_node.SelectSingleNode("fields").ChildNodes;
                foreach (XmlNode field in field_list)
                {
                    Hashtable field_map = x_util.ParseXmlToHtml(field);
                    HtmlNode field_node = null;
                    switch (field.Name)
                    {
                        case "text_field":
                        case "hidden_field":
                             field_node = htmlDoc.CreateElement("input");
                            break;
                        case "map":
                        case "photo":
                        case "gps":
                            continue;
                        case "alert":
                        case "button":
                        case "image_button":
                            field_node = htmlDoc.CreateElement("a");
                            break;
                        default:
                            field_node = htmlDoc.CreateElement("div");
                            break;
                    }

                   content_node.AppendChild(field_node);
                   field_node.Attributes.Append(htmlDoc.CreateAttribute("field_type", field.Name));
                   SetCommonHtmlAttributes(htmlDoc, field_node, field_map, x_size_factor, y_size_factor, field.Name);

                    switch (field.Name)
                    {
                        case "image":
                            string image_url = field_map["image_source"].ToString();
                            if (!field_map.ContainsKey("width")) //the xml does not have the width and height so use the width and height of the actual image
                            {
                                Size size = util.GetImageSize(image_url);
                                if (size != null)
                                {
                                    field_node.Attributes["style"].Value += "width:" + size.Width.ToString() + "px;height:" + size.Height.ToString() + "px;";
                                }
                            }
                            x_util.AddImageNode(htmlDoc, field_node, image_url);
                            break;
                        case "audio":
                            break;
                        case "label":
                            field_node.InnerHtml = field_map["text"].ToString();
                            field_node.Attributes["style"].Value += "text-shadow:none;";
                            break;
                        case "text_field":
                            field_node.Attributes["style"].Value += "background-color:#f0f0f0;text-shadow:none;";
                            if (field_map["text"] != null)
                            {
                                HtmlAttribute text_field_value = htmlDoc.CreateAttribute("value");
                                text_field_value.Value = HttpUtility.HtmlAttributeEncode(field_map["text"].ToString());
                                field_node.Attributes.Append(text_field_value);
                            }
                            field_node.Attributes.Append(htmlDoc.CreateAttribute("type", field_map["type"].ToString()));
                            break;
                        case "text_area":  
                            field_node.Attributes["style"].Value += "overflow-y: hidden;";
                            HtmlNode nav = htmlDoc.CreateElement("nav");
                            field_node.AppendChild(nav);
                            HtmlNode textarea = htmlDoc.CreateElement("textarea");
                            textarea.Attributes.Append(htmlDoc.CreateAttribute("data-role","none" ));
                            textarea.Attributes.Append(htmlDoc.CreateAttribute("style", "text-shadow:none;margin: 0px;padding: 7px 7px 7px 7px;border-color:#aaaaaa;"));
                            SetTextHtmlAttributes(textarea, field_map);
                            if (field_map["edit_type"] != null && field_map["edit_type"].ToString() == "non_editable")
                                textarea.Attributes.Append(htmlDoc.CreateAttribute("readonly", "readonly"));
                            textarea.InnerHtml = HttpUtility.HtmlEncode(field_map["text"].ToString().Replace("\\n", "\n"));
                            nav.AppendChild(textarea);
                             break;
                        case "image_button":
                            field_node.Attributes["style"].Value += "margin: 0 0 0 0;";
                             Hashtable imagebuttonAction = GetAction(field);
                            SetActions(page_node, field_map, imagebuttonAction, functions);
                            if (imagebuttonAction["next_page"] != null)
                                field_node.Attributes.Append(htmlDoc.CreateAttribute("href", "#" + imagebuttonAction["next_page"].ToString()));
                            else if (imagebuttonAction["previous_page"] != null)//do not use history function (commented code) because it does not work with embedded iframe. Use ChangePage instead
                            {
                                 field_node.Name = "div";
                            }
                            else
                                field_node.Attributes.Append(htmlDoc.CreateAttribute("href", "#"));

                            HtmlNode image_button_node = htmlDoc.CreateElement("img");
                            image_button_node.Attributes.Append(htmlDoc.CreateAttribute("src", field_map["image_source"].ToString()));
                            image_button_node.Attributes.Append(htmlDoc.CreateAttribute("width", "100%"));
                            image_button_node.Attributes.Append(htmlDoc.CreateAttribute("height", "100%"));
                            field_node.AppendChild(image_button_node);
                           break;
                        case "button":
                            field_node.Attributes["style"].Value += "margin: 0 0 0 0;";
                            Hashtable buttonAction = GetAction(field);
                            Boolean isActiveButton = true;
                            if (buttonAction["next_page"] != null)
                            {
                                field_node.Attributes.Append(htmlDoc.CreateAttribute("href", "#" + buttonAction["next_page"].ToString()));
                            }
                            else if (buttonAction["previous_page"] != null)//do not use history function (commented code) because it does not work with embedded iframe. Use ChangePage instead
                            {
                                field_node.Name = "div";
                            }
                            else
                            {
                                field_node.Name = "div";
                                isActiveButton = false;
                            }

                            if (isActiveButton)
                            {
                                field_node.Attributes.Append(htmlDoc.CreateAttribute("data-role", "button"));
                                field_node.Attributes.Append(htmlDoc.CreateAttribute("data-inline", "true"));
                                field_node.Attributes.Append(htmlDoc.CreateAttribute("data-transition", viziapps_transition_type));
                            }

                            //get the button theme
                            if (field_map["image_source"] != null)
                            {
                                string button_image_url = field_map["image_source"].ToString();
                                string theme = button_image_url.Substring(button_image_url.LastIndexOf("/") + 1);
                                if (theme == "a" || theme == "b" || theme == "c" || theme == "d" || theme == "e")
                                     field_node.Attributes.Append(htmlDoc.CreateAttribute("data-theme", theme));
                                else
                                    field_node.Attributes["style"].Value += "background-image:url(" + field_map["image_source"].ToString() + ");background-size:100% 100%;";
                            }
                            else
                            {
                                field_node.Attributes.Append(htmlDoc.CreateAttribute("data-theme", "b"));
                            }

                            SetActions(page_node, field_map, buttonAction, functions);
                            HtmlNode button_table_node = htmlDoc.CreateElement("table");
                            int height_start = field_node.Attributes["style"].Value.IndexOf("height:") + 7;
                            int height_end = field_node.Attributes["style"].Value.IndexOf("px", height_start);
                            button_table_node.Attributes.Append(htmlDoc.CreateAttribute("style", "width:100%;height:" +
                            field_node.Attributes["style"].Value.Substring(height_start, height_end - height_start) + "px"));
                            HtmlNode row_node = htmlDoc.CreateElement("tr");
                            HtmlNode column_node = htmlDoc.CreateElement("td");
                            column_node.InnerHtml = field_map["text"].ToString();
                            column_node.Attributes.Append(htmlDoc.CreateAttribute("style", "text-shadow:none;display:table-cell; vertical-align:middle;text-align:center"));
                            row_node.AppendChild(column_node);
                            button_table_node.AppendChild(row_node);
                            field_node.AppendChild(button_table_node);
                            break;
                        case "picker":
                            field_node.Attributes.Append(htmlDoc.CreateAttribute("data-role", "fieldcontain"));
                            field_node.Attributes["style"].Value += "height:25px;font-size:16px;margin: 0 0 0 0;padding: 0 0 0 0;";

                            HtmlNode select_node = htmlDoc.CreateElement("select");
                            select_node.Attributes.Append(htmlDoc.CreateAttribute("name", field_map["id"].ToString()));
                            select_node.Attributes.Append(htmlDoc.CreateAttribute("id", field_map["id"].ToString()));
                            select_node.Attributes.Append(htmlDoc.CreateAttribute("data-native-menu", "false"));
                            field_node.AppendChild(select_node);

                            string picker_type = field_map["picker_type"].ToString();
                            if (picker_type.Contains("section"))
                            {
                                XmlNodeList picker_fields = field.SelectNodes("picker_fields/picker_field");
                                StringBuilder option_list = new StringBuilder();
                                foreach (XmlNode picker_field in picker_fields) //this code does not process multiple fields yet
                                {
                                    XmlNode name_node = picker_field.SelectSingleNode("name");
                                    XmlNode options_node = picker_field.SelectSingleNode("options");
                                    if (options_node != null)
                                    {
                                        string option_string = options_node.InnerText;
                                        string[] picker_option_list = option_string.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                        foreach (string option in picker_option_list)
                                        {
                                            HtmlNode option_node = htmlDoc.CreateElement("option");
                                            option_node.Attributes.Append(htmlDoc.CreateAttribute("value", option));
                                            option_node.InnerHtml = option;
                                            select_node.AppendChild(option_node);
                                        }
                                    }
                                }
                            }
                            break;
                        case "switch":
                            field_node.Attributes["style"].Value += "margin: 0 0 0 0;";
                            Hashtable switchAction = GetAction(field);
                            SetActions(page_node, field_map, switchAction, functions);
                            string default_switch_value = field_map["default_value"].ToString();
 
                            field_node.Attributes.Append(htmlDoc.CreateAttribute("data-role", "fieldcontain"));

                            HtmlNode switch_node = htmlDoc.CreateElement("select");
                            switch_node.Attributes.Append(htmlDoc.CreateAttribute("data-role", "slider"));
                            HtmlNode option_off = htmlDoc.CreateElement("option");
                            option_off.Attributes.Append(htmlDoc.CreateAttribute("value", "off"));
                            HtmlNode option_on = htmlDoc.CreateElement("option");
                            option_on.Attributes.Append(htmlDoc.CreateAttribute("value", "on"));
                            switch_node.AppendChild(option_off);
                            switch_node.AppendChild(option_on);
                               
                            if (field_map["type"] == null)
                                field_map["type"] = "on_off";
 
                            switch (field_map["type"].ToString())
                            {
                                case "on_off":
                                    option_off.InnerHtml = "Off";
                                    option_on.InnerHtml = "On";
                                    if (default_switch_value == "on")
                                        option_on.Attributes.Append(htmlDoc.CreateAttribute("selected", "selected"));
                                    else
                                        option_off.Attributes.Append(htmlDoc.CreateAttribute("selected", "selected"));
                                    break;
                                case "true_false":
                                    option_off.InnerHtml = "False";
                                    option_on.InnerHtml = "True";
                                    if (default_switch_value == "true")
                                        option_on.Attributes.Append(htmlDoc.CreateAttribute("selected", "selected"));
                                    else
                                        option_off.Attributes.Append(htmlDoc.CreateAttribute("selected", "selected"));
                                    break;
                                case "yes_no":
                                    option_off.InnerHtml = "No";
                                    option_on.InnerHtml = "Yes";
                                    if (default_switch_value == "yes")
                                        option_on.Attributes.Append(htmlDoc.CreateAttribute("selected", "selected"));
                                    else
                                        option_off.Attributes.Append(htmlDoc.CreateAttribute("selected", "selected"));
                                    break;
                            }
                            field_node.AppendChild(switch_node);
                            
                            break;
                        case "checkbox":
                            field_node.Attributes["style"].Value += "margin: 0 0 0 0;";
                            Hashtable checkboxAction = GetAction(field);
                            SetActions(page_node, field_map, checkboxAction, functions);
                            HtmlNode checkbox_node = htmlDoc.CreateElement("input");
                            checkbox_node.Attributes.Append(htmlDoc.CreateAttribute("type", "checkbox"));
                            checkbox_node.Attributes.Append(htmlDoc.CreateAttribute("class", "custom"));
                            checkbox_node.Attributes.Append(htmlDoc.CreateAttribute("name", field_map["id"].ToString() + "_0"));
                            checkbox_node.Attributes.Append(htmlDoc.CreateAttribute("id", field_map["id"].ToString() + "_0"));
                            string default_value = field_map["default_value"].ToString();
                            if (default_value == "checked")
                                checkbox_node.Attributes.Append(htmlDoc.CreateAttribute("checked", "checked"));
                            field_node.AppendChild(checkbox_node);
                            HtmlNode checkbox_label_node = htmlDoc.CreateElement("label");
                            checkbox_label_node.InnerHtml = "&nbsp;";
                            checkbox_label_node.Attributes.Append(htmlDoc.CreateAttribute("for", field_map["id"].ToString() + "_0"));
                            field_node.AppendChild(checkbox_label_node);  
                            break;
                        case "slider":
                            HtmlNode slider_node = htmlDoc.CreateElement("input");
                            slider_node.Attributes.Append(htmlDoc.CreateAttribute("name", field_map["id"].ToString()));
                            slider_node.Attributes.Append(htmlDoc.CreateAttribute("type", "range"));
                            slider_node.Attributes.Append(htmlDoc.CreateAttribute("min", field_map["min_value"].ToString()));
                            slider_node.Attributes.Append(htmlDoc.CreateAttribute("max", field_map["max_value"].ToString()));
                            double slider_value = (Convert.ToDouble(field_map["min_value"].ToString()) + Convert.ToDouble(field_map["max_value"].ToString())) / 2.0;
                            slider_node.Attributes.Append(htmlDoc.CreateAttribute("value", slider_value.ToString()));
                            slider_node.Attributes.Append(htmlDoc.CreateAttribute("data-theme", "b"));
                            slider_node.Attributes.Append(htmlDoc.CreateAttribute("data-track-theme", "c"));
                            field_node.AppendChild(slider_node);
                            break;
                        case "table":
                            HtmlNode table_nav_node = htmlDoc.CreateElement("nav");
                            HtmlNode table_node = htmlDoc.CreateElement("ul");
                            table_nav_node.AppendChild(table_node);
                            field_node.AppendChild(table_nav_node);
                            Hashtable tableActions = GetAction(field);
                            table_node.Attributes.Append(htmlDoc.CreateAttribute("data-role", "listview"));
                            table_node.Attributes.Append(htmlDoc.CreateAttribute("data-inset", "true"));
                            table_node.Attributes.Append(htmlDoc.CreateAttribute("data-theme", "d"));
                            HtmlNode header = htmlDoc.CreateElement("li");
                            header.Attributes.Append(htmlDoc.CreateAttribute("data-role", "list-divider"));
                            if (field_map["display_name"] == null)
                                header.InnerHtml = field_map["id"].ToString();
                            else
                                header.InnerHtml = field_map["display_name"].ToString();
                            table_node.AppendChild(header);

                            XmlNodeList table_fields = field.SelectNodes("table_fields/table_field");
                            StringBuilder table_option_list = new StringBuilder();
                            ArrayList rows = new ArrayList();
                            int field_index = 0;
                            string[] field_types = new string[table_fields.Count];
                            string[] field_name_list = new string[table_fields.Count];
                            string[,] table = null;
                            int n_rows = 0;
                            int n_cols = 0;
                            string hidden_field_name = null;
                            foreach (XmlNode table_field in table_fields)
                            {
                                XmlNode field_name_node = table_field.SelectSingleNode("name");
                                field_name_list[field_index] = field_name_node.InnerText;
                                XmlNode type_node = table_field.SelectSingleNode("type");
                                field_types[field_index] = type_node.InnerText;
                                if (type_node.InnerText == "hidden")
                                    hidden_field_name = field_name_list[field_index];
                                XmlNode options_node = table_field.SelectSingleNode("options");
                                if (options_node != null)
                                {
                                    string option_string = options_node.InnerText;
                                    string[] option_list = option_string.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                    int row_index = 0;

                                    foreach (string option in option_list)
                                    {
                                        if (table == null) //the first array of options sets the number of rows
                                        {
                                            n_rows = option_list.Length;
                                            n_cols = table_fields.Count;
                                            table = new string[n_rows, n_cols];
                                        }
                                        if (row_index >= n_rows)
                                            return "Error: Field " + field_name_list[field_index] + " has more than the " + n_rows.ToString() + " rows in field " + field_name_list[0];
                                        table[row_index, field_index] = option;
                                        row_index++;
                                    }
                                }
                                else //when there are no preloaded lists create 1 row as a template for the others to come
                                {
                                    if (table == null)
                                    {
                                        n_rows = 1;
                                        n_cols = table_fields.Count;
                                        table = new string[n_rows, n_cols];
                                    }
                                    table[0, field_index] = "";

                                }
                                field_index++;
                            }

                            SetTableRowActions(page_node, field_map["id"].ToString(), field_map, tableActions, functions);

                            //selected how row will appear
                            string row_html = null;
                            switch (field_map["table_type"].ToString())
                            {
                                case "1text":
                                    row_html = Constants.one_text;
                                    break;
                                case "1texthidden":
                                    row_html = Constants.one_text_hidden;
                                    break;
                                case "2texts":
                                    row_html = Constants.two_texts;
                                    break;
                                case "2textshidden":
                                    row_html = Constants.two_texts_hidden;
                                    break;
                                case "image1text":
                                    row_html = Constants.image_1text;
                                    break;
                                case "image1texthidden":
                                    row_html = Constants.image_1text_hidden;
                                    break;
                                case "image2texts":
                                    row_html = Constants.image_2texts;
                                    break;
                                case "image2textshidden":
                                    row_html = Constants.image_2texts_hidden;
                                    break;
                            }
                            for (int r = 0; r < n_rows; r++)
                            {
                                HtmlNode row = htmlDoc.CreateElement("li");
                                row.Attributes.Append(htmlDoc.CreateAttribute("data-icon", "arrow-r"));
                                string id = field_map["id"].ToString() + "_" + r.ToString();
                                row.Attributes.Append(htmlDoc.CreateAttribute("id", id));
                                HtmlNode a = htmlDoc.CreateElement("a");
                                a.Attributes.Append(htmlDoc.CreateAttribute("data-transition", viziapps_transition_type));
                                if (tableActions["next_page"] != null)
                                {
                                    a.Attributes.Append(htmlDoc.CreateAttribute("href", "#" + tableActions["next_page"].ToString()));
                                }
                                else
                                    a.Attributes.Append(htmlDoc.CreateAttribute("href", "#"));

                                a.Attributes.Append(htmlDoc.CreateAttribute("style", "padding:0;width:100%"));
                                a.InnerHtml = row_html;
                                HtmlNode a_table = a.SelectSingleNode("table");
                                a_table.Attributes["style"].Value = "width:" + (Convert.ToInt16(field_map["width"].ToString())).ToString() + "px";
                                field_index = 0;
                                for (int c = 0; c < n_cols; c++)
                                {
                                    if (table[r, c] != null)
                                    {
                                        switch (field_types[field_index])
                                        {
                                            case "image":
                                                HtmlNode td_img = a.SelectSingleNode("table/tr/td");
                                                td_img.Attributes["name"].Value = field_name_list[field_index];
                                                HtmlNode img = td_img.SelectSingleNode("img");
                                                img.Attributes["src"].Value =  table[r, c]; 
                                                break;
                                            case "text":
                                                HtmlNode td  = null;
                                                int td_width_adjust = 30;
                                                switch (field_map["table_type"].ToString())
                                                {
                                                    case "1texthidden":
                                                    case "1text":
                                                        td = a.SelectSingleNode("table/tr/td");
                                                        break;
                                                    case "2textshidden":
                                                    case "2texts":
                                                        if (c == 0)
                                                            td = a.SelectSingleNode("table/tr/td");
                                                        else
                                                        {
                                                            td = a.SelectSingleNode("table/tr").NextSibling;
                                                            td = td.SelectSingleNode("td");
                                                        }
                                                        break;
                                                    case "image1texthidden":
                                                    case "image1text":
                                                        td = a.SelectSingleNode("table/tr/td").NextSibling;
                                                        td_width_adjust = 120;
                                                        break;
                                                    case "image2textshidden":
                                                    case "image2texts":
                                                        td_width_adjust = 120;
                                                        if (c == 1)
                                                            td = a.SelectSingleNode("table/tr/td").NextSibling;
                                                        else
                                                        {
                                                            td = a.SelectSingleNode("table/tr").NextSibling;
                                                            td = td.SelectSingleNode("td");
                                                        }
                                                        break;
                                                }
                                                
                                                td.Attributes["name"].Value =  field_name_list[field_index];
                                                HtmlNode td_div = td.SelectSingleNode("div");
                                                td_div.Attributes.Append(htmlDoc.CreateAttribute("style", "width:" + (Convert.ToInt16(field_map["width"].ToString()) - td_width_adjust).ToString() + "px"));
                                                td_div.InnerHtml = table[r, c];
                                                break;
                                            case "hidden":
                                                HtmlNode hidden = a.SelectSingleNode("table").NextSibling;
                                                hidden.Attributes["name"].Value = field_name_list[field_index];
                                                hidden.Attributes["value"].Value = table[r, c];
                                                break;
                                        }
                                    }
                                    field_index++;
                                }
                                row.AppendChild(a);
                                table_node.AppendChild(row);
                            }

                            break;
                        case "alert":
                            //<a href="foo.html" data-rel="dialog">Open dialog</a>
                            field_node.Attributes.Append(htmlDoc.CreateAttribute("data-role", "button"));
                            field_node.Attributes.Append(htmlDoc.CreateAttribute("data-rel", "dialog"));
                            field_node.Attributes.Append(htmlDoc.CreateAttribute("data-transition", "pop"));
                            field_node.Attributes.Append(htmlDoc.CreateAttribute("href", "#"+field_map["id"].ToString() + "_page"));
                            field_node.Attributes["style"].Value += "display:none";

                            HtmlNode alert_page_node = htmlDoc.CreateElement("div");
                            alert_page_node.Attributes.Append(htmlDoc.CreateAttribute("data-role", "page"));
                            alert_page_node.Attributes.Append(htmlDoc.CreateAttribute("field_type", "alert"));
                            alert_page_node.Attributes.Append(htmlDoc.CreateAttribute("id", field_map["id"].ToString() + "_page"));

                            HtmlNode alert_header = htmlDoc.CreateElement("div");
                            alert_header.Attributes.Append(htmlDoc.CreateAttribute("data-role", "header"));
                            alert_header.Attributes.Append(htmlDoc.CreateAttribute("data-theme", "d"));
      
                            HtmlNode alert_title = htmlDoc.CreateElement("h1");
                            alert_title.Attributes.Append(htmlDoc.CreateAttribute("id", field_map["id"].ToString() + "_title"));
                            alert_title.InnerHtml = "Alert";
                            alert_header.AppendChild(alert_title);

                            alert_page_node.AppendChild(alert_header);

                            HtmlNode alert_content_node = htmlDoc.CreateElement("div");
                            alert_content_node.Attributes.Append(htmlDoc.CreateAttribute("data-role", "content"));
                            alert_content_node.Attributes.Append(htmlDoc.CreateAttribute("data-theme", "c"));

                            HtmlNode dialog_message = htmlDoc.CreateElement("p");
                            dialog_message.Attributes.Append(htmlDoc.CreateAttribute("id", field_map["id"].ToString() + "_message"));
                            dialog_message.InnerHtml = "Message";
                            alert_content_node.AppendChild(dialog_message);

                            alert_page_node.AppendChild(alert_content_node);

                            dialogs_in_page.Add(alert_page_node);
                            break;
                        case "hidden_field":
                            field_node.Attributes.Append(htmlDoc.CreateAttribute("type", "hidden"));
                            break;
                        case "html_panel":
                            field_node.Attributes.Append(htmlDoc.CreateAttribute("data-role", "none"));
                            field_node.Attributes["style"].Value += "margin: 0 0 0 0;overflow-x:auto;overflow-y:hidden;background-color:#ffffff;";
                            HtmlNode panel_nav = htmlDoc.CreateElement("nav");
                            field_node.AppendChild(panel_nav); 
                            HtmlNode container_node = htmlDoc.CreateElement("div");
                            container_node.Attributes.Append(htmlDoc.CreateAttribute("style", "width:100%;height:100%;"));
                            container_node.InnerHtml = HttpUtility.HtmlDecode(field_map["html"].ToString());
                            panel_nav.AppendChild(container_node);
                            break;
                        case "web_view":
                            field_node.Attributes["style"].Value += "margin: 0 0 0 0;overflow-x:scroll;overflow-y:scroll";
                            HtmlNode iframe_node = htmlDoc.CreateElement("iframe");
                            field_node.AppendChild(iframe_node);
                            if(field_map["url"] == null)
                                iframe_node.Attributes.Append(htmlDoc.CreateAttribute("src",""));
                            else
                                 iframe_node.Attributes.Append(htmlDoc.CreateAttribute("src", field_map["url"].ToString()));
                            iframe_node.Attributes.Append(htmlDoc.CreateAttribute("width", "100%"));
                            iframe_node.Attributes.Append(htmlDoc.CreateAttribute("height", "100%"));
                            iframe_node.Attributes.Append(htmlDoc.CreateAttribute("style", "border-width:0;overflow-x:scroll;overflow-y:scroll"));
                            break;
                        case "gps":
                            break;
                        case "photo":
                            break;
                    }
                }

                if(isFirstPage){
                    isFirstPage = false;
                    if ((bool)State["IsProduction"] == false &&  State["SelectedAppType"].ToString() == Constants.HYBRID_APP_TYPE)
                    {
                        //add login button
                        HtmlNode login_button_node = htmlDoc.CreateElement("div");
                        int settings_button_left = 280;
                        int settings_button_top = 5;
                        int settings_button_height = 32;
                        if (x_size_factor != 1.0D)
                        {
                            settings_button_left = Convert.ToInt16(292.0D * x_size_factor);
                            settings_button_top = Convert.ToInt16(3.0D * y_size_factor);
                            settings_button_height = Convert.ToInt16(22.0D * y_size_factor);
                        }
                        login_button_node.Attributes.Append(htmlDoc.CreateAttribute("style", "position:absolute;height:" + settings_button_height.ToString() + "px;left:" + settings_button_left.ToString() + "px;top:" + settings_button_top.ToString() + "px;z-index:50000;width:" + settings_button_height.ToString() + "px;"));
                        login_button_node.Attributes.Append(htmlDoc.CreateAttribute("id", "viziapps_login_button"));
                        HtmlNode image_button_node = htmlDoc.CreateElement("img");
                        image_button_node.Attributes.Append(htmlDoc.CreateAttribute("src", "http://viziapps.s3-website-us-east-1.amazonaws.com/apps/viziapps_settings_button.png"));
                        image_button_node.Attributes.Append(htmlDoc.CreateAttribute("width", "100%"));
                        image_button_node.Attributes.Append(htmlDoc.CreateAttribute("height", "100%"));
                        login_button_node.AppendChild(image_button_node);
                        content_node.AppendChild(login_button_node);
                    }
                }
                body_node.AppendChild(html_page_node);
                if (dialogs_in_page.Count > 0)
                {
                    foreach (HtmlNode dialog_node in dialogs_in_page)
                    {
                        body_node.AppendChild(dialog_node);
                    }
                }

                //Set data source operations
                XmlNode data_sources = page_node.SelectSingleNode("data_sources");
                if (data_sources == null)
                    continue;

                XmlNodeList data_source_list = data_sources.SelectNodes("data_source");
                int data_source_index = 0;
                string page_name = page_node.SelectSingleNode("name").InnerText;
                foreach (XmlNode data_source in data_source_list)
                {
                    //get data source type
                    XmlNode data_source_id = data_source.SelectSingleNode("data_source_id");
                    XmlNode app_data_source_id = xmlDoc.SelectSingleNode("//application/data_sources/data_source/data_source_id[.='" + data_source_id.InnerText + "']");
                    if (app_data_source_id != null)
                    {
                        XmlNode data_source_type = app_data_source_id.ParentNode.SelectSingleNode("data_source_type");
                        string connection_string = app_data_source_id.ParentNode.SelectSingleNode("data_source_configuration/connection_string").InnerText;
                        if (data_source_type.InnerText == "google_spreadsheet")
                        {
                            XmlNode event_node = data_source.SelectSingleNode("event");
                            if (event_node == null)
                                continue;
                            XmlNode data_source_event_type = event_node.SelectSingleNode("data_source_event_type");
                            string sql_command_list_name = "data_source_operations_" + page_name + "_" + data_source_index.ToString();
                            if (data_source_event_type.InnerText == "page")
                            {
                                XmlNodeList sql_command_list = event_node.SelectNodes("data_source_operations/sql_commands/sql_command");
                                customInitScript.Append(SetDataSourceSqlCommands(xmlDoc, page_name, data_source_index, sql_command_list));

                                StringBuilder spreadsheet_actions = new StringBuilder();
                                if (page_name == first_page_name) //this binds to before the first page is shown
                                {
                                    spreadsheet_actions.Append("$(window).load(function() {\n");
                                    spreadsheet_actions.Append("\tdoGoogleDocsInterface(\"" + connection_string + "\"," + sql_command_list_name + ");\n");
                                    spreadsheet_actions.Append("});\n");
                                    functions.Append(spreadsheet_actions.ToString());
                                }

                                //this bind to before the page is shown after first time
                                else
                                {
                                    spreadsheet_actions.Append("$('#" + page_name + "').live('pagebeforeshow', function (event, ui) {\n");
                                    spreadsheet_actions.Append("\tdoGoogleDocsInterface(\"" + connection_string + "\"," + sql_command_list_name + ");\n");
                                    spreadsheet_actions.Append("});\n");
                                    functions.Append(spreadsheet_actions.ToString());
                                }
                            }
                            else if (data_source_event_type.InnerText == "field")
                            {
                                XmlNode data_source_event_field = event_node.SelectSingleNode("data_source_event_field");
                                XmlNodeList sql_command_list = event_node.SelectNodes("data_source_operations/sql_commands/sql_command");
                                customInitScript.Append(SetDataSourceSqlCommands(xmlDoc, page_name, data_source_index, sql_command_list));

                                StringBuilder spreadsheet_actions = new StringBuilder("\t$('#" + data_source_event_field.InnerText + "').bind('tap',function(event){\n");
                                spreadsheet_actions.Append("\tdoGoogleDocsInterface(\"" + connection_string + "\"," + sql_command_list_name + ");\n");
                                spreadsheet_actions.Append("\tevent.stopPropagation();\n");
                                spreadsheet_actions.Append("});\n");

                                functions.Append(spreadsheet_actions.ToString());
                            }
                        }
                    }
                    data_source_index++;
                }
            }

            //insert all custom scripts
            HtmlNodeCollection script_nodes = root.SelectNodes("//script");
            foreach (HtmlNode script_node in script_nodes)
            {
                if (script_node.InnerText.Contains("CUSTOM_VIZIAPPS_INIT"))
                {
                    script_node.InnerHtml = script_node.InnerHtml.Replace("CUSTOM_VIZIAPPS_INIT", customInitScript.ToString());                   
                }
                else if (script_node.InnerText.Contains("CUSTOM_VIZIAPPS_FUNCTIONS"))
                {
                    script_node.InnerHtml = script_node.InnerHtml.Replace("CUSTOM_VIZIAPPS_FUNCTIONS", functions.ToString());                   
                }
            }

            return htmlDoc.DocumentNode.WriteContentTo();
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
    private string SetDataSourceSqlCommands(XmlDocument doc,string page_name, int data_source_index,  XmlNodeList sql_command_list)
    {
        DataSources DS = new DataSources();
        string sql_command_list_name = "data_source_operations_" + page_name + "_" + data_source_index.ToString();
        StringBuilder script = new StringBuilder("var " + sql_command_list_name + " = new Array(" + sql_command_list .Count.ToString() + ");\n");
        int index = 0;
        foreach (XmlNode sql_command in sql_command_list)
        {
            string hashtable_name = sql_command_list_name + "_" + index.ToString();
            script.Append("var " + hashtable_name + " = new Hashtable();\n");
            script = SetJavascriptHashtable(script, hashtable_name, DS.ParseSqlCommand(doc,sql_command));
            script.Append(sql_command_list_name + "[" + index.ToString() + "] = " + hashtable_name + ";\n");
            index++;
        }
        return script.ToString();
    }
    private StringBuilder SetJavascriptHashtable(StringBuilder script, string hashtable_name, Hashtable Command)
    {
        foreach (string key in Command.Keys)
        {
            object o = Command[key];
            if(o.GetType() == typeof(string))
                script.Append(hashtable_name + ".put('" + key + "','" + Command[key].ToString() + "');\n");
            else if (o.GetType() == typeof(int))
                script.Append(hashtable_name + ".put('" + key + "','" + ((int)Command[key]).ToString()+"');\n");
            else if (o.GetType() == typeof(System.Collections.Hashtable))
            {
                Hashtable subTable = (Hashtable)Command[key];
                string sub_table_name = hashtable_name + "_" + key + "_hashtable";
                script.Append("var " + sub_table_name + " = new Hashtable();\n");
                script = SetJavascriptHashtable(script, sub_table_name, subTable);
                script.Append(hashtable_name + ".put('" + key + "'," + sub_table_name + ");\n");
            }
            else if (o.GetType() == typeof(System.Collections.ArrayList))
            {
                ArrayList array = (ArrayList)Command[key];
                if (array.Count == 0)
                    continue;
                string array_name = hashtable_name + "_" + key + "_array";
                script.Append("var " + array_name + " = new Array(" + array.Count.ToString() + ");\n");
                int index = 0;
                foreach (Hashtable item in array)
                {
                    string sub_table_name = array_name + "_hashtable_" + index.ToString();
                    script.Append("var " + sub_table_name + " = new Hashtable();\n");
                    script = SetJavascriptHashtable(script, sub_table_name, item);
                    script.Append(array_name + "[" + index.ToString() + "] = " + sub_table_name + ";\n");
                    index++;
                }
                script.Append(hashtable_name + ".put('" + key + "'," +array_name  + ");\n");
            }
        }
        return script;
    }
    public void SetActions(XmlNode page_node, Hashtable field_map, Hashtable buttonAction, StringBuilder functions)
    {
        if (field_map["compute"] == null && buttonAction["next_page"] == null && buttonAction["previous_page"] == null)
            return;

        StringBuilder button_actions = new StringBuilder("\t$('#" + field_map["id"].ToString() + "').bind('tap',function(event){\n");

        if (buttonAction["previous_page"] != null)//overlapping buttons 2 adjacent pages will both trigger events on both pages. JQM bug
        {
            button_actions.Append("\t$.mobile.changePage( previous_page_name, { transition: viziapps_transition_type} );\n");
        }
        if (field_map["compute"] != null)
        {
            button_actions.Append("\tdoFieldCompute(this,'" + field_map["compute"].ToString() + "');\n");
        }

        button_actions.Append("\t\tevent.stopPropagation();\n");
        button_actions.Append("\t});\n");
 
        functions.Append(button_actions.ToString());
    }
    public void SetTableRowActions(XmlNode page_node, String id, Hashtable field_map, Hashtable tableAction, StringBuilder functions)
    {
        if (field_map["compute"] == null && tableAction.Count == 0)
            return;

        StringBuilder table_actions = new StringBuilder("\t$('#" + id + "').bind('tap',function(event){\n");
        table_actions.Append("storeTableSelectedIndex(event);\n");
        if (tableAction["page_from_field"] != null)
        {
            table_actions.Append("gotoPageFromTableField('" + tableAction["page_from_field"].ToString() + "');\n");
         }
        if (field_map["compute"] != null)
        {
            table_actions.Append("doFieldCompute(this,'" + field_map["compute"].ToString() + "');\n");
        }
        table_actions.Append("});\n");
        functions.Append(table_actions.ToString());
    }
    public void SetCommonHtmlAttributes(HtmlDocument htmlDoc, HtmlNode field_node, Hashtable field_map,  double x_size_factor ,  double y_size_factor , string field_type)
    {       
        XmlUtil x_util = new XmlUtil();
        //init style
        HtmlAttribute style = htmlDoc.CreateAttribute("style", "position:absolute;");
        field_node.Attributes.Append(style);
        HtmlAttribute icon_field = null;

        //set default z-index
        if (!field_map.ContainsKey("z_index"))
            field_map["z_index"] = "50";
        if (field_type == "text_field" || field_type == "text_area" || field_type == "label" || field_type == "button")
        {
            if (!field_map.ContainsKey("font_style"))
                field_map["font_style"] = "normal";
            if (!field_map.ContainsKey("font_weight"))
                field_map["font_weight"] = "normal";
            if (!field_map.ContainsKey("text_decoration"))
                field_map["text_decoration"] = "none";
            /*if (field_type == "text_area")
            {
                //add 20 px to height and width of top node and then subtract 20 px for subnode of textarea later in SetTextHtmlAttributes
                field_map["height"] = Convert.ToInt32(field_map["height"].ToString()) + 20;
                field_map["width"] = Convert.ToInt32(field_map["width"].ToString()) + 20;
            }*/
        }
        //process buttons specially
        if (field_type == "button")
        {
            int height = Convert.ToInt32(field_map["height"].ToString());
           // int width = Convert.ToInt32(field_map["width"].ToString());
            //field_map["width"] = (width + 20).ToString();
            int top = Convert.ToInt32(field_map["top"].ToString());
            //int left = Convert.ToInt32(field_map["left"].ToString());
            //field_map["left"] = (left - 10).ToString();
            if (height < 30)
            {
                field_map["height"] = "30";
                top -= (30-height)/2;
                field_map["top"] = top.ToString();
            }
        }
        else if (field_type == "text_field")
        {
            int height = Convert.ToInt32(field_map["height"].ToString());
            int top = Convert.ToInt32(field_map["top"].ToString());
            if (height < 40)
            {
                field_map["height"] = "40";
                top -= (40 - height) / 2;
                field_map["top"] = top.ToString();
            }
        }

        foreach (string key in field_map.Keys)
        {
            switch (key)
            {
                case "icon_field":
                    if (field_node.Attributes["icon_field"] == null)
                    {
                        icon_field = htmlDoc.CreateAttribute("icon_field", "field:" + field_map[key].ToString() + ";");
                        field_node.Attributes.Append(icon_field);
                    }
                    else
                        field_node.Attributes["icon_field"].Value += "field:" + field_map[key].ToString() + ";";
                    break;
                case "icon_width":
                    if (field_node.Attributes["icon_field"] == null)
                    {
                        icon_field = htmlDoc.CreateAttribute("icon_field", "width:" + field_map[key].ToString() + ";");
                        field_node.Attributes.Append(icon_field);
                    }
                    else
                        field_node.Attributes["icon_field"].Value += "width:" + field_map[key].ToString() + ";";
                    break;
                case "icon_height":
                    if (field_node.Attributes["icon_field"] == null)
                    {
                        icon_field = htmlDoc.CreateAttribute("icon_field", "height:" + field_map[key].ToString() + ";");
                        field_node.Attributes.Append(icon_field);
                    }
                    else
                        field_node.Attributes["icon_field"].Value += "height:" + field_map[key].ToString() + ";";
                    break;
                case "url":
                case "alt":
                case "title":
                case "id":
                case "class":
                case "src":
                    HtmlAttribute attr = htmlDoc.CreateAttribute(key, field_map[key].ToString());
                    field_node.Attributes.Append(attr);
                    break;
                case "value":
                    HtmlAttribute val_attr = htmlDoc.CreateAttribute(key, HttpUtility.HtmlAttributeEncode(x_util.UnescapeXml(field_map[key].ToString())));
                    field_node.Attributes.Append(val_attr);
                    break;
                case "top":
                    int top = Convert.ToInt32(field_map[key].ToString());                    
                    if (y_size_factor == 1.0D)
                        field_node.Attributes["style"].Value += key + ":" + top.ToString() + "px;";
                    else
                    {
                        double y = Math.Round(Convert.ToDouble(top) * y_size_factor);
                        field_node.Attributes["style"].Value += key + ":" + y.ToString() + "px;";
                    }
                    break;
                case "left":
                    int left = Convert.ToInt32(field_map[key].ToString());
                    if (x_size_factor == 1.0D)
                        field_node.Attributes["style"].Value += key + ":" + left.ToString() + "px;";
                    else
                    {
                        double x = Math.Round(Convert.ToDouble(left) * x_size_factor);
                        field_node.Attributes["style"].Value += key + ":" + x.ToString() + "px;";
                    }
                    break;
                case "height":
                    int height = Convert.ToInt32(field_map[key].ToString());
                    if (field_type == "text_field")
                    {
                        height -= 16;
                    }
                    if (y_size_factor != 1.0D)
                    {
                        if (field_type == "image" || field_type == "image_button")
                        {
                            //maintain aspect ratios for images
                            height = Convert.ToInt32(Math.Round(Convert.ToDouble(height) * x_size_factor));
                        }
                        else
                        {
                            height = Convert.ToInt32(Math.Round(Convert.ToDouble(height) * y_size_factor));
                        }
                    }
  
                     field_node.Attributes["style"].Value += key + ":" + height.ToString() + "px;";
                     break;
                  
                 case "width":
                    int width = Convert.ToInt32(field_map[key].ToString());
                    if (x_size_factor != 1.0D)
                    {
                         width = Convert.ToInt32(Math.Round(Convert.ToDouble(width) * x_size_factor));
                    }
                    if (field_type == "slider")
                    {
                        width = Math.Max(width, Convert.ToInt32(150 * x_size_factor));
                        field_node.Attributes["style"].Value += key + ":" + width.ToString() + "px;";
                    }
                    else
                        field_node.Attributes["style"].Value += key + ":" + width.ToString() + "px;";                  
                    break;
                 case "font_size":
                    int font_size = Convert.ToInt32(field_map[key].ToString());
                    if (y_size_factor != 1.0D)
                    {
                        font_size = Convert.ToInt32(Math.Round(Convert.ToDouble(font_size) * y_size_factor));
                    }
                    field_node.Attributes["style"].Value += key.Replace("_", "-") + ":" + font_size.ToString() + "px;";
                    break;
                 case "z_index":
                    int z_index = Math.Min(Convert.ToInt32(field_map[key].ToString()), 99);
                    field_node.Attributes["style"].Value += key.Replace("_", "-") + ":" + z_index.ToString() + ";";
                    break;
                case "color":
                case "font_style":
                case "font_weight":
                case "text_decoration":
                    field_node.Attributes["style"].Value += key.Replace("_", "-") + ":" + field_map[key].ToString() + ";";
                    break;
                case "font_family":
                    field_node.Attributes["style"].Value += key.Replace("_", "-") + ":" + field_map[key].ToString().ToLower().Replace("tahoma", "helvetica").Replace("verdana", "helvetica").Replace("calibri", "helvetica") + ";";
                    break;
            }
        }
    }
    public void SetTextHtmlAttributes(HtmlNode field_node, Hashtable field_map)
    {
        if (!field_map.ContainsKey("font_style"))
            field_map["font_style"] = "normal";
        if (!field_map.ContainsKey("font_weight"))
            field_map["font_weight"] = "normal";
        if (!field_map.ContainsKey("text_decoration"))
            field_map["text_decoration"] = "none";

        foreach (string key in field_map.Keys)
        {
            switch (key)
            {
                case "font_size":
                    field_node.Attributes["style"].Value += key.Replace("_", "-") + ":" + field_map[key].ToString() + "px;";
                    break;
                case "color":
                case "font_style":
                case "font_weight":
                case "text_decoration":
                    field_node.Attributes["style"].Value += key.Replace("_", "-") + ":" + field_map[key].ToString() + ";";
                    break;
                case "font_family":
                    field_node.Attributes["style"].Value += key.Replace("_", "-") + ":" + field_map[key].ToString().ToLower().Replace("tahoma", "helvetica").Replace("verdana", "helvetica").Replace("calibri", "helvetica") + ";"; 
                    break;
                case "height":
                    int height = Convert.ToInt32(field_map[key].ToString()) - 20;
                    field_node.Attributes["style"].Value += key + ":" + height.ToString() + "px;";
                     break;
                case "width":
                    int width = Convert.ToInt32(field_map[key].ToString()) - 20;
                    field_node.Attributes["style"].Value += key + ":" + width.ToString() + "px;";                    
                    break;
            }
        }
    }
    private Hashtable GetAction(XmlNode field)
    {
        Hashtable actionTable = new Hashtable();
        XmlNode submit = field.SelectSingleNode("submit");
        if (submit != null)
        {
            if(submit.FirstChild == null)
                return actionTable;
            XmlNode action_node = submit.FirstChild.NextSibling;
            if (action_node == null)
                return actionTable;
            string action = action_node.Name;
            switch (action)
            {
                case "next_page":
                    actionTable[action] = action_node.SelectSingleNode("page").InnerText;
                    break;
                case "page_from_field":
                    actionTable[action] = action_node.SelectSingleNode("field").InnerText;
                    break;
                default:
                   actionTable[action] = true;
                   break;                  
            }
        }
        return actionTable;
    }
}