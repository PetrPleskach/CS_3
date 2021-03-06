﻿using MailSender.Commands;
using MailSender.Infrastructure.Interfaces;
using MailSender.Models;
using MailSender.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MailSender.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IMailService _MailService;        
        private readonly IServersStorage _ServerStorage;
        private readonly ISendersStorage _SendersStorage;
        private readonly IRecipientsStorage _RecipientsStorage;
        private readonly IMessagesStorage _MessagesStorage;

        public MainWindowViewModel(IMailService mailService, IServersStorage ServerStorage, ISendersStorage SendersStorage,
            IRecipientsStorage RecipientsStorage, IMessagesStorage MessagesStorage)
        {
            _MailService = mailService;
            _ServerStorage = ServerStorage;
            _SendersStorage = SendersStorage;
            _RecipientsStorage = RecipientsStorage;
            _MessagesStorage = MessagesStorage;
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
            _ServerStorage.Load();
            _SendersStorage.Load();
            _RecipientsStorage.Load();
            _MessagesStorage.Load();

            Servers = new ObservableCollection<Server>(_ServerStorage.Items);
            Senders = new ObservableCollection<Sender>(_SendersStorage.Items);
            Recipients = new ObservableCollection<Recipient>(_RecipientsStorage.Items);
            Messages = new ObservableCollection<Message>(_MessagesStorage.Items);
        }

        private ICommand _SaveDataCommand;
        public ICommand SaveDataCommand => _SaveDataCommand ??= new LambdaCommand(OnSaveDataCommandExecuted);
        private void OnSaveDataCommandExecuted(object obj)
        {
            _ServerStorage.SaveChanges();
            _SendersStorage.SaveChanges();
            _RecipientsStorage.SaveChanges();
            _MessagesStorage.SaveChanges();
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

            _ServerStorage.Items.Add(server);
            Servers.Add(server);
        }

        //-------------------------------------------------------------------//

        private ICommand _EditServerCommand;
        public ICommand EditServerCommand => _EditServerCommand ??= new LambdaCommand(OnEditServerCommandExecuted);
        private void OnEditServerCommandExecuted(object obj)
        {
            if (obj is not Server server) return;

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
            if (obj is not Server server) return;

            _ServerStorage.Items.Remove(server);
            Servers.Remove(server);
        }

        #endregion

        #endregion        
    }
}
