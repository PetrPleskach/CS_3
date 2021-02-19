﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Models
{
    class Sheduler
    {
        public DateTime Time { get; set; }
        public Server Server { get; set; }
        public Sender Sender { get; set; }
        public List<Recipient> Recipients { get; set; }
        public Message Message { get; set; }
    }
}
