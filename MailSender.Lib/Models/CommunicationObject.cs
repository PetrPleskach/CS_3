
namespace MailSender.Models
{
    /// <summary>
    /// Базовый класс для отправителя и получателя
    /// </summary>
    public abstract class CommunicationObject : Entity
    {        
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }
    }
}
