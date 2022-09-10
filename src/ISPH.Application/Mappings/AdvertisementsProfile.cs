using System;
using AutoMapper;
using ISPH.Domain.Models.Advertisements;
using ISPH.Shared.Dtos.Advertisements;

namespace ISPH.Application.Mappings;

public class AdvertisementsProfile : Profile
{
    public AdvertisementsProfile()
    {
        CreateMap<Advertisement, AdvertisementViewDto>()
            .ForMember(a => a.PostedAt, opt => opt.MapFrom(a => a.PostedAt.ToShortDateString()))
            .ForMember(a => a.EmploymentType, opt => opt.MapFrom(a => a.Employment));
        CreateMap<Advertisement, AdvertisementItemDto>()
            .ForMember(a => a.EmploymentType, opt => opt.MapFrom(a => a.Employment))
            .ForMember(a => a.CompanyName, opt => opt.MapFrom(a => a.Employer.Company.Name))
            .ForMember(a => a.PositionName, opt => opt.MapFrom(a => a.Position.Name))
            .ForMember(a => a.PostedAt, opt => opt.MapFrom(a => a.PostedAt.ToShortDateString()));
        CreateMap<AdvertisementCreateDto, Advertisement>()
            .ForMember(a => a.WorkTimeType, opt => opt.MapFrom(a => Enum.Parse<WorkTime>(a.WorkTime)))
            .ForMember(a => a.EmploymentType, opt => opt.MapFrom(a => Enum.Parse<EmploymentType>(a.EmploymentType)));
        CreateMap<AdvertisementUpdateDto, Advertisement>()
            .ForMember(a => a.WorkTimeType, opt =>
            {
                opt.PreCondition(src => !string.IsNullOrEmpty(src.WorkTime));
                opt.MapFrom(a => Enum.Parse<WorkTime>(a.WorkTime!));
            })
            .ForMember(a => a.EmploymentType, opt =>
            {
                opt.PreCondition(src => !string.IsNullOrEmpty(src.EmploymentType));
                opt.MapFrom(a => Enum.Parse<EmploymentType>(a.EmploymentType!));
            });
    }
}