using MailSender.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public class DebugMailSender : IMailSender
    {
        private readonly string _Address;
        private readonly int _Port;
        private readonly bool _UseSsl;
        private readonly string _Login;
        private readonly string _Password;

        public DebugMailSender(string adress, int port, bool useSsl, string login, string password)
        {
            _Address = adress;
            _Port = port;
            _UseSsl = useSsl;
            _Login = login;
            _Password = password;
        }

        public void Send(string from, string to, string subject, string body)
        {
            Debug.WriteLine($"Почтовый сервер {_Address} {_Port} useSSL{_UseSsl} login [{_Login}] password [{_Password}]");
            Debug.WriteLine($"Send message from {from} to {to} subject {subject} body {body}");
        }
    }
}
