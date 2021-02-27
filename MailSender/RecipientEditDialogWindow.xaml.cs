using System.Windows;
using System.Windows.Controls;

namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для RecipientEditDialogWindow.xaml
    /// </summary>
    public partial class RecipientEditDialogWindow : Window
    {
        public RecipientEditDialogWindow() => InitializeComponent();

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = !((Button)e.OriginalSource).IsCancel;
            Close();
        }

        public static bool ShowDialog(string title, ref string name, ref string address, ref string description)
        {
            var window = new RecipientEditDialogWindow
            {
                Title = title,
                RecipientName = { Text = name },
                RecipientAddress = { Text = address },
                RecipientDescription = { Text = description }
            };

            if (window.ShowDialog() != true) return false;

            name = window.RecipientName.Text;
            address = window.RecipientAddress.Text;
            description = window.RecipientDescription.Text;

            return true;
        }

        public static bool Create(out string name, out string address, out string description)
        {
            name = address = description = null;
            return ShowDialog("Добавить нового получателя", ref name, ref address, ref description);
        }
    }
}
