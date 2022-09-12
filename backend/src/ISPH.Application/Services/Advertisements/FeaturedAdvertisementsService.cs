using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISPH.Data.Contexts;
using ISPH.Domain.Models.Advertisements;
using ISPH.Shared.Dtos.Advertisements.Featured;
using ISPH.Shared.Interfaces.Advertisements;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Application.Services.Advertisements;

public class FeaturedAdvertisementsService : IFeaturedAdvertisementsService
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public FeaturedAdvertisementsService(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddAsync(Guid studentId, Guid advertisementId, CancellationToken token = default)
    {
        _context.FeaturedAdvertisements.Add(new FeaturedAdvertisement { AdvertisementId = advertisementId, StudentId = studentId });
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(Guid studentId, Guid advertisementId, CancellationToken token = default)
    {
        var featured = await _context.FeaturedAdvertisements
            .FirstOrDefaultAsync(f => f.StudentId == studentId && f.AdvertisementId == advertisementId, token);
        if (featured == null) throw new ArgumentException($"Advertisement {advertisementId} is not found in featured");
        _context.FeaturedAdvertisements.Remove(featured);
        await _context.SaveChangesAsync(token);
    }

    public async Task<FeaturedAdvertisementViewDto?> GetByIdsAsync(Guid studentId, Guid advertisementId, CancellationToken token = default) =>
        await _context.FeaturedAdvertisements.ProjectTo<FeaturedAdvertisementViewDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(f =>
                f.StudentId == studentId && f.AdvertisementId == advertisementId, cancellationToken: token);

    public async Task<IEnumerable<FeaturedAdvertisementViewDto>> GetByStudentIdAsync(Guid studentId, CancellationToken token = default) =>
        await _context.FeaturedAdvertisements
            .ProjectTo<FeaturedAdvertisementViewDto>(_mapper.ConfigurationProvider)
            .Where(f => f.StudentId == studentId).ToListAsync(cancellationToken: token);

    public async Task<bool> CheckFeatured(Guid studentId, Guid advertisementId, CancellationToken token = default)
        => await _context.FeaturedAdvertisements.AnyAsync(
            f => f.StudentId == studentId && f.AdvertisementId == advertisementId, token);
}