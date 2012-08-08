using System;
using System.Collections.Generic;
using System.Configuration;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Amazon;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for AmazonSES
/// </summary>
public class AmazonSES
{
	public AmazonSES()
	{
	}
    public string SendEmail(Hashtable State, string from, string to, string cc, string bcc, string subject, string body, string attach_path, bool isBodyHtml)
    {
        string AWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        string AWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];

        AmazonSimpleEmailService ses = AWSClientFactory.CreateAmazonSimpleEmailServiceClient(AWSAccessKey, AWSSecretKey);

        SendEmailRequest request = new SendEmailRequest();
        Destination destination = new Destination();

        List<String> To = new List<String>(to.Replace(", ", ",").Split(','));
        destination.WithToAddresses(To);

        if (cc != null && cc.Length > 0)
        {
            List<String> CC = new List<String>(cc.Replace(", ", ",").Split(','));
            destination.WithCcAddresses(CC);
        }

        if (bcc != null && bcc.Length > 0)
        {
            List<String> BCC = new List<String>(bcc.Replace(", ", ",").Split(','));
            destination.WithBccAddresses(BCC);
        }

        request.WithDestination(destination);

        Content SESsubject = new Content();
        SESsubject.WithCharset("UTF-8");
        SESsubject.WithData(subject);

        Body SESbody = new Body();
        if (isBodyHtml)
        {
            Content SEShtml = new Content();
            SEShtml.WithCharset("UTF-8");
            SEShtml.WithData(body); 
            SESbody.WithHtml(SEShtml);
        }
        else
        {
            Content text = new Content();
            text.WithCharset("UTF-8");
            text.WithData(body);
            SESbody.WithText(text);
        }

        Message message = new Message();
        message.WithBody(SESbody);
        message.WithSubject(SESsubject);

        request.WithMessage(message);
        request.WithSource(from);

        List<String> replyto = new List<String>(from.Replace(", ", ",").Split(','));
        request.WithReplyToAddresses(replyto);

       /* if (returnPath != null)
        {
            request.WithReturnPath(returnPath);
        }*/

        try
        {
            SendEmailResponse response = ses.SendEmail(request);
            SendEmailResult result = response.SendEmailResult;
            return "OK";
        }
        catch (Exception ex)
        {
            Util util = new Util();
            util.LogError(State, ex);
            return ex.Message;
        }
    }
}