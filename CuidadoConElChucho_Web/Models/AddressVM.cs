using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Models
{
    public class AddressVM
    {
        public int AddressId { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "La calle es requerida")]
        [StringLength(150)]
        public string Street { get; set; }

        [Required(ErrorMessage = "El número exterior es requerido")]
        [StringLength(20)]
        public string ExteriorNumber { get; set; }

        [StringLength(20)]
        public string? InteriorNumber { get; set; }

        [Required(ErrorMessage = "La colonia es requerida")]
        [StringLength(100)]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "La ciudad es requerida")]
        [StringLength(100)]
        public string City { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        [StringLength(100)]
        public string State { get; set; }

        [Required(ErrorMessage = "El código postal es requerido")]
        [StringLength(10)]
        public string PostalCode { get; set; }
    }
}
