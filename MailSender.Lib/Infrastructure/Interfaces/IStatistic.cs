using System;

namespace MailSender.Infrastructure.Interfaces
{
    public interface IStatistic
    {
        int SendedMailCount { get; }
        event EventHandler SendedMailCountChanged;

        void MailSended();

        int SendersCount { get; }
        int RecipientsCount { get; }
        public TimeSpan UpTime { get; }
    }
}
