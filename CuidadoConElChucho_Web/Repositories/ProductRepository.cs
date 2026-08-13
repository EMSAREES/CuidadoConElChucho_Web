using CuidadoConElChucho_Web.Context;
using CuidadoConElChucho_Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuidadoConElChucho_Web.Repositories
{
    public class ProductRepository(AppDbContext dbContext) : GenericRepository<Product>(dbContext)
    {
        public async Task<List<Product>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Variations).ThenInclude(v => v.Color)
                .OrderByDescending(p => p.ProductId)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdWithDetailsAsync(int productId)
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Variations).ThenInclude(v => v.Color)
                .Include(p => p.Variations).ThenInclude(v => v.Size)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<bool> ExistsByNameAsync(string name, int excludeId = 0)
        {
            return await _dbSet.AnyAsync(p =>
                p.Name.ToLower() == name.ToLower() && p.ProductId != excludeId);
        }
    }
}
