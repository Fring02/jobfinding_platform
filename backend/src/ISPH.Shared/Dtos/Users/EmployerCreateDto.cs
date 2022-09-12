using ISPH.Shared.Dtos.Authorization;

namespace ISPH.Shared.Dtos.Users;

public record EmployerCreateDto : RegisterDto<Guid>
{
    [Required]
    public Guid CompanyId { get; set; }
}