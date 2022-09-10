using ISPH.Api.Controllers.Base.Auth;
using ISPH.Domain.Models.Users;
using ISPH.Shared.Dtos.Users;
using ISPH.Shared.Dtos.Users.Base;
using ISPH.Shared.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.Api.Controllers.Users;

[Route("api/v1/[controller]")]
public class StudentsController : UsersController<Student, Guid, StudentCreateDto, UserUpdateDto<Guid>, StudentViewDto, StudentItemDto, IStudentsService>
{
    public StudentsController(IStudentsService service) : base(service)
    {
    }
}