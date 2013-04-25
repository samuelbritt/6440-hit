using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using EASendMail; //add EASendMail namespace

using System.Diagnostics;
using HealthVaultHelper;

using Microsoft.Health.PatientConnect;
using System.Collections.ObjectModel;

namespace mysendemail
{
    public class HotmailEmail
    {
        public HotmailEmail()
        {

        }

        public void Sender(String msg, String email)
        {
            String subject = "Clinical Trial HealthVault Account Authentication Email";
            if (email == "gtvistaorganicit@gmail.com")
                subject = "Clinical Trial Data for Patient";
            Sender(msg, subject, string.Empty, email);
        }
        public void Sender(String msg, String subject, String attachmentFileName, String email)
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();
            Debug.WriteLine(msg);
            //
            // Your Hotmail email address
            //oMail.From = "organicit@direct.i3l.gatech.edu";
            oMail.From = "organicit@hotmail.com";
            // Set recipient email address
            oMail.To = email;

            // Set email subject
            oMail.Subject = subject;

            // Set email body
            oMail.TextBody = msg;

            if (!string.IsNullOrEmpty(attachmentFileName))
                oMail.AddAttachment(attachmentFileName);

            // Hotmail SMTP server address
            SmtpServer oServer = new SmtpServer("smtp.live.com");
            // Hotmail user authentication should use your 
            // email address as the user name. 
            oServer.User = "organicit@hotmail.com";
            oServer.Password = "6440mark";

            // detect SSL/TLS connection automatically
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            //oServer.ConnectType = SmtpConnectType.ConnectNormal;

            try
            {
                Debug.WriteLine("start to send email over SSL...");
                oSmtp.SendMail(oServer, oMail);
                Debug.WriteLine("email was sent successfully!");
            }
            catch (Exception ep)
            {
                Debug.WriteLine("failed to send email with the following error:");
                Debug.WriteLine(ep.Message);
            }
            Debug.WriteLine(msg);

        }
    }
}