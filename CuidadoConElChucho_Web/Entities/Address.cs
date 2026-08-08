using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Entities
{
    public class Address
    {
        public int AddressId { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Street { get; set; } = null!;

        [Required]
        public string ExteriorNumber { get; set; } = null!;

        public string? InteriorNumber { get; set; }

        [Required]
        public string Neighborhood { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string State { get; set; } = null!;

        [Required]
        public string PostalCode { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
