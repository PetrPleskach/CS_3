using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data
{
    class TestData
    {
        public static List<Server> Servers { get; } = Enumerable.Range(1, 10)
            .Select(i => new Server
            {
                Name = $"Name - {i}",
                Adress = $"Adress - {i}",
                Login = $"Login - {i}",
                Password = $"Password - {i}"
            }).ToList();

        public static List<Sender> Senders { get; } = Enumerable.Range(1, 10)
            .Select(i => new Sender
            {
                Name = $"Sender - {i}",
                Adress = $"sender_{i}@server.ru"
            }).ToList();

        public static List<Recipient> Recipients { get; } = Enumerable.Range(1, 10)
            .Select(i => new Recipient
            {
                Name = $"Recipient - {i}",
                Adress = $"recipient_{i}@server.ru"
            }).ToList();

        public static List<Message> Messages { get; } = Enumerable.Range(1, 10)
            .Select(i => new Message
            {
                Subject = "Title",
                Body = "Text"
            }).ToList();
    }
}
