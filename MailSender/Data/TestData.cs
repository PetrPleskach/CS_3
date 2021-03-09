using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MailSender.Models;

namespace MailSender.Data
{
    [Serializable]
    public class TestData
    {
        public IList<Server> Servers { get; set; } = Enumerable.Range(1, 10)
            .Select(i => new Server
            {
                Name = $"Name - {i}",
                Adress = $"Adress - {i}",
            }).ToList();

        public IList<Sender> Senders { get; set; } = Enumerable.Range(1, 10)
            .Select(i => new Sender
            {
                Name = $"Sender - {i}",
                Adress = $"sender_{i}@server.ru",
                Password =$"senderPass{i}"
            }).ToList();

        public IList<Recipient> Recipients { get; set; } = Enumerable.Range(1, 10)
            .Select(i => new Recipient
            {
                Name = $"Recipient - {i}",
                Adress = $"recipient_{i}@server.ru"
            }).ToList();

        public IList<Message> Messages { get; set; } = Enumerable.Range(1, 10)
            .Select(i => new Message
            {
                Subject = $"Title {i}",
                Body = $"Text {i}"
            }).ToList();


        #region Методы чтения/записи тестовых данных в XML файл
        public static TestData LoadFromXML(string fileName)
        {
            var serializer = new XmlSerializer(typeof(TestData));
            using var file = File.OpenText(fileName);
            return serializer.Deserialize(file) as TestData;
        }

        public void SaveToXML(string fileName)
        {
            var serializer = new XmlSerializer(typeof(TestData));
            using var file = File.Create(fileName);
            serializer.Serialize(file, this);
        }
        #endregion
    }
}
