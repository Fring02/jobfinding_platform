using ISPH.Domain.Models.Users;
using ISPH.Shared.Dtos.Users;
using ISPH.Shared.Interfaces.Base;

namespace ISPH.Shared.Interfaces.Users;

public interface IEmployersService : IUserService<Employer, EmployerCreateDto, EmployerUpdateDto, Guid>
{
}