using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Configuration;
using Telerik.Web.UI;

public partial class PublishingFormWebApps : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../../Default.aspx');", true); return; }

        try
        {
            ClearMessages();

            State["ApplicationID"] = util.GetAppID(State);

            App.Text = "Test Web App Name: " +  State["SelectedApp"].ToString();

            if (util.IsAppStoreSubmissionPaid(State,  State["SelectedApp"].ToString()))
            {
                //SubmitForProvisioning.Visible = true;
                PurchaseButton.Visible = false;
             }
            else
            {
                //SubmitForProvisioning.Visible = false;
                PurchaseButton.Visible = true;
                //ProvisioningMessage.Text = "You can fill this form any time, but to submit your app for production, you need to first purchase one of the ViziApps services to submit the app to an app store.";
                PurchaseButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
    "http://stores.homestead.com/MobiFlexStore/StoreFront.bok", 700, 900, false, false, false, true));
            }

            if (!IsPostBack)
            {
                XmlUtil x_util = new XmlUtil();
                //State["SelectedDeviceView"] = 
                State["SelectedDeviceType"] = x_util.GetAppDeviceType(State);
                if (State["SelectedDeviceType"] == null)
                {
                   // State["SelectedDeviceView"] = 
                    State["SelectedDeviceType"] = Constants.IPHONE;
                }

                //check on device type
                switch(State["SelectedDeviceType"].ToString())                   
                {
                     case Constants.IPAD:
                        SplashUploadLabel.Text = "Splash Image ( 768 X 1004 pixels from .jpg file )";
                        ScreenSplashButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                                "ScreenShot.aspx", 1004, 768, false, false, false, true));
                        break;
                     case Constants.ANDROID_TABLET:
                        SplashUploadLabel.Text = "Splash Image ( 800 X 1233 pixels from .jpg file )";
                        ScreenSplashButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                                "ScreenShot.aspx", 1233, 800, false, false, false, true));
                        break;
                     case Constants.IPHONE:
                        SplashUploadLabel.Text = "Splash Image ( 320 X 460 pixels from .jpg file )";
                        ScreenSplashButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                                "ScreenShot.aspx", 460, 320, false, false, false, true));
                       break;
                     case Constants.ANDROID_PHONE:
                        SplashUploadLabel.Text = "Splash Image ( 320 X 508 pixels from .jpg file )";
                         ScreenSplashButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                                "ScreenShot.aspx", 508, 320, false, false, false, true));
                      break;
                }
                SelectedDeviceType.Text = State["SelectedDeviceType"].ToString();

                DB db = new DB();
                StringBuilder b_sql = new StringBuilder("SELECT * FROM applications ");
                b_sql.Append("WHERE application_name='" +  State["SelectedApp"].ToString() + "'");
                b_sql.Append(" AND customer_id='" +  State["CustomerID"].ToString() + "'");
                DataRow[] rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
                DataRow row = rows[0];
                if (row["production_app_name"] != null)
                    ProductionAppName.Text = row["production_app_name"].ToString(); 

                if (row["production_app_xml"] != DBNull.Value)
                    ProductionDesignExists.Visible = true;
                else
                    ProductionDesignExists.Visible = false;

                 bool use_1_user_credential = false;
                if (row["use_1_user_credential"] != DBNull.Value)
                {
                    string use_1_cred =  row["use_1_user_credential"].ToString();
                    use_1_user_credential = (use_1_cred.ToLower() == "true") ? true : false;
                }
                bool has_unlimited_users = false;
                if (row["has_unlimited_users"] != DBNull.Value)
                {
                    string has_unlimited = row["has_unlimited_users"].ToString();
                    has_unlimited_users = (has_unlimited.ToLower() == "true") ? true : false;
                }                

                b_sql = new StringBuilder("SELECT * FROM branding_images ");
                b_sql.Append("WHERE application_id='" +  State["ApplicationID"].ToString() + "'");
                rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
                foreach (DataRow image_row in rows)
                {
                    if (image_row["type"].ToString() == "icon" && image_row["width"].ToString() == "512")
                    {
                        LargeIconButton.Visible = true;
                        DeleteIcon.Visible = true;
                    }
                    if (image_row["type"].ToString() == "splash")
                    {
                        ScreenSplashButton.Visible = true;
                        DeleteSplashImage.Visible = true;
                    }
                }
                db.CloseViziAppsDatabase(State);

            }

        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }
    }
    protected void UploadLargeIconButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        if (UploadLargeIcon.UploadedFiles.Count > 0)
        {
            repeaterResultsLargeIcon.DataSource = UploadLargeIcon.UploadedFiles;
            repeaterResultsLargeIcon.DataBind();
            repeaterResultsLargeIcon.Visible = true;
            string targetFolder = Server.MapPath(UploadLargeIcon.TargetFolder);
            foreach (UploadedFile file in UploadLargeIcon.UploadedFiles)
            {
                string name = file.GetName(); //file name and suffix
                if (!name.ToLower().EndsWith(".jpg"))
                {
                    IconUploadMessage.Text = "File must be .jpg file";
                    return;
                }

                string file_path = targetFolder + @"\" + name;
                if (System.IO.File.Exists(file_path))
                {
                    byte[] image_data = File.ReadAllBytes(file_path);
                    ImageConverter ic = new ImageConverter();
                    System.Drawing.Image img = (System.Drawing.Image)ic.ConvertFrom(image_data);
                    Bitmap bitmap = new Bitmap(img);
                    if (bitmap.Width != 512 || bitmap.Height != 512)
                    {
                        IconUploadMessage.Text = "The image '" + name + "' is not 512 X 512";
                        return;
                    }

                    string file_name = name.Replace(" ", "_");

                    util.SetApplicationLargeIcon(State, State["ApplicationID"].ToString(), bitmap, file_name, file_path); 
                    
                    LargeIconButton.Visible = true;
                    DeleteIcon.Visible = true;
                }
            }
        }
        else
        {
            repeaterResultsLargeIcon.Visible = false;
            IconUploadMessage.Text = "Browse for a file";
        }

    }
    protected void UploadScreenSplashButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        if (UploadScreenSplash.UploadedFiles.Count > 0)
        {
            repeaterResultsScreenSplash.DataSource = UploadScreenSplash.UploadedFiles;
            repeaterResultsScreenSplash.DataBind();
            repeaterResultsScreenSplash.Visible = true;
            string targetFolder = Server.MapPath(UploadScreenSplash.TargetFolder);
            foreach (UploadedFile file in UploadScreenSplash.UploadedFiles)
            {
                string name = file.GetName();
                string file_path = targetFolder + @"\" + name;
                if (System.IO.File.Exists(file_path))
                {
                    byte[] image_data = File.ReadAllBytes(file_path);
                    ImageConverter ic = new ImageConverter();
                    System.Drawing.Image img = (System.Drawing.Image)ic.ConvertFrom(image_data);
                    Bitmap bitmap = new Bitmap(img);
                    int expected_width = Constants.IPHONE_DISPLAY_WIDTH;
                    int expected_height = Constants.IPHONE_SCROLL_HEIGHT;
                    switch (State["SelectedDeviceType"].ToString())
                    {
                        case Constants.IPAD:
                           expected_width = Constants.IPAD_DISPLAY_WIDTH;
                           expected_height = Constants.IPAD_SCROLL_HEIGHT;
                            break;
                        case Constants.ANDROID_TABLET:
                            expected_width = Constants.ANDROID_TABLET_DISPLAY_WIDTH;
                            expected_height = Constants.ANDROID_TABLET_SCROLL_HEIGHT;
                            break;
                        case Constants.IPHONE:
                            expected_width = Constants.IPHONE_DISPLAY_WIDTH;
                            expected_height = Constants.IPHONE_SCROLL_HEIGHT;
                           break;
                        case Constants.ANDROID_PHONE:
                           expected_width = Constants.ANDROID_PHONE_DISPLAY_WIDTH;
                           expected_height = Constants.ANDROID_PHONE_SCROLL_HEIGHT;
                            break;
                    }
                    if (bitmap.Width != expected_width || bitmap.Height != expected_height)
                    {
                        SplashUploadMessage.Text = "The image '" + name + "' is not the right size";
                        return;
                    }

                    AmazonS3 s3 = new AmazonS3();
                    string file_name = name.Replace(" ", "_");
                    string url = s3.UploadFile(State, file_name, file_path);
                    if (!url.StartsWith("http"))
                        return;

                    if (File.Exists(file_path))
                        File.Delete(file_path);

                    util.SetApplicationSplashImage(State,  State["ApplicationID"].ToString(), url);
                    
                    ScreenSplashButton.Visible = true;
                    DeleteSplashImage.Visible = true;

                }
            }
        }
        else
        {
            repeaterResultsScreenSplash.Visible = false;
            SplashUploadMessage.Text = "Browse for a file";
        }

    }
  
    protected void Publish_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        //check if entries were set
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT * FROM applications ");
        b_sql.Append("WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" +  State["CustomerID"].ToString() + "'");
        DataRow[] rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
        DataRow row = rows[0];
         if (row["production_app_name"] == DBNull.Value || row["production_app_name"].ToString().Length == 0)
         {
             PublishMessage.Text = "The Published App Name needs to be set and saved";
            return;
         }
         if (row["production_app_xml"] == DBNull.Value)
         {
             PublishMessage.Text = "The Publish Design needs to be saved";
            return;
         }

        string icon_url = util.GetApplicationLargeIcon(State,  State["ApplicationID"].ToString());
        if (icon_url == null || icon_url.Length == 0)
        {
            PublishMessage.Text = "The Icon image needs to be uploaded";
            return;
        }
        string splash_url = util.GetApplicationSplashImage(State,  State["ApplicationID"].ToString());
        if (splash_url == null || splash_url.Length == 0)
        {
            PublishMessage.Text = "The splash image needs to be uploaded";
            return;
        }

        //check on paid service
        //is payment current
        XmlUtil x_util = new XmlUtil();
        Hashtable features = util.IsProductionAppPaid(State);
        if (features == null)
        {
            PublishMessage.Text = "A production service needs to be paid for your app.";
            return;
        }
        else //check number of pages
        {
            int page_count = x_util.GetProductionAppPageCount(State);
            int sku_page_count = (int)features["max_pages"];
            if (page_count > sku_page_count)
            {
                PublishMessage.Text = "Your production app of " + page_count.ToString() + " pages exceeds the page limit of " + sku_page_count .ToString() + " for the production service you paid for.";
                return;
            }
        }

        State["UrlAccountIdentifier"] = util.GetUrlAccountIdentifier(State);
        if (State["UrlAccountIdentifier"].ToString().Length == 0)
        {
            PublishMessage.Text = "The Account Identifier has not been set. Go the Design Page and set the Account Identifier in the app properties";
            return;
        }

        WebAppsUtil web_util = new WebAppsUtil();
        AmazonS3 s3 = new AmazonS3();
        string ApplicationHomePath =  State["ApplicationHomePath"].ToString();
        string media_home_path = ApplicationHomePath + @"\customer_media";
        util.CheckDirectory(media_home_path);
        string customer_media_home_path = media_home_path + @"\" +  State["Username"].ToString();
        util.CheckDirectory(customer_media_home_path);
         State["IsProduction"] = true;
        string file_name =  State["SelectedApp"].ToString() + "/index.html";
        file_name = file_name.Replace(" ", "_");
        string save_file_path = customer_media_home_path + @"\" + file_name.Replace("/index.html", ".html");

        if (File.Exists(save_file_path))
            File.Delete(save_file_path);

        string html = web_util.GetWebApp(State,util.GetStagingAppXml(State),1.0D,1.0D);
        File.WriteAllText(save_file_path, html);
         string key =  State["UrlAccountIdentifier"].ToString() + "/" + file_name;
        s3.UploadFileWithKey(State, file_name, save_file_path, key);

 
        if (File.Exists(save_file_path))
            File.Delete(save_file_path);

        string filename =  State["SelectedApp"].ToString().Replace(" ", "_") + "_qrcode.png";
 
        string url = "http://viziapps.s3-website-us-east-1.amazonaws.com/" +  State["UrlAccountIdentifier"].ToString() + "/" +  State["SelectedApp"].ToString().Replace(" ", "_");
        PublishMessage.Text = "Pulished App URL: " + url;

        BitlyData.LoginName = ConfigurationManager.AppSettings["BitlyLoginName"];
        BitlyData.APIKEY = ConfigurationManager.AppSettings["BitlyAPIKey"];
        String bitly_url = Bitly.ShortURL(url, Bitly.Format.TXT);
        QRCode.Src = Bitly.GetQRCodeURL(bitly_url);
        PublishedAppURL.Text = "Short-length published App URL: " + bitly_url;

        QRCode.Style.Value = "";
        QRCodeLabel.Style.Value = "";
        QRCodeLabel.Text = "QR Code for Published Web App: " + State["SelectedApp"].ToString() + ". Capture the URL from this image with any app that reads QR codes and you will see your app on your device in seconds.";

    }
    private void ClearMessages()
    {
        SaveProductionAppNameMessage.Text = "";
        PublishMessage.Text = "";
        IconUploadMessage.Text = "";
        SplashUploadMessage.Text = "";
        repeaterResultsLargeIcon.Visible = false;
        repeaterResultsScreenSplash.Visible = false;
    }
    private void saveJpeg(string path, Bitmap img, long quality)
    {
        // Encoder parameter for image quality
        EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

        // Jpeg image codec
        ImageCodecInfo jpegCodec = this.getEncoderInfo("image/jpeg");

        if (jpegCodec == null)
            return;

        EncoderParameters encoderParams = new EncoderParameters(1);
        encoderParams.Param[0] = qualityParam;

        img.Save(path, jpegCodec, encoderParams);
    }

    private ImageCodecInfo getEncoderInfo(string mimeType)
    {
        // Get image codecs for all image formats
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

        // Find the correct image codec
        for (int i = 0; i < codecs.Length; i++)
            if (codecs[i].MimeType == mimeType)
                return codecs[i];
        return null;
    }
    protected void DeleteIcon_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        util.DeleteLargeIcon(State, State["ApplicationID"].ToString());
        IconUploadMessage.Text = "Icon Deleted.";
        LargeIconButton.Visible = false;
        DeleteIcon.Visible = false;
    }
    protected void DeleteSplashImage_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        util.DeleteSplashImage(State, State["ApplicationID"].ToString());
        SplashUploadMessage.Text = "Splash Image Deleted.";
        ScreenSplashButton.Visible = false;
        DeleteSplashImage.Visible = false;
    }
    protected void CopyDesignToProduction_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        util.CopyStagingDesignToProduction(State);
        CopyDesignMessage.Text = "Done.";
        ProductionDesignExists.Visible = true;
    }

    protected void SaveProductionAppNameButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        if (ProductionAppName.Text.Trim().Length == 0)
        {
             SaveProductionAppNameMessage.Text = "Enter an app name for production";
            return;
        }
         util.SetProductionAppName(State, ProductionAppName.Text.Trim()); 
        SaveProductionAppNameMessage.Text = "Saved.";
    }
    protected void ShowPublishedWebApp()
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "Default.aspx")) return;

        if (State["SelectedApp"] == null || State["SelectedApp"].ToString().Contains("->"))
            return;

        WebAppsUtil web_util = new WebAppsUtil();
        AmazonS3 s3 = new AmazonS3();
        string ApplicationHomePath = State["ApplicationHomePath"].ToString();
        string media_home_path = ApplicationHomePath + @"\customer_media";
        util.CheckDirectory(media_home_path);
        string customer_media_home_path = media_home_path + @"\" + State["Username"].ToString();
        util.CheckDirectory(customer_media_home_path);
        State["IsProduction"] = true;
        string file_name = State["SelectedApp"].ToString().Replace(" ", "_") + Constants.WEB_APP_TEST_SUFFIX + "/index.html";
        string save_file_path = customer_media_home_path + @"\" + file_name.Replace(Constants.WEB_APP_TEST_SUFFIX + "/index.html", ".html");

        string ret = web_util.GetWebApp(State, util.GetStagingAppXml(State),1.0D, 1.0D);
        if (ret.StartsWith("Error:"))
        {
            PublishMessage.Text = ret;
             return;
        }
        File.WriteAllText(save_file_path, ret);

        if (State["UrlAccountIdentifier"] == null)
        {
            PublishMessage.Text = "The Account Identifier has not been set.";
            return;
        }
        string key = State["UrlAccountIdentifier"].ToString() + "/" + file_name;
        s3.UploadFileWithKey(State, file_name, save_file_path, key);
        string url = "http://viziapps.s3-website-us-east-1.amazonaws.com/" + State["UrlAccountIdentifier"].ToString() + "/" + State["SelectedApp"].ToString().Replace(" ", "_") ;

        if (File.Exists(save_file_path))
            File.Delete(save_file_path);

        BitlyData.LoginName = ConfigurationManager.AppSettings["BitlyLoginName"];
        BitlyData.APIKEY = ConfigurationManager.AppSettings["BitlyAPIKey"];
        String bitly_url = Bitly.ShortURL(url, Bitly.Format.TXT);
        QRCode.Src = Bitly.GetQRCodeURL(bitly_url);

        PublishedAppURL.Text = "Published App URL: " + bitly_url;
        QRCode.Style.Value = "";
        QRCodeLabel.Style.Value = "";
        QRCodeLabel.Text = "QR Code for Published Web App: " + State["SelectedApp"].ToString() + ". Capture the URL from this image with any app that reads QR codes and you will see your app on your device in seconds.";

    }

}