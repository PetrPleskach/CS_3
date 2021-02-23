namespace MailSender.Models
{
    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public int Port { get; set; } = 25;
        public bool UseSSL { get; set; } = true;
        public string Description { get; set; }
    }
}
