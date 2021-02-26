using MailSender.Infrastructure.Interfaces;
using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services
{
    class InMemoryDataStorage : IServersStorage, ISendersStorage, IRecipientsStorage, IMessagesStorage
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

        }

        public void SaveChanges()
        {

        }
    }
}
