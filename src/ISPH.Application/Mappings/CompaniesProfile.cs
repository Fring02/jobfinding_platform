using AutoMapper;
using ISPH.Domain.Models;
using ISPH.Shared.Dtos.Companies;

namespace ISPH.Application.Mappings;

public class CompaniesProfile : Profile
{
    public CompaniesProfile()
    {
        CreateMap<Company, CompanyItemDto>();
        CreateMap<Company, CompanyViewDto>();
        CreateMap<CompanyCreateDto, Company>();
        CreateMap<CompanyUpdateDto, Company>();
    }
}