using Supabase;
using furniro_server_hari.Models;

namespace furniro_server_hari.Services
{
    public class SupabaseService
    {
        private readonly Client _client;

        public SupabaseService(string supabaseUrl, string supabaseKey)
        {
            _client = new Client(supabaseUrl, supabaseKey);
            _client.InitializeAsync().Wait();
        }

        public Client GetClient()
        {
            return _client;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await GetClient().From<Product>().Get();
            return products.Models;
        }

        public async Task<Product?> GetProductByIdAsync(string id)
        {
            var response = await GetClient().From<Product>().Where(x => x.ProductId == id).Single();
            return response;
        }

        public async Task<Product?> CreateProductAsync(Product product)
        {
            var response = await GetClient().From<Product>().Insert(product);
            return response.Model;
        }


        public async Task<Product?> UpdateProductAsync(Product product)
        {
            var response = await _client.From<Product>().Where(x => x.ProductId == product.ProductId).Update(product);
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
                throw;
            }
        }

        public async Task<List<Product>> GetRelatedProducts(string productId)
        {
            // Fetch the current product based on the productId
            var currentProduct = await _client
                .From<Product>()
                .Where(p => p.ProductId == productId)
                .Single();

            if (currentProduct == null) return [];

            // Fetch related products based on category and tags
            var relatedProducts = await _client
                .From<Product>()
                .Where(p => p.Category == currentProduct.Category && p.ProductId != currentProduct.ProductId)
                .Limit(10) // Limiting the number of related products
                .Get();

            return relatedProducts.Models;
        }

    }
}