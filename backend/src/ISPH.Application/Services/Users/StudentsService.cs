using System;
using AutoMapper;
using ISPH.Application.Services.Base;
using ISPH.Application.Utils.Auth;
using ISPH.Application.Utils.Users;
using ISPH.Data.Contexts;
using ISPH.Domain.Configuration;
using ISPH.Domain.Models.Users;
using ISPH.Shared.Dtos.Users;
using ISPH.Shared.Dtos.Users.Base;
using ISPH.Shared.Interfaces.Users;
using Microsoft.Extensions.Options;

namespace ISPH.Application.Services.Users;

public class StudentsService : BaseUserService<Student, StudentCreateDto, UserUpdateDto<Guid>, Guid>, IStudentsService
{
    public StudentsService(ApplicationContext context, IMapper mapper, JwtService tokenService, 
        IOptions<AuthenticationOptions> options, EmailNotifier notifier) : base(context, mapper, tokenService, options, notifier)
    {
    }
}