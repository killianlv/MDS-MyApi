using MyApi.Entity;

namespace MyApi.Repository
{
    public class ProductRepository : EntityRepositoryBase<ProductEntity, int>, IProductRepository
    {
        public ProductRepository(DataContext dbContext) : base(dbContext)
        {
        }
    }
}
