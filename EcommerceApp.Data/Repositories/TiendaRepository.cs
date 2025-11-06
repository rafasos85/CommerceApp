using EcommerceApp.Data.Interfaces;
using EcommerceApp.Entities;

namespace EcommerceApp.Data.Repositories
{
    public class TiendaRepository : Repository<Tienda>, ITiendaRepository
    {
        public TiendaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
