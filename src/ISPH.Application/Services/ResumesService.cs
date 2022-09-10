using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISPH.Application.Services.Base;
using ISPH.Data.Contexts;
using ISPH.Domain.Models;
using ISPH.Shared.Dtos.Resumes;
using ISPH.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Application.Services;

public class ResumesService : BaseCrudService<Resume, ResumeCreateDto, ResumeDto, Guid>, IResumesService
{
    public ResumesService(ApplicationContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<ResumeCreateDto> CreateAsync(ResumeCreateDto entity, CancellationToken token = default)
    {
        if (!await _context.Students.AnyAsync(s => s.Id == entity.StudentId, token))
            throw new ArgumentException($"Student with id {entity.StudentId} is not found");
        if (await _context.Resumes.AnyAsync(r => r.StudentId == entity.StudentId, token))
            throw new ArgumentException("Resume already exists");

        string filesPath = entity.Path[..entity.Path.LastIndexOf("/", StringComparison.Ordinal)];
        if (!Directory.Exists(filesPath)) Directory.CreateDirectory(filesPath);
        await using (var fileStream = new FileStream(entity.Path, FileMode.Create))
        {
            await entity.File.CopyToAsync(fileStream, token);
        }
        return await base.CreateAsync(entity, token);
    }

    public async Task<ResumeDto?> GetByStudentIdAsync(Guid studentId, CancellationToken token = default)
        => await _context.Resumes.Where(r => r.StudentId == studentId)
            .ProjectTo<ResumeDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(token);

    public async Task<DownloadableResumeDto> DownloadAsync(Guid id, CancellationToken token = default)
    {
        var resume = await _context.Resumes.FindAsync(new object?[] { id }, cancellationToken: token);
        if (resume == null) throw new ArgumentException($"Resume with id {id} is not found");
        return new(){File = await resume.DownloadAsync(), Path = resume.Path};
    }

    public override async Task DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        var resume = await _context.Resumes.FindAsync(new object?[] { id }, cancellationToken: token);
        if (resume == null) throw new ArgumentException($"Resume with id {id} is not found");
        File.Delete(resume.Path);
        await base.DeleteByIdAsync(id, token);
    }
}