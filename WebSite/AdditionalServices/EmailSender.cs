using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AdditionalServices
{
    public class EmailSender : IEmailSender
    {
        private SmtpSection _config;

        //public static SmtpSection Config
        //{
        //    get
        //    {
        //        if (_config == null)
        //        {
        //            _config = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;

        //        }
        //        return _config;
        //    }
        //}

        public EmailSender(SmtpSection config)
        {
            _config = config;
        }

        public void SendMail(string email, string subject, string body, MailAddress mailAddress = null)
        {
            try
            {
                if (mailAddress == null)
                {
                    mailAddress = new MailAddress(_config.From);
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
                    Host = _config.Network.Host,
                    Port = _config.Network.Port,
                    UseDefaultCredentials = _config.Network.DefaultCredentials,
                    EnableSsl = _config.Network.EnableSsl,
                    Credentials =
                        new NetworkCredential(_config.Network.UserName,
                                              _config.Network.Password),
                    DeliveryMethod = _config.DeliveryMethod
                };
                client.Send(message);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
