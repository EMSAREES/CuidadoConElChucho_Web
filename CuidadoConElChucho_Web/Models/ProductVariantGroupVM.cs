namespace CuidadoConElChucho_Web.Models
{
    // Un bloque de "color": su imagen representativa y las tallas disponibles en ese color
    public class ProductVariantGroupVM
    {
        public int? ColorId { get; set; }

        public string? ColorName { get; set; }
        public string? ColorHex { get; set; }

        public IFormFile? ImageFile { get; set; }
        public string? ImageName { get; set; }

        public List<ProductVariantSizeVM> Sizes { get; set; } = new();
    }
}
