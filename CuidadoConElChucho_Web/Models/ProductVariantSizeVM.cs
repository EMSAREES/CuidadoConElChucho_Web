using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Models
{
    // Una talla concreta dentro de un color: su stock y si está disponible
    public class ProductVariantSizeVM
    {
        public int ProductVariationId { get; set; }

        [Required]
        public int SizeId { get; set; }

        public string? SizeName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        public bool IsActive { get; set; }

        public string? SKU { get; set; }
    }
}
