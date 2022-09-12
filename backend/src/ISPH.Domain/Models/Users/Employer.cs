using ISPH.Domain.Configuration;
using ISPH.Domain.Models.Advertisements;
using ISPH.Domain.Models.Base;
using ISPH.Domain.Models.Messaging;

namespace ISPH.Domain.Models.Users;

public class Employer : BaseUser<Guid>
{
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public IEnumerable<Advertisement> Advertisements { get; set; }
    public override string Role => Roles.Employer;
    public IEnumerable<Chat> Chats { get; set; }
}