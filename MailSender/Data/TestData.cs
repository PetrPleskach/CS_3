using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Models;

namespace MailSender.Data
{
    class TestData
    {
        public static IList<Server> Servers { get; } = Enumerable.Range(1, 10)
            .Select(i => new Server
            {
                Name = $"Name - {i}",
                Adress = $"Adress - {i}",
            }).ToList();

        public static IList<Sender> Senders { get; } = Enumerable.Range(1, 10)
            .Select(i => new Sender
            {
                Name = $"Sender - {i}",
                Adress = $"sender_{i}@server.ru",
                Password =$"senderPass{i}"
            }).ToList();

        public static IList<Recipient> Recipients { get; } = Enumerable.Range(1, 10)
            .Select(i => new Recipient
            {
                Name = $"Recipient - {i}",
                Adress = $"recipient_{i}@server.ru"
            }).ToList();

        public static IList<Message> Messages { get; } = Enumerable.Range(1, 10)
            .Select(i => new Message
            {
                Subject = $"Title {i}",
                Body = $"Text {i}"
            }).ToList();
    }
}
