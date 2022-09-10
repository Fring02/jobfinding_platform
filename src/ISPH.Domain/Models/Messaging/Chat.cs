using ISPH.Domain.Models.Base;
using ISPH.Domain.Models.Users;

namespace ISPH.Domain.Models.Messaging;

public class Chat : BaseEntity<Guid>
{
    public Guid EmployerId { get; set; }
    public Employer Employer { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public ICollection<Message> Messages { get; set; }
}