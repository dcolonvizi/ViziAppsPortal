using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Constants
/// </summary>
public class Constants
{
    public const string WEB_APP_TYPE = "web";
    public const string NATIVE_APP_TYPE = "native";
    public const string HYBRID_APP_TYPE = "hybrid";
    public const string WEB_APP_TEST_SUFFIX = "~test";

    public const string IPHONE = "iphone";
    public const string IPHONE_LABEL = "iPhone";
    public const int IPHONE_DISPLAY_WIDTH = 320;
    public const double IPHONE_DISPLAY_WIDTH_D = 320.0D;
    public const string IPHONE_DISPLAY_WIDTH_S = "320";
    public const int IPHONE_DISPLAY_HEIGHT = 480;
    public const double IPHONE_DISPLAY_HEIGHT_D = 480;
    public const string IPHONE_DISPLAY_HEIGHT_S = "480";
    public const string IPHONE_SCROLL_HEIGHT_S = "460";
    public const string IPHONE_SCROLL_WIDTH_S = "320";
    public const double IPHONE_SCROLL_HEIGHT_D = 460.0D;
    public const int    IPHONE_SCROLL_HEIGHT = 460;
    public const double IPHONE_SCROLL_WIDTH_D = 320.0D;
    public const string IPHONE_LANDSCAPE_WIDTH_FACTOR = "1.5";
    public const string IPHONE_LANDSCAPE_HEIGHT_FACTOR = "1.0";

    public const string IPAD = "ipad";
    public const string IPAD_LABEL = "iPad";
    public const int IPAD_DISPLAY_WIDTH = 768;
    public const double IPAD_DISPLAY_WIDTH_D = 768.0D;
    public const string IPAD_DISPLAY_WIDTH_S = "768";
    public const int IPAD_DISPLAY_HEIGHT = 1024;
    public const double IPAD_DISPLAY_HEIGHT_D = 1024.0D;
    public const string IPAD_DISPLAY_HEIGHT_S = "1024";
    public const string IPAD_SPLASH_PORTRAIT_HEIGHT_S = "1004";
    public const string IPAD_SPLASH_PORTRAIT_WEB_HEIGHT_S = "948";
    public const string IPAD_SPLASH_PORTRAIT_WIDTH_S = "768";
    public const string IPAD_SPLASH_LANDSCAPE_HEIGHT_S = "748";
    public const string IPAD_SPLASH_LANDSCAPE_WIDTH_S = "1024";
    public const string IPAD_SCROLL_HEIGHT_S = "1004";
    public const string IPAD_SCROLL_WIDTH_S = "768";
    public const double IPAD_SCROLL_HEIGHT_D = 1004.0D;
    public const int    IPAD_SCROLL_HEIGHT = 1004;
    public const double IPAD_SCROLL_WIDTH_D = 768.0D;
    public const string IPAD_LANDSCAPE_WIDTH_FACTOR = "1.3333333";
    public const string IPAD_LANDSCAPE_HEIGHT_FACTOR = "0.74501992";

    public const string ANDROID_PHONE = "android_phone";
    public const string ANDROID_PHONE_LABEL = "Android Phone";
    public const int ANDROID_PHONE_DISPLAY_WIDTH = 320;
    public const double ANDROID_PHONE_DISPLAY_WIDTH_D = 320.0D;
    public const string ANDROID_PHONE_DISPLAY_WIDTH_S = "320";
    public const int ANDROID_PHONE_DISPLAY_HEIGHT = 533;
    public const double ANDROID_PHONE_DISPLAY_HEIGHT_D = 533;
    public const string ANDROID_PHONE_DISPLAY_HEIGHT_S = "533";
    public const string ANDROID_PHONE_SCROLL_HEIGHT_S = "508";
    public const string ANDROID_PHONE_SCROLL_WIDTH_S = "320";
    public const double ANDROID_PHONE_SCROLL_HEIGHT_D = 508.0D;
    public const int    ANDROID_PHONE_SCROLL_HEIGHT = 508;
    public const double ANDROID_PHONE_SCROLL_WIDTH_D = 320.0D;
 
    public const string ANDROID_PHONE_LANDSCAPE_WIDTH_FACTOR = "1.665625";
    public const string ANDROID_PHONE_LANDSCAPE_HEIGHT_FACTOR = "1.0";

    public const string ANDROID_TABLET = "android_tablet";
    public const string ANDROID_TABLET_LABEL = "Android Tablet";
    public const int ANDROID_TABLET_DISPLAY_WIDTH = 800;
    public const double ANDROID_TABLET_DISPLAY_WIDTH_D = 800.0D;
    public const string ANDROID_TABLET_DISPLAY_WIDTH_S = "800";
    public const int ANDROID_TABLET_DISPLAY_HEIGHT = 1280;
    public const double ANDROID_TABLET_DISPLAY_HEIGHT_D = 1280;
    public const string ANDROID_TABLET_DISPLAY_HEIGHT_S = "1280";
    public const string ANDROID_TABLET_LANDSCAPE_WIDTH_FACTOR = "1.6";
    public const string ANDROID_TABLET_LANDSCAPE_HEIGHT_FACTOR = "0.619047619";
    public const string ANDROID_TABLET_SPLASH_PORTRAIT_HEIGHT_S = "1233";
    public const string ANDROID_TABLET_PORTRAIT_WIDTH_S = "800";
    public const string ANDROID_TABLET_SPLASH_PORTRAIT_WIDTH_S = "800";
    public const int ANDROID_TABLET_SCROLL_HEIGHT = 1233;
    public const double ANDROID_TABLET_SCROLL_HEIGHT_D = 1233.0D;
    public const double ANDROID_TABLET_SCROLL_WIDTH_D = 800.0D;


    public const string BLACKBERRY_BOLD = "blackberry_bold";
    public const string BLACKBERRY_BOLD_LABEL = "Blackberry Bold";
    public const int BLACKBERRY_BOLD_DISPLAY_WIDTH = 320;
    public const double BLACKBERRY_BOLD_DISPLAY_WIDTH_D = 320.0D;
    public const string BLACKBERRY_BOLD_DISPLAY_WIDTH_S = "320";
    public const int BLACKBERRY_BOLD_DISPLAY_HEIGHT = 240;
    public const double BLACKBERRY_BOLD_DISPLAY_HEIGHT_D = 240;
    public const string BLACKBERRY_BOLD_DISPLAY_HEIGHT_S = "240";

    public const string BLACKBERRY_TORCH = "blackberry_torch";
    public const string BLACKBERRY_TORCH_LABEL = "Blackberry Torch";
    public const int BLACKBERRY_TORCH_DISPLAY_WIDTH = 320;
    public const double BLACKBERRY_TORCH_DISPLAY_WIDTH_D = 320.0D;
    public const string BLACKBERRY_TORCH_DISPLAY_WIDTH_S = "320";
    public const int BLACKBERRY_TORCH_DISPLAY_HEIGHT = 480;
    public const double BLACKBERRY_TORCH_DISPLAY_HEIGHT_D = 480;
    public const string BLACKBERRY_TORCH_DISPLAY_HEIGHT_S = "480";

    public const int GOOGLE_DOCS_INDEX = 1;
    public const int DATABASE_INDEX = 2;
    public const int WEB_SERVICE_INDEX = 3;

    public const int BLANK_PAGE = 1;
    public const int GOOGLE_DOCS_PAGE = 3;
    public const int DATABASE_PAGE = 2;
    public const int WEB_SERVICE_PAGE = 0;

    public const string one_text = "<table style=\"width:100%\"><tr><td name=\"\" field_type=\"table_field\"><div class=\"viziapps-first-table-text\" ></div></td></tr></table>";
    public const string one_text_hidden = "<table style=\"width:100%\"><tr><td name=\"\" field_type=\"table_field\"><div class=\"viziapps-first-table-text\" ></div></td></tr></table>" + 
        "<input type=\"hidden\" name=\"\" field_type=\"table_field\" value=\"\"/>"    ;
    public const string two_texts = "<table style=\"width:100%\"><tr><td name=\"\" field_type=\"table_field\"><div class=\"viziapps-first-table-text\"></div></td></tr>" +
        "<tr><td name=\"\" field_type=\"table_field\"><div class=\"viziapps-second-table-text\"></div></td></tr></table>";
    public const string two_texts_hidden = "<table style=\"width:100%\"><tr><td name=\"\" field_type=\"table_field\"><div  class=\"viziapps-first-table-text\" ></div></td></tr>" +
        "<tr><td name=\"\" field_type=\"table_field\"><div class=\"viziapps-second-table-text\" ></div></td></tr></table>" +
        "<input type=\"hidden\" name=\"\" field_type=\"table_field\" value=\"\"/>";
    public const string image_1text = "<table style=\"width:100%\"><tr><td style=\"width:80px;height:60px\" name=\"\" field_type=\"table_field\"><img width=\"80\" height=\"60\" style=\"width:auto;height:auto;object-fit: contain;\" alt=\"\" src=\"\"  /></td>" +
        "<td name=\"\" field_type=\"table_field\"><div class=\"viziapps-first-table-text\" ></div></td></tr></table>";
    public const string image_1text_hidden = "<table style=\"width:100%\"><tr><td style=\"width:80px;height:60px\" name=\"\" field_type=\"table_field\"><img width=\"80\" height=\"60\" style=\"width:auto;height:auto;object-fit: contain;\" alt=\"\" src=\"\" /></td>" +
        "<td name=\"\" field_type=\"table_field\"><div class=\"viziapps-first-table-text\"></div></td></tr></table>" +
         "<input type=\"hidden\" name=\"\" field_type=\"table_field\" value=\"\"/>";
    public const string image_2texts = "<table style=\"width:100%\"><tr><td rowspan=\"2\" style=\"width:80px;height:60px\" name=\"\" field_type=\"table_field\"><img width=\"80\" height=\"60\" style=\"width:auto;height:auto;object-fit: contain;\" alt=\"\" src=\"\" /></td>" +
        "<td name=\"\" field_type=\"table_field\"><div class=\"viziapps-first-table-text\"></div></td></tr>" +
        "<tr><td name=\"\" field_type=\"table_field\"><div class=\"viziapps-second-table-text\"></div></td></tr></table>";
    public const string image_2texts_hidden = "<table style=\"width:100%\"><tr><td rowspan=\"2\" style=\"width:80px;height:60px\" name=\"\" field_type=\"table_field\"><img width=\"80\" height=\"60\" style=\"width:auto;height:auto;object-fit: contain;\" alt=\"\" src=\"\" /></td>" +
        "<td name=\"\" field_type=\"table_field\"><div  class=\"viziapps-first-table-text\"></div></td></tr>" +
        "<tr><td name=\"\" field_type=\"table_field\"><div class=\"viziapps-second-table-text\"></div></td></tr></table>" +
        "<input type=\"hidden\" name=\"\" field_type=\"table_field\" value=\"\"/>";


  
    public Constants()
    {
    }
}
