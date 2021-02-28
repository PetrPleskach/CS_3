using MailSender.Infrastructure.Interfaces;
using MailSender.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MailSender.Services
{
    public class InMemoryDataStorage : IServersStorage, ISendersStorage, IRecipientsStorage, IMessagesStorage
    {
        public ICollection<Server> Servers { get; set; } = new List<Server>();
        public ICollection<Sender> Senders { get; set; } = new List<Sender>();
        public ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        ICollection<Server> IStorage<Server>.Items => Servers;
        ICollection<Sender> IStorage<Sender>.Items => Senders;
        ICollection<Recipient> IStorage<Recipient>.Items => Recipients;
        ICollection<Message> IStorage<Message>.Items => Messages;

        public void Load()
        {
            Debug.WriteLine("Вызвана процедура загрузки данных");

            if (Servers is null || Servers.Count == 0)
                Servers = new List<Server>
                {
                    new Server
                    {
                        Id = 1,
                        Name = "Yandex",
                        Adress = "smpt.yandex.ru",
                        Port = 465,
                        UseSSL = true,
                    },
                    new Server
                    {
                        Id = 2,
                        Name = "Google",
                        Adress = "smpt.gmail.com",
                        Port = 465,
                        UseSSL = true,
                    }
                };

            if (Senders is null || Senders.Count == 0)
                Senders = new List<Sender>
                {
                    new Sender
                    {
                        Id = 1,
                        Name = "Иванов",
                        Adress = "ivanov@server.ru",
                        Password = "qwerty",
                        Description = "Почта от Иванова"
                    },
                    new Sender
                    {
                        Id = 2,
                        Name = "Петров",
                        Adress = "petrov@server.ru",
                        Password = "qwerty",
                        Description = "Почта от Петрова"
                    },
                    new Sender
                    {
                        Id = 3,
                        Name = "Сидоров",
                        Adress = "sidorov@server.ru",
                        Password = "qwerty",
                        Description = "Почта от Сидорова"
                    }
                };

            if (Recipients is null || Recipients.Count == 0)
                Recipients = new List<Recipient>
                {
                    new Recipient
                    {
                         Id = 1,
                         Name = "Иванов",
                         Adress = "ivanov@server.ru",
                         Description = "Почта для Иванова"
                    },
                    new Recipient
                    {
                         Id = 2,
                         Name = "Петров",
                         Adress = "petrov@server.ru",
                         Description = "Почта для Петрова"
                    },
                    new Recipient
                    {
                         Id = 3,
                         Name = "Сидоров",
                         Adress = "sidorov@server.ru",
                         Description = "Почта для Сидорова"
                    }
                };

            if (Messages is null || Messages.Count == 0)
                Messages = Enumerable.Range(1, 10).Select(i => new Message
                {
                    Id = i,
                    Subject = $"Сообщение {i}",
                    Body = $"Текст сообщения {i}"
                }).ToList();
        }

        public void SaveChanges()
        {
            Debug.WriteLine("Вызвана процедура сохранения данных");
        }
    }
}
