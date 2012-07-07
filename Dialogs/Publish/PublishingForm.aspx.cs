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

using Aspose.Excel;
using Telerik.Web.UI;

public partial class ProvisionForm : System.Web.UI.Page
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

            App.Text = "Test Native App Name: " + State["SelectedApp"].ToString();

            if (util.IsAppStoreSubmissionPaid(State, State["SelectedApp"].ToString()))
            {
                SubmitForProvisioning.Visible = true;
                SubmissionNotes.Visible = true;
                SubmissionNotesLabel.Visible = true;
                PurchaseButton.Visible = false;
             }
            else
            {
                SubmitForProvisioning.Visible = false;
                SubmissionNotes.Visible = false;
                SubmissionNotesLabel.Visible = false;
                PurchaseButton.Visible = true;
                ProvisioningMessage.Text = "You can fill this form any time, but to submit your app for production, you need to first purchase one of the ViziApps services to submit the app to an app store.";
                PurchaseButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
    "http://stores.homestead.com/MobiFlexStore/StoreFront.bok", 700, 900, false, false, false, true));

            }

            if (!IsPostBack)
            {
                DB db = new DB();
                StringBuilder b_sql = new StringBuilder("SELECT * FROM applications ");
                b_sql.Append("WHERE application_name='" + State["SelectedApp"].ToString() + "'");
                b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
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

                if (use_1_user_credential)
                {
                    NumberOfUsers.Style.Value = "";
                    NumberOfUsersLabel.Style.Value = "";
 
                    NumberOfUsers.SelectedIndex = 1;
                    ArrayList credential = util.GetEndUserCredentials(State);
                    if (credential != null && credential.Count > 0)
                    {
                        string[] output = (string[])credential[0];
                        SingleUsername.Text = output[0];
                        SinglePassword.Text = output[1];
                    }
                    LimitedUsersPanel.Style.Value = "";
                }
                else if (has_unlimited_users)
                {
                    NumberOfUsers.Style.Value = "display:none";
                    NumberOfUsersLabel.Style.Value = "display:none";
                    LimitedUsersPanel.Style.Value = "display:none";
                }
                else
                {
                    long max_users = util.GetMaxUsers(State);
                    if (max_users > 1000)
                    {
                        NumberOfUsers.Style.Value = "display:none";
                        NumberOfUsersLabel.Style.Value = "display:none";
                        LimitedUsersPanel.Style.Value = "display:none";
                        util.SetUnlimitedUsers(State);
                    }
                    else
                    {
                        UploadPublishedUserCredentials.Style.Value = "";
                        ViewPublishedUserCredentials.Style.Value = "";
                        UploadPublishedUserCredentials.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                 "../../Dialogs/Publish/UploadUserCredentials.aspx", 350, 800, false, false, false, true));
                        ViewPublishedUserCredentials.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
                "../../Dialogs/Publish/ViewUserCredentials.aspx", 800, 350, false, false, false, true));
                    }
                }

                b_sql = new StringBuilder("SELECT * FROM branding_images ");
                b_sql.Append("WHERE application_id='" + State["ApplicationID"].ToString() + "'");
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
                if(!name.ToLower().EndsWith(".jpg")){
                    SplashUploadMessage.Text = "File must be .jpg file";
                    return;
                }
                string file_path = targetFolder + @"\" + name;
                if (System.IO.File.Exists(file_path))
                {
                    byte[] image_data = File.ReadAllBytes(file_path);
                    ImageConverter ic = new ImageConverter();
                    System.Drawing.Image img = (System.Drawing.Image)ic.ConvertFrom(image_data);
                    Bitmap bitmap = new Bitmap(img);
                    if (bitmap.Width != 320 || bitmap.Height != 460)
                    {
                        SplashUploadMessage.Text = "The image '" + name + "' is not 320 X 460";
                        return;
                    }

                    AmazonS3 s3 = new AmazonS3();
                    string file_name = name.Replace(" ", "_");
                    string url = s3.UploadFile(State, file_name, file_path);
                    if (!url.StartsWith("http"))
                        return;

                    if (File.Exists(file_path))
                        File.Delete(file_path);

                    util.SetApplicationSplashImage(State, State["ApplicationID"].ToString(), url);
                    
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
  
    protected void SubmitForProvisioning_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        if (!util.IsAppStoreSubmissionPaid(State, State["SelectedApp"].ToString()))
        {
            ProvisioningMessage.Text = "You need to first purchase one of the ViziApps services to submit the app to an app store.";
            return;
        }

        //check if entries were set
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT * FROM applications ");
        b_sql.Append("WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        DataRow[] rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
        DataRow row = rows[0];
         if (row["production_app_name"] == DBNull.Value || row["production_app_name"].ToString().Length == 0)
         {
             ProvisioningMessage.Text = "The Published App Name needs to be set and saved";
            return;
         }
         if (row["production_app_xml"] == DBNull.Value)
         {
             ProvisioningMessage.Text = "The Publish Design has not been saved";
            return;
         }

         string url = util.GetApplicationLargeIcon(State, State["ApplicationID"].ToString());
        if (url == null || url.Length == 0)
        {
            ProvisioningMessage.Text = "The Icon image has not been uploaded";
            return;
        }
        url = util.GetApplicationSplashImage(State, State["ApplicationID"].ToString());
        if (url == null || url.Length == 0)
        {
            ProvisioningMessage.Text = "The splash image has not been uploaded";
            return;
        }

        util.SetFreeProductionExpiration(State, DateTime.Now.ToUniversalTime().AddDays(10.0D));
 
        StringBuilder body = new StringBuilder("Customer Username: " + State["Username"].ToString() + "\n");        
        body.Append("App Name: " + State["SelectedApp"].ToString() + "\n");
        if(SubmissionNotes.Text.Length > 0)
            body.Append("Customer Notes: " + SubmissionNotes.Text + "\n");
        body.Append("\n-- ViziApps Support");

        Email email = new Email();
  
        string status = email.SendEmail(State, State["TechSupportEmail"].ToString(), State["TechSupportEmail"].ToString(), 
            "", "", "Customer Request to Submit or Update App to Store", body.ToString(), "",false);
        if (status.IndexOf("OK") >= 0)
        {
            ProvisioningMessage.Text = "Your application has been submitted for production. When it is ready, you will get an email.";
        }
        else
        {
            ProvisioningMessage.Text = "There has been a problem sending your request. Please contact support@viziapps.com";
        }
    }
    private void ClearMessages()
    {
        SaveCredentialsMessage.Text = "";
        SaveNumberOfUsersMessage.Text = "";
        SaveProductionAppNameMessage.Text = "";
        ProvisioningMessage.Text = "";
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

    protected void NumberOfUsers_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        NumberOfUsers.SelectedValue = e.Value;
        NumberOfUsers.Text = e.Text;
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        if (NumberOfUsers.SelectedIndex == 1)
        {
            LimitedUsersPanel.Style.Value = "";
            UploadPublishedUserCredentials.Style.Value = "display:none";
            ViewPublishedUserCredentials.Style.Value = "display:none";
         }
        else
        {
            util.SetLimitedUsersCredentialMethod(State, false, "", "");
            LimitedUsersPanel.Style.Value = "display:none";
            UploadPublishedUserCredentials.Style.Value = "";
            ViewPublishedUserCredentials.Style.Value = "";
            UploadPublishedUserCredentials.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
     "../../Dialogs/Publish/UploadUserCredentials.aspx", 350, 800, false, false, false, true));
            ViewPublishedUserCredentials.Attributes.Add("onclick", PopupHelper.GeneratePopupScript(
    "../../Dialogs/Publish/ViewUserCredentials.aspx", 800, 350, false, false, false, true));

        }
    }
    protected void SaveUserCredentials_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;


        if (SingleUsername.Text.Length == 0)
        {
            SaveNumberOfUsersMessage.Text = "Enter Single Username.";
            return;
        }
        if (SinglePassword.Text.Length == 0)
        {
            SaveNumberOfUsersMessage.Text = "Enter Single Password.";
            return;
        }
        util.SetLimitedUsersCredentialMethod(State, true, SingleUsername.Text.Trim(), SinglePassword.Text.Trim());
        SaveCredentialsMessage.Text = "saved.";

    }
}