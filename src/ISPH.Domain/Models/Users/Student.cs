using ISPH.Domain.Configuration;
using ISPH.Domain.Models.Advertisements;
using ISPH.Domain.Models.Base;
using ISPH.Domain.Models.Messaging;

namespace ISPH.Domain.Models.Users;

public class Student : BaseUser<Guid>
{
    public Resume Resume { get; set; }
    public IEnumerable<FeaturedAdvertisement> FeaturedAdvertisements { get; set; }
    public IEnumerable<AdvertisementResponse> AdvertisementResponses { get; set; }
    public override string Role => Roles.Student;
    public string University { get; set; }
    public string Faculty { get; set; }
    public ushort Course { get; set; }
    public IEnumerable<Chat> Chats { get; set; }
}