using ISPH.Shared.Dtos.Authorization;

namespace ISPH.Shared.Dtos.Users;

public record StudentCreateDto : RegisterDto<Guid>
{
    [Required]
    public string University { get; set; }
    [Required]
    public string Faculty { get; set; }
    [Range(1, 5), Required]
    public ushort Course { get; set; }
}