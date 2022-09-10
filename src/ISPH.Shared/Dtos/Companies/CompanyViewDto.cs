using ISPH.Shared.Dtos.Users;

namespace ISPH.Shared.Dtos.Companies;

public record CompanyViewDto : IDto<Guid>
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Website { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public IEnumerable<EmployerItemDto> Employers { get; set; }
}