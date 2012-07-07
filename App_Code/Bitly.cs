using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;


    public class BitlyData
    {
        #region Properties

        public static string APIKEY { get; set; }
        public static string LoginName { get; set; }

        public static string StatusCode { get; set; }
        public static string StatusDesc { get; set; }
        public static string ShortURL { get; set; }
        public static string LongURL { get; set; }
        public static string HashCode { get; set; }
        public static string GlobalCode { get; set; }
        public static string NewHash { get; set; }

        #endregion
    }

    public class Bitly
    {

        #region Bit.Ly Variables

        private static string URL = "http://api.bit.ly/v3/shorten?login=";

        private static string BitResponse = "";

        #endregion

        #region Enums

        public enum Format
        {
            XML,
            JSON,
            TXT
        }

        #endregion

        public static string ShortURL(string LongURL, Format ReqFormat = Format.TXT)
        {
            string sURL = string.Empty;
            //string BitLyURL = URL + BitlyData.LoginName + "&amp;apikey=" + BitlyData.APIKEY + "&amp;longUrl=" + LongURL + "&amp;format=";
            string BitLyURL = URL + BitlyData.LoginName + "&apikey=" + BitlyData.APIKEY + "&longUrl=" + LongURL + "&format=";

            if (ReqFormat == Format.JSON)
            {
                throw new NotImplementedException("This method is not implemented yet!");
            }

            if (ReqFormat == Format.XML)
            {
                BitResponse = new WebClient().DownloadString(BitLyURL + "xml");
                string Response = BitResponse;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Response);
                XmlNodeList XlistResponse = doc.GetElementsByTagName("response");

                foreach (XmlNode XResnode in XlistResponse)
                {
                    XmlElement ResElm = (XmlElement)XResnode;

                    BitlyData.StatusCode = ResElm.GetElementsByTagName("status_code")[0].InnerText;

                    if (BitlyData.StatusCode != "200")
                    {
                        BitlyData.StatusDesc = ResElm.GetElementsByTagName("status_txt")[0].InnerText;
                        return BitlyData.StatusDesc;
                    }
                }

                if (BitlyData.StatusCode == "200")
                {

                    XmlNodeList XlistData = doc.GetElementsByTagName("data");

                    foreach (XmlNode XDatanode in XlistData)
                    {
                        XmlElement DataElm = (XmlElement)XDatanode;

                        BitlyData.ShortURL = DataElm.GetElementsByTagName("url")[0].InnerText;
                        BitlyData.LongURL = DataElm.GetElementsByTagName("long_url")[0].InnerText;
                        BitlyData.HashCode = DataElm.GetElementsByTagName("hash")[0].InnerText;
                        BitlyData.GlobalCode = DataElm.GetElementsByTagName("global_hash")[0].InnerText;
                        BitlyData.NewHash = DataElm.GetElementsByTagName("new_hash")[0].InnerText;
                    }
                }
            }

            if (ReqFormat == Format.TXT)
            {
                if (ReqFormat == Format.TXT)
                {
                    BitResponse = new WebClient().DownloadString(BitLyURL + "txt");
                    string Response = BitResponse;

                    BitlyData.ShortURL = Response;
                }
            }

            return sURL = BitlyData.ShortURL;
        }

        public static string ReverseShortURL(string ShortURL)
        {
            HttpWebRequest Webrequest = (HttpWebRequest)HttpWebRequest.Create(ShortURL);
            HttpWebResponse Webresponse = (HttpWebResponse)Webrequest.GetResponse();
            Uri uri = Webresponse.ResponseUri;
            return uri.AbsoluteUri;
        }

        public static string GetQRCodeURL(string ShortURL)
        {
            string QRCodeURL = string.Empty;
            QRCodeURL = ShortURL + ".qrcode";
            return QRCodeURL;
        }
    }
