using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Models
{
    public class ProductVariationVM
    {
        public int ProductVariationId { get; set; }

        [Required(ErrorMessage = "El producto es requerido")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El color es requerido")]
        public int ColorId { get; set; }

        [Required(ErrorMessage = "La talla es requerida")]
        public int SizeId { get; set; }

        [Required(ErrorMessage = "El SKU es requerido")]
        [StringLength(50)]
        public string SKU { get; set; }

        [Required(ErrorMessage = "El stock es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        public string? ImageName { get; set; }

        // Opcionales para mostrar en listados
        public string? ProductName { get; set; }
        public string? ColorName { get; set; }
        public string? SizeName { get; set; }
    }
}
