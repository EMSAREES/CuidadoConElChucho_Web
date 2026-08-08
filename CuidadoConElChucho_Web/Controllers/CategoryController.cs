using Microsoft.AspNetCore.Mvc;
using CuidadoConElChucho_Web.Context;
using CuidadoConElChucho_Web.Models;
using CuidadoConElChucho_Web.Services;

namespace CuidadoConElChucho_Web.Controllers
{
    public class CategoryController(CategoryService _categoryService) : Controller
    {
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllAsync().Result;
            return View(categories);
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
