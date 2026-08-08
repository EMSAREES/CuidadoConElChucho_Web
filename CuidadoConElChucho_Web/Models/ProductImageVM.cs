using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Models
{
    public class ProductImageVM
    {
        public int ProductImageId { get; set; }

        [Required(ErrorMessage = "El producto es requerido")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "La imagen es requerida")]
        [StringLength(255)]
        public string ImageName { get; set; }

        public bool IsPrimary { get; set; }
    }
}
