using EcommerceApp.Entities;
using System.Threading.Tasks;

namespace EcommerceApp.Data.Interfaces
{
    public interface ICarritoRepository : IRepository<Carrito>
    {
        Task<Carrito> GetCarritoActivoByClienteIdAsync(int clienteId);
        Task<Carrito> GetCarritoWithItemsAsync(int carritoId);
    }
}
