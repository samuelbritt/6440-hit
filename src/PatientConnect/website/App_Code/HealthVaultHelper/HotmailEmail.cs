using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System;
using System.Collections.Generic;
using System.Text;
using EASendMail; //add EASendMail namespace

using System.Diagnostics;
using HealthVaultHelper;

using Microsoft.Health.PatientConnect;
using System.Collections.ObjectModel;

namespace mysendemail
{
    class HotmailEmail
    {
         public void Sender(String msg)
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();
            Debug.WriteLine(msg);
            //
            // Your Hotmail email address
            oMail.From = "organicit@direct.i3l.gatech.edu";
            
            // Set recipient email address
            oMail.To = "organicit@direct.i3l.gatech.edu";
            
            // Set email subject
            oMail.Subject = "tEST i gUESS";
            
            // Set email body
            oMail.TextBody = msg;

            // Hotmail SMTP server address
            SmtpServer oServer = new SmtpServer("mail.i3l.gatech.edu");
            
            // Hotmail user authentication should use your 
            // email address as the user name. 
            oServer.User = "organicit";
            oServer.Password = "organicit";

            // detect SSL/TLS connection automatically
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

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