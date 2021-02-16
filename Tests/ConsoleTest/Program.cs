using System;
using System.Net;
using System.Net.Mail;
using System.Security;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MailAddress from = new("", "");
            MailAddress to = new("");
            MailMessage message = new(from, to);
            
            message.Subject = "Test";
            message.Body = "Test message";            

            SmtpClient client = new("smtp.mail.ru", 25);

            SecureString secureString = new();

            

            client.EnableSsl = true;
            client.Credentials = new NetworkCredential { UserName = "", Password = "" };             

            
            client.Send(message);
            Console.WriteLine("OK!");
            Console.ReadLine();
        }
    }
}
