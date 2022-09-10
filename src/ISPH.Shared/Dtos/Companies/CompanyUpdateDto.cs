namespace ISPH.Shared.Dtos.Companies;
public record CompanyUpdateDto : IDto<Guid>
{
    public string? Name { get; set; }
    public string? Website { get; set; }
    public string? Path { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    [JsonIgnore]
    public Guid? Id { get; set; }
}