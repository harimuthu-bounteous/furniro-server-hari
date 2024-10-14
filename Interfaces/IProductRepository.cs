
using furniro_server_hari.Models;

namespace furniro_server_hari.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(string id);
        Task<bool> DoesProductExist(string id);
        Task<Product?> CreateProductAsync(Product product);
        Task<Product?> UpdateProductAsync(string id, Product product);
        Task<bool> DeleteProductAsync(string id);
        Task<List<Product>> GetRelatedProducts(string productId);
    }
}