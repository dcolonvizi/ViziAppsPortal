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
using Telerik.Web.UI;

public partial class Dialogs_UploadBackgroundImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlUtil x_util = new XmlUtil();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        string device_type = x_util.GetAppDeviceType(State);
        if (device_type == null)
        {
            UploadMessage.Text = "Before you can add a custom background you first need to save the app.";
            return;
        }

        switch (device_type)
        {
            case Constants.ANDROID_PHONE:
                Width.Text = Constants.ANDROID_PHONE_DISPLAY_WIDTH_S;
                Height.Text = (Constants.ANDROID_PHONE_DISPLAY_HEIGHT - 20).ToString();
                break;
            case Constants.ANDROID_TABLET:
                Width.Text = Constants.ANDROID_TABLET_DISPLAY_WIDTH_S;
                Height.Text = (Constants.ANDROID_TABLET_DISPLAY_HEIGHT - 25).ToString();
                break;
            case Constants.IPAD:
                Width.Text = Constants.IPAD_DISPLAY_WIDTH_S;
                Height.Text = (Constants.IPAD_DISPLAY_HEIGHT - 25).ToString();
                break;
            case Constants.IPHONE:
            default:
                Width.Text = Constants.IPHONE_DISPLAY_WIDTH_S;
                Height.Text = (Constants.IPHONE_DISPLAY_HEIGHT - 20).ToString();
                break;
        }
    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        if (UploadMessage.Text.Length > 0)
            return;
 
        int width =  Convert.ToInt32(Width.Text);
        int height = Convert.ToInt32(Height.Text);

        if (UploadBackground.UploadedFiles.Count > 0)
        {
             string targetFolder = Server.MapPath(UploadBackground.TargetFolder);
            foreach (UploadedFile file in UploadBackground.UploadedFiles)
            {
                string name = file.GetName();
                if (!name.EndsWith(".png") && !name.EndsWith(".jpg") && !name.EndsWith(".gif"))
                {
                    UploadMessage.Text = "The image format for '" + name + "' is not allowed";
                    return;
                }
                string file_path = targetFolder + @"\" + name;
                if (System.IO.File.Exists(file_path))
                {
                    byte[] image_data = File.ReadAllBytes(file_path);
                    ImageConverter ic = new ImageConverter();
                    System.Drawing.Image img = (System.Drawing.Image)ic.ConvertFrom(image_data);
                    Bitmap bitmap = new Bitmap(img);
                    if (bitmap.Width != width || bitmap.Height != height)
                    {
                        UploadMessage.Text = "The image '" + name + "' is not " + Width.Text + " X " + Height.Text;
                        return;
                    }
                    
                    string file_name =  util.FilterWebFileName(name);
                    string save_file_path = State["TempFilesPath"].ToString() + State["Username"].ToString() + "." + file_name;

                    try
                    {
                        if (File.Exists(save_file_path))
                            File.Delete(save_file_path);

                        File.WriteAllBytes(save_file_path, image_data);
                        File.Delete(file_path);
                    }
                    catch
                    {
                        //Trying to overwrite the same file
                    }

                    AmazonS3 s3 = new AmazonS3();
                    string url = s3.UploadFile(State, file_name, save_file_path);
                    if (!url.StartsWith("http"))
                        return ;

                    if (File.Exists(save_file_path))
                        File.Delete(save_file_path);

                    ImageSource.Text = url;
                    UploadMessage.Text = "Upload Successful. Click Close.";
                }
            }
        }
        else
        {
            UploadMessage.Text = "Browse for a file";
        }
    }
}