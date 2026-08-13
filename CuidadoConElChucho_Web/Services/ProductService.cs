using CuidadoConElChucho_Web.Entities;
using CuidadoConElChucho_Web.Extensions;
using CuidadoConElChucho_Web.Models;
using CuidadoConElChucho_Web.Repositories;

namespace CuidadoConElChucho_Web.Services
{
    public class ProductService(
            ProductRepository _productRepository,
            CategoryRepository _categoryRepository,
            ColorRepository _colorRepository,
            SizeRepository _sizeRepository,
            ProductVariationRepository _variationRepository,
            ProductImageRepository _imageRepository,
            IImageService _imageService )
    {
        private const string ProductImageFolder = "images/products";
        private const string VariantImageFolder = "images/products/variants";

        // ---------- LISTADOS (ADMIN) ----------

        public async Task<IEnumerable<ProductVM>> GetAllAsync()
        {
            var products = await _productRepository.GetAllWithDetailsAsync();
            return products.Select(MapToSummaryVM).ToList();
        }

        public async Task<ProductVM?> GetByIdAsync(int productId)
        {
            var product = await _productRepository.GetByIdWithDetailsAsync(productId);
            if (product == null) return null;

            var vm = MapToSummaryVM(product);

            vm.GalleryImages = (await _imageRepository.GetByProductIdAsync(productId))
                .Select(i => new ProductImageVM
                {
                    ProductImageId = i.ProductImageId,
                    ProductId = i.ProductId,
                    ImageName = i.ImageName,
                    Label = i.Label,
                    IsPrimary = i.IsPrimary
                }).ToList();

            vm.Variants = product.Variations
                .GroupBy(v => v.ColorId)
                .Select(g => new ProductVariantGroupVM
                {
                    ColorId = g.Key,
                    ColorName = g.First().Color.Name,
                    ColorHex = g.First().Color.HexCode,
                    ImageName = g.Select(v => v.ImageName).FirstOrDefault(n => !string.IsNullOrEmpty(n)),
                    Sizes = g.Select(v => new ProductVariantSizeVM
                    {
                        ProductVariationId = v.ProductVariationId,
                        SizeId = v.SizeId,
                        SizeName = v.Size.Name,
                        Stock = v.Stock,
                        IsActive = v.IsActive,
                        SKU = v.SKU
                    }).OrderBy(s => s.SizeId).ToList()
                })
                .ToList();

            return vm;
        }

        // ---------- PÁGINA PÚBLICA DE DETALLE ----------

        public async Task<ProductDetailVM?> GetDetailByIdAsync(int productId)
        {
            var product = await _productRepository.GetByIdWithDetailsAsync(productId);
            if (product == null || !product.IsActive)
            {
                return null;
            }

            var galleryImages = await _imageRepository.GetByProductIdAsync(productId);

            var images = new List<string>();
            if (!string.IsNullOrEmpty(product.ImageName))
            {
                images.Add(product.ImageName);
            }
            images.AddRange(galleryImages.Select(g => g.ImageName));

            var colors = product.Variations
                .Where(v => v.IsActive)
                .GroupBy(v => v.ColorId)
                .Select(g => new ProductDetailColorVM
                {
                    ColorId = g.Key,
                    Name = g.First().Color.Name,
                    HexCode = g.First().Color.HexCode,
                    ImageName = g.Select(v => v.ImageName).FirstOrDefault(n => !string.IsNullOrEmpty(n)),
                    Sizes = g.Select(v => new ProductDetailSizeVM
                    {
                        SizeId = v.SizeId,
                        Name = v.Size.Name,
                        Stock = v.Stock,
                        SKU = v.SKU
                    }).OrderBy(s => s.SizeId).ToList()
                })
                .ToList();

            var similar = await GetSimilarProductsAsync(product.CategoryId, product.ProductId);

            return new ProductDetailVM
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                GenderName = product.Gender.GetDisplayName(),
                Price = product.Price,
                SalePrice = product.SalePrice,
                Images = images,
                Colors = colors,
                SimilarProducts = similar
            };
        }

        public async Task<List<ProductCardVM>> GetSimilarProductsAsync(int categoryId, int excludeProductId, int take = 4)
        {
            var products = await _productRepository.GetAllWithDetailsAsync();

            return products
                .Where(p => p.CategoryId == categoryId && p.ProductId != excludeProductId && p.IsActive)
                .Take(take)
                .Select(p => new ProductCardVM
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    ImageName = p.ImageName,
                    Price = p.Price,
                    SalePrice = p.SalePrice
                })
                .ToList();
        }

        // ---------- DATOS PARA EL FORMULARIO (ADMIN) ----------

        public async Task<IEnumerable<CategoryVM>> GetCategoriesForSelectAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryVM { CategoryId = c.CategoryId, Name = c.Name })
                .OrderBy(c => c.Name).ToList();
        }

        public async Task<IEnumerable<ColorVM>> GetColorsForSelectAsync()
        {
            var colors = await _colorRepository.GetAllAsync();
            return colors.Select(c => new ColorVM { ColorId = c.ColorId, Name = c.Name, HexCode = c.HexCode })
                .OrderBy(c => c.Name).ToList();
        }

        public async Task<IEnumerable<SizeVM>> GetSizesForSelectAsync()
        {
            var sizes = await _sizeRepository.GetAllAsync();
            return sizes.Select(s => new SizeVM { SizeId = s.SizeId, Name = s.Name }).ToList();
        }

        // ---------- CREAR ----------

        public async Task<bool> AddAsync(ProductVM productVM)
        {
            var exists = await _productRepository.ExistsByNameAsync(productVM.Name.Trim());
            if (exists) return false;

            var product = new Product
            {
                CategoryId = productVM.CategoryId,
                Name = productVM.Name.Trim(),
                Description = productVM.Description.Trim(),
                Price = productVM.Price,
                SalePrice = productVM.SalePrice,
                IsActive = productVM.IsActive,
                Gender = productVM.Gender,
                ImageName = productVM.ImageName
            };

            await _productRepository.AddAsync(product);

            await SaveGalleryImagesAsync(product.ProductId, productVM.GalleryImages);
            await SaveVariantsAsync(product.ProductId, productVM.Variants);

            return true;
        }

        // ---------- EDITAR ----------

        public async Task<bool> EditAsync(ProductVM productVM)
        {
            var exists = await _productRepository.ExistsByNameAsync(productVM.Name.Trim(), productVM.ProductId);
            if (exists) return false;

            var product = new Product
            {
                ProductId = productVM.ProductId,
                CategoryId = productVM.CategoryId,
                Name = productVM.Name.Trim(),
                Description = productVM.Description.Trim(),
                Price = productVM.Price,
                SalePrice = productVM.SalePrice,
                IsActive = productVM.IsActive,
                Gender = productVM.Gender,
                ImageName = productVM.ImageName
            };

            await _productRepository.AditAsync(product);

            await SyncGalleryImagesAsync(productVM.ProductId, productVM.GalleryImages);
            await SyncVariantsAsync(productVM.ProductId, productVM.Variants);

            return true;
        }

        // ---------- ELIMINAR ----------

        public async Task<string?> DeleteAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null) return null;

            await _productRepository.DeleteAsync(product);
            return product.ImageName;
        }

        // ---------- GALERÍA ----------

        private async Task SaveGalleryImagesAsync(int productId, List<ProductImageVM> gallery)
        {
            foreach (var image in gallery.Where(g => g.ImageFile != null))
            {
                var fileName = await _imageService.SaveImageAsync(image.ImageFile!, ProductImageFolder);
                await _imageRepository.AddAsync(new ProductImage
                {
                    ProductId = productId,
                    ImageName = fileName,
                    Label = image.Label,
                    IsPrimary = false
                });
            }
        }

        private async Task SyncGalleryImagesAsync(int productId, List<ProductImageVM> gallery)
        {
            var existing = await _imageRepository.GetByProductIdAsync(productId);

            var toDelete = existing
                .Where(e => gallery.Any(g => g.ProductImageId == e.ProductImageId && g.ToDelete))
                .ToList();

            foreach (var img in toDelete)
            {
                _imageService.DeleteImage(img.ImageName, ProductImageFolder);
            }

            if (toDelete.Count > 0)
            {
                await _imageRepository.RemoveRangeAsync(toDelete);
            }

            foreach (var image in gallery.Where(g => g.ImageFile != null && g.ProductImageId == 0))
            {
                var fileName = await _imageService.SaveImageAsync(image.ImageFile!, ProductImageFolder);
                await _imageRepository.AddAsync(new ProductImage
                {
                    ProductId = productId,
                    ImageName = fileName,
                    Label = image.Label,
                    IsPrimary = false
                });
            }
        }

        // ---------- VARIANTES (COLOR + TALLA) ----------

        private async Task SaveVariantsAsync(int productId, List<ProductVariantGroupVM> groups)
        {
            foreach (var group in groups)
            {
                if (!group.ColorId.HasValue) continue;
                var colorId = group.ColorId.Value;

                var activeSizes = group.Sizes.Where(s => s.IsActive).ToList();
                if (activeSizes.Count == 0) continue;

                string? imageName = null;
                if (group.ImageFile != null)
                {
                    imageName = await _imageService.SaveImageAsync(group.ImageFile, VariantImageFolder);
                }

                foreach (var size in activeSizes)
                {
                    var sku = await GenerateUniqueSkuAsync(productId, colorId, size.SizeId);

                    await _variationRepository.AddAsync(new ProductVariation
                    {
                        ProductId = productId,
                        ColorId = colorId,
                        SizeId = size.SizeId,
                        SKU = sku,
                        Stock = size.Stock,
                        ImageName = imageName,
                        IsActive = true
                    });
                }
            }
        }

        private async Task SyncVariantsAsync(int productId, List<ProductVariantGroupVM> groups)
        {
            var existingVariations = await _variationRepository.GetByProductIdAsync(productId);
            var postedActiveCombos = new HashSet<(int ColorId, int SizeId)>();

            foreach (var group in groups)
            {
                if (!group.ColorId.HasValue) continue;
                var colorId = group.ColorId.Value;

                string? groupImageName = group.ImageName;
                if (group.ImageFile != null)
                {
                    groupImageName = await _imageService.SaveImageAsync(group.ImageFile, VariantImageFolder);
                }

                foreach (var size in group.Sizes)
                {
                    if (size.IsActive)
                    {
                        postedActiveCombos.Add((colorId, size.SizeId));
                    }

                    var current = existingVariations
                        .FirstOrDefault(v => v.ColorId == colorId && v.SizeId == size.SizeId);

                    if (current == null)
                    {
                        if (!size.IsActive) continue;

                        var sku = await GenerateUniqueSkuAsync(productId, colorId, size.SizeId);
                        await _variationRepository.AddAsync(new ProductVariation
                        {
                            ProductId = productId,
                            ColorId = colorId,
                            SizeId = size.SizeId,
                            SKU = sku,
                            Stock = size.Stock,
                            ImageName = groupImageName,
                            IsActive = true
                        });
                    }
                    else
                    {
                        current.Stock = size.IsActive ? size.Stock : 0;
                        current.IsActive = size.IsActive;
                        if (groupImageName != null)
                        {
                            current.ImageName = groupImageName;
                        }
                        await _variationRepository.AditAsync(current);
                    }
                }
            }

            var orphaned = existingVariations
                .Where(v => v.IsActive && !postedActiveCombos.Contains((v.ColorId, v.SizeId)))
                .ToList();

            foreach (var variation in orphaned)
            {
                variation.IsActive = false;
                variation.Stock = 0;
                await _variationRepository.AditAsync(variation);
            }
        }

        private async Task<string> GenerateUniqueSkuAsync(int productId, int colorId, int sizeId)
        {
            var baseSku = $"P{productId}-C{colorId}-S{sizeId}";
            var sku = baseSku;
            var suffix = 1;

            while (await _variationRepository.SkuExistsAsync(sku))
            {
                sku = $"{baseSku}-{suffix++}";
            }

            return sku;
        }

        // ---------- MAPEO ----------

        private static ProductVM MapToSummaryVM(Product product)
        {
            var activeVariations = product.Variations.Where(v => v.IsActive).ToList();

            return new ProductVM
            {
                ProductId = product.ProductId,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                SalePrice = product.SalePrice,
                IsActive = product.IsActive,
                Gender = product.Gender,
                ImageName = product.ImageName,
                CategoryName = product.Category?.Name,
                TotalStock = activeVariations.Sum(v => v.Stock),
                ColorSwatches = activeVariations
                    .GroupBy(v => v.ColorId)
                    .Select(g => g.First().Color.HexCode ?? "#cccccc")
                    .Distinct()
                    .ToList(),
                SizeCount = activeVariations.Select(v => v.SizeId).Distinct().Count()
            };
        }
    }
}
