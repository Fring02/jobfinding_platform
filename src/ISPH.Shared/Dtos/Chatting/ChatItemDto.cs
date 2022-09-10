namespace ISPH.Shared.Dtos.Chatting;

public record ChatItemDto
{
    public Guid Id { get; set; }
    public string RecipientName { get; set; }
    public string LastMessage { get; set; }
}