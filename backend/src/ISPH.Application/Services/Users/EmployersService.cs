using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.Application.Services.Base;
using ISPH.Application.Utils.Auth;
using ISPH.Application.Utils.Users;
using ISPH.Data.Contexts;
using ISPH.Domain.Configuration;
using ISPH.Domain.Models.Users;
using ISPH.Shared.Dtos.Authorization;
using ISPH.Shared.Dtos.Users;
using ISPH.Shared.Interfaces.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ISPH.Application.Services.Users;
public class EmployersService : BaseUserService<Employer, EmployerCreateDto, EmployerUpdateDto, Guid>, IEmployersService
{
    public EmployersService(ApplicationContext context, IMapper mapper, JwtService tokenService, 
        IOptions<AuthenticationOptions> options, EmailNotifier notifier) : base(context, mapper, tokenService, options, notifier)
    {
    }
    public override async Task<TokensDto> RegisterAsync(EmployerCreateDto registerUser, CancellationToken token = default)
    {
        if (!await _context.Companies.AnyAsync(c => c.Id == registerUser.CompanyId, token))
            throw new ArgumentException("Company with this id doesn't exist");
        return await base.RegisterAsync(registerUser, token);
    }

    public override async Task UpdateAsync(EmployerUpdateDto entity, CancellationToken token = default)
    {
        var user = await _context.Employers.FindAsync(new object?[] { entity.Id }, cancellationToken: token);
        if (user == null) throw new ArgumentException($"User with id {entity.Id} is not found");
        if (!await _context.Companies.AnyAsync(c => c.Id == entity.CompanyId, token))
            throw new ArgumentException($"Company with id {entity.CompanyId} is not found");
        
        user.CompanyId = entity.CompanyId;
        if(!string.IsNullOrEmpty(entity.Password))
            user.CreatePassword(entity.Password);

        if (!string.IsNullOrEmpty(entity.Email))
            user.Email = entity.Email;

        if (!string.IsNullOrEmpty(entity.Phone))
            user.Phone = entity.Phone;

        _context.Employers.Update(user);
        await _context.SaveChangesAsync(token);
    }

}