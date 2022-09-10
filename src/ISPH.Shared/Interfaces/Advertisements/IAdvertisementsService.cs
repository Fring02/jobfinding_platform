using ISPH.Domain.Models.Advertisements;
using ISPH.Shared.Dtos.Advertisements;
using ISPH.Shared.Interfaces.Base;

namespace ISPH.Shared.Interfaces.Advertisements;
public interface IAdvertisementsService : ICrudService<Advertisement, AdvertisementCreateDto, AdvertisementUpdateDto, Guid>
{
    Task<FilteredAdvertisementsDto> FilterAsync(AdvertisementFilterDto ad, int page = 0, int pageCount = 5, CancellationToken token = default);
    Task DeleteByEmployerIdAsync(Guid employerId, CancellationToken token = default);
}