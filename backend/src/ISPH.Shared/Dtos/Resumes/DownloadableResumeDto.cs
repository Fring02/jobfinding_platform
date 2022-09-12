namespace ISPH.Shared.Dtos.Resumes;

public record DownloadableResumeDto
{
    public Stream File { get; set; }
    public string Path { get; set; }
}