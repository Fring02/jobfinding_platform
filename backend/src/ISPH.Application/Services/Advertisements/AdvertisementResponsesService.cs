using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISPH.Data.Contexts;
using ISPH.Domain.Models.Advertisements;
using ISPH.Shared.Dtos.Advertisements.Responses;
using ISPH.Shared.Interfaces.Advertisements;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Application.Services.Advertisements;

public class AdvertisementResponsesService : IAdvertisementResponsesService
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;
    public AdvertisementResponsesService(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddAsync(Guid advertisementId, Guid studentId, string? coverLetter, CancellationToken token = default)
    {
        if (!await _context.Advertisements.AnyAsync(a => a.Id == advertisementId, token))
            throw new ArgumentException($"Advertisement with id {advertisementId} is not found");
        if (!await _context.Students.AnyAsync(s => s.Id == studentId, token))
            throw new ArgumentException($"Student with id {studentId} is not found");
        AdvertisementResponse response = new() { CoverLetter = coverLetter, AdvertisementId = advertisementId, StudentId = studentId };
        _context.AdvertisementResponses.Add(response);
        await _context.SaveChangesAsync(token);
    }

    public async Task<bool> CheckResponseAsync(Guid advertisementId, Guid studentId, CancellationToken token = default)
        => await _context.AdvertisementResponses.AnyAsync(r => r.AdvertisementId == advertisementId && r.StudentId == studentId, token);

    public async Task DeleteAsync(Guid advertisementId, Guid studentId, CancellationToken token = default)
    {
        var response = await _context.AdvertisementResponses.FirstOrDefaultAsync(r => r.AdvertisementId == advertisementId && r.StudentId == studentId, token);
        if (response == null) throw new ArgumentException($"Response is not found");
        _context.AdvertisementResponses.Remove(response);
        await _context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<AdvertisementResponseDto>> GetByCompanyAsync(Guid companyId, CancellationToken token = default)
        => await _context.AdvertisementResponses.AsNoTracking().Where(r => r.Advertisement.Employer.CompanyId == companyId)
            .ProjectTo<AdvertisementResponseDto>(_mapper.ConfigurationProvider).ToListAsync(token);
}