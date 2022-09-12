using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using ISPH.Api.Controllers.Base;
using ISPH.Domain.Configuration;
using ISPH.Domain.Models.Advertisements;
using ISPH.Shared.Dtos.Advertisements;
using ISPH.Shared.Interfaces.Advertisements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.Api.Controllers.Advertisements;

[Route("api/v1/[controller]")]
public class AdvertisementsController : CrudController<Advertisement, Guid, AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementViewDto,
    AdvertisementItemDto, IAdvertisementsService>
{
    public AdvertisementsController(IAdvertisementsService service) : base(service)
    {
    }
    [HttpGet]
    public override async Task<IActionResult> GetAllAsync(CancellationToken token, int page = 0)
    {
        var (data, maxSalary, count) = await Service.FilterAsync(AdvertisementFilter, page, token: token);
        return Ok(new { count, data, maxSalary });
    }
    [HttpDelete, Authorize(Roles = Roles.Employer)]
    public async Task<IActionResult> DeleteByEmployerAsync([Required(AllowEmptyStrings = false)] Guid employerId, CancellationToken token)
    {
        if (HttpContext.User.Claims.First(c => c.Type != ClaimTypes.NameIdentifier).Value == employerId.ToString()) return Forbid();
        await Service.DeleteByEmployerIdAsync(employerId, token);
        return Ok("Deleted for employer");
    }
    [AllowAnonymous]
    public override Task<IActionResult> GetByIdAsync(Guid id, CancellationToken token) => base.GetByIdAsync(id, token);


    private AdvertisementFilterDto AdvertisementFilter
    {
        get
        {
            var filter = new AdvertisementFilterDto();
            if (Guid.TryParse(Request.Query["companyId"], out var companyId)) filter.CompanyId = companyId;
            if (Guid.TryParse(Request.Query["positionId"], out var positionId)) filter.PositionId = positionId;
            if (uint.TryParse(Request.Query["salaryLeft"], out var salaryLeftBound)) filter.SalaryLeftBound = salaryLeftBound;
            if (uint.TryParse(Request.Query["salaryRight"], out var salaryRightBound)) filter.SalaryRightBound = salaryRightBound;
            if (!string.IsNullOrEmpty(Request.Query["value"])) filter.Value = Request.Query["value"];
            if (!string.IsNullOrEmpty(Request.Query["workTime"])) filter.WorkTime = Request.Query["workTime"];
            if (!string.IsNullOrEmpty(Request.Query["employmentType"])) filter.EmploymentType = Request.Query["employmentType"];
            return filter;
        }
    }
}