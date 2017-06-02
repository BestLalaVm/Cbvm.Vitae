using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Mail;

namespace XMCAServer.Data
{
    public static class EmailHelper
    {
        private static SmtpClient _Client;
        private static SmtpClient Client
        {
            get
            {
                if (_Client == null)
                {
                    _Client = new SmtpClient(AppConfig.MailHostName, AppConfig.MailPort);
                    _Client.Credentials = new System.Net.NetworkCredential(AppConfig.MailUserName, AppConfig.MailPassword);
                }
                return _Client;
            }
        }

        public static bool SendEmail(string subject, string body, string receiveEmail, string fromEmail, string fromName)
        {
            try
            {
                var mailMessage = new MailMessage();
                //mailMessage.Sender = new MailAddress(fromEmail, fromName);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.To.Add(new MailAddress(receiveEmail));
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new MailAddress(AppConfig.MailUserName, AppConfig.MailUserName);
                Client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex.ToString(), LogType.Error);
            }
            return false;
        }
    }
}
