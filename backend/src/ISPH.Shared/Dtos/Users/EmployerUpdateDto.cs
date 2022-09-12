using ISPH.Shared.Dtos.Users.Base;

namespace ISPH.Shared.Dtos.Users;

public record EmployerUpdateDto : UserUpdateDto<Guid>
{
    public Guid CompanyId { get; set; }
}