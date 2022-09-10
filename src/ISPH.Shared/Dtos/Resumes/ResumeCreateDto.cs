namespace ISPH.Shared.Dtos.Resumes;

public record ResumeCreateDto : IDto<Guid>
{
    public Guid StudentId { get; set; }
    public string Path { get; set; }
    public string Name { get; set; }
    public Stream File { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? Id { get; set; }
}