using MailSender.Infrastructure.Interfaces;

namespace MailSender.Services
{
    public class SmtpMailService : IMailService
    {
        public IMailSender GetSender(string address, int port, bool useSSL, string login, string password) => new SmtpSender(address, port, useSSL, login, password);
    }
}
