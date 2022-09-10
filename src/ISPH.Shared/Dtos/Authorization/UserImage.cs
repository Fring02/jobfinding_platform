namespace ISPH.Shared.Dtos.Authorization;

public record UserImage
{
    public Stream File { get; set; }
    public string Path { get; set; }
    public string Name { get; set; }
}