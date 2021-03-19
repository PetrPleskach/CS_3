using MailSender.Infrastructure.Interfaces;
using MailSender.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MailSender.ViewModels
{
    internal class StatisticViewModel : ViewModel
    {
        private readonly IStatistic _Statistic;

        public StatisticViewModel(IStatistic Statistic)
        {
            _Statistic = Statistic;
            var timer = new Timer(1000);
            timer.Elapsed += (_, _) => OnPropertyChanged(nameof(UpTime));
            timer.Start();

            _Statistic.SendedMailCountChanged += (_, _) => OnPropertyChanged(nameof(SentMailsCount));
        }

        public int SentMailsCount => _Statistic.SendedMailCount;
        public int SendersCount => _Statistic.SendersCount;
        public int RecipientsCount => _Statistic.RecipientsCount;
        public TimeSpan UpTime => _Statistic.UpTime;
    }
}
