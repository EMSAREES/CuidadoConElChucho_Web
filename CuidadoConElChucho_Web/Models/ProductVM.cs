using CuidadoConElChucho_Web.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CuidadoConElChucho_Web.Models
{
    public class ProductVM
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "La categoría es requerida")]
        [Display(Name = "Categoría")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(150)]
        [Display(Name = "Nombre del producto")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        [Display(Name = "Precio")]
        public decimal Price { get; set; }

        [Display(Name = "Producto activo")]
        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "El género es requerido")]
        [Display(Name = "Género")]
        public Gender Gender { get; set; }

        public string? ImageName { get; set; }

        [Display(Name = "Imagen de portada")]
        public IFormFile? ImageFile { get; set; }

        public string? CategoryName { get; set; }

        // Solo lectura, calculados para el listado (Index)
        public int TotalStock { get; set; }
        public List<string> ColorSwatches { get; set; } = new();
        public int SizeCount { get; set; }

        // Fotos adicionales (frente, espalda, perfiles...)
        public List<ProductImageVM> GalleryImages { get; set; } = new();

        // Variantes: un bloque por color, con sus tallas y stock
        public List<ProductVariantGroupVM> Variants { get; set; } = new();
    }
}
