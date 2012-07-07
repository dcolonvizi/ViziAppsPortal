using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.Text;

/// <summary>
/// Summary description for Email
/// </summary>
public class Email
{
    protected SmtpClient mailClient;
	public Email()
	{
	}
    public string SendEmail(Hashtable State,string from, string to, string cc, string bcc, string subject, string body, string attach_path,bool isBodyHtml)
    {
        MailMessage mail = new MailMessage(from, to, subject, body);
        mail.BodyEncoding = Encoding.ASCII;
        mail.IsBodyHtml = isBodyHtml;

        if (cc.Length > 0)
        {
            mail.CC.Add(cc);
        }
        if (bcc.Length > 0)
        {
            mail.Bcc.Add(bcc);
        }
        if (attach_path.Length > 0)
        {
            mail.Attachments.Add(new Attachment(attach_path));
        }

        string smtp_port = State["smtp_port"].ToString();
        string smtp = State["smtp"].ToString();
        if (smtp_port == null || smtp_port.Length == 0)
        {
            mailClient = new SmtpClient(smtp);
        }
        else
        {
            int port = Convert.ToInt32(smtp_port);
            mailClient = new SmtpClient(smtp, port);
        }

        try
        {
            string smtp_username = State["smtp_username"].ToString();
            if (smtp_username.Length > 0)
                mailClient.Credentials = new System.Net.NetworkCredential(smtp_username,
                    State["smtp_password"].ToString());
            
            mailClient.Send(mail);
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
