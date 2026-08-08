using Microsoft.EntityFrameworkCore;
using CuidadoConElChucho_Web.Context;

namespace CuidadoConElChucho_Web.Repositories
{
    public abstract class GenericRepository<TEntity> where TEntity : class
    {

        protected readonly AppDbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        protected GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task AditAsync(TEntity entity)
        {
             _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

    //public class GenericRepository<TEntity>(AppDbContext _dbContext) where TEntity : class
    //{
    //    public async Task<IEnumerable<TEntity>> GetAllAsync()
    //    {
    //        return await _dbContext.Set<TEntity>().ToListAsync();
    //    }
    //}
}
