using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using furniro_server_hari.Models;
using furniro_server_hari.Services;
// using furniro_server_hari.DTO.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using furniro_server_hari.DTO.ResponseDTO;
using furniro_server_hari.DTO.ProductDTOs;

namespace furniro_server_hari.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAllProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(JsonConvert.SerializeObject(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById([FromRoute] string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product is null)
                return NotFound("Product not found");
            return Ok(JsonConvert.SerializeObject(product));
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductDto productDto)
        {
            var createdProduct = await _productService.CreateProductAsync(productDto);
            if (createdProduct == null)
                return BadRequest("Failed to create product");
            return CreatedAtAction(
                        nameof(GetProductById),
                        new { id = createdProduct.ProductId },
                        JsonConvert.SerializeObject(createdProduct)
                    );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] UpdateProductDto productDto)
        {
            if (!await _productService.DoesProductExist(id))
                return NotFound("Product not found!!");

            var updatedProduct = await _productService.UpdateProductAsync(productDto, id);
            if (updatedProduct == null)
                return BadRequest("Failed to update the product");

            var successResponse = new MutationResponseDTO("Product updated successfully");
            return Ok(successResponse);
            // return Ok("Product updated successfully");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            // var user = HttpContext.User;
            // if (!user.Identity.IsAuthenticated)
            // {
            //     var errorResponse = new ErrorResponseDTO("Unauthorized access. Please log in.", StatusCodes.Status401Unauthorized);
            //     return Unauthorized(errorResponse);
            // }

            var result = await _productService.DeleteProductAsync(id);
            if (!result)
                return NotFound(new ErrorResponseDTO("Product not found!", StatusCodes.Status404NotFound));

            var successResponse = new MutationResponseDTO("Product deleted successfully");
            return Ok(successResponse);
        }

        [HttpGet("{id}/related")]
        public async Task<IActionResult> GetRelatedProducts([FromRoute] string id)
        {
            var relatedProducts = await _productService.GetRelatedProducts(id);
            if (relatedProducts == null || relatedProducts.Count == 0)
                return Ok();

            return Ok(JsonConvert.SerializeObject(relatedProducts));
        }
    }
}