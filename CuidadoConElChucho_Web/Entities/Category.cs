using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
