using EcommerceApp.Data.Interfaces;
using EcommerceApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Repositories
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        public ArticuloRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Articulo> GetByCodigoAsync(string codigo)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Codigo == codigo);
        }

        public async Task<IEnumerable<Articulo>> GetByTiendaIdAsync(int tiendaId)
        {
            return await _context.ArticuloTiendas
                .Where(at => at.TiendaId == tiendaId)
                .Include(at => at.Articulo)
                .Select(at => at.Articulo)
                .ToListAsync();
        }
    }
}
