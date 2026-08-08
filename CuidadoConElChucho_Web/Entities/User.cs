using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CuidadoConElChucho_Web.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Type { get; set; } = null!;

        public ICollection<Order> Orders { get; set; }
            = new List<Order>();

        public ICollection<Address> Addresses { get; set; }
            = new List<Address>();
    }
}
