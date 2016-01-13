using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace WebSite.Tools
{
    public static class MailSender
    {
        private static SmtpSection _config;

        public static SmtpSection Config
        {
            get
            {
                if (_config == null)
                {
                    _config =  ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;

                }
                return _config;
            }
        }

        public static void SendMail(string email, string subject, string body, MailAddress mailAddress = null)
        {
            try
            {

                if (mailAddress == null)
                {
                    mailAddress = new MailAddress(Config.From);
                }
                MailMessage message = new MailMessage(
                    mailAddress,
                    new MailAddress(email))
                {
                    Subject = subject,
                    BodyEncoding = Encoding.UTF8,
                    Body = body,
                    IsBodyHtml = true,
                    SubjectEncoding = Encoding.UTF8
                };
                SmtpClient client = new SmtpClient
                {
                    Host = Config.Network.Host,
                    Port = Config.Network.Port,
                    UseDefaultCredentials = Config.Network.DefaultCredentials,
                    EnableSsl = Config.Network.EnableSsl,
                    Credentials =
                        new NetworkCredential(Config.Network.UserName,
                                              Config.Network.Password),
                    DeliveryMethod = Config.DeliveryMethod
                };
                client.Send(message);

            }
            catch (Exception ex)
            {

            }
        }
    }
}