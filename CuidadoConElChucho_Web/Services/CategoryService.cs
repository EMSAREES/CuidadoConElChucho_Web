using CuidadoConElChucho_Web.Models;
using CuidadoConElChucho_Web.Repositories;
using CuidadoConElChucho_Web.Entities;

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

        public async Task<bool> AddAsync(CategoryVM categoryVM)
        {
            // Las categorías siempre se almacenan en mayúsculas.
            var categoryName = categoryVM.Name.Trim().ToUpper();

            // Verificar si ya existe.
            var exists = await categoryRepository
                .ExistsByNameAsync(categoryName);

            if (exists)
            {
                return false;
            }

            var category = new Category
            {
                Name = categoryName
            };

            await categoryRepository.AddAsync(category);

            return true;
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
