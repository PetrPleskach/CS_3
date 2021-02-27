namespace MailSender.Infrastructure.Interfaces
{
    public interface IMailSender
    {
        void Send(string from, string to, string subject, string body);
    }
}
