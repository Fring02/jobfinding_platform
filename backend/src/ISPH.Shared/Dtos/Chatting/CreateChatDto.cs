namespace ISPH.Shared.Dtos.Chatting;

public record CreateChatDto
{
    [Required]
    public Guid EmployerId { get; set; }
    [Required]
    public Guid StudentId { get; set; }
    [Required]
    public string Message { get; set; }
}