using ISPH.Api.Controllers.Base;
using ISPH.Domain.Models;
using ISPH.Shared.Dtos.Companies;
using ISPH.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.Api.Controllers;

[Route("api/v1/[controller]")]
public class CompaniesController : CrudController<Company, Guid, CompanyCreateDto, CompanyUpdateDto, CompanyViewDto, CompanyItemDto,
    ICompaniesService>
{
    public CompaniesController(ICompaniesService service) : base(service)
    {
    }
}