using System.Security.Claims;
using ISPH.Api.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace ISPH.Api.Authorization.Handlers;

public class SameUserAuthorizationHandler : AuthorizationHandler<SameUserRequirement, Guid>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement,
        Guid resource)
    {
        if(context.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value == resource.ToString())
            context.Succeed(requirement);
        return Task.CompletedTask;
    }
}