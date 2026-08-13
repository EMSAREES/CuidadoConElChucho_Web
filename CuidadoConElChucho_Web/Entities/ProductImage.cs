namespace CuidadoConElChucho_Web.Entities
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }

        public int ProductId { get; set; }

        public string ImageName { get; set; } = null!;

        public bool IsPrimary { get; set; }

        public string? Label { get; set; }

        public Product Product { get; set; } = null!;
    }
}
