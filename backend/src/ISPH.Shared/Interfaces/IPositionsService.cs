using ISPH.Domain.Models;
using ISPH.Shared.Dtos.Positions;
using ISPH.Shared.Interfaces.Base;

namespace ISPH.Shared.Interfaces;

public interface IPositionsService : ICrudService<Position, PositionCreateDto, PositionUpdateDto, Guid>
{
    Task<PositionViewDto?> GetByNameAsync(string name, CancellationToken token = default);
}