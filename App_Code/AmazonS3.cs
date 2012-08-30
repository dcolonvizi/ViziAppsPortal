using System;
using System.Data;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Telerik.Web.UI;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

/// <summary>
/// Summary description for AmazonS3
/// </summary>
public class AmazonS3
{
    public const int ONE_MINUTE = 60 * 1000;

	public AmazonS3()
	{
	}
    public string UploadFile(Hashtable State, string file_name, string local_file_path)
    {
        string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
         string Bucket = null;
         if (file_name.EndsWith(".html") || file_name.EndsWith(".xml"))
            Bucket = ConfigurationManager.AppSettings["WebAppBucket"];
        else
           Bucket = ConfigurationManager.AppSettings["ImageBucket"];

        string key = State["Username"].ToString() + "/" + file_name;
        TransferUtility transferUtility = new TransferUtility(AWSAccessKey, AWSSecretKey);
        TransferUtilityUploadRequest request = null;
        string url = "https://s3.amazonaws.com/" + Bucket + "/" + key;
        try
        {
           // // Make sure the bucket exists
           // transferUtility.S3Client.PutBucket(new PutBucketRequest().WithBucketName(Bucket));
           if (S3ObjectExists(Bucket, key))
           {
                DeleteS3Object(Bucket, key);
           }
           request = new TransferUtilityUploadRequest()
           .WithBucketName(Bucket)
           .WithFilePath(local_file_path)
           .WithTimeout(ONE_MINUTE)
           .WithKey(key)
           .WithCannedACL(S3CannedACL.PublicRead);
            transferUtility.Upload(request);
        }
        catch (AmazonS3Exception ex)
        {
            return ex.Message + ": " + ex.StackTrace;
        }

        return url;
    }
    public string UploadFileWithKey(Hashtable State, string file_name, string local_file_path,string key)
    {
        string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        string Bucket = null;
        if (file_name.EndsWith(".html") || file_name.EndsWith(".xml"))
            Bucket = ConfigurationManager.AppSettings["WebAppBucket"];
        else
            Bucket = ConfigurationManager.AppSettings["ImageBucket"];

        TransferUtility transferUtility = new TransferUtility(AWSAccessKey, AWSSecretKey);
        TransferUtilityUploadRequest request = null;
        string url = "https://s3.amazonaws.com/" + Bucket + "/" + key;
        try
        {
            // // Make sure the bucket exists
            // transferUtility.S3Client.PutBucket(new PutBucketRequest().WithBucketName(Bucket));
            if (S3ObjectExists(Bucket, key))
            {
                DeleteS3Object(Bucket, key);
            }
            request = new TransferUtilityUploadRequest()
            .WithBucketName(Bucket)
            .WithFilePath(local_file_path)
            .WithTimeout(ONE_MINUTE)
            .WithKey(key)
            .WithCannedACL(S3CannedACL.PublicRead);
            transferUtility.Upload(request);
        }
        catch (AmazonS3Exception ex)
        {
            return ex.Message + ": " + ex.StackTrace;
        }

        return url;
    }
    public bool S3ObjectExists(string bucket, string key)
    {
        string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

        using (AmazonS3Client client = new AmazonS3Client(AWSAccessKey, AWSSecretKey))
        {
            GetObjectRequest request = new GetObjectRequest();
            request.BucketName = bucket;
            request.Key = key;

            try
            {
                S3Response response = client.GetObject(request);
                if (response.ResponseStream != null)
                {
                    return true;
                }
            }
            catch (AmazonS3Exception)
            {
                return false;
            }
            catch (WebException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        return false;
    }
    public string DeleteS3Object(string Bucket, string key)
    {
        string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        TransferUtility transferUtility = new TransferUtility(AWSAccessKey, AWSSecretKey);
        try
        {
            DeleteObjectRequest request = new DeleteObjectRequest();
             request.WithBucketName(Bucket)
                .WithKey(key);
            using (DeleteObjectResponse response = transferUtility.S3Client.DeleteObject(request))
            {
                WebHeaderCollection headers = response.Headers;
             }
        }
        catch (AmazonS3Exception ex)
        {
           return ex.Message + ": " + ex.StackTrace;
        }

        return "OK";
    }

    public string DeleteImageFile(Hashtable State, string url)
    {
        string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        string Bucket = ConfigurationManager.AppSettings["ImageBucket"];
        TransferUtility transferUtility = new TransferUtility(AWSAccessKey, AWSSecretKey);
        try
        {
            DeleteObjectRequest request = new DeleteObjectRequest();
            string file_name = url.Substring(url.LastIndexOf("/") + 1);
            string key = State["Username"].ToString() + "/" + file_name;
            request.WithBucketName(Bucket)
                .WithKey(key);
            using (DeleteObjectResponse response = transferUtility.S3Client.DeleteObject(request))
            {
                WebHeaderCollection headers = response.Headers;
             }
        }
        catch (AmazonS3Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            return ex.Message + ": " + ex.StackTrace;
        }

        return "OK";
    }
    public ArrayList GetStockImageUrls(Hashtable State)
    {
        ArrayList image_list = new ArrayList();
        string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        string Bucket = ConfigurationManager.AppSettings["ImageBucket"];
        Amazon.S3.AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(AWSAccessKey, AWSSecretKey);
        try
        {
            ListObjectsRequest request = new ListObjectsRequest();
            request.BucketName = Bucket;

            // list only things starting with "apps/images"
           
            string prefix = "https://s3.amazonaws.com/" + Bucket + "/";
            request.WithPrefix("apps/images/bars");
            image_list = GetImages(client, request, prefix, image_list);

            request.WithPrefix("apps/images/blank_buttons");
            image_list = GetImages(client, request, prefix, image_list);

            request.WithPrefix("apps/images/jquery_buttons");
            image_list = GetImages(client, request, prefix, image_list);

            request.WithPrefix("apps/images/bw_dark_icons");
            image_list = GetImages(client,request,prefix, image_list);

            request.WithPrefix("apps/images/bw_light_icons");
            image_list = GetImages(client, request, prefix, image_list);
            
            request.WithPrefix("apps/images/icon_buttons");
            image_list = GetImages(client, request, prefix, image_list);
            
            request.WithPrefix("apps/images/navigation_buttons");
            image_list = GetImages(client, request, prefix, image_list);

            request.WithPrefix("apps/images/ratings");
            image_list = GetImages(client, request, prefix, image_list);
        }
        catch (AmazonS3Exception ex)
        {
            Util util = new Util();
            util.LogError(State,ex);
            throw new Exception( ex.Message + ": " + ex.StackTrace);
        }

        return image_list;
    }
    public ArrayList GetAccountImageArchiveUrls(Hashtable State)
    {
        ArrayList raw_image_list = new ArrayList();
        ArrayList image_list = new ArrayList();
        string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        string Bucket = ConfigurationManager.AppSettings["ImageBucket"];
        Amazon.S3.AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(AWSAccessKey, AWSSecretKey);
        try
        {
            ListObjectsRequest request = new ListObjectsRequest();
            request.BucketName = Bucket;

            // list only things starting with "apps/images"

            string prefix = "https://s3.amazonaws.com/" + Bucket + "/";
            request.WithPrefix(State["Username"].ToString());
            raw_image_list = GetImages(client, request, prefix, raw_image_list);
            foreach (string url in raw_image_list)
            {
                if (url.EndsWith(".jpg") || url.EndsWith(".png") || url.EndsWith(".gif"))
                {
                    image_list.Add(url);
                }
            }
        }
        catch (AmazonS3Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }

        return image_list;
    }
    public ArrayList GetAppImageArchiveUrls(Hashtable State)
    {
        ArrayList raw_image_list = new ArrayList();
        ArrayList image_list = new ArrayList();
        string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        string Bucket = ConfigurationManager.AppSettings["ImageBucket"];
        Amazon.S3.AmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(AWSAccessKey, AWSSecretKey);
        try
        {
            ListObjectsRequest request = new ListObjectsRequest();
            request.BucketName = Bucket;
            string prefix = "https://s3.amazonaws.com/" + Bucket + "/";
            request.WithPrefix(State["Username"].ToString());
            raw_image_list = GetImages(client, request, prefix, raw_image_list);
            string app_string = "/" + State["SelectedApp"].ToString().Replace(" ","_") + "/";
            foreach (string url in raw_image_list)
            {
                if ( url.Contains(app_string))
                {
                    image_list.Add(url);
                }
            }
        }
        catch (AmazonS3Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            throw new Exception(ex.Message + ": " + ex.StackTrace);
        }

        return image_list;
    }
    private ArrayList GetImages(Amazon.S3.AmazonS3 client, ListObjectsRequest request, string prefix, ArrayList image_list)
    {
        using (ListObjectsResponse response = client.ListObjects(request))
        {
            foreach (S3Object entry in response.S3Objects)
            {
                if (!entry.Key.Contains("."))
                    continue;

                image_list.Add(prefix + entry.Key);
            }
        }
        return image_list;
    }
}