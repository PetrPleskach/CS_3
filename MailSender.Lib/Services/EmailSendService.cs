using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MailSender.Services
{
    public class EmailSendService
    {
        private readonly string _Address;
        private readonly int _Port;
        private readonly bool _UseSsl;
        private readonly string _Login;
        private readonly string _Password;

        public EmailSendService(string adress, int port, bool useSsl, string login, string password)
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
