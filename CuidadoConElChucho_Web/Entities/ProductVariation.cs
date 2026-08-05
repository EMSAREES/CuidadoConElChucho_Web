using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Entities
{
    public class ProductVariation
    {
        public int ProductVariationId { get; set; }

        [Required]
        public string Color { get; set; }   // Ej. "Rojo", "Azul"

        [Required]
        public string Size { get; set; }    // Ej. "S", "M", "L", "XL"

        public int Stock { get; set; }      // Stock específico para esa talla/color
        public string? ImageName { get; set; }

        // Relación con producto
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
