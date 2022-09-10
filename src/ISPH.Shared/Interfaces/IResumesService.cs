using ISPH.Domain.Models;
using ISPH.Shared.Dtos.Resumes;
using ISPH.Shared.Interfaces.Base;

namespace ISPH.Shared.Interfaces;

public interface IResumesService : ICrudService<Resume, ResumeCreateDto, ResumeDto, Guid>
{
    Task<ResumeDto?> GetByStudentIdAsync(Guid studentId, CancellationToken token = default);
    Task<DownloadableResumeDto> DownloadAsync(Guid id, CancellationToken token = default);
}