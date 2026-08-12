using Microsoft.AspNetCore.Mvc;
using CuidadoConElChucho_Web.Context;
using CuidadoConElChucho_Web.Models;
using CuidadoConElChucho_Web.Services;

namespace CuidadoConElChucho_Web.Controllers
{
    public class CategoryController(CategoryService _categoryService) : Controller
    {
        // Usar async/await evita bloquear el hilo principal.
        // Con async Task<IActionResult> el controlador libera recursos mientras espera,
        // mientras que usar .Result en un método síncrono bloquea la ejecución y puede causar deadlocks.
        // Siempre es mejor preferir async/await en ASP.NET Core.

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id)
        {
            // Si no llega id (null) o es 0 -> nueva categoría (modelo vacío)
            if (id == null || id == 0)
            {
                return View(new CategoryVM());
            }

            var categoryVM = await _categoryService.GetByIdAsync(id.Value);

            // Si no existe la categoría, mostramos un formulario vacío o redirigimos
            if (categoryVM == null)
            {
                // opción: redirigir a Index
                // return RedirectToAction(nameof(Index));
                return View(new CategoryVM());
            }

            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(CategoryVM entityVM)
        {
            ViewBag.Message = null;

            // Si el modelo NO es válido, volvemos a la vista con el mismo modelo para mostrar errores
            if (!ModelState.IsValid)
            {
                return View(entityVM);
            }

            if (entityVM.CategoryId == 0)
            {
                bool result = await _categoryService.AddAsync(entityVM);

                if (result)
                {
                    ModelState.Clear();
                    entityVM = new CategoryVM(); // limpiar formulario
                    ViewBag.Message = "Categoría agregada correctamente.";
                    return View(entityVM);
                }
                else
                {
                    ViewBag.Message = "La categoría ya existe.";
                    return View(entityVM);
                }
            }
            else
            {
                await _categoryService.EditAsync(entityVM);
                ViewBag.Message = "Categoría actualizada correctamente.";
                return View(entityVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

    //public class CategoryController(AppDbContext _dbContext) : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        var categories = _dbContext.Categories.Select(item =>
    //            new CategoryVM
    //            {
    //                CategoryId = item.CategoryId,
    //                Name = item.Name
    //            }).ToList();

    //        return View(categories);
    //    }
    //}
}
