namespace MailSender.Models
{
    public class Server
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public int Port { get; set; } = 25;
        public string Login { get; set; }
        public string Password { get; set; }
        public bool useSSL { get; set; } = true;
    }
}
