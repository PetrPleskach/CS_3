using MailSender.Infrastructure.Interfaces;
using System.Diagnostics;

namespace MailSender.Services
{
    public class DebugMailSender : IMailSender
    {
        private readonly string _Address;
        private readonly int _Port;
        private readonly bool _UseSsl;
        private readonly string _Login;
        private readonly string _Password;
        private readonly IStatistic _Statistic;

        public DebugMailSender(string adress, int port, bool useSsl, string login, string password, IStatistic statistic)
        {
            _Address = adress;
            _Port = port;
            _UseSsl = useSsl;
            _Login = login;
            _Password = password;
            _Statistic = statistic;
        }

        public void Send(string from, string to, string subject, string body)
        {
            Debug.WriteLine($"Почтовый сервер {_Address} {_Port} useSSL{_UseSsl} login [{_Login}] password [{_Password}]");
            Debug.WriteLine($"Send message from {from} to {to} subject {subject} body {body}");
            _Statistic.MailSended();
        }
    }
}
