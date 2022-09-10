using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace ISPH.Api.Filters;

public class ExceptionHandleFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            ArgumentException e => new BadRequestObjectResult(e.Message),
            SecurityTokenException e => new ForbidResult(e.Message),
            _ => new StatusCodeResult(500)
        };
    }
}