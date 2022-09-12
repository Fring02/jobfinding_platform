using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace ISPH.Api.Filters;

public class ExceptionHandleFilter : ActionFilterAttribute, IExceptionFilter
{
    private readonly ILogger<ExceptionHandleFilter> _logger;
    public ExceptionHandleFilter(ILogger<ExceptionHandleFilter> logger) => _logger = logger;

    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ArgumentException e:
                _logger.LogWarning("400 Bad Request: {e.Message}", e.Message);
                context.Result = new BadRequestObjectResult(e.Message);
                break;
            case SecurityTokenException e:
                _logger.LogWarning("403 Forbidden: {e.Message}", e.Message);
                context.Result = new ForbidResult(e.Message);
                break;
            default:
                _logger.LogError("500 Server Error: {e.Message}", context.Exception.Message);
                context.Result = new StatusCodeResult(500);
                break;
        }
    }
}