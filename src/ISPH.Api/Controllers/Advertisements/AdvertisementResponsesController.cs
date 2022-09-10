using ISPH.Domain.Configuration;
using ISPH.Shared.Dtos.Advertisements.Responses;
using ISPH.Shared.Interfaces.Advertisements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ISPH.Api.Extensions;

namespace ISPH.Api.Controllers.Advertisements;

[Route("api/v1/responses")]
[ApiController]
public class AdvertisementResponsesController : ControllerBase
{
    private readonly IAdvertisementResponsesService _service;
    public AdvertisementResponsesController(IAdvertisementResponsesService service) => _service = service;
    
    [HttpPost, Authorize(Roles = Roles.Student)]
    public async Task<IActionResult> CreateAdvertisementResponseAsync([FromBody] AdvertisementResponseCreateDto dto, CancellationToken token)
    {
        await _service.AddAsync(dto.AdvertisementId, dto.StudentId, dto.CoverLetter, token);
        return Ok("Your response was successfully added. Please, await for employer to contact you.");
    }
    
    [HttpGet, Authorize(Roles = Roles.Employer)]
    public async Task<IActionResult> GetAdvertisementResponsesAsync(Guid companyId, CancellationToken token) => 
        Ok(await _service.GetByCompanyAsync(companyId, token));

    [HttpGet("check"), Authorize(Roles = Roles.Student)]
    public async Task<IActionResult> CheckAdvertisementResponseAsync(Guid advertisementId, Guid studentId, CancellationToken token) => 
        Ok(await _service.CheckResponseAsync(advertisementId, studentId, token));

    [HttpDelete, Authorize(Roles = Roles.Employer)]
    public async Task<IActionResult> DeleteAdvertisementResponseAsync(Guid advertisementId, Guid studentId, CancellationToken token)
    {
        await _service.DeleteAsync(advertisementId, studentId, token);
        return Ok("Deleted");
    }
}