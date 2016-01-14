using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IEmailSender
    {
        void SendMail(string email, string subject, string body, MailAddress mailAddress= null);

    }
}
