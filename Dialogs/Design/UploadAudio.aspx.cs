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
using Telerik.Web.UI;

public partial class Dialogs_UploadAudio : System.Web.UI.Page
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

        if (UploadBackground.UploadedFiles.Count > 0)
        {
            string targetFolder = Server.MapPath(UploadBackground.TargetFolder);
            foreach (UploadedFile file in UploadBackground.UploadedFiles)
            {
                string name = file.GetName();
                string file_path = targetFolder + @"\" + name;
                if (file_path.Contains(".wav") && file_path.Contains(".mp3"))
                {
                    UploadMessage.Text = name + "' is not a .mp3 or .wav file";
                    return;
                }

                if (System.IO.File.Exists(file_path))
                {
                    byte[] audio_data = File.ReadAllBytes(file_path);
 
                    string file_name =  name.Replace(" ", "_").Replace("%20", "_");                    
                    string save_file_path = State["TempFilesPath"].ToString() + State["Username"].ToString() + "." + file_name;

                    try
                    {
                        if (File.Exists(save_file_path))
                            File.Delete(save_file_path);

                        File.WriteAllBytes(save_file_path, audio_data);
                        File.Delete(file_path);
                    }
                    catch
                    {
                        //Trying to overwrite the same file
                    }
 
                    AmazonS3 s3 = new AmazonS3();
                    string url = s3.UploadFile(State, file_name, save_file_path);
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

                    ImageSource.Value = url;
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