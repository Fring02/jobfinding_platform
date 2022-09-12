using System.Security.Claims;
using ISPH.Application.Utils.Auth;
using ISPH.Domain.Configuration;
using ISPH.Shared.Interfaces.Users;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ISPH.Api.Filters;

public class AutomaticRefreshTokenFilter : ActionFilterAttribute, IAsyncResourceFilter
{
    private readonly JwtService _service;
    private readonly IStudentsService _studentsService;
    private readonly IEmployersService _employersService;
    private readonly ILogger<AutomaticRefreshTokenFilter> _logger;
    public AutomaticRefreshTokenFilter(JwtService service, IStudentsService studentsService, IEmployersService employersService,
        ILogger<AutomaticRefreshTokenFilter> logger)
    {
        _service = service;
        _studentsService = studentsService;
        _employersService = employersService;
        _logger = logger;
    }

    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        if (context.HttpContext.Request.Headers.Authorization.Count > 0)
        {
            var stringToken = context.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
            var token = _service.ParseToken(stringToken);
            _logger.LogInformation("Access token: {StringToken}", stringToken);
            if (token.ValidTo - DateTime.Now <= TimeSpan.FromMinutes(5))
            {
                _logger.LogWarning("Token needs to be refreshed: Expiry date = {ExpiryDate}", token.ValidTo);
                var cancellationTokenSource = new CancellationTokenSource();
                var (accessToken, refreshToken) = (context.HttpContext.Request.Cookies["jwt-access"],
                    context.HttpContext.Request.Cookies["jwt-refresh"]);
                _logger.LogInformation(
                    "Tokens found in request cookies: Access = {AccessToken}, Refresh = {RefreshToken}", accessToken,
                    refreshToken);
                var (newAccessToken, newRefreshToken) =
                    token.Payload.Claims.Single(c => c.Type == ClaimTypes.Role).Value == Roles.Student
                        ? await _studentsService.RefreshTokenAsync(accessToken!, refreshToken!,
                            cancellationTokenSource.Token)
                        : await _employersService.RefreshTokenAsync(accessToken!, refreshToken!,
                            cancellationTokenSource.Token);

                _logger.LogInformation(
                    "Successfully refreshed token: new Access = {AccessToken}, new Refresh = {RefreshToken}",
                    newAccessToken, newRefreshToken);
            }
        }
        await next();
    }
}