using CuidadoConElChucho_Web.Models;
using CuidadoConElChucho_Web.Repositories;
using CuidadoConElChucho_Web.Entities;

namespace CuidadoConElChucho_Web.Services
{
    public class CategoryService(CategoryRepository _categoryRepository)
    {
        //private readonly CategoryRepository _categoryRepository;

        //public CategoryService(CategoryRepository categoryRepository)
        //{
        //    _categoryRepository = categoryRepository;
        //}

        public async Task<IEnumerable<CategoryVM>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

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
            var exists = await _categoryRepository
                .ExistsByNameAsync(categoryName);

            if (exists)
            {
                return false;
            }

            var entity = new Category
            {
                Name = categoryName
            };

            await _categoryRepository.AddAsync(entity);

            return true;
        }

        public async Task<CategoryVM?> GetByIdAsync(int categoryId)
        {
            var entity = await _categoryRepository.GetByIdAsync(categoryId);
            if (entity == null)
            {
                return null;
            }
            var categoryVM = new CategoryVM
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name
            };
            return categoryVM;
        }

        public async Task<CategoryVM?> GetIdAsync(int id)
        {
            var categoy = await _categoryRepository.GetByIdAsync(id);
            var categoryVM = new CategoryVM();

            if (categoy != null)
            {
                categoryVM.CategoryId = categoy.CategoryId;
                categoryVM.Name = categoy.Name;
            }

            return categoryVM;
        }

        public async Task EditAsync (CategoryVM categoryVM)
        {
            var entity = new Category
            {
                CategoryId = categoryVM.CategoryId,
                Name = categoryVM.Name.Trim().ToUpper()
            };
            await _categoryRepository.AditAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(category!);

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
