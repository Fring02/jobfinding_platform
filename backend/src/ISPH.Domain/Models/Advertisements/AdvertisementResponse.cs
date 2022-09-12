using ISPH.Domain.Models.Base;
using ISPH.Domain.Models.Users;

namespace ISPH.Domain.Models.Advertisements;
public class AdvertisementResponse : BaseEntity<int>
{
    public Guid AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public string? CoverLetter { get; set; }
    public DateTime LeftAt { get; set; } = DateTime.Now;
}