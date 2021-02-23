using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.Services
{
    public static class EmailSendServiceClass
    {
        public static MailMessage CreateMessage(string from, string to, string subject, string body)
        {            
            if (!MailAddress.TryCreate(from, out MailAddress CreatedFrom) || !MailAddress.TryCreate(to, out MailAddress CreatedTo))
            {
                throw new InvalidCastException("Некорректный формат e-mail адреса");
            }

            MailMessage msg = new(CreatedFrom, CreatedTo);
            msg.Subject = subject;
            msg.Body = body;
            return msg;

        }

        public static void MailSend(Server server, MailMessage msg, SecureString password)
        {
            using SmtpClient client = new(server.Adress, server.Port);
            client.EnableSsl = server.useSSL;
            client.Credentials = new NetworkCredential(msg.From.ToString(), password);
            client.Send(msg);
        }
    }
}
