using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Entities
{
    public class Color
    {
        public int ColorId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? HexCode { get; set; }

        public ICollection<ProductVariation> ProductVariations { get; set; }
            = new List<ProductVariation>();
    }
}
