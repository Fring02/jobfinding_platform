namespace ISPH.Shared.Dtos.Users.Base;
public record UserUpdateDto<TId> : IDto<TId> where TId : struct
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Phone { get; set; }
    [JsonIgnore]
    public TId? Id { get; set; }
}