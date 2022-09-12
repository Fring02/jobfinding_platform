using ISPH.Domain.Models.Advertisements;
using ISPH.Domain.Models.Base;

namespace ISPH.Domain.Models;

public class Position : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;
    public int Amount { get; set; }
    public string Path { get; set; } = null!;
    public string? Description { get; set; }
    public IEnumerable<Advertisement> Advertisements { get; set; }
}