using ISPH.Domain.Models.Users;
using ISPH.Shared.Dtos.Users;
using ISPH.Shared.Dtos.Users.Base;
using ISPH.Shared.Interfaces.Base;

namespace ISPH.Shared.Interfaces.Users;

public interface IStudentsService : IUserService<Student, StudentCreateDto, UserUpdateDto<Guid>, Guid>
{
}