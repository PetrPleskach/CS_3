using MailSender.Models;
using System.Collections.Generic;

namespace MailSender.Infrastructure.Interfaces
{
    public interface IStorage<T>
    {
        ICollection<T> Items { get; }

        void Load();
        void SaveChanges();
    }

    public interface IServersStorage : IStorage<Server> { }
    public interface ISendersStorage : IStorage<Sender> { }
    public interface IRecipientsStorage : IStorage<Recipient> { }
    public interface IMessagesStorage : IStorage<Message> { }
}
