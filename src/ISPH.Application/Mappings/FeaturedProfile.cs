using AutoMapper;
using ISPH.Domain.Models.Advertisements;
using ISPH.Shared.Dtos.Advertisements.Featured;

namespace ISPH.Application.Mappings;

public class FeaturedProfile : Profile
{
    public FeaturedProfile()
    {
        CreateMap<FeaturedAdvertisement, FeaturedAdvertisementViewDto>()
            .ForMember(a => a.Title, opt => opt.MapFrom(d => d.Advertisement.Title))
            .ForMember(a => a.Salary, opt => opt.MapFrom(d => d.Advertisement.Salary));
        CreateMap<FeaturedAdvertisement, FeaturedAdvertisementItemDto>();
        CreateMap<FeaturedAdvertisementCreateDto, FeaturedAdvertisement>();
    }
}