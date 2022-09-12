using ISPH.Api.Attributes;
using ISPH.Domain.Models.Base;
using ISPH.Shared.Dtos.Authorization;
using ISPH.Shared.Dtos.Interfaces;
using ISPH.Shared.Dtos.Users.Base;
using ISPH.Shared.Interfaces.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.Api.Controllers.Base.Auth;

public abstract class UsersController<TUser, TId, TRegisterDto, TUpdateDto, TViewDto, TItemDto, TService> 
    : CrudController<TUser, TId, TRegisterDto, TUpdateDto, TViewDto, TItemDto, TService> 
    where TUser : BaseUser<TId> 
    where TRegisterDto : RegisterDto<TId> 
    where TUpdateDto : UserUpdateDto<TId>
    where TViewDto : class, IDto<TId>
    where TItemDto : class, IDto<TId>
    where TService : IUserService<TUser, TRegisterDto, TUpdateDto, TId>
    where TId : struct
{
    private readonly CookieOptions _accessOptions = new()
    {
        HttpOnly = false, IsEssential = true, Secure = true, Expires = new DateTimeOffset(DateTime.Today.AddDays(1)), 
        SameSite = SameSiteMode.None
    };
    private readonly CookieOptions _refreshOptions = new()
    {
        IsEssential = true, Secure = true, Expires = new DateTimeOffset(DateTime.Today.AddDays(7)), SameSite = SameSiteMode.None
    };
    protected UsersController(TService service) : base(service)
    {
    }
    [HttpPost("register")]
    public override async Task<IActionResult> CreateAsync([FromBody] TRegisterDto registerUser, CancellationToken token)
    {
        var (accessToken, refreshToken) = await Service.RegisterAsync(registerUser, token);
        Response.Cookies.Append("jwt-access", accessToken, _accessOptions);
        Response.Cookies.Append("jwt-refresh", refreshToken, _refreshOptions);
        return Created(Request.Path, new { accessToken, refreshToken });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginUser, CancellationToken token)
    {
        var (accessToken, refreshToken) = await Service.LoginAsync(loginUser.Email, loginUser.Password, token);
        Response.Cookies.Append("jwt-access", accessToken, _accessOptions);
        Response.Cookies.Append("jwt-refresh", refreshToken, _refreshOptions);
        return Accepted(Request.Path, new { accessToken, refreshToken });
    }

    [HttpPost("refresh"), Authorize]
    public async Task<IActionResult> RefreshToken([FromHeader(Name = "Access-Token")] string accessToken, 
        [FromHeader(Name = "Refresh-Token")] string refreshToken, CancellationToken token)
    {
        var (newAccessToken, newRefreshToken) = await Service.RefreshTokenAsync(accessToken, refreshToken, token); 
        Response.Cookies.Append("jwt-access", newAccessToken, _accessOptions);
        Response.Cookies.Append("jwt-refresh", newRefreshToken, _refreshOptions);
        return Accepted(Request.Path, new { accessToken = newAccessToken, refreshToken = newRefreshToken });
    }

    [HttpPost("revoke"), Authorize]
    public async Task<IActionResult> RevokeToken(CancellationToken token,
        [FromHeader(Name = "Access-Token")] string accessToken, [FromHeader(Name = "Refresh-Token")] string refreshToken)
    {
        await Service.RevokeTokenAsync(accessToken, refreshToken, token);
        Response.Cookies.Delete("jwt-access");
        Response.Cookies.Delete("jwt-refresh");
        return Ok("Successful revoke");
    }

    [HttpPut("update_email/{id}"), Authorize, ResourceAuthorize]
    public async Task<IActionResult> SendUpdateEmailMessageAsync(TId id, CancellationToken token)
    {
        await Service.MessageUpdateEmailAsync(id, token);
        return Ok("A message of confirming email update has been sent to your old email address. Please, check it.");
    }
}