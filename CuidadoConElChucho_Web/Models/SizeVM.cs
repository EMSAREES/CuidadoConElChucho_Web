using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Models
{
    public class SizeVM
    {
        public int SizeId { get; set; }

        [Required(ErrorMessage = "El nombre de la talla es requerido")]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
