namespace ISPH.Shared.Dtos.Advertisements.Featured;

public record FeaturedAdvertisementItemDto
{
    public Guid AdvertisementId { get; set; }
    public AdvertisementItemDto Advertisement { get; set; }
}