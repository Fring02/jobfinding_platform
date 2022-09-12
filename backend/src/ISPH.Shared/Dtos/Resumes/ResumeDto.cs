namespace ISPH.Shared.Dtos.Resumes;

public record ResumeDto : IDto<Guid>
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
}