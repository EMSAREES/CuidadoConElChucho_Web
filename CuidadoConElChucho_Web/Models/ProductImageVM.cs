using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Models
{
    public class ProductImageVM
    {
        public int ProductImageId { get; set; }
        public int ProductId { get; set; }

        public string? ImageName { get; set; }
        public IFormFile? ImageFile { get; set; }

        [StringLength(50)]
        public string? Label { get; set; }

        public bool IsPrimary { get; set; }
        public bool ToDelete { get; set; }
    }
}
