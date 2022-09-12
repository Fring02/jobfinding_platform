using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISPH.Application.Services.Base;
using ISPH.Data.Contexts;
using ISPH.Domain.Models;
using ISPH.Shared.Dtos.Positions;
using ISPH.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Application.Services;

public class PositionsService : BaseCrudService<Position, PositionCreateDto, PositionUpdateDto, Guid>, IPositionsService
{
    public PositionsService(ApplicationContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<PositionViewDto?> GetByNameAsync(string name, CancellationToken token = default) =>
        await _context.Positions.ProjectTo<PositionViewDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Name == name, token);
}