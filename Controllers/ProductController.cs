using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using furniro_server_hari.Models;
using furniro_server_hari.Services;
using furniro_server_hari.DTO;

namespace furniro_server_hari.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly SupabaseService _supabaseService;

        public ProductsController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _supabaseService.GetProductsAsync();
            return Ok(JsonConvert.SerializeObject(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _supabaseService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound("Product not found");
            return Ok(JsonConvert.SerializeObject(product));
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductDto productDto)
        {
            try
            {
                var product = new Product
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Reviews = productDto.Reviews,
                    Rating = productDto.Rating,
                    Description = productDto.Description,
                    SKU = productDto.SKU,
                    Category = productDto.Category,
                    Label = productDto.Label,
                    Discount = productDto.Discount,
                    Tags = productDto.Tags,
                    Sizes = productDto.Sizes,
                    Colors = productDto.Colors,
                    ThumbNailImages = productDto.ThumbNailImages,
                    DescriptionImages = productDto.DescriptionImages,
                    ShareLinks = productDto.ShareLinks
                };

                var createdProduct = await _supabaseService.CreateProductAsync(product);

                if (createdProduct == null)
                {
                    return BadRequest("Failed to create product");
                }
                // return Ok("Product created successfully");
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, JsonConvert.SerializeObject(createdProduct));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                return StatusCode(200, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, Product product)
        {
            if (id != product.SKU) return BadRequest();

            var updatedProduct = await _supabaseService.UpdateProductAsync(product);
            if (updatedProduct == null) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var result = await _supabaseService.DeleteProductAsync(id);
            if (!result)
                return NotFound();
            return Ok("Product deleted successfully");
        }

        [HttpGet("{id}/related")]
        public async Task<IActionResult> GetRelatedProducts(string id)
        {
            var relatedProducts = await _supabaseService.GetRelatedProducts(id);

            if (relatedProducts == null || relatedProducts.Count == 0)
                // "No related products found."
                return Ok();
            return Ok(JsonConvert.SerializeObject(relatedProducts));
        }
    }
}
