using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISPH.Data.Contexts;
using ISPH.Domain.Configuration;
using ISPH.Domain.Models.Messaging;
using ISPH.Shared.Dtos.Chatting;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Application.Services.Messaging;

public class ChatService
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;
    public ChatService(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task StartChatAsync(CreateChatDto chat, CancellationToken token = default)
    {
        var newChat = new Chat { EmployerId = chat.EmployerId, StudentId = chat.StudentId, Id = Guid.NewGuid() };
        _context.Chats.Add(newChat);
        _context.Messages.Add(new Message { ChatId = newChat.Id, Text = chat.Message, LeftAt = DateTime.Now });
        await _context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<ChatItemDto>> GetAllChatsAsync(Guid userId, string role, CancellationToken token = default)
    {
        var chats = _context.Chats.AsNoTracking();
        return role switch
        {
            Roles.Employer => await chats.Where(c => c.EmployerId == userId).Select(c => new ChatItemDto
            {
                Id = c.Id, RecipientName = c.Student.FirstName + " " + c.Student.LastName, 
                LastMessage = c.Messages.OrderBy(m => m.LeftAt).Last().Text
            }).ToListAsync(token),
            Roles.Student => await chats.Where(c => c.StudentId == userId).Select(c => new ChatItemDto
            {
                Id = c.Id, RecipientName = c.Employer.FirstName + " " + c.Employer.LastName, LastMessage = c.Messages.OrderBy(m => m.LeftAt).Last().Text
            }).ToListAsync(token),
            _ => Array.Empty<ChatItemDto>()
        };
    }
    public async Task<ChatViewDto> GetChatByIdAsync(Guid chatId, CancellationToken token = default) => 
        await _context.Chats.AsNoTracking().ProjectTo<ChatViewDto>(_mapper.ConfigurationProvider).SingleAsync(c => c.Id == chatId, token);

    public async Task SaveMessageAsync(Guid chatId, string text)
    {
        var message = new Message { ChatId = chatId, Text = text, LeftAt = DateTime.Now };
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
    }
}