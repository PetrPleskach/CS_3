
namespace MailSender.Models
{
    /// <summary>
    /// Базовый класс для сущностей с идентифкатором
    /// </summary>
    public abstract class Entity
    {
        public int Id { get; set; }
    }
}
