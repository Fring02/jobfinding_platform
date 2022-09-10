using System.Linq.Expressions;
using ISPH.Domain.Models.Base;

namespace ISPH.Shared.Interfaces.Base;

public interface ICrudService<TEntity, TCreate, in TUpdate, TId> where TEntity : BaseEntity<TId>
where TCreate : IDto<TId> where TUpdate : IDto<TId> where TId : struct
{
    Task<TCreate> CreateAsync(TCreate entity, CancellationToken token = default);
    Task UpdateAsync(TUpdate entity, CancellationToken token = default);
    Task DeleteAsync(TEntity entity, CancellationToken token = default);
    Task DeleteByIdAsync(TId id, CancellationToken token = default);
    Task<TResult?> GetByIdAsync<TResult>(TId id, CancellationToken token = default) where TResult : class, IDto<TId>;
    Task<IReadOnlyCollection<TResult>> GetAllAsync<TResult>(int page = 0, int pageCount = 5, CancellationToken token = default);

    Task<bool> HasEntityAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token = default);
}