using CuidadoConElChucho_Web.Models;
using CuidadoConElChucho_Web.Repositories;

namespace CuidadoConElChucho_Web.Services
{
    public class CategoryService(CategoryRepository categoryRepository)
    {
        //private readonly CategoryRepository _categoryRepository;

        //public CategoryService(CategoryRepository categoryRepository)
        //{
        //    _categoryRepository = categoryRepository;
        //}

        public async Task<IEnumerable<CategoryVM>> GetAllAsync()
        {
            var categories = await categoryRepository.GetAllAsync();

            var categoryVMs = categories.Select(item =>
                new CategoryVM
                {
                    CategoryId = item.CategoryId,
                    Name = item.Name
                }).ToList();

            return categoryVMs;
        }
    }

    //public class CategoryService(GenericRepository<Category> _categoryRepository)
    //{
        //public class CategoryService
        //{
        //    private readonly CategoryRepository _categoryRepository;

        //    public async Task<IEnumerable<CategoryVM>> GetAllAsync()
        //    {
        //        var categories = await _categoryRepository.GetAllAsync();

        //        var categoryVMs = categories.Select(item =>
        //            new CategoryVM
        //            {
        //                CategoryId = item.CategoryId,
        //                Name = item.Name
        //            }).ToList();

        //        return categoryVMs;
        //    }
        //}

    //}
}
