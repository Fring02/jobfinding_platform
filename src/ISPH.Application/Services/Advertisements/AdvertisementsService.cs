using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISPH.Application.Services.Base;
using ISPH.Data.Contexts;
using ISPH.Data.Extensions;
using ISPH.Data.Filtering;
using ISPH.Domain.Models.Advertisements;
using ISPH.Shared.Dtos.Advertisements;
using ISPH.Shared.Interfaces.Advertisements;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Application.Services.Advertisements;

public class AdvertisementsService : BaseCrudService<Advertisement, AdvertisementCreateDto, AdvertisementUpdateDto, Guid>, 
    IAdvertisementsService
{
    public AdvertisementsService(ApplicationContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<AdvertisementCreateDto> CreateAsync(AdvertisementCreateDto entity, CancellationToken token = default)
    {
        if(!await _context.Employers.AnyAsync(e => e.Id == entity.EmployerId, cancellationToken: token))
            throw new ArgumentException("Employer not found by id " + entity.EmployerId);
        
        var position = await _context.Positions.FindAsync(new object?[] { entity.PositionId }, cancellationToken: token);
        if (position == null) throw new ArgumentException("Position not found by id " + entity.PositionId);
        position.Amount++;
        return await base.CreateAsync(entity, token);
    }

    public override async Task DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        var entity = await _context.Advertisements.FindAsync(new object?[] { id }, cancellationToken: token);
        if (entity == null) throw new ArgumentException($"Advertisement with id {id} is not found");
        
        var position = await _context.Positions.FindAsync(new object?[] { entity.PositionId }, cancellationToken: token);
        if (position == null) throw new ArgumentException($"Position by id {entity.PositionId} is not found");
        position.Amount--;
        
        await base.DeleteByIdAsync(id, token);
    }
    public async Task<FilteredAdvertisementsDto> FilterAsync(AdvertisementFilterDto ad, int page = 0, int pageCount = 5,
        CancellationToken token = default)
    {
        var filter = new ExpressionFilterBuilder();
        if (!string.IsNullOrEmpty(ad.Value))
            filter = filter.With(a => a.Title.Contains(ad.Value));
        else
        {
            if (ad.SalaryLeftBound > 0 && ad.SalaryRightBound > ad.SalaryLeftBound)
                filter = filter.With(a => a.Salary >= ad.SalaryLeftBound && a.Salary <= ad.SalaryRightBound);
            if (ad.CompanyId != default) filter = filter.With(a => a.Employer.CompanyId == ad.CompanyId);
            if (ad.PositionId != default) filter = filter.With(a => a.PositionId == ad.PositionId);
            if (!string.IsNullOrEmpty(ad.WorkTime))
            {
                var workTime = Enum.Parse<WorkTime>(ad.WorkTime);
                filter = filter.With(a => a.WorkTimeType == workTime);
            }
            if (!string.IsNullOrEmpty(ad.EmploymentType))
            {
                var employmentType = Enum.Parse<EmploymentType>(ad.EmploymentType);
                filter = filter.With(a => a.EmploymentType == employmentType);
            }
        }
        var filteredAdvertisements = await _context.Advertisements.Where(filter.Result).Paginate(page, pageCount)
            .ProjectTo<AdvertisementItemDto>(_mapper.ConfigurationProvider).ToListAsync(token);
        uint salary;
        try
        {
            salary = await _context.Advertisements.MaxAsync(a => a.Salary, token);
        }
        catch (InvalidOperationException)
        {
            salary = 0;
        }
        return new(filteredAdvertisements, filteredAdvertisements.Count, salary);
    }

    public async Task DeleteByEmployerIdAsync(Guid employerId, CancellationToken token = default)
    {
        if (!await _context.Advertisements.AnyAsync(a => a.EmployerId == employerId, token))
            throw new ArgumentException($"Advertisements of employer with id {employerId} are not found");
        _context.Advertisements.RemoveRange(_context.Advertisements.Where(a => a.EmployerId == employerId));
        await _context.SaveChangesAsync(token);
    }

    public override async Task UpdateAsync(AdvertisementUpdateDto entity, CancellationToken token = default)
    {
        var advertisement = await _context.Advertisements.FindAsync(new object?[] { entity.Id }, cancellationToken: token);
        if (advertisement == null) throw new ArgumentException($"Advertisement with id {entity.Id} is not found");
        
        if (!string.IsNullOrEmpty(entity.Title)) advertisement.Title = entity.Title;
        if (entity.Salary > 0) advertisement.Salary = entity.Salary.Value;
        if (entity.PositionId.HasValue && entity.PositionId != Guid.Empty) advertisement.PositionId = entity.PositionId.Value;
        if (!string.IsNullOrEmpty(entity.WorkTime)) advertisement.WorkTimeType = Enum.Parse<WorkTime>(entity.WorkTime);
        if (!string.IsNullOrEmpty(entity.EmploymentType)) advertisement.EmploymentType = Enum.Parse<EmploymentType>(entity.EmploymentType);
        if (!string.IsNullOrEmpty(entity.Description)) advertisement.Description = entity.Description;
        await _context.SaveChangesAsync(token);
    }
}