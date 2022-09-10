using ISPH.Shared.Dtos.Advertisements.Featured;

namespace ISPH.Shared.Interfaces.Advertisements;

public interface IFeaturedAdvertisementsService
{
    Task AddAsync(Guid studentId, Guid advertisementId, CancellationToken token = default);
    Task DeleteAsync(Guid studentId, Guid advertisementId, CancellationToken token = default);
    Task<FeaturedAdvertisementViewDto?> GetByIdsAsync(Guid studentId, Guid advertisementId, CancellationToken token = default);
    Task<IEnumerable<FeaturedAdvertisementViewDto>> GetByStudentIdAsync(Guid studentId, CancellationToken token = default);
    Task<bool> CheckFeatured(Guid studentId, Guid advertisementId, CancellationToken token = default);
}