using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Models
{
    /// <summary>
    /// Базовый класс для отправителя и получателя
    /// </summary>
    public abstract class CommunicationObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }
    }
}
