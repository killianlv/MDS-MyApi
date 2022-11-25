using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApi.Entity;
using MyApi.Repository;

namespace MyApi.Services.ProductService
{
    internal class ProductService : IProductService
    {

        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<Product?> AddProduct(Product product)
        {
            var entity = new ProductEntity { Name = product.Name, Stock = product.Stock };
            var id = await _repository.Create(entity);
            return await GetSigleProduct(id);
        }
        
        public async Task<bool> DeleteProduct(int id)
        {
            await _repository.Delete(id);
            var entity = GetSigleProduct(id);
            if (entity == null)
                return false;
            return true;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _repository.Get();
            var model = new List<Product>();
            foreach(ProductEntity element in products)
            {
                var temp = new Product { Id = element.Id, Name = element.Name, Stock = element.Stock };
                model.Add(temp);
            }
            return model;
        }
        
        public async Task<Product?> GetSigleProduct(int id)
        {
            var entity = await _repository.Get(id);
            if (entity == null) return null;
            var model = new Product { Id = entity.Id, Name = entity.Name, Stock = entity.Stock };
            return model;
        }
        
        public async Task<Product?> UpdateProduct(int id, Product requets)
        {

            if (requets == null) return null;
            var current = await _repository.Get(id);
            if (current == null) return null;
            var updated = new ProductEntity
            {
                Id = id,
                Name = (requets.Name == null ? current.Name : requets.Name),
                Stock = (requets.Stock == null ? current.Stock : requets.Stock),
            };
            await _repository.Update(updated);
            current = await _repository.Get(id);
            if(current == null) return null;
            var model = new Product { Id = current.Id, Name = current.Name, Stock = current.Stock };
            return model;
        }
    }
}
