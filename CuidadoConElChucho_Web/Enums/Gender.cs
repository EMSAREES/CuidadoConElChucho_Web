using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Enums
{
    public enum Gender
    {
        [Display(Name = "Hombre")]
        Men,

        [Display(Name = "Mujer")]
        Women,

        [Display(Name = "Unisex")]
        Unisex,

        [Display(Name = "Niños")]
        Kids
    }
}
