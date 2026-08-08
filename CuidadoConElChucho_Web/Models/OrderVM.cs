using CuidadoConElChucho_Web.Entities;
using CuidadoConElChucho_Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace CuidadoConElChucho_Web.Models
{
    public class OrderVM
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "El usuario es requerido")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Range(0, double.MaxValue, ErrorMessage = "El total no puede ser negativo")]
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        // Para mostrar en listados
        public string? UserFullName { get; set; }
        public List<OrderItemVM> Items { get; set; } = new List<OrderItemVM>();
    }
}
