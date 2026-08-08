using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace CuidadoConElChucho_Web.Entities
{
    public class ProductVariation
    {
        public int ProductVariationId { get; set; }

        public int ProductId { get; set; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }

        [Required]
        public string SKU { get; set; } = null!;

        public int Stock { get; set; }

        public string? ImageName { get; set; }

        public Product Product { get; set; } = null!;

        public Color Color { get; set; } = null!;

        public Size Size { get; set; } = null!;
    }
}
