using MailSender.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WpfTest.Data;
using static WpfTest.Services.EmailSendServiceClass;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        List<string> result;

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

            result = await Task.Run(() => LoadFile(fileName)).ConfigureAwait(true);

            StatusTextBlock.Text = "Done!";
            LoadButton.IsEnabled = false;
            SaveButton.IsEnabled = true;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog
            {
                Title = "Сохранить как TXT-файл",
                Filter = "Txt файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                InitialDirectory = Environment.CurrentDirectory,
                OverwritePrompt = true,
            };

            if (saveDialog.ShowDialog() == false) return;

            string fileName = saveDialog.FileName;

            await Task.Run(() => SaveFile(fileName, result)).ConfigureAwait(true);

            SaveButton.IsEnabled = false;
            LoadButton.IsEnabled = true;
        }

        private static List<string> LoadFile(string fileName)
        {
            var strings = new List<string>();
            try
            {
                var list = File.ReadAllLines(fileName);
                foreach (var str in list)
                    strings.Add(str);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return strings;
        }

        private static void SaveFile(string fileName, List<string> dataToSave)
        {
            try
            {
                using var writer = new StreamWriter(fileName);
                foreach (var item in dataToSave)
                {
                    writer.WriteLine(item);
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);                
            }
        }
    }
}
