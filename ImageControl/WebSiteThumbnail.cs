using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Collections;

namespace PAB.WebControls
{
    public class WebSiteThumbnail
    {
        private string url = null;      
        private Bitmap bmp = null;
        public Bitmap Image 
        {
            get 
            { 
                return bmp; 
            } 
        }
        private ManualResetEvent mre = new ManualResetEvent(false);
        private int timeout = 5; // this could be adjusted up or down
        private int thumbWidth;
        private int thumbHeight;
        private int width;
        private int height;
        private string absolutePath;
        #region Static Methods
        public static Bitmap GetSiteThumbnail(string url, int width, int height, int thumbWidth, int thumbHeight)
        {
            WebSiteThumbnail thumb = new WebSiteThumbnail(url, width, height, thumbWidth, thumbHeight);
            Bitmap b = thumb.GetScreenShot();
            if (b == null)
                b = (Bitmap)System.Drawing.Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PAB.WebControls.Notavailable.jpg"));
            return b;
        }

        public static Bitmap GetSiteThumbnail(string url, int width, int height, int thumbWidth, int thumbHeight,string absolutePath)
        {           
            WebSiteThumbnail thumb = new WebSiteThumbnail(url, width, height, thumbWidth, thumbHeight,absolutePath );
            Bitmap b = thumb.GetScreenShot();
            if (b == null)
                b = (Bitmap)System.Drawing.Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("PAB.WebControls.Notavailable.jpg"));
             return b;
        }
        #endregion

        #region Ctors
        public WebSiteThumbnail(string url, int width, int height,int thumbWidth, int thumbHeight)
        {
            this.url = url;
            this.width = width;
            this.height = height;
            this.thumbHeight = thumbHeight;
            this.thumbWidth = thumbWidth;
        }
        public WebSiteThumbnail(string url, int width, int height, int thumbWidth, int thumbHeight,string absolutePath)
        {
            this.url = url;
            this.width = width;
            this.height = height;
            this.thumbHeight = thumbHeight;
            this.thumbWidth = thumbWidth;
            this.absolutePath = absolutePath;
        }
        #endregion

        #region ScreenShot
        public Bitmap GetScreenShot()
        {
             string fileName = url.Replace("http://", "") + ".jpg";
             fileName = System.Web.HttpUtility.UrlEncode(fileName);
            if (absolutePath != null &&  File.Exists(absolutePath + fileName))
            {                
                bmp = (Bitmap)System.Drawing.Image.FromFile(absolutePath + fileName);                   
            }
            else
            {
                Thread t = new Thread(new ThreadStart(_GetScreenShot));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                mre.WaitOne();
                t.Abort();
            }
            return bmp;
        }
        #endregion
        private void _GetScreenShot()
        {
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.ScrollBarsEnabled = false;
            DateTime time = DateTime.Now;
            webBrowser.Navigate(url);
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);
            while (true)
            {
                Thread.Sleep(0);
                TimeSpan elapsedTime = DateTime.Now - time;
                if (elapsedTime.Seconds >= timeout)
                {
                    mre.Set();
                }
                Application.DoEvents();
            }


        }
        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
 
            WebBrowser webBrowser = (WebBrowser)sender;
            webBrowser.ClientSize = new Size(this.width, this.height);
            webBrowser.ScrollBarsEnabled = false;
            bmp = new Bitmap(webBrowser.Bounds.Width, webBrowser.Bounds.Height);
            webBrowser.BringToFront();
            webBrowser.DrawToBitmap(bmp, webBrowser.Bounds); 
            Image img = bmp.GetThumbnailImage(thumbWidth, thumbHeight, null, IntPtr.Zero);
             bmp = (Bitmap)img;     
            if (absolutePath != null)
            {
                img.Save(absolutePath);
            }
            /*string fileName = url.Replace("http://", "") + ".jpg";
            fileName = System.Web.HttpUtility.UrlEncode(fileName);
          if (absolutePath != null && !File.Exists(absolutePath + fileName))
          {
              img.Save(absolutePath + fileName);
          }  */
                 
            webBrowser.Dispose();
          if (mre != null) 
              mre.Set();
        }
 
        public void Dispose()
        {
            if (bmp != null) this.bmp.Dispose();
        }
    }
}