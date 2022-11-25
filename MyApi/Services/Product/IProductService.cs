namespace MyApi.Services.ProductService
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetSigleProduct(int id);
        Task<List<Product>> AddProduct(Product product);
        Task<List<Product>?> UpdateProduct(int id, Product requets);
        Task<List<Product>?> DeleteProduct(int id);
    }
}
