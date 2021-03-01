using MailSender.Data;
using MailSender.Models;
using MailSender.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace MailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();


        #region ServerButtons
        private void onAddServerButtonClick(object sender, RoutedEventArgs e)
        {
            if (!ServerEditDialogWindow.Create(
                out var name,
                out var adress,
                out var port,
                out var useSSl,
                out var descraption)) return;

            var server = new Server
            {
                Id = TestData.Servers.DefaultIfEmpty().Max(s => s.Id) + 1,
                Name = name,
                Adress = adress,
                Port = port,
                UseSSL = useSSl,
                Description = descraption,
            };

            TestData.Servers.Add(server);

            ServerBox.ItemsSource = null;
            ServerBox.ItemsSource = TestData.Servers;
            ServerBox.SelectedItem = server;
        }

        private void OnEditServerButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(ServerBox.SelectedItem is Server server)) return;

            var name = server.Name;
            var adress = server.Adress;
            var port = server.Port;
            var useSsl = server.UseSSL;
            var description = server.Description;

            if (!ServerEditDialogWindow.ShowDialog("Редактирование сервера",
                ref name,
                ref adress,
                ref port,
                ref useSsl,
                ref description)) return;

            server.Name = name;
            server.Adress = adress;
            server.Port = port;
            server.UseSSL = useSsl;
            server.Description = description;

            ServerBox.ItemsSource = null;
            ServerBox.ItemsSource = TestData.Servers;
        }

        private void OnDeleteServerButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(ServerBox.SelectedItem is Server server)) return;

            TestData.Servers.Remove(server);

            ServerBox.ItemsSource = null;
            ServerBox.ItemsSource = TestData.Servers;
            ServerBox.SelectedItem = TestData.Servers.FirstOrDefault();
        }
        #endregion

        #region RecipientButtons

        private void onAddRecipientButtonClick(object sender, RoutedEventArgs e)
        {
            if (!RecipientEditDialogWindow.Create(out var name, out var address, out var description)) return;

            var recipient = new Recipient
            {
                Id = TestData.Recipients.DefaultIfEmpty().Max(r => r.Id),
                Name = name,
                Adress = address,
                Description = description
            };

            TestData.Recipients.Add(recipient);
            RecipientList.ItemsSource = null;
            RecipientList.ItemsSource = TestData.Recipients;
        }

        private void OnEditRecipientButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(RecipientList.SelectedItem is Recipient recipient)) return;

            var name = recipient.Name;
            var address = recipient.Adress;
            var description = recipient.Description;

            if (!RecipientEditDialogWindow.ShowDialog("Редактирование получателя", ref name, ref address, ref description)) return;

            recipient.Name = name;
            recipient.Adress = address;
            recipient.Description = description;

            RecipientList.ItemsSource = null;
            RecipientList.ItemsSource = TestData.Recipients;
        }

        private void OnDeleteRecipientButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(RecipientList.SelectedItem is Recipient recipient)) return;

            TestData.Recipients.Remove(recipient);

            RecipientList.ItemsSource = null;
            RecipientList.ItemsSource = TestData.Recipients;
        }

        #endregion

        private void OnSendNowButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(SenderBox.SelectedItem is Sender sen)) return;
            if (!(RecipientList.SelectedItem is Recipient recipient)) return;
            if (!(ServerBox.SelectedItem is Server server)) return;
            if (!(MessageList.SelectedItem is Message message)) return;

            var mailSender = new EmailSendService(server.Adress, server.Port, server.UseSSL, sen.Adress, sen.Password);

            try
            {
                var timer = Stopwatch.StartNew();
                mailSender.Send(sen.Adress, recipient.Adress, message.Subject, message.Body);
                timer.Stop();

                var elapsed = timer.Elapsed.TotalSeconds;
                MessageBox.Show($"Почта успешно отправлена за {elapsed:0.##}сек.", "Отправка почты", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при отправке почты", "Отправка почты", MessageBoxButton.OK, MessageBoxImage.Error);
            }




        }
    }
}
