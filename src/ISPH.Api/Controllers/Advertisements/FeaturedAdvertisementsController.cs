﻿using ISPH.Domain.Configuration;
using ISPH.Shared.Interfaces.Advertisements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ISPH.Api.Extensions;

namespace ISPH.Api.Controllers.Advertisements;

[Route("api/v1/featured")]
[Authorize(Roles = Roles.Student)]
public class FeaturedAdvertisementsController : ControllerBase
{
    private readonly IFeaturedAdvertisementsService _service;
    public FeaturedAdvertisementsController(IFeaturedAdvertisementsService service) => _service = service;
    
    [HttpPost]
    public async Task<IActionResult> AddToFeatured(Guid advertisementId, Guid studentId, CancellationToken token)
    {
        await _service.AddAsync(studentId, advertisementId, token);
        return Ok("Added to featured");
    }
    [HttpGet("check")]
    public async Task<IActionResult> CheckFeaturedAdvertisement(Guid studentId, Guid advertisementId, CancellationToken token) => 
        Ok(await _service.CheckFeatured(studentId, advertisementId, token));

    [HttpGet]
    public async Task<IActionResult> GetFeaturedAdvertisements(Guid studentId, Guid advertisementId, CancellationToken token)
    {
        if (advertisementId == Guid.Empty)
        {
            var data = await _service.GetByStudentIdAsync(studentId, token);
            return data.Any() ? Ok(data) : NoContent();
        }
        else
        {
            var data = await _service.GetByIdsAsync(studentId, advertisementId, token);
            return data != null ? Ok(data) : NoContent();
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteFromFeatured(Guid advertisementId, Guid studentId, CancellationToken token)
    {
        await _service.DeleteAsync(studentId, advertisementId, token);
        return Ok("Deleted from featured");
    }
}