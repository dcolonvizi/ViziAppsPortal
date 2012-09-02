using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
using Telerik.Web.UI;

public partial class ProvisionForm : System.Web.UI.Page
{

    public string AppType = "";
    public string AppID = "";
    public string AppSKU = "";



    protected void Page_Load(object sender, EventArgs e)
    {

        Util util = new Util();


        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        try
        {
            ClearMessages();

            if (!IsPostBack)
            {
                NATIVE.Visible = false;
                WEB.Visible = false;


                if (InitAppsList(State, RadComboAppSelector) == false)
                {
                    //There are no Apps which can add a NEW Service at this moment.
                    RadNotification1.Title = "WARNING";
                    RadNotification1.Text = "There are no Apps that require a Publishing Form";
                    RadNotification1.Visible = true;
                    RadNotification1.Show();
                }



                RadComboAppSelector.Visible = true;



            }
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }
    }


    // Modified from original viziapps code with the Additional paid check so only valid ones are added to the ComboBox.
    private bool InitAppsList(Hashtable State, RadComboBox AppsList)
    {
        Util util = new Util();
        try
        {
            if ((AppsList == null) || (State["CustomerID"] == null))
                return false;
            
            string sql = "SELECT DISTINCT application_name,application_id FROM applications WHERE customer_id='" + State["CustomerID"].ToString() + "' ORDER BY application_name";
            DB db = new DB();
            DataRow[] rows = db.ViziAppsExecuteSql(State, sql);
            AppsList.Items.Clear();
            foreach (DataRow row in rows)
            {

                string app_name = row["application_name"].ToString();
                string application_id = row["application_id"].ToString();

                
                //if (util.IsAppStoreSubmissionPaid(State, app_name) == false)

                //Inserting only Apps not done submitting the Publishing Form
                if(util.IsFreeProductionValid(State, application_id) == false)
                {
                    AppsList.Items.Add(new RadComboBoxItem(app_name, app_name));

                }//End paid check
                               
            }
            
            if (AppsList.IsEmpty)
                return false;

            AppsList.Items.Insert(0, new RadComboBoxItem("Select App ->", "Select App ->"));
            AppsList.Items[0].Selected = true;


            return true;
        }
        catch (Exception ex)
        {
            util.ProcessMainExceptions(State, Response, ex);
        }
        return false;
    }

    private void page_refresh()
    {

        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];



        XmlUtil x_util = new XmlUtil();
        //State["SelectedDeviceView"] = 
        State["SelectedDeviceType"] = x_util.GetAppDeviceType(State);
        if (State["SelectedDeviceType"] == null)
        {
            // State["SelectedDeviceView"] = 
            State["SelectedDeviceType"] = Constants.IPHONE;
        }

        //check on device type
        switch (State["SelectedDeviceType"].ToString())
        {
            case Constants.IPAD:
                Web_SplashUploadLabel.Text = "Splash Image ( 768 X 1004 pixels from .jpg file )";
                Web_ScreenSplashButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript("../Dialogs/Publish/ScreenShot.aspx", 1004, 768, false, false, false, true));
                break;
            case Constants.ANDROID_TABLET:
                Web_SplashUploadLabel.Text = "Splash Image ( 800 X 1233 pixels from .jpg file )";
                Web_ScreenSplashButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript("../Dialogs/Publish/ScreenShot.aspx", 1233, 800, false, false, false, true));
                break;
            case Constants.IPHONE:
                Web_SplashUploadLabel.Text = "Splash Image ( 320 X 460 pixels from .jpg file )";
                Web_ScreenSplashButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript("../Dialogs/Publish/ScreenShot.aspx", 460, 320, false, false, false, true));
                break;
            case Constants.ANDROID_PHONE:
                Web_SplashUploadLabel.Text = "Splash Image ( 320 X 508 pixels from .jpg file )";
                Web_ScreenSplashButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript("../Dialogs/Publish/ScreenShot.aspx", 508, 320, false, false, false, true));
                break;
        }
        SelectedDeviceType.Text = State["SelectedDeviceType"].ToString();
        
                
        
        State["ApplicationID"] = util.GetAppID(State);

        if (State["SelectedAppType"].ToString().Contains("web"))
        {
            App.Text = "Test Web App Name: " + State["SelectedApp"].ToString();
            Web_SubmitPublishingForm.Visible = true;
            SubmitPublishingForm.Visible = false;
            SubmissionNotes.Visible = true;
            SubmissionNotesLabel.Visible = true;
        }

        else
        {
            App.Text = "Test Native/Hybrid App Name: " + State["SelectedApp"].ToString();
            SubmitPublishingForm.Visible = true;
            Web_SubmitPublishingForm.Visible = false;
            //Web_SubmissionNotes.Visible = true;
            //Web_SubmissionNotesLabel.Visible = true;
        }


        /*if (util.IsAppStoreSubmissionPaid(State, State["SelectedApp"].ToString()))
        {*/
            
            /*SubmissionNotes.Visible = true;
            SubmissionNotesLabel.Visible = true;*/
            //PurchaseButton.Visible = false;
        /*}
        else
        {
            SubmitForProvisioning.Visible = false;
            SubmissionNotes.Visible = false;
            SubmissionNotesLabel.Visible = false;
            //PurchaseButton.Visible = true;
            //ProvisioningMessage.Text = "You can fill this form any time, but to submit your app for production, you need to first purchase one of the ViziApps services to submit the app to an app store.";
            //PurchaseButton.Attributes.Add("onclick", PopupHelper.GeneratePopupScript("http://stores.homestead.com/MobiFlexStore/StoreFront.bok", 700, 900, false, false, false, true));

        }*/


        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT * FROM applications ");
        b_sql.Append("WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        DataRow[] rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
        if (rows.Length > 0)
        {
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
                string use_1_cred = row["use_1_user_credential"].ToString();
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
                //NumberOfUsers.Style.Value = "";
                //NumberOfUsersLabel.Style.Value = "";

                //NumberOfUsers.SelectedIndex = 1;
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
                //NumberOfUsers.Style.Value = "display:none";
                //NumberOfUsersLabel.Style.Value = "display:none";
                LimitedUsersPanel.Style.Value = "display:none";
            }
            else
            {
                long max_users = util.GetMaxUsers(State);
                if (max_users > 1000)
                {
                    //NumberOfUsers.Style.Value = "display:none";
                    //NumberOfUsersLabel.Style.Value = "display:none";
                    LimitedUsersPanel.Style.Value = "display:none";
                    util.SetUnlimitedUsers(State);
                }
                else
                {
                    UploadPublishedUserCredentials.Style.Value = "";
                    ViewPublishedUserCredentials.Style.Value = "";
                    UploadPublishedUserCredentials.Attributes.Add("onclick", PopupHelper.GeneratePopupScript("~/Dialogs/Publish/UploadUserCredentials.aspx", 350, 800, false, false, false, true));
                    ViewPublishedUserCredentials.Attributes.Add("onclick", PopupHelper.GeneratePopupScript("~/Dialogs/Publish/ViewUserCredentials.aspx", 800, 350, false, false, false, true));
                }
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

    protected void UploadLargeIconButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

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
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;


        if (UploadScreenSplash.UploadedFiles.Count > 0)
        {
            repeaterResultsScreenSplash.DataSource = UploadScreenSplash.UploadedFiles;
            repeaterResultsScreenSplash.DataBind();
            repeaterResultsScreenSplash.Visible = true;
            string targetFolder = Server.MapPath(UploadScreenSplash.TargetFolder);
            foreach (UploadedFile file in UploadScreenSplash.UploadedFiles)
            {
                string name = file.GetName();
                if (!name.ToLower().EndsWith(".jpg"))
                {
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

    protected void SubmitPublishingFormClicked(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        /*if (!util.IsAppStoreSubmissionPaid(State, State["SelectedApp"].ToString()))
        {
            ProvisioningMessage.Text = "You need to first purchase one of the ViziApps services to submit the app to an app store.";
            return;
        }*/

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



        
        StringBuilder body = new StringBuilder("Customer Username: " + State["Username"].ToString() + "\n");
        body.Append("App Name: " + State["SelectedApp"].ToString() + "\n");
        body.Append("App Type: " + State["SelectedAppType"].ToString() + "\n");
        if (SubmissionNotes.Text.Length > 0)
            body.Append("Customer Notes: " + SubmissionNotes.Text + "\n");
        body.Append("\n-- ViziApps Support");

        Email email = new Email();

        string status = email.SendEmail(State,  HttpRuntime.Cache["TechSupportEmail"].ToString(),  HttpRuntime.Cache["TechSupportEmail"].ToString(), "", "", "Customer Submitted Publishing Form", body.ToString(), "", false);
        
        if (status.IndexOf("OK") >= 0)
        {
            ProvisioningMessage.Text = "Your publishing form has been received.";
        }
        else
        {
            ProvisioningMessage.Text = "There has been a problem submitting your publishing form. Please contact support@viziapps.com";
        }


        util.SetFreeProductionExpiration(State, DateTime.Now.ToUniversalTime().AddDays(14.0D));

        
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
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        util.DeleteLargeIcon(State, State["ApplicationID"].ToString());
        IconUploadMessage.Text = "Icon Deleted.";
        LargeIconButton.Visible = false;
        DeleteIcon.Visible = false;
    }

    protected void DeleteSplashImage_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        util.DeleteSplashImage(State, State["ApplicationID"].ToString());
        SplashUploadMessage.Text = "Splash Image Deleted.";
        ScreenSplashButton.Visible = false;
        DeleteSplashImage.Visible = false;
    }

    protected void CopyDesignToProduction_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        util.CopyStagingDesignToProduction(State);
        CopyDesignMessage.Text = "Done.";
        ProductionDesignExists.Visible = true;
    }

    protected void SaveProductionAppNameButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        if (ProductionAppName.Text.Trim().Length == 0)
        {
            SaveProductionAppNameMessage.Text = "Enter an app name for production";
            return;
        }
        util.SetProductionAppName(State, ProductionAppName.Text.Trim());
        SaveProductionAppNameMessage.Text = "Saved.";
    }

    protected void NumberOfUsersCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        /*        if (NumberOfUsers.SelectedIndex == 1)
        { */
        
        
        LimitedUsersPanel.Style.Value = "";
        UploadPublishedUserCredentials.Style.Value = "display:none";
        ViewPublishedUserCredentials.Style.Value = "display:none";

        
        /*}
        else
        {
            util.SetLimitedUsersCredentialMethod(State, false, "", "");
            LimitedUsersPanel.Style.Value = "display:none";
            UploadPublishedUserCredentials.Style.Value = "";
            ViewPublishedUserCredentials.Style.Value = "";
            UploadPublishedUserCredentials.Attributes.Add("onclick", PopupHelper.GeneratePopupScript("~/Dialogs/Publish/UploadUserCredentials.aspx", 350, 800, false, false, false, true));
            ViewPublishedUserCredentials.Attributes.Add("onclick", PopupHelper.GeneratePopupScript("~/Dialogs/Publish/ViewUserCredentials.aspx", 800, 350, false, false, false, true));
        }*/
               
    }

    protected void SaveUserCredentials_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;


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

    protected void BackButton_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../TabPublish.aspx");
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;
        string account_type = util.GetAccountType(State);
        util.Logout(State);
        if (account_type != null && account_type.Contains("google_apps"))
            Response.Redirect("../LogoutForGoogleApps.aspx", false);
        else
            Response.Redirect("../Default.aspx", false);

    }

    protected void TabMenu_ItemClick(object sender, RadMenuEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        //if (SavedCanvasHtml.Text.Length > 0)
        //     SavePage();

        string tab = e.Item.Value;
        Session["MainMenu"] = tab;
        if (tab == "DesignHybrid" || tab == "DesignWeb")
            State["SelectedApp"] = null;

        Response.Redirect("../Tab" + tab + ".aspx", false);
    }

    protected void RadComboAppSelector_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;
        try
        {
            ClearMessages();

            InitCurrentApp(RadComboAppSelector.SelectedValue.ToString());
            
            page_refresh();
           
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);

        }
    }

    public void InitCurrentApp(string app)
    {
        try
        {
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            RadComboAppSelector.SelectedValue = app;
            Util util = new Util();
            if (app.Contains("->"))
            {
                State["SelectedDeviceType"] = Constants.IPHONE;
                State["SelectedDeviceView"] = Constants.IPHONE;
                return;
            }
            State["SelectedApp"] = app;
            State["SelectedAppType"] = util.GetAppType(State);

            if (State["SelectedAppType"].ToString().Contains("web"))
            {
                WEB.Visible = true;
                NATIVE.Visible = false;
            }
            else
            {
                NATIVE.Visible = true;
                WEB.Visible = false;
            }


            if (State["SelectedDeviceType"] == null)
            {
                State["SelectedDeviceView"] = State["SelectedDeviceType"] = Constants.IPHONE;

            }
  
        }
        catch (Exception ex)
        {
            Util util = new Util();
            Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
            util.LogError(State, ex);
        }
    }

    protected void Web_SaveProductionAppNameButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        if (Web_ProductionAppName.Text.Trim().Length == 0)
        {
            Web_SaveProductionAppNameMessage.Text = "Enter an app name for production";
            return;
        }
        util.SetProductionAppName(State, Web_ProductionAppName.Text.Trim());
        Web_SaveProductionAppNameMessage.Text = "Saved.";

    }

    protected void Web_CopyDesignToProduction_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        util.CopyStagingDesignToProduction(State);
        Web_CopyDesignMessage.Text = "Done.";
        Web_ProductionDesignExists.Visible = true;

    }

    protected void Web_UploadLargeIconButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        if (Web_UploadLargeIcon.UploadedFiles.Count > 0)
        {
            Web_repeaterResultsLargeIcon.DataSource = Web_UploadLargeIcon.UploadedFiles;
            Web_repeaterResultsLargeIcon.DataBind();
            Web_repeaterResultsLargeIcon.Visible = true;
            string targetFolder = Server.MapPath(Web_UploadLargeIcon.TargetFolder);
            foreach (UploadedFile file in Web_UploadLargeIcon.UploadedFiles)
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

                    Web_LargeIconButton.Visible = true;
                    Web_DeleteIcon.Visible = true;
                }
            }
        }
        else
        {
            Web_repeaterResultsLargeIcon.Visible = false;
            Web_IconUploadMessage.Text = "Browse for a file";
        }
    }

    protected void Web_UploadScreenSplashButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        if (Web_UploadScreenSplash.UploadedFiles.Count > 0)
        {
            Web_repeaterResultsScreenSplash.DataSource = Web_UploadScreenSplash.UploadedFiles;
            Web_repeaterResultsScreenSplash.DataBind();
            Web_repeaterResultsScreenSplash.Visible = true;
            string targetFolder = Server.MapPath(Web_UploadScreenSplash.TargetFolder);
            foreach (UploadedFile file in Web_UploadScreenSplash.UploadedFiles)
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
                        Web_SplashUploadMessage.Text = "The image '" + name + "' is not the right size";
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

                    Web_ScreenSplashButton.Visible = true;
                    Web_DeleteSplashImage.Visible = true;

                }
            }
        }
        else
        {
            Web_repeaterResultsScreenSplash.Visible = false;
            Web_SplashUploadMessage.Text = "Browse for a file";
        }


    }

    protected void Web_SubmitPublishingForm_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../../Default.aspx")) return;

        //check if entries were set
        DB db = new DB();
        StringBuilder b_sql = new StringBuilder("SELECT * FROM applications ");
        b_sql.Append("WHERE application_name='" + State["SelectedApp"].ToString() + "'");
        b_sql.Append(" AND customer_id='" + State["CustomerID"].ToString() + "'");
        DataRow[] rows = db.ViziAppsExecuteSql(State, b_sql.ToString());
        DataRow row = rows[0];
        if (row["production_app_name"] == DBNull.Value || row["production_app_name"].ToString().Length == 0)
        {
            Web_PublishMessage.Text = "The Published App Name needs to be set and saved";
            return;
        }
        if (row["production_app_xml"] == DBNull.Value)
        {
            Web_PublishMessage.Text = "The Publish Design needs to be saved";
            return;
        }

        string icon_url = util.GetApplicationLargeIcon(State, State["ApplicationID"].ToString());
        if (icon_url == null || icon_url.Length == 0)
        {
            Web_PublishMessage.Text = "The Icon image needs to be uploaded";
            return;
        }
        string splash_url = util.GetApplicationSplashImage(State, State["ApplicationID"].ToString());
        if (splash_url == null || splash_url.Length == 0)
        {
            Web_PublishMessage.Text = "The splash image needs to be uploaded";
            return;
        }

        //check on paid service
        //is payment current
        XmlUtil x_util = new XmlUtil();
        /*Hashtable features = util.IsProductionAppPaid(State);
        if (features == null)
        {
            Web_PublishMessage.Text = "A production service needs to be paid for your app.";
            return;
        }
        else //check number of pages
        {
            int page_count = x_util.GetProductionAppPageCount(State);
            int sku_page_count = (int)features["max_pages"];
            if (page_count > sku_page_count)
            {
                Web_PublishMessage.Text = "Your production app of " + page_count.ToString() + " pages exceeds the page limit of " + sku_page_count.ToString() + " for the production service you paid for.";
                return;
            }
        }*/

        State["UrlAccountIdentifier"] = util.GetUrlAccountIdentifier(State);
        if (State["UrlAccountIdentifier"].ToString().Length == 0)
        {
            Web_PublishMessage.Text = "The Account Identifier has not been set. Go the Design Page and set the Account Identifier in the app properties";
            return;
        }

        WebAppsUtil web_util = new WebAppsUtil();
        AmazonS3 s3 = new AmazonS3();
        State["IsProduction"] = true;
        
        string file_name = State["SelectedApp"].ToString() + "/index.html";
        file_name = file_name.Replace(" ", "_");
        string save_file_path =  HttpRuntime.Cache["TempFilesPath"].ToString() + State["Username"].ToString() + "." + file_name.Replace("/index.html", ".html");

        if (File.Exists(save_file_path))
            File.Delete(save_file_path);

        string html = web_util.GetWebApp(State, util.GetStagingAppXml(State), 1.0D, 1.0D);
        File.WriteAllText(save_file_path, html);
        string key = State["UrlAccountIdentifier"].ToString() + "/" + file_name;
        s3.UploadFileWithKey(State, file_name, save_file_path, key);


        if (File.Exists(save_file_path))
            File.Delete(save_file_path);

        string filename = State["SelectedApp"].ToString().Replace(" ", "_") + "_qrcode.png";

        string url = "http://viziapps.s3-website-us-east-1.amazonaws.com/" + State["UrlAccountIdentifier"].ToString() + "/" + State["SelectedApp"].ToString().Replace(" ", "_");
        Web_PublishMessage.Text = "Published App URL: " + url;

        BitlyData.LoginName = ConfigurationManager.AppSettings["BitlyLoginName"];
        BitlyData.APIKEY = ConfigurationManager.AppSettings["BitlyAPIKey"];
        String bitly_url = Bitly.ShortURL(url, Bitly.Format.TXT);
        Web_QRCode.Src = Bitly.GetQRCodeURL(bitly_url);
        Web_PublishedAppURL.Text = "Short-length published App URL: " + bitly_url;

        Web_QRCode.Style.Value = "";
        Web_QRCodeLabel.Style.Value = "";
        Web_QRCodeLabel.Text = "QR Code for Published Web App: " + State["SelectedApp"].ToString() + ". Capture the URL from this image with any app that reads QR codes and you will see your app on your device in seconds.";

        util.SetFreeProductionExpiration(State, DateTime.Now.ToUniversalTime().AddDays(14.0D));

    }

    private void Web_ClearMessages()
    {
        Web_SaveProductionAppNameMessage.Text = "";
        Web_PublishMessage.Text = "";
        Web_IconUploadMessage.Text = "";
        Web_SplashUploadMessage.Text = "";
        Web_repeaterResultsLargeIcon.Visible = false;
        Web_repeaterResultsScreenSplash.Visible = false;
    }
    private void Web_saveJpeg(string path, Bitmap img, long quality)
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

    private ImageCodecInfo Web_getEncoderInfo(string mimeType)
    {
        // Get image codecs for all image formats
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

        // Find the correct image codec
        for (int i = 0; i < codecs.Length; i++)
            if (codecs[i].MimeType == mimeType)
                return codecs[i];
        return null;
    }


    protected void Web_DeleteIcon_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        util.DeleteLargeIcon(State, State["ApplicationID"].ToString());
        Web_IconUploadMessage.Text = "Icon Deleted.";
        Web_LargeIconButton.Visible = false;
        Web_DeleteIcon.Visible = false;
    }

    protected void Web_DeleteSplashImage_Click(object sender, ImageClickEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        util.DeleteSplashImage(State, State["ApplicationID"].ToString());
        Web_SplashUploadMessage.Text = "Splash Image Deleted.";
        Web_ScreenSplashButton.Visible = false;
        Web_DeleteSplashImage.Visible = false;
    }

    protected void ShowPublishedWebApp()
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        if (State["SelectedApp"] == null || State["SelectedApp"].ToString().Contains("->"))
            return;

        WebAppsUtil web_util = new WebAppsUtil();
        AmazonS3 s3 = new AmazonS3();
        State["IsProduction"] = true;

        string file_name = State["SelectedApp"].ToString().Replace(" ", "_") + Constants.WEB_APP_TEST_SUFFIX + "/index.html";
        string save_file_path =  HttpRuntime.Cache["TempFilesPath"].ToString() + State["Username"].ToString() + "." + file_name.Replace(Constants.WEB_APP_TEST_SUFFIX + "/index.html", ".html");

        string ret = web_util.GetWebApp(State, util.GetStagingAppXml(State), 1.0D, 1.0D);
        if (ret.StartsWith("Error:"))
        {
            Web_PublishMessage.Text = ret;
            return;
        }
        File.WriteAllText(save_file_path, ret);

        if (State["UrlAccountIdentifier"] == null)
        {
            Web_PublishMessage.Text = "The Account Identifier has not been set.";
            return;
        }
        string key = State["UrlAccountIdentifier"].ToString() + "/" + file_name;
        s3.UploadFileWithKey(State, file_name, save_file_path, key);
        string url = "http://viziapps.s3-website-us-east-1.amazonaws.com/" + State["UrlAccountIdentifier"].ToString() + "/" + State["SelectedApp"].ToString().Replace(" ", "_");

        if (File.Exists(save_file_path))
            File.Delete(save_file_path);

        BitlyData.LoginName = ConfigurationManager.AppSettings["BitlyLoginName"];
        BitlyData.APIKEY = ConfigurationManager.AppSettings["BitlyAPIKey"];
        String bitly_url = Bitly.ShortURL(url, Bitly.Format.TXT);
        Web_QRCode.Src = Bitly.GetQRCodeURL(bitly_url);

        Web_PublishedAppURL.Text = "Published App URL: " + bitly_url;
        Web_QRCode.Style.Value = "";
        Web_QRCodeLabel.Style.Value = "";
        Web_QRCodeLabel.Text = "QR Code for Published Web App: " + State["SelectedApp"].ToString() + ". Capture the URL from this image with any app that reads QR codes and you will see your app on your device in seconds.";

    }


    
}