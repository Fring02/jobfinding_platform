using System;
using System.Threading.Tasks;
using ISPH.Application.Services.Messaging;
using Microsoft.AspNetCore.SignalR;

namespace ISPH.Application.Hubs;
public class ChatHub : Hub
{
    private readonly ChatService _chatService;
    public ChatHub(ChatService chatService) => _chatService = chatService;
    public async Task Start(Guid chatId) => await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    public async Task Message(Guid chatId, string message, string userName) => 
        await _chatService.SaveMessageAsync(chatId, message)
            .ContinueWith(_ => Clients.Group(chatId.ToString()).SendAsync("Message", message, userName));
    public async Task End(Guid chatId) => await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
}