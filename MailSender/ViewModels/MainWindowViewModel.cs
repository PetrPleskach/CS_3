using MailSender.Commands;
using MailSender.Data;
using MailSender.Infrastructure.Interfaces;
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

        private readonly IMailService _MailService;
        
        public MainWindowViewModel(IMailService mailService)
        {
            _MailService = mailService;
        } 

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

        #region Свойства

        private Server _SelectedServer;
        public Server SelectedServer
        {
            get => _SelectedServer;
            set => Set(ref _SelectedServer, value);
        }

        private Sender _SelectedSender;
        public Sender SelectedSender
        {
            get => _SelectedSender;
            set => Set(ref _SelectedSender, value);
        }

        private Recipient _SelectedRecipient;
        public Recipient SelectedRecipient
        {
            get => _SelectedRecipient;
            set => Set(ref _SelectedRecipient, value);
        }

        private Message _SelectedMessage;
        public Message SelectedMessage
        {
            get => _SelectedMessage;
            set => Set(ref _SelectedMessage, value);
        }


        #endregion

        #region Команды

        #region Загрузка/Сохранение данных
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

        private ICommand _SendMailMessageCommand;
        public ICommand SendMailMessageCommand => _SendMailMessageCommand ??= new LambdaCommand(OnSendMailMessageCommandExecuted, CanSendMailMessageCommandExecute);

        private bool CanSendMailMessageCommandExecute(object obj)
        {
            return SelectedServer != null && SelectedSender != null && SelectedRecipient != null && SelectedMessage != null;
        }

        private void OnSendMailMessageCommandExecuted(object obj)
        {
            var server = SelectedServer;
            var sender = SelectedSender;
            var recipient = SelectedRecipient;
            var message = SelectedMessage;

            var client = _MailService.GetSender(server.Adress, server.Port, server.UseSSL, sender.Adress, sender.Password);

            client.Send(sender.Adress, recipient.Adress, message.Subject, message.Body);
        }

        #region Server Commands

        private ICommand _CreateServerCommand;
        public ICommand CreateServerCommand => _CreateServerCommand ??= new LambdaCommand(OnCreateServerCommandExecuted);
        private void OnCreateServerCommandExecuted(object obj)
        {
            if (!ServerEditDialogWindow.Create(
                out var name,
                out var address,
                out var port,
                out var useSSL,
                out string description)) return;

            var server = new Server
            {
                Id = Servers.DefaultIfEmpty().Max(s => s?.Id ?? 0) + 1,
                Name = name,
                Adress = address,
                Port = port,
                UseSSL = useSSL,
                Description = description
            };

            Servers.Add(server);
        }

        //-------------------------------------------------------------------//

        private ICommand _EditServerCommand;
        public ICommand EditServerCommand => _EditServerCommand ??= new LambdaCommand(OnEditServerCommandExecuted);
        private void OnEditServerCommandExecuted(object obj)
        {
            if (!(obj is Server server)) return;

            var name = server.Name;
            var address = server.Adress;
            var port = server.Port;
            var useSSL = server.UseSSL;
            var description = server.Description;

            if (!ServerEditDialogWindow.ShowDialog("Редактирование сервера",
                ref name,
                ref address,
                ref port,
                ref useSSL,
                ref description)) return;

            server.Name = name;
            server.Adress = address;
            server.Port = port;
            server.UseSSL = useSSL;
            server.Description = description;
        }

        //------------------------------------------------------------------//

        private ICommand _DeleteServerCommand;
        public ICommand DeleteServerCommand => _DeleteServerCommand ??= new LambdaCommand(OnDeleteDataCommandExecuted);
        private void OnDeleteDataCommandExecuted(object obj)
        {
            if (!(obj is Server server)) return;

            Servers.Remove(server);
        }

        #endregion

        #endregion
    }
}
