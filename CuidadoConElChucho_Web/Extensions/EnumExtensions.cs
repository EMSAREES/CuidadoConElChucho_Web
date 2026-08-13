using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Extensions
{
    public static class EnumExtensions
    {
        // Devuelve el texto del[Display(Name = "...")] de un valor de enum,
        // o el nombre del enum si no tiene ese atributo.
        public static string GetDisplayName(this Enum value)
        {
            var member = value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault();

            var attribute = member?
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return attribute?.Name ?? value.ToString();
        }
    }
}
