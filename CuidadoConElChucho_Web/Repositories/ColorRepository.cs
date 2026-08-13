using CuidadoConElChucho_Web.Context;
using CuidadoConElChucho_Web.Entities;

namespace CuidadoConElChucho_Web.Repositories
{
    public class ColorRepository(AppDbContext dbContext) : GenericRepository<Color>(dbContext)
    {
    }
}
