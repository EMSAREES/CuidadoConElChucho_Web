using CuidadoConElChucho_Web.Context;
using CuidadoConElChucho_Web.Entities;

namespace CuidadoConElChucho_Web.Repositories
{
    public class SizeRepository(AppDbContext dbContext) : GenericRepository<Size>(dbContext)
    {
    }
}
