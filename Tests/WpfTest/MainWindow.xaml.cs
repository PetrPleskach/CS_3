﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MailAddress from = new(EmailBox.Text, "");
            MailAddress to = new("pleskach_petr@mail.ru");
            MailMessage message = new(from, to);

            message.Subject = "Test";
            message.Body = "Test message";

            using SmtpClient client = new("smtp.mail.ru", 25);

            client.EnableSsl = true;
            client.Credentials = new NetworkCredential { UserName = EmailBox.Text, SecurePassword = PasswordBox.SecurePassword };

            client.Send(message);
        }
    }
}
