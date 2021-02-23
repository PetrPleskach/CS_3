using MailSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.Data
{
    internal static class TestData
    {
        static TestData()
        {
            Servers = new List<Server>();
            AddServers();
        }

        public static List<Server> Servers { get; }

        private static void AddServers()
        {
            Servers.Add(new Server { Name = "Mail.ru", Adress = "smtp.mail.ru", Port = 25 });
            Servers.Add(new Server { Name = "Yandex.ru", Adress = "smtp.yandex.ru", Port = 25 });
        }
    }
}
