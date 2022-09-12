namespace ISPH.Shared.Dtos.Advertisements;

public record AdvertisementFilterDto
{
    public string? Value { get; set; }
    [JsonPropertyName("salaryLeft")]
    public uint? SalaryLeftBound { get; set; }
    [JsonPropertyName("salaryRight")]
    public uint? SalaryRightBound { get; set; }
    public string? WorkTime { get; set; }
    public string? EmploymentType { get; set; }
    public Guid? CompanyId { get; set; }
    public Guid? PositionId { get; set; }
}