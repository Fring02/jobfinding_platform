namespace ISPH.Shared.Dtos.Advertisements;

public record AdvertisementUpdateDto : IDto<Guid>
{
    public string? Title { get; set; }
    public uint? Salary { get; set; }
    public string? Description { get; set; }
    public Guid? PositionId { get; set; }
    public string? WorkTime { get; set; }
    public string? EmploymentType { get; set; }
    [JsonIgnore]
    public Guid? Id { get; set; }
}