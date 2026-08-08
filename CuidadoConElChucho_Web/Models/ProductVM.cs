using CuidadoConElChucho_Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Models
{
    public class ProductVM
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "La categoría es requerida")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(150)]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "El género es requerido")]
        public Gender Gender { get; set; }

        public string? ImageName { get; set; }

        // Para mostrar en listados/detalle, opcional
        public string? CategoryName { get; set; }
    }
}
