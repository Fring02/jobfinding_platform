using ISPH.Domain.Configuration;
using ISPH.Shared.Dtos.Resumes;
using ISPH.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ISPH.Api.Extensions;
namespace ISPH.Api.Controllers;

[Route("api/v1/[controller]/{studentId:guid}")]
[ApiController]
public class ResumesController : ControllerBase
{
    private readonly IResumesService _resumesService;
    private readonly string _folderPath;
    public ResumesController(IResumesService resumesService, IWebHostEnvironment environment)
    {
        _resumesService = resumesService;
        _folderPath = environment.WebRootPath + "/resumes/";
    }

    [HttpPost, Authorize(Roles = Roles.Student)]
    public async Task<IActionResult> UploadResume(IFormFile? file, Guid studentId, CancellationToken token)
    {
        if (file == null) return BadRequest("You didn't upload file");
        if (file.ContentType != "application/pdf") return BadRequest("File format must be in .pdf");
        var resume = new ResumeCreateDto { Name = file.FileName, Path = _folderPath + file.FileName, StudentId = studentId, File = file.OpenReadStream() };
        await _resumesService.CreateAsync(resume, token);
        return Ok("Uploaded resume");
    }

    [HttpGet, Authorize]
    public async Task<IActionResult> DownloadResume(Guid studentId, CancellationToken token)
    {
        var resume = await _resumesService.GetByStudentIdAsync(studentId, token);
        if (resume == null) return BadRequest("Resume not found");
        var resumeFile = await _resumesService.DownloadAsync(resume.Id!.Value, token);
        return File(resumeFile.File, "application/pdf", Path.GetFileName(resume.Path));
    }

    [HttpDelete, Authorize(Roles = Roles.Student)]
    public async Task<IActionResult> DeleteResumeAsync(Guid studentId, CancellationToken token)
    {
        var resume = await _resumesService.GetByStudentIdAsync(studentId, token);
        if (resume == null) return BadRequest("Resume not found");
        await _resumesService.DeleteByIdAsync(resume.Id.Value, token);
        return Ok("Deleted resume");
    }
}