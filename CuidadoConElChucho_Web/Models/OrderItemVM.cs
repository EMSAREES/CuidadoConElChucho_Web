using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Models
{
    public class OrderItemVM
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        [Required(ErrorMessage = "La variación de producto es requerida")]
        public int ProductVariationId { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Price { get; set; }

        // Opcionales para mostrar en el detalle del pedido
        public string? ProductName { get; set; }
        public string? ColorName { get; set; }
        public string? SizeName { get; set; }
    }
}
