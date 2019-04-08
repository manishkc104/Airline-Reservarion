using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public static class MailManagement
    {
        public static bool SendEmail(string subject, string toEmail, string message, string fromEmail = "")
        {
            bool isSuccess = true;
            try
            {
                int portNumber = Convert.ToInt32(GetPort());
                bool enableSSL = true;
                string emailFrom = fromEmail;
                string email = GetDefaultFromEmail();

                var smtpAddress = GetIPAddress();

                if (fromEmail == "")
                {
                    emailFrom = GetDefaultFromEmail();
                }

                var password = GetDefaultPassword();

                string emailTo = toEmail;
                string body = message;

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;
                  
                    ////
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.EnableSsl = enableSSL;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(email, password);
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception e)
            {
                var trace = e.StackTrace;
                // AssignOperationMessage.messageList.Add(new MessageList { IsSuccess = false, Message = "failed sending mail!!", OperationName = "Mail Send" });
                isSuccess = false;
            }
            return isSuccess;
        }
       

        private static string GetDefaultFromEmail()
        {
            string fromEmail = ReadConfigData.GetAppSettingsValue("FromEmail");
            return fromEmail;
        }

        private static string GetDefaultPassword()
        {
            string fromEmailPassword = ReadConfigData.GetAppSettingsValue("EmailPassword");
            return fromEmailPassword;
        }

        private static string GetDefaultfileName()
        {
            string defaultFileName = "TestAttachment";
            return defaultFileName;
        }

        private static string GetFileServerAddress()
        {
            string serverAddress = "C:\\";
            return serverAddress;
        }

        private static string GetIPAddress()
        {
            string nedIp = ReadConfigData.GetAppSettingsValue("Host");
            return nedIp;
        }

        private static string GetPort()
        {
            string nedIp = ReadConfigData.GetAppSettingsValue("Port");
            return nedIp;
        }
    }
}
