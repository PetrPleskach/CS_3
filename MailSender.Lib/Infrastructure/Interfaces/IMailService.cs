namespace MailSender.Infrastructure.Interfaces
{
    public interface IMailService
    {
        IMailSender GetSender(string address, int port, bool useSSL, string login, string password);
    }
}
