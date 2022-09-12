using ISPH.Api.Controllers.Base;
using ISPH.Domain.Models;
using ISPH.Shared.Dtos.Positions;
using ISPH.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.Api.Controllers;

[Route("api/v1/[controller]")]
public class PositionsController : CrudController<Position, Guid, PositionCreateDto, PositionUpdateDto, PositionViewDto, 
    PositionItemDto, IPositionsService>
{
    public PositionsController(IPositionsService service) : base(service)
    {
    }
}