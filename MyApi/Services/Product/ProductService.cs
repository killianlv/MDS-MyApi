using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApi.Entity;
using MyApi.Repository;

namespace MyApi.Services.ProductService
{
    internal class ProductService : IProductService
    {

        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<Product?> AddProduct(Product product)
        {
            var entity = _mapper.Map<Product, ProductEntity>(product);
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
                var temp = _mapper.Map<ProductEntity, Product>(element);
                model.Add(temp);
            }

            return model;
        }
        
        public async Task<Product?> GetSigleProduct(int id)
        {
            var entity = await _repository.Get(id);
            if (entity == null) return null;
            var model = _mapper.Map<ProductEntity, Product>(entity);
            return model;
        }
        
        public async Task<Product?> UpdateProduct(int id, Product requets)
        {

            if (requets == null) return null;
            var current = await _repository.Get(id);
            if (current == null) return null;
            var updated = _mapper.Map(requets, current);
            await _repository.Update(updated);
            current = await _repository.Get(id);
            if(current == null) return null;
            var model = _mapper.Map<ProductEntity, Product>(current);
            return model;
        }
    }
}
