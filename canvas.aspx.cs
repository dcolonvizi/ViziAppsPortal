using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class canvas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        if (util.CheckSessionTimeout(State,Response,"Default.aspx")) return;

        if (!IsPostBack)
        {
 
            XmlUtil x_util = new XmlUtil();
            if ( State["PageHtml"] == null) //get html if there is a selected app
                x_util.GetStagingAppXml(State);

            if ( State["PageHtml"] == null) //no selected app
                return;

            int left = 0;
            if ( State["SelectedDeviceView"] != null &&  
                (State["SelectedDeviceView"].ToString() == Constants.IPAD ||
                 State["SelectedDeviceView"].ToString() == Constants.ANDROID_TABLET))
            {
                if (State["SelectedAppType"].ToString() == Constants.WEB_APP_TYPE || State["SelectedAppType"].ToString() == Constants.HYBRID_APP_TYPE)
                {
                    if ( State["BackgroundColor"] == null)
                         State["BackgroundColor"] = "#cccccc";

                    string background_color_div_prefix = null;
                    if (State["SelectedDeviceView"].ToString() == Constants.IPAD)
                        background_color_div_prefix = "<div style=\"border:0px;width:" + Constants.IPAD_SPLASH_PORTRAIT_WIDTH_S + "px;height:" + Constants.IPAD_SPLASH_PORTRAIT_HEIGHT_S + "px;vertical-align:top;background-color:" + State["BackgroundColor"].ToString() + "\" >";
                    if (State["SelectedDeviceView"].ToString() == Constants.ANDROID_TABLET)
                        background_color_div_prefix = "<div style=\"border:0px;width:" + Constants.ANDROID_TABLET_SPLASH_PORTRAIT_WIDTH_S + "px;height:" + Constants.ANDROID_TABLET_SPLASH_PORTRAIT_HEIGHT_S + "px;vertical-align:top;background-color:" + State["BackgroundColor"].ToString() + "\" >";
                    
                    string background_color_div_suffix = "</div>";
                   html_content.Text = background_color_div_prefix +  State["PageHtml"].ToString() + background_color_div_suffix;
                }
                else //for native type
                {
                    string background = "<img id=\"background_image\" src=\"" + State["BackgroundImageUrl"].ToString() + "\" style=\"position:absolute;top:0px;left:0px;\"/>";
                    if (( State["SelectedAppPage"] == null || x_util.IsFirstAppPage(State,  State["SelectedAppPage"].ToString())) )
                    {
                        left = (State["SelectedDeviceView"].ToString() == Constants.IPAD) ? 731 : 763;
                        background += "<img src=\"images/editor_images/settings_button.png\" style=\"position:absolute;top:5px;left:" + left.ToString() + "px\"/>";
                    }
                    html_content.Text = background +  State["PageHtml"].ToString();
                }
            }
            else{
                left = 283;                
                 if ( State["BackgroundImageUrl"] == null)
                      State["BackgroundImageUrl"] = "https://s3.amazonaws.com/MobiFlexImages/apps/images/backgrounds/standard_w_header_iphone.jpg";

                  string background = "<img id=\"background_image\" src=\"" + State["BackgroundImageUrl"].ToString() + "\" style=\"position:absolute;top:0px;left:0px;height:100%;width:100%\"/>";
                 if ((State["SelectedAppPage"] == null || x_util.IsFirstAppPage(State, State["SelectedAppPage"].ToString())) && State["SelectedAppType"].ToString() == Constants.NATIVE_APP_TYPE)
                 {
                     background += "<img src=\"images/editor_images/settings_button.png\" style=\"position:absolute;top:5px;left:" + left.ToString() + "px\"/>";
                 }

                 html_content.Text = background +  State["PageHtml"].ToString();
                  State["BackgroundHtml"] = background;
            }  
        }
    }
}