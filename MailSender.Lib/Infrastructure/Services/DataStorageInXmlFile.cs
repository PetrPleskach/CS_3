using MailSender.Infrastructure.Interfaces;
using MailSender.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MailSender.Services
{
    public class DataStorageInXmlFile : IServersStorage, ISendersStorage, IRecipientsStorage, IMessagesStorage
    {

        /// <summary>
        /// Внутренняя структура данных для удобства сереализации/десериализации
        /// </summary>
        public class DataStructure
        {
            public List<Server> Servers { get; set; } = new();
            public List<Sender> Senders { get; set; } = new();
            public List<Recipient> Recipients { get; set; } = new();
            public List<Message> Messages { get; set; } = new();
        }

        private readonly string _FileName;
        private DataStructure data = new();

        public DataStorageInXmlFile(string fileName) => _FileName = fileName;

        ICollection<Server> IStorage<Server>.Items => data.Servers;
        ICollection<Sender> IStorage<Sender>.Items => data.Senders;
        ICollection<Recipient> IStorage<Recipient>.Items => data.Recipients;
        ICollection<Message> IStorage<Message>.Items => data.Messages;

        public void Load()
        {
            data = new();
            if (!File.Exists(_FileName)) return;

            using var file = File.OpenText(_FileName);
            if (file.BaseStream.Length == 0) return;

            var serializer = new XmlSerializer(typeof(DataStructure));
            data = serializer.Deserialize(file) as DataStructure;
        }

        public void SaveChanges()
        {
            using var file = File.CreateText(_FileName);
            var serializer = new XmlSerializer(typeof(DataStructure));
            serializer.Serialize(file, data);
        }

    }
}
