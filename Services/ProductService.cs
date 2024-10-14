using AutoMapper;
using furniro_server_hari.DTO.ProductDTOs;
using furniro_server_hari.Interfaces;
using furniro_server_hari.Models;

namespace furniro_server_hari.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return productDtos;
        }

        public async Task<ProductDto?> GetProductByIdAsync(string id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return null;
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DoesProductExist(string id)
        {
            try
            {
                return await _productRepository.DoesProductExist(id);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Message : " + e.Message);
                return false;
            }
        }

        public async Task<ProductDto?> CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = await _productRepository.CreateProductAsync(product);
            if (createdProduct == null)
                return null;
            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task<Product?> UpdateProductAsync(UpdateProductDto productDto, string id)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                product.ProductId = id;

                var updatedProduct = await _productRepository.UpdateProductAsync(id, product);
                if (updatedProduct == null)
                    return null;
                return updatedProduct;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Message : " + e.Message);
                return null;
            }
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }

        public async Task<List<ProductDto>> GetRelatedProducts(string productId)
        {
            var relatedProducts = await _productRepository.GetRelatedProducts(productId);
            return _mapper.Map<List<ProductDto>>(relatedProducts);
        }
    }
}