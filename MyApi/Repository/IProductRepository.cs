using MyApi.Entity;
using System.Linq.Expressions;

namespace MyApi.Repository
{
    internal interface IProductRepository
    {
        Task<IList<ProductEntity>> Get(params Expression<Func<ProductEntity, bool>>[] filters);

        Task<ProductEntity?> Get(int id);

        Task<int> Create(ProductEntity item);

        Task Delete(int id);

        Task Update(ProductEntity item);
    }
}
