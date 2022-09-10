namespace ISPH.Shared.Dtos.Advertisements.Responses;

public record AdvertisementResponseDto
{
    public string LeftAt { get; set; }
    public string? CoverLetter { get; set; }
    public string Title { get; set; }
    public string PositionName { get; set; }
    public string WorkTime { get; set; }
    public string EmploymentType { get; set; }
    public Guid AdvertisementId { get; set; }
    public AdvertisementResponseStudentDto Student { get; set; }
}
