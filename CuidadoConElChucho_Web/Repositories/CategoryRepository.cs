using CuidadoConElChucho_Web.Context;
using CuidadoConElChucho_Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuidadoConElChucho_Web.Repositories
{
    public class CategoryRepository(AppDbContext dbContext) : GenericRepository<Category>(dbContext)
    {
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbSet.AnyAsync(c => c.Name == name);
        }
    }

    //public class CategoryRepository: GenericRepository<Category>
    //{
    //    // De aqui pasamos el AppDbContext del CategoryRepository al GenericRepository.
    //    public CategoryRepository(AppDbContext dbContext) : base(dbContext)
    //    {
    //    }

}
