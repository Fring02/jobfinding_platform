using AutoMapper;
using ISPH.Domain.Models;
using ISPH.Shared.Dtos.Positions;

namespace ISPH.Application.Mappings;

public class PositionsProfile : Profile
{
    public PositionsProfile()
    {
        CreateMap<Position, PositionItemDto>();
        CreateMap<Position, PositionViewDto>();
        CreateMap<PositionCreateDto, Position>();
        CreateMap<PositionUpdateDto, Position>().ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));
    }
}