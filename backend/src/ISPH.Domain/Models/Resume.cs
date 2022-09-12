using ISPH.Domain.Models.Base;
using ISPH.Domain.Models.Users;

namespace ISPH.Domain.Models;

public class Resume : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public async Task<Stream> DownloadAsync()
    {
        var memStream = new MemoryStream();
        await using var pathStream = new FileStream(Path, FileMode.Open);
        await pathStream.CopyToAsync(memStream);
        memStream.Position = 0;
        return memStream;
    }
}