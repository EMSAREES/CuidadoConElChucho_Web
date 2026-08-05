using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string? ImageName { get; set; }

        public Category? Category { get; set; }

        public ICollection<ProductVariation> Variations { get; set; } = new List<ProductVariation>();

    }
}
