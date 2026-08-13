using CuidadoConElChucho_Web.Context;
using CuidadoConElChucho_Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuidadoConElChucho_Web.Repositories
{
    public class ProductImageRepository(AppDbContext dbContext) : GenericRepository<ProductImage>(dbContext)
    {
        public async Task<List<ProductImage>> GetByProductIdAsync(int productId)
        {
            return await _dbContext.ProductImages
                .Where(i => i.ProductId == productId)
                .ToListAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<ProductImage> images)
        {
            _dbContext.ProductImages.RemoveRange(images);
            await _dbContext.SaveChangesAsync();
        }
    }
}
