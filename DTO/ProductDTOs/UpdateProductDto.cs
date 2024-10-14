using furniro_server_hari.Models;

namespace furniro_server_hari.DTO.ProductDTOs
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Reviews { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? Label { get; set; }
        public string? Discount { get; set; }
        public List<string> Tags { get; set; } = [];
        public List<string> Sizes { get; set; } = [];
        public List<Color> Colors { get; set; } = [];
        public List<Image> ThumbNailImages { get; set; } = [];
        public List<Image> DescriptionImages { get; set; } = [];
        public ShareLink ShareLinks { get; set; } = new ShareLink();

    }
}