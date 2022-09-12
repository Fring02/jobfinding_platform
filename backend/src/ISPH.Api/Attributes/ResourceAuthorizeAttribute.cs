using ISPH.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.Api.Attributes;

public class ResourceAuthorizeAttribute : TypeFilterAttribute
{
    public ResourceAuthorizeAttribute() : base(typeof(ResourceAuthorizationFilter))
    {
    }
}