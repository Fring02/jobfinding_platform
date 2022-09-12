using ISPH.Api.Attributes;
using ISPH.Api.Controllers.Base.Auth;
using ISPH.Domain.Configuration;
using ISPH.Domain.Models.Users;
using ISPH.Shared.Dtos.Users;
using ISPH.Shared.Dtos.Users.Base;
using ISPH.Shared.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.Api.Controllers.Users;

[Route("api/v1/[controller]")]
public class StudentsController : UsersController<Student, Guid, StudentCreateDto, UserUpdateDto<Guid>, StudentViewDto, StudentItemDto, IStudentsService>
{
    public StudentsController(IStudentsService service) : base(service)
    {
    }

    [HttpGet("{id}"), Authorize(Roles = Roles.Student), ResourceAuthorize]
    public override Task<IActionResult> GetByIdAsync(Guid id, CancellationToken token) => base.GetByIdAsync(id, token);
}