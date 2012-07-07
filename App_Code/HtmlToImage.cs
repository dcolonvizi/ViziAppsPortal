using System;
using System.Data;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Text;
using System.Threading;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

/// <summary>
/// Summary description for HtmlToImage
/// </summary>
public class HtmlToImage
{
	public HtmlToImage()
	{
	}
    public string ConvertToImageLink(Hashtable State,string page_name, string html)
    { 
        //fill in link prefixes where they are left out
        int start = 0;
        string search = " src=\"";
        int end = 0;
        StringBuilder sb_html = new StringBuilder();

        if (State["BackgroundColor"] == null && State["BackgroundHtml"] != null)
        {
            string html_and_back = State["BackgroundHtml"].ToString() + html;
            sb_html.Append(html_and_back);
        }
        else
        {
            sb_html.Append(html);
        }

        do
        {
            end = sb_html.ToString().ToLower().IndexOf(search, start);
            if (end > 0)
            {
                end += search.Length;
                start = end ;
                if (sb_html.ToString().Substring(start, 4) != "http")
                {
                    sb_html.Insert(start,State["PublicViziAppsUrl"].ToString() + "/");
                }
            }
        }while(end >=0);

        //filter for table images
        start = 0;
        search = "url(";
        do
        {
            end = sb_html.ToString().ToLower().IndexOf(search, start);
            if (end > 0)
            {
                end += search.Length ;
                string quote_type = "none";
                if (sb_html[end] == '&')
                {
                    start = end + 6;
                    quote_type = "encoded";
                }
                else if (sb_html[end] == '"')
                {
                    start = end + 1;
                    quote_type = "quote";
                }
                else 
                    start = end;

                if (sb_html.ToString().Substring(start, 4) != "http")
                {
                    sb_html.Insert(start, State["PublicViziAppsUrl"].ToString() + "/");
                }
                //get image url
                switch (quote_type)
                {
                    case "none":
                        end = sb_html.ToString().IndexOf(")", start);
                        break;
                    case "encoded":
                        end = sb_html.ToString().IndexOf("&quot;", start);
                        break;
                    case "quote":
                        end = sb_html.ToString().IndexOf("\"", start);
                        break;
                }

                string image_url = sb_html.ToString().Substring(start, end - start);
                start = end + 1;
                end = sb_html.ToString().IndexOf(">", start);
                start = end + 1;
                sb_html.Insert(start, " <img src=\"" + image_url + "\"/>");
                sb_html.Insert(end, " style=\"overflow:hidden\" "); 
            }
        } while (end >= 0);

        Util util = new Util();
        string ApplicationHomePath = State["ApplicationHomePath"].ToString();
        string media_home_path = ApplicationHomePath + @"\customer_media";
        util.CheckDirectory(media_home_path);
        string customer_media_home_path = media_home_path + @"\" + State["Username"].ToString();
        util.CheckDirectory(customer_media_home_path);
        string file_name = State["SelectedApp"].ToString() + "." + page_name + ".jpg";
        file_name = file_name.Replace(" ","_");
        string save_file_path = customer_media_home_path + @"\" + file_name;

        int browserWidth = Constants.IPHONE_DISPLAY_WIDTH;
        int browserHeight = Convert.ToInt32(Constants.IPHONE_SCROLL_HEIGHT_S);
        int thumbnailWidth = browserWidth;
        int thumbnailHeight = browserHeight;
        if (State["SelectedDeviceType"].ToString() == Constants.IPAD)
        {
            browserWidth = Constants.IPAD_DISPLAY_WIDTH;
            browserHeight = Constants.IPAD_DISPLAY_HEIGHT;
            thumbnailWidth = browserWidth;
            thumbnailHeight = browserHeight;
        }
        else if (State["SelectedDeviceType"].ToString() == Constants.ANDROID_TABLET)
        {
            browserWidth = Constants.ANDROID_TABLET_DISPLAY_WIDTH;
            browserHeight = Constants.ANDROID_TABLET_DISPLAY_HEIGHT;
            thumbnailWidth = browserWidth;
            thumbnailHeight = browserHeight;
        }
        string whole_html = State["CanvasHtml"].ToString().Replace("<REPLACE>", sb_html.ToString());

        string temp_store_path = customer_media_home_path + @"\canvas.htm" ;
        File.WriteAllText(temp_store_path, whole_html.Replace("iframe","div"));

        string siteurl = "file:///" + temp_store_path;
        System.Drawing.Image img =
                (System.Drawing.Image)PAB.WebControls.WebSiteThumbnail.GetSiteThumbnail(siteurl,
                          browserWidth, browserHeight, thumbnailWidth, thumbnailHeight, save_file_path);

        if (!File.Exists(save_file_path))
            return null;

        try
        {
            if (File.Exists(temp_store_path))
                File.Delete(temp_store_path);
        }
        catch{
            //file access issues
        };

        string url = null;
        try{
            AmazonS3 s3 = new AmazonS3();
            url = s3.UploadFile(State, file_name, save_file_path);
            if (!url.StartsWith("http"))
                return null;

            if (File.Exists(save_file_path))
                File.Delete(save_file_path);
        }
        catch{
            //file access issues
        };
 
        return url;        
    }

    public void DeleteImageFromUrl(Hashtable State, string url)
    {
        AmazonS3 s3 = new AmazonS3();
        s3.DeleteImageFile(State, url);
    }   
}