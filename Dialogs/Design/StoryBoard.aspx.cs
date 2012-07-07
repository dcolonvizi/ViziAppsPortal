using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Web.UI;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Collections;
using System.Text;
using iTextSharp.text.xml;
using iTextSharp.text.html;
using HtmlAgilityPack;

public partial class Dialogs_StoryBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (State == null || State.Count <= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "timeOut('../Default.aspx');", true); return; }

        if (State["SelectedAppType"] != null)
        {
            SelectedAppType.Value = State["SelectedAppType"].ToString();

            int thumbWidth = Constants.IPHONE_DISPLAY_WIDTH/2;
            int thumbHeight = Convert.ToInt32(Constants.IPHONE_SCROLL_HEIGHT_S)/2;

            if (State["SelectedDeviceType"] == null)
            {
                XScaleFactor.Value = (thumbWidth / Constants.IPHONE_DISPLAY_WIDTH_D).ToString();
                YScaleFactor.Value = (thumbHeight / Constants.IPHONE_DISPLAY_HEIGHT_D).ToString();

            }
            else
            {
                switch (State["SelectedDeviceType"].ToString())
                {
                    case Constants.IPHONE:
                    case Constants.ANDROID_PHONE:
                    default:
                        XScaleFactor.Value = (thumbWidth / Constants.IPHONE_DISPLAY_WIDTH_D).ToString();
                        YScaleFactor.Value = (thumbHeight / Constants.IPHONE_DISPLAY_HEIGHT_D).ToString();
                        break;
                    case Constants.IPAD:
                        XScaleFactor.Value = (thumbWidth / Constants.IPAD_DISPLAY_WIDTH_D).ToString();
                        YScaleFactor.Value = (thumbHeight / Constants.IPAD_DISPLAY_HEIGHT_D).ToString();
                        break;
                    case Constants.ANDROID_TABLET:
                        XScaleFactor.Value = (thumbWidth / Constants.ANDROID_TABLET_DISPLAY_WIDTH_D).ToString();
                        YScaleFactor.Value = (thumbHeight / Constants.ANDROID_TABLET_DISPLAY_HEIGHT_D).ToString();
                        break;
                }
            }
        }
        try
        {
            if ( State["UseFullPageImage"] == null)
                ChangeScale.Text = "Scale Up Images";
            else
                ChangeScale.Text = "Scale Down Images";

             LoadStoryBoard();
        }
        catch (Exception ex)
        {
            util.LogError(State, ex);
        }
    }
    private void LoadStoryBoard()
    {
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        Util util = new Util();
        try
        {
            string app_id = null;
            PageTreeView.Nodes.Clear();
            if (State["SelectedApp"] == null || State["SelectedApp"].ToString().Contains("->"))
            {
                ExportDesign.Style.Value = "display:none";

                if (State["SelectedApp"] == null)
                {
                    string app  = Request.QueryString.Get("app");
                    if (app == null)
                        return;
                    State["CustomerID"] = Request.QueryString.Get("customerid");
                    State["SelectedApp"] = app;
                    app_id = util.GetAppIDFromAppName(State, app);
               }
                else
                    return;
            }
            if (app_id == null)
                app_id = util.GetAppID(State);

            ExportDesign.Style.Value = "";

            XmlUtil x_util = new XmlUtil();
            XmlDocument doc = x_util.GetStagingAppXml(State);
            if (doc == null)
                return;

            string[] pages = x_util.GetAppPageNames(State,  State["SelectedApp"].ToString());
            State["PageViewAppID"] = app_id;
            for (int i = 0; i < pages.Length; i++)
            {
                RadTreeNode page_node = new RadTreeNode();
                page_node.CssClass = "RadTreeView";
                page_node.Category = "page";
                page_node.PostBack = false;

                Control PageControl = LoadControl("../../Controls/PageView.ascx", pages[i]);
                page_node.Controls.Add(PageControl);
                PageTreeView.Nodes.Add(page_node);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
    private UserControl LoadControl(string UserControlPath, params object[] constructorParameters)
    {
        try
        {
            List<Type> constParamTypes = new List<Type>();
            foreach (object constParam in constructorParameters)
            {
                constParamTypes.Add(constParam.GetType());
            }

            UserControl ctl = Page.LoadControl(UserControlPath) as UserControl;

            // Find the relevant constructor
            ConstructorInfo constructor = ctl.GetType().BaseType.GetConstructor(constParamTypes.ToArray());

            //And then call the relevant constructor
            if (constructor == null)
            {
                throw new MemberAccessException("The requested constructor was not found on : " + ctl.GetType().BaseType.ToString());
            }
            else
            {
                try
                {
                    constructor.Invoke(ctl, constructorParameters);
                }
                catch (Exception ex)
                {
                    StringBuilder error = new StringBuilder("LoadControl in StoryBoard could not construct: ");
                    foreach (object constParam in constructorParameters)
                    {
                        error.Append(constParam.ToString());
                    }
                    throw new Exception(error.ToString() + " - " + ex.Message + ": " + ex.StackTrace);
                }
            }

            // Finally return the fully initialized UC
            return ctl;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
    protected void ChangeScale_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;
 
        if ( State["UseFullPageImage"] == null)
        {
             State["UseFullPageImage"] = true;
            ChangeScale.Text = "Scale Down Images";
            PageTreeView.Width = 800;
        }
        else
        {
             State["UseFullPageImage"] = null;
            ChangeScale.Text = "Scale Up Images";
            PageTreeView.Width = 440;
        }
        LoadStoryBoard();
    }
    protected void ExportDesign_Click(object sender, EventArgs e)
    {
        Util util = new Util();
        Hashtable State = (Hashtable)HttpRuntime.Cache[Session.SessionID];
        if (util.CheckSessionTimeout(State, Response, "../Default.aspx")) return;

        try
        {
            XmlUtil x_util = new XmlUtil();

            //get PDF file path
            string path = Server.MapPath(".") + @"\PDF";
            util.CheckDirectory(path);

            //delete previous files older than 1 minute
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.LastWriteTime < DateTime.Now.AddSeconds(-10.0D))
                    File.Delete(file);
            }
            string file_name = State["Username"].ToString() + "_" + State["SelectedApp"].ToString() + ".pdf";
            file_name = file_name.Replace(" ", "_");
            string save_file_path = path + @"\" + file_name;

            //open PDF doc
            var doc = new Document(PageSize.LETTER);
            PdfWriter.GetInstance(doc, new FileStream(save_file_path, FileMode.Create));
            doc.Open();

            //get pages
            string[] pages = x_util.GetAppPageNames(State, State["SelectedApp"].ToString());

            //push content to PDF
            foreach (string page in pages)
            {
                //get page image
                string url = util.GetAppPageImage(State, State["PageViewAppID"].ToString(), page);

                string[] fields = x_util.GetAppPageFields(State, State["SelectedApp"].ToString(), page);
                 StringBuilder fields_string = new StringBuilder();
                bool isFirst = true;
                foreach (string field in fields)
                {
                    if (isFirst)
                        isFirst = false;
                    else
                        fields_string.Append(", ");
                    fields_string.Append(field);

                }

                doc.Add(new Paragraph("Page: " + page));
                if (fields.Length == 0)
                {
                    doc.Add(new Paragraph("There are no fields on this page"));
                    doc.NewPage();
                    continue;
                }
                doc.Add(new Paragraph("Fields: " + fields_string.ToString()));
                doc.Add(new Paragraph(" "));

                try
                {
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(new Uri(url));

                    image.Border = Rectangle.BOX;
                    image.BorderColor = BaseColor.BLACK;
                    image.BorderWidth = 1f;

                    if (State["SelectedDeviceType"].ToString() == Constants.IPAD ||
                        State["SelectedDeviceType"].ToString() == Constants.ANDROID_TABLET)
                        image.ScaleToFit(450f, 600f);
                    else
                        image.ScaleToFit(320f, 460f);

                    doc.Add(image);
                }
                catch (Exception ex0)
                { } //bad image url exception - skip
                doc.NewPage();

            }
            doc.Close();

            doPopup.Text = "PDF/" + file_name;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }
    }
}