namespace ISPH.Shared.Dtos.Advertisements;

public record AdvertisementItemDto : IDto<Guid>
{
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public uint Salary { get; set; }
    public string Description { get; set; }
    public string PositionName { get; set; }
    public string CompanyName { get; set; }
    public string WorkTime { get; set; }
    public string EmploymentType { get; set; }
    public string PostedAt { get; set; }
}