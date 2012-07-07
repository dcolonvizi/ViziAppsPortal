using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Net;
using System.IO;
using System.Drawing.Imaging;

public partial class Dialogs_AppImages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        if (!IsPostBack)
        {
            AmazonS3 S3 = new AmazonS3();
            ArrayList images = S3.GetAppImageArchiveUrls(State);
            images.Sort();
            ImageList.Items.Clear();
            foreach (string url in images)
            {
                string name = url.Substring(url.LastIndexOf("/") + 1);
                ImageList.Items.Add(new RadComboBoxItem(name, url));
            }
        }
    }
    protected void imageList_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        ImageList.FindItemByText(e.Text).Selected = true;
        SelectedImage.ImageUrl = e.Value;
        ScaleToFit(e.Value);   
    }
    protected void Zoom_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;
        
        int index = ((RadButton)sender).SelectedToggleStateIndex;
       if (index == 0)
       {
           ScaleToFit(SelectedImage.ImageUrl);
       }
       else
       {
           SelectedImage.Style.Value = "";
       }
    }
    protected void ScaleToFit(string url)
    {
        WebRequest request = WebRequest.Create(url);
        WebResponse response = request.GetResponse();
        System.Drawing.Image image = System.Drawing.Image.FromStream(response.GetResponseStream());

        int width = image.Width;
        int height = image.Height;
        bool use_zoom = false;
        if (width > height)
        {
            if (width > 1180)
            {
                SelectedImage.Style.Value = "width:1180px;height:auto;";
                use_zoom = true;
            }
            else if (height > 910)
            {
                SelectedImage.Style.Value = "width:auto;height:910px;";
                use_zoom = true;
            }
        }
        else
        {
            if (height > 910)
            {
                SelectedImage.Style.Value = "width:auto;height:910px;";
                use_zoom = true;
            }
            else if (width > 1180)
            {
                SelectedImage.Style.Value = "width:1180px;height:auto;";
                use_zoom = true;
            }
        }
        if (use_zoom)
            Zoom.Style.Value = "display:inline";
        else
            Zoom.Style.Value = "display:none";
    }
}