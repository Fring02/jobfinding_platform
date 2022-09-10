using ISPH.Domain.Models.Base;
using ISPH.Domain.Models.Users;

namespace ISPH.Domain.Models;

public class Company : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;
    public string Address { get; set; }
    public string? Description { get; set; }
    public string Path { get; set; } = null!;
    public string Website { get; set; } = null!;
    public IEnumerable<Employer> Employers { get; set; }
}