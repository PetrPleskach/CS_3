using MailSender.Data;
using MailSender.Models;
using System;
using System.Collections.Generic;
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

        private void onAddServerButtonClick(object sender, RoutedEventArgs e)
        {
            if (!ServerEditDialogWindow.Create(
                out var name,
                out var adress,
                out var port,
                out var useSSl,
                out var descraption,
                out var login,
                out var password)) return;

            var server = new Server
            {
                Id = TestData.Servers.DefaultIfEmpty().Max(s => s.Id) + 1,
                Name = name,
                Adress = adress,
                Port = port,
                UseSSL = useSSl,
                Description = descraption,
                Login = login,
                Password = password
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
            var login = server.Login;
            var password = server.Password;

            if (!ServerEditDialogWindow.ShowDialog("Редактировиние сервера",
                ref name,
                ref adress,
                ref port,
                ref useSsl,
                ref description,
                ref login,
                ref password)) return;

            server.Name = name;
            server.Adress = adress;
            server.Port = port;
            server.UseSSL = useSsl;
            server.Description = description;
            server.Login = login;
            server.Password = password;

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
    }
}
