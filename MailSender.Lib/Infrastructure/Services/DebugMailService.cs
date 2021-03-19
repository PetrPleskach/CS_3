using MailSender.Infrastructure.Interfaces;

namespace MailSender.Services
{
    public class DebugMailService : IMailService
    {
        private readonly IStatistic _Statistic;
        public DebugMailService(IStatistic statistic)
        {
            _Statistic = statistic;
        }

        public IMailSender GetSender(string address, int port, bool useSSL, string login, string password)
            => new DebugMailSender(address, port, useSSL, login, password, _Statistic);
    }
}
