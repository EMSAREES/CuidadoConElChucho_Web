using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Models
{
    public class ColorVM
    {
        public int ColorId { get; set; }

        [Required(ErrorMessage = "El nombre del color es requerido")]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(7)]
        [RegularExpression("^#([A-Fa-f0-9]{6})$", ErrorMessage = "Código hex inválido")]
        public string? HexCode { get; set; }
    }
}
