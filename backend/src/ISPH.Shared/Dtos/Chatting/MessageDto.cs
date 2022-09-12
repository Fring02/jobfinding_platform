namespace ISPH.Shared.Dtos.Chatting;

public record MessageDto
{
    public string SenderName { get; set; }
    public string Role { get; set; }
    public DateTime LeftAt { get; set; }
    public string Text { get; set; }
}