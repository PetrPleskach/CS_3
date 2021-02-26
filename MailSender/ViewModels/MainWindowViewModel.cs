using MailSender.Commands;
using MailSender.Data;
using MailSender.Models;
using MailSender.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string __DataFileName = "";

        #region Title
        private string _title = "Рассыльщик";

        public string Title
        {
            get { return _title; }
            set => Set(ref _title, value);
        }
        #endregion

        #region Коллекции основных сущностей

        private ObservableCollection<Server> _Servers;
        public ObservableCollection<Server> Servers
        {
            get => _Servers;
            set => Set(ref _Servers, value);
        }

        private ObservableCollection<Sender> _Senders;
        public ObservableCollection<Sender> Senders
        {
            get => _Senders;
            set => Set(ref _Senders, value);
        }

        private ObservableCollection<Recipient> _Recipients;
        public ObservableCollection<Recipient> Recipients
        {
            get => _Recipients;
            set => Set(ref _Recipients, value);
        }

        private ObservableCollection<Message> _Messages; 
        public ObservableCollection<Message> Messages
        {
            get => _Messages;
            set => Set(ref _Messages, value);
        }

        #endregion

        #region Команды

        private ICommand _LoadDataCommand; 
        public ICommand LoadDataCommand => _LoadDataCommand ??= new LambdaCommand(OnLoadDataCommandExecuted);        
        private void OnLoadDataCommandExecuted(object obj)
        {
            var data = File.Exists(__DataFileName) ? TestData.LoadFromXML(__DataFileName) : new TestData();  

            Servers = new(data.Servers);
            Senders = new(data.Senders);
            Recipients = new(data.Recipients);
            Messages = new(data.Messages);
        }

        //--------------------------------------------------------------------------------------------------//

        private ICommand _SaveDataCommand;
        public ICommand SaveDataCommand => _SaveDataCommand ??= new LambdaCommand(OnSaveDataCommandExecuted);

        private void OnSaveDataCommandExecuted(object obj)
        {
            var data = new TestData
            {
                Servers = Servers,
                Senders = Senders,
                Recipients = Recipients,
                Messages = Messages
            };

            data.SaveToXML(__DataFileName ??= "SomeFileName");
        }

        #endregion
    }
}
