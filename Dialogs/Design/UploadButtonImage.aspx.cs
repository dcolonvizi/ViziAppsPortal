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

public partial class Dialogs_UploadButtonImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }
    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;
        
        int width = 1024;
        int height = 1024;

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
                    if (bitmap.Width > width || bitmap.Height > height)
                    {
                        UploadMessage.Text = "The image '" + name + "' is greater than 1024 w X 1024 h pixels";
                        return;
                    }
                    
                    string file_name = State["Username"].ToString()+ "." + util.FilterWebFileName(name);
                    string save_file_path = State["TempFilesPath"].ToString() + file_name;

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
                    // Get the Image size
                    System.Drawing.Image objImage = System.Drawing.Image.FromFile(save_file_path);
                    ImageWidth.Value = objImage.Width.ToString() ;
                    ImageHeight.Value = objImage.Height.ToString() ;

                    AmazonS3 s3 = new AmazonS3();
                    string url = s3.UploadFile((Hashtable)HttpRuntime.Cache[Session.SessionID], file_name, save_file_path);
                    if (!url.StartsWith("http"))
                        return;

                    try
                    {
                        if (File.Exists(save_file_path))
                            File.Delete(save_file_path);
                    }
                    catch (Exception ex)
                    {
                    }

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