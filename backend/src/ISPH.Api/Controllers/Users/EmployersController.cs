using System.Security.Claims;
using ISPH.Api.Attributes;
using ISPH.Api.Controllers.Base.Auth;
using ISPH.Application.Services.Messaging;
using ISPH.Domain.Configuration;
using ISPH.Domain.Models.Users;
using ISPH.Shared.Dtos.Chatting;
using ISPH.Shared.Dtos.Users;
using ISPH.Shared.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.Api.Controllers.Users;
[Route("api/v1/[controller]")]
public class EmployersController : UsersController<Employer, Guid, EmployerCreateDto, EmployerUpdateDto, EmployerViewDto,
    EmployerItemDto, IEmployersService>
{
    private readonly ChatService _chatService;
    public EmployersController(IEmployersService service, ChatService chatService) : base(service) => _chatService = chatService;

    [HttpPost("start_chat"), Authorize(Roles = Roles.Employer)]
    public async Task<IActionResult> StartChatAsync([FromBody] CreateChatDto chat, CancellationToken token)
    {
        if (HttpContext.User.Claims.First(c => c.Type != ClaimTypes.NameIdentifier).Value == chat.EmployerId.ToString()) return Forbid();
        await _chatService.StartChatAsync(chat, token);
        return Ok();
    }

    [HttpGet("{id}"), Authorize(Roles = Roles.Employer), ResourceAuthorize]
    public override Task<IActionResult> GetByIdAsync(Guid id, CancellationToken token) => base.GetByIdAsync(id, token);
}