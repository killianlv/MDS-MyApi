using Microsoft.EntityFrameworkCore;
using MyApi.Entity;
using System.Linq.Expressions;

namespace MyApi.Repository
{
    public class EntityRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected readonly DataContext DbContext;

        protected EntityRepositoryBase(DataContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<int> Create(TEntity item)
        {
            DbContext.Add(item);
            await DbContext.SaveChangesAsync();
            var id = item?.Id?.ToString();
            return int.Parse(id);
        }

        public async Task Delete(TKey id)
        {
            var entity = await FindAsync(id);
            if (entity == null) return;
            DbContext.Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task<IList<TEntity>> Get(params Expression<Func<TEntity, bool>>[] filters)
        {
            var entitySet = DbContext
                .Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (filter == null) continue;
                    entitySet = entitySet.Where(filter);
                }
            }

            var entities = await entitySet
                .ToArrayAsync();

            return entities;
        }

        public async Task<TEntity?> Get(TKey id)
        {
            var entity = await FindAsync(id);
            return entity;
        }

        public virtual async Task Update(TEntity item)
        {
            DbContext.Update(item);
            await SaveChangesAsync();
        }

        protected async Task SaveChangesAsync(bool acceptAllChanges = true)
        {
            await DbContext.SaveChangesAsync(acceptAllChanges);
        }

        protected async Task<TEntity?> FindAsync(TKey id)
        {
            var entity = await DbContext.FindAsync<TEntity>(id);
            if (entity != null)
            {
                DbContext.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }
    }
}
