namespace ISPH.Shared.Dtos.Advertisements.Responses;

public record AdvertisementResponseCreateDto
{
    [Required]
    public Guid AdvertisementId { get; set; }
    [Required]
    public Guid StudentId { get; set; }
    public string? CoverLetter { get; set; }
}