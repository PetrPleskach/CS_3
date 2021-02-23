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
using System.Windows.Shapes;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для ServerEditDialogWindow.xaml
    /// </summary>
    public partial class ServerEditDialogWindow : Window
    {
        public ServerEditDialogWindow() => InitializeComponent();

        private void OnPortTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(sender is TextBox textBox) || textBox.Text == "") return;
            e.Handled = !int.TryParse(textBox.Text, out _);
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }

        public static bool ShowDialog(string title, ref string name, ref string adress, ref int port,
            ref bool useSSl, ref string description)
        {
            var window = new ServerEditDialogWindow
            {
                Title = title,
                ServerName = { Text = name },
                ServerAddress = { Text = adress },
                ServerPort = { Text = port.ToString() },
                ServerSSL = { IsChecked = useSSl },
                ServerDescription = { Text = description },

                Owner = Application.Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive)
            };

            if (window.ShowDialog() != true) return false;

            name = window.ServerName.Text;
            adress = window.ServerAddress.Text;
            port = int.Parse(window.ServerPort.Text);
            description = window.ServerDescription.Text;
            useSSl = (bool)window.ServerSSL.IsChecked;

            return true;
        }

        public static bool Create(
            out string name,
            out string adress,
            out int port,
            out bool useSSL,
            out string description)
        {
            name = null;
            adress = null;
            port = 25;
            useSSL = true;
            description = null;

            return ShowDialog("Добавить новый сервер", ref name, ref adress, ref port, ref useSSL, ref description);
        }
    }
}
