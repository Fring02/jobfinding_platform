using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISPH.Data.Contexts;
using ISPH.Data.Extensions;
using ISPH.Domain.Models.Base;
using ISPH.Shared.Dtos.Interfaces;
using ISPH.Shared.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Application.Services.Base;

public abstract class BaseCrudService<TEntity, TCreate, TUpdate, TId> : ICrudService<TEntity, TCreate, TUpdate, TId>
    where TEntity : BaseEntity<TId> where TCreate : IDto<TId> where TUpdate : IDto<TId> where TId : struct
{
    protected readonly ApplicationContext _context;
    protected readonly IMapper _mapper;

    protected BaseCrudService(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public virtual async Task<TCreate> CreateAsync(TCreate entity, CancellationToken token = default)
    {
        var model = _mapper.Map<TEntity>(entity);
        _context.Set<TEntity>().Add(model);
        await _context.SaveChangesAsync(token);
        entity.Id = model.Id;
        return entity;
    }

    public virtual async Task UpdateAsync(TUpdate entity, CancellationToken token = default)
    {
        var model = await _context.Set<TEntity>().FindAsync(new object?[] { entity.Id }, cancellationToken: token);
        if (model == null) throw new ArgumentException($"Entity with id {entity.Id} is not found");
        var entry = _context.Entry(model);
        foreach (var property in typeof(TUpdate).GetProperties())
        {
            var value = property.GetValue(entity);
            if(value != null) entry.Property(property.Name).CurrentValue = value;
        }
        _context.Set<TEntity>().Update(model);
        await _context.SaveChangesAsync(token);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken token = default)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync(token);
    }

    public virtual async Task DeleteByIdAsync(TId id, CancellationToken token = default)
    {
        var entity = await _context.Set<TEntity>().FindAsync(new object?[] { id }, cancellationToken: token);
        if (entity == null)
            throw new ArgumentException($"Cannot delete by id {id}");
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync(token);
    }

    public async Task<TResult?> GetByIdAsync<TResult>(TId id, CancellationToken token = default) where TResult : class, IDto<TId> =>
        await _context.Set<TEntity>().ProjectTo<TResult>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id.Equals(id), token);

    public async Task<IReadOnlyCollection<TResult>> GetAllAsync<TResult>(int page = 0, int pageCount = 5, CancellationToken token = default)
    {
        var all = await _context.Set<TEntity>().ProjectTo<TResult>(_mapper.ConfigurationProvider)
            .Paginate(page, pageCount).ToListAsync(token);
        return new ReadOnlyCollection<TResult>(all);
    }

    public async Task<bool> HasEntityAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token = default) => 
        await _context.Set<TEntity>().AnyAsync(filter, token);
}