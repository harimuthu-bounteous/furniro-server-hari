using Supabase;
using furniro_server_hari.Models;
using furniro_server_hari.Interfaces;
using Supabase.Postgrest.Exceptions;
// using furniro_server_hari.DTO.ProductDTOs;

namespace furniro_server_hari.Repository
{
  public class ProductRepository : IProductRepository
  {
    private readonly Client _client;

    public ProductRepository(Client client)
    {
      _client = client;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
      var products = await _client.From<Product>().Get();
      return products.Models;
    }

    public async Task<Product?> GetProductByIdAsync(string id)
    {
      var response = await _client.From<Product>().Where(x => x.ProductId == id).Single();
      return response;
    }

    public async Task<bool> DoesProductExist(string id)
    {
      var product = await _client.From<Product>().Where(p => p.ProductId == id).Single();
      return product != null;
    }

    public async Task<Product?> CreateProductAsync(Product product)
    {
      try
      {
        var response = await _client.From<Product>().Insert(product);
        return response.Model;
      }
      catch (PostgrestException ex)
      {
        Console.WriteLine($"PostgrestException: {ex.Message}"); // This will give more details about the error
        return null;
      }

    }

    public async Task<Product?> UpdateProductAsync(string id, Product product)
    {
      var response = await _client.From<Product>().Where(x => x.ProductId == id).Update(product);
      return response.Model;
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
      try
      {
        await _client.From<Product>().Where(x => x.ProductId == id).Delete();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public async Task<List<Product>> GetRelatedProducts(string productId)
    {
      var currentProduct = await _client.From<Product>().Where(p => p.ProductId == productId).Single();
      if (currentProduct == null) return [];

      var relatedProducts = await _client.From<Product>()
          .Where(p => p.Category == currentProduct.Category && p.ProductId != currentProduct.ProductId)
          .Limit(10)
          .Get();

      return relatedProducts.Models;
    }

  }
}
