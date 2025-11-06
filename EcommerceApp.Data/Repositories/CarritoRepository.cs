using EcommerceApp.Data.Interfaces;
using EcommerceApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Repositories
{
    public class CarritoRepository : Repository<Carrito>, ICarritoRepository
    {
        public CarritoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Carrito> GetCarritoActivoByClienteIdAsync(int clienteId)
        {
            return await _dbSet
                .Include(c => c.Items)
                .ThenInclude(i => i.Articulo)
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId && !c.Completado);
        }

        public async Task<Carrito> GetCarritoWithItemsAsync(int carritoId)
        {
            return await _dbSet
                .Include(c => c.Items)
                .ThenInclude(i => i.Articulo)
                .FirstOrDefaultAsync(c => c.Id == carritoId);
        }
    }
}
