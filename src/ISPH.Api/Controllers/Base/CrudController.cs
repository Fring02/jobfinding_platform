using ISPH.Domain.Configuration;
using ISPH.Domain.Models.Base;
using ISPH.Shared.Dtos.Interfaces;
using ISPH.Shared.Interfaces.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.Api.Controllers.Base;

[ApiController]
public abstract class CrudController<TEntity, TId, TCreateDto, TUpdateDto, TViewDto, TItemDto, TService> : ControllerBase
where TEntity : BaseEntity<TId>
where TService : ICrudService<TEntity, TCreateDto, TUpdateDto, TId>
where TCreateDto : IDto<TId> where TUpdateDto : IDto<TId>
where TViewDto : class, IDto<TId>
where TItemDto : class, IDto<TId>
where TId : struct
{
    protected readonly TService Service;
    protected CrudController(TService service) => Service = service;

    [HttpDelete("{id}"), Authorize(Roles = Roles.Admin)]
    public virtual async Task<IActionResult> DeleteAsync(TId id, CancellationToken token)
    {
        await Service.DeleteByIdAsync(id, token);
        return Ok("Deleted");
    }
    
    [HttpGet("{id}"), Authorize]
    public virtual async Task<TViewDto?> GetByIdAsync(TId id, CancellationToken token) => 
        await Service.GetByIdAsync<TViewDto>(id, token);

    [HttpGet]
    public virtual async Task<IActionResult> GetAllAsync(CancellationToken token, int page = 0)
    {
        var data = await Service.GetAllAsync<TItemDto>(page, token: token);
        return Ok(new{data, count = data.Count});
    }

    [HttpPost]
    public virtual async Task<IActionResult> CreateAsync([FromBody] TCreateDto createdEntity, CancellationToken token) => 
        Created(Request.Path, await Service.CreateAsync(createdEntity, token));

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> UpdateAsync(TId id, [FromBody] TUpdateDto dto, CancellationToken token)
    {
        dto.Id = id;
        await Service.UpdateAsync(dto, token);
        return Ok("Updated");
    }
}