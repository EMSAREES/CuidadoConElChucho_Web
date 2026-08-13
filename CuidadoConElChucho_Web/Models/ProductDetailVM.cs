namespace CuidadoConElChucho_Web.Models
{
    public class ProductDetailVM
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string GenderName { get; set; } = null!;

        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }

        public bool HasDiscount => SalePrice.HasValue && SalePrice.Value < Price;

        public decimal DisplayPrice => HasDiscount ? SalePrice!.Value : Price;

        public int DiscountPercentage => HasDiscount
            ? (int)Math.Round((1 - (SalePrice!.Value / Price)) * 100)
            : 0;

        public List<string> Images { get; set; } = new();
        public List<ProductDetailColorVM> Colors { get; set; } = new();
        public List<ProductCardVM> SimilarProducts { get; set; } = new();
    }

    public class ProductDetailColorVM
    {
        public int ColorId { get; set; }
        public string Name { get; set; } = null!;
        public string? HexCode { get; set; }
        public string? ImageName { get; set; }
        public List<ProductDetailSizeVM> Sizes { get; set; } = new();
    }

    public class ProductDetailSizeVM
    {
        public int SizeId { get; set; }
        public string Name { get; set; } = null!;
        public int Stock { get; set; }
        public string SKU { get; set; } = null!;
        public bool InStock => Stock > 0;
    }

    public class ProductCardVM
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageName { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }

        public bool HasDiscount => SalePrice.HasValue && SalePrice.Value < Price;

        public decimal DisplayPrice => HasDiscount ? SalePrice!.Value : Price;

        public int DiscountPercentage => HasDiscount
            ? (int)Math.Round((1 - (SalePrice!.Value / Price)) * 100)
            : 0;
    }
}
