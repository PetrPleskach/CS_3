using MailSender.Commands;
using MailSender.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _title = "Рассыльщик";

        public string Title
        {
            get { return _title; }
            set => Set(ref _title, value);
        }
    }
}
