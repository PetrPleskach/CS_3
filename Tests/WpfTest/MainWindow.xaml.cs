using MailSender.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using static WpfTest.Services.EmailSendServiceClass;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                Title = "Открыть CSV-файл",
                Filter = "Csv файлы (*.csv)|*.csv|Все файлы (*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory
            };

            if (openDialog.ShowDialog() == false) return;
            
            string fileName = openDialog.FileName;
            if (File.Exists(fileName) == false) return;



            var button = sender as Button;
            button.IsEnabled = false;
            StatusTextBlock.Text = "";

            var result = await Task.Run(() => LoadFile(fileName)).ConfigureAwait(true);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private List<string> LoadFile(string fileName)
        {
            return new List<string>();
            try
            {
                var list = File.ReadAllLines(fileName).Select(l => l.Split(';'));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
