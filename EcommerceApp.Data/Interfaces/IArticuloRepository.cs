using EcommerceApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Interfaces
{
    public interface IArticuloRepository : IRepository<Articulo>
    {
        Task<Articulo> GetByCodigoAsync(string codigo);
        Task<IEnumerable<Articulo>> GetByTiendaIdAsync(int tiendaId);
    }
}
