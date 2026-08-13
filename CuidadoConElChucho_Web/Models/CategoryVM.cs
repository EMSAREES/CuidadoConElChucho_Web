using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Models
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public int ProductCount { get; set; } 
    }
}
