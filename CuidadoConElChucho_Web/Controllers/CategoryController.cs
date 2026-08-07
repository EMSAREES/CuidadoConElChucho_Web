using Microsoft.AspNetCore.Mvc;
using CuidadoConElChucho_Web.Context;
using CuidadoConElChucho_Web.Models;

namespace CuidadoConElChucho_Web.Controllers
{
    public class CategoryController(AppDbContext _dbContext) : Controller
    {
        public IActionResult Index()
        {
            var categories = _dbContext.Categories.Select(item =>
                new CategoryVM
                {
                    CategoryId = item.CategoryId,
                    Name = item.Name
                }).ToList();

            return View(categories);
        }
    }
}
