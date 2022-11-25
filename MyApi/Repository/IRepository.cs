using MyApi.Entity;
using System.Linq.Expressions;

namespace MyApi.Repository
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        Task<IList<TEntity>> Get(params Expression<Func<TEntity, bool>>[] filters);

        Task<TEntity?> Get(TKey id);

        Task<int> Create(TEntity item);

        Task Delete(TKey id);

        Task Update(TEntity item);
    }
}
