using ISPH.Domain.Models;
using ISPH.Shared.Dtos.Companies;
using ISPH.Shared.Interfaces.Base;

namespace ISPH.Shared.Interfaces;

public interface ICompaniesService : ICrudService<Company, CompanyCreateDto, CompanyUpdateDto, Guid>
{
    Task<CompanyViewDto?> GetByNameAsync(string name, CancellationToken token = default);
}