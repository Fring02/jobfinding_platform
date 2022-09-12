using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ISPH.Api.Filters;

public class ResourceAuthorizationFilter : IAsyncResourceFilter
{
    private readonly IAuthorizationService _authorizationService;
    public ResourceAuthorizationFilter(IAuthorizationService authorizationService) => _authorizationService = authorizationService;

    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var userId = context.RouteData.Values.Values.Last();
        if (userId == null) userId = context.HttpContext.Request.Query["userId"];
        var result = await _authorizationService.AuthorizeAsync(context.HttpContext.User, Guid.Parse(userId.ToString()), "SameUserPolicy");
        if (!result.Succeeded)
        {
            if (context.HttpContext.User.Identity is not { IsAuthenticated: true })
                context.Result = new UnauthorizedResult();
            else if (context.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value !=
                     userId!.ToString()) context.Result = new ForbidResult();
        }
        else await next();
    }
}