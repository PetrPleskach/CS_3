using MailSender.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public class SmtpMailService : IMailService
    {
        public IMailSender GetSender(string address, int port, bool useSSL, string login, string password) => new SmtpSender(address, port, useSSL, login, password);
    }
}
