using System;
using AutoMapper;
using ISPH.Domain.Models.Users;
using ISPH.Shared.Dtos.Users;
using ISPH.Shared.Dtos.Users.Base;

namespace ISPH.Application.Mappings;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<StudentCreateDto, Student>();
        CreateMap<Student, StudentViewDto>();
        CreateMap<Student, StudentItemDto>();
        CreateMap<UserUpdateDto<Guid>, Student>();

        CreateMap<EmployerCreateDto, Employer>();
        CreateMap<Employer, AdvertisementEmployerDto>();
        CreateMap<Employer, EmployerViewDto>().ForMember(e => e.PostedAdvertisements, opt => opt.MapFrom(e => e.Advertisements));
        CreateMap<Employer, EmployerItemDto>();
        CreateMap<UserUpdateDto<Guid>, Employer>();
    }
}