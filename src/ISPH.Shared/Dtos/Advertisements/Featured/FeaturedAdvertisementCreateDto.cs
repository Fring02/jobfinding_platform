namespace ISPH.Shared.Dtos.Advertisements.Featured;

public record FeaturedAdvertisementCreateDto
{
    [Required]
    public Guid StudentId { get; set; }
    [Required]
    public Guid AdvertisementId { get; set; }
}