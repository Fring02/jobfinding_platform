using ISPH.Shared.Dtos.Users;

namespace ISPH.Shared.Dtos.Advertisements.Featured;

public record FeaturedAdvertisementViewDto
{
    public Guid StudentId { get; set; }
    public StudentViewDto Student { get; set; }
    public Guid AdvertisementId { get; set; }
    public AdvertisementItemDto Advertisement { get; set; }
    public string Title { get; set; }
    public uint Salary { get; set; }
}