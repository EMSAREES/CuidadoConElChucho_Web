using CuidadoConElChucho_Web.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CuidadoConElChucho_Web.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        public Gender Gender { get; set; }

        public string? ImageName { get; set; }

        public Category Category { get; set; } = null!;

        public ICollection<ProductVariation> Variations { get; set; }
            = new List<ProductVariation>();

    }
}
