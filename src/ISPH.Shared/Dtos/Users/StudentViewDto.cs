using ISPH.Shared.Dtos.Advertisements.Featured;
using ISPH.Shared.Dtos.Resumes;

namespace ISPH.Shared.Dtos.Users;
public record StudentViewDto : IDto<Guid>
{
    public Guid? Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string University { get; set; }
    public string Faculty { get; set; }
    public ushort Course { get; set; }
    public ResumeDto Resume { get; set; }
    public IEnumerable<FeaturedAdvertisementItemDto> FeaturedAdvertisements { get; set; }
}