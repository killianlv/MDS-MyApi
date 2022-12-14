namespace MyApi.Services.ProductService
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetSigleProduct(int id);
        Task<Product?> AddProduct(Product product);
        Task<Product?> UpdateProduct(int id, Product requets);
        Task<Product?> AddProductStock(int id, int stock);
        Task<bool> DeleteProduct(int id);
    }
}
