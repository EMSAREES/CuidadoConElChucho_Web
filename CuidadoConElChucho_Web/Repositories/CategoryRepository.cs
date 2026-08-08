using CuidadoConElChucho_Web.Context;
using CuidadoConElChucho_Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuidadoConElChucho_Web.Repositories
{
    public class CategoryRepository(AppDbContext dbContext) : GenericRepository<Category>(dbContext)
    {
    }

    //public class CategoryRepository: GenericRepository<Category>
    //{
    //    // De aqui pasamos el AppDbContext del CategoryRepository al GenericRepository.
    //    public CategoryRepository(AppDbContext dbContext) : base(dbContext)
    //    {
    //    }

}
