using ISPH.Shared.Dtos.Positions;
using ISPH.Shared.Dtos.Users;

namespace ISPH.Shared.Dtos.Advertisements;

public record AdvertisementViewDto : IDto<Guid>
{
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public uint Salary { get; set; }
    public string Description { get; set; }
    public string WorkTime { get; set; }
    public string EmploymentType { get; set; }
    public string PostedAt { get; set; }
    public PositionItemDto Position { get; set; }
    public AdvertisementEmployerDto Employer { get; set; }
}