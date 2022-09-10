using ISPH.Shared.Dtos.Advertisements.Responses;

namespace ISPH.Shared.Interfaces.Advertisements;
public interface IAdvertisementResponsesService
{
    Task AddAsync(Guid advertisementId, Guid studentId, string? coverLetter, CancellationToken token = default);
    Task<bool> CheckResponseAsync(Guid advertisementId, Guid studentId, CancellationToken token = default);
    Task DeleteAsync(Guid advertisementId, Guid studentId, CancellationToken token = default);
    Task<IEnumerable<AdvertisementResponseDto>> GetByCompanyAsync(Guid companyId, CancellationToken token = default);
}