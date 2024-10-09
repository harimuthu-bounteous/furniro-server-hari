using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace furniro_server_hari.Models
{
    [Table("products")]
    public class Product : BaseModel
    {
        [PrimaryKey("product_id", false)]
        public string? ProductId { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("price")]
        public decimal Price { get; set; }

        [Column("reviews")]
        public int Reviews { get; set; }

        [Column("rating")]
        public double Rating { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("sku")]
        public string SKU { get; set; } = string.Empty;

        [Column("category")]
        public string Category { get; set; } = string.Empty;

        [Column("label")]
        public string? Label { get; set; }

        [Column("discount")]
        public string? Discount { get; set; }

        [Column("tags")]
        public List<string> Tags { get; set; } = [];

        [Column("sizes")]
        public List<string> Sizes { get; set; } = [];

        [Column("colors")]
        public List<Color> Colors { get; set; } = [];

        [Column("thumbnail_images")]
        public List<Image> ThumbNailImages { get; set; } = [];

        [Column("description_images")]
        public List<Image> DescriptionImages { get; set; } = [];

        [Column("share_links")]
        public ShareLink ShareLinks { get; set; } = new ShareLink();
    }

    public class Color
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class Image
    {
        public string Alt { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }

    public class ShareLink
    {
        public string Facebook { get; set; } = string.Empty;
        public string Linkedin { get; set; } = string.Empty;
        public string Twitter { get; set; } = string.Empty;
    }
}