using AutoMapper;
using ISPH.Domain.Models.Advertisements;
using ISPH.Domain.Models.Users;
using ISPH.Shared.Dtos.Advertisements.Responses;

namespace ISPH.Application.Mappings;

public class AdvertisementResponsesProfile : Profile
{
    public AdvertisementResponsesProfile()
    {
        CreateMap<AdvertisementResponse, AdvertisementResponseDto>()
            .ForMember(r => r.EmploymentType, opt => opt.MapFrom(r => r.Advertisement.Employment))
            .ForMember(r => r.PositionName, opt => opt.MapFrom(r => r.Advertisement.Position.Name))
            .ForMember(r => r.Title, opt => opt.MapFrom(r => r.Advertisement.Title))
            .ForMember(r => r.WorkTime, opt => opt.MapFrom(r => r.Advertisement.WorkTime))
            .ForMember(r => r.LeftAt, opt => opt.MapFrom(r => r.LeftAt.ToString("dd MMMM yyyy")));

        CreateMap<Student, AdvertisementResponseStudentDto>()
            .ForMember(s => s.FullName, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName} {s.Patronymic}"));
    }
}