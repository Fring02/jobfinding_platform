namespace ISPH.Shared.Dtos.Advertisements;
public record AdvertisementCreateDto : IDto<Guid>
{
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public uint Salary { get; set; }
    [Required]
    public string Description { get; set; } = null!;
    public string WorkTime { get; set; } = "Undefined";
    public string EmploymentType { get; set; } = "Undefined";
    [Required]
    public Guid PositionId { get; set; }
    [Required]
    public Guid EmployerId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid? Id { get; set; }
}