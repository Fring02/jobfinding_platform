using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISPH.Application.Services.Base;
using ISPH.Data.Contexts;
using ISPH.Domain.Models;
using ISPH.Shared.Dtos.Companies;
using ISPH.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Application.Services;

public class CompaniesService : BaseCrudService<Company, CompanyCreateDto, CompanyUpdateDto, Guid>, ICompaniesService
{
    public CompaniesService(ApplicationContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<CompanyCreateDto> CreateAsync(CompanyCreateDto entity, CancellationToken token = default)
    {
        if (await _context.Companies.AnyAsync(c => c.Name == entity.Name, token))
            throw new ArgumentException($"Company with name {entity.Name} already exists");
        return await base.CreateAsync(entity, token);
    }

    public async Task<CompanyViewDto?> GetByNameAsync(string name, CancellationToken token = default) =>
        await _context.Companies.ProjectTo<CompanyViewDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Name == name, token);
}