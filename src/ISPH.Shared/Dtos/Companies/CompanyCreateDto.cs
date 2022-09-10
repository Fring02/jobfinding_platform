namespace ISPH.Shared.Dtos.Companies;

public record CompanyCreateDto : IDto<Guid>
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Path { get; set; }
    [Required]
    public string Website { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Address { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? Id { get; set; }
}