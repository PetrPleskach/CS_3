using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Infrastructure.Interfaces
{
    public interface IMailSender
    {
        void Send(string from, string to, string subject, string body);
    }
}
