using Microsoft.AspNetCore.Mvc;
using CuidadoConElChucho_Web.Models;
using CuidadoConElChucho_Web.Services;

namespace CuidadoConElChucho_Web.Controllers
{
    public class ProductController(ProductService _productService, IImageService _imageService) : Controller
    {
        private const string ImageFolder = "images/products";

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id)
        {
            await LoadFormDataAsync();

            if (id == null || id == 0)
            {
                return View(new ProductVM());
            }

            var productVM = await _productService.GetByIdAsync(id.Value);
            return View(productVM ?? new ProductVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(ProductVM entityVM)
        {
            ViewBag.Message = null;
            entityVM.Variants ??= new();

            // Ya NO se descarta en silencio: si un bloque tiene color sin tallas,
            // o tallas sin color, se avisa y se regresa al formulario con lo que el
            // usuario ya llenó (para que no pierda su trabajo).
            var incompleteBlocks = entityVM.Variants
                .Where(v =>
                    (v.ColorId.HasValue && v.ColorId.Value > 0 && !v.Sizes.Any(s => s.IsActive)) ||
                    ((!v.ColorId.HasValue || v.ColorId.Value == 0) && v.Sizes.Any(s => s.IsActive)))
                .ToList();

            if (incompleteBlocks.Count > 0)
            {
                ModelState.AddModelError(string.Empty,
                    "Revisa los bloques de color: cada uno necesita un color seleccionado y al menos una talla marcada.");
            }

            // Los bloques totalmente vacíos (sin color y sin tallas) sí se descartan,
            // porque no representan ninguna intención del usuario.
            entityVM.Variants = entityVM.Variants
                .Where(v => v.ColorId.HasValue && v.ColorId.Value > 0 && v.Sizes.Any(s => s.IsActive))
                .ToList();

            if (!ModelState.IsValid)
            {
                await LoadFormDataAsync();
                return View(entityVM);
            }

            if (entityVM.SalePrice.HasValue && entityVM.SalePrice.Value >= entityVM.Price)
            {
                ModelState.AddModelError(nameof(entityVM.SalePrice),
                    "El precio con descuento debe ser menor al precio regular.");
            }

            if (entityVM.ImageFile != null && entityVM.ImageFile.Length > 0)
            {
                entityVM.ImageName = await _imageService.SaveImageAsync(entityVM.ImageFile, ImageFolder);
            }

            bool result = entityVM.ProductId == 0
                ? await _productService.AddAsync(entityVM)
                : await _productService.EditAsync(entityVM);

            await LoadFormDataAsync();

            if (!result)
            {
                ViewBag.Message = "Ya existe un producto con ese nombre.";
                return View(entityVM);
            }

            if (entityVM.ProductId == 0)
            {
                ViewBag.Message = "Producto agregado correctamente.";
                ModelState.Clear();
                return View(new ProductVM());
            }

            // Se recarga desde la BD para reflejar SKUs generados y tallas desactivadas
            var updatedVM = await _productService.GetByIdAsync(entityVM.ProductId);
            ViewBag.Message = "Producto actualizado correctamente.";
            return View(updatedVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var imageName = await _productService.DeleteAsync(id);
            _imageService.DeleteImage(imageName, ImageFolder);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadFormDataAsync()
        {
            ViewBag.Categories = await _productService.GetCategoriesForSelectAsync();
            ViewBag.Colors = await _productService.GetColorsForSelectAsync();
            ViewBag.Sizes = await _productService.GetSizesForSelectAsync();
        }
    }
}
