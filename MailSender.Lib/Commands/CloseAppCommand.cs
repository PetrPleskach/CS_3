﻿using MailSender.Commands.Base;
using System.Windows;

namespace MailSender.Commands
{
    public class CloseAppCommand : Command
    {
        public override void Execute(object parameter) => Application.Current.Shutdown();
    }
}
