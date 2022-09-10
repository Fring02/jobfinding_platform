using ISPH.Domain.Models.Base;

namespace ISPH.Domain.Models.Messaging;

public class Message : BaseEntity<long>
{
    public Guid ChatId { get; set; } 
    public Chat Chat { get; set; }
    public DateTime LeftAt { get; set; }
    public string Text { get; set; }
}