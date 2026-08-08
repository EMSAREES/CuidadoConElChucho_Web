using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Entities
{
    public class Size
    {
        public int SizeId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public ICollection<ProductVariation> ProductVariations { get; set; }
            = new List<ProductVariation>();
    }
}
