namespace ISPH.Shared.Dtos.Authorization;

public record RegisterDto<TId> : IDto<TId> where TId : struct
{
    [Required] 
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Patronymic { get; set; } = null!;
    [Required, EmailAddress(ErrorMessage = "Email in in wrong format")]
    public string Email { get; set; } = null!;
    [Required, Phone(ErrorMessage = "Phone is in wrong format")]
    public string Phone { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Compare("Password", ErrorMessage = "Password does not match entered one")]
    public string RepeatPassword { get; set; } = null!;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TId? Id { get; set; }
}