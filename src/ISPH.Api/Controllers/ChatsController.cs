﻿using System.ComponentModel.DataAnnotations;
using ISPH.Application.Services.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class ChatsController : ControllerBase
{
    private readonly ChatService _service;
    public ChatsController(ChatService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAllChats([Required(AllowEmptyStrings = false)] Guid userId, [Required(AllowEmptyStrings = false)] string role, 
        CancellationToken token) 
        => Ok(await _service.GetAllChatsAsync(userId, role, token));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetChatById([Required(AllowEmptyStrings = false)] Guid id, CancellationToken token) => 
        Ok(await _service.GetChatByIdAsync(id, token));
}