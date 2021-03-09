using MailSender.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public class SmtpSender : IMailSender
    {
        private readonly string _Address;
        private readonly int _Port;
        private readonly bool _UseSsl;
        private readonly string _Login;
        private readonly string _Password;

        public SmtpSender(string adress, int port, bool useSsl, string login, string password)
        {
            _Address = adress;
            _Port = port;
            _UseSsl = useSsl;
            _Login = login;
            _Password = password;
        }

        public void Send(string from, string to, string subject, string body)
        {
            using var message = new MailMessage(from, to) { Subject = subject, Body = body };
            using var client = new SmtpClient(_Address, _Port)
            {
                EnableSsl = _UseSsl,
                Credentials = new NetworkCredential(_Login, _Password)
            };
            client.Send(message);
        }
    }
}
