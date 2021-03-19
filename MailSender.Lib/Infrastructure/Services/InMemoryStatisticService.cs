using MailSender.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public class InMemoryStatisticService : IStatistic
    {
        public event EventHandler SendedMailCountChanged;

        private int _SendedMailCount;

        public InMemoryStatisticService(InMemoryDataStorage storage)
        {
            SendersCount = storage.Senders.Count;
            RecipientsCount = storage.Recipients.Count;
        }

        public int SendedMailCount => _SendedMailCount;
        public int SendersCount { get; }
        public int RecipientsCount { get; }

        private readonly Stopwatch _StopWatchTimer = Stopwatch.StartNew();
        public TimeSpan UpTime => _StopWatchTimer.Elapsed;

        public void MailSended()
        {
            _SendedMailCount++;
            SendedMailCountChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
