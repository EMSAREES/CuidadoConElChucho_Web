using CuidadoConElChucho_Web.Context;
using CuidadoConElChucho_Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuidadoConElChucho_Web.Repositories
{
    public class ProductVariationRepository(AppDbContext dbContext) : GenericRepository<ProductVariation>(dbContext)
    {
        public async Task<List<ProductVariation>> GetByProductIdAsync(int productId)
        {
            return await _dbContext.ProductVariations
                .Include(v => v.Color)
                .Include(v => v.Size)
                .Where(v => v.ProductId == productId)
                .ToListAsync();
        }

        public async Task<bool> SkuExistsAsync(string sku)
        {
            return await _dbSet.AnyAsync(v => v.SKU == sku);
        }
    }
}
