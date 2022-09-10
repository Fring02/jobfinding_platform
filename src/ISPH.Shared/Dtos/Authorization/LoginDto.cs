namespace ISPH.Shared.Dtos.Authorization;

public record LoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}