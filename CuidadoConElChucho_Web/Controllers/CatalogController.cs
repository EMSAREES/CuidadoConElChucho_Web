using Microsoft.AspNetCore.Mvc;
using CuidadoConElChucho_Web.Services;

namespace CuidadoConElChucho_Web.Controllers
{
    [Route("producto")]
    public class CatalogController(ProductService _productService) : Controller
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetDetailByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
