using AutoMapper;
using ISPH.Domain.Models;
using ISPH.Shared.Dtos.Resumes;

namespace ISPH.Application.Mappings;
public class ResumesProfile : Profile
{
    public ResumesProfile()
    {
        CreateMap<Resume, ResumeDto>().ReverseMap();
        CreateMap<ResumeCreateDto, Resume>();
    }
}