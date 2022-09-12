using ISPH.Api.Authorization;
using ISPH.Api.Authorization.Handlers;
using ISPH.Api.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace ISPH.Api.Extensions;

public static class AuthorizationRegistration
{
    public static void AddResourceAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(opts => opts.AddPolicy("SameUserPolicy", policy => policy.Requirements.Add(new SameUserRequirement())));
        services.AddSingleton<IAuthorizationHandler, SameUserAuthorizationHandler>();
    }
}