namespace ISPH.Shared.Dtos.Advertisements.Responses;

public record AdvertisementResponseStudentDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string University { get; set; }
    public string Faculty { get; set; }
    public ushort Course { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}