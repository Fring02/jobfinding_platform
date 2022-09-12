namespace ISPH.Shared.Dtos.Chatting;

public class ChatViewDto
{
    public Guid Id { get; set; }
    public string RecipientName { get; set; }
    public IEnumerable<MessageDto> Messages { get; set; }
}