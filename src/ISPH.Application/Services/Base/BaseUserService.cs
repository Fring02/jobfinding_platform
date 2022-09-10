using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ISPH.Application.Utils.Auth;
using ISPH.Application.Utils.Users;
using ISPH.Data.Contexts;
using ISPH.Domain.Configuration;
using ISPH.Domain.Models.Base;
using ISPH.Shared.Dtos.Authorization;
using ISPH.Shared.Dtos.Interfaces;
using ISPH.Shared.Dtos.Users.Base;
using ISPH.Shared.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ISPH.Application.Services.Base;

public abstract class BaseUserService<TUser, TRegister, TUpdate, TId> : BaseCrudService<TUser, TRegister, TUpdate, TId>, IUserService<TUser, TRegister, TUpdate, TId>
    where TUser : BaseUser<TId> 
    where TRegister : RegisterDto<TId> 
    where TUpdate : UserUpdateDto<TId>, IDto<TId> 
    where TId : struct
{
    protected readonly JwtService _tokenService;
    protected readonly AuthenticationOptions _options;
    protected readonly EmailNotifier _notifier;
    protected BaseUserService(ApplicationContext context, IMapper mapper, JwtService tokenService,
        IOptions<AuthenticationOptions> options, EmailNotifier notifier) : base(context, mapper)
    {
        _tokenService = tokenService;
        _notifier = notifier;
        _options = options.Value;
    }

    public virtual async Task<TokensDto> RegisterAsync(TRegister registerUser, CancellationToken token = default)
    {
        if (await HasEntityAsync(e => e.Email == registerUser.Email, token))
            throw new ArgumentException("This user already exists");
        
        var user = _mapper.Map<TUser>(registerUser);
        user.CreatePassword(registerUser.Password);
        var refreshToken = _tokenService.CreateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.Now.AddDays(_options.RefreshTokenLifetime);
        _context.Set<TUser>().Add(user);
        await _context.SaveChangesAsync(token);
        var identity = _tokenService.CreateIdentity<TUser, TId>(user);
        string accessToken = _tokenService.CreateToken(identity);
        return new(accessToken, refreshToken);
    }

    public async Task<TokensDto> LoginAsync(string email, string password, CancellationToken token = default)
    {
        var user = await _context.Set<TUser>().FirstOrDefaultAsync(e => e.Email == email, token);
        if (user == null) throw new ArgumentException($"User with {email} does not exist. Please, sign up");
        if (!user.VerifyPassword(password)) throw new ArgumentException("Password is incorrect.");
        
        var refreshToken = _tokenService.CreateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.Now.AddDays(_options.RefreshTokenLifetime);
        
        _context.Set<TUser>().Update(user);
        await _context.SaveChangesAsync(token);
        var identity = _tokenService.CreateIdentity<TUser, TId>(user);
        string accessToken = _tokenService.CreateToken(identity);
        return new(accessToken, refreshToken);
    }

    public async Task<TokensDto> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null) throw new ArgumentException("Invalid access or refresh token");

        var email = principal.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        var user = await GetByEmailAsync(email, cancellationToken);
        if (user == null || user.RefreshToken != refreshToken /*|| user.RefreshTokenExpiry > DateTime.Now*/)
            throw new ArgumentException("Invalid refresh token");

        var newAccessToken = _tokenService.CreateToken((ClaimsIdentity)principal.Identity!);
        var newRefreshToken = _tokenService.CreateRefreshToken();
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.Now.AddDays(_options.RefreshTokenLifetime);
        _context.Set<TUser>().Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return new(newAccessToken, newRefreshToken);
    }

    protected async Task<TUser?> GetByEmailAsync(string email, CancellationToken cancellationToken) => 
        await _context.Set<TUser>().FirstOrDefaultAsync(st => st.Email == email, cancellationToken);

    public async Task RevokeTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null) throw new ArgumentException("Invalid access or refresh token");

        var email = principal.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        var user = await GetByEmailAsync(email, cancellationToken);
        if (user == null || user.RefreshToken != refreshToken)
            throw new ArgumentException("Invalid refresh token");

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        _context.Set<TUser>().Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task MessageUpdateEmailAsync(TId userId, CancellationToken token = default)
    {
        var user = await _context.Set<TUser>().FindAsync(userId);
        if (user == null) throw new ArgumentException($"User with id {userId} is not found");
        await _notifier.SendEmailUpdateMessageAsync<TUser, TId>(user, token);
    }
}